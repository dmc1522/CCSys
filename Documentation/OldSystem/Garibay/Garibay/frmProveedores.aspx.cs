using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace Garibay
{
    public partial class WebForm5 : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Agregando(false, true);
                this.btnModificarDeLista.Visible = false;
                this.btnEliminarDeLista.Visible = false;
                this.btnCuentasProv.Visible = false;
                this.panelCuentasProv.Visible = false;
                try
                {
                     Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PROVEEDORES, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "EL USUARIO VISITÓ LA PÁGINA PROVEEDORES");

                }
                catch (Exception exception)
                {
                	Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL,Logger.typeUserActions.SELECT,int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                }
            }
            if (this.panelmensaje.Visible == true)
            {
                this.panelmensaje.Visible = false;
            }
            this.gridProveedores.DataSourceID = "SqlDataSource3";
            this.grdvCuentasProv.DataSourceID = "SqlDSCuentasBancoProv";
            this.compruebasecurityLevel();
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.btnAgregarcuentaProv.Visible = false; 
                this.btnModificarDeLista.Visible = false;
                this.btnEliminarDeLista.Visible = false;
                this.btnEliminarCuentaProv.Visible = false;
                this.btnAgregardeLista.Visible = false;
                this.panelAgregarProvedor.Visible = false;
                this.btnCuentasProv.Visible = false;
                this.panelCuentasProv.Visible = false;
                this.panelAgregarCuentaProv.Visible = false;
                this.gridProveedores.Columns[0].Visible = false;
                this.grdvCuentasProv.Columns[0].Visible = false;

            }

        }
        protected void Agregando(Boolean activaacgregar, Boolean semuestrbotonaagregar)
        {
            this.panelAgregarProvedor.Visible = activaacgregar;
            this.btnAgregardeLista.Visible = !activaacgregar;
            this.btnModificarDeLista.Visible = !activaacgregar;
            this.btnEliminarDeLista.Visible = !activaacgregar;
            this.btnAceptar.Visible = semuestrbotonaagregar;
            this.btnModificarpro.Visible = !semuestrbotonaagregar;
            if (this.gridProveedores.SelectedIndex == -1)
            {
                this.btnModificarDeLista.Visible = false;
                this.btnEliminarDeLista.Visible = false;
                this.btnCuentasProv.Visible = false;
            }
            if (this.grdvCuentasProv.SelectedIndex == -1)
            {
                this.btnEliminarCuentaProv.Visible = false;
                this.btnModificarCuentaProv_lista.Visible = false;
               
            }
            this.gridProveedores.Columns[0].Visible = !activaacgregar;
        }
        
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            this.Agregando(true, false);
            this.lblProveedores.Text = "MODIFICAR PROVEEDOR";
        }

        protected void btnAgregardeLista_Click(object sender, EventArgs e)
        {
            this.Agregando(true, true);
            this.lblProveedores.Text = "AGREGAR PROVEEDOR";
            this.limpiacampos();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string qryIns = "INSERT INTO Proveedores(Nombre, Direccion, CP, Comunidad, Municipio, estadoID, Teléfono, Celular, Fechaalta, Nombrecontacto, banco, Observaciones, storeTS, updateTS)";
            qryIns += "VALUES (@Nombre, @Direccion, @CP, @Comunidad, @Municipio, @estadoID, @Teléfono, @Celular, @Fechaalta, @Nombrecontacto, @banco, @Observaciones, @storeTS, @updateTS)";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);

            try
            {
                cmdIns.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = this.TxtNombre.Text;
                cmdIns.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = this.txtdireccion.Text;
                cmdIns.Parameters.Add("@CP", SqlDbType.VarChar).Value = this.txtCp.Text;
                cmdIns.Parameters.Add("@Comunidad", SqlDbType.VarChar).Value = this.txtComunidad.Text;
                cmdIns.Parameters.Add("@Municipio", SqlDbType.NChar).Value = this.txtMunicipio.Text;
                cmdIns.Parameters.Add("@estadoID", SqlDbType.Int).Value = int.Parse(this.drplstEstado.SelectedValue);
                cmdIns.Parameters.Add("@Teléfono", SqlDbType.VarChar).Value = this.txtTelefono.Text;
                cmdIns.Parameters.Add("@Celular", SqlDbType.VarChar).Value = this.txtCel.Text;
                cmdIns.Parameters.Add("@Fechaalta", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(Utils.Now.ToString());
                cmdIns.Parameters.Add("@Nombrecontacto", SqlDbType.VarChar).Value = this.txtNomCon.Text;
                cmdIns.Parameters.Add("@banco", SqlDbType.Text).Value = this.txtBanco.Text;
                cmdIns.Parameters.Add("@Observaciones", SqlDbType.Text).Value = this.txtObser.Text;
                cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(Utils.Now.ToString());
                cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(Utils.Now.ToString());

                conGaribay.Open();

                int numregistros = cmdIns.ExecuteNonQuery();

                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("PROVEEDOREXECUTEFAILED"), "AGREGADO", "AGREGARON", numregistros.ToString()));
                }
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PROVEEDORADDEDEXITO"), this.TxtNombre.Text.ToUpper());
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                this.Agregando(false, true);
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PROVEEDORES, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("SE AGREGÓ EL PROVEEDOR: " + this.TxtNombre.Text));
                this.limpiacampos();
                this.drplstEstado.SelectedValue = "14";

            }
            catch (InvalidOperationException exception)
            {
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PROVEEDORADDEDFAILED"), this.TxtNombre.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
            }
            catch (SqlException exception)
            {
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PROVEEDORADDEDFAILED"), this.TxtNombre.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());

            }
            catch (Exception exception)
            {
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PROVEEDORADDEDFAILED"), this.TxtNombre.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());

            }
            finally
            {
                conGaribay.Close();
            }



        }

        protected void btnCancelar_Click1(object sender, EventArgs e)
        {
            this.Agregando(false, true);
        }

        protected void limpiacampos()
        {
            this.txtBanco.Text = "";
            this.txtCel.Text = "";
            this.txtComunidad.Text = "";
            this.txtCp.Text = "";
            this.txtdireccion.Text = "";
            this.txtMunicipio.Text = "";
            this.TxtNombre.Text = "";
            this.txtNomCon.Text = "";
            this.txtObser.Text = "";
            this.txtTelefono.Text = "";
            if (this.drplstEstado.SelectedIndex >= 0)
            {
                this.drplstEstado.DataBind();
                this.drplstEstado.SelectedValue = "14";
            }
        }

        protected void gridProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drplstEstado.DataBind();
            if (panelAgregarProvedor.Visible == false)
            {
                btnEliminarDeLista.Visible = true;
                btnModificarDeLista.Visible = true;
                this.btnCuentasProv.Visible = true;
            }
            this.txtBanco.Text = this.gridProveedores.SelectedDataKey["banco"].ToString();
            this.txtCel.Text = this.gridProveedores.SelectedDataKey["Celular"].ToString();
            this.txtComunidad.Text = this.gridProveedores.SelectedDataKey["Comunidad"].ToString();
            this.txtCp.Text = this.gridProveedores.SelectedDataKey["CP"].ToString();
            this.txtdireccion.Text = this.gridProveedores.SelectedDataKey["Direccion"].ToString();
            this.txtMunicipio.Text = this.gridProveedores.SelectedDataKey["Municipio"].ToString();
            this.TxtNombre.Text = this.gridProveedores.SelectedDataKey["Nombre"].ToString();
            this.txtNomCon.Text = this.gridProveedores.SelectedDataKey["Nombrecontacto"].ToString();
            this.txtObser.Text = this.gridProveedores.SelectedDataKey["Observaciones"].ToString();
            this.txtTelefono.Text = this.gridProveedores.SelectedDataKey["Teléfono"].ToString();
            this.drplstEstado.SelectedValue = this.gridProveedores.SelectedDataKey["estadoID"].ToString();
            string msgDel = "return confirm('¿Realmente desea eliminar el proveedor: ";
            msgDel += this.gridProveedores.SelectedDataKey["Nombre"].ToString();
            msgDel += "?')";
            this.btnEliminarDeLista.Attributes.Add("onclick", msgDel);
            this.txtcuentasdeProv.Text = this.gridProveedores.SelectedDataKey["proveedorID"].ToString();
        }

        protected void btnEliminarDeLista_Click(object sender, EventArgs e)
        {
            string qryDel = "DELETE FROM Proveedores WHERE proveedorID=@proveedorID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdDel = new SqlCommand(qryDel, conGaribay);

            try
            {
                cmdDel.Parameters.Add("@proveedorID", SqlDbType.Int).Value = int.Parse(gridProveedores.SelectedDataKey["proveedorID"].ToString());

                conGaribay.Open();

                int numregistros = cmdDel.ExecuteNonQuery();

                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("PROVEEDOREXECUTEFAILED"), "ELIMINADO", "ELIMINARON", numregistros.ToString()));
                }
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PROVEEDORDELETEDEXITO"), gridProveedores.SelectedDataKey["Nombre"].ToString().ToUpper());
                this.lblMensajeException.Text = ""; // NO HAY EXCEPTION

                btnModificarDeLista.Visible = false;
                btnEliminarDeLista.Visible = false;
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PROVEEDORES, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ EL PROVEEDOR: " + gridProveedores.SelectedDataKey["Nombre"].ToString().ToUpper()));
                this.gridProveedores.SelectedIndex = -1;
                this.limpiacampos();
                this.Agregando(false, true);
                this.gridProveedores.DataBind();

            }
            catch (InvalidOperationException exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PROVEEDORDELETEDFAILED"), this.gridProveedores.SelectedDataKey["Nombre"].ToString().ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
            }
            catch (SqlException exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PROVEEDORDELETEDFAILED"), this.gridProveedores.SelectedDataKey["Nombre"].ToString().ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;

            }
            catch (Exception exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PROVEEDORDELETEDFAILED"), this.gridProveedores.SelectedDataKey["Nombre"].ToString().ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;

            }
            finally
            {
                conGaribay.Close();
            }
        }

        protected void btnModificarpro_Click(object sender, EventArgs e)
        {
            string qryIns = "UPDATE Proveedores SET Nombre = @Nombre, Direccion = @Direccion, CP = @CP, Comunidad = @Comunidad, Municipio = @Municipio, estadoID = @estadoID, Teléfono = @Teléfono, Celular = @Celular, Fechaalta = @Fechaalta, Nombrecontacto = @Nombrecontacto, banco = @banco, Observaciones = @Observaciones, updateTS = @updateTS WHERE proveedorID = @proveedorID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            if (this.gridProveedores.SelectedDataKey["Nombre"].ToString() != this.TxtNombre.Text)
            {
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PROVEEDORES, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ EL NOMBRE DEL PROVEEDOR: \"" + this.gridProveedores.SelectedDataKey["Nombre"].ToString().ToUpper() + "\" POR: \"" + this.TxtNombre.Text.ToUpper()) + "\"");
            }
            try
            {
                cmdIns.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = this.TxtNombre.Text;
                cmdIns.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = this.txtdireccion.Text;
                cmdIns.Parameters.Add("@CP", SqlDbType.VarChar).Value = this.txtCp.Text;
                cmdIns.Parameters.Add("@Comunidad", SqlDbType.VarChar).Value = this.txtComunidad.Text;
                cmdIns.Parameters.Add("@Municipio", SqlDbType.NChar).Value = this.txtMunicipio.Text;
                cmdIns.Parameters.Add("@estadoID", SqlDbType.Int).Value = int.Parse(this.drplstEstado.SelectedValue);
                cmdIns.Parameters.Add("@Teléfono", SqlDbType.VarChar).Value = this.txtTelefono.Text;
                cmdIns.Parameters.Add("@Celular", SqlDbType.VarChar).Value = this.txtCel.Text;
                cmdIns.Parameters.Add("@Fechaalta", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(Utils.Now.ToString());
                cmdIns.Parameters.Add("@Nombrecontacto", SqlDbType.VarChar).Value = this.txtNomCon.Text;
                cmdIns.Parameters.Add("@banco", SqlDbType.Text).Value = this.txtBanco.Text;
                cmdIns.Parameters.Add("@Observaciones", SqlDbType.Text).Value = this.txtObser.Text;
                cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(Utils.Now.ToString());
                cmdIns.Parameters.Add("@proveedorID", SqlDbType.Int).Value = int.Parse(this.gridProveedores.SelectedDataKey["proveedorID"].ToString());

                conGaribay.Open();

                int numregistros = cmdIns.ExecuteNonQuery();

                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("PROVEEDOREXECUTEFAILED"), "MODIFICADO", "MODIFICARON", numregistros.ToString()));
                }
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PROVEEDORMODIFIEDEXITO"), this.TxtNombre.Text.ToUpper());
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                this.Agregando(false, true);
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PROVEEDORES, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ EL PROVEEDOR: " + this.TxtNombre.Text.ToUpper()));
                this.limpiacampos();

            }
            catch (InvalidOperationException exception)
            {
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PROVEEDORMODIFIEDFAILED"), this.TxtNombre.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
            }
            catch (SqlException exception)
            {
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PROVEEDORMODIFIEDFAILED"), this.TxtNombre.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());

            }
            catch (Exception exception)
            {
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PROVEEDORMODIFIEDFAILED"), this.TxtNombre.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());

            }
            finally
            {
                conGaribay.Close();
            }
        }

        protected void btnCuentasProv_Click(object sender, EventArgs e)
        {
            this.panelCuentasProv.Visible = true;
            this.panellistaCuentas.Visible = true;
            this.panelAgregarCuentaProv.Visible = false;
            this.btnAgregarcuentaProv.Visible = true;
            this.btnModificarCuentaProv_lista.Visible = false;
            this.btnEliminarCuentaProv.Visible = false;
            this.lblListaCuentaProv.Text="Cuentas de Banco del Proveedor "+ this.gridProveedores.SelectedDataKey["Nombre"].ToString();
            this.panelfmrprovedor.Visible = false;
        }

        protected void btnAgregarCuenta_Click(object sender, EventArgs e)
        {
            this.panelAgregarCuentaProv.Visible = true;
            this.grdvCuentasProv.Columns[0].Visible = false;
            this.btnModificarcuentaProv.Visible = false;
            this.btnAgregarcuentaProv.Visible = true;
            limpiarcamposdeCuentas();
            this.drpdlProveedor.DataBind();
            this.drpdlProveedor.SelectedValue = this.gridProveedores.SelectedDataKey["proveedorID"].ToString();
            this.lblcuentadebancoprov.Text="AGREGAR NUEVA CUENTA DE BANCO DE PROVEEDOR";
            this.grdvCuentasProv.SelectedIndex=-1;
        }

        protected void btnModificarCuentaProv_Click(object sender, EventArgs e)
        {
            this.panelAgregarCuentaProv.Visible = true;
            this.grdvCuentasProv.Columns[0].Visible = false;
            this.btnAgregarcuentaProv.Visible = false;
            this.btnModificarcuentaProv.Visible = true;
            this.lblcuentadebancoprov.Text = "MODIFICANDO LA CUENTA DE BANCO " + this.grdvCuentasProv.SelectedDataKey["numCuenta"].ToString().ToUpper() + " DEL PROVEEDOR " + this.grdvCuentasProv.SelectedDataKey["Expr1"].ToString().ToUpper();
        }

        protected void btnAgregarcuentaProv_Click(object sender, EventArgs e)
        {

            string qryIns = "INSERT INTO CuentasBancoProveedores(bancoID, proveedorID, numCuenta, nombre)";
            qryIns += "VALUES (@bancoID, @proveedorID, @numCuenta, @nombre)";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);

            try
            {
                cmdIns.Parameters.Add("bancoID", SqlDbType.Int).Value = int.Parse(this.drpdlbanco.SelectedValue);
                cmdIns.Parameters.Add("@proveedorID", SqlDbType.Int).Value = int.Parse(this.drpdlProveedor.SelectedValue);
                cmdIns.Parameters.Add("@numCuenta", SqlDbType.VarChar).Value = this.txtCuentaBanco.Text;
                cmdIns.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtTitular.Text;
                conGaribay.Open();
                int numregistros = cmdIns.ExecuteNonQuery();

                if (numregistros != 1)
                {
                   throw new Exception(string.Format(myConfig.StrFromMessages("CUENTAPROVEEDOREXECUTEFAILED"), "AGREGADO", "AGREGARON", numregistros.ToString()));
                }
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTAPROVEEDORADDEDEXITO"), this.txtCuentaBanco.Text.ToUpper(),this.drpdlProveedor.SelectedItem.Text.ToUpper());
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                this.panelAgregarCuentaProv.Visible = false;
                this.grdvCuentasProv.Columns[0].Visible = true;
                this.grdvCuentasProv.SelectedIndex=-1;
                this.Agregando(false, true);
                
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PROVEEDORES, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("SE AGREGÓ LA CUENTA DE BANCO " + this.txtCuentaBanco.Text + "AL PROVEEDOR: " + this.drpdlProveedor.SelectedItem.Text.ToUpper()));
                limpiarcamposdeCuentas();
                this.limpiacampos();

            }
            catch (InvalidOperationException exception)
            {
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTAPROVEEDORADDEDFAILED"), this.TxtNombre.Text.ToUpper(), this.txtCuentaBanco.Text,this.drpdlProveedor.SelectedItem.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
            }
            catch (SqlException exception)
            {
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTAPROVEEDORADDEDFAILED"), this.TxtNombre.Text.ToUpper(), this.txtCuentaBanco.Text, this.drpdlProveedor.SelectedItem.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());

            }
            catch (Exception exception)
            {
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTAPROVEEDORADDEDFAILED"), this.TxtNombre.Text.ToUpper(), this.txtCuentaBanco.Text, this.drpdlProveedor.SelectedItem.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());

            }
            finally
            {
                conGaribay.Close();
            }

        }

        protected void grdvCuentasProv_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            this.drpdlbanco.DataBind();
            this.drpdlProveedor.DataBind();
            
            
                this.btnModificarCuentaProv_lista.Visible = true;
                this.btnEliminarCuentaProv.Visible = true;
            
            
            this.drpdlbanco.SelectedValue = this.grdvCuentasProv.SelectedDataKey["bancoID"].ToString();
            this.drpdlProveedor.SelectedValue = this.grdvCuentasProv.SelectedDataKey["proveedorID"].ToString();
            this.txtCuentaBanco.Text = this.grdvCuentasProv.SelectedDataKey["numCuenta"].ToString();
            this.txtTitular.Text = this.grdvCuentasProv.SelectedDataKey["Expr2"].ToString();

            string msgDel = "return confirm('¿Realmente desea eliminar la Cuenta de Banco " + this.grdvCuentasProv.SelectedDataKey["numCuenta"].ToString() + " del proveedor: " + this.grdvCuentasProv.SelectedDataKey["Expr1"].ToString();
            msgDel += "?')";
            this.btnEliminarCuentaProv.Attributes.Add("onclick", msgDel);
            
        }

        protected void btnModificarcuentaProv_Click1(object sender, EventArgs e)
        {
            string qryIns = "UPDATE CuentasBancoProveedores SET bancoID = @bancoID, proveedorID = @proveedorID, numCuenta = @numCuenta, nombre = @nombre WHERE cuentaID = @cuentaID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            
            try
            {
                cmdIns.Parameters.Add("@bancoID", SqlDbType.Int).Value = int.Parse(this.drpdlbanco.SelectedValue);
                cmdIns.Parameters.Add("@proveedorID", SqlDbType.Int).Value = int.Parse(this.drpdlProveedor.SelectedValue);
                cmdIns.Parameters.Add("@numCuenta", SqlDbType.VarChar).Value = this.txtCuentaBanco.Text;
                cmdIns.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtTitular.Text;
                cmdIns.Parameters.Add("@cuentaID", SqlDbType.VarChar).Value = int.Parse(this.grdvCuentasProv.SelectedDataKey["cuentaID"].ToString());

                conGaribay.Open();

                int numregistros = cmdIns.ExecuteNonQuery();

                if (numregistros != 1)
                {
                       throw new Exception(string.Format(myConfig.StrFromMessages("CUENTAPROVEEDOREXECUTEFAILED"), "MODIFICADO", "MODIFICARON", numregistros.ToString()));
                }
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTAPROVEEDORMODIFIEDEXITO"), this.txtCuentaBanco.Text,this.drpdlProveedor.SelectedItem.Text.ToUpper());
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PROVEEDORES, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ LA CUENTA DE BANCO " + this.txtCuentaBanco.Text + " DEL PROVEEDOR: " + this.drpdlProveedor.SelectedItem.Text.ToUpper()));
                if (this.grdvCuentasProv.SelectedDataKey["bancoID"].ToString() != this.drpdlbanco.SelectedValue)
                {
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PROVEEDORES, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ EL BANCO DE LA CUENTA DE BANCO "+ this.grdvCuentasProv.SelectedDataKey["numCuenta"] + " DEL PROVEEDOR "+this.grdvCuentasProv.SelectedDataKey["Expr1"]+" DE "+ this.grdvCuentasProv.SelectedDataKey["nombre"].ToString().ToUpper()+" A " +this.drpdlbanco.SelectedItem.Text.ToUpper()));
                }
                if (this.grdvCuentasProv.SelectedDataKey["proveedorID"].ToString() != this.drpdlProveedor.SelectedValue)
                {
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PROVEEDORES, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ EL PROVEEDOR DE LA CUENTA DE BANCO "+ this.grdvCuentasProv.SelectedDataKey["numCuenta"] + " DE "+this.grdvCuentasProv.SelectedDataKey["Expr1"]+" A "+ this.drpdlProveedor.SelectedItem.Text.ToUpper()));
                }
                
                this.limpiacampos();
                limpiarcamposdeCuentas();
                this.panelAgregarCuentaProv.Visible = false;
                this.grdvCuentasProv.Columns[0].Visible = true;
                this.grdvCuentasProv.SelectedIndex = -1;
                this.Agregando(false, true);
            }
            catch (InvalidOperationException exception)
            {
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTAPROVEEDORMODIFIEDFAILED"), this.txtCuentaBanco.Text, this.drpdlProveedor.SelectedItem.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
            }
            catch (SqlException exception)
            {
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTAPROVEEDORMODIFIEDFAILED"), this.txtCuentaBanco.Text, this.drpdlProveedor.SelectedItem.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());

            }
            catch (Exception exception)
            {
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTAPROVEEDORMODIFIEDFAILED"), this.txtCuentaBanco.Text, this.drpdlProveedor.SelectedItem.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());

            }
            finally
            {
                conGaribay.Close();
            }
        }

        protected void btnEliminarCuentaProv_Click(object sender, EventArgs e)
        {
            string qryDel = "DELETE FROM CuentasBancoProveedores WHERE cuentaID=@cuentaID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdDel = new SqlCommand(qryDel, conGaribay);

            try
            {
                cmdDel.Parameters.Add("@cuentaID", SqlDbType.Int).Value = int.Parse(this.grdvCuentasProv.SelectedDataKey["cuentaID"].ToString());

                conGaribay.Open();

                int numregistros = cmdDel.ExecuteNonQuery();

                if (numregistros != 1)
                {
                   throw new Exception(string.Format(myConfig.StrFromMessages("CUENTAPROVEEDOREXECUTEFAILED"), "ELIMINADO", "ELIMINARON", numregistros.ToString()));
                }
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTAPROVEEDORDELETEDEXITO"), this.txtCuentaBanco.Text, this.drpdlProveedor.SelectedItem.Text.ToUpper());
                this.lblMensajeException.Text = ""; // NO HAY EXCEPTION

                btnModificarDeLista.Visible = false;
                btnEliminarDeLista.Visible = false;
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PROVEEDORES, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ LA CUENTA DE BANCO " + this.txtCuentaBanco.Text + " DEL PROVEEDOR: " + this.drpdlProveedor.SelectedItem.Text.ToUpper()));
                this.gridProveedores.SelectedIndex = -1;
                //this.limpiacampos();
                
                this.gridProveedores.DataBind();
                this.grdvCuentasProv.Columns[0].Visible = true;
                this.grdvCuentasProv.SelectedIndex = -1;
                this.Agregando(false, true);
            }
            catch (InvalidOperationException exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTAPROVEEDORDELETEDFAILED"), this.txtCuentaBanco.Text, this.drpdlProveedor.SelectedItem.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
            }
            catch (SqlException exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTAPROVEEDORDELETEDFAILED"), this.txtCuentaBanco.Text, this.drpdlProveedor.SelectedItem.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;

            }
            catch (Exception exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTAPROVEEDORDELETEDFAILED"), this.txtCuentaBanco.Text, this.drpdlProveedor.SelectedItem.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;

            }
            finally
            {
                conGaribay.Close();
            }
        }

        protected void btnRegresarLista_Click(object sender, EventArgs e)
        {
            this.panelfmrprovedor.Visible = true;
            this.panelCuentasProv.Visible = false;
        }

        protected void btncancelarcuentasProv_Click(object sender, EventArgs e)
        {
            limpiarcamposdeCuentas();
            this.panelAgregarCuentaProv.Visible = false;
        }
        protected void limpiarcamposdeCuentas(){

            this.drpdlbanco.SelectedIndex = -1;
            this.drpdlProveedor.SelectedIndex = -1;
            this.txtCuentaBanco.Text = "";
            this.txtTitular.Text = "";

        }

       
  
    }
}
