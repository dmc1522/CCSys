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
    public partial class frmListDeleteCheques : Garibay.BasePage
    {
        string idtomodify;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.btnAgregarDeLista.Visible = true;
                this.btnModificarDeLista.Visible = false;
                this.btnEliminar.Visible = false;
                this.panelmensaje.Visible = false;
                if (this.panelmensaje.Visible == true)
                {
                    this.panelmensaje.Visible = false;
                }
            }
            this.gridCheques.DataSourceID = "SqlDataSource1";
        }

        protected void btnAgregarDeLista_Click(object sender, EventArgs e)
        {
            this.Server.Transfer("~/frmAddModifyCheques.aspx");
        }

        protected void btnModificarDeLista_Click(object sender, EventArgs e)
        {
            this.gridCheques.DataBind();
            if (this.gridCheques.SelectedIndex > -1)
            {
                idtomodify = this.gridCheques.Rows[this.gridCheques.SelectedIndex].Cells[1].Text;
                string strRedirect = "~/frmAddModifyCheques.aspx";
                string datosaencriptar;
                datosaencriptar = "idtomodify=";
                datosaencriptar += idtomodify;
                datosaencriptar += "&";
                strRedirect += "?data=";
                strRedirect += encriptacadena(datosaencriptar);
                Response.Redirect(strRedirect, true);
            }
            else
            {
                return;
            }

        }

        protected void gridCheques_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.gridCheques.SelectedDataKey["chequeID"] != null)
            {
                this.btnModificarDeLista.Visible = true;
                this.btnEliminar.Visible = true;
                string msgDel = "return confirm('¿Realmente desea eliminar cheque: ";
                msgDel += this.gridCheques.SelectedDataKey["chequeID"].ToString().ToUpper();
                msgDel += "?')";
                btnEliminar.Attributes.Add("onclick", msgDel);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (this.gridCheques.SelectedDataKey[0] != null)
            {

                string qryDel = "DELETE FROM Cheques WHERE chequeID=@chequeID";
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdDel = new SqlCommand(qryDel, conGaribay);
                this.gridCheques.DataBind();
                try
                {
                    cmdDel.Parameters.Add("@chequeID", SqlDbType.Int).Value = int.Parse(this.gridCheques.SelectedDataKey["chequeID"].ToString());
                    conGaribay.Open();
                    int numregistros = cmdDel.ExecuteNonQuery();

                    if (numregistros != 1)
                    {
                        //throw new Exception(string.Format(myConfig.StrFromMessages("PRODUCTOREXECUTEFAILED"), "ELIMINADO", "ELIMINARON", numregistros.ToString()));
                    }
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                    //this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTORDELETEDEXITO"), this.gridProductores.SelectedDataKey[1].ToString().ToUpper(), this.gridProductores.SelectedDataKey[2].ToString().ToUpper(), this.gridProductores.SelectedDataKey[3].ToString().ToUpper());
                    this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION      
                    this.gridCheques.SelectedIndex = -1;
                    //this.btnVerEstadodeCuenta.Visible = false;
                    this.btnModificarDeLista.Visible = false;
                    this.btnEliminar.Visible = false;
                    this.imagenmal.Visible = false;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = true;
                    this.gridCheques.DataBind();
                }
                catch (InvalidOperationException exception)
                {
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    //this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTORDELETEDFAILED"), this.gridProductores.SelectedDataKey[1].ToString().ToUpper(), this.gridProductores.SelectedDataKey[2].ToString().ToUpper(), this.gridProductores.SelectedDataKey[3].ToString().ToUpper());
                    this.lblMensajeException.Text = exception.Message;
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;
                }
                catch (SqlException exception)
                {
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    //this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTORDELETEDFAILED"), this.gridProductores.SelectedDataKey[1].ToString().ToUpper(), this.gridProductores.SelectedDataKey[2].ToString().ToUpper(), this.gridProductores.SelectedDataKey[3].ToString().ToUpper());
                    this.lblMensajeException.Text = exception.Message;
                    this.imagenmal.Visible = true;
                    this.panelmensaje.Visible = true;
                    this.imagenbien.Visible = false;

                }
                catch (Exception exception)
                {
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                    //this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("PRODUCTORDELETEDFAILED"), this.gridProductores.SelectedDataKey[1].ToString().ToUpper(), this.gridProductores.SelectedDataKey[2].ToString().ToUpper(), this.gridProductores.SelectedDataKey[3].ToString().ToUpper());
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
            else
            {
                return;
            }
        }
    }

}