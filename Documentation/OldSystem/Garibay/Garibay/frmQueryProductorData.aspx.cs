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
    public partial class frmQueryProductorData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                        String sXML = "";

            if (this.Request.Params["ProductorID"] != null && this.Request.Params["ProductorID"].Length > 0)
            {
                SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand comm = new SqlCommand();
                try
                {
                    conn.Open();
                    comm.Connection = conn;
                    string qrySel = "SELECT     Productores.municipio, Productores.domicilio, Productores.poblacion, Estados.estado, Productores.telefono, Productores.celular, Productores.IFE,  apaterno + ' ' +amaterno+ ' '+nombre as nombre ";
                     qrySel+= "FROM         Productores INNER JOIN Estados ON Productores.estadoID = Estados.estadoID WHERE productorID=@prodID";

                    comm.CommandText = qrySel;
                    comm.Parameters.Add("@prodID", SqlDbType.Int).Value = this.Request.Params["ProductorID"].ToString();
                    SqlDataAdapter sqlDA = new SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    ds.Tables.Clear();

                    if (sqlDA.Fill(ds) >0)
                    {
                        sXML = "<data>";
                        //sXML += "<ID>" + ds.Tables[0].Rows[0]["clienteventaID"].ToString() + "</ID>" ;
                        sXML += "<destino>" + ds.Tables[0].Rows[0]["municipio"].ToString() + "</destino>";
                        sXML += "<municipio>" + ds.Tables[0].Rows[0]["municipio"].ToString() + "</municipio>" ;
                        sXML += "<domicilio>" + ds.Tables[0].Rows[0]["domicilio"].ToString() + "</domicilio>" ;
                        sXML += "<poblacion>" + ds.Tables[0].Rows[0]["poblacion"].ToString() + "</poblacion>" ;
                        sXML += "<estado>" + ds.Tables[0].Rows[0]["estado"].ToString() + "</estado>" ;
                        sXML += "<telefono>" + ds.Tables[0].Rows[0]["telefono"].ToString() + "</telefono>" ;
                        sXML += "<celular>" + ds.Tables[0].Rows[0]["celular"].ToString() + "</celular>";
                        sXML += "<IFE>" + ds.Tables[0].Rows[0]["IFE"].ToString() + "</IFE>";
                        sXML += "<NOMBRE>" + ds.Tables[0].Rows[0]["nombre"].ToString() + "</NOMBRE>";
                        
                        sXML += "</data>" ;
                    }
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "ERR consultando datos de Producto", this.Request.Url.ToString(), ref ex);
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

