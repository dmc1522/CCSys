using LasMargaritas.BL;
using LasMargaritas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Script.Serialization;

namespace LasMargaritas.UT.API
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
            baseUrl = @"http://lasmargaritas.azurewebsites.net/";
            insertAction = "Producer/Add";
            updateAction = "Producer/Update";
            deleteAction = "Producer/Delete";
            getAllAction = "Producer/GetAll";
            getByIdAction = "Producer/GetById";
            getSelectableModelsAction = "Producer/GetSelectableModels";
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
            Assert.IsTrue(getSelectableModelResponse.SelectableModels.Count > 0);        }

    }
}
