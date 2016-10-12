using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
namespace Garibay
{
    public partial class frmDocsProductor : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                this.PanelActionResult.Visible = false;
                this.btnEliminar.Visible = false;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.DOCUMENTOS, Logger.typeUserActions.SELECT, int.Parse(Session["USERID"].ToString()), "VISITÓ LA PAGINA DE DOCUMENTOS DE PRODUCTOR");
            }
            this.PanelActionResult.Visible = this.PanelActionResult.Visible ? false : false;
            this.gvDocumentos.DataSourceID = this.gvDocumentos.DataSourceID;
            this.compruebasecurityLevel();
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
            }

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            byte[] bytesArchivo = null;
            try
            {
                if(this.filUpArchivo.HasFile)
                {
                    sqlConn.Open();
                    SqlCommand sqlComm = new SqlCommand();
                    sqlComm.Connection = sqlConn;
                    String sQuery = "INSERT INTO Documents (productorID, documentName, filename, contentType) VALUES (@productorID,@documentName,@filename, @contentType)";
                    sqlComm.CommandText = sQuery;
                    sqlComm.Parameters.Add("@productorID", System.Data.SqlDbType.Int).Value = int.Parse(this.ddlProductor.SelectedValue);
                    //sqlComm.Parameters.Add("@documentTypeID", System.Data.SqlDbType.Int).Value = int.Parse(this.ddlDocType.SelectedValue);
                    sqlComm.Parameters.Add("@documentName", System.Data.SqlDbType.NVarChar).Value = this.txtNombreDoc.Text;
//                     using (BinaryReader reader = new BinaryReader(this.filUpArchivo.PostedFile.InputStream))
//                     {
//                         bytesArchivo = reader.ReadBytes(this.filUpArchivo.PostedFile.ContentLength);
//                     }

                    sqlComm.Parameters.Add("@filename", System.Data.SqlDbType.VarChar).Value = this.filUpArchivo.FileName;
                    sqlComm.Parameters.Add("@contentType", System.Data.SqlDbType.VarChar).Value = this.filUpArchivo.PostedFile.ContentType;
                    
                    if (sqlComm.ExecuteNonQuery() > 0)
                    {
                        String sProdID = this.ddlProductor.SelectedValue;
                        String sFilePath = Server.MapPath("~/");
                        sFilePath += "ProdDocs\\" + sProdID + "\\";
                        if (!Directory.Exists(sFilePath))
                        {
                            Directory.CreateDirectory(sFilePath);
                        }
                        sFilePath += this.filUpArchivo.FileName;
                        //Logger.Instance.LogMessage(Logger.typeLogMessage.DEBUG, Logger.typeUserActions.INSERT, this.UserID, "path: " + sFilePath, this.Request.Url.ToString());//
                        Byte []bytes = new Byte[this.filUpArchivo.PostedFile.ContentLength];
                        this.filUpArchivo.PostedFile.InputStream.Read(bytes, 0, this.filUpArchivo.PostedFile.ContentLength);

                        FileStream newFile = new FileStream(sFilePath, FileMode.Create);

                        // Write data to the file

                        newFile.Write(bytes, 0, bytes.Length);

                        // Close file

                        newFile.Close();



                        string data = "AGREGÓ EL DOCUMENTO: " + this.txtNombreDoc.Text + " AL PRODUCTOR: "+ this.ddlProductor.SelectedItem.Text;
                        Logger.Instance.LogUserSessionRecord(Logger.typeModulo.DOCUMENTOS, Logger.typeUserActions.INSERT, int.Parse(Session["USERID"].ToString()), data );
                        Logger.Instance.LogMessage(Logger.typeLogMessage.DEBUG, Logger.typeUserActions.INSERT, -1, data, this.Request.Url.ToString());
                        this.PanelActionResult.Visible = true;
                        this.lblMensajetitle.Text = "DOCUMENTO AGREGADO EXITOSAMENTE";
                        this.lblMensajeOperationresult.Visible = false;
                        this.lblMensajeException.Visible = false;
                        this.imagenmal.Visible = !(this.imagenbien.Visible = true);
                        this.txtNombreDoc.Text = "";
                        int oldindex = this.ddlProductor.SelectedIndex;
                        this.ddlProductor.DataBind();
                        this.ddlProductor.SelectedIndex = oldindex;
                    }
                }
            }
            catch(SqlException ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.DEBUG, Logger.typeUserActions.INSERT, -1, "Error SQL: " +ex.Message, this.Request.Url.ToString());
                this.PanelActionResult.Visible = true;
                this.lblMensajetitle.Text = "FALLÓ";
                this.lblMensajeOperationresult.Visible = true;
                this.lblMensajeException.Visible = true;
                this.imagenmal.Visible = !(this.imagenbien.Visible = true);
                this.lblMensajeOperationresult.Text = "EL DOCUMENTO NO PUDO SER AGREGADO";
                this.lblMensajeException.Text = "Error SQL: " +ex.Message;
            }
            catch (System.Exception ex)
            {
                this.PanelActionResult.Visible = true;
                this.lblMensajetitle.Text = "FALLÓ";
                this.lblMensajeOperationresult.Visible = true;
                this.lblMensajeException.Visible = true;
                this.imagenmal.Visible = !(this.imagenbien.Visible = true);
                this.lblMensajeOperationresult.Text = "EL DOCUMENTO NO PUDO SER AGREGADO";
                this.lblMensajeException.Text = "CRITICO: " + ex.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.DEBUG, Logger.typeUserActions.INSERT, -1, "Error: " +ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                sqlConn.Close();
                this.DataBind();
            }
        }



        protected void ddlProductor_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvDocumentos.DataBind();
        }

        protected void gvDocumentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.gvDocumentos.SelectedIndex > -1)
            {
                string mensaje;
                mensaje = "return confirm('¿Desea eliminar el documento " + this.gvDocumentos.SelectedDataKey["filename"].ToString().ToUpper() + "?. ";
                mensaje += "')";
                this.btnEliminar.Attributes.Add("onclick", mensaje);
                this.btnEliminar.Visible = true;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.CommandText = "delete from Documents where docID = @docID and productorID = @productorID";
                sqlComm.Parameters.AddWithValue("@docID", this.gvDocumentos.SelectedDataKey["docID"].ToString());
                sqlComm.Parameters.AddWithValue("@productorID", this.ddlProductor.SelectedValue);
                sqlComm.Connection = sqlConn;
                sqlConn.Open();

                if (sqlComm.ExecuteNonQuery() == 1)
                {
                    string data = "ELIMINÓ EL DOCUMENTO: " + this.gvDocumentos.SelectedDataKey["filename"].ToString() + " AL PRODUCTOR: " + this.ddlProductor.SelectedItem.Text;
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.DOCUMENTOS, Logger.typeUserActions.DELETE, int.Parse(Session["USERID"].ToString()), data );
                    Logger.Instance.LogMessage(Logger.typeLogMessage.DEBUG, Logger.typeUserActions.DELETE, -1, data, this.Request.Url.ToString());
                    this.PanelActionResult.Visible = true;
                    this.lblMensajetitle.Text = "DOCUMENTO ELIMINADO EXITOSAMENTE";
                    this.lblMensajeOperationresult.Visible = false;
                    this.lblMensajeException.Visible = false;
                    this.imagenmal.Visible = !(this.imagenbien.Visible = true);
                    this.txtNombreDoc.Text = "";
                    int oldindex = this.ddlProductor.SelectedIndex;
                    this.ddlProductor.DataBind();
                    this.ddlProductor.SelectedIndex = oldindex;
                    this.gvDocumentos.SelectedIndex = -1;
                    this.btnEliminar.Visible = false;
                }
            }
            catch (SqlException ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.DEBUG, Logger.typeUserActions.DELETE, -1, "Error SQL: " + ex.Message, this.Request.Url.ToString());
                this.PanelActionResult.Visible = true;
                this.lblMensajetitle.Text = "FALLÓ";
                this.lblMensajeOperationresult.Visible = true;
                this.lblMensajeException.Visible = true;
                this.imagenmal.Visible = !(this.imagenbien.Visible = true);
                this.lblMensajeOperationresult.Text = "EL DOCUMENTO NO PUDO SER ELIMINADO";
                this.lblMensajeException.Text = "Error SQL: " + ex.Message;
            }
            catch (System.Exception ex)
            {
                this.PanelActionResult.Visible = true;
                this.lblMensajetitle.Text = "FALLÓ";
                this.lblMensajeOperationresult.Visible = true;
                this.lblMensajeException.Visible = true;
                this.imagenmal.Visible = !(this.imagenbien.Visible = true);
                this.lblMensajeOperationresult.Text = "EL DOCUMENTO NO PUDO SER ELIMINADO";
                this.lblMensajeException.Text = "CRITICO: " + ex.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.DEBUG, Logger.typeUserActions.DELETE, -1, "Error: " + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                sqlConn.Close();
                this.DataBind();
            }
        }

        protected void gvDocumentos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            HyperLink lnk = (HyperLink)e.Row.FindControl("lnkDownload");
            if (lnk != null)
            {
                lnk.NavigateUrl = this.Request.Url.ToString();
                JSUtils.OpenNewWindowOnClick(ref lnk, "frmDescargar.aspx" + Utils.GetEncriptedQueryString("docID=" + this.gvDocumentos.DataKeys[e.Row.RowIndex]["docID"].ToString()), "Descargar", true);
            }
        }

        protected void gvDocumentos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.CommandText = "delete from Documents where docID = @docID and productorID = @productorID";
                sqlComm.Parameters.Add("@docID", System.Data.SqlDbType.Int).Value = this.gvDocumentos.DataKeys[e.RowIndex]["docID"].ToString();
                sqlComm.Parameters.Add("@productorID", System.Data.SqlDbType.Int).Value =  this.ddlProductor.SelectedValue;
                sqlComm.Connection = sqlConn;
                sqlConn.Open();

                if (sqlComm.ExecuteNonQuery() == 1)
                {
                    String sFilePath = "";
                    try
                    {
                        String sProdID = this.ddlProductor.SelectedValue;
                        sFilePath = Server.MapPath("~/");
                        sFilePath += "ProdDocs\\" + sProdID + "\\";
                        sFilePath += this.gvDocumentos.DataKeys[e.RowIndex]["filename"].ToString();
                        File.Delete(sFilePath);
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.DELETE, "error eliminando archivo :" + sFilePath, ref ex);
                    }
                    string data = "ELIMINÓ EL DOCUMENTO: " + this.gvDocumentos.DataKeys[e.RowIndex]["filename"].ToString() + " AL PRODUCTOR: " + this.ddlProductor.SelectedItem.Text;
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.DOCUMENTOS, Logger.typeUserActions.DELETE, int.Parse(Session["USERID"].ToString()), data);
                    Logger.Instance.LogMessage(Logger.typeLogMessage.DEBUG, Logger.typeUserActions.DELETE, -1, data, this.Request.Url.ToString());
                    this.PanelActionResult.Visible = true;
                    this.lblMensajetitle.Text = "DOCUMENTO ELIMINADO EXITOSAMENTE";
                    this.lblMensajeOperationresult.Visible = false;
                    this.lblMensajeException.Visible = false;
                    this.imagenmal.Visible = !(this.imagenbien.Visible = true);
                    this.txtNombreDoc.Text = "";
                    int oldindex = this.ddlProductor.SelectedIndex;
                    this.ddlProductor.DataBind();
                    this.ddlProductor.SelectedIndex = oldindex;
                    this.gvDocumentos.SelectedIndex = -1;
                    this.gvDocumentos.EditIndex = -1;
                }
            }
            catch (SqlException ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.DEBUG, Logger.typeUserActions.DELETE, -1, "Error SQL: " + ex.Message, this.Request.Url.ToString());
                this.PanelActionResult.Visible = true;
                this.lblMensajetitle.Text = "FALLÓ";
                this.lblMensajeOperationresult.Visible = true;
                this.lblMensajeException.Visible = true;
                this.imagenmal.Visible = !(this.imagenbien.Visible = true);
                this.lblMensajeOperationresult.Text = "EL DOCUMENTO NO PUDO SER ELIMINADO";
                this.lblMensajeException.Text = "Error SQL: " + ex.Message;
            }
            catch (System.Exception ex)
            {
                this.PanelActionResult.Visible = true;
                this.lblMensajetitle.Text = "FALLÓ";
                this.lblMensajeOperationresult.Visible = true;
                this.lblMensajeException.Visible = true;
                this.imagenmal.Visible = !(this.imagenbien.Visible = true);
                this.lblMensajeOperationresult.Text = "EL DOCUMENTO NO PUDO SER ELIMINADO";
                this.lblMensajeException.Text = "CRITICO: " + ex.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.DEBUG, Logger.typeUserActions.DELETE, -1, "Error: " + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                sqlConn.Close();
                this.DataBind();
            }
        }
    }
}
