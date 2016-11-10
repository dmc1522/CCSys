using LasMargaritas.BL;
using LasMargaritas.BL.Views;
using LasMargaritas.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Controls;
using System.Collections.Generic;
using LasMargaritas.BL.Presenters;
using WebEye.Controls.Wpf;
using System.Linq;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace LasMargaritas.UI.UserControls
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class ProducerList : UserControl, IProducerView
    {
        
        private string baseUrl;
        private string insertAction;
        private string updateAction;
        private string deleteAction;
        private string getAllAction;
        private string getByIdAction;
        private ProducerPresenter presenter;        
        private List<Producer> _Producers;
        private bool listLoaded;

        public Token Token { get; set; }
        public Producer CurrentProducer
        {
            set
            {
                ListBoxProducers.SelectedItem = value;
            }
                
            get
            {
                if (ListBoxProducers != null)
                    return (Producer)ListBoxProducers.SelectedItem;
                return null;
                
            }
        }


        public List<Producer> Producers
        {
            get
            { 
                return _Producers;
            }
            set
            {
                _Producers = value;
                ListBoxProducers.ItemsSource = _Producers;
            }
        }                    

        public ProducerList()
        {
            InitializeComponent();          
            presenter = new ProducerPresenter(this);          
        }   

        private void PrintGaffete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button b = sender as Button;
            Producer producer = b.CommandParameter as Producer;
            System.Drawing.Image logo = System.Drawing.Image.FromFile("Images\\Logo.jpg");
            System.Drawing.Bitmap scaledLogo = ImageScaler.ResizeImage(logo, 60, 60);
            BadgePrinterHelper.PrintBadge(producer.Id, "Comercializadora Las Margaritas", "PRODUCTOR DISTINGUIDO", "Avenida Patria 10. Ameca, Jalisco", producer.Name + " " + producer.LastName, scaledLogo, 5, 9, true, true);
        }

        public void HandleException(Exception ex, string method, Guid errorId)
        {           
        }
       

        private void UserControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true && !listLoaded)
            {
                presenter.Token = Token;
                presenter.LoadProducers();
                listLoaded = true;
            }
        }

      
        private void ButtonGetImage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!webCameraControl.IsCapturing)
            {
                IEnumerable<WebCameraId> cameras = webCameraControl.GetVideoCaptureDevices();
                webCameraControl.StartCapture(cameras.ElementAt(0));
                TextBoxImageInstructions.Content = "Click para GUARDAR foto";
                ButtonCaptureImage.Visibility = System.Windows.Visibility.Hidden;
                ButtonGetImage.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                TextBoxImageInstructions.Content = "Click para CAPTURAR foto";
                ButtonCaptureImage.Visibility = System.Windows.Visibility.Visible;
                ButtonGetImage.Visibility = System.Windows.Visibility.Hidden;
                Bitmap bitmap = webCameraControl.GetCurrentImage();             
                using (var memoryStream = new MemoryStream())
                {
                    bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);                    
                    CurrentProducer.Photo = memoryStream.ToArray();
                }                    
                webCameraControl.StopCapture();
            }
        }

        private void ButtonSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            presenter.SaveProducer();
        }
    

        private void ButtonPrintGaffete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Gaffette_Preview preview = new Gaffette_Preview(CurrentProducer);
            preview.Show();
        }
    }
}
