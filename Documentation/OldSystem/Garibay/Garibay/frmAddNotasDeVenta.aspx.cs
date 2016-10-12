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
    public partial class frmNotasDeVenta : Garibay.BasePage
    {

        dsNV.dtNVdatosDataTable dtNotaVentaData = new dsNV.dtNVdatosDataTable();
        dsNV.dtNVdetalleDataTable dtNotaDetalle;

        string dtSessionDetalle = "dtNVDetalle";
        string dtSession = "dtNVData";
        string dtSessionBoletas = "dtBoletas";

        internal void SaveDTInSession()
        {
            this.Session[this.dtSessionDetalle] = this.dtNotaDetalle;
            this.Session[this.dtSession] = this.dtNotaVentaData;
            //this.Session[this.dtSessionBoletas] = this.dtBoletas;
        }

        internal void LoadDTFromSession()
        {
            this.dtNotaDetalle = this.Session[this.dtSessionDetalle] != null ? (dsNV.dtNVdetalleDataTable)this.Session[this.dtSessionDetalle] : new dsNV.dtNVdetalleDataTable();
            this.dtNotaVentaData = this.Session[this.dtSession] != null ? (dsNV.dtNVdatosDataTable)this.Session[this.dtSession] : new dsNV.dtNVdatosDataTable();
            //this.dtBoletas = this.Session[this.dtSessionBoletas] != null ? (dsBoletas.dtBoletasDataTable)this.Session[this.dtSessionBoletas] : new dsBoletas.dtBoletasDataTable();
        }


        private void JavaScriptABotones()
        {
            JSUtils.AddDisableWhenClick(ref this.btnActualizarListaBoletas, this);
            
            JSUtils.AddDisableWhenClick(ref this.btnActulizarcmbClientes, this);
            JSUtils.AddDisableWhenClick(ref this.btnAddPago, this);
            JSUtils.AddDisableWhenClick(ref this.btnAgregarBoleta, this);
            JSUtils.AddDisableWhenClick(ref this.btnAgregarNotaV, this);
            JSUtils.AddDisableWhenClick(ref this.btnAgregarProducto, this);
            JSUtils.AddDisableWhenClick(ref this.btnGuardaNotaVenta, this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.divAgregarNuevoPago.Attributes.Add("style", this.chkMostrarAgregarPago.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divaCredito.Attributes.Add("style", this.chkaCredito.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            if (!this.IsPostBack)
            {

                this.JavaScriptABotones();
                this.Session.Remove(this.dtSessionDetalle);
                this.Session.Remove(this.dtSession);
                this.txtNewFechaEntrada.Text = this.txtNewFechaSalida.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.divFechaSalidaNewBoleta.Attributes.Add("style", this.chkChangeFechaSalidaNewBoleta.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.txtFecha.Text = Utils.Now.ToShortDateString();
                this.lblPagos.Text = string.Format("{0:C2}", 0);
                this.pnladdproducto.Visible = true;
                this.pnlBoletas.Visible = true;
                this.pnlCentralData.Visible = false;
                this.chkAgregarBoletas.Visible = true;
                this.chkMostrarAgregarProductos.Visible = false;
                this.txtFecha.Text = Utils.Now.ToShortDateString();
                this.txtFechaproducto.Text = Utils.Now.ToShortDateString();
                this.chkMostrarAgregarProductos.Visible = false;
                this.cmbNombre.DataBind();
                this.cmbNombre.SelectedIndex = 0;
                this.cargadatosProductor(int.Parse(this.cmbNombre.SelectedValue));
                if (Request.QueryString["data"] != null && this.loadqueryStrings(Request.QueryString["data"].ToString()) && this.myQueryStrings != null && this.myQueryStrings["NotaVentaID"] != null)
                {

                    this.txtNotaIDToMod.Text = this.lblNumOrdenDeSalida.Text = this.myQueryStrings["NotaVentaID"].ToString();
                    this.LoadNotaVenta(this.myQueryStrings["NotaVentaID"].ToString());
                    this.pnladdproducto.Visible = true;
                    this.pnlBoletas.Visible = true;
                    this.pnlCentralData.Visible = true;
                    this.chkAgregarBoletas.Visible = true;
                    this.camposReadOnly();
                    this.btnAgregarNotaV.Visible = false;
                    this.chkMostrarAgregarProductos.Visible = true;


                }
                else
                {
                    //this.chkPnlAddProd.Checked = true;
                }
                this.AddjstoControls();
                this.divPagoMovCaja.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.divMovBanco.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "TRANSFERENCIA" || this.cmbTipodeMovPago.SelectedItem.Text == "DEPOSITO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.divboletas.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "BOLETA"? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.divCheque.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                this.divDiesel.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "TARJETA DIESEL" ? "visibility: visible; display: block" : "visibility: hidden; display: none");


                //divIDDiesel, divIDCaja,divIDBoletas,divIDBancos, divIDCheque
                String sOnchagemov = "checkValueInListNotasVentas2(";
                sOnchagemov += "this" + ",'";
                sOnchagemov += this.divDiesel.ClientID + "','";
                sOnchagemov += this.divPagoMovCaja.ClientID + "','";
                sOnchagemov += this.divboletas.ClientID + "','";
                sOnchagemov += this.divMovBanco.ClientID + "','";
                sOnchagemov += this.divCheque.ClientID + "')";
                this.cmbTipodeMovPago.Attributes.Add("onChange", sOnchagemov);
                this.cmbTipodeMovPago.DataBind();
                //this.divDiesel.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO" ? "visibility: visible; display: block" : "visibility: hdden; display: none");

                //                                                                                <asp:ListItem Value="0">EFECTIVO</asp:ListItem>
                //                                                                                <asp:ListItem Value="1">CHEQUE</asp:ListItem>
                //                                                                                <asp:ListItem Value="2">TARJETA DIESEL</asp:ListItem>
                //                                                                                <asp:ListItem Value="3">BOLETA</asp:ListItem>
                //                                                                                <asp:ListItem Value="4">TRANSFERENCIA</asp:ListItem>


                //this.divMovBanco.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "TRANSFERENCIA" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                //String sOnchagemov = "checkValueInListNotasVentas(";
                //sOnchagemov += "this" + ",'TRANSFERENCIA','";
                //sOnchagemov += this.divMovBanco.ClientID + "','" + this.divDiesel.ClientID + "')";


                //this.cmbTipodeMovPago.Attributes.Add("onChange", sOnchagemov);
                //this.cmbConceptomovBancoPago.DataBind();

                //this.divCheque.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                //sOnchagemov = "checkValueInList(";
                //sOnchagemov += "this" + ",'CHEQUE','";
                //sOnchagemov += this.divCheque.ClientID + "');";
                //this.cmbConceptomovBancoPago.Attributes.Add("onChange", sOnchagemov);

                this.imgBienPago.Visible = false;
                this.imgMalPago.Visible = false;
                this.pnlNewPago.Visible = false;
                this.LoadDTFromSession();
                this.ActualizaTotales();
                this.cambiaTipoNota();
            }

            this.LoadDTFromSession();
            try
            {
                if (this.ddlProductos.Items.Count > 0 && this.ddlProductos.SelectedValue != null && this.ddlProductos.SelectedValue.Trim().Length > 0)
                {
                    this.txtExistencia.Text = string.Format("{0:n2}", dbFunctions.sacaExistenciadeproducto(int.Parse(this.ddlProductos.SelectedValue.ToString()), int.Parse(this.ddlBodega.SelectedValue.ToString())));
                }
            }
            catch{}
            CreateURLForPagare();
            this.drpdlGrupoCatalogosCajaChica.SelectedValue = "14";
            this.drpdlGrupoCuentaFiscal.SelectedValue = "14";
        }


        private void camposReadOnly()
        {
            this.txtDomicilio.ReadOnly = true;
            cmbNombre.Enabled = false;
            btnAgregarClienteRapido.Visible = false;
            btnActulizarcmbClientes.Visible = false;
            //this.txtDestino.ReadOnly = true;
            //this.txtPermiso.ReadOnly=true;
            //this.txtTransportista.ReadOnly=true;
            //this.txtNombreChofer.ReadOnly=true;
            //this.txtTractorCamion.ReadOnly=true;
            //this.txtColor.ReadOnly=true;
            //this.txtPlacas.ReadOnly=true;
            this.txtFolio.ReadOnly = true;
        }

        private void LoadNotaVenta(String idnota)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            SqlConnection connDetalle = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                //dsFacturasClientesTableAdapters.FacturasClientesVentaTableAdapter ta = new dsFacturasClientesTableAdapters.FacturasClientesVentaTableAdapter();
                String query = "Select * from Notasdeventa where notadeventaID =  @notadeventaID";
                conn.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter(query, conn);
                SqlCommandBuilder sqlBuilder = new SqlCommandBuilder(sqlDA);
                sqlDA.SelectCommand.Parameters.Add("@notadeventaID", SqlDbType.Int).Value = idnota;

                sqlDA.Fill(this.dtNotaVentaData);
                if (this.dtNotaVentaData.Rows.Count != 1)
                {
                    this.Response.Redirect("frmFacturaVentaClientes.aspx");
                }
                //si se cargo la factura entonces acomodamos todo lo demas para que se carguen los datos.
                this.cmbNombre.DataBind();
                dsNV.dtNVdatosRow actualRow = ((dsNV.dtNVdatosRow)this.dtNotaVentaData.Rows[0]);
                this.cargadatosProductor(actualRow.productorID);

                this.cmbNombre.SelectedValue = actualRow.productorID.ToString();
                this.cmbCiclo.SelectedValue = actualRow.cicloID.ToString();
                this.txtFolio.Text = actualRow.folio;
                this.txtFecha.Text = actualRow.fecha.ToString("dd/MM/yyyy");
                this.lblTotal.Text = string.Format("{0:C2}", actualRow.total);
                this.lblSubtotal.Text = string.Format("{0:C2}", actualRow.subtotal);
                this.lblIva.Text = string.Format("{0:C2}", actualRow.iva);
                this.txtObservaciones.Text = actualRow.observaciones;
                this.chkboxFertilizante.Checked = actualRow.notaDeFertilizante;
                this.lnkImprimePagare.Visible = this.chkaCredito.Checked = actualRow.acredito;

                
                this.txtTractorCamion.Text = actualRow.tractorcamion;
                if (actualRow.acredito)
                {
                    this.ddlCredito.DataBind();
                    this.ddlCredito.SelectedValue = actualRow.creditoID.ToString();
                }
                this.ddltipoCalculoIntereses.SelectedValue = actualRow.tipocalculodeinteresID.ToString();
                this.txtPermiso.Text = actualRow.numeropermiso;
                this.txtTransportista.Text = actualRow.transportista;

                this.txtNombreChofer.Text = actualRow.nombrechofer;
                this.txtColor.Text = actualRow.color;
                this.txtPlacas.Text = actualRow.placas;




                //load detalle
                this.dtNotaDetalle = new dsNV.dtNVdetalleDataTable();


                String sQuery = " SELECT     Productos.Nombre, NotasdeVenta_detalle.cantidad, NotasdeVenta_detalle.precio, NotasdeVenta_detalle.sacos, ";
                      sQuery +=" NotasdeVenta_detalle.cantidad * NotasdeVenta_detalle.precio AS Importe, NotasdeVenta_detalle.NDVdetalleID, NotasdeVenta_detalle.notadeventaID, ";
                      sQuery +=" NotasdeVenta_detalle.productoID, Bodegas.bodegaID, NotasdeVenta_detalle.userID, NotasdeVenta_detalle.cicloID, NotasdeVenta_detalle.fecha, ";
                      sQuery +=" Productos.presentacionID, Productos.unidadID, Productos.productoGrupoID, Presentaciones.peso, Presentaciones.Presentacion, Unidades.Unidad";
                       sQuery +=" FROM        NotasdeVenta_detalle INNER JOIN";
                    sQuery +="            Productos ON NotasdeVenta_detalle.productoID = Productos.productoID INNER JOIN";
                      sQuery +=" Bodegas ON NotasdeVenta_detalle.bodegaID = Bodegas.bodegaID INNER JOIN";
                      sQuery +=" Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID INNER JOIN";
                      sQuery +=" Unidades ON Productos.unidadID = Unidades.unidadID";
                    sQuery +=" WHERE     (NotasdeVenta_detalle.notadeventaID = @notadeventaID)";


                
                
                connDetalle.Open();
                SqlCommand commDetalle = new SqlCommand(sQuery, connDetalle);
                commDetalle.Parameters.Add("@notadeventaID", SqlDbType.Int).Value = idnota;
                SqlDataAdapter detalleDA = new SqlDataAdapter(commDetalle);
                DataTable dtDetalleTemp = new DataTable();
                detalleDA.Fill(dtDetalleTemp);
                foreach (DataRow row in dtDetalleTemp.Rows)
                {
                    dsNV.dtNVdetalleRow newRow = this.dtNotaDetalle.NewdtNVdetalleRow();
                    //dsFacturasClientes.dtFacAClientesDetallesRow newRow = this.dtFacDetalle.NewdtFacAClientesDetallesRow();
                    newRow.NDVdetalleID = (int)row["NDVdetalleID"];
                    newRow.notadeventaID = (int)row["notadeventaID"];
                    newRow.productoID = (int)row["productoID"];
                    newRow.bodegaID = (int)row["bodegaID"];
                    newRow.cantidad = (double)row["cantidad"];
                    newRow.precio = double.Parse(row["precio"].ToString());

                    newRow.Importe = (double)row["Importe"];
                    newRow.sacos = (double)row["sacos"];
                    newRow.Nombre = row["Nombre"].ToString();
                    newRow.userID = (int)row["userID"];
                    newRow.cicloID = (int)row["cicloID"];
                    newRow.fecha = (DateTime)row["fecha"];
                    newRow.presentacionID = (int)row["presentacionID"];
                    newRow.unidadID = (int)row["unidadID"];
                    newRow.grupoID = (int)row["productoGrupoID"];
                    newRow.peso = Double.Parse(row["peso"].ToString());
                    if (this.chkboxFertilizante.Checked)
                    {
                        newRow.peso = (double)(newRow.cantidad * 50) / 1000;
                    }
                    else
                    {
                        newRow.peso = (double)newRow.cantidad;
                    }
                    if (this.chkboxFertilizante.Checked)
                        newRow.Importe = newRow.peso * newRow.precio;
                    else
                        newRow.Importe = newRow.cantidad * newRow.precio;
                    newRow.Presentacion = row["Presentacion"].ToString();
                    newRow.Unidad = row["Unidad"].ToString();

                    this.dtNotaDetalle.AdddtNVdetalleRow(newRow);
                }

                this.grdvProNotasVenta.DataSource = this.dtNotaDetalle;
                this.grdvProNotasVenta.DataBind();
                this.btnAgregarNotaV.Visible = false;

                //Preselecciona datos del pago
                this.PopCalendar6.SetDateValue(Utils.Now);
                this.txtNombrePago.Text = this.cmbNombre.SelectedItem.Text;
                //this.txtChequeNombre.Text = this.txtNombrePago.Text;

                this.SaveDTInSession();

                JSUtils.OpenNewWindowOnClick(ref this.btnPrintNotaVenta , "frmPrintNotaVenta.aspx" + Utils.GetEncriptedQueryString("notadeventaID=" + this.lblNumOrdenDeSalida.Text), "Imprimir Nota de venta", true);
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.NOTAVENTA, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "SE CARGARON LOS DATOS DE LA NOTA DE VENTA No. " + lblNumOrdenDeSalida.Text + ".");
                
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "LoadFactura", this.Request.Url.ToString(), ref ex);
            }
            finally
            {
                conn.Close();
                connDetalle.Close();
            }

        }

        private void AddjstoControls()
        {
            string parameter;
            parameter = "javascript:url('";
            parameter += "frmAddQuickProductor.aspx";
            parameter += "', '";
            parameter += "Agregar Productor Rápido";
            parameter += "',0,200,400,500); return false;";

            this.btnAgregarClienteRapido.Attributes.Clear();
            this.btnAgregarClienteRapido.Attributes.Add("onClick", parameter);

           

            

            String sQuery = "QueryExistencias('" + this.txtExistencia.ClientID + "','" + this.cmbCiclo.ClientID + "','" + this.ddlProductos.ClientID + "','" + this.ddlBodega.ClientID + "');QueryPrecios('" + this.ddlProductos.ClientID + "','" + "1" + "','" + this.txtPrecio.ClientID + "'); return false;";

            this.ddlProductos.Attributes.Add("onChange", sQuery);
            this.ddlBodega.Attributes.Add("onChange", sQuery);
            this.cmbCiclo.Attributes.Add("onChange", sQuery);

            //sQuery = "validarExitenciaSacaPesoYImporte('" + ddlProductos.ClientID+"','"+ddlBodega.ClientID + "','" + this.txtCantidad.ClientID+ "','" + this.txtExistencia.ClientID + "','" + this.txtPrecio.ClientID + "','" + this.txtImporte.ClientID + "','" + this.txtPesoTonelada.ClientID + "');";
            //this.txtCantidad.Attributes.Clear();
            //this.txtCantidad.Attributes.Add("onChange", sQuery);

            //validarExitenciaSacaPesoYImporte(ddlProducto, ddlBodega, txtCantidad, txtExistencia, txtPrecio, txtImporte, txtPesoTon) {


            sQuery = "validarExistencia('" + this.txtCantidad.ClientID + "','" + this.txtExistencia.ClientID + "','" + this.txtPrecio.ClientID + "','" + this.txtImporte.ClientID + "');";
            this.txtCantidad.Attributes.Clear();
            this.txtCantidad.Attributes.Add("onChange", sQuery);

            this.divaCredito.Attributes.Add("style", this.chkaCredito.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            String sOnchange = "ShowHideDivOnChkBox('";
            sOnchange += this.chkaCredito.ClientID + "','";
            sOnchange += this.divaCredito.ClientID + "')";
            this.chkaCredito.Attributes.Clear();
            this.chkaCredito.Attributes.Add("onclick", sOnchange);

            sOnchange = "loadProducerData('";
            sOnchange += this.cmbNombre.ClientID + "','";
            sOnchange += this.txtDestino.ClientID + "','";
            sOnchange += this.txtDomicilio.ClientID + "','";
            sOnchange += this.txtPoblacion.ClientID + "','";
            sOnchange += this.txtEstado.ClientID + "','";
            sOnchange += this.txtTelefono.ClientID + "','";
            sOnchange += this.txtCelular.ClientID + "','";
            sOnchange += this.txtIFE.ClientID + "','";
            sOnchange += this.txtNombreChofer.ClientID + "','";
            sOnchange += this.txtTransportista.ClientID + "'); return false;";

            this.cmbNombre.Attributes.Add("onChange", sOnchange);

            sQuery = "notaventaID=" + this.lblNumOrdenDeSalida.Text;
            sQuery = Utils.GetEncriptedQueryString(sQuery);
            string strRedirect = "frmMovBancoAddQuick.aspx";
            strRedirect += sQuery;
            parameter = "javascript:url('";
            parameter += strRedirect;
            parameter += "', '";
            parameter += "Agregar Movimiento de Banco";
            parameter += "',0,200,700,700); return false;";

            //this.btnOpenNewMovBan.Attributes.Clear();
            //this.btnOpenNewMovBan.Attributes.Add("onClick", parameter);

            //             sOnchange="calculaIvaNotaVenta('";
            //             sOnchange+=this.lblSubtotal.ClientID+"','";
            //             sOnchange+=this.lblIva.ClientID+"','";
            //             sOnchange+=this.lblPagos.ClientID+"','";
            //             sOnchange+=this.lblTotal.ClientID+"','";
            //             sOnchange+=this.chbIVA.ClientID+"')";
            //             this.chbIVA.Attributes.Clear();
            //             this.chbIVA.Attributes.Add("onClick", sOnchange);

            String sOnchangeMul = "mulTextBoxesNotNeg('";
            sOnchangeMul += this.txtCantidad.ClientID + "','";
            sOnchangeMul += this.txtPrecio.ClientID + "','" + this.txtImporte.ClientID + "');";
            this.txtPrecio.Attributes.Add("onChange", sOnchangeMul);


            sQuery = "notaventaID=" + this.txtNotaIDToMod.Text + "&productorID=" + this.cmbNombre.SelectedValue;
            sQuery = Utils.GetEncriptedQueryString(sQuery);
            strRedirect = "frmBoletaNewQuick.aspx";
            strRedirect += sQuery;

            String sOnClick = "popupCenteredFACBOL('";
            sOnClick += strRedirect;
            sOnClick += "',600, 600); return false;";
            this.btnAgregarBoleta.Attributes.Add("onClick", sOnClick);


            //String sOnchangeAB = "ShowHideDivOnChkBox('";
            //sOnchangeAB += this.chkMostrarAgregarPago.ClientID + "','";
            //sOnchangeAB += this.divAgregarNuevoPago.ClientID + "')";
            //this.chkMostrarAgregarPago.Attributes.Add("onclick", sOnchangeAB);
            String sOnchagemov = "checkValueInListNotasVentas2(";
            sOnchagemov += "this" + ",'";
            sOnchagemov += this.divDiesel.ClientID + "','";
            sOnchagemov += this.divPagoMovCaja.ClientID + "','";
            sOnchagemov += this.divboletas.ClientID + "','";
            sOnchagemov += this.divMovBanco.ClientID + "','";
            sOnchagemov += this.divCheque.ClientID + "')";
            this.cmbTipodeMovPago.Attributes.Add("onChange", sOnchagemov);
            this.cmbTipodeMovPago.DataBind();

             sOnchange = "ShowHideDivOnChkBox('";
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
            sOnchange += this.cmbNombre.ClientID + "','";
            sOnchange += this.ddlNewBoletaProductor.ClientID + "');";
            this.cmbNombre.Attributes.Add("onChange", sOnchange);
        }

        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
            }
        }



        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //this.Server.Transfer("~/frmListNotasDeVenta.aspx");
        }

        protected bool cargadatosProductor(int productorid)
        {
            Boolean result = false;
            string qrySel = "SELECT     Productores.municipio, Productores.domicilio, Productores.poblacion, Estados.estado, Productores.telefono, Productores.celular, Productores.IFE ";
            qrySel += "FROM         Productores INNER JOIN Estados ON Productores.estadoID = Estados.estadoID WHERE productorID=@prodID";
            //string qrySel = "SELECT Productores.municipio + ', ' + Estados.estado as poblacion FROM Productores INNER JOIN Estados on Productores.estadoID = Estados.estadoID where productorID = @prodID ";
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
                        this.txtDestino.Text = datos[0].ToString();
                        this.txtDomicilio.Text = datos[1].ToString();
                        this.txtPoblacion.Text = datos[2].ToString();
                        this.txtEstado.Text = datos[3].ToString();
                        this.txtTelefono.Text = datos[4].ToString();
                        this.txtCelular.Text = datos[5].ToString();
                        this.txtIFE.Text = datos[6].ToString();
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

        protected void cmbCredito_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected bool cargadatosCreditos(int creditoID)
        {
            
            return true;

        }

        
        protected void calculaTotales()
        {
            float subtotal = 0, total = 0, iva = 0, cantidad = 0, importe = 0, precio = 0;
            this.dtNotaDetalle = (dsNV.dtNVdetalleDataTable)(this.Session[this.dtSessionDetalle]);
            foreach (dsNV.dtNVdetalleRow row in dtNotaDetalle)
            {
                cantidad = float.Parse(row.cantidad.ToString());
                precio = float.Parse(row.precio.ToString());

                float peso = float.Parse(row.peso.ToString());
                if (this.chkboxFertilizante.Checked)
                    importe = peso * precio;
                else
                    importe = cantidad * precio;
                row.Importe = double.Parse(importe.ToString());
                subtotal += float.Parse(row.Importe.ToString());
            }
            if (this.chbIVA.Checked)
            {
                iva = subtotal * float.Parse("0.16");
            }
            String pagos = this.lblPagos.Text.Replace("$", "");
            pagos = pagos.Replace(",", "");
            pagos = pagos.Replace("-", "");
            total = iva + subtotal - float.Parse(pagos);
            this.lblSubtotal.Text = string.Format("{0:c}", subtotal);
            this.lblTotal.Text = string.Format("{0:c}", total);
            this.lblIva.Text = string.Format("{0:c}", iva);
        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {
            
        }

        protected void cmbTipodePago_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void btnActulizarcmbClientes_Click(object sender, EventArgs e)
        {
            this.cmbNombre.DataBind();
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                dsNV.dtNVdetalleRow newRow = this.dtNotaDetalle.NewdtNVdetalleRow();
                newRow.notadeventaID = int.Parse(this.lblNumOrdenDeSalida.Text);
                newRow.productoID = int.Parse(this.ddlProductos.SelectedValue);
                double dTemp = 0;
                double dTemp2 = 0;
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                conn.Open();
                comm.CommandText = "SELECT     Presentaciones.peso, Presentaciones.Presentacion, Unidades.Unidad, Productos.unidadID, Productos.presentacionID FROM   Presentaciones INNER JOIN  Productos ON Presentaciones.presentacionID = Productos.presentacionID INNER JOIN Unidades ON Productos.unidadID = Unidades.unidadID WHERE Productos.productoID=@productoID";
                comm.Parameters.Add("@productoID", SqlDbType.Int).Value = int.Parse(this.ddlProductos.SelectedValue);
                SqlDataReader rd = comm.ExecuteReader();
                if(rd.HasRows&&rd.Read()){
                    //pesobase = double.Parse(rd["peso"].ToString());
                    newRow.peso=double.Parse(rd["peso"].ToString());
                    newRow.unidadID= int.Parse(rd["unidadID"].ToString());
                    newRow.Presentacion = rd["Presentacion"].ToString();
                    newRow.presentacionID = int.Parse(rd["presentacionID"].ToString());
                    newRow.Unidad = rd["Unidad"].ToString();

                }
                
                double.TryParse(this.txtCantidad.Text, out dTemp);
                double.TryParse(this.txtCantidad.Text, out dTemp);
                newRow.cantidad = dTemp;
                if(this.chkboxFertilizante.Checked)
                {
                    newRow.peso = (double)(newRow.cantidad * 50)/1000;
                }
                else
                {
                    newRow.peso = (double)newRow.cantidad;
                }
                double.TryParse(this.txtPrecio.Text, out dTemp2);
                newRow.precio = dTemp2;
                if (this.chkboxFertilizante.Checked)
                    newRow.Importe = (double)newRow.peso * newRow.precio;
                else
                    newRow.Importe = newRow.cantidad * newRow.precio;
                newRow.Nombre = this.ddlProductos.SelectedItem.Text;
                newRow.bodegaID = int.Parse(this.ddlBodega.SelectedValue);
                newRow.userID = this.UserID;
                newRow.fecha = Convert.ToDateTime(this.txtFechaproducto.Text);
                newRow.cicloID = int.Parse(this.cmbCiclo.SelectedValue);

                double.TryParse(this.txtSacos.Text, out dTemp2);
                newRow.sacos = dTemp2;
                bool bFound = false;
                dsNV.dtNVdetalleRow row = null;
                for (int i = 0; i < this.dtNotaDetalle.Rows.Count && !bFound; i++)
                {
                    row = (dsNV.dtNVdetalleRow)this.dtNotaDetalle.Rows[i];
                    if (row.productoID == newRow.productoID && row.precio == newRow.precio)
                    {
                        row.peso += newRow.peso;
                        row.cantidad += newRow.cantidad;
                        if (this.chkboxFertilizante.Checked)
                            row.Importe += newRow.peso * newRow.precio;
                        else
                            row.Importe += newRow.cantidad * newRow.precio;
                        bFound = true;
                    }
                }
                if (!bFound)
                {
                    this.dtNotaDetalle.AdddtNVdetalleRow(newRow);
                }
                this.SaveDTInSession();
                try
                {
                    this.calculaTotales();
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "calculando totales", ref ex);
                }


                this.grdvProNotasVenta.DataSource = this.dtNotaDetalle;
                this.grdvProNotasVenta.DataBind();

                //Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.SELECT, this.UserID, "SE AGREGO EL PRODUCTO " + this.ddlProductos.SelectedItem.Text + "A LA NOTA DE VENTA " + this.lblNumOrdenDeSalida.Text, Request.Url.ToString());
             btnGuardaNotaVenta_Click(null, null);
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "agregando producto en NV", ref ex);

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
                if (this.chkaCredito.Checked) { comm.CommandText += ", creditoID "; }
                comm.CommandText += ",Fechadepago, Interes, Observaciones, acredito, tipocalculodeinteresID,origen, remitente,domicilio, telefono,destino, numeropermiso, transportista,nombrechofer,tractorcamion, color, placas,storeTS,fechainiciobrointereses, fechafincobrointereses, notaDeFertilizante)";
                comm.CommandText += " VALUES (@productorID, @cicloID, @userID, @Fecha, @Folio, @Pagada, @Total,  @Subtotal, @Iva";
                if (this.chkaCredito.Checked) { comm.CommandText += ", @creditoID "; }
                comm.CommandText += ",@Fechadepago, @Interes, @Observaciones, @acredito, @tipocalculodeinteresID, @origen, @remitente, @domicilio, @telefono,@destino, @numeropermiso, @transportista,@nombrechofer,@tractorcamion, @color, @placas, @storeTS, @fechainiciobrointereses, @fechafincobrointereses, @notaDeFertilizante); select notadeventaID = SCOPE_IDENTITY();";
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
                comm.Parameters.Add("@notaDeFertilizante", SqlDbType.Bit).Value = this.chkboxFertilizante.Checked;
                int identity = int.Parse(comm.ExecuteScalar().ToString());
                this.lblNumOrdenDeSalida.Text = identity.ToString();
                this.chkboxFertilizante.Checked = true;


                    

                comm.CommandText = "UPDATE Notasdeventa SET Folio=@fol where notadeventaID=@notaID";
                comm.Parameters.Add("@fol", SqlDbType.Int).Value = identity;
                comm.Parameters.Add("@notaID", SqlDbType.Int).Value = identity;

                comm.ExecuteNonQuery();

                if (identity > 0)
                {

                    String sQuery = "NotaVentaID=" + identity.ToString();
                    sQuery = Utils.GetEncriptedQueryString(sQuery);
                    strRedirect = "~/frmAddNotasDeVenta.aspx" + sQuery;
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.NOTAVENTA, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), "SE AGREGÓ LA NOTA DE VENTA No. " + identity.ToString() + ".");
                    //String sQuery = "NotaVentaID=" + identity.ToString();
                    //sQuery = Utils.GetEncriptedQueryString(sQuery);
                    //String strRedirect = "~/frmAddNotasDeVenta.aspx" + sQuery;
                    
                    
                    //this.LoadFactura(identity.ToString());
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



        protected void grdvProNotasVenta_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                this.dtNotaDetalle.Rows.RemoveAt(e.RowIndex);
                this.grdvProNotasVenta.DataSource = this.dtNotaDetalle;
                this.grdvProNotasVenta.EditIndex = -1;
                this.grdvProNotasVenta.DataBind();
                this.calculaTotales();
                this.SaveDTInSession();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "grdvProNotasVenta_RowDeleting", this.Request.Url.ToString(), ref ex);
            }
        }

        protected void grdvProNotasVenta_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grdvProNotasVenta.EditIndex = e.NewEditIndex;
            this.grdvProNotasVenta.DataSource = this.dtNotaDetalle;
            this.grdvProNotasVenta.DataBind();
            dsNV.dtNVdetalleRow row = (dsNV.dtNVdetalleRow)this.dtNotaDetalle.Rows[e.NewEditIndex];
            DropDownList ddlProd = (DropDownList)this.grdvProNotasVenta.Rows[this.grdvProNotasVenta.EditIndex].FindControl("ddlProdEdit");
            ddlProd.DataBind();
            ddlProd.SelectedValue = row.productoID.ToString();
        }

        protected void grdvProNotasVenta_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grdvProNotasVenta.EditIndex = -1;
            this.grdvProNotasVenta.DataSource = dtNotaDetalle;
            this.grdvProNotasVenta.DataBind();
        }

        protected void grdvProNotasVenta_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                dsNV.dtNVdetalleRow row = (dsNV.dtNVdetalleRow)this.dtNotaDetalle.Rows[grdvProNotasVenta.EditIndex];
                //                 DropDownList ddlBodega = (DropDownList)this.grdvProNotas.Rows[grdvProNotas.EditIndex].FindControl("ddlBodegasEdit");
                DropDownList ddlProd = (DropDownList)this.grdvProNotasVenta.Rows[grdvProNotasVenta.EditIndex].FindControl("ddlProdEdit");
                //                 row.Bodega = ddlBodega.SelectedItem.Text;
                //                 row.bodegaID = int.Parse(ddlBodega.SelectedItem.Value);
                row.Nombre = ddlProd.SelectedItem.Text;
                row.productoID = int.Parse(ddlProd.SelectedItem.Value);


                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                conn.Open();
                comm.CommandText = "SELECT     Presentaciones.peso, Presentaciones.Presentacion, Unidades.Unidad, Productos.unidadID, Productos.presentacionID FROM   Presentaciones INNER JOIN  Productos ON Presentaciones.presentacionID = Productos.presentacionID INNER JOIN Unidades ON Productos.unidadID = Unidades.unidadID WHERE Productos.productoID=@productoID";
                comm.Parameters.Add("@productoID", SqlDbType.Int).Value = row.productoID;
                SqlDataReader rd = comm.ExecuteReader();
                if (rd.HasRows && rd.Read())
                {
                    //pesobase = double.Parse(rd["peso"].ToString());
                    row.peso = double.Parse(rd["peso"].ToString());
                    row.unidadID = int.Parse(rd["unidadID"].ToString());
                    row.Presentacion = rd["Presentacion"].ToString();
                    row.presentacionID = int.Parse(rd["presentacionID"].ToString());
                    row.Unidad = rd["Unidad"].ToString();

                }
                

                TextBox txt = (TextBox)this.grdvProNotasVenta.Rows[grdvProNotasVenta.EditIndex].FindControl("txtCantidadEdit");
                double dTemp = 0;
                if (double.TryParse(txt.Text, out dTemp))
                {
                    row.cantidad = dTemp;
                }
                txt = (TextBox)this.grdvProNotasVenta.Rows[grdvProNotasVenta.EditIndex].FindControl("txtPrecioEdit");
                dTemp = 0;
                
                if (double.TryParse(txt.Text, out dTemp))
                {
                    row.precio = dTemp;
                }
                if (this.chkboxFertilizante.Checked)
                {
                    row.peso = (row.cantidad * 50)/1000;
                    row.Importe = row.peso * row.precio;
                }
                else
                {
                    row.peso = (double)row.cantidad;
                    row.Importe = row.cantidad * row.precio;
                }
              
                CheckBox chec = (CheckBox)this.grdvProNotasVenta.Rows[grdvProNotasVenta.EditIndex].FindControl("chkboxSalidaPro");
                row.tieneBoletas = chec.Checked;
                this.SaveDTInSession();
                this.grdvProNotasVenta.DataSource = this.dtNotaDetalle;
                this.grdvProNotasVenta.EditIndex = -1;
                this.grdvProNotasVenta.DataBind();
                this.ActualizaTotales();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "actualizando datos de la row", this.Request.Url.ToString(), ref ex);
            }
        }

        protected void grdvProNotasVenta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       

        protected void chkMostrarAgregarProductos_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnGuardaNotaVenta_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                String query = "Select * from Notasdeventa where notadeventaID =  @NVID";
                conn.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter(query, conn);
                SqlCommandBuilder sqlBuilder = new SqlCommandBuilder(sqlDA);
                sqlDA.SelectCommand.Parameters.Add("@NVID", SqlDbType.Int).Value = int.Parse(this.lblNumOrdenDeSalida.Text);

                sqlDA.Fill(this.dtNotaVentaData);
                if (this.dtNotaVentaData.Rows.Count != 1)
                {
                    //   this.imgBien.Visible = false;
                    //  this.imgMal.Visible = this.pnlFacturaResult.Visible = true;
                    //  this.lblFacturaResult.Text = "ESTO ES VERGONZOSO, HA OCURRIDO UNA EXCEPCION Y NO SE PUDO GUARDAR LA FACTURA,<BR> YA QUE NO SE PUDIERON LEER LOS DATOS DE ÉSTA<BR> POR FAVOR ESPERE UN MOMENTO E INTENTELO DE NUEVO";
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
                row.notaDeFertilizante = this.chkboxFertilizante.Checked;
                if(this.chkaCredito.Checked)
                {
                    row.creditoID = int.Parse(this.ddlCredito.SelectedValue);
                    row.acredito = true;
                }
                else
                {
                    row.creditoID = -1;
                    row.acredito = false;

                }


                if (sqlDA.Update(this.dtNotaVentaData) != 1)
                {
                    //this.imgBien.Visible = false;
                    //this.imgMal.Visible = this.pnlFacturaResult.Visible = true;
                    //this.lblFacturaResult.Text = "ESTO ES VERGONZOSO, HA OCURRIDO UNA EXCEPCION Y NO SE PUDO GUARDAR LA FACTURA,<BR> YA QUE NO SE PUDIERON ACTUALIZAR LOS DATOS DE ÉSTA<BR> POR FAVOR ESPERE UN MOMENTO E INTENTELO DE NUEVO";
                }

                //save data from detalles
                conn.Close();
                conn.Open();
                query = "Select NDVdetalleID, notadeventaID, productoID, bodegaID, cantidad, precio, sacos, userID, cicloID, fecha, tieneBoletas, salidaprodID from NotasdeVenta_detalle where notadeventaID  =  @NVID";

                SqlDataAdapter sqlDADetalle = new SqlDataAdapter(query, conn);
                SqlCommandBuilder sqlBuilderDet = new SqlCommandBuilder(sqlDADetalle);
                sqlDADetalle.SelectCommand.Parameters.Add("@NVID", SqlDbType.Int).Value = int.Parse(this.lblNumOrdenDeSalida.Text);

                dsNV.dtNVdetalleDataTable dtDetalle = new dsNV.dtNVdetalleDataTable();
                dtDetalle.Rows.Clear();
                sqlDADetalle.Fill(dtDetalle);
                if (dtDetalle.Rows.Count > 0)
                {
                    foreach (DataRow rowDetalle in dtDetalle.Rows)
                    {
                        rowDetalle.Delete();
                    }
                    sqlDADetalle.Update(dtDetalle.GetChanges(DataRowState.Deleted));

                }

                if (this.dtNotaDetalle.Rows.Count > 0)
                {
                    String sError = "";
                    int newID = -1;
                    int rowIndex = 0;
                    foreach (dsNV.dtNVdetalleRow rowDetalle in this.dtNotaDetalle.Rows)
                    {
                        rowDetalle.notadeventaID = int.Parse(this.lblNumOrdenDeSalida.Text);

                        try
                        {
                            GridViewRow otraRow = this.grdvProNotasVenta.Rows[rowIndex++];
                            rowDetalle.tieneBoletas = ((CheckBox)otraRow.FindControl("chkTieneBoletas")).Checked;
                        }
                        catch (System.Exception ex)
                        {
                            Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "err getting checkbox for boleta", ref ex);
                        }

                        if (!rowDetalle.tieneBoletas)
                        {
                            newID = dbFunctions.insertSalidaDeProducto(rowDetalle.productoID, rowDetalle.bodegaID, 3, rowDetalle.fecha, (double)rowDetalle.cantidad, "SE AGREGÓ ESTA ENTRADA POR LA NOTA DE VENTA: " + this.lblNumOrdenDeSalida.Text, rowDetalle.cicloID, (double)rowDetalle.sacos, this.UserID);
                            rowDetalle.salidaprodID = newID;
                        }
                        else
                            rowDetalle.salidaprodID = -1;
                        dtDetalle.ImportRow(rowDetalle);
                    }
                    sqlDADetalle.Update(dtDetalle.GetChanges(DataRowState.Added));
                }

                this.pnlNotaVentaResult.Visible = this.imgBien.Visible = true;
                this.imgMal.Visible = false;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.NOTAVENTA, Logger.typeUserActions.UPDATE, "ACTUALIZO LA NOTA DE VENTA FOLIO: " + this.txtFolio.Text);
                this.lblNotaVentaResult.Text = "LOS DATOS DE LA NOTA DE VENTA HAN SIDO GUARDADOS CORRECTAMENTE. " + Utils.Now.ToString("dd/MM/yyyy HH:mm:ss");
                CreateURLForPagare();
                //this.pnlFacturaResult.Visible = this.imgBien.Visible = true;
                //this.imgMal.Visible = false;
                //Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURADEVENTA, Logger.typeUserActions.UPDATE, "ACTUALIZO LA FACTURA DE CLIENTE FOLIO: " + this.txtFacturaIDToMod.Text);
                //this.lblFacturaResult.Text = "LOS DATOS DE LA FACTURA HAN SIDO GUARDADOS CORRECTAMENTE. " + Utils.Now.ToString("dd/MM/yyyy HH:mm:ss");



            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "err updating boleta", ref ex);
                //this.imgBien.Visible = false;
                //this.imgMal.Visible = this.pnlFacturaResult.Visible = true;
                //this.lblFacturaResult.Text = "ESTO ES VERGONZOSO, HA OCURRIDO UNA EXCEPCION Y NO SE PUDO GUARDAR LA FACTURA.<BR>POR FAVOR ESPERE UN MOMENTO E INTENTELO DE NUEVO<BR>Descripción del error<BR>" + ex.Message;

            }
            finally
            {
                conn.Close();
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.NOTAVENTA, Logger.typeUserActions.UPDATE, this.UserID, "GUARDO LA NOTA DE VENTA: " + this.lblNumOrdenDeSalida.Text);
            }
        }

        protected void btnNewMovCajaChica_Click(object sender, EventArgs e)
        {

        }


        protected void actualizaPagos1()
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand comm = new SqlCommand();
            try
            {
                String sqry = "SELECT  isnull(SUM(MovimientosCaja.cargo),0)+ isnull(SUM(MovimientosCuentasBanco.cargo),0)";
                sqry += " FROM         MovimientosCaja right JOIN";
                sqry += " Pagos_NotaVenta ON MovimientosCaja.movimientoID = Pagos_NotaVenta.movimientoID Left JOIN";
                sqry += " MovimientosCuentasBanco ON Pagos_NotaVenta.movbanID = MovimientosCuentasBanco.movbanID";
                sqry += " WHERE     (Pagos_NotaVenta.notadeventaID = @notaventaID)";
                conn.Open();
                comm.Connection = conn;
                comm.CommandText = sqry;
                comm.Parameters.Add("@notaventaID", SqlDbType.Int).Value = this.lblNumOrdenDeSalida.Text;
                this.lblPagos.Text = string.Format("{0:C2}", float.Parse(comm.ExecuteScalar().ToString()));
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "actualiza Pagos de la Nota de Venta", ref ex);
            }
        }

        protected void gvPagosBancosVenta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        

        protected void chbIVA_CheckedChanged(object sender, EventArgs e)
        {
            this.calculaTotales();
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

        protected void btnActualizarListaBoletas_Click(object sender, EventArgs e)
        {
            this.gvBoletas.DataBind();
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
                    comm.Parameters.Add("@boletaID", SqlDbType.Int).Value = e.Keys["boletaID"];
                    comm.ExecuteNonQuery();
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
            
            this.lblPagos.Text = string.Format("{0:C2}", this.sumaMontos());

            double subtotal = 0, total = 0, iva = 0, cantidad = 0, importe = 0, precio = 0;
            //this.dtNotaDetalle = (dsNV.dtNVdetalleDataTable)(this.Session[this.dtSessionDetalle]);
            this.LoadDTFromSession();
            foreach (dsNV.dtNVdetalleRow row in dtNotaDetalle)
            {
                cantidad = double.Parse(row.cantidad.ToString());
                precio = double.Parse(row.precio.ToString());


                if (this.chkboxFertilizante.Checked)
                    importe = row.peso * precio;
                else
                    importe = cantidad * precio;
                
                
                //importe = cantidad * precio;


                row.Importe = importe;
                subtotal += double.Parse(row.Importe.ToString());

            }
            if (this.chbIVA.Checked)
            {
                iva = subtotal * double.Parse("0.16");
            }
            total = iva + subtotal;
            this.lblSubtotal.Text = string.Format("{0:c2}", subtotal);
            this.lblTotal.Text = string.Format("{0:c2}", total - (Utils.GetSafeFloat(this.lblPagos.Text)));
            this.lblIva.Text = string.Format("{0:c2}", iva);

        }


       

        protected void cmdAceptar_Click(object sender, EventArgs e)
        {

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

            //<asp:ListItem Value="0">EFECTIVO</asp:ListItem>
            //<asp:ListItem Value="1">CHEQUE</asp:ListItem>
            //<asp:ListItem Value="2">TARJETA DIESEL</asp:ListItem>
            //<asp:ListItem Value="3">BOLETA</asp:ListItem>
            //<asp:ListItem Value="4">TRANSFERENCIA</asp:ListItem>

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
                    dtRowainsertar2.Observaciones = "Pago a nota de compra";
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


                            Logger.Instance.LogUserSessionRecord(Logger.typeModulo.NOTACOMPRA, Logger.typeUserActions.UPDATE, this.UserID, "SE INSERTÓ UN PAGO A LA NOTA DE COMPRA " + this.txtNotaIDToMod.Text + " EL MOV DE CAJA CHICA FUE: " + dtRowainsertar2.movimientoID.ToString());

                        }
                        catch (Exception ex)
                        {
                            Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL INSERTAR UN PAGO A LA NOTA (CAJA CHICA): " + this.txtNotaIDToMod.Text + ". EX " + ex.Message, this.Request.Url.ToString());

                        }
                        //this.pnlNewPago.Visible = true;
                        //this.imgBienPago.Visible = true;
                        //this.imgMalPago.Visible = false;
                        //this.lblNewPagoResult.Text = string.Format(myConfig.StrFromMessages("MOVCAJAADDEDEXITO"), sNewMov);
                        Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDECAJACHICA, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), "AGREGÓ EL MOVIMIENTO DE CAJA CHICA NÚMERO: " + dtRowainsertar2.movimientoID.ToString());


                      //  this.ActualizaTotales();
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
                {//Tranferencia
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
                    a = new ListBox();
                    if (dbFunctions.insertaMovBanco(ref dtRowainsertar, ref serror, int.Parse(this.Session["USERID"].ToString()), int.Parse(this.cmbCuentaPago.SelectedValue), int.Parse(this.cmbCiclo.SelectedValue), -1, "", "PAGO A NOTA DE VENTA"))
                    {

                        //if the mov banco were added successfully then check if add into factura



                        SqlConnection connVenta = new SqlConnection(myConfig.ConnectionInfo);
                        try
                        {
                            connVenta.Open();
                            SqlCommand commVenta = new SqlCommand();
                            commVenta.Connection = connVenta;
                            commVenta.CommandText = "INSERT INTO Pagos_NotaVenta(fecha, notadeventaID, movbanID) VALUES (@fecha,@notadeventaID,@movbanID) ";
                            //(@FacturaCVID,@fecha,@movbanID,@movCajaID,@userID)
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



                        //limpiacampos();

                        this.cmbTipodeMovPago.SelectedIndex = 0;

                        this.pnlNewPago.Visible = true;
                        this.imgBienPago.Visible = true;
                        this.imgMalPago.Visible = false;
                        this.lblNewPagoResult.Text = "EL MOVIMIENTO DE BANCO NÚMERO :" + dtRowainsertar.movBanID.ToString() + " SE HA AGREGADO SATISFACTORIAMENTE";
                    }


                }//aquiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii
                else
                    if (this.cmbTipodeMovPago.SelectedValue == "1")//CHEQUE
                    {
                        bool result;
                        int iChequeRecID= -1;
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

                        if(result)
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
            btnGuardaNotaVenta_Click(null, null);

            

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

        //protected void Button7_Click(object sender, EventArgs e)
        //{
        //    string serror = "";
        //    try
        //    {
        //        //if (this.chbMostrarPnlAddTarjDiesel.Checked)
        //        //{
        //            if (dbFunctions.addTarjetaDiesel(int.Parse(this.lblNumOrdenDeSalida.Text),
        //                                         int.Parse(txtfoliodiesel.Text),
        //                                         float.Parse(this.txtMontoTarjetaDiesel.Text),
        //                                         float.Parse(this.txtLitrosTarjetaDiesel.Text),
        //                                         ref serror,
        //                                         int.Parse(this.Session["USERID"].ToString()), 1))
        //            {
                        
        //                ActualizaTotales();
        //                this.grvPagos.DataBind();
        //                //this.txtFolio.ReadOnly = true;
        //            }

        //            else
        //            {
        //                throw new Exception(serror);
        //            }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        this.cmbTipodeMovPago.SelectedIndex = 0;
        //        this.pnlNewPago.Visible = true;
        //        this.imgBienPago.Visible = false;
        //        this.imgMalPago.Visible = true;
        //        this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR LA TARJETA DIESEL";
        //        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "Error adding tarjeta diesel. El error fue :" + ex.Message, this.Request.Url.ToString());

        //    }
        //}

        

        

        

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
                    lbl.Text = string.Format("{0:C2}",Utils.GetSafeFloat(comm.ExecuteScalar().ToString())) ;

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
                    lbl.Text = string.Format("{0:C2}",Utils.GetSafeFloat(comm.ExecuteScalar().ToString()));

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

                        lbl.Text = rd["numCheque"].ToString()!="0"?rd["numCheque"].ToString(): "";

                        lbl = (Label)e.Row.FindControl("Label9");
                        lbl.Text = rd["Concepto"].ToString();


                        
                        lbl = (Label)e.Row.FindControl("Label12");
                        lbl.Text = string.Format("{0:c2}",Utils.GetSafeFloat(rd["abono"].ToString()));
                        lbl = (Label)e.Row.FindControl("Label11");
                        lbl.Text = rd["nombre"].ToString();
                    }

                }
                else if (this.grvPagos.DataKeys[e.Row.RowIndex]["boletaID"].ToString() != "")
                {



                    String query = "SELECT     Pagos_NotaVenta.fecha, Boletas.Ticket, Boletas.totalapagar ";
                            query +=" FROM         Boletas INNER JOIN ";
                      query +=" Pagos_NotaVenta ON Boletas.boletaID = Pagos_NotaVenta.boletaID ";
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

                }finally{

                    conn.Close();
                }

            
                }

            if (e.Row.RowType == DataControlRowType.Footer){
                
                e.Row.Cells[7].Text = "TOTAL";


                e.Row.Cells[8].Text = string.Format("{0:C2}",sumaMontos());
            }
                
        }

            
        protected double sumaMontos(){
            Label lbl;
            double total=0.0;
            foreach(GridViewRow row in this.grvPagos.Rows){
                lbl = (Label)row.Cells[7].FindControl("Label12");
                total += Utils.GetSafeFloat(lbl.Text.Replace("$","").Replace(",",""));


            }
            return total;
        }

        protected void grvPagos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            SqlPagos.DeleteCommand = "DELETE FROM Pagos_NotaVenta WHERE (Pagos_NotaVenta.PagoNotaVentaID = @PagoNotaVentaID)";
            SqlPagos.DeleteParameters.Add("@PagoNotaVentaID", e.Keys["PagoNotaVentaID"].ToString());
        }

        protected void grdvProNotasVenta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int a = 1;
            a++;


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                
                
                
                
                
                try
                {                   
                    conn.Open();

                    dsNV.dtNVdetalleRow Row = (dsNV.dtNVdetalleRow)this.dtNotaDetalle.Rows[e.Row.RowIndex];
                        Label lbl = (Label)e.Row.FindControl("lblPresentacionGrid");
                        lbl.Text=Row.Presentacion+"-"+Row.Unidad;
                        


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

        }

        protected void chkboxFertilizante_CheckedChanged(object sender, EventArgs e)
        {
            this.cambiaTipoNota();
        }
        private void cambiaTipoNota()
        {            
                this.grdvProNotasVenta.Columns[5].Visible = this.chkboxFertilizante.Checked; 
                if(this.chkboxFertilizante.Checked)
                {
                    this.sqlGrupos.FilterExpression = string.Empty;
                    this.sqlGrupos.FilterExpression = " Grupo = 'Fertilizantes'";

                }
                else
                {
                    this.sqlGrupos.FilterExpression = string.Empty;
                    this.sqlGrupos.FilterExpression = " Grupo <> 'Fertilizantes'";
                }
                this.ddlGrupos.DataBind();
        }

        protected void cmbNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cargadatosProductor(int.Parse(this.cmbNombre.SelectedValue));
        }

        protected void cmbTipodeMovPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpdlGrupoCatalogosInternaPago_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                    string URLcomplete = sURL + "?data=";
                    URLcomplete += Utils.encriptacadena(datosaencriptar);
                    lnkImprimePagare.NavigateUrl = this.Request.Url.ToString();
                    JSUtils.OpenNewWindowOnClick(ref lnkImprimePagare, URLcomplete, "Pagare", true);
                }
            }
            catch{}
            #endregion
        }

        protected void grvPagos_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            this.grvPagos.DataBind();
            this.ActualizaTotales();
        }

        protected void grdvProNotasVenta_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            this.grdvProNotasVenta.DataBind();
            this.ActualizaTotales();
        }

        protected void grdvProNotasVenta_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            this.grdvProNotasVenta.DataBind();
            this.ActualizaTotales();
        }
    }
}