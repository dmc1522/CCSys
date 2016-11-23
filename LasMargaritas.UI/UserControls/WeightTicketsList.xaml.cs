using LasMargaritas.BL.Views;
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
using LasMargaritas.Models;
using LasMargaritas.BL.Presenters;
using LasMargaritas.BL.Utils;

namespace LasMargaritas.UI.UserControls
{
    /// <summary>
    /// Interaction logic for WeightTicketList.xaml
    /// </summary>
    public partial class WeightTicketList : UserControl, IWeightTicketView
    {
        #region Private variables        
        private bool listLoaded;
        private WeightTicketPresenter presenter;
        private List<SelectableModel> _WeightTickets;
        #endregion

        #region Public properties
        public Token Token { get; set; }
        #endregion

        #region IWeightTicketView
        public List<SelectableModel> Producers { get; set; }
        public List<SelectableModel> WareHouses { get; set; }

        public List<SelectableModel> Products { get; set; }

        public List<SelectableModel> FilterCicles { get; set; }

        public List<SelectableModel> Cicles { get; set; }

        public List<SelectableModel> Ranchers { get; set; }

        public List<SelectableModel> Suppliers { get; set; }

        public List<SelectableModel> SalesCustomers { get; set; }

        public int SelectedFilterCicleId
        {
            get
            {
                if (ComboBoxCiclesFilter.SelectedItem != null)
                    return ((SelectableModel)ComboBoxCiclesFilter.SelectedItem).Id;
                return -1;
            }
        }
        public List<SelectableModel> WeightTickets
        {
            get
            {
                return _WeightTickets;
            }
            set
            {
                _WeightTickets = value;
                ListBoxTickets.ItemsSource = _WeightTickets;
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListBoxTickets.ItemsSource);
                view.Filter = WeightTicketFilter;
            }
        }
        private bool WeightTicketFilter(object item)
        {
            if (string.IsNullOrEmpty(txbSearhcTicket.Text))
                return true;
            else
                return ((item as SelectableModel).Name != null && (item as SelectableModel).Name.IndexOf(txbSearhcTicket.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    || string.Compare(((item as SelectableModel).Id.ToString()), txbSearhcTicket.Text) == 0;

        }

        public int SelectedId
        {
            get
            {
                if (ListBoxTickets.SelectedItem != null)
                {
                    return ((SelectableModel)ListBoxTickets.SelectedItem).Id;
                }
                return -1;
            }
            set
            {
                if (value <= 0)
                {

                    ListBoxTickets.SelectedValue = null;
                    ListBoxTickets.SelectedIndex = -1;
                }
                else
                {
                    foreach (var item in ListBoxTickets.Items)
                    {
                        if (((SelectableModel)item).Id == value)
                        {
                            ListBoxTickets.SelectedItem = item;
                            ListBoxTickets.ScrollIntoView(item);
                            break;
                        }
                    }
                }

            }
        }

        public WeightTicket CurrentWeightTicket { get; set; }

        public bool ObtainEntranceWeightEnable
        {
            get
            {
                return btnWeight1.IsEnabled;
            }

            set
            {
                btnWeight1.IsEnabled = value;
            }
        }

        public bool ObtainExitWeightEnable
        {
            get
            {
                return btnWeight2.IsEnabled;
            }

            set
            {
                btnWeight2.IsEnabled = value;
            }
        }

        public WeightTicketType WeightTicketType
        {
            get
            {
                if (ComboBoxProducer.SelectedItem != null)
                    return WeightTicketType.Producer;
                else if (ComboBoxRancher.SelectedItem != null)
                    return WeightTicketType.Rancher;
                else if (ComboBoxSalesCustomer.SelectedItem != null)
                    return WeightTicketType.SaleCustomer;
                else if (ComboBoxSupplier.SelectedItem != null)
                    return WeightTicketType.Supplier;
                return WeightTicketType.None;
            }
        }

        public string CurrentBuyerSaler
        {
            get
            {
                if (WeightTicketType == WeightTicketType.Rancher)
                    return ((SelectableModel)ComboBoxRancher.SelectedItem).Name;
                else if (WeightTicketType == WeightTicketType.Producer)
                    return ((SelectableModel)ComboBoxProducer.SelectedItem).Name;
                else if (WeightTicketType == WeightTicketType.SaleCustomer)
                    return ((SelectableModel)ComboBoxSalesCustomer.SelectedItem).Name;
                else if (WeightTicketType == WeightTicketType.Supplier)
                    return ((SelectableModel)ComboBoxSupplier.SelectedItem).Name;
                return string.Empty;
            }
        }

        public string CurrentProduct
        {
            get
            {
                if (ComboBoxProducts.SelectedItem != null)
                    return ((SelectableModel)ComboBoxProducts.SelectedItem).Name;
                return string.Empty;
            }
        }

        public void HandleException(Exception ex, string method, Guid errorId)
        {
            
        }
        #endregion
        #region Constructors

        public WeightTicketList()
        {
            InitializeComponent();
            presenter = new WeightTicketPresenter(this);
            CurrentWeightTicket = new WeightTicket();            
            GridTicketsDetails.DataContext = CurrentWeightTicket;
        }
        #endregion

        #region Private methods
        
        private void btnNewTicket_Click(object sender, RoutedEventArgs e)
        {
            presenter.NewWeightTicket();
        }
        #endregion

        private void btnReloadTicket_Click(object sender, RoutedEventArgs e)
        {
            presenter.LoadWeightTickets();
        }
       
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true && !listLoaded)
            {
                presenter.Token = Token;
                presenter.LoadCatalogs();                
                ComboBoxCiclesFilter.ItemsSource = FilterCicles;
                ComboBoxCicles.ItemsSource = Cicles;
                ComboBoxProducer.ItemsSource = Producers;                
                ComboBoxRancher.ItemsSource = Ranchers;
                ComboBoxSalesCustomer.ItemsSource = SalesCustomers;
                ComboBoxSupplier.ItemsSource = Suppliers;
                ComboboxWareHouse.ItemsSource = WareHouses;
                listLoaded = true;
                ComboBoxCiclesFilter.SelectedIndex = 0;
                ComboBoxTicketType.SelectedIndex = 0;
                presenter.LoadProducts();
                ComboBoxProducts.ItemsSource = Products;
                presenter.LoadWeightTickets();
                CreateDummyTicket();
            }
        }
        private  void CreateDummyTicket()
        {
            WeightTicket weightTicket = CurrentWeightTicket;
            weightTicket.SubTotal = 10000;
            weightTicket.ApplyDrying = true;
            weightTicket.ApplyHumidity = true;
            weightTicket.ApplyImpurities = true;
            weightTicket.BrokenGrainDiscount = 10;
            weightTicket.CicleId = 1;
            weightTicket.CrashedGrainDiscount = 10;
            weightTicket.DamagedGrainDiscount = 10;
            weightTicket.Driver = "Driver";
            weightTicket.DryingDiscount = 10;
            weightTicket.EntranceDate = DateTime.Now;
            weightTicket.EntranceNetWeight = 1000;
            weightTicket.EntranceWeigher = "Weigher";
            weightTicket.EntranceWeightKg = 18840;
            weightTicket.ExitWeightKg = 9880;
            weightTicket.ExitDate = DateTime.Now;
            weightTicket.ExitNetWeight = 200;
            weightTicket.ExitWeigher = "Weigher";
            weightTicket.Folio = DateTime.Now.ToString("MMddHHmmssfff");
            weightTicket.Humidity = 18.6F;
            weightTicket.HumidityDiscount = 10;
            weightTicket.Impurities = 4.8F;
            weightTicket.ImpuritiesDiscount = 10;
            weightTicket.NetWeight = 400;
            weightTicket.Number = "N"+ DateTime.Now.ToString("MMddHHmmssfff");
            weightTicket.Paid = false;
            weightTicket.Plate = "Plate";
            weightTicket.Price = 3.35M;
            weightTicket.ProducerId = 1;
            weightTicket.ProductId = 1;
            weightTicket.SmallGrainDiscount = 10;
            weightTicket.StoreTs = DateTime.Now;
            weightTicket.TotalDiscount = 50;
            weightTicket.TotalToPay = 100;
            weightTicket.UpdateTs = DateTime.Now;
            //weightTicket.UserId = "35d360f3-c296-4113-ab34-9b91fe729c18";
            weightTicket.WarehouseId = 1;
            weightTicket.Freight = true;
            weightTicket.Id = 0;
            weightTicket.RaiseUpdateProperties();         
        }


        private void ComboBoxTicketType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedValue = ((ComboBoxItem)ComboBoxTicketType.SelectedValue).Content.ToString();
            LabelProducer.Visibility = Visibility.Collapsed;
            ComboBoxProducer.Visibility = Visibility.Collapsed;
            ComboBoxProducer.SelectedIndex = -1;
            LabelRancher.Visibility = Visibility.Collapsed;
            ComboBoxRancher.Visibility = Visibility.Collapsed;
            ComboBoxRancher.SelectedIndex = -1;
            LabelSalesCustomer.Visibility = Visibility.Collapsed;
            ComboBoxSalesCustomer.Visibility = Visibility.Collapsed;
            ComboBoxSalesCustomer.SelectedIndex = -1;
            LabelSupplier.Visibility = Visibility.Collapsed;
            ComboBoxSupplier.Visibility = Visibility.Collapsed;
            ComboBoxSupplier.SelectedIndex = -1;
            GridCattle.Visibility = Visibility.Collapsed;
            CattleSeparator.Visibility = Visibility.Collapsed;
            GridCattleHeader.Visibility = Visibility.Collapsed;
            if (selectedValue == "Productor")
            {
                LabelProducer.Visibility = Visibility.Visible;
                ComboBoxProducer.Visibility = Visibility.Visible;
                ComboBoxProducer.SelectedIndex = 0;

            }
            else if (selectedValue == "Ganadero")
            {
                LabelRancher.Visibility = Visibility.Visible;
                ComboBoxRancher.Visibility = Visibility.Visible;
                GridCattle.Visibility = Visibility.Visible;
                CattleSeparator.Visibility = Visibility.Visible;
                GridCattleHeader.Visibility = Visibility.Visible;
                ComboBoxRancher.SelectedIndex = 0;
            }
            else if (selectedValue == "Cliente de Venta")
            {
                LabelSalesCustomer.Visibility = Visibility.Visible;
                ComboBoxSalesCustomer.Visibility = Visibility.Visible;
                ComboBoxSalesCustomer.SelectedIndex = 0;
            }
            else if (selectedValue == "Proveedor")
            {
                LabelSupplier.Visibility = Visibility.Visible;
                ComboBoxSupplier.Visibility = Visibility.Visible;
                ComboBoxSupplier.SelectedIndex = 0;
            }
            presenter.LoadProducts();
            ComboBoxProducts.ItemsSource = Products;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            presenter.SaveWeightTicket();
        }

        private void ButtonGetDate2_Click(object sender, RoutedEventArgs e)
        {
            presenter.SetExitDateToNow();
        }

        private void ButtonGetDate_Click(object sender, RoutedEventArgs e)
        {
            presenter.SetEntranceDateToNow();
        }

        private void CheckBoxCalculatePrices_CheckedUnChecked(object sender, RoutedEventArgs e)
        {
            presenter.CalculateTotals();
        }

        private void CalculatePrices_LostFocus(object sender, RoutedEventArgs e)
        {
            presenter.CalculateTotals();
        }

        private void btnWeight1_Click(object sender, RoutedEventArgs e)
        {
            WeightReader reader = new WeightReader();            
            reader.ShowDialog();
            CurrentWeightTicket.EntranceWeightKg = reader.Weight;
            CurrentWeightTicket.RaiseUpdateProperties();
            presenter.CalculateTotals();   

        }

        private void btnWeight2_Click(object sender, RoutedEventArgs e)
        {
            WeightReader reader = new WeightReader();
            reader.ShowDialog();
            CurrentWeightTicket.ExitWeightKg = reader.Weight;
            CurrentWeightTicket.RaiseUpdateProperties();
            presenter.CalculateTotals();
        }

        private void ButtonEntrancePrint_Click(object sender, RoutedEventArgs e)
        {
            presenter.PrintFirstPart();            
        }

        private void ButtonExitPrint_Click(object sender, RoutedEventArgs e)
        {
            presenter.PrintSecondPart();
        }

        private void txbSearhcTicket_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxTickets.ItemsSource).Refresh();
        }

        private void ListBoxTickets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxTickets.SelectedItem != null)
            {
                presenter.UpdateCurrentWeightTicket();
            }
        }
    }     
}
