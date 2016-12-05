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
    public class WareHousePresenter
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

        private List<SelectableModel> filterWareHouses;
        private List<SelectableModel> originalWareHouses;
        IWareHouseView rancherView;
        #endregion

        #region Constructor
        public WareHousePresenter(IWareHouseView view)
        {
            baseUrl = @"http://lasmargaritasdev.azurewebsites.net/";
            if (ConfigurationManager.AppSettings["baseUrl"] != null)
            {
                baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            }
     
            insertAction = "WareHouse/Add";
            updateAction = "WareHouse/Update";
            deleteAction = "WareHouse/Delete";
            getAllAction = "WareHouse/GetAll";
            getByIdAction = "WareHouse/GetById";
            getSelectableModelsAction = "WareHouse/GetSelectableModels";
            rancherView = view;

        }
        #endregion

        #region Public properties       
        public Token Token { get; set; }

        #endregion

        #region Private Properties

        #endregion


        #region Private methods

        private void LoadWareHouses()
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
                GetWareHousesFromApi();
                rancherView.WareHouses = originalWareHouses;
              
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(0);
                MethodBase currentMethodName = sf.GetMethod();
                Guid errorId = Guid.NewGuid();
                //Log error here
                rancherView.HandleException(ex, currentMethodName.Name, errorId);
            }

        }

      /*  private void GetCatalogs()
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
                    rancherView.States = getSelectableModelResponse.SelectableModels;

                }
            }
           
        }*/
        #endregion


        #region Public methods
        public void Initialize()
        {
            //GetCatalogs();
            LoadWareHouses();
        }

        public void DeleteWareHouse()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            HttpResponseMessage response = client.PostAsJsonAsync(deleteAction, new IdModel(rancherView.SelectedId)).Result;
            if (response.IsSuccessStatusCode)
            {
                WareHouseResponse rancherResponse = response.Content.ReadAsAsync<WareHouseResponse>().Result;
                if(rancherResponse.Success)
                {
                    //Deleted!
                    LoadWareHouses();
                    PropertyCopier.CopyProperties(new WareHouse(), rancherView.CurrentWareHouse);
                    rancherView.CurrentWareHouse.RaiseUpdateProperties();
                    rancherView.SelectedId = -1;
                }
            }
           
        }
        public void NewWareHouse()
        {
            PropertyCopier.CopyProperties(new WareHouse(), rancherView.CurrentWareHouse);
            rancherView.CurrentWareHouse.RaiseUpdateProperties();
            rancherView.SelectedId = -1;
        }
        public void UpdateCurrentWareHouse()
        {
            if (rancherView.SelectedId == -1)
            {
                rancherView.CurrentWareHouse = new WareHouse();
            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                HttpResponseMessage response = client.GetAsync(string.Format("{0}?id={1}",getByIdAction, rancherView.SelectedId)).Result;
                if (response.IsSuccessStatusCode)
                {
                    GetWareHouseResponse getWareHouseResponse = response.Content.ReadAsAsync<GetWareHouseResponse>().Result;
                    if (getWareHouseResponse.Success)
                    {
                        PropertyCopier.CopyProperties(getWareHouseResponse.WareHouses[0], rancherView.CurrentWareHouse);
                        rancherView.CurrentWareHouse.RaiseUpdateProperties();                       
                    }
                }
            }
        }
   
        public void SaveWareHouse()
        {
            bool reLoadList = false;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);            
            string action = string.Empty;
            if (rancherView.CurrentWareHouse.Id == 0)
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
            HttpResponseMessage response = client.PostAsync(action, rancherView.CurrentWareHouse, bsonFormatter).Result;
            response.EnsureSuccessStatusCode();
            MediaTypeFormatter[] formatters = new MediaTypeFormatter[] { bsonFormatter};
            WareHouseResponse rancherResponse = response.Content.ReadAsAsync<WareHouseResponse>(formatters).Result;
            if (rancherResponse.Success)
            {
                if (rancherResponse.WareHouse != null)
                {
                    if (reLoadList)
                    {
                        LoadWareHouses();
                        rancherView.SelectedId = rancherResponse.WareHouse.Id;
                    }
                }
            }
            else
            {
                throw new WareHouseException(rancherResponse.ErrorCode, rancherResponse.ErrorMessage);
            }
        }

        public void FilterWareHouseList()
        {
        }

        #endregion

        #region Private methods
   
        private void GetWareHousesFromApi()
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
                    originalWareHouses = getSelectableModelResponse.SelectableModels;
                }
                else
                {
                    throw new WareHouseException(getSelectableModelResponse.ErrorMessage);
                }
            }
            else
            {
                throw new WareHouseException(WareHouseError.ApiCommunicationError);
            }
        }
        #endregion
    }
}
