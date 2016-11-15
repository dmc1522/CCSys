using LasMargaritas.BL;
using LasMargaritas.Models;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;


namespace LasMargaritas.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Token token;

        private string baseUrl;
        public MainWindow()
        {
            baseUrl = @"http://lasmargaritasdev.azurewebsites.net/";
            if (ConfigurationManager.AppSettings["baseUrl"] != null)
            {
                baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            }
            token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3");
            InitializeComponent();
            producerList.Token = token;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonViewWeightTickets_Click(object sender, RoutedEventArgs e)
        {
            tickets.Visibility = Visibility.Visible;
            producerList.Visibility = Visibility.Hidden;
        }

        private void buttonViewProducers_Click(object sender, RoutedEventArgs e)
        {
            tickets.Visibility = Visibility.Hidden;
            producerList.Visibility = Visibility.Visible;
        }

    }
}
