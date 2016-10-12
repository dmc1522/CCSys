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


namespace Garibay
{
    public partial class frmAjustesCheques : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack){
                this.cmbBanco.DataBind();
                this.actualizaRegistros();
                this.cargaDatosPos(int.Parse(this.cmbBanco.SelectedValue));
                this.panelmensaje.Visible=false;
                this.paneldatos.Visible=true;
            }
            if(this.panelmensaje.Visible){
                this.panelmensaje.Visible=false;
                this.paneldatos.Visible=true;
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

        protected void cargaDatosPos(int bancoID){
            float fPosX = 0, fPosY = 0;
            HttpCookie cookie = Request.Cookies["PRINTCONF"];
            if (cookie == null)
            {
                fPosX = 0;
                fPosY = 0;
            }
            else
            {
                try
                {
                    float.TryParse(cookie[bancoID.ToString() + "posX"].ToString(), out fPosX);
                    float.TryParse(cookie[bancoID.ToString() + "posY"].ToString(), out fPosY);
                }
                catch(Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "CargaDatosPos", ref ex);    
                }
            }

            this.txtPosX.Text = fPosX.ToString();
            this.txtPosY.Text = fPosY.ToString();
        }

        protected void cmbBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cargaDatosPos(int.Parse(this.cmbBanco.SelectedValue));
            this.actualizaRegistros();
            
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string qryIns = "INSERT INTO PosCamposCheque (campoChequeID,bancoID,cuentaID,posX,posY) VALUES (@campoChequeID,@bancoID,@cuentaID,@posX,@posY)";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);


            try
            {


                this.creaCookie();

                string sqlSacaLastID = "select max(campoChequeID) from PosCamposCheque";
                SqlConnection conSacalastID = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdSacalastID = new SqlCommand(sqlSacaLastID, conSacalastID);
                conSacalastID.Open();
                int ID = 1;
                try
                {
                    object lastID = cmdSacalastID.ExecuteScalar();
                    ID = lastID != null ? int.Parse(lastID.ToString()) + 1 : 1;

                }
                catch (Exception ex)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.DEBUG, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL SACAR EL MAX ID DE LA TABLA POSCAMPOSCHEQUE. " + ex.Message, this.Request.Url.ToString());
                }
                finally
                {
                    conSacalastID.Close();
                }



                
                cmdIns.Parameters.Add("@campoChequeID", SqlDbType.Int).Value = ID;

                cmdIns.Parameters.Add("@bancoID", SqlDbType.Int).Value = ID;//TEMPORAL HASTA QUE SEA QUITADO DE LA BASE DE DATOS
                cmdIns.Parameters.Add("@cuentaID", SqlDbType.Int).Value = int.Parse(this.cmbBanco.SelectedValue);
                cmdIns.Parameters.Add("@posX", SqlDbType.Float).Value = int.Parse(this.txtPosX.Text);
                cmdIns.Parameters.Add("@posY", SqlDbType.Float).Value = int.Parse(this.txtPosY.Text);
                conGaribay.Open();
                int numregistros = cmdIns.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("CICLOEXECUTEFAILED"), "AGREGADO", "AGREGARON", numregistros.ToString()));
                }



                
                this.lblMensajeOperationresult.Text = "LOS DATOS PARA LA IMPRESIÓN DE CHEQUES DEL BANCO: " + this.cmbBanco.SelectedItem.Text.ToUpper() + ", HAN SIDO GUARDADOS.";
                this.lblMensajeException.Text = "";
                this.imagenbien.Visible = true;
                this.imagenmal.Visible = false;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CHEQUES, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("SE GUARDARON LOS DATOS DE IMPRESIÓN  DE LOS CHEQUES DE LA CUENTA: " + this.cmbBanco.SelectedItem.Text.ToUpper()));

            }
            catch (InvalidOperationException exc)
            {
                this.lblMensajetitle.Text = "ERROR";
                this.lblMensajeOperationresult.Text = "LOS DATOS PARA LA IMPRESIÓN DE CHEQUES DEL BANCO: " + this.cmbBanco.SelectedItem.Text + ", NO HAN SIDO GUARDADOS.";
                this.lblMensajeException.Text = exc.Message;

                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["userID"].ToString()), exc.Message , this.Request.Url.ToString());
                //Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["userID"].ToString()), "ERROR AL QUERER GUARDAR LOS DATOS DE LA IMPRESIÓN DE CHEQUES DEL BANCO: " + this.cmbBanco.SelectedItem.Text, this.Request.Url.ToString());


            }
            catch (SqlException exc)
            {
                this.lblMensajetitle.Text = "ERROR";
                this.lblMensajeOperationresult.Text = "LOS DATOS PARA LA IMPRESIÓN DE CHEQUES DEL BANCO: " + this.cmbBanco.SelectedItem.Text + ", NO HAN SIDO GUARDADOS.";
                this.lblMensajeException.Text = exc.Message;

                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["userID"].ToString()), exc.Message, this.Request.Url.ToString());
                //Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["userID"].ToString()), "ERROR AL QUERER GUARDAR LOS DATOS DE LA IMPRESIÓN DE CHEQUES DEL BANCO: " + this.cmbBanco.SelectedItem.Text, this.Request.Url.ToString());


            }
            catch (Exception exc)
            {
                this.lblMensajetitle.Text = "ERROR";
                this.lblMensajeOperationresult.Text = "LOS DATOS PARA LA IMPRESIÓN DE CHEQUES DEL BANCO: " + this.cmbBanco.SelectedItem.Text + ", NO HAN SIDO GUARDADOS.";
                this.lblMensajeException.Text = exc.Message;

                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["userID"].ToString()), exc.Message, this.Request.Url.ToString());
                //Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["userID"].ToString()), "ERROR AL QUERER GUARDAR LOS DATOS DE LA IMPRESIÓN DE CHEQUES DEL BANCO: " + this.cmbBanco.SelectedItem.Text, this.Request.Url.ToString());


            }
            finally
            {
                this.panelmensaje.Visible = true;
                this.paneldatos.Visible = false;
                conGaribay.Close();
            }
        }

        protected void creaCookie()
        {


            HttpCookie cookie = Request.Cookies["PRINTCONF"];
            if (cookie == null)
            {
                cookie = new HttpCookie("PRINTCONF");
            }

            cookie[this.cmbBanco.SelectedValue + "posX"] = txtPosX.Text;
            cookie[this.cmbBanco.SelectedValue + "posY"] = txtPosX.Text;

            cookie.Expires = Utils.Now.AddYears(1);
            Response.Cookies.Add(cookie);
             
     
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {

        }
        private void actualizaRegistros(){

            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                GridView1.DataBind();
                int max = dbFunctions.GetExecuteIntScalar("SELECT Count(*) FROM CamposCheque", 0);
                if (GridView1.Rows.Count < max)
                {
                   
                    String qryIns = "INSERT INTO PosCamposCheque (campoChequeID,bancoID,cuentaID,posX,posY) VALUES (@campoChequeID,@bancoID,@cuentaID,0,0)";
                    String qrySel = "SELECT CamposCheque.campoChequeID FROM CamposCheque INNER JOIN PosCamposCheque ON CamposCheque.campoChequeID = PosCamposCheque.campoChequeID WHERE PosCamposCheque.cuentaID = @cuentaID";
                    SqlCommand cmd = new SqlCommand(qrySel, conGaribay);
                    cmd.Parameters.Add("@cuentaID", SqlDbType.Int).Value = int.Parse(this.cmbBanco.SelectedValue);
                    SqlDataReader rd;
                    conGaribay.Open();
                    rd = cmd.ExecuteReader();
                    


                    ArrayList camposExistentes = new ArrayList();
                    while (rd.Read())
                    {
                        camposExistentes.Add(int.Parse(rd[0].ToString()));

                    }
                    rd.Close();


                    cmd = new SqlCommand(qryIns, conGaribay);
                    
                    for (int i = 1; i < max+1; i++)
                    {
                        if (!camposExistentes.Contains(i))
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add("@campoChequeID", SqlDbType.Int).Value = i;
                            cmd.Parameters.Add("@bancoID", SqlDbType.Int).Value = 1;
                            cmd.Parameters.Add("@cuentaID", SqlDbType.Int).Value = int.Parse(cmbBanco.SelectedValue);
                            int numregistros=cmd.ExecuteNonQuery();
                            if (numregistros != 1)
                            {
                                throw new Exception(string.Format(myConfig.StrFromMessages("CICLOEXECUTEFAILED"), "AGREGADO", "AGREGARON", numregistros.ToString()));
                            }

                            }
                    }

                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CHEQUES, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("SE INSERTARON LOS DATOS DE IMPRESIÓN  DE LOS CHEQUES CON VALORES EN 0 DE LA CUENTA: " + this.cmbBanco.SelectedItem.Text.ToUpper()));
                }

            }
            catch (InvalidOperationException exc)
            {
                
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["userID"].ToString()), exc.Message, this.Request.Url.ToString());
                //Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["userID"].ToString()), "ERROR AL QUERER GUARDAR LOS DATOS DE LA IMPRESIÓN DE CHEQUES DEL BANCO: " + this.cmbBanco.SelectedItem.Text, this.Request.Url.ToString());


            }
            catch (SqlException exc)
            {
                
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["userID"].ToString()), exc.Message, this.Request.Url.ToString());
                //Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["userID"].ToString()), "ERROR AL QUERER GUARDAR LOS DATOS DE LA IMPRESIÓN DE CHEQUES DEL BANCO: " + this.cmbBanco.SelectedItem.Text, this.Request.Url.ToString());


            }
            catch (Exception exc)
            {
                
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["userID"].ToString()), exc.Message, this.Request.Url.ToString());
                //Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["userID"].ToString()), "ERROR AL QUERER GUARDAR LOS DATOS DE LA IMPRESIÓN DE CHEQUES DEL BANCO: " + this.cmbBanco.SelectedItem.Text, this.Request.Url.ToString());


            }
            finally
            {
                
                conGaribay.Close();
            }
            GridView1.DataBind();



        }

    }
}
