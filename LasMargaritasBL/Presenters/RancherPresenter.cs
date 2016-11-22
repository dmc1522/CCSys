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
    public class RancherPresenter
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

        private List<SelectableModel> filterRanchers;
        private List<SelectableModel> originalRanchers;
        IRancherView rancherView;
        #endregion

        #region Constructor
        public RancherPresenter(IRancherView view)
        {
            baseUrl = @"http://lasmargaritasdev.azurewebsites.net/";
            if (ConfigurationManager.AppSettings["baseUrl"] != null)
            {
                baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            }
     
            insertAction = "Rancher/Add";
            updateAction = "Rancher/Update";
            deleteAction = "Rancher/Delete";
            getAllAction = "Rancher/GetAll";
            getByIdAction = "Rancher/GetById";
            getSelectableModelsAction = "Rancher/GetSelectableModels";
            getStatesAction = "Catalog/GetStates";
            rancherView = view;

        }
        #endregion

        #region Public properties       
        public Token Token { get; set; }

        #endregion

        #region Private Properties

        #endregion


        #region Private methods

        private void LoadRanchers()
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
                GetRanchersFromApi();
                rancherView.Ranchers = originalRanchers;
              
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
                    rancherView.States = getSelectableModelResponse.SelectableModels;

                }
            }
           
        }
        #endregion


        #region Public methods
        public void Initialize()
        {
            GetCatalogs();
            LoadRanchers();
        }

        public void DeleteRancher()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            HttpResponseMessage response = client.PostAsJsonAsync(deleteAction, new IdModel(rancherView.SelectedId)).Result;
            if (response.IsSuccessStatusCode)
            {
                RancherResponse rancherResponse = response.Content.ReadAsAsync<RancherResponse>().Result;
                if(rancherResponse.Success)
                {
                    //Deleted!
                    LoadRanchers();
                    PropertyCopier.CopyProperties(new Rancher(), rancherView.CurrentRancher);
                    rancherView.CurrentRancher.RaiseUpdateProperties();
                    rancherView.SelectedId = -1;
                }
            }
           
        }
        public void NewRancher()
        {
            PropertyCopier.CopyProperties(new Rancher(), rancherView.CurrentRancher);
            rancherView.CurrentRancher.RaiseUpdateProperties();
            rancherView.SelectedId = -1;
        }
        public void UpdateCurrentRancher()
        {
            if (rancherView.SelectedId == -1)
            {
                rancherView.CurrentRancher = new Rancher();
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
                    GetRancherResponse getRancherResponse = response.Content.ReadAsAsync<GetRancherResponse>().Result;
                    if (getRancherResponse.Success)
                    {
                        PropertyCopier.CopyProperties(getRancherResponse.Ranchers[0], rancherView.CurrentRancher);
                        rancherView.CurrentRancher.RaiseUpdateProperties();                       
                    }
                }
            }
        }
   
        public void SaveRancher()
        {
            bool reLoadList = false;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);            
            string action = string.Empty;
            if (rancherView.CurrentRancher.Id == 0)
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
            HttpResponseMessage response = client.PostAsync(action, rancherView.CurrentRancher, bsonFormatter).Result;
            response.EnsureSuccessStatusCode();
            MediaTypeFormatter[] formatters = new MediaTypeFormatter[] { bsonFormatter};
            RancherResponse rancherResponse = response.Content.ReadAsAsync<RancherResponse>(formatters).Result;
            if (rancherResponse.Success)
            {
                if (rancherResponse.Rancher != null)
                {
                    if (reLoadList)
                    {
                        LoadRanchers();
                        rancherView.SelectedId = rancherResponse.Rancher.Id;
                    }
                }
            }
            else
            {
                throw new RancherException(rancherResponse.ErrorCode, rancherResponse.ErrorMessage);
            }
        }

        public void FilterRancherList()
        {
        }

        #endregion

        #region Private methods
   
        private void GetRanchersFromApi()
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
                    originalRanchers = getSelectableModelResponse.SelectableModels;
                }
                else
                {
                    throw new RancherException(getSelectableModelResponse.ErrorMessage);
                }
            }
            else
            {
                throw new RancherException(RancherError.ApiCommunicationError);
            }
        }
        #endregion
    }
}
