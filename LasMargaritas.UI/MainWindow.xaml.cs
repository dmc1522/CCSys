using LasMargaritas.BL;
using LasMargaritas.Models;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using WPFTabTip;

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
            FrameworkCompatibilityPreferences.KeepTextBoxDisplaySynchronizedWithTextProperty = false;
            TabTipAutomation.IgnoreHardwareKeyboard = HardwareKeyboardIgnoreOptions.IgnoreAll;
            TabTipAutomation.BindTo<TextBox>();
            TabTipAutomation.BindTo<PasswordBox>();
            
            TabTip.Open();
            TabTip.Close();        
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            baseUrl = @"http://lasmargaritasdev.azurewebsites.net/";
            if (ConfigurationManager.AppSettings["baseUrl"] != null)
            {
                baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            }
            token = TokenHelper.GetToken(baseUrl, txtUser.Text,password.Password);
            if (token == null || string.IsNullOrEmpty(token.access_token))
            {
                MessageBox.Show("Usuario o contraseña no válidos. Reintente de nuevo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            InitializeComponent();
            producerList.Token = token;
            weightTickets.Token = token;
            ranchers.Token = token;
            saleCustomers.Token = token;
            suppliers.Token = token;


            login.Visibility = Visibility.Hidden;
            controls.Visibility = Visibility.Visible;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonViewWeightTickets_Click(object sender, RoutedEventArgs e)
        {
            weightTickets.Visibility = Visibility.Visible;
            producerList.Visibility = Visibility.Hidden;
            ranchers.Visibility = Visibility.Hidden;
            saleCustomers.Visibility = Visibility.Hidden;
            suppliers.Visibility = Visibility.Hidden;
        }

        private void buttonViewProducers_Click(object sender, RoutedEventArgs e)
        {
            weightTickets.Visibility = Visibility.Hidden;
            producerList.Visibility = Visibility.Visible;
            ranchers.Visibility = Visibility.Hidden;
            saleCustomers.Visibility = Visibility.Hidden;
            suppliers.Visibility = Visibility.Hidden;
        }

        private void buttonViewSuppliers_Click(object sender, RoutedEventArgs e)
        {
            weightTickets.Visibility = Visibility.Hidden;
            producerList.Visibility = Visibility.Hidden;
            ranchers.Visibility = Visibility.Hidden;
            saleCustomers.Visibility = Visibility.Hidden;
            suppliers.Visibility = Visibility.Visible;

        }

        private void buttonViewRanchers_Click(object sender, RoutedEventArgs e)
        {
            weightTickets.Visibility = Visibility.Hidden;
            producerList.Visibility = Visibility.Hidden;
            ranchers.Visibility = Visibility.Visible;
            saleCustomers.Visibility = Visibility.Hidden;
            suppliers.Visibility = Visibility.Hidden;
        }

        private void buttonViewSaleCustomers_Click(object sender, RoutedEventArgs e)
        {
            weightTickets.Visibility = Visibility.Hidden;
            producerList.Visibility = Visibility.Hidden;
            ranchers.Visibility = Visibility.Hidden;
            saleCustomers.Visibility = Visibility.Visible;
            suppliers.Visibility = Visibility.Hidden;
        }
    }
}
