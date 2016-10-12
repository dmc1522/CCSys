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
using System.Globalization;

namespace Garibay
{
    public partial class frmListSolicitudes : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsSistemBanco)
            {
                this.dataSourceLista.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.dataSourceLista.SelectCommand);
                this.dataSourceCredito.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.dataSourceCredito.SelectCommand);
            }
            if(!this.IsPostBack)
            {
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.SELECT, int.Parse(Session["USERID"].ToString()), "VISITÓ LA PAGINA DE LISTA DE SOLICITUDES");
                this.Eliminar(false);
                this.ddlCiclo.DataBind();
                this.ddlCredito.DataBind();
                dataSourceLista.FilterExpression = cadenaFiltros();
                this.btnImprimir.Visible = false;
                this.btnImprimirContrato.Visible = false;
                this.gridSolicitudes.PageSize = 500;
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
                this.btnAgregar.Visible = false;
                this.gridSolicitudes.Columns[0].Visible = false;
             
            }

        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/frmAddSolicitud.aspx");
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            dataSourceLista.FilterExpression = cadenaFiltros();
            this.gridSolicitudes.DataSourceID = "dataSourceLista";

            this.gridSolicitudes.PageIndex = 0;
            this.gridSolicitudes.DataBind();
        }

        private void showHideColumns()
        {
            foreach (DataControlField col in this.gridSolicitudes.Columns)
            {
                ListItem item = this.cblMostrar.Items.FindByText(col.HeaderText);
                if (item != null)
                {
                    col.Visible = item.Selected;
                }
            }
        }


        protected string cadenaFiltros()
        {
            string filtros = "";
            filtros += "cicloID = " + this.ddlCiclo.SelectedValue.ToString();
            if(this.cbCredito.Checked)
            {
                filtros += "AND creditoID = " + this.ddlCredito.SelectedValue.ToString();
            }
            

            return filtros;
        }

        protected void btnActualiza_Click(object sender, EventArgs e)
        {
            dataSourceLista.FilterExpression = cadenaFiltros();
            this.showHideColumns();
        }

        protected void btnSelAll_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in this.cblMostrar.Items)
            {
                item.Selected = true;
            }
            dataSourceLista.FilterExpression = cadenaFiltros();
            this.showHideColumns();
        }

        protected void btnUnSel_Click(object sender, EventArgs e)

        {
            foreach (ListItem item in this.cblMostrar.Items)
            {
                item.Selected = false;
            }
            dataSourceLista.FilterExpression = cadenaFiltros();
            this.showHideColumns();
        }

        protected void ddlCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataSourceLista.FilterExpression = cadenaFiltros();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //this.gridSolicitudes.DataBind();
            if (this.gridSolicitudes.SelectedDataKey[0]!=null)
            {
                string idtomodify;
                idtomodify = this.gridSolicitudes.SelectedDataKey[0].ToString();
                string strRedirect = "~/frmAddSolicitud.aspx";
                string datosaencriptar;
                datosaencriptar = "idtomodify=";
                datosaencriptar += idtomodify;
                datosaencriptar += "&";
                strRedirect += "?data=";
                strRedirect += Utils.encriptacadena(datosaencriptar);
                Response.Redirect(strRedirect, true);
            }
            else
            {
                return;
            }
        }

       

        protected void btnImprimirContrato_Click(object sender, EventArgs e)
        {
            if (this.gridSolicitudes.SelectedDataKey[0] != null)
            {
                FormatosPdf.imprimeContrato(int.Parse(this.gridSolicitudes.SelectedDataKey[0].ToString()),this.UserID);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        protected void gridSolicitudes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType != DataControlRowType.DataRow){
                return;
            }
            //HyperLink link = (HyperLink)e.Row.FindControl("HyperLink1");
//             if (link != null)
//             {
//                 link.Text = "Abrir";
//                 String sQuery = "idtomodify=" + this.gridSolicitudes.DataKeys[e.Row.RowIndex]["solicitudID"].ToString();
//                 sQuery = Utils.GetEncriptedQueryString(sQuery);
//                 String strRedirect = "~/frmAddSolicitud.aspx";
//                 strRedirect += sQuery;
//                 link.NavigateUrl = strRedirect;
//             }

            HyperLink link = (HyperLink)e.Row.FindControl("lnkSolicitud2010");
            if (link != null)
            {
                String sQuery = "SolID=" + this.gridSolicitudes.DataKeys[e.Row.RowIndex]["solicitudID"].ToString();
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = "~/frmAddSolicitud2010.aspx";
                strRedirect += sQuery;
                link.NavigateUrl = strRedirect;
                link.Text = "Abrir";
            }

//             LinkButton LB = (LinkButton)e.Row.FindControl("LBPrintSolicitud");
//             if(LB!=null)
//             {
//                 string sFileName = "SOLICITUD.pdf";
//                 sFileName = sFileName.Replace(" ", "_");
//                 String sURL = "frmDescargaTmpFile.aspx";
//                 string datosaencriptar;
//                 datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
//                 datosaencriptar = datosaencriptar + "solID=" + this.gridSolicitudes.DataKeys[e.Row.RowIndex]["solicitudID"].ToString() + "&";
//                 datosaencriptar += "impsol=1&";
//                 String URLcomplete = sURL + "?data=";
//                 URLcomplete += Utils.encriptacadena(datosaencriptar);
//                 //LinkButton l = new LinkButton();
//                 JSUtils.OpenNewWindowOnClick(ref LB,URLcomplete, "Solicitud ", true);
//                 LB.Text = "SOLICITUD";
//             } 
            LinkButton LB = (LinkButton)e.Row.FindControl("LBPrintContrato");
            if(LB!=null)
            {
                string sFileName = "CONTRATO.pdf";
                sFileName = sFileName.Replace(" ", "_");
                string sURL = "frmDescargaTmpFile.aspx";
                string datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
                datosaencriptar = datosaencriptar + "solID=" + this.gridSolicitudes.DataKeys[e.Row.RowIndex]["solicitudID"].ToString() + "&";
                datosaencriptar += "impcont=1&";
                string URLcomplete = sURL + "?data=";
                URLcomplete += Utils.encriptacadena(datosaencriptar);
                JSUtils.OpenNewWindowOnClick(ref LB, URLcomplete, "Contrato", true);
                LB.Text = "CONTRATO";
            }
//             LB = (LinkButton)e.Row.FindControl("LBPrintCaratula");
//             if(LB!=null)
//             {    
//                 string sFileName = "CARATULA_ANEXA.pdf";
//                 sFileName = sFileName.Replace(" ", "_");
//                 string sURL = "frmDescargaTmpFile.aspx";
//                 string datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
//                 datosaencriptar = datosaencriptar + "solID=" + this.gridSolicitudes.DataKeys[e.Row.RowIndex]["solicitudID"].ToString() + "&";
//                 datosaencriptar += "impcar=1&";
//                 string URLcomplete = sURL + "?data=";
//                 URLcomplete += Utils.encriptacadena(datosaencriptar);
//                 LB.Text = "CARATULA ANEXA";
//                 JSUtils.OpenNewWindowOnClick(ref LB, URLcomplete, "Caratula_Anexa", true);
//             }

//             LB = (LinkButton)e.Row.FindControl("LBPrintSeguro");
//             if(LB!=null){
//                     string sFileName = "CONTRATO_SEGURO.pdf";
//                     sFileName = sFileName.Replace(" ", "_");
//                     string sURL = "frmDescargaTmpFile.aspx";
//                     string datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
//                     datosaencriptar = datosaencriptar + "solID=" + this.gridSolicitudes.DataKeys[e.Row.RowIndex]["solicitudID"].ToString() + "&";
//                     datosaencriptar += "impseg=1&";
//                     string URLcomplete = sURL + "?data=";
//                     URLcomplete += Utils.encriptacadena(datosaencriptar);
//                     JSUtils.OpenNewWindowOnClick(ref LB, URLcomplete, "Contrato Seguro", true);
//             }
            HyperLink lnk = (HyperLink)e.Row.FindControl("lnkPagare");
            if (lnk != null)
            {
                Solicitudes sol;
                if ( this.IsSistemBanco )
                    sol = SolicitudesSisBancos.Get(int.Parse(this.gridSolicitudes.DataKeys[e.Row.RowIndex]["solicitudID"].ToString()));
                else
                    sol = Solicitudes.Get(int.Parse(this.gridSolicitudes.DataKeys[e.Row.RowIndex]["solicitudID"].ToString()));
                string sFileName = "PAGARE.pdf";
                sFileName = sFileName.Replace(" ", "_");
                string sURL = "frmDescargaTmpFile.aspx";
                string datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
                datosaencriptar = datosaencriptar + "creditoID=" + sol.CreditoID.ToString() + "&";
                datosaencriptar += "solID=" + sol.SolicitudID.ToString() + "&";
                datosaencriptar += "impPagare=1&";
                string URLcomplete = sURL + "?data=";
                URLcomplete += Utils.encriptacadena(datosaencriptar);
                lnk.NavigateUrl = this.Request.Url.ToString();
                lnk.Text = "PAGARE";
                JSUtils.OpenNewWindowOnClick(ref lnk, URLcomplete, "Pagare", true);
            }
            LB = (LinkButton)e.Row.FindControl("LBTermsAndCon");
            if(LB!=null)
            {
                string sFileName = "TERMINOS Y CONDICIONES.pdf";
                sFileName = sFileName.Replace(" ", "_");
                string sURL = "frmDescargaTmpFile.aspx";
                string datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
                datosaencriptar = datosaencriptar + "solID=" + this.gridSolicitudes.DataKeys[e.Row.RowIndex]["solicitudID"].ToString() + "&";
                datosaencriptar += "imptermsandcond=1&";
                string URLcomplete = sURL + "?data=";
                URLcomplete += Utils.encriptacadena(datosaencriptar);
                LB.Text = "TERMINOS Y CONDICIONES";
                JSUtils.OpenNewWindowOnClick(ref LB, URLcomplete, "Terminos y condiciones", true);
            }
            LB = (LinkButton)e.Row.FindControl("LBSolicitud2010");
            if(LB!=null)
            {
                string sFileName = "SOLICITUD.pdf";
                sFileName = sFileName.Replace(" ", "_");
                string sURL = "frmDescargaTmpFile.aspx";
                string datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
                datosaencriptar = datosaencriptar + "solID=" + this.gridSolicitudes.DataKeys[e.Row.RowIndex]["solicitudID"].ToString() + "&";
                datosaencriptar += "impSol2010=1&";
                string URLcomplete = sURL + "?data=";
                URLcomplete += Utils.encriptacadena(datosaencriptar);
                JSUtils.OpenNewWindowOnClick(ref LB, URLcomplete, "Solicitud", true);
                LB.Text = "SOLICITUD";
            }
            LB = (LinkButton)e.Row.FindControl("LBBuroCredito");
            if(LB!=null)
            {
                string sFileName = "BUROCREDITO.pdf";
                sFileName = sFileName.Replace(" ", "_");
                string sURL = "frmDescargaTmpFile.aspx";
                string datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
                datosaencriptar = datosaencriptar + "solID=" + this.gridSolicitudes.DataKeys[e.Row.RowIndex]["solicitudID"].ToString() + "&";
                datosaencriptar += "buroCredito=1&";
                string URLcomplete = sURL + "?data=";
                URLcomplete += Utils.encriptacadena(datosaencriptar);
                JSUtils.OpenNewWindowOnClick(ref LB, URLcomplete, "BuroCredito", true);
                LB.Text = "BURO";
            }
            LB = (LinkButton)e.Row.FindControl("LBCartaCompromiso");
            if(LB!=null)
            {
                string sFileName = "CARTACOMPROMISO.pdf";
                sFileName = sFileName.Replace(" ", "_");
                string sURL = "frmDescargaTmpFile.aspx";
                string datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
                datosaencriptar = datosaencriptar + "solID=" + this.gridSolicitudes.DataKeys[e.Row.RowIndex]["solicitudID"].ToString() + "&";
                datosaencriptar += "cartaCompromiso=1&";
                string URLcomplete = sURL + "?data=";
                URLcomplete += Utils.encriptacadena(datosaencriptar);
                JSUtils.OpenNewWindowOnClick(ref LB, URLcomplete, "CartaCompromiso", true);
                LB.Text = "CARTA COMPROMISO";
            }


            LB = (LinkButton)e.Row.FindControl("LBEvaluacion");
            if(LB!=null)
            {
                string sFileName = "EVALUACION.pdf";
                sFileName = sFileName.Replace(" ", "_");
                string sURL = "frmDescargaTmpFile.aspx";
                string datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
                datosaencriptar = datosaencriptar + "solID=" + this.gridSolicitudes.DataKeys[e.Row.RowIndex]["solicitudID"].ToString() + "&";
                datosaencriptar += "evaluacion=1&";
                string URLcomplete = sURL + "?data=";
                URLcomplete += Utils.encriptacadena(datosaencriptar);
                JSUtils.OpenNewWindowOnClick(ref LB, URLcomplete, "evaluacion", true);
                LB.Text = "EVALUACION";
            }  



            
            
        }

        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdUp = new SqlCommand();
            cmdUp.Connection = sqlcon;

            string queryUpSolicitud="update Solicitudes set statusID = @statusID Where solicitudID = @solicitudID";
            string queryupCredito="UPDATE  Creditos SET statusID = @statusID FROM  Solicitudes INNER JOIN Creditos ON ";
            queryupCredito += " Solicitudes.creditoID = Creditos.creditoID WHERE  (Solicitudes.solicitudID = @solicitudID)";
            if (this.IsSistemBanco)
            {
                queryUpSolicitud = dbFunctions.UpdateSDSForSisBanco(queryUpSolicitud);
                queryupCredito = dbFunctions.UpdateSDSForSisBanco(queryupCredito);
            }
         
           foreach (GridViewRow row in this.gridSolicitudes.Rows)
           {
                  try{
                       DropDownList ddlStatus = (DropDownList)(row.FindControl("drpdlStatus"));
                       if(ddlStatus!=null)
                       {
                         
                           if(ddlStatus.SelectedItem.Text != row.Cells[11].Text)
                           {
                               sqlcon.Open();
                               cmdUp.CommandText = queryUpSolicitud;
                               cmdUp.Parameters.Clear();
                               cmdUp.Parameters.Add("statusID", SqlDbType.Int).Value = int.Parse(ddlStatus.SelectedValue);
                               cmdUp.Parameters.Add("solicitudID", SqlDbType.Int).Value = int.Parse(row.Cells[1].Text);
                               cmdUp.ExecuteNonQuery();
                               cmdUp.CommandText = queryupCredito;
                               cmdUp.Parameters.Clear();
                               cmdUp.Parameters.Add("statusID", SqlDbType.Int).Value = int.Parse(ddlStatus.SelectedValue);
                               cmdUp.Parameters.Add("solicitudID", SqlDbType.Int).Value = int.Parse(row.Cells[1].Text);
                               cmdUp.ExecuteNonQuery();
                               this.gridSolicitudes.DataBind();
                               Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.UPDATE, this.UserID, "ACTUALIZÓ EL STATUS DE LA SOLICITUD: " + this.gridSolicitudes.DataKeys[row.RowIndex][0].ToString() + " AL ESTADO DE: " + ddlStatus.SelectedItem.Text);
                              

                           }
                       }
                   }
                   catch(Exception ex)
                   {
                       Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR ACTUALIZANDO STATUS DE SOLICITUD:  " + this.gridSolicitudes.DataKeys[row.RowIndex][0].ToString() + ". " + ex.Message, this.Request.Url.ToString());
                   }
               
           }
           
        }

        protected void Eliminar(Boolean activaacgregar)
        {

            this.btnEliminar.Visible = activaacgregar;
            if (this.gridSolicitudes.SelectedIndex == -1)
            {
                this.btnEliminar.Visible = false;
            }
        }


        protected void btnEliminar_Click1(object sender, EventArgs e)
        {
            string qryDel = "DELETE FROM Solicitudes WHERE solicitudID = @solicitudID";
            if (this.IsSistemBanco)
                qryDel = dbFunctions.UpdateSDSForSisBanco(qryDel);
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdDel = new SqlCommand(qryDel, conGaribay);
            string solicitudID = this.gridSolicitudes.SelectedDataKey["solicitudID"].ToString();
            try
            {
                cmdDel.Parameters.Add("@solicitudID", SqlDbType.Int).Value = int.Parse(this.gridSolicitudes.SelectedDataKey["solicitudID"].ToString());

                conGaribay.Open();
                //this.Eliminar(false, true);
                this.imagenmal.Visible = false;
                this.imagenbien.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("SOLICITUDDELETEDEXITO"), this.gridSolicitudes.SelectedDataKey["solicitudID"].ToString());
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                int numregistros = cmdDel.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("SOLICITUDEXECUTEFAILED"), "ELIMINADA", "ELIMINARON", numregistros.ToString()));
                }
                this.panelmensaje.Visible = true;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ LA SOLICITUD: " + solicitudID));

            }
            catch (InvalidOperationException err1)
            {
                
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("SOLICITUDDELETEDFAILED"), solicitudID);
                this.lblMensajeException.Text = err1.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());
                this.panelmensaje.Visible = true;
            }
            catch (SqlException err2)
            {
                
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("SOLICITUDDELETEDFAILED"), solicitudID);
                this.lblMensajeException.Text = err2.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());
                this.panelmensaje.Visible = true;
            }
            catch (Exception err3)
            {
                
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("SOLICITUDDELETEDFAILED"), solicitudID);
                this.lblMensajeException.Text = err3.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err3.Message, this.Request.Url.ToString());
                this.panelmensaje.Visible = true;
            }
            finally
            {
                conGaribay.Close();
                this.gridSolicitudes.DataBind();
                if (this.gridSolicitudes.Rows.Count < 1)
                {
                    this.btnEliminar.Visible = false;
                    //this.btnModificarDeLista.Visible = false;
                }
            }

        }

        protected void gridSolicitudes_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.gridSolicitudes.DataBind();
            dataSourceLista.FilterExpression = cadenaFiltros();
            this.btnImprimirContrato.Visible = true;
            this.btnImprimir.Visible = true;


            //JSUtils.OpenNewWindowOnClick(ref this.btnImprimir, URLcomplete, "Solicitud ", true);
            //             sFileName = "CONTRATO_SOLICITUD.pdf";
            //             sFileName = sFileName.Replace(" ", "_");
            //             sURL = "frmDescargaTmpFile.aspx";
            //             datosaencriptar = "archivo=";
            //             datosaencriptar += FormatosPdf.imprimeContrato(int.Parse(this.gridSolicitudes.SelectedDataKey["solicitudID"].ToString()),this.UserID);
            //             datosaencriptar += "&filename=" + sFileName + "&ContentType=application/pdf&";
            //             URLcomplete = sURL + "?data=";
            //             URLcomplete += Utils.encriptacadena(datosaencriptar);
            //             JSUtils.OpenNewWindowOnClick(ref this.btnImprimirContrato, URLcomplete, "Contrato_Solicitud ", true);
            //             sFileName = "CARATULA_ANEXA.pdf";
            //             sFileName = sFileName.Replace(" ", "_");
            //             sURL = "frmDescargaTmpFile.aspx";
            //             datosaencriptar = "archivo=";
            //             datosaencriptar += FormatosPdf.imprimeCaratulaAnexa(int.Parse(this.gridSolicitudes.SelectedDataKey["solicitudID"].ToString()), this.UserID);
            //             datosaencriptar += "&filename=" + sFileName + "&ContentType=application/pdf&";
            //             URLcomplete = sURL + "?data=";
            //             URLcomplete += Utils.encriptacadena(datosaencriptar);
            //             JSUtils.OpenNewWindowOnClick(ref this.btnImprimirContrato, URLcomplete, "Caratula_Anexa ", true);
            this.Eliminar(true);
            string msgDel = "return confirm('¿Realmente desea eliminar la solicitud: ";
            msgDel += this.gridSolicitudes.SelectedDataKey["solicitudID"].ToString();
            msgDel += "?')";
            this.btnEliminar.Attributes.Add("onclick", msgDel);
        }

        

    }
}
