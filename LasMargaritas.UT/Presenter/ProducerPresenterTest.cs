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
            public int SelectedId { get { return 1; } }
        }

        string baseUrl;
        public ProducerPresenterTest()
        {
            baseUrl = @"http://lasmargaritas.azurewebsites.net/";
        }
        
        [TestMethod]
        public void TestLoadSaveAndFilterProducers()
        {           
            //Clean local caché
            if(File.Exists("producers.json"))
            {
                File.Delete("producers.json");
            }          
            //Load producers            
            Token token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3");
            ProducerPresenter presenter = new ProducerPresenter(new DummyProducerView());
            presenter.Token = token;
            presenter.LoadProducers();
            DateTime? lastSynchronization = presenter.LastSynchronizationTimeStamp;
            Thread.Sleep(2000);
            presenter.LoadProducers();
            //Verify no synchronization happened, and cached version was returned
            Assert.IsTrue(lastSynchronization.Equals(presenter.LastSynchronizationTimeStamp));
            //TODO insert a new producer and verify
        }       
    }
}
