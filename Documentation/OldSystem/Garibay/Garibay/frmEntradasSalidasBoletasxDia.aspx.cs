using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Garibay
{
    public partial class frmEntradasSalidasBoletasxDia : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                if(this.LoadEncryptedQueryString() > 0)
                {
                    if(this.myQueryStrings["productoid"] != null)
                    {
                        try
                        {
                            this.ddlProductos.DataBind();
                            this.ddlProductos.SelectedValue = this.myQueryStrings["productoid"].ToString();
                        }
                        catch (System.Exception ex)
                        {
                            this.ddlProductos.DataBind();
                            Logger.Instance.LogException(Logger.typeUserActions.SELECT, "obteniendo lista de productos", ex);
                        }
                        this.ddlCiclo.DataBind();
                        this.gvReporte.DataBind();
                    }
                }
            }
        }

        protected void ddlProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvReporte.DataBind();
        }

        protected void ddlCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvReporte.DataBind();
        }

        protected void gvReporte_DataBound(object sender, EventArgs e)
        {
            try
            {
                DataView dv = (DataView)this.sdsReporteBoletas.Select(DataSourceSelectArguments.Empty);
                DataTable dt = dv.ToTable();
                Label lbl = null;
                if (this.gvReporte.FooterRow == null)
                {
                    return;
                }
                lbl = (Label)this.gvReporte.FooterRow.FindControl("lblEntrada");
                if (lbl != null)
                {
                    lbl.Text = double.Parse(dt.Compute("SUM(Entrada)", "").ToString()).ToString("N2");
                }
                lbl = (Label)this.gvReporte.FooterRow.FindControl("lblDescuentos");
                if (lbl != null)
                {
                    lbl.Text = double.Parse(dt.Compute("SUM(KG_Descuentos)", "").ToString()).ToString("N2");
                }
                lbl = (Label)this.gvReporte.FooterRow.FindControl("lblEntradaNeto");
                if (lbl != null)
                {
                    lbl.Text = double.Parse(dt.Compute("SUM(Entrada_Neto)", "").ToString()).ToString("N2");
                }
                lbl = (Label)this.gvReporte.FooterRow.FindControl("lblSalida");
                if (lbl != null)
                {
                    lbl.Text = double.Parse(dt.Compute("SUM(Salida)", "").ToString()).ToString("N2");
                }
                lbl = (Label)this.gvReporte.FooterRow.FindControl("lblBoletas");
                if (lbl != null)
                {
                    lbl.Text = int.Parse(dt.Compute("SUM(Cantidad_De_Boletas)", "").ToString()).ToString();
                }

            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "calculating totales I/O boletas por dia.", ex);
            }

        }
    }
}
