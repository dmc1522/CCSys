using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Garibay
{
    public partial class frmFacturaGanadoPrint : BasePage
    {
        protected PdfPTable GetGanaderaData()
        {
            PdfPTable teibol = new PdfPTable(1);
            teibol.AddCell(PdfCreator.getCellWithFormat("AV. PATRIA OTE. #10", true, Element.ALIGN_LEFT));
            teibol.AddCell(PdfCreator.getCellWithFormat("AMECA, JALISCO 46600", true, Element.ALIGN_LEFT));
            teibol.AddCell(PdfCreator.getCellWithFormat("Tel. 01375 7581199 Fax 01375 7580262", true, Element.ALIGN_LEFT));
            return teibol;
        }
        protected PdfPTable GetFacturaData()
        {
            PdfPTable teibol = new PdfPTable(1);
            teibol.AddCell(PdfCreator.getCellWithFormat("FOLIO:", true, Element.ALIGN_LEFT));
            teibol.AddCell(PdfCreator.getCellWithFormat(this.txtFolio.Text, true, Element.ALIGN_LEFT));
            teibol.AddCell(PdfCreator.getCellWithFormat("FECHA:", true, Element.ALIGN_LEFT));
            teibol.AddCell(PdfCreator.getCellWithFormat(this.txtFecha.Text, true, Element.ALIGN_LEFT));
            return teibol;
        }

        protected PdfPTable GetProveedorDetailsViewAsPdfTable(DetailsView dv)
        {
            dv.DataBind();
            PdfPTable teibol = new PdfPTable(2);
            PdfPCell celdaHeader = PdfCreator.getCellWithFormat("PROVEEDOR", true, Element.ALIGN_CENTER);
            celdaHeader.Colspan = 2;
            teibol.AddCell(celdaHeader);
            foreach(DetailsViewRow row in dv.Rows)
            {
                PdfPCell celda = PdfCreator.getCellWithFormat(row.Cells[0].Text.ToUpper(), true, Element.ALIGN_LEFT, new iTextSharp.text.BaseColor(row.Cells[0].ForeColor));
                //celda.BackgroundColor = new iTextSharp.text.BaseColor(row.Cells[0].BackColor);
                teibol.AddCell(celda);
                celda = PdfCreator.getCellWithFormat(row.Cells[1].Text.ToUpper(), true, Element.ALIGN_LEFT, new iTextSharp.text.BaseColor(row.Cells[1].ForeColor));
                //celda.BackgroundColor = new iTextSharp.text.BaseColor(row.Cells[1].BackColor);
                teibol.AddCell(celda);
            }
            return teibol;
        }

        protected PdfPTable GetDetailsViewAsPdfTable(DetailsView dv)
        {
            dv.DataBind();
            PdfPTable teibol = new PdfPTable(2);
            foreach (DetailsViewRow row in dv.Rows)
            {
                PdfPCell celda = PdfCreator.getCellWithFormat(row.Cells[0].Text.ToUpper(), true, Element.ALIGN_LEFT, new iTextSharp.text.BaseColor(row.Cells[0].ForeColor));
                //celda.BackgroundColor = new iTextSharp.text.BaseColor(row.Cells[0].BackColor);
                teibol.AddCell(celda);
                celda = PdfCreator.getCellWithFormat(row.Cells[1].Text.ToUpper(), true, Element.ALIGN_LEFT, new iTextSharp.text.BaseColor(row.Cells[1].ForeColor));
                //celda.BackgroundColor = new iTextSharp.text.BaseColor(row.Cells[1].BackColor);
                celda.HorizontalAlignment = row.Cells[1].HorizontalAlign == HorizontalAlign.Right ? Element.ALIGN_RIGHT : Element.ALIGN_LEFT;
                teibol.AddCell(celda);
            }
            return teibol;
        }

        protected PdfPTable GetProveedorData()
        {
            return this.GetProveedorDetailsViewAsPdfTable(this.dvProveedorData);
        }

        protected byte [] PrintFacturaDeGanado()
        {
            byte[] bytes = null;

            MemoryStream ms = new MemoryStream();
            try
            {
                Document document = PdfCreator.inicializadocumento(Garibay.PdfCreator.tamañoPapel.CARTA, Garibay.PdfCreator.orientacionPapel.VERTICAL);
                float fMargin = (float)Utils.conviertedecmsapoints(1);
                document.SetMargins(fMargin, fMargin, fMargin, fMargin);

                PdfWriter pdfW = PdfWriter.GetInstance(document, ms);
                document.Open();
                PdfPTable Teibol = new PdfPTable(3);
                Teibol.WidthPercentage = 100;
                #region Logo
                iTextSharp.text.Image logo;
                logo = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/imagenes/LogoGaribay124x90.jpg"));
                logo.ScalePercent(80);
                PdfPCell celda = new PdfPCell(logo);
                celda.HorizontalAlignment = Element.ALIGN_CENTER;
                celda.VerticalAlignment = Element.ALIGN_MIDDLE;
                Teibol.AddCell(celda);
                #endregion

                #region Garibay data
                Teibol.AddCell(this.GetGanaderaData());
                #endregion

                #region Factura Data
                Teibol.AddCell(this.GetFacturaData());
                #endregion

                document.Add(Teibol);

                Teibol = this.GetProveedorData();   
                Teibol.HorizontalAlignment = Element.ALIGN_LEFT;
                Teibol.WidthPercentage = 50;
                Teibol.SetWidths(new float[2] { 20, 80 });
                document.Add(Teibol);
                document.Add(new Paragraph(" "));
                this.gvGanado.Columns[0].Visible = false;
                this.gvGanado.Columns[1].Visible = false;

                document.Add(PdfCreator.GridViewToPdfPTable(ref this.gvGanado, 100, null, 0.0f));
                document.Add(PdfCreator.GridViewToPdfPTable(ref this.gvPesosMerma));
                document.Add(new Paragraph(" "));
                
                Teibol = new PdfPTable( new float[2] {73,27});
                Teibol.WidthPercentage = 100;
                this.gvPagosFactura.Columns[6].Visible = false;
                if (this.gvPagosFactura.Rows.Count > 0)
                {
                    Teibol.AddCell(new PdfPCell(PdfCreator.GridViewToPdfPTable(ref this.gvPagosFactura, 100, null, 0.0f)));
                }
                else
                {
                    Teibol.AddCell(new PdfPCell());
                }
                celda = new PdfPCell();
                celda.AddElement(PdfCreator.GridViewToPdfPTable(ref this.gvConcentrado, 100, null, -1.5f));
                celda.AddElement(this.GetDetailsViewAsPdfTable(this.dvTotales));
                celda.VerticalAlignment = Element.ALIGN_TOP;
                celda.HorizontalAlignment = Element.ALIGN_RIGHT;
                Teibol.AddCell(celda);
                document.Add(Teibol);


                document.Close();
                bytes = ms.GetBuffer();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.PRINT, "Printing FacturadeGanado", ref ex);
            }
            
            return bytes;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!this.IsPostBack)
            {
                this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
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
                    this.gvConcentrado.DataBind();
                    this.gvGanado.DataBind();
                    this.dvProveedorData.DataBind();
                    this.dvTotales.DataBind();
                    byte[] printingbytes = this.PrintFacturaDeGanado();
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment; filename=LiquidacionDeGanado.pdf");
                    Response.ContentType = "application/pdf";
                    Response.Buffer = Response.BufferOutput = false;
                    Response.ContentType = "application/pdf";
                    Response.BinaryWrite(printingbytes);
                    Response.Flush();
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

                
            }
            this.pnlNewPago.Visible = false;
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
                    this.txtFecha.Text = DateTime.Parse(r["fecha"].ToString()).ToString("dd/MM/yyyy");
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
//                     sRedirect = "frmFacturaGanadoPrint.aspx";
//                     sRedirect += Utils.GetEncriptedQueryString("FacturaID=" + newID.ToString());
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
                    commFactura.CommandText = "INSERT INTO PagosFacturadeGanado (FacturadeGanadoID, movbanID) VALUES (@FacturaID,@movbanID) ";
                    //(@FacturaCVID,@fecha,@movbanID,@movCajaID,@userID)
                    commFactura.Parameters.Add("@FacturaID", SqlDbType.Int).Value = int.Parse(this.txtFolio.Text);
                    commFactura.Parameters.Add("@movbanID", SqlDbType.Int).Value = dtRowainsertar.movBanID;
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
                cmdDelete.CommandText = "delete from PagosFacturaDeGanado where pagoID = @pagoID";
                cmdDelete.Parameters.Add("@pagoID", SqlDbType.Int).Value = (int)(this.gvPagosFactura.DataKeys[e.RowIndex]["pagoID"]);
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
        }

        protected void gvPagosFactura_DataBound(object sender, EventArgs e)
        {
            this.dvTotales.DataBind();
        }

                

    }
}
