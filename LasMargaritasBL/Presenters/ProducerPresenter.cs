using LasMargaritas.BL.Cache;
using LasMargaritas.BL.Utils;
using LasMargaritas.BL.Views;
using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private string getSelectableModelsAction;
        private string getLastModification;        
        private string getStatesAction;
        private string getRegimesAction;
        private string getCivilStatusAction;
        private string getGenderAction;
        private List<SelectableModel> filterProducers;
        private List<SelectableModel> originalProducers;
        IProducerView producerView;
        #endregion

        #region Constructor
        public ProducerPresenter(IProducerView view)
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
            getLastModification = "LastModification/GetLastModification";
            getSelectableModelsAction = "Producer/GetSelectableModels";
            getStatesAction = "Catalog/GetStates";
            getRegimesAction = "Catalog/GetRegimes";
            getCivilStatusAction = "Catalog/GetCivilStatus";
            getGenderAction = "Catalog/GetGenders";
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

        #region Private methods

        private void LoadProducers()
        {
           
            if (Token == null)
                throw new InvalidOperationException("Login first");
            //Get last remote modification
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            GetProducersFromApi();
            producerView.Producers = originalProducers;
            /* string url = string.Format("{0}?module={1}", getLastModification, (int)Models.Module.Producers); //Producers
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
            */
        }

        private void GetCatalogs()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            //States
            HttpResponseMessage response = client.GetAsync(getStatesAction).Result;
            if (response.IsSuccessStatusCode)
            {
                GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                if (getSelectableModelResponse.Success)
                {
                    producerView.States = getSelectableModelResponse.SelectableModels;

                }
                else
                {
                    throw new SelectableModelException(getSelectableModelResponse.ErrorCode, getSelectableModelResponse.ErrorMessage);
                }
            }
            //Genders
            response = client.GetAsync(getGenderAction).Result;
            if (response.IsSuccessStatusCode)
            {
                GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                if (getSelectableModelResponse.Success)
                {
                    producerView.Genders = getSelectableModelResponse.SelectableModels;
                }
                else
                {
                    throw new SelectableModelException(getSelectableModelResponse.ErrorCode, getSelectableModelResponse.ErrorMessage);
                }
            }
            //Civil Status
            response = client.GetAsync(getCivilStatusAction).Result;
            if (response.IsSuccessStatusCode)
            {
                GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                if (getSelectableModelResponse.Success)
                {
                    producerView.CivilStatus = getSelectableModelResponse.SelectableModels;
                }
                else
                {
                    throw new SelectableModelException(getSelectableModelResponse.ErrorCode, getSelectableModelResponse.ErrorMessage);
                }
            }
            //Regimes
            response = client.GetAsync(getRegimesAction).Result;
            if (response.IsSuccessStatusCode)
            {
                GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                if (getSelectableModelResponse.Success)
                {
                    producerView.Regimes = getSelectableModelResponse.SelectableModels;
                }
                else
                {
                    throw new SelectableModelException(getSelectableModelResponse.ErrorCode, getSelectableModelResponse.ErrorMessage);
                }
            }
        }
        #endregion

        #region Public methods
        public void Initialize()
        {
            try
            {
                GetCatalogs();
                LoadProducers();
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(0);
                MethodBase currentMethodName = sf.GetMethod();
                Guid errorId = Guid.NewGuid();
                //Log error here
                producerView.HandleException(ex, currentMethodName.Name, errorId);
            }
        }
        public void ReloadProducerList()
        {
            try
            {
                LoadProducers();
                PropertyCopier.CopyProperties(new Producer(), producerView.CurrentProducer);
                producerView.CurrentProducer.RaiseUpdateProperties();
                producerView.SelectedId = -1;
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(0);
                MethodBase currentMethodName = sf.GetMethod();
                Guid errorId = Guid.NewGuid();
                //Log error here
                producerView.HandleException(ex, currentMethodName.Name, errorId);
            }
        }

        public void DeleteProducer()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                HttpResponseMessage response = client.PostAsJsonAsync(deleteAction, new IdModel(producerView.SelectedId)).Result;
                response.EnsureSuccessStatusCode();
                ProducerResponse producerResponse = response.Content.ReadAsAsync<ProducerResponse>().Result;
                if (producerResponse.Success)
                {
                    //Deleted!
                    LoadProducers();
                    PropertyCopier.CopyProperties(new Producer(), producerView.CurrentProducer);
                    producerView.CurrentProducer.RaiseUpdateProperties();
                    producerView.SelectedId = -1;
                }
                else
                {
                    throw new ProducerException(producerResponse.ErrorCode, producerResponse.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(0);
                MethodBase currentMethodName = sf.GetMethod();
                Guid errorId = Guid.NewGuid();
                //Log error here
                producerView.HandleException(ex, currentMethodName.Name, errorId);
            }
        }

        public void NewProducer()
        {
            try
            {
                PropertyCopier.CopyProperties(new Producer(), producerView.CurrentProducer);
                producerView.CurrentProducer.RaiseUpdateProperties();
                producerView.SelectedId = -1;
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(0);
                MethodBase currentMethodName = sf.GetMethod();
                Guid errorId = Guid.NewGuid();
                //Log error here
                producerView.HandleException(ex, currentMethodName.Name, errorId);
            }
        }

        public void UpdateCurrentProducer()
        {
            try
            {
                if (producerView.SelectedId == -1)
                {
                    producerView.CurrentProducer = new Producer();
                }
                else
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                    HttpResponseMessage response = client.GetAsync(string.Format("{0}?id={1}", getByIdAction, producerView.SelectedId)).Result;
                    response.EnsureSuccessStatusCode();
                    GetProducerResponse getProducerResponse = response.Content.ReadAsAsync<GetProducerResponse>().Result;
                    if (getProducerResponse.Success)
                    {
                        PropertyCopier.CopyProperties(getProducerResponse.Producers[0], producerView.CurrentProducer);
                        producerView.CurrentProducer.RaiseUpdateProperties();
                    }
                    else
                    {
                        throw new ProducerException(getProducerResponse.ErrorCode, getProducerResponse.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
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
            try
            {
                bool reLoadList = false;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                string action = string.Empty;
                if (producerView.CurrentProducer.Id == 0)
                {
                    //insert
                    action = insertAction;
                    reLoadList = true;
                }
                else
                {
                    action = updateAction;
                }
                //update
                MediaTypeFormatter bsonFormatter = new BsonMediaTypeFormatter();
                HttpResponseMessage response = client.PostAsync(action, producerView.CurrentProducer, bsonFormatter).Result;
                response.EnsureSuccessStatusCode();
                MediaTypeFormatter[] formatters = new MediaTypeFormatter[] { bsonFormatter };
                ProducerResponse producerResponse = response.Content.ReadAsAsync<ProducerResponse>(formatters).Result;
                if (producerResponse.Success)
                {
                    if (producerResponse.Producer != null)
                    {
                        if (reLoadList)
                        {
                            LoadProducers();
                            producerView.SelectedId = producerResponse.Producer.Id;
                        }
                    }
                }
                else
                {
                    throw new ProducerException(producerResponse.ErrorCode, producerResponse.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(0);
                MethodBase currentMethodName = sf.GetMethod();
                Guid errorId = Guid.NewGuid();
                //Log error here
                producerView.HandleException(ex, currentMethodName.Name, errorId);
            }
        }     

        #endregion

        #region Private methods
        /* private void SaveProducersToCache()
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
           */

        private void GetProducersFromApi()
        {
            //get all 
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            HttpResponseMessage response = client.GetAsync(getSelectableModelsAction).Result;
            response.EnsureSuccessStatusCode();
            GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
            if (getSelectableModelResponse.Success)
            {
                originalProducers = getSelectableModelResponse.SelectableModels;
            }
            else
            {
                throw new SelectableModelException(getSelectableModelResponse.ErrorMessage);
            }                      
        }
        #endregion
    }
}
