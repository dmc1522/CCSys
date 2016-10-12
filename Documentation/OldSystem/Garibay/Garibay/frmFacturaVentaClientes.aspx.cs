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
    public partial class frmFacturaVentaClientes : Garibay.BasePage
    {
        dsBoletas.dtBoletasDataTable dtBoletas = new dsBoletas.dtBoletasDataTable();
        dsFacturasClientes.FacturasClientesVentaDataTable dtFacturaData = new dsFacturasClientes.FacturasClientesVentaDataTable();
        dsFacturasClientes.dtFacAClientesDetallesDataTable dtFacDetalle;
        string dtSessionDetalle = "dtFacClienteDetalle";
        string dtSession = "dtFacturaData";
        string dtSessionBoletas = "dtBoletas";
    
        internal void SaveDTInSession()
        {
            this.Session[this.dtSessionDetalle] = this.dtFacDetalle;
            this.Session[this.dtSession] = this.dtFacturaData;
            this.Session[this.dtSessionBoletas] = this.dtBoletas;
        }

        internal void LoadDTFromSession()
        {
            this.dtFacDetalle = this.Session[this.dtSessionDetalle] != null ? (dsFacturasClientes.dtFacAClientesDetallesDataTable)this.Session[this.dtSessionDetalle] : new dsFacturasClientes.dtFacAClientesDetallesDataTable();
            this.dtFacturaData = this.Session[this.dtSession] != null ? (dsFacturasClientes.FacturasClientesVentaDataTable)this.Session[this.dtSession] : new dsFacturasClientes.FacturasClientesVentaDataTable();
            this.dtBoletas = this.Session[this.dtSessionBoletas] != null ? (dsBoletas.dtBoletasDataTable)this.Session[this.dtSessionBoletas] : new dsBoletas.dtBoletasDataTable();
        }

        private void AddJSToControls()
        {
            String sOnchangeMul = "mulTextBoxesNotNeg('";
            sOnchangeMul += this.txtCantidad.ClientID + "','";
            sOnchangeMul += this.txtPrecio.ClientID + "','" + this.txtImporte.ClientID + "');";
            this.txtCantidad.Attributes.Add("onChange", sOnchangeMul);
            this.txtPrecio.Attributes.Add("onChange", sOnchangeMul);

            String sOnchange = "loadClienteData('" + this.drpdlCliente.ClientID + "', '" + this.txtDomicilio.ClientID + "', '" + this.txtCiudad.ClientID + "', '" + this.txtEstado.ClientID + "', '" + this.txtRFC.ClientID + "', '" + this.txtTelefono.ClientID + "','" + this.txtColonia.ClientID +"','" + this.txtCP.ClientID + "'); return false;";
            this.drpdlCliente.Attributes.Add("onChange", sOnchange);

            JSUtils.AddDisableWhenClick(ref this.btnAddproduct, this);
            JSUtils.AddDisableWhenClick(ref this.btnAgregarNewFactura, this);


            String sQuery = "FacID=" + this.txtFacturaIDToMod.Text;
            sQuery = Utils.GetEncriptedQueryString(sQuery);
            String strRedirect = "frmBoletasdeFacturaCV.aspx";
            strRedirect += sQuery;

            String sOnClick = "popupCenteredFACBOL('";
            sOnClick += strRedirect;
            sOnClick += "',400, 400); return false;";
            this.btnAddBoletas.Attributes.Add("onClick", sOnClick);

            String sOnchangeAB = "ShowHideDivOnChkBox('";
            sOnchangeAB += this.chkMostrarAgregarPago.ClientID + "','";
            sOnchangeAB += this.divAgregarNuevoPago.ClientID + "')";
            this.chkMostrarAgregarPago.Attributes.Add("onChange", sOnchangeAB);

            sQuery = "CopyTextBoxValue('"+ this.txtNombrePago.ClientID +"','"+ this.txtChequeNombre.ClientID +"')";
            this.txtNombrePago.Attributes.Add("OnChange", sQuery);

            String sOnchagemov = "checkValueInList(";
            sOnchagemov += "this" + ",'CHEQUE','";
            sOnchagemov += this.divCheque.ClientID + "');";
            this.cmbConceptomovBancoPago.Attributes.Add("onChange", sOnchagemov);
        }


        private void CalculaPagos()
        {
            try
            {
                double dPagos = 0;
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "SELECT sum(ISNULL(bancoabono,0)) - sum(ISNULL(bancocargo,0)) + sum(ISNULL(cajaabono,0)) - sum(ISNULL(cajacargo,0)) as Pagos FROM [PagosFacturasCliente] group by facturacvid HAVING ([FacturaCVID] = @FacturaCVID)";
                comm.Parameters.Add("@FacturaCVID", SqlDbType.Int).Value = this.txtFacturaIDToMod.Text;
                DataTable dt =  dbFunctions.GetDataTable(comm);
                if (dt.Rows.Count > 0)
                {
                    if (!double.TryParse(dt.Rows[0][0].ToString(),out dPagos))
                    {
                        dPagos = 0;
                    }
                }
                this.lblPagos.Text = string.Format("{0:C2}", dPagos);
                double restante = 0;
                restante = Utils.GetSafeFloat(this.lblTotal.Text) - Utils.GetSafeFloat(this.lblPagos.Text);
                restante = restante < 0 ? 0 : restante;
                this.lblRestanteAPagar.Text = restante.ToString("C2");
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Calculating pagos", ex);
                this.lblPagos.Text = "$ 0.00";
            }
            
        }

        private void LoadFactura(string iFactID)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            SqlConnection connDetalle = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                //dsFacturasClientesTableAdapters.FacturasClientesVentaTableAdapter ta = new dsFacturasClientesTableAdapters.FacturasClientesVentaTableAdapter();
                String query = "Select * from FacturasClientesVenta where FacturaCV =  @FacID";
                conn.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter(query, conn);
                SqlCommandBuilder sqlBuilder = new SqlCommandBuilder(sqlDA);
                sqlDA.SelectCommand.Parameters.Add("@FacID", SqlDbType.Int).Value = iFactID;

                sqlDA.Fill(this.dtFacturaData);
                if (this.dtFacturaData.Rows.Count != 1)
                {
                    this.Response.Redirect("frmFacturaVentaClientes.aspx");
                }
                //si se cargo la factura entonces acomodamos todo lo demas para que se carguen los datos.
                this.drpdlCliente.DataBind();
                this.cargaDatosClientes();
                dsFacturasClientes.FacturasClientesVentaRow actualRow = ((dsFacturasClientes.FacturasClientesVentaRow)this.dtFacturaData.Rows[0]);
                this.chbIVA.Checked = actualRow.llevaIVA;
                this.lblSubtotal.Text = string.Format("{0:C2}", actualRow.subtotal);
                this.lblIva.Text = string.Format("{0:C2}", actualRow.IVA);
                this.txtRETIVA.Text = string.Format("{0:C2}", actualRow.RETIVA);
                this.lblTotal.Text = string.Format("{0:C2}", actualRow.total);
                this.CalculaPagos();


                this.drpdlCliente.SelectedValue = actualRow.clienteVentaID.ToString();
                this.cargaDatosClientes();
                this.txtNumFactura.Text = actualRow.facturaNo;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURADEVENTA, Logger.typeUserActions.SELECT, "CONSULTO LA FACTURA DE CLIENTE FOLIO: " + actualRow.facturaNo +  " ID:" + iFactID.ToString());
                this.txtObservaciones.Text = actualRow.Observaciones;
                this.drpdlCiclo.DataBind();
                this.drpdlCiclo.SelectedValue = actualRow.cicloID.ToString();

                this.txtFecha.Text = actualRow.fecha.ToString("dd/MM/yyyy");

                //load detalle
                this.dtFacDetalle = new dsFacturasClientes.dtFacAClientesDetallesDataTable();
                String sQuery = "SELECT     FacturaClienteVentaDetalle.FacturaCVDetalleID, FacturaClienteVentaDetalle.FacturaCVID, FacturaClienteVentaDetalle.productoID, FacturaClienteVentaDetalle.Cantidad, "
                    + " FacturaClienteVentaDetalle.precio, FacturaClienteVentaDetalle.Cantidad * FacturaClienteVentaDetalle.precio AS Importe, "
                    + " Productos.Nombre + ' - ' + Presentaciones.Presentacion + ' - ' + Unidades.Unidad AS Producto "
                    + "  FROM         FacturaClienteVentaDetalle INNER JOIN "
                    + " Productos ON FacturaClienteVentaDetalle.productoID = Productos.productoID INNER JOIN "
                    + "  Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID INNER JOIN "
                    + "  Unidades ON Productos.unidadID = Unidades.unidadID WHERE        (FacturaClienteVentaDetalle.FacturaCVID = @FacturaCVID)";
                connDetalle.Open();
                SqlCommand commDetalle = new SqlCommand(sQuery, connDetalle);
                commDetalle.Parameters.Add("@FacturaCVID", SqlDbType.Int).Value = iFactID;
                SqlDataAdapter detalleDA = new SqlDataAdapter(commDetalle);
                DataTable dtDetalleTemp = new DataTable();
                detalleDA.Fill(dtDetalleTemp);
                foreach(DataRow row in dtDetalleTemp.Rows)
                {
                    dsFacturasClientes.dtFacAClientesDetallesRow newRow = this.dtFacDetalle.NewdtFacAClientesDetallesRow();
                    newRow.FacturaCVDetalleID = (int)row[0];
                    newRow.FacturaCVID = (int)row[1];
                    newRow.productoID = (int)row[2];
                    newRow.Cantidad = (decimal)(double)row[3];
                    newRow.precio = (decimal)(double)row[4];
                    newRow.Importe = (decimal)(double)row[5];
                    newRow.Producto = row[6].ToString();
                    this.dtFacDetalle.AdddtFacAClientesDetallesRow(newRow);
                }

                this.grdvProNotas.DataSource = this.dtFacDetalle;
                this.grdvProNotas.DataBind();

                this.drpdlCliente.Enabled = false;
                this.pnlCentral.Visible = true;
                this.btnAgregarNewFactura.Visible = false;
                this.txtFacturaIDToMod.Text = iFactID.ToString();

                //Preselecciona datos de pago
                this.txtNombrePago.Text = this.drpdlCliente.SelectedItem.Text;
                this.PopCalendar6.SetDateValue(Utils.Now);
                this.txtChequeNombre.Text = this.txtNombrePago.Text;

                this.SaveDTInSession();
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

        private void JavaScriptABotones()
        {
            JSUtils.AddDisableWhenClick(ref this.btnActualizaTotales, this);
            JSUtils.AddDisableWhenClick(ref this.btnAddPago, this);
            JSUtils.AddDisableWhenClick(ref this.btnAddproduct, this);
            JSUtils.AddDisableWhenClick(ref this.btnActualizaClientes, this);
            JSUtils.AddDisableWhenClick(ref this.btnActualizarBoletas, this);
            JSUtils.AddDisableWhenClick(ref this.btnAgregarNewFactura, this);
            JSUtils.AddDisableWhenClick(ref this.btnGuardaFactura, this);
            JSUtils.AddDisableWhenClick(ref this.btnActualizaPagos, this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.divAgregarNuevoPago.Attributes.Add("style", this.chkMostrarAgregarPago.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            
            this.compruebasecurityLevel();
            if(!this.IsPostBack)
            {
                this.JavaScriptABotones();
                try
                {
                    ddlGrupoProducto.DataBind();
                    drpdlProducto.DataBind();
                    string parameter;
                    parameter = "javascript:url('";
                    parameter += "frmAddQuickClienteVenta.aspx";
                    parameter += "', '";
                    parameter += "Agregar Productor Rápido";
                    parameter += "',0,200,400,800); return false;";
                    this.btnAgregarClienteRapido.Attributes.Clear();
                    this.btnAgregarClienteRapido.Attributes.Add("onClick", parameter);

                    this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
                    this.pnlCentral.Visible = false;
                    
                    this.Session[this.dtSession] = null;
                    this.AddJSToControls();
                    this.drpdlCliente.DataBind();
                    this.drpdlBodega.DataBind();
                    this.drpdlCiclo.DataBind();
                    if (this.drpdlCliente.Items.Count >0 )
                    {
                        this.drpdlCliente.SelectedIndex = 0;
                        this.cargaDatosClientes();
                    }
                    JSUtils.AddDisableWhenClick(ref this.btnActualizaClientes, this);
                    
                    this.drpdlProducto.DataBind();
                    //this.txtExistencia.Text = dbFunctions.sacaExistenciadeproducto(int.Parse(this.drpdlProducto.SelectedValue), int.Parse(this.drpdlBodega.SelectedValue), int.Parse(this.drpdlCiclo.SelectedValue)).ToString();
                    // dbFunctions.inicializadtNota(ref dtNCdetalle, int.Parse(this.drpdlBodega.SelectedValue));
                    this.grdvProNotas.DataSource = ((DataTable)(this.Session[dtSession]));
                    this.grdvProNotas.DataBind();

                    if (Request.QueryString["data"]!= null && this.loadqueryStrings(Request.QueryString["data"].ToString()) && this.myQueryStrings != null && this.myQueryStrings["FacID"] != null)
                    {
                        this.txtFacturaIDToMod.Text = this.myQueryStrings["FacID"].ToString();
                        this.LoadFactura(this.myQueryStrings["FacID"].ToString());             
                    }
                    else
                    {
                        this.chkPnlAddProd.Checked = true;
                    }
                    JSUtils.OpenNewWindowOnClick(ref this.btnAddBoletas, "frmBoletaNewQuick.aspx" + Utils.GetEncriptedQueryString("FacturaID=" + this.txtFacturaIDToMod.Text + "&clienteventaID=" + this.drpdlCliente.SelectedValue), "Nueva Boleta", true);
                    JSUtils.OpenNewWindowOnClick(ref this.btnOpenNewMovBan, "frmMovBancoAddQuick.aspx" + Utils.GetEncriptedQueryString("FacturaCVID=" + this.txtFacturaIDToMod.Text), "Nuevo movimiento a Factura", true);
                    JSUtils.OpenNewWindowOnClick(ref this.btnPrintFactura, "frmFacturaPrint.aspx" + Utils.GetEncriptedQueryString("FacturaID=" + this.txtFacturaIDToMod.Text), "Imprimir Factura", true);
                    this.SaveDTInSession();
                    this.cmbConceptomovBancoPago.DataBind();

                    this.cmbCuentaPago.DataBind();
                    this.cmbCuentaPago.SelectedValue = "12";
                    
                    this.drpdlGrupoCuentaFiscal.DataBind();
                    this.drpdlGrupoCuentaFiscal.SelectedValue = "2";
                    this.drpdlCatalogocuentafiscalPago.DataBind();
                    this.drpdlCatalogocuentafiscalPago.SelectedValue = "15";
                    this.drpdlSubcatalogofiscalPago.DataBind();

                    this.drpdlGrupoCatalogosInternaPago.DataBind();
                    this.drpdlGrupoCatalogosInternaPago.SelectedValue = "2";
                    this.drpdlCatalogoInternoPago.DataBind();
                    this.drpdlCatalogoInternoPago.SelectedValue = "15";
                    this.drpdlSubcatologointernaPago.DataBind();
                    this.ddlGrupoProducto.SelectedValue = "3";
                  
                    this.drpdlProducto.DataBind();
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Page_Load", this.Request.Url.ToString(), ref ex);
                }
            }
            this.LoadDTFromSession();
            this.grdvProNotas.DataSource = this.dtFacDetalle;
            //this.divAgregarNuevoPago.Visible = false;
            this.pnlNewPago.Visible = false;
            this.pnlFacturaResult.Visible = false;
            try
            {
                this.divCheque.Attributes.Add("style", this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            }
            catch {}
            this.lblEfectivoResult.Text = string.Empty;
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
            }

        }
        protected void drpdlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.cargaDatosClientes(int.Parse(this.drpdlCliente.SelectedValue));
            

        }
        protected void cargaDatosClientes(){
            if (this.drpdlCliente.Items.Count < 1)
            {
                return;
            }
            SqlConnection conSacaDatos = new SqlConnection(myConfig.ConnectionInfo);
            string query = "select nombre, domicilio, ciudad, telefono, RFC, colonia, CP, Estados.estado from ClientesVentas INNER join Estados on ClientesVentas.estadoID = Estados.estadoID where clienteventaID = @clienteID";
            SqlCommand cmdsacaDatos = new SqlCommand(query, conSacaDatos);
            conSacaDatos.Open();
            try
            {
                cmdsacaDatos.Parameters.Clear();
                cmdsacaDatos.Parameters.Add("@clienteID", SqlDbType.Int).Value = this.drpdlCliente.SelectedValue;
                SqlDataReader rd = cmdsacaDatos.ExecuteReader();
                if (rd.HasRows &&  rd.Read())
                {
                    this.txtDomicilio.Text = rd["domicilio"].ToString();
                    this.txtTelefono.Text = rd["telefono"].ToString();
                    this.txtCiudad.Text = rd["ciudad"].ToString();
                    this.txtEstado.Text = rd["estado"].ToString();
                    this.txtRFC.Text = rd["RFC"].ToString();
                    this.txtColonia.Text = rd["colonia"].ToString();
                    this.txtCP.Text = rd["CP"].ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL CARGAR LOS DATOS A MODIFICAR DEL CLIENTE DE VENTA CON EL ID: " + this.drpdlCliente.SelectedValue.ToString() + ". LA EXC FUE: " + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                conSacaDatos.Close();
            }

        }

        protected void drpdlBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.txtExistencia.Text = dbFunctions.sacaExistenciadeproducto(int.Parse(this.drpdlProducto.SelectedValue),int.Parse(this.drpdlBodega.SelectedValue),int.Parse(this.drpdlCiclo.SelectedValue)).ToString();
        }

        protected void drpdlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {

            //this.txtExistencia.Text = dbFunctions.sacaExistenciadeproducto(int.Parse(this.drpdlProducto.SelectedValue), int.Parse(this.drpdlBodega.SelectedValue), int.Parse(this.drpdlCiclo.SelectedValue)).ToString();

        }

        protected void btnAddproduct_Click(object sender, EventArgs e)
        {
            try
            {
                dsFacturasClientes.dtFacAClientesDetallesRow newRow = this.dtFacDetalle.NewdtFacAClientesDetallesRow();
                newRow.FacturaCVID = int.Parse(this.txtFacturaIDToMod.Text);
                
                decimal dTemp =0;
                decimal.TryParse(this.txtCantidad.Text, out dTemp);
                newRow.Cantidad = dTemp;
                newRow.Producto = this.drpdlProducto.SelectedItem.Text;
                int prodID = -1;
                int.TryParse(this.drpdlProducto.SelectedItem.Value, out prodID);
                newRow.productoID = prodID;
                dTemp = 0;
                decimal.TryParse(this.txtPrecio.Text, out dTemp);
                newRow.precio = dTemp;
                newRow.Importe = newRow.Cantidad * newRow.precio;
                //bool bFound = false;
                /*
                dsFacturasClientes.dtFacAClientesDetallesRow row = null;
                                for (int i = 0; i <this.dtFacDetalle.Rows.Count && !bFound; i++ )
                                {
                                    row = (dsFacturasClientes.dtFacAClientesDetallesRow)this.dtFacDetalle.Rows[i];
                                    if (row.productoID == newRow.productoID && row.precio == newRow.precio)
                                    {
                                        row.Cantidad += newRow.Cantidad;
                                        row.Importe = row.Cantidad * row.precio;
                                        bFound = true;
                                    }
                                }
                                if (!bFound)
                                {*/
                
                    this.dtFacDetalle.Rows.Add(newRow);
               // }
                this.SaveDTInSession();


                this.grdvProNotas.DataSource = this.dtFacDetalle;
                this.grdvProNotas.DataBind();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error agregando producto a factura", this.Request.Url.ToString(), ref ex);
            }


            this.ActualizaTotales();

        }

        private void ActualizaTotales()
        {
            double fSubTotal = 0.0f, fTemp;
            foreach (GridViewRow row in this.grdvProNotas.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    fTemp = Utils.GetSafeFloat(row.Cells[4].Text);
                    fSubTotal += fTemp;
                }
            }
            this.lblSubtotal.Text = String.Format("{0:c}", fSubTotal);
            this.lblIva.Text = String.Format("{0:c}", this.chbIVA.Checked ? (fSubTotal * 0.16) : 0);
            this.txtRETIVA.Text = String.Format("{0:C2}", Utils.GetSafeFloat(this.txtRETIVA.Text));
            this.lblTotal.Text = String.Format("{0:c}", fSubTotal + (this.chbIVA.Checked ? (fSubTotal * 0.16) : 0) - Utils.GetSafeFloat(this.txtRETIVA.Text));
            this.CalculaPagos();
            
        }

        protected void grdvProNotas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grdvProNotas.EditIndex = e.NewEditIndex;
            this.grdvProNotas.DataBind();

//             DropDownList ddlBodega = (DropDownList)this.grdvProNotas.Rows[e.NewEditIndex].FindControl("ddlBodegasEdit");
//             ddlBodega.DataBind();
             dsFacturasClientes.dtFacAClientesDetallesRow row = (dsFacturasClientes.dtFacAClientesDetallesRow) this.dtFacDetalle.Rows[e.NewEditIndex];
//             ddlBodega.SelectedValue = row.bodegaID.ToString();

            DropDownList ddlProd = (DropDownList)this.grdvProNotas.Rows[e.NewEditIndex].FindControl("ddlProdEdit");
            ddlProd.DataBind();
            ddlProd.SelectedValue = row.productoID.ToString();
        }

        protected void grdvProNotas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grdvProNotas.EditIndex = -1;
            this.grdvProNotas.DataBind();
        }

        protected void grdvProNotas_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
        }

        protected void grdvProNotas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                dsFacturasClientes.dtFacAClientesDetallesRow row = (dsFacturasClientes.dtFacAClientesDetallesRow)this.dtFacDetalle.Rows[this.grdvProNotas.EditIndex];
//                 DropDownList ddlBodega = (DropDownList)this.grdvProNotas.Rows[grdvProNotas.EditIndex].FindControl("ddlBodegasEdit");
                 DropDownList ddlProd = (DropDownList)this.grdvProNotas.Rows[grdvProNotas.EditIndex].FindControl("ddlProdEdit");
//                 row.Bodega = ddlBodega.SelectedItem.Text;
//                 row.bodegaID = int.Parse(ddlBodega.SelectedItem.Value);
                row.Producto = ddlProd.SelectedItem.Text;
                row.productoID = int.Parse(ddlProd.SelectedItem.Value);
                TextBox txt = (TextBox)this.grdvProNotas.Rows[grdvProNotas.EditIndex].FindControl("txtCantidadEdit");
                decimal dTemp = 0;
                if (decimal.TryParse(txt.Text, out dTemp) )
                {
                    row.Cantidad = dTemp;
                }
                txt = (TextBox)this.grdvProNotas.Rows[grdvProNotas.EditIndex].FindControl("txtPrecioEdit");
                dTemp = 0;
                if (decimal.TryParse(txt.Text, out dTemp))
                {
                    row.precio = dTemp;
                }
                row.Importe = row.Cantidad * row.precio;
                this.SaveDTInSession();
                this.grdvProNotas.DataSource = this.dtFacDetalle;
                this.grdvProNotas.EditIndex = -1;
                this.grdvProNotas.DataBind();
                this.ActualizaTotales();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "actualizando datos de la row", this.Request.Url.ToString(), ref ex);
            }
        }

        protected void grdvProNotas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                this.dtFacDetalle.Rows.RemoveAt(e.RowIndex);
                this.grdvProNotas.DataSource = this.dtFacDetalle;
                this.grdvProNotas.EditIndex = -1;
                this.grdvProNotas.DataBind();
                this.ActualizaTotales();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "grdvProNotas_RowDeleting", this.Request.Url.ToString(), ref ex);
            }
        }

        protected void btnAgregarNewFactura_Click(object sender, EventArgs e)
        {
            //add validacion
            //add new factura and then load it. :-)
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "INSERT INTO FacturasClientesVenta (cicloID, clienteVentaID, facturaNo, userID, fecha, storeTS, updateTS) VALUES (@cicloID,@clienteVentaID,@facturaNo,@userID,@fecha,@storeTS, @updateTS); select facturaID = SCOPE_IDENTITY();";
                comm.Connection = conn;

                //,@clienteVentaID,@facturaNo,@userID,@fecha

                comm.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(this.drpdlCiclo.SelectedValue);
                comm.Parameters.Add("@clienteVentaID", SqlDbType.Int).Value = int.Parse(this.drpdlCliente.SelectedValue);
                comm.Parameters.Add("@facturaNo",SqlDbType.VarChar).Value = this.txtNumFactura.Text;
                comm.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                comm.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Parse(this.txtFecha.Text);
                comm.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = DateTime.Parse(Utils.getNowFormattedDate());
                comm.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = DateTime.Parse(Utils.getNowFormattedDate());
                
                //int irow = ta.Update(dtFacturaData);
                ////aqui me quede
                int irow = int.Parse(comm.ExecuteScalar().ToString());
                if (irow >0)
                {
                    try
                    {
                        comm.Parameters.Clear();
                        comm.CommandText = "insert FacturasCV_Boletas(FacturaCV,boletaID) SELECT DISTINCT @FacturaCV, Boletas.boletaID FROM Boletas INNER JOIN ClienteVenta_Boletas ON Boletas.boletaID = ClienteVenta_Boletas.BoletaID "
                            + "  WHERE     (ClienteVenta_Boletas.clienteventaID = @clienteventaID) AND Boletas.boletaid not in (SELECT     Boletas.boletaID "
                            + " FROM         ClienteVenta_Boletas INNER JOIN "
                            + " Boletas ON ClienteVenta_Boletas.BoletaID = Boletas.boletaID INNER JOIN "
                            + " FacturasCV_Boletas ON Boletas.boletaID = FacturasCV_Boletas.boletaID); "
                            + " insert into FacturaClienteVentaDetalle(cantidad, productoid, precio, FacturaCVID) SELECT     Boletas.pesonetosalida, Boletas.productoID, Boletas.precioapagar, @FacturaCV "
                            + " FROM         Boletas INNER JOIN "
                            + " FacturasCV_Boletas ON Boletas.boletaID = FacturasCV_Boletas.boletaID "
                            + " WHERE     (FacturasCV_Boletas.FacturaCV = @FacturaCV);";
                        comm.Parameters.Add("@FacturaCV", SqlDbType.Int).Value = irow;
                        comm.Parameters.Add("@clienteventaID", SqlDbType.Int).Value = int.Parse(this.drpdlCliente.SelectedValue);
                        int boletasAdded = -1;
                        if( (boletasAdded = comm.ExecuteNonQuery()) > 0)
                        {
                            Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.INSERT, this.UserID, "Se agregaron " + boletasAdded.ToString() + " boletas a la factura " + irow.ToString(), this.Request.Url.ToString());
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.INSERT, "err agregando boletas y detalle a factura de cliente", ex);
                    }
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURADEVENTA, Logger.typeUserActions.INSERT, "AGREGO LA FACTURA DE CLIENTE FOLIO: " + this.txtFacturaIDToMod.Text);
                    String sQuery = "FacID=" + irow.ToString();
                    sQuery = Utils.GetEncriptedQueryString(sQuery);
                    String strRedirect = "~/frmFacturaVentaClientes.aspx" + sQuery;
                    Response.Redirect(strRedirect);
                    this.LoadFactura(irow.ToString());
                }
                else
                {
                    this.lblAddResult.Text = "Algo raro paso, se debio de haber insertado una factura pero el sistema regreso: " + irow.ToString();
                    throw new Exception("int irow = ta.Update(dtFacturaData); devolvio: " + irow.ToString());
                }
                
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "btnAgregarNewFactura_Click", this.Request.Url.ToString(), ref ex);
            }

        }

        protected void btnActualizaClientes_Click(object sender, EventArgs e)
        {
            this.drpdlCliente.DataBind();
        }

        protected void btnAgregarClienteRapido_Click(object sender, EventArgs e)
        {

        }

        protected void btnActualizarBoletas_Click(object sender, EventArgs e)
        {
            this.gvBoletasDisponibles.DataBind();
        }

        protected void chbIVA_CheckedChanged(object sender, EventArgs e)
        {
            this.ActualizaTotales();
        }

        protected void btnAddBoletas_Click(object sender, EventArgs e)
        {

        }

        protected void btnGuardaFactura_Click(object sender, EventArgs e)
        {
            this.GuardarFactura(true);
            
        }
        private void GuardarFactura(bool showMessage)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                String query = "Select * from FacturasClientesVenta where FacturaCV =  @FacID";
                conn.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter(query, conn);
                SqlCommandBuilder sqlBuilder = new SqlCommandBuilder(sqlDA);
                sqlDA.SelectCommand.Parameters.Add("@FacID", SqlDbType.Int).Value = int.Parse(this.txtFacturaIDToMod.Text);

                sqlDA.Fill(this.dtFacturaData);
                if (this.dtFacturaData.Rows.Count != 1)
                {
                    this.imgBien.Visible = false;
                    this.imgMal.Visible = this.pnlFacturaResult.Visible = true;
                    this.lblFacturaResult.Text = "ESTO ES VERGONZOSO, HA OCURRIDO UNA EXCEPCION Y NO SE PUDO GUARDAR LA FACTURA,<BR> YA QUE NO SE PUDIERON LEER LOS DATOS DE ÉSTA<BR> POR FAVOR ESPERE UN MOMENTO E INTENTELO DE NUEVO";
                    return;
                }
                dsFacturasClientes.FacturasClientesVentaRow row = (dsFacturasClientes.FacturasClientesVentaRow)this.dtFacturaData.Rows[0];
                row.cicloID = int.Parse(this.drpdlCiclo.SelectedValue);
                row.fecha = DateTime.Parse(this.txtFecha.Text);
                row.llevaIVA = this.chbIVA.Checked;
                row.subtotal = Utils.GetSafeFloat(this.lblSubtotal.Text);
                row.IVA = this.chbIVA.Checked ? Utils.GetSafeFloat(this.lblIva.Text) : 0.0f;
                row.RETIVA = Utils.GetSafeFloat(this.txtRETIVA.Text);
                row.total = row.subtotal + row.IVA - row.RETIVA;
                this.CalculaPagos();
                row.Observaciones = this.txtObservaciones.Text;
                row.facturaNo = this.txtNumFactura.Text.Trim(); //load factura number

                if (sqlDA.Update(this.dtFacturaData) != 1)
                {
                    this.imgBien.Visible = false;
                    this.imgMal.Visible = this.pnlFacturaResult.Visible = true;
                    this.lblFacturaResult.Text = "ESTO ES VERGONZOSO, HA OCURRIDO UNA EXCEPCION Y NO SE PUDO GUARDAR LA FACTURA,<BR> YA QUE NO SE PUDIERON ACTUALIZAR LOS DATOS DE ÉSTA<BR> POR FAVOR ESPERE UN MOMENTO E INTENTELO DE NUEVO";
                    return;
                }

                //save data from detalles
                conn.Close();
                conn.Open();
                query = "Select * from FacturaClienteVentaDetalle where FacturaCVID  =  @FacID";

                SqlDataAdapter sqlDADetalle = new SqlDataAdapter(query, conn);
                SqlCommandBuilder sqlBuilderDet = new SqlCommandBuilder(sqlDADetalle);
                sqlDADetalle.SelectCommand.Parameters.Add("@FacID", SqlDbType.Int).Value = int.Parse(this.txtFacturaIDToMod.Text);

                dsFacturasClientes.dtFacAClientesDetallesDataTable dtDetalle = new dsFacturasClientes.dtFacAClientesDetallesDataTable();
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

                if (this.dtFacDetalle.Rows.Count > 0)
                {
                    foreach (dsFacturasClientes.dtFacAClientesDetallesRow rowDetalle in this.dtFacDetalle.Rows)
                    {
                        rowDetalle.FacturaCVID = int.Parse(this.txtFacturaIDToMod.Text);
                        dtDetalle.ImportRow(rowDetalle);
                    }
                    sqlDADetalle.Update(dtDetalle.GetChanges(DataRowState.Added));
                }
                if(showMessage)
                {
                    this.pnlFacturaResult.Visible = this.imgBien.Visible = true;
                    this.imgMal.Visible = false;
                    this.lblFacturaResult.Text = "LOS DATOS DE LA FACTURA HAN SIDO GUARDADOS CORRECTAMENTE. " + Utils.Now.ToString("dd/MM/yyyy HH:mm:ss");
                }
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURADEVENTA, Logger.typeUserActions.UPDATE, "ACTUALIZO LA FACTURA DE CLIENTE FOLIO: " + this.txtFacturaIDToMod.Text);
                this.ActualizaMovimientosRelatedToFactura();

            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "err updating boleta", ref ex);
                if (showMessage)
                {
                    this.imgBien.Visible = false;
                    this.imgMal.Visible = this.pnlFacturaResult.Visible = true;
                    this.lblFacturaResult.Text = "ESTO ES VERGONZOSO, HA OCURRIDO UNA EXCEPCION Y NO SE PUDO GUARDAR LA FACTURA.<BR>POR FAVOR ESPERE UN MOMENTO E INTENTELO DE NUEVO<BR>Descripción del error<BR>" + ex.Message;
                }
            }
            finally
            {
                conn.Close();
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURADEVENTA, Logger.typeUserActions.UPDATE, this.UserID, "GUARDO LA FACTURA  DE VENTA: " + this.txtNumFactura.Text);
            }
        }
        private void ActualizaMovimientosRelatedToFactura()
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Close();
                SqlCommand cmdUpdateMovimientos = new SqlCommand("UPDATEFACTURACAMPOINMOVIMIENTOSCUENTABANCO", conn);
                conn.Open();
                cmdUpdateMovimientos.CommandType = CommandType.StoredProcedure;
                cmdUpdateMovimientos.Parameters.Add("@FacturaCV", SqlDbType.Int).Value = int.Parse(this.txtFacturaIDToMod.Text);
                cmdUpdateMovimientos.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "ERROR AL ACTUALIZAR EL CAMPO DE FACTURA EN MOV BANCOS ",ex);
            }
            finally
            {
                conn.Close();
            }

              

        }
        protected void btnActualizaPagos_Click(object sender, EventArgs e)
        {
            this.gvPagosFactura.DataBind();
        }

        protected void btnActualizaTotales_Click(object sender, EventArgs e)
        {
            this.ActualizaTotales();
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

	                comm.CommandText = "DELETE FROM  FacturasCV_Boletas WHERE (boletaID = @boletaID);";
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
        }

      
        protected void btnAddPago_Click(object sender, EventArgs e)
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
                
            if (this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE")
            {
                if (int.TryParse(this.txtChequeNum.Text, out cheque))
                {
                    if (dbFunctions.ChequeAlreadyExists(cheque, this.cmbCuentaPago.SelectedValue))
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
                if (!dbFunctions.numChequeValido(cheque, int.Parse(this.cmbCuentaPago.SelectedValue)))
                {

                    this.pnlNewPago.Visible = true;
                    this.imgBienPago.Visible = false;
                    this.imgMal.Visible = false;
                    this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, EL NUMERO DE CHEQUE NO CORRESPONDE A EL NUMERO DE CUENTA";


                    return;


                }
            }
            double dCurrPagos = 0;
            foreach(GridViewRow row in this.gvPagosFactura.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    double dValor = 0;
                    try
                    {
                        dValor = double.Parse(row.Cells[4].Text, NumberStyles.Currency);
                    }
                    catch
                    {
                    	dValor = 0;
                    }
                    dCurrPagos -= dValor;
                    try
                    {
                        dValor = double.Parse(row.Cells[5].Text, NumberStyles.Currency);
                    }
                    catch
                    {
                        dValor = 0;
                    }
                    dCurrPagos += dValor;

                    try
                    {
                        dValor = double.Parse(row.Cells[6].Text, NumberStyles.Currency);
                    }
                    catch
                    {
                        dValor = 0;
                    }
                    dCurrPagos -= dValor;
                    try
                    {
                        dValor = double.Parse(row.Cells[7].Text, NumberStyles.Currency);
                    }
                    catch
                    {
                        dValor = 0;
                    }
                    dCurrPagos += dValor;


                }
            }
            this.ActualizaTotales();
            double dRestoaPagar = 0;
            try
            {
                dRestoaPagar = double.Parse(this.lblTotal.Text, NumberStyles.Currency);
            }
            catch
            {
                dRestoaPagar = 0;
            }
            
            if (monto > (dRestoaPagar-dCurrPagos))
            {
                this.pnlNewPago.Visible = true;
                this.imgBienPago.Visible = false;
                this.imgMalPago.Visible = true;
                this.lblNewPagoResult.Text = "NO SE PUEDE REALIZAR UN PAGO MAYOR AL MONTO DE LA FACTURA";
                //this.cmbTipodeMovPago.SelectedIndex = 0;
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

            //this.cmbTipodeMov.DataBind();
            dsMovBanco.dtMovBancoDataTable tablaaux = new dsMovBanco.dtMovBancoDataTable();
            dsMovBanco.dtMovBancoRow dtRowainsertar = tablaaux.NewdtMovBancoRow();
            dtRowainsertar.chequecobrado = false;
            dtRowainsertar.conceptoID = int.Parse(this.cmbConceptomovBancoPago.SelectedValue);
            dtRowainsertar.nombre = this.txtNombrePago.Text;
            dtRowainsertar.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaNPago.Text));
            //dats de cheque
            dtRowainsertar.numCheque = this.txtChequeNum.Text.Length > 0 ? int.Parse(this.txtChequeNum.Text) : 0;
            dtRowainsertar.chequeNombre = this.txtChequeNombre.Text;
            dtRowainsertar.facturaOlarguillo = this.txtNumFactura.Text.Trim();
            dtRowainsertar.numCabezas = 0;//this.txtNumCabezas.Text.Length > 0 ? double.Parse(this.txtNumCabezas.Text) : 0;        
            dtRowainsertar.catalogoMovBancoFiscalID = int.Parse(this.drpdlCatalogocuentafiscalPago.SelectedValue);
            if (this.drpdlSubcatalogofiscalPago.SelectedIndex > -1)
                dtRowainsertar.subCatalogoMovBancoFiscalID = int.Parse(this.drpdlSubcatalogofiscalPago.SelectedValue);

            if (dtRowainsertar.numCheque > 0)
            {
                dtRowainsertar.catalogoMovBancoInternoID = int.Parse(this.drpdlCatalogoInternoPago.SelectedValue);
                if (this.drpdlSubcatologointernaPago.SelectedIndex > -1)
                    dtRowainsertar.subCatalogoMovBancoInternoID = int.Parse(this.drpdlSubcatologointernaPago.SelectedValue);
            }
            else
            {
                dtRowainsertar.catalogoMovBancoInternoID = dtRowainsertar.catalogoMovBancoFiscalID;
                dtRowainsertar.subCatalogoMovBancoInternoID = dtRowainsertar.subCatalogoMovBancoFiscalID;
            }
            dtRowainsertar.cargo = 0.00;
            dtRowainsertar.abono = this.txtMonto.Text.Length > 0 ? double.Parse(this.txtMonto.Text) : 0;
            dtRowainsertar.storeTS = DateTime.Parse(Utils.getNowFormattedDate());
            dtRowainsertar.updateTS = DateTime.Parse(Utils.getNowFormattedDate());
            String serror = "", tipo = "";
            dtRowainsertar.cuentaID = int.Parse(this.cmbCuentaPago.SelectedValue);
            dtRowainsertar.creditoFinancieroID = -1;
            ListBox a = new ListBox();
            if (dbFunctions.insertaMovBanco(ref dtRowainsertar, ref serror, int.Parse(this.Session["USERID"].ToString()), int.Parse(this.cmbCuentaPago.SelectedValue), int.Parse(this.drpdlCiclo.SelectedValue), -1,"",this.txtObserv.Text))
            {

                SqlConnection connFactura = new SqlConnection(myConfig.ConnectionInfo);
                try
                {
                    connFactura.Open();
                    SqlCommand commFactura = new SqlCommand();
                    commFactura.Connection = connFactura;
                    commFactura.CommandText = "INSERT INTO PagosFacturasClientesVenta (FacturaCVID, fecha, movbanID, userID) VALUES (@FacturaCVID,@fecha,@movbanID,@userID) ";
                    //(@FacturaCVID,@fecha,@movbanID,@movCajaID,@userID)
                    commFactura.Parameters.Add("@FacturaCVID", SqlDbType.Int).Value = int.Parse(this.txtFacturaIDToMod.Text);
                    commFactura.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Parse(this.txtFechaNPago.Text);
                    commFactura.Parameters.Add("@movbanID", SqlDbType.Int).Value = dtRowainsertar.movBanID;
                    commFactura.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                    if (commFactura.ExecuteNonQuery() != 1)
                    {
                        throw new Exception("This must almost never happen");
                    
                    }
                    this.lblNewPagoResult.Text = "SE AGREGÓ EL MOVIMIENTO EXITOSAMENTE.";             
                    this.ActualizaMovimientosRelatedToFactura();
                    GuardarFactura(false);
                    this.btnActualizaPagos_Click(null, null);
                    this.pnlNewPago.Visible = true;
                    this.imgBienPago.Visible = true;
                    this.imgMalPago.Visible = false;
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
            else
            {
                this.pnlNewPago.Visible = true;
                this.imgBienPago.Visible = false;
                this.imgMalPago.Visible = true;
                this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO";
            }
        }

       

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            
        }

        protected void gvPagosFactura_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Button btn = (Button)e.Row.FindControl("btnDelete");
            if (btn != null)
            {
                JSUtils.AddConfirmToCtrlOnClick(ref btn, "¿Realmente desea eliminar el pago?");
            }
        }

        protected void gvPagosFactura_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (e.Keys["pagoFVID"] != null)
            {
                SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                try
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    conn.Open();

                    comm.CommandText = "DELETE FROM  PagosFacturasClientesVenta WHERE (pagoFVID = @pagoFVID);";
                    comm.Parameters.Add("@pagoFVID", SqlDbType.Int).Value = e.Keys["pagoFVID"];
                    comm.ExecuteNonQuery();
                    this.gvBoletas.DataBind();
                    this.imgBien.Visible  = this.pnlFacturaResult.Visible = true;
                    this.lblFacturaResult.Text = "MOVIMIENTO ELIMINADO EXITOSAMENTE.";
                    this.imgMal.Visible = false;
                    this.GuardarFactura(false);
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.DELETE, "deleting PAgo factura " + e.Keys["pagoFVID"].ToString(), ref ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected void gvPagosFactura_DataBound(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                conn.Open();

                comm.CommandText = "SELECT     SUM(ISNULL(BancoCargo,0)) AS BancoCargo, SUM(ISNULL(BancoAbono,0)) AS BancoAbono, SUM(ISNULL(CajaCargo,0)) AS CajaCargo, SUM(ISNULL(CajaAbono,0)) AS CajaAbono, SUM(ISNULL(BancoAbono,0) - ISNULL(BancoCargo,0) + ISNULL(CajaAbono,0) - ISNULL(CajaCargo,0))as Total, FacturaCVID FROM         dbo.PagosFacturasCliente group by FacturaCVID HAVING (FacturaCVID = @FacturaCVID);";
                comm.Parameters.Add("@FacturaCVID", SqlDbType.Int).Value = int.Parse(this.txtFacturaIDToMod.Text);
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    Label lbl = (Label)this.gvPagosFactura.FooterRow.FindControl("lblCargoBanco");
                    if (lbl != null)
                    {
                        lbl.Text = string.Format("{0:C2}", double.Parse(reader["BancoCargo"].ToString()));
                    }
                    lbl = (Label)this.gvPagosFactura.FooterRow.FindControl("lblAbonoBanco");
                    if (lbl != null)
                    {
                        lbl.Text = string.Format("{0:C2}", double.Parse(reader["BancoAbono"].ToString()));
                    }
                    lbl = (Label)this.gvPagosFactura.FooterRow.FindControl("lblCargoCajaChica");
                    if (lbl != null)
                    {
                        lbl.Text = string.Format("{0:C2}", double.Parse(reader["CajaCargo"].ToString()));
                    }
                    lbl = (Label)this.gvPagosFactura.FooterRow.FindControl("lblAbonoCajaChica");
                    if (lbl != null)
                    {
                        lbl.Text = string.Format("{0:C2}", double.Parse(reader["CajaAbono"].ToString()));
                    }
                    lbl = (Label)this.gvPagosFactura.FooterRow.FindControl("lblTotales");
                    if (lbl != null)
                    {
                        lbl.Text = string.Format("{0:C2}", double.Parse(reader["Total"].ToString()));
                    }
                }
                this.CalculaPagos();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "err getting concentrado de pagos", ref ex);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void ddlClientesVentaboletas_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvBoletasDisponibles.DataBind();
        }

        protected void gvBoletasDisponibles_DataBound(object sender, EventArgs e)
        {
            this.btnAddBoletasIntoFactura.Visible = this.gvBoletasDisponibles.Rows.Count > 0;
        }

        protected void btnAddBoletasIntoFactura_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                conn.Open();
                comm.CommandText = "INSERT INTO dbo.FacturasCV_Boletas (FacturaCV, boletaID) VALUES (@FacturaCV,@boletaID)";
                foreach(GridViewRow row in this.gvBoletasDisponibles.Rows)
                {
                    if (((CheckBox)row.FindControl("chkAddBoleta")).Checked)
                    {
                        comm.Parameters.Clear();
                        comm.Parameters.Add("@FacturaCV", SqlDbType.Int).Value = int.Parse(this.txtFacturaIDToMod.Text);
                        comm.Parameters.Add("@boletaID", SqlDbType.Int).Value = int.Parse(this.gvBoletasDisponibles.DataKeys[row.RowIndex][0].ToString());
                        comm.ExecuteNonQuery();

                        DataRow BoletaData = dbFunctions.GetBoletaData(int.Parse(this.gvBoletasDisponibles.DataKeys[row.RowIndex][0].ToString()));
                        if(BoletaData != null)
                        {
                            dsFacturasClientes.dtFacAClientesDetallesRow newRow = this.dtFacDetalle.NewdtFacAClientesDetallesRow();
                            newRow.FacturaCVID = int.Parse(this.txtFacturaIDToMod.Text);

                            decimal dTemp = 0;
                            dTemp = decimal.Parse(BoletaData["pesonetosalida"].ToString());
                            newRow.Cantidad = dTemp;
                            newRow.Producto = dbFunctions.GetProductoName(BoletaData["productoID"].ToString());
                            newRow.productoID = (int)BoletaData["productoID"];
                            dTemp = 0;
                            decimal.TryParse(BoletaData["precioapagar"].ToString(), out dTemp);
                            newRow.precio = dTemp;
                            newRow.Importe = newRow.Cantidad * newRow.precio;
                            this.dtFacDetalle.Rows.Add(newRow);
                            this.SaveDTInSession();
                        }
                    }
                }
                this.grdvProNotas.DataSource = this.dtFacDetalle;
                this.grdvProNotas.DataBind();
                this.gvBoletas.DataBind();
                this.ddlClientesVentaboletas.DataBind();
                this.gvBoletasDisponibles.DataBind();

            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "Adding boletas into facturas", ref ex);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void gvBoletas_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            if(e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "delete boleta from factura " + this.txtFacturaIDToMod.Text , e.Exception);
                e.ExceptionHandled = true;
            }
            this.gvBoletas.DataBind();
            this.ddlClientesVentaboletas.DataBind();
            this.gvBoletasDisponibles.DataBind();
        }

        protected void gvBoletas_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            if(e.Exception!=null)
            {
                e.ExceptionHandled = true;
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "Erro modificando boleta", e.Exception);
            }
        }

        protected void gvBoletas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            double pesoTotal = Math.Abs(double.Parse(e.NewValues["PesoDeEntrada"].ToString()) - double.Parse(e.NewValues["PesoDeSalida"].ToString()));
            e.NewValues["pesonetoapagar"] = pesoTotal;
            e.NewValues["pesonetosalida"] = pesoTotal;
         
        }

        protected void ddlEfectivoGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadCatalogos();
        }

        protected void LoadCatalogos()
        {
            try
            {
                DropDownList ddl = (DropDownList)this.dvAddEfectivo.FindControl("ddlEfectivoGrupo");
                SqlDataSource sds = (SqlDataSource)this.dvAddEfectivo.FindControl("sdsEfectivoCatalogos");
                DropDownList ddl2 = (DropDownList)this.dvAddEfectivo.FindControl("ddlEfectivoCatalogo");
                if (ddl != null && sds != null)
                {
                    sds.FilterExpression = "grupoCatalogoID=" + ddl.SelectedValue;
                    ddl2.DataBind();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "changing the subcatalogos", ex);
            }
        }

        protected void ddlEfectivoGrupo_DataBound(object sender, EventArgs e)
        {
            this.LoadCatalogos();
        }

        protected void ddlEfectivoCatalogo_DataBound(object sender, EventArgs e)
        {
            this.LoadSubCatalogos();
        }

        protected void ddlEfectivoCatalogo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadSubCatalogos();
        }
        protected void LoadSubCatalogos()
        {
            try
            {
                DropDownList ddl = (DropDownList)this.dvAddEfectivo.FindControl("ddlEfectivoCatalogo");
                SqlDataSource sds = (SqlDataSource)this.dvAddEfectivo.FindControl("sdsEfectivoSubCatalogos");
                DropDownList ddl2 = (DropDownList)this.dvAddEfectivo.FindControl("ddlEfectivoSubCatalogo");
                if (ddl != null && sds != null)
                {
                    sds.FilterExpression = "catalogoMovBancoID=" + ddl.SelectedValue;
                    ddl2.DataBind();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "changing the subcatalogos", ex);
            }
        }

        protected void dvAddEfectivo_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            //@cargo, @abono, @catalogoMovBancoID, @subCatalogoMovBancoID, @bodegaID)
            try
            {
                e.Values["userID"] = this.UserID;
                e.Values["cargo"] = 0;
                e.Values["catalogoMovBancoID"] = ((DropDownList)this.dvAddEfectivo.FindControl("ddlEfectivoCatalogo")).SelectedValue;
                e.Values["subCatalogoMovBancoID"] = ((DropDownList)this.dvAddEfectivo.FindControl("ddlEfectivoSubCatalogo")).SelectedValue;
                e.Values["subCatalogoMovBancoID"] = e.Values["subCatalogoMovBancoID"] == null || e.Values["subCatalogoMovBancoID"].ToString().Trim().Length == 0 ? -1 : e.Values["subCatalogoMovBancoID"];
                e.Values["FacturaCVID"] = this.txtFacturaIDToMod.Text;
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "configuring values for efectivo", ex);
            }
        }

        protected void dvAddEfectivo_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "inserting mov Efectivo", e.Exception);
                this.lblEfectivoResult.Text = "NO SE PUDO AGREGAR EL PAGO, REVISE LOS DATOS E INTENTELO DE NUEVO <BR />"
                    + e.Exception.ToString();
                e.ExceptionHandled = true;
            }
            if (e.AffectedRows > 0)
            {
                this.lblEfectivoResult.Text = "EL PAGO FUE AGREGADO EXITOSAMENTE.";
            }
            this.gvPagosFactura.DataBind();
        }

        protected int newID = -1;
        protected void sdsEfectivo_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            if (e.AffectedRows == 2)
            {
                newID = (int)e.Command.Parameters["@newID"].Value;
            }
        }
      

    }
}

