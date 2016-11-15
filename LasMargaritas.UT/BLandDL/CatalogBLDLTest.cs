using LasMargaritas.BL;
using LasMargaritas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
namespace LasMargaritas.ULT
{
    [TestClass]
    public class CatalogBLDLTest
    {
        private string connectionString;
        public CatalogBLDLTest()
        {
            connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDB;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
        }
        
        [TestMethod]
        public void TestGetCatalogs()
        {
            //Test get by Id
            CatalogBL catalogBL = new CatalogBL(connectionString);
            List<SelectableModel> brands = catalogBL.GetAgriculturalBrands();
            Assert.IsTrue(brands.Count() > 0);
            List<SelectableModel> civilStatus = catalogBL.GetCivilStatus();
            Assert.IsTrue(civilStatus.Count() > 0);
            List<SelectableModel> genders = catalogBL.GetGenders();
            Assert.IsTrue(genders.Count() > 0);
            List<SelectableModel> presentations = catalogBL.GetPresentations();
            Assert.IsTrue(presentations.Count() > 0);
            List<SelectableModel> productGroups = catalogBL.GetProductGroups();
            Assert.IsTrue(productGroups.Count() > 0);
            List<SelectableModel> regimes = catalogBL.GetRegimes();
            Assert.IsTrue(regimes.Count() > 0);
            List<SelectableModel> units = catalogBL.GetUnits();
            Assert.IsTrue(units.Count() > 0);
            List<SelectableModel> states = catalogBL.GetStates();
            Assert.IsTrue(states.Count() > 0);



        }

      
    }
}
