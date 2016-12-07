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
    public class ProductPresenter
    {
        #region Private variables
        private string baseUrl;
        private string insertAction;
        private string updateAction;
        private string deleteAction;
        private string getAllAction;
        private string getByIdAction;    
        private string getUnitsAction;
        private string getPresentationsAction;
        private string getProductGroupsAction;
        private string getAgriculturalBrandsAction;
        private string getSelectableModelsAction;

        private List<SelectableModel> filterProducts;
        private List<SelectableModel> originalProducts;
        IProductView productView;
        #endregion

        #region Constructor
        public ProductPresenter(IProductView view)
        {
            baseUrl = @"http://lasmargaritasdev.azurewebsites.net/";
            if (ConfigurationManager.AppSettings["baseUrl"] != null)
            {
                baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            }
     
            insertAction = "Product/Add";
            updateAction = "Product/Update";
            deleteAction = "Product/Delete";
            getAllAction = "Product/GetAll";
            getByIdAction = "Product/GetById";
            getSelectableModelsAction = "Product/GetSelectableModels";
            // Need to create Method to load Units,presentation,productgroup,agricbrand
            getUnitsAction = "Catalog/GetUnits";
            getPresentationsAction = "Catalog/GetPresentations";
            getProductGroupsAction = "Catalog/GetProductGroups";
            getAgriculturalBrandsAction = "Catalog/GetAgriculturalBrands";
            productView = view;

        }
        #endregion

        #region Public properties       
        public Token Token { get; set; }

        #endregion

        #region Private Properties

        #endregion


        #region Private methods

        private void LoadProducts()
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
                GetProductsFromApi();
                productView.Products = originalProducts;
              
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(0);
                MethodBase currentMethodName = sf.GetMethod();
                Guid errorId = Guid.NewGuid();
                //Log error here
                productView.HandleException(ex, currentMethodName.Name, errorId);
            }

        }

        private void GetCatalogs()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            //States
            HttpResponseMessage response = client.GetAsync(getUnitsAction).Result;
            if (response.IsSuccessStatusCode)
            {
                GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                if (getSelectableModelResponse.Success)
                {
                    productView.Units = getSelectableModelResponse.SelectableModels;

                }
            }
            response = client.GetAsync(getPresentationsAction).Result;
            if (response.IsSuccessStatusCode)
            {
                GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                if (getSelectableModelResponse.Success)
                {
                    productView.Presentations = getSelectableModelResponse.SelectableModels;

                }
            }
            response = client.GetAsync(getProductGroupsAction).Result;
            if (response.IsSuccessStatusCode)
            {
                GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                if (getSelectableModelResponse.Success)
                {
                    productView.ProductGroups = getSelectableModelResponse.SelectableModels;

                }
            }
            response = client.GetAsync(getAgriculturalBrandsAction).Result;
            if (response.IsSuccessStatusCode)
            {
                GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                if (getSelectableModelResponse.Success)
                {
                    productView.AgriculturalBrands = getSelectableModelResponse.SelectableModels;

                }
            }

        }
        #endregion


        #region Public methods
        public void Initialize()
        {
            GetCatalogs();
            LoadProducts();
        }

        public void DeleteProduct()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            HttpResponseMessage response = client.PostAsJsonAsync(deleteAction, new IdModel(productView.SelectedId)).Result;
            if (response.IsSuccessStatusCode)
            {
                ProductResponse productResponse = response.Content.ReadAsAsync<ProductResponse>().Result;
                if(productResponse.Success)
                {
                    //Deleted!
                    LoadProducts();
                    PropertyCopier.CopyProperties(new Product(), productView.CurrentProduct);
                    productView.CurrentProduct.RaiseUpdateProperties();
                    productView.SelectedId = -1;
                }
            }
           
        }
        public void NewProduct()
        {
            PropertyCopier.CopyProperties(new Product(), productView.CurrentProduct);
            productView.CurrentProduct.RaiseUpdateProperties();
            productView.SelectedId = -1;
        }
        public void UpdateCurrentProduct()
        {
            if (productView.SelectedId == -1)
            {
                productView.CurrentProduct = new Product();
            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                HttpResponseMessage response = client.GetAsync(string.Format("{0}?id={1}",getByIdAction, productView.SelectedId)).Result;
                if (response.IsSuccessStatusCode)
                {
                    GetProductResponse getProductResponse = response.Content.ReadAsAsync<GetProductResponse>().Result;
                    if (getProductResponse.Success)
                    {
                        PropertyCopier.CopyProperties(getProductResponse.Products[0], productView.CurrentProduct);
                        productView.CurrentProduct.RaiseUpdateProperties();                       
                    }
                }
            }
        }
   
        public void SaveProduct()
        {
            bool reLoadList = false;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);            
            string action = string.Empty;
            if (productView.CurrentProduct.Id == 0)
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
            HttpResponseMessage response = client.PostAsync(action, productView.CurrentProduct, bsonFormatter).Result;
            response.EnsureSuccessStatusCode();
            MediaTypeFormatter[] formatters = new MediaTypeFormatter[] { bsonFormatter};
            ProductResponse productResponse = response.Content.ReadAsAsync<ProductResponse>(formatters).Result;
            if (productResponse.Success)
            {
                if (productResponse.Product != null)
                {
                    if (reLoadList)
                    {
                        LoadProducts();
                        productView.SelectedId = productResponse.Product.Id;
                    }
                }
            }
            else
            {
                throw new ProductException(productResponse.ErrorCode, productResponse.ErrorMessage);
            }
        }

        public void FilterProductList()
        {
        }

        #endregion

        #region Private methods
   
        private void GetProductsFromApi()
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
                    originalProducts = getSelectableModelResponse.SelectableModels;
                }
                else
                {
                    throw new ProductException(getSelectableModelResponse.ErrorMessage);
                }
            }
            else
            {
                throw new ProductException(ProductError.ApiCommunicationError);
            }
        }
        #endregion
    }
}
