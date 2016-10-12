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

namespace Garibay
{
    public partial class frmLiquidacionEsqueletoaspx : Garibay.BasePage 
    {
        private DataTable dtBoletaData = null;
        private dsBoletas.dtBoletasDataTable dtBoletas = null;
        private dsLiquidacion.dtLiquidacionDataDataTable dtLiquidacionData = null;
        private dsLiquidacion.dtLiquidacionDataRow rowLiqData = null;
        int iLiqID = -1;
        private dsLiquidacion dsPagos = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack){
               
                if (Request.QueryString["data"] != null)
                {
                    if (this.loadqueryStrings(Request.QueryString["data"].ToString()))
                    {
                        
                        this.dtBoletas = new dsBoletas.dtBoletasDataTable();
                        this.iLiqID = int.Parse(myQueryStrings["id"].ToString());
                        if (this.LoadBoletaData())

                        {
                            float fBoletas = 0, fAnticipos = 0, /*fFetilizantes = 0,*/ fPagos = 0, fTemp = 0;

                            fBoletas = float.Parse(dtBoletas.Compute("SUM(totalapagar)", "").ToString());
                            rowLiqData.totalBoletas = fBoletas;
                            gvAnticipos.DataBind();
                            foreach(GridViewRow row in gvAnticipos.Rows)
                            {
                                fTemp = 0;
                                try
                                {
                                    fTemp = float.Parse(this.Server.HtmlDecode( row.Cells[6].Text), NumberStyles.Currency);
                                }
                                catch (System.Exception ex)
                                {
                                    fTemp = 0;	
                                }
                                fAnticipos += fTemp;
                                fTemp = 0;
                                try
                                {
                                    fTemp = float.Parse(this.Server.HtmlDecode(row.Cells[7].Text), NumberStyles.Currency);
                                }
                                catch (System.Exception ex)
                                {
                                    fTemp = 0;
                                }
                                
                                fAnticipos += fTemp;
                            }
                            rowLiqData.totalAnticipos = fAnticipos;
                            gvPagosLiquidacion.DataBind();
                            fPagos = 0;
                            foreach(GridViewRow row in gvPagosLiquidacion.Rows)
                            {
                                fTemp = 0;
                                try
                                {
                                    fTemp = float.Parse(this.Server.HtmlDecode(row.Cells[5].Text), NumberStyles.Currency);
                                }
                                catch (System.Exception ex)
                                {
                                    fTemp = 0;
                                }
                                fPagos += fTemp;
                                fTemp = 0;
                                try
                                {
                                    fTemp = float.Parse(this.Server.HtmlDecode(row.Cells[6].Text), NumberStyles.Currency);
                                }
                                catch (System.Exception ex)
                                {
                                    fTemp = 0;
                                }

                                fPagos += fTemp;
                            }
                            rowLiqData.totalPagos = fPagos;
                            //gvAnticipos.Columns[0].Visible = false;
                            this.gvBoletas.DataBind();

                            string auxparafooter;
                            auxparafooter = dtBoletas.Compute("sum(pesonetoentrada)", "").ToString();
                            this.gvBoletas.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                            this.gvBoletas.FooterRow.Cells[4].Text = auxparafooter.Length > 0 ? string.Format("{0:N2}", float.Parse(auxparafooter)) : "0.00";
                            auxparafooter = dtBoletas.Compute("sum(dctoHumedad)", "").ToString();
                            this.gvBoletas.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                            this.gvBoletas.FooterRow.Cells[7].Text = auxparafooter.Length > 0 ? string.Format("{0:N2}", float.Parse(auxparafooter)) : "0.00";
                            auxparafooter = dtBoletas.Compute("sum(dctoImpurezas)", "").ToString();
                            this.gvBoletas.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                            this.gvBoletas.FooterRow.Cells[9].Text = auxparafooter.Length > 0 ? string.Format("{0:N2}", float.Parse(auxparafooter)) : "0.00";
                            auxparafooter = dtBoletas.Compute("sum(pesonetoapagar)", "").ToString();
                            this.gvBoletas.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                            this.gvBoletas.FooterRow.Cells[10].Text = auxparafooter.Length > 0 ? string.Format("{0:N2}", float.Parse(auxparafooter)) : "0.00";
                            auxparafooter = dtBoletas.Compute("sum(importe)", "").ToString();
                            this.gvBoletas.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                            this.gvBoletas.FooterRow.Cells[12].Text = auxparafooter.Length > 0 ? string.Format("{0:C2}", float.Parse(auxparafooter, NumberStyles.Currency)) : "$ 0.00";
                            auxparafooter = dtBoletas.Compute("sum(dctoSecado)", "").ToString();
                            this.gvBoletas.FooterRow.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                            this.gvBoletas.FooterRow.Cells[13].Text = auxparafooter.Length > 0 ? string.Format("{0:C2}", float.Parse(auxparafooter, NumberStyles.Currency)) : "$ 0.00";
                            auxparafooter = dtBoletas.Compute("sum(totalapagar)", "").ToString();
                            this.gvBoletas.FooterRow.Cells[14].HorizontalAlign = HorizontalAlign.Right;
                            this.gvBoletas.FooterRow.Cells[14].Text = auxparafooter.Length > 0 ? string.Format("{0:C2}", float.Parse(auxparafooter, NumberStyles.Currency)) : "$ 0.00";


                            float[] fColSizeBoletas = new float[this.gvBoletas.Columns.Count];
                            fColSizeBoletas[0] = 0; //botones unused
                            fColSizeBoletas[1] = 0;
                            fColSizeBoletas[2] = 8; //boleta
                            fColSizeBoletas[3] = 8; //ticket
                            fColSizeBoletas[4] = 8; //KG
                            fColSizeBoletas[5] = 22; //producto
                            fColSizeBoletas[6] = 5; //HUM.
                            fColSizeBoletas[7] = 6; //decto hum.
                            fColSizeBoletas[8] = 5; //impurezas
                            fColSizeBoletas[9] = 6; //dcto impurezas
                            fColSizeBoletas[10] = 8; //kg netos
                            fColSizeBoletas[11] = 8; //precio
                            fColSizeBoletas[12] = 9; //importe
                            fColSizeBoletas[13] = 7; //secado
                            fColSizeBoletas[14] = 9; //importe
                            //fColSizeBoletas[15] = 10;
                            float[] fAnticiposColSize = new float[this.gvAnticipos.Columns.Count];
                            fAnticiposColSize[0] = 9;
                            fAnticiposColSize[1] = 4;
                            fAnticiposColSize[2] = 20;
                            fAnticiposColSize[3] = 15;
                            fAnticiposColSize[4] = 7;
                            fAnticiposColSize[5] = 7;
                            fAnticiposColSize[6] = 10;
                            fAnticiposColSize[7] = 10;
                            

                            float[] fPagosColSize = new float[this.gvPagosLiquidacion.Columns.Count];
                            fPagosColSize[0] = 9;
                            fPagosColSize[1] = 20;
                            fPagosColSize[2] = 15;
                            fPagosColSize[3] = 5;
                            fPagosColSize[4] = 6;
                            fPagosColSize[5] = 10;
                            fPagosColSize[6] = 10;

                            dvTotalesLiq.DataBind();
                            byte [] bytes =PdfCreator.printLiquidacion(iLiqID, PdfCreator.tamañoPapel.CARTA, PdfCreator.orientacionPapel.HORIZONTAL, ref this.gvBoletas, ref this.gvAnticipos, ref this.gvPagosLiquidacion, ref rowLiqData, fColSizeBoletas, fAnticiposColSize, fPagosColSize, dvTotalesLiq);
                            Logger.Instance.LogUserSessionRecord(Logger.typeModulo.LIQUIDACIONES, Logger.typeUserActions.PRINT, this.UserID, "IMPRIMIÓ LA LIQUIDACIÓN NÚMERO:  " + this.txtLiquidacionID.Text);
                            Response.ClearHeaders();
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("Content-Disposition", "attachment;filename=Liquidacion"+rowLiqData.nombre.ToUpper().Replace(" ","-")+".pdf");
                            Response.BinaryWrite(bytes);
                            Response.Flush();
                            Response.End();
                            
                           
                        }
                        else
                        {
                            this.panelEsqueleto.Visible = false;
                            this.pnlResult.Visible = true;
                            this.imgBienResult.Visible = false;
                            this.imgMalResult.Visible = true;
                            this.lblResult.Text = "HA OCURRIDO UN ERROR MIENTRAS SE INTENTABA CARGAR LOS DATOS DE LA LIQUIDACIÓN";
                        }
                        
                    }
                    else
                    {

                        this.panelEsqueleto.Visible = false;
                        this.pnlResult.Visible = true;
                        this.imgBienResult.Visible = false;
                        this.imgMalResult.Visible = true;
                        this.lblResult.Text = "HA OCURRIDO UN ERROR MIENTRAS SE INTENTABA CARGAR LOS DATOS DE LA LIQUIDACIÓN";

                    }
                }
            }

        }
        private void LoadXMLPagos(String sXML)
        {
            bool result = false;
           
            try
            {
                if (this.dsPagos == null)
                {
                    this.dsPagos = new dsLiquidacion();
                    this.dsPagos.Tables.Clear();
                }
                if (sXML != "")
                {
                    using (StringReader sr = new StringReader(sXML))
                    {
                        this.dsPagos.ReadXml(sr);

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
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(this.Session["userID"].ToString()), "ERROR CASTING XML TO PAGOS DATA EX:" + ex.Message, "LoadXMLPagos");
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
                this.dsPagos = new dsLiquidacion();
                this.dsPagos.Tables.Clear();
                this.dsPagos.Tables.Add(new dsLiquidacion.dtPagosDataTable());
                sqlDA.Fill(this.dsPagos.Tables[0]);
                this.gvPagosLiquidacion.DataSource = this.dsPagos.Tables[0];
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
        protected bool LoadBoletaData()
        {

            this.txtLiquidacionID.Text = this.iLiqID.ToString();
            SqlCommand sqlComm = new SqlCommand();
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlComm);
            bool bResult = true;
            this.dtLiquidacionData = new dsLiquidacion.dtLiquidacionDataDataTable();
            this.rowLiqData = this.dtLiquidacionData.NewdtLiquidacionDataRow();
            try
            {
                this.dtBoletaData = new DataTable();
                sqlConn.Open();
                sqlComm.Connection = sqlConn;

                sqlComm.CommandText = "SELECT Liquidaciones.LiquidacionID, Liquidaciones.productorID, Liquidaciones.cicloID, Liquidaciones.userID, Liquidaciones.nombre, Liquidaciones.domicilio,  Liquidaciones.poblacion, Liquidaciones.fecha, Liquidaciones.fechalarga, Liquidaciones.subTotal,  Liquidaciones.anticipos, Liquidaciones.total, Liquidaciones.cobrada, Liquidaciones.pagosData, Liquidaciones.userIdejecuto,  Liquidaciones.fechaEjecucion, Liquidaciones.notas, Liquidaciones.intereses, Liquidaciones.seguro, Users.Nombre AS EjecutadaPor FROM            Liquidaciones INNER JOIN Users ON Liquidaciones.userID = Users.userID WHERE (LiquidacionID = @LIQUIDACIONID)";
                sqlComm.Parameters.Add("@LIQUIDACIONID", SqlDbType.Int).Value = this.iLiqID;

                if (sqlDA.Fill(this.dtBoletaData) == 1)
                {
                    
                   
                    this.rowLiqData.cicloID = int.Parse(dtBoletaData.Rows[0]["cicloID"].ToString());
                    this.rowLiqData.productorID = int.Parse(this.dtBoletaData.Rows[0]["productorID"].ToString());
                    this.rowLiqData.fecha = DateTime.Parse(this.dtBoletaData.Rows[0]["fecha"].ToString());
                    this.rowLiqData.fechalarga = this.dtBoletaData.Rows[0]["fechalarga"].ToString();
                    this.rowLiqData.pagosdata = this.dtBoletaData.Rows[0]["pagosData"].ToString();
                    //this.LoadXMLPagos(rowLiqData.pagosdata);
                    this.LoadPagosFromDB();
                    // this.gvPagosLiquidacion.DataSource = null;
                    this.gvPagosLiquidacion.DataSourceID = "";
                    this.gvPagosLiquidacion.DataSource = dsPagos.Tables[0];
                    this.gvPagosLiquidacion.DataBind();
                    this.rowLiqData.nombre = this.dtBoletaData.Rows[0]["nombre"].ToString();
                    this.rowLiqData.domicilio = this.dtBoletaData.Rows[0]["domicilio"].ToString();
                    this.rowLiqData.poblacion = this.dtBoletaData.Rows[0]["poblacion"].ToString();

                    this.rowLiqData.notas = double.Parse(this.dtBoletaData.Rows[0]["notas"].ToString());
                    this.rowLiqData.intereses = double.Parse(this.dtBoletaData.Rows[0]["intereses"].ToString());
                    this.rowLiqData.seguro = double.Parse(this.dtBoletaData.Rows[0]["seguro"].ToString());

                    this.rowLiqData.EjecutadaPor = this.dtBoletaData.Rows[0]["EjecutadaPor"].ToString().ToUpper();


                    SqlConnection BoletasConn = new SqlConnection(myConfig.ConnectionInfo);
                    BoletasConn.Open();
                    try
                    {
                        SqlCommand BoletasComm = new SqlCommand("SELECT     Boletas.boletaID, Boletas.cicloID, Boletas.userID, Boletas.productorID, Boletas.productoID, Boletas.bodegaID, Boletas.NumeroBoleta, Boletas.Ticket, Boletas.codigoClienteProvArchivo, Boletas.NombreProductor, Boletas.Placas, Boletas.FechaEntrada, Boletas.PesadorEntrada, Boletas.PesoDeEntrada, Boletas.BasculaEntrada, Boletas.FechaSalida, Boletas.PesoDeSalida, Boletas.PesadorSalida, Boletas.BasculaSalida, Boletas.pesonetoentrada, Boletas.pesonetosalida, Boletas.humedad, Boletas.dctoHumedad, Boletas.impurezas, Boletas.dctoImpurezas, Boletas.totaldescuentos, Boletas.pesonetoapagar, Boletas.precioapagar, Boletas.importe, Boletas.dctoSecado, Boletas.totalapagar, Boletas.chofer, Boletas.pagada, Boletas.storeTS, Boletas.updateTS, Productos.Nombre AS Producto FROM         Boletas INNER JOIN Liquidaciones_Boletas ON Boletas.boletaID = Liquidaciones_Boletas.BoletaID INNER JOIN Productos ON Boletas.productoID = Productos.productoID "
                        + " WHERE (Liquidaciones_Boletas.LiquidacionID = @LiqID)");
                        BoletasComm.Connection = BoletasConn;
                        BoletasComm.Parameters.Add("@LiqID", SqlDbType.Int).Value = this.iLiqID;
                        SqlDataAdapter BoletasAD = new SqlDataAdapter(BoletasComm);
                        BoletasAD.Fill(this.dtBoletas);
                        this.gvBoletas.DataSource = dtBoletas;
                        this.gvBoletas.DataBind();
                    }
                    catch (System.Exception ex)
                    {
                        bResult = false;
                        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(this.Session["userID"].ToString()), "Error load Liq_Boletas EX:" + ex.Message, this.Request.Url.ToString());
                    }
                    finally
                    {
                        BoletasConn.Close();
                    }
                   
                    
                }
            }
            catch (System.Exception ex)
            {
                bResult = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(Session["USERID"].ToString()), "Error load liquidacion EX:" + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                sqlConn.Close();
            }
            return bResult;
        }
    }
}
