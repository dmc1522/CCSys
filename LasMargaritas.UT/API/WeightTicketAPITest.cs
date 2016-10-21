using LasMargaritas.BL;
using LasMargaritas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

namespace LasMargaritas.ULT
{
    [TestClass]
    public class WeightTicketApiTest
    {
        private string baseUrl;
        private string insertAction;
        private string updateAction;
        private string deleteAction;
        private string getAllAction;
        private string getByIdAction;
        public WeightTicketApiTest()
        {
            baseUrl = @"http://lasmargaritas.azurewebsites.net/";
            insertAction = "WeightTicket/Add";
            updateAction = "WeightTicket/Update";
            deleteAction = "WeightTicket/Delete";
            getAllAction = "WeightTicket/GetAll";
            getByIdAction = "WeightTicket/GetById";
        }
        private void FillTicket(WeightTicket weightTicket)
        {            
            weightTicket.Amount = 10000;
            weightTicket.ApplyDrying = true;
            weightTicket.ApplyHumidity = true;
            weightTicket.ApplyImpurities = true;
            weightTicket.BrokenGrainDiscount = 10;
            weightTicket.CicleId = 1;
            weightTicket.CrashedGrainDiscount = 10;
            weightTicket.DamagedGrainDiscount = 10;
            weightTicket.Driver = "Driver";
            weightTicket.DryingDiscount = 10;
            weightTicket.EntranceDate = DateTime.Now;
            weightTicket.EntranceNetWeight = 1000;
            weightTicket.EntranceWeigher = "Weigher";
            weightTicket.EntranceWeightKg = 1500;
            weightTicket.ExitWeightKg = 1000;
            weightTicket.ExitDate = DateTime.Now;
            weightTicket.ExitNetWeight = 200;
            weightTicket.ExitWeigher = "Weigher";
            weightTicket.Folio = "Folio" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            weightTicket.Humidity = 10;
            weightTicket.HumidityDiscount = 10;
            weightTicket.Impurities = 10;
            weightTicket.ImpuritiesDiscount = 10;
            weightTicket.NetWeight = 400;
            weightTicket.Number = "Number" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            weightTicket.Paid = false;
            weightTicket.Plate = "Plate";
            weightTicket.Price = 10;
            weightTicket.ProducerId = 1;
            weightTicket.ProductId = 1;
            weightTicket.SmallGrainDiscount = 10;
            weightTicket.StoreTs = DateTime.Now;
            weightTicket.TotalDiscount = 50;
            weightTicket.TotalToPay = 100;
            weightTicket.UpdateTs = DateTime.Now;
            weightTicket.UserId = "35d360f3-c296-4113-ab34-9b91fe729c18";
            weightTicket.WarehouseId = 1;
        }

        [TestMethod]
        public void TestInsertUpdateAndGetWeightTicket()
        {
            //Get token
            Token token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3");
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //Test insert
            WeightTicket weightTicket = new WeightTicket();
            FillTicket(weightTicket);
            string jsonTicket = javaScriptSerializer.Serialize(weightTicket);
            //Post add ticket
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            HttpResponseMessage response = client.PostAsJsonAsync(insertAction, weightTicket).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            WeightTicketResponse weightTicketResponse = response.Content.ReadAsAsync<WeightTicketResponse>().Result;
            Assert.IsTrue(weightTicketResponse.Success);
            Assert.IsTrue(weightTicketResponse != null);
            Assert.IsTrue(weightTicketResponse.WeightTicket.Id > 0);
            //get by id
            string getByIdUrl = string.Format("{0}?id={1}", getByIdAction, weightTicketResponse.WeightTicket.Id.ToString());
            response = client.GetAsync(getByIdUrl).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            GetWeightTicketResponse getWeightTicketResponse = response.Content.ReadAsAsync<GetWeightTicketResponse>().Result;
            Assert.IsTrue(getWeightTicketResponse.Success);
            Assert.IsTrue(getWeightTicketResponse.WeightTickets.Count == 1);
            Assert.IsTrue(getWeightTicketResponse.WeightTickets.ElementAt(0).Id == weightTicketResponse.WeightTicket.Id);
            //get all 
            response = client.GetAsync(getAllAction).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            getWeightTicketResponse = response.Content.ReadAsAsync<GetWeightTicketResponse>().Result;
            Assert.IsTrue(getWeightTicketResponse.Success);
            WeightTicket ticketFound = (from ticket in getWeightTicketResponse.WeightTickets where ticket.Id == weightTicketResponse.WeightTicket.Id select ticket).FirstOrDefault();
            Assert.IsTrue(ticketFound != null);
            //test update 
            weightTicket.Id = weightTicketResponse.WeightTicket.Id;                           
            weightTicket.Amount = 20;
            weightTicket.ApplyDrying = false; //Todo check other properties
            response = client.PostAsJsonAsync(updateAction, weightTicket).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            weightTicketResponse = response.Content.ReadAsAsync<WeightTicketResponse>().Result;
            Assert.IsTrue(weightTicketResponse.Success);
            //Get ticket again and check it was updated
            response = client.GetAsync(getByIdUrl).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            getWeightTicketResponse = response.Content.ReadAsAsync<GetWeightTicketResponse>().Result;
            Assert.IsTrue(getWeightTicketResponse.Success);
            Assert.IsTrue(getWeightTicketResponse.WeightTickets.Count == 1);
            Assert.IsTrue(getWeightTicketResponse.WeightTickets.ElementAt(0).Amount == 20);
            Assert.IsFalse(getWeightTicketResponse.WeightTickets.ElementAt(0).ApplyDrying);
            //test delete
            response = client.PostAsJsonAsync(deleteAction, new IdModel(weightTicketResponse.WeightTicket.Id)).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            weightTicketResponse = response.Content.ReadAsAsync<WeightTicketResponse>().Result;
            Assert.IsTrue(weightTicketResponse.Success);            
            //Get ticket again and check it is not found
            response = client.GetAsync(getByIdUrl).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            getWeightTicketResponse = response.Content.ReadAsAsync<GetWeightTicketResponse>().Result;
            Assert.IsTrue(getWeightTicketResponse.Success);
            Assert.IsTrue(getWeightTicketResponse.WeightTickets.Count == 0);

        }
        
        [TestMethod]
        public void TestInsertUpdateErrors()
        {
            Token token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3");  
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //Test insert
            WeightTicket weightTicket = new WeightTicket();        
            string jsonTicket = javaScriptSerializer.Serialize(weightTicket);
            //Post add ticket
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            //Test insert
            HttpResponseMessage response = client.PostAsJsonAsync(insertAction, weightTicket).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            WeightTicketResponse weightTicketResponse = response.Content.ReadAsAsync<WeightTicketResponse>().Result;
            Assert.IsFalse(weightTicketResponse.Success);                
            Assert.IsTrue(weightTicketResponse.ErrorCode.HasFlag(WeightTicketError.InvalidCicle));
            Assert.IsTrue(weightTicketResponse.ErrorCode.HasFlag(WeightTicketError.InvalidFolio));
            Assert.IsTrue(weightTicketResponse.ErrorCode.HasFlag(WeightTicketError.InvalidProducer));
            Assert.IsTrue(weightTicketResponse.ErrorCode.HasFlag(WeightTicketError.InvalidWareHouse));
            Assert.IsTrue(weightTicketResponse.ErrorCode.HasFlag(WeightTicketError.InvalidProduct));
            //Test update
            response = client.PostAsJsonAsync(updateAction, weightTicket).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            weightTicketResponse = response.Content.ReadAsAsync<WeightTicketResponse>().Result;
            Assert.IsFalse(weightTicketResponse.Success);
            Assert.IsTrue(weightTicketResponse.ErrorCode.HasFlag(WeightTicketError.InvalidCicle));
            Assert.IsTrue(weightTicketResponse.ErrorCode.HasFlag(WeightTicketError.InvalidFolio));
            Assert.IsTrue(weightTicketResponse.ErrorCode.HasFlag(WeightTicketError.InvalidProducer));
            Assert.IsTrue(weightTicketResponse.ErrorCode.HasFlag(WeightTicketError.InvalidWareHouse));
            Assert.IsTrue(weightTicketResponse.ErrorCode.HasFlag(WeightTicketError.InvalidProduct));
        }
        
        [TestMethod]
        public void TestDeleteErrors()
        {
            Token token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3"); 
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();            
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            //Test delete
            HttpResponseMessage response = client.PostAsJsonAsync(deleteAction, new IdModel(0)).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            WeightTicketResponse weightTicketResponse = response.Content.ReadAsAsync<WeightTicketResponse>().Result;
            Assert.IsFalse(weightTicketResponse.Success);
            Assert.IsTrue(weightTicketResponse.ErrorCode.HasFlag(WeightTicketError.InvalidId));            
        }

        [TestMethod]
        public void TestGetErrors()
        {
            Token token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3");
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            //Test get by id            
            string getByIdUrl = string.Format("{0}?id={1}", getByIdAction, 0);
            HttpResponseMessage response = client.GetAsync(getByIdUrl).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            GetWeightTicketResponse getWeightTicketResponse = response.Content.ReadAsAsync<GetWeightTicketResponse>().Result;
            Assert.IsFalse(getWeightTicketResponse.Success);
            Assert.IsTrue(getWeightTicketResponse.ErrorCode.HasFlag(WeightTicketError.InvalidId));
            
        }
    }
}

