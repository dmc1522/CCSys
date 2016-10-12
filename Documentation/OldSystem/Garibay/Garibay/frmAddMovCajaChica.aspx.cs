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
    public partial class WebForm4 : Garibay.BasePage
    {
        public WebForm4():base()
        {
            this.hasCalendar = true;
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
            this.compruebasecurityLevel();
            try
            {           
                if (!this.IsPostBack)
                {
                    this.chbMostrarPnlAddTarjDiesel.Checked = false;
                    this.txtNewFechaEntrada.Text = Utils.Now.ToString("dd/MM/yyyy");
                    //this.btnAgregarBoleta.Visible = true;
                    //this.btnRemoveBoleta.Visible = true;
                    this.divPrestamo.Attributes.Add("style", this.drpdlTipoAnticipo.SelectedItem.Text == "PRESTAMO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                    this.divdiesel.Attributes.Add("style", "visibility: hidden; display: none");
                    String sOnchange = "ShowHideDivOnChkBox('";
                    sOnchange += this.chkboxAnticipo.ClientID + "','";
                    sOnchange += this.divanticipo.ClientID + "')";
                    this.chkboxAnticipo.Attributes.Add("onclick", sOnchange);

                    sOnchange = "ShowHideDivOnChkBox('";
                    sOnchange += this.chkNewBoleta.ClientID + "','";
                    sOnchange += this.divNewBoleta.ClientID + "')";
                    this.chkNewBoleta.Attributes.Add("onclick", sOnchange);

                    sOnchange = "ShowHideDivOnChkBox('";
                    sOnchange += this.chkChangeFechaSalidaNewBoleta.ClientID + "','";
                    sOnchange += this.divFechaSalidaNewBoleta.ClientID + "')";
                    this.chkChangeFechaSalidaNewBoleta.Attributes.Add("onclick", sOnchange);

                    sOnchange = "subTextBoxes('";
                    sOnchange += this.txtNewPesoEntrada.ClientID + "','";
                    sOnchange += this.txtNewPesoSalida.ClientID + "','";
                    sOnchange += this.txtPesoNetoNewBoleta.ClientID + "')";
                    this.txtNewPesoEntrada.Attributes.Add("onKeyUp", sOnchange);
                    this.txtNewPesoEntrada.Attributes.Add("onBlur", sOnchange);
                    this.txtNewPesoSalida.Attributes.Add("onKeyUp", sOnchange);
                    this.txtNewPesoSalida.Attributes.Add("onBlur", sOnchange);

                    sOnchange = "showDivOnListContains(";
                    sOnchange += /*this.ddlConcepto.UniqueID*/"this" + ",'TRASPASOS','";
                    sOnchange += this.divConCuentaYCajaDestino.ClientID + "')";
                    this.drpdlGrupoCatalogos.Attributes.Add("onChange", sOnchange);

                    sOnchange = "showDivOnListContains(";
                    sOnchange += /*this.ddlConcepto.UniqueID*/"this" + ",'CAJA CHICA','";
                    sOnchange += this.divCajaDestino.ClientID + "');";
                    //this.drpdlCatalogoInterno.Attributes.Add("onChange", sOnchange);

                    sOnchange += "showDivOnListNOTContains(";
                    sOnchange += /*this.ddlConcepto.UniqueID*/"this" + ",'CAJA CHICA','";
                    sOnchange += this.divCuentaDestino.ClientID + "')";
                    this.drpdlCatalogoInterno.Attributes.Add("onChange", sOnchange);


                    sOnchange = "checkValueInList(";
                    sOnchange += /*this.ddlConcepto.UniqueID*/"this" + ",'PRESTAMO','";
                    sOnchange += this.divPrestamo.ClientID + "')";
                    this.drpdlTipoAnticipo.Attributes.Add("onChange", sOnchange);

                    sOnchange = "ShowHideDivOnChkBox('";
                    sOnchange +=chbMostrarPnlAddTarjDiesel.ClientID + "','";
                    sOnchange += this.divdiesel.ClientID + "');";
                    this.chbMostrarPnlAddTarjDiesel.Attributes.Add("onclick", sOnchange);
                    this.panelMensaje.Visible = false;
                    this.ddlIdCiclo.DataBind();
                    //this.drpdlProductorBoletas.DataBind();
                    this.drpdlGrupoCatalogos.DataBind();
                    this.drpdlGrupoCatalogos.DataBind();
                    this.drpdlSubcatologointerna.DataBind();
                    this.drpdlTipoAnticipo.DataBind();
                    try
                    {
                        this.ddlBodegas.SelectedValue = this.BodegaID.ToString();
                    }
                    catch
                    {
                        this.ddlBodegas.DataBind();
                        this.ddlBodegas.SelectedIndex = 0;
                    }
                    

                    if (Request.QueryString["data"] != null)
                    {
                        
                        if (this.loadqueryStrings(Request.QueryString["data"].ToString()))
                        {
                            this.lblMovCajaChica.Text = "MODIFICAR UN MOVIMIENTO DE CAJA CHICA";

                            if (this.cargadatostomodify())
                            {
                                this.txtIDDetails.Text = myQueryStrings["idtomodify"].ToString();
                                this.PopCalendar1.Enabled = false;
                            }
                            else
                            {
                                this.txtIDDetails.Text = "-1";

                            }
                            this.txtFolio.ReadOnly = true;
                            //this.btnAddTarjetaDiesel.Visible = false;
                            this.btnModificar.Visible = true;
                            this.cmdAceptar.Visible = false;
                        }
                        else
                        {
                            myQueryStrings.Clear();
                            Response.Redirect("~/frmAddMovCajaChica.aspx", true);

                        }
                    }
                    else
                    {

                        this.lblMovCajaChica.Text = "AGREGAR NUEVO MOVIMIENTO DE CAJA CHICA";
                        this.btnAceptardtlst.Visible = true;
                        this.btnModificar.Visible = false;
                        //this.btnModTarjetaDiesel.Visible = false;
                        //this.btnAddTarjetaDiesel.Visible = true;
                        this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
                        this.txtFechaLimite.Text = this.txtFecha.Text;
                    }
                }

                this.divConCuentaYCajaDestino.Attributes.Add("style", (this.drpdlGrupoCatalogos.SelectedItem != null && this.drpdlGrupoCatalogos.SelectedItem.Text.IndexOf("TRASPASOS") > -1) ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.divCajaDestino.Attributes.Add("style", (this.drpdlCatalogoInterno.SelectedItem != null && this.drpdlCatalogoInterno.SelectedItem.Text.IndexOf("CAJA CHICA") > -1) ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.divCuentaDestino.Attributes.Add("style", (this.drpdlCatalogoInterno.SelectedItem != null && this.drpdlCatalogoInterno.SelectedItem.Text.IndexOf("CAJA CHICA") <= -1) ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                
                //this.gridBoletasSistema.DataSourceID = "sdsBoletasdelSistema";
                this.divanticipo.Attributes.Add("style", this.chkboxAnticipo.Checked? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.divNewBoleta.Attributes.Add("style", this.chkNewBoleta.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.divFechaSalidaNewBoleta.Attributes.Add("style", this.chkChangeFechaSalidaNewBoleta.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.divPrestamo.Attributes.Add("style", this.drpdlTipoAnticipo.SelectedItem.Text == "PRESTAMO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");

                this.panelNewBoletaResult.Visible = false;

            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "page_load de add caja chica", ref ex);
            }
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
            }
        }


        protected void cmdCancelar_Click(object sender, EventArgs e)
        {
            this.Server.Transfer("~/frmListMovCajaChica.aspx");
        }

        protected void cmdAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                dsMovCajaChica.dtMovCajaChicaDataTable tablaaux = new dsMovCajaChica.dtMovCajaChicaDataTable();
                dsMovCajaChica.dtMovCajaChicaRow dtRowainsertar = tablaaux.NewdtMovCajaChicaRow();
                dtRowainsertar.nombre = this.txtNombre.Text;
                dtRowainsertar.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFecha.Text));
                dtRowainsertar.cargo = this.cmbTipodeMov.SelectedIndex == 0 ? double.Parse(this.txtMonto.Text) : 0f;
                dtRowainsertar.abono = this.cmbTipodeMov.SelectedIndex == 1 ? double.Parse(this.txtMonto.Text) : 0f;
                dtRowainsertar.storeTS = DateTime.Parse(Utils.getNowFormattedDate());
                dtRowainsertar.updateTS = DateTime.Parse(Utils.getNowFormattedDate());
                dtRowainsertar.Observaciones = this.txtObser.Text;
                dtRowainsertar.catalogoMovBancoInternoID = int.Parse(this.drpdlCatalogoInterno.SelectedValue);
                if (this.drpdlSubcatologointerna.SelectedIndex > 0)
                    dtRowainsertar.subCatalogoMovBancoInternoID = int.Parse(this.drpdlSubcatologointerna.SelectedValue);
                dtRowainsertar.numCabezas = this.txtNumCabezas.Text.Length > 0 ? double.Parse(this.txtNumCabezas.Text) : 0f;
                dtRowainsertar.facturaOlarguillo = this.txtNumFacturaoLarguillo.Text;
                dtRowainsertar.bodegaID = int.Parse(this.ddlBodegas.SelectedValue);
                dtRowainsertar.Bodega = this.ddlBodegas.SelectedItem.Text;



                String serror = "";
                if (dbFunctions.insertMovCajaChica(ref dtRowainsertar, ref serror, this.UserID, int.Parse(this.ddlIdCiclo.SelectedValue), this.chkboxAnticipo.Checked, int.Parse(this.drpdlProductor.SelectedValue), ref listBoxAgregadas, int.Parse(this.drpdlTipoAnticipo.SelectedValue), this.txtInteresAnual.Text.Length > 0 ? float.Parse(this.txtInteresAnual.Text) : 0f, this.txtInteresmoratorio.Text.Length > 0 ? float.Parse(this.txtInteresmoratorio.Text) : 0f, DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaLimite.Text)), this.drpdlProductor.SelectedIndex > 0 ? this.drpdlProductor.SelectedItem.Text : ""))
                {
                    if(this.chbMostrarPnlAddTarjDiesel.Checked)
                    {
                        if (dbFunctions.addTarjetaDiesel(dtRowainsertar.movimientoID,
                                                     int.Parse(this.txtFolio.Text),
                                                     float.Parse(this.txtMontoTarjetaDiesel.Text),
                                                     float.Parse(this.txtLitrosTarjetaDiesel.Text),
                                                     ref serror, this.UserID, 0))
                        {
                            this.txtFolio.ReadOnly = true;
                            this.chbMostrarPnlAddTarjDiesel.Checked = false;
                            this.txtMontoTarjetaDiesel.Text = "";
                            this.txtLitrosTarjetaDiesel.Text = "";
                            this.txtFolio.Text = "";
                        }
                        else
                        {
                            throw new Exception(serror);
                        }
                    }
                    String sNewMov = dtRowainsertar.movimientoID.ToString();
                    if (this.drpdlCatalogoInterno.SelectedIndex != null && this.drpdlGrupoCatalogos.SelectedItem.Text.IndexOf("TRASPASOS") > -1)
                    {
                        //si es un traspaso entonces verificar el destino
                        if (this.drpdlCatalogoInterno.SelectedItem != null && this.drpdlCatalogoInterno.SelectedItem.Text.IndexOf("CAJA CHICA") > -1)
                        {
                            dtRowainsertar.movimientoID = -1;
                            dtRowainsertar.bodegaID = int.Parse(this.ddlCajaDestino.SelectedValue);
                            double fMonto;
                            fMonto = dtRowainsertar.cargo;
                            dtRowainsertar.cargo = dtRowainsertar.abono;
                            dtRowainsertar.abono = fMonto;
                            int cicloID = int.Parse(this.ddlCicloDestino.SelectedValue);
                            if (dbFunctions.insertMovCajaChica(ref dtRowainsertar, ref serror, this.UserID, cicloID, false, -1, ref this.listBoxAgregadas, -1, 0, 0, Utils.Now, ""))
                            {
                                //if the mov destino were successfully added then add the relation with the mov origen
                                SqlConnection connOrigen = new SqlConnection(myConfig.ConnectionInfo);
                                SqlCommand commOrigen = new SqlCommand();
                                commOrigen.CommandText = "INSERT INTO MOVIMIENTOORIGEN(movimientoID) VALUES(@movimientoID);select movimientoID = SCOPE_IDENTITY();";
                                try
                                {
                                    commOrigen.Connection = connOrigen;
                                    connOrigen.Open();
                                    commOrigen.Parameters.Add("movimientoID", SqlDbType.Int).Value = int.Parse(sNewMov);
                                    int movorigenid = int.Parse(commOrigen.ExecuteScalar().ToString());
                                    commOrigen.CommandText = "update movimientosCaja set movOrigenID = @movOrigenID where movimientoID = @movimientoID";
                                    commOrigen.Parameters.Clear();
                                    commOrigen.Parameters.Add("movOrigenID", SqlDbType.Int).Value = movorigenid;
                                    commOrigen.Parameters.Add("movimientoID", SqlDbType.Int).Value = dtRowainsertar.movimientoID;
                                    commOrigen.ExecuteNonQuery();

                                    
                                }
                                catch (System.Exception ex)
                                {
                                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "Error en mov de origen :" + ex.Message + " stack: " + Environment.StackTrace, this.Request.Url.ToString());
                                }
                                finally
                                {
                                    connOrigen.Close();
                                }
                            }
                        }
                        else
                        {
                            ////insert the mov de banco destino
                            //es a otra cuenta de banco
                            dsMovBanco.dtMovBancoRow rowBanco = new dsMovBanco.dtMovBancoDataTable().NewdtMovBancoRow();
                            rowBanco.conceptoID = 7;// dtRowainsertar.conceptoID;
                            rowBanco.nombre = dtRowainsertar.nombre;
                            rowBanco.fecha = dtRowainsertar.fecha;
                            //dats de cheque
                            rowBanco.numCheque = 0;
                            rowBanco.chequeNombre = "";
                            rowBanco.facturaOlarguillo = "";
                            rowBanco.numCabezas = 0;

                            rowBanco.catalogoMovBancoInternoID = dtRowainsertar.catalogoMovBancoInternoID;
                            rowBanco.subCatalogoMovBancoInternoID = dtRowainsertar.IssubCatalogoMovBancoInternoIDNull() ? -1 : dtRowainsertar.subCatalogoMovBancoInternoID;

                            rowBanco.catalogoMovBancoFiscalID = rowBanco.catalogoMovBancoInternoID;
                            rowBanco.subCatalogoMovBancoFiscalID = dtRowainsertar.IssubCatalogoMovBancoInternoIDNull() ? -1 : dtRowainsertar.subCatalogoMovBancoInternoID;

                            if (dtRowainsertar.cargo > 0)
                            {
                                rowBanco.abono = dtRowainsertar.cargo;
                                rowBanco.cargo = 0;
                            }
                            else
                            {
                                rowBanco.cargo = dtRowainsertar.abono;
                                rowBanco.abono = dtRowainsertar.cargo;
                            }

                            String serrorBanco = "";
                            rowBanco.cuentaID = int.Parse(this.ddlCuentaDestino.SelectedValue);
                            ListBox tempLB = new ListBox();
                            if (dbFunctions.insertMovementdeBanco(ref rowBanco, ref serrorBanco, this.UserID, rowBanco.cuentaID, false, -1, ref tempLB, -1, 0f, 0f, Utils.Now, -1, ""))
                            {
                                //if the mov destino were successfully added then add the relation with the mov origen
                                SqlConnection connOrigen = new SqlConnection(myConfig.ConnectionInfo);
                                SqlCommand commOrigen = new SqlCommand();
                                commOrigen.CommandText = "INSERT INTO MOVIMIENTOORIGEN(movimientoID) VALUES(@movimientoID);select movimientoID = SCOPE_IDENTITY();";
                                try
                                {
                                    commOrigen.Connection = connOrigen;
                                    connOrigen.Open();
                                    commOrigen.Parameters.Add("movimientoID", SqlDbType.Int).Value = int.Parse(sNewMov);
                                    int movorigenid = int.Parse(commOrigen.ExecuteScalar().ToString());
                                    commOrigen.CommandText = "update movimientosCuentasBanco set movOrigenID = @movOrigenID where movbanID = @movbanID";
                                    commOrigen.Parameters.Clear();
                                    commOrigen.Parameters.Add("movOrigenID", SqlDbType.Int).Value = movorigenid;
                                    commOrigen.Parameters.Add("movbanID", SqlDbType.Int).Value = rowBanco.movBanID;
                                    commOrigen.ExecuteNonQuery();
                                }
                                catch (System.Exception ex)
                                {
                                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "Error en mov de origen :" + ex.Message, this.Request.Url.ToString());
                                }
                                finally
                                {
                                    connOrigen.Close();
                                }
                            }
                            //end insert the mov de banco destino
                        }
                    }


                    this.panelagregar.Visible = false;
                    this.panelMensaje.Visible = true;
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                    this.txtIDDetails.Text = sNewMov;
                    //  this.DetailsView1.DataSource = tablaaux;
                    this.DetailsView1.DataBind();
                    this.imagenbien.Visible = true;
                    this.imagenmal.Visible = false;
                    this.limpiacampos();
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("MOVCAJAADDEDEXITO"), sNewMov);
                    this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION    
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDECAJACHICA, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), "AGREGÓ EL MOVIMIENTO DE CAJA CHICA NÚMERO: " + dtRowainsertar.movimientoID.ToString());
                    this.chbMostrarPnlAddTarjDiesel.Checked = false;

                }
                else
                {
                    this.panelagregar.Visible = false;
                    this.panelMensaje.Visible = true;
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("MOVCAJAADDEDFAILED"), dtRowainsertar.movimientoID.ToString());
                    this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), serror, this.Request.Url.ToString());
                    this.chbMostrarPnlAddTarjDiesel.Checked = false;
                }
            }
            catch(Exception ex){
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "ERROR AL INSERTAR MOVIMIENTO DE CAJA. EX : " + ex.Message, this.Request.Url.ToString());
                this.chbMostrarPnlAddTarjDiesel.Checked = false;
                //this.panelagregar.Visible = true;
                
                

            }
        }

        protected void btnAceptardtlst_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["data"] != null)
            {
                Response.Redirect("~/frmListMovCajaChica.aspx", true);
            }
            else
            {
                this.panelMensaje.Visible = false;
                this.panelagregar.Visible = true;
            }
        }

        protected void limpiarcampos(){
            this.ddlIdCiclo.SelectedIndex = -1;
            //this.DDLIDConceptoMov.SelectedIndex = -1;
            this.txtObser.Text = "";
            //this.txtAbono.Text = "";
            //this.txtCargo.Text = "";
            this.txtNombre.Text = "";

            //Campos de tarjeta diesel
            this.txtMontoTarjetaDiesel.Text = "";
            this.txtFolio.Text = "";
            this.txtLitrosTarjetaDiesel.Text = "";
  


        }
        protected bool cargadatostomodify()
        {
            string qryIns = "SELECT     MovimientosCaja.cicloID, MovimientosCaja.nombre, MovimientosCaja.cargo, MovimientosCaja.abono, MovimientosCaja.Observaciones, MovimientosCaja.fecha, ";
            qryIns+="          MovimientosCaja.catalogoMovBancoID, MovimientosCaja.subCatalogoMovBancoID, MovimientosCaja.facturaOlarguillo, MovimientosCaja.numCabezas, ";
            qryIns+="          GruposCatalogosMovBancos.grupoCatalogosID, TarjetasDiesel.monto, TarjetasDiesel.litros, TarjetasDiesel.folio";
qryIns+=" FROM         MovimientosCaja LEFT JOIN";
         qryIns+="             MovimientosCaja_TarjetasDiesel ON MovimientosCaja.movimientoID = MovimientosCaja_TarjetasDiesel.movimientoID LEFT JOIN";
         qryIns+="             TarjetasDiesel ON MovimientosCaja_TarjetasDiesel.folio = TarjetasDiesel.folio INNER JOIN";
         qryIns+="             catalogoMovimientosBancos ON MovimientosCaja.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID INNER JOIN";
         qryIns+="             GruposCatalogosMovBancos ON catalogoMovimientosBancos.grupoCatalogoID = GruposCatalogosMovBancos.grupoCatalogosID";
qryIns+=" WHERE     (MovimientosCaja.movimientoID = @movimientoID)";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            try
            {
                cmdIns.Parameters.Add("@movimientoID", SqlDbType.Int).Value = int.Parse(this.myQueryStrings["idtomodify"].ToString());
                conGaribay.Open();
                SqlDataReader datostomodify;
                datostomodify = cmdIns.ExecuteReader();
                this.ddlIdCiclo.DataBind();
                //this.DDLIDConceptoMov.DataBind();
              
                if (!datostomodify.HasRows)
                { //EL ID NO ES VALIDO
                    this.lblMensajeOperationresult.Text = myConfig.StrFromMessages("FALLOCARGARMODIFICAR");
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXCEPTION
                    this.imagenmal.Visible = true;
                    this.panelMensaje.Visible = true;
                    this.imagenbien.Visible = false;
                    this.txtIDDetails.Text = "-1";
                    this.panelagregar.Visible = false;
                    return false;
                }

                if (datostomodify.HasRows && datostomodify.Read())
                {
                    //this.DDLIDConceptoMov.SelectedValue = datostomodify[0].ToString();
                    this.ddlIdCiclo.SelectedValue = datostomodify["cicloID"].ToString();
                    
                    this.txtNombre.Text = datostomodify["nombre"].ToString();
                    //this.txtCargo.Text = datostomodify[3].ToString();
                    //this.txtAbono.Text = datostomodify[4].ToString();
                    this.txtObser.Text = datostomodify["Observaciones"].ToString();
                    this.txtFecha.Text = DateTime.Parse(datostomodify["fecha"].ToString()).ToString("dd/MM/yyyy");
                    
                    this.drpdlGrupoCatalogos.DataBind();
                    this.drpdlGrupoCatalogos.SelectedValue = datostomodify["grupoCatalogosID"].ToString();

                    this.drpdlCatalogoInterno.DataBind();
                    this.drpdlCatalogoInterno.SelectedValue = datostomodify["catalogoMovBancoID"].ToString();
                    if (datostomodify["subCatalogoMovBancoID"] != null && datostomodify["subCatalogoMovBancoID"].ToString() != "" && datostomodify["subCatalogoMovBancoID"].ToString() != "-1")
                    {
                        this.drpdlSubcatologointerna.DataBind();
                        this.drpdlSubcatologointerna.SelectedValue = datostomodify["subCatalogoMovBancoID"].ToString();
                    }

                    this.txtNumFacturaoLarguillo.Text = datostomodify["facturaOlarguillo"].ToString();
                    this.txtNumCabezas.Text = datostomodify["numCabezas"].ToString();
                    
                    if (datostomodify["cargo"] != null && float.Parse(datostomodify["cargo"].ToString()) > 0)
                    {
                        this.txtMonto.Text = datostomodify["cargo"].ToString();
                        this.cmbTipodeMov.Text = "CARGO";
                    }
                    else
                    {
                        this.txtMonto.Text = datostomodify["abono"].ToString();
                        this.cmbTipodeMov.Text = "ABONO";
                    }
                    this.txtFolio.Text = datostomodify["folio"] != null ? datostomodify["folio"].ToString() : "" ;
                    if (datostomodify["folio"] != null)
                    {
                        this.chbMostrarPnlAddTarjDiesel.Checked = true;
                        //this.pnlAddTarjetaDiesel_CollapsiblePanelExtender.Collapsed = true;
                        this.pnlAddTarjetaDiesel.Visible = true;
                    }
                    else
                    {
                        this.chbMostrarPnlAddTarjDiesel.Checked = false;
                        //this.pnlAddTarjetaDiesel_CollapsiblePanelExtender.Collapsed = false;
                        this.pnlAddTarjetaDiesel.Visible = false;
                    }
                    this.txtFolio.ReadOnly = true;
                    this.txtMontoTarjetaDiesel.Text = datostomodify["monto"] != null ?datostomodify["monto"].ToString() :"";
                    this.txtLitrosTarjetaDiesel.Text = datostomodify["litros"]!=null?datostomodify["litros"].ToString():"";
                    
                }
               
            }
            catch (Exception exception)
            {
                this.lblMensajeOperationresult.Text = myConfig.StrFromMessages("FALLOCARGARMODIFICAR");
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelMensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.txtIDDetails.Text = "-1";
                this.panelagregar.Visible = false;

                return false;
            }
            return true;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try{
                dsMovCajaChica.dtMovCajaChicaDataTable tablaaux = new dsMovCajaChica.dtMovCajaChicaDataTable();
                dsMovCajaChica.dtMovCajaChicaRow dtrowtoupdate = tablaaux.NewdtMovCajaChicaRow();
                string sError = "Error";
                int movID = int.Parse(this.txtIDDetails.Text);
                dtrowtoupdate.nombre = this.txtNombre.Text;
                dtrowtoupdate.Observaciones = this.txtObser.Text;

                /*
                dtrowtoupdate.abono = double.Parse(this.txtAbono.Text);
                            dtrowtoupdate.cargo = double.Parse(this.txtCargo.Text);*/
                dtrowtoupdate.cargo = this.cmbTipodeMov.SelectedIndex == 0 ? double.Parse(this.txtMonto.Text) : 0f;
                dtrowtoupdate.abono = this.cmbTipodeMov.SelectedIndex == 1 ? double.Parse(this.txtMonto.Text) : 0f;

                dtrowtoupdate.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFecha.Text));
                dtrowtoupdate.updateTS = Utils.Now;


                dtrowtoupdate.nombre = this.txtNombre.Text;
                dtrowtoupdate.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFecha.Text));
                dtrowtoupdate.cargo = this.cmbTipodeMov.SelectedIndex == 0 ? double.Parse(this.txtMonto.Text) : 0f;
                dtrowtoupdate.abono = this.cmbTipodeMov.SelectedIndex == 1 ? double.Parse(this.txtMonto.Text) : 0f;
                dtrowtoupdate.updateTS = Utils.Now;
                dtrowtoupdate.Observaciones = this.txtObser.Text;
                dtrowtoupdate.catalogoMovBancoInternoID = int.Parse(this.drpdlCatalogoInterno.SelectedValue);
                if (this.drpdlSubcatologointerna.SelectedIndex > 0)
                    dtrowtoupdate.subCatalogoMovBancoInternoID = int.Parse(this.drpdlSubcatologointerna.SelectedValue);
                dtrowtoupdate.numCabezas = this.txtNumCabezas.Text.Length > 0 ? double.Parse(this.txtNumCabezas.Text) : 0f;
                dtrowtoupdate.facturaOlarguillo = this.txtNumFacturaoLarguillo.Text;
                dtrowtoupdate.bodegaID = int.Parse(this.ddlBodegas.SelectedValue);
                dtrowtoupdate.Bodega = this.ddlBodegas.SelectedItem.Text;
               
                if (dbFunctions.updateMovementdeCajaChica(ref dtrowtoupdate,movID, ref sError, int.Parse(this.Session["USERID"].ToString())))
                {
                    if (this.chbMostrarPnlAddTarjDiesel.Checked)
                    {
                        if (!dbFunctions.upTarjetaDiesel(int.Parse(this.txtFolio.Text),
                                                     float.Parse(this.txtMontoTarjetaDiesel.Text),
                                                     float.Parse(this.txtLitrosTarjetaDiesel.Text),
                                                     ref sError,
                                                     int.Parse(this.Session["USERID"].ToString())))
                            throw new Exception(sError);
                    }
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDECAJACHICA, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), "MODIFICÓ EL MOVIMIENTO DE CAJA CHICA NÚMERO: " + movID.ToString() + ".");
                    this.panelMensaje.Visible = true;
                    this.imagenbien.Visible = true;
                    this.imagenmal.Visible = false;
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("MOVCAJAMODIFIEDEXITO"), this.txtIDDetails.Text);
                    this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION        
                    this.panelagregar.Visible = false;
                    this.limpiarcampos();
                }
                else
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), sError, this.Request.Url.ToString());
                    this.panelMensaje.Visible = true;
                    this.imagenbien.Visible = false;
                    this.imagenmal.Visible = true;
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CREDITOMODIFIEDFAILED"), this.txtIDDetails.Text);
                    this.lblMensajeException.Text = sError;
                    this.panelagregar.Visible = false;

                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL MODIFICAR MOVIMIENTO DE CAJA. EX : " + ex.Message, this.Request.Url.ToString());
            }
        }
        protected void limpiacampos(){
            this.ddlIdCiclo.SelectedIndex = 0;
            this.drpdlCatalogoInterno.SelectedIndex = 0;
            this.drpdlGrupoCatalogos.SelectedIndex = 0;
            if(this.drpdlSubcatologointerna.SelectedIndex>0)
               this.drpdlSubcatologointerna.SelectedIndex= 0;

            this.txtMonto.Text = "";
            this.txtObser.Text = "";
            this.txtNombre.Text = "";
        
        }

        protected void drpdlGrupoCatalogos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlCatalogoInterno.DataBind();
            this.drpdlSubcatologointerna.DataBind();
        }

    
        protected void drpdlProductorBoletas_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            this.drpdlProducto.DataBind();
                        if(this.drpdlProducto.Items.Count>0)
                            this.drpdlProducto.SelectedIndex = 0;
                        this.sdsBoletasdelSistema.DataBind();
                        this.gridBoletasSistema.DataBind();*/
            
        }

        protected void drpdlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.gridBoletasSistema.DataBind();
        }

        protected void btnAgregarBoleta_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnAgregarBoleta_Click1(object sender, EventArgs e)
        {
//             if (this.gridBoletasSistema.SelectedDataKey["Ticket"] != null)
//             {
//                 ListItem item = new ListItem(this.gridBoletasSistema.SelectedDataKey["Ticket"].ToString());
//                 listBoxAgregadas.Items.Add(item);
//                 this.filtraBoletasDeSistema();
//                 this.gridBoletasSistema.SelectedIndex = -1;
// 
// 
//             }
        }
        protected void filtraBoletasDeSistema(){
            string filter = "";
            if (this.listBoxAgregadas.Items.Count > 0)
            {
                filter = " Ticket NOT IN(";
                filter += this.listBoxAgregadas.Items[0].Value;
                for (int i = 1; i < this.listBoxAgregadas.Items.Count; i++)
                {
                    filter += ",";
                    filter += this.listBoxAgregadas.Items[i].ToString();
                }
                filter += ")";
//                 this.sdsBoletasdelSistema.FilterExpression = filter;
//                 this.gridBoletasSistema.DataSourceID = "sdsBoletasdelSistema";
//                 this.gridBoletasSistema.DataBind();
            }

            

        }

        protected void gridBoletasAgregadas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.btnRemoveBoleta.Visible = true;
        }

    
     

        protected void btnQuitarBoletadeAnticipo_Click(object sender, EventArgs e)
        {

            if (this.listBoxAgregadas.SelectedIndex > -1)
            {
                this.listBoxAgregadas.Items.RemoveAt(this.listBoxAgregadas.SelectedIndex);

            }
        }

        protected void ddlBodegas_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BodegaID = int.Parse(this.ddlBodegas.SelectedValue);
        }



        protected void btnAgregarBol_Click(object sender, EventArgs e)
        {
            if (dbFunctions.BoletaAlreadyExists(-1,this.txtNewNumBoleta.Text, this.txtNewTicket.Text) )
            {
                this.lblMsgResult.Text = "ERROR: LA BOLETA YA EXISTE EN EL SISTEMA";
                this.imgNewTache.Visible = true;
                this.imgNewPalomita.Visible = false;
                this.panelNewBoletaResult.Visible = true;
                return;
            }
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                sqlConn.Open();
                dsBoletas.dtBoletasDataTable dtTempBoletas = new dsBoletas.dtBoletasDataTable();
                dsBoletas.dtBoletasRow newRow = dtTempBoletas.NewdtBoletasRow(); // (dsBoletas.dtBoletasRow)dtNewBoleta.NewRow();

                //                 sqlDA.InsertCommand.CommandText += "; SET @Identity = SCOPE_IDENTITY();";
                //                 sqlDA.InsertCommand.Parameters.Add("@Identity", SqlDbType.Int, 0, "boletaID").Direction = ParameterDirection.Output;

                newRow.productorID = int.Parse(this.ddlNewBoletaProductor.SelectedValue);
                newRow.NombreProductor = this.ddlNewBoletaProductor.SelectedItem.Text;
                newRow.NumeroBoleta = this.txtNewNumBoleta.Text;
                newRow.Ticket = this.txtNewTicket.Text;
                newRow.bodegaID = int.Parse(this.ddlNewBoletaBodega.SelectedItem.Value);

                newRow.cicloID = int.Parse(this.ddlCiclo.SelectedItem.Value);

                DateTime dtFechaEntrada = new DateTime();
                if (!DateTime.TryParse(this.txtNewFechaEntrada.Text /*+ " " + this.txtNewHoraEntrada.Text*/, out dtFechaEntrada))
                {
                    DateTime.TryParse(this.txtNewFechaEntrada.Text, out dtFechaEntrada);
                }
                newRow.FechaEntrada = dtFechaEntrada;
                double dPesoEntrada = 0;
                double.TryParse(this.txtNewPesoEntrada.Text, out dPesoEntrada);
                newRow.PesoDeEntrada = dPesoEntrada;

                DateTime dtFechaSalida = new DateTime();
                if (this.chkChangeFechaSalidaNewBoleta.Checked)
                {
                    if (!DateTime.TryParse(this.txtNewFechaSalida.Text /*+ " " + this.txtNewHoraEntrada.Text*/, out dtFechaSalida))
                    {
                        DateTime.TryParse(this.txtNewFechaSalida.Text, out dtFechaSalida);
                    }
                }
                else
                {
                    dtFechaSalida = dtFechaEntrada;
                }
                newRow.FechaSalida = dtFechaSalida;
                double dPesoSalida = 0;
                double.TryParse(this.txtNewPesoSalida.Text, out dPesoSalida);
                newRow.PesoDeSalida = dPesoSalida;

                if (newRow.PesoDeEntrada - newRow.PesoDeSalida > 0)
                {
                    newRow.pesonetoentrada = newRow.PesoDeEntrada - newRow.PesoDeSalida;
                    newRow.pesonetosalida = 0;
                }
                else
                {
                    newRow.pesonetoentrada = 0;
                    newRow.pesonetosalida = newRow.PesoDeSalida - newRow.PesoDeEntrada;
                }


                newRow.productoID = int.Parse(this.ddlNewBoletaProducto.SelectedValue);
                newRow.Producto = this.ddlNewBoletaProducto.SelectedItem.Text;

                double dHumedad = 0;
                double.TryParse(this.txtNewHumedad.Text, out dHumedad);
                newRow.humedad = dHumedad;
                double dImpurezas = 0;
                double.TryParse(this.txtNewImpurezas.Text, out dImpurezas);
                newRow.impurezas = dImpurezas;
                decimal dPrecio = 0;
                decimal.TryParse(this.txtNewPrecio.Text, out dPrecio);
                newRow.precioapagar = dPrecio;
                double dSecado = 0;
                double.TryParse(this.txtNewSecado.Text, out dSecado);
                newRow.dctoSecado = dSecado;

                newRow.userID = int.Parse(this.Session["USERID"].ToString());

                dtTempBoletas.AdddtBoletasRow(newRow);

                //                 dtNewBoleta.ImportRow(dtTempBoletas.Rows[0]);
                //                 sqlDA.Update(dtNewBoleta.GetChanges(DataRowState.Added));
                //                 dtNewBoleta.AcceptChanges();
                //                 DataRow nRow = dtNewBoleta.Rows[dtNewBoleta.Rows.Count - 1];

                SqlConnection addConn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand addComm = new SqlCommand();
                addComm.Connection = addConn;
                addConn.Open();
                addComm.CommandText = "INSERT INTO Boletas (productorID, NombreProductor, NumeroBoleta, Ticket, bodegaID, cicloID, FechaEntrada, PesoDeEntrada, FechaSalida, PesoDeSalida, pesonetoentrada,  pesonetosalida, productoID, humedad, impurezas, precioapagar, dctoSecado, userID) VALUES     (@productorID,@NombreProductor,@NumeroBoleta,@Ticket,@bodegaID,@cicloID,@FechaEntrada,@PesoDeEntrada,@FechaSalida,@PesoDeSalida,@pesonetoentrada,@pesonetosalida,@productoID,@humedad,@impurezas,@precioapagar,@dctoSecado,@userID); select boletaID = SCOPE_IDENTITY();";

                addComm.Parameters.Add("@productorID", SqlDbType.Int).Value = newRow.productorID;
                // addComm.Parameters.AddWithValue("@productorID", newRow.productorID);
                addComm.Parameters.Add("@NombreProductor", SqlDbType.NVarChar).Value = newRow.NombreProductor;
                //addComm.Parameters.AddWithValue("@NombreProductor", newRow.NombreProductor);
                addComm.Parameters.Add("@NumeroBoleta", SqlDbType.NVarChar).Value = newRow.NumeroBoleta;
                //addComm.Parameters.AddWithValue("@NumeroBoleta", newRow.NumeroBoleta);
                //  addComm.Parameters.AddWithValue("@Ticket", newRow.Ticket);
                addComm.Parameters.Add("@Ticket", SqlDbType.NVarChar).Value = newRow.Ticket;
                //  addComm.Parameters.AddWithValue("@bodegaID", newRow.bodegaID);
                addComm.Parameters.Add("@bodegaID", SqlDbType.Int).Value = newRow.bodegaID;
                //  addComm.Parameters.AddWithValue("@cicloID", newRow.cicloID);
                addComm.Parameters.Add("@cicloID", SqlDbType.Int).Value = newRow.cicloID;
                //  addComm.Parameters.AddWithValue("@FechaEntrada", newRow.FechaEntrada);
                addComm.Parameters.Add("@FechaEntrada", SqlDbType.DateTime).Value = newRow.FechaEntrada;
                addComm.Parameters.Add("@PesoDeEntrada", SqlDbType.Float).Value = (float)newRow.PesoDeEntrada;
                addComm.Parameters.Add("@FechaSalida", SqlDbType.DateTime).Value = newRow.FechaSalida;
                // addComm.Parameters.AddWithValue("@FechaSalida", newRow.FechaSalida);
                addComm.Parameters.Add("@PesoDeSalida", SqlDbType.Float).Value = newRow.PesoDeSalida;
                addComm.Parameters.Add("@pesonetoentrada", SqlDbType.Float).Value = newRow.pesonetoentrada;
                addComm.Parameters.Add("@pesonetosalida", SqlDbType.Float).Value = newRow.pesonetosalida;
                // addComm.Parameters.AddWithValue("@productoID", newRow.productoID);
                addComm.Parameters.Add("@productoID", SqlDbType.Int).Value = newRow.productoID;

                addComm.Parameters.Add("@humedad", SqlDbType.Float).Value = newRow.humedad;
                addComm.Parameters.Add("@impurezas", SqlDbType.Float).Value = newRow.impurezas;
                addComm.Parameters.Add("@precioapagar", SqlDbType.Float).Value = newRow.precioapagar;
                addComm.Parameters.Add("@dctoSecado", SqlDbType.Float).Value = newRow.dctoSecado;
                //addComm.Parameters.AddWithValue("@userID", newRow.userID);
                addComm.Parameters.Add("@userID", SqlDbType.Int).Value = newRow.userID;

                newRow.boletaID = int.Parse(addComm.ExecuteScalar().ToString());

                newRow.boletaID = int.Parse(addComm.ExecuteScalar().ToString());
                ListItem item = new ListItem(newRow.Ticket);
                this.listBoxAgregadas.Items.Add(item);

                this.lblMsgResult.Text = "LA BOLETA FUÉ INGRESADA Y ASIGNADA A LA LIQUIDACIÓN";
                this.imgNewTache.Visible = false;
                this.imgNewPalomita.Visible = true;
                this.panelNewBoletaResult.Visible = true;

                this.ddlCiclo.DataBind();
                this.ddlCiclo.SelectedIndex = 0;
                this.ddlNewBoletaProductor.DataBind();
                this.ddlNewBoletaProductor.SelectedIndex = 0;
                this.txtNewNumBoleta.Text = "";
                this.txtNewTicket.Text = "";
                this.ddlNewBoletaProducto.DataBind();
                this.ddlNewBoletaProducto.SelectedIndex = 0;
                this.ddlNewBoletaBodega.DataBind();
                this.ddlNewBoletaBodega.SelectedIndex = 0;
                this.txtNewFechaEntrada.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.txtNewPesoEntrada.Text = this.txtNewPesoSalida.Text = this.txtPesoNetoNewBoleta.Text = "0";
                this.txtNewHumedad.Text = this.txtNewImpurezas.Text = this.txtNewPrecio.Text = this.txtNewSecado.Text = "0";

                this.chkNewBoleta.Checked = false;
                this.divNewBoleta.Attributes.Add("style", this.chkNewBoleta.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                /*
                this.drpdlProductorBoletas.DataBind();
                                this.drpdlProducto.DataBind();
                                this.gridBoletasSistema.DataBind();*/
                

            }
            catch (System.Exception ex)
            {
                this.imgNewTache.Visible = true;
                this.imgNewPalomita.Visible = false;
                this.lblMsgResult.Text = "ERROR INGRESANDO LA BOLETA: " + ex.Message;
                this.panelNewBoletaResult.Visible = true;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(Session["USERID"].ToString()), "Error Insertando Nueva Boleta EX:" + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                sqlConn.Close();
            }
        }

        protected void btnAgregarBoletadesdeTicket_Click(object sender, EventArgs e)
        {
            if (this.txtTicket.Text.Length > 0)
            {
                if (dbFunctions.BoletaAlreadyExists(-1,"-1",this.txtTicket.Text))
                {
                   return;
                }
                if (dbFunctions.insertaBoletaEnBlanco(this.txtTicket.Text, int.Parse(drpdlProductor.SelectedValue), this.drpdlProductor.SelectedItem.Text, int.Parse(this.ddlCicloQuickBoleta.SelectedValue), int.Parse(this.ddlNewQuickBoletaProducto.SelectedValue), int.Parse(this.ddlNewQuickBoletaBodega.SelectedValue), int.Parse(this.Session["userID"].ToString())))
                {
                    ListItem item = new ListItem(this.txtTicket.Text);
                    this.listBoxAgregadas.Items.Add(item);
                    this.txtTicket.Text = "";
                    this.filtraBoletasDeSistema();
                }
            } 
        }

        protected void btnAddTarjetaDiesel_Click(object sender, EventArgs e)
        {
            string sqlIns = "Insert into TarjetasDiesel(folio, monto, litros) values (@folio, @monto, @litros);";
            //sqlIns += "Insert into MovimientosCaja_TarjetasDiesel(movimientoID, folio) values (@movimientoID, @folio)";
            SqlConnection addConn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand addComm = new SqlCommand(sqlIns,addConn);
            try
            {
                addComm.Parameters.Add("@folio", SqlDbType.Int).Value = int.Parse(this.txtFolio.Text);
                addComm.Parameters.Add("@monto", SqlDbType.Float).Value = double.Parse(this.txtMontoTarjetaDiesel.Text);
                addComm.Parameters.Add("@litros", SqlDbType.Float).Value = double.Parse(this.txtLitrosTarjetaDiesel.Text);
                
                addConn.Open();
                addComm.ExecuteNonQuery();
                this.chbMostrarPnlAddTarjDiesel.Checked = false;
          
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "ERROR AL INSERTAR LA TARJETA DIESEL. EX : " + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                addConn.Close();
            }
            this.txtFolio.ReadOnly = true;
            //this.btnAddTarjetaDiesel.Visible = false;
            //this.btnModTarjetaDiesel.Visible = true;

        }

        protected void btnModTarjetaDiesel_Click(object sender, EventArgs e)
        {
            string sqlIns = "Update TarjetasDiesel set monto=@monto, litros=@litros) Where folio = @folio";
            SqlConnection addConn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand addComm = new SqlCommand(sqlIns, addConn);
            try
            {
                addComm.Parameters.Add("@monto", SqlDbType.Float).Value = double.Parse(this.txtMontoTarjetaDiesel.Text);
                addComm.Parameters.Add("@litros", SqlDbType.Float).Value = double.Parse(this.txtLitrosTarjetaDiesel.Text);
                addComm.Parameters.Add("@folio", SqlDbType.Int).Value = int.Parse(this.txtFolio.Text);
                addConn.Open();
                addComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL MODIFICAR LA TARJETA DIESEL. EX : " + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                addConn.Close();
            }
        }

       
        

       
    }
}
