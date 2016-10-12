using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmListaConversionesProducto : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblResultMsg.Text = string.Empty;
        }

        protected void gvConversiones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
        }

        protected void gvConversiones_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            this.lblResultMsg.Text = string.Empty;
            if(e.AffectedRows == 1)
            {
                this.lblResultMsg.Text = "CONVERSION ELIMINADA, ESTO AFECTO EL INVENTARIO.";
            }
            else
            {
                this.lblResultMsg.Text = "CONVERSION NO PUDE SER ELIMINADA, LAS FILAS AFECTADAS DEVOLVIO :" + e.AffectedRows.ToString();
            }
            if (e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "eliminando conversion de producto", e.Exception);
                e.ExceptionHandled = true;
                this.lblResultMsg.Text += "<BR /> Excepcion: " + e.Exception.ToString();
            }
            this.gvConversiones.SelectedIndex = -1;
        }
    }
}
