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
        
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("ZBRGraphics.dll")]
        public static extern int ZBRGDIInitGraphics(byte[] printerName, ref int myHandle, ref long errValue);

        [DllImport("ZBRGraphics.dll")]
        public static extern int ZBRGDIClearGraphics(ref long errValue);

        [DllImport("ZBRGraphics.dll")]
        public static extern int ZBRGDICloseGraphics(int myHandle, ref long errValue);

        [DllImport("ZBRGraphics.dll")]
        public static extern int ZBRGDIPrintGraphics(int myHandle, ref long errValue);

        [DllImport("ZBRGraphics.dll")] 
        public static extern int ZBRGDIDrawImageRect(byte[] fileName, int x, int y, int width, int height, ref long errValue);     
            
        [DllImport("ZBRGraphics.dll")] 
        public static extern int ZBRGDIDrawText(int x, int y, byte[] text, byte[] font, int fontSize, int fontStyle, int color, ref long errValue);

        [DllImport("ZBRGraphics.dll")]
        public static extern int ZBRGDIDrawTextUnicode(int x, int y, byte[] text, byte[] font, int fontSize, int fontStyle, int color, ref long errValue);


        [DllImport("ZBRGraphics.dll")] 
        public static extern int ZBRGDIDrawLine(int x, int y, int x2, int y2, int color, float thicknes, ref long errValue);

        [DllImport("ZBRGraphics.dll")]
        public static extern int ZBRGDIPreviewGraphics(IntPtr pictureBoxHandler, ref long errValue);                                        
        
        public Producer Producer { get; set; }

        private string defaultPrinter;
        public Gaffette_Preview(Producer producer)
        {
            InitializeComponent();
            defaultPrinter = "Zebra ZXP Series 3 USB Card Printer"; //"PDFCreator";
            Producer = producer;
            var is64 = IntPtr.Size == 8;
            LoadLibrary(is64 ? "x64/ZBRGraphics.dll" : "x86/ZBRGraphics.dll");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {         
           
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            File.Delete(System.IO.Path.GetTempPath() + "\\photo.bmp");
            File.Delete(System.IO.Path.GetTempPath() + "\\qr.bmp");

        }

        private void Print(bool isPreview)
        {           
            int handle = 0;
            long error = 0;
            //Front
            byte[] printerName = Encoding.ASCII.GetBytes(defaultPrinter);
            byte[] producerName = Encoding.ASCII.GetBytes((Producer.Name + " " + Producer.PaternalSurname + " " + Producer.MaternalSurname).ToUpper().Replace("Ñ", "N"));
            byte[] qrCodeText = Encoding.ASCII.GetBytes("Id: " + Producer.BarCode);
            byte[] producerTitle = Encoding.ASCII.GetBytes("PRODUCTOR");
            byte[] myFont = Encoding.ASCII.GetBytes("Arial");
            byte[] frontBackGround = Encoding.ASCII.GetBytes("back.jpg");
            byte[] backBackGround = Encoding.ASCII.GetBytes("front.jpg");
            File.WriteAllBytes(System.IO.Path.GetTempPath() +"\\photo.bmp", Producer.Photo);
            Bitmap qrCodeImage = BadgePrinterHelper.GetQRCode(Producer.BarCode);
            qrCodeImage.Save(System.IO.Path.GetTempPath()+"\\qr.bmp");
            byte[] photo = Encoding.ASCII.GetBytes(System.IO.Path.GetTempPath() + "\\photo.bmp");
            byte[] qrCodeImageBytes = Encoding.ASCII.GetBytes(System.IO.Path.GetTempPath() + "\\qr.bmp");
            byte[] businessData = Encoding.ASCII.GetBytes("Grupo Garibay. Avenida Patria No 10 Ameca, Jalisco.");
            int result = ZBRGDIInitGraphics(printerName, ref handle, ref error);
            if (result != 1)
            {
                MessageBox.Show("No se encontró la impresora", "Error", MessageBoxButton.OK, MessageBoxImage.Error);                
                return;
            }
            result = ZBRGDIDrawImageRect(frontBackGround, 10, 10, 1054, 654, ref error);
            result = ZBRGDIDrawImageRect(photo, 700, 105, 300, 300, ref error);
            result = ZBRGDIDrawImageRect(qrCodeImageBytes, 215, 320, 225, 225, ref error);
            result = ZBRGDIDrawText(215, 590, qrCodeText, myFont, 10, 1, 1, ref error);
            //result = ZBRGDIDrawLine(30, 75, 520, 75, 1, 75, ref error);
            result = ZBRGDIDrawText(50, 120, producerName, myFont, 9, 1, 1, ref error);
            result = ZBRGDIDrawText(70, 220, producerTitle, myFont, 7, 1, 1, ref error);
            //result = ZBRGDIDrawText(30, 580, businessData, myFont, 6, 1, 16777215, ref error);
            if(isPreview)
                result = ZBRGDIPreviewGraphics(ImageFront.PreviewImageHandle, ref error);
            else
                result = ZBRGDIPrintGraphics(handle, ref error);
            result = ZBRGDIClearGraphics(ref error);
            //Back
            result = ZBRGDIDrawImageRect(backBackGround, 10, 10, 1054, 654, ref error);
            if(isPreview)
                result = ZBRGDIPreviewGraphics(ImageBack.PreviewImageHandle, ref error);
            else
                result = ZBRGDIPrintGraphics(handle, ref error);
            ZBRGDICloseGraphics(handle, ref error);
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            ImageFront.Title = "Frente";            
            ImageBack.Title = "Reverso";
            Print(true);
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Print(false);
        }
    }    
}
