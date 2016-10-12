using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Garibay
{
    public partial class logout : System.Web.UI.Page
    {
        public Hashtable myQueryStrings;

        protected void Page_Load(object sender, EventArgs e)
        {
            myQueryStrings = new Hashtable();
            this.Session.Clear();
            if (Request.QueryString["data"] != null)
            {
                if (Utils.loadqueryStrings(Request.QueryString["data"].ToString(), ref this.myQueryStrings))
                {
                    if (this.myQueryStrings["valor"] != null && this.myQueryStrings["valor"].ToString() == "1")
                    {
                        this.lblMsjLogout.Text = "SESION INVALIDA";
                    }
                    else
                    {
                        this.lblMsjLogout.Text = "¡Su sesión se ha cerrado exitosamente! Espere unos segundos mientras se redirecciona a la página principal";
                    }
                }
                else
                {
                    this.lblMsjLogout.Text = "¡Su sesión se ha cerrado exitosamente! Espere unos segundos mientras se redirecciona a la página principal";
                }
            }
            else
            {
                this.lblMsjLogout.Text = "¡Su sesión se ha cerrado exitosamente! Espere unos segundos mientras se redirecciona a la página principal";
                //System.Threading.Thread.Sleep(10000);
                //Server.Transfer("~/Default.aspx");
            }           
        }
       
    }
}
