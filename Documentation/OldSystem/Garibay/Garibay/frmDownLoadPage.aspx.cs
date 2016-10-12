using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;

namespace Garibay
{
    public partial class frmDownLoadPage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.LoadEncryptedQueryString() > 0 && 
                this.myQueryStrings["printing"] != null &&
                this.myQueryStrings["printing"].ToString() == "1")
            {
                Response.Clear();
                byte[] bytes = null;
                switch(this.myQueryStrings["printing"].ToString())
                {
                    case "1":
                        {
                            frmEstadodeCuentaCredito frm = (frmEstadodeCuentaCredito) Context.Handler;
                            bytes = frm.DownloadBytes;
                        }
                        break;
                }
// 
//                 string url = "http://" + Request.ServerVariables["HTTP_URL"];
//                 url = "http://" + Request.Url.Host + ":" + Request.Url.Port;
//                 url += "/" + this.Session["DOWNURL"].ToString();
//                 Logger.Instance.LogMessage(Logger.typeLogMessage.DEBUG, Logger.typeUserActions.PRINT,
//                     this.UserID, "url to print", this.Request.Url.ToString());
//                 WebRequest mywebReq = WebRequest.Create(url);
//                 WebResponse mywebResp = mywebReq.GetResponse();
//                 StreamReader sr = new StreamReader(mywebResp.GetResponseStream());
//                 byte[] bytes = new byte[100000];
//                 MemoryStream ms = new MemoryStream();
//                 int count = 0;
//                 while ((count = sr.BaseStream.Read(bytes,0,100000)) > 0)
//                 {
//                     ms.Write(bytes,0,count);
//                 }
                 Response.Clear();
                 //Response.AddHeader("content-disposition", "attachment; filename=EstadoDeCuenta.pdf");
                 Response.ContentType = "application/pdf";
                 Response.BinaryWrite(bytes);
            }
        }
    }
}
