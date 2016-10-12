using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmCajaChicaSaldosMensuales : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.ddlBodegas.DataBind();
                this.ddlBodegas.SelectedValue = this.BodegaID.ToString();
            }
        }

        protected void ddlBodegas_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvSaldosMensuales.DataBind();
        }
    }
}
