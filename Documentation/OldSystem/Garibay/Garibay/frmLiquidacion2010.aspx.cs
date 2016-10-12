using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Services;

namespace Garibay
{
    public partial class frmLiquidacion2010 : BasePage
    {
        protected void AddJSToControls()
        {
            JSUtils.AddDisableWhenClick(ref this.btnAgregarLiquidacion, this);
            JSUtils.AddDisableWhenClick(ref this.btnAgregarPagoACredito, this);
            JSUtils.AddDisableWhenClick(ref this.btnVerificarAntesAdd, this);
            JSUtils.AddDisableWhenClick(ref this.btnUpdateBoletasExistentes, this);
            JSUtils.AddDisableWhenClick(ref this.btnAgregarBoletasaLiq, this);

            String sOnchagemov = "checkValueInList(";
            sOnchagemov += "this" + ",'EFECTIVO','";
            sOnchagemov += this.divPagoMovCaja.ClientID + "');";
            this.cmbTipodeMovPago.Attributes.Add("onChange", sOnchagemov);

            sOnchagemov += "checkValueInList(";
            sOnchagemov += "this" + ",'MOVIMIENTO DE BANCO','";
            sOnchagemov += this.divMovBanco.ClientID + "');";
            this.cmbTipodeMovPago.Attributes.Add("onChange", sOnchagemov);

            sOnchagemov = "checkValueInList(";
            sOnchagemov += "this" + ",'CHEQUE','";
            sOnchagemov += this.divCheque.ClientID + "');";
            this.cmbConceptomovBancoPago.Attributes.Add("onChange", sOnchagemov);
        }

        protected void seleccionacatalogomaiz()
        {
            try
            {
                string tipo = "MAIZ";
                this.gvBoletas.DataBind();
                if (this.gvBoletas.Rows.Count > 0)
                {
                    switch (tipo)
                    {
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
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "seleccionando el producto preseleccionado", ex);
            }
        }
        private void addHandlersForDatasources()
        {              
            this.sdsAnticipos.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsBoletasData.Selected  += new SqlDataSourceStatusEventHandler(handler);
            this.sdsBoletasExistentes.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsCatalogoCuentaCajaChica.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsCatalogoCuentaFiscal.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsCatalogoCuentaInternaPago.Selected += new SqlDataSourceStatusEventHandler(handler);                    
            this.sdsCiclos.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsCiclosBoletasExistentes.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsCiclosParaCreditosParaLiq.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsConceptoPago.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsCreditosEnLiquidacion.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsCreditosDisponiblesParaPagar.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsCuentaPago.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsGruposCatalogosCajaChica.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsGruposCatalogosfiscalPago.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsGruposCatalogosInternaPago.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsLiquidacionesYaEnSistema.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsPagosBodegas.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsPagosDeLiquidacion.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsProductorData.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsProductores.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsProductoresAnticipos.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsProductoresBolExistentes.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsProductoresPago.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsSubcatalogoCajaChica.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsSubcatalogofiscalPago.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsSubCatalogoInternaPago.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.sdsTotalesLiq.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.SqlDataSourceAnticiposDisponibles.Selected += new SqlDataSourceStatusEventHandler(handler);
            this.SqlDataSourceCiclosAnticiposDisponibles.Selected += new SqlDataSourceStatusEventHandler(handler);

            this.sdsAnticipos.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsBoletasData.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsBoletasExistentes.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsCatalogoCuentaCajaChica.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsCatalogoCuentaFiscal.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsCatalogoCuentaInternaPago.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsCiclos.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsCiclosBoletasExistentes.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsCiclosParaCreditosParaLiq.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsConceptoPago.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsCreditosEnLiquidacion.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsCreditosDisponiblesParaPagar.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsCuentaPago.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsGruposCatalogosCajaChica.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsGruposCatalogosfiscalPago.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsGruposCatalogosInternaPago.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsLiquidacionesYaEnSistema.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsPagosBodegas.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsPagosDeLiquidacion.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsProductorData.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsProductores.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsProductoresAnticipos.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsProductoresBolExistentes.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsProductoresPago.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsSubcatalogoCajaChica.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsSubcatalogofiscalPago.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsSubCatalogoInternaPago.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.sdsTotalesLiq.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.SqlDataSourceAnticiposDisponibles.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
            this.SqlDataSourceCiclosAnticiposDisponibles.Selecting += new SqlDataSourceSelectingEventHandler(handlerForSelecting);
         
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(!this.IsPostBack)
                {
                    this.addHandlersForDatasources();
                    this.cmbCuentaPago.DataBind();
                    this.cmbCuentaPago.SelectedValue = "12";
                    this.AddJSToControls();
                    this.txtFechaPago.Text = this.txtFechaLiquidacion.Text = Utils.Now.ToString("dd/MM/yyyy");

                    this.ddlProductor.DataBind();
                    this.ddlProductor.SelectedIndex = 0;
                    this.PreselectProductorPago();
                    if (this.LoadEncryptedQueryString() > 0 
                        && this.myQueryStrings["liqID"] != null
                        && int.TryParse(this.myQueryStrings["liqID"].ToString(), out this.iLiqID))
                    {
                        this.LoadLiqData();
                    }
                    this.seleccionacatalogomaiz();
                    this.cmbConceptomovBancoPago.DataBind();
                    this.cmbConceptomovBancoPago.SelectedIndex = 0;
                }
                this.iLiqID = int.Parse(this.txtLiquidacionID.Text);
                this.pnlBoletaGridViewResult.Visible = false;
                this.pnlAnticiposGridViewResult.Visible = false;
                this.pnlAddBoletaResult.Visible = false;
                this.pnlNewPagoResult.Visible = false;
                this.pnlLiquidacionResult.Visible = false;
                this.pnlAddCreditoALiqResult.Visible = false;
                this.pnlCreditosGridViewResult.Visible = false;


                this.divPagoMovCaja.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.divMovBanco.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "MOVIMIENTO DE BANCO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.divCheque.Attributes.Add("style", this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.gvAnticipos.DataBind();

                this.ShowCreditoPendienteMessage();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "loading liquidacion", ref ex);
            }
        }

        
        protected void btnActualizaComboProductores_Click(object sender, EventArgs e)
        {
            this.ddlProductor.DataBind();
        }

        protected string GetLiqExistenteURL(string sLiqID)
        {
            String sQuery = "liqID=" + sLiqID;
            sQuery = Utils.GetEncriptedQueryString(sQuery);
            String strRedirect = "~/frmLiquidacion2010.aspx";
            strRedirect += sQuery;
            return strRedirect;
        }

        protected void btnVerificarAntesAdd_Click(object sender, EventArgs e)
        {
            this.lblValidacionRes.Text = "";
            this.lblValidacionRes.Visible = false;
            this.gvLiquidacionesSinCerrar.Visible = true;
            this.sdsLiquidacionesYaEnSistema.DataBind();
            this.gvLiquidacionesSinCerrar.DataBind();
            this.btnVerificarAntesAdd.Visible = false;
            this.btnAgregarLiquidacion.Visible = this.gvLiquidacionesSinCerrar.Rows.Count == 0;
            if (this.gvLiquidacionesSinCerrar.Rows.Count > 0)
            {
                this.lblValidacionRes.Text = "YA EXISTEN LIQUIDACIONES PENDIENTES DEL PRODUCTOR";
                lblValidacionRes.Visible = true;
            }
        }

        protected void ddlCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected int iLiqID = -1;

        protected void PreselectProductorPago()
        {
            this.cmbProductoresPago.DataBind();
            this.cmbProductoresPago.SelectedValue = this.ddlProductor.SelectedValue;
            this.txtChequeNombre0.Text =  this.txtNombrePago.Text = this.cmbProductoresPago.SelectedItem.Text;
        }

        protected void ShowCreditoPendienteMessage()
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "SELECT     COUNT(*) FROM Creditos "
                + " WHERE (productorID = @productorID) AND (statusID = 1) AND (pagado = 0)";
                comm.Parameters.Add("@productorID", SqlDbType.Int).Value = this.ddlProductor.SelectedValue;
                object oResult = comm.ExecuteScalar();
                this.pnlCreditoPendiente.Visible = oResult != null && ((int)oResult) > 0;
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "show if credito pendiente : " + this.iLiqID.ToString(), ex);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void LoadLiqData()
        {
            this.txtLiquidacionID.Text = this.iLiqID.ToString();

            SqlCommand sqlComm = new SqlCommand();
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandText = "SELECT * FROM Liquidaciones WHERE (LiquidacionID = @LIQUIDACIONID)";
                sqlComm.Parameters.Add("@LIQUIDACIONID", SqlDbType.Int).Value = this.iLiqID;

                SqlDataReader dr = sqlComm.ExecuteReader();

                if(dr.HasRows && dr.Read())
                {
                    this.btnVerificarAntesAdd.Visible = this.btnAgregarLiquidacion.Visible = false;
                    lblValidacionRes.Visible = this.chkAsignarAnticipos.Visible = this.chkBoxTraeBoletas.Visible = false;
                    this.pnlCentral.Visible = true;
                    this.lblNumLiquidacion.Text = this.iLiqID.ToString();
                    this.txtFechaLiquidacion.Text = ((DateTime)dr["fecha"]).ToString("dd/MM/yyyy");
                    this.ddlCiclo.DataBind();
                    this.ddlCiclo.SelectedValue = dr["cicloID"].ToString();
                    this.ddlProductor.DataBind();
                    this.ddlProductor.SelectedValue = dr["productorID"].ToString();
                    this.ddlProductor.Enabled = false;
                    this.dvProductorData.DataBind();

                    this.gvBoletas.DataBind();
                    this.gvAnticipos.DataBind();

                    this.PreselectProductorPago();
                    this.CreatePrintLink();

                    this.EnableBlockMode((bool)dr["cobrada"]);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, this.UserID, "Error cargando informacion de la liquidacion ex: " + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                sqlConn.Close();
            }
        }

        protected void btnAgregarLiquidacion_Click(object sender, EventArgs e)
        {
            //this.CargaProductor();
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.Connection = conn;
                sqlComm.CommandText = "INSERT INTO Liquidaciones (productorID, cicloID, userID, nombre, domicilio, poblacion, fecha, fechalarga) "
                    + " select @productorID,@cicloID,@userID, (SELECT     LTRIM(apaterno + ' ' + amaterno + ' '+ nombre) as Nombre "
                    + " FROM Productores where productorID = @productorID), "
                    + " (SELECT domicilio FROM Productores where productorID = @productorID), "
                    + " (SELECT poblacion FROM Productores where productorID = @productorID),@fecha,@fechalarga;";
                sqlComm.CommandText += "select liquidacionID = SCOPE_IDENTITY();";
                sqlComm.Parameters.Add("@productorID", SqlDbType.Int).Value = this.ddlProductor.SelectedItem.Value;
                sqlComm.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(this.ddlCiclo.SelectedItem.Value);
                sqlComm.Parameters.Add("@userID", SqlDbType.Int).Value = int.Parse(Session["USERID"].ToString());
                sqlComm.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Parse(this.txtFechaLiquidacion.Text);
                sqlComm.Parameters.Add("@fechalarga", SqlDbType.VarChar).Value = DateTime.Parse(this.txtFechaLiquidacion.Text).ToString("dddd, dd") + " de " + DateTime.Parse(this.txtFechaLiquidacion.Text).ToString("MMMM, yyyy");

                int.TryParse(sqlComm.ExecuteScalar().ToString(), out this.iLiqID);
                this.lblNumLiquidacion.Text = this.txtLiquidacionID.Text = this.iLiqID.ToString();

                if (this.iLiqID > 0 && this.chkBoxTraeBoletas.Checked)
                {
                    //agregar todas las boletas que no esten en una liquidacion del productor.
                    sqlComm.CommandText = "insert into Liquidaciones_Boletas(LiquidacionID,BoletaID) SELECT @LiquidacionID,boletaID FROM         dbo.Boletas WHERE     (boletaID NOT IN (SELECT     BoletaID FROM          dbo.Liquidaciones_Boletas)) AND productorID = @productorID and boletas.cicloid = @cicloid";
                    sqlComm.Parameters.Clear();
                    sqlComm.Parameters.Add("@LiquidacionID", SqlDbType.Int).Value = this.iLiqID;
                    sqlComm.Parameters.Add("@cicloid", SqlDbType.Int).Value = int.Parse(this.ddlCiclo.SelectedItem.Value); ;
                    sqlComm.Parameters.Add("@productorID", SqlDbType.Int).Value = this.ddlProductor.SelectedItem.Value;
                    sqlComm.ExecuteNonQuery();
                }

                if (this.iLiqID > 0 && this.chkAsignarAnticipos.Checked)
                {
                    //agregar todas las boletas que no esten en una liquidacion del productor.
                    sqlComm.CommandText = "insert into Liquidaciones_anticipos(LiquidacionID,Anticipos) SELECT @LiquidacionID, anticipoID FROM         Anticipos WHERE     (anticipoID NOT IN (SELECT     Anticipos.anticipoID FROM          Liquidaciones_Anticipos)) AND Anticipos.productorID = @productorID AND  (Anticipos.tipoAnticipoID = 1) ";
                    sqlComm.Parameters.Clear();
                    sqlComm.Parameters.Add("@LiquidacionID", SqlDbType.Int).Value = this.iLiqID;
                    sqlComm.Parameters.Add("@productorID", SqlDbType.Int).Value = this.ddlProductor.SelectedItem.Value;
                    sqlComm.ExecuteNonQuery();
                }
                String sQuery = "liqID=" + this.iLiqID.ToString();
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = "~/frmLiquidacion2010.aspx";
                strRedirect += sQuery;
                Response.Redirect(strRedirect, true);
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

        protected void ddlProductor_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblValidacionRes.Text = string.Empty;
            this.lblValidacionRes.Visible = false;
            this.gvLiquidacionesSinCerrar.Visible = false;
            this.btnVerificarAntesAdd.Visible = true;
            this.btnAgregarLiquidacion.Visible = false;
        }

        protected void dvNewBoleta_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            //@FechaSalida,@PesoDeSalida,@humedad,@impurezas,@precioapagar,@chofer,@applyHumedad,@applyImpurezas,@applySecado,@dctoHumedad,@dctoImpurezas,@dctoSecado
            try
            {
                e.Values["userID"] = this.UserID;
                e.Values["cicloID"] = this.ddlCiclo.SelectedValue;
                e.Values["FechaEntrada"] = DateTime.Parse(e.Values["FechaEntrada"].ToString());
                if (e.Values["FechaSalida"] == null)
                {
                    e.Values["FechaSalida"] = e.Values["FechaEntrada"];
                }
                e.Values["FechaSalida"] = DateTime.Parse(e.Values["FechaSalida"].ToString());

                e.Values["applyHumedad"] = true;
                e.Values["applyImpurezas"] = true;
                e.Values["applySecado"] = true;

                if (e.Values["precioapagar"] == null)
                {
                    e.Values["precioapagar"] = 0;
                }

                if(e.Values["humedad"] == null)
                {
                    e.Values["humedad"] = 0;
                }

                if (e.Values["impurezas"] == null)
                {
                    e.Values["impurezas"] = 0;
                }
                double pesoNetoEntrada = 0;

                pesoNetoEntrada = Utils.GetSafeFloat(e.Values["PesoDeEntrada"].ToString())
                    - Utils.GetSafeFloat(e.Values["PesoDeSalida"].ToString());

                e.Values["pesonetoentrada"] = pesoNetoEntrada;
                e.Values["dctoHumedad"] = Utils.getDesctoHumedad(Utils.GetSafeFloat(e.Values["humedad"].ToString()), pesoNetoEntrada);
                e.Values["dctoImpurezas"] = Utils.getDesctoImpurezas(Utils.GetSafeFloat(e.Values["impurezas"].ToString()), pesoNetoEntrada);
                e.Values["dctoSecado"] = Utils.getDesctoHumedad(Utils.GetSafeFloat(e.Values["humedad"].ToString()), pesoNetoEntrada);

                e.Values["pesonetoapagar"] = pesoNetoEntrada
                    - Utils.getDesctoHumedad(Utils.GetSafeFloat(e.Values["humedad"].ToString()), pesoNetoEntrada)
                    - Utils.getDesctoImpurezas(Utils.GetSafeFloat(e.Values["impurezas"].ToString()), pesoNetoEntrada);

                e.Values["importe"] = Utils.GetSafeFloat(e.Values["pesonetoapagar"].ToString())
                    * Utils.GetSafeFloat(e.Values["precioapagar"].ToString());

                e.Values["totalapagar"] = Utils.GetSafeFloat(e.Values["pesonetoapagar"].ToString()) 
                    * Utils.GetSafeFloat(e.Values["precioapagar"].ToString())
                    - Utils.getDesctoHumedad(Utils.GetSafeFloat(e.Values["humedad"].ToString()), pesoNetoEntrada);

                e.Values["LiquidacionID"] = this.iLiqID;
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "add new boleta into liquidacion", ref ex);
            }
        }

        protected void dvNewBoleta_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            if(e.Exception != null)
            {
                e.ExceptionHandled = true;
                Exception ex = e.Exception;
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "add new boleta into liquidacion", ref ex);
            }
            if(e.AffectedRows == 0 || e.Exception != null)
            {
                e.KeepInInsertMode = true;
                this.pnlAddBoletaResult.Visible = true;
                this.lblAddBoletaResult.Text = "LA BOLETA NO HA PODIDO SER AGREGADA REVISE LOS DATOS E INTENTELO DE NUEVO.";
            }
            else
            {
                this.pnlAddBoletaResult.Visible = true;
                this.lblAddBoletaResult.Text = "LA BOLETA HA SIDO AGREGADA A LA LIQUIDACION EXITOSAMENTE.";
            }
        }

        protected void gvBoletas_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvBoletas.EditIndex = this.gvBoletas.SelectedIndex;
        }

        protected void gvBoletas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvBoletas.EditIndex = e.NewEditIndex;
        }

        protected void gvBoletas_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            if(e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "quitando boleta de liquidacion", e.Exception);
                e.ExceptionHandled = true;
            }
            if(e.AffectedRows > 0 )
            {
                this.pnlBoletaGridViewResult.Visible = true;
                this.lblBoletaGridViewResult.Text = "LA BOLETA SE QUITO DE LA LIQUIDACION";
            }
        }

        protected void gvAnticipos_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            if (e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "quitando anticipo de liquidacion", e.Exception);
                e.ExceptionHandled = true;
            }
            if (e.AffectedRows > 0)
            {
                this.pnlAnticiposGridViewResult.Visible = true;
                this.lblAnticiposGridViewResult.Text = "EL ANTICIPO SE QUITO DE LA LIQUIDACION";
                this.GridViewAnticiposDisponibles.DataBind();
                this.gvAnticipos.DataBind();
            }
        }

        protected void ddlCicloParaCreditoParaLiq_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlCreditosDisponiblesParaPagar.DataBind();
        }

        protected void btnAgregarPagoACredito_Click(object sender, EventArgs e)
        {
            this.pnlAddCreditoALiqResult.Visible = true;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "INSERT INTO Liquidaciones_Creditos (LiquidacionID, creditoID, pago) VALUES (@LiquidacionID,@creditoID,@pago); UPDATE Creditos SET pagado = @pagado WHERE (creditoID = @creditoID);";
                comm.Connection = conn;
                conn.Open();
                comm.Parameters.Add("@LiquidacionID", SqlDbType.Int).Value = this.iLiqID;
                comm.Parameters.Add("@creditoID", SqlDbType.Int).Value = this.ddlCreditosDisponiblesParaPagar.SelectedValue;
                comm.Parameters.Add("@pago", SqlDbType.Float).Value = Utils.GetSafeFloat(this.txtAbonoACredito.Text);
                comm.Parameters.Add("@pagado", SqlDbType.Bit).Value = this.chkMarcarCreditoPagado.Checked? 1: 0;
                if(comm.ExecuteNonQuery() > 0)
                {
                    this.txtAbonoACredito.Text = "";
                    this.chkMarcarCreditoPagado.Checked = false;
                    this.lblpnlAddCreditoALiqResult.Text = "CREDITO AGREGADO EXITOSAMENTE";
                    this.ddlCreditosDisponiblesParaPagar.DataBind();
                }
             }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "inserting pago en liquidacion para credito", ex);
                this.lblpnlAddCreditoALiqResult.Text = "ERROR AGREGANDO EL PAGO A LA LIQUIDACION: " + ex.ToString();
            }
            finally
            {
                conn.Close();
            }
            this.gvCreditosEnLiquidacion.DataBind();
        }

        protected void ddlCicloBoletaExistente_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlProductoresBolExistentes.DataBind();
            this.gvBoletasExistentes.DataBind();
        }

        protected void ddlProductoresBolExistentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvBoletasExistentes.DataBind();
        }

        protected void btnAgregarBoletasaLiq_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "INSERT INTO Liquidaciones_Boletas (LiquidacionID, BoletaID) VALUES (@LiquidacionID,@BoletaID)";
                foreach (GridViewRow row in this.gvBoletasExistentes.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkBoletaAdd");
                    if(chk != null && chk.Checked)
                    {
                        comm.Parameters.Clear();
                        comm.Parameters.Add("@LiquidacionID", SqlDbType.Int).Value = this.iLiqID;
                        comm.Parameters.Add("@BoletaID", SqlDbType.Int).Value = (int)this.gvBoletasExistentes.DataKeys[row.RowIndex]["boletaID"];
                        comm.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error agregando boleta a liquidacion", ex);
            }
            finally
            {
                conn.Close();
            }
            this.gvBoletasExistentes.DataBind();
            this.gvBoletas.DataBind();
        }

        protected void btnUpdateBoletasExistentes_Click(object sender, EventArgs e)
        {
            string id = this.ddlProductoresBolExistentes.SelectedValue;
            this.ddlProductoresBolExistentes.DataBind();
            this.ddlProductoresBolExistentes.SelectedValue = id;
            this.gvBoletasExistentes.DataBind();
        }

        protected void gvBoletas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            /*
UPDATE       Boletas
SET                productoID = @productoID, Ticket = @Ticket, pesonetoentrada = @pesonetoentrada, humedad = @humedad, dctoHumedad = @dctoHumedad, 
                         impurezas = @impurezas, dctoImpurezas = @dctoImpurezas, totaldescuentos = @totaldescuentos, pesonetoapagar = @pesonetoapagar, 
                         precioapagar = @precioapagar, importe = @importe, dctoSecado = @dctoSecado, totalapagar = @totalapagar, applyHumedad = @applyHumedad, 
                         applyImpurezas = @applyImpurezas, applySecado = @applySecado
WHERE        (boletaID = @boletaID)*/

            double pesoNeto = Utils.GetSafeFloat(e.NewValues["PesoDeEntrada"]) - Utils.GetSafeFloat(e.NewValues["PesoDeSalida"]);
            double totalDescuentos = 0;

            e.NewValues["pesonetoentrada"] = pesoNeto;
            totalDescuentos = bool.Parse(e.NewValues["applyHumedad"].ToString())? Utils.getDesctoHumedad(Utils.GetSafeFloat(e.NewValues["humedad"]), pesoNeto) : 0;

            e.NewValues["dctoHumedad"] = bool.Parse(e.NewValues["applyHumedad"].ToString()) ? Utils.getDesctoHumedad(Utils.GetSafeFloat(e.NewValues["humedad"]), pesoNeto) : 0;

            totalDescuentos += bool.Parse(e.NewValues["applyImpurezas"].ToString()) ? Utils.getDesctoImpurezas(Utils.GetSafeFloat(e.NewValues["impurezas"]), pesoNeto) : 0;
            e.NewValues["dctoImpurezas"] = bool.Parse(e.NewValues["applyImpurezas"].ToString()) ? Utils.getDesctoImpurezas(Utils.GetSafeFloat(e.NewValues["impurezas"]), pesoNeto) : 0;
          
            e.NewValues["pesonetoapagar"] = pesoNeto - totalDescuentos;
            e.NewValues["totaldescuentos"] = totalDescuentos;
            e.NewValues["importe"] = (pesoNeto - totalDescuentos) * Utils.GetSafeFloat(e.NewValues["precioapagar"]);

            e.NewValues["dctoSecado"] = bool.Parse(e.NewValues["applySecado"].ToString()) ? Utils.getDesctoSecado(Utils.GetSafeFloat(e.NewValues["humedad"]), pesoNeto) : 0;

            e.NewValues["totalapagar"] = (pesoNeto - totalDescuentos) * Utils.GetSafeFloat(e.NewValues["precioapagar"]) - (bool.Parse(e.NewValues["applySecado"].ToString()) ? Utils.getDesctoSecado(Utils.GetSafeFloat(e.NewValues["humedad"]), pesoNeto) : 0);



        }

        protected void gvBoletas_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            if(e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "updating boleta", e.Exception);
                e.ExceptionHandled = true;
            }
        }

        protected void gvBoletas_DataBound(object sender, EventArgs e)
        {
            try
            {
	            this.dvTotalesLiq.DataBind();
	            DataView dv = (DataView) this.sdsBoletasData.Select(DataSourceSelectArguments.Empty);
	            DataTable dt = dv.ToTable();
	            Label lbl = null;
	            if (dt.Rows.Count > 0)
	            {
	                lbl = (Label)this.gvBoletas.FooterRow.FindControl("lblPesoNeto");
	                if (lbl != null)
	                {
	                    lbl.Text = double.Parse(dt.Compute("SUM(pesonetoentrada)", "").ToString()).ToString("N2");
	                }
	                lbl = (Label)this.gvBoletas.FooterRow.FindControl("lblDctoHum");
	                if (lbl != null)
	                {
	                    lbl.Text = double.Parse(dt.Compute("SUM(dctoHumedad)", "").ToString()).ToString("N2");
	                }
	                lbl = (Label)this.gvBoletas.FooterRow.FindControl("lblDctoImpurezas");
	                if (lbl != null)
	                {
	                    lbl.Text = double.Parse(dt.Compute("SUM(dctoImpurezas)", "").ToString()).ToString("N2");
	                }
	
	                lbl = (Label)this.gvBoletas.FooterRow.FindControl("lblKgNetos");
	                if (lbl != null)
	                {
	                    lbl.Text = double.Parse(dt.Compute("SUM(pesonetoapagar)", "").ToString()).ToString("N2");
	                }
	
	                lbl = (Label)this.gvBoletas.FooterRow.FindControl("lblImporte");
	                if (lbl != null)
	                {
	                    lbl.Text = double.Parse(dt.Compute("SUM(importe)", "").ToString()).ToString("C2");
	                }
	
	                lbl = (Label)this.gvBoletas.FooterRow.FindControl("lblDctoSecado");
	                if (lbl != null)
	                {
	                    lbl.Text = double.Parse(dt.Compute("SUM(dctoSecado)", "").ToString()).ToString("C2");
	                }
	
	                lbl = (Label)this.gvBoletas.FooterRow.FindControl("lblTotalPagar");
	                if (lbl != null)
	                {
	                    lbl.Text = double.Parse(dt.Compute("SUM(totalapagar)", "").ToString()).ToString("C2");
	                }
	            }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "getting totales de boletas in liquidacion", ex);
            }
        }

        protected void gvPagosLiquidacion_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            if (e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "Deleting pago de liquidacion", e.Exception);
                e.ExceptionHandled = true;
            }
            this.gvPagosLiquidacion.EditIndex = -1;
            this.gvPagosLiquidacion.SelectedIndex = -1;
            this.gvPagosLiquidacion.DataBind();
        }

        protected void drpdlGrupoCatalogosCajaChica_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlCatalogocuentaCajaChica.DataBind();
            this.drpdlSubcatalogoCajaChica.DataBind();
        }

        protected void drpdlCatalogocuentaCajaChica_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlSubcatalogoCajaChica.DataBind();
        }

        protected void drpdlGrupoCuentaFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlCatalogocuentafiscalPago.DataBind();
            this.drpdlSubcatalogofiscalPago.DataBind();
        }

        protected void drpdlCatalogocuentafiscalPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlSubcatalogofiscalPago.DataBind();
        }

        protected void drpdlGrupoCatalogosInternaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlCatalogoInternoPago.DataBind();
            this.drpdlSubcatologointernaPago.DataBind();
        }

        protected void drpdlCatalogoInternoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlSubcatologointernaPago.DataBind();
        }

        protected void btnAddPago_Click(object sender, EventArgs e)
        {
            DateTime dtFecha;
            double dMonto = 0;
            bool HayError = false;
            string sResult = string.Empty;

            this.pnlNewPagoResult.Visible = true;

            if(!DateTime.TryParse(this.txtFechaPago.Text, out dtFecha))
            {
                HayError = true;
                sResult += "LA FECHA DE PAGO ES INVALIDA<BR />";
            }

            if (this.txtNombrePago.Text.Trim().Length == 0)
            {
                HayError = true;
                sResult += "DEBE INDICAR UN NOMBRE<BR />";
            }

            if (!double.TryParse(this.txtMonto.Text, out dMonto))
            {
                HayError = true;
                sResult += "EL MONTO DE PAGO ES INVALIDO<BR />";
            }
            int cheque = 0;
            if (int.TryParse(this.txtChequeNum.Text, out cheque) && cheque > 0)
            {
                if (!(this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO") && this.cmbConceptomovBancoPago.SelectedItem.Text.ToUpper().Equals("CHEQUE") && dbFunctions.ChequeAlreadyExists(cheque, this.cmbCuentaPago.SelectedItem.Value.ToString()))
                {
                    HayError = true;
                    sResult += "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, EL CHEQUE YA ESTA EXISTE<BR />";
                }

                if (!(this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO") && this.cmbConceptomovBancoPago.SelectedItem != null && this.cmbConceptomovBancoPago.SelectedItem.Text.IndexOf("CHEQUE") > -1 && !dbFunctions.numChequeValido(cheque, int.Parse(this.cmbCuentaPago.SelectedValue.ToString())))
                {
                    HayError = true;
                    sResult += "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, EL NUMERO DE CHEQUE NO CORRESPONDE A EL NUMERO DE CUENTA<BR />";
                }
            }

            SqlCommand commSaldo = new SqlCommand();
            commSaldo.CommandText = "select ISNULL(Boletas - Anticipos - Pagos_creditos - Pagos,0) from vTotalesLiquidacion where LiquidacionID = @LiquidacionID";
            commSaldo.Parameters.Add("@LiquidacionID", SqlDbType.Int).Value = this.iLiqID;
            double SaldoRestante = dbFunctions.GetExecuteDoubleScalar(commSaldo, 0);
            SaldoRestante = Math.Round(SaldoRestante, 2);

            if (dMonto > SaldoRestante)
            {
                HayError = true;
                sResult += "EL PAGO NO PUEDE SER MAYOR QUE EL SALDO RESTANTE DE LA LIQUIDIACION<BR />";
            }

            if(!HayError)
            {
                if (this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO")
                {
                    SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                    try
                    {
                        conn.Open();
                        SqlCommand comm = new SqlCommand();
                        comm.Connection = conn;
                        comm.CommandText = "INSERT INTO MovimientosCaja "
                            + " (cicloID, userID, nombre, cargo, abono, fecha, catalogoMovBancoID, subCatalogoMovBancoID, bodegaID) "
                            + " VALUES     (@cicloID,@userID,@nombre,@cargo,@abono,@fecha,@catalogoMovBancoID,@subCatalogoMovBancoID,@bodegaID); "
                            + " INSERT INTO PagosLiquidacion "
                            + " (liquidacionID, cicloID, productorID, fecha, movimientoID, userID) "
                            + " VALUES     (@liquidacionID,@cicloID,@productorID,@fecha,SCOPE_IDENTITY(),@userID)";

                        comm.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(this.ddlCiclo.SelectedValue);
                        comm.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                        comm.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtNombrePago.Text;
                        comm.Parameters.Add("@cargo", SqlDbType.Float).Value = dMonto;
                        comm.Parameters.Add("@abono", SqlDbType.Float).Value = 0;
                        comm.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtFecha;
                        comm.Parameters.Add("@catalogoMovBancoID", SqlDbType.Int).Value = int.Parse(this.drpdlCatalogocuentaCajaChica.SelectedValue);
                        comm.Parameters.Add("@subCatalogoMovBancoID", SqlDbType.Int).Value = this.drpdlSubcatalogoCajaChica.Items.Count > 0 ? int.Parse(this.drpdlSubcatalogoCajaChica.SelectedValue) : -1;
                        comm.Parameters.Add("@bodegaID", SqlDbType.Int).Value = int.Parse(this.ddlPagosBodegas.SelectedValue);
                        comm.Parameters.Add("@liquidacionID", SqlDbType.Int).Value = this.iLiqID;

                        comm.Parameters.Add("@productorID", SqlDbType.Int).Value = int.Parse(this.cmbProductoresPago.SelectedValue);
                        if(comm.ExecuteNonQuery() == 2)
                        {
                            HayError = false;
                            sResult = "EL PAGO FUE AGREGADO EXITOSAMENTE";
                        }
                        else
                        {
                            HayError = true;
                            sResult = "NO FUE POSIBLE AGREGAR EL PAGO, ERROR DESCONOCIDO.";
                        }
                    }
                    catch (System.Exception ex)
                    {
                        HayError = true;
                        sResult += "NO SE PUDO AGREGAR EL PAGO EL ERROR ES: <BR />";
                        sResult += ex.Message;
                        Logger.Instance.LogException(Logger.typeUserActions.INSERT, "err insertando pago de efectivo a liquidacion", ex);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                    try
                    {
                        conn.Open();
                        SqlCommand comm = new SqlCommand();
                        comm.Connection = conn;
                        comm.CommandText = "INSERT INTO MovimientosCuentasBanco "
                            + " (cuentaID, ConceptoMovCuentaID, fecha, cargo, abono, userID, nombre, numCheque, chequeNombre,  catalogoMovBancoFiscalID, "
                            + " subCatalogoMovBancoFiscalID, catalogoMovBancoInternoID, subCatalogoMovBancoInternoID, fechaCheque, fechacobrado) "
                            + " VALUES     (@cuentaID,@ConceptoMovCuentaID,@fecha,@cargo,@abono,@userID,@nombre,@numCheque,@chequeNombre,@catalogoMovBancoFiscalID,@subCatalogoMovBancoFiscalID,@catalogoMovBancoInternoID,@subCatalogoMovBancoInternoID, @fechaCheque, @fechacobrado);"
                            + " INSERT INTO PagosLiquidacion "
                            + " (liquidacionID, cicloID, productorID, fecha, movbanID, userID) "
                            + " VALUES     (@liquidacionID,@cicloID,@productorID,@fecha,SCOPE_IDENTITY(),@userID)";

                        comm.Parameters.Add("@cuentaID", SqlDbType.Int).Value = int.Parse(this.cmbCuentaPago.SelectedValue);
                        comm.Parameters.Add("@ConceptoMovCuentaID", SqlDbType.Int).Value = int.Parse(this.cmbConceptomovBancoPago.SelectedValue);
                        comm.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtFecha;
                        comm.Parameters.Add("@fechacobrado", SqlDbType.DateTime).Value = dtFecha;
                        comm.Parameters.Add("@fechaCheque", SqlDbType.DateTime).Value = dtFecha;
                        comm.Parameters.Add("@cargo", SqlDbType.Float).Value = dMonto;
                        comm.Parameters.Add("@abono", SqlDbType.Float).Value = 0;
                        comm.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                        comm.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtNombrePago.Text.Trim().ToUpper();
                        
                        comm.Parameters.Add("@chequeNombre", SqlDbType.VarChar).Value = this.txtChequeNombre0.Text.Trim().ToUpper();

                        comm.Parameters.Add("@catalogoMovBancoFiscalID", SqlDbType.Int).Value = int.Parse(this.drpdlCatalogocuentafiscalPago.Text);
                        comm.Parameters.Add("@subCatalogoMovBancoFiscalID", SqlDbType.Int).Value = this.drpdlSubcatalogofiscalPago.Items.Count > 0 ? int.Parse(this.drpdlSubcatalogofiscalPago.Text) : -1;

                        if (this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE")
                        {
                            comm.Parameters.Add("@catalogoMovBancoInternoID", SqlDbType.Int).Value = int.Parse(this.drpdlCatalogoInternoPago.Text);
                            comm.Parameters.Add("@subCatalogoMovBancoInternoID", SqlDbType.Int).Value = this.drpdlSubcatologointernaPago.Items.Count > 0 ? int.Parse(this.drpdlSubcatologointernaPago.Text) : -1;
                            comm.Parameters.Add("@numCheque", SqlDbType.Int).Value = int.Parse(this.txtChequeNum.Text);
                        }
                        else
                        {
                            comm.Parameters.Add("@catalogoMovBancoInternoID", SqlDbType.Int).Value = int.Parse(this.drpdlCatalogocuentafiscalPago.Text);
                            comm.Parameters.Add("@subCatalogoMovBancoInternoID", SqlDbType.Int).Value = this.drpdlSubcatalogofiscalPago.Items.Count > 0 ? int.Parse(this.drpdlSubcatalogofiscalPago.Text) : -1;
                            comm.Parameters.Add("@numCheque", SqlDbType.Int).Value = 0;
                        }
                        comm.Parameters.Add("@liquidacionID", SqlDbType.Int).Value = this.iLiqID;
                        comm.Parameters.Add("@productorID", SqlDbType.Int).Value = int.Parse(this.cmbProductoresPago.SelectedValue);
                        comm.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(this.ddlCiclo.SelectedValue);
                        if (comm.ExecuteNonQuery() == 2)
                        {
                            HayError = false;
                            sResult = "EL PAGO FUE AGREGADO EXITOSAMENTE";
                            this.txtMonto.Text = string.Empty;
                        }
                        else
                        {
                            HayError = true;
                            sResult = "NO FUE POSIBLE AGREGAR EL PAGO, ERROR DESCONOCIDO.";
                        }
                    }
                    catch (System.Exception ex)
                    {
                        HayError = true;
                        sResult += "NO SE PUDO AGREGAR EL PAGO EL ERROR ES: <BR />";
                        sResult += ex.Message;
                        Logger.Instance.LogException(Logger.typeUserActions.INSERT, "err insertando pago de efectivo a liquidacion", ex);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            this.imgMalPago.Visible = HayError;
            this.imgBienPago.Visible = !HayError;
            this.lblNewPagoResult.Text = sResult;
            this.gvPagosLiquidacion.DataBind();
            this.dvTotalesLiq.DataBind();
            UpdateAddNewPago.Update();
            String sQuery = "liqID=" + this.iLiqID.ToString();
            sQuery = Utils.GetEncriptedQueryString(sQuery);
            String strRedirect = "~/frmLiquidacion2010.aspx";
            strRedirect += sQuery;
            Response.Redirect(strRedirect, true);
        }

        protected void gvPagosLiquidacion_DataBound(object sender, EventArgs e)
        {
            this.dvTotalesLiq.DataBind();
        }

        protected void btnDeshacer_Click1(object sender, EventArgs e)
        {
            this.SaveAndBlockLiquidacion(false);
            this.EnableBlockMode(false);
        }

        protected void btnPrintLiquidacion_Click(object sender, EventArgs e)
        {
        }

        private void SaveAndBlockLiquidacion(bool Ejecutar)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            this.pnlLiquidacionResult.Visible = true;
            try
            {
                SqlCommand comm = new SqlCommand();
                conn.Open();
                comm.Connection = conn;

                comm.CommandText = "update Boletas set pagada=@cobrada where boletaID in (SELECT     BoletaID FROM         Liquidaciones_Boletas WHERE     (LiquidacionID = @LiquidacionID)); update Liquidaciones set cobrada = @cobrada, userIdejecuto = @userIdejecuto, fechaEjecucion = @fechaEjecucion where LiquidacionID = @LiquidacionID;";
                comm.Parameters.Add("@cobrada", SqlDbType.Bit).Value = Ejecutar ? 1: 0;
                comm.Parameters.Add("@LiquidacionID", SqlDbType.Int).Value = this.iLiqID;
                comm.Parameters.Add("@userIdejecuto", SqlDbType.Int).Value = this.UserID;
                comm.Parameters.Add("@fechaEjecucion", SqlDbType.DateTime).Value = Utils.Now;
                int iRows = comm.ExecuteNonQuery();
                if(iRows > 0)
                {
                    this.imgLiquidacionBien.Visible = true;
                    this.imgLiquidacionMal.Visible = false;
                    this.lblNewLiquidacionresult.Text = "La liquidacion ha sido actualizada exitosamente.";
                }

            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "error salvando y bloqueando liquidacion", ex);
                this.imgLiquidacionBien.Visible = true;
                this.imgLiquidacionMal.Visible = false;
                this.lblNewLiquidacionresult.Text = "La liquidacion no pudo ser actualizada, el error: " + ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        private void CreatePrintLink()
        {
            //TODO: change here to use the new print
            string datosaencriptar;
            datosaencriptar = "id=";
            datosaencriptar += this.lblNumLiquidacion.Text;
            datosaencriptar += "&";

            string parameter = "javascript:url('";
            parameter += "frmLiquidacionEsqueleto.aspx?data=";
            parameter += Utils.encriptacadena(datosaencriptar);
            parameter += "', 'Impresion de liquidacion',200,200,300,300); return false;";
            this.btnPrintLiquidacion.Attributes.Add("onclick", parameter);
        }

        protected void btnRealizaLiq_Click(object sender, EventArgs e)
        {
            this.SaveAndBlockLiquidacion(true);
            this.CreatePrintLink();
            this.EnableBlockMode(true);
        }

        protected void EnableBlockMode(bool IsBlockedModeEnabled)
        {
            this.chkAddBoletaExistente.Visible = !IsBlockedModeEnabled && (ConfigurationManager.AppSettings["chkAddBoletaExistente"] != null 
                                                                           && bool.Parse(ConfigurationManager.AppSettings["chkAddBoletaExistente"]));
            this.pnlAddBoletaExistente.Visible = !IsBlockedModeEnabled && (ConfigurationManager.AppSettings["pnlAddBoletaExistente"] != null
                                                                           && bool.Parse(ConfigurationManager.AppSettings["pnlAddBoletaExistente"]));
            this.chkAddNewBoleta.Visible = !IsBlockedModeEnabled && (ConfigurationManager.AppSettings["chkAddNewBoleta"] != null
                                                                           && bool.Parse(ConfigurationManager.AppSettings["chkAddNewBoleta"]));
            this.pnlNewBoleta.Visible = !IsBlockedModeEnabled && (ConfigurationManager.AppSettings["pnlNewBoleta"] != null
                                                                           && bool.Parse(ConfigurationManager.AppSettings["pnlNewBoleta"]));
            this.btnRealizaLiq.Visible = !IsBlockedModeEnabled && (ConfigurationManager.AppSettings["btnRealizaLiq"] != null
                                                                           && bool.Parse(ConfigurationManager.AppSettings["btnRealizaLiq"]));
            this.btnDeshacer.Visible = IsBlockedModeEnabled && (ConfigurationManager.AppSettings["btnDeshacer"] != null
                                                                           && bool.Parse(ConfigurationManager.AppSettings["btnDeshacer"]));
            this.btnPrintLiquidacion.Visible = IsBlockedModeEnabled && (ConfigurationManager.AppSettings["btnPrintLiquidacion"] != null
                                                                           && bool.Parse(ConfigurationManager.AppSettings["btnPrintLiquidacion"]));
            this.gvBoletas.Enabled = !IsBlockedModeEnabled && (ConfigurationManager.AppSettings["gvBoletas"] != null
                                                                           && bool.Parse(ConfigurationManager.AppSettings["gvBoletas"]));
            this.gvAnticipos.Enabled = !IsBlockedModeEnabled && (ConfigurationManager.AppSettings["gvAnticipos"] != null
                                                                           && bool.Parse(ConfigurationManager.AppSettings["gvAnticipos"]));
            this.chkAddCreditoALiq.Visible = !IsBlockedModeEnabled && (ConfigurationManager.AppSettings["chkAddCreditoALiq"] != null
                                                                           && bool.Parse(ConfigurationManager.AppSettings["chkAddCreditoALiq"]));
            this.pnlAddCreditoALiq.Visible = !IsBlockedModeEnabled && (ConfigurationManager.AppSettings["pnlAddCreditoALiq"] != null
                                                                           && bool.Parse(ConfigurationManager.AppSettings["pnlAddCreditoALiq"]));
            this.gvPagosLiquidacion.Enabled = !IsBlockedModeEnabled && (ConfigurationManager.AppSettings["gvPagosLiquidacion"] != null
                                                                           && bool.Parse(ConfigurationManager.AppSettings["gvPagosLiquidacion"]));
            this.chkAddNewPago.Visible = !IsBlockedModeEnabled && (ConfigurationManager.AppSettings["chkAddNewPago"] != null
                                                                           && bool.Parse(ConfigurationManager.AppSettings["chkAddNewPago"]));
            this.panelNuevoPago.Visible = !IsBlockedModeEnabled && (ConfigurationManager.AppSettings["panelNuevoPago"] != null
                                                                           && bool.Parse(ConfigurationManager.AppSettings["panelNuevoPago"]));
        }

        protected bool GetChequeVisible(string ChequeNum)
        {
            return (ChequeNum.Trim().Length > 0);
        }

        protected string GetChequePrintLink(string iMovID)
        {
            string parameter, ventanatitle = "IMPRIMIR CHEQUE";
            string datosaencriptar;
            datosaencriptar = "iMovID=";
            datosaencriptar += iMovID;
            datosaencriptar += "&";

            parameter = "javascript:url('";
            parameter += "frmPrintCheque.aspx?data=";
            parameter += Utils.encriptacadena(datosaencriptar);
            parameter += "', '";
            parameter += ventanatitle;
            parameter += "',200,200,300,300); return false;";
            return parameter; // "frmPrintCheque.aspx?data=" + Utils.encriptacadena(datosaencriptar);
        }

        protected void gvCreditosEnLiquidacion_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            this.pnlCreditosGridViewResult.Visible = true;
            if(e.Exception != null)
            {
                e.ExceptionHandled = true;
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "quitando pago a credito de liquidacion", e.Exception);
                this.lblCreditosGridViewResult.Text = "ERROR QUITANDO PAGO A CREDITO DE LIQUIDACION, ERROR: " + e.Exception.ToString();
            }
            else
            {
                this.lblCreditosGridViewResult.Text = "EL PAGO A CREDITO FUE ELIMINADO EXITOSAMENTE";
                this.ddlCreditosDisponiblesParaPagar.DataBind();
            }
            this.dvTotalesLiq.DataBind();
        }

        protected void gvCreditosEnLiquidacion_DataBound(object sender, EventArgs e)
        {
            this.dvTotalesLiq.DataBind();
        }

        protected void gvAnticipos_DataBound(object sender, EventArgs e)
        {
            this.dvTotalesLiq.DataBind();
        }

       

        protected void btnAgregarAnticipos_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "INSERT INTO Liquidaciones_Anticipos (LiquidacionID, Anticipos) VALUES (@LiquidacionID,@Anticipos)";
                foreach (GridViewRow row in this.GridViewAnticiposDisponibles.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkRowSelected");
                    if (chk != null && chk.Checked)
                    {
                        comm.Parameters.Clear();
                        comm.Parameters.Add("@LiquidacionID", SqlDbType.Int).Value = this.iLiqID;
                        comm.Parameters.Add("@Anticipos", SqlDbType.Int).Value = (int)this.GridViewAnticiposDisponibles.DataKeys[row.RowIndex]["anticipoID"];
                        comm.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error agregando anticipo a liquidacion", ex);
            }
            finally
            {
                conn.Close();
            }
            this.GridViewAnticiposDisponibles.DataBind();
            this.gvAnticipos.DataBind();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            this.GridViewAnticiposDisponibles.DataBind();
        }

        protected void ddlCiclosAnticipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlProductoresAnticipos.DataBind();
        }

        protected void ddlProductoresAnticipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GridViewAnticiposDisponibles.DataBind();
        }



        
        void handler(object sender, SqlDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Error loading liquidacion, command " + e.Command.CommandText, e.Exception);
            }
        }

        void handlerForSelecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            if (e.Command != null)
            {
                e.Command.CommandTimeout = 120;
            }
        }
        [WebMethod]
        public static string getCreditoSaldo(string creditoId)
        {
            float saldo = dbFunctions.GetSaldoCredito(int.Parse(creditoId));
            return string.Format("$ {0:N2}", saldo);
        }
       

    }
}

