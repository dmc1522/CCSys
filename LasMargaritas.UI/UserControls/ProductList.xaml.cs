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
    public partial class ProductList : UserControl, IProductView
    {
        #region Private variables
        private ProductPresenter presenter;
        private List<SelectableModel> _Products;
        private bool listLoaded;
        #endregion

        #region Public Properties
        public Token Token { get; set; }




        #endregion

        #region IProductView implementation
        public Product CurrentProduct { get; set; }
        
        public List<SelectableModel> Units { get; set; }
        public List<SelectableModel> Presentations { get; set; }
        public List<SelectableModel> ProductGroups { get; set; }
        public List<SelectableModel> AgriculturalBrands { get; set; }

        public int SelectedId
        {
            get
            {
                if(ListBoxProducts.SelectedItem != null)
                {
                    return ((SelectableModel)ListBoxProducts.SelectedItem).Id;
                }
                return -1;
            }
            set
            {
                if (value <= 0)
                {

                    ListBoxProducts.SelectedValue = null;
                    ListBoxProducts.SelectedIndex = -1;
                }
                else
                {
                    foreach (var item in ListBoxProducts.Items)
                    {
                        if(((SelectableModel)item).Id == value)
                        {
                            ListBoxProducts.SelectedItem = item;
                            ListBoxProducts.ScrollIntoView(item);                            
                            break;
                        }
                    }
                }

            }
        }        

        public List<SelectableModel> Products
        {
            get
            { 
                return _Products;
            }
            set
            {
                _Products = value;
                ListBoxProducts.ItemsSource = _Products;
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListBoxProducts.ItemsSource);
                view.Filter = ProductFilter;
            }
        }

        private bool ProductFilter(object item)
        {
            if (string.IsNullOrEmpty(TextBoxSearchProducts.Text))
                return true;
            else
                return ((item as SelectableModel).Name != null && (item as SelectableModel).Name.IndexOf(TextBoxSearchProducts.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    || ((item as SelectableModel).Id.ToString().IndexOf(TextBoxSearchProducts.Text, StringComparison.OrdinalIgnoreCase) >= 0) ;
                       
        }


        public void HandleException(Exception ex, string method, Guid errorId)
        {
        }
        
        #endregion

        #region Constructor
        public ProductList()
        {
            InitializeComponent();           
            presenter = new ProductPresenter(this);
            CurrentProduct = new Product();
            GridProductDetails.DataContext = CurrentProduct;
        }
        #endregion

        #region Private Methods
        private void UserControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true && !listLoaded)
            {
                presenter.Token = Token;
                presenter.Initialize();             
                ComboBoxUnit.ItemsSource = Units;
                ComboBoxAgriculturalBrand.ItemsSource = AgriculturalBrands;
                ComboBoxProductGroup.ItemsSource = ProductGroups;                
                listLoaded = true;
            }
        }

       

        private void ButtonSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            presenter.SaveProduct();
        }
    

        private void ListBoxProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListBoxProducts.SelectedItem != null)
            {
                presenter.UpdateCurrentProduct();
            }            
        }
        #endregion

        private void ButtonAddProduct_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            presenter.NewProduct();
        }

        private void TextBoxSearchProducts_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxProducts.ItemsSource).Refresh();
        }

        private void ButtonDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Realmente deseas eliminar a este Proveedor?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                presenter.DeleteProduct();
            }

        }

   
    }
}
