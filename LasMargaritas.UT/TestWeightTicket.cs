using LasMargaritas.BL;
using LasMargaritas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace LasMargaritas.ULT
{
    [TestClass]
    public class TestWeightTicket
    {
        private string connectionString;
        public TestWeightTicket()
        {
            connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDB;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
        }
        [TestMethod]
        public void TestInsertAndGetWeightTicket()
        {
            WeightTicketsBL weightTicketBL = new WeightTicketsBL(connectionString);
            WeightTicket weightTicket = new WeightTicket();
            weightTicket.CicleId = 0;
            weightTicket.EntranceWeightKg = 2;
            weightTicket.ExitWeightKg = 1;
            weightTicket.Number = "Test1";
            weightTicket = weightTicketBL.InsertWeightTicket(weightTicket);
            Assert.IsTrue(weightTicket.Id > 0);


        }
    }
}
