using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Garibay
{
    public partial class frmConciliacionBancos : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack){
                this.chkMuestraConciliados.Checked = true;
                this.chkMuestraNoConciliados.Checked = true;
                this.drpdlCuenta.DataBind();
                this.drddlAnio.DataBind();
                this.drpdlMes.DataBind();
                this.drpdlMes.SelectedValue = Utils.Now.Month.ToString();
                this.drddlAnio.SelectedValue = Utils.Now.Year.ToString();
                this.actualizaFilterExpression();
                string sOnchange;
                sOnchange = "ShowHideDivOnChkBox('";
                sOnchange += this.chkMuestraConciliados.ClientID + "','";
                sOnchange += this.Panelconciliados.ClientID + "')";
                this.chkMuestraConciliados.Attributes.Add("onclick", sOnchange);
               
                sOnchange = "ShowHideDivOnChkBox('";
                sOnchange += this.chkMuestraNoConciliados.ClientID + "','";
                sOnchange += this.PanelNoConciliados.ClientID + "')";
                this.chkMuestraNoConciliados.Attributes.Add("onclick", sOnchange);
              
            }
            this.Panelconciliados.Attributes.Add("style", this.chkMuestraConciliados.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");

            this.PanelNoConciliados.Attributes.Add("style", this.chkMuestraNoConciliados.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");

            this.compruebasecurityLevel();
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
            }

        }

        protected void drpdlCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.actualizaFilterExpression();
        }
        protected void actualizaFilterExpression(){
           
            DateTime dtInicio = new DateTime();
            DateTime dtFin = new DateTime();
            TimeSpan tsOneDay = new TimeSpan(1, 0, 0, 0);
            dtInicio = new DateTime(int.Parse(this.drddlAnio.SelectedValue), int.Parse(this.drpdlMes.SelectedValue), 1);
            // TimeSpan tsUndia = new TimeSpan(1, 0, 0, 0);
            if (dtInicio.Month < 12)
                dtFin = new DateTime(dtInicio.Year, dtInicio.Month + 1, 1, 23, 59, 59);
            else
                dtFin = new DateTime(dtInicio.Year + 1, 1, 1, 23, 59, 59);
            dtFin = dtFin - tsOneDay;
            this.txtFechaIncioLargo.Text = dtInicio.ToString("dd/MM/yyyy hh:ss:mm");
            this.txtFechaFinLargo.Text = dtFin.ToString("dd/MM/yyyy 00:00:00");
            this.sdsConciliados.FilterExpression = " fecha>= '";
            this.sdsConciliados.FilterExpression += txtFechaIncioLargo.Text;
            this.sdsConciliados.FilterExpression += "' AND fecha<= '";
            this.sdsConciliados.FilterExpression += txtFechaFinLargo.Text;
            this.sdsConciliados.FilterExpression += "' ";
            this.sdsNoConciliados.FilterExpression = this.sdsConciliados.FilterExpression;
            this.gvConciliados.DataBind();
            this.gvNoConciliados.DataBind();
            
        }

        protected void drpdlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.actualizaFilterExpression();
        }

        protected void drddlAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.actualizaFilterExpression();
        }

        protected void btnConciliacion_Click(object sender, EventArgs e)
        {
            string query = "update MovimientosCuentasBanco set chequecobrado = @cobrado where movbanID = @movID";
            SqlConnection conUpdate = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdUpdate = new SqlCommand(query, conUpdate);
            conUpdate.Open();
            try
            {
                foreach (GridViewRow row in this.gvNoConciliados.Rows)
                {
                    CheckBox aux = ((CheckBox)(row.Cells[0].Controls[1]));
                    cmdUpdate.Parameters.Clear();
                    cmdUpdate.Parameters.Add("@cobrado", SqlDbType.Bit).Value = aux.Checked ? 1 : 0;
                    cmdUpdate.Parameters.Add("@movID", SqlDbType.Int).Value = int.Parse(this.gvNoConciliados.DataKeys[row.RowIndex]["movbanID"].ToString());
                    cmdUpdate.ExecuteNonQuery();

                }
                foreach (GridViewRow row in this.gvConciliados.Rows)
                {
                    CheckBox aux = ((CheckBox)(row.Cells[0].Controls[1]));
                    cmdUpdate.Parameters.Clear();
                    cmdUpdate.Parameters.Add("@cobrado", SqlDbType.Bit).Value = aux.Checked ? 0 : 1;
                    cmdUpdate.Parameters.Add("@movID", SqlDbType.Int).Value = int.Parse(this.gvConciliados.DataKeys[row.RowIndex]["movbanID"].ToString());
                    cmdUpdate.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR  ACTUALIZANDO EL ESTADO DE MOVIMIENTOS DE BANCO, LA EXC ES: " + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                conUpdate.Close();
            }
            this.actualizaFilterExpression();
            //this.sacaTotales();
        }
    }
}
