using LasMargaritas.BL;
using LasMargaritas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
namespace LasMargaritas.ULT
{
    [TestClass]
    public class WareHouseBLDLTest
    {
        private string connectionString;
        public WareHouseBLDLTest()
        {
            connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDB;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
        }

        [TestMethod]
        public void TestInsertUpdateAndGetWareHouse()
        {
            WareHouseBL wareHouseBL = new WareHouseBL(connectionString);
            WareHouse wareHouse = WareHouseHelper.CreateDummyWareHouse();
            wareHouse = wareHouseBL.InsertWareHouse(wareHouse);
            Assert.IsTrue(wareHouse.Id > 0);
            //Test get by Id
            List<WareHouse> savedWareHouses = wareHouseBL.GetWareHouse(wareHouse.Id);
            Assert.IsTrue(savedWareHouses.Count() == 1);
            WareHouse savedWareHouse = savedWareHouses[0];
            Assert.IsTrue(savedWareHouse.Id == wareHouse.Id); //Todo create custom validator to check all properties
            //Test get all
            savedWareHouses = wareHouseBL.GetWareHouse();
            savedWareHouse = (from ticket in savedWareHouses where ticket.Id == savedWareHouse.Id select ticket).FirstOrDefault();
            Assert.IsTrue(savedWareHouse != null);
            //Test update
            wareHouse.Name = "WareHouse Updated";
            wareHouse.Address = "Address Updated"; //Todo check other properties
            wareHouse = wareHouseBL.UpdateWareHouse(wareHouse);
            savedWareHouses = wareHouseBL.GetWareHouse(wareHouse.Id);
            Assert.IsTrue(savedWareHouses != null && savedWareHouses.Count() == 1);
            savedWareHouse = savedWareHouses[0];
            Assert.IsTrue(savedWareHouse.Name == "wareHouseUpdated");
            Assert.IsTrue(savedWareHouse.Address == "Address Updated");
            //Test delete
            bool result = wareHouseBL.DeleteWareHouse(savedWareHouse.Id);
            Assert.IsTrue(result);
            savedWareHouses = wareHouseBL.GetWareHouse(wareHouse.Id);
            Assert.IsTrue(savedWareHouses.Count() == 0);
        }

        [TestMethod]
        public void TestInsertUpdateErrors()
        {
            WareHouseBL wareHouseBL = new WareHouseBL(connectionString);
            WareHouse wareHouse = new WareHouse();
            try
            {
                wareHouseBL.InsertWareHouse(wareHouse);
            }
            catch (WareHouseException exception)
            {
                Assert.IsTrue(exception.Error.HasFlag(WareHouseError.InvalidAddress));             
                Assert.IsTrue(exception.Error.HasFlag(WareHouseError.InvalidName));

            }
            try
            {
                wareHouseBL.UpdateWareHouse(wareHouse);
            }
            catch (WareHouseException exception)
            {
                Assert.IsTrue(exception.Error.HasFlag(WareHouseError.InvalidAddress));
                Assert.IsTrue(exception.Error.HasFlag(WareHouseError.InvalidName));
            }
        }
        [TestMethod]
        public void TestDeleteErrors()
        {
            WareHouseBL wareHouseBL = new WareHouseBL(connectionString);
            try
            {
                wareHouseBL.DeleteWareHouse(0);
            }
            catch (WareHouseException exception)
            {
                Assert.IsTrue(exception.Error.HasFlag(WareHouseError.InvalidId));
            }
        }

        [TestMethod]
        public void TestGetErrors()
        {
            WareHouseBL wareHouseBL = new WareHouseBL(connectionString);
            try
            {
                wareHouseBL.GetWareHouse(0);
            }
            catch (WareHouseException exception)
            {
                Assert.IsTrue(exception.Error.HasFlag(WareHouseError.InvalidId));
            }
        }
    }
}
