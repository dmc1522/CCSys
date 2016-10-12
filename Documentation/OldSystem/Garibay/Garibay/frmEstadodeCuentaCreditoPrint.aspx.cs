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
    public partial class frmEstadodeCuentaCreditoPrint : Garibay.BasePage
    {
        private byte[] printingbytes;
        public byte[] DownloadBytes
        {
            get { return this.printingbytes; }
        }
        public frmEstadodeCuentaCreditoPrint()
        {
        }

        protected void UpdateBtnPrint()
        {
            String sQuery = "printing=1&creditoID=" + this.ddlCredito.SelectedValue;
            sQuery = Utils.GetEncriptedQueryString(sQuery);
            String strRedirect = "frmEstadodeCuentaCredito.aspx";
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
                    if (this.myQueryStrings["FechaCorte"] != null)
                    {
                        this.txtFechaQuery.Text = this.txtFecha.Text = this.myQueryStrings["FechaCorte"].ToString();
                    }
                    if (this.myQueryStrings["cicloID"] != null)
                    {
                        this.ddlCiclos.DataBind();
                        this.ddlCiclos.SelectedValue = this.myQueryStrings["cicloID"].ToString();
                    }
                }
                else
                {
                    Response.Buffer = Response.BufferOutput = true;
                }
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
                    this.txtFechaQuery.Text = this.txtFecha.Text = Utils.Now.ToString("yyyy-MM-dd 23:59:59");
                   if (this.myQueryStrings["FechaCorte"] != null)
                   {
                       this.txtFechaQuery.Text = this.txtFecha.Text = this.myQueryStrings["FechaCorte"].ToString();
                   }
                   this.ddlCiclos.DataBind();
                   if (this.ddlCiclos.Items.Count > 0)
                   {
                       this.ddlCiclos.SelectedIndex = 0;
                   }
                   if (this.myQueryStrings["cicloID"] != null)
                   {
                       this.ddlCiclos.DataBind();
                       this.ddlCiclos.SelectedValue = this.myQueryStrings["cicloID"].ToString();
                   }
                     
                   this.ddlCredito.DataBind();
                   this.cargaDatosProductor();
                   this.gridEstadodeCuenta.DataBind();
                   this.dvEstadoGeneral.DataBind();
                   if (this.myQueryStrings != null && this.myQueryStrings["creditoID"] != null)
                   {
                       this.ddlCredito.SelectedValue = this.myQueryStrings["creditoID"].ToString();
                       this.cargaDatosProductor();
                       if (this.myQueryStrings["FechaCorte"] != null)
                       {
                           this.txtFechaQuery.Text = this.txtFecha.Text = this.myQueryStrings["FechaCorte"].ToString();
                       }
                       if (this.myQueryStrings["printing"] != null && this.myQueryStrings["printing"].ToString() == "1")
                       {
                           this.dvEstadoGeneral.DataBind();
                           this.grdvProNotasVenta.DataBind();
                           this.gvSeguro.DataBind();
                           this.gridEstadodeCuenta.DataBind();
                           this.GridViewPagos.DataBind();
                           this.printingbytes = this.PrintEstadoDeCuenta(this.myQueryStrings["creditoID"].ToString(),
                               ref this.dvEstadoGeneral,
                               ref this.grdvProNotasVenta,
                               ref this.gvSeguro,
                               ref this.gridEstadodeCuenta,
                               ref this.GridViewPagos,
                               int.Parse(this.myQueryStrings["creditoID"].ToString()));
                           Logger.Instance.LogMessage(Logger.typeLogMessage.DEBUG,
                               Logger.typeUserActions.PRINT,
                               this.UserID,
                               "bytes count" + printingbytes != null ? printingbytes.Length.ToString() : "0",
                               this.Request.Url.ToString()
                               );
                           if (printingbytes != null)
                           {
                               Response.Clear();
                               Response.ContentType = "application/pdf";
                               Response.BinaryWrite(printingbytes);
                               Response.Flush();
                           }
                       }
                   }
                   this.UpdateBtnPrint();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.PRINT, "printing estado de cuenta", ref ex);
            }
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
                       
                       double.TryParse(row.Cells[7].Text,NumberStyles.Currency, null, out interes);
                       if (interes == 0) row.Cells[7].Text = "";
                       
                       double.TryParse(row.Cells[8].Text,NumberStyles.Currency, null, out debe);
                       if (debe == 0) row.Cells[8].Text = "";
                       
                       double.TryParse(row.Cells[9].Text,NumberStyles.Currency, null, out abono);
                       if (abono == 0) row.Cells[9].Text = "";

                       if (row.Cells[2].Text.IndexOf("CORTE") >= 0)
                       {
                           Utils.MergeColumnsPerRow(ref this.gridEstadodeCuenta, row.RowIndex, 2, 10);
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
            this.cargaDatosProductor();
            this.UpdateBtnPrint();
        }
        protected void cargaDatosProductor()
        {
            string sql = "SELECT     creditos.creditoId, Productores.poblacion + ' ' + Productores.municipio + ' ' + Estados.estado AS Poblacion, Productores.telefono, Productores.domicilio, " +
                         " Solicitudes.Descripciondegarantias, ConceptoSoporteGarantia " +
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

        public Byte[] PrintEstadoDeCuenta(String sCredito, ref DetailsView dvEdoGral, ref GridView gvDetalle, ref GridView gvSeguro, ref GridView gvIntereses, ref GridView gridViewPagos, int sCreditoID)
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
                bool imprimeGarantias = false;
                #region Productor Data
                string sql = "SELECT     Solicitudes.Descripciondegarantias, Creditos.ImprimeGarantias FROM         Productores INNER JOIN "
                + " Creditos ON Productores.productorID = Creditos.productorID INNER JOIN "
                + " Estados ON Productores.estadoID = Estados.estadoID LEFT OUTER JOIN "
                + " Solicitudes ON Creditos.creditoID = Solicitudes.creditoID where Creditos.creditoID = @creditoID ";
                sql = dbFunctions.UpdateSDSForSisBanco(sql);
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
                        if (reader["ImprimeGarantias"] != null && bool.Parse(reader["ImprimeGarantias"].ToString()))
                        {
                            imprimeGarantias = true;
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

                //teibol.AddCell(celda);
                teibol.AddCell(celda);

                PdfPTable teibolTotales = new PdfPTable(new float[2] { 0.3f, 0.7f });
                for (int i = 1; i < dvEdoGral.Rows.Count; i++)
                {
                    //Intereses (+)
                    /*
                    if (dvEdoGral.Rows[i].Cells[0].Text == "Intereses (+)")
                                        {
                                            teibolTotales.AddCell(PdfCreator.getCellWithFormat(dvEdoGral.Rows[i].Cells[0].Text, true, Element.ALIGN_LEFT));
                                            double dIntereses = 0;
                                            dIntereses = Utils.GetSafeFloat(dvEdoGral.Rows[i].Cells[1].Text);
                                            dIntereses -= Utils.GetSafeFloat(dvEdoGral.Rows[i++].Cells[1].Text);
                                            teibolTotales.AddCell(PdfCreator.getCellWithFormat(string.Format("{0:C2}", dIntereses), false, Element.ALIGN_RIGHT));
                                        }
                                        else*/
                    
                    {
                        teibolTotales.AddCell(PdfCreator.getCellWithFormat(dvEdoGral.Rows[i].Cells[0].Text, true, Element.ALIGN_LEFT));
                        teibolTotales.AddCell(PdfCreator.getCellWithFormat(dvEdoGral.Rows[i].Cells[1].Text, false, Element.ALIGN_RIGHT));
                    }
                }
                celda = new PdfPCell();
                celda.HorizontalAlignment = Element.ALIGN_RIGHT;
                celda.VerticalAlignment = Element.ALIGN_TOP;
                celda.AddElement(teibolTotales);

                teibol.AddCell(celda);

                document.Add(teibol);
                teibol = new PdfPTable(1);
                teibol.WidthPercentage = 100;
                int cols = gvIntereses.Columns.Count;
                celda = PdfCreator.getCellWithFormat("Notas con interes", true, Element.ALIGN_LEFT);
                celda.Border = Rectangle.BOTTOM_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                teibol.AddCell(celda);
                celda = new PdfPCell();
                celda.Border = Rectangle.BOTTOM_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                //celda.AddElement(PdfCreator.GridViewToPdfPTable(ref gvIntereses, 100, new float[14] { 0.7f, 0.6f, 1.3f, 0.9f, 0.8f, 0.4f, 0.8f, 0.8f, 0.8f, 0.8f, 1.1f, 0 }, -2.0f, true));
                celda.AddElement(PdfCreator.GridViewToPdfPTable(ref gvIntereses, 100,null, -2.0f, true));
                teibol.AddCell(celda);
                if (this.gridViewNotasSinInteres.Rows.Count > 0)
                {
                    celda = PdfCreator.getCellWithFormat("Notas sin interes", true, Element.ALIGN_LEFT);
                    celda.Border = Rectangle.BOTTOM_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                    teibol.AddCell(celda);
                    celda = new PdfPCell();
                    celda.AddElement(PdfCreator.GridViewToPdfPTable(ref this.gridViewNotasSinInteres, -1, null, -2.0f));
                    celda.Border = Rectangle.BOTTOM_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                    teibol.AddCell(celda);
                }
                if (this.GridViewPagos.Rows.Count >0)
                {
                    celda = new PdfPCell();
                    celda = PdfCreator.getCellWithFormat("Pagos", true, Element.ALIGN_LEFT);
                    celda.Border = Rectangle.BOTTOM_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                    teibol.AddCell(celda);
                    celda.Border = Rectangle.BOTTOM_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                    celda.AddElement(PdfCreator.GridViewToPdfPTable(ref gridViewPagos, 100, new float[3] { 0.25f, 0.35f, 0.4f }, -2.0f, false));
                    teibol.AddCell(celda);
                }
                
                
                document.Add(teibol);
                if (imprimeGarantias)
                {
                    teibol = new PdfPTable(1);
                    teibol.WidthPercentage = 100;
                    celda = PdfCreator.getCellWithFormat("Recibi los siguientes documentos por constancia de garantias. Descripción: ", true, Element.ALIGN_JUSTIFIED);
                    celda.Border = 0;
                    teibol.AddCell(celda);
                    celda = PdfCreator.getCellWithFormat(sGarantias, false, Element.ALIGN_JUSTIFIED);
                    celda.Border = 0;
                    teibol.AddCell(celda);
                    celda = PdfCreator.getCellWithFormat("Firma de conformidad", false, Element.ALIGN_CENTER);
                    celda.Border = 0;
                    teibol.AddCell(celda);
                    celda = PdfCreator.getCellWithFormat("_________________________________", false, Element.ALIGN_CENTER);
                    celda.Border = 0;
                    teibol.AddCell(celda);
                    celda = PdfCreator.getCellWithFormat(sProductor, false, Element.ALIGN_CENTER);
                    celda.Border = 0;
                    teibol.AddCell(celda);
                    document.Add(teibol);
                }

//                 teibol = new PdfPTable(new float[] { 0.5f, 0.5f });
//                 teibol.WidthPercentage = 100;
//                 celda = new PdfPCell();
// 
//                 if (this.grdvProNotasVenta.Rows.Count > 0)
//                 {
//                     celda.AddElement(PdfCreator.GridViewToPdfPTable(ref this.grdvProNotasVenta, 100, null, -2.5f));
//                 }
//                 teibol.AddCell(celda);
//                 celda = new PdfPCell();
//                 if (this.grdvProNotasVenta0.Rows.Count > 0)
//                 {
//                     celda.AddElement(PdfCreator.GridViewToPdfPTable(ref this.grdvProNotasVenta0, 100, null, -2.5f));
//                 }
//                 teibol.AddCell(celda);
//              document.Add(teibol);
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
            foreach (GridViewRow row in this.gridViewNotasSinInteres.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                    total += double.Parse(row.Cells[2].Text, NumberStyles.Currency);
            }
            if (this.gridViewNotasSinInteres.FooterRow != null)
                this.gridViewNotasSinInteres.FooterRow.Cells[2].Text = string.Format("{0:c2}", total);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }

    }
}
