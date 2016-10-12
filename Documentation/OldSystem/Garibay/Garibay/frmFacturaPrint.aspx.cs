using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Garibay
{
    public partial class frmFacturaPrint : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.LoadEncryptedQueryString() > 0 && this.myQueryStrings["FacturaID"] != null)
                {
                    try
                    {
                        int id = -1;
                        int.TryParse(this.myQueryStrings["FacturaID"].ToString(), out id);

                        Byte[] bytes = PdfCreator.printFacturaVenta(id);
                        Response.ClearHeaders();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Content-Disposition", "attachment;filename=Factura_" + id.ToString() + ".pdf");
                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        Response.End();
                        /*
                        try
                                                {
                                                    File.Delete(pathArchivotemp);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Logger.Instance.LogException(Logger.typeUserActions.DELETE, "ERROR BORRANDO ARCHIVO TEMP DE PAQUETE", ref ex);
                                                }*/
                        
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.SELECT, "couldn't print factura: " + this.myQueryStrings["FacturaID"].ToString(), ref ex);
                        Response.Write("La factura no pudo ser impresa el error fue: " + ex.Message);
                        Response.Write("<br />");
                        Response.Write("Pila: " + ex.StackTrace);
                    }
                }
            }
            else
            {
                Response.Clear();
                Response.Write("<script type=\"text/javascript\">window.close();</script>");
                Response.End();
            }
        }
    }
}
