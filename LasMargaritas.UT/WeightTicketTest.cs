using LasMargaritas.BL;
using LasMargaritas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
namespace LasMargaritas.ULT
{
    [TestClass]
    public class WeightTicketTest
    {
        private string connectionString;
        public WeightTicketTest()
        {
            connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDB;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
        }
        [TestMethod]
        public void TestInsertUpdateAndGetWeightTicket()
        {
            WeightTicketsBL weightTicketBL = new WeightTicketsBL(connectionString);
            //Test insert
            WeightTicket weightTicket = new WeightTicket();
            weightTicket.Amount = 10000;
            weightTicket.ApplyDrying = true;
            weightTicket.ApplyHumidity = true;
            weightTicket.ApplyImpurities = true;
            weightTicket.BrokenGrainDiscount = 10;
            weightTicket.CicleId = 1;
            weightTicket.CrashedGrainDiscount = 10;
            weightTicket.DamagedGrainDiscount = 10;
            weightTicket.Driver = "Driver";
            weightTicket.DryingDiscount = 10;
            weightTicket.EntranceDate = DateTime.Now;
            weightTicket.EntranceNetWeight = 1000;
            weightTicket.EntranceWeigher = "Weigher";
            weightTicket.EntranceWeightKg = 1500;
            weightTicket.ExitWeightKg = 1000;
            weightTicket.ExitDate = DateTime.Now;
            weightTicket.ExitNetWeight = 200;
            weightTicket.ExitWeigher = "Weigher";
            weightTicket.Folio = "Folio" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            weightTicket.Humidity = 10;
            weightTicket.HumidityDiscount = 10;
            weightTicket.Impurities = 10;
            weightTicket.ImpuritiesDiscount = 10;
            weightTicket.NetWeight = 400;
            weightTicket.Number = "Number" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            weightTicket.Paid = false;
            weightTicket.Plate = "Plate";
            weightTicket.Price = 10;
            weightTicket.ProducerId = 1;
            weightTicket.ProductId = 1;
            weightTicket.SmallGrainDiscount = 10;
            weightTicket.StoreTs = DateTime.Now;
            weightTicket.TotalDiscount = 50;
            weightTicket.TotalToPay = 100;
            weightTicket.UpdateTs = DateTime.Now;
            weightTicket.UserId = "35d360f3-c296-4113-ab34-9b91fe729c18";
            weightTicket.WarehouseId = 1;
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
            weightTicket.Amount = 20;
            weightTicket.ApplyDrying = false; //Todo check other properties
            weightTicket = weightTicketBL.UpdateWeightTicket(weightTicket);
            savedTickets = weightTicketBL.GetWeightTicket(weightTicket.Id);
            Assert.IsTrue(savedTickets != null && savedTickets.Count() == 1);
            savedTicket = savedTickets[0];
            Assert.IsTrue(savedTicket.Amount == 20);
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
