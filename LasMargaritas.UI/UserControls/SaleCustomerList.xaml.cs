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
    public partial class SaleCustomerList : UserControl, ISaleCustomerView
    {
        #region Private variables
        private SaleCustomerPresenter presenter;
        private List<SelectableModel> _SaleCustomers;
        private bool listLoaded;
        #endregion

        #region Public Properties
        public Token Token { get; set; }




        #endregion

        #region IProducerView implementation
        public SaleCustomer CurrentSaleCustomer { get; set; }
        
        public List<SelectableModel> States { get; set; }

        public int SelectedId
        {
            get
            {
                if(ListBoxSaleCustomers.SelectedItem != null)
                {
                    return ((SelectableModel)ListBoxSaleCustomers.SelectedItem).Id;
                }
                return -1;
            }
            set
            {
                if (value <= 0)
                {

                    ListBoxSaleCustomers.SelectedValue = null;
                    ListBoxSaleCustomers.SelectedIndex = -1;
                }
                else
                {
                    foreach (var item in ListBoxSaleCustomers.Items)
                    {
                        if(((SelectableModel)item).Id == value)
                        {
                            ListBoxSaleCustomers.SelectedItem = item;
                            ListBoxSaleCustomers.ScrollIntoView(item);                            
                            break;
                        }
                    }
                }

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
                ListBoxSaleCustomers.ItemsSource = _SaleCustomers;
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListBoxSaleCustomers.ItemsSource);
                view.Filter = SaleCustomerFilter;
            }
        }

        private bool SaleCustomerFilter(object item)
        {
            if (string.IsNullOrEmpty(TextBoxSearchSaleCustomers.Text))
                return true;
            else
                return ((item as SelectableModel).Name != null && (item as SelectableModel).Name.IndexOf(TextBoxSearchSaleCustomers.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    || ((item as SelectableModel).Id.ToString().IndexOf(TextBoxSearchSaleCustomers.Text, StringComparison.OrdinalIgnoreCase) >= 0) ;
                       
        }


        public void HandleException(Exception ex, string method, Guid errorId)
        {
        }
        
        #endregion

        #region Constructor
        public SaleCustomerList()
        {
            InitializeComponent();           
            presenter = new SaleCustomerPresenter(this);
            CurrentSaleCustomer = new SaleCustomer();
            GridSaleCustomerDetails.DataContext = CurrentSaleCustomer;
        }
        #endregion

        #region Private Methods
        private void UserControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true && !listLoaded)
            {
                presenter.Token = Token;
                presenter.Initialize();             
                ComboBoxState.ItemsSource = States;           
                listLoaded = true;
            }
        }

       

        private void ButtonSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            presenter.SaveSaleCustomer();
        }
    

        private void ListBoxSaleCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListBoxSaleCustomers.SelectedItem != null)
            {
                presenter.UpdateCurrentSaleCustomer();
            }            
        }
        #endregion

        private void ButtonAddSaleCustomer_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            presenter.NewSaleCustomer();
        }

        private void TextBoxSearchSaleCustomers_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxSaleCustomers.ItemsSource).Refresh();
        }

        private void ButtonDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Realmente deseas eliminar a este Cliente de venta?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                presenter.DeleteSaleCustomer();
            }

        }

   
    }
}
