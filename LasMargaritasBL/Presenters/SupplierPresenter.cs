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
    public class SupplierPresenter
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

        private List<SelectableModel> filterSuppliers;
        private List<SelectableModel> originalSuppliers;
        ISupplierView supplierView;
        #endregion

        #region Constructor
        public SupplierPresenter(ISupplierView view)
        {
            baseUrl = @"http://lasmargaritasdev.azurewebsites.net/";
            if (ConfigurationManager.AppSettings["baseUrl"] != null)
            {
                baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            }
     
            insertAction = "Supplier/Add";
            updateAction = "Supplier/Update";
            deleteAction = "Supplier/Delete";
            getAllAction = "Supplier/GetAll";
            getByIdAction = "Supplier/GetById";
            getStatesAction = "Catalog/GetStates";
            supplierView = view;

        }
        #endregion

        #region Public properties       
        public Token Token { get; set; }

        #endregion

        #region Private Properties

        #endregion


        #region Private methods

        private void LoadSuppliers()
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
                GetSuppliersFromApi();
                supplierView.Suppliers = originalSuppliers;
              
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(0);
                MethodBase currentMethodName = sf.GetMethod();
                Guid errorId = Guid.NewGuid();
                //Log error here
                supplierView.HandleException(ex, currentMethodName.Name, errorId);
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
                    supplierView.States = getSelectableModelResponse.SelectableModels;

                }
            }
           
        }
        #endregion


        #region Public methods
        public void Initialize()
        {
            GetCatalogs();
            LoadSuppliers();
        }

        public void DeleteSupplier()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            HttpResponseMessage response = client.PostAsJsonAsync(deleteAction, new IdModel(supplierView.SelectedId)).Result;
            if (response.IsSuccessStatusCode)
            {
                SupplierResponse supplierResponse = response.Content.ReadAsAsync<SupplierResponse>().Result;
                if(supplierResponse.Success)
                {
                    //Deleted!
                    LoadSuppliers();
                    PropertyCopier.CopyProperties(new Supplier(), supplierView.CurrentSupplier);
                    supplierView.CurrentSupplier.RaiseUpdateProperties();
                    supplierView.SelectedId = -1;
                }
            }
           
        }
        public void NewSupplier()
        {
            PropertyCopier.CopyProperties(new Supplier(), supplierView.CurrentSupplier);
            supplierView.CurrentSupplier.RaiseUpdateProperties();
            supplierView.SelectedId = -1;
        }
        public void UpdateCurrentSupplier()
        {
            if (supplierView.SelectedId == -1)
            {
                supplierView.CurrentSupplier = new Supplier();
            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                HttpResponseMessage response = client.GetAsync(string.Format("{0}?id={1}",getByIdAction, supplierView.SelectedId)).Result;
                if (response.IsSuccessStatusCode)
                {
                    GetSupplierResponse getSupplierResponse = response.Content.ReadAsAsync<GetSupplierResponse>().Result;
                    if (getSupplierResponse.Success)
                    {
                        PropertyCopier.CopyProperties(getSupplierResponse.Suppliers[0], supplierView.CurrentSupplier);
                        supplierView.CurrentSupplier.RaiseUpdateProperties();                       
                    }
                }
            }
        }
   
        public void SaveSupplier()
        {
            bool reLoadList = false;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);            
            string action = string.Empty;
            if (supplierView.CurrentSupplier.Id == 0)
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
            HttpResponseMessage response = client.PostAsync(action, supplierView.CurrentSupplier, bsonFormatter).Result;
            response.EnsureSuccessStatusCode();
            MediaTypeFormatter[] formatters = new MediaTypeFormatter[] { bsonFormatter};
            SupplierResponse supplierResponse = response.Content.ReadAsAsync<SupplierResponse>(formatters).Result;
            if (supplierResponse.Success)
            {
                if (supplierResponse.Supplier != null)
                {
                    if (reLoadList)
                    {
                        LoadSuppliers();
                        supplierView.SelectedId = supplierResponse.Supplier.Id;
                    }
                }
            }
            else
            {
                throw new SupplierException(supplierResponse.ErrorCode, supplierResponse.ErrorMessage);
            }
        }

        public void FilterSupplierList()
        {
        }

        #endregion

        #region Private methods
   
        private void GetSuppliersFromApi()
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
                    originalSuppliers = getSelectableModelResponse.SelectableModels;
                }
                else
                {
                    throw new SupplierException(getSelectableModelResponse.ErrorMessage);
                }
            }
            else
            {
                throw new SupplierException(SupplierError.ApiCommunicationError);
            }
        }
        #endregion
    }
}
