using QRCoder;
using System.Drawing;
using System.Drawing.Printing;

namespace LasMargaritas.BL
{

    public class BadgePrinterHelper
    {        
        public static Bitmap GetQRCode(string data)
        {
            string level = "L";
            QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, eccLevel))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        Bitmap code = qrCode.GetGraphic(20, Color.Black, Color.White, null, 0);
                        return code;
                    }
                }
            }
        }      
    }
}

