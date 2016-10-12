using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Garibay
{
    public partial class frmGruposCatalogos : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.panelmensaje.Visible = false;
        }

        protected void GridViewGruposCatalogos_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            if(e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "Error modificando catálogo", e.Exception);
                e.ExceptionHandled = true;
                this.ShowMensajePanel("ERROR AL MODIFICAR EL CATÁLOGO", false, e.Exception.Message);
          
            }
        }

        protected void GridViewGruposCatalogos_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            if (e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "Error eliminando catálogo", e.Exception);
                e.ExceptionHandled = true;
                this.ShowMensajePanel("ERROR AL ELIMINAR EL CATÁLOGO", false, e.Exception.Message);
          
            }

        }

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.CommandText = "INSERT INTO GruposCatalogosMovBancos values (@grupoCatalogo)";
            connection.Open();
            try
            {
                cmdInsert.Connection = connection;
                cmdInsert.Parameters.Add("@grupoCatalogo",System.Data.SqlDbType.VarChar).Value = this.TextBoxGuardar.Text;
                cmdInsert.ExecuteNonQuery();
                this.GridViewGruposCatalogos.DataBind();
                this.ShowMensajePanel("SE HA AGREGADO EL CATÁLOGO EXITOSAMENTE", true, string.Empty);
                
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error insertando nuevo grupo de catálogos", ex);
                this.ShowMensajePanel("ERROR AL AGREGAR EL CATÁLOGO", false, ex.Message);
          
            }
            finally
            {
                connection.Close();
            }
        }
        private void ShowMensajePanel(string texto, bool exito, string exception)
        {
            this.panelmensaje.Visible = true;
            this.lblMensajeException.Text = exception;
            this.lblMensajetitle.Text = texto;
            this.imagenbien.Visible = exito;
            this.imagenmal.Visible = !exito;
            this.lblMensajetitle.Visible = false;
        }
    }
}
