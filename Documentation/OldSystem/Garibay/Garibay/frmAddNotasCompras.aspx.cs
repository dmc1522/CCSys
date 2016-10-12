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
using System.Globalization;


namespace Garibay
{
    public partial class frmAddNotasCompras : Garibay.BasePage
    {
        private void JavaScriptABotones()
        {
            JSUtils.AddDisableWhenClick(ref this.btnActualizaTotales, this);
            JSUtils.AddDisableWhenClick(ref this.btnAddPago, this);
            JSUtils.AddDisableWhenClick(ref this.btnAddproduct, this);
            JSUtils.AddDisableWhenClick(ref this.btnAgregarNewNotaCompra, this);
            JSUtils.AddDisableWhenClick(ref this.btnGuardaNotaCompra, this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.txtMonto.Text = "1";
            this.divAgregarNuevoPago.Attributes.Add("style", this.chkMostrarAgregarPago.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");           
            if (!IsPostBack)
            {
                this.JavaScriptABotones();
                try
                {
                    this.compruebasecurityLevel();
                    this.drpdlProveedor.DataBind();
                    this.drpdlBodega.DataBind();
                    this.drpdlCiclo.DataBind();
                    this.drpdlProducto.DataBind();
                    if (this.drpdlProveedor.Items.Count>0)
                    {
                        this.drpdlProveedor.SelectedIndex = 0;
                        this.cargadatosProveedor(int.Parse(this.drpdlProveedor.SelectedValue));
                    }
                    this.txtFecha.Text = this.txtFechapago.Text = Utils.Now.ToString("dd/MM/yyyy");
                    this.pnlCentral.Visible = false;
                    if (this.LoadEncryptedQueryString() > 0 && this.myQueryStrings["NotaComID"] != null)
                    {
                        this.txtNotaIDToMod.Text = this.myQueryStrings["NotaComID"].ToString();
                        this.LoadNota(this.myQueryStrings["NotaComID"].ToString());
                        this.pnlCentral.Visible = true;
                        this.btnAgregarNewNotaCompra.Visible = false;
                    }
                    else
                    {
                        this.chkPnlAddProd.Checked = true;
                    }
                    this.AddjstoControls();
                    this.divPagoMovCaja.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                    String sOnchagemov = "checkValueInList(";
                    sOnchagemov += "this" + ",'EFECTIVO','";
                    sOnchagemov += this.divPagoMovCaja.ClientID + "');";
                    this.cmbTipodeMovPago.Attributes.Add("onChange", sOnchagemov);
                    this.divMovBanco.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "MOVIMIENTO DE BANCO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                    sOnchagemov += "checkValueInList(";
                    sOnchagemov += "this" + ",'MOVIMIENTO DE BANCO','";
                    sOnchagemov += this.divMovBanco.ClientID + "');";
                    this.cmbTipodeMovPago.Attributes.Add("onChange", sOnchagemov);
                    this.cmbConceptomovBancoPago.DataBind();
                    this.divCheque.Attributes.Add("style", this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                    sOnchagemov = "checkValueInList(";
                    sOnchagemov += "this" + ",'CHEQUE','";
                    sOnchagemov += this.divCheque.ClientID + "');";
                    this.cmbConceptomovBancoPago.Attributes.Add("onChange", sOnchagemov);
                    this.imgBienPago.Visible = false;
                    this.imgMalPago.Visible = false;
                    this.pnlNewPago.Visible = false;
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Page_Load", this.Request.Url.ToString(), ref ex);
                }
            }


            ////
            this.divPagoMovCaja.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divMovBanco.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "MOVIMIENTO DE BANCO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divCheque.Attributes.Add("style", this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            ///

            this.pnlNotaCompraResult.Visible = false;
        }

        private void AddjstoControls()
        {
            String sOnchangeMul = "mulTextBoxesNotNeg('";
            sOnchangeMul += this.txtCantidad.ClientID + "','";
            sOnchangeMul += this.txtPrecio.ClientID + "','" + this.txtImporte.ClientID + "');";
            this.txtCantidad.Attributes.Add("onChange", sOnchangeMul);
            this.txtPrecio.Attributes.Add("onChange", sOnchangeMul);

            String sOnchangeAB = "ShowHideDivOnChkBox('";
            sOnchangeAB += this.chkMostrarAgregarPago.ClientID + "','";
            sOnchangeAB += this.divAgregarNuevoPago.ClientID + "')";
            this.chkMostrarAgregarPago.Attributes.Add("onclick", sOnchangeAB);
        }

        protected void btnAgregarNewNotaCompra_Click(object sender, EventArgs e)
        {
            this.pnlCentral.Visible = true;
            this.btnAgregarNewNotaCompra.Visible = false;

            try
            {

                NotasDeCompra nota = new NotasDeCompra();
                nota.ProveedorID = int.Parse(this.drpdlProveedor.SelectedValue);
                nota.CicloID = int.Parse(this.drpdlCiclo.SelectedValue);
                nota.Fecha = DateTime.Parse(this.txtFecha.Text); 
                nota.Folio =  this.txtNumNota.Text;
                nota.Observaciones = this.txtObservaciones.Text;
                nota.UserID = this.UserID;
                nota.StoreTS = nota.UpdateTS = Utils.Now;
                nota.TipomonedaID = int.Parse(this.ddlTipoDeMoneda.SelectedItem.Value);
                nota.LlevaIVA = false;
                nota.Pagada = false;
                nota.Insert();

                if (nota.NotadecompraID > 0)
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.NOTACOMPRA, Logger.typeUserActions.INSERT, int.Parse(Session["USERID"].ToString()), "AGREGÓ LA NOTA DE COMPRA:" + nota.NotadecompraID);
                    String sQuery = "NotaComID=" + nota.NotadecompraID.ToString();
                    sQuery = Utils.GetEncriptedQueryString(sQuery);
                    String strRedirect = "~/frmAddNotasCompras.aspx" + sQuery;
                    Response.Redirect(strRedirect);
                }
                else
                    throw new Exception("error insertando nota de compra");
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "btnAgregarNewNotaCompra_Click", this.Request.Url.ToString(), ref ex);
            }
        }

        protected void drpdlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            
                cargadatosProveedor(int.Parse(this.drpdlProveedor.SelectedValue));
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "drpdlProveedor_SelectedIndexChanged", ref ex);
            }
        }

        protected bool cargadatosProveedor(int proveedorID)
        {
            bool bTodoBien = false;
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                Proveedores prov = Proveedores.Get(proveedorID);
                this.txtDomicilio.Text = prov.Direccion;
                this.txtCP.Text = prov.Cp;
                this.txtComunidad.Text = prov.Comunidad;
                this.txtNomContacto.Text = prov.Nombrecontacto;
                this.txtTelefono.Text = prov.Teléfono;
                this.txtCelular.Text = prov.Celular;
                this.txtMunicipio.Text = prov.Municipio;

                this.txtFechaNPago.Text = this.PopCalendar3.SelectedDate;
                this.txtNombrePago.Text = this.drpdlProveedor.SelectedItem.Text;
                this.txtChequeNombre.Text = this.txtNombrePago.Text;
                bTodoBien = true;
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "cargadatosProveedor", ref ex);
                bTodoBien = false;
            }
            return bTodoBien;
        }

        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
            }

        }

        private void LoadNota(string NotaID)
        {
            try
            {
                NotasDeCompra nota = NotasDeCompra.Get(int.Parse(NotaID));
                this.drpdlProveedor.SelectedValue = nota.ProveedorID.ToString();
                this.drpdlProveedor_SelectedIndexChanged(null, null);
                this.drpdlCiclo.SelectedValue = nota.CicloID.ToString();
                this.PopCalendar3.SelectedDate = Utils.converttoshortFormatfromdbFormat(nota.Fecha.ToString());
                this.txtNumNota.Text = nota.Folio.ToString();
                this.ddlTipoDeMoneda.DataBind();
                this.ddlTipoDeMoneda.SelectedValue = nota.TipomonedaID.ToString();
                this.ActualizaTotales();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "LoadNota", this.Request.Url.ToString(), ref ex);
            }
        }

        protected void btnAddproduct_Click(object sender, EventArgs e)
        {
            try
            {
                NotasDeCompraDetalle detalle = new NotasDeCompraDetalle();
                detalle.ProductoID = int.Parse(this.drpdlProducto.SelectedValue);
                detalle.Preciodecompra = decimal.Parse(this.txtPrecio.Text);
                detalle.NotadecompraID = int.Parse(this.txtNotaIDToMod.Text);

                NotasDeCompraDetalle []detalles = NotasDeCompraDetalle.Search(detalle);

                if (detalles.Length == 1)
                {
                    detalles[0].Cantidad += double.Parse(this.txtCantidad.Text);
                    detalles[0].Sacos = 1;
                    detalles[0].Update();
                }
                else
                {
                    detalle.NotadecompraID = int.Parse(this.txtNotaIDToMod.Text);
                    detalle.ProductoID = int.Parse(this.drpdlProducto.SelectedValue);
                    detalle.BodegaID = int.Parse(this.drpdlBodega.SelectedValue);
                    detalle.Cantidad = double.Parse(this.txtCantidad.Text);
                    detalle.Preciodecompra = decimal.Parse(this.txtPrecio.Text);
                    detalle.Sacos = 1;
                    detalle.Insert();
                }
                this.txtCantidad.Text = this.txtPrecio.Text = this.txtImporte.Text = "";
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error agregando producto a nota", this.Request.Url.ToString(), ref ex);
            }
            this.grdvProNotas.DataBind();
            this.btnGuardaNotaCompra_Click(null, null);
        }

        private void ActualizaTotales()
        {
            this.dvTotales.DataBind();
        }

        protected void grdvProNotas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //this.grdvProNotas.EditIndex = e.NewEditIndex;
            //this.grdvProNotas.DataBind();
            //DropDownList ddlProd = (DropDownList)this.grdvProNotas.Rows[this.grdvProNotas.EditIndex].FindControl("ddlProdEdit");
            //ddlProd.DataBind();
            //ddlProd.SelectedValue = this.grdvProNotas.DataKeys[1].Value.ToString();
        }

        protected void grdvProNotas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grdvProNotas.EditIndex = -1;
            this.grdvProNotas.DataBind();
        }

        protected void grdvProNotas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        protected void grdvProNotas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
        }

        protected void btnGuardaNotaCompra_Click(object sender, EventArgs e)
        {
            try
            {
                NotasDeCompra nota = NotasDeCompra.Get(int.Parse(this.txtNotaIDToMod.Text));
                nota.ProveedorID = int.Parse(this.drpdlProveedor.SelectedValue);
                nota.CicloID = int.Parse(this.drpdlCiclo.SelectedValue);
                nota.Fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFecha.Text));
                nota.Folio = this.txtNumNota.Text;
                nota.Observaciones = this.txtObservaciones.Text;
                nota.Fechapago = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechapago.Text));
                nota.TipomonedaID = int.Parse(this.ddlTipoDeMoneda.SelectedItem.Value);
                nota.Update();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "err updating NOTA", ref ex);
                this.imgBien.Visible = false;
                this.imgMal.Visible = this.pnlNotaCompraResult.Visible = true;
                this.lblNotaCompraResult.Visible = true;
                this.lblNotaCompraResult.Text = "ESTO ES VERGONZOSO, HA OCURRIDO UNA EXCEPCION Y NO SE PUDO GUARDAR LA NOTA.<BR>POR FAVOR ESPERE UN MOMENTO E INTENTELO DE NUEVO<BR>Descripción del error<BR>" + ex.Message;
            }
        }

        protected void chbIVA_CheckedChanged1(object sender, EventArgs e)
        {
            this.ActualizaTotales();
        }

        

        protected void btnUpdateMovCaja_Click(object sender, EventArgs e)
        {
            //this.grdvMovCaja.DataBind();
        }

        protected void btnActualizaTotales_Click1(object sender, EventArgs e)
        {
            this.ActualizaTotales();
        }

        

        
        protected void actualizaPagos()
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand comm = new SqlCommand();
            try
            {
                String sqry = "SELECT     SUM(ISNULL(MovimientosCuentasBanco.cargo, MovimientosCaja.cargo) * Pagos_NotaCompra.tasaDeCambio) AS Abono_Aplicable "
                    + " FROM Pagos_NotaCompra LEFT OUTER JOIN "
                    + " ConceptosMovCuentas INNER JOIN "
                    + " MovimientosCuentasBanco ON ConceptosMovCuentas.ConceptoMovCuentaID = MovimientosCuentasBanco.ConceptoMovCuentaID INNER JOIN "
                    + " CuentasDeBanco ON MovimientosCuentasBanco.cuentaID = CuentasDeBanco.cuentaID INNER JOIN "
                    + " TiposDeMoneda ON CuentasDeBanco.tipomonedaID = TiposDeMoneda.tipomonedaID ON "
                    + " Pagos_NotaCompra.movbanID = MovimientosCuentasBanco.movbanID LEFT OUTER JOIN "
                    + " MovimientosCaja INNER JOIN "
                    + " TiposDeMoneda AS TiposDeMoneda_1 ON MovimientosCaja.tipomonedaID = TiposDeMoneda_1.tipomonedaID ON "
                    + " Pagos_NotaCompra.movimientoID = MovimientosCaja.movimientoID "
                    + " WHERE     (Pagos_NotaCompra.notadecompraID = @notadecompraID)";

                conn.Open();
                comm.Connection = conn;
                comm.CommandText = sqry;
                comm.Parameters.Add("@notadecompraID", SqlDbType.Int).Value = this.txtNotaIDToMod.Text;
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "actualizar Pagos de la Nota de Compra", ref ex);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnAddPago_Click(object sender, EventArgs e)
        {
            this.pnlNewPago.Visible = false;
            if(this.cmbTipodeMovPago.SelectedValue == "0")//Es movimiento de caja chica
            {
                try
                {
                    dsMovCajaChica.dtMovCajaChicaDataTable tablaaux = new dsMovCajaChica.dtMovCajaChicaDataTable();
                    dsMovCajaChica.dtMovCajaChicaRow dtRowainsertar = tablaaux.NewdtMovCajaChicaRow();
                    dtRowainsertar.nombre = this.txtNombrePago.Text;
                    dtRowainsertar.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaNPago.Text));
                    try
                    {
                        dtRowainsertar.cargo = double.Parse(this.txtMonto.Text);
                    }
                    catch
                    {
                        dtRowainsertar.cargo = 0.0f;
                    }
                    
                    dtRowainsertar.abono = 0.00;
                    dtRowainsertar.storeTS = DateTime.Parse(Utils.getNowFormattedDate());
                    dtRowainsertar.updateTS = DateTime.Parse(Utils.getNowFormattedDate());
                    dtRowainsertar.Observaciones = "Pago a nota de compra";
                    dtRowainsertar.catalogoMovBancoInternoID = int.Parse(this.drpdlCatalogocuentaCajaChica.SelectedValue);
                    if (this.drpdlSubcatalogoCajaChica.SelectedIndex > 0)
                        dtRowainsertar.subCatalogoMovBancoInternoID = int.Parse(this.drpdlSubcatalogoCajaChica.SelectedValue);
                    dtRowainsertar.numCabezas = 0;
                    dtRowainsertar.facturaOlarguillo = "";
                    dtRowainsertar.bodegaID = int.Parse(this.ddlPagosBodegas.SelectedValue);
                    dtRowainsertar.cobrado = true;
                    //dtRowainsertar.Bodega = this.ddlBodegas.SelectedItem.Text;

                    String serror = "";
                    ListBox listBoxAgregadas = new ListBox();
                    if (dbFunctions.insertaMovCaja(ref dtRowainsertar, ref serror, this.UserID, int.Parse(this.drpdlCiclo.SelectedValue)))
                    {
                        String sNewMov = dtRowainsertar.movimientoID.ToString();
                        SqlConnection conInsertNota = new SqlConnection(myConfig.ConnectionInfo);
                        string sqlInsert = "INSERT INTO Pagos_NotaCompra(notadecompraID, movimientoID) VALUES (@notadecompraID, @movimientoID);";
                        try
                        {
                            SqlCommand cmdInsert = new SqlCommand(sqlInsert, conInsertNota);
                            conInsertNota.Open();
                            cmdInsert.Parameters.Add("@notadecompraID", SqlDbType.Int).Value = int.Parse(this.txtNotaIDToMod.Text);
                            cmdInsert.Parameters.Add("@movimientoID", SqlDbType.Int).Value = dtRowainsertar.movimientoID;
                            if (cmdInsert.ExecuteNonQuery() != 1)
                            {
                                throw new Exception("This must almost never happen");
                            }

                            Logger.Instance.LogUserSessionRecord(Logger.typeModulo.NOTACOMPRA, Logger.typeUserActions.UPDATE, this.UserID, "SE INSERTÓ UN PAGO A LA NOTA DE COMPRA " + this.txtNotaIDToMod.Text + " EL MOV DE CAJA CHICA FUE: " + dtRowainsertar.movimientoID.ToString());

                        }
                        catch (Exception ex)
                        {
                            Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL INSERTAR UN PAGO A LA NOTA (CAJA CHICA): " + this.txtNotaIDToMod.Text + ". EX " + ex.Message, this.Request.Url.ToString());
                        }
                        finally
                        {
                            conInsertNota.Close();
                        }
                        //this.pnlNewPago.Visible = true;
                        //this.imgBienPago.Visible = true;
                        //this.imgMalPago.Visible = false;
                        //this.lblNewPagoResult.Text = string.Format(myConfig.StrFromMessages("MOVCAJAADDEDEXITO"), sNewMov);
                        Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDECAJACHICA, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), "AGREGÓ EL MOVIMIENTO DE CAJA CHICA NÚMERO: " + dtRowainsertar.movimientoID.ToString());
                        this.ActualizaTotales();
                    }
                    else
                    {
                        this.pnlNewPago.Visible = true;
                        this.imgBienPago.Visible = false;
                        this.imgMalPago.Visible = true;
                        this.lblNewPagoResult.Text = string.Format(myConfig.StrFromMessages("MOVCAJAADDEDFAILED"), dtRowainsertar.movimientoID.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "ERROR AL INSERTAR MOVIMIENTO DE CAJA. EX : " + ex.Message, this.Request.Url.ToString());
                }
            }
            else
                if (this.cmbTipodeMovPago.SelectedValue == "1")//Es movimiento de banco
            {
                int cheque = 0;
                bool hayerrorenmonto = false;
                double monto = 0;
                double.TryParse(this.txtMonto.Text, out monto);
                if (monto == 0)
                {
                    if (this.drpdlCatalogoInternoPago.SelectedItem.Text != "10J -  CHEQUES CANCELADOS")
                    {
                        hayerrorenmonto = true;
                    }
                }
                if (hayerrorenmonto)
                {
                    this.pnlNewPago.Visible = true;
                    this.imgMalPago.Visible = true;
                    this.imgBienPago.Visible = false;
                    this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, ERROR EN MONTO, ESCRIBA CANTIDAD VÁLIDA";
                    return;
                }
                if (dbFunctions.FechaEnPeriodoBloqueado(DateTime.Parse(this.txtFechaNPago.Text), int.Parse(this.cmbCuentaPago.SelectedValue)))
                {
                    this.pnlNewPago.Visible = true;
                    this.imgBienPago.Visible = false;
                    this.imgMalPago.Visible = true;
                    this.lblNewPagoResult.Text = "EL MOVIMIENTO NO PUEDE SER AGREGADO YA QUE LA FECHA ESTA DENTRO DE UN PERIODO BLOQUEADO<BR />DESBLOQUEE EL PERIODO PARA PERMITIR INGRESAR EL MOVIMIENTO";
                    return;
                }

                if (this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE")
                {
                    if (int.TryParse(this.txtChequeNum.Text, out cheque))
                    {
                        if (dbFunctions.ChequeAlreadyExists(cheque, this.cmbCuentaPago.SelectedValue))
                        {
                            this.pnlNewPago.Visible = true;
                            this.imgBienPago.Visible = false;
                            this.imgMalPago.Visible = true;
                            this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, ESTE CHEQUE YA HA SIDO AGREGADO";
                            //this.cmbTipodeMovPago.SelectedIndex = 0;
                            return;
                        }    
                    }else
                    {
                        this.pnlNewPago.Visible = true;
                        this.imgBienPago.Visible = false;
                        this.imgMalPago.Visible = true;
                        this.lblNewPagoResult.Text = "ERROR!! EL NUMERO DE CHEQUE ES INCORRECTO";
                        //this.cmbTipodeMovPago.SelectedIndex = 0;
                        return;
                    }


                    if (!numChequeValido(cheque, int.Parse(this.cmbCuentaPago.SelectedValue)))
                    {

                        this.pnlNewPago.Visible = true;
                        this.imgBienPago.Visible = false;
                        this.imgMal.Visible = false;
                        this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, EL NUMERO DE CHEQUE NO CORRESPONDE A EL NUMERO DE CUENTA";
                        return;
                    }
                }

                //this.cmbTipodeMov.DataBind();
                dsMovBanco.dtMovBancoDataTable tablaaux = new dsMovBanco.dtMovBancoDataTable();
                dsMovBanco.dtMovBancoRow dtRowainsertar = tablaaux.NewdtMovBancoRow();
                //dtRowainsertar.cicloID = int.Parse(this.drpdlCiclo.SelectedValue);
                dtRowainsertar.chequecobrado = true;
                dtRowainsertar.conceptoID = int.Parse(this.cmbConceptomovBancoPago.SelectedValue);
                dtRowainsertar.nombre = this.txtNombrePago.Text;
                dtRowainsertar.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaNPago.Text));
                //dats de cheque
                dtRowainsertar.numCheque = this.txtChequeNum.Text.Length > 0 ? int.Parse(this.txtChequeNum.Text) : 0;
                dtRowainsertar.chequeNombre = this.txtChequeNombre.Text;
                dtRowainsertar.facturaOlarguillo = this.txtFacturaLarguillo.Text;
                dtRowainsertar.numCabezas = 0;//this.txtNumCabezas.Text.Length > 0 ? double.Parse(this.txtNumCabezas.Text) : 0;

                dtRowainsertar.catalogoMovBancoInternoID = int.Parse(this.drpdlCatalogoInternoPago.SelectedValue);
                if (this.drpdlSubcatologointernaPago.SelectedIndex > -1)
                    dtRowainsertar.subCatalogoMovBancoInternoID = int.Parse(this.drpdlSubcatologointernaPago.SelectedValue);
                //if (!this.chkMostrarFiscales.Checked)
                //{
                //    dtRowainsertar.catalogoMovBancoFiscalID = int.Parse(this.drpdlCatalogoInterno.SelectedValue);
                //    if (this.drpdlSubcatologointerna.SelectedIndex > -1)
                //        dtRowainsertar.subCatalogoMovBancoFiscalID = int.Parse(this.drpdlSubcatologointerna.SelectedValue);
                ////}
                //else
                //{
                if (this.cmbConceptomovBancoPago.SelectedItem != null && this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE")
                {
                    dtRowainsertar.catalogoMovBancoFiscalID = int.Parse(this.drpdlCatalogocuentafiscalPago.SelectedValue);
                    if (this.drpdlSubcatalogofiscalPago.SelectedIndex > -1)
                        dtRowainsertar.subCatalogoMovBancoFiscalID = int.Parse(this.drpdlSubcatalogofiscalPago.SelectedValue);
                }
                else
                {
                    dtRowainsertar.catalogoMovBancoFiscalID = dtRowainsertar.catalogoMovBancoInternoID;
                    dtRowainsertar.subCatalogoMovBancoFiscalID = dtRowainsertar.subCatalogoMovBancoInternoID;
                }
                //}

                //if (cmbTipodeMov.SelectedIndex == 0)
                //{//ES CARGO

                    dtRowainsertar.cargo = this.txtMonto.Text.Length > 0 ? double.Parse(this.txtMonto.Text) : 0;
                    dtRowainsertar.abono = 0.00;

                //}
                //else
                //{//ES ABONO
                //    dtRowainsertar.abono = this.txtMonto.Text.Length > 0 ? double.Parse(this.txtMonto.Text) : 0;
                //    dtRowainsertar.cargo = 0.00;
                //}
                dtRowainsertar.storeTS = DateTime.Parse(Utils.getNowFormattedDate());
                dtRowainsertar.updateTS = DateTime.Parse(Utils.getNowFormattedDate());

                //             if (this.chkAssignToCredit.Checked)
                //             {
                //                 dtRowainsertar.creditoFinancieroID = int.Parse(this.ddlCreditoFinanciero.SelectedValue);
                //             }


                String serror = "", tipo = "";
                //bool bTodobien = true;
                //tipo = this.cmbTipodeMov.Text;
                dtRowainsertar.cuentaID = int.Parse(this.cmbCuentaPago.SelectedValue);
                //CHECAMOS SI SE VA A AGREGAR A UN CRÉDITO FINANCIERO
                //if (int.TryParse(this.lblcredFinID.Text, out credFinID) && credFinID > -1)
                //{
                //    dtRowainsertar.creditoFinancieroID = credFinID;
                //}
                //else
                //{
                    dtRowainsertar.creditoFinancieroID = -1;

                //}
                ListBox a = new ListBox();
                if (dbFunctions.insertaMovBanco(ref dtRowainsertar, ref serror, int.Parse(this.Session["USERID"].ToString()), int.Parse(this.cmbCuentaPago.SelectedValue), int.Parse(this.drpdlCiclo.SelectedValue), -1,"","PAGO A NOTA DE COMPRA"))
                {
                        SqlConnection connVenta = new SqlConnection(myConfig.ConnectionInfo);
                        try
                        {
                            connVenta.Open();
                            SqlCommand commVenta = new SqlCommand();
                            commVenta.Connection = connVenta;
                            commVenta.CommandText = "INSERT INTO Pagos_NotaCompra(notadecompraID, movbanID) VALUES (@notadecompraID, @movbanID);";
                            commVenta.Parameters.Add("@notadecompraID", SqlDbType.Int).Value = int.Parse(this.txtNotaIDToMod.Text);
                            commVenta.Parameters.Add("@movbanID", SqlDbType.Int).Value = dtRowainsertar.movBanID;
                            
                            if (commVenta.ExecuteNonQuery() != 1)
                            {
                                throw new Exception("This must almost never happen");
                            }
                        }
                        catch (System.Exception ex)
                        {
                            Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error adding new movbanco->notadecompra", ref ex);
                        }
                        finally
                        {
                            connVenta.Close();
                        }
                        this.cmbTipodeMovPago.SelectedIndex = 0;
                        this.divPagoMovCaja.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                        this.divMovBanco.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "MOVIMIENTO DE BANCO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                        this.divCheque.Attributes.Add("style", this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                }
                else
                {
                    //this.cmbTipodeMovPago.SelectedIndex = 0;
                    this.pnlNewPago.Visible = true;
                    this.imgBienPago.Visible = false;
                    this.imgMalPago.Visible = true;
                    this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO";
                }
            }
            this.ActualizaTotales();
        }

        protected bool numChequeValido(int ChequeNum, int Cuenta)
        {
            Boolean sresult;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            String query = "SELECT numchequeinicio, numchequefin FROM CuentasDeBanco WHERE cuentaID=@cuentaID";
            SqlCommand comm = new SqlCommand(query, conn);
            SqlDataReader rd;
            try
            {
                conn.Open();
                comm.Parameters.Add("@cuentaID", SqlDbType.Int).Value = Cuenta;
                rd = comm.ExecuteReader();
                if (rd.HasRows && rd.Read())
                {

                    if (ChequeNum <= int.Parse(rd["numchequefin"].ToString()) && ChequeNum >= int.Parse(rd["numchequeinicio"].ToString()))
                    {
                        sresult = true;
                    }
                    else { sresult = false; }

                }
                else
                {
                    sresult = false;
                }


            }
            catch (Exception ex)
            {
                sresult = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, this.UserID, "ERROR  SELECCIONANDOEL TIRAJE DE CHEQUES EXC ES: " + ex.Message, this.Request.Url.ToString());

            }
            finally
            {

                conn.Close();
            }

            return sresult;


        }

        protected void grvPagos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = "TOTAL";
                e.Row.Cells[4].Text = string.Format("{0:C2}", sumaMontos());
            }
            
        }

        protected double sumaMontos()
        {
            double total = 0.0;
            foreach (GridViewRow row in this.grvPagos.Rows)
            {
                total = total - Utils.GetSafeFloat(row.Cells[3].Text) + Utils.GetSafeFloat(row.Cells[8].Text);
            }
            return total;
        }

        protected void grvPagos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string pagoProvID;
            pagoProvID = e.Keys["pagosProveedoresID"].ToString();
            SqlPagos.DeleteCommand = "DELETE FROM Pagos_Proveedores WHERE (Pagos_Proveedores.pagosProveedoresID = @pagosProveedoresID); ";          
            SqlPagos.DeleteParameters.Add("@pagosProveedoresID", pagoProvID);

            
            
        }

        protected void grvPagos_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            this.ActualizaTotales();
        }

        protected void cmbTipodeMovPago_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void grvPagos_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            if(e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "error actualizando pagos en notas de compra", e.Exception);
                e.ExceptionHandled = true;
            }
            if (e.AffectedRows == 1)
                e.KeepInEditMode = false;
            ActualizaTotales();
        }

        protected void grvPagos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        protected void grvPagos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grvPagos.EditIndex = e.NewEditIndex;
        }

        protected void grvPagos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvPagos.EditIndex = -1;
        }

        protected void grdvProNotas_DataBound(object sender, EventArgs e)
        {
            this.dvTotales.DataBind();
        }

        protected void grdvProNotas_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }

        protected void grdvProNotas_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }

        protected void grdvProNotas_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            e.NewValues.Add("notadecompraID", this.txtNotaIDToMod.Text);
        }

        protected void sdsDetallePRocs_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {

        }
    }
}
