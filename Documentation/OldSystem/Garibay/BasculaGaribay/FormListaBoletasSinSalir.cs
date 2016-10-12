using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace BasculaGaribay
{
    public partial class FormBoletasSinSalir : DockContent
    {
        
        private DataTable dtCiclos = new DataTable();
        private DataTable dtClientes = new DataTable();
        private DataTable dtProductos = new DataTable();
        private DataTable dtProductores = new DataTable();
        private DataTable dtBoletas = new DataTable();
        private DataTable dtBodegas = new DataTable();
        public FormBoletasSinSalir()
        {
            InitializeComponent();
        }
        private void UpdateCombos()
        {
            try
            {
                if(WSConnector.Instance.GetAllCiclos(out dtCiclos))
                {
                    this.cmbCiclos.DataSource = dtCiclos;
                    this.cmbCiclos.ValueMember = "cicloID";
                    this.cmbCiclos.DisplayMember = "Ciclo";

                    if (WSConnector.Instance.GetAllBodegas(out dtBodegas))
                    {
                        this.cmbBodegas.DataSource = dtBodegas;
                        this.cmbBodegas.ValueMember = "bodegaID";
                        this.cmbBodegas.DisplayMember = "bodega";
                        this.cmbBodegas.SelectedIndex = 0;
          
                    }
                    else
                    {
                        MessageBox.Show("No se han podido consultar los datos de las bodegas", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.cmbBodegas.Enabled = false;
                     
                    }
                }
                else
                {
                    MessageBox.Show("No se han podido consultar los datos de los Ciclos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.cmbBodegas.Enabled = false;
                    this.cmbCiclos.Enabled = false;                  

                }       
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR!!");
            }
        }
        private void UpdateLista()
        {             
            if (WSConnector.Instance.GetBoletas(Addfiltros(),out dtBoletas))
            {
                try
                {

                    //dtBoletas.DefaultView.RowFilter = "BODEGA='SUCURSAL MARGARITAS'";
                    this.dgvBoletas.DataSource = dtBoletas;
                    this.dgvBoletas.Columns[15].Visible = false;
                    this.dgvBoletas.Columns[16].Visible = false;
                    this.dgvBoletas.Columns[17].Visible = false;
                    this.dgvBoletas.Columns[18].Visible = false;
                    this.dgvBoletas.Columns["bodegaID"].Visible = false;
                    this.dgvBoletas.Columns["No_BOLETA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dgvBoletas.Columns["Ticket"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dgvBoletas.Columns["PESO_NETO_DE_ENTRADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dgvBoletas.Columns["PESO_NETO_DE_ENTRADA"].DefaultCellStyle.Format="N2";
                    this.dgvBoletas.Columns["PESO_NETO_DE_SALIDA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dgvBoletas.Columns["PESO_NETO_DE_SALIDA"].DefaultCellStyle.Format="N2";
                    this.dgvBoletas.Columns["HUMEDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dgvBoletas.Columns["HUMEDAD"].DefaultCellStyle.Format = "N2";
                    this.dgvBoletas.Columns["IMPUREZAS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dgvBoletas.Columns["IMPUREZAS"].DefaultCellStyle.Format = "N2";
                    this.dgvBoletas.Columns["DCTO DE SECADO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dgvBoletas.Columns["DCTO DE SECADO"].DefaultCellStyle.Format = "N2";
                    this.dgvBoletas.Columns["LIQUIDACION"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dgvBoletas.Columns["DCTO DE HUMEDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dgvBoletas.Columns["DCTO DE HUMEDAD"].DefaultCellStyle.Format = "C2";
                    this.dgvBoletas.Columns["FECHA_DE_ENTRADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dgvBoletas.Columns["FECHA_DE_ENTRADA"].DefaultCellStyle.Format = "d";
                    this.dgvBoletas.Columns["DCTO DE IMPUREZAS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    this.dgvBoletas.Columns["DCTO DE IMPUREZAS"].DefaultCellStyle.Format = "C2";        
                    this.txtCantidadBoletas.Text = dtBoletas.Rows.Count.ToString();
                    this.btnActualizarDatos_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR!!");
                }
            }
            else
            {
                MessageBox.Show("No se han podido consultar los datos de las Boletas", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }        
        }
        private String Addfiltros()
        {
            String filtros = String.Empty;
            try
            {
                filtros += "pesodesalida = 0";
                if (this.cmbBodegas.Enabled&&this.cmbBodegas.SelectedValue.ToString() != "0")
                {                   
                    filtros += " AND BODEGA='" + this.cmbBodegas.Text + "'";
                }
                if (this.cmbCiclos.Enabled&&this.cmbCiclos.SelectedValue.ToString() != "0")
                {                    
                    filtros += " and CICLO='" + this.cmbCiclos.Text + "'";
                }           
                
                if (this.txtTicket.Text != String.Empty)
                {
                    
                    filtros += " and Ticket='" + this.txtTicket.Text + "'";

                }
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message, "ERROR!");
            }
            return filtros;

        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {

            String mensaje = "";
            if (WSConnector.Instance.validaLaSession(out mensaje))
                this.UpdateLista();
            
        }

        private void btnActualizarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgvBoletas.Columns[15].Visible = false;
                this.dgvBoletas.Columns[16].Visible = false;
                this.dgvBoletas.Columns[17].Visible = false;
                this.dgvBoletas.Columns[18].Visible = false;
                this.dgvBoletas.Columns["PesoDeEntrada"].Visible = false;
                this.dgvBoletas.Columns["PesoDeSalida"].Visible = false;                    
                this.dgvBoletas.Columns["bodegaID"].Visible = false;
                this.txtCantidadBoletas.Text = dtBoletas.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR!!");
            }
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {

            if (WSConnector.Instance.SessionValida)
            {
                this.cmbBodegas.SelectedIndex = 0;
                this.cmbCiclos.SelectedIndex = 0;               
                this.UpdateLista();
            }
            this.txtCantidadBoletas.Text = string.Empty;
            this.txtTicket.Text = string.Empty; 
        }

        private void FormBoletasSinSalir_Load(object sender, EventArgs e)
        {
            this.UpdateCombos();
            this.UpdateLista();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            String mensaje = "";
            if (WSConnector.Instance.validaLaSession(out mensaje))
            {
                frmAddBoletas myForm = new frmAddBoletas(this.dgvBoletas.SelectedRows[0].Cells["boletaID"].Value.ToString());
                Principal.OpenFormInDock(myForm);

            }
            else
            {
                MessageBox.Show(mensaje, "ERROR!!");
                Login fr = new Login(1);
                fr.ShowDialog();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            frmAddBoletas myForm = new frmAddBoletas();
            Principal.OpenFormInDock(myForm);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            String mensaje = null;
            try
            {
                if (DialogResult.Yes == MessageBox.Show("REALMENTE QUIERE ELIMINAR LA BOLETA : \n" + this.dgvBoletas.SelectedCells[18].Value.ToString(), "ALERTA!!", MessageBoxButtons.YesNo))
                {

                    
                    
                        if (WSConnector.Instance.deleteBoleta(int.Parse(this.dgvBoletas.SelectedRows[0].Cells["boletaID"].Value.ToString()), out mensaje))
                        {
                            MessageBox.Show(mensaje, "EXITO!");
                            this.UpdateLista();
                        }
                        else
                        {
                            MessageBox.Show(mensaje, "ERROR!");
                        }
                    
                }
            }
            catch(Exception ex)
            {
                    MessageBox.Show(ex.Message, "ERROR!");
            }           
        }

        private void dgvBoletas_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            String mensaje = "";
            if (WSConnector.Instance.validaLaSession(out mensaje))
            {
                frmAddBoletas myForm = new frmAddBoletas(this.dgvBoletas.SelectedRows[0].Cells["boletaID"].Value.ToString());
                Principal.OpenFormInDock(myForm);

            }
            else
            {
                MessageBox.Show(mensaje, "ERROR!!");
                Login fr = new Login(1);
                fr.ShowDialog();
            }
        }
    }
}


