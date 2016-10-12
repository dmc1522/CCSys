using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmReporteGralPorCatalogo : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack){

                DateTime dtInicio = new DateTime();
                DateTime dtFin = new DateTime();
                DateTime dtHoy = Utils.Now;
                dtInicio = new DateTime(dtHoy.Year, dtHoy.Month, 1);
                // TimeSpan tsOneDay = new TimeSpan(1, 0, 0, 0);
                dtFin = Utils.Now;
                /*
                TimeSpan tsDays = dtFin - dtInicio;
                               dtFin = new DateTime(dtHoy.Year,dtHoy.Month,tsDays.Days);*/

                this.txtFecha1.Text = dtInicio.ToString("dd/MM/yyyy");
                this.txtFecha2.Text = dtFin.ToString("dd/MM/yyyy");
                this.txtFecha1Larga.Text = Utils.converttoLongDBFormat(this.txtFecha1.Text);
                this.txtFecha2Larga.Text = Utils.converttoFechaForFilterLimite(this.txtFecha2.Text);
                this.gridMovBancosCatalogo.DataBind();
            }

        }

        protected void PopCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            this.txtFecha1Larga.Text = Utils.converttoLongDBFormat(this.txtFecha1.Text);

        }

        protected void PopCalendar2_SelectionChanged(object sender, EventArgs e)
        {
            this.txtFecha2Larga.Text = Utils.converttoLongDBFormat(this.txtFecha2.Text);
            
        }

   
        protected void gridMovBancosCatalogo_PreRender(object sender, EventArgs e)
        {
            Utils.MergeSameRowsInGV(ref this.gridMovBancosCatalogo); 
        }
    }
}
