using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

namespace Garibay
{
    public partial class frmLiquidacionesLista : Garibay.BasePage
    {
    

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnEliminarLiq.Visible = false;
            if(!this.IsPostBack){
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.LIQUIDACIONES, Logger.typeUserActions.SELECT, this.UserID, "VISITÓ PÁGINA LIQUIDACIONES");
            }
            this.compruebasecurityLevel();
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.btnEliminarLiq.Visible = false;
                this.gvLiquidaciones.Columns[0].Visible = false;
            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.gvLiquidaciones.SelectedDataKey["LiquidacionID"].ToString() != "")
            {
                this.btnOpenLiq.Visible = true;
                if(!bool.Parse(this.gvLiquidaciones.SelectedDataKey["cobrada"].ToString())){
                    this.btnEliminarLiq.Visible = true;
                    this.btnEliminarLiq.Text = "Borrar liq: " + this.gvLiquidaciones.SelectedDataKey[0].ToString();
                }
                else{

                    this.btnEliminarLiq.Visible = false;
                    
                }
              
            }
        }

        protected void btnOpenLiq_Click(object sender, EventArgs e)
        {
            if (this.gvLiquidaciones.SelectedDataKey["LiquidacionID"].ToString() != "")
            {
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.LIQUIDACIONES, Logger.typeUserActions.SELECT, this.UserID, "ABRIÓ LA LIQUIDACIÓN No." + this.gvLiquidaciones.SelectedDataKey["LiquidacionID"].ToString());
                String sQuery = "liqID=" + this.gvLiquidaciones.SelectedDataKey["LiquidacionID"].ToString();
                sQuery = Utils.GetEncriptedQueryString(sQuery);
                String strRedirect = "~/frmLiquidacion2010.aspx";
                strRedirect += sQuery;
                Response.Redirect(strRedirect, true);
            }
        }

        protected void gvLiquidaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }
            HyperLink link = (HyperLink)e.Row.Cells[9].FindControl("LinkButton1");
            if (link != null)
            {
                string parameter, ventanatitle = "IMPRIMIR LIQUIDACION";
               // String pathArchivotemp = PdfCreator.printLiquidacion(0, PdfCreator.tamañoPapel.CARTA, PdfCreator.orientacionPapel.VERTICAL, ref this.gvBoletas, ref gvAnticipos, ref gvPagosLiquidacion);
                string datosaencriptar;
                datosaencriptar = "id=";
                datosaencriptar += e.Row.Cells[2].Text;
                datosaencriptar += "&";

                parameter = "javascript:url('";
                parameter += "frmLiquidacionEsqueleto.aspx?data=";
                parameter += Utils.encriptacadena(datosaencriptar);
                parameter += "', '";
                parameter += ventanatitle;
                parameter += "',200,200,300,300); return false;";
                link.Attributes.Add("onClick", parameter);
                link.NavigateUrl = this.Request.Url.ToString();
                link.Visible = ((CheckBox)e.Row.Cells[11].Controls[0]).Checked;
            }
            /*
            link = (HyperLink)e.Row.Cells[10].FindControl("lnkOpenLiq");
                        if (link != null)
                        {
                            String sQuery = "liqID=" + e.Row.Cells[2].Text;
                            sQuery = Utils.GetEncriptedQueryString(sQuery);
                            String strRedirect = "~/frmLiquidacion2010.aspx";
                            strRedirect += sQuery;
                            link.NavigateUrl = strRedirect;
                        }*/
            
        }

        protected void filtrar()
        {
            string filtros = "";
            bool haymas = false;
            if (this.dprdlProductor.SelectedIndex > 0) {
                filtros = "productorID = ";
                filtros += this.dprdlProductor.SelectedValue;
                haymas = true;
            }
            if (this.drpdlEstado.SelectedIndex > 0)
            {
                if (haymas) { filtros += " AND "; }
                filtros += " cobrada = ";
                filtros += this.drpdlEstado.SelectedValue;
            }
            this.sdsLiquidaciones.FilterExpression = filtros;
            this.gvLiquidaciones.DataBind();
                
        }

        protected void dprdlProductor_DataBound(object sender, EventArgs e)
        {
            int newValue = -1;
            this.dprdlProductor.Items.Insert(0, new ListItem("TODOS LOS PRODUCTORES", newValue.ToString()));
            this.dprdlProductor.SelectedIndex = 0;
        }

        protected void dprdlProductor_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.filtrar();
        }

        protected void drpdlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.filtrar();
        }

        protected void gvLiquidaciones_DataBound(object sender, EventArgs e)
        {
            GridViewRow currRow, nextRow;
            for (int i = 0; i < this.gvLiquidaciones.Rows.Count; i++)
            {
                currRow = this.gvLiquidaciones.Rows[i];
            	if (currRow.RowType == DataControlRowType.DataRow && i < this.gvLiquidaciones.Rows.Count - 1)
            	{
                    int iRowSpan = 1;
                    try
                    {
                        do 
                        {
                            if (i >= this.gvLiquidaciones.Rows.Count -1)
                            {
                                break;
                            }
                            nextRow = this.gvLiquidaciones.Rows[i + 1];
                            if (nextRow.Cells[1].Text == currRow.Cells[1].Text)
                            {
                                iRowSpan++; i++;
                                nextRow.Cells[1].Visible = false;
                                if (i+1 >= this.gvLiquidaciones.Rows.Count)
                                {
                                    break;
                                }
                                nextRow = this.gvLiquidaciones.Rows[i + 1];
                            }
                        } while (i < this.gvLiquidaciones.Rows.Count - 1 && nextRow.Cells[1].Text == currRow.Cells[1].Text);
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.SELECT, "gvLiquidaciones_DataBound", this.Request.Url.ToString(), ref ex);
                    }
                    currRow.Cells[1].RowSpan = iRowSpan;
            	}
            }
        }

        protected void btnEliminarLiq_Click(object sender, EventArgs e)
        {
            string sError = "";
            if (dbFunctions.deleteLiquidacion(int.Parse(this.gvLiquidaciones.SelectedDataKey[0].ToString()), this.UserID, ref sError)){
            
                this.pnlDeleteLiq.Visible=true;
                this.imgBien.Visible = true;
                this.imgMal.Visible = false;
                this.lblDeleteResult.Text = "LA LIQUIDACION No. " + this.gvLiquidaciones.SelectedDataKey[0].ToString() + " FUE ELIMINADA EXITOSAMENTE";
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.LIQUIDACIONES, Logger.typeUserActions.DELETE, this.UserID, "LIQUIDACION No. " + this.gvLiquidaciones.SelectedDataKey[0].ToString());
                this.gvLiquidaciones.SelectedIndex = -1;
                this.gvLiquidaciones.DataBind();
                this.btnEliminarLiq.Visible = false;
                
               
            }
            else{

                this.pnlDeleteLiq.Visible = true;
                this.imgBien.Visible = false;
                this.imgMal.Visible = true;
                this.lblDeleteResult.Text = "LA LIQUIDACION No. " + this.gvLiquidaciones.SelectedDataKey[0].ToString() + " NO HA PODIDO SER ELIMINADA. ERROR: " + sError;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, this.UserID, "ERROR EN LA LIQ. " + this.gvLiquidaciones.SelectedDataKey[0].ToString() + " : " + sError, this.Request.Url.ToString());
              

            }
        }

        protected void drpdlCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvLiquidaciones.SelectedIndex = -1;
            
            this.btnEliminarLiq.Visible = false;
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
            return ( ID != null && ID.Trim().Length > 0 ? true : false);
        }
        
    }
}
