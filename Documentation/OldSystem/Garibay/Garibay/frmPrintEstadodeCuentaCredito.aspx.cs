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
using System.Globalization;

namespace Garibay
{
    public partial class frmPrintEstadodeCuentaCredito : Garibay.BasePage
    {
        DataTable dtEstadoCuenta;
        protected void Page_Load(object sender, EventArgs e)
        {
           if(!this.IsPostBack)
           {

              this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
               this.ddlCredito.DataBind();
               this.cargaDatosProductor();
               this.gridEstadodeCuenta.DataBind();
               this.dvEstadoGeneral.DataBind();
               if (Request.QueryString["data"] != null && this.loadqueryStrings(Request.QueryString["data"].ToString()) && this.myQueryStrings != null && this.myQueryStrings["creditoID"] != null)
               {
                   this.ddlCredito.SelectedValue = this.myQueryStrings["creditoID"].ToString();
                   this.cargaDatosProductor();
                   this.gridEstadodeCuenta.DataBind();
                   this.dvEstadoGeneral.DataBind();
                   this.gridProductos.DataBind();
                   Byte[] bytes;
                   bytes = PdfCreator.PrintEstadoDeCuenta(this.ddlCredito.SelectedItem.Text, 
                       ref this.dvEstadoGeneral, 
                       ref this.gridProductos, ref this.GridView1, ref this.gridEstadodeCuenta, int.Parse(this.myQueryStrings["creditoID"].ToString()));
                   Response.Clear();
                   Response.ContentType = "application/pdf";
                   Response.AddHeader("content-disposition", "attachment; filename=EstadoDeCuenta.pdf");
                   Response.BinaryWrite(bytes);
                   Response.Flush();
                   Response.End();
               }
           }

        }
       
       
        protected void gridEstadodeCuenta_DataBound(object sender, EventArgs e)
        {
            Utils.MergeSameRowsInGVPerColumn(ref this.gridEstadodeCuenta,0);
            double montocredito = 0, interes = 0, debe = 0, abono = 0;
            int dias = 0;
             foreach (GridViewRow row in this.gridEstadodeCuenta.Rows)
               {
                   if (row.RowType == DataControlRowType.DataRow)
                   {
                       double.TryParse(row.Cells[3].Text,NumberStyles.Currency, null, out montocredito);
                       if (montocredito == 0) row.Cells[3].Text = "";
                       
                       int.TryParse(row.Cells[5].Text,NumberStyles.Currency, null, out dias);
                       if (dias == 0) row.Cells[5].Text = "";
                       
                       double.TryParse(row.Cells[6].Text,NumberStyles.Currency, null, out interes);
                       if (interes == 0) row.Cells[6].Text = "";
                       
                       double.TryParse(row.Cells[7].Text,NumberStyles.Currency, null, out debe);
                       if (debe == 0) row.Cells[7].Text = "";
                       
                       double.TryParse(row.Cells[8].Text,NumberStyles.Currency, null, out abono);
                       if (abono == 0) row.Cells[8].Text = "";

                       if (row.Cells[2].Text.IndexOf("CORTE") >= 0)
                       {
                           Utils.MergeColumnsPerRow(ref this.gridEstadodeCuenta, row.RowIndex, 2, 9);
                       }
                   }
               }
        }

        protected void btnAddPago_Click(object sender, EventArgs e)
        {
            String sNewUrl = "~/frmPagosCreditosAdd.aspx?data=";
            sNewUrl += Utils.encriptacadena("creditoID=" + this.ddlCredito.Text);
            Response.Redirect(sNewUrl);
        }

        protected void gridProductos_DataBound(object sender, EventArgs e)
        {
//             if(this.gridProductos.Rows.Count>0)
//             {
//                 DateTime fecha = (DateTime)(this.gridProductos.DataKeys[0]["Fecha"]);
//                 string sql = " SELECT     SegurosAgricolas.Nombre, Solicitudes.CostoTotalSeguro " +
//                              " FROM         Solicitudes INNER JOIN " +
//                              " Creditos ON Solicitudes.creditoID = Creditos.creditoID INNER JOIN " +
//                              " solicitud_SeguroAgricola ON Solicitudes.solicitudID = solicitud_SeguroAgricola.solicitudID INNER JOIN " +
//                              " SegurosAgricolas ON solicitud_SeguroAgricola.seguroID = SegurosAgricolas.seguroID ";
//                 SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
//                 SqlCommand cmdGaribay = new SqlCommand(sql, conGaribay);
//                 GridView row = this.gridProductos,
//             }
            
        }

        protected void ddlCredito_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cargaDatosProductor();
        }
        protected void cargaDatosProductor()
        {
            string sql = "SELECT     Productores.poblacion + ' ' + Productores.municipio + ' ' + Estados.estado AS Poblacion, Productores.telefono, Productores.domicilio, " +
                         " Solicitudes.Descripciondegarantias " +
                         " FROM        Productores INNER JOIN " +
                         " Creditos ON Productores.productorID = Creditos.productorID INNER JOIN " +
                         " Estados ON Productores.estadoID = Estados.estadoID INNER JOIN " +
                         " Solicitudes ON Creditos.creditoID = Solicitudes.creditoID where Creditos.creditoID = @creditoID ";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdSacadata = new SqlCommand(sql,conGaribay);
            try
            {
                conGaribay.Open();
                cmdSacadata.Parameters.Add("@creditoID", SqlDbType.Int).Value = int.Parse(this.ddlCredito.SelectedValue);
                SqlDataReader reader = cmdSacadata.ExecuteReader();
                if (reader.Read())
                {
                    this.txtPoblacion.Text = reader["Poblacion"].ToString();
                    this.txtTelefono.Text = reader["telefono"].ToString();
                    this.txtDireccion.Text = reader["domicilio"].ToString();
                    this.txtGarantias.Text = reader["Descripciondegarantias"].ToString();

                }
            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "ERROR AL SACAR DATOS DEL PRODUCTOR EN ESTADO DE CUENTA", ref ex);
            }
            finally
            {
                conGaribay.Close();
            }
        }
    }
}
