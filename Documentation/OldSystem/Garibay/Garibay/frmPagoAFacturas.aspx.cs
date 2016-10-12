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
    public partial class frmPagoAFacturas : BasePage
    {
        private void AddJSToControls()
        {
            JSUtils.AddDisableWhenClick(ref this.btnAddFacturaAPago, this);
            JSUtils.AddDisableWhenClick(ref this.btnAddPago, this);
            JSUtils.AddDisableWhenClick(ref this.btnRemoveFacturaPago, this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.pnlNewPago.Visible = false;
            if (!this.IsPostBack)
            {
                this.drpdlGrupoCuentaFiscal.DataBind();
                this.drpdlGrupoCuentaFiscal.SelectedValue = "2";
                this.drpdlCatalogocuentafiscalPago.DataBind();
                this.drpdlCatalogocuentafiscalPago.SelectedValue = "13";


                this.AddJSToControls();
                this.txtFechaNPago.Text = Utils.Now.ToString("dd/MM/yyyy");
                if (this.LoadEncryptedQueryString()> 0 && this.myQueryStrings["movID"] != null)
                {
                    this.txtMovID.Text = this.myQueryStrings["movID"].ToString();
                    this.btnAddPago.Visible = false;
                    this.ddlCiclo.DataBind();
                    this.ddlClientesVenta.DataBind();
                    this.ddlClientesVenta.SelectedIndex = 0;
                    this.GridViewFacturasEnPago.DataBind();
                    this.GridViewFacturasDisponibles.DataBind();
                    this.PanelFacturasToMov.Visible = true;
                    this.LoadMovData();
                }
            }
            this.dvSaldo.DataBind();
        }

        private void LoadMovData()
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.CommandText = " SELECT     MovimientosCuentasBanco.movbanID, MovimientosCuentasBanco.cuentaID, MovimientosCuentasBanco.ConceptoMovCuentaID, MovimientosCuentasBanco.fecha, "
                    + " MovimientosCuentasBanco.abono, MovimientosCuentasBanco.nombre, MovimientosCuentasBanco.Observaciones, "
                    + " MovimientosCuentasBanco.catalogoMovBancoFiscalID, MovimientosCuentasBanco.subCatalogoMovBancoFiscalID, "
                    + " catalogoMovimientosBancos.grupoCatalogoID "
                    + " FROM         MovimientosCuentasBanco INNER JOIN "
                    +"  catalogoMovimientosBancos ON MovimientosCuentasBanco.catalogoMovBancoInternoID = catalogoMovimientosBancos.catalogoMovBancoID AND "
                    + " MovimientosCuentasBanco.catalogoMovBancoFiscalID = catalogoMovimientosBancos.catalogoMovBancoID "
                    + " WHERE     (MovimientosCuentasBanco.movbanID = @movbanID)";
                conn.Open();
                comm.Connection = conn;
                comm.Parameters.Add("@movbanID", SqlDbType.Int).Value = this.txtMovID.Text;
                SqlDataReader r = comm.ExecuteReader();
                if (r.HasRows && r.Read())
                {
                    this.txtFechaNPago.Text = DateTime.Parse(r["fecha"].ToString()).ToString("dd/MM/yyyy");
                    this.txtNombrePago.Text = r["nombre"].ToString();
                    this.txtMonto.Text = string.Format("{0:C2}", double.Parse(r["abono"].ToString()));
                    this.cmbConceptomovBancoPago.DataBind();
                    this.cmbConceptomovBancoPago.SelectedValue = r["ConceptoMovCuentaID"].ToString();
                    this.cmbCuentaPago.DataBind();
                    this.cmbCuentaPago.SelectedValue = r["cuentaID"].ToString();
                    this.drpdlGrupoCuentaFiscal.DataBind();
                    this.drpdlGrupoCuentaFiscal.SelectedValue = r["grupoCatalogoID"].ToString();
                    this.drpdlCatalogocuentafiscalPago.DataBind();
                    this.drpdlCatalogocuentafiscalPago.SelectedValue = r["catalogoMovBancoFiscalID"].ToString();
                    this.drpdlSubcatalogofiscalPago.DataBind();
                    if (r["subCatalogoMovBancoFiscalID"] != null && ((int)r["subCatalogoMovBancoFiscalID"]) > -1)
                    {
                        this.drpdlSubcatalogofiscalPago.SelectedValue = r["subCatalogoMovBancoFiscalID"].ToString();
                    }
                    this.txtObservaciones.Text = r["Observaciones"].ToString();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "cargando datos de pagos a factura", ref ex);
            }
        }

        protected void btnAddPago_Click(object sender, EventArgs e)
        {
            double monto = 0;
            double.TryParse(this.txtMonto.Text, out monto);
            if (monto == 0)
            {

                this.pnlNewPago.Visible = true;
                this.imgMalPago.Visible = true;
                this.imgBienPago.Visible = false;
                this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, ERROR EN MONTO, ESCRIBA CANTIDAD VÁLIDA";
                return;
            }

            if (dbFunctions.FechaEnPeriodoBloqueado(DateTime.Parse(this.txtFechaNPago.Text), int.Parse(this.cmbCuentaPago.SelectedValue)))
            {
                this.pnlNewPago.Visible = true;
                this.imgBienPago.Visible = false;
                this.imgMalPago.Visible = true;
                this.lblNewPagoResult.Text = "EL MOVIMIENTO NO PUEDE SER AGREGADO YA QUE LA FECHA ESTA DENTRO DE UN PERIODO BLOQUEADO<BR />DESBLOQUEE EL PERIODO PARA PERMITIR INGRESAR EL MOVIMIENTO";
                return;
            }

            //this.cmbTipodeMov.DataBind();
            dsMovBanco.dtMovBancoDataTable tablaaux = new dsMovBanco.dtMovBancoDataTable();
            dsMovBanco.dtMovBancoRow dtRowainsertar = tablaaux.NewdtMovBancoRow();
            dtRowainsertar.chequecobrado = false;
            dtRowainsertar.conceptoID = int.Parse(this.cmbConceptomovBancoPago.SelectedValue);
            dtRowainsertar.nombre = this.txtNombrePago.Text;
            dtRowainsertar.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaNPago.Text));
            //dats de cheque
            dtRowainsertar.numCheque = 0;
            dtRowainsertar.chequeNombre = string.Empty;
            dtRowainsertar.facturaOlarguillo = string.Empty;
            dtRowainsertar.numCabezas = 0;//this.txtNumCabezas.Text.Length > 0 ? double.Parse(this.txtNumCabezas.Text) : 0;

            dtRowainsertar.catalogoMovBancoFiscalID = int.Parse(this.drpdlCatalogocuentafiscalPago.SelectedValue);
            if (this.drpdlSubcatalogofiscalPago.SelectedIndex > -1)
                dtRowainsertar.subCatalogoMovBancoFiscalID = int.Parse(this.drpdlSubcatalogofiscalPago.SelectedValue);

            dtRowainsertar.catalogoMovBancoInternoID = dtRowainsertar.catalogoMovBancoFiscalID;
            dtRowainsertar.subCatalogoMovBancoInternoID = dtRowainsertar.subCatalogoMovBancoFiscalID;

            dtRowainsertar.cargo = 0.00;
            dtRowainsertar.abono = this.txtMonto.Text.Length > 0 ? double.Parse(this.txtMonto.Text) : 0;
            dtRowainsertar.storeTS = DateTime.Parse(Utils.getNowFormattedDate());
            dtRowainsertar.updateTS = DateTime.Parse(Utils.getNowFormattedDate());

            String serror = "", tipo = "";

            dtRowainsertar.cuentaID = int.Parse(this.cmbCuentaPago.SelectedValue);

            dtRowainsertar.creditoFinancieroID = -1;

            bool bMovimientoAdded = false;
            ListBox a = new ListBox();
            if (dbFunctions.insertaMovBanco(ref dtRowainsertar, ref serror, int.Parse(this.Session["USERID"].ToString()), int.Parse(this.cmbCuentaPago.SelectedValue), int.Parse(this.ddlCiclo.SelectedValue), -1, "", this.txtObservaciones.Text))
            {
                this.txtMovID.Text = dtRowainsertar.movBanID.ToString();
                this.GridViewFacturasDisponibles.DataBind();
                this.GridViewFacturasEnPago.DataBind();
                this.ddlClientesVenta.DataBind();
                this.PanelFacturasToMov.Visible = true;
                this.pnlNewPago.Visible = true;
                this.imgBienPago.Visible = true;
                this.imgMalPago.Visible = false;
                this.lblNewPagoResult.Text = "SE AGREGO EL PAGO CORRECTAMENTE";
                bMovimientoAdded = true;
            }
            else
            {
                this.pnlNewPago.Visible = true;
                this.imgBienPago.Visible = false;
                this.imgMalPago.Visible = true;
                this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO";
            }
            if (bMovimientoAdded)
            {
                string sRedirect = "~/frmPagoAFacturas.aspx";
                sRedirect += Utils.GetEncriptedQueryString("movID=" + this.txtMovID.Text);
                Response.Redirect(sRedirect);
            }
            this.dvSaldo.DataBind();
        }

        protected void acturaaPago_Click(object sender, EventArgs e)
        {
            SqlConnection connFactura = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                connFactura.Open();
                SqlCommand commFactura = new SqlCommand();
                commFactura.Connection = connFactura;
                commFactura.CommandText = "INSERT INTO PagosFacturasClientesVenta (FacturaCVID, fecha, movbanID, userID) VALUES (@FacturaCVID,@fecha,@movbanID,@userID); update movimientoscuentasbanco set facturaolarguillo = dbo.udf_update_concat(movbanID) where movbanid in (SELECT     movbanID FROM         PagosFacturasClientesVenta WHERE     (movbanID IS NOT NULL));";
                //(@FacturaCVID,@fecha,@movbanID,@movCajaID,@userID)
                //commFactura.Parameters.Add("@FacturaCVID", SqlDbType.Int).Value = int.Parse(this.txtFacturaIDToMod.Text);
                commFactura.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Parse(this.txtFechaNPago.Text);
                commFactura.Parameters.Add("@movbanID", SqlDbType.Int).Value = this.txtMovID.Text;
                commFactura.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                for(int i = 0; i < this.GridViewFacturasDisponibles.Rows.Count; i++)
                {
                    GridViewRow row = this.GridViewFacturasDisponibles.Rows[i];
                    CheckBox cb = (CheckBox)row.FindControl("chkFacturaID");
                    if (cb != null && cb.Checked)
                    {
                        SqlParameter sqlParam = commFactura.Parameters.Add("@FacturaCVID", SqlDbType.Int);
                        sqlParam.Value = this.GridViewFacturasDisponibles.DataKeys[i]["FacturaCV"].ToString();
                        if (commFactura.ExecuteNonQuery() <1)
                        {
                            throw new Exception("This must almost never happen");
                        }
                        commFactura.Parameters.Remove(sqlParam);
                    }
                }
                this.GridViewFacturasDisponibles.DataBind();
                this.GridViewFacturasEnPago.DataBind();
                this.pnlNewPago.Visible = true;
                this.imgBienPago.Visible = true;
                this.imgMalPago.Visible = false;
                this.lblNewPagoResult.Text = "LAS FACTURAS HAN SIDO RELACIONADAS AL PAGO EXITOSAMENTE";
                this.dvSaldo.DataBind();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error adding new movbanco->factura", ref ex);
                this.pnlNewPago.Visible = true;
                this.imgBienPago.Visible = false;
                this.imgMalPago.Visible = true;
                this.lblNewPagoResult.Text = "LAS FACTURAS NO HAN PODIDO SER RELACIONADAS AL PAGO, EL ERROR ES: <BR />" + ex.Message;
            }
            finally
            {
                connFactura.Close();
            }
        }

        protected void ddlClientesVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GridViewFacturasDisponibles.DataBind();
            this.GridViewFacturasEnPago.DataBind();
        }

        protected void btnRemoveFacturaPago_Click(object sender, EventArgs e)
        {
            SqlConnection connFactura = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                connFactura.Open();
                SqlCommand commFactura = new SqlCommand();
                commFactura.Connection = connFactura;
                commFactura.CommandText = "DISABLE TRIGGER TGR_DELETE_RELATIONS_PAGOS_FACTURASCLIENTESVENTA on PagosFacturasClientesVenta; DELETE FROM PagosFacturasClientesVenta WHERE     (pagoFVID = @pagoFVID); ENABLE TRIGGER TGR_DELETE_RELATIONS_PAGOS_FACTURASCLIENTESVENTA on PagosFacturasClientesVenta";
                for (int i = 0; i < this.GridViewFacturasEnPago.Rows.Count; i++)
                {
                    GridViewRow row = this.GridViewFacturasEnPago.Rows[i];
                    CheckBox cb = (CheckBox)row.FindControl("chkFacturaID");
                    if (cb != null && cb.Checked)
                    {
                        SqlParameter sqlParam = commFactura.Parameters.Add("@pagoFVID", SqlDbType.Int);
                        sqlParam.Value = this.GridViewFacturasEnPago.DataKeys[i]["pagoFVID"].ToString();
                        if (commFactura.ExecuteNonQuery() != 1)
                        {
                            throw new Exception("This must almost never happen");
                        }
                        commFactura.Parameters.Remove(sqlParam);
                    }
                }
                this.GridViewFacturasDisponibles.DataBind();
                this.GridViewFacturasEnPago.DataBind();
                this.pnlNewPago.Visible = true;
                this.imgBienPago.Visible = true;
                this.imgMalPago.Visible = false;
                this.lblNewPagoResult.Text = "SE QUITO LA RELACION DE LAS FACTURAS CON EL PAGO EXITOSAMENTE";
                this.dvSaldo.DataBind();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error adding new movbanco->factura", ref ex);
                this.pnlNewPago.Visible = true;
                this.imgBienPago.Visible = false;
                this.imgMalPago.Visible = true;
                this.lblNewPagoResult.Text = "LAS FACTURAS NO HAN PODIDO SER QUITADAS DE LA RELACION AL PAGO, EL ERROR ES: <BR />" + ex.Message;
            }
            finally
            {
                connFactura.Close();
            }
        }
    }
}
