using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;

namespace Garibay
{
    public partial class frmDescargaTmpFile :Garibay.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    if (Request.QueryString["data"] != null)
                    {
                        if (this.loadqueryStrings(Request.QueryString["data"].ToString()))
                        {
                            string path = "";
                            Byte[] bytes = null;
                            if (myQueryStrings["solID"] != null)
                            { // ES SOLICITUD
                                if (myQueryStrings["impsol"] != null)
                                {
                                    path = FormatosPdf.imprimeSolicitud(int.Parse(myQueryStrings["solID"].ToString()));
                                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, this.UserID, "IMPRIMIO LA SOLICITUD: " + myQueryStrings["solID"].ToString());
                                }

                                if ((myQueryStrings["impSol2010"] != null))
                                {
                                    bytes = FormatosPdf.imprimeSolicitud2010(int.Parse(myQueryStrings["solID"].ToString()));
                                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, this.UserID, "IMPRIMIO LA SOLICITUD: " + myQueryStrings["solID"].ToString());
                                }
                                if (myQueryStrings["impcont"] != null)
                                {
                                    bytes = FormatosPdf.imprimeContrato(int.Parse(myQueryStrings["solID"].ToString()), this.UserID);
                                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, this.UserID, "IMPRIMIO EL CONTRATO DE LA SOLICITUD: " + myQueryStrings["solID"].ToString());
                                }
                                if (myQueryStrings["impcar"] != null)
                                {
                                    bytes = FormatosPdf.imprimeCaratulaAnexaSola(int.Parse(myQueryStrings["solID"].ToString()), this.UserID);
                                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, this.UserID, "IMPRIMIO LA CARÁTULA DE LA SOLICITUD: " + myQueryStrings["solID"].ToString());
                                }
                                if (myQueryStrings["impPagare"] != null)
                                {
                                    /*
                                     * filename=" + sFileName + "&ContentType=application/pdf&";
                datosaencriptar = datosaencriptar + "creditoID=" + actualRow.creditoID.ToString() +"&";
                datosaencriptar += "impPagare=1&monto=" + actualRow.total.ToString() + "&";
                string URLcomplete = sURL + "?data=";
                                     */
                                    int solicitud = -1;
                                    if (myQueryStrings["creditoID"] != null)
                                    {
                                        SqlCommand comm = new SqlCommand("SELECT solicitudID FROM Solicitudes WHERE (creditoID = @creditoID)");
                                        comm.CommandText = dbFunctions.UpdateSDSForSisBanco(comm.CommandText);
                                        comm.Parameters.Add("@creditoID", System.Data.SqlDbType.Int).Value = int.Parse(myQueryStrings["creditoID"].ToString());
                                        solicitud = dbFunctions.GetExecuteIntScalar(comm, -1);
                                        if (solicitud != -1)
                                        {
//                                            Solicitudes sol = Solicitudes.Get(solicitud);
                                            Solicitudes sol;
                                            if (this.IsSistemBanco)
                                                sol = SolicitudesSisBancos.Get(solicitud);
                                            else
                                                sol = Solicitudes.Get(solicitud);
                                            if (this.myQueryStrings["fecha"] == null)
                                            {
                                                this.myQueryStrings.Add("fecha", ((DateTime)sol.Fecha).ToString("yyyy/MM/dd"));
                                            }
                                            //else
                                              //  this.myQueryStrings["fecha"] = ((DateTime)sol.Fecha).ToString("yyyy/MM/dd");
                                            
                                            if (this.myQueryStrings["monto"] == null)
                                            {
                                                this.myQueryStrings.Add("monto", sol.Monto.ToString());
                                            }
                                            //else
                                            //    this.myQueryStrings["monto"] = sol.Monto.ToString();
                                            
                                            DateTime fecha;
                                            if (myQueryStrings["fecha"] != null && DateTime.TryParse(myQueryStrings["fecha"].ToString(), out fecha))
                                            {
                                                DateTime FechaPagare;
                                                if (myQueryStrings["fechaPagare"] != null && DateTime.TryParse(myQueryStrings["fechaPagare"].ToString(), out FechaPagare))
                                                {
                                                    bytes = FormatosPdf.imprimePagare(solicitud, this.UserID, double.Parse(myQueryStrings["monto"].ToString()), fecha, FechaPagare, true);
                                                }
                                                else
                                                {
                                                    bytes = FormatosPdf.imprimePagare(solicitud, this.UserID, double.Parse(myQueryStrings["monto"].ToString()), fecha);
                                                }
                                            }
                                            else
                                            {
                                                bytes = FormatosPdf.imprimePagare(solicitud, this.UserID, double.Parse(myQueryStrings["monto"].ToString()));
                                            }
                                        }
                                        else
                                        {
                                            if (int.TryParse(myQueryStrings["creditoID"].ToString(), out solicitud))
                                            {
                                                DateTime fecha;
                                                if (myQueryStrings["fecha"] != null && DateTime.TryParse(myQueryStrings["fecha"].ToString(), out fecha))
                                                {
                                                    DateTime FechaPagare;
                                                    if(myQueryStrings["fechaPagare"] != null && DateTime.TryParse(myQueryStrings["fechaPagare"].ToString(), out FechaPagare))
                                                    {
                                                        bytes = FormatosPdf.imprimePagareCredito(solicitud, this.UserID, double.Parse(myQueryStrings["monto"].ToString()), fecha, FechaPagare, true);
                                                    }
                                                    else
                                                    {
                                                        bytes = FormatosPdf.imprimePagareCredito(solicitud, this.UserID, double.Parse(myQueryStrings["monto"].ToString()), fecha);
                                                    }
                                                }
                                                else
                                                {
                                                    bytes = FormatosPdf.imprimePagareCredito(solicitud, this.UserID, double.Parse(myQueryStrings["monto"].ToString()));
                                                }

                                            }
                                            
                                        }
                                        
                                    }
                                    else
                                    {
                                        solicitud = int.Parse(myQueryStrings["solID"].ToString());
                                        bytes = FormatosPdf.imprimePagare(solicitud, this.UserID);
                                    }
                                    
                                    
                                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, this.UserID, "IMPRIMIO LA CARÁTULA DE LA SOLICITUD: " + myQueryStrings["solID"].ToString());
                                }
                                if (myQueryStrings["impseg"] != null)
                                {
                                    string sError = "";

                                    FormatosPdf.imprimeReporteSeguro(int.Parse(myQueryStrings["solID"].ToString()), ref path, ref sError);
                                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, this.UserID, "IMPRIMIO EL FORMATO DE SEGURO DE LA SOLICITUD: " + myQueryStrings["solID"].ToString());
                                }
                                if (myQueryStrings["imptermsandcond"] != null)
                                {
                                  

                                    bytes = FormatosPdf.imprimeTermsAndConditions(int.Parse(myQueryStrings["solID"].ToString()), UserID);
                                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, this.UserID, "IMPRIMIO EL FORMATO DE TERMINOS Y CONDICIONES: " + myQueryStrings["solID"].ToString());
                                }
                                if (myQueryStrings["cartaCompromiso"] != null)
                                {


                                    bytes = FormatosPdf.imprimeCartaCompromiso(int.Parse(myQueryStrings["solID"].ToString()));
                                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, this.UserID, "IMPRIMIO EL FORMATO DE CARTA COMPROMISO: " + myQueryStrings["solID"].ToString());
                                }

                                if (myQueryStrings["evaluacion"] != null)
                                {
                                    bytes = FormatosPdf.imprimeEvaluacion(int.Parse(myQueryStrings["solID"].ToString()));
                                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, this.UserID, "IMPRIMIO EL FORMATO DE Evaluacion: " + myQueryStrings["solID"].ToString());
                                }

                                
                                if (myQueryStrings["buroCredito"] != null)
                                {

                                    bytes = FormatosPdf.imprimeBuroCredito(int.Parse(myQueryStrings["solID"].ToString()));
                                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, this.UserID, "IMPRIMIO EL FORMATO DE TERMINOS Y CONDICIONES: " + myQueryStrings["solID"].ToString());
                                }
                                
                            }

                            if (myQueryStrings["NotaCompraFormato"] != null)
                            {
                                string sError = "";

                                FormatosPdf.imprimeOrdendeCompraFormato(int.Parse(myQueryStrings["NotaCompraFormato"].ToString()), ref path, ref sError);
                                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, this.UserID, "IMPRIMIO EL FORMATO DE SEGURO DE LA SOLICITUD: " + myQueryStrings["NotaCompraFormato"].ToString());
                            }
                            if (myQueryStrings["ordenDeCargaId"] != null)
                            {
                                string sError = "";

                                FormatosPdf.imprimeOrdendeCarga(int.Parse(myQueryStrings["ordenDeCargaId"].ToString()), ref path, ref sError);
                                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, this.UserID, "IMPRIMIO LA ORDEN DE CARGA: " + myQueryStrings["ordenDeCargaId"].ToString());
                            }

                            //printSeguro=1&solSeguroID="
                            if (myQueryStrings["printSeguro"] != null)
                            {
                                string sError = "";
                                bytes = FormatosPdf.imprimeSeguro(int.Parse(myQueryStrings["solSeguroID"].ToString()));
                                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.PRINT, this.UserID, "IMPRIMIO EL SEGURO: " + myQueryStrings["solSeguroID"].ToString());
                            }
                            Response.ClearHeaders();
                            Response.Clear();
                            
                            if (myQueryStrings["ContentType"] == null || myQueryStrings["ContentType"].ToString().Length <= 0)
                            {
                                Response.ContentType = "application/pdf";
                            }
                            else
                            {
                                Response.ContentType = myQueryStrings["ContentType"].ToString();//"application/pdf";
                            }

                            Response.AddHeader("Content-Disposition", "attachment;filename=" + myQueryStrings["filename"].ToString());
                            if (myQueryStrings["archivo"] != null)
                            {
                                path = myQueryStrings["archivo"].ToString();
                            }
                            if (bytes != null)
                            {
                                Response.BinaryWrite(bytes);
                            }
                            else
                            {
                                Response.WriteFile(path);
                                Response.Flush();
                                //try to delete the tmp file
                                try
                                {
                                    if (File.Exists(path))
                                    {
                                        File.Delete(path);
                                    }
                                }
                                catch{}
                            }
                            //Response.End();
                        }
                        else
                        {
                            myQueryStrings.Clear();
                            //Response.Redirect("~/frmAddModifyProductores.aspx", true);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.PRINT, "Error imprimiendo", ref ex);
            }
            
        }

        
    }
}
