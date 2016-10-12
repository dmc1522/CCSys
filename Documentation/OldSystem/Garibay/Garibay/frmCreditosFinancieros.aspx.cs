using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Garibay
{
    public partial class frmCreditosFinancieros : Garibay.BasePage
    {
        public frmCreditosFinancieros(): base()
        {
            this.hasCalendar = true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.panelMensaje.Visible = false;
            this.compruebasecurityLevel();
            if(!this.IsPostBack){
                this.gridCreditos.DataBind();
            }
            this.gridCreditos.DataSourceID = "sdsCreditos";

        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
               // this.panelMensaje.Visible = false;
               // this.divAgregar.Visible = false;
                this.gridCreditos.Columns[0].Visible = false;
            }

        }

       
        protected void gridCreditos_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void gridCreditos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sError = "";
            int creditoID = int.Parse(this.gridCreditos.DataKeys[e.RowIndex]["creditoFinancieroID"].ToString());
            if(!dbFunctions.EliminaCreditoFinanciero(creditoID, this.UserID, ref sError))
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, this.UserID, "NO SE PUDO ELIMINAR EL CREDITO FINANCIERO", this.Request.Url.ToString());
               
            }
            this.gridCreditos.EditIndex = -1;
        }

        protected void gridCreditos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }
            LinkButton link = (LinkButton)e.Row.FindControl("LinkButton1");
            if (link != null){

               

                String sQuery = "CreditoFinID=" + this.gridCreditos.DataKeys[e.Row.RowIndex]["creditoFinancieroID"];
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = "~/frmCreditoFinancieroAdd.aspx";
                strRedirect += sQuery;
                link.PostBackUrl = strRedirect;


            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            if(this.ddlEstado.SelectedValue=="0")
            {
                this.sdsCreditos.FilterExpression = "";
                this.gridCreditos.DataBind();

            }
            else if(this.ddlEstado.SelectedValue=="1")
            {
                this.sdsCreditos.FilterExpression = "pagado=TRUE";
                this.gridCreditos.DataBind();
            }
            else if (this.ddlEstado.SelectedValue == "2")
            {
                this.sdsCreditos.FilterExpression = "pagado<>TRUE";
                this.gridCreditos.DataBind();
            }
            
        }

    }
}
