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
    public partial class frmLiquidacionTransportista : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
                if (this.LoadEncryptedQueryString() > 0 && this.myQueryStrings["liqID"] != null)
                {
                    int liqID = -1;
                    if (int.TryParse(this.myQueryStrings["liqID"].ToString(), out liqID) && liqID > 0)
                    {
                        this.lblLiqNum.Text = liqID.ToString();
                        this.LoadLiqData();
                    }
                }
            }
            this.lblEfectivoResult.Visible = false;
            this.lblPagoResult.Visible = false;
            this.lblPagoDeleteResult.Visible = false;
            
        }

        protected void LoadLiqData()
        {
            int liqid = -1;
            if (int.TryParse(this.lblLiqNum.Text, out liqid) && liqid > 0)
            {
                this.ddlClientes.DataBind();
                this.ddlTransportista.DataBind();

                SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                try
                {
                    SqlCommand comm = new SqlCommand();
                    conn.Open();
                    comm.Connection = conn;
                    comm.CommandText = "SELECT     transportistaID, fecha, userID FROM LiquidacionTransportistas "
                        + " WHERE     (LiqTransportistaID = @LiqTransportistaID)";
                    comm.Parameters.Add("@LiqTransportistaID", SqlDbType.Int).Value = liqid;
                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        this.ddlTransportista.SelectedValue = reader["transportistaID"].ToString();
                        this.txtFecha.Text = ((DateTime)reader["fecha"]).ToString("dd/MM/yyyy");
                        this.lblLiqNum.Text = liqid.ToString();
                        this.gvBoletas.DataBind();
                    }
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Error getting liq trans data: " + this.lblLiqNum.Text, ex);	
                }
                finally
                {
                    conn.Close();
                }
                this.pnlCentral.Visible = true;
            }
        }

        protected void btnNewLiq_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "INSERT INTO LiquidacionTransportistas (transportistaID, fecha, userID) "
                    + " VALUES (@transportistaID,@fecha,@userID); select SCOPE_IDENTITY();";
                comm.Parameters.Add("@transportistaID", SqlDbType.Int).Value = this.ddlTransportista.SelectedValue;
                comm.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Parse(this.txtFecha.Text);
                comm.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                object result = comm.ExecuteScalar();
                if (result != null)
                {
                    this.lblLiqNum.Text = result.ToString();
                    this.LoadLiqData();
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error Add Liq a Transportista", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void ddlTransportista_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dvTransportista.DataBind();
        }

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvBoletasClientes.DataBind();
        }

        protected void btnAddBoletasLiq_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                int liqid = -1;
                liqid =  int.Parse(this.lblLiqNum.Text);
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "INSERT INTO LiqTransportista_Boletas (LiqTransportistaID, boletaID) "
                    + " VALUES     (@LiqTransportistaID,@boletaID)";
                CheckBox chk = null;
                foreach(GridViewRow row in this.gvBoletasClientes.Rows)
                {
                    chk = (CheckBox)row.FindControl("chkAddBoleta");
                    if (chk != null && chk.Checked)
                    {
                        comm.Parameters.Clear();
                        comm.Parameters.Add("@LiqTransportistaID", SqlDbType.Int).Value = liqid;
                        comm.Parameters.Add("@boletaID", SqlDbType.Int).Value = this.gvBoletasClientes.DataKeys[row.RowIndex][0];
                        comm.ExecuteNonQuery();
                    }
                }
                this.gvBoletasClientes.DataBind();
                this.gvBoletas.DataBind();
                this.DetailsViewEstadoGeneral.DataBind();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "adding boletas to liq trans", ex);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void gvBoletas_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            this.gvBoletasClientes.DataBind();
        }
        #region cajachica
        protected void ddlEfectivoGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadCatalogos();
        }

        protected void LoadCatalogos()
        {
            try
            {
                DropDownList ddl = (DropDownList)this.dvAddEfectivo.FindControl("ddlEfectivoGrupo");
                SqlDataSource sds = (SqlDataSource)this.dvAddEfectivo.FindControl("sdsEfectivoCatalogos");
                DropDownList ddl2 = (DropDownList)this.dvAddEfectivo.FindControl("ddlEfectivoCatalogo");
                if (ddl != null && sds != null)
                {
                    sds.FilterExpression = "grupoCatalogoID=" + ddl.SelectedValue;
                    ddl2.DataBind();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "changing the subcatalogos", ex);
            }
        }

        protected void ddlEfectivoGrupo_DataBound(object sender, EventArgs e)
        {
            this.LoadCatalogos();
        }

        protected void ddlEfectivoCatalogo_DataBound(object sender, EventArgs e)
        {
            this.LoadSubCatalogos();
        }

        protected void ddlEfectivoCatalogo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadSubCatalogos();
        }
        protected void LoadSubCatalogos()
        {
            try
            {
                DropDownList ddl = (DropDownList)this.dvAddEfectivo.FindControl("ddlEfectivoCatalogo");
                SqlDataSource sds = (SqlDataSource)this.dvAddEfectivo.FindControl("sdsEfectivoSubCatalogos");
                DropDownList ddl2 = (DropDownList)this.dvAddEfectivo.FindControl("ddlEfectivoSubCatalogo");
                if (ddl != null && sds != null)
                {
                    sds.FilterExpression = "catalogoMovBancoID=" + ddl.SelectedValue;
                    ddl2.DataBind();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "changing the subcatalogos", ex);
            }
        }

        protected void dvAddEfectivo_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            //@cargo, @abono, @catalogoMovBancoID, @subCatalogoMovBancoID, @bodegaID)
            try
            {
                e.Values["userID"] = this.UserID;
                e.Values["abono"] = 0;
                e.Values["catalogoMovBancoID"] = ((DropDownList)this.dvAddEfectivo.FindControl("ddlEfectivoCatalogo")).SelectedValue;
                e.Values["subCatalogoMovBancoID"] = ((DropDownList)this.dvAddEfectivo.FindControl("ddlEfectivoSubCatalogo")).SelectedValue;
                e.Values["subCatalogoMovBancoID"] = e.Values["subCatalogoMovBancoID"] == null || e.Values["subCatalogoMovBancoID"].ToString().Trim().Length == 0 ? -1 : e.Values["subCatalogoMovBancoID"];
                e.Values["liquidaciontransportistaid"] = this.lblLiqNum.Text;
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "configuring values for efectivo", ex);
            }
        }

        protected void dvAddEfectivo_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "inserting mov Efectivo", e.Exception);
                this.lblEfectivoResult.Text = "NO SE PUDO AGREGAR EL PAGO, REVISE LOS DATOS E INTENTELO DE NUEVO <BR />"
                    + e.Exception.ToString();
                e.ExceptionHandled = true;
            }
            if (e.AffectedRows > 0)
            {
                this.lblEfectivoResult.Text = "EL PAGO FUE AGREGADO EXITOSAMENTE.";
            }
            this.DetailsViewEstadoGeneral.DataBind();
            this.gvPagos.DataBind();
        }

        #endregion

        #region Bancos
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
                if (e.Values["subCatalogoMovBancoFiscalID"] == null || e.Values["subCatalogoMovBancoFiscalID"].ToString().Trim().Length == 0)
                {
                    e.Values["subCatalogoMovBancoFiscalID"] = -1;
                }
                e.Values["numCheque"] = ((TextBox)this.DetailsView1.FindControl("txtChequeNum")).Text;
                e.Values["numCheque"] = e.Values["numCheque"] == null || e.Values["numCheque"].ToString().Trim().Length == 0 ? 0 : int.Parse(e.Values["numCheque"].ToString());
                e.Values["chequeNombre"] = ((TextBox)this.DetailsView1.FindControl("txtChequeNombre")).Text;
                e.Values["abono"] = 0;
                CheckBox chk = ((CheckBox)this.DetailsView1.FindControl("chkChangeSubCat"));
                if (chk != null && chk.Checked)
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
                this.InsertaNewPagoToLiquidacion();
                this.lblPagoResult.Text = "EL PAGO FUE AGREGADO EXITOSAMENTE CON EL ID: " + newID.ToString();
                this.gvPagos.DataBind();
                this.DetailsViewEstadoGeneral.DataBind();
            }
            
            this.lblPagoResult.Visible = true;
        }
        protected int newID = -1;
       

        protected void DetailsView1_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {

        }
        protected void InsertaNewPagoToLiquidacion()
        {
            //
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "INSERT INTO PagosLiquidacionTransportista (liquidaciontransportistaid, movbanID, monto,userId) VALUES     (@liquidaciontransportistaid,@movbanID,@monto,@userId)";
                comm.Parameters.Add("@liquidaciontransportistaid", SqlDbType.Int).Value = int.Parse(this.lblLiqNum.Text);
                comm.Parameters.Add("@movbanID", SqlDbType.Int).Value = this.newID;
                comm.Parameters.Add("@monto", SqlDbType.Float).Value = 0; //en caso que después quieran múltiples pagos.
                comm.Parameters.Add("@userId", SqlDbType.Int).Value = this.UserID; //en caso que después quieran múltiples pagos.

                comm.ExecuteNonQuery();
                   
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
        #endregion

        protected void sdsMovimientoDeBanco_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
       
            newID = int.Parse(e.Command.Parameters["@newID"].Value.ToString());
        
        }

        protected void gvPagos_RowDeleted(object sender, GridViewDeletedEventArgs e)
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
                this.gvPagos.DataBind();
                this.DetailsViewEstadoGeneral.DataBind();

            }
            this.lblPagoDeleteResult.Visible = true;
        }

        protected void gvBoletas_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            this.DetailsViewEstadoGeneral.DataBind();
        }

     
      
        
    }
}
