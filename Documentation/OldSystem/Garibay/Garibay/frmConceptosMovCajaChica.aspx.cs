using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;



namespace Garibay
{
    public partial class WebForm3 : Garibay.BasePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.grdvlista_conceptosCajaChica.DataSourceID = "sqldtsrcConCajaChica";
            if (!this.IsPostBack)
            {
                this.Agregando(false, true);
                
                try
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CONCEPTOSCAJACHICA, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "SE VISITÓ LA PÁGINA DE CONCEPTOS DE MOVIMIENTOS DE CAJA CHICA");
                }
                catch (Exception exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());

                }
            }
            if (this.panelmensaje.Visible == true)
            {
                this.panelmensaje.Visible = false;
            }
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.btnAceptar.Visible = false;
                this.btnModificar.Visible = false;

                this.btnCancelar.Visible = false;
                this.btnEliminarDeLista.Visible = false;
                this.btnAgregarDeLista.Visible = false;
                this.btnModificarDeLista.Visible = false;
                
            }

        }

        protected void btnAgregarDeLista_Click(object sender, EventArgs e)
        {
            this.Agregando(true, true);
            
            this.txtConcepto.Text = "";
            this.lblConceptoCajaChica.Text = "AGREGAR CONCEPTO DE MOVIMIENTO DE CAJA CHICA";
            this.grdvlista_conceptosCajaChica.Columns[0].Visible = false;
            this.grdvlista_conceptosCajaChica.SelectedIndex = -1;
        }

        protected void btnModificarDeLista_Click(object sender, EventArgs e)
        {
            this.Agregando(true, false);
            this.lblConceptoCajaChica.Text = "MODIFICAR CONCEPTO DE MOVIMIENTO DE CAJA CHICA";
            this.grdvlista_conceptosCajaChica.Columns[0].Visible = false;
        }

        protected void cmdCancelar_Click(object sender, EventArgs e)
        {
            this.Agregando(false, true);
            this.grdvlista_conceptosCajaChica.Columns[0].Visible = true;
        }
        
        protected void Agregando(Boolean activaacgregar, Boolean semuestrbotonaagregar)
        {
            this.panelNuevoConcepto.Visible = activaacgregar;

            this.btnAgregarDeLista.Visible = !activaacgregar;
            this.btnModificarDeLista.Visible = !activaacgregar;
            this.btnEliminarDeLista.Visible = !activaacgregar;
            if (this.grdvlista_conceptosCajaChica.SelectedIndex == -1)
            {
                this.btnEliminarDeLista.Visible = false;
                this.btnModificarDeLista.Visible = false;
            }
            this.btnAceptar.Visible = semuestrbotonaagregar;
            this.btnModificar.Visible = !semuestrbotonaagregar;
            this.grdvlista_conceptosCajaChica.Columns[0].Visible = !activaacgregar;
        }

        protected void btnEliminarDeLista_Click(object sender, EventArgs e)
        {
            string sqlQuery = "Delete from [Conceptosmovimientos] where ConceptomovID=@conceptomovID";
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand commandGaribay = new SqlCommand(sqlQuery, conGaribay);
                
                try
                {
                    commandGaribay.Parameters.Add("@conceptomovID", SqlDbType.Int).Value = (int)this.grdvlista_conceptosCajaChica.SelectedDataKey.Value;
                    conGaribay.Open();
                    int rowsafected = commandGaribay.ExecuteNonQuery();
                    if(rowsafected!=1){
                        throw new Exception(string.Format(myConfig.StrFromMessages("CONCEPTOEXECUTEFAILED"), "ELIMINADO", "ELIMINARON", rowsafected.ToString()));
                    }
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CONCEPTODELETEDEXITO"), this.txtConcepto.Text.ToUpper());
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CONCEPTOSCAJACHICA, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ EL CONCEPTO: " + this.txtConcepto.Text.ToUpper()));
                    this.lblMensajeException.Text = "";
                    this.grdvlista_conceptosCajaChica.SelectedIndex = -1;
                    this.Agregando(false, true);
                    this.imagenmal.Visible = false;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = true;
                    this.txtConcepto.Text = "";
                    
                }
                catch(InvalidOperationException err) 
                {
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CONCEPTODELETEDFAILED"), this.txtConcepto.Text.ToUpper());
                    this.lblMensajeException.Text = err.Message;
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err.Message, this.Request.Url.ToString());
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;
                }
                catch(SqlException err2)
                {
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CONCEPTODELETEDFAILED"), this.txtConcepto.Text.ToUpper());
                    this.lblMensajeException.Text = err2.Message;
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;
                }
                catch (Exception err3)
                {
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CONCEPTODELETEDFAILED"), this.txtConcepto.Text.ToUpper());
                    this.lblMensajeException.Text = err3.Message;
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err3.Message, this.Request.Url.ToString());
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;
                }
                finally{
                conGaribay.Close();
                    }
               
            
         }
            
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string sqlQuery = "Insert Into [Conceptosmovimientos] Values (@concepto)";
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand commandGaribay = new SqlCommand(sqlQuery, conGaribay);
                
                try
                {
                    commandGaribay.Parameters.Add("@concepto", SqlDbType.NVarChar).Value = txtConcepto.Text;
                    conGaribay.Open();
                    
                    int rowsafected = commandGaribay.ExecuteNonQuery();
                    if (rowsafected != 1)
                    {
                        throw new Exception(string.Format(myConfig.StrFromMessages("CONCEPTOEXECUTEFAILED"), "AGREGADO", "AGREGARON", rowsafected.ToString()));
                    }
                    this.Agregando(false, true);
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CONCEPTOADDEDEXITO"), this.txtConcepto.Text.ToUpper());
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CONCEPTOSCAJACHICA, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("SE AGREGÓ EL CONCEPTO: " + this.txtConcepto.Text.ToUpper()));
                    this.lblMensajeException.Text = "";
                    this.txtConcepto.Text = "";
                    this.imagenmal.Visible = false;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = true;
                    
                }
                catch(InvalidOperationException err)
                {
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CONCEPTOADDEDFAILED"), this.txtConcepto.Text.ToUpper());
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), err.Message, this.Request.Url.ToString());
                    this.lblMensajeException.Text = err.Message;
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;
                }
                catch(SqlException err2)
                {
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CONCEPTOADDEDFAILED"), this.txtConcepto.Text.ToUpper());
                    this.lblMensajeException.Text = err2.Message;
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;
                }
                catch (Exception err3)
                {
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CONCEPTOADDEDFAILED"), this.txtConcepto.Text.ToUpper());
                    this.lblMensajeException.Text = err3.Message;
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), err3.Message, this.Request.Url.ToString());
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;
                }
            finally{
                conGaribay.Close();
                }
        }

        protected void grdvlista_conceptosCajaChica_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnEliminarDeLista.Visible = true;
            this.btnModificarDeLista.Visible = true;
            this.txtConcepto.Text = this.grdvlista_conceptosCajaChica.SelectedDataKey["concepto"].ToString();
            this.btnEliminarDeLista.Attributes.Add("onclick", "return confirm('¿Desea eliminar el concepto " + this.grdvlista_conceptosCajaChica.SelectedDataKey["concepto"].ToString() + "?')");
        }
            
        protected void btnModificar_Click(object sender, EventArgs e)
            {
                string sqlQuery = "UPDATE [Conceptosmovimientos] SET concepto =@concepto WHERE conceptomovID=@conceptomovID";
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand commandGaribay = new SqlCommand(sqlQuery, conGaribay);
               
                try
                {
                    commandGaribay.Parameters.Add("@concepto", SqlDbType.NVarChar).Value = this.txtConcepto.Text;
                    commandGaribay.Parameters.Add("@conceptomovID", SqlDbType.Int).Value = this.grdvlista_conceptosCajaChica.SelectedDataKey["conceptomovID"].ToString();
                    conGaribay.Open();
                    int rowsafected = commandGaribay.ExecuteNonQuery();
                    
                    
                    if (rowsafected != 1)
                    {
                        throw new Exception(string.Format(myConfig.StrFromMessages("CONCEPTOEXECUTEFAILED"), "MODIFICADO", "MODIFICARON", rowsafected.ToString()));
                    }
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CONCEPTOMODIFIEDEXITO"), this.txtConcepto.Text.ToUpper());
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CONCEPTOSCAJACHICA, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ EL CONCEPTO: " + this.txtConcepto.Text.ToUpper()));
                    this.lblMensajeException.Text ="";
                    this.Agregando(false, true);
                    this.imagenmal.Visible = false;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = true;
                    
                }
                catch (InvalidOperationException err)
                {
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CONCEPTOMODIFIEDFAILED"), this.txtConcepto.Text.ToUpper());
                    this.lblMensajeException.Text = err.Message;
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err.Message, this.Request.Url.ToString());
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;
                }
                catch (SqlException err1) 
                {
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CONCEPTOMODIFIEDFAILED"), this.txtConcepto.Text.ToUpper());
                    this.lblMensajeException.Text = err1.Message;
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;
                
                }
                catch (Exception err3)
                {
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CONCEPTOMODIFIEDFAILED"), this.txtConcepto.Text.ToUpper());
                    this.lblMensajeException.Text = err3.Message;
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err3.Message, this.Request.Url.ToString());
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;

                }
                finally { 
                conGaribay.Close();
                        }
                }
            }
        }