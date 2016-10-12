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
    public partial class frmPagosAProveedores : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.divAgregarNuevoPago.Attributes.Add("style", this.chkMostrarAgregarPago.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            if (!this.IsPostBack)
            {
                try
                {
                    this.compruebasecurityLevel();
                    this.drpdlProveedor.DataBind();
                    this.drpdlProveedor.SelectedIndex = -1;
                    this.drpdlCiclo.DataBind();
                    
                    if (this.drpdlProveedor.Items.Count > 0)
                    {
                        this.drpdlProveedor.SelectedIndex = 0;
                    }
                    
                    if (this.LoadEncryptedQueryString() > 0 && this.myQueryStrings["proveedorID"] != null)
                    {
                        this.drpdlProveedor.SelectedValue = this.myQueryStrings["proveedorID"].ToString();
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
                    this.txtFechaNPago.Text = Utils.Now.ToString("dd/MM/yyyy");
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Page_Load", this.Request.Url.ToString(), ref ex);
                }
            }

            cargadatosProveedor(int.Parse(this.drpdlProveedor.SelectedValue));
            ////
            this.divPagoMovCaja.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divMovBanco.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "MOVIMIENTO DE BANCO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divCheque.Attributes.Add("style", this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            ///
        }

        private void AddjstoControls()
        {
            String sOnchangeAB = "ShowHideDivOnChkBox('";
            sOnchangeAB += this.chkMostrarAgregarPago.ClientID + "','";
            sOnchangeAB += this.divAgregarNuevoPago.ClientID + "')";
            this.chkMostrarAgregarPago.Attributes.Add("onclick", sOnchangeAB);
        }

        protected void drpdlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                cargadatosProveedor(int.Parse(this.drpdlProveedor.SelectedValue));
            
        }

        protected bool cargadatosProveedor(int proveedorID)
        {
            bool bTodoBien = false;
           
            this.txtNombrePago.Text = this.drpdlProveedor.SelectedItem.Text;
            this.txtChequeNombre.Text = this.txtNombrePago.Text;
            bTodoBien = true;
            return bTodoBien;
        }

        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
            }

        }

        protected void btnAddPago_Click(object sender, EventArgs e)
        {
            this.pnlNewPago.Visible = false;
            if (this.cmbTipodeMovPago.SelectedValue == "0")//Es movimiento de caja chica
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
                        string sqlInsert = "INSERT INTO Pagos_Proveedores(fecha, proveedorID, movimientoID, cicloID, userID) VALUES (@fecha, @proveedorID, @movimientoID, @cicloID, @userID);";
                        SqlCommand cmdInsert = new SqlCommand(sqlInsert, conInsertNota);
                        conInsertNota.Open();
                        try
                        {
                            cmdInsert.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtRowainsertar.fecha;
                            cmdInsert.Parameters.Add("@proveedorID", SqlDbType.Int).Value = int.Parse(this.drpdlProveedor.SelectedValue);
                            cmdInsert.Parameters.Add("@movimientoID", SqlDbType.Int).Value = dtRowainsertar.movimientoID;
                            cmdInsert.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(this.drpdlCiclo.SelectedValue);
                            cmdInsert.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;

                            if (cmdInsert.ExecuteNonQuery() != 1)
                            {
                                throw new Exception("This must almost never happen");
                            }

                            Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PROVEEDORES, Logger.typeUserActions.UPDATE, this.UserID, "SE INSERTÓ UN PAGO AL PROVEEDOR " + this.drpdlProveedor.SelectedValue + " EL MOV DE CAJA CHICA FUE: " + dtRowainsertar.movimientoID.ToString());

                        }
                        catch (Exception ex)
                        {
                            Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL INSERTAR UN PAGO AL PROVEEDOR (CAJA CHICA): " + this.drpdlProveedor.SelectedValue + ". EX " + ex.Message, this.Request.Url.ToString());

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
                            if (dbFunctions.ChequeAlreadyExists(cheque, this.cmbCuentaPago.SelectedValue ))
                            {
                                this.pnlNewPago.Visible = true;
                                this.imgBienPago.Visible = false;
                                this.imgMalPago.Visible = true;
                                this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, ESTE CHEQUE YA HA SIDO AGREGADO";
                                //this.cmbTipodeMovPago.SelectedIndex = 0;
                                return;
                            }
                        }
                        else
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
                            this.imgMalPago.Visible = true;
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
                    if (dbFunctions.insertaMovBanco(ref dtRowainsertar, ref serror, int.Parse(this.Session["USERID"].ToString()), int.Parse(this.cmbCuentaPago.SelectedValue), int.Parse(this.drpdlCiclo.SelectedValue), -1, "", "PAGO A NOTA DE COMPRA"))
                    {





                        SqlConnection connVenta = new SqlConnection(myConfig.ConnectionInfo);
                        try
                        {
                            connVenta.Open();
                            SqlCommand commVenta = new SqlCommand();
                            commVenta.Connection = connVenta;
                            commVenta.CommandText = "INSERT INTO Pagos_Proveedores(fecha, proveedorID, movbanID, cicloID, userID) VALUES (@fecha, @proveedorID, @movbanID, @cicloID, @userID);";
                            commVenta.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtRowainsertar.fecha;
                            commVenta.Parameters.Add("@proveedorID", SqlDbType.Int).Value = int.Parse(this.drpdlProveedor.SelectedValue);
                            commVenta.Parameters.Add("@movbanID", SqlDbType.Int).Value = dtRowainsertar.movBanID;
                            commVenta.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(this.drpdlCiclo.SelectedValue);
                            commVenta.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;

                            if (commVenta.ExecuteNonQuery() != 1)
                            {
                                throw new Exception("This must almost never happen");
                            }
                        }
                        catch (System.Exception ex)
                        {
                            Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error adding new movbanco->PROVEEDOR PAGOS", ref ex);
                        }
                        finally
                        {
                            connVenta.Close();
                        }



                        //limpiacampos();
                        this.cmbTipodeMovPago.SelectedIndex = 0;
                        this.divPagoMovCaja.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                        this.divMovBanco.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "MOVIMIENTO DE BANCO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                        this.divCheque.Attributes.Add("style", this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                        //this.pnlNewPago.Visible = true;
                        //this.imgBienPago.Visible = true;
                        //this.imgMalPago.Visible = false;

                        //if (this.chkCreditoFinanciero.Checked)
                        //{
                        //    SqlConnection connCertificados = new SqlConnection(myConfig.ConnectionInfo);
                        //    try
                        //    {
                        //        connCertificados.Open();
                        //        SqlCommand commCertificados = new SqlCommand();
                        //        commCertificados.Connection = connCertificados;
                        //        commCertificados.CommandText = "INSERT INTO Certificado_MovBanco (CredFinCertID, movBanID) VALUES ";
                        //        commCertificados.Parameters.Add("@movBanID", SqlDbType.Int).Value = dtRowainsertar.movBanID;
                        //        int i = 0;
                        //        foreach (ListItem item in this.chkListCredBinCertificados.Items)
                        //        {
                        //            if (item.Selected)
                        //            {
                        //                if (i > 0)
                        //                {
                        //                    commCertificados.CommandText += ",";
                        //                }
                        //                commCertificados.CommandText += "(@CredFinCertID_" + i.ToString() + ",@movBanID)";
                        //                commCertificados.Parameters.Add("@CredFinCertID_" + i.ToString(), SqlDbType.Int).Value = item.Value;
                        //                i++;
                        //            }
                        //        }
                        //        if (i > 0)
                        //        {
                        //            int iInsertadas = ((int)commCertificados.ExecuteNonQuery());
                        //            if (iInsertadas <= 0)
                        //            {
                        //                throw new Exception("This must almost never happen");
                        //            }
                        //        }

                        //    }
                        //    catch (System.Exception ex)
                        //    {
                        //        Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error adding new movbanco->factura", ref ex);
                        //    }
                        //    finally
                        //    {
                        //        connCertificados.Close();
                        //    }
                        //}
                        //if (int.TryParse(this.lblcredFinID.Text, out credFinID) && credFinID > -1)
                        //{
                        //    this.lblNewMovResult.Text = "SE HA AGREGADO EXITOSAMENTE EL MOVIMIENTO DE BANCO AL CRÉDITO FINANCIERO";
                        //}
                        //else
                        //{
                        //    this.lblNewMovResult.Text = "SE HA AGREGADO EXITOSAMENTE EL MOVIMIENTO DE BANCO.";
                        //}
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
            this.btnActualizar_Click(null,null);
            this.txtMonto.Text = this.txtNombrePago.Text = this.txtFacturaLarguillo.Text = this.txtChequeNum.Text = this.txtChequeNombre.Text = "";
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
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {

                    //,,


                    if (this.grvPagos.DataKeys[e.Row.RowIndex]["movimientoID"].ToString() != "")
                    {
                        Label lbl = (Label)e.Row.FindControl("Label10");
                        lbl.Text = "";
                        lbl = (Label)e.Row.FindControl("Label11");
                        lbl.Text = "";
                        lbl = (Label)e.Row.FindControl("Label9");

                        //if columns are added/ removed please update the index.
                        lbl.Text = "EFECTIVO";

                        String query = "SELECT cargo FROM MovimientosCaja WHERE movimientoID=@movimientoID";
                        SqlCommand comm = new SqlCommand(query, conn);
                        comm.Parameters.Add("@movimientoID", SqlDbType.Float).Value = int.Parse(this.grvPagos.DataKeys[e.Row.RowIndex]["movimientoID"].ToString());
                        conn.Open();
                        lbl = (Label)e.Row.FindControl("Label12");
                        lbl.Text = string.Format("{0:C2}", Utils.GetSafeFloat(comm.ExecuteScalar().ToString()));

                    }
                    else if (this.grvPagos.DataKeys[e.Row.RowIndex]["movbanID"].ToString() != "")
                    {
                        String query = "SELECT     ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.cargo, Bancos.nombre, MovimientosCuentasBanco.numCheque ";
                        query += " FROM          MovimientosCuentasBanco INNER JOIN ";
                        query += " ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID ";
                        query += " INNER JOIN ";
                        query += " CuentasDeBanco ON MovimientosCuentasBanco.cuentaID = CuentasDeBanco.cuentaID INNER JOIN ";
                        query += " Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID ";

                        query += " where MovimientosCuentasBanco.movbanID=@movbanID";
                        SqlCommand comm = new SqlCommand(query, conn);
                        conn.Open();
                        comm.Parameters.Add("@movbanID", SqlDbType.Int).Value = int.Parse(this.grvPagos.DataKeys[e.Row.RowIndex]["movbanID"].ToString());
                        SqlDataReader rd = comm.ExecuteReader(); ;

                        //if columns are added/ removed please update the index.
                        if (rd.HasRows && rd.Read())
                        {
                            Label lbl = (Label)e.Row.FindControl("Label10");

                            lbl.Text = rd["numCheque"].ToString() != "0" ? rd["numCheque"].ToString() : "";

                            lbl = (Label)e.Row.FindControl("Label9");
                            lbl.Text = rd["Concepto"].ToString();



                            lbl = (Label)e.Row.FindControl("Label12");
                            lbl.Text = string.Format("{0:c2}", Utils.GetSafeFloat(rd["cargo"].ToString()));
                            lbl = (Label)e.Row.FindControl("Label11");
                            lbl.Text = rd["nombre"].ToString();
                        }

                    }

                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "error al meter js a controles en row", ref ex);

                }
                finally
                {

                    conn.Close();
                }


            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[6].Text = "TOTAL";


                e.Row.Cells[7].Text = string.Format("{0:C2}", sumaMontos());
            }

        }

        protected double sumaMontos()
        {
            Label lbl;
            double total = 0.0;
            foreach (GridViewRow row in this.grvPagos.Rows)
            {
                lbl = (Label)row.FindControl("Label12");
                total += Utils.GetSafeFloat(lbl.Text.Replace("$", "").Replace(",", ""));


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

        private void ActualizaTotales()
        {
            this.grvPagos.DataBind();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            this.ActualizaTotales();
        }
    }
}
