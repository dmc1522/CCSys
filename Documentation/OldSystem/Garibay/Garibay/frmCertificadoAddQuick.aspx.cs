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
    public partial class frmCertificadoAddQuick : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.btnModificar.Visible = false;
                this.btnAgregar0.Visible = true;
                int certid = -1, credid = -1;
                this.ddlCFSeguro.DataBind();
                this.drpdlCred.DataBind();
                string msgConf;
                this.drpdlBodega.DataBind();
            //    msgConf = "return alert('¡¡NO OLVIDE ACTUALIZAR LA LISTA DE CERTIFICADOS !! '); window.close(); ";
                JSUtils.closeCurrentWindow(ref this.btnCancelar);

       /*         this.btnCancelar.Attributes.Add("onclick", "<script type=\"text/javascript\">window.close();</script>");*/

                string sOnchange;
                sOnchange = "ShowHideDivOnChkBox('";
                sOnchange += this.chkAsignarClienteVenta.ClientID + "','";
                sOnchange += this.divAsignaraClientedeVenta.ClientID + "')";
                this.chkAsignarClienteVenta.Attributes.Add("onclick", sOnchange);

               
                sOnchange = "ShowHideDivOnChkBox('";
                sOnchange += this.chkAsignarCredito.ClientID + "','";
                sOnchange += this.divAsignaraCredito.ClientID + "')";
                this.chkAsignarCredito.Attributes.Add("onclick", sOnchange);

                JSUtils.AddDisableWhenClick(ref this.btnAgregar0, this);
                JSUtils.AddDisableWhenClick(ref this.btnModificar, this);
               // this.gridCreditos.DataBind();
                this.drpdlClienteVenta.DataBind();
                // CHECAMOS SI HAY QUERYS
                if (Request.QueryString["data"] != null && this.loadqueryStrings(Request.QueryString["data"].ToString())){
                    // CHECAMOS SI ES PARA MODIFICAR UN CERT
                    if(this.myQueryStrings["certID"]!=null && int.TryParse(this.myQueryStrings["certID"].ToString(), out certid) && certid > -1)
                    {
                        this.lblcertID.Text = certid.ToString();
                        this.cargaCert();
                        this.btnAgregar0.Visible = false;
                        this.btnModificar.Visible = true;
                        this.lblAction.Text = "MODIFICAR UN CERTIFICADO";
                        


                    }
                    else{
                        //CHECAMOS SI NOS PASARON UN CREDITOID 
                        if (this.myQueryStrings["credID"] != null && int.TryParse(this.myQueryStrings["credID"].ToString(), out credid) && credid > -1)
                        {
                                this.lblcredID.Text = credid.ToString();
                                this.chkAsignarCredito.Checked=true;
                                this.chkAsignarCredito.Enabled =false; 
                                this.drpdlCred.Enabled = false;
                                this.chkAsignarClienteVenta.Checked = false;
                                this.chkAsignarClienteVenta.Enabled = false;
                                this.drpdlCred.SelectedValue = this.lblcredID.Text;
                                this.lblAction.Text = "EL CERTIFICADO SE AGREGARÁ AL CRÉDITO FINANCIERO CON EL ID: " + credid.ToString();
                               
                             
                          
                        }
                        this.btnModificar.Visible = false;
                        this.btnAgregar0.Visible = true;
                 
                    }
                }

            }
            this.pnlNewMov.Visible = false;
             this.divAsignaraClientedeVenta.Attributes.Add("style", this.chkAsignarClienteVenta.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
             this.divAsignaraCredito.Attributes.Add("style", this.chkAsignarCredito.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");

        }
        protected bool cargaCert(){
            string selquery = " SELECT CredFinCertificados.bodegaID,CredFinCertificados.numCabezas, CredFinCertificados.numdeCertificados, CredFinCertificados.fechaEmision, CredFinCertificados.fechaVencimiento,   CredFinCertificados.KG, CredFinCertificados.Precio, CredFinCertificados.MontoDelCert, CredFinCertificados.productoID, Certificado_Credito_ClienteVenta.CreditoID, " +
                       " CredFinCertificados.empresaCertificadaID, CredFinCertificados.credFinSeguroID, Certificado_Credito_ClienteVenta.ClienteVentaID " +
                       " FROM CreditosFinancieros RIGHT OUTER JOIN " +
                       " Certificado_Credito_ClienteVenta ON CreditosFinancieros.creditoFinancieroID = Certificado_Credito_ClienteVenta.CreditoID RIGHT OUTER JOIN " +
                       " CredFinCertificados ON Certificado_Credito_ClienteVenta.CredFinCertID = CredFinCertificados.CredFinCertID LEFT OUTER JOIN " +
                       " ClientesVentas ON Certificado_Credito_ClienteVenta.ClienteVentaID = ClientesVentas.clienteventaID where CredFinCertificados.credFinCertID = @certID " ;
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdSel = new SqlCommand(selquery, conGaribay);
            conGaribay.Open();
            try{
                cmdSel.Parameters.Clear();
                cmdSel.Parameters.Add("@certID", SqlDbType.Int).Value = int.Parse(this.lblcertID.Text);
                SqlDataReader selrd = cmdSel.ExecuteReader();
                if(!selrd.Read()){
                    throw new Exception("ERROR AL LEER LOS DATOS DEL CERT CON EL ID: " + this.lblcertID.Text);

                }
                this.txtnumCabezas.Text = selrd["numCabezas"].ToString();
                this.drpdlBodega.SelectedValue = selrd["bodegaID"].ToString();
                this.txtNumCertificados.Text = selrd["numdeCErtificados"].ToString();
                this.txtCFFechaEmision.Text = Utils.converttoshortFormatfromdbFormat(selrd["fechaEmision"].ToString());
                this.txtCFFechaVencimiento.Text = Utils.converttoshortFormatfromdbFormat(selrd["fechaVencimiento"].ToString());
                this.txtCFKG.Text = selrd["KG"].ToString();
                this.txtCFPrecio.Text = selrd["Precio"].ToString();
                this.txtCFMonto.Text = selrd["MontoDelCert"].ToString();
                this.drpdlProducto.SelectedValue = selrd["productoID"].ToString();
                if(selrd["CreditoID"]!=null && selrd["CreditoID"].ToString().Length>0){
                    //ESTA RELACIONADO CON UN CREDITO, SELECCIONARÏAMOS EL CREDITO DEL GRID
                    this.chkAsignarCredito.Checked = true;
                    this.drpdlCred.SelectedValue = selrd["CreditoID"].ToString();

                }
                else{
                    if (selrd["ClienteVentaID"] != null && selrd["ClienteVentaID"].ToString().Length > 0)
                    {
                        //ESTA RELACIONADO CON UN CLIENTE DE VENTA
                        this.chkAsignarClienteVenta.Checked = true;
                        this.drpdlClienteVenta.SelectedValue = selrd["ClienteVentaID"].ToString();

                    }
                }
                this.ddlCFSeguro.SelectedValue = selrd["credFinSeguroID"].ToString();
                this.drpdlEmpresa.SelectedValue = selrd["empresaCertificadaID"].ToString();


            }
            catch(Exception ex){
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, this.UserID, "ERROR AL CARGAR DATOS DEL CERTIFICADO. EX: " + ex.Message, this.Request.Url.ToString());
            }

            return true;
        }

        protected void btnAgregar0_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "INSERT INTO CredFinCertificados " +
                         " (numCabezas, bodegaID, numdeCertificados, fechaEmision, fechaVencimiento, KG, Precio, MontoDelCert, credFinSeguroID, userID, productoID, empresaCertificadaID) " +
                         " VALUES        (@numCabezas, @bodegaID, @numdeCertificados,@fechaEmision,@fechaVencimiento,@KG,@Precio,@MontoDelCert,@credFinSeguroID,@userID, @productoID, @empresaCertificadaID)" +
                    " SELECT NewID = SCOPE_IDENTITY();";
               // comm.Parameters.Add("@creditoFinancieroID", SqlDbType.Int).Value = int.Parse(this.txtIDtoMod.Text);
                double cabezas = -1;
                double.TryParse(this.txtnumCabezas.Text,out cabezas);
                comm.Parameters.Add("@numCabezas", SqlDbType.Float).Value = cabezas;
                comm.Parameters.Add("@numdeCertificados", SqlDbType.VarChar).Value = this.txtNumCertificados.Text;
                comm.Parameters.Add("@fechaEmision", SqlDbType.DateTime).Value = DateTime.Parse(this.txtCFFechaEmision.Text);
                comm.Parameters.Add("@fechaVencimiento", SqlDbType.DateTime).Value = DateTime.Parse(this.txtCFFechaVencimiento.Text);
                comm.Parameters.Add("@KG", SqlDbType.Float).Value = double.Parse(this.txtCFKG.Text);
                comm.Parameters.Add("@Precio", SqlDbType.Float).Value = double.Parse(this.txtCFPrecio.Text);
                comm.Parameters.Add("@MontoDelCert", SqlDbType.Float).Value = double.Parse(this.txtCFMonto.Text);
                comm.Parameters.Add("@credFinSeguroID", SqlDbType.Int).Value = int.Parse(this.ddlCFSeguro.SelectedValue);
                comm.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                comm.Parameters.Add("@productoID", SqlDbType.Int).Value = int.Parse(this.drpdlProducto.SelectedValue);
                comm.Parameters.Add("@empresaCertificadaID", SqlDbType.Int).Value = int.Parse(this.drpdlEmpresa.SelectedValue);
                comm.Parameters.Add("@bodegaID", SqlDbType.Int).Value = int.Parse(this.drpdlBodega.SelectedValue);
                int id = int.Parse(comm.ExecuteScalar().ToString());
                if (this.chkAsignarClienteVenta.Checked || this.chkAsignarCredito.Checked)
                {
                    comm.CommandText = "Insert into Certificado_Credito_ClienteVenta(CredFinCertID, ClienteVentaID, CreditoID) values(@CredFinCertID, @ClienteVentaID, @CreditoID) ";
                    comm.Parameters.Clear();
                    comm.Parameters.Add("@CredFinCertID", SqlDbType.Int).Value = id;
                    comm.Parameters.Add("@ClienteVentaID", SqlDbType.Int).Value = this.chkAsignarClienteVenta.Checked ? int.Parse(this.drpdlClienteVenta.SelectedValue) : -1;
                    comm.Parameters.Add("@CreditoID", SqlDbType.Int).Value = this.chkAsignarCredito.Checked ? int.Parse(this.drpdlCred.SelectedValue) : -1;
                    comm.ExecuteNonQuery();
                }
               
                this.pnlNewMov.Visible = true;
                this.lblResult.Text = "SE AGREGÓ EL CERTIFICADO: " + this.txtNumCertificados.Text.ToUpper() + " EXITOSAMENTE.";
                this.txtNumCertificados.Text = "";
                this.txtCFFechaEmision.Text = "";
                this.txtCFFechaVencimiento.Text = "";
                this.txtCFKG.Text = "";
                this.txtCFPrecio.Text = "";
                this.txtCFMonto.Text = "";
                this.ddlCFSeguro.SelectedIndex = 0;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CERTIFICADOS, Logger.typeUserActions.INSERT, this.UserID, "INSERTO EL CERTIFICADO No. : " + id.ToString());
                String sNewUrl = "~/frmCertificadoAddQuick.aspx";
                sNewUrl += Utils.GetEncriptedQueryString("certID=" + id.ToString());
                Response.Redirect(sNewUrl);
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error insertando certificado", ref ex);
                this.lblResult.Text = ex.Message;
                this.pnlNewMov.Visible = true;
                this.imgMal.Visible = true;
                this.imgBien.Visible = false;
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "Update CredFinCertificados set numCabezas = @numCabezas, numdeCertificados = @numdeCertificados, fechaEmision = @fechaEmision, " +
                         " bodegaID = @bodegaID, fechaVencimiento = @fechaVencimiento, KG = @KG, Precio=@Precio, MontoDelCert = @MontoDelCert, credFinSeguroID = @credFinSeguroID, " +
                         " userID = @userID, productoID = @productoID, empresaCertificadaID = @empresaCertificadaID Where CredFinCertificados.credFinCertID = @certID  " ;
                // comm.Parameters.Add("@creditoFinancieroID", SqlDbType.Int).Value = int.Parse(this.txtIDtoMod.Text);
                double cabezas=-1;
                double.TryParse(this.txtnumCabezas.Text, out cabezas);
                comm.Parameters.Add("@numCabezas", SqlDbType.Float).Value = cabezas;
                comm.Parameters.Add("@numdeCertificados", SqlDbType.VarChar).Value = this.txtNumCertificados.Text;
                comm.Parameters.Add("@fechaEmision", SqlDbType.DateTime).Value = DateTime.Parse(this.txtCFFechaEmision.Text);
                comm.Parameters.Add("@fechaVencimiento", SqlDbType.DateTime).Value = DateTime.Parse(this.txtCFFechaVencimiento.Text);
                comm.Parameters.Add("@KG", SqlDbType.Float).Value = double.Parse(this.txtCFKG.Text);
                comm.Parameters.Add("@Precio", SqlDbType.Float).Value = double.Parse(this.txtCFPrecio.Text);
                comm.Parameters.Add("@MontoDelCert", SqlDbType.Float).Value = double.Parse(this.txtCFMonto.Text);
                comm.Parameters.Add("@credFinSeguroID", SqlDbType.Int).Value = int.Parse(this.ddlCFSeguro.SelectedValue);
                comm.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                comm.Parameters.Add("@productoID", SqlDbType.Int).Value = int.Parse(this.drpdlProducto.SelectedValue);
                comm.Parameters.Add("@empresacertificadaID", SqlDbType.Int).Value = int.Parse(this.drpdlEmpresa.SelectedValue);
                comm.Parameters.Add("@certID", SqlDbType.Int).Value = int.Parse(this.lblcertID.Text);
                comm.Parameters.Add("@bodegaID", SqlDbType.Int).Value = int.Parse(this.drpdlBodega.SelectedValue);
              
                comm.ExecuteNonQuery();

                comm.CommandText = "delete from Certificado_Credito_ClienteVenta where CredFinCertID = @certID"; // BORRAMOS TODA LA RELACION
                comm.Parameters.Clear();
                comm.Parameters.Add("@certID", SqlDbType.Int).Value = int.Parse(this.lblcertID.Text);
                comm.ExecuteNonQuery();
                if (this.chkAsignarClienteVenta.Checked || this.chkAsignarCredito.Checked)
                {
                    comm.CommandText = "Insert into Certificado_Credito_ClienteVenta(CredFinCertID, ClienteVentaID, CreditoID) values(@CredFinCertID, @ClienteVentaID, @CreditoID) ";
                    comm.Parameters.Clear();
                    comm.Parameters.Add("@CredFinCertID", SqlDbType.Int).Value = int.Parse(this.lblcertID.Text);
                    comm.Parameters.Add("@ClienteVentaID", SqlDbType.Int).Value = this.chkAsignarClienteVenta.Checked ? int.Parse(this.drpdlClienteVenta.SelectedValue) : -1;
                    comm.Parameters.Add("@CreditoID", SqlDbType.Int).Value = this.chkAsignarCredito.Checked ? int.Parse(this.drpdlCred.SelectedValue) : -1;
                    comm.ExecuteNonQuery();
                }
               
                this.pnlNewMov.Visible = true;
                this.lblResult.Text = "SE MODIFICÓ EL CERTIFICADO: " + this.txtNumCertificados.Text.ToUpper() + " EXITOSAMENTE.";
                
                this.imgBien.Visible = true;
                this.imgMal.Visible = false;
               
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CERTIFICADOS, Logger.typeUserActions.UPDATE, this.UserID, "MODIFICÓ EL CERTIFICADO No. : " + this.lblcertID.Text.ToUpper());
//                 String sNewUrl = "~/frmCertificadoAddQuick.aspx";
//                 sNewUrl += Utils.GetEncriptedQueryString("certID=" + this.lblcertID.Text);
//                 Response.Redirect(sNewUrl);
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error modificando certificado No. " + this.lblcertID.Text, ref ex);
                this.lblResult.Text = ex.Message;
                this.pnlNewMov.Visible = true;
                this.imgMal.Visible = true;
                this.imgBien.Visible = false;
            }
            finally
            {
                conn.Close();
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Write("<script type=\"text/javascript\">window.close();</script>");
        }

    }
}
