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
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            baseUrl = @"http://lasmargaritasdev.azurewebsites.net/";
            if (ConfigurationManager.AppSettings["baseUrl"] != null)
            {
                baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            }
            token = TokenHelper.GetToken(baseUrl, txtUser.Text,password.Password);
            InitializeComponent();
            producerList.Token = token;

            login.Visibility = Visibility.Hidden;
            controls.Visibility = Visibility.Visible;
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
