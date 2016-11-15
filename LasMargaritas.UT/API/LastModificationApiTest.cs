using LasMargaritas.BL;
using LasMargaritas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace LasMargaritas.UT.API
{
    [TestClass]
    public class LastModificationApiTest
    {
        private string baseUrl;
        private string getLastModification;
        public LastModificationApiTest()
        {
            baseUrl = @"http://lasmargaritasdev.azurewebsites.net/";
            if (ConfigurationManager.AppSettings["baseUrl"] != null)
            {
                baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            }
            getLastModification = "LastModification/GetLastModification";            
        }

        [TestMethod]
        public void TestGetWeightTicketsProducts()
        {
            //insert dummy data
            InsertDummyData();
            //Get token
            Token token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3");
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //Post add ticket
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            string url = string.Format("{0}?module={1}", getLastModification, 1); //Producers
            HttpResponseMessage response = client.GetAsync(url).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            GetLastModificationResponse getLastModificationResponse = response.Content.ReadAsAsync<GetLastModificationResponse>().Result;
            Assert.IsTrue(getLastModificationResponse.Success);
            Assert.IsTrue(getLastModificationResponse.LastModifications.Count > 0);
        }

        private void InsertDummyData()
        {
            string connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDBDev;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            LastModificationBL bl = new LastModificationBL(connectionString);
            DateTime startTime = DateTime.Now;
            bl.SetLastModification(new LastModification() { Module = Module.Producers });
        }
    }
}
