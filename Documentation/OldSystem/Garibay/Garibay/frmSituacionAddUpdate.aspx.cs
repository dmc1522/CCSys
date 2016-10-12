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
    public partial class frmSituacionAddUpdate : BasePage
    {
        public frmSituacionAddUpdate()
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.pnlAddNewComment.Visible = false;
                this.lblFecha.Text = Utils.Now.ToString("dd/MM/yyyy hh:mm:ss tt");

                if (this.LoadEncryptedQueryString() > 0 && this.myQueryStrings["situacionID"] != null && int.Parse(this.myQueryStrings["situacionID"].ToString()) > 0)
                {
                    this.txtSituacionID.Text = this.myQueryStrings["situacionID"].ToString();
                    this.LoadSituacion();
                    this.btnAgregar.Visible = false;
                    this.pnlAddNewComment.Visible = true;
                    this.pnlSituacionHistory.Visible = true;
                    this.gvHistory.DataBind();
                }
            }
        }

        private void LoadSituacion()
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand comm = new SqlCommand();
                conn.Open();
                comm.Connection = conn;
                comm.CommandText = "SELECT situacionID, fecha, titulo, descripcion, userID, activa FROM Situacion where situacionID = @situacionID;";
                comm.Parameters.Add("@situacionID", SqlDbType.Int).Value = this.txtSituacionID.Text;
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    this.lblFecha.Text = ((DateTime)reader[1]).ToString("dd/MM/yyyy hh:mm:ss tt");
                    this.txtTitulo.Text = reader[2].ToString();
                    this.txtDescripcion.Text = reader[3].ToString();
                    this.chkActivo.Checked = ((bool)reader[5]);
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "no se pudo consultar la solicitud: " + this.txtSituacionID.Text, ref ex);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand comm = new SqlCommand();
            try
            {
                conn.Open();
                comm.Connection = conn;
                comm.CommandText = "insert into situacion(titulo,descripcion,userID,activa) "
                        + " values(@titulo,@descripcion,@userID,@activa); SELECT NewID = SCOPE_IDENTITY();";
                comm.Parameters.Add("@titulo", SqlDbType.VarChar).Value = this.txtTitulo.Text;
                comm.Parameters.Add("@descripcion", SqlDbType.Text).Value = this.txtDescripcion.Text;
                comm.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                comm.Parameters.Add("@activa", SqlDbType.Bit).Value = this.chkActivo.Checked ? 1 : 0;
                int newID = int.Parse(comm.ExecuteScalar().ToString());
                if (newID <= 0)
                {
                    throw new Exception("No se pudo agregar la situacion nueva.");
                }
                conn.Close();
                try
                {
                    String sTo = "cheliskis@gmail.com,patriciagaribay@corporativogaribay.com,melvinquintero@hotmail.com";
                    String sBody = "<html><body>Se agrego una nueva situacion con los siguientes datos:<br />"
                        + "<b>Fecha: </b>" + this.lblFecha.Text.ToUpper() + "<br />"
                        + "<b>Titulo: </b>" + this.txtTitulo.Text.ToUpper() + "<br />"
                        + "<b>Descripcion: </b>" + this.txtDescripcion.Text.ToUpper() + "<br />"
                        + "<b>Generado por: </b>" + this.CurrentUserName.ToUpper()
                        + "</body></html>";
                    EMailUtils.SendTextEmail("noreply@corporativogaribay.com", "Se Agrego nueva situacion: " + this.txtTitulo.Text.ToUpper(), sBody, true, sTo);
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "error enviando mail de situacion", ref ex);
                }
                
                Response.Redirect("frmSituacionAddUpdate.aspx" + Utils.GetEncriptedQueryString("situacionID=" + newID.ToString()));
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error agregando situacion", ref ex);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnAddHistory_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand comm = new SqlCommand();
            try
            {
                conn.Open();
                comm.Connection = conn;
                comm.CommandText = "INSERT INTO situacionHistory (descripcion, userID, situacionID) VALUES (@descripcion,@userID,@situacionID)";
                comm.Parameters.Add("@descripcion", SqlDbType.Text).Value = this.txtHistoryDescripcion.Text;
                comm.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                comm.Parameters.Add("@situacionID", SqlDbType.Int).Value = this.txtSituacionID.Text;
                int newID = comm.ExecuteNonQuery();
                if (newID <= 0)
                {
                    throw new Exception("No se pudo agregar la descripcion");
                }
                try
                {
                    String sTo = "cheliskis@gmail.com,patriciagaribay@corporativogaribay.com,melvinquintero@hotmail.com";
                    String sBody = "<html><body>Se agrego una nueva Actualizacion de situacion con los siguientes datos:<br />"
                        + "<b>Fecha: </b>" + Utils.Now.ToString("dd/MM/yyyy")
                        + "<b>Descripcion: </b>" + this.txtHistoryDescripcion.Text.ToUpper() + "<br />"
                        + "<b>Generado por: </b>" + this.CurrentUserName.ToUpper()
                        + "</body></html>";
                    EMailUtils.SendTextEmail(sTo, "Se Actualizacion a situacion: " + this.txtTitulo.Text.ToUpper(), sBody, true);
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "error enviando mail de situacion", ref ex);
                }
                this.txtHistoryDescripcion.Text = "";
                this.gvHistory.DataBind();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error agregando situacion", ref ex);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand comm = new SqlCommand();
            try
            {
                conn.Open();
                comm.Connection = conn;
                comm.CommandText = "UPDATE Situacion SET titulo = @titulo, descripcion = @descripcion, "
                    + " activa = @activa  WHERE     (situacionID = @situacionID)";
                comm.Parameters.Add("@titulo", SqlDbType.VarChar).Value = this.txtTitulo.Text;
                comm.Parameters.Add("@descripcion", SqlDbType.Text).Value = this.txtDescripcion.Text;
                comm.Parameters.Add("@activa", SqlDbType.Bit).Value = this.chkActivo.Checked ? 1 : 0;
                comm.Parameters.Add("@situacionID", SqlDbType.Int).Value = this.txtSituacionID.Text;
                int iRowsUpdated = comm.ExecuteNonQuery();
                if (iRowsUpdated != 1)
                {
                    throw new Exception("No se pudo actualizar la situacion, ID = " + this.txtSituacionID.Text);
                }
                conn.Close();
                try
                {
                    String sTo = "cheliskis@gmail.com,mariaangelica@corporativogaribay.com,MERCEDESDEANDA@CORPORATIVOGARIBAY.COM,LUZMARIADEANDA@CORPORATIVOGARIBAY.COM,patriciagaribay@corporativogaribay.com,hec_mol@hotmail.com,melvinquintero@hotmail.com";
                    String sBody = "<html><body>Se Actualizo la situacion con los siguientes datos:<br />"
                        + "<b>Fecha: </b>" + this.lblFecha.Text.ToUpper() + "<br />"
                        + "<b>Titulo: </b>" + this.txtTitulo.Text.ToUpper() + "<br />"
                        + "<b>Descripcion: </b>" + this.txtDescripcion.Text.ToUpper() + "<br />"
                        + "<b>Modificado por: </b>" + this.CurrentUserName.ToUpper()
                        + "</body></html>";
                    EMailUtils.SendTextEmail(sTo, "Se Modifico la situacion: " + this.txtTitulo.Text.ToUpper(), sBody, true);
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "error enviando mail de situacion", ref ex);
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "error modificando situacion", ref ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
