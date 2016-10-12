using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Garibay
{
    public partial class ReporteGeneralBoletasLiquidadasYFacturadasConcentradasPorProducto : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DropDownListCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DropDownListProducto.DataBind();
        }

        protected void DropDownListProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvBoletasNoLiquidadas.DataBind();
            this.GridViewBoletasLiquidadas.DataBind();
            this.gvBoletasFacturadas.DataBind();
            this.gvBoletasNoFacturadas.DataBind();
        }

        protected void GridViewBoletasLiquidadas_DataBound(object sender, EventArgs e)
        {
            try
            {
                string []cols  = new string [] {"CantidadBoletas", "PesoBruto", "Descuento_Humedad",
                "Descuento_Impurezas", "PesoNeto", "TotalAPagar", "Descuento_Secado", "anticipos",
                "Pagos_Creditos", "Pagos", "Restante"};

                string[] formats = new string[] {"N0", "N2", "N2","N2", "N2", "C2", "C2", "C2","C2", "C2", "C2"};
                DataView dv = (DataView)this.SqlDataSourceBoletasLiquidadas.Select(DataSourceSelectArguments.Empty);
                DataTable dt = dv.ToTable();
                if(dt.Rows.Count > 0)
                {
                    double d = 0;
                    for (int i = 2; i < this.GridViewBoletasLiquidadas.Columns.Count; i++)
                    {
                        d = double.Parse(dt.Compute("sum(" + cols[i - 2] + ")", "").ToString());
                        this.GridViewBoletasLiquidadas.FooterRow.Cells[i].Text = string.Format("{0:" + formats[i - 2] + "}", d);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "calculating footer in bol liq", ex);
            }
        }

        protected void gvBoletasNoLiquidadas_DataBound(object sender, EventArgs e)
        {
            try
            {
                if (this.gvBoletasNoLiquidadas.FooterRow == null)
                {
                    return;
                }
                DataView dv = (DataView)this.SqlDataSourceBoletasNoLiquidadas.Select(DataSourceSelectArguments.Empty);
                DataTable dt = dv.ToTable();

                if (dt.Rows.Count == 0)
                {
                    return;
                }

                Label lbl = null;

                lbl = (Label)this.gvBoletasNoLiquidadas.FooterRow.FindControl("lblTotalBoletas");
                if (lbl != null)
                {
                    lbl.Text = string.Format("{0:N0}", int.Parse(dt.Compute("SUM(CantidadBoletas)", "").ToString()));
                }

                lbl = (Label)this.gvBoletasNoLiquidadas.FooterRow.FindControl("lblPesoBruto");
                if (lbl != null)
                {
                    lbl.Text = string.Format("{0:N2}", double.Parse(dt.Compute("SUM(PesoBruto)", "").ToString()));
                }

                lbl = (Label)this.gvBoletasNoLiquidadas.FooterRow.FindControl("lblPesoNeto");
                if (lbl != null)
                {
                    lbl.Text = string.Format("{0:N2}", double.Parse(dt.Compute("SUM(PesoNeto)", "").ToString()));
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "calulando footer de gvBoletasNoLiq", ex);
            }
        }

        protected void gvBoletasFacturadas_DataBound(object sender, EventArgs e)
        {
            if (this.gvBoletasFacturadas.FooterRow == null)
            {
                return;
            }
            DataView dv = (DataView)this.SqlDataSourceBoletasFacturadas.Select(DataSourceSelectArguments.Empty);
            DataTable dt = dv.ToTable();

            if (dt.Rows.Count == 0)
            {
                return;
            }

            this.GetGVTotalFooterFormatted(gvBoletasFacturadas,
                dt,
                "{0:N0}",
                "SUM(CantidadBoletas)",
                "lblTotalBoletas");

            this.GetGVTotalFooterFormatted(gvBoletasFacturadas,
                dt,
                "{0:N2}",
                "SUM(PesoBruto)",
                "lblPesoBruto");

            this.GetGVTotalFooterFormatted(gvBoletasFacturadas,
                dt,
                "{0:N2}",
                "SUM(PesoNeto)",
                "lblPesoNeto");

            this.GetGVTotalFooterFormatted(gvBoletasFacturadas,
                dt,
                "{0:C2}",
                "SUM(TotalAPagar)",
                "lblTotalAPagar");

            this.GetGVTotalFooterFormatted(gvBoletasFacturadas,
                dt,
                "{0:C2}",
                "SUM(Pagos)",
                "lblPagos");
        }

        protected void GetGVTotalFooterFormatted(GridView gv, DataTable dt, string formatString, string dtExpresion, string footerLabel)
        {
            if (gv == null || dt.Rows.Count == 0)
            {
                return;
            }
            try
            {
                Label lbl = null;
                lbl = (Label)gv.FooterRow.FindControl(footerLabel);
                if (lbl != null)
                {
                    lbl.Text = string.Format(formatString, double.Parse(dt.Compute(dtExpresion, "").ToString()));
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "GetGVTotalFooterFormatted", ex);
            }
        }

        protected void gvBoletasNoFacturadas_DataBound(object sender, EventArgs e)
        {
            if (this.gvBoletasNoFacturadas.FooterRow == null)
            {
                return;
            }
            DataView dv = (DataView)this.SqlDataSourceBoletasNoFacturadas.Select(DataSourceSelectArguments.Empty);
            DataTable dt = dv.ToTable();

            if (dt.Rows.Count == 0)
            {
                return;
            }

            this.GetGVTotalFooterFormatted(gvBoletasNoFacturadas,
                dt,
                "{0:N0}",
                "SUM(CantidadBoletas)",
                "lblCantidadBoletas");

            this.GetGVTotalFooterFormatted(gvBoletasNoFacturadas,
                dt,
                "{0:N2}",
                "SUM(PesoBruto)",
                "lblPesoBruto");

            this.GetGVTotalFooterFormatted(gvBoletasNoFacturadas,
                dt,
                "{0:N2}",
                "SUM(PesoNeto)",
                "lblPesoNeto");
        }
    }
}
