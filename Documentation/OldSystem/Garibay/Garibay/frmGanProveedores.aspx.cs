using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

namespace Garibay
{
    public partial class frmGanProveedores : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack){
                this.cmbEstado.DataBind();
                this.Agregando(false, true);
                this.cmbEstado.SelectedIndex = 14;
                 try
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.GANPROVEEDORES, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "EL USUARIO VISITÓ LA PÁGINA PROVEEDORES PARA LA VENTA DE GANADO");
                }
                catch (Exception exception) {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                
                }
            }
            this.panelmensaje.Visible = false;
        }

        protected void btnAgregarDeForm_Click(object sender, EventArgs e)
        {
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            string sql = "insert into gan_Proveedores (nombre, direccion, ciudad, estadoID, userID, RFC) values(@nombre, @direccion,@ciudad, @estadoID, @userID, @RFC); ";
            SqlCommand cmd  = new SqlCommand(sql,conGaribay);
            conGaribay.Open();
            try{

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = this.txtNombre.Text;
                cmd.Parameters.Add("@direccion", SqlDbType.NVarChar).Value = this.txtDireccion.Text;
                cmd.Parameters.Add("@ciudad", SqlDbType.NVarChar).Value = this.txtCiudad.Text;
                cmd.Parameters.Add("@estadoID", SqlDbType.Int).Value = int.Parse(this.cmbEstado.SelectedValue);
                cmd.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                cmd.Parameters.Add("@RFC", SqlDbType.NVarChar).Value = this.txtRfc.Text;
                int numregistros = cmd.ExecuteNonQuery();
                if(numregistros!=1){
                    throw new Exception("LA BASE DE DATOS REGRESO QUE SE ALTERARON: " + numregistros.ToString() + " REGISTROS AL INSERTAR UN PROVEEDOR PARA VENTA DE GANADO");
                }
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = "EL PROVEEDOR PARA LA VENTA DE GANADO: " + this.txtNombre.Text.ToUpper() + " HA SIDO AGREGADO EXITOSAMENTE";
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                this.Agregando(false, true);
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.GANPROVEEDORES, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("SE AGREGÓ EL PROVEEDOR PARA LA VENTA DE GANADO: " + this.txtNombre.Text.ToUpper()));
                this.limpiacampos();
                
            }
            catch(Exception ex){
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "EL PROVEEDOR PARA LA VENTA DE GANADO " + this.txtNombre.Text.ToUpper() + " NO HA PODIDO SER AGREGADO" ;
                this.lblMensajeException.Text = ex.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ex.Message, this.Request.Url.ToString());


            }
            finally{
                conGaribay.Close();
            }


        }
        protected void limpiacampos(){
            this.txtNombre.Text = "";
            this.txtRfc.Text = "";
            this.txtDireccion.Text = "";
            this.txtCiudad.Text = "";
            this.cmbEstado.SelectedIndex = 14;
        }

        protected void Agregando(Boolean muestrapanelgregar, Boolean semuestrabotonagregardeabajo)
        {
            this.panelagregarUsuario.Visible = muestrapanelgregar;
            this.btnAgregarDeLista.Visible = !muestrapanelgregar;
            this.btnModificarDeLista.Visible = !muestrapanelgregar;
            this.btnEliminar.Visible = !muestrapanelgregar;
            if (this.gridProvGan.SelectedIndex == -1)
            {
                this.btnModificarDeLista.Visible = false;
                this.btnEliminar.Visible = false;
            }

            this.btnAgregarDeForm.Visible = semuestrabotonagregardeabajo;
            this.btnModificarDeForm.Visible = !semuestrabotonagregardeabajo;
            this.gridProvGan.Columns[0].Visible = !muestrapanelgregar;

        }

        protected void btnAgregarDeLista_Click(object sender, EventArgs e)
        {
            this.Agregando(true, true);
            this.lblProveedores.Text = "AGREGAR NUEVO PROVEEDOR PARA VENTA DE GANADO";
            this.limpiacampos();
        }

        protected void gridProvGan_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            if (this.gridProvGan.SelectedDataKey["ganProveedorID"] != null)
            {
                if (panelagregarUsuario.Visible == false)
                {
                    this.btnModificarDeLista.Visible = true;
                    this.btnEliminar.Visible = true;
                }


                this.txtNombre.Text = this.gridProvGan.SelectedDataKey["Nombre"].ToString();
                this.txtRfc.Text = this.gridProvGan.SelectedDataKey["RFC"].ToString();
               // this.cmbLevelSecurity.SelectedValue = this.gridUsers.SelectedDataKey["securitylevelID"].ToString();
                this.txtCiudad.Text= this.gridProvGan.SelectedDataKey["ciudad"].ToString();
                this.cmbEstado.SelectedValue = this.gridProvGan.SelectedDataKey["estadoID"].ToString();
                
                string msgDel = "return confirm('¿Realmente desea eliminar el proveedor para la venta de ganado: ";
                msgDel += this.gridProvGan.SelectedDataKey["Nombre"].ToString().ToUpper();
                msgDel += "?')";
                btnEliminar.Attributes.Add("onclick", msgDel);
                this.txtDireccion.Text = this.gridProvGan.SelectedDataKey["direccion"].ToString();
               // this.txtTelefono.Text = this.gridProvGan.SelectedDataKey["Telefono"].ToString();

            }
        }

        protected void btnModificarDeForm_Click(object sender, EventArgs e)
        {
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            string sql = "update gan_Proveedores set nombre = @nombre,  direccion=@direccion, ciudad=@ciudad, estadoID=@estadoID, userID= @userID, RFC =@RFC ";
            sql += "where ganProveedorID = @ganProveedorID";
           
            SqlCommand cmd = new SqlCommand(sql, conGaribay);
            conGaribay.Open();
            try
            {

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = this.txtNombre.Text;
                cmd.Parameters.Add("@direccion", SqlDbType.NVarChar).Value = this.txtDireccion.Text;
                cmd.Parameters.Add("@ciudad", SqlDbType.NVarChar).Value = this.txtCiudad.Text;
                cmd.Parameters.Add("@estadoID", SqlDbType.Int).Value = int.Parse(this.cmbEstado.SelectedValue);
                cmd.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                cmd.Parameters.Add("@ganProveedorID",SqlDbType.Int).Value = int.Parse(this.gridProvGan.SelectedDataKey["ganProveedorID"].ToString());
                cmd.Parameters.Add("@RFC", SqlDbType.NVarChar).Value = this.txtRfc.Text;
                int numregistros = cmd.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception("LA BASE DE DATOS REGRESO QUE SE ALTERARON: " + numregistros.ToString() + " REGISTROS AL MODIFICAR UN PROVEEDOR PARA VENTA DE GANADO");
                }
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = "EL PROVEEDOR PARA LA VENTA DE GANADO: " + this.txtNombre.Text.ToUpper() + " HA SIDO MODIFICADO EXITOSAMENTE";
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                this.Agregando(false, true);
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.GANPROVEEDORES, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ EL PROVEEDOR PARA LA VENTA DE GANADO: " + this.txtNombre.Text.ToUpper()));
                this.limpiacampos();
                this.gridProvGan.DataBind();
                this.gridProvGan.SelectedIndex = -1;
                this.btnModificarDeLista.Visible = false;
                this.btnEliminar.Visible = false;


            }
            catch (Exception ex)
            {
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "EL PROVEEDOR PARA LA VENTA DE GANADO " + this.txtNombre.Text.ToUpper() + " NO HA PODIDO SER MODIFICADO";
                this.lblMensajeException.Text = ex.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ex.Message, this.Request.Url.ToString());


            }
            finally
            {
                conGaribay.Close();
            }

        }

        protected void btnModificarDeLista_Click(object sender, EventArgs e)
        
             
        {
            this.Agregando(true, false);
            this.lblProveedores.Text = "MODIFICAR UN PROVEEDOR PARA LA VENTA DE GANADO";
           
        
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            string sql = "DELETE FROM gan_Proveedores  ";
            sql += "where ganProveedorID = @ganProveedorID";
           
            SqlCommand cmd = new SqlCommand(sql, conGaribay);
            conGaribay.Open();
            try
            {

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@ganProveedorID", SqlDbType.Int).Value = int.Parse(this.gridProvGan.SelectedDataKey["ganProveedorID"].ToString());
                int numregistros = cmd.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception("LA BASE DE DATOS REGRESO QUE SE ALTERARON: " + numregistros.ToString() + " REGISTROS AL ELIMINAR UN PROVEEDOR PARA VENTA DE GANADO");
                }
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = "EL PROVEEDOR PARA LA VENTA DE GANADO: " + this.txtNombre.Text.ToUpper() + " HA SIDO ELIMINADO EXITOSAMENTE";
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                this.Agregando(false, true);
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.GANPROVEEDORES, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ EL PROVEEDOR PARA LA VENTA DE GANADO: " + this.txtNombre.Text.ToUpper()));
                this.limpiacampos();
                this.gridProvGan.DataBind();
                this.gridProvGan.SelectedIndex = -1;
                this.btnModificarDeLista.Visible = false;
                this.btnEliminar.Visible = false;

            }
            catch (Exception ex)
            {
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "EL PROVEEDOR PARA LA VENTA DE GANADO " + this.txtNombre.Text.ToUpper() + " NO HA PODIDO SER ELIMINADO";
                this.lblMensajeException.Text = ex.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ex.Message, this.Request.Url.ToString());
            }



        }

    }
}
