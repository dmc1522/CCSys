using LasMargaritas.BL;
using LasMargaritas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LasMargaritas.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string baseUrl;
        private string insertAction;
        private string updateAction;
        private string deleteAction;
        private string getAllAction;
        private string getByIdAction;
       

        public MainWindow()
        {
            InitializeComponent();
            baseUrl = @"http://lasmargaritas.azurewebsites.net/";
            insertAction = "Producer/Add";
            updateAction = "Producer/Update";
            deleteAction = "Producer/Delete";
            getAllAction = "Producer/GetAll";
            getByIdAction = "Producer/GetById";
        }

        private void ProductsBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void WeightTicketsBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.Invoke(new Action(() => 
            {
                SetView(View.WeightTicketsMain);
            }));
        }
        private enum View
        {
            Main,
            WeightTicketsMain,
            WeightTicketsCreateStep1
        }
        private void SetView(View view)
        {
            switch(view)
            {
                case View.Main:
                    MainView.Visibility = Visibility.Visible;
                    WeightTicketsMainView.Visibility = Visibility.Collapsed;
                    WeighTicketsCreateStep1View.Visibility = Visibility.Collapsed;
                    break;
                case View.WeightTicketsMain:
                    MainView.Visibility = Visibility.Collapsed;
                    WeightTicketsMainView.Visibility = Visibility.Visible;
                    WeighTicketsCreateStep1View.Visibility = Visibility.Collapsed;
                    break;
                case View.WeightTicketsCreateStep1:
                    MainView.Visibility = Visibility.Collapsed;
                    WeightTicketsMainView.Visibility = Visibility.Collapsed;
                    WeighTicketsCreateStep1View.Visibility = Visibility.Visible;
                    break;
            }

        }
      

       
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {

            Dispatcher.Invoke(new Action(() =>
            {
                SetView(View.Main);
            }));
        }

        private void GoBackFromWeightTicketsMainBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                SetView(View.Main);
            }));
        }


        private void GoBackFromWeightTicketCreateStep1Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                SetView(View.WeightTicketsMain);
            }));
        }
       
        private void WeightTicketsCreateBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                SetView(View.WeightTicketsCreateStep1);
            }));

        }

     

        private void TextBoxProducerId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {                
                Token token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3");
                string getByIdUrl = string.Format("{0}?id={1}", getByIdAction, TextBoxProducerId.Text);
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
                HttpResponseMessage response = client.GetAsync(getByIdUrl).Result;
                if (response.IsSuccessStatusCode)
                {                 
                    GetProducerResponse getProducerResponse = response.Content.ReadAsAsync<GetProducerResponse>().Result;
                    if (getProducerResponse.Producers.Count == 1)
                    {
                        GridProducerData.Visibility = Visibility.Visible;
                        LabelProducerName.Content = getProducerResponse.Producers[0].Name;
                        LabelProducerLastName.Content = getProducerResponse.Producers[0].LastName;
                        LabelProducerAddress.Content = getProducerResponse.Producers[0].Address;
                        LabelProducerCity.Content = getProducerResponse.Producers[0].City;
                    }
                }
            }
            else
            {
                GridProducerData.Visibility = Visibility.Hidden;
            }
        }
    }
}

