using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmTransportistas : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblAddTransportistaResult.Text = string.Empty;
        }

        protected void dvTransportista_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            this.lblAddTransportistaResult.Text = string.Empty;
            if (e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error inserting", e.Exception);
                this.lblAddTransportistaResult.Text += "Error insertando: " + e.Exception.ToString() + "<BR />";
                e.ExceptionHandled = true;
            }
            if (e.AffectedRows == 1)
            {
                this.lblAddTransportistaResult.Text += "SE HA AGREGADO EL TRANSPORTISTA CORRECTAMENTE";
            }
            else
            {
                this.lblAddTransportistaResult.Text += "NO SE HA AGREGADO EL TRANSPORTISTA CORRECTAMENTE SE DEVOLVIO: " + e.AffectedRows.ToString();
            }
            
        }

        protected void gvTransportistas_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            this.lblAddTransportistaResult.Text = string.Empty;
            if (e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "error deleting Transportista", e.Exception);
                this.lblAddTransportistaResult.Text += "Error borrando: " + e.Exception.ToString() + "<BR />";
                e.ExceptionHandled = true;
            }
            if (e.AffectedRows == 1)
            {
                this.lblAddTransportistaResult.Text += "SE HA ELIMINADO EL TRANSPORTISTA CORRECTAMENTE";
            }
            else
            {
                this.lblAddTransportistaResult.Text += "NO SE HA ELIMINADO EL TRANSPORTISTA CORRECTAMENTE SE DEVOLVIO: " + e.AffectedRows.ToString();
            }
        }
    }
}
