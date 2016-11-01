using LasMargaritas.BL;
using LasMargaritas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasMargaritas.UT.BLandDL
{
    [TestClass]
    public class LastModificationBLDLTest
    {
        private string connectionString;
        public LastModificationBLDLTest()
        {
            connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDB;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
        }

        [TestMethod]
        public void TestSetAndGetLastModification()
        {
            LastModificationBL bl = new LastModificationBL(connectionString);
            DateTime startTime = DateTime.Now;
            bl.SetLastModification(new LastModification() { Module = Module.Producers });
            LastModification modification = bl.GetLastModification(Module.Producers);
            Assert.IsTrue(modification.Timestamp.HasValue);
            Assert.IsTrue(modification.Timestamp.Value.Subtract(startTime).TotalSeconds < 4);
        }
    }
}
