using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
//using Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Core;
using System.IO;
using iTextSharp.text.pdf;
using System.Data.SqlClient;
using System.Data;
using iTextSharp.text;
using System.Globalization;


namespace Garibay
{
    class FormatosPdf : System.Web.UI.Page
    {
        /*      ESTA PAGINA LA EXLUÍMOS DE LA COMPRESIÓN   */
        #region Nota de compra proveedor formato
        public static string imprimeOrdendeCompraFormato(int ID, ref string path, ref string sError)
        {
            path = Path.GetTempFileName();
            FileStream fS = new FileStream(path, FileMode.Open);
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            string sqlQuery = "SELECT     ordenDeCompraFormato.empresaID, ordenDeCompraFormato.fecha, ordenDeCompraFormato.comprade, ordenDeCompraFormato.preciode, "
                + " ordenDeCompraFormato.entrega, Empresas.Empresa, Empresas.logoRelativePath, Empresas.RFC, Proveedores.Nombrecontacto, Proveedores.Nombre, "
                + " Proveedores.Direccion, Proveedores.Municipio, Proveedores.CP, Proveedores.Teléfono "
                + " FROM ordenDeCompraFormato INNER JOIN "
                + " Empresas ON ordenDeCompraFormato.empresaID = Empresas.empresaID INNER JOIN "
                + " Proveedores ON ordenDeCompraFormato.proveedorID = Proveedores.proveedorID "
                + " where ordenID = @orderID";
            SqlCommand cmdGaribay = new SqlCommand(sqlQuery, conGaribay);
            try
            {
                conGaribay.Open();
                cmdGaribay.Parameters.Add("@orderID", SqlDbType.Int).Value = ID;
                SqlDataReader rd = cmdGaribay.ExecuteReader();
                if (!rd.HasRows) { conGaribay.Close(); }
                PdfStamper ps = null;
                PdfReader r = new PdfReader(new RandomAccessFileOrArray(HttpContext.Current.Request.MapPath("/formatos/ORDEN_DE_COMPRA_FORMATO.pdf")), null);
                ps = new PdfStamper(r, fS);
                AcroFields af = ps.AcroFields;
                try
                {
                    if (rd.Read())
                    {
                        af.SetField("txtEmpresa", rd["Empresa"].ToString().ToUpper());
                        af.SetField("txtRFC", rd["RFC"].ToString().ToUpper());
                        af.SetField("txtProveedorContacto", rd["Nombrecontacto"].ToString().ToUpper());
                        af.SetField("txtProveedorNombre", rd["Nombre"].ToString().ToUpper());
                        af.SetField("txtProveedorDom", rd["Direccion"].ToString().ToUpper());
                        af.SetField("txtCiudad", rd["Municipio"].ToString().ToUpper());
                        af.SetField("txtProveedorCP", rd["CP"].ToString().ToUpper());
                        af.SetField("txtTel", rd["Teléfono"].ToString().ToUpper());
                        af.SetField("txtCompraDe", rd["comprade"].ToString().ToUpper());
                        af.SetField("txtPrecioDe", rd["preciode"].ToString().ToUpper());
                        af.SetField("txtEntrega", rd["entrega"].ToString().ToUpper());
                    }
                    ps.FormFlattening = true;
                    ps.Close();
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.PRINT, new BasePage().UserID, "NO SE PUDO IMPRIMIR LA ORDEN DE COMPRA No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, "FORMATOSPDF.CS");
                }
                finally { if (ps != null) { fS.Close(); ps.Close(); } }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.PRINT, new BasePage().UserID, "NO SE PUDO IMPRIMIR LA ORDEN DE COMPRA No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, "FORMATOSPDF.CS");
                sError = ex.Message;
            }
            finally
            {
                conGaribay.Close();
            }
            return path;



        }
        #endregion
        public static void stampImage(ref PdfReader reader, ref PdfStamper stamper, string fieldName, string imagePath)
        {
            AcroFields.FieldPosition photograph = stamper.AcroFields.GetFieldPositions(fieldName).ElementAt(0);
            Rectangle rect = new Rectangle(photograph.position.Left, photograph.position.Bottom, photograph.position.Right, photograph.position.Bottom);
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(HttpContext.Current.Request.MapPath(imagePath));
            img.ScaleToFit(rect.Width, rect.Height);
            img.SetAbsolutePosition(photograph.position.Left + (rect.Width -  img.ScaledWidth), photograph.position.Bottom + (rect.Height - img.ScaledHeight));
            PdfContentByte cb = stamper.GetOverContent(photograph.page);
            cb.AddImage(img);
            stamper.Close();

        }
        public static string imprimeOrdendeCarga(int ID, ref string path, ref string sError)
        {
            path = Path.GetTempFileName();
            FileStream fS = new FileStream(path, FileMode.Open);
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            string sqlQuery = "SELECT     Empresas.Empresa, OrdenesDeCarga.fecha, OrdenesDeCarga.chofer, OrdenesDeCarga.placas, OrdenesDeCarga.marca, OrdenesDeCarga.anio,  "+
                      " OrdenesDeCarga.color, OrdenesDeCarga.jaula, OrdenesDeCarga.origen, OrdenesDeCarga.producto, OrdenesDeCarga.presentacion, OrdenesDeCarga.bodega,  " +
                      " OrdenesDeCarga.ubicacion, OrdenesDeCarga.destino, OrdenesDeCarga.facturar_a, OrdenesDeCarga.observaciones, OrdenesDeCarga.storeTS, Users.Nombre, " + 
                      " OrdenesDeCarga.emailToSend, OrdenesDeCarga.emailCC, OrdenesDeCarga.emaildescription, Users_1.Nombre AS SentbyUser, " +
                      " Proveedores.Nombre AS ProveedorName, OrdenesDeCarga.ordenDeCargaID, Empresas.RFC, Empresas.logoRelativePath " +
                      " FROM                  OrdenesDeCarga INNER JOIN "+
                      " Users ON OrdenesDeCarga.userID = Users.userID INNER JOIN "+
                      " Proveedores ON OrdenesDeCarga.proveedorID = Proveedores.proveedorID INNER JOIN "+
                      " Empresas ON OrdenesDeCarga.empresaID = Empresas.empresaID LEFT OUTER JOIN "+
                      " Users AS Users_1 ON OrdenesDeCarga.emailsentby = Users_1.userID where ordenDeCargaID = @ordenDeCargaID ";

            SqlCommand cmdGaribay = new SqlCommand(sqlQuery, conGaribay);
            try
            {
                conGaribay.Open();
                cmdGaribay.Parameters.Add("@ordenDeCargaID",SqlDbType.Int).Value = ID;
                SqlDataReader rd= cmdGaribay.ExecuteReader();
                if (!rd.HasRows) { conGaribay.Close();  }
                 PdfStamper ps = null;
                 PdfReader r = new PdfReader(new RandomAccessFileOrArray(HttpContext.Current.Request.MapPath("/formatos/FormatoOrdendeCarga.pdf")), null);
                 ps = new PdfStamper(r, fS );
                 AcroFields af = ps.AcroFields;
                 try
                 {
                    if (rd.Read())

                    {
                        af.SetField("txtFecha", Utils.getFechaconLetraSinDia(DateTime.Parse(rd["Fecha"].ToString().ToUpper())));
                        af.SetField("txtEmpresa", rd["Empresa"].ToString().ToUpper());
                        af.SetField("txtRfc", rd["RFC"].ToString().ToUpper());
                        af.SetField("txtCliente", rd["Empresa"].ToString().ToUpper());
                        af.SetField("txtChofer ", rd["chofer"].ToString().ToUpper());
                        af.SetField("txtPlacas", rd["placas"].ToString().ToUpper());
                        af.SetField("txtMarca", rd["marca"].ToString().ToUpper());
                        af.SetField("txtAño", rd["anio"].ToString().ToUpper());
                        af.SetField("txtColor", rd["color"].ToString().ToUpper());
                        af.SetField("txtJaula", rd["jaula"].ToString().ToUpper());
                        af.SetField("txtOrigen", rd["origen"].ToString().ToUpper());
                        af.SetField("txtProducto", rd["Producto"].ToString().ToUpper());
                        af.SetField("txtPresentacion", rd["presentacion"].ToString().ToUpper());
                        af.SetField("txtBodega", rd["presentacion"].ToString().ToUpper());
                        af.SetField("txtUbicacion", rd["ubicacion"].ToString().ToUpper());
                        af.SetField("txtDestino", rd["destino"].ToString().ToUpper());
                        af.SetField("txtFacturara", rd["facturar_a"].ToString().ToUpper());
                        af.SetField("txtObservaciones", rd["observaciones"].ToString().ToUpper());
                        af.SetField("txtAutorizo", rd["Nombre"].ToString().ToUpper());
                        af.SetField("txtOrdenID", ID.ToString());
                        stampImage(ref r, ref ps, "txtImagen", rd["logoRelativePath"].ToString());
                        
                    }
                    ps.FormFlattening = true;
                    ps.Close();
                 }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.PRINT,  "NO SE PUDO IMPRIMIR LA ORDEN DE CARGA No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, ref ex);
                }
                finally { if (ps != null) { fS.Close(); ps.Close(); } }
          }
          catch(Exception ex)
          {
              sError = ex.Message;
              Logger.Instance.LogException(Logger.typeUserActions.PRINT, "NO SE PUDO IMPRIMIR LA ORDEN DE CARGA No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, ref ex);  
          }
          finally
            {
                conGaribay.Close();
            }
            return path;
        }

#region Print Pagare
        public static Byte[] imprimePagareCredito(int ID, int userID, double MontoPagare)
        {
            Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CREDITOS, Logger.typeUserActions.PRINT, userID,
                "Imprimio imprimePagareCredito, ID:" + ID.ToString() + " monto: " + MontoPagare.ToString());
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                string sql = "SELECT     dbo.Productores.apaterno + ' ' + dbo.Productores.amaterno + ' ' + dbo.Productores.nombre AS name, "
                    + " dbo.Productores.domicilio + ',  ' + dbo.Productores.municipio + ', ' + dbo.Estados.estado AS Dom, "
                    + " dbo.Creditos.Interesanual, dbo.Creditos.FechaFinCiclo, "
                    + " dbo.Productores.domicilio, dbo.Productores.poblacion, dbo.Productores.municipio, dbo.Estados.estado, "
                    + " dbo.Productores.telefono, dbo.Creditos.Fecha "
                    + " FROM         dbo.Estados INNER JOIN "
                    + " dbo.Productores ON dbo.Estados.estadoID = dbo.Productores.estadoID INNER JOIN "
                    + " dbo.Creditos ON dbo.Productores.productorID = dbo.Creditos.productorID";
                sql += " WHERE (creditos.creditoid = @creditoid) ";
                sql = dbFunctions.UpdateSDSForSisBanco(sql);
                
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdsel = new SqlCommand(sql, conGaribay);
                try
                {
                    conGaribay.Open();
                    cmdsel.Parameters.Add("@creditoid", SqlDbType.Int).Value = ID;
                    SqlDataReader rd = cmdsel.ExecuteReader();
                    PdfStamper ps = null;
                    PdfReader r = new PdfReader(new RandomAccessFileOrArray(HttpContext.Current.Request.MapPath("/formatos/FormatoPagareLast.pdf")), null);
                    ps = new PdfStamper(r, ms);
                    AcroFields af = ps.AcroFields;
                    try
                    {
                        if (rd.HasRows && rd.Read())
                        {
                            double tasaInteres = 0.00, montosol = 0.00;
                            double.TryParse(rd["InteresAnual"].ToString(), out tasaInteres);
                            montosol = MontoPagare;
                            tasaInteres = tasaInteres * 100;
                            DateTime fecha = DateTime.Parse(rd["Fecha"].ToString());
                            af.SetField("txtMontoPagare", string.Format("{0:C2}", montosol));
                            string textoPrimerParrafo = "En la ciudad de Ameca, Jalisco, el día "
                            + fecha.Day.ToString(CultureInfo.CurrentCulture) + " de "
                            + Utils.getMonthName(fecha.Month)
                            + " del año "
                            + fecha.Year.ToString(CultureInfo.CurrentCulture) + ", por el presente pagaré reconozco deber y me "
                            + "obligo incondicionalmente a pagar a la orden de la sociedad "
                            + "COMERCIALIZADORA LAS MARGARITAS S.P.R. DE R.L. en el domicilio ubicado en Av. Patria Oriente No. 10, en Ameca, Jalisco, "
                            + "la cantidad de "
                            + string.Format("{0:C2}", montosol)
                            + " ( " + Utils.NumeroALetras(string.Format("{0:F2}", montosol))
                            + " ). Valor recibido a mi entera satisfacción. ";
                            af.SetField("txtPrimerParrafo", textoPrimerParrafo);


                            string textoSegundoParrafo = rd["Name"].ToString().ToUpper()
                                + " reconozco deber y me obligo incondicionalmente a pagar a la COMERCIALIZADORA LAS MARGARITAS"
                                + " SPR DE RL todas las obligaciones cambiarias asumidas en los términos literales del presente"
                                + " pagaré por concepto de capital, el día "
                                + fecha.Day.ToString(CultureInfo.CurrentCulture) + " de " + Utils.getMonthName(fecha.Month) + " de " + fecha.Year.ToString(CultureInfo.CurrentCulture)
                                + " valor recibido a mi entera"
                                + " satisfacción.";
                            af.SetField("txtSegundoParrafo", textoSegundoParrafo);

                            string textoTercerParrafo = "El Presente Pagare causara interés ordinario a la tasa anual"
                            + " fija de 18 (diez y ocho porciento anual), pagaderos los días 02 de cada mes, hasta la liquidación del capital. En caso de incumplimiento"
                            + " de pago del capital e intereses ordinarios, a su vencimiento se causara intereses moratorios calculados a la tasa de"
                            + " interés ordinarias multiplicadas por dos veces, durante el tiempo que dure la mora o hasta"
                            + " que se realice el pago del insoluto, pagaderos en esta ciudad juntamente con el capital.";

                            af.SetField("txtTercerParrafo", textoTercerParrafo);

                            string textoCuartoParrafo = "Con fundamento en lo dispuesto por el artículo 363 trescientos"
                            + " sesenta y tres del Código de Comercio, las partes acuerdan expresamente que los intereses"
                            + " ordinarios o moratorios vencidos y no cubiertos, las prestaciones, gastos, comisiones y demás"
                            + " accesorios legales o convencionales que con motivo de este documento se generen y no sean pagados"
                            + " por el deudor " + rd["Name"].ToString().ToUpper() + " se capitalizarán mensualmente.";
                            af.SetField("txtCuartoParrafo", textoCuartoParrafo);

                            string textoQuintoParrafo = " Las entregas de los pagos a cuenta del adeudo, se aplicaran en primer término al pago de intereses por orden de vencimientos y después a capital.";
                            af.SetField("txtQuintoParrafo", textoQuintoParrafo);

                            string textSextoParrafo = "Para la interpretación y cumplimiento de las obligaciones asumidas"
                            + " en los términos del presente pagaré, " + rd["Name"].ToString().ToUpper()
                            + " se somete expresa e incondicionalmente a la jurisdicción y competencia de los tribunales"
                            + " competentes en materia mercantil de Primer Partido en el Estado de Jalisco, con residencia"
                            + " en la ciudad de Ameca, Jalisco, México; renunciando a cualquier fuero que en razón de su"
                            + " domicilio presente o futuro o por cualquier otro causa, le pudiera corresponder.";
                            af.SetField("txtSextoParrafo", textSextoParrafo);

                            string textSeptimoParrafo = "El presente pagaré se suscribe en la Ciudad de Ameca, Jalisco,"
                            + " México el día " + Utils.getFechaconLetraSinDia(fecha)
                            + " la fecha de vencimiento es "
                            + Utils.getFechaconLetraSinDia(DateTime.Parse(rd["FechaFinCiclo"].ToString()));
                            af.SetField("txtSeptimoParrafo", textSeptimoParrafo);

                            af.SetField("txtNombre", rd["Name"].ToString().ToUpper());
                            af.SetField("txtDireccion", rd["domicilio"].ToString().ToUpper());
                            af.SetField("txtMunicipio", rd["municipio"].ToString().ToUpper());
                            af.SetField("txtPoblacion", rd["poblacion"].ToString().ToUpper());
                            af.SetField("txtEstado", rd["estado"].ToString().ToUpper());
                            af.SetField("txtTelefono", rd["telefono"].ToString().ToUpper());

                        }
                        // make resultant PDF read-only for end-user
                        ps.FormFlattening = true;
                        // forget to close() PdfStamper, you end up with
                        // a corrupted file!
                        ps.Close();
                        res = ms.GetBuffer();
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.PRINT, userID, "NO SE PUDO IMPRIMIR LA CARATULA ANEXA DE LA SOLICITUD No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, "FORMATOSPDF.CS");

                    }
                    finally { if (ps != null) { ps.Close(); } }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.PRINT, userID, "NO SE PUDO IMPRIMIR LA CARATULA ANEXA DE LA SOLICITUD No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, "FORMATOSPDF.CS");

                }
                finally
                {
                    conGaribay.Close();

                }
            }

            return res;
        }
        public static Byte[] imprimePagareCredito(int ID, int userID, double MontoPagare, DateTime fecha)
        {
            return imprimePagareCredito(ID, userID, MontoPagare, fecha, Utils.Now, false);
        }
        public static Byte[] imprimePagareCredito(int ID, int userID, double MontoPagare, DateTime fecha, DateTime FechaPagare, bool UseFechaPagare)
        {
            Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CREDITOS, Logger.typeUserActions.PRINT, userID,
                "Imprimio Pagare, ID:" + ID.ToString() + " monto: " + MontoPagare.ToString() + " fecha: " + fecha.ToString()
                + "fecha pagare: " + FechaPagare.ToString() + "usefechapagare:" + UseFechaPagare.ToString());
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                string sql = "SELECT     dbo.Productores.apaterno + ' ' + dbo.Productores.amaterno + ' ' + dbo.Productores.nombre AS name, "
                    + " dbo.Productores.domicilio + ',  ' + dbo.Productores.municipio + ', ' + dbo.Estados.estado AS Dom, "
                    + " dbo.Creditos.Interesanual, dbo.Creditos.FechaFinCiclo, "
                    + " dbo.Productores.domicilio, dbo.Productores.poblacion, dbo.Productores.municipio, dbo.Estados.estado, "
                    + " dbo.Productores.telefono, dbo.Creditos.Fecha "
                    + " FROM         dbo.Estados INNER JOIN "
                    + " dbo.Productores ON dbo.Estados.estadoID = dbo.Productores.estadoID INNER JOIN "
                    + " dbo.Creditos ON dbo.Productores.productorID = dbo.Creditos.productorID";
                sql += " WHERE (creditos.creditoid = @creditoid) ";
                sql = dbFunctions.UpdateSDSForSisBanco(sql);

                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdsel = new SqlCommand(sql, conGaribay);
                try
                {
                    conGaribay.Open();
                    cmdsel.Parameters.Add("@creditoid", SqlDbType.Int).Value = ID;
                    SqlDataReader rd = cmdsel.ExecuteReader();
                    PdfStamper ps = null;
                    PdfReader r = new PdfReader(new RandomAccessFileOrArray(HttpContext.Current.Request.MapPath("/formatos/FormatoPagareLast.pdf")), null);
                    ps = new PdfStamper(r, ms);
                    AcroFields af = ps.AcroFields;
                    try
                    {
                        if (rd.HasRows && rd.Read())
                        {
                            double tasaInteres = 0.00, montosol = 0.00;
                            double.TryParse(rd["InteresAnual"].ToString(), out tasaInteres);
                            montosol = MontoPagare;
                            tasaInteres = tasaInteres * 100;

                            af.SetField("txtMontoPagare", string.Format("{0:C2}", montosol));
                            string textoPrimerParrafo = "En la ciudad de Ameca, Jalisco, el día "
                            + fecha.Day.ToString(CultureInfo.CurrentCulture) + " de "
                            + Utils.getMonthName(fecha.Month)
                            + " del año "
                            + fecha.Year.ToString(CultureInfo.CurrentCulture) + ", por el presente pagaré reconozco deber y me "
                            + "obligo incondicionalmente a pagar a la orden de la sociedad "
                            + "COMERCIALIZADORA LAS MARGARITAS S.P.R. DE R.L. en el domicilio ubicado en Av. Patria Oriente No. 10, en Ameca, Jalisco, "
                            + "la cantidad de "
                            + string.Format("{0:C2}", montosol)
                            + " ( " + Utils.NumeroALetras(string.Format("{0:F2}", montosol))
                            + " ). Valor recibido a mi entera satisfacción. ";
                            af.SetField("txtPrimerParrafo", textoPrimerParrafo);


                            string textoSegundoParrafo = rd["Name"].ToString().ToUpper()
                                + " reconozco deber y me obligo incondicionalmente a pagar a la COMERCIALIZADORA LAS MARGARITAS"
                                + " SPR DE RL todas las obligaciones cambiarias asumidas en los términos literales del presente"
                                + " pagaré por concepto de capital, el día "
                                + fecha.Day.ToString(CultureInfo.CurrentCulture) + " de " + Utils.getMonthName(fecha.Month) + " de " + fecha.Year.ToString(CultureInfo.CurrentCulture)
                                + " valor recibido a mi entera"
                                + " satisfacción.";
                            af.SetField("txtSegundoParrafo", textoSegundoParrafo);

                            string textoTercerParrafo = "El Presente Pagare causara interés ordinario a la tasa anual"
                            + " fija de 18 (diez y ocho porciento anual), pagaderos los días 02 de cada mes, hasta la liquidación del capital. En caso de incumplimiento"
                            + " de pago del capital e intereses ordinarios, a su vencimiento se causara intereses moratorios calculados a la tasa de"
                            + " interés ordinarias multiplicadas por dos veces, durante el tiempo que dure la mora o hasta"
                            + " que se realice el pago del insoluto, pagaderos en esta ciudad juntamente con el capital.";

                            af.SetField("txtTercerParrafo", textoTercerParrafo);

                            string textoCuartoParrafo = "Con fundamento en lo dispuesto por el artículo 363 trescientos"
                            + " sesenta y tres del Código de Comercio, las partes acuerdan expresamente que los intereses"
                            + " ordinarios o moratorios vencidos y no cubiertos, las prestaciones, gastos, comisiones y demás"
                            + " accesorios legales o convencionales que con motivo de este documento se generen y no sean pagados"
                            + " por el deudor " + rd["Name"].ToString().ToUpper() + " se capitalizarán mensualmente.";
                            af.SetField("txtCuartoParrafo", textoCuartoParrafo);

                            string textoQuintoParrafo = " Las entregas de los pagos a cuenta del adeudo, se aplicaran en primer término al pago de intereses por orden de vencimientos y después a capital.";
                            af.SetField("txtQuintoParrafo", textoQuintoParrafo);

                            string textSextoParrafo = "Para la interpretación y cumplimiento de las obligaciones asumidas"
                            + " en los términos del presente pagaré, " + rd["Name"].ToString().ToUpper()
                            + " se somete expresa e incondicionalmente a la jurisdicción y competencia de los tribunales"
                            + " competentes en materia mercantil de Primer Partido en el Estado de Jalisco, con residencia"
                            + " en la ciudad de Ameca, Jalisco, México; renunciando a cualquier fuero que en razón de su"
                            + " domicilio presente o futuro o por cualquier otro causa, le pudiera corresponder.";
                            af.SetField("txtSextoParrafo", textSextoParrafo);

                            string textSeptimoParrafo = "El presente pagaré se suscribe en la Ciudad de Ameca, Jalisco,"
                            + " México el día " + Utils.getFechaconLetraSinDia(fecha)
                            + " la fecha de vencimiento es "
                            + Utils.getFechaconLetraSinDia(DateTime.Parse(rd["FechaFinCiclo"].ToString()));
                            af.SetField("txtSeptimoParrafo", textSeptimoParrafo);

                            af.SetField("txtNombre", rd["Name"].ToString().ToUpper());
                            af.SetField("txtDireccion", rd["domicilio"].ToString().ToUpper());
                            af.SetField("txtMunicipio", rd["municipio"].ToString().ToUpper());
                            af.SetField("txtPoblacion", rd["poblacion"].ToString().ToUpper());
                            af.SetField("txtEstado", rd["estado"].ToString().ToUpper());
                            af.SetField("txtTelefono", rd["telefono"].ToString().ToUpper());

                        }
                        // make resultant PDF read-only for end-user
                        ps.FormFlattening = true;
                        // forget to close() PdfStamper, you end up with
                        // a corrupted file!
                        ps.Close();
                        res = ms.GetBuffer();
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.PRINT, userID, "NO SE PUDO IMPRIMIR LA CARATULA ANEXA DE LA SOLICITUD No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, "FORMATOSPDF.CS");

                    }
                    finally { if (ps != null) { ps.Close(); } }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.PRINT, userID, "NO SE PUDO IMPRIMIR LA CARATULA ANEXA DE LA SOLICITUD No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, "FORMATOSPDF.CS");

                }
                finally
                {
                    conGaribay.Close();

                }
            }

            return res;
        }
        public static Byte[] imprimePagare(int ID, int userID)
        {
            return imprimePagare(ID, userID, 0);
        }
        public static Byte[] imprimePagare(int ID, int userID, double MontoPagare)
        {
            Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CREDITOS, Logger.typeUserActions.PRINT, userID,
                "Imprimio Pagare, ID:" + ID.ToString() + " monto: " + MontoPagare.ToString());
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                string sql = "SELECT     Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS name, Productores.domicilio + ',  ' + Productores.municipio + ', ' + Estados.estado AS Dom, Creditos.InteresAnual, ";
                sql += " Solicitudes.Fecha, Solicitudes.Monto, Creditos.FechaFinCiclo, Solicitudes.Plazo, Solicitudes.RecursosPropios, Solicitudes.testigo1, Solicitudes.testigo2, Solicitudes.aval1, Solicitudes.aval2, Solicitudes.Aval1Dom , Solicitudes.Aval2Dom, ";
                sql += " Solicitudes.UbicacionGarantia, Productores.domicilio, Productores.poblacion,  Productores.municipio , Estados.estado, Productores.telefono FROM         Solicitudes INNER JOIN ";
                sql += " Creditos ON Creditos.creditoID = Solicitudes.creditoID INNER JOIN ";
                sql += " Productores ON Productores.productorID = Solicitudes.productorID INNER JOIN ";
                sql += " Estados ON Productores.estadoID = Estados.estadoID ";
                sql += " WHERE (Solicitudes.solicitudID = @solID) ";
                sql = dbFunctions.UpdateSDSForSisBanco(sql);
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdsel = new SqlCommand(sql, conGaribay);
                try
                {
                    conGaribay.Open();
                    cmdsel.Parameters.Add("@solID", SqlDbType.Int).Value = ID;
                    SqlDataReader rd = cmdsel.ExecuteReader();
                    if (!rd.HasRows) { conGaribay.Close(); }
                    PdfStamper ps = null;
                    PdfReader r =  null;
                    if (!new BasePage().IsSistemBanco)
                    {
                        r = new PdfReader(new RandomAccessFileOrArray(HttpContext.Current.Request.MapPath("/formatos/FormatoPagareLast.pdf")), null);
                    }
                    else
                    {
                        r = new PdfReader(new RandomAccessFileOrArray(HttpContext.Current.Request.MapPath("/formatos/FormatoPagareBajioConTabla.pdf")), null);
                    }
                    ps = new PdfStamper(r, ms);
                    AcroFields af = ps.AcroFields;
                    try
                    {
                        if (rd.HasRows && rd.Read())
                        {
                            double tasaInteres = 0.00, montorecpropios = 0.00, montosol = 0.00;
                            double.TryParse(rd["InteresAnual"].ToString(), out tasaInteres);
                            double.TryParse(rd["RecursosPropios"].ToString(), out montorecpropios);
                            double.TryParse(rd["Monto"].ToString(), out montosol);
                            if (MontoPagare > 0)
                            {
                                montosol = MontoPagare;
                            } 
                            tasaInteres = tasaInteres * 100;
                            DateTime fecha = DateTime.Parse(rd["Fecha"].ToString());

                            
                            if (!new BasePage().IsSistemBanco)
                            {
#region PAGARE MARGARITAS PARAFINANCIERA
                                af.SetField("txtMontoPagare", string.Format("{0:C2}", montosol));
                                string textoPrimerParrafo = "En la ciudad de Ameca, Jalisco, el día "
                                + fecha.Day.ToString(CultureInfo.CurrentCulture) + " de "
                                + Utils.getMonthName(fecha.Month)
                                + " del año "
                                + fecha.Year.ToString(CultureInfo.CurrentCulture) + ", por el presente pagaré reconozco deber y me "
                                + "obligo incondicionalmente a pagar a la orden de la sociedad "
                                + "COMERCIALIZADORA LAS MARGARITAS S.P.R. DE R.L. en el domicilio ubicado en Av. Patria Oriente No. 10, en Ameca, Jalisco, "
                                + "la cantidad de "
                                + string.Format("{0:C2}", montosol)
                                + " ( " + Utils.NumeroALetras(string.Format("{0:F2}", montosol))
                                + " ). Valor recibido a mi entera satisfacción. ";
                                af.SetField("txtPrimerParrafo", textoPrimerParrafo);

                                
                                string textoSegundoParrafo = rd["Name"].ToString().ToUpper()
                                    + " reconozco deber y me obligo incondicionalmente a pagar a la COMERCIALIZADORA LAS MARGARITAS"
                                    + " SPR DE RL todas las obligaciones cambiarias asumidas en los términos literales del presente"
                                    + " pagaré por concepto de capital, el día "
                                    + fecha.Day.ToString(CultureInfo.CurrentCulture) + " de " + Utils.getMonthName(fecha.Month) + " de " + fecha.Year.ToString(CultureInfo.CurrentCulture)
                                    + " valor recibido a mi entera"
                                    + " satisfacción.";
                                af.SetField("txtSegundoParrafo", textoSegundoParrafo);

                                string textoTercerParrafo = "El Presente Pagare causara interés ordinario a la tasa anual"
                                + " fija de 18 (diez y ocho porciento anual), pagaderos los días 02 de cada mes, hasta la liquidación del capital. En caso de incumplimiento"
                                + " de pago del capital e intereses ordinarios, a su vencimiento se causara intereses moratorios calculados a la tasa de"
                                + " interés ordinarias multiplicadas por dos veces, durante el tiempo que dure la mora o hasta"
                                + " que se realice el pago del insoluto, pagaderos en esta ciudad juntamente con el capital.";

                                af.SetField("txtTercerParrafo", textoTercerParrafo);

                                string textoCuartoParrafo = "Con fundamento en lo dispuesto por el artículo 363 trescientos"
                                + " sesenta y tres del Código de Comercio, las partes acuerdan expresamente que los intereses"
                                + " ordinarios o moratorios vencidos y no cubiertos, las prestaciones, gastos, comisiones y demás"
                                + " accesorios legales o convencionales que con motivo de este documento se generen y no sean pagados"
                                + " por el deudor " + rd["Name"].ToString().ToUpper() + " se capitalizarán mensualmente.";
                                af.SetField("txtCuartoParrafo", textoCuartoParrafo);

                                string textoQuintoParrafo = " Las entregas de los pagos a cuenta del adeudo, se aplicaran en primer término al pago de intereses por orden de vencimientos y después a capital.";
                                af.SetField("txtQuintoParrafo", textoQuintoParrafo);

                                string textSextoParrafo = "Para la interpretación y cumplimiento de las obligaciones asumidas"
                                + " en los términos del presente pagaré, " + rd["Name"].ToString().ToUpper()
                                + " se somete expresa e incondicionalmente a la jurisdicción y competencia de los tribunales"
                                + " competentes en materia mercantil de Primer Partido en el Estado de Jalisco, con residencia"
                                + " en la ciudad de Ameca, Jalisco, México; renunciando a cualquier fuero que en razón de su"
                                + " domicilio presente o futuro o por cualquier otro causa, le pudiera corresponder.";
                                af.SetField("txtSextoParrafo", textSextoParrafo);

                                string textSeptimoParrafo = "El presente pagaré se suscribe en la Ciudad de Ameca, Jalisco,"
                                + " México el día " + Utils.getFechaconLetraSinDia(fecha)
                                + " la fecha de vencimiento es "
                                + Utils.getFechaconLetraSinDia(DateTime.Parse(rd["FechaFinCiclo"].ToString()));
                                af.SetField("txtSeptimoParrafo", textSeptimoParrafo);

                                af.SetField("txtNombre", rd["Name"].ToString().ToUpper());
                                af.SetField("txtDireccion", rd["domicilio"].ToString().ToUpper());
                                af.SetField("txtMunicipio", rd["municipio"].ToString().ToUpper());
                                af.SetField("txtPoblacion", rd["poblacion"].ToString().ToUpper());
                                af.SetField("txtEstado", rd["estado"].ToString().ToUpper());
                                af.SetField("txtTelefono", rd["telefono"].ToString().ToUpper());
#endregion
                            }
                            else
                            {
                                #region PAGARE MARGARITAS
                                af.SetField("txtMontoPagare", string.Format("{0:C2}", montosol));
                                string textoParrafo = "Por este pagaré prometo(emos) y me(nos) obligo(amos) a pagar incondicionalmente a la orden de COMERCIALIZADORA LAS MARGARITAS S.P.R. DE R.L. , en sus oficinas ubicadas en Av. Patria Oriente No. 10, en Ameca, Jalisco precisamente el día 31 DE ENERO DE 2011, la cantidad de "
                                + string.Format("{0:C2}", montosol)
                                + " ( " + Utils.NumeroALetras(string.Format("{0:F2}", montosol))
                                + ")., de acuerdo al calendario de amortizaciones que se hace constar al reverso del presente instrumento, la cual causará intereses ordinarios sobre saldos insolutos a razón de la tasa anual del  18 %, mismo que será pagadero el último día hábil de cada mes.";
                                af.SetField("txtPrimerParrafo", textoParrafo);


                                textoParrafo = "Si el importe total o la parte proporcional correspondiente a este pagaré no fuere pagado a su vencimiento, causará intereses moratorios a razón de la tasa que se resulte de multiplicar por 2 dos la Tasa de Interés Ordinaria. Dichos intereses se causarán desde la fecha en que incurra en el incumplimiento hasta la regularización de los pagos.";
                                af.SetField("txtSegundoParrafo", textoParrafo);

                                textoParrafo = "Este pagaré solo podrá ser negociado a favor de Banco del Bajío, S.A, Institución de Banca Múltiple y/o con alguno de los Fideicomisos Instituidos con Relación a la Agricultura (FIRA).";
                                af.SetField("txtTercerParrafo", textoParrafo);


                                textoParrafo = "El (los) suscriptor (res) y su (s) avalista (s), se someten expresamente para el caso de controversia judicial, a la competencia de los tribunales de la ciudad de AMECA, Estado de JALISCO.";
                                af.SetField("txtCuartoParrafo", textoParrafo);

                                textoParrafo = "En la ciudad de AMECA, JALISCO a los "
                                + fecha.Day.ToString(CultureInfo.CurrentCulture) + " dias del mes de "
                                + Utils.getMonthName(fecha.Month)
                                + " de "
                                + fecha.Year.ToString(CultureInfo.CurrentCulture) + ".";
                                af.SetField("txtQuintoParrafo", textoParrafo);

                                af.SetField("txtNombre", rd["Name"].ToString().ToUpper());
                                af.SetField("txtDireccion", rd["domicilio"].ToString().ToUpper() + ", " + rd["municipio"].ToString().ToUpper());

                                af.SetField("txtFecha", "31 DE ENERO DE 2011");
                                #endregion
                            }
                            
                                
                            
//                             af.SetField("txtCiudadFinal", "AMECA, JALISCO.");
//                             af.SetField("txtCiudadPagare", "AMECA, JALISCO.");
//                             af.SetField("txtFechaFinalPagare", "A " + Utils.getFechaconLetraSinDia(DateTime.Parse(rd["Fecha"].ToString()),userID).ToUpper());
//                             af.SetField("txtFirmaPagare",rd["Name"].ToString().ToUpper());
//                             af.SetField("txtDomProdPagare",rd["Dom"].ToString().ToUpper());
                            af.SetField("txtAval1Pag",rd["aval1"].ToString().ToUpper());                    
                            af.SetField("txtAval1PagDom",rd["Aval1Dom"].ToString().ToUpper());
                            af.SetField("txtAval2Pag",rd["aval2"].ToString().ToUpper());
                            af.SetField("txtAval2PagDom",rd["Aval2Dom"].ToString().ToUpper());

                        }
                        // make resultant PDF read-only for end-user
                        ps.FormFlattening = true;
                        // forget to close() PdfStamper, you end up with
                        // a corrupted file!
                        ps.Close();
                        res = ms.GetBuffer();
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.PRINT, userID, "NO SE PUDO IMPRIMIR LA CARATULA ANEXA DE LA SOLICITUD No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, "FORMATOSPDF.CS");

                    }
                    finally { if (ps != null) { ps.Close(); } }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.PRINT, userID, "NO SE PUDO IMPRIMIR LA CARATULA ANEXA DE LA SOLICITUD No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, "FORMATOSPDF.CS");

                }
                finally
                {
                    conGaribay.Close();

                }
            }

            return res;
        }

        public static Byte[] imprimePagare(int ID, int userID, double MontoPagare, DateTime fecha)
        {
            return FormatosPdf.imprimePagare(ID, userID, MontoPagare, fecha, Utils.Now, false);
        }
        public static Byte[] imprimePagare(int ID, 
            int userID, 
            double MontoPagare, 
            DateTime fecha, 
            DateTime fechaPagare, 
            bool UseFechaPagare)
        {
            Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CREDITOS, Logger.typeUserActions.PRINT, userID,
                "Imprimio Pagare, ID:" + ID.ToString() + " monto: " + MontoPagare.ToString() + " fecha: " + fecha.ToString()
                + "fecha pagare: " + fechaPagare.ToString() + "usefechapagare:" + UseFechaPagare.ToString());
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                string sql = "SELECT     Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS name, Productores.domicilio + ',  ' + Productores.municipio + ', ' + Estados.estado AS Dom, Creditos.InteresAnual, ";
                sql += " Solicitudes.Fecha, Solicitudes.Monto, Creditos.FechaFinCiclo, Solicitudes.Plazo, Solicitudes.RecursosPropios, Solicitudes.testigo1, Solicitudes.testigo2, Solicitudes.aval1, Solicitudes.aval2, Solicitudes.Aval1Dom , Solicitudes.Aval2Dom, ";
                sql += " Solicitudes.UbicacionGarantia, Productores.domicilio, Productores.poblacion,  Productores.municipio , Estados.estado, Productores.telefono FROM         Solicitudes INNER JOIN ";
                sql += " Creditos ON Creditos.creditoID = Solicitudes.creditoID INNER JOIN ";
                sql += " Productores ON Productores.productorID = Solicitudes.productorID INNER JOIN ";
                sql += " Estados ON Productores.estadoID = Estados.estadoID ";
                sql += " WHERE (Solicitudes.solicitudID = @solID) ";
                sql = dbFunctions.UpdateSDSForSisBanco(sql);
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdsel = new SqlCommand(sql, conGaribay);
                try
                {
                    conGaribay.Open();
                    cmdsel.Parameters.Add("@solID", SqlDbType.Int).Value = ID;
                    SqlDataReader rd = cmdsel.ExecuteReader();
                    if (!rd.HasRows) { conGaribay.Close(); }
                    PdfStamper ps = null;
                    PdfReader r = new PdfReader(new RandomAccessFileOrArray(HttpContext.Current.Request.MapPath("/formatos/FormatoPagareBajioConTabla.pdf")), null);
                    ps = new PdfStamper(r, ms);
                    AcroFields af = ps.AcroFields;
                    try
                    {
                        if (rd.HasRows && rd.Read())
                        {
                            double tasaInteres = 0.00, montorecpropios = 0.00, montosol = 0.00;
                            double.TryParse(rd["InteresAnual"].ToString(), out tasaInteres);
                            double.TryParse(rd["RecursosPropios"].ToString(), out montorecpropios);
                            double.TryParse(rd["Monto"].ToString(), out montosol);
                            if (MontoPagare > 0)
                            {
                                montosol = MontoPagare;
                            }
                            tasaInteres = tasaInteres * 100;
                            #region PAGARE MARGARITAS
                            af.SetField("txtMontoPagare", string.Format("{0:C2}", montosol));
                            string textoParrafo = "Por este pagaré prometo(emos) y me(nos) obligo(amos) a pagar incondicionalmente a la orden de COMERCIALIZADORA LAS MARGARITAS S.P.R. DE R.L. , en sus oficinas ubicadas en Av. Patria Oriente No. 10, en Ameca, Jalisco precisamente el día "
                            + Utils.getFechaconLetraSinDia(UseFechaPagare ? fechaPagare : DateTime.Parse(rd["FechaFinCiclo"].ToString()))
                            + ", la cantidad de "
                            + string.Format("{0:C2}", montosol)
                            + " ( " + Utils.NumeroALetras(string.Format("{0:F2}", montosol))
                            + ")., de acuerdo al calendario de amortizaciones que se hace constar al reverso del presente instrumento, la cual causará intereses ordinarios sobre saldos insolutos a razón de la tasa anual del  18 %, mismo que será pagadero el último día hábil de cada mes.";
                            af.SetField("txtPrimerParrafo", textoParrafo);


                            textoParrafo = "Si el importe total o la parte proporcional correspondiente a este pagaré no fuere pagado a su vencimiento, causará intereses moratorios a razón de la tasa que se resulte de multiplicar por 2 dos la Tasa de Interés Ordinaria. Dichos intereses se causarán desde la fecha en que incurra en el incumplimiento hasta la regularización de los pagos.";
                            af.SetField("txtSegundoParrafo", textoParrafo);

                            textoParrafo = "Este pagaré solo podrá ser negociado a favor de Banco del Bajío, S.A, Institución de Banca Múltiple y/o con alguno de los Fideicomisos Instituidos con Relación a la Agricultura (FIRA).";
                            af.SetField("txtTercerParrafo", textoParrafo);


                            textoParrafo = "El (los) suscriptor (res) y su (s) avalista (s), se someten expresamente para el caso de controversia judicial, a la competencia de los tribunales de la ciudad de AMECA, Estado de JALISCO.";
                            af.SetField("txtCuartoParrafo", textoParrafo);

                            textoParrafo = "En la ciudad de AMECA, JALISCO a los "
                            + fecha.Day.ToString(CultureInfo.CurrentCulture) + " dias del mes de "
                            + Utils.getMonthName(fecha.Month)
                            + " de "
                            + fecha.Year.ToString(CultureInfo.CurrentCulture) + ".";
                            af.SetField("txtQuintoParrafo", textoParrafo);

                            af.SetField("txtNombre", rd["Name"].ToString().ToUpper());
                            af.SetField("txtDireccion", rd["domicilio"].ToString().ToUpper() + ", " + rd["municipio"].ToString().ToUpper());

                            af.SetField("txtFecha", Utils.getFechaconLetraSinDia(UseFechaPagare ? fechaPagare : DateTime.Parse(rd["FechaFinCiclo"].ToString())));
                            #endregion
                            

                            
                            /*
                            if()
                                                        {
                                                            string textoPrimerParrafo = "En la ciudad de Ameca, Jalisco, el día "
                                                            + fecha.Day.ToString(CultureInfo.CurrentCulture) + " de "
                                                            + Utils.getMonthName(fecha.Month)
                                                            + " del año "
                                                            + fecha.Year.ToString(CultureInfo.CurrentCulture) + ", por el presente pagaré reconozco deber y me "
                                                            + "obligo incondicionalmente a pagar a la orden de la sociedad "
                                                            + "COMERCIALIZADORA LAS MARGARITAS S.P.R. DE R.L. en el domicilio ubicado en Av. Patria Oriente No. 10, en Ameca, Jalisco, "
                                                            + "la cantidad de "
                                                            + string.Format("{0:C2}", montosol)
                                                            + " ( " + Utils.NumeroALetras(string.Format("{0:F2}", montosol))
                                                            + " ). Valor recibido a mi entera satisfacción. ";
                                                            af.SetField("txtPrimerParrafo", textoPrimerParrafo);
                            
                            
                                                            string textoSegundoParrafo = rd["Name"].ToString().ToUpper()
                                                                + " reconozco deber y me obligo incondicionalmente a pagar a la COMERCIALIZADORA LAS MARGARITAS"
                                                                + " SPR DE RL todas las obligaciones cambiarias asumidas en los términos literales del presente"
                                                                + " pagaré por concepto de capital, el día "
                                                                + fecha.Day.ToString(CultureInfo.CurrentCulture) + " de " + Utils.getMonthName(fecha.Month) + " de " + fecha.Year.ToString(CultureInfo.CurrentCulture)
                                                                + " valor recibido a mi entera"
                                                                + " satisfacción.";
                                                            af.SetField("txtSegundoParrafo", textoSegundoParrafo);
                            
                                                            string textoTercerParrafo = "El Presente Pagare causara interés ordinario a la tasa anual"
                                                            + " fija de 18 (diez y ocho porciento anual), pagaderos los días 02 de cada mes, hasta la liquidación del capital. En caso de incumplimiento"
                                                            + " de pago del capital e intereses ordinarios, a su vencimiento se causara intereses moratorios calculados a la tasa de"
                                                            + " interés ordinarias multiplicadas por dos veces, durante el tiempo que dure la mora o hasta"
                                                            + " que se realice el pago del insoluto, pagaderos en esta ciudad juntamente con el capital.";
                            
                                                            af.SetField("txtTercerParrafo", textoTercerParrafo);
                            
                                                            string textoCuartoParrafo = "Con fundamento en lo dispuesto por el artículo 363 trescientos"
                                                            + " sesenta y tres del Código de Comercio, las partes acuerdan expresamente que los intereses"
                                                            + " ordinarios o moratorios vencidos y no cubiertos, las prestaciones, gastos, comisiones y demás"
                                                            + " accesorios legales o convencionales que con motivo de este documento se generen y no sean pagados"
                                                            + " por el deudor " + rd["Name"].ToString().ToUpper() + " se capitalizarán mensualmente.";
                                                            af.SetField("txtCuartoParrafo", textoCuartoParrafo);
                            
                                                            string textoQuintoParrafo = " Las entregas de los pagos a cuenta del adeudo, se aplicaran en primer término al pago de intereses por orden de vencimientos y después a capital.";
                                                            af.SetField("txtQuintoParrafo", textoQuintoParrafo);
                            
                                                            string textSextoParrafo = "Para la interpretación y cumplimiento de las obligaciones asumidas"
                                                            + " en los términos del presente pagaré, " + rd["Name"].ToString().ToUpper()
                                                            + " se somete expresa e incondicionalmente a la jurisdicción y competencia de los tribunales"
                                                            + " competentes en materia mercantil de Primer Partido en el Estado de Jalisco, con residencia"
                                                            + " en la ciudad de Ameca, Jalisco, México; renunciando a cualquier fuero que en razón de su"
                                                            + " domicilio presente o futuro o por cualquier otro causa, le pudiera corresponder.";
                                                            af.SetField("txtSextoParrafo", textSextoParrafo);
                            
                                                            string textSeptimoParrafo = "El presente pagaré se suscribe en la Ciudad de Ameca, Jalisco,"
                                                            + " México el día " + Utils.getFechaconLetraSinDia(fecha)
                                                            + " la fecha de vencimiento es "
                                                            + Utils.getFechaconLetraSinDia(UseFechaPagare ? fechaPagare : DateTime.Parse(rd["FechaFinCiclo"].ToString()));
                                                            af.SetField("txtSeptimoParrafo", textSeptimoParrafo);
                                                        }*/
                            
                            /*
                            af.SetField("txtNombre", rd["Name"].ToString().ToUpper());
                                                        af.SetField("txtDireccion", rd["domicilio"].ToString().ToUpper());
                                                        af.SetField("txtMunicipio", rd["municipio"].ToString().ToUpper());
                                                        af.SetField("txtPoblacion", rd["poblacion"].ToString().ToUpper());
                                                        af.SetField("txtEstado", rd["estado"].ToString().ToUpper());
                                                        af.SetField("txtTelefono", rd["telefono"].ToString().ToUpper());*/
                            


                            //                             af.SetField("txtCiudadFinal", "AMECA, JALISCO.");
                            //                             af.SetField("txtCiudadPagare", "AMECA, JALISCO.");
                            //                             af.SetField("txtFechaFinalPagare", "A " + Utils.getFechaconLetraSinDia(DateTime.Parse(rd["Fecha"].ToString()),userID).ToUpper());
                            //                             af.SetField("txtFirmaPagare",rd["Name"].ToString().ToUpper());
                            //                             af.SetField("txtDomProdPagare",rd["Dom"].ToString().ToUpper());
                            af.SetField("txtAval1Pag", rd["aval1"].ToString().ToUpper());
                            af.SetField("txtAval1PagDom", rd["Aval1Dom"].ToString().ToUpper());
                            af.SetField("txtAval2Pag", rd["aval2"].ToString().ToUpper());
                            af.SetField("txtAval2PagDom", rd["Aval2Dom"].ToString().ToUpper());

                        }
                        // make resultant PDF read-only for end-user
                        ps.FormFlattening = true;
                        // forget to close() PdfStamper, you end up with
                        // a corrupted file!
                        ps.Close(); 
                        res = ms.GetBuffer();
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.PRINT, userID, "NO SE PUDO IMPRIMIR LA CARATULA ANEXA DE LA SOLICITUD No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, "FORMATOSPDF.CS");

                    }
                    finally { if (ps != null) { ps.Close(); } }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.PRINT, userID, "NO SE PUDO IMPRIMIR LA CARATULA ANEXA DE LA SOLICITUD No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, "FORMATOSPDF.CS");

                }
                finally
                {
                    conGaribay.Close();

                }
            }

            return res;
        }
#endregion



        public static Byte [] imprimeCaratulaAnexaSola(int ID, int userID)
        {
            Byte[] res = null;
            using(MemoryStream ms = new MemoryStream())
            {
                string sql = "SELECT     Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS name, Productores.domicilio + ',  ' + Productores.municipio + ', ' + Estados.estado AS Dom, Creditos.InteresAnual, ";
                sql += " Solicitudes.Fecha, Solicitudes.Monto, Creditos.FechaFinCiclo, Solicitudes.Plazo, Solicitudes.RecursosPropios, Solicitudes.testigo1, Solicitudes.testigo2, Solicitudes.aval1, Solicitudes.aval2, Solicitudes.Aval1Dom , Solicitudes.Aval2Dom, ";
                sql += " Solicitudes.UbicacionGarantia FROM         Solicitudes INNER JOIN ";
                sql += " Creditos ON Creditos.creditoID = Solicitudes.creditoID INNER JOIN ";
                sql += " Productores ON Productores.productorID = Solicitudes.productorID INNER JOIN ";
                sql += " Estados ON Productores.estadoID = Estados.estadoID ";
                sql += " WHERE (Solicitudes.solicitudID = @solID) ";
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdsel = new SqlCommand(sql, conGaribay);
                try
                {
                    conGaribay.Open();
                    cmdsel.Parameters.Add("@solID", SqlDbType.Int).Value = ID;
                    SqlDataReader rd = cmdsel.ExecuteReader();
                    if (!rd.HasRows) { conGaribay.Close(); }
                    PdfStamper ps = null;
                    PdfReader r = new PdfReader(new RandomAccessFileOrArray(HttpContext.Current.Request.MapPath("/formatos/FormatoCaratulaAnexaLast.pdf")), null);
                    ps = new PdfStamper(r, ms);
                    AcroFields af = ps.AcroFields;
                    try
                    {
                        while (rd.Read())
                        {
                            double tasaInteres = 0.00, montorecpropios = 0.00, montosol = 0.00;
                            double.TryParse(rd["InteresAnual"].ToString(), out tasaInteres);
                            double.TryParse(rd["RecursosPropios"].ToString(), out montorecpropios);
                            double.TryParse(rd["Monto"].ToString(), out montosol);
                            tasaInteres = tasaInteres * 100;

                            af.SetField("txtNombre", rd["name"].ToString().ToUpper()); af.SetField("txtDomicilio", rd["Dom"].ToString().ToUpper());
                            af.SetField("txtAval1Name", rd["aval1"].ToString().ToUpper()); af.SetField("txtAval2Name", rd["aval2"].ToString().ToUpper());
                            af.SetField("txtAval1Dom ", rd["Aval1Dom"].ToString().ToUpper()); af.SetField("txtAval2Dom", rd["Aval2Dom"].ToString().ToUpper());
                            af.SetField("txtObjetodelCredito", "COSTO DE CULTIVO DE MAÍZ DE TEMPORAL PARCIAL");
                            af.SetField("txtPlazo", rd["Plazo"].ToString().ToUpper() + " MESES ");
                            af.SetField("txtMonto", string.Format("{0:C2}", montosol)); af.SetField("txtTasaFija", string.Format("{0:N2}", tasaInteres) + "%");
                            af.SetField("txtDomicilioDep", rd["UbicacionGarantia"].ToString().ToUpper());
                            af.SetField("txtNombreyFirma", rd["name"].ToString().ToUpper());
                            af.SetField("txtAcreditante", "COMERCIALIZADORA LAS MARGARITAS S.P.R. DE R.L.");

                            af.SetField("txtAval2", rd["aval2"].ToString().ToUpper()); af.SetField("txtAval1", rd["aval1"].ToString().ToUpper());
                            af.SetField("txtTestigo1", rd["testigo1"].ToString().ToUpper()); af.SetField("txtTestigo2", rd["testigo2"].ToString().ToUpper());
                            af.SetField("txtFechaFirma", Utils.getFechaconLetra(Utils.Now, userID));
                            af.SetField("textRecursosComplementarios", string.Format("{0:c2}", montorecpropios));


                        }
                        // make resultant PDF read-only for end-user
                        ps.FormFlattening = true;
                        // forget to close() PdfStamper, you end up with
                        // a corrupted file!
                        ps.Close();
                        res = ms.GetBuffer();
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.PRINT, userID, "NO SE PUDO IMPRIMIR LA CARATULA ANEXA DE LA SOLICITUD No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, "FORMATOSPDF.CS");

                    }
                    finally { if (ps != null) { ps.Close(); } }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.PRINT, userID, "NO SE PUDO IMPRIMIR LA CARATULA ANEXA DE LA SOLICITUD No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, "FORMATOSPDF.CS");

                }
                finally
                {
                    conGaribay.Close();

                }
            }
            
            return res;
        }
        public static Byte[] imprimeTermsAndConditions(int ID, int userID)
        {
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                string sql = "SELECT     Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS name, Productores.domicilio + ',  ' + Productores.municipio + ', ' + Estados.estado AS Dom, Creditos.InteresAnual, ";
                sql += " Solicitudes.Fecha, Solicitudes.Monto, Creditos.FechaFinCiclo, Solicitudes.Plazo, Solicitudes.RecursosPropios, Solicitudes.testigo1, Solicitudes.testigo2, Solicitudes.aval1, Solicitudes.aval2, Solicitudes.Aval1Dom , Solicitudes.Aval2Dom, ";
                sql += " Solicitudes.UbicacionGarantia, Productores.conyugue, Solicitudes.firmaAutorizada1, Solicitudes.firmaAutorizada2, Solicitudes.firmaAutorizada3, Solicitudes.firmaAutorizada4, Solicitudes.firmaAutorizada5, Solicitudes.ConceptoSoporteGarantia, Solicitudes.domicilioDelDeposito, ";
                sql += " Solicitudes.DescripciondegarantiaS, Solicitudes.superficieFinanciada ";
                if (new BasePage().IsSistemBanco)
                    sql += ", Solicitudes.aportaciondelproductor";
                sql += " FROM         Solicitudes INNER JOIN ";
                sql += " Creditos ON Creditos.creditoID = Solicitudes.creditoID INNER JOIN ";
                sql += " Productores ON Productores.productorID = Solicitudes.productorID INNER JOIN ";
                sql += " Estados ON Productores.estadoID = Estados.estadoID ";
                sql += " WHERE (Solicitudes.solicitudID = @solID) ";
                if (new BasePage().IsSistemBanco)
                {
                    sql = dbFunctions.UpdateSDSForSisBanco(sql);
                }
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdsel = new SqlCommand(sql, conGaribay);
                try
                {
                    conGaribay.Open();
                    cmdsel.Parameters.Add("@solID", SqlDbType.Int).Value = ID;
                    SqlDataReader rd = cmdsel.ExecuteReader();
                    if (!rd.HasRows) { conGaribay.Close(); }
                    PdfStamper ps = null;
                    PdfReader r = new PdfReader(new RandomAccessFileOrArray(HttpContext.Current.Request.MapPath("/formatos/FormatoCaratulaAnexa2010Bajio.pdf")), null);
                    ps = new PdfStamper(r, ms);
                    AcroFields af = ps.AcroFields;
                    try
                    {
                        if (rd.Read())
                        {
                            double tasaInteres = 0.00, montorecpropios = 0.00, montosol = 0.00, superficie = 0.00;
                            double aportaciondelproductor = 0.0;
                            double.TryParse(rd["InteresAnual"].ToString(), out tasaInteres);
                            double.TryParse(rd["RecursosPropios"].ToString(), out montorecpropios);
                            double.TryParse(rd["Monto"].ToString(), out montosol);
                            double.TryParse(rd["superficieFinanciada"].ToString(), out superficie);
                            if(new BasePage().IsSistemBanco)
                                double.TryParse(rd["aportaciondelproductor"].ToString(), out aportaciondelproductor);
                            tasaInteres = tasaInteres * 100;
                           

                            af.SetField("txtEmpresaNombre", "COMERCIALIZADORA LAS MARGARITAS SPR DE RL");//
                            af.SetField("txtEmpresaConstitutiva", "16 DIECISEIS DE OCTUBRE DEL AÑO 2001 DOS MIL UNO");
                            af.SetField("txtEmpresaDatosRegistrales", "FOLIO ------ ESCRITURA 14505");
                            af.SetField("txtEmpresaRepresentanteLegal", "NORMA PATRICIA GARIBAY GUTIERREZ");
                            af.SetField("txtEmpresaDatosDelPoder", "ESCRITURA PUBLICA NUMERO 2486 DOS MIL CUATROCIENTOS OCHENTA Y SEIS");
                            af.SetField("txtEmpresaDatosRegistralesDelPoder", "TOMO V QUINTO, LIBRO FOLIOS DEL 9850 AL 9852");
                            af.SetField("txtEmpresaDomicilio", "AV. ALLENDE #2500. AMECA, JALISCO.");
                            af.SetField("txtNombre", rd["name"].ToString().ToUpper()); 
                            af.SetField("txtDomicilio", rd["Dom"].ToString().ToUpper());
                            af.SetField("txtAval1Name", rd["aval1"].ToString().ToUpper()); 
                            af.SetField("txtAval2Name", rd["aval2"].ToString().ToUpper());
                            af.SetField("txtAval1Dom", rd["Aval1Dom"].ToString().ToUpper()); 
                            af.SetField("txtAval2Dom", rd["Aval2Dom"].ToString().ToUpper());
                            af.SetField("txtObjetodelCredito", "COSTO DE CULTIVO DE MAÍZ DE TEMPORAL PARCIAL");
                            StringBuilder objetoDeCredito = new StringBuilder("CREDITO PARA SIEMBRA Y CULTIVO DE:  ");
                            objetoDeCredito.Append(string.Format("{0:n2} HAS DE MAIZ", superficie));
                            af.SetField("txtObjetodelCredito", objetoDeCredito.ToString());
                 
                            af.SetField("txtPlazo", "0 AÑOS " + rd["Plazo"].ToString().ToUpper() + " MESES ");
                            af.SetField("txtMonto", string.Format("{0:C2}", montosol));
                            af.SetField("txtTasaVariable", "NO APLICA");
                            af.SetField("txtTasaFija", string.Format("{0:N2}", tasaInteres) + "%");


                            SqlCommand commUbicacion = new SqlCommand();
                            commUbicacion.CommandText = "SELECT     dbo.SolicitudesSisBancos_SeguroAgricola.solicitudID, dbo.UDF_CONCAT_SEGUROSPREDIOSSISBAN(dbo.SolicitudesSisBancos_SeguroAgricola.solicitudID) "
                            + " FROM         dbo.SegurosAgricolasPrediosSisBancos INNER JOIN "
                            + " dbo.SolicitudesSisBancos_SeguroAgricola ON dbo.SegurosAgricolasPrediosSisBancos.sol_sa_ID = dbo.SolicitudesSisBancos_SeguroAgricola.sol_sa_ID "
                            + " Where dbo.SolicitudesSisBancos_SeguroAgricola.solicitudID = @solicitudID"
                            + " group by dbo.SolicitudesSisBancos_SeguroAgricola.solicitudID;";
                            commUbicacion.Parameters.Add("@solicitudID", SqlDbType.Int).Value = ID;

                            SqlDataReader ubicacionReader = dbFunctions.ExecuteReader(commUbicacion);
                            string ubicacionDep = rd["domicilioDelDeposito"].ToString().ToUpper();
                            if (ubicacionReader != null && ubicacionReader.HasRows && ubicacionReader.Read())
                            {
                                ubicacionDep = ubicacionReader[1].ToString().ToUpper();
                            }
                            if (commUbicacion.Connection != null)
                            {
                                commUbicacion.Connection.Close();
                            }
                            af.SetField("txtDomicilioDep", ubicacionDep);
                            
                            af.SetField("txtNombreyFirma", rd["name"].ToString().ToUpper());
                            af.SetField("txtAcreditante", "COMERCIALIZADORA LAS MARGARITAS S.P.L. DE R.L.");

                            af.SetField("txtAval2", rd["aval2"].ToString().ToUpper()); 
                            af.SetField("txtAval1", rd["aval1"].ToString().ToUpper());
                            af.SetField("txtTestigo1", rd["testigo1"].ToString().ToUpper()); af.SetField("txtTestigo2", rd["testigo2"].ToString().ToUpper());
                            af.SetField("txtFechaFirma", Utils.getFechaconLetra(Utils.Now, userID));
                            af.SetField("textRecursosComplementarios", string.Format("{0:c2}", montorecpropios));
                            StringBuilder recursosComplementarios = new StringBuilder("Total del proyecto: ");
                            if (aportaciondelproductor <= 0)
                                recursosComplementarios.Append(string.Format("{0:C2}", montosol * 1.445417));
                            else
                                recursosComplementarios.Append(string.Format("{0:C2}", montosol + aportaciondelproductor));
                            recursosComplementarios.Append(". Aportación del productor: ");
                            if (aportaciondelproductor <= 0)
                                recursosComplementarios.Append(string.Format("{0:c2}", montosol * 1.445417 - montosol));
                            else
                                recursosComplementarios.Append(string.Format("{0:C2}", aportaciondelproductor));
                            af.SetField("textRecursosComplementariosConLeyenda", recursosComplementarios.ToString().ToUpper());
                            
                            af.SetField("txtNumSolicitud", ID.ToString());
                            af.SetField("txtFecha", Utils.getFechaconLetraSinDia(DateTime.Parse(rd["fecha"].ToString().ToUpper())));

                            af.SetField("txtMunicipioPara", "AMECA, JALISCO A " + Utils.getFechaconLetraSinDia(DateTime.Parse(rd["fecha"].ToString().ToUpper())));

                            af.SetField("txtFechaAbajo", Utils.getFechaconLetraSinDia(DateTime.Parse(rd["fecha"].ToString().ToUpper())));
                            af.SetField("txtNombreFirma", rd["name"].ToString().ToUpper());
                            af.SetField("txtConyuge", rd["conyugue"].ToString().ToUpper());
                            af.SetField("txtFirma1", rd["firmaAutorizada1"].ToString().ToUpper());
                            af.SetField("txtFirma2", rd["firmaAutorizada2"].ToString().ToUpper());
                            af.SetField("txtFirma3", rd["firmaAutorizada3"].ToString().ToUpper());
                            af.SetField("txtFirma4", rd["firmaAutorizada4"].ToString().ToUpper());
                            af.SetField("txtFirma5", rd["firmaAutorizada5"].ToString().ToUpper());
                            af.SetField("txtMontoLetra", Utils.NumeroALetras(montosol.ToString()));
                            af.SetField("txtConceptoSoporteGarantia", rd["ConceptoSoporteGarantia"].ToString().ToUpper() + " " + rd["Descripciondegarantias"].ToString());
                        }                    

                            
                          
                        // make resultant PDF read-only for end-user
                        ps.FormFlattening = true;
                        // forget to close() PdfStamper, you end up with
                        // a corrupted file!
                        ps.Close();
                        res = ms.GetBuffer();
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.PRINT, userID, "NO SE PUDO IMPRIMIR LA CARATULA ANEXA DE LA SOLICITUD No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, "FORMATOSPDF.CS");

                    }
                    finally { if (ps != null) { ps.Close(); } }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.PRINT, userID, "NO SE PUDO IMPRIMIR LA CARATULA ANEXA DE LA SOLICITUD No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, "FORMATOSPDF.CS");

                }
                finally
                {
                    conGaribay.Close();

                }
            }

            return res;
        }

        public static bool imprimeReporteSeguro(int ID, ref string path, ref string sError)
        {
            string sql;
            bool sepudo = true;
            path = Path.GetTempFileName();
            FileStream fS = new FileStream(path, FileMode.Open);

            sql = " SELECT  Productores.telefono,   Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS NombreProductor, Productores.domicilio,  " +
                   " Productores.poblacion + ', ' + Productores.municipio + ', ' + Estados.estado + '.' AS Ciudad, Productores.telefono, Productores.RFC, Productores.CURP, " +
                   " Solicitudes.descParcelas, SegurosAgricolas.Nombre, SegurosAgricolas.CostoPorHectarea, SegurosAgricolas.Descripcion, Solicitudes.hectAseguradas,  " +
                   " Solicitudes.CostoTotalSeguro, Solicitudes.Monto " +
                   " FROM         SegurosAgricolas INNER JOIN " +
                   " solicitud_SeguroAgricola ON SegurosAgricolas.seguroID = solicitud_SeguroAgricola.seguroID RIGHT OUTER JOIN " +
                   " Productores INNER JOIN " +
                   " Solicitudes ON Productores.productorID = Solicitudes.productorID INNER JOIN " +
                   " Estados ON Productores.estadoID = Estados.estadoID ON solicitud_SeguroAgricola.solicitudID = Solicitudes.solicitudID where Solicitudes.solicitudID = @solID ";
            SqlConnection sqlCon = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand sqlCmd = new SqlCommand(sql, sqlCon);
           
            try{
                sqlCon.Open();
                sqlCmd.Parameters.Add("@solID", SqlDbType.Int).Value = ID;
                SqlDataReader rd = sqlCmd.ExecuteReader();
                if(!rd.HasRows){
                    sepudo = false;
                    throw new Exception("AL LEER NO SE ENCONTRARON FILAS QUE LEER EN LA IMPRESIÓN DEL FORMATO DEL SEGURO. SOLID " + ID.ToString());
                    
                }
                
                PdfStamper ps = null;
                // read existing PDF document
                PdfReader r = new PdfReader(new RandomAccessFileOrArray(HttpContext.Current.Request.MapPath("/formatos/FormatoSeguro.pdf")), null);
                ps = new PdfStamper(r, fS);
                // retrieve properties of PDF form w/AcroFields object
                AcroFields af = ps.AcroFields;
                // fill in PDF fields by parameter:
                // 1. field name
                // 2. text to insert
                //String aux = "";
                try
                {
                    while (rd.Read())
                    {
                       
                        //af.SetField("txtEmpresa", "INTEGRADORA DE PRODUCTORES DE JALISCO S.P.R. DE R.L."); 
                        af.SetField("textProductor", rd["NombreProductor"].ToString().ToUpper());
                        double valor;
                        af.SetField("txtTelefono", rd["telefono"].ToString().ToUpper());
                        af.SetField("txtDomicilio", rd["domicilio"].ToString().ToUpper()); af.SetField("txtColoniaCiudad", rd["Ciudad"].ToString().ToUpper()); af.SetField("txtCurp", rd["Curp"].ToString().ToUpper());
                        af.SetField("txtRfc", rd["Rfc"].ToString().ToUpper()); af.SetField("txtDescParcelas", rd["descParcelas"].ToString().ToUpper());
                        af.SetField("txtNombreSeguro", rd["Nombre"].ToString().ToUpper()); valor = 0; double.TryParse(rd["CostoPorHEctarea"].ToString().ToUpper(), out valor); af.SetField("txtMontoporHect", string.Format("{0:c2}",valor));
                        af.SetField("txtDescripcionSeguro", rd["Descripcion"].ToString().ToUpper());
                        valor = 0; double.TryParse(rd["hectAseguradas"].ToString().ToUpper(), out valor); af.SetField("txtHecAseguradas", string.Format("{0:f2}",valor));
                        valor = 0; double.TryParse(rd["CostoTotalSeguro"].ToString().ToUpper(), out valor); af.SetField("txtMontoTotal", string.Format("{0:c2}", valor));
                        af.SetField("txtFirmaProductor", rd["NombreProductor"].ToString().ToUpper());
                        valor = 0; double.TryParse(rd["Monto"].ToString().ToUpper(), out valor); af.SetField("txtLimite", string.Format("{0:c2}", valor));
                        af.SetField("txtAcreditanteTitle", "COMERCIALIZADORA LAS MARGARITAS S.P.R. DE R.L.");
                        af.SetField("txtAcreditante", "COMERCIALIZADORA LAS MARGARITAS S.P.R. DE R.L.");

                        
                      
                    }
                    // make resultant PDF read-only for end-user
                    ps.FormFlattening = true;
                    // forget to close() PdfStamper, you end up with
                    // a corrupted file!
                    ps.Close();
                    //Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, "SE IMPRIMIÓ El REPORTE DEL SEGURO DE LA SOLICITUD No. " + ID);

                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.PRINT, "ERROR AL IMPRIMIR FORMATO SEGURO", ref ex);
                    
                }
                finally { if (ps != null) { fS.Close(); ps.Close(); } }
             

                
            }
            catch(Exception ex){
                sError = ex.Message;
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "ERROR AL IMPRIMIR FORMATO SEGURO", ref ex);
            }
            finally
            {
                sqlCon.Close();

            }
            return sepudo;
        }

        public static string impVariasSol(int[] id)
        {
            string destino = Path.GetTempFileName();
            string[] sources = new string[id.Length];
            for (int i = 0; i < id.Length; i++)
            {

                // add content to existing PDF document with PdfStamper
                string path = Path.GetTempFileName();
                FileStream fS = new FileStream(path, FileMode.Open);

                string sql = "SELECT Solicitudes.creditoID, Productores.Apaterno + ' ' + Productores.Amaterno + ' ' + Productores.nombre as name, Productores.domicilio,  Productores.poblacion + ', ' + Productores.municipio + ', ' + Estados.estado + '.' as ciudadestado, Productores.Rfc, ";
                sql += " Productores.Telefono, Productores.Fax, Productores.email, Productores.CP, Productores.Curp, Solicitudes.Experiencia, Productores.municipio, Solicitudes.Monto, Solicitudes.Superficieasembrar, Solicitudes.RecursosPropios, Solicitudes.Descripciondegarantias, Solicitudes.Valordegarantias, Creditos.cicloID, Solicitudes.plazo, Solicitudes.solicitudID FROM Solicitudes INNER JOIN Creditos ON Creditos.creditoID = Solicitudes.creditoID INNER JOIN Productores ON Productores.productorID = Solicitudes.productorID INNER JOIN Estados ON Productores.estadoID = Estados.estadoID where solicitudID = @solID";
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdsel = new SqlCommand(sql, conGaribay);
                try
                {
                    conGaribay.Open();
                    cmdsel.Parameters.Add("@solID", SqlDbType.Int).Value = id[i];

                    SqlDataReader rd = cmdsel.ExecuteReader();
                    if (!rd.HasRows) { conGaribay.Close(); }
                    PdfStamper ps = null;
                    // read existing PDF document
                    PdfReader r = new PdfReader(new RandomAccessFileOrArray(HttpContext.Current.Request.MapPath("/formatos/FormatoSolicitud.pdf")), null);
                    ps = new PdfStamper(r, fS);
                    // retrieve properties of PDF form w/AcroFields object
                    AcroFields af = ps.AcroFields;
                    // fill in PDF fields by parameter:
                    // 1. field name
                    // 2. text to insert
                    //String aux = "";
                    try
                    {
                        while (rd.Read())
                        {
                            string auxiliar;
                            af.SetField("txtSolID", rd["solicitudID"].ToString());
                            af.SetField("txtFecha", Utils.Now.ToString("dd/MM/yyyy")); af.SetField("txtNombrefirma", rd[1].ToString().ToUpper());
                            af.SetField("txtNombre", rd[1].ToString().ToUpper()); af.SetField("txtContacto", rd[1].ToString().ToUpper()); af.SetField("txtDomicilio", rd[2].ToString().ToUpper());
                            af.SetField("txtColoniaCiudadEstado", rd[3].ToString().ToUpper()); af.SetField("txtRfc", rd[4].ToString().ToUpper());
                            af.SetField("txtTelefono", rd[5].ToString().ToUpper()); af.SetField("txtFax", rd[6].ToString().ToUpper());
                            af.SetField("txtCorreo", rd[7].ToString().ToUpper()); af.SetField("txtCP", rd[8].ToString().ToUpper());
                            af.SetField("txtCurp", rd[9].ToString().ToUpper()); af.SetField("txtExperiencia", rd[10].ToString().ToUpper()); af.SetField("txtMunicipioEmpresa", rd[11].ToString().ToUpper());
                            af.SetField("txtMonto", string.Format("{0:c}", float.Parse(rd[12].ToString().ToUpper()))); af.SetField("txtSup", rd[13].ToString().ToUpper());
                            af.SetField("txtRecursosCredito", string.Format("{0:c}", float.Parse(rd[12].ToString().ToUpper()))); af.SetField("txtRecursosPropios", string.Format("{0:c}", float.Parse(rd[14].ToString().ToUpper())));
                            af.SetField("txtDescGarantias", rd[15].ToString().ToUpper()); af.SetField("txtValorGarantias", string.Format("{0:c}", float.Parse(rd[16].ToString().ToUpper())));
                            af.SetField("txtValorTotalGarantias", string.Format("{0:c}", float.Parse(rd[16].ToString().ToUpper())));
                            af.SetField("txtPlazo", rd["plazo"].ToString().ToUpper());
                        }
                        // make resultant PDF read-only for end-user
                        ps.FormFlattening = true;
                        // forget to close() PdfStamper, you end up with
                        // a corrupted file!
                        ps.Close();
                        //Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, "SE IMPRIMIÓ LA SOLICITUD NÚMERO : " + ID);
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.PRINT, "ERROR AL IMPRIMIR LA SOLICITUD NÚMERO : " + id[i], ref ex);
                    }
                    finally { if (ps != null) { fS.Close(); ps.Close(); } }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.PRINT, "ERROR AL IMPRIMIR LA SOLICITUD NÚMERO : " + id[i], ref ex);
                }
                finally
                {
                    conGaribay.Close();
                }
                sources[i] = path;
            }
            MergeFiles(destino, sources);
            return destino;
        }

        public static void MergeFiles(string destinationFile, string[] sourceFiles)
        {
            try
            {
                int f = 0;
                // we create a reader for a certain document
                PdfReader reader = new PdfReader(sourceFiles[f]);
                // we retrieve the total number of pages
                int n = reader.NumberOfPages;
                Console.WriteLine("There are " + n + " pages in the original file.");
                // step 1: creation of a document-object
                Document document = new Document(reader.GetPageSizeWithRotation(1));
                // step 2: we create a writer that listens to the document
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationFile, FileMode.Create));
                // step 3: we open the document
                document.Open();
                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;
                int rotation;
                // step 4: we add content
                while (f < sourceFiles.Length)
                {
                    int i = 0;
                    while (i < n)
                    {
                        i++;
                        document.SetPageSize(reader.GetPageSizeWithRotation(i));
                        document.NewPage();
                        page = writer.GetImportedPage(reader, i);
                        rotation = reader.GetPageRotation(i);
                        if (rotation == 90 || rotation == 270)
                        {
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        }
                        else
                        {
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        }
                        Console.WriteLine("Processed page " + i);
                    }
                    f++;
                    if (f < sourceFiles.Length)
                    {
                        reader = new PdfReader(sourceFiles[f]);
                        // we retrieve the total number of pages
                        n = reader.NumberOfPages;
                        Console.WriteLine("There are " + n + " pages in the original file.");
                    }
                }
                // step 5: we close the document
                document.Close();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Console.Error.WriteLine(e.StackTrace);
            }
            finally
            {
                try
                {

                    foreach (string str in sourceFiles)
                    {
                        File.Delete(str);
                    }
                }
                catch(Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.DELETE, "ERROR AL BORRAR ARCHIVOS TEMP", ref ex);
                }
            }
        }

        public static string imprimeSolicitud(int ID)
        {
            // add content to existing PDF document with PdfStamper
            string path = Path.GetTempFileName();
            FileStream fS = new FileStream(path, FileMode.Open);

            string sql = "SELECT Solicitudes.creditoID, Productores.Apaterno + ' ' + Productores.Amaterno + ' ' + Productores.nombre as name, Productores.domicilio,  Productores.poblacion + ', ' + Productores.municipio + ', ' + Estados.estado + '.' as ciudadestado, Productores.Rfc, ";
            sql += " Productores.Telefono, Productores.Fax, Productores.email, Productores.CP, Productores.Curp, Solicitudes.Experiencia, Productores.municipio, Solicitudes.Monto, Solicitudes.Superficieasembrar, Solicitudes.RecursosPropios, Solicitudes.Descripciondegarantias, Solicitudes.Valordegarantias, ";
            sql += " Creditos.cicloID, Solicitudes.plazo, Solicitudes.solicitudID, Solicitudes.fecha, Productores.poblacion, Productores.CP, Ciclos.Montoporhectarea, Users.Nombre as usuario, Solicitudes.aval1, Solicitudes.Aval1Dom, Solicitudes.UbicacionGarantia, Productores.municipio, Productores.telefono FROM Solicitudes INNER JOIN Creditos ON Creditos.creditoID = Solicitudes.creditoID INNER JOIN Productores ON Productores.productorID = Solicitudes.productorID INNER JOIN Estados ON Productores.estadoID = Estados.estadoID INNER JOIN Ciclos ON Ciclos.cicloID = Creditos.cicloID INNER JOIN Users on Users.userId = Solicitudes.userId  where solicitudID = @solID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdsel = new SqlCommand(sql,conGaribay);
            try{
                conGaribay.Open();
                cmdsel.Parameters.Add("@solID", SqlDbType.Int).Value = ID;
                
                SqlDataReader rd= cmdsel.ExecuteReader();
                if (!rd.HasRows) { conGaribay.Close();  }
                 PdfStamper ps = null;
                 // read existing PDF document
                 PdfReader r = new PdfReader(new RandomAccessFileOrArray(HttpContext.Current.Request.MapPath("/formatos/FormatoSolicitudNuevo.pdf")), null);
                 ps = new PdfStamper(r, fS );
                 // retrieve properties of PDF form w/AcroFields object
                 AcroFields af = ps.AcroFields;
                 // fill in PDF fields by parameter:
                 // 1. field name
                 // 2. text to insert
                 //String aux = "";
                 try
                 {
                    while (rd.Read())
                    {
                        //string auxiliar;
                        af.SetField("txtSolID", rd["solicitudID"].ToString());
                        af.SetField("txtFecha", Utils.converttoshortFormatfromdbFormat(rd["fecha"].ToString())); 
                        af.SetField("txtNombrefirma", rd[1].ToString().ToUpper());
                        af.SetField("txtNombre", rd[1].ToString().ToUpper()); 
                        af.SetField("txtContacto", rd[1].ToString().ToUpper()); 
                        af.SetField("txtDomicilio", rd[2].ToString().ToUpper());
                        af.SetField("txtColoniaCiudadEstado", rd[3].ToString().ToUpper()); 
                        af.SetField("txtRfc", rd[4].ToString().ToUpper());
                        af.SetField("txtTelefono", rd[5].ToString().ToUpper()); 
                        af.SetField("txtFax", rd[6].ToString().ToUpper());
                        af.SetField("txtCorreo", rd[7].ToString().ToUpper()); 
                        af.SetField("txtCP", rd[8].ToString().ToUpper());
                        af.SetField("txtCurp", rd[9].ToString().ToUpper()); 
                        af.SetField("txtExperiencia", rd[10].ToString().ToUpper()); 
                        af.SetField("txtMunicipioEmpresa", rd[11].ToString().ToUpper());
                        af.SetField("txtMonto", string.Format("{0:c}", float.Parse(rd[12].ToString().ToUpper()))); 
                        af.SetField("txtSup", rd[13].ToString().ToUpper());
                        af.SetField("txtRecursosCredito", string.Format("{0:c}", float.Parse(rd[12].ToString().ToUpper()))); 
                        af.SetField("txtRecursosPropios", string.Format("{0:c}",float.Parse(rd[14].ToString().ToUpper())));
                        af.SetField("txtDescGarantias", rd[15].ToString().ToUpper()); 
                        af.SetField("txtValorGarantias", string.Format("{0:c}", float.Parse(rd[16].ToString().ToUpper())));
                        af.SetField("txtValorTotalGarantias", string.Format("{0:c}", float.Parse(rd[16].ToString().ToUpper())));
                        af.SetField("txtPlazo", rd["plazo"].ToString().ToUpper());
                        af.SetField("txtFechaLargaHoja2",  Utils.getFechaconLetraSinDia(DateTime.Parse(rd["fecha"].ToString())));
                        af.SetField("txtNombreHoja2", rd["name"].ToString().ToUpper());
                        af.SetField("txtDomicilioHoja2", rd["domicilio"].ToString().ToUpper());
                        af.SetField("txtCPHoja2", rd["cp"].ToString().ToUpper());
                        af.SetField("txtLocalidadHoja2", rd["poblacion"].ToString().ToUpper());
                         af.SetField("txtMunicipoHoja2", rd["municipio"].ToString().ToUpper());
                         af.SetField("txtTelefonoHoja2", rd["telefono"].ToString().ToUpper());
                        
                            
                        double doubleAux =0;
                        double.TryParse(rd["Montoporhectarea"].ToString(),out doubleAux);
                        af.SetField("txtCostoPorHaHoja2", string.Format("{0:C2}",doubleAux));
                        int numhectareas = 0;
                        int.TryParse(rd["Superficieasembrar"].ToString(), out numhectareas);
                        af.SetField("txtNumHectareas", string.Format("{0:n2}",numhectareas));
                        doubleAux =0;
                        double.TryParse(rd["Monto"].ToString(),out doubleAux);
                        af.SetField("txtMontoCreditoHoja2", string.Format("{0:C2}", doubleAux));
                        doubleAux = 0;
                        double.TryParse(rd["Valordegarantias"].ToString(), out doubleAux);
                        StringBuilder txtFullGarantias = new StringBuilder();
                        if(doubleAux>0)
                        {
                            txtFullGarantias.Append("Valor aproximado: " + string.Format("{0:c2}", doubleAux));
                        }
                        if (rd["Descripciondegarantias"] != null && !string.IsNullOrEmpty(rd["Descripciondegarantias"].ToString()) && rd["Descripciondegarantias"].ToString()!="")
                        {
                            txtFullGarantias.Append("Descripción de garantías: " + rd["Descripciondegarantias"].ToString());
                        }
                        if (rd["UbicacionGarantia"] != null && !string.IsNullOrEmpty(rd["UbicacionGarantia"].ToString()) && rd["Descripciondegarantias"].ToString()!="")
                        {
                            txtFullGarantias.Append("Ubicación de garantías: " + rd["UbicacionGarantia"].ToString());
                        }

                        af.SetField("txtGarantias", txtFullGarantias.ToString());
                        af.SetField("txtAvalHoja2", rd["aval1"].ToString().ToUpper());
                        af.SetField("txtDomicilioAvalHoja2", rd["Aval1Dom"].ToString().ToUpper());                        
                        af.SetField("txtUsuarioHoja2", rd["usuario"].ToString().ToUpper());
                        
  
                    }
                    // make resultant PDF read-only for end-user
                    ps.FormFlattening = true;
                    // forget to close() PdfStamper, you end up with
                    // a corrupted file!
                    ps.Close();
                    //Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, "SE IMPRIMIÓ LA SOLICITUD NÚMERO : " + ID);
                }
                catch (Exception ex){
                    Logger.Instance.LogException(Logger.typeUserActions.PRINT, "ERROR AL IMPRIMIR LA SOLICITUD NÚMERO : "+ ID, ref ex);
                }
                 finally { if (ps != null) { fS.Close(); ps.Close(); } }
          }
          catch(Exception ex)
          {
              Logger.Instance.LogException(Logger.typeUserActions.PRINT, "ERROR AL IMPRIMIR LA SOLICITUD NÚMERO : " + ID, ref ex);
          }
          finally
          {
           conGaribay.Close();
          }

           

         return path;   

      }
        public static Byte [] imprimeSolicitud2010(int ID)
        {
            // add content to existing PDF document with PdfStamper
//             string path = Path.GetTempFileName();
//             FileStream fS = new FileStream(path, FileMode.Open);
            MemoryStream ms = new MemoryStream();

            string sql = "SELECT     Solicitudes.creditoID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS name, Productores.domicilio, " 
                      + "  Productores.poblacion + ', ' + Productores.municipio + ', ' + Estados.estado + '.' AS ciudadestado, Productores.RFC, Productores.telefono, Productores.fax, "
                      + " Productores.email, Productores.CP, Productores.CURP, Solicitudes.Experiencia, Productores.municipio, Solicitudes.Monto, Solicitudes.Superficieasembrar, "
                      + " Solicitudes.RecursosPropios, Solicitudes.Descripciondegarantias, Solicitudes.Valordegarantias, Creditos.cicloID, Solicitudes.Plazo, Solicitudes.solicitudID, "
                      + " Solicitudes.fecha, Productores.poblacion, Productores.CP AS Expr1, Ciclos.Montoporhectarea, Users.Nombre AS usuario, Solicitudes.aval1, Solicitudes.Aval1Dom, "
                      + " Solicitudes.aval2, Solicitudes.Aval2Dom, Solicitudes.UbicacionGarantia, Productores.municipio, Productores.telefono, Regimenes.Regimen, "
                      + " EstadosCiviles.EstadoCivil, ConceptoSoporteGarantia, montoSoporteGarantia, Solicitudes.otrosPasivosMonto, Solicitudes.otrosPasivosAQuienLeDebe 	"
                      + " FROM         Solicitudes INNER JOIN "
                      + " Creditos ON Creditos.creditoID = Solicitudes.creditoID INNER JOIN "
                      + " Productores ON Productores.productorID = Solicitudes.productorID INNER JOIN "
                      + " Estados ON Productores.estadoID = Estados.estadoID INNER JOIN "
                      + " Ciclos ON Ciclos.cicloID = Creditos.cicloID INNER JOIN "
                      + " Users ON Users.userID = Solicitudes.userID LEFT OUTER JOIN  "
                      + " Regimenes ON Productores.regimenID = Regimenes.regimenID LEFT OUTER JOIN "
                      + " EstadosCiviles ON Productores.estadocivilID = EstadosCiviles.estadoCivilID  "
                      + " where solicitudID = @solID";
            if (new BasePage().IsSistemBanco)
            {
                sql = dbFunctions.UpdateSDSForSisBanco(sql);
            }
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdsel = new SqlCommand(sql, conGaribay);
            try
            {
                conGaribay.Open();
                cmdsel.Parameters.Add("@solID", SqlDbType.Int).Value = ID;

                SqlDataReader rd = cmdsel.ExecuteReader();
                if (!rd.HasRows) { conGaribay.Close(); }
                PdfStamper ps = null;
                // read existing PDF document
                PdfReader r = new PdfReader(new RandomAccessFileOrArray(HttpContext.Current.Request.MapPath("/formatos/FormatoSolicitud2010CONAUTORIZACION.pdf")), null);
                ps = new PdfStamper(r, ms);
                // retrieve properties of PDF form w/AcroFields object
                AcroFields af = ps.AcroFields;
                // fill in PDF fields by parameter:
                // 1. field name
                // 2. text to insert
                //String aux = "";
                try
                {
                    while (rd.Read())
                    {
                        //string auxiliar;
                        string empresa = "COMERCIALIZADORA LAS MARGARITAS S.P.R. DE R.L.";
                        af.SetField("txtActividad", "AGRICULTURA");
                        af.SetField("txtEmpresa", empresa);
                        af.SetField("txtSolID", rd["solicitudID"].ToString());
                        af.SetField("txtFecha", Utils.getFechaconLetraSinDia(DateTime.Parse(rd["fecha"].ToString())));
                        af.SetField("txtNombrefirma", rd[1].ToString().ToUpper());
                        af.SetField("txtNombre", rd[1].ToString().ToUpper());
                        af.SetField("txtContacto", rd[1].ToString().ToUpper());
                        af.SetField("txtDomicilio", rd[2].ToString().ToUpper());
                        af.SetField("txtColoniaCiudadEstado", rd[3].ToString().ToUpper());
                        af.SetField("txtRfc", rd[4].ToString().ToUpper());
                        af.SetField("txtTelefono", rd[5].ToString().ToUpper());
                        af.SetField("txtFax", rd[6].ToString().ToUpper());
                        af.SetField("txtCorreo", rd[7].ToString().ToUpper());
                        af.SetField("txtCP", rd[8].ToString().ToUpper());
                        af.SetField("txtCurp", rd[9].ToString().ToUpper());
                        af.SetField("txtExperiencia", rd[10].ToString().ToUpper());
                        af.SetField("txtMunicipioEmpresa", rd[11].ToString().ToUpper());
                        af.SetField("txtMonto", string.Format("{0:c}", float.Parse(rd[12].ToString().ToUpper())));
                        af.SetField("txtSup", rd[13].ToString().ToUpper());
                        af.SetField("txtRecursosCredito", string.Format("{0:c}", float.Parse(rd[12].ToString().ToUpper())));
                        af.SetField("txtRecursosPropios", string.Format("{0:c}", float.Parse(rd[14].ToString().ToUpper())));
                        af.SetField("txtDescGarantias", rd["ConceptoSoporteGarantia"].ToString().ToUpper() + " " + rd["Descripciondegarantias"].ToString().ToUpper());
                        af.SetField("txtValorGarantias", string.Format("{0:c}", float.Parse(rd["montoSoporteGarantia"].ToString().ToUpper())));
                        af.SetField("txtValorTotalGarantias", string.Format("{0:c}", float.Parse(rd["montoSoporteGarantia"].ToString().ToUpper())));
                        af.SetField("txtPlazo", rd["plazo"].ToString().ToUpper());
                        af.SetField("txtAval1", rd["aval1"].ToString().ToUpper());
                        af.SetField("txtAvalFirma", rd["aval1"].ToString().ToUpper());                                              
                        af.SetField("txtAval2", rd["aval2"].ToString().ToUpper());
                        af.SetField("txtAvalDom1", rd["Aval1Dom"].ToString().ToUpper());
                        af.SetField("txtAvalDom2", rd["Aval2Dom"].ToString().ToUpper());
                        af.SetField("txtFechaLargaHoja2", Utils.getFechaconLetraSinDia(DateTime.Parse(rd["fecha"].ToString())));
                        af.SetField("txtNombreHoja2", rd["name"].ToString().ToUpper());
                        af.SetField("txtDomicilioHoja2", rd["domicilio"].ToString().ToUpper());
                        af.SetField("txtCPHoja2", rd["cp"].ToString().ToUpper());
                        af.SetField("txtLocalidadHoja2", rd["poblacion"].ToString().ToUpper());
                        af.SetField("txtMunicipoHoja2", rd["municipio"].ToString().ToUpper());
                        af.SetField("txtTelefonoHoja2", rd["telefono"].ToString().ToUpper());
                        double montoPasivos = 0;
                        double.TryParse(rd["otrosPasivosMonto"].ToString(), out montoPasivos);
                        af.SetField("txtMontoPasivosFinancieros", string.Format("{0:c2}", montoPasivos));
                        af.SetField("txtDescPasivosFinancieros", rd["otrosPasivosAQuienLeDebe"].ToString());
                     
                        string estadoYRegimen = "";
                        if(rd["EstadoCivil"]!=null && !(string.IsNullOrEmpty(rd["EstadoCivil"].ToString()) && rd["EstadoCivil"].ToString()!=""))
                        {
                            estadoYRegimen = rd["EstadoCivil"].ToString();
                            if(rd["Regimen"]!=null && !(string.IsNullOrEmpty(rd["Regimen"].ToString()) && rd["Regimen"].ToString()!=""))
                            {
                                estadoYRegimen += " - " + rd["Regimen"].ToString();
                            }
                            af.SetField("txtEstadoCivilRegimen", estadoYRegimen.ToUpper());
                       
                        }

                        double doubleAux = 0;
                        double.TryParse(rd["Montoporhectarea"].ToString(), out doubleAux);
                        af.SetField("txtCostoPorHaHoja2", string.Format("{0:C2}", doubleAux));
                        int numhectareas = 0;
                        int.TryParse(rd["Superficieasembrar"].ToString(), out numhectareas);
                        af.SetField("txtNumHectareas", string.Format("{0:n2}", numhectareas));
                        doubleAux = 0;
                        double.TryParse(rd["Monto"].ToString(), out doubleAux);
                        af.SetField("txtMontoCreditoHoja2", string.Format("{0:C2}", doubleAux));
                        doubleAux = 0;
                        double.TryParse(rd["Valordegarantias"].ToString(), out doubleAux);
                        StringBuilder txtFullGarantias = new StringBuilder();
                        if (doubleAux > 0)
                        {
                            txtFullGarantias.Append("Valor aproximado: " + string.Format("{0:c2}", doubleAux));
                        }
                        if (rd["Descripciondegarantias"] != null && !string.IsNullOrEmpty(rd["Descripciondegarantias"].ToString()) && rd["Descripciondegarantias"].ToString() != "")
                        {
                            txtFullGarantias.Append("Descripción de garantías: " + rd["Descripciondegarantias"].ToString());
                        }
                        if (rd["UbicacionGarantia"] != null && !string.IsNullOrEmpty(rd["UbicacionGarantia"].ToString()) && rd["Descripciondegarantias"].ToString() != "")
                        {
                            txtFullGarantias.Append("Ubicación de garantías: " + rd["UbicacionGarantia"].ToString());
                        }

                        af.SetField("txtGarantias", txtFullGarantias.ToString());
                        af.SetField("txtAvalHoja2", rd["aval1"].ToString().ToUpper());
                        af.SetField("txtDomicilioAvalHoja2", rd["Aval1Dom"].ToString().ToUpper());
                        af.SetField("txtUsuarioHoja2", rd["usuario"].ToString().ToUpper());


                    }
                    // make resultant PDF read-only for end-user
                    ps.FormFlattening = true;
                    // forget to close() PdfStamper, you end up with
                    // a corrupted file!
                    ps.Close();
                    //Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, "SE IMPRIMIÓ LA SOLICITUD NÚMERO : " + ID);
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.PRINT, "ERROR AL IMPRIMIR LA SOLICITUD NÚMERO : " + ID, ref ex);
                }
                finally { if (ps != null) { /*fS.Close();*/ ps.Close(); } }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.PRINT, "ERROR AL IMPRIMIR LA SOLICITUD NÚMERO : " + ID, ref ex);
            }
            finally
            {
                conGaribay.Close();
            }



            return ms.GetBuffer();

        }
   
        public static byte[] imprimeContrato(int ID, int userID)
        {
            // add content to existing PDF document with PdfStamper
            MemoryStream ms = new MemoryStream();
            string sql = "SELECT Productores.Apaterno + ' ' + Productores.Amaterno + ' ' + Productores.nombre as name, Solicitudes.testigo1, Solicitudes.testigo2, Solicitudes.aval1, Solicitudes.aval2, Solicitudes.Fecha ";
            sql += " FROM Solicitudes INNER JOIN Productores ON Productores.productorID = Solicitudes.productorID where solicitudID = @solID";
            if (new BasePage().IsSistemBanco)
            {
                sql = dbFunctions.UpdateSDSForSisBanco(sql);
            }
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdsel = new SqlCommand(sql, conGaribay);
            try
            {
                conGaribay.Open();
                cmdsel.Parameters.Add("@solID", SqlDbType.Int).Value = ID;

                SqlDataReader rd = cmdsel.ExecuteReader();
                if (!rd.HasRows) {  }
                PdfStamper ps = null;
                // read existing PDF document
                PdfReader r = new PdfReader(new RandomAccessFileOrArray(HttpContext.Current.Request.MapPath("/formatos/FormatoContrato.pdf")), null);
                ps = new PdfStamper(r, ms);
                // retrieve properties of PDF form w/AcroFields object
                AcroFields af = ps.AcroFields;
                // fill in PDF fields by parameter:
                // 1. field name
                // 2. text to insert
                try
                {
                    if (rd.HasRows && rd.Read())
                    {
                        af.SetField("txtFecha","AMECA, JALISCO " + Utils.getFechaconLetra(DateTime.Parse(rd["fecha"].ToString()),userID));
                        af.SetField("txtNombre", rd["name"].ToString().ToUpper());
                        af.SetField("txtAcreditante", "COMERCIALIZADORA LAS MARGARITAS S.P.R. DE R.L.");
                        af.SetField("txtTestigo1", rd["testigo1"].ToString().ToUpper());
                        af.SetField("txtTestigo2", rd["testigo2"].ToString().ToUpper());
                        af.SetField("txtAval1", rd["aval1"].ToString().ToUpper());
                        af.SetField("txtAval2", rd["aval2"].ToString().ToUpper());
                    }
                    // make resultant PDF read-only for end-user
                    ps.FormFlattening = true;
                    // forget to close() PdfStamper, you end up with
                    // a corrupted file!
                    ps.Close();
                    
                    
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.PRINT, "ERROR AL IMPRIMIR EL CONTRATO DE LA SOLICITUD NÚMERO : " + ID, ref ex);
                }
                finally { if (ps != null) { ps.Close(); /*fS.Close();*/ } }
            }
            catch (Exception ex)
            {
                //Logger.Instance.LogException(Logger.typeUserActions.PRINT, "ERROR AL IMPRIMIR EL CONTRATP LA SOLICITUD NÚMERO : " + ID, ref ex);
               // return path;
            }
            finally
            {
                conGaribay.Close();

            }



            return ms.GetBuffer();

        }
        #region Print Seguro

        public static Byte[] imprimeSeguro(int ID)
        {
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                string sql = "SELECT     Solicitudes.fecha, SegurosAgricolas.tituloPrint, "
                + "LTRIM(Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre) AS Productor, "
                + " Productores.CURP, Productores.RFC, Productores.domicilio, "
                + " LTRIM(Productores.poblacion + ', ' + Productores.municipio + ', ' + Estados.estado) AS Domicilio2, "
                + " Productores.telefono, SegurosAgricolas.esquema, SegurosAgricolas.rendimientoProtegido, "
                + " SegurosAgricolas.PrecioPactado, SegurosAgricolas.TipoDeAjuste, SegurosAgricolas.RiesgosCubiertos, "
                + " SegurosAgricolas.Deducible, SegurosAgricolas.CostoPorHectarea, SegurosAgricolas.TextoAPagar, "
                + " solicitud_SeguroAgricola.hectAseguradas, solicitud_SeguroAgricola.CostoTotalSeguro, "
                + " solicitud_SeguroAgricola.sol_sa_ID FROM Solicitudes INNER JOIN"
                + " solicitud_SeguroAgricola ON Solicitudes.solicitudID = solicitud_SeguroAgricola.solicitudID "
                + " INNER JOIN SegurosAgricolas ON solicitud_SeguroAgricola.seguroID = "
                + " SegurosAgricolas.seguroID INNER JOIN Productores ON Solicitudes.productorID = "
                + " Productores.productorID INNER JOIN Estados ON Productores.estadoID = Estados.estadoID "
                + " WHERE (solicitud_SeguroAgricola.sol_sa_ID = @sol_sa_ID) ";
                if (new BasePage().IsSistemBanco)
                {
                    sql = dbFunctions.UpdateSDSForSisBanco(sql);
                }
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdsel = new SqlCommand(sql, conGaribay);
                try
                {
                    conGaribay.Open();
                    cmdsel.Parameters.Add("@sol_sa_ID", SqlDbType.Int).Value = ID;
                    SqlDataReader rd = cmdsel.ExecuteReader();
                    if (!rd.HasRows) { conGaribay.Close(); }
                    PdfStamper ps = null;
                    PdfReader r = new PdfReader(new RandomAccessFileOrArray(HttpContext.Current.Request.MapPath("/formatos/FormatoSeguroFrente.pdf")), null);
                    ps = new PdfStamper(r, ms);
                    AcroFields af = ps.AcroFields;
                    try
                    {
                        if (rd.Read())
                        {
                            af.SetField("txtFecha",  "AMECA, JALISCO A " + Utils.getFechaconLetraSinDia(DateTime.Parse(rd["fecha"].ToString())).ToUpper());
                            af.SetField("txtTipoDeSeguro", rd["tituloPrint"].ToString().ToUpper());
                            af.SetField("txtNombreCompleto", rd["Productor"].ToString().ToUpper());
                            af.SetField("txtCURP", rd["CURP"].ToString().ToUpper());
                            af.SetField("txtRFC", rd["RFC"].ToString().ToUpper());
                            af.SetField("txtDomicilio", rd["domicilio"].ToString().ToUpper());
                            af.SetField("txtColonia", rd["Domicilio2"].ToString().ToUpper());
                            af.SetField("txtTelefono", rd["telefono"].ToString().ToUpper());
                            af.SetField("txtEsquema", rd["esquema"].ToString().ToUpper());
                            af.SetField("txtRendimiento", rd["rendimientoProtegido"].ToString().ToUpper());
                            af.SetField("txtPrecioPactado", rd["PrecioPactado"].ToString().ToUpper());
                            af.SetField("txtTipoDeAjuste", rd["TipoDeAjuste"].ToString().ToUpper());
                            af.SetField("txtRiesgos", rd["RiesgosCubiertos"].ToString().ToUpper());
                            af.SetField("txtDeducible", rd["Deducible"].ToString().ToUpper());
                            double costoxHectarea = 0;
                            if(!double.TryParse(rd["CostoPorHectarea"].ToString(), out costoxHectarea))
                                costoxHectarea = 0;
                            af.SetField("txtCostoXHec", string.Format("{0:C2} PESOS",costoxHectarea));
                            af.SetField("txtPagoAlProd", rd["TextoAPagar"].ToString().ToUpper());
                            double SupAsegu = 0;
                            if (!double.TryParse(rd["hectAseguradas"].ToString(), out SupAsegu))
                            {
                                SupAsegu = 0;
                            }
                            af.SetField("txtSuperficieAsegurada", string.Format("{0:n2} Has", SupAsegu));
                            double costototal = 0;
                            if (!double.TryParse(rd["CostoTotalSeguro"].ToString(), out costototal))
                            {
                                costototal = 0;
                            }
                            af.SetField("txtCostoTotal", string.Format("{0:C2} PESOS", costototal));
                            af.SetField("txtSumaAseguTotal", string.Format("{0:C2} PESOS", 12000 * SupAsegu));

                        }
                        // make resultant PDF read-only for end-user
                        ps.FormFlattening = true;
                        // forget to close() PdfStamper, you end up with
                        // a corrupted file!
                        ps.Close();

                        #region Print Predios data
                        
                        //SELECT     Folio, Nombre, Superficie, Ubicacion, sol_sa_ID FROM SegurosAgricolasPredios WHERE     (sol_sa_ID = @sol_sa_ID)
                        res = ms.GetBuffer();
                        // we create a reader for a certain document
                        PdfReader reader = new PdfReader(res);

                        MemoryStream msFinal = new MemoryStream();
                        // we retrieve the total number of pages                        
                        // step 1: creation of a document-object
                        Document document = new Document(reader.GetPageSizeWithRotation(1));
                        // step 2: we create a writer that listens to the document
                        PdfWriter writer = PdfWriter.GetInstance(document, msFinal);
                        // step 3: we open the document
                        document.Open();
                        PdfContentByte cb = writer.DirectContent;
                        PdfImportedPage page;
                        // step 4: we add content
                        document.SetPageSize(reader.GetPageSizeWithRotation(1));
                        document.NewPage();
                        page = writer.GetImportedPage(reader, 1);
                        
                        int rotation;
                        rotation = reader.GetPageRotation(1);
                        if (rotation == 90 || rotation == 270)
                        {
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(1).Height);
                        }
                        else
                        {
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        }

                        SqlCommand comm = new SqlCommand();
                        comm.CommandText = "SELECT     count(*) "
                        + " FROM SegurosAgricolasPredios WHERE     (sol_sa_ID = @sol_sa_ID);";
                        if (new BasePage().IsSistemBanco)
                        {
                            comm.CommandText = dbFunctions.UpdateSDSForSisBanco(comm.CommandText);
                        }
                        comm.Parameters.Add("@sol_sa_ID", SqlDbType.Int).Value = ID;
                        int cantidad;
                        cantidad = dbFunctions.GetExecuteIntScalar(comm, 0);
                        for (int i = 0; i < cantidad; i +=3 )
                        {
                            Byte[] b = FormatosPdf.imprimePrediosSeguro(ID, i);
                            reader = new PdfReader(b);
                            document.SetPageSize(reader.GetPageSizeWithRotation(1));
                            document.NewPage();
                            page = writer.GetImportedPage(reader, 1);
                            rotation = reader.GetPageRotation(1);
                            if (rotation == 90 || rotation == 270)
                            {
                                cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(1).Height);
                            }
                            else
                            {
                                cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                            }
                        }
                        if (cantidad == 0)
                        {
                            Byte[] b = FormatosPdf.imprimePrediosSeguro(ID, 0);
                            reader = new PdfReader(b);
                            document.SetPageSize(reader.GetPageSizeWithRotation(1));
                            document.NewPage();
                            page = writer.GetImportedPage(reader, 1);
                            rotation = reader.GetPageRotation(1);
                            if (rotation == 90 || rotation == 270)
                            {
                                cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(1).Height);
                            }
                            else
                            {
                                cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                            }
                        }
                        document.Close();
                        res = msFinal.GetBuffer();

                        #endregion


                        
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.PRINT, new BasePage().UserID, "NO SE PUDO IMPRIMIR LA CARATULA ANEXA DE LA SOLICITUD No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, "FORMATOSPDF.CS");

                    }
                    finally { if (ps != null) { ps.Close(); } }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.PRINT, new BasePage().UserID, "NO SE PUDO IMPRIMIR LA CARATULA ANEXA DE LA SOLICITUD No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, "FORMATOSPDF.CS");

                }
                finally
                {
                    conGaribay.Close();

                }
            }

            return res;
        }

        public static Byte[] imprimePrediosSeguro(int ID, int offsetPredio)
            {
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                string sql = "SELECT     Folio, Nombre, Superficie, Ubicacion "
                 + " FROM SegurosAgricolasPredios WHERE     (sol_sa_ID = @sol_sa_ID)";
                if (new BasePage().IsSistemBanco)
                {
                    sql = dbFunctions.UpdateSDSForSisBanco(sql);
                }
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdsel = new SqlCommand(sql, conGaribay);
                try
                {
                    conGaribay.Open();
                    cmdsel.Parameters.Add("@sol_sa_ID", SqlDbType.Int).Value = ID;
                    SqlDataReader rd = cmdsel.ExecuteReader();
                    bool bTodavia = rd.HasRows;
                    //if (!rd.HasRows) { conGaribay.Close(); }
                    int i = 0;
                    while (i < offsetPredio && bTodavia)
                    {
                        bTodavia = rd.Read();
                        i++;
                    }
                    PdfStamper ps = null;
                    PdfReader r = new PdfReader(new RandomAccessFileOrArray(HttpContext.Current.Request.MapPath("/formatos/FormatoSeguroReverso.pdf")), null);
                    ps = new PdfStamper(r, ms);
                    AcroFields af = ps.AcroFields;
                    try
                    {
                        if (rd.HasRows && rd.Read())
                        {
                            af.SetField("txtFolio1", rd["Folio"].ToString().ToUpper());
                            af.SetField("txtNombre1", rd["Nombre"].ToString().ToUpper());
                            double superficie = 0;
                            if (!double.TryParse(rd["Superficie"].ToString(), out superficie))
                            {
                                superficie = 0;
                            }
                            af.SetField("txtSuperficie1", string.Format("{0:n2}", superficie));
                            af.SetField("txtUbicacion1", rd["Ubicacion"].ToString().ToUpper());
                            if (rd.Read())
                            {
                                af.SetField("txtFolio2", rd["Folio"].ToString().ToUpper());
                                af.SetField("txtNombre2", rd["Nombre"].ToString().ToUpper());
                                superficie = 0;
                                if (!double.TryParse(rd["Superficie"].ToString(), out superficie))
                                {
                                    superficie = 0;
                                }
                                af.SetField("txtSuperficie2", string.Format("{0:n2}", superficie));
                                af.SetField("txtUbicacion2", rd["Ubicacion"].ToString().ToUpper());
                                if (rd.Read())
                                {
                                    af.SetField("txtFolio3", rd["Folio"].ToString().ToUpper());
                                    af.SetField("txtNombre3", rd["Nombre"].ToString().ToUpper());
                                    superficie = 0;
                                    if (!double.TryParse(rd["Superficie"].ToString(), out superficie))
                                    {
                                        superficie = 0;
                                    }
                                    af.SetField("txtSuperficie3", string.Format("{0:n2}", superficie));
                                    af.SetField("txtUbicacion3", rd["Ubicacion"].ToString().ToUpper());
                                }
                            }
                        }
                        // make resultant PDF read-only for end-user
                        ps.FormFlattening = true;
                        // forget to close() PdfStamper, you end up with
                        // a corrupted file!
                        ps.Close();

                        res = ms.GetBuffer();
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.PRINT, new BasePage().UserID, "NO SE PUDO IMPRIMIR LA CARATULA ANEXA DE LA SOLICITUD No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, "FORMATOSPDF.CS");

                    }
                    finally { if (ps != null) { ps.Close(); } }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.PRINT, new BasePage().UserID, "NO SE PUDO IMPRIMIR LA CARATULA ANEXA DE LA SOLICITUD No. " + ID + " LA EXCEPCIÓN FUE: " + ex.Message, "FORMATOSPDF.CS");

                }
                finally
                {
                    conGaribay.Close();

                }
            }

            return res;
        }
        #endregion
        public static Byte []  imprimeBuroCredito(int ID)
        {
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                // add content to existing PDF document with PdfStamper
                //string path = Path.GetTempFileName();
                string sql = "SELECT     Solicitudes.creditoID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS name, Productores.domicilio, "
                      + "  Productores.poblacion + ', ' + Productores.municipio + ', ' + Estados.estado + '.' AS ciudadestado, Productores.RFC, Productores.telefono, Productores.fax, "
                      + " Productores.email, Productores.CP, Productores.CURP, Solicitudes.Experiencia, Productores.municipio, Solicitudes.Monto, Solicitudes.Superficieasembrar, "
                      + " Solicitudes.RecursosPropios, Solicitudes.Descripciondegarantias, Solicitudes.Valordegarantias, Creditos.cicloID, Solicitudes.Plazo, Solicitudes.solicitudID, "
                      + " Solicitudes.fecha, Productores.poblacion, Productores.CP AS Expr1, Ciclos.Montoporhectarea, Users.Nombre AS usuario, Solicitudes.aval1, Solicitudes.Aval1Dom, "
                      + " Solicitudes.aval2, Solicitudes.Aval2Dom, Solicitudes.UbicacionGarantia, Productores.municipio, Productores.telefono, Regimenes.Regimen, "
                      + " EstadosCiviles.EstadoCivil, ConceptoSoporteGarantia, montoSoporteGarantia, Solicitudes.otrosPasivosMonto, Solicitudes.otrosPasivosAQuienLeDebe, Productores.Colonia, Estados.estado	"
                      + " FROM         Solicitudes INNER JOIN "
                      + " Creditos ON Creditos.creditoID = Solicitudes.creditoID INNER JOIN "
                      + " Productores ON Productores.productorID = Solicitudes.productorID INNER JOIN "
                      + " Estados ON Productores.estadoID = Estados.estadoID INNER JOIN "
                      + " Ciclos ON Ciclos.cicloID = Creditos.cicloID INNER JOIN "
                      + " Users ON Users.userID = Solicitudes.userID LEFT OUTER JOIN  "
                      + " Regimenes ON Productores.regimenID = Regimenes.regimenID LEFT OUTER JOIN "
                      + " EstadosCiviles ON Productores.estadocivilID = EstadosCiviles.estadoCivilID  "
                      + " where solicitudID = @solID";
                if (new BasePage().IsSistemBanco)
                {
                    sql = dbFunctions.UpdateSDSForSisBanco(sql);
                }
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdsel = new SqlCommand(sql, conGaribay);
                try
                {
                    conGaribay.Open();
                    cmdsel.Parameters.Add("@solID", SqlDbType.Int).Value = ID;

                    SqlDataReader rd = cmdsel.ExecuteReader();
                    if (!rd.HasRows) { conGaribay.Close(); }
                    PdfStamper ps = null;
                    // read existing PDF document
                    PdfReader r = new PdfReader(new RandomAccessFileOrArray(HttpContext.Current.Request.MapPath("/formatos/FormatoBuroGaribay.pdf")), null);
                    ps = new PdfStamper(r, ms);
                    // retrieve properties of PDF form w/AcroFields object
                    AcroFields af = ps.AcroFields;
                    // fill in PDF fields by parameter:
                    // 1. field name
                    // 2. text to insert
                    //String aux = "";
                    try
                    {
                        if (rd.Read())
                        {
                            //string auxiliar;
                            af.SetField("txtFecha", Utils.getFechaconLetraSinDia(DateTime.Parse(rd["fecha"].ToString())));
                            af.SetField("txtNombreProductor", rd["name"].ToString());
                            af.SetField("txtRfc", rd["RFC"].ToString());
                            af.SetField("txtDomicilio", rd["Domicilio"].ToString().ToUpper());
                            af.SetField("txtColonia", rd["Colonia"].ToString().ToUpper());
                            af.SetField("txtMunicipio", rd["municipio"].ToString().ToUpper());
                            af.SetField("txtEstado", rd["estado"].ToString().ToUpper());
                            af.SetField("txtCp", rd["CP"].ToString().ToUpper());
                            af.SetField("txtTelefono", rd["telefono"].ToString().ToUpper());
                            af.SetField("txtLugaryFecha", "AMECA JALISCO. A " + Utils.getFechaconLetraSinDia(DateTime.Parse(rd["fecha"].ToString())));
                            af.SetField("txtNombreyFirma", rd["name"].ToString().ToUpper());
                            


                        }
                        // make resultant PDF read-only for end-user
                        ps.FormFlattening = true;
                        // forget to close() PdfStamper, you end up with
                        // a corrupted file!
                        ps.Close();
                        res = ms.GetBuffer();
                        //Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, "SE IMPRIMIÓ LA SOLICITUD NÚMERO : " + ID);
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.PRINT, "ERROR AL IMPRIMIR LA SOLICITUD NÚMERO : " + ID, ref ex);
                    }
                    finally { if (ps != null) { ps.Close(); } }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.PRINT, "ERROR AL IMPRIMIR LA SOLICITUD NÚMERO : " + ID, ref ex);
                }
                finally
                {
                    conGaribay.Close();
                }
            }
            return res;

        }
        public static Byte[] imprimeCartaCompromiso(int ID)
        {
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                // add content to existing PDF document with PdfStamper
                //string path = Path.GetTempFileName();
                string sql = "SELECT     Solicitudes.creditoID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS name, Productores.domicilio, "
                      + "  Productores.poblacion + ', ' + Productores.municipio + ', ' + Estados.estado + '.' AS ciudadestado, Productores.RFC, Productores.telefono, Productores.fax, "
                      + " Productores.email, Productores.CP, Productores.CURP, Solicitudes.Experiencia, Productores.municipio, Solicitudes.Monto, Solicitudes.Superficieasembrar, "
                      + " Solicitudes.RecursosPropios, Solicitudes.Descripciondegarantias, Solicitudes.Valordegarantias, Creditos.cicloID, Solicitudes.Plazo, Solicitudes.solicitudID, "
                      + " Solicitudes.fecha, Productores.poblacion, Productores.CP AS Expr1, Ciclos.Montoporhectarea, Users.Nombre AS usuario, Solicitudes.aval1, Solicitudes.Aval1Dom, "
                      + " Solicitudes.aval2, Solicitudes.Aval2Dom, Solicitudes.UbicacionGarantia, Productores.municipio, Productores.telefono, Regimenes.Regimen, "
                      + " EstadosCiviles.EstadoCivil, ConceptoSoporteGarantia, montoSoporteGarantia, Solicitudes.otrosPasivosMonto, Solicitudes.otrosPasivosAQuienLeDebe, Productores.Colonia, Estados.estado, Solicitudes.ejido	"
                      + " FROM         Solicitudes INNER JOIN "
                      + " Creditos ON Creditos.creditoID = Solicitudes.creditoID INNER JOIN "
                      + " Productores ON Productores.productorID = Solicitudes.productorID INNER JOIN "
                      + " Estados ON Productores.estadoID = Estados.estadoID INNER JOIN "
                      + " Ciclos ON Ciclos.cicloID = Creditos.cicloID INNER JOIN "
                      + " Users ON Users.userID = Solicitudes.userID LEFT OUTER JOIN  "
                      + " Regimenes ON Productores.regimenID = Regimenes.regimenID LEFT OUTER JOIN "
                      + " EstadosCiviles ON Productores.estadocivilID = EstadosCiviles.estadoCivilID  "
                      + " where solicitudID = @solID";
                if (new BasePage().IsSistemBanco)
                {
                    sql = dbFunctions.UpdateSDSForSisBanco(sql);
                }
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdsel = new SqlCommand(sql, conGaribay);
                try
                {
                    conGaribay.Open();
                    cmdsel.Parameters.Add("@solID", SqlDbType.Int).Value = ID;

                    SqlDataReader rd = cmdsel.ExecuteReader();
                    if (!rd.HasRows) { conGaribay.Close(); }
                    PdfStamper ps = null;
                    // read existing PDF document
                    PdfReader r = new PdfReader(new RandomAccessFileOrArray(HttpContext.Current.Request.MapPath("/formatos/FormatoCartaCompromiso.pdf")), null);
                    ps = new PdfStamper(r, ms);
                    // retrieve properties of PDF form w/AcroFields object
                    AcroFields af = ps.AcroFields;
                    // fill in PDF fields by parameter:
                    // 1. field name
                    // 2. text to insert
                    //String aux = "";
                    try
                    {
                        if (rd.Read())
                        {
                            af.SetField("txtLugarYFecha", "AMECA JALISCO. A " + Utils.getFechaconLetraSinDia(DateTime.Parse(rd["fecha"].ToString())));
                            af.SetField("txtFirma", rd["name"].ToString().ToUpper());
                            StringBuilder textoParrafo = new StringBuilder();
                            textoParrafo.Append("    EL QUE SUSCRIBE EL SR. ");
                            textoParrafo.Append(rd["name"].ToString().ToUpper());
                            textoParrafo.Append(", AGRICULTOR  DEL EJIDO: ");
                            textoParrafo.Append(rd["ejido"].ToString().ToUpper());
                            textoParrafo.Append(" MUNICIPIO DE ");
                            textoParrafo.Append(rd["municipio"].ToString().ToUpper());
                            textoParrafo.Append(", ");
                            textoParrafo.Append(rd["estado"].ToString().ToUpper());
                            textoParrafo.Append(", A QUIEN SE LE FINANCIO ");
                            double superficie = 0;
                            double.TryParse(rd["Superficieasembrar"].ToString(), out superficie);
                            textoParrafo.Append(string.Format("{0:F2}",superficie));
                            textoParrafo.Append(" HECTAREAS PARA EL CULTIVO DE MAIZ CICLO P.V. 2012-2013. POR MEDIO DE LA PRESENTE ME COMPROMETO A ENTREGAR MI COSECHA DE MAIZ EN LAS INSTALACIONES DE LA PARAFINANCIERA COMERCIALIZADORA LAS MARGARITAS S.P.R. DE R.L. ");
                            af.SetField("txtParrafo", textoParrafo.ToString());
                           

                        }
                        // make resultant PDF read-only for end-user
                        ps.FormFlattening = true;
                        // forget to close() PdfStamper, you end up with
                        // a corrupted file!
                        ps.Close();
                        res = ms.GetBuffer();
                        //Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, "SE IMPRIMIÓ LA SOLICITUD NÚMERO : " + ID);
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.PRINT, "ERROR AL IMPRIMIR LA SOLICITUD NÚMERO : " + ID, ref ex);
                    }
                    finally { if (ps != null) { ps.Close(); } }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.PRINT, "ERROR AL IMPRIMIR LA SOLICITUD NÚMERO : " + ID, ref ex);
                }
                finally
                {
                    conGaribay.Close();
                }
            }
            return res;

        }

        public static ExcelFileReader GetExcelParametricaWithSolicitudData(int solicitudID)
        {
            ExcelFileReader excel = null;
            try
            {
                string filepath = "/formatos/PARAMETRICA_MAIZ_AMECA_2010.xls";
                excel = new ExcelFileReader(filepath);
                excel.Open();

                string sql = "SELECT     LTRIM(Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre) AS Productor, Productores.domicilio + ' ' + Productores.colonia AS domicilioColonia, Productores.poblacion, Productores.municipio, Estados.estado, Productores.CP, Productores.telefono, Productores.CURP, Productores.RFC, EstadosCiviles.EstadoCivil, Productores.conyugue, Solicitudes.Experiencia, Solicitudes.otrosPasivosMonto, Solicitudes.otrosPasivosAQuienLeDebe, Solicitudes.superficieFinanciada, Solicitudes.Superficieasembrar, Solicitudes.ingNetosAnualOtrosCultivos, Solicitudes.ingNetosAnualGanaderia, Solicitudes.ingNetosComercioServicios, Solicitudes.casaHabitacion, Solicitudes.rastra, Solicitudes.Arado, Solicitudes.Cultivadora, Solicitudes.Subsuelo, Solicitudes.tractor, Solicitudes.sembradora, Solicitudes.camioneta, Solicitudes.otrosActivos, Solicitudes.totalActivos, Solicitudes.garantiaLiquida, Solicitudes.ConceptoSoporteGarantia, Solicitudes.montoSoporteGarantia, Solicitudes.domicilioDelDeposito, Solicitudes.aval1, Solicitudes.Aval1Dom, Solicitudes.solicitudID, Solicitudes.creditoID, Solicitudes.fecha FROM         Solicitudes INNER JOIN Productores ON Solicitudes.productorID = Productores.productorID INNER JOIN Estados ON Productores.estadoID = Estados.estadoID INNER JOIN EstadosCiviles ON Productores.estadocivilID = EstadosCiviles.estadoCivilID"
                      + " where solicitudID = @solID";
                sql = dbFunctions.UpdateSDSForSisBanco(sql);
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdsel = new SqlCommand(sql, conGaribay);
                try
                {
                    conGaribay.Open();
                    cmdsel.Parameters.Add("@solID", SqlDbType.Int).Value = solicitudID;
                    SqlDataReader rd = cmdsel.ExecuteReader();
                    if (rd.HasRows && rd.Read())
                    {
                        excel.ChangeCurrentSheet(0);
                        excel.setCellValue("B10", rd["Productor"].ToString());excel.setCellValue("C10", rd["domicilioColonia"].ToString());excel.setCellValue("D10", rd["poblacion"].ToString());excel.setCellValue("E10", rd["municipio"].ToString());excel.setCellValue("G10", rd["CP"].ToString());excel.setCellValue("H10", rd["telefono"].ToString());excel.setCellValue("I10", rd["CURP"].ToString());excel.setCellValue("J10", rd["RFC"].ToString());excel.setCellValue("L10", rd["EstadoCivil"].ToString().IndexOf("CASADO") >= 0? "CASADO 1": "SOLTERO");excel.setCellValue("N10", rd["conyugue"] != null ? rd["conyugue"].ToString(): "");excel.setCellValue("P10", rd["Experiencia"].ToString());excel.setCellValue("Q10", rd["otrosPasivosMonto"].ToString());excel.setCellValue("R10", rd["otrosPasivosAQuienLeDebe"].ToString());excel.setCellValue("S10", rd["superficieFinanciada"].ToString());excel.setCellValue("T10", rd["Superficieasembrar"].ToString());excel.setCellValue("V10", rd["ingNetosAnualOtrosCultivos"].ToString());excel.setCellValue("W10", rd["ingNetosAnualGanaderia"].ToString());excel.setCellValue("X10", rd["ingNetosComercioServicios"].ToString());excel.setCellValue("Z10", rd["casaHabitacion"].ToString());excel.setCellValue("AA10", rd["rastra"].ToString());excel.setCellValue("AB10", rd["Arado"].ToString());excel.setCellValue("AC10", rd["Cultivadora"].ToString());
                        excel.setCellValue("AD10", rd["Subsuelo"].ToString());
                        excel.setCellValue("AE10", rd["tractor"].ToString());
                        excel.setCellValue("AF10", rd["sembradora"].ToString());
                        excel.setCellValue("AG10", rd["camioneta"].ToString());
                        excel.setCellValue("AH10", rd["otrosActivos"].ToString());
                        excel.setCellValue("AJ10", rd["garantiaLiquida"].ToString());
                        excel.setCellValue("AK10", rd["ConceptoSoporteGarantia"].ToString());
                        excel.setCellValue("AL10", rd["montoSoporteGarantia"].ToString());
                        excel.setCellValue("AM10", rd["domicilioDelDeposito"].ToString());
                        excel.setCellValue("AO10", rd["aval1"].ToString());
                        excel.setCellValue("AP10", rd["Aval1Dom"].ToString());

                        excel.ChangeCurrentSheet(1);
                        excel.setCellValue("D4", 1);
                        excel.setCellValue("D57", (DateTime)rd["fecha"]);
                    }
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "rellenando etsel", ref ex);
                }
                finally
                {
                    conGaribay.Close();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "rellenando etsel", ref ex);
            }
            return excel;
        }


        /***************evaluacion***************************/
        public static Byte[] imprimeEvaluacion(int solicitudID)
        {
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                // add content to existing PDF document with PdfStamper
                try
                {
                    PdfStamper ps = null;
                    // read existing PDF document
                    PdfReader r = new PdfReader(new RandomAccessFileOrArray(HttpContext.Current.Request.MapPath("/formatos/FormatoHojaEvaluacion.pdf")), null);
                    ps = new PdfStamper(r, ms);
                    // retrieve properties of PDF form w/AcroFields object
                    AcroFields af = ps.AcroFields;
                    // fill in PDF fields by parameter:
                    // 1. field name
                    // 2. text to insert
                    //String aux = "";
                    try
                    {
                        ExcelFileReader excel = FormatosPdf.GetExcelParametricaWithSolicitudData(solicitudID);
                        if (excel != null)
                        {
                            excel.ChangeCurrentSheet(9);
                            excel.RecalculateSheet();
                            MemoryStream msBook = new MemoryStream();
                            excel.WriteTo(msBook);
                            excel = new ExcelFileReader(ref msBook);
                            excel.Open();
                            excel.ChangeCurrentSheet(9);
                            foreach (string celda in af.Fields.Keys)
                            {
                                try
                                {
                                    af.SetField(celda, excel.getStringCellValue(celda));
                                }
                                catch (Exception ex)
                                {
                                    Logger.Instance.LogException(Logger.typeUserActions.PRINT, "field: " + celda, ref ex);
                                }
                            }

                        }
                        // make resultant PDF read-only for end-user
                        ps.FormFlattening = true;
                        // forget to close() PdfStamper, you end up with
                        // a corrupted file!
                        ps.Close();
                        res = ms.GetBuffer();
                        //Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, "SE IMPRIMIÓ LA SOLICITUD NÚMERO : " + ID);
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.PRINT, "ERROR AL IMPRIMIR LA SOLICITUD NÚMERO : " + solicitudID, ref ex);
                    }
                    finally { if (ps != null) { ps.Close(); } }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.PRINT, "ERROR AL IMPRIMIR LA SOLICITUD NÚMERO : " + solicitudID, ref ex);
                }
            }
            return res;

        }
        /******************************************************/


    }
}
