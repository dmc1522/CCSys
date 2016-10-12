using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace Garibay
{
    public partial class frmPrintPaqueteFormatos : BasePage
    {
        //DataTable tablaSolImpr = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.grdvSolNoPrint.DataBind();
            //if (!IsPostBack)
            //{
            //    tablaSolImpr.Columns.Add("solicitudID");
            //    tablaSolImpr.PrimaryKey = new DataColumn[] { tablaSolImpr.Columns["solicitudID"] };
            //    tablaSolImpr.Columns.Add("Productor");
            //    tablaSolImpr.Columns.Add("Fecha");
            //    tablaSolImpr.Columns.Add("Monto");
            //}
        }

        protected void btnaddAFactura_Click(object sender, EventArgs e)
        {
  
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            
            grdvSolNoPrint.DataBind();
            CheckBox che;


            int[] aimpr = new int[grdvSolNoPrint.Rows.Count];

            for (int i = 0; i < grdvSolNoPrint.Rows.Count; i++ )
            {
                //che = (CheckBox)grdvSolNoPrint.Rows[i].Cells[0].FindControl("chkSol");
                //if (che.Checked)
                //{
                    aimpr[i] = int.Parse(grdvSolNoPrint.Rows[i].Cells[1].Text);
                //}
            }

            String pathArchivotemp;
            pathArchivotemp = FormatosPdf.impVariasSol(aimpr);

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename= paquete.pdf");
            Response.WriteFile(pathArchivotemp);
            Response.Flush();
            Response.End();
            try
            {
                File.Delete(pathArchivotemp);
            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "ERROR BORRANDO ARCHIVO TEMP DE PAQUETE", ref ex);
            }
        }
    }
}
