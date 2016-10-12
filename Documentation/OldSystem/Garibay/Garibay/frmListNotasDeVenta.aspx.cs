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
    public partial class frmListNotasDeVenta : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(this.IsSistemBanco)
            {
                this.sdsNotasVenta.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.sdsNotasVenta.SelectCommand);
                this.SqlCredito.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.SqlCredito.SelectCommand);
            }

            if (!this.IsPostBack)
            {
                this.panelMensaje.Visible = false;
                
                this.btnEliminar.Visible = false;
                
                this.ddlCiclos.DataBind();
                this.cmbCliente.DataBind();
                this.cmbCredito.DataBind();
                this.cmbTipoPago.DataBind();

                //ddlCiclos.SelectedValue = "0";
                this.cmbCliente.SelectedValue = "0";
                this.cmbCredito.SelectedValue = "0";
                this.cmbTipoPago.SelectedValue = "0";
                this.ddlCiclos.SelectedValue = "0";
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.NOTAVENTA, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "SE VISITÓ LA LISTA DE LAS NOTAS DE VENTAS");

            }
            if (this.panelMensaje.Visible)
            {
                this.panelMensaje.Visible = false;
            }
            this.gridNotasVenta.DataSourceID = "sdsNotasVenta";
            this.cmbCliente.DataSourceID ="SqlProductores";
            this.cmbCredito.DataSourceID = "SqlCredito";
            this.compruebasecurityLevel();
            
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.btnAgregarNota.Visible = false;
            }

        }
        protected void PopCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            this.txtFechaDe.Text = PopCalendar1.SelectedDate;
        }

        protected void PopCalendar2_SelectionChanged(object sender, EventArgs e)
        {
            this.txtFechaA.Text = PopCalendar2.SelectedDate;
        }

        protected void btnAgregarNota_Click(object sender, EventArgs e)
        {
            this.Server.Transfer("~/frmNotadeVentaAddNew.aspx");
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {



            if (filtrar())
            {
                sdsNotasVenta.FilterExpression = cadenaFiltros();
             
            }
            this.gridNotasVenta.DataSourceID = "sdsNotasVenta";
            this.gridNotasVenta.PageIndex = 0;
        }


        protected bool filtrar()
        {
            if (this.txtFechaA.Text == "" && this.txtFechaDe.Text == "" && this.cmbTipoPago.SelectedValue == "0" && this.cmbCredito.SelectedValue == "0" && this.cmbCliente.SelectedValue == "0"&&this.txtFolio.Text=="")
            {
                return false;
            }
            return true;
        }


        protected string cadenaFiltros()
        {
            string filtros = "";
            bool masdeuno = false;
            if (this.cmbCliente.SelectedValue!= "0")
            {
                filtros += "productorID = " + this.cmbCliente.SelectedValue;
                masdeuno = true;

            }
            
            if (this.cmbTipoPago.SelectedValue != "0")
            {

                if (this.cmbTipoPago.SelectedValue == "2")
                {

                    if (this.cmbCredito.SelectedValue != "0")
                    {
                        if (masdeuno)
                        {
                            filtros += " AND ";
                        }
                        filtros += "creditoID = " + this.cmbCredito.SelectedValue;
                        masdeuno = true;

                
                    }
                    else
                    {

                        if (masdeuno)
                        {
                            filtros += " AND ";
                        }
                        filtros += "acredito = TRUE";
                        masdeuno = true;
                    }

                    
                }
                else
                {

                    if (masdeuno)
                    {
                        filtros += " AND ";
                    }
                    filtros += "acredito = FALSE";
                    masdeuno = true;
                }

            }
            if (this.txtFechaDe.Text != "")
            {
                if (masdeuno)
                {
                    filtros += " AND ";
                }

                filtros += "Fecha >='" + Utils.converttoLongDBFormat(txtFechaDe.Text) + "'";

                masdeuno = true;


            }
            if (this.txtFechaA.Text != "")
            {
                if (masdeuno)
                {
                    filtros += " AND ";
                }
                filtros += "Fecha <='" + Utils.converttoLongDBFormat(this.txtFechaA.Text) + "'";


            }
            if (this.txtFolio.Text != "")
            {
                if (masdeuno)
                {
                    filtros += " AND ";
                }
                filtros += "Folio =" + this.txtFolio.Text ;


            }
            return filtros;
        }


        protected String GetURLToOpenNV(string id)
        {
            return "~/frmNotadeVentaAddNew.aspx" + Utils.GetEncriptedQueryString("NotaVentaID=" + id);
        }
        protected String GetURLToOpenNVNueva(string id)
        {
            return "~/frmNotadeVentaAddNew.aspx" + Utils.GetEncriptedQueryString("NotaVentaID=" + id);
        }
        

        //protected void gridNotasVenta_SelectedIndexChanged(object sender, EventArgs e)
        //{
          
            
        //}

        protected void gridNotasVenta_SelectedIndexChanged1(object sender, EventArgs e)
        {
          string mensaje, folio;
            folio = this.gridNotasVenta.SelectedDataKey["notadeventaID"].ToString().ToUpper();
            mensaje = "return confirm('¿Desea eliminar la nota de venta con el ID número: " + folio + "?. Tenga en cuenta que esto eliminará la salida de producto relacionada con esta Nota de Venta. ";
            mensaje += "')";
            this.btnEliminar.Attributes.Add("onclick", mensaje);
            this.btnEliminar.Visible = true;
            //this.btnVisible = true;
            this.gridNotasVenta.DataSourceID = "sdsNotasVenta";//SqlDataSource1";

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string serror = "";
            if (dbFunctions.deleteNotadeVenta(int.Parse(this.gridNotasVenta.SelectedDataKey["notadeventaID"].ToString()), ref serror))
            {
                this.lblMensajeException.Text = "";
                this.lblMensajeOperationresult.Text = " LA NOTA DE VENTA CON EL ID NÚMERO: " + this.gridNotasVenta.SelectedDataKey["notadeventaID"].ToString() + " HA SIDO ELIMINADA EXITOSAMENTE";
                this.lblMensajetitle.Text = "ÉXITO";
                this.imagenbien.Visible = true;
                this.imagenmal.Visible = false;
                this.panelMensaje.Visible = true;
            }
            else
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, this.UserID, "NO SE PUDO ELIMINAR LA NOTA DE VENTA CON EL ID: " + this.gridNotasVenta.SelectedDataKey["notadeventaID"].ToString(), this.Request.Url.ToString());
                this.lblMensajeException.Text = serror;
                this.lblMensajeOperationresult.Text = " LA NOTA DE VENTA CON EL ID NÚMERO: " + this.gridNotasVenta.SelectedDataKey["notadeventaID"].ToString() + " NO HA PODIDO SER ELIMINADA";
                this.lblMensajetitle.Text = "FALLO";
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.panelMensaje.Visible = true;

            }

        }

        protected void btnEliminarFiltros_Click(object sender, EventArgs e)
        {

            this.txtFechaA.Text = ""; 
            this.txtFechaDe.Text = ""; 
            this.cmbTipoPago.SelectedValue = "0";
            this.cmbCredito.SelectedValue = "0";
            this.cmbCliente.SelectedValue = "0";
            this.ddlCiclos.SelectedValue = "0";
            this.txtFolio.Text = "";

        }

        protected void ddlCiclos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.ddlCiclos.SelectedValue!="0"){
                this.SqlProductores.FilterExpression="cicloID=" +this.ddlCiclos.SelectedValue+" OR cicloID=0";

                this.SqlCredito.FilterExpression = "cicloID=" + this.ddlCiclos.SelectedValue + " OR cicloID=0";
                this.sdsNotasVenta.FilterExpression = "cicloID=" + this.ddlCiclos.SelectedValue;
            }
            else{
                this.SqlProductores.FilterExpression = null;
                this.SqlCredito.FilterExpression = null;
                this.sdsNotasVenta.FilterExpression = null;
            }
          //  this.SqlProductores.DataBind();
            
        }
        protected String GetURLPrintNota(string id)
        {
            return "~/frmPrintNotaVenta.aspx" + Utils.GetEncriptedQueryString("notadeventaID=" + id);
        }
    }
}
