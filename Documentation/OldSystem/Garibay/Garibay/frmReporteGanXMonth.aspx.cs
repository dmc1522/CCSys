using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Garibay
{
    public partial class frmReporteGanXMonth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void aplicaFiltro()
        {
            string filter = string.Empty;
            if (this.ddlMes.SelectedValue != "0")
            {
                filter = "Month = " + this.ddlMes.SelectedValue.ToString();
            }
            if (this.ddlAnio.SelectedValue != "0")
            {
                filter += " and Year = " + this.ddlAnio.SelectedValue.ToString();
            }
            this.sdsReporteXMonth.FilterExpression = filter;
            this.gvReporte.DataBind();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            this.aplicaFiltro();
        }

        protected void gvReporte_DataBound(object sender, EventArgs e)
        {
            try
            {
                DataView dv = (DataView)this.sdsReporteXMonth.Select(DataSourceSelectArguments.Empty);
                DataTable dt = dv.ToTable();
                Label lbl = null;
                lbl = (Label)this.gvReporte.FooterRow.FindControl("lblTotal");
                if (lbl != null)
                {
                    lbl.Text = double.Parse(dt.Compute("SUM(total)", "").ToString()).ToString("C2");
                }

                lbl = (Label)this.gvReporte.FooterRow.FindControl("lblBecerros");
                if (lbl != null)
                {
                    lbl.Text = int.Parse(dt.Compute("SUM(Becerros)", "").ToString()).ToString();
                }

                lbl = (Label)this.gvReporte.FooterRow.FindControl("lblGanado");
                if (lbl != null)
                {
                    lbl.Text = int.Parse(dt.Compute("SUM(Ganado)", "").ToString()).ToString();
                }

                lbl = (Label)this.gvReporte.FooterRow.FindControl("lblVacas");
                if (lbl != null)
                {
                    lbl.Text = int.Parse(dt.Compute("SUM(Vacas)", "").ToString()).ToString();
                }


                lbl = (Label)this.gvReporte.FooterRow.FindControl("lblVaquillas");
                if (lbl != null)
                {
                    lbl.Text = int.Parse(dt.Compute("SUM(Vaquillas)", "").ToString()).ToString();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "calculating totales footer in gvReporte", ex);
            }
        }
    }
}
