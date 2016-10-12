using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Garibay
{
    public partial class frmReporteLiqXProductorXProductos : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.REPORTES, Logger.typeUserActions.SELECT, "EL REPORTE \"REPORTE POR PRODUCTOR POR PRODUCTO\"");
            }
        }
        public void GetNavigateURL(String ProductorID)
        {

        }

        protected void gvReporte_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            HyperLink lnk = (HyperLink)e.Row.FindControl("lnkProductor");
            if (lnk != null)
            {
                string parameter, ventanatitle = "REPORTE CONCENTRADO DE LIQUIDACIONES POR PRODUCTOR DETALLADO";
                // String pathArchivotemp = PdfCreator.printLiquidacion(0, PdfCreator.tamañoPapel.CARTA, PdfCreator.orientacionPapel.VERTICAL, ref this.gvBoletas, ref gvAnticipos, ref gvPagosLiquidacion);
                string datosaencriptar;
                datosaencriptar = "prodID=";
                datosaencriptar += this.gvReporte.DataKeys[e.Row.RowIndex]["productorID"].ToString();
                datosaencriptar += "&";
                datosaencriptar += "cicloID=";
                datosaencriptar += this.ddlCiclos.SelectedValue;
                datosaencriptar += "&";
                parameter = "javascript:url('";
                parameter += "frmReporteDesglosadoLiquidacionesconDetalleProductor.aspx?data=";
                parameter += Utils.encriptacadena(datosaencriptar);
                parameter += "', '";
                parameter += ventanatitle;
                parameter += "',200,70,1020,600); return false;";
                lnk.Attributes.Add("onClick", parameter);
                lnk.NavigateUrl = this.Request.Url.ToString();
            }
        }

        protected void ddlCiclos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlProductos.DataBind();
            this.gvReporte.DataBind();
        }

        protected void ddlProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvReporte.DataBind();
            this.gvPorProducto.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            this.gvReporte.DataBind();
            this.gvPorProducto.DataBind();
        }

        protected void gvPorProducto_DataBound(object sender, EventArgs e)
        {
            if (gvPorProducto.FooterRow == null)
            {
                return;
            }

            DataView dv = (DataView)this.sdsProductoPorPrecio.Select(DataSourceSelectArguments.Empty);
            DataTable dt = dv.ToTable();

            if (dt.Rows.Count  == 0)
            {
                return;
            }


            Utils.GetGVTotalFooterFormatted(gvPorProducto, dt,
                "{0:C2}", "AVG(precioapagar)", "lblprecioapagar");

            Utils.GetGVTotalFooterFormatted(gvPorProducto, dt,
                "{0:N2}", "SUM(PesoaPagar)", "lblPesoaPagar");

            Utils.GetGVTotalFooterFormatted(gvPorProducto, dt,
                "{0:C2}", "SUM(TotalPagar)", "lblTotalPagar");


        }
    }
}
