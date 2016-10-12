using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmExistencias : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.LoadEncryptedQueryString() > 0)
            {
                this.bUseMasterPage = false;
            }
        }

        protected void gvExistencias_DataBound(object sender, EventArgs e)
        {
            Utils.MergeSameRowsInGV(ref this.gvExistencias);
        }
    }
}
