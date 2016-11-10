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

namespace LasMargaritas.UI.UserControls
{
    /// <summary>
    /// Interaction logic for Tickets.xaml
    /// </summary>
    public partial class Tickets : UserControl
    {
        public Tickets()
        {
            InitializeComponent();
        }

        private void btnSearchingTicket_Click(object sender, RoutedEventArgs e)
        {
            Random ticket = new Random();
            var button = new Button() { Content = "#000" + ticket.Next().ToString(), Margin = new Thickness(5, 1, 5, 1), Height = 25, Width = 150, HorizontalAlignment = HorizontalAlignment.Center };
            lbTickets.Items.Add(button);
        }

        private void btnNewTicket_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
