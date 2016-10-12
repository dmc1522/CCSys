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
    public partial class frmLiquidacionAdd : Garibay.BasePage
    {
//          TimeSpan stop;
//          TimeSpan start;
        private dsBoletas.dtBoletasDataTable dtBoletas = null;
        private String sSessiondtBoletas = "LiqBoletas";
        private String sSessiondtExistBoletas = "LiqExistBoletas";
        private String sSessiondtBoletaData = "LiqBoletaData";
        private String sSessiondtPagos = "LiqBoletaPagos";
        private DataTable dtBoletaData = null;
        private dsLiquidacion dsPagos = null;
        private int iLiqID = -1;

       
        private dsLiquidacion DsPAGOS
        {
            get { 
                return this.Session[this.sSessiondtPagos] == null ? new dsLiquidacion() : (dsLiquidacion)this.Session[this.sSessiondtPagos]; 
            }
            set{
                this.Session[this.sSessiondtPagos] = (value == null ? new dsLiquidacion() : (dsLiquidacion)value);
            }
        }

      
        private void LoadXMLPagos(String sXML)
        {
           
            //bool result = false;
            this.dsPagos = this.DsPAGOS;
            try
            {
	            if (sXML != "")
                {
                    dsLiquidacion dsTemp = new dsLiquidacion();
                    dsTemp.Tables.Clear();
                    using (StringReader sr = new StringReader(sXML))
                    {
                        dsTemp.ReadXml(sr);
                    }
                    if (dsTemp.Tables.Count == 1)
                    {
                        this.dsPagos.Tables.Clear();
                        this.dsPagos.Tables.Add(new dsLiquidacion.dtPagosDataTable());
                        foreach(DataRow row in dsTemp.Tables[0].Rows)
                        {
                            this.dsPagos.Tables[0].ImportRow(row);
                        }
                    }
                }
                if (dsPagos.Tables.Count != 1)
                {
                    this.dsPagos.Tables.Clear();
                    this.dsPagos.Tables.Add(new dsLiquidacion.dtPagosDataTable());
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, this.UserID, "ERROR CASTING XML TO PAGOS DATA EX:" + ex.Message, "LoadXMLPagos");
            }
          
        }


        private string GetXMLPagos()
        {
            string sXml;
            StringWriter tw = new StringWriter();
            this.DsPAGOS.WriteXml(tw, XmlWriteMode.WriteSchema);
            sXml = tw.ToString();
            return sXml;
        }
       
        private void AddJSInControls()
        {
           
            String sOnchangeAB = "ShowHideDivOnChkBox('";
            sOnchangeAB += this.chkAgregarBoletas.ClientID + "','";
            sOnchangeAB += this.divAddBoletas.ClientID + "')";
            this.chkAgregarBoletas.Attributes.Add("onclick", sOnchangeAB);

            sOnchangeAB = "ShowHideDivOnChkBox('";
            sOnchangeAB += this.chkMostrarAgregarPago.ClientID + "','";
            sOnchangeAB += this.divAgregarNuevoPago.ClientID + "')";
            this.chkMostrarAgregarPago.Attributes.Add("onclick", sOnchangeAB);

            String sOnchange = "ShowHideDivOnChkBox('";
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

            sOnchange = "SelectOnChange('";
            sOnchange += this.ddlProductor.ClientID + "','";
            sOnchange += this.ddlNewBoletaProductor.ClientID + "');";
            this.ddlProductor.Attributes.Add("onChange", sOnchange);
           
        }

    

        protected void Page_Load(object sender, EventArgs e)
         {
             
            this.divAddBoletas.Attributes.Add("style", this.chkAgregarBoletas.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divAgregarNuevoPago.Attributes.Add("style", this.chkMostrarAgregarPago.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");           
            this.divFechaSalidaNewBoleta.Attributes.Add("style", this.chkChangeFechaSalidaNewBoleta.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.pnlNewBoleta.Visible = false;
            this.pnlLiquidacionResult.Visible = false;
            this.imgLiquidacionBien.Visible = false;
            this.imgLiquidacionMal.Visible = false;
           

            if (this.txtLiquidacionID.Text.Length > 0)
            {
                if(!int.TryParse(this.txtLiquidacionID.Text, out this.iLiqID))
                {
                    this.iLiqID = -1;
                }
            }
            if (!this.IsPostBack)
            {
              
                this.AddJSInControls();
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.LIQUIDACIONES, Logger.typeUserActions.SELECT, this.UserID, "VISITÓ LA PÁGINA AGREGAR/MODIFICAR LIQUIDACION." );
                string parameter;
                parameter = "javascript:url('";
                parameter += "frmAddQuickProductor.aspx";
                parameter += "', '";
                parameter += "Agregar Productor Rápido";
                parameter += "',0,200,400,800); return false;";



                this.btnAddQuickProductor.Attributes.Clear();
                this.btnAddQuickProductor.Attributes.Add("onClick", parameter);

                JSUtils.AddDisableWhenClick(ref this.btnActualizaComboProductores, this);
       //         JSUtils.AddDisableWhenClick(ref this.btnAddQuickProductor, this);
                JSUtils.AddDisableWhenClick(ref this.btnRealizaLiq, this);
                JSUtils.AddDisableWhenClick(ref this.btnAddNewBoleta, this);
                JSUtils.AddDisableWhenClick(ref this.btnAddPago, this);
                JSUtils.AddDisableWhenClick(ref this.btnAgregarBoletaaLista, this);
                JSUtils.AddDisableWhenClick(ref this.btnDeshacer, this);
                JSUtils.AddDisableWhenClick(ref this.btnVerificarAntesAdd, this);
                JSUtils.AddDisableWhenClick(ref this.btnAgregarLiquidacion, this);
                
                

                this.cmbCuentaPago.DataBind();
                this.cmbCuentaPago.SelectedIndex = 4;

                this.btnAgregarLiquidacion.Visible = false;
                this.btnVerificarAntesAdd.Visible = true;
                this.btnDeshacer.Visible = false;
                this.DsPAGOS = new dsLiquidacion();
                dsBoletas.dtBoletasDataTable dtExist = new dsBoletas.dtBoletasDataTable();
                this.dtBoletas = new dsBoletas.dtBoletasDataTable();
                this.Session[this.sSessiondtExistBoletas] = dtExist;
                this.Session[this.sSessiondtBoletas] = dtBoletas;

                this.dtBoletas = (dsBoletas.dtBoletasDataTable)this.Session[this.sSessiondtBoletas];
                this.gvBoletas.DataSource = this.dtBoletas;

                this.ddlCiclo.DataBind();
                this.ddlProdBoletas.DataBind();
                this.ddlProductoExistBoleta.DataBind();
                this.ddlProductor.DataBind();
                this.cmbProductoresPago.DataBind();
                this.ddlNewBoletaProductor.DataBind();

                this.txtFechaPago.Text = Utils.Now.ToString("dd/MM/yyyy");
                

                if (Request.QueryString["data"] != null && this.loadqueryStrings(Request.QueryString["data"].ToString()) && int.TryParse(this.myQueryStrings["liqID"].ToString(), out iLiqID) && iLiqID > -1)
                {
                    
                   
                    
                    this.GridView1.Visible = false;
                    this.txtLiquidacionID.Text = this.iLiqID.ToString();
                    this.gvAnticipos.DataBind();
                    
                    this.LoadBoletaData();
                   
                    //this.updateFilterExistBoletas();
                    this.ddlCiclo.Enabled = false;
                    this.ddlProductor.Enabled = false;
                    this.gvBoletas.DataSource = this.dtBoletas;
                    this.chkAsignarAnticipos.Visible = false;
                    this.seleccionavalorproductor(this.ddlProductor.SelectedValue, this.ddlProductor.SelectedItem.Text);
                    // this.seejecutoliquidacion(true); // HAY QUE METER ESTO SOLO SI LA LIQ QUE SE ESTA VIENDO YA ESTA PAGADA!!!!!
                    //this.PopCalendar1.Enabled = false;
                    this.updateTotales();
                    try
                    {

                        if (this.gvBoletas.Rows.Count > 0 && this.gvBoletas.DataKeys[0] != null && this.gvBoletas.DataKeys[0]["Producto"] != null)
                        {
                            string producto = this.gvBoletas.DataKeys[0]["Producto"].ToString();
                            if (producto.IndexOf("BLANCO") > 0 || producto.IndexOf("AMARILLO") > 0)
                            {
                                seleccionacatalogomaiz("MAIZ");
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {

                    }
                    
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.LIQUIDACIONES, Logger.typeUserActions.SELECT, this.UserID, "ABRIÓ LA LIQUIDACIÓN NÚMERO." + this.lblNumLiquidacion.Text);
               

            
                }
                else
                {
                    this.ddlCiclo.SelectedIndex = 0;
                    this.ddlProdBoletas.SelectedIndex = 0;
                    this.txtFechaLiquidacion.Text = Utils.Now.ToString("dd/MM/yyyy");
                    this.PopCalendar1_SelectionChanged(null, null);
                    this.gvBoletas.DataSource = this.dtBoletas;
                    this.seejecutoliquidacion(false);
                }

                
                this.gvBoletas.DataSource = this.dtBoletas;
                this.updateTotales();
                
                this.updateFilterExistBoletas();

                //this.cmbConceptomovBanco.DataBind();
                //this.cmbTipodeMov.DataBind();
                this.cmbConceptomovBancoPago.DataBind();
                this.cmbTipodeMovPago.DataBind();

            }
            
            this.gvExistBoletas.DataSource = (dsBoletas.dtBoletasDataTable)this.Session[this.sSessiondtExistBoletas];
            //this.gvExistBoletas.DataBind();

            this.dtBoletas = (dsBoletas.dtBoletasDataTable)this.Session[this.sSessiondtBoletas];
            this.gvBoletas.DataSource = this.dtBoletas;

            if (this.Session[this.sSessiondtBoletaData] != null)
            {
                this.dtBoletaData = (DataTable)this.Session[this.sSessiondtBoletaData];
                this.Session[this.sSessiondtBoletaData] = this.dtBoletaData;
            }
            //this.gvBoletas.DataBind();
            //this.updateFilterExistBoletas();    
            this.divPagoMovCaja.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text== "EFECTIVO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            String sOnchagemov = "checkValueInList(";
            sOnchagemov += "this" + ",'EFECTIVO','";
            sOnchagemov += this.divPagoMovCaja.ClientID + "');";
            this.cmbTipodeMovPago.Attributes.Add("onChange", sOnchagemov);
            this.divMovBanco.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "MOVIMIENTO DE BANCO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            sOnchagemov += "checkValueInList(";
            sOnchagemov += "this" + ",'MOVIMIENTO DE BANCO','";
            sOnchagemov += this.divMovBanco.ClientID + "');";
            this.cmbTipodeMovPago.Attributes.Add("onChange", sOnchagemov);
            this.divCheque.Attributes.Add("style", this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            sOnchagemov = "checkValueInList(";
            sOnchagemov += "this" + ",'CHEQUE','";
            sOnchagemov += this.divCheque.ClientID + "');";
            this.cmbConceptomovBancoPago.Attributes.Add("onChange", sOnchagemov);
            this.pnlNewPago.Visible = false;
        

            this.compruebasecurityLevel();

        }

        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                if (!this.btnPrintLiquidacion.Visible)
                {
                    this.Response.Redirect("~/frmUnauthorizedAccess.aspx");

                }
                else{
                    this.panelAddLiquidacion.Visible = false;
                    this.divAddBoletas.Visible = false;
                    this.gvBoletas.Columns[0].Visible = false;
                    this.btnDeshacer.Visible = false;
                    this.divAgregarNuevoPago.Visible = false;
                    this.chkAgregarBoletas.Visible = false;
                    this.chkMostrarAgregarPago.Visible = false;
                    this.btnSaveLiq.Visible = false;
                    this.btnRealizaLiq.Visible = false;
                }
            }

        }
        
        protected void LoadPagosFromDB()
        {
          
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SET CONCAT_NULL_YIELDS_NULL OFF;"
                + " SELECT     PagosLiquidacion.pagoLiqID, PagosLiquidacion.liquidacionID, PagosLiquidacion.fecha, Bancos.nombre + CuentasDeBanco.NumeroDeCuenta AS Cuenta,  CuentasDeBanco.cuentaID, MovimientosCuentasBanco.cargo AS monto, MovimientosCuentasBanco.numCheque, MovimientosCaja.cargo AS Efectivo,  MovimientosCuentasBanco.movbanID, MovimientosCaja.movimientoID, PagosLiquidacion.cicloID, PagosLiquidacion.userID, MovimientosCaja.bodegaID,  Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS nombre, CONVERT(varchar(255), MovimientosCuentasBanco.Observaciones)  + CONVERT(varchar(255), MovimientosCaja.Observaciones) AS Observaciones, catalogoMovimientosBancos.catalogoMovBancoID AS catalogoCajaChicaID,  catalogoMovimientosBancos.claveCatalogo + catalogoMovimientosBancos.catalogoMovBanco AS catalogoCajaChica,  SubCatalogoMovimientoBanco.subCatalogoMovBancoID AS subCatalogoCajaChicaID,  SubCatalogoMovimientoBanco.subCatalogoClave + SubCatalogoMovimientoBanco.subCatalogo AS subCatalogoCajaChica,  catalogoMovimientosBancos_1.catalogoMovBancoID AS catalogoInternoMovBancoID,  catalogoMovimientosBancos_1.claveCatalogo + catalogoMovimientosBancos_1.catalogoMovBanco AS catalogoInternoMovBanco, "
                + " SubCatalogoMovimientoBanco_1.subCatalogoMovBancoID AS subCatalogoInternoMovBancoID, SubCatalogoMovimientoBanco_1.subCatalogoClave + SubCatalogoMovimientoBanco_1.subCatalogo AS subCatalogoInternoMovBanco,  catalogoMovimientosBancos_2.catalogoMovBancoID AS catalogoFiscalMovBancoID,  catalogoMovimientosBancos_2.claveCatalogo + catalogoMovimientosBancos_2.catalogoMovBanco AS catalogoFiscalMovBanco,  SubCatalogoMovimientoBanco_2.subCatalogoMovBancoID AS subCatalogoFiscalMovBancoID,  SubCatalogoMovimientoBanco_2.subCatalogoClave + SubCatalogoMovimientoBanco_2.subCatalogo AS subCatalogoFiscalMovBanco,  MovimientosCuentasBanco.facturaOlarguillo + MovimientosCaja.facturaOlarguillo AS facturaolarguillo,  MovimientosCuentasBanco.numCabezas + MovimientosCaja.numCabezas AS numCabezas, MovimientosCuentasBanco.chequeNombre AS chequenombre,  PagosLiquidacion.productorID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS productorNombre,  ConceptosMovCuentas.Concepto AS concepto, ConceptosMovCuentas.ConceptoMovCuentaID AS conceptoID FROM         catalogoMovimientosBancos AS catalogoMovimientosBancos_1 INNER JOIN CuentasDeBanco INNER JOIN MovimientosCuentasBanco ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco.cuentaID INNER JOIN "
                + " Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID ON  catalogoMovimientosBancos_1.catalogoMovBancoID = MovimientosCuentasBanco.catalogoMovBancoInternoID INNER JOIN catalogoMovimientosBancos AS catalogoMovimientosBancos_2 ON  MovimientosCuentasBanco.catalogoMovBancoFiscalID = catalogoMovimientosBancos_2.catalogoMovBancoID INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID LEFT OUTER JOIN SubCatalogoMovimientoBanco AS SubCatalogoMovimientoBanco_1 ON  MovimientosCuentasBanco.subCatalogoMovBancoInternoID = SubCatalogoMovimientoBanco_1.subCatalogoMovBancoID LEFT OUTER JOIN SubCatalogoMovimientoBanco AS SubCatalogoMovimientoBanco_2 ON  MovimientosCuentasBanco.subCatalogoMovBancoFiscalID = SubCatalogoMovimientoBanco_2.subCatalogoMovBancoID RIGHT OUTER JOIN catalogoMovimientosBancos INNER JOIN MovimientosCaja ON catalogoMovimientosBancos.catalogoMovBancoID = MovimientosCaja.catalogoMovBancoID LEFT OUTER JOIN SubCatalogoMovimientoBanco ON MovimientosCaja.subCatalogoMovBancoID = SubCatalogoMovimientoBanco.subCatalogoMovBancoID RIGHT OUTER JOIN Productores INNER JOIN PagosLiquidacion ON Productores.productorID = PagosLiquidacion.productorID ON MovimientosCaja.movimientoID = PagosLiquidacion.movimientoID ON  MovimientosCuentasBanco.movbanID = PagosLiquidacion.movbanID "
                + " where PagosLiquidacion.liquidacionID = @liqID";
            comm.Parameters.Add("@liqID", SqlDbType.Int).Value = this.iLiqID;
            comm.Connection = conn;
            SqlDataAdapter sqlDA = new SqlDataAdapter(comm);
            try
            {
                conn.Open();
                this.dsPagos = this.DsPAGOS;
                this.dsPagos.Tables.Clear();
                this.dsPagos.Tables.Add(new dsLiquidacion.dtPagosDataTable());
                sqlDA.Fill(this.dsPagos.Tables[0]);
                this.DsPAGOS = this.dsPagos;
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, this.UserID, "error: " + ex.Message + "Stack:" + Environment.StackTrace, this.Request.Url.ToString());
            }
            finally
            {
                conn.Close();
            }
            
             
        }

       
        /// <summary>
        /// Load the liquidacion data only
        /// </summary>
        /// <returns></returns>
        protected bool LoadLiqData()
        {
          
           
             
            SqlCommand sqlComm = new SqlCommand();
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlComm);
            bool bResult = false;
            try
            {
                this.dtBoletaData = new DataTable();
                sqlConn.Open();
                sqlComm.Connection = sqlConn;

                sqlComm.CommandText = "SELECT * FROM Liquidaciones WHERE (LiquidacionID = @LIQUIDACIONID)";
                sqlComm.Parameters.Add("@LIQUIDACIONID", SqlDbType.Int).Value = this.iLiqID;

                if (sqlDA.Fill(this.dtBoletaData) == 1)
                {
                    bResult = true;
                }
                else
                {
                    bResult = false;
                }
            }
            catch(Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, this.UserID, "Error cargando informacion de la liquidacion ex: " + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                sqlConn.Close();
            }

          
            return bResult;
        }

      
        protected bool LoadBoletaData()
        {
            SqlCommand sqlComm = new SqlCommand();
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlComm);
            bool bResult = false;
            try
            {
                this.dtBoletaData = new DataTable();
	            sqlConn.Open();
                sqlComm.Connection = sqlConn;

                sqlComm.CommandText = "SELECT * FROM Liquidaciones WHERE (LiquidacionID = @LIQUIDACIONID)";
                sqlComm.Parameters.Add("@LIQUIDACIONID", SqlDbType.Int).Value = this.iLiqID;

                if (sqlDA.Fill(this.dtBoletaData) == 1)
                {
                    this.Session[this.sSessiondtBoletaData] = this.dtBoletaData;
                    this.ddlCiclo.DataBind();
                    this.ddlCiclo.SelectedValue = this.dtBoletaData.Rows[0]["cicloID"].ToString();
                    /*
SELECT        productorID, LTRIM(apaterno + ' ' + amaterno + ' ' + nombre) AS Productor FROM Productores 
FROM            Productores INNER JOIN
                         Liquidaciones ON Productores.productorID = Liquidaciones.productorID
WHERE        (Liquidaciones.LiquidacionID = @liqID)*/

                    this.sdsProductores.SelectCommand = "SELECT Productores.productorID, LTRIM(Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre) AS Productor FROM Productores INNER JOIN Liquidaciones ON Productores.productorID = Liquidaciones.productorID WHERE (Liquidaciones.LiquidacionID = @liqID)";
                    this.sdsProductores.SelectParameters.Add("liqID", this.iLiqID.ToString());
                    this.ddlProductor.DataBind();

                    this.ddlProductor.SelectedValue = this.dtBoletaData.Rows[0]["productorID"].ToString();
                    this.txtFechaLiquidacion.Text = DateTime.Parse(this.dtBoletaData.Rows[0]["fecha"].ToString()).ToString("dd/MM/yyyy");
                    this.txtFechaLuiquidacionLong.Text = this.dtBoletaData.Rows[0]["fechalarga"].ToString();
                    this.txtTotalNotas.Text = this.dtBoletaData.Rows[0]["notas"].ToString();
                    this.txtIntereses.Text = this.dtBoletaData.Rows[0]["intereses"].ToString();
                    this.txtSeguro.Text = this.dtBoletaData.Rows[0]["seguro"].ToString();
                    string pagodata = this.dtBoletaData.Rows[0]["pagosData"].ToString();
                    this.DsPAGOS.Tables.Clear();
                    if (bool.Parse(this.dtBoletaData.Rows[0]["cobrada"].ToString()))// ESTA COBRADA
                    {
                        this.LoadPagosFromDB();
                    }
                    else
                    {
                        this.LoadXMLPagos(pagodata);
                    }
                    //
                   // this.gvPagosLiquidacion.DataSource = null;
                    this.gvPagosLiquidacion.DataSourceID = "";
                    this.gvPagosLiquidacion.DataSource = dsPagos.Tables[0];
                    //this.gvPagosLiquidacion.DataBind();
                    this.DsPAGOS = dsPagos;



                    this.updatePanelBoletas.Visible = true;
                    this.btnAgregarLiquidacion.Visible = false;
                    this.btnVerificarAntesAdd.Visible = false;

                    this.lblNumLiquidacion.Text = this.iLiqID.ToString();
                    this.txtNombre.Text = this.dtBoletaData.Rows[0]["nombre"].ToString();
                    this.txtDomicilio.Text = this.dtBoletaData.Rows[0]["domicilio"].ToString();
                    this.txtPoblacion.Text = this.dtBoletaData.Rows[0]["poblacion"].ToString();
                    
//                     this.chkAplicarImpureza.Checked = (bool)this.dtBoletaData.Rows[0]["applyImpureza"];
//                     this.chkDescuentoSecado.Checked = (bool)this.dtBoletaData.Rows[0]["applySecado"];
//                     this.chkBosCalculaHumedad.Checked = (bool)this.dtBoletaData.Rows[0]["applyHumedad"];
                    this.updateChkDescSecado();
                    this.updateChkHumedad();
               

                    SqlConnection BoletasConn = new SqlConnection(myConfig.ConnectionInfo);
                    BoletasConn.Open();
                    try
                    {
                        SqlCommand BoletasComm = new SqlCommand();
                        BoletasComm.CommandText = "SELECT     Boletas.boletaID, Boletas.cicloID, Boletas.userID, Boletas.productorID, Boletas.productoID, Boletas.bodegaID, Boletas.NumeroBoleta, Boletas.Ticket,  Boletas.codigoClienteProvArchivo, Boletas.NombreProductor, Boletas.placas, Boletas.FechaEntrada, Boletas.PesadorEntrada, Boletas.PesoDeEntrada,  Boletas.BasculaEntrada, Boletas.FechaSalida, Boletas.PesoDeSalida, Boletas.PesadorSalida, Boletas.BasculaSalida, Boletas.pesonetoentrada,  Boletas.pesonetosalida, Boletas.humedad, Boletas.dctoHumedad, Boletas.impurezas, Boletas.dctoImpurezas, Boletas.totaldescuentos, Boletas.pesonetoapagar,  Boletas.precioapagar, Boletas.importe, Boletas.dctoSecado, Boletas.totalapagar, Boletas.chofer, Boletas.pagada, Boletas.storeTS, Boletas.updateTS,  Productos.Nombre AS Producto, Boletas.applyHumedad, Boletas.applyImpurezas, Boletas.applySecado, Bodegas.bodega FROM Boletas INNER JOIN Liquidaciones_Boletas ON Boletas.boletaID = Liquidaciones_Boletas.BoletaID INNER JOIN Productos ON Boletas.productoID = Productos.productoID INNER JOIN Bodegas ON Boletas.bodegaID = Bodegas.bodegaID";
                        BoletasComm.CommandText += " WHERE (Liquidaciones_Boletas.LiquidacionID = @LiqID)";
                        BoletasComm.Connection = BoletasConn;
                        BoletasComm.Parameters.Add("@LiqID", SqlDbType.Int).Value = this.iLiqID;
                        SqlDataAdapter BoletasAD = new SqlDataAdapter(BoletasComm);
                        BoletasAD.Fill(this.dtBoletas);
                        this.Session[this.sSessiondtBoletas] = dtBoletas;
                        this.gvBoletas.DataSource = dtBoletas;
                        this.gvBoletas.DataBind();
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, this.UserID, "Error load Liq_Boletas EX:" + ex.Message, this.Request.Url.ToString());
                    }
                    finally
                    {
                        BoletasConn.Close();
                    }
                    if (bool.Parse(this.dtBoletaData.Rows[0]["cobrada"].ToString()))// ESTA COBRADA
                    {
                        this.seejecutoliquidacion(true);
                    }
                    else
                    {
                        this.seejecutoliquidacion(false);
                    }
                    this.gvPagosLiquidacion.DataBind();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(Session["USERID"].ToString()), "Error load liquidacion EX:" + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                sqlConn.Close();
            }
           
           
            return bResult;
        }

       
        protected void updateFilterExistBoletas()
        {
            
           
            String sBoletasInGV = "";
            if (this.ddlCiclo.SelectedIndex < 0)
            {
                this.ddlCiclo.DataBind();
                this.ddlCiclo.SelectedIndex = 0;
            }
            if (this.ddlProdBoletas.SelectedIndex < 0)
            {
                this.ddlProdBoletas.DataBind();
                this.ddlProdBoletas.SelectedIndex = 0;
            }
            if (this.ddlProductoExistBoleta.SelectedIndex < 0)
            {
                this.ddlProductoExistBoleta.DataBind();
                this.ddlProductoExistBoleta.SelectedIndex = 0;
            }
            SqlCommand sqlcomm = new SqlCommand("SELECT     Boletas.boletaID, Boletas.productorID, Productos.Nombre AS Producto, Boletas.NumeroBoleta, Boletas.Ticket, Boletas.pesonetoentrada, Boletas.cicloID,  Boletas.productoID, Boletas.bodegaID, Boletas.FechaEntrada, Boletas.PesoDeEntrada, Boletas.FechaSalida, Boletas.PesoDeSalida, Boletas.pesonetosalida,  Boletas.humedad, Boletas.dctoHumedad, Boletas.impurezas, Boletas.dctoImpurezas, Boletas.pesonetoapagar, Boletas.precioapagar, Boletas.importe,  Boletas.dctoSecado, Boletas.totalapagar, Boletas.pagada, Boletas.applyHumedad, Boletas.applyImpurezas, Boletas.applySecado FROM         Boletas INNER JOIN Productos ON Boletas.productoID = Productos.productoID LEFT OUTER JOIN Liquidaciones_Boletas ON Boletas.boletaID = Liquidaciones_Boletas.BoletaID" 
                + " WHERE (Boletas.productorID = @productorID) AND (Boletas.cicloID = @cicloID) AND (Boletas.productoID = @productoID) AND (Boletas.pagada = 0) AND "
                + " (Liquidaciones_Boletas.LiquidacionID IS NULL)");
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            sqlcomm.Connection = sqlConn;
            /*
            this.ddlCiclo.DataBind();
                        this.ddlProductoExistBoleta.DataBind();
                        this.ddlProdBoletas.DataBind();*/
            
            sqlcomm.Parameters.AddWithValue("@productorID",this.ddlProdBoletas.SelectedValue);
            sqlcomm.Parameters.AddWithValue("@cicloID",this.ddlCiclo.SelectedValue);
            sqlcomm.Parameters.AddWithValue("@productoID", this.ddlProductoExistBoleta.SelectedValue);
            sqlConn.Open();
            if (this.dtBoletas.Rows.Count > 0)
            {
                dsBoletas.dtBoletasRow row = (dsBoletas.dtBoletasRow)this.dtBoletas.Rows[0];
                sBoletasInGV = " AND Boletas.boletaID not in (" + row.boletaID.ToString();
                for (int i = 1; i < this.dtBoletas.Rows.Count; i++)
                {
                    row = (dsBoletas.dtBoletasRow)this.dtBoletas.Rows[i];
                    sBoletasInGV += ", " + row.boletaID.ToString();
                }
                sBoletasInGV += ")";
                sqlcomm.CommandText += sBoletasInGV;
            }
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcomm);
            dsBoletas.dtBoletasDataTable dtExist = (dsBoletas.dtBoletasDataTable)this.Session[this.sSessiondtExistBoletas];
            dtExist = new dsBoletas.dtBoletasDataTable();
            sqlda.Fill(dtExist);
            sqlConn.Close();
            this.gvExistBoletas.DataSource = dtExist;
            this.gvExistBoletas.DataBind();
            this.Session[this.sSessiondtExistBoletas] = dtExist;
          
           
        }

        protected void CargaProductor()
        {
           SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = " SELECT domicilio, poblacion, municipio FROM Productores where productorID = @productorID";
                comm.Parameters.AddWithValue("@productorID", this.ddlProductor.SelectedItem.Value);
                conn.Open();
                SqlDataReader sqlReader = comm.ExecuteReader();
                if (sqlReader.HasRows && sqlReader.Read())
                {
                    this.txtProductorID.Text = this.ddlProductor.SelectedItem.Value;
                    this.txtNombre.Text = this.ddlProductor.SelectedItem.Text;
                    this.txtDomicilio.Text = sqlReader["domicilio"].ToString();
                    this.txtPoblacion.Text = sqlReader["poblacion"].ToString() + ", " + sqlReader["municipio"].ToString();
                }
            }
            catch (System.Exception ex)
            {
                this.txtNombre.Text = "NO SE PUDIERON CARGAR LOS DATOS DEL PRODUCTOR";
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(Session["USERID"].ToString()), "NO SE PUDO CONSULTAR LOS DATOS DEL PRODUCTOR SELECCIONADO :"+ this.ddlProductor.SelectedItem.Value.ToString() +" EX:" + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                conn.Close();
            }
           
           
        }

        protected void PopCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Parse(this.txtFechaLiquidacion.Text);
            this.txtFechaLuiquidacionLong.Text = fecha.ToString("dddd, dd") + " de " + fecha.ToString("MMMM, yyyy");
        }

        protected void ddlProdBoletas_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlProductoExistBoleta.DataBind();
            this.updateFilterExistBoletas();
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            this.dtBoletas = (dsBoletas.dtBoletasDataTable)this.Session[this.sSessiondtBoletas];
            for (int i = 0; i < this.gvExistBoletas.Rows.Count; i++)
            {
                CheckBox chkDelete = (CheckBox)
                   gvExistBoletas.Rows[i].Cells[0].FindControl("chkRowSelected");
                if (chkDelete != null)
                {
                    if (chkDelete.Checked)
                    {
                        dsBoletas.dtBoletasRow newRow = this.dtBoletas.NewdtBoletasRow();
                        newRow.NumeroBoleta = gvExistBoletas.Rows[i].Cells[1].Text;
                        newRow.Ticket = gvExistBoletas.Rows[i].Cells[2].Text;
                        newRow.Producto = gvExistBoletas.Rows[i].Cells[3].Text;
                        newRow.pesonetoentrada = double.Parse(gvExistBoletas.Rows[i].Cells[4].Text);
                        newRow.boletaID = int.Parse(gvExistBoletas.Rows[i].Cells[5].Text);
                        this.dtBoletas.Rows.Add(newRow);
                    }
                }
            }
            this.Session[this.sSessiondtBoletas] = dtBoletas;
            this.gvBoletas.DataSource = this.dtBoletas;
            this.gvBoletas.DataBind();
            this.updateFilterExistBoletas();
        }

        protected void ddlCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlProdBoletas.DataBind();
            this.ddlProductoExistBoleta.DataBind();
            this.updateFilterExistBoletas();
        }

        protected void ddlProductoExistBoleta_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.updateFilterExistBoletas();
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            this.updateFilterExistBoletas();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.dtBoletas = (dsBoletas.dtBoletasDataTable)this.Session[this.sSessiondtBoletas];
            dsBoletas.dtBoletasDataTable dtExist = (dsBoletas.dtBoletasDataTable)this.Session[this.sSessiondtExistBoletas];
            this.gvExistBoletas.DataSourceID = this.gvExistBoletas.DataSourceID;
            for (int i = 0; i < this.gvExistBoletas.Rows.Count; i++)
            {
                CheckBox chkDelete = (CheckBox)
                   gvExistBoletas.Rows[i].Cells[0].FindControl("chkRowSelected");
                if (chkDelete != null)
                {
                    if (chkDelete.Checked)
                    {
                        dsBoletas.dtBoletasRow newRow = this.dtBoletas.NewdtBoletasRow();

                        newRow.NumeroBoleta = Server.HtmlDecode(gvExistBoletas.Rows[i].Cells[1].Text).Trim();
                        newRow.Ticket = Server.HtmlDecode(gvExistBoletas.Rows[i].Cells[2].Text).Trim();
                        newRow.Producto = gvExistBoletas.Rows[i].Cells[3].Text;
                        newRow.pesonetoentrada = double.Parse(gvExistBoletas.Rows[i].Cells[4].Text);
                        newRow.boletaID = int.Parse(gvExistBoletas.Rows[i].Cells[5].Text);

                        /////calculate totales
                        double KG = (double)newRow.pesonetoentrada;

                        newRow.applyHumedad = true;
                        newRow.dctoHumedad = Utils.getDesctoHumedad(newRow.humedad, KG);
                        double fImpurezas = 0;

                        newRow.impurezas = 0f;
                        newRow.applyImpurezas = true;
                        newRow.dctoImpurezas = (decimal)Utils.getDesctoImpurezas(fImpurezas, KG);
                        newRow.applySecado = true;
                        newRow.dctoSecado = Utils.getDesctoSecado(newRow.humedad, KG);
                        newRow.pesonetoapagar = KG - (newRow.applyHumedad ? Utils.getDesctoHumedad(newRow.humedad, KG) : 0) - (newRow.applyImpurezas ? Utils.getDesctoImpurezas(fImpurezas, KG) : 0);
                        double fPrecio = 0;
                        newRow.precioapagar = 0;
                        double fKgNetos = 0;
                        fKgNetos = newRow.pesonetoapagar;
                        newRow.importe = (decimal)(fKgNetos * fPrecio);
                        newRow.totalapagar = (fKgNetos * fPrecio) - (newRow.applySecado ? Utils.getDesctoSecado(newRow.humedad, KG) : 0);

                        //////


                        dsBoletas.dtBoletasRow rowToAdd = (dsBoletas.dtBoletasRow )dtExist.Rows[i];
                        rowToAdd.dctoHumedad = Utils.getDesctoHumedad(rowToAdd.humedad, rowToAdd.pesonetoentrada);
                        rowToAdd.dctoImpurezas = (decimal)Utils.getDesctoImpurezas(rowToAdd.impurezas, rowToAdd.pesonetoentrada);
                        rowToAdd.pesonetoapagar = rowToAdd.pesonetoentrada - rowToAdd.dctoHumedad - (double)rowToAdd.dctoImpurezas;
                        rowToAdd.importe = (decimal)((rowToAdd.pesonetoapagar * (double)rowToAdd.precioapagar));
                        rowToAdd.dctoSecado = Utils.getDesctoSecado(rowToAdd.humedad, rowToAdd.pesonetoentrada);
                        rowToAdd.totalapagar = (double)rowToAdd.importe - rowToAdd.dctoSecado;


                        //this.dtBoletas.Rows.Add(newRow);
                        this.dtBoletas.ImportRow(rowToAdd);
                        this.traeAnticipos(newRow.boletaID, newRow.Ticket);
                        this.updateSaldosBoletas();
                    }

                }
            }
            this.Session[this.sSessiondtBoletas] = dtBoletas;
            this.gvBoletas.DataSource = this.dtBoletas;
            this.gvBoletas.DataBind();
            this.updateFilterExistBoletas();
            string producto = this.gvBoletas.DataKeys[0]["Producto"].ToString();
            if (producto.IndexOf("BLANCO") > 0 || producto.IndexOf("AMARILLO") > 0)
            {
                seleccionacatalogomaiz("MAIZ");
            }

        }

        protected void gvBoletas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
        protected void seleccionacatalogomaiz(string tipo){
          
            if (this.gvBoletas.Rows.Count > 0)
            {
                switch (tipo){
                    case "MAIZ":
                
                        this.drpdlGrupoCatalogosCajaChica.SelectedValue = "2";
                        this.drpdlGrupoCatalogosCajaChica.DataBind();
                        this.drpdlCatalogocuentaCajaChica.DataBind();

                        this.drpdlCatalogocuentaCajaChica.SelectedValue = "13";
                        //                     this.drpdlCatalogocuentaCajaChica.DataBind();


                        this.drpdlGrupoCuentaFiscal.SelectedValue = "2";
                        this.drpdlGrupoCuentaFiscal.DataBind();
                        this.drpdlCatalogocuentafiscalPago.DataBind();

                        // 
                        this.drpdlCatalogocuentafiscalPago.SelectedValue = "13";
                        //                     this.drpdlCatalogocuentafiscalPago.DataBind();
                        // 

                        this.drpdlGrupoCatalogosInternaPago.SelectedValue = "2";
                        this.drpdlGrupoCatalogosInternaPago.DataBind();
                        this.drpdlCatalogoInternoPago.DataBind();
                        // 
                        this.drpdlCatalogoInternoPago.SelectedValue = "13";
                        //                     this.drpdlCatalogoInternoPago.DataBind();
                        // 
                        //
                    break;
                }     

               
            }
           
        }

        protected void gvBoletas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvBoletas.EditIndex = e.NewEditIndex;
            this.gvBoletas.DataBind();


            DropDownList drpProducto = ((DropDownList)(this.gvBoletas.Rows[e.NewEditIndex].Cells[17].FindControl("drpdlProductoupBoletas")));
            if (drpProducto != null)
            {
                drpProducto.SelectedValue = this.gvBoletas.DataKeys[e.NewEditIndex]["productoID"].ToString();
            }

            DropDownList drpBodega = ((DropDownList)(this.gvBoletas.Rows[e.NewEditIndex].Cells[21].FindControl("drpdlBodega")));
            if (drpBodega != null)
            {
                drpBodega.SelectedValue = this.gvBoletas.DataKeys[e.NewEditIndex]["bodegaID"].ToString();
            }
            TextBox txtKilos = ((TextBox)(this.gvBoletas.Rows[e.NewEditIndex].Cells[4].FindControl("txtKilos")));
            if(double.Parse(this.gvBoletas.DataKeys[e.NewEditIndex]["PesoDeEntrada"].ToString()) <= 0 && double.Parse(this.gvBoletas.DataKeys[e.NewEditIndex]["PesoDeEntrada"].ToString()) <= 0) {
                txtKilos.ReadOnly = false;
            }
            else{
                txtKilos.ReadOnly = true;
            }

        }

        protected void gvBoletas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvBoletas.EditIndex = -1;
            this.gvBoletas.DataBind();
        }

        protected void updateTotales()
        {
            double fTotalBoletas = 0;
            double fTotalPagos = 0;
            double fTotalFinal = 0;
            double fTotalNotas = 0;
            double fTotalIntereses = 0;
            double fTotalSeguro = 0;
            double fTotalAnticipos =0;
            double fTotalKilos=0, fTotalDctoHumedad=0, fTotalDctoImpurezas=0, fTotalKgNetos=0, fTotalImporte=0, fTotalDctoSecado=0, fTotalaPagar=0;
            
            if (dtBoletas != null)
            {
                foreach (dsBoletas.dtBoletasRow row in this.dtBoletas.Rows)
                {
                    fTotalBoletas +=row.totalapagar;
                }
            }
        
            if (this.gvBoletas.FooterRow != null)
            {
                this.gvBoletas.DataBind();
                this.gvBoletas.FooterRow.Cells[2].Text = "TOTALES"; 
                
                string auxparafooter;
                auxparafooter = dtBoletas.Compute("sum(pesonetoentrada)", "").ToString();
                this.gvBoletas.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                this.gvBoletas.FooterRow.Cells[4].Text = auxparafooter.Length > 0 ? string.Format("{0:N2}", double.Parse(auxparafooter)) : "0.00";
                auxparafooter = dtBoletas.Compute("sum(dctoHumedad)", "").ToString();
                this.gvBoletas.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                this.gvBoletas.FooterRow.Cells[7].Text = auxparafooter.Length > 0 ? string.Format("{0:N2}", double.Parse(auxparafooter)) : "0.00";
                auxparafooter = dtBoletas.Compute("sum(dctoImpurezas)", "").ToString();
                this.gvBoletas.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                this.gvBoletas.FooterRow.Cells[9].Text = auxparafooter.Length > 0 ? string.Format("{0:N2}", double.Parse(auxparafooter)) : "0.00";
                auxparafooter = dtBoletas.Compute("sum(pesonetoapagar)", "").ToString();
                this.gvBoletas.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                this.gvBoletas.FooterRow.Cells[10].Text = auxparafooter.Length > 0 ? string.Format("{0:N2}", double.Parse(auxparafooter)) : "0.00";
                auxparafooter = dtBoletas.Compute("sum(importe)", "").ToString();
                this.gvBoletas.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                this.gvBoletas.FooterRow.Cells[12].Text = auxparafooter.Length > 0 ? string.Format("{0:C2}", double.Parse(auxparafooter,NumberStyles.Currency)) : "$ 0.00";
                auxparafooter = dtBoletas.Compute("sum(dctoSecado)", "").ToString();
                this.gvBoletas.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                this.gvBoletas.FooterRow.Cells[13].Text = auxparafooter.Length > 0 ? string.Format("{0:C2}", double.Parse(auxparafooter, NumberStyles.Currency)) : "$ 0.00";
                auxparafooter = dtBoletas.Compute("sum(totalapagar)", "").ToString();
                this.gvBoletas.FooterRow.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                this.gvBoletas.FooterRow.Cells[14].Text = auxparafooter.Length > 0 ? string.Format("{0:C2}", double.Parse(auxparafooter, NumberStyles.Currency)) : "$ 0.00";
                
            
            
            }

            

             
           // auxparafooter = dtBoletas.Compute("sum(pesonetoentrada)", "").ToString();


            this.dsPagos = this.DsPAGOS;
            
                foreach (dsLiquidacion.dtPagosRow row in this.dsPagos.Tables[0].Rows)
                {
                    fTotalPagos += ((row.IsmontoNull()?0:row.monto) + (row.IsEfectivoNull()? 0: row.Efectivo));


                }
            foreach (GridViewRow row in gvAnticipos.Rows)
            {
                try
                {
                    fTotalAnticipos += Utils.GetSafeFloat(row.Cells[6].Text);
                }
                catch {}
                try
                {
                     fTotalAnticipos += Utils.GetSafeFloat(row.Cells[7].Text);
                }
                catch { }
            }

        
            
            fTotalNotas = Utils.GetSafeFloat(this.txtTotalNotas.Text);
            fTotalIntereses = Utils.GetSafeFloat(this.txtIntereses.Text);
            fTotalSeguro = Utils.GetSafeFloat(this.txtSeguro.Text);

          
            fTotalFinal = fTotalBoletas - fTotalAnticipos - fTotalNotas - fTotalIntereses - fTotalSeguro - fTotalPagos;
            fTotalFinal = fTotalFinal < 0 ? fTotalFinal = 0 : fTotalFinal;
            this.lblTotalAPagar.Text = string.Format("{0:C2}", (decimal)fTotalBoletas);
            this.lblPagos.Text = string.Format("{0:C2}", fTotalPagos);
            this.lblAnticipos.Text = string.Format("{0:C2}", fTotalAnticipos);
            this.lblTotalFinal.Text = string.Format("{0:C2}", fTotalFinal);
            
           
        }

        protected void gvBoletas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = this.gvBoletas.Rows[e.RowIndex];

            int i = 1, iTicket = 0, iHumedad = 0, iPrecio = 0, iSecado = 0, iImpurezas = -1;

            foreach (DataControlField col in this.gvBoletas.Columns)
            {
                switch (col.HeaderText)
                {
                    case "TICKET":
                        iTicket = i;
                	break;
                    case "HUM.":
                        iHumedad = i;
                    break;
                    case "PRECIO (por KG)":
                        iPrecio = i;
                        break;
                    case "DSCTO SECADO":
                        iSecado = i;
                        break;
                    case "IMPUREZAS":
                        iImpurezas = i;
                        break;
                }
                if (col.Visible)
                {
                    i++;
                }
            }
            CheckBox chkHumedad = (CheckBox)row.FindControl("chkApplyHumABol");
            CheckBox chkImpurezas = (CheckBox)row.FindControl("chkApplyImpABol");
            CheckBox chkSecado = (CheckBox)row.FindControl("chkApplySecadoABoleta");

            dsBoletas.dtBoletasRow rowBoleta = (dsBoletas.dtBoletasRow)this.dtBoletas.Rows[e.RowIndex];
            rowBoleta.applyHumedad = chkHumedad.Checked;
            rowBoleta.applyImpurezas = chkImpurezas.Checked;
            rowBoleta.applySecado = chkSecado.Checked;

            this.dtBoletas.Rows[e.RowIndex]["pesonetoentrada"] = double.Parse(((TextBox)(this.gvBoletas.Rows[e.RowIndex].Cells[4].FindControl("txtKilos"))).Text);
            
            this.dtBoletas.Rows[e.RowIndex]["Ticket"] = ((TextBox)row.Cells[3].Controls[0]).Text;
            //this.dtBoletas.Rows[e.RowIndex]["Placas"] = ((TextBox)row.Cells[7].Controls[0]).Text;
            double KG =0;
            if (double.TryParse(this.dtBoletas.Rows[e.RowIndex]["pesonetoentrada"].ToString(), out KG) && KG <= 0)
            {
                if (!double.TryParse(this.dtBoletas.Rows[e.RowIndex]["pesonetosalida"].ToString(), out KG))
                    KG = 0;
            }
            this.dtBoletas.Rows[e.RowIndex]["humedad"] = double.Parse(((TextBox)row.Cells[6].Controls[0]).Text);
            this.dtBoletas.Rows[e.RowIndex]["dctoHumedad"] = chkHumedad.Checked ? Utils.getDesctoHumedad(double.Parse(((TextBox)row.Cells[6].Controls[0]).Text), KG) : 0;
            double fImpurezas = 0;
//             if(this.gvBoletas.Columns[8].Visible)
            if (!double.TryParse(((TextBox)row.Cells[8].Controls[0]).Text, out fImpurezas))
                fImpurezas = 0;
            this.dtBoletas.Rows[e.RowIndex]["impurezas"] = fImpurezas;
            this.dtBoletas.Rows[e.RowIndex]["dctoImpurezas"] = chkImpurezas.Checked ? Utils.getDesctoImpurezas(fImpurezas, KG) : 0;
            this.dtBoletas.Rows[e.RowIndex]["dctoSecado"] = chkSecado.Checked ? Utils.getDesctoSecado(double.Parse(((TextBox)row.Cells[6].Controls[0]).Text), KG) : 0;
            double pesonetoapagar = 0;
            pesonetoapagar = KG;
            pesonetoapagar -= chkHumedad.Checked ? Utils.getDesctoHumedad(double.Parse(((TextBox)row.Cells[6].Controls[0]).Text),KG) : 0;
            pesonetoapagar -= chkImpurezas.Checked ? Utils.getDesctoImpurezas(fImpurezas, KG) : 0;
            this.dtBoletas.Rows[e.RowIndex]["pesonetoapagar"] = pesonetoapagar;
            this.dtBoletas.Rows[e.RowIndex]["productoID"] = int.Parse(((DropDownList)(this.gvBoletas.Rows[e.RowIndex].Cells[17].FindControl("drpdlProductoupBoletas"))).SelectedValue);
            this.dtBoletas.Rows[e.RowIndex]["bodegaID"] = int.Parse(((DropDownList)(this.gvBoletas.Rows[e.RowIndex].Cells[21].FindControl("drpdlBodega"))).SelectedValue);
             this.dtBoletas.Rows[e.RowIndex]["Producto"] = ((DropDownList)(this.gvBoletas.Rows[e.RowIndex].Cells[17].FindControl("drpdlProductoupBoletas"))).SelectedItem.Text;
             this.dtBoletas.Rows[e.RowIndex]["Bodega"] = ((DropDownList)(this.gvBoletas.Rows[e.RowIndex].Cells[21].FindControl("drpdlBodega"))).SelectedItem.Text;
            double fPrecio = 0; 
            double.TryParse(((TextBox)row.Cells[11].Controls[0]).Text, out fPrecio);
            this.dtBoletas.Rows[e.RowIndex]["precioapagar"] = fPrecio;
            double fKgNetos = 0;
            double.TryParse(this.dtBoletas.Rows[e.RowIndex]["pesonetoapagar"].ToString(), out fKgNetos);
            this.dtBoletas.Rows[e.RowIndex]["importe"] =( fKgNetos* fPrecio);
            double descuentosecado = 0;

            if (chkSecado.Checked)
                descuentosecado = Utils.getDesctoSecado(double.Parse(((TextBox)row.Cells[6].Controls[0]).Text),KG);

            this.dtBoletas.Rows[e.RowIndex]["totalapagar"] = (fKgNetos * fPrecio) - descuentosecado;
            this.Session[this.sSessiondtBoletas] = dtBoletas;
            this.gvBoletas.EditIndex = -1;
            //this.gvBoletas.DataBind();

            this.updateSaldosBoletas();
            this.gvBoletas.DataBind();
            //this.updateTotales();
        }

        protected void updateSaldosBoletas()
        {
            foreach(GridViewRow row in this.gvBoletas.Rows)
            {
                double KG = 0;
                if (double.TryParse(this.dtBoletas.Rows[row.RowIndex]["pesonetoentrada"].ToString(), out KG) && KG <= 0)
                {
                    if (!double.TryParse(this.dtBoletas.Rows[row.RowIndex]["pesonetosalida"].ToString(), out KG))
                        KG = 0;
                }
                dsBoletas.dtBoletasRow rowBoleta = (dsBoletas.dtBoletasRow)this.dtBoletas.Rows[row.RowIndex];
                this.dtBoletas.Rows[row.RowIndex]["dctoHumedad"] = rowBoleta.applyHumedad ? Utils.getDesctoHumedad(double.Parse(this.dtBoletas.Rows[row.RowIndex]["humedad"].ToString()), KG) : 0;
                double fImpurezas = 0;
                double.TryParse(this.dtBoletas.Rows[row.RowIndex]["impurezas"].ToString(),out fImpurezas);
                this.dtBoletas.Rows[row.RowIndex]["dctoImpurezas"] = rowBoleta.applyImpurezas ? Utils.getDesctoImpurezas(fImpurezas, KG) : 0;
                this.dtBoletas.Rows[row.RowIndex]["dctoSecado"] = rowBoleta.applySecado ? Utils.getDesctoSecado(double.Parse(this.dtBoletas.Rows[row.RowIndex]["humedad"].ToString()), KG) : 0;
                this.dtBoletas.Rows[row.RowIndex]["pesonetoapagar"] = KG - (rowBoleta.applyHumedad ? Utils.getDesctoHumedad(double.Parse(this.dtBoletas.Rows[row.RowIndex]["humedad"].ToString()), KG) :0 ) - (rowBoleta.applyImpurezas ? Utils.getDesctoImpurezas(fImpurezas, KG) : 0);
                double fPrecio = 0;
                double.TryParse(this.dtBoletas.Rows[row.RowIndex]["precioapagar"].ToString(), out fPrecio);
                double fKgNetos = 0;
                double.TryParse(this.dtBoletas.Rows[row.RowIndex]["pesonetoapagar"].ToString(), out fKgNetos);
                this.dtBoletas.Rows[row.RowIndex]["importe"] = (fKgNetos * fPrecio);
                this.dtBoletas.Rows[row.RowIndex]["totalapagar"] = (fKgNetos * fPrecio) - (rowBoleta.applySecado ? Utils.getDesctoSecado(double.Parse(this.dtBoletas.Rows[row.RowIndex]["humedad"].ToString()), KG) : 0);
            }
            this.Session[this.sSessiondtBoletas] = dtBoletas;
            this.gvBoletas.DataBind();
            this.updateTotales();
        }
        protected void btnAddNewBoleta_Click(object sender, EventArgs e)
        {

           // start = new TimeSpan(Utils.Now.Ticks);
            if (dbFunctions.BoletaAlreadyExists(-1,this.txtNewNumBoleta.Text,this.txtNewTicket.Text ))
            {
                this.pnlNewBoleta.Visible = true;
                this.lblNewBoletaResult.Text = "NO SE PUEDE AGREGAR LA BOLETA, YA SE ENCUENTRA EN EL SISTEMA";
                this.imgBien.Visible = false;
                this.imgMal.Visible = true;
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
	            newRow.Ticket =  this.txtNewTicket.Text;
	            newRow.bodegaID = int.Parse(this.ddlNewBoletaBodega.SelectedValue);
                newRow.bodega = this.ddlNewBoletaBodega.SelectedItem.Text;
                newRow.applySecado = newRow.applyImpurezas = newRow.applyHumedad = true;

                newRow.cicloID = int.Parse(this.ddlCiclo.SelectedItem.Value);
	
	            DateTime dtFechaEntrada = new DateTime();
	            if (!DateTime.TryParse(this.txtNewFechaEntrada.Text /*+ " " + this.txtNewHoraEntrada.Text*/, out dtFechaEntrada))
	            {
	                /*DateTime.TryParse(this.txtNewFechaEntrada.Text, out dtFechaEntrada);*/
                    dtFechaEntrada = Utils.Now;
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
                        //DateTime.TryParse(this.txtNewFechaSalida.Text, out dtFechaSalida);
                        dtFechaSalida = Utils.Now;
                    }
                }
                else
                    dtFechaSalida = dtFechaEntrada;
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
	            newRow.humedad      = dHumedad;
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
                addComm.Parameters.Add("@NombreProductor", SqlDbType.VarChar).Value = newRow.NombreProductor;
                //addComm.Parameters.AddWithValue("@NombreProductor", newRow.NombreProductor);
                addComm.Parameters.Add("@NumeroBoleta", SqlDbType.VarChar).Value = newRow.NumeroBoleta;
                //addComm.Parameters.AddWithValue("@NumeroBoleta", newRow.NumeroBoleta);
                //  addComm.Parameters.AddWithValue("@Ticket", newRow.Ticket);
                addComm.Parameters.Add("@Ticket", SqlDbType.VarChar).Value = newRow.Ticket;
                //  addComm.Parameters.AddWithValue("@bodegaID", newRow.bodegaID);
                addComm.Parameters.Add("@bodegaID", SqlDbType.Int).Value = newRow.bodegaID;
                //  addComm.Parameters.AddWithValue("@cicloID", newRow.cicloID);
                addComm.Parameters.Add("@cicloID", SqlDbType.Int).Value = newRow.cicloID;
                //  addComm.Parameters.AddWithValue("@FechaEntrada", newRow.FechaEntrada);
                addComm.Parameters.Add("@FechaEntrada", SqlDbType.DateTime).Value = newRow.FechaEntrada;
                addComm.Parameters.Add("@PesoDeEntrada", SqlDbType.Float).Value = newRow.PesoDeEntrada;
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

                this.dtBoletas.ImportRow(newRow);
                this.Session[this.sSessiondtBoletas] = dtBoletas;
                
                this.gvBoletas.DataBind();

                this.updateSaldosBoletas();
                this.traeAnticipos(newRow.boletaID, newRow.Ticket);

                string producto = newRow.Producto;// this.gvBoletas.DataKeys[0]["Producto"].ToString();
                if (producto.IndexOf("BLANCO") > 0 || producto.IndexOf("AMARILLO") > 0)
                {
                    seleccionacatalogomaiz("MAIZ");
                }

                this.ddlNewBoletaProductor.SelectedValue = this.ddlProductor.SelectedValue;
                this.txtNewNumBoleta.Text = "";
                this.txtNewTicket.Text = "";
                this.ddlNewBoletaProducto.SelectedIndex = 0;
                
                this.txtNewFechaEntrada.Text = this.txtNewFechaSalida.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.txtPesoNetoNewBoleta.Text = this.txtNewPesoEntrada.Text = this.txtNewPesoSalida.Text = "0";
                this.txtNewSecado.Text = this.txtNewHumedad.Text = this.txtNewImpurezas.Text = this.txtNewPrecio.Text = "0";

                

                this.ddlNewBoletaBodega.DataBind();
                this.ddlNewBoletaBodega.SelectedValue = this.BodegaID.ToString();
        	
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error agregando boleta", this.Request.Url.ToString(), ref ex);
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(Session["USERID"].ToString()), "Error Insertando Nueva Boleta EX:" + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                sqlConn.Close();
            }
            
          //  Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.SELECT, this.UserID, "EL TIEMPO QUE TARDÓ EN AGREGAR UNA BOLETA A LA LIQUIDACIÓN " + this.lblNumLiquidacion.Text + " FUE: " + stop.Subtract(start).TotalMilliseconds.ToString() + " MILISECONDS", this.Request.Url.ToString());
   

        }

        protected void chkAplicarImpureza_CheckedChanged(object sender, EventArgs e)
        {
            this.updateChkImpurezas();
        }

        protected void chkDescuentoSecado_CheckedChanged(object sender, EventArgs e)
        {
            this.updateChkDescSecado();
        }


        protected void LimpiaCampos()
        {
            this.ddlNewBoletaProductor.SelectedIndex = 0;
            this.txtNewNumBoleta.Text = "";
            this.txtNewTicket.Text = "";
            this.ddlNewBoletaBodega.SelectedIndex = 0;
            this.txtNewFechaEntrada.Text = "";
            this.txtNewPesoEntrada.Text = "";
            this.txtNewFechaSalida.Text = "";
            this.txtNewPesoSalida.Text = "";
            this.ddlNewBoletaProducto.SelectedIndex = 0;
            this.txtNewHumedad.Text = "";
            this.txtNewImpurezas.Text = "";
            this.txtNewSecado.Text = "";
            this.txtNewPrecio.Text = "";
        }

        protected void updateChkDescSecado()
        {
            foreach (DataControlField col in this.gvBoletas.Columns)
            {
                if (col.HeaderText.IndexOf("SECADO") > -1)
                {
                    //col.Visible = this.chkDescuentoSecado.Checked;
                }
            }
            this.gvBoletas.DataBind();
            this.updateSaldosBoletas();
        }

        protected void updateChkImpurezas()
        {
            foreach (DataControlField col in this.gvBoletas.Columns)
            {
                if (col.HeaderText.IndexOf("IMPUREZAS") > -1)
                {
                    //col.Visible = this.chkAplicarImpureza.Checked;
                }
            }
            this.gvBoletas.DataBind();
            this.updateSaldosBoletas();
        }
        protected void updateChkHumedad()
        {
            foreach (DataControlField col in this.gvBoletas.Columns)
            {
                if (col.HeaderText.IndexOf("HUM") > -1)
                {
                   // col.Visible = this.chkBosCalculaHumedad.Checked;
                }
            }
            this.gvBoletas.DataBind();
            this.updateSaldosBoletas();
        }

        protected void btnAgregarLiquidacion_Click(object sender, EventArgs e)
        {
            this.CargaProductor();
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.Connection = conn;
                sqlComm.CommandText = "INSERT INTO Liquidaciones (productorID, cicloID, userID, nombre, domicilio, poblacion, fecha, fechalarga) VALUES (@productorID,@cicloID,@userID,@nombre,@domicilio,@poblacion,@fecha,@fechalarga);";
                sqlComm.CommandText += "select liquidacionID = SCOPE_IDENTITY();";
                sqlComm.Parameters.AddWithValue("@productorID", this.txtProductorID.Text);
                sqlComm.Parameters.AddWithValue("@cicloID", this.ddlCiclo.SelectedItem.Value);
                sqlComm.Parameters.AddWithValue("@userID", int.Parse(Session["USERID"].ToString()));
                sqlComm.Parameters.AddWithValue("@nombre", this.txtNombre.Text);
                sqlComm.Parameters.AddWithValue("@domicilio", this.txtDomicilio.Text);
                this.txtPoblacion.Text = this.txtPoblacion.Text.Replace(',', ' ').Trim();
                sqlComm.Parameters.AddWithValue("@poblacion", this.txtPoblacion.Text);
                sqlComm.Parameters.Add("@fecha", SqlDbType.DateTime).Value= DateTime.Parse(this.txtFechaLiquidacion.Text);
                sqlComm.Parameters.AddWithValue("@fechalarga", this.txtFechaLuiquidacionLong.Text);
                int.TryParse(sqlComm.ExecuteScalar().ToString(), out this.iLiqID);
                this.lblNumLiquidacion.Text = this.txtLiquidacionID.Text = this.iLiqID.ToString();
                this.LoadLiqData();
                this.updatePanelBoletas.Visible = true;
                this.btnAgregarLiquidacion.Visible = false;
                this.btnVerificarAntesAdd.Visible = false;
                this.ddlCiclo.Enabled = false;
                this.ddlProductor.Enabled = false;
                this.chkAsignarAnticipos.Enabled = false;
                this.chkBoxTraeBoletas.Enabled = false;
                this.GridView1.Visible = false;
                this.updateFilterExistBoletas();
                if(this.chkAsignarAnticipos.Checked){
                    
                    string qrySacaAnticipos= "SELECT     Anticipos.anticipoID FROM Anticipos LEFT OUTER JOIN Liquidaciones_Anticipos ON Anticipos.anticipoID = Liquidaciones_Anticipos.Anticipos WHERE     (Liquidaciones_Anticipos.LiquidacionID IS NULL AND Anticipos.productorID = @productorID)";
                    SqlConnection conSacaAnticipos = new SqlConnection(myConfig.ConnectionInfo);
                    SqlCommand cmdSelAnticipos = new SqlCommand(qrySacaAnticipos, conSacaAnticipos);
                    conSacaAnticipos.Open();
                    cmdSelAnticipos.Parameters.Add("@productorID", SqlDbType.Int).Value = int.Parse(this.ddlProductor.SelectedValue);
                    SqlDataReader rdAnticipos = cmdSelAnticipos.ExecuteReader();
                    while(rdAnticipos.Read()){
                        //INSERTAMOS LOS DETALLES DE LIQ ANTICIPOS
                        SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                        string query = "insert into Liquidaciones_Anticipos (LiquidacionID, Anticipos) values (@liquidacionID, @anticipoID)";
                        SqlCommand cmdins = new SqlCommand(query, conGaribay);
                        conGaribay.Open();
                        cmdins.Parameters.Add("@liquidacionID", SqlDbType.Int).Value = iLiqID;
                        cmdins.Parameters.Add("@anticipoID", SqlDbType.Int).Value = int.Parse(rdAnticipos[0].ToString());
                        cmdins.ExecuteNonQuery();
                        conGaribay.Close();

                    }
                    conSacaAnticipos.Close();
                   

                }
                if(this.chkBoxTraeBoletas.Checked){
                    string qrySacaBoletas = "SELECT Boletas.boletaID FROM  Boletas LEFT OUTER JOIN Liquidaciones_Boletas ON Boletas.boletaID = Liquidaciones_Boletas.BoletaID WHERE     (Liquidaciones_Boletas.BoletaID IS NULL AND Boletas.productorID = @productorID) "; 
                    SqlConnection conSacaBoletas= new SqlConnection(myConfig.ConnectionInfo);
                    SqlCommand cmdSelBoletas = new SqlCommand(qrySacaBoletas, conSacaBoletas);
                    conSacaBoletas.Open();
                    cmdSelBoletas.Parameters.Add("@productorID", SqlDbType.Int).Value = int.Parse(this.ddlProductor.SelectedValue);
                    SqlDataReader rdBoletas = cmdSelBoletas.ExecuteReader();
                    while (rdBoletas.Read())
                    {
                        //INSERTAMOS LOS DETALLES DE LIQ BOLETAS
                        SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                        string query = "insert into Liquidaciones_Boletas (LiquidacionID, BoletaID) values (@liquidacionID, @boletaID)";
                        SqlCommand cmdins = new SqlCommand(query, conGaribay);
                        conGaribay.Open();
                        cmdins.Parameters.Add("@liquidacionID", SqlDbType.Int).Value = iLiqID;
                        cmdins.Parameters.Add("@boletaID", SqlDbType.Int).Value = int.Parse(rdBoletas[0].ToString());
                        cmdins.ExecuteNonQuery();
                        conGaribay.Close();

                    }
                    conSacaBoletas.Close();
                    this.LoadBoletaData();
                    this.updateSaldosBoletas();
                    this.updateTotales();
                    this.updateFilterExistBoletas();
                    this.seleccionavalorproductor(this.ddlProductor.SelectedValue, this.ddlProductor.SelectedItem.Text);
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(Session["USERID"].ToString()), "Error agregando liquidacion EX:" + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                conn.Close();
            }
          
        }

        private void UpdateProductorData()
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "UPDATE Productores SET domicilio = @domicilio, poblacion = @poblacion, municipio = @municipio where productorid = " + this.ddlProductor.SelectedValue;
                comm.Connection = conn;
                comm.Parameters.Add("@domicilio", SqlDbType.VarChar).Value = this.txtDomicilio.Text;
                String sPoblacion = "", sMunicipio = "";
                if (this.txtPoblacion.Text.IndexOf(",") <=0)
                {
                    sPoblacion = sMunicipio = this.txtPoblacion.Text;
                }
                else
                {
                    sPoblacion = this.txtPoblacion.Text.Substring(0, this.txtPoblacion.Text.IndexOf(","));
                    sMunicipio = this.txtPoblacion.Text.Substring(this.txtPoblacion.Text.IndexOf(",") + 1, this.txtPoblacion.Text.Length - this.txtPoblacion.Text.IndexOf(",") - 1).Trim();
                    if (sMunicipio.Trim().Length <= 0)
                    {
                        sMunicipio = sPoblacion;
                    }
                }
                comm.Parameters.Add("@poblacion", SqlDbType.VarChar).Value = sPoblacion;
                comm.Parameters.Add("@municipio", SqlDbType.VarChar).Value = sMunicipio;
                if (comm.ExecuteNonQuery() != 1)
                {
                    throw new Exception("No se pudieron actualizar los datos del productor update devolvió mas de un row modificado.");
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, "ERROR ACTUALIZANDO DATOS DE PRODUCTOR CON DATOS DE LA LIQUIDACION", this.Request.Url.ToString(), ref ex);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnSaveLiq_Click(object sender, EventArgs e)
        {

          //  start = new TimeSpan(Utils.Now.Ticks);
            this.updateTotales();
            SqlCommand sqlComm = new SqlCommand();
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlComm);
            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;

                sqlComm.CommandText = "SELECT * FROM Liquidaciones WHERE (LiquidacionID = @LIQUIDACIONID)";
                sqlComm.Parameters.Add("@LIQUIDACIONID", SqlDbType.Int).Value = this.iLiqID;

                SqlCommandBuilder sqlCB = new SqlCommandBuilder(sqlDA);

                this.dtBoletaData.Rows.Clear();
                sqlDA.Fill(this.dtBoletaData);

                //////////////////////////////////////////////////////////////////////////
                
                this.dtBoletaData.Rows[0]["cicloID"] = int.Parse(this.ddlCiclo.SelectedValue.ToString());
                //this.ddlProductor.SelectedValue = this.dtBoletaData.Rows[0]["productorID"].ToString();
//                 this.txtFechaLiquidacion.Text = DateTime.Parse(this.dtBoletaData.Rows[0]["fecha"].ToString()).ToString("dd/MM/yyyy");
//                 this.txtFechaLuiquidacionLong.Text = this.dtBoletaData.Rows[0]["fechalarga"].ToString();


                this.dtBoletaData.Rows[0]["fecha"] = this.txtFechaLiquidacion.Text;
                this.dtBoletaData.Rows[0]["fechalarga"] = this.txtFechaLuiquidacionLong.Text;
                this.dtBoletaData.Rows[0]["nombre"] = this.txtNombre.Text;
                this.dtBoletaData.Rows[0]["domicilio"] = this.txtDomicilio.Text;
                this.dtBoletaData.Rows[0]["poblacion"] = this.txtPoblacion.Text;
//                 this.dtBoletaData.Rows[0]["applyImpureza"] = this.chkAplicarImpureza.Checked;
//                 this.dtBoletaData.Rows[0]["applySecado"] = this.chkDescuentoSecado.Checked;
//                 this.dtBoletaData.Rows[0]["applyHumedad"] = this.chkBosCalculaHumedad.Checked;
                this.dtBoletaData.Rows[0]["pagosdata"] = this.GetXMLPagos();
                this.dtBoletaData.Rows[0]["subTotal"] = double.Parse(this.lblTotalAPagar.Text, NumberStyles.Currency);
                this.dtBoletaData.Rows[0]["anticipos"] = double.Parse(this.lblAnticipos.Text, NumberStyles.Currency);

                double fTotalNotas = 0, fTotalIntereses = 0, fTotalSeguro = 0;

                if (!double.TryParse(this.txtTotalNotas.Text, NumberStyles.Currency, null, out fTotalNotas))
                {

                    if (!double.TryParse(this.txtTotalNotas.Text, out fTotalNotas))
                    {
                        fTotalNotas = 0;
                    }
                }
                if(!double.TryParse(this.txtIntereses.Text,NumberStyles.Currency,null,out fTotalIntereses)){
                
                    if (!double.TryParse(this.txtIntereses.Text,out fTotalIntereses))
                    {
                        fTotalIntereses = 0;
                    }
                }

                if (!double.TryParse(this.txtSeguro.Text, NumberStyles.Currency, null, out fTotalSeguro))
                {

                    if (!double.TryParse(this.txtSeguro.Text, out fTotalSeguro))
                    {
                        fTotalSeguro = 0;
                    }
                }

                this.dtBoletaData.Rows[0]["notas"] = fTotalNotas;
                this.dtBoletaData.Rows[0]["intereses"] = fTotalIntereses;
                this.dtBoletaData.Rows[0]["seguro"] = fTotalSeguro;
                //////////////////////////////////////////////////////////////////////////

                if (this.dtBoletaData.GetChanges(DataRowState.Modified).Rows.Count > 0)
                {
                    if (sqlDA.Update(this.dtBoletaData.GetChanges(DataRowState.Modified)) == 1)
                    {
                        if (chkUpdateProductorData.Checked)
                        {
                            this.UpdateProductorData();
                        }
                        this.dtBoletas = (dsBoletas.dtBoletasDataTable)this.Session[this.sSessiondtBoletas];


                        //actualiza relacion Liq_Boleta
                        SqlCommand sqlCommLiqBol = new SqlCommand("SELECT LiquidacionID, BoletaID FROM Liquidaciones_Boletas WHERE LIQUIDACIONID = @LIQUIDACIONID");
                        sqlCommLiqBol.Parameters.Add("@LIQUIDACIONID", SqlDbType.Int).Value = int.Parse(this.dtBoletaData.Rows[0]["LiquidacionID"].ToString());
                        sqlCommLiqBol.Connection = new SqlConnection(myConfig.ConnectionInfo);
                        sqlCommLiqBol.Connection.Open();

                        SqlDataAdapter sqlDALiqBol = new SqlDataAdapter(sqlCommLiqBol);
                        SqlCommandBuilder sqlCBBol = new SqlCommandBuilder(sqlDALiqBol);
                        DataTable dtLiqBol = new DataTable();

                        sqlDALiqBol.Fill(dtLiqBol);

                        foreach (DataRow row in dtLiqBol.Rows)
                        {
                            row.Delete();
                        }
                        if (dtLiqBol.GetChanges(DataRowState.Deleted) != null && dtLiqBol.GetChanges(DataRowState.Deleted).Rows.Count >0)
                        {
                            sqlDALiqBol.Update(dtLiqBol.GetChanges(DataRowState.Deleted));
                        }
                        this.iLiqID = int.Parse(txtLiquidacionID.Text);
                        foreach (dsBoletas.dtBoletasRow row in this.dtBoletas.Rows)
                        {
                            dtLiqBol.Rows.Add(new object[2] { iLiqID, row.boletaID });
                        }
                        if (dtLiqBol.GetChanges(DataRowState.Added) != null && dtLiqBol.GetChanges(DataRowState.Added).Rows.Count > 0)
                        {
                            sqlDALiqBol.Update(dtLiqBol.GetChanges(DataRowState.Added));
                        }

                        //Actualiza datos de boletas.
                        SqlConnection BolUptconn = new SqlConnection(myConfig.ConnectionInfo);
                        
                        foreach (dsBoletas.dtBoletasRow row in this.dtBoletas.Rows)
                        {
                            try
                            {
                                BolUptconn.Open();
                                SqlCommand BolUpComm = new SqlCommand("UPDATE    Boletas SET productoID = @productoID, Ticket = @Ticket, "
                                + "pesonetoentrada = @pesonetoentrada, pesonetosalida = @pesonetosalida, humedad = @humedad, "
                                + "dctoHumedad = @dctoHumedad, impurezas = @impurezas, dctoImpurezas = @dctoImpurezas, "
                                + "dctoSecado = @dctoSecado, pesonetoapagar = @pesonetoapagar, precioapagar = @precioapagar, "
                                + " importe = @importe, totalapagar = @totalapagar, applyHumedad = @applyHumedad, applyImpurezas = @applyImpurezas, applySecado = @applySecado, bodegaID = @bodegaID where boletaID = @boletaID");
                                BolUpComm.Connection = BolUptconn;
                                BolUpComm.Parameters.Add("@productoID", SqlDbType.Int).Value = row.productoID;
                                BolUpComm.Parameters.Add("@Ticket", SqlDbType.VarChar).Value = row.Ticket;
                                BolUpComm.Parameters.Add("@pesonetoentrada",SqlDbType.Float).Value = row.pesonetoentrada;
                                BolUpComm.Parameters.Add("@pesonetosalida",SqlDbType.Float).Value = row.pesonetosalida;
                                BolUpComm.Parameters.Add("@humedad",SqlDbType.Float).Value = row.humedad;
                                BolUpComm.Parameters.Add("@dctoHumedad",SqlDbType.Float).Value = row.dctoHumedad;
                                BolUpComm.Parameters.Add("@impurezas",SqlDbType.Float).Value = row.impurezas;
                                BolUpComm.Parameters.Add("@dctoImpurezas",SqlDbType.Float).Value = row.dctoImpurezas;
                                BolUpComm.Parameters.Add("@dctoSecado",SqlDbType.Float).Value = row.dctoSecado;
                                BolUpComm.Parameters.Add("@pesonetoapagar",SqlDbType.Float).Value = row.pesonetoapagar;
                                BolUpComm.Parameters.Add("@precioapagar",SqlDbType.Float).Value = row.precioapagar;
                                BolUpComm.Parameters.Add("@importe",SqlDbType.Float).Value = row.importe;
                                BolUpComm.Parameters.Add("@totalapagar",SqlDbType.Float).Value = row.totalapagar;
                                BolUpComm.Parameters.Add("@applyHumedad", SqlDbType.Bit).Value = row.applyHumedad;
                                BolUpComm.Parameters.Add("@applyImpurezas", SqlDbType.Bit).Value = row.applyImpurezas;
                                BolUpComm.Parameters.Add("@applySecado", SqlDbType.Bit).Value = row.applySecado;
                                BolUpComm.Parameters.Add("@bodegaID", SqlDbType.Int).Value = row.bodegaID;

                                BolUpComm.Parameters.Add("@boletaID",SqlDbType.Float).Value = row.boletaID;
                                
                            

                                if (BolUpComm.ExecuteNonQuery() != 1)
                                {
                                    throw new Exception("ACTUALIZAR DATOS DE BOLETA NO DEVOLVIÓ QUE UNA SOLA BOLETA SE ACTUALIZÓ");
                                }
                            }
                            catch (System.Exception ex)
                            {
                                Logger.Instance.LogException(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, "Err Modify boleta: ", this.Request.Url.ToString(), ref ex);
                                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "Error actualizando Boleta EX:" + ex.Message, this.Request.Url.ToString());
                            }
                            finally
                            {
                                BolUptconn.Close();
                            }
                        }

                        //INSERTAMOS EL PAGOSDATA A LA LIQ
                        string sqlUpdateXmlPagos = "update Liquidaciones set pagosdata = @pagosdata where liquidacionID = @liqID";
                        SqlConnection conUpdatePagos = new SqlConnection(myConfig.ConnectionInfo);
                        SqlCommand cmdupdatepagos = new SqlCommand(sqlUpdateXmlPagos, conUpdatePagos);
                        try
                        {
                            conUpdatePagos.Open();
                            cmdupdatepagos.Parameters.Add("@liqID", SqlDbType.Int).Value = int.Parse(this.lblNumLiquidacion.Text);
                            cmdupdatepagos.Parameters.Add("@pagosdata", SqlDbType.Text).Value = this.GetXMLPagos();
                            if (cmdupdatepagos.ExecuteNonQuery() != 1)
                            {
                                throw new Exception("ACTUALIZAR PAGOS DE BOLETA NO DEVOLVIÓ QUE UNA SOLA LIQUIDACION SE ACTUALIZÓ");
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "Error actualizando liq", this.Request.Url.ToString(), ref ex);
                            Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "Error actualizando Liquidación. EX:" + ex.Message, this.Request.Url.ToString());

                        }
                        finally
                        {
                            conUpdatePagos.Close();
                        }
                        DateTime fecha = Utils.Now;
                        this.lblLastSaved.Text = "ULTIMA VEZ QUE FUE GUARDADA LA LIQUIDACIÓN: " + fecha.ToString("dddd, dd") + " de " + fecha.ToString("MMMM, yyyy HH:mm:ss");
                    }
                }
                else
                {
                    this.lblLastSaved.Text = "LA LIQUIDACIÓN NO TIENE MODIFICACIONES";
                }
            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "Error guardando liq", this.Request.Url.ToString(), ref ex);
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(Session["USERID"].ToString()), "Error actualizando liquidacion "+this.iLiqID.ToString()+" EX:" + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                sqlConn.Close();
            }

         //   stop = new TimeSpan(Utils.Now.Ticks);
          //  Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.SELECT, this.UserID, "EL TIEMPO QUE TARDÓ EN GUERDAR LA LIQUIDACIÓN " + this.lblNumLiquidacion.Text + " FUE: " + stop.Subtract(start).TotalMilliseconds.ToString() + " MILISECONDS", this.Request.Url.ToString());
    
        }

        protected void btnAsignarAnticipoaLiq_Click(object sender, EventArgs e)
        {
            this.btnSaveLiq_Click(null, null);
            String sQuery = "liqID=" + this.iLiqID.ToString();
            sQuery = Utils.GetEncriptedQueryString(sQuery);
            String strRedirect = "~/frmAsignaranticiposaLiq.aspx";
            strRedirect += sQuery;
            Response.Redirect(strRedirect, true);
        }

        protected void drpdlGrupoCuentaFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpdlCatalogocuentafiscal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpdlGrupoCatalogos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpdlCatalogoInterno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void btnRealizaLiq_Click(object sender, EventArgs e)
        {


          //  start = new TimeSpan(Utils.Now.Ticks);
            this.btnSaveLiq_Click(null, null); //save changes before anything :-)
           string error = "";
           dsLiquidacion.dtPagosDataTable dt = (dsLiquidacion.dtPagosDataTable)this.DsPAGOS.Tables[0];
            foreach(dsLiquidacion.dtPagosRow row in dt.Rows)
            {
                row.liquidacionID = this.iLiqID;
            }

            bool bHayErrors = false;
            String sError = "";
            foreach(dsBoletas.dtBoletasRow row in this.dtBoletas)
            {
                if (row.pesonetoentrada <= 0)
                {
                    bHayErrors = true;
                    sError += "BOLETA ID: " + row.boletaID.ToString() + " no tiene KG<br/>";
                }
                if (row.precioapagar <=0)
                {
                    bHayErrors = true;
                    sError += "BOLETA ID: " + row.boletaID.ToString() + " no tiene precio a pagar.<br/>";
                }
            }
            if(this.DsPAGOS.Tables[0].Rows.Count <= 0 &&  double.Parse(this.lblPagos.Text,NumberStyles.Currency)<=0 && double.Parse(this.txtTotalNotas.Text,NumberStyles.Currency)<=0 && double.Parse(this.txtIntereses.Text,NumberStyles.Currency)<=0 && double.Parse(this.txtSeguro.Text,NumberStyles.Currency)<=0 ){
                bHayErrors = true;
                sError = "DEBE EXISTIR ALGUNA CANTIDAD EN LOS CONCEPTOS NOTAS, SEGURO O PAGOS PARA PODER REALIZAR ESTA LIQUIDACION. POR FAVOR INDIQUE AL MENOS UNO DE ESTOS CONCEPTOS.";

            }

            if (!bHayErrors)
            {
                if (dbFunctions.realizaLiquidacion(int.Parse(this.lblNumLiquidacion.Text), ref dt, ref error))
                {
                    this.seejecutoliquidacion(true);
                    this.pnlLiquidacionResult.Visible = true;
                    this.imgLiquidacionMal.Visible = false;
                    this.imgLiquidacionBien.Visible = true;
                    this.UpdateAddNewPago.Visible = false;
                    this.chkMostrarAgregarPago.Checked = false;
                    this.chkMostrarAgregarPago.Enabled = false;
                    this.lblNewLiquidacionresult.Text = "LA LIQUIDACION FUE REALIZADA CORRECTAMENTE";
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.LIQUIDACIONES, Logger.typeUserActions.UPDATE, this.UserID, "EJECUTO LA LIQUIDACION No. " + this.lblNumLiquidacion.Text.ToString());
                    this.LoadPagosFromDB();
                    this.DsPAGOS = this.dsPagos;
                    this.gvPagosLiquidacion.DataSource = null;
                    this.gvPagosLiquidacion.DataSourceID = "";
                    this.gvPagosLiquidacion.DataSource = this.dsPagos.Tables[0];
                    this.btnPrintLiquidacion.Visible = true;
                    this.gvPagosLiquidacion.DataBind();
                    this.updateTotales();

                }
                else
                {
                    this.pnlLiquidacionResult.Visible = true;
                    this.imgLiquidacionMal.Visible = true;
                    this.imgLiquidacionBien.Visible = false;
                    this.lblNewLiquidacionresult.Text = "NO SE PUDO EJECUTAR LA LIQUIDACION. ERROR:" + error;
                }
            }
            else
            {
                this.pnlLiquidacionResult.Visible = true;
                this.imgLiquidacionMal.Visible = true;
                this.imgLiquidacionBien.Visible = false;
                this.lblNewLiquidacionresult.Text = sError;
            }

           // stop = new TimeSpan(Utils.Now.Ticks);
           // Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.SELECT, this.UserID, "EL TIEMPO QUE TARDÓ EN REALIZAR LA LIQUIDACIÓN " + this.lblNumLiquidacion.Text + " FUE: " + stop.Subtract(start).TotalMilliseconds.ToString() + " MILISECONDS", this.Request.Url.ToString());
    
           

        }
         protected void seejecutoliquidacion(bool seejecuto)
         {
            this.btnSaveLiq.Visible = !seejecuto;
            if(this.SecurityLevel==1 && seejecuto){
                this.btnDeshacer.Visible = true;
            }
            else{
                this.btnDeshacer.Visible = false;
            }
            this.btnPrintLiquidacion.Visible = seejecuto;
            this.btnRealizaLiq.Visible = !seejecuto;
            this.chkAgregarBoletas.Enabled = !seejecuto;
            //this.chkBosCalculaHumedad.Enabled = !seejecuto;
            this.chkMostrarAgregarPago.Enabled = !seejecuto;
            this.txtTotalNotas.Enabled = !seejecuto;
            this.txtIntereses.Enabled = !seejecuto;
            this.txtSeguro.Enabled = !seejecuto;
            this.gvBoletas.Columns[0].Visible = !seejecuto;
            this.btnAsignarAnticipoaLiq.Visible = !seejecuto;
//             this.chkAplicarImpureza.Enabled = !seejecuto;
//             this.chkDescuentoSecado.Enabled = !seejecuto;
            if (seejecuto)
            {
                this.gvPagosLiquidacion.Columns[0].Visible = false;
                this.pnlNewPago.Enabled = false;
                this.chkMostrarAgregarPago.Checked = false;
                this.chkMostrarAgregarPago.Enabled = false;
                string parameter, ventanatitle = "IMPRIMIR LIQUIDACION";
               // String pathArchivotemp = PdfCreator.printLiquidacion(0, PdfCreator.tamañoPapel.CARTA, PdfCreator.orientacionPapel.VERTICAL, ref this.gvBoletas, ref gvAnticipos, ref gvPagosLiquidacion);
                string datosaencriptar;
                datosaencriptar = "id=";
                datosaencriptar += this.lblNumLiquidacion.Text;
                datosaencriptar += "&";

                parameter = "javascript:url('";
                parameter += "frmLiquidacionEsqueleto.aspx?data=";
                parameter += Utils.encriptacadena(datosaencriptar);
                parameter += "', '";
                parameter += ventanatitle;
                parameter += "',200,200,300,300); return false;";
                this.btnPrintLiquidacion.Attributes.Add("onclick", parameter);
                try
                {

                    this.txtTotalNotas.Text = this.txtTotalNotas.Text.Length > 0 ? string.Format("{0:C2}", double.Parse(this.txtTotalNotas.Text)) : "$ 0.00";
                    this.txtSeguro.Text = this.txtSeguro.Text.Length > 0 ? string.Format("{0:C2}", double.Parse(this.txtSeguro.Text)) : "$ 0.00";
                    this.txtIntereses.Text = this.txtIntereses.Text.Length > 0 ? string.Format("{0:C2}", double.Parse(this.txtIntereses.Text)) : "$ 0.00";
                }
                catch(Exception ex){
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, this.UserID, "ERROR AL PARSEAR DATOS (NOTAS, SEGURO, INTERESES. EX FUE: " + ex.Message, this.Request.Url.ToString());

                }

                
            }
                
        }

         protected void btnAddPago_Click(object sender, EventArgs e)
         {
            // start = new TimeSpan(Utils.Now.Ticks);
           
             this.updateTotales();
             double fTotalBoletas = 0;
             double fTotalPagos = 0;
             double fTotalporPagar = 0;
             double fTotalNotas = 0;
             double fTotalAnticipos = 0;
             double fInteres = 0;
             double montopago;
             fTotalBoletas = Utils.GetSafeFloat(this.lblTotalAPagar.Text);
             fTotalPagos = Utils.GetSafeFloat(this.lblPagos.Text);
             fInteres = Utils.GetSafeFloat(this.txtIntereses.Text);
             fTotalNotas = Utils.GetSafeFloat(this.txtTotalNotas.Text);
             fTotalAnticipos = Utils.GetSafeFloat(this.lblAnticipos.Text);
             fTotalporPagar = fTotalBoletas - fTotalPagos - fTotalNotas - fTotalAnticipos - fInteres; 
             montopago = Utils.GetSafeFloat(this.txtMonto.Text);
             int cheque = 0;
             int ChequeNumber = -1;
             if (!(this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO") && this.cmbConceptomovBancoPago.SelectedItem.Text.ToUpper().Equals("CHEQUE") && (this.txtChequeNum.Text.Length <= 0 || !int.TryParse(this.txtChequeNum.Text, out ChequeNumber)))
             {
                 this.pnlNewPago.Visible = true;
                 this.imgBienPago.Visible = false;
                 this.imgMal.Visible = true;
                 this.lblNewPagoResult.Text = "EL NUMERO DE CHEQUE NO ES VALIDO";
                 return;
             }
             if (int.TryParse(this.txtChequeNum.Text, out cheque))
             {
                 if (!(this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO") && this.cmbConceptomovBancoPago.SelectedItem.Text.ToUpper().Equals("CHEQUE") && dbFunctions.ChequeAlreadyExists(cheque, this.cmbCuentaPago.SelectedValue))
                 {
                     this.pnlNewPago.Visible = true;
                     this.imgBienPago.Visible = false;
                     this.imgMal.Visible = false;
                     this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, EL CHEQUE YA ESTA EXISTE";
                     

                     return;

                 }
             }
             if (!(this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO") && this.cmbConceptomovBancoPago.SelectedItem != null && this.cmbConceptomovBancoPago.SelectedItem.Text.IndexOf("CHEQUE") > -1 && !numChequeValido(cheque, int.Parse(this.cmbCuentaPago.SelectedValue)))
             {
                     this.pnlNewPago.Visible = true;
                     this.imgBienPago.Visible = false;
                     this.imgMal.Visible = false;
                     this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, EL NUMERO DE CHEQUE NO CORRESPONDE A EL NUMERO DE CUENTA";
                     return;
             }
             if(fTotalporPagar<montopago){
                 this.pnlNewPago.Visible = true;
                 this.imgBienPago.Visible = false;
                 this.imgMal.Visible = true;
                 this.lblNewPagoResult.Text = "LA CANTIDAD DEL PAGO NO PUEDE SER MAYOR A LA CANTIDAD A PAGAR POR LA LIQUIDACION";
                 return;
             }
             
             try
             {
                 this.dsPagos = this.DsPAGOS;
                 dsLiquidacion.dtPagosRow newRow = (dsLiquidacion.dtPagosRow)this.dsPagos.Tables[0].NewRow();
                 newRow.productorID = int.Parse(this.cmbProductoresPago.SelectedValue);
                 newRow.productorNombre = this.cmbProductoresPago.SelectedItem.Text;
                 newRow.fecha = DateTime.Parse(this.txtFechaPago.Text);
                 newRow.esmovimientodecaja = this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO";
                 newRow.nombre = this.txtNombrePago.Text;
                 newRow.Efectivo = newRow.esmovimientodecaja ? double.Parse(this.txtMonto.Text) : 0.0;
                 newRow.conceptoID = newRow.esmovimientodecaja ? -1 : int.Parse(this.cmbConceptomovBancoPago.SelectedValue);
                 newRow.concepto = newRow.esmovimientodecaja ? "EFECTIVO" : this.cmbConceptomovBancoPago.SelectedItem.Text;
                 newRow.monto = !newRow.esmovimientodecaja ? double.Parse(this.txtMonto.Text) : 0.0;
                 newRow.bodegaID = newRow.esmovimientodecaja ? int.Parse(this.ddlPagosBodegas.SelectedValue) : -1;
                 newRow.cuentaID = !newRow.esmovimientodecaja ? int.Parse(this.cmbCuentaPago.SelectedValue) : -1;
                 newRow.Cuenta = !newRow.esmovimientodecaja ? this.cmbCuentaPago.SelectedItem.Text : "";
                 newRow.numCheque = !newRow.esmovimientodecaja ? this.txtChequeNum.Text.Length > 0 ? int.Parse(this.txtChequeNum.Text) : 0 : 0;
                 newRow.cicloID = newRow.esmovimientodecaja ? int.Parse(this.ddlCiclo.SelectedValue) : -1;
                 newRow.userID = this.UserID;
                 newRow.Observaciones = "PAGO POR LIQUIDACION";
                 newRow.catalogoCajaChica = newRow.esmovimientodecaja ? this.drpdlCatalogocuentaCajaChica.SelectedItem.Text : "";
                 newRow.catalogoCajaChicaID = newRow.esmovimientodecaja ? int.Parse(this.drpdlCatalogocuentaCajaChica.SelectedValue) : -1;
                 newRow.subCatalogoCajaChica = (newRow.esmovimientodecaja && this.drpdlSubcatalogoCajaChica.SelectedItem != null) ? this.drpdlSubcatalogoCajaChica.SelectedItem.Text : "";
                 newRow.subCatalogoCajaChicaID = (newRow.esmovimientodecaja && this.drpdlSubcatalogoCajaChica.SelectedItem != null) ? int.Parse(this.drpdlSubcatalogoCajaChica.SelectedValue) : -1;

                 newRow.catalogoInternoMovBanco = !newRow.esmovimientodecaja ? this.drpdlCatalogoInternoPago.SelectedItem.Text : "";
                 newRow.catalogoInternoMovBancoID = !newRow.esmovimientodecaja ? int.Parse(this.drpdlCatalogoInternoPago.SelectedValue) : -1;
                 newRow.subCatalogoInternoMovBanco = (!newRow.esmovimientodecaja && this.drpdlSubcatologointernaPago.SelectedItem != null) ? this.drpdlSubcatologointernaPago.SelectedItem.Text : "";
                 newRow.subCatalogoInternoMovBancoID = (!newRow.esmovimientodecaja && this.drpdlSubcatologointernaPago.SelectedItem != null) ? int.Parse(this.drpdlSubcatologointernaPago.SelectedValue) : -1;

                 newRow.catalogoFiscalMovBanco = !newRow.esmovimientodecaja ? this.drpdlCatalogocuentafiscalPago.SelectedItem.Text : "";
                 newRow.catalogoFiscalMovBancoID = !newRow.esmovimientodecaja ? int.Parse(this.drpdlCatalogocuentafiscalPago.SelectedValue) : -1;
                 newRow.subCatalogoFiscalMovBanco = (!newRow.esmovimientodecaja && this.drpdlSubcatalogofiscalPago.SelectedItem != null) ? this.drpdlSubcatalogofiscalPago.SelectedItem.Text : "";
                 newRow.subCatalogoFiscalMovBancoID = (!newRow.esmovimientodecaja && this.drpdlSubcatalogofiscalPago.SelectedItem != null) ? int.Parse(this.drpdlSubcatalogofiscalPago.SelectedValue) : -1;

                 newRow.facturaolarguillo = !newRow.esmovimientodecaja ? this.txtFacturaLarguillo.Text : "";
                 newRow.chequenombre = !newRow.esmovimientodecaja ? this.txtChequeNombre0.Text : "";

                 ((dsLiquidacion.dtPagosDataTable)this.dsPagos.Tables[0]).AdddtPagosRow(newRow);
                 this.DsPAGOS = this.dsPagos;
                 this.gvPagosLiquidacion.DataSource = null;
                 this.gvPagosLiquidacion.DataSourceID = "";
                 this.gvPagosLiquidacion.DataSource = this.dsPagos.Tables[0];
                 this.gvPagosLiquidacion.DataBind();
                 this.updateTotales();

                 //////////////////////////////////////////////////////////////////////////
                 //restart to default values
                 this.cmbProductoresPago.SelectedValue = this.ddlProductor.SelectedValue;
                 this.txtFechaPago.Text = Utils.Now.ToString("dd/MM/yyyy");
                 //this.cmbTipodeMovPago.SelectedIndex = 0;
                 //this.txtNombrePago.Text = "";
                 this.txtMonto.Text = "";
                 this.ddlPagosBodegas.SelectedIndex = 0;
                 //this.cmbCuentaPago.SelectedIndex = 0;
                 this.txtChequeNum.Text = "";
                 //this.ddlCiclo.SelectedIndex = 0;

                 /*
                 drpdlGrupoCuentaFiscal.SelectedIndex = 0;
                                  drpdlCatalogocuentafiscalPago.DataBind();
                                  drpdlCatalogocuentafiscalPago.SelectedIndex = 0;
                                  drpdlSubcatalogofiscalPago.DataBind();
                                  drpdlSubcatalogofiscalPago.SelectedIndex = 0;*/
                 

                 /*
                 drpdlGrupoCatalogosInternaPago.SelectedIndex = 0;
                                  drpdlCatalogoInternoPago.DataBind();
                                  drpdlCatalogoInternoPago.SelectedIndex = 0;
                                  drpdlSubcatologointernaPago.DataBind();
                                  drpdlSubcatologointernaPago.SelectedIndex = 0;*/
                 

                 
                 this.txtFacturaLarguillo.Text = "";
                 this.txtChequeNombre0.Text = "";
                 //////////////////////////////////////////////////////////////////////////
             }
             catch (Exception ex)
             {

             }

             this.txtFechaPago.Text = Utils.Now.ToString("dd/MM/yyyy");

//              stop = new TimeSpan(Utils.Now.Ticks);
//              Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.SELECT, this.UserID, "EL TIEMPO QUE TARDÓ EN AGREGAR UN PAGO A LA LIQUIDACIÓN " + this.lblNumLiquidacion.Text + " FUE: " + stop.Subtract(start).TotalMilliseconds.ToString() + " MILISECONDS", this.Request.Url.ToString());
   
         }

         protected void gvPagosLiquidacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
         {
             this.dsPagos = this.DsPAGOS;
             this.dsPagos.Tables[0].Rows.RemoveAt(e.RowIndex);
             this.gvPagosLiquidacion.DataSourceID = "";
             this.gvPagosLiquidacion.DataSource = this.dsPagos.Tables[0];
             this.gvPagosLiquidacion.DataBind();
             this.DsPAGOS = this.dsPagos;
             this.btnSaveLiq_Click(null, null);
         }

         protected void gvBoletas_RowDeleting(object sender, GridViewDeleteEventArgs e)
         {
             this.dtBoletas.Rows.RemoveAt(e.RowIndex);
             this.gvBoletas.DataBind();
             this.Session[this.sSessiondtBoletas] = dtBoletas;
             this.updateTotales();
         }

         protected void chkBosCalculaHumedad_CheckedChanged(object sender, EventArgs e)
         {
             //this.chkDescuentoSecado.Checked = this.chkBosCalculaHumedad.Checked;
             this.updateChkHumedad();
             this.updateChkDescSecado();
             
         }
        protected void traeAnticipos(int boletaID, string numTicket){
            string sql  = " SELECT     Boletas_Anticipos.anticipoID FROM         Boletas_Anticipos INNER JOIN ";
                   sql += " Anticipos ON Boletas_Anticipos.anticipoID = Anticipos.anticipoID LEFT OUTER JOIN ";
                   sql += " Liquidaciones_Anticipos ON Anticipos.anticipoID = Liquidaciones_Anticipos.Anticipos ";
                   sql += " WHERE     (Liquidaciones_Anticipos.LiquidacionID IS NULL AND (Boletas_Anticipos.boletaID = @boletaID OR Boletas_Anticipos.Ticket = @Ticket))";
            SqlConnection conTraeAnticipos = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdTraeAnticipos = new SqlCommand(sql, conTraeAnticipos);
            try{
                conTraeAnticipos.Open();
                cmdTraeAnticipos.Parameters.Clear();
                cmdTraeAnticipos.Parameters.Add("@boletaID",SqlDbType.Int).Value=boletaID;
                cmdTraeAnticipos.Parameters.Add("@Ticket", SqlDbType.NVarChar).Value = numTicket;
                SqlDataReader read = cmdTraeAnticipos.ExecuteReader();
                while(read.Read()){//INSERTAMOS EN DETALLE DE LIQ_ANTICIPO
                    string sqlinsert = "insert into Liquidaciones_Anticipos (LiquidacionID, Anticipos) values(@liquidacion,@anticipoID)";
                    SqlConnection conInsert = new SqlConnection(myConfig.ConnectionInfo);
                    SqlCommand cmdinsert = new SqlCommand(sqlinsert, conInsert);
                    conInsert.Open();
                    cmdinsert.Parameters.Clear();
                    cmdinsert.Parameters.Add("@liquidacion", SqlDbType.Int).Value=int.Parse(this.lblNumLiquidacion.Text);
                    cmdinsert.Parameters.Add("@anticipoID", SqlDbType.Int).Value = int.Parse(read[0].ToString());
                    cmdinsert.ExecuteNonQuery();
                    conInsert.Close();
                }
            }
            catch(Exception e){
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT,int.Parse(this.Session["userID"].ToString()),"ERROR AL TRAER ANTICIPOS A LA LIQUIDACION " + this.iLiqID.ToString() + ". LA EXC FUE: " + e.Message,this.Request.Url.ToString());
            }
            finally{
                conTraeAnticipos.Close();
            }
            this.gvAnticipos.DataBind();

        }
        protected void seleccionavalorproductor(string valuecombox, string nombre){
            //this.ddlProdBoletas.SelectedValue =  valuecombox;
            this.ddlNewBoletaProductor.SelectedValue = valuecombox;
            this.cmbProductoresPago.SelectedValue = valuecombox;
            this.txtNombrePago.Text = nombre;
            this.txtChequeNombre0.Text = nombre;

        }

     

        protected void btnDeshacer_Click1(object sender, EventArgs e)
        {

            string sError = "";
            //GUARDAMOS TODO ANTES
            this.btnSaveLiq_Click(null, null);
            if (dbFunctions.reverseLiquidacion(int.Parse(this.lblNumLiquidacion.Text), ref sError))
            {
                String sSubject, sContent;
                sSubject ="El usuario " + this.UserID + " ha deshecho la liquidacion " + this.iLiqID.ToString();
                sContent = "Este correo fue enviado por que se deshizo una liquidacion <br />";
                sContent += "Liquidacion: " + this.iLiqID.ToString() + "<BR />";
                sContent += "a Nombre de: " + this.txtNombre.Text;
                sContent += "por el total de:" + this.lblTotalFinal.Text;
                EMailUtils.SendTextEmail("patriciagaribay@corporativogaribay.com",sSubject,sContent, true);
                this.pnlLiquidacionResult.Visible = true;
                this.imgLiquidacionMal.Visible = false;
                this.imgLiquidacionBien.Visible = true;
                this.lblNewLiquidacionresult.Text = "LA LIQUIDACION FUE DESHECHA EXITOSAMENTE";
                this.seejecutoliquidacion(false);
                this.dsPagos = this.DsPAGOS;
                this.dsPagos.Tables[0].Clear();
                this.DsPAGOS = this.dsPagos;
                this.gvPagosLiquidacion.DataBind();
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.LIQUIDACIONES, Logger.typeUserActions.UPDATE, this.UserID, "DESHIZO LA LIQUIDACIÓN No. " + this.lblNumLiquidacion.Text);
                String sQuery = "liqID=" + this.lblNumLiquidacion.Text;
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = "~/frmLiquidacion2010.aspx";
                strRedirect += sQuery;
                this.txtTotalNotas.Text = double.Parse(this.txtTotalNotas.Text, NumberStyles.Currency).ToString();
                Response.Redirect(strRedirect, true);
            }
            else
            {
                this.pnlLiquidacionResult.Visible = true;
                this.imgLiquidacionMal.Visible = true;
                this.imgLiquidacionBien.Visible = false;
                this.lblNewLiquidacionresult.Text = "LA LIQUIDACION NO HA PODIDO SER DESHECHA. EL ERROR ES: " + sError;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "NO SE PUDO DESHACER LA LIQUIDACION No. " + this.lblNumLiquidacion.Text + ". EL ERROR FUE: " + sError, this.Request.Url.ToString());
            }
        }

        protected void ddlProductor_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.sdsLiquidacionesYaEnSistema.DataBind();
            this.GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }
            HyperLink link = (HyperLink)e.Row.Cells[4].FindControl("HyperLink1");
            if (link != null)
            {
                String sQuery = "liqID=" + e.Row.Cells[0].Text;
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = "~/frmLiquidacion2010.aspx";
                strRedirect += sQuery;
                link.NavigateUrl = strRedirect;
            }
        }

        protected void btnVerificarAntesAdd_Click(object sender, EventArgs e)
        {
            this.lblValidacionRes.Text = "";
            this.GridView1.Visible = true;
            this.sdsLiquidacionesYaEnSistema.DataBind();
            this.GridView1.DataBind();
            this.btnVerificarAntesAdd.Visible = false;
            this.btnAgregarLiquidacion.Visible = this.GridView1.Rows.Count == 0;
            if (this.GridView1.Rows.Count > 0)
            {
                this.lblValidacionRes.Text = "YA EXISTEN LIQUIDACIONES PENDIENTES DEL PRODUCTOR";
            }
        }

        protected void cmbTipodeMovPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvPagosLiquidacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
            
                HyperLink btnPrint;

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }
                btnPrint = (HyperLink)e.Row.FindControl("btnPrintCheque"); 
                if (btnPrint == null)
                {
                    return;
                }
                String celda = this.Server.HtmlDecode(e.Row.Cells[5].Text).Trim();
                btnPrint.Visible = celda.Length > 0 && this.btnPrintLiquidacion.Visible;

                if (btnPrint.Visible)
                {
                    this.dsPagos = this.DsPAGOS;
                    string parameter, ventanatitle = "IMPRIMIR CHEQUE";
                    // String pathArchivotemp = PdfCreator.printLiquidacion(0, PdfCreator.tamañoPapel.CARTA, PdfCreator.orientacionPapel.VERTICAL, ref this.gvBoletas, ref gvAnticipos, ref gvPagosLiquidacion);
                    string datosaencriptar;
                    datosaencriptar = "iMovID=";
                    datosaencriptar += this.dsPagos.Tables[0].Rows[e.Row.RowIndex]["movbanID"].ToString();
                    datosaencriptar += "&";

                    parameter = "javascript:url('";
                    parameter += "frmPrintCheque.aspx?data=";
                    parameter += Utils.encriptacadena(datosaencriptar);
                    parameter += "', '";
                    parameter += ventanatitle;
                    parameter += "',200,200,300,300); return false;";
                    btnPrint.Attributes.Add("onClick", parameter);
                    btnPrint.NavigateUrl = this.Request.Url.ToString();
                    //link.Visible = ((CheckBox)e.Row.Cells[8].Controls[0]).Checked;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.PRINT, "no se pudo generar el cheque para imprimir", this.Request.Url.ToString(), ref ex);
            }
        }

        protected void btnActualizaComboProductores_Click(object sender, EventArgs e)
        {
            this.ddlProductor.DataBind();
            this.cmbProductoresPago.DataBind();
            this.ddlProdBoletas.DataBind();
            this.ddlNewBoletaProductor.DataBind();
            
        }

        protected void btnAddQuickProductor_Click(object sender, EventArgs e)
        {

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
        
    }
}
