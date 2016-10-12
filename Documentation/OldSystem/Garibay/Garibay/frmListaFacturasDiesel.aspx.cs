using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class Formulario_web14 : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.panelMensaje.Visible)
                this.panelMensaje.Visible = false;

            if(!IsPostBack)
            {
                this.panelMensaje.Visible = false;
                if (Request.QueryString["data"] != null&&this.loadqueryStrings(Request.QueryString["data"].ToString())&&myQueryStrings["FacturaFolio"]!=null)
                {                    
                    if(borrarFactura())
                    {
                    }
                }
                else
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURASDIESEL, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "SE VISITÓ LA PAGINA DE LA LISTA DE FACTURA DIESEL");
                }
                    
                        
            }        

        }

        public String GetFacturaNavigationURL(String sFolio)
        {
            String sQuery = "FacturaDieselID=" + sFolio;
            sQuery = Utils.GetEncriptedQueryString(sQuery);
            String strRedirect = "~/frmFacturaDiesel.aspx";
            strRedirect += sQuery;
            return strRedirect;
        }
        protected void grdvListaFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                SqlConnection con = new SqlConnection(myConfig.ConnectionInfo);
                string query = " SELECT     folio FROM         TarjetasDiesel WHERE     (FacturaFolio = @FacturaFolio)";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader read;
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@FacturaFolio", SqlDbType.Int).Value = int.Parse(this.grdvListaFacturas.DataKeys[e.Row.RowIndex]["FacturaFolio"].ToString());
                    read = cmd.ExecuteReader();

                    Label lista = (Label)e.Row.Cells[3].FindControl("Label2");
                    if (read.HasRows)
                    {
                        lista.Text = "";
                        int i = 0;
                        while (read.Read())
                        {

                            lista.Text += read[0].ToString() + "-";
                            if (i % 4 == 0)
                                lista.Text += "</br>";


                            i++;
                        }
                    }
                    else
                    {
                        lista.Text = " ";


                    }




                    read.Close();


                }
                catch (Exception ex)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["userID"].ToString()), "ERROR AL AL CARGAR LOS TEMPLATES EN LA TABLA. LA EXC FUE: " + ex.Message,Request.Url.ToString());

                }
                finally
                {
                    con.Close();
                }
            }
        }

       protected String GetURLDelete(string id)
        {
            return "~/frmListaFacturasDiesel.aspx" + Utils.GetEncriptedQueryString("FacturaFolio=" + id);
        }
       protected String getText()
       {
           return "ELIMINAR";
       }


        protected bool borrarFactura(){

            bool sresult;
             SqlConnection con = new SqlConnection(myConfig.ConnectionInfo);
                string query = "DELETE FROM FacturasDiesel WHERE  (FacturaFolio = @FacturaFolio)";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                
                try
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@FacturaFolio", SqlDbType.Int).Value = int.Parse(myQueryStrings["FacturaFolio"].ToString());
                    int drow=cmd.ExecuteNonQuery();
                    if(drow!=1){

                        throw new Exception("ERROR EN LA BASE DE DATOS, SE MODIFICARON "+drow.ToString() +"CUANDO SE ESPERABA QUE SE MODIFICARA 1");
                    }
                    this.panelMensaje.Visible = true;
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                    this.lblMensajeOperationresult.Text = "SE ELIMINÓ LA FACTURA DIESEL CON  EL FOLIO :" + myQueryStrings["FacturaFolio"].ToString()+" SATISFACTORIAMENTE";
                    this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION
                    this.imagenbien.Visible = true;
                    this.imagenmal.Visible = false;
                    sresult = true;
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURASDIESEL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), "SE ELIMINÓ LA FACTURA DIESEL CON  EL FOLIO :" + myQueryStrings["FacturaFolio"].ToString()+ "SATISFACTORIAMENTE");
                    
                }
                catch (Exception ex)
                {
                    this.panelMensaje.Visible = true;
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = "ERROR AL ELIMINAR LA FACTURA DIESEL CON  EL FOLIO :" + myQueryStrings["FacturaFolio"].ToString();
                    this.lblMensajeException.Text = ex.Message; //BORRAMOS PORQUE NO HAY EXcEPTION
                    this.imagenbien.Visible = false;
                    this.imagenmal.Visible = true;                    
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["userID"].ToString()), " ERROR AL AL CARGAR LOS TEMPLATES EN LA TABLA. LA EXC FUE: " + ex.Message, Request.Url.ToString());
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
