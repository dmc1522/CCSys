using LasMargaritas.BL.Views;
using LasMargaritas.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.BL.Presenters
{
    public class ProducerPresenter
    {
        private string baseUrl;
        private string insertAction;
        private string updateAction;
        private string deleteAction;
        private string getAllAction;
        private string getByIdAction;
        private string getLastModification;
        public ProducerPresenter(IProducerView view)
        {            
            baseUrl = @"http://lasmargaritas.azurewebsites.net/";
            insertAction = "Producer/Add";
            updateAction = "Producer/Update";
            deleteAction = "Producer/Delete";
            getAllAction = "Producer/GetAll";
            getByIdAction = "GetLastModification/GetById";
            getLastModification = "LastModification/GetLastModification";

        }

        public bool IsDataFromCache { get; set; }
        
        public Token Token { get; set; }

        List<Producer> Producers { get; set; }
        
        DateTime? LastSynchronizationTimeStamp { get; set; }

        DateTime? LastRemoteModificationTimeStamp { get; set; }

        public void LoadProducers()
        {
            if (Token == null)
                throw new InvalidOperationException("Login first");
            //Get last remote modification
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            string url = string.Format("{0}?module={1}", getLastModification, (int)Module.Producers); //Producers
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                GetLastModificationResponse getLastModificationResponse = response.Content.ReadAsAsync<GetLastModificationResponse>().Result;
                if(getLastModificationResponse.LastModifications.Count == 1)
                    LastRemoteModificationTimeStamp = getLastModificationResponse.LastModifications.ElementAt(0).Timestamp;
            }

            if (LastRemoteModificationTimeStamp.HasValue)
            {
                //Get local remote 
                if (File.Exists("lastModificationProducers.json"))
                {
                    string json = File.ReadAllText("lastModificationProducers.json");
                    LastSynchronizationTimeStamp = JsonConvert.DeserializeObject<DateTime>(json);
                }
                if (!LastSynchronizationTimeStamp.HasValue || (!LastRemoteModificationTimeStamp.Value.Equals(LastSynchronizationTimeStamp)))
                {
                    client = new HttpClient();
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);                  
                    response = client.GetAsync(getAllAction).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        GetProducerResponse getProducersResponse = response.Content.ReadAsAsync<GetProducerResponse>().Result;
                        Producers = getProducersResponse.Producers;
                        string json = JsonConvert.SerializeObject(Producers.ToArray());
                        //write string to file
                        System.IO.File.WriteAllText("producers.json", json);
                        json = JsonConvert.SerializeObject(LastRemoteModificationTimeStamp);
                        System.IO.File.WriteAllText("lastModificationProducers.json", json);
                        IsDataFromCache = false;
                    }
                }
                else
                {
                    string json = File.ReadAllText("producers.json");
                    //write string to file
                    Producers = JsonConvert.DeserializeObject<List<Producer>>(json);
                    IsDataFromCache = true;
                }
            }            
        }
    }
}
