using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;

namespace Garibay
{
    public partial class frmDescargar : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.LoadEncryptedQueryString() <= 0 || this.myQueryStrings["docID"] == null)
            {
                Response.Clear();
                Response.Write("NO EXISTE EL DOCUMENTO SELECCIONADO");
                Response.End();
            }
            else
            {
                SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
                try
                {
                    SqlCommand sqlComm = new SqlCommand();
                    sqlComm.CommandText = "SELECT documentName, productorID, filename, contentType, docID FROM Documents where (docID = @docID)";
                    sqlComm.Connection = sqlConn;
                    sqlConn.Open();
                    sqlComm.Parameters.Clear();
                    sqlComm.Parameters.Add("@docID", System.Data.SqlDbType.Int).Value = this.myQueryStrings["docID"].ToString();
                    SqlDataReader sqlReader = sqlComm.ExecuteReader();
                    if (sqlReader.HasRows && sqlReader.Read())
                    {
                        String sProdID = sqlReader["productorID"].ToString();
                        String sFilePath = Server.MapPath("~/");
                        sFilePath += "ProdDocs\\" + sProdID + "\\";
                        sFilePath += sqlReader["filename"].ToString();
                        if (File.Exists(sFilePath))
                        {
                            Response.ClearHeaders();
                            Response.Clear();
                            Response.ContentType = sqlReader["contentType"].ToString();
                            Response.ContentType = "application/octet-stream";
                            Response.AddHeader("content-disposition", "attachment;filename=" + sqlReader["filename"].ToString());

                            //Response.Flush();

                            //Response.WriteFile(sFilePath);
                            Response.TransmitFile(sFilePath);
                            Response.Flush();
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Page_load", ref ex);
                }
                finally
                {
                    sqlConn.Close();
                }
            }
        }
    }
}
