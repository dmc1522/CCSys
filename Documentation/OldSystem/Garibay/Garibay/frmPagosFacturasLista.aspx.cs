using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmPagosFacturasLista : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                this.ddlCiclos.DataBind();
                this.gvPagos.DataBind();
            }
        }

        protected void ddlCiclos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvPagos.DataBind();
        }

        protected string GetURL(string id)
        {
            string myUrl = "frmPagoAFacturas.aspx";
            myUrl += Utils.GetEncriptedQueryString("movID=" + id);
            return myUrl;
        }
    }
}
