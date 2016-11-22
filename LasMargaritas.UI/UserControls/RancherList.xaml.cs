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
    public partial class RancherList : UserControl, IRancherView
    {
        #region Private variables
        private RancherPresenter presenter;
        private List<SelectableModel> _Ranchers;
        private bool listLoaded;
        #endregion

        #region Public Properties
        public Token Token { get; set; }




        #endregion

        #region IRancherView implementation
        public Rancher CurrentRancher { get; set; }
        
        public List<SelectableModel> States { get; set; }

        public int SelectedId
        {
            get
            {
                if(ListBoxRanchers.SelectedItem != null)
                {
                    return ((SelectableModel)ListBoxRanchers.SelectedItem).Id;
                }
                return -1;
            }
            set
            {
                if (value <= 0)
                {

                    ListBoxRanchers.SelectedValue = null;
                    ListBoxRanchers.SelectedIndex = -1;
                }
                else
                {
                    foreach (var item in ListBoxRanchers.Items)
                    {
                        if(((SelectableModel)item).Id == value)
                        {
                            ListBoxRanchers.SelectedItem = item;
                            ListBoxRanchers.ScrollIntoView(item);                            
                            break;
                        }
                    }
                }

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
                ListBoxRanchers.ItemsSource = _Ranchers;
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListBoxRanchers.ItemsSource);
                view.Filter = RancherFilter;
            }
        }

        private bool RancherFilter(object item)
        {
            if (string.IsNullOrEmpty(TextBoxSearchRanchers.Text))
                return true;
            else
                return ((item as SelectableModel).Name != null && (item as SelectableModel).Name.IndexOf(TextBoxSearchRanchers.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    || ((item as SelectableModel).Id.ToString().IndexOf(TextBoxSearchRanchers.Text, StringComparison.OrdinalIgnoreCase) >= 0) ;
                       
        }


        public void HandleException(Exception ex, string method, Guid errorId)
        {
        }
        
        #endregion

        #region Constructor
        public RancherList()
        {
            InitializeComponent();           
            presenter = new RancherPresenter(this);
            CurrentRancher = new Rancher();
            GridRancherDetails.DataContext = CurrentRancher;
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
            presenter.SaveRancher();
        }
    

        private void ListBoxRanchers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListBoxRanchers.SelectedItem != null)
            {
                presenter.UpdateCurrentRancher();
            }            
        }
        #endregion

        private void ButtonAddRancher_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            presenter.NewRancher();
        }

        private void TextBoxSearchRanchers_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxRanchers.ItemsSource).Refresh();
        }

        private void ButtonDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Realmente deseas eliminar a este Cliente de venta?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                presenter.DeleteRancher();
            }

        }

   
    }
}
