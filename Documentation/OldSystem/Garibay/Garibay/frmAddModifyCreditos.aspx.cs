using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;



namespace Garibay
{
    public partial class frmAddModifyCreditos : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.panelmensaje.Visible = false;
                this.dprdlStatus.DataBind();
                this.cmbZona.DataBind();
                this.cmbCiclo.DataBind();
                this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");

                if (Request.QueryString["data"] != null)
                {
                    if (this.loadqueryStrings(Request.QueryString["data"].ToString()))
                    {
                        this.lblTitle.Text = "MODIFICAR UN CRÉDITO";

                        if (this.cargadatosmodify())
                        {
                            this.txtIDdetails.Text = myQueryStrings["idtomodify"].ToString();
                        }
                        else
                        {
                            this.txtIDdetails.Text = "-1";

                        }
                        this.btnModificar.Visible = true;
                        this.btnAceptar.Visible = false;
                    }
                    else
                    {
                        myQueryStrings.Clear();
                        Response.Redirect("~/frmAddModifyCreditos.aspx", true);

                    }
                }
                else
                {

                    this.lblTitle.Text = "AGREGAR NUEVO CRÉDITO";
                    this.btnAceptar.Visible = true;
                    this.btnModificar.Visible = false;
                    
                }
                this.cmbZona_SelectedIndexChanged(null, null);

            }
            this.cmbStatus.Enabled = false;
            if(this.panelmensaje.Visible){
                this.panelagregar.Visible = true;
                this.panelmensaje.Visible = false;
                this.txtIDdetails.Text = "-1";
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

    
        void limpiacampos(){
            this.cmbCiclo.SelectedIndex = 0;
            this.cmbStatus.SelectedIndex = 0;
            this.cmbZona.SelectedIndex = 0;
            this.cmbProductor.SelectedIndex = 0;
            this.txtInteresAnual.Text = "0.00";
            this.txtLimite.Text = "0.00";
          


        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string sqlQuery = "INSERT INTO Creditos (productorID, cicloID, Fecha, Zona, FechaFinCiclo, InteresAnual, userID, storeTS, updateTS, statusID, LimitedeCredito) VALUES (@productorID,@cicloID,@Fecha,@Zona,@FechaFinCiclo,@InteresAnual,@userID,@storeTS,@updateTS,@statusID, @limitedecredito)";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(sqlQuery, conGaribay);

            try
            {


                cmdIns.Parameters.Add("@productorID", SqlDbType.Int).Value = this.cmbProductor.SelectedValue;
                cmdIns.Parameters.Add("@cicloID", SqlDbType.NVarChar).Value = this.cmbCiclo.SelectedValue;
                cmdIns.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(this.txtFecha.Text);
                cmdIns.Parameters.Add("@Zona", SqlDbType.Int).Value = this.cmbZona.SelectedIndex + 1;
                SqlConnection conGaribaytemp = new SqlConnection(myConfig.ConnectionInfo);
                string sqlQuerysel = "";
                if (this.cmbZona.SelectedIndex == 0)
                    sqlQuerysel = "Select FechaFinZona1 from Ciclos where cicloID = @cicloID";
                else
                    sqlQuerysel = "Select FechaFinZona2 from Ciclos where cicloID = @cicloID";
                SqlCommand cmdSel = new SqlCommand(sqlQuerysel, conGaribaytemp);
                cmdSel.Parameters.Add("@cicloID", SqlDbType.Int).Value = this.cmbCiclo.SelectedValue;
                conGaribaytemp.Open();
                string fecha = cmdSel.ExecuteScalar().ToString();
                cmdIns.Parameters.Add("@FechaFinCiclo", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(fecha);
                conGaribaytemp.Close();
                double dValoraux;
                dValoraux = 0.00; double.TryParse(this.txtInteresAnual.Text, out dValoraux);
                cmdIns.Parameters.Add("@InteresAnual", SqlDbType.Float).Value = dValoraux/100;
                cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = int.Parse(this.Session["USERID"].ToString());
                cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                cmdIns.Parameters.Add("@statusID", SqlDbType.Int).Value = this.cmbStatus.SelectedValue;
                dValoraux = 0.00; double.TryParse(this.txtLimite.Text, out dValoraux);
                cmdIns.Parameters.Add("@limitedecredito", SqlDbType.Int).Value = double.Parse(this.txtLimite.Text);
                conGaribay.Open();
                int numregistros;
                numregistros = cmdIns.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("CREDITOEXECUTEFAILED"), "AGREGADO", "AGREGARON", numregistros.ToString()));
                }
                sqlQuery = "SELECT max(creditoID) FROM Creditos";
                int maximo;
                cmdIns.Parameters.Clear();
                cmdIns.CommandText = sqlQuery;
                maximo = (int)cmdIns.ExecuteScalar();
                this.txtIDdetails.Text = maximo.ToString();
                this.panelagregar.Visible = false;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CREDITOS, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("AGREGÓ EL CREDITO: " + this.txtIDdetails.Text.ToUpper()));
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CREDITOADDEDEXITO"), txtIDdetails.Text.ToUpper());
                this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION        
                this.limpiacampos();
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;

            }
            catch (Exception exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CREDITOADDEDFAILED"));
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.txtIDdetails.Text = "-1";
                this.panelagregar.Visible = false;

            }
            finally
            {

                conGaribay.Close();

            }
        }

        protected void DetailsView1_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {

        }
        protected bool cargadatosmodify()
        {
            string qryIns = "SELECT Creditos.productorID, Creditos.Fecha, Creditos.Interesanual, Creditos.statusID, Creditos.zona, Creditos.cicloID, Creditos.LimitedeCredito FROM Creditos where creditoID = @creditoID ";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            try
            {
                cmdIns.Parameters.Add("@creditoID", SqlDbType.Int).Value = int.Parse(this.myQueryStrings["idtomodify"].ToString());
                conGaribay.Open();
                SqlDataReader datostomodify;
                datostomodify = cmdIns.ExecuteReader();
                this.cmbCiclo.DataBind();
                this.cmbProductor.DataBind();
                this.cmbStatus.DataBind();
                this.cmbZona.DataBind();
                if (!datostomodify.HasRows)
                { //EL ID NO ES VALIDO
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

                while (datostomodify.Read())
                {
                    this.cmbProductor.SelectedValue = datostomodify[0].ToString();
                    this.txtFecha.Text = Utils.converttoshortFormatfromdbFormat(datostomodify[1].ToString());
                    float interes;
                    interes = float.Parse(datostomodify[2].ToString())*100;
                    this.txtInteresAnual.Text = interes.ToString() ;
                    this.cmbStatus.SelectedValue = datostomodify[3].ToString();
                    this.cmbZona.SelectedValue = datostomodify[4].ToString();
                    this.cmbCiclo.SelectedValue = datostomodify[5].ToString();
                    double dValoraux;
                    dValoraux = 0.00; double.TryParse(datostomodify["LimitedeCredito"].ToString(), out dValoraux);
                    this.txtLimite.Text = dValoraux.ToString();
                    
                   
                }
                this.cmbProductor.Enabled = false;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CREDITOS, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), ("SE CARGÓ EL CREDITO: " + int.Parse(this.myQueryStrings["idtomodify"].ToString())+" PARA MODIFICAR"));
                
            }
            catch (Exception exception)
            {
                this.lblMensajeOperationresult.Text = myConfig.StrFromMessages("FALLOCARGARMODIFICAR");
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeException.Text = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.txtIDdetails.Text = "-1";
                this.panelagregar.Visible = false;

                return false;
            }
            return true;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string sqlQuery = "Update Creditos set productorID = @productorID, cicloID = @cicloID, Fecha = @Fecha, Zona = @Zona, FechaFinCiclo =@FechaFinCiclo, InteresAnual = @InteresAnual,  " +
                               " userID = @userID, updateTS = @updateTS, statusID = @statusID, LimitedeCredito = @limitedecredito " +
                               " where creditoID = @creditoID ";

            
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(sqlQuery, conGaribay);

            try
            {


                cmdIns.Parameters.Add("@productorID", SqlDbType.Int).Value = this.cmbProductor.SelectedValue;
                cmdIns.Parameters.Add("@cicloID", SqlDbType.NVarChar).Value = this.cmbCiclo.SelectedValue;
                cmdIns.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(this.txtFecha.Text);
                cmdIns.Parameters.Add("@Zona", SqlDbType.Int).Value = this.cmbZona.SelectedIndex + 1;
                SqlConnection conGaribaytemp = new SqlConnection(myConfig.ConnectionInfo);
                string sqlQuerysel = "";
                if (this.cmbZona.SelectedIndex == 0)
                    sqlQuerysel = "Select FechaFinZona1 from Ciclos where cicloID = @cicloID";
                else
                    sqlQuerysel = "Select FechaFinZona2 from Ciclos where cicloID = @cicloID";
                SqlCommand cmdSel = new SqlCommand(sqlQuerysel, conGaribaytemp);
                cmdSel.Parameters.Add("@cicloID", SqlDbType.Int).Value = this.cmbCiclo.SelectedValue;
                conGaribaytemp.Open();
                string fecha = cmdSel.ExecuteScalar().ToString();
                cmdIns.Parameters.Add("@FechaFinCiclo", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(fecha);
                conGaribaytemp.Close();
                double dValoraux;
                dValoraux = 0.00; double.TryParse(this.txtInteresAnual.Text, out dValoraux);
                cmdIns.Parameters.Add("@InteresAnual", SqlDbType.Float).Value = dValoraux / 100;
                cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = int.Parse(this.Session["USERID"].ToString());
               
                cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                cmdIns.Parameters.Add("@statusID", SqlDbType.Int).Value = dprdlStatus.SelectedValue;
                dValoraux = 0.00; double.TryParse(this.txtLimite.Text, out dValoraux);
                cmdIns.Parameters.Add("@limitedecredito", SqlDbType.Int).Value = double.Parse(this.txtLimite.Text);
                cmdIns.Parameters.Add("@creditoID", int.Parse(this.txtIDdetails.Text));
                conGaribay.Open();
                int numregistros;
                numregistros = cmdIns.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("CREDITOEXECUTEFAILED"), "MODIFICADO", "MODIFICARON", numregistros.ToString()));
                }




                this.panelagregar.Visible = false;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CREDITOS, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("MODIFICÓ EL CREDITO: " + this.txtIDdetails.Text.ToUpper()));
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CREDITOMODIFIEDEXITO"), txtIDdetails.Text.ToUpper());
                this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION        
                this.limpiacampos();
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                cmdIns.CommandText="UPDATE Solicitudes SET   statusID = @statusID WHERE     (creditoID = @creditoID)";
                cmdIns.Parameters.Clear();
                cmdIns.Parameters.Add("statusID", SqlDbType.Int).Value = this.dprdlStatus.SelectedValue;
                cmdIns.Parameters.Add("creditoID", SqlDbType.Int).Value = int.Parse(this.txtIDdetails.Text);
                try
                {
                    cmdIns.ExecuteNonQuery();
                }catch(Exception ex){


                }





            }
            catch (Exception exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CREDITOMODIFIEDFAILED"));
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.txtIDdetails.Text = "-1";
                this.panelagregar.Visible = false;

            }
            finally
            {

                conGaribay.Close();

            }

        }

        protected void btnAceptardetails_Click(object sender, EventArgs e)
        {
            if(this.lblTitle.Text.CompareTo("MODIFICAR UN CRÉDITO")==0){//SE ESTABA MODIFICANDO
                Response.Redirect("~/frmListofCreditos.aspx", true);
            }
            
        }

        protected void cmbZona_SelectedIndexChanged(object sender, EventArgs e)
        {

            string sql = "Select ";
            sql += this.cmbZona.SelectedItem.Text == "ZONA 1" ? " fechaFinZona1 " : "fechaFinZona2";
            sql += " FROM Ciclos where cicloID = @cicloID";
            SqlConnection sqlConSacaFecha = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand selFecha = new SqlCommand(sql, sqlConSacaFecha);
            sqlConSacaFecha.Open();
            try
            {
                selFecha.Parameters.Add("@cicloID", SqlDbType.Int).Value = this.cmbCiclo.SelectedValue;
                DateTime fechaFin;
                if (!DateTime.TryParse(selFecha.ExecuteScalar().ToString(), out fechaFin))
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, this.UserID, "ERROR AL SACAR FECHA DE ZONA", this.Request.Url.ToString());
                    return;
                }
                this.txtFechaFinCredito.Text = fechaFin.ToString("dd/MM/yyyy");
               




            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "ERROR AL SELECCIONAR FECHA DE ZONA EN CREDITO", ref ex);
            }
        }

        protected void cmbCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbZona_SelectedIndexChanged(null, null);
        }

      

      

       
       
    }
}
