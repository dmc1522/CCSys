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
    public partial class frmReporteGeneralLiquidaciones : Garibay.BasePage
    {
        public frmReporteGeneralLiquidaciones():base()
        {
            this.hasCalendar = true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.drpdlCiclo.DataBind();            
            }

        }

        protected void PopCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            this.ponfechaslargas();
        }

        protected void PopCalendar2_SelectionChanged(object sender, EventArgs e)
        {
            this.ponfechaslargas();
        }

        protected void drpdlCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvReporte.DataBind();
            this.gvTotalPorProducto.DataBind();
            
        }
        protected void ponfechaslargas(){

            /*
            this.txtFecha1Larga.Text = Utils.converttoLongDBFormat(this.txtFechaInicio.Text);
                        this.txtFecha2Larga.Text = Utils.converttoFechaForFilterLimite(this.txtFechaFin.Text);*/
            
        }

        protected void gvReporte_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow){
               string sql = " SELECT     SUM(MovimientosCuentasBanco.cargo) AS TotalBancos, SUM(MovimientosCaja.cargo) AS TotalCaja ";
               sql += "FROM         Liquidaciones INNER JOIN ";
               sql +=  "PagosLiquidacion ON Liquidaciones.LiquidacionID = PagosLiquidacion.liquidacionID LEFT OUTER JOIN ";
               sql += " MovimientosCaja ON PagosLiquidacion.movimientoID = MovimientosCaja.movimientoID LEFT OUTER JOIN ";
               sql += " MovimientosCuentasBanco ON PagosLiquidacion.movbanID = MovimientosCuentasBanco.movbanID ";
               sql += " WHERE     (Liquidaciones.cicloID = @cicloID)  AND (Liquidaciones.cobrada = 1) ";
               SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
               SqlCommand cmdSel = new SqlCommand(sql, conGaribay);
               conGaribay.Open();
                try{
                    cmdSel.Parameters.Clear();
                    cmdSel.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(this.drpdlCiclo.SelectedValue);
//                     cmdSel.Parameters.Add("@fechaini", SqlDbType.DateTime).Value = this.txtFecha1Larga.Text;
//                     cmdSel.Parameters.Add("@fechafin", SqlDbType.DateTime).Value = this.txtFecha2Larga.Text;
                    SqlDataReader reader = cmdSel.ExecuteReader();
                    if(reader.Read()){
                        e.Row.Cells[6].Text = reader[0].ToString().Length>0 ? string.Format("{0:C2}", double.Parse(reader[0].ToString())) : "$ 0.00";
                        e.Row.Cells[7].Text = reader[1].ToString().Length > 0 ? string.Format("{0:C2}", double.Parse(reader[1].ToString())) : "$ 0.00";
                    }



                }
                catch (Exception ex){
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, this.UserID, "ERROR AL SACAR TOTAL EN BANCOS Y TOTAL EN CAJA DEL REPORTE GENERAL DE LIQUIDACIONES. EX: " + ex.Message, this.Request.Url.ToString());

                }
                finally{
                    conGaribay.Close();
                }
            }
        }

       
    }
}
