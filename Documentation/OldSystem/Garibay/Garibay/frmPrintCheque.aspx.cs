using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmPrintCheque : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // do not forget add support to obtain the ajustes from cookie.
            int iMovID = -1;
            if (Request.QueryString["data"] != null &&
                this.loadqueryStrings(Request.QueryString["data"].ToString()) &&
                int.TryParse(this.myQueryStrings["iMovID"].ToString(), out iMovID) &&
                iMovID > -1)
            {
                byte [] bytes = PdfCreator.printCheque(iMovID, PdfCreator.orientacionPapel.VERTICAL, PdfCreator.tamañoPapel.CARTA, 0, 0,this.UserID);

                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=Impresion_Cheque.pdf");
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();               
            }
        }
    }
}
