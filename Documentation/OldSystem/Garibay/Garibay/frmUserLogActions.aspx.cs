using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmUserLogActions : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnAplicaFiltros_Click(object sender, EventArgs e)
        {
            this.GridViewUserLog.PageSize = int.Parse(this.ddlElemXPage.Text);
            string sFiltros = " ";
            bool bANDreq = false;
            if (this.chkFiltroUsuarios.Checked)
            {
                sFiltros = " userID = ";
                sFiltros += this.ddlUsuarios.SelectedValue;
                bANDreq = true;
            }
            if (this.chkFiltroModulos.Checked)
            {
                sFiltros += bANDreq ? " AND " : "";
                sFiltros += " moduleID = ";
                sFiltros += this.ddlModulos.SelectedValue;
            }

            this.sdsListaUserLog.FilterExpression = sFiltros;
            this.GridViewUserLog.PageIndex = 0;
        }
    }
}
