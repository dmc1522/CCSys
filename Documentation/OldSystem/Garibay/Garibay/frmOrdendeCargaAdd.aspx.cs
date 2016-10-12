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
    public partial class frmOrdendeCargaAdd : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                this.ddlProveedor.DataBind();
                this.drpdlCiclo.DataBind();
                this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.btnAgregar.Visible = true;
                this.btnModificar.Visible = false;
                this.btnPrint.Visible = false;
                this.btnEnviar.Visible = false;
                if (Request.QueryString["data"] != null && this.loadqueryStrings(Request.QueryString["data"].ToString()) && this.myQueryStrings != null && this.myQueryStrings["ordenID"] != null)
                {
                    this.txtNumOrdenCarga.Text = this.myQueryStrings["ordenID"].ToString();
                    this.txtIdToModify.Text = this.myQueryStrings["ordenID"].ToString();
                    this.LoadOrdendeCargaData();
                    this.btnAgregar.Visible = false;
                    this.btnModificar.Visible = true;
                    this.btnPrint.Visible = true;
                    this.btnEnviar.Visible = true;
                }
            }
            this.pnlOrdenResult.Visible = false;
        }
        private void LoadOrdendeCargaData()
        {
            string sqlQuery = "select * from OrdenesDeCarga where ordenDeCargaID = @ordenDeCargaID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdGaribay = new SqlCommand(sqlQuery, conGaribay);
            try
            {
                conGaribay.Open();
                cmdGaribay.Parameters.Add("ordenDeCargaID", SqlDbType.Int).Value = int.Parse(this.txtNumOrdenCarga.Text);
                SqlDataReader reader = cmdGaribay.ExecuteReader();
                if(reader.Read())
                {
                    this.txtAño.Text = reader["anio"].ToString();
                    this.drpdlCiclo.SelectedValue = reader["cicloId"].ToString();
                    this.txtBodega.Text = reader["bodega"].ToString();
                    this.txtCC.Text = reader["emailCC"].ToString();
                    this.txtChofer.Text = reader["chofer"].ToString();
                    this.txtColor.Text = reader["color"].ToString();
                    this.txtDescripcionEmail.Text = reader["emaildescription"].ToString();
                    this.txtDestino.Text = reader["destino"].ToString();
                    this.txtEmail.Text = reader["emailToSend"].ToString();
                    this.txtFacturara.Text = reader["facturar_a"].ToString();
                    this.txtFecha.Text = Utils.converttoshortFormatfromdbFormat(reader["fecha"].ToString());
                    this.txtJaula.Text = reader["jaula"].ToString();
                    this.txtMarca.Text = reader["marca"].ToString();
                    this.txtObservaciones.Text = reader["observaciones"].ToString();
                    this.txtOrigen.Text = reader["origen"].ToString();
                    this.txtPlacas.Text = reader["placas"].ToString();
                    this.txtPresentacion.Text = reader["presentacion"].ToString();
                    this.txtProducto.Text = reader["producto"].ToString();
                    this.txtUbicacion.Text = reader["ubicacion"].ToString();
                    this.ddlEmpresa.SelectedValue = reader["empresaId"].ToString();
                    this.ddlProveedor.SelectedValue = reader["proveedorId"].ToString();

                    string sFileName = "ORDENDECARGA.pdf";
                    sFileName = sFileName.Replace(" ", "_");
                    String sURL = "frmDescargaTmpFile.aspx";
                    string datosaencriptar;
                    datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
                    datosaencriptar = datosaencriptar + "ordenDeCargaId=" + this.txtNumOrdenCarga.Text.ToString() + "&";
                    String URLcomplete = sURL + "?data=";
                    URLcomplete += Utils.encriptacadena(datosaencriptar);
                    JSUtils.OpenNewWindowOnClick(ref this.btnPrint, URLcomplete, "Orden de carga ", true);
                }

            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "ERROR AL INSERTAR ORDEN DE CARGA", ref ex);
            }
            finally
            {
                conGaribay.Close();
            }

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            string sqlQuery = "Insert into OrdenesdeCarga (cicloID, empresaId, fecha,chofer,placas,marca,anio,color,jaula, origen, producto,presentacion, bodega, " +
                              "ubicacion, destino, facturar_a, observaciones,userId, emailToSend, emailCC, emailDescription, proveedorId)    " +
                              "values (@cicloId, @empresaId, @fecha,@chofer,@placas,@marca,@anio,@color,@jaula, @origen, @producto,@presentacion, @bodega, " +
                              "@ubicacion, @destino, @facturar_a, @observaciones,@userId, @emailToSend, @emailCC, @emailDescription,@proveedorId ); select ordenDeCargaID = SCOPE_IDENTITY();";
            SqlCommand cmdGaribay = new SqlCommand(sqlQuery, conGaribay);
            try
            {
                conGaribay.Open();
                cmdGaribay.Parameters.Add("cicloID", SqlDbType.Int).Value = int.Parse(this.drpdlCiclo.SelectedValue);
                cmdGaribay.Parameters.Add("@empresaId", SqlDbType.Int).Value = int.Parse(this.ddlEmpresa.SelectedValue);
                cmdGaribay.Parameters.Add("@fecha", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(this.txtFecha.Text);
                cmdGaribay.Parameters.Add("@chofer", SqlDbType.NVarChar).Value =  this.txtChofer.Text;
                cmdGaribay.Parameters.Add("@placas", SqlDbType.NVarChar).Value = this.txtPlacas.Text;
                cmdGaribay.Parameters.Add("@marca", SqlDbType.NVarChar).Value = this.txtMarca.Text;
                int anio = 0;
                int.TryParse(this.txtAño.Text, out anio);
                cmdGaribay.Parameters.Add("@anio", SqlDbType.Int).Value = anio;
                cmdGaribay.Parameters.Add("@color", SqlDbType.NVarChar).Value = this.txtColor.Text;
                cmdGaribay.Parameters.Add("@jaula", SqlDbType.NVarChar).Value = this.txtJaula.Text;
                cmdGaribay.Parameters.Add("@origen", SqlDbType.NVarChar).Value = this.txtOrigen.Text;
                cmdGaribay.Parameters.Add("@producto", SqlDbType.NVarChar).Value = this.txtProducto.Text;
                cmdGaribay.Parameters.Add("@presentacion", SqlDbType.NVarChar).Value = this.txtPresentacion.Text;
                cmdGaribay.Parameters.Add("@bodega", SqlDbType.NVarChar).Value = this.txtBodega.Text;
                cmdGaribay.Parameters.Add("@ubicacion", SqlDbType.NVarChar).Value = this.txtUbicacion.Text;
                cmdGaribay.Parameters.Add("@destino", SqlDbType.Text).Value = this.txtDestino.Text;               
                cmdGaribay.Parameters.Add("@facturar_a", SqlDbType.Text).Value = this.txtFacturara.Text;               
                cmdGaribay.Parameters.Add("@observaciones", SqlDbType.Text).Value = this.txtObservaciones.Text;               
                cmdGaribay.Parameters.Add("@userId", SqlDbType.Int).Value = this.UserID;
                cmdGaribay.Parameters.Add("@emailToSend", SqlDbType.Text).Value = this.txtEmail.Text;               
                cmdGaribay.Parameters.Add("@emailCC", SqlDbType.Text).Value = this.txtCC.Text;
                cmdGaribay.Parameters.Add("@emailDescription", SqlDbType.Text).Value = this.txtDescripcionEmail.Text;               
                cmdGaribay.Parameters.Add("@proveedorId", SqlDbType.NVarChar).Value =int.Parse(this.ddlProveedor.SelectedValue);
                int ordenCargaId =  int.Parse(cmdGaribay.ExecuteScalar().ToString());
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PROVEEDORES, Logger.typeUserActions.INSERT, "AGREGO UNA ORDEN DE CARGA  " + ordenCargaId.ToString());
                String sQuery = "ordenID=" + ordenCargaId.ToString();
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = "~/frmOrdendeCargaAdd.aspx" + sQuery;
                Response.Redirect(strRedirect);
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "ERROR AL INSERTAR ORDEN DE CARGA", ref ex);
                this.pnlOrdenResult.Visible = true;
                this.lblOrdenResult.Text = "NO SE HA PODIDO AGREGAR LA ORDEN. Error: " + ex.Message;
                this.imgBien.Visible = false;
                this.imgMal.Visible = true;
            }
            finally
            {
                conGaribay.Close();
            }

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            string sqlQuery = "Update OrdenesdeCarga set cicloID= @cicloID, empresaId = @empresaId, fecha = @fecha, chofer = @chofer, placas = @placas, marca=@marca,      " +
                              " anio = @anio,color = @color,jaula = @jaula, origen = @origen, producto=@producto, presentacion = @presentacion,         " +
                              " bodega = @bodega, ubicacion = @ubicacion, destino = @destino, facturar_a = @facturar_a, observaciones = @observaciones, " +
                              " userId = @userId, emailToSend = @emailToSend, emailCC = @emailCC, emailDescription = @emailDescription,                 " +
                              " proveedorId = @proveedorId where ordenDeCargaId = @ordenDeCargaId";
            SqlCommand cmdGaribay = new SqlCommand(sqlQuery, conGaribay);
            try
            {
                conGaribay.Open();
                cmdGaribay.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(this.drpdlCiclo.SelectedValue);
                cmdGaribay.Parameters.Add("@empresaId", SqlDbType.Int).Value = int.Parse(this.ddlEmpresa.SelectedValue);
                cmdGaribay.Parameters.Add("@fecha", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(this.txtFecha.Text);
                cmdGaribay.Parameters.Add("@chofer", SqlDbType.NVarChar).Value = this.txtChofer.Text;
                cmdGaribay.Parameters.Add("@placas", SqlDbType.NVarChar).Value = this.txtPlacas.Text;
                cmdGaribay.Parameters.Add("@marca", SqlDbType.NVarChar).Value = this.txtMarca.Text;
                int anio = 0;
                int.TryParse(this.txtAño.Text, out anio);
                cmdGaribay.Parameters.Add("@anio", SqlDbType.Int).Value = anio;
                cmdGaribay.Parameters.Add("@color", SqlDbType.NVarChar).Value = this.txtColor.Text;
                cmdGaribay.Parameters.Add("@jaula", SqlDbType.NVarChar).Value = this.txtJaula.Text;
                cmdGaribay.Parameters.Add("@origen", SqlDbType.NVarChar).Value = this.txtOrigen.Text;
                cmdGaribay.Parameters.Add("@producto", SqlDbType.NVarChar).Value = this.txtProducto.Text;
                cmdGaribay.Parameters.Add("@presentacion", SqlDbType.NVarChar).Value = this.txtPresentacion.Text;
                cmdGaribay.Parameters.Add("@bodega", SqlDbType.NVarChar).Value = this.txtBodega.Text;
                cmdGaribay.Parameters.Add("@ubicacion", SqlDbType.NVarChar).Value = this.txtUbicacion.Text;
                cmdGaribay.Parameters.Add("@destino", SqlDbType.Text).Value = this.txtDestino.Text;
                cmdGaribay.Parameters.Add("@facturar_a", SqlDbType.Text).Value = this.txtFacturara.Text;
                cmdGaribay.Parameters.Add("@observaciones", SqlDbType.Text).Value = this.txtFacturara.Text;
                cmdGaribay.Parameters.Add("@userId", SqlDbType.Int).Value = this.UserID;
                cmdGaribay.Parameters.Add("@emailToSend", SqlDbType.Text).Value = this.txtEmail.Text;
                cmdGaribay.Parameters.Add("@emailCC", SqlDbType.Text).Value = this.txtCC.Text;
                cmdGaribay.Parameters.Add("@emailDescription", SqlDbType.Text).Value = this.txtDescripcionEmail.Text;
                cmdGaribay.Parameters.Add("@proveedorId", SqlDbType.NVarChar).Value = int.Parse(this.ddlProveedor.SelectedValue);
                cmdGaribay.Parameters.Add("@ordenDeCargaId", SqlDbType.Int).Value = int.Parse(this.txtNumOrdenCarga.Text);
                int ordenCargaId = cmdGaribay.ExecuteNonQuery();
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PROVEEDORES, Logger.typeUserActions.UPDATE, "MODIFICO UNA ORDEN DE CARGA  " + ordenCargaId.ToString());
                String sQuery = "ordenID=" + ordenCargaId.ToString();
                this.pnlOrdenResult.Visible = true;
                this.lblOrdenResult.Text = "SE HA MODIFICADO LA ORDEN DE CARGA No. " + this.txtNumOrdenCarga.Text + " EXITOSAMENTE. ";
                this.imgBien.Visible = true;
                this.imgMal.Visible = false;
               
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "ERROR AL INSERTAR ORDEN DE CARGA", ref ex);
                this.pnlOrdenResult.Visible = true;
                this.lblOrdenResult.Text = "NO SE HA PODIDO MODIFICAR LA ORDEN " + this.txtNumOrdenCarga.Text  + ". Error: " + ex.Message;
                this.imgBien.Visible = false;
                this.imgMal.Visible = true;
            }
            finally
            {
                conGaribay.Close();
            }

        }

        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            string query = " select Empresa, logoRelativePath from Empresas where empresaID = @empresaId ";
            SqlCommand cmdGaribay = new SqlCommand(query, conGaribay);
            try
            {
                conGaribay.Open();
                cmdGaribay.Parameters.Add("@empresaId", SqlDbType.Int).Value = int.Parse(this.ddlEmpresa.SelectedValue);
                SqlDataReader read = cmdGaribay.ExecuteReader();
                if(read.Read())
                {
                    this.lblEmpresa.Text = read["Empresa"].ToString();
                    if(read["logoRelativePath"] != null && !string.IsNullOrEmpty(read["logoRelativePath"].ToString()) && read["logoRelativePath"].ToString() != "" )
                    {
                        this.imgEmpresa.ImageUrl = read["logoRelativePath"].ToString();
                    }

                }
                
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "ERROR CAMBIANDO DATOS DE EMPRESA.", ref ex);
            	
            }
        }

    
    }
}
