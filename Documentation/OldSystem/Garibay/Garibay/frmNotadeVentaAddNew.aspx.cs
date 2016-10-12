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
    public partial class frmNotadeVentaAddNew : Garibay.BasePage
    {
        dsNV.dtNVdatosDataTable dtNotaVentaData = new dsNV.dtNVdatosDataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(this.IsSistemBanco)
            {
                this.SqlCreditos.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.SqlCreditos.SelectCommand);
                this.sdsNOFertilizante.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.sdsNOFertilizante.SelectCommand);
                this.sdsFertilizantes.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.sdsFertilizantes.SelectCommand);
                this.SqlPagos.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.SqlPagos.SelectCommand);
                this.sdsBoletasNV.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.sdsBoletasNV.SelectCommand);
            }
            if (!this.IsPostBack)
            {
                this.txtFechaNPago.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.divaCredito_CollapsiblePanelExtender.Collapsed = true;
                this.txtFechaproducto.Text = this.txtFecha.Text = this.txtNewFechaEntrada.Text = this.txtNewFechaSalida.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.divFechaSalidaNewBoleta.Attributes.Add("style", this.chkChangeFechaSalidaNewBoleta.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.lblPagos.Text = string.Format("{0:C2}", 0);
                this.pnladdproducto.Visible = true;
                this.pnlBoletas.Visible = true;
                this.pnlCentralData.Visible = false;
                this.pnlGuardarNota.Visible = false;
                this.chkAgregarBoletas.Visible = true;
                this.chkMostrarAgregarProductos.Visible = false;
                this.cmbNombre.DataBind();
                this.cmbNombre.SelectedIndex = 0;
                this.cargadatosProductor(int.Parse(this.cmbNombre.SelectedValue));
                if (Request.QueryString["data"] != null && this.loadqueryStrings(Request.QueryString["data"].ToString()) && this.myQueryStrings != null && this.myQueryStrings["NotaVentaID"] != null)
                {

                    this.txtNotaIDToMod.Text = this.lblNumOrdenDeSalida.Text = this.myQueryStrings["NotaVentaID"].ToString();
                    this.LoadNotaVenta(this.myQueryStrings["NotaVentaID"].ToString());
                    this.pnladdproducto.Visible = true;
                    this.pnlBoletas.Visible = this.pnlCentralData.Visible = this.chkAgregarBoletas.Visible = true;
                    this.btnAgregarNotaV.Visible = false;
                    this.ActualizaTotales();
                    this.chkMostrarAgregarProductos.Visible = true;
                    this.ddlGrupos.DataBind();
                    this.ddlProductos.DataBind();
                    this.ddlBodega.DataBind();
                    this.changeExistencia();
                    this.pnlGuardarNota.Visible = true;
                }
                this.divPagoMovCaja.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.divMovBanco.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "TRANSFERENCIA" || this.cmbTipodeMovPago.SelectedItem.Text == "DEPOSITO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.divboletas.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "BOLETA" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.divCheque.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.divDiesel.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "TARJETA DIESEL" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.cmbTipodeMovPago.DataBind();
                this.imgBienPago.Visible = false;
                this.imgMalPago.Visible = false;
                this.pnlNewPago.Visible = false;
                this.AddJSToControls();
                this.drpdlGrupoCatalogosCajaChica.DataBind();
                this.drpdlGrupoCuentaFiscal.DataBind();
                this.drpdlGrupoCatalogosCajaChica.SelectedValue = "14";
                this.drpdlGrupoCuentaFiscal.SelectedValue = "14";
               
                
            }
            this.gvBoletas.Visible = !this.IsSistemBanco;
            this.chkAgregarBoletas.Visible = !this.IsSistemBanco;
            this.grvPagos.Visible = !this.IsSistemBanco;
            this.pnlAgregarNuevoPago.Visible = !this.IsSistemBanco;
            this.chkAddNewPago.Visible = !this.IsSistemBanco;
            this.changeExistencia();
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            string sQuery;
            int newID=-1;
            try
            {
                if (!this.IsSistemBanco && !this.chkTieneBoletas.Checked)
                {   
                    newID = dbFunctions.insertSalidaDeProducto(int.Parse(this.ddlProductos.SelectedValue),int.Parse(this.ddlBodega.SelectedValue), 3, DateTime.Parse(txtFechaproducto.Text), double.Parse(this.txtCantidad.Text), "SE AGREGÓ ESTA ENTRADA POR LA NOTA DE VENTA: " + this.lblNumOrdenDeSalida.Text, int.Parse(this.cmbCiclo.SelectedValue), 0, this.UserID);
                }
                conn.Open();
                sQuery = "SELECT NDVdetalleID FROM NotasdeVenta_detalle WHERE (notadeventaID = @notadeventaID AND productoID = @productoID AND precio = @precio)";
                sQuery = dbFunctions.UpdateSDSForSisBanco(sQuery);
                SqlCommand sqlComm = new SqlCommand(sQuery, conn);
                sqlComm.Parameters.Add("@notadeventaID", SqlDbType.Int).Value = int.Parse(this.lblNumOrdenDeSalida.Text);
                double precio = 0;
                double.TryParse(this.txtPrecio.Text, out precio);
                sqlComm.Parameters.Add("@precio", SqlDbType.Float).Value = precio;
                sqlComm.Parameters.Add("@productoId", SqlDbType.Float).Value = int.Parse(this.ddlProductos.SelectedValue);
                int notaVentaDetalleID = -1;
                if(this.IsSistemBanco)
                {
                    sqlComm.CommandText = dbFunctions.UpdateSDSForSisBanco(sqlComm.CommandText);
                }
                if(sqlComm.ExecuteScalar()!= null && int.TryParse(sqlComm.ExecuteScalar().ToString(),out notaVentaDetalleID) && notaVentaDetalleID!=-1)
                {   
                    sQuery = "UPDATE NotasdeVenta_detalle SET  cantidad=cantidad+@cantidad WHERE (NDVdetalleID = @NDVdetalleID)";
                    sqlComm = new SqlCommand(sQuery, conn);
                    double cantidad = 0;
                    double.TryParse(this.txtCantidad.Text, out cantidad);
                    sqlComm.Parameters.Add("@cantidad", SqlDbType.Int).Value = cantidad;
                    sqlComm.Parameters.Add("@NDVdetalleID", SqlDbType.Int).Value = notaVentaDetalleID;
                    sqlComm.CommandText = dbFunctions.UpdateSDSForSisBanco(sqlComm.CommandText);
                    sqlComm.ExecuteNonQuery();                    
                }
                else
                {
                    sQuery= "INSERT INTO NotasdeVenta_detalle (productoID, bodegaID, cantidad, precio, notadeventaID, userID, cicloID, fecha, tieneBoletas, salidaprodID) ";
                    sQuery += " VALUES (@productoID, @bodegaID, @cantidad, @precio, @notadeventaID, @userID, @cicloID, @fecha, @tieneBoletas, @salidaprodID)";
                    sqlComm = new SqlCommand(sQuery, conn);
                    sqlComm.Parameters.Add("@productoID",SqlDbType.Int).Value=int.Parse(this.ddlProductos.SelectedValue);
                    sqlComm.Parameters.Add("@bodegaID",SqlDbType.Int).Value=int.Parse(this.ddlBodega.SelectedValue);
                    sqlComm.Parameters.Add("@cantidad",SqlDbType.Float).Value=double.Parse(this.txtCantidad.Text);
                    sqlComm.Parameters.Add("@precio",SqlDbType.Float).Value=double.Parse(this.txtPrecio.Text);
                    sqlComm.Parameters.Add("@notadeventaID",SqlDbType.Int).Value=int.Parse(this.lblNumOrdenDeSalida.Text);
                    sqlComm.Parameters.Add("@userID",SqlDbType.Int).Value=this.UserID;
                    sqlComm.Parameters.Add("@cicloID",SqlDbType.Int).Value=int.Parse(this.cmbCiclo.SelectedValue);
                    sqlComm.Parameters.Add("@fecha",SqlDbType.DateTime).Value=DateTime.Parse(this.txtFechaproducto.Text);
                    sqlComm.Parameters.Add("@tieneBoletas",SqlDbType.Bit).Value=this.chkTieneBoletas.Checked;
                    sqlComm.Parameters.Add("@salidaprodID",SqlDbType.Int).Value=newID;
                    sqlComm.CommandText = dbFunctions.UpdateSDSForSisBanco(sqlComm.CommandText);
                    sqlComm.ExecuteNonQuery();
                }
                this.grdvProNotasVenta.DataBind();
                this.grdvProNotasVenta0.DataBind();
                this.ActualizaTotales();
                this.btnGuardaNotaVenta_Click(null, null);
            }   
            catch(Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "ERROR AL AGREGAR PRODUCTO A NOTA DE VENTA " + this.lblNumOrdenDeSalida.Text, ref ex);
            }
            finally
            {
                conn.Close();
            }
            this.changeExistencia();

        }
        private void AddJSToControls()
        {

            string parameter;
            parameter = "javascript:url('";
            parameter += "frmAddQuickProductor.aspx";
            parameter += "', '";
            parameter += "Agregar Productor Rápido";
            parameter += "',0,200,400,500); return false;";

            this.btnAgregarClienteRapido.Attributes.Clear();
            this.btnAgregarClienteRapido.Attributes.Add("onClick", parameter);

            JSUtils.AddDisableWhenClick(ref this.btnActualizarListaBoletas, this);
            JSUtils.AddDisableWhenClick(ref this.btnActulizarcmbClientes, this);
            JSUtils.AddDisableWhenClick(ref this.btnAddPago, this);
            JSUtils.AddDisableWhenClick(ref this.btnAgregarBoleta, this);
            JSUtils.AddDisableWhenClick(ref this.btnAgregarNotaV, this);
            JSUtils.AddDisableWhenClick(ref this.btnAgregarProducto, this);
            JSUtils.AddDisableWhenClick(ref this.btnGuardaNotaVenta, this);
            String sOnchagemov = "checkValueInListNotasVentas2(";
            sOnchagemov += "this" + ",'";
            sOnchagemov += this.divDiesel.ClientID + "','";
            sOnchagemov += this.divPagoMovCaja.ClientID + "','";
            sOnchagemov += this.divboletas.ClientID + "','";
            sOnchagemov += this.divMovBanco.ClientID + "','";
            sOnchagemov += this.divCheque.ClientID + "')";
            this.cmbTipodeMovPago.Attributes.Add("onChange", sOnchagemov);

            string sQuery = "", strRedirect = "";
            sQuery = "notaventaID=" + this.txtNotaIDToMod.Text + "&productorID=" + this.cmbNombre.SelectedValue;
            sQuery = Utils.GetEncriptedQueryString(sQuery);
            strRedirect = "frmBoletaNewQuick.aspx";
            strRedirect += sQuery;

            String sOnClick = "popupCenteredFACBOL('";
            sOnClick += strRedirect;
            sOnClick += "',600, 600); return false;";
            this.btnAgregarBoleta.Attributes.Add("onClick", sOnClick);
        }
        protected bool cargadatosProductor(int productorid)
        {
            Boolean result = false;
            string qrySel = "SELECT     Productores.municipio, Productores.domicilio, Productores.poblacion, Estados.estado, Productores.telefono, Productores.celular, Productores.IFE ";
            qrySel += "FROM         Productores INNER JOIN Estados ON Productores.estadoID = Estados.estadoID WHERE productorID=@prodID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdsel = new SqlCommand(qrySel, conGaribay);
            conGaribay.Open();
            try
            {
                cmdsel.Parameters.Add("@prodID", SqlDbType.Int).Value = productorid;
                SqlDataReader datos;
                datos = cmdsel.ExecuteReader();
                if (!datos.HasRows)
                {
                    result = false;
                }
                else
                    if (datos.Read())
                    {
                        this.cmbNombre.DataBind();
                        this.cmbNombre.SelectedValue = productorid.ToString();
                        this.txtDestino.Text = datos["municipio"].ToString();
                        this.txtDomicilio.Text = datos["domicilio"].ToString();
                        this.txtPoblacion.Text = datos["poblacion"].ToString();
                        this.txtEstado.Text = datos["estado"].ToString();
                        this.txtTelefono.Text = datos["telefono"].ToString();
                        this.txtCelular.Text = datos["celular"].ToString();
                        this.txtIFE.Text = datos["IFE"].ToString();
                        this.txtNombrePago.Text = this.cmbNombre.SelectedItem.Text;
                        result = true;
                    }
                //Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.SELECT, this.UserID, "SE CARGARON LOS DATOS DE EL PRODUCTOR EN LA NOTA DE VENTA", Request.Url.ToString());

            }
            catch (Exception exc)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "valor cargando datos de productor: " + productorid.ToString(), ref exc);
                result = false;
            }
            finally
            {
                conGaribay.Close();
            }
            return result;
        }
        
        private void LoadNotaVenta(String idnota)
        {

            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            SqlConnection connDetalle = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                //dsFacturasClientesTableAdapters.FacturasClientesVentaTableAdapter ta = new dsFacturasClientesTableAdapters.FacturasClientesVentaTableAdapter();
                String query = "Select * from Notasdeventa where notadeventaID =  @notadeventaID";
                if (this.IsSistemBanco)
                {
                    query = dbFunctions.UpdateSDSForSisBanco(query);
                }
                conn.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter(query, conn);
                SqlCommandBuilder sqlBuilder = new SqlCommandBuilder(sqlDA);
                sqlDA.SelectCommand.Parameters.Add("@notadeventaID", SqlDbType.Int).Value = idnota;

                sqlDA.Fill(this.dtNotaVentaData);
                if (this.dtNotaVentaData.Rows.Count != 1)
                {
                    this.Response.Redirect("frmNotadeVentaAddNew.aspx");
                }
                this.cmbNombre.DataBind();
                dsNV.dtNVdatosRow actualRow = ((dsNV.dtNVdatosRow)this.dtNotaVentaData.Rows[0]);
                this.cargadatosProductor(actualRow.productorID);
                this.cmbNombre.SelectedValue = actualRow.productorID.ToString();
                this.cmbCiclo.DataBind();
                this.cmbCiclo.SelectedValue = actualRow.cicloID.ToString();
                this.txtFolio.Text = actualRow.folio;
                this.txtFecha.Text = actualRow.fecha.ToString("dd/MM/yyyy");
                this.lblTotal.Text = string.Format("{0:C2}", actualRow.total);
                this.lblSubtotal.Text = string.Format("{0:C2}", actualRow.subtotal);
                this.lblIva.Text = string.Format("{0:C2}", actualRow.iva);
                this.txtObservaciones.Text = actualRow.observaciones;                
                this.lnkImprimePagare.Visible = this.chkaCredito.Checked = actualRow.acredito;               
                this.txtTractorCamion.Text = actualRow.tractorcamion;
                if (actualRow.acredito)
                {
                    this.ddlCredito.DataBind();
                    this.ddlCredito.SelectedValue = actualRow.creditoID.ToString();
                    this.divaCredito_CollapsiblePanelExtender.Collapsed = false;
                    this.lnkImprimePagare.Visible = true;
                    this.chkNVPagada.Checked = dbFunctions.IsCreditoPagado(actualRow.creditoID);
                    this.chkNVPagada.Enabled = false;
                }
                else
                {
                    this.divaCredito_CollapsiblePanelExtender.Collapsed = true;
                    this.chkNVPagada.Checked = actualRow.pagada;
                    this.chkNVPagada.Enabled = true;
                }
                this.ddltipoCalculoIntereses.SelectedValue = actualRow.tipocalculodeinteresID.ToString();
                this.txtPermiso.Text = actualRow.numeropermiso;
                this.txtTransportista.Text = actualRow.transportista;
                this.txtNombreChofer.Text = actualRow.nombrechofer;
                this.txtColor.Text = actualRow.color;
                this.txtPlacas.Text = actualRow.placas;

                if(!string.IsNullOrEmpty(actualRow.personaautorizada))
                {
                    this.dropDownListFirmas.SelectedValue = actualRow.personaautorizada;
                    this.CheckBoxPersonaAutorizada.Checked = true;
                    this.pnlFirmasAutorizadas_CollapsiblePanelExtender.Collapsed = false;
                }
                this.txtFechaPagare.Text = actualRow.fechaPagare.ToString("dd/MM/yyyy");
                //JSUtils.OpenNewWindowOnClick(ref this.btnPrintNotaVenta , "frmPrintNotaVenta.aspx" + Utils.GetEncriptedQueryString("notadeventaID=" + this.lblNumOrdenDeSalida.Text), "Imprimir Nota de venta", true);
                JSUtils.OpenNewWindowOnClick(ref this.btnPrintNotaVenta, "frmPrintNotaVenta2010.aspx" + Utils.GetEncriptedQueryString("notadeventaID=" + this.lblNumOrdenDeSalida.Text), "Imprimir Nota de venta", false);
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.NOTAVENTA, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "SE CARGARON LOS DATOS DE LA NOTA DE VENTA No. " + lblNumOrdenDeSalida.Text + ".");
                CreateURLForPagare();

                this.SetBlockMode(actualRow.bloqueada);
            
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Load New nota de venta", this.Request.Url.ToString(), ref ex);
            }
            finally
            {
                conn.Close();
                connDetalle.Close();
            }

        }

        protected void btnAgregarNotaV_Click(object sender, EventArgs e)
        {
            String strRedirect = string.Empty;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();

                comm.CommandText = "INSERT INTO Notasdeventa (productorID, cicloID, userID, Fecha, Folio, Pagada, Total,  Subtotal, Iva ";
                if (this.chkaCredito.Checked)
                { 
                    comm.CommandText += ", creditoID "; 
                }
                comm.CommandText += ",Fechadepago, Interes, Observaciones, acredito, tipocalculodeinteresID,origen, remitente,domicilio, telefono,destino, numeropermiso, transportista,nombrechofer,tractorcamion, color, placas,storeTS,fechainiciobrointereses, fechafincobrointereses, notaDeFertilizante,newNota,fechaPagare)";
                comm.CommandText += " VALUES (@productorID, @cicloID, @userID, @Fecha, @Folio, @Pagada, @Total,  @Subtotal, @Iva";
                if (this.chkaCredito.Checked) 
                { 
                    comm.CommandText += ", @creditoID "; 
                }
                comm.CommandText += ",@Fechadepago, @Interes, @Observaciones, @acredito, @tipocalculodeinteresID, @origen, @remitente, @domicilio, @telefono,@destino, @numeropermiso, @transportista,@nombrechofer,@tractorcamion, @color, @placas, @storeTS, @fechainiciobrointereses, @fechafincobrointereses, @notaDeFertilizante,1,@fechaPagare); select notadeventaID = SCOPE_IDENTITY();";
                comm.Connection = conn;
                comm.Parameters.Add("@productorID", SqlDbType.Int).Value = int.Parse(this.cmbNombre.SelectedValue);
                comm.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(this.cmbCiclo.SelectedValue);
                comm.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                comm.Parameters.Add("@Folio", SqlDbType.VarChar).Value = this.txtFolio.Text;
                comm.Parameters.Add("@Pagada", SqlDbType.Bit).Value = 0;
                comm.Parameters.Add("@Total", SqlDbType.Money).Value = 0;
                comm.Parameters.Add("@Subtotal", SqlDbType.Money).Value = 0;
                comm.Parameters.Add("@Iva", SqlDbType.Money).Value = 0;
                comm.Parameters.Add("@Fechadepago", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(this.txtFecha.Text);
                comm.Parameters.Add("@Interes", SqlDbType.Money).Value = 0;
                comm.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(this.txtFecha.Text);
                comm.Parameters.Add("@Observaciones", SqlDbType.Text).Value = this.txtObservaciones.Text;
                comm.Parameters.Add("@acredito", SqlDbType.Bit).Value = this.chkaCredito.Checked ? 1 : 0;
                comm.Parameters.Add("@tipocalculodeinteresID", SqlDbType.Int).Value = int.Parse(this.ddltipoCalculoIntereses.SelectedValue);
                comm.Parameters.Add("@origen", SqlDbType.VarChar).Value = this.lblOrigen.Text;
                comm.Parameters.Add("@remitente", SqlDbType.VarChar).Value = this.lblremitente.Text;
                comm.Parameters.Add("@domicilio", SqlDbType.VarChar).Value = this.lblDomicilio.Text;
                comm.Parameters.Add("@telefono", SqlDbType.VarChar).Value = this.lblTelefono.Text;
                comm.Parameters.Add("@destino", SqlDbType.VarChar).Value = this.txtDestino.Text;
                comm.Parameters.Add("@numeropermiso", SqlDbType.VarChar).Value = this.txtPermiso.Text;
                comm.Parameters.Add("@transportista", SqlDbType.VarChar).Value = this.txtTransportista.Text;
                comm.Parameters.Add("@nombrechofer", SqlDbType.VarChar).Value = this.txtNombreChofer.Text;
                comm.Parameters.Add("@tractorcamion", SqlDbType.VarChar).Value = this.txtTractorCamion.Text;
                comm.Parameters.Add("@color", SqlDbType.VarChar).Value = this.txtColor.Text;
                if (this.chkaCredito.Checked) { comm.Parameters.Add("@creditoID", SqlDbType.Int).Value = int.Parse(this.ddlCredito.SelectedValue); }
                comm.Parameters.Add("@placas", SqlDbType.VarChar).Value = this.txtPlacas.Text;
                comm.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.Now;
                comm.Parameters.Add("@fechainiciobrointereses", SqlDbType.DateTime).Value = Utils.Now;
                comm.Parameters.Add("@fechafincobrointereses", SqlDbType.DateTime).Value = Utils.Now; /// hay que modificar esta fecha
                comm.Parameters.Add("@notaDeFertilizante", SqlDbType.Bit).Value = false;
                comm.Parameters.Add("@fechaPagare", SqlDbType.DateTime).Value = DateTime.Parse(this.txtFechaPagare.Text);
                if(this.IsSistemBanco)
                {
                    comm.CommandText = dbFunctions.UpdateSDSForSisBanco(comm.CommandText);
                }
                int identity = int.Parse(comm.ExecuteScalar().ToString());
                if (identity > 0)
                {
                    this.lblNumOrdenDeSalida.Text = identity.ToString();
                    comm.CommandText = "UPDATE Notasdeventa SET Folio=@fol where notadeventaID=@notaID";
                    comm.Parameters.Add("@fol", SqlDbType.Int).Value = identity;
                    comm.Parameters.Add("@notaID", SqlDbType.Int).Value = identity;
                    comm.CommandText = dbFunctions.UpdateSDSForSisBanco(comm.CommandText);
                    comm.ExecuteNonQuery();
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.NOTAVENTA, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), "SE AGREGÓ LA NOTA DE VENTA No. " + identity.ToString() + ".");

                    String sQuery = "NotaVentaID=" + identity.ToString();
                    sQuery = Utils.GetEncriptedQueryString(sQuery);
                    strRedirect = "~/frmNotaDeVentaAddNew.aspx" + sQuery;
                }
            }
            catch (System.Exception ex)
            {

                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "AL AGREGAR UNA NOTA DE VENTA", this.Request.Url.ToString(), ref ex);
            }
            finally
            {
                conn.Close();

            }
            if (strRedirect.Trim().Length > 0)
            {
                Response.Redirect(strRedirect);
            }
        }
        protected void cmbNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cargadatosProductor(int.Parse(this.cmbNombre.SelectedValue));
        }
        protected void btnActualizarListaBoletas_Click(object sender, EventArgs e)
        {
            this.gvBoletas.DataBind();
        }
        protected void btnActulizarcmbClientes_Click(object sender, EventArgs e)
        {
            this.cmbNombre.DataBind();
        }
        protected void gvBoletas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if columns are added/ removed please update the index.
                try
                {
                    //                     Button btn = (Button)e.Row.Cells[0].Controls[0];
                    //                     JSUtils.AddConfirmToCtrlOnClick(ref btn, "Realmente desea eliminar la Boleta??", true);
                    HyperLink lnk = (HyperLink)e.Row.FindControl("lnkEditar");
                    JSUtils.OpenNewWindowOnClick(ref lnk, "frmBoletaNewQuick.aspx" + Utils.GetEncriptedQueryString("boletaID=" + this.gvBoletas.DataKeys[e.Row.RowIndex][0].ToString() + "&notaventaID=" + this.txtNotaIDToMod.Text + "&productorID=" + this.cmbNombre.SelectedValue), "Boletas", true);
                    lnk.NavigateUrl = this.Request.Url.ToString();
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "error al meter js a controles en row", ref ex);
                }

            }
        }
        protected void gvBoletas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (e.Keys["boletaID"] != null)
            {
                SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                try
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    conn.Open();

                    comm.CommandText = "DELETE FROM  NotasdeVenta_Boletas WHERE (boletaID = @boletaID);"
                        + " DELETE FROM Boletas WHERE (boletaID = @boletaID);";
                    comm.CommandText = dbFunctions.UpdateSDSForSisBanco(comm.CommandText);
                    comm.Parameters.Add("@boletaID", SqlDbType.Int).Value = e.Keys["boletaID"];
                    comm.ExecuteNonQuery();
                    this.btnGuardaNotaVenta_Click(null, null);
                    this.gvBoletas.DataBind();
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.DELETE, "deleting boleta " + e.Keys["boletaID"].ToString(), ref ex);
                }
                finally
                {
                    conn.Close();
                }
            }
            this.gvBoletas.DataBind();
        }
        private void ActualizaTotales()
        {
            this.grdvProNotasVenta.DataBind();
            this.grdvProNotasVenta0.DataBind();
            double dPagos = this.sumaPagos();
            double iva = 0,  importe = 0;
            foreach (GridViewRow row in this.grdvProNotasVenta.Rows)
            {      
                importe += Utils.GetSafeFloat(row.Cells[10].Text);
            }

            foreach (GridViewRow row in this.grdvProNotasVenta0.Rows)
            {
                importe += Utils.GetSafeFloat(row.Cells[11].Text);

            }
            this.lblSubtotal.Text = importe.ToString();
            if(this.chbIVA.Checked){
                iva = importe * 0.16;
            }
            
            this.lblTotal.Text = (importe - dPagos+iva).ToString();
            this.lblSubtotal.Text = string.Format("{0:c2}", double.Parse(this.lblSubtotal.Text));
            this.lblTotal.Text = string.Format("{0:c2}", double.Parse(this.lblTotal.Text));
            this.lblIva.Text = string.Format("{0:c2}", iva);
            this.lblPagos.Text = string.Format("{0:C2}", dPagos);
        }
        protected double sumaPagos()
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
        protected void grdvProNotasVenta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                try
                {
                    conn.Open();
                    string sQuery=" SELECT     Presentaciones.Presentacion, Unidades.Unidad FROM Presentaciones INNER JOIN ";
                      sQuery+=" Productos ON Presentaciones.presentacionID = Productos.presentacionID INNER JOIN ";
                      sQuery += " Unidades ON Productos.unidadID = Unidades.unidadID where productoID=@productoID ";
                      SqlCommand sqlComm = new SqlCommand(sQuery, conn);

                     // this.grdvProNotasVenta.SelectedIndex = e.Row.RowIndex;
                      sqlComm.Parameters.Add("@productoID", SqlDbType.Int).Value = int.Parse(this.grdvProNotasVenta.DataKeys[e.Row.RowIndex]["productoID"].ToString());

                      SqlDataReader sqlRd = sqlComm.ExecuteReader();
                      if (sqlRd.HasRows && sqlRd.Read())
                      {
                          Label lbl = (Label)e.Row.FindControl("lblPresentacionGrid");

                          if (lbl != null)
                          {
                              lbl.Text = sqlRd["Presentacion"].ToString() + "-" + sqlRd["Unidad"].ToString();
                          }
                      }


                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "error al hacer rowdatabound", ref ex);

                }
                finally
                {

                    conn.Close();
                }


            }

        }
        protected void grdvProNotasVenta_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            this.grdvProNotasVenta.DataBind();
            this.grdvProNotasVenta0.DataBind();
            this.ActualizaTotales();
            this.btnGuardaNotaVenta_Click(null, null);
        }

     

       
        protected void grdvProNotasVenta_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grdvProNotasVenta.EditIndex = e.NewEditIndex;
            this.grdvProNotasVenta.DataBind();
            this.changeExistencia();
            this.ActualizaTotales();
            this.btnGuardaNotaVenta_Click(null, null);
        }

        protected void grdvProNotasVenta_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grdvProNotasVenta.EditIndex = -1;
            this.grdvProNotasVenta.DataBind();
        }

        protected void grdvProNotasVenta_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                CheckBox chkTieneBoleta = (CheckBox)this.grdvProNotasVenta.Rows[e.RowIndex].FindControl("checkBoxTieneBoleta");
                double newCantidad = 0, newPrecio = 0;
                TextBox txtNewCantidad = ((TextBox)(this.grdvProNotasVenta.Rows[e.RowIndex].FindControl("textBoxCantidad")));
                if (txtNewCantidad != null)
                {
                    double.TryParse(txtNewCantidad.Text, out newCantidad);
                }
                TextBox txtNewPrecio = ((TextBox)(this.grdvProNotasVenta.Rows[e.RowIndex].FindControl("textBoxPrecio")));
                if (txtNewPrecio != null)
                {
                    double.TryParse(txtNewPrecio.Text, out newPrecio);
                }
                SqlCommand cmdUpdate = new SqlCommand();
                cmdUpdate.Connection = conn;
                cmdUpdate.CommandText = "update NotasdeVenta_detalle set precio = @precio, cantidad = @cantidad, tieneBoletas = @tieneBoletas where NDVdetalleID = @NDVdetalleID";
                cmdUpdate.CommandText = dbFunctions.UpdateSDSForSisBanco(cmdUpdate.CommandText);
                cmdUpdate.Parameters.Add("@precio", SqlDbType.Float).Value = newPrecio;
                cmdUpdate.Parameters.Add("@cantidad", SqlDbType.Float).Value = newCantidad;
                cmdUpdate.Parameters.Add("@tieneBoletas", SqlDbType.Bit).Value = chkTieneBoleta.Checked;
                cmdUpdate.Parameters.Add("@NDVdetalleID", SqlDbType.Int).Value = (int)(this.grdvProNotasVenta.DataKeys[e.RowIndex]["NDVdetalleID"]);

                cmdUpdate.ExecuteNonQuery();
                if (!this.IsSistemBanco)
                {
                    if (!chkTieneBoleta.Checked)
                    {
                        SqlCommand cmdUpdateSalida = new SqlCommand();
                        cmdUpdateSalida.Connection = conn;
                        cmdUpdateSalida.CommandText = "update SalidadeProductos set cantidad = @cantidad, updateTS = @updateTS, userID = @userID where salidaprodID = (Select salidaprodID from NotasdeVenta_Detalle where NDVdetalleID = @NDVdetalleID)";
                        cmdUpdateSalida.Parameters.Add("@cantidad", SqlDbType.Float).Value = newCantidad;
                        cmdUpdateSalida.Parameters.Add("@NDVdetalleID", SqlDbType.Int).Value = (int)(this.grdvProNotasVenta.DataKeys[e.RowIndex]["NDVdetalleID"]);
                        cmdUpdateSalida.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.Now;
                        cmdUpdateSalida.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;

                        cmdUpdateSalida.ExecuteNonQuery();
                    }
                }
                this.grdvProNotasVenta.EditIndex = -1;
                this.grdvProNotasVenta.DataBind();
                this.ActualizaTotales();
                this.btnGuardaNotaVenta_Click(null, null);
                conn.Close();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "actualizando datos de la row", this.Request.Url.ToString(), ref ex);
            }
            finally
            {
                conn.Close();
            }
            this.grdvProNotasVenta.EditIndex = -1;
            this.grdvProNotasVenta.DataBind();
            this.changeExistencia();
        }


        protected void chbIVA_CheckedChanged(object sender, EventArgs e)
        {
            this.ActualizaTotales();
        }

        

        protected void btnAddPago_Click(object sender, EventArgs e)
        {
            double monto = 0;
            double.TryParse(this.txtMonto.Text, out monto);
            double pagosmade = 0, debt = 0;
            pagosmade = Utils.GetSafeFloat(this.lblPagos.Text);
            if(pagosmade + monto > Utils.GetSafeFloat(this.lblPagos.Text))
            {

                this.pnlNewPago.Visible = true;
                this.lblNewPagoResult.Text = "ERROR EL AGREGAR PAGO, EXCEDE DE LO QUE DEBE, VERIFICAR DATOS DE NUEVO.";
            }
            this.pnlNewPago.Visible = false;
            int cheque = 0;
            bool hayerrorenmonto = false;
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
                    dtRowainsertar2.Observaciones = "Pago a nota de venta.";
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
                        string sqlInsert = "insert into Pagos_NotaVenta (fecha, notadeventaID, movimientoID) VALUES(@fecha, @notadeventaID, @movimientoID)";
                        SqlCommand cmdInsert = new SqlCommand(sqlInsert, conInsertNota);
                        conInsertNota.Open();
                        try
                        {
                            cmdInsert.Parameters.Clear();
                            cmdInsert.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtRowainsertar2.fecha;
                            cmdInsert.Parameters.Add("@notadeventaID", SqlDbType.Int).Value = int.Parse(this.txtNotaIDToMod.Text);
                            cmdInsert.Parameters.Add("@movimientoID", SqlDbType.Int).Value = dtRowainsertar2.movimientoID;
                            int numregistros = cmdInsert.ExecuteNonQuery();
                            if (numregistros != 1)
                            {
                                throw new Exception("ERROR AL INSERTAR RELACION NV - PAGOS. LA DB REGRESÓ QUE SE ALTERARON " + numregistros.ToString() + "REGISTROS");
                            }
                            Logger.Instance.LogUserSessionRecord(Logger.typeModulo.NOTAVENTA, Logger.typeUserActions.UPDATE, this.UserID, "SE INSERTÓ UN PAGO A LA NOTA DE VENTA " + this.txtNotaIDToMod.Text + " EL MOV DE CAJA CHICA FUE: " + dtRowainsertar2.movimientoID.ToString());

                        }
                        catch (Exception ex)
                        {
                            Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL INSERTAR UN PAGO A LA NOTA (CAJA CHICA): " + this.txtNotaIDToMod.Text + ". EX " + ex.Message, this.Request.Url.ToString());

                        }
                        this.pnlNewPago.Visible = true;
                        this.imgBienPago.Visible = true;
                        this.imgMalPago.Visible = false;
                        this.lblNewPagoResult.Text = string.Format(myConfig.StrFromMessages("MOVCAJAADDEDEXITO"), sNewMov);
                        Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDECAJACHICA, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), "AGREGÓ EL MOVIMIENTO DE CAJA CHICA NÚMERO: " + dtRowainsertar2.movimientoID.ToString());
                        this.ActualizaTotales();
                        this.btnGuardaNotaVenta_Click(null, null);
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
                if (this.cmbTipodeMovPago.SelectedValue == "4" || this.cmbTipodeMovPago.SelectedValue == "5")
                {
                    if (dbFunctions.FechaEnPeriodoBloqueado(DateTime.Parse(this.txtFechaNPago.Text), int.Parse(this.cmbCuentaPago.SelectedValue)))
                    {
                        this.pnlNewPago.Visible = true;
                        this.imgBienPago.Visible = false;
                        this.imgMalPago.Visible = true;
                        this.lblNewPagoResult.Text = "EL MOVIMIENTO NO PUEDE SER AGREGADO YA QUE LA FECHA ESTA DENTRO DE UN PERIODO BLOQUEADO<BR />DESBLOQUEE EL PERIODO PARA PERMITIR INGRESAR EL MOVIMIENTO";
                        return;
                    }
                    cheque = 0;
                    hayerrorenmonto = false;
                    monto = 0;
                    double.TryParse(this.txtMonto.Text, out monto);
                    tablaaux = new dsMovBanco.dtMovBancoDataTable();
                    dtRowainsertar = tablaaux.NewdtMovBancoRow();
                    //dtRowainsertar.cicloID = int.Parse(this.drpdlCiclo.SelectedValue);
                    dtRowainsertar.chequecobrado = true;
                    dtRowainsertar.conceptoID = this.cmbTipodeMovPago.SelectedValue == "4" ? 1 : 2;
                    dtRowainsertar.nombre = this.txtNombrePago.Text;
                    dtRowainsertar.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaNPago.Text));
                    //dats de cheque
                    dtRowainsertar.numCheque = this.txtChequeNum.Text.Length > 0 ? int.Parse(this.txtChequeNum.Text) : 0;
                    dtRowainsertar.chequeNombre = "";
                    dtRowainsertar.facturaOlarguillo = "";
                    dtRowainsertar.numCabezas = 0;//this.txtNumCabezas.Text.Length > 0 ? double.Parse(this.txtNumCabezas.Text) : 0;

                    dtRowainsertar.catalogoMovBancoInternoID = int.Parse(this.drpdlCatalogocuentafiscalPago.SelectedValue);
                    if (this.drpdlSubcatalogofiscalPago.SelectedIndex > -1)
                        dtRowainsertar.subCatalogoMovBancoInternoID = int.Parse(this.drpdlSubcatalogofiscalPago.SelectedValue);
                    //if (!this.chkMostrarFiscales.Checked)
                    //{
                    //    dtRowainsertar.catalogoMovBancoFiscalID = int.Parse(this.drpdlCatalogoInterno.SelectedValue);
                    //    if (this.drpdlSubcatologointerna.SelectedIndex > -1)
                    //        dtRowainsertar.subCatalogoMovBancoFiscalID = int.Parse(this.drpdlSubcatologointerna.SelectedValue);
                    ////}
                    //else
                    //{
                    dtRowainsertar.catalogoMovBancoFiscalID = int.Parse(this.drpdlCatalogocuentafiscalPago.SelectedValue);
                    if (this.drpdlSubcatalogofiscalPago.SelectedIndex > -1)
                        dtRowainsertar.subCatalogoMovBancoFiscalID = int.Parse(this.drpdlSubcatalogofiscalPago.SelectedValue);
                    //}

                    //if (cmbTipodeMov.SelectedIndex == 0)
                    //{//ES CARGO

                    dtRowainsertar.cargo = 0.00;
                    dtRowainsertar.abono = this.txtMonto.Text.Length > 0 ? double.Parse(this.txtMonto.Text) : 0;

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


                    serror = "";
                    tipo = "";
                    dtRowainsertar.cuentaID = int.Parse(this.cmbCuentaPago.SelectedValue);
                    //CHECAMOS SI SE VA A AGREGAR A UN CRÉDITO FINANCIERO
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
                            commVenta.CommandText = "INSERT INTO Pagos_NotaVenta(fecha, notadeventaID, movbanID) VALUES (@fecha,@notadeventaID,@movbanID) ";
                            commVenta.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtRowainsertar.fecha;
                            commVenta.Parameters.Add("@notadeventaID", SqlDbType.Int).Value = int.Parse(this.txtNotaIDToMod.Text);
                            commVenta.Parameters.Add("@movbanID", SqlDbType.Int).Value = dtRowainsertar.movBanID;

                            if (commVenta.ExecuteNonQuery() != 1)
                            {
                                throw new Exception("This must almost never happen");
                            }
                        }


                        catch (System.Exception ex)
                        {
                            Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error adding new movbanco->notadeventa", ref ex);
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
                    bool result;
                    int iChequeRecID = -1;
                    cheque = 0;
                    hayerrorenmonto = false;
                    monto = 0;
                    hayerrorenmonto = !double.TryParse(this.txtMonto.Text, out monto);
                    if (hayerrorenmonto)
                    {
                        this.pnlNewPago.Visible = true;
                        this.imgMalPago.Visible = true;
                        this.imgBienPago.Visible = false;
                        this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, ERROR EN MONTO, ESCRIBA CANTIDAD VÁLIDA";
                        return;
                    }

                    SqlConnection connVenta = new SqlConnection(myConfig.ConnectionInfo);
                    try
                    {


                        connVenta.Open();
                        SqlCommand commVenta = new SqlCommand();
                        commVenta.Connection = connVenta;
                        commVenta.CommandText = "INSERT INTO ChequesRecibidos(numcheque, ANombreDe, fecha, monto, bancoID, userID) VALUES (@numcheque, @ANombreDe, @fecha, @monto, @bancoID, @userID); SELECT NewID = SCOPE_IDENTITY();";
                        commVenta.Parameters.Clear();
                        commVenta.Parameters.Add("@numcheque", SqlDbType.VarChar).Value = this.txtChequeNum.Text;
                        commVenta.Parameters.Add("@ANombreDe", SqlDbType.VarChar).Value = this.txtNombrePago.Text;
                        commVenta.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaNPago.Text));
                        commVenta.Parameters.Add("@monto", SqlDbType.Float).Value = this.txtMonto.Text.Length > 0 ? double.Parse(this.txtMonto.Text) : 0;
                        commVenta.Parameters.Add("@bancoID", SqlDbType.Int).Value = int.Parse(this.drpdlBancos.SelectedValue);
                        commVenta.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;

                        iChequeRecID = int.Parse(commVenta.ExecuteScalar().ToString());
                        if (iChequeRecID == -1)
                        {
                            throw new Exception("This must almost never happen");
                        }

                        commVenta.CommandText = "INSERT INTO Pagos_NotaVenta(fecha, notadeventaID, chequesRecibidoID) VALUES (@fecha,@notadeventaID,@chequesRecibidoID) ";
                        commVenta.Parameters.Clear();
                        commVenta.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaNPago.Text));
                        commVenta.Parameters.Add("@notadeventaID", SqlDbType.Int).Value = int.Parse(this.txtNotaIDToMod.Text);
                        commVenta.Parameters.Add("@chequesRecibidoID", SqlDbType.Int).Value = iChequeRecID;

                        if (commVenta.ExecuteNonQuery() != 1)
                        {
                            throw new Exception("This must almost never happen");
                        }
                        result = true;
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error adding new cheque->notadeventa", ref ex);
                        result = false;
                    }
                    finally
                    {
                        connVenta.Close();
                    }

                    if (result)
                    {
                        this.cmbTipodeMovPago.SelectedIndex = 0;

                        this.pnlNewPago.Visible = true;
                        this.imgBienPago.Visible = true;
                        this.imgMalPago.Visible = false;
                        this.lblNewPagoResult.Text = "EL CHEQUE DE PRODUCTOR NUMERO :" + iChequeRecID + " SE HA AGREGADO SATISFACTORIAMENTE";
                    }
                    else
                    {
                        this.cmbTipodeMovPago.SelectedIndex = 0;
                        this.pnlNewPago.Visible = true;
                        this.imgBienPago.Visible = false;
                        this.imgMalPago.Visible = true;
                        this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL CHEQUE DE PRODUCTOR";
                    }
                }
                else if (this.cmbTipodeMovPago.SelectedValue == "3")//Boleta
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


                        addComm.Parameters.Add("@productorID", SqlDbType.Int).Value = int.Parse(this.cmbNombre.SelectedValue);
                        addComm.Parameters.Add("@NombreProductor", SqlDbType.VarChar).Value = this.cmbNombre.Text;
                        //    //addComm.Parameters.AddWithValue("@NombreProductor", newRow.NombreProductor);
                        addComm.Parameters.Add("@NumeroBoleta", SqlDbType.VarChar).Value = this.txtNewNumBoleta.Text;
                        //    //addComm.Parameters.AddWithValue("@NumeroBoleta", newRow.NumeroBoleta);
                        //    //  addComm.Parameters.AddWithValue("@Ticket", newRow.Ticket);
                        addComm.Parameters.Add("@Ticket", SqlDbType.VarChar).Value = this.txtNewTicket.Text;
                        //    //  addComm.Parameters.AddWithValue("@bodegaID", newRow.bodegaID);
                        addComm.Parameters.Add("@bodegaID", SqlDbType.Int).Value = int.Parse(this.ddlNewBoletaBodega.SelectedValue);

                        addComm.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(this.cmbCiclo.SelectedValue);

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
                        //    // addComm.Parameters.AddWithValue("@FechaSalida", newRow.FechaSalida);

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




                        //    //addComm.Parameters.AddWithValue("@userID", newRow.userID);
                        addComm.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;

                        //(PesoNetoEntrada * float.Parse(txtPrecio.Text) - (chkbSecado.Checked ? Utils.getDesctoSecado(humedad, PesoNetoEntrada) : 0)).ToString();
                        //addComm.Parameters.Add("@totalapagar", SqlDbType.Float).Value = (dPesoNetoEntrada*float.Parse(dPrecio.ToString()); -dPesoNetoSalida;

                        int newbol = int.Parse(addComm.ExecuteScalar().ToString());
                        string qr = "INSERT INTO Pagos_NotaVenta  (fecha, notadeventaID, boletaID) VALUES     (@fecha, @notadeventaID, @boletaID)";
                        addComm.Parameters.Clear();
                        addComm.CommandText = qr;
                        addComm.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtFechaEntrada;
                        addComm.Parameters.Add("@notadeventaID", SqlDbType.Int).Value = int.Parse(this.lblNumOrdenDeSalida.Text);
                        addComm.Parameters.Add("@boletaID", SqlDbType.Int).Value = newbol;
                        addComm.ExecuteNonQuery();


                        this.ddlNewBoletaProductor.SelectedValue = this.cmbNombre.SelectedValue;
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
                        Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error agregando boleta En Nota de Venta", this.Request.Url.ToString(), ref ex);
                        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(Session["USERID"].ToString()), "Error Insertando Nueva Boleta EX:" + ex.Message, this.Request.Url.ToString());
                    }
                    finally
                    {
                        sqlConn.Close();
                    }

                    ////  Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.SELECT, this.UserID, "EL TIEMPO QUE TARDÓ EN AGREGAR UNA BOLETA A LA LIQUIDACIÓN " + this.lblNumLiquidacion.Text + " FUE: " + stop.Subtract(start).TotalMilliseconds.ToString() + " MILISECONDS", this.Request.Url.ToString());



                }
                else if (this.cmbTipodeMovPago.SelectedValue == "2")//TARJETA DIESEL
                {

                    serror = "";
                    try
                    {
                        //if (this.chbMostrarPnlAddTarjDiesel.Checked)
                        //{
                        if (dbFunctions.addTarjetaDiesel(int.Parse(this.lblNumOrdenDeSalida.Text),
                                                     int.Parse(txtfoliodiesel.Text),
                                                     float.Parse(this.txtMonto.Text),
                                                     float.Parse(this.txtLitrosTarjetaDiesel.Text),
                                                     ref serror,
                                                     int.Parse(this.Session["USERID"].ToString()), 1))
                        {

                            ActualizaTotales();
                            this.grvPagos.DataBind();
                            //this.txtFolio.ReadOnly = true;
                        }

                        else
                        {
                            throw new Exception(serror);
                        }
                        //}
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
            this.ActualizaTotales();
            //btnGuardaNotaVenta_Click(null, null);
            this.limpiaPagos();


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



                        String query = "SELECT     Pagos_NotaVenta.fecha, Boletas.Ticket, Boletas.totalapagar ";
                        query += " FROM         Boletas INNER JOIN ";
                        query += " Pagos_NotaVenta ON Boletas.boletaID = Pagos_NotaVenta.boletaID ";
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
                    else if (this.grvPagos.DataKeys[e.Row.RowIndex]["chequesRecibidoID"].ToString() != "")
                    {
                        String query = "SELECT     Pagos_NotaVenta.fecha, ChequesRecibidos.numcheque, ChequesRecibidos.monto, Bancos.nombre ";
                        query += " FROM         Pagos_NotaVenta INNER JOIN ";
                        query += "                       ChequesRecibidos ON Pagos_NotaVenta.chequesRecibidoID = ChequesRecibidos.chequeRecibidoID INNER JOIN ";
                        query += " Bancos ON ChequesRecibidos.bancoID = Bancos.bancoID ";
                        query += " WHERE     (ChequesRecibidos.chequeRecibidoID = @chequeID)";

                        SqlCommand comm = new SqlCommand(query, conn);
                        conn.Open();
                        comm.Parameters.Add("@chequeID", SqlDbType.Int).Value = int.Parse(this.grvPagos.DataKeys[e.Row.RowIndex]["chequesRecibidoID"].ToString());
                        SqlDataReader rd = comm.ExecuteReader(); ;

                        //if columns are added/ removed please update the index.
                        if (rd.HasRows && rd.Read())
                        {
                            Label lbl = (Label)e.Row.FindControl("Label10");

                            lbl.Text = rd["numcheque"].ToString();

                            lbl = (Label)e.Row.FindControl("Label9");
                            lbl.Text = "CHEQUE";



                            lbl = (Label)e.Row.FindControl("Label12");
                            lbl.Text = string.Format("{0:c2}", Utils.GetSafeFloat(rd["monto"].ToString()));
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

                e.Row.Cells[7].Text = "TOTAL";


                e.Row.Cells[8].Text = string.Format("{0:C2}", sumaPagos());
            }

        }

        protected void grvPagos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            SqlPagos.DeleteCommand = "DELETE FROM Pagos_NotaVenta WHERE (Pagos_NotaVenta.PagoNotaVentaID = @PagoNotaVentaID)";
            SqlPagos.DeleteParameters.Add("@PagoNotaVentaID", e.Keys["PagoNotaVentaID"].ToString());

        }
        protected void grvPagos_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            this.grvPagos.DataBind();
            this.ActualizaTotales();
        }

        protected void btnGuardaNotaVenta_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                String query = "Select * from Notasdeventa where notadeventaID =  @NVID";
                if (this.IsSistemBanco) 
                {
                    query = dbFunctions.UpdateSDSForSisBanco(query);
                }
                conn.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter(query, conn);
                SqlCommandBuilder sqlBuilder = new SqlCommandBuilder(sqlDA);
                sqlDA.SelectCommand.Parameters.Add("@NVID", SqlDbType.Int).Value = int.Parse(this.lblNumOrdenDeSalida.Text);
                
                sqlDA.Fill(this.dtNotaVentaData);
                if (this.dtNotaVentaData.Rows.Count != 1)
                {
                    this.imgBien.Visible = false;
                    this.imgMal.Visible = this.lblNotaVentaResult.Visible = true;
                    this.lblNotaVentaResult.Text = "ESTO ES VERGONZOSO, HA OCURRIDO UNA EXCEPCION Y NO SE PUDO GUARDAR LA NOTA,<BR> YA QUE NO SE PUDIERON LEER LOS DATOS DE ÉSTA<BR> POR FAVOR ESPERE UN MOMENTO E INTENTELO DE NUEVO";
                }
                dsNV.dtNVdatosRow row = (dsNV.dtNVdatosRow)this.dtNotaVentaData.Rows[0];
                row.cicloID = int.Parse(this.cmbCiclo.SelectedValue);
                row.fecha = DateTime.Parse(this.txtFecha.Text);
                //row.iva = this.chbIVA.Checked;
                row.subtotal = Utils.GetSafeFloat(this.lblSubtotal.Text);
                row.iva = this.chbIVA.Checked ? Utils.GetSafeFloat(this.lblIva.Text) : 0;
                //row.RETIVA = Utils.GetSafeFloat(this.txtRETIVA.Text);
                row.total = row.subtotal + row.iva;// -row.RETIVA;
                row.observaciones = this.txtObservaciones.Text;
                row.transportista = this.txtTransportista.Text;
                row.telefono = this.txtTelefono.Text;
                row.domicilio = this.txtDomicilio.Text;
                row.numeropermiso = this.txtPermiso.Text;
                row.nombrechofer = this.txtNombreChofer.Text;
                row.tipocalculodeinteresID = int.Parse(this.ddltipoCalculoIntereses.SelectedValue);
                row.tractorcamion = this.txtTractorCamion.Text;
                row.color = this.txtColor.Text;
                row.placas = this.txtPlacas.Text;
                row.productorID = int.Parse(this.cmbNombre.SelectedValue);
                row.fechaPagare = DateTime.Parse(this.txtFechaPagare.Text);
                if (!this.chkaCredito.Checked)
                    row.pagada = this.chkNVPagada.Checked;
                else
                    row.pagada = dbFunctions.IsCreditoPagado(int.Parse(this.ddlCredito.SelectedValue));
                if(this.CheckBoxPersonaAutorizada.Checked && this.dropDownListFirmas.SelectedValue != null)
                {
                    row.personaautorizada = this.dropDownListFirmas.SelectedItem.Text;
                }
                else
                {
                    row.personaautorizada = null;
                }
               
                if (this.chkaCredito.Checked)
                {      
                    row.creditoID = int.Parse(this.ddlCredito.SelectedValue);
                    row.acredito = true;
                    this.lnkImprimePagare.Visible = true;
                    CreateURLForPagare();
        
                }
                else
                {
                    row.creditoID = -1;
                    row.acredito = false;
                    this.lnkImprimePagare.Visible = false;

                }
               if (sqlDA.Update(this.dtNotaVentaData) != 1)
               {
                  this.imgBien.Visible = false;
                  this.imgMal.Visible = this.pnlNotaVentaResult.Visible = true;
                  this.lblNotaVentaResult.Text = "ESTO ES VERGONZOSO, HA OCURRIDO UNA EXCEPCION Y NO SE PUDO GUARDAR LA NOTA,<BR> YA QUE NO SE PUDIERON ACTUALIZAR LOS DATOS DE ÉSTA<BR> POR FAVOR ESPERE UN MOMENTO E INTENTELO DE NUEVO";
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "err updating boleta", ref ex);
                this.imgBien.Visible = false;
                this.imgMal.Visible = this.pnlNotaVentaResult.Visible = true;
                this.lblNotaVentaResult.Text = "ESTO ES VERGONZOSO, HA OCURRIDO UNA EXCEPCION Y NO SE PUDO GUARDAR LA NOTA.<BR>POR FAVOR ESPERE UN MOMENTO E INTENTELO DE NUEVO<BR>Descripción del error<BR>" + ex.Message;
            }
            finally
            {
                conn.Close();
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.NOTAVENTA, Logger.typeUserActions.UPDATE, this.UserID, "GUARDO LA NOTA DE VENTA: " + this.lblNumOrdenDeSalida.Text);
                this.imgMal.Visible = false;
                this.imgBien.Visible = this.pnlNotaVentaResult.Visible = true;
                this.lblNotaVentaResult.Text = "LA NOTA SE HA GUARDADO  EXITOSAMENTE";
               
            }
        }

        protected void cmbTipodeMovPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpdlGrupoCatalogosInternaPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

     

        

        protected void ddlProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.changeExistencia();
        }
        private void changeExistencia()
        {
            if(this.ddlProductos.SelectedValue != null && this.ddlProductos.SelectedValue.ToString().Length > 0)
            {
                this.txtExistencia.Text = string.Format("{0:f2}", dbFunctions.sacaExistenciadeproducto(int.Parse(this.ddlProductos.SelectedValue), int.Parse(this.ddlBodega.SelectedValue)));
            }
            else
            {
                this.txtExistencia.Text = "0.00";
            }
    
        }

        protected void ddlGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlProductos.DataBind();
            this.changeExistencia();
        }
      
       
        protected void grdvProNotasVenta0_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grdvProNotasVenta0.EditIndex = -1;
            this.grdvProNotasVenta0.DataBind();
        }

        protected void grdvProNotasVenta0_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                CheckBox chkTieneBoleta = (CheckBox)this.grdvProNotasVenta0.Rows[e.RowIndex].FindControl("checkBoxTieneBoleta");
                double newCantidad = 0, newPrecio = 0;
                TextBox txtNewCantidad = ((TextBox)(this.grdvProNotasVenta0.Rows[e.RowIndex].FindControl("textBoxCantidad")));
                if (txtNewCantidad != null)
                {
                    double.TryParse(txtNewCantidad.Text, out newCantidad);
                }
                TextBox txtNewPrecio = ((TextBox)(this.grdvProNotasVenta0.Rows[e.RowIndex].FindControl("textBoxPrecio")));
                if (txtNewPrecio != null)
                {
                    double.TryParse(txtNewPrecio.Text, out newPrecio);
                }
                SqlCommand cmdUpdate = new SqlCommand();
                cmdUpdate.Connection = conn;
                cmdUpdate.CommandText = "update NotasdeVenta_detalle set precio = @precio, cantidad = @cantidad, tieneBoletas = @tieneBoletas where NDVdetalleID = @NDVdetalleID";
                cmdUpdate.Parameters.Add("@precio", SqlDbType.Float).Value = newPrecio;
                cmdUpdate.Parameters.Add("@cantidad", SqlDbType.Float).Value = newCantidad;
                cmdUpdate.Parameters.Add("@tieneBoletas", SqlDbType.Bit).Value = chkTieneBoleta.Checked;
                cmdUpdate.Parameters.Add("@NDVdetalleID", SqlDbType.Int).Value = (int)(this.grdvProNotasVenta0.DataKeys[e.RowIndex]["NDVdetalleID"]);

                cmdUpdate.ExecuteNonQuery();
                if (!chkTieneBoleta.Checked)
                {
                    SqlCommand cmdUpdateSalida = new SqlCommand();
                    cmdUpdateSalida.Connection = conn;
                    cmdUpdateSalida.CommandText = "update SalidadeProductos set cantidad = @cantidad, updateTS = @updateTS, userID = @userID where salidaprodID = (Select salidaprodID from NotasdeVenta_Detalle where NDVdetalleID = @NDVdetalleID)";
                    cmdUpdateSalida.Parameters.Add("@cantidad", SqlDbType.Float).Value = newCantidad;
                    cmdUpdateSalida.Parameters.Add("@NDVdetalleID", SqlDbType.Int).Value = (int)(this.grdvProNotasVenta0.DataKeys[e.RowIndex]["NDVdetalleID"]);
                    cmdUpdateSalida.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.Now;
                    cmdUpdateSalida.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;

                    cmdUpdateSalida.ExecuteNonQuery();
                }
                this.changeExistencia();
                this.grdvProNotasVenta0.EditIndex = -1;
                this.grdvProNotasVenta0.DataBind();
                this.ActualizaTotales();
                this.btnGuardaNotaVenta_Click(null, null);
                conn.Close();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "actualizando datos de la row", this.Request.Url.ToString(), ref ex);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void grdvProNotasVenta_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            this.grdvProNotasVenta.EditIndex = -1;
            this.grdvProNotasVenta.DataBind();
            this.ActualizaTotales();
            this.btnGuardaNotaVenta_Click(null, null);
        }

        protected void grdvProNotasVenta0_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            this.grdvProNotasVenta0.EditIndex = -1;
            this.grdvProNotasVenta0.DataBind();
            this.ActualizaTotales();
            this.btnGuardaNotaVenta_Click(null, null);
            this.changeExistencia();

        }

        protected void CreateURLForPagare()
        {
            #region Add link for print pagare
            try
            {
                this.lnkImprimePagare.Visible = this.chkaCredito.Checked;
                if (this.chkaCredito.Checked)
                {
                    string sFileName = "PAGARE.pdf";
                    sFileName = sFileName.Replace(" ", "_");
                    string sURL = "frmDescargaTmpFile.aspx";
                    string datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
                    datosaencriptar = datosaencriptar + "solID=-1&creditoID=" + this.ddlCredito.SelectedValue.ToString() + "&";
                    datosaencriptar += "impPagare=1&monto=" + Utils.GetSafeFloat(this.lblTotal.Text).ToString() + "&";
                    datosaencriptar += "fecha=" + this.txtFecha.Text + "&";
                    datosaencriptar += "fechaPagare=" + this.txtFechaPagare.Text + "&";
                    string URLcomplete = sURL + "?data=";
                    URLcomplete += Utils.encriptacadena(datosaencriptar);
                    lnkImprimePagare.NavigateUrl = this.Request.Url.ToString();
                    JSUtils.OpenNewWindowOnClick(ref lnkImprimePagare, URLcomplete, "Pagare", true);
                }
            }
            catch { }
            #endregion
        }


        protected void grdvProNotasVenta0_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grdvProNotasVenta0.EditIndex = e.NewEditIndex;
            this.grdvProNotasVenta0.DataBind();
            this.ActualizaTotales();
            this.btnGuardaNotaVenta_Click(null, null);
            this.changeExistencia();
        }

        protected void grdvProNotasVenta_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "DELETE FROM NotasdeVenta_detalle WHERE (NDVdetalleID = @NDVdetalleID)";
                comm.CommandText = dbFunctions.UpdateSDSForSisBanco(comm.CommandText);
                comm.Parameters.Add("@NDVdetalleID", SqlDbType.Int).Value= e.Keys["NDVdetalleID"].ToString();
                dbFunctions.ExecuteCommand(comm);
                if (comm.Connection != null)
                {
                    comm.Connection.Close();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "grdvProNotasVenta_RowDeleting", this.Request.Url.ToString(), ref ex);
            }
            this.grdvProNotasVenta.DataBind();
            this.ActualizaTotales();
            this.btnGuardaNotaVenta_Click(null, null);
            this.changeExistencia();
        }

        protected void grdvProNotasVenta0_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "DELETE FROM NotasdeVenta_detalle WHERE (NDVdetalleID = @NDVdetalleID)";
                comm.CommandText = dbFunctions.UpdateSDSForSisBanco(comm.CommandText);
                comm.Parameters.Add("@NDVdetalleID", SqlDbType.Int).Value = e.Keys["NDVdetalleID"].ToString();
                dbFunctions.ExecuteCommand(comm);
                if (comm.Connection != null)
                {
                    comm.Connection.Close();
                }
//                 this.sdsFertilizantes.DeleteCommand = "DELETE FROM NotasdeVenta_detalle WHERE (NDVdetalleID = @NDVdetalleID)";
//                 this.sdsFertilizantes.DeleteParameters.Clear();
//                 this.sdsFertilizantes.DeleteParameters.Add("@NDVdetalleID",DbType.Int32, e.Keys["NDVdetalleID"].ToString());
//                 this.sdsFertilizantes.Delete();
                this.ActualizaTotales();
                this.btnGuardaNotaVenta_Click(null, null);
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "grdvProNotasVenta0_RowDeleting", this.Request.Url.ToString(), ref ex);
            }
            this.grdvProNotasVenta0.DataBind();
            this.changeExistencia();
        }
        private void limpiaPagos()
        {
            this.txtMonto.Text = "1";
            //this.txtNombrePago.Text = "1";
            this.txtFechaNPago.Text = Utils.Now.ToString("dd/MM/yyyy");
            this.txtLitrosTarjetaDiesel.Text = "";
            this.txtfoliodiesel.Text = "";

        }

        protected void btnPrintNotaVenta_Click(object sender, EventArgs e)
        {
            if (!this.btnDesbloquear.Visible)
            {
                this.btnGuardaNotaVenta_Click(null, null);
                this.BloqueaNota(true);
            }
        }

        private void BloqueaNota(bool IsBlocked)
        {
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "UPDATE NOTASDEVENTA SET BLOQUEADA = @block WHERE notadeventaID=@notadeventaID";
                comm.CommandText = dbFunctions.UpdateSDSForSisBanco(comm.CommandText);
                comm.Parameters.Add("@notadeventaID", SqlDbType.Int).Value = this.txtNotaIDToMod.Text;
                comm.Parameters.Add("@block", SqlDbType.Bit).Value = IsBlocked ? 1 : 0;
                dbFunctions.ExecuteCommand(comm);
                this.SetBlockMode(IsBlocked);
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "bloqueando nota", ref ex);
            }
            finally
            {
                if (comm.Connection != null)
                {
                    comm.Connection.Close();
                }
            }
        }

        private void SetBlockMode(bool IsBlockModeON)
        {
            this.cmbNombre.Enabled = !IsBlockModeON;
            this.btnActulizarcmbClientes.Visible = !IsBlockModeON;
            this.ddltipoCalculoIntereses.Enabled = !IsBlockModeON;
            this.chkaCredito.Enabled = !IsBlockModeON;
            this.ddlCredito.Enabled = !IsBlockModeON;
            this.btnAgregarBoleta.Visible = !IsBlockModeON;
            this.btnAgregarProducto.Visible = !IsBlockModeON;
            this.grdvProNotasVenta.Enabled = !IsBlockModeON;
            this.grdvProNotasVenta0.Enabled = !IsBlockModeON;
            this.chbIVA.Enabled = !IsBlockModeON;
            this.grvPagos.Enabled = !IsBlockModeON;
            this.chkAddNewPago.Checked = false;
            this.chkAddNewPago.Enabled = !IsBlockModeON;
            this.pnlAgregarNuevoPago_CollapsiblePanelExtender.Collapsed = true;
            this.btnGuardaNotaVenta.Visible = !IsBlockModeON;
            this.btnDesbloquear.Visible = IsBlockModeON;

        }

        protected void btnDesbloquear_Click(object sender, EventArgs e)
        {
            this.BloqueaNota(false);
        }

      
        private void LoadFirmasAutorizadas()
        {
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand cmdGaribay = new SqlCommand();
                cmdGaribay.CommandText = "SELECT     Solicitudes.firmaAutorizada1, Solicitudes.firmaAutorizada2, Solicitudes.firmaAutorizada3, Solicitudes.firmaAutorizada4, Solicitudes.firmaAutorizada5 FROM         Solicitudes INNER JOIN  Creditos ON Solicitudes.creditoID = Creditos.creditoID where Solicitudes.creditoId = @creditoId";
                cmdGaribay.CommandText = dbFunctions.UpdateSDSForSisBanco(cmdGaribay.CommandText);
                cmdGaribay.Parameters.Add("@creditoId", SqlDbType.Int).Value = int.Parse(this.ddlCredito.SelectedValue);
                cmdGaribay.Connection = conGaribay;
                conGaribay.Open();
                SqlDataReader reader = cmdGaribay.ExecuteReader();
                DataTable dtFirmas = new DataTable();
                dtFirmas.Columns.Add("firmaId",typeof(string));
                dtFirmas.Columns.Add("Firma",typeof(string));
                if(reader.Read())
                {
                    if(reader["firmaAutorizada1"] != null && reader["firmaAutorizada1"].ToString().Length >0)
                    {
                        dtFirmas.Rows.Add(reader["firmaAutorizada1"], reader["firmaAutorizada1"]);  
                    }
                    if (reader["firmaAutorizada2"] != null && reader["firmaAutorizada2"].ToString().Length > 0)
                    {
                        dtFirmas.Rows.Add(reader["firmaAutorizada2"], reader["firmaAutorizada2"]);
                    }
                    if (reader["firmaAutorizada3"] != null && reader["firmaAutorizada3"].ToString().Length > 0)
                    {
                        dtFirmas.Rows.Add(reader["firmaAutorizada3"], reader["firmaAutorizada3"]);
                    }
                    if (reader["firmaAutorizada4"] != null && reader["firmaAutorizada4"].ToString().Length > 0)
                    {
                        dtFirmas.Rows.Add(reader["firmaAutorizada4"], reader["firmaAutorizada4"]);
                    }
                    if (reader["firmaAutorizada5"] != null && reader["firmaAutorizada5"].ToString().Length > 0)
                    {
                        dtFirmas.Rows.Add(reader["firmaAutorizada5"], reader["firmaAutorizada5"]);
                    }
                    if(dtFirmas.Rows.Count == 0)
                    {
                        dtFirmas.Rows.Add("NO HAY FIRMAS AUTORIZADAS.", "NO HAY FIRMAS AUTORIZADAS.");
                    }
                    this.dropDownListFirmas.DataSource = dtFirmas;
                    this.dropDownListFirmas.DataValueField = "firmaId";
                    this.dropDownListFirmas.DataTextField = "Firma";
                    this.dropDownListFirmas.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "ERROR SACANDO FIRMAS AUTORIZADAS", ref ex);
            }

        }

        protected void ddlCredito_DataBound(object sender, EventArgs e)
        {
            this.LoadFirmasAutorizadas();
        }

        protected void ddlCredito_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadFirmasAutorizadas();
        }

        protected void cmbCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fechaPagare = Utils.Now.ToString("dd/MM/yyyy");
            try
            {
                DateTime inicio = new DateTime();
                DateTime fin = new DateTime();
                int cicloid = int.Parse(this.cmbCiclo.SelectedValue.ToString());
                dbFunctions.sacaFechasCiclo(cicloid, ref inicio, ref fin, this.UserID);
                fechaPagare = fin.ToString("dd/MM/yyyy");
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "getting fecha fin ciclo", ref ex);
            }
            this.txtFechaPagare.Text = fechaPagare;
        }

        protected void cmbCiclo_DataBound(object sender, EventArgs e)
        {
            string fechaPagare = Utils.Now.ToString("dd/MM/yyyy");
            try
            {
                DateTime inicio = new DateTime();
                DateTime fin = new DateTime();
                int cicloid = int.Parse(this.cmbCiclo.SelectedValue.ToString());
                dbFunctions.sacaFechasCiclo(cicloid, ref inicio, ref fin, this.UserID);
                fechaPagare = fin.ToString("dd/MM/yyyy");
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "getting fecha fin ciclo", ref ex);
            }
            this.txtFechaPagare.Text = fechaPagare;
        }

        protected void PopCalendar7_SelectionChanged(object sender, EventArgs e)
        {
            this.txtFechaPagare.Text = this.PopCalendar7.DateValue.ToString("dd/MM/yyyy");
        }
    
       

       

    }
      
}
