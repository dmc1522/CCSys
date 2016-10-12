using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace Garibay
{
    public partial class frmListSalidaDeProducto : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                this.cmbBodega.DataBind();
                this.cmbProducto.DataBind();
                this.ddlTipodeSalida.DataBind();
            }
            this.compruebasecurityLevel();
            
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.btnAgregarSalida.Visible = false;
            }

        }

     
        protected void btnAgregarSalida_Click(object sender, EventArgs e)
        {
            this.Server.Transfer("~/frmAddModifySalidaDeProducto.aspx");
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            this.btnFiltrar.DataBind();
        }
    }
}
