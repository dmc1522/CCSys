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
    public partial class WebForm7 : Garibay.BasePage
    {
      
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.panelmensaje.Visible = false;
                
      
                if (Request.QueryString["data"] != null)
                {
                    if (this.loadqueryStrings(Request.QueryString["data"].ToString()))
                    {
                        this.lblEntPro.Text = "MODIFICANDO LA ENTRADA DE PRODUCTO NÚMERO: "+myQueryStrings["idtomodify"].ToString();
                        if (this.cargadatosmodify())
                        {
                            this.txtIdToModify.Text = myQueryStrings["idtomodify"].ToString();
                            this.cmbCiclo.Enabled=false;
                            this.drpdlproducto.Enabled = false;
                            this.PopCalendar1.Enabled = false;
                            this.drpdlBodega.Enabled = false;
         
                        }
                        else
                        {
                            this.txtIdToModify.Text = "-1";

                        }
                        this.btnModificar.Visible = true;
                        this.btnAgregar.Visible = false;
                    }
                    else
                    {
                        myQueryStrings.Clear();
                        Response.Redirect("~/frmListEntradaProductos.aspx", true);

                    }
                }
                else
                {
                    this.lblEntPro.Text = "AGREGAR NUEVA ENTRADA DE PRODUCTO";
                    

                    this.btnAgregar.Visible = true;
                    this.btnModificar.Visible = false;


                }

            }
            this.compruebasecurityLevel();

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
            this.Server.Transfer("~/frmListEntradaProductos.aspx");
            
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            String sError="";
           
            
            if (dbFunctions.addEntradaPro(int.Parse(this.drpdlproducto.SelectedValue), int.Parse(this.drpdlBodega.SelectedValue), int.Parse(this.drpdlTipoMov.SelectedValue), Utils.converttoLongDBFormat(this.txtFecha.Text), float.Parse(this.txtCant.Text), float.Parse(this.txtPrecio.Text),this.txtobser.Text, int.Parse(this.Session["USERID"].ToString()), int.Parse(this.cmbCiclo.SelectedValue), ref sError))
            {
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.ENTRADAPRODUCTOS, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("SE AGREGÓ LA ENTRADA DEL PRODUCTO: " + this.drpdlproducto.SelectedItem.Text.ToUpper()));
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("ENTRADAPRODUCTOSADDEDEXITO"), this.drpdlproducto.SelectedItem.Text.ToUpper());
                this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION        
                this.limpiarcampos();
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                String sqlQuery = "SELECT max(entradaprodID) FROM EntradaDeProductos";
                SqlCommand cmdIns = new SqlCommand(sqlQuery, conGaribay);
                conGaribay.Open();
                
                int maximo;
                
                cmdIns.Parameters.Clear();
                cmdIns.CommandText = sqlQuery;
                maximo = (int)cmdIns.ExecuteScalar();
                this.txtIdDetails.Text = maximo.ToString();
                this.panelagregar.Visible = false;
                this.DetvEntradaProductos.Fields[9].Visible = false;

                conGaribay.Close();
            }
            

            
                
            else{
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), sError, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("ENTRADAPRODUCTOSADDEDFAILED"), this.drpdlproducto.SelectedItem.Text.ToUpper());
                this.lblMensajeException.Text = sError;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.txtIdDetails.Text = "-1";
                this.panelagregar.Visible = false;
                this.limpiarcampos();
            }

            
        }

        protected void txtAceptarList_Click(object sender, EventArgs e)
        {
            
                if (Request.QueryString.Count>0)
                {
                    this.Response.Redirect("~/frmListEntradaProductos.aspx", true);

                }

            
            
            this.panelmensaje.Visible = false;
            this.panelagregar.Visible = true;
        }

        protected bool cargadatosmodify()
        {
            string qryIns = "SELECT productoID, bodegaID, tipoMovProdID, cicloID, Fecha, precio, cantidad, observaciones FROM [EntradaDeProductos] WHERE (entradaprodID = ";
            qryIns += this.myQueryStrings["idtomodify"].ToString();
            qryIns += ")";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            try
            {
                conGaribay.Open();
                SqlDataReader datostomodify;
                datostomodify = cmdIns.ExecuteReader();
                this.drpdlBodega.DataBind();
                this.drpdlproducto.DataBind();
                this.drpdlTipoMov.DataBind();
                if (!datostomodify.HasRows)
                {
                    this.lblMensajeOperationresult.Text = myConfig.StrFromMessages("FALLOCARGARMODIFICAR");
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeException.Text = "";
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;
                    this.panelagregar.Visible = false;
                    return false;
                }

                while (datostomodify.Read())
                {
                    this.drpdlproducto.SelectedValue = datostomodify[0].ToString();
                    this.drpdlBodega.SelectedValue = datostomodify[1].ToString();
                    this.drpdlTipoMov.Text = datostomodify[2].ToString();
                    this.txtCant.Text = datostomodify[6].ToString();
                    this.txtcanAux.Text = datostomodify[6].ToString();
                    this.txtobser.Text = datostomodify[7].ToString();
                    this.txtFecha.Text=Utils.converttoshortFormatfromdbFormat(datostomodify[4].ToString());
                    this.txtPrecio.Text= datostomodify[5].ToString();
                    //this.txtIdToModify.Text = datostomodify[0].ToString();
                    this.txtIdToModify.Text = this.myQueryStrings["idtomodify"].ToString();
                    
                }
            }
            catch (InvalidOperationException exception)
            {
                this.lblMensajeOperationresult.Text = myConfig.StrFromMessages("FALLOCARGARMODIFICAR");
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;

                this.panelagregar.Visible = false;

                return false;
            }
            catch (SqlException exception)
            {
                this.lblMensajeOperationresult.Text = myConfig.StrFromMessages("FALLOCARGARMODIFICAR");
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;

                this.panelagregar.Visible = false;

                return false;

            }
            catch (Exception exception)
            {
                this.lblMensajeOperationresult.Text = myConfig.StrFromMessages("FALLOCARGARMODIFICAR");
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;


                return false;

            }
            finally
            {
                conGaribay.Close();

            }
            return true;

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

            String sError = "";


            if (dbFunctions.updateEntradaPro(int.Parse(this.txtIdToModify.Text), int.Parse(this.drpdlTipoMov.SelectedValue), float.Parse(this.txtCant.Text), float.Parse(this.txtPrecio.Text), this.txtobser.Text, ref sError, int.Parse(this.drpdlBodega.SelectedValue), int.Parse(this.drpdlproducto.SelectedValue), int.Parse(this.cmbCiclo.SelectedValue), int.Parse(this.Session["USERID"].ToString()), float.Parse(this.txtcanAux.Text)))
            {
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.ENTRADAPRODUCTOS, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ LA ENTRADA DEL PRODUCTO: " + this.drpdlproducto.SelectedItem.Text.ToUpper()));
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("ENTRADAPRODUCTOSMODIFIEDEXITO"), this.drpdlproducto.SelectedItem.Text.ToUpper());
                this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION        
                this.limpiarcampos();
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                this.panelagregar.Visible = false;
                this.DetvEntradaProductos.Fields[9].Visible = false;
                this.txtIdDetails.Text = this.txtIdToModify.Text;
            }
            else
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), sError, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("ENTRADAPRODUCTOSMODIFIEDFAILED"), this.drpdlproducto.SelectedItem.Text.ToUpper());
                this.lblMensajeException.Text = sError;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.txtIdDetails.Text = "-1";
                this.panelagregar.Visible = false;
                this.limpiarcampos();
            }


        }

        protected void limpiarcampos(){
            this.drpdlproducto.SelectedIndex=-1;
            this.drpdlBodega.SelectedIndex=-1;
            this.drpdlTipoMov.SelectedIndex=-1;
            this.txtCant.Text="";
            this.txtobser.Text="";
            this.txtPrecio.Text = "";
            
        }

        

    }

}
