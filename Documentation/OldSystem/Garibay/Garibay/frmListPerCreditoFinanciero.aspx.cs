using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmListPerCreditoFinanciero : Garibay.BasePage
    {
        dsMovBanco.dtMovBancoDataTable dtTeibol = new dsMovBanco.dtMovBancoDataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
//                 if (dbFunctions.fillDTMovBancos(int.Parse(-1, fechainicio, fechafin, ref fSaldoInicial, ref fSaldoFinal, ref dtTeibol))
//                 {
//                     this.gridMovCuentasBanco.DataSourceID = "";
//                     this.gridMovCuentasBanco.DataSource = dtTeibol;
//                     this.gridMovCuentasBanco.DataBind();
//                 }
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
