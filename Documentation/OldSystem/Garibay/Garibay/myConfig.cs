using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Resources;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace Garibay
{
    public class myConfig
    {
        public static string ConnectionInfo
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["GaribayConnectionString"].ConnectionString;
            }
        }

        public enum CATEGORIA
        {
            MOVBANCOS  = 1,
            CHEQUES,
            FACTURAVENTA
        }

        public static void SetStringConfig(String sParametro, CATEGORIA CAT, String sParamValue )
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "update ConfigParams set paramValue = @paramValue where CategoryID = @CategoryID AND paramName = @paramName";
                comm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = (int)CAT;
                comm.Parameters.Add("@paramName", SqlDbType.VarChar).Value = sParametro;
                comm.Parameters.Add("@paramValue", SqlDbType.VarChar).Value = sParamValue;
                
                comm.ExecuteNonQuery();

            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "could not save param: " + sParamValue + " value:" + sParamValue, "SetStringConfig", ref ex);
            }
            finally
            {
                conn.Close();
            }
        }
        public static double GetDoubleConfig(String sParametro, CATEGORIA CAT, double fDefault )
        {
            double dValue = fDefault;
            try
            {
                double.TryParse(GetStringConfig(sParametro, CAT, fDefault.ToString()), out dValue);
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "GetDoubleConfig(" + sParametro + "," + CAT.ToString() + "," + fDefault.ToString() + ")", ref ex);
            }
            return dValue;
        }
        public static String GetStringConfig(String sParametro, CATEGORIA CAT, String sDefault )
        {
            String sResult = sDefault;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "select paramValue from ConfigParams where CategoryID = @CategoryID AND paramName = @paramName";
                comm.Parameters.Add("@CategoryID", SqlDbType.Int).Value = (int)CAT;
                comm.Parameters.Add("@paramName", SqlDbType.VarChar).Value = sParametro;

                SqlDataReader r = comm.ExecuteReader();
                if (r.HasRows && r.Read() && r[0] != null && r[0].ToString().Length > 0)
                {
                    sResult = r[0].ToString();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "No se pudo consultar parametro", "GetStringConfig", ref ex);
                sResult = sDefault;
            }
            finally
            {
                conn.Close();
            }
            return sResult;
        }
        public static bool GetBoolConfig(String sParamName, CATEGORIA CAT, bool bDefault)
        {
            bool bRes = false;
            String sTemp = myConfig.GetStringConfig(sParamName, CAT, "");
            if (sTemp.Length >0)
            {
                switch(sTemp)
                {
                    case "TRUE":
                        bRes = true;
                        break;
                    case "FALSE":
                        bRes = false;
                        break;
                    default:
                        bRes = bDefault;
                        break;
                }
            }
            else
            {
                bRes = bDefault;
            }
            return bRes;
        }

        public static string StrFromMessages(string msgString)
        {
            try
            {
                ResourceManager rm = new ResourceManager("Garibay.Messages", Assembly.GetExecutingAssembly());
                return rm.GetString(msgString);
            }
            catch (System.Exception ex)
            {
                return "";
            }
        }
    }
}
