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
using System.Globalization;
using System.Text;
using System.IO;

namespace Garibay
{
    public partial class WebForm2 : Garibay.BasePage
    {

        protected override void SavePageStateToPersistenceMedium(object state)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter swr = new StringWriter(sb);

            LosFormatter formatter = new LosFormatter();
            formatter.Serialize(swr, state);
            swr.Close();

            // Store the textual representation of ViewState in the 
            // database or elsewhere
            // The serialized view state is available via sb.ToString ()
            this.Session[this.Request.Url.ToString()] = sb.ToString();

        }
        protected override object LoadPageStateFromPersistenceMedium()
        {
            object objViewState;
            string strViewState = "";

            if (this.Session[this.Request.Url.ToString()] != null)
            {
                strViewState = this.Session[this.Request.Url.ToString()].ToString();
            }
            // Viewstate should be read from the database or  
            // elsewhere into strViewState
            LosFormatter formatter = new LosFormatter();
            try
            {
                objViewState = formatter.Deserialize(strViewState);
            }
            catch
            {
                throw new HttpException("Invalid viewstate");
            }
            return objViewState;
        }

        public WebForm2():base()
        {
            this.hasCalendar = true;
        }
        dsMovCajaChica.dtMovCajaChicaDataTable dtteibol = new dsMovCajaChica.dtMovCajaChicaDataTable();
        DataView dtv = new DataView();
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dtInicio = new DateTime();
            DateTime dtFin = new DateTime();
            if (!this.IsPostBack)
            {
                this.ddlBodegas.DataBind();
                this.ddlBodegas.SelectedValue = this.BodegaID.ToString();
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDECAJACHICA, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), "SE VISITÓ LA PÁGINA LISTA DE MOVIMIENTOS DE CAJA CHICA");
                this.btnEliminar.Visible = false;
                this.btnModificar.Visible = false;
                DateTime dtHoy = Utils.Now;
                dtInicio = new DateTime(dtHoy.Year, dtHoy.Month, 1);
                dtFin = Utils.Now;
                /*
                TimeSpan tsDays = dtFin - dtInicio;
                               dtFin = new DateTime(dtHoy.Year,dtHoy.Month,tsDays.Days);*/

                this.txtFecha1.Text = dtInicio.ToString("dd/MM/yyyy");
                this.txtFecha2.Text = dtFin.ToString("dd/MM/yyyy");
                //this.ddlCuentas.DataBind();
               // this.showHideColumns();
            }
            this.reloadGridView();
            this.showHideColumns();

            /*this.gridMovCuentasBanco.DataSourceID = "SqlDataSource1";*/

            if (this.panelMensaje.Visible == true) { this.panelMensaje.Visible = false; this.panelagregar.Visible = true; }


            if (this.gridMovCajaChica.SelectedIndex == -1) { this.btnEliminar.Visible = false; }
            else { this.btnEliminar.Visible = true; }
            this.compruebasecurityLevel();
           
          
        }
        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.btnAgregarNuevo.Visible = false;
                this.btnModificar.Visible = false;
                this.btnEliminar.Visible = false;
                this.gridMovCajaChica.Columns[0].Visible = false;
                this.btnActualizaStatus.Visible = false;
            }

        }

       
          
       

//        protected void btnModificar_Click(object sender, EventArgs e)
//         {
//             if (this.GVlistaMovCajaChica.SelectedIndex > -1)
//             {
//                 
//                 string strRedirect = "~/frmAddMovCajaChica.aspx";
//                 string datosaencriptar;
//                 datosaencriptar = "idtomodify=";
//                 datosaencriptar += this.GVlistaMovCajaChica.SelectedDataKey["movimientoID"].ToString();
//                 datosaencriptar += "&concepto=" + this.GVlistaMovCajaChica.SelectedDataKey["concepto"].ToString()+"&";                strRedirect += "?data=";
//                 strRedirect += Utils.encriptacadena(datosaencriptar);
//                 Response.Redirect(strRedirect, true);
//             }
//             else
//             {
//                 return;
//             }
//             
//         }


        


       private void reloadGridView()
        {
            DateTime dtInicio = DateTime.Parse(this.txtFecha1.Text, new CultureInfo("es-Mx"));
            DateTime dtFin = DateTime.Parse(this.txtFecha2.Text, new CultureInfo("es-Mx"));

            this.rellenadt(dtInicio, dtFin);
        }
       protected void rellenadt(DateTime fechainicio, DateTime fechafin)
       {
           try{
           
               // this.ddlCuentas.DataBind();
               float fSaldoInicial = 0, fSaldoFinal = 0;
               if (dbFunctions.fillDTMovCajaChica(fechainicio,fechafin,ref fSaldoInicial,ref fSaldoFinal,ref dtteibol, int.Parse(this.ddlBodegas.SelectedValue)))
               {
                               
                       this.gridMovCajaChica.DataSource = dtteibol;
                       this.gridMovCajaChica.DataSourceID = "";
                       

                       if (dtteibol.Rows.Count == 0)
                       {
                           fSaldoFinal =  fSaldoInicial;

                       }
                       this.lblSaldofinal.Text = string.Format("{0:c}", fSaldoFinal);
                       this.lblSaldoinicial.Text = string.Format("{0:c}", fSaldoInicial);
                       gridMovCajaChica.Columns[7].Visible = true;
                       this.panelsaldos.Visible = true;
                   }
                   this.gridMovCajaChica.DataBind();
               
               }


          catch (Exception exception)
           {
               Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(this.Session["USERID"].ToString()), exception.Message, this.Request.Url.ToString());

           }

       }
       private void showHideColumns()
       {
           foreach (DataControlField col in this.gridMovCajaChica.Columns)
           {
               ListItem item = this.cblColToShow.Items.FindByText(col.HeaderText);
               if (item != null)
               {
                   col.Visible = item.Selected;
               }
           }
       }

       protected void btnFiltrar_Click(object sender, EventArgs e)
       {
           this.showHideColumns();
           this.reloadGridView();
       }

       protected void btnEliminar_Click(object sender, EventArgs e)
       {

           string sError = "ERROR";
           int movID = int.Parse(this.gridMovCajaChica.SelectedDataKey[0].ToString());
           try
           {

               if (dbFunctions.deleteMovementdeCaja(movID, ref sError, int.Parse(this.Session["USERID"].ToString())))
               {
                   this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                   this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("MOVCAJADELETEDEXITO"), this.gridMovCajaChica.SelectedDataKey["movimientoID"].ToString());
                   this.lblMensajeException.Text = "";
                   this.imagenmal.Visible = false;
                   this.panelMensaje.Visible = true;
                   this.imagenbien.Visible = true;
                   this.btnEliminar.Visible = false;
                   this.btnModificar.Visible = false;
                   Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDECAJACHICA, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ EL MOVIMIENTO DE CAJA CHICA NÚMERO " + this.gridMovCajaChica.SelectedDataKey["movimientoID"].ToString()));
                   //Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), "SE ELIMINÓ EL MOVIMIENTO DE CAJA NÚMERO " + this.gridMovCajaChica.SelectedDataKey["movimientoID"].ToString(), this.Request.Url.ToString());
                   //int movBancoOrigen = -1,  movCajaOrigen = -1;
                   //                if() 
                   // 
                   //                    if(read["movBanID"]!=null && read["movBanID"].ToString().Length>0){ //EL MOVIMIENTO ORIGEN FUE  MOV DE BANCO
                   //                        movBancoOrigen = int.Parse(read["movBanID"].ToString());
                   //                       
                   //                        dbFunctions.deleteMovementdeBanco(movBancoOrigen,sError,this.UserID,int.Parse(this.gridMovCajaChica.SelectedDataKey["cuentaID"].ToString()));
                   // 
                   //                    }
                   //                    else{
                   //                        //EL MOV ORIGEN FUE OTRO MOV DE CAJA
                   //                        movBancoOrigen = int.Parse(read["movBanID"].ToString());
                   //                        dbFunctions.deleteMovementdeCaja()
                   //                    }
                   //                }
                   this.gridMovCajaChica.SelectedIndex = -1;
                   this.gridMovCajaChica.DataBind();
                   this.panelagregar.Visible = false;


               }
               else
               {
                   this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                   this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("MOVCAJADELETEDFAILED"), this.gridMovCajaChica.SelectedDataKey["movimientoID"].ToString().ToUpper());
                   this.lblMensajeException.Text = sError;
                   this.imagenmal.Visible = true;
                   this.panelMensaje.Visible = true;
                   this.imagenbien.Visible = false;
                   Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), sError, this.Request.Url.ToString());
               }
           }
           catch(Exception ex){
               Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, this.UserID, "ERROR AL ELIMINAR MOV CAJA CHICA. EX " + ex.Message, this.Request.Url.ToString());
           }

       }

       protected void gridMovCajaChica_SelectedIndexChanged(object sender, EventArgs e)
       {
           this.btnEliminar.Visible = true;
           this.btnModificar.Visible = true;
           this.btnEliminar.Attributes.Add("onclick", "return confirm('¿Desea eliminar el Movimiento Caja chica número: " + this.gridMovCajaChica.SelectedDataKey["movimientoID"].ToString().ToUpper() + "?')");
           if (this.panelMensaje.Visible == true)
           {
             this.panelMensaje.Visible = false;
           }

       }

       protected void btnModificar_Click(object sender, EventArgs e)
       {
           if (this.gridMovCajaChica.SelectedIndex > -1)
           {

               string strRedirect = "~/frmAddMovCajaChica.aspx";
               string datosaencriptar;
               datosaencriptar = "idtomodify=";
               datosaencriptar += this.gridMovCajaChica.SelectedDataKey["movimientoID"].ToString() + "&";
               //datosaencriptar += "&concepto=" + this.gridMovCajaChica.SelectedDataKey["concepto"].ToString() + "&"; 
               strRedirect += "?data=";
               strRedirect += Utils.encriptacadena(datosaencriptar);
               Response.Redirect(strRedirect, true);
           }
           else
           {
               return;
           }

       }
       private void ExportToExcel(string strFileName, GridView dg)
       {
           Response.Clear();
           Response.Buffer = true;
           Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", strFileName));
           Response.ContentType = "application/ms-excel";
           Response.Charset = "";
           this.EnableViewState = false;
           System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
           System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
           string sHeader = "<table><tr><td><b>";
           //sHeader += "Cuenta: </b></td><td colspan=\"3\">" + "AXXXX" + "</td></tr><tr><td><b>";
           sHeader += "Periodo: </b></td><td>" + this.txtFecha1.Text;
           sHeader += "</td><td><b>A:</b></td><td>" + this.txtFecha2.Text + "</td></tr>";
           sHeader += "<tr><td><b>Saldo Inicial:</b></td><td>" + this.lblSaldoinicial.Text;
           sHeader += "</td><td><b>Saldo Final:</b></td><td>" + this.lblSaldofinal.Text;
           sHeader += "</td></tr></table>";
           this.gridMovCajaChica.AllowPaging = false; //all the movements in one page.
           this.gridMovCajaChica.Columns[0].Visible = false; //hide the select button
           this.gridMovCajaChica.DataBind();
           Page pPage = new Page();
           HtmlForm fForm = new HtmlForm();
           this.gridMovCajaChica.EnableViewState = false;
           this.gridMovCajaChica.Columns[13].Visible  = this.gridMovCajaChica.Columns[1].Visible  = this.gridMovCajaChica.Columns[0].Visible = false;
           pPage.Controls.Add(fForm);
           Label lblHeader = new Label();
           lblHeader.Text = sHeader;
           //this.gridMovCuentasBanco.RenderControl(oHtmlTextWriter);
           fForm.Controls.Add(lblHeader);
           fForm.Controls.Add(this.gridMovCajaChica);
           pPage.RenderControl(oHtmlTextWriter);
           this.gridMovCajaChica.AllowPaging = true;
           this.gridMovCajaChica.Columns[0].Visible = true;
           this.gridMovCajaChica.Columns[13].Visible = this.gridMovCajaChica.Columns[1].Visible = this.gridMovCajaChica.Columns[0].Visible = true;
           Response.Write(oStringWriter.ToString());
           Response.End();
       }

       protected void btnImprimir_Click(object sender, EventArgs e)
       {
           string sFileName = "ReporteDeMovCajaChica" + Utils.Now.ToString("dd-MM-yyyy") + ".xls";
           ExportToExcel(sFileName, this.gridMovCajaChica);
       }

       protected void btnAgregarNuevo_Click(object sender, EventArgs e)
       {
           Response.Redirect("~/frmAddMovCajaChica.aspx");
       }

  
       protected void btnActualizaStatus_Click(object sender, EventArgs e)
       {
           string query = "update MovimientosCaja set cobrado = @cobrado where movimientoID = @movID";
           SqlConnection conUpdate = new SqlConnection(myConfig.ConnectionInfo);
           SqlCommand cmdUpdate = new SqlCommand(query, conUpdate);
           conUpdate.Open();
           try
           {
               foreach (GridViewRow row in this.gridMovCajaChica.Rows)
               {
                   CheckBox aux = ((CheckBox)(row.Cells[12].FindControl("chkCobrado")));
                   cmdUpdate.Parameters.Clear();
                   cmdUpdate.Parameters.Add("@cobrado", SqlDbType.Bit).Value = aux.Checked ? 1 : 0;
                   cmdUpdate.Parameters.Add("@movID", SqlDbType.Int).Value = int.Parse(this.gridMovCajaChica.DataKeys[row.RowIndex]["movimientoID"].ToString());
                   cmdUpdate.ExecuteNonQuery();

               }

           }
           catch (Exception ex)
           {
               Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR  ACTUALIZANDO EL ESTADO DEL MOV CAJA CHICA, LA EXC ES: " + ex.Message, this.Request.Url.ToString());
           }
           finally
           {
               conUpdate.Close();
           }
           
       }

       protected void gridMovCajaChica_PreRender(object sender, EventArgs e)
       {
           Utils.CellOnRedIfLessThanZero(8, ref this.gridMovCajaChica);
       }

       protected void drpSubCatalogo_DataBound(object sender, EventArgs e)
       {
           int newValue = -1;
           ((DropDownList)(sender)).Items.Insert(0, new ListItem("", newValue.ToString()));
          
       }

       protected void gridMovCajaChica_RowEditing(object sender, GridViewEditEventArgs e)
       {
           try
           {
               ((GridView)(sender)).EditIndex = e.NewEditIndex;
               this.reloadGridView();
               TextBox txtFecha = ((TextBox)(this.gridMovCajaChica.Rows[e.NewEditIndex].Cells[2].FindControl("txtFecha")));
               txtFecha.Text = Utils.converttoshortFormatfromdbFormat(txtFecha.Text);

               DropDownList drpCatalogo = ((DropDownList)(this.gridMovCajaChica.Rows[e.NewEditIndex].Cells[4].FindControl("drpdlCatalogo")));
               if (drpCatalogo != null)
               {
                   drpCatalogo.SelectedValue = this.gridMovCajaChica.DataKeys[e.NewEditIndex]["catalogoMovBancoInternoID"].ToString();
               }
               DropDownList drpSubCatalogo = ((DropDownList)(this.gridMovCajaChica.Rows[e.NewEditIndex].Cells[5].FindControl("drpSubCatalogo")));
               if (drpSubCatalogo != null)
               {
                   drpSubCatalogo.SelectedValue = this.gridMovCajaChica.DataKeys[e.NewEditIndex]["subCatalogoMovBancoInternoID"].ToString();
               }
           }
           catch(Exception ex){
               Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "ERROR AL PRESELECCIONAR EL VALOR DE COMBOS EN LISTA MOV CAJA CHICA " + ex.Message, this.Request.Url.ToString());
           }
       }

       protected void gridMovCajaChica_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
       {
           ((GridView)(sender)).EditIndex = -1;
           this.reloadGridView();

       }

       protected void gridMovCajaChica_RowUpdating(object sender, GridViewUpdateEventArgs e)
       {
           try
           {
               dsMovCajaChica.dtMovCajaChicaDataTable dt = new dsMovCajaChica.dtMovCajaChicaDataTable();
               dsMovCajaChica.dtMovCajaChicaRow row = dt.NewdtMovCajaChicaRow();
               row.movimientoID = int.Parse(this.gridMovCajaChica.DataKeys[e.RowIndex]["movimientoID"].ToString());

               TextBox txt=(TextBox)(this.gridMovCajaChica.Rows[e.RowIndex].Cells[7].Controls[0]);
               TextBox txt2 = (TextBox)(this.gridMovCajaChica.Rows[e.RowIndex].Cells[8].Controls[0]);
               
               row.cargo = txt.Text.Length > 0 ? double.Parse(txt.Text) : 0;
               row.abono = txt2.Text.Length > 0 ? double.Parse(txt2.Text) : 0;
               row.catalogoMovBancoInternoID = int.Parse(((DropDownList)(this.gridMovCajaChica.Rows[e.RowIndex].Cells[5].FindControl("drpdlCatalogo"))).SelectedValue);
               row.subCatalogoMovBancoInternoID = int.Parse(((DropDownList)(this.gridMovCajaChica.Rows[e.RowIndex].Cells[6].FindControl("drpSubCatalogo"))).SelectedValue);
               row.cobrado = (((CheckBox)(this.gridMovCajaChica.Rows[e.RowIndex].Cells[13].Controls[1])).Checked);
               row.facturaOlarguillo = (((TextBox)(this.gridMovCajaChica.Rows[e.RowIndex].Cells[11].Controls[0])).Text);
               row.fecha = DateTime.Parse(Utils.converttoLongDBFormat((((TextBox)(this.gridMovCajaChica.Rows[e.RowIndex].Cells[3].FindControl("txtFecha"))).Text)));
               row.nombre = (((TextBox)(this.gridMovCajaChica.Rows[e.RowIndex].Cells[4].Controls[0])).Text);
               row.numCabezas = ((TextBox)(this.gridMovCajaChica.Rows[e.RowIndex].Cells[12].Controls[0])).Text.Length > 0 ? double.Parse(((TextBox)(this.gridMovCajaChica.Rows[e.RowIndex].Cells[12].Controls[0])).Text) : 0;
               row.Observaciones = ((TextBox)(this.gridMovCajaChica.Rows[e.RowIndex].Cells[10].Controls[0])).Text;
               row.updateTS = Utils.Now;
               row.userID = int.Parse(this.Session["userID"].ToString());
               string sError = "";
               if (dbFunctions.updateMovementdeCajaChica(ref row, row.movimientoID, ref sError, row.userID))
               {
                                  SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                                  SqlCommand comm = new SqlCommand();
                                  try
                                  {
                                      conn.Open();
                                      comm.Connection = conn;
                                      comm.CommandText = "SELECT     MovimientosCuentasBanco.cuentaID, MovimientoOrigen.movbanID FROM         MovimientosCaja INNER JOIN MovimientoOrigen ON MovimientosCaja.movOrigenID = MovimientoOrigen.movOrigenID INNER JOIN MovimientosCuentasBanco ON MovimientoOrigen.movbanID = MovimientosCuentasBanco.movbanID where (MovimientoOrigen.movimientoID = @movID)";
                                      comm.Parameters.Clear();
                                      comm.Parameters.Add("@movID", SqlDbType.Int).Value = row.movimientoID;
                                      dsMovBanco.dtMovBancoDataTable table = new dsMovBanco.dtMovBancoDataTable();
                                      dsMovBanco.dtMovBancoRow rowban = table.NewdtMovBancoRow();
                                      
                                      SqlDataReader reader = comm.ExecuteReader();
                                      if (reader.HasRows && reader.Read()) //EL ORIGEN FUE UN MOV BANCO
                                      {
                                          rowban.nombre = row.nombre;
                                          rowban.fecha = row.fecha;
                                          if (row.cargo > 0)
                                          {
                                              rowban.abono = row.cargo;
                                              rowban.cargo = row.abono;
                                          }
                                          else
                                          {
                                              rowban.cargo = row.abono;
                                              rowban.abono = row.cargo;
                                          }
                                          // = "TRASPASO DE UNA CUENTA DE BANCO";
                                          rowban.catalogoMovBancoInternoID = row.catalogoMovBancoInternoID;
                                          rowban.subCatalogoMovBancoInternoID = row.IssubCatalogoMovBancoInternoIDNull() ? -1 : row.subCatalogoMovBancoInternoID;
                                          rowban.numCabezas = 0;
                                          rowban.facturaOlarguillo = "";
                                          //rowCajaChica.bodegaID = int.Parse(this.ddlCajaDestino.SelectedValue);
                                          //Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.UPDATE, this.UserID, "Se encontró un movimiento destino para actualizar", this.Request.Url.ToString());
                                          int iMovToUpt = int.Parse(reader["movbanID"].ToString());
                                          rowban.movBanID = iMovToUpt;
                                          rowban.cuentaID = int.Parse(reader["cuentaID"].ToString());

                                          if (!dbFunctions.updateMovementdeBanco(ref rowban,rowban.movBanID, ref sError, row.userID, rowban.cuentaID))
                                          {
                                              Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "No se pudo actualizar cuentaID: " + rowban.cuentaID + " movID: " + rowban.movBanID, this.Request.Url.ToString());
                                          }

                                        
                                      }
                                      else
                                      {
                                          //CHECK IF THERE IS AT LEAST A MOV CAJA CHICA.
                                          conn.Close();
                                          conn.Open();
                                          comm.Connection = conn;
                                          comm.CommandText = "SELECT     MovimientoOrigen.movimientoID FROM         MovimientosCaja INNER JOIN MovimientoOrigen ON MovimientosCaja.movOrigenID = MovimientoOrigen.movOrigenID  where (MovimientoOrigen.movimientoID = @movID)";
                                          comm.Parameters.Clear();
                                          comm.Parameters.Add("@movID", SqlDbType.Int).Value = row.movimientoID;
                                          SqlDataReader readermov = comm.ExecuteReader();
                                          if (readermov.HasRows && readermov.Read())
                                          {
                                              //Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.UPDATE, this.UserID, "Se encontró un movimiento caja chica destino para actualizar", this.Request.Url.ToString());
                                              int iMovToUpt = int.Parse(readermov["movimientoID"].ToString());
                                              row.movimientoID = iMovToUpt;
                                              //rowCajaChica = int.Parse(reader["cuentaID"].ToString());

                                              if (!dbFunctions.updateMovementdeCajaChica(ref row, row.movimientoID, ref sError, this.UserID))
                                              {
                                                  Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "No se pudo actualizar el mov caja chica: " + row.movimientoID.ToString(), this.Request.Url.ToString());
                                              }
                                          }
                                      }
               
                                  }
                                  catch (System.Exception ex)
                                  {
                                      Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, this.UserID, "Error obteniendo mov origen ex: " + ex.Message, this.Request.Url.ToString());
                                  }
                                  finally
                                  {
                                      conn.Close();
                                  }
                              }

               this.gridMovCajaChica.EditIndex = -1;
               this.reloadGridView();
           }
           catch (Exception ex)
           {
               Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, int.Parse(this.Session["userID"].ToString()), "EL ERROR SE DIO CUANDO SE TRATABA DE MODIFICAR EL MOV DE BANCAJA CHICA. LA EXC FUE: " + ex.Message, this.Request.Url.ToString());

           }
           
       }

       protected void gridMovCajaChica_RowDataBound(object sender, GridViewRowEventArgs e)
       {
           LinkButton linkDel = (LinkButton)e.Row.Cells[1].FindControl("lnkDelete");
           if (linkDel != null)
           {
               linkDel.Attributes.Add("onclick", "return confirm('¿Desea eliminar el Movimiento Caja chica número: " + this.gridMovCajaChica.DataKeys[e.Row.RowIndex]["movimientoID"].ToString() + "?')");               
           }
           if (e.Row.RowType == DataControlRowType.DataRow)
           {
               SqlConnection conSacaTickets = new SqlConnection(myConfig.ConnectionInfo);
               string query = " SELECT  Boletas_Anticipos.Ticket, Boletas_Anticipos.boletaID FROM Boletas_Anticipos INNER JOIN Anticipos ON Boletas_Anticipos.anticipoID = Anticipos.anticipoID where Anticipos.movimientoID = @movID";
               SqlCommand cmdSacaTickets = new SqlCommand(query, conSacaTickets);
               conSacaTickets.Open();
               SqlDataReader read;
               try
               {
                   cmdSacaTickets.Parameters.Clear();
                   cmdSacaTickets.Parameters.Add("@movID", SqlDbType.Int).Value = int.Parse(this.gridMovCajaChica.DataKeys[e.Row.RowIndex]["movimientoID"].ToString());
                   read = cmdSacaTickets.ExecuteReader();
                   Label listaBol=(Label)e.Row.Cells[22].FindControl("Label5");
                   while (read.Read())
                   {
                       String sQueryBol = "idtomodify=" + read["boletaID"].ToString();
                       sQueryBol = Utils.GetEncriptedQueryString(sQueryBol);

                       listaBol.Text += "<a href=\"frmAddModifyBoletas.aspx" + sQueryBol + "\">" + read["Ticket"].ToString() + "</a> - ";
                   }
                   read.Close();
                   cmdSacaTickets.CommandText = "SET CONCAT_NULL_YIELDS_NULL OFF; ";
                   cmdSacaTickets.CommandText += "SELECT     LTRIM(Bancos.nombre + ' ' + CuentasDeBanco.NumeroDeCuenta + ' ' + CAST(MovimientosCuentasBanco_1.numCheque AS varchar(25))  + CAST(Bodegas.bodega AS varchar(50))) AS origen, MovimientosCaja_1.movimientoID, Bodegas.bodega,MovimientosCuentasBanco_1.movBanID, MovimientosCuentasBanco_1.cuentaID   FROM         MovimientosCaja INNER JOIN Bodegas ON MovimientosCaja.bodegaID = Bodegas.bodegaID RIGHT OUTER JOIN MovimientosCaja AS MovimientosCaja_1 INNER JOIN MovimientoOrigen ON MovimientosCaja_1.movOrigenID = MovimientoOrigen.movOrigenID ON  MovimientosCaja.movimientoID = MovimientoOrigen.movimientoID LEFT OUTER JOIN Bancos INNER JOIN CuentasDeBanco INNER JOIN MovimientosCuentasBanco AS MovimientosCuentasBanco_1 ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco_1.cuentaID ON  Bancos.bancoID = CuentasDeBanco.bancoID ON MovimientoOrigen.movbanID = MovimientosCuentasBanco_1.movbanID WHERE     (MovimientosCaja_1.movimientoID = @movimientoID)";
                   cmdSacaTickets.Parameters.Clear();
                   cmdSacaTickets.Parameters.Add("@movimientoID", SqlDbType.Int).Value = int.Parse(this.gridMovCajaChica.DataKeys[e.Row.RowIndex]["movimientoID"].ToString());

                   read = cmdSacaTickets.ExecuteReader();
                   if (read.HasRows && read.Read())
                   {
                       HyperLink link = (HyperLink)e.Row.Cells[16].FindControl("HyperLink1");
                       if (link != null)
                       {
                           if (read["movBanID"] != null && read["movBanID"].ToString().Length>0)
                           {
                               string parameter, ventanatitle = "DETALLES DE MOVIMIENTO DE CUENTA";
                               // String pathArchivotemp = PdfCreator.printLiquidacion(0, PdfCreator.tamañoPapel.CARTA, PdfCreator.orientacionPapel.VERTICAL, ref this.gvBoletas, ref gvAnticipos, ref gvPagosLiquidacion);
                               string datosaencriptar;
                               datosaencriptar = "id=";
                               datosaencriptar += read["movBanID"];
                               datosaencriptar += "&";
                               parameter = "javascript:url('";
                               parameter += "frmMovBancoDetails.aspx?data=";
                               parameter += Utils.encriptacadena(datosaencriptar);
                               parameter += "', '";
                               parameter += ventanatitle;
                               parameter += "',0,250,400,800); return false;";
                               link.Attributes.Add("onClick", parameter);
                              
                              
                               
                           }
                           link.NavigateUrl = this.Request.Url.ToString();
                           link.Text = read["origen"].ToString();
                       }
                       
                   }
                   else{
                        HyperLink link = (HyperLink)e.Row.Cells[16].FindControl("HyperLink1");
                        link.Visible = false;
                   }




                   read.Close();
                   query = " SELECT  liquidacionID FROM  PagosLiquidacion WHERE movimientoID=@movID";
                   SqlCommand cmdSacaLiq = new SqlCommand(query, conSacaTickets);

                   cmdSacaLiq.Parameters.Clear();
                   cmdSacaLiq.Parameters.Add("@movID", SqlDbType.Int).Value = int.Parse(this.gridMovCajaChica.DataKeys[e.Row.RowIndex]["movimientoID"].ToString());
                   read = cmdSacaLiq.ExecuteReader();
                   
                   HyperLink linkliq = (HyperLink)e.Row.Cells[19].FindControl("HyperLink2");
                   if(read.HasRows&&read.Read())
                   {
                       if (linkliq != null)
                        {
                           //liquidacion
                            String sQuery = "liqID=" + read["liquidacionID"].ToString();
                            sQuery = Utils.GetEncriptedQueryString(sQuery);
                            String strRedirect = "~/frmLiquidacion2010.aspx";
                            strRedirect += sQuery;
                            linkliq.NavigateUrl = strRedirect;
                            linkliq.Text = read["liquidacionID"].ToString();
                            read.Close();
                           //boletas de la liquidacion
                            query = "SELECT Boletas.Ticket, Liquidaciones_Boletas.BoletaID FROM Liquidaciones_Boletas INNER JOIN Boletas ON Liquidaciones_Boletas.BoletaID = Boletas.boletaID WHERE (Liquidaciones_Boletas.LiquidacionID = @Liq)";
                            SqlCommand cmdSacaBol_Liq = new SqlCommand(query, conSacaTickets);
                            cmdSacaBol_Liq.Parameters.Clear();
                            cmdSacaBol_Liq.Parameters.Add("@Liq", SqlDbType.Int).Value = int.Parse(linkliq.Text);
                            read = cmdSacaBol_Liq.ExecuteReader();
                            Label lblboletas = (Label)e.Row.Cells[20].FindControl("Label4");
                            lblboletas.Text = "";
                            int cont = 1;
                            if (read.HasRows)
                            {
                                while(read.Read())
                                {
                                    String sQueryBol = "idtomodify=" + read["BoletaID"].ToString();
                                    sQueryBol = Utils.GetEncriptedQueryString(sQueryBol);

                                    lblboletas.Text += "<a href=\"frmAddModifyBoletas.aspx" + sQueryBol + "\">" + read["Ticket"].ToString() + "</a>-";
                                    if ((cont++)%10 == 0)
                                    {
                                        lblboletas.Text += " ";
                                    }
                                    

                                }
                            }
                        }
                       else
                       {
                           if (linkliq != null)
                           linkliq.Visible=false;
                       }
                   }
                   else
                   {
                       if(linkliq != null)
                        linkliq.Visible = false;
                   }
                   
                   read.Close();
                   query = "SELECT  Anticipos.anticipoID FROM MovimientosCaja INNER JOIN Anticipos ON MovimientosCaja.movimientoID = Anticipos.movimientoID WHERE MovimientosCaja.movimientoID=@movID";
                   SqlCommand cmdSacaAnt = new SqlCommand(query, conSacaTickets);
                   
                   cmdSacaAnt.Parameters.Clear();
                   cmdSacaAnt.Parameters.Add("@movID", SqlDbType.Int).Value = int.Parse(this.gridMovCajaChica.DataKeys[e.Row.RowIndex]["movimientoID"].ToString());
                   read = cmdSacaAnt.ExecuteReader();
                   
                   Label linkAnt = (Label)e.Row.Cells[21].FindControl("Label7");
                   if (linkAnt != null)
                   {
                       linkAnt.Text = "";
                       if(read.HasRows&&read.Read())
                       {
                           linkAnt.Text = read["anticipoID"].ToString();
                       }
                   }
                   read.Close();
               }
               catch (Exception ex)
               {
                   Logger.Instance.LogException(Logger.typeUserActions.SELECT, "ERROR AL TRATAR DE MOSTRAR LAS BOLETAS RELACIONADAS CON UN ANTICIPO EN CAJA CHICA.", ref ex);

               }
               finally
               {
                   conSacaTickets.Close();
               }
           }
       }

       protected void gridMovCajaChica_RowDeleting(object sender, GridViewDeleteEventArgs e)
       {
           string sError = "ERROR";
           int movID = int.Parse(this.gridMovCajaChica.DataKeys[e.RowIndex]["movimientoID"].ToString());
           try
           {

               if (dbFunctions.deleteMovementdeCaja(movID, ref sError, int.Parse(this.Session["USERID"].ToString())))
               {
                   this.lblMensajetitle.Text = myConfig.StrFromMessages("EXITO");
                   this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("MOVCAJADELETEDEXITO"), this.gridMovCajaChica.DataKeys[e.RowIndex]["movimientoID"].ToString());
                   this.lblMensajeException.Text = "";
                   this.imagenmal.Visible = false;
                   this.panelMensaje.Visible = true;
                   this.imagenbien.Visible = true;
                   this.btnEliminar.Visible = false;
                   this.btnModificar.Visible = false;
                   Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDECAJACHICA, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), ("SE ELIMINÓ EL MOVIMIENTO DE CAJA CHICA NÚMERO " + this.gridMovCajaChica.DataKeys[e.RowIndex]["movimientoID"].ToString()));
                   //Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), "SE ELIMINÓ EL MOVIMIENTO DE CAJA NÚMERO " + this.gridMovCajaChica.DataKeys[e.RowIndex]["movimientoID"].ToString(), this.Request.Url.ToString());
                   this.gridMovCajaChica.SelectedIndex = -1;
                   this.gridMovCajaChica.DataBind();
                   this.panelagregar.Visible = false;


               }
               else
               {
                   this.lblMensajetitle.Text = myConfig.StrFromMessages("FALLO");
                   this.lblMensajeOperationresult.Text = string.Format(myConfig.StrFromMessages("MOVCAJADELETEDFAILED"), this.gridMovCajaChica.DataKeys[e.RowIndex]["movimientoID"].ToString());
                   this.lblMensajeException.Text = sError;
                   this.imagenmal.Visible = true;
                   this.panelMensaje.Visible = true;
                   this.imagenbien.Visible = false;
                   Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, int.Parse(this.Session["USERID"].ToString()), sError, this.Request.Url.ToString());
               }
           }
           catch (Exception ex)
           {
               Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, this.UserID, "ERROR AL ELIMINAR MOV CAJA CHICA. EX " + ex.Message, this.Request.Url.ToString());
           }


       }

   

    }
}

