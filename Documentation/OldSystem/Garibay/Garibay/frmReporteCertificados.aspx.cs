using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmReporteCertificados : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack){
                this.gvCertificados.DataBind();
              //  this.gvReporteTodos.DataBind();
            }

        }
    }
}
