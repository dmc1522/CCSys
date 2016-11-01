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
            

            public List<Producer> Producer
            {
                get; set;
            }
        }

        string baseUrl;
        public ProducerPresenterTest()
        {
            baseUrl = @"http://lasmargaritas.azurewebsites.net/";
        }
        
        [TestMethod]
        public void TestLoadProducers()
        {           
            //Clean local caché
            if(File.Exists("producers.json"))
            {
                File.Delete("producers.json");
            }
            if (File.Exists("lastModificationProducers.json"))
            {
                File.Delete("lastModificationProducers.json");
            }
            //Load producers            
            Token token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3");
            ProducerPresenter presenter = new ProducerPresenter(new DummyProducerView());
            presenter.Token = token;
            presenter.LoadProducers();
            Assert.IsFalse(presenter.IsDataFromCache);
            presenter.LoadProducers();
            Assert.IsTrue(presenter.IsDataFromCache);
            //Load producers again


        }       
    }
}
