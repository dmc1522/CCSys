using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

namespace Garibay
{
    public partial class frmMovBancoAddQuick : Garibay.BasePage
    {
        int imovID = -1, credFinID = -1, FacturaVentaID = -1, notaventaID = -1, notacompraID=-1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.lblDescripcion.Visible = false;
                String sOnchange;
                this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
                JSUtils.AddDisableWhenClick(ref this.btnAgregar0, this);
                JSUtils.AddDisableWhenClick(ref this.btnModificar, this);
                sOnchange = "EnableDisableAbonosCargos('";
                sOnchange += this.ddlConcepto.ClientID + "','";
                sOnchange += this.cmbTipodeMov.ClientID + "');";
                this.ddlConcepto.Attributes.Add("onChange", sOnchange);
                JSUtils.AddMsgToCtrlOnClick(ref this.btnCancelar, "¡¡NO OLVIDE ACTUALIZAR LA LISTA DE MOVIMIENTOS DE BANCO !!", false);
                JSUtils.closeCurrentWindow(ref this.btnCancelar);
                if (Request.QueryString["data"] != null)
                {
                    if (this.loadqueryStrings(Request.QueryString["data"].ToString()))
                    {
                        this.btnAgregar0.Visible = true;
                        this.btnModificar.Visible = false;
                        if (this.myQueryStrings["movID"] != null && int.TryParse(this.myQueryStrings["movID"].ToString(), out imovID) && imovID > -1) //CHECAMOS SI ES PARA MODIFICAR
                        {
                            this.lblmovID.Text = imovID.ToString();
                            //AQUI VERIFICAMOS SI EL MOVIMIENTO VIENE DE UN CRÉDITO 
                            if (this.cargadatostomodify())
                            {
                                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDEBANCO, Logger.typeUserActions.SELECT, this.UserID, "ABRIÓ EL MOVIMIENTO DE BANCO NÚMERO." + this.lblmovID.Text);
                                this.btnModificar.Visible = true;
                                this.btnAgregar0.Visible = false;
                                this.drpdlCuenta.Enabled = false;
                          
                            }
                            else
                            {
                                Response.Write("<script type=\"text/javascript\">window.close();</script>");
                            }
                        }
//                         else
//                         {
//                            //AQUI PONEMOS EL CODIGO SI RECIBIMOS EL ANTICIPO ID O ALGO
//                             if (this.myQueryStrings["creditoFinID"] != null && int.TryParse(this.myQueryStrings["creditoFinID"].ToString(), out credFinID) && credFinID > -1)
//                             {
//                                 this.lblcredFinID.Text = credFinID.ToString();
//                                 this.lblDescripcion.Text = "ESTE MOVIMIENTO SE AGREGARÁ AL CREDITO FINANCIERO: " + credFinID.ToString();
//                                 this.lblDescripcion.Visible = true;
//                             }
//                         }

                        //here is where if the mov de banco is currently related to anything
                        //load the data for modify it
                        if (this.myQueryStrings["creditoFinID"] != null && int.TryParse(this.myQueryStrings["creditoFinID"].ToString(), out credFinID) && credFinID > -1)
                        {
                            this.lblcredFinID.Text = credFinID.ToString();
                            this.lblDescripcion.Text = "ESTE MOVIMIENTO ESTÁ AGREGADO AL CREDITO FINANCIERO: " + credFinID.ToString();
                            this.lblDescripcion.Visible = true;
                            
                            this.chkNotadeCompra.Visible = false;
                            this.pnlNotaCompra.Visible = false;
                            this.chkCreditoFinanciero.Enabled = false;
                            this.pnlCreditoFinanciero.Visible = true;
                            this.chkCreditoFinanciero.Checked = true;
                       
                            this.pnlFactura.Visible = false;
                            this.chkFactura.Visible = false;
                            this.chkNotadeVenta.Visible = false;
                            this.pnlNotadeVenta.Visible = false;
                            this.LoadCreditoFinancieroData(credFinID);
                        }
                        else
                            if (this.myQueryStrings["FacturaCVID"] != null && int.TryParse(this.myQueryStrings["FacturaCVID"].ToString(), out this.FacturaVentaID) && FacturaVentaID > -1)
                            {
                                this.lblcredFinID.Text = this.FacturaVentaID.ToString();
                                this.lblDescripcion.Text = "ESTE MOVIMIENTO ESTÁ LIGADO A LA FACTURA DE VENTA: " + this.FacturaVentaID.ToString();
                                this.lblDescripcion.Visible = true;
                                this.chkNotadeCompra.Visible = false;
                                this.pnlNotaCompra.Visible = false;
                                this.chkCreditoFinanciero.Visible = false;
                                this.pnlCreditoFinanciero.Visible = false;
                                this.chkFactura.Enabled = false;
                                this.pnlFactura.Visible = true;
                                this.chkFactura.Checked = true;
                                this.chkNotadeVenta.Visible = false;
                                this.pnlNotadeVenta.Visible = false;
                             
                                this.pnlFactura_CollapsiblePanelExtender.Collapsed = false;
                                SqlConnection connFac = new SqlConnection(myConfig.ConnectionInfo);
                                try
                                {
                                    SqlCommand comm = new SqlCommand();
                                    connFac.Open();
                                    comm.Connection = connFac;
                                    comm.CommandText = "SELECT FacturaCV, cicloID, clienteVentaID FROM FacturasClientesVenta WHERE (FacturaCV = @FacturaCV)";
                                    comm.Parameters.Add("@FacturaCV", SqlDbType.Int).Value = FacturaVentaID;
                                    SqlDataReader reader = comm.ExecuteReader();
                                    reader.Read();
                                    this.ddlFacturaCiclo.DataBind();
                                    this.ddlFacturaCiclo.SelectedValue = reader["cicloID"].ToString();
                                    this.ddlFacturaCiclo.Enabled = false;
                                    this.ddlFacturaClientesVenta.DataBind();
                                    this.ddlFacturaClientesVenta.SelectedValue = reader["clienteVentaID"].ToString();
                                    this.ddlFacturaClientesVenta.Enabled = false;
                                    this.ddlFacturas.DataBind();
                                    this.ddlFacturas.SelectedValue = FacturaVentaID.ToString();
                                    this.ddlFacturas.Enabled = false;
                                }
                                catch (System.Exception ex)
                                {
                                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "getting ciclo y cliente id's", ref ex);
                                }
                                finally
                                {
                                    connFac.Close();
                                }
                            }
                            else
                                if(this.myQueryStrings["notaventaID"] != null && int.TryParse(this.myQueryStrings["notaventaID"].ToString(), out notaventaID) && notaventaID > -1)
                                {

                                    this.lblNotaVentaID.Text = this.notaventaID.ToString();
                                    this.lblDescripcion.Text = "ESTE MOVIMIENTO ESTÁ LIGADO A LA NOTA DE VENTA: " + this.notaventaID.ToString();
                                    this.lblDescripcion.Visible = true;
                                    this.chkNotadeCompra.Visible = false;
                                    this.pnlNotaCompra.Visible = false;
                                    this.chkCreditoFinanciero.Visible = false;
                                    this.pnlCreditoFinanciero.Visible = false;
                                    this.chkFactura.Visible = false;
                                    this.pnlFactura.Visible = false;
                                    this.chkNotadeVenta.Enabled = false;
                                    this.pnlNotadeVenta.Visible = true;
                                    this.chkNotadeVenta.Checked = true;
                                    this.CollapsiblePanelExtender3.Collapsed = false;
                             
                                    SqlConnection connFac = new SqlConnection(myConfig.ConnectionInfo);
                                    try
                                    {
                                        SqlCommand comm = new SqlCommand();
                                        connFac.Open();
                                        comm.Connection = connFac;
                                        comm.CommandText = "SELECT  cicloID, productorID,   Folio + ' - ' + CAST(notadeventaID AS VARCHAR(50)) + ' ' + CONVERT(varchar(50), Fecha, 103) AS Nota, notadeventaID FROM Notasdeventa WHERE (notadeventaID = @notadeventaID)";
                                        comm.Parameters.Add("@notadeventaID", SqlDbType.Int).Value = notaventaID;
                                        SqlDataReader reader = comm.ExecuteReader();
                                        reader.Read();
                                        this.ddlFacturaCiclo.DataBind();
                                        this.drpdlCicloNotaVenta.SelectedValue = reader["cicloID"].ToString();
                                        this.drpdlCicloNotaVenta.Enabled = false;
                                        this.drpdlCicloNotaVenta.DataBind();
                                        this.drpdlProductoresNotaVenta.SelectedValue = reader["productorID"].ToString();
                                        this.drpdlProductoresNotaVenta.Enabled = false;
                                        this.drpdlProductoresNotaVenta.DataBind();
                                        this.dprdlNota.SelectedValue = notaventaID.ToString();
                                        this.dprdlNota.Enabled = false;
                                    }
                                    catch (System.Exception ex)
                                    {
                                        Logger.Instance.LogException(Logger.typeUserActions.SELECT, "getting ciclo y productor id's", ref ex);
                                    }
                                    finally
                                    {
                                        connFac.Close();
                                    }

                                }
                                else
                                {
                                    if (this.myQueryStrings["notacompraID"] != null && int.TryParse(this.myQueryStrings["notacompraID"].ToString(), out notacompraID) && notacompraID > -1)
                                    {
                                        this.lblNotaCompraID.Text = this.notacompraID.ToString();
                                        this.cargaDatosNCompra();
                                        
                                    }

                                }

                    }
                    else
                    {
                        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL,Logger.typeUserActions.SELECT, this.UserID, "ERROR AL CARGAR DATOS EN ADDMOVQUICK, LOS PARÄMETROS PASADOS COMO QUERYSTRINGS NO CORRESPONDEN A ALGUN MOVIMIENTO ",this.Request.Url.ToString());
                        Response.Write("<script type=\"text/javascript\">window.close();</script>");
                    }

                }
                else{
                    this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
                    this.lblDescripcion.Visible = false ;
                }
                //this.divConceptosFiscales.Attributes.Add("style", this.chkMostrarFiscales.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.pnlNewMov.Visible = false;
            }
        }
        private void cargaDatosNCompra()
        {


            
            this.lblDescripcion.Text = "ESTE MOVIMIENTO ESTÁ LIGADO A LA NOTA DE COMPRA: " + this.lblNotaCompraID.Text;
            this.lblDescripcion.Visible = true;
            this.chkCreditoFinanciero.Visible = false;
            this.pnlCreditoFinanciero.Visible = false;
            this.chkFactura.Visible = false;
            this.pnlFactura.Visible = false;
            this.chkNotadeVenta.Visible = false;
            this.pnlNotadeVenta.Visible = false;
            this.chkNotadeCompra.Enabled = false;
            this.chkNotadeCompra.Checked = true;
            this.CollapsiblePanelExtender1.Collapsed = false;
         
            SqlConnection connFac = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand comm = new SqlCommand();
                connFac.Open();
                comm.Connection = connFac;
                comm.CommandText = "SELECT cicloID, proveedorID, Folio + ' - ' + CAST(notadecompraID AS VARCHAR(50)) + ' ' + CONVERT (varchar(50), Fecha, 103) AS NotaDeCompra, notadecompraID FROM NotasdeCompra WHERE (notadecompraID = @notadecompraID)";
                comm.Parameters.Add("@notadecompraID", SqlDbType.Int).Value = this.lblNotaCompraID.Text;
                SqlDataReader reader = comm.ExecuteReader();
                reader.Read();
                this.ddlFacturaCiclo.DataBind();
                this.drpdlCicloNotaCompra.SelectedValue = reader["cicloID"].ToString();
                this.drpdlCicloNotaCompra.Enabled = false;
                this.drpdlCicloNotaCompra.DataBind();
                this.drpdlProveedoresNotaCompra.SelectedValue = reader["proveedorID"].ToString();
                this.drpdlProveedoresNotaCompra.Enabled = false;
                this.drpdlProveedoresNotaCompra.DataBind();
                this.drpdlNotaCompra.SelectedValue = this.lblNotaCompraID.Text;
                this.drpdlNotaCompra.Enabled = false;
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "getting ciclo y proveedor id's", ref ex);
            }
            finally
            {
                connFac.Close();
            }


        }
        private bool LoadCreditoFinancieroData(int creditoID)
        {
            bool res = false;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "SELECT CreditosFinancieros.bancoID FROM CreditosFinancieros where CreditosFinancieros.creditoFinancieroID = @creditoFinancieroID";
                comm.Parameters.Add("@creditoFinancieroID", SqlDbType.Int).Value = creditoID;
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    this.ddlCreditoFinBancos.DataBind();
                    this.ddlCreditoFinBancos.SelectedValue = reader[0].ToString();

                    this.ddlCreditosFinancieros.DataBind();
                    this.ddlCreditosFinancieros.SelectedValue = creditoID.ToString();

                    this.chkListCredBinCertificados.DataBind();
                    comm.CommandText = "select CredFinCertID from Certificado_MovBanco where movBanID = @movBanID";
                    comm.Parameters.Clear();
                    comm.Parameters.Add("@movBanID", SqlDbType.Int).Value = imovID;
                    reader.Close();
                    reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        
                        foreach(ListItem item in  this.chkListCredBinCertificados.Items)
                        {
                            if (item.Value == reader[0].ToString())
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "loading credito financiero data" + creditoID.ToString(), ref ex);
            }
            finally
            {
                conn.Close();
            }
            return res;
        }

        protected bool cargadatostomodify()
        {
            this.ddlConcepto.DataBind();
          
            this.drpdlGrupoCatalogos.DataBind();
            this.drpdlGrupoCuentaFiscal.DataBind();
            this.drpdlCatalogocuentafiscal.DataBind();
            this.drpdlCatalogoInterno.DataBind();
            this.drpdlSubcatalogofiscal.DataBind();
            this.drpdlSubcatologointerna.DataBind();
            this.drpdlCuenta.DataBind();

            string qrySel = "SELECT     MovimientosCuentasBanco.cuentaID, MovimientosCuentasBanco.cargo, MovimientosCuentasBanco.abono, ConceptosMovCuentas.Concepto, ";
            qrySel += " MovimientosCuentasBanco.fecha, MovimientosCuentasBanco.ConceptoMovCuentaID, MovimientosCuentasBanco.nombre, ";
            qrySel += " MovimientosCuentasBanco.facturaOlarguillo, MovimientosCuentasBanco.numCabezas, MovimientosCuentasBanco.numCheque, ";
            qrySel += " MovimientosCuentasBanco.chequeNombre, MovimientosCuentasBanco.catalogoMovBancoFiscalID, ";
            qrySel += " MovimientosCuentasBanco.subCatalogoMovBancoFiscalID, MovimientosCuentasBanco.catalogoMovBancoInternoID, ";
            qrySel += " MovimientosCuentasBanco.subCatalogoMovBancoInternoID, catalogoMovimientosBancos_1.catalogoMovBancoID AS grupofiscal, ";
            qrySel += " catalogoMovimientosBancos.catalogoMovBancoID AS grupointerno ";
            qrySel += " FROM         MovimientosCuentasBanco INNER JOIN ";
            qrySel += " ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID INNER JOIN ";
            qrySel += " catalogoMovimientosBancos ON MovimientosCuentasBanco.catalogoMovBancoFiscalID = catalogoMovimientosBancos.catalogoMovBancoID INNER JOIN ";
            qrySel += " catalogoMovimientosBancos AS catalogoMovimientosBancos_1 ON ";
            qrySel += " MovimientosCuentasBanco.catalogoMovBancoInternoID = catalogoMovimientosBancos_1.catalogoMovBancoID where MovimientosCuentasBanco.movbanID = @movbanID ";

            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdSel = new SqlCommand(qrySel, conGaribay);
            try
            {
                cmdSel.Parameters.Add("@movBanID", SqlDbType.Int).Value = int.Parse(this.myQueryStrings["movID"].ToString());
                conGaribay.Open();
                SqlDataReader datostomodify;
                datostomodify = cmdSel.ExecuteReader();
                this.drpdlCuenta.DataBind();
                this.cmbTipodeMov.DataBind();
                this.ddlConcepto.DataBind();
                if (!datostomodify.HasRows)
                { //EL ID NO ES VALIDO
                    this.lblNewMovResult.Text = myConfig.StrFromMessages("FALLOCARGARMODIFICAR");
                    this.imgMal.Visible = true;
                    this.pnlNewMov.Visible = true;
                    this.imgBien.Visible = false;
                    //this.txtIDdetails.Text = "-1";
                    //this.panelagregar.Visible = false;
                    this.btnModificar.Visible = true;
                    this.btnAgregar0.Visible = true;


                    return false;

                }

                if (datostomodify.Read())
                {
                    this.drpdlCuenta.SelectedValue = datostomodify["cuentaID"].ToString();
                    double cargo = 0, abono = 0;
                    if (double.TryParse(datostomodify["cargo"].ToString(),out cargo) && cargo > 0) //ES CARGO
                    {
                        this.cmbTipodeMov.SelectedValue = "CARGO";
                        this.txtMonto.Text = cargo.ToString();
                        lblMontoAnt.Text = this.txtMonto.Text;
                    }
                    else//ES ABONO
                    {
                        double.TryParse(datostomodify["cargo"].ToString(), out abono);
                        this.cmbTipodeMov.SelectedValue = "ABONO";
                        this.txtMonto.Text = abono.ToString();
                        lblMontoAnt.Text = this.txtMonto.Text;
                    }
                    this.ddlConcepto.Text = datostomodify["concepto"].ToString();
                    this.txtNombre.Text = datostomodify["nombre"].ToString();
                    this.txtFecha.Text = Utils.converttoshortFormatfromdbFormat(datostomodify["fecha"].ToString());
                    this.txtChequeNum.Text = datostomodify["numCheque"].ToString();
                    this.txtChequeNombre.Text = datostomodify["chequeNombre"].ToString();

                    //this.divCheque.Attributes.Add("style", this.ddlConcepto.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                    this.txtFacturaLarguillo.Text = datostomodify["facturaOlarguillo"].ToString();
                    this.txtNumCabezas.Text = datostomodify["numCabezas"].ToString();
                    this.drpdlGrupoCuentaFiscal.SelectedValue = datostomodify["grupofiscal"].ToString();
                    this.drpdlGrupoCatalogos.SelectedValue = datostomodify["grupointerno"].ToString();
                    this.drpdlCatalogocuentafiscal.DataBind();
                    this.drpdlSubcatalogofiscal.DataBind();
                    this.drpdlCatalogocuentafiscal.SelectedValue = datostomodify["catalogoMovBancoFiscalID"].ToString();
                    this.drpdlCatalogoInterno.DataBind();
                    this.drpdlSubcatologointerna.DataBind();
                    this.drpdlCatalogoInterno.SelectedValue = datostomodify["catalogoMovBancoInternoID"].ToString();
                    if (datostomodify["subCatalogoMovBancoFiscalID"].ToString() != "-1") { this.drpdlSubcatalogofiscal.SelectedValue = datostomodify["subCatalogoMovBancoFiscalID"].ToString(); }
                    if (datostomodify["subCatalogoMovBancoInternoID"].ToString() != "-1") { this.drpdlSubcatologointerna.SelectedValue = datostomodify["subCatalogoMovBancoInternoID"].ToString(); }
                   // datostomodify[""].ToString();
               
                    
                   // this.drpdlGrupoCuentaFiscal.DataBind();
                 
                    


                }
            }
          
            catch (Exception exception)
            {
                this.lblNewMovResult.Text = "ERROR AL SELECCIONAR LOS DATOS. EX: " + exception.Message;
                this.imgMal.Visible = true;
                this.pnlNewMov.Visible = true;
                this.imgBien.Visible = false;
                //this.txtIDdetails.Text = "-1";
                //this.panelagregar.Visible = false;
                this.btnModificar.Visible = true;
                this.btnAgregar0.Visible = true;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, this.UserID, "ERROR AL CARGAR LOS DATOS DEL MOVIMIENTO EN FRMADDMOVBANCOQUICK. EX: " + exception.Message, this.Request.Url.ToString());
                return false;
            }
            finally
            {
                conGaribay.Close();
            }
            return true;
           
        }
        protected void drpdlGrupoCatalogos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlCatalogoInterno.DataBind();
            this.drpdlSubcatologointerna.DataBind();
        }

        protected void drpdlGrupoCuentaFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlCatalogocuentafiscal.DataBind();
            this.drpdlSubcatalogofiscal.DataBind();
        }

        protected void btnAgregar0_Click(object sender, EventArgs e)
        {
            int cheque = 0;
            bool hayerrorenmonto = false;
            double monto = 0;
            double.TryParse(this.txtMonto.Text, out monto);

            if (monto == 0)
            {
                if (this.drpdlCatalogoInterno.SelectedItem.Text != "10J -  CHEQUES CANCELADOS")
                {
                    hayerrorenmonto = true;
                }
            }
            if (hayerrorenmonto)
            {
                this.pnlNewMov.Visible = true;
                this.imgBien.Visible = true;
                this.imgMal.Visible = false;
                this.lblNewMovResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, ERROR EN MONTO, ESCRIBA CANTIDAD VÁLIDA";
                return;
            }
            if (int.TryParse(this.txtChequeNum.Text, out cheque))
            {
                if (this.ddlConcepto.SelectedItem.Text.IndexOf("CHEQUE") > 0 && dbFunctions.ChequeAlreadyExists(cheque, this.drpdlCuenta.SelectedValue))
                {
                    this.pnlNewMov.Visible = true;
                    this.imgBien.Visible = true;
                    this.imgMal.Visible = false;
                    this.lblNewMovResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, ESTE CHEQUE YA HA SIDO AGREGADO";
                    return;

                   

                }
            }
            //this.cmbTipodeMov.DataBind();
            dsMovBanco.dtMovBancoDataTable tablaaux = new dsMovBanco.dtMovBancoDataTable();
            dsMovBanco.dtMovBancoRow dtRowainsertar = tablaaux.NewdtMovBancoRow();
            dtRowainsertar.conceptoID = int.Parse(this.ddlConcepto.SelectedValue);
            if (this.txtNombre.Text.Length > 0)
            {
                dtRowainsertar.nombre = this.txtNombre.Text;
            }
            else
            {
                dtRowainsertar.nombre = this.txtChequeNombre.Text;
            }
            
            dtRowainsertar.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFecha.Text));
            //dats de cheque
            dtRowainsertar.numCheque = this.txtChequeNum.Text.Length > 0 ? int.Parse(this.txtChequeNum.Text) : 0;
            dtRowainsertar.chequeNombre = this.txtChequeNombre.Text;
            dtRowainsertar.facturaOlarguillo = this.txtFacturaLarguillo.Text;
            dtRowainsertar.numCabezas = this.txtNumCabezas.Text.Length > 0 ? double.Parse(this.txtNumCabezas.Text) : 0;

            dtRowainsertar.catalogoMovBancoInternoID = int.Parse(this.drpdlCatalogoInterno.SelectedValue);
            if (this.drpdlSubcatologointerna.SelectedIndex > -1)
                dtRowainsertar.subCatalogoMovBancoInternoID = int.Parse(this.drpdlSubcatologointerna.SelectedValue);
            if (!this.chkMostrarFiscales.Checked)
            {
                dtRowainsertar.catalogoMovBancoFiscalID = int.Parse(this.drpdlCatalogoInterno.SelectedValue);
                if (this.drpdlSubcatologointerna.SelectedIndex > -1)
                    dtRowainsertar.subCatalogoMovBancoFiscalID = int.Parse(this.drpdlSubcatologointerna.SelectedValue);
            }
            else
            {
                dtRowainsertar.catalogoMovBancoFiscalID = int.Parse(this.drpdlCatalogocuentafiscal.SelectedValue);
                if (this.drpdlSubcatalogofiscal.SelectedIndex > -1)
                    dtRowainsertar.subCatalogoMovBancoFiscalID = int.Parse(this.drpdlSubcatalogofiscal.SelectedValue);
            }

            if (cmbTipodeMov.SelectedIndex == 0)
            {//ES CARGO

                dtRowainsertar.cargo = this.txtMonto.Text.Length > 0 ? double.Parse(this.txtMonto.Text) : 0;
                dtRowainsertar.abono = 0.00;

            }
            else
            {//ES ABONO
                dtRowainsertar.abono = this.txtMonto.Text.Length > 0 ? double.Parse(this.txtMonto.Text) : 0;
                dtRowainsertar.cargo = 0.00;
            }
            dtRowainsertar.storeTS = DateTime.Parse(Utils.getNowFormattedDate());
            dtRowainsertar.updateTS = DateTime.Parse(Utils.getNowFormattedDate());

//             if (this.chkAssignToCredit.Checked)
//             {
//                 dtRowainsertar.creditoFinancieroID = int.Parse(this.ddlCreditoFinanciero.SelectedValue);
//             }


            String serror = "", tipo = "";
            //bool bTodobien = true;
            tipo = this.cmbTipodeMov.Text;
            dtRowainsertar.cuentaID = int.Parse(this.drpdlCuenta.SelectedValue);
            //CHECAMOS SI SE VA A AGREGAR A UN CRÉDITO FINANCIERO
            if(int.TryParse(this.lblcredFinID.Text,out credFinID) && credFinID>-1)
            {
                dtRowainsertar.creditoFinancieroID = credFinID;
            }
            else
            {
                dtRowainsertar.creditoFinancieroID = -1;
            
            }
           ListBox a = new ListBox();
            if (dbFunctions.insertMovementdeBanco(ref dtRowainsertar, ref serror, int.Parse(this.Session["USERID"].ToString()), int.Parse(this.drpdlCuenta.SelectedValue),false,-1,ref a,-1,0.00f,0.00f, Utils.Now, -1,""))
            {
                
                //if the mov banco were added successfully then check if add into factura
                if (this.chkFactura.Checked)
                {
                    SqlConnection connFactura = new SqlConnection(myConfig.ConnectionInfo);
                    try
                    {
                        connFactura.Open();
                        SqlCommand commFactura = new SqlCommand();
                        commFactura.Connection = connFactura;
                        commFactura.CommandText = "INSERT INTO PagosFacturasClientesVenta (FacturaCVID, fecha, movbanID, userID) VALUES (@FacturaCVID,@fecha,@movbanID,@userID) ";
                        //(@FacturaCVID,@fecha,@movbanID,@movCajaID,@userID)
                        commFactura.Parameters.Add("@FacturaCVID", SqlDbType.Int).Value = int.Parse(this.ddlFacturas.SelectedValue);
                        commFactura.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Parse(this.txtFecha.Text);
                        commFactura.Parameters.Add("@movbanID", SqlDbType.Int).Value = dtRowainsertar.movBanID;
                        commFactura.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                        if (commFactura.ExecuteNonQuery() != 1)
                        {
                            throw new Exception("This must almost never happen");
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error adding new movbanco->factura", ref ex);
                    }
                    finally
                    {
                        connFactura.Close();
                    }
                }
                if(this.chkNotadeVenta.Checked)
                {
                    SqlConnection connVenta = new SqlConnection(myConfig.ConnectionInfo);
                    try
                    {
                        connVenta.Open();
                        SqlCommand commVenta = new SqlCommand();
                        commVenta.Connection = connVenta;
                        commVenta.CommandText = "INSERT INTO Pagos_NotaVenta(fecha, notadeventaID, movbanID) VALUES (@fecha,@notadeventaID,@movbanID) ";
                        //(@FacturaCVID,@fecha,@movbanID,@movCajaID,@userID)
                        commVenta.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtRowainsertar.fecha;
                        commVenta.Parameters.Add("@notadeventaID", SqlDbType.Int).Value = int.Parse(this.dprdlNota.SelectedValue);
                        commVenta.Parameters.Add("@movbanID", SqlDbType.Int).Value = dtRowainsertar.movBanID;
                        
                        if (commVenta.ExecuteNonQuery() != 1)
                        {
                            throw new Exception("This must almost never happen");
                        }
                        Logger.Instance.LogUserSessionRecord(Logger.typeModulo.NOTAVENTA, Logger.typeUserActions.UPDATE,this.UserID,"SE AGREGÓ EL MOV BANCO " + dtRowainsertar.movBanID.ToString() + ". A LA NOTA DE VENTA: " + this.lblNotaVentaID.ToString());
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error adding new movbanco->notadeventa", ref ex);
                    }
                    finally
                    {
                        connVenta.Close();
                    }

                }
                if (this.chkNotadeCompra.Checked)
                {
                    SqlConnection connVenta = new SqlConnection(myConfig.ConnectionInfo);
                    try
                    {
                        connVenta.Open();
                        SqlCommand commVenta = new SqlCommand();
                        commVenta.Connection = connVenta;
                        commVenta.CommandText = "INSERT INTO Pagos_NotaCompra(fecha, notadecompraID, movbanID) VALUES (@fecha,@notadecompraID,@movbanID) ";
                        //(@FacturaCVID,@fecha,@movbanID,@movCajaID,@userID)
                        commVenta.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtRowainsertar.fecha;
                        commVenta.Parameters.Add("@notadecompraID", SqlDbType.Int).Value = int.Parse(this.drpdlNotaCompra.SelectedValue);
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

                }

                limpiacampos();
                this.pnlNewMov.Visible = true;
                this.imgBien.Visible = true;
                this.imgMal.Visible = false;

                if (this.chkCreditoFinanciero.Checked)
                {
                    SqlConnection connCertificados = new SqlConnection(myConfig.ConnectionInfo);
                    try
                    {
                        connCertificados.Open();
                        SqlCommand commCertificados = new SqlCommand();
                        commCertificados.Connection = connCertificados;
                        commCertificados.CommandText = "INSERT INTO Certificado_MovBanco (CredFinCertID, movBanID) VALUES ";
                        commCertificados.Parameters.Add("@movBanID", SqlDbType.Int).Value = dtRowainsertar.movBanID;
                        int i = 0;
                        foreach(ListItem item  in this.chkListCredBinCertificados.Items)
                        {
                            if (item.Selected)
                            {
                                if (i > 0)
                                {
                                    commCertificados.CommandText += ",";
                                }
                                commCertificados.CommandText += "(@CredFinCertID_" + i.ToString() + ",@movBanID)";
                                commCertificados.Parameters.Add("@CredFinCertID_" + i.ToString(), SqlDbType.Int).Value = item.Value;
                                i++;
                            }
                        }
                        if (i > 0)
                        {
                            int iInsertadas = ((int)commCertificados.ExecuteNonQuery());
                            if (iInsertadas <= 0)
                            {
                                throw new Exception("This must almost never happen");
                            }
                        }
                        
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error adding new movbanco->factura", ref ex);
                    }
                    finally
                    {
                        connCertificados.Close();
                    }
                }                
                if(int.TryParse(this.lblcredFinID.Text,out credFinID) && credFinID>-1)
                {
                 this.lblNewMovResult.Text = "SE HA AGREGADO EXITOSAMENTE EL MOVIMIENTO DE BANCO AL CRÉDITO FINANCIERO";
                }
                else
                {
                 this.lblNewMovResult.Text = "SE HA AGREGADO EXITOSAMENTE EL MOVIMIENTO DE BANCO.";
                }
                this.btnAgregar0.Visible = false;
            }
            else
            {
                this.pnlNewMov.Visible = true;
                this.imgBien.Visible = false;
                this.imgMal.Visible = true;
                this.lblNewMovResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO";

            }
        }
        private void limpiacampos(){
            this.txtChequeNombre.Text="";
            this.txtChequeNum.Text = "";
            this.txtFacturaLarguillo.Text="";
            this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
            this.txtNombre.Text ="";
            this.txtNumCabezas.Text = "0.00";
            this.txtMonto.Text = "0.00";
           
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Write("<script type=\"text/javascript\">window.close();</script>");

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

            int cheque = 0;
            bool hayerrorenmonto = false;
            double monto = 0;
            double.TryParse(this.txtMonto.Text, out monto);

            if (monto == 0)
            {
                if (this.drpdlCatalogoInterno.SelectedItem.Text != "10J -  CHEQUES CANCELADOS")
                {
                    hayerrorenmonto = true;
                }
            }
            if (hayerrorenmonto)
            {
                this.pnlNewMov.Visible = true;
                this.imgBien.Visible = true;
                this.imgMal.Visible = false;
                this.lblNewMovResult.Text = "NO SE HA PODIDO MODIFICAR EL MOVIMIENTO DE BANCO, ERROR EN MONTO, ESCRIBA CANTIDAD VÁLIDA";
                return;
            }
//             if ((this.txtChequeNum.Text != this.lblMontoAnt.Text) && int.TryParse(this.txtChequeNum.Text, out cheque))
//             {
//                 if (dbFunctions.ChequeAlreadyExists(cheque))
//                 {
//                     this.pnlNewMov.Visible = true;
//                     this.imgBien.Visible = true;
//                     this.imgMal.Visible = false;
//                     this.lblNewMovResult.Text = "NO SE HA PODIDO MODIFICAR EL MOVIMIENTO DE BANCO, ESTE CHEQUE YA HA SIDO AGREGADO";
//                     return;
//                 }
//             }
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

            dtRowainsertar.catalogoMovBancoInternoID = int.Parse(this.drpdlCatalogoInterno.SelectedValue);
            if (this.drpdlSubcatologointerna.SelectedIndex > -1)
                dtRowainsertar.subCatalogoMovBancoInternoID = int.Parse(this.drpdlSubcatologointerna.SelectedValue);
            if (!this.chkMostrarFiscales.Checked)
            {
                dtRowainsertar.catalogoMovBancoFiscalID = int.Parse(this.drpdlCatalogoInterno.SelectedValue);
                if (this.drpdlSubcatologointerna.SelectedIndex > -1)
                    dtRowainsertar.subCatalogoMovBancoFiscalID = int.Parse(this.drpdlSubcatologointerna.SelectedValue);
            }
            else
            {
                dtRowainsertar.catalogoMovBancoFiscalID = int.Parse(this.drpdlCatalogocuentafiscal.SelectedValue);
                if (this.drpdlSubcatalogofiscal.SelectedIndex > -1)
                    dtRowainsertar.subCatalogoMovBancoFiscalID = int.Parse(this.drpdlSubcatalogofiscal.SelectedValue);
            }

            if (cmbTipodeMov.SelectedIndex == 0)
            {//ES CARGO

                dtRowainsertar.cargo = this.txtMonto.Text.Length > 0 ? double.Parse(this.txtMonto.Text) : 0;
                dtRowainsertar.abono = 0.00;

            }
            else
            {//ES ABONO
                dtRowainsertar.abono = this.txtMonto.Text.Length > 0 ? double.Parse(this.txtMonto.Text) : 0;
                dtRowainsertar.cargo = 0.00;
            }
            dtRowainsertar.storeTS = DateTime.Parse(Utils.getNowFormattedDate());
            dtRowainsertar.updateTS = DateTime.Parse(Utils.getNowFormattedDate());

            //             if (this.chkAssignToCredit.Checked)
            //             {
            //                 dtRowainsertar.creditoFinancieroID = int.Parse(this.ddlCreditoFinanciero.SelectedValue);
            //             }


            String serror = "", tipo = "";
            //bool bTodobien = true;
            tipo = this.cmbTipodeMov.Text;
            dtRowainsertar.cuentaID = int.Parse(this.drpdlCuenta.SelectedValue);
            //CHECAMOS SI SE VA A AGREGAR A UN CRÉDITO FINANCIERO
            if (int.TryParse(this.lblcredFinID.Text, out credFinID) && credFinID > -1)
            {
                dtRowainsertar.creditoFinancieroID = credFinID;
            }
            else
            {
                dtRowainsertar.creditoFinancieroID = -1;

            }
            
            ListBox a = new ListBox();
            if (dbFunctions.updateMovementdeBanco(ref dtRowainsertar, int.Parse(this.lblmovID.Text), ref serror, this.UserID, int.Parse(this.drpdlCuenta.SelectedValue)))
            {
                limpiacampos();
                this.pnlNewMov.Visible = true;
                this.imgBien.Visible = true;
                this.imgMal.Visible = false;

                if (int.TryParse(this.lblcredFinID.Text, out credFinID) && credFinID > -1)
                {
                    this.lblNewMovResult.Text = "SE HA MODIFICADO EXITOSAMENTE EL MOVIMIENTO DE BANCO AL CRÉDITO FINANCIERO";
                }
                else
                {
                    this.lblNewMovResult.Text = "SE HA MODIFICADO EXITOSAMENTE EL MOVIMIENTO DE BANCO.";
                }
                //this.panelAgregar.Attributes.Add("style", this.chkMostrarAddMov.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            }
            else
            {
                this.pnlNewMov.Visible = true;
                this.imgBien.Visible = false;
                this.imgMal.Visible = true;
                this.lblNewMovResult.Text = "NO SE HA PODIDO MODIFICAR EL MOVIMIENTO DE BANCO";

            }

        }

        protected void ddlFacturaCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlFacturaClientesVenta.DataBind();
            this.ddlFacturas.DataBind();
        }

        protected void ddlFacturaClientesVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlFacturas.DataBind();
        }

        protected void ddlCreditoFinBancos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.chkListCredBinCertificados.DataBind();
        }

        protected void ddlCreditosFinancieros_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.chkListCredBinCertificados.DataBind();
        }

        protected void drpdlCicloNotaVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dprdlNota.DataBind();
        }

        protected void drpdlProductoresNotaVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dprdlNota.DataBind();
        }

        protected void drpdlCicloNotaCompra_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlNotaCompra.DataBind();
        }

        protected void drpdlProveedoresNotaCompra_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlNotaCompra.DataBind();
        }
    }
}
