using LasMargaritas.BL;
using LasMargaritas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
namespace LasMargaritas.ULT
{
    [TestClass]
    public class CicleBLDLTest
    {
        private string connectionString;
        public CicleBLDLTest()
        {
            connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDB;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
        }

        [TestMethod]
        public void TestInsertUpdateAndGetCicle()
        {
            CicleBL cicleBL = new CicleBL(connectionString);
            Cicle cicle = CicleHelper.CreateDummyCicle();
            cicle = cicleBL.InsertCicle(cicle);
            Assert.IsTrue(cicle.Id > 0);
            //Test get by Id
            List<Cicle> savedCicles = cicleBL.GetCicle(cicle.Id);
            Assert.IsTrue(savedCicles.Count() == 1);
            Cicle savedCicle = savedCicles[0];
            Assert.IsTrue(savedCicle.Id == cicle.Id); //Todo create custom validator to check all properties
            //Test get all
            savedCicles = cicleBL.GetCicle();
            savedCicle = (from ticket in savedCicles where ticket.Id == savedCicle.Id select ticket).FirstOrDefault();
            Assert.IsTrue(savedCicle != null);
            //Test update
            cicle.Name = "cicleUpdated";
            cicle.Closed = true; //Todo check other properties
            cicle = cicleBL.UpdateCicle(cicle);
            savedCicles = cicleBL.GetCicle(cicle.Id);
            Assert.IsTrue(savedCicles != null && savedCicles.Count() == 1);
            savedCicle = savedCicles[0];
            Assert.IsTrue(savedCicle.Name == "cicleUpdated");
            Assert.IsTrue(savedCicle.Closed);
            //Test delete
            bool result = cicleBL.DeleteCicle(savedCicle.Id);
            Assert.IsTrue(result);
            savedCicles = cicleBL.GetCicle(cicle.Id);
            Assert.IsTrue(savedCicles.Count() == 0);
        }

        [TestMethod]
        public void TestInsertUpdateErrors()
        {
            CicleBL cicleBL = new CicleBL(connectionString);
            Cicle cicle = new Cicle();
            try
            {
                cicleBL.InsertCicle(cicle);
            }
            catch (CicleException exception)
            {
                Assert.IsTrue(exception.Error.HasFlag(CicleError.InvalidAmount));
                Assert.IsTrue(exception.Error.HasFlag(CicleError.InvalidDateZone1));
                Assert.IsTrue(exception.Error.HasFlag(CicleError.InvalidDateZone2));
                Assert.IsTrue(exception.Error.HasFlag(CicleError.InvalidIsClosed));
                Assert.IsTrue(exception.Error.HasFlag(CicleError.InvalidStartDate));
                Assert.IsTrue(exception.Error.HasFlag(CicleError.InvalidName));
                
            }
            try
            {
                cicleBL.UpdateCicle(cicle);
            }
            catch (CicleException exception)
            {
                Assert.IsTrue(exception.Error.HasFlag(CicleError.InvalidAmount));
                Assert.IsTrue(exception.Error.HasFlag(CicleError.InvalidDateZone1));
                Assert.IsTrue(exception.Error.HasFlag(CicleError.InvalidDateZone2));
                Assert.IsTrue(exception.Error.HasFlag(CicleError.InvalidIsClosed));
                Assert.IsTrue(exception.Error.HasFlag(CicleError.InvalidStartDate));
                Assert.IsTrue(exception.Error.HasFlag(CicleError.InvalidName));
            }
        }
        [TestMethod]
        public void TestDeleteErrors()
        {
            CicleBL cicleBL = new CicleBL(connectionString);
            try
            {
                cicleBL.DeleteCicle(0);
            }
            catch (CicleException exception)
            {
                Assert.IsTrue(exception.Error.HasFlag(CicleError.InvalidId));
            }
        }

        [TestMethod]
        public void TestGetErrors()
        {
            CicleBL cicleBL = new CicleBL(connectionString);
            try
            {
                cicleBL.GetCicle(0);
            }
            catch (CicleException exception)
            {
                Assert.IsTrue(exception.Error.HasFlag(CicleError.InvalidId));
            }
        }
    }
}
