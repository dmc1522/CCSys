using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmOrdendeCargaList :Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvOrdenesDeCarga_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = (LinkButton)e.Row.Cells[0].FindControl("LBAbrirOrdenDeCarga");
                if (lb != null)
                {

                    String sQuery = "ordenID=" + this.gvOrdenesDeCarga.DataKeys[e.Row.RowIndex]["ordenDeCargaID"].ToString();
                    sQuery = Utils.GetEncriptedQueryString(sQuery);
                    String strRedirect = "~/frmOrdendeCargaAdd.aspx" + sQuery;
                    lb.PostBackUrl = strRedirect;


                }
                lb = (LinkButton)e.Row.Cells[0].FindControl("LBPrintOrdenCarga");
                if (lb != null)
                {
                    string sFileName = "ORDENDECARGA.pdf";
                    sFileName = sFileName.Replace(" ", "_");
                    String sURL = "frmDescargaTmpFile.aspx";
                    string datosaencriptar;
                    datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
                    datosaencriptar = datosaencriptar + "ordenDeCargaId=" + this.gvOrdenesDeCarga.DataKeys[e.Row.RowIndex]["ordenDeCargaID"].ToString() + "&";
                    String URLcomplete = sURL + "?data=";
                    URLcomplete += Utils.encriptacadena(datosaencriptar);
                    JSUtils.OpenNewWindowOnClick(ref lb, URLcomplete, "Orden de carga ", true);
                }
            }
        }

        protected void btnAgregarOrdenDeCarga_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmOrdendeCargaAdd.aspx", true);
        }


       
    }
}
