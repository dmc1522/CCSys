using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Configuration;
using System.Globalization;
//using System.Web.UI.HtmlControls;


namespace Garibay
{
    public partial class frmRecoverPassword : System.Web.UI.Page
    {
        public Hashtable myQueryStrings;
        public string newpass;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.IsPostBack)
            {

                this.pnlenviaemail.Visible = false;
                this.pnlMensaje.Visible = false;
                this.btnredirec.Visible = false;
                this.btnIntentarDeNuevo.Visible = false;
                myQueryStrings = new Hashtable();
                if (this.Request.QueryString["data"] != null && Utils.loadqueryStrings(Request.QueryString["data"].ToString(), ref this.myQueryStrings))
                {


                    this.pnlenviaemail.Visible = false;
                    //si la direccion de correo está en la base de datos
                    if (validaemail())
                    {
                        // si el tiempo en que el correo es regresado menor a 10 minutos
                        if (tiempovalido())
                        {
                            //si se envia el nuevo pass
                            if (enviarnewpass())
                            {   //si se actualiza el pass

                                this.pnlMensaje.Visible = true;
                                this.lblMensaje.Text = "SE HA ENVIADO SU NUEVA CONTRASEÑA A SU CORREO ELECTRÓNICO";
                                this.btnredirec.Visible = true;
                                this.btnIntentarDeNuevo.Visible = false;
                                myQueryStrings.Clear();

                            }
                            else
                            {
                                this.pnlMensaje.Visible = true;
                                this.lblMensaje.Text = "NO SE HA PODIDO GENERAR TU NUEVA CONTRASEÑA";
                                this.btnIntentarDeNuevo.Visible = true;
                            }

                        }
                        else
                        {
                            this.pnlMensaje.Visible = true;
                            this.lblMensaje.Text = "TU TIEMPO PARA REACTIVAR TU NUEVA CONTRASEÑA HA CADUCADO";
                            this.btnIntentarDeNuevo.Visible = true;
                        }
                    }
                    else
                    {
                        this.pnlMensaje.Visible = true;
                        this.lblMensaje.Text = "TÚ CORREO ELECTRÓNICO NO EXISTE EN NUESTRA BASE DE DATOS";
                        this.btnIntentarDeNuevo.Visible = true;
                    }

                }
                else
                {
                    this.pnlenviaemail.Visible = true;
                    this.pnlMensaje.Visible = false;

                }


            }
            
        }


        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("noreply@corporativogaribay.com");
            correo.To.Add(this.txtemail.Text);
            correo.Subject = "Solicitud de nueva contraseña";
            correo.IsBodyHtml = true;
            correo.Body = HttpUtility.HtmlEncode("Este correo a sido enviado ya que se solicitó una nueva constraseña. Si usted no solicitó una nueva contraseña por favor haga caso omiso a este correo electronico.");
            correo.Body += "<br/>Para solicitar una Nueva Contrase" + HttpUtility.HtmlDecode("ñ") + "a haga Click en el siguiente link <br/> <a href=http://www.lasmargaritas.corporativogaribay.com/frmRecoverPassword.aspx?data=" + Utils.encriptacadena("email=" + this.txtemail.Text + "&timestamp=" + Utils.Now.ToString() + "&") + ">GENERAR UNA NUEVA CONTRASE" + HttpUtility.HtmlDecode("Ñ") + "A</a>";
            correo.Body += "<br/>Sitio WEB : <a href='www.lasmargaritas.corporativogaribay.com'>www.lasmargaritas.corporativogaribay.com</a>";

            SmtpClient smtp = new SmtpClient("localhost");
            
            try
            {
             
            smtp.Send(correo);
                //Logger.Instance.LogUserSessionRecord(Logger.typeModulo.RECOVERPASS, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("SE ENVIÓ EL CORREO DE RECORDAR CONTRASEÑA"));
            this.pnlMensaje.Visible = true;
            this.pnlenviaemail.Visible = false;

            this.lblMensaje.Text = "SE HA ENVIADO UN CORREO PARA SOLICITAR UNA NUEVA CONTRASEÑA, SI NO SE RESPONDE EL CORREO EN MENOS DE 10 MINUTOS LA PETICIÓN PARA GENERAR UNA NUEVA CONTRASEÑA SERÁ IGNORADA";
            this.btnredirec.Visible = true;
            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "btnEnviarClick", ref ex);
                this.pnlMensaje.Visible = true;
                this.pnlenviaemail.Visible = false;
                
                this.lblMensaje.Text = "NO SE PUDO ENVIAR EL CORREO";
                this.btnIntentarDeNuevo.Visible = true;
              //Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.LOGIN, 6,ex.Message, this.Request.Url.ToString());
            
            }

        }
        protected bool validaemail(){

            string qrySel = "SELECT * FROM Users  WHERE email = @email";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdSel = new SqlCommand(qrySel, conGaribay);

            try
            {


                cmdSel.Parameters.Add("@email", SqlDbType.NVarChar).Value = myQueryStrings["email"].ToString();
                conGaribay.Open();
                SqlDataReader sqldr = cmdSel.ExecuteReader();
                if (sqldr.HasRows)
                {
               
                    
                    conGaribay.Close();
             
                }
                return true;

            }
            /*
            catch (InvalidOperationException exception)
                        {
                            //Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.LOGIN,exception.Message , this.Request.Url.ToString());
                            return false;
                        }
                        catch (SqlException exception)
                        {
                            //Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.LOGIN,exception.Message , this.Request.Url.ToString());   
                            return false;
            
                        }*/
            
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "validaemail", ref ex);
                //Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.LOGIN,exception.Message , this.Request.Url.ToString());
                return false;

            }
            finally
            {
                conGaribay.Close();
            }

            return true;

        }
        protected bool actualizapass(){

            string qryUp = "UPDATE Users SET Password = @Password WHERE email = @email";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdUp = new SqlCommand(qryUp, conGaribay);
            
            try
            {
                newpass = GetRandomPasswordUsingGUID(8);

                cmdUp.Parameters.Add("@Password", SqlDbType.NVarChar).Value = FormsAuthentication.HashPasswordForStoringInConfigFile(newpass.ToUpper(), "SHA1");
                cmdUp.Parameters.Add("@email", SqlDbType.NVarChar).Value = myQueryStrings["email"].ToString();
                conGaribay.Open();
                int numregistros = cmdUp.ExecuteNonQuery();

                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("CHANGEPASSEXECUTEFAILED"), "MODIFICADO", "MODIFICARON", numregistros.ToString()));

                    
                }
                //Logger.Instance.LogUserSessionRecord(Logger.typeModulo.RECOVERPASS, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ EL PASS"));
                
                return true;
            }
            /*
            catch (InvalidOperationException exception)
                        {
                            //Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.LOGIN,exception.Message , this.Request.Url.ToString());
                            return false;
                        }
                        catch (SqlException exception)
                        {
                            //Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.LOGIN,exception.Message , this.Request.Url.ToString());   
                            return false;
            
                        }*/
            
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "actualizapass", ref ex);
                //Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.LOGIN,exception.Message , this.Request.Url.ToString());
                return false;

            }
            finally
            {
                conGaribay.Close();
            }
            return true;
    }
        protected string GetRandomPasswordUsingGUID(int length)
        {
            // Obtener el GUID
            string guidResult = System.Guid.NewGuid().ToString();

            // eliminar el guión
            guidResult = guidResult.Replace("-", string.Empty);

            // asegurar que la longuitud sea valida
            if (length <= 0 || length > guidResult.Length)
                throw new ArgumentException("La longuitud deberá ser entre 1 y " + guidResult.Length);

            // retorna la primera cadena (contraseña)
            return guidResult.Substring(0, length);
        }
        protected bool tiempovalido(){
            DateTime sendmailtime = Convert.ToDateTime(HttpUtility.HtmlDecode(myQueryStrings["timestamp"].ToString()));
           

                System.TimeSpan dif = Utils.Now - sendmailtime;
                if (dif.TotalMinutes > 10 ){

                    return false;
                }
                return true;

        }

        protected bool enviarnewpass(){


            if (actualizapass())
            {
                
                
                MailMessage correo2 = new MailMessage();
                correo2.From = new MailAddress("noreply@corporativogaribay.com");
                correo2.To.Add(myQueryStrings["email"].ToString());
                correo2.Subject = "Nueva de contraseña";
                correo2.IsBodyHtml = true;
                correo2.Body = "La nueva constraseña se ha generado:";
                correo2.Body += "<br/>Nueva contrase" + HttpUtility.HtmlDecode("ñ").ToString() + "a: " + newpass.ToUpper();
                correo2.Body += "<br/>Se recomienda la nueva contrase" + HttpUtility.HtmlDecode("ñ").ToString() + "a sea modificada a la brevedad para mayor seguridad: <br/>";
                correo2.Body += "<br/>Sitio WEB : <a href='www.lasmargaritas.corporativogaribay.com'>www.lasmargaritas.corporativogaribay.com</a>";


                SmtpClient smtp = new SmtpClient("localhost");

                try
                {

                    smtp.Send(correo2);

                    //Logger.Instance.LogUserSessionRecord(Logger.typeModulo.RECOVERPASS, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("SE ENVIÓ EL NEW PASS"));
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "enviarnewpass", ref ex);
                    //Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.LOGIN, ex.Message, this.Request.Url.ToString());

                    return false;
                }


                
            }
            else
            {
                this.pnlMensaje.Visible = true;
                this.lblMensaje.Text = "NO SE PUDO GENERAR TU NUEVA CONTRASEÑA";
                this.btnIntentarDeNuevo.Visible = true;
                this.btnredirec.Visible=false;
                return false;
            }

            return true;
        }

        protected void btnIntentarDeNuevo_Click(object sender, EventArgs e)
        {
            
            
            Response.Redirect("frmRecoverPassword.aspx");

            this.pnlenviaemail.Visible = true;
            this.pnlMensaje.Visible = false;
            
        }

        protected void btnredirec_Click(object sender, EventArgs e)
        {
            
            
            Response.Redirect("Default.aspx");
        }


    }
}
