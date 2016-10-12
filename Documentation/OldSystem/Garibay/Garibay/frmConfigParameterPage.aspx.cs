using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using System.Text;
using System.Data;

namespace Garibay
{
    public partial class frmConfigParameterPage : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                this.LoadFonts();
                JSUtils.OpenNewWindowOnClick(ref this.btnChequesConfig, "frmAjustesCheques.aspx", "Configuracion de Cheques", true);
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
        private void LoadFonts()
        {
            int totalfonts = FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
            DataTable dt = new DataTable();
            dt.Columns.Add("Font", typeof(String));
            foreach (string fontname in FontFactory.RegisteredFonts)
            {
                dt.Rows.Add(new object[1] { fontname });
            }
            dt.DefaultView.Sort = "Font";
            this.ddlChequeFont.DataSource = dt;
            this.ddlChequeFont.DataTextField = "Font";
            this.ddlChequeFont.DataValueField = "Font";
            this.ddlChequeFont.DataBind();
            this.ddlChequeFont.SelectedIndex = 0;
        }

        protected void btnSaveConfig_Click(object sender, EventArgs e)
        {
            myConfig.SetStringConfig("CHEQUEFONT", myConfig.CATEGORIA.CHEQUES, this.ddlChequeFont.SelectedValue);
        }
    }
}
