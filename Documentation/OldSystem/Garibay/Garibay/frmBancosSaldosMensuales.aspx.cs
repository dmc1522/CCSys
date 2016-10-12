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
using System.Text;
using System.IO;

namespace Garibay
{
    public partial class frmBancosSaldosMensuales : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                this.cmbCuenta.DataBind();
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDEBANCO, Logger.typeUserActions.SELECT, this.UserID, "VISITÓ SALDOS MENSUALES DE BANCO"); 
            }
            this.LoadSaldos();
        }



        protected void cmbCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadSaldos()
        {
            dsMovBanco.dtSaldosDataTable dtSaldos = new dsMovBanco.dtSaldosDataTable();
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand comm = new SqlCommand();
            try
            {
                SqlDataAdapter sqlDA = new SqlDataAdapter(comm);
                comm.Connection = conn;
                comm.CommandText = "SELECT     MovimientosCuentasBanco.cuentaID, SUM(MovimientosCuentasBanco.cargo) AS Cargos, SUM(MovimientosCuentasBanco.abono) AS Abonos, Meses.month, YEAR(MovimientosCuentasBanco.fecha) as year FROM         MovimientosCuentasBanco INNER JOIN Meses ON MONTH(MovimientosCuentasBanco.fecha) = Meses.monthID where cuentaID = @cuentaID GROUP BY MovimientosCuentasBanco.cuentaID, MONTH(MovimientosCuentasBanco.fecha), YEAR(MovimientosCuentasBanco.fecha), Meses.month  ORDER BY YEAR(MovimientosCuentasBanco.fecha) DESC, MONTH(MovimientosCuentasBanco.fecha) DESC";
                comm.Parameters.Add("@cuentaID", SqlDbType.Int).Value = this.cmbCuenta.SelectedValue;
                //comm.Parameters.Add("@year", SqlDbType.Int).Value = this.drddlAnio.SelectedValue;
                sqlDA.Fill(dtSaldos);
                double fSaldo = 0;
                for (int i = dtSaldos.Rows.Count - 1; i >= 0; i--)
                {
                    dsMovBanco.dtSaldosRow row = (dsMovBanco.dtSaldosRow)dtSaldos.Rows[i];
                    fSaldo += row.Abonos - row.Cargos;
                    row.Saldo = fSaldo;
                }
                //dtSaldos.DefaultView.RowFilter = "year = " + this.drddlAnio.SelectedValue;
                this.gridSaldosMensuales.DataSource = dtSaldos;
                this.gridSaldosMensuales.DataBind();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "LoadSaldos", ref ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
