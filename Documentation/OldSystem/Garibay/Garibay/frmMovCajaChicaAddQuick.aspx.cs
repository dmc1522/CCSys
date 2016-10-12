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
    public partial class frmMovCajaChicaAddQuick : Garibay.BasePage
    {
        internal void AddJSInCtrls()
        {
            JSUtils.closeCurrentWindow(ref this.btnAceptardtlst);
            this.divPrestamo.Attributes.Add("style", this.drpdlTipoAnticipo.SelectedItem.Text == "PRESTAMO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
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
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.compruebasecurityLevel();
            try
            {
                if (!this.IsPostBack)
                {
                    this.lblReminder.Visible = true;

                    this.AddJSInCtrls();
                    this.chkboxAnticipo.Visible = false;
                    this.txtNewFechaEntrada.Text = Utils.Now.ToString("dd/MM/yyyy");
                    
                    this.panelMensaje.Visible = false;
                    this.ddlIdCiclo.DataBind();
                    this.drpdlGrupoCatalogos.DataBind();
                    this.drpdlGrupoCatalogos.DataBind();
                    this.drpdlSubcatologointerna.DataBind();
                    this.drpdlTipoAnticipo.DataBind();
                    this.ddlBodegas.DataBind();
                    this.ddlBodegas.SelectedValue = this.BodegaID.ToString();

                    if (Request.QueryString["data"] != null)
                    {
                        if (this.loadqueryStrings(Request.QueryString["data"].ToString()))
                        {
                            if(myQueryStrings["idtomodify"]!=null)
                            {
                                //SE VA A MODIFICAR EL MOVIMIENTO
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
                                this.btnModificar.Visible = true;
                                this.cmdAceptar.Visible = false;
                            }
                            if(myQueryStrings["notaventaID"]!=null)
                            {
                                //RECIBIMOS UN NOTA DE VENTA ID
                                this.lblNotadeVentaID.Text = myQueryStrings["notaventaID"].ToString();
                                this.lblMovCajaChica.Text = "AGREGAR MOVIMIENTO DE CAJA CHICA A LA NOTA DE VENTA CON EL ID: " + this.lblNotadeVentaID.Text;
                                this.btnModificar.Visible = false;
                                this.cmdAceptar.Visible = true;


                            }
                            if (myQueryStrings["notacompraID"] != null)
                            {
                                //RECIBIMOS UN NOTA DE COMPRA ID
                                this.lblNotadeCompraID.Text = myQueryStrings["notacompraID"].ToString();
                                this.lblMovCajaChica.Text = "AGREGAR MOVIMIENTO DE CAJA CHICA A LA NOTA DE COMPRA CON EL ID: " + this.lblNotadeCompraID.Text;
                                this.btnModificar.Visible = false;
                                this.cmdAceptar.Visible = true;


                            }

                         
                        }
                        else
                        {
                            myQueryStrings.Clear();
                            this.lblNotadeVentaID.Text = "";
                            this.lblMovCajaChica.Text = "AGREGAR UN MOVIMIENTO DE CAJA CHICA";
                            Response.Redirect("~/frmAddMovCajaChica.aspx", true);

                        }
                    }
                    else
                    {

                        this.lblMovCajaChica.Text = "AGREGAR NUEVO MOVIMIENTO DE CAJA CHICA";
                        this.btnAceptardtlst.Visible = true;
                        this.btnModificar.Visible = false;
                        this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
                        this.txtFechaLimite.Text = this.txtFecha.Text;

                    }


                }

                this.divConCuentaYCajaDestino.Attributes.Add("style", (this.drpdlGrupoCatalogos.SelectedItem != null && this.drpdlGrupoCatalogos.SelectedItem.Text.IndexOf("TRASPASOS") > -1) ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.divCajaDestino.Attributes.Add("style", (this.drpdlCatalogoInterno.SelectedItem != null && this.drpdlCatalogoInterno.SelectedItem.Text.IndexOf("CAJA CHICA") > -1) ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.divCuentaDestino.Attributes.Add("style", (this.drpdlCatalogoInterno.SelectedItem != null && this.drpdlCatalogoInterno.SelectedItem.Text.IndexOf("CAJA CHICA") <= -1) ? "visibility: visible; display: block" : "visibility: hidden; display: none");

                //this.gridBoletasSistema.DataSourceID = "sdsBoletasdelSistema";
                this.divanticipo.Attributes.Add("style", this.chkboxAnticipo.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
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
        protected bool cargadatostomodify()
        {
            string qryIns = "SELECT MovimientosCaja.cicloID, MovimientosCaja.nombre, MovimientosCaja.cargo, MovimientosCaja.abono, MovimientosCaja.Observaciones, MovimientosCaja.fecha,  MovimientosCaja.catalogoMovBancoID, MovimientosCaja.subCatalogoMovBancoID, MovimientosCaja.facturaOlarguillo, MovimientosCaja.numCabezas,  GruposCatalogosMovBancos.grupoCatalogosID FROM         MovimientosCaja INNER JOIN catalogoMovimientosBancos ON MovimientosCaja.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID INNER JOIN GruposCatalogosMovBancos ON catalogoMovimientosBancos.grupoCatalogoID = GruposCatalogosMovBancos.grupoCatalogosID where movimientoID = @movimientoID ";
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
                    String sNewMov = dtRowainsertar.movimientoID.ToString();
                    if (this.lblNotadeVentaID.Text != "")
                    {
                        SqlConnection conInsertNota = new SqlConnection(myConfig.ConnectionInfo);
                        string sqlInsert = "insert into Pagos_NotaVenta (fecha, notadeventaID, movimientoID) VALUES(@fecha, @notadeventaID, @movimientoID)";
                        SqlCommand cmdInsert = new SqlCommand(sqlInsert, conInsertNota);
                        conInsertNota.Open();
                        try
                        {
                            cmdInsert.Parameters.Clear();
                            cmdInsert.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtRowainsertar.fecha;
                            cmdInsert.Parameters.Add("@notadeventaID", SqlDbType.Int).Value = int.Parse(this.lblNotadeVentaID.Text);
                            cmdInsert.Parameters.Add("@movimientoID", SqlDbType.Int).Value = dtRowainsertar.movimientoID;
                            int numregistros = cmdInsert.ExecuteNonQuery();
                            if (numregistros != 1)
                            {
                                throw new Exception("ERROR AL INSERTAR RELACION NV - PAGOS. LA DB REGRESÓ QUE SE ALTERARON " + numregistros.ToString() + "REGISTROS");
                            }
                            Logger.Instance.LogUserSessionRecord(Logger.typeModulo.NOTAVENTA, Logger.typeUserActions.UPDATE, this.UserID, "SE INSERTÓ UN PAGO A LA NOTA DE VENTA " + this.lblNotadeVentaID.Text + " EL MOV FUE: " + dtRowainsertar.movimientoID.ToString());
                        }
                        catch (Exception ex)
                        {
                            Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL INSERTAR UN PAGO A LA NOTA: " + this.lblNotadeVentaID.Text + ". EX " + ex.Message, this.Request.Url.ToString());

                        }
                    }
                    if(this.lblNotadeCompraID.Text!="")
                    {
                        SqlConnection conInsertNota = new SqlConnection(myConfig.ConnectionInfo);
                        string sqlInsert = "insert into Pagos_NotaCompra (fecha, notadecompraID, movimientoID) VALUES(@fecha, @notadecompraID, @movimientoID)";
                        SqlCommand cmdInsert = new SqlCommand(sqlInsert, conInsertNota);
                        conInsertNota.Open();
                        try
                        {
                            cmdInsert.Parameters.Clear();
                            cmdInsert.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtRowainsertar.fecha;
                            cmdInsert.Parameters.Add("@notadecompraID", SqlDbType.Int).Value = int.Parse(this.lblNotadeCompraID.Text);
                            cmdInsert.Parameters.Add("@movimientoID", SqlDbType.Int).Value = dtRowainsertar.movimientoID;
                            int numregistros = cmdInsert.ExecuteNonQuery();
                            if (numregistros != 1)
                            {
                                throw new Exception("ERROR AL INSERTAR RELACION NV - PAGOS. LA DB REGRESÓ QUE SE ALTERARON " + numregistros.ToString() + "REGISTROS");
                            }
                            Logger.Instance.LogUserSessionRecord(Logger.typeModulo.NOTACOMPRA, Logger.typeUserActions.UPDATE, this.UserID, "SE INSERTÓ UN PAGO A LA NOTA DE COMPRA " + this.lblNotadeVentaID.Text + " EL MOV DE CAJA CHICA FUE: " + dtRowainsertar.movimientoID.ToString());

                        }
                        catch (Exception ex)
                        {
                            Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL INSERTAR UN PAGO A LA NOTA (CAJA CHICA): " + this.lblNotadeVentaID.Text + ". EX " + ex.Message, this.Request.Url.ToString());

                        }


                    }
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
                    this.lblReminder.Visible = true;
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDECAJACHICA, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), "AGREGÓ EL MOVIMIENTO DE CAJA CHICA NÚMERO: " + dtRowainsertar.movimientoID.ToString());

                }
                else
                {
                    this.panelagregar.Visible = false;
                    this.panelMensaje.Visible = true;
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("MOVCAJAADDEDFAILED"), dtRowainsertar.movimientoID.ToString());
                    this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), serror, this.Request.Url.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "ERROR AL INSERTAR MOVIMIENTO DE CAJA. EX : " + ex.Message, this.Request.Url.ToString());
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
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

                if (dbFunctions.updateMovementdeCajaChica(ref dtrowtoupdate, movID, ref sError, int.Parse(this.Session["USERID"].ToString())))
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDECAJACHICA, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), "MODIFICÓ EL MOVIMIENTO DE CAJA CHICA NÚMERO: " + movID.ToString() + ".");
                    this.panelMensaje.Visible = true;
                    this.imagenbien.Visible = true;
                    this.imagenmal.Visible = false;
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("MOVCAJAMODIFIEDEXITO"), this.txtIDDetails.Text);
                    this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION        
                    this.panelagregar.Visible = false;
                    this.limpiacampos();
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

        protected void limpiacampos()
        {
            this.ddlIdCiclo.SelectedIndex = 0;
            this.drpdlCatalogoInterno.SelectedIndex = 0;
            this.drpdlGrupoCatalogos.SelectedIndex = 0;
            if (this.drpdlSubcatologointerna.SelectedIndex > 0)
                this.drpdlSubcatologointerna.SelectedIndex = 0;

            this.txtMonto.Text = "";
            this.txtObser.Text = "";
            this.txtNombre.Text = "";

        }

        protected void cmdCancelar_Click(object sender, EventArgs e)
        {

        }

        protected void btnAgregarBol_Click(object sender, EventArgs e)
        {

        }

        protected void btnQuitarBoletadeAnticipo_Click(object sender, EventArgs e)
        {

        }

        protected void ddlBodegas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpdlGrupoCatalogos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpdlCatalogoInterno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAgregarBoletadesdeTicket_Click(object sender, EventArgs e)
        {

        }

      
    }
}
