using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

namespace LasMargaritas.BL
{

    public class TokenHelper
    {        
        public static Token GetToken(string baseUrl, string userName, string password)
        {
            string token = string.Empty;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("userName", userName),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            });            
            HttpResponseMessage response = client.PostAsync("token",content).Result;
            string result =  response.Content.ReadAsStringAsync().Result;
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Deserialize<Token>(result);

        }
    }
}

