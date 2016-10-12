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
using System.Data.Odbc;
using System.Data.SqlClient;

namespace Garibay
{
    public partial class frmMissingCheques : Garibay.BasePage
    {
        int cuentaID;

        DataTable dt;
           
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                dt = new DataTable();
                if (Request.QueryString["data"] != null)
                {

                    if (this.loadqueryStrings(Request.QueryString["data"].ToString()))
                    {
                        cuentaID = int.Parse(myQueryStrings["cuentaID"].ToString());
                        this.cargaCheques();

                    }
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
        protected bool cargaCheques(){
            dt.Columns.Add("chequenumero",typeof(long));
            string sqlSacaTodos = "Select numCheque from MovimientosCuentasBanco where numCheque>0  and cuentaID = @cuentaID order by numCheque ASC ";
            SqlConnection conSacaTodos = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdSacaTodos = new SqlCommand(sqlSacaTodos, conSacaTodos);
           
             long primercheque = 0;
            long chequededb = 0;
            try
            {
                
                conSacaTodos.Open();
                cmdSacaTodos.Parameters.Clear();
                cmdSacaTodos.Parameters.Add("@cuentaID", SqlDbType.Int).Value = cuentaID;
                SqlDataReader reader = cmdSacaTodos.ExecuteReader();
           
               
                reader.Read();
                primercheque = long.Parse(reader[0].ToString());
                primercheque++;
                while(reader.Read())
                {
                    chequededb = long.Parse(reader[0].ToString());
                    if (primercheque != chequededb)
                    {
                        while (primercheque != chequededb)
                        {
                            dt.Rows.Add(primercheque);
                            primercheque++;
                        }
                    }
                    else {
                        primercheque++;
                    }
                        
                   
                    
                }

                this.gridMissingCheques.DataSource = dt;
                this.Session["dt"] = dt;
                this.gridMissingCheques.DataBind();

            }
            catch (Exception e)
            {
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CHEQUES, Logger.typeUserActions.SELECT, this.UserID, "HUBO UN ERROR AL CARGAR CHEQUES FALTANTES. LA EXC FUE:  " + e.Message);

            }
            finally
            {
               
                conSacaTodos.Close();
              
            }
            return true;

        }

        protected void gridMissingCheques_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           this.gridMissingCheques.PageIndex = e.NewPageIndex;
           dt = ((DataTable)(this.Session["dt"]));
           this.gridMissingCheques.DataSource = dt;
           this.gridMissingCheques.DataBind();

        }
    }
}
