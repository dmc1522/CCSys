using LasMargaritas.BL;
using LasMargaritas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
namespace LasMargaritas.ULT
{
    [TestClass]
    public class SettlementBLDLTest
    {
        private string connectionString;
        public SettlementBLDLTest()
        {
            connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDBDev;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
        }
        [TestMethod]
        public void TestPrintSettlement()
        {
            string anotherConnectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDB;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SettlementBL bl = new SettlementBL(anotherConnectionString);
            MemoryStream stream = bl.PrintSettlement(1);
            string fileName = DateTime.Now.ToString("testyyyyMMddHHMMssfff")+".pdf";
            File.WriteAllBytes(fileName, stream.ToArray());
            System.Diagnostics.Process.Start(fileName);
            
        }

        [TestMethod]
        public void TestDeletePayment()
        {
            SettlementBL settlementBL = new SettlementBL(connectionString);
            bool result = settlementBL.DeleteSettlementPayment(6);
            Assert.IsTrue(result);
        }
        public void TestInsertSettlement()
        {
            SettlementBL settlementBL = new SettlementBL(connectionString);
            Settlement settlement = new Settlement();
            settlement.CicleId = 1; //Assume we have a cicle with ID 1
            settlement.ProducerId = 1; //Assume we have a producer with ID 1
            settlement.CashAdvancesTotal = 1000;
            settlement.Date = System.DateTime.Now;
            settlement.Paid = true;
            settlement.CreditsTotal = 3000;
            settlement.WeightTicketsTotal = 4000;
            settlement.Total = 5000;
            settlement.User = "0ba16e0c-a1e4-44c1-8a94-e1070d03de10"; //Assume we have a user with this ID
            settlement.WeightTicketsIds = new System.Collections.ObjectModel.Collection<int>() { 1 };
            settlementBL.InsertSettlement(settlement);
            settlementBL.UpdateSettlement(settlement);
            List<Settlement> settlements = settlementBL.GetSettlement(null, null);
        }
        
    }
}
