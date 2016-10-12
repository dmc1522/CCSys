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
    public partial class frmAddModifyCheques : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                this.panelmensaje.Visible = false;
                if (Request.QueryString["data"] != null)
                {
                    if(this.loadqueryStrings(Request.QueryString["data"].ToString()))
                    {
                        this.lblHeader.Text = "MODIFICAR CHEQUE";
                        if(this.cargaDatosModify())
                        {
                            this.txtIdtomodify.Text = myQueryStrings["idtomodify"].ToString();
                        }else
                        {
                            this.txtIdtomodify.Text = "-1";
                        }
                        this.btnModificar.Visible = true;
                        this.btnAceptar.Visible = false;
                    }else
                    {
                        myQueryStrings.Clear();
                        Response.Redirect("~/frmAddModifyCheques.aspx", true);

                    }
                }
                else
                {
                    this.lblHeader.Text = "AGREGAR CHEQUE";
                    this.txtFecha.Text = Utils.getNowFormattedDateNormal();
                    this.btnAceptar.Visible = true;
                    this.btnModificar.Visible = false;
                }
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
        protected bool cargaDatosModify()
        {
            //string qrySel = "SELECT Cheques.chequeID, Cheques.cuentaID, Cheques.fecha, Cheques.monto, Cheques.nombre, Cheques.nombrequienrecibe, Cheques.chequestatusID WHERE Cheques.chequeID =";
            string qrySel = " SELECT Cheques.chequenumero, Cheques.cuentaID, Cheques.cicloID, Cheques.fecha, Cheques.monto, Cheques.nombre, Cheques.nombrequienrecibe FROM Cheques INNER JOIN Ciclos ON Cheques.cicloID = Ciclos.cicloID INNER JOIN CuentasDeBanco ON Cheques.cuentaID = CuentasDeBanco.cuentaID Where Cheques.chequeID=";
            qrySel += this.myQueryStrings["idtomodify"].ToString();
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdSel = new SqlCommand(qrySel, conGaribay);
            try
            {
                conGaribay.Open();
                SqlDataReader datostomodify;
                datostomodify = cmdSel.ExecuteReader();
                this.cmbCuenta.DataBind();
                this.cmbCiclo.DataBind();
                
                if(!datostomodify.HasRows)
                {
                    this.lblMensajeOperationresult.Text = myConfig.StrFromMessages("FALLOCARGARMODIFICAR");
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXCEPTION
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;
                    this.txtIDdetails.Text = "-1";
                    this.panelagregar.Visible = false;
                    return false;
                }
                while(datostomodify.Read())
                {
                    this.txtNumCheque.Text = datostomodify[0].ToString();
                    this.txtNumCheque.ReadOnly = true;
                    this.cmbCuenta.SelectedValue = datostomodify[1].ToString();
                    this.cmbCiclo.SelectedValue = datostomodify[2].ToString();
                    this.txtFecha.Text = Utils.converttoshortFormatfromdbFormat(datostomodify[3].ToString());
                    this.txtMonto.Text = datostomodify[4].ToString();
                    this.txtNombre.Text = datostomodify[5].ToString();
                    this.txtRecibe.Text = datostomodify[6].ToString();

                }
            }
            catch (InvalidOperationException exception)
            {
                this.lblMensajeOperationresult.Text = myConfig.StrFromMessages("FALLOCARGARMODIFICAR");
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.txtIDdetails.Text = "-1";
                this.panelagregar.Visible = false;

                return false;
            }
            catch (SqlException exception)
            {
                this.lblMensajeOperationresult.Text = myConfig.StrFromMessages("FALLOCARGARMODIFICAR");
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.txtIDdetails.Text = "-1";
                this.panelagregar.Visible = false;

                return false;

            }
            catch (Exception exception)
            {
                this.lblMensajeOperationresult.Text = myConfig.StrFromMessages("FALLOCARGARMODIFICAR");
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;

                this.txtIDdetails.Text = "-1";
                return false;

            }
            finally
            {
                conGaribay.Close();

            }
            return true;
        }

        protected void PopCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            this.txtFecha.Text = this.PopCalendar1.SelectedDate;
        }

        protected void btnAceptardetails_Click(object sender, EventArgs e)
        {
            if (this.txtIdtomodify.Text.Length > 0)
            {
                if (this.txtIdtomodify.Text == "-1")
                {
                    this.Response.Redirect("~/frmListCheques.aspx", true);
                }
            }
            this.panelmensaje.Visible = false;
            this.panelagregar.Visible = true;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string sqlQuery = "INSERT INTO Cheques (userID,cuentaID,chequenumero,fecha,monto,nombre,nombrequienrecibe,chequestatusID,storeTS,updateTS,cicloID) VALUES (@userID,@cuentaID,@chequenumero,@fecha,@monto,@nombre,@nombrequienrecibe,@chequestatusID,@storeTS,@updateTS,@cicloID)";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(sqlQuery, conGaribay);
            this.cmbCuenta.DataBind();
            this.cmbCiclo.DataBind();

            try
            {
                cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = int.Parse(this.Session["USERID"].ToString());
                cmdIns.Parameters.Add("@cuentaID", SqlDbType.Int).Value = int.Parse(this.cmbCuenta.SelectedValue);
                cmdIns.Parameters.Add("@chequenumero", SqlDbType.VarChar).Value = this.txtNumCheque.Text;
                cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(this.txtFecha.Text);
                cmdIns.Parameters.Add("@monto", SqlDbType.Float).Value = double.Parse(this.txtMonto.Text);
                cmdIns.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtNombre.Text;
                cmdIns.Parameters.Add("@nombrequienrecibe", SqlDbType.VarChar).Value = this.txtRecibe.Text;
                cmdIns.Parameters.Add("@chequestatusID", SqlDbType.Int).Value = 0;
                cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                cmdIns.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(cmbCiclo.SelectedValue);
                conGaribay.Open();
                int numregistros;
                numregistros = cmdIns.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("CHEQUEEXECUTEFAILED"), "AGREGADO", "AGREGARON", numregistros.ToString()));
                }
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CHEQUES, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("AGREGÓ EL CHEQUE NÚMERO: " + this.txtNumCheque.Text.ToString().ToUpper()));
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHEQUEADDEDEXITO"), this.txtNumCheque.Text);
                this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION        
                this.limpiacampos();
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                sqlQuery = "SELECT max(chequeID) FROM Cheques";
                int maximo;
                cmdIns.Parameters.Clear();
                cmdIns.CommandText = sqlQuery;
                maximo = (int)cmdIns.ExecuteScalar();
                this.txtIDdetails.Text = maximo.ToString();
                this.panelagregar.Visible = false;
            }
            catch (InvalidOperationException exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString()); 
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHEQUEADDEDFAILED"),this.txtNumCheque.Text);
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.txtIDdetails.Text = "-1";
                this.panelagregar.Visible = false;
            }
            catch (SqlException exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString()); 
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHEQUEADDEDFAILED"),this.txtNumCheque.Text);
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.txtIDdetails.Text = "-1";
                this.panelagregar.Visible = false;

            }
            catch (Exception exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString()); 
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHEQUEADDEDFAILED"), this.txtNumCheque.Text);
                this.lblMensajeException.Text = exception.Message;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                imagenmal.Visible = true;
                this.panelagregar.Visible = false;

            }
            finally
            {
                conGaribay.Close();
            }

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string sqlQuery = "UPDATE Cheques SET userID = @userID, cuentaID = @cuentaID, fecha = @fecha, monto = @monto, nombre = @nombre, nombrequienrecibe = @nombrequienrecibe, updateTS=@updateTS WHERE chequeID = @chequeID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdUp = new SqlCommand(sqlQuery, conGaribay);

            try
            {

                cmdUp.Parameters.Add("@userID", SqlDbType.Int).Value = int.Parse(this.Session["USERID"].ToString());
                cmdUp.Parameters.Add("@cuentaID", SqlDbType.Int).Value = int.Parse(this.cmbCuenta.SelectedValue);
                cmdUp.Parameters.Add("@fecha", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(this.txtFecha.Text);
                cmdUp.Parameters.Add("@monto", SqlDbType.Float).Value = double.Parse(this.txtMonto.Text);
                cmdUp.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtNombre.Text;
                cmdUp.Parameters.Add("@nombrequienrecibe", SqlDbType.VarChar).Value = this.txtRecibe.Text;
                cmdUp.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                cmdUp.Parameters.Add("@chequeID", SqlDbType.Int).Value = int.Parse(this.txtIdtomodify.Text);
                conGaribay.Open();
                int numregistros = cmdUp.ExecuteNonQuery();

                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("CHEQUEEXECUTEFAILED"), "MODIFICADO", "MODIFICARON", numregistros.ToString()));
                }

                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CHEQUES, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("MODIFICÓ EL CHEQUE NÚMERO: " + this.txtNumCheque.Text.ToString().ToUpper()));
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                this.imagenmal.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHEQUEMODIFIEDEXITO"), this.txtNumCheque.Text.ToUpper());
                this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION        
                this.txtIDdetails.Text = this.txtIdtomodify.Text;
                this.txtIdtomodify.Text = "-1";
                this.panelagregar.Visible = false;



            }
            catch (InvalidOperationException err)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err.Message, this.Request.Url.ToString()); 
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHEQUEMODIFIEDFAILED"), this.txtNumCheque.Text.ToUpper());
                this.lblMensajeException.Text = err.Message;

                this.panelagregar.Visible = false;

            }

            catch (SqlException err1)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString()); 
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHEQUEMODIFIEDFAILED"), this.txtNumCheque.Text.ToUpper());
                this.lblMensajeException.Text = err1.Message;

                this.panelagregar.Visible = false;

            }
            catch (Exception err2)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString()); 
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHEQUEMODIFIEDFAILED"), this.txtNumCheque.Text.ToUpper());
                this.lblMensajeException.Text = err2.Message;

                this.panelagregar.Visible = false;

            }
            finally
            {
                conGaribay.Close();

            }
        }

        protected void limpiacampos()
        {
            this.cmbCuenta.SelectedIndex = 0;
            this.cmbCiclo.SelectedIndex = 0;
            this.txtNombre.Text = "";
            this.txtRecibe.Text = "";
            this.txtMonto.Text = "";
            this.txtNumCheque.Text = "";
            this.txtFecha.Text = "";
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}
