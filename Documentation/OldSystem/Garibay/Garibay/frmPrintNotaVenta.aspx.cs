using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;



namespace Garibay
{
    public partial class frmPrintNotaVenta : Garibay.BasePage
    {

       
        dsNV.dtNVdetalleDataTable dt;
        dsNV.dtNVdatosDataTable dtDatos;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                if (Request.QueryString["data"] != null)
                {
                    if (this.loadqueryStrings(Request.QueryString["data"].ToString()))
                    {
                        SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                        
                        try
                        {
                            conn.Open();
                            this.txtNotaventaID.Text=myQueryStrings["notadeventaID"].ToString();
                            dt = new dsNV.dtNVdetalleDataTable();                          
                            string query ="SELECT     Notasdeventa.notadeventaID, Notasdeventa.productorID, Notasdeventa.cicloID, Notasdeventa.userID, Notasdeventa.Folio, Notasdeventa.Fecha, "; 
                            query +=" Notasdeventa.Pagada, Notasdeventa.Total, Notasdeventa.Subtotal, Notasdeventa.Iva, Notasdeventa.creditoID, Notasdeventa.Fechadepago, Notasdeventa.Interes, "; 
                            query +=" Notasdeventa.Observaciones, Notasdeventa.acredito, Notasdeventa.tipocalculodeinteresID, Notasdeventa.origen, Notasdeventa.remitente, Notasdeventa.domicilio, "; 
                            query +=" Notasdeventa.telefono, Notasdeventa.destino, Notasdeventa.numeropermiso, Notasdeventa.transportista, Notasdeventa.nombrechofer, Notasdeventa.tractorcamion, "; 
                            query +=" Notasdeventa.color, Notasdeventa.placas, Notasdeventa.storeTS, Notasdeventa.fechainiciobrointereses, Notasdeventa.fechafincobrointereses, ";
                            query += " LTRIM(Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre) AS Productor, Ciclos.CicloName, Users.Nombre AS hechoxNombre"; 
                            query +=" FROM         Notasdeventa INNER JOIN "; 
                            query +=" Productores ON Notasdeventa.productorID = Productores.productorID INNER JOIN "; 
                            query +=" Ciclos ON Notasdeventa.cicloID = Ciclos.cicloID INNER JOIN ";
                            query +=" Users ON Notasdeventa.userID = Users.userID ";
                            query += " WHERE     (Notasdeventa.notadeventaID = @notadeventaID) ";
                            SqlDataAdapter sqlDA = new SqlDataAdapter(query,conn);
                            dtDatos = new dsNV.dtNVdatosDataTable();
                            sqlDA.SelectCommand.Parameters.Add("@notadeventaID", SqlDbType.Int).Value = myQueryStrings["notadeventaID"].ToString();
                            sqlDA.Fill(dtDatos);
                            this.grdNota.DataSource = dtDatos;
                            this.grdNota.DataBind();
                            SqlCommand SacaEsFertilizante = new SqlCommand("select notaDeFertilizante from NotasdeVenta where notadeventaID = @notadeventaID",conn);
                            SacaEsFertilizante.Parameters.Add("@notadeventaID", SqlDbType.Int).Value = int.Parse(myQueryStrings["notadeventaID"].ToString());
                            this.chkboxFertilizante.Checked = bool.Parse(SacaEsFertilizante.ExecuteScalar().ToString());



                            
                               
                            float[] a= new float[10];
                            float[] fDetalleColSize = new float[]{6,9,9,6,6,6,6};
                            float[] fPagosColSize = new float[]{6,0,0,0,8,6,6,8,6,0};
                            
                            this.SqlPagos.SelectCommand.Replace("@notadeventaID", myQueryStrings["notadeventaID"].ToString());
                            this.grvPagos.DataBind();
                            this.GridView4.DataBind();

                            dsNV.dtNVdetalleDataTable dtNotaDetalle = new dsNV.dtNVdetalleDataTable();


                            String sQuery = " SELECT     Productos.Nombre, NotasdeVenta_detalle.cantidad, NotasdeVenta_detalle.precio, NotasdeVenta_detalle.sacos, ";
                            sQuery += " NotasdeVenta_detalle.cantidad * NotasdeVenta_detalle.precio AS Importe, NotasdeVenta_detalle.NDVdetalleID, NotasdeVenta_detalle.notadeventaID, ";
                            sQuery += " NotasdeVenta_detalle.productoID, Bodegas.bodegaID, NotasdeVenta_detalle.userID, NotasdeVenta_detalle.cicloID, NotasdeVenta_detalle.fecha, ";
                            sQuery += " Productos.presentacionID, Productos.unidadID, Productos.productoGrupoID, Presentaciones.peso, Presentaciones.Presentacion, Unidades.Unidad";
                            sQuery += " FROM        NotasdeVenta_detalle INNER JOIN";
                            sQuery += "            Productos ON NotasdeVenta_detalle.productoID = Productos.productoID INNER JOIN";
                            sQuery += " Bodegas ON NotasdeVenta_detalle.bodegaID = Bodegas.bodegaID INNER JOIN";
                            sQuery += " Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID INNER JOIN";
                            sQuery += " Unidades ON Productos.unidadID = Unidades.unidadID";
                            sQuery += " WHERE     (NotasdeVenta_detalle.notadeventaID = @notadeventaID)";
                            SqlCommand commDetalle = new SqlCommand(sQuery, conn);
                            commDetalle.Parameters.Add("@notadeventaID", SqlDbType.Int).Value = int.Parse(myQueryStrings["notadeventaID"].ToString());
                            SqlDataAdapter detalleDA = new SqlDataAdapter(commDetalle);
                            DataTable dtDetalleTemp = new DataTable();
                            detalleDA.Fill(dtDetalleTemp);
                            foreach (DataRow row in dtDetalleTemp.Rows)
                            {
                                dsNV.dtNVdetalleRow newRow = dtNotaDetalle.NewdtNVdetalleRow();
                                //dsFacturasClientes.dtFacAClientesDetallesRow newRow = this.dtFacDetalle.NewdtFacAClientesDetallesRow();
                                newRow.NDVdetalleID = (int)row["NDVdetalleID"];
                                newRow.notadeventaID = (int)row["notadeventaID"];
                                newRow.productoID = (int)row["productoID"];
                                newRow.bodegaID = (int)row["bodegaID"];
                                newRow.cantidad = (double)row["cantidad"];
                                newRow.precio = double.Parse(row["precio"].ToString());
                                newRow.Importe = (double)row["Importe"];
                                newRow.sacos = (double)row["sacos"];
                                newRow.Nombre = row["Nombre"].ToString();
                                newRow.userID = (int)row["userID"];
                                newRow.cicloID = (int)row["cicloID"];
                                newRow.fecha = (DateTime)row["fecha"];
                                newRow.Presentacion = row["Presentacion"].ToString() + " - " + row["Unidad"].ToString();
                                newRow.presentacionID = (int)row["presentacionID"];
                                newRow.unidadID = (int)row["unidadID"];
                                newRow.grupoID = (int)row["productoGrupoID"];
                                newRow.peso = Double.Parse(row["peso"].ToString());
                                if (this.chkboxFertilizante.Checked)
                                {
                                    newRow.peso = (double)(newRow.cantidad * 50) / 1000;                                    
                                    newRow.Importe = newRow.peso * newRow.precio;
                                }
                                else
                                {
                                    newRow.peso = (double)newRow.cantidad;
                                    newRow.Importe = newRow.cantidad * newRow.precio;
                                }
                                 
                               // newRow.Presentacion = row["Presentacion"].ToString();
                                newRow.Unidad = row["Unidad"].ToString();

                                dtNotaDetalle.AdddtNVdetalleRow(newRow);
                            }

                            this.grdvProNotasVenta.DataSource = dtNotaDetalle;
                            if(!this.chkboxFertilizante.Checked)
                            {
                                this.grdvProNotasVenta.Columns[4].Visible = false;

                            }
                            //this.grdvProNotasVenta.Columns[0].Visible = this.grdvProNotasVenta.Columns[7].Visible = false;
                            this.grdvProNotasVenta.DataBind();



                            byte [] bytes = PdfCreator.printNotaVenta(int.Parse(myQueryStrings["notadeventaID"].ToString()), PdfCreator.tamañoPapel.CARTA, PdfCreator.orientacionPapel.HORIZONTAL, ref this.grdNota, ref this.grdvProNotasVenta, ref this.grvPagos,ref this.GridView4, fDetalleColSize, fPagosColSize);
                            Logger.Instance.LogUserSessionRecord(Logger.typeModulo.NOTAVENTA, Logger.typeUserActions.PRINT, this.UserID, "IMPRIMIÓ LA NOTA DE VENTA  NÚMERO:  " + myQueryStrings["notadeventaID"].ToString());
                            Response.ClearHeaders();
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("Content-Disposition", "attachment;filename=NotaVenta" + myQueryStrings["notadeventaID"].ToString() + ".pdf");
                            Response.BinaryWrite(bytes);
                            Response.Flush();
                            Response.End();

                        }
                        catch (Exception ex)
                        {


                        }
                    }
                }
            }
        }



        

        protected void grdvProNotasVenta_DataBound(object sender, EventArgs e)
        {

            
            
        }

        protected double sumaMontos()
        {
            Label lbl;
            double total = 0.0;
            foreach (GridViewRow row in this.grvPagos.Rows)
            {

                total += Utils.GetSafeFloat(row.Cells[7].Text.Replace("$", "").Replace(",", ""));


            }
            return total;
        }

       

        protected void grvPagos_RowDataBound1(object sender, GridViewRowEventArgs e)
        {


            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {

                    //,,


                    if (this.grvPagos.DataKeys[e.Row.RowIndex]["movimientoID"].ToString() != "")
                    {

                        e.Row.Cells[4].Text = "EFECTIVO";
                        e.Row.Cells[5].Text = "";
                        e.Row.Cells[6].Text = "";
                        
                        
                        
                      

                        //if columns are added/ removed please update the index.
                        

                        String query = "SELECT abono FROM MovimientosCaja WHERE movimientoID=@movimientoID";
                        SqlCommand comm = new SqlCommand(query, conn);
                        comm.Parameters.Add("@movimientoID", SqlDbType.Float).Value = int.Parse(this.grvPagos.DataKeys[e.Row.RowIndex]["movimientoID"].ToString());
                        conn.Open();
                       
                        
                        e.Row.Cells[7].Text = string.Format("{0:C2}", Utils.GetSafeFloat(comm.ExecuteScalar().ToString()));

                    }
                    else if (this.grvPagos.DataKeys[e.Row.RowIndex]["tarjetaDieselID"].ToString() != "")
                    {


                        e.Row.Cells[4].Text = "TARJETA DIESEL";
                        
                        e.Row.Cells[6].Text = "";
                       

                        
                        e.Row.Cells[5].Text = this.grvPagos.DataKeys[e.Row.RowIndex]["tarjetaDieselID"].ToString();
                        

                        //if columns are added/ removed please update the index.
                        

                        String query = "SELECT monto FROM TarjetasDiesel WHERE folio=@folio";
                        SqlCommand comm = new SqlCommand(query, conn);
                        comm.Parameters.Add("@folio", SqlDbType.Int).Value = int.Parse(this.grvPagos.DataKeys[e.Row.RowIndex]["tarjetaDieselID"].ToString());
                        conn.Open();
                        e.Row.Cells[7].Text = string.Format("{0:C2}", Utils.GetSafeFloat(comm.ExecuteScalar().ToString()));

                    }
                    else if (this.grvPagos.DataKeys[e.Row.RowIndex]["movbanID"].ToString() != "")
                    {



                        String query = "SELECT     ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.abono, Bancos.nombre, MovimientosCuentasBanco.numCheque ";
                        query += " FROM          MovimientosCuentasBanco INNER JOIN ";
                        query += " ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID ";
                        query += " INNER JOIN ";
                        query += " CuentasDeBanco ON MovimientosCuentasBanco.cuentaID = CuentasDeBanco.cuentaID INNER JOIN ";
                        query += " Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID ";
                        query += " where MovimientosCuentasBanco.movbanID=@movbanID";
                        SqlCommand comm = new SqlCommand(query, conn);
                        conn.Open();
                        comm.Parameters.Add("@movbanID", SqlDbType.Int).Value = int.Parse(this.grvPagos.DataKeys[e.Row.RowIndex]["movbanID"].ToString());
                        SqlDataReader rd = comm.ExecuteReader(); ;

                        //if columns are added/ removed please update the index.
                        if (rd.HasRows && rd.Read())
                        {
                            

                            e.Row.Cells[5].Text = rd["numCheque"].ToString() != "0" ? rd["numCheque"].ToString() : "";

                            
                            e.Row.Cells[4].Text = rd["Concepto"].ToString();



                            
                            e.Row.Cells[7].Text = string.Format("{0:c2}", Utils.GetSafeFloat(rd["abono"].ToString()));

                            e.Row.Cells[6].Text = rd["nombre"].ToString();
                        }

                    }
                    else if (this.grvPagos.DataKeys[e.Row.RowIndex]["boletaID"].ToString() != "")
                    {



                        String query = "SELECT     Pagos_NotaVenta.fecha, Boletas.Ticket, Boletas.totalapagar ";
                        query += " FROM         Boletas INNER JOIN ";
                        query += " Pagos_NotaVenta ON Boletas.boletaID = Pagos_NotaVenta.boletaID ";
                        query += " WHERE     (Boletas.boletaID = @boletaID)";


                        SqlCommand comm = new SqlCommand(query, conn);
                        conn.Open();
                        comm.Parameters.Add("@boletaID", SqlDbType.Int).Value = int.Parse(this.grvPagos.DataKeys[e.Row.RowIndex]["boletaID"].ToString());
                        SqlDataReader rd = comm.ExecuteReader(); ;

                        //if columns are added/ removed please update the index.
                        if (rd.HasRows && rd.Read())
                        {
                            
                           
                            e.Row.Cells[5].Text = rd["Ticket"].ToString();


                            e.Row.Cells[4].Text = "BOLETA";



                            
                            e.Row.Cells[7].Text = string.Format("{0:c2}", Utils.GetSafeFloat(rd["totalapagar"].ToString()));

                            e.Row.Cells[6].Text = "";
                        }

                    }
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "error al meter js a controles en row", ref ex);

                }
                finally
                {

                    conn.Close();
                }


            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                   e.Row.Cells[6].Text = "TOTAL";


              e.Row.Cells[7].Text = string.Format("{0:C2}", sumaMontos());
            }


        }

      
    }
}
