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
    public partial class frmAddMovBancos : Garibay.BasePage
    {
        public frmAddMovBancos():base()
        {
            this.hasCalendar = true;
        }
        string montoant = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.ddlConcepto.DataBind();
                this.cmbTipodeMov.DataBind();
                this.drpdlCatalogoInterno.DataBind();
                this.drpdlGrupoCatalogos.DataBind();
                this.drpdlSubcatologointerna.DataBind();
                
               
                this.panelMensaje.Visible = false;
                this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
                if (Request.QueryString["data"] != null)
                {
                    if (this.loadqueryStrings(Request.QueryString["data"].ToString()))
                    {
                        this.lblHeader.Text = "MODIFICAR MOVIMIENTO DE BANCO";

                        if (this.cargadatosmodify())
                        {
                            this.txtChequeNum.ReadOnly = true;
                            this.ddlConcepto.Enabled = false;
                            this.txtidToModify.Text = myQueryStrings["idtomodify"].ToString();
                        }
                        else
                        {
                            this.ddlConcepto.Enabled = false;
                            this.txtChequeNum.ReadOnly = false;
                           this.txtidToModify.Text = "-1";

                        }
                        this.btnModificar.Visible = true;
                        this.btnAgregar.Visible = false;
                    }
                    else
                    {
                        myQueryStrings.Clear();
                        Response.Redirect("~/frmAddModifyProductores.aspx", true);

                    }
                }
                else
                {

                    this.lblHeader.Text = "AGREGAR NUEVO MOVIMIENTO DE BANCO";
                    this.btnAgregar.Visible = true;
                    this.btnModificar.Visible = false;
                    //this.lblNombreaModificar.Visible = false;
                }
               
            }
            this.divCheque.Attributes.Add("style", this.ddlConcepto.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            String sOnchange = "checkValueInList(";
            sOnchange += /*this.ddlConcepto.UniqueID*/"this" + ",'CHEQUE','";
            sOnchange += this.divCheque.ClientID + "')";
            this.ddlConcepto.Attributes.Add("onChange", sOnchange);
  /*
            this.drpdlGrupoCatalogos.DataSourceID = "sdsGruposCatalogos";
              this.drpdlCatalogoInterno.DataSourceID = "sdsCatalogoCuentaInterna";
              this.drpdlSubcatologointerna.DataSourceID = "sdsSubCatalogoInterna";
              this.drpdlGrupoCuentaFiscal.DataSourceID = "sdsGruposCatalogosfiscal";
              this.drpdlCatalogocuentafiscal.DataSourceID = "sdsCatalogoCuentaFiscal";
              this.drpdlSubcatalogofiscal.DataSourceID = "sdsSubcatalogofiscal";
                     */
            this.compruebasecurityLevel();
  
                  

        
         
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
            }
        }

        protected void cmbCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void PopCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            this.txtFecha.Text = PopCalendar1.SelectedDate;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.Server.Transfer("~/frmListDeleteMovBancos.aspx?");
        }

        
        protected void btnAgregar_Click1(object sender, EventArgs e)
        {

            this.cmbTipodeMov.DataBind();
            dsMovBanco.dtMovBancoDataTable tablaaux = new dsMovBanco.dtMovBancoDataTable();
            dsMovBanco.dtMovBancoRow dtRowainsertar = tablaaux.NewdtMovBancoRow();
            dtRowainsertar.conceptoID = int.Parse(this.ddlConcepto.SelectedValue);
            dtRowainsertar.nombre = this.txtNombre.Text;
            dtRowainsertar.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFecha.Text));
            //dats de cheque
            dtRowainsertar.numCheque = this.txtChequeNum.Text.Length > 0 ? int.Parse(this.txtChequeNum.Text) : 0;
            dtRowainsertar.chequeNombre = this.txtChequeNombre.Text;
            dtRowainsertar.facturaOlarguillo = this.txtFacturaLarguillo.Text;
            dtRowainsertar.numCabezas = this.txtNumCabezas.Text.Length > 0 ? double.Parse(this.txtNumCabezas.Text) : 0;
            dtRowainsertar.catalogoMovBancoFiscalID = int.Parse(this.drpdlCatalogocuentafiscal.SelectedValue);
            if(this.drpdlSubcatalogofiscal.SelectedIndex>-1)
                dtRowainsertar.subCatalogoMovBancoFiscalID = int.Parse(this.drpdlSubcatalogofiscal.SelectedValue);

            if (this.ddlConcepto.SelectedItem.Text != "CHEQUE") {
                dtRowainsertar.catalogoMovBancoInternoID = int.Parse(this.drpdlCatalogocuentafiscal.SelectedValue);
                if (this.drpdlSubcatalogofiscal.SelectedIndex > -1)
                    dtRowainsertar.subCatalogoMovBancoInternoID = int.Parse(this.drpdlSubcatalogofiscal.SelectedValue);
            }
            else{
                dtRowainsertar.catalogoMovBancoInternoID = int.Parse(this.drpdlCatalogoInterno.SelectedValue);
                if (this.drpdlSubcatologointerna.SelectedIndex > -1)
                   dtRowainsertar.subCatalogoMovBancoInternoID = int.Parse(this.drpdlSubcatologointerna.SelectedValue);

            }
            if (cmbTipodeMov.SelectedIndex == 0)
            {//ES CARGO
                dtRowainsertar.cargo = double.Parse(this.txtMonto.Text);
                dtRowainsertar.abono = 0.00;

            }
            else
            {//ES ABONO
                dtRowainsertar.abono = double.Parse(this.txtMonto.Text);
                dtRowainsertar.cargo = 0.00;
            }
            dtRowainsertar.storeTS = DateTime.Parse(Utils.getNowFormattedDate());
            dtRowainsertar.updateTS = DateTime.Parse(Utils.getNowFormattedDate());


            String serror = "", tipo = "";
            bool bTodobien = true;
            tipo = this.cmbTipodeMov.Text;
            dtRowainsertar.cuentaID = int.Parse(this.cmbCuenta.SelectedValue);
   /*
                     if (dbFunctions.insertMovementdeBanco(ref dtRowainsertar, ref serror, int.Parse(this.Session["USERID"].ToString()), int.Parse(this.cmbCuenta.SelectedValue),false,-1,null,-1,2,2,Utils.Now))
                        {
            
                            if (bTodobien)
                            {
                                this.panelagregar.Visible = false;
                                this.panelMensaje.Visible = true;
                                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                                this.TextBox1.Text = dtRowainsertar.movBanID.ToString();
                                if (dtRowainsertar.conceptoID == 3)
                                {
                                    this.btnPrintCheque.Visible = true;
                                }
                                this.DetailsView1.DataSource = tablaaux;
                                this.DetailsView1.DataBind();
                                this.imagenbien.Visible = true;
                                this.imagenmal.Visible = false;
                                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("MOVCUENTASADDEDEXITO"), tipo, this.txtMonto.Text, this.cmbCuenta.Items[this.cmbCuenta.SelectedIndex].Text, dtRowainsertar.movBanID.ToString());
                                this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION    
                                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDEBANCO, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), "AGREGÓ EL MOVIMIENTO DE BANCO NÚMERO: " + dtRowainsertar.movBanID.ToString() + " DE LA CUENTA CON EL ID: " + this.cmbCuenta.SelectedValue);
                                this.limpiarcampos();
                            }
                        }
                        else
                        {
                            this.panelagregar.Visible = false;
                            this.btnPrintCheque.Visible = true;
                            this.panelMensaje.Visible = true;
                            this.imagenbien.Visible = false;
                            this.imagenmal.Visible = true;
                            this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                            this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("MOVCUENTASADDEDFAILED"), tipo, this.txtMonto.Text, this.cmbCuenta.Items[this.cmbCuenta.SelectedIndex].Text);
                            this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION
                            Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), serror, this.Request.Url.ToString());
                        }*/
            
        }

        protected void btnAceptardtlst_Click(object sender, EventArgs e)
        {
            this.panelMensaje.Visible = false;
            this.panelagregar.Visible = true;
            this.limpiarcampos();
        }

        protected void limpiarcampos()
        {
            this.cmbCuenta.SelectedIndex = 0;
            this.cmbTipodeMov.SelectedIndex = 0;
            this.txtMonto.Text = "0.00";
            this.txtFecha.Text = "";
            this.txtChequeNombre.Text = "";
            this.txtChequeNum.Text = "";
   
            this.txtNombre.Text = "";
            this.txtFacturaLarguillo.Text = "";
            this.txtNumCabezas.Text = "";
        }

        protected bool cargadatosmodify()
        {
            string qrySel = "SELECT     MovimientosCuentasBanco.cuentaID, MovimientosCuentasBanco.cargo, MovimientosCuentasBanco.abono, ConceptosMovCuentas.Concepto, ";
                  qrySel+=    " MovimientosCuentasBanco.fecha, MovimientosCuentasBanco.ConceptoMovCuentaID, MovimientosCuentasBanco.nombre, ";
                  qrySel+=   " MovimientosCuentasBanco.facturaOlarguillo, MovimientosCuentasBanco.numCabezas, MovimientosCuentasBanco.numCheque, ";
                  qrySel+=   " MovimientosCuentasBanco.chequeNombre, MovimientosCuentasBanco.catalogoMovBancoFiscalID, ";
                  qrySel+=   " MovimientosCuentasBanco.subCatalogoMovBancoFiscalID, MovimientosCuentasBanco.catalogoMovBancoInternoID, ";
                  qrySel+=   " MovimientosCuentasBanco.subCatalogoMovBancoInternoID, catalogoMovimientosBancos_1.catalogoMovBancoID AS grupofiscal, ";
                  qrySel+=   " catalogoMovimientosBancos.catalogoMovBancoID AS grupointerno ";
                  qrySel+=    " FROM         MovimientosCuentasBanco INNER JOIN ";
                  qrySel+=    " ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID INNER JOIN ";
                  qrySel+=    " catalogoMovimientosBancos ON MovimientosCuentasBanco.catalogoMovBancoFiscalID = catalogoMovimientosBancos.catalogoMovBancoID INNER JOIN ";
                  qrySel+=   " catalogoMovimientosBancos AS catalogoMovimientosBancos_1 ON ";
                  qrySel+=    " MovimientosCuentasBanco.catalogoMovBancoInternoID = catalogoMovimientosBancos_1.catalogoMovBancoID where MovimientosCuentasBanco.movbanID = @movbanID ";
           
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdSel = new SqlCommand(qrySel, conGaribay);
            try
            {
                cmdSel.Parameters.Add("@movBanID", SqlDbType.Int).Value = int.Parse(this.myQueryStrings["idtomodify"].ToString());
                conGaribay.Open();
                SqlDataReader datostomodify;
                datostomodify = cmdSel.ExecuteReader();
                this.cmbCuenta.DataBind();
                this.cmbTipodeMov.DataBind();
                this.ddlConcepto.DataBind();
                if (!datostomodify.HasRows)
                { //EL ID NO ES VALIDO
                    this.lblMensajeOperationresult.Text = myConfig.StrFromMessages("FALLOCARGARMODIFICAR");
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXCEPTION
                    this.imagenmal.Visible = true;
                    this.panelMensaje.Visible = true;
                    this.imagenbien.Visible = false;
                    //this.txtIDdetails.Text = "-1";
                    this.panelagregar.Visible = false;


                    return false;

                }

                if(datostomodify.Read())
                {
                    this.cmbCuenta.SelectedValue = datostomodify["cuentaID"].ToString();
                    if(double.Parse(datostomodify["cargo"].ToString())>0) //ES CARGO
                    {
                        this.cmbTipodeMov.SelectedValue = "CARGO";
                        this.txtMonto.Text = datostomodify["cargo"].ToString();
                    }else//ES ABONO
                    {
                        this.cmbTipodeMov.SelectedValue = "ABONO";
                        this.txtMonto.Text = datostomodify["abono"].ToString();
                        montoant = this.txtMonto.Text;
                    }
                    this.ddlConcepto.Text = datostomodify["concepto"].ToString();
                    this.txtNombre.Text = datostomodify["nombre"].ToString();
                    this.txtFecha.Text = Utils.converttoshortFormatfromdbFormat(datostomodify["fecha"].ToString());
                    this.txtChequeNum.Text = datostomodify["numCheque"].ToString();
                    this.txtChequeNombre.Text = datostomodify["chequeNombre"].ToString();
                  
                    this.divCheque.Attributes.Add("style", this.ddlConcepto.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                    this.txtFacturaLarguillo.Text = datostomodify["facturaOlarguillo"].ToString();
                    this.txtNumCabezas.Text = datostomodify["numCabezas"].ToString();
                    this.drpdlCatalogocuentafiscal.SelectedValue = datostomodify["catalogoMovBancoFiscalID"].ToString();
                    this.drpdlSubcatalogofiscal.SelectedValue = datostomodify["subCatalogoMovBancoFiscalID"].ToString();
                    this.drpdlCatalogoInterno.SelectedValue = datostomodify["catalogoMovBancoInternoID"].ToString();
                    this.drpdlSubcatologointerna.SelectedValue = datostomodify["subCatalogoMovBancoInternoID"].ToString();
                    this.drpdlGrupoCatalogos.SelectedValue = datostomodify["grupointerno"].ToString();
                    this.drpdlGrupoCuentaFiscal.SelectedValue = datostomodify["grupofiscal"].ToString();


                }
            }
            catch (InvalidOperationException exception)
            {
                this.lblMensajeOperationresult.Text = myConfig.StrFromMessages("FALLOCARGARMODIFICAR");
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelMensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.panelagregar.Visible = false;
                return false;
            }
            catch (SqlException exception)
            {
                this.lblMensajeOperationresult.Text = myConfig.StrFromMessages("FALLOCARGARMODIFICAR");
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelMensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.panelagregar.Visible = false;
                return false;

            }
            catch (Exception exception)
            {
                this.lblMensajeOperationresult.Text = myConfig.StrFromMessages("FALLOCARGARMODIFICAR");
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelMensaje.Visible = true;
                this.imagenbien.Visible = false;
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
            dsMovBanco.dtMovBancoDataTable tablaaux = new dsMovBanco.dtMovBancoDataTable();
            dsMovBanco.dtMovBancoRow drUpdate = tablaaux.NewdtMovBancoRow();
            string sError = "ERROR";
            int movID = int.Parse(this.txtidToModify.Text);
            if(this.cmbTipodeMov.SelectedValue=="CARGO")//ES CARGO
            {
                drUpdate.abono = 0;
                drUpdate.cargo = double.Parse(this.txtMonto.Text);
            }else
            {
                drUpdate.cargo = 0;
                drUpdate.abono = Double.Parse(this.txtMonto.Text);
            }
            drUpdate.conceptoID = int.Parse(this.ddlConcepto.SelectedValue);
            drUpdate.nombre = this.txtNombre.Text;
            drUpdate.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFecha.Text));
            drUpdate.updateTS = Utils.Now;
            drUpdate.chequeNombre = this.txtChequeNombre.Text;
            drUpdate.numCabezas = this.txtNumCabezas.Text.Length > 0 ? double.Parse(this.txtNumCabezas.Text) : 0;
            drUpdate.facturaOlarguillo = this.txtFacturaLarguillo.Text;
            drUpdate.numCheque = this.txtChequeNum.Text.Length > 0 ? int.Parse(this.txtChequeNum.Text) : 0;
            drUpdate.catalogoMovBancoFiscalID = int.Parse(this.drpdlCatalogocuentafiscal.SelectedValue);
            if (this.drpdlSubcatalogofiscal.SelectedIndex > -1)
                drUpdate.subCatalogoMovBancoFiscalID = int.Parse(this.drpdlSubcatalogofiscal.SelectedValue);

            if (this.ddlConcepto.SelectedItem.Text != "CHEQUE")
            {
                drUpdate.catalogoMovBancoInternoID = int.Parse(this.drpdlCatalogocuentafiscal.SelectedValue);
                if (this.drpdlSubcatalogofiscal.SelectedIndex > -1)
                    drUpdate.subCatalogoMovBancoInternoID = int.Parse(this.drpdlSubcatalogofiscal.SelectedValue);
            }
            else
            {
                drUpdate.catalogoMovBancoInternoID = int.Parse(this.drpdlCatalogoInterno.SelectedValue);
                if (this.drpdlSubcatologointerna.SelectedIndex > -1)
                    drUpdate.subCatalogoMovBancoInternoID = int.Parse(this.drpdlSubcatologointerna.SelectedValue);

            }
            if (dbFunctions.updateMovementdeBanco(ref drUpdate,movID, ref sError, int.Parse(this.Session["USERID"].ToString()), int.Parse(this.cmbCuenta.SelectedValue)))
            {
                this.panelMensaje.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("MOVCUENTASMODIFIEDEXITO"), movID.ToString(), this.cmbCuenta.SelectedItem);
                this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION
                this.imagenbien.Visible = true;
                this.imagenmal.Visible = false;
                this.panelagregar.Visible = false;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDEBANCO, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), "EL MOVIMIENTO DE BANCO NÚMERO: " + movID.ToString() + "DE LA CUENTA: " + this.cmbCuenta.SelectedItem + "HA SIDO MODIFICADO. LA CANTIDAD ANTERIOR ERA: $ " +montoant +" LA NUEVA ES: $ "+this.cmbCuenta.Text);

            }
            else
            {
                this.panelMensaje.Visible = true;
                this.panelagregar.Visible = false;
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("MOVCUENTASMODIFIEDFAILED"), movID.ToString(), this.cmbCuenta.SelectedItem);
                this.lblMensajeException.Text = sError; //BORRAMOS PORQUE NO HAY EXcEPTION
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), sError, this.Request.Url.ToString());


            }
        }

        protected void btnPrintCheque_Click(object sender, EventArgs e)
        {
            string query = "SELECT     Bancos.bancoID FROM         Bancos INNER JOIN ";
            query += " CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID INNER JOIN ";
            query += "  MovimientosCuentasBanco ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco.cuentaID where MovimientosCuentasBanco.movbanID = @movbanID ";
            int banID=-1;
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdsel = new SqlCommand(query,conGaribay);
            conGaribay.Open();
            try{
                cmdsel.Parameters.Add("@movbanID",SqlDbType.Int).Value = int.Parse(this.TextBox1.Text);
                SqlDataReader read = cmdsel.ExecuteReader();
                if(read.HasRows){
                    read.Read();
                    banID = int.Parse(read[0].ToString());
                    
                }
            }
            catch(Exception ex){

            }
            finally{
                conGaribay.Close();
            }
          
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
                    float.TryParse(cookie[banID.ToString() + "posX"].ToString(), out fPosX);
                    float.TryParse(cookie[banID.ToString() + "posY"].ToString(), out fPosY);
                }
                catch (NullReferenceException exp)
                {

                }
            }
// 
//           //  String pathArchivotemp = PdfCreator.printCheque(int.Parse(this.TextBox1.Text), PdfCreator.orientacionPapel.VERTICAL, PdfCreator.tamañoPapel.CARTA,fPosX,fPosY, this.userID);
//             Response.ClearHeaders();
//             Response.ContentType = "application/pdf";
//             Response.AddHeader("Content-Disposition", "attachment;filename=ImpresionCheque.pdf");
//             Response.WriteFile(pathArchivotemp);
//             Response.Flush();
//             Response.End();

        }

        protected void cmbTipodeMov_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpdlGrupoCuentaFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlCatalogocuentafiscal.DataBind();
            this.drpdlSubcatalogofiscal.DataBind();
        }

        protected void drpdlGrupoCatalogos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlCatalogoInterno.DataBind();
            this.drpdlCatalogoInterno.DataBind();
        }
    }
}
