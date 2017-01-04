using LasMargaritas.BL.Cache;
using LasMargaritas.BL.Utils;
using LasMargaritas.BL.Views;
using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Windows.Forms;

namespace LasMargaritas.BL.Presenters
{
    public class WeightTicketReportPresenter
    {
        #region Private variables
        private string baseUrl;
        private string getAllAction;
        private string getProductsAction;
        private string getProducersAction;
        private string getSalesCustomersAction;
        private string getSuppliersAction;
        private string getRanchersAction;
        private string getCiclesActions;
        private string getWareHousesAction;
        private string getReportAction;
        private List<string> excludedColumns;
        private IWeightTicketReportView view;        
        #endregion

        #region Constructor
        public WeightTicketReportPresenter(IWeightTicketReportView view)
        {            
            this.view = view;
            baseUrl = @"http://lasmargaritasdev.azurewebsites.net/";
            if (ConfigurationManager.AppSettings["baseUrl"] != null)
            {
                baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            }     
            getAllAction = "WeightTicket/GetWeightTicketsReport";
            getProductsAction = "Product/GetWeightTicketProducts";
            getProducersAction = "Producer/GetSelectableModels";
            getSalesCustomersAction = "SaleCustomer/GetSelectableModels";
            getSuppliersAction = "Supplier/GetSelectableModels";
            getRanchersAction = "Rancher/GetSelectableModels";
            getWareHousesAction = "WareHouse/GetSelectableModels";
            getCiclesActions = "Cicle/GetSelectableModels";
            getReportAction = "WeightTicket/GetWeightTicketsReport";
            excludedColumns = new List<string>();
        }
        #endregion

        #region Public properties       
        public Token Token { get; set; }
        #endregion

        #region Private Properties

        #endregion

        #region Private methods
        private void LoadCicles()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            HttpResponseMessage response;

            //Cicles
            response = client.GetAsync(getCiclesActions).Result;
            response.EnsureSuccessStatusCode();
            GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
            if (getSelectableModelResponse.Success)
            {
                view.Cicles = getSelectableModelResponse.SelectableModels;
            }
            else
            {
                throw new SelectableModelException(getSelectableModelResponse.ErrorCode, getSelectableModelResponse.ErrorMessage);
            }            
        }

        private void LoadProducers()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            HttpResponseMessage response;
            //Producers
            response = client.GetAsync(getProducersAction).Result;
            response.EnsureSuccessStatusCode();
            GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
            if (getSelectableModelResponse.Success)
            {
                view.Producers = getSelectableModelResponse.SelectableModels;
            }
            else
            {
                throw new SelectableModelException(getSelectableModelResponse.ErrorCode, getSelectableModelResponse.ErrorMessage);
            }
        }

        private void LoadRanchers()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            HttpResponseMessage response;
            //Ranchers
            response = client.GetAsync(getRanchersAction).Result;
            response.EnsureSuccessStatusCode();
            GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
            if (getSelectableModelResponse.Success)
            {
                view.Ranchers = getSelectableModelResponse.SelectableModels;
            }
            else
            {
                throw new SelectableModelException(getSelectableModelResponse.ErrorCode, getSelectableModelResponse.ErrorMessage);
            }
        }

        private void LoadSuppliers()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            HttpResponseMessage response;
            //Suppliers
            response = client.GetAsync(getSuppliersAction).Result;
            response.EnsureSuccessStatusCode();
            GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
            if (getSelectableModelResponse.Success)
            {
                view.Suppliers = getSelectableModelResponse.SelectableModels;
            }
            else
            {
                throw new SelectableModelException(getSelectableModelResponse.ErrorCode, getSelectableModelResponse.ErrorMessage);
            }
        }

        private void LoadSaleCustomers()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            HttpResponseMessage response;
            //Sales customers
            response = client.GetAsync(getSalesCustomersAction).Result;
            response.EnsureSuccessStatusCode();
            GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
            if (getSelectableModelResponse.Success)
            {
                view.SaleCustomers = getSelectableModelResponse.SelectableModels;
            }
            else
            {
                throw new SelectableModelException(getSelectableModelResponse.ErrorCode, getSelectableModelResponse.ErrorMessage);
            }
        }

        public void LoadProducts()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                HttpResponseMessage response;
                //Products
                string action = string.Format("{0}?type={1}", getProductsAction, (int)view.WeightTicketType);
                response = client.GetAsync(action).Result;
                response.EnsureSuccessStatusCode();
                GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                if (getSelectableModelResponse.Success)
                {
                    view.Products = getSelectableModelResponse.SelectableModels;
                }
                else
                {
                    throw new SelectableModelException(getSelectableModelResponse.ErrorCode, getSelectableModelResponse.ErrorMessage);
                }

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
        private void LoadWareHouses()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                HttpResponseMessage response;                
                //Warehouses
                response = client.GetAsync(getWareHousesAction).Result;
                response.EnsureSuccessStatusCode();
                GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                if (getSelectableModelResponse.Success)
                {
                    view.WareHouses = getSelectableModelResponse.SelectableModels;
                }
                else
                {
                    throw new SelectableModelException(getSelectableModelResponse.ErrorCode, getSelectableModelResponse.ErrorMessage);
                }

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
        #endregion
        #region Public methods
        public void LoadReport()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);                
                HttpResponseMessage response = client.GetAsync(string.Format("{0}?{1}", getReportAction, view.CurrentFilters.GetUrlQuery())).Result;
                response.EnsureSuccessStatusCode();
                GetReportDataResponse getReportDataResponse = response.Content.ReadAsAsync<GetReportDataResponse>().Result;                
                view.ReportData = getReportDataResponse.ReportData;
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
        public void LoadCatalogs()
        {
            try
            {
                LoadProducers();
                LoadRanchers();
                LoadSuppliers();
                LoadSaleCustomers();
                LoadCicles();
                LoadProducts();
                LoadWareHouses();
                                

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


        #endregion
        }
}

