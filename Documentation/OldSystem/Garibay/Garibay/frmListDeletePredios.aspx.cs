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
    public partial class frmListDeletePredios : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.compruebasecurityLevel();
            if(!this.IsPostBack)
            {
                this.showHideColumns();
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PREDIOS, Logger.typeUserActions.SELECT, int.Parse(Session["USERID"].ToString()), "VISITÓ LA PAGINA DE LISTA DE PREDIOS");
                this.btnAgregarDeLista.Visible = true;
               
                this.panelmensaje.Visible = false;
            }
            this.panelmensaje.Visible = false;
            this.gridPredios.DataSourceID = this.gridPredios.DataSourceID;
            
            
            

        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.btnAgregarDeLista.Visible = false;
             
                this.gridPredios.Columns[0].Visible = false;
            }

        }

        protected void btnAgregarDeLista_Click(object sender, EventArgs e)
        {
            this.Server.Transfer("~/frmAddModifyPredios.aspx");
        }

        protected void btnModificarDeLista_Click(object sender, EventArgs e)
        {
            if (this.gridPredios.SelectedIndex > -1)
            {
                String idtomodify = this.gridPredios.SelectedDataKey[0].ToString();
                string strRedirect = "~/frmAddModifyPredios.aspx";
                string datosaencriptar;
                datosaencriptar = "predioID=";
                datosaencriptar += idtomodify;
                datosaencriptar += "&";
                strRedirect += "?data=";
                strRedirect += Utils.encriptacadena(datosaencriptar);
                Response.Redirect(strRedirect, true);
            }
        }

        protected void btnMostrarColumnas_Click(object sender, EventArgs e)
        {
            this.showHideColumns();
            int cantXPage = -1;
            if (int.TryParse(this.ddlCantXPage.SelectedValue, out cantXPage))
            {
                this.gridPredios.AllowPaging = true;
                this.gridPredios.PageSize = cantXPage;
            }
            else
            {
                this.gridPredios.AllowPaging = false;
            }
        }
        private void showHideColumns()
        {
            foreach (DataControlField col in this.gridPredios.Columns)
            {
                ListItem item = this.cblCampos.Items.FindByText(col.HeaderText);
                if (item != null)
                {
                    col.Visible = item.Selected;
                }
            }
        }

        protected void btnExportarAExcel_Click(object sender, EventArgs e)
        {
            string sFileName = "ListaDePredios" + Utils.Now.ToString("dd-MM-yyyy") + ".xls";
            ExportToExcel(sFileName, this.gridPredios);
        }
        private void ExportToExcel(string strFileName, GridView dg)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", strFileName));
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            dg.AllowPaging = false; //all the movements in one page.
            dg.AllowSorting = false;
            dg.BorderWidth = 2;
            dg.DataBind();
            Page pPage = new Page();
            HtmlForm fForm = new HtmlForm();
            dg.EnableViewState = false;
            pPage.Controls.Add(fForm);
            Label lblHeader = new Label();
            lblHeader.Text = "<table><tr><td colspan='4'><H2>LISTA DE PREDIOS</H2></td></tr></table>";
            
            fForm.Controls.Add(lblHeader);
            dg.Columns[22].Visible = false;
            dg.Columns[23].Visible = false;
            fForm.Controls.Add(dg);
            pPage.RenderControl(oHtmlTextWriter);
            dg.Columns[22].Visible = true;
            dg.Columns[23].Visible = true;
            dg.AllowPaging = true;
            dg.AllowSorting = true;
            Response.Write(oStringWriter.ToString());
            Response.End();
        }

        protected void gridPredios_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (this.gridPredios.SelectedDataKey[0] != null)
            {

                string qryDel = "DELETE FROM Predios WHERE predioID=@predioID";
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdDel = new SqlCommand(qryDel, conGaribay);
                this.gridPredios.DataBind();
                try
                {
                    cmdDel.Parameters.Add("@predioID", SqlDbType.Int).Value = int.Parse(this.gridPredios.SelectedDataKey[0].ToString());
                    conGaribay.Open();
                    int numregistros = cmdDel.ExecuteNonQuery();

                    if (numregistros != 1)
                    {
                        throw new Exception("NO SE PUDO ELIMINAR EL PREDIO : " + int.Parse(this.gridPredios.SelectedDataKey[0].ToString()));
                    }
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PREDIOS, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ EL PREDIO: " + this.gridPredios.SelectedDataKey[0].ToString().ToUpper() ));
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                    this.lblMensajeOperationresult.Text = string.Format("EL PREDIO " + this.gridPredios.SelectedDataKey[0].ToString().ToUpper() + " HA SIDO ELIMINADO EXITOSAMENTE");
                    this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION      
                    this.gridPredios.SelectedIndex = -1;
                 //   this.btnModificarDeLista.Visible = false;
                //    btnEliminar.Visible = false;
                    this.imagenmal.Visible = false;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = true;
                    this.gridPredios.DataBind();
                }
                catch (SqlException exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), "SQL Exception :" + exception.Message, this.Request.Url.ToString());
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = "EL PREDIO " + this.gridPredios.SelectedDataKey[0].ToString().ToUpper() + " NO PUDO SER ELIMINADO, ERROR EN SQL";
                    this.lblMensajeException.Text = exception.Message;
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;

                }
                catch (Exception exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), "Exception :" + exception.Message, this.Request.Url.ToString());
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = "EL PREDIO " + this.gridPredios.SelectedDataKey[0].ToString().ToUpper() + " NO PUDO SER ELIMINADO, GENERÓ UNA EXCEPCIÓN";
                    this.lblMensajeException.Text = exception.Message;
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;

                }
                finally
                {
                    conGaribay.Close();
                }




            }
            else
            {
                return;
            }
        }

        protected void gridPredios_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void gridPredios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }

            HyperLink link = (HyperLink)e.Row.FindControl("linkOpenPredio");
            if (link != null)
            {
                String sQuery = "predioID=" + this.gridPredios.DataKeys[e.Row.RowIndex]["predioID"].ToString();
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = "~/frmAddModifyPredios.aspx";
                strRedirect += sQuery;
                link.NavigateUrl = strRedirect;
            }
            Button bt = (Button)e.Row.Cells[22].FindControl("btnDeletePredio");
            if (bt != null)
            {
                bt.CommandArgument = this.gridPredios.DataKeys[e.Row.RowIndex]["predioID"].ToString();
                string msgDel = "return confirm('¿Realmente desea eliminar el predio con el folio: ";
                msgDel += this.gridPredios.DataKeys[e.Row.RowIndex]["folioPredio"].ToString().ToUpper();
                msgDel += "?')";
                bt.Attributes.Add("onclick", msgDel);
                
            }
        }

        protected void gridPredios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(((Button)(e.CommandSource)).Text=="Eliminar"){
               

                    string qryDel = "DELETE FROM Predios WHERE predioID=@predioID";
                    SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                    SqlCommand cmdDel = new SqlCommand(qryDel, conGaribay);
                    this.gridPredios.DataBind();
                    int predID = int.Parse(e.CommandArgument.ToString());
                  
                    try
                    {
                       cmdDel.Parameters.Add("@predioID", SqlDbType.Int).Value = predID;
                        conGaribay.Open();
                        int numregistros = cmdDel.ExecuteNonQuery();

                        if (numregistros != 1)
                        {
                            throw new Exception("NO SE PUDO ELIMINAR EL PREDIO : " + predID.ToString());
                        }
                        Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PREDIOS, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ EL PREDIO: " + predID.ToString().ToUpper()));
                        this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                        this.lblMensajeOperationresult.Text = string.Format("EL PREDIO " + predID.ToString().ToUpper() + " HA SIDO ELIMINADO EXITOSAMENTE");
                        this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION      
                        this.gridPredios.SelectedIndex = -1;
                       
                        this.imagenmal.Visible = false;
                        this.panelmensaje.Visible = true;
                        this.imagenbien.Visible = true;
                        this.gridPredios.DataBind();
                    }
                  
                    catch (Exception exception)
                    {
                        Logger.Instance.LogException(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, "EXCEPCION AL BORRAR PREDIO", this.Request.Url.ToString(), ref exception);
                        this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                        this.lblMensajeOperationresult.Text = "EL PREDIO " + predID.ToString().ToUpper() + " NO PUDO SER ELIMINADO, GENERÓ UNA EXCEPCIÓN";
                        this.lblMensajeException.Text = exception.Message;
                        this.imagenmal.Visible = true;
                        this.panelmensaje.Visible = true;
                        this.imagenbien.Visible = false;

                    }
                    finally
                    {
                        conGaribay.Close();
                    }




               
               
            }
        }
    }
}
