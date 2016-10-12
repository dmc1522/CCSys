using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmListaLiqTransportistas : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                this.ddlTransportistas.DataBind();
                this.gvLiquidacionesTrans.DataBind();                
            }
            this.labelResult.Visible = false;
        }

        protected string GetLiqExistenteURL(string sLiqID)
        {
            String sQuery = "liqID=" + sLiqID;
            sQuery = Utils.GetEncriptedQueryString(sQuery);
            String strRedirect = "~/frmLiquidacionTransportista.aspx";
            strRedirect += sQuery;
            return strRedirect;
        }

        protected void ddlTransportistas_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvLiquidacionesTrans.DataBind();
        }

        protected void gvLiquidacionesTrans_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

            if (e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "deleting mov banco", e.Exception);
                this.labelResult.Text = "NO SE PUDO ELIMINAR EL PAGO, REVISE LOS DATOS E INTENTELO DE NUEVO <BR />"
                    + e.Exception.ToString();
                e.ExceptionHandled = true;
            }
            if (e.AffectedRows > 0)
            {
                this.labelResult.Text = "EL PAGO FUE ELIMINADO EXITOSAMENTE";
                this.gvLiquidacionesTrans.DataBind();               
            }
            this.labelResult.Visible = true;
        }

     

       
    }
}
