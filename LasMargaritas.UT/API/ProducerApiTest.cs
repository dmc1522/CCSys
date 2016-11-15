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
    public class ProducerApiTest
    {
        private string baseUrl;
        private string insertAction;
        private string updateAction;
        private string deleteAction;
        private string getAllAction;
        private string getByIdAction;
        private string getSelectableModelsAction;
        public ProducerApiTest()
        {
            baseUrl = @"http://lasmargaritasdev.azurewebsites.net/";
            if (ConfigurationManager.AppSettings["baseUrl"] != null)
            {
                baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            }
            insertAction = "Producer/Add";
            updateAction = "Producer/Update";
            deleteAction = "Producer/Delete";
            getAllAction = "Producer/GetAll";
            getByIdAction = "Producer/GetById";
            getSelectableModelsAction = "Producer/GetSelectableModels";
        }

        [TestMethod]
        public void TestInsertUpdateAndGetProducer()
        {
            //Get token
            Token token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3");
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //Test insert
            Producer producer = ProducerHelper.CreateDummyProducer();
            string jsonTicket = javaScriptSerializer.Serialize(producer);
            //Post add ticket
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            HttpResponseMessage response = client.PostAsJsonAsync(insertAction, producer).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            ProducerResponse producerResponse = response.Content.ReadAsAsync<ProducerResponse>().Result;
            Assert.IsTrue(producerResponse.Success);
            Assert.IsTrue(producerResponse != null);
            Assert.IsTrue(producerResponse.Producer.Id > 0);
            //get by id
            string getByIdUrl = string.Format("{0}?id={1}", getByIdAction, producerResponse.Producer.Id.ToString());
            response = client.GetAsync(getByIdUrl).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            GetProducerResponse getProducerResponse = response.Content.ReadAsAsync<GetProducerResponse>().Result;
            Assert.IsTrue(getProducerResponse.Success);
            Assert.IsTrue(getProducerResponse.Producers.Count == 1);
            Assert.IsTrue(getProducerResponse.Producers.ElementAt(0).Id == producerResponse.Producer.Id);
            //get all 
            response = client.GetAsync(getAllAction).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            getProducerResponse = response.Content.ReadAsAsync<GetProducerResponse>().Result;
            Assert.IsTrue(getProducerResponse.Success);
            Producer producerFound = (from produc in getProducerResponse.Producers where produc.Id == producerResponse.Producer.Id select produc).FirstOrDefault();
            Assert.IsTrue(producerFound != null);
            //test update 
            producer.Id = producerResponse.Producer.Id;
            producer.RFC = "UpdatedProducerRFC";
            producer.GenderId = 2;
            response = client.PostAsJsonAsync(updateAction, producer).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            producerResponse = response.Content.ReadAsAsync<ProducerResponse>().Result;
            Assert.IsTrue(producerResponse.Success);
            //Get ticket again and check it was updated
            response = client.GetAsync(getByIdUrl).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            getProducerResponse = response.Content.ReadAsAsync<GetProducerResponse>().Result;
            Assert.IsTrue(getProducerResponse.Success);
            Assert.IsTrue(getProducerResponse.Producers.Count == 1);
            Assert.IsTrue(getProducerResponse.Producers.ElementAt(0).GenderId == 2);
            Assert.IsTrue(getProducerResponse.Producers.ElementAt(0).RFC == "UpdatedProducerRFC");
            //test delete
            response = client.PostAsJsonAsync(deleteAction, new IdModel(producerResponse.Producer.Id)).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            producerResponse = response.Content.ReadAsAsync<ProducerResponse>().Result;
            Assert.IsTrue(producerResponse.Success);
            //Get ticket again and check it is not found
            response = client.GetAsync(getByIdUrl).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            getProducerResponse = response.Content.ReadAsAsync<GetProducerResponse>().Result;
            Assert.IsTrue(getProducerResponse.Success);
            Assert.IsTrue(getProducerResponse.Producers.Count == 0);

        }

        public void TestInsertUpdateErrors()
        {
            Token token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3");
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //Test insert
            Producer producer = new Producer();
            string jsonTicket = javaScriptSerializer.Serialize(producer);
            //Post add ticket
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            //Test insert
            HttpResponseMessage response = client.PostAsJsonAsync(insertAction, producer).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            ProducerResponse producerResponse = response.Content.ReadAsAsync<ProducerResponse>().Result;
            Assert.IsFalse(producerResponse.Success);
            Assert.IsTrue(producerResponse.ErrorCode.HasFlag(ProducerError.InvalidName));
            Assert.IsTrue(producerResponse.ErrorCode.HasFlag(ProducerError.InvalidCivilStatus));
            Assert.IsTrue(producerResponse.ErrorCode.HasFlag(ProducerError.InvalidGender));
            
            
            //Test update
            response = client.PostAsJsonAsync(updateAction, producer).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            producerResponse = response.Content.ReadAsAsync<ProducerResponse>().Result;
            Assert.IsFalse(producerResponse.Success);
            Assert.IsTrue(producerResponse.ErrorCode.HasFlag(ProducerError.InvalidName));
            Assert.IsTrue(producerResponse.ErrorCode.HasFlag(ProducerError.InvalidCivilStatus));
            Assert.IsTrue(producerResponse.ErrorCode.HasFlag(ProducerError.InvalidGender));
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
            ProducerResponse producerResponse = response.Content.ReadAsAsync<ProducerResponse>().Result;
            Assert.IsFalse(producerResponse.Success);
            Assert.IsTrue(producerResponse.ErrorCode.HasFlag(ProducerError.InvalidId));
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
            GetProducerResponse getProducerResponse = response.Content.ReadAsAsync<GetProducerResponse>().Result;
            Assert.IsFalse(getProducerResponse.Success);
            Assert.IsTrue(getProducerResponse.ErrorCode.HasFlag(ProducerError.InvalidId));

        }
        [TestMethod]
        public void TestGetBasicModels()
        {
            Token token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3");
            //get all 
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            HttpResponseMessage response = client.GetAsync(getSelectableModelsAction).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
            Assert.IsTrue(getSelectableModelResponse.Success);
            Assert.IsTrue(getSelectableModelResponse.SelectableModels.Count > 0);
        }

    }
}
