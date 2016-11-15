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
    public class WareHouseApiTest
    {
        private string baseUrl;
        private string insertAction;
        private string updateAction;
        private string deleteAction;
        private string getAllAction;
        private string getByIdAction;
        public WareHouseApiTest()
        {
            baseUrl = @"http://lasmargaritasdev.azurewebsites.net/";
            if (ConfigurationManager.AppSettings["baseUrl"] != null)
            {
                baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            }
            insertAction = "WareHouse/Add";
            updateAction = "WareHouse/Update";
            deleteAction = "WareHouse/Delete";
            getAllAction = "WareHouse/GetAll";
            getByIdAction = "WareHouse/GetById";
        }

        [TestMethod]
        public void TestInsertUpdateAndGetWareHouse()
        {
            //Get token
            Token token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3");
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //Test insert
            WareHouse wareHouse = WareHouseHelper.CreateDummyWareHouse();
            string jsonTicket = javaScriptSerializer.Serialize(wareHouse);
            //Post add ticket
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            HttpResponseMessage response = client.PostAsJsonAsync(insertAction, wareHouse).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            WareHouseResponse wareHouseResponse = response.Content.ReadAsAsync<WareHouseResponse>().Result;
            Assert.IsTrue(wareHouseResponse.Success);
            Assert.IsTrue(wareHouseResponse != null);
            Assert.IsTrue(wareHouseResponse.WareHouse.Id > 0);
            //get by id
            string getByIdUrl = string.Format("{0}?id={1}", getByIdAction, wareHouseResponse.WareHouse.Id.ToString());
            response = client.GetAsync(getByIdUrl).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            GetWareHouseResponse getWareHouseResponse = response.Content.ReadAsAsync<GetWareHouseResponse>().Result;
            Assert.IsTrue(getWareHouseResponse.Success);
            Assert.IsTrue(getWareHouseResponse.WareHouses.Count == 1);
            Assert.IsTrue(getWareHouseResponse.WareHouses.ElementAt(0).Id == wareHouseResponse.WareHouse.Id);
            //get all 
            response = client.GetAsync(getAllAction).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            getWareHouseResponse = response.Content.ReadAsAsync<GetWareHouseResponse>().Result;
            Assert.IsTrue(getWareHouseResponse.Success);
            WareHouse wareHouseFound = (from produc in getWareHouseResponse.WareHouses where produc.Id == wareHouseResponse.WareHouse.Id select produc).FirstOrDefault();
            Assert.IsTrue(wareHouseFound != null);
            //test update 
            wareHouse.Id = wareHouseResponse.WareHouse.Id;
            wareHouse.Address = "UpdatedAddress";
            wareHouse.Name = "UpdatedName";
            response = client.PostAsJsonAsync(updateAction, wareHouse).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            wareHouseResponse = response.Content.ReadAsAsync<WareHouseResponse>().Result;
            Assert.IsTrue(wareHouseResponse.Success);
            //Get ticket again and check it was updated
            response = client.GetAsync(getByIdUrl).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            getWareHouseResponse = response.Content.ReadAsAsync<GetWareHouseResponse>().Result;
            Assert.IsTrue(getWareHouseResponse.Success);
            Assert.IsTrue(getWareHouseResponse.WareHouses.Count == 1);
            Assert.IsTrue(getWareHouseResponse.WareHouses.ElementAt(0).Address == "UpdatedAddress");
            Assert.IsTrue(getWareHouseResponse.WareHouses.ElementAt(0).Name == "UpdatedName");
            //test delete
            response = client.PostAsJsonAsync(deleteAction, new IdModel(wareHouseResponse.WareHouse.Id)).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            wareHouseResponse = response.Content.ReadAsAsync<WareHouseResponse>().Result;
            Assert.IsTrue(wareHouseResponse.Success);
            //Get ticket again and check it is not found
            response = client.GetAsync(getByIdUrl).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            getWareHouseResponse = response.Content.ReadAsAsync<GetWareHouseResponse>().Result;
            Assert.IsTrue(getWareHouseResponse.Success);
            Assert.IsTrue(getWareHouseResponse.WareHouses.Count == 0);

        }

        public void TestInsertUpdateErrors()
        {
            Token token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3");
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //Test insert
            WareHouse wareHouse = new WareHouse();
            string jsonTicket = javaScriptSerializer.Serialize(wareHouse);
            //Post add ticket
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            //Test insert
            HttpResponseMessage response = client.PostAsJsonAsync(insertAction, wareHouse).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            WareHouseResponse wareHouseResponse = response.Content.ReadAsAsync<WareHouseResponse>().Result;
            Assert.IsFalse(wareHouseResponse.Success);
            Assert.IsTrue(wareHouseResponse.ErrorCode.HasFlag(WareHouseError.InvalidName));
            Assert.IsTrue(wareHouseResponse.ErrorCode.HasFlag(WareHouseError.InvalidAddress));
            


            //Test update
            response = client.PostAsJsonAsync(updateAction, wareHouse).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            wareHouseResponse = response.Content.ReadAsAsync<WareHouseResponse>().Result;
            Assert.IsFalse(wareHouseResponse.Success);
            Assert.IsTrue(wareHouseResponse.ErrorCode.HasFlag(WareHouseError.InvalidName));
            Assert.IsTrue(wareHouseResponse.ErrorCode.HasFlag(WareHouseError.InvalidAddress));
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
            WareHouseResponse wareHouseResponse = response.Content.ReadAsAsync<WareHouseResponse>().Result;
            Assert.IsFalse(wareHouseResponse.Success);
            Assert.IsTrue(wareHouseResponse.ErrorCode.HasFlag(WareHouseError.InvalidId));
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
            GetWareHouseResponse getWareHouseResponse = response.Content.ReadAsAsync<GetWareHouseResponse>().Result;
            Assert.IsFalse(getWareHouseResponse.Success);
            Assert.IsTrue(getWareHouseResponse.ErrorCode.HasFlag(WareHouseError.InvalidId));

        }
    }
}
