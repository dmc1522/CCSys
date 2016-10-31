using LasMargaritas.BL;
using LasMargaritas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

namespace LasMargaritas.UT.API
{
    [TestClass]
    public class ProductApiTest
    {
        private string baseUrl;
        private string insertAction;
        private string updateAction;
        private string deleteAction;
        private string getAllAction;
        private string getByIdAction;
        private string getGetWeightTicketProductsAction;
        public ProductApiTest()
        {
            baseUrl = @"http://lasmargaritas.azurewebsites.net/";
            insertAction = "Product/Add";
            updateAction = "Product/Update";
            deleteAction = "Product/Delete";
            getAllAction = "Product/GetAll";
            getByIdAction = "Product/GetById";
            getGetWeightTicketProductsAction = "Product/GetWeightTicketProducts";
        }

        [TestMethod]
        public void TestGetWeightTicketsProducts()
        {
            //Get token
            Token token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3");
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //Post add ticket
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            HttpResponseMessage response = client.GetAsync(getGetWeightTicketProductsAction).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            GetProductResponse getProductResponse = response.Content.ReadAsAsync<GetProductResponse>().Result;
            Assert.IsTrue(getProductResponse.Success);
            Assert.IsTrue(getProductResponse.Products.Count > 0);
        }
    }
}
