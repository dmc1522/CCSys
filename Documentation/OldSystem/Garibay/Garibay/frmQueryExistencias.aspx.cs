using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Garibay
{
    public partial class frmQueryExistencias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            double existencia = 0;
            String sExis = "0";
            
            
            if (this.Request.Params["prodID"] != null && this.Request.Params["prodID"].Length > 0 &&
                this.Request.Params["bodID"] != null && this.Request.Params["bodID"].Length > 0 /*
                &&
                                this.Request.Params["cicloID"] != null && this.Request.Params["cicloID"].Length > 0*/
                )
            {
                SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand comm = new SqlCommand();
                try
                {
                    conn.Open();
                    comm.Connection = conn;
                    comm.CommandText = "SELECT Existencia FROM ExistenciasView WHERE (bodegaID = @bodID) AND (productoID = @prodID)";
                    comm.Parameters.Add("@bodID", SqlDbType.Int).Value = this.Request.Params["bodID"].ToString();
                    comm.Parameters.Add("@prodID",SqlDbType.Int).Value = this.Request.Params["prodID"].ToString();
                    //comm.Parameters.Add("@cicloID", SqlDbType.Int).Value = this.Request.Params["cicloID"].ToString();
                    SqlDataReader r = comm.ExecuteReader();
                    
                    if (r.HasRows && r.Read() && r[0] != null && double.TryParse(r[0].ToString(), out existencia))
                    {
                        //sExis = string.Format("{0:n}", existencia);
                        sExis = existencia.ToString();
                    }
                   
                    


                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Frm_load", ref ex);
                }
            }
            Response.Clear();
            Response.Write(sExis);
            Response.End();
        }
    }
}
