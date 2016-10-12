using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace Garibay
{
    public partial class frmCreditoFinancieroAdd : BasePage
    {
        
        internal void LoadCreditoData()
        {
            
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                String Query = "select * from CreditosFinancieros where creditoFinancieroID = @creditoFinancieroID";
                SqlCommand comm = new SqlCommand(Query, conn);
                SqlDataAdapter sqlDA = new SqlDataAdapter(comm);
                conn.Open();
                comm.Parameters.Add("@creditoFinancieroID", SqlDbType.Int).Value = int.Parse(this.txtIDtoMod.Text);
                DataTable dt = new DataTable();
                if (sqlDA.Fill(dt) > 0 )
                {
                    this.ddlBanco.DataBind();
                    this.ddlBanco.SelectedValue = dt.Rows[0]["bancoID"].ToString();
                    this.txtNumCredito.Text = dt.Rows[0]["numCredito"].ToString();
                    this.txtNumControl.Text = dt.Rows[0]["numControl"].ToString();
                    this.txtEmpresaAcreditada.Text = dt.Rows[0]["empresa_acreditada"].ToString();
                    this.txtMonto.Text = string.Format("{0:N2}", double.Parse(dt.Rows[0]["monto"].ToString()));
                    this.txtFechaApertura.Text = DateTime.Parse(dt.Rows[0]["fechadeapertura"].ToString()).ToString("dd/MM/yyyy");
                    this.txtfechaVencimiento.Text = DateTime.Parse(dt.Rows[0]["fechadevencimiento"].ToString()).ToString("dd/MM/yyyy");
                    this.txtTasaInteres.Text = dt.Rows[0]["tasadeinteres"].ToString();
                    this.txtGarantiaHip.Text = dt.Rows[0]["garantiaHipotecaria"].ToString();
                    this.chkPagado.Checked = bool.Parse(dt.Rows[0]["pagado"].ToString());
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error insertando credito financiero", ref ex);
            }
            finally
            {
                conn.Close();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                
                JSUtils.AddDisableWhenClick(ref this.btnAdd, this);
                JSUtils.AddDisableWhenClick(ref this.btnModificar, this);
              


                int iCreditoFinID = -1;
                this.btnAdd.Visible = true;
              
                this.btnModificar.Visible = true;
                if (Request.QueryString["data"] != null && this.loadqueryStrings(Request.QueryString["data"].ToString()) && int.TryParse(this.myQueryStrings["CreditoFinID"].ToString(), out iCreditoFinID) && iCreditoFinID > -1)
                {
                    this.txtIDtoMod.Text = iCreditoFinID.ToString();
                    this.LoadCreditoData();
                    this.GridView1.DataBind();
                  
                    this.btnModificar.Visible = true;
                    this.btnAdd.Visible = false;
                    string parameter;
                    string datosaencriptar;
                    datosaencriptar = "creditoFinID=";
                    datosaencriptar += this.txtIDtoMod.Text;
                    datosaencriptar += "&";
                    parameter = "javascript:url('";
                    parameter += "frmMovBancoAddQuick.aspx?data=";
                    parameter += Utils.encriptacadena(datosaencriptar);
                    parameter += "', '";
                    parameter += "Agregar Movimiento de Banco a un crédito Financiero";
                    parameter += "',0,200,600,550); return false;";
                    this.btnAddMov.Attributes.Add("onclick", parameter);

                    datosaencriptar = "credID=";
                    datosaencriptar += this.txtIDtoMod.Text;
                    datosaencriptar += "&";
                    parameter = "javascript:url('";
                    parameter += "frmCertificadoAddQuick.aspx?data=";
                    parameter += Utils.encriptacadena(datosaencriptar);
                    parameter += "', '";
                    parameter += "Agregar Certificado a un Crédito Financiero";
                    parameter += "',0,200,600,550); return false;";
                    this.btnAgregarCert.Attributes.Add("onclick", parameter);
                   

                    this.btnAddMov.Visible = true;
                    this.btnRecargaMovements.Visible = true;
                    
                }
                else
                {
                    this.pnlMovimientos.Visible = false;
                
                    this.btnAddMov.Visible = false;
                    this.btnRecargaMovements.Visible = true;

                }
            }
            this.pnlMensajeAddCredito.Visible = false;
            
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "INSERT INTO CreditosFinancieros " +
                    " (bancoID, numCredito, numControl, empresa_acreditada, monto, fechadeapertura, fechadevencimiento, tasadeinteres, userID, garantiaHipotecaria, pagado) " +
                    " VALUES (@bancoID,@numCredito,@numControl,@empresa_acreditada,@monto,@fechadeapertura,@fechadevencimiento,@tasadeinteres,@userID, @garantiaHipotecaria,@pagado) " +
                    " SELECT NewID = SCOPE_IDENTITY();";
                comm.Parameters.Add("@bancoID", SqlDbType.Int).Value = int.Parse(this.ddlBanco.SelectedValue);
                comm.Parameters.Add("@numCredito", SqlDbType.VarChar).Value = this.txtNumCredito.Text;
                comm.Parameters.Add("@numControl", SqlDbType.VarChar).Value = this.txtNumControl.Text;
                comm.Parameters.Add("@empresa_acreditada", SqlDbType.VarChar).Value = this.txtEmpresaAcreditada.Text;
                comm.Parameters.Add("@monto", SqlDbType.Float).Value = double.Parse(this.txtMonto.Text);
                comm.Parameters.Add("@fechadeapertura", SqlDbType.DateTime).Value = DateTime.Parse(this.txtFechaApertura.Text);
                comm.Parameters.Add("@fechadevencimiento", SqlDbType.DateTime).Value = DateTime.Parse(this.txtfechaVencimiento.Text);
                comm.Parameters.Add("@tasadeinteres", SqlDbType.VarChar).Value = this.txtTasaInteres.Text;
                comm.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                comm.Parameters.Add("@garantiaHipotecaria", SqlDbType.Text).Value = this.txtGarantiaHip.Text.Trim();
                comm.Parameters.Add("@pagado", SqlDbType.Bit).Value = this.chkPagado.Checked ? 1 : 0;
                
                int id = int.Parse(comm.ExecuteScalar().ToString());
                String sNewUrl = "~/frmCreditoFinancieroAdd.aspx";
                sNewUrl += Utils.GetEncriptedQueryString("CreditoFinID=" + id.ToString());
                Response.Redirect(sNewUrl);
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error insertando credito financiero", ref ex);
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
                comm.CommandText = "Update CreditosFinancieros " +
                    " set bancoID = @bancoID, numCredito = @numCredito, numControl = @numControl, empresa_acreditada = @empresa_acreditada, " +
                    " monto = @monto, fechadeapertura = @fechadeapertura, fechadevencimiento = @fechadevencimiento, tasadeinteres = @tasadeinteres, " +
                    " userID = @userID, garantiaHipotecaria = @garantiaHipotecaria, pagado=@pagado where creditoFinancieroID =  @creditoID ";
                   
                comm.Parameters.Add("@bancoID", SqlDbType.Int).Value = int.Parse(this.ddlBanco.SelectedValue);
                comm.Parameters.Add("@numCredito", SqlDbType.VarChar).Value = this.txtNumCredito.Text;
                comm.Parameters.Add("@numControl", SqlDbType.VarChar).Value = this.txtNumControl.Text;
                comm.Parameters.Add("@empresa_acreditada", SqlDbType.VarChar).Value = this.txtEmpresaAcreditada.Text;
                comm.Parameters.Add("@monto", SqlDbType.Float).Value = double.Parse(this.txtMonto.Text);
                comm.Parameters.Add("@fechadeapertura", SqlDbType.DateTime).Value = DateTime.Parse(this.txtFechaApertura.Text);
                comm.Parameters.Add("@fechadevencimiento", SqlDbType.DateTime).Value = DateTime.Parse(this.txtfechaVencimiento.Text);
                comm.Parameters.Add("@tasadeinteres", SqlDbType.VarChar).Value = this.txtTasaInteres.Text;
                comm.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                comm.Parameters.Add("@garantiaHipotecaria", SqlDbType.Text).Value = this.txtGarantiaHip.Text.Trim();
                comm.Parameters.Add("@creditoID", SqlDbType.Int).Value = int.Parse(this.txtIDtoMod.Text);
                comm.Parameters.Add("@pagado", SqlDbType.Bit).Value = this.chkPagado.Checked ? 1 : 0;
                comm.ExecuteNonQuery();
                this.pnlMensajeAddCredito.Visible = true;
                this.imagenbien.Visible = true;
                this.imagenmal.Visible = false;
                this.lblMsgResult0.Text = "CREDITO: " + this.txtIDtoMod.Text + " MODIFICADO EXITOSAMENTE.";
               
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error modificando credito financiero: " + this.txtIDtoMod.Text, ref ex);
                this.pnlMensajeAddCredito.Visible = true;
                this.lblMsgResult0.Text = "CREDITO: " + this.txtIDtoMod.Text + " NO HA PODIDO SER MODIFICADO.";
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
            }
            finally
            {
                conn.Close();
            }

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }
            HyperLink link = (HyperLink)e.Row.Cells[6].FindControl("HyperLink1");
            if (link != null)
            {




                string parameter, ventanatitle = "Modificar un certificado financiero";
                // String pathArchivotemp = PdfCreator.printLiquidacion(0, PdfCreator.tamañoPapel.CARTA, PdfCreator.orientacionPapel.VERTICAL, ref this.gvBoletas, ref gvAnticipos, ref gvPagosLiquidacion);
                string datosaencriptar;
                datosaencriptar = "certID=";
                datosaencriptar += this.GridView1.DataKeys[e.Row.RowIndex]["CredFinCertID"].ToString();
                parameter = "javascript:url('";
                parameter += "frmCertificadoAddQuick.aspx?data=";
                parameter += Utils.encriptacadena(datosaencriptar);
                parameter += "', '";
                parameter += ventanatitle;
                parameter += "',0,200,800,550); return false;";
                link.Attributes.Add("onClick", parameter);
                link.NavigateUrl = this.Request.Url.ToString();


            }
        }


        protected void ecargaMovements_Click(object sender, EventArgs e)
        {
            this.gridMovCuentasBanco.DataBind();
        }

        protected void gridMovCuentasBanco_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }
            HyperLink link = (HyperLink)e.Row.Cells[16].FindControl("LinkButton1");
            if (link != null)
            {
                string parameter, ventanatitle = "Modificar un movimiento de banco asignado a un crédito Financiero";
                // String pathArchivotemp = PdfCreator.printLiquidacion(0, PdfCreator.tamañoPapel.CARTA, PdfCreator.orientacionPapel.VERTICAL, ref this.gvBoletas, ref gvAnticipos, ref gvPagosLiquidacion);
                string datosaencriptar;
                datosaencriptar = "movID=";
                datosaencriptar += this.gridMovCuentasBanco.DataKeys[e.Row.RowIndex]["movbanID"];
                datosaencriptar += "&";
                datosaencriptar = datosaencriptar + "creditoFinID=" + this.txtIDtoMod.Text + "&";


                parameter = "javascript:url('";
                parameter += "frmMovBancoAddQuick.aspx?data=";
                parameter += Utils.encriptacadena(datosaencriptar);
                parameter += "', '";
                parameter += ventanatitle;
                parameter += "',0,200,700,550); return false;";
                link.Attributes.Add("onClick", parameter);
                link.NavigateUrl = this.Request.Url.ToString();
              //  link.Visible = ((CheckBox)e.Row.Cells[8].Controls[0]).Checked;
            }
        }

        protected void gridMovCuentasBanco_DataBound(object sender, EventArgs e)
        {
            this.calculaTotales();

        }
        protected void calculaTotales()
        {
            if (this.gridMovCuentasBanco.FooterRow != null)
            {
                // this.gvLiquidaciones.DataBind();
                this.gridMovCuentasBanco.FooterRow.Cells[1].Text = "TOTALES";
                double totalcargos = 0, totalabonos = 0;

                foreach (GridViewRow row in this.gridMovCuentasBanco.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        double cargo = 0, abono = 0;
                        double.TryParse(this.gridMovCuentasBanco.DataKeys[row.RowIndex]["cargo"].ToString(), out cargo);
                        totalcargos += cargo;
                        double.TryParse(this.gridMovCuentasBanco.DataKeys[row.RowIndex]["abono"].ToString(), out abono);
                        totalabonos += abono;


                    }

                }
                this.gridMovCuentasBanco.FooterRow.Cells[12].Text = string.Format("{0:C2}", totalcargos);
                this.gridMovCuentasBanco.FooterRow.Cells[13].Text = string.Format("{0:C2}", totalabonos);



            }


        }

        protected void btnRecargaCertificados_Click(object sender, EventArgs e)
        {
            this.GridView1.DataBind();
        }

      

     
    }
}
