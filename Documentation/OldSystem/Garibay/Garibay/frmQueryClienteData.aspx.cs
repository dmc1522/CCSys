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
    public partial class frmQueryClienteData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String sXML = "";

            if (this.Request.Params["clienteID"] != null && this.Request.Params["clienteID"].Length > 0)
            {
                SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand comm = new SqlCommand();
                try
                {
                    conn.Open();
                    comm.Connection = conn;
                    comm.CommandText = "SELECT        ClientesVentas.clienteventaID, ClientesVentas.nombre, ClientesVentas.domicilio, ClientesVentas.ciudad, ClientesVentas.telefono, ClientesVentas.RFC,  Estados.estado, ClientesVentas.colonia, ClientesVentas.CP FROM            ClientesVentas INNER JOIN Estados ON ClientesVentas.estadoID = Estados.estadoID where ClientesVentas.clienteventaID = @clienteventaID";
                    comm.Parameters.Add("@clienteventaID", SqlDbType.Int).Value = this.Request.Params["clienteID"].ToString();
                    SqlDataAdapter sqlDA = new SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    ds.Tables.Clear();

                    if (sqlDA.Fill(ds) >0)
                    {
                        sXML = "<data>";
                        sXML += "<ID>" + ds.Tables[0].Rows[0]["clienteventaID"].ToString() + "</ID>" ;
                        sXML += "<name>" + ds.Tables[0].Rows[0]["nombre"].ToString() + "</name>" ;
                        sXML += "<address>" + ds.Tables[0].Rows[0]["domicilio"].ToString() + "</address>" ;
                        sXML += "<city>" + ds.Tables[0].Rows[0]["ciudad"].ToString() + "</city>" ;
                        sXML += "<phone>" + ds.Tables[0].Rows[0]["telefono"].ToString() + "</phone>" ;
                        sXML += "<rfc>" + ds.Tables[0].Rows[0]["RFC"].ToString() + "</rfc>" ;
                        sXML += "<state>" + ds.Tables[0].Rows[0]["estado"].ToString() + "</state>";
                        sXML += "<col>" + ds.Tables[0].Rows[0]["colonia"].ToString() + "</col>";
                        sXML += "<cp>" + ds.Tables[0].Rows[0]["CP"].ToString() + "</cp>";
                        sXML += "</data>" ;
                    }
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "ERR consultando datos de cliente", this.Request.Url.ToString(), ref ex);
                }
                finally
                {
                    conn.Close();
                }
            }
            Response.Clear();
            Response.ContentType = "text/xml";
            Response.Write(sXML);
            Response.End();
        }
    }
}
