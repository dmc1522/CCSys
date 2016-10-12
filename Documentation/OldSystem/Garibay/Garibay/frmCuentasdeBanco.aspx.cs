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
    public partial class frmCuentasdeBanco : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Agregando(false, true);
                try
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CUENTASDEBANCOS, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "SE VISITÓ LA PÁGINA DE CUENTAS DE BANCO");
                }
                catch (Exception exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());

                }
                
            }
            this.gridCuentasdeBanco.DataSourceID = "SqlDataSource1";
            
            if (this.panelMensaje.Visible) {
                this.panelMensaje.Visible = false;
            }
            this.compruebasecurityLevel();
         }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.btnAgregardeabajo.Visible = false;
                this.btnAgregardelaLista.Visible = false;
                this.btnModificardeAbajo.Visible = false;
                this.btnModificardelaLista.Visible = false;
                this.btnEliminardelaLista.Visible = false;
                this.panelMensaje.Visible = false;
                this.Panelagregar.Visible = false;
                this.gridCuentasdeBanco.Columns[0].Visible = false;
            }

        }

      
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.Agregando(false, true);
        }

        protected void btnAgregardelaLista_Click(object sender, EventArgs e)
        {
            this.gridCuentasdeBanco.SelectedIndex = -1;
            this.Agregando(true,true);
            limpiarCampos();
            this.lblHeader.Text = "AGREGAR NUEVA CUENTA DE BANCO";

            this.txtCuentaSelectedID.Text = "-1";
            this.cblCuentasPermitidas.DataBind();
        }

        protected void btnModificardelaLista_Click(object sender, EventArgs e)
        {
            this.Agregando(true, false);
            this.lblHeader.Text = "MODIFICAR CUENTA DE BANCO"; 
        }
        
        protected void Agregando(Boolean muestrapanelgregar, Boolean semuestrabotonagregardeabajo)
        {
            this.Panelagregar.Visible = muestrapanelgregar;
            this.btnAgregardelaLista.Visible = !muestrapanelgregar;
            this.btnModificardelaLista.Visible = !muestrapanelgregar;
            this.btnEliminardelaLista.Visible = !muestrapanelgregar;
            if (this.gridCuentasdeBanco.SelectedIndex == -1)
            {
                this.btnModificardelaLista.Visible = false;
                this.btnEliminardelaLista.Visible = false;
            }

            this.btnAgregardeabajo.Visible = semuestrabotonagregardeabajo;
            this.btnModificardeAbajo.Visible = !semuestrabotonagregardeabajo;
            this.gridCuentasdeBanco.Columns[0].Visible = !muestrapanelgregar;
        }

        protected void btnEliminardelaLista_Click(object sender, EventArgs e)
        {
            string sqlQuery = "Delete from [Cuentasdebanco] where cuentaID=@cuentaID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand commandGaribay = new SqlCommand(sqlQuery, conGaribay);

            try
            {
                
                commandGaribay.Parameters.Add("@cuentaID", SqlDbType.Int).Value = int.Parse(this.gridCuentasdeBanco.SelectedDataKey["cuentaID"].ToString());
                conGaribay.Open();
                int rowsafected = commandGaribay.ExecuteNonQuery();
                if (rowsafected != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("CUENTABANCOSEXECUTEFAILED"), "ELIMINADO", "ELIMINARON", rowsafected.ToString()));
                }
                this.panelMensaje.Visible = true;
                this.imagenbien.Visible = true;
                this.imagenmal.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTADEBANCODELETEDEXITO"), this.txtNumerodecuenta.Text.ToUpper());
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CUENTASDEBANCOS, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ LA CUENTA DE BANCO: " + this.txtNumerodecuenta.Text.ToUpper()));
                this.lblMensajeException.Text = "";
                this.gridCuentasdeBanco.SelectedIndex = -1;
                this.Agregando(false, true);
                
            }
            catch (InvalidOperationException err)
            {
                this.panelMensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTADEBANCODELETEDFAILED"), this.txtNumerodecuenta.Text.ToUpper());
                this.lblMensajeException.Text = err.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err.Message, this.Request.Url.ToString());
                this.Agregando(false, true);
            }

            catch (SqlException err1)
            {
                this.panelMensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTADEBANCODELETEDFAILED"), this.txtNumerodecuenta.Text.ToUpper());
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());
                this.lblMensajeException.Text = err1.Message;
                this.Agregando(false, true);
            }
            catch (Exception err2)
            {
                this.panelMensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTADEBANCODELETEDFAILED"), this.txtNumerodecuenta.Text.ToUpper());
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());
                this.lblMensajeException.Text = err2.Message;
                this.Agregando(false, true);
            }
            finally{
            conGaribay.Close();
                this.gridCuentasdeBanco.DataBind();
            }
            }
        

        protected void gridCuentasdeBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnEliminardelaLista.Visible = true;
            this.btnModificardelaLista.Visible = true;
            
            if (this.gridCuentasdeBanco.SelectedDataKey != null)
            {

                
                this.cmbBanco.DataBind();
                
                this.cmbBanco.SelectedIndex = int.Parse(this.gridCuentasdeBanco.SelectedDataKey["bancoID"].ToString())-1;
                this.btnEliminardelaLista.Attributes.Add("onclick", "return confirm('¿Desea eliminar la cuenta "+this.gridCuentasdeBanco.SelectedDataKey["NumeroDeCuenta"].ToString()+"?')");


                this.txtNumerodecuenta.Text = this.gridCuentasdeBanco.SelectedDataKey["NumeroDeCuenta"].ToString();
                this.txtTitular.Text = this.gridCuentasdeBanco.SelectedDataKey["Titular"].ToString();
                this.txtCLABE.Text = this.gridCuentasdeBanco.SelectedDataKey["CLABE"].ToString();
                this.txtInicio.Text = this.gridCuentasdeBanco.SelectedDataKey["numchequeinicio"].ToString();
                this.txtFin.Text = this.gridCuentasdeBanco.SelectedDataKey["numchequefin"].ToString();
                this.drpdlTipodeMoneda.SelectedValue = this.gridCuentasdeBanco.SelectedDataKey["tipomonedaID"].ToString();
                this.txtCuentaSelectedID.Text = this.gridCuentasdeBanco.SelectedDataKey["cuentaID"].ToString();
                this.LoadTraspasosPermitidos();
            }
        }
        internal void SaveTraspasosPermitidos()
        {
            SaveTraspasosPermitidos(int.Parse(this.gridCuentasdeBanco.SelectedDataKey["cuentaID"].ToString()));
        }
        internal void SaveTraspasosPermitidos(int CuentaID)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("delete from TraspasosPermitidos where cuentaID=@cuentaID OR cuentaDestinoID=@cuentaID", conn);
                comm.Parameters.Add("@cuentaID", SqlDbType.Int).Value = CuentaID;
                comm.ExecuteNonQuery();
                comm.CommandText = "insert into TraspasosPermitidos values(@cuentaID, @cuentaDestinoID)";
                foreach (ListItem item in cblCuentasPermitidas.Items)
                {
                    comm.Parameters.Clear();
                    if (item.Selected)
                    {
                        comm.Parameters.Add("@cuentaID", SqlDbType.Int).Value = CuentaID;
                        comm.Parameters.Add("@cuentaDestinoID", SqlDbType.Int).Value = item.Value;
                        comm.ExecuteNonQuery();
                        comm.Parameters.Clear();
                        try
                        {
                            comm.Parameters.Add("@cuentaID", SqlDbType.Int).Value = item.Value;
                            comm.Parameters.Add("@cuentaDestinoID", SqlDbType.Int).Value = CuentaID;
                            comm.ExecuteNonQuery();
                        }
                        catch{}
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "err saving traspasos permitidos", ref ex);
            }
            finally
            {
                conn.Close();
            }
        }
        internal void LoadTraspasosPermitidos()
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                this.txtCuentaSelectedID.Text =  this.gridCuentasdeBanco.SelectedDataKey["cuentaID"].ToString();
                this.cblCuentasPermitidas.DataBind();
                conn.Open();
                SqlCommand comm = new SqlCommand("select cuentaDestinoID from TraspasosPermitidos where cuentaID=@cuentaID", conn);
                comm.Parameters.Add("@cuentaID", SqlDbType.Int).Value = int.Parse(this.gridCuentasdeBanco.SelectedDataKey["cuentaID"].ToString());
                SqlDataReader rd = comm.ExecuteReader();
                while (rd.Read())
                {
                    foreach(ListItem item in cblCuentasPermitidas.Items)
                    {
                        if (item.Value.Equals(rd[0].ToString()))
                        {
                            item.Selected = true;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "err cargando traspasos permitidos", ref ex);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnAgregardeabajo_Click(object sender, EventArgs e)
        {
            string sqlQuery = "Insert Into [Cuentasdebanco](bancoID,NumeroDeCuenta,Titular, CLABE, numchequeinicio, numchequefin, tipomonedaID ) Values (@bancoID,@NumeroDeCuenta,@Titular, @CLABE, @numchequeinicio, @numchequefin, @tipomonedaID); select boletaID = SCOPE_IDENTITY();";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand commandGaribay = new SqlCommand(sqlQuery, conGaribay);

            try
            {
                commandGaribay.Parameters.Add("@bancoID", SqlDbType.Int).Value = this.cmbBanco.SelectedValue;
                commandGaribay.Parameters.Add("@NumeroDeCuenta", SqlDbType.NChar).Value = this.txtNumerodecuenta.Text;
                commandGaribay.Parameters.Add("@Titular", SqlDbType.VarChar).Value = this.txtTitular.Text;
                commandGaribay.Parameters.Add("@CLABE", SqlDbType.VarChar).Value = this.txtCLABE.Text;
                commandGaribay.Parameters.Add("@numchequeinicio", SqlDbType.Int).Value = int.Parse(this.txtInicio.Text);
                commandGaribay.Parameters.Add("@numchequefin", SqlDbType.Int).Value = int.Parse(this.txtFin.Text);
                commandGaribay.Parameters.Add("@tipomonedaID", SqlDbType.Int).Value = int.Parse(this.drpdlTipodeMoneda.SelectedValue);
                
                conGaribay.Open();
                int newcuentaID = int.Parse(commandGaribay.ExecuteScalar().ToString());
                if (newcuentaID <= 0)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("CUENTABANCOSEXECUTEFAILED"), "AGREGADO", "AGREGARON", newcuentaID.ToString()));
                }
                this.SaveTraspasosPermitidos(newcuentaID);
                this.panelMensaje.Visible = true;
                this.imagenbien.Visible = true;
                this.imagenmal.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTADEBANCOADDEDEXITO"), this.txtNumerodecuenta.Text.ToUpper());
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CUENTASDEBANCOS, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("SE INSERTÓ LA CUENTA DE BANCO: " + this.txtNumerodecuenta.Text.ToUpper()));
                this.lblMensajeException.Text = "";
                this.Agregando(false, true);
                
            }
            catch (InvalidOperationException err)
            {
                this.panelMensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTADEBANCOADDEDFAILED"), this.txtNumerodecuenta.Text.ToUpper());
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), err.Message, this.Request.Url.ToString());
                this.lblMensajeException.Text = err.Message;
                this.Agregando(false, true);
            }

            catch (SqlException err1)
            {
                this.panelMensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTADEBANCOADDEDFAILED"), this.txtNumerodecuenta.Text.ToUpper());
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());
                this.lblMensajeException.Text = err1.Message;
                this.Agregando(false, true);
            }
            catch (Exception err2)
            {
                this.panelMensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTADEBANCOADDEDFAILED"), this.txtNumerodecuenta.Text.ToUpper());
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());
                this.lblMensajeException.Text = err2.Message;
                this.Agregando(false, true);
            }
            finally{
            conGaribay.Close();
            }
            }

        protected void limpiarCampos() 
        {
            this.cmbBanco.SelectedIndex = -1;
            this.txtNumerodecuenta.Text = "";
            this.txtTitular.Text = "";
        }

        protected void btnModificardeAbajo_Click(object sender, EventArgs e)
        {
            string sqlQuery = "UPDATE [Cuentasdebanco] SET tipomonedaID = @tipomonedaID, bancoID = @bancoID, NumeroDeCuenta= @NumeroDeCuenta, Titular = @Titular, CLABE = @CLABE, numchequeinicio=@numchequeinicio, numchequefin=@numchequefin WHERE cuentaID = @cuentaID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand commandGaribay = new SqlCommand(sqlQuery, conGaribay);

            try
            {
                commandGaribay.Parameters.Add("@bancoID", SqlDbType.Int).Value = this.cmbBanco.SelectedValue;
                commandGaribay.Parameters.Add("@NumeroDeCuenta", SqlDbType.NChar).Value = this.txtNumerodecuenta.Text;
                commandGaribay.Parameters.Add("@Titular", SqlDbType.VarChar).Value = this.txtTitular.Text;
                commandGaribay.Parameters.Add("@CLABE", SqlDbType.VarChar).Value = this.txtCLABE.Text;
                commandGaribay.Parameters.Add("@numchequeinicio", SqlDbType.Int).Value = int.Parse(this.txtInicio.Text);
                commandGaribay.Parameters.Add("@numchequefin", SqlDbType.Int).Value = int.Parse(this.txtFin.Text);
                commandGaribay.Parameters.Add("@cuentaID", SqlDbType.Int).Value = int.Parse(this.gridCuentasdeBanco.SelectedDataKey["cuentaID"].ToString());
                commandGaribay.Parameters.Add("@tipomonedaID", SqlDbType.Int).Value = int.Parse(this.drpdlTipodeMoneda.SelectedValue);
                conGaribay.Open();
                int rowsafected = commandGaribay.ExecuteNonQuery();


                if (rowsafected != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("CUENTABANCOSEXECUTEFAILED"), "MODIFICADO", "MODIFICARON", rowsafected.ToString()));
                }
                this.SaveTraspasosPermitidos();
                this.panelMensaje.Visible = true;
                this.imagenbien.Visible = true;
                this.imagenmal.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTADEBANCOMODIFIEDEXITO"), this.txtNumerodecuenta.Text.ToUpper());
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CUENTASDEBANCOS, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ LA CUENTA DE BANCO: " + this.txtNumerodecuenta.Text.ToUpper()));
                this.lblMensajeException.Text = "";
                this.Agregando(false, true);
                
            }
            catch (InvalidOperationException err)
            {
                this.panelMensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTADEBANCOMODIFIEDFAILED"), this.txtNumerodecuenta.Text.ToUpper());
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err.Message, this.Request.Url.ToString());
                this.lblMensajeException.Text = err.Message;
                this.Agregando(false, true);
            }

            catch (SqlException err1)
            {
                this.panelMensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTADEBANCOMODIFIEDFAILED"), this.txtNumerodecuenta.Text.ToUpper());
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());
                this.lblMensajeException.Text = err1.Message;
                this.Agregando(false, true);
            }
            catch (Exception err2)
            {
                this.panelMensaje.Visible = true;
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CUENTADEBANCOMODIFIEDFAILED"), this.txtNumerodecuenta.Text.ToUpper());
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());
                this.lblMensajeException.Text = err2.Message;
                this.Agregando(false, true);
            }
            finally
            {
                conGaribay.Close();
            }
        }
        }
    }

