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
using System.Data.Odbc;
using System.Data.SqlClient;


namespace Garibay
{
    public partial class frmProductoresaspx : Garibay.BasePage
    {
        public frmProductoresaspx():base()
        {
            this.hasCalendar = true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtApaterno.Text = this.txtApaterno.Text.ToUpper().Trim();
            this.txtAMaterno.Text = this.txtAMaterno.Text.ToUpper().Trim();
            this.txtNombres.Text = this.txtNombres.Text.ToUpper().Trim();
            if (!this.IsPostBack)
            {
                JSUtils.AddDisableWhenClick(ref this.btnValidar, this);
                JSUtils.AddDisableWhenClick(ref this.btnAceptar, this);
                JSUtils.AddDisableWhenClick(ref this.btnModificar, this);
                JSUtils.AddDisableWhenClick(ref this.btnAceptardetails, this);
                this.panelmensaje.Visible = false;

                this.cmbEstado.DataBind();
                this.cmbEstadoCivil.DataBind();
                this.cmbRegimen.DataBind();
              //  this.cmbSexo.DataBind();
                this.cmbEstado.SelectedIndex = 13;
                if (Request.QueryString["data"] != null)
                {
                    if (this.loadqueryStrings(Request.QueryString["data"].ToString()))
                    {
                        this.lblProductoresParecidos.Visible = this.GridView1.Visible = false;
                        this.lblHeader.Text = "MODIFICANDO AL PRODUCTOR";
                        
                        if (this.cargadatosmodify())
                        {
                            this.txtIdtomodify.Text= myQueryStrings["idtomodify"].ToString();
                        }
                        else{
                            this.txtIdtomodify.Text = "-1";

                        }
                        this.btnModificar.Visible = true;
                        this.btnAceptar.Visible = false;
                    }
                    else
                    {
                        myQueryStrings.Clear();
                        Response.Redirect("~/frmAddModifyProductores.aspx", true);
                        
                    }
                }
                else{
                    
                    this.lblHeader.Text = "AGREGAR NUEVO UN PRODUCTOR";
                  //  this.txtFechanacimiento.Text = Utils.Now.ToString("dd/MM/yyyy");
                    //this.btnAceptar.Visible = true;
                    this.btnModificar.Visible = false;
                    this.lblNombreaModificar.Visible = false;
                }
               
            }
            //this.sdsProductoresRepetidos.SelectCommand = "SELECT [apaterno], [amaterno], [nombre] FROM [Productores] WHERE (([apaterno] LIKE '%' + @apaterno + '%') OR ([amaterno] LIKE '%' + @amaterno + '%') OR ([nombre] LIKE '%' + @nombre + '%')) ORDER BY [apaterno], [amaterno], [nombre]";
            this.sdsProductoresRepetidos.DataBind();
            this.GridView1.DataBind();
            //this.btnAceptar.Visible = (this.GridView1.Rows.Count == 0);
            this.btnModificar.Visible = ( (myQueryStrings["idtomodify"] != null && myQueryStrings["idtomodify"].ToString().Length > 0) &&  !this.GridView1.Visible );
            this.compruebasecurityLevel();
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
              
            }
        }

        protected void UpdateFilters()
        {
            /*
            String sFilters;
                        sFilters = "(apaterno LIKE '%"+ this.txtApaterno.Text+"%') OR (amaterno LIKE '%"+ this.txtAMaterno.Text+"%') OR (nombre LIKE '%"+this.txtNombres.Text+"%')";
                        this.sdsProdLikeName.FilterExpression = sFilters;
                        this.sdsProdLikeName.DataBind();*/
            
        }
        protected void PopCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            this.txtFechanacimiento.Text = PopCalendar1.SelectedDate;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            

            string sqlQuery = "INSERT INTO Productores (apaterno, amaterno, nombre, ";
            if(this.txtFechanacimiento.Text.Length>0)
                sqlQuery += " fechanacimiento, ";
            sqlQuery += " IFE, CURP, domicilio, poblacion, municipio, estadoID, CP, RFC, sexoID, telefono, telefonotrabajo, celular, fax, email, estadocivilID, regimenID, codigoBoletasFile, colonia, conyugue) ";
            sqlQuery += " VALUES(@apaterno,@amaterno,@nombre, ";
            if(this.txtFechanacimiento.Text.Length>0)
                sqlQuery += " @fechanacimiento, ";
            sqlQuery += "@IFE,@CURP,@domicilio,@poblacion,@municipio,@estadoID,@CP,@RFC,@sexoID,@telefono,@telefonotrabajo,@celular,@fax,@email,@estadocivilID,@regimenID,@codigoBoletasFile, @colonia, @conyugue) ";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(sqlQuery, conGaribay);

            try
            {
             
                
                cmdIns.Parameters.Add("@apaterno", SqlDbType.NVarChar).Value = this.txtApaterno.Text;
                cmdIns.Parameters.Add("@amaterno", SqlDbType.NVarChar).Value = this.txtAMaterno.Text;
                cmdIns.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = this.txtNombres.Text;
                if(this.txtFechanacimiento.Text.Length>0)
                    cmdIns.Parameters.Add("@fechanacimiento", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(this.txtFechanacimiento.Text);
                cmdIns.Parameters.Add("@IFE", SqlDbType.NVarChar).Value = this.txtIfe.Text;
                cmdIns.Parameters.Add("@CURP", SqlDbType.NVarChar).Value = this.txtCurp.Text;
                cmdIns.Parameters.Add("@domicilio", SqlDbType.NText).Value = this.txtDomicilio.Text;
                cmdIns.Parameters.Add("@poblacion", SqlDbType.NVarChar).Value = this.txtPoblacion.Text;
                cmdIns.Parameters.Add("@municipio", SqlDbType.NVarChar).Value = this.txtMunicipio.Text;
                cmdIns.Parameters.Add("@estadoID", SqlDbType.Int).Value =int.Parse(this.cmbEstado.SelectedValue);
                cmdIns.Parameters.Add("@CP", SqlDbType.NVarChar).Value = this.txtCodigopostal.Text;
                cmdIns.Parameters.Add("@RFC", SqlDbType.NVarChar).Value = this.txtRfc.Text;
                cmdIns.Parameters.Add("@sexoID", SqlDbType.Int).Value = int.Parse(this.rdSex.SelectedValue);
                cmdIns.Parameters.Add("@telefono", SqlDbType.NVarChar).Value = this.txtTelefono.Text;
                cmdIns.Parameters.Add("@telefonotrabajo", SqlDbType.NVarChar).Value = this.txtTelefonotrabajo.Text;
                cmdIns.Parameters.Add("@celular", SqlDbType.NVarChar).Value = this.txtCelular.Text;
                cmdIns.Parameters.Add("@fax", SqlDbType.NVarChar).Value = this.txtFax.Text;
                cmdIns.Parameters.Add("@email", SqlDbType.NVarChar).Value = this.txtCorreo.Text;
                cmdIns.Parameters.Add("@estadocivilID", SqlDbType.Int).Value = int.Parse(cmbEstadoCivil.SelectedValue);
                cmdIns.Parameters.Add("@regimenID", SqlDbType.Int).Value = int.Parse(this.cmbRegimen.SelectedValue);
                cmdIns.Parameters.Add("@codigoBoletasFile", SqlDbType.VarChar).Value = this.txtCodigoBoletasFile.Text;
                cmdIns.Parameters.Add("@colonia", SqlDbType.VarChar).Value = this.txtColonia.Text;
                cmdIns.Parameters.Add("@conyugue", SqlDbType.VarChar).Value = this.txtConyugue.Text;
                
                conGaribay.Open();
                int numregistros;
                numregistros = cmdIns.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("PRODUCTOREXECUTEFAILED"),"AGREGADO", "AGREGARON",numregistros.ToString()));
                }
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PRODUCTORES, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("SE AGREGÓ EL PRODUCTOR: " + this.txtApaterno.Text.ToUpper() + " " + this.txtAMaterno.Text.ToUpper() + " " + this.txtNombres.Text.ToUpper()));
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTORADDEDEXITO"), txtApaterno.Text.ToUpper(), txtAMaterno.Text.ToUpper(), txtNombres.Text.ToUpper());
                this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION        
                this.limpiacampos();
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                sqlQuery = "SELECT max(productorID) FROM Productores";
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
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTORADDEDFAILED"), txtApaterno.Text.ToUpper(), txtAMaterno.Text.ToUpper(), txtNombres.Text.ToUpper());
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
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTORADDEDFAILED"), txtApaterno.Text.ToUpper(), txtAMaterno.Text.ToUpper(), txtNombres.Text.ToUpper());
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
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTORADDEDFAILED"), txtApaterno.Text.ToUpper(), txtAMaterno.Text.ToUpper(), txtNombres.Text.ToUpper());
                this.lblMensajeException.Text = exception.Message; 
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                imagenmal.Visible = true;
                this.panelagregar.Visible = false;

            }
            finally{
                conGaribay.Close();

            }


        }
 

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Server.Transfer("~/frmListDeleteProductores.aspx");
        }
        protected void limpiacampos()
        {
            this.txtApaterno.Text = "";
            this.txtAMaterno.Text = "";
            this.txtNombres.Text = "";
            //this.cmbSexo.SelectedIndex = 0;
            this.txtIfe.Text = "";
            this.txtCurp.Text = "";
            this.txtDomicilio.Text = "";
            this.txtPoblacion.Text = "";
            this.cmbEstado.SelectedIndex = 13;
            this.cmbRegimen.SelectedIndex = 0;
            this.txtMunicipio.Text = "";
            this.txtCodigopostal.Text = "";
            this.txtRfc.Text = "";
            this.txtTelefono.Text = "";
            this.txtTelefonotrabajo.Text = "";
            this.txtCelular.Text = "";
            this.txtCorreo.Text = "";
            this.cmbRegimen.SelectedIndex = 0;
            this.txtFax.Text = "";
            this.txtFechanacimiento.Text = "";
            this.cmbEstadoCivil.SelectedIndex = 0;
            this.txtCodigoBoletasFile.Text = "";
            this.txtColonia.Text = "";
            this.txtConyugue.Text = "";

        }

        protected void btnAceptardetails_Click(object sender, EventArgs e)
        {
      
            if (this.txtIdtomodify.Text.Length > 0)
            {
                if (this.txtIdtomodify.Text == "-1")
                {
                    this.Response.Redirect("~/frmListDeleteProductores.aspx", true);
                }
            

            }
            this.panelmensaje.Visible = false;
            this.panelagregar.Visible = true;
           
        }
        protected bool cargadatosmodify() {
            string qryIns = "SELECT     Productores.productorID, Productores.apaterno, Productores.amaterno, Productores.nombre, Productores.IFE, Productores.CURP, Productores.domicilio, "+
                            " Productores.poblacion, Productores.municipio, Estados.estado, Productores.CP, Productores.RFC, Sexo.sexo, Productores.telefono, Productores.telefonotrabajo, " +
                            " Productores.celular, Productores.fax, Productores.email, EstadosCiviles.EstadoCivil, Regimenes.Regimen, Productores.fechanacimiento, Productores.estadocivilID, " +
                            " Productores.regimenID, Productores.sexoID, Productores.estadoID, Productores.codigoBoletasFile, Sexo.sexoID AS Expr1, Regimenes.regimenID AS Expr2,Productores.colonia, Productores.conyugue " + 
                            " FROM                        Productores LEFT OUTER JOIN " +
                            "  Estados ON Productores.estadoID = Estados.estadoID LEFT OUTER JOIN "+
                            " EstadosCiviles ON Productores.estadocivilID = EstadosCiviles.estadoCivilID LEFT OUTER JOIN " +
                            " Regimenes ON Productores.regimenID = Regimenes.regimenID LEFT OUTER JOIN " +
                            " Sexo ON Productores.sexoID = Sexo.sexoID  Where Productores.productorID = @prodID ";
           
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            try
            {
                cmdIns.Parameters.Clear();
                cmdIns.Parameters.Add("@prodID", SqlDbType.Int).Value = myQueryStrings["idtomodify"].ToString();
                conGaribay.Open();
                SqlDataReader datostomodify;
                datostomodify = cmdIns.ExecuteReader();
//                 this.cmbEstado.DataBind();
//                 this.cmbEstadoCivil.DataBind();
//                 this.cmbRegimen.DataBind();
//                 this.cmbSexo.DataBind();
                if(!datostomodify.HasRows){ //EL ID NO ES VALIDO
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
                   
                if (datostomodify.Read())
                {
                    this.txtApaterno.Text = datostomodify[1].ToString();
                    this.txtAMaterno.Text = datostomodify[2].ToString();
                    this.txtNombres.Text = datostomodify[3].ToString();
                    this.lblNombreaModificar.Text = datostomodify[1].ToString() + " " + datostomodify[2].ToString() + " " + datostomodify[3].ToString();
                    this.lblNombreaModificar.Visible = true;
                    this.txtIfe.Text = datostomodify[4].ToString();
                    this.txtCurp.Text = datostomodify[5].ToString();
                    this.txtDomicilio.Text = datostomodify[6].ToString();
                    this.txtPoblacion.Text = datostomodify[7].ToString();
                    this.txtMunicipio.Text = datostomodify[8].ToString();
                    this.txtCodigopostal.Text = datostomodify[10].ToString();
                    this.txtRfc.Text = datostomodify[11].ToString();
                    this.txtTelefono.Text = datostomodify[13].ToString();
                    this.txtTelefonotrabajo.Text = datostomodify[14].ToString();
                    this.txtCelular.Text = datostomodify[15].ToString();
                    this.txtFax.Text = datostomodify[16].ToString();
                    this.txtCorreo.Text = datostomodify[17].ToString();
                    this.txtFechanacimiento.Text = Utils.converttoshortFormatfromdbFormat(datostomodify[20].ToString());
                    this.txtCodigoBoletasFile.Text = datostomodify["codigoBoletasFile"].ToString();
                    this.txtColonia.Text = datostomodify["colonia"].ToString();
                    this.txtConyugue.Text = datostomodify["conyugue"].ToString();

                    try
                    {
                        if (datostomodify[23] != null)
	                    {
	                        this.rdSex.SelectedValue = datostomodify[23].ToString();
	                    }
	                    else
	                    {
	                        this.rdSex.SelectedIndex = 0;
	                    }
	                    if (datostomodify[24] != null)
	                    {
	                        this.cmbEstado.SelectedValue = datostomodify[24].ToString();
	                    }
	                    else
	                    {
	                        this.cmbEstado.SelectedIndex = 0;
	                    }
	
	                    if (datostomodify[21] != null)
	                    {
	                        this.cmbEstadoCivil.SelectedValue = datostomodify[21].ToString();
	                    }
	                    else
	                    {
	                        this.cmbEstadoCivil.SelectedIndex = 0;
	                    }
	                    if (datostomodify[22] != null)
	                    {
	                        this.cmbRegimen.SelectedValue = datostomodify[22].ToString();
	                    }
	                    else
	                    {
	                        this.cmbRegimen.SelectedIndex = 0;
	                    }
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.SELECT, "error parseando datos a modificar de productor", ref ex);
                    }
                    
                    
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

        protected void btnModificar_Click(object sender, EventArgs e)
        {
           
            string sqlQuery = "UPDATE Productores SET apaterno = @apaterno, amaterno= @amaterno, nombre = @nombre, fechanacimiento = @fechanacimiento, IFE = @IFE, CURP = @CURP, domicilio = @domicilio,";
            sqlQuery += " poblacion = @poblacion, municipio = @municipio, estadoID = @estadoID, CP = @CP, RFC = @RFC, sexoID = @Sexo, telefono = @telefono,  telefonotrabajo = @telefonotrabajo,";
            sqlQuery += " celular = @celular, estadocivilID = @estadocivilID, regimenID = @regimenID, email = @email, codigoBoletasFile = @codigoBoletasFile, updateTS = @updateTS, colonia = @colonia, conyugue = @conyugue WHERE productorID = @productorID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(sqlQuery, conGaribay);

            try
            {
                
                cmdIns.Parameters.Add("@apaterno", SqlDbType.NVarChar).Value = this.txtApaterno.Text;
                cmdIns.Parameters.Add("@amaterno", SqlDbType.NVarChar).Value = this.txtAMaterno.Text;
                cmdIns.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = this.txtNombres.Text;
                cmdIns.Parameters.Add("@fechanacimiento", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(this.txtFechanacimiento.Text);
                cmdIns.Parameters.Add("@IFE", SqlDbType.NVarChar).Value = this.txtIfe.Text;
                cmdIns.Parameters.Add("@CURP", SqlDbType.NVarChar).Value = this.txtCurp.Text;
                cmdIns.Parameters.Add("@domicilio", SqlDbType.NText).Value = this.txtDomicilio.Text;
                cmdIns.Parameters.Add("@poblacion", SqlDbType.NVarChar).Value = this.txtPoblacion.Text;
                cmdIns.Parameters.Add("@municipio", SqlDbType.NVarChar).Value = this.txtMunicipio.Text;
                cmdIns.Parameters.Add("@estadoID", SqlDbType.Int).Value = int.Parse(this.cmbEstado.SelectedValue);
                cmdIns.Parameters.Add("@CP", SqlDbType.NVarChar).Value = this.txtCodigopostal.Text;
                cmdIns.Parameters.Add("@RFC", SqlDbType.NVarChar).Value = this.txtRfc.Text;
                cmdIns.Parameters.Add("@Sexo", SqlDbType.Int).Value = int.Parse(this.rdSex.SelectedValue);
                cmdIns.Parameters.Add("@telefono", SqlDbType.NVarChar).Value = this.txtTelefono.Text;
                cmdIns.Parameters.Add("@telefonotrabajo", SqlDbType.NVarChar).Value = this.txtTelefonotrabajo.Text;
                cmdIns.Parameters.Add("@celular", SqlDbType.NVarChar).Value = this.txtCelular.Text;
                cmdIns.Parameters.Add("@fax", SqlDbType.NVarChar).Value = this.txtFax.Text;
                cmdIns.Parameters.Add("@email", SqlDbType.NVarChar).Value = this.txtCorreo.Text;
                cmdIns.Parameters.Add("@estadocivilID", SqlDbType.Int).Value = int.Parse(cmbEstadoCivil.SelectedValue);
                cmdIns.Parameters.Add("@regimenID", SqlDbType.Int).Value = int.Parse(this.cmbRegimen.SelectedValue);
                cmdIns.Parameters.Add("@productorID", SqlDbType.Int).Value = int.Parse(this.txtIdtomodify.Text);
                cmdIns.Parameters.Add("@codigoBoletasFile", SqlDbType.VarChar).Value = this.txtCodigoBoletasFile.Text;
                cmdIns.Parameters.Add("@colonia", SqlDbType.VarChar).Value =this.txtColonia.Text;
                cmdIns.Parameters.Add("@conyugue", SqlDbType.VarChar).Value = this.txtConyugue.Text;
                string sDateNow = Utils.getNowFormattedDate();
                cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = sDateNow;
                
                conGaribay.Open();
                int numregistros = cmdIns.ExecuteNonQuery();
               
                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("PRODUCTOREXECUTEFAILED"), "MODIFICADO", "MODIFICARON", numregistros.ToString()));
                }
                if (this.lblNombreaModificar.Text.ToUpper().CompareTo(txtApaterno.Text.ToUpper() + " " + txtAMaterno.Text.ToUpper() + " " + txtNombres.Text.ToUpper())!=0) {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PRODUCTORES, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ EL NOMBRE DEL PRODUCTOR DE: " + this.lblNombreaModificar.Text + " A: " + this.txtApaterno.Text.ToUpper() + " " + this.txtAMaterno.Text.ToUpper() + " " + this.txtNombres.Text.ToUpper()));
                }
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PRODUCTORES, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ EL PRODUCTOR: " + this.txtApaterno.Text.ToUpper() + " " + this.txtAMaterno.Text.ToUpper() + " " + this.txtNombres.Text.ToUpper()));
               
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                this.imagenmal.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTORMODIFIEDEXITO"), txtApaterno.Text.ToUpper(), txtAMaterno.Text.ToUpper(), txtNombres.Text.ToUpper());
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
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTORMODIFIEDFAILED"), txtApaterno.Text.ToUpper(), txtAMaterno.Text.ToUpper(), txtNombres.Text.ToUpper());
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
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTORMODIFIEDFAILED"), txtApaterno.Text.ToUpper(), txtAMaterno.Text.ToUpper(), txtNombres.Text.ToUpper());
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
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTORMODIFIEDFAILED"), txtApaterno.Text.ToUpper(), txtAMaterno.Text.ToUpper(), txtNombres.Text.ToUpper());
                this.lblMensajeException.Text = err2.Message;       
            
                this.panelagregar.Visible = false;
                
            }
            finally
            {
                conGaribay.Close();

            }
        }

        protected void txtNombres_TextChanged(object sender, EventArgs e)
        {
            this.UpdateFilters();
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
                checkComm.CommandText = "SELECT [apaterno], [amaterno], [nombre] FROM [Productores] WHERE (([apaterno] LIKE '%' + @apaterno + '%') OR ([amaterno] LIKE '%' + @amaterno + '%') OR ([nombre] LIKE '%' + @nombre + '%')) ORDER BY [apaterno], [amaterno], [nombre]";
                checkComm.Parameters.Add("@apaterno", SqlDbType.VarChar).Value = this.txtApaterno.Text;
                checkComm.Parameters.Add("@amaterno", SqlDbType.VarChar).Value = this.txtAMaterno.Text;
                checkComm.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtNombres.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(checkComm);
                da.Fill(dt);

                //check if there is a productor with the same name:
                checkComm.CommandText = "SELECT count(*) FROM [Productores] WHERE (([apaterno] LIKE '%' + @apaterno + '%') AND ([amaterno] LIKE '%' + @amaterno + '%') AND ([nombre] LIKE '%' + @nombre + '%'))";
                checkComm.Parameters.Clear();
                checkComm.Parameters.Add("@apaterno", SqlDbType.VarChar).Value = this.txtApaterno.Text;
                checkComm.Parameters.Add("@amaterno", SqlDbType.VarChar).Value = this.txtAMaterno.Text;
                checkComm.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtNombres.Text;

                Boolean bYaExiste = (int.Parse(checkComm.ExecuteScalar().ToString()) > 0);
                if (dt.Rows.Count > 0)
                {
                    this.lblProductoresParecidos.Visible =  this.GridView1.Visible = true;
                    this.GridView1.DataSourceID = "";
                    this.GridView1.DataSource = dt;
                    this.GridView1.DataBind();
                }
                
                if (bYaExiste)
                {
                    this.btnAceptar.Visible = false;
                    this.btnModificar.Visible = false;
                }
                else
                {
                    if (Request.QueryString["data"] != null)
                    {
                        this.btnAceptar.Visible = false;
                        this.btnModificar.Visible = true;
                    }
                    else
                    {
                        this.btnAceptar.Visible = true;
                        this.btnModificar.Visible = false;
                    }
                }
                this.lblProductorYaExiste.Visible = bYaExiste;
                this.btnValidar.Visible = bYaExiste;

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
     
       
    }
}
