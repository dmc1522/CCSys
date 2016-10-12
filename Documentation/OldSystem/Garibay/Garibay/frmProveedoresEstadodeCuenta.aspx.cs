using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Garibay
{
    public partial class frmProveedoresEstadodeCuenta : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.divAgregarNuevoPago.Attributes.Add("style", this.chkMostrarAgregarPago.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divPagoMovCaja.Attributes.Add("style", "visibility: hidden; display: none");
            if(!this.IsPostBack)
            {
                this.AddjstoControls();                    
                this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.ddlCiclos.DataBind();
                this.ddlProveedores.DataBind();
                this.getProveedoresValues();
                this.txtFechaNPago.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.cmbConceptomovBancoPago.DataBind();
                this.cmbCuentaPago.DataBind();
                this.drpdlCatalogocuentafiscalPago.DataBind();
                this.drpdlSubcatalogofiscalPago.DataBind();
                this.drpdlCatalogoInternoPago.DataBind();
                this.drpdlSubcatologointernaPago.DataBind();
                this.drpdlGrupoCatalogosInternaPago.DataBind();
                this.drpdlGrupoCuentaFiscal.DataBind();
                this.grvPagos.DataBind();
            }
            this.pnlNewPago.Visible = false;
            this.lblNotaDeCreditoResult.Text = string.Empty;
        }
        private void AddjstoControls()
        {
            String sOnchangeAB = "ShowHideDivOnChkBox('";
            sOnchangeAB += this.chkMostrarAgregarPago.ClientID + "','";
            sOnchangeAB += this.divAgregarNuevoPago.ClientID + "')";
            this.chkMostrarAgregarPago.Attributes.Add("onclick", sOnchangeAB);
            String sOnchagemov = "";
            this.divMovBanco.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "MOVIMIENTO DE BANCO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            sOnchagemov += "checkValueInList(";
            sOnchagemov += "this" + ",'MOVIMIENTO DE BANCO','";
            sOnchagemov += this.divMovBanco.ClientID + "');";
            this.cmbTipodeMovPago.Attributes.Add("onChange", sOnchagemov);
            this.cmbConceptomovBancoPago.DataBind();
            this.divCheque.Attributes.Add("style", this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            sOnchagemov = "checkValueInList(";
            sOnchagemov += "this" + ",'CHEQUE','";
            sOnchagemov += this.divCheque.ClientID + "');";
            this.cmbConceptomovBancoPago.Attributes.Add("onChange", sOnchagemov);
            this.imgBienPago.Visible = false;
            this.imgMalPago.Visible = false;
            this.pnlNewPago.Visible = false;
        }
        private void getProveedoresValues()
        {
            string sql = "select Direccion, Comunidad, Municipio, Teléfono, Estados.estado  from Proveedores INNER JOIN Estados on Proveedores.estadoID = Estados.estadoID where proveedorID = @proveedorID ";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdProveedoresData = new SqlCommand(sql, conGaribay);
            try
            {
                conGaribay.Open();
                int proveedorID = -1;
                if(int.TryParse(this.ddlProveedores.SelectedValue, out proveedorID) && proveedorID > -1)
                {
                    cmdProveedoresData.Parameters.Add("@proveedorID",SqlDbType.Int).Value =  proveedorID;
                    SqlDataReader reader = cmdProveedoresData.ExecuteReader();
                    if(reader.Read())
                    {
                        this.txtDireccion.Text = reader["Direccion"].ToString();
                        this.txtComunidad.Text = reader["Comunidad"].ToString();
                        this.txtEstado.Text = reader["estado"].ToString();
                        this.txtTelefono.Text = reader["Teléfono"].ToString();
                        this.textBoxMunicipio.Text = reader["Municipio"].ToString();
                    }
                   
                }
                
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "ERROR AL CARGAR DATOS DE PROV", ref ex);
            }
            finally
            {
                conGaribay.Close();
            }
        }

        protected void grdvProNotasVenta_DataBound(object sender, EventArgs e)
        {
            //Utils.CellOnRedIfGreaterThanZero(16, ref this.grdvProNotasVenta);
        }

        protected void ddlProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.getProveedoresValues();
        }
        protected bool numChequeValido(int ChequeNum, int Cuenta)
        {
            Boolean sresult;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            String query = "SELECT numchequeinicio, numchequefin FROM CuentasDeBanco WHERE cuentaID=@cuentaID";
            SqlCommand comm = new SqlCommand(query, conn);
            SqlDataReader rd;
            try
            {
                conn.Open();
                comm.Parameters.Add("@cuentaID", SqlDbType.Int).Value = Cuenta;
                rd = comm.ExecuteReader();
                if (rd.HasRows && rd.Read())
                {

                    if (ChequeNum <= int.Parse(rd["numchequefin"].ToString()) && ChequeNum >= int.Parse(rd["numchequeinicio"].ToString()))
                    {
                        sresult = true;
                    }
                    else { sresult = false; }

                }
                else
                {
                    sresult = false;
                }


            }
            catch (Exception ex)
            {
                sresult = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, this.UserID, "ERROR  SELECCIONANDOEL TIRAJE DE CHEQUES EXC ES: " + ex.Message, this.Request.Url.ToString());

            }
            finally
            {

                conn.Close();
            }

            return sresult;


        }
        protected void btnAddPagoaProveedor_Click(object sender, EventArgs e)
        {
            this.pnlNewPago.Visible = false;
            double cargo = 0, abono = 0;
            double.TryParse(this.txtAbono.Text, out abono);
            double.TryParse(this.txtCargo.Text, out cargo);
            if (this.cmbTipodeMovPago.SelectedValue == "0")
            {
                try
                {
                    dsMovCajaChica.dtMovCajaChicaDataTable tablaaux = new dsMovCajaChica.dtMovCajaChicaDataTable();
                    dsMovCajaChica.dtMovCajaChicaRow dtRowainsertar = tablaaux.NewdtMovCajaChicaRow();
                    dtRowainsertar.nombre = this.txtNombrePago.Text;
                    dtRowainsertar.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaNPago.Text));
                    dtRowainsertar.cargo = cargo;
                    dtRowainsertar.abono = abono;
                    dtRowainsertar.storeTS = DateTime.Parse(Utils.getNowFormattedDate());
                    dtRowainsertar.updateTS = DateTime.Parse(Utils.getNowFormattedDate());
                    dtRowainsertar.Observaciones = "Pago a nota de compra";
                    dtRowainsertar.catalogoMovBancoInternoID = int.Parse(this.drpdlCatalogocuentaCajaChica.SelectedValue);
                    if (this.drpdlSubcatalogoCajaChica.SelectedIndex > 0)
                        dtRowainsertar.subCatalogoMovBancoInternoID = int.Parse(this.drpdlSubcatalogoCajaChica.SelectedValue);
                    dtRowainsertar.numCabezas = 0;
                    dtRowainsertar.facturaOlarguillo = "";
                    dtRowainsertar.bodegaID = int.Parse(this.ddlPagosBodegas.SelectedValue);
                    dtRowainsertar.cobrado = true;
                    String serror = "";
                    ListBox listBoxAgregadas = new ListBox();
                    if (dbFunctions.insertaMovCaja(ref dtRowainsertar, ref serror, this.UserID, int.Parse(this.ddlCiclos.SelectedValue)))
                    {
                        String sNewMov = dtRowainsertar.movimientoID.ToString();
                        SqlConnection conInsertNota = new SqlConnection(myConfig.ConnectionInfo);
                        string sqlInsert = "INSERT INTO Pagos_Proveedores(fecha, proveedorID, movimientoID, cicloID, userID) VALUES (@fecha, @proveedorID, @movimientoID, @cicloID, @userID);";
                        SqlCommand cmdInsert = new SqlCommand(sqlInsert, conInsertNota);
                        conInsertNota.Open();
                        try
                        {
                            cmdInsert.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtRowainsertar.fecha;
                            cmdInsert.Parameters.Add("@proveedorID", SqlDbType.Int).Value = int.Parse(this.ddlProveedores.SelectedValue);
                            cmdInsert.Parameters.Add("@movimientoID", SqlDbType.Int).Value = dtRowainsertar.movimientoID;
                            cmdInsert.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(this.ddlCiclos.SelectedValue);
                            cmdInsert.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;

                            if (cmdInsert.ExecuteNonQuery() != 1)
                            {
                                throw new Exception("This must almost never happen");
                            }

                            Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PROVEEDORES, Logger.typeUserActions.UPDATE, this.UserID, "SE INSERTÓ UN PAGO AL PROVEEDOR " + this.ddlProveedores.SelectedValue + " EL MOV DE CAJA CHICA FUE: " + dtRowainsertar.movimientoID.ToString());

                        }
                        catch (Exception ex)
                        {
                            Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL INSERTAR UN PAGO AL PROVEEDOR (CAJA CHICA): " + this.ddlProveedores.SelectedValue + ". EX " + ex.Message, this.Request.Url.ToString());

                        }
                        Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDECAJACHICA, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), "AGREGÓ EL MOVIMIENTO DE CAJA CHICA NÚMERO: " + dtRowainsertar.movimientoID.ToString());
                        
                    }
                    else
                    {
                        this.pnlNewPago.Visible = true;
                        this.imgBienPago.Visible = false;
                        this.imgMalPago.Visible = true;
                        this.lblNewPagoResult.Text = string.Format(myConfig.StrFromMessages("MOVCAJAADDEDFAILED"), dtRowainsertar.movimientoID.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "ERROR AL INSERTAR MOVIMIENTO DE CAJA. EX : " + ex.Message, this.Request.Url.ToString());
                }
            }
            else
                if (this.cmbTipodeMovPago.SelectedValue == "1")
                {
                    int cheque = 0;
                    bool hayerrorenmonto = false;
                    if (hayerrorenmonto)
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
                                return;
                            }
                        }
                        else
                        {
                            this.pnlNewPago.Visible = true;
                            this.imgBienPago.Visible = false;
                            this.imgMalPago.Visible = true;
                            this.lblNewPagoResult.Text = "ERROR!! EL NUMERO DE CHEQUE ES INCORRECTO";
                            return;
                        }


                        if (!numChequeValido(cheque, int.Parse(this.cmbCuentaPago.SelectedValue)))
                        {

                            this.pnlNewPago.Visible = true;
                            this.imgBienPago.Visible = false;
                            this.imgMalPago.Visible = true;
                            this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO, EL NUMERO DE CHEQUE NO CORRESPONDE A EL NUMERO DE CUENTA";
                            return;
                        }


                    }
                    dsMovBanco.dtMovBancoDataTable tablaaux = new dsMovBanco.dtMovBancoDataTable();
                    dsMovBanco.dtMovBancoRow dtRowainsertar = tablaaux.NewdtMovBancoRow();
                    dtRowainsertar.cargo = cargo;
                    dtRowainsertar.abono = abono;
                
                   
                    dtRowainsertar.chequecobrado = true;
                    dtRowainsertar.conceptoID = int.Parse(this.cmbConceptomovBancoPago.SelectedValue);
                    dtRowainsertar.nombre = this.txtNombrePago.Text;
                    dtRowainsertar.fecha = DateTime.Parse(Utils.converttoLongDBFormat(this.txtFechaNPago.Text));
                    dtRowainsertar.numCheque = this.txtChequeNum.Text.Length > 0 ? int.Parse(this.txtChequeNum.Text) : 0;
                    dtRowainsertar.chequeNombre = this.txtChequeNombre.Text;
                    dtRowainsertar.facturaOlarguillo = this.txtFacturaLarguillo.Text;
                    dtRowainsertar.numCabezas = 0;//this.txtNumCabezas.Text.Length > 0 ? double.Parse(this.txtNumCabezas.Text) : 0;

                    dtRowainsertar.catalogoMovBancoInternoID = int.Parse(this.drpdlCatalogoInternoPago.SelectedValue);
                    if (this.drpdlSubcatologointernaPago.SelectedIndex > -1)
                        dtRowainsertar.subCatalogoMovBancoInternoID = int.Parse(this.drpdlSubcatologointernaPago.SelectedValue);
                    if (this.cmbConceptomovBancoPago.SelectedItem != null && this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE")
                    {
                        dtRowainsertar.catalogoMovBancoFiscalID = int.Parse(this.drpdlCatalogocuentafiscalPago.SelectedValue);
                        if (this.drpdlSubcatalogofiscalPago.SelectedIndex > -1)
                            dtRowainsertar.subCatalogoMovBancoFiscalID = int.Parse(this.drpdlSubcatalogofiscalPago.SelectedValue);
                    }
                    else
                    {
                        dtRowainsertar.catalogoMovBancoFiscalID = dtRowainsertar.catalogoMovBancoInternoID;
                        dtRowainsertar.subCatalogoMovBancoFiscalID = dtRowainsertar.subCatalogoMovBancoInternoID;
                    }
                    dtRowainsertar.storeTS = DateTime.Parse(Utils.getNowFormattedDate());
                    dtRowainsertar.updateTS = DateTime.Parse(Utils.getNowFormattedDate());
                    String serror = "", tipo = "";
                    dtRowainsertar.cuentaID = int.Parse(this.cmbCuentaPago.SelectedValue);
                    dtRowainsertar.creditoFinancieroID = -1;
                    ListBox a = new ListBox();
                    if (dbFunctions.insertaMovBanco(ref dtRowainsertar, ref serror, int.Parse(this.Session["USERID"].ToString()), int.Parse(this.cmbCuentaPago.SelectedValue), int.Parse(this.ddlCiclos.SelectedValue), -1, "", "PAGO A NOTA DE COMPRA"))
                    {
                        SqlConnection connVenta = new SqlConnection(myConfig.ConnectionInfo);
                        try
                        {
                            connVenta.Open();
                            SqlCommand commVenta = new SqlCommand();
                            commVenta.Connection = connVenta;
                            commVenta.CommandText = "INSERT INTO Pagos_Proveedores(fecha, proveedorID, movbanID, cicloID, userID) VALUES (@fecha, @proveedorID, @movbanID, @cicloID, @userID);";
                            commVenta.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtRowainsertar.fecha;
                            commVenta.Parameters.Add("@proveedorID", SqlDbType.Int).Value = int.Parse(this.ddlProveedores.SelectedValue);
                            commVenta.Parameters.Add("@movbanID", SqlDbType.Int).Value = dtRowainsertar.movBanID;
                            commVenta.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(this.ddlCiclos.SelectedValue);
                            commVenta.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;

                            if (commVenta.ExecuteNonQuery() != 1)
                            {
                                throw new Exception("This must almost never happen");
                            }
                        }
                        catch (System.Exception ex)
                        {
                            Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error adding new movbanco->PROVEEDOR PAGOS", ref ex);
                        }
                        finally
                        {
                            connVenta.Close();
                        }                       
                        this.cmbTipodeMovPago.SelectedIndex = 0;
                        this.divPagoMovCaja.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "EFECTIVO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                        this.divMovBanco.Attributes.Add("style", this.cmbTipodeMovPago.SelectedItem.Text == "MOVIMIENTO DE BANCO" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                        this.divCheque.Attributes.Add("style", this.cmbConceptomovBancoPago.SelectedItem.Text == "CHEQUE" ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                       
                    }
                    else
                    {
                        this.pnlNewPago.Visible = true;
                        this.imgBienPago.Visible = false;
                        this.imgMalPago.Visible = true;
                        this.lblNewPagoResult.Text = "NO SE HA PODIDO AGREGAR EL MOVIMIENTO DE BANCO";
                    }
                }
            this.txtAbono.Text = this.txtCargo.Text = this.txtNombrePago.Text = this.txtFacturaLarguillo.Text = this.txtChequeNum.Text = this.txtChequeNombre.Text = "";
            this.grdvProNotasVenta.DataBind();
            this.detailsViewTotales.DataBind();
            this.grvPagos.DataBind();
            this.chkMostrarAgregarPago.Checked = false;
            this.divAgregarNuevoPago.Attributes.Add("style", this.chkMostrarAgregarPago.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
           

        }

        protected void grvPagos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {

                    if (this.grvPagos.DataKeys[e.Row.RowIndex]["movimientoID"].ToString() != "")
                    {
                        Label lbl = (Label)e.Row.FindControl("Label10");
                        lbl.Text = "";
                        lbl = (Label)e.Row.FindControl("Label11");
                        lbl.Text = "";
                        lbl = (Label)e.Row.FindControl("Label9");
                        lbl.Text = "EFECTIVO";
                        String query = "SELECT cargo FROM MovimientosCaja WHERE movimientoID=@movimientoID";
                        SqlCommand comm = new SqlCommand(query, conn);
                        comm.Parameters.Add("@movimientoID", SqlDbType.Float).Value = int.Parse(this.grvPagos.DataKeys[e.Row.RowIndex]["movimientoID"].ToString());
                        conn.Open();
                        lbl = (Label)e.Row.FindControl("Label12");
                        lbl.Text = string.Format("{0:C2}", Utils.GetSafeFloat(comm.ExecuteScalar().ToString()));

                    }
                    else if (this.grvPagos.DataKeys[e.Row.RowIndex]["movbanID"].ToString() != "")
                    {
                        String query = "SELECT     ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.cargo, MovimientosCuentasBanco.abono, Bancos.nombre, MovimientosCuentasBanco.numCheque ";
                        query += " FROM          MovimientosCuentasBanco INNER JOIN ";
                        query += " ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID ";
                        query += " INNER JOIN ";
                        query += " CuentasDeBanco ON MovimientosCuentasBanco.cuentaID = CuentasDeBanco.cuentaID INNER JOIN ";
                        query += " Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID ";

                        query += " where MovimientosCuentasBanco.movbanID=@movbanID";
                        SqlCommand comm = new SqlCommand(query, conn);
                        conn.Open();
                        comm.Parameters.Add("@movbanID", SqlDbType.Int).Value = int.Parse(this.grvPagos.DataKeys[e.Row.RowIndex]["movbanID"].ToString());
                        SqlDataReader rd = comm.ExecuteReader(); ;

                        //if columns are added/ removed please update the index.
                        if (rd.HasRows && rd.Read())
                        {
                            Label lbl = (Label)e.Row.FindControl("Label10");

                            lbl.Text = rd["numCheque"].ToString() != "0" ? rd["numCheque"].ToString() : "";

                            lbl = (Label)e.Row.FindControl("Label9");
                            lbl.Text = rd["Concepto"].ToString();
                            lbl = (Label)e.Row.FindControl("Label12");
                            lbl.Text = string.Format("{0:c2}", Utils.GetSafeFloat(rd["cargo"].ToString()) - Utils.GetSafeFloat(rd["abono"].ToString()));
                            lbl = (Label)e.Row.FindControl("Label11");
                            lbl.Text = rd["nombre"].ToString();
                        }

                    }

                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "error al meter js a controles en row", ref ex);

                }
                finally
                {

                    conn.Close();
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[6].Text = "TOTAL";
                e.Row.Cells[7].Text = string.Format("{0:C2}", sumaMontos());
            }
        }
        protected double sumaMontos()
        {
            Label lbl;
            double total = 0.0;
            foreach (GridViewRow row in this.grvPagos.Rows)
            {
                lbl = (Label)row.FindControl("Label12");
                total += Utils.GetSafeFloat(lbl.Text.Replace("$", "").Replace(",", ""));


            }
            return total;
        }

        protected void grdvProNotasVenta_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (this.grdvProNotasVenta.DataKeys[e.NewEditIndex]["ordendetalleID"] != null && !string.IsNullOrEmpty(this.grdvProNotasVenta.DataKeys[e.NewEditIndex]["ordendetalleID"].ToString()))
            {
                this.grdvProNotasVenta.EditIndex = e.NewEditIndex;
                this.grdvProNotasVenta.DataBind();
            }
            else
            {
                this.grdvProNotasVenta.EditIndex =-1;
                this.grdvProNotasVenta.DataBind();
            }
              
        }

        protected void grdvProNotasVenta_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
           if (this.grdvProNotasVenta.DataKeys[e.RowIndex]["ordendetalleID"] != null && !string.IsNullOrEmpty(this.grdvProNotasVenta.DataKeys[e.RowIndex]["ordendetalleID"].ToString()))
           {  

                string FolioFactura = "", fechaFactura = "";
                double descFactura= 0, descDel = 0;
                FolioFactura = ((TextBox)(this.grdvProNotasVenta.Rows[e.RowIndex].FindControl("txtNumFactura"))).Text;
                fechaFactura  = ((TextBox)(this.grdvProNotasVenta.Rows[e.RowIndex].FindControl("txtFechaFactura"))).Text;
                double.TryParse(((TextBox)(this.grdvProNotasVenta.Rows[e.RowIndex].FindControl("txtDescuento"))).Text,out descFactura);
                double.TryParse(((TextBox)(this.grdvProNotasVenta.Rows[e.RowIndex].FindControl("txtDescuentoDel"))).Text, out descDel);
                    
                SqlConnection conUpdate = new SqlConnection(myConfig.ConnectionInfo);
                string sql = "update Orden_de_entrada_detalle set folioFactura = @folioFactura, ";
                if(!string.IsNullOrEmpty(fechaFactura) && fechaFactura != "" )
                    sql += "fechaFactura=@fechaFactura, ";
                sql += "descFactura = @descFactura, descDel = @descDel where ordenDetalleID = @ordenDetalleID";
                SqlCommand cmdUpdate = new SqlCommand(sql, conUpdate);
                try
                {
                    conUpdate.Open();
                    cmdUpdate.Parameters.Add("@folioFactura", SqlDbType.NVarChar).Value = FolioFactura;
                    cmdUpdate.Parameters.Add("@fechaFactura", SqlDbType.NVarChar).Value = Utils.converttoLongDBFormat(fechaFactura);
                    cmdUpdate.Parameters.Add("@descFactura", SqlDbType.Float).Value = descFactura/100;
                    cmdUpdate.Parameters.Add("@descDel", SqlDbType.Float).Value = descDel/100;
                    cmdUpdate.Parameters.Add("@ordenDetalleID", SqlDbType.Int).Value = int.Parse(this.grdvProNotasVenta.DataKeys[e.RowIndex]["ordendetalleID"].ToString());
                    cmdUpdate.ExecuteNonQuery();
                    this.grdvProNotasVenta.EditIndex = -1;
                    this.grdvProNotasVenta.DataBind();
                    this.detailsViewTotales.DataBind();
                    this.grvPagos.DataBind();
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "UPDATING DETALLE ORDEN ENTRADA", ref ex);
                }
                finally
                {
                    conUpdate.Close();
                }
        }

        }

        protected void grdvProNotasVenta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
//             if (e.Row.RowType != DataControlRowType.DataRow)
//                 return;
//             if(this.grdvProNotasVenta.DataKeys[e.Row.RowIndex]["ordendetalleID"] == null  || string.IsNullOrEmpty(this.grdvProNotasVenta.DataKeys[e.Row.RowIndex]["ordendetalleID"].ToString()))
//             {
//                 this.grdvProNotasVenta.Rows[e.Row.RowIndex].Cells[0].Controls[0].Visible = false;
//             }
        }

        protected void dvNotaDeCredito_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            try
            {
                e.Values["proveedorID"] = this.ddlProveedores.SelectedItem.Value;
                e.Values["cicloID"] = this.ddlCiclos.SelectedItem.Value;
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error inserting nota de credito", ex);
                this.lblNotaDeCreditoResult.Text = ex.ToString();
            }
        }

        protected void dvNotaDeCredito_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error inserting nota de credito", e.Exception);
                e.ExceptionHandled = true;
            }
            if (e.AffectedRows > 0)
            {
                this.lblNotaDeCreditoResult.Text = "SE HA AGREGADO LA NOTA DE CREDITO CORRECTAMENTE";
            }
            else
            {
                this.lblNotaDeCreditoResult.Text = "LA NOTA DE CREDITO NO PUDO SER AGREGADA.";
            }
        }

    }
}
