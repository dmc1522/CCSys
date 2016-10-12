using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Garibay
{
    public partial class frmReporteGeneralCreditos : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                this.TextBoxFecha.Text = Utils.Now.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture);
            }
            if (this.IsSistemBanco)
            {
                this.sdsReporteGlobal.SelectCommand = "ReturnReporteGlobalCreditos_SisBancos";
            }
            else
            {
                this.sdsReporteGlobal.SelectCommand = "ReturnReporteGlobalCreditos";
            }
            
        }

        protected void ddlCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GridViewReporteGlobal.DataBind();
        }

        protected void GridViewReporteGlobal_DataBound(object sender, EventArgs e)
        {
            double limite = 0, notasConInteres = 0, notasSinInteres = 0, prestamos = 0, seguro = 0, intereses = 0, totaladeudos = 0, abonado = 0, total = 0, descuentos = 0;
            foreach(GridViewRow row in GridViewReporteGlobal.Rows)
            {
                if(row.RowType == DataControlRowType.DataRow)
                {
                    limite += Utils.GetSafeFloat(row.Cells[3].Text);
                    notasConInteres += Utils.GetSafeFloat(row.Cells[4].Text);
                    notasSinInteres += Utils.GetSafeFloat(row.Cells[5].Text);
                    prestamos += Utils.GetSafeFloat(row.Cells[6].Text);
                    seguro += Utils.GetSafeFloat(row.Cells[7].Text);
                    intereses += Utils.GetSafeFloat(row.Cells[8].Text);
                    totaladeudos += Utils.GetSafeFloat(row.Cells[9].Text);
                    abonado += Utils.GetSafeFloat(row.Cells[10].Text);
                    descuentos += Utils.GetSafeFloat(row.Cells[11].Text);
                    total += Utils.GetSafeFloat(row.Cells[12].Text);                    
                }
            }
            if (GridViewReporteGlobal.FooterRow != null)
            {
                this.GridViewReporteGlobal.FooterRow.Cells[3].Text = string.Format("{0:c2}", limite);
                this.GridViewReporteGlobal.FooterRow.Cells[4].Text = string.Format("{0:c2}", notasConInteres);
                this.GridViewReporteGlobal.FooterRow.Cells[5].Text = string.Format("{0:c2}", notasSinInteres);
                this.GridViewReporteGlobal.FooterRow.Cells[6].Text = string.Format("{0:c2}", prestamos);
                this.GridViewReporteGlobal.FooterRow.Cells[7].Text = string.Format("{0:c2}", seguro);
                this.GridViewReporteGlobal.FooterRow.Cells[8].Text = string.Format("{0:c2}", intereses);
                this.GridViewReporteGlobal.FooterRow.Cells[9].Text = string.Format("{0:c2}", totaladeudos);
                this.GridViewReporteGlobal.FooterRow.Cells[10].Text = string.Format("{0:c2}", abonado);
                this.GridViewReporteGlobal.FooterRow.Cells[11].Text = string.Format("{0:c2}", descuentos);
                this.GridViewReporteGlobal.FooterRow.Cells[12].Text = string.Format("{0:c2}", total);
            }
        }
    }
}
