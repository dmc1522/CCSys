using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

namespace Garibay
{

    public partial class frmBoletaNewQuick : Garibay.BasePage
    {
        public enum panelesHabilitar
        {
            TODOS = 0,
            NINGUNO,
            FACVENTA,
            NOTACOMPRA,
            NOTAVENTA
            
        }

        int iBoletaID, iliqID, iproveedorID, iNotaCompraID;

        internal void AddJSInControls()
        {
            String sOnchangedeResta = "; RestaYDivide('";
            sOnchangedeResta += this.txtImporte.ClientID + "','";
            sOnchangedeResta += this.txtImporteFlete.ClientID + "','";
            sOnchangedeResta += this.txtPesoNetoNewBoleta.ClientID + "','";
            sOnchangedeResta += this.txtPrecioNeto.ClientID + "')";

            String sOnchangeMerma = "; subTextBoxes('";
            sOnchangeMerma += this.txtPesoDestino.ClientID + "','";
            sOnchangeMerma += this.txtPesoNetoNewBoleta.ClientID + "','";
            sOnchangeMerma += this.txtMerma.ClientID + "')";

            String sOnchange = "ShowHideDivOnChkBox('";
            sOnchange += this.chkChangeFechaSalidaNewBoleta.ClientID + "','";
            sOnchange += this.divFechaSalidaNewBoleta.ClientID + "')";
            this.chkChangeFechaSalidaNewBoleta.Attributes.Add("onclick", sOnchange);

            sOnchange = "subTextBoxes('";
            sOnchange += this.txtNewPesoEntrada.ClientID + "','";
            sOnchange += this.txtNewPesoSalida.ClientID + "','";
            sOnchange += this.txtPesoNetoNewBoleta.ClientID + "')";
            this.txtNewPesoEntrada.Attributes.Add("onKeyUp", sOnchange + sOnchangedeResta + sOnchangeMerma);
            this.txtNewPesoEntrada.Attributes.Add("onBlur", sOnchange + sOnchangedeResta + sOnchangeMerma);
            this.txtNewPesoSalida.Attributes.Add("onKeyUp", sOnchange + sOnchangedeResta + sOnchangeMerma);
            this.txtNewPesoSalida.Attributes.Add("onBlur", sOnchange + sOnchangedeResta + sOnchangeMerma);

          
            sOnchange = "mulTextBoxesNotNeg('";
            sOnchange += this.txtPesoDestino.ClientID + "','";
            sOnchange += this.txtNewPrecio.ClientID + "','";
            sOnchange += this.txtImporte.ClientID + "')";
            this.txtPesoDestino.Attributes.Add("onKeyUp", sOnchange + sOnchangedeResta + sOnchangeMerma);
            this.txtPesoDestino.Attributes.Add("onBlur", sOnchange + sOnchangedeResta + sOnchangeMerma);
            this.txtNewPrecio.Attributes.Add("onKeyUp", sOnchange + sOnchangedeResta);
            this.txtNewPrecio.Attributes.Add("onBlur", sOnchange + sOnchangedeResta);


            sOnchange = "mulTextBoxesNotNeg('";
            sOnchange += this.txtPesoNetoNewBoleta.ClientID + "','";
            sOnchange += this.txtFlete.ClientID + "','";
            sOnchange += this.txtImporteFlete.ClientID + "')";
            this.txtPesoNetoNewBoleta.Attributes.Add("onKeyUp", sOnchange + sOnchangedeResta);
            this.txtPesoNetoNewBoleta.Attributes.Add("onBlur", sOnchange+ sOnchangedeResta);
            this.txtFlete.Attributes.Add("onKeyUp", sOnchange + sOnchangedeResta);
            this.txtFlete.Attributes.Add("onBlur", sOnchange + sOnchangedeResta);

          

            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(!this.IsPostBack)
            {
                this.pnlDatosBoletaSalida.Visible = false;
                this.drpdlCiclo.DataBind();
                this.drpdlCiclo.SelectedIndex = 0;
                this.txtNewFechaEntrada.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.AddJSInControls();
                this.btnModificar.Visible = false;
            
                iBoletaID = -1;
                iliqID=-1;
                //this.rbProductor.Checked = true;
                if (Request.QueryString["data"] != null)
                {
                  
                    if (this.loadqueryStrings(Request.QueryString["data"].ToString()))
                    {
                        if (this.myQueryStrings["boletaID"] != null && int.TryParse(this.myQueryStrings["boletaID"].ToString(), out iBoletaID) && iBoletaID > -1) //CHECAMOS SI ES PARA MODIFICAR
                        {
                            this.lblNumBoleta.Text = iBoletaID.ToString();
                            if (this.cargadatosmodify())
                            {
                                this.btnAgregar.Visible = false;
                                this.btnModificar.Visible = true;
                                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.LIQUIDACIONES, Logger.typeUserActions.SELECT, this.UserID, "ABRIÓ LA BOLETA NÚMERO." + this.lblNumBoleta.Text);
                            }
                            else
                            {
                                Response.Write("<script type=\"text/javascript\">window.close();</script>");
                            }
                        }
                        if (this.myQueryStrings["liqID"] != null && int.TryParse(this.myQueryStrings["liqID"].ToString(), out iliqID) && iliqID > -1) //CHECAMOS SI ES PARA AGREGAR A UNA LIQ
                        {
                                this.lblLiqID.Text = iliqID.ToString();
                                this.txtNewFechaEntrada.Text = Utils.Now.ToString("dd/MM/yyyy");
                                this.txtNewFechaEntrada.Text = Utils.Now.ToString("dd/MM/yyyy");
                        }
                        int FacturaID = -1, iclienteVentaID = -1;
                        if (this.myQueryStrings["FacturaID"] != null && int.TryParse(this.myQueryStrings["FacturaID"].ToString(), out FacturaID) && this.myQueryStrings["clienteventaID"]!=null && int.TryParse(this.myQueryStrings["clienteventaID"].ToString(),out iclienteVentaID))
                        {
                            this.habilitaPaneles(panelesHabilitar.FACVENTA, FacturaID, iclienteVentaID);
                            this.pnlDatosBoletaSalida.Visible = true;
                           
                        }
                        int iNotaVentaID = -1, iProductorID = -1;
                        if (this.myQueryStrings["notaventaID"] != null && int.TryParse(this.myQueryStrings["notaventaID"].ToString(), out iNotaVentaID) && this.myQueryStrings["productorID"] != null && int.TryParse(this.myQueryStrings["productorID"].ToString(), out iProductorID))
                        {
                            this.habilitaPaneles(panelesHabilitar.NOTAVENTA, iNotaVentaID, iProductorID);

                        }
                        if (this.myQueryStrings["notacompraID"] != null && int.TryParse(this.myQueryStrings["notacompraID"].ToString(), out iNotaCompraID) && this.myQueryStrings["proveedorID"]!=null && int.TryParse(this.myQueryStrings["proveedorID"].ToString(),out iproveedorID))
                        {
                            this.lblNotaCompraID.Text = iNotaCompraID.ToString();

                            this.habilitaPaneles(panelesHabilitar.NOTACOMPRA, iNotaCompraID, iproveedorID);
                            
                           

                        }
                    }
                    else
                    {
                        Response.Write("<script type=\"text/javascript\">window.close();</script>");
                    }
                }
                else
                {
                    this.txtNewFechaEntrada.Text = Utils.Now.ToString("dd/MM/yyyy");
                    this.txtNewFechaEntrada.Text = Utils.Now.ToString("dd/MM/yyyy");
                    this.ddlNewBoletaProductor.DataBind();
                    this.drpdlCiclo.DataBind();
                    this.ddlNewBoletaProducto.DataBind();
                    this.ddlNewBoletaBodega.DataBind();

                    this.btnAgregar.Visible = true;
                    this.btnModificar.Visible = false;
                }
            }
            this.pnlNewBoleta.Visible = false;
            this.divFechaSalidaNewBoleta.Attributes.Add("style", this.chkChangeFechaSalidaNewBoleta.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");

        }
        private bool cargadatosmodify()
        {
            bool bResult = false;
            string qryIns = " SELECT     Boletas.cicloID, Boletas.productorID, Boletas.productoID, Boletas.bodegaID, Boletas.NumeroBoleta, Boletas.Ticket, Boletas.FechaEntrada, Boletas.PesoDeEntrada, "+
                      " Boletas.FechaSalida, Boletas.PesoDeSalida, Boletas.humedad, Boletas.impurezas, Boletas.precioapagar, Boletas.dctoSecado, Boletas.chofer, Boletas.placas,  "+
                      "  FacturasCV_Boletas.FacturaCV, FacturasClientesVenta.clienteVentaID, Boletas_NotasDeCompra.notadecompraID, NotasDeCompra.proveedorID , Boletas.transportistaID" + 
                      " FROM         FacturasCV_Boletas INNER JOIN " +
                      " FacturasClientesVenta ON FacturasCV_Boletas.FacturaCV = FacturasClientesVenta.FacturaCV RIGHT OUTER JOIN "+
                      " Boletas_NotasDeCompra INNER JOIN "+
                      " NotasDeCompra ON Boletas_NotasDeCompra.notadecompraID = NotasDeCompra.notadecompraID RIGHT OUTER JOIN " +
                      "  Boletas ON Boletas_NotasDeCompra.boletaID = Boletas.boletaID ON FacturasCV_Boletas.boletaID = Boletas.boletaID " +
                      "  WHERE     (Boletas.boletaID = @boletaID)";

            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            conGaribay.Open();
            try
            {
                cmdIns.Parameters.Clear();
                cmdIns.Parameters.Add("@boletaID", SqlDbType.Int).Value = iBoletaID;

                SqlDataReader datostomodify;
                datostomodify = cmdIns.ExecuteReader();

                if (datostomodify.HasRows && datostomodify.Read())
                {
                    this.ddlNewBoletaBodega.DataBind();
                    this.ddlNewBoletaProducto.DataBind();
                    this.drpdlCiclo.DataBind();
                    this.ddlNewBoletaProductor.DataBind();
                    this.drpdlCiclo.SelectedValue = datostomodify["cicloID"].ToString();
                    this.ddlNewBoletaProductor.SelectedValue = datostomodify["productorID"].ToString();
                    int iprodID = -1;
                    if (datostomodify["productorID"]!= null && int.TryParse(datostomodify["productorID"].ToString(), out iprodID) && iprodID != -1)
                    {
                        this.ddlNewBoletaProductor.SelectedValue = datostomodify["productorID"].ToString();
                        this.rbClienteVenta.Checked = false;
                        this.rbProductor.Checked = true;
                       
                       
                    }
                    else
                    {
                        if (datostomodify["FacturaCV"] != null && datostomodify["FacturaCV"].ToString() != "" && datostomodify["clienteventaID"] != null && datostomodify["clienteventaID"].ToString() != "")
                        {
                            this.habilitaPaneles(panelesHabilitar.FACVENTA, int.Parse(datostomodify["FacturaCV"].ToString()),int.Parse(datostomodify["clienteventaID"].ToString()));
                        }
                        else{
                            if (datostomodify["proveedorID"] != null && datostomodify["proveedorID"].ToString() != "" && datostomodify["notadecompraID"] != null && datostomodify["notadecompraID"].ToString() != "")
                            {
                                this.habilitaPaneles(panelesHabilitar.NOTACOMPRA, int.Parse(datostomodify["notadecompraID"].ToString()), int.Parse(datostomodify["proveedorID"].ToString()));
                            }
                        }
                        
                        
                    }
                    this.ddlNewBoletaBodega.SelectedValue = datostomodify["bodegaID"].ToString();
                    this.txtNewNumBoleta.Text = datostomodify["NumeroBoleta"].ToString();
                    this.txtNewTicket.Text = datostomodify["Ticket"].ToString();
                    this.txtNewFechaEntrada.Text = Utils.converttoshortFormatfromdbFormat(datostomodify["FechaEntrada"].ToString());
                    //this.txtNewHoraEntrada.Text = DateTime.Parse(this.txtNewFechaEntrada.Text).ToString("hh:mm:ss");
                    this.txtNewPesoEntrada.Text = double.Parse(datostomodify["PesoDeEntrada"].ToString()).ToString();
                    this.txtNewFechaSalida.Text = Utils.converttoshortFormatfromdbFormat(datostomodify["FechaSalida"].ToString());
                  //  this.txtNewHoraSalida.Text = DateTime.Parse(this.txtNewFechaSalida.Text).ToString("hh:mm:ss");
                    this.txtNewPesoSalida.Text = datostomodify["PesoDeSalida"].ToString().Length > 0 ? double.Parse(datostomodify["PesoDeSalida"].ToString()).ToString() : "0.00";
                    this.txtNewHumedad.Text = datostomodify["humedad"].ToString();
                    this.txtNewImpurezas.Text = datostomodify["impurezas"].ToString();
                    this.txtNewPrecio.Text = datostomodify["precioapagar"].ToString().Length > 0 ? double.Parse(datostomodify["precioapagar"].ToString()).ToString() : "0.00";
                    this.txtNewSecado.Text = datostomodify["dctoSecado"].ToString().Length > 0 ? double.Parse(datostomodify["dctoSecado"].ToString()).ToString() : "0.00";
                    this.txtPlacas.Text = datostomodify["placas"].ToString();
                    this.txtChofer.Text = datostomodify["chofer"].ToString();

                    int itransportistaID = -1;
                    if (int.TryParse(datostomodify["transportistaID"].ToString(), out itransportistaID))
                    {
                        this.ddlTransportista.DataBind();
                        this.ddlTransportista.SelectedValue = itransportistaID.ToString();
                    }
                    bResult = true;
                }
            }
            catch( Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "opening quick BoletaID = " + iBoletaID.ToString(), ref ex);
                bResult = false;
            }
            finally
            {
                conGaribay.Close();
            }
            return bResult;

        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
           // start = new TimeSpan(Utils.Now.Ticks);
            Boolean salir = false;
            int boletaagregadaid = 0;
            if (dbFunctions.BoletaAlreadyExists(-1, this.txtNewNumBoleta.Text, this.txtNewTicket.Text))
            {
                this.pnlNewBoleta.Visible = true;
                this.lblNewBoletaResult.Text = "NO SE PUEDE AGREGAR LA BOLETA, YA SE ENCUENTRA EN EL SISTEMA";
                this.imgBien.Visible = false;
                this.imgMal.Visible = true;
                return;
            }
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            SqlConnection addConn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                sqlConn.Open();
                dsBoletas.dtBoletasDataTable dtTempBoletas = new dsBoletas.dtBoletasDataTable();
                dsBoletas.dtBoletasRow newRow = dtTempBoletas.NewdtBoletasRow(); 

                newRow.productorID = this.rbProductor.Checked ? int.Parse(this.ddlNewBoletaProductor.SelectedValue): -1;
                newRow.NombreProductor = this.ddlNewBoletaProductor.SelectedItem.Text.ToUpper();
                newRow.NumeroBoleta = this.txtNewNumBoleta.Text;
                newRow.Ticket = this.txtNewTicket.Text;
                newRow.bodegaID = int.Parse(this.ddlNewBoletaBodega.SelectedItem.Value);
                newRow.bodega = this.ddlNewBoletaBodega.SelectedItem.Text.ToUpper();
                newRow.applySecado = newRow.applyImpurezas = newRow.applyHumedad = true;

                newRow.chofer = this.txtChofer.Text;
                newRow.Placas = this.txtPlacas.Text;

                newRow.cicloID = int.Parse(this.drpdlCiclo.SelectedItem.Value);

                DateTime dtFechaEntrada = new DateTime();
                if (!DateTime.TryParse(this.txtNewFechaEntrada.Text, out dtFechaEntrada))
                {
                    dtFechaEntrada = Utils.Now;
                }
                newRow.FechaEntrada = dtFechaEntrada;
                double dPesoEntrada = 0;
                double.TryParse(this.txtNewPesoEntrada.Text, out dPesoEntrada);
                newRow.PesoDeEntrada = dPesoEntrada;


                DateTime dtFechaSalida = new DateTime();
                if (this.chkChangeFechaSalidaNewBoleta.Checked)
                {
                    if (!DateTime.TryParse(this.txtNewFechaSalida.Text, out dtFechaSalida))
                    {
                        //DateTime.TryParse(this.txtNewFechaSalida.Text, out dtFechaSalida);
                        dtFechaSalida = Utils.Now;
                    }
                }
                else
                    dtFechaSalida = dtFechaEntrada;
                newRow.FechaSalida = dtFechaSalida;
                double dPesoSalida = 0;
                double.TryParse(this.txtNewPesoSalida.Text, out dPesoSalida);
                newRow.PesoDeSalida = dPesoSalida;

                if (newRow.PesoDeEntrada - newRow.PesoDeSalida > 0)
                {
                    newRow.pesonetoentrada = newRow.PesoDeEntrada - newRow.PesoDeSalida;
                    newRow.pesonetosalida = 0;
                }
                else
                {
                    newRow.pesonetoentrada = 0;
                    newRow.pesonetosalida = newRow.PesoDeSalida - newRow.PesoDeEntrada;
                }


                newRow.productoID = int.Parse(this.ddlNewBoletaProducto.SelectedValue);
                newRow.Producto = this.ddlNewBoletaProducto.SelectedItem.Text.ToUpper();

                double dHumedad = 0;
                double.TryParse(this.txtNewHumedad.Text, out dHumedad);
                newRow.humedad = dHumedad;
                double dImpurezas = 0;
                double.TryParse(this.txtNewImpurezas.Text, out dImpurezas);
                newRow.impurezas = dImpurezas;
                decimal dPrecio = 0;
                decimal.TryParse(this.txtNewPrecio.Text, out dPrecio);
                newRow.precioapagar = dPrecio;
                double dSecado = 0;
                double.TryParse(this.txtNewSecado.Text, out dSecado);
                newRow.dctoSecado = dSecado;


                newRow.applyHumedad = newRow.applyImpurezas = newRow.applySecado = true;
                newRow.dctoHumedad      = Utils.getDesctoHumedad(newRow.humedad, newRow.pesonetoentrada, this.rbClienteVenta.Checked);
                newRow.dctoImpurezas    = decimal.Parse(Utils.getDesctoImpurezas(newRow.impurezas, newRow.pesonetoentrada, this.rbClienteVenta.Checked).ToString());
                newRow.dctoSecado       = Utils.getDesctoSecado(newRow.humedad, newRow.pesonetoentrada, this.rbClienteVenta.Checked);
               
                newRow.userID = int.Parse(this.Session["USERID"].ToString());

                dtTempBoletas.AdddtBoletasRow(newRow);

                
                SqlCommand addComm = new SqlCommand();
                addComm.Connection = addConn;
                addConn.Open();

                addComm.CommandText = "INSERT INTO Boletas (productorID, NombreProductor, NumeroBoleta, Ticket, bodegaID, cicloID, FechaEntrada, PesoDeEntrada, FechaSalida, PesoDeSalida, pesonetoentrada,  pesonetosalida, productoID, humedad, impurezas, precioapagar, dctoSecado, userID, chofer, placas) VALUES     (@productorID,@NombreProductor,@NumeroBoleta,@Ticket,@bodegaID,@cicloID,@FechaEntrada,@PesoDeEntrada,@FechaSalida,@PesoDeSalida,@pesonetoentrada,@pesonetosalida,@productoID,@humedad,@impurezas,@precioapagar,@dctoSecado,@userID, @chofer, @placas); select boletaID = SCOPE_IDENTITY();";



                addComm.Parameters.Add("@productorID", SqlDbType.Int).Value = newRow.productorID;
                // addComm.Parameters.AddWithValue("@productorID", newRow.productorID);
                addComm.Parameters.Add("@NombreProductor", SqlDbType.VarChar).Value = newRow.NombreProductor;
                //addComm.Parameters.AddWithValue("@NombreProductor", newRow.NombreProductor);
                addComm.Parameters.Add("@NumeroBoleta", SqlDbType.VarChar).Value = newRow.NumeroBoleta;
                //addComm.Parameters.AddWithValue("@NumeroBoleta", newRow.NumeroBoleta);
                //  addComm.Parameters.AddWithValue("@Ticket", newRow.Ticket);
                addComm.Parameters.Add("@Ticket", SqlDbType.VarChar).Value = newRow.Ticket;
                //  addComm.Parameters.AddWithValue("@bodegaID", newRow.bodegaID);
                addComm.Parameters.Add("@bodegaID", SqlDbType.Int).Value = newRow.bodegaID;
                //  addComm.Parameters.AddWithValue("@cicloID", newRow.cicloID);
                addComm.Parameters.Add("@cicloID", SqlDbType.Int).Value = newRow.cicloID;
                //  addComm.Parameters.AddWithValue("@FechaEntrada", newRow.FechaEntrada);
                addComm.Parameters.Add("@FechaEntrada", SqlDbType.DateTime).Value = newRow.FechaEntrada;
                addComm.Parameters.Add("@PesoDeEntrada", SqlDbType.Float).Value = (float)newRow.PesoDeEntrada;
                addComm.Parameters.Add("@FechaSalida", SqlDbType.DateTime).Value = newRow.FechaSalida;
                // addComm.Parameters.AddWithValue("@FechaSalida", newRow.FechaSalida);
                addComm.Parameters.Add("@PesoDeSalida", SqlDbType.Float).Value = newRow.PesoDeSalida;
                addComm.Parameters.Add("@pesonetoentrada", SqlDbType.Float).Value = (float)newRow.pesonetoentrada;
                addComm.Parameters.Add("@pesonetosalida", SqlDbType.Float).Value = (float)newRow.pesonetosalida;
                // addComm.Parameters.AddWithValue("@productoID", newRow.productoID);
                addComm.Parameters.Add("@productoID", SqlDbType.Int).Value = newRow.productoID;

                addComm.Parameters.Add("@humedad", SqlDbType.Float).Value = newRow.humedad;
                addComm.Parameters.Add("@impurezas", SqlDbType.Float).Value = newRow.impurezas;
                addComm.Parameters.Add("@precioapagar", SqlDbType.Float).Value = newRow.precioapagar;
                addComm.Parameters.Add("@dctoSecado", SqlDbType.Float).Value = newRow.dctoSecado;
                //addComm.Parameters.AddWithValue("@userID", newRow.userID);
                addComm.Parameters.Add("@userID", SqlDbType.Int).Value = newRow.userID;
                addComm.Parameters.Add("@chofer", SqlDbType.VarChar).Value = newRow.chofer;
                addComm.Parameters.Add("@placas", SqlDbType.VarChar).Value = newRow.Placas;

                newRow.boletaID = int.Parse(addComm.ExecuteScalar().ToString());
                
                boletaagregadaid = newRow.boletaID;

                this.UpdateTransportista(newRow.boletaID);

                this.txtNewNumBoleta.Text = "";
                this.txtNewTicket.Text = "";
                this.ddlNewBoletaProducto.SelectedIndex = 0;
                this.txtNewFechaEntrada.Text = this.txtNewFechaSalida.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.txtPesoNetoNewBoleta.Text = this.txtNewPesoEntrada.Text = this.txtNewPesoSalida.Text = "0";
                this.txtNewSecado.Text = this.txtNewHumedad.Text = this.txtNewImpurezas.Text = this.txtNewPrecio.Text = "0";
                this.ddlNewBoletaBodega.DataBind();
                if(this.BodegaID.ToString()!="-1")
                    this.ddlNewBoletaBodega.SelectedValue = this.BodegaID.ToString();
                this.lblNewBoletaResult.Text = "BOLETA AGREGADA EXITOSAMENTE";
                this.pnlNewBoleta.Visible = true;
                this.imgBien.Visible = true;
                this.btnAgregar.Visible = false;
                this.btnModificar.Visible = false;
                this.imgMal.Visible = false;
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.BOLETAS, Logger.typeUserActions.INSERT, this.UserID, "AGREGÓ LA BOLETA CON EL ID: " + newRow.boletaID + ". DESDE BOLETANEWQUICK.");
                //AGREGAMOS LA BOLETA A LA TABLA DE RELACION CON CLIENTE DE VENTA
                if (this.rbClienteVenta.Checked)
                { 
                    //UPDATE BOLETASSALIDA
                    salir = true;
                    this.updateBoletadatosSalida(newRow.boletaID);
                    this.lblNewBoletaResult.Text += " AL CLIENTE DE VENTA ";
                    this.lblNewBoletaResult.Text += this.ddlClientes.SelectedItem.Text.ToUpper() + ". ";
                    
                    addComm.CommandText = "insert into ClienteVenta_Boletas(clienteventaID, BoletaID) Values(@clienteID, @bolID)";
                    addComm.Parameters.Clear();
                    addComm.Parameters.Add("@clienteID", SqlDbType.Int).Value = int.Parse(this.ddlClientes.SelectedValue);
                    addComm.Parameters.Add("@bolID", SqlDbType.Int).Value = (newRow.boletaID);
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.BOLETAS, Logger.typeUserActions.INSERT, this.UserID, "AGREGÓ LA BOLETA CON EL ID: " + newRow.boletaID + ". DESDE BOLETANEWQUICK. AL CLIENTE DE VENTA: " + this.ddlClientes.SelectedItem.Text.ToUpper());
                    addComm.ExecuteNonQuery();
                }
                
              
                //CHECAMOS SI TENEMOS UNA LIQ ID
                int liq;
                if(int.TryParse(this.lblLiqID.Text,out liq))
                {//CHECAMOS SI RECIBIMOS UNA LIQID Y METEMOS A LA TABLA LIQ_BOLETAS
                    addComm.CommandText = "insert into Liquidaciones_Boletas(LiquidacionID, BoletaID) Values(@liqID, @bolID)";
                    addComm.Parameters.Clear();
                    addComm.Parameters.Add("@liqID", SqlDbType.Int).Value = liq;
                    addComm.Parameters.Add("@bolID", SqlDbType.Int).Value = (newRow.boletaID);
                    addComm.ExecuteNonQuery();
                    this.lblNewBoletaResult.Text += " A LA LIQUIDACION ";
                    this.lblNewBoletaResult.Text += this.lblLiqID.Text.ToString(); this.lblNewBoletaResult.Text += ". ";
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.LIQUIDACIONES, Logger.typeUserActions.UPDATE, this.UserID, "AGREGÓ LA BOLETA CON EL ID: " + newRow.boletaID + ". DESDE BOLETANEWQUICK. A LA LIQUIDACION " + this.lblLiqID.ToString() + ".");
                    salir = true;
                }
                if (this.chkFacturaClienteVenta.Checked)
                {
                    salir = true;
                    SqlConnection connFactura = new SqlConnection(myConfig.ConnectionInfo);
                    try
                    {
                        connFactura.Open();
                        SqlCommand commFactura = new SqlCommand();
                        commFactura.Connection = connFactura;
                        commFactura.CommandText = "insert into FacturasCV_Boletas values(@FacturaCV,@boletaID);";
                        int FacturaID = -1;
                        int.TryParse(this.ddlFacturaClienteVenta.SelectedValue, out FacturaID);
                        commFactura.Parameters.Add("@FacturaCV", SqlDbType.Int).Value = FacturaID;
                        commFactura.Parameters.Add("@boletaID", SqlDbType.Int).Value = newRow.boletaID;
                        if (commFactura.ExecuteNonQuery() != 1)
                        {
                            throw new Exception("error en insert into FacturasCV_Boletas values(@FacturaCV,@boletaID) factura: " + FacturaID.ToString() + " boletaid: "+ newRow.boletaID);
                        }
                        this.lblNewBoletaResult.Text += " Y A LA FACTURA DE VENTA: ";
                        this.lblNewBoletaResult.Text += this.ddlFacturaClienteVenta.SelectedItem.Text.ToUpper() + ". ";
                    
                        Logger.Instance.LogUserSessionRecord(Logger.typeModulo.BOLETAS, Logger.typeUserActions.INSERT, this.UserID, "AGREGÓ LA BOLETA CON EL ID: " + newRow.boletaID + ". DESDE BOLETANEWQUICK. A LA FACTURA DE VENTA: " + FacturaID.ToString());
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.INSERT, "ingresando Boleta -> Factura", ref ex);
                    }
                    finally
                    {
                        connFactura.Close();
                    }
                }
                if (this.chkBoxNotadeVenta.Checked)
                {
                    SqlConnection connBoletaProd = new SqlConnection(myConfig.ConnectionInfo);
                    salir = true;
                    try
                    {
                        connBoletaProd.Open();
                        SqlCommand commNotadeVenta = new SqlCommand();
                        commNotadeVenta.Connection = connBoletaProd;
                        commNotadeVenta.CommandText = "insert into NotasdeVenta_boletas(boletaID, notadeventaID) values(@boletaID,@notadeventaID);";
                        commNotadeVenta.Parameters.Add("@boletaID", SqlDbType.Int).Value = newRow.boletaID;
                        commNotadeVenta.Parameters.Add("@notadeventaID", SqlDbType.Int).Value = int.Parse(this.drpdlNotadeVenta.SelectedValue);
                        if (commNotadeVenta.ExecuteNonQuery() != 1)
                        {
                            throw new Exception("error en insert into notadeventa_boletas values(@boletaID,@notadeventaID) boleta: " + newRow.boletaID.ToString() + " not: " + this.drpdlProveedor.SelectedValue);
                        }
                        this.lblNewBoletaResult.Text += " AL PRODUCTOR: ";
                        this.lblNewBoletaResult.Text += this.ddlNewBoletaProductor.SelectedItem.Text.ToUpper() + ". Y A LA NOTA DE VENTA: " + this.drpdlNotadeVenta.SelectedItem.Text.ToUpper();
                        Logger.Instance.LogUserSessionRecord(Logger.typeModulo.BOLETAS, Logger.typeUserActions.INSERT, this.UserID, "AGREGÓ LA BOLETA CON EL ID: " + newRow.boletaID + ". DESDE BOLETANEWQUICK. AL PRODUCTOR: " + this.ddlNewBoletaProductor.SelectedItem.Text.ToUpper() + ". Y A LA NOTA DE VENTA: " + this.drpdlNotadeVenta.SelectedItem.Text.ToUpper());
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.INSERT, "ingresando Boleta -> Productor - Nota de Venta", ref ex);
                    }
                    finally
                    {
                        connBoletaProd.Close();
                    }
                }
                if (this.rbProveedor.Checked)
                {
                    SqlConnection connBoletaProv = new SqlConnection(myConfig.ConnectionInfo);
                    salir = true;
                    try
                    {
                        connBoletaProv.Open();
                        SqlCommand commFactura = new SqlCommand();
                        commFactura.Connection = connBoletaProv;
                        commFactura.CommandText = "insert into boleta_proveedor values(@boletaID,@proveedorID);";


                        commFactura.Parameters.Add("@boletaID", SqlDbType.Int).Value = newRow.boletaID;
                        commFactura.Parameters.Add("@proveedorID", SqlDbType.Int).Value = int.Parse(this.drpdlProveedor.SelectedValue);
                        if (commFactura.ExecuteNonQuery() != 1)
                        {
                            throw new Exception("error en insert into boletas_proveedor values(@boletaID,@proveedorID) boleta: " + newRow.boletaID.ToString() + " proveedorID: " + this.drpdlProveedor.SelectedValue);
                        }
                        this.lblNewBoletaResult.Text += " AL PROVEEDOR: ";
                        this.lblNewBoletaResult.Text += this.drpdlProveedor.SelectedItem.Text.ToUpper() + ". ";
                        Logger.Instance.LogUserSessionRecord(Logger.typeModulo.BOLETAS, Logger.typeUserActions.INSERT, this.UserID, "AGREGÓ LA BOLETA CON EL ID: " + newRow.boletaID + ". DESDE BOLETANEWQUICK. AL PROVEEDOR: " + this.drpdlProveedor.SelectedItem.Text.ToUpper());
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.INSERT, "ingresando Boleta -> Proveedor", ref ex);
                    }
                    finally
                    {
                        connBoletaProv.Close();
                    }
                }
                if (this.chkBoxNotadeCompra.Checked)
                {
                    salir = true;
                    SqlConnection connNotaCompra = new SqlConnection(myConfig.ConnectionInfo);
                    try
                    {
                        connNotaCompra.Open();
                        SqlCommand commNotaCompra = new SqlCommand();
                        commNotaCompra.Connection = connNotaCompra;
                        commNotaCompra.CommandText = "insert into Boletas_NotasDeCompra values(@notadecompraID,@boletaID);";


                        commNotaCompra.Parameters.Add("@boletaID", SqlDbType.Int).Value = newRow.boletaID;
                        commNotaCompra.Parameters.Add("@notadecompraID", SqlDbType.Int).Value = int.Parse(this.lblNotaCompraID.Text);
                        if (commNotaCompra.ExecuteNonQuery() != 1)
                        {
                            throw new Exception("error en insert into boletas_notadecompra values(@boletaID,@notadecompraID) boleta: " + newRow.boletaID.ToString() + " notadecompra: " + this.lblNotaCompraID.Text);
                        }
                        this.lblNewBoletaResult.Text += " Y A LA NOTA DE COMPRA: ";
                        this.lblNewBoletaResult.Text += this.drpdlNotadeCompra.SelectedItem.Text.ToUpper() + ". ";
                        
                        Logger.Instance.LogUserSessionRecord(Logger.typeModulo.BOLETAS, Logger.typeUserActions.INSERT, this.UserID, "AGREGÓ LA BOLETA CON EL ID: " + newRow.boletaID + ". DESDE BOLETANEWQUICK. A LA NOTA DE COMPRA: " + this.lblNotaCompraID.Text);
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.INSERT, "ingresando Boleta -> Nota de Compra", ref ex);
                    }
                    finally
                    {
                        connNotaCompra.Close();
                    }
                }
            }
            catch (System.Exception ex)
            {
                this.lblNewBoletaResult.Text = "LA BOLETA NO HA PODIDO SER AGREGADA. EX: "+ ex.Message;
                this.imgBien.Visible = false;
                this.imgMal.Visible = true;
                this.pnlNewBoleta.Visible = true;
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error agregando boleta desde AddNewBoletaQuick. ", this.Request.Url.ToString(), ref ex);
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(Session["USERID"].ToString()), "Error Insertando Nueva Boleta desde AddNewBoletaQuick EX:" + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                sqlConn.Close();
                addConn.Close();
                if (!salir)
                {
                    String sNewUrl = "~/frmBoletaNewQuick.aspx?data=";
                    sNewUrl += Utils.encriptacadena("boletaID=" + boletaagregadaid.ToString() + "&");
                    Response.Redirect(sNewUrl);
                }
                this.btnAgregar.Visible = false;
                this.btnModificar.Visible = false;
            }
        }

        protected void updateBoletadatosSalida(int id){
            SqlConnection conUpdate = new SqlConnection(myConfig.ConnectionInfo);
            String strUpdate = "update Boletas set FolioDestino = @folioDestino, PesoDestino = @pesoDestiono, Merma = @merma, Flete = @flete, ";
            strUpdate += "ImporteFlete = @importeFlete, PrecioNetoDestino = @precioNetoDestino, dctoGranoChico = @dctoGranoChico, ";
            strUpdate += " dctoGranoDanado = @dctoGranoDanado, dctoGranoQuebrado = @dctoGranoQuebrado, dctoGranoEstrellado = @dctoGranoEstrellado where";
            strUpdate += " boletaID = @boletaID";


            SqlCommand cmdUpdate = new SqlCommand(strUpdate,conUpdate);
            conUpdate.Open();
            try
            {
                cmdUpdate.Parameters.Clear();
                double datoaux;
                cmdUpdate.Parameters.Add("@FolioDestino", SqlDbType.Text).Value = this.txtFolioDestino.Text;
                datoaux = 0.00;  double.TryParse(this.txtFolioDestino.Text, out datoaux);
                cmdUpdate.Parameters.Add("@pesoDestiono", SqlDbType.Float).Value = Utils.GetSafeFloat(this.txtPesoDestino.Text);
                datoaux = 0.00; double.TryParse(this.txtPesoDestino.Text, out datoaux);
                cmdUpdate.Parameters.Add("@merma", SqlDbType.Float).Value = Utils.GetSafeFloat(this.txtMerma.Text);
                datoaux = 0.00; double.TryParse(this.txtMerma.Text, out datoaux);
                cmdUpdate.Parameters.Add("@flete", SqlDbType.Float).Value = Utils.GetSafeFloat(this.txtFlete.Text);
                datoaux = 0.00; double.TryParse(this.txtFlete.Text, out datoaux);
                cmdUpdate.Parameters.Add("@importeFlete", SqlDbType.Float).Value = Utils.GetSafeFloat(this.txtImporteFlete.Text);
                datoaux = 0.00; double.TryParse(this.txtImporteFlete.Text, out datoaux);
                cmdUpdate.Parameters.Add("@precioNetoDestino", SqlDbType.Float).Value = Utils.GetSafeFloat(this.txtPrecioNeto.Text);
                datoaux = 0.00; double.TryParse(this.txtPrecioNeto.Text, out datoaux);
                cmdUpdate.Parameters.Add("@dctoGranoChico", SqlDbType.Float).Value = Utils.GetSafeFloat(this.txtGranoChico.Text);
                datoaux = 0.00; double.TryParse(this.txtGranoChico.Text, out datoaux);
                cmdUpdate.Parameters.Add("@dctoGranoDanado", SqlDbType.Float).Value = Utils.GetSafeFloat(this.txtGranoDanado.Text);
                datoaux = 0.00; double.TryParse(this.txtGranoQuebrado.Text, out datoaux);
                cmdUpdate.Parameters.Add("@dctoGranoQuebrado", SqlDbType.Float).Value = Utils.GetSafeFloat(this.txtGranoQuebrado.Text);
                datoaux = 0.00; double.TryParse(this.txtGranoEstrellado.Text, out datoaux);
                cmdUpdate.Parameters.Add("@dctoGranoEstrellado", SqlDbType.Float).Value = Utils.GetSafeFloat(this.txtGranoEstrellado.Text);
                cmdUpdate.Parameters.Add("@boletaID", SqlDbType.Int).Value = id;
                cmdUpdate.ExecuteNonQuery();
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.BOLETAS, Logger.typeUserActions.UPDATE, this.UserID, "SE ACTUALIZARON DATOS DE BOLETA DE SALIDA CON EL ID: " + id.ToString() + ".");
            }
            catch(Exception ex)
            {
                this.lblNewBoletaResult.Text = "LA BOLETA NO HA PODIDO SER AGREGADA";
                this.imgBien.Visible = false;
                this.imgMal.Visible = true;
                this.pnlNewBoleta.Visible = true;
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error modificando boleta desde AddNewBoletaQuick. ", this.Request.Url.ToString(), ref ex);
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(Session["USERID"].ToString()), "Error modificando boleta de salida. EX:" + ex.Message, this.Request.Url.ToString());
                //Logger.Instance.LogUserSessionRecord(Logger.typeModulo.BOLETAS, Logger.typeUserActions.UPDATE, this.UserID, "SE ACTUALIZARON DATOS DE BOLETA DE SALIDA CON EL ID: " + id.ToString() + ".");
              

            }
            finally{
                conUpdate.Close();
            }

        }
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                sqlConn.Open();
                dsBoletas.dtBoletasDataTable dtTempBoletas = new dsBoletas.dtBoletasDataTable();
                dsBoletas.dtBoletasRow newRow = dtTempBoletas.NewdtBoletasRow(); // (dsBoletas.dtBoletasRow)dtNewBoleta.NewRow();

                newRow.boletaID = int.Parse(this.lblNumBoleta.Text);

                newRow.productorID = int.Parse(this.ddlNewBoletaProductor.SelectedValue);
                newRow.NombreProductor = this.ddlNewBoletaProductor.SelectedItem.Text.ToUpper();
                newRow.NumeroBoleta = this.txtNewNumBoleta.Text;
                newRow.Ticket = this.txtNewTicket.Text;
                newRow.bodegaID = int.Parse(this.ddlNewBoletaBodega.SelectedItem.Value);
                newRow.cicloID = int.Parse(this.drpdlCiclo.SelectedItem.Value);
                DateTime dtFechaEntrada = new DateTime();
                DateTime.TryParse(this.txtNewFechaEntrada.Text, out dtFechaEntrada);
                newRow.FechaEntrada = dtFechaEntrada;
                double dPesoEntrada = 0;
                double.TryParse(this.txtNewPesoEntrada.Text, out dPesoEntrada);
                newRow.PesoDeEntrada = dPesoEntrada;
                DateTime dtFechaSalida = new DateTime();
                DateTime.TryParse(this.txtNewFechaSalida.Text, out dtFechaSalida);
                newRow.FechaSalida = dtFechaSalida;
                double dPesoSalida = 0;
                double.TryParse(this.txtNewPesoSalida.Text, out dPesoSalida);
                newRow.PesoDeSalida = dPesoSalida;

                newRow.chofer = this.txtChofer.Text;
                newRow.Placas = this.txtPlacas.Text;

                if (newRow.PesoDeEntrada - newRow.PesoDeSalida > 0)
                {
                    newRow.pesonetoentrada = newRow.PesoDeEntrada - newRow.PesoDeSalida;
                    newRow.pesonetosalida = 0;
                }
                else
                {
                    newRow.pesonetoentrada = 0;
                    newRow.pesonetosalida = newRow.PesoDeSalida - newRow.PesoDeEntrada;
                }
                newRow.productoID = int.Parse(this.ddlNewBoletaProducto.SelectedValue);
                newRow.Producto = this.ddlNewBoletaProducto.SelectedItem.Text.ToUpper();
                double dHumedad = 0;
                double.TryParse(this.txtNewHumedad.Text, out dHumedad);
                newRow.humedad = dHumedad;
                double dImpurezas = 0;
                double.TryParse(this.txtNewImpurezas.Text, out dImpurezas);
                newRow.impurezas = dImpurezas;
                decimal dPrecio = 0;
                decimal.TryParse(this.txtNewPrecio.Text, out dPrecio);
                newRow.precioapagar = dPrecio;
                double dSecado = 0;
                double.TryParse(this.txtNewSecado.Text, out dSecado);
                newRow.dctoSecado = dSecado;
                newRow.userID = int.Parse(this.Session["USERID"].ToString());
               // dtTempBoletas.AdddtBoletasRow(newRow);
                SqlConnection addConn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand addComm = new SqlCommand();
                addComm.Connection = addConn;
                addConn.Open();
                addComm.CommandText = "Update Boletas set productorID = @productorID, NombreProductor = @NombreProductor, NumeroBoleta = @NumeroBoleta, Ticket = @Ticket, bodegaID = @bodegaID, cicloID = @cicloID, FechaEntrada = @FechaEntrada, PesoDeEntrada = @PesoDeEntrada , ";
                addComm.CommandText += " FechaSalida = @FechaSalida, PesoDeSalida = @PesoDeSalida, pesonetoentrada = @pesonetoentrada,  pesonetosalida = @pesonetosalida, productoID = @productoID, humedad = @humedad, impurezas = @impurezas, precioapagar = @precioapagar, dctoSecado = @dctoSecado, userID = @userID, chofer = @chofer, placas = @placas ";
                addComm.CommandText += " where boletaID = @boletaID";
                addComm.Parameters.Add("@productorID", SqlDbType.Int).Value = newRow.productorID;
                // addComm.Parameters.AddWithValue("@productorID", newRow.productorID);
                addComm.Parameters.Add("@NombreProductor", SqlDbType.NVarChar).Value = newRow.NombreProductor;
                //addComm.Parameters.AddWithValue("@NombreProductor", newRow.NombreProductor);
                addComm.Parameters.Add("@NumeroBoleta", SqlDbType.NVarChar).Value = newRow.NumeroBoleta;
                //addComm.Parameters.AddWithValue("@NumeroBoleta", newRow.NumeroBoleta);
                //  addComm.Parameters.AddWithValue("@Ticket", newRow.Ticket);
                addComm.Parameters.Add("@Ticket", SqlDbType.NVarChar).Value = newRow.Ticket;
                //  addComm.Parameters.AddWithValue("@bodegaID", newRow.bodegaID);
                addComm.Parameters.Add("@bodegaID", SqlDbType.Int).Value = newRow.bodegaID;
                //  addComm.Parameters.AddWithValue("@cicloID", newRow.cicloID);
                addComm.Parameters.Add("@cicloID", SqlDbType.Int).Value = newRow.cicloID;
                //  addComm.Parameters.AddWithValue("@FechaEntrada", newRow.FechaEntrada);
                addComm.Parameters.Add("@FechaEntrada", SqlDbType.DateTime).Value = newRow.FechaEntrada;
                addComm.Parameters.Add("@PesoDeEntrada", SqlDbType.Float).Value = (float)newRow.PesoDeEntrada;
                addComm.Parameters.Add("@FechaSalida", SqlDbType.DateTime).Value = newRow.FechaSalida;
                // addComm.Parameters.AddWithValue("@FechaSalida", newRow.FechaSalida);
                addComm.Parameters.Add("@PesoDeSalida", SqlDbType.Float).Value = newRow.PesoDeSalida;
                addComm.Parameters.Add("@pesonetoentrada", SqlDbType.Float).Value = newRow.pesonetoentrada;
                addComm.Parameters.Add("@pesonetosalida", SqlDbType.Float).Value = newRow.pesonetosalida;
                // addComm.Parameters.AddWithValue("@productoID", newRow.productoID);
                addComm.Parameters.Add("@productoID", SqlDbType.Int).Value = newRow.productoID;
                addComm.Parameters.Add("@humedad", SqlDbType.Float).Value = newRow.humedad;
                addComm.Parameters.Add("@impurezas", SqlDbType.Float).Value = newRow.impurezas;
                addComm.Parameters.Add("@precioapagar", SqlDbType.Float).Value = newRow.precioapagar;
                addComm.Parameters.Add("@dctoSecado", SqlDbType.Float).Value = newRow.dctoSecado;
                //addComm.Parameters.AddWithValue("@userID", newRow.userID);
                addComm.Parameters.Add("@userID", SqlDbType.Int).Value = newRow.userID;
                addComm.Parameters.Add("@boletaID", SqlDbType.Int).Value = newRow.boletaID;
                addComm.Parameters.Add("@chofer", SqlDbType.VarChar).Value = newRow.chofer;
                addComm.Parameters.Add("@placas", SqlDbType.VarChar).Value = newRow.Placas;

                addComm.ExecuteNonQuery();
                this.UpdateTransportista(newRow.boletaID);
                if (this.rbClienteVenta.Checked)
                {
                    //UPDATE BOLETASSALIDA
                    this.updateBoletadatosSalida(newRow.boletaID);
                    //BORRAMOS LA RELACION CON CLIENTEDEVENTA
                    addComm.CommandText = "delete from ClienteVenta_Boletas where BoletaID = @boletaID";
                    addComm.Parameters.Clear();
                    addComm.Parameters.Add("@boletaID",SqlDbType.Int).Value =   newRow.boletaID;
                    addComm.ExecuteNonQuery();
                    addComm.CommandText = "insert into ClienteVenta_Boletas(clienteventaID, BoletaID) Values(@clienteID, @bolID)";
                    addComm.Parameters.Clear();
                    addComm.Parameters.Add("@clienteID", SqlDbType.Int).Value = int.Parse(this.ddlClientes.SelectedValue);
                    addComm.Parameters.Add("@bolID", SqlDbType.Int).Value = (newRow.boletaID);
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.BOLETAS, Logger.typeUserActions.INSERT, this.UserID, "AGREGÓ LA BOLETA CON EL ID: " + newRow.boletaID + ". DESDE BOLETANEWQUICK. AL CLIENTE DE VENTA: " + this.ddlClientes.SelectedItem.Text.ToUpper());
                    addComm.ExecuteNonQuery();
                }
                
                this.pnlNewBoleta.Visible = true;
                this.imgBien.Visible = true;
                this.imgMal.Visible = false;
                this.lblNewBoletaResult.Text = "LA BOLETA SE HA MODIFICADO EXITOSAMENTE";
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.BOLETAS, Logger.typeUserActions.UPDATE, this.UserID, "MODIFICÓ LA BOLETA: " + this.lblNumBoleta.Text + ". DESDE BOLETANEWQUICK.") ;
                this.myQueryStrings.Clear();
                this.btnAgregar.Visible = false;
                this.btnModificar.Visible = false;
            }
            catch (System.Exception ex)
            {

                this.pnlNewBoleta.Visible = true;
                this.imgBien.Visible = false;
                this.imgMal.Visible = true;
                this.lblNewBoletaResult.Text = "LA BOLETA NO HA PODIDO SER MODIFICADA. EX: " + ex.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(Session["USERID"].ToString()), "Error Modificando Boleta: " + this.lblNumBoleta.Text + ". DESDE BOLETANEWQUICK. EX:" + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                sqlConn.Close();
            }

        }
        internal void UpdateTransportista(int iBoletaID)
        {
            int iTransportista = -1;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "update boletas set transportistaID = @transportistaID where boletaID = @boletaID";

                if (this.chkTransportista.Checked)
                {
                    iTransportista = int.Parse(this.ddlTransportista.SelectedValue);
                }

                comm.Parameters.Add("@transportistaID", SqlDbType.Int).Value = iTransportista;
                comm.Parameters.Add("@boletaID", SqlDbType.Int).Value = iBoletaID;
                if (comm.ExecuteNonQuery() != 1)
                {
                    throw new Exception("UpdateTransportista: comm.ExecuteNonQuery() != 1 ");
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "no se pudo actualizar boleta:" + iBoletaID.ToString() + " transportista :" + iTransportista.ToString(), ref ex);
            }
            finally
            {
                conn.Close();
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Write("<script type=\"text/javascript\">window.close();</script>");
        }
         private void habilitaPaneles(panelesHabilitar panelahabilitar, int notaofacID, int provprodID){
             
             //DESAHABILITAMOS TODOS :D
             this.rbClienteVenta.Checked = this.rbClienteVenta.Enabled = this.chkFacturaClienteVenta.Checked = this.chkFacturaClienteVenta.Enabled = this.ddlClientes.Enabled = this.ddlFacturaClienteVenta.Enabled = pnlFactura.Visible = false;
             this.rbProveedor.Checked = this.rbProveedor.Enabled = this.chkBoxNotadeCompra.Enabled =this.chkBoxNotadeCompra.Checked = this.drpdlProveedor.Enabled = this.drpdlNotadeCompra.Enabled = this.pnlNotadeCompra.Visible = false;
             this.rbProductor.Checked = this.rbProductor.Enabled = this.chkBoxNotadeVenta.Enabled = this.chkBoxNotadeVenta.Checked = this.ddlNewBoletaProductor.Enabled = this.drpdlNotadeVenta.Enabled = this.pnlNotadeVenta.Visible=false;
             switch(panelahabilitar){
                 case  panelesHabilitar.FACVENTA:
                     this.rbClienteVenta.Checked = this.chkFacturaClienteVenta.Checked = this.pnlFactura.Visible = true; this.rbClienteVenta.Enabled =  this.chkFacturaClienteVenta.Enabled = this.ddlClientes.Enabled = this.ddlFacturaClienteVenta.Enabled = false; ;
                     this.ddlFacturaClienteVenta.DataBind();
                     this.ddlFacturaClienteVenta.SelectedValue = notaofacID.ToString();
                     this.ddlClientes.DataBind();
                     this.ddlClientes.SelectedValue = provprodID.ToString();
                     this.pnlDatosBoletaSalida.Visible = true;
                   
                 break;
                 case panelesHabilitar.NOTACOMPRA:
                       this.rbProveedor.Checked = this.chkBoxNotadeCompra.Checked = this.pnlNotadeCompra.Visible = true ; this.rbProveedor.Enabled = this.chkBoxNotadeCompra.Enabled =  this.drpdlProveedor.Enabled = this.drpdlNotadeCompra.Enabled = false ;
                       
                       this.drpdlNotadeCompra.DataBind();
                       this.drpdlNotadeCompra.SelectedValue = notaofacID.ToString();
                       this.drpdlProveedor.DataBind();
                       this.drpdlProveedor.SelectedValue = provprodID.ToString();
                 break;

                 case panelesHabilitar.NOTAVENTA:
                     this.rbProductor.Checked = this.chkBoxNotadeVenta.Checked = this.pnlNotadeVenta.Visible = true; this.rbProductor.Enabled = this.chkBoxNotadeCompra.Enabled = this.ddlNewBoletaProductor.Enabled = this.drpdlNotadeVenta.Enabled = false;

                     this.drpdlNotadeVenta.DataBind();
                     this.drpdlNotadeVenta.SelectedValue = notaofacID.ToString();
                     this.ddlNewBoletaProductor.DataBind();
                     this.ddlNewBoletaProductor.SelectedValue = provprodID.ToString();
                 break;

             }
            
             

        }
    }
}
