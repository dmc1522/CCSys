using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace Garibay
{
    public partial class frmAddModifySalidaDeProducto : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                this.btnModificar.Visible = false;
                this.btnAgregar.Visible = true;
                this.cmbProducto.DataBind();
                this.cmbBodega.DataBind();
                this.cmbTipoMovProd.DataBind();
                if (this.LoadEncryptedQueryString() > 0 && this.myQueryStrings["idtomodify"] != null)
                {
                    this.textBoxID.Text = this.myQueryStrings["idtomodify"].ToString();
                    this.LoadSalidaProducto();
                    this.btnAgregar.Visible = false;
                    this.btnModificar.Visible = true;
                    this.lblSalidaProducto.Text = "MODIFICAR SALIDA DE PRODUCTO";
                }
            }
            this.compruebasecurityLevel();
            if(this.panelmensaje.Visible)
            {
                this.panelmensaje.Visible = false;

            }
        }
        protected void LoadSalidaProducto()
        {
            SqlConnection conSacaSalidas = new SqlConnection(myConfig.ConnectionInfo);
            string query = "SELECT     tipoMovProdID, Fecha, cantidad, observaciones, productoID, bodegaID ";
            query += " FROM         SalidaDeProductos where salidaprodID = @salidaprodID";
            SqlCommand cmdSacaSalidas = new SqlCommand(query, conSacaSalidas);
            try
            {
                conSacaSalidas.Open();
                cmdSacaSalidas.Parameters.Clear();
                cmdSacaSalidas.Parameters.Add("@salidaprodID", SqlDbType.Int).Value = int.Parse(this.textBoxID.Text);
                SqlDataReader reader = cmdSacaSalidas.ExecuteReader();
                if(reader.Read())
                {
                    this.cmbTipoMovProd.SelectedValue = reader["tipoMovProdID"].ToString();
                    this.textBoxFecha.Text = Utils.converttoshortFormatfromdbFormat(reader["Fecha"].ToString());
                    this.txtCantidad.Text = string.Format("{0:F2}", double.Parse(reader["cantidad"].ToString()));
                    this.txtObservaciones.Text = reader["observaciones"].ToString();
                    this.cmbProducto.SelectedValue = reader["productoID"].ToString();
                    this.cmbBodega.SelectedValue = reader["bodegaID"].ToString();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "ERROR AL CARGAR DATOS DE SALIDA", ref ex);
            }
            finally
            {
                conSacaSalidas.Close();
            }
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
            }
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Server.Transfer("~/frmListSalidaDeProducto.aspx");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
          
            int id = dbFunctions.insertSalidaDeProducto(int.Parse(this.cmbProducto.SelectedValue), int.Parse(this.cmbBodega.SelectedValue), int.Parse(this.cmbTipoMovProd.SelectedValue), DateTime.Parse(this.textBoxFecha.Text), double.Parse(this.txtCantidad.Text), this.txtObservaciones.Text, -1, 0, this.UserID);
            if (id != -1)
            {
                this.btnAgregar.Visible = false;
                this.btnModificar.Visible = false;
                this.btnCancelar.Visible = false;
                this.panelmensaje.Visible = true;
                this.lblMensajetitle.Text = "EXITO";
                this.lblMensajeOperationresult.Text = "SALIDA AGREGADA EXITOSAMENTE";
                this.lblMensajeException.Text = "";
                this.imagenbien.Visible = true;
                this.imagenmal.Visible = false;
                this.btnAceptarList.Visible = true;
                this.textBoxID.Text = id.ToString();
            }
            else
            {
                this.btnAgregar.Visible = true;
                this.btnModificar.Visible = true;
                this.btnCancelar.Visible = true;
                this.panelmensaje.Visible = true;
                this.lblMensajetitle.Text = "FALLO";
                this.lblMensajeOperationresult.Text = "NO SE PUDO INSERTAR LA SALIDA, REVISE LOS DATOS";
                this.lblMensajeException.Text = "";
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.btnAceptarList.Visible = false;

            }
      
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if(dbFunctions.updateSalidaDeProducto(int.Parse(this.textBoxID.Text),int.Parse(this.cmbProducto.SelectedValue), int.Parse(this.cmbBodega.SelectedValue), int.Parse(this.cmbTipoMovProd.SelectedValue), DateTime.Parse(this.textBoxFecha.Text), double.Parse(this.txtCantidad.Text), this.txtObservaciones.Text, -1, 0, this.UserID))
            {
                
                this.panelmensaje.Visible = true;
                this.lblMensajetitle.Text = "EXITO";
                this.lblMensajeOperationresult.Text = "SALIDA MODIFICADA EXITOSAMENTE";
                this.lblMensajeException.Text = "";
                this.imagenbien.Visible = true;
                this.imagenmal.Visible = false;
                this.btnAceptarList.Visible = false;
            }
            else
            {
            
                this.panelmensaje.Visible = true;
                this.lblMensajetitle.Text = "FALLO";
                this.lblMensajeOperationresult.Text = "NO SE PUDO MODIFICAR LA SALIDA, REVISE LOS DATOS";
                this.lblMensajeException.Text = "";
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.btnAceptarList.Visible = false;

            }
      

        }

        protected void txtAceptarList_Click(object sender, EventArgs e)
        {
            String sQuery = "idtomodify=" + this.textBoxID.Text;
            sQuery = Utils.GetEncriptedQueryString(sQuery);
            String strRedirect = "~/frmAddModifySalidaDeProducto.aspx";
            Response.Redirect(strRedirect + sQuery);
        }
    }
}
