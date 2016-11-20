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
    public class WeightTicketPresenter
    {
        #region Private variables
        private string baseUrl;
        private string insertAction;
        private string updateAction;
        private string deleteAction;
        private string getAllAction;
        private string getByIdAction;
        private string getProductsAction;
        private string getProducersAction;
        private string getSalesCustomersAction;
        private string getSuppliersAction;
        private string getRanchersAction;
        private string getCiclesActions;         
        private List<SelectableModel> currentTickets;
        private string getWareHousesAction;   
        IWeightTicketView view;
        #endregion

        #region Constructor
        public WeightTicketPresenter(IWeightTicketView view)
        {
            this.view = view;
            baseUrl = @"http://lasmargaritasdev.azurewebsites.net/";
            if (ConfigurationManager.AppSettings["baseUrl"] != null)
            {
                baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            }
     
            insertAction = "WeightTicket/Add";
            updateAction = "WeightTicket/Update";
            deleteAction = "WeightTicket/Delete";
            getAllAction = "WeightTicket/GetSelectableModels";
            getByIdAction = "WeightTicket/GetById";
            getProductsAction = "Product/GetWeightTicketProducts";
            getProducersAction = "Producer/GetSelectableModels";
            getSalesCustomersAction = "SalesCustomer/GetSelectableModels";
            getSuppliersAction = "Supplier/GetSelectableModels";
            getRanchersAction = "Rancher/GetSelectableModels";
            getWareHousesAction = "WareHouse/GetSelectableModels";
            getCiclesActions = "Cicle/GetSelectableModels";
        }
        #endregion

        #region Public properties       
        public Token Token { get; set; }
        #endregion

        #region Private Properties

        #endregion

        #region Private methods

        private void LoadWeightTickets()
        {
            try
            {
                if (Token == null)
                    throw new InvalidOperationException("Login first");
                GetWeightTicketsFromApi();                
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
            catch (Exception ex)
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(0);
                MethodBase currentMethodName = sf.GetMethod();
                Guid errorId = Guid.NewGuid();
                //Log error here
                view.HandleException(ex, currentMethodName.Name, errorId);
            }

        }

        private void GetCatalogs()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            HttpResponseMessage response;
            //Producers
            response = client.GetAsync(getProducersAction).Result;
            if (response.IsSuccessStatusCode)
            {
                GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                if (getSelectableModelResponse.Success)
                {
                    view.Producers = getSelectableModelResponse.SelectableModels;
                }
            }
            /* //Ranchers
             HttpResponseMessage response = client.GetAsync(getRanchersAction).Result;
             if (response.IsSuccessStatusCode)
             {
                 GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                 if (getSelectableModelResponse.Success)
                 {
                     view.Ranchers = getSelectableModelResponse.SelectableModels;

                 }
             }

             //Sales customers
             response = client.GetAsync(getSalesCustomersAction).Result;
             if (response.IsSuccessStatusCode)
             {
                 GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                 if (getSelectableModelResponse.Success)
                 {
                     view.SalesCustomers = getSelectableModelResponse.SelectableModels;
                 }
             }
             //Suppliers
             response = client.GetAsync(getSuppliersAction).Result;
             if (response.IsSuccessStatusCode)
             {
                 GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                 if (getSelectableModelResponse.Success)
                 {
                     view.Suppliers = getSelectableModelResponse.SelectableModels;
                 }
             }*/

            //Cicles
            response = client.GetAsync(getCiclesActions).Result;
            if (response.IsSuccessStatusCode)
            {
                GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                if (getSelectableModelResponse.Success)
                {
                    view.Cicles = getSelectableModelResponse.SelectableModels;
                }
            }
            //Filter cicles
            response = client.GetAsync(getCiclesActions).Result;
            if (response.IsSuccessStatusCode)
            {
                GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                if (getSelectableModelResponse.Success)
                {
                    view.FilterCicles = getSelectableModelResponse.SelectableModels;
                }
            }
            //Products
            response = client.GetAsync(getProductsAction).Result;
            if (response.IsSuccessStatusCode)
            {
                GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                if (getSelectableModelResponse.Success)
                {
                    view.Products = getSelectableModelResponse.SelectableModels;
                }
            }
            //Warehouses
            response = client.GetAsync(getWareHousesAction).Result;
            if (response.IsSuccessStatusCode)
            {
                GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                if (getSelectableModelResponse.Success)
                {
                    view.WareHouses = getSelectableModelResponse.SelectableModels;
                }
            }
        }
        #endregion

        #region Public methods
        public void SetEntranceDateToNow()
        {
            view.CurrentWeightTicket.EntranceDate = DateTime.Now;
            view.CurrentWeightTicket.RaiseUpdateProperties();
        }
        public void SetExitDateToNow()
        {
            view.CurrentWeightTicket.ExitDate = DateTime.Now;
            view.CurrentWeightTicket.RaiseUpdateProperties();
        }
        public void Initialize()
        {
            GetCatalogs();
            LoadWeightTickets();
        }
        public void ReloadWeightTicketsList()
        {
            LoadWeightTickets();
            PropertyCopier.CopyProperties(new WeightTicket(), view.CurrentWeightTicket);
            view.CurrentWeightTicket.RaiseUpdateProperties();
            view.SelectedId = -1;
        }

        public void DeleteTicket()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            HttpResponseMessage response = client.PostAsJsonAsync(deleteAction, new IdModel(view.SelectedId)).Result;
            if (response.IsSuccessStatusCode)
            {
                ProducerResponse producerResponse = response.Content.ReadAsAsync<ProducerResponse>().Result;
                if(producerResponse.Success)
                {
                    //Deleted!
                    LoadWeightTickets();
                    PropertyCopier.CopyProperties(new Producer(), view.CurrentWeightTicket);
                    view.CurrentWeightTicket.RaiseUpdateProperties();
                    view.SelectedId = -1;
                }
            }
           
        }

        public void NewWeightTicket()
        {
            PropertyCopier.CopyProperties(new WeightTicket(), view.CurrentWeightTicket);
            view.CurrentWeightTicket.RaiseUpdateProperties();
            view.SelectedId = -1;
        }

        public void UpdateCurrentWeightTicket()
        {
            if (view.SelectedId == -1)
            {
                PropertyCopier.CopyProperties(new Producer(), view.CurrentWeightTicket);
                      
            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                HttpResponseMessage response = client.GetAsync(string.Format("{0}?id={1}",getByIdAction, view.SelectedId)).Result;
                if (response.IsSuccessStatusCode)
                {
                    GetWeightTicketResponse getWeightTicketResponse = response.Content.ReadAsAsync<GetWeightTicketResponse>().Result;
                    if (getWeightTicketResponse.Success)
                    {
                        PropertyCopier.CopyProperties(getWeightTicketResponse.WeightTickets[0], view.CurrentWeightTicket);
                        view.CurrentWeightTicket.RaiseUpdateProperties();                       
                    }
                }
            }
            view.CurrentWeightTicket.RaiseUpdateProperties();
        }
   
        public void SaveWeightTicket()
        {
            bool reLoadList = false;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);            
            string action = string.Empty;
            if (view.CurrentWeightTicket.Id == 0)
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
            HttpResponseMessage response = client.PostAsync(action, view.CurrentWeightTicket, bsonFormatter).Result;
            response.EnsureSuccessStatusCode();
            MediaTypeFormatter[] formatters = new MediaTypeFormatter[] { bsonFormatter};
            WeightTicketResponse weightTicketResponse = response.Content.ReadAsAsync<WeightTicketResponse>(formatters).Result;
            if (weightTicketResponse.Success)
            {
                if (weightTicketResponse.WeightTicket != null)
                {
                    if (reLoadList)
                    {
                        LoadWeightTickets();
                        view.SelectedId = weightTicketResponse.WeightTicket.Id;
                    }
                }
            }
            else
            {
                throw new WeightTicketException(weightTicketResponse.ErrorCode, weightTicketResponse.ErrorMessage);
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

        private void GetWeightTicketsFromApi()
        {
            //get all 
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            HttpResponseMessage response = client.GetAsync(getAllAction).Result;
            if (response.IsSuccessStatusCode)
            {
                GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                if (getSelectableModelResponse.Success)
                {
                    currentTickets = getSelectableModelResponse.SelectableModels;
                }
                else
                {
                    throw new WeightTicketException(getSelectableModelResponse.ErrorMessage);
                }
            }
            else
            {
                throw new WeightTicketException(WeightTicketError.ApiCommunicationError);
            }
        }
        #endregion
    }
}

