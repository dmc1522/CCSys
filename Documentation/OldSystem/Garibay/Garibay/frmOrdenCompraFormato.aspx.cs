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
    public partial class frmOrdenCompraFormato : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.btnPrint.Visible = false;
                if (this.LoadEncryptedQueryString()>0)
                {
                    if (this.myQueryStrings["notaCompraID"] != null)
                    {
                        this.txtNotaID.Text = this.myQueryStrings["notaCompraID"].ToString();
                        this.LoadNota();
                        this.btnPrint.Visible = true;
                        this.SetPrintString(int.Parse(this.myQueryStrings["notaCompraID"].ToString()));
                        this.btnAgregar.Visible = false;
                    }
                }
            }
            this.pnlNewPago.Visible = false;
        }
        internal void LoadNota()
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "select empresaID, fecha, comprade, preciode, entrega, proveedorID from ordenDeCompraFormato "
                    + " where ordenID = @orderID";
                comm.Parameters.Add("@orderID", SqlDbType.Int).Value = this.txtNotaID.Text;
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    this.ddlEmpresa.DataBind();
                    this.ddlEmpresa.SelectedValue = reader["empresaID"].ToString();
                    this.txtFecha.Text = ((DateTime)reader["fecha"]).ToString("dd/MM/yyyy");
                    this.txtComprade.Text = reader["comprade"].ToString();
                    this.txtPrecioDe.Text = reader["preciode"].ToString();
                    this.txtEntrega.Text = reader["entrega"].ToString();
                    this.ddlProveedores.DataBind();
                    this.ddlProveedores.SelectedValue = reader["proveedorID"].ToString();
                    this.btnAgregar.Visible = false;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "cargando datos de nota", ref ex);
            }
            finally
            {
                conn.Close();
            }
        }
        internal void SetPrintString(int notaCompra)
        {
            JSUtils.OpenNewWindowOnClick(ref this.btnPrint, "frmDescargaTmpFile.aspx" + Utils.GetEncriptedQueryString("NotaCompraFormato=" + notaCompra.ToString() + "&filename=ordencompra.pdf"), "Imprimir Formato de Compra", true);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            int NewNota = -1;
            String RedirectUrl = "";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "INSERT INTO ordenDeCompraFormato "
                    + "(empresaID, userID, fecha, comprade, preciode, entrega, proveedorID) "
                    + "VALUES (@empresaID,@userID,@fecha,@comprade,@preciode,@entrega, @proveedorID);"
                    + "SELECT NewID = SCOPE_IDENTITY();";
                comm.Connection = conn;
                comm.Parameters.Add("@empresaID", SqlDbType.Int).Value = int.Parse(this.ddlEmpresa.SelectedValue);
                comm.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                comm.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Parse(this.txtFecha.Text);
                comm.Parameters.Add("@comprade", SqlDbType.VarChar).Value = this.txtComprade.Text;
                comm.Parameters.Add("@preciode", SqlDbType.VarChar).Value = this.txtPrecioDe.Text;
                comm.Parameters.Add("@entrega", SqlDbType.VarChar).Value = this.txtEntrega.Text;
                comm.Parameters.Add("@proveedorID", SqlDbType.Int).Value = int.Parse(this.ddlProveedores.SelectedValue);
                NewNota = int.Parse(comm.ExecuteScalar().ToString());
                if (NewNota > 0)
                {
                    RedirectUrl = "frmOrdenCompraformato.aspx" + Utils.GetEncriptedQueryString("notaCompraID=" + NewNota.ToString());
                    this.SetPrintString(NewNota);
                    this.pnlNewPago.Visible = true;
                    this.imgBienPago.Visible = true;
                    this.imgMalPago.Visible = false;
                    this.lblNewNota.Text = "LA NOTA FUE AGREGADA EXISTOSAMENTE";
                    this.btnAgregar.Visible = false;
                    this.btnPrint.Visible = true;
                }
                else
                {
                    this.pnlNewPago.Visible = true;
                    this.imgBienPago.Visible = false;
                    this.imgMalPago.Visible = true;
                    this.lblNewNota.Text = "LA NOTA NO PUDO SER AGREGADA";
                }

            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error agregando nota de compra formato", ref ex);
                this.pnlNewPago.Visible = true;
                this.imgBienPago.Visible = false;
                this.imgMalPago.Visible = true;
                this.lblNewNota.Text = "LA NOTA NO PUDO SER AGREGADA, EL ERROR ES: " + ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
