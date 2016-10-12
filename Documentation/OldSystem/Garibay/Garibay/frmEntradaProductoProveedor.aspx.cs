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
    public partial class Formulario_web15 : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            this.panelmensaje.Visible = false;
            if (!IsPostBack)
            {
                this.ddlCiclos.DataBind();
                this.ddlGrupos.DataBind();
                this.ddlNewBoletaBodega.DataBind();
                this.ddlNewBoletaProducto.DataBind();
                this.ddlProveedor.DataBind();
               
                this.btnAddOrdenEntrada.Visible=true;
                this.btnModifyOrdenEntrada.Visible=false;
                this.txtFecha.Text = Utils.Now.ToShortDateString();
                
                if (Request.QueryString["data"] != null && this.loadqueryStrings(Request.QueryString["data"].ToString()) && this.myQueryStrings != null && this.myQueryStrings["OrdenID"] != null)
                {
                    this.panelmensaje.Visible = false;


                    this.lblOrdenID.Text = "ORDEN DE ENTRADA : " + this.myQueryStrings["OrdenID"].ToString();
                    this.txtOrdenID.Text=this.myQueryStrings["OrdenID"].ToString();


                    if (this.LoadOrden())
                    {
                        this.pnlAddProd.Visible = true;
                        this.btnAddOrdenEntrada.Visible=false;
                        this.btnModifyOrdenEntrada.Visible=true;
             
                    }
                    else
                    {
                        this.pnlAddProd.Visible = false;
                    }

                }
                else
                {
                    this.pnlAddProd.Visible = false;
                }

            }

            string sOnchange = "ShowHideDivOnChkBox('";

            sOnchange += this.chkChangeFechaSalidaNewBoleta.ClientID + "','";
            sOnchange += this.divFechaSalidaNewBoleta.ClientID + "')";
            this.chkChangeFechaSalidaNewBoleta.Attributes.Add("onclick", sOnchange);
            this.divFechaSalidaNewBoleta.Attributes.Add("style", this.chkChangeFechaSalidaNewBoleta.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            sOnchange = "subTextBoxes('";
            sOnchange += this.txtNewPesoEntrada.ClientID + "','";
            sOnchange += this.txtNewPesoSalida.ClientID + "','";
            sOnchange += this.txtPesoNetoNewBoleta.ClientID + "')";
            this.txtNewPesoEntrada.Attributes.Add("onKeyUp", sOnchange);
            this.txtNewPesoEntrada.Attributes.Add("onBlur", sOnchange);
            this.txtNewPesoSalida.Attributes.Add("onKeyUp", sOnchange);
            this.txtNewPesoSalida.Attributes.Add("onBlur", sOnchange);

        }

        protected void btnAddproduct_Click(object sender, EventArgs e)
        {
            int entradaID=0;
            string error="";

            if(dbFunctions.addEntradaPro(int.Parse(this.drpdlProducto.SelectedValue),int.Parse(this.drpdlBodega.SelectedValue),2,Utils.Now.ToString(),double.Parse(this.txtCantidad.Text),double.Parse(this.txtPrecio.Text),"",this.UserID,int.Parse(this.ddlCiclos.SelectedValue),ref error, out entradaID))
            {
                if(dbFunctions.addOrden_EntradaPro(int.Parse(this.txtOrdenID.Text),int.Parse(this.drpdlProducto.SelectedValue),double.Parse(this.txtCantidad.Text),double.Parse(this.txtPrecio.Text),0,false,entradaID,-1))
                {
                    
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.ENTRADAPRODUCTOS, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("SE AGREGÓ EL PRODUCTO: " + this.drpdlProducto.SelectedItem.Text));
                    this.grdvProductosRecibidos.DataBind();
                }
            }

            }

        protected void btnAddOrdenEntrada_Click(object sender, EventArgs e)
        {
            int idOrden;
            if (!dbFunctions.folioDeOrdenExiste(this.txtFolio.Text, out idOrden))
            {
            string sqlQuery = "INSERT INTO [Orden_de_entrada] (proveedorID,cicloID,Fecha,userID,Folio) VALUES (@proveedorID,@cicloID,@Fecha,@userID,@Folio); select ID = SCOPE_IDENTITY();";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(sqlQuery, conGaribay);
            int ID = -1;
            string sError = "";
            try
            {


                cmdIns.Parameters.Add("@proveedorID", SqlDbType.Int).Value = int.Parse(this.ddlProveedor.SelectedValue);
                cmdIns.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(this.ddlCiclos.SelectedValue);
                cmdIns.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = (DateTime)DateTime.Parse(this.txtFecha.Text);
                cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                cmdIns.Parameters.Add("@Folio", SqlDbType.NVarChar).Value = this.txtFolio.Text;
                
                conGaribay.Open();
                int numregistros;
                numregistros = int.Parse(cmdIns.ExecuteScalar().ToString());
                if (numregistros <= 0)
                {
                    throw new Exception("AL TRATAR DE INSERTAR UNA ORDEN DE ENTRADA. LA BASE DE DATOS REGRESÓ QUE SE ALTERARAON " + numregistros.ToString() + " REGISTROS");
                }
                ID = numregistros;
                String Query = "OrdenID=" + ID;
                Query = Utils.GetEncriptedQueryString(Query);
                String strRedirect = "~/frmEntradaProductoProveedor.aspx" + Query;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.ENTRADAPRODUCTOS, Logger.typeUserActions.INSERT, " SE AGREGO LA ORDEN DE ENTRADA : " + this.txtFolio.Text);

                Response.Redirect(strRedirect);
                   


            }
            catch (Exception exception)
            {
                sError = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT,this.UserID, exception.Message, "EL ERROR SE DIÓ AL INGRESAR UNA ORDEN DE ENTRADA");
                

            }
            finally
            {
                conGaribay.Close();

            }


            }
            else
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "LA ORDEN DE ENTRADA DE PRODUCTO CON EL FOLIO " + this.txtFolio.Text+" YA EXISTE";
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                this.hlnkAbrir.Text = "ABRIR ORDEN DE ENTRADA CON EL FOLIO " + this.txtFolio.Text;
                this.hlnkAbrir.NavigateUrl=this.GetOrdenNavigationURL(idOrden.ToString());
            }

        }
        protected bool LoadOrden()
        {

            bool sresult;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            string sQuery = " SELECT    proveedorID, cicloID, userID, fecha, Folio FROM Orden_de_entrada where ordenID=@OrdenID";
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = sQuery;
                comm.Parameters.Add("@OrdenID", SqlDbType.Int).Value = int.Parse(this.txtOrdenID.Text);
                SqlDataReader rd;
                rd = comm.ExecuteReader();
                if (rd.HasRows && rd.Read())
                {
                    this.txtFecha.Text = DateTime.Parse(rd["fecha"].ToString()).ToShortDateString().ToString();
                    
                    this.txtFolio.Text = rd["Folio"].ToString();
                    this.txtFolioActual.Text=this.txtFolio.Text;
            
                    this.ddlProveedor.SelectedValue =rd["proveedorID"].ToString();
                    this.ddlCiclos.SelectedValue = rd["cicloID"].ToString();

                    
                }
                
                sresult = true;
                //Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.SELECT, this.UserID, "SE CARGARON LOS DATOS DE LA ORDEN DE ENTRADA CON EL FOLIO " + this.txtFolio.Text, this.Request.Url.ToString());

            }
            catch (Exception ex)
            {
                sresult = false;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, this.UserID, "ERROR AL CARGAR DE LA ORDEN DE ENTRADA:  EX " + ex.Message, this.Request.Url.ToString());
            }
            return sresult;
        }

        protected void btnModifyOrdenEntrada_Click(object sender, EventArgs e)
        {
            int idOrden;

            if(this.txtFolioActual.Text==this.txtFolio.Text){
                this.updateOrden();
            }
            else if(!dbFunctions.folioDeOrdenExiste(this.txtFolio.Text, out idOrden))
            {
                this.updateOrden();
            }
            else
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "LA ORDEN DE ENTRADA DE PRODUCTO CON EL FOLIO " + this.txtFolio.Text + " YA EXISTE";
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                this.hlnkAbrir.Text = "ABRIR ORDEN DE ENTRADA CON EL FOLIO " + this.txtFolio.Text;
                this.hlnkAbrir.NavigateUrl = this.GetOrdenNavigationURL(idOrden.ToString());
            }
            
            


        }

        protected void grdvProductosRecibidos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            this.SqlProductosRecibidos.DeleteParameters.Add("@ordenDetalleID", e.Keys[0].ToString());
        }

        protected void btnAgregarBoleta_Click(object sender, EventArgs e)
        {
            //if (!dbFunctions.folioDeOrdenExiste(this.txtFolio.Text, out idOrden))
            //{
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                sqlConn.Open();
                SqlConnection addConn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand addComm = new SqlCommand();
                addComm.Connection = addConn;
                addConn.Open();
                addComm.CommandText = "INSERT INTO Boletas (productorID, NombreProductor, NumeroBoleta, Ticket, bodegaID, cicloID, FechaEntrada, PesoDeEntrada, FechaSalida, PesoDeSalida, pesonetoentrada,  pesonetosalida, totalapagar, productoID, humedad, impurezas, pesonetoapagar, importe, precioapagar, dctoSecado, userID) VALUES     (@productorID,@NombreProductor,@NumeroBoleta,@Ticket,@bodegaID,@cicloID,@FechaEntrada,@PesoDeEntrada,@FechaSalida,@PesoDeSalida,@pesonetoentrada,@pesonetosalida,@totalapagar,@productoID,@humedad,@impurezas,@pesonetoapagar,@importe, @precioapagar,@dctoSecado,@userID); select boletaID = SCOPE_IDENTITY();";
                addComm.Parameters.Add("@productorID", SqlDbType.Int).Value = -1;
                addComm.Parameters.Add("@NombreProductor", SqlDbType.VarChar).Value = "";
                addComm.Parameters.Add("@NumeroBoleta", SqlDbType.VarChar).Value = this.txtNewNumBoleta.Text;
                addComm.Parameters.Add("@Ticket", SqlDbType.VarChar).Value = this.txtNewTicket.Text;
                addComm.Parameters.Add("@bodegaID", SqlDbType.Int).Value = int.Parse(this.ddlNewBoletaBodega.SelectedValue);
                addComm.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(this.ddlCiclos.SelectedValue);
                DateTime dtFechaEntrada = new DateTime();
                if (!DateTime.TryParse(this.txtNewFechaEntrada.Text /*+ " " + this.txtNewHoraEntrada.Text*/, out dtFechaEntrada))
                {
                    dtFechaEntrada = Utils.Now;
                }

                addComm.Parameters.Add("@FechaEntrada", SqlDbType.DateTime).Value = dtFechaEntrada;
                double dPesoEntrada = 0;
                double.TryParse(this.txtNewPesoEntrada.Text, out dPesoEntrada);
                addComm.Parameters.Add("@PesoDeEntrada", SqlDbType.Float).Value = dPesoEntrada;
                DateTime dtFechaSalida = new DateTime();
                if (this.chkChangeFechaSalidaNewBoleta.Checked)
                {
                    if (!DateTime.TryParse(this.txtNewFechaSalida.Text /*+ " " + this.txtNewHoraEntrada.Text*/, out dtFechaSalida))
                    {
                        dtFechaSalida = Utils.Now;
                    }
                }
                else
                    dtFechaSalida = dtFechaEntrada;
                addComm.Parameters.Add("@FechaSalida", SqlDbType.DateTime).Value = dtFechaSalida;
                //    // addComm.Parameters.AddWithValue("@FechaSalida", newRow.FechaSalida);
                double dPesoSalida = 0;
                double.TryParse(this.txtNewPesoSalida.Text, out dPesoSalida);
                addComm.Parameters.Add("@PesoDeSalida", SqlDbType.Float).Value = dPesoSalida;
                double dPesoNetoEntrada = 0;
                double dPesoNetoSalida = 0;
                if (dPesoEntrada - dPesoSalida > 0)
                {
                    dPesoNetoEntrada = dPesoEntrada - dPesoSalida;
                    dPesoNetoSalida = 0;
                }
                else
                {
                    dPesoNetoEntrada = 0;
                    dPesoNetoSalida = dPesoSalida - dPesoEntrada;
                }
                addComm.Parameters.Add("@pesonetoentrada", SqlDbType.Float).Value = dPesoNetoEntrada;
                addComm.Parameters.Add("@pesonetosalida", SqlDbType.Float).Value = dPesoNetoSalida;
                addComm.Parameters.Add("@pesonetoapagar", SqlDbType.Float).Value = dPesoNetoEntrada - dPesoNetoSalida;
                decimal dPrecio = 0;
                decimal.TryParse(this.txtNewPrecio.Text, out dPrecio);
                addComm.Parameters.Add("@importe", SqlDbType.Float).Value = (dPesoNetoEntrada - dPesoNetoSalida) * float.Parse(dPrecio.ToString());
                addComm.Parameters.Add("@productoID", SqlDbType.Int).Value = int.Parse(this.ddlNewBoletaProducto.SelectedValue);
                double dHumedad = 0;
                double.TryParse(this.txtNewHumedad.Text, out dHumedad);
                double dImpurezas = 0;
                double.TryParse(this.txtNewImpurezas.Text, out dImpurezas);
                addComm.Parameters.Add("@humedad", SqlDbType.Float).Value = dHumedad;
                addComm.Parameters.Add("@impurezas", SqlDbType.Float).Value = dImpurezas;
                dPrecio = 0;
                decimal.TryParse(this.txtNewPrecio.Text, out dPrecio);
                double dSecado = 0;
                double.TryParse(this.txtNewSecado.Text, out dSecado);
                addComm.Parameters.Add("@dctoSecado", SqlDbType.Float).Value = dSecado;
                addComm.Parameters.Add("@precioapagar", SqlDbType.Float).Value = dPrecio;
                double kgh = 0;
                double kgi = 0;           
                Utils.getDesctoHumedad(dHumedad, kgh);
                Utils.getDesctoImpurezas(dImpurezas, kgi);               
                addComm.Parameters.Add("@totalapagar", SqlDbType.Float).Value = (dPesoNetoEntrada - kgh - kgi) * float.Parse(dPrecio.ToString());
                double pesotot=(dPesoNetoEntrada - kgh - kgi);
                addComm.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                int newbol = int.Parse(addComm.ExecuteScalar().ToString());
                string qr = "INSERT INTO boleta_proveedor (boletaID,proveedorID) VALUES (@boletaID,@proveedorID)";
                addComm.Parameters.Clear();
                addComm.CommandText = qr;                
                addComm.Parameters.Add("@proveedorID", SqlDbType.Int).Value = int.Parse(this.ddlProveedor.SelectedValue);
                addComm.Parameters.Add("@boletaID", SqlDbType.Int).Value = newbol;
                addComm.ExecuteNonQuery();
                addComm.Parameters.Clear();
                addComm.CommandText="SELECT     max(entradaprodID) FROM  EntradaDeProductos";
                int entradaID = int.Parse(addComm.ExecuteScalar().ToString());
                if (dbFunctions.addOrden_EntradaPro(int.Parse(this.txtOrdenID.Text), int.Parse(this.ddlNewBoletaProducto.SelectedValue), pesotot, double.Parse(this.txtNewPrecio.Text), 0, false, entradaID, newbol))
                {
                    this.grdvProductosRecibidos.DataBind();
                }                
                this.txtNewNumBoleta.Text = "";
                this.txtNewTicket.Text = "";
                this.ddlNewBoletaProducto.SelectedIndex = 0;
                this.txtNewFechaEntrada.Text = this.txtNewFechaSalida.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.txtPesoNetoNewBoleta.Text = this.txtNewPesoEntrada.Text = this.txtNewPesoSalida.Text = "0";
                this.txtNewSecado.Text = this.txtNewHumedad.Text = this.txtNewImpurezas.Text = this.txtNewPrecio.Text = "0";
                this.ddlNewBoletaBodega.DataBind();
                this.ddlNewBoletaBodega.SelectedValue = this.BodegaID.ToString();

            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error agregando boleta de proveedor", this.Request.Url.ToString(), ref ex);
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "Error Insertando Nueva Boleta EX:" + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public String GetOrdenNavigationURL(String sorden)
        {
            String sQuery = "OrdenID=" + sorden;
            sQuery = Utils.GetEncriptedQueryString(sQuery);
            String strRedirect = "~/frmEntradaProductoProveedor.aspx";
            strRedirect += sQuery;
            return strRedirect;
        }

        protected void updateOrden(){



            string sqlQuery = "update [Orden_de_entrada] set proveedorID=@proveedorID ,cicloID=@cicloID,Fecha=@Fecha,userID=@userID,Folio=@Folio Where ordenID=@OrdenID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(sqlQuery, conGaribay);
            
            string sError = "";
            try
            {


                cmdIns.Parameters.Add("@proveedorID", SqlDbType.Int).Value = int.Parse(this.ddlProveedor.SelectedValue);
                cmdIns.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(this.ddlCiclos.SelectedValue);
                cmdIns.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = (DateTime)DateTime.Parse(this.txtFecha.Text);
                cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                cmdIns.Parameters.Add("@Folio", SqlDbType.NVarChar).Value = this.txtFolio.Text;
                cmdIns.Parameters.Add("@OrdenID", SqlDbType.Int).Value = int.Parse(this.txtOrdenID.Text);

                conGaribay.Open();
                int numregistros;
                numregistros = int.Parse(cmdIns.ExecuteNonQuery().ToString());
                if (numregistros <= 0)
                {
                    throw new Exception("AL TRATAR DE Modificar UNA ORDEN DE ENTRADA. LA BASE DE DATOS REGRESÓ QUE SE ALTERARAON " + numregistros.ToString() + " REGISTROS");
                }

                String Query = "OrdenID=" + this.txtOrdenID.Text;
                Query = Utils.GetEncriptedQueryString(Query);
                String strRedirect = "~/frmEntradaProductoProveedor.aspx" + Query;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.ENTRADAPRODUCTOS, Logger.typeUserActions.INSERT, " SE AGREGO LA ORDEN DE ENTRADA : " + this.txtFolio.Text);

                Response.Redirect(strRedirect);



            }
            catch (Exception exception)
            {
                sError = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, exception.Message, "EL ERROR SE DIÓ AL INGRESAR UNA ORDEN DE ENTRADA");


            }
            finally
            {
                conGaribay.Close();

            }


        }
    }
}

