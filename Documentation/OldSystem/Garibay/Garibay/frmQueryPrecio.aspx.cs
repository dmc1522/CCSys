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
using System.Data.SqlClient;

namespace Garibay
{
    public partial class frmQueryPrecio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             float precioaux = 0.0f;

            String precio = "0";
            
            if (this.Request.Params["prodID"] != null && this.Request.Params["prodID"].Length > 0 &&
                this.Request.Params["precioID"] != null && this.Request.Params["precioID"].Length > 0 )
            {

                SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand comm = new SqlCommand();
                try
                {

                    int prodID;
                    int precioID;
                    if(int.TryParse(this.Request.Params["prodID"].ToString(),out prodID)&&int.TryParse(this.Request.Params["precioID"].ToString(),out precioID)){


                        switch(precioID){
                            case 1:
                                comm.CommandText = "SELECT precio1 FROM Productos WHERE (productoID=@productoID)";

                                break;
                            case 2:
                                comm.CommandText = "SELECT precio2 FROM Productos WHERE (productoID=@productoID)";

                                break;
                            case 3:
                                comm.CommandText = "SELECT precio3 FROM Productos WHERE (productoID=@productoID)";

                                break;
                            case 4:
                                comm.CommandText = "SELECT precio4 FROM Productos WHERE (productoID=@productoID)";

                                break;



                        }

                        conn.Open();
                    comm.Connection = conn;
              
                    //comm.Parameters.Add("@cicloID", SqlDbType.Int).Value = this.Request.Params["cicloID"].ToString();
                    SqlDataReader r;
                    
                    
           
                    

                    comm.Parameters.Add("@productoID", SqlDbType.Int).Value = prodID;
                    
                    r = comm.ExecuteReader();
                    
                    if (r.HasRows && r.Read() && r[0] != null && float.TryParse(r[0].ToString(), out precioaux))
                    {
                        precio = string.Format("{0:n}", precioaux);
                    }
                    }
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Frm_load", ref ex);
                }
            }
            Response.Clear();
            Response.Write(precio);
            Response.End();
        }

       

    }
}
