using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace Garibay
{
    public partial class frmCiclos : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                this.Agregando(false, true);
                btnModificarLista.Visible = false;
                btnEliminar.Visible = false;
                try
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CICLOS, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "SE VISITÓ LA PÁGINA DE CICLOS");
                }
                catch (Exception exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());

                }
            }
            if(this.panelmensaje.Visible==true)
            {
                this.panelmensaje.Visible = false;
            }
            this.gridCiclos.DataSourceID = "SqlDataSource1";
            this.compruebasecurityLevel();
        }
        protected void compruebasecurityLevel()
        {
               if (this.SecurityLevel == 4)
                {
                    this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
                }
            
        }


        protected void Agregando(Boolean activaacgregar, Boolean semuestrbotonaagregar)
        {
            this.pnlNuevo_ciclo.Visible = activaacgregar;
            this.btnAgregarLista.Visible = !activaacgregar;
            this.btnModificarLista.Visible = !activaacgregar;
            this.btnEliminar.Visible = !activaacgregar;
            this.btnAceptar.Visible = semuestrbotonaagregar;
            this.btnModificarCiclo.Visible = !semuestrbotonaagregar;
            this.gridCiclos.Columns[0].Visible = !activaacgregar;
            if (this.gridCiclos.SelectedIndex == -1)
            {
                this.btnModificarLista.Visible = false;
                this.btnEliminar.Visible = false;
            }

        }

        protected void PopCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            this.txtInicioCiclo.Text = this.PopCalendar1.SelectedDate;
        }

        protected void PopCalendar2_SelectionChanged(object sender, EventArgs e)
        {
            this.txtFinCiclo.Text = this.PopCalendar2.SelectedDate;
        }

        protected void PopCalendar3_SelectionChanged(object sender, EventArgs e)
        {
            this.txtFinCiclo2.Text = this.PopCalendar3.SelectedDate;
        }

        protected void btnAgregarLista_Click(object sender, EventArgs e)
        {
            this.lblCiclos.Text = "AGREGAR CICLO";
            this.Agregando(true, true);
            this.limpiarCampos();
            this.gridCiclos.SelectedIndex = -1;
        }

        protected void btnModificarLista_Click(object sender, EventArgs e)
        {
            this.lblCiclos.Text = "MODIFICAR CICLO";
            this.Agregando(true, false);
        }

        protected void cmdCancelar_Click(object sender, EventArgs e)
        {

            this.Agregando(false, true);
        }

        protected void gridCiclos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pnlNuevo_ciclo.Visible == false)
            {
                this.btnEliminar.Visible = true;
                this.btnModificarLista.Visible = true;
            }
            this.txtNombreCiclo.Text = this.gridCiclos.SelectedDataKey["CicloName"].ToString();
            this.txtInicioCiclo.Text = Utils.converttoshortFormatfromdbFormat(this.gridCiclos.SelectedDataKey["fechaInicio"].ToString());
            this.txtFinCiclo.Text = Utils.converttoshortFormatfromdbFormat(this.gridCiclos.SelectedDataKey["fechaFinZona1"].ToString());
            this.txtFinCiclo2.Text = Utils.converttoshortFormatfromdbFormat(this.gridCiclos.SelectedDataKey["fechaFinZona2"].ToString());
            this.TxtMontoXhec.Text = Utils.conviertedemonadouble(this.gridCiclos.SelectedDataKey["Montoporhectarea"].ToString());
            this.chkCiclo_Cerrado.Checked = (bool)this.gridCiclos.SelectedDataKey["cerrado"];
            string msgDel = "return confirm('¿Realmente desea eliminar el ciclo: ";
            msgDel += this.gridCiclos.SelectedDataKey["CicloName"].ToString();
            msgDel += "?')";
            this.btnEliminar.Attributes.Add("onclick", msgDel);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string qryIns = "INSERT INTO Ciclos VALUES (@CicloName,@fechaInicio,@fechaFinZona1,@fechaFinZona2,@Montoporhectarea,@cerrado)";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            try
            {
                cmdIns.Parameters.Add("@CicloName", SqlDbType.VarChar).Value = this.txtNombreCiclo.Text;
                cmdIns.Parameters.Add("@fechaInicio", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(this.txtInicioCiclo.Text);
                cmdIns.Parameters.Add("@fechaFinZona1", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(this.txtFinCiclo.Text);
                cmdIns.Parameters.Add("@fechaFinZona2", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(this.txtFinCiclo2.Text);
                cmdIns.Parameters.Add("@Montoporhectarea", SqlDbType.Money).Value = float.Parse(this.TxtMontoXhec.Text);
                cmdIns.Parameters.Add("@cerrado", SqlDbType.Bit).Value = this.chkCiclo_Cerrado.Checked;


                conGaribay.Open();
                int numregistros = cmdIns.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("CICLOEXECUTEFAILED"), "AGREGADO", "AGREGARON", numregistros.ToString()));
                }

                this.Agregando(false, true);
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = false;
                this.imagenbien.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CICLOADDEDEXITO"),this.txtNombreCiclo.Text.ToUpper());
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CICLOS, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("SE AGREGÓ EL CICLO: " + this.txtNombreCiclo.Text.ToUpper()));
                this.limpiarCampos();
            }
            catch (InvalidOperationException err1)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CICLOADDEDFAILED"), this.txtNombreCiclo.Text.ToUpper());
                this.lblMensajeException.Text = err1.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());
            }
            catch (SqlException err2)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CICLOADDEDFAILED"), this.txtNombreCiclo.Text.ToUpper());
                this.lblMensajeException.Text = err2.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());

            }
            catch (Exception err3)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CICLOADDEDFAILED"), this.txtNombreCiclo.Text.ToUpper());
                this.lblMensajeException.Text = err3.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), err3.Message, this.Request.Url.ToString());

            }
            finally
            {
                conGaribay.Close();
            }

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string qryDel = "DELETE FROM Ciclos WHERE cicloID=@cicloID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdDel = new SqlCommand(qryDel, conGaribay);

            try
            {
                cmdDel.Parameters.Add("@cicloID", SqlDbType.Int).Value = (int)this.gridCiclos.SelectedDataKey["cicloID"];

                conGaribay.Open();
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = false;
                this.imagenbien.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CICLODELETEDEXITO"), this.gridCiclos.SelectedDataKey["CicloName"].ToString().ToUpper());
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                int numregistros = cmdDel.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("CICLOEXECUTEFAILED"), "ELIMINADO", "ELIMINARON", numregistros.ToString()));
                }
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CICLOS, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ EL CICLO: " + this.txtNombreCiclo.Text.ToUpper()));
            }
            catch (InvalidOperationException err1)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CICLODELETEDFAILED"), this.gridCiclos.SelectedDataKey["CicloName"].ToString().ToUpper());
                this.lblMensajeException.Text = err1.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());

            }
            catch (SqlException err2)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CICLODELETEDFAILED"), this.gridCiclos.SelectedDataKey["CicloName"].ToString().ToUpper());
                this.lblMensajeException.Text = err2.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());

            }
            catch (Exception err3)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CICLODELETEDFAILED"), this.gridCiclos.SelectedDataKey["CicloName"].ToString().ToUpper());
                this.lblMensajeException.Text = err3.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err3.Message, this.Request.Url.ToString());
            }
            finally
            {
                conGaribay.Close();
                this.gridCiclos.DataBind();
                if(this.gridCiclos.Rows.Count<1)
                {
                    this.btnEliminar.Visible = false;
                    this.btnModificarLista.Visible = false;
                }
            }
        }

        protected void btnModificarCiclo_Click(object sender, EventArgs e)
        {
            string qryUp = "UPDATE Ciclos SET CicloName = @CicloName, fechaInicio = @fechaInicio, fechaFinZona1 = @fechaFinZona1, fechaFinZona2 = @fechaFinZona2, Montoporhectarea = @Montoporhectarea, cerrado = @cerrado WHERE cicloID = @cicloID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdUp = new SqlCommand(qryUp, conGaribay);
            if (this.gridCiclos.SelectedDataKey["CicloName"].ToString() != this.txtNombreCiclo.Text)
            {
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CICLOS, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ EL NOMBRE DEL CICLO: \"" +this.gridCiclos.SelectedDataKey["CicloName"].ToString().ToUpper()+"\" POR: \""+ this.txtNombreCiclo.Text.ToUpper())+"\"");
            }
            try
            {              
                cmdUp.Parameters.Add("@CicloName", SqlDbType.NVarChar).Value = this.txtNombreCiclo.Text;
                cmdUp.Parameters.Add("@fechaInicio", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(this.txtInicioCiclo.Text);
                cmdUp.Parameters.Add("@fechaFinZona1", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(this.txtFinCiclo.Text);
                cmdUp.Parameters.Add("@fechaFinZona2", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(this.txtFinCiclo2.Text);
                cmdUp.Parameters.Add("@Montoporhectarea", SqlDbType.Float).Value = float.Parse(this.TxtMontoXhec.Text);
                cmdUp.Parameters.Add("@cerrado", SqlDbType.Bit).Value = this.chkCiclo_Cerrado.Checked;
                cmdUp.Parameters.Add("@cicloID", SqlDbType.Int).Value = this.gridCiclos.SelectedDataKey.Value;

                conGaribay.Open();
                int numregistros = cmdUp.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("CICLOEXECUTEFAILED"), "MODIFICADO", "MODIFICARON", numregistros.ToString()));
                }
                this.Agregando(false, true);
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = false;
                this.imagenbien.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CICLOMODIFIEDEXITO"), this.txtNombreCiclo.Text.ToUpper());
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CICLOS, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ EL CICLO: " + this.txtNombreCiclo.Text.ToUpper()));
            }
            catch (InvalidOperationException err1)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CICLOMODIFIEDFAILED"), this.txtNombreCiclo.Text.ToUpper());
                this.lblMensajeException.Text = err1.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());
            }
            catch (SqlException err2)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CICLOMODIFIEDFAILED"), this.txtNombreCiclo.Text.ToUpper());
                this.lblMensajeException.Text = err2.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());
            }
            catch (Exception err3)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CICLOMODIFIEDFAILED"), this.txtNombreCiclo.Text.ToUpper());
                this.lblMensajeException.Text = err3.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err3.Message, this.Request.Url.ToString());
            }
            finally
            {
                conGaribay.Close();
            }
        }

        protected void limpiarCampos()
        {
            this.txtNombreCiclo.Text = "";
            this.txtInicioCiclo.Text = "";
            this.txtFinCiclo.Text = "";
            this.txtFinCiclo2.Text = "";
            this.TxtMontoXhec.Text = "";
            this.chkCiclo_Cerrado.Checked = false;
        }

    }
}
