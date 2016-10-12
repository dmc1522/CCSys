using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.IO;
using System.IO.Compression;
using System.Web.UI;
using System.Data.SqlClient;

namespace Garibay
{
    public class Global : System.Web.HttpApplication
    {
        void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            String[] sPagExcluded = { "Esqueleto", "FormatosPdf", "Print", "QueryExistencias", "frmDescarga" };
           
            if(this.Request.Url.ToString().IndexOf(".aspx")==-1){
                return;
            }
            foreach (String item in sPagExcluded)
            {
                if (this.Request.Url.ToString().IndexOf(item) > -1)
                {
                    return;
                }
            }
         
            HttpApplication app = sender as HttpApplication;
            string acceptEncoding = app.Request.Headers["Accept-Encoding"];
            Stream prevUncompressedStream = app.Response.Filter;

            if (!(app.Context.CurrentHandler is Page ||
                app.Context.CurrentHandler.GetType().Name == "SyncSessionlessHandler")/*
                 ||
                                app.Request["HTTP_X_MICROSOFTAJAX"] != null*/
                )
                return;

            if (acceptEncoding == null || acceptEncoding.Length == 0)
                return;

            acceptEncoding = acceptEncoding.ToLower();
            if (acceptEncoding.Contains("deflate") || acceptEncoding == "*")
            {
                // defalte
                app.Response.Filter = new DeflateStream(prevUncompressedStream,
                    CompressionMode.Compress);
                app.Response.AppendHeader("Content-Encoding", "deflate");
            }
            else
                if (acceptEncoding.Contains("gzip"))
                {
                    // gzip
                    app.Response.Filter = new GZipStream(prevUncompressedStream,
                        CompressionMode.Compress);
                    app.Response.AppendHeader("Content-Encoding", "gzip");
                }
                
            //else 
        }

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                Exception ex = Server.GetLastError().GetBaseException();
                string error = string.Empty;
                if (ex is SqlException)
                {
                    error = ((SqlException)(ex)).Procedure;
                    error += ((SqlException)(ex)).Server;
                }
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Application Error" + error, ref ex);
            }
            catch
            {}
            
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}