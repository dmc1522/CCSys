using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Garibay
{
    public partial class frmReporteConcentradoLiquidaciones :Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvLiquidaciones_DataBound(object sender, EventArgs e)
        {
            GridViewRow currRow, nextRow;
            for (int i = 0; i < this.gvLiquidaciones.Rows.Count; i++)
            {
                currRow = this.gvLiquidaciones.Rows[i];
                if (currRow.RowType == DataControlRowType.DataRow && i < this.gvLiquidaciones.Rows.Count - 1)
                {
                    int iRowSpan = 1;
                    try
                    {
                        do
                        {
                            if (i >= this.gvLiquidaciones.Rows.Count - 1)
                            {
                                break;
                            }
                            nextRow = this.gvLiquidaciones.Rows[i + 1];
                            if (nextRow.Cells[0].Text == currRow.Cells[0].Text)
                            {
                                iRowSpan++; i++;
                                nextRow.Cells[0].Visible = false;
                                if (i + 1 >= this.gvLiquidaciones.Rows.Count)
                                {
                                    break;
                                }
                                nextRow = this.gvLiquidaciones.Rows[i + 1];
                            }
                        } while (i < this.gvLiquidaciones.Rows.Count - 1 && nextRow.Cells[0].Text == currRow.Cells[0].Text);
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.SELECT, "gvLiquidaciones_DataBound en ReporteDesglosado", this.Request.Url.ToString(), ref ex);
                    }
                    currRow.Cells[0].RowSpan = iRowSpan;
                }
            }
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
                        kgrecibidos += double.Parse(row.Cells[2].Text);
                        dctoPorHumedad += double.Parse(row.Cells[3].Text);
                        dctoPorImpurezas += double.Parse(row.Cells[4].Text);
                        totalDescuentos += double.Parse(row.Cells[5].Text);
                        kgnetos += double.Parse(row.Cells[6].Text);
                        importe += double.Parse(row.Cells[7].Text,NumberStyles.Currency);
                        dctoSecado += double.Parse(row.Cells[8].Text, NumberStyles.Currency);
                        importeNeto += double.Parse(row.Cells[9].Text, NumberStyles.Currency);
                        notas += double.Parse(row.Cells[10].Text, NumberStyles.Currency);
                        interes += double.Parse(row.Cells[11].Text, NumberStyles.Currency);
                        seguro += double.Parse(row.Cells[12].Text, NumberStyles.Currency);
                        totalapagar += double.Parse(row.Cells[13].Text, NumberStyles.Currency);
                    }

                }
                this.gvLiquidaciones.FooterRow.Cells[2].Text = string.Format("{0:N2}", kgrecibidos);
                this.gvLiquidaciones.FooterRow.Cells[3].Text = string.Format("{0:N2}", dctoPorHumedad);
                this.gvLiquidaciones.FooterRow.Cells[4].Text = string.Format("{0:N2}", dctoPorImpurezas);
                this.gvLiquidaciones.FooterRow.Cells[5].Text = string.Format("{0:N2}", totalDescuentos);
                this.gvLiquidaciones.FooterRow.Cells[6].Text = string.Format("{0:N2}", kgnetos);
                this.gvLiquidaciones.FooterRow.Cells[7].Text = string.Format("{0:C2}", importe);
                this.gvLiquidaciones.FooterRow.Cells[8].Text = string.Format("{0:C2}", dctoSecado);
                this.gvLiquidaciones.FooterRow.Cells[9].Text = string.Format("{0:C2}", importeNeto);
                this.gvLiquidaciones.FooterRow.Cells[10].Text = string.Format("{0:C2}", notas);
                this.gvLiquidaciones.FooterRow.Cells[11].Text = string.Format("{0:C2}", interes);
                this.gvLiquidaciones.FooterRow.Cells[12].Text = string.Format("{0:C2}", seguro);
                this.gvLiquidaciones.FooterRow.Cells[13].Text = string.Format("{0:C2}", totalapagar);

              


            }

        }
    }
}
