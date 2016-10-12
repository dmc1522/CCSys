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
    public partial class WebForm11 : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request.QueryString["data"] != null)
                {

                    if (this.loadqueryStrings(Request.QueryString["data"].ToString()))
                    {
                        this.lblHeader.Text = "MODIFICANDO BOLETA";

                        if (this.cargadatosmodify())
                        {
                            this.txtIdtoModify.Text = myQueryStrings["idtomodify"].ToString();
                        }
                        else
                        {
                            this.txtIdtoModify.Text = "";
                            myQueryStrings.Clear();
                            Response.Redirect("~/frmAddModifyBoletas.aspx", true);

                        }
                        this.btnModificar.Visible = true;
                        this.btnAgregar.Visible = false;
                    }
                    else
                    {
                        myQueryStrings.Clear();
                        Response.Redirect("~/frmAddModifyBoletas.aspx", true);

                    }
                }
            }
            if(this.pnlMensaje.Visible){
                this.pnlMensaje.Visible = false;
                this.pnlAgregar.Visible = true;
            }

            compruebasecurityLevel();

        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            
        }

        

        protected void btnAgregar_Click1(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                sqlConn.Open();
                dsBoletas.dtBoletasDataTable dtTempBoletas = new dsBoletas.dtBoletasDataTable();
                dsBoletas.dtBoletasRow newRow = dtTempBoletas.NewdtBoletasRow(); // (dsBoletas.dtBoletasRow)dtNewBoleta.NewRow();

                //                 sqlDA.InsertCommand.CommandText += "; SET @Identity = SCOPE_IDENTITY();";
                //                 sqlDA.InsertCommand.Parameters.Add("@Identity", SqlDbType.Int, 0, "boletaID").Direction = ParameterDirection.Output;

                newRow.productorID = int.Parse(this.ddlNewBoletaProductor.SelectedValue);
                newRow.NombreProductor = this.ddlNewBoletaProductor.SelectedItem.Text;
                newRow.NumeroBoleta = this.txtNewNumBoleta.Text;
                newRow.Ticket = this.txtNewTicket.Text;
                newRow.bodegaID = int.Parse(this.ddlNewBoletaBodega.SelectedItem.Value);

                newRow.cicloID = int.Parse(this.ddlCiclo.SelectedItem.Value);

                DateTime dtFechaEntrada = new DateTime();
                if (!DateTime.TryParse(this.txtNewFechaEntrada.Text + " " + this.txtNewHoraEntrada.Text, out dtFechaEntrada))
                {
                    DateTime.TryParse(this.txtNewFechaEntrada.Text, out dtFechaEntrada);
                }
                newRow.FechaEntrada = dtFechaEntrada;
                double dPesoEntrada = 0;
                double.TryParse(this.txtNewPesoEntrada.Text, out dPesoEntrada);
                newRow.PesoDeEntrada = dPesoEntrada;

                DateTime dtFechaSalida = new DateTime();
                if (!DateTime.TryParse(this.txtNewFechaSalida.Text + " " + this.txtNewHoraEntrada.Text, out dtFechaSalida))
                {
                    DateTime.TryParse(this.txtNewFechaSalida.Text, out dtFechaSalida);
                }
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
                newRow.Producto = this.ddlNewBoletaProducto.SelectedItem.Text;

                double dHumedad = 0;
                double.TryParse(this.txtNewHumedad.Text, out dHumedad);
                newRow.humedad = dHumedad;
                double dImpurezas = 0;
                double.TryParse(this.txtNewImpurezas.Text, out dImpurezas);
                newRow.impurezas = dImpurezas;
                decimal dPrecio = 0;
                decimal.TryParse(this.txtNewPrecio.Text, out dPrecio);
                newRow.precioapagar = dPrecio;
                //addComm.Parameters.AddWithValue("@userID", newRow.userID);
               // double dctoHumedad = 0, dctoImpurezas = 0, dctoSecado = 0;
                newRow.dctoHumedad = this.chkHumedad.Checked ? Utils.getDesctoHumedad(double.Parse(this.txtNewHumedad.Text), newRow.pesonetoentrada) : 0;
                newRow.dctoImpurezas = this.chkImpurezas.Checked ? decimal.Parse(Utils.getDesctoImpurezas(double.Parse(this.txtNewImpurezas.Text), newRow.pesonetoentrada).ToString()) : 0;
                newRow.dctoSecado = this.chkSecado.Checked ? Utils.getDesctoSecado(double.Parse(this.txtNewHumedad.Text),newRow.pesonetoentrada) : 0;

//                 double dSecado = 0;
//                 double.TryParse(this.txtNewSecado.Text, out dSecado);
//                 newRow.dctoSecado = dSecado;

                newRow.userID = int.Parse(this.Session["USERID"].ToString());

                dtTempBoletas.AdddtBoletasRow(newRow);

                //                 dtNewBoleta.ImportRow(dtTempBoletas.Rows[0]);
                //                 sqlDA.Update(dtNewBoleta.GetChanges(DataRowState.Added));
                //                 dtNewBoleta.AcceptChanges();
                //                 DataRow nRow = dtNewBoleta.Rows[dtNewBoleta.Rows.Count - 1];

                SqlConnection addConn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand addComm = new SqlCommand();
                addComm.Connection = addConn;
                addConn.Open();
                addComm.CommandText = "INSERT INTO Boletas (productorID, NombreProductor, NumeroBoleta, Ticket, bodegaID, cicloID, FechaEntrada, PesoDeEntrada, FechaSalida, PesoDeSalida, pesonetoentrada,  pesonetosalida, productoID, humedad, impurezas, precioapagar, dctoSecado, dctoHumedad, dctoImpurezas, userID, applyHumedad, applyImpurezas, applySecado) VALUES     (@productorID,@NombreProductor,@NumeroBoleta,@Ticket,@bodegaID,@cicloID,@FechaEntrada,@PesoDeEntrada,@FechaSalida,@PesoDeSalida,@pesonetoentrada,@pesonetosalida,@productoID,@humedad,@impurezas,@precioapagar,@dctoSecado, @dctoHumedad, @dctoImpurezas, @userID, @applyHumedad,  @applyImpurezas, @applySecado); select boletaID = SCOPE_IDENTITY();";

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
                
                addComm.Parameters.Add("@dctoHumedad", SqlDbType.Float).Value = newRow.dctoHumedad;
                addComm.Parameters.Add("@dctoImpurezas", SqlDbType.Float).Value = newRow.dctoImpurezas;
                addComm.Parameters.Add("@applyHumedad", SqlDbType.Bit).Value = this.chkHumedad.Checked;
                addComm.Parameters.Add("@applySecado", SqlDbType.Bit).Value = this.chkImpurezas.Checked;
                addComm.Parameters.Add("@applyImpurezas", SqlDbType.Bit).Value = this.chkSecado.Checked;
                
                addComm.Parameters.Add("@userID", SqlDbType.Int).Value = newRow.userID;

                newRow.boletaID = int.Parse(addComm.ExecuteScalar().ToString());

                pnlMensaje.Visible = true;
                pnlAgregar.Visible = false;
                this.imagenbien.Visible = true;
                this.imagenmal.Visible = false;
                this.lblMensajetitle.Text = "ÉXITO";
                this.lblMensajeOperationresult.Text = "LA BOLETA SE HA AGREGADO EXITOSAMENTE";
                this.lblMensajeException.Text = "";
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.BOLETAS, Logger.typeUserActions.INSERT, this.UserID, "EL USUARIO AGREGEGÓ LA BOLETA: " + newRow.boletaID.ToString());
                this.limpiacampos();

            }
            catch (System.Exception ex)
            {

                pnlMensaje.Visible = true;
                pnlAgregar.Visible = false;
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.lblMensajetitle.Text = "FALLO";
                this.lblMensajeOperationresult.Text = "LA BOLETA NO HA PODIDO SER AGREGADA";
                this.lblMensajeException.Text = ex.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(Session["USERID"].ToString()), "Error Insertando Nueva Boleta EX:" + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                sqlConn.Close();
            }

        }
        protected void limpiacampos(){
            this.txtNewFechaEntrada.Text = "";
            this.txtNewFechaSalida.Text = "";
            this.txtNewHoraSalida.Text = "";
            this.txtNewImpurezas.Text = "";
            this.txtNewNumBoleta.Text = "";
            this.txtNewPrecio.Text = "";
            this.txtNewPesoEntrada.Text = "";
            this.txtNewPesoSalida.Text = "";
            //this.txtNewSecado.Text = "";
            this.txtNewTicket.Text = "";
            this.txtNewHoraSalida.Text = "";
            this.ddlCiclo.SelectedIndex = 0;
            this.ddlNewBoletaBodega.SelectedIndex = 0;
            this.ddlNewBoletaProductor.SelectedIndex = 0;
            this.ddlNewBoletaProducto.SelectedIndex = 0;
        }
        protected bool cargadatosmodify()
        {
            string qryIns = "SELECT     cicloID, productorID, productoID, bodegaID, NumeroBoleta, Ticket, FechaEntrada, PesoDeEntrada, FechaSalida, PesoDeSalida, humedad, impurezas,  precioapagar, applyHumedad, applySecado, applyImpurezas FROM Boletas Where boletaID = @boletaID"; 
            
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            conGaribay.Open();
            try
            {
                cmdIns.Parameters.Clear();
                cmdIns.Parameters.Add("@boletaID",SqlDbType.Int).Value = int.Parse(this.myQueryStrings["idtomodify"].ToString());
            
                SqlDataReader datostomodify;
                datostomodify = cmdIns.ExecuteReader();
                if (!datostomodify.HasRows)
                { //EL ID NO ES VALIDO
                    this.lblMensajeOperationresult.Text = myConfig.StrFromMessages("FALLOCARGARMODIFICAR");
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXCEPTION
                    this.imagenmal.Visible = true;
                    this.pnlMensaje.Visible = true;
                    this.imagenbien.Visible = false;
                    this.pnlAgregar.Visible = false;
                    return false;

                }

                if (datostomodify.Read())
                {
                    this.ddlCiclo.SelectedValue = datostomodify["cicloID"].ToString();
                    this.ddlNewBoletaProductor.SelectedValue = datostomodify["productorID"].ToString();
                    this.ddlNewBoletaProducto.SelectedValue = datostomodify["productoID"].ToString();
                    this.ddlNewBoletaBodega.SelectedValue = datostomodify["bodegaID"].ToString();
                    this.txtNewNumBoleta.Text = datostomodify["NumeroBoleta"].ToString();
                    this.txtNewTicket.Text = datostomodify["Ticket"].ToString();
                    this.txtNewFechaEntrada.Text = Utils.converttoshortFormatfromdbFormat(datostomodify["FechaEntrada"].ToString());
                    this.txtNewHoraEntrada.Text = DateTime.Parse(this.txtNewFechaEntrada.Text).ToString("hh:mm:ss");
                    this.txtNewPesoEntrada.Text = double.Parse(datostomodify["PesoDeEntrada"].ToString()).ToString();
                    this.txtNewFechaSalida.Text = Utils.converttoshortFormatfromdbFormat(datostomodify["FechaSalida"].ToString());
                    this.txtNewHoraSalida.Text = DateTime.Parse(this.txtNewFechaSalida.Text).ToString("hh:mm:ss");
                    this.txtNewPesoSalida.Text = datostomodify["PesoDeSalida"].ToString().Length > 0 ? double.Parse(datostomodify["PesoDeSalida"].ToString()).ToString() : "0.00";
                    this.txtNewHumedad.Text = datostomodify["humedad"].ToString();
                    this.txtNewImpurezas.Text = datostomodify["impurezas"].ToString();
                    this.txtNewPrecio.Text = datostomodify["precioapagar"].ToString().Length > 0 ?  double.Parse(datostomodify["precioapagar"].ToString()).ToString() : "0.00";
                    this.chkHumedad.Checked = bool.Parse(datostomodify["applyHumedad"].ToString());
                    this.chkImpurezas.Checked = bool.Parse(datostomodify["applyImpurezas"].ToString());
                    this.chkSecado.Checked = bool.Parse(datostomodify["applySecado"].ToString());
                   // this.txtNewPrecio.Text = datostomodify["precioapagar"].ToString().Length > 0 ? double.Parse(datostomodify["precioapagar"].ToString()).ToString() : "0.00";

                  //  this.txtNewSecado.Text = datostomodify["dctoSecado"].ToString().Length > 0 ? double.Parse(datostomodify["dctoSecado"].ToString()).ToString() : "0.00";
                    


                }
            }
            catch (Exception exception)
            {
                this.lblMensajeOperationresult.Text = myConfig.StrFromMessages("FALLOCARGARMODIFICAR");
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.pnlMensaje.Visible = true;
                this.imagenbien.Visible = false;
                return false;

            }
            finally
            {
                conGaribay.Close();

            }
            return true;

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                sqlConn.Open();
                dsBoletas.dtBoletasDataTable dtTempBoletas = new dsBoletas.dtBoletasDataTable();
                dsBoletas.dtBoletasRow newRow = dtTempBoletas.NewdtBoletasRow(); // (dsBoletas.dtBoletasRow)dtNewBoleta.NewRow();
                newRow.productorID = int.Parse(this.ddlNewBoletaProductor.SelectedValue);
                newRow.NombreProductor = this.ddlNewBoletaProductor.SelectedItem.Text;
                newRow.NumeroBoleta = this.txtNewNumBoleta.Text;
                newRow.Ticket = this.txtNewTicket.Text;
                newRow.bodegaID = int.Parse(this.ddlNewBoletaBodega.SelectedItem.Value);
                newRow.cicloID = int.Parse(this.ddlCiclo.SelectedItem.Value);
                DateTime dtFechaEntrada = new DateTime();
                if (!DateTime.TryParse(this.txtNewFechaEntrada.Text + " " + this.txtNewHoraEntrada.Text, out dtFechaEntrada))
                {
                    DateTime.TryParse(this.txtNewFechaEntrada.Text, out dtFechaEntrada);
                }
                newRow.FechaEntrada = dtFechaEntrada;
                double dPesoEntrada = 0;
                double.TryParse(this.txtNewPesoEntrada.Text, out dPesoEntrada);
                newRow.PesoDeEntrada = dPesoEntrada;
                DateTime dtFechaSalida = new DateTime();
                if (!DateTime.TryParse(this.txtNewFechaSalida.Text + " " + this.txtNewHoraEntrada.Text, out dtFechaSalida))
                {
                    DateTime.TryParse(this.txtNewFechaSalida.Text, out dtFechaSalida);
                }
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
                newRow.Producto = this.ddlNewBoletaProducto.SelectedItem.Text;
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
                newRow.dctoHumedad = this.chkHumedad.Checked ? Utils.getDesctoHumedad(double.Parse(this.txtNewHumedad.Text), newRow.pesonetoentrada) : 0;
                newRow.dctoImpurezas = this.chkImpurezas.Checked ? decimal.Parse(Utils.getDesctoImpurezas(double.Parse(this.txtNewImpurezas.Text), newRow.pesonetoentrada).ToString()) : 0;
                newRow.dctoSecado = this.chkSecado.Checked ? Utils.getDesctoSecado(double.Parse(this.txtNewHumedad.Text), newRow.pesonetoentrada) : 0;

               // double.TryParse(this.txtNewSecado.Text, out dSecado);
              //  newRow.dctoSecado = dSecado;
                newRow.userID = int.Parse(this.Session["USERID"].ToString());
                dtTempBoletas.AdddtBoletasRow(newRow);
                SqlConnection addConn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand addComm = new SqlCommand();
                addComm.Connection = addConn;
                addConn.Open();
                addComm.CommandText = "Update Boletas set productorID = @productorID, NombreProductor = @NombreProductor, NumeroBoleta = @NumeroBoleta, Ticket = @Ticket, bodegaID = @bodegaID, cicloID = @cicloID, FechaEntrada = @FechaEntrada, PesoDeEntrada = @PesoDeEntrada , ";
                addComm.CommandText += " FechaSalida = @FechaSalida, PesoDeSalida = @PesoDeSalida, pesonetoentrada = @pesonetoentrada,  pesonetosalida = @pesonetosalida, productoID = @productoID, humedad = @humedad, impurezas = @impurezas, precioapagar = @precioapagar,  userID = @userID, ";
                addComm.CommandText += " dctoSecado = @dctoSecado, dctoHumedad = @dctoHumedad, dctoImpurezas = @dctoImpurezas, applyImpurezas = @applyImpurezas, applyHumedad=@applyHumedad, applySecado=applySecado ";
                addComm.CommandText+= " where boletaID = @boletaID";
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
                addComm.Parameters.Add("@dctoHumedad", SqlDbType.Float).Value = newRow.dctoHumedad;
                addComm.Parameters.Add("@dctoImpurezas", SqlDbType.Float).Value = newRow.dctoImpurezas;
                addComm.Parameters.Add("@applyHumedad", SqlDbType.Bit).Value = this.chkHumedad.Checked;
                addComm.Parameters.Add("@applySecado", SqlDbType.Bit).Value = this.chkImpurezas.Checked;
                addComm.Parameters.Add("@applyImpurezas", SqlDbType.Bit).Value = this.chkSecado.Checked;
                addComm.Parameters.Add("@userID", SqlDbType.Int).Value = newRow.userID;
                addComm.Parameters.Add("@boletaID", SqlDbType.Int).Value = int.Parse(this.txtIdtoModify.Text);

                addComm.ExecuteNonQuery();
                pnlMensaje.Visible = true;
                pnlAgregar.Visible = false;
                this.imagenbien.Visible = true;
                this.imagenmal.Visible = false;
                this.lblMensajetitle.Text = "ÉXITO";
                this.lblMensajeOperationresult.Text = "LA BOLETA SE HA MODIFICADO EXITOSAMENTE";
                this.lblMensajeException.Text = "";
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.BOLETAS, Logger.typeUserActions.UPDATE, this.UserID, "MODIFICÓ LA BOLETA: " + this.txtIdtoModify.Text);
                this.myQueryStrings.Clear();
            }
            catch (System.Exception ex)
            {

                pnlMensaje.Visible = true;
                pnlAgregar.Visible = false;
                this.imagenbien.Visible = false;
                this.imagenmal.Visible = true;
                this.lblMensajetitle.Text = "FALLO";
                this.lblMensajeOperationresult.Text = "LA BOLETA NO HA PODIDO SER AGREGADA";
                this.lblMensajeException.Text = ex.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(Session["USERID"].ToString()), "Error Modificando Boleta: "  + this.txtIdtoModify.Text +  ". EX:" + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
                sqlConn.Close();
            }


        }

        protected void btnAceptardtlst_Click(object sender, EventArgs e)
        {
            if(this.txtIdtoModify.Text!=""){
                this.Response.Redirect("~/frmListBoletas.aspx",true);

            }
        }

        protected void btnCancelar_Click1(object sender, EventArgs e)
        {
            this.Server.Transfer("~/frmListBoletas.aspx");

        }

    }
}
