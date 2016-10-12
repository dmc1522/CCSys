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
    public partial class frmFacturasDeCompra : BasePage
    {
        dsFacturaProveedor.FacturaDeProveedorDataTable dtFacturaDeProveedorData = new dsFacturaProveedor.FacturaDeProveedorDataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.panelmensaje.Visible = false;
            if(!this.IsPostBack)
            {
                JSUtils.AddDisableWhenClick(ref this.btnAddproduct, this);
                JSUtils.AddDisableWhenClick(ref this.btnAddFacturaCompra, this);
                JSUtils.AddDisableWhenClick(ref this.btnModifyFacturaCompra, this);
                this.ddlCiclos.DataBind();
                this.ddlGrupos.DataBind();
                this.ddlProveedor.DataBind();
                this.drpdlBodega.DataBind();
                this.drpdlProducto.DataBind();
                this.panelmensaje.Visible = false;
                this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.pnlAddProd.Visible = false;
                this.btnAddFacturaCompra.Visible = true;
                
                if (Request.QueryString["data"] != null && this.loadqueryStrings(Request.QueryString["data"].ToString()) && this.myQueryStrings != null && this.myQueryStrings["FacturaDeCompraID"] != null)
                {
                    this.lblFacturaId.Text = this.myQueryStrings["FacturaDeCompraID"].ToString();
                    this.LoadFacturaData();
                    
                }
            }
        }

      
        private void LoadFacturaData()
        {
            try
            {
                FacturaDeProveedor fac = FacturaDeProveedor.Get(int.Parse(this.lblFacturaId.Text));
                if (fac == null)
                {
                    this.Response.Redirect("/frmFacturasDeCompra.aspx");
                }
                this.ddlProveedor.DataBind();
                this.ddlProveedor.SelectedValue = fac.ProveedorID.ToString();
                this.ddlCiclos.DataBind();
                this.ddlCiclos.SelectedValue = fac.CicloId.ToString();
                this.txtFolio.Text = fac.NumFactura.ToString();
                this.txtFecha.Text = fac.Fecha.Value.ToString("dd/MM/yyyy");
                this.TextBoxIva.Text = string.Format("{0:c2}", fac.Iva);
                this.TextBoxDescuento.Text = string.Format("{0:c2}", fac.Descuento);
                this.ddlMoneda.DataBind();
                this.ddlMoneda.SelectedValue = fac.Tipomonedaid.ToString();
                this.pnlAddProd.Visible = true;
                this.pnlCentral.Visible = true;
                this.btnModifyFacturaCompra.Visible = true;
                this.btnAddFacturaCompra.Visible = false;
                this.txtObservaciones.Text = fac.Observaciones;
            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "ERROR AL CARGAR FACTURA DE COMPRA", ref ex);
                this.panelmensaje.Visible = true;
                this.lblMensajeException.Text = "Descripción: " + ex.Message;
                this.lblMensajetitle.Text = "ERROR";
                this.lblMensajeOperationresult.Text = "NO SE PUDO CARGAR LA FACTURA DE COMPRA.";
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
            }  
        }

        protected void btnAddOrdenEntrada_Click(object sender, EventArgs e)
        {
            string sQuery = string.Empty;
            try
            {
                FacturaDeProveedor fac = new FacturaDeProveedor();
                fac.ProveedorID = int.Parse(this.ddlProveedor.SelectedValue);
                fac.NumFactura = this.txtFolio.Text;
                fac.Fecha = DateTime.Parse(this.txtFecha.Text);
                fac.Iva = 0;
                fac.Descuento = 0.0f;
                fac.CicloId = int.Parse(this.ddlCiclos.SelectedValue);
                fac.Tipomonedaid = int.Parse(this.ddlMoneda.SelectedValue.ToString());
                fac.Observaciones = this.txtObservaciones.Text;
                fac.Insert();
                sQuery = "FacturaDeCompraID=" + fac.Facturaid.ToString();
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                sQuery = "~/frmFacturasDeCompra.aspx" + sQuery;
            }
            catch (System.Exception ex)
            {
                this.panelmensaje.Visible = true;
                this.lblMensajeException.Text = "Descripción: " + ex.Message;
                this.lblMensajetitle.Text = "ERROR";
                this.lblMensajeOperationresult.Text = "NO SE PUDO AGREGAR LA FACTURA DE COMPRA.";
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "SE INSERTÓ UNA FACTURA DE COMPRA", ref ex);
            }
            if (!string.IsNullOrEmpty(sQuery))
            {
                Response.Redirect(sQuery);
            }
        }

        protected void btnAddproduct_Click(object sender, EventArgs e)
        {
            try
            {
                FacturaDeProveedorDetalle detalle = new FacturaDeProveedorDetalle();
                detalle.Cantidad = Utils.GetSafeFloat(this.txtCantidad.Text);
                detalle.Facturaid = int.Parse(this.lblFacturaId.Text);
                detalle.Precio = Utils.GetSafeFloat(this.txtPrecio.Text);
                detalle.ProductoID = int.Parse(this.drpdlProducto.SelectedValue.ToString());
                detalle.BodegaID = int.Parse(this.drpdlBodega.SelectedValue.ToString());
                detalle.Insert();
                this.grdvProductosRecibidos.DataBind();
            }
            catch (System.Exception ex)
            {
                this.panelmensaje.Visible = true;
                this.lblMensajeException.Text = "Descripción: " + ex.Message;
                this.lblMensajetitle.Text = "ERROR";
                this.lblMensajeOperationresult.Text = "NO SE PUDO AGREGAR EL DETALLE A LA FACTURA DE COMPRA.";
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "DETALLE A FACTURA DE COMPRA", ref ex);
            }
        }

        protected void grdvProductosRecibidos_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                this.panelmensaje.Visible = true;
                this.lblMensajeException.Text = "Descripción: " + e.Exception.Message;
                this.lblMensajetitle.Text = "ERROR";
                this.lblMensajeOperationresult.Text = "NO SE PUDO UPDATE EL DETALLE A LA FACTURA DE COMPRA.";
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "SE MODIFICO UNA FACTURA DE COMPRA", e.Exception);
                e.ExceptionHandled = true;
            }
        }

        protected void grdvProductosRecibidos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            e.NewValues.Add("facturaid", int.Parse(this.lblFacturaId.Text));
        }

        protected void btnModifyFacturaCompra_Click(object sender, EventArgs e)
        {
            try
            {
                FacturaDeProveedor fac = FacturaDeProveedor.Get(int.Parse(this.lblFacturaId.Text));
                fac.ProveedorID = int.Parse(this.ddlProveedor.SelectedValue);
                fac.NumFactura = this.txtFolio.Text;
                fac.Fecha = DateTime.Parse(this.txtFecha.Text);
                fac.Iva = 0;
                fac.Descuento = 0.0f;
                fac.CicloId = int.Parse(this.ddlCiclos.SelectedValue);
                fac.Tipomonedaid = int.Parse(this.ddlMoneda.SelectedValue.ToString());
                fac.Observaciones = this.txtObservaciones.Text;
                fac.Update();
                this.panelmensaje.Visible = true;
                this.lblMensajeException.Text = string.Empty;
                this.lblMensajetitle.Text = "Factura Guardada";
                this.lblMensajeOperationresult.Text = "LA FACTURA DE COMPRA FUE GUARDADA";
                this.imagenbien.Visible = true;
                this.imagenmal.Visible = false;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PROVEEDORES, Logger.typeUserActions.UPDATE, "ACTUALIZO LA FACTURA DE COMPRA: " + this.lblFacturaId.Text);
            }
            catch (System.Exception ex)
            {
                this.panelmensaje.Visible = true;
                this.lblMensajeException.Text = "Descripción: " + ex.Message;
                this.lblMensajetitle.Text = "ERROR";
                this.lblMensajeOperationresult.Text = "NO SE PUDO GUARDAR LA FACTURA DE COMPRA.";
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "SE GUARDAR LA FACTURA DE COMPRA", ref ex);
            }
        }       
    }
}
