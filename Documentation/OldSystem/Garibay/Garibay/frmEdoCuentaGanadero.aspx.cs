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
    public partial class frmEdoCuentaGanadero : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.ddlGanadero.DataBind();
                this.gvAnticiposDadosAlProductor.DataBind();
                this.gvFacturas.DataBind();
                this.gvPagosFactura.DataBind();
                this.dvGeneral.DataBind();
            }
            this.pnlPagoResult.Visible = false;
            this.lblPagoResult.Text = string.Empty;
        }
        protected string GetOpenURL(string id)
        {
            string sRedirect = "frmFacturaGanado.aspx";
            sRedirect += Utils.GetEncriptedQueryString("FacturaID=" + id);
            return sRedirect;
        }
        protected void gvFacturas_DataBound(object sender, EventArgs e)
        {
            try
            {
                DataView dv = (DataView)this.sdFacturasGanado.Select(DataSourceSelectArguments.Empty);
                DataTable dt = dv.ToTable();
                if (dt.Rows.Count ==0)
                {
                    return;
                }
                Label lbl = null;
                lbl = (Label)this.gvFacturas.FooterRow.FindControl("lblTotal");
                if (lbl != null)
                {
                    lbl.Text = double.Parse(dt.Compute("SUM(total)", "").ToString()).ToString("C2");
                }

                lbl = (Label)this.gvFacturas.FooterRow.FindControl("lblBecerros");
                if (lbl != null)
                {
                    lbl.Text = int.Parse(dt.Compute("SUM(Becerros)", "").ToString()).ToString();
                }

                lbl = (Label)this.gvFacturas.FooterRow.FindControl("lblGanado");
                if (lbl != null)
                {
                    lbl.Text = int.Parse(dt.Compute("SUM(Ganado)", "").ToString()).ToString();
                }

                lbl = (Label)this.gvFacturas.FooterRow.FindControl("lblVacas");
                if (lbl != null)
                {
                    lbl.Text = int.Parse(dt.Compute("SUM(Vacas)", "").ToString()).ToString();
                }


                lbl = (Label)this.gvFacturas.FooterRow.FindControl("lblVaquillas");
                if (lbl != null)
                {
                    lbl.Text = int.Parse(dt.Compute("SUM(Vaquillas)", "").ToString()).ToString();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "calculating totales footer in gvFacturas", ex);
            }
        }

        protected void ddlGanadero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSubCatalogo_DataBound(object sender, EventArgs e)
        {
            /*
            DropDownList ddl = (DropDownList)this.DetailsView1.FindControl("ddlSubCatalogo");
                        if (ddl != null)
                        {
                            ddl.Items.Add(new ListItem("<-- Seleccione -->", "-1"));
                        }*/
            
        }

        protected void ddlCatalogoFiscal_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadSubcatalogoFiscalFilter();
        }

        protected void LoadSubcatalogoFiscalFilter()
        {
            try
            {
                DropDownList ddl = (DropDownList)this.DetailsView1.FindControl("ddlSubCatalogoFiscal");
                SqlDataSource sds = (SqlDataSource)this.DetailsView1.FindControl("sdsSubcatalogoFiscal");
                if (ddl != null && sds != null)
                {
                    sds.FilterExpression = "catalogoMovBancoID = " + ((DropDownList)this.DetailsView1.FindControl("ddlCatalogoFiscal")).SelectedValue;
                    ddl.DataBind();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "querying subcatalogos", ex);
            }
        }

        protected void ddlCatalogoFiscal_DataBound(object sender, EventArgs e)
        {
            this.LoadSubcatalogoFiscalFilter();
        }

        protected void ddlConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowHideChequePanel();
        }

        protected void ShowHideChequePanel()
        {
            try
            {
                Panel p = (Panel)this.DetailsView1.FindControl("pnlDatosCheque");
                DropDownList ddl = (DropDownList)this.DetailsView1.FindControl("ddlConcepto");
                if (p != null && ddl != null)
                {
                    p.Visible = ddl.SelectedItem.Text.Contains("CHEQUE");
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "show/hide concepto", ex);
            }
        }

        protected void ddlConcepto_DataBound(object sender, EventArgs e)
        {
            this.ShowHideChequePanel();
        }

        protected void ddlGrupoInterno_DataBound(object sender, EventArgs e)
        {
            this.LoadCatInternos();
        }

        protected void ddlGrupoInterno_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadCatInternos();
        }

        protected void LoadCatInternos()
        {
            try
            {
                DropDownList ddl = (DropDownList)this.DetailsView1.FindControl("ddlCatalogoInterno");
                SqlDataSource sds = (SqlDataSource)this.DetailsView1.FindControl("sdsCatalogosInterno");
                if (ddl != null && sds != null)
                {
                    sds.FilterExpression = "grupoCatalogoID  = " + ((DropDownList)this.DetailsView1.FindControl("ddlGrupoInterno")).SelectedValue;
                    ddl.DataBind();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "selecting catalogos int", ex);
            }
        }

        protected void ddlCatalogoInterno_DataBound(object sender, EventArgs e)
        {
            this.LoadSubCatInternos();
        }

        protected void ddlCatalogoInterno_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadSubCatInternos();
        }


        protected void LoadSubCatInternos()
        {
            try
            {
                DropDownList ddl = (DropDownList)this.DetailsView1.FindControl("ddlSubCatInterno");
                SqlDataSource sds = (SqlDataSource)this.DetailsView1.FindControl("sdsSubCatalogoInterno");
                if (ddl != null && sds != null)
                {
                    sds.FilterExpression = "catalogoMovBancoID  = " + ((DropDownList)this.DetailsView1.FindControl("ddlCatalogoInterno")).SelectedValue;
                    ddl.DataBind();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "selecting subcatalogos int", ex);
            }
        }

        protected void DetailsView1_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            try
            {
                e.Values["fechacobrado"] = e.Values["fechaCheque"] = e.Values["fecha"] = DateTime.Parse(e.Values["fecha"].ToString());
                e.Values["subCatalogoMovBancoFiscalID"] = ((DropDownList)this.DetailsView1.FindControl("ddlSubCatalogoFiscal")).SelectedValue;
                if (e.Values["subCatalogoMovBancoFiscalID"] == null || e.Values["subCatalogoMovBancoFiscalID"].ToString().Trim().Length ==0)
                {
                    e.Values["subCatalogoMovBancoFiscalID"] = -1;
                }
                e.Values["numCheque"] = ((TextBox)this.DetailsView1.FindControl("txtChequeNum")).Text;
                e.Values["numCheque"] = e.Values["numCheque"] == null || e.Values["numCheque"].ToString().Trim().Length == 0? 0 : int.Parse(e.Values["numCheque"].ToString());
                e.Values["chequeNombre"] = ((TextBox)this.DetailsView1.FindControl("txtChequeNombre")).Text;
                e.Values["abono"] = 0;
                CheckBox chk = ((CheckBox)this.DetailsView1.FindControl("chkChangeSubCat"));
                if(chk != null && chk.Checked)
                {
                    e.Values["catalogoMovBancoInternoID"] = ((DropDownList)this.DetailsView1.FindControl("ddlCatalogoInterno")).SelectedValue;
                    e.Values["subCatalogoMovBancoInternoID"] = ((DropDownList)this.DetailsView1.FindControl("ddlSubCatInterno")).SelectedValue;
                }
                else
                {
                    e.Values["catalogoMovBancoInternoID"] = e.Values["catalogoMovBancoFiscalID"];
                    e.Values["subCatalogoMovBancoInternoID"] = e.Values["subCatalogoMovBancoFiscalID"];
                }

                if (e.Values["subCatalogoMovBancoInternoID"] == null || e.Values["subCatalogoMovBancoInternoID"].ToString().Trim().Length == 0)
                {
                    e.Values["subCatalogoMovBancoInternoID"] = -1;
                }
                e.Values["userID"] = this.UserID;
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "selecting subcatalogos int", ex);
            }
        }

        protected void DetailsView1_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "inserting mov banco", e.Exception);
                this.lblPagoResult.Text = "NO SE PUDO AGREGAR EL PAGO, REVISE LOS DATOS E INTENTELO DE NUEVO <BR />"
                    + e.Exception.ToString();
                e.ExceptionHandled = true;
            }
            if (e.AffectedRows > 0)
            {
                this.InsertFacturasANewPago();
                this.lblPagoResult.Text = "EL PAGO FUE AGREGADO EXITOSAMENTE CON EL ID: " + newID.ToString();
                this.gvPagosFactura.DataBind();
                this.gvFacturasDisponibles.DataBind();
                this.gvFacturas.DataBind();
            }
            this.pnlPagoResult.Visible = true;
        }

        /// <summary>
        /// If efectivo is going to be added, then add another parameter to insert all at same time.
        /// if there are 2 parameters always one has to be -1
        /// </summary>
        protected void InsertFacturasANewPago()
        {
            //
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "INSERT INTO PagosFacturaDeGanado (FacturadeGanadoID, movbanID, montoPago) VALUES     (@FacturadeGanadoID,@movbanID,@montoPago)";
                TextBox txt = null;
                CheckBox chk = null;
                foreach(GridViewRow r in this.gvFacturasDisponibles.Rows)
                {
                    if (r.RowType == DataControlRowType.DataRow)
                    {
                        chk = (CheckBox)r.FindControl("chkAdd");
                        if(chk != null && chk.Checked)
                        {
                            comm.Parameters.Clear();
                            comm.Parameters.Add("@FacturadeGanadoID", SqlDbType.Int).Value = this.gvFacturasDisponibles.DataKeys[r.RowIndex]["FacturadeGanadoID"].ToString();
                            comm.Parameters.Add("@movbanID", SqlDbType.Int).Value = this.newID;
                            txt = (TextBox)r.FindControl("txtPago");
                            comm.Parameters.Add("@montoPago", SqlDbType.Float).Value = Utils.GetSafeFloat(txt.Text);
                            comm.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "inserting pago -> mov", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        protected int newID = -1;
        protected void sdsGanadero_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            
        }

        protected void sdsMovimientoDeBanco_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            newID = int.Parse(e.Command.Parameters["@newID"].Value.ToString());
        }

        protected void gvPagosFactura_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            if (e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.DELETE, "deleting mov banco", e.Exception);
                this.lblPagoResult.Text = "NO SE PUDO ELIMINAR EL PAGO, REVISE LOS DATOS E INTENTELO DE NUEVO <BR />"
                    + e.Exception.ToString();
                e.ExceptionHandled = true;
            }
            if (e.AffectedRows > 0)
            {
                this.lblPagoResult.Text = "EL PAGO FUE ELIMINADO EXITOSAMENTE";
                this.gvPagosFactura.DataBind();
                this.gvFacturasDisponibles.DataBind();
                this.gvFacturas.DataBind();
            }
            this.pnlPagoResult.Visible = true;
        }


    }
}
