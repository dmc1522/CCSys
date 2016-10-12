using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;

namespace Garibay
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                
                /*HttpCookie galleta;
                galleta = Response.Cookies["galleta"];
                if (galleta == null)
                {
                    galleta = new HttpCookie("galleta");
                }
                galleta["id"] = "";
                galleta.Expires = DateTime.Now;
                Response.Cookies.Add(galleta);*/
                this.pnlLogin.Visible = true;
                this.pnlWelcome.Visible = false;
            }
        }
        private bool validateUser()
        {
            bool bResult = false;
            string sSql = "SELECT userID, Nombre, securitylevelID, enabled FROM Users WHERE username = @Username AND password = @Password;";
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand sqlComm = new SqlCommand(sSql,sqlConn);
                sqlComm.Parameters.Add("@Username", SqlDbType.VarChar).Value = this.txtUsuario.Text;
                sqlComm.Parameters.Add("@Password", SqlDbType.VarChar).Value = FormsAuthentication.HashPasswordForStoringInConfigFile(this.txtContrasena0.Text, "SHA1");

                sqlConn.Open();
                SqlDataReader dr = sqlComm.ExecuteReader();
                
                if (dr.HasRows && dr.Read() && dr.GetSqlBoolean(3))
                {
                    this.Session["USUARIO"]     = dr["Nombre"].ToString();
                    this.Session["USERID"]      = dr["userID"].ToString();
                    this.Session["SECURITYID"]  = dr["securitylevelID"].ToString();
                    this.Session["SISTEMABANCO"] = this.chkSistemaBanco.Checked;
                    bResult = true;
                }
                
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "validateUser", ref ex);
            }
            finally
            {
                sqlConn.Close();
            }
            return bResult;
        }

        private void SaveSessionToCookie()
        {
            HttpCookie galleta = null;
            //galleta = Response.Cookies["galleta"];
            if (galleta == null)
            {
                galleta = new HttpCookie("galleta");
            }
            String cookiesession = "SISTEMABANCO=";
            cookiesession += this.Session["SISTEMABANCO"].ToString();
            cookiesession += "&USERID=" + this.Session["USERID"].ToString();
            cookiesession += "&USUARIO=" + this.Session["USUARIO"].ToString();
            cookiesession += "&SECURITYID=" + this.Session["SECURITYID"].ToString();
            cookiesession += "&date=" + DateTime.Now.AddHours(4).ToString();

            galleta.Value = Utils.encriptacadena(cookiesession);
            this.Session["id"] = galleta.Value;
            galleta.Expires = DateTime.Now.AddHours(4);
            Response.Cookies.Add(galleta);
        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            bool validated = false;
            DateTime lastTry = DateTime.MinValue;
            string user = null;
            int ntries = 0;
            string ip = null;

            string qryIns = "INSERT INTO loginRecords VALUES (@datestamp, @username, @trynum, @IPAddress)";
            string qrySel = "SELECT TOP 1 * FROM loginRecords WHERE username = @username ORDER BY loginRecordID DESC";
            SqlConnection sqlCon = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand cmdSel = new SqlCommand(qrySel, sqlCon);
                cmdSel.Parameters.Add("@username", SqlDbType.NVarChar).Value = this.txtUsuario.Text;
                sqlCon.Open();
                SqlDataReader rdr = cmdSel.ExecuteReader();
                if (rdr.HasRows && rdr.Read())
                {
                    lastTry = (DateTime)rdr["datestamp"];
                    user = rdr["username"].ToString();
                    ntries = (int)rdr["trynum"];
                    ip = rdr["IPAddress"].ToString();
                }
                sqlCon.Close();
                System.TimeSpan dif = Utils.Now - lastTry;
                if (dif.TotalMinutes > 10 || (dif.TotalMinutes < 10 && ntries < 5))
                {
                    this.lblMsjIntentos.Visible = false;
                    if (validated = this.validateUser())
                    {
                        ntries = 0;
                        Logger.Instance.LogUserSessionRecord(Logger.typeModulo.LOGIN, Logger.typeUserActions.LOGIN, int.Parse(this.Session["USERID"].ToString()), "EL USUARIO: \"" +this.txtUsuario.Text+"\" SE VALIDÓ SATISFACTORIAMENTE");
                    }
                    else
                    {
                        this.lblLoginResult.Text = myConfig.StrFromMessages("LOGONERR");
                        this.lblMensaje.Text = myConfig.StrFromMessages("AUTHFAILED");
                        this.panelMensaje.Visible = true;
                        if (dif.TotalMinutes > 10)
                        {
                            ntries = 1;
                        }
                        else
                        {
                            ntries++;
                        }
                    }
                    SqlCommand cmdIns = new SqlCommand(qryIns, sqlCon);
                    cmdIns.Parameters.Add("@datestamp", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(Utils.Now.ToString());
                    cmdIns.Parameters.Add("@username", SqlDbType.NVarChar).Value = this.txtUsuario.Text;
                    cmdIns.Parameters.Add("@trynum", SqlDbType.Int).Value = ntries;
                    cmdIns.Parameters.Add("@IPAddress", SqlDbType.NVarChar).Value = this.Request.UserHostAddress;
                    sqlCon.Open();
                    cmdIns.ExecuteNonQuery();
                }
                else
                {
                    ntries = 0;
                    System.TimeSpan esp = lastTry.AddMinutes(10) - Utils.Now;
                    this.lblMsjIntentos.Visible = true;
                    this.lblMsjIntentos.Text = string.Format(myConfig.StrFromMessages("USERBLOCKED"), this.txtUsuario.Text.ToUpper(), esp.Minutes.ToString(), esp.Seconds.ToString());
                    this.lblLoginResult.Text = "";
                    this.lblMensaje.Text = "";
                    this.panelMensaje.Visible = true;

                }
            }
            catch (Exception err3)
            {
                this.lblMsjIntentos.Text = err3.Message;
                this.panelMensaje.Visible = true;
            }
            finally
            {
                sqlCon.Close();
            }
           
            if (validated)
            {

                this.Session["SISTEMABANCO"] = this.chkSistemaBanco.Checked;
                if (this.Session["BACKPAGE"] != null && this.Session["BACKPAGE"].ToString().Length > 0)
                {
                    String urlReferred = this.Session["BACKPAGE"].ToString();
                    this.Session["BACKPAGE"] = "";
                    Response.Redirect(urlReferred);
                }
                this.SaveSessionToCookie();
                Response.Redirect("frmAtGlance.aspx");
                //this.pnlLogin.Visible = false;
                //this.pnlWelcome.Visible = true;
            }
            else
            {
                //this.lblLoginResult.Text = myConfig.StrFromMessages("LOGONERR");
                //this.lblMensaje.Text = myConfig.StrFromMessages("AUTHFAILED");
                //this.panelMensaje.Visible = true;
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("www.corporativogaribay.com");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.txtUsuario.Text = this.txtUsuarioMargaritas.Text;
            this.txtContrasena0.Text = this.txtPassMargaritas.Text;
            this.chkSistemaBanco.Checked = true;
            this.btnEntrar_Click(null, null);
            this.txtUsuario.Text = "";
            this.txtContrasena0.Text = "";
            this.chkSistemaBanco.Checked = false;
        }
    }
}
