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
    public partial class frmPagosAFacturasDeGanado : BasePage
    {
        private void AddJSToControls()
        {
            JSUtils.AddDisableWhenClick(ref this.btnAddPago, this);
            //JSUtils.AddDisableWhenClick(ref this.btnAddPago, this);
            //JSUtils.AddDisableWhenClick(ref this.btnRemoveFacturaPago, this);

            String sOnchagemov = "checkValueInList(";
            sOnchagemov += "this" + ",'CHEQUE','";
            sOnchagemov += this.divCheque.ClientID + "');";
            this.cmbConceptomovBancoPago.Attributes.Add("onChange", sOnchagemov);

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                this.AddJSToControls();
                this.txtFechaPago.Text = Utils.Now.ToString("dd/MM/yyyy");
            }
            this.pnlNewPagoResult.Visible = false;
        }

        protected void btnAddPago_Click(object sender, EventArgs e)
        {
            int newID = -1;
            DateTime dtFecha;
            double dMonto = 0;
            bool HayError = false;
            string sResult = string.Empty;

            this.pnlNewPagoResult.Visible = true;

            if (!DateTime.TryParse(this.txtFechaPago.Text, out dtFecha))
            {
                HayError = true;
                sResult += "LA FECHA DE PAGO ES INVALIDA<BR />";
            }

            if (this.txtNombrePago.Text.Trim().Length == 0)
            {
                HayError = true;
                sResult += "DEBE INDICAR UN NOMBRE<BR />";
            }

            if (!double.TryParse(this.txtMonto.Text, out dMonto))
            {
                HayError = true;
                sResult += "EL MONTO DE PAGO ES INVALIDO<BR />";
            }
            int cheque = 0;
            if (int.TryParse(this.txtChequeNum.Text, out cheque) && cheque > 0)
            {
                if (this.cmbConceptomovBancoPago.SelectedItem.Text.ToUpper().Equals("CHEQUE") && dbFunctions.ChequeAlreadyExists(cheque, this.cmbCuentaPago.SelectedItem.Value.ToString()))
                {
                    HayError = true;
                    sResult += "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, EL CHEQUE YA ESTA EXISTE<BR />";
                }

                if (this.cmbConceptomovBancoPago.SelectedItem != null && this.cmbConceptomovBancoPago.SelectedItem.Text.IndexOf("CHEQUE") > -1 && !dbFunctions.numChequeValido(cheque, int.Parse(this.cmbCuentaPago.SelectedValue.ToString())))
                {
                    HayError = true;
                    sResult += "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, EL NUMERO DE CHEQUE NO CORRESPONDE A EL NUMERO DE CUENTA<BR />";
                }
            }

            /*
            SqlCommand commSaldo = new SqlCommand();
                        commSaldo.CommandText = "select ISNULL(Boletas - Anticipos - Pagos_creditos - Pagos,0) from vTotalesLiquidacion where LiquidacionID = @LiquidacionID";
                        commSaldo.Parameters.Add("@LiquidacionID", SqlDbType.Int).Value = this.iLiqID;
                        double SaldoRestante = dbFunctions.GetExecuteDoubleScalar(commSaldo, 0);
                        SaldoRestante = Math.Round(SaldoRestante, 2);
            
                        if (dMonto > SaldoRestante)
                        {
                            HayError = true;
                            sResult += "EL PAGO NO PUEDE SER MAYOR QUE EL SALDO RESTANTE DE LA LIQUIDIACION<BR />";
                        }*/
            

            if (!HayError)
            {
                {
                    SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                    try
                    {
                        conn.Open();
                        SqlCommand comm = new SqlCommand();
                        comm.Connection = conn;
                        comm.CommandText = "INSERT INTO MovimientosCuentasBanco "
                            + " (cuentaID, ConceptoMovCuentaID, fecha, cargo, abono, userID, nombre, numCheque, chequeNombre,  catalogoMovBancoFiscalID, "
                            + " subCatalogoMovBancoFiscalID, catalogoMovBancoInternoID, subCatalogoMovBancoInternoID, fechaCheque) "
                            + " VALUES     (@cuentaID,@ConceptoMovCuentaID,@fecha,@cargo,@abono,@userID,@nombre,@numCheque,@chequeNombre,@catalogoMovBancoFiscalID,@subCatalogoMovBancoFiscalID,@catalogoMovBancoInternoID,@subCatalogoMovBancoInternoID, @fechaCheque);"
                            + " SELECT SCOPE_IDENTITY();";

                        comm.Parameters.Add("@cuentaID", SqlDbType.Int).Value = int.Parse(this.cmbCuentaPago.SelectedValue);
                        comm.Parameters.Add("@ConceptoMovCuentaID", SqlDbType.Int).Value = int.Parse(this.cmbConceptomovBancoPago.SelectedValue);
                        comm.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtFecha;
                        comm.Parameters.Add("@fechaCheque", SqlDbType.DateTime).Value = dtFecha;
                        comm.Parameters.Add("@cargo", SqlDbType.Float).Value = dMonto;
                        comm.Parameters.Add("@abono", SqlDbType.Float).Value = 0;
                        comm.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                        comm.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtNombrePago.Text.Trim().ToUpper();

                        comm.Parameters.Add("@chequeNombre", SqlDbType.VarChar).Value = this.txtChequeNombre.Text.Trim().ToUpper();

                        comm.Parameters.Add("@catalogoMovBancoFiscalID", SqlDbType.Int).Value = int.Parse(this.drpdlCatalogocuentafiscalPago.Text);
                        comm.Parameters.Add("@subCatalogoMovBancoFiscalID", SqlDbType.Int).Value = this.drpdlSubcatalogofiscalPago.Items.Count > 0 ? int.Parse(this.drpdlSubcatalogofiscalPago.Text) : -1;

                        if (this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE")
                        {
                            comm.Parameters.Add("@catalogoMovBancoInternoID", SqlDbType.Int).Value = int.Parse(this.drpdlCatalogoInternoPago.Text);
                            comm.Parameters.Add("@subCatalogoMovBancoInternoID", SqlDbType.Int).Value = this.drpdlSubcatologointernaPago.Items.Count > 0 ? int.Parse(this.drpdlSubcatologointernaPago.Text) : -1;
                            comm.Parameters.Add("@numCheque", SqlDbType.Int).Value = int.Parse(this.txtChequeNum.Text);
                        }
                        else
                        {
                            comm.Parameters.Add("@catalogoMovBancoInternoID", SqlDbType.Int).Value = int.Parse(this.drpdlCatalogocuentafiscalPago.Text);
                            comm.Parameters.Add("@subCatalogoMovBancoInternoID", SqlDbType.Int).Value = this.drpdlSubcatalogofiscalPago.Items.Count > 0 ? int.Parse(this.drpdlSubcatalogofiscalPago.Text) : -1;
                            comm.Parameters.Add("@numCheque", SqlDbType.Int).Value = 0;
                        }
                        comm.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(this.ddlCiclos.SelectedValue);
                        newID = int.Parse(comm.ExecuteScalar().ToString());
                        if (newID > -1)
                        {
                            HayError = false;
                            sResult = "EL PAGO FUE AGREGADO EXITOSAMENTE";
                            this.txtMonto.Text = string.Empty;
                        }
                        else
                        {
                            HayError = true;
                            sResult = "NO FUE POSIBLE AGREGAR EL PAGO, ERROR DESCONOCIDO.";
                        }
                    }
                    catch (System.Exception ex)
                    {
                        HayError = true;
                        sResult += "NO SE PUDO AGREGAR EL PAGO EL ERROR ES: <BR />";
                        sResult += ex.Message;
                        Logger.Instance.LogException(Logger.typeUserActions.INSERT, "err insertando pago de efectivo a liquidacion", ex);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            this.imgMalPago.Visible = HayError;
            this.imgBienPago.Visible = !HayError;
            this.lblNewPagoResult.Text = sResult;
            this.dvDetalleMov.DataBind();
            if (!HayError && newID > -1)
            {
                this.pnlMovimientoDetalle.Visible = true;
                this.pnlAddNewPago.Visible = false;
                this.lblMovBanID.Text = newID.ToString();
                this.pnlFacturasToAdd.Visible = true;
                this.dvDetalleMov.DataBind();
                this.gvFacturasGanado.DataBind();
            }
        }

        protected void ddlGanaderos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvFacturasGanado.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "INSERT INTO PagosFacturaDeGanado (FacturadeGanadoID, movbanID, montoPago) VALUES     (@FacturadeGanadoID,@movbanID,@montoPago)";
                foreach(GridViewRow row in this.gvFacturasGanado.Rows)
                {
                    try
                    {
                        if (((CheckBox)row.FindControl("chkAgregar")).Checked)
                        {
                            comm.Parameters.Clear();
                            comm.Parameters.Add("@FacturadeGanadoID", SqlDbType.Int).Value = this.gvFacturasGanado.DataKeys[row.RowIndex][0].ToString();
                            comm.Parameters.Add("movbanID", SqlDbType.Int).Value = this.lblMovBanID.Text;
                            comm.Parameters.Add("@montoPago", SqlDbType.Float).Value = Utils.GetSafeFloat(((TextBox)row.FindControl("txtCantidadAPagar")).Text);
                            comm.ExecuteNonQuery();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.INSERT, "inserting pago into factura de ganado", ex);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Pagos agregados a facturas error", ex);
            }
            finally
            {
                conn.Close();
                this.gvFacturasRelacionadas.DataBind();
                this.gvFacturasGanado.DataBind();
            }
        }

        protected void btnQuitarPagosDeFacturas_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "DELETE FROM PagosFacturaDeGanado WHERE  pagoID = @pagoID";
                foreach (GridViewRow row in this.gvFacturasRelacionadas.Rows)
                {
                    try
                    {
                        if (((CheckBox)row.FindControl("chkQuitar")).Checked)
                        {
                            comm.Parameters.Clear();
                            comm.Parameters.Add("@pagoID", SqlDbType.Int).Value = this.gvFacturasRelacionadas.DataKeys[row.RowIndex][0].ToString();
                            comm.ExecuteNonQuery();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.INSERT, "DELETEING pago into factura de ganado", ex);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Pagos eliminados a facturas error", ex);
            }
            finally
            {
                conn.Close();
                this.gvFacturasRelacionadas.DataBind();
                this.gvFacturasGanado.DataBind();
            }
        }
    }
}
