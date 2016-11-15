using LasMargaritas.BL;
using LasMargaritas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

namespace LasMargaritas.ULT
{
    public class CicleApiTest
    {
        private string baseUrl;
        private string insertAction;
        private string updateAction;
        private string deleteAction;
        private string getAllAction;
        private string getByIdAction;
        public CicleApiTest()
        {
            baseUrl = @"http://lasmargaritas.azurewebsites.net/";
            insertAction = "Cicle/Add";
            updateAction = "Cicle/Update";
            deleteAction = "Cicle/Delete";
            getAllAction = "Cicle/GetAll";
            getByIdAction = "Cicle/GetById";
        }

        [TestMethod]
        public void TestInsertUpdateAndGetCicle()
        {
            //Get token
            Token token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3");
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //Test insert
            Cicle cicle = CicleHelper.CreateDummyCicle();
            string jsonTicket = javaScriptSerializer.Serialize(cicle);
            //Post add ticket
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            HttpResponseMessage response = client.PostAsJsonAsync(insertAction, cicle).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            CicleResponse cicleResponse = response.Content.ReadAsAsync<CicleResponse>().Result;
            Assert.IsTrue(cicleResponse.Success);
            Assert.IsTrue(cicleResponse != null);
            Assert.IsTrue(cicleResponse.Cicle.Id > 0);
            //get by id
            string getByIdUrl = string.Format("{0}?id={1}", getByIdAction, cicleResponse.Cicle.Id.ToString());
            response = client.GetAsync(getByIdUrl).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            GetCicleResponse getCicleResponse = response.Content.ReadAsAsync<GetCicleResponse>().Result;
            Assert.IsTrue(getCicleResponse.Success);
            Assert.IsTrue(getCicleResponse.Cicles.Count == 1);
            Assert.IsTrue(getCicleResponse.Cicles.ElementAt(0).Id == cicleResponse.Cicle.Id);
            //get all 
            response = client.GetAsync(getAllAction).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            getCicleResponse = response.Content.ReadAsAsync<GetCicleResponse>().Result;
            Assert.IsTrue(getCicleResponse.Success);
            Cicle cicleFound = (from aCicle in getCicleResponse.Cicles where aCicle.Id == cicleResponse.Cicle.Id select aCicle).FirstOrDefault();
            Assert.IsTrue(cicleFound != null);
            //test update 
            cicle.Id = cicleResponse.Cicle.Id;
            cicle.Name = "CicleNameUpdated";
            cicle.AmountPerHectarea = 2;
            response = client.PostAsJsonAsync(updateAction, cicle).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            cicleResponse = response.Content.ReadAsAsync<CicleResponse>().Result;
            Assert.IsTrue(cicleResponse.Success);
            //Get ticket again and check it was updated
            response = client.GetAsync(getByIdUrl).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            getCicleResponse = response.Content.ReadAsAsync<GetCicleResponse>().Result;
            Assert.IsTrue(getCicleResponse.Success);
            Assert.IsTrue(getCicleResponse.Cicles.Count == 1);
            Assert.IsTrue(getCicleResponse.Cicles.ElementAt(0).AmountPerHectarea == 2);
            Assert.IsTrue(getCicleResponse.Cicles.ElementAt(0).Name == "CicleNameUpdated");
            //test delete
            response = client.PostAsJsonAsync(deleteAction, new IdModel(cicleResponse.Cicle.Id)).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            cicleResponse = response.Content.ReadAsAsync<CicleResponse>().Result;
            Assert.IsTrue(cicleResponse.Success);
            //Get ticket again and check it is not found
            response = client.GetAsync(getByIdUrl).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            getCicleResponse = response.Content.ReadAsAsync<GetCicleResponse>().Result;
            Assert.IsTrue(getCicleResponse.Success);
            Assert.IsTrue(getCicleResponse.Cicles.Count == 0);

        }

        public void TestInsertUpdateErrors()
        {
            Token token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3");
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //Test insert
            Cicle cicle = new Cicle();
            string jsonTicket = javaScriptSerializer.Serialize(cicle);
            //Post add ticket
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            //Test insert
            HttpResponseMessage response = client.PostAsJsonAsync(insertAction, cicle).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            CicleResponse cicleResponse = response.Content.ReadAsAsync<CicleResponse>().Result;
            Assert.IsFalse(cicleResponse.Success);
            Assert.IsTrue(cicleResponse.ErrorCode.HasFlag(CicleError.InvalidAmount));
            Assert.IsTrue(cicleResponse.ErrorCode.HasFlag(CicleError.InvalidDateZone1));
            Assert.IsTrue(cicleResponse.ErrorCode.HasFlag(CicleError.InvalidDateZone2));
            Assert.IsTrue(cicleResponse.ErrorCode.HasFlag(CicleError.InvalidIsClosed));
            Assert.IsTrue(cicleResponse.ErrorCode.HasFlag(CicleError.InvalidStartDate));
            Assert.IsTrue(cicleResponse.ErrorCode.HasFlag(CicleError.InvalidName));
            

            //Test update
            response = client.PostAsJsonAsync(updateAction, cicle).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            cicleResponse = response.Content.ReadAsAsync<CicleResponse>().Result;
            Assert.IsFalse(cicleResponse.Success);
            Assert.IsTrue(cicleResponse.ErrorCode.HasFlag(CicleError.InvalidAmount));
            Assert.IsTrue(cicleResponse.ErrorCode.HasFlag(CicleError.InvalidDateZone1));
            Assert.IsTrue(cicleResponse.ErrorCode.HasFlag(CicleError.InvalidDateZone2));
            Assert.IsTrue(cicleResponse.ErrorCode.HasFlag(CicleError.InvalidIsClosed));
            Assert.IsTrue(cicleResponse.ErrorCode.HasFlag(CicleError.InvalidStartDate));
            Assert.IsTrue(cicleResponse.ErrorCode.HasFlag(CicleError.InvalidName));

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
            CicleResponse cicleResponse = response.Content.ReadAsAsync<CicleResponse>().Result;
            Assert.IsFalse(cicleResponse.Success);
            Assert.IsTrue(cicleResponse.ErrorCode.HasFlag(CicleError.InvalidId));
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
            GetCicleResponse getCicleResponse = response.Content.ReadAsAsync<GetCicleResponse>().Result;
            Assert.IsFalse(getCicleResponse.Success);
            Assert.IsTrue(getCicleResponse.ErrorCode.HasFlag(CicleError.InvalidId));

        }
    }
}
