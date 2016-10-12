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
    public partial class frmListChequesProductor : Garibay.BasePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (this.panelMensaje.Visible) {
                this.panelMensaje.Visible = false;
            }
               if(!IsPostBack)
            {
                
                      this.grdvCheques.DataBind();
                
                if (Request.QueryString["data"] != null&&this.loadqueryStrings(Request.QueryString["data"].ToString())&&myQueryStrings["chequeProductorID"]!=null)
                {
                    this.txtIdToModify.Text = myQueryStrings["chequeProductorID"].ToString();
                    this.CargaDatosCheque(myQueryStrings["chequeProductorID"].ToString());
                    
                    
                }
                else
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURASDIESEL, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "SE VISITÓ LA PAGINA DE LA LISTA DE FACTURA DIESEL");
                }
                    
         

               }
               this.addjsTocontrols();
        }

        protected void addjsTocontrols(){
            this.divMovimientos.Attributes.Add("style", this.chkCobrado.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divPagoMovCaja.Attributes.Add("style", this.rdbCaja.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.divMovBanco.Attributes.Add("style", this.rdbBanco.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");

            string sOnchange = "ShowHideDivOnChkBox('";
            sOnchange += this.chkCobrado.ClientID + "','";
            sOnchange += this.divMovimientos.ClientID + "')";
            this.chkCobrado.Attributes.Add("onClick", sOnchange);

            sOnchange = "ShowHideDivOnChkBox2('";
            sOnchange += this.rdbCaja.ClientID + "','";
            sOnchange += this.divPagoMovCaja.ClientID + "','";
            sOnchange += this.divMovBanco.ClientID + "')";
            this.rdbCaja.Attributes.Add("onchange", sOnchange);

            sOnchange = "ShowHideDivOnChkBox2('";
            sOnchange += this.rdbBanco.ClientID + "','";
            sOnchange += this.divMovBanco.ClientID + "','";
            sOnchange += this.divPagoMovCaja.ClientID + "')";
            this.rdbBanco.Attributes.Add("onchange", sOnchange);
        }
        protected void grdvCheques_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.sqlCheques.DeleteParameters.Add("@chequeRecibidoID", System.TypeCode.Int32, e.Keys[0].ToString());
        }

        protected void grdvCheques_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton linkDel = (LinkButton)e.Row.FindControl("lnkDel");
            if (linkDel != null)
            {
                
                linkDel.Attributes.Add("onclick", "return confirm('¿Desea eliminar el Cheque número: " + this.grdvCheques.DataKeys[e.Row.RowIndex]["numcheque"].ToString() + "?')");
            }
            

        }

        protected void grdvCheques_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CheckBox aux = (CheckBox)this.grdvCheques.Rows[this.grdvCheques.SelectedIndex].Cells[7].Controls[0];
            //cobAnterior = this.chkCobrado.Checked = aux.Checked;
            //this.txtNumCheq.Text = this.grdvCheques.SelectedDataKey["numcheque"].ToString();
            //this.txtName.Text = this.grdvCheques.SelectedDataKey["ANombreDe"].ToString();
            //this.txtMonto.Text = this.grdvCheques.SelectedDataKey["monto"].ToString();
            //this.pnlModCheque.Visible = true;
        }
        protected String GetURLOpen(string id)
        {
            
            
            return "~/frmListChequesProductor.aspx" + Utils.GetEncriptedQueryString("chequeProductorID=" + id);
        }

        protected void CargaDatosCheque(string id)
        {

            this.pnlModCheque.Visible = true;
            SqlConnection con = new SqlConnection(myConfig.ConnectionInfo);
            string query = " SELECT ChequesRecibidos.numcheque, ChequesRecibidos.ANombreDe, ChequesRecibidos.fecha, ChequesRecibidos.monto, ChequesRecibidos.fechacobrado, ChequesRecibidos.cobrado, ChequesRecibidos.chequeRecibidoID, ChequesRecibidos.userID, ChequesRecibidos.bancoID FROM ChequesRecibidos WHERE ChequesRecibidos.chequeRecibidoID=@chequeRecibidoID";

                
            try{
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@chequeRecibidoID", SqlDbType.Int).Value = int.Parse(id);
           
                con.Open();
                SqlDataReader read=cmd.ExecuteReader();
                if (read.HasRows && read.Read())
                {
                    this.txtNumCheq.Text = read["numcheque"].ToString();
                    this.txtName.Text = read["ANombreDe"].ToString();
                    this.txtMonto.Text = read["monto"].ToString();
                    this.txtFechaCobrado.Text = Utils.converttoshortFormatfromdbFormat(read["fechacobrado"].ToString());
                    this.txtFecha.Text = read["fecha"].ToString();
                    this.ddlBancoOrigen.DataBind();
                    this.ddlBancoOrigen.SelectedValue = read["bancoID"].ToString();
                    this.chkCobrado.Checked = (Boolean)read["cobrado"];
                    if (this.chkCobrado.Checked)
                    {
                        string queryRelacion = "SELECT movCajaID, chequeRecibidoID FROM ChequesRecibidos_MovimientosCaja where chequeRecibidoID=@chequeRecibidoID";
                        cmd = new SqlCommand(queryRelacion, con);
                cmd.Parameters.Add("@chequeRecibidoID", SqlDbType.Int).Value = int.Parse(id);
           
                
                        read.Dispose();
                read=cmd.ExecuteReader();
                if (read.HasRows && read.Read())//SI ES MOVIMIENTO DE CAJA 
                {
                    this.rdbCaja.Checked = true;
                    this.rdbBanco.Checked = false;
                    this.txtTipoPago.Text = "CAJA";
                    queryRelacion = " SELECT    MovimientosCaja.*, catalogoMovimientosBancos.grupoCatalogoID FROM catalogoMovimientosBancos INNER JOIN  MovimientosCaja ON MovimientosCaja.catalogoMovBancoID=catalogoMovimientosBancos.catalogoMovBancoID where movimientoID=@movimientoID";

                      
                    cmd = new SqlCommand(queryRelacion, con);
                    cmd.Parameters.Add("@movimientoID", SqlDbType.Int).Value = read["movCajaID"];

                    read.Dispose();
                    read = cmd.ExecuteReader();
                    if (read.HasRows && read.Read())
                    {
                        //llenar campos del movimiento de Caja
                        this.ddlPagosBodegas.SelectedValue = read["bodegaID"].ToString();
                        this.txtFechaCobrado.Text = read["fecha"].ToString();
                        
                        this.drpdlGrupoCatalogosCajaChica.DataBind();
                        this.drpdlGrupoCatalogosCajaChica.SelectedValue = read["grupoCatalogoID"].ToString();

                        this.drpdlCatalogocuentaCajaChica.DataBind();
                        this.drpdlCatalogocuentaCajaChica.SelectedValue = read["catalogoMovBancoID"].ToString();
                        if (read["subCatalogoMovBancoID"] != null && read["subCatalogoMovBancoID"].ToString() != "" && read["subCatalogoMovBancoID"].ToString() != "-1")
                        {
                            
                            this.drpdlSubcatalogoCajaChica.SelectedValue = read["subCatalogoMovBancoID"].ToString();
                        }
                        //this.drpdlSubcatalogoCajaChica.SelectedValue = read["subCatalogoMovBancoID"].ToString();
                        
                        


                    }

                }
                else {// es movimiento de banco

                    queryRelacion = "SELECT     chequeRecibidoID, movbanID FROM  ChequesRecibidos_MovBancos where chequeRecibidoID=@chequeRecibidoID";
                    cmd = new SqlCommand(queryRelacion, con);
                    cmd.Parameters.Add("@chequeRecibidoID", SqlDbType.Int).Value = int.Parse(id);
                    this.txtTipoPago.Text = "BANCO";
                    
                    read.Dispose();
                    read = cmd.ExecuteReader();
                    this.rdbCaja.Checked = false;
                    this.rdbBanco.Checked = true;
                    if (read.HasRows && read.Read())
                    {
                        queryRelacion = " SELECT  MovimientosCuentasBanco.*, catalogoMovimientosBancos.grupoCatalogoID FROM   MovimientosCuentasBanco INNER JOIN catalogoMovimientosBancos ON MovimientosCuentasBanco.catalogoMovBancoFiscalID = catalogoMovimientosBancos.catalogoMovBancoID  where MovimientosCuentasBanco.movbanID=@movbanID";
                    cmd = new SqlCommand(queryRelacion, con);
                    cmd.Parameters.Add("@movbanID", SqlDbType.Int).Value = int.Parse(read["movbanID"].ToString());
                    read.Dispose();
                    read = cmd.ExecuteReader();
                    if (read.HasRows && read.Read())
                    {
                        //llenar campos del movimiento de banco
                        this.cmbCuentaPago.SelectedValue=read["cuentaID"].ToString();
                        this.drpdlGrupoCuentaFiscal.SelectedValue=read["ConceptoMovCuentaID"].ToString();
                        this.txtFechaCobrado.Text=read["fecha"].ToString();

                        this.drpdlGrupoCuentaFiscal.DataBind();
                        this.drpdlGrupoCuentaFiscal.SelectedValue = read["grupoCatalogoID"].ToString();

                        this.drpdlCatalogocuentafiscalPago.DataBind();

                        this.drpdlCatalogocuentafiscalPago.SelectedValue = read["catalogoMovBancoFiscalID"].ToString();
                        this.drpdlSubcatalogofiscalPago.DataBind();
                        this.drpdlSubcatalogofiscalPago.SelectedValue = read["subCatalogoMovBancoFiscalID"].ToString();
                        

                    }
                }
                
                }
                    }
                    this.addjsTocontrols();
                    this.txtCobAnterior.Text = this.chkCobrado.Checked ? "1" : "0";

                }
            }
            catch (Exception ex)
            {
               


            }
            
        }

        protected void btnModificarCheque_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                this.pnlModCheque.Visible = true;
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.CommandText = "UPDATE  ChequesRecibidos ";
                sqlComm.CommandText += "SET numcheque =@numcheque, ANombreDe =@ANombreDe, fecha =@fecha, monto = @monto, fechacobrado =@fechacobrado, bancoID =@bancoID, cobrado =@cobrado, userID =@userID ";
                sqlComm.CommandText += "WHERE chequeRecibidoID=@chequeRecibidoID";

                sqlComm.Parameters.Add("@numcheque", SqlDbType.VarChar).Value = this.txtNumCheq.Text;
                sqlComm.Parameters.Add("@ANombreDe", SqlDbType.VarChar).Value = this.txtName.Text;
                sqlComm.Parameters.Add("@fecha", SqlDbType.DateTime).Value = (DateTime)DateTime.Parse(this.txtFecha.Text);
                sqlComm.Parameters.Add("@monto", SqlDbType.Float).Value = double.Parse(this.txtMonto.Text);

                if(this.txtCobAnterior.Text=="0"&&this.chkCobrado.Checked)
                {

                    sqlComm.Parameters.Add("@fechacobrado", SqlDbType.DateTime).Value =Utils.Now;

                }
                else if (this.txtCobAnterior.Text == "1" && this.chkCobrado.Checked)
                {
                    sqlComm.Parameters.Add("@fechacobrado", SqlDbType.DateTime).Value = (DateTime)DateTime.Parse(this.txtFechaCobrado.Text);
                }
                else if (!this.chkCobrado.Checked)
                {
                    sqlComm.Parameters.Add("@fechacobrado", SqlDbType.DateTime).Value = DBNull.Value;
                }

                sqlComm.Parameters.Add("@bancoID", SqlDbType.Int).Value = int.Parse(this.ddlBancoOrigen.SelectedValue);


                sqlComm.Parameters.Add("@cobrado", SqlDbType.NVarChar).Value = this.chkCobrado.Checked;
                sqlComm.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;

                sqlComm.Parameters.Add("@chequeRecibidoID", SqlDbType.Int).Value = int.Parse(this.txtIdToModify.Text);
                
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                if (sqlComm.ExecuteNonQuery() == 1)
                {
                    if (this.chkCobrado.Checked&&this.txtCobAnterior.Text=="1")
                    {
                        
                        if (this.rdbCaja.Checked&&this.txtTipoPago.Text=="BANCO")//cambio el tipo de pago de banco a Caja
                        {
                            //insertamos el movimiento de caja chica
                            string query = "  INSERT INTO MovimientosCaja (cicloID, userID, nombre, abono, storeTS, updateTS, fecha, bodegaID, cobrado, catalogoMovBancoID, subCatalogoMovBancoID) VALUES     (@cicloID, @userID, @nombre, @abono, @storeTS, @updateTS, @fecha, @bodegaID, @cobrado, @catalogoMovBancoID, @subCatalogoMovBancoID);";
                            query += " SELECT NewID = SCOPE_IDENTITY();";
                            SqlCommand cmdIns = new SqlCommand(query, sqlConn);

                            cmdIns.Parameters.Add("@cicloID", SqlDbType.Int).Value = 4;
                            cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                            cmdIns.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtName.Text;
                            
                            cmdIns.Parameters.Add("@abono", SqlDbType.Money).Value = double.Parse(this.txtMonto.Text);
                            cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.Now;
                            cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.Now;
                            cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = (DateTime)DateTime.Parse(this.txtFechaCobrado.Text);
                            cmdIns.Parameters.Add("@bodegaID", SqlDbType.Int).Value = int.Parse(this.ddlPagosBodegas.SelectedValue);
                            cmdIns.Parameters.Add("@cobrado", SqlDbType.Bit).Value = 1;
                            cmdIns.Parameters.Add("@catalogoMovBancoID", SqlDbType.Int).Value = int.Parse(this.drpdlCatalogocuentaCajaChica.SelectedValue);
                            cmdIns.Parameters.Add("@subCatalogoMovBancoID", SqlDbType.Int).Value = this.drpdlSubcatalogoCajaChica.SelectedValue!=""?int.Parse(this.drpdlSubcatalogoCajaChica.SelectedValue):-1;
                            
                            
                            
                             int movcajaID = int.Parse(cmdIns.ExecuteScalar().ToString());
                             //insertamos la relacion
                             query = "INSERT INTO ChequesRecibidos_MovimientosCaja (chequeRecibidoID, movCajaID) VALUES   (@chequeRecibidoID, @movCajaID)";

                             cmdIns.Parameters.Clear();
                             cmdIns.CommandText = query;
                             cmdIns.Parameters.Add("@chequeRecibidoID", SqlDbType.Int).Value = int.Parse((this.txtIdToModify.Text));
                             cmdIns.Parameters.Add("@movCajaID", SqlDbType.Int).Value = movcajaID;
                             cmdIns.ExecuteNonQuery();

                             //borramos la relacion , que contiene un trigger que elimina el movimiento

                             query = "DELETE FROM ChequesRecibidos_MovBancos  WHERE chequeRecibidoID=@chequeRecibidoID";
                             cmdIns.Parameters.Clear();
                             cmdIns.CommandText = query;
                             cmdIns.Parameters.Add("@chequeRecibidoID", SqlDbType.Int).Value = int.Parse(this.txtIdToModify.Text);
                            
                             cmdIns.ExecuteNonQuery();
                            

                                 



                        }
                        else if (this.rdbCaja.Checked && this.txtTipoPago.Text == "CAJA")//se actualizo el movimiento de caja
                        {




                        string queryRelacion = "SELECT movCajaID, chequeRecibidoID FROM ChequesRecibidos_MovimientosCaja where chequeRecibidoID=@chequeRecibidoID";
                        SqlCommand cmd = new SqlCommand(queryRelacion, sqlConn);
                        cmd.Parameters.Add("@chequeRecibidoID", SqlDbType.Int).Value = int.Parse(this.txtIdToModify.Text);
           
                
                        
                        SqlDataReader read=cmd.ExecuteReader();
                        if (read.HasRows && read.Read())//SI ES MOVIMIENTO DE CAJA 
                        {

                            string query = "  UPDATE MovimientosCaja SET cicloID=@cicloID, userID=@userID, nombre=@nombre, abono=@abono, updateTS=@updateTS, fecha=@fecha, bodegaID=@bodegaID, cobrado=@cobrado, catalogoMovBancoID=@catalogoMovBancoID, subCatalogoMovBancoID = @subCatalogoMovBancoID where movimientoID=@movimientoID;";
                            cmd = new SqlCommand(query, sqlConn);
                            cmd.Parameters.Add("@cicloID", SqlDbType.Int).Value = 4;
                            cmd.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtName.Text;
                            cmd.Parameters.Add("@abono", SqlDbType.Money).Value = double.Parse(this.txtMonto.Text);
                            cmd.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.Now;
                            cmd.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.Now;
                            cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = (DateTime)DateTime.Parse(this.txtFechaCobrado.Text);
                            cmd.Parameters.Add("@bodegaID", SqlDbType.Int).Value = int.Parse(this.ddlPagosBodegas.SelectedValue);
                            cmd.Parameters.Add("@cobrado", SqlDbType.Bit).Value = 1;
                            cmd.Parameters.Add("@catalogoMovBancoID", SqlDbType.Int).Value = int.Parse(this.drpdlCatalogocuentaCajaChica.SelectedValue);
                            cmd.Parameters.Add("@subCatalogoMovBancoID", SqlDbType.Int).Value = this.drpdlSubcatalogoCajaChica.SelectedValue != "" ? int.Parse(this.drpdlSubcatalogoCajaChica.SelectedValue) : -1;
                            cmd.Parameters.Add("@movimientoID", SqlDbType.Int).Value = int.Parse(read["movCajaID"].ToString());
                            read.Dispose();
                            read.Close();
                            cmd.ExecuteNonQuery();


                        }
                        }
                        else if(this.rdbBanco.Checked && this.txtTipoPago.Text == "CAJA")//se cambio el movimiento por un movimiento de banco
                        {
                            //insertamos el mov de banco
                            string qryIns = "INSERT INTO MovimientosCuentasBanco (cuentaID, ConceptoMovCuentaID, fecha, abono, userID, nombre , numCheque, chequeNombre, chequecobrado, fechacobrado, Observaciones, catalogoMovBancoFiscalID, subCatalogoMovBancoFiscalID, storeTS, updateTS) VALUES ";
                            qryIns += "                                         ( @cuentaID, @ConceptoMovCuentaID, @fecha,@abono, @userID, @nombre,@numCheque, @chequeNombre,@chequecobrado, @fechacobrado, @Observaciones, @catalogoMovBancoFiscalID, @subCatalogoMovBancoFiscalID, @storeTS, @updateTS);";
                            qryIns += "SELECT NewID = SCOPE_IDENTITY();";
                            SqlCommand cmdIns = new SqlCommand(qryIns, sqlConn);
                            cmdIns.Parameters.Clear();
                            cmdIns.CommandText = qryIns;
                            
                            //cmdIns.Parameters.Add("@cicloID", SqlDbType.Int).Value = cicloID;
                            cmdIns.Parameters.Add("@cuentaID", SqlDbType.Int).Value = int.Parse(this.cmbCuentaPago.SelectedValue);
                            cmdIns.Parameters.Add("@ConceptoMovCuentaID", SqlDbType.Int).Value = 3;
                            cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = (DateTime)DateTime.Parse(this.txtFechaCobrado.Text);
                            
                            cmdIns.Parameters.Add("@abono", SqlDbType.Money).Value = double.Parse(this.txtMonto.Text);
                            cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                            cmdIns.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtName.Text;
                            
                            cmdIns.Parameters.Add("@numCheque", SqlDbType.Int).Value = -1;
                            cmdIns.Parameters.Add("@chequeNombre", SqlDbType.NVarChar).Value = this.txtName.Text;
                            
                            cmdIns.Parameters.Add("@chequecobrado", SqlDbType.Bit).Value = 0;
                            cmdIns.Parameters.Add("@fechacobrado", SqlDbType.DateTime).Value = (DateTime)DateTime.Parse(this.txtFechaCobrado.Text); ;
                            cmdIns.Parameters.Add("@Observaciones", SqlDbType.Text).Value = "CHEQUE DE PRODUCTOR";
                            cmdIns.Parameters.Add("@catalogoMovBancoFiscalID", SqlDbType.Int).Value = int.Parse(this.drpdlCatalogocuentafiscalPago.SelectedValue);
                            cmdIns.Parameters.Add("@subCatalogoMovBancoFiscalID", SqlDbType.Int).Value = this.drpdlSubcatalogofiscalPago.SelectedValue != "" ? int.Parse(this.drpdlSubcatalogofiscalPago.SelectedValue) : -1;
                            cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.Now;
                            cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.Now;



                            //insertamos la relacion
            
                                
                            int movbanid = int.Parse(cmdIns.ExecuteScalar().ToString());
                            string query = "INSERT INTO ChequesRecibidos_MovBancos     (chequeRecibidoID, movbanID) VALUES     (@chequeRecibidoID, @movbanID)";
                            cmdIns.Parameters.Clear();
                            cmdIns.CommandText = query;
                            cmdIns.Parameters.Add("@chequeRecibidoID", SqlDbType.Int).Value = int.Parse(this.txtIdToModify.Text);
                            cmdIns.Parameters.Add("@movbanID", SqlDbType.Int).Value = movbanid;
                            cmdIns.ExecuteNonQuery();

                            //borramos la relacion , que contiene un trigger que elimina el movimiento
                            query = "DELETE FROM ChequesRecibidos_MovimientosCaja WHERE chequeRecibidoID=@chequeRecibidoID";
                            cmdIns.Parameters.Clear();
                            cmdIns.CommandText = query;
                            cmdIns.Parameters.Add("@chequeRecibidoID", SqlDbType.Int).Value = int.Parse((this.txtIdToModify.Text));

                            cmdIns.ExecuteNonQuery();

                        }
                        else if(this.rdbBanco.Checked && this.txtTipoPago.Text == "BANCO")// se actualizo el movimiento de banco
                        {

                            string queryRelacion = "SELECT movbanID FROM ChequesRecibidos_MovBancos where chequeRecibidoID=@chequeRecibidoID";
                        SqlCommand cmdIns = new SqlCommand(queryRelacion, sqlConn);
                        cmdIns.Parameters.Add("@chequeRecibidoID", SqlDbType.Int).Value = int.Parse(this.txtIdToModify.Text);
           
                        SqlDataReader read=cmdIns.ExecuteReader();
                        if (read.HasRows && read.Read())//SI ES MOVIMIENTO DE banco 
                        {

                            string query = "UPDATE  MovimientosCuentasBanco SET cuentaID=@cuentaID, ConceptoMovCuentaID=@ConceptoMovCuentaID, fecha=@fecha, abono=@abono, userID=@userID, nombre=@nombre, numCheque=@numCheque, chequeNombre=@chequeNombre, chequecobrado=@chequecobrado, fechacobrado=@fechacobrado, Observaciones=@Observaciones, catalogoMovBancoFiscalID=@catalogoMovBancoFiscalID, subCatalogoMovBancoFiscalID=@subCatalogoMovBancoFiscalID, catalogoMovBancoInternoID=@catalogoMovBancoInternoID, subCatalogoMovBancoInternoID=@subCatalogoMovBancoInternoID,  updateTS=@updateTS where  movbanID=@movbanID";
                            cmdIns = new SqlCommand(query, sqlConn);
                            cmdIns.Parameters.Add("@cuentaID", SqlDbType.Int).Value = int.Parse(this.cmbCuentaPago.SelectedValue);
                            cmdIns.Parameters.Add("@ConceptoMovCuentaID", SqlDbType.Int).Value = 3;
                            cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = (DateTime)DateTime.Parse(this.txtFechaCobrado.Text);

                            cmdIns.Parameters.Add("@abono", SqlDbType.Money).Value = double.Parse(this.txtMonto.Text);
                            cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                            cmdIns.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtName.Text;

                            cmdIns.Parameters.Add("@numCheque", SqlDbType.Int).Value = -1;
                            cmdIns.Parameters.Add("@chequeNombre", SqlDbType.NVarChar).Value = this.txtName.Text;
                            
                            cmdIns.Parameters.Add("@chequecobrado", SqlDbType.Bit).Value = 0;
                            cmdIns.Parameters.Add("@fechacobrado", SqlDbType.DateTime).Value = (DateTime)DateTime.Parse(this.txtFechaCobrado.Text); ;
                            cmdIns.Parameters.Add("@Observaciones", SqlDbType.Text).Value = "CHEQUE DE PRODUCTOR";
                            cmdIns.Parameters.Add("@catalogoMovBancoFiscalID", SqlDbType.Int).Value = int.Parse(this.drpdlCatalogocuentafiscalPago.SelectedValue);
                            cmdIns.Parameters.Add("@subCatalogoMovBancoFiscalID", SqlDbType.Int).Value = this.drpdlSubcatalogofiscalPago.SelectedValue != "" ? int.Parse(this.drpdlSubcatalogofiscalPago.SelectedValue) : -1;
                            
                            cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.Now;
                            cmdIns.Parameters.Add("@movbanID", SqlDbType.Int).Value = int.Parse(read["movbanID"].ToString());
                            read.Dispose();
                            read.Close();
                            cmdIns.ExecuteNonQuery();


                        }
                        }
                        
                    }
                    else if(!this.chkCobrado.Checked&&this.txtCobAnterior.Text=="1")//se marco como no cobrado
                    {
                        SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                        try
                        {
                            conn.Open();
                            SqlCommand comm = new SqlCommand("ELIMINA_RELACION_DE_PAGOS_DE_CHEQUE_RECIBIDO", conn);
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Parameters.Add("@CHEQUEPRODUCTORID", SqlDbType.Int).Value = int.Parse(this.txtIdToModify.Text);
                            comm.ExecuteNonQuery();
                           
                           
                        }
                        catch (Exception ex)
                        {
                           
                        }
                        finally
                        {
                            conn.Close();
                        }

                    }
                    else if (this.chkCobrado.Checked && this.txtCobAnterior.Text == "0")//se marco como no cobrado
                    {

                        if (this.rdbCaja.Checked)//se cobro como pago a Caja
                        {
                            //insertamos el movimiento de caja chica
                            string query = "  INSERT INTO MovimientosCaja (cicloID, userID, nombre, abono, storeTS, updateTS, fecha, bodegaID, cobrado, catalogoMovBancoID, subCatalogoMovBancoID) VALUES     (@cicloID, @userID, @nombre, @abono, @storeTS, @updateTS, @fecha, @bodegaID, @cobrado, @catalogoMovBancoID, @subCatalogoMovBancoID);";
                            query += " SELECT NewID = SCOPE_IDENTITY();";
                            SqlCommand cmdIns = new SqlCommand(query, sqlConn);

                            cmdIns.Parameters.Add("@cicloID", SqlDbType.Int).Value = 4;
                            cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                            cmdIns.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtName.Text;

                            cmdIns.Parameters.Add("@abono", SqlDbType.Money).Value = double.Parse(this.txtMonto.Text);
                            cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.Now;
                            cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.Now;
                            cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = (DateTime)DateTime.Parse(DateTime.Parse(this.txtFechaCobrado.Text).ToString("dd/MM/yyyy"));
                            cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Parse(this.txtFechaCobrado.Text);
                            cmdIns.Parameters.Add("@bodegaID", SqlDbType.Int).Value = int.Parse(this.ddlPagosBodegas.SelectedValue);
                            cmdIns.Parameters.Add("@cobrado", SqlDbType.Bit).Value = 1;
                            cmdIns.Parameters.Add("@catalogoMovBancoID", SqlDbType.Int).Value = int.Parse(this.drpdlCatalogocuentaCajaChica.SelectedValue);
                            cmdIns.Parameters.Add("@subCatalogoMovBancoID", SqlDbType.Int).Value = this.drpdlSubcatalogoCajaChica.SelectedValue != "" ? int.Parse(this.drpdlSubcatalogoCajaChica.SelectedValue) : -1;



                            int movcajaID = int.Parse(cmdIns.ExecuteScalar().ToString());
                            //insertamos la relacion
                            query = "INSERT INTO ChequesRecibidos_MovimientosCaja (chequeRecibidoID, movCajaID) VALUES   (@chequeRecibidoID, @movCajaID)";

                            cmdIns.Parameters.Clear();
                            cmdIns.CommandText = query;
                            cmdIns.Parameters.Add("@chequeRecibidoID", SqlDbType.Int).Value = int.Parse((this.txtIdToModify.Text));
                            cmdIns.Parameters.Add("@movCajaID", SqlDbType.Int).Value = movcajaID;
                            cmdIns.ExecuteNonQuery();
                        }else{
                            //insertamos el mov de banco
                            string qryIns = "INSERT INTO MovimientosCuentasBanco (cuentaID, ConceptoMovCuentaID, fecha, abono, userID, nombre , numCheque, chequeNombre, chequecobrado, fechacobrado, Observaciones, catalogoMovBancoFiscalID, subCatalogoMovBancoFiscalID, storeTS, updateTS) VALUES ";
                            qryIns += "                                         ( @cuentaID, @ConceptoMovCuentaID, @fecha,@abono, @userID, @nombre,@numCheque, @chequeNombre,@chequecobrado, @fechacobrado, @Observaciones, @catalogoMovBancoFiscalID, @subCatalogoMovBancoFiscalID, @storeTS, @updateTS);";
                            qryIns += "SELECT NewID = SCOPE_IDENTITY();";
                            SqlCommand cmdIns = new SqlCommand(qryIns, sqlConn);
                            cmdIns.Parameters.Clear();
                            cmdIns.CommandText = qryIns;

                            //cmdIns.Parameters.Add("@cicloID", SqlDbType.Int).Value = cicloID;
                            cmdIns.Parameters.Add("@cuentaID", SqlDbType.Int).Value = int.Parse(this.cmbCuentaPago.SelectedValue);
                            cmdIns.Parameters.Add("@ConceptoMovCuentaID", SqlDbType.Int).Value = 3;
                            cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = (DateTime)DateTime.Parse(this.txtFechaCobrado.Text);

                            cmdIns.Parameters.Add("@abono", SqlDbType.Money).Value = double.Parse(this.txtMonto.Text);
                            cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                            cmdIns.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtName.Text;

                            cmdIns.Parameters.Add("@numCheque", SqlDbType.Int).Value = -1;
                            cmdIns.Parameters.Add("@chequeNombre", SqlDbType.NVarChar).Value = this.txtName.Text;

                            cmdIns.Parameters.Add("@chequecobrado", SqlDbType.Bit).Value = 0;
                            cmdIns.Parameters.Add("@fechacobrado", SqlDbType.DateTime).Value = (DateTime)DateTime.Parse(this.txtFechaCobrado.Text); ;
                            cmdIns.Parameters.Add("@Observaciones", SqlDbType.Text).Value = "CHEQUE DE PRODUCTOR";
                            cmdIns.Parameters.Add("@catalogoMovBancoFiscalID", SqlDbType.Int).Value = int.Parse(this.drpdlCatalogocuentafiscalPago.SelectedValue);
                            cmdIns.Parameters.Add("@subCatalogoMovBancoFiscalID", SqlDbType.Int).Value = this.drpdlSubcatalogofiscalPago.SelectedValue != "" ? int.Parse(this.drpdlSubcatalogofiscalPago.SelectedValue) : -1;
                            cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.Now;
                            cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.Now;



                            //insertamos la relacion


                            int movbanid = int.Parse(cmdIns.ExecuteScalar().ToString());
                            string query = "INSERT INTO ChequesRecibidos_MovBancos     (chequeRecibidoID, movbanID) VALUES     (@chequeRecibidoID, @movbanID)";
                            cmdIns.Parameters.Clear();
                            cmdIns.CommandText = query;
                            cmdIns.Parameters.Add("@chequeRecibidoID", SqlDbType.Int).Value = int.Parse(this.txtIdToModify.Text);
                            cmdIns.Parameters.Add("@movbanID", SqlDbType.Int).Value = movbanid;
                            cmdIns.ExecuteNonQuery();


                        }
                    }
            
            

                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CHEQUES, Logger.typeUserActions.UPDATE, this.UserID, "MODIFICÓ EL CHEQUE DEL PRODUCTOR: " + this.txtNumCheq.Text);
                    
                    

                    this.lblMensajetitle.Text = "CHEQUE MODIFICADO EXITOSAMENTE";
                    this.lblMensajeOperationresult.Visible = false;
                    this.lblMensajeException.Visible = false;
                    this.imagenmal.Visible = !(this.imagenbien.Visible = true);
                    this.panelMensaje.Visible = true;
                    this.pnlModCheque.Visible = false;
                    this.grdvCheques.DataBind();
                    
                }
            }
            catch (SqlException ex)
            {
                
                String sTemp = "update Predio SQL Error: " + ex.Message + " DATA: " + ex.Data.ToString();
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, sTemp, this.Request.Url.ToString());
                this.lblMensajetitle.Text = "MODIFICAR CHEQUE";
                this.lblMensajeOperationresult.Text = "NO SE PUDO MODIFICAR EL CHEQUE, ERROR SQL";
                this.lblMensajeException.Text = ex.Message;
            }
            catch (System.Exception ex)
            {
                
                String sTemp = "Update Predio Ex:" + ex.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, sTemp, this.Request.Url.ToString());
                this.lblMensajetitle.Text = "MODIFICAR CHEQUE";
                this.lblMensajeOperationresult.Text = "NO SE PUDO MODIFICAR EL CHEQUE";
                this.lblMensajeException.Text = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }
        }
       
    }

    
}
