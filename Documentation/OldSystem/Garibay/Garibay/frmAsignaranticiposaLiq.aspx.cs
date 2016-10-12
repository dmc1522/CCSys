using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Data;

namespace Garibay
{
    public partial class frmAsignaranticiposaLiq : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack){
                this.btnBackLiq.Visible = false;
                this.cmbProductorLiquidacion.DataBind();
                this.cmbProductor.DataBind();
                this.btnAgregar.Visible = false;
                this.btnRemover.Visible = false;
                this.btnEliminarAnticipo.Visible = false;
                if (Request.QueryString["data"] != null)
                {
                    if (this.loadqueryStrings(Request.QueryString["data"].ToString()) && myQueryStrings["liqID"] != null)
                    {
                       this.cmbProductorLiquidacion.SelectedValue = myQueryStrings["liqID"].ToString();
                       this.cmbProductorLiquidacion.Enabled = false;
                       this.btnBackLiq.Visible = true;
                    }
                    else
                    {
                        myQueryStrings.Clear();
                        Response.Redirect("~/frmAsignaranticiposaLiq.aspx", true);
                    }
                }
            }
      
            this.gridAnticipossinasignar.DataBind();
            this.gridAnticiposAgregados.DataBind();
            if(this.cmbProductorLiquidacion.Items.Count<0){
                this.btnAgregar.Visible = false;
                this.btnRemover.Visible = false;
            }
//             if(this.gridAnticipossinasignar.SelectedIndex<0){
//                 this.btnEliminarAnticipo.Visible = false;
//             }
//             else{
//                 this.btnEliminarAnticipo.Visible = true;
//             }
//           
            this.compruebasecurityLevel();
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
            }
        }

        protected void gridAnticipossinasignar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.gridAnticipossinasignar.SelectedDataKey[0]!=null){
                if (this.cmbProductorLiquidacion.SelectedIndex != -1)
                {
                    this.btnAgregar.Visible = true;
                }
                this.btnEliminarAnticipo.Visible = true;
                string msgDel = "return confirm('¿Realmente desea eliminar el anticipo número: ";
                msgDel += this.gridAnticipossinasignar.SelectedDataKey[0].ToString().ToUpper();
                msgDel += "?')";
                btnEliminarAnticipo.Attributes.Add("onclick", msgDel); 
               
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (this.gridAnticipossinasignar.SelectedDataKey[0] != null)
            {
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                string query = "insert into Liquidaciones_Anticipos (LiquidacionID, Anticipos) values (@liquidacionID, @anticipoID)";
                SqlCommand cmdins = new SqlCommand(query, conGaribay);
                conGaribay.Open();
                try
                {
                    cmdins.Parameters.Add("@liquidacionID", SqlDbType.Int).Value = int.Parse(this.cmbProductorLiquidacion.SelectedValue);
                    cmdins.Parameters.Add("@anticipoID", SqlDbType.Int).Value = int.Parse(this.gridAnticipossinasignar.SelectedDataKey[0].ToString());
                    cmdins.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "BtnAgregar", ref ex);
                }
                finally
                {
                    conGaribay.Close();
                }
                this.gridAnticiposAgregados.DataBind();
                this.gridAnticipossinasignar.DataBind();



            }
        }

        protected void gridAnticiposAgregados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.gridAnticiposAgregados.SelectedDataKey[0]!=null){
                this.btnRemover.Visible = true;

            }
        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {
            if (this.gridAnticiposAgregados.SelectedDataKey[0] != null)
            {
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                string query = "delete from Liquidaciones_Anticipos where LiquidacionID = @liqID AND Anticipos = @anticipoID";
                SqlCommand cmddel = new SqlCommand(query, conGaribay);
                conGaribay.Open();
                try
                {
                    cmddel.Parameters.Add("@liqID", SqlDbType.Int).Value = int.Parse(this.cmbProductorLiquidacion.SelectedValue);
                    cmddel.Parameters.Add("@anticipoID", SqlDbType.Int).Value = int.Parse(this.gridAnticiposAgregados.SelectedDataKey[0].ToString());
                    cmddel.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conGaribay.Close();
                }
                this.gridAnticiposAgregados.DataBind();
                this.gridAnticipossinasignar.DataBind();

                
                

            }

        }

        protected void cmbProductorLiquidacion_SelectedIndexChanged(object sender, EventArgs e)
        {
           // this.gridAnticiposAgregados.DataBind();
        }

        protected void cmbProductor_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  this.gridAnticipossinasignar.DataBind();
        }

        protected void btnEliminarAnticipo_Click(object sender, EventArgs e)
        {
            if (this.gridAnticipossinasignar.SelectedDataKey[0] != null)
            {
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                string query = "delete from Anticipos where anticipoID = @anticipoID";
                SqlCommand cmddel = new SqlCommand(query, conGaribay);
                conGaribay.Open();
                try
                {
                    cmddel.Parameters.Add("@anticipoID", SqlDbType.Int).Value = int.Parse(this.gridAnticipossinasignar.SelectedDataKey[0].ToString());
                    cmddel.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conGaribay.Close();
                }
                this.gridAnticiposAgregados.DataBind();
                this.gridAnticipossinasignar.DataBind();
            }


        }

        protected void btnBackLiq_Click(object sender, EventArgs e)
        {
            String sQuery = "liqID=" + this.cmbProductorLiquidacion.SelectedValue.ToString();
            sQuery = Utils.GetEncriptedQueryString(sQuery);
            String strRedirect = "~/frmLiquidacion2010.aspx";
            strRedirect += sQuery;
            Response.Redirect(strRedirect, true);
        }
    }
}
