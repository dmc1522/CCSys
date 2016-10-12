using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmConcentradoProductosdeBoletas : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack){
                this.drpdlCiclo.DataBind();
                this.gridEntrada.DataBind();
                this.gridBoletasPorProductor.DataBind();
                
            }
        }

        protected void drpdlCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gridEntrada.DataBind();
            this.gridBoletasPorProductor.DataBind();
        }

        protected void gridEntrada_DataBound(object sender, EventArgs e)
        {               
            Utils.MergeSameRowsInGVPerColumn(ref this.gridEntrada, 0);        
        }

        protected void gridBoletasPorProductor_DataBound(object sender, EventArgs e)
        {
            Utils.MergeSameRowsInGVPerColumn(ref this.gridBoletasPorProductor, 0);
           // Utils.MergeSameRowsInGVPerColumn(ref this.gridBoletasPorProductor, 1); 
        }
    }
}
