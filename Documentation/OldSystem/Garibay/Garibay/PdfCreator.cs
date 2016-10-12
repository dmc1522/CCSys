using System;
using System.Collections.Generic;
using System.Text;

//--- Add the following to make this code work
using iTextSharp.text;
using iTextSharp.text.pdf;
//using System.Drawing ;
using System.IO;
using System.Diagnostics;
//using VVX;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web;
using System.Collections;
using System.Web.UI.HtmlControls;
using iTextSharp.text.html;
using System.Xml;
using iTextSharp.text.xml;
using Microsoft.SqlServer;

namespace Garibay
{
    public class PdfCreator
    {
        static bool bRet = false;


        public bool Success
        {
            get { return bRet; }
            set { bRet = value; }
        }
        public enum tamañoPapel
        {
            CARTA = 0,
            OFICIO = 1,
        }
        public enum orientacionPapel
        {
            VERTICAL = 0,
            HORIZONTAL = 1,
        }
        public static Document inicializadocumento(tamañoPapel tamañopapel, orientacionPapel orientacionpapel)
        {
            Document document = new Document(PageSize.LEGAL.Rotate()); ;
            switch (orientacionpapel)
            {
                case orientacionPapel.VERTICAL:
                    switch (tamañopapel)
                    {
                        case tamañoPapel.CARTA:
                            document = new Document(PageSize.LETTER);
                            break;
                        case tamañoPapel.OFICIO:
                            document = new Document(PageSize.LEGAL);
                            break;
                    }
                    break;
                case orientacionPapel.HORIZONTAL:
                    switch (tamañopapel)
                    {
                        case tamañoPapel.CARTA:
                            document = new Document(PageSize.LETTER.Rotate());
                            break;
                        case tamañoPapel.OFICIO:
                            document = new Document(PageSize.LEGAL.Rotate());
                            break;
                    }
                    break;
            }
            return document;
        }
        public static float GetYWithAdjust(float y, Document doc)
        {
            return (doc.PageSize.Height - (float)Utils.getPointsFromCMasFloat(y));
        }

        /// <summary>
        /// Print a Factura de venta data in a PDF file.
        /// </summary>
        /// <param name="FacturaID">the id of the factura de venta</param>
        /// <returns>Path to the temp file.</returns>
        public static Byte[] printFacturaVenta(int FacturaID)
        {
            //String sPathArchivo = Path.GetTempFileName();
            Byte[] res = null;
            MemoryStream ms = new MemoryStream();
            Document document = inicializadocumento(tamañoPapel.CARTA, orientacionPapel.VERTICAL);
            document.SetMargins(0, 0, 0, 0);
            double fAjusteX = myConfig.GetDoubleConfig("AJUSTE_X", myConfig.CATEGORIA.FACTURAVENTA, 0.0);
            double fAjusteY = myConfig.GetDoubleConfig("AJUSTE_Y", myConfig.CATEGORIA.FACTURAVENTA, 0.0);
            double fFontSize = myConfig.GetDoubleConfig("FONTSIZE", myConfig.CATEGORIA.FACTURAVENTA, 9.0);
            double fFontSizeNumLetra = myConfig.GetDoubleConfig("FONTSIZELETRA", myConfig.CATEGORIA.FACTURAVENTA, 7.5);
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            SqlConnection connDetalle = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                //PdfWriter pdfW = PdfWriter.GetInstance(document, new FileStream(sPathArchivo, FileMode.Create));
                PdfWriter pdfW = PdfWriter.GetInstance(document, ms);

                document.Open();
                PdfContentByte cb;
                BaseFont fnt;
                fnt = FontFactory.GetFont(iTextSharp.text.FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL).BaseFont;
                cb = pdfW.DirectContent;
                cb.BeginText();
                cb.SetFontAndSize(fnt, (float)fFontSize);
                cb.SetColorFill(BaseColor.BLACK);
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "SELECT     FacturasClientesVenta.fecha, ClientesVentas.nombre, ClientesVentas.domicilio, ClientesVentas.ciudad, Estados.estado, ClientesVentas.RFC, ClientesVentas.telefono, "
                    + " FacturasClientesVenta.subtotal, FacturasClientesVenta.IVA, FacturasClientesVenta.total, FacturasClientesVenta.RETIVA, ClientesVentas.colonia, "
                    + " ClientesVentas.CP, FacturasClientesVenta.observaciones FROM         FacturasClientesVenta INNER JOIN ClientesVentas ON FacturasClientesVenta.clienteVentaID = ClientesVentas.clienteventaID INNER JOIN "
                    + " Estados ON ClientesVentas.estadoID = Estados.estadoID WHERE (FacturasClientesVenta.FacturaCV = @FacturaCV)";
                comm.Parameters.Add("@FacturaCV", SqlDbType.Int).Value = FacturaID;
                SqlDataReader readerFactura = comm.ExecuteReader();
                if (!readerFactura.HasRows || !readerFactura.Read())
                {
                    throw new Exception("No existen los datos para la factura seleccionada");
                }
                //print data for factura
                String sText = ((DateTime)readerFactura["fecha"]).ToString("dd/MM/yyyy");
                float px, py;
                //fecha
                px = (float)myConfig.GetDoubleConfig("FECHA_X", myConfig.CATEGORIA.FACTURAVENTA, (double)18.0);
                px = Utils.getPointsFromCMasFloat((double)(18.0 + fAjusteX));
                py = (float)myConfig.GetDoubleConfig("FECHA_Y", myConfig.CATEGORIA.FACTURAVENTA, (double)2.7);
                py = GetYWithAdjust((float)(py + fAjusteY), document);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, sText, px, py, 0);
                //nombre 4.07
                sText = readerFactura["nombre"].ToString().ToUpper();
                px = (float)myConfig.GetDoubleConfig("NOMBRE_X", myConfig.CATEGORIA.FACTURAVENTA, 1.0);
                px = Utils.getPointsFromCMasFloat((double)(px + fAjusteX));
                py = (float)myConfig.GetDoubleConfig("NOMBRE_Y", myConfig.CATEGORIA.FACTURAVENTA, 4.1);
                py = GetYWithAdjust((float)(py + fAjusteY), document);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, sText, px, py, 0);
                //domicilio
                sText = readerFactura["domicilio"].ToString().ToUpper();
                px = (float)myConfig.GetDoubleConfig("DOMICILIO_X", myConfig.CATEGORIA.FACTURAVENTA, 11.0);
                px = Utils.getPointsFromCMasFloat((double)(px + fAjusteX));
                py = (float)myConfig.GetDoubleConfig("DOMICILIO_Y", myConfig.CATEGORIA.FACTURAVENTA, 4.0);
                py = GetYWithAdjust((float)(py + fAjusteY), document);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, sText, px, py, 0);

                //Colonia y cp
                sText = "";
                if (readerFactura["colonia"] != null && readerFactura["colonia"].ToString().Length > 0)
                {
                    sText = readerFactura["colonia"].ToString().ToUpper();
                }
                if (readerFactura["CP"] != null && readerFactura["CP"].ToString().Length > 0)
                {
                    sText += "   CP: " + readerFactura["CP"].ToString().ToUpper();
                }
                if (sText.Length > 0)
                {
                    px = (float)myConfig.GetDoubleConfig("COLONIA_CP_X", myConfig.CATEGORIA.FACTURAVENTA, 11.0);
                    px = Utils.getPointsFromCMasFloat((double)(px + fAjusteX));
                    py = (float)myConfig.GetDoubleConfig("COLONIA_CP_Y", myConfig.CATEGORIA.FACTURAVENTA, 5.0);
                    py = GetYWithAdjust((float)(py + fAjusteY), document);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, sText, px, py, 0);
                }

                //ciudad 
                sText = readerFactura["ciudad"].ToString().ToUpper();
                px = (float)myConfig.GetDoubleConfig("CIUDAD_X", myConfig.CATEGORIA.FACTURAVENTA, 1.0);
                px = Utils.getPointsFromCMasFloat((double)(px + fAjusteX));
                py = (float)myConfig.GetDoubleConfig("CIUDAD_Y", myConfig.CATEGORIA.FACTURAVENTA, 6.27);
                py = GetYWithAdjust((float)(py + fAjusteY), document);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, sText, px, py, 0);
                //estado
                sText = readerFactura["estado"].ToString().ToUpper();
                px = (float)myConfig.GetDoubleConfig("ESTADO_X", myConfig.CATEGORIA.FACTURAVENTA, 5.79);
                px = Utils.getPointsFromCMasFloat((double)(px + fAjusteX));
                py = (float)myConfig.GetDoubleConfig("ESTADO_Y", myConfig.CATEGORIA.FACTURAVENTA, 6.27);
                py = GetYWithAdjust((float)(py + fAjusteY), document);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, sText, px, py, 0);
                //RFC
                sText = readerFactura["RFC"].ToString().ToUpper();
                px = (float)myConfig.GetDoubleConfig("RFC_X", myConfig.CATEGORIA.FACTURAVENTA, 10.74);
                px = Utils.getPointsFromCMasFloat((double)(px + fAjusteX));
                py = (float)myConfig.GetDoubleConfig("RFC_Y", myConfig.CATEGORIA.FACTURAVENTA, 6.27);
                py = GetYWithAdjust((float)(py + fAjusteY), document);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, sText, px, py, 0);
                //Telefono
                sText = readerFactura["telefono"].ToString().ToUpper();
                px = (float)myConfig.GetDoubleConfig("ESTADO_X", myConfig.CATEGORIA.FACTURAVENTA, 15.63);
                px = Utils.getPointsFromCMasFloat((double)(px + fAjusteX));
                py = (float)myConfig.GetDoubleConfig("ESTADO_Y", myConfig.CATEGORIA.FACTURAVENTA, 6.27);
                py = GetYWithAdjust((float)(py + fAjusteY), document);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, sText, px, py, 0);
                //subtotal
                sText = String.Format("{0:C2}", (double)readerFactura["subtotal"]);
                px = (float)myConfig.GetDoubleConfig("SUBTOTAL_X", myConfig.CATEGORIA.FACTURAVENTA, 20.21);
                px = Utils.getPointsFromCMasFloat((double)(px + fAjusteX));
                py = (float)myConfig.GetDoubleConfig("SUBTOTAL_Y", myConfig.CATEGORIA.FACTURAVENTA, 17.88);
                py = GetYWithAdjust((float)(py + fAjusteY), document);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, sText, px, py, 0);
                //IVA
                sText = String.Format("{0:C2}", (double)readerFactura["IVA"]);
                px = (float)myConfig.GetDoubleConfig("IVA_X", myConfig.CATEGORIA.FACTURAVENTA, 20.21);
                px = Utils.getPointsFromCMasFloat((double)(px + fAjusteX));
                py = (float)myConfig.GetDoubleConfig("IVA_Y", myConfig.CATEGORIA.FACTURAVENTA, 18.99);
                py = GetYWithAdjust((float)(py + fAjusteY), document);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, sText, px, py, 0);
                //RETIVA
                sText = String.Format("{0:C2}", (double)readerFactura["RETIVA"]);
                px = (float)myConfig.GetDoubleConfig("RETIVA_X", myConfig.CATEGORIA.FACTURAVENTA, 20.21);
                px = Utils.getPointsFromCMasFloat((double)(px + fAjusteX));
                py = (float)myConfig.GetDoubleConfig("RETIVA_Y", myConfig.CATEGORIA.FACTURAVENTA, 20.11);
                py = GetYWithAdjust((float)(py + fAjusteY), document);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, sText, px, py, 0);
                //total
                sText = String.Format("{0:C2}", (double)readerFactura["total"]);
                px = (float)myConfig.GetDoubleConfig("TOTAL_X", myConfig.CATEGORIA.FACTURAVENTA, 20.21);
                px = Utils.getPointsFromCMasFloat((double)(px + fAjusteX));
                py = (float)myConfig.GetDoubleConfig("TOTAL_Y", myConfig.CATEGORIA.FACTURAVENTA, 21.16);
                py = GetYWithAdjust((float)(py + fAjusteY), document);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, sText, px, py, 0);
                
                
                
                //cantidad con letra
                sText = Utils.NumeroALetras(readerFactura["total"].ToString().ToUpper());
                px = (float)myConfig.GetDoubleConfig("TOTAL_X", myConfig.CATEGORIA.FACTURAVENTA, 6.61);
                px = Utils.getPointsFromCMasFloat((double)(px + fAjusteX));
                py = (float)myConfig.GetDoubleConfig("TOTAL_X", myConfig.CATEGORIA.FACTURAVENTA, 17.88);
                py = GetYWithAdjust((float)(py + fAjusteY), document);
                cb.SetFontAndSize(fnt, (float)fFontSizeNumLetra);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, sText, px, py, 0);
                cb.SetFontAndSize(fnt, (float)fFontSize);


                //print detalle
                SqlCommand commDetalle = new SqlCommand();
                comm.Connection = connDetalle;
                connDetalle.Open();
                comm.CommandText = "SELECT     FacturaClienteVentaDetalle.Cantidad, Unidades.Unidad + '  ' + Productos.Nombre AS Nombre, FacturaClienteVentaDetalle.precio, "
                + " FacturaClienteVentaDetalle.Cantidad * FacturaClienteVentaDetalle.precio AS Importe, FacturaClienteVentaDetalle.FacturaCVID "
                + " FROM         FacturaClienteVentaDetalle INNER JOIN "
                + " Productos ON FacturaClienteVentaDetalle.productoID = Productos.productoID INNER JOIN "
                + " Unidades ON Productos.unidadID = Unidades.unidadID "
                + "  WHERE (FacturaClienteVentaDetalle.FacturaCVID = @FacturaCVID)";
                comm.Parameters.Add("@FacturaCVID", SqlDbType.Int).Value = FacturaID;
                SqlDataReader readerDetalle = comm.ExecuteReader();
                //px = (float)myConfig.GetDoubleConfig("DETALLE_X", myConfig.CATEGORIA.FACTURAVENTA, 2.04);
                py = (float)myConfig.GetDoubleConfig("DETALLE_Y", myConfig.CATEGORIA.FACTURAVENTA, 7.96);
                cb.SetFontAndSize(fnt, (float)fFontSizeNumLetra);
                py = GetYWithAdjust((float)(py + fAjusteY), document);
                float fDetalleCantidadX = (float)myConfig.GetDoubleConfig("DETALLE_CANTIDAD_X", myConfig.CATEGORIA.FACTURAVENTA, 2.04);
                float fDetalleNombreX = (float)myConfig.GetDoubleConfig("DETALLE_NOMBRE_X", myConfig.CATEGORIA.FACTURAVENTA, 2.41);
                float fDetallePrecioX = (float)myConfig.GetDoubleConfig("DETALLE_PRECIO_X", myConfig.CATEGORIA.FACTURAVENTA, 17.22);
                float fDetalleImporteX = (float)myConfig.GetDoubleConfig("DETALLE_IMPORTE_X", myConfig.CATEGORIA.FACTURAVENTA, 20.37);
                while(readerDetalle.Read())
                {
                    //Cantidad
                    px = fDetalleCantidadX;
                    sText = String.Format("{0:n2}", (double)readerDetalle["Cantidad"]);
                    px = Utils.getPointsFromCMasFloat((double)(px + fAjusteX));
                    //py = GetYWithAdjust((float)(py + fAjusteY), document);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, sText, px, py, 0);
                    //Nombre
                    px = fDetalleNombreX;
                    sText = readerDetalle["Nombre"].ToString().ToUpper();
                    px = Utils.getPointsFromCMasFloat((double)(px + fAjusteX));
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, sText, px, py, 0);
                    //P.Unitario
                    px = fDetallePrecioX;
                    sText = String.Format("{0:C5}", (double)readerDetalle["precio"]);
                    px = Utils.getPointsFromCMasFloat((double)(px + fAjusteX));
                    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, sText, px, py, 0);
                    //Importe
                    px = fDetalleImporteX;
                    sText = String.Format("{0:C2}", (double)readerDetalle["Importe"]);
                    px = Utils.getPointsFromCMasFloat((double)(px + fAjusteX));
                    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, sText, px, py, 0);
                    py -= 7.5f;
                    
                }

                if (readerFactura["observaciones"] != null && readerFactura["observaciones"].ToString().Trim().Length > 0)
                {
                    //py -= (7.5f * 5);
                    string[] strs = readerFactura["observaciones"].ToString().Trim().Split(new char[] { '\n' });
                    py = (float)(GetYWithAdjust((float)(17.0 + fAjusteY), document) + (8f * strs.Length));
                    foreach (string s in strs)
                    {
                        cb.SetFontAndSize(fnt, (float)(fFontSizeNumLetra + 1.0));
                        px = fDetalleCantidadX;
                        sText = s.ToUpper();
                        px = Utils.getPointsFromCMasFloat((double)(px + fAjusteX));
                        //py = GetYWithAdjust((float)(py + fAjusteY), document);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, sText, px, py, 0);
                        py -= (7.5f);
                    }
                }

                cb.EndText(); 
                document.Close();
                res = ms.GetBuffer();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Error printing Factura", ref ex);
            }
            finally
            {
                conn.Close();
            }
            return res;
        }
        public static byte[] printLiquidacion(int idliquidacion, tamañoPapel tamañopapel, orientacionPapel orientacionpapel, ref GridView gvBoletas, ref GridView gvAnticipos, ref GridView gvPagos, ref dsLiquidacion.dtLiquidacionDataRow rowLiqData)
        {
            return PdfCreator.printLiquidacion(idliquidacion, tamañopapel, orientacionpapel, ref gvBoletas, ref gvAnticipos, ref gvPagos, ref rowLiqData, null, null, null);
        }

        public static byte[] printLiquidacion(int idliquidacion, tamañoPapel tamañopapel, orientacionPapel orientacionpapel, ref GridView gvBoletas, ref GridView gvAnticipos, ref GridView gvPagos, ref dsLiquidacion.dtLiquidacionDataRow rowLiqData, float[] fBoletasColSize, float[] fAnticiposColSize, float[] fPagoColSize)
        {
            return PdfCreator.printLiquidacion(idliquidacion, tamañopapel, orientacionpapel, ref gvBoletas, ref gvAnticipos, ref gvPagos, ref rowLiqData, fBoletasColSize, fAnticiposColSize, fPagoColSize, null);
        }

        public static byte[] printLiquidacion(int idliquidacion, tamañoPapel tamañopapel, orientacionPapel orientacionpapel, ref GridView gvBoletas, ref GridView gvAnticipos, ref GridView gvPagos, ref dsLiquidacion.dtLiquidacionDataRow rowLiqData, float[] fBoletasColSize, float[] fAnticiposColSize, float[] fPagoColSize, DetailsView dvTotales)
        {

            //String pathArchivo = Path.GetTempFileName();
             Document document = inicializadocumento(tamañopapel, orientacionpapel);
            float fMarginsPoints = (float)Utils.conviertedecmsapoints(1);
            document.SetMargins(fMarginsPoints, fMarginsPoints, fMarginsPoints, fMarginsPoints);

//             frmLiquidacionTemplate frmTemplate = new frmLiquidacionTemplate();
//             
//             StreamReader SR;
//             String shtmlContent;
//             SR=File.OpenText(HttpContext.Current.Server.MapPath("frmLiquidacionTemplate.aspx"));
//             shtmlContent = SR.ReadToEnd();
//             SR.Close();
// 
//             shtmlContent = shtmlContent.Replace("NOMBREPRODUCTOR", "JOSE LUIS GOMEZ AGUILAR");
//             shtmlContent = shtmlContent.Replace("DOMICILIOPRODUCTOR", "JOSE LUIS GOMEZ AGUILAR");
//             shtmlContent = shtmlContent.Replace("POBLACIONPRODUCTOR", "JOSE LUIS GOMEZ AGUILAR");
//             shtmlContent = shtmlContent.Replace("lblFechaLiq", Utils.Now.ToString("d"));
            float fAjusteLetra = 5.0f;
            //String sOutputPath = Path.GetTempFileName();  //HttpContext.Current.Server.MapPath("test.pdf"); //
            MemoryStream ms = new MemoryStream();
             try
             {
                 PdfWriter pdfW = PdfWriter.GetInstance(document, ms);
                 document.Open();
                 PdfPTable teibol =  new PdfPTable(new float[]{2f,3f,5f});
                 teibol.WidthPercentage = 100;
                 PdfPCell cell = PdfCreator.getCellWithFormat("LIQUIDACION # " + idliquidacion.ToString(), true, Element.ALIGN_CENTER, 15.0f + fAjusteLetra);
                 cell.Colspan = 3;
                 cell.Border = 0;
                 teibol.AddCell(cell);
                 cell = PdfCreator.getCellWithFormat("NOMBRE:", true, Element.ALIGN_LEFT, 7f + fAjusteLetra);
                 cell.Border = 0;
                 teibol.AddCell(cell);
                 cell = PdfCreator.getCellWithFormat(rowLiqData.nombre.ToUpper(), false, Element.ALIGN_LEFT, 6f + fAjusteLetra);
                 cell.Border = 0;
                 teibol.AddCell(cell);

                 
                 iTextSharp.text.Image logo;
                 logo = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/imagenes/LogoIPROJALMediumHalf.jpg"));
                 logo.ScalePercent(20);
                 PdfPCell celda = new PdfPCell(logo);
                 celda.HorizontalAlignment = Element.ALIGN_RIGHT;
                 celda.VerticalAlignment = Element.ALIGN_MIDDLE;
                 celda.PaddingTop = 5;
                 celda.PaddingBottom = 5;
                 celda.PaddingRight = 5;
                 celda.Rowspan = 4;
                 celda.Border = 0;

                 teibol.AddCell(celda);


                 celda = PdfCreator.getCellWithFormat("DOMICILIO:", true, Element.ALIGN_LEFT, 7f + fAjusteLetra);
                 celda.Border = 0;
                 teibol.AddCell(celda);
                 celda = PdfCreator.getCellWithFormat(rowLiqData.domicilio.ToUpper(), false, Element.ALIGN_LEFT, 6f + fAjusteLetra);
                 celda.Border = 0;
                 teibol.AddCell(celda);

                 celda = PdfCreator.getCellWithFormat("POBLACION:", true, Element.ALIGN_LEFT, 7f + fAjusteLetra);
                 celda.Border = 0;
                 teibol.AddCell(celda);
                 celda = PdfCreator.getCellWithFormat(rowLiqData.poblacion.ToUpper(), false, Element.ALIGN_LEFT, 6f + fAjusteLetra);
                 celda.Border = 0;
                 teibol.AddCell(celda);

                 celda = PdfCreator.getCellWithFormat("FECHA:", true, Element.ALIGN_LEFT, 7f + fAjusteLetra);
                 celda.Border = 0;
                 teibol.AddCell(celda);
                 celda = PdfCreator.getCellWithFormat(rowLiqData.fechalarga.ToUpper(), false, Element.ALIGN_LEFT, 6f + fAjusteLetra);
                 celda.Border = 0;
                 teibol.AddCell(celda);

                 document.NewPage();
                 document.Add(teibol);
                 document.Add(new Paragraph(" ", new Font(Font.FontFamily.TIMES_ROMAN, 5.0f + fAjusteLetra, Element.ALIGN_LEFT))); //for new line :-D
                 gvBoletas.Columns[0].Visible = false;
                 document.Add(PdfCreator.GridViewToPdfPTable(ref gvBoletas,100, fBoletasColSize, 2f));
                 gvBoletas.Columns[0].Visible = true;

                 document.Add(new Paragraph(" ", new Font(Font.FontFamily.TIMES_ROMAN, 5.0f + fAjusteLetra, Element.ALIGN_LEFT))); //for new line :-D

                 teibol = new PdfPTable(new float[] { 7.5f, 2f });
                 teibol.WidthPercentage = 100;

                 teibol.AddCell(PdfCreator.getCellWithFormat("ANTICIPOS:", true, Element.ALIGN_CENTER, 9.0f + fAjusteLetra));

                 //celda totales
                 //celda totales
                 celda = new PdfPCell();
                 celda.Border = 0;
                 celda.Rowspan = 4;
                 PdfPTable teibolTotales = new PdfPTable(2);
                 teibolTotales.WidthPercentage = 100;

                 if (dvTotales == null)
                 {
                     teibolTotales.AddCell(PdfCreator.getCellWithFormat("Boletas:", true, Element.ALIGN_LEFT, 7.0f + fAjusteLetra));
                     teibolTotales.AddCell(PdfCreator.getCellWithFormat(string.Format("{0:c}", rowLiqData.totalBoletas), true, Element.ALIGN_RIGHT, 7.0f + fAjusteLetra));
                     teibolTotales.AddCell(PdfCreator.getCellWithFormat("Anticipos:", true, Element.ALIGN_LEFT, 7.0f + fAjusteLetra));
                     teibolTotales.AddCell(PdfCreator.getCellWithFormat(string.Format("{0:c}", rowLiqData.totalAnticipos), true, Element.ALIGN_RIGHT, 7.0f + fAjusteLetra));
                     teibolTotales.AddCell(PdfCreator.getCellWithFormat("Notas:", true, Element.ALIGN_LEFT, 7.0f + fAjusteLetra));
                     teibolTotales.AddCell(PdfCreator.getCellWithFormat(string.Format("{0:c}", rowLiqData.notas), true, Element.ALIGN_RIGHT, 7.0f + fAjusteLetra));

                     teibolTotales.AddCell(PdfCreator.getCellWithFormat("Intereses:", true, Element.ALIGN_LEFT, 7.0f + fAjusteLetra));
                     teibolTotales.AddCell(PdfCreator.getCellWithFormat(string.Format("{0:c}", rowLiqData.intereses), true, Element.ALIGN_RIGHT, 7.0f + fAjusteLetra));

                     teibolTotales.AddCell(PdfCreator.getCellWithFormat("Seguro:", true, Element.ALIGN_LEFT, 7.0f + fAjusteLetra));
                     teibolTotales.AddCell(PdfCreator.getCellWithFormat(string.Format("{0:c}", rowLiqData.seguro), true, Element.ALIGN_RIGHT, 7.0f + fAjusteLetra));

                     teibolTotales.AddCell(PdfCreator.getCellWithFormat("Pagos:", true, Element.ALIGN_LEFT, 7.0f + fAjusteLetra));
                     teibolTotales.AddCell(PdfCreator.getCellWithFormat(string.Format("{0:c}", rowLiqData.totalPagos), true, Element.ALIGN_RIGHT, 7.0f + fAjusteLetra));
                 }
                 else
                 {
                     foreach(DetailsViewRow row in dvTotales.Rows)
                     {
                         teibolTotales.AddCell(PdfCreator.getCellWithFormat(row.Cells[0].Text, true, Element.ALIGN_LEFT, 7.0f + fAjusteLetra));
                         teibolTotales.AddCell(PdfCreator.getCellWithFormat(row.Cells[1].Text, true, Element.ALIGN_RIGHT, 7.0f + fAjusteLetra));
                     }
                 }
                 celda.AddElement(teibolTotales);

                 teibol.AddCell(celda);
                 //celda totales

                 celda = new PdfPCell();
                 celda.AddElement(PdfCreator.GridViewToPdfPTable(ref gvAnticipos, 100, fAnticiposColSize, 2f));
                 celda.Border = 0;
                 celda.HorizontalAlignment = Element.ALIGN_LEFT;
                 teibol.AddCell(celda);

                 teibol.AddCell(PdfCreator.getCellWithFormat("PAGOS:", true, Element.ALIGN_CENTER, 9.0f + fAjusteLetra));
                 
                 //gvPagos.Columns[0].Visible = false;
                 celda = new PdfPCell();
                 celda.Border = 0;
                 celda.AddElement(PdfCreator.GridViewToPdfPTable(ref gvPagos, 100, fPagoColSize, 2f));
                 celda.HorizontalAlignment = Element.ALIGN_LEFT;
                 teibol.AddCell(celda);
                 //gvPagos.Columns[0].Visible = true;
                 document.Add(teibol);

                 document.Add(new Paragraph(" ", new Font(Font.FontFamily.TIMES_ROMAN, 10.0f + fAjusteLetra, Element.ALIGN_LEFT))); //for new line :-D
                 document.Add(new Paragraph(" ", new Font(Font.FontFamily.TIMES_ROMAN, 10.0f + fAjusteLetra, Element.ALIGN_LEFT))); //for new line :-D
                 teibol = new PdfPTable(1);
                 teibol.WidthPercentage = 100;
                 cell = PdfCreator.getCellWithFormat("__________________________________________", true, Element.ALIGN_CENTER, 7.0f + fAjusteLetra) ;
                 cell.Border = 0;
                 teibol.AddCell(cell);
                 cell = PdfCreator.getCellWithFormat(rowLiqData.nombre.ToUpper(), true, Element.ALIGN_CENTER, 7.0f + fAjusteLetra);
                 cell.Border = 0;
                 teibol.AddCell(cell);
                 document.Add(teibol);

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
                 sFooter += " Realizada por: " + rowLiqData.EjecutadaPor.ToUpper();
                 sFooter += " # Liquidacion: " + idliquidacion.ToString();
                 cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, sFooter, (float) Utils.conviertedecmsapoints(1), document.PageSize.Height - (document.PageSize.Height - (float)Utils.conviertedecmsapoints(1)), 0);
                 cb.EndText();

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
             //document.close();
            
                  
          return ms.GetBuffer();
        }
        
        public static byte [] printNotaVenta(int idNotaVenta, tamañoPapel tamanopapel, orientacionPapel orientacionpapel, ref GridView gvNota, ref GridView gvDetalle, ref GridView gvPagos, ref GridView grvTotales, float[] fDetalleColSize, float[] fPago)
        {
            
            Document document = inicializadocumento(tamanopapel, orientacionpapel);
            float fMarginsPoints = (float)Utils.conviertedecmsapoints(1);
            document.SetMargins(fMarginsPoints, fMarginsPoints, fMarginsPoints, fMarginsPoints);
            

            
            float fAjusteLetra = 5.0f;
            int iBorder = 0;
            MemoryStream ms = new MemoryStream();
            try
            {
                PdfWriter pdfW = PdfWriter.GetInstance(document, ms);
                document.Open();

                PdfPTable teibol = new PdfPTable(new float[] {1.5f,6f,2.5f});
                teibol.WidthPercentage = 100;

                #region Titulo Nota de Venta
                Font a =new Font();
                a.Color = new BaseColor(gvNota.HeaderStyle.ForeColor);
                Phrase phr = new Phrase("NOTA DE VENTA", a);
                PdfPCell cell = new PdfPCell(phr);
                cell.Border = iBorder;
                cell.Colspan = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);
                teibol.AddCell(cell);
                #endregion

                #region Logotipo
                iTextSharp.text.Image logo;
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
                PdfPTable teiboldatos = new PdfPTable(1) ;
                teiboldatos.WidthPercentage = 100;
                
                cell = PdfCreator.getCellWithFormat("COMERCIALIZADORA LAS MARGARITAS S.P.R. DE R.L.", true, Element.ALIGN_LEFT, 8f + fAjusteLetra);
                cell.Border = iBorder;
                teiboldatos.AddCell(cell);
                
                cell = PdfCreator.getCellWithFormat("C.P. 46600 Ameca, Jalisco", false, Element.ALIGN_LEFT, 6f + fAjusteLetra);cell.Border = 0;teiboldatos.AddCell(cell);
                cell = PdfCreator.getCellWithFormat("Av. Patria Oriente No. 10  ", false, Element.ALIGN_LEFT, 6f + fAjusteLetra);cell.Border = 0;teiboldatos.AddCell(cell);
                cell = PdfCreator.getCellWithFormat("01(375) 758 1199  ", false, Element.ALIGN_LEFT, 6f + fAjusteLetra);cell.Border = 0;teiboldatos.AddCell(cell);
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
                cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);
                teibolDAT.AddCell(cell);
                
                phr = new Phrase(gvNota.Rows[0].Cells[3].Text);
                cell = new PdfPCell(phr);
                cell.Border = iBorder;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);
                teibolDAT.AddCell(cell);


                phr = new Phrase("FECHA", a);
                cell = new PdfPCell(phr);
                cell.Border = iBorder;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);
                teibolDAT.AddCell(cell);

                phr = new Phrase(gvNota.Rows[0].Cells[2].Text);
                cell = new PdfPCell(phr);
                cell.Border = iBorder;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);
                teibolDAT.AddCell(cell);

                phr = new Phrase("CICLO",a);
                cell = new PdfPCell(phr);
                cell.Border = iBorder;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);
                teibolDAT.AddCell(cell);

                phr = new Phrase(gvNota.Rows[0].Cells[20].Text);
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
                PdfPTable teibolProductor = new PdfPTable(new float[]{3f,2.5f,2.5f,2f});
                teibolDAT.WidthPercentage = 100;              

                Font fntProductorData = new Font(Font.FontFamily.HELVETICA, (float)(5.0f + fAjusteLetra));

                phr = new Phrase("CLIENTE",a);
                cell = new PdfPCell(phr);cell.Border = 1;cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);teibolProductor.AddCell(cell);

                phr = new Phrase("DESTINO", a);
                cell = new PdfPCell(phr);cell.Border = 1;cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);teibolProductor.AddCell(cell);

                phr = new Phrase("No. PERMISO", a);cell = new PdfPCell(phr);cell.Border = 1;cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);teibolProductor.AddCell(cell);

                phr = new Phrase("TRACTORCAMION", a);cell = new PdfPCell(phr);cell.Border = 1;cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);teibolProductor.AddCell(cell);

                //segunda row
                //cliente
                phr = new Phrase(HttpContext.Current.Server.HtmlDecode(gvNota.Rows[0].Cells[1].Text).ToUpper(), fntProductorData);
                cell = new PdfPCell(phr);cell.Border = 1;teibolProductor.AddCell(cell);
                //destino
                phr = new Phrase(HttpContext.Current.Server.HtmlDecode(gvNota.Rows[0].Cells[19].Text).ToUpper(), fntProductorData);
                cell = new PdfPCell(phr);cell.Border = 1;teibolProductor.AddCell(cell);
                //# permiso
                phr = new Phrase(HttpContext.Current.Server.HtmlDecode(gvNota.Rows[0].Cells[10].Text).ToUpper(), fntProductorData);
                cell = new PdfPCell(phr);cell.Border = 1;teibolProductor.AddCell(cell);
                //Tractorcamion
                phr = new Phrase(HttpContext.Current.Server.HtmlDecode(gvNota.Rows[0].Cells[13].Text).ToUpper(), fntProductorData);
                cell = new PdfPCell(phr);cell.Border = 1;teibolProductor.AddCell(cell);

                //tercera row
                phr = new Phrase("TRANSPORTISTA", a);cell = new PdfPCell(phr);cell.Border = 1;cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);teibolProductor.AddCell(cell);

                phr = new Phrase("CHOFER", a);cell = new PdfPCell(phr);cell.Border = 1;cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);teibolProductor.AddCell(cell);

                phr = new Phrase("PLACAS", a);cell = new PdfPCell(phr);cell.Border = 1;cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);teibolProductor.AddCell(cell);

                phr = new Phrase("COLOR", a);cell = new PdfPCell(phr);cell.Border = 1;cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);teibolProductor.AddCell(cell);

                //segunda row
                //transportista
                phr = new Phrase(HttpContext.Current.Server.HtmlDecode(gvNota.Rows[0].Cells[11].Text).ToUpper(), fntProductorData);
                cell = new PdfPCell(phr);cell.Border = 1;teibolProductor.AddCell(cell);
                //chofer
                phr = new Phrase(HttpContext.Current.Server.HtmlDecode(gvNota.Rows[0].Cells[12].Text).ToUpper(), fntProductorData);
                cell = new PdfPCell(phr);cell.Border = 1;teibolProductor.AddCell(cell);
                //placas
                phr = new Phrase(HttpContext.Current.Server.HtmlDecode(gvNota.Rows[0].Cells[21].Text).ToUpper(), fntProductorData);
                cell = new PdfPCell(phr);cell.Border = 1;teibolProductor.AddCell(cell);
                //color
                phr = new Phrase(HttpContext.Current.Server.HtmlDecode(gvNota.Rows[0].Cells[14].Text).ToUpper(), fntProductorData);
                cell = new PdfPCell(phr);cell.Border = 1;teibolProductor.AddCell(cell);

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
                cell = PdfCreator.getCellWithFormat("DETALLE DE PRODUCTOS", true, Element.ALIGN_CENTER, 14.0f);
                cell.Border = 0;
                teibolGrids.AddCell(cell);
                PdfPTable t = PdfCreator.GridViewToPdfPTable(ref gvDetalle, 65, fDetalleColSize, 2f);
                cell = new PdfPCell(t); cell.Border = 0; cell.HorizontalAlignment = Element.ALIGN_LEFT; teibolGrids.AddCell(cell);

                document.Add(teibolGrids);
                #endregion


                //teibol de grids
                teibolGrids = new PdfPTable(new float []{7.5f,2.5f});
                teibolGrids.WidthPercentage = 100;
                teibolGrids.CompleteRow();

                teibol = new PdfPTable(1);
                teibol.WidthPercentage = 100;

                //pagosCaja Chica
                #region PAGOS A LA NOTA
                if (gvPagos.Rows.Count > 0)
                {
                    phr = new Phrase("PAGOS A LA NOTA");
                    cell = new PdfPCell(phr); cell.Border = iBorder; cell.HorizontalAlignment = Element.ALIGN_LEFT; teibol.AddCell(cell);

                    cell = new PdfPCell(PdfCreator.GridViewToPdfPTable(ref gvPagos, 100, fPago, 2f));
                    cell.Border = iBorder; cell.HorizontalAlignment = Element.ALIGN_CENTER; teibol.AddCell(cell);
                }
                #endregion

                //#region MOVIMIENTOS BANCARIOS
                //if (gvPagosBanco.Rows.Count > 0)
                //{
                //    phr = new Phrase("MOVIMIENTOS BANCARIOS");
                //    cell = new PdfPCell(phr); cell.Border = iBorder; cell.HorizontalAlignment = Element.ALIGN_LEFT; teibol.AddCell(cell);

                //    cell = new PdfPCell(PdfCreator.GridViewToPdfPTable(ref gvPagosBanco, 60, fPagoBancoColSize, 2f));
                //    cell.Border = 0; cell.HorizontalAlignment = Element.ALIGN_CENTER; teibol.AddCell(cell);
                //}
                //#endregion

                //#region TARJETAS DIESEL
                //if (gvPagosDiesel.Rows.Count > 0)
                //{
                //    phr = new Phrase("TARJETAS DIESEL");
                //    cell = new PdfPCell(phr); cell.Border = 0; cell.HorizontalAlignment = Element.ALIGN_LEFT; teibol.AddCell(cell);
                //    cell = new PdfPCell(PdfCreator.GridViewToPdfPTable(ref gvPagosDiesel, 100, fPagoDieselColSize, 2f));
                //    cell.Border = 0; cell.HorizontalAlignment = Element.ALIGN_CENTER; teibol.AddCell(cell);
                //}
                //#endregion

                
                cell = new PdfPCell(teibol);
                cell.Border = 0;
                teibolGrids.AddCell(cell);



                PdfPTable teibolTotales = new PdfPTable(2);
                teibolTotales.WidthPercentage = 100;

                #region TOTALES
                phr = new Phrase(" " /*"TOTALES"*/);
                cell = new PdfPCell(phr); cell.Colspan = 2; cell.Border = 0; cell.HorizontalAlignment = Element.ALIGN_CENTER;
                teibolTotales.AddCell(cell);

                phr = new Phrase("Sub Total", a);
                cell = new PdfPCell(phr); cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);
                teibolTotales.AddCell(cell);
                //subtotal
                phr = new Phrase(gvNota.Rows[0].Cells[4].Text);cell = new PdfPCell(phr); cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                teibolTotales.AddCell(cell);

                phr = new Phrase("IVA", a);
                cell = new PdfPCell(phr); cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);
                teibolTotales.AddCell(cell);
                //IVA
                phr = new Phrase(gvNota.Rows[0].Cells[6].Text);cell = new PdfPCell(phr); cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                teibolTotales.AddCell(cell);

                phr = new Phrase("EFECTIVO", a);
                cell = new PdfPCell(phr);cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);teibolTotales.AddCell(cell);
                //EFECTIVO
                phr = new Phrase("-" + HttpContext.Current.Server.HtmlDecode(grvTotales.Rows[0].Cells[0].Text));
                cell = new PdfPCell(phr);cell.HorizontalAlignment = Element.ALIGN_RIGHT;teibolTotales.AddCell(cell);

                phr = new Phrase("MOV. BANCOS", a);
                cell = new PdfPCell(phr);cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);teibolTotales.AddCell(cell);
                //MOV. BANCOS
                phr = new Phrase("-" + HttpContext.Current.Server.HtmlDecode(grvTotales.Rows[0].Cells[1].Text));
                cell = new PdfPCell(phr);cell.HorizontalAlignment = Element.ALIGN_RIGHT;teibolTotales.AddCell(cell);

                phr = new Phrase("T .DIESEL", a);
                cell = new PdfPCell(phr);cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);teibolTotales.AddCell(cell);
                //T .DIESEL
                phr = new Phrase("-" + HttpContext.Current.Server.HtmlDecode(grvTotales.Rows[0].Cells[2].Text));
                cell = new PdfPCell(phr);cell.HorizontalAlignment = Element.ALIGN_RIGHT;teibolTotales.AddCell(cell);
                phr = new Phrase("BOLETAS", a);
                cell = new PdfPCell(phr); cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor); teibolTotales.AddCell(cell);
                //boletas
                phr = new Phrase("-" + HttpContext.Current.Server.HtmlDecode(grvTotales.Rows[0].Cells[3].Text));
                cell = new PdfPCell(phr); cell.HorizontalAlignment = Element.ALIGN_RIGHT; teibolTotales.AddCell(cell);


                double total = Utils.GetSafeFloat(gvNota.Rows[0].Cells[5].Text);
                double pagos = Utils.GetSafeFloat(grvTotales.Rows[0].Cells[0].Text) + Utils.GetSafeFloat(grvTotales.Rows[0].Cells[1].Text) + Utils.GetSafeFloat(grvTotales.Rows[0].Cells[2].Text) + Utils.GetSafeFloat(grvTotales.Rows[0].Cells[3].Text);
                string textototal = string.Format("{0:c2}", (total - pagos));

                phr = new Phrase("TOTAL", a);
                cell = new PdfPCell(phr);cell.BackgroundColor = new BaseColor(gvNota.HeaderStyle.BackColor);teibolTotales.AddCell(cell);
                //TOTAL
                phr = new Phrase(textototal);
                cell = new PdfPCell(phr);cell.HorizontalAlignment = Element.ALIGN_RIGHT;teibolTotales.AddCell(cell);

                cell = new PdfPCell(teibolTotales);
                cell.Border = 0;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //teibol.AddCell(cell);
                

                teibolGrids.AddCell(cell);
                #endregion

                #region PRINT CREDITO RELATED
                CheckBox acredito = (CheckBox)gvNota.Rows[0].Cells[22].Controls[0];
                if (acredito.Checked)
                {
                    //teibol.AddCell(cell);
                    phr = new Phrase("CREDITO RELACIONADO  : " + gvNota.Rows[0].Cells[23].Text + " - "+ HttpContext.Current.Server.HtmlDecode(gvNota.Rows[0].Cells[1].Text).ToUpper());
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
                cell = PdfCreator.getCellWithFormat(HttpContext.Current.Server.HtmlDecode(gvNota.Rows[0].Cells[1].Text).ToUpper(), true, Element.ALIGN_CENTER, 7.0f + fAjusteLetra);
                cell.Border = 0;
                tabla.AddCell(cell);
                tabla.CompleteRow();
                document.Add(tabla);
                #endregion

                //print observaciones only when there is something to print
                String sObservaciones = HttpContext.Current.Server.HtmlDecode(gvNota.Rows[0].Cells[7].Text.Trim()).Trim();
                #region OBSERVACIONES
                if (sObservaciones.Length > 0)
                {
                    document.Add(new Paragraph(" "));
                    PdfPTable teibolComments = new PdfPTable(1);
                    teibolComments.WidthPercentage = 100;
                    teibolComments.HorizontalAlignment = Element.ALIGN_LEFT;
                    teibolComments.AddCell(getCellWithFormat("Observaciones: " + sObservaciones, false, Element.ALIGN_JUSTIFIED, 8.0f));
                    document.Add(teibolComments);
                }
                #endregion

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
                sFooter += " Realizada por: " + HttpContext.Current.Server.HtmlDecode(gvNota.Rows[0].Cells[24].Text).ToUpper();
                sFooter += " # Nota de Venta: " + gvNota.Rows[0].Cells[0].Text;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, sFooter, (float)Utils.conviertedecmsapoints(1), document.PageSize.Height - (document.PageSize.Height - (float)Utils.conviertedecmsapoints(1)), 0);
                cb.EndText();

                
                
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
            //document.close();


            return ms.GetBuffer();
        }
        
        public static PdfPTable GridViewToPdfPTable(ref GridView gvGridView)
        {
            float[] fColSize = null;
            return PdfCreator.GridViewToPdfPTable(ref gvGridView, -1.0f, fColSize);
        }
        public static PdfPTable GridViewToPdfPTable(ref GridView gvGridView, float fWidthPercentage)
        {
            float[] fColSize = null;
            return PdfCreator.GridViewToPdfPTable(ref gvGridView, fWidthPercentage, fColSize);
        }
        public static PdfPTable GridViewToPdfPTable(ref GridView gvGridView, float fWidthPercentage, float [] fColSize)
        {
            return PdfCreator.GridViewToPdfPTable(ref gvGridView, fWidthPercentage, fColSize, 0);
        }
        public static PdfPTable GridViewToPdfPTable(ref GridView gvGridView, float fWidthPercentage, float [] fColSize, float fAjusteLetra)
        {
            return GridViewToPdfPTable(ref gvGridView, fWidthPercentage, fColSize, fAjusteLetra, false);
        }

        public static PdfPTable GridViewToPdfPTable(ref GridView gvGridView, float fWidthPercentage, float [] fColSize, float fAjusteLetra, bool bMerge)
        {
            gvGridView.AllowPaging = false;
           // gvGridView.DataBind();

            int iVisCols = 0;
            foreach(DataControlField col in gvGridView.Columns)
            {
                iVisCols += col.Visible ? 1 : 0;
            }
            //Create a table
            PdfPTable table = new PdfPTable(iVisCols);
            if (fWidthPercentage> 0)
            {
                table.WidthPercentage = fWidthPercentage;
            }

            //table.Cellpadding = 5;
            //Set the column widths
            float fTotalColSize = 0;
            for(int j = 0; fColSize != null && j < gvGridView.Columns.Count; j++)
            {
                fTotalColSize += gvGridView.Columns[j].Visible? fColSize[j] : 0;
            }
            int[] widths = new int[iVisCols];
            int iWidth = 0;
            for (int x = 0; x < gvGridView.Columns.Count; x++)
            {
                if (gvGridView.Columns[x].Visible)
                {
                    widths[iWidth++] = fColSize == null? 0: (int)(fColSize[x] * fWidthPercentage / fTotalColSize);  //(int)gvGridView.Columns[x].ItemStyle.Width.Value;
                    string cellText = gvGridView.Columns[x] != null && gvGridView.Columns[x].HeaderText != null ? HttpContext.Current.Server.HtmlDecode(gvGridView.Columns[x].HeaderText) : "";

                    PdfPCell cell = PdfCreator.getCellWithFormat(cellText, true, Element.ALIGN_LEFT, new BaseColor(gvGridView.HeaderStyle.ForeColor), 7.5f + fAjusteLetra);
                    cell.BackgroundColor = new BaseColor(gvGridView.HeaderStyle.BackColor);
                        //new iTextSharp.text.Cell(new Paragraph(cellText, font));
                    //cell.BackgroundColor = new BaseColor(gvGridView.Columns[x].ItemStyle.BackColor);
                    table.AddCell(cell);   
                }
            }
            /*table.wi*/
            if (fColSize != null)
            {
                table.SetWidths(widths);
            }
            //Transfer rows from GridView to table
            for (int i = 0; i < gvGridView.Rows.Count; i++)
            {
              
                    if (gvGridView.Rows[i].RowType == DataControlRowType.DataRow)
                    {
                        for (int j = 0; j < gvGridView.Columns.Count; j++)
                        {
                            while (j < gvGridView.Columns.Count && gvGridView.Columns[j].Visible == false)
                            {
                                j++;
                            }
                            if (j >= gvGridView.Columns.Count)
                            {
                                continue;
                            }
                            string cellText = HttpContext.Current.Server.HtmlDecode(gvGridView.Rows[i].Cells[j].Text.ToUpper());
                            //iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);
                            int Align = 0;
                            switch (gvGridView.Columns[j].ItemStyle.HorizontalAlign)
                            {
                                case HorizontalAlign.Center:
                                    Align = Element.ALIGN_CENTER;
                                    break;
                                case HorizontalAlign.Right:
                                    Align = Element.ALIGN_RIGHT;
                                    break;
                                case HorizontalAlign.Left:
                                default:
                                    Align = Element.ALIGN_LEFT;
                                    break;
                            }
                            PdfPCell cell = PdfCreator.getCellWithFormat(cellText, false, Align, 7.5f + fAjusteLetra);
                            cell.BackgroundColor = new BaseColor(gvGridView.RowStyle.BackColor);
                            cell.Rowspan = gvGridView.Rows[i].Cells[j].RowSpan > 0 ? gvGridView.Rows[i].Cells[j].RowSpan : 1;
                            //Set Color of Alternating row
                            if (i % 2 != 0)
                            {
                                cell.BackgroundColor = new BaseColor(gvGridView.AlternatingRowStyle.BackColor);
                            }
                            if (bMerge && i > 0 && j == 0)
                            {
                                String sCellAnt = HttpContext.Current.Server.HtmlDecode(gvGridView.Rows[i - 1].Cells[j].Text.ToUpper());
                                if (sCellAnt != cellText)
                                {
                                    table.AddCell(cell);
                                }
                            }
                            else
                            {
                                table.AddCell(cell);
                            }
                        }
                    }
                
            }
            //METEMOS UNA ROW 
           if(gvGridView.ShowFooter && gvGridView.FooterRow!=null){ //SI TIENE FOOTER LO IMPRIMIMOS
               bool primeracolumn = true;
                for (int j = 0; j < gvGridView.Columns.Count; j++)
                    {
                        while (j < gvGridView.Columns.Count && gvGridView.Columns[j].Visible == false)
                        {
                            j++;
                        }
                        if (j >= gvGridView.Columns.Count)
                        {
                            continue;
                        }
                        string cellText = HttpContext.Current.Server.HtmlDecode(gvGridView.FooterRow.Cells[j].Text);
                        //iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);
                        int Align = 0;
                        switch (gvGridView.Columns[j].ItemStyle.HorizontalAlign)
                        {
                            case HorizontalAlign.Center:
                                Align = Element.ALIGN_CENTER;
                                break;
                            case HorizontalAlign.Right:
                                Align = Element.ALIGN_RIGHT;
                                break;
                            case HorizontalAlign.Left:
                            default:
                                Align = Element.ALIGN_LEFT;
                                break;
                        }
                        
                        if(primeracolumn)
                        {
                            cellText = "TOTALES";
                            PdfPCell cell = PdfCreator.getCellWithFormat(cellText, false, Align, 7.5f + fAjusteLetra);
                            cell.BackgroundColor = new BaseColor(gvGridView.RowStyle.BackColor);
                            table.AddCell(cell);
                            primeracolumn = false;
                        }
                        else
                        {
                            PdfPCell cell = PdfCreator.getCellWithFormat(cellText, false, Align, 7.5f);
                            cell.BackgroundColor = new BaseColor(gvGridView.RowStyle.BackColor);
                            table.AddCell(cell);
                            
                        }
                       
                    }
                }

            
            return table;
        }

        public static PdfPCell getCellWithFormat(String sText, Boolean bBold, int Align, float fSize)
        {
            return PdfCreator.getCellWithFormat(sText, bBold, Align, new BaseColor(System.Drawing.Color.Black), fSize);
        }
        public static PdfPCell getCellWithFormat( String sText, Boolean bBold, int Align )
        {
            return PdfCreator.getCellWithFormat(sText, bBold, Align, new BaseColor(System.Drawing.Color.Black), 4.5f + 3);
        }
        public static PdfPCell getCellWithFormat(String sText, Boolean bBold, int Align, BaseColor FontColor)
        {
            return PdfCreator.getCellWithFormat(sText, bBold, Align, FontColor, -1);
        }
        public static PdfPCell getCellWithFormat(String sText, Boolean bBold, int Align, BaseColor FontColor, float fSize)
        {
            if (sText == "&NBSP;")
            {
                sText = " ";
            }
            sText = HttpContext.Current.Server.HtmlDecode(sText);
            Font font = new Font(Font.FontFamily.TIMES_ROMAN, (float)(fSize> 0? fSize: 4.5 + 3), (bBold? Font.BOLD: Font.NORMAL));
            font.Color = FontColor;
            PdfPCell celda = new PdfPCell(new Phrase(sText,font));
            celda.HorizontalAlignment = Align;
            return celda;
        }
        public static string printGridView(string TitulodeReporte, GridView lista, float[] anchodecolumnas, orientacionPapel orientacionpapel, tamañoPapel tamañopapel)
        {

            bRet = false;

            //Creates an instance of the iTextSharp.text.Document-object:
            Document document = inicializadocumento(tamañopapel, orientacionpapel);
            String pathArchivo = Path.GetTempFileName();
            //             FileStream fileStream;
            //             fileStream=File.OpenWrite(pathArchivo)
            //             
            try
            {

                //Creates a Writer that listens to this document and writes the document to the Stream of your choice:

                PdfWriter.GetInstance(document, new FileStream(pathArchivo, FileMode.Create));
                //AGREGAMOS EL FOOTER
                /*string footertext;
                footertext = "PÁGINA {0} DE {1}";
                HeaderFooter footer = new HeaderFooter(new Phrase("PAGINA: "), true);
                footer.Border = Rectangle.NO_BORDER;
                footer.Alignment = Element.ALIGN_RIGHT;
                document.Footer = footer;*/
                document.SetMargins(50, 50, 50, 50);
                //Opens the document:
                document.Open();


                //LOGO
                iTextSharp.text.Image logo;
                logo = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/imagenes/IPROJALmedium.jpg"));
                logo.Alignment = Element.ALIGN_LEFT;
                int numcolumnas;
                numcolumnas = lista.Columns.Count;

                //INICIAMOS HEADER
                PdfPTable table = new PdfPTable(anchodecolumnas);
                float[] anchodecolumnasdelheader = new float[] { 80, 20 };
                PdfPTable tableHeader = new PdfPTable(anchodecolumnasdelheader);
                PdfPCell cell = new PdfPCell();
                PdfPCell celltableHeader = new PdfPCell();
                table.WidthPercentage = 100;

                tableHeader.WidthPercentage = 100;
                table.HeaderRows = 2;

                cell.BorderColor = BaseColor.WHITE;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = numcolumnas;

                celltableHeader.Colspan = 1;
                celltableHeader.BorderColor = BaseColor.WHITE;
                celltableHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                celltableHeader.Phrase = new Phrase("INTEGRADORA DE PRODUCTORES DE JALISCO SPR DE RL", FontFactory.GetFont(FontFactory.HELVETICA, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK));

                tableHeader.AddCell(celltableHeader);
                celltableHeader.AddElement(logo);
                tableHeader.AddCell(celltableHeader);
                celltableHeader.Phrase = new Phrase(TitulodeReporte, FontFactory.GetFont(FontFactory.HELVETICA, 14, BaseColor.BLACK));
                tableHeader.AddCell(celltableHeader);
                celltableHeader.Phrase = new Phrase(Utils.getNowFormattedDateNormal(), FontFactory.GetFont(FontFactory.HELVETICA, 14, BaseColor.BLACK));
                tableHeader.AddCell(celltableHeader);
                cell.AddElement(tableHeader);
                table.AddCell(cell);
                cell.Colspan = 1;
                cell.BorderWidth = 0.5f;
                cell.BorderColor = BaseColor.BLACK;
                for (int i = 1; i < lista.Columns.Count; i++)
                {
                    if (lista.Columns[i].Visible)
                    {
                        cell.Phrase = new Phrase(lista.Columns[i].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                        table.AddCell(cell);
                    }
                }



                //                 
                //                 cell.Colspan = System.Convert.ToInt32(anchodecolumnas.LongLength * .8);
                //                 cell.Phrase = new Phrase(TitulodeReporte, FontFactory.GetFont(FontFactory.HELVETICA, 14, BaseColor.BLACK));
                //                 table.AddCell(cell);
                //                 cell.Colspan = System.Convert.ToInt32(anchodecolumnas.LongLength * .2);
                //                 cell.Phrase = new Phrase(Utils.converttoshortFormatfromdbFormat(Utils.Now.ToString()), FontFactory.GetFont(FontFactory.HELVETICA, 14, BaseColor.BLACK));
                //                 table.AddCell(cell);
                ///INICIAMOS CUERPO
                for (int i = 0; i < lista.Rows.Count; i++)
                {
                    for (int j = 1; j < lista.Columns.Count; j++)
                    {

                        if (lista.Columns[j].Visible)
                        {
                            if (lista.Rows[i].Cells[j].Controls.Count > 0)
                            {
                                string control;
                                control = lista.Rows[i].Cells[j].Controls[0].ToString();
                                if (control == "System.Web.UI.WebControls.CheckBox")
                                {
                                    CheckBox check;
                                    check = (CheckBox)lista.Rows[i].Cells[j].Controls[0];
                                    if (check.Checked)
                                    {
                                        cell.Phrase = new Phrase("SI", FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK));
                                        table.AddCell(cell);
                                    }
                                    else
                                    {
                                        cell.Phrase = new Phrase("NO", FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK));
                                        table.AddCell(cell);
                                    }
                                }
                            }
                            else
                            {

                                cell.Phrase = new Phrase(System.Web.HttpUtility.HtmlDecode(lista.Rows[i].Cells[j].Text), FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK));
                                table.AddCell(cell);
                            }
                        }
                    }
                }

                document.Add(table);






            }
            catch(Exception e)
            {
                Logger.Instance.LogException(Logger.typeUserActions.PRINT, "Error printing gridview", "NONE", ref e);
            }

            finally
            {
                document.Close();
            }


            return pathArchivo;
        }

        public static byte [] printCheque(int movID, orientacionPapel orientacionpapel, tamañoPapel tamañopapel,float ajustex, float ajustey, int userID)
        {
            bRet = false;

            //Creates an instance of the iTextSharp.text.Document-object:
            Document document = inicializadocumento(tamañopapel, orientacionpapel);
            //String pathArchivo = Path.GetTempFileName();
            //             FileStream fileStream;
            //             fileStream=File.OpenWrite(pathArchivo)
            //   
            PdfWriter pdfw;
            PdfContentByte cb;
            BaseFont fnt;
            float XposRecibe, YposRecibe, XposFecha, YposFecha, XposCantidad, YposCantidad, XposCantidadLetra, YposCantidadLetra, XposConcepto, YposConcepto;
            XposRecibe = YposRecibe =  XposFecha =  YposFecha =  XposCantidad = YposCantidad = XposCantidadLetra =  YposCantidadLetra= XposConcepto =  YposConcepto= 0;
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            string query = "SELECT     CuentasDeBanco.bancoID, MovimientosCuentasBanco.fecha, MovimientosCuentasBanco.fechaCheque, MovimientosCuentasBanco.cargo, MovimientosCuentasBanco.nombre as chequeNombre, ";
            query += " MovimientosCuentasBanco.chequeQuienrecibe, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.numCheque, MovimientosCuentasBanco.cuentaID ";
                   query += " FROM         MovimientosCuentasBanco INNER JOIN ";
                   query += " CuentasDeBanco ON MovimientosCuentasBanco.cuentaID = CuentasDeBanco.cuentaID INNER JOIN ";
                   query += " ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID WHERE MovimientosCuentasBanco.movbanID = @movbanID";

            SqlCommand cmdselcamposcheques = new SqlCommand(query, conGaribay);
            SqlConnection conGaribay2 = new SqlConnection(myConfig.ConnectionInfo);
            string query2 = " SELECT     CamposCheque.campo, PosCamposCheque.posX, PosCamposCheque.posY FROM         CamposCheque INNER JOIN PosCamposCheque ON CamposCheque.campoChequeID = PosCamposCheque.campoChequeID where PosCamposCheque.cuentaID = @cuentaID";

            SqlCommand cmdselposcampos = new SqlCommand(query2, conGaribay2);
            conGaribay2.Open();
            conGaribay.Open();
            MemoryStream ms = new MemoryStream(); 
            //FileStream archivo = new FileStream(pathArchivo, FileMode.Create);
            try
            {
                cmdselcamposcheques.Parameters.Add("@movbanID", SqlDbType.Int).Value = movID;
                SqlDataReader readcamposcheque = cmdselcamposcheques.ExecuteReader();
                readcamposcheque.Read();
                string bancoID = readcamposcheque[0].ToString();
                cmdselposcampos.Parameters.Add("@cuentaID", SqlDbType.Int).Value = int.Parse(readcamposcheque["cuentaID"].ToString());
                SqlDataReader readposcampos = cmdselposcampos.ExecuteReader();
                try
                {

                    String sTipoLetra = myConfig.GetStringConfig("CHEQUEFONT", myConfig.CATEGORIA.CHEQUES, "TIMES");
//                     bool bBool = myConfig.GetBoolConfig("BOLDFONT", myConfig.CATEGORIA.CHEQUES, false);
//                     
//                     if (bBool)
//                     {
//                         //further versions
//                         //fnt.SetStyle("Bold");
//                     }
                    fnt = FontFactory.GetFont(sTipoLetra).BaseFont;
                    if(fnt==null){
                        FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                        myConfig.SetStringConfig("CHEQUEFONT", myConfig.CATEGORIA.CHEQUES, "ARIAL");
                        sTipoLetra=myConfig.GetStringConfig("CHEQUEFONT", myConfig.CATEGORIA.CHEQUES, "ARIAL");
                        fnt = FontFactory.GetFont(sTipoLetra).BaseFont;
                    }

//                     switch(sTipoLetra){
//                        case "COURIER":
//                              fnt = FontFactory.GetFont(iTextSharp.text.FontFactory.COURIER_BOLD, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.BOLD).BaseFont;
//                         break;
//                         case "HELVETICA":
//                              fnt = FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.BOLD).BaseFont;
//                         break;
//                         default: 
//                              fnt = FontFactory.GetFont(iTextSharp.text.FontFactory.TIMES_BOLD, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.BOLD).BaseFont;
//                         break;
// 
//                     }
                    pdfw = PdfWriter.GetInstance(document, ms);
                    document.Open();
                    document.SetMargins(0, 0, 0, 0);
                    cb = pdfw.DirectContent;
                    //document.SetMargins(50, 50, 50, 50);
                    document.NewPage();
                    cb.BeginText();
                    cb.SetColorFill(BaseColor.BLACK);
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CHEQUES, Logger.typeUserActions.PRINT, userID, "EL USUARIO IMPRIMIO EL CHEQUE: " + readcamposcheque["numCheque"].ToString());
               
                    while (readposcampos.Read())
                    {
                        cb.SetFontAndSize(fnt, 12f);
                        
                        switch (readposcampos[0].ToString())
                        {

                            case "NOMBRE":
                                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, 
                                    readcamposcheque[4].ToString().ToUpper(), 
                                    (float)Utils.conviertedecmsapoints(float.Parse(readposcampos[1].ToString())) + (float)Utils.conviertedecmsapoints(ajustex),
                                    document.PageSize.Height - (float)Utils.conviertedecmsapoints(double.Parse(readposcampos[2].ToString()))  -((float)Utils.conviertedecmsapoints(ajustey)), 
                                    0);

                                break;
                            case "FECHA":
                                DateTime fecha = DateTime.Parse(Utils.converttoshortFormatfromdbFormat(readcamposcheque[2].ToString()));
                                string fechillalarga = Utils.UppercaseFirst(fecha.ToString("dddd, dd")) + " de " + Utils.UppercaseFirst(fecha.ToString("MMMM, yyyy")) + ".";
                               // BaseFont fnt2 = FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.BOLD).BaseFont;
                                cb.SetFontAndSize(fnt, 10f);
                                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT,fechillalarga,
                                    (float)Utils.conviertedecmsapoints(double.Parse(readposcampos[1].ToString())) + (float)Utils.conviertedecmsapoints(ajustex),
                                    document.PageSize.Height - (float)Utils.conviertedecmsapoints(double.Parse(readposcampos[2].ToString())) - ((float)Utils.conviertedecmsapoints(ajustey)), 
                                    0);
                                break;

                            case "CANTIDAD":
                                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, 
                                    string.Format("{0:N}",double.Parse(readcamposcheque[3].ToString())),
                                    (float)Utils.conviertedecmsapoints(double.Parse(readposcampos[1].ToString())) + (float)Utils.conviertedecmsapoints(ajustex),
                                    (float)document.PageSize.Height - (float)Utils.conviertedecmsapoints(double.Parse(readposcampos[2].ToString())) - ((float)Utils.conviertedecmsapoints(ajustey)), 
                                    0);
                                break;


                            case "CANTIDALETRA":
                                {
                                    String sTemp = Utils.NumeroALetras(readcamposcheque[3].ToString());
                                    sTemp = sTemp.Replace(" 0/100", " 00/100");
                                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, 
                                        sTemp,
                                        (float)Utils.conviertedecmsapoints(double.Parse(readposcampos[1].ToString())) + (float)Utils.conviertedecmsapoints(ajustex),
                                        (float)document.PageSize.Height - (float)Utils.conviertedecmsapoints(double.Parse(readposcampos[2].ToString())) - ((float)Utils.conviertedecmsapoints(ajustey)), 0);
                                }
                                break;

                            case "CONCEPTO":
                                //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, readcamposcheque[5].ToString().ToUpper(), Utils.conviertedecmsapoints(float.Parse(readposcampos[1].ToString())) + Utils.conviertedecmsapoints(ajustex), document.PageSize.Height - Utils.conviertedecmsapoints(float.Parse(readposcampos[2].ToString())) - (Utils.conviertedecmsapoints(ajustey)), 0);
                                break;
                        }
                    }
                    cb.EndText();
                    pdfw.Flush();
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.PRINT, "Error printing cheque", "NONE", ref ex);
                }
                finally
                {
                  document.Close();
                }
            }
            catch (Exception ioe)
            {
                Logger.Instance.LogException(Logger.typeUserActions.PRINT, "Error printing cheque", "NONE", ref ioe);
            }

            finally
            {
                conGaribay.Close();
                conGaribay2.Close();
            }
            return ms.GetBuffer();


        }

        //PRINT CHEQUE FROM FORM
        public static byte [] printCheque(orientacionPapel orientacionpapel, tamañoPapel tamañopapel, float ajustex, float ajustey, String sNombre, String sFecha, String sCantidad, String sConcepto, String sBancoID)
        {
            bRet = false;
            //Creates an instance of the iTextSharp.text.Document-object:
            Document document = inicializadocumento(tamañopapel, orientacionpapel);
            //             FileStream fileStream;
            //             fileStream=File.OpenWrite(pathArchivo)
            //   
            PdfWriter pdfw;
            PdfContentByte cb;
            BaseFont fnt;
            float XposRecibe, YposRecibe, XposFecha, YposFecha, XposCantidad, YposCantidad, XposCantidadLetra, YposCantidadLetra, XposConcepto, YposConcepto;
            XposRecibe = YposRecibe = XposFecha = YposFecha = XposCantidad = YposCantidad = XposCantidadLetra = YposCantidadLetra = XposConcepto = YposConcepto = 0;
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);

            //SqlCommand cmdselcamposcheques = new SqlCommand(query, conGaribay);
            SqlConnection conGaribay2 = new SqlConnection(myConfig.ConnectionInfo);
            string query2 = " SELECT     CamposCheque.campo, PosCamposCheque.posX, PosCamposCheque.posY ";
            query2 += " FROM         CamposCheque INNER JOIN";
            query2 += " PosCamposCheque ON CamposCheque.campoChequeID = PosCamposCheque.campoChequeID where PosCamposCheque.bancoID = @bancoID";

            SqlCommand cmdselposcampos = new SqlCommand(query2, conGaribay2);
            conGaribay2.Open();
            conGaribay.Open();
            MemoryStream ms = new MemoryStream();            
            try
            {
                //cmdselcamposcheques.Parameters.Add("@movbanID", SqlDbType.Int).Value = movID;
                //SqlDataReader readcamposcheque = cmdselcamposcheques.ExecuteReader();
                //readcamposcheque.Read();
                //string bancoID = readcamposcheque[0].ToString();
                cmdselposcampos.Parameters.Add("@bancoID", SqlDbType.Int).Value = int.Parse(sBancoID);
                SqlDataReader readposcampos = cmdselposcampos.ExecuteReader();
                try
                {
                    fnt = FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.BOLD).BaseFont;
                    pdfw = PdfWriter.GetInstance(document, ms);
                    document.Open();
                    document.SetMargins(0, 0, 0, 0);
                    cb = pdfw.DirectContent;
                    //document.SetMargins(50, 50, 50, 50);
                    document.NewPage();
                    cb.BeginText();
                    cb.SetColorFill(BaseColor.BLACK);
                    float px, py;
                    while (readposcampos.Read())
                    {
                        cb.SetFontAndSize(fnt, 12f);
                        px = (float)Utils.conviertedecmsapoints(double.Parse(readposcampos[1].ToString())) + (float)Utils.conviertedecmsapoints(ajustex);
                        py = (float)document.PageSize.Height - (float)Utils.conviertedecmsapoints(double.Parse(readposcampos[2].ToString())) - ((float)Utils.conviertedecmsapoints(ajustey));
                        switch (readposcampos[0].ToString())
                        {

                            case "NOMBRE":
                                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, sNombre.ToUpper(), px, py, 0);

                                break;
                            case "FECHA":
                                DateTime fecha = DateTime.Parse(Utils.converttoshortFormatfromdbFormat(sFecha));
                                string fechillalarga = Utils.UppercaseFirst(fecha.ToString("dddd, dd")) + " de " + Utils.UppercaseFirst(fecha.ToString("MMMM, yyyy")) + ".";
                                BaseFont fnt2 = FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.BOLD).BaseFont;
                                cb.SetFontAndSize(fnt2, 10f);
                                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, fechillalarga, px,py, 0);
                                break;

                            case "CANTIDAD":
                                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, string.Format("{0:N2}", double.Parse(sCantidad)), px, py, 0);
                                break;


                            case "CANTIDALETRA":
                                {
                                    String sTemp = Utils.NumeroALetras(sCantidad);
                                    sTemp = sTemp.Replace(" 0/100", " 00/100");
                                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, sTemp.ToUpper(), px, py, 0);
                                }
                                break;

                            case "CONCEPTO":
                                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, sConcepto.ToUpper(), px,py, 0);
                                break;
                        }
                    }
                    cb.EndText();
                    pdfw.Flush();
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.PRINT, "Error printing cheque", "NONE", ref ex);
                }
                finally
                {
                    document.Close();
                }
            }
            catch (Exception ioe)
            {
                Logger.Instance.LogException(Logger.typeUserActions.PRINT, "Error printing cheque", "NONE", ref ioe);
            }

            finally
            {
                conGaribay.Close();
                conGaribay2.Close();
            }
            return ms.GetBuffer();


        }
        public static string printLista(string title, GridView gvReport, orientacionPapel orientacion, tamañoPapel tamano)
        {
            String pathArchivo = Path.GetTempFileName();
            FileStream archivo = new FileStream(pathArchivo, FileMode.Create);
            int noOfColumns = 0, noOfRows = 0;
          
                noOfColumns = gvReport.Columns.Count;
                noOfRows = gvReport.Rows.Count;

            float HeaderTextSize = 8;
            float ReportNameSize = 10;
            float ReportTextSize = 8;
            //float ApplicationNameSize = 7;

            Document document;

            document = inicializadocumento(tamano, orientacion);
            iTextSharp.text.pdf.PdfPTable mainTable = new iTextSharp.text.pdf.PdfPTable(noOfColumns);
            mainTable.HeaderRows = 4;
            iTextSharp.text.pdf.PdfPTable headerTable = new iTextSharp.text.pdf.PdfPTable(new float[] { 80, 20 });

            // Creates a phrase to hold the application name at the left hand side of the header.
            Phrase phApplicationName = new Phrase("INTEGRADORA DE PRODUCTORES DE JALISCO SPR DE RL", FontFactory.GetFont(FontFactory.HELVETICA, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK));

            //Creates a phrase to hold the date
            Phrase phDate = new Phrase(Utils.getNowFormattedDateNormal(), FontFactory.GetFont(FontFactory.HELVETICA, 14, BaseColor.BLACK));

            // Creates a PdfPCell which accepts a phrase as a parameter.
            PdfPCell clApplicationName = new PdfPCell(phApplicationName);
            
            clApplicationName.HorizontalAlignment = Element.ALIGN_CENTER;
            // Sets the border of the cell to zero.
            clApplicationName.Border = PdfPCell.NO_BORDER;

            PdfPCell clDate = new PdfPCell(phDate);
            clDate.Colspan = 2;
            clDate.HorizontalAlignment = Element.ALIGN_RIGHT;
            clDate.Border = PdfPCell.NO_BORDER;

            iTextSharp.text.Image logo;
            logo = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/imagenes/IPROJALsmall.jpg"));
            logo.Alignment = Element.ALIGN_LEFT;
            PdfPCell cllogo  = new PdfPCell(logo);

            // Sets the Horizontal Alignment of the PdfPCell to right.
            cllogo.HorizontalAlignment = Element.ALIGN_RIGHT;
            // Sets the border of the cell to zero.
            logo.Border = PdfPCell.NO_BORDER;

            // Adds the cell which holds the application name to the headerTable.
            headerTable.AddCell(clApplicationName);
            // Adds the cell which holds the date to the headerTable.
            headerTable.AddCell(logo);
            headerTable.AddCell(clDate);
            // Sets the border of the headerTable to zero.
            headerTable.DefaultCell.Border = PdfPCell.NO_BORDER;

            // Creates a PdfPCell that accepts the headerTable as a parameter and then adds that cell to the main PdfPTable.
            PdfPCell cellHeader = new PdfPCell(headerTable);
            cellHeader.Border = PdfPCell.NO_BORDER;
            // Sets the column span of the header cell to noOfColumns.
            cellHeader.Colspan = noOfColumns;
            // Adds the above header cell to the table.
            mainTable.AddCell(cellHeader);

            // Creates a phrase which holds the file name.
            Phrase phHeader = new Phrase(title, FontFactory.GetFont(FontFactory.HELVETICA, ReportNameSize, iTextSharp.text.Font.BOLD));
            PdfPCell clHeader = new PdfPCell(phHeader);
            clHeader.Colspan = noOfColumns;
            clHeader.Border = PdfPCell.NO_BORDER;
            clHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            mainTable.AddCell(clHeader);

            // Creates a phrase for a new line.
            Phrase phSpace = new Phrase("\n");
            PdfPCell clSpace = new PdfPCell(phSpace);
            clSpace.Border = PdfPCell.NO_BORDER;
            clSpace.Colspan = noOfColumns;
            mainTable.AddCell(clSpace);

            // Sets the gridview column names as table headers.
            for (int i = 0; i < noOfColumns; i++)
            {
                Phrase ph = null;
                ph = new Phrase(gvReport.Columns[i].HeaderText, FontFactory.GetFont(FontFactory.HELVETICA, HeaderTextSize, iTextSharp.text.Font.BOLD));
        
                mainTable.AddCell(ph);
            }

            // Reads the gridview rows and adds them to the mainTable
            for (int rowNo = 0; rowNo < noOfRows; rowNo++)
            {
                for (int columnNo = 0; columnNo < noOfColumns; columnNo++)
                {
                    if (gvReport.AutoGenerateColumns)
                    {
                        string s = gvReport.Rows[rowNo].Cells[columnNo].Text.Trim();
                        Phrase ph = new Phrase(s, FontFactory.GetFont(FontFactory.HELVETICA, ReportTextSize, iTextSharp.text.Font.NORMAL));
                        mainTable.AddCell(ph);
                    }
                    else
                    {
                        if (gvReport.Columns[columnNo] is TemplateField)
                        {
                            DataBoundLiteralControl lc = gvReport.Rows[rowNo].Cells[columnNo].Controls[0] as DataBoundLiteralControl;
                            string s = lc.Text.Trim();
                            Phrase ph = new Phrase(s, FontFactory.GetFont(FontFactory.HELVETICA, ReportTextSize, iTextSharp.text.Font.NORMAL));
                            mainTable.AddCell(ph);
                        }
                        else
                        {
                            string s = gvReport.Rows[rowNo].Cells[columnNo].Text.Trim();
                            Phrase ph = new Phrase(s, FontFactory.GetFont(FontFactory.HELVETICA, ReportTextSize, iTextSharp.text.Font.NORMAL));
                            mainTable.AddCell(ph);
                        }
                    }
                }

                // Tells the mainTable to complete the row even if any cell is left incomplete.
                mainTable.CompleteRow();
            }

            // Gets the instance of the document created and writes it to the output stream of the Response object.
            PdfWriter.GetInstance(document, archivo);

            // Creates a footer for the PDF document.
            /*HeaderFooter pdfFooter = new HeaderFooter(new Phrase(), true);
            pdfFooter.Alignment = Element.ALIGN_CENTER;
            pdfFooter.Border = iTextSharp.text.Rectangle.NO_BORDER;

            // Sets the document footer to pdfFooter.
            document.Footer = pdfFooter;*/
            // Opens the document.
            document.Open();
            // Adds the mainTable to the document.
            document.Add(mainTable);
            // Closes the document.
            document.Close();

            return pathArchivo;
        }
        
        
        public static Byte[] PrintEstadoDeCuenta(String sCredito,ref DetailsView dvEdoGral, ref GridView gvDetalle, ref GridView gvSeguro, ref GridView gvIntereses, int sCreditoID)
        {
            Byte[] res = null;
            MemoryStream ms = new MemoryStream();
            Document document = inicializadocumento(tamañoPapel.CARTA, orientacionPapel.VERTICAL);
            float fMargin = (float)Utils.conviertedecmsapoints(1);
            document.SetMargins(fMargin,fMargin,fMargin,fMargin);
            try
            {
                PdfWriter pdfW = PdfWriter.GetInstance(document,ms);
                document.Open();
                BaseFont fnt;
                fnt = FontFactory.GetFont(iTextSharp.text.FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL).BaseFont;

                document.Add(new Paragraph(" "));

                PdfPTable teibol = new PdfPTable(new float[2] { 0.5f, 0.5f });
                teibol.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell celda = getCellWithFormat("ESTADO DE CUENTA DE CREDITO: " + sCredito,true, Element.ALIGN_CENTER,12.0f);
                celda.Colspan = 2;
                teibol.AddCell(celda);

                String sGarantias = "NO DEJÓ GARANTIAS";
#region Productor Data
                string sql = "SELECT     Productores.poblacion + ' ' + Productores.municipio + ' ' + Estados.estado AS Poblacion, Productores.telefono, Productores.domicilio, "
                + " Solicitudes.Descripciondegarantias FROM         Productores INNER JOIN "
                + " Creditos ON Productores.productorID = Creditos.productorID INNER JOIN "
                + " Estados ON Productores.estadoID = Estados.estadoID LEFT OUTER JOIN "
                + " Solicitudes ON Creditos.creditoID = Solicitudes.creditoID where Creditos.creditoID = @creditoID ";
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdSacadata = new SqlCommand(sql,conGaribay);
                try
                {
                    conGaribay.Open();
                    cmdSacadata.Parameters.Add("@creditoID", SqlDbType.Int).Value = sCreditoID;
                    SqlDataReader reader = cmdSacadata.ExecuteReader();
                    if (reader.Read())
                    {
                        celda = new PdfPCell();
                        celda.HorizontalAlignment = Element.ALIGN_MIDDLE;
                        celda.VerticalAlignment = Element.ALIGN_MIDDLE;
                        PdfPTable teibolDataProd = new PdfPTable(new float[2]{ 0.3f, 0.7f });
                        teibolDataProd.HorizontalAlignment = Element.ALIGN_LEFT;
                        teibolDataProd.WidthPercentage = 100;
                        teibolDataProd.AddCell(getCellWithFormat("Direccion:", true, Element.ALIGN_LEFT));
                        teibolDataProd.AddCell(getCellWithFormat(reader["domicilio"].ToString(), false, Element.ALIGN_LEFT));
                        teibolDataProd.AddCell(getCellWithFormat("Poblacion:", true, Element.ALIGN_LEFT));
                        teibolDataProd.AddCell(getCellWithFormat(reader["Poblacion"].ToString(), false, Element.ALIGN_LEFT));
                        teibolDataProd.AddCell(getCellWithFormat("Telefono:", true, Element.ALIGN_LEFT));
                        teibolDataProd.AddCell(getCellWithFormat(reader["telefono"].ToString(), false, Element.ALIGN_LEFT));
                        teibolDataProd.AddCell(getCellWithFormat("Garantias:", true, Element.ALIGN_LEFT));
                        sGarantias = reader["Descripciondegarantias"].ToString();
                        //teibolDataProd.AddCell(getCellWithFormat(, false, Element.ALIGN_LEFT));
                        celda.AddElement(teibolDataProd);

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
                
                
#endregion
                PdfPTable teibolGarantias = new PdfPTable(1);
                teibolGarantias.AddCell(getCellWithFormat("GARANTIAS:", true, Element.ALIGN_LEFT, 7f));
                teibolGarantias.AddCell(getCellWithFormat(sGarantias, false, Element.ALIGN_JUSTIFIED, 6f));
                teibolGarantias.HorizontalAlignment = Element.ALIGN_LEFT;
                teibolGarantias.WidthPercentage = 100;
                celda.AddElement(teibolGarantias);
                celda.AddElement(GridViewToPdfPTable(ref gvSeguro, 100, new float[3] { 0.4f, 0.4f, 0.2f }, -2.5f));
                teibol.AddCell(celda);

                PdfPTable teibolTotales = new PdfPTable(new float[2] { 0.3f, 0.7f });
                for (int i = 1; i < dvEdoGral.Rows.Count ; i++)
                {
                    teibolTotales.AddCell(getCellWithFormat(dvEdoGral.Rows[i].Cells[0].Text, true, Element.ALIGN_LEFT));
                    teibolTotales.AddCell(getCellWithFormat(dvEdoGral.Rows[i].Cells[1].Text, false, Element.ALIGN_RIGHT));
                }
                celda = new PdfPCell();
                celda.HorizontalAlignment = Element.ALIGN_RIGHT;
                celda.VerticalAlignment = Element.ALIGN_TOP;
                celda.AddElement(teibolTotales);
                teibol.AddCell(celda);

                document.Add(teibol);
                int cols = gvIntereses.Columns.Count;
                document.Add(GridViewToPdfPTable(ref gvIntereses, 100, new float[12] { 0.7f, 0.6f, 1.3f, 0.9f, 0.8f, 0.4f, 0.8f, 0.8f, 0.8f, 0.8f, 1.1f, 0 }, -2.0f, true));

                document.Close();

                res = ms.GetBuffer();

            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.PRINT, "error printing estado de cuenta", ref ex);
            }
            finally
            {

            }
            return res;
        }
    }

 

}