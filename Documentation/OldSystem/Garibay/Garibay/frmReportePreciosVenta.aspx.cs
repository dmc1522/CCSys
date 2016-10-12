using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

namespace Garibay
{
    public partial class frmReportePreciosVenta : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            this.gvReporte.DataBind();
            String pathArchivotemp;
            pathArchivotemp = PdfCreator.printLista("LISTA DE PRECIOS DE VENTA DE " + this.ddlTiposProducto.SelectedItem.Text, gvReporte, PdfCreator.orientacionPapel.VERTICAL, PdfCreator.tamañoPapel.CARTA);

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename= preciosdeventa.pdf");
            Response.WriteFile(pathArchivotemp);
            Response.Flush();
            Response.End();
            try
            {
                File.Delete(pathArchivotemp);
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "ERROR BORRANDO ARCHIVO TEMP DE CHEQUES", ref ex);
            }

                
        }
    }
}
