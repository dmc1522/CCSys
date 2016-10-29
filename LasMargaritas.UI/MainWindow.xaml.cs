using System;
using System.Collections.Generic;
using System.Linq;
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
        public MainWindow()
        {
            InitializeComponent();
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
    }
}
