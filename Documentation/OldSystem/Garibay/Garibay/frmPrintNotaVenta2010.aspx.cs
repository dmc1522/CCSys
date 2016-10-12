using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Garibay
{
    public partial class frmPrintNotaVenta2010 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.sdsFertilizantes.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.sdsFertilizantes.SelectCommand);
            this.sdsNOFertilizantes.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.sdsNOFertilizantes.SelectCommand);
            this.SqlDataSource4.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.SqlDataSource4.SelectCommand);
            this.SqlPagos.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.SqlPagos.SelectCommand);
            if (this.LoadEncryptedQueryString() == 1 )
            {
                this.txtNotaID.Text = myQueryStrings["notadeventaID"].ToString();
                this.gvConcentradoPagos.DataBind();
                this.grdvProNotasVentaFertilizantes.DataBind();
                this.grdvProNotasVentaInsumos.DataBind();
                this.grvPagos.DataBind();
                float[] fDetalleColSize = new float[] { 8, 9, 4, 4, 4, 4, 4 };
                float[] fPagosColSize = new float[] { 5, 1, 1, 1, 10, 6, 6, 6, 6, 0 };
                byte[] bytes = this.printNotaVenta(PdfCreator.tamañoPapel.CARTA,
                    PdfCreator.orientacionPapel.HORIZONTAL, fDetalleColSize, fPagosColSize);
                if (bytes != null)
                {
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment;filename=NotaVenta" + this.txtNotaID.Text + ".pdf");
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        private SqlDataReader GetNotaData(SqlCommand comm)
        {
            SqlDataReader dr = null;
            
            try
            {
                string query = "SELECT     Notasdeventa.notadeventaID, Notasdeventa.productorID, Notasdeventa.cicloID, Notasdeventa.userID, Notasdeventa.Folio, Notasdeventa.Fecha, "
                + " Notasdeventa.Pagada, Notasdeventa.Total, Notasdeventa.Subtotal, Notasdeventa.Iva, Notasdeventa.creditoID, Notasdeventa.Fechadepago, Notasdeventa.Interes, "
                + " Notasdeventa.Observaciones, Notasdeventa.acredito, Notasdeventa.tipocalculodeinteresID, Notasdeventa.origen, Notasdeventa.remitente, Notasdeventa.domicilio, "
                + " Notasdeventa.telefono, Notasdeventa.destino, Notasdeventa.numeropermiso, Notasdeventa.transportista, Notasdeventa.nombrechofer, Notasdeventa.tractorcamion, "
                + "  Notasdeventa.color, Notasdeventa.placas, Notasdeventa.storeTS, Notasdeventa.fechainiciobrointereses, Notasdeventa.fechafincobrointereses, "
                + " LTRIM(Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre) AS Productor, Ciclos.CicloName, Users.Nombre AS hechoxNombre, "
                + " LTRIM(Productores_1.apaterno + SPACE(1) + Productores_1.amaterno + SPACE(1) + Productores_1.nombre) AS CreditoNombre, Notasdeventa.personaautorizada "
                + " FROM         Productores AS Productores_1 INNER JOIN "
                + " Creditos ON Productores_1.productorID = Creditos.productorID RIGHT OUTER JOIN Notasdeventa INNER JOIN Productores ON Notasdeventa.productorID = Productores.productorID INNER JOIN "
                + " Ciclos ON Notasdeventa.cicloID = Ciclos.cicloID INNER JOIN Users ON Notasdeventa.userID = Users.userID ON Creditos.creditoID = Notasdeventa.creditoID ";
                query += " WHERE     (Notasdeventa.notadeventaID = @notadeventaID) ";
                query = dbFunctions.UpdateSDSForSisBanco(query);
                comm.CommandText = query;
                comm.Parameters.Add("@notadeventaID", SqlDbType.Int).Value = this.txtNotaID.Text;
                dr = dbFunctions.ExecuteReader(comm);
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "getting nota data", ref ex);
            }
            return dr;
        }

        public byte [] printNotaVenta(Garibay.PdfCreator.tamañoPapel tamanopapel, Garibay.PdfCreator.orientacionPapel orientacionpapel, float[] fDetalleColSize, float[] fPago)
        {
            byte[] bytes = null;
            //String pathArchivo = Path.GetTempFileName();
            Document document = PdfCreator.inicializadocumento(tamanopapel, orientacionpapel);
            float fMarginsPoints = (float)Utils.conviertedecmsapoints(1);
            document.SetMargins(fMarginsPoints, fMarginsPoints, fMarginsPoints, fMarginsPoints);

            float fAjusteLetra = 5.0f;
            //String sOutputPath = Path.GetTempFileName();  //HttpContext.Current.Server.MapPath("test.pdf"); //
            int iBorder = 0;
            using(MemoryStream ms = new MemoryStream())
            {
                SqlDataReader drNota = null;
                SqlCommand comm = new SqlCommand();
                try
                {
                    #region Datos Nota Venta
                    
                    drNota = this.GetNotaData(comm);
                    if (drNota == null)
                    {
                        //note: do not do this, i did it because it is a quick fix.
                        return null;
                    }
                    drNota.Read();
                    #endregion

                    PdfWriter pdfW = PdfWriter.GetInstance(document, ms);
                    document.Open();

                    PdfPTable teibol = new PdfPTable(new float[] { 1.5f, 6f, 2.5f });
                    teibol.WidthPercentage = 100;
                    BaseColor BgColor = new BaseColor(this.grdvProNotasVentaFertilizantes.HeaderStyle.BackColor);

                    #region Titulo Nota de Venta
                    Font a = new Font();
                    a.Color = new BaseColor(this.grdvProNotasVentaFertilizantes.HeaderStyle.ForeColor);
                    Phrase phr = new Phrase("NOTA DE VENTA", a);
                    PdfPCell cell = new PdfPCell(phr);
                    cell.Border = iBorder;
                    cell.Colspan = 3;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = new BaseColor(this.grdvProNotasVentaFertilizantes.HeaderStyle.BackColor);
                    teibol.AddCell(cell);
                    #endregion

                    #region Logotipo
                    iTextSharp.text.Image logo;
                    //todo: change here to banco sistema.
                    logo = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/imagenes/LogoMargaritasMedium.jpg"));
                    logo.ScalePercent(20);
                    PdfPCell celda = new PdfPCell(logo);
                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                    celda.VerticalAlignment = Element.ALIGN_MIDDLE;
                    celda.Border = iBorder;
                    teibol.AddCell(celda);
                    //imagen
                    #endregion

                    #region Datos de la empresa
                    PdfPTable teiboldatos = new PdfPTable(1);
                    teiboldatos.WidthPercentage = 100;

                    cell = PdfCreator.getCellWithFormat("COMERCIALIZADORA LAS MARGARITAS S.P.R. DE R.L.", true, Element.ALIGN_LEFT, 8f + fAjusteLetra);
                    cell.Border = iBorder;
                    teiboldatos.AddCell(cell);

                    cell = PdfCreator.getCellWithFormat("C.P. 46600 Ameca, Jalisco", false, Element.ALIGN_LEFT, 6f + fAjusteLetra); cell.Border = 0; teiboldatos.AddCell(cell);
                    cell = PdfCreator.getCellWithFormat("Av. Patria Oriente No. 10  ", false, Element.ALIGN_LEFT, 6f + fAjusteLetra); cell.Border = 0; teiboldatos.AddCell(cell);
                    cell = PdfCreator.getCellWithFormat("01(375) 758 1199  ", false, Element.ALIGN_LEFT, 6f + fAjusteLetra); cell.Border = 0; teiboldatos.AddCell(cell);
                    cell = PdfCreator.getCellWithFormat("R.F.C. CMA-011023-JP9   ", false, Element.ALIGN_LEFT, 6f + fAjusteLetra); cell.Border = 0; teiboldatos.AddCell(cell);

                    cell = new PdfPCell(teiboldatos);
                    cell.Border = iBorder;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    teibol.AddCell(cell);
                    #endregion

                    #region Datos de la nota
                    PdfPTable teibolDAT = new PdfPTable(1);
                    teibolDAT.WidthPercentage = 100;

                    phr = new Phrase("FOLIO", a);
                    cell = new PdfPCell(phr);
                    cell.Border = iBorder;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = BgColor;
                    teibolDAT.AddCell(cell);

                    phr = new Phrase(drNota["Folio"].ToString());
                    cell = new PdfPCell(phr);
                    cell.Border = iBorder;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);
                    teibolDAT.AddCell(cell);


                    phr = new Phrase("FECHA", a);
                    cell = new PdfPCell(phr);
                    cell.Border = iBorder;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = BgColor;
                    teibolDAT.AddCell(cell);

                    phr = new Phrase(((DateTime)drNota["Fecha"]).ToString("dd/MM/yyyy"));
                    cell = new PdfPCell(phr);
                    cell.Border = iBorder;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);
                    teibolDAT.AddCell(cell);

                    phr = new Phrase("CICLO", a);
                    cell = new PdfPCell(phr);
                    cell.Border = iBorder;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = BgColor;
                    teibolDAT.AddCell(cell);

                    phr = new Phrase(drNota["CicloName"].ToString());
                    cell = new PdfPCell(phr);
                    cell.Border = iBorder;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);
                    teibolDAT.AddCell(cell);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    celda.VerticalAlignment = Element.ALIGN_TOP;
                    cell = new PdfPCell(teibolDAT);
                    cell.Border = iBorder;
                    teibol.AddCell(cell);
                    #endregion
                    //finalizacion de la parte de arriba de la nota de venta
                    document.Add(teibol);


                    //principio de los datos del productor
                    #region Datos del productor
                    PdfPTable teibolProductor = new PdfPTable(new float[] { 3f, 2.5f, 2.5f, 2f });
                    teibolDAT.WidthPercentage = 100;

                    Font fntProductorData = new Font(Font.FontFamily.HELVETICA, (float)(5.0f + fAjusteLetra));

                    phr = new Phrase("CLIENTE", a);
                    cell = new PdfPCell(phr); cell.Border = 1; cell.BackgroundColor = BgColor; teibolProductor.AddCell(cell);

                    phr = new Phrase("DESTINO", a);
                    cell = new PdfPCell(phr); cell.Border = 1; cell.BackgroundColor = BgColor; teibolProductor.AddCell(cell);

                    phr = new Phrase("No. PERMISO", a); cell = new PdfPCell(phr); cell.Border = 1; cell.BackgroundColor = BgColor; teibolProductor.AddCell(cell);

                    phr = new Phrase("TRACTORCAMION", a); cell = new PdfPCell(phr); cell.Border = 1; cell.BackgroundColor = BgColor; teibolProductor.AddCell(cell);

                    //segunda row
                    //cliente
                    phr = new Phrase(HttpContext.Current.Server.HtmlDecode(drNota["Productor"].ToString()).ToUpper(), fntProductorData);
                    cell = new PdfPCell(phr); cell.Border = 1; teibolProductor.AddCell(cell);
                    //destino
                    phr = new Phrase(HttpContext.Current.Server.HtmlDecode(drNota["destino"].ToString()).ToUpper(), fntProductorData);
                    cell = new PdfPCell(phr); cell.Border = 1; teibolProductor.AddCell(cell);
                    //# permiso
                    phr = new Phrase(HttpContext.Current.Server.HtmlDecode(drNota["numeropermiso"].ToString()).ToUpper(), fntProductorData);
                    cell = new PdfPCell(phr); cell.Border = 1; teibolProductor.AddCell(cell);
                    //Tractorcamion
                    phr = new Phrase(HttpContext.Current.Server.HtmlDecode(drNota["tractorcamion"].ToString()).ToUpper(), fntProductorData);
                    cell = new PdfPCell(phr); cell.Border = 1; teibolProductor.AddCell(cell);

                    //tercera row
                    phr = new Phrase("TRANSPORTISTA", a); cell = new PdfPCell(phr); cell.Border = 1; cell.BackgroundColor = BgColor; teibolProductor.AddCell(cell);

                    phr = new Phrase("CHOFER", a); cell = new PdfPCell(phr); cell.Border = 1; cell.BackgroundColor = BgColor; teibolProductor.AddCell(cell);

                    phr = new Phrase("PLACAS", a); cell = new PdfPCell(phr); cell.Border = 1; cell.BackgroundColor = BgColor; teibolProductor.AddCell(cell);

                    phr = new Phrase("COLOR", a); cell = new PdfPCell(phr); cell.Border = 1; cell.BackgroundColor = BgColor; teibolProductor.AddCell(cell);

                    //segunda row
                    //transportista
                    phr = new Phrase(HttpContext.Current.Server.HtmlDecode(drNota["transportista"].ToString()).ToUpper(), fntProductorData);
                    cell = new PdfPCell(phr); cell.Border = 1; teibolProductor.AddCell(cell);
                    //chofer
                    phr = new Phrase(HttpContext.Current.Server.HtmlDecode(drNota["nombrechofer"].ToString()).ToUpper(), fntProductorData);
                    cell = new PdfPCell(phr); cell.Border = 1; teibolProductor.AddCell(cell);
                    //placas
                    phr = new Phrase(HttpContext.Current.Server.HtmlDecode(drNota["placas"].ToString()).ToUpper(), fntProductorData);
                    cell = new PdfPCell(phr); cell.Border = 1; teibolProductor.AddCell(cell);
                    //color
                    phr = new Phrase(HttpContext.Current.Server.HtmlDecode(drNota["color"].ToString()).ToUpper(), fntProductorData);
                    cell = new PdfPCell(phr); cell.Border = 1; teibolProductor.AddCell(cell);

                    teibolProductor.WidthPercentage = 100;
                    document.Add(teibolProductor);

                    #endregion

                    teibol = new PdfPTable(new float[] { 1.5f, 6f, 2.5f });
                    teibol.WidthPercentage = 100;

                    //datos de nota y productor agregados

                    //Productos del detalle


                    #region Detalle de productos
                    PdfPTable teibolGrids = new PdfPTable(new float[] { 10.0f });
                    teibolGrids.WidthPercentage = 100;
                    teibolGrids.CompleteRow();

                    PdfPTable t;
                    if (this.grdvProNotasVentaFertilizantes.Rows.Count > 0)
                    {
                        cell = PdfCreator.getCellWithFormat("FERTILIZANTES", true, Element.ALIGN_CENTER, 9.5f);
                        cell.Border = 0;
                        teibolGrids.AddCell(cell);

                        t = PdfCreator.GridViewToPdfPTable(ref this.grdvProNotasVentaFertilizantes, 100, fDetalleColSize, 2f);
                        cell.AddElement(t);
                        cell.Border = 0; cell.HorizontalAlignment = Element.ALIGN_LEFT; teibolGrids.AddCell(cell);
                    }
                    
                    if (this.grdvProNotasVentaInsumos.Rows.Count > 0)
                    {
                        cell = PdfCreator.getCellWithFormat("OTROS PRODUCTOS", true, Element.ALIGN_CENTER, 9.5f);
                        cell.Border = 0;
                        teibolGrids.AddCell(cell);

                        t = PdfCreator.GridViewToPdfPTable(ref this.grdvProNotasVentaInsumos, 100, fDetalleColSize, 2f);
                        cell = new PdfPCell();
                        cell.AddElement(t);
                        cell.Border = 0; cell.HorizontalAlignment = Element.ALIGN_LEFT; teibolGrids.AddCell(cell);
                    }                 

                    document.Add(teibolGrids);
                    #endregion


                    //teibol de grids
                    teibolGrids = new PdfPTable(new float[] { 7.5f, 2.5f });
                    teibolGrids.WidthPercentage = 100;
                    teibolGrids.CompleteRow();

                    teibol = new PdfPTable(1);
                    teibol.WidthPercentage = 100;

                    //pagosCaja Chica

                    #region PAGOS A LA NOTA
                    if (this.grvPagos.Rows.Count > 0)
                    {
                        Font fntPagos = new Font(a.BaseFont, 9.0f);
                        phr = new Phrase("PAGOS A LA NOTA",fntPagos);
                        cell = new PdfPCell(phr); cell.Border = iBorder; cell.HorizontalAlignment = Element.ALIGN_LEFT; teibol.AddCell(cell);

                        cell = new PdfPCell(PdfCreator.GridViewToPdfPTable(ref grvPagos, 100, fPago, 2f));
                        cell.Border = iBorder; cell.HorizontalAlignment = Element.ALIGN_CENTER; teibol.AddCell(cell);
                    }
                    #endregion


                    cell = new PdfPCell(teibol);
                    cell.Border = 0;
                    teibolGrids.AddCell(cell);



                    PdfPTable teibolTotales = new PdfPTable(2);
                    teibolTotales.WidthPercentage = 100;

                    #region TOTALES

                    a = new Font(a.BaseFont, 10.0f);
                    Font fntNumeros = new Font(a.BaseFont, 10.0f);
                    fntNumeros.Color = BaseColor.BLACK;
                    a.Color = new BaseColor(this.grdvProNotasVentaFertilizantes.HeaderStyle.ForeColor);


//                     phr = new Phrase(" " /*"TOTALES"*/);
//                     cell = new PdfPCell(phr); cell.Colspan = 2; cell.Border = 0; cell.HorizontalAlignment = Element.ALIGN_CENTER;
//                     teibolTotales.AddCell(cell);

                    phr = new Phrase("Sub Total", a);
                    cell = new PdfPCell(phr); cell.BackgroundColor = BgColor;
                    teibolTotales.AddCell(cell);
                    //subtotal
                    phr = new Phrase(string.Format("{0:C2}", Utils.GetSafeFloat(drNota["Subtotal"].ToString())), fntNumeros); cell = new PdfPCell(phr); cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    teibolTotales.AddCell(cell);

                    phr = new Phrase("IVA", a);
                    cell = new PdfPCell(phr); cell.BackgroundColor = BgColor;
                    teibolTotales.AddCell(cell);
                    //IVA
                    phr = new Phrase(string.Format("{0:C2}", Utils.GetSafeFloat(drNota["IVA"].ToString())), fntNumeros); cell = new PdfPCell(phr); cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    teibolTotales.AddCell(cell);

                    /////calculate pagos

                    phr = new Phrase("EFECTIVO", a);
                    cell = new PdfPCell(phr); cell.BackgroundColor = BgColor; teibolTotales.AddCell(cell);
                    //EFECTIVO
                    phr = new Phrase("-" + HttpContext.Current.Server.HtmlDecode(this.gvConcentradoPagos.Rows[0].Cells[0].Text), fntNumeros);
                    cell = new PdfPCell(phr); cell.HorizontalAlignment = Element.ALIGN_RIGHT; teibolTotales.AddCell(cell);

                    phr = new Phrase("MOV. BANCOS", a);
                    cell = new PdfPCell(phr); cell.BackgroundColor = BgColor; teibolTotales.AddCell(cell);
                    //MOV. BANCOS
                    phr = new Phrase("-" + HttpContext.Current.Server.HtmlDecode(gvConcentradoPagos.Rows[0].Cells[1].Text), fntNumeros);
                    cell = new PdfPCell(phr); cell.HorizontalAlignment = Element.ALIGN_RIGHT; teibolTotales.AddCell(cell);

                    phr = new Phrase("T .DIESEL", a);
                    cell = new PdfPCell(phr); cell.BackgroundColor = BgColor; teibolTotales.AddCell(cell);
                    //T .DIESEL
                    phr = new Phrase("-" + HttpContext.Current.Server.HtmlDecode(gvConcentradoPagos.Rows[0].Cells[2].Text), fntNumeros);
                    cell = new PdfPCell(phr); cell.HorizontalAlignment = Element.ALIGN_RIGHT; teibolTotales.AddCell(cell);
                    phr = new Phrase("BOLETAS", a);
                    cell = new PdfPCell(phr); cell.BackgroundColor = BgColor; teibolTotales.AddCell(cell);
                    //boletas
                    phr = new Phrase("-" + HttpContext.Current.Server.HtmlDecode(gvConcentradoPagos.Rows[0].Cells[3].Text), fntNumeros);
                    cell = new PdfPCell(phr); cell.HorizontalAlignment = Element.ALIGN_RIGHT; teibolTotales.AddCell(cell);


                    double total = Utils.GetSafeFloat(drNota["Total"].ToString());
                    double pagos = Utils.GetSafeFloat(gvConcentradoPagos.Rows[0].Cells[0].Text) + Utils.GetSafeFloat(gvConcentradoPagos.Rows[0].Cells[1].Text) + Utils.GetSafeFloat(gvConcentradoPagos.Rows[0].Cells[2].Text) + Utils.GetSafeFloat(gvConcentradoPagos.Rows[0].Cells[3].Text);
                    string textototal = string.Format("{0:c2}", (total - pagos));

                    phr = new Phrase("TOTAL", a);
                    cell = new PdfPCell(phr); cell.BackgroundColor = BgColor; teibolTotales.AddCell(cell);
                    //TOTAL
                    phr = new Phrase(textototal, fntNumeros);
                    cell = new PdfPCell(phr); cell.HorizontalAlignment = Element.ALIGN_RIGHT; teibolTotales.AddCell(cell);

                    cell = new PdfPCell(teibolTotales);
                    cell.Border = 0;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    //teibol.AddCell(cell);


                    teibolGrids.AddCell(cell);
                    #endregion

                    #region PRINT CREDITO RELATED
                    //CheckBox acredito = (CheckBox)gvNota.Rows[0].Cells[22].Controls[0];
                    if (((bool)drNota["acredito"]))
                    {
                        //teibol.AddCell(cell);
                        phr = new Phrase("CREDITO RELACIONADO  : " + drNota["creditoID"].ToString() + " - " + drNota["CreditoNombre"].ToString().ToUpper());
                        cell = new PdfPCell(phr);
                        cell.Border = 0;
                        cell.Colspan = 2;
                        cell.VerticalAlignment = Element.ALIGN_LEFT;

                        teibolGrids.AddCell(cell);
                    }
                    #endregion
                    document.Add(teibolGrids);

                    #region PRODUCTOR NAME AND SIGNATURE
                    document.Add(new Paragraph(" ", new Font(Font.FontFamily.TIMES_ROMAN, 10.0f + fAjusteLetra, Element.ALIGN_LEFT))); //for new line :-D
                    PdfPTable tabla = new PdfPTable(1);
                    tabla.WidthPercentage = 100;
                    cell = PdfCreator.getCellWithFormat("__________________________________________", true, Element.ALIGN_CENTER, 7.0f + fAjusteLetra);
                    cell.Border = 0;
                    tabla.AddCell(cell);
                    string sProdName = drNota["Productor"].ToString().ToUpper();
                    if(drNota["personaautorizada"] != null &&
                        drNota["personaautorizada"].ToString().Trim().Length > 0)
                    {
                        sProdName = drNota["personaautorizada"].ToString().Trim();
                    }
                    cell = PdfCreator.getCellWithFormat(sProdName.ToUpper(), true, Element.ALIGN_CENTER, 7.0f + fAjusteLetra);
                    cell.Border = 0;
                    tabla.AddCell(cell);
                    tabla.CompleteRow();
                    document.Add(tabla);
                    #endregion

                    //print observaciones only when there is something to print
                    String sObservaciones = drNota["Observaciones"].ToString().ToUpper();
                    #region OBSERVACIONES
                    if (sObservaciones.Length > 0)
                    {
                        document.Add(new Paragraph(" "));
                        PdfPTable teibolComments = new PdfPTable(1);
                        teibolComments.WidthPercentage = 100;
                        teibolComments.HorizontalAlignment = Element.ALIGN_LEFT;
                        teibolComments.AddCell(PdfCreator.getCellWithFormat("Observaciones: " + sObservaciones, false, Element.ALIGN_JUSTIFIED, 8.0f));
                        document.Add(teibolComments);
                    }
                    #endregion

                    if (!this.IsSistemBanco)
                    {
                        PdfContentByte cb;
                        BaseFont fnt;
                        fnt = FontFactory.GetFont(iTextSharp.text.FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL).BaseFont;
                        cb = pdfW.DirectContent;
                        cb.BeginText();
                        cb.SetFontAndSize(fnt, 8f);
                        cb.SetColorFill(BaseColor.BLACK);
                        String sFooter = "Impreso: ";
                        sFooter += Utils.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                        sFooter += " Impreso por: ";
                        sFooter += new BasePage().CurrentUserName;
                        sFooter += " Realizada por: " + drNota["hechoxNombre"].ToString().ToUpper();
                        sFooter += " # Nota de Venta: " + drNota["notadeventaID"].ToString();
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, sFooter, (float)Utils.conviertedecmsapoints(1), document.PageSize.Height - (document.PageSize.Height - (float)Utils.conviertedecmsapoints(1)), 0);
                        cb.EndText();
                    }

                    if (comm.Connection != null)
                    {
                        comm.Connection.Close();
                    }

                    //aqui va la impresion del sello
                    //                  PdfContentByte cb = pdfW.DirectContent;
                    //                  cb.Rectangle()
                }
                catch (Exception e)
                {
                    // handle exception
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.PRINT, new BasePage().UserID, "Error Printing liquidacion Ex:" + e.Message + " Stack: " + Environment.StackTrace, "PRINT LIQUIDACION");
                }
                document.Close();
                bytes = ms.GetBuffer();
                if (comm != null && comm.Connection != null)
                {
                    comm.Connection.Close();
                }
            }
            //document.close();


            return bytes;
        }
        

        protected double sumaPagos()
        {
            Label lbl;
            double total = 0.0;



            foreach (GridViewRow row in this.grvPagos.Rows)
            {
                lbl = (Label)row.FindControl("Label12");
                string text = row.Cells[7].Text;
                total += Utils.GetSafeFloat(text.Replace("$", "").Replace(",", ""));
            }
            return total;
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
                        e.Row.Cells[4].Text = lbl.Text;

                        String query = "SELECT abono FROM MovimientosCaja WHERE movimientoID=@movimientoID";
                        SqlCommand comm = new SqlCommand(query, conn);
                        comm.Parameters.Add("@movimientoID", SqlDbType.Float).Value = int.Parse(this.grvPagos.DataKeys[e.Row.RowIndex]["movimientoID"].ToString());
                        conn.Open();
                        lbl = (Label)e.Row.FindControl("Label12");
                        lbl.Text = string.Format("{0:C2}", Utils.GetSafeFloat(comm.ExecuteScalar().ToString()));
                        e.Row.Cells[7].Text = lbl.Text;

                    }
                    else if (this.grvPagos.DataKeys[e.Row.RowIndex]["tarjetaDieselID"].ToString() != "")
                    {
                        Label lbl = (Label)e.Row.FindControl("Label10");
                        lbl.Text = this.grvPagos.DataKeys[e.Row.RowIndex]["tarjetaDieselID"].ToString();
                        e.Row.Cells[5].Text = lbl.Text;
                        lbl = (Label)e.Row.FindControl("Label11");
                        lbl.Text = "";
                        lbl = (Label)e.Row.FindControl("Label9");

                        //if columns are added/ removed please update the index.
                        lbl.Text = "TARJETA DIESEL";
                        e.Row.Cells[4].Text = lbl.Text;

                        String query = "SELECT monto FROM TarjetasDiesel WHERE folio=@folio";
                        SqlCommand comm = new SqlCommand(query, conn);
                        comm.Parameters.Add("@folio", SqlDbType.Int).Value = int.Parse(this.grvPagos.DataKeys[e.Row.RowIndex]["tarjetaDieselID"].ToString());
                        conn.Open();
                        lbl = (Label)e.Row.FindControl("Label12");
                        lbl.Text = string.Format("{0:C2}", Utils.GetSafeFloat(comm.ExecuteScalar().ToString()));
                        e.Row.Cells[7].Text = lbl.Text;

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
                            e.Row.Cells[5].Text = lbl.Text;
                            lbl = (Label)e.Row.FindControl("Label9");
                            lbl.Text = rd["Concepto"].ToString();
                            e.Row.Cells[4].Text = lbl.Text;



                            lbl = (Label)e.Row.FindControl("Label12");
                            lbl.Text = string.Format("{0:c2}", Utils.GetSafeFloat(rd["abono"].ToString()));
                            e.Row.Cells[7].Text = lbl.Text;
                            lbl = (Label)e.Row.FindControl("Label11");
                            lbl.Text = rd["nombre"].ToString();
                            e.Row.Cells[6].Text = lbl.Text;
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
                            e.Row.Cells[5].Text = lbl.Text;
                            lbl = (Label)e.Row.FindControl("Label9");
                            lbl.Text = "BOLETA";
                            e.Row.Cells[4].Text = lbl.Text;


                            lbl = (Label)e.Row.FindControl("Label12");
                            lbl.Text = string.Format("{0:c2}", Utils.GetSafeFloat(rd["totalapagar"].ToString()));
                            e.Row.Cells[7].Text = lbl.Text;
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
                            e.Row.Cells[5].Text = lbl.Text;
                            lbl = (Label)e.Row.FindControl("Label9");
                            lbl.Text = "CHEQUE";
                            e.Row.Cells[4].Text = lbl.Text;


                            lbl = (Label)e.Row.FindControl("Label12");
                            lbl.Text = string.Format("{0:c2}", Utils.GetSafeFloat(rd["monto"].ToString()));
                            e.Row.Cells[7].Text = lbl.Text;
                            lbl = (Label)e.Row.FindControl("Label11");
                            lbl.Text = rd["nombre"].ToString();
                            e.Row.Cells[6].Text = lbl.Text;
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

                e.Row.Cells[6].Text = "TOTAL";


                e.Row.Cells[7].Text = string.Format("{0:C2}", sumaPagos());
            }

        }
    }
}
