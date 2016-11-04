using LasMargaritas.BL.Cache;
using LasMargaritas.BL.Views;
using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;

namespace LasMargaritas.BL.Presenters
{
    public class ProducerPresenter
    {
        #region Private variables
        private Cacher<Producer> cacher;
        private string baseUrl;
        private string insertAction;
        private string updateAction;
        private string deleteAction;
        private string getAllAction;
        private string getByIdAction;
        private string getLastModification;
        private List<Producer> filterProducers;
        private List<Producer> originalProducers;
        IProducerView producerView;
        #endregion

        #region Constructor
        public ProducerPresenter(IProducerView view)
        {
            baseUrl = @"http://lasmargaritas.azurewebsites.net/";
            insertAction = "Producer/Add";
            updateAction = "Producer/Update";
            deleteAction = "Producer/Delete";
            getAllAction = "Producer/GetAll";
            getByIdAction = "GetLastModification/GetById";
            getLastModification = "LastModification/GetLastModification";
            producerView = view;
            cacher = new Cacher<Producer>("producers.json");

        }
        #endregion

        #region Public properties       
        public Token Token { get; set; }

        public DateTime? LastSynchronizationTimeStamp { get; set; }

        public DateTime? LastRemoteModificationTimeStamp { get; set; }
        #endregion

        #region Private Properties

        #endregion

        #region Public methods
        public void LoadProducers()
        {
            try
            {
                if (Token == null)
                    throw new InvalidOperationException("Login first");
                //Get last remote modification
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                string url = string.Format("{0}?module={1}", getLastModification, (int)Models.Module.Producers); //Producers
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    GetLastModificationResponse getLastModificationResponse = response.Content.ReadAsAsync<GetLastModificationResponse>().Result;
                    if (getLastModificationResponse.LastModifications.Count == 1)
                        LastRemoteModificationTimeStamp = getLastModificationResponse.LastModifications.ElementAt(0).Timestamp;
                }
                //No log here ...                     
                if (LastRemoteModificationTimeStamp.HasValue)
                {
                    //Check cached version
                    CachedModel<Producer> cachedProducers = cacher.LoadFromCache();
                    if (null != cachedProducers && LastRemoteModificationTimeStamp.Equals(cachedProducers.CachedDate))
                    {
                        //cached version is the same
                        originalProducers = cachedProducers.Models;
                        LastSynchronizationTimeStamp = cacher.CachedDate;
                    }
                    else
                    {
                        GetProducersFromApi();
                        SaveProducersToCache();
                        LastSynchronizationTimeStamp = cacher.CachedDate;
                    }
                }
                else
                {
                    LastRemoteModificationTimeStamp = DateTime.Now; //nothing to do here
                    GetProducersFromApi();
                    SaveProducersToCache();
                    LastSynchronizationTimeStamp = cacher.CachedDate;
                }
                producerView.Producers = originalProducers;
            }
            catch(Exception ex)
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(0);
                MethodBase currentMethodName = sf.GetMethod();
                Guid errorId = Guid.NewGuid();            
                //Log error here
                producerView.HandleException(ex, currentMethodName.Name, errorId);
            }
            
        }

        public void SaveProducer()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);            
            string action = string.Empty;
            if (producerView.CurrentProducer.Id == 0)
            {
                //insert
                action = insertAction;

            }
            else
            {
                action = updateAction;
            }            
            //update
            MediaTypeFormatter bsonFormatter = new BsonMediaTypeFormatter();
            HttpResponseMessage response = client.PostAsync(action, producerView.CurrentProducer, bsonFormatter).Result;
            response.EnsureSuccessStatusCode();
            MediaTypeFormatter[] formatters = new MediaTypeFormatter[] { bsonFormatter};
            GetProducerResponse getProducerResponse = response.Content.ReadAsAsync<GetProducerResponse>(formatters).Result;
            if (getProducerResponse.Success)
            {
                if (getProducerResponse.Producers.Count == 1)
                {
                    //Saved!
                }
            }
            else
            {
                throw new ProducerException(getProducerResponse.ErrorCode, getProducerResponse.ErrorMessage);
            }
        }

        public void FilterProducerList()
        {
        }

        #endregion

        #region Private methods
        private void SaveProducersToCache()
        {
            CachedModel<Producer> cachedVersion = new CachedModel<Producer>();
            cachedVersion.Models = originalProducers;
            cachedVersion.CachedDate = LastRemoteModificationTimeStamp.Value;
            cacher.SaveToCache(cachedVersion);
        }

        private void GetProducersFromApi()
        {
            //get all 
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            HttpResponseMessage response = client.GetAsync(getAllAction).Result;           
            if (response.IsSuccessStatusCode)
            {
                GetProducerResponse getProducerResponse = response.Content.ReadAsAsync<GetProducerResponse>().Result;
                if (getProducerResponse.Success)
                {
                    originalProducers = getProducerResponse.Producers;
                }
                else
                {
                    throw new ProducerException(getProducerResponse.ErrorCode, getProducerResponse.ErrorMessage);
                }
            }
            else
            {
                throw new ProducerException(ProducerError.ApiCommunicationError);
            }
        }
        #endregion
    }
}
