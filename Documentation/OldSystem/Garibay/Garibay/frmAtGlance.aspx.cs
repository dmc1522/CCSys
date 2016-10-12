using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Garibay
{
    public partial class frmAtGlance : Garibay.BasePage
    {
        internal void UpdateBancosMenu()
        {
            if (this.IsSistemBanco)
            {
                this.divAtGlanceBanco.Visible = true;
                this.divAtGlance.Visible = false;
            }
            else
            {
                this.divAtGlance.Visible = true;
                this.divAtGlanceBanco.Visible = false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UpdateBancosMenu();
            if (!this.IsPostBack)
            {
                this.ddlCurrBodega.DataBind();
                if (this.BodegaID == -1)
                {
                    this.ddlCurrBodega.SelectedIndex = 0;
                    this.BodegaID = int.Parse(this.ddlCurrBodega.SelectedValue);
                }
                this.ddlCurrBodega.SelectedValue = this.BodegaID.ToString();
            }
            String sQuery = "SELECT COUNT(LiquidacionID) AS TotalLiq FROM Liquidaciones where storeTS >= @storeTS";

            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand comm = new SqlCommand(sQuery, conn);
                comm.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = DateTime.Parse(Utils.Now.ToString("dd/MM/yyyy"));
                conn.Open();
                this.lblCountLiq.Text =  int.Parse(comm.ExecuteScalar().ToString()).ToString();

                comm.CommandText = "SELECT COUNT(boletaID) AS Cantidad  FROM Boletas where storeTS >= @storeTS";
                this.lblCountBoletas.Text = int.Parse(comm.ExecuteScalar().ToString()).ToString();
            }
            catch (System.Exception ex)
            {
                this.lblCountLiq.Text = this.lblCountLiq.Text = "0";
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "No se ejecuto: " + sQuery, this.Request.Url.ToString(), ref ex);
            }
            finally
            {
                conn.Close();
            }

            SqlCommand commNV = new SqlCommand();
            commNV.CommandText = "SELECT     count(*) FROM Notasdeventa where storeTS >= @storeTS";
            try
            {
                commNV.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.Now.ToString("dd/MM/yyyy");
                int NVadded = dbFunctions.GetExecuteIntScalar(commNV,0);
                this.lblCountNV.Text = NVadded.ToString();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "getting nv added today", ref ex);
            }
        }

        protected void ddlCurrBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BodegaID = int.Parse(this.ddlCurrBodega.SelectedValue);
        }

        protected void btnBusquedaGeneral_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("frmBusquedaGeneral.aspx");
        }

        protected void ddlCicloReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvReporteBoletas.DataBind();
            this.gvPagosGas.DataBind();
            this.gvReporteBoletasMargaritas.DataBind();
        }

        protected string GetReporteEntradasSalidasURL(string productoID)
        {
            String strRedirect = "~/frmEntradasSalidasBoletasxDia.aspx";
            if (productoID != null &&  productoID.Trim().Length > 0)
            {
                String sQuery = "productoid=" + productoID;
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                strRedirect += sQuery;
            }
            return strRedirect;
        }

       
    }
}
