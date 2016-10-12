using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmSituacionesLista : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                this.gvSituaciones.DataBind();
            }
        }

        protected void gvSituaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if(e.Row.RowType != DataControlRowType.DataRow){
                return;

            }
            HyperLink HL = (HyperLink)(e.Row.Cells[5].FindControl("HLOpen"));
            if(HL!=null)
            {
                String sQuery = "situacionID=" + this.gvSituaciones.DataKeys[e.Row.RowIndex]["situacionID"].ToString();
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = "~/frmSituacionAddUpdate.aspx";
                strRedirect += sQuery;
                HL.NavigateUrl = strRedirect;

            }
        }
    }
}
