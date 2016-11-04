using LasMargaritas.BL;
using LasMargaritas.Models;
using System;
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
            baseUrl = @"http://lasmargaritas.azurewebsites.net/";
            token = TokenHelper.GetToken(baseUrl, "Melvin3", "MelvinPass3");
            InitializeComponent();
            producerList.Token = token;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSearchingTicket_Click(object sender, RoutedEventArgs e)
        {
            Random ticket = new Random();
            var button = new Button() { Content = "#000" + ticket.Next().ToString(), Margin = new Thickness(5, 1, 5, 1), Height = 25, Width = 150, HorizontalAlignment = HorizontalAlignment.Center };
            lbTickets.Items.Add(button);
        }

        private void buttonViewWeightTickets_Click(object sender, RoutedEventArgs e)
        {
            GridWeightTickets.Visibility = Visibility.Visible;
            producerList.Visibility = Visibility.Collapsed;
        }

        private void buttonViewProducers_Click(object sender, RoutedEventArgs e)
        {
            GridWeightTickets.Visibility = Visibility.Collapsed;
            producerList.Visibility = Visibility.Visible;
        }

    }       
}
