using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Threading;
using System.Diagnostics;

namespace BasculaGaribay
{
    public partial class frmBoletasPendientesToolBox : DockContent
    {
        public frmBoletasPendientesToolBox()
        {
            InitializeComponent();
        }

        private void frmBoletasPendientesToolBox_Load(object sender, EventArgs e)
        {
            this.UpdateLista();
            this.StartThread();
        }

        private void StartThread()
        {
            this.UpdaterThread = new Thread(new ThreadStart(LoadLista));
            this.KeepThreadRunning = true;
            this.UpdaterThread.Start();
        }

        private DateTime LastUpdate = DateTime.Now;

        private void LoadLista()
        {
            while (this.KeepThreadRunning)
            {
                try
                {
                    Debug.WriteLine(string.Format("Mins from Last Update: {0}", (DateTime.Now - LastUpdate).TotalMinutes));
                    if (((DateTime.Now - LastUpdate).TotalMinutes > 1))
                    {
                        this.LastUpdate = DateTime.Now;
                        this.UpdateLista();
                    }
                    Thread.Sleep(3000);
                }
                catch {}
            }
        }

        private Thread UpdaterThread = null;
        private bool _KeepThreadRunning = true;
        private bool _IsUpdating = false;
        
        public bool IsUpdating
        {
            get { return _IsUpdating; }
            set { _IsUpdating = value; }
        }
        
        public bool KeepThreadRunning
        {
            get { lock(this) {return _KeepThreadRunning; }}
            set { lock(this) {_KeepThreadRunning = value;} }
        }

        private String Addfiltros()
        {
            String filtros = String.Empty;
            try
            {
                filtros = "pesodesalida = 0 AND CICLOID >4 AND BodegaID = 1 ";
            }
            catch
            {
            }
            return filtros;
        }

        private DataTable dtBoletas = new DataTable();

        private delegate void UpdateDelegate();

        private void UpdateLista()
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new UpdateDelegate(UpdateLista));
                return;
            }
            if(!this.IsUpdating)
            {
                try
                {
                    this.IsUpdating = true;
                    Debug.WriteLine("Updating Boletas Pendientes");
                    if (WSConnector.Instance.GetBoletasPendientes(out dtBoletas))
                    {
                        try
                        {
                            this.dgvBoletas.DataSource = dtBoletas;
                            foreach (DataGridViewColumn col in this.dgvBoletas.Columns)
                            {
                                col.Visible = false;
                            }
                            this.dgvBoletas.Columns["CicloName"].Visible = true;
                            this.dgvBoletas.Columns["CicloName"].HeaderText = "Ciclo";
                            this.dgvBoletas.Columns["boletaID"].Visible = true;
                        }
                        catch{}
                    }
                    this.IsUpdating = false;
                }
                catch(Exception ex)
                {
                    Logger.Instance.LogException(ex);
                }
            }
        }

        private void frmBoletasPendientesToolBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.KeepThreadRunning = false;
        }

        private void dgvBoletas_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvBoletas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String mensaje = "";
                if (WSConnector.Instance.validaLaSession(out mensaje))
                {
                    frmAddBoletas myForm = new frmAddBoletas(this.dgvBoletas.Rows[e.RowIndex].Cells["boletaID"].Value.ToString());
                    Principal.OpenFormInDock(myForm);

                }
                else
                {
                    MessageBox.Show(mensaje, "ERROR!!");
                    Login fr = new Login(1);
                    fr.ShowDialog();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(ex);
            }
        }
    }
}
