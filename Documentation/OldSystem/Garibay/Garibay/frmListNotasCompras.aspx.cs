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
using System.Globalization;

namespace Garibay
{
    public partial class WebForm9 : Garibay.BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                this.showHideColums();
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.NOTACOMPRA, Logger.typeUserActions.SELECT, int.Parse(Session["USERID"].ToString()), "VISITÓ LA PAGINA DE LISTA DE NOTAS DE COMPRA");
                this.panelMensaje.Visible = false;
                this.btnVerNota.Visible = false;
                this.btnEliminar.Visible = false;
            }
            if(this.panelMensaje.Visible)
            {
                this.panelMensaje.Visible = false;
            }
            this.grdvListEntPro.DataSourceID = "SqlDataSource1";
            this.compruebasecurityLevel();

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            this.Server.Transfer("~/frmAddNotasCompras.aspx");
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.btnAgregar.Visible = false;
                this.btnEliminar.Visible = false;
                this.btnVerNota.Visible = false;
                this.grdvListEntPro.Columns[0].Visible = false;
                
            }

        }

        
       
        protected void cmbProveedor_DataBound(object sender, EventArgs e)
        {
            int newValue = 0;
            this.cmbProveedor.Items.Insert(0, new ListItem("TODOS LOS PROVEEDORES", newValue.ToString()));
            this.cmbProveedor.SelectedIndex = 0;
            this.cmbProveedor.SelectedValue = newValue.ToString(); 
        }

       
        protected bool filtrar()
        {
            if (this.txtFecha1.Text == "" && this.txtFecha2.Text == "" && this.cmbProveedor.SelectedItem.Text == "TODOS LOS PROVEEDORES")
            {
                return false;
            }
            return true;
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            this.showHideColums();
            if (!this.filtrar())
            {
                this.grdvListEntPro.DataSourceID = "SqlDataSource1";
            }
            else
            {
                this.SqlDataSource1.FilterExpression = cadenfiltros();
                this.grdvListEntPro.DataSourceID = "SqlDataSource1";
            }
            

        }
        protected string cadenfiltros()
        {
            string filtros = "";
            bool masdeuno = false;
            if (this.cmbProveedor.SelectedItem.Text != "TODOS LOS PROVEEDORES")
            {
                filtros = " proveedorID = " + this.cmbProveedor.SelectedValue;
                masdeuno = true;

            }
            if (this.txtFecha1.Text != "" && this.txtFecha2.Text!= "")
            {
                if(masdeuno)
                {
                    filtros += " AND ";    
                }
                filtros += " fecha >= '";
                filtros += Utils.converttoLongDBFormat(this.txtFecha1.Text);
                filtros += "' AND fecha <= '";
                string fechafin;
                DateTime dt;
                dt = DateTime.Parse(this.txtFecha2.Text, new CultureInfo("es-Mx"));
                fechafin = dt.ToString("yyyy/MM/dd");
                fechafin += " 23:59:59";
                filtros += fechafin;
                filtros += "'";
                
            }
            return filtros;
        }
        protected void showHideColums()
        {
            foreach (DataControlField col in this.grdvListEntPro.Columns)
            {
                ListItem item = this.cblColToShow.Items.FindByText(col.HeaderText);
                if (item != null)
                {
                    col.Visible = item.Selected;
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string serror="";
            if(dbFunctions.deleteNotadeCompra(int.Parse(this.grdvListEntPro.SelectedDataKey["notadecompraID"].ToString()),ref serror))
            {
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.NOTACOMPRA, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ LA NOTA DE COMPRA: " + this.grdvListEntPro.SelectedDataKey["notadecompraID"].ToString().ToUpper()));
                this.lblMensajeException.Text = "";
                this.lblMensajeOperationresult.Text =" LA NOTA DE COMPRA CON EL FOLIO NÚMERO: " + this.grdvListEntPro.SelectedDataKey["folio"].ToString() + " HA SIDO ELIMINADA EXITOSAMENTE";
                this.lblMensajetitle.Text = "ÉXITO";
                this.imagenbien.Visible=true;
                this.imagenmal.Visible=false;
                this.panelMensaje.Visible=true;
            }
            else
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, this.UserID, "NO SE PUDO ELIMINAR LA NOTA DE COMPRA CON EL ID: " + this.grdvListEntPro.SelectedDataKey["notadecompraID"].ToString(),this.Request.Url.ToString());
                this.lblMensajeException.Text = serror;
                this.lblMensajeOperationresult.Text =" LA NOTA DE COMPRA CON EL FOLIO NÚMERO: " + this.grdvListEntPro.SelectedDataKey["folio"].ToString() + " NO HA PODIDO SER ELIMINADA";
                this.lblMensajetitle.Text = "FALLO";
                this.imagenbien.Visible=false;
                this.imagenmal.Visible=true;
                this.panelMensaje.Visible=true;

            }

        }

        protected void grdvListEntPro_SelectedIndexChanged(object sender, EventArgs e)
        {

            string mensaje, folio;
            folio = this.grdvListEntPro.SelectedDataKey["folio"].ToString().ToUpper();
            mensaje = "return confirm('¿Desea eliminar la nota de compra con el folio número: " + this.grdvListEntPro.SelectedDataKey["folio"].ToString().ToUpper() + "?. Tenga en cuenta que esto eliminará la entrada de producto relacionada con esta Nota de Compra. ";
            mensaje += "')";
            this.btnEliminar.Attributes.Add("onclick", mensaje);
            this.btnEliminar.Visible = true;
            this.btnVerNota.Visible = true;
            this.grdvListEntPro.DataSourceID="SqlDataSource1";
            
        }

        protected void btnVerNota_Click(object sender, EventArgs e)
        {
            if (this.grdvListEntPro.SelectedDataKey["notadecompraID"] != null)
            {
                string idtomodify;
                idtomodify = this.grdvListEntPro.SelectedDataKey["notadecompraID"].ToString();
                string strRedirect = "~/frmAddNotasCompras.aspx";
                string datosaencriptar;
                datosaencriptar = "NotaComID=";
                datosaencriptar += idtomodify;
                datosaencriptar += "&";
                strRedirect += "?data=";
                strRedirect += Utils.encriptacadena(datosaencriptar);
                Response.Redirect(strRedirect, true);
            }
            else
            {
                return;
            }
        }

        protected String GetURLToOpenNC(string id)
        {
            return "~/frmAddNotasCompras.aspx" + Utils.GetEncriptedQueryString("NotaComID=" + id);
        }
      
    }
}
