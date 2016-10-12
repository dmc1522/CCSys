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
    public partial class frmPagosCreditosAdd : Garibay.BasePage

    {
        protected void Page_Load(object sender, EventArgs e)
        {

           // this.divAgregarNuevoPago.Attributes.Add("style", this.chkMostrarAgregarPago.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");

            if (!this.IsPostBack)
            {
                this.divFechaSalidaNewBoleta.Attributes.Add("style", this.chkChangeFechaSalidaNewBoleta.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                if (Request.QueryString["data"] != null && this.loadqueryStrings(Request.QueryString["data"].ToString()) && this.myQueryStrings != null && this.myQueryStrings["creditoID"] != null)
                {
                    this.ddlCredito.DataBind();
                    this.ddlCredito.SelectedValue = this.myQueryStrings["creditoID"].ToString();
                    this.TextBox1.Text = this.myQueryStrings["creditoID"].ToString();                   
                    this.ddlCredito.Enabled = false;
                }
                else
                {
                    //this.chkPnlAddProd.Checked = true;
                }

                this.txtFechaNPago.Text = Utils.Now.ToShortDateString();               
                
                //this.AddjstoControls();
                //this.cmbTipodeMovPago.DataBind();
                this.imgBienPago.Visible = false;
                this.imgMalPago.Visible = false;
                this.pnlNewPago.Visible = false;
                this.ddlCredito.DataBind();
                this.TextBox1.Text = this.ddlCredito.SelectedValue;

                if (this.ddlCredito.Items.Count > 0)
                    this.ddlCredito_SelectedIndexChanged(null, null);
            }
            this.AddjstoControls();
            //this.LoadDTFromSession();
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            string serror = "";
            try
            {
                //if (this.chbMostrarPnlAddTarjDiesel.Checked)
                //{
                //    if (dbFunctions.addTarjetaDiesel(int.Parse(this.ddlCredito.SelectedValue),
                //                                 int.Parse(txtfoliodiesel.Text),
                //                                 float.Parse(this.txtMontoTarjetaDiesel.Text),
                //                                 float.Parse(this.txtLitrosTarjetaDiesel.Text),
                //                                 ref serror,
                //                                 int.Parse(this.Session["USERID"].ToString()), 2))
                //    {
                        
                        
                //        //this.txtFolio.ReadOnly = true;
                //    }

                //    else
                //    {
                //        throw new Exception(serror);
                //    }
                //}
                //this.grdTarjetasDiesel.DataBind();
            }
            catch (Exception ex)
            {
                this.cmbTipodeMovPago.SelectedIndex = 0;
                this.pnlNewPago.Visible = true;
                this.imgBienPago.Visible = false;
                this.imgMalPago.Visible = true;
                this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR LA TARJETA DIESEL";
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "Error adding tarjeta diesel. El error fue :" + ex.Message, this.Request.Url.ToString());

            }
        }


        private void AddjstoControls()
        {
            this.divPagoMovCaja.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divMovBanco.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "TRANSFERENCIA" || this.cmbTipodeMovPago.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divboletas.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "BOLETA" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divCheque.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divDiesel.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "TARJETA DIESEL" ? "visibility: visible; display: block" : "visibility: hidden; display: none");

            String sOnchagemov = "checkValueInListNotasVentas2(";
            sOnchagemov += "this" + ",'";
            sOnchagemov += this.divDiesel.ClientID + "','";
            sOnchagemov += this.divPagoMovCaja.ClientID + "','";
            sOnchagemov += this.divboletas.ClientID + "','";
            sOnchagemov += this.divMovBanco.ClientID + "','";

            sOnchagemov += this.divCheque.ClientID + "')";

            this.cmbTipodeMovPago.Attributes.Add("onChange", sOnchagemov);
            //this.cmbTipodeMovPago.DataBind();

            sOnchagemov = "ShowHideDivOnChkBox('";
            sOnchagemov += this.chkChangeFechaSalidaNewBoleta.ClientID + "','";
            sOnchagemov += this.divFechaSalidaNewBoleta.ClientID + "')";
            this.chkChangeFechaSalidaNewBoleta.Attributes.Add("onclick", sOnchagemov);

            sOnchagemov = "subTextBoxes('";
            sOnchagemov += this.txtNewPesoEntrada.ClientID + "','";
            sOnchagemov += this.txtNewPesoSalida.ClientID + "','";
            sOnchagemov += this.txtPesoNetoNewBoleta.ClientID + "')";
            this.txtNewPesoEntrada.Attributes.Add("onKeyUp", sOnchagemov);
            this.txtNewPesoEntrada.Attributes.Add("onBlur", sOnchagemov);
            this.txtNewPesoSalida.Attributes.Add("onKeyUp", sOnchagemov);
            this.txtNewPesoSalida.Attributes.Add("onBlur", sOnchagemov);
            
        }

        protected void btnAddPago_Click(object sender, EventArgs e)
        {
            this.pnlNewPago.Visible = false;
            int cheque = 0;
            bool hayerrorenmonto = false;
            double monto = 0;
            dsMovBanco.dtMovBancoDataTable tablaaux = new dsMovBanco.dtMovBancoDataTable();
            dsMovBanco.dtMovBancoRow dtRowainsertar = tablaaux.NewdtMovBancoRow();
            String serror = "", tipo = "";
            ListBox a = new ListBox();
            if (this.cmbTipodeMovPago.SelectedValue == "0")//Es movimiento de caja chica
            {
                try
                {
                    dsMovCajaChica.dtMovCajaChicaDataTable tablaaux2 = new dsMovCajaChica.dtMovCajaChicaDataTable();
                    dsMovCajaChica.dtMovCajaChicaRow dtRowainsertar2 = tablaaux2.NewdtMovCajaChicaRow();
                    dtRowainsertar2.nombre = this.txtNombrePago.Text;
                    dtRowainsertar2.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaNPago.Text));
                    dtRowainsertar2.cargo = 0.00;
                    dtRowainsertar2.abono = double.Parse(this.txtMonto.Text);
                    dtRowainsertar2.storeTS = DateTime.Parse(Utils.getNowFormattedDate());
                    dtRowainsertar2.updateTS = DateTime.Parse(Utils.getNowFormattedDate());
                    dtRowainsertar2.Observaciones = "Pago a credito";
                    dtRowainsertar2.catalogoMovBancoInternoID = int.Parse(this.drpdlCatalogocuentaCajaChica.SelectedValue);
                    if (this.drpdlSubcatalogoCajaChica.SelectedIndex > 0)
                        dtRowainsertar2.subCatalogoMovBancoInternoID = int.Parse(this.drpdlSubcatalogoCajaChica.SelectedValue);
                    dtRowainsertar2.numCabezas = 0;
                    dtRowainsertar2.facturaOlarguillo = "";
                    dtRowainsertar2.bodegaID = int.Parse(this.ddlPagosBodegas.SelectedValue);
                    dtRowainsertar2.cobrado = true;
                    //dtRowainsertar.Bodega = this.ddlBodegas.SelectedItem.Text;

                    serror = "";
                    ListBox listBoxAgregadas = new ListBox();
                    if (dbFunctions.insertaMovCaja(ref dtRowainsertar2, ref serror, this.UserID, int.Parse(this.cmbCiclo.SelectedValue)))
                    {
                        String sNewMov = dtRowainsertar2.movimientoID.ToString();
                        SqlConnection conInsertNota = new SqlConnection(myConfig.ConnectionInfo);
                        string sqlInsert = "insert into Pagos_Credito (fecha, creditoID, movimientoID) VALUES(@fecha, @creditoID, @movimientoID)";
                        SqlCommand cmdInsert = new SqlCommand(sqlInsert, conInsertNota);
                        conInsertNota.Open();
                        try
                        {
                            cmdInsert.Parameters.Clear();
                            cmdInsert.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtRowainsertar2.fecha;
                            cmdInsert.Parameters.Add("@creditoID", SqlDbType.Int).Value = int.Parse(this.ddlCredito.SelectedValue);
                            cmdInsert.Parameters.Add("@movimientoID", SqlDbType.Int).Value = dtRowainsertar2.movimientoID;
                            int numregistros = cmdInsert.ExecuteNonQuery();
                            if (numregistros != 1)
                            {
                                throw new Exception("ERROR AL INSERTAR RELACION Creditos - PAGOS. LA DB REGRESÓ QUE SE ALTERARON " + numregistros.ToString() + "REGISTROS");
                            }


                            Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CREDITOS, Logger.typeUserActions.UPDATE, this.UserID, "SE INSERTÓ UN PAGO AL CREDITO " + this.ddlCredito.SelectedValue + " EL MOV DE CAJA CHICA FUE: " + dtRowainsertar2.movimientoID.ToString());

                        }
                        catch (Exception ex)
                        {
                            Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL INSERTAR UN PAGO AL CREDITO (CAJA CHICA): " + this.ddlCredito.SelectedValue +  ". EX " + ex.Message, this.Request.Url.ToString());

                        }
                        //this.pnlNewPago.Visible = true;
                        //this.imgBienPago.Visible = true;
                        //this.imgMalPago.Visible = false;
                        //this.lblNewPagoResult.Text = string.Format(myConfig.StrFromMessages("MOVCAJAADDEDEXITO"), sNewMov);
                        Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDECAJACHICA, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), "AGREGÓ EL MOVIMIENTO DE CAJA CHICA NÚMERO: " + dtRowainsertar2.movimientoID.ToString());


                        //this.ActualizaTotales();
                    }
                    else
                    {
                        this.pnlNewPago.Visible = true;
                        this.imgBienPago.Visible = false;
                        this.imgMalPago.Visible = true;
                        this.lblNewPagoResult.Text = string.Format(myConfig.StrFromMessages("MOVCAJAADDEDFAILED"), dtRowainsertar2.movimientoID.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "ERROR AL INSERTAR MOVIMIENTO DE CAJA. EX : " + ex.Message, this.Request.Url.ToString());
                }
            }
            else
                if (this.cmbTipodeMovPago.SelectedValue == "4" || this.cmbTipodeMovPago.SelectedValue == "5") //ES TRANSFERENCIA O DEPOSITO
                {
                    cheque = 0;
                    hayerrorenmonto = false;
                    monto = 0;
                    double.TryParse(this.txtMonto.Text, out monto);
                    tablaaux = new dsMovBanco.dtMovBancoDataTable();
                    dtRowainsertar = tablaaux.NewdtMovBancoRow();
                    dtRowainsertar.chequecobrado = true;
                    dtRowainsertar.conceptoID = this.cmbTipodeMovPago.SelectedValue == "4" ? 1 : 2;
                    dtRowainsertar.nombre = this.txtNombrePago.Text;
                    dtRowainsertar.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaNPago.Text));
                    dtRowainsertar.numCheque = this.txtChequeNum.Text.Length > 0 ? int.Parse(this.txtChequeNum.Text) : 0;
                    dtRowainsertar.chequeNombre = this.txtChequeNombre.Text;
                    dtRowainsertar.facturaOlarguillo = this.txtFacturaLarguillo.Text;
                    dtRowainsertar.numCabezas = 0;
                    dtRowainsertar.catalogoMovBancoInternoID = int.Parse(this.drpdlCatalogoInternoPago.SelectedValue);
                    if (this.drpdlSubcatologointernaPago.SelectedIndex > -1)
                        dtRowainsertar.subCatalogoMovBancoInternoID = int.Parse(this.drpdlSubcatologointernaPago.SelectedValue);
                    dtRowainsertar.catalogoMovBancoFiscalID = int.Parse(this.drpdlCatalogocuentafiscalPago.SelectedValue);
                    if (this.drpdlSubcatalogofiscalPago.SelectedIndex > -1)
                        dtRowainsertar.subCatalogoMovBancoFiscalID = int.Parse(this.drpdlSubcatalogofiscalPago.SelectedValue);
                    dtRowainsertar.cargo = 0.00;
                    dtRowainsertar.abono = this.txtMonto.Text.Length > 0 ? double.Parse(this.txtMonto.Text) : 0;
                    dtRowainsertar.storeTS = DateTime.Parse(Utils.getNowFormattedDate());
                    dtRowainsertar.updateTS = DateTime.Parse(Utils.getNowFormattedDate());
                    serror = "";
                    tipo = "";
                    dtRowainsertar.cuentaID = int.Parse(this.cmbCuentaPago.SelectedValue);
                    dtRowainsertar.creditoFinancieroID = -1;
                    a = new ListBox();
                    if (dbFunctions.insertaMovBanco(ref dtRowainsertar, ref serror, int.Parse(this.Session["USERID"].ToString()), int.Parse(this.cmbCuentaPago.SelectedValue), int.Parse(this.cmbCiclo.SelectedValue), -1, "", "PAGO A NOTA DE VENTA"))
                    {
                        SqlConnection connVenta = new SqlConnection(myConfig.ConnectionInfo);
                        try
                        {
                            connVenta.Open();
                            SqlCommand commVenta = new SqlCommand();
                            commVenta.Connection = connVenta;
                            commVenta.CommandText = "INSERT INTO Pagos_Credito(fecha, creditoID, movbanID) VALUES (@fecha,@creditoID,@movbanID) ";
                            commVenta.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtRowainsertar.fecha;
                            commVenta.Parameters.Add("@creditoID", SqlDbType.Int).Value = int.Parse(this.ddlCredito.SelectedValue);
                            commVenta.Parameters.Add("@movbanID", SqlDbType.Int).Value = dtRowainsertar.movBanID;

                            if (commVenta.ExecuteNonQuery() != 1)
                            {
                                throw new Exception("This must almost never happen");
                            }
                        }


                        catch (System.Exception ex)
                        {
                            Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error adding new movbanco->credito", ref ex);
                        }
                        finally
                        {
                            connVenta.Close();
                        }
                        this.cmbTipodeMovPago.SelectedIndex = 0;
                        this.pnlNewPago.Visible = true;
                        this.imgBienPago.Visible = true;
                        this.imgMalPago.Visible = false;
                        this.lblNewPagoResult.Text = "EL MOVIMIENTO DE BANCO NÚMERO :" + dtRowainsertar.movBanID.ToString() + " SE HA AGREGADO SATISFACTORIAMENTE";
                    }


                }
                else
                    if (this.cmbTipodeMovPago.SelectedValue == "1")//CHEQUE
                    {
                        cheque = 0;
                        hayerrorenmonto = false;
                        monto = 0;
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

                        if (this.cmbTipodeMovPago.SelectedItem.Text == "CHEQUE")
                        {
                            if (int.TryParse(this.txtChequeNum.Text, out cheque))
                            {
                                if (dbFunctions.ChequeAlreadyExists(cheque, this.cmbCuentaPago.SelectedValue))
                                {
                                    this.pnlNewPago.Visible = true;
                                    this.imgBienPago.Visible = false;
                                    this.imgMalPago.Visible = true;
                                    this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, ESTE CHEQUE YA HA SIDO AGREGADO";
                                    this.cmbTipodeMovPago.SelectedIndex = 0;
                                    return;
                                }
                            }
                            else
                            {
                                this.pnlNewPago.Visible = true;
                                this.imgBienPago.Visible = false;
                                this.imgMalPago.Visible = true;
                                this.lblNewPagoResult.Text = "ERROR!! EL NUMERO DE CHEQUE ES INCORRECTO";
                                this.cmbTipodeMovPago.SelectedIndex = 0;
                                return;
                            }

                            if (!numChequeValido(cheque, int.Parse(this.cmbCuentaPago.SelectedValue)))
                            {

                                this.pnlNewPago.Visible = true;
                                this.imgBienPago.Visible = false;
                                this.imgMalPago.Visible = false;
                                this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, EL NUMERO DE CHEQUE NO CORRESPONDE A EL NUMERO DE CUENTA";
                                return;
                            }
                        }

                        tablaaux = new dsMovBanco.dtMovBancoDataTable();
                        dtRowainsertar = tablaaux.NewdtMovBancoRow();
                        dtRowainsertar.chequecobrado = true;
                        dtRowainsertar.conceptoID = 3;
                        dtRowainsertar.nombre = this.txtNombrePago.Text;
                        dtRowainsertar.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaNPago.Text));
                        dtRowainsertar.numCheque = this.txtChequeNum.Text.Length > 0 ? int.Parse(this.txtChequeNum.Text) : 0;
                        dtRowainsertar.chequeNombre = this.txtChequeNombre.Text;
                        dtRowainsertar.facturaOlarguillo = this.txtFacturaLarguillo.Text;
                        dtRowainsertar.numCabezas = 0;//this.txtNumCabezas.Text.Length > 0 ? double.Parse(this.txtNumCabezas.Text) : 0;

                        dtRowainsertar.catalogoMovBancoInternoID = int.Parse(this.drpdlCatalogoInternoPago.SelectedValue);
                        if (this.drpdlSubcatologointernaPago.SelectedIndex > -1)
                            dtRowainsertar.subCatalogoMovBancoInternoID = int.Parse(this.drpdlSubcatologointernaPago.SelectedValue);
                        dtRowainsertar.catalogoMovBancoFiscalID = int.Parse(this.drpdlCatalogocuentafiscalPago.SelectedValue);
                        if (this.drpdlSubcatalogofiscalPago.SelectedIndex > -1)
                            dtRowainsertar.subCatalogoMovBancoFiscalID = int.Parse(this.drpdlSubcatalogofiscalPago.SelectedValue);
                        dtRowainsertar.cargo = 0.00;
                        dtRowainsertar.abono = this.txtMonto.Text.Length > 0 ? double.Parse(this.txtMonto.Text) : 0;
                        dtRowainsertar.storeTS = DateTime.Parse(Utils.getNowFormattedDate());
                        dtRowainsertar.updateTS = DateTime.Parse(Utils.getNowFormattedDate());
                        serror = "";
                        tipo = "";
                        dtRowainsertar.cuentaID = int.Parse(this.cmbCuentaPago.SelectedValue);
                        dtRowainsertar.creditoFinancieroID = -1;
                        a = new ListBox();
                        if (dbFunctions.insertaMovBanco(ref dtRowainsertar, ref serror, int.Parse(this.Session["USERID"].ToString()), int.Parse(this.cmbCuentaPago.SelectedValue), int.Parse(this.cmbCiclo.SelectedValue), -1, "", "PAGO A NOTA DE VENTA"))
                        {
                            SqlConnection connVenta = new SqlConnection(myConfig.ConnectionInfo);
                            try
                            {
                                connVenta.Open();
                                SqlCommand commVenta = new SqlCommand();
                                commVenta.Connection = connVenta;
                                commVenta.CommandText = "INSERT INTO Pagos_Credito(fecha, creditoID, movbanID) VALUES (@fecha,@creditoID,@movbanID) ";
                                commVenta.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtRowainsertar.fecha;
                                commVenta.Parameters.Add("@creditoID", SqlDbType.Int).Value = int.Parse(this.ddlCredito.SelectedValue);
                                commVenta.Parameters.Add("@movbanID", SqlDbType.Int).Value = dtRowainsertar.movBanID;

                                if (commVenta.ExecuteNonQuery() != 1)
                                {
                                    throw new Exception("This must almost never happen");
                                }
                            }
                            catch (System.Exception ex)
                            {
                                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error adding new movbanco->credito", ref ex);
                            }
                            finally
                            {
                                connVenta.Close();
                            }
                            this.cmbTipodeMovPago.SelectedIndex = 0;
                            this.pnlNewPago.Visible = true;
                            this.imgBienPago.Visible = true;
                            this.imgMalPago.Visible = false;
                            this.lblNewPagoResult.Text = "EL MOVIMIENTO DE BANCO NÚMERO :" + dtRowainsertar.movBanID.ToString() + " SE HA AGREGADO SATISFACTORIAMENTE";
                        }
                        else
                        {
                            this.cmbTipodeMovPago.SelectedIndex = 0;
                            this.pnlNewPago.Visible = true;
                            this.imgBienPago.Visible = false;
                            this.imgMalPago.Visible = true;
                            this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO";
                        }
                    }
                    else if (this.cmbTipodeMovPago.SelectedValue == "3")
                    {
                        SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
                        try
                        {
                            sqlConn.Open();
                           
                            SqlConnection addConn = new SqlConnection(myConfig.ConnectionInfo);
                            SqlCommand addComm = new SqlCommand();
                            addComm.Connection = addConn;
                            addConn.Open();
                            addComm.CommandText = "INSERT INTO Boletas (productorID, NombreProductor, NumeroBoleta, Ticket, bodegaID, cicloID, FechaEntrada, PesoDeEntrada, FechaSalida, PesoDeSalida, pesonetoentrada,  pesonetosalida, totalapagar, productoID, humedad, impurezas, pesonetoapagar, importe, precioapagar, dctoSecado, userID) VALUES     (@productorID,@NombreProductor,@NumeroBoleta,@Ticket,@bodegaID,@cicloID,@FechaEntrada,@PesoDeEntrada,@FechaSalida,@PesoDeSalida,@pesonetoentrada,@pesonetosalida,@totalapagar,@productoID,@humedad,@impurezas,@pesonetoapagar,@importe, @precioapagar,@dctoSecado,@userID); select boletaID = SCOPE_IDENTITY();";


                            addComm.Parameters.Add("@productorID", SqlDbType.Int).Value = int.Parse(this.ddlNewBoletaProductor.SelectedValue);
                            addComm.Parameters.Add("@NombreProductor", SqlDbType.VarChar).Value = this.ddlNewBoletaProductor.SelectedItem.Text;
                            //    //addComm.Parameters.AddWithValue("@NombreProductor", newRow.NombreProductor);
                            addComm.Parameters.Add("@NumeroBoleta", SqlDbType.VarChar).Value = this.txtNewNumBoleta.Text;
                            //    //addComm.Parameters.AddWithValue("@NumeroBoleta", newRow.NumeroBoleta);
                            //    //  addComm.Parameters.AddWithValue("@Ticket", newRow.Ticket);
                            addComm.Parameters.Add("@Ticket", SqlDbType.VarChar).Value = this.txtNewTicket.Text;
                            //    //  addComm.Parameters.AddWithValue("@bodegaID", newRow.bodegaID);
                            addComm.Parameters.Add("@bodegaID", SqlDbType.Int).Value = int.Parse(this.ddlNewBoletaBodega.SelectedValue);
                            //    //  addComm.Parameters.AddWithValue("@cicloID", newRow.cicloID);
                            addComm.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(this.cmbCiclo.SelectedValue);
                            //    //  addComm.Parameters.AddWithValue("@FechaEntrada", newRow.FechaEntrada);
                            DateTime dtFechaEntrada = new DateTime();
                            if (!DateTime.TryParse(this.txtNewFechaEntrada.Text /*+ " " + this.txtNewHoraEntrada.Text*/, out dtFechaEntrada))
                            {
                                /*DateTime.TryParse(this.txtNewFechaEntrada.Text, out dtFechaEntrada);*/
                                dtFechaEntrada = Utils.Now;
                            }

                            addComm.Parameters.Add("@FechaEntrada", SqlDbType.DateTime).Value = dtFechaEntrada;
                            double dPesoEntrada = 0;
                            double.TryParse(this.txtNewPesoEntrada.Text, out dPesoEntrada);
                            addComm.Parameters.Add("@PesoDeEntrada", SqlDbType.Float).Value = dPesoEntrada;



                            DateTime dtFechaSalida = new DateTime();
                            if (this.chkChangeFechaSalidaNewBoleta.Checked)
                            {
                                if (!DateTime.TryParse(this.txtNewFechaSalida.Text /*+ " " + this.txtNewHoraEntrada.Text*/, out dtFechaSalida))
                                {
                                    //DateTime.TryParse(this.txtNewFechaSalida.Text, out dtFechaSalida);
                                    dtFechaSalida = Utils.Now;
                                }
                            }
                            else
                                dtFechaSalida = dtFechaEntrada;
                            addComm.Parameters.Add("@FechaSalida", SqlDbType.DateTime).Value = dtFechaSalida;
                            double dPesoSalida = 0;
                            double.TryParse(this.txtNewPesoSalida.Text, out dPesoSalida);
                            addComm.Parameters.Add("@PesoDeSalida", SqlDbType.Float).Value = dPesoSalida;
                            double dPesoNetoEntrada = 0;
                            double dPesoNetoSalida = 0;
                            if (dPesoEntrada - dPesoSalida > 0)
                            {
                                dPesoNetoEntrada = dPesoEntrada - dPesoSalida;
                                dPesoNetoSalida = 0;
                            }
                            else
                            {
                                dPesoNetoEntrada = 0;
                                dPesoNetoSalida = dPesoSalida - dPesoEntrada;
                            }

                            addComm.Parameters.Add("@pesonetoentrada", SqlDbType.Float).Value = dPesoNetoEntrada;
                            addComm.Parameters.Add("@pesonetosalida", SqlDbType.Float).Value = dPesoNetoSalida;
                            addComm.Parameters.Add("@pesonetoapagar", SqlDbType.Float).Value = dPesoNetoEntrada - dPesoNetoSalida;


                            decimal dPrecio = 0;

                            decimal.TryParse(this.txtNewPrecio.Text, out dPrecio);
                            addComm.Parameters.Add("@importe", SqlDbType.Float).Value = (dPesoNetoEntrada - dPesoNetoSalida) * float.Parse(dPrecio.ToString());

                            //    // addComm.Parameters.AddWithValue("@productoID", newRow.productoID);
                            addComm.Parameters.Add("@productoID", SqlDbType.Int).Value = int.Parse(this.ddlNewBoletaProducto.SelectedValue);

                            double dHumedad = 0;
                            double.TryParse(this.txtNewHumedad.Text, out dHumedad);

                            double dImpurezas = 0;
                            double.TryParse(this.txtNewImpurezas.Text, out dImpurezas);
                            addComm.Parameters.Add("@humedad", SqlDbType.Float).Value = dHumedad;
                            addComm.Parameters.Add("@impurezas", SqlDbType.Float).Value = dImpurezas;
                            dPrecio = 0;
                            decimal.TryParse(this.txtNewPrecio.Text, out dPrecio);
                            double dSecado = 0;
                            double.TryParse(this.txtNewSecado.Text, out dSecado);
                            addComm.Parameters.Add("@dctoSecado", SqlDbType.Float).Value = dSecado;
                            addComm.Parameters.Add("@precioapagar", SqlDbType.Float).Value = dPrecio;
                            double kgh = 0;
                            double kgi = 0;
                            double kgs = 0;
                            Utils.getDesctoHumedad(dHumedad, kgh);
                            Utils.getDesctoImpurezas(dImpurezas, kgi);
                            Utils.getDesctoSecado(dImpurezas, kgs);
                            addComm.Parameters.Add("@totalapagar", SqlDbType.Float).Value = (dPesoNetoEntrada - kgh - kgi) * float.Parse(dPrecio.ToString()) - kgs;
                            addComm.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                            int newbol = int.Parse(addComm.ExecuteScalar().ToString());
                            string qr = "INSERT INTO Pagos_Credito  (fecha, creditoID, boletaID) VALUES     (@fecha, @creditoID, @boletaID)";
                            addComm.Parameters.Clear();
                            addComm.CommandText = qr;
                            addComm.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtFechaEntrada;
                            addComm.Parameters.Add("@creditoID", SqlDbType.Int).Value = int.Parse(this.ddlCredito.SelectedValue);
                            addComm.Parameters.Add("@boletaID", SqlDbType.Int).Value = newbol;
                            addComm.ExecuteNonQuery();
                            this.txtNewNumBoleta.Text = "";
                            this.txtNewTicket.Text = "";
                            this.ddlNewBoletaProducto.SelectedIndex = 0;

                            this.txtNewFechaEntrada.Text = this.txtNewFechaSalida.Text = Utils.Now.ToString("dd/MM/yyyy");
                            this.txtPesoNetoNewBoleta.Text = this.txtNewPesoEntrada.Text = this.txtNewPesoSalida.Text = "0";
                            this.txtNewSecado.Text = this.txtNewHumedad.Text = this.txtNewImpurezas.Text = this.txtNewPrecio.Text = "0";


                            this.ddlNewBoletaBodega
                                .DataBind();
                            this.ddlNewBoletaBodega.SelectedValue = this.BodegaID.ToString();

                        }
                        catch (System.Exception ex)
                        {
                            Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error agregando boleta En Credito", this.Request.Url.ToString(), ref ex);
                            Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(Session["USERID"].ToString()), "Error Insertando Nueva Boleta EX:" + ex.Message, this.Request.Url.ToString());
                        }
                        finally
                        {
                            sqlConn.Close();
                        }
                    }
                    else if (this.cmbTipodeMovPago.SelectedValue == "2")//TARJETA DIESEL
                    {

                        serror = "";
                        try
                        {
                            if (dbFunctions.addTarjetaDiesel(int.Parse(this.ddlCredito.SelectedValue),
                                                         int.Parse(txtfoliodiesel.Text),
                                                         float.Parse(this.txtMonto.Text),
                                                         float.Parse(this.txtLitrosTarjetaDiesel.Text),
                                                         ref serror,
                                                         int.Parse(this.Session["USERID"].ToString()), 2))
                            {
                                this.grvPagos.DataBind();                               
                            }

                            else
                            {
                                throw new Exception(serror);
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            this.cmbTipodeMovPago.SelectedIndex = 0;
                            this.pnlNewPago.Visible = true;
                            this.imgBienPago.Visible = false;
                            this.imgMalPago.Visible = true;
                            this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR LA TARJETA DIESEL";
                            Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "Error adding tarjeta diesel. El error fue :" + ex.Message, this.Request.Url.ToString());

                        }

                    }
            this.cmbTipodeMovPago.SelectedIndex = 0;
            this.txtChequeNum.Text = "";
            this.grvPagos.DataBind();
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

        protected void ddlCredito_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i =this.ddlCredito.SelectedIndex;
            string nombre = "";
            this.TextBox1.Text = this.ddlCredito.SelectedValue;
            nombre = this.ddlCredito.SelectedItem.Text;

            nombre = nombre.Substring(0, nombre.LastIndexOf(" -"));


            this.txtChequeNombre.Text = nombre;
            this.txtNombrePago.Text = nombre;

            //this.cmbTipodeMovPago.SelectedIndex = 0;
        }

        protected void btnEdoDeCuenta_Click(object sender, EventArgs e)
        {
            String sNewUrl = "~/frmEstadodeCuentaCredito.aspx?data=";
            sNewUrl += Utils.encriptacadena("creditoID=" + this.ddlCredito.SelectedValue);
            Response.Redirect(sNewUrl);
        }

        protected void actualizaPagos()
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand comm = new SqlCommand();
            try
            {
                String sqry = "SELECT  isnull(SUM(MovimientosCaja.cargo),0)+ isnull(SUM(MovimientosCuentasBanco.cargo),0)";
                sqry += " FROM         MovimientosCaja right JOIN";
                sqry += " Pagos_Credito ON MovimientosCaja.movimientoID = Pagos_Credito.movimientoID Left JOIN";
                sqry += " MovimientosCuentasBanco ON Pagos_Credito.movbanID = MovimientosCuentasBanco.movbanID";
                sqry += " WHERE     (Pagos_Credito.creditoID = @creditoIDotaventaID)";
                conn.Open();
                comm.Connection = conn;
                comm.CommandText = sqry;
                comm.Parameters.Add("@creditoID", SqlDbType.Int).Value = this.ddlCredito.SelectedValue;
                //this.lblPagos.Text = string.Format("{0:C2}", float.Parse(comm.ExecuteScalar().ToString()));
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "actualiza Pagos de la Nota de Venta", ref ex);
            }
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

                        String query = "SELECT abono FROM MovimientosCaja WHERE movimientoID=@movimientoID";
                        SqlCommand comm = new SqlCommand(query, conn);
                        comm.Parameters.Add("@movimientoID", SqlDbType.Float).Value = int.Parse(this.grvPagos.DataKeys[e.Row.RowIndex]["movimientoID"].ToString());
                        conn.Open();
                        lbl = (Label)e.Row.FindControl("Label12");
                        lbl.Text = string.Format("{0:C2}", Utils.GetSafeFloat(comm.ExecuteScalar().ToString()));

                    }
                    else if (this.grvPagos.DataKeys[e.Row.RowIndex]["tarjetaDieselID"].ToString() != "")
                    {
                        Label lbl = (Label)e.Row.FindControl("Label10");
                        lbl.Text = this.grvPagos.DataKeys[e.Row.RowIndex]["tarjetaDieselID"].ToString();
                        lbl = (Label)e.Row.FindControl("Label11");
                        lbl.Text = "";
                        lbl = (Label)e.Row.FindControl("Label9");

                        //if columns are added/ removed please update the index.
                        lbl.Text = "TARJETA DIESEL";

                        String query = "SELECT monto FROM TarjetasDiesel WHERE folio=@folio";
                        SqlCommand comm = new SqlCommand(query, conn);
                        comm.Parameters.Add("@folio", SqlDbType.Int).Value = int.Parse(this.grvPagos.DataKeys[e.Row.RowIndex]["tarjetaDieselID"].ToString());
                        conn.Open();
                        lbl = (Label)e.Row.FindControl("Label12");
                        lbl.Text = string.Format("{0:C2}", Utils.GetSafeFloat(comm.ExecuteScalar().ToString()));

                    }
                    else if (this.grvPagos.DataKeys[e.Row.RowIndex]["movbanID"].ToString() != "")
                    {
                        String query = "SELECT     ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.abono, Bancos.nombre, MovimientosCuentasBanco.numCheque ";
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
                            lbl.Text = string.Format("{0:c2}", Utils.GetSafeFloat(rd["abono"].ToString()));
                            lbl = (Label)e.Row.FindControl("Label11");
                            lbl.Text = rd["nombre"].ToString();
                        }
                    }
                    else if (this.grvPagos.DataKeys[e.Row.RowIndex]["boletaID"].ToString() != "")
                    {
                        String query = "SELECT     Pagos_Credito.fecha, Boletas.Ticket, Boletas.totalapagar ";
                        query += " FROM         Boletas INNER JOIN ";
                        query += " Pagos_Credito ON Boletas.boletaID = Pagos_Credito.boletaID ";
                        query += " WHERE     (Boletas.boletaID = @boletaID)";
                        SqlCommand comm = new SqlCommand(query, conn);
                        conn.Open();
                        comm.Parameters.Add("@boletaID", SqlDbType.Int).Value = int.Parse(this.grvPagos.DataKeys[e.Row.RowIndex]["boletaID"].ToString());
                        SqlDataReader rd = comm.ExecuteReader(); ;

                        //if columns are added/ removed please update the index.
                        if (rd.HasRows && rd.Read())
                        {
                            Label lbl = (Label)e.Row.FindControl("Label10");
                            lbl.Text = rd["Ticket"].ToString();
                            lbl = (Label)e.Row.FindControl("Label9");
                            lbl.Text = "BOLETA";
                            lbl = (Label)e.Row.FindControl("Label12");
                            lbl.Text = string.Format("{0:c2}", Utils.GetSafeFloat(rd["totalapagar"].ToString()));
                            lbl = (Label)e.Row.FindControl("Label11");
                            lbl.Text = "";
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
                e.Row.Cells[7].Text = "TOTAL";
                e.Row.Cells[8].Text = string.Format("{0:C2}", sumaMontos());
            }
        }

        protected double sumaMontos()
        {
            Label lbl;
            double total = 0.0;
            foreach (GridViewRow row in this.grvPagos.Rows)
            {
                lbl = (Label)row.Cells[7].FindControl("Label12");
                total += Utils.GetSafeFloat(lbl.Text.Replace("$", "").Replace(",", ""));


            }
            return total;
        }

        protected void grvPagos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection conDeleteMovimientos = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdDeleteMovimientos = new SqlCommand();
            cmdDeleteMovimientos.Connection = conDeleteMovimientos;
            conDeleteMovimientos.Open();
            try
            {
                cmdDeleteMovimientos.CommandText = "DELETE from movimientoscaja where movimientoId = (select movimientoId from pagos_credito where pagoCreditoId = @pagoCreditoId)";
                cmdDeleteMovimientos.Parameters.Add("@pagoCreditoId", SqlDbType.Int).Value = (int)e.Keys["pagoCreditoID"];
                cmdDeleteMovimientos.ExecuteNonQuery();
                cmdDeleteMovimientos.CommandText = "DELETE from movimientoscuentasbanco where movBanId = (select movBanId from pagos_credito where pagoCreditoId = @pagoCreditoId)";
                cmdDeleteMovimientos.ExecuteNonQuery();
                SqlPagos.DeleteCommand = "DELETE FROM Pagos_Credito WHERE (Pagos_Credito.pagoCreditoID = @pagoCreditoID)";
                SqlPagos.DeleteParameters.Add("@pagoCreditoID", e.Keys["pagoCreditoID"].ToString());
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "ERROR BORRANDO MOVIMIENTOS RELACIONADOS A PAGO CREDITO ", ref ex);
            }
          
        }

        protected void grvPagos_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            if(e.Exception!=null)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, this.UserID, "ERROR BORRANDO PAGO " + e.Exception.StackTrace, this.Request.Url.ToString());

            }
        }

        protected void drpdlCatalogocuentaCajaChica_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpdlGrupoCuentaFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpdlCatalogocuentafiscalPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpdlGrupoCatalogosInternaPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpdlSubcatalogofiscalPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpdlGrupoCatalogosCajaChica_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpdlSubcatalogoCajaChica_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
