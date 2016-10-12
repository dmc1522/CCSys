using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Garibay
{
    public partial class frmFacturaGanado : BasePage
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
              
            if (!this.IsPostBack)
            {
                this.txtFechaPagoCredito.Text = this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.AddJSToControls();
                try
                {
                    this.ddlProveedores.DataBind();
                    this.ddlProveedores.SelectedIndex = 0;
                }
                finally { }
                if (this.LoadEncryptedQueryString() > 0 && this.myQueryStrings["FacturaID"] != null)
                {
                    this.txtFolio.Text = this.myQueryStrings["FacturaID"].ToString();
                    this.LoadFactura();
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURASGANADO, Logger.typeUserActions.SELECT, "VISITO LA PÁGINA PARA MODIFICAR FACTURA DE GANADO");
         
                }
                else
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURASGANADO, Logger.typeUserActions.SELECT, "VISITO LA PÁGINA PARA MODIFICAR FACTURA DE GANADO");
                }

                this.drpdlGrupoCuentaFiscal.DataBind();
                this.drpdlGrupoCuentaFiscal.SelectedValue = "3";
                this.drpdlGrupoCatalogosInternaPago.DataBind();
                this.drpdlGrupoCatalogosInternaPago.SelectedValue = "3";
                this.drpdlCatalogocuentafiscalPago.DataBind();
                this.drpdlCatalogoInternoPago.DataBind();
                this.ddlProveedores.DataBind();
                this.cmbConceptomovBancoPago.DataBind();
                this.txtFechaNPago.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.txtNombrePago.Text = this.ddlProveedores.SelectedItem.Text;

                this.ddlCiclosPagoCredito.DataBind();
                this.ddlCreditoAPagar.DataBind();
                
            }
            this.pnlNewPago.Visible = false;
            this.lblAddAbonoResult.Text = string.Empty;
            this.divAgregarNuevoPago.Attributes.Add("style", this.chkMostrarAgregarPago.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divCheque.Attributes.Add("style", this.cmbConceptomovBancoPago.SelectedItem != null && this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");

        }

        protected void LoadFactura()
        {
            SqlCommand comm = new SqlCommand();
            SqlDataReader r = null;
            try
            {
                comm.CommandText = "SELECT     fecha, userID, ganProveedorID FROM FacturasdeGanado "
                + " WHERE     (FacturadeGanadoID = @FacturadeGanadoID)";
                comm.Parameters.Add("@FacturadeGanadoID", SqlDbType.Int).Value = this.txtFolio.Text;
                r = dbFunctions.ExecuteReader(comm);
                if (r.HasRows && r.Read())
                {
                    this.txtFechaPagoCredito.Text = this.txtFecha.Text = DateTime.Parse(r["fecha"].ToString()).ToString("dd/MM/yyyy");
                    this.ddlProveedores.DataBind();
                    this.ddlProveedores.SelectedValue = r["ganProveedorID"].ToString();
                    this.btnNewFactura.Visible = false;
                    this.pnlDetalle.Visible = true;
                    this.gvGanado.DataBind();
                    this.dvTotales.DataBind();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "cargando factura :" + this.txtFolio.Text, ref ex);
            }
            finally
            {
                if (r != null && comm.Connection != null)
                {
                    comm.Connection.Close();
                }
            }
            this.UpdatePrintButton();
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                double dKG = 0;
                double dDieta = 0;
                double dPrecio = 0;
                if (!double.TryParse(this.txtKG.Text, out dKG))
                {
                    dKG = 0;
                }
                if (!double.TryParse(this.txtPrecio.Text, out dPrecio))
                {
                    dPrecio = 0;
                }
                if (!double.TryParse(this.txtDieta.Text, out dDieta))
                {
                    dDieta = 0;
                }
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "INSERT INTO FacturasDeGanadoDetalle "
                    + " (FacturadeGanadoID, arete, Factura, KG, No, productoID, dieta, KGNetos, Precio) "
                    + " VALUES (@FacturadeGanadoID,@arete,@Factura,@KG,@No,@productoID,@dieta,@KGNetos,@Precio)";
                comm.Parameters.Add("@FacturadeGanadoID", SqlDbType.Int).Value = this.txtFolio.Text;
                comm.Parameters.Add("@arete", SqlDbType.VarChar).Value = this.txtArete.Text;
                comm.Parameters.Add("@Factura", SqlDbType.VarChar).Value = this.txtFactura.Text;
                comm.Parameters.Add("@KG", SqlDbType.Float).Value = dKG;
                comm.Parameters.Add("@No", SqlDbType.Int).Value = this.txtCantidad.Text;
                comm.Parameters.Add("@productoID", SqlDbType.Int).Value = this.ddlProductos.SelectedItem.Value;
                comm.Parameters.Add("@dieta", SqlDbType.Float).Value = dDieta;
                comm.Parameters.Add("@KGNetos", SqlDbType.Float).Value = dKG - (dKG * dDieta / 100.0);
                comm.Parameters.Add("@Precio", SqlDbType.Float).Value = dPrecio;
                if (comm.ExecuteNonQuery() > 0)
                {
                    this.gvGanado.DataBind();
                    this.txtArete.Text = "";
                    this.txtFactura.Text = "";
                    this.txtKG.Text = "";
                    this.txtCantidad.Text = "";
                    this.txtDieta.Text = "0";
                    this.txtPrecio.Text = "";
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(Garibay.Logger.typeUserActions.INSERT, "Error insertando producto a factura ganado", ref ex);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void ddlProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dvProveedorData.DataBind();
            this.txtNombrePago.Text = this.ddlProveedores.SelectedItem.Text;
            this.gvFacturasEnCero.DataBind();
        }
        protected void btnNewFactura_Click(object sender, EventArgs e)
        {
            int newID = -1;
            SqlCommand comm = new SqlCommand();
            string sRedirect = string.Empty;
            try
            {
                comm.CommandText = "INSERT INTO FacturasdeGanado (fecha, userID, ganProveedorID) VALUES (@fecha,@userID,@ganProveedorID)" +
                "SELECT NewID = SCOPE_IDENTITY();";
                comm.Parameters.Add("@fecha", SqlDbType.DateTime).Value = this.txtFecha.Text;
                comm.Parameters.Add("@userid", SqlDbType.Int).Value = this.UserID;
                comm.Parameters.Add("@ganProveedorID", SqlDbType.Int).Value = this.ddlProveedores.SelectedItem.Value;
                if ((newID = dbFunctions.GetExecuteIntScalar(comm, -1)) > -1)
                {
                    this.myQueryStrings["FacturaID"] = newID.ToString();
                    this.txtFolio.Text = newID.ToString();
//                     sRedirect = "frmFacturaGanado.aspx";
//                     sRedirect += Utils.GetEncriptedQueryString("FacturaID=" + newID.ToString());
                    this.UpdatePrintButton();
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURASGANADO, Logger.typeUserActions.INSERT, this.UserID, "AGREGÓ UNA FACTURA DE GANADO");
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Insertando Factura", ref ex);
            }
            if (newID > 0)
            {
                this.LoadFactura();
            }
        }

        protected void UpdatePrintButton()
        {
            string sRedirect = "frmFacturaGanadoPrint.aspx";
            sRedirect += Utils.GetEncriptedQueryString("FacturaID=" + this.txtFolio.Text);
            JSUtils.OpenNewWindowOnClick(ref this.btnPrint, sRedirect, "Liquidacion de ganado", true);
        }


        protected void gvGanado_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (e.RowIndex > -1 && 
                this.gvGanado.DataKeys[e.RowIndex]["facturaGanDetalleID"] != null)
            {
                sdsDetalleGanado.DeleteParameters["facturaGanDetalleID"].DefaultValue =
                    this.gvGanado.DataKeys[e.RowIndex]["facturaGanDetalleID"].ToString();
            }            
        }

        protected void gvGanado_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvGanado.EditIndex = this.gvGanado.SelectedIndex;
        }

        protected void gvGanado_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGanado.EditIndex = e.NewEditIndex;
        }

        protected void gvGanado_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvGanado.EditIndex = -1;
        }

        protected void gvGanado_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && 
                    this.gvGanado.DataKeys[e.RowIndex]["facturaGanDetalleID"] != null)
                {
                    double dKG = 0;
                    double dDieta = 0;
                    double dPrecio = 0;
                    if (e.NewValues["KG"] == null || !double.TryParse(e.NewValues["KG"].ToString(), out dKG))
                    {
                        dKG = 0;
                    }
                    if (e.NewValues["dieta"] == null || !double.TryParse(e.NewValues["dieta"].ToString(), out dDieta))
                    {
                        dDieta = 0;
                    }
                    if (e.NewValues["Precio"] == null || !double.TryParse(e.NewValues["Precio"].ToString(), out dPrecio))
                    {
                        dPrecio = 0;
                    }

                    this.sdsDetalleGanado.UpdateParameters["arete"].DefaultValue = e.NewValues["arete"] != null? e.NewValues["arete"].ToString() : "";
                    this.sdsDetalleGanado.UpdateParameters["Factura"].DefaultValue = e.NewValues["Factura"] != null? e.NewValues["Factura"].ToString() : "";
                    this.sdsDetalleGanado.UpdateParameters["KG"].DefaultValue = dKG.ToString();
                    this.sdsDetalleGanado.UpdateParameters["No"].DefaultValue = e.NewValues["No"] != null? e.NewValues["No"].ToString() : "";
                    string ProductoID = e.NewValues["productoID"] != null? e.NewValues["productoID"].ToString() : "-1";
                    DropDownList ddl = (DropDownList)this.gvGanado.Rows[e.RowIndex].FindControl("ddlProducto");
                    if (ddl != null)
                    {
                        ProductoID = ddl.SelectedValue;
                    }
                    try
                    {
                        e.NewValues["productoID"] = ProductoID;
                        e.OldValues["productoID"] = e.NewValues["productoID"] != null ? e.NewValues["productoID"].ToString() : "-1";
                    }
                    finally
                    {
                    	
                    }
                    this.sdsDetalleGanado.UpdateParameters["productoID"].DefaultValue = ProductoID;
                    this.sdsDetalleGanado.UpdateParameters["dieta"].DefaultValue = dDieta.ToString();
                    
                    this.sdsDetalleGanado.UpdateParameters["KGNetos"].DefaultValue = (dKG - (dKG * dDieta / 100.0)).ToString();
                    this.sdsDetalleGanado.UpdateParameters["Precio"].DefaultValue = dPrecio.ToString();

                    sdsDetalleGanado.UpdateParameters["facturaGanDetalleID"].DefaultValue =
                    this.gvGanado.DataKeys[e.RowIndex]["facturaGanDetalleID"].ToString();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "updating row factura ganado", ref ex);
            }
        }

        protected void gvGanado_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "UPDATE FacturasDeGanadoDetalle SET productoID = @productoID WHERE (facturaGanDetalleID = @facturaGanDetalleID)";
                DropDownList ddl = (DropDownList)this.gvGanado.Rows[this.gvGanado.EditIndex].FindControl("ddlProducto");
                string ProductoID = "-1";
                if (ddl != null)
                {
                    ProductoID = ddl.SelectedValue;
                }
                try
                {
                    e.NewValues["productoID"] = ProductoID;
                    e.OldValues["productoID"] = e.NewValues["productoID"].ToString();
                }
                finally { }
                comm.Parameters.Add("@productoID", SqlDbType.Int).Value = int.Parse(ProductoID);
                comm.Parameters.Add("facturaGanDetalleID", SqlDbType.Int).Value =
                        this.gvGanado.DataKeys[this.gvGanado.EditIndex]["facturaGanDetalleID"].ToString();
                dbFunctions.ExecuteCommand(comm);
                if (comm.Connection != null)
                {
                    comm.Connection.Close();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "Row Updated", ref ex);
            }
            
        }
        private void AddJSToControls()
        {
            

            String sOnchangeAB = "ShowHideDivOnChkBox('";
            sOnchangeAB += this.chkMostrarAgregarPago.ClientID + "','";
            sOnchangeAB += this.divAgregarNuevoPago.ClientID + "')";
            this.chkMostrarAgregarPago.Attributes.Add("onChange", sOnchangeAB);

            String sOnchagemov = "checkValueInList(";
            sOnchagemov += "this" + ",'CHEQUE','";
            sOnchagemov += this.divCheque.ClientID + "');";
            this.cmbConceptomovBancoPago.Attributes.Add("onChange", sOnchagemov);
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
                this.pnlNewPago.Focus();
                return;
            }
            //COMPROBAMOS QUE NO SE PASE DE TOTAL
            double totalFactura = 0, totalAbonos = 0;
            try
            {
                double.TryParse(this.dvTotales.Rows[0].Cells[1].Text, NumberStyles.Currency, null, out totalFactura);
                double.TryParse(this.dvTotales.Rows[1].Cells[1].Text, NumberStyles.Currency, null, out totalAbonos);
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Calculando total y pagos para evitar pagar de mas", ref ex);
            }
            if((totalAbonos + monto) > totalFactura)
            {       
                this.pnlNewPago.Visible = true;
                this.imgMalPago.Visible = true;
                this.imgBienPago.Visible = false;
                this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, LOS PAGOS NO PUEDEN SER MAYORES AL TOTAL DE LA FACTURA";
                this.pnlNewPago.Focus();
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
                    this.imgMalPago.Visible = false;
                    this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, EL NUMERO DE CHEQUE NO CORRESPONDE A EL NUMERO DE CUENTA";


                    return;


                }
            }
            
            if (dbFunctions.FechaEnPeriodoBloqueado(DateTime.Parse(this.txtFechaNPago.Text), int.Parse(this.cmbCuentaPago.SelectedValue)))
            {
                this.pnlNewPago.Visible = true;
                this.imgBienPago.Visible = false;
                this.imgMalPago.Visible = true;
                this.lblNewPagoResult.Text = "EL MOVIMIENTO NO PUEDE SER AGREGADO YA QUE LA FECHA ESTA DENTRO DE UN PERIODO BLOQUEADO<BR />DESBLOQUEE EL PERIODO PARA PERMITIR INGRESAR EL MOVIMIENTO";
                return;
            }

            dsMovBanco.dtMovBancoDataTable tablaaux = new dsMovBanco.dtMovBancoDataTable();
            dsMovBanco.dtMovBancoRow dtRowainsertar = tablaaux.NewdtMovBancoRow();
            dtRowainsertar.chequecobrado = false;
            dtRowainsertar.conceptoID = int.Parse(this.cmbConceptomovBancoPago.SelectedValue);
            dtRowainsertar.nombre = this.txtNombrePago.Text;
            dtRowainsertar.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaNPago.Text));
            dtRowainsertar.numCheque = this.txtChequeNum.Text.Length > 0 ? int.Parse(this.txtChequeNum.Text) : 0;
            dtRowainsertar.chequeNombre = this.txtChequeNombre.Text;
            dtRowainsertar.facturaOlarguillo = this.txtFolio.Text;
            dtRowainsertar.numCabezas = 0;
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

            dtRowainsertar.cargo = this.txtMonto.Text.Length > 0 ? double.Parse(this.txtMonto.Text) : 0;
            dtRowainsertar.abono = 0.00;
            dtRowainsertar.storeTS = DateTime.Parse(Utils.getNowFormattedDate());
            dtRowainsertar.updateTS = DateTime.Parse(Utils.getNowFormattedDate());
            String serror = "", tipo = "";
            dtRowainsertar.cuentaID = int.Parse(this.cmbCuentaPago.SelectedValue);
            dtRowainsertar.creditoFinancieroID = -1;
            ListBox a = new ListBox();
            if (dbFunctions.insertaMovBanco(ref dtRowainsertar, ref serror, int.Parse(this.Session["USERID"].ToString()), int.Parse(this.cmbCuentaPago.SelectedValue), dbFunctions.GetLastCiclo(), -1, "", this.txtObserv.Text))
            {
                SqlConnection connFactura = new SqlConnection(myConfig.ConnectionInfo);
                try
                {
                    connFactura.Open();
                    SqlCommand commFactura = new SqlCommand();
                    commFactura.Connection = connFactura;
                    commFactura.CommandText = "INSERT INTO PagosFacturadeGanado (FacturadeGanadoID, movbanID, montoPago) VALUES (@FacturaID,@movbanID,@montoPago) ";
                    //(@FacturaCVID,@fecha,@movbanID,@movCajaID,@userID)
                    commFactura.Parameters.Add("@FacturaID", SqlDbType.Int).Value = int.Parse(this.txtFolio.Text);
                    commFactura.Parameters.Add("@movbanID", SqlDbType.Int).Value = dtRowainsertar.movBanID;
                    commFactura.Parameters.Add("@montoPago", SqlDbType.Float).Value = dtRowainsertar.cargo;
                    if (commFactura.ExecuteNonQuery() != 1)
                    {
                        throw new Exception("This must almost never happen");
                    }
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error adding new movbanco->factura", ref ex);
                }
                finally
                {
                    connFactura.Close();
                }
                //this.btnActualizarLista.DataBind();
                this.pnlNewPago.Visible = true;
                this.imgBienPago.Visible = true;
                this.imgMalPago.Visible = false;
                this.lblNewPagoResult.Text = "SE AGREGO EL PAGO CORRECTAMENTE";
                this.gvPagosFactura.DataBind();
                this.dvTotales.DataBind();
            }
            else
            {
                this.pnlNewPago.Visible = true;
                this.imgBienPago.Visible = false;
                this.imgMalPago.Visible = true;
                this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO";
            }

        }

        protected void btnActualizarLista_Click(object sender, EventArgs e)
        {
            this.gvPagosFactura.DataBind();
        }

        protected void gvPagosFactura_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection conDelete = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdDelete = new SqlCommand();
            try
            {
                conDelete.Open();
                cmdDelete.Connection = conDelete;
                cmdDelete.CommandText = "delete from PagosFacturaDeGanado where pagoID = @pagoID;delete from MovimientosCuentasBanco where movbanID = @movbanID;";
                cmdDelete.Parameters.Add("@pagoID", SqlDbType.Int).Value = (int)(this.gvPagosFactura.DataKeys[e.RowIndex]["pagoID"]);
                cmdDelete.Parameters.Add("@movbanID", SqlDbType.Int).Value = (int)(this.gvPagosFactura.DataKeys[e.RowIndex]["movbanID"]);
                cmdDelete.ExecuteNonQuery();
                this.gvPagosFactura.DataBind();               
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "ERROR DELETING PAGOS IN FACTURAS DE GANADO", ref ex);
            }
            finally
            {
                conDelete.Close();
            }
        }

        protected void gvGanado_DataBound(object sender, EventArgs e)
        {
            this.gvConcentrado.DataBind();
            this.dvTotales.DataBind();
            try
            {               
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "SELECT SUM(KG) as KGs, SUM(No) as cabezas, SUM(KGNetos) as Netos, SUM(KGNetos * Precio) AS Total FROM FacturasDeGanadoDetalle WHERE (FacturadeGanadoID = @FacturadeGanadoID) GROUP BY FacturadeGanadoID";
                comm.Parameters.Add("@FacturadeGanadoID", SqlDbType.Int).Value = this.txtFolio.Text;
                SqlDataReader reader = dbFunctions.ExecuteReader(comm);
                if (reader.HasRows && reader.Read())
                {
                    Label lbl = (Label)this.gvGanado.FooterRow.FindControl("lblTotalCabezas");
                    if (lbl != null)
                    {
                        lbl.Text = reader["cabezas"] != null? reader["cabezas"].ToString() : "0";
                    }
                    lbl = (Label)this.gvGanado.FooterRow.FindControl("lblKGBrutos");
                    if (lbl != null)
                    {
                        lbl.Text = reader["KGs"] != null ? ((double)reader["KGs"]).ToString("N2") : "0.00";
                    }
                    lbl = (Label)this.gvGanado.FooterRow.FindControl("lblKGNetos");
                    if (lbl != null)
                    {
                        lbl.Text = reader["Netos"] != null ? ((double)reader["Netos"]).ToString("N2") : "0.00";
                    }
                    lbl = (Label)this.gvGanado.FooterRow.FindControl("lblTotales");
                    if (lbl != null)
                    {
                        lbl.Text = reader["Total"] != null ? ((double)reader["Total"]).ToString("C2") : "$0.00";
                    }
                    
                    
                }
                if (comm.Connection != null)
                {
                    comm.Connection.Close();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "calculando totales", ref ex);
            }
        }

        protected void gvPagosFactura_DataBound(object sender, EventArgs e)
        {
            this.dvTotales.DataBind();
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

        protected void gvAnticiposFactura_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection conDelete = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdDelete = new SqlCommand();
            try
            {
                conDelete.Open();
                cmdDelete.Connection = conDelete;
                cmdDelete.CommandText = "update anticipos_facturasganado set FacturadeGanadoID = NULL where movbanID = @movbanID";
                cmdDelete.Parameters.Add("@movbanID", SqlDbType.Int).Value = (int)(this.gvAnticiposFactura.DataKeys[e.RowIndex]["movbanID"]);
                cmdDelete.ExecuteNonQuery();
                this.gvAnticiposDadosAlProductor.DataBind();
                this.gvAnticiposFactura.DataBind();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "ERROR DELETING PAGOS IN FACTURAS DE GANADO", ref ex);
            }
            finally
            {
                conDelete.Close();
            }
        }

        protected void btnAddAnticiposAFactura_Click(object sender, EventArgs e)
        {
            foreach(GridViewRow row in this.gvAnticiposDadosAlProductor.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkAddAnticipo");
                if (chk != null && chk.Checked)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = "update anticipos_facturasganado set FacturadeGanadoID = @FacturadeGanadoID where movbanID = @movbanID";
                    comm.Parameters.Add("@FacturadeGanadoID", SqlDbType.Int).Value = this.txtFolio.Text;
                    comm.Parameters.Add("@movbanID", SqlDbType.Int).Value = (int)(this.gvAnticiposDadosAlProductor.DataKeys[row.RowIndex]["movbanID"]);
                    dbFunctions.ExecuteCommand(comm);
                    if (comm.Connection != null)
                    {
                        comm.Connection.Close();
                    }

                }
            }
            this.gvAnticiposDadosAlProductor.DataBind();
            this.gvAnticiposFactura.DataBind();
        }

        protected string GetOpenFacturaURL(string id)
        {
            string sRedirect = "frmFacturaGanado.aspx";
            sRedirect += Utils.GetEncriptedQueryString("FacturaID=" + id);
            return sRedirect;
        }

        protected void gvFacturasEnCero_DataBound(object sender, EventArgs e)
        {
            this.btnNewFactura.Visible = this.gvFacturasEnCero.Rows.Count == 0;
            this.gvFacturasEnCero.Visible = this.txtFolio.Text.Trim().Length == 0;
        }

        protected void gvAnticiposDadosAlProductor_DataBound(object sender, EventArgs e)
        {
            this.pnlAnticiposProveedor.Visible = this.gvAnticiposDadosAlProductor.Rows.Count > 0;
        }

        protected void gvPesosMerma_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvPesosMerma.EditIndex = e.NewEditIndex;
        }

        protected void gvPesosMerma_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvPesosMerma.EditIndex = -1;
        }

        protected void gvPesosMerma_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 &&
                    this.txtFolio.Text.Trim().Length >0)
                {

                    this.sdsPesosMerma.UpdateParameters["pesoembarcado"].DefaultValue = e.NewValues["pesoembarcado"].ToString();
                    this.sdsPesosMerma.UpdateParameters["pesodestino"].DefaultValue = e.NewValues["pesodestino"].ToString();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "updating row factura ganado", ref ex);
            }
        }

        protected void gvPesosMerma_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            this.gvPesosMerma.DataBind();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmd = new SqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE FacturasdeGanado SET ganProveedorID = @ganProveedorID, fecha = @fecha WHERE     (FacturadeGanadoID = @FacturadeGanadoID)";
                cmd.Parameters.Add("@ganProveedorID", SqlDbType.Int).Value = int.Parse(this.ddlProveedores.SelectedItem.Value);
                cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Parse(this.txtFecha.Text);
                cmd.Parameters.Add("@FacturadeGanadoID", SqlDbType.Int).Value = int.Parse(this.txtFolio.Text);
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "ERROR UPDATING FACTURA DE GANADO", ref ex);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void ddlCiclosPagoCredito_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlCreditoAPagar.DataBind();
        }

        protected void btnAddAbonoCredito_Click(object sender, EventArgs e)
        {
            this.lblAddAbonoResult.Text = string.Empty;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "INSERT INTO FacturaGanado_Credito (FacturadeGanadoID, creditoID, monto,fecha) VALUES (@FacturadeGanadoID,@creditoID,@monto, @fecha); UPDATE Creditos SET pagado = @pagado WHERE (creditoID = @creditoID);";
                comm.Connection = conn;
                conn.Open();
                comm.Parameters.Add("@FacturadeGanadoID", SqlDbType.Int).Value = int.Parse(this.txtFolio.Text);
                comm.Parameters.Add("@creditoID", SqlDbType.Int).Value = this.ddlCreditoAPagar.SelectedValue;
                comm.Parameters.Add("@monto", SqlDbType.Money).Value = Utils.GetSafeFloat(this.txtAbonoCredito.Text);
                comm.Parameters.Add("@pagado", SqlDbType.Bit).Value = this.chkMarcarPagado.Checked ? 1 : 0;
                comm.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Parse(this.txtFechaPagoCredito.Text);
                if (comm.ExecuteNonQuery() > 0)
                {
                    this.txtAbonoCredito.Text = "";
                    this.chkMarcarPagado.Checked = false;
                    this.lblAddAbonoResult.Text = "ABONO CREDITO AGREGADO EXITOSAMENTE";
                    this.gvPagosCreditos.DataBind();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "inserting pago en LA FACTURA para credito", ex);
                this.lblAddAbonoResult.Text = "ERROR AGREGANDO EL PAGO A LA FACTURA: " + this.txtFolio.Text + " EX: " + ex.ToString();
            }
            finally
            {
                conn.Close();
            }
            this.gvPagosCreditos.DataBind();
        }

        protected void PopCalendar8_SelectionChanged(object sender, EventArgs e)
        {
            this.ddlCreditoAPagar.DataBind();
        }

        protected void gvPagosCreditos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            
        }

        protected void gvPagosCreditos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                e.NewValues["fecha"] = DateTime.Parse(e.NewValues["fecha"].ToString());
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "editing pago en LA FACTURA para credito", ex);
            }
        }

        protected void gvPagosCreditos_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            this.ddlCreditoAPagar.DataBind();
        }

        protected void gvPagosCreditos_DataBound(object sender, EventArgs e)
        {
            this.dvTotales.DataBind();
        }

    }
}
