using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Garibay
{
    public partial class frmReporteDesglosadoLiquidacionesconDetalleProductor : Garibay.BasePage
    {
        int iProdID, iCicloID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)

            {
                iProdID = -1; iCicloID = -1;
                if (Request.QueryString["data"] != null && this.loadqueryStrings(Request.QueryString["data"].ToString()) && int.TryParse(this.myQueryStrings["prodID"].ToString(), out iProdID) && iProdID > -1 && int.TryParse(this.myQueryStrings["cicloID"].ToString(),out iCicloID)&& iCicloID>-1 )
                {

                    this.txtIdProductor.Text = iProdID.ToString();
                    txtIdCicloID.Text = iCicloID.ToString();
                    this.gvLiquidaciones.DataBind();
                    this.gvAnticipos.DataBind();
                    this.gridProductosPagados.DataBind();
                    this.gvPagos.DataBind();
                  


                }
            }


        }

        protected void gvLiquidaciones_DataBound(object sender, EventArgs e)
        {
         
            this.calculatotalesLiq();
        }
        protected void calculatotalesLiq(){
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
        protected void calculatotalesAnticipos()
        {
            if (this.gvAnticipos.FooterRow != null)
            {
                // this.gvLiquidaciones.DataBind();
                this.gvAnticipos.FooterRow.Cells[0].Text = "TOTALES";
                double totalBancos = 0, totalCaja = 0;

                foreach (GridViewRow row in this.gvAnticipos.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        totalBancos += double.Parse(row.Cells[5].Text,NumberStyles.Currency);
                        totalCaja += double.Parse(row.Cells[6].Text,NumberStyles.Currency);
                      
                       
                    }

                }
                this.gvAnticipos.FooterRow.Cells[5].Text = string.Format("{0:C2}", totalBancos);
                this.gvAnticipos.FooterRow.Cells[6].Text = string.Format("{0:C2}", totalCaja);
               




            }

        }

        protected void gvAnticipos_DataBound(object sender, EventArgs e)
        {
            this.calculatotalesAnticipos();
        }
        protected void calculatotalesPagos()
        {
            if (this.gvPagos.FooterRow != null)
            {
                // this.gvLiquidaciones.DataBind();
                this.gvPagos.FooterRow.Cells[0].Text = "TOTALES";
                double totalBancos = 0, totalCaja = 0;

                foreach (GridViewRow row in this.gvPagos.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        totalBancos += double.Parse(row.Cells[5].Text, NumberStyles.Currency);
                        totalCaja += double.Parse(row.Cells[6].Text, NumberStyles.Currency);


                    }

                }
                this.gvPagos.FooterRow.Cells[5].Text = string.Format("{0:C2}", totalBancos);
                this.gvPagos.FooterRow.Cells[6].Text = string.Format("{0:c2}", totalCaja);





            }

        }

        protected void gvPagos_DataBound(object sender, EventArgs e)
        {
            this.calculatotalesPagos();
        }

        protected void gridProductosPagados_DataBound(object sender, EventArgs e)
        {
            this.calculatotalesProductos();

        }
        protected void calculatotalesProductos()
        {
            if (this.gridProductosPagados.FooterRow != null)
            {
                // this.gvLiquidaciones.DataBind();
                this.gridProductosPagados.FooterRow.Cells[1].Text = "TOTALES";
                double totalAPagar = 0;

                foreach (GridViewRow row in this.gridProductosPagados.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        totalAPagar += double.Parse(row.Cells[4].Text, NumberStyles.Currency);
                       

                    }

                }
                this.gridProductosPagados.FooterRow.Cells[4].Text = string.Format("{0:C2}", totalAPagar);
                //this.gvPagos.FooterRow.Cells[6].Text = string.Format("{0:c2}", totalCaja);





            }

        }


        
    }
}
