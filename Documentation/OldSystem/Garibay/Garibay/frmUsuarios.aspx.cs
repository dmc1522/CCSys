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
using System.Data.Odbc;
using System.IO;


namespace Garibay
{
    public partial class WebForm1 : Garibay.BasePage
    {
        //String nombreanterior;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Agregando(false, true);
                this.btnEliminar.Visible = false;
                this.btnModificarDeLista.Visible = false;
                try
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.USUARIOS, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "EL USUARIO VISITÓ LA PÁGINA USUARIOS");
                }
                catch (Exception exception) {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                
                }
            }
            if (this.panelmensaje.Visible == true)
            {
                this.panelmensaje.Visible = false;
                
            }
            this.gridUsers.DataSourceID = "SqlDataSource1";
            this.compruebasecurityLevel();
            
         
         

        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {


                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
                
            }

        }

        protected void Agregando(Boolean muestrapanelgregar, Boolean semuestrabotonagregardeabajo)
        {
            this.panelagregarUsuario.Visible = muestrapanelgregar;
            this.btnAgregarDeLista.Visible = !muestrapanelgregar;
            this.btnModificarDeLista.Visible = !muestrapanelgregar;
            this.btnEliminar.Visible = !muestrapanelgregar;
            if (this.gridUsers.SelectedIndex == -1)
            {
                this.btnModificarDeLista.Visible = false;
                this.btnEliminar.Visible = false;
            }

            this.btnAgregarDeForm.Visible = semuestrabotonagregardeabajo;
            this.btnModificarDeForm.Visible = !semuestrabotonagregardeabajo;
            this.gridUsers.Columns[0].Visible = !muestrapanelgregar;

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            this.Agregando(true, true);
            this.lblUsuarios.Text = "AGREGAR NUEVO USUARIO";
            this.limpiacampos();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            this.Agregando(true, false);
            this.lblUsuarios.Text = "MODIFICAR USUARIO";
           
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Agregando(false, true);
        }

        protected void btnAgregarDeForm_Click(object sender, EventArgs e)
        {


            string qryIns = "INSERT INTO Users(username,password,securitylevelID,enabled,Nombre,email) VALUES (@username,@password,@securitylevelID,@enable,@Nombre,@email)", clavesha1;
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            cmdIns.Parameters.Add("@username", SqlDbType.VarChar).Value = txtUsername.Text;
            clavesha1 = FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtPassword.Text, "SHA1");
            cmdIns.Parameters.Add("@password", SqlDbType.VarChar).Value = clavesha1;
            cmdIns.Parameters.Add("@securitylevelID", SqlDbType.Int).Value = int.Parse(this.cmbLevelSecurity.SelectedValue);
            cmdIns.Parameters.Add("@enable", SqlDbType.Bit).Value = this.chkActivo.Checked;
            cmdIns.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = this.txtNombre.Text;
            cmdIns.Parameters.Add("@email", SqlDbType.VarChar).Value = this.txtemail.Text;

            try
            {
                conGaribay.Open();
                
                int numregistros = cmdIns.ExecuteNonQuery();

                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("USUARIOEXECUTEFAILED"), "AGREGADO", "AGREGARON", numregistros.ToString()));
                }
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("USUARIOADDEDEXITO"),this.txtUsername.Text.ToUpper());
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                this.Agregando(false, true);
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.USUARIOS, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("SE AGREGÓ EL USUARIO: " + this.txtUsername.Text));
                this.limpiacampos();

            }
             catch (InvalidOperationException exception)
            {
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("USUARIOADDEDFAILED"), this.txtUsername.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
            }
            catch (SqlException exception)
            {
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("USUARIOADDEDFAILED"), this.txtUsername.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());

            }
            catch (Exception exception)
            {
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("USUARIOADDEDFAILED"), this.txtUsername.Text.ToUpper());
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
        protected void limpiacampos()
        {
            this.txtNombre.Text = "";
            this.txtUsername.Text = "";
            this.txtPassword.Text = "";
            this.txtPassword2.Text = "";
            this.txtemail.Text = "";
            if (this.cmbLevelSecurity.SelectedIndex > 0)
                this.cmbLevelSecurity.SelectedIndex = 0;

        }

        protected void gridUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            
          
            this.cmbLevelSecurity.DataBind();
            
            
            if (this.gridUsers.SelectedDataKey["userID"] != null)
            {
                if (panelagregarUsuario.Visible == false)
                {
                    this.btnModificarDeLista.Visible = true;
                    this.btnEliminar.Visible = true;
                }
               
                
                this.txtNombre.Text = this.gridUsers.SelectedDataKey["Nombre"].ToString();
                this.txtUsername.Text = this.gridUsers.SelectedDataKey["username"].ToString();
                this.cmbLevelSecurity.SelectedValue = this.gridUsers.SelectedDataKey["securitylevelID"].ToString();
                this.chkActivo.Checked = (bool)this.gridUsers.SelectedDataKey["enabled"];
                string msgDel = "return confirm('¿Realmente desea eliminar el usuario: ";
                msgDel += this.gridUsers.SelectedDataKey["Nombre"].ToString();
                msgDel += "?')";
                btnEliminar.Attributes.Add("onclick", msgDel); 
                this.txtemail.Text=this.gridUsers.SelectedDataKey["email"].ToString();


            }


        }

        protected void btnModificarDeForm_Click(object sender, EventArgs e)
        {


            string qryUp = "UPDATE Users SET Username = @Username, Password = @Password,securitylevelID = @securitylevelID, enabled = @enabled, Nombre = @Nombre,  email=@email WHERE userID = @userID", clavesha1;
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdUp = new SqlCommand(qryUp, conGaribay);
            clavesha1 = FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtPassword.Text, "SHA1");
            try
            {
                cmdUp.Parameters.Add("@Username", SqlDbType.NVarChar).Value = this.txtUsername.Text;
                cmdUp.Parameters.Add("@Password", SqlDbType.NVarChar).Value = clavesha1;
                cmdUp.Parameters.Add("@securitylevelID", SqlDbType.Int).Value = int.Parse(this.cmbLevelSecurity.SelectedValue);
                cmdUp.Parameters.Add("@enabled", SqlDbType.Bit).Value = this.chkActivo.Checked;
                cmdUp.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = this.txtNombre.Text;
                cmdUp.Parameters.Add("@email", SqlDbType.VarChar).Value = this.txtemail.Text;
                cmdUp.Parameters.Add("@userID", SqlDbType.Int).Value = int.Parse(gridUsers.SelectedDataKey[0].ToString());
                conGaribay.Open();
                int numregistros =  cmdUp.ExecuteNonQuery();

                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("USUARIOEXECUTEFAILED"), "MODIFICADO", "MODIFICARON", numregistros.ToString()));
                }
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("USUARIOMODIFIEDEXITO"), this.txtUsername.Text.ToUpper());
                if (this.txtUsername.Text.ToUpper().CompareTo(this.gridUsers.SelectedDataKey["username"].ToString().ToUpper()) != 0)
                {

                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.USUARIOS, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ EL NOMBRE DEL USUARIO DE: " + this.gridUsers.SelectedDataKey[1].ToString().ToUpper() + "A: " + this.txtUsername.Text.ToUpper()));
                }
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.USUARIOS, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ EL USUARIO: " + this.txtUsername.Text.ToUpper()));
               
                this.lblMensajeException.Text = ""; //NO HAY EXCEPTION;
                
                this.Agregando(false, true);
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
            }
            catch (InvalidOperationException exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("USUARIOMODIFIEDFAILED"), this.txtUsername.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
            }
            catch (SqlException exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("USUARIOMODIFIEDFAILED"), this.txtUsername.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;

            }
            catch(Exception exception){
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("USUARIOMODIFIEDFAILED"), this.txtUsername.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message; this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;

            }
            finally{
                conGaribay.Close();
            }


        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

            string qryDel = "DELETE FROM Users WHERE userID=@userID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdDel = new SqlCommand(qryDel, conGaribay);
                    
            try
            {
                cmdDel.Parameters.Add("@userID", SqlDbType.Int).Value = int.Parse(gridUsers.SelectedDataKey[0].ToString());
                
                conGaribay.Open();

                int numregistros = cmdDel.ExecuteNonQuery();

                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("USUARIOEXECUTEFAILED"), "ELIMINADO", "ELIMINARON", numregistros.ToString()));
                }
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("USUARIODELETEDEXITO"), gridUsers.SelectedDataKey["Nombre"].ToString().ToUpper());
                this.lblMensajeException.Text = ""; // NO HAY EXCEPTION
                
                btnModificarDeLista.Visible = false;
                btnEliminar.Visible = false;
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.USUARIOS, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ EL USUARIO: " + gridUsers.SelectedDataKey["Nombre"].ToString().ToUpper()));
                this.gridUsers.SelectedIndex = -1;
                this.limpiacampos();
                this.Agregando(false, true);
                this.gridUsers.DataBind();
                
            }
            catch (InvalidOperationException exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("USUARIODELETEDFAILED"), this.gridUsers.SelectedDataKey["nombre"].ToString().ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
            }
            catch (SqlException exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("USUARIODELETEDFAILED"), this.gridUsers.SelectedDataKey["Nombre"].ToString().ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;

            }
            catch (Exception exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("USUARIODELETEDFAILED"), this.gridUsers.SelectedDataKey["Nombre"].ToString().ToUpper());
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

        protected void btnPrintList_Click(object sender, EventArgs e)
        {
            this.gridUsers.AllowPaging = false;
            this.gridUsers.DataBind();
            float[] anchodecolumnas = new float[] { 10, 25, 25, 10, 30 };
            String pathArchivotemp;
            pathArchivotemp =  PdfCreator.printGridView("LISTA DE USUARIOS", gridUsers, anchodecolumnas,PdfCreator.orientacionPapel.VERTICAL,PdfCreator.tamañoPapel.CARTA);
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=listausuarios.pdf");
            Response.WriteFile(pathArchivotemp);
            Response.Flush();
            Response.End();
            this.gridUsers.AllowPaging = true;
            try
            {
                File.Delete(pathArchivotemp);
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "ERROR BORRANDO ARCHIVO TEMP DE CHEQUES", ref ex);
            }



        }

        protected void txtUsername_TextChanged(object sender, EventArgs e)
        {
            
        }

    }
}