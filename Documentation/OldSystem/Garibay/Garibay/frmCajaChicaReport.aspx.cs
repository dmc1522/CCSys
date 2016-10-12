using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Garibay
{
    public partial class frmCajaChicaReport : BasePage
    {
        public frmCajaChicaReport():base()
        {
            this.hasCalendar = true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack){
                this.panelMensaje.Visible = false;
                DateTime dtInicio, dtFin;
                DateTime dtHoy = Utils.Now;
                dtInicio = new DateTime(dtHoy.Year, dtHoy.Month, 1);
                dtFin = Utils.Now;
                /*
                TimeSpan tsDays = dtFin - dtInicio;
                               dtFin = new DateTime(dtHoy.Year,dtHoy.Month,tsDays.Days);*/

                this.txtFecha1.Text = dtInicio.ToString("dd/MM/yyyy");
                this.txtFecha2.Text = dtFin.ToString("dd/MM/yyyy");
                this.txtFecha1formatted.Text = Utils.converttoLongDBFormat(this.txtFecha1.Text);
                this.txtFecha2formatted.Text = Utils.converttoLongDBFormat(this.txtFecha2.Text);
                this.gridConcentrado.DataBind();
                
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.txtFecha1formatted.Text = Utils.converttoLongDBFormat(this.txtFecha1.Text);
            this.txtFecha2formatted.Text = Utils.converttoLongDBFormat(this.txtFecha2.Text);
            this.gridConcentrado.DataBind();

        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            string sFileName = "ConcentradoCajaChica" + Utils.Now.ToString("dd-MM-yyyy") + ".xls";
            ExportToExcel(sFileName, ref this.gridConcentrado);
        }

        private void ExportToExcel(string strFileName, ref GridView dg)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", strFileName));
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            dg.AllowPaging = false; //all the movements in one page.
            dg.AllowSorting = false;
            dg.EnableViewState = false;
            dg.DataBind();
            Page pPage = new Page();
            HtmlForm fForm = new HtmlForm();
            Label lbltemp = new Label();
            pPage.SkinID = "skinverde";
            string sHeader = "<table><tr><td colspan='4' class='TableHeader'><b>REPORTE CONCENTRADO DE MOVIMIENTOS DE CAJA CHICA</b></td></tr><tr><td><b>";
            sHeader += "Periodo: </b></td><td>" + this.txtFecha1.Text;
            sHeader += "</td><td><b>A:</b></td><td>" + this.txtFecha2.Text + "</td></tr>";
            sHeader += "</table>";
            lbltemp.Text = sHeader;
            pPage.Controls.Add(lbltemp);
            pPage.Controls.Add(fForm);
            fForm.Attributes.Add("runat", "server");
            //this.gridMovCuentasBanco.RenderControl(oHtmlTextWriter);
            dg.BorderWidth = 1;
            fForm.Controls.Add(dg);
            pPage.EnableTheming = true;
            pPage.RenderControl(oHtmlTextWriter);
            dg.AllowPaging = true;
            dg.AllowSorting = true;
            dg.BorderWidth = 0;
            Response.Write(oStringWriter.ToString());
            Response.End();
        }
    }
}
