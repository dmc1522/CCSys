using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.IO;


namespace Garibay
{
    public partial class ListCreditos : BasePage
    {
        string idtomodify;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsSistemBanco)
            {
                this.SqlDataSource1.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.SqlDataSource1.SelectCommand);
            }
            if(!this.IsPostBack){
                this.gridCreditos.SelectedIndex = -1;
                this.panelMensaje.Visible = false;
                this.btnExport.Visible = false;
                this.cmbCiclo.DataBind();
                this.gridCreditos.DataBind();
                this.cmbProductor.DataBind();
            
                this.showHideColumns();
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CREDITOS, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "SE VISITÓ LA LISTA DE CREDITOS");
            }
            if(this.panelMensaje.Visible){
                this.panelMensaje.Visible = false;
                this.panelagregar.Visible = true;
            }
            
            this.compruebasecurityLevel();
            this.Eliminar(true);

        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.btnAgregarNuevo.Visible = false;
                //this.btnModificar.Visible = false;
                this.gridCreditos.Columns[13].Visible = false;
               

            }

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
           
            if (this.gridCreditos.SelectedIndex > -1)
            {
                idtomodify = this.gridCreditos.SelectedDataKey["creditoID"].ToString();
                string strRedirect = "~/frmAddModifyCreditos.aspx";
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

        protected void gridCreditos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
          //  this.gridCreditos.PageSize = int.Parse(this.ddlElemXPage.Text);
            string sFiltros = " ";
            bool bAndReq = false;
            
            if (this.chkProductor.Checked)
            {
                sFiltros = " productorID = ";
                sFiltros += this.cmbProductor.SelectedValue;
                bAndReq = true;

              
            }
            if (this.chkStatus.Checked)
            {
                sFiltros += bAndReq ? " AND " : "" ;
                sFiltros += " statusID = ";
                sFiltros += this.cmbEstado.SelectedValue;
            }

            this.SqlDataSource1.FilterExpression = sFiltros;
            this.gridCreditos.PageIndex = 0;
            this.showHideColumns();
        }
        private void showHideColumns()
        {
            foreach (DataControlField col in this.gridCreditos.Columns)
            {
                ListItem item = this.cblColToShow.Items.FindByText(col.HeaderText);
                if (item != null)
                {
                    col.Visible = item.Selected;
                }
            }
        }

        protected void btnAgregarNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmAddModifyCreditos.aspx", true);
        }

        protected void btnAceptarMensaje_Click(object sender, EventArgs e)
        {

        }

        protected void gridCreditos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink link = (HyperLink)e.Row.Cells[13].FindControl("HyperLink1");
                if (link != null)
                {
                    link.Text = "Abrir";
                    String sQuery = "idtomodify=" + this.gridCreditos.DataKeys[e.Row.RowIndex]["creditoID"].ToString();
                    sQuery = Utils.GetEncriptedQueryString(sQuery);
                    String strRedirect = "~/frmAddModifyCreditos.aspx";
                    strRedirect += sQuery;
                    link.NavigateUrl = strRedirect;
                }
                link = (HyperLink)e.Row.Cells[13].FindControl("HPEstadodeCuenta");
                /*
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                            string sql = "SELECT Distinct Creditos.creditoID  FROM Creditos INNER JOIN Productores ON Creditos.productorID = Productores.productorID INNER JOIN Notasdeventa ON Creditos.creditoID = Notasdeventa.creditoID where Creditos.creditoID =@creditoID";
                            SqlCommand cmd = new SqlCommand(sql,conGaribay);*/

                try
                {
                    /*
                    conGaribay.Open();
                                    cmd.Parameters.Add("@creditoID", SqlDbType.Int).Value = int.Parse(this.gridCreditos.DataKeys[e.Row.RowIndex]["creditoID"].ToString());
                                    object ob = cmd.ExecuteScalar();
                                    if(ob!=null)
                                    {*/

                    link.Text = "Abrir";
                    String sQuery = "creditoID=" + this.gridCreditos.DataKeys[e.Row.RowIndex]["creditoID"].ToString();
                    sQuery = Utils.GetEncriptedQueryString(sQuery);
                    String strRedirect = "~/frmEstadodeCuentaCredito.aspx";
                    strRedirect += sQuery;
                    link.NavigateUrl = strRedirect;

                    /*
                    }
                                    else
                                    {
                                        link.Visible = false;
                                    }*/


                }
                catch 
                {

                }
            }
            

        }

      
        private String ExportToExcel(GridView dg)
        {
            String sFilename = "";
            sFilename = Path.GetTempFileName();
            StreamWriter sw = new StreamWriter(sFilename);
            try
            {
                this.EnableViewState = false;
                System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
                string sHeader = "<table><tr><td><b>";
                sHeader += "REPORTE DE CRÉDITOS</b></td></tr>";
                sHeader += "</table>";
                dg.AllowPaging = false; //all the movements in one page.
                dg.Columns[13].Visible = false; //hide the select button
                dg.DataBind();
                Page pPage = new Page();
                pPage.EnableViewState = false;
                pPage.EnableEventValidation = false;
                pPage.DesignerInitialize();


                HtmlForm fForm = new HtmlForm();
                //dg.EnableViewState = false;
                pPage.Controls.Add(fForm);
                Label lblHeader = new Label();
                lblHeader.Text = sHeader;
                //this.gridMovCuentasBanco.RenderControl(oHtmlTextWriter);
                fForm.Controls.Add(lblHeader);
                dg.GridLines = GridLines.Both;
                fForm.Controls.Add(dg);
                pPage.RenderControl(oHtmlTextWriter);
                //dg.AllowPaging = true;
                dg.Columns[13].Visible = true;
                dg.GridLines = GridLines.None;
                //dg.EnableViewState = true;
                sw.Write(oStringWriter.ToString());
                sw.Close();
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CREDITOS, Logger.typeUserActions.PRINT, int.Parse(this.Session["USERID"].ToString()), "SE EXPORTÓ A EXCEL EL CREDITO ");
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.PRINT, "ExportToExcel", ref ex);
            }
            return sFilename;
        }


        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {

                string sFileName = "REPORTE DE CREDITOS.xls";
                sFileName = sFileName.Replace(" ", "_");
                String sPath = "";
                this.gridCreditos.DataBind();
                Control ctrl = this.gridCreditos.Parent;
                sPath = ExportToExcel(this.gridCreditos);
                String sURL = "frmDescargaTmpFile.aspx";

                string datosaencriptar;
                datosaencriptar = "archivo=";
                datosaencriptar += sPath;
                datosaencriptar += "&filename=" + sFileName + "&ContentType=application/ms-excel&";

                String parameter = sURL + "?data=";
                parameter += Utils.encriptacadena(datosaencriptar);

                JSUtils.OpenNewWindowOnClick(ref this.btnExport, parameter, "Reporte de Créditos", true);
                this.btnGenerar.Visible = false;
                this.btnExport.Visible = true;
                ctrl.Controls.Add(this.gridCreditos);
                this.gridCreditos.DataBind();
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CREDITOS, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "SE GENERÓ EL REPORTE DE CRÉDITOS");
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.PRINT, "btnGeneraReporte_Click", ref ex);
            }
            

        }

        protected void Eliminar(Boolean activaacgregar)
        {

            this.btnEliminar.Visible = activaacgregar;
            if (this.gridCreditos.SelectedIndex == -1)
            {
                this.btnEliminar.Visible = false;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string qryDel = "DELETE FROM Creditos WHERE creditoID = @creditoID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdDel = new SqlCommand(qryDel, conGaribay);
            string creditoID = this.gridCreditos.SelectedDataKey["creditoID"].ToString();
            try
            {
                cmdDel.Parameters.Add("@creditoID", SqlDbType.Int).Value = int.Parse(creditoID);

                conGaribay.Open();
                //this.Eliminar(false, true);
                this.imagenmal.Visible = false;
                this.imagenbien.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CREDITODELETEDEXITO"), creditoID);
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                int numregistros = cmdDel.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("CREDITOEXECUTEFAILED"), "ELIMINADO", "ELIMINARON", numregistros.ToString()));
                }
                this.panelMensaje.Visible = true;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CREDITOS, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ EL CRÉDITO: " + creditoID));

            }
            catch (InvalidOperationException err1)
            {

                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CREDITODELETEDFAILED"), creditoID);
                this.lblMensajeException.Text = err1.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());
                this.panelMensaje.Visible = true;
            }
            catch (SqlException err2)
            {

                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CREDITODELETEDFAILED"),creditoID);
                this.lblMensajeException.Text = err2.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());
                this.panelMensaje.Visible = true;
            }
            catch (Exception err3)
            {

                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CREDITODELETEDFAILED"), creditoID);
                this.lblMensajeException.Text = err3.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err3.Message, this.Request.Url.ToString());
                this.panelMensaje.Visible = true;
            }
            finally
            {
                conGaribay.Close();
                this.gridCreditos.DataBind();
                if (this.gridCreditos.Rows.Count < 1)
                {
                    this.btnEliminar.Visible = false;
                    //this.btnModificarDeLista.Visible = false;
                }
            }
        }

        protected void gridCreditos_SelectedIndexChanged1(object sender, EventArgs e)
        {
            this.Eliminar(true);
        }

        


    }
}
