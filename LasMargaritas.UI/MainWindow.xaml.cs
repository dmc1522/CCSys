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
        public MainWindow()
        {
            InitializeComponent();
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
