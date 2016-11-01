using LasMargaritas.BL;
using LasMargaritas.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Controls;


namespace LasMargaritas.UI.UserControls
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class ProducerAddUpdate : UserControl
    {
        private string baseUrl;
        private string insertAction;
        private string updateAction;
        private string deleteAction;
        private string getAllAction;
        private string getByIdAction;
        public ProducerAddUpdate()
        {
            InitializeComponent();            
            baseUrl = @"http://lasmargaritas.azurewebsites.net/";
            insertAction = "Producer/Add";
            updateAction = "Producer/Update";
            deleteAction = "Producer/Delete";
            getAllAction = "Producer/GetAll";
            getByIdAction = "Producer/GetById";
           
        }

    

     
    }
}
