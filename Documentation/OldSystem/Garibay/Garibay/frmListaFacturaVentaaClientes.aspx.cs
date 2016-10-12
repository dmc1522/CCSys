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
    public partial class frmListaFacturaVentaaClientes : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.drpdlCiclo.DataBind();
                this.drpdlCliente.DataBind();
                this.actualizaGrid();
                this.panelmensaje.Visible = false;

            }
            this.panelmensaje.Visible = false;
            this.compruebasecurityLevel();
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.btnDelteFactura.Visible = false;
                this.GridView1.Columns[0].Visible = false;
            }

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }
            HyperLink link = (HyperLink)e.Row.FindControl("lnkOpenFac");

            if (link != null)
            {
                String sQuery = "FacID=" + e.Row.Cells[1].Text;
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = "~/frmFacturaVentaClientes.aspx";
                strRedirect += sQuery;
                link.NavigateUrl = strRedirect;
            }

            Button btn = (Button)e.Row.FindControl("btnEliminar");

            if (btn != null)
            {
                btn.CommandArgument = e.Row.Cells[1].Text; ;
                string msgDel = "return confirm('¿Realmente desea eliminar La Factura de Venta con el Número: ";
                msgDel += this.GridView1.DataKeys[e.Row.RowIndex]["facturaNo"].ToString().ToUpper();
                msgDel += "?')";
                btn.Attributes.Add("onclick", msgDel);
            }

            //if (bt != null)
            //{
            // 

            //}

        }

        protected void drpdlCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.actualizaGrid();

        }

        protected void drpdlCliente_DataBound(object sender, EventArgs e)
        {
            int newValue = -1;
            this.drpdlCliente.Items.Insert(0, new ListItem("TODOS LOS CLIENTES", newValue.ToString()));
            this.drpdlCliente.SelectedIndex = 0;
        }
        protected void actualizaGrid()
        {
            sdsFacturas.FilterExpression = "cicloID = ";
            sdsFacturas.FilterExpression += this.drpdlCiclo.SelectedValue;
            if (this.drpdlCliente.SelectedIndex > 0)
            {
                sdsFacturas.FilterExpression += " AND clienteVentaID = ";
                sdsFacturas.FilterExpression += this.drpdlCliente.SelectedValue;
            }
            this.GridView1.DataBind();
        }

        protected void drpdlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.actualizaGrid();
        }

        protected void btnOpenFac_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string type = e.CommandSource.GetType().ToString();
             
            if (type=="System.Web.UI.WebControls.Button" && ((Button)(e.CommandSource)).Text=="Eliminar")
            {


                string qryDel = "DELETE FROM FacturasClientesVenta WHERE FacturaCV=@FacturaCV";
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdDel = new SqlCommand(qryDel, conGaribay);
                this.GridView1.DataBind();
                int facturaID = int.Parse(e.CommandArgument.ToString());

                try
                {
                    cmdDel.Parameters.Add("@FacturaCV", SqlDbType.Int).Value = facturaID;
                    conGaribay.Open();
                    int numregistros = cmdDel.ExecuteNonQuery();

                    if (numregistros != 1)
                    {
                        throw new Exception("NO SE PUDO ELIMINAR LA FACTURA : " + facturaID.ToString());
                    }
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURADEVENTA, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ LA FACTURA: " + facturaID.ToString().ToUpper()));
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                    this.lblMensajeOperationresult.Text = string.Format("La Factura " + facturaID.ToString().ToUpper() + " HA SIDO ELIMINADO EXITOSAMENTE");
                    this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION      
                    this.GridView1.SelectedIndex = -1;

                    this.imagenmal.Visible = false;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = true;
                    this.actualizaGrid();
                }

                catch (Exception exception)
                {
                    Logger.Instance.LogException(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, "EXCEPCION AL BORRAR UNA FACTURA DE VENTA", this.Request.Url.ToString(), ref exception);
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    this.lblMensajeOperationresult.Text = "LA FACTURA " + facturaID.ToString().ToUpper() + " NO PUDO SER ELIMINADa, GENERÓ UNA EXCEPCIÓN";
                    this.lblMensajeException.Text = exception.Message;
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;

                }
                finally
                {
                    conGaribay.Close();
                }



            }


        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            double dSubtotal = 0.0f, dIVA = 0.0f, dTotal = 0.0f;
            int iCol = 0;
            foreach (GridViewRow row in this.GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    iCol = 0;
                    foreach(DataControlField col in  this.GridView1.Columns)
                    {
                        switch (col.HeaderText)
                        {
                            case "Subtotal":
                                    dSubtotal += Utils.GetSafeFloat(((Label)row.Cells[iCol].Controls[1]).Text);
                        	    break;
                            case "Iva":
                                    dIVA += Utils.GetSafeFloat(((Label)row.Cells[iCol].Controls[1]).Text);
                                break;
                            case "Total":
                                    dTotal += Utils.GetSafeFloat(((Label)row.Cells[iCol].Controls[1]).Text);
                                break;
                        }
                        iCol++;
                    }
                }
            }

            GridViewRow rowf = this.GridView1.FooterRow;
            if (rowf != null)
            {
                Label lbl = (Label)rowf.FindControl("lblFooterSubTotal");
                lbl.Text = string.Format("{0:C2}", dSubtotal);
                lbl = (Label)rowf.FindControl("lblFooterIVA");
                lbl.Text = string.Format("{0:C2}", dIVA);
                lbl = (Label)rowf.FindControl("lblFooterTOTAL");
                lbl.Text = string.Format("{0:C2}", dTotal);
            }
        }

        protected void btnDelteFactura_Click(object sender, EventArgs e)
        {
            string qryDel = "DELETE FROM FacturasClientesVenta WHERE FacturaCV=@FacturaCV";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdDel = new SqlCommand(qryDel, conGaribay);
            this.GridView1.DataBind();
            int facturaID = int.Parse(this.GridView1.SelectedDataKey["FacturaCV"].ToString());

            try
            {
                cmdDel.Parameters.Add("@FacturaCV", SqlDbType.Int).Value = facturaID;
                conGaribay.Open();
                int numregistros = cmdDel.ExecuteNonQuery();

                if (numregistros != 1)
                {
                    throw new Exception("NO SE PUDO ELIMINAR EL PREDIO : " + facturaID.ToString());
                }
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.FACTURADEVENTA, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ LA FACTURA: " + facturaID.ToString().ToUpper()));
                this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                this.lblMensajeOperationresult.Text = string.Format("La Factura " + facturaID.ToString().ToUpper() + " HA SIDO ELIMINADO EXITOSAMENTE");
                this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION      
                this.GridView1.SelectedIndex = -1;

                this.imagenmal.Visible = false;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = true;
                this.GridView1.DataBind();
            }

            catch (Exception exception)
            {
                Logger.Instance.LogException(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, "EXCEPCION AL BORRAR UNA FACTURA DE VENTA", this.Request.Url.ToString(), ref exception);
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                this.lblMensajeOperationresult.Text = "LA FACTURA " + facturaID.ToString().ToUpper() + " NO PUDO SER ELIMINADa, GENERÓ UNA EXCEPCIÓN";
                this.lblMensajeException.Text = exception.Message;
                this.imagenmal.Visible = true;
                this.panelmensaje.Visible = true;
                this.imagenbien.Visible = false;

            }
            finally
            {
                conGaribay.Close();
            }
        }
    }
}