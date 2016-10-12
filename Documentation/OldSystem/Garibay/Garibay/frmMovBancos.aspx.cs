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
using System.Text;
using System.IO;

namespace Garibay
{
    public partial class frmMovBancos : Garibay.BasePage
    {
        dsMovBanco.dtMovBancoDataTable dtTeibol = new dsMovBanco.dtMovBancoDataTable(); 
     

        protected void rellenadt(DateTime fechainicio, DateTime fechafin)
        {
            try
            {
                double fSaldoInicial = 0, fSaldoFinal = 0;
                if (dbFunctions.fillDTMovBancos(int.Parse(this.drpdlCuenta.SelectedValue), fechainicio, fechafin, ref fSaldoInicial, ref fSaldoFinal, ref dtTeibol))
                {
                    this.gridMovCuentasBanco.DataSourceID = "";
                    this.gridMovCuentasBanco.DataSource = dtTeibol;
                    this.gridMovCuentasBanco.DataBind();
                    if (dtTeibol.Rows.Count == 0)
                    {
                        fSaldoFinal =  fSaldoInicial;

                    }
                    this.lblSaldofinal.Text = string.Format("{0:c}", Utils.GetSafeFloat(fSaldoFinal));
                    this.lblSaldoInicial.Text = string.Format("{0:c}", Utils.GetSafeFloat(fSaldoInicial));
                }
            }
            catch (Exception exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());

            }

    }
        private void reloadGridView()
        {
            DateTime dtInicio = new DateTime();
            DateTime dtFin = new DateTime();
            TimeSpan tsOneDay = new TimeSpan(1, 0, 0, 0);
            if (chkPorMes.Checked || (!chkPorMes.Checked && !chkPorFechas.Checked))
            {
                this.chkPorMes.Checked = true;
                dtInicio = new DateTime(int.Parse(this.drddlAnio.SelectedValue), int.Parse(this.drpdlMes.SelectedValue), 1);
                // TimeSpan tsUndia = new TimeSpan(1, 0, 0, 0);
                if (dtInicio.Month < 12)
                    dtFin = new DateTime(dtInicio.Year, dtInicio.Month + 1, 1, 23, 59, 59);
                else
                    dtFin = new DateTime(dtInicio.Year + 1, 1, 1, 23, 59, 59);
                dtFin = dtFin - tsOneDay;
            }
            else
            {
                try
                {
                    dtInicio = DateTime.Parse(this.txtFechaIncio.Text, new CultureInfo("es-Mx"));
                    dtFin = DateTime.Parse(this.txtFechaFin.Text, new CultureInfo("es-Mx"));
                }
                catch
                {
                    DateTime dtInicio1 = new DateTime();
                    DateTime dtFin1 = new DateTime();

                    dtInicio1 = new DateTime(Utils.Now.Year, Utils.Now.Month, 1);
                    dtFin1 = Utils.Now;
                    this.txtFechaIncio.Text = dtInicio1.ToString("dd/MM/yyyy");
                    this.txtFechaFin.Text = dtFin1.ToString("dd/MM/yyyy");
                }
            }
            this.lblFechaInicio.Text = dtInicio.ToString("dd/MM/yyyy");
            this.lblfechaFIn.Text = dtFin.ToString("dd/MM/yyyy");
            this.rellenadt(dtInicio, dtFin);
            this.sacaCabezasdeGanado(dtInicio, dtFin);
            this.sacaTotales();
            this.LoadSaldos();
            this.MakeVisibleBloqDesPeriodos();
        }

        protected void sacaCabezasdeGanado(DateTime fechainicio, DateTime fechafin)
        {
            SqlConnection conSacaCabezas = new SqlConnection(myConfig.ConnectionInfo);
            string query = "SELECT     sum(numCabezas) FROM         MovimientosCuentasBanco ";
            query += "where cuentaID = @cuentaID AND fecha>=@fechaini and fecha <= @fechafin ";
            SqlCommand cmdSacaCabezas = new SqlCommand(query, conSacaCabezas);
            try{
                conSacaCabezas.Open();
                this.lblContador.Text = "0";
                cmdSacaCabezas.Parameters.Clear();
                cmdSacaCabezas.Parameters.Add("@fechaini", SqlDbType.DateTime).Value = fechainicio.ToString("yyyy/MM/dd 00:00:00");
                cmdSacaCabezas.Parameters.Add("@fechafin", SqlDbType.DateTime).Value = fechafin.ToString("yyyy/MM/dd 23:59:59");
                cmdSacaCabezas.Parameters.Add("@cuentaID", SqlDbType.Int).Value = int.Parse(this.drpdlCuenta.SelectedValue);
                this.lblContador.Text = cmdSacaCabezas.ExecuteScalar() != null ? cmdSacaCabezas.ExecuteScalar().ToString() : "0";
                   

            }
            catch(Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, this.UserID, "ERROR OBTENIENDO CABEZAS " + ex.Message, this.Request.Url.ToString());
                

            }
            finally
            {
                conSacaCabezas.Close();
            }

        }
     
        private void LoadSaldos()
        {     
           try
           {
           
                dsMovBanco.dtSaldosDataTable dtSaldos = new dsMovBanco.dtSaldosDataTable();
                SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand comm = new SqlCommand();
                DateTime dtBaseInicio = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaIncio.Text));
                DateTime dtBaseFin = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaFin.Text));
                DateTime dtBaseFinAux = new DateTime();
                DateTime dtBaseInAux = new DateTime();
                if (this.chkPorFechas.Checked && dtBaseFin.Year - dtBaseInicio.Year<2 && ((dtBaseFin.Year == dtBaseInicio.Year && dtBaseFin.Month-dtBaseInicio.Month==1)) || (dtBaseFin.Year-dtBaseInicio.Year==1 && dtBaseFin.Month==1 && dtBaseInicio.Month==12))
                {
                    this.gridSaldosMensuales.Visible = false;
                    this.gridSaldosporPeriodos.Visible = true;
                    this.lblCalculoSaldos.Text = "SALDOS DE MOV BANCOS POR PERIODO";
                    string sqlSacaMovimiento = "select top 1 fecha from MovimientosCuentasBanco where cuentaID = @cuentaID order by fecha ";
                    SqlConnection conSacaMov = new SqlConnection(myConfig.ConnectionInfo);
                    SqlCommand cmdSacaMov = new SqlCommand(sqlSacaMovimiento, conSacaMov);
                    conSacaMov.Open();
                    cmdSacaMov.Parameters.Add("@cuentaID", SqlDbType.Int).Value = this.drpdlCuenta.SelectedValue;
                    DateTime fechaprimermov;
                    if(cmdSacaMov.ExecuteScalar() == null ||  !DateTime.TryParse(cmdSacaMov.ExecuteScalar().ToString(), out fechaprimermov))
                    {
                        fechaprimermov = Utils.Now;
                    }
                    sqlSacaMovimiento = "select top 1 fecha from MovimientosCuentasBanco where cuentaID =@cuentaID order by fecha DESC ";
                    cmdSacaMov.CommandText = sqlSacaMovimiento;
                    DateTime fechaultimomov;
                    if (cmdSacaMov.ExecuteScalar() == null || !DateTime.TryParse(cmdSacaMov.ExecuteScalar().ToString(), out fechaultimomov))
                    {
                        fechaultimomov = Utils.Now;
                    }
                    
                    TimeSpan timedif = fechaultimomov.Subtract(fechaprimermov);
                    int numperiodos = (int)Math.Round(timedif.TotalDays / 28);
                    int diadecorte = dtBaseFin.Day;
                    if(diadecorte<fechaultimomov.Day)
                    {
                        int year =  fechaultimomov.Year; 
                        int month = fechaultimomov.Month+1; 
                        if(month > 12)
                        {
                            month = 1;
                            year += 1; 
                        }
                        dtBaseFinAux = new DateTime(year, month, diadecorte);
                        numperiodos++;
                    }
                    else
                    {
                        dtBaseFinAux = new DateTime(fechaultimomov.Year, fechaultimomov.Month, diadecorte);
                    }
                    
                   // DateTime oneMonth = new DateTime(0, 1, 0);
                    for (int i = numperiodos; i > 0; i--)
                    {
                       
                        string sqlsacasaldo = "SELECT     cuentaID, SUM(cargo) AS Cargos, SUM(abono) AS Abonos FROM         MovimientosCuentasBanco WHERE     (cuentaID = @cuentaID) AND (fecha <= @fechaFin) " +
                            " GROUP BY (cuentaID) ";
                        comm.Parameters.Clear();
                        comm.Parameters.Add("@cuentaID", SqlDbType.Int).Value = this.drpdlCuenta.SelectedValue;
                        comm.Parameters.Add("@fechaFin", SqlDbType.DateTime).Value = dtBaseFinAux;
                        int newmonth, newyear;
                        comm.CommandText = sqlsacasaldo;
                        comm.Connection = conn;
                        conn.Open();

                        SqlDataReader rdSaldos = comm.ExecuteReader();

                        if (rdSaldos.Read())
                        {
                            dsMovBanco.dtSaldosRow row = dtSaldos.NewdtSaldosRow();
                           
                            double abonos = 0.00, cargos = 0.00;
                            double.TryParse(rdSaldos[2].ToString(), out abonos);
                            double.TryParse(rdSaldos[1].ToString(), out cargos);
                            row.Saldo = abonos - cargos;
                            newmonth = dtBaseFinAux.Month > 1 ? dtBaseFinAux.Month - 1 : 12;
                            newyear = newmonth == 12 ? newyear = dtBaseFinAux.Year - 1 : dtBaseFinAux.Year;
                            dtBaseInAux = new DateTime(newyear, newmonth, dtBaseInicio.Day);
                            row.Periodo = dtBaseInAux.ToString("dd/") + Utils.getMonthName(dtBaseInAux.Month).Substring(0,3) + dtBaseInAux.ToString("/yyyy - ");
                            row.Periodo += dtBaseFinAux.ToString("dd/") + Utils.getMonthName(dtBaseFinAux.Month).Substring(0, 3) + dtBaseFinAux.ToString("/yyyy");
                            dtBaseFinAux = new DateTime(newyear, newmonth, diadecorte);
                            dtSaldos.Rows.Add(row);
                        }
                        conn.Close();
                    }
                    this.gridSaldosporPeriodos.DataSource = dtSaldos;
                    this.gridSaldosporPeriodos.DataBind();
                }
                else
                {
                    this.gridSaldosMensuales.Visible = true;
                    this.gridSaldosporPeriodos.Visible = false;
                    this.lblCalculoSaldos.Text = "SALDOS MENSUALES DE MOV BANCOS";



                    try
                    {
                        SqlDataAdapter sqlDA = new SqlDataAdapter(comm);
                        comm.Connection = conn;
                        comm.CommandText = "SELECT     MovimientosCuentasBanco.cuentaID, SUM(MovimientosCuentasBanco.cargo) AS Cargos, SUM(MovimientosCuentasBanco.abono) AS Abonos, Meses.month, YEAR(MovimientosCuentasBanco.fecha) as year FROM         MovimientosCuentasBanco INNER JOIN Meses ON MONTH(MovimientosCuentasBanco.fecha) = Meses.monthID where cuentaID = @cuentaID GROUP BY MovimientosCuentasBanco.cuentaID, MONTH(MovimientosCuentasBanco.fecha), YEAR(MovimientosCuentasBanco.fecha), Meses.month  ORDER BY YEAR(MovimientosCuentasBanco.fecha) DESC, MONTH(MovimientosCuentasBanco.fecha) DESC";
                        comm.Parameters.Add("@cuentaID", SqlDbType.Int).Value = this.drpdlCuenta.SelectedValue;
                        //comm.Parameters.Add("@year", SqlDbType.Int).Value = this.drddlAnio.SelectedValue;
                        sqlDA.Fill(dtSaldos);
                        double fSaldo = 0;
                        for (int i = dtSaldos.Rows.Count - 1; i >= 0; i--)
                        {
                            dsMovBanco.dtSaldosRow row = (dsMovBanco.dtSaldosRow)dtSaldos.Rows[i];
                            fSaldo += row.Abonos - row.Cargos;
                            row.Saldo = fSaldo;
                        }
                        //dtSaldos.DefaultView.RowFilter = "year = " + this.drddlAnio.SelectedValue;
                        this.gridSaldosMensuales.DataSource = dtSaldos;
                        this.gridSaldosMensuales.DataBind();
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.SELECT, "LoadSaldos", ref ex);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
           }
           catch (System.Exception ex)
           {
               Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Err sacando saldos", ref ex);
           }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UICulture = "es";
            Page.Culture = "es-MX";
            string Lang = "es-MX";//set your culture here
            System.Threading.Thread.CurrentThread.CurrentCulture =
                new System.Globalization.CultureInfo(Lang);

            if (!this.IsPostBack)
            {
                this.drpdlCuenta.DataBind();
                this.drpdlCuenta.SelectedIndex = 0;
                this.txtFechaChequeCobrado.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.chkPorMes.Checked = true;
                DateTime dtInicio = new DateTime();
                DateTime dtFin = new DateTime();
              
                dtInicio = new DateTime(Utils.Now.Year, Utils.Now.Month, 1);
                dtFin = Utils.Now;
                this.txtFechaIncio.Text = dtInicio.ToString("dd/MM/yyyy");
                this.txtFechaFin.Text = dtFin.ToString("dd/MM/yyyy");
                JSUtils.AddDisableWhenClick(ref this.btnAgregar, this);
                JSUtils.AddDisableWhenClick(ref this.btnAgregarBol, this);
                JSUtils.AddDisableWhenClick(ref this.btnQuitarBoleta, this);

                String sOnchange = "ShowHideDivOnChkBox('";
                sOnchange += this.chkboxAnticipo.ClientID + "','";
                sOnchange += this.divanticipo.ClientID + "')";
                this.chkboxAnticipo.Attributes.Add("onclick", sOnchange);



                sOnchange = "ShowHideDivOnChkBox('";
                sOnchange += this.chkNewBoleta.ClientID + "','";
                sOnchange += this.divNewBoleta.ClientID + "')";
                this.chkNewBoleta.Attributes.Add("onclick", sOnchange);

                sOnchange = "EnableDisableAbonosCargos('";
                sOnchange += this.ddlConcepto.ClientID + "','";
                sOnchange += this.cmbTipodeMov.ClientID + "');";
                this.ddlConcepto.Attributes.Add("onChange", sOnchange);


                sOnchange = "checkValueInList(";
                sOnchange += /*this.ddlConcepto.UniqueID*/"this" + ",'PRESTAMO','";
                sOnchange += this.divPrestamo.ClientID + "')";
                this.ddlTipoAnticipo.Attributes.Add("onChange", sOnchange);

                sOnchange = "ShowHideDivOnChkBox('";
                sOnchange += this.chkChangeFechaSalidaNewBoleta.ClientID + "','";
                sOnchange += this.divFechaSalidaNewBoleta.ClientID + "')";
                this.chkChangeFechaSalidaNewBoleta.Attributes.Add("onclick", sOnchange);

                sOnchange = "ShowHideDivOnChkBox('";
                sOnchange += this.chkAssignToCredit.ClientID + "','";
                sOnchange += this.divAssignCredito.ClientID + "')";
                this.chkAssignToCredit.Attributes.Add("onclick", sOnchange);

                sOnchange = "ShowHideDivOnChkBox('";
                sOnchange += this.chkMostrarAddMov.ClientID + "','";
                sOnchange += this.panelAgregar.ClientID + "')";
                this.chkMostrarAddMov.Attributes.Add("onclick", sOnchange);

           


                sOnchange = "ShowHideDivOnChkBox('";
                sOnchange += this.chkMostrarFiscales.ClientID + "','";
                sOnchange += this.divConceptosFiscales.ClientID + "')";
                this.chkMostrarFiscales.Attributes.Add("onclick", sOnchange);

                ///para mostrar las capas de cuenta destino y caja destino
                sOnchange = "showDivOnListContains(";
                sOnchange += /*this.ddlConcepto.UniqueID*/"this" + ",'TRASPASOS','";
                sOnchange += this.divConCuentaYCajaDestino.ClientID + "');";
                this.drpdlGrupoCatalogos.Attributes.Add("onChange", sOnchange);

                sOnchange = "showHideDivTipoDeCambio('";
                sOnchange += this.pnlTipoDeCambio.ClientID + "','" + this.drpdlCuenta.ClientID;
                sOnchange += "','" + this.ddlCuentaDestino.ClientID + "');";
                this.ddlCuentaDestino.Attributes.Add("onChange", sOnchange);


                sOnchange = "showDivOnListContains(";
                sOnchange += /*this.ddlConcepto.UniqueID*/"this" + ",'CAJA CHICA','";
                sOnchange += this.divCajaDestino.ClientID + "');";
                //this.drpdlCatalogoInterno.Attributes.Add("onChange", sOnchange);

                sOnchange += "showDivOnListNOTContains(";
                sOnchange += /*this.ddlConcepto.UniqueID*/"this" + ",'CAJA CHICA','";
                sOnchange += this.divCuentaDestino.ClientID + "')";
                this.drpdlCatalogoInterno.Attributes.Add("onChange", sOnchange);
               

                sOnchange = "subTextBoxes('";
                sOnchange += this.txtNewPesoEntrada.ClientID + "','";
                sOnchange += this.txtNewPesoSalida.ClientID + "','";
                sOnchange += this.txtPesoNetoNewBoleta.ClientID + "')";
                this.txtNewPesoEntrada.Attributes.Add("onKeyUp", sOnchange);
                this.txtNewPesoEntrada.Attributes.Add("onBlur", sOnchange);
                this.txtNewPesoSalida.Attributes.Add("onKeyUp", sOnchange);
                this.txtNewPesoSalida.Attributes.Add("onBlur", sOnchange);

                sOnchange = "CopyTextBoxValue('";
                sOnchange += this.txtChequeNombre.ClientID + "','";
                sOnchange += this.txtNombre.ClientID + "')";
                this.txtChequeNombre.Attributes.Add("onKeyUp", sOnchange);
                this.txtChequeNombre.Attributes.Add("onBlur", sOnchange);

                //por si el numero de cheque es Not Null, seleccionar de concepto CHEQUE
                this.ddlConcepto.DataBind();
              //  int indexOfDDLConceptoCheque = this.sacaidCheque();            
                sOnchange = "iftextboxNotNullChangeDDl('";
                sOnchange += this.txtChequeNum.ClientID + "','";
                sOnchange += this.ddlConcepto.ClientID + "', '"+this.cmbTipodeMov.ClientID+"', 'CHEQUE')";

                this.txtChequeNum.Attributes.Add("onChange", sOnchange);
                this.cmbTipodeMov.DataBind();
                this.ddlTipoAnticipo.DataBind();
                this.drpdlCatalogoInterno.DataBind();
                this.drpdlGrupoCatalogos.DataBind();
                this.drpdlSubcatologointerna.DataBind();
                this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.txtFechaLimite.Text = this.txtFecha.Text;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDEBANCO, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "VISITÓ LA PÁGINA LISTA DE MOVIMIENTOS DE CUENTAS DE BANCOS");
                DateTime dtHoy = Utils.Now;
                this.drddlAnio.DataBind();
                this.drpdlMes.DataBind();
               
                this.drpdlBancos.DataBind();
                this.drpdlBancos.SelectedIndex = 0;

                //this.SqlDataSource1.SelectParameters.Add("@bancoID", System.TypeCode.Int32, this.drpdlBancos.SelectedValue);
                //this.SqlDataSource1.DataBind();
                this.drpdltitular.DataBind();
                this.drpdltitular.SelectedIndex = 0;
                //this.sdsCuentas.SelectParameters.Add("@bancoID", System.TypeCode.Int32, this.drpdlBancos.SelectedValue);
                //this.sdsCuentas.SelectParameters.Add("@Titular", System.TypeCode.String, this.drpdltitular.SelectedValue);
                this.drpdlCuenta.DataBind();
                this.drpdlCuenta.SelectedIndex = 0;
                
                this.generacadenaparamissingcheques(int.Parse(this.drpdlCuenta.SelectedValue.ToString()));
                this.drpdlMes.SelectedValue = dtHoy.Month.ToString();
                this.drddlAnio.SelectedValue = dtHoy.Year.ToString();
                this.showHideColumns();
                this.reloadGridView();                
                
                
                this.ddlNewQuickBoletaBodega.DataBind();
                this.ddlNewQuickBoletaProducto.DataBind();
                this.ddlCicloQuickBoleta.DataBind();
                this.cmbTipodeMov.SelectedIndex = 1;
                this.cmbTipodeMov.Enabled = false;

            }
            //this.panelAgregar.Attributes.Add("style", this.chkMostrarAddMov.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divConceptosFiscales.Attributes.Add("style", this.chkMostrarFiscales.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");

            this.divAssignCredito.Attributes.Add("style", this.chkAssignToCredit.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            //this.divCheque.Attributes.Add("style", this.ddlConcepto.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            //this.panelAgregar.Attributes.Add("style", this.btnShowHideAgregar.Text != "MOSTRAR AGREGAR MOVIMIENTO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divanticipo.Attributes.Add("style", this.chkboxAnticipo.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            //this.gridSaldosMensuales.DataSourceID = "sdsSaldosMensuales";
            //this.gridBoletasSistema.DataSourceID = this.gridBoletasSistema.DataSourceID;
            if (this.ddlTipoAnticipo.SelectedItem == null)
            {
                this.divPrestamo.Attributes.Add("style", "visibility: hidden; display: none");
            }
            else
            {
                this.divPrestamo.Attributes.Add("style", this.ddlTipoAnticipo.SelectedItem.Text == "PRESTAMO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            }
            
            this.divNewBoleta.Attributes.Add("style", this.chkNewBoleta.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            
            
            this.divFechaSalidaNewBoleta.Attributes.Add("style", this.chkChangeFechaSalidaNewBoleta.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");

            this.divConCuentaYCajaDestino.Attributes.Add("style", (this.drpdlGrupoCatalogos.SelectedItem != null && this.drpdlGrupoCatalogos.SelectedItem.Text.IndexOf("TRASPASOS") > -1) ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divCajaDestino.Attributes.Add("style", (this.drpdlCatalogoInterno.SelectedItem != null && this.drpdlCatalogoInterno.SelectedItem.Text.IndexOf("CAJA CHICA") > -1) ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divCuentaDestino.Attributes.Add("style", (this.drpdlCatalogoInterno.SelectedItem != null && this.drpdlCatalogoInterno.SelectedItem.Text.IndexOf("CAJA CHICA") <= -1) ? "visibility: visible; display: block" : "visibility: hidden; display: none");

            if (this.drpdlCuenta.SelectedItem != null && this.ddlCuentaDestino.SelectedItem != null)
            {
                this.pnlTipoDeCambio.Attributes.Add("style", (
                    (this.drpdlCuenta.SelectedItem.Text.IndexOf("PESOS") > -1 && 
                    this.ddlCuentaDestino.SelectedItem.Text.IndexOf("DOLARES") > -1) ||
                    (this.drpdlCuenta.SelectedItem.Text.IndexOf("DOLARES") > -1 &&
                    this.ddlCuentaDestino.SelectedItem.Text.IndexOf("PESOS") > -1) 
                    ? "visibility: visible; display: block" : "visibility: hidden; display: none"));
            }
            

            this.panelNewBoletaResult.Visible = false;
            this.panelNewMovimientodeBanco.Visible = false;
            this.HyperLink2.Visible = false;
            
                this.LoadSaldos();
           
            this.btnImprimeReporte.Visible = false;
            this.btnGeneraReporte.Visible = true;

            this.compruebasecurityLevel();


            //get cheques no conciliados de meses anteriores
            DateTime dtCheques = DateTime.Parse(this.drddlAnio.SelectedValue + "/" + this.drpdlMes.SelectedValue + "/01 00:00:00");
            this.txtFechaCheqNoConc.Visible = true;
            this.txtFechaCheqNoConc.Text = dtCheques.ToString("yyyy/MM/dd 00:00:00");
            this.gvChequesNoConciliados.DataBind();
            this.txtFechaCheqNoConc.Visible = false;
            this.pnlChequesAnteriores.Visible = (this.gvChequesNoConciliados.Rows.Count > 0);

            this.lblTraerChequesNoConciliados.Text = "";

            this.pnlResPeriodoBloq.Visible = false;
        }
        

        protected void gridMovCuentasBanco_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.panelAgregar.Visible = false;
                this.chkMostrarAddMov.Visible = false;
                this.gridMovCuentasBanco.Columns[0].Visible = false;
                this.btnMissingCheques.Visible = false;
                this.btnActualizaChequeStatus.Visible = false;
                this.btnIrAconciliación.Visible = false;
                this.gridMovCuentasBanco.Columns[0].Visible = false;
                this.pnlChequesAnteriores.Visible = false;
            }

        }

        protected void btnAgregar_Click1(object sender, EventArgs e)
        {
            int cheque=0;
            bool hayerrorenmonto=false;
            double monto=0;
            double.TryParse(this.txtMonto.Text, out monto);

            if(monto==0)
            {
                if (this.drpdlCatalogoInterno.SelectedItem.Text != "10J -  CHEQUES CANCELADOS")
                {
                    hayerrorenmonto = true;
                }
            }

            int ChequeNumber = -1;
            if (this.ddlConcepto.SelectedItem.Text.ToUpper().Equals("CHEQUE") && (this.txtChequeNum.Text.Length <= 0 || !int.TryParse(this.txtChequeNum.Text, out ChequeNumber)))
            {
                this.panelNewMovimientodeBanco.Visible = true;
                this.imgNewTacheBanco.Visible = true;
                this.imgNewPalomitaBanco.Visible = false;
                this.lblMensajeNewMovBanco.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO";
                this.lblMsgResultAddMovBanco.Text = "EL NUMERO DE CHEQUE NO ES VALIDO";
                return;
            }
            

            if (this.txtChequeNum.Text.Length >0 && int.Parse(this.txtChequeNum.Text) > 0 && !numChequeValido(int.Parse(this.txtChequeNum.Text), int.Parse(drpdlCuenta.SelectedValue)))
            {
                this.panelNewMovimientodeBanco.Visible = true;
                this.imgNewTacheBanco.Visible = true;
                this.imgNewPalomitaBanco.Visible = false;
                this.lblMensajeNewMovBanco.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO";
                this.lblMsgResultAddMovBanco.Text = "EL NUMERO DE CHEQUE, NO CORRESPONDE A LOS TIRAJES CONFIGURADOS PARA LA CUENTA <BR />CONFIGURE LOS TIRAJES EN LA PAGINA DE LA CUENTA DE BANCO";
                return;
            }
            if (dbFunctions.FechaEnPeriodoBloqueado(DateTime.Parse(this.txtFecha.Text), int.Parse(this.drpdlCuenta.SelectedValue)))
            {
                this.panelNewMovimientodeBanco.Visible = true;
                this.imgNewTacheBanco.Visible = true;
                this.imgNewPalomitaBanco.Visible = false;
                this.lblMensajeNewMovBanco.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO";
                this.lblMsgResultAddMovBanco.Text = "EL MOVIMIENTO NO PUEDE SER AGREGADO YA QUE LA FECHA ESTA DENTRO DE UN PERIODO BLOQUEADO<BR />DESBLOQUEE EL PERIODO PARA PERMITIR INGRESAR EL MOVIMIENTO";
                return;
            }
            if(hayerrorenmonto)
            {
                this.panelNewMovimientodeBanco.Visible = true;
                this.imgNewTacheBanco.Visible = true;
                this.imgNewPalomitaBanco.Visible = false;
                this.lblMensajeNewMovBanco.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO";
                this.lblMsgResultAddMovBanco.Text = "ERROR EN EL MONTO, ESCRIBA CANTIDAD VALIDA";
                return;
            }
            if (int.TryParse(this.txtChequeNum.Text, out cheque))
            {
                if (dbFunctions.ChequeAlreadyExists(cheque, this.drpdlCuenta.SelectedValue))
                {
                    this.panelNewMovimientodeBanco.Visible = true;
                    this.imgNewTacheBanco.Visible = true;
                    this.imgNewPalomitaBanco.Visible = false;
                    this.lblMensajeNewMovBanco.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO";
                    this.lblMsgResultAddMovBanco.Text = "YA EXISTE UN CHEQUE CON ESE NÚMERO";
                    return;

                }
            }
            this.cmbTipodeMov.DataBind();
            dsMovBanco.dtMovBancoDataTable tablaaux = new dsMovBanco.dtMovBancoDataTable();
            dsMovBanco.dtMovBancoRow dtRowainsertar = tablaaux.NewdtMovBancoRow();
            dtRowainsertar.conceptoID = int.Parse(this.ddlConcepto.SelectedValue);
            dtRowainsertar.nombre = this.txtNombre.Text;
            dtRowainsertar.chequecobrado = false;
            dtRowainsertar.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFecha.Text));
            if (this.chkFechaChequeCobrado.Checked)
            {
                dtRowainsertar.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaChequeCobrado.Text));
                dtRowainsertar.fechachequedecobro = dtRowainsertar.fecha;
                dtRowainsertar.chequecobrado = true;
            }

            if (dbFunctions.FechaEnPeriodoBloqueado(dtRowainsertar.fecha, int.Parse(this.drpdlCuenta.SelectedValue)))
            {
                this.panelNewMovimientodeBanco.Visible = true;
                this.imgNewTacheBanco.Visible = true;
                this.imgNewPalomitaBanco.Visible = false;
                this.lblMensajeNewMovBanco.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO";
                this.lblMsgResultAddMovBanco.Text = "LA FECHA DEL MOVIMIENTO CONUERDA CON UN PERIODO EL CUAL ESTA BLOQUEADO PARA MODIFICACIONES.";
                return;
            }

            //dats de cheque
            dtRowainsertar.numCheque = this.txtChequeNum.Text.Length > 0 ? int.Parse(this.txtChequeNum.Text): 0;
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
                
                dtRowainsertar.cargo = this.txtMonto.Text.Length>0 ? double.Parse(this.txtMonto.Text) : 0;
                dtRowainsertar.abono = 0.00;

            }
            else
            {//ES ABONO
                dtRowainsertar.abono = this.txtMonto.Text.Length > 0 ? double.Parse(this.txtMonto.Text) : 0;
                dtRowainsertar.cargo = 0.00;
            }
            dtRowainsertar.storeTS = DateTime.Parse(Utils.getNowFormattedDate());
            dtRowainsertar.updateTS = DateTime.Parse(Utils.getNowFormattedDate());

            if (this.chkAssignToCredit.Checked)
            {
                dtRowainsertar.creditoFinancieroID = int.Parse(this.ddlCreditoFinanciero.SelectedValue);
            }
           

            String serror = "", tipo = "";
            //bool bTodobien = true;
            tipo = this.cmbTipodeMov.Text;
            dtRowainsertar.cuentaID = int.Parse(this.drpdlCuenta.SelectedValue);
            if (dbFunctions.insertMovementdeBanco(ref dtRowainsertar, ref serror, int.Parse(this.Session["USERID"].ToString()), int.Parse(this.drpdlCuenta.SelectedValue), this.chkboxAnticipo.Checked, int.Parse(this.drpdlProductorAnticipo.SelectedValue), ref listBoxAgregadas, int.Parse(this.ddlTipoAnticipo.SelectedValue), this.txtInteresAnual.Text.Length > 0 ? double.Parse(this.txtInteresAnual.Text) : 0f, this.txtInteresmoratorio.Text.Length > 0 ? double.Parse(this.txtInteresmoratorio.Text) : 0f,DateTime.Parse(Utils.converttoLongDBFormat(this.txtInteresAnual.Text)),int.Parse(this.ddlCiclo.SelectedValue),this.drpdlProductorAnticipo.SelectedIndex>0? this.drpdlProductorAnticipo.SelectedItem.Text : ""))
            {
                if(this.chkboxAnticipoGanado.Checked)
                {
                    try
                    {
                        SqlCommand commAnticipoGanado = new SqlCommand();
                        commAnticipoGanado.CommandText = "INSERT INTO Anticipos_FacturasGanado "+
                            " (movBanID,ganProveedorID) VALUES (@movBanID,@ganProveedorID);";
                        commAnticipoGanado.Parameters.Add("@movBanID", SqlDbType.Int).Value = dtRowainsertar.movBanID;
                        commAnticipoGanado.Parameters.Add("@ganProveedorID", SqlDbType.Int).Value = this.ddlProveedorGanado.SelectedValue;
                        dbFunctions.ExecuteCommand(commAnticipoGanado);
                        if (commAnticipoGanado.Connection != null)
                        {
                            commAnticipoGanado.Connection.Close();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.INSERT, "insertando anticipo a ganado", ref ex);
                    }
                }
                if (this.drpdlCatalogoInterno.SelectedItem != null && this.drpdlGrupoCatalogos.SelectedItem.Text.IndexOf("TRASPASOS") > -1 )
                {
                    //si es un traspaso entonces verificar el destino
                    if(this.drpdlCatalogoInterno.SelectedItem != null && this.drpdlCatalogoInterno.SelectedItem.Text.IndexOf("CAJA CHICA") > -1) 
                    {
                        //es caja chica
                        dsMovCajaChica.dtMovCajaChicaRow rowCajaChica = new dsMovCajaChica.dtMovCajaChicaDataTable().NewdtMovCajaChicaRow();
                        rowCajaChica.nombre = dtRowainsertar.nombre;
                        rowCajaChica.fecha = dtRowainsertar.fecha;
                        if (dtRowainsertar.cargo > 0)
                        {
                            rowCajaChica.abono = dtRowainsertar.cargo;
                            rowCajaChica.cargo = dtRowainsertar.abono;
                        }
                        else
                        {
                            rowCajaChica.cargo = dtRowainsertar.abono;
                            rowCajaChica.abono = dtRowainsertar.cargo;
                        }
                        rowCajaChica.Observaciones = "TRASPASO DE UNA CUENTA DE BANCO";
                        rowCajaChica.catalogoMovBancoInternoID = dtRowainsertar.catalogoMovBancoInternoID;
                        rowCajaChica.subCatalogoMovBancoInternoID = dtRowainsertar.IssubCatalogoMovBancoInternoIDNull()? -1: dtRowainsertar.subCatalogoMovBancoInternoID;
                        rowCajaChica.numCabezas = 0;
                        rowCajaChica.facturaOlarguillo = "";
                        rowCajaChica.bodegaID = int.Parse(this.ddlCajaDestino.SelectedValue);

                        String serrorInt = "";
                        ListBox tempLB = new ListBox();
                        if (dbFunctions.insertMovCajaChica(ref rowCajaChica, ref serrorInt, this.UserID, int.Parse(this.ddlCicloDestino.SelectedValue), false, -1, ref tempLB, -1, 0f, 0f, Utils.Now,""))
                        {
                            //if the mov destino were successfully added then add the relation with the mov origen
                            SqlConnection connOrigen = new SqlConnection(myConfig.ConnectionInfo);
                            SqlCommand commOrigen = new SqlCommand();
                            commOrigen.CommandText = "INSERT INTO MOVIMIENTOORIGEN(MOVBANID) VALUES(@MOVBANID);select MOVBANID = SCOPE_IDENTITY();";
                            try
                            {
                                commOrigen.Connection = connOrigen;
                                connOrigen.Open();
                                commOrigen.Parameters.Add("MOVBANID", SqlDbType.Int).Value = dtRowainsertar.movBanID;
                                int movorigenid = int.Parse(commOrigen.ExecuteScalar().ToString());
                                commOrigen.CommandText = "update movimientosCaja set movOrigenID = @movOrigenID where movimientoID = @movimientoID";
                                commOrigen.Parameters.Clear();
                                commOrigen.Parameters.Add("movOrigenID",SqlDbType.Int).Value = movorigenid;
                                commOrigen.Parameters.Add("movimientoID",SqlDbType.Int).Value = rowCajaChica.movimientoID;
                                commOrigen.ExecuteNonQuery();
                            }
                            catch (System.Exception ex)
                            {
                            	Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID,"Error en mov de origen :" + ex.Message + " stack: " + Environment.StackTrace, this.Request.Url.ToString());
                            }
                            finally
                            {
                                connOrigen.Close();
                            }
                        }
                    }
                    else
                    {
                        //es a otra cuenta de banco
                        dsMovBanco.dtMovBancoRow rowBanco = new dsMovBanco.dtMovBancoDataTable().NewdtMovBancoRow();
                        rowBanco.conceptoID = dtRowainsertar.conceptoID;
                        rowBanco.nombre = dtRowainsertar.nombre;
                        rowBanco.fecha = dtRowainsertar.fecha;
                        //dats de cheque
                        rowBanco.numCheque = 0;
                        rowBanco.chequeNombre = "";
                        rowBanco.facturaOlarguillo = "";
                        rowBanco.numCabezas = 0;

                        rowBanco.catalogoMovBancoInternoID =  dtRowainsertar.catalogoMovBancoInternoID;
                        rowBanco.subCatalogoMovBancoInternoID = dtRowainsertar.IssubCatalogoMovBancoInternoIDNull()? -1:dtRowainsertar.subCatalogoMovBancoInternoID;

                        rowBanco.catalogoMovBancoFiscalID = dtRowainsertar.catalogoMovBancoFiscalID;
                        rowBanco.subCatalogoMovBancoFiscalID = dtRowainsertar.IssubCatalogoMovBancoFiscalIDNull() ? -1 : dtRowainsertar.subCatalogoMovBancoFiscalID;
                        
//                         if (this.txtTipodeCambio.Text.Trim().Length <= 0)
//                         {
//                             this.txtTipodeCambio.Text = "1";
//                         }
                        if (this.drpdlCuenta.SelectedItem.Text.IndexOf("PESOS") > -1 &&
                            this.ddlCuentaDestino.SelectedItem.Text.IndexOf("DOLARES") > -1)
                        {
                            double dTipoCambio = 0;
                            if (double.TryParse(this.txtTipodeCambio.Text, out dTipoCambio) && dTipoCambio > 0)
                            {
                                dTipoCambio = 1.0 / dTipoCambio;
                                this.txtTipodeCambio.Text = dTipoCambio.ToString(); // string.Format("{0:n2}", dTipoCambio);
                            }
                            else
                            {
                                this.txtTipodeCambio.Text = "1";
                            }
                        }

                        if (dtRowainsertar.cargo > 0)
                        {
                            double tipoCambio = 1;
                            double.TryParse(this.txtTipodeCambio.Text, out tipoCambio);
                            if (tipoCambio == 0)
                            {
                                tipoCambio = 1;
                            }
                            rowBanco.abono = dtRowainsertar.cargo * tipoCambio;
                            rowBanco.cargo = 0;
                        }
                        else
                        {
                            double tipoCambio = 1;
                            double.TryParse(this.txtTipodeCambio.Text, out tipoCambio);
                            if (tipoCambio == 0)
                            {
                                tipoCambio = 1;
                            }
                            rowBanco.cargo = dtRowainsertar.abono*tipoCambio;
                            rowBanco.abono = dtRowainsertar.cargo*tipoCambio;
                        }
                        
                        String serrorBanco = "";
                        rowBanco.cuentaID =  int.Parse(this.ddlCuentaDestino.SelectedValue);
                        ListBox tempLB = new ListBox();
                        if (dbFunctions.insertMovementdeBanco(ref rowBanco, ref serrorBanco, this.UserID, rowBanco.cuentaID, false, -1, ref tempLB, -1, 0f, 0f, Utils.Now, -1,""))
                        {
                            //if the mov destino were successfully added then add the relation with the mov origen
                            SqlConnection connOrigen = new SqlConnection(myConfig.ConnectionInfo);
                            SqlCommand commOrigen = new SqlCommand();
                            commOrigen.CommandText = "INSERT INTO MOVIMIENTOORIGEN(MOVBANID, TIPODECAMBIO) VALUES(@MOVBANID, @TIPODECAMBIO);select MOVBANID = SCOPE_IDENTITY();";
                            try
                            {
                                float tipodeCambio = 1;
                                float.TryParse(this.txtTipodeCambio.Text, out tipodeCambio);
                                commOrigen.Connection = connOrigen;
                                connOrigen.Open();
                                commOrigen.Parameters.Add("MOVBANID", SqlDbType.Int).Value = dtRowainsertar.movBanID;
                                commOrigen.Parameters.Add("TIPODECAMBIO", SqlDbType.Float).Value = tipodeCambio;
                                int movorigenid = int.Parse(commOrigen.ExecuteScalar().ToString());
                                commOrigen.CommandText = "DISABLE TRIGGER MOVIMIENTOSCUENTASBANCO_UPDATE on MovimientosCuentasBanco;";
                                commOrigen.CommandText += " update movimientosCuentasBanco set movOrigenID = @movOrigenID where movbanID = @movbanID;";
                                commOrigen.CommandText += " ENABLE TRIGGER MOVIMIENTOSCUENTASBANCO_UPDATE on MovimientosCuentasBanco;";
                                commOrigen.Parameters.Clear();
                                commOrigen.Parameters.Add("movOrigenID", SqlDbType.Int).Value = movorigenid;
                                commOrigen.Parameters.Add("movbanID", SqlDbType.Int).Value = rowBanco.movBanID;
                                commOrigen.ExecuteNonQuery();
                            }
                            catch (System.Exception ex)
                            {
                                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "Error en mov de origen :" + ex.Message, this.Request.Url.ToString());
                                this.panelNewMovimientodeBanco.Visible = true;
                                this.imgNewTacheBanco.Visible = true;
                                this.imgNewPalomitaBanco.Visible = false;
                                this.lblMensajeNewMovBanco.Text = "ERROR";
                                this.lblMsgResultAddMovBanco.Text = ex.Message ;
                            }
                            finally
                            {
                                connOrigen.Close();
                            }
                        }
                    }


                }
                limpiacampos();
                //this.lbAgregar.Text = "Mostrar 'Agregar Movimiento'";
                //this.panelAgregar.Visible = false;
                this.reloadGridView();
                //this.btnShowHideAgregar_Click(null, null);
                this.chkMostrarAddMov.Checked = false;
                this.chkboxAnticipo.Checked = false;
                this.drpdlProductorAnticipo.SelectedIndex = 0;
                this.listBoxAgregadas.Items.Clear();
                this.divanticipo.Attributes.Add("style", this.chkboxAnticipo.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            
                this.chkboxAnticipo.Checked = false;
                this.txtTicket.Text = "";
                this.chkFechaChequeCobrado.Checked = false;
                this.txtFechaChequeCobrado.Text = Utils.Now.ToString("dd/MM/yyyy");
                if(dtRowainsertar.conceptoID ==3)
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
                    HyperLink2.Attributes.Add("onClick", parameter);
                    HyperLink2.NavigateUrl = this.Request.Url.ToString();
                    HyperLink2.Visible = true;
                    HyperLink2.Text = "IMPRIMIR CHEQUE";

                    this.panelNewMovimientodeBanco.Visible = true;
                    this.imgNewTacheBanco.Visible = false;
                    this.imgNewPalomitaBanco.Visible = true;
                    this.lblMensajeNewMovBanco.Text = "ÉXITO";
                    this.lblMsgResultAddMovBanco.Text = "SE AGREGÓ EL MOVIMIENTO EXITOSAMENTE";

                }
                //this.panelAgregar.Attributes.Add("style", this.chkMostrarAddMov.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            }
            else
            {
                this.panelNewMovimientodeBanco.Visible = true;
                this.imgNewTacheBanco.Visible = true;
                this.imgNewPalomitaBanco.Visible = false;
                this.lblMensajeNewMovBanco.Text = "ERROR";
                this.lblMsgResultAddMovBanco.Text = serror;

            }
        }
        protected void limpiacampos()
        {
          //  this.cmbCuenta.SelectedIndex = 0;
            this.txtMonto.Text = "0.00";
            this.txtFacturaLarguillo.Text = "";
            this.txtNombre.Text = "";
            this.txtNumCabezas.Text = "";
            this.txtChequeNombre.Text = "";
            if(!this.chkNoResetControls.Checked)
            {
                this.cmbTipodeMov.SelectedIndex = 0;
                this.txtTipodeCambio.Text = "1";
                this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.ddlConcepto.DataBind();
                this.cmbTipodeMov.DataBind();
                this.ddlTipoAnticipo.DataBind();
                this.drpdlCatalogoInterno.DataBind();
                this.drpdlGrupoCatalogos.DataBind();
                this.drpdlSubcatologointerna.DataBind();
                this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.txtFechaLimite.Text = this.txtFecha.Text;
                this.ddlNewQuickBoletaBodega.DataBind();
                this.ddlNewQuickBoletaProducto.DataBind();
                this.ddlCicloQuickBoleta.DataBind();
                this.cmbTipodeMov.SelectedIndex = 1;
                this.cmbTipodeMov.Enabled = false;
                this.drpdlGrupoCatalogos.DataBind();
                this.drpdlGrupoCatalogos.SelectedIndex = 0;
                this.divConCuentaYCajaDestino.Attributes.Add("style", (this.drpdlGrupoCatalogos.SelectedItem != null && this.drpdlGrupoCatalogos.SelectedItem.Text.IndexOf("TRASPASOS") > -1) ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            }
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
            this.drpdlSubcatologointerna.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        /*
        protected void lbAgregar_Click(object sender, EventArgs e)
                {
                    if(this.panelAgregar.Visible){
                        this.lbAgregar.Text = "Mostrar 'Agregar Movimiento'";
                        this.panelAgregar.Visible = false;
        
                    }
                    else{
                        this.lbAgregar.Text = "Ocultar 'Agregar Movimiento'";
                        this.panelAgregar.Visible = true;
        
                    }
                }*/
        

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            this.showHideColumns();
            this.reloadGridView();
        }
        private void showHideColumns()
        {
            foreach (DataControlField col in this.gridMovCuentasBanco.Columns)
            {
                ListItem item = this.cblColToShow.Items.FindByText(col.HeaderText);
                if (item != null)
                {
                    col.Visible = item.Selected;
                }
            }
        }

        protected void gridMovCuentasBanco_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gridMovCuentasBanco.EditIndex = -1;

            this.reloadGridView();

        }

        protected void gridMovCuentasBanco_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                this.gridMovCuentasBanco.EditIndex = e.NewEditIndex;
                this.reloadGridView();
                TextBox txtFecha = ((TextBox)(this.gridMovCuentasBanco.Rows[e.NewEditIndex].Cells[2].FindControl("txtFecha")));
                txtFecha.Text = Utils.converttoshortFormatfromdbFormat(txtFecha.Text);
                if( ((CheckBox)(this.gridMovCuentasBanco.Rows[e.NewEditIndex].Cells[16].FindControl("chkChequeCobrado"))).Checked){
                    ((TextBox)(this.gridMovCuentasBanco.Rows[e.NewEditIndex].Cells[13].FindControl("txtCargo"))).Enabled=false;
                    ((TextBox)(this.gridMovCuentasBanco.Rows[e.NewEditIndex].Cells[14].FindControl("txtAbono"))).Enabled = false;
                    
                }
                if (this.gridMovCuentasBanco.DataKeys[e.NewEditIndex]["concepto"].ToString() != "CHEQUE")
                {
                    ((TextBox)(this.gridMovCuentasBanco.Rows[e.NewEditIndex].Cells[12].FindControl("txtNumCheque"))).Text = "0";
                }
                DropDownList drpConcepto = ((DropDownList)(this.gridMovCuentasBanco.Rows[e.NewEditIndex].Cells[6].FindControl("drpdlConceptosgrid")));
                if (drpConcepto != null)
                {
                    drpConcepto.SelectedValue = this.gridMovCuentasBanco.DataKeys[e.NewEditIndex]["conceptoID"].ToString();
                }
                DropDownList drpCatalogoFiscal = ((DropDownList)(this.gridMovCuentasBanco.Rows[e.NewEditIndex].Cells[6].FindControl("drpdlCatalogoFiscalgrid")));
                if (drpCatalogoFiscal != null)
                {
                    drpCatalogoFiscal.SelectedValue = this.gridMovCuentasBanco.DataKeys[e.NewEditIndex]["catalogoMovBancoFiscalID"].ToString();
                }
                DropDownList drpSubCatalogoFiscal = ((DropDownList)(this.gridMovCuentasBanco.Rows[e.NewEditIndex].Cells[6].FindControl("drpSubCatalogoFiscalgrid")));

                if (drpSubCatalogoFiscal != null)
                {
                    /* if (this.gridMovCuentasBanco.DataKeys[e.NewEditIndex]["subCatalogoMovBancoInternoID"].ToString() == "-1")
                     {
                         this.gridMovCuentasBanco.SelectedIndex = -1;
                     }
                     else
                     {*/
                    drpSubCatalogoFiscal.SelectedValue = this.gridMovCuentasBanco.DataKeys[e.NewEditIndex]["subCatalogoMovBancoFiscalID"].ToString();
                    //}
                }
                DropDownList drpCatalogoInterno = ((DropDownList)(this.gridMovCuentasBanco.Rows[e.NewEditIndex].Cells[6].FindControl("drpdlCatalogoInternogrid")));
                if (drpCatalogoInterno != null)
                {
                    drpCatalogoInterno.SelectedValue = this.gridMovCuentasBanco.DataKeys[e.NewEditIndex]["catalogoMovBancoInternoID"].ToString();
                }
                DropDownList drpSubCatalogoInterno = ((DropDownList)(this.gridMovCuentasBanco.Rows[e.NewEditIndex].Cells[6].FindControl("drpdlSubcatalogoInternogrid")));
                if (drpSubCatalogoInterno != null)
                {
                    drpSubCatalogoInterno.SelectedValue = this.gridMovCuentasBanco.DataKeys[e.NewEditIndex]["subCatalogoMovBancoInternoID"].ToString();
                 }
            }
            catch(Exception ex){
               Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["userID"].ToString()), "EL ERROR SE DIO CUANDO SE TRATABA DE EDITAR EL MOV DE BANCO. LA EXC FUE: " + ex.Message,this.Request.Url.ToString());
          
            }

        }

        protected void gridMovCuentasBanco_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                dsMovBanco.dtMovBancoDataTable dt = new dsMovBanco.dtMovBancoDataTable();
                dsMovBanco.dtMovBancoRow row = dt.NewdtMovBancoRow();
                
                   row.cargo= ((TextBox)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[13].FindControl("txtCargo"))).Text.Length > 0 ? double.Parse(((TextBox)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[13].FindControl("txtCargo"))).Text) : 0;
                   row.abono = ((TextBox)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[14].FindControl("txtAbono"))).Text.Length > 0 ? double.Parse(((TextBox)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[14].FindControl("txtAbono"))).Text) : 0;
//                 row.cargo = ((TextBox)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[13].Controls[0])).Text.Length > 0 ? double.Parse(((TextBox)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[13].Controls[0])).Text) : 0;
//                 row.abono = ((TextBox)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[14].Controls[0])).Text.Length > 0 ? double.Parse(((TextBox)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[14].Controls[0])).Text) : 0;
                
                row.catalogoMovBancoFiscalID = int.Parse(((DropDownList)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[7].FindControl("drpdlCatalogoFiscalgrid"))).SelectedValue);
                row.catalogoMovBancoInternoID = int.Parse(((DropDownList)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[10].FindControl("drpdlCatalogoInternogrid"))).SelectedValue);
                row.conceptoID = int.Parse(((DropDownList)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[6].FindControl("drpdlConceptosgrid"))).SelectedValue);
                row.chequecobrado = (((CheckBox)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[16].FindControl("chkChequeCobrado"))).Checked);
                row.chequeNombre = (((TextBox)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[9].Controls[0])).Text);
              //  row.chequecobrado = ((CheckBox)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[16].FindControl("chkChequeCobrado"))).Checked;
                row.cuentaID = int.Parse(this.drpdlCuenta.SelectedValue);
                row.facturaOlarguillo = (((TextBox)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[4].Controls[0])).Text);
                row.fecha = DateTime.Parse(Utils.converttoLongDBFormat((((TextBox)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[2].FindControl("txtFecha"))).Text)));
                row.movBanID = int.Parse(((GridView)(sender)).DataKeys[e.RowIndex]["movbanID"].ToString());
                row.nombre = (((TextBox)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[3].Controls[0])).Text);
                row.numCabezas = ((TextBox)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[5].Controls[0])).Text.Length > 0 ? double.Parse(((TextBox)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[5].Controls[0])).Text) : 0;
                //SOLO MODIFICAMOS SI ELEGIMOS CHEQUE
                row.numCheque = row.conceptoID == 3 ? int.Parse(((TextBox)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[12].FindControl("txtNumCheque"))).Text) : 0;
                row.subCatalogoMovBancoFiscalID = int.Parse(((DropDownList)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[8].FindControl("drpSubCatalogoFiscalgrid"))).SelectedValue);
                row.subCatalogoMovBancoInternoID = int.Parse(((DropDownList)(this.gridMovCuentasBanco.Rows[e.RowIndex].Cells[11].FindControl("drpdlSubcatalogoInternogrid"))).SelectedValue);
                row.updateTS = Utils.Now;
                row.userID = int.Parse(this.Session["userID"].ToString());
                string sError = "";
                if (!dbFunctions.updateMovementdeBanco(ref row, row.movBanID, ref sError, row.userID, row.cuentaID))
                { //DONE WITH TRIGGERS (UPDATE ORIGENES AND DESTINOS
                    throw new Exception("EXCEPCION AL HACER UPDATE DEL MOVIMIENTO. EX " + sError);  
                }

                this.gridMovCuentasBanco.EditIndex = -1;
                this.reloadGridView();
            }
            catch(Exception ex){
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["userID"].ToString()), "EL ERROR SE DIO CUANDO SE TRATABA DE MODIFICAR EL MOV DE BANCO. LA EXC FUE: " + ex.Message,this.Request.Url.ToString());

            }
           
           
        }

        protected void drpdlCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            String tarjeta = "tdc 4555138407795516";
            string tarjeta2 = "tdc 4555138609409874";
            if(this.drpdlCuenta.SelectedItem.Text.IndexOf(tarjeta)>0 ||
                this.drpdlCuenta.SelectedItem.Text.IndexOf(tarjeta2) > 0)
            {
                DateTime dtInicio = new DateTime(Utils.Now.Year,Utils.Now.Month, 3);
                DateTime dtFin;
                
                
                if(dtInicio.Month==12)
                {
                    dtFin = new DateTime(dtInicio.Year + 1, 1, 2);
                }
                else
                {
                    dtFin = new DateTime(dtInicio.Year, dtInicio.Month+1,2);
                }
                this.txtFechaIncio.Text = dtInicio.ToString("dd/MM/yyyy");
                this.txtFechaFin.Text = dtFin.ToString("dd/MM/yyyy");
                this.chkPorFechas.Checked = true;
                this.chkPorMes.Checked = false;
               
            }
           
            this.reloadGridView();

            this.generacadenaparamissingcheques(int.Parse(this.drpdlCuenta.SelectedValue));
        }

        protected void drpdlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.reloadGridView();
        }
        protected void generacadenaparamissingcheques(int cuentaID)
        {
            String sQuery = "cuentaID=" + cuentaID.ToString();
            sQuery = Utils.GetEncriptedQueryString(sQuery);
            string parameter = "";
            parameter = "javascript:url('";
            parameter += "frmMissingCheques.aspx";
            parameter += sQuery;
            parameter += "', '";
            parameter += "Cheques Faltantes";
            parameter += "',0,200,400,800); return false;";
            this.btnMissingCheques.Attributes.Clear();
            this.btnMissingCheques.Attributes.Add("onclick", parameter);

        }

        protected void drddlAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.reloadGridView();
        }
        protected void sacaTotales()
        {
            double cantidad = 0f, totalencobrados = 0f, totalennocobrados = 0f, totalengirados = 0f;
            double fTotalCargos = 0f, fTotalAbonos = 0f;
            int totalcobrados = 0, totalnocobrados = 0, totalgirados = 0;
             foreach (GridViewRow row in gridMovCuentasBanco.Rows)
             {                
                 if (row.RowType != DataControlRowType.DataRow)
                 {
                     continue;
                 }
                 cantidad = int.Parse(((Label)(row.FindControl("lblNumCheque"))).Text) > 0 ? Utils.GetSafeFloat(((Label)row.Cells[13].FindControl("lblCargo")).Text):0;
                CheckBox aux = ((CheckBox)(row.FindControl("chkChequeCobrado")));
                if (int.Parse(((Label)(row.FindControl("lblNumCheque"))).Text) > 0)
                {
                    if(aux.Checked){
                        totalcobrados++;
                        totalencobrados += cantidad;
                    }
                    else
                    {
                        totalnocobrados++;
                        totalennocobrados += cantidad;
                    }
                    totalengirados += cantidad;
                }
                
                fTotalCargos += Utils.GetSafeFloat(((Label)row.FindControl("lblCargo")).Text);
                fTotalAbonos += Utils.GetSafeFloat(((Label)row.FindControl("lblAbono")).Text);
            }

             this.lblTotalCargos.Text = string.Format("{0:C2}", fTotalCargos);
             this.lblTotalAbonos.Text = string.Format("{0:C2}", fTotalAbonos);
            totalengirados = totalencobrados + totalennocobrados;
            totalgirados = totalnocobrados + totalcobrados;
            this.lblChequesGirados.Text = totalgirados.ToString();
            this.lblChequesNoCobrados.Text = totalnocobrados.ToString();
            this.lblChequesCobrados.Text = totalcobrados.ToString();
            this.lblTotalCobrados.Text = string.Format("{0:c}", totalencobrados);
            this.lblTotalGirados.Text = string.Format("{0:c}", totalengirados);
            this.lblTotalNoCobrados.Text = string.Format("{0:c}", totalennocobrados);
        }

        protected void ddlConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnShowHideAgregar_Click(object sender, EventArgs e)
        {
//             this.btnShowHideAgregar.Text = this.btnShowHideAgregar.Text == "OCULTAR AGREGAR MOVIMIENTO" ? "MOSTRAR AGREGAR MOVIMIENTO" : "OCULTAR AGREGAR MOVIMIENTO";
//             this.panelAgregar.Attributes.Add("style", this.btnShowHideAgregar.Text != "MOSTRAR AGREGAR MOVIMIENTO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
        }

        protected void drpdlSubcatologointerna_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAgregarBoleta_Click(object sender, EventArgs e)
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

        protected void btnQuitarBoleta_Click(object sender, EventArgs e)
        {
            if (this.listBoxAgregadas.SelectedIndex >-1)
            {
                this.listBoxAgregadas.Items.RemoveAt(this.listBoxAgregadas.SelectedIndex);
                
            }
        }
        protected void filtraBoletasDeSistema()
        {
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

        protected void drpdlProductorBoletas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.drpdlProducto.DataBind();
        }

        protected void ddlCiclosAnticipos_SelectedIndexChanged(object sender, EventArgs e)
        {
//             this.drpdlProductorBoletas.DataBind();
//             this.drpdlProducto.DataBind();
        }

        protected void chkboxAnticipo_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void gridMovCuentasBanco_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
              string sError = "";
              int movID = int.Parse(this.gridMovCuentasBanco.DataKeys[e.RowIndex]["movbanID"].ToString());
              dbFunctions.deleteMovementdeBanco(movID, sError, int.Parse(this.Session["USERID"].ToString()), int.Parse(this.drpdlCuenta.SelectedValue));
              this.gridMovCuentasBanco.EditIndex = -1;
              this.reloadGridView();
        }

        protected void btnAgregarBoletadesdeTicket_Click(object sender, EventArgs e)
        {
            if (this.txtTicket.Text.Length > 0)
            {
                if (dbFunctions.BoletaAlreadyExists(-1, "-1", this.txtTicket.Text))
                {
                    return;
                }
                
                if (dbFunctions.insertaBoletaEnBlanco(this.txtTicket.Text, int.Parse(drpdlProductorAnticipo.SelectedValue), this.drpdlProductorAnticipo.SelectedItem.Text, int.Parse(this.ddlCicloQuickBoleta.SelectedValue), int.Parse(this.ddlNewQuickBoletaProducto.SelectedValue), int.Parse(this.ddlNewQuickBoletaBodega.SelectedValue), int.Parse(this.Session["userID"].ToString())))
                {
                    ListItem item = new ListItem(this.txtTicket.Text);
                    this.listBoxAgregadas.Items.Add(item);
                    this.txtTicket.Text = "";
                    this.filtraBoletasDeSistema();
                }
            } 
               //this.gridBoletasSistema.SelectedIndex = -1;
        }

        protected void btnAgregarBol_Click(object sender, EventArgs e)
        {
            if (dbFunctions.BoletaAlreadyExists(-1, this.txtNewNumBoleta.Text, this.txtNewTicket.Text))
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
                addComm.Parameters.Add("@PesoDeEntrada",SqlDbType.Float).Value =  (double)newRow.PesoDeEntrada;
                addComm.Parameters.Add("@FechaSalida", SqlDbType.DateTime).Value = newRow.FechaSalida;
               // addComm.Parameters.AddWithValue("@FechaSalida", newRow.FechaSalida);
                addComm.Parameters.Add("@PesoDeSalida",SqlDbType.Float).Value =  newRow.PesoDeSalida;
                addComm.Parameters.Add("@pesonetoentrada",SqlDbType.Float).Value = newRow.pesonetoentrada;
                addComm.Parameters.Add("@pesonetosalida",SqlDbType.Float).Value = newRow.pesonetosalida;
               // addComm.Parameters.AddWithValue("@productoID", newRow.productoID);
                addComm.Parameters.Add("@productoID", SqlDbType.Int).Value = newRow.productoID;

                addComm.Parameters.Add("@humedad",SqlDbType.Float).Value=  newRow.humedad;
                addComm.Parameters.Add("@impurezas",SqlDbType.Float).Value =  newRow.impurezas;
                addComm.Parameters.Add("@precioapagar",SqlDbType.Float).Value =  newRow.precioapagar;
                addComm.Parameters.Add("@dctoSecado",SqlDbType.Float).Value =  newRow.dctoSecado;
                //addComm.Parameters.AddWithValue("@userID", newRow.userID);
                addComm.Parameters.Add("@userID", SqlDbType.Int).Value = newRow.userID;
                
                newRow.boletaID = int.Parse(addComm.ExecuteScalar().ToString());
                //dtTempBoletas.AdddtBoletasRow(newRow);

                
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
//                 this.drpdlProductorBoletas.DataBind();
//                 this.drpdlProducto.DataBind();
//                 this.gridBoletasSistema.DataBind();

            }
            catch (System.Exception ex)
            {

                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(Session["USERID"].ToString()), "Error Insertando Nueva Boleta EX:" + ex.Message, this.Request.Url.ToString());
                this.lblMsgResult.Text = "OCURRIO UN ERROR AL AGREGAR LA BOLETA. ERROR:" + ex.Message ;
                this.imgNewTache.Visible = true;
                this.imgNewPalomita.Visible = false;
                this.panelNewBoletaResult.Visible = true;
            }
            finally
            {
                sqlConn.Close();
            }
        }

        protected void drpSubCatalogoFiscalgrid_DataBound(object sender, EventArgs e)
        {
            int newValue = -1;
            ((DropDownList)(sender)).Items.Insert(0, new ListItem("", newValue.ToString()));
            //this.drpSubCatalogoFiscalgrid.SelectedIndex = 0;
        }

        protected void drpdlSubcatalogoInternogrid_DataBound(object sender, EventArgs e)
        {
            int newValue = -1;
            ((DropDownList)(sender)).Items.Insert(0, new ListItem("", newValue.ToString()));
        }

        protected void gridMovCuentasBanco_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Button db = (Button)e.Row.FindControl("btnModificar");
                    if (db != null)
                    {
                        string nombre = this.gridMovCuentasBanco.DataKeys[e.Row.RowIndex]["nombre"].ToString();
                        string fecha = DateTime.Parse(this.gridMovCuentasBanco.DataKeys[e.Row.RowIndex]["fecha"].ToString()).ToString("dd/MM/yyyy");
                        JSUtils.AddConfirmToCtrlOnClick(ref db, "Realmente desea modificar el movimiento con nombre: " + nombre +" y fecha: " + fecha +"?", true);
                        //db.OnClientClick = "return confirm('Realmente desea modificar el movimiento?');";
                    }
                    db = (Button)e.Row.FindControl("btnEliminar");
                    if (db != null)
                    {
                        string nombre = this.gridMovCuentasBanco.DataKeys[e.Row.RowIndex]["nombre"].ToString();
                        string fecha = DateTime.Parse(this.gridMovCuentasBanco.DataKeys[e.Row.RowIndex]["fecha"].ToString()).ToString("dd/MM/yyyy");
                        db.OnClientClick = "javascript: return confirm('Realmente desea eliminar el movimiento con nombre " + nombre + " y fecha " + fecha +"?');";
                    }
                }
                catch
                {
                	
                }
                


                CheckBox aux = ((CheckBox)(e.Row.Cells[16].FindControl("chkChequeCobrado")));
                TextBox tb = (TextBox)e.Row.FindControl("txtFechaCobrado");
                aux.Visible = true;     
                if(aux.Checked){
                    try
                    {
                        ((Button)(e.Row.Cells[0].Controls[1])).Visible = false;
                        ((Button)(e.Row.Cells[0].Controls[3])).Visible = false;
                        if (tb != null)
                        {
                            tb.Visible = false;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.SELECT, "error mostrando campos", ref ex);
                    }
                    

                }
                else
                {
                    try
                    {
                        if (tb != null)
                        {
                            tb.Visible = true;
                            double fChequeNum = double.Parse(((Label)e.Row.FindControl("lblNumCheque")).Text);
                            if(this.gridMovCuentasBanco.DataKeys[e.Row.RowIndex]["concepto"].ToString() != "CHEQUE"){
                                ((Label)e.Row.FindControl("lblNumCheque")).Text = "0";
                            }
                            //tb.Visible = (fChequeNum > 0);
                        }
                        //String onclick = "ShowPopupCalendar(this); return false;";
                        //tb.Attributes.Add("Onclick", onclick);
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.SELECT, "error visible calendar fecha cheque", ref ex);
                    }
                }
                
                SqlConnection conSacaTickets = new SqlConnection(myConfig.ConnectionInfo);
                string query = " SELECT     Boletas_Anticipos.Ticket FROM Boletas_Anticipos INNER JOIN Anticipos ON Boletas_Anticipos.anticipoID = Anticipos.anticipoID where Anticipos.movbanID = @movID";

                SqlCommand cmdSacaTickets = new SqlCommand(query, conSacaTickets);
                conSacaTickets.Open();
                SqlDataReader read;
                try
                {
                    //////////////////////////////////////////////////////////////////////////
                    //this code is disabled in order to avoid unneccesary processing.
                    //cmdSacaTickets.Parameters.Clear();
                    //cmdSacaTickets.Parameters.Add("@movID", SqlDbType.Int).Value = int.Parse(this.gridMovCuentasBanco.DataKeys[e.Row.RowIndex]["movbanID"].ToString());

//                     read = cmdSacaTickets.ExecuteReader();
//                     ArrayList listaboletas = new ArrayList();
//                     while (read.Read())
//                     {
//                         listaboletas.Add(read["Ticket"].ToString());
//                     }
//                     string boletas = string.Join(",", (string[])(listaboletas.ToArray(typeof(string))));
//                     e.Row.Cells[27].Text = boletas;
//                     
//                     read.Close();
                    DateTime dtStart = Utils.Now;
                    cmdSacaTickets.CommandText = "SELECT     MovimientosCuentasBanco.movbanID, Bancos.nombre + ' - ' + CuentasDeBanco.NumeroDeCuenta AS Movimiento " 
                                               + " FROM         Bancos INNER JOIN "
                                               + " CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID INNER JOIN "
                                               + " MovimientosCuentasBanco ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco.cuentaID INNER JOIN "
                                               + " MovimientosCuentasBanco AS MovimientosCuentasBanco_1 INNER JOIN "
                                               + " MovimientoOrigen ON MovimientosCuentasBanco_1.movOrigenID = MovimientoOrigen.movOrigenID ON "
                                               + " MovimientosCuentasBanco.movbanID = MovimientoOrigen.movbanID "
                                               + " WHERE     (MovimientosCuentasBanco_1.movbanID = @movBanID)";
                    cmdSacaTickets.Parameters.Clear();
                    cmdSacaTickets.Parameters.Add("@movBanID", SqlDbType.Int).Value = int.Parse(this.gridMovCuentasBanco.DataKeys[e.Row.RowIndex]["movbanID"].ToString());

                    read = cmdSacaTickets.ExecuteReader();
                    if(read.HasRows && read.Read())
                    {
                        HyperLink link = (HyperLink)e.Row.Cells[28].FindControl("HyperLink1");
                        if (link != null)
                        {
                            string parameter, ventanatitle = "DETALLES DE MOVIMIENTO DE CUENTA";
                            // String pathArchivotemp = PdfCreator.printLiquidacion(0, PdfCreator.tamañoPapel.CARTA, PdfCreator.orientacionPapel.VERTICAL, ref this.gvBoletas, ref gvAnticipos, ref gvPagosLiquidacion);
                            string datosaencriptar;
                            datosaencriptar = "id=";
                            datosaencriptar += read["movBanID"].ToString();
                            datosaencriptar += "&";
                            parameter = "javascript:url('";
                            parameter += "frmMovBancoDetails.aspx?data=";
                            parameter += Utils.encriptacadena(datosaencriptar);
                            parameter += "', '";
                            parameter += ventanatitle;
                            parameter += "',0,250,400,800); return false;";
                            link.Attributes.Add("onClick", parameter);
                            link.NavigateUrl = this.Request.Url.ToString();
                            link.Text = read["Movimiento"].ToString();
                        }
                       
                    }
                    else{
                        HyperLink link = (HyperLink)e.Row.Cells[28].FindControl("HyperLink1");
                        if(link!=null) link.Visible = false;

                    }
//                     //Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.SELECT,
//                         this.UserID, "MovOrigen took: " + (Utils.Now - dtStart).TotalSeconds.ToString(),
//                         this.Request.Url.ToString());

                }
                catch (Exception ex)
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDEBANCO, Logger.typeUserActions.SELECT, int.Parse(this.Session["userID"].ToString()), "ERROR AL TRATAR DE MOSTRAS LAS BOLETAS RELACIONADAS CON UN ANTICIPO EN MOV BANCOS. LA EXC FUE: " + ex.Message);

                }
                finally
                {
                    conSacaTickets.Close();
                }
            }
        }

        protected void btnActualizaChequeStatus_Click(object sender, EventArgs e)
        {
            string query = "update MovimientosCuentasBanco set chequecobrado = @cobrado, fecha = @fecha where movbanID = @movID";
            SqlConnection conUpdate = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdUpdate = new SqlCommand(query, conUpdate);
            conUpdate.Open();
            try{
                foreach (GridViewRow row in this.gridMovCuentasBanco.Rows)
                {
                    try
                    {
                        CheckBox aux = ((CheckBox)(row.FindControl("chkChequeCobrado")));

                        double fChequeNum = row.FindControl("lblNumCheque") != null? double.Parse(((Label)row.FindControl("lblNumCheque")).Text) : 0;
                        TextBox tb = (TextBox)row.FindControl("txtFechaCobrado");
                        DateTime dt = new DateTime();
                        cmdUpdate.Parameters.Clear();
                        cmdUpdate.Parameters.Add("@cobrado", SqlDbType.Bit).Value = aux.Checked ? 1 : 0;
                        cmdUpdate.Parameters.Add("@movID", SqlDbType.Int).Value = int.Parse(this.gridMovCuentasBanco.DataKeys[row.RowIndex]["movbanID"].ToString());
                        if (fChequeNum > 0 && aux.Checked && tb != null &&  DateTime.TryParse(tb.Text, out dt)) 
                        {
                            //es cheque
                            query = "update MovimientosCuentasBanco set chequecobrado = @cobrado, fecha = @fecha where movbanID = @movID";
                            cmdUpdate.CommandText = query;
                            cmdUpdate.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Parse(tb.Text);
                        }
                        else
                        {
                            query = "update MovimientosCuentasBanco set chequecobrado = @cobrado where movbanID = @movID";
                            cmdUpdate.CommandText = query;
                        }
                                                
                        cmdUpdate.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "updating conciliacion", ref ex);
                    }

                }

            }
            catch(Exception ex){
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR  ACTUALIZANDO EL ESTADO DE CHEQUES, LA EXC ES: " + ex.Message, this.Request.Url.ToString());
            }
            finally{
                conUpdate.Close();
            }
            this.reloadGridView();
            this.LoadSaldos();
            this.sacaTotales();
        }

        protected void gridMovCuentasBanco_PreRender(object sender, EventArgs e)
        {
            double fValue = 0;
            foreach(GridViewRow row in this.gridMovCuentasBanco.Rows)
            {
                if(row.RowType == DataControlRowType.DataRow || row.RowType == DataControlRowType.Footer)
                {
                    /*for (int i = 0; i < this.gridMovCuentasBanco.Columns.Count ; i++)*/
                    {
                    	try
                    	{
                            fValue = double.Parse(row.Cells[15].Text, NumberStyles.Currency);
                            if (fValue < 0)
                            {
                                row.Cells[15].ForeColor = System.Drawing.Color.Red;
                            }
                    	}
                    	catch {}
                    }
                }
            }
        }

        protected void btnIrAconciliación_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/frmConciliacionBancos.aspx");
        }

        protected void btnGeneraReporte_Click(object sender, EventArgs e)
        {
            try
            {
            
                string sFileName = this.drpdlCuenta.SelectedItem.Text + "_"+ this.UserID.ToString() +".xls";
                sFileName = sFileName.Replace(" ","_");
                String sPath = "";
                reloadGridView();
                Control ctrl = this.gridMovCuentasBanco.Parent;
                sPath = ExportToExcel();
                String sURL = "frmDescargaTmpFile.aspx";

                string datosaencriptar;
                datosaencriptar = "archivo=";
                datosaencriptar += sPath;
                datosaencriptar += "&filename="+ sFileName + "&ContentType=application/ms-excel&";

                String parameter = sURL + "?data=";
                parameter += Utils.encriptacadena(datosaencriptar);

                JSUtils.OpenNewWindowOnClick(ref this.btnImprimeReporte, parameter, "Reporte Movimientos Bancos", true);
                this.btnImprimeReporte.Visible = true;
                this.btnGeneraReporte.Visible = false;
                ctrl.Controls.Add(this.gridMovCuentasBanco);
                this.reloadGridView();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.PRINT, "btnGeneraReporte_Click", ref ex);
            }
            
        }

        private String ExportToExcel()
        {
            String sFilename = "";
            sFilename = Path.GetTempFileName();
            StreamWriter sw = new StreamWriter(sFilename);
            try
            {
                this.EnableViewState = false;
                System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
                string sHeader = "<table><tr><td><b>";
                sHeader += "Cuenta: </b></td><td colspan=\"3\">" + this.drpdlCuenta.SelectedItem.Text + "</td></tr><tr><td><b>";
                sHeader += "Periodo: </b></td><td>";
                sHeader += "</td><td><b>A:</b></td><td></td></tr>";
                sHeader += "<tr><td><b>Saldo Inicial:</b></td><td>" + this.lblSaldoInicial.Text;
                sHeader += "</td><td><b>Saldo Final:</b></td><td>" + this.lblSaldofinal.Text;
                sHeader += "</td></tr></table>";
                this.gridMovCuentasBanco.AllowPaging = false; //all the movements in one page.
                this.gridMovCuentasBanco.Columns[0].Visible = false; //hide the select button
                this.gridMovCuentasBanco.DataBind();
                Page pPage = new Page();
                pPage.EnableViewState = false;
                pPage.EnableEventValidation = false;
                pPage.DesignerInitialize();


                HtmlForm fForm = new HtmlForm();
                //dg.EnableViewState = false;
                pPage.Controls.Add(fForm);
                Label lblHeader = new Label();
                lblHeader.Text = sHeader;
                //this.gridMovCuentasBanco.RenderControl(oHtmlTextWriter);
                fForm.Controls.Add(lblHeader);
                this.gridMovCuentasBanco.GridLines = GridLines.Both;
                fForm.Controls.Add(this.gridMovCuentasBanco);
                pPage.RenderControl(oHtmlTextWriter);
                //dg.AllowPaging = true;
                this.gridMovCuentasBanco.Columns[0].Visible = true;
                this.gridMovCuentasBanco.GridLines = GridLines.None;
                //dg.EnableViewState = true;
                string ss = oStringWriter.ToString();
                ss = string.Format("<html><body>{0}<body></html>", ss);
                sw.Write(ss);
                sw.Close();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.PRINT, "ExportToExcel", ref ex);
            }            
            return sFilename;
        }

        protected void btnTraerChequesAlMes_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand comm = new SqlCommand();
            try
            {
                conn.Open();
                comm.Connection = conn;
                comm.CommandText = "update MovimientosCuentasBanco set MovimientosCuentasBanco.fecha = @fechaset where MovimientosCuentasBanco.movbanID IN (SELECT MovimientosCuentasBanco.movbanID FROM MovimientosCuentasBanco INNER JOIN Meses ON MONTH(MovimientosCuentasBanco.fecha) = Meses.monthID WHERE (MovimientosCuentasBanco.numCheque > 0) AND (MovimientosCuentasBanco.fecha < @fechaWhere) AND (MovimientosCuentasBanco.chequecobrado = 0) AND (MovimientosCuentasBanco.cuentaID = @cuentaID))";
                comm.Parameters.Add("@fechaset", SqlDbType.DateTime).Value = DateTime.Parse(this.drddlAnio.SelectedValue + "/" + this.drpdlMes.SelectedValue + "/01 00:00:00");
                comm.Parameters.Add("@fechaWhere", SqlDbType.DateTime).Value = DateTime.Parse(this.txtFechaCheqNoConc.Text);
                comm.Parameters.Add("@cuentaID", SqlDbType.Int).Value = this.drpdlCuenta.SelectedValue;
                if (comm.ExecuteNonQuery() <= 0)
                {
                    throw new Exception("No se pudo traer los cheques a la fecha seleccionada");
                }

                //get cheques no conciliados de meses anteriores
                DateTime dtCheques = DateTime.Parse(this.drddlAnio.SelectedValue + "/" + this.drpdlMes.SelectedValue + "/01 00:00:00");
                this.txtFechaCheqNoConc.Visible = true;
                this.txtFechaCheqNoConc.Text = dtCheques.ToString("yyyy/MM/dd 00:00:00");
                this.gvChequesNoConciliados.DataBind();
                this.txtFechaCheqNoConc.Visible = false;
                this.pnlChequesAnteriores.Visible = (this.gvChequesNoConciliados.Rows.Count > 0);
                this.reloadGridView();
                this.sacaTotales();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "Error trayendo meses a este mes", ref ex);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void chkPorFechas_CheckedChanged(object sender, EventArgs e)
        {
            this.reloadGridView();
        }

        protected void chkPorMes_CheckedChanged(object sender, EventArgs e)
        {
            this.reloadGridView();
        }

        protected void PopCalendar5_SelectionChanged(object sender, EventArgs e)
        {
            this.reloadGridView();
        }

        protected void PopCalendar6_SelectionChanged(object sender, EventArgs e)
        {
            this.reloadGridView();
        }

        protected int sacaidCheque()
        {
            int i;
            for(i=0;i<this.ddlConcepto.Items.Count;i++){

                if(this.ddlConcepto.Items[i].Text=="CHEQUE"){
                    break;
                }
            }
            return i;
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.SqlDataSource1.DeleteParameters.Add("@bancoID", System.TypeCode.Int32,this.drpdlBancos.SelectedValue);
            
            this.drpdltitular.DataBind();
            this.drpdlCuenta.DataBind();
            this.gridMovCuentasBanco.DataBind();
            this.gridSaldosMensuales.DataBind();
            this.gridSaldosporPeriodos.DataBind();
            reloadGridView();

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.sdsCuentas.DeleteParameters.Add("@bancoID", System.TypeCode.Int32, this.drpdlBancos.SelectedValue);
            //this.sdsCuentas.DeleteParameters.Add("@Titular", System.TypeCode.Int32, this.drpdltitular.SelectedValue);
            
            //this.drpdltitular.DataBind();
            this.drpdlCuenta.DataBind();
            this.gridMovCuentasBanco.DataBind();
            this.gridSaldosMensuales.DataBind();
            this.gridSaldosporPeriodos.DataBind();
            reloadGridView();

        }
        protected bool numChequeValido(int ChequeNum,int Cuenta){
            Boolean sresult;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            String query = "SELECT numchequeinicio, numchequefin FROM CuentasDeBanco WHERE cuentaID=@cuentaID";
            SqlCommand comm = new SqlCommand(query, conn);
            SqlDataReader rd;
            try 
            {
                conn.Open();
                comm.Parameters.Add("@cuentaID", SqlDbType.Int).Value =Cuenta;
                rd = comm.ExecuteReader();
                if (rd.HasRows && rd.Read())
                {
                    
                    if (ChequeNum <= int.Parse(rd["numchequefin"].ToString()) && ChequeNum >= int.Parse(rd["numchequeinicio"].ToString()))
                    {
                        sresult = true;
                    }
                    else 
                    { 
                        sresult =false; 
                    }
                }
                else
                {
                    sresult = false;
                }
            

                }
            catch(Exception ex)
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


        protected void btnBloquearPeriodo_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            DateTime dtInicio = new DateTime();
            DateTime dtFin = new DateTime();
            try
            {
                TimeSpan tsOneDay = new TimeSpan(1, 0, 0, 0);
                if (chkPorMes.Checked || (!chkPorMes.Checked && !chkPorFechas.Checked))
                {
                    this.chkPorMes.Checked = true;
                    dtInicio = new DateTime(int.Parse(this.drddlAnio.SelectedValue), int.Parse(this.drpdlMes.SelectedValue), 1);
                    // TimeSpan tsUndia = new TimeSpan(1, 0, 0, 0);
                    if (dtInicio.Month < 12)
                        dtFin = new DateTime(dtInicio.Year, dtInicio.Month + 1, 1, 23, 59, 59);
                    else
                        dtFin = new DateTime(dtInicio.Year + 1, 1, 1, 23, 59, 59);
                    dtFin = dtFin - tsOneDay;
                }
                else
                {
                    dtInicio = DateTime.Parse(this.txtFechaIncio.Text, new CultureInfo("es-Mx"));
                    dtFin = DateTime.Parse(this.txtFechaFin.Text, new CultureInfo("es-Mx"));
                }
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                conn.Open();
                comm.CommandText = "INSERT INTO PeriodosBloqueados (cuentaid, periodoINI, periodoFIN) "
                    + " VALUES (@cuentaid,@periodoINI,@periodoFIN)";
                comm.Parameters.Add("@cuentaid", SqlDbType.Int).Value = this.drpdlCuenta.SelectedValue;
                comm.Parameters.Add("@periodoINI", SqlDbType.DateTime).Value = dtInicio;
                comm.Parameters.Add("@periodoFIN", SqlDbType.DateTime).Value = dtFin;
                if (comm.ExecuteNonQuery() != 1)
                {
                    throw new Exception("No se pudo agregar el periodo bloqueado.");
                }
                this.MakeVisibleBloqDesPeriodos();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Err adding Periodo bloqueado, ini: " + dtInicio.ToString() + " fin: " + dtFin.ToString(), ref ex);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnDesbloquearPeriodo_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            DateTime dtInicio = new DateTime();
            DateTime dtFin = new DateTime();
            try
            {
                TimeSpan tsOneDay = new TimeSpan(1, 0, 0, 0);
                if (chkPorMes.Checked || (!chkPorMes.Checked && !chkPorFechas.Checked))
                {
                    this.chkPorMes.Checked = true;
                    dtInicio = new DateTime(int.Parse(this.drddlAnio.SelectedValue), int.Parse(this.drpdlMes.SelectedValue), 1);
                    // TimeSpan tsUndia = new TimeSpan(1, 0, 0, 0);
                    if (dtInicio.Month < 12)
                        dtFin = new DateTime(dtInicio.Year, dtInicio.Month + 1, 1, 23, 59, 59);
                    else
                        dtFin = new DateTime(dtInicio.Year + 1, 1, 1, 23, 59, 59);
                    dtFin = dtFin - tsOneDay;
                }
                else
                {
                    dtInicio = DateTime.Parse(this.txtFechaIncio.Text, new CultureInfo("es-Mx"));
                    dtFin = DateTime.Parse(this.txtFechaFin.Text, new CultureInfo("es-Mx"));
                }
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                conn.Open();
                comm.CommandText = "delete PeriodosBloqueados WHERE (cuentaid = @cuentaid) "
                    + " AND (@periodoINI = periodoINI AND @periodoFIN = periodoFIN);";
                comm.Parameters.Add("@cuentaid", SqlDbType.Int).Value = this.drpdlCuenta.SelectedValue;
                comm.Parameters.Add("@periodoINI", SqlDbType.DateTime).Value = dtInicio;
                comm.Parameters.Add("@periodoFIN", SqlDbType.DateTime).Value = dtFin;
                if (comm.ExecuteNonQuery() <= 0)
                {
                    throw new Exception("No se pudo eliminar el periodo bloqueado.");
                }
                this.MakeVisibleBloqDesPeriodos();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Err deleting Periodo bloqueado, ini: " + dtInicio.ToString() + " fin: " + dtFin.ToString(), ref ex);
            }
            finally
            {
                conn.Close();
            }
        }

        internal void MakeVisibleBloqDesPeriodos()
        {

            DateTime dtInicio = new DateTime();
            DateTime dtFin = new DateTime();
            try
            {
                TimeSpan tsOneDay = new TimeSpan(1, 0, 0, 0);
                if (chkPorMes.Checked || (!chkPorMes.Checked && !chkPorFechas.Checked))
                {
                    this.chkPorMes.Checked = true;
                    dtInicio = new DateTime(int.Parse(this.drddlAnio.SelectedValue), int.Parse(this.drpdlMes.SelectedValue), 1);
                    // TimeSpan tsUndia = new TimeSpan(1, 0, 0, 0);
                    if (dtInicio.Month < 12)
                        dtFin = new DateTime(dtInicio.Year, dtInicio.Month + 1, 1);
                    else
                        dtFin = new DateTime(dtInicio.Year + 1, 1, 1);
                    dtFin = dtFin - tsOneDay;
                }
                else
                {
                    dtInicio = DateTime.Parse(this.txtFechaIncio.Text, new CultureInfo("es-Mx"));
                    dtFin = DateTime.Parse(this.txtFechaFin.Text, new CultureInfo("es-Mx"));
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "Err adding Periodo bloqueado, ini: " + dtInicio.ToString() + " fin: " + dtFin.ToString(), ref ex);
            }

            this.btnBloquearPeriodo.Visible = !dbFunctions.FechaEnPeriodoBloqueado(dtFin, int.Parse(this.drpdlCuenta.SelectedValue));
            this.lblPeriodoBloqueado.Visible = this.btnDesbloquearPeriodo.Visible = !this.btnBloquearPeriodo.Visible;
            this.gvPeriodosBloqueados.DataBind();
        }

        protected void gvPeriodosBloqueados_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            this.MakeVisibleBloqDesPeriodos();
        }

        protected void gvPeriodosBloqueados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.MakeVisibleBloqDesPeriodos();
        }

      
    }
}
