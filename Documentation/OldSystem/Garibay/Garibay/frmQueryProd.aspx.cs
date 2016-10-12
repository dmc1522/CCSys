using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Garibay
{
    public partial class frmQueryProd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String sXML = "";

            if (this.Request.Params["prodID"] != null && this.Request.Params["prodID"].Length > 0)
            {
                SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand comm = new SqlCommand();
                try
                {
                    conn.Open();
                    comm.Connection = conn;
                    comm.CommandText = "SELECT productoID, precio1, precio2, precio3, precio4 FROM Productos WHERE (productoID = @productoID)";
                    comm.Parameters.Add("@productoID", SqlDbType.Int).Value = this.Request.Params["prodID"].ToString();

                    SqlDataReader r = comm.ExecuteReader();

                    if (r.HasRows && r.Read())
                    {
                        sXML = "<data>";
                        sXML += "<ID>" + r["productoID"].ToString() + "</ID>";
                        sXML += "<p1>" + r["precio1"].ToString() + "</p1>";
                        sXML += "<p2>" + r["precio2"].ToString() + "</p2>";
                        sXML += "<p3>" + r["precio3"].ToString() + "</p3>";
                        sXML += "<p4>" + r["precio4"].ToString() + "</p4></data>";
                    }
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Page_load", ref ex);
                }
            }
            Response.Clear();
            Response.ContentType = "text/xml";
            Response.Write(sXML);
            Response.End();
        }
    }
}
