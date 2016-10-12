using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Garibay
{
    public partial class frmAddQuickProductor : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack){
                this.btnAceptar.Visible = false;
                this.panelmensaje.Visible = false;
                this.ddlEstado.DataBind();
                this.ddlEstado.SelectedValue = "14";


                
                this.btnValidar.Visible = true;
                string msgConf = "return alert('¡¡NO OLVIDE ACTUALIZAR LA LISTA DE PRODUCTORES !! ') ";
                btnSalir.Attributes.Add("onclick", msgConf);
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PRODUCTORES, Logger.typeUserActions.SELECT, this.UserID, "VISITÓ LA PÁGINA PARA AGREGAR PRODUCTOR RÁPIDO");
            }
            
            if(this.panelmensaje.Visible){
                this.panelmensaje.Visible = false;
                this.btnValidar.Visible = true;
                this.btnAceptar.Visible = false;
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

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            SqlConnection checkConn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand checkComm = new SqlCommand();
            checkComm.Connection = checkConn;
            try
            {
                checkConn.Open();
                //SELECT [apaterno], [amaterno], [nombre] FROM [Productores] WHERE (([apaterno] LIKE '%' + @apaterno + '%') AND ([amaterno] LIKE '%' + @amaterno + '%') AND ([nombre] LIKE '%' + @nombre + '%')) ORDER BY [apaterno], [amaterno], [nombre]
                checkComm.CommandText = "SELECT [apaterno], [amaterno], [nombre], [codigoBoletasFile] FROM [Productores] WHERE (([apaterno] LIKE '%' + @apaterno + '%') OR ([amaterno] LIKE '%' + @amaterno + '%') OR ([nombre] LIKE '%' + @nombre + '%')) ORDER BY [apaterno], [amaterno], [nombre]";
                checkComm.Parameters.Add("@apaterno", SqlDbType.VarChar).Value = this.txtPaterno.Text;
                checkComm.Parameters.Add("@amaterno", SqlDbType.VarChar).Value = this.txtMaterno.Text;
                checkComm.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtNombres.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(checkComm);
                da.Fill(dt);

                //check if there is a productor with the same name:
                checkComm.CommandText = "SELECT count(*) FROM [Productores] WHERE (([apaterno] LIKE '%' + @apaterno + '%') AND ([amaterno] LIKE '%' + @amaterno + '%') AND ([nombre] LIKE '%' + @nombre + '%'))";
                checkComm.Parameters.Clear();
                checkComm.Parameters.Add("@apaterno", SqlDbType.VarChar).Value = this.txtPaterno.Text;
                checkComm.Parameters.Add("@amaterno", SqlDbType.VarChar).Value = this.txtMaterno.Text;
                checkComm.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtNombres.Text;

                Boolean bYaExiste = (int.Parse(checkComm.ExecuteScalar().ToString()) > 0);
                if (dt.Rows.Count > 0)
                {
                    this.lblProductoresParecidos.Visible = this.GridView1.Visible = true;
                    this.GridView1.DataSourceID = "";
                    this.GridView1.DataSource = dt;
                    this.GridView1.DataBind();
                }

                if (bYaExiste)
                {
                    this.btnAceptar.Visible = false;
                  
                }
                
                this.lblProductorYaExiste.Visible = bYaExiste;
                this.btnValidar.Visible = bYaExiste;
                this.btnAceptar.Visible = !bYaExiste;

            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, "Error validando productor", this.Request.Url.ToString(), ref ex);
            }
            finally
            {
                checkConn.Close();
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string sqlQuery = "INSERT INTO Productores (apaterno, amaterno, nombre,domicilio, poblacion, municipio, estadoID, telefono, celular, codigoBoletasFile) VALUES(@apaterno, @amaterno, @nombre, @domicilio, @poblacion, @municipio, @estadoID, @telefono, @celular, @codigoBoletasFile,@colonia)";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(sqlQuery, conGaribay);
            conGaribay.Open();
            try
            {


                cmdIns.Parameters.Add("@apaterno", SqlDbType.NVarChar).Value = this.txtPaterno.Text;
                cmdIns.Parameters.Add("@amaterno", SqlDbType.NVarChar).Value = this.txtMaterno.Text;
                cmdIns.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = this.txtNombres.Text;
                cmdIns.Parameters.Add("@codigoBoletasFile", SqlDbType.NVarChar).Value = this.txtCodeBoletas.Text;
                cmdIns.Parameters.Add("@domicilio", SqlDbType.NVarChar).Value = this.txtDomicilio.Text;
                cmdIns.Parameters.Add("@poblacion", SqlDbType.NVarChar).Value = this.txtPoblacion.Text;
                cmdIns.Parameters.Add("@municipio", SqlDbType.NVarChar).Value = this.txtMunicipio.Text;
                cmdIns.Parameters.Add("@estadoID", SqlDbType.NVarChar).Value = int.Parse(this.ddlEstado.SelectedValue);
                cmdIns.Parameters.Add("@telefono", SqlDbType.NVarChar).Value =this.txtTelefono.Text;
                cmdIns.Parameters.Add("@celular", SqlDbType.NVarChar).Value = this.txtCelular.Text;
                cmdIns.Parameters.Add("@colonia", SqlDbType.NVarChar).Value = this.txtColonia.Text;
                

                int numregistros;
                numregistros = cmdIns.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("PRODUCTOREXECUTEFAILED"), "AGREGADO", "AGREGARON", numregistros.ToString()));
                }
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PRODUCTORES, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("AGREGÓ EL PRODUCTOR RÁPIDO: " + this.txtPaterno.Text.ToUpper() + " " + this.txtMaterno.Text.ToUpper() + " " + this.txtNombres.Text.ToUpper()));
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTORADDEDEXITO"), txtPaterno.Text.ToUpper(), txtMaterno.Text.ToUpper(), txtNombres.Text.ToUpper());
                this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION        
                this.limpiacampos();
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.GridView1.DataSource = null;
                this.GridView1.DataSourceID = "";
                this.GridView1.DataBind();
                this.lblProductoresParecidos.Visible = false;
                this.lblProductorYaExiste.Visible = false;
                this.btnAceptar.Visible = false;
                this.imagenbien.Visible = true;
                
            }
            catch (Exception exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTORADDEDFAILED") + "  ERA PRODUCTOR RÁPIDO", txtPaterno.Text.ToUpper(), txtMaterno.Text.ToUpper(), txtNombres.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                //this.panelagregar.Visible = false;
            }
            
            finally
            {
                conGaribay.Close();

            }
        }
        protected void limpiacampos(){
            this.txtPaterno.Text = "";
            this.txtMaterno.Text = "";
            this.txtNombres.Text = "";
            this.txtCodeBoletas.Text = "";
            this.txtDomicilio.Text = "";
            this.txtPoblacion.Text = "";
            this.txtMunicipio.Text = "";
            this.txtTelefono.Text = "";
            this.txtCelular.Text = "";
            this.ddlEstado.SelectedValue = "14";

        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Write("<script type=\"text/javascript\">window.close();</script>");
        }
    }
}
