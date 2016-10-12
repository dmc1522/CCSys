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
    public partial class Formulario_web11 : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                this.drpdlDestino.DataBind();
                this.drpdlOrigen.DataBind();
                this.ddlCiclos.DataBind();
                this.dddlBodega.SelectedIndex = 0;
                this.ddlCiclos.SelectedIndex = 0;
                this.drpdlOrigen.SelectedIndex = 0;    
            }
            if(this.panelmensaje.Visible)
            {
                this.panelmensaje.Visible = false;
            }
            addjsToControls();
        }

        protected void btnConvert_Click(object sender, EventArgs e)
        {
            String err="";
            int ident;
            int idsalida;
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            string query = "insert into ConversionProducto(productoorigenID, productodestinoID, fecha, entradaID, salidaID, storeTS) ";
            query += " values(@productoorigenID, @productodestinoID, @fecha, @entradaID, @salidaID, @storeTS);";
            SqlCommand cmd = new SqlCommand(query, conGaribay);
            try
            {
                if ((idsalida = dbFunctions.insertSalidaDeProducto(int.Parse(drpdlOrigen.SelectedValue), int.Parse(this.dddlBodega.SelectedValue), 5, Utils.Now, Utils.GetSafeFloat(this.txtCantidadOrigen.Text), "SALIDA DE PARA CONVERSION DE PRODUCTO", int.Parse(this.ddlCiclos.SelectedValue), 0, int.Parse(this.Session["userID"].ToString()))) > -1)
                {
                    if (dbFunctions.addEntradaPro(int.Parse(drpdlDestino.SelectedValue), int.Parse(this.dddlBodega.SelectedValue), 5, Utils.Now.ToString(), int.Parse(this.txtCantidadDestino.Text), 0, "CONVERSION DE PRODUCTO", int.Parse(this.Session["userID"].ToString()), int.Parse(this.ddlCiclos.SelectedValue), ref err,out ident))
                    {
                        cmd.Parameters.Add("@productoorigenID", SqlDbType.Int).Value = int.Parse(drpdlOrigen.SelectedValue);
                        cmd.Parameters.Add("@productodestinoID", SqlDbType.Int).Value = int.Parse(drpdlDestino.SelectedValue);
                        cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = Utils.Now;
                        cmd.Parameters.Add("@entradaID", SqlDbType.Int).Value = ident;
                        cmd.Parameters.Add("@salidaID", SqlDbType.Int).Value = idsalida;
                        cmd.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.Now;
                        
                        conGaribay.Open();
                        cmd.ExecuteNonQuery();
                        this.panelmensaje.Visible = true;
                        this.imagenmal.Visible = false;
                        this.imagenbien.Visible = true;
                        this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                        this.lblMensajeOperationresult.Text = "SE CONVIRTIÓ EL PRODUCTO " + this.drpdlOrigen.SelectedItem.Text + " A " + this.drpdlDestino.SelectedItem.Text +" SATISFACTORIAMENTE";
                        this.lblMensajeException.Text = "";//NO HAY EXCEPTION

                    }
                }
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CONVERSIONPRODUCTO, Logger.typeUserActions.UPDATE, "SE CONVIRTIÓ EL PRODUCTO " + this.drpdlOrigen.SelectedItem.Text + " A " + this.drpdlDestino.SelectedItem.Text);
           }catch(Exception ex){
               this.panelmensaje.Visible = true;
               this.imagenmal.Visible = true;
               this.imagenbien.Visible = false;
               this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
               this.lblMensajeOperationresult.Text = "ERROR  AL HACER LA CONVERSION DEL PRODUCTO " + this.drpdlOrigen.SelectedItem.Text + " A " + this.drpdlDestino.SelectedItem.Text;
               this.lblMensajeException.Text = ex.Message;
               Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["userID"].ToString()), "ERROR  AL HACER LA CONVERSION DEL PRODUCTO " + this.drpdlOrigen.SelectedItem.Text + " A " + this.drpdlDestino.SelectedItem.Text +"ERROR FUE "+ ex.Message, this.Request.Url.ToString()); 

               }
                
           
        }
        protected void addjsToControls() 
        {
            string sQuery = "QueryExistencias('" + this.txtExistenciaO.ClientID + "','" + this.ddlCiclos.ClientID + "','" + this.drpdlOrigen.ClientID + "','" + this.dddlBodega.ClientID + "');return false;";
            this.drpdlOrigen.Attributes.Clear();
            this.drpdlOrigen.Attributes.Add("onChange", sQuery);
            this.dddlBodega.Attributes.Add("onChange", sQuery);
            
            sQuery = "QueryExistencias('" + this.txtExistenciaDestino.ClientID + "','" + this.ddlCiclos.ClientID + "','" + this.drpdlDestino.ClientID + "','" + this.dddlBodega.ClientID + "');return false;";
            this.drpdlDestino.Attributes.Clear();
            this.drpdlDestino.Attributes.Add("onChange", sQuery);

            /*
            sQuery = "validarExistencia2('" + this.txtCantidadOrigen.ClientID + "','" + this.txtExistenciaO.ClientID + "');";
                        this.txtCantidadOrigen.Attributes.Clear();
                        this.txtCantidadOrigen.Attributes.Add("onChange", sQuery);*/
            

        
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmProductos.aspx",true);
        }
    }
}
