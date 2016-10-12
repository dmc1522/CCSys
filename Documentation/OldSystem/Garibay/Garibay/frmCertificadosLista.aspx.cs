using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
namespace Garibay
{
    public partial class frmCreditosFinancierosLista : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(this.IsPostBack){
                this.GridView1.DataBind();
            }

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
            {
                return;
            }
            HyperLink link = (HyperLink)e.Row.Cells[10].FindControl("HyperLink1");
            if (link != null)
            {



                
                string parameter, ventanatitle = "Modificar un certificado financiero";
                // String pathArchivotemp = PdfCreator.printLiquidacion(0, PdfCreator.tamañoPapel.CARTA, PdfCreator.orientacionPapel.VERTICAL, ref this.gvBoletas, ref gvAnticipos, ref gvPagosLiquidacion);
                string datosaencriptar;
                datosaencriptar = "certID=";
                datosaencriptar += this.GridView1.DataKeys[e.Row.RowIndex]["CredFinCertID"].ToString();
                parameter = "javascript:url('";
                parameter += "frmCertificadoAddQuick.aspx?data=";
                parameter += Utils.encriptacadena(datosaencriptar);
                parameter += "', '";
                parameter += ventanatitle;
                parameter += "',0,200,800,550); return false;";
                link.Attributes.Add("onClick", parameter);
                link.NavigateUrl = this.Request.Url.ToString();


            }
        }

        protected void btnActualiza_Click(object sender, EventArgs e)
        {
            this.GridView1.DataBind();
        }
        protected void calculaTotales(){
            if (this.GridView1.FooterRow != null)
            {
                // this.gvLiquidaciones.DataBind();
                this.GridView1.FooterRow.Cells[0].Text = "TOTALES";
                double total = 0;

                foreach (GridViewRow row in this.GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        total += double.Parse(row.Cells[8].Text, NumberStyles.Currency);
                        // totalCaja += double.Parse(row.Cells[6].Text, NumberStyles.Currency);


                    }

                }
                this.GridView1.FooterRow.Cells[8].Text = string.Format("{0:C2}", total);



            }


        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            GridViewRow currRow, nextRow;
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                currRow = this.GridView1.Rows[i];
                if (currRow.RowType == DataControlRowType.DataRow && i < this.GridView1.Rows.Count - 1)
                {
                    int iRowSpan = 1;
                    try
                    {
                        do
                        {
                            if (i >= this.GridView1.Rows.Count - 1)
                            {
                                break;
                            }
                            nextRow = this.GridView1.Rows[i + 1];
                            if (nextRow.Cells[0].Text == currRow.Cells[0].Text)
                            {
                                iRowSpan++; i++;
                                nextRow.Cells[0].Visible = false;
                                if (i + 1 >= this.GridView1.Rows.Count)
                                {
                                    break;
                                }
                                nextRow = this.GridView1.Rows[i + 1];
                            }
                        } while (i < this.GridView1.Rows.Count - 1 && nextRow.Cells[0].Text == currRow.Cells[0].Text);


                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.SELECT, "gvLiquidaciones_DataBound", this.Request.Url.ToString(), ref ex);
                    }
                    currRow.Cells[0].RowSpan = iRowSpan;
                }
            }
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                currRow = this.GridView1.Rows[i];
                if (currRow.RowType == DataControlRowType.DataRow && i < this.GridView1.Rows.Count - 1)
                {
                    int iRowSpan = 1;
                    try
                    {
                        do
                        {
                            if (i >= this.GridView1.Rows.Count - 1)
                            {
                                break;
                            }
                            nextRow = this.GridView1.Rows[i + 1];
                            if (nextRow.Cells[1].Text == currRow.Cells[1].Text)
                            {
                                iRowSpan++; i++;
                                nextRow.Cells[1].Visible = false;
                                if (i + 1 >= this.GridView1.Rows.Count)
                                {
                                    break;
                                }
                                nextRow = this.GridView1.Rows[i + 1];
                            }
                        } while (i < this.GridView1.Rows.Count - 1 && nextRow.Cells[1].Text == currRow.Cells[1].Text);
                        
                    
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.SELECT, "gvLiquidaciones_DataBound", this.Request.Url.ToString(), ref ex);
                    }
                    currRow.Cells[1].RowSpan = iRowSpan;
                }
            }
            this.calculaTotales();
        }
    }
}
