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
    public partial class WareHouseList : UserControl, IWareHouseView
    {
        #region Private variables
        private WareHousePresenter presenter;
        private List<SelectableModel> _WareHouses;
        private bool listLoaded;
        #endregion

        #region Public Properties
        public Token Token { get; set; }




        #endregion

        #region IWareHouseView implementation
        public WareHouse CurrentWareHouse { get; set; }
                
        public int SelectedId
        {
            get
            {
                if(ListBoxWareHouses.SelectedItem != null)
                {
                    return ((SelectableModel)ListBoxWareHouses.SelectedItem).Id;
                }
                return -1;
            }
            set
            {
                if (value <= 0)
                {

                    ListBoxWareHouses.SelectedValue = null;
                    ListBoxWareHouses.SelectedIndex = -1;
                }
                else
                {
                    foreach (var item in ListBoxWareHouses.Items)
                    {
                        if(((SelectableModel)item).Id == value)
                        {
                            ListBoxWareHouses.SelectedItem = item;
                            ListBoxWareHouses.ScrollIntoView(item);                            
                            break;
                        }
                    }
                }

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
                ListBoxWareHouses.ItemsSource = _WareHouses;
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListBoxWareHouses.ItemsSource);
                view.Filter = WareHouseFilter;
            }
        }

        private bool WareHouseFilter(object item)
        {
            if (string.IsNullOrEmpty(TextBoxSearchWareHouses.Text))
                return true;
            else
                return ((item as SelectableModel).Name != null && (item as SelectableModel).Name.IndexOf(TextBoxSearchWareHouses.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    || ((item as SelectableModel).Id.ToString().IndexOf(TextBoxSearchWareHouses.Text, StringComparison.OrdinalIgnoreCase) >= 0) ;
                       
        }


        public void HandleException(Exception ex, string method, Guid errorId)
        {
        }
        
        #endregion

        #region Constructor
        public WareHouseList()
        {
            InitializeComponent();           
            presenter = new WareHousePresenter(this);
            CurrentWareHouse = new WareHouse();
            GridWareHouseDetails.DataContext = CurrentWareHouse;
        }
        #endregion

        #region Private Methods
        private void UserControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true && !listLoaded)
            {
                presenter.Token = Token;
                presenter.Initialize();                               
                listLoaded = true;
            }
        }

       

        private void ButtonSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            presenter.SaveWareHouse();
        }
    

        private void ListBoxWareHouses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListBoxWareHouses.SelectedItem != null)
            {
                presenter.UpdateCurrentWareHouse();
            }            
        }
        #endregion

        private void ButtonAddWareHouse_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            presenter.NewWareHouse();
        }

        private void TextBoxSearchWareHouses_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxWareHouses.ItemsSource).Refresh();
        }

        private void ButtonDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Realmente deseas eliminar este Almacen?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                presenter.DeleteWareHouse();
            }

        }

   
    }
}
