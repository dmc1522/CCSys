using LasMargaritas.BL;
using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LasMargaritas.UI
{
    /// <summary>
    /// Interaction logic for Gaffette_Preview.xaml
    /// </summary>
    public partial class Gaffette_Preview : Window
    {
        [DllImport("ZBRGraphics.dll")]
        public static extern int ZBRGDIInitGraphics(byte[] printerName, ref int myHandle, ref long errValue);

        [DllImport("ZBRGraphics.dll")]
        public static extern int ZBRGDICloseGraphics(int myHandle, ref long errValue);

        [DllImport("ZBRGraphics.dll")]
        public static extern int ZBRGDIPrintGraphics(int myHandle, ref long errValue);

        [DllImport("ZBRGraphics.dll")] 
        public static extern int ZBRGDIDrawImageRect(byte[] fileName, int x, int y, int width, int height, ref long errValue);     
            
        [DllImport("ZBRGraphics.dll")] 
        public static extern int ZBRGDIDrawText(int x, int y, byte[] text, byte[] font, int fontSize, int fontStyle, int color, ref long errValue);

        [DllImport("ZBRGraphics.dll")] 
        public static extern int ZBRGDIDrawLine(int x, int y, int x2, int y2, int color, float thicknes, ref long errValue);

        [DllImport("ZBRGraphics.dll")]
        public static extern int ZBRGDIPreviewGraphics(IntPtr pictureBoxHandler, ref long errValue);                                        
        
        public Producer Producer { get; set; }
        public Gaffette_Preview(Producer producer)
        {
            InitializeComponent();
            Producer = producer;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {         
           
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            File.Delete("test.bmp");
            File.Delete("test2.bmp");
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            int handle = 0;
            long error = 0;
            byte[] printerName = Encoding.ASCII.GetBytes("PDFCreator");
            byte[] myText = Encoding.ASCII.GetBytes(Producer.Name + " " + Producer.LastName);
            byte[] myFont = Encoding.ASCII.GetBytes("Arial");
            byte[] backGround = Encoding.ASCII.GetBytes("back.jpg");
            File.WriteAllBytes("test.bmp", Producer.Photo);
            Bitmap qrCode = BadgePrinterHelper.GetQRCode(Producer.Id.ToString());
            qrCode.Save("test2.bmp");
            byte[] photo = Encoding.ASCII.GetBytes("test.bmp");
            byte[] barCode = Encoding.ASCII.GetBytes("test2.bmp");
            byte[] businessData = Encoding.ASCII.GetBytes("Grupo Garibay. " + Environment.NewLine + "Avenida Patria No 10 " + Environment.NewLine + "Ameca, Jalisco.");
            int result = ZBRGDIInitGraphics(printerName, ref handle, ref error);
            result = ZBRGDIDrawImageRect(backGround, 10, 10, 1054, 654, ref error);
            result = ZBRGDIDrawImageRect(photo, 675, 30, 300, 300, ref error);
            result = ZBRGDIDrawImageRect(barCode, 800, 400, 200, 200, ref error);
            result = ZBRGDIDrawLine(30, 75, 520, 75, 1, 75, ref error);
            result = ZBRGDIDrawText(50, 50, myText, myFont, 12, 1, 16777215, ref error);
            result = ZBRGDIDrawText(40, 200, businessData, myFont, 9, 1, 12388103, ref error);
          
            HwndSource hwndSource = PresentationSource.FromVisual(Gafette) as HwndSource;

            if (hwndSource != null)
            {
                result = ZBRGDIPreviewGraphics(hwndSource.Handle, ref error);
            }
            ZBRGDICloseGraphics(handle, ref error);
        }
    }    
}
