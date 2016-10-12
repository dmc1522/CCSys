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
    public partial class Formulario_web16 : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.panelMensaje.Visible)
                this.panelMensaje.Visible = false;

            if (!IsPostBack)
            {
                this.panelMensaje.Visible = false;
                if (Request.QueryString["data"] != null && this.loadqueryStrings(Request.QueryString["data"].ToString()) && myQueryStrings["ordenDeleteID"] != null)
                {
                    if (this.borrarOrden())
                    {
                    }
                }
                else
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.ENTRADAPRODUCTOS, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "SE VISITÓ LA PAGINA DE LA LISTA DE ORDEN DE ENTRADAS DE PRODUCTO POR PROVEEDOR");
                }

            }  
        }


        public String GetOrdenNavigationURL(String sorden)
        {
            String sQuery = "OrdenID=" + sorden;
            sQuery = Utils.GetEncriptedQueryString(sQuery);
            String strRedirect = "~/frmEntradaProductoProveedor.aspx";
            strRedirect += sQuery;
            return strRedirect;
        }
        public String GetDeleteNavigationURL(String sorden)
        {
            String sQuery = "ordenDeleteID=" + sorden;
            sQuery = Utils.GetEncriptedQueryString(sQuery);
            String strRedirect = "~/frmListOrdenEntradasProveedores.aspx";
            strRedirect += sQuery;
            return strRedirect;
        }


        protected bool borrarOrden()
        {

            bool sresult;
            SqlConnection con = new SqlConnection(myConfig.ConnectionInfo);
            string query = "DELETE FROM Orden_de_entrada where ordenID=@ordenID";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            try
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@ordenID", SqlDbType.Int).Value = int.Parse(myQueryStrings["ordenDeleteID"].ToString());
                int drow = cmd.ExecuteNonQuery();
                if (drow != 1)
                {

                    throw new Exception("ERROR EN LA BASE DE DATOS, SE MODIFICARON " + drow.ToString() + "CUANDO SE ESPERABA QUE SE MODIFICARA 1");
                }
                this.panelMensaje.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = "SE ELIMINÓ LA ORDEN DE ENTRADA : :" + myQueryStrings["ordenDeleteID"].ToString() + " SATISFACTORIAMENTE";
                this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION
                this.imagenbien.Visible = true;
                this.imagenmal.Visible = false;
                sresult = true;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.ENTRADAPRODUCTOS, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), "SE ELIMINÓ LA ORDEN DE ENTRADA CON  EL FOLIO :" + myQueryStrings["ordenDeleteID"].ToString() + "SATISFACTORIAMENTE");

            }
            catch (Exception ex)
            {
                this.panelMensaje.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "ERROR AL ELIMINAR LA ORDEN DE ENTRADA DE PRODUCTO :" + myQueryStrings["ordenDeleteID"].ToString();
                this.lblMensajeException.Text = ex.Message; //BORRAMOS PORQUE NO HAY EXcEPTION
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, this.UserID, " ERROR AL AL CARGAR LOS TEMPLATES EN LA TABLA. LA EXC FUE: " + ex.Message, Request.Url.ToString());
                sresult = false;
            }
            finally
            {
                con.Close();
            }

            return sresult;
        }

    }
}
