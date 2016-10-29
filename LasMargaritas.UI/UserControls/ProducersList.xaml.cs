using LasMargaritas.BL;
using LasMargaritas.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Controls;


namespace LasMargaritas.UI.UserControls
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class ProducerList : UserControl
    {
        private string baseUrl;
        private string insertAction;
        private string updateAction;
        private string deleteAction;
        private string getAllAction;
        private string getByIdAction;
        public ProducerList()
        {
            InitializeComponent();            
            baseUrl = @"http://lasmargaritas.azurewebsites.net/";
            insertAction = "Producer/Add";
            updateAction = "Producer/Update";
            deleteAction = "Producer/Delete";
            getAllAction = "Producer/GetAll";
            getByIdAction = "Producer/GetById";
            Token token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            HttpResponseMessage response = client.GetAsync(getAllAction).Result;
            
            if (response.IsSuccessStatusCode)
            {
                GetProducerResponse getProducerResponse = response.Content.ReadAsAsync<GetProducerResponse>().Result;
                ProducerListView.ItemsSource = getProducerResponse.Producers;                
            }
        }

    

        private void PrintGaffete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button b = sender as Button;
            Producer producer = b.CommandParameter as Producer;
            System.Drawing.Image logo = System.Drawing.Image.FromFile("Images\\Logo.jpg");
            System.Drawing.Bitmap scaledLogo = ImageScaler.ResizeImage(logo, 60, 60);
            BadgePrinterHelper.PrintBadge(producer.Id, "Comercializadora Las Margaritas", "PRODUCTOR DISTINGUIDO", "Avenida Patria 10. Ameca, Jalisco", producer.Name + " " + producer.LastName, scaledLogo, 5, 9, true, true);
        }
    }
}
