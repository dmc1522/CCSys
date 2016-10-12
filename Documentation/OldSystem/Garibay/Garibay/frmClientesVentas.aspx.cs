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


namespace Garibay
{
    public partial class frmClientesVentas : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack){
                this.panelmensaje.Visible=false;
                this.panelAgregaCliente.Visible=false;
                this.btnEliminar.Visible = false;
                this.btnMostrarModificar.Visible = false;
                this.drpdlEstado.DataBind();
                this.drpdlEstado.SelectedIndex = 13;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CLIENTESVENTAS, Logger.typeUserActions.SELECT, this.UserID, "VISITO LA PÁGINA DE CLIENTES PARA VENTAS");
            }
            if(this.panelmensaje.Visible){
                this.panelmensaje.Visible=false;
            }
            this.compruebasecurityLevel();

        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.btnAgregarDeForm.Visible = false;
                this.btnModificarDeForm.Visible = false;
                this.btnEliminar.Visible = false;
                this.btnMostrarAgregar.Visible = false;
                this.btnMostrarModificar.Visible = false;
                this.grdvClientes.Columns[0].Visible = false;
            }

        }
        protected void btnAgregarDeForm_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConAgregar = new SqlConnection(myConfig.ConnectionInfo);
            string sqlstr = "insert into ClientesVentas (nombre,domicilio,ciudad,telefono, estadoID,RFC, userID, colonia, CP) values(@nombre, @domicilio,@ciudad,@telefono, @estadoID,@RFC, @userID, @colonia, @CP) ";
            SqlCommand cmdInsertCLientesVentas = new SqlCommand(sqlstr, sqlConAgregar);
            sqlConAgregar.Open();
            try
            {
                cmdInsertCLientesVentas.Parameters.Clear();
                cmdInsertCLientesVentas.Parameters.Add("@nombre",SqlDbType.VarChar).Value = this.txtNombreCliente.Text;
                cmdInsertCLientesVentas.Parameters.Add("@domicilio",SqlDbType.VarChar).Value = this.txtDomicilioCliente.Text;
                cmdInsertCLientesVentas.Parameters.Add("@ciudad",SqlDbType.VarChar).Value = this.txtCiudad.Text;
                cmdInsertCLientesVentas.Parameters.Add("@telefono",SqlDbType.VarChar).Value = this.txtTelefono.Text;
                cmdInsertCLientesVentas.Parameters.Add("@estadoID",SqlDbType.Int).Value = int.Parse(this.drpdlEstado.SelectedValue);
                cmdInsertCLientesVentas.Parameters.Add("@RFC",SqlDbType.VarChar).Value = this.txtRFC.Text;
                cmdInsertCLientesVentas.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                cmdInsertCLientesVentas.Parameters.Add("@colonia", SqlDbType.VarChar).Value = this.txtColonia.Text;
                cmdInsertCLientesVentas.Parameters.Add("@CP", SqlDbType.VarChar).Value = this.txtCP.Text;




                int numregistros = cmdInsertCLientesVentas.ExecuteNonQuery();
                if(numregistros!=1){
                   throw new Exception("AL QUERER INSERTAR UN CLIENTE DE VENTA LA DB REGRESÓ QUE SE MODIFICARON: " + numregistros.ToString());
                }
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CLIENTESVENTAS, Logger.typeUserActions.INSERT, this.UserID, "AGREGO EL CLIENTE DE VENTA: " + this.txtNombreCliente.Text);

                this.panelAgregaCliente.Visible=false;
                //this.panelListaProductos.Visible = false;
                this.panelmensaje.Visible=true;
                this.lblMensajeException.Text ="";
                this.lblMensajeOperationresult.Text = "SE AGREGO EL CIENTE " + this.txtNombreCliente.Text.ToUpper() + " EXITOSAMENTE";
                this.lblMensajetitle.Text = "ÉXITO";
                this.imagenbien.Visible = true;
                this.imagenmal.Visible = false;
                this.limpiacampos();
                this.grdvClientes.DataBind();


            }
            catch(Exception ex){
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "ERROR AL INSERTAR UN CLIENTE PARA VENTA. LA EXC. FUE: " + ex.Message, this.Request.Url.ToString());
                this.panelAgregaCliente.Visible=false;
               //this.panelListaProductos.Visible = false;
               this.panelmensaje.Visible=true;
               this.lblMensajeException.Text =ex.Message;
               this.lblMensajeOperationresult.Text = "EL CLIENTE " + this.txtNombreCliente.Text.ToUpper() + "NO HA PODIDO SER AGREGADO";
               this.lblMensajetitle.Text = "FALLO";
               this.imagenmal.Visible=true;
                this.imagenbien.Visible=false;

                
            }
            finally{
                sqlConAgregar.Close();
            }

        }
        protected void limpiacampos(){
            this.txtNombreCliente.Text = "";
            this.txtTelefono.Text = "";
            this.txtRFC.Text = "";
            this.txtCiudad.Text = "";
            this.txtDomicilioCliente.Text = "";
            this.drpdlEstado.SelectedIndex = 13;
        }

        protected void btnMostrarAgregar_Click(object sender, EventArgs e)
        {
            this.panelAgregaCliente.Visible = true;
            //this.panelListaClientes.Visible = false;
            this.lblNewCliente.Text = "AGREGAR NUEVO CLIENTE PARA VENTA";
            this.grdvClientes.SelectedIndex = -1;
            this.btnAgregarDeForm.Visible = true;
            this.btnModificarDeForm.Visible = false;
            this.btnMostrarModificar.Visible = false;
            this.btnEliminar.Visible = false;
        }

        protected void grdvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.grdvClientes.SelectedDataKey[0]!=null){
                this.btnMostrarModificar.Visible = true;
                this.btnEliminar.Visible = true;
                string msgDel = "return confirm('¿Realmente desea eliminar productor: ";
                msgDel += this.grdvClientes.SelectedDataKey[1].ToString();
                msgDel += "?')";
                btnEliminar.Attributes.Add("onclick", msgDel);
                this.panelAgregaCliente.Visible = false;

            }
        }

        protected void btnMostrarModificar_Click(object sender, EventArgs e)
        {
            if(this.grdvClientes.SelectedDataKey[0]!=null){
                this.CargaDatosModificar(int.Parse(this.grdvClientes.SelectedDataKey[0].ToString()));
                this.btnAgregarDeForm.Visible = false;
                this.btnModificarDeForm.Visible = true;
                this.lblNewCliente.Text = "MODIFICAR CLIENTE PARA VENTA:  " + this.grdvClientes.SelectedDataKey[1].ToString();
            
            }
            
        }
        protected void CargaDatosModificar(int id){
            SqlConnection conSacaDatos = new SqlConnection(myConfig.ConnectionInfo);
            string query ="select nombre, domicilio, ciudad, telefono, estadoID, RFC from ClientesVentas where clienteventaID = @clienteID";
            SqlCommand cmdsacaDatos = new SqlCommand(query, conSacaDatos);
            conSacaDatos.Open();
            try{
                cmdsacaDatos.Parameters.Clear();
                cmdsacaDatos.Parameters.Add("@clienteID",SqlDbType.Int).Value = id;
                SqlDataReader rd = cmdsacaDatos.ExecuteReader();
                while(rd.Read()){
                    this.txtNombreCliente.Text = rd["nombre"].ToString();
                    this.txtDomicilioCliente.Text = rd["domicilio"].ToString();
                    this.txtTelefono.Text = rd["telefono"].ToString();
                    this.txtCiudad.Text = rd["ciudad"].ToString();
                    this.drpdlEstado.SelectedValue = rd["estadoID"].ToString();
                    this.txtRFC.Text = rd["RFC"].ToString();
                    this.panelAgregaCliente.Visible = true;

                }

            }
            catch(Exception ex){
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL CARGAR LOS DATOS A MODIFICAR DEL CLIENTE DE VENTA CON EL ID: " + id.ToString() + ". LA EXC FUE: " + ex.Message, this.Request.Url.ToString());
                this.panelmensaje.Visible = true;
                this.lblMensajeException.Text = ex.Message;
                this.lblMensajeOperationresult.Text = "ERROR AL CARGAR EL CLIENTE CON EL ID:  " + id.ToString() + ". LA EXC FUE: " + ex.Message;
                this.lblMensajetitle.Text = "FALLO";
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;


            }
            finally{
                conSacaDatos.Close();
            }

        }

        protected void btnModificarDeForm_Click(object sender, EventArgs e)
        {
            if (this.grdvClientes.SelectedDataKey[0] != null)
            {
                SqlConnection sqlConModificar = new SqlConnection(myConfig.ConnectionInfo);
                string sqlstr = "update ClientesVentas set nombre = @nombre, domicilio = @domicilio, ciudad = @ciudad, telefono =@telefono, estadoID = @estadoID,RFC = @RFC, colonia = @colonia, CP = @CP where  clienteventaID = @clienteID";
                SqlCommand cmdModifyCLientesVentas = new SqlCommand(sqlstr, sqlConModificar);
                sqlConModificar.Open();
                try
                {
                    cmdModifyCLientesVentas.Parameters.Clear();
                    cmdModifyCLientesVentas.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = this.txtNombreCliente.Text;
                    cmdModifyCLientesVentas.Parameters.Add("@domicilio", SqlDbType.NVarChar).Value = this.txtDomicilioCliente.Text;
                    cmdModifyCLientesVentas.Parameters.Add("@ciudad", SqlDbType.NVarChar).Value = this.txtCiudad.Text;
                    cmdModifyCLientesVentas.Parameters.Add("@telefono", SqlDbType.NVarChar).Value = this.txtTelefono.Text;
                    cmdModifyCLientesVentas.Parameters.Add("@estadoID", SqlDbType.Int).Value = int.Parse(this.drpdlEstado.SelectedValue);
                    cmdModifyCLientesVentas.Parameters.Add("@RFC", SqlDbType.NVarChar).Value = this.txtRFC.Text;
                    cmdModifyCLientesVentas.Parameters.Add("@colonia", SqlDbType.NVarChar).Value = this.txtColonia.Text;
                    cmdModifyCLientesVentas.Parameters.Add("@CP", SqlDbType.NVarChar).Value = this.txtCP.Text;
                    cmdModifyCLientesVentas.Parameters.Add("@clienteID", SqlDbType.Int).Value = int.Parse(this.grdvClientes.SelectedDataKey[0].ToString());

                    int numregistros = cmdModifyCLientesVentas.ExecuteNonQuery();
                    if (numregistros != 1)
                    {
                        throw new Exception("AL QUERER MODIFICAR EL CLIENTE DE VENTA LA DB REGRESÓ QUE SE MODIFICARON: " + numregistros.ToString());
                    }
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CLIENTESVENTAS, Logger.typeUserActions.UPDATE, this.UserID, "MODIFICÓ EL CLIENTE DE VENTA: " + this.txtNombreCliente.Text);

                    this.panelAgregaCliente.Visible = false;
                    //this.panelListaProductos.Visible = false;
                    this.panelmensaje.Visible = true;
                    this.lblMensajeException.Text = "";
                    this.lblMensajeOperationresult.Text = "SE MODIFICÓ EL CIENTE " + this.txtNombreCliente.Text.ToUpper() + "EXITOSAMENTE";
                    this.lblMensajetitle.Text = "ÉXITO";
                    this.imagenbien.Visible = true;
                    this.imagenmal.Visible = false;
                    this.limpiacampos();
                    this.grdvClientes.DataBind();
                    this.btnEliminar.Visible = false;
                    this.btnMostrarAgregar.Visible = true;
                    this.btnMostrarModificar.Visible = false;
                    this.grdvClientes.SelectedIndex = -1;


                }
                catch (Exception ex)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL MODIFICAR UN CLIENTE PARA VENTA. LA EXC. FUE: " + ex.Message, this.Request.Url.ToString());
                    this.panelAgregaCliente.Visible = false;
                    //this.panelListaProductos.Visible = false;
                    this.panelmensaje.Visible = true;
                    this.lblMensajeException.Text = ex.Message;
                    this.lblMensajeOperationresult.Text = "EL CLIENTE " + this.txtNombreCliente.Text.ToUpper() + "NO HA PODIDO SER MODIFICADO";
                    this.lblMensajetitle.Text = "FALLO";
                    this.imagenmal.Visible = true;
                    this.imagenbien.Visible = false;


                }
                finally
                {
                    sqlConModificar.Close();
                }
            }
            

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (this.grdvClientes.SelectedDataKey[0] != null)
            {
                SqlConnection sqlconEliminar = new SqlConnection(myConfig.ConnectionInfo);
                string sqlstr = "Delete from  ClientesVentas where  clienteventaID = @clienteID";
                SqlCommand cmdEliminar= new SqlCommand(sqlstr, sqlconEliminar);
                sqlconEliminar.Open();
                try
                {
                   cmdEliminar.Parameters.Add("@clienteID", SqlDbType.Int).Value = int.Parse(this.grdvClientes.SelectedDataKey[0].ToString());
                    int numregistros = cmdEliminar.ExecuteNonQuery();
                    if (numregistros != 1)
                    {
                        throw new Exception("AL QUERER ELIMINAR EL CLIENTE DE VENTA LA DB REGRESÓ QUE SE MODIFICARON: " + numregistros.ToString());
                    }
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CLIENTESVENTAS, Logger.typeUserActions.DELETE, this.UserID, "ELIMINÓ EL CLIENTE DE VENTA: " + this.txtNombreCliente.Text);

                    this.panelAgregaCliente.Visible = false;
                    //this.panelListaProductos.Visible = false;
                    this.panelmensaje.Visible = true;
                    this.lblMensajeException.Text = "";
                    this.lblMensajeOperationresult.Text = "SE ELIMINÓ EL CIENTE " + this.txtNombreCliente.Text.ToUpper() + "EXITOSAMENTE";
                    this.lblMensajetitle.Text = "ÉXITO";
                    this.imagenbien.Visible = true;
                    this.imagenmal.Visible = false;
                    this.limpiacampos();
                    this.grdvClientes.DataBind();
                    this.btnEliminar.Visible = false;
                    this.grdvClientes.SelectedIndex = -1;
                    this.btnMostrarModificar.Visible = false;


                }
                catch (Exception ex)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, this.UserID, "ERROR AL ELIMINAR UN CLIENTE PARA VENTA. LA EXC. FUE: " + ex.Message, this.Request.Url.ToString());
                    this.panelAgregaCliente.Visible = false;
                    //this.panelListaProductos.Visible = false;
                    this.panelmensaje.Visible = true;
                    this.lblMensajeException.Text = ex.Message;
                    this.lblMensajeOperationresult.Text = "EL CLIENTE " + this.txtNombreCliente.Text.ToUpper() + "NO HA PODIDO SER ELIMINADO";
                    this.lblMensajetitle.Text = "FALLO";
                    this.imagenmal.Visible = true;
                    this.imagenbien.Visible = false;


                }
                finally
                {
                    sqlconEliminar.Close();
                }
            }
            
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.panelAgregaCliente.Visible = false;
            
        }
    }
}