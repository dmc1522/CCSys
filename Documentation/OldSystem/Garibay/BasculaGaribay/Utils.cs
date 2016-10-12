using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Web;



namespace BasculaGaribay
{
    class Utils
    {
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
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ds = intDS;
            return bResult;
        }

        public static bool desEncriptaCadena(String cadenaadividir, out String newcadena)
        {
            newcadena = "";
            try
            {
                //byte[] bytesadesencriptar = HttpServerUtility.UrlTokenDecode(cadenaadividir);
                byte[] bytesadesencriptar = HttpUtility.UrlDecodeToBytes(cadenaadividir);
                CCryptorEngine cryp = new CCryptorEngine();
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
                
                return false;
            }
            return true;
        }

        public static string encriptacadena(string cadenaaencriptar)
        {
            CCryptorEngine cryp = new CCryptorEngine();
            byte[] bytesencriptados = cryp.Encriptar(cadenaaencriptar);
            string ax;
            ax = HttpUtility.UrlEncode(bytesencriptados);
            return ax;
        }


        //
        public static double getDesctoHumedad(double fHumedad, double fKg)
        {
            return getDesctoHumedad(fHumedad, fKg, false);
            //return (double)(fHumedad > 14.0 ? ((fHumedad - 14.0) * 0.0116 * fKg) : (double)0.0);
        }
        public static double getDesctoHumedad(double fHumedad, double fKg, bool esdesalida)
        {
            double dcto = 0.00;
            if (!esdesalida)
            {
                dcto = (double)(fHumedad > 14.0 ? ((fHumedad - 14.0) * 0.0116 * fKg) : 0.00);
            }
            else
            {
                dcto = (fHumedad - 14.0) * 0.0116 * fKg;
            }
            return dcto;
        }

        public static double getDesctoImpurezas(double fImpurezas, double fKg)
        {
            return getDesctoImpurezas(fImpurezas, fKg, false);
            //return (double)(fImpurezas > 14.0 ? ((fImpurezas - 14.0) * 0.0116 * fKg) : (float)0.0);
        }
        public static double getDesctoImpurezas(double fImpurezas, double fKg, bool esdesalida)
        {
            double dcto = 0.00;
            if (!esdesalida)
            {
                dcto = (double)(fImpurezas > 2 ? ((fImpurezas - 2) * 0.01f * fKg) : 0f);
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
                return (fHumedad >= 16 ? ((fHumedad - 16.0f) * 10.0f + 50.0f) * fKg / 1000.0f : 0.0f);
            }
        }
        //






        //public static float getDesctoHumedad(float fHumedad, float fKg)
        //{
        //    return (float)(fHumedad > 14.0 ? ((fHumedad - 14.0) * 0.0116 * fKg) : (float)0.0);
        //}

        //public static float getDesctoImpurezas(float fImpurezas, float fKg)
        //{
        //    return (float)(fImpurezas > 2 ? ((fImpurezas - 2) * 0.01f * fKg) : 0f);
        //    //return (float)(fImpurezas > 14.0 ? ((fImpurezas - 14.0) * 0.0116 * fKg) : (float)0.0);
        //}

        //public static float getDesctoSecado(float fHumedad, float fKg)
        //{
        //    return (fHumedad >= 16 ? ((fHumedad - 16.0f) * 10.0f + 50.0f) * fKg / 1000.0f : 0.0f);
        //}

        //public static String getIp()
        //{


        //    //return HttpContext.Current.Request.UserHostAddress;
        ////System.Net.IPAddress[] sd = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());
        //System.Net.IPAddress[] sd = System.Net.Dns.GetHostAddresses("localhost");
        //return sd[1].ToString();
        
        //}

    }
}
