using LasMargaritas.BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.IO;

namespace LasMargaritas.UT
{
    [TestClass]
    public class BadgePrinterHelperTest
    {
        [TestMethod]
        public void PrintFullBadge()
        {
            Image logo = Image.FromFile("Images\\Logo.jpg");
            Bitmap scaledLogo = ImageScaler.ResizeImage(logo, 60, 60);
            BadgePrinterHelper.PrintBadge(1, "Comercializadora Las Margaritas", "PRODUCTOR DISTINGUIDO", "Avenida Patria 10. Ameca, Jalisco", "Manuel A. Quintero Sánchez", scaledLogo, 5, 9, true, true);
        }
    }  
}
