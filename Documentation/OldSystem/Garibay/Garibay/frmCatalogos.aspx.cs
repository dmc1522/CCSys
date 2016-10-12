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
using System.Text;


namespace Garibay
{
    public partial class WebForm14 : Garibay.BasePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.ActualizaPagina(false, true);
                this.drpdlGrupoAddSubcatalogo.DataBind();
                btnModificarListaCatalogo.Visible = false;
                btnEliminarCatalogo.Visible = false;
                btnModificarListaSubCatalogo.Visible = false;
                btnEliminarSubCatalogo.Visible = false;
                this.GrdvCatalogos.PageSize = int.Parse(this.ddlElemXPage.SelectedValue);
                this.grdvSubCatalogos.PageSize = int.Parse(this.ddlElemXpageSub.SelectedValue);
                this.pnladdSubCatalogo.Visible = false;
                this.drpdlCatalogoforSub.DataBind();
                this.drpDlGrupo.DataBind();
                this.pnladdSubCatalogo.Visible = false;
                this.pnlSubCatalogo.Visible = false;
                try
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CATALOGO, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "SE VISITÓ LA PÁGINA DE CATÁLOGOS");
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

            this.GrdvCatalogos.DataSourceID = "SqlCatalogos";
            if(this.GrdvCatalogos.SelectedIndex==-1){
                this.btnEliminarCatalogo.Visible = false;
                this.btnModificarListaCatalogo.Visible = false;
//                 this.btnVerSubCatalogo.Visible=false;

            }
            this.lblAddResult.Visible = false;
            
            this.grdvSubCatalogos.DataSourceID = "SqlSubCatalogos";
            if (this.grdvSubCatalogos.SelectedIndex == -1)
            {
                this.btnEliminarSubCatalogo.Visible = false;
                this.btnModificarListaSubCatalogo.Visible = false;

            }
            this.compruebasecurityLevel();

            
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel != 1 )
            {
                this.btnAgregarCatalogo.Visible = false;
                this.btnAgregarListaCatalogo.Visible = false;
                this.btnModificarCatalogo.Visible = false;
                this.btnModificarListaCatalogo.Visible = false;
                this.btnEliminarCatalogo.Visible=false;
                this.GrdvCatalogos.Columns[0].Visible = false;
                this.grdvSubCatalogos.Columns[0].Visible = false;
                this.btnAgregarSubCatalogo.Visible = false;
                this.btnAgregarListaSubCatalogo.Visible = false;
                this.btnModificarSubCatalogo.Visible = false;
                this.btnModificarListaSubCatalogo.Visible = false;
                this.btnEliminarSubCatalogo.Visible = false;
                this.panelUnauthorized.Visible = true;
                this.checkBoxShowFormSendAuth.Visible = true;
                
            }
            else
            {
                this.checkBoxShowFormSendAuth.Visible = false;
                this.panelUnauthorized.Visible = false;
            }
        }


        protected void ActualizaPagina(Boolean activaacgregar, Boolean semuestrbotonaagregar)
        {


            this.Pnladdcatalogo.Visible = activaacgregar;
            this.btnAgregarListaCatalogo.Visible = !activaacgregar;
            this.btnModificarListaCatalogo.Visible = !activaacgregar;
           // this.btnVerSubCatalogo.Visible = !activaacgregar;

            this.btnAgregarListaSubCatalogo.Visible = !activaacgregar;
            this.btnModificarListaSubCatalogo.Visible = !activaacgregar;

            this.btnEliminarCatalogo.Visible = !activaacgregar;
            this.btnAgregarCatalogo.Visible = semuestrbotonaagregar;
            this.btnModificarCatalogo.Visible = !semuestrbotonaagregar;
            this.GrdvCatalogos.Columns[0].Visible = !activaacgregar;
            if (this.GrdvCatalogos.SelectedIndex == -1)
            {
                this.btnModificarListaCatalogo.Visible = false;
                this.btnEliminarCatalogo.Visible = false;
          //      this.btnVerSubCatalogo.Visible = false;
                
            }
            this.pnladdSubCatalogo.Visible = activaacgregar; ;

        }

        protected void ddlElemXPage_SelectedIndexChanged(object sender, EventArgs e)
        {
         this.GrdvCatalogos.PageSize = int.Parse(this.ddlElemXPage.SelectedValue);
            
        }

        protected void btnAgregarListaCatalogo_Click(object sender, EventArgs e)
        {
            
            this.lblAddmodifyCatalogo.Text = "AGREGAR CATALOGO DE MOVIMIENTO DE BANCO";
            this.GrdvCatalogos.SelectedIndex = -1;
            this.ActualizaPagina(true, true);
            this.pnladdSubCatalogo.Visible = false;
            this.pnlSubCatalogo.Visible = false;
            //this.limpiarCampos();
            
        }

        protected void btnModificarListaCatalogo_Click(object sender, EventArgs e)
        {
            this.ddlGrupoCat.DataBind();
            this.lblAddmodifyCatalogo.Text="MODIFICAR CATALOGO DE MOVIMIENTO DE BANCO";
            this.ddlGrupoCat.SelectedValue = this.GrdvCatalogos.SelectedDataKey["grupoCatalogoID"].ToString();
            this.txtclaveCatalogo.Text = this.GrdvCatalogos.SelectedDataKey["claveCatalogo"].ToString();
            this.txtcatalogoMovBanco.Text = this.GrdvCatalogos.SelectedDataKey["catalogoMovBanco"].ToString();
            this.ActualizaPagina(true, false);
            this.pnladdSubCatalogo.Visible = false;
            this.pnlSubCatalogo.Visible = false;
        }

        
        
        protected void btnCancelarCatalogo_Click(object sender, EventArgs e)
        {
            this.ActualizaPagina(false, true);
            this.pnladdSubCatalogo.Visible = false;
            this.pnlSubCatalogo.Visible = false;
        }

        protected void GrdvCatalogos_SelectedIndexChanged1(object sender, EventArgs e)
        {
//             bool existe = false;
//             string value = this.GrdvCatalogos.SelectedDataKey[0].ToString();
//             foreach (ListItem list in drpdlCatalogoforSub.Items)
//             {
//                 if(list.Value==value){
//                     existe = true;
//                 }
//             }

//            this.btnVerSubCatalogo.Visible = existe;
           
          
             this.btnModificarListaCatalogo.Visible = true;
            this.btnEliminarCatalogo.Visible = true;
           
            string msgDel = "return confirm('¿Realmente desea eliminar el Catálogo: ";
            msgDel += this.GrdvCatalogos.SelectedDataKey["catalogoMovBanco"].ToString();
            msgDel += "?')";

            this.btnEliminarCatalogo.Attributes.Add("onclick", msgDel); 
        }

        protected void btnAgregarCatalogo_Click(object sender, EventArgs e)
        {
            string qryIns = "INSERT INTO catalogoMovimientosBancos (grupoCatalogoID, claveCatalogo, catalogoMovBanco) VALUES (@grupoCatalogoID, @claveCatalogo, @catalogoMovBanco)";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            try
            {
                cmdIns.Parameters.Add("@grupoCatalogoID", SqlDbType.Int).Value = int.Parse(this.ddlGrupoCat.SelectedValue);
                cmdIns.Parameters.Add("@claveCatalogo", SqlDbType.VarChar).Value = this.txtclaveCatalogo.Text;
                cmdIns.Parameters.Add("@catalogoMovBanco", SqlDbType.VarChar).Value = this.txtcatalogoMovBanco.Text;
                


                conGaribay.Open();
                int numregistros = cmdIns.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception("EL CATALOGO NO PUDO SER AGREGADO, DADO QUE LA FUNCION A LA BASE DE DATOS REGRESO QUE SE AGREGARON "+numregistros +" REGISTROS.");
                }

                this.ActualizaPagina(false, true);
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = false;
                this.imagenbien.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = "El CATÁLOGO DEL MOVIMIENTO DE BANCO " + this.txtcatalogoMovBanco.Text.ToUpper() +"SE HA AGREGADO SATISFACTORIAMENTE";//string.Format(myConfig.StrFromMessages("CICLOADDEDEXITO"), this.txtNombreCiclo.Text.ToUpper());
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CATALOGO, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("SE AGREGÓ EL CATALOGO: " + this.txtcatalogoMovBanco.Text.ToUpper()));
                //this.limpiarCampos();
            }
            catch (Exception err1)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "El CATÁLOGO DEL MOVIMIENTO NO HA PODIDO SER AGREGADA";//string.Format(myConfig.StrFromMessages("CICLOADDEDFAILED"), this.txtNombreCiclo.Text.ToUpper());
                this.lblMensajeException.Text = err1.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());
            }
            finally{

                conGaribay.Close();
            }


        }

        protected void btnModificarCatalogo_Click(object sender, EventArgs e)
        {


            string qryUp = "UPDATE catalogoMovimientosBancos SET grupoCatalogoID = @grupoCatalogoID, claveCatalogo = @claveCatalogo, catalogoMovBanco = @catalogoMovBanco WHERE catalogoMovBancoID = @catalogoMovBancoID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdUp = new SqlCommand(qryUp, conGaribay);
            
            try
            {
                cmdUp.Parameters.Add("@grupoCatalogoID", SqlDbType.Int).Value = int.Parse(this.ddlGrupoCat.SelectedValue);
                cmdUp.Parameters.Add("@claveCatalogo", SqlDbType.VarChar).Value = this.txtclaveCatalogo.Text;
                cmdUp.Parameters.Add("@catalogoMovBanco", SqlDbType.VarChar).Value = this.txtcatalogoMovBanco.Text;
                cmdUp.Parameters.Add("@catalogoMovBancoID", SqlDbType.Int).Value = int.Parse(this.GrdvCatalogos.SelectedDataKey["catalogoMovBancoID"].ToString());
                conGaribay.Open();
                int numregistros = cmdUp.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception("EL CATALOGO NO PUDO SER MODIFICADO, DADO QUE LA FUNCION A LA BASE DE DATOS REGRESO QUE SE MODIFICARON "+numregistros +" REGISTROS.");
                }
                if (this.GrdvCatalogos.SelectedDataKey["catalogoMovBanco"].ToString() != this.txtcatalogoMovBanco.Text)
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CATALOGO, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ EL NOMBRE DEL CATALOGO: \"" + this.GrdvCatalogos.SelectedDataKey["catalogoMovBanco"].ToString().ToUpper() + "\" POR: \"" + this.txtcatalogoMovBanco.Text.ToUpper()) + "\"");
                    this.lblMensajeOperationresult.Text = "SE MODIFICÓ EL NOMBRE DEL CATALOGO: \"" + this.GrdvCatalogos.SelectedDataKey["catalogoMovBanco"].ToString().ToUpper() + "\" POR: \"" + this.txtcatalogoMovBanco.Text.ToUpper() + "\" SATISFACTORIAMENTE";
                }
                else { Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CATALOGO, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ EL CATALOGO: " + this.txtcatalogoMovBanco.Text.ToUpper()));
                this.lblMensajeOperationresult.Text = "EL CATALOGO " + this.GrdvCatalogos.SelectedDataKey["catalogoMovBanco"].ToString().ToUpper() +" SE MODIFICÓ SATISFACTORIAMENTE";
                }
                this.ActualizaPagina(false, true);
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = false;
                this.imagenbien.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                
            }
            catch (InvalidOperationException err1)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "EL CATALOGO " + this.GrdvCatalogos.SelectedDataKey["catalogoMovBanco"].ToString().ToUpper() + " NO HA PODIDO SER MODIFICADO";
                
                this.lblMensajeException.Text = err1.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());
            }
            catch (SqlException err2)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "EL CATALOGO " + this.GrdvCatalogos.SelectedDataKey["catalogoMovBanco"].ToString().ToUpper() + " NO HA PODIDO SER MODIFICADO";
                this.lblMensajeException.Text = err2.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());
            }
            catch (Exception err3)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "EL CATALOGO " + this.GrdvCatalogos.SelectedDataKey["catalogoMovBanco"].ToString().ToUpper() + " NO HA PODIDO SER MODIFICADO";
                this.lblMensajeException.Text = err3.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err3.Message, this.Request.Url.ToString());
            }
            finally
            {
                conGaribay.Close();
            }
        }

        protected void btnEliminarCatalogo_Click(object sender, EventArgs e)
        {
            string qryDel = "DELETE FROM catalogoMovimientosBancos WHERE catalogoMovBancoID=@catalogoMovBancoID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdDel = new SqlCommand(qryDel, conGaribay);

            try
            {
                cmdDel.Parameters.Add("@catalogoMovBancoID", SqlDbType.VarChar).Value = this.GrdvCatalogos.SelectedDataKey["catalogoMovBancoID"].ToString();

                conGaribay.Open();
                
                int numregistros = cmdDel.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception("EL CATALOGO NO PUDO SER ELIMINADO, DADO QUE LA FUNCION A LA BASE DE DATOS REGRESO QUE SE ELIMINARON " + numregistros + " REGISTROS.");
                }
                
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = false;
                this.imagenbien.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = "EL CATALOGO " + this.GrdvCatalogos.SelectedDataKey["catalogoMovBanco"].ToString().ToUpper() + " HA SIDO ELIMINADO SATISFACTORIAMENTE";
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION

                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CATALOGO, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ EL CATÁLOGO " + this.GrdvCatalogos.SelectedDataKey["catalogoMovBanco"].ToString().ToUpper()));
                this.GrdvCatalogos.SelectedIndex = -1;
                this.ActualizaPagina(false, true);
            }
            catch (InvalidOperationException err1)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "EL CATALOGO " + this.GrdvCatalogos.SelectedDataKey["catalogoMovBanco"].ToString().ToUpper() + " NO HA PODIDO SER ELIMINADO";
                this.lblMensajeException.Text = err1.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());

            }
            catch (SqlException err2)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "EL CATALOGO " + this.GrdvCatalogos.SelectedDataKey["catalogoMovBanco"].ToString().ToUpper() + " NO HA PODIDO SER ELIMINADO";
                this.lblMensajeException.Text = err2.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());

            }
            catch (Exception err3)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "EL CATALOGO " + this.GrdvCatalogos.SelectedDataKey["catalogoMovBanco"].ToString().ToUpper() + " NO HA PODIDO SER ELIMINADO";
                this.lblMensajeException.Text = err3.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err3.Message, this.Request.Url.ToString());
            }
            finally
            {
                conGaribay.Close();
                
            }
        }

        protected void ddlElemXpageSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grdvSubCatalogos.PageSize = int.Parse(this.ddlElemXpageSub.SelectedValue);
        }

        protected void ActualizaPagina2(Boolean activaacgregar, Boolean semuestrbotonaagregar)
        {


            this.pnladdSubCatalogo.Visible = activaacgregar;
            this.btnAgregarListaSubCatalogo.Visible = !activaacgregar;
            this.btnModificarListaSubCatalogo.Visible = !activaacgregar;
            
            
            this.btnEliminarSubCatalogo.Visible = !activaacgregar;
            this.btnAgregarSubCatalogo.Visible = semuestrbotonaagregar;
            this.btnModificarSubCatalogo.Visible = !semuestrbotonaagregar;
            this.grdvSubCatalogos.Columns[0].Visible = !activaacgregar;
            if (this.grdvSubCatalogos.SelectedIndex == -1)
            {
                this.btnModificarListaSubCatalogo.Visible = false;
                this.btnEliminarSubCatalogo.Visible = false;
            }
            this.pnladdSubCatalogo.Visible = activaacgregar; 

        }

        protected void grdvSubCatalogos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ActualizaPagina2(false, true);
            string msgDel = "return confirm('¿Realmente desea eliminar el sub-catálogo: ";
            msgDel += this.grdvSubCatalogos.SelectedDataKey["subCatalogo"].ToString();
            msgDel += "?')";

            this.btnEliminarSubCatalogo.Attributes.Add("onclick", msgDel);
            
            this.grdvSubCatalogos.DataSourceID = "SqlSubCatalogos";
        }

        

        protected void btnAgregarListaSubCatalogo_Click(object sender, EventArgs e)
        {

            this.GrdvCatalogos.DataBind();
            //this.SqlSubCatalogos.FilterExpression = "catalogoMovBancoID = " + this.GrdvCatalogos.SelectedDataKey["catalogoMovBancoID"].ToString();
            this.lblAddmodifySubcatalogo.Text = "AGREGAR SUB-CATALOGO DE MOVIMIENTO DE BANCO";
            
            this.grdvSubCatalogos.DataSourceID = "SqlSubCatalogos";
            this.grdvSubCatalogos.DataBind();
            this.grdvSubCatalogos.SelectedIndex = -1;
            this.ActualizaPagina2(true, true);
            this.txtCalveSubCatalogo.Text = "";
            this.txtSubCatalogo.Text = "";
            //this.limpiarCampos();

        }

        protected void btnModificarListaSubCatalogo_Click(object sender, EventArgs e)
        {
            this.ddlCatalogo.DataBind();
            
            this.lblAddmodifySubcatalogo.Text = "MODIFICAR SUB-CATÁLOGO \""+this.grdvSubCatalogos.SelectedDataKey["subCatalogo"].ToString()+"\" DE MOVIMIENTO DE BANCO";

            this.drpdlGrupoAddSubcatalogo.SelectedValue = this.grdvSubCatalogos.SelectedDataKey["grupoCatalogoID"].ToString();
            this.drpdlGrupoAddSubcatalogo.DataBind();
            this.ddlCatalogo.DataBind();
            this.ddlCatalogo.SelectedValue = this.grdvSubCatalogos.SelectedDataKey["catalogoMovBancoID"].ToString();
            this.txtCalveSubCatalogo.Text = this.grdvSubCatalogos.SelectedDataKey["subCatalogoClave"].ToString();
            this.txtSubCatalogo.Text = this.grdvSubCatalogos.SelectedDataKey["subCatalogo"].ToString();
            
            this.grdvSubCatalogos.DataSourceID = "SqlSubCatalogos";
            this.grdvSubCatalogos.DataBind();
            this.ActualizaPagina2(true, false);
            

        }

        protected void btnCancelarSubCatalogo_Click(object sender, EventArgs e)
        {
            this.ActualizaPagina2(false, true);
            //his.lblAddmodifyCatalogo.Text = "AGREGAR SUB-CATALOGO DE MOVIMIENTO DE BANCO";
       
        }

        protected void btnAgregarSubCatalogo_Click(object sender, EventArgs e)
        {
            string sqlSacaLastID = "select max(subCatalogoMovBancoID) from subCatalogoMovimientoBanco";
            SqlConnection conSacalastID = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdSacalastID = new SqlCommand(sqlSacaLastID, conSacalastID);
            conSacalastID.Open();
            int ID = 1;
            try
            {
                object lastID = cmdSacalastID.ExecuteScalar();
                ID= lastID!=null ? int.Parse(lastID.ToString()) + 1 : 1;
                
            }
            catch (Exception ex){
                Logger.Instance.LogMessage(Logger.typeLogMessage.DEBUG, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL AGREGAR CATALOGO. " + ex.Message, this.Request.Url.ToString());
            }
            finally{
                conSacalastID.Close();
            }

            string qryIns = "INSERT INTO subCatalogoMovimientoBanco (subCatalogoMovBancoID,subCatalogoClave, subCatalogo, catalogoMovBancoID) VALUES (@subCatalogoMovBancoID,@subCatalogoClave, @subCatalogo, @catalogoMovBancoID)";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            try
            {
                cmdIns.Parameters.Add("@subCatalogoMovBancoID", SqlDbType.Int).Value = ID;
                cmdIns.Parameters.Add("@subCatalogoClave", SqlDbType.VarChar).Value = this.txtCalveSubCatalogo.Text;
                
                cmdIns.Parameters.Add("@subCatalogo", SqlDbType.VarChar).Value = this.txtSubCatalogo.Text;
                cmdIns.Parameters.Add("@catalogoMovBancoID", SqlDbType.Int).Value = int.Parse(this.ddlCatalogo.SelectedValue);



                conGaribay.Open();
                int numregistros = cmdIns.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception("EL SUB-CATALOGO NO PUDO SER AGREGADO, DADO QUE LA FUNCION A LA BASE DE DATOS REGRESO QUE SE AGREGARON " + numregistros + " REGISTROS.");
                }

                this.ActualizaPagina2(false, true);
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = false;
                this.imagenbien.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = "El SUB-CATÁLOGO " +this.txtSubCatalogo.Text.ToUpper()+ " SE HA AGREGADO SATISFACTORIAMENTE";
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CATALOGO, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("SE AGREGÓ EL SUBCATALOGO: " + this.txtSubCatalogo.Text.ToUpper()));
                //this.limpiarCampos();
            }
            catch (Exception err1)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "El SUB-CATÁLOGO " + this.txtSubCatalogo.Text.ToUpper() + " NO SE HA PODIDO AGREGAR";
                this.lblMensajeException.Text = err1.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());
            }
            finally
            {

                conGaribay.Close();
            }
            if (this.GrdvCatalogos.SelectedDataKey != null && 
                this.GrdvCatalogos.SelectedDataKey["catalogoMovBancoID"] != null) 
            { 
                this.SqlSubCatalogos.FilterExpression = "catalogoMovBancoID = " + this.GrdvCatalogos.SelectedDataKey["catalogoMovBancoID"].ToString(); 
            }
            this.grdvSubCatalogos.DataSourceID = "SqlSubCatalogos";

        }

        protected void btnModificarSubCatalogo_Click(object sender, EventArgs e)
        {
            string qryUp = "UPDATE subCatalogoMovimientoBanco SET subCatalogoClave = @subCatalogoClave, subCatalogo = @subCatalogo, catalogoMovBancoID = @catalogoMovBancoID WHERE subCatalogoMovBancoID = @subCatalogoMovBancoID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdUp = new SqlCommand(qryUp, conGaribay);

            try
            {
                cmdUp.Parameters.Add("@subCatalogoClave", SqlDbType.VarChar).Value = this.txtCalveSubCatalogo.Text;
                cmdUp.Parameters.Add("@subCatalogo", SqlDbType.VarChar).Value = this.txtSubCatalogo.Text;
                cmdUp.Parameters.Add("@catalogoMovBancoID", SqlDbType.Int).Value = int.Parse(this.ddlCatalogo.SelectedValue);
                cmdUp.Parameters.Add("@subCatalogoMovBancoID", SqlDbType.Int).Value = this.grdvSubCatalogos.SelectedDataKey["subCatalogoMovBancoID"].ToString();
                conGaribay.Open();
                int numregistros = cmdUp.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception("EL SUB-CATALOGO NO PUDO SER MIDIFICADO, DADO QUE LA FUNCION A LA BASE DE DATOS REGRESO QUE SE MODIFICARON " + numregistros + " REGISTROS.");
                }
                if (this.grdvSubCatalogos.SelectedDataKey["subCatalogo"].ToString() != this.txtSubCatalogo.Text)
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CATALOGO, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ EL NOMBRE DEL SUB-CATALOGO: \"" + this.grdvSubCatalogos.SelectedDataKey["subCatalogo"].ToString().ToUpper() + "\" POR: \"" + this.txtSubCatalogo.Text + "\""));
                    this.lblMensajeOperationresult.Text = "SE MODIFICÓ EL NOMBRE DEL SUB-CATALOGO: \"" + this.grdvSubCatalogos.SelectedDataKey["subCatalogo"].ToString().ToUpper() + "\" POR: \"" + this.txtSubCatalogo.Text + "\"";
                }
                else {
                    this.lblMensajeOperationresult.Text = "El SUB-CATÁLOGO " + this.txtSubCatalogo.Text.ToUpper() + " SE HA MODIFICADO SATISFACTORIAMENTE";
                }
                this.ActualizaPagina2(false, true);
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = false;
                this.imagenbien.Visible = true;
                
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CATALOGO, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ EL SUBCATALOGO: " + this.txtSubCatalogo.Text.ToUpper()));
            }
            catch (InvalidOperationException err1)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "El SUB-CATÁLOGO " + this.txtSubCatalogo.Text.ToUpper() + " NO SE HA PODIDO MODIFICAR";
                this.lblMensajeException.Text = err1.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());
            }
            catch (SqlException err2)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "El SUB-CATÁLOGO " + this.txtSubCatalogo.Text.ToUpper() + " NO SE HA PODIDO MODIFICAR";
                this.lblMensajeException.Text = err2.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());
            }
            catch (Exception err3)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "El SUB-CATÁLOGO " + this.txtSubCatalogo.Text.ToUpper() + " NO SE HA PODIDO MODIFICAR";
                this.lblMensajeException.Text = err3.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err3.Message, this.Request.Url.ToString());
            }
            finally
            {
                conGaribay.Close();
            }
           
            this.grdvSubCatalogos.DataSourceID = "SqlSubCatalogos";

        }

        protected void btnEliminarSubCatalogo_Click(object sender, EventArgs e)
        {
            string qryDel = "DELETE FROM subCatalogoMovimientoBanco WHERE subCatalogoMovBancoID=@subCatalogoMovBancoID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdDel = new SqlCommand(qryDel, conGaribay);

            try
            {
                cmdDel.Parameters.Add("@subCatalogoMovBancoID", SqlDbType.Int).Value = int.Parse(this.grdvSubCatalogos.SelectedDataKey["subCatalogoMovBancoID"].ToString());

                conGaribay.Open();

                int numregistros = cmdDel.ExecuteNonQuery();
                if (numregistros != 1)
                {

                    throw new Exception("EL SUB-CATALOGO NO PUDO SER ELIMINADO, DADO QUE LA FUNCION A LA BASE DE DATOS REGRESO QUE SE ELIMINARON " + numregistros + " REGISTROS.");
                }
                this.SqlSubCatalogos.FilterExpression = "catalogoMovBancoID = " + this.GrdvCatalogos.SelectedDataKey["catalogoMovBancoID"].ToString();
                this.grdvSubCatalogos.DataSourceID = "SqlSubCatalogos";
                this.grdvSubCatalogos.SelectedIndex = -1;
                this.ActualizaPagina2(false, true);
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = false;

                this.imagenbien.Visible = true;
                this.lblMensajeOperationresult.Text = "El SUB-CATÁLOGO " + this.grdvSubCatalogos.SelectedDataKey["subCatalogo"].ToString().ToUpper() + " SE HA ELIMINADO SATISFACTORIAMENTE";
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION

                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CATALOGO, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ EL CATÁLOGO: " + this.grdvSubCatalogos.SelectedDataKey["subCatalogo"].ToString().ToUpper()));
            }
            catch (InvalidOperationException err1)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "El SUB-CATÁLOGO " + this.grdvSubCatalogos.SelectedDataKey["subCatalogo"].ToString().ToUpper() + " NO HA PODIDO SER ELIMINADO";
                this.lblMensajeException.Text = err1.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());

            }
            catch (SqlException err2)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "El SUB-CATÁLOGO " + this.grdvSubCatalogos.SelectedDataKey["subCatalogo"].ToString().ToUpper() + " NO HA PODIDO SER ELIMINADO";
                this.lblMensajeException.Text = err2.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());

            }
            catch (Exception err3)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "El SUB-CATÁLOGO " + this.grdvSubCatalogos.SelectedDataKey["subCatalogo"].ToString().ToUpper() + " NO HA PODIDO SER ELIMINADO";
                this.lblMensajeException.Text = err3.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err3.Message, this.Request.Url.ToString());
            }
            finally
            {
                conGaribay.Close();

            }
            

        }

        protected void btnVerSubCatalogo_Click(object sender, EventArgs e)
        {
            
            //this.lblsubCatalogos.Text = "SUB-CATÁLOGOS DEL CATÁLOGO " + this.GrdvCatalogos.SelectedDataKey["catalogoMovBanco"].ToString().ToUpper();
            this.panelCatalogo.Visible = false;
            this.Pnladdcatalogo.Visible = false;
            this.pnlSubCatalogo.Visible = true;
            this.pnladdSubCatalogo.Visible = false;
            bool existe = false;
            if (this.GrdvCatalogos.SelectedDataKey != null)
            {
                string value = this.GrdvCatalogos.SelectedDataKey[0].ToString();
                foreach (ListItem list in drpdlCatalogoforSub.Items)
                {
                    if (list.Value == value)
                    {
                        existe = true;
                    }
                }
                if(existe){
                    this.drpdlCatalogoforSub.SelectedValue = value;
                }
            }
            
        }

        protected void btnVerCatalogos_Click(object sender, EventArgs e)
        {
            this.panelCatalogo.Visible = true;
            this.pnlSubCatalogo.Visible = false;
        }

        protected void rbTodos_CheckedChanged(object sender, EventArgs e)
        {
            this.GrdvCatalogos.DataSourceID = "SqlSubCatalogos";
            this.lblsubCatalogos.Text="LISTA DE TODOS LOS SUB-CATÁLOGOS";
        }
        protected void rbSelec_CheckedChanged(object sender, EventArgs e)
        {
            this.GrdvCatalogos.DataBind();
            this.lblsubCatalogos.Text = "SUB-CATÁLOGOS DEL CATÁLOGO " + this.GrdvCatalogos.SelectedDataKey["catalogoMovBanco"].ToString().ToUpper();
            this.SqlSubCatalogos.FilterExpression = "catalogoMovBancoID = " + this.GrdvCatalogos.SelectedDataKey["catalogoMovBancoID"].ToString();
            this.grdvSubCatalogos.DataSourceID = "SqlSubCatalogos";
        }

        protected void drpdlCatalogoforSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pnladdSubCatalogo.Visible = false;
            this.GrdvCatalogos.SelectedIndex = -1;
            this.ActualizaPagina2(false, true);
        }

        protected void drpDlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Pnladdcatalogo.Visible = false;
            this.grdvSubCatalogos.SelectedIndex = -1;
            this.ActualizaPagina(false, true);
               
        }

        protected void buttonSend_Click(object sender, EventArgs e)
        {

            string asunto = "SOLICITUD PARA CAMBIAR CATALOGOS";
            StringBuilder contenido = new StringBuilder("EL USUARIO: ");
            contenido.Append(this.CurrentUserName);
            contenido.Append(" SOLICITÓ QUE SE ");
            if(this.checkBoxListAction.Items[0].Selected)
            {
                contenido.Append("AGREGARA EL CATÁLOGO: ");
                contenido.Append(this.textBoxCatalogo.Text);
            }
            else
            {
                if(this.checkBoxListAction.Items[1].Selected)
                {
                    contenido.Append("MODIFICARA EL CATÁLOGO: ");
                    contenido.Append(this.textBoxCatalogo.Text); 
                    contenido.Append(", POR ESTE OTRO: ");
                    contenido.Append(this.textBoxNuevoCatalogo.Text); 
                }
                else
                {
                    contenido.Append("AGREGARA EL CATÁLOGO: ");
                    contenido.Append(this.textBoxCatalogo.Text);                    
                }
            }
            contenido.Append("<br /> ENTRE AL SISTEMA EN LA SECCIÓN DE CONFIGURACIÓN Y ADMINISTRACIÓN / CATALOGOS Y SUBCATÁLOGOS PARA REALIZAR CUALQUIER CAMBIO.");
            EMailUtils.SendTextEmail("pgaribay35@hotmail.com, cheliskis@gmail.com", asunto,contenido.ToString().ToUpper(),true);
            this.lblAddResult.Text = "SOLICITUD ENVIADA EXITOSAMENTE";
            this.lblAddResult.Visible = true;
        }

    }
}
