using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Globalization;
using System.Collections;
using System.Web.UI.WebControls;
using System.Data;

namespace Garibay
{
    public class Utils
    {   
        public static int HoursFromUTC
        {
            get { return -6; }
        }
        public static int GetSafeInt(TextBox tb)
        {
            int iResult = 0;
            try
            {
                iResult = int.Parse(tb.Text, NumberStyles.Currency);
            }
            catch
            {
                iResult = 0;
            }
            return iResult;
        }

        public static string getFechaconLetra(DateTime fecha, int userID){
            string fechalarga="1 DE ENERO DE 1900";
            try
            {
                fechalarga = fecha.ToString("dddd, dd") + " de " + fecha.ToString("MMMM, yyyy").ToUpper();
                fechalarga = fechalarga.ToUpper();
            }
            catch(Exception ex)
            {
                fechalarga = "1 DE ENERO DE 1900";
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL,Logger.typeUserActions.SELECT,userID,"ERROR CONVIRTIENDO FECHA LARGA. EX: " + ex.Message, "UTILS - CONVERTIR FECHA");
            }
            return fechalarga;
            
        }
        public static string getFechaconLetraSinDia(DateTime fecha)
        {
            string fechalarga = "1 DE ENERO DE 1900";
            try
            {
                fechalarga = fecha.ToString("dd") + " DE " + fecha.ToString("MMMM DE yyyy").ToUpper();
                fechalarga = fechalarga.ToUpper();
            }
            catch (Exception ex)
            {
                fechalarga = "1 DE ENERO DE 1900";
                Logger.Instance.LogException(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, "ERROR CONVIRTIENDO FECHA LARGA. EX: " + ex.Message, "UTILS - CONVERTIR FECHA", ref ex);
            }
            return fechalarga;

        }
        public static double GetSafeFloat(double f)
        {
            double fRes = 0.0f;
            try
            {
                fRes = double.Parse(string.Format("{0:N}", f), NumberStyles.Currency);
            }
            catch (System.Exception ex)
            {
                string stacktrace = Environment.StackTrace;
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Error en getSafeFloat :"+stacktrace, ref ex);
            }
            return fRes;
        }

        public static double GetSafeFloat(object s)
        {
            if (s != null)
                return Utils.GetSafeFloat(s.ToString());
            else
                return 0;
        }
        public static double GetSafeFloat(String s)
        {
            double fRes = 0.0f;
            if(s.Length>0)
            {
                try
                {
                    fRes = double.Parse(s, NumberStyles.Currency);
                }
                catch //(System.Exception ex)
                {
//                     string stacktrace = Environment.StackTrace;
//                     Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Error en getSafeFloat :"+stacktrace, ref ex);
                }
               
            }          
           
            return fRes;
        }

        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static void CellOnRedIfLessThanZero(int iColIndex, ref GridView gv)
        {
            double fValue = 0;
            foreach (GridViewRow row in gv.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow || row.RowType == DataControlRowType.Footer)
                {
                    /*for (int i = 0; i < this.gridMovCuentasBanco.Columns.Count ; i++)*/
                    {
                        try
                        {
                            fValue = double.Parse(row.Cells[iColIndex].Text, NumberStyles.Currency);
                            if (fValue < 0)
                            {
                                row.Cells[iColIndex].ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        catch { }
                    }
                }
            }
        }
        public static void CellOnRedIfGreaterThanZero(int iColIndex, ref GridView gv)
        {
            double fValue = 0;
            foreach (GridViewRow row in gv.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow || row.RowType == DataControlRowType.Footer)
                {
                    /*for (int i = 0; i < this.gridMovCuentasBanco.Columns.Count ; i++)*/
                    {
                        try
                        {
                            fValue = double.Parse(row.Cells[iColIndex].Text, NumberStyles.Currency);
                            if (fValue >= 0)
                            {
                                row.Cells[iColIndex].ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        catch { }
                    }
                }
            }
        }

        public static void MergeSameRowsInGV(ref GridView grToDecorate)
        {
            GridViewRow currRow, nextRow;
            for (int iCol = 0; iCol < grToDecorate.Columns.Count; iCol++ )
            {
                for (int i = 0; i < grToDecorate.Rows.Count; i++)
                {
                    currRow = grToDecorate.Rows[i];
                    String sCelltext = HttpContext.Current.Server.HtmlDecode(currRow.Cells[iCol].Text.ToLower()).ToUpper().Trim();
                    try
                    {
                        double.Parse(sCelltext, NumberStyles.Currency);
                        sCelltext = "";
                    }
                    catch 
                    {
                    	
                    }
                    if (currRow.RowType == DataControlRowType.DataRow && sCelltext != "" && i < grToDecorate.Rows.Count - 1)
                    {
                        int iRowSpan = 1;
                        do
                        {
                            nextRow = grToDecorate.Rows[i + 1];
                            if (nextRow.Cells[iCol].Text == currRow.Cells[iCol].Text)
                            {
                                iRowSpan++; i++;
                                nextRow.Cells[iCol].Visible = false;
                                if (i < grToDecorate.Rows.Count - 1)
                                    nextRow = grToDecorate.Rows[i + 1];
                            }
                        } while (i < grToDecorate.Rows.Count - 1 && nextRow.Cells[iCol].Text == currRow.Cells[iCol].Text);
                        currRow.Cells[iCol].RowSpan = iRowSpan;
                    }
                }
            }
        }
        public static void MergeSameRowsInGVPerColumn(ref GridView grToDecorate, int iCol)
        {
            GridViewRow currRow, nextRow;
           
                for (int i = 0; i < grToDecorate.Rows.Count; i++)
                {
                    currRow = grToDecorate.Rows[i];
                    String sCelltext = HttpContext.Current.Server.HtmlDecode(currRow.Cells[iCol].Text.ToLower()).ToUpper().Trim();
                    try
                    {
                        double.Parse(sCelltext, NumberStyles.Currency);
                        sCelltext = "";
                    }
                    catch
                    {

                    }
                    if (currRow.RowType == DataControlRowType.DataRow && sCelltext != "" && i < grToDecorate.Rows.Count - 1)
                    {
                        int iRowSpan = 1;
                        do
                        {
                            nextRow = grToDecorate.Rows[i + 1];
                            if (nextRow.Cells[iCol].Text == currRow.Cells[iCol].Text)
                            {
                                iRowSpan++; i++;
                                nextRow.Cells[iCol].Visible = false;
                                if (i < grToDecorate.Rows.Count - 1)
                                    nextRow = grToDecorate.Rows[i + 1];
                            }
                        } while (i < grToDecorate.Rows.Count - 1 && nextRow.Cells[iCol].Text == currRow.Cells[iCol].Text);
                        currRow.Cells[iCol].RowSpan = iRowSpan;
                    }
                }
           
        }
        public static void MergeColumnsPerRow(ref GridView grToDecorate, int iRow, int iColStar, int iColEnd)
        {
            int iSpan = iColEnd - iColStar;
            for (int i = iColStar + 1; i < iColEnd; i++)
                grToDecorate.Rows[iRow].Cells[i].Visible = false;

            grToDecorate.Rows[iRow].Cells[iColStar].ColumnSpan = iSpan;
            grToDecorate.Rows[iRow].Cells[iColStar].Style["text-align"] = "center";
            
        }



        public static int countRowsInDataReader(SqlDataReader dr)
        {
            return 0;
        }
        public static string conviertedemonadouble(string moneda){
            
            NumberFormatInfo numInfo = new NumberFormatInfo();
            numInfo.CurrencySymbol = "$";
            try{
               
               return double.Parse(moneda, NumberStyles.Any, numInfo).ToString();
              
            }
            catch(Exception exception){
                return "0.00";
            }
        }
        public static float getPointsFromCMasFloat(double cm)
        {
            return (float)conviertedecmsapoints(cm);
        }
        public static double conviertedecmsapoints(double cm){
            double points = 0f;
            points = cm * 28.46504f;
            return points;
        }
        public static string getMonthName(int month){
            String fechaaux, mes="INVALIDMONTHNUM";
          
            
           DateTime dtaux = new DateTime(1900,month,1) ;
     
           try
           {
                
                mes=dtaux.ToString("MMMM",new CultureInfo("es-Mx")).ToUpper();
            
           }
           catch (System.Exception ex)
           {
               return mes;
           	
           }
            return mes;
           

        }


        public static string getNowFormattedDate()
        {
            string resultDate;
            try
            {
            	resultDate = Utils.converttoLongDBFormat(DateTime.UtcNow.AddHours(Utils.HoursFromUTC).ToString("yyyy/MM/dd HH:mm:ss"));
            }
            catch 
            {
                resultDate = "1900/01/01 00:00:00";
            }
            return resultDate;
        }

        public static DateTime Now
        {
            get { return DateTime.UtcNow.AddHours(Utils.HoursFromUTC); }
        }
        public static string getNowFormattedDateNormal()
        {
            string resultDate;
            try
            {
                resultDate = Utils.Now.ToString("dd/MM/yyyy");
            }
            catch (System.Exception ex)
            {
                resultDate = "01/01/1999 ";
            }
            return resultDate;
        }
        public static string converttoLongDBFormat(string fecha){
            try
            {
                DateTime dt;
                dt = DateTime.Parse(fecha,new CultureInfo("es-Mx"));
                fecha = dt.ToString("yyyy/MM/dd HH:mm:ss");
                return fecha;
            }
            catch(Exception e){
                return "1900/01/01 00:00:00";
            }
        }
        public static string converttoFechaForFilterLimite(string fecha)
        {
            try
            {
                DateTime dt;
                dt = DateTime.Parse(fecha, new CultureInfo("es-Mx"));
                fecha = dt.ToString("yyyy/MM/dd");
                fecha += " 23:59:59";
                return fecha;
            }
            catch (Exception e)
            {
                return "1900/01/01 00:00:00";
            }
        }
        public static string converttoshortFormatfromdbFormat(string fecha)
        {
            try
            {
                fecha = DateTime.Parse(fecha).ToString("dd/MM/yyyy");
                 return fecha;
            }
            catch (Exception e)
            {
                return "01/01/1900";
            }
        }

        public static String GetEncriptedQueryString(string sCadenaaEncriptar)
        {
            string sQuery = "?data=";
            sQuery += Utils.encriptacadena(sCadenaaEncriptar);
            return sQuery;
        }
        public static string encriptacadena(string cadenaaencriptar)
        {
            CCryptorEngine cryp = new CCryptorEngine();
            byte[] bytesencriptados = cryp.Encriptar(cadenaaencriptar);
            string ax;
            ax = HttpServerUtility.UrlTokenEncode(bytesencriptados);
            return ax;
        }

        public static bool loadqueryStrings(String cadenaadividir, ref Hashtable  myQueryStrings)
        {
            try
            {
                byte[] bytesadesencriptar = HttpServerUtility.UrlTokenDecode(cadenaadividir);
                CCryptorEngine cryp = new CCryptorEngine();
                cadenaadividir = cryp.Desencriptar(bytesadesencriptar);

                if (cadenaadividir != null)
                {
                    string[] elementosdehash = cadenaadividir.Split('&');

                    foreach (string elementodehash in elementosdehash)
                    {
                        if (elementodehash != null && elementodehash.Length > 0)
                        {
                            string[] valores = elementodehash.Split('=');
                            if (valores.Length == 2 && valores[0].ToString().Length > 0 && valores[1].ToString().Length > 0)
                            {
                                String par1 = valores[0].ToString();
                                String par2 = valores[1].ToString();

                                myQueryStrings.Add(par1, par2);
                            }
                            else
                            {
                                return false;
                            }


                        }

                    }
                }
                else
                {
                    myQueryStrings.Clear();
                    return false;
                }
            }
            catch (Exception exception)
            {
                return false;
            }
            return true;
        }
        
        public static string getStartDateForFilter(string fecha)
        {
            try
            {
                fecha = DateTime.Parse(fecha).ToString("yyyy/MM/dd 00:00:00");
                return fecha;
            }
            catch 
            {
                return "1900/01/01 00:00:00";
            }
        }

        public static string getEndDateForFilter(string fecha)
        {
            try
            {
                fecha = DateTime.Parse(fecha).ToString("yyyy/MM/dd 23:59:59");
                return fecha;
            }
            catch 
            {
                return "2099/01/01 23:59:59";
            }
        }

        public static double getDesctoHumedad(double fHumedad, double fKg)
        {
            return getDesctoHumedad(fHumedad,fKg,false);
            //return (double)(fHumedad > 14.0 ? ((fHumedad - 14.0) * 0.0116 * fKg) : (double)0.0);
        }
        public static double getDesctoHumedad(double fHumedad, double fKg,bool esdesalida)
        {
            double dcto = 0.00;
            if (!esdesalida)
            {
                dcto = (double)(fHumedad > 14.0 ? ((fHumedad - 14.0) * 0.0116 * fKg):0.00);
            }
            else
            {
                dcto = (fHumedad - 14.0) * 0.0116 * fKg;
            }
            return dcto;
        }

        public static double getDesctoImpurezas(double fImpurezas, double fKg)
        {
            return getDesctoImpurezas(fImpurezas,fKg,false);
            //return (double)(fImpurezas > 14.0 ? ((fImpurezas - 14.0) * 0.0116 * fKg) : (float)0.0);
        }
        public static double getDesctoImpurezas(double fImpurezas, double fKg,  bool esdesalida)
        {
            double dcto = 0.00;
            if(!esdesalida)
            {
                dcto= (double)(fImpurezas > 2 ? ((fImpurezas - 2) * 0.01f * fKg) : 0f);
            }
            else
            {
                dcto = (fImpurezas - 2) * 0.01f * fKg;
            }
            return dcto;
            
            //return (double)(fImpurezas > 14.0 ? ((fImpurezas - 14.0) * 0.0116 * fKg) : (float)0.0);
        }

        public static double getDesctoSecado(double fHumedad, double fKg)
        {

            return getDesctoSecado(fHumedad, fKg, false);
        }


        public static double getDesctoSecado(double fHumedad, double fKg, bool esdesalida)
        {
            if (esdesalida)
            {
                return 0.00;
            }
            else
            {
                return (fHumedad >= 16 ? ((fHumedad - 16.0f) * 10.0f + 120.0f) * fKg / 1000.0f : 0.0f);
            }
        }


       public static string NumeroALetras(string num)

       {

           string res, dec = "";
           Int64 entero;
           int decimales;
           double nro;
           try
           {
               nro = Convert.ToDouble(num);
           }
           catch
           {
               return "";
           }
           entero = Convert.ToInt64(Math.Truncate(nro));
           decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
//            if (decimales > 0)
//            {
               if(decimales>= 0 && decimales<=9){
                   dec = "0";
               }
               dec += decimales.ToString() + "/100 M.N.";
            //   dec = dec.Replace(" 0/100", " 00/100");
//            }
           res = Utils.NumeroALetras(Convert.ToDouble(entero)) + " PESOS " + dec;
           return res;

       }

 

       private static string NumeroALetras(double value)

       {
           string Num2Text = "";
           value = Math.Truncate(value);
           if (value == 0) Num2Text = "CERO";
           else if (value == 1) Num2Text = "UNO";
           else if (value == 2) Num2Text = "DOS";
           else if (value == 3) Num2Text = "TRES";
           else if (value == 4) Num2Text = "CUATRO";
           else if (value == 5) Num2Text = "CINCO";
           else if (value == 6) Num2Text = "SEIS";
           else if (value == 7) Num2Text = "SIETE";
           else if (value == 8) Num2Text = "OCHO";
           else if (value == 9) Num2Text = "NUEVE";
           else if (value == 10) Num2Text = "DIEZ";
           else if (value == 11) Num2Text = "ONCE";
           else if (value == 12) Num2Text = "DOCE";
           else if (value == 13) Num2Text = "TRECE";
           else if (value == 14) Num2Text = "CATORCE";
           else if (value == 15) Num2Text = "QUINCE";
           else if (value < 20) Num2Text = "DIECI" + NumeroALetras(value - 10);
           else if (value == 20) Num2Text = "VEINTE";
           else if (value < 30) Num2Text = "VEINTI" + NumeroALetras(value - 20);
           else if (value == 30) Num2Text = "TREINTA";
           else if (value == 40) Num2Text = "CUARENTA";
           else if (value == 50) Num2Text = "CINCUENTA";
           else if (value == 60) Num2Text = "SESENTA";
           else if (value == 70) Num2Text = "SETENTA";
           else if (value == 80) Num2Text = "OCHENTA";
           else if (value == 90) Num2Text = "NOVENTA";
           else if (value < 100) Num2Text = NumeroALetras(Math.Truncate(value / 10) * 10) + " Y " + NumeroALetras(value % 10);
           else if (value == 100) Num2Text = "CIEN";
           else if (value < 200) Num2Text = "CIENTO " + NumeroALetras(value - 100);
           else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = NumeroALetras(Math.Truncate(value / 100)) + "CIENTOS";
           else if (value == 500) Num2Text = "QUINIENTOS";
           else if (value == 700) Num2Text = "SETECIENTOS";
           else if (value == 900) Num2Text = "NOVECIENTOS";
           else if (value < 1000) Num2Text = NumeroALetras(Math.Truncate(value / 100) * 100) + " " + NumeroALetras(value % 100);
           else if (value == 1000) Num2Text = "MIL";
           else if (value < 2000) Num2Text = "MIL " + NumeroALetras(value % 1000);
           else if (value < 1000000)
           {
              Num2Text = NumeroALetras(Math.Truncate(value / 1000)) + " MIL";
              if ((value % 1000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value % 1000);

           }
           else if (value == 1000000) Num2Text = "UN MILLON";
           else if (value < 2000000) Num2Text = "UN MILLON " + NumeroALetras(value % 1000000);
           else if (value < 1000000000000)
           {
               Num2Text = NumeroALetras(Math.Truncate(value / 1000000)) + " MILLONES ";
               if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000) * 1000000);
           }

           else if (value == 1000000000000) Num2Text = "UN BILLON";
           else if (value < 2000000000000) Num2Text = "UN BILLON " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
           else
          {
               Num2Text = NumeroALetras(Math.Truncate(value / 1000000000000)) + " BILLONES";
               if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
          }
          return Num2Text;

       }

       public static void GetGVTotalFooterFormatted(GridView gv, DataTable dt, string formatString, string dtExpresion, string footerLabel)
       {
           if (gv == null || dt.Rows.Count == 0)
           {
               return;
           }
           try
           {
               Label lbl = null;
               lbl = (Label)gv.FooterRow.FindControl(footerLabel);
               if (lbl != null)
               {
                   lbl.Text = string.Format(formatString, double.Parse(dt.Compute(dtExpresion, "").ToString()));
               }
           }
           catch (System.Exception ex)
           {
               Logger.Instance.LogException(Logger.typeUserActions.SELECT, "GetGVTotalFooterFormatted", ex);
           }
       }

   }

}
    