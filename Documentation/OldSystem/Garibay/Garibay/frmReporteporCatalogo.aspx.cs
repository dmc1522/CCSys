using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmReporteporCatalogo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                DateTime dtInicio, dtFin;
                 dtInicio = new DateTime(Utils.Now.Year, Utils.Now.Month, 1);
                 TimeSpan tsUndia = new TimeSpan(1, 0, 0, 0);
                if (dtInicio.Month < 12)
                    dtFin = new DateTime(dtInicio.Year, dtInicio.Month + 1, 1, 23, 59, 59);
                else
                    dtFin = new DateTime(dtInicio.Year + 1, 1, 1, 23, 59, 59);
                dtFin = dtFin - tsUndia;
                this.txtDE.Text = dtInicio.ToString("dd/MM/yyyy");
                this.txtA.Text = dtFin.ToString("dd/MM/yyyy");
                this.changeFechas();
             
            }

        }

        protected void PopCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            this.changeFechas();
        }
        protected void changeFechas(){
            this.txtFechaInicio.Text = DateTime.Parse(this.txtDE.Text).ToString("yyyy/MM/dd 00:00:00");
            this.txtFechaFin.Text = DateTime.Parse(this.txtA.Text).ToString("yyyy/MM/dd 23:59:59");
            this.gvReporte.DataBind();
        }

        protected void PopCalendar2_SelectionChanged(object sender, EventArgs e)
        {
            this.changeFechas();
        }
    }
}
