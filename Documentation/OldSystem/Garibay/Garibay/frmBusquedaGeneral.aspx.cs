using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmBusquedaGeneral : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.LoadEncryptedQueryString() == 1)
                {
                    if (this.myQueryStrings["Search"] != null)
                    {
                        this.txtPalabraaBuscar.Text = this.myQueryStrings["Search"].ToString().ToUpper(); 
                    }
                }
                this.btnActualizaGrids_Click(null, null);
                this.pnlSolicitudesyCreditos.Visible = this.pnlLiquidaciones.Visible = this.pnlMovimientosBanco.Visible = this.pnlMovimientosCaja.Visible = this.pnlNotasdeCompra.Visible = this.pnlNotasdeVenta.Visible = this.pnlProductores.Visible = false;
            }
        }

        protected void gridProductores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            HyperLink link;
            link = (HyperLink)e.Row.Cells[2].FindControl("HPProductor");
            if (link != null)
            {
                String sQuery = "idtomodify=" + this.gridProductores.DataKeys[e.Row.RowIndex]["productorID"].ToString();
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = "~/frmAddModifyProductores.aspx";
                strRedirect += sQuery;
                link.NavigateUrl= strRedirect;
            }
        }

        protected void gridMovBanco_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            HyperLink link;
            link = (HyperLink)e.Row.Cells[7].FindControl("HPMovBanco");
            if (link != null)
            {
                String sQuery = "movID=" + this.gridMovBanco.DataKeys[e.Row.RowIndex]["movbanID"].ToString();
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = "~/frmMovBancoAddQuick.aspx";
                strRedirect += sQuery;
                link.NavigateUrl = strRedirect;
            }
        }

        protected void gridCaja_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            HyperLink link;
            link = (HyperLink)e.Row.Cells[5].FindControl("HPMovCaja");
            if (link != null)
            {
                String sQuery = "idtomodify=" + this.gridCaja.DataKeys[e.Row.RowIndex]["movimientoID"].ToString();
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = "~/frmMovCajaChicaAddQuick.aspx";
                strRedirect += sQuery;
                link.NavigateUrl = strRedirect;
            }
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            this.txtPalabraaBuscar.Text = this.txtPalabraaBuscar.Text.ToUpper();
            if (this.cblColToShow.Items[0].Selected)
            {
                this.gridProductores.DataBind();
                this.pnlProductores.Visible = true;
            }
            else
            {
                this.pnlProductores.Visible = false;
            }
            if (this.cblColToShow.Items[1].Selected)
            {
                this.gridMovBanco.DataBind();
                this.pnlMovimientosBanco.Visible = true;
            }
            else
            {
                this.pnlMovimientosBanco.Visible = false;
            }
            if (this.cblColToShow.Items[2].Selected)
            {
                this.gridCaja.DataBind();
                this.pnlMovimientosCaja.Visible = true;
            }
            else
            {
                this.pnlMovimientosCaja.Visible = false;
            }
            if (this.cblColToShow.Items[3].Selected)
            {
                this.gridLiquidaciones.DataBind();
                this.pnlLiquidaciones.Visible = true;
            }
            else
            {
                this.pnlLiquidaciones.Visible = false;
            }
            if (this.cblColToShow.Items[4].Selected)
            {
                this.gridNotasdeVenta.DataBind();
                this.pnlNotasdeVenta.Visible = true;
            }
            else
            {
                this.pnlNotasdeVenta.Visible = false;
            }
            if (this.cblColToShow.Items[5].Selected)
            {

                this.gridNotasdeCompra.DataBind();
                this.pnlNotasdeCompra.Visible = true;
            }
            else
            {
                this.pnlNotasdeVenta.Visible = false;
            }
            if (this.cblColToShow.Items[6].Selected)
            {

                this.gridSolyCreditos.DataBind();
                this.pnlSolicitudesyCreditos.Visible = true;
            }
            else
            {
                this.pnlSolicitudesyCreditos.Visible = false;
            }
         
         
          
            
            
          
        }

        protected void gridLiquidaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            HyperLink link;
            link = (HyperLink)e.Row.Cells[3].FindControl("HPliquidacion");
            if (link != null)
            {
                String sQuery = "liqID=" + this.gridLiquidaciones.DataKeys[e.Row.RowIndex]["LiquidacionID"].ToString();
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = "~/frmLiquidacion2010.aspx";
                strRedirect += sQuery;
                link.NavigateUrl = strRedirect;
            }

        }

        protected void gridNotasdeVenta_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            HyperLink link;
            link = (HyperLink)e.Row.Cells[7].FindControl("HPNotaDeVenta");
            if (link != null)
            {
                String sQuery = "NotaVentaID=" + this.gridNotasdeVenta.DataKeys[e.Row.RowIndex]["notadeventaID"].ToString();
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = "~/frmNotadeVentaAddNew.aspx";
                strRedirect += sQuery;
                link.NavigateUrl = strRedirect;
            }

        }

        protected void gridNotasdeCompra_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            HyperLink link;
            link = (HyperLink)e.Row.Cells[6].FindControl("HPNotaDeCompra");
            if (link != null)
            {
                String sQuery = "NotaComID=" + this.gridNotasdeCompra.DataKeys[e.Row.RowIndex]["notadecompraID"].ToString();
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = "~/frmAddNotasCompras.aspx";
                strRedirect += sQuery;
                link.NavigateUrl = strRedirect;
            }


        }

        protected void gridSolyCreditos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            HyperLink link;
            link = (HyperLink)e.Row.Cells[5].FindControl("HPSolicitud");
            if (link != null)
            {
                String sQuery = "idtomodify=" + this.gridSolyCreditos.DataKeys[e.Row.RowIndex]["solicitudID"].ToString();
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = "~/frmAddSolicitud.aspx";
                strRedirect += sQuery;
                link.NavigateUrl = strRedirect;
            }
            link = (HyperLink)e.Row.Cells[6].FindControl("HPCredito");
            if (link != null)
            {
                String sQuery = "idtomodify=" + this.gridSolyCreditos.DataKeys[e.Row.RowIndex]["creditoID"].ToString();
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = "~/frmAddModifyCreditos.aspx";
                strRedirect += sQuery;
                link.NavigateUrl = strRedirect;
            }
            

            

        }

        protected void btnActualizaGrids_Click(object sender, EventArgs e)
        {
         
            foreach(ListItem elem in this.cblColToShow.Items)
            {
                elem.Selected = true;

            }
        }

        protected void btnQuiterSeleccion_Click(object sender, EventArgs e)
        {

            foreach (ListItem elem in this.cblColToShow.Items)
            {
                elem.Selected = false;

            }

        }
    }
}
