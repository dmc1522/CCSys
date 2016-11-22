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
    public partial class SupplierList : UserControl, ISupplierView
    {
        #region Private variables
        private SupplierPresenter presenter;
        private List<SelectableModel> _Suppliers;
        private bool listLoaded;
        #endregion

        #region Public Properties
        public Token Token { get; set; }




        #endregion

        #region IProducerView implementation
        public Supplier CurrentSupplier { get; set; }
        
        public List<SelectableModel> States { get; set; }

        public int SelectedId
        {
            get
            {
                if(ListBoxSuppliers.SelectedItem != null)
                {
                    return ((SelectableModel)ListBoxSuppliers.SelectedItem).Id;
                }
                return -1;
            }
            set
            {
                if (value <= 0)
                {

                    ListBoxSuppliers.SelectedValue = null;
                    ListBoxSuppliers.SelectedIndex = -1;
                }
                else
                {
                    foreach (var item in ListBoxSuppliers.Items)
                    {
                        if(((SelectableModel)item).Id == value)
                        {
                            ListBoxSuppliers.SelectedItem = item;
                            ListBoxSuppliers.ScrollIntoView(item);                            
                            break;
                        }
                    }
                }

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
                ListBoxSuppliers.ItemsSource = _Suppliers;
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListBoxSuppliers.ItemsSource);
                view.Filter = SupplierFilter;
            }
        }

        private bool SupplierFilter(object item)
        {
            if (string.IsNullOrEmpty(TextBoxSearchSuppliers.Text))
                return true;
            else
                return ((item as SelectableModel).Name != null && (item as SelectableModel).Name.IndexOf(TextBoxSearchSuppliers.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    || ((item as SelectableModel).Id.ToString().IndexOf(TextBoxSearchSuppliers.Text, StringComparison.OrdinalIgnoreCase) >= 0) ;
                       
        }


        public void HandleException(Exception ex, string method, Guid errorId)
        {
        }
        
        #endregion

        #region Constructor
        public SupplierList()
        {
            InitializeComponent();           
            presenter = new SupplierPresenter(this);
            CurrentSupplier = new Supplier();
            GridSupplierDetails.DataContext = CurrentSupplier;
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
            presenter.SaveSupplier();
        }
    

        private void ListBoxSuppliers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListBoxSuppliers.SelectedItem != null)
            {
                presenter.UpdateCurrentSupplier();
            }            
        }
        #endregion

        private void ButtonAddSupplier_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            presenter.NewSupplier();
        }

        private void TextBoxSearchSuppliers_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxSuppliers.ItemsSource).Refresh();
        }

        private void ButtonDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Realmente deseas eliminar a este Proveedor?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                presenter.DeleteSupplier();
            }

        }

   
    }
}
