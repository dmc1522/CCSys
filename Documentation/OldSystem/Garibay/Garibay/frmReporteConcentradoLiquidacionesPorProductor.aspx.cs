using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Garibay
{
    public partial class frmReporteConcentradoLiquidacionesPorProductor : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvLiquidaciones_DataBound(object sender, EventArgs e)
        {
           
            this.calculatotales();
        }
        protected void calculatotales(){
            if (this.gvLiquidaciones.FooterRow != null)
            {
               // this.gvLiquidaciones.DataBind();
                this.gvLiquidaciones.FooterRow.Cells[1].Text = "TOTALES";
                double kgrecibidos = 0, dctoPorHumedad = 0, dctoPorImpurezas = 0, totalDescuentos = 0, kgnetos=0, importe = 0, dctoSecado = 0, importeNeto = 0, notas = 0, interes = 0, seguro = 0, totalapagar = 0;

                foreach(GridViewRow row in this.gvLiquidaciones.Rows){
                    if(row.RowType == DataControlRowType.DataRow){
                        kgrecibidos += double.Parse(row.Cells[1].Text);
                        dctoPorHumedad += double.Parse(row.Cells[2].Text);
                        dctoPorImpurezas += double.Parse(row.Cells[3].Text);
                        totalDescuentos += double.Parse(row.Cells[4].Text);
                        kgnetos += double.Parse(row.Cells[5].Text);
                        importe += double.Parse(row.Cells[6].Text,NumberStyles.Currency);
                        dctoSecado += double.Parse(row.Cells[7].Text, NumberStyles.Currency);
                        importeNeto += double.Parse(row.Cells[8].Text, NumberStyles.Currency);
                        notas += double.Parse(row.Cells[9].Text, NumberStyles.Currency);
                        interes += double.Parse(row.Cells[10].Text, NumberStyles.Currency);
                        seguro += double.Parse(row.Cells[11].Text, NumberStyles.Currency);
                        totalapagar += double.Parse(row.Cells[12].Text, NumberStyles.Currency);
                    }

                }
                this.gvLiquidaciones.FooterRow.Cells[1].Text = string.Format("{0:N2}", kgrecibidos);
                this.gvLiquidaciones.FooterRow.Cells[2].Text = string.Format("{0:N2}", dctoPorHumedad);
                this.gvLiquidaciones.FooterRow.Cells[3].Text = string.Format("{0:N2}", dctoPorImpurezas);
                this.gvLiquidaciones.FooterRow.Cells[4].Text = string.Format("{0:N2}", totalDescuentos);
                this.gvLiquidaciones.FooterRow.Cells[5].Text = string.Format("{0:N2}", kgnetos);
                this.gvLiquidaciones.FooterRow.Cells[6].Text = string.Format("{0:C2}", importe);
                this.gvLiquidaciones.FooterRow.Cells[7].Text = string.Format("{0:C2}", dctoSecado);
                this.gvLiquidaciones.FooterRow.Cells[8].Text = string.Format("{0:C2}", importeNeto);
                this.gvLiquidaciones.FooterRow.Cells[9].Text = string.Format("{0:C2}", notas);
                this.gvLiquidaciones.FooterRow.Cells[10].Text = string.Format("{0:C2}", interes);
                this.gvLiquidaciones.FooterRow.Cells[11].Text = string.Format("{0:C2}", seguro);
                this.gvLiquidaciones.FooterRow.Cells[12].Text = string.Format("{0:C2}", totalapagar);

              


            }

        }

        protected void gvLiquidaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }
            HyperLink link = (HyperLink)e.Row.Cells[0].FindControl("HyperLink1");
            if (link != null)
            {
                string parameter, ventanatitle = "REPORTE CONCENTRADO DE LIQUIDACIONES POR PRODUCTOR DETALLADO";
                // String pathArchivotemp = PdfCreator.printLiquidacion(0, PdfCreator.tamañoPapel.CARTA, PdfCreator.orientacionPapel.VERTICAL, ref this.gvBoletas, ref gvAnticipos, ref gvPagosLiquidacion);
                string datosaencriptar;
                datosaencriptar = "prodID=";
                datosaencriptar += this.gvLiquidaciones.DataKeys[e.Row.RowIndex]["productorID"].ToString();
                datosaencriptar += "&";
                datosaencriptar += "cicloID=";
                datosaencriptar += this.drpdlCiclo.SelectedValue;
                datosaencriptar += "&";

                parameter = "javascript:url('";
                parameter += "frmReporteDesglosadoLiquidacionesconDetalleProductor.aspx?data=";
                parameter += Utils.encriptacadena(datosaencriptar);
                parameter += "', '";
                parameter += ventanatitle;
                parameter += "',200,70,1020,600); return false;";
                link.Attributes.Add("onClick", parameter);
                link.NavigateUrl = this.Request.Url.ToString();
                //link.Visible = ((CheckBox)e.Row.Cells[8].Controls[0]).Checked;
            }
           
        }
    }
}
