using LasMargaritas.BL;
using LasMargaritas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

namespace LasMargaritas.ULT
{
    [TestClass]
    public class SettlementApiTest
    {
        private string baseUrl;
        private string insertAction;
        private string insertWeightTicketAction;
        private string insertPaymentAction;
        private string getPaymentsAction;
        private string removePaymentsAction;
        private string updateAction;
        private string deleteAction;
        private string getAllAction;
        private string getByIdAction;
        private string getReportAction;
        private Token token;
        public SettlementApiTest()
        {
            baseUrl = @"http://lasmargaritasdev.azurewebsites.net/";
            if (ConfigurationManager.AppSettings["baseUrl"] != null)
            {
                baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            }
            insertAction = "Settlement/Add";
            insertWeightTicketAction = "Settlement/Add";
            insertPaymentAction = "Settlement/AddSettlementPayment";
            removePaymentsAction = "Settlement/RemoveAllSettlementPayments";
            getPaymentsAction = "Settlement/GetSettlementPayments";
            updateAction = "Settlement/Update";
            deleteAction = "Settlement/Delete";
            getAllAction = "Settlement/GetAll";
            getByIdAction = "Settlement/GetById";
            getReportAction = "Settlement/GetWeightTicketsReport";
            token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3");
        }

        [TestMethod]
        public void TestInsertUpdateGetAndDeleteSettlement()
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            SettlementResponse settlementResponse = new SettlementResponse();
            //Test insert            
            Settlement settlement = new Settlement();
            settlement.CicleId = 1; //Assume we have a cicle with ID 1
            settlement.ProducerId = 1; //Assume we have a producer with ID 1
            settlement.CashAdvancesTotal = 1000;
            settlement.Date = DateTime.Now;
            settlement.CreditsTotal = 2000;
            settlement.Paid = false;
            settlement.WeightTicketsTotal = 2500;
            settlement.Total = 5000;
            settlement.User = "0ba16e0c-a1e4-44c1-8a94-e1070d03de10"; //Assume we have a user with this ID
            settlement.WeightTicketsIds = new System.Collections.ObjectModel.Collection<int>() { 1 };
            string jsonSettlement = javaScriptSerializer.Serialize(settlement);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            HttpResponseMessage response = client.PostAsJsonAsync(insertAction, settlement).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            settlementResponse = response.Content.ReadAsAsync<SettlementResponse>().Result;
            Assert.IsTrue(settlementResponse.Success);
            //Test update
            settlement = settlementResponse.Settlement;
            settlement.Paid = true;
            response = client.PostAsJsonAsync(updateAction, settlement).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            settlementResponse = response.Content.ReadAsAsync<SettlementResponse>().Result;
            Assert.IsTrue(settlementResponse.Success);
            //TODO VERIFY TICKETS CHANGED PAID STATUS
            //Test get all
            GetSettlementResponse getSettlementResponse = new GetSettlementResponse();
            response = client.GetAsync(getAllAction).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            getSettlementResponse = response.Content.ReadAsAsync<GetSettlementResponse>().Result;
            Assert.IsTrue(getSettlementResponse.Success);
            Assert.IsTrue(getSettlementResponse.Settlements.Count > 0);
            //Test get by Id
            string getByIdUrl = string.Format("{0}?id={1}", getByIdAction, settlement.Id.ToString());
            response = client.GetAsync(getByIdUrl).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            getSettlementResponse = response.Content.ReadAsAsync<GetSettlementResponse>().Result;
            Assert.IsTrue(getSettlementResponse.Success);
            Assert.IsTrue(getSettlementResponse.Settlements.Count > 0);
            //Test delete
            response = client.PostAsJsonAsync(deleteAction, new IdModel(settlementResponse.Settlement.Id)).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            settlementResponse = response.Content.ReadAsAsync<SettlementResponse>().Result;
            Assert.IsTrue(response.IsSuccessStatusCode);

        }

        [TestMethod]
        public void TestAddAndRemovePayment()
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            SettlementPayment payment = new SettlementPayment();
            int settlementId = InsertDummySettlement();
            payment.SettlementId = settlementId;
            payment.Description = "Test description";
            payment.CheckOrVoucher = "101";
            payment.Date = DateTime.Now;
            payment.PaymentType = PaymentType.Cash;
            payment.Total = 100;
            payment.User = "0ba16e0c-a1e4-44c1-8a94-e1070d03de10"; //Assume we have a user with this ID
            string jsonSettlement = javaScriptSerializer.Serialize(payment);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            SettlementPaymentResponse settlementPaymentResponse = new SettlementPaymentResponse();
            //Add payment
            HttpResponseMessage response = client.PostAsJsonAsync(insertPaymentAction, payment).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            settlementPaymentResponse = response.Content.ReadAsAsync<SettlementPaymentResponse>().Result;
            Assert.IsTrue(settlementPaymentResponse.Success);
            Assert.IsTrue(settlementPaymentResponse.Payment.Id > 0);
            //Get payments for settlement
            GetSettlementPaymentResponse getSettlementPaymentResponse = new GetSettlementPaymentResponse();
            string getAction = string.Format("{0}?settlementId={1}", getPaymentsAction, settlementId);
            response = client.GetAsync(getAction).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            getSettlementPaymentResponse = response.Content.ReadAsAsync<GetSettlementPaymentResponse>().Result;
            Assert.IsTrue(getSettlementPaymentResponse.Success);
            Assert.IsTrue(getSettlementPaymentResponse.Payments.Count > 0);
            //Remove all payments
            //Test delete
            response = client.PostAsJsonAsync(removePaymentsAction, new IdModel(settlementId)).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            settlementPaymentResponse = response.Content.ReadAsAsync<SettlementPaymentResponse>().Result;
            Assert.IsTrue(settlementPaymentResponse.Success);
            //Get payments, expecting 0 
            getSettlementPaymentResponse = new GetSettlementPaymentResponse();
            response = client.GetAsync(getAction).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            getSettlementPaymentResponse = response.Content.ReadAsAsync<GetSettlementPaymentResponse>().Result;
            Assert.IsTrue(getSettlementPaymentResponse.Success);
            Assert.IsTrue(getSettlementPaymentResponse.Payments.Count == 0);
            //delete the dummy settlement
            response = client.PostAsJsonAsync(deleteAction, new IdModel(settlementId)).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            SettlementResponse settlementResponse = response.Content.ReadAsAsync<SettlementResponse>().Result;
            Assert.IsTrue(response.IsSuccessStatusCode);

        }

        private int InsertDummySettlement()
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            SettlementResponse settlementResponse = new SettlementResponse();
            //Test insert            
            Settlement settlement = new Settlement();
            settlement.CicleId = 1; //Assume we have a cicle with ID 1
            settlement.ProducerId = 1; //Assume we have a producer with ID 1
            settlement.CashAdvancesTotal = 1000;
            settlement.Date = DateTime.Now;
            settlement.CreditsTotal = 2000;
            settlement.Paid = false;
            settlement.WeightTicketsTotal = 2500;
            settlement.Total = 5000;
            settlement.User = "0ba16e0c-a1e4-44c1-8a94-e1070d03de10"; //Assume we have a user with this ID
            settlement.WeightTicketsIds = new System.Collections.ObjectModel.Collection<int>() { 1 };
            string jsonSettlement = javaScriptSerializer.Serialize(settlement);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            HttpResponseMessage response = client.PostAsJsonAsync(insertAction, settlement).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            settlementResponse = response.Content.ReadAsAsync<SettlementResponse>().Result;
            Assert.IsTrue(settlementResponse.Success);
            return settlementResponse.Settlement.Id;
        }
    }
}

