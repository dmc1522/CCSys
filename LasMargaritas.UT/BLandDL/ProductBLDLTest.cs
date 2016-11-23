using LasMargaritas.BL;
using LasMargaritas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
namespace LasMargaritas.ULT
{
    [TestClass]
    public class ProductBLDLTest
    {
        private string connectionString;
        public ProductBLDLTest()
        {
            connectionString = "Server=tcp:lasmargaritas.database.windows.net,1433;Initial Catalog=LasMargaritasDBDev;Persist Security Info=False;User ID=LasMargaritasDbUser;Password=LasMargaritasPassw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            if (ConfigurationManager.ConnectionStrings["LasMargaritasDb"] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
            }
            connectionString = ConfigurationManager.ConnectionStrings["LasMargaritasDb"].ConnectionString;
        }
        //[TestMethod]
        //public void TestGetWeightTicketProducts()
        //{
        //    //Test get by Id
        //    ProductBL productBL = new ProductBL(connectionString);
        //    List<SelectableModel> products = productBL.GetWeightTicketProducts();
        //    Assert.IsTrue(products.Count() > 1);
        //}
    }
}

