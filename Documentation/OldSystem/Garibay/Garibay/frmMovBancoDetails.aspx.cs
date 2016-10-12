using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class MovBancoDetails :Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (Request.QueryString["data"] != null)
                {
                    if (this.loadqueryStrings(Request.QueryString["data"].ToString()))
                    {
                         this.txtIDDetails.Text = myQueryStrings["id"].ToString();
                         this.lblMensajeException.Visible = false;
                         this.lblMensajeOperationresult.Visible = false;
                         this.lblMensajetitle.Visible = false;
                         this.imagenbien.Visible = false;
                         this.imagenmal.Visible = false;
                        // this.txtIDDetails.Text = "-1";
                         this.DetailsView1.DataBind();
                    }
                    else
                    {
                        this.lblMensajeOperationresult.Text = "ERROR AL CARGAR LOS DATOS DEL MOVIMIENTO";
                        this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                        this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXCEPTION
                        this.imagenmal.Visible = true;
                        this.lblMensajeException.Visible = true;
                        this.lblMensajeOperationresult.Visible = false;
                        this.lblMensajetitle.Visible = true;
                        this.imagenbien.Visible = false;
                        this.txtIDDetails.Text = "-1";
                       
                        
                    }
                }
                else{
                    this.lblMensajeOperationresult.Text = "ERROR AL CARGAR LOS DATOS DEL MOVIMIENTO";
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXCEPTION
                    this.imagenmal.Visible = true;
                    this.lblMensajeException.Visible = true;
                    this.lblMensajeOperationresult.Visible = false;
                    this.lblMensajetitle.Visible = true;
                    this.imagenbien.Visible = false;
                    this.txtIDDetails.Text = "-1";
                    
                 
                }
               
            

        }
    }
}
