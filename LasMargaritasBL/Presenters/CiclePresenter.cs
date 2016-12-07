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
    public class CiclePresenter
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

        private List<SelectableModel> filterCicles;
        private List<SelectableModel> originalCicles;
        ICicleView cicleView;
        #endregion

        #region Constructor
        public CiclePresenter(ICicleView view)
        {
            baseUrl = @"http://lasmargaritasdev.azurewebsites.net/";
            if (ConfigurationManager.AppSettings["baseUrl"] != null)
            {
                baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            }
     
            insertAction = "Cicle/Add";
            updateAction = "Cicle/Update";
            deleteAction = "Cicle/Delete";
            getAllAction = "Cicle/GetAll";
            getByIdAction = "Cicle/GetById";
            getSelectableModelsAction = "Cicle/GetSelectableModels";            
            cicleView = view;

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
            try
            {
                if (Token == null)
                    throw new InvalidOperationException("Login first");
                //Get last remote modification
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                GetCiclesFromApi();
                cicleView.Cicles = originalCicles;
              
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(0);
                MethodBase currentMethodName = sf.GetMethod();
                Guid errorId = Guid.NewGuid();
                //Log error here
                cicleView.HandleException(ex, currentMethodName.Name, errorId);
            }

        }

       /* private void GetCatalogs()
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
                    cicleView.States = getSelectableModelResponse.SelectableModels;

                }
            }
           
        }*/
        #endregion


        #region Public methods
        public void Initialize()
        {
            // Cicle does not have any Catalogs to load
            //GetCatalogs(); 
            LoadCicles();
        }

        public void DeleteCicle()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            HttpResponseMessage response = client.PostAsJsonAsync(deleteAction, new IdModel(cicleView.SelectedId)).Result;
            if (response.IsSuccessStatusCode)
            {
                CicleResponse cicleResponse = response.Content.ReadAsAsync<CicleResponse>().Result;
                if(cicleResponse.Success)
                {
                    //Deleted!
                    LoadCicles();
                    PropertyCopier.CopyProperties(new Cicle(), cicleView.CurrentCicle);
                    cicleView.CurrentCicle.RaiseUpdateProperties();
                    cicleView.SelectedId = -1;
                }
            }
           
        }
        public void NewCicle()
        {
            PropertyCopier.CopyProperties(new Cicle(), cicleView.CurrentCicle);
            cicleView.CurrentCicle.RaiseUpdateProperties();
            cicleView.SelectedId = -1;
        }
        public void UpdateCurrentCicle()
        {
            if (cicleView.SelectedId == -1)
            {
                cicleView.CurrentCicle = new Cicle();
            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
                HttpResponseMessage response = client.GetAsync(string.Format("{0}?id={1}",getByIdAction, cicleView.SelectedId)).Result;
                if (response.IsSuccessStatusCode)
                {
                    GetCicleResponse getCicleResponse = response.Content.ReadAsAsync<GetCicleResponse>().Result;
                    if (getCicleResponse.Success)
                    {
                        PropertyCopier.CopyProperties(getCicleResponse.Cicles[0], cicleView.CurrentCicle);
                        cicleView.CurrentCicle.RaiseUpdateProperties();                       
                    }
                }
            }
        }
   
        public void SaveCicle()
        {
            bool reLoadList = false;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);            
            string action = string.Empty;
            if (cicleView.CurrentCicle.Id == 0)
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
            HttpResponseMessage response = client.PostAsync(action, cicleView.CurrentCicle, bsonFormatter).Result;
            response.EnsureSuccessStatusCode();
            MediaTypeFormatter[] formatters = new MediaTypeFormatter[] { bsonFormatter};
            CicleResponse cicleResponse = response.Content.ReadAsAsync<CicleResponse>(formatters).Result;
            if (cicleResponse.Success)
            {
                if (cicleResponse.Cicle != null)
                {
                    if (reLoadList)
                    {
                        LoadCicles();
                        cicleView.SelectedId = cicleResponse.Cicle.Id;
                    }
                }
            }
            else
            {
                throw new CicleException(cicleResponse.ErrorCode, cicleResponse.ErrorMessage);
            }
        }

        public void FilterCicleList()
        {
        }

        #endregion

        #region Private methods
   
        private void GetCiclesFromApi()
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
                    originalCicles = getSelectableModelResponse.SelectableModels;
                }
                else
                {
                    throw new CicleException(getSelectableModelResponse.ErrorMessage);
                }
            }
            else
            {
                throw new CicleException(CicleError.ApiCommunicationError);
            }
        }
        #endregion
    }
}
