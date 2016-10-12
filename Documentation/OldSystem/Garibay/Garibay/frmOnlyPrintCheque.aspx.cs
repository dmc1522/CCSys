using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Garibay
{
    public partial class frmOnlyPrintCheque : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.compruebasecurityLevel();
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
            }

        }
        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                String sDescription;
                sDescription = "Imprimio el cheque con los datos:";
                sDescription += " Nombre: " + this.txtNombre.Text;
                sDescription += " Fecha: " + this.txtFecha.Text;
                sDescription += " Cantidad: " + this.txtCantidad.Text;
                sDescription += " concepto: " + this.txtConcepto.Text;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDEBANCO, Logger.typeUserActions.PRINT, this.UserID, sDescription);
                byte [] bytes = PdfCreator.printCheque(PdfCreator.orientacionPapel.VERTICAL, PdfCreator.tamañoPapel.CARTA, 0, 0, this.txtNombre.Text, this.txtFecha.Text, this.txtCantidad.Text, this.txtConcepto.Text, this.ddlCuenta.SelectedValue);
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=Impresion_Cheque.pdf");
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.PRINT, "error imprimiendo cheque desde pagiona de formato cheque", this.Request.Url.ToString(), ref ex);
            }
        }
    }
}
