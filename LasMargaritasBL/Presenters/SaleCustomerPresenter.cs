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
    public class SaleCustomerPresenter
    {
        #region Private variables
        private string baseUrl;
        private string insertAction;
        private string updateAction;
        private string deleteAction;
        private string getAllAction;
        private string getByIdAction;    
        private string getStatesAction;
        private string getSelectableModelsAction;

        private List<SelectableModel> filterSaleCustomers;
        private List<SelectableModel> originalSaleCustomers;
        ISaleCustomerView saleCustomerView;
        #endregion

        #region Constructor
        public SaleCustomerPresenter(ISaleCustomerView view)
        {
            baseUrl = @"http://lasmargaritasdev.azurewebsites.net/";
            if (ConfigurationManager.AppSettings["baseUrl"] != null)
            {
                baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            }
     
            insertAction = "SaleCustomer/Add";
            updateAction = "SaleCustomer/Update";
            deleteAction = "SaleCustomer/Delete";
            getAllAction = "SaleCustomer/GetAll";
            getByIdAction = "SaleCustomer/GetById";
            getStatesAction = "Catalog/GetStates";
            saleCustomerView = view;

        }
        #endregion

        #region Public properties       
        public Token Token { get; set; }

        #endregion

        #region Private Properties

        #endregion


        #region Private methods

        private void LoadSaleCustomers()
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
                GetSaleCustomersFromApi();
                saleCustomerView.SaleCustomers = originalSaleCustomers;
              
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(0);
                MethodBase currentMethodName = sf.GetMethod();
                Guid errorId = Guid.NewGuid();
                //Log error here
                saleCustomerView.HandleException(ex, currentMethodName.Name, errorId);
            }

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
                    saleCustomerView.States = getSelectableModelResponse.SelectableModels;

                }
            }
           
        }
        #endregion


        #region Public methods
        public void Initialize()
        {
            GetCatalogs();
            LoadSaleCustomers();
        }

        public void DeleteSaleCustomer()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            HttpResponseMessage response = client.PostAsJsonAsync(deleteAction, new IdModel(saleCustomerView.SelectedId)).Result;
            if (response.IsSuccessStatusCode)
            {
                SaleCustomerResponse saleCustomerResponse = response.Content.ReadAsAsync<SaleCustomerResponse>().Result;
                if(saleCustomerResponse.Success)
                {
                    //Deleted!
                    LoadSaleCustomers();
                    PropertyCopier.CopyProperties(new SaleCustomer(), saleCustomerView.CurrentSaleCustomer);
                    saleCustomerView.CurrentSaleCustomer.RaiseUpdateProperties();
                    saleCustomerView.SelectedId = -1;
                }
            }
           
        }
        public void NewSaleCustomer()
        {
            PropertyCopier.CopyProperties(new SaleCustomer(), saleCustomerView.CurrentSaleCustomer);
            saleCustomerView.CurrentSaleCustomer.RaiseUpdateProperties();
            saleCustomerView.SelectedId = -1;
        }
        public void UpdateCurrentSaleCustomer()
        {
            if (saleCustomerView.SelectedId == -1)
            {
                saleCustomerView.CurrentSaleCustomer = new SaleCustomer();
            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                HttpResponseMessage response = client.GetAsync(string.Format("{0}?id={1}",getByIdAction, saleCustomerView.SelectedId)).Result;
                if (response.IsSuccessStatusCode)
                {
                    GetSaleCustomerResponse getSaleCustomerResponse = response.Content.ReadAsAsync<GetSaleCustomerResponse>().Result;
                    if (getSaleCustomerResponse.Success)
                    {
                        PropertyCopier.CopyProperties(getSaleCustomerResponse.SaleCustomers[0], saleCustomerView.CurrentSaleCustomer);
                        saleCustomerView.CurrentSaleCustomer.RaiseUpdateProperties();                       
                    }
                }
            }
        }
   
        public void SaveSaleCustomer()
        {
            bool reLoadList = false;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);            
            string action = string.Empty;
            if (saleCustomerView.CurrentSaleCustomer.Id == 0)
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
            HttpResponseMessage response = client.PostAsync(action, saleCustomerView.CurrentSaleCustomer, bsonFormatter).Result;
            response.EnsureSuccessStatusCode();
            MediaTypeFormatter[] formatters = new MediaTypeFormatter[] { bsonFormatter};
            SaleCustomerResponse saleCustomerResponse = response.Content.ReadAsAsync<SaleCustomerResponse>(formatters).Result;
            if (saleCustomerResponse.Success)
            {
                if (saleCustomerResponse.SaleCustomer != null)
                {
                    if (reLoadList)
                    {
                        LoadSaleCustomers();
                        saleCustomerView.SelectedId = saleCustomerResponse.SaleCustomer.Id;
                    }
                }
            }
            else
            {
                throw new SaleCustomerException(saleCustomerResponse.ErrorCode, saleCustomerResponse.ErrorMessage);
            }
        }

        public void FilterSaleCustomerList()
        {
        }

        #endregion

        #region Private methods
   
        private void GetSaleCustomersFromApi()
        {
            //get all 
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            HttpResponseMessage response = client.GetAsync(getSelectableModelsAction).Result;
            if (response.IsSuccessStatusCode)
            {
                GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                if (getSelectableModelResponse.Success)
                {
                    originalSaleCustomers = getSelectableModelResponse.SelectableModels;
                }
                else
                {
                    throw new SaleCustomerException(getSelectableModelResponse.ErrorMessage);
                }
            }
            else
            {
                throw new SaleCustomerException(SaleCustomerError.ApiCommunicationError);
            }
        }
        #endregion
    }
}
