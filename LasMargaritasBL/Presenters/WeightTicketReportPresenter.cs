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
      /*  #region Private variables
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
        private int currentPage;
        private bool printingFirstPart;
        #endregion

        #region Constructor
        public WeightTicketReportPresenter(IWeightTicketReportView view)
        {
            currentPage = 0;
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
            getSalesCustomersAction = "SaleCustomer/GetSelectableModels";
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

        public void LoadWeightTickets()
        {
            try
            {
                if (Token == null)
                    throw new InvalidOperationException("Login first");
                GetWeightTicketsFromApi();
                view.WeightTickets = currentTickets;     
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

        public void LoadProducts()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            HttpResponseMessage response;
            //Products
            string action = string.Format("{0}?type={1}", getProductsAction, (int)view.WeightTicketType);
            response = client.GetAsync(action).Result;
            if (response.IsSuccessStatusCode)
            {
                GetSelectableModelResponse getSelectableModelResponse = response.Content.ReadAsAsync<GetSelectableModelResponse>().Result;
                if (getSelectableModelResponse.Success)
                {
                    view.Products = getSelectableModelResponse.SelectableModels;
                }
            }
        }
        public void LoadCatalogs()
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
             //Ranchers
             response = client.GetAsync(getRanchersAction).Result;
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
             }

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
        public void CalculateTotals()
        {
            if (view.CurrentWeightTicket.IsEntranceWeightTicket)
            {
                view.CurrentWeightTicket.EntranceNetWeight = view.CurrentWeightTicket.EntranceWeightKg - view.CurrentWeightTicket.ExitWeightKg;
                view.CurrentWeightTicket.ExitNetWeight = 0;
                view.CurrentWeightTicket.NetWeight = view.CurrentWeightTicket.EntranceNetWeight;
            }
            else
            {
                view.CurrentWeightTicket.ExitNetWeight = view.CurrentWeightTicket.ExitWeightKg - view.CurrentWeightTicket.EntranceWeightKg;
                view.CurrentWeightTicket.EntranceNetWeight = 0;
                view.CurrentWeightTicket.NetWeight = view.CurrentWeightTicket.ExitNetWeight;
            }
            if (view.CurrentWeightTicket.ApplyHumidity)
            {
                view.CurrentWeightTicket.HumidityDiscount = DiscountCalculator.GetDiscount(DiscountType.Humidity, view.CurrentWeightTicket.Humidity, view.CurrentWeightTicket.NetWeight, view.CurrentWeightTicket.IsEntranceWeightTicket);
            }
            else
            {
                view.CurrentWeightTicket.HumidityDiscount = 0;
            }
            if (view.CurrentWeightTicket.ApplyImpurities)
            {
                view.CurrentWeightTicket.ImpuritiesDiscount = DiscountCalculator.GetDiscount(DiscountType.Impurities, view.CurrentWeightTicket.Impurities, view.CurrentWeightTicket.NetWeight, view.CurrentWeightTicket.IsEntranceWeightTicket);
            }
            else
            {
                view.CurrentWeightTicket.ImpuritiesDiscount = 0;
            }
            view.CurrentWeightTicket.TotalWeightToPay = view.CurrentWeightTicket.NetWeight - view.CurrentWeightTicket.HumidityDiscount - view.CurrentWeightTicket.ImpuritiesDiscount;
            view.CurrentWeightTicket.SubTotal = view.CurrentWeightTicket.Price * (decimal)view.CurrentWeightTicket.TotalWeightToPay;
            if (view.CurrentWeightTicket.ApplyDrying)
            {
                view.CurrentWeightTicket.DryingDiscount = DiscountCalculator.GetDiscount(DiscountType.Drying, view.CurrentWeightTicket.Humidity, view.CurrentWeightTicket.NetWeight, view.CurrentWeightTicket.IsEntranceWeightTicket);
            }
            else
            {
                view.CurrentWeightTicket.DryingDiscount = 0;
            }
            view.CurrentWeightTicket.TotalToPay = view.CurrentWeightTicket.SubTotal - (decimal)view.CurrentWeightTicket.DryingDiscount
                                                  - (decimal)view.CurrentWeightTicket.BrokenGrainDiscount - (decimal)view.CurrentWeightTicket.CrashedGrainDiscount 
                                                  - (decimal)view.CurrentWeightTicket.DamagedGrainDiscount - (decimal)view.CurrentWeightTicket.SmallGrainDiscount;
            view.CurrentWeightTicket.RaiseUpdateProperties();
        }
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
       
        public void ReloadWeightTicketsList()
        {
            LoadWeightTickets();
            PropertyCopier.CopyProperties(new WeightTicket(), view.CurrentWeightTicket,true);
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
            this.printingFirstPart = false;
            view.ObtainEntranceWeightEnable = true;
            view.ObtainEntranceWeightEnable = true;
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
                        PropertyCopier.CopyProperties(getWeightTicketResponse.WeightTickets[0], view.CurrentWeightTicket,true);                     
                    }
                }
            }
            view.CurrentWeightTicket.RaiseUpdateProperties();
        }

        public void PrintFirstPart()
        {
            SaveWeightTicket();
            printingFirstPart = true;
            if (view.CurrentWeightTicket.EntranceWeightKg > 0)
                view.ObtainEntranceWeightEnable = false;
            else
                view.ObtainEntranceWeightEnable = true;
            currentPage = 0;
            PrintDocument printDocument = new PrintDocument();
            printDocument.DocumentName = "Boleta: " + view.CurrentWeightTicket.Folio + "3";
            PrintDialog pd = new PrintDialog();
            printDocument.PrinterSettings.PrinterName = "tickets";//TODO Change!
            pd.Document = printDocument;
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            PaperSize ps = new PaperSize();
            ps.Width = 425;
            ps.Height = 551;
            printDocument.DefaultPageSettings.PaperSize = ps;
            printDocument.Print();
        }

        public void PrintSecondPart()
        {
            SaveWeightTicket();
            printingFirstPart = false;
            if (view.CurrentWeightTicket.EntranceWeightKg > 0)
                view.ObtainEntranceWeightEnable = false;
            else
                view.ObtainEntranceWeightEnable = true;
            currentPage = 0;
            PrintDocument printDocument = new PrintDocument();
            printDocument.DocumentName = "Boleta: " + view.CurrentWeightTicket.Folio+ "3";
            printDocument.PrinterSettings.PrinterName = "tickets";//TODO Change!
            PrintDialog pd = new PrintDialog();
            pd.Document = printDocument;
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            PaperSize ps = new PaperSize();
            ps.Width = 425;
            ps.Height = 551;
            printDocument.DefaultPageSettings.PaperSize = ps;
            printDocument.Print();
        }

        void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.HasMorePages = ++currentPage < e.PageSettings.PrinterSettings.Copies;
            Graphics g = e.Graphics;
            Brush brush = Brushes.Black;
            g.PageUnit = GraphicsUnit.Millimeter;

            float fAjusteX = 0.0f;
            float fAjusteY = 0.0f;
            double fFontSize = 8;
            try
            {
                System.Drawing.Font fnt = new System.Drawing.Font("Arial", (float)fFontSize, GraphicsUnit.Point);
                System.Drawing.Font fntDetalle = new System.Drawing.Font("Arial", (float)fFontSize, GraphicsUnit.Point);


                String sText = string.Empty;
                float px, py;
                //fecha dia 
                px = 0.0f + fAjusteX;
                py = 10.0f + fAjusteY;
                SizeF fontSize = g.MeasureString("TEST", fntDetalle);
                if (printingFirstPart)
                {
                    sText = "BOLETA ID: " + view.CurrentWeightTicket.Id.ToString() + " Ticket: " + view.CurrentWeightTicket.Folio;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    fontSize = g.MeasureString(sText.ToUpper(), fntDetalle);
                    sText = view.CurrentBuyerSaler;
                    py += fontSize.Height;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    sText = "PLACAS: " + view.CurrentWeightTicket.Plate;
                    py += fontSize.Height;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    sText = "CHOFER: ";
                    py += fontSize.Height;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    sText = view.CurrentWeightTicket.Driver;
                    py += fontSize.Height;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    sText = "PRODUCTO: ";
                    py += fontSize.Height;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    sText = view.CurrentProduct;
                    py += fontSize.Height;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    sText = "PRIMERA PESADA";
                    py += fontSize.Height * 2;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    sText = "FECHA: " + view.CurrentWeightTicket.EntranceDate.Value.ToString("dd/MM/yyyy HH:mm:ss");
                    py += fontSize.Height;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    sText = "PESO: " + view.CurrentWeightTicket.EntranceWeightKg.ToString() + " Kg";
                    py += fontSize.Height;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                }
                else
                {
                    py += (fontSize.Height * 12);
                    sText = "SEGUNDA PESADA   Ticket: " + view.CurrentWeightTicket.Folio;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    py += fontSize.Height;
                    sText = "FECHA: " + view.CurrentWeightTicket.ExitDate.Value.ToString("dd/MM/yyyy HH:mm:ss");
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    py += fontSize.Height;
                    sText = "PESO: " + view.CurrentWeightTicket.ExitWeightKg + " Kg";
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);

                    py += fontSize.Height;
                    sText = "PESO NETO: " + view.CurrentWeightTicket.NetWeight+ " Kg";
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);

                    py += fontSize.Height;
                    if (!(view.WeightTicketType == WeightTicketType.Rancher))
                    {                        
                        if (view.CurrentWeightTicket.Humidity > 0)
                        {
                            sText = "HUMEDAD: " + view.CurrentWeightTicket.Humidity.ToString("N2");
                        }
                        else
                        {
                            sText = "HUMEDAD: ________";
                        }
                        g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                        py += fontSize.Height;

                        if (view.CurrentWeightTicket.Impurities > 0)
                        {
                            sText = "IMPUREZAS: " + view.CurrentWeightTicket.Impurities.ToString("N2");
                        }
                        else
                        {
                            sText = "IMPUREZAS: ________";
                        }
                        g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    }
                    else
                    {
                        sText = "CABEZAS: " + view.CurrentWeightTicket.Cattle.ToString();
                        g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    }                    
                }
            }
            catch (System.Exception ex)
            {
               //TODO LOG!
            }
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
           

        private void GetWeightTicketsFromApi()
        {
            //get all 
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            string action = string.Format("{0}?cicleId={1}", getAllAction, view.SelectedFilterCicleId);
            HttpResponseMessage response = client.GetAsync(action).Result;
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
        #endregion*/
    }
}

