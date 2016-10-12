using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.Sql;
using System.Runtime.InteropServices;
using WeifenLuo.WinFormsUI.Docking;
using WeifenLuo.WinFormsUI;

namespace BasculaGaribay
{
    public partial class frmAddProductor : DockContent
    {
        private DataTable dtRegimenes = new DataTable();
        private DataTable dtEstadoCiviles = new DataTable();
        private DataTable dtEstados = new DataTable();
        private String tipodeForm;
        private String[] datos;
        private String idtomodify;
        

        public frmAddProductor()
        {
            InitializeComponent();
            this.tipodeForm = "AGREGAR";

        }

        public frmAddProductor(String idtoModify)
        {
            InitializeComponent();
            this.tipodeForm = "MODIFICAR";
            
            this.idtomodify = idtoModify;
            this.datos = new String[24];
            
        }

        private bool consultaproductor()
        {
            
            try
            {
                return WSConnector.Instance.Getproductor(idtomodify, out datos);
            }catch(Exception ex){

                MessageBox.Show(ex.Message, "ERROR!!");
                return false;
            }
        
        }

        private void llenaCampos()
        {
            if(consultaproductor())
            {
                this.txtPaterno.Text = datos[1].ToString();
                this.txtMaterno.Text = datos[2].ToString();
                this.txtNombre.Text = datos[3].ToString();

                this.Text = (this.txtPaterno.Text + " " + this.txtMaterno.Text + " " + this.txtNombre.Text).Trim();

                this.dtpFechaNacimiento.Text = datos[4].ToString();
                this.txtIfe.Text = datos[5].ToString();

                this.txtcurp.Text = datos[6].ToString();
                this.txtDomicilio.Text = datos[7].ToString();
                this.txtPoblacion.Text = datos[8].ToString();
                this.txtMunicipio.Text = datos[9].ToString();
                
                this.cmbEstado.SelectedValue = datos[10];
                this.txtCp.Text = datos[11].ToString();
                this.txtRFC.Text = datos[12].ToString();
                if (datos[13].ToString() == "2") 
                { 
                    this.rdFemenino.Checked = false;
                    this.rbMasculino.Checked = true;
                }
                else
                {
                    this.rdFemenino.Checked = true;
                    this.rbMasculino.Checked = false;
                }

                this.txtTelefono.Text=datos[14].ToString();
                this.txtTtrabajo.Text = datos[15].ToString();
                this.txtCel.Text = datos[16].ToString();
                this.txtFax.Text = datos[17].ToString();
                this.txtEmail.Text = datos[18].ToString();
                this.cmbEstadoCivil.SelectedValue = int.Parse(datos[19]);
                this.cmbRegimen.SelectedValue = int.Parse(datos[20]);

                
                this.txtCodigoboletafile.Text = datos[21].ToString();
            }
            else
            {
                MessageBox.Show("NO SE PUDO CARGAR LOS DATOS DEL PRODUCTOR \n SELECCIONADO", "ERROR");
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void txtCp_MouseClick(object sender, MouseEventArgs e)
        {
            
            SendKeys.Send("{HOME}");

        }

        
        private void txtTelefono_MouseClick(object sender, MouseEventArgs e)
        {
            SendKeys.Send("{HOME}");
        }

        private void txtTtrabajo_MouseClick(object sender, MouseEventArgs e)
        {
            SendKeys.Send("{HOME}");
        }

        private void maskedTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            SendKeys.Send("{HOME}");
        }

        private void maskedTextBox2_MouseClick(object sender, MouseEventArgs e)
        {
            SendKeys.Send("{HOME}");
        }
        

        private void UpdateCombos()
        {
            try
            {
                if (WSConnector.Instance.GetAllRegimenes(out dtRegimenes))
                {


                    this.cmbRegimen.DataSource = dtRegimenes;
                    this.cmbRegimen.ValueMember = "regimenID";
                    this.cmbRegimen.DisplayMember = "Regimen";
                    this.cmbRegimen.SelectedIndex = 0;

                    if (WSConnector.Instance.GetAllEstadosCiviles(out dtEstadoCiviles))
                    {
                        this.cmbEstadoCivil.DataSource = dtEstadoCiviles;
                        this.cmbEstadoCivil.ValueMember = "estadoCivilID";
                        this.cmbEstadoCivil.DisplayMember = "EstadoCivil";
                        this.cmbEstadoCivil.SelectedIndex = 0;
                        
                        if (WSConnector.Instance.GetAllEstados(out dtEstados))
                        {
                            this.cmbEstado.DataSource = dtEstados;
                            this.cmbEstado.ValueMember = "estadoID";
                            this.cmbEstado.DisplayMember = "estado";
                            this.cmbEstado.SelectedIndex = 0;
                        }
                        else
                        {

                            MessageBox.Show("No se pudieron consultar los datos de los Estados", "ERROR!");
                            this.cmbEstado.Enabled = false;
                          

                        }
                    }
                    else
                    {

                        MessageBox.Show("No se pudieron consultar los datos de los Estados Civiles", "ERROR!");
                        this.cmbEstado.Enabled = false;
                        this.cmbEstadoCivil.Enabled = false;
                        
                    }

                }else{

                    MessageBox.Show("No se pudieron consultar los datos de los Regimenes", "ERROR!");
                    this.cmbEstado.Enabled = false;
                    this.cmbEstadoCivil.Enabled = false;
                    this.cmbRegimen.Enabled = false;
                }

                

                

            }catch(Exception ex){
                MessageBox.Show(ex.Message, "ERROR!!");
            }
        }
        
        //Este Ejemplo puede que funcione con otras teclas, si quieres puedes probar.

        //Declaración de constantes  
        private const int KEYEVENTF_EXTENDEDKEY = 0x1;
        private const int KEYEVENTF_KEYUP = 0x2;

        //Declaracion Api 
        [DllImport("user32")]
        private static extern void keybd_event(Keys bVk, int bScan, int dwFlags, int dwExtraInfo);

        private void frmAddProductor_Load(object sender, EventArgs e)
        {
            
            try
            {
                if (!System.Console.CapsLock)
                {
                    keybd_event(Keys.CapsLock, 0x45, KEYEVENTF_EXTENDEDKEY | 0, 0);
                    keybd_event(Keys.CapsLock, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
                }
                this.UpdateCombos();
                if (tipodeForm == "MODIFICAR")
                {
                    this.llenaCampos();
                    this.btnModificar.Visible = true;
                    this.btnAgregar.Visible = false;
                    this.chkAddAsClienteDeVenta.Visible = this.chkAddAsProveedorGanado.Visible = false;
                }
                else
                {
                    this.btnModificar.Visible = false;
                    this.btnAgregar.Visible = true;
                }
                this.cmbEstado.SelectedValue = "14";
            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(ex);
                MessageBox.Show(ex.Message, "ERROR!!");
            }
            if(!WSConnector.Instance.SessionValida)
            {
                this.btnAgregar.Enabled = false;
            }
            
        }

        public static bool validarEmail(string email)
        {
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                { return true; }
                else
                { return false; }
            }
            else
            { return false; }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Cache.Instance.InvalidChaceEntry(CacheTables.PRODUCTORES);
            Cache.Instance.InvalidChaceEntry(CacheTables.PRODUCTORESFORCMB);

            String mensajedeCamposinvalidos = camposValidos();
            
            string exc = "";
            try
            {
                if (mensajedeCamposinvalidos == "")
                    {
                        if (WSConnector.Instance.InsertProductor(arregloDeDatos(), out exc))
                        {
                            MessageBox.Show("Se ha agregado el Productor: \n" + this.txtNombre.Text.ToUpper() + " " + this.txtPaterno.Text.ToUpper() + " " + this.txtMaterno.Text.ToUpper() + " \nsatisfactoriamente", "ÉXITO");

                            DataTable dtProductores = new DataTable();
                            DataTable dt = new DataTable();
                            WSConnector.Instance.GetAllProductores(out dtProductores);
                            WSConnector.Instance.GetAllProductoresforCmb(out dt);

                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show(mensajedeCamposinvalidos, "ERRORES DEL FORMULARIO");
                     }
            } catch(Exception ex){
                MessageBox.Show(ex.Message, "ERROR!!");

            }
        }


        private String camposValidos()
        {
            String mensajeError = "";

            if(this.txtPaterno.Text=="")
            {
                mensajeError = " El campo A. Paterno es Necesario\n";

            }
            if (this.txtMaterno.Text == "")
            {
                mensajeError = mensajeError + " El campo A. Materno es Necesario\n";

            }
            if(this.txtNombre.Text=="")
            {
                mensajeError = mensajeError + " El campo Nombre(s) es Necesario\n";
            }
            if(this.txtEmail.Text!=""&&!validarEmail(this.txtEmail.Text))
            {
                mensajeError = mensajeError + " El campo E-Mail no es Válido\n";
            }

            return mensajeError;
            }

       private String[] arregloDeDatos()
       {
           String[] datos = new String[25];

           try
           {
               
                datos[0] = this.txtPaterno.Text.ToUpper();
                datos[1] = this.txtMaterno.Text.ToUpper();
                datos[2] = this.txtNombre.Text.ToUpper();
                datos[3] = this.dtpFechaNacimiento.Value.ToString();
                datos[4] = this.txtIfe.Text.ToUpper();
                datos[5] = this.txtcurp.Text.ToUpper();
                datos[6] = this.txtDomicilio.Text.ToUpper();
                datos[7] = this.txtPoblacion.Text.ToUpper();
                datos[8] = this.txtMunicipio.Text.ToUpper();
                datos[9] = this.cmbEstado.SelectedValue.ToString().ToUpper();
                datos[10] = this.txtCp.Text;
                datos[11] = this.txtRFC.Text.ToUpper();
                if (rdFemenino.Checked)
                   datos[12] = "1";
                else
                   datos[12] = "2";
                datos[13] = this.txtTelefono.Text;
                datos[14] = this.txtTtrabajo.Text;
                datos[15] = this.txtCel.Text;
                datos[16] = this.txtFax.Text;
                datos[17] = this.txtEmail.Text.ToUpper();
                datos[18] = this.cmbEstadoCivil.SelectedValue.ToString();
                datos[19] = this.cmbRegimen.SelectedValue.ToString();
                datos[20] = this.txtCodigoboletafile.Text;
                datos[21] = this.txtColonia.Text.ToUpper();
                datos[22] = this.txtConyugue.Text.ToUpper();
                datos[23] = this.chkAddAsClienteDeVenta.Checked.ToString();
                datos[24] = this.chkAddAsProveedorGanado.Checked.ToString();
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message, "ERROR!");
               return null;

           }
           return datos;

       }

       private void btnModificar_Click(object sender, EventArgs e)
       {
           String mensajedeCamposInvalidos = camposValidos();
           string exc = "";
           try
           {
               
                   if (mensajedeCamposInvalidos == "")
                   {
                       if (WSConnector.Instance.ModifyProductor(arregloDeDatos(), idtomodify, out exc))
                       {
                           MessageBox.Show("Se ha Modificado el Productor \n" + this.txtNombre.Text.ToUpper() + " " + this.txtPaterno.Text.ToUpper() + " " + this.txtMaterno.Text.ToUpper() + " \nsatisfactoriamente", "ÉXITO");
                           this.Close();
                       }
                  
                   }
                   else
                   {

                       MessageBox.Show(mensajedeCamposInvalidos, "ERRORES DEL FORMULARIO");

                   }
               //}
               //else
               //{
               //    MessageBox.Show(mensajeError, "ERROR!!");
               //    Login fr = new Login(1);
               //    fr.ShowDialog();
               //}
           }catch(Exception ex){
               MessageBox.Show(ex.Message, "Error!!");
           }
       }

       private void lblEstado_Click(object sender, EventArgs e)
       {

       }

       private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
       {

       }

      

      

      
    }
}


