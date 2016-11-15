using LasMargaritas.BL;
using LasMargaritas.BL.Presenters;
using LasMargaritas.BL.Views;
using LasMargaritas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace LasMargaritas.UT.Presenter
{
    [TestClass]
    public class ProducerPresenterTest
    {
        public class DummyProducerView : IProducerView
        {
            public Producer CurrentProducer { get; set; }
            public List<SelectableModel> Producers { get; set; }
            public void HandleException(Exception ex, string method, Guid errorId)
            {             
            }            

            public int SelectedId { get; set; }
            
            public List<SelectableModel> States { get; set; }          

            public List<SelectableModel> CivilStatus { get; set; }
           

            public List<SelectableModel> Regimes { get; set; }
           

            public List<SelectableModel> Genders { get; set; }
            
        }

        string baseUrl;
        public ProducerPresenterTest()
        {
            baseUrl = @"http://lasmargaritasdev.azurewebsites.net/";
            if (ConfigurationManager.AppSettings["baseUrl"] != null)
            {
                baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            }
        }    
    }
}
