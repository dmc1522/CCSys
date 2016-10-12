using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmPagoANotasDeVenta : System.Web.UI.Page
    {
        protected void AddJSToControls()
        {
            JSUtils.AddDisableWhenClick(ref this.btnAddPago, this);

            String sOnchagemov = "checkValueInList(";
            sOnchagemov += "this" + ",'EFECTIVO','";
            sOnchagemov += this.divPagoMovCaja.ClientID + "');";
            this.cmbTipodeMovPago.Attributes.Add("onChange", sOnchagemov);

            sOnchagemov += "checkValueInList(";
            sOnchagemov += "this" + ",'MOVIMIENTO DE BANCO','";
            sOnchagemov += this.divMovBanco.ClientID + "');";
            this.cmbTipodeMovPago.Attributes.Add("onChange", sOnchagemov);

            sOnchagemov = "checkValueInList(";
            sOnchagemov += "this" + ",'CHEQUE','";
            sOnchagemov += this.divCheque.ClientID + "');";
            this.cmbConceptomovBancoPago.Attributes.Add("onChange", sOnchagemov);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.AddJSToControls();
                this.cmbTipodeMovPago.SelectedIndex = 0;
                this.cmbConceptomovBancoPago.DataBind();
                this.cmbConceptomovBancoPago.SelectedIndex = 0;
            }

            this.divPagoMovCaja.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divMovBanco.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "MOVIMIENTO DE BANCO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divCheque.Attributes.Add("style", this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
        }

        protected void btnAddPago_Click(object sender, EventArgs e)
        {



            
        }

        protected void drpdlGrupoCatalogosCajaChica_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlCatalogocuentaCajaChica.DataBind();
            this.drpdlSubcatalogoCajaChica.DataBind();
        }

        protected void drpdlCatalogocuentaCajaChica_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlSubcatalogoCajaChica.DataBind();
        }

        protected void drpdlGrupoCuentaFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlCatalogocuentafiscalPago.DataBind();
            this.drpdlSubcatalogofiscalPago.DataBind();
        }

        protected void drpdlCatalogocuentafiscalPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlSubcatalogofiscalPago.DataBind();
        }

        protected void drpdlGrupoCatalogosInternaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlCatalogoInternoPago.DataBind();
            this.drpdlSubcatologointernaPago.DataBind();
        }

        protected void drpdlCatalogoInternoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlSubcatologointernaPago.DataBind();
        }
    }
}
