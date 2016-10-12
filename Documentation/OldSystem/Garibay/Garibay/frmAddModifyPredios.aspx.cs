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
using System.Data.Sql;
using System.Data.SqlClient;

namespace Garibay
{
    public partial class frmAddModifyPredios : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.compruebasecurityLevel();
            this.PanelActionResult.Visible = false;
            if (!this.IsPostBack)
            {
                this.divPropietarioPredioID.Attributes.Add("style", this.chkDatosPropietarioID.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                String sOnchange = "ShowHideDivOnChkBox('";
                sOnchange += this.chkDatosPropietarioID.ClientID + "','";
                sOnchange += this.divPropietarioPredioID.ClientID + "')";
                this.chkDatosPropietarioID.Attributes.Add("onclick", sOnchange);
                this.cmbProductor.DataBind();



                string parameter;
                parameter = "javascript:url('";
                parameter += "frmAddQuickProductor.aspx";
                parameter += "', '";
                parameter += "Agregar Productor Rápido";
                parameter += "',10,200,400,500); return false;";
                this.btnAddQuickProductor.Attributes.Add("onClick", parameter);

                this.ddlPropietario.Attributes.Add("onChange", "SelectOnChange('" + this.ddlPropietario.ClientID + "','" + this.cmbProductor.ClientID + "')");
                this.btnModificarDeForm.Visible = false;
                this.PanelActionResult.Visible = false;
                this.PanelAgregar.Visible = true;
                this.cmbCultivo.DataBind();
                this.cmbCultivo.SelectedIndex = 0;
                if (Request.QueryString["data"] != null)
                {
                    if (this.loadqueryStrings(Request.QueryString["data"].ToString()))
                    {
                        if (myQueryStrings["predioID"] != null)
                        {
                            this.txtPredioIDToMod.Text = myQueryStrings["predioID"].ToString();
                            if (this.CargaDatosDePredio())
                            {

                                this.lblPredios.Text = "MODIFICAR PREDIO";
                                this.btnModificarDeForm.Visible = true;
                                this.btnAgregarDeForm.Visible = false;
                            }
                            else
                            {
                                this.txtPredioIDToMod.Text = "-1";
                                this.btnModificarDeForm.Visible = false;
                                this.btnAgregarDeForm.Visible = true;
                            }
                        }
                        if(myQueryStrings["productorID"]!=null && myQueryStrings["solID"]!=null){
                            this.cmbProductor.SelectedValue = myQueryStrings["productorID"].ToString();
                            this.txtSolID.Text= myQueryStrings["solID"].ToString();
                            this.btnRregresaraSol.Visible = true;
                            this.cmbProductor.Enabled = false;
                        }
                    }
                    else
                    {
                        myQueryStrings.Clear();
                        Response.Redirect("~/frmAddModifyPredios.aspx", true);

                    }
                }
                else
                {
                    this.lblPredios.Text = "AGREGAR NUEVO UN PREDIO";
                    this.btnModificarDeForm.Visible = false;
                    this.btnAgregarDeForm.Visible = true;
                }
            }
            
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
            }
        }

        private bool CargaDatosDePredio()
        {
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            bool result = false;
            try
            {
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.CommandText = "SELECT predioID, propietarioID, folioPropietario, productorID, folioProductor, DDR, CADER, Nombre, folioPredio, Ejido, codigoCultivo,";
                sqlComm.CommandText += " CultivoID, Superficie, FolioPROCAMPO, RegistroAlterno, ";
                sqlComm.CommandText += " Norte, Sur, Este, Oeste  FROM Predios where predioID = @predioID;";
                sqlComm.Parameters.Add("@predioID", SqlDbType.Int).Value = this.txtPredioIDToMod.Text;
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                SqlDataReader sqlReader = sqlComm.ExecuteReader();
                if (sqlReader.HasRows && sqlReader.Read())
                {
                    result = true;
                    this.cmbProductor.DataBind();
                    if(sqlReader["propietarioID"].ToString()!="-1")
                    {
                        this.chkDatosPropietarioID.Checked = true;
                        this.ddlPropietario.DataBind();
                        this.ddlPropietario.SelectedValue = sqlReader["propietarioID"].ToString();
                        this.txtFolioPropietario.Text = sqlReader["folioPropietario"].ToString();
                        this.divPropietarioPredioID.Attributes.Add("style", this.chkDatosPropietarioID.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
                    }
                    this.cmbProductor.SelectedValue = sqlReader["productorID"].ToString();
                    this.txtFolioProductor.Text     = sqlReader["folioProductor"].ToString();
                    this.txtDDR.Text                = sqlReader["DDR"].ToString();
                    this.txtCADER.Text              = sqlReader["CADER"].ToString();
                    this.txtNombre.Text             = sqlReader["Nombre"].ToString();
                    this.txtFolioDelPredio.Text     = sqlReader["folioPredio"].ToString();
                    this.txtEjido.Text              = sqlReader["Ejido"].ToString();
                    this.txtCodigoCultivo.Text      = sqlReader["codigoCultivo"].ToString();

                    
                   
                    this.cmbCultivo.DataBind();
                    this.cmbCultivo.SelectedValue   = sqlReader["CultivoID"].ToString();
                    this.txtSuperficie.Text         = sqlReader["Superficie"].ToString();
                    this.txtFolioPROCAMPO.Text      = sqlReader["FolioPROCAMPO"].ToString();
                    //this.txtProductorPROCAMPO.Text  = sqlReader["ProductorPROCAMPO"].ToString();
                    this.txtRegistroAlterno.Text    = sqlReader["RegistroAlterno"].ToString();
                    this.txtNorte.Text              = sqlReader["Norte"].ToString();
                    this.txtSur.Text                = sqlReader["Sur"].ToString();
                    this.txtEste.Text               = sqlReader["Este"].ToString();
                    this.txtOeste.Text              = sqlReader["Oeste"].ToString();
                }
            }
            catch (SqlException ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(Session["USERID"].ToString()), "NO SE PUDÓ OBTENER LOS DATOS DEL PREDIO A MODIFICAR. SQL ERROR. EX:" + ex.Message, this.Request.Url.ToString());
            }
            catch (System.Exception ex)
            {
            	Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(Session["USERID"].ToString()),"NO SE PUDÓ OBTENER LOS DATOS DEL PREDIO A MODIFICAR. EX:" + ex.Message,this.Request.Url.ToString());
            }
            finally
            {
                sqlConn.Close();
            }
            return result;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Server.Transfer("~/frmListDeletePredios.aspx");
        }

        protected void btnAgregarDeForm_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand sqlComm = new SqlCommand();
            String sQuery = "INSERT INTO Predios " +
                " (propietarioID, folioPropietario, productorID, folioProductor, DDR, CADER, Nombre, folioPredio, Ejido, codigoCultivo, CultivoID, Superficie, FolioPROCAMPO,  " +
                " RegistroAlterno, Norte, Sur, Este, Oeste, storeTS, updateTS, userID) " +
                " VALUES        (@propietarioID,@folioPropietario,@productorID,@folioProductor,@DDR,@CADER,@Nombre,@folioPredio,@Ejido,@codigoCultivo,@CultivoID,@Superficie,@FolioPROCAMPO,@RegistroAlterno,@Norte,@Sur,@Este,@Oeste, @storeTS, @updateTS, @userID)";
            sQuery += "SELECT NewID = SCOPE_IDENTITY();";
            try
            {
                sqlComm.Connection = sqlConn;
                sqlComm.CommandText = sQuery;
                sqlConn.Open();
                //@Norte,@Sur,@Este,@Oeste
                sqlComm.Parameters.Add("@propietarioID", SqlDbType.Int).Value = this.chkDatosPropietarioID.Checked? int.Parse(this.ddlPropietario.SelectedValue):-1;
                sqlComm.Parameters.Add("@folioPropietario", SqlDbType.VarChar).Value = this.txtFolioPropietario.Text;
                sqlComm.Parameters.Add("@productorID",SqlDbType.Int).Value = int.Parse(this.cmbProductor.SelectedValue);
                sqlComm.Parameters.Add("@folioProductor", SqlDbType.VarChar).Value = this.txtFolioProductor.Text;
                sqlComm.Parameters.Add("@DDR", SqlDbType.VarChar).Value = this.txtDDR.Text;
                sqlComm.Parameters.Add("@CADER", SqlDbType.VarChar).Value = this.txtCADER.Text;
                sqlComm.Parameters.Add("@Nombre",SqlDbType.NVarChar).Value = this.txtNombre.Text;
                sqlComm.Parameters.Add("@folioPredio", SqlDbType.VarChar).Value = this.txtFolioDelPredio.Text;
                sqlComm.Parameters.Add("@Ejido", SqlDbType.NVarChar).Value = this.txtEjido.Text;
                sqlComm.Parameters.Add("@codigoCultivo", SqlDbType.VarChar).Value = this.txtCodigoCultivo.Text;
                sqlComm.Parameters.Add("@CultivoID", SqlDbType.Int).Value = int.Parse(this.cmbCultivo.SelectedValue);
                sqlComm.Parameters.Add("@Superficie", SqlDbType.Float).Value = Utils.GetSafeFloat(this.txtSuperficie.Text);
                sqlComm.Parameters.Add("@FolioPROCAMPO", SqlDbType.NVarChar).Value = this.txtFolioPROCAMPO.Text;
                sqlComm.Parameters.Add("@RegistroAlterno", SqlDbType.NVarChar).Value = this.txtRegistroAlterno.Text;
                sqlComm.Parameters.Add("@Norte", SqlDbType.NVarChar).Value = this.txtNorte.Text;
                sqlComm.Parameters.Add("@Sur", SqlDbType.NVarChar).Value = this.txtSur.Text;
                sqlComm.Parameters.Add("@Este", SqlDbType.NVarChar).Value = this.txtEste.Text;
                sqlComm.Parameters.Add("@Oeste", SqlDbType.NVarChar).Value = this.txtOeste.Text;
                sqlComm.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.Now;
                sqlComm.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.Now;
                sqlComm.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                                      
                String sPredioID;
                sPredioID = sqlComm.ExecuteScalar().ToString();
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PREDIOS, Logger.typeUserActions.INSERT, int.Parse(Session["USERID"].ToString()),"AGREGÓ EL PREDIO CON NOMBRE:" + this.txtNombre.Text + ", FOLIO: " + this.txtFolioDelPredio.Text + " Y PRODUCTOR: " + this.cmbProductor.SelectedItem.Text);
                this.txtPredioID.Text = sPredioID;
                this.PanelActionResult.Visible = true;
                this.PanelAgregar.Visible = false;
                this.lblMensajetitle.Text = "PREDIO AGREGADO EXITOSAMENTE";
                this.lblMensajeOperationresult.Visible = false;
                this.lblMensajeException.Visible = false;
                this.imagenmal.Visible = !(this.imagenbien.Visible = true);
            }
            catch(SqlException  ex)
            {
                this.PanelActionResult.Visible = true;
                Exception x = (Exception)ex;
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "err insert predio", ref x);
                String sTemp = "Add Predio SQL Error: " + ex.Message + " DATA: " + ex.Data.ToString();
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(Session["USERID"].ToString()),sTemp, this.Request.Url.ToString());
                this.lblMensajetitle.Text = "AGREGAR PREDIO";
                this.lblMensajeOperationresult.Text = "NO SE PUDO AGREGAR EL PREDIO, ERROR SQL";
                this.lblMensajeException.Text = ex.Message;
            }
            catch (System.Exception ex)
            {
                this.PanelActionResult.Visible = true;
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "err insert predio", ref ex);
                String sTemp = "Add Predio Ex:" + ex.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(Session["USERID"].ToString()), sTemp, this.Request.Url.ToString());
                this.lblMensajetitle.Text = "AGREGAR PREDIO";
                this.lblMensajeOperationresult.Text = "NO SE PUDO AGREGAR EL PREDIO";
                this.lblMensajeException.Text = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }
        }

        protected void btnAceptardtlst_Click(object sender, EventArgs e)
        {           
            this.Response.Redirect("frmAddModifyPredios.aspx");
        }

        protected void btnIrALista_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/frmListDeletePredios.aspx");
        }

        protected void btnModificarDeForm_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.CommandText = "UPDATE Predios SET propietarioID=@propietarioID, folioPropietario=@folioPropietario, productorID = @productorID,";
                sqlComm.CommandText += " folioProductor=@folioProductor, DDR=@DDR, CADER=@CADER, Nombre = @Nombre, folioPredio=@folioPredio, Ejido = @Ejido, ";
                sqlComm.CommandText += " codigoCultivo=@codigoCultivo, CultivoID = @CultivoID, Superficie = @Superficie, FolioPROCAMPO = @FolioPROCAMPO, ";
                sqlComm.CommandText += " RegistroAlterno = @RegistroAlterno, Norte = @Norte, Sur = @Sur, Este = @Este, Oeste = @Oeste, updateTS=@updateTS, userID=@userID where predioID = @predioID";

                sqlComm.Parameters.Add("@propietarioID", SqlDbType.Int).Value = this.chkDatosPropietarioID.Checked ? int.Parse(this.ddlPropietario.SelectedValue) : -1;
                sqlComm.Parameters.Add("@folioPropietario", SqlDbType.VarChar).Value = this.txtFolioPropietario.Text;
                sqlComm.Parameters.Add("@productorID", SqlDbType.Int).Value = int.Parse(this.cmbProductor.SelectedValue);
                sqlComm.Parameters.Add("@folioProductor", SqlDbType.VarChar).Value = this.txtFolioProductor.Text;
                sqlComm.Parameters.Add("@DDR", SqlDbType.VarChar).Value = this.txtDDR.Text;
                sqlComm.Parameters.Add("@CADER", SqlDbType.VarChar).Value = this.txtCADER.Text;
                sqlComm.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = this.txtNombre.Text;
                sqlComm.Parameters.Add("@folioPredio", SqlDbType.VarChar).Value = this.txtFolioDelPredio.Text;
                sqlComm.Parameters.Add("@Ejido", SqlDbType.NVarChar).Value = this.txtEjido.Text;
                sqlComm.Parameters.Add("@codigoCultivo", SqlDbType.VarChar).Value = this.txtCodigoCultivo.Text;
                sqlComm.Parameters.Add("@CultivoID", SqlDbType.Int).Value = int.Parse(this.cmbCultivo.SelectedValue);
                sqlComm.Parameters.Add("@Superficie", SqlDbType.Float).Value = Utils.GetSafeFloat(this.txtSuperficie.Text);
                sqlComm.Parameters.Add("@FolioPROCAMPO", SqlDbType.NVarChar).Value = this.txtFolioPROCAMPO.Text;
                sqlComm.Parameters.Add("@RegistroAlterno", SqlDbType.NVarChar).Value = this.txtRegistroAlterno.Text;
                sqlComm.Parameters.Add("@Norte", SqlDbType.NVarChar).Value = this.txtNorte.Text;
                sqlComm.Parameters.Add("@Sur", SqlDbType.NVarChar).Value = this.txtSur.Text;
                sqlComm.Parameters.Add("@Este", SqlDbType.NVarChar).Value = this.txtEste.Text;
                sqlComm.Parameters.Add("@Oeste", SqlDbType.NVarChar).Value = this.txtOeste.Text;
                
                sqlComm.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.Now;
                sqlComm.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                sqlComm.Parameters.Add("@predioID", SqlDbType.Int).Value = this.txtPredioIDToMod.Text;

                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                if (sqlComm.ExecuteNonQuery() == 1)
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PREDIOS, Logger.typeUserActions.UPDATE, int.Parse(Session["USERID"].ToString()), "MODIFICÓ EL PREDIO: " + this.txtPredioIDToMod.Text);
                    this.txtPredioID.Text = this.txtPredioIDToMod.Text;
                    this.PanelActionResult.Visible = true;
                    this.PanelAgregar.Visible = false;
                    this.lblMensajetitle.Text = "PREDIO MODIFICADO EXITOSAMENTE";
                    this.lblMensajeOperationresult.Visible = false;
                    this.lblMensajeException.Visible = false;
                    this.imagenmal.Visible = !(this.imagenbien.Visible = true);
                }
            }
            catch (SqlException ex)
            {
                this.PanelActionResult.Visible = true;
                String sTemp = "update Predio SQL Error: " + ex.Message + " DATA: " + ex.Data.ToString();
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(Session["USERID"].ToString()), sTemp, this.Request.Url.ToString());
                this.lblMensajetitle.Text = "MODIFICAR PREDIO";
                this.lblMensajeOperationresult.Text = "NO SE PUDO MODIFICAR EL PREDIO, ERROR SQL";
                this.lblMensajeException.Text = ex.Message;
            }
            catch (System.Exception ex)
            {
                this.PanelActionResult.Visible = true;
                String sTemp = "Update Predio Ex:" + ex.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(Session["USERID"].ToString()), sTemp, this.Request.Url.ToString());
                this.lblMensajetitle.Text = "AGREGAR PREDIO";
                this.lblMensajeOperationresult.Text = "NO SE PUDO AGREGAR EL PREDIO";
                this.lblMensajeException.Text = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }
        }

        protected void btnActualizarListas_Click(object sender, EventArgs e)
        {
            this.ddlPropietario.DataBind();
            this.cmbProductor.DataBind();
        }
        private void limpiacampos()
        {
            this.chkDatosPropietarioID.Checked = false;
            this.ddlPropietario.SelectedIndex =0;
            this.txtFolioPropietario.Text ="";
            this.divPropietarioPredioID.Attributes.Add("style", this.chkDatosPropietarioID.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            this.cmbProductor.SelectedIndex = 0;
            this.txtFolioProductor.Text = "";
            this.txtDDR.Text = "";
            this.txtCADER.Text = "";
            this.txtNombre.Text = "";
            this.txtFolioDelPredio.Text = "";
            this.txtEjido.Text = "";
            this.txtCodigoCultivo.Text = "";           
            this.cmbCultivo.SelectedIndex =0 ;
            this.txtSuperficie.Text = "";
            this.txtFolioPROCAMPO.Text = "";
            this.txtRegistroAlterno.Text ="";
            this.txtNorte.Text = "";
            this.txtSur.Text = "";
            this.txtEste.Text = "";
            this.txtOeste.Text = "";
        }

        protected void btnRregresaraSol_Click(object sender, EventArgs e)
        {
            String sNewUrl = "~/frmAddSolicitud.aspx";
            sNewUrl += Utils.GetEncriptedQueryString("idtomodify=" + this.txtSolID.Text);
            Response.Redirect(sNewUrl);
        }
    }
}
