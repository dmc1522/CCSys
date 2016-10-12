using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Garibay
{
    public partial class frmListaNotasDeCompraFormato : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string getOpenLink(string id)
        {
            string redirect = "frmOrdenCompraFormato.aspx";
            redirect += Utils.GetEncriptedQueryString("notaCompraID=" + id);
            return redirect;
        }
    }
}
