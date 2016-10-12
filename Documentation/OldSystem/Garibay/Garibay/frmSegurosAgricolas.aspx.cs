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
    public partial class Formulario_web1 : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.compruebasecurityLevel();
            if (this.panelmensaje.Visible)
            {
                this.panelmensaje.Visible = false;
            }
            if(!IsPostBack){
                ///
                if (Request.QueryString["data"] != null)
                {
                    if (this.loadqueryStrings(Request.QueryString["data"].ToString()))
                    {
                        //this.lblProductoresParecidos.Visible = this.GridView1.Visible = false;
                        //this.lblHeader.Text = "MODIFICANDO AL PRODUCTOR";
                        if (myQueryStrings["opID"]!=null)
                        {
                            this.txtidModDel.Text = myQueryStrings["opID"].ToString();
                        if (this.cargadatosmodify())
                        {
                            
                            this.btnModificarAbajo.Visible = true;
                            this.btnAddAbajo.Visible = false;
                            this.actualizaBotones(true, false);  
                        }
                        else
                        {
                            this.txtidModDel.Text = "-1";

                        }
                        }
                        else if (myQueryStrings["delID"]!=null)
                        {
                            
                                this.txtidModDel.Text = myQueryStrings["delID"].ToString();
                                this.eliminarFunction();
                                this.pnlAdd.Visible = false;
                            
                                this.txtidModDel.Text = "-1";

                         }
                        }
                        
                    }
                    else
                    {
                        myQueryStrings.Clear();
                        this.pnlAdd.Visible = false;
                        //this.btnModificar.Visible = false;
                        //this.btnEliminar.Visible = false;
                        this.btnModificarAbajo.Visible = false;
                        this.panelmensaje.Visible = false;
                        //Response.Redirect("~/frmSegurosAgricolas.aspx", true);

                    }
                }
                
                /////
                


            
    
           
          //  if(this.grdvSeguros.SelectedIndex>-1){
            //    this.btnEliminar.Visible = true;
              //  this.btnModificar.Visible = true;

            //}else{
              //  this.btnEliminar.Visible = false;
                //this.btnModificar.Visible = false;

            //}
            

        }
        
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");

            }
        }


        protected void btnAddAbajo_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand sqlComm = new SqlCommand();
            String sQuery = "INSERT INTO SegurosAgricolas (Nombre, CostoPorHectarea, Descripcion, storeTS, updateTS) VALUES (@Nombre, @CostoPorHectarea, @Descripcion, @storeTS, @updateTS)";
            sQuery += "SELECT NewID = SCOPE_IDENTITY();";
            try
            {
                sqlComm.Connection = sqlConn;
                sqlComm.CommandText = sQuery;
                sqlConn.Open();
                //@Norte,@Sur,@Este,@Oeste
                sqlComm.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = this.txtNombre.Text;
                
                sqlComm.Parameters.Add("@CostoPorHectarea", SqlDbType.Float).Value =Utils.GetSafeFloat(this.txtMontoXhectarea.Text);
                sqlComm.Parameters.Add("@Descripcion", SqlDbType.Text).Value = this.txtDescrip.Text;
                sqlComm.Parameters.Add("@storeTS", SqlDbType.DateTime).Value =  Utils.Now;
                sqlComm.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.Now;



                int idinserted = int.Parse(sqlComm.ExecuteScalar().ToString());
                 this.actualizaBotones(false, true);

                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SEGUROS, Logger.typeUserActions.INSERT, int.Parse(Session["USERID"].ToString()), "AGREGÓ EL SEGURO CON NOMBRE:" + this.txtNombre.Text + ", ID : " + idinserted );
                //this.txtPredioID.Text = sPredioID;
                //this.PanelActionResult.Visible = true;
                this.panelmensaje.Visible = true;
                this.pnlAdd.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Visible = true;

                this.lblMensajeOperationresult.Text = "SEGURO AGRÍCOLA AGREGADO EXITOSAMENTE"; 
                this.lblMensajeException.Visible = false;
                this.imagenmal.Visible = !(this.imagenbien.Visible = true);
                this.pnlAdd.Visible = false;
                this.btnAgregar.Visible = true;
                this.grdvSeguros.Columns[0].Visible = true;
                //this.btnEliminar.Visible = false;
                //this.btnModificar.Visible = false;
                this.grdvSeguros.DataBind();
            }
            catch (SqlException ex)
            {
                //this.PanelActionResult.Visible = true;
                Exception x = (Exception)ex;
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "err insert Seguro", ref x);
                String sTemp = "Add Seguro SQL Error: " + ex.Message + " DATA: " + ex.Data.ToString();
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(Session["USERID"].ToString()), sTemp, this.Request.Url.ToString());
                this.lblMensajetitle.Text = "AGREGAR SEGURO";
                this.lblMensajeOperationresult.Text = "NO SE PUDO AGREGAR EL SEGURO, ERROR SQL";
                this.lblMensajeException.Text = ex.Message;
                this.pnlAdd.Visible = false;
            }
            catch (System.Exception ex)
            {
                //this.PanelActionResult.Visible = true;
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "err insert Seguro", ref ex);
                String sTemp = "Add Seguro Ex:" + ex.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(Session["USERID"].ToString()), sTemp, this.Request.Url.ToString());
                this.lblMensajetitle.Text = "AGREGAR Seguro";
                this.lblMensajeOperationresult.Text = "NO SE PUDO AGREGAR EL SEGURO";
                this.lblMensajeException.Text = ex.Message;
                this.pnlAdd.Visible = false;
            }
            finally
            {
                sqlConn.Close();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            this.pnlAdd.Visible = true;
            this.limpiarCampos();
            //this.grdvSeguros.Columns[0].Visible = false;
            this.grdvSeguros.SelectedIndex = -1;
            this.actualizaBotones(true, true);
        }

        //protected void btnModificar_Click(object sender, EventArgs e)
        //{


        //    if (this.grdvSeguros.SelectedIndex > -1)
        //    {
        //        this.txtNombre.Text = this.grdvSeguros.SelectedRow.Cells[2].Text;
        //        this.txtMontoXhectarea.Text = this.grdvSeguros.SelectedRow.Cells[3].Text.Replace("$","");
        //        this.txtDescrip.Text = this.grdvSeguros.SelectedRow.Cells[4].Text;

        //        this.pnlAdd.Visible = true;
        //        this.actualizaBotones(true, false);  
              
                
        //    }


        //}

        protected void btnModificarAbajo_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand sqlComm = new SqlCommand();
            String sQuery = "UPDATE  SegurosAgricolas SET Nombre=@Nombre, CostoPorHectarea=@CostoPorHectarea, Descripcion=@Descripcion, updateTS=@updateTS WHERE seguroID=@seguroID";
            //sQuery += "SELECT NewID = SCOPE_IDENTITY();";
            try
            {
                sqlComm.Connection = sqlConn;
                sqlComm.CommandText = sQuery;
                sqlConn.Open();
                //@Norte,@Sur,@Este,@Oeste
                sqlComm.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = this.txtNombre.Text;

                sqlComm.Parameters.Add("@CostoPorHectarea", SqlDbType.Float).Value = Utils.GetSafeFloat(this.txtMontoXhectarea.Text);
                sqlComm.Parameters.Add("@Descripcion", SqlDbType.Text).Value = this.txtDescrip.Text;
                sqlComm.Parameters.Add("@seguroID", SqlDbType.Int).Value = int.Parse(this.txtidModDel.Text);
                sqlComm.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.Now;



                sqlComm.ExecuteNonQuery();
                this.actualizaBotones(false, true);
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SEGUROS, Logger.typeUserActions.UPDATE, int.Parse(Session["USERID"].ToString()), "MODIFICÓ EL SEGURO CON EL ID : " + this.txtidModDel.Text);
                //this.txtPredioID.Text = sPredioID;
                //this.PanelActionResult.Visible = true;
                this.panelmensaje.Visible = true;
                this.pnlAdd.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Visible = true;
                this.lblMensajeOperationresult.Text = "SEGURO AGRÍCOLA " + this.txtidModDel.Text + " MODIFICADÓ EXITOSAMENTE ";
                this.lblMensajeException.Visible = false;
                this.imagenmal.Visible = !(this.imagenbien.Visible = true);
                this.pnlAdd.Visible = false;

               

                this.grdvSeguros.DataBind();
             //   this.grdvSeguros.Columns[0].Visible = true;
            }
            catch (SqlException ex)
            {
                //this.PanelActionResult.Visible = true;
                Exception x = (Exception)ex;
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "err modify Seguro", ref x);
                String sTemp = "MOD Seguro SQL Error: " + ex.Message + " DATA: " + ex.Data.ToString();
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(Session["USERID"].ToString()), sTemp, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Visible = true;
                this.lblMensajeOperationresult.Text = "NO SE PUDO MODIFICAR EL SEGURO" + this.txtidModDel.Text + " , ERROR SQL";
                this.lblMensajeException.Text = ex.Message;
                this.pnlAdd.Visible = false;
            }
            catch (System.Exception ex)
            {
                //this.PanelActionResult.Visible = true;
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "err modified Seguro", ref ex);
                String sTemp = "MOD Seguro Ex:" + ex.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(Session["USERID"].ToString()), sTemp, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Visible = true;
                this.lblMensajeOperationresult.Text = "NO SE PUDO MODIFICAR EL SEGURO" + this.txtidModDel.Text;
                this.lblMensajeException.Text = ex.Message;
                this.pnlAdd.Visible = false;
            }
            finally
            {
                sqlConn.Close();
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.pnlAdd.Visible = false;
           // this.grdvSeguros.Columns[0].Visible = true;
            this.limpiarCampos();
        }
        protected void limpiarCampos(){
            this.txtNombre.Text = "";
            this.txtMontoXhectarea.Text = "";
            this.txtDescrip.Text = "";

        }

        protected void grdvSeguros_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(this.grdvSeguros.SelectedIndex!=-1){
                
            //    this.btnEliminar.Visible = true;
            //    this.btnModificar.Visible = true;
            //    this.btnAgregar.Visible = true;
            //    string msgDel = "return confirm('¿Realmente desea eliminar el ciclo: ";
            //    msgDel += this.grdvSeguros.SelectedDataKey[1].ToString();
            //    msgDel += "?')";
            //    this.btnEliminar.Attributes.Add("onclick", msgDel);

            //}

        }
        protected void actualizaBotones(Boolean activaacgregar, Boolean semuestrbotonaagregar)
        {
            this.pnlAdd.Visible = activaacgregar;
            this.btnAgregar.Visible = !activaacgregar;
            //this.btnModificar.Visible = !activaacgregar;
            //this.btnEliminar.Visible = !activaacgregar;
            this.btnAddAbajo.Visible = semuestrbotonaagregar;
            this.btnModificarAbajo.Visible = !semuestrbotonaagregar;
            this.grdvSeguros.Columns[0].Visible = !activaacgregar;
            //if (this.grdvSeguros.SelectedIndex == -1)
           // {
             //   this.btnModificar.Visible = false;
               // this.btnEliminar.Visible = false;
            //}

        }

        //protected void btnEliminar_Click(object sender, EventArgs e)
        //{
        //    string qryDel = "DELETE FROM SegurosAgricolas WHERE seguroID=@seguroID";
        //    SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
        //    SqlCommand cmdDel = new SqlCommand(qryDel, conGaribay);

        //    try
        //    {
        //        cmdDel.Parameters.Add("@seguroID", SqlDbType.Int).Value = (int)this.grdvSeguros.SelectedDataKey[0];

        //        conGaribay.Open();
        //        this.panelmensaje.Visible = true;
        //        this.imagenmal.Visible = false;
        //        this.imagenbien.Visible = true;
        //        this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
        //        this.lblMensajeOperationresult.Text = "EL SEGURO " + this.grdvSeguros.SelectedDataKey[1].ToString().ToUpper() + " SE ELIMINÓ SATISFACTORIAMENTE "; 
        //        //string.Format(myConfig.StrFromMessages("CICLODELETEDEXITO"), this.gridCiclos.SelectedDataKey["CicloName"].ToString().ToUpper());
        //        this.lblMensajeException.Text = "";//NO HAY EXCEPTION
        //        int numregistros = cmdDel.ExecuteNonQuery();
        //        if (numregistros != 1)
        //        {
        //            throw new Exception(string.Format(myConfig.StrFromMessages("SEGUROEXECUTEFAILED"), "ELIMINADO", "ELIMINARON", numregistros.ToString()));
        //        }
        //        Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SEGUROS, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ EL SEGURO: " + this.grdvSeguros.SelectedDataKey[1].ToString().ToUpper()));
        //    }
        //    catch (InvalidOperationException err1)
        //    {
        //        this.panelmensaje.Visible = true;
        //        this.imagenmal.Visible = true;
        //        this.imagenbien.Visible = false;
        //        this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
        //        this.lblMensajeOperationresult.Text = "NO SE PUDO ELMINAR EL SEGURO " + this.grdvSeguros.SelectedDataKey[1].ToString().ToUpper();
        //        this.lblMensajeException.Text = err1.Message;
        //        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());

        //    }
        //    catch (SqlException err2)
        //    {
        //        this.panelmensaje.Visible = true;
        //        this.imagenmal.Visible = true;
        //        this.imagenbien.Visible = false;
        //        this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
        //        this.lblMensajeOperationresult.Text = "NO SE PUDO ELMINAR EL SEGURO " + this.grdvSeguros.SelectedDataKey[1].ToString().ToUpper();
        //        this.lblMensajeException.Text = err2.Message;
        //        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());

        //    }
        //    catch (Exception err3)
        //    {
        //        this.panelmensaje.Visible = true;
        //        this.imagenmal.Visible = true;
        //        this.imagenbien.Visible = false;
        //        this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
        //        this.lblMensajeOperationresult.Text = "NO SE PUDO ELMINAR EL SEGURO " + this.grdvSeguros.SelectedDataKey[1].ToString().ToUpper();
        //        this.lblMensajeException.Text = err3.Message;
        //        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err3.Message, this.Request.Url.ToString());
        //    }
        //    finally
        //    {
        //        conGaribay.Close();
        //        this.grdvSeguros.SelectedIndex = -1;
        //        this.grdvSeguros.DataBind();
        //        if (this.grdvSeguros.Rows.Count < 1)
        //        {
        //            this.btnEliminar.Visible = false;
        //            this.btnModificar.Visible = false;
        //        }
        //    }
        //}

        protected void grdvSeguros_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }
           // LinkButton link = (LinkButton)e.Row.Cells[7].FindControl("LinkButton1");
            //if (link != null)
            //{
              //  string parameter, ventanatitle = "ABRIR ";
                // String pathArchivotemp = PdfCreator.printLiquidacion(0, PdfCreator.tamañoPapel.CARTA, PdfCreator.orientacionPapel.VERTICAL, ref this.gvBoletas, ref gvAnticipos, ref gvPagosLiquidacion);
                
            //}
            HyperLink link = (HyperLink)e.Row.Cells[6].FindControl("LinkButton1");
            if (link != null)
            {
                String sQuery = "opID=" + e.Row.Cells[0].Text;
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = link.NavigateUrl;
                strRedirect += sQuery;
                link.NavigateUrl = strRedirect;
            }
            link = (HyperLink)e.Row.Cells[7].FindControl("LinkButton2");
            if (link != null)
            {
                String sQuery = "delID=" + e.Row.Cells[0].Text;
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = link.NavigateUrl;
                strRedirect += sQuery;
                link.NavigateUrl = strRedirect;
            }


        }
        protected bool cargadatosmodify(){
            string sQuery;
            Boolean result;
            SqlConnection con = new SqlConnection(myConfig.ConnectionInfo);
            try{
            sQuery = "SELECT * FROM SegurosAgricolas where seguroID=@seguroID";
            
            SqlCommand sqlcomm = new SqlCommand(sQuery, con);
            sqlcomm.Parameters.Add("@seguroID", SqlDbType.Int).Value = int.Parse(this.txtidModDel.Text);
            SqlDataReader rd;
                con.Open();
                

            rd = sqlcomm.ExecuteReader();
                rd.Read();
                if(!rd.HasRows){
                    this.lblMensajeOperationresult.Text = myConfig.StrFromMessages("FALLOCARGARMODIFICAR");
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXCEPTION
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;
                    //this.txtIDdetails.Text = "-1";
                    this.pnlAdd.Visible = false;

                    result= false;
                }else{

                    this.txtNombre.Text = rd["Nombre"].ToString().ToUpper();
                    this.txtMontoXhectarea.Text =  rd["CostoPorHectarea"].ToString().Replace("$", "");
                    this.txtDescrip.Text = rd["Descripcion"].ToString();

                    this.pnlAdd.Visible = true;
                    this.actualizaBotones(true, false);
                    result = true;
                }

                
            }catch(Exception ex){

                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "error parseando datos a modificar seguro", ref ex);

                result = false;
            }finally{
                con.Close();
            }

            return result;

        }
        protected void eliminarFunction(){
            string qryDel = "DELETE FROM SegurosAgricolas WHERE seguroID=@seguroID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdDel = new SqlCommand(qryDel, conGaribay);

            try
            {
                cmdDel.Parameters.Add("@seguroID", SqlDbType.Int).Value = int.Parse(this.txtidModDel.Text);

                conGaribay.Open();
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = false;
                this.imagenbien.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = "EL SEGURO NÚMERO " + this.txtidModDel.Text + " SE ELIMINÓ SATISFACTORIAMENTE ";
                //string.Format(myConfig.StrFromMessages("CICLODELETEDEXITO"), this.gridCiclos.SelectedDataKey["CicloName"].ToString().ToUpper());
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                int numregistros = cmdDel.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("SEGUROEXECUTEFAILED"), "ELIMINADO", "ELIMINARON", numregistros.ToString()));
                }
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SEGUROS, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ EL SEGURO: " + this.txtidModDel.Text ));
            }
            catch (InvalidOperationException err1)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "NO SE PUDO ELMINAR EL SEGURO " + this.txtidModDel.Text;
                this.lblMensajeException.Text = err1.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());

            }
            catch (SqlException err2)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "NO SE PUDO ELMINAR EL SEGURO " + this.txtidModDel.Text;
                this.lblMensajeException.Text = err2.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());

            }
            catch (Exception err3)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "NO SE PUDO ELMINAR EL SEGURO " + this.txtidModDel.Text;
                this.lblMensajeException.Text = err3.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err3.Message, this.Request.Url.ToString());
            }
            finally
            {
                conGaribay.Close();
                this.grdvSeguros.SelectedIndex = -1;
                this.grdvSeguros.DataBind();
                //if (this.grdvSeguros.Rows.Count < 1)
                //{
                //    this.btnEliminar.Visible = false;
                //    this.btnModificar.Visible = false;
                //}
            }

        }
        
    }
}
