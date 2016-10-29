using LasMargaritas.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

namespace LasMargaritas.BL
{
    
    public class BadgePrinterHelper
    {
        private static int producerId;
        private static string producerName;
        private static Image logo;
        private static double badgeHeightInCm;
        private static double badgeWidthInCm;
        private static bool printBarCode;
        private static bool printQrCode;
        private static double leftBorderFromPageInCm = 2;
        private static double topBorderFromPageInCm = 2;
        private static double leftBorderFromRectangle = .8;
        private static double topBorderFromRectangle = .3;
        private static string businessName;
        private static string businessAddress;
        private static string title;

        public static void PrintBadge(int producerId, string businessName, string title, string businessAddress, string producerName, 
                                      Image logo, double badgeHeightInCm, double badgeWidthInCm, bool printBarCode, bool printQrCode)
        {
            BadgePrinterHelper.producerId = producerId;
            BadgePrinterHelper.producerName = producerName;
            BadgePrinterHelper.badgeHeightInCm = badgeHeightInCm;
            BadgePrinterHelper.logo = logo;
            BadgePrinterHelper.badgeWidthInCm = badgeWidthInCm;
            BadgePrinterHelper.printBarCode = printBarCode;
            BadgePrinterHelper.printQrCode = printQrCode;
            BadgePrinterHelper.businessName = businessName;
            BadgePrinterHelper.businessAddress = businessAddress;
            BadgePrinterHelper.title = title;
            PrintDocument pd = new PrintDocument();         
            pd.PrintPage += PrintPage;
            pd.Print();
        }

        private static void PrintLogo(PrintPageEventArgs e)
        {
            Point location = new Point();
            location.X = (int)(leftBorderFromPageInCm * 10) + (int)(leftBorderFromRectangle * 10); //border
            location.Y = (int)(topBorderFromPageInCm * 10) + 3; //border 
            e.Graphics.DrawImage(logo, location);
        }
        private static void PrintRectangle(PrintPageEventArgs e)
        {          
            Pen pen = new Pen(Color.Green);
            SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Green);
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            e.Graphics.DrawRectangle(pen, (int)leftBorderFromPageInCm * 10, (int)topBorderFromPageInCm * 10, (int)badgeWidthInCm *10, (int)badgeHeightInCm *10);
            e.Graphics.FillRectangle(drawBrush, ((int)leftBorderFromPageInCm * 10), ((int)topBorderFromPageInCm * 10), 8, ((int)badgeHeightInCm * 10));
        }

        private static void PrintProducerData(PrintPageEventArgs e)
        {
            Font drawFont = new Font("Arial", 12);
            SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Black);
            Pen pen = new Pen(Color.Black);
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            Point location = new Point();
            location.X = (int)(leftBorderFromPageInCm * 10) + (int)(leftBorderFromRectangle * 10) + (15); //border plus 2.5 centimers
            location.Y = (int)(topBorderFromPageInCm * 10) + (int)(topBorderFromRectangle * 10) + 25; //border + 3cm
            e.Graphics.DrawString(producerName, drawFont, drawBrush, location);
        }

        private static void PrintTitle(PrintPageEventArgs e)
        {
            Font drawFont = new Font("Arial", 12);
            SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.DarkBlue);            
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            Point location = new Point();
            location.X = (int)(leftBorderFromPageInCm * 10) + (int)(leftBorderFromRectangle * 10) + (15); //border plus 2.5 centimers
            location.Y = (int)(topBorderFromPageInCm * 10) + (int)(topBorderFromRectangle * 10) + 20; //border  plus 2 cm
            e.Graphics.DrawString(title, drawFont, drawBrush, location);
        }
        private static void PrintBusinessName(PrintPageEventArgs e)
        {
            Font drawFont = new Font("Georgia", 12);
            SolidBrush drawBrush = new SolidBrush(Color.Green);            
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            Point location = new Point();
            location.X = (int)(leftBorderFromPageInCm * 10) + (int)(leftBorderFromRectangle * 10) + (17); //border plus 1.7 centimers
            location.Y = (int)(topBorderFromPageInCm * 10) + (int)(topBorderFromRectangle * 10); //border 
            e.Graphics.DrawString(businessName, drawFont, drawBrush, location);
        }
        private static void PrintBusinessAddress(PrintPageEventArgs e)
        {
            Font drawFont = new Font("Georgia", 6);
            SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Black);            
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            Point location = new Point();
            location.X = (int)(leftBorderFromPageInCm * 10) + (int)(leftBorderFromRectangle * 10) + (27); //border plus 3 centimers
            location.Y = (int)(topBorderFromPageInCm * 10) + (int)(topBorderFromRectangle * 10) + 5; //border + .5 cm      
            e.Graphics.DrawString(businessAddress, drawFont, drawBrush, location);
        }

        private static void PrintBarCode()
        {

        }

        private static void PrintQrCode(PrintPageEventArgs e)
        {
            string level = "L";
            QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(producerId.ToString(), eccLevel))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        Bitmap code = qrCode.GetGraphic(20, Color.Black, Color.White, null, 0);
                        Point location = new Point();
                        location.X = (int)(leftBorderFromPageInCm * 10) + (int)(leftBorderFromRectangle * 10) + 25; 
                        location.Y = (int)(topBorderFromPageInCm * 10) + (int)(topBorderFromRectangle * 10) + 30; //border + 1 cm
                        code = ImageScaler.ResizeImage(code, 60, 60);
                        e.Graphics.DrawImage(code, location);
                    }
                }
            }
        }

        private static void PrintPage(object o, PrintPageEventArgs e)
        {
            PrintRectangle(e);
            PrintProducerData(e);
            PrintBusinessName(e);
            PrintBusinessAddress(e);
            PrintTitle(e);
            PrintLogo(e);
            PrintQrCode(e);
        }
    }
}

