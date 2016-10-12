using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmAddModifyAnticipo : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                this.cmbCiclo.DataBind();
                this.cmbProductor.DataBind();
                this.cmbTipodeMov.DataBind();
                this.cmbConceptomovBanco.DataBind();
               // this.ddlConcepto.DataBind();
                this.cmbTipodeMov.DataBind();
                this.drpdlCatalogoInterno.DataBind();
                this.drpdlGrupoCatalogos.DataBind();
                this.drpdlSubcatologointerna.DataBind();
                
                this.cmbTipoAnticipo.DataBind();
                this.cmbCuenta.DataBind();
                this.panelMensaje.Visible = false;
            }
            if(this.panelMensaje.Visible)
            {
                this.panelMensaje.Visible = false;
                this.panelagregar.Visible = true;
            }
            this.divCheque.Attributes.Add("style", this.cmbConceptomovBanco.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            String sOnchange = "checkValueInList(";
            sOnchange += "this" + ",'CHEQUE','";
            sOnchange += this.divCheque.ClientID + "')";
            this.cmbConceptomovBanco.Attributes.Add("onChange", sOnchange);

            this.divMovBanco.Attributes.Add("style", this.cmbTipodeMov.SelectedItem.Text == "MOVIMIENTO DE BANCO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            String sOnchagemov = "checkValueInList(";
            sOnchagemov += "this" + ",'MOVIMIENTO DE BANCO','";
            sOnchagemov += this.divMovBanco.ClientID + "')";
            this.cmbTipodeMov.Attributes.Add("onChange", sOnchagemov);

            this.divPrestamo.Attributes.Add("style", this.cmbTipoAnticipo.SelectedItem.Text == "PRESTAMO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            String sOnchagetipomov = "checkValueInList(";
            sOnchagetipomov += "this" + ",'PRESTAMO','";
            sOnchagetipomov += this.divPrestamo.ClientID + "')";
            this.cmbTipoAnticipo.Attributes.Add("onChange", sOnchagetipomov);

            this.gridBoletas.DataBind();
            this.gridAnticipos.DataBind();
            compruebasecurityLevel();


           

        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string Error="";
            float interesmoratorio = 0, interesanual = 0;
            if (this.txtInteresMoratorio.Text != "") { interesmoratorio = float.Parse(this.txtInteresMoratorio.Text); }
            if (this.txtInteresAnual.Text != "") { interesanual = float.Parse(this.txtInteresAnual.Text); }
            int catalogointernoID = -1, catalogofiscalID = -1, subcatalogointernoID = -1, subcatalogofiscalID = -1;
            if (this.cmbTipodeMov.SelectedItem.Text == "MOVIMIENTO DE BANCO")
            {
                catalogofiscalID=int.Parse(this.drpdlCatalogocuentafiscal.SelectedValue);
                if (this.drpdlSubcatalogofiscal.SelectedIndex > -1)
                    subcatalogofiscalID = int.Parse(this.drpdlSubcatalogofiscal.SelectedValue);

                if (this.cmbConceptomovBanco.SelectedItem.Text != "CHEQUE")
                {
                    catalogointernoID = int.Parse(this.drpdlCatalogocuentafiscal.SelectedValue);
                    if (this.drpdlSubcatalogofiscal.SelectedIndex > -1)
                        subcatalogointernoID = int.Parse(this.drpdlSubcatalogofiscal.SelectedValue);
                }
                else
                {
                   catalogointernoID = int.Parse(this.drpdlCatalogoInterno.SelectedValue);
                    if (this.drpdlSubcatologointerna.SelectedIndex > -1)
                        subcatalogointernoID = int.Parse(this.drpdlSubcatologointerna.SelectedValue);
                }
            }
            
            /*if (dbFunctions.insertAnticipo(int.Parse(this.cmbCiclo.SelectedValue),
                int.Parse(this.cmbProductor.SelectedValue),
                this.UserID,
                ref

                int.Parse(this.cmbCuenta.SelectedValue),
                ,
                , int.Parse(this.Session["userID"].ToString()), Utils.converttoLongDBFormat(this.txtFecha.Text), this.txtNombre.Text, float.Parse(this.txtMonto.Text), this.cmbTipodeMov.SelectedIndex, int.Parse(this.cmbConceptomovBanco.SelectedValue), txtChequeNum.Text, this.txtChequeNombre.Text, ref Error, interesanual,interesmoratorio, Utils.converttoLongDBFormat(this.txtFechaLimiteDePAgo.Text),catalogointernoID,subcatalogointernoID,catalogofiscalID,subcatalogofiscalID))
            {

                this.lblMensajeException.Text = "";
                this.lblMensajetitle.Text = "EXITO";
                this.imagenbien.Visible = true;
                this.imagenmal.Visible = false;
                this.lblMensajeOperationresult.Text = "EL ANTICIPO HA SIDO AGREGADO EXITOSAMENTE";
                this.panelagregar.Visible = false;
                this.limpiacampos();

            }
            else
            {
            
                this.lblMensajeException.Text = Error;
                this.lblMensajetitle.Text = "FALLO";
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.lblMensajeOperationresult.Text = "EL ANTICIPO NO HA PODIDO SER AGREGADO";
                this.panelagregar.Visible = false;

            }*/
            this.panelagregar.Visible = false;
            this.panelMensaje.Visible = true;

        }
        protected void limpiacampos(){
            this.txtChequeNombre.Text = "";
            this.txtMonto.Text = "";
            this.txtNombre.Text = "";
            this.txtChequeNum.Text = "";
        }

        protected void txtidToModify_TextChanged(object sender, EventArgs e)
        {

        }

        protected void cmbTipodeMov_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.cmbTipodeMov.SelectedItem.Text == "MOVIMIENTO DE BANCO"){
                this.lblNombre.Text = "Nombre fiscal: ";
            }
            else{
                this.lblNombre.Text = "Nombre: ";
            }
        }

        protected void drpdlGrupoCuentaFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.drpdlCatalogocuentafiscal.DataBind();
            this.drpdlSubcatalogofiscal.DataBind();
        }

        protected void drpdlGrupoCatalogos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlCatalogoInterno.DataBind();
            this.drpdlCatalogoInterno.DataBind();
        }
    }
}
