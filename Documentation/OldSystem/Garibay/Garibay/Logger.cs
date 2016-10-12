using System.Collections.Generic;

using System.IO;

using System.Web;
using System.Data.SqlClient;
using System.Globalization;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.Odbc;
using System.Text;


namespace Garibay
{
    public sealed class Logger : Page
    {
        private static readonly Logger instance = new Logger();

        private Logger(){

        }
        int maxFileSize = 1048576; //BYTES
        
        public static Logger Instance{
            get
            {
                return instance;
            }
        }
        public enum typeLogMessage
        {
           CRITICAL=0,
           INFO,
           DEBUG


        }
     
        public enum typeModulo
        {
            LOGIN=0,
            USUARIOS, 
            PRODUCTORES, 
            CICLOS, 
            PRODUCTOS, 
            CHEQUES, 
            CONCEPTOSCAJACHICA, 
            CUENTASDEBANCOS, 
            MOVIMIENTOSDEBANCO, 
            PROVEEDORES, 
            ENTRADAPRODUCTOS, 
            MOVIMIENTOSDECAJACHICA,
            MICUENTA,
            CREDITOS,
            SALIDADEPRODUCTOS,
            SOLICITUDES,
            PREDIOS,
            DOCUMENTOS,
            NOTACOMPRA,
            LIQUIDACIONES,
            CATALOGO,
            BOLETAS,
            CLIENTESVENTAS,
            GANPROVEEDORES,
            FACTURADEVENTA,
            CERTIFICADOS,
            NOTAVENTA,
            SEGUROS,
            REPORTES,
            CONVERSIONPRODUCTO,
            TARJETASDIESEL,
            FACTURASDIESEL,
            FACTURASGANADO

        }

        public enum typeUserActions
        {
            SELECT = 0,
            INSERT, UPDATE, DELETE, LOGIN, PRINT

        }

        public void LogUserSessionRecord(typeModulo Modulo, typeUserActions UserAction, string description)
        {
            LogUserSessionRecord(Modulo, UserAction, new BasePage().UserID, description);
        }
        public void LogUserSessionRecord(typeModulo Modulo, typeUserActions UserAction, int iduser, string  description)
        {
            string qryIns = "INSERT INTO UserSessionRecords(userID,moduleID,useractionID,timestamp,description) VALUES (@userID,@moduleID, @useractionID, @timestamp, @description)";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            try
            {
                cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = iduser;
                cmdIns.Parameters.Add("@moduleID", SqlDbType.Int).Value = (int)Modulo;
                cmdIns.Parameters.Add("@useractionID", SqlDbType.Int).Value = (int)UserAction;
                cmdIns.Parameters.Add("@timestamp", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                cmdIns.Parameters.Add("@description", SqlDbType.Text).Value = description;
                conGaribay.Open();
                int numregistros = cmdIns.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception(myConfig.StrFromMessages("LOGGERACTIONFAILED"));
                }
               
            }
            catch (Exception err3)
            {
                this.LogMessage(Logger.typeLogMessage.CRITICAL, UserAction, iduser, err3.Message, "Error enviado desde LogUserSessionRecords");
                
            }
            finally
            {
                conGaribay.Close();
            }
           
        }

        public void LogException(typeUserActions UserAction, string description, Exception ex)
        {
            Exception e = ex;
            LogException(UserAction, description, HttpContext.Current.Request.Url.ToString(), ref e);
        }

        public void LogException(typeUserActions UserAction, string description, ref Exception ex)
        {
            LogException(UserAction, description, HttpContext.Current.Request.Url.ToString(), ref ex);
        }
        public void LogException(typeUserActions UserAction, string description, string urlpage, ref Exception ex)
        {
            Logger.Instance.LogException(typeLogMessage.CRITICAL, UserAction, description, urlpage, ref ex);
        }

        public void LogException(typeLogMessage MessageID, typeUserActions UserAction, string description, string urlpage, ref Exception ex)
        {
            if ( ex.GetType() == typeof(SqlException))
            {
                StringBuilder sb = new StringBuilder();
                try
                {
                    SqlException sqlEx = (SqlException)ex;
                    for (int i = 0; i < sqlEx.Errors.Count; i++)
                    {
                        sb.Append("Index #");
                        sb.AppendLine(i.ToString());
                        sb.Append("Message: ");
                        sb.AppendLine(sqlEx.Errors[i].Message);
                        sb.Append("LineNumber: ");
                        sb.AppendLine(sqlEx.Errors[i].LineNumber.ToString());
                        sb.Append("Source: ");
                        sb.AppendLine(sqlEx.Errors[i].Source);
                        sb.Append("Procedure: ");
                        sb.AppendLine(sqlEx.Errors[i].Procedure);
                    }
                }
                finally
                {
                    description = description + sb.ToString();
                }
            }
            description =  description + " logging exception: EX:" + ex.Message + " stack: " + ex.StackTrace;
            if (ex.InnerException != null)
            {
                description += " Innerexception: " + ex.InnerException.Message + " stack: " + ex.InnerException.StackTrace;
            }
            try
            {
                description += " USUARIO: " + new BasePage().UserID + " NOMBRE: " + new BasePage().CurrentUserName;
                description += "<BR />Page:" + HttpContext.Current.Request.Url.ToString();
            }
            catch{}
            try
            {
                String sTo = "cheliskis@gmail.com, melvinquintero@hotmail.com";
                StringBuilder Subject = new StringBuilder();
                Subject.Append("Exception: ");
                Subject.Append(ex.Message);
                StringBuilder Body = new StringBuilder();
                Body.Append("Time: ");
                Body.Append(Utils.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                Body.Append("<br />");
                Body.Append("Message: ");
                Body.Append(ex.Message);
                Body.Append("<br />");
                Body.Append("Stack: ");
                Body.Append(ex.StackTrace);
                Body.Append("<br />");
                Body.Append("FullDesc: ");
                Body.Append(description);
                Body.Append("<br />");
                Body.Append(" :-( do not let this happen again");
                try
                {
                    EMailUtils.SendTextEmail(sTo, Subject.ToString(), Body.ToString(), true);
                }
                catch {}
            }
            catch {}
            this.LogMessage(MessageID, UserAction, new BasePage().UserID, description  , urlpage);
        }
        public void LogMessage(typeLogMessage MessageID, typeUserActions UserAction, int iduser, string description, string urlpage)
        {
            string qryIns = "INSERT INTO LogMessages VALUES (@logmsgTypeID,@userID, @useractionID, @urlpage, @datestamp,  @message)";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            try
            {
                cmdIns.Parameters.Add("@logmsgTypeID", SqlDbType.Int).Value = (int)MessageID;
                cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = iduser;
                cmdIns.Parameters.Add("@useractionID", SqlDbType.Int).Value = (int)UserAction;
                cmdIns.Parameters.Add("@urlpage", SqlDbType.NVarChar).Value = urlpage;
                cmdIns.Parameters.Add("@datestamp", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
//                 if (MessageID == typeLogMessage.CRITICAL)
//                 {
//                     description += +" stackTrace: " + Environment.StackTrace;
//                 }
                cmdIns.Parameters.Add("@message", SqlDbType.Text).Value = description;
                conGaribay.Open();
                int numregistros = cmdIns.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception(myConfig.StrFromMessages("LOGGERACTIONFAILED"));
                }
                string cadenaaloggear = "SE GENERÓ UN MENSAJE DEL TIPO: ";
                cadenaaloggear += MessageID.ToString();
                cadenaaloggear += ", CUANDO EL USUARIO CON EL ID: ";
                cadenaaloggear += iduser.ToString();
                cadenaaloggear += " "; cadenaaloggear += UserAction.ToString();
                cadenaaloggear += ". EN LA PÁGINA: ";
                cadenaaloggear += urlpage;
                cadenaaloggear += ". EL MENSAJE FUE: "; cadenaaloggear += description;

            }
           
            catch (Exception exc)
            {

                string cadenaaloggear = "SE DIO LA SIGUIENTE EXCEPCION AL TRATAR DE LOGGEAR UN MENSAJE: ";
                cadenaaloggear += exc.Message;
                cadenaaloggear += " EL MENSAJE ERA DEL TIPO: ";
                cadenaaloggear += MessageID.ToString();
                cadenaaloggear += ", CUANDO EL USUARIO CON EL ID: ";
                cadenaaloggear += iduser.ToString();
                cadenaaloggear += " "; cadenaaloggear += UserAction.ToString();
                cadenaaloggear += ". EN LA PÁGINA: ";
                cadenaaloggear += urlpage;
                cadenaaloggear += ". EL MENSAJE FUE: "; cadenaaloggear += description;

            }
            finally
            {
                conGaribay.Close();
            }

        }
        
      
    }

}
