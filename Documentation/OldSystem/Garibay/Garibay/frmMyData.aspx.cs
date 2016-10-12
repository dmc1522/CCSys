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

using System.IO;


namespace Garibay
{
    public partial class frmMyData : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack){
                
                if(this.Session["USERID"]!=null){
                    cargadatos();

                }
                try
                {

                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MICUENTA, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "EL USUARIO VISITÓ LA PÁGINA DE SU CUENTA");
                }
                catch (Exception exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());

                }
            }
            if (this.panelmensaje.Visible == true)
            {
                this.panelmensaje.Visible = false;

            }
            this.compruebasecurityLevel();

        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
            }

        }

        protected void btnAceptarPass_Click(object sender, EventArgs e)
        {
            
            if (escorrecto())
            {

                string qryUp = "UPDATE Users SET Password = @Password WHERE userID = @userID", clavesha1;
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdUp = new SqlCommand(qryUp, conGaribay);
                clavesha1 = FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtNewPassword.Text, "SHA1");
                try
                {
                    
                    cmdUp.Parameters.Add("@Password", SqlDbType.NVarChar).Value = clavesha1;
                    cmdUp.Parameters.Add("@userID", SqlDbType.Int).Value = int.Parse(this.Session["USERID"].ToString());
                    conGaribay.Open();
                    int numregistros = cmdUp.ExecuteNonQuery();

                    if (numregistros != 1)
                    {
                        throw new Exception(string.Format(myConfig.StrFromMessages("CHANGEPASSEXECUTEFAILED"), "MODIFICADO", "MODIFICARON", numregistros.ToString()));
                    }
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHANGEPASSMODIFIEDEXITO"), this.Session["USUARIO"].ToString().ToUpper());

                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MICUENTA, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), "EL USUARIO " + this.Session["USUARIO"].ToString().ToUpper() +" MODIFICÓ SU PASSWORD");

                    this.lblMensajeException.Text = ""; //NO HAY EXCEPTION;

                    //this.Agregando(false, true);
                    this.imagenmal.Visible = false;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = true;
                }
                catch (InvalidOperationException exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHANGEPASSMODIFIEDFAILED"), this.Session["USERID"].ToString());
                    this.lblMensajeException.Text = exception.Message;
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;
                }
                catch (SqlException exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHANGEPASSMODIFIEDFAILED"), this.Session["USERID"].ToString().ToUpper());
                    this.lblMensajeException.Text = exception.Message;
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;

                }
                catch (Exception exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHANGEPASSMODIFIEDFAILED"), this.Session["USUARIO"].ToString().ToUpper());
                    this.lblMensajeException.Text = exception.Message; this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;

                }
                finally
                {
                    conGaribay.Close();
                }
            }
            else{
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), "El USUARIO NO INGRESO SU CONTRASEÑA CORRECTAMENTE", this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHANGEPASSMODIFIEDFAILED"), this.Session["USUARIO"].ToString().ToUpper());
                this.lblMensajeException.Text = "CONTRASEÑA NO VÁLIDA"; 
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
            }
       }

       protected bool escorrecto() {
           string qrySel = "SELECT * FROM Users  WHERE userID = @userID and Password = @Password ", clavesha1;
           SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
           SqlCommand cmdSel = new SqlCommand(qrySel, conGaribay);



           try
           {
               clavesha1 = FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtpassword.Text, "SHA1");
               cmdSel.Parameters.Add("@Password", SqlDbType.NVarChar).Value = clavesha1;
               cmdSel.Parameters.Add("@userID", SqlDbType.Int).Value = this.Session["USERID"].ToString();
               conGaribay.Open();
               SqlDataReader sqldr=cmdSel.ExecuteReader();
               if(sqldr.HasRows){
                   conGaribay.Close();
                   return true;
               }
            
           }
           catch {
               return false;
           }
           
           return false;
        }


       protected void cargadatos(){

           string qrySel = "SELECT  Nombre,email FROM Users  WHERE userID = "+this.Session["USERID"].ToString();
           SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
           SqlCommand cmdSel = new SqlCommand(qrySel, conGaribay);
           try
           {
           
               conGaribay.Open();
               SqlDataReader sqldr = cmdSel.ExecuteReader();
               sqldr.Read();
               this.txtNombre.Text = sqldr[0].ToString();
               this.txtemail.Text = sqldr[1].ToString();
                   conGaribay.Close();
                   
               

           }
           catch
           {
           
           }
           
       }

       protected void btnAceptarDatos_Click(object sender, EventArgs e)
       {
           string qryUp = "UPDATE Users SET Nombre = @Nombre, email = @email WHERE userID=@userID", clavesha1;
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdUp = new SqlCommand(qryUp, conGaribay);
                
                try
                {
                    
                    cmdUp.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = this.txtNombre.Text;
                    cmdUp.Parameters.Add("@email", SqlDbType.NVarChar).Value = this.txtemail.Text;
                    cmdUp.Parameters.Add("@userID", SqlDbType.Int).Value = int.Parse(this.Session["USERID"].ToString());
                    conGaribay.Open();
                    int numregistros = cmdUp.ExecuteNonQuery();

                    if (numregistros != 1)
                    {
                        throw new Exception(string.Format(myConfig.StrFromMessages("CHANGEDATAEXECUTEFAILED"),"MODIFICARON", numregistros.ToString()));
                    }
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHANGEDATAMODIFIEDEXITO"), this.Session["USUARIO"].ToString().ToUpper());

                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MICUENTA, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), "EL USUARIO" + this.Session["USUARIO"].ToString().ToUpper() +" MODIFICÓ SUS DATOS");

                    this.lblMensajeException.Text = ""; //NO HAY EXCEPTION;

                    //this.Agregando(false, true);
                    this.imagenmal.Visible = false;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = true;
                }
                catch (InvalidOperationException exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHANGEDATAMODIFIEDFAILED"), this.Session["USERID"].ToString());
                    this.lblMensajeException.Text = exception.Message;
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;
                }
                catch (SqlException exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHANGEDATAMODIFIEDFAILED"), this.Session["USERID"].ToString().ToUpper());
                    this.lblMensajeException.Text = exception.Message;
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;

                }
                catch (Exception exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHANGEDATAMODIFIEDFAILED"), this.Session["USUARIO"].ToString().ToUpper());
                    this.lblMensajeException.Text = exception.Message; this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;

                }
                finally
                {
                    conGaribay.Close();
                }
            }

       protected void btnCancelar_Click(object sender, EventArgs e)
       {
           
       }

       
       }

    }

