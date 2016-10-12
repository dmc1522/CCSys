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

namespace Garibay
{
    public partial class WebForm10 : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.txtFechaFin.Text = this.txtFechaIni.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.drpdlCiclo.DataBind();
                this.drpdlTipo.DataBind();
                this.drpdlBodega.DataBind();
                /*this.ddlProductos.DataBind();*/
                this.gvBoletas.DataBind();
                this.btnModificar.Visible = false;
                this.btnEliminarBoleta.Visible = false;
                this.drpdlfiltroProductor.DataBind();
                this.ddlProductos.DataBind();
                this.filtrar();
                /*DataTable dt = new DataTable();
                dt.Columns.Add("productoID",typeof(int));
                dt.Columns.Add("Nombre",typeof(string));
                SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "SELECT distinct Boletas.productoID, Productos.Nombre FROM Productos INNER JOIN Boletas ON Productos.productoID = Boletas.productoID ORDER BY Productos.Nombre";
                comm.Connection = conn;
                conn.Open();
                dt.Rows.Add(new object[2] { (int)-1, "TODOS LOS PRODUCTOS" });
                try
                {
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        dt.Rows.Add(new object[2] { int.Parse(reader["productoID"].ToString()), reader["Nombre"].ToString() });
                    }
                    this.ddlProductos.DataSourceID = "";
                    this.ddlProductos.DataSource = dt;
                    this.ddlProductos.DataValueField = "productoID";
                    this.ddlProductos.DataTextField = "Nombre";
                    this.ddlProductos.DataBind();
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Page_load", ref ex);
                    conn.Close();
                }
                finally { conn.Close(); }*/
            }
            this.compruebasecurityLevel();
           
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.btnAgregar.Visible = false;
                this.btnModificar.Visible = false;
                this.btnEliminarBoleta.Visible = false;
                this.gvBoletas.Columns[0].Visible = false;
            }

        }
       

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmAddModifyBoletas.aspx", true);

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            
            if (this.gvBoletas.SelectedDataKey[0] != null)
            {
                string idtomodify = "";
                idtomodify = this.gvBoletas.SelectedDataKey[0].ToString();
                string strRedirect = "~/frmAddModifyBoletas.aspx";
                string datosaencriptar;
                datosaencriptar = "idtomodify=";
                datosaencriptar += idtomodify;
                datosaencriptar += "&";
                strRedirect += "?data=";
                strRedirect += Utils.encriptacadena(datosaencriptar);
                Response.Redirect(strRedirect, true);
            }
            else
            {
                return;
            }
            
        }
            

        protected void gvBoletas_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnEliminarBoleta.Visible = true;
            this.btnModificar.Visible = true;
        }

        protected void drpdlfiltroProductor_DataBound(object sender, EventArgs e)
        {
            int newValue = -1;
            this.drpdlfiltroProductor.Items.Insert(0, new ListItem("TODOS LOS PRODUCTORES", newValue.ToString()));
            this.drpdlfiltroProductor.SelectedIndex = 0;
            //this.filtrar();        
        }
        protected void filtrar()
        {
            string cadenafiltros = " 1=1 ";
            if(!string.IsNullOrEmpty(this.drpdlBodega.SelectedValue))
            {
                cadenafiltros += " AND bodegaID = ";
                cadenafiltros += this.drpdlBodega.SelectedValue;
            }
            if(this.rbProductor.Checked)
            {
                if (this.drpdlfiltroProductor.SelectedIndex > 0)
                {
                    cadenafiltros += " AND productorID = ";
                    cadenafiltros += this.drpdlfiltroProductor.SelectedValue;
                }
                else
                {
                    cadenafiltros += " AND clienteventaID IS NULL AND ganProveedorID IS NULL ";
                }
            }

            if (this.rbClienteVenta.Checked)
            {
                cadenafiltros += " AND clienteventaID = ";
                cadenafiltros += this.ddlClientesVenta.SelectedValue;
            }

            if (this.rbProvGanado.Checked)
            {
                cadenafiltros += " AND ganProveedorID = ";
                cadenafiltros += this.ddlGanaderos.SelectedValue;
            }

            if (this.rbProveedor.Checked)
            {
                cadenafiltros += " AND proveedorID = ";
                cadenafiltros += this.DropDownListProveedor.SelectedValue;
            }

            if (this.ddlProductos.SelectedItem.Value != "-1")
            {
                cadenafiltros += " AND productoID = ";
                cadenafiltros += this.ddlProductos.SelectedItem.Value;
                
            }

            if (this.drpdlTipo.SelectedIndex > 0) 
            {
                if (this.drpdlTipo.SelectedIndex == 1)
                {
                    
                    cadenafiltros += " AND ";
                    cadenafiltros += " pesonetoentrada >0 ";
                   
                }
                else
                {

                    cadenafiltros += " AND ";
                    cadenafiltros += " pesonetosalida >0 ";
                }
            }
            if(this.txtNumBoleta.Text!="")
            {
                cadenafiltros += " AND ";
                cadenafiltros += " NumeroBoleta = '";
                cadenafiltros += this.txtNumBoleta.Text;
                cadenafiltros += "'";
       
            }

            if (this.chkPeriodoFilter.Checked)
            {
                cadenafiltros += " AND FechaEntrada >= '";
                cadenafiltros += DateTime.Parse(this.txtFechaIni.Text).ToString("yyyy/MM/dd 00:00:00");
                cadenafiltros += "' AND FechaEntrada <= '";
                cadenafiltros += DateTime.Parse(this.txtFechaFin.Text).ToString("yyyy/MM/dd 23:59:59");
                cadenafiltros += "'";
            }
            this.sdsBoletas.FilterExpression = cadenafiltros;
            this.sdsBoletas.DataBind();
            this.gvBoletas.DataBind();

        }

        protected void drpdlCultivo_DataBound(object sender, EventArgs e)
        {
            int newValue = -1;
            /*
            this.drpdlCultivo.Items.Insert(0, new ListItem("TODOS LOS CULTIVOS", newValue.ToString()));
                        this.drpdlCultivo.SelectedIndex = 0;*/
            

        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            filtrar();
        }

        protected void drpdlCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        protected void btnEliminarBoleta_Click(object sender, EventArgs e)
        {
            if (this.gvBoletas.SelectedDataKey["boletaID"] != null)
            {
                if(dbFunctions.EliminaBoleta(this.gvBoletas.SelectedDataKey["boletaID"].ToString(),this.UserID))
                {
                    this.gvBoletas.DataBind();
                    this.gvBoletas.SelectedIndex = -1;
                    this.btnEliminarBoleta.Visible = false;
                }
            }
        }

        protected void ddlProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpdlfiltroProductor.DataBind();
        }


        protected void ddlProductos_DataBound(object sender, EventArgs e)
        {
            int newValue = -1;
            this.ddlProductos.Items.Insert(0, new ListItem("TODOS LOS PRODUCTOS", newValue.ToString()));
            this.ddlProductos.SelectedIndex = 0;
        }

        protected void gvBoletas_DataBound(object sender, EventArgs e)
        {
            try
            {
                DataView dv = (DataView)this.sdsBoletas.Select(DataSourceSelectArguments.Empty);
                DataTable dt = dv.ToTable();
                if (dt.Rows.Count == 0)
                {
                    return;
                }
                Label lbl = (Label)this.gvBoletas.FooterRow.FindControl("lblPesoEntradaTotal");
                if (lbl != null)
                {
                    lbl.Text = (double.Parse(dt.Compute("SUM(pesonetoentrada)", "").ToString())).ToString("N2");
                }
                lbl = (Label)this.gvBoletas.FooterRow.FindControl("lblPesoSalidaTotal");
                if (lbl != null)
                {
                    lbl.Text = (double.Parse(dt.Compute("SUM(pesonetosalida)", "").ToString())).ToString("N2");
                }
                lbl = (Label)this.gvBoletas.FooterRow.FindControl("lblCabezasTotal");
                if (lbl != null)
                {
                    lbl.Text = int.Parse(dt.Compute("SUM(cabezasDeGanado)", "").ToString()).ToString();
                }
                lbl = (Label)this.gvBoletas.FooterRow.FindControl("lblHumedadAVG");
                if (lbl != null)
                {
                    lbl.Text = (double.Parse(dt.Compute("AVG(humedad)", "").ToString())).ToString("N2");
                }

                lbl = (Label)this.gvBoletas.FooterRow.FindControl("lblDctoHumedadTotal");
                if (lbl != null)
                {
                    lbl.Text = (double.Parse(dt.Compute("SUM(dctoHumedad)", "").ToString())).ToString("N2");
                }


                lbl = (Label)this.gvBoletas.FooterRow.FindControl("lblImpurezasAVG");
                if (lbl != null)
                {
                    lbl.Text = (double.Parse(dt.Compute("AVG(impurezas)", "").ToString())).ToString("N2");
                }

                lbl = (Label)this.gvBoletas.FooterRow.FindControl("lblDctoImpurezasTotal");
                if (lbl != null)
                {
                    lbl.Text = (double.Parse(dt.Compute("SUM(dctoImpurezas)", "").ToString())).ToString("N2");
                }


                lbl = (Label)this.gvBoletas.FooterRow.FindControl("lblSecadoTotal");
                if (lbl != null)
                {
                    lbl.Text = (double.Parse(dt.Compute("SUM(dctoSecado)", "").ToString())).ToString("N2");
                }

            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "calculando totales en lista boletas", ex);
            }
        }

        protected string GetNewOpenLiqUrl(string ID)
        {
            String sQuery = "liqID=" + ID;
            sQuery = Utils.GetEncriptedQueryString(sQuery);
            String strRedirect = "~/frmLiquidacion2010.aspx";
            strRedirect += sQuery;
            return strRedirect;
        }

        protected bool GetLiqLinkVisible(string ID)
        {
            return (ID != null && ID.Trim().Length > 0 ? true : false);
        }

        protected string GetOpenLiqFacturaCV(string ID)
        {
            String sQuery = "FacID=" + ID;
            sQuery = Utils.GetEncriptedQueryString(sQuery);
            String strRedirect = "~/frmFacturaVentaClientes.aspx";
            strRedirect += sQuery;
            return strRedirect;
        }

        protected bool GetFacturaLinkVisible(string ID)
        {
            return (ID != null && ID.Trim().Length > 0 ? true : false);
        }
    }
}

