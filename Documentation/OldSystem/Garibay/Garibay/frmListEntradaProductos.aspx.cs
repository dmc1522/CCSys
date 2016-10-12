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
using System.IO;

namespace Garibay
{
    public partial class WebForm6 : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.btnAgregar.Visible = true;
                this.btnModificar.Visible = false;
                this.btnEliminar.Visible = false;
                this.panelmensaje.Visible = false;
                
                try
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.ENTRADAPRODUCTOS, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "EL USUARIO VISITÓ LA PÁGINA LISTA DE PRODUCTORES");
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
            this.grdvListEntPro.DataSourceID = "SqlDataSource2";
            this.compruebasecurityLevel();

          
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.btnAgregar.Visible = false;
                this.btnModificar.Visible = false;
                this.btnEliminar.Visible = false;
                this.grdvListEntPro.Columns[0].Visible = false;
            }

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            this.Server.Transfer("~/frmAddModifyEntradaProductos.aspx");
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string strRedirect = "~/frmAddModifyEntradaProductos.aspx";
            string datosaencriptar;
            datosaencriptar = "idtomodify=";
            datosaencriptar += this.grdvListEntPro.SelectedDataKey["entradaprodID"].ToString();
            datosaencriptar += "&producto=" + this.grdvListEntPro.SelectedDataKey["Nombre"].ToString()+"&";
            strRedirect += "?data=";
            strRedirect += Utils.encriptacadena(datosaencriptar);
            Response.Redirect(strRedirect, true); 
        }

        protected void click_selectionChanged(object sender, EventArgs e)
        {
            SqlDataSource2.FilterExpression = cadenaFiltros();
            this.txtFechainicio.Text = PopCalendar1.SelectedDate;

        }

        protected void click_selectionChanged1(object sender, EventArgs e)
        {
            SqlDataSource2.FilterExpression = cadenaFiltros();
            this.txtFechafin.Text = PopCalendar2.SelectedDate;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string sError="";
          if(dbFunctions.deleteEntradaPro(int.Parse(grdvListEntPro.SelectedDataKey["entradaprodID"].ToString()),int.Parse(this.Session["USERID"].ToString()), ref sError)){
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PRODUCTORES, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ LA ENTRADA DEL PRODUCTO: " + this.grdvListEntPro.SelectedDataKey["Nombre"].ToString().ToUpper()));
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("ENTRADAPRODUCTOSDELETEDEXITO"), this.grdvListEntPro.SelectedDataKey["Nombre"].ToString().ToUpper());
                this.lblMensajeException.Text = "";
                this.btnModificar.Visible = false;
                btnEliminar.Visible = false;
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                this.grdvListEntPro.SelectedIndex = -1;
          }
          else
          {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), sError, this.Request.Url.ToString());
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("ENTRADAPRODUCTOSDELETEDFAILED"), this.grdvListEntPro.SelectedDataKey["Nombre"].ToString().ToUpper());
                this.lblMensajeException.Text = sError;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;
          }
          


            
        }

        protected void grdvListEntPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnEliminar.Visible = true;
            this.btnModificar.Visible = true;
            this.btnEliminar.Attributes.Add("onclick", "return confirm('¿Desea eliminar el la entrada del producto " + this.grdvListEntPro.SelectedDataKey["Nombre"].ToString() + "?')");
            if (this.panelmensaje.Visible == true)
            {
                this.panelmensaje.Visible = false;
            }
        }

        

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            this.drpdlProducto.SelectedIndex = 0;
            this.drpdlbodega.SelectedIndex = 0;
            this.drpdlTipoMovimientoP.SelectedIndex =0;
            this.txtFechafin.Text = "";
            this.txtFechainicio.Text = "";

        }

        protected void btnfiltrar_Click(object sender, EventArgs e)
        {
            
            
            if (!filtrar())
            {
                this.grdvListEntPro.DataSourceID = "SqlDataSource2";
            }
            else
            {
                SqlDataSource2.FilterExpression = cadenaFiltros();
                this.grdvListEntPro.DataSourceID = "SqlDataSource2";
            }
                this.grdvListEntPro.PageIndex = 0;
                this.grdvListEntPro.PageSize = int.Parse(this.drpdlElemXpag.SelectedValue);
        }
        protected void drpdlProducto_DataBound(object sender, EventArgs e)
        {
            int newValue = 0;
            this.drpdlProducto.Items.Insert(0, new ListItem("TODOS", newValue.ToString()));
            this.drpdlProducto.SelectedIndex = 0;
            this.drpdlProducto.SelectedValue = newValue.ToString();

         }

      
        protected bool filtrar()
        {
            if (this.txtFechafin.Text == "" && this.txtFechainicio.Text == "" && this.drpdlProducto.SelectedItem.Text == "TODOS" && this.drpdlbodega.SelectedItem.Text == "TODOS" && this.drpdlTipoMovimientoP.SelectedItem.Text == "TODOS" && this.ddlGrupo.SelectedItem.Text == "TODOS")
            {
                return false;
            }
            return true;
        }
        protected string cadenaFiltros()
        {
            string filtros = "";
            bool masdeuno = false;
            if (this.drpdlProducto.SelectedItem.Text != "TODOS")
            {
                filtros += "productoId = " + this.drpdlProducto.SelectedValue;
                masdeuno = true;

            }
            if (this.drpdlbodega.SelectedItem.Text != "TODOS")
            {
                if (masdeuno)
                {
                    filtros += " AND ";
                }
                filtros += "bodega = '" + this.drpdlbodega.SelectedItem.Text + "'";
                masdeuno = true;

            }
            if (this.drpdlTipoMovimientoP.SelectedItem.Text != "TODOS")
            {
                if (masdeuno)
                {
                    filtros += " AND ";
                }
                filtros += "tipoMovimiento = '" + this.drpdlTipoMovimientoP.SelectedItem.Text + "'";
                masdeuno = true;

            }
            if (this.txtFechainicio.Text != "")
            {
                if (masdeuno)
                {
                    filtros += " AND ";
                }

                filtros += "Fecha >='" + Utils.converttoLongDBFormat(txtFechainicio.Text) + "'";

                masdeuno = true;


            }
            if (this.txtFechafin.Text != "")
            {
                if (masdeuno)
                {
                    filtros += " AND ";
                }
                filtros += "Fecha <='" + Utils.converttoLongDBFormat(this.txtFechafin.Text) + "'";


            }
            if (this.ddlGrupo.SelectedItem.Text != "TODOS")
            {
                if (masdeuno)
                {
                    filtros += " AND ";
                }
                filtros += "grupoId = " + this.ddlGrupo.SelectedValue;

            }
            return filtros;
        }

        protected void drpdlbodega_DataBound(object sender, EventArgs e)
        {int newvalue=0;
            this.drpdlbodega.Items.Insert(0, new ListItem("TODOS", newvalue.ToString()));
            this.drpdlbodega.SelectedIndex =0;
            this.drpdlbodega.SelectedValue = newvalue.ToString();
        }

        protected void drpdlTipoMovimientoP_DataBound(object sender, EventArgs e)
        {
            int newvalue = 0;
            this.drpdlTipoMovimientoP.Items.Insert(0, new ListItem("TODOS", newvalue.ToString()));
            this.drpdlTipoMovimientoP.SelectedIndex = 0;
            this.drpdlTipoMovimientoP.SelectedValue = newvalue.ToString();
        }

        protected void btnImprimeList_Click(object sender, EventArgs e)
        {
            this.grdvListEntPro.AllowPaging = false;
            this.grdvListEntPro.DataBind();
            float[] anchodecolumnas = new float[] { 3, 10, 10, 12, 10,6,9,12,7,10, 10 };
            String pathArchivotemp;
            pathArchivotemp = PdfCreator.printGridView("LISTA DE ENTRADAS DE PRODUCTO", grdvListEntPro, anchodecolumnas, PdfCreator.orientacionPapel.HORIZONTAL, PdfCreator.tamañoPapel.CARTA);
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=listausuarios.pdf");
            Response.WriteFile(pathArchivotemp);

            Response.Flush();
            Response.End();
            this.grdvListEntPro.AllowPaging = true;
            try
            {
                File.Delete(pathArchivotemp);
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "ERROR BORRANDO ARCHIVO TEMP DE CHEQUES", ref ex);
            }

        }

        protected void btnExportaraExcel_Click(object sender, EventArgs e)
        {
            string sFileName = "ListaDeEntradadeProducto" + Utils.Now.ToString("dd-MM-yyyy") + ".xls";
            ExportToExcel(sFileName, this.grdvListEntPro);
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
            lblHeader.Text = "<table><tr><td colspan='4'><H2>LISTA DE ENTRADA DE PRODUCTO</H2></td></tr></table>";
            fForm.Attributes.Add("runat", "server");

            pPage.EnableEventValidation = false;
            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            pPage.DesignerInitialize(); 



            fForm.Controls.Add(lblHeader);
            //fForm.Controls.Add(this.pnlfiltros);
            fForm.Controls.Add(dg);
            pPage.RenderControl(oHtmlTextWriter);
            dg.AllowPaging = true;
            dg.AllowSorting = true;
            Response.Write(oStringWriter.ToString());
            Response.End();
        }

        protected void ddlGrupo_DataBound(object sender, EventArgs e)
        {
            int newvalue = 0;
            this.ddlGrupo.Items.Insert(0, new ListItem("TODOS", newvalue.ToString()));
            this.ddlGrupo.SelectedIndex = 0;
            this.ddlGrupo.SelectedValue = newvalue.ToString();

        }
 
    }
}
