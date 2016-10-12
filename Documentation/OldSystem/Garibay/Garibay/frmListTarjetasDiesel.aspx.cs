using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace Garibay
{
    public partial class Formulario_web12 : Garibay.BasePage

    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.panelmensaje.Visible)
            {
                this.panelmensaje.Visible = false;

            }
            if(!IsPostBack){
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.TARJETASDIESEL, Logger.typeUserActions.SELECT, "EL USUARIO " + this.UserID.ToString() + " VISITO LA PAGINA DE LISTA DE TARJETAS DIESEL");
            if (Request.QueryString["data"] != null)
            {

                if (this.loadqueryStrings(Request.QueryString["data"].ToString()))
                {
            //        this.lblMovCajaChica.Text = "MODIFICAR UN MOVIMIENTO DE CAJA CHICA";

                    if (this.eliminaTarjetaDiesel())
                    {
                        this.panelmensaje.Visible = true;
                        this.imagenmal.Visible = false; ;
                        this.imagenbien.Visible = true;
                        this.lblMensajetitle.Text = myConfig.StrFromMessages("ÉXITO");
                        this.lblMensajeOperationresult.Text = "EL MOVIMIENTO DE TARJETA DE DIESEL CON EL FOLIO " + myQueryStrings["iddiesel"].ToString()+" SE HA ELIMINADO SATISFACTORIAMENTE";
                        this.lblMensajeException.Text = "";
                        
                       // this.txtIDDetails.Text = myQueryStrings["idtomodify"].ToString();
                        //this.PopCalendar1.Enabled = false;
                    }
                    else
                    {
                        //this.txtIDDetails.Text = "-1";
                        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, this.UserID, "NO SE PUDO ELIMINAR LA TARJETA DIESEL", this.Request.Url.ToString());
                    }
                    //this.btnModificar.Visible = true;
                    //this.cmdAceptar.Visible = false;
                }
                else
                {
                    myQueryStrings.Clear();
                    Response.Redirect("~/frmListTarjetasDiesel.aspx", true);

                }
            }
            else
            {

                //this.lblMovCajaChica.Text = "AGREGAR NUEVO MOVIMIENTO DE CAJA CHICA";
                //this.btnAceptardtlst.Visible = true;
                //this.btnModificar.Visible = false;
                //this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
                //this.txtFechaLimite.Text = this.txtFecha.Text;

            }
            }
      
        
        }

        protected void grdvTarjetas_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            string query = " SELECT  movimientoID FROM  MovimientosCaja_TarjetasDiesel WHERE  (folio = @folio)";
            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader read;
                cmd.Parameters.Add("@folio", SqlDbType.Int).Value = int.Parse(e.Row.Cells[0].Text);
                read = cmd.ExecuteReader();
                int idmov=-1;
                HyperLink link = new HyperLink();
                link = (HyperLink)e.Row.Cells[3].FindControl("HyperLink1");
                if (read.HasRows && read.Read() && read["movimientoID"].ToString()!="")
                {
                    if (link != null)
                    {
                        link.Text = read["movimientoID"].ToString();
                        String sQuery = "idtomodify=" + link.Text;// = read["movimientoID"].ToString();
                        sQuery = Utils.GetEncriptedQueryString(sQuery);
                        String strRedirect = "~/frmAddMovCajaChica.aspx";
                        strRedirect += sQuery;
                        link.NavigateUrl = strRedirect;
                        idmov = int.Parse(link.Text);
                    }
                }
                else
                {
                    link.Text = "";
                }
                link = new HyperLink();
                link = (HyperLink)e.Row.Cells[5].FindControl("HyperLink2");
                if (link != null)
                {
                    link.Attributes.Clear();
                    string msgDel = "return confirm('¿Realmente desea eliminar la tarjeta diesel: ";
                    msgDel += int.Parse(e.Row.Cells[0].Text);
                    msgDel += "? Tenga en cuenta que esto borrara el pago que se realizó con la tarjeta')";
                    link.Attributes.Add("onclick", msgDel);
                    link.Text = "ELIMINAR";

                    String sQuery = "iddiesel=" + e.Row.Cells[0].Text + "&idmov=" + idmov;// = read["movimientoID"].ToString();
                    sQuery = Utils.GetEncriptedQueryString(sQuery);
                    String strRedirect = "~/frmListTarjetasDiesel.aspx";
                    strRedirect += sQuery;
                    link.NavigateUrl = strRedirect;
                }
                else
                {
                    link.Text = "";
                }
                read.Close();
                link = (HyperLink)e.Row.Cells[4].FindControl("HyperLink3");
                if (link != null)
                {
                    query = " SELECT DISTINCT FacturaFolio FROM         TarjetasDiesel WHERE  (folio = @folio)";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add("@folio", SqlDbType.Int).Value = int.Parse(e.Row.Cells[0].Text);
                    read = cmd.ExecuteReader();
                    if (read.HasRows && read.Read() && read["FacturaFolio"].ToString() != "" && read["FacturaFolio"].ToString() != "-1")
                    {
                        String sQuery = "FacturaDieselID=" + read["FacturaFolio"].ToString(); // = read["movimientoID"].ToString();
                        sQuery = Utils.GetEncriptedQueryString(sQuery);
                        String strRedirect = "~/frmFacturaDiesel.aspx";
                        strRedirect += sQuery;
                        link.NavigateUrl = strRedirect;
                        link.Text = read["FacturaFolio"].ToString();
                    }                
                    else
                    {
                        link.Text = "";
                    }
                }

            }    
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, this.UserID, "NO SE PUDIERON CARGAR LOS LINKS DEL LA TABLA", this.Request.Url.ToString());
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "NO SE PUDIERON CARGAR LOS LINKS DEL LA TABLA", ref ex);
            }
            finally
            {
                conn.Close();
            }

        }
            
        

        protected bool eliminaTarjetaDiesel()
        {

            Boolean sresult;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            string query = "DELETE FROM MovimientosCaja where movimientoID=@movimientoID";
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                cmd.Parameters.Add("@movimientoID", SqlDbType.Int).Value = int.Parse(myQueryStrings["idmov"].ToString());
                conn.Open();
                int delrow = cmd.ExecuteNonQuery();
                if(delrow!=1)
                {
                    throw new Exception("HUBO UN ERROR CON LA BASE DE DATOS");
                }             
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.TARJETASDIESEL, Logger.typeUserActions.DELETE, "ELIMINÓ EL MOVIMIENTO DE LA TARJETA DIESEL : " + myQueryStrings["iddiesel"].ToString());
                sresult = true;
            }
            catch(Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, this.UserID, "NO SE PUDÓ ELIMINAR EL MOVIMIENTO DE CAJA CHICA LA EXC FUE" + ex.Message, this.Request.Url.ToString());
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "NO SE PUDÓ ELIMINAR EL MOVIMIENTO DE CAJA CHICA", ref ex);
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "EL MOVIMIENTO DE TARJETA DE DIESEL CON EL FOLIO " + myQueryStrings["iddiesel"].ToString() + " NO PUDO ELIMINARSE";
                //this.lblMensajeException.Text = err1.Message;
                sresult = false;
            }
            finally
            {
                conn.Close();
            }

            return sresult;
        }

        protected void grdvTarjetas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
