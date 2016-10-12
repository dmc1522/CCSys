using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
namespace Garibay
{
    public partial class frmReporteLiquidacionconNotas : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack){
                this.drpdlCiclo.DataBind();
            }
         
         }
        protected void gvLiquidaciones_DataBound(object sender, EventArgs e)
        {
            try
            {
                double fBoletas = 0, fNotas = 0, fSeguro = 0, fInteres = 0, fPagos = 0;
                foreach (GridViewRow row in gvLiquidaciones.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        if (row.Cells[3].Text.Length > 0 && row.Cells[3].Text!= "&nbsp;") fBoletas += double.Parse(row.Cells[3].Text, NumberStyles.Currency);

                        if (row.Cells[4].Text.Length > 0 && row.Cells[4].Text != "&nbsp;") fNotas += double.Parse(row.Cells[4].Text, NumberStyles.Currency);
                        if (row.Cells[5].Text.Length > 0 && row.Cells[5].Text!= "&nbsp;") fInteres += double.Parse(row.Cells[5].Text, NumberStyles.Currency);
                        if (row.Cells[6].Text.Length > 0 && row.Cells[6].Text != "&nbsp;") fSeguro += double.Parse(row.Cells[6].Text, NumberStyles.Currency);
                        if (row.Cells[7].Text.Length > 0 && row.Cells[7].Text != "&nbsp;") fPagos += double.Parse(row.Cells[7].Text, NumberStyles.Currency);

                    }

                }
                this.gvLiquidaciones.FooterRow.Cells[0].Text = "TOTALES";
                this.gvLiquidaciones.FooterRow.Cells[3].Text = string.Format("{0:C2}", fBoletas);
                this.gvLiquidaciones.FooterRow.Cells[4].Text = string.Format("{0:C2}", fNotas);
                this.gvLiquidaciones.FooterRow.Cells[5].Text = string.Format("{0:C2}", fInteres);
                this.gvLiquidaciones.FooterRow.Cells[6].Text = string.Format("{0:C2}", fSeguro);
                this.gvLiquidaciones.FooterRow.Cells[7].Text = string.Format("{0:C2}", fPagos);
            }
            catch(Exception ex){
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, this.UserID, "ERROR AL CONVERTIR TOTALES EN REPORTE. EX", this.Request.Url.ToString());
            }
           
        }   
       

        protected void gvLiquidaciones_DataBound()
        {
        
        }
    }
}
