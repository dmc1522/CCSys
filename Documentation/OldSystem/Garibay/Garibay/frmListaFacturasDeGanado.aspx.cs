using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Garibay
{
    public partial class frmListaFacturasDeGanado : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURASGANADO, Logger.typeUserActions.SELECT, "VISITO LA PÁGINA DE FACTURAS DE GANADO");
            }
        }

        protected string GetOpenURL(string id)
        {
            string sRedirect = "frmFacturaGanado.aspx";
            sRedirect += Utils.GetEncriptedQueryString("FacturaID=" + id);
            return sRedirect;
        }

        protected void gvFacturas_DataBound(object sender, EventArgs e)
        {
            try
            {
                DataView dv = (DataView)this.sdFacturasGanado.Select(DataSourceSelectArguments.Empty);
                DataTable dt = dv.ToTable();
                Label lbl = null;
                lbl = (Label)this.gvFacturas.FooterRow.FindControl("lblTotal");
                if (lbl != null)
                {
                    lbl.Text = double.Parse(dt.Compute("SUM(total)", "").ToString()).ToString("C2");
                }

                lbl = (Label)this.gvFacturas.FooterRow.FindControl("lblBecerros");
                if (lbl != null)
                {
                    lbl.Text = int.Parse(dt.Compute("SUM(Becerros)", "").ToString()).ToString();
                }

                lbl = (Label)this.gvFacturas.FooterRow.FindControl("lblGanado");
                if (lbl != null)
                {
                    lbl.Text = int.Parse(dt.Compute("SUM(Ganado)", "").ToString()).ToString();
                }

                lbl = (Label)this.gvFacturas.FooterRow.FindControl("lblVacas");
                if (lbl != null)
                {
                    lbl.Text = int.Parse(dt.Compute("SUM(Vacas)", "").ToString()).ToString();
                }


                lbl = (Label)this.gvFacturas.FooterRow.FindControl("lblVaquillas");
                if (lbl != null)
                {
                    lbl.Text = int.Parse(dt.Compute("SUM(Vaquillas)", "").ToString()).ToString();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "calculating totales footer in gvFacturas", ex);
            }
        }

        protected void ddlGanaderos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filter = string.Empty;
            if (this.ddlGanaderos.SelectedValue != "0")
            {
                filter = "ganProveedorID = " + this.ddlGanaderos.SelectedValue.ToString();
            }
            this.sdFacturasGanado.FilterExpression = filter;
            this.gvFacturas.DataBind();
        }
    }
}
