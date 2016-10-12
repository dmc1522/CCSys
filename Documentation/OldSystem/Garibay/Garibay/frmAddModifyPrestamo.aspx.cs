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
    public partial class frmAddModifyPrestamo : BasePage
    {

        private void LoadMontoEntregado()
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT SUM(ISNULL(MovimientosCuentasBanco.cargo, MovimientosCaja.cargo)) AS entregado FROM         MovimientosCuentasBanco RIGHT OUTER JOIN MovimientosCaja RIGHT OUTER JOIN Anticipos_Movimientos ON MovimientosCaja.movimientoID = Anticipos_Movimientos.movimientoID ON  MovimientosCuentasBanco.movbanID = Anticipos_Movimientos.movbanID WHERE     (Anticipos_Movimientos.anticipoID = @anticipoID)";
            comm.Parameters.Add("@anticipoID", SqlDbType.Int).Value = this.txtIdtoMod.Text;
            double monto = dbFunctions.GetExecuteDoubleScalar(comm, 0);
            this.lblMontoEntregado.Text = string.Format("{0:C2}", monto);
            this.lblMontoRestante.Text = string.Format("{0:C2}", Utils.GetSafeFloat(this.txtMonto.Text) -  monto);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            //this.divAgregarNuevoPago.Attributes.Add("style", this.chkMostrarAgregarPago.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            if (!IsPostBack)
            {
                try
                {
                    this.txtMonto.Text = "1";
                    this.compruebasecurityLevel();
                    this.ddlCiclos.DataBind();
                    this.ddlCredito.DataBind();
                    this.cmbConceptomovBancoPago.DataBind();
                    this.ddlCredito_SelectedIndexChanged(null, null);
                    try
                    {
                        DateTime ini = new DateTime();
                        DateTime fin = new DateTime();
                        int cicloID = 1;
                        if (int.TryParse(this.ddlCiclos.SelectedValue, out cicloID))
                        {
                            dbFunctions.sacaFechasCiclo(cicloID, ref ini, ref fin, this.UserID);
                            this.txtFechaLimite.Text = fin.ToString("dd/MM/yyyy");
                        }
                    }
                    catch (System.Exception ex)
                    {
                    	Logger.Instance.LogException(Logger.typeUserActions.SELECT,"fecha fin ciclo limite de pago", ref ex);
                    }
                    this.txtFechaPago.Text = this.txtFechaNPago.Text = Utils.Now.ToString("dd/MM/yyyy");
                    if (this.LoadEncryptedQueryString() > 0 && this.myQueryStrings["idtomodify"] != null)
                    {
                        this.txtIdtoMod.Text = this.myQueryStrings["idtomodify"].ToString();
                        this.lblTitle.Text = "MODIFICAR PRESTAMO";
                        this.LoadPrestamo(this.myQueryStrings["idtomodify"].ToString());
                        this.btnAddPrestamo.Visible = false;
                    }
                    else
                    {
                        this.btnUpdatePres.Visible = false;
                        //this.chkPnlAddProd.Checked = true;
                    }
                    this.imgBien.Visible = false;
                    this.imgMal.Visible = false;
                    this.pnlMensaje.Visible = false;
                    this.drpdlGrupoCatalogosInternaPago.SelectedValue = "12";
                    this.drpdlGrupoCuentaFiscal.SelectedValue = "12";
                    this.drpdlGrupoCatalogosCajaChica.SelectedValue = "12";
                    //this.drpdlGrupoCatalogosInternaPago.Enabled = false;
                    //this.drpdlGrupoCuentaFiscal.Enabled = false;
                    //this.drpdlGrupoCatalogosCajaChica.Enabled = false;
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Page_Load", this.Request.Url.ToString(), ref ex);
                }
            }
            this.divPagoMovCaja.Attributes.Add("style", this.txtIdtoMod.Text.Trim().Length > 0 && this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            String sOnchagemov = "";
            if (this.txtIdtoMod.Text.Trim().Length > 0)
            {

                sOnchagemov = "checkValueInList(";
                sOnchagemov += "this" + ",'EFECTIVO','";
                sOnchagemov += this.divPagoMovCaja.ClientID + "');";
                this.cmbTipodeMovPago.Attributes.Add("onChange", sOnchagemov);
            }
            this.divMovBanco.Attributes.Add("style", this.txtIdtoMod.Text.Trim().Length > 0 && this.cmbTipodeMovPago.SelectedItem.Text == "MOVIMIENTO DE BANCO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            if (this.txtIdtoMod.Text.Trim().Length > 0)
            {
                sOnchagemov += "checkValueInList(";
                sOnchagemov += "this" + ",'MOVIMIENTO DE BANCO','";
                sOnchagemov += this.divMovBanco.ClientID + "');";
                this.cmbTipodeMovPago.Attributes.Add("onChange", sOnchagemov);
            }

            this.divCheque.Attributes.Add("style", this.txtIdtoMod.Text.Trim().Length > 0 && this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            if (this.txtIdtoMod.Text.Trim().Length > 0)
            {
                sOnchagemov = "checkValueInList(";
                sOnchagemov += "this" + ",'CHEQUE','";
                sOnchagemov += this.divCheque.ClientID + "');";
                this.cmbConceptomovBancoPago.Attributes.Add("onChange", sOnchagemov);
            }
            this.LoadMontoEntregado();
            this.pnlAddMovMsg.Visible = false;
        }

        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
            }
        }

        protected void btnAddPrestamo_Click(object sender, EventArgs e)
        {
            this.addModifyPrestamo(true);
            this.txtInteresAnual.Text = this.txtInteresMoratorio.Text = this.txtMonto.Text = "";
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

        private void LoadPrestamo(string prestamoID)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            //SqlConnection conMov;
            try
            {
                
                //dsFacturasClientesTableAdapters.FacturasClientesVentaTableAdapter ta = new dsFacturasClientesTableAdapters.FacturasClientesVentaTableAdapter();
                String query = "SELECT     dbo.Anticipos.*, dbo.credito_prestamo.creditoID FROM dbo.credito_prestamo INNER JOIN dbo.Anticipos ON dbo.credito_prestamo.anticipoID = dbo.Anticipos.anticipoID where Anticipos.anticipoID =  @anticipoID";
                SqlCommand sqlComm = new SqlCommand(query, conn);
                sqlComm.Parameters.Add("@anticipoID", SqlDbType.Int).Value = prestamoID;
                conn.Open();
                
                SqlDataReader rdr = sqlComm.ExecuteReader();
                if (rdr.Read())
                {
                    this.ddlCiclos.DataBind();
                    this.txtIdtoMod.Text = rdr["anticipoID"].ToString();
                    this.ddlCiclos.SelectedValue = rdr["cicloID"].ToString();
                    this.ddlCredito.SelectedValue = rdr["creditoID"].ToString();
                    this.ddlCredito_SelectedIndexChanged(this, null);
                    this.PopCalendar7.SelectedDate = Utils.converttoshortFormatfromdbFormat(rdr["fecha"].ToString());
                    string aux = rdr["interesAnual"].ToString();
                    double x = Double.Parse(aux);
                    this.txtInteresAnual.Text = (x * 100).ToString();
                    aux = rdr["interesMoratorio"].ToString();
                    x = Double.Parse(aux);
                    this.txtInteresMoratorio.Text = (x * 100).ToString();
                    this.PopCalendar8.SelectedDate = Utils.converttoshortFormatfromdbFormat(rdr["fechaLimitePagoPrestamo"].ToString());
                    this.chkAddNewPago.Visible = true;
                    this.pnlAddPagos.Visible = true;
                    this.txtMonto.Text = ((double)rdr["monto"]).ToString("C2");
                    this.lnkImprimirPagare.Visible = true;
                    CreateURLForPagare(this.lnkImprimirPagare, this.ddlCredito.SelectedValue, this.txtMonto.Text);
                    this.chkCarteraVencida.Visible = false;
                    this.UpdateAddNewPago.Visible = this.chkAddNewPago.Visible = this.pnlAddPagos.Visible =!(bool)rdr["esCarteraVencida"];
                    if ((bool)rdr["esCarteraVencida"])
                    {
                        this.btnUpdatePres.Text = this.lblTitle.Text = "MODIFICAR CARTERA VENCIDA";
                    }
                }
                
                
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "LOAD PRESTAMO", this.Request.Url.ToString(), ref ex);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void addModifyPrestamo(bool agregar)
        {
            linkImpCheque.Visible = false;
            int prodID = -1;
            SqlConnection conSelecProd = new SqlConnection(myConfig.ConnectionInfo);
            string qryProd = "Select productorID from Creditos Where creditoID = @creditoID";
            SqlCommand cmdProd = new SqlCommand(qryProd, conSelecProd);

            conSelecProd.Open();
            try
            {
                cmdProd.Parameters.Add("@creditoID", SqlDbType.Int).Value = int.Parse(this.ddlCredito.SelectedValue);
                SqlDataReader rdr = cmdProd.ExecuteReader();
                if (rdr.Read())
                {
                    prodID = int.Parse(rdr[0].ToString());
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "adding prestamo", ref ex);
            }
            finally
            {
                conSelecProd.Close();
            }

            SqlCommand commAnticipo = new SqlCommand();
            string sqlInsert = string.Empty;
            if (agregar)
            {
                sqlInsert = "INSERT INTO Anticipos (tipoAnticipoID, cicloID, productorID, fecha, movimientoID, movbanID,interesAnual, interesMoratorio, fechaLimitePagoPrestamo, userID, storeTS, monto, esCarteraVencida) ";
                sqlInsert += "VALUES (@tipoAnticipoID, @cicloID, @productorID, @fecha, @movimientoID, @movbanID ,@interesAnual, @interesMoratorio, @fechaLimitePagoPrestamo, @userID, @storeTS, @monto, @esCarteraVencida); SELECT NewID = SCOPE_IDENTITY();";
                commAnticipo.CommandText = sqlInsert;
            }
            else
            {
                sqlInsert = "UPDATE Anticipos SET tipoAnticipoID = @tipoAnticipoID, cicloID = @cicloID, productorID = @productorID, fecha = @fecha, movimientoID = @movimientoID, movbanID = @movbanID ,interesAnual = @interesAnual, interesMoratorio = @interesMoratorio, fechaLimitePagoPrestamo = @fechaLimitePagoPrestamo, userID = @userID, monto=@monto ";
                sqlInsert += "Where anticipoID = @anticipoID;";
                commAnticipo.CommandText = sqlInsert;
            }
            commAnticipo.Parameters.Clear();
            commAnticipo.Parameters.Add("@tipoAnticipoID", SqlDbType.Int).Value = 2;
            commAnticipo.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(this.ddlCiclos.SelectedValue);
            commAnticipo.Parameters.Add("@productorID", SqlDbType.Int).Value = prodID;
            commAnticipo.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaNPago.Text));
            commAnticipo.Parameters.Add("@movimientoID", SqlDbType.Int).Value = DBNull.Value;
            commAnticipo.Parameters.Add("@movbanID", SqlDbType.Int).Value = DBNull.Value;
            commAnticipo.Parameters.Add("@interesAnual", SqlDbType.Float).Value = this.txtInteresAnual.Text == "" ? 0 : Utils.GetSafeFloat(float.Parse(this.txtInteresAnual.Text)) / 100;
            commAnticipo.Parameters.Add("@interesMoratorio", SqlDbType.Float).Value = this.txtInteresMoratorio.Text == "" ? 0 : Utils.GetSafeFloat(float.Parse(this.txtInteresMoratorio.Text)) / 100;
            commAnticipo.Parameters.Add("@fechaLimitePagoPrestamo", SqlDbType.DateTime).Value = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaLimite.Text));
            commAnticipo.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
            commAnticipo.Parameters.Add("@monto", SqlDbType.Int).Value = Utils.GetSafeFloat(this.txtMonto.Text);
            if (agregar)
            {
                commAnticipo.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.Now;
                commAnticipo.Parameters.Add("@esCarteraVencida", SqlDbType.Bit).Value = this.chkCarteraVencida.Checked ? 1 : 0;
            }
            else
            {
                commAnticipo.Parameters.Add("@anticipoID", SqlDbType.Int).Value = int.Parse(this.txtIdtoMod.Text);
            }

            
            if (!agregar)
            {
                int numregistros = 0;
                try
                {
                    conSelecProd.Open();
                    commAnticipo.Connection = conSelecProd;
                    numregistros = commAnticipo.ExecuteNonQuery();
                }
                finally
                {
                    conSelecProd.Close();
                }
                
                if (numregistros != 1)
                {
                    throw new Exception("ERROR AL MODIFICAR PRESTAMO. LA DB REGRESÓ QUE SE ALTERARON " + numregistros.ToString() + "REGISTROS");
                }
            }
            else
            {
                this.txtIdtoMod.Text = dbFunctions.GetExecuteIntScalar(commAnticipo,1).ToString();
                SqlConnection conInsRel = new SqlConnection(myConfig.ConnectionInfo);
                string qryRel = "DELETE FROM dbo.credito_prestamo WHERE (anticipoID = @anticipoID);INSERT INTO credito_prestamo (creditoID, anticipoID) Values (@creditoID, @anticipoID)";
                SqlCommand cmdRel = new SqlCommand(qryRel, conInsRel);

                conInsRel.Open();
                try
                {
                    cmdRel.Parameters.Add("@creditoID", SqlDbType.Int).Value = int.Parse(this.ddlCredito.SelectedValue);
                    cmdRel.Parameters.Add("@anticipoID", SqlDbType.Int).Value = int.Parse(this.txtIdtoMod.Text);
                    int numRegistros = cmdRel.ExecuteNonQuery();
                    if (numRegistros != 1)
                    {
                        throw new Exception("ERROR AL INSERTAR RELACIO PRESTAMO - CREDITO. LA DB REGRESÓ QUE SE ALTERARON " + numRegistros.ToString() + "REGISTROS");
                    }
                    String sQuery = "idtomodify=" + this.txtIdtoMod.Text;
                    sQuery = Utils.GetEncriptedQueryString(sQuery);
                    String strRedirect = "~/frmAddModifyPrestamo.aspx";
                    Response.Redirect(strRedirect + sQuery);
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "Error al insertar relacion prestamo-credito", this.Request.Url.ToString());
                }
                finally
                {
                    conInsRel.Close();
                }

            }


           this.pnlMensaje.Visible = false;
        }

        protected void btnUpdatePres_Click(object sender, EventArgs e)
        {
            this.addModifyPrestamo(false);
        }

        protected void ddlCredito_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtChequeNombre.Text = this.ddlCredito.SelectedItem.Text.Substring(0,this.ddlCredito.SelectedItem.Text.IndexOf(" -"));
            this.txtNombrePago.Text = this.ddlCredito.SelectedItem.Text.Substring(0,this.ddlCredito.SelectedItem.Text.IndexOf(" -") );
        }





        protected void CreateURLForPagare(HyperLink lnkImprimePagare, string idCredito, string total)
        {
            #region Add link for print pagare
            try
            {

                string sFileName = "PAGARE.pdf";
                sFileName = sFileName.Replace(" ", "_");
                string sURL = "frmDescargaTmpFile.aspx";
                string datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
                datosaencriptar = datosaencriptar + "solID=-1&creditoID=" + idCredito + "&";
                datosaencriptar += "impPagare=1&monto=" + Utils.GetSafeFloat(this.txtMonto.Text).ToString() + "&";
                datosaencriptar += "fecha=" + this.txtFechaNPago.Text + "&";
                datosaencriptar += "fechaPagare=" + this.txtFechaLimite.Text + "&";
                string URLcomplete = sURL + "?data=";
                URLcomplete += Utils.encriptacadena(datosaencriptar);
                lnkImprimePagare.NavigateUrl = this.Request.Url.ToString();
                JSUtils.OpenNewWindowOnClick(ref lnkImprimePagare, URLcomplete, "Pagare", true);
            }

            catch { }
            #endregion
        }

        protected void btnAgregarMovimiento_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlInsert = string.Empty;
                if (this.cmbTipodeMovPago.SelectedValue == "0")//Es movimiento de caja chica
                {
                    try
                    {
                        double montoEfectivo = 0;
                        if(!double.TryParse(this.txtMontoEfectivo.Text, out montoEfectivo))
                        {
                            this.pnlAddMovMsg.Visible = true;
                            this.pnlAddMovMsgBien.Visible = false;
                            this.pnlAddMovMsgMal.Visible = true;
                            this.lblpnlAddMovMsgResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO, MONTO DE EFECTIVO INVALIDO";
                            return;
                        }

                        if (montoEfectivo > (Utils.GetSafeFloat(this.txtMonto.Text) - Utils.GetSafeFloat(this.lblMontoEntregado.Text)))
                        {
                            this.pnlAddMovMsg.Visible = true;
                            this.pnlAddMovMsgBien.Visible = false;
                            this.pnlAddMovMsgMal.Visible = true;
                            this.lblpnlAddMovMsgResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO, EL MONTO A PAGAR ES MAYOR QUE EL MONTO RESTANTE A ENTREGAR";
                            return;
                        }


                        dsMovCajaChica.dtMovCajaChicaDataTable tablaaux = new dsMovCajaChica.dtMovCajaChicaDataTable();
                        dsMovCajaChica.dtMovCajaChicaRow dtRowainsertar = tablaaux.NewdtMovCajaChicaRow();
                        dtRowainsertar.nombre = this.txtNombrePago.Text;
                        dtRowainsertar.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaPago.Text));
                        dtRowainsertar.cargo = double.Parse(this.txtMontoEfectivo.Text);
                        dtRowainsertar.abono = 0.00;
                        dtRowainsertar.storeTS = DateTime.Parse(Utils.getNowFormattedDate());
                        dtRowainsertar.updateTS = DateTime.Parse(Utils.getNowFormattedDate());
                        dtRowainsertar.Observaciones = "Prestamo";
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


                        if (dbFunctions.insertaMovCaja(ref dtRowainsertar, ref serror, this.UserID, int.Parse(this.ddlCiclos.SelectedValue)))
                        {
                            this.txtMovBan.Text = "";
                            this.txtMovCaj.Text = dtRowainsertar.movimientoID.ToString();
                            /////////////////////////Si se inserto correctamente el mov de caja insertar el anticipo(Prestamo)
                            String sNewMov = dtRowainsertar.movimientoID.ToString();
                            sqlInsert = string.Empty;

                            SqlConnection conInsRel = new SqlConnection(myConfig.ConnectionInfo);
                            string qryRel = "INSERT INTO Anticipos_Movimientos (anticipoID, movimientoID) Values (@anticipoID, @movimientoID)";
                            SqlCommand cmdRel = new SqlCommand(qryRel, conInsRel);
                            conInsRel.Open();
                            try
                            {
                                cmdRel.Parameters.Add("@movimientoID", SqlDbType.Int).Value = dtRowainsertar.movimientoID;
                                cmdRel.Parameters.Add("@anticipoID", SqlDbType.Int).Value = int.Parse(this.txtIdtoMod.Text);
                                int numRegistros = cmdRel.ExecuteNonQuery();
                                if (numRegistros != 1)
                                {
                                    throw new Exception("ERROR AL INSERTAR RELACIO PRESTAMO - MOVIMIENTO. LA DB REGRESÓ QUE SE ALTERARON " + numRegistros.ToString() + "REGISTROS");
                                }
                                //String sQuery = "idtomodify=" + this.txtIdtoMod.Text;
                                //sQuery = Utils.GetEncriptedQueryString(sQuery);
                                //String strRedirect = "~/frmAddModifyPrestamo.aspx";
                                //Response.Redirect(strRedirect+sQuery);
                            }
                            catch (System.Exception ex)
                            {
                                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "Error al insertar relacion prestamo-credito", this.Request.Url.ToString());
                                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "ERROR AL INSERTAR RELACION PRESTAMO - MOVIMIENTO", ex);
                            }
                            finally
                            {
                                conInsRel.Close();
                            }


                            
                            this.pnlAddMovMsg.Visible = true;
                            this.pnlAddMovMsgBien.Visible = true;
                            this.pnlAddMovMsgMal.Visible = false;
                            this.lblpnlAddMovMsgResult.Text = "EL MOVIMIENTO SE HA AGREGADO SATISFACTORIAMENTE";
                            //Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDECAJACHICA, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), "AGREGÓ EL MOVIMIENTO DE CAJA CHICA NÚMERO: " + dtRowainsertar.movimientoID.ToString());
                            //this.btnActualizaPagos_Click(null, null);
                            //this.ActualizaTotales();
                        }
                        else
                        {
                            this.pnlAddMovMsg.Visible = true;
                            this.pnlAddMovMsgBien.Visible = false;
                            this.pnlAddMovMsgMal.Visible = true;
                            this.lblpnlAddMovMsgResult.Text = string.Format(myConfig.StrFromMessages("MOVCAJAADDEDFAILED"), dtRowainsertar.movimientoID.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "ERROR AL INSERTAR MOVIMIENTO DE CAJA. EX : " + ex.Message, this.Request.Url.ToString());
                        this.pnlAddMovMsg.Visible = true;
                        this.pnlAddMovMsgBien.Visible = false;
                        this.pnlAddMovMsgMal.Visible = true;
                        this.lblpnlAddMovMsgResult.Text = ex.Message;
                        return;
                    }
                }
                else
                    if (this.cmbTipodeMovPago.SelectedValue == "1")//Es movimiento de banco
                    {
                        double montoBanco = 0;
                        if (!double.TryParse(this.txtMontoMovimiento.Text, out montoBanco))
                        {
                            this.pnlAddMovMsg.Visible = true;
                            this.pnlAddMovMsgBien.Visible = false;
                            this.pnlAddMovMsgMal.Visible = true;
                            this.lblpnlAddMovMsgResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO, MONTO DE BANCO INVALIDO";
                            return;
                        }

                        if (montoBanco > (Utils.GetSafeFloat(this.txtMonto.Text) - Utils.GetSafeFloat(this.lblMontoEntregado.Text)) )
                        {
                            this.pnlAddMovMsg.Visible = true;
                            this.pnlAddMovMsgBien.Visible = false;
                            this.pnlAddMovMsgMal.Visible = true;
                            this.lblpnlAddMovMsgResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO, EL MONTO A PAGAR ES MAYOR QUE EL MONTO RESTANTE A ENTREGAR";
                            return;
                        }



                        if (dbFunctions.FechaEnPeriodoBloqueado(DateTime.Parse(this.txtFechaNPago.Text), int.Parse(this.cmbCuentaPago.SelectedValue)))
                        {
                            this.pnlAddMovMsg.Visible = true;
                            this.pnlAddMovMsgBien.Visible = false;
                            this.pnlAddMovMsgMal.Visible = true;
                            this.lblpnlAddMovMsgResult.Text = "EL MOVIMIENTO NO PUEDE SER AGREGADO YA QUE LA FECHA ESTA DENTRO DE UN PERIODO BLOQUEADO<BR />DESBLOQUEE EL PERIODO PARA PERMITIR INGRESAR EL MOVIMIENTO";
                            return;
                        }
                        int cheque = 0;
                        bool hayerrorenmonto = false;
                        double monto = 0;
                        double.TryParse(this.txtMontoMovimiento.Text, out monto);
                        dsMovBanco.dtMovBancoDataTable tablaaux = new dsMovBanco.dtMovBancoDataTable();
                        dsMovBanco.dtMovBancoRow dtRowainsertar = tablaaux.NewdtMovBancoRow();
                        if (monto == 0)
                        {
                            if (this.drpdlCatalogoInternoPago.SelectedItem.Text != "10J -  CHEQUES CANCELADOS")
                            {
                                hayerrorenmonto = true;
                            }
                        }
                        if (hayerrorenmonto)
                        {
                            this.pnlAddMovMsg.Visible = true;
                            this.pnlAddMovMsgBien.Visible = false;
                            this.pnlAddMovMsgMal.Visible = true;
                            this.lblpnlAddMovMsgResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, ERROR EN MONTO, ESCRIBA CANTIDAD VÁLIDA";
                            return;
                        }


                        //dtRowainsertar.cicloID = int.Parse(this.drpdlCiclo.SelectedValue);
                        dtRowainsertar.chequecobrado = false;
                        dtRowainsertar.conceptoID = int.Parse(this.cmbConceptomovBancoPago.SelectedValue);
                        dtRowainsertar.nombre = this.txtNombrePago.Text;
                        dtRowainsertar.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaPago.Text));
                        //datos de cheque

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

                        dtRowainsertar.cargo = this.txtMontoMovimiento.Text.Length > 0 ? double.Parse(this.txtMontoMovimiento.Text) : 0;
                        dtRowainsertar.abono = 0.00;

                        dtRowainsertar.storeTS = DateTime.Parse(Utils.getNowFormattedDate());
                        dtRowainsertar.updateTS = DateTime.Parse(Utils.getNowFormattedDate());

                        String serror = "", tipo = "";
                        dtRowainsertar.cuentaID = int.Parse(this.cmbCuentaPago.SelectedValue);

                        dtRowainsertar.creditoFinancieroID = -1;

                        if (this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE")
                        {
                            if (int.TryParse(this.txtChequeNum.Text, out cheque))
                            {
                                if (dbFunctions.ChequeAlreadyExists(cheque, this.cmbCuentaPago.SelectedValue))
                                {
                                    this.pnlAddMovMsg.Visible = true;
                                    this.pnlAddMovMsgBien.Visible = false;
                                    this.pnlAddMovMsgMal.Visible = true;
                                    this.lblpnlAddMovMsgResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, ESTE CHEQUE YA HA SIDO AGREGADO";
                                    return;
                                }
                            }
                            else
                            {
                                this.pnlAddMovMsg.Visible = true;
                                this.pnlAddMovMsgBien.Visible = false;
                                this.pnlAddMovMsgMal.Visible = true;
                                this.lblpnlAddMovMsgResult.Text = "ERROR!! EL NUMERO DE CHEQUE ES INCORRECTO";
                                return;
                            }


                            if (!numChequeValido(cheque, int.Parse(this.cmbCuentaPago.SelectedValue)))
                            {
                                this.pnlAddMovMsg.Visible = true;
                                this.pnlAddMovMsgBien.Visible = false;
                                this.pnlAddMovMsgMal.Visible = true;
                                this.lblpnlAddMovMsgResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, EL NUMERO DE CHEQUE NO CORRESPONDE A EL NUMERO DE CUENTA";
                                return;
                            }
                            //dtRowainsertar.fechaCheque = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaCheque.Text));
                            if (this.chkCobradoFecha.Checked)
                            {
                                dtRowainsertar.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaCheque.Text));
                                dtRowainsertar.fechachequedecobro = dtRowainsertar.fecha;
                                dtRowainsertar.chequecobrado = true;
                            }
                        }


                        ListBox a = new ListBox();
                        if (dbFunctions.insertaMovBanco(ref dtRowainsertar, ref serror, this.UserID, int.Parse(this.cmbCuentaPago.SelectedValue), int.Parse(this.ddlCiclos.SelectedValue), -1, "", "PAGO EN PRESTAMO"))
                        {
                            this.txtMovBan.Text = dtRowainsertar.movBanID.ToString();
                            this.txtMovCaj.Text = "";
                            //////////////////////////Si el mov de banco se inserto correctamente metemos el anticipo(prestamo)                        

                            String sNewMov = dtRowainsertar.movBanID.ToString();
                            sqlInsert = "";
                            SqlConnection conInsRel = new SqlConnection(myConfig.ConnectionInfo);
                            string qryRel = "INSERT INTO Anticipos_Movimientos (anticipoID, movbanID) Values (@anticipoID, @movbanID)";
                            SqlCommand cmdRel = new SqlCommand(qryRel, conInsRel);
                            conInsRel.Open();
                            try
                            {
                                cmdRel.Parameters.Add("@movbanID", SqlDbType.Int).Value = dtRowainsertar.movBanID;
                                cmdRel.Parameters.Add("@anticipoID", SqlDbType.Int).Value = int.Parse(this.txtIdtoMod.Text);
                                int numRegistros = cmdRel.ExecuteNonQuery();
                                if (numRegistros != 1)
                                {
                                    throw new Exception("ERROR AL INSERTAR RELACIO PRESTAMO - CREDITO. LA DB REGRESÓ QUE SE ALTERARON " + numRegistros.ToString() + "REGISTROS");
                                }
                            }
                            catch (System.Exception ex)
                            {
                                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "Error al insertar relacion prestamo-credito", this.Request.Url.ToString());
                                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "ERROR AL INSERTAR MOVIMIENTO DE CAJA. EX : " + ex.Message, this.Request.Url.ToString());
                                this.pnlAddMovMsg.Visible = true;
                                this.pnlAddMovMsgBien.Visible = false;
                                this.pnlAddMovMsgMal.Visible = true;
                                this.lblpnlAddMovMsgResult.Text = ex.Message;
                            }
                            finally
                            {
                                conInsRel.Close();
                            }
                            this.pnlAddMovMsg.Visible = true;
                            this.pnlAddMovMsgBien.Visible = true;
                            this.pnlAddMovMsgMal.Visible = false;
                            //////////////////////////////////////7
                            CreateURLForPagare(this.lnkImprimirPagare, this.ddlCredito.SelectedValue, this.txtMonto.Text);
/*
                            if (dtRowainsertar.conceptoID == 3)
                            {
                                string parameter, ventanatitle = "IMPRIMIR CHEQUE";
                                // String pathArchivotemp = PdfCreator.printLiquidacion(0, PdfCreator.tamañoPapel.CARTA, PdfCreator.orientacionPapel.VERTICAL, ref this.gvBoletas, ref gvAnticipos, ref gvPagosLiquidacion);
                                string datosaencriptar;
                                datosaencriptar = "iMovID=";
                                datosaencriptar += dtRowainsertar.movBanID.ToString();
                                datosaencriptar += "&";

                                parameter = "javascript:url('";
                                parameter += "frmPrintCheque.aspx?data=";
                                parameter += Utils.encriptacadena(datosaencriptar);
                                parameter += "', '";
                                parameter += ventanatitle;
                                parameter += "',200,200,300,300); return false;";
                                linkImpCheque.Attributes.Add("onClick", parameter);
                                linkImpCheque.NavigateUrl = this.Request.Url.ToString();
                                linkImpCheque.Visible = true;
                                linkImpCheque.Text = "IMPRIMIR CHEQUE";
*/

                            }

                            this.lblpnlAddMovMsgResult.Text = "EL MOVIMIENTO SE HA AGREGADO SATISFACTORIAMENTE";
                        }
                        else
                        {
                            this.pnlAddMovMsg.Visible = true;
                            this.pnlAddMovMsgBien.Visible = false;
                            this.pnlAddMovMsgMal.Visible = true;
                            this.lblpnlAddMovMsgResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO";

                        }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "insertando movimiento para prestamo", ref ex);
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "ERROR AL INSERTAR MOVIMIENTO DE CAJA. EX : " + ex.Message, this.Request.Url.ToString());
                this.pnlAddMovMsg.Visible = true;
                this.pnlAddMovMsgBien.Visible = false;
                this.pnlAddMovMsgMal.Visible = true;
                this.lblpnlAddMovMsgResult.Text = ex.Message;
            }
            this.gvPagosFactura.DataSourceID = "sdsPagosFactura";
            this.gvPagosFactura.DataBind();
        }


        protected void gvPagosFactura_DataBound(object sender, EventArgs e)
        {
            this.LoadMontoEntregado();
        }

        protected string GetPrintChequeURL(string id)
        {
            string parameter = "javascript:url('";
            parameter += "frmPrintCheque.aspx" + Utils.GetEncriptedQueryString("iMovID=" + id);
            parameter += "', 'Print Cheque',200,200,300,300); return false;";
            return parameter;
        }

        protected bool IsChequeVisible(string id)
        {
            int cheq = -1;
            if (id != null && int.TryParse(id.Trim(), out cheq) && cheq > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void gvPagosFactura_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                int movid = -1;
                string error = string.Empty;
                if (e.Keys["movbanID"] != null && int.TryParse(e.Keys["movbanID"].ToString(), out movid))
                {
                    dbFunctions.deleteMovementdeBanco(movid, error, this.UserID,  -1);
                    comm.CommandText = "delete from Anticipos_Movimientos  where movbanID = @movbanID ";
                    comm.Parameters.Add("@movbanID", SqlDbType.Int).Value = movid;
                    comm.ExecuteNonQuery();
                }
                else
                    if (e.Keys["movimientoID"] != null && int.TryParse(e.Keys["movimientoID"].ToString(), out movid))
                    {
                        dbFunctions.deleteMovementdeCaja(movid, ref error, this.UserID);
                        comm.CommandText = "delete from Anticipos_Movimientos  where movimientoID = @movimientoID ";
                        comm.Parameters.Add("@movimientoID", SqlDbType.Int).Value = movid;
                        comm.ExecuteNonQuery();
                    }
                
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "ERROR DELETING PAGOS IN FACTURAS DE GANADO", ref ex);
            }
            finally
            {
                conn.Close();
                this.gvPagosFactura.DataBind();
            }
        }

        protected void ddlCiclos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlCredito.DataBind();
        }

    }
}
