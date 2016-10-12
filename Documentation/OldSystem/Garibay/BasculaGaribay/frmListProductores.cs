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
    public partial class frmListProductores : DockContent
    {
        
        private DataTable dtProductores;

        public frmListProductores()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAddProductor myform = new frmAddProductor();
            Principal.OpenFormInDock(myform);            
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
           frmAddProductor myform = new frmAddProductor(this.dgvProductores.SelectedRows[0].Cells["ID"].Value.ToString());
           Principal.OpenFormInDock(myform);
        }

        private void btnActualiza_Click(object sender, EventArgs e)
        {
            Cache.Instance.InvalidChaceEntry(CacheTables.PRODUCTORES);
            Cache.Instance.InvalidChaceEntry(CacheTables.PRODUCTORESFORCMB);
            this.UpdateList();
        }

        private void UpdateList()
        {
            try
            {
                dtProductores = new DataTable();
                String queryestatico = " Productores.productorID as ID, Productores.apaterno as 'A. PATERNO', Productores.amaterno AS 'A. MATERNO', Productores.nombre AS NOMBRE, Productores.fechanacimiento AS 'FECHA DE NACIMIENTO'";
                String camposSelec = ckboxSelec();
                
                if (WSConnector.Instance.GetProductores(queryestatico + camposSelec,  out dtProductores))
                {
                    this.dgvProductores.DataSource = null;

                    this.dgvProductores.DataSource = dtProductores;
                    if (this.chkbproductorid.Checked)
                    {
                        this.dgvProductores.Columns[0].Visible = true;
                    }
                    else
                    {
                        this.dgvProductores.Columns[0].Visible = false;

                    }


                }
                else
                {
                    MessageBox.Show("No se han podido consultar los datos de los productores", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }catch(Exception ex){
                MessageBox.Show(ex.Message, "ERROR!!");
            }

        }

        private String ckboxSelec(){
            
            String camposSeleccionados = "";
            foreach (CheckBox chek in this.pnlFiltros.Controls)
            {
                if(chek.Checked){
                switch(chek.Name)
                {
                    case "chkbife":
                        camposSeleccionados = ", Productores.IFE " + camposSeleccionados;
                        break;
                    case "chkbCurp":
                        camposSeleccionados = ", Productores.CURP " + camposSeleccionados;
                        break;
                    case "chkbDomicilio":
                        camposSeleccionados = ", Productores.domicilio " + camposSeleccionados;
                        break;
                    case "chkPoblacion":
                        camposSeleccionados = ", Productores.Poblacion " + camposSeleccionados;
                        break;
                    case "chkbMun":
                        camposSeleccionados = ", Productores.municipio " + camposSeleccionados;
                        break;
                    case "chkbEstado":
                        camposSeleccionados = ", Estados.estado " + camposSeleccionados;
                        break;
                    case "chkbCP":
                        camposSeleccionados = ", Productores.CP " + camposSeleccionados;
                        break;
                    case "chkbRFC":
                        camposSeleccionados = ", Productores.RFC " + camposSeleccionados;
                        break;
                    case "chkbSexo":
                        camposSeleccionados = ", Sexo.sexo " + camposSeleccionados;
                        break;
                    case "chkbTel":
                        camposSeleccionados = ", Productores.telefono " + camposSeleccionados;
                        break;
                    case "chkbTelTabajo":
                        camposSeleccionados = ", Productores.telefonotrabajo As Teléfono_de_Trabajo" + camposSeleccionados;
                        break;
                    case "chkbCel":
                        camposSeleccionados = ", Productores.celular " + camposSeleccionados;
                        break;
                    case "chkBFax":
                        camposSeleccionados = ", Productores.fax " + camposSeleccionados;
                        break;
                    case "chkbEmail":
                        camposSeleccionados = ", Productores.email " + camposSeleccionados;
                        break;
                    case "chkbEcivil":
                        camposSeleccionados = ", EstadosCiviles.EstadoCivil  as Estado_Civil" + camposSeleccionados;
                        break;
                    case "chkbRegimen":
                        camposSeleccionados = ", Regimenes.Regimen" + camposSeleccionados;
                        break;
                    case "chkbFechaI":
                        camposSeleccionados = ", Productores.storeTS as Fecha_de_Ingreso" + camposSeleccionados;
                        break;
                    case "chkbFM":
                        camposSeleccionados = ", Productores.updateTS as Última_Modificación" + camposSeleccionados;
                        break;
                    case "chkConyugue":
                        camposSeleccionados = ", Productores.conyugue as Conyugue" + camposSeleccionados;
                        break;
                    case "chkColonia":
                        camposSeleccionados = ", Productores.colonia as Colonia" + camposSeleccionados;
                        break;
                }
                }
            }
            return camposSeleccionados;
        }

        private void btnSelTodas_Click(object sender, EventArgs e)
        {
           foreach (CheckBox chek in this.pnlFiltros.Controls)
            {
                chek.Checked = true;
            }
        }

        private void btnQuitSel_Click(object sender, EventArgs e)
        {
            foreach (CheckBox chek in this.pnlFiltros.Controls)
            {
                chek.Checked = false;
            }
        }

        private void dgvProductores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvProductores_SelectionChanged(object sender, EventArgs e)
        {
            this.btnModificar.Visible = false;
            this.btnEliminar.Visible = false;

            if(this.dgvProductores.SelectedRows.Count==1){
            this.btnModificar.Visible = true;
            this.btnEliminar.Visible = true;
            }
            
        }

        private void frmListProductores_Load(object sender, EventArgs e)
        {
            this.UpdateList();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
           String mensaje=null;
            try{
           if (System.Windows.Forms.DialogResult.Yes == MessageBox.Show("REALMENTE QUIERE ELIMINAR AL PRODUCTOR : \n" + this.dgvProductores.SelectedCells[1].Value.ToString() + " " + this.dgvProductores.SelectedCells[2].Value.ToString() + " " + this.dgvProductores.SelectedCells[3].Value.ToString() + "", "ALERTA!!", MessageBoxButtons.YesNo))
           {
               //if (WSConnector.Instance.validaLaSession(out mensaje))
               //{
                   if (WSConnector.Instance.deleteProductor(int.Parse(this.dgvProductores.SelectedRows[0].Cells["ID"].Value.ToString()), this.dgvProductores.SelectedCells[1].Value.ToString() + " " + this.dgvProductores.SelectedCells[2].Value.ToString() + " " + this.dgvProductores.SelectedCells[3].Value.ToString(), out mensaje))
                   {
                       MessageBox.Show(mensaje, "EXITO!");
                       UpdateList();
                   }
                   else
                   {
                       MessageBox.Show(mensaje, "ERROR!");
                   }

             
           }
                }catch(Exception ex){
                    MessageBox.Show(ex.Message, "ERROR!!");
                }
        } 
    }
}
