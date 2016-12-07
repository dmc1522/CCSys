using LasMargaritas.BL;
using LasMargaritas.BL.Views;
using LasMargaritas.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Controls;
using System.Collections.Generic;
using LasMargaritas.BL.Presenters;
using WebEye.Controls.Wpf;
using System.Linq;
using System.IO;    
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Data;
using System.Windows;

namespace LasMargaritas.UI.UserControls
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class WeightTicketsReport : UserControl, IWeightTicketReportView
    {
        #region Private variables
        private WeightTicketReportPresenter presenter;
        private ReportData _ReportData;
        private List<SelectableModel> _Producers;
        private List<SelectableModel> _Ranchers;
        private List<SelectableModel> _Suppliers;
        private List<SelectableModel> _SaleCustomers;
        private List<SelectableModel> _Products;
        private List<SelectableModel> _WareHouses;
        private List<SelectableModel> _Cicles;        
        private bool listShown;

        #endregion

        #region Constructor
        public WeightTicketsReport()
        {
            InitializeComponent();
            presenter = new WeightTicketReportPresenter(this);
            CurrentFilters = new WeightTicketReportFilterModel();            
            GridFilters.DataContext = CurrentFilters;
                        
        }
        #endregion
        #region Public Properties
        public Token Token { get; set; }

        #endregion

        #region IWeightTicketReportView implementation        

        public ReportData ReportData
        {
            get
            {
                return _ReportData;
            }

            set
            {
                _ReportData = value;
                /*//Get the column names from first element
                if(ReportData != null && ReportData.Count > 0)
                {
                    List<ReportDataItem> firstElement = ReportData.ElementAt(0);
                    foreach(ReportDataItem columnData in firstElement)
                    {
                        DataGridTextColumn textColumn = new DataGridTextColumn();
                        textColumn.Header = columnData.Name;                       
                        textColumn.Binding = new Binding(".Value");
                        if (columnData.Type == typeof(int))
                            textColumn.Binding.StringFormat = "N";
                        if (columnData.Type == typeof(decimal))
                            textColumn.Binding.StringFormat = "C2";
                        if (columnData.Type == typeof(float))
                            textColumn.Binding.StringFormat = "N2";
                        if (columnData.Type == typeof(DateTime))
                            textColumn.Binding.StringFormat = "dd/MM/yyy HH:mm";
                        DataGridWeightTickets.Columns.Add(textColumn);
                    }
                }
                DataGridWeightTickets.ItemsSource = _ReportData;*/                
            }
        }


        public WeightTicketReportFilterModel CurrentFilters { get; set; }
        

        public List<SelectableModel> Products
        {
            get
            {
                return _Products;
            }

            set
            {
                _Products = value;
                _Products.Insert(0, new SelectableModel(-1, "TODOS"));
                ComboBoxProduct.ItemsSource = _Products;
            }
        }
        public List<SelectableModel> Producers
        {
            get
            {
                return _Producers;
            }

            set
            {
                _Producers = value;
                _Producers.Insert(0, new SelectableModel(-1, "TODOS"));
                ComboBoxProducer.ItemsSource = _Producers;
            }
        }

        public List<SelectableModel> Cicles
        {
            get
            {
                return _Cicles;
            }

            set
            {
                _Cicles = value;
                ComboBoxCicle.ItemsSource = _Cicles;
            }
        }

        public List<SelectableModel> Ranchers
        {
            get
            {
                return _Ranchers;
            }

            set
            {
                _Ranchers = value;
                _Ranchers.Insert(0, new SelectableModel(-1, "TODOS"));
                ComboBoxRancher.ItemsSource = _Ranchers;
            }
        }

        public List<SelectableModel> Suppliers
        {
            get
            {
                return _Suppliers;
            }

            set
            {
                _Suppliers = value;
                _Suppliers.Insert(0, new SelectableModel(-1, "TODOS"));
                ComboBoxSupplier.ItemsSource = _Suppliers;
            }
        }

        public List<SelectableModel> SaleCustomers
        {
            get
            {
                return _SaleCustomers;
            }

            set
            {
                _SaleCustomers = value;
                _SaleCustomers.Insert(0, new SelectableModel(-1, "TODOS"));
                ComboBoxSaleCustomer.ItemsSource = _SaleCustomers;
            }
        }

     
        public List<SelectableModel> WareHouses
        {
            get
            {
                return _WareHouses;
            }

            set
            {
                _WareHouses = value;
                _WareHouses.Insert(0, new SelectableModel(-1, "TODAS"));
                ComboBoxWareHouse.ItemsSource = _WareHouses;
            }
        }

        public void HandleException(Exception ex, string method, Guid errorId)
        {

            string errorMessage = string.Empty;           
            errorMessage = "Hubo un problema en la última acción. Detalles: - Unknown Exception ";            
            errorMessage += ". " + ex.Message + ". Method: " + method;
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        #endregion

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true && !listShown)
            {
                presenter.Token = Token;
                presenter.LoadCatalogs();
                ComboBoxProduct.SelectedIndex = 0;
                ComboBoxProducer.SelectedIndex = 0;
                ComboBoxRancher.SelectedIndex = 0;
                ComboBoxSaleCustomer.SelectedIndex = 0;
                ComboBoxSupplier.SelectedIndex = 0;
                ComboBoxWareHouse.SelectedIndex = 0;
                RadioButtonProducer.IsChecked = true;
                ComboBoxCicle.SelectedIndex = 0;
                TextBoxStartDate.IsEnabled = false;
                TextBoxEndDate.IsEnabled = false;
                listShown = true;
            }
        }

        private void ButtonReloadReport_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentFilters.ProducerId == -1) CurrentFilters.ProducerId = null;
            if (CurrentFilters.RancherId == -1) CurrentFilters.RancherId = null;
            if (CurrentFilters.SaleCustomerId == -1) CurrentFilters.SaleCustomerId = null;
            if (CurrentFilters.SupplierId == -1) CurrentFilters.SupplierId = null;
            if (CurrentFilters.ProductId == -1) CurrentFilters.ProductId = null;
            if (CurrentFilters.WareHouseId == -1) CurrentFilters.WareHouseId = null;
            if (CheckBoxDateFilter.IsChecked.HasValue && !CheckBoxDateFilter.IsChecked.Value)
            {
                CurrentFilters.EndDateTime = null;
                CurrentFilters.StartDateTime = null;
            }
            if (RadioButtonProducer.IsChecked.HasValue && RadioButtonProducer.IsChecked.Value) CurrentFilters.WeightTicketType = (int) WeightTicketType.Producer;
            if (RadioButtonRancher.IsChecked.HasValue && RadioButtonRancher.IsChecked.Value) CurrentFilters.WeightTicketType = (int)WeightTicketType.Rancher;
            if (RadioButtonSaleCustomer.IsChecked.HasValue && RadioButtonSaleCustomer.IsChecked.Value) CurrentFilters.WeightTicketType = (int)WeightTicketType.SaleCustomer;
            if (RadioButtonSupplier.IsChecked.HasValue && RadioButtonSupplier.IsChecked.Value) CurrentFilters.WeightTicketType = (int)WeightTicketType.Supplier;

            presenter.LoadReport();
        }

    
        private void RadioButtonProducer_Checked(object sender, RoutedEventArgs e)
        {
            ComboBoxRancher.IsEnabled = false;            
            ComboBoxSaleCustomer.IsEnabled = false;            
            ComboBoxSupplier.IsEnabled = false;
            ComboBoxProducer.IsEnabled = true;
            
        }

        private void RadioButtonSaleCustomer_Checked(object sender, RoutedEventArgs e)
        {
            ComboBoxRancher.IsEnabled = false;
            ComboBoxSaleCustomer.IsEnabled = true;
            ComboBoxSupplier.IsEnabled = false;
            ComboBoxProducer.IsEnabled = false;
        }

        private void RadioButtonSupplier_Checked(object sender, RoutedEventArgs e)
        {
            ComboBoxRancher.IsEnabled = false;
            ComboBoxSaleCustomer.IsEnabled = false;
            ComboBoxSupplier.IsEnabled = true;
            ComboBoxProducer.IsEnabled = false;
        }

        private void RadioButtonRancher_Checked(object sender, RoutedEventArgs e)
        {
            ComboBoxRancher.IsEnabled = true;
            ComboBoxSaleCustomer.IsEnabled = false;
            ComboBoxSupplier.IsEnabled = false;
            ComboBoxProducer.IsEnabled = false;

        }

        private void CheckBoxDateFilter_Checked(object sender, RoutedEventArgs e)
        {
            TextBoxStartDate.IsEnabled = true;
            TextBoxEndDate.IsEnabled = true;
        }

        private void CheckBoxDateFilter_Unchecked(object sender, RoutedEventArgs e)
        {
            TextBoxStartDate.IsEnabled = false;
            TextBoxEndDate.IsEnabled = false;
        }

        private void ComboBoxEntranceExitType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentFilters.EntranceWeightTicketsOnly = null;
            CurrentFilters.ExitWeightTicketsOnly = null;
            if (ComboBoxEntranceExitType.SelectedIndex == 1)
            {
                CurrentFilters.EntranceWeightTicketsOnly = true;                
            }
            else
            {                
                CurrentFilters.ExitWeightTicketsOnly = true;
            }
        }
    }
}
