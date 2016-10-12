using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmListaAnticipos : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            this.sdsAnticipos.DataBind();
            this.gvAnticipos.DataBind();
        }

        protected String GetURLToOpenLiq(string id)
        {
            return "~/frmLiquidacion2010.aspx" + Utils.GetEncriptedQueryString("liqID=" + id);
        }

    }

}
