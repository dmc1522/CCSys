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
using System.IO;



namespace Garibay
{
    public partial class frmListDeleteProductores : Garibay.BasePage
    {
        String idtomodify;//, idtodelete;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.btnAgregar.Visible = true;
                this.btnModificar.Visible = false;
                this.btnEliminar.Visible = false;
                this.btnVerEstadodeCuenta.Visible = false;
                this.panelmensaje.Visible = false;
                this.showHideColumns();
                try
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PRODUCTORES, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "EL USUARIO VISITÓ LA PÁGINA LISTA DE PRODUCTORES");
                }
                catch (Exception exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                }



            }
            if(this.panelmensaje.Visible==true){
                this.panelmensaje.Visible = false;
            }
            this.gridProductores.DataSourceID = "SqlDataSource1";
            this.compruebasecurityLevel();


            

        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.btnAgregar.Visible = false;
                this.btnModificar.Visible = false;
                this.btnEliminar.Visible = false;
                this.gridProductores.Columns[0].Visible = false;
               
            }

        }

        protected void btnVerEstadodeCuenta_Click(object sender, EventArgs e)
        {
            this.Server.Transfer("~/frmEstadodeCuentadeProductor.aspx");

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            this.Server.Transfer("~/frmAddModifyProductores.aspx");
            
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            this.gridProductores.DataBind();
            if (this.gridProductores.SelectedIndex > -1)
            {
                idtomodify = this.gridProductores.Rows[this.gridProductores.SelectedIndex].Cells[1].Text;
                string strRedirect = "~/frmAddModifyProductores.aspx";
                string datosaencriptar;
                datosaencriptar = "idtomodify=";
                datosaencriptar += idtomodify;
                datosaencriptar += "&";
                strRedirect += "?data=";
                strRedirect += Utils.encriptacadena(datosaencriptar);
                Response.Redirect(strRedirect, true);
            }
            else{
                return;
            }
            

        }

        protected void gridProductores_SelectedIndexChanged(object sender, EventArgs e)
        {
     
            if (this.gridProductores.SelectedDataKey[0] != null){
                this.btnModificar.Visible = true;
                this.btnEliminar.Visible = true;
                this.btnVerEstadodeCuenta.Visible = true;
                string msgDel = "return confirm('¿Realmente desea eliminar productor: ";
                msgDel += this.gridProductores.SelectedDataKey[1].ToString().ToUpper();
                msgDel += " ";
                msgDel += this.gridProductores.SelectedDataKey[2].ToString().ToUpper();
                msgDel += " ";
                msgDel += this.gridProductores.SelectedDataKey[3].ToString().ToUpper();
                msgDel += "?')";
                btnEliminar.Attributes.Add("onclick", msgDel); 
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            
            if (this.gridProductores.SelectedDataKey[0] != null)
            {
                
                string qryDel = "DELETE FROM Productores WHERE productorID=@productorID";
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdDel = new SqlCommand(qryDel, conGaribay);
                this.gridProductores.DataBind();
                try
                {
                    cmdDel.Parameters.Add("@productorID", SqlDbType.Int).Value = int.Parse(this.gridProductores.SelectedDataKey[0].ToString());
                    conGaribay.Open();
                    int numregistros = cmdDel.ExecuteNonQuery();

                    if (numregistros != 1)
                    {
                        throw new Exception(string.Format(myConfig.StrFromMessages("PRODUCTOREXECUTEFAILED"), "ELIMINADO", "ELIMINARON", numregistros.ToString()));
                    }
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PRODUCTORES, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ EL PRODUCTOR: " + this.gridProductores.SelectedDataKey[1].ToString().ToUpper() + " " +  this.gridProductores.SelectedDataKey[2].ToString().ToUpper() + " " + this.gridProductores.SelectedDataKey[3].ToString().ToUpper()));
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTORDELETEDEXITO"), this.gridProductores.SelectedDataKey[1].ToString().ToUpper(), this.gridProductores.SelectedDataKey[2].ToString().ToUpper(), this.gridProductores.SelectedDataKey[3].ToString().ToUpper());
                    this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION      
                    this.gridProductores.SelectedIndex = -1;
                    this.btnVerEstadodeCuenta.Visible = false;
                    this.btnModificar.Visible = false;
                    btnEliminar.Visible = false;
                    this.imagenmal.Visible = false;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = true;
                    this.gridProductores.DataBind();
                }
                catch (InvalidOperationException exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTORDELETEDFAILED"), this.gridProductores.SelectedDataKey[1].ToString().ToUpper(), this.gridProductores.SelectedDataKey[2].ToString().ToUpper(), this.gridProductores.SelectedDataKey[3].ToString().ToUpper());
                    this.lblMensajeException.Text = exception.Message; 
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;
                }
                catch (SqlException exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTORDELETEDFAILED"), this.gridProductores.SelectedDataKey[1].ToString().ToUpper(), this.gridProductores.SelectedDataKey[2].ToString().ToUpper(), this.gridProductores.SelectedDataKey[3].ToString().ToUpper());
                    this.lblMensajeException.Text = exception.Message; 
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;

                }
                catch (Exception exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTORDELETEDFAILED"), this.gridProductores.SelectedDataKey[1].ToString().ToUpper(), this.gridProductores.SelectedDataKey[2].ToString().ToUpper(), this.gridProductores.SelectedDataKey[3].ToString().ToUpper());
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

        protected void btnPrintList_Click(object sender, EventArgs e)
        {
           
            this.gridProductores.DataBind();
            float[] anchodecolumnas = new float[] { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3 };
            String pathArchivotemp= PdfCreator.printGridView("LISTA DE PRODUCTORES", gridProductores, anchodecolumnas, PdfCreator.orientacionPapel.HORIZONTAL, PdfCreator.tamañoPapel.OFICIO);
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=listaproductores.pdf");
            Response.WriteFile(pathArchivotemp);
            Response.Flush();
            Response.End();
            try
            {
                File.Delete(pathArchivotemp);
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "ERROR BORRANDO ARCHIVO TEMP DE LIST DE CHEQUES", ref ex);
            }


        }

        private void showHideColumns()
        {
            foreach (DataControlField col in this.gridProductores.Columns)
            {
                ListItem item = this.cblColToShow.Items.FindByText(col.HeaderText);
                if (item != null)
                {
                    col.Visible = item.Selected;
                }
            }
        }

        protected void btnActualizaColumna_Click(object sender, EventArgs e)
        {
            this.showHideColumns();
        }

        protected void btnLimpiaFiltros_Click(object sender, EventArgs e)
        {
            foreach(ListItem item in this.cblColToShow.Items)
            {
                item.Selected = false;
            }
            this.showHideColumns();
        }

        protected void btnSeleccionaTodas_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in this.cblColToShow.Items)
            {
                item.Selected = true;
            }
            this.showHideColumns();
        }

        protected void btnExportarAExcel_Click(object sender, EventArgs e)
        {
            string sFileName = "ListadeProductores" + Utils.Now.ToString("dd-MM-yyyy") + ".xls";
            ExportToExcel(sFileName, ref this.gridProductores);
        }

        private void ExportToExcel(string strFileName, ref GridView dg)
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
            dg.EnableViewState = false;
            dg.Columns[0].Visible = false;
            dg.DataBind();
            Page pPage = new Page();
            HtmlForm fForm = new HtmlForm();
            Label lbltemp = new Label();
            pPage.SkinID = "skinverde";
            string sHeader = "<table><tr><td colspan='4' class='TableHeader'><b>REPORTE DE PRODUCTORES</b></td></tr></table>";
            lbltemp.Text = sHeader;
            pPage.Controls.Add(lbltemp);
            pPage.Controls.Add(fForm);
            fForm.Attributes.Add("runat", "server");
            //this.gridMovCuentasBanco.RenderControl(oHtmlTextWriter);
            dg.BorderWidth = 1;
            fForm.Controls.Add(dg);
            pPage.EnableTheming = true;
            pPage.DataBind();
            pPage.RenderControl(oHtmlTextWriter);
            dg.Columns[0].Visible = true;
            dg.AllowPaging = true;
            dg.AllowSorting = true;
            dg.BorderWidth = 0;
            Response.Write(oStringWriter.ToString());
            Response.End();
        }

        protected void gridProductores_DataBound(object sender, EventArgs e)
        {
            
        }

        protected void gridProductores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lnk = (HyperLink)e.Row.FindControl("lnkEditar");
                if (lnk != null)
                {
                    lnk.NavigateUrl = "~/frmAddModifyProductores.aspx" + Utils.GetEncriptedQueryString("idtomodify=" + this.gridProductores.DataKeys[e.Row.RowIndex]["productorID"].ToString());
                }
            }
        }
      
      
    }
}
