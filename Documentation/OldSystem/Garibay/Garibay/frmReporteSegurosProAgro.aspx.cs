using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmReporteSegurosProAgro : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ddlCiclos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvSeguros.DataBind();
        }
    }
}
