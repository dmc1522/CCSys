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
    public partial class WebForm13 : Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFechaFin.Text = "";
                txtFechainicio.Text = "";
                

            }
            this.grdvLogMessages.DataSourceID = "SqlDataSource2";

            this.grdvLogMessages.PageSize = int.Parse(this.ddlElemXPage.SelectedValue);
            this.compruebasecurityLevel();
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
            }

        }
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {

          if (!filtrar())
            {
                
                this.grdvLogMessages.DataSourceID = "SqlDataSource2";
                
                
            }
            else {
                SqlDataSource2.FilterExpression = cadenaFiltros();
                this.grdvLogMessages.DataSourceID = "SqlDataSource2";
            }

          this.grdvLogMessages.PageIndex = 0;
        }

        protected void drpdlfiltroTipo_DataBound(object sender, EventArgs e)
        {
            int newValue = -1;
            this.drpdlfiltroTipo.Items.Insert(0, new ListItem("TODOS", newValue.ToString()));
            this.drpdlfiltroTipo.SelectedIndex = 0;
            //this.drpdlfiltroTipo.SelectedValue = newValue.ToString(); 
            
        }

        protected void drpdlusuario_DataBound(object sender, EventArgs e)
        {
            int newValue = -1;
            this.drpdlusuario.Items.Insert(0, new ListItem("TODOS", newValue.ToString()));
            this.drpdlusuario.SelectedIndex = 0;

            //this.drpdlusuario.SelectedValue = newValue.ToString(); 
            
        }

        protected bool filtrar(){
            if(this.txtFechaFin.Text==""&&this.txtFechainicio.Text==""&&this.drpdlfiltroTipo.SelectedItem.Text=="TODOS"&&this.drpdlusuario.SelectedItem.Text=="TODOS"&&this.drpdlAccion.SelectedItem.Text=="TODOS"){
                return false;
            }
            return true;
        }

        protected void drpdlAccion_DataBound(object sender, EventArgs e)
        {
            int newValue = -1;
            this.drpdlAccion.Items.Insert(0, new ListItem("TODOS", newValue.ToString()));
            this.drpdlAccion.SelectedIndex = 0;
            //this.drpdlAccion.SelectedValue = newValue.ToString(); 
            
        }
        protected string cadenaFiltros(){
            string filtros="";
            bool masdeuno=false;
        if(this.drpdlfiltroTipo.SelectedItem.Text!="TODOS"){
            filtros += "logmsgTypeID = " + this.drpdlfiltroTipo.SelectedValue.ToString();
            masdeuno = true;

        }
        if (this.drpdlAccion.SelectedItem.Text != "TODOS")
        {
            if(masdeuno){
                filtros += " AND ";
            }
            filtros += "useractionID = " + this.drpdlAccion.SelectedValue.ToString();
            masdeuno = true;

        }
        if (this.drpdlusuario.SelectedItem.Text != "TODOS")
        {
            if (masdeuno)
            {
                filtros += " AND ";
            }
            filtros += "userid = " + this.drpdlusuario.SelectedValue.ToString();
            masdeuno = true;

        }
        if (this.txtFechainicio.Text != "")
        {
            if (masdeuno)
            {
                filtros += " AND ";
            }
            
            filtros += "datestamp >='"+Utils.converttoLongDBFormat(txtFechainicio.Text)+"'";
            
            masdeuno = true;
            

        }
        if (this.txtFechaFin.Text != "")
        {
            if (masdeuno)
            {
                filtros += " AND ";
            }
            filtros += "datestamp <='" + Utils.converttoLongDBFormat(this.txtFechaFin.Text)+"'";
           

        }
            return filtros;
        }

        protected void PopCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            SqlDataSource2.FilterExpression = cadenaFiltros();
            
            this.txtFechainicio.Text = this.PopCalendar1.SelectedDate;
            
            //this.grdvLogMessages.DataSourceID = "SqlDataSource2";
        }

        protected void PopCalendar2_SelectionChanged(object sender, EventArgs e)
        {
            SqlDataSource2.FilterExpression = cadenaFiltros();
            
            this.txtFechaFin.Text = this.PopCalendar2.SelectedDate;
            
        }

        
            
            
        

       
        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
                this.txtFechainicio.Text = "";
                this.txtFechainicio.Text = "";
                this.drpdlfiltroTipo.SelectedIndex=this.drpdlfiltroTipo.Items.Count-1;
                this.drpdlusuario.SelectedIndex=this.drpdlusuario.Items.Count-1;
                this.drpdlAccion.SelectedIndex = this.drpdlAccion.Items.Count-1;
                SqlDataSource2.FilterExpression = cadenaFiltros();
        }
    }
}
