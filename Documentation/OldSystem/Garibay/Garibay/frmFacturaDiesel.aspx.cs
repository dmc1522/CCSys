using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Garibay
{
    public partial class Formulario_web13 : Garibay.BasePage
    {
        private void AddJSToControls()
        {
            String sOnchagemov = "checkValueInList(";
            sOnchagemov += "this" + ",'CHEQUE','";
            sOnchagemov += this.divCheque.ClientID + "');";
            this.cmbConceptomovBancoPago.Attributes.Add("onChange", sOnchagemov);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.cmbConceptomovBancoPago.DataBind();
                this.cmbCuentaPago.DataBind();
                this.AddJSToControls();
                this.txtFechaFactura.Text = Utils.Now.ToShortDateString();
                this.panelMensaje.Visible = false;
                if (Request.QueryString["data"] != null && this.loadqueryStrings(Request.QueryString["data"].ToString()) && this.myQueryStrings != null && this.myQueryStrings["FacturaDieselID"] != null)
                {
                    this.lblFoliotitle.Visible = true;
                    this.lblFolio.Text = this.myQueryStrings["FacturaDieselID"].ToString();
                    this.txtIdtoModify.Text = this.myQueryStrings["FacturaDieselID"].ToString();
                    if (this.LoadFactura(this.myQueryStrings["FacturaDieselID"].ToString()))
                    {
                        Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURASDIESEL, Logger.typeUserActions.SELECT, "Cargo Factura: " + this.lblFolio.Text);
                        this.pnlTarjetas.Visible = true;
                        this.btnAdd.Visible = false;
                        this.btnSaveFactura.Visible = true;
                    }
                }
                else
                {
                    this.lblFoliotitle.Visible = false;
                    this.pnlAgregar.Visible = true;
                    this.pnlTarjetas.Visible = false;
                    this.lblFolio.Text = "";
                }
            }
            this.panelMensaje.Visible=false;
            this.divCheque.Attributes.Add("style", this.cmbConceptomovBancoPago.SelectedItem != null && this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.pnlNewPago.Visible = false;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            string sQuery = " INSERT INTO FacturasDiesel (FacturaFolio, Fecha, monto, userid, storeTS) VALUES (@FacturaFolio,@Fecha, @monto, @userid, @storeTS);";            
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = sQuery;
                comm.Parameters.Add("@FacturaFolio", SqlDbType.Int).Value = this.txtNoFactura.Text;
                comm.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaFactura.Text));
                comm.Parameters.Add("@monto", SqlDbType.Float).Value = double.Parse(this.txtMonto.Text);
                comm.Parameters.Add("@userid", SqlDbType.Int).Value = this.UserID;
                comm.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.Now;
                comm.ExecuteNonQuery();
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURASDIESEL, Logger.typeUserActions.INSERT, "Nueva Factura Diesel: " + this.txtNoFactura.Text);
                String Query = "FacturaDieselID=" + this.txtNoFactura.Text;
                Query = Utils.GetEncriptedQueryString(Query);
                String strRedirect = "~/frmFacturaDiesel.aspx" + Query;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURASDIESEL, Logger.typeUserActions.INSERT, " SE AGREGO LA FACTURA DE DIESEL CON EL FOLIO : " + this.txtNoFactura.Text);
                Response.Redirect(strRedirect);
            }
            catch(Exception ex)
            {
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.lblMensajeException.Text = ex.Message;
                this.lblMensajeOperationresult.Text = "NO SE PUDO AGREGAR LA FACTURA DIESEL";
                this.lblMensajetitle.Text = "FALLO";
                this.panelMensaje.Visible = true;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "ERROR AL INSERTAR UNA FACTURA DE DIESEL:  EX " + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        protected bool LoadFactura(string id)
        {
            bool sresult;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            string sQuery = " SELECT FacturaFolio, Fecha, monto FROM FacturasDiesel WHERE FacturaFolio = @FacturaFolio";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = sQuery;
                comm.Parameters.Add("@FacturaFolio", SqlDbType.Int).Value = int.Parse(id);
                SqlDataReader rd;
                rd = comm.ExecuteReader();
                if(rd.HasRows&&rd.Read())
                {
                    this.txtFechaFactura.Text = DateTime.Parse(rd["Fecha"].ToString()).ToShortDateString().ToString();
                    this.txtNoFactura.Text = id;
                    this.lblFoliotitle.Visible = true;
                    this.txtMonto.Text = rd["monto"].ToString();
                }
                this.sumaMonto();
                sresult = true;
                //Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.SELECT, this.UserID, "SE CARGARON LOS DATOS DE LA FACTURA DIESEL CON EL FOLIO " + id, this.Request.Url.ToString());

            }
            catch(Exception ex)
            {
                sresult = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, this.UserID, "ERROR AL CARGAR UNA FACTURA DE DIESEL:  EX " + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                conn.Close();
            }
            return sresult;
        }

        protected void btnaddAFactura_Click(object sender, EventArgs e)
        {
            string folios = "";
            CheckBox che = new CheckBox();

            foreach(GridViewRow row in grdvNoRelacionadas.Rows)
            {
                che = (CheckBox)row.FindControl("CheckBox1");
                if (che.Checked)
                {
                    SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                    string squery = "UPDATE TarjetasDiesel SET FacturaFolio = @FacturaFolio WHERE (folio = @folio)";
                    try
                    {
                        conn.Open();
                        SqlCommand comm = new SqlCommand();
                        comm.Connection = conn;
                        comm.CommandText = squery;
                        comm.Parameters.Add("@FacturaFolio", SqlDbType.Int).Value = int.Parse(this.lblFolio.Text);
                        comm.Parameters.Add("@folio", SqlDbType.Int).Value = int.Parse(row.Cells[1].Text);
                        int upr = comm.ExecuteNonQuery();
                        if (upr != 1)
                        {
                            throw new Exception("ERROR AL MODIFICAR  LA TARJETA DIESEL CON EL FOLIO " + row.Cells[1].Text);

                        }
                        folios += row.Cells[1].Text + "-";
                        this.imagenbien.Visible = true;
                        this.imagenmal.Visible = false;
                        this.lblMensajeException.Text = "";
                        this.lblMensajeOperationresult.Text = "LA TARJETA(S) DIESEL CON EL FOLIO(S) : " + folios + " SE HA AGREGADO(ARON) A LA FACTURA SATISFACTORIAMENTE";
                        this.lblMensajetitle.Text = "EXITO";
                        this.panelMensaje.Visible = true;

                        Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURASDIESEL, Logger.typeUserActions.UPDATE, " SE AGREGO LA TARJETA DIESEL CON EL FOLIO " + row.Cells[1].Text + " A LA FACTURA : " + this.lblFolio.Text);
                        this.grdvRelaionados.SelectedIndex = -1;
                        this.grdvNoRelacionadas.SelectedIndex = -1;
                    }
                    catch (Exception ex)
                    {
                        this.imagenbien.Visible = false;
                        this.imagenmal.Visible = true;
                        this.lblMensajeException.Text = ex.Message;
                        this.lblMensajeOperationresult.Text = "NO SE PUDO AGREGAR LA TARJETA DIESEL CON EL FOLIO: " + this.grdvNoRelacionadas.SelectedRow.Cells[1].Text;
                        this.lblMensajetitle.Text = "FALLO";
                        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL AGREGAR LA TARJETA DIESEL CON EL FOLIO :" + row.Cells[1].Text + " A LA  FACTURA DE DIESEL: " + this.lblFolio.Text + " EX " + ex.Message, this.Request.Url.ToString());
                        this.panelMensaje.Visible = true;

                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            this.grdvNoRelacionadas.DataBind();
            this.grdvRelaionados.DataBind();
            sumaMonto();
        }

        protected void btnRemovFromFactura_Click(object sender, EventArgs e)
        {
            CheckBox che = new CheckBox();
            string folios = "";
            foreach(GridViewRow row in grdvRelaionados.Rows)
            {
                che = (CheckBox)row.FindControl("CheckBox2");
                if (che.Checked)
                {
                    SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                    string squery = "UPDATE TarjetasDiesel SET FacturaFolio = @FacturaFolio WHERE (folio = @folio)";
                    try
                    {
                        conn.Open();
                        SqlCommand comm = new SqlCommand();
                        comm.Connection = conn;
                        comm.CommandText = squery;
                        comm.Parameters.Add("@FacturaFolio", SqlDbType.Int).Value = DBNull.Value;
                        comm.Parameters.Add("@folio", SqlDbType.Int).Value = int.Parse(row.Cells[1].Text);
                        int upr = comm.ExecuteNonQuery();
                        if (upr != 1)
                        {
                            throw new Exception("ERROR AL MODIFICAR  LA TARJETA DIESEL CON EL FOLIO " + row.Cells[1].Text);

                        }
                        folios += row.Cells[1].Text + "-";
                        this.imagenbien.Visible = true;
                        this.imagenmal.Visible = false;
                        this.lblMensajeException.Text = "";
                         
                        this.lblMensajeOperationresult.Text = "LA TARJETA(S) DIESEL CON EL FOLIO(S) : " + folios + " SE ELIMINO(ARON) DE LA FACTURA SATISFACTORIAMENTE";
                        this.lblMensajetitle.Text = "EXITO";
                        this.panelMensaje.Visible = true;
                        
                        Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURASDIESEL, Logger.typeUserActions.UPDATE, " SE AGREGO LA TARJETA DIESEL CON EL FOLIO : " + row.Cells[1].Text + " SE ELIMINO DE LA FACTURA  : " + this.lblFolio.Text);
                        this.grdvRelaionados.SelectedIndex = -1;
                        this.grdvNoRelacionadas.SelectedIndex = -1;
                    }
                    catch (Exception ex)
                    {
                        this.imagenbien.Visible = false;
                        this.imagenmal.Visible = true;
                        this.lblMensajeException.Text = ex.Message;
                        this.lblMensajeOperationresult.Text = "NO SE PUDO ELIMINAR LA TARJETA DIESEL CON EL FOLIO: " + row.Cells[1].Text;
                        this.lblMensajetitle.Text = "FALLO";
                        this.panelMensaje.Visible = true;
                        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL ELIMINAR LA TARJETA DIESEL CON EL FOLIO :" + row.Cells[1].Text + " A LA  FACTURA DE DIESEL: " + this.lblFolio.Text + " EX " + ex.Message, this.Request.Url.ToString());

                    }
                    finally
                    {
                        conn.Close();
                    }

                    this.grdvNoRelacionadas.DataBind();
                    this.grdvRelaionados.DataBind();

                    sumaMonto();
                }
            }
        }


        protected void sumaMonto()
        {
            double monto=0d;
            foreach (GridViewRow row in this.grdvRelaionados.Rows)
            {

                monto += Utils.GetSafeFloat(row.Cells[2].Text);
            }
            this.lblMonto.Text = string.Format("{0:c2}",monto);
            double montoFact;
            montoFact = double.Parse(this.txtMonto.Text);
            this.lblMontoRestante.Text = string.Format("{0:c2}", montoFact - monto - Utils.GetSafeFloat(this.lblTotalPagos.Text));
        }

        protected void actualizamonto(double monto)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            string sQuery = " UPDATE FacturasDiesel SET monto=@monto WHERE FacturaFolio=@FacturaFolio";
            
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = sQuery;
                
                comm.Parameters.Add("@monto", SqlDbType.Float).Value = monto;
                comm.Parameters.Add("@FacturaFolio", SqlDbType.Int).Value = int.Parse(this.lblFolio.Text);
                int upr = comm.ExecuteNonQuery();
                if (upr != 1)
                {
                    throw new Exception("ERROR AL MODIFICAR EL MONTO DE LA FACTURA " + this.lblFolio.Text);

                }
            }
            catch(Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL ACTUALIZAR EL MONTO DE LA FACTURA DIESEL: " + this.lblFolio.Text + " EX " + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnSaveFactura_Click(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            //string sQuery = " UPDATE FacturasDiesel SET monto=@monto,  FacturaFolio=@FacturaFolio, Fecha=@Fecha, userid=@userid WHERE FacturaFolio=@Fac";

            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("UPDATE_TARJETASDIESEL",conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("@MONTO", SqlDbType.Float).Value = this.txtMonto.Text;
                comm.Parameters.Add("@FACTURAFOLIONEW", SqlDbType.Int).Value = int.Parse(this.txtNoFactura.Text);
                comm.Parameters.Add("@FECHA", SqlDbType.DateTime).Value = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaFactura.Text));
                comm.Parameters.Add("@USERID", SqlDbType.Int).Value = this.UserID;
                comm.Parameters.Add("@FACTURAFOLIO", SqlDbType.Int).Value = int.Parse(this.txtIdtoModify.Text);
                int upr = comm.ExecuteNonQuery();
                if (upr < 1)
                {
                    throw new Exception("ERROR AL MODIFICAR LA FACTURA " + this.lblFolio.Text);

                }
                this.imagenbien.Visible = true;
                this.imagenmal.Visible = false;
                this.lblMensajeException.Text = "";
                this.lblMensajeOperationresult.Text = "LA FACTURA DE TARJETA DIESEL CON EL FOLIO : " + this.txtIdtoModify.Text + " SE MODIFICÓ DE LA FACTURA SATISFACTORIAMENTE";
                this.lblMensajetitle.Text = "EXITO";
                this.panelMensaje.Visible = true;
                lblFolio.Text = this.txtNoFactura.Text;
                this.txtIdtoModify.Text = this.txtNoFactura.Text;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURASDIESEL, Logger.typeUserActions.UPDATE, " SE MODIFICÓ LA FACTURA DE TARJETA DIESEL CON EL FOLIO : " + this.lblFolio.Text);

            }
            catch (Exception ex)
            {

                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.lblMensajeException.Text = "";
                this.lblMensajeOperationresult.Text = "ERROR AL MODIFICAR LA FACTURA DE TARJETA DIESEL CON EL FOLIO : " + this.txtIdtoModify.Text;
                this.lblMensajetitle.Text = "FALLO";
                this.panelMensaje.Visible = true;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL ACTUALIZAR LA FACTURA DIESEL: " + this.lblFolio.Text + " EX " + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnAddPago_Click(object sender, EventArgs e)
        {

            int cheque = 0;
            bool hayerrorenmonto = false;
            double monto = 0;
            double.TryParse(this.txtMontoPago.Text, out monto);

            hayerrorenmonto = (monto == 0);
            
            if (hayerrorenmonto)
            {
                this.pnlNewPago.Visible = true;
                this.imgMalPago.Visible = true;
                this.imgBienPago.Visible = false;
                this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, ERROR EN MONTO, ESCRIBA CANTIDAD VÁLIDA";
                this.pnlNewPago.Focus();
                return;
            }
            //COMPROBAMOS QUE NO SE PASE DE TOTAL
            double totalRestante = 0, totalPagos = 0;
            totalRestante = Utils.GetSafeFloat(this.lblMontoRestante.Text);
            totalPagos = Utils.GetSafeFloat(this.lblTotalPagos.Text);
            if (monto > totalRestante)
            {
                this.pnlNewPago.Visible = true;
                this.imgMalPago.Visible = true;
                this.imgBienPago.Visible = false;
                this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, LOS PAGOS NO PUEDEN SER MAYORES AL TOTAL RESTANTE";
                this.pnlNewPago.Focus();
                return;
            }
            if (this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE")
            {
                if (int.TryParse(this.txtChequeNum.Text, out cheque))
                {
                    if (dbFunctions.ChequeAlreadyExists(cheque, this.cmbCuentaPago.SelectedValue))
                    {
                        this.pnlNewPago.Visible = true;
                        this.imgBienPago.Visible = false;
                        this.imgMalPago.Visible = true;
                        this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, ESTE CHEQUE YA HA SIDO AGREGADO";
                        //this.cmbTipodeMovPago.SelectedIndex = 0;
                        return;
                    }
                }
                else
                {
                    this.pnlNewPago.Visible = true;
                    this.imgBienPago.Visible = false;
                    this.imgMalPago.Visible = true;
                    this.lblNewPagoResult.Text = "ERROR!! EL NUMERO DE CHEQUE ES INCORRECTO";
                    //this.cmbTipodeMovPago.SelectedIndex = 0;
                    return;
                }
                if (!dbFunctions.numChequeValido(cheque, int.Parse(this.cmbCuentaPago.SelectedValue)))
                {

                    this.pnlNewPago.Visible = true;
                    this.imgBienPago.Visible = false;
                    this.imgMalPago.Visible = false;
                    this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, EL NUMERO DE CHEQUE NO CORRESPONDE A EL NUMERO DE CUENTA";


                    return;


                }
            }

            if (dbFunctions.FechaEnPeriodoBloqueado(DateTime.Parse(this.txtFechaNPago.Text), int.Parse(this.cmbCuentaPago.SelectedValue)))
            {
                this.pnlNewPago.Visible = true;
                this.imgBienPago.Visible = false;
                this.imgMalPago.Visible = true;
                this.lblNewPagoResult.Text = "EL MOVIMIENTO NO PUEDE SER AGREGADO YA QUE LA FECHA ESTA DENTRO DE UN PERIODO BLOQUEADO<BR />DESBLOQUEE EL PERIODO PARA PERMITIR INGRESAR EL MOVIMIENTO";
                return;
            }

            dsMovBanco.dtMovBancoDataTable tablaaux = new dsMovBanco.dtMovBancoDataTable();
            dsMovBanco.dtMovBancoRow dtRowainsertar = tablaaux.NewdtMovBancoRow();
            dtRowainsertar.chequecobrado = false;
            dtRowainsertar.conceptoID = int.Parse(this.cmbConceptomovBancoPago.SelectedValue);
            dtRowainsertar.nombre = this.txtNombrePago.Text;
            dtRowainsertar.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaNPago.Text));
            dtRowainsertar.numCheque = this.txtChequeNum.Text.Length > 0 ? int.Parse(this.txtChequeNum.Text) : 0;
            dtRowainsertar.chequeNombre = this.txtChequeNombre.Text;
            dtRowainsertar.facturaOlarguillo = "";
            dtRowainsertar.numCabezas = 0;
            dtRowainsertar.catalogoMovBancoFiscalID = int.Parse(this.drpdlCatalogocuentafiscalPago.SelectedValue);
            if (this.drpdlSubcatalogofiscalPago.SelectedIndex > -1)
                dtRowainsertar.subCatalogoMovBancoFiscalID = int.Parse(this.drpdlSubcatalogofiscalPago.SelectedValue);
            if (dtRowainsertar.numCheque > 0)
            {
                dtRowainsertar.catalogoMovBancoInternoID = int.Parse(this.drpdlCatalogoInternoPago.SelectedValue);
                if (this.drpdlSubcatologointernaPago.SelectedIndex > -1)
                    dtRowainsertar.subCatalogoMovBancoInternoID = int.Parse(this.drpdlSubcatologointernaPago.SelectedValue);
            }
            else
            {
                dtRowainsertar.catalogoMovBancoInternoID = dtRowainsertar.catalogoMovBancoFiscalID;
                dtRowainsertar.subCatalogoMovBancoInternoID = dtRowainsertar.subCatalogoMovBancoFiscalID;
            }

            dtRowainsertar.cargo = monto;
            dtRowainsertar.abono = 0.00;
            dtRowainsertar.storeTS = DateTime.Parse(Utils.getNowFormattedDate());
            dtRowainsertar.updateTS = DateTime.Parse(Utils.getNowFormattedDate());
            String serror = "", tipo = "";
            dtRowainsertar.cuentaID = int.Parse(this.cmbCuentaPago.SelectedValue);
            dtRowainsertar.creditoFinancieroID = -1;
            ListBox a = new ListBox();
            if (dbFunctions.insertaMovBanco(ref dtRowainsertar, ref serror, int.Parse(this.Session["USERID"].ToString()), int.Parse(this.cmbCuentaPago.SelectedValue), dbFunctions.GetLastCiclo(), -1, "", this.txtObserv.Text))
            {
                SqlConnection connFactura = new SqlConnection(myConfig.ConnectionInfo);
                try
                {
                    connFactura.Open();
                    SqlCommand commFactura = new SqlCommand();
                    commFactura.Connection = connFactura;
                    commFactura.CommandText = "INSERT INTO PagosFacturasDiesel (FacturaFolio, movbanID) VALUES (@FacturaFolio,@movbanID) ";
                    //(@FacturaCVID,@fecha,@movbanID,@movCajaID,@userID)
                    commFactura.Parameters.Add("@FacturaFolio", SqlDbType.Int).Value = int.Parse(this.lblFolio.Text);
                    commFactura.Parameters.Add("@movbanID", SqlDbType.Int).Value = dtRowainsertar.movBanID;
                    if (commFactura.ExecuteNonQuery() != 1)
                    {
                        throw new Exception("This must almost never happen");
                    }
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error adding new movbanco->factura", ref ex);
                }
                finally
                {
                    connFactura.Close();
                }
                //this.btnActualizarLista.DataBind();
                this.pnlNewPago.Visible = true;
                this.imgBienPago.Visible = true;
                this.imgMalPago.Visible = false;
                this.lblNewPagoResult.Text = "SE AGREGO EL PAGO CORRECTAMENTE";
                this.gvPagosFactura.DataBind();
//                 this.dvTotales.DataBind();
            }
            else
            {
                this.pnlNewPago.Visible = true;
                this.imgBienPago.Visible = false;
                this.imgMalPago.Visible = true;
                this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO";
            }

        }

        protected string GetPrintChequeURL(string id)
        {
            string parameter = "javascript:url('";
            parameter += "frmPrintCheque.aspx" + Utils.GetEncriptedQueryString("iMovID=" + id);
            parameter += "', 'Print Cheque',200,200,300,300); return false;";
            return parameter;
        }

        protected bool IsChequeVisible(string id)
        {
            int cheq = -1;
            if (id != null && int.TryParse(id.Trim(), out cheq) && cheq > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void gvPagosFactura_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection conDelete = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdDelete = new SqlCommand();
            try
            {
                conDelete.Open();
                cmdDelete.Connection = conDelete;
                cmdDelete.CommandText = "delete from PagosFacturasDiesel where movbanID = @movbanID;delete from MovimientosCuentasBanco where movbanID = @movbanID;";
                cmdDelete.Parameters.Add("@movbanID", SqlDbType.Int).Value = (int)(this.gvPagosFactura.DataKeys[e.RowIndex]["movbanID"]);
                cmdDelete.ExecuteNonQuery();
                this.gvPagosFactura.DataBind();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "ERROR DELETING PAGOS IN FACTURAS DE GANADO", ref ex);
            }
            finally
            {
                conDelete.Close();
            }
        }

        protected void gvPagosFactura_DataBound(object sender, EventArgs e)
        {
            try
            {
                double total = 0;
                foreach(GridViewRow row in this.gvPagosFactura.Rows)
                {
                    total += Utils.GetSafeFloat(row.Cells[3].Text);
                }
                this.lblTotalPagos.Text = string.Format("{0:C2}", total);
                this.sumaMonto();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "getting pagos", ref ex);
            }
        }

    }
}
