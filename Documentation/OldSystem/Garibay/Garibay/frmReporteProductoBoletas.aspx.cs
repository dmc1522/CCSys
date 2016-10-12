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
    public partial class frmReporteProductoBoletas : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void drpdlCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gridBoletasProductos.DataBind();
        }

      
    }
}
