using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmPresentaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvPresentaciones_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE,
                    "updating presentaciones",
                    e.Exception);
                e.ExceptionHandled = true;
            }
        }

        protected void gvPresentaciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        protected void dvPresentacion_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancelar")
            {
            }
        }

        protected void dvPresentacion_ItemCreated(object sender, EventArgs e)
        {

        }

        protected void dvPresentacion_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE,
                    "inserting presentacion",
                    e.Exception);
                e.ExceptionHandled = true;
                e.KeepInInsertMode = true;
            }
            else
            {
                this.gvPresentaciones.DataBind();
            }
        }
    }
}
