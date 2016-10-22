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
using System.Web.Script.Serialization;

namespace LasMargaritas.ULT
{
    
    public class TokenHelper
    {
        public static string baseUrl = "http://lasmargaritas.azurewebsites.net/";
        public static Token GetToken()
        {
            string token = string.Empty;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("userName", "Melvin3"),
                new KeyValuePair<string, string>("password", "MelvinPass3"),
                new KeyValuePair<string, string>("grant_type", "password")

            });            
            HttpResponseMessage response = client.PostAsync("token",content).Result;
            string result =  response.Content.ReadAsStringAsync().Result;
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Deserialize<Token>(result);

        }
    }
}

