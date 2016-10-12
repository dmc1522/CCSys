using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Collections.Generic;



using System.Globalization;
using System.Collections;
using System.IO;



namespace WSGaribay
{
    public class WSUtils
    {
        public static DateTime myNow
        {
            get
            {
                return DateTime.UtcNow.AddHours(-5);
            }
        }

        public static bool LoadXMLinDataSet(String sXML, out DataSet ds)
        {
            bool bResult = false;
            DataSet intDS = new DataSet();
            try
            {
                if (sXML != "")
                {
                    intDS.Tables.Clear();
                    using (StringReader sr = new StringReader(sXML))
                    {
                        intDS.ReadXml(sr);
                        bResult = true;
                    }
                }
            }
            catch 
            {
                
            }
            ds = intDS;
            return bResult;
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

        public static string converttoLongDBFormat(string fecha)
        {
            try
            {
                DateTime dt;
                dt = DateTime.Parse(fecha, new CultureInfo("es-Mx"));
                fecha = dt.ToString("yyyy/MM/dd HH:mm:ss");
                return fecha;
            }
            catch (Exception e)
            {
                return "1900/01/01 00:00:00";
            }
        }

        public static string encriptacadena(string cadenaaencriptar)
        {
            WSCCryptorEngine cryp = new WSCCryptorEngine();
            byte[] bytesencriptados = cryp.Encriptar(cadenaaencriptar);
            string ax;
            ax = HttpUtility.UrlEncode(bytesencriptados);
            return ax;
        }


        public static String desEncriptaCadena(String cadenaadividir)
        {
            String newcadena = "";
            try
            {
                //byte[] bytesadesencriptar = HttpServerUtility.UrlTokenDecode(cadenaadividir);
                byte[] bytesadesencriptar = HttpUtility.UrlDecodeToBytes(cadenaadividir);
                WSCCryptorEngine cryp = new WSCCryptorEngine();
                newcadena = cryp.Desencriptar(bytesadesencriptar);

                //if (cadenaadividir != null)
                //{
                //    string[] elementosdehash = cadenaadividir.Split('&');

                //    foreach (string elementodehash in elementosdehash)
                //    {
                //        if (elementodehash != null && elementodehash.Length > 0)
                //        {
                //            string[] valores = elementodehash.Split('=');
                //            if (valores.Length == 2 && valores[0].ToString().Length > 0 && valores[1].ToString().Length > 0)
                //            {
                //                String par1 = valores[0].ToString();
                //                String par2 = valores[1].ToString();

                //                myQueryStrings.Add(par1, par2);
                //            }
                //            else
                //            {
                //                return false;
                //            }


                //        }

                //    }
                //}
                //else
                //{
                //    myQueryStrings.Clear();
                //    return false;
                //}
            }
            catch (Exception exception)
            {

                return null;
            }
            return newcadena;
        }
    }
}
