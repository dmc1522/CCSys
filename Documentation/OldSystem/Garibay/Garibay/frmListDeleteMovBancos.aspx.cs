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
    public partial class frmListMovBancos : Garibay.BasePage
    {
        public frmListMovBancos():base()
        {
            this.hasCalendar = true;
        }
        dsMovBanco.dtMovBancoDataTable dtTeibol = new dsMovBanco.dtMovBancoDataTable();
        protected void rellenadt(DateTime fechainicio, DateTime fechafin){
            try
            {
               // this.ddlCuentas.DataBind();
                double fSaldoInicial = 0, fSaldoFinal = 0;
                 if (dbFunctions.fillDTMovBancos(int.Parse(this.ddlCuentas.SelectedValue),
                    fechainicio,
                    fechafin,
                    ref fSaldoInicial,
                    ref fSaldoFinal,
                    ref dtTeibol))
                {
                    this.gridMovCuentasBanco.DataSourceID = "";
                    this.gridMovCuentasBanco.DataSource = dtTeibol;
                    this.gridMovCuentasBanco.DataBind();
                    if (dtTeibol.Rows.Count == 0)
                    {
                        fSaldoFinal = 0.00F;
                        fSaldoInicial = 0.00F;

                    }
                    this.lblSaldofinal.Text = string.Format("{0:c}",fSaldoFinal);
                    this.lblSaldoinicial.Text = string.Format("{0:c}", fSaldoInicial);
                }
               
            }
            catch (Exception exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());

            }

        }
        private void reloadGridView()
       {
            DateTime dtInicio = DateTime.Parse(this.txtFecha1.Text, new CultureInfo("es-Mx"));
            DateTime dtFin = DateTime.Parse(this.txtFecha2.Text, new CultureInfo("es-Mx"));
            this.rellenadt(dtInicio, dtFin);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dtInicio = new DateTime();
            DateTime dtFin = new DateTime();
            if (!this.IsPostBack)
            {
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDEBANCO, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "VISITÓ LA PÁGINA LISTA DE MOVIMIENTOS DE CUENTAS DE BANCOS");
               this.btnEliminar.Visible = false;
               /*this.btnModificar.Visible = false;*/
               DateTime dtHoy = Utils.Now;
               dtInicio = new DateTime(dtHoy.Year, dtHoy.Month, 1);
               TimeSpan tsOneDay = new TimeSpan(1, 0, 0, 0);
               dtFin = Utils.Now + tsOneDay;
               /*
               TimeSpan tsDays = dtFin - dtInicio;
                              dtFin = new DateTime(dtHoy.Year,dtHoy.Month,tsDays.Days);*/
               
               this.txtFecha1.Text = dtInicio.ToString("dd/MM/yyyy");
               this.txtFecha2.Text = dtFin.ToString("dd/MM/yyyy");
               this.ddlCuentas.DataBind();
               this.showHideColumns();
            }
            this.reloadGridView();
           
            /*this.gridMovCuentasBanco.DataSourceID = "SqlDataSource1";*/

            if (this.panelMensaje.Visible == true) { this.panelMensaje.Visible = false; this.panelagregar.Visible = true; }
            
           
            if (this.gridMovCuentasBanco.SelectedIndex == -1) { this.btnEliminar.Visible = false; }
            else { this.btnEliminar.Visible = true; }
            
        }

        protected void btnAgregarNuevo_Click(object sender, EventArgs e)
        {
            this.panelMensaje.Visible = false;
            this.Server.Transfer("~/frmAddmovBancos.aspx");
        }

        protected void gridMovCuentasBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mensaje;
            mensaje = "return confirm('¿Desea eliminar el Movimiento de Cuenta número: " + this.gridMovCuentasBanco.SelectedDataKey["movbanID"].ToString().ToUpper() + "?. ";
            mensaje += "')";
            this.btnEliminar.Attributes.Add("onclick",mensaje);
            if (this.panelMensaje.Visible == true)
            {
                this.panelMensaje.Visible = false;
            }
//             this.gridMovCuentasBanco.DataSourceID = "";
//             this.gridMovCuentasBanco.DataSource = dtTeibol; 
            this.btnEliminar.Visible = true;
           // this.btnModificar.Visible = true;
            
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string tipo = "", cantidad = "0.00";
            string a;
            a = this.gridMovCuentasBanco.SelectedDataKey[2].ToString();
            if (int.Parse(this.gridMovCuentasBanco.SelectedDataKey[2].ToString()) != 0)
            {//ES ABONO
                tipo = "ABONO";
                cantidad = double.Parse(this.gridMovCuentasBanco.SelectedDataKey[2].ToString()).ToString();
            }
            else { // ES ABONO
                tipo = "CARGO";
                cantidad = double.Parse(this.gridMovCuentasBanco.SelectedDataKey[3].ToString()).ToString();
               
            }
            

                
            string sError="ERROR";
            int movID = int.Parse(this.gridMovCuentasBanco.SelectedDataKey[0].ToString());
            if (dbFunctions.deleteMovementdeBanco(movID,sError,int.Parse(this.Session["USERID"].ToString()),int.Parse(this.ddlCuentas.SelectedValue)))
            {
              this.panelMensaje.Visible=true;
              
              this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
              this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("MOVCUENTASDELETEDEXITO"), tipo, cantidad, this.ddlCuentas.Items[this.ddlCuentas.SelectedIndex].Text, movID.ToString());
              this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION
              this.ddlCuentas.DataBind();
              this.reloadGridView();
              this.gridMovCuentasBanco.SelectedIndex = -1;
              this.imagenbien.Visible = true;
              this.imagenmal.Visible = false;
              this.panelagregar.Visible = false;
              Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDEBANCO, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), this.lblMensajeOperationresult.Text);
              
            }
            else
            {
                this.panelMensaje.Visible = true;
                this.panelagregar.Visible = false;
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("MOVCUENTASDELETEDFAILED"), tipo, cantidad, this.ddlCuentas.Items[this.ddlCuentas.SelectedIndex].Text);
                this.lblMensajeException.Text = sError; //BORRAMOS PORQUE NO HAY EXcEPTION
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), sError, this.Request.Url.ToString());


            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.showHideColumns();
            this.reloadGridView();
        }

       /* protected void btnModificar_Click(object sender, EventArgs e)
        {
            this.gridMovCuentasBanco.DataBind();
            if (this.gridMovCuentasBanco.SelectedIndex > -1)
            {
                string strRedirect = "~/frmAddMovBancos.aspx";
                string datosaencriptar;
                datosaencriptar = "idtomodify=";
                datosaencriptar += this.gridMovCuentasBanco.SelectedDataKey["movbanID"].ToString();
                //datosaencriptar += "&";
                strRedirect += "?data=";
                strRedirect += Utils.encriptacadena(datosaencriptar);
                Response.Redirect(strRedirect, true);
            }
            else
            {
                return;
            }
        }*/
        private void showHideColumns()
        {
            foreach (DataControlField col in this.gridMovCuentasBanco.Columns)
            {
                ListItem item = this.cblColToShow.Items.FindByText(col.HeaderText);
                if (item != null)
                {
                    col.Visible = item.Selected;
                }
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            string sFileName = "ReporteDeCuenta"+ Utils.Now.ToString("dd-MM-yyyy") +".xls";
            ExportToExcel(sFileName,this.gridMovCuentasBanco);
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
            string sHeader = "<table><tr><td><b>";
            sHeader += "Cuenta: </b></td><td colspan=\"3\">" + this.ddlCuentas.SelectedItem.Text + "</td></tr><tr><td><b>";
            sHeader += "Periodo: </b></td><td>" + this.txtFecha1.Text;
            sHeader += "</td><td><b>A:</b></td><td>" + this.txtFecha2.Text + "</td></tr>";
            sHeader += "<tr><td><b>Saldo Inicial:</b></td><td>" + this.lblSaldoinicial.Text ;
            sHeader += "</td><td><b>Saldo Final:</b></td><td>" + this.lblSaldofinal.Text;
            sHeader += "</td></tr></table>";
            this.gridMovCuentasBanco.AllowPaging = false; //all the movements in one page.
            this.gridMovCuentasBanco.Columns[0].Visible = false; //hide the select button
            this.gridMovCuentasBanco.DataBind();
            Page pPage = new Page();
            HtmlForm fForm = new HtmlForm();
            this.gridMovCuentasBanco.EnableViewState = false;
            pPage.Controls.Add(fForm);
            Label lblHeader = new Label();
            lblHeader.Text = sHeader;
            //this.gridMovCuentasBanco.RenderControl(oHtmlTextWriter);
            fForm.Controls.Add(lblHeader);
            fForm.Controls.Add(this.gridMovCuentasBanco);
            pPage.RenderControl(oHtmlTextWriter);
            this.gridMovCuentasBanco.AllowPaging = true;
            this.gridMovCuentasBanco.Columns[0].Visible = true;
            Response.Write(oStringWriter.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */

        }
    }
}
