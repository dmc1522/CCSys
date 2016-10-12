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
    class CalculoIntereses : System.Web.UI.Page
    {
        public enum posDataTable{
            posFecha = 0, 
            posMontoCredito,
            posFechadePago,
            posTotalDias,
            posInteres,
            posDebe,
            posAbonado,
            posTotalDebe,
            posMes,
            posConcepto,
            posDescPago
        }

       public static void  aceptanulls(ref DataTable dt){
           for(int i=0;i<dt.Columns.Count; i++)
           {
               dt.Columns[i].AllowDBNull = true;
           }
       }
       public static DataTable calculaInteres(int creditoID, DateTime fechafin, ref string sError){
            
            dsEstadodeCuenta.dtEstadodeCuentaDataTable dtEstadoCuenta = new dsEstadodeCuenta.dtEstadodeCuentaDataTable();
            DataTable dtOrdenada = new DataTable(); DataTable dtprimerOrdenada = new DataTable();
            DataTable dtSegundaOrdenada = new DataTable();
            aceptanulls(ref dtOrdenada); aceptanulls(ref dtprimerOrdenada); aceptanulls(ref dtSegundaOrdenada);

            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdGaribay = new SqlCommand();
            double tasainteres = 0;
            SqlDataReader read;
            try{
                conGaribay.Open();
                //SACAMOS EL INTERES
                string sql;
                sql = "SELECT     Creditos.Interesanual,Solicitudes.CostoTotalSeguro " +
                      "FROM         Creditos INNER JOIN " +
                      " Solicitudes ON Creditos.creditoID = Solicitudes.creditoID " +
                      " WHERE     (Creditos.creditoID = @creditoID)";

                     
                cmdGaribay.Connection = conGaribay;
                cmdGaribay.CommandText = sql;
                cmdGaribay.Parameters.Add("@creditoID", SqlDbType.Int).Value = creditoID;
                cmdGaribay.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fechafin;
                read = cmdGaribay.ExecuteReader();
                
                double montoseguro =0;
                if (read.Read())
                {
                    double.TryParse(read["CostoTotalSeguro"].ToString(), out montoseguro);
                    double.TryParse(read["Interesanual"].ToString(), out tasainteres);
                }

              

               
               //PRIMERO METEMOS TODOS LAS NOTAS 
                sql = "SELECT   Fecha, Total, Folio FROM         Notasdeventa where creditoID = @creditoID and Fecha <=@fecha order by fecha";
                cmdGaribay.CommandText=sql;
                read.Close();
                read = cmdGaribay.ExecuteReader();
                DateTime fechainicio = new DateTime();
                bool esprimero = true;

                while (read.Read())
                {
                    dsEstadodeCuenta.dtEstadodeCuentaRow row = dtEstadoCuenta.NewdtEstadodeCuentaRow();
                    DateTime fecha;
                    double abono = 0, cargo = 0;

                    if (read["Fecha"] != null && read["Fecha"].ToString() != "")// ES UNA NOTA
                    {
                        DateTime.TryParse(read["Fecha"].ToString(), out fecha); //sacamos fecha
                        Double.TryParse(read["Total"].ToString(), out cargo);
                        row.fecha = fecha;
                        row.montocredito = cargo;
                        row.concepto = "NOTA DE VENTA No. " + read["Folio"].ToString();
                        row.debe  = 0;
                        row.abonado = 0;
                        row.mes = Utils.getMonthName(row.fecha.Month);
                        row.intereses = 0;
                        dtEstadoCuenta.AdddtEstadodeCuentaRow(row);
                        if (esprimero)
                        {
                            fechainicio = fecha;
                            esprimero = false;
                           
                        }
                        else
                        {
                            if (fechainicio > fecha)
                            {
                                fechainicio = fecha;
                            }
                           
                        }
                    }
                }
                //METEMOS EL SEGURO CON LA MISMA FECHA DE INICIO
                dsEstadodeCuenta.dtEstadodeCuentaRow rowSeguro = dtEstadoCuenta.NewdtEstadodeCuentaRow();
                rowSeguro.fecha = fechainicio;
                rowSeguro.montocredito = montoseguro;
                rowSeguro.concepto = "SEGURO AGRICOLA";               
                rowSeguro.abonado = 0;
                rowSeguro.mes = Utils.getMonthName(rowSeguro.fecha.Month);
                rowSeguro.intereses = 0;
                dtEstadoCuenta.AdddtEstadodeCuentaRow(rowSeguro);
                
                // DESPUES METEMOS TODOS LOS CORTES CORTE VACÍO QUE SOLO MOSTRARA EL DEBE Y  OTRA ROW SALDO INICIAL

                DateTime fechacorte = new DateTime(fechainicio.Year, fechainicio.Month, 1);
                sumaunmes(ref fechacorte);
                do
                {
                    dsEstadodeCuenta.dtEstadodeCuentaRow row = dtEstadoCuenta.NewdtEstadodeCuentaRow();
                    row.fecha = restaundia(fechacorte);
                    row.montocredito = 0;
                    row.concepto = "CORTE DEL MES ";
                    row.concepto += Utils.getMonthName(row.fecha.Month);
                    row.montocredito = 0;
                    row.abonado = 0;
                    row.mes = Utils.getMonthName(row.fecha.Month);
                    row.intereses = 0;
                    dtEstadoCuenta.AdddtEstadodeCuentaRow(row);
                    row = dtEstadoCuenta.NewdtEstadodeCuentaRow();
                    row.fecha = fechacorte;
                    row.montocredito = 0;
                    row.concepto = "SALDO INICIAL DE MES ";
                   // row.concepto += row.fecha.Month == 1 ? Utils.getMonthName(12) : Utils.getMonthName(row.fecha.Month - 1);
                    row.montocredito = 0;
                    row.abonado = 0;
                    row.mes = Utils.getMonthName(row.fecha.Month);
                    row.intereses = 0;
                    dtEstadoCuenta.AdddtEstadodeCuentaRow(row);



                    sumaunmes(ref fechacorte);
                }
                while (fechacorte < fechafin);
                    
                dtEstadoCuenta.DefaultView.Sort = "fecha ASC";
                dtprimerOrdenada = dtEstadoCuenta.DefaultView.ToTable();
                int posultimanota = dtprimerOrdenada.Rows.Count-1;

                //DESPUES SACAMOS LOS PAGOS

                
                sql = " SELECT    MovimientosCaja.movimientoID, MovimientosCaja.abono AS PagoCaja, MovimientosCaja.fecha AS FechaPagoCaja, MovimientosCuentasBanco.abono AS PagoBanco, " +
                                        " MovimientosCuentasBanco.fecha AS FechaPagoBanco, MovimientosCuentasBanco.movbanID " +
                                        " FROM         Creditos INNER JOIN " +
                                        " Pagos_Credito ON Creditos.creditoID = Pagos_Credito.creditoID LEFT OUTER JOIN " +
                                        " MovimientosCaja ON Pagos_Credito.movimientoID = MovimientosCaja.movimientoID LEFT OUTER JOIN " +
                                        " MovimientosCuentasBanco ON Pagos_Credito.movbanID = MovimientosCuentasBanco.movbanID where Pagos_Credito.creditoID =@creditoID and (MovimientosCaja.fecha<=@fecha or MovimientosCuentasBanco.fecha<=@fecha)";
                

                cmdGaribay.CommandText = sql;
                read.Close();
                
                read = cmdGaribay.ExecuteReader();
                
                while (read.Read())
                {
                    //POR CADA PAGO QUE HAYA SE METERA LA FECHA DE PAGO EN LA TRANSACCIÓN ANTERIOR MÁS RECIENTE
                    
                    DateTime fecha = new DateTime();
                    double abono = 0, cargo = 0;
                    DataRow row;
                    DateTime fechadetran;
                        
                    if (read["FechaPagoCaja"] != null && read["FechaPagoCaja"].ToString() != "")// ES UN PAGO DE CAJA
                    {
                        DateTime.TryParse(read["FechaPagoCaja"].ToString(), out fecha); //sacamos fecha
                         Double.TryParse(read["PagoCaja"].ToString(), out abono);
                        //CON ESTA FECHA SETEAMOS EL VALOR DEL PAGO A LA TRANSACCIÓN MÁS RECIENTE
                        bool encontramosrow = false;
                        int i=0;
                        do
                        {
                            row = dtprimerOrdenada.Rows[i];
                            fechadetran = (DateTime)(row.ItemArray[(int)(posDataTable.posFecha)]);
                            if(fechadetran>fecha)
                            {                                                // encontramos donde insertarla
                                encontramosrow=true;
                                
                                dtprimerOrdenada.Rows[i - 1][(int)(posDataTable.posFechadePago)] = fecha;
                                dtprimerOrdenada.Rows[i - 1][(int)(posDataTable.posAbonado)] = abono;
                                dtprimerOrdenada.Rows[i - 1][(int)(posDataTable.posDescPago)] = "PAGO CON EL MOVIMIENTO DE CAJA No. " + read["movimientoID"].ToString();
                               

                            }
                            else
                            {
                                i++;
                                if (i >= posultimanota)//METEMOS EL PAGO HASTA EL FINAL
                                {
                                    DataRow rowamodificar = dtprimerOrdenada.Rows[i ];
                                    rowamodificar[(int)(posDataTable.posFechadePago)] = fecha;
                                    rowamodificar[(int)(posDataTable.posAbonado)] = abono;
                                    rowamodificar[(int)(posDataTable.posDescPago)] = "PAGO CON EL MOVIMIENTO DE CAJA No. " + read["movimientoID"].ToString();
                                    encontramosrow = true;

                                }
                            }


                        }while(!encontramosrow && i<posultimanota);
                    }

                    // sacamos pagos bancos
                    else
                    {
                        DateTime.TryParse(read["FechaPagoBanco"].ToString(), out fecha); //sacamos fecha
                        Double.TryParse(read["PagoBanco"].ToString(), out abono);
                        //CON ESTA FECHA SETEAMOS EL VALOR DEL PAGO A LA TRANSACCIÓN MÁS RECIENTE
                        bool encontramosrow = false;
                        int i = 0;
                        do
                        {
                            row = dtprimerOrdenada.Rows[i];
                            fechadetran = (DateTime)(row.ItemArray[(int)(posDataTable.posFecha)]);
                            if (fechadetran > fecha)
                            {                                                // encontramos donde insertarla
                                encontramosrow = true;
                                DataRow rowamodificar = dtprimerOrdenada.Rows[i - 1];
                                rowamodificar[(int)(posDataTable.posFechadePago)] = fecha;
                                rowamodificar[(int)(posDataTable.posAbonado)] = abono;
                                rowamodificar[(int)(posDataTable.posDescPago)] = "PAGO CON EL MOVIMIENTO DE BANCO No. " + read["movbanID"].ToString();


                            }
                            else
                            {
                                i++;
                                if (i >= posultimanota)//METEMOS EL PAGO HASTA EL FINAL
                                {
                                    DataRow rowamodificar = dtprimerOrdenada.Rows[i];
                                    encontramosrow=true;
                                    rowamodificar[(int)(posDataTable.posFechadePago)] = fecha;
                                    rowamodificar[(int)(posDataTable.posAbonado)] = abono;
                                    rowamodificar[(int)(posDataTable.posDescPago)] = "PAGO CON EL MOVIMIENTO DE BANCO No. " + read["movbanID"].ToString();


                                }
                            }


                        } while (!encontramosrow && i<posultimanota );
                    }

                    //insertamos otra row que tenga como fecha fecha de pago
                    dtprimerOrdenada.Rows.Add(fecha,0,fecha,0,0,0,0,0,Utils.getMonthName(fecha.Month), "SALDO INICIAL DESPUES DE PAGO","");           
                      


                }
                dtprimerOrdenada.DefaultView.Sort = "fecha ASC";
                dtSegundaOrdenada = dtprimerOrdenada.DefaultView.ToTable();
                //SACAMOS LAS DEVOLUCIONES
                rellenadatos(ref dtSegundaOrdenada,tasainteres,fechafin);
              

                

            }
            catch(Exception ex)
            {

            }
            finally
            {
                conGaribay.Close();
            }
           
           
            //FINALMENTE REGRESAMOS EL DATASET

            
            
            
            return dtSegundaOrdenada;
        }
        public static void rellenadatos(ref DataTable dt, double tasainteres, DateTime fechafin)
        {
            //PRIMERO SETEAMOS TODAS LAS FECHAS DE PAGO
            if(dt.Rows.Count>0){

                DateTime fechaasetear = fechafin;
                for(int i = dt.Rows.Count-1; i>=0; i--)
                {


                    string concepto = (dt.Rows[i][(int)posDataTable.posConcepto]).ToString();
                    if(concepto.IndexOf("CORTE") >= 0   || concepto.IndexOf("DESPUES DE PAGO")>=0)
                    {
                        //CAMBIAMOS LA FECHA A SETEAR solo si es despues de pago
                        if(concepto.IndexOf("DESPUES DE PAGO")>=0)
                            dt.Rows[i][(int)(posDataTable.posFechadePago)] = fechaasetear;

                        fechaasetear = (DateTime)(dt.Rows[i][(int)posDataTable.posFecha]);
                    }
                    else
                    {
                        dt.Rows[i][(int)(posDataTable.posFechadePago)] = fechaasetear;
                    }
                        
                }
                double montodesdecorteant = 0;
                for(int i=0; i<dt.Rows.Count; i++)
                {
                    
                   string concepto = (dt.Rows[i][(int)posDataTable.posConcepto]).ToString();
                   if(concepto.IndexOf("CORTE")>=0 )//SI ES CORTE O ES SALDO DESPUES DE PAGO SOLO METEMOS EL TOTAL DEBE Y ESE DATO LO METEMOS COMO MONTO DE LA SIGUIENTE ROW
                    {
                       
                        
                        (dt.Rows[i][(int)posDataTable.posTotalDias]) = DBNull.Value;
                        (dt.Rows[i][(int)posDataTable.posInteres]) = DBNull.Value;
                        (dt.Rows[i][(int)posDataTable.posDebe]) = DBNull.Value;
                        (dt.Rows[i][(int)posDataTable.posAbonado]) = DBNull.Value;
                        (dt.Rows[i][(int)posDataTable.posTotalDebe]) = montodesdecorteant;
                        (dt.Rows[i][(int)posDataTable.posDescPago]) = DBNull.Value;
                        if((i+1)<dt.Rows.Count){
                            (dt.Rows[i+1][(int)posDataTable.posMontoCredito]) = montodesdecorteant;
                        }
                        montodesdecorteant = 0;
                      
                    }
                    else{
                        if (concepto.IndexOf("DESPUES DE PAGO") >= 0)
                        {
                            (dt.Rows[i][(int)posDataTable.posMontoCredito]) = montodesdecorteant;
                            montodesdecorteant = 0;
                        }
                        double monto = (double)(dt.Rows[i][(int)posDataTable.posMontoCredito]);
                        DateTime fechatran = (DateTime)(dt.Rows[i][(int)posDataTable.posFecha]);
                        DateTime fechapago = (DateTime)(dt.Rows[i][(int)posDataTable.posFechadePago]);
                        TimeSpan diasdif = fechapago.Subtract(fechatran);
                        int dias = diasdif.Days + 1;
                        
                        (dt.Rows[i][(int)posDataTable.posTotalDias]) = dias;
                        double interes = (tasainteres / 360) * dias * monto;
                        (dt.Rows[i][(int)posDataTable.posInteres]) = interes;
                        (dt.Rows[i][(int)posDataTable.posDebe]) = monto + interes;
                        double abono = 0;
                        double.TryParse(dt.Rows[i][(int)posDataTable.posAbonado].ToString(),out abono);
                        (dt.Rows[i][(int)posDataTable.posTotalDebe]) = monto + interes - abono;
                        //(dt.Rows[i][(int)posDataTable.posDescPago]) = "";
                        montodesdecorteant += (monto + interes - abono);
                    }
                }
            }


        }
        public static double sacaInteresdedosfechas(DateTime fechainicio, DateTime fechafin, double monto, double tasadeinteresanual)
        {
            double interes = 0;
            TimeSpan dias = fechafin - fechainicio;
            interes = (tasadeinteresanual / 360) * dias.Days * monto;
            return interes;
        }
        public static void  sumaunmes(ref DateTime fecha)
        {
            
            int anio, mes, dia;
            anio = fecha.Year;
            mes = fecha.Month;
            dia = fecha.Day;
            if(fecha.Month==12){
                mes = 1;
                anio +=1;
            }
            else{
                mes += 1;
            }

            fecha = new DateTime(anio, mes, dia);
            
           

        }
        public static DateTime restaundia(DateTime fecha){
            TimeSpan oneDay = new TimeSpan(1,0,0,0);
            fecha = fecha.Subtract(oneDay);
            return fecha;
        }

        
        
    }
}
