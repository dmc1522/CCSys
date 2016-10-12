using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Garibay
{
    public partial class frmListPrestamos : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.gdvPrestamos.SelectedIndex = -1;
                this.panelMensaje.Visible = false;
                //this.btnExport.Visible = false;
                //                 foreach (ListItem cb in this.cblColToShow.Items)
                //                 {
                //                     cb.Selected = true;
                //                 }
                this.ddlCiclos.DataBind();
                this.gdvPrestamos.DataBind();
                this.ddlCredito.DataBind();
                this.gdvPrestamos.SelectedIndex = -1;
                this.showHideColumns();
            }
            if (this.panelMensaje.Visible)
            {
                this.panelMensaje.Visible = false;
                this.panelagregar.Visible = true;
            }

            this.compruebasecurityLevel();
            this.Eliminar(true);

        }

        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.btnAgregarNuevo.Visible = false;
                //this.btnModificar.Visible = false;
                //this.gdvPrestamos.Columns[13].Visible = false;


            }

        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            this.UpdateGrid();
        }

        protected void UpdateGrid()
        {
            string sFiltros = " ";
            if (this.chkCredito.Checked)
            {
                sFiltros = "creditoID = ";
                sFiltros += this.ddlCredito.SelectedValue;
            }

            this.sqlPrestamos.FilterExpression = sFiltros;
            this.gdvPrestamos.DataBind();
            this.gdvPrestamos.PageIndex = 0;
            this.showHideColumns();
        }

        private void showHideColumns()
        {
            foreach (DataControlField col in this.gdvPrestamos.Columns)
            {
                ListItem item = this.cblColToShow.Items.FindByText(col.HeaderText);
                if (item != null)
                {
                    col.Visible = item.Selected;
                }
            }
        }

        protected void btnAceptarMensaje_Click(object sender, EventArgs e)
        {

        }

        protected void gdvPrestamos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            HyperLink link = (HyperLink)e.Row.FindControl("linkAbrir");
            if (link != null)
            {
                link.Text = "Abrir";
                String sQuery = "idtomodify=" + this.gdvPrestamos.DataKeys[e.Row.RowIndex]["anticipoID"].ToString();
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = "~/frmAddModifyPrestamo.aspx";
                strRedirect += sQuery;
                link.NavigateUrl = strRedirect;
            }

            LinkButton linkDel = (LinkButton)e.Row.Cells[0].FindControl("lnkDelete");
            if (linkDel != null)
            {
                linkDel.Attributes.Add("onclick", "return confirm('¿Desea eliminar el Prestamo número: " + this.gdvPrestamos.DataKeys[e.Row.RowIndex]["anticipoID"].ToString() + "?')");
            }

            HyperLink  linkPagare = (HyperLink)e.Row.FindControl("lnkPagare");
            if (linkPagare != null)
            {
                CreateURLForPagare(linkPagare, gdvPrestamos.DataKeys[e.Row.RowIndex]["creditoID"].ToString(), e.Row.Cells[9].Text, this.gdvPrestamos.DataKeys[e.Row.RowIndex]["fecha"].ToString());
            }
            
        }
        protected void Eliminar(Boolean activaacgregar)
        {

            this.btnEliminar.Visible = activaacgregar;
            if (this.gdvPrestamos.SelectedIndex == -1)
            {
                this.btnEliminar.Visible = false;
            }
        }

        protected void gdvPrestamos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Eliminar(true);
        }

        protected void ddlCiclos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        protected void btnAgregarNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmAddModifyPrestamo.aspx", true);
        }

        protected void gdvPrestamos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.sqlPrestamos.DeleteParameters.Add("@anticipoID", System.TypeCode.Int32, e.Keys[0].ToString());
        }

        protected void gdvPrestamos_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            this.gdvPrestamos.DataBind();
        }

        protected void CreateURLForPagare(HyperLink lnkImprimePagare, string idCredito, string total, string fecha)
        {
            #region Add link for print pagare
            try
            {
                
                    string sFileName = "PAGARE.pdf";
                    sFileName = sFileName.Replace(" ", "_");
                    string sURL = "frmDescargaTmpFile.aspx";
                    string datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
                    datosaencriptar = datosaencriptar + "solID=-1&creditoID=" + idCredito + "&";
                    datosaencriptar += "impPagare=1&monto=" + Utils.GetSafeFloat(total).ToString() + "&";
                    datosaencriptar += "fecha=" + fecha + "&";
                    string URLcomplete = sURL + "?data=";
                    URLcomplete += Utils.encriptacadena(datosaencriptar);
                    lnkImprimePagare.NavigateUrl = this.Request.Url.ToString();
                    JSUtils.OpenNewWindowOnClick(ref lnkImprimePagare, URLcomplete, "Pagare", true);
                }
            
            catch { }
            #endregion
        }

        protected void ddlCiclos_SelectedIndexChanged1(object sender, EventArgs e)
        {
            this.UpdateGrid();
        }


    }
}
