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

namespace Garibay
{
    public partial class frmProductos : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Agregando(false, true);
                btnEliminar.Visible = false;
                btnModificarDeLista.Visible = false;
                try
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PRODUCTOS, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "SE VISITÓ LA PÁGINA DE PRODUCTOS");
                }
                catch (Exception exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());

                }
            }
            if (this.panelmensaje.Visible == true)
            {
                this.panelmensaje.Visible = false;
            }
            this.grdvProductos.DataSourceID = "SqlDataSource3";
            this.compruebasecurityLevel();
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.panelAgregaProducto.Visible = false;
                this.btnAgregarDeLista.Visible = false;
                this.btnModificarDeLista.Visible = false;
                this.btnEliminar.Visible = false;
                this.grdvProductos.Columns[0].Visible = false;
            }

        }

        protected void Agregando(Boolean activaacgregar, Boolean semuestrbotonaagregar)
        {

            this.panelAgregaProducto.Visible = activaacgregar;
            this.btnAgregarDeLista.Visible = !activaacgregar;
            this.btnModificarDeLista.Visible = !activaacgregar;
            this.btnEliminar.Visible = !activaacgregar;
            this.btnAgregarDeForm.Visible = semuestrbotonaagregar;
            this.btnModificarDeForm.Visible = !semuestrbotonaagregar;
            this.grdvProductos.Columns[0].Visible = !activaacgregar;
            if (this.grdvProductos.SelectedIndex == -1)
            {
                this.btnModificarDeLista.Visible = false;
                this.btnEliminar.Visible = false;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Agregando(false, true);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            this.Agregando(true, true);
            this.lblPredios.Text = "AGREGAR NUEVO PRODUCTO";
            this.limpiarCampos();
            this.grdvProductos.SelectedIndex = -1;
        }

        protected void cmdModificar_Click(object sender, EventArgs e)
        {
            this.Agregando(true, false);
            this.lblPredios.Text = "MODIFICAR PRODUCTO";
            this.sdsCambiosPrecio.DataBind();
            this.gvCambiosPrecio.DataBind();
        }

        protected void grdvProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbGrupoID.DataBind();
            if (panelAgregaProducto.Visible == false)
            {
                this.btnEliminar.Visible = true;
                this.btnModificarDeLista.Visible = true;
            }
            this.txtNombre.Text = this.grdvProductos.SelectedDataKey["Nombre"].ToString();
            this.txtCodigo.Text = this.grdvProductos.SelectedDataKey["codigo"].ToString();
            this.txtDescripcion.Text = this.grdvProductos.SelectedDataKey["descripcion"].ToString();
            this.cmbGrupoID.SelectedValue = this.grdvProductos.SelectedDataKey["productoGrupoID"].ToString();
            this.txtPrecio1.Text = Utils.conviertedemonadouble(this.grdvProductos.SelectedDataKey["precio1"].ToString());
            this.txtPrecio2.Text = Utils.conviertedemonadouble(this.grdvProductos.SelectedDataKey["precio2"].ToString());
            this.txtPrecio3.Text = Utils.conviertedemonadouble(this.grdvProductos.SelectedDataKey["precio3"].ToString());
            this.txtPrecio4.Text = Utils.conviertedemonadouble(this.grdvProductos.SelectedDataKey["precio4"].ToString());
            this.txtCodigoBoletasFile.Text = this.grdvProductos.SelectedDataKey["codigoBascula"].ToString();
            this.ddlCasaAgricola.DataBind();
            this.ddlCasaAgricola.SelectedValue = this.grdvProductos.SelectedDataKey["casaagricolaID"].ToString();
            string msgDel = "return confirm('¿Realmente desea eliminar el producto: ";
            msgDel += this.grdvProductos.SelectedDataKey["Nombre"].ToString();
            msgDel += "?')";
            this.ddlunidad.DataBind();
            this.ddlpresentacion.DataBind();
            this.ddlunidad.SelectedValue = this.grdvProductos.SelectedDataKey["unidadID"].ToString();
            this.ddlpresentacion.SelectedValue = this.grdvProductos.SelectedDataKey["presentacionID"].ToString();
            this.btnEliminar.Attributes.Add("onclick", msgDel);
        }

        protected void limpiarCampos()
        {
            cmbGrupoID.DataBind();
            txtNombre.Text = "";
            txtCodigo.Text = "";
            txtDescripcion.Text = "";
            txtPrecio1.Text = "";
            txtPrecio2.Text = "";
            txtPrecio3.Text = "";
            txtPrecio4.Text = "";
            cmbGrupoID.SelectedIndex = 0;
            this.ddlunidad.DataBind();
            this.ddlunidad.SelectedIndex = 0;
            this.ddlpresentacion.DataBind();
            this.ddlpresentacion.SelectedIndex = 0;
            this.txtCodigoBoletasFile.Text = "";
        }

        protected void btnAgregarDeForm_Click(object sender, EventArgs e)
        {
            string qryIns = "INSERT INTO Productos(Nombre, codigo, descripcion, precio1, precio2, precio3, precio4, storeTS, updateTS, unidadID, presentacionID, codigoBascula, productoGrupoID, casaagricolaID) VALUES (@Nombre, @codigo, @descripcion, @precio1, @precio2, @precio3, @precio4, @storeTS, @updateTS, @unidadID, @presentacionID, @codigoBascula, @productoGrupoID, @casaagricolaID)";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            try
            {
                cmdIns.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = this.txtNombre.Text;
                cmdIns.Parameters.Add("@codigo", SqlDbType.NVarChar).Value = this.txtCodigo.Text;
                cmdIns.Parameters.Add("@descripcion", SqlDbType.Text).Value = this.txtDescripcion.Text;
                cmdIns.Parameters.Add("@precio1", SqlDbType.Float).Value = float.Parse(this.txtPrecio1.Text);
                cmdIns.Parameters.Add("@precio2", SqlDbType.Float).Value = float.Parse(this.txtPrecio2.Text);
                cmdIns.Parameters.Add("@precio3", SqlDbType.Float).Value = float.Parse(this.txtPrecio3.Text);
                cmdIns.Parameters.Add("@precio4", SqlDbType.Float).Value = float.Parse(this.txtPrecio4.Text);
                cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(Utils.Now.ToString());
                cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(Utils.Now.ToString());
                cmdIns.Parameters.Add("@unidadID", SqlDbType.Int).Value = int.Parse(this.ddlunidad.SelectedValue);
                cmdIns.Parameters.Add("@presentacionID", SqlDbType.Int).Value = int.Parse(this.ddlpresentacion.SelectedValue);
                cmdIns.Parameters.Add("@codigoBascula", SqlDbType.VarChar).Value = this.txtCodigoBoletasFile.Text;
                cmdIns.Parameters.Add("@productoGrupoID", SqlDbType.Int).Value = int.Parse(this.cmbGrupoID.SelectedValue);
                cmdIns.Parameters.Add("@casaagricolaID", SqlDbType.Int).Value = int.Parse(this.ddlCasaAgricola.SelectedItem.Value);
                conGaribay.Open();
                int numregistros = cmdIns.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("PRODUCTOEXECUTEFAILED"), "AGREGADO", "AGREGARON", numregistros.ToString()));
                }

                qryIns = "SELECT max(productoID) FROM Productos";
                
                

                int maximo;
                
                cmdIns.Parameters.Clear();
                cmdIns.CommandText = qryIns;
                maximo = (int)cmdIns.ExecuteScalar();
                
                
                 
                

                this.Agregando(false, true);
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = false;
                this.imagenbien.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTOADDEDEXITO"), this.txtNombre.Text.ToUpper());
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PRODUCTOS, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("SE AGREGÓ EL PRODUCTO: " + this.txtNombre.Text.ToUpper()));
                if(!agregarAExistencias(maximo)){
                    throw new Exception("SE INSERTO EL PRODUCTO "+this.txtNombre.Text.ToUpper()+". PERO NO SE PUDO INGRESAR CORRECTAMENTE, PORQUE NO SE INGRESO A LA TABLA DE EXISTENCIAS");

                }
                this.limpiarCampos();


            }
            catch (InvalidOperationException err1)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTOADDEDFAILED"), this.txtNombre.Text.ToUpper());
                this.lblMensajeException.Text = err1.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());

            }
            catch (SqlException err2)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTOADDEDFAILED"), this.txtNombre.Text.ToUpper());
                this.lblMensajeException.Text = err2.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());

            }
            catch (Exception err3)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTOADDEDFAILED"), this.txtNombre.Text.ToUpper());
                this.lblMensajeException.Text = err3.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), err3.Message, this.Request.Url.ToString());
            }
            finally
            {
                conGaribay.Close();
            }
        }

        protected void btnModificarDeForm_Click(object sender, EventArgs e)
        {
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                string qryUp = "UPDATE Productos SET Nombre = @Nombre, codigo = @codigo, descripcion = @descripcion, casaagricolaID=@casaagricolaID , ";
                if (this.txtPrecio1.Text != Utils.conviertedemonadouble(this.grdvProductos.SelectedDataKey["precio1"].ToString()) ||
                    this.txtPrecio2.Text != Utils.conviertedemonadouble(this.grdvProductos.SelectedDataKey["precio2"].ToString()) ||
                    this.txtPrecio3.Text != Utils.conviertedemonadouble(this.grdvProductos.SelectedDataKey["precio3"].ToString()) ||
                    this.txtPrecio4.Text != Utils.conviertedemonadouble(this.grdvProductos.SelectedDataKey["precio4"].ToString()))
                {
                    qryUp += "precio1 = @precio1, precio2 = @precio2, precio3 = @precio3, precio4 = @precio4,";
                }
                qryUp += " updateTS = @updateTS, unidadID = @unidadID, presentacionID = @presentacionID, codigoBascula = @codigoBascula, productoGrupoID = @productoGrupoID  WHERE productoID = @productoID";
                
                SqlCommand cmdUp = new SqlCommand(qryUp, conGaribay);
            
                cmdUp.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = this.txtNombre.Text;
                cmdUp.Parameters.Add("@codigo", SqlDbType.NVarChar).Value = this.txtCodigo.Text;
                cmdUp.Parameters.Add("@descripcion", SqlDbType.Text).Value = this.txtDescripcion.Text;
             
                cmdUp.Parameters.Add("@precio1", SqlDbType.Float).Value = float.Parse(this.txtPrecio1.Text);
                cmdUp.Parameters.Add("@precio2", SqlDbType.Float).Value = float.Parse(this.txtPrecio2.Text);
                cmdUp.Parameters.Add("@precio3", SqlDbType.Float).Value = float.Parse(this.txtPrecio3.Text);
                cmdUp.Parameters.Add("@precio4", SqlDbType.Float).Value = float.Parse(this.txtPrecio4.Text);
                cmdUp.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(Utils.Now.ToString());
                cmdUp.Parameters.Add("@productoID", SqlDbType.Int).Value = int.Parse(this.grdvProductos.SelectedDataKey["productoID"].ToString());
                cmdUp.Parameters.Add("@unidadID", SqlDbType.Int).Value = int.Parse(this.ddlunidad.SelectedValue);
                cmdUp.Parameters.Add("@presentacionID", SqlDbType.Int).Value = int.Parse(this.ddlpresentacion.SelectedValue);
                cmdUp.Parameters.Add("@codigoBascula", SqlDbType.VarChar).Value = this.txtCodigoBoletasFile.Text;
                cmdUp.Parameters.Add("@productoGrupoID", SqlDbType.Int).Value = int.Parse(this.cmbGrupoID.SelectedValue);
                cmdUp.Parameters.Add("@casaagricolaID", SqlDbType.Int).Value = int.Parse(this.ddlCasaAgricola.SelectedItem.Value);

                conGaribay.Open();
                int numregistros = cmdUp.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("PRODUCTOEXECUTEFAILED"), "MODIFICADO", "MODIFICARON", numregistros.ToString()));
                }
                if (this.grdvProductos.SelectedDataKey["Nombre"].ToString() != this.txtNombre.Text)
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PRODUCTOS, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ EL NOMBRE DEL PRODUCTO: \"" + this.grdvProductos.SelectedDataKey["Nombre"].ToString().ToUpper() + "\" POR: \"" + this.txtNombre.Text.ToUpper()) + "\"");
                }
                this.Agregando(false, true);
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = false;
                this.imagenbien.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTOMODIFIEDEXITO"), this.txtNombre.Text.ToUpper());
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION            
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PRODUCTOS, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("SE MODIFICÓ EL PRODUCTO: " + this.txtNombre.Text.ToUpper()));

            }
            catch (InvalidOperationException err1)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTOMODIFIEDFAILED"), this.txtNombre.Text.ToUpper());
                this.lblMensajeException.Text = err1.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());

            }
            catch (SqlException err2)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTOMODIFIEDFAILED"), this.txtNombre.Text.ToUpper());
                this.lblMensajeException.Text = err2.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());

            }
            catch (Exception err3)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTOMODIFIEDFAILED"), this.txtNombre.Text.ToUpper());
                this.lblMensajeException.Text = err3.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), err3.Message, this.Request.Url.ToString());

            }
            finally
            {
                conGaribay.Close();
                }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string qryDel = "DELETE FROM Productos WHERE productoID = @productoID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdDel = new SqlCommand(qryDel, conGaribay);
            try
            {
                cmdDel.Parameters.Add("@productoID", SqlDbType.Int).Value = int.Parse(this.grdvProductos.SelectedDataKey["productoID"].ToString());

                conGaribay.Open();
                this.Agregando(false, true);
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = false;
                this.imagenbien.Visible = true;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTODELETEDEXITO"), this.grdvProductos.SelectedDataKey["Nombre"].ToString().ToUpper());
                this.lblMensajeException.Text = "";//NO HAY EXCEPTION
                int numregistros = cmdDel.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("PRODUCTOEXECUTEFAILED"), "ELIMINADO", "ELIMINARON", numregistros.ToString()));
                }
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.PRODUCTOS, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ EL PRODUCTO: " + this.txtNombre.Text.ToUpper()));

            }
            catch (InvalidOperationException err1)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTODELETEDFAILED"), this.grdvProductos.SelectedDataKey["Nombre"].ToString().ToUpper());
                this.lblMensajeException.Text = err1.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());

            }
            catch (SqlException err2)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTODELETEDFAILED"), this.grdvProductos.SelectedDataKey["Nombre"].ToString().ToUpper());
                this.lblMensajeException.Text = err2.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());

            }
            catch (Exception err3)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTODELETEDFAILED"), this.grdvProductos.SelectedDataKey["Nombre"].ToString().ToUpper());
                this.lblMensajeException.Text = err3.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), err3.Message, this.Request.Url.ToString());

            }
            finally
            {
                conGaribay.Close();
                this.grdvProductos.DataBind();
                if(this.grdvProductos.Rows.Count<1)
                {
                    this.btnEliminar.Visible = false;
                    this.btnModificarDeLista.Visible = false;
                }
            }

        }

        protected void cmbGrupoID_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected bool agregarAExistencias(int id){
            
            String query1="SELECT bodegaID FROM Bodegas";
            String query2="SELECT cicloID FROM ciclos";
            
            string qryIns3 = "INSERT INTO Existencias (bodegaID, productoID, cicloID, cantidad)VALUES (@bodegaID, @productoID,@cicloID, 0)";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdInsbodega  = new SqlCommand(query1, conGaribay);
            SqlCommand cmdInsciclo = new SqlCommand(query2, conGaribay);
            SqlCommand cmdInsexist = new SqlCommand(qryIns3, conGaribay);
            try
            {

                //cmdIns.Parameters.Add("@productoID", SqlDbType.NVarChar).Value = id;
                

                conGaribay.Open();
                SqlDataReader sqldrbodega= cmdInsbodega.ExecuteReader();
                

                ArrayList bodegas=new ArrayList();
                ArrayList ciclos =new ArrayList();
                

                int numregistros;
                int ciclo = 0;
                int bodega = 0;
                while(sqldrbodega.Read()){
                    bodegas.Add(sqldrbodega[0]);
                    
                
                    
            }
                sqldrbodega.Close();
                SqlDataReader sqldrciclo = cmdInsciclo.ExecuteReader();
                
                while (sqldrciclo.Read())
                {
                    ciclos.Add(sqldrciclo[0]);
                    
                    
                }
                sqldrciclo.Close();
                while(ciclo<ciclos.Count){

                    while (bodega < bodegas.Count)
                    {
                        cmdInsexist.Parameters.Clear();

                        cmdInsexist.Parameters.Add("@productoID", SqlDbType.Int).Value = id;
                        cmdInsexist.Parameters.Add("@bodegaID", SqlDbType.Int).Value = int.Parse(bodegas[bodega].ToString());
                        cmdInsexist.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(ciclos[ciclo].ToString());
                        
                        numregistros = cmdInsexist.ExecuteNonQuery();
                        if (numregistros != 1)
                        {
                            throw new Exception("AL INTENTAR CREAR LA EXISTENCIA DEL PRODUCTO NO SE ALTERARON EL NÚMERO DE REGISTROS ESPERADOS");
                        }
                        
                        bodega = bodega + 1;
                }
                    ciclo = ciclo + 1;
                    bodega = 0;

                }

                

                return true;

            }
            catch (InvalidOperationException err1)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "SE INSERTO EL PRODUCTO. PERO NO SE PUDO INGRESAR CORRECTAMENTE, PORQUE NO SE INGRESO A LA TABLA DE EXISTENCIAS";
                this.lblMensajeException.Text = err1.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), err1.Message, this.Request.Url.ToString());
                return false;
            }
            catch (SqlException err2)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "SE INSERTO EL PRODUCTO. PERO NO SE PUDO INGRESAR CORRECTAMENTE, PORQUE NO SE INGRESO A LA TABLA DE EXISTENCIAS";
                this.lblMensajeException.Text = err2.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), err2.Message, this.Request.Url.ToString());
                return false;
            }
            catch (Exception err3)
            {
                this.panelmensaje.Visible = true;
                this.imagenmal.Visible = true;
                this.imagenbien.Visible = false;
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "SE INSERTO EL PRODUCTO. PERO NO SE PUDO INGRESAR CORRECTAMENTE, PORQUE NO SE INGRESÓ A LA TABLA DE EXISTENCIAS";
                this.lblMensajeException.Text = err3.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), err3.Message, this.Request.Url.ToString());
                return false;
            }
            finally
            {
                conGaribay.Close();
            }
            
        }
    }
}
