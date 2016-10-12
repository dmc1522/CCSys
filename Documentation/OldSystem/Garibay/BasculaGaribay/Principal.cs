using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Microsoft.Synchronization.Data;

namespace BasculaGaribay
{
    public partial class Principal : Form
    {
        public static Principal _FormPrincipal = null;

        public static void OpenFormInDock(DockContent dialog)
        {
            dialog.Show(_FormPrincipal._dockPanel, DockState.Document);
        }
        private Boolean cerrosession;
        public Principal()
        {
            _FormPrincipal = this;
            InitializeComponent();
            cerrosession = false;

#if DEBUG
            WSConnector.Instance.Username = "jluis";
            WSConnector.Instance.Password = "JO123SE";
            WSConnector.Instance.Login();            
#endif
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //String exc = "";
            //if (WSConnector.Instance.validaLaSession(out exc))
            //{
            frmAddProductor myform = new frmAddProductor();
            myform.ShowDialog();
            //}
            //else
            //{
                
            //    MessageBox.Show(exc, "ERROR!!");
            //    Login fr = new Login(1);
            //    fr.ShowDialog();
               
                

            //}
        }

        private void button4_Click(object sender, EventArgs e)

        {
            //String exc = ""; 
            frmAddBoletas addBoleta = new frmAddBoletas();
            addBoleta.ShowDialog();
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //String exc = "";
            //if(WSConnector.Instance.validaLaSession(out exc)){
            frmListBoletas listBoleta = new frmListBoletas();

            listBoleta.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show(exc, "ERROR!!");
            //    Login fr = new Login(1);
            //    fr.ShowDialog();
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            //String exc="";
            //if(WSConnector.Instance.validaLaSession(out exc)){
            frmListProductores myform = new frmListProductores();
            myform.ShowDialog();
            //}else{
            //    MessageBox.Show(exc, "ERROR!!");
            //    Login fr = new Login(1);
            //    fr.ShowDialog();
            //}
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WSConnector.Instance.logOut();
            cerrosession = true;
            this.Close(); 
        }

        private void btnConfigurations_Click(object sender, EventArgs e)
        {
            //String exc = "";
            
            try
            {
                String url = WSConnector.Instance.Url;
                frmConfiguracion cfg = new frmConfiguracion(url);
                cfg.ShowDialog(this);
                WSConnector.Instance.Url = cfg.Url;
                WSConnector.Instance.Login();
            }catch(Exception ex){
                MessageBox.Show(ex.Message, "ERROR!");
            }
            
        }


        /*
         * String exc = "";
            
            try
            {
                String url = WSConnector.Instance.Url;
                frmConfiguracion cfg = new frmConfiguracion(url);
                cfg.ShowDialog(this);
                WSConnector.Instance.Url = cfg.Url;
                WSConnector.Instance.Login();
            }catch(Exception ex){
                MessageBox.Show(ex.Message, "ERROR!");
            }
         */
        private void UpdateButtonStatus()
        {
            
        }

        private void Principal_Load(object sender, EventArgs e)
        {
#if DEBUG
            this.cacheToolStripMenuItem.Visible = true;
#else
            this.cacheToolStripMenuItem.Visible = false;
#endif
            frmBoletasPendientesToolBox dlg = new frmBoletasPendientesToolBox();
            dlg.Show(this._dockPanel, DockState.DockRight);
        }

        private void Principal_Deactivate(object sender, EventArgs e)
        {
         

        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!cerrosession)
            WSConnector.Instance.logOut();
            
        }

        private void agregarProductorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddProductor dlg = new frmAddProductor();
            dlg.Show(this._dockPanel, DockState.Document);
        }

        private void listaDeProductoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListProductores dlg = new frmListProductores();
            dlg.Show(this._dockPanel, DockState.Document);
        }

        private void agregarBoletaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddBoletas dlg = new frmAddBoletas();
            dlg.Show(this._dockPanel, DockState.Document);
        }

        private void listaDeBoletasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListBoletas dlg = new frmListBoletas();
            dlg.Show(this._dockPanel, DockState.Document);
        }

        private void listaDeBoletasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormBoletasSinSalir formBoletasSinSalir = new FormBoletasSinSalir();
            formBoletasSinSalir.Show(this._dockPanel, DockState.Document);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WSConnector.Instance.SaveCacheForOfflineMode();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WSConnector.Instance.LoadCacheForOfflineMode();
        }

        private void syncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WSConnector.Instance.SyncDatabase();
        }

        private void boletasListInDSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Principal.OpenFormInDock(new frmBoletasListFromDS());
        }

        private void recreateCacheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WSConnector.Instance.RecreateDatabase();
        }

        private void goOfflineModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WSConnector.Instance.IsOfflineMode = !WSConnector.Instance.IsOfflineMode;
        }

        private void listaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Principal.OpenFormInDock(new frmProveedores());
        }

        private void agregarNuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Principal.OpenFormInDock(new frmProveedor());
        }

    }
}
