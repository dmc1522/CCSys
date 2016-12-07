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
    public partial class CicleList : UserControl, ICicleView
    {
        #region Private variables
        private CiclePresenter presenter;
        private List<SelectableModel> _Cicles;
        private bool listLoaded;
        #endregion

        #region Public Properties
        public Token Token { get; set; }




        #endregion

        #region ICicleView implementation
        public Cicle CurrentCicle { get; set; }
                
        public int SelectedId
        {
            get
            {
                if(ListBoxCicles.SelectedItem != null)
                {
                    return ((SelectableModel)ListBoxCicles.SelectedItem).Id;
                }
                return -1;
            }
            set
            {
                if (value <= 0)
                {

                    ListBoxCicles.SelectedValue = null;
                    ListBoxCicles.SelectedIndex = -1;
                }
                else
                {
                    foreach (var item in ListBoxCicles.Items)
                    {
                        if(((SelectableModel)item).Id == value)
                        {
                            ListBoxCicles.SelectedItem = item;
                            ListBoxCicles.ScrollIntoView(item);                            
                            break;
                        }
                    }
                }

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
                ListBoxCicles.ItemsSource = _Cicles;
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListBoxCicles.ItemsSource);
                view.Filter = CicleFilter;
            }
        }

        private bool CicleFilter(object item)
        {
            if (string.IsNullOrEmpty(TextBoxSearchCicles.Text))
                return true;
            else
                return ((item as SelectableModel).Name != null && (item as SelectableModel).Name.IndexOf(TextBoxSearchCicles.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    || ((item as SelectableModel).Id.ToString().IndexOf(TextBoxSearchCicles.Text, StringComparison.OrdinalIgnoreCase) >= 0) ;
                       
        }


        public void HandleException(Exception ex, string method, Guid errorId)
        {
        }
        
        #endregion

        #region Constructor
        public CicleList()
        {
            InitializeComponent();           
            presenter = new CiclePresenter(this);
            CurrentCicle = new Cicle();
            GridCicleDetails.DataContext = CurrentCicle;
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
            presenter.SaveCicle();
        }
    

        private void ListBoxCicles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListBoxCicles.SelectedItem != null)
            {
                presenter.UpdateCurrentCicle();
            }            
        }
        #endregion

        private void ButtonAddCicle_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            presenter.NewCicle();
        }

        private void TextBoxSearchCicles_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxCicles.ItemsSource).Refresh();
        }

        private void ButtonDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Realmente deseas eliminar este Ciclo?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                presenter.DeleteCicle();
            }

        }

   
    }
}
