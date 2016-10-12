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
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
namespace Garibay
{
    public partial class frmEstadodeCuentaCredito : Garibay.BasePage
    {
        private byte[] printingbytes;
        public byte[] DownloadBytes
        {
            get { return this.printingbytes; }
        }
        public frmEstadodeCuentaCredito()
        {
        }

        protected void UpdateBtnPrint()
        {
            String sQuery = "printing=1&creditoID=" + this.ddlCredito.SelectedValue;
            sQuery += "&FechaCorte=" + this.txtFechaQuery.Text;
            sQuery += "&cicloID=" + this.ddlCiclos.SelectedValue.ToString();
            sQuery = Utils.GetEncriptedQueryString(sQuery);
            String strRedirect = "frmEstadodeCuentaCreditoPrint.aspx";
            strRedirect += sQuery;
//             this.Session["DOWNURL"] = strRedirect;
//             strRedirect = "frmDownLoadPage.aspx" + Utils.GetEncriptedQueryString("printing=1");
            JSUtils.OpenNewWindowOnClick(ref this.btnPrint, strRedirect, "Print Estado de Cuenta", true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.LoadEncryptedQueryString() > 1 && this.myQueryStrings["printing"] != null && this.myQueryStrings["printing"].ToString() == "1")
                {
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment; filename=EstadoDeCuenta.pdf");
                    Response.ContentType = "application/pdf";
                    Response.Buffer = Response.BufferOutput = false;
                }
                /*
                else
                                {
                                    Response.Buffer = Response.BufferOutput = true;
                                }*/
                
                if (this.IsSistemBanco)
                {
                    this.SqlCreditos.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.SqlCreditos.SelectCommand);
                    this.sdsEstadoGeneral.SelectCommand = "ReturnEstadodeCuentaTotales_SisBancos";
                    this.sdsEstadodeCuenta.SelectCommand = "ReturnEstadodeCuenta_SisBancos";
                    this.sdsFertilizantes.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.sdsFertilizantes.SelectCommand);
                    this.sdsNOFertilizante.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.sdsNOFertilizante.SelectCommand);
                    this.SDSSEGURO.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.SDSSEGURO.SelectCommand);
                    this.sdsNotasSinInteres.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.sdsNotasSinInteres.SelectCommand);
                   
                }
                if(!this.IsPostBack)
                {
                   this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
                   this.txtFechaQuery.Text = DateTime.Parse(this.txtFecha.Text).ToString("yyyy-MM-dd 23:59:59");
                   this.UpdateBtnPrint();
                   this.ddlCiclos.DataBind();
                   if (this.ddlCiclos.Items.Count > 0)
                   {
                       this.ddlCiclos.SelectedIndex = 0;
                   }
                   this.GridViewPagos.DataBind();
                     
                   this.ddlCredito.DataBind();
                   this.cargaDatosProductorYCredito();
                   this.gridEstadodeCuenta.DataBind();
                   this.dvEstadoGeneral.DataBind();
                   if (this.myQueryStrings != null && this.myQueryStrings["creditoID"] != null)
                   {
                       this.ddlCredito.SelectedValue = this.myQueryStrings["creditoID"].ToString();
                       this.cargaDatosProductorYCredito();
                       if (this.myQueryStrings["printing"] != null && this.myQueryStrings["printing"].ToString() == "1")
                       {
                           this.dvEstadoGeneral.DataBind();
                           this.grdvProNotasVenta.DataBind();
                           this.gvSeguro.DataBind();
                           this.gridEstadodeCuenta.DataBind();
                           this.printingbytes = this.PrintEstadoDeCuenta(this.myQueryStrings["creditoID"].ToString(),
                               ref this.dvEstadoGeneral,
                               ref this.grdvProNotasVenta,
                               ref this.gvSeguro,
                               ref this.gridEstadodeCuenta,
                               int.Parse(this.myQueryStrings["creditoID"].ToString()));
                           Logger.Instance.LogMessage(Logger.typeLogMessage.DEBUG,
                               Logger.typeUserActions.PRINT,
                               this.UserID,
                               "bytes count" + printingbytes != null ? printingbytes.Length.ToString() : "0",
                               this.Request.Url.ToString()
                               );
                           if (printingbytes != null)
                           {
                               //Response.Clear();
                               Response.ContentType = "application/pdf";
                               this.Server.Transfer("frmDownLoadPage.aspx" + Utils.GetEncriptedQueryString("printing=1"));
                               //Response.BinaryWrite(printingbytes);
                               //Response.Flush();
                           }
                       }
                   }
                   this.UpdateBtnPrint();
                }
                this.txtFechaQuery.Text = DateTime.Parse(this.txtFecha.Text).ToString("yyyy-MM-dd 23:59:59");
                this.UpdateBtnPrint();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.PRINT, "printing estado de cuenta", ref ex);
            }
            this.lblIndeminzacionResult.Visible = false;
        }
       
       
        protected void gridEstadodeCuenta_DataBound(object sender, EventArgs e)
        {
            Utils.MergeSameRowsInGVPerColumn(ref this.gridEstadodeCuenta,0);
            double montocredito = 0, interes = 0, debe = 0, abono = 0, tasainteres = 0;
            int dias = 0;
             foreach (GridViewRow row in this.gridEstadodeCuenta.Rows)
               {
                   if (row.RowType == DataControlRowType.DataRow)
                   {
                       double.TryParse(row.Cells[3].Text,NumberStyles.Currency, null, out montocredito);
                       if (montocredito == 0) row.Cells[3].Text = "";
                       
//                        int.TryParse(row.Cells[5].Text,NumberStyles.Currency, null, out dias);
//                        if (dias == 0) row.Cells[5].Text = "";

                       double.TryParse(row.Cells[7].Text, out tasainteres);
                       if (tasainteres == 0) row.Cells[7].Text = "";
                       else row.Cells[7].Text = string.Format("{0:P2}",tasainteres);
                       
//                        double.TryParse(row.Cells[7].Text,NumberStyles.Currency, null, out interes);
//                        if (interes == 0) row.Cells[7].Text = "";
                       
                       double.TryParse(row.Cells[8].Text,NumberStyles.Currency, null, out debe);
                       if (debe == 0) row.Cells[8].Text = "";
                       
                       double.TryParse(row.Cells[9].Text,NumberStyles.Currency, null, out abono);
                       if (abono == 0) row.Cells[9].Text = "";

                       if (row.Cells[2].Text.IndexOf("CORTE") >= 0)
                       {
                           Utils.MergeColumnsPerRow(ref this.gridEstadodeCuenta, row.RowIndex, 2, 6);
                           row.Cells[2].Font.Bold = true;
                           row.Cells[2].Font.Size =  16;
                           row.Cells[12].Font.Bold = true;
                           row.Cells[12].Font.Size = 16;
                           row.Cells[11].Font.Bold = true;
                           row.Cells[11].Font.Size = 16;
                           double monto = Utils.GetSafeFloat(row.Cells[4].Text);
                           if(monto < 0 )
                               row.Cells[10].ForeColor = System.Drawing.Color.Red;

                       }

                   }
               }
        }

        protected void btnAddPago_Click(object sender, EventArgs e)
        {
            String sNewUrl = "~/frmPagosCreditosAdd.aspx?data=";
            sNewUrl += Utils.encriptacadena("creditoID=" + this.ddlCredito.Text);
            Response.Redirect(sNewUrl);
        }

        protected void gridProductos_DataBound(object sender, EventArgs e)
        {
//             if(this.gridProductos.Rows.Count>0)
//             {
//                 DateTime fecha = (DateTime)(this.gridProductos.DataKeys[0]["Fecha"]);
//                 string sql = " SELECT     SegurosAgricolas.Nombre, Solicitudes.CostoTotalSeguro " +
//                              " FROM         Solicitudes INNER JOIN " +
//                              " Creditos ON Solicitudes.creditoID = Creditos.creditoID INNER JOIN " +
//                              " solicitud_SeguroAgricola ON Solicitudes.solicitudID = solicitud_SeguroAgricola.solicitudID INNER JOIN " +
//                              " SegurosAgricolas ON solicitud_SeguroAgricola.seguroID = SegurosAgricolas.seguroID ";
//                 SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
//                 SqlCommand cmdGaribay = new SqlCommand(sql, conGaribay);
//                 GridView row = this.gridProductos,
//             }
            
        }

        protected void ddlCredito_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cargaDatosProductorYCredito();
            this.UpdateBtnPrint();
        }

        protected void EnablePagadoStatus(bool isCreditoPagado)
        {
            this.pnlCreditoPagado.Visible = isCreditoPagado;
            this.txtFecha.Enabled = !isCreditoPagado;
            this.TextBoxFechaUltimoCalculo.Enabled = !isCreditoPagado;
            this.CheckBoxMostrarDescuentos.Enabled = !isCreditoPagado;
            this.btnAddPago.Visible = !isCreditoPagado;
        }
        protected void cargaDatosProductorYCredito()
        {
            if (this.ddlCredito.SelectedValue == null
                || this.ddlCredito.SelectedValue.ToString().Length == 0)
            {
                return;
            }
            string sql = "SELECT     creditos.creditoId, Productores.poblacion + ' ' + Productores.municipio + ' ' + Estados.estado AS Poblacion, Productores.telefono, Productores.domicilio, " +
                         " Solicitudes.Descripciondegarantias, ConceptoSoporteGarantia, creditos.descuentosDeInteres, creditos.descuentosDescripcion, creditos.finCalculoIntereses, creditos.pagado, creditos.indemnizacion, creditos.ImprimeGarantias " +
                         " FROM        Productores INNER JOIN " +
                         " Creditos ON Productores.productorID = Creditos.productorID INNER JOIN  " +
                         " Estados ON Productores.estadoID = Estados.estadoID LEFT JOIN " +
                         " Solicitudes ON Creditos.creditoID = Solicitudes.creditoID  where Creditos.creditoID = @creditoID ";
            if(this.IsSistemBanco)
            {
                sql = dbFunctions.UpdateSDSForSisBanco(sql);
            }
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdSacadata = new SqlCommand(sql,conGaribay);
            try
            {
                conGaribay.Open();
                cmdSacadata.Parameters.Add("@creditoID", SqlDbType.Int).Value = int.Parse(this.ddlCredito.SelectedValue);
                SqlDataReader reader = cmdSacadata.ExecuteReader();
                if (reader.Read())
                {
                    this.TextBoxDescripcion.Text = reader["descuentosDescripcion"] != null ? reader["descuentosDescripcion"].ToString() : string.Empty;
                    this.TextBoxMonto.Text = reader["descuentosDeInteres"] != null ? reader["descuentosDeInteres"].ToString() : string.Empty;
                    //this.txtIndemnizacionSeguro.Text = reader["indemnizacion"].ToString();
                    this.txtPoblacion.Text = reader["Poblacion"] != null ? reader["Poblacion"].ToString() : string.Empty;
                    this.txtTelefono.Text = reader["telefono"] != null ? reader["telefono"].ToString() : string.Empty;
                    this.txtDireccion.Text = reader["domicilio"] != null ? reader["domicilio"].ToString() : string.Empty;
                    
                    StringBuilder garantias = new StringBuilder();
                    if(reader["Descripciondegarantias"] != null && !string.IsNullOrEmpty(reader["Descripciondegarantias"].ToString()))
                    {
                        garantias.Append(reader["Descripciondegarantias"].ToString());
                    }
                    if(reader["ConceptoSoporteGarantia"] != null && !string.IsNullOrEmpty(reader["ConceptoSoporteGarantia"].ToString()))
                    {
                        garantias.Append("\r\n");
                        garantias.Append(reader["ConceptoSoporteGarantia"].ToString());
                    }
                    this.txtGarantias.Text = garantias.ToString();
                    this.TextBoxFechaUltimoCalculo.Text = reader["finCalculoIntereses"] != null ? Utils.converttoshortFormatfromdbFormat(reader["finCalculoIntereses"].ToString()) : string.Empty;
                    this.CheckBoxCreditoPagado.Checked = bool.Parse(reader["pagado"].ToString());
                    this.CheckBoxEntregaDeGarantias.Checked = bool.Parse(reader["ImprimeGarantias"].ToString());
                    this.EnablePagadoStatus(this.CheckBoxCreditoPagado.Checked);

                }
            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "ERROR AL SACAR DATOS DEL PRODUCTOR EN ESTADO DE CUENTA", ref ex);
            }
            finally
            {
                conGaribay.Close();
            }
        }

        protected void ddlCiclos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlCredito.DataBind();
        }

        public Byte[] PrintEstadoDeCuenta(String sCredito, ref DetailsView dvEdoGral, ref GridView gvDetalle, ref GridView gvSeguro, ref GridView gvIntereses, int sCreditoID)
        {
            Byte[] res = null;
            MemoryStream ms = new MemoryStream();
            Document document = PdfCreator.inicializadocumento(Garibay.PdfCreator.tamañoPapel.CARTA, Garibay.PdfCreator.orientacionPapel.VERTICAL);
            float fMargin = (float)Utils.conviertedecmsapoints(1);
            document.SetMargins(fMargin, fMargin, fMargin, fMargin);
            try
            {
                PdfWriter pdfW = PdfWriter.GetInstance(document, ms);
                document.Open();
                BaseFont fnt;
                fnt = FontFactory.GetFont(iTextSharp.text.FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL).BaseFont;

                document.Add(new Paragraph(" "));

                PdfPTable teibol = new PdfPTable(2/*new float[2] { 0.5f, 0.5f }*/);
                teibol.WidthPercentage = 100;
                teibol.HorizontalAlignment = Element.ALIGN_CENTER;
                string sProductor = this.ddlCredito.SelectedItem.Text;
                PdfPCell celda = PdfCreator.getCellWithFormat("ESTADO DE CUENTA DE CREDITO: " + sProductor, true, Element.ALIGN_CENTER, 12.0f);
                celda.Colspan = 2;
                teibol.AddCell(celda);
                celda = new PdfPCell(PdfCreator.GridViewToPdfPTable(ref this.gvCreditoData, 100, null, -1));
                celda.Colspan = 2;
                teibol.AddCell(celda);

                String sGarantias = "NO DEJÓ GARANTIAS";
                #region Productor Data
                string sql = "SELECT     Solicitudes.Descripciondegarantias FROM         Productores INNER JOIN "
                + " Creditos ON Productores.productorID = Creditos.productorID INNER JOIN "
                + " Estados ON Productores.estadoID = Estados.estadoID LEFT OUTER JOIN "
                + " Solicitudes ON Creditos.creditoID = Solicitudes.creditoID where Creditos.creditoID = @creditoID ";
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdSacadata = new SqlCommand(sql, conGaribay);
                try
                {
                    conGaribay.Open();
                    cmdSacadata.Parameters.Add("@creditoID", SqlDbType.Int).Value = sCreditoID;
                    SqlDataReader reader = cmdSacadata.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader["Descripciondegarantias"] != null && reader["Descripciondegarantias"].ToString().Length > 0)
                        {
                            sGarantias = reader["Descripciondegarantias"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "ERROR AL SACAR DATOS DEL PRODUCTOR EN ESTADO DE CUENTA", ref ex);
                }
                finally
                {
                    conGaribay.Close();
                }


                #endregion
                PdfPTable teibolGarantiasYTotales = new PdfPTable(2);
                PdfPTable teibolGarantias = new PdfPTable(1);
                celda = new PdfPCell();
                teibolGarantias.AddCell(PdfCreator.getCellWithFormat("GARANTIAS:", true, Element.ALIGN_LEFT, 7f));
                teibolGarantias.AddCell(PdfCreator.getCellWithFormat(sGarantias, false, Element.ALIGN_JUSTIFIED, 6f));
                teibolGarantias.HorizontalAlignment = Element.ALIGN_LEFT;
                teibolGarantias.WidthPercentage = 100;
                celda.AddElement(teibolGarantias);
                celda.HorizontalAlignment = Element.ALIGN_LEFT;
                if (this.gvSeguro.Rows.Count > 0)
                {
                    celda.AddElement(PdfCreator.GridViewToPdfPTable(ref gvSeguro, 100, new float[3] { 0.4f, 0.4f, 0.2f }, -2.5f));
                }
                else
                {
                    PdfPTable tableSeguro = new PdfPTable(1);
                    tableSeguro.AddCell(PdfCreator.getCellWithFormat("NO CONTRATO SEGURO", true, Element.ALIGN_LEFT, 7f));
                    tableSeguro.HorizontalAlignment = Element.ALIGN_LEFT;
                    tableSeguro.WidthPercentage = 100;
                    celda.AddElement(tableSeguro);
                }

                if (this.grdvProNotasVenta.Rows.Count > 0)
                {
                    celda.AddElement(PdfCreator.GridViewToPdfPTable(ref this.grdvProNotasVenta,100, null, -2.5f));
                }

                if (this.grdvProNotasVenta0.Rows.Count > 0)
                {
                    celda.AddElement(PdfCreator.GridViewToPdfPTable(ref this.grdvProNotasVenta0, 100, null, -2.5f));
                }
                //teibol.AddCell(celda);
                teibol.AddCell(celda);

                PdfPTable teibolTotales = new PdfPTable(new float[2] { 0.3f, 0.7f });
                for (int i = 1; i < dvEdoGral.Rows.Count; i++)
                {
                    teibolTotales.AddCell(PdfCreator.getCellWithFormat(dvEdoGral.Rows[i].Cells[0].Text, true, Element.ALIGN_LEFT));
                    teibolTotales.AddCell(PdfCreator.getCellWithFormat(dvEdoGral.Rows[i].Cells[1].Text, false, Element.ALIGN_RIGHT));
                }
                celda = new PdfPCell();
                celda.HorizontalAlignment = Element.ALIGN_RIGHT;
                celda.VerticalAlignment = Element.ALIGN_TOP;
                celda.AddElement(teibolTotales);

                teibol.AddCell(celda);

                document.Add(teibol);
                int cols = gvIntereses.Columns.Count;
                document.Add(PdfCreator.GridViewToPdfPTable(ref gvIntereses, 100, new float[12] { 0.7f, 0.6f, 1.3f, 0.9f, 0.8f, 0.4f, 0.8f, 0.8f, 0.8f, 0.8f, 1.1f, 0 }, -2.0f, true));

                document.Close();

                res = ms.GetBuffer();

            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.PRINT, "error printing estado de cuenta", ref ex);
            }
            finally
            {

            }
            return res;
        }

        protected void gridViewNotasSinInteres_DataBound(object sender, EventArgs e)
        {
            double total = 0;
            foreach(GridViewRow row in this.gridViewNotasSinInteres.Rows)
            {
                if(row.RowType == DataControlRowType.DataRow)
                    total += double.Parse(row.Cells[3].Text, NumberStyles.Currency);
            }
            if(this.gridViewNotasSinInteres.FooterRow!=null)
                this.gridViewNotasSinInteres.FooterRow.Cells[3].Text = string.Format("{0:c2}", total);
        }

        protected void ButtonModificar_Click(object sender, EventArgs e)
        {
            SqlConnection conInsertaDescuentos = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdInsertaDescuentos = new SqlCommand();
            cmdInsertaDescuentos.Connection = conInsertaDescuentos;
            cmdInsertaDescuentos.CommandText = "UPDATE Creditos set descuentosDeInteres = @descuentosDeInteres, descuentosDescripcion = @descuentosDescripcion where creditoID = @creditoId";
            conInsertaDescuentos.Open();
            try
            {
                cmdInsertaDescuentos.Parameters.Add("@descuentosDeInteres", SqlDbType.Float).Value=  Utils.GetSafeFloat(this.TextBoxMonto.Text);
                cmdInsertaDescuentos.Parameters.Add("@descuentosDescripcion",SqlDbType.Text).Value =  this.TextBoxDescripcion.Text;
                //cmdInsertaDescuentos.Parameters.Add("@indemnizacion", SqlDbType.Float).Value = Utils.GetSafeFloat(this.txtIndemnizacionSeguro.Text);
                cmdInsertaDescuentos.Parameters.Add("@creditoID",SqlDbType.Int).Value = int.Parse(this.ddlCredito.SelectedValue);
                cmdInsertaDescuentos.ExecuteNonQuery();
                this.dvEstadoGeneral.DataBind();
                this.CheckBoxMostrarDescuentos.Checked = false;
                this.pnlShowDescuentos_CollapsiblePanelExtender.Collapsed = true;
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "MODIFICANDO DESCUENTO DE INTERES", ref ex);
            	
            }
        }

        protected void PopCalendar4_SelectionChanged(object sender, EventArgs e)
        {
            if(ddlCredito.SelectedItem != null)
            {
                SqlConnection conUpdateFechaFinInteres = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdUpdateFecha = new SqlCommand();
                try
                {
                    conUpdateFechaFinInteres.Open();
                    cmdUpdateFecha.Connection = conUpdateFechaFinInteres;
                    cmdUpdateFecha.CommandText = dbFunctions.UpdateSDSForSisBanco("Update creditos set  finCalculoIntereses = @finCalculoIntereses where creditoId = @creditoId");
                    cmdUpdateFecha.Parameters.Add("@creditoId", SqlDbType.Int).Value = int.Parse(this.ddlCredito.SelectedValue);
                    cmdUpdateFecha.Parameters.Add("@finCalculoIntereses", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(this.TextBoxFechaUltimoCalculo.Text);
                    cmdUpdateFecha.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "ERROR AL MODIFICAR FECHA CALCULO FIN DE INTERES", ref ex);
                }
                finally
                {
                    conUpdateFechaFinInteres.Close();
                }

            }
             
        }

        protected void CheckBoxCreditoPagado_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlCredito.SelectedItem != null)
            {
                SqlConnection conUpdateFechaFinInteres = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdUpdateCreditoYNotas = new SqlCommand();
                try
                {
                    conUpdateFechaFinInteres.Open();
                    cmdUpdateCreditoYNotas.Connection = conUpdateFechaFinInteres;
                    cmdUpdateCreditoYNotas.CommandText = dbFunctions.UpdateSDSForSisBanco("Update creditos set  pagado = @pagado where creditoId = @creditoId");
                    cmdUpdateCreditoYNotas.Parameters.Add("@creditoId", SqlDbType.Int).Value = int.Parse(this.ddlCredito.SelectedValue);
                    cmdUpdateCreditoYNotas.Parameters.Add("@pagado", SqlDbType.Bit).Value = this.CheckBoxCreditoPagado.Checked;
                    cmdUpdateCreditoYNotas.ExecuteNonQuery();
                    cmdUpdateCreditoYNotas.CommandText = dbFunctions.UpdateSDSForSisBanco("Update notasdeventa set pagada = @pagado where creditoId = @creditoId");
                    cmdUpdateCreditoYNotas.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "ERROR AL MODIFICAR EL CREDITO COMO PAGADO", ref ex);
                }
                finally
                {
                    conUpdateFechaFinInteres.Close();
                }

            }
            this.cargaDatosProductorYCredito();
        }

        protected void dvIndemnizacion_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            try
            {
                e.Values["creditoID"] = this.ddlCredito.SelectedValue;
                if(e.Values["fecha"] == null)
                {
                    e.Values["fecha"] = Utils.Now;
                }
                else
                {
                    e.Values["fecha"] = DateTime.Parse(e.Values["fecha"].ToString());
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "agregando indemnizacion", ex);
            }
        }

        protected void dvIndemnizacion_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            string sResult = string.Empty;
            if(e.Exception != null)
            {
                e.ExceptionHandled = true;
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "inserting indemnizacion", e.Exception);
                sResult = "ERROR INSERTANDO LA INDEMNIZACION, REVISE LOS DATOS E INTENTELO DE NUEVO <BR />";
                sResult += "DESCRIPCION DEL ERROR: " + e.Exception.ToString();
                e.KeepInInsertMode = true;
            }
            if (e.AffectedRows == 1)
            {
                sResult = "INDEMNIZACION AGREGADA EXITOSAMENTE.";
                e.KeepInInsertMode = false;
            }
            this.lblIndeminzacionResult.Visible = true;
            this.lblIndeminzacionResult.Text = sResult;
            this.gvIndemnizaciones.DataBind();
            this.gvCreditoData.DataBind();
            this.dvEstadoGeneral.DataBind();
        }

        protected void gvIndemnizaciones_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            string sResult = string.Empty;
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "updating indemnizacion", e.Exception);
                sResult = "ERROR ACTUALIZANDO LA INDEMNIZACION, REVISE LOS DATOS E INTENTELO DE NUEVO <BR />";
                sResult += "DESCRIPCION DEL ERROR: " + e.Exception.ToString();
            }
            if (e.AffectedRows == 1)
            {
                sResult = "INDEMNIZACION ACTUALIZADA EXITOSAMENTE.";
            }
            this.lblIndeminzacionResult.Visible = true;
            this.lblIndeminzacionResult.Text = sResult;
            this.gvIndemnizaciones.DataBind();
            this.gvCreditoData.DataBind();
            this.dvEstadoGeneral.DataBind();
        }

        protected void gvIndemnizaciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                e.NewValues["creditoID"] = this.ddlCredito.SelectedValue;
                if (e.NewValues["fecha"] == null)
                {
                    e.NewValues["fecha"] = Utils.Now;
                }
                else
                {
                    e.NewValues["fecha"] = DateTime.Parse(e.NewValues["fecha"].ToString());
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "updating indemnizacion", ex);
            }
        }

        protected void gvIndemnizaciones_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
        }

        protected void gvIndemnizaciones_DataBound(object sender, EventArgs e)
        {
            //this.gvIndemnizaciones.DataBind();
            this.gvCreditoData.DataBind();
            this.dvEstadoGeneral.DataBind();
        }

        protected void CheckBoxEntregaDeGarantias_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlCredito.SelectedItem != null)
            {
                SqlConnection conUpdate = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdUpdate = new SqlCommand();
                try
                {
                    conUpdate.Open();
                    cmdUpdate.Connection = conUpdate;
                    cmdUpdate.CommandText = dbFunctions.UpdateSDSForSisBanco("Update creditos set  ImprimeGarantias = @ImprimeGarantias where creditoId = @creditoId");
                    cmdUpdate.Parameters.Add("@creditoId", SqlDbType.Int).Value = int.Parse(this.ddlCredito.SelectedValue);
                    cmdUpdate.Parameters.Add("@ImprimeGarantias", SqlDbType.Bit).Value = this.CheckBoxEntregaDeGarantias.Checked;
                    cmdUpdate.ExecuteNonQuery();
                  
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "ERROR AL MODIFICAR EL CREDITO PARA IMPRIMIR ENTREGA DE GARANTIAS", ref ex);
                }
                finally
                {
                    conUpdate.Close();
                }

            }
            this.cargaDatosProductorYCredito();

        }


    }
}
