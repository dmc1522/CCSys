using LasMargaritas.BL;
using LasMargaritas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
namespace LasMargaritas.ULT
{
    [TestClass]
    public class ProducerBLDLTest
    {
        private string connectionString;
        public ProducerBLDLTest()
        {
            connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDBDev;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
        }
        
        [TestMethod]
        public void TestInsertUpdateAndGetWeightTicket()
        {
            //Test get by Id
            ProducerBL producerBL = new ProducerBL(connectionString);
            List<Producer> producers = producerBL.GetProducer(2);
            Assert.IsTrue(producers.Count() == 1);
            Producer producer = producers[0];
            //Assert.IsTrue(producer.Id == weightTicket.Id); //Todo create custom validator to check all properties
         
        }

      
    }
}
