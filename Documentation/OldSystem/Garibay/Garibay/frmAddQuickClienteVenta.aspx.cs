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
    public partial class frmAddQuickClienteVenta : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.btnAceptar.Visible = false;
                this.drpdlEstado.DataBind();
                this.drpdlEstado.SelectedIndex = 14;


                this.btnValidar.Visible = true;
                string msgConf = "return alert('¡¡NO OLVIDE ACTUALIZAR LA LISTA DE CLIENTES !! ') ";
                btnSalir.Attributes.Add("onclick", msgConf);
            }

            if (this.panelmensaje.Visible)
            {
                this.panelmensaje.Visible = false;
                this.btnValidar.Visible = true;
                this.btnAceptar.Visible = true;
            }
            this.compruebasecurityLevel();

        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
            }
        }


        protected void btnValidar_Click(object sender, EventArgs e)
        {
            SqlConnection checkConn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand checkComm = new SqlCommand();
            checkComm.Connection = checkConn;
            try
            {
                checkConn.Open();
                //SELECT [apaterno], [amaterno], [nombre] FROM [Productores] WHERE (([apaterno] LIKE '%' + @apaterno + '%') AND ([amaterno] LIKE '%' + @amaterno + '%') AND ([nombre] LIKE '%' + @nombre + '%')) ORDER BY [apaterno], [amaterno], [nombre]
                checkComm.CommandText = "SELECT ClientesVentas.nombre, ClientesVentas.domicilio, ClientesVentas.ciudad, ClientesVentas.telefono, Estados.estado FROM ClientesVentas INNER JOIN Estados ON ClientesVentas.estadoID = Estados.estadoID WHERE (([nombre] LIKE '%' + @nombre + '%')) order by ClientesVentas.nombre ASC ";
                checkComm.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtNombre.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(checkComm);
                da.Fill(dt);

                //check if there is a productor with the same name:
                checkComm.CommandText = "SELECT count(*) FROM [ClientesVentas] WHERE (([nombre] = @nombre ))";
                checkComm.Parameters.Clear();
                checkComm.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtNombre.Text;
               
                Boolean bYaExiste = (int.Parse(checkComm.ExecuteScalar().ToString()) > 0);
                if (dt.Rows.Count > 0)
                {
                    this.lblProductoresParecidos.Visible = this.GridView1.Visible = true;
                    this.GridView1.DataSourceID = "";
                    this.GridView1.DataSource = dt;
                    this.GridView1.DataBind();
                }

                if (bYaExiste)
                {
                    this.btnAceptar.Visible = false;

                }

                this.lblProductorYaExiste.Visible = bYaExiste;
                this.btnValidar.Visible = bYaExiste;
                this.btnAceptar.Visible = !bYaExiste;

            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, "Error validando clienteparaVenta", this.Request.Url.ToString(), ref ex);
            }
            finally
            {
                checkConn.Close();
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConAgregar = new SqlConnection(myConfig.ConnectionInfo);
            string sqlstr = "insert into ClientesVentas (nombre,domicilio,ciudad,telefono, estadoID,RFC, userID) values(@nombre, @domicilio,@ciudad,@telefono, @estadoID,@RFC, @userID) ";
            SqlCommand cmdInsertCLientesVentas = new SqlCommand(sqlstr, sqlConAgregar);
            sqlConAgregar.Open();
            try
            {
                cmdInsertCLientesVentas.Parameters.Clear();
                cmdInsertCLientesVentas.Parameters.Add("@nombre", SqlDbType.VarChar).Value = this.txtNombre.Text;
                cmdInsertCLientesVentas.Parameters.Add("@domicilio", SqlDbType.VarChar).Value = this.txtDomicilio.Text;
                cmdInsertCLientesVentas.Parameters.Add("@ciudad", SqlDbType.VarChar).Value = this.txtCiudad.Text;
                cmdInsertCLientesVentas.Parameters.Add("@telefono", SqlDbType.VarChar).Value = this.txtTelefono.Text;
                cmdInsertCLientesVentas.Parameters.Add("@estadoID", SqlDbType.Int).Value = int.Parse(this.drpdlEstado.SelectedValue);
                cmdInsertCLientesVentas.Parameters.Add("@RFC", SqlDbType.VarChar).Value = this.txtRfc.Text;
                cmdInsertCLientesVentas.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;



                int numregistros = cmdInsertCLientesVentas.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception("AL QUERER INSERTAR UN CLIENTE DE VENTA LA DB REGRESÓ QUE SE MODIFICARON: " + numregistros.ToString());
                }
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CLIENTESVENTAS, Logger.typeUserActions.INSERT, this.UserID, "AGREGO EL CLIENTE DE VENTA: " + this.txtNombre.Text);

                //this.panelListaProductos.Visible = false;
                this.panelmensaje.Visible = true;
                this.lblMensajeException.Text = "";
                this.lblMensajeOperationresult.Text = "SE AGREGO EL CIENTE " + this.txtNombre.Text.ToUpper() + " EXITOSAMENTE";
                this.lblMensajetitle.Text = "ÉXITO";
                this.imagenbien.Visible = true;
                this.imagenmal.Visible = false;
                this.limpiacampos();
                this.limpiacampos();
                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.GridView1.DataSource = null;
                this.GridView1.DataSourceID = "";
                this.GridView1.DataBind();
                this.lblProductoresParecidos.Visible = false;
                this.lblProductorYaExiste.Visible = false;
                this.btnAceptar.Visible = false;
                this.imagenbien.Visible = true;
                


            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, this.UserID, "ERROR AL INSERTAR UN CLIENTE PARA VENTA. LA EXC. FUE: " + ex.Message, this.Request.Url.ToString());
               // this.panelAgregaCliente.Visible = false;
                //this.panelListaProductos.Visible = false;
                this.panelmensaje.Visible = true;
                this.lblMensajeException.Text = ex.Message;
                this.lblMensajeOperationresult.Text = "EL CLIENTE " + this.txtNombre.Text.ToUpper() + "NO HA PODIDO SER AGREGADO";
                this.lblMensajetitle.Text = "FALLO";
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;


            }
            finally
            {
                sqlConAgregar.Close();
            }
        }
        protected void limpiacampos(){
            this.txtCiudad.Text = "";
            this.txtDomicilio.Text = "";
            this.txtTelefono.Text = "";
            this.txtRfc.Text = "";
            this.drpdlEstado.SelectedIndex = 14;
            this.txtNombre.Text = "";
            

        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Write("<script type=\"text/javascript\">window.close();</script>");
        }
    }
}
