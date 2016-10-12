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
using System.IO;

namespace Garibay
{
    public partial class frmListadeCheques : Garibay.BasePage
    {
        //String idtomodify;
        protected void Page_Load(object sender, EventArgs e)
        {

            
            
            if (!this.IsPostBack)
            {
                this.cmbStatus.DataBind();
                this.btnAjustesImpresion.Attributes.Add("onclick", "javascript:url();");
                this.ddlCuentasDeBanco.DataBind();
                DateTime dtInicioMes = new DateTime(Utils.Now.Year,Utils.Now.Month,1);
                this.txtFechaDe.Text = dtInicioMes.ToString("dd/MM/yyyy");
                this.txtFechaA.Text = Utils.Now.ToString("dd/MM/yyyy");
                this.panelLista.Visible = true;
                this.showHideColumns();
                this.actualizaFiltros();
                this.gridCheques.DataBind();
             
                try
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CHEQUES, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "VISITÓ LA PÁGINA LISTA DE CHEQUES");
                }
                catch (Exception exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                }
            }
            this.gridCheques.DataSourceID = "SqlDataSource1";
            this.sacaTotales();
            this.compruebasecurityLevel();
         
                
            
            
            
            
        }

        private void showHideColumns()
        {
            foreach (DataControlField col in this.gridCheques.Columns)
            {
                ListItem item = this.cblCampos.Items.FindByText(col.HeaderText);
                if (item != null)
                {
                    col.Visible = item.Selected;
                }
            }
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.gridCheques.Columns[0].Visible = false;
                this.gridCheques.Columns[16].Visible = false;

                
            }

        }

        private void actualizaFiltros()
        {
            string filtros;
            filtros = "fecha >= '";
            filtros += Utils.converttoLongDBFormat(this.txtFechaDe.Text);
            filtros += "' AND fecha <= '";
            filtros += Utils.converttoLongDBFormat(this.txtFechaA.Text);
            filtros += "' ";
            if(this.cmbStatus.SelectedValue!= "-1"){
                filtros += "AND chequecobrado = ";
                filtros += this.cmbStatus.SelectedValue;
            }
            this.SqlDataSource1.FilterExpression = filtros;
            this.gridCheques.DataSourceID = "SqlDataSource1";
        }

        protected void gridCheques_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Select the checkboxes from the GridView control
         
        }
        
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            this.Server.Transfer("~/frmAddModifyCheques.aspx");
        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {
            
          //  this.gridCheques.RowDataBound();
             String strresult;
           
           
            DataTable tabledetails;
            tabledetails = new DataTable();
            tabledetails.Columns.Add("# Cheque");
            tabledetails.Columns.Add("# Cuenta");
            tabledetails.Columns.Add("Descripción");
         
           
             
            for (int i = 0; i < this.gridCheques.Rows.Count; i++ )
            {
                GridViewRow row = this.gridCheques.Rows[i];
                if (((CheckBox)row.Cells[0].FindControl("chkSelect")).Checked)
                
                 { 
                   
                    String idcheque;
                         idcheque = gridCheques.DataKeys[row.DataItemIndex]["chequeID"].ToString();
                     if(gridCheques.DataKeys[row.DataItemIndex]["chequestatusID"].ToString().CompareTo("0")==0){//COMPROBAMOS QUE EL estado SEA GIRADO
                         Boolean marcadocomopagado;
                         marcadocomopagado = false;
                         string sqlQuery = "UPDATE Cheques SET chequestatusID = @chequestatusID WHERE chequeID = @chequeID";
                         SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                         SqlCommand cmdIns = new SqlCommand(sqlQuery, conGaribay);

                         try
                         {
                             cmdIns.Parameters.Add("@chequestatusID", SqlDbType.Int).Value = 1;
                             cmdIns.Parameters.Add("@chequeID", SqlDbType.Int).Value = int.Parse(idcheque);
                             conGaribay.Open();
                             int numregistros = cmdIns.ExecuteNonQuery();
                             if (numregistros != 1)
                             {   
                                 string exc;
                                 exc="AL INTENTAR MODIFICAR EL CHEQUE {0} EL NÚMERO DE REGISTROS A MODIFICAR NO FUE EL ESPERADO";
                                 throw new Exception(string.Format(exc,idcheque));
                             }
                             Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CHEQUES, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ("MARCÓ EL CHEQUE: " + gridCheques.DataKeys[row.DataItemIndex]["chequenumero"].ToString().ToUpper() + " COMO COBRADO"));
                             tabledetails.Rows.Add(gridCheques.DataKeys[row.DataItemIndex]["chequenumero"].ToString(), gridCheques.DataKeys[row.DataItemIndex]["NumeroDeCuenta"].ToString(), "El cheque ha sido marcado como cobrado.");
                             marcadocomopagado = true;
                             //MOVIMIENTO DE CUENTA DE BANCO
                             sqlQuery = "INSERT INTO MovimientosCuentasBanco (cuentaID,cicloID,tipomovcuentaID,fecha,monto,userID,chequeID, storeTS, updateTS) VALUES (@cuentaID,@cicloID,@tipomovcuentaID,@fecha,@monto,@userID,@chequeID,@storeTS,@updateTS)";
                             cmdIns.Parameters.Clear();
                             cmdIns.CommandText = sqlQuery;
                             cmdIns.Parameters.Add("@cuentaID", SqlDbType.Int).Value = int.Parse(gridCheques.DataKeys[row.DataItemIndex]["cuentaID"].ToString());
                             cmdIns.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(gridCheques.DataKeys[row.DataItemIndex]["cicloID"].ToString());
                             cmdIns.Parameters.Add("@tipomovcuentaID", SqlDbType.Int).Value = 3;
                             cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                             cmdIns.Parameters.Add("@monto", SqlDbType.Float).Value = double.Parse(gridCheques.DataKeys[row.DataItemIndex]["monto"].ToString());
                             cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = int.Parse(this.Session["USERID"].ToString());
                             cmdIns.Parameters.Add("@chequeID", SqlDbType.Int).Value = int.Parse(gridCheques.DataKeys[row.DataItemIndex]["chequeID"].ToString());
                             cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                             cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                                           
                             numregistros = cmdIns.ExecuteNonQuery();
                             if (numregistros != 1)
                             {
                                 string exc;
                                 exc = "ERROR AL INTENTAR METER EL MOVIMIENTO DE CUENTA DE BANCO DEL CHEQUE NÚMERO {0}";
                                 throw new Exception(string.Format(exc, gridCheques.DataKeys[row.DataItemIndex]["chequenumero"].ToString()));  

                                
                             }
                             Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDEBANCO, Logger.typeUserActions.INSERT, int.Parse(this.Session["USERID"].ToString()), ("INSERTÓ UN NUEVO MOVIMIENTO DE BANCO POR MOTIVO DEL CHEQUE NÚMERO: " + gridCheques.DataKeys[row.DataItemIndex]["chequenumero"].ToString().ToUpper()));
                          
                            
                       }
                         catch (Exception exception)
                         {
                             Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                             if(!marcadocomopagado){//LA EXCEPCION SE DIO ANTES DE MARCARLO
                                 tabledetails.Rows.Add(gridCheques.DataKeys[row.DataItemIndex]["chequenumero"].ToString(), gridCheques.DataKeys[row.DataItemIndex]["NumeroDeCuenta"].ToString(), "El cheque no ha sido marcado como cobrado porque se arrojo la siguiente excepcion: " + exception);

                             }
                             else{//MARCAMOS COMO GIRADO
                                 try{
                                     tabledetails.Rows.RemoveAt(tabledetails.Rows.Count-1); //QUITAMOS LA ROW
                                     tabledetails.Rows.Add(gridCheques.DataKeys[row.DataItemIndex]["chequenumero"].ToString(), gridCheques.DataKeys[row.DataItemIndex]["NumeroDeCuenta"].ToString(), "El cheque no ha sido marcado como cobrado porque no se pudo insertar el movimiento de banco correspondiente.");
                                     cmdIns.CommandText = "UPDATE Cheques SET chequestatusID = @chequestatusID WHERE chequeID = @chequeID";
                                     cmdIns.Parameters.Clear();
                                     cmdIns.Parameters.Add("@chequestatusID", SqlDbType.Int).Value = 0;
                                     cmdIns.Parameters.Add("@chequeID", SqlDbType.Int).Value = int.Parse(idcheque);
                                     cmdIns.ExecuteNonQuery();

                                  }
                                 catch(Exception ex){
                                     Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["USERID"].ToString()), ex.Message, this.Request.Url.ToString());
                                 }

                                 
                             }
                             
                            
                             

                         }
                         finally
                         {
                             conGaribay.Close();
                             //this.gridCheques.DataBind();

                         }
                     }
                     else{


                         tabledetails.Rows.Add(gridCheques.DataKeys[row.DataItemIndex]["chequenumero"].ToString(), gridCheques.DataKeys[row.DataItemIndex]["NumeroDeCuenta"].ToString(), "El cheque no ha sido marcado como cobrado porque tenía el estado: " + gridCheques.DataKeys[row.DataItemIndex]["chequestatus"].ToString());
                         
                         
                     }
                    /*
                    this.panelDetalles.Visible=true;
                                        
                                        this.gridDetails.DataSource=tabledetails;*/
                    
                   
                 }
               
                }
           /* this.gridDetails.DataSource = tabledetails;*/
            this.panelLista.Visible = false;
            this.gridCheques.DataBind();
            /*this.gridDetails.DataBind();*/
            
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int marcados=0;
            String idcheque="-1", numcheque="-1";
            for (int i = 0; i < this.gridCheques.Rows.Count; i++)
            {
                GridViewRow row = this.gridCheques.Rows[i];
                if (((CheckBox)row.Cells[0].FindControl("chkSelect")).Checked)
                {               
                    idcheque = gridCheques.DataKeys[row.DataItemIndex]["chequeID"].ToString();
                    numcheque = gridCheques.DataKeys[row.DataItemIndex]["chequenumero"].ToString();
                    marcados++;
                    if (marcados > 1)
                        i = this.gridCheques.Rows.Count;
                
                }

            }
            if(marcados==1){//SI SÓLO ERA UNO MARCADO ELIMINAMOS
                Boolean chequeeliminado = false;
                string qryDel = "DELETE FROM Cheques WHERE chequeID=@chequeID";
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdDel = new SqlCommand(qryDel, conGaribay);
                try
                {
                    cmdDel.Parameters.Add("@chequeID", SqlDbType.Int).Value = int.Parse(idcheque);
                    conGaribay.Open();
                    int numregistros = cmdDel.ExecuteNonQuery();
                    if (numregistros != 1)
                    {
                        throw new Exception(string.Format(myConfig.StrFromMessages("CHEQUEEXECUTEFAILED"), "ELIMINADO", "ELIMINARON", numregistros.ToString()));
                    }
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CHEQUES, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ EL CHEQUE: " + numcheque.ToString().ToUpper()));
                    chequeeliminado = true;
                    //ELIMINAMOS EL MOVIMIENTO DE CUENTA
                    qryDel = "DELETE FROM MovimientosCuentasBanco WHERE chequeID=@chequeID";
                    cmdDel.Parameters.Clear();
                    cmdDel.CommandText = qryDel;
                    cmdDel.Parameters.Add("@chequeID", SqlDbType.Int).Value = int.Parse(idcheque);
                    numregistros = cmdDel.ExecuteNonQuery();
                    if (numregistros != 1) {
                        string exc;
                        exc = "ERROR AL INTENTAR ELIMINAR EL MOVIMIENTO DE CUENTA DE BANCO DEL CHEQUE NÚMERO {0}";
                        throw new Exception(string.Format(exc, numcheque));  
                    }
                    /*
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                                        this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHEQUEDELETEDEXITO"), numcheque.ToString().ToUpper());
                                        this.lblMensajeException.Text = ""; //BORRAMOS PORQUE NO HAY EXcEPTION      
                                        this.imagenmal.Visible = false;
                                        this.panelmensaje.Visible = true;
                                        this.imagenbien.Visible = true;
                                        this.panelmensaje.Visible = true;*/
                    
                    this.panelLista.Visible = false;
                }
                catch (InvalidOperationException exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                    /*
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                                        this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHEQUEDELETEDFAILED"), numcheque.ToString().ToUpper());
                                        this.lblMensajeException.Text = exception.Message;
                                        this.imagenmal.Visible = true;
                                        this.panelmensaje.Visible = true;
                                        this.imagenbien.Visible = false;*/
                    
                    this.panelLista.Visible = false;
                }
                catch (SqlException exception)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                    /*
                    this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                                        this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHEQUEDELETEDFAILED"), numcheque.ToString().ToUpper());
                                        this.lblMensajeException.Text = exception.Message;
                                        this.imagenmal.Visible = true;
                                        this.panelmensaje.Visible = true;
                                        this.imagenbien.Visible = false;*/
                    
                    this.panelLista.Visible = false;
                }
                catch (Exception exception)
                {
                    if(!chequeeliminado) {//LA EXCEPCION SE DIO ANTES DE ELIMINARLO
                        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                        /*
                        this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                                                this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("CHEQUEDELETEDFAILED"), numcheque.ToString().ToUpper());
                                                this.lblMensajeException.Text = exception.Message;
                                                this.imagenmal.Visible = true;
                                                this.panelmensaje.Visible = true;
                                                this.imagenbien.Visible = false;*/
                        
                        this.panelLista.Visible = false;
                    }
                    else {
                        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());
                        /*
                        this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                                                this.lblMensajeOperationresult.Text = "NO SE PUEDO ELIMINAR EL MOVIMIENTO DE CAJA RELACIONADO CON EL CHEQUE NÚMERO: " + numcheque.ToString().ToUpper();
                                                this.lblMensajeException.Text = exception.Message;
                                                this.imagenmal.Visible = true;
                                                this.panelmensaje.Visible = true;
                                                this.imagenbien.Visible = false;*/
                        
                        this.panelLista.Visible = false;
                    }

                }
                finally
                {
                    conGaribay.Close();
                }


            }
            else{
                /*
                this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                                if(marcados==0){
                                    this.lblMensajeOperationresult.Text = "DEBE SELECCIONAR UN CHEQUE PARA PODERLO ELIMINAR";
                                }
                                else{
                                    this.lblMensajeOperationresult.Text = "DEBE SELECCIONAR SÓLO UN CHEQUE PARA PODERLO ELIMINAR";
                                }
                                this.lblMensajeException.Text = "";     
                                this.imagenmal.Visible = true;
                                this.panelmensaje.Visible = true;
                                this.imagenbien.Visible = false;*/
                
                this.panelLista.Visible = false;

            }

            
            
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            this.showHideColumns();
            this.actualizaFiltros();
            this.sacaTotales();
        }

        protected void btnPrintList_Click(object sender, EventArgs e)
        {
            string sFileName = "Cheques" + Utils.Now.ToString("dd-MM-yyyy") + ".xls";
            ExportToExcel(sFileName, ref this.gridCheques);
        }

        private void ExportToExcel(string strFileName, ref GridView dg)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", strFileName));
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            this.gridCheques.AllowPaging = false; //all the movements in one page.
            this.gridCheques.DataBind();
            Page pPage = new Page();
            HtmlForm fForm = new HtmlForm();
            this.gridCheques.EnableViewState = false;
            Label lbltemp = new Label();
            lbltemp.Text = "<table><tr><td class='TableField'>Cuenta: </td><td colspan = '3'>";
            lbltemp.Text += this.ddlCuentasDeBanco.SelectedItem.Text + "</td></tr></table>";
            pPage.Controls.Add(lbltemp);
            pPage.Controls.Add(fForm);
            fForm.Attributes.Add("runat","server");
            //this.gridMovCuentasBanco.RenderControl(oHtmlTextWriter);
            this.gridCheques.BorderWidth = 1;
            fForm.Controls.Add(this.gridCheques);
            pPage.RenderControl(oHtmlTextWriter);
            this.gridCheques.AllowPaging = true;
            this.gridCheques.BorderWidth = 0;
            Response.Write(oStringWriter.ToString());
            Response.End();
        }

  
        protected void gridCheques_RowUpdating(object sender, GridViewUpdateEventArgs e )
        {

//             string sql = "update MovimientosCuentasBanco set chequecobrado = @pagado, fechacobrado=@fechacobrado where movbanID = @movbanID";
//             SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
//             this.gridCheques.Columns[1].Visible = true;
//             this.gridCheques.DataBind();
//          
//             SqlCommand cmdupdate = new SqlCommand(sql, conGaribay);
//             try
//             {
//                 //cmdupdate.Parameters.Clear();
//                 conGaribay.Open();
//              //   string fecha = e.NewValues["movbanID"].ToString();
//                 cmdupdate.Parameters.Add("@pagado", SqlDbType.Bit).Value = e.NewValues["chequecobrado"];
//                 cmdupdate.Parameters.Add("@fechacobrado", SqlDbType.DateTime).Value = DateTime.Parse(e.NewValues["fechacobrado"].ToString());
//                 cmdupdate.Parameters.Add("@movbanID", SqlDbType.Int).Value = this.gridCheques.Rows[e.RowIndex].Cells[1].Text;
//                 int numregistros = cmdupdate.ExecuteNonQuery();
//             }
//             catch(Exception exc){
//             }
//             finally{
//                 this.gridCheques.EditIndex = -1;
//                 conGaribay.Close();
//                 this.gridCheques.Columns[1].Visible = false;
//                 this.DataBind();
//             }
            string fecha;
            //fecha = DateTime.Parse(e.NewValues["fechacobrado"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
              
              SqlDataSource1.UpdateParameters.Add("@chequecobrado",TypeCode.Boolean,e.NewValues["chequecobrado"].ToString());
              SqlDataSource1.UpdateParameters.Add("@fechacobrado", TypeCode.DateTime, Utils.converttoLongDBFormat(e.NewValues["fechacobrado"].ToString()));//.ToString("dd/MM/yyyy HH:mm:ss"));
              SqlDataSource1.UpdateParameters.Add("@movbanID",TypeCode.Int32,this.gridCheques.Rows[e.RowIndex].Cells[1].Text);
              SqlDataSource1.Update();
              this.gridCheques.EditIndex = -1;
              this.gridCheques.DataBind();

        }

        protected void gridCheques_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gridCheques.EditIndex = e.NewEditIndex;
            //this.gridCheques.DataBind();

        }

        protected void gridCheques_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gridCheques.EditIndex = -1;

            this.gridCheques.DataBind();
        }
        protected void sacaTotales(){
            float cantidad=0f, totalencobrados=0f, totalennocobrados=0f,totalengirados=0f;
            int totalcobrados=0, totalnocobrados=0, totalgirados=0;
            foreach (GridViewRow row in gridCheques.Rows){
                cantidad = float.Parse(row.Cells[8].Text,NumberStyles.Currency);
                if(((CheckBox)(row.Cells[11].Controls[0])).Checked){//ESTA COBRADO
                    totalcobrados++;
                    totalencobrados+=cantidad;
                }
                else{
                    totalnocobrados++;
                    totalennocobrados += cantidad;
                }
                totalengirados +=cantidad;

            }
            totalengirados = totalencobrados + totalennocobrados;
            totalgirados = totalnocobrados + totalcobrados;
            this.lblChequesGirados.Text = totalgirados.ToString();
            this.lblChequesNoCobrados.Text = totalnocobrados.ToString();
            this.lblChequesCobrados.Text = totalcobrados.ToString();
            this.lblTotalCobrados.Text = string.Format("{0:c}", totalennocobrados);
            this.lblTotalGirados.Text = string.Format("{0:c}", totalengirados);
            this.lblTotalNoCobrados.Text = string.Format("{0:c}",totalennocobrados);

        }

        protected void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    

        protected void gridCheques_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "printCheque")
            {
                // Retrieve the row index stored in the 
                // CommandArgument property.
                int index = Convert.ToInt32(e.CommandArgument);
                string idtoprint;
                idtoprint=  gridCheques.DataKeys[index][0].ToString();
                string query = "SELECT     Bancos.bancoID FROM         Bancos INNER JOIN ";
                query += " CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID INNER JOIN ";
                query += "  MovimientosCuentasBanco ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco.cuentaID where MovimientosCuentasBanco.movbanID = @movbanID ";
                int banID = -1;
                SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand cmdsel = new SqlCommand(query, conGaribay);
                conGaribay.Open();
                try
                {
                    cmdsel.Parameters.Add("@movbanID", SqlDbType.Int).Value = int.Parse(idtoprint);
                    SqlDataReader read = cmdsel.ExecuteReader();
                    if (read.HasRows)
                    {
                        read.Read();
                        banID = int.Parse(read[0].ToString());

                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conGaribay.Close();
                }

                float fPosX = 0, fPosY = 0;
                HttpCookie cookie = Request.Cookies["PRINTCONF"];
                if (cookie == null)
                {
                    fPosX = 0;
                    fPosY = 0;
                }
                else
                {
                    try
                    {
                        float.TryParse(cookie[banID.ToString() + "posX"].ToString(), out fPosX);
                        float.TryParse(cookie[banID.ToString() + "posY"].ToString(), out fPosY);
                    }
                    catch (NullReferenceException exp)
                    {

                    }
                }

                byte [] bytes = PdfCreator.printCheque(int.Parse(idtoprint), PdfCreator.orientacionPapel.VERTICAL, PdfCreator.tamañoPapel.CARTA, fPosX, fPosY,this.UserID);
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=ImpresionCheque.pdf");
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
               
                // Add code here to add the item to the shopping cart.
            }

        }

    
    }
}
