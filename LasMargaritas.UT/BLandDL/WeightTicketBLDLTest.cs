using LasMargaritas.BL;
using LasMargaritas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
namespace LasMargaritas.ULT
{
    [TestClass]
    public class WeightTicketBLDLTest
    {
        private string connectionString;
        public WeightTicketBLDLTest()
        {
            connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDBDev;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
        }

        [TestMethod]
        public void TestGetWeightTicketReport()
        {
            WeightTicketsBL weightTicketBL = new WeightTicketsBL(connectionString);
            WeightTicketReportFilterModel filter = new WeightTicketReportFilterModel();
            filter.CicleId = 1;
            filter.WeightTicketType = 1;
            List<List<ReportDataItem>> data = weightTicketBL.GetWeightTicketsReport(filter);

        }
        [TestMethod]
        public void TestInsertUpdateAndGetWeightTicket()
        {
            WeightTicketsBL weightTicketBL = new WeightTicketsBL(connectionString);
            WeightTicket weightTicket = WeightTicketHelper.CreateDummyTicket();
            weightTicket = weightTicketBL.InsertWeightTicket(weightTicket);
            Assert.IsTrue(weightTicket.Id > 0);
            //Test get by Id
            List<WeightTicket> savedTickets = weightTicketBL.GetWeightTicket(weightTicket.Id);
            Assert.IsTrue(savedTickets.Count() == 1);
            WeightTicket savedTicket = savedTickets[0];
            Assert.IsTrue(savedTicket.Id == weightTicket.Id); //Todo create custom validator to check all properties
            //Test get all
            savedTickets = weightTicketBL.GetWeightTicket();
            savedTicket = (from ticket in savedTickets where ticket.Id == savedTicket.Id select ticket).FirstOrDefault();
            Assert.IsTrue(savedTicket != null);
            //Test update
            weightTicket.SubTotal = 20;
            weightTicket.ApplyDrying = false; //Todo check other properties
            weightTicket = weightTicketBL.UpdateWeightTicket(weightTicket);
            savedTickets = weightTicketBL.GetWeightTicket(weightTicket.Id);
            Assert.IsTrue(savedTickets != null && savedTickets.Count() == 1);
            savedTicket = savedTickets[0];            
            Assert.IsTrue(savedTicket.SubTotal == 20);
            Assert.IsTrue(!savedTicket.ApplyDrying);
            //Test delete
            bool result = weightTicketBL.DeleteWeightTicket(savedTicket.Id);
            Assert.IsTrue(result);
            savedTickets = weightTicketBL.GetWeightTicket(weightTicket.Id);
            Assert.IsTrue(savedTickets.Count() == 0);
        }

        [TestMethod]
        public void TestInsertUpdateErrors()
        {
            WeightTicketsBL weightTicketBL = new WeightTicketsBL(connectionString);
            WeightTicket weightTicket = new WeightTicket();
            try
            {
                weightTicketBL.InsertWeightTicket(weightTicket);
            }
            catch (WeightTicketException exception)
            {
                Assert.IsTrue(exception.Error.HasFlag(WeightTicketError.InvalidCicle));
                Assert.IsTrue(exception.Error.HasFlag(WeightTicketError.InvalidFolio));
                Assert.IsTrue(exception.Error.HasFlag(WeightTicketError.InvalidProducer));
                Assert.IsTrue(exception.Error.HasFlag(WeightTicketError.InvalidWareHouse));
                Assert.IsTrue(exception.Error.HasFlag(WeightTicketError.InvalidProduct));
            }
            try
            {
                weightTicketBL.UpdateWeightTicket(weightTicket);
            }
            catch (WeightTicketException exception)
            {
                Assert.IsTrue(exception.Error.HasFlag(WeightTicketError.InvalidCicle));
                Assert.IsTrue(exception.Error.HasFlag(WeightTicketError.InvalidFolio));
                Assert.IsTrue(exception.Error.HasFlag(WeightTicketError.InvalidProducer));
                Assert.IsTrue(exception.Error.HasFlag(WeightTicketError.InvalidWareHouse));
                Assert.IsTrue(exception.Error.HasFlag(WeightTicketError.InvalidProduct));
            }
        }
        [TestMethod]
        public void TestDeleteErrors()
        {
            WeightTicketsBL weightTicketBL = new WeightTicketsBL(connectionString);
            try
            {
                weightTicketBL.DeleteWeightTicket(0);
            }
            catch (WeightTicketException exception)
            {
                Assert.IsTrue(exception.Error.HasFlag(WeightTicketError.InvalidId));
            }
        }

        [TestMethod]
        public void TestGetErrors()
        {
            WeightTicketsBL weightTicketBL = new WeightTicketsBL(connectionString);
            try
            {
                weightTicketBL.GetWeightTicket(0);
            }
            catch (WeightTicketException exception)
            {
                Assert.IsTrue(exception.Error.HasFlag(WeightTicketError.InvalidId));
            }
        }
    }
}
