using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using WeifenLuo.WinFormsUI.Docking;


namespace BasculaGaribay
{
    public partial class frmListBoletas : DockContent
    {
        public frmListBoletas()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAddBoletas myForm = new frmAddBoletas();
            Principal.OpenFormInDock(myForm);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            String mensaje="";
            if(WSConnector.Instance.validaLaSession(out mensaje)){
            frmAddBoletas myForm = new frmAddBoletas(this.dgvBoletas.SelectedRows[0].Cells["boletaID"].Value.ToString());
            Principal.OpenFormInDock(myForm);
            
            }else{
            MessageBox.Show(mensaje, "ERROR!!");
            Login fr = new Login(1);
            fr.ShowDialog();
            }
        }


        private void UpdateCombos()
        {
            try
            {
                if(WSConnector.Instance.GetAllCiclos(out dtCiclos)){
                this.cmbCiclos.DataSource = dtCiclos;
                this.cmbCiclos.ValueMember = "cicloID";
                this.cmbCiclos.DisplayMember = "Ciclo";

                if (WSConnector.Instance.GetAllProductosforCmb(out dtProductos))
                {
                    this.cmbProductos.DataSource = dtProductos;
                    this.cmbProductos.ValueMember = "productoID";
                    this.cmbProductos.DisplayMember = "Nombre";
                    this.cmbProductos.SelectedIndex = 0;
                    Proveedores []Provs = WSConnector.Instance.GetAllProveedores();
                    Proveedores p = new Proveedores();
                    p.ProveedorID = -1;
                    p.Nombre = "TODOS LOS PROVEEDORES";
                    Proveedores [] proveedores = new Proveedores [Provs.Length+1];
                    proveedores.SetValue(p,0);
                    Provs.CopyTo(proveedores, 1);
                    this.proveedoresBindingSource.DataSource = proveedores;
                    this.cmbProveedores.SelectedValue = -1;

                if (WSConnector.Instance.GetAllProductores(out dtProductores))
                {

                    this.cmbProductores.DataSource = dtProductores;
                    this.cmbProductores.DisplayMember = "Nombre";
                    this.cmbProductores.ValueMember = "productorID";

                    this.cmbProductores.SelectedIndex = 0;

                    if (WSConnector.Instance.GetAllBodegas(out dtBodegas))
                    {
                        this.cmbBodegas.DataSource = dtBodegas;
                        this.cmbBodegas.ValueMember = "bodegaID";
                        this.cmbBodegas.DisplayMember = "bodega";

                        this.cmbBodegas.SelectedIndex = 0;
                        this.cmbTipodeBoleta.SelectedIndex = 0;
                        //this.cmbBodegas.Enabled = false;

                        if (WSConnector.Instance.GetAllClientesVentas(out dtClientes))
                        {
                            this.cmbClientesVentas.DataSource = dtClientes;
                            this.cmbClientesVentas.ValueMember = "clienteventaID";
                            this.cmbClientesVentas.DisplayMember = "nombre";
                            this.cmbClientesVentas.SelectedIndex = 0;
                            this.uploadLista();
                        }

                        

                        
                    }
                    else
                    {
                        MessageBox.Show("No se han podido consultar los datos de las bodegas", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);


                        this.cmbBodegas.Enabled = false;
                        

                        this.cmbTipodeBoleta.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("No se han podido consultar los datos de los productores", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.cmbBodegas.Enabled = false;
                    this.cmbProductores.Enabled = false;
                    
                    this.cmbTipodeBoleta.Enabled = false;
                    
                }

                }
                else
                {
                    MessageBox.Show("No se han podido consultar los datos de los Productos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.cmbBodegas.Enabled = false;
                    this.cmbProductores.Enabled = false;
                    this.cmbProductos.Enabled = false;
                    this.cmbTipodeBoleta.Enabled = false;
                }

                }
                else
                {
                    MessageBox.Show("No se han podido consultar los datos de los Ciclos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.cmbBodegas.Enabled = false;
                    this.cmbCiclos.Enabled = false;
                    this.cmbProductores.Enabled = false;
                    this.cmbProductos.Enabled = false;
                    this.cmbTipodeBoleta.Enabled = false;

                }


                
                //WSConnector.Instance.GetProductores("productorID, LTRIM(apaterno + ' ' + amaterno + ' ' + nombre) as Nombre", "", "", out dtProductores);
                DataTable dtProvGanado;
                if (WSConnector.Instance.GetProvDeGanado(out dtProvGanado))
                {
                    dtProvGanado.DefaultView.Sort = "ganProveedorID ASC";
                    this.cmbProveedorGanado.DataSource = dtProvGanado.DefaultView;
                    this.cmbProveedorGanado.ValueMember = "ganProveedorID";
                    this.cmbProveedorGanado.DisplayMember = "Nombre";
                    this.cmbProveedorGanado.SelectedValue = 0;
                }
                
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR!!");
            }
            
        }
        private void uploadLista()
        {
            if (WSConnector.Instance.GetBoletas(addfiltros(),out dtBoletas))
            {
                try
                {
                    //dtBoletas.DefaultView.RowFilter = "BODEGA='SUCURSAL MARGARITAS'";
                    this.dgvBoletas.DataSource = dtBoletas;
                    this.dgvBoletas.Columns[16].Visible = false;
                    this.dgvBoletas.Columns[17].Visible = false;
                    this.dgvBoletas.Columns[18].Visible = false;
                    this.dgvBoletas.Columns[19].Visible = false;
                    this.dgvBoletas.Columns["PesoDeEntrada"].Visible = false;
                    this.dgvBoletas.Columns["PesoDeSalida"].Visible= false;
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
        private void uploadListaFiltros()
        {
            try
            {
                uploadLista();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR!!");
            }

        }
        private String addfiltros()
        {
            String filtros = "";
            try
            {
                Boolean masdeuno = false;
                if (this.cmbBodegas.Enabled&&this.cmbBodegas.SelectedValue.ToString() != "0")
                {
                    masdeuno = true;
                    filtros += "BODEGA='" + this.cmbBodegas.Text + "'";
                }
                if (this.rdbProductor.Checked&&this.cmbProductores.Enabled&&this.cmbProductores.SelectedValue.ToString() != "0")
                {
                    if (masdeuno)
                        filtros += " and ";
                    masdeuno = true;
                    filtros += "PRODUCTORID='" + this.cmbProductores.SelectedValue.ToString() + "'";
                }
                if (this.rdbProveedores.Checked &&
                    this.cmbProveedores.SelectedValue != null &&
                    this.cmbProveedores.SelectedValue.ToString().Trim().Length > 0)
                {
                    if (masdeuno)
                        filtros += " and ";
                    masdeuno = true;
                    if (this.cmbProveedores.SelectedItem.GetType() == typeof(Proveedores)&&
                        ((Proveedores)this.cmbProveedores.SelectedItem).ProveedorID == -1)
                    {
                        filtros += " PROVEEDORID > -1 ";
                    }
                    else
                    {
                        filtros += " PROVEEDORID = " + ((Proveedores)this.cmbProveedores.SelectedItem).ProveedorID;
                    }
                }
                if (this.cmbCiclos.Enabled&&this.cmbCiclos.SelectedValue.ToString() != "0")
                {

                    if (masdeuno)
                        filtros += " and ";
                    masdeuno = true;
                    filtros += "CICLO='" + this.cmbCiclos.Text + "'";
                }
                if (this.cmbTipodeBoleta.SelectedIndex >0)
                {
                    if (masdeuno)
                        filtros += " and ";
                    masdeuno = true;
                    if (this.cmbTipodeBoleta.Text.ToString() == "ENTRADA")
                    {

                        filtros += "PESO_NETO_DE_ENTRADA>'0'";
                    }
                    else
                    {
                        filtros += "PESO_NETO_DE_SALIDA>'0'";
                    }
                }
                
                if (this.cmbProductos.Enabled&&this.cmbProductos.SelectedValue.ToString() != "0")
                {
                    if (masdeuno)
                        filtros += " and ";
                    masdeuno = true;
                    filtros += "DESCRIPCION='" + this.cmbProductos.Text + "'";
                   
                }
                if (this.rdbCliente.Checked &&
                    this.cmbClientesVentas.Enabled)
                {
                    if (masdeuno)
                        filtros += " and ";
                    masdeuno = true;
                    if (this.cmbClientesVentas.SelectedValue.ToString() != "0")
                    {
                        filtros += "clienteBoletaID='" + this.cmbClientesVentas.SelectedValue.ToString() + "'";
                    }
                    else
                    {
                        filtros += "clienteBoletaID > 0";
                    }
                }

                if (this.rdbProveedorGanado.Checked && this.cmbProveedorGanado.SelectedValue.ToString() != "0")
                {
                    if (masdeuno)
                        filtros += " and ";
                    masdeuno = true;
                    filtros += "ganProveedorID=" + this.cmbProveedorGanado.SelectedValue.ToString() + " ";

                }

                if (this.chkbFiltrarxFecha.Checked)
                {
                    if (masdeuno)
                        filtros += " and ";
                    masdeuno = true;

                    filtros += "FECHA_DE_ENTRADA<='" + this.DtpkfechaFin.Text + "' and ";
                    filtros += "FECHA_DE_ENTRADA>='" + this.dtpkFechaInicio.Text + "'";
                }

                if (this.txtBoleta.Text != "")
                {
                    if (masdeuno)
                        filtros += " and ";
                    masdeuno = true;
                    filtros += "No_BOLETA='" + this.txtBoleta.Text + "'";

                }
                if (this.txtTicket.Text != "")
                {
                    if (masdeuno)
                        filtros += " and ";
                    masdeuno = true;
                    filtros += "Ticket='" + this.txtTicket.Text + "'";

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
            if(WSConnector.Instance.validaLaSession(out mensaje))
                this.uploadListaFiltros();
            
        }

        private DataTable dtCiclos = new DataTable();
        private DataTable dtClientes = new DataTable();
        private DataTable dtProductos = new DataTable();
        private DataTable dtProductores = new DataTable();
        private DataTable dtBoletas = new DataTable();
        private DataTable dtBodegas = new DataTable();


        private void frmListBoletas_Load(object sender, EventArgs e)
        {
            this.btnModificar.Visible = false;
            this.btnEliminar.Visible = false;

            this.UpdateCombos();
            if(this.rdbProductor.Checked)
            {
                this.cmbProductores.Enabled = true;
                this.cmbClientesVentas.Enabled = false;
            }
            else
            {
                this.cmbProductores.Enabled = false;
                this.cmbClientesVentas.Enabled = true;
            }
            this.DtpkfechaFin.Enabled = false;
            this.dtpkFechaInicio.Enabled = false;
        }

        private void chkbFiltrarxFecha_CheckedChanged(object sender, EventArgs e)
        {
            if(this.chkbFiltrarxFecha.Checked){
                this.dtpkFechaInicio.Enabled = true;
                this.DtpkfechaFin.Enabled = true;


            }
            else{
                this.dtpkFechaInicio.Enabled = false;
                this.DtpkfechaFin.Enabled = false;


            }
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {

            if (WSConnector.Instance.SessionValida)
            {
                this.cmbBodegas.SelectedIndex = 0;
                this.cmbProductores.SelectedIndex = 0;
                this.cmbProductos.SelectedIndex = 0;
                this.cmbCiclos.SelectedIndex = 0;
                this.cmbTipodeBoleta.SelectedIndex = 0;
                this.cmbClientesVentas.SelectedIndex = 0;
                this.uploadLista();
            }
            this.txtBoleta.Text = "";
            this.txtCantidadBoletas.Text = "";
            this.txtTicket.Text = "";
            this.chkbFiltrarxFecha.Checked = false;
            
        }

        private void dgvBoletas_SelectionChanged(object sender, EventArgs e)
        {
            this.btnModificar.Visible = false;
            this.btnEliminar.Visible = false;

            if (this.dgvBoletas.SelectedRows.Count == 1)
            {
                this.btnModificar.Visible = true;
                this.btnEliminar.Visible = true;
                
            }
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
                            this.uploadLista();
                        }
                        else
                        {
                            MessageBox.Show(mensaje, "ERROR!");
                        }
                    
                }
            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(ex);
                MessageBox.Show(ex.Message, "ERROR!");
            }

           }

        private void btnActualizarDatos_Click(object sender, EventArgs e)
        {
           // String mensaje="";
            try
            {
                Cache.Instance.InvalidChaceEntry(CacheTables.PRODUCTORES);
                Cache.Instance.InvalidChaceEntry(CacheTables.PRODUCTORESFORCMB);
                if(WSConnector.Instance.GetBoletas(this.addfiltros(),out dtBoletas))
                {
                
                    this.dgvBoletas.DataSource = dtBoletas;
                    this.dgvBoletas.Columns[15].Visible = false;
                    this.dgvBoletas.Columns[16].Visible = false;
                    this.dgvBoletas.Columns[17].Visible = false;
                    this.dgvBoletas.Columns[18].Visible = false;
                    this.dgvBoletas.Columns["PesoDeEntrada"].Visible = false;
                    this.dgvBoletas.Columns["PesoDeSalida"].Visible = false;
                   


                    this.dgvBoletas.Columns["bodegaID"].Visible = false;

                    this.txtCantidadBoletas.Text = dtBoletas.Rows.Count.ToString();
                }
                
            }catch(Exception ex){
                MessageBox.Show(ex.Message, "ERROR!!");
            }
        }

        private void rdbCliente_CheckedChanged(object sender, EventArgs e)
        {
            this.cmbProductores.Enabled = this.rdbProductor.Checked;
            this.cmbClientesVentas.Enabled = this.rdbCliente.Checked;
            this.cmbProveedorGanado.Enabled = this.rdbProveedorGanado.Checked;
        }
    }
            
        
}

