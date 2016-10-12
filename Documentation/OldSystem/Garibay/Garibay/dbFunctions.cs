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
using System.Text;

namespace Garibay
{
    public class dbFunctions
    {
        public static float GetSaldoCredito(int creditoId)
        {
            float saldo = 0;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = "ReturnEstadodeCuentatOTALES";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@creditoId", SqlDbType.Int)).Value = creditoId;
                command.Parameters.Add(new SqlParameter("@fechafin", SqlDbType.DateTime)).Value = DateTime.Now;
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    saldo = float.Parse(reader["TotalDebe"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "getting saldo for credito: " + creditoId.ToString(), ex);
            }
            return saldo;
        }
        public static bool IsCreditoPagado(int creditoID)
        {
            bool bRes = false;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "SELECT pagado FROM Creditos WHERE     (creditoID = @creditoID)";
                comm.Parameters.Add("@creditoID", SqlDbType.Int).Value = creditoID;
                object obj = comm.ExecuteScalar();
                bRes = obj != null && bool.Parse(obj.ToString());
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "getting boleta's data", ex);
            }
            finally
            {
                conn.Close();
            }
            return bRes;
        }

        public static DataRow GetBoletaData(int boletaid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "select * from Boletas where boletaid = @boletaid";
                comm.Parameters.Add("@boletaid", SqlDbType.Int).Value = boletaid;
                dt = dbFunctions.GetDataTable(comm);
                if (dt.Rows.Count > 0)
                {

                }
            }
            catch (System.Exception ex)
            {
	            Logger.Instance.LogException(Logger.typeUserActions.SELECT, "getting boleta's data", ex);
            }
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public static DataTable GetDataTable(SqlCommand comm)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                comm.Connection = conn;
                SqlDataAdapter da = new SqlDataAdapter(comm);
                conn.Open();
                da.Fill(dt);
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "No se ejecuto GetDataTable", ref ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public static string GetProductoName(string productoID)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "select nombre from productos where productoid = @productoid";
                comm.Parameters.Add("productoid", SqlDbType.Int).Value = int.Parse(productoID);
                comm.Connection = conn;
                conn.Open();
                SqlDataReader r = comm.ExecuteReader();
                if (r.HasRows && r.Read())
                {
                    string str = r[0].ToString();
                    conn.Close();
                    return str;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "No se ejecuto GetProductoName", ref ex);
            }
            finally
            {
                conn.Close();
            }
            return "";
        }


        /// <summary>
        /// Compara si una fecha esta dentro de un periodo bloqueado de la cuenta indicada
        /// </summary>
        /// <param name="dtFechaToCheck">Fecha a verificar</param>
        /// <param name="cuentaID">Cuenta para verificar los periodos</param>
        /// <returns>true  si existe dentro de algun periodo bloqueado.</returns>
        public static bool FechaEnPeriodoBloqueado(DateTime dtFechaToCheck, int cuentaID)
        {
            bool bResult = false;
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT COUNT(*) AS Cantidad  FROM PeriodosBloqueados "
                + " WHERE (cuentaid = @cuentaid) AND (@dtFechaToCheck >= periodoINI AND "
                + " @dtFechaToCheck <= periodoFIN);";
            comm.Parameters.Add("@cuentaid", SqlDbType.Int).Value = cuentaID;
            comm.Parameters.Add("@dtFechaToCheck", SqlDbType.DateTime).Value = dtFechaToCheck;
            bResult = (GetExecuteIntScalar(comm, 0) > 0);
            return bResult;
        }

        public static  bool numChequeValido(int ChequeNum, int Cuenta)
        {
            Boolean sresult;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            String query = "SELECT numchequeinicio, numchequefin FROM CuentasDeBanco WHERE cuentaID=@cuentaID";
            SqlCommand comm = new SqlCommand(query, conn);
            SqlDataReader rd;
            try
            {
                conn.Open();
                comm.Parameters.Add("@cuentaID", SqlDbType.Int).Value = Cuenta;
                rd = comm.ExecuteReader();
                if (rd.HasRows && rd.Read())
                {

                    if (ChequeNum <= int.Parse(rd["numchequefin"].ToString()) && ChequeNum >= int.Parse(rd["numchequeinicio"].ToString()))
                    {
                        sresult = true;
                    }
                    else { sresult = false; }

                }
                else
                {
                    sresult = false;
                }


            }
            catch (Exception ex)
            {
                sresult = false;
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "ERROR GETTING NUM VALIDOS", ref ex);

            }
            finally
            {

                conn.Close();
            }

            return sresult;


        }
        public static int GetLastCiclo()
        {
            int cicloId = -1;
            SqlConnection conSacaLastCiclo = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdSacaCicloId = new SqlCommand();
            
            try
            {
                conSacaLastCiclo.Open();
                cmdSacaCicloId.CommandText = "select max (cicloId) from Ciclos";
                cmdSacaCicloId.Connection = conSacaLastCiclo;
                cicloId = (int)cmdSacaCicloId.ExecuteScalar();
                 
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "ERROR GETTING LAST CYCLE", ref ex);
            }
            return cicloId;

        }
        /// <summary>
        /// Execute a command and return reader
        /// </summary>
        /// <param name="comm">Command to execute</param>
        /// <param name="iDefault">default value if the query couldn't be executed</param>
        /// <returns>ExecuteScalar result</returns>
        public static SqlDataReader ExecuteReader(SqlCommand comm)
        {
            SqlDataReader res = null;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                comm.Connection = conn;
                conn.Open();
                res = comm.ExecuteReader();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "No se ejecuto ExecuteReader", ref ex);
                conn.Close();
            }
            finally
            {
            }
            return res;
        }


        /// <summary>
        /// Execute a command and return int updated rows
        /// </summary>
        /// <param name="comm">Command to execute</param>
        /// <param name="iDefault">default value if the query couldn't be executed</param>
        /// <returns>ExecuteScalar result</returns>
        public static int ExecuteCommand(SqlCommand comm)
        {
            int iUpdatedRows = 0;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                comm.Connection = conn;
                conn.Open();
                iUpdatedRows = (int)comm.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "No se ejecuto ExecuteReader", ref ex);
            }
            finally
            {
            }
            return iUpdatedRows;
        }

        /// <summary>
        /// Execute a command and return the result as double
        /// </summary>
        /// <param name="comm">Command to execute</param>
        /// <param name="iDefault">default value if the query couldn't be executed</param>
        /// <returns>ExecuteScalar result</returns>
        public static double GetExecuteDoubleScalar(SqlCommand comm, double iDefault)
        {
            double iRes = iDefault;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                comm.Connection = conn;
                conn.Open();
                Object result = double.Parse(comm.ExecuteScalar().ToString());
                if(result!=null)
                    iRes = double.Parse(result.ToString());
              
            }
            catch (System.Exception ex)
            {
                iRes = iDefault;
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "No se ejecuto GetExecuteIntScalar", ref ex);
            }
            finally
            {
                conn.Close();
            }
            return iRes;
        }

        /// <summary>
        /// Execute a command and return the result as int
        /// </summary>
        /// <param name="comm">Command to execute</param>
        /// <param name="iDefault">default value if the query couldn't be executed</param>
        /// <returns>ExecuteScalar result</returns>
        public static int GetExecuteIntScalar(SqlCommand comm, int iDefault)
        {
            int iRes = iDefault;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                comm.Connection = conn;
                conn.Open();
                Object result =  int.Parse(comm.ExecuteScalar().ToString());
                if (result != null)
                    iRes = int.Parse(result.ToString());
            }
            catch (System.Exception ex)
            {
                iRes = iDefault;
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "No se ejecuto GetExecuteIntScalar", ref ex);
            }
            finally
            {
                conn.Close();
            }
            return iRes;
        }

        /// <summary>
        /// Execute a query and return the result as int
        /// </summary>
        /// <param name="sQuery">Query to execute</param>
        /// <param name="iDefault">default value if the query couldnt be executed</param>
        /// <returns>ExecuteScalar result</returns>
        public static int GetExecuteIntScalar(String sQuery, int iDefault)
        {
            int iRes = iDefault;
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand comm = new SqlCommand(sQuery, conn);
                conn.Open();
                iRes = int.Parse(comm.ExecuteScalar().ToString());
            }
            catch (System.Exception ex)
            {
                iRes = iDefault;
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "No se ejecuto: " + sQuery, "GetExecuteIntScalar", ref ex);
            }
            finally
            {
                conn.Close();
            }
            return iRes;
        }

		public static bool insertaBoletaEnBlanco(string ticket, int productorID, string productornombre, int cicloID, int productoID, int bodegaID, int userID){
            bool valoraretornar = true;
            dsBoletas.dtBoletasDataTable dt = new dsBoletas.dtBoletasDataTable();
            dsBoletas.dtBoletasRow newRow = dt.NewdtBoletasRow();
            newRow.productorID = productorID;
            newRow.NombreProductor = productornombre;
            newRow.Ticket = ticket;
            newRow.cicloID = cicloID;
            newRow.productoID = productoID;
            newRow.bodegaID = bodegaID;
            newRow.userID = userID;
            newRow.FechaEntrada = Utils.Now;
            newRow.PesoDeEntrada = 0;
            newRow.FechaSalida = Utils.Now;
            newRow.PesoDeSalida = 0;
            newRow.pesonetoentrada= 0;
            newRow.pesonetosalida = 0;
            newRow.humedad = 0;
            newRow.impurezas = 0;
            newRow.precioapagar = 0;
            SqlConnection addConn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand addComm = new SqlCommand();
            try
            {
                addComm.Connection = addConn;
                addConn.Open();
                addComm.CommandText = "INSERT INTO Boletas (productorID, NombreProductor, Ticket, bodegaID, cicloID, FechaEntrada, PesoDeEntrada, FechaSalida, PesoDeSalida, pesonetoentrada,  pesonetosalida, productoID, humedad, impurezas, precioapagar, dctoSecado, userID) VALUES     (@productorID,@NombreProductor,@Ticket,@bodegaID,@cicloID,@FechaEntrada,@PesoDeEntrada,@FechaSalida,@PesoDeSalida,@pesonetoentrada,@pesonetosalida,@productoID,@humedad,@impurezas,@precioapagar,@dctoSecado,@userID); select boletaID = SCOPE_IDENTITY();";
                addComm.Parameters.Add("@productorID", SqlDbType.Int).Value = newRow.productorID;
                // addComm.Parameters.AddWithValue("@productorID", newRow.productorID);
                addComm.Parameters.Add("@NombreProductor", SqlDbType.NVarChar).Value = newRow.NombreProductor;
                //addComm.Parameters.AddWithValue("@NombreProductor", newRow.NombreProductor);
              //  addComm.Parameters.Add("@NumeroBoleta", SqlDbType.NVarChar).Value = newRow.NumeroBoleta;
                //addComm.Parameters.AddWithValue("@NumeroBoleta", newRow.NumeroBoleta);
                //  addComm.Parameters.AddWithValue("@Ticket", newRow.Ticket);
                addComm.Parameters.Add("@Ticket", SqlDbType.NVarChar).Value = newRow.Ticket;
                //  addComm.Parameters.AddWithValue("@bodegaID", newRow.bodegaID);
                addComm.Parameters.Add("@bodegaID", SqlDbType.Int).Value = newRow.bodegaID;
                //  addComm.Parameters.AddWithValue("@cicloID", newRow.cicloID);
                addComm.Parameters.Add("@cicloID", SqlDbType.Int).Value = newRow.cicloID;
                //  addComm.Parameters.AddWithValue("@FechaEntrada", newRow.FechaEntrada);
                addComm.Parameters.Add("@FechaEntrada", SqlDbType.DateTime).Value = newRow.FechaEntrada;
                addComm.Parameters.Add("@PesoDeEntrada", SqlDbType.Float).Value = (float)newRow.PesoDeEntrada;
                addComm.Parameters.Add("@FechaSalida", SqlDbType.DateTime).Value = newRow.FechaSalida;
                // addComm.Parameters.AddWithValue("@FechaSalida", newRow.FechaSalida);
                addComm.Parameters.Add("@PesoDeSalida", SqlDbType.Float).Value = newRow.PesoDeSalida;
                addComm.Parameters.Add("@pesonetoentrada", SqlDbType.Float).Value = newRow.pesonetoentrada;
                addComm.Parameters.Add("@pesonetosalida", SqlDbType.Float).Value = newRow.pesonetosalida;
                // addComm.Parameters.AddWithValue("@productoID", newRow.productoID);
                addComm.Parameters.Add("@productoID", SqlDbType.Int).Value = newRow.productoID;

                addComm.Parameters.Add("@humedad", SqlDbType.Float).Value = newRow.humedad;
                addComm.Parameters.Add("@impurezas", SqlDbType.Float).Value = newRow.impurezas;
                addComm.Parameters.Add("@precioapagar", SqlDbType.Float).Value = newRow.precioapagar;
                addComm.Parameters.Add("@dctoSecado", SqlDbType.Float).Value = newRow.dctoSecado;
                //addComm.Parameters.AddWithValue("@userID", newRow.userID);
                addComm.Parameters.Add("@userID", SqlDbType.Int).Value = newRow.userID;

                newRow.boletaID = int.Parse(addComm.ExecuteScalar().ToString());
                valoraretornar=true;
            }
            catch(Exception Ex){
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, "ERROR AL INSERTAR UNA BOLETA. LA EXCEPCION FUE: "  + Ex.Message, "PAGINA DE INSERCION EN BLANCO DE BOLETA");
                valoraretornar = false;
            }
            finally{
                addConn.Close();
            }
            return valoraretornar;
        }
        public static bool EliminaCreditoFinanciero(int creditoID, int userID, ref string sError)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand Comm = new SqlCommand();
            bool result = false;
            try
            {
                conn.Open();
                Comm.Connection = conn;
                Comm.CommandText = "delete from creditosfinancieros where creditoFinancieroID = @creditoFinancieroID";
                Comm.Parameters.Add("@creditoFinancieroID", SqlDbType.Int).Value = creditoID;
                if(Comm.ExecuteNonQuery() == 1)
                {
                    result = true;
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CUENTASDEBANCOS, Logger.typeUserActions.DELETE, userID, "ELIMINO EL CREDITO : " + creditoID.ToString());
                }
            }
            catch (System.Exception ex)
            {
                if (sError != null)
                {
                    sError = ex.Message;
                }
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, userID, "NO SE PUDO ELIMINAR EL CREDITO: " + creditoID.ToString() + " ex: "+ ex.Message, "EliminaCreditoFinanciero");
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        public static bool EliminaBoleta(String sBoletaID, int userID)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand Comm = new SqlCommand();
            try
            {
                conn.Open();
                Comm.Connection = conn;
                Comm.CommandText = "delete from Boletas where boletaID = @boletaID";
                Comm.Parameters.Add("@boletaID", SqlDbType.Int).Value = int.Parse(sBoletaID);
                if(Comm.ExecuteNonQuery() == 1)
                {
                    conn.Close();
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, userID, "NO SE PUDO BORRAR LA BOLETA No. " + sBoletaID + ". LA EXC FUE: " + ex.Message, "LISTA DE BOLETAS");
            }
            finally
            {
                conn.Close();
            }
            return false;
        }
        public static bool BoletaAlreadyExists(int iBoletaID, String sNumBoleta, String sTicket)
        {
            bool bBoletaExists = false;
            //check id
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                if (iBoletaID != null)
                {
                    comm.CommandText = "select count(*) from boletas where boletaID = @boletaid";
                    comm.Parameters.Add("@boletaid", SqlDbType.Int).Value = iBoletaID;
                    if (int.Parse(comm.ExecuteScalar().ToString()) >0)
                    {
                        bBoletaExists = true;
                    }
                }
                if (!bBoletaExists && sNumBoleta != null && sNumBoleta.Trim() != "")
                {
                    comm.CommandText = "select count(*) from boletas where NumeroBoleta = @NumeroBoleta";
                    comm.Parameters.Clear();
                    comm.Parameters.Add("@NumeroBoleta", SqlDbType.VarChar).Value = sNumBoleta.Trim();
                    if (int.Parse(comm.ExecuteScalar().ToString()) > 0)
                    {
                        bBoletaExists = true;
                    }
                }
                if (!bBoletaExists && sTicket != null && sTicket.Trim() != "")
                {
                    comm.CommandText = "select count(*) from boletas where Ticket = @Ticket";
                    comm.Parameters.Clear();
                    comm.Parameters.Add("@Ticket", SqlDbType.VarChar).Value = sTicket.Trim();
                    if (int.Parse(comm.ExecuteScalar().ToString()) > 0)
                    {
                        bBoletaExists = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "consultando existencia de boleta", ref ex);	
            }
            finally
            {
                conn.Close();
            }
            return bBoletaExists;
        }


        public static bool ChequeAlreadyExists(int numCheque, String sCuenta)
        {
            int iCuenta = -1;
            if (int.TryParse(sCuenta, out iCuenta))
            {
                return dbFunctions.ChequeAlreadyExists(numCheque, iCuenta);
            }
            return false;
            
        }
        public static bool ChequeAlreadyExists(int numCheque, int iCuenta)
        {
            bool bChequeExists = false;
            //check id
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                if (numCheque != null)
                {
                    comm.CommandText = "select count(*) from MovimientosCuentasBanco where cuentaID = @cuentaID and numCheque = @numCheque";
                    comm.Parameters.Add("@cuentaID", SqlDbType.Int).Value = iCuenta;
                    comm.Parameters.Add("@numCheque", SqlDbType.Int).Value = numCheque;
                    if (int.Parse(comm.ExecuteScalar().ToString()) > 0)
                    {
                        bChequeExists = true;
                    }
                }
              
              
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "cheque exists", ex);
            }
            finally
            {
                conn.Close();
            }
            return bChequeExists;
        }
        /// <summary>
        /// fill a data table with the movements of bancos between dates calculating the saldos
        /// </summary>
        /// <param name="cuentaID"></param>
        /// <param name="dtFechainicio"></param>
        /// <param name="dtFechafin"></param>
        /// <param name="fSaldoInicial"></param>
        /// <param name="fSaldoFinal"></param>
        /// <param name="dtTable"></param>
        /// <returns></returns>
        public static bool fillDTMovBancos(int cuentaID, DateTime dtFechainicio, DateTime dtFechafin, ref double fSaldoInicial, ref double fSaldoFinal, ref dsMovBanco.dtMovBancoDataTable dtMBTable)
        {
            if (dtMBTable == null || dtFechafin == null || dtFechainicio == null)
            {
                return false;
            }
            fSaldoFinal = fSaldoInicial = 0.0F;
            try
            {
                //get the saldo of the previous month
                string sSql = "SELECT (sum(abono) - sum(cargo)) as saldo FROM MovimientosCuentasBanco where cuentaID=@cuentaID and fecha <= @fecha";
                SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand sqlComm = new SqlCommand(sSql, sqlConn);
                sqlConn.Open();
                sqlComm.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtFechainicio.ToString("yyyy/MM/dd 00:00:00");
                sqlComm.Parameters.Add("@cuentaID", SqlDbType.Int).Value = cuentaID;
                try
                {
                    fSaldoInicial = double.Parse(sqlComm.ExecuteScalar().ToString());
                }
                catch (System.Exception ex)
                {
                    fSaldoInicial = 0.0f;
                }
                finally
                {
                    sqlConn.Close();
                    sqlComm.Parameters.Clear();
                }              

                String sOrder = "";
                SqlConnection orderConn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand orderComm = new SqlCommand();
                try
                {
                    orderConn.Open();
                    orderComm.Connection = orderConn;
                    orderComm.CommandText = "select paramValue from ConfigParams where paramName= 'MOVORDER' and categoryID = 1;";
                    SqlDataReader rOrder = orderComm.ExecuteReader();
                    if (rOrder.HasRows && rOrder.Read())
                    {
                        if (rOrder[0] != null && rOrder[0].ToString().Length > 0)
                        {
                            sOrder = rOrder[0].ToString();
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, "error obteniendo order de mov bancos", "fillDTmovBancos", ref ex);
                }
                finally
                {
                    orderConn.Close();
                }


                if (sOrder.Length == 0)
                {
                    sOrder = "chequecobrado ASC, abono DESC, numCheque DESC, cargo DESC";
                }

                //Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.SELECT, new BasePage().UserID, "ORDER: " + sOrder, "fillDTMovBancos");

                sSql = "returnmovbancos";
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = sSql;
                sqlComm.Parameters.Add("@fechaini", SqlDbType.DateTime).Value = dtFechainicio.ToString("yyyy/MM/dd 00:00:00");
                sqlComm.Parameters.Add("@fechafin", SqlDbType.DateTime).Value = dtFechafin.ToString("yyyy/MM/dd 00:00:00");
                sqlComm.Parameters.Add("@cuentaID", SqlDbType.Int).Value = cuentaID;
                sqlConn.Open();
                try
                {
                    SqlDataReader sqlReader = sqlComm.ExecuteReader();
                    if (sqlReader.HasRows)
                    {
                        dtMBTable.Rows.Clear();
                        double fCurrSaldo = fSaldoInicial;
                        while (sqlReader.Read())
                        {
                            int movID = 0, userID = 0;
                            double cargo = 0, abono = 0;
                            string concepto = "", nombre = "";
                            try
                            {
                                movID = int.Parse(sqlReader["movbanID"].ToString());
                                userID = int.Parse(sqlReader["userID"].ToString());
                                //cuentaID = int.Parse(sqlReader["cuentaID"].ToString());
                                abono = Utils.GetSafeFloat(sqlReader["abono"]);
                                cargo = Utils.GetSafeFloat(sqlReader["cargo"]);
                                //abono = sqlReader["abono"] == null ? 0.00f : float.Parse(sqlReader["abono"].ToString());
                                //cargo = sqlReader["cargo"] == null ? 0.00f : float.Parse(sqlReader["cargo"].ToString());
                                fCurrSaldo += Utils.GetSafeFloat(abono) - Utils.GetSafeFloat(cargo);
                                fSaldoFinal = fCurrSaldo;
                                concepto = sqlReader["concepto"].ToString();
                                nombre = sqlReader["nombre"].ToString();
                                bool chequecobrado;
                                string fechacobrado = sqlReader["fechacobrado"].ToString();
                                if (sqlReader["chequecobrado"] != null && bool.Parse(sqlReader["chequecobrado"].ToString()))
                                {
                                    chequecobrado = true;
                                }
                                else
                                {
                                    chequecobrado = false;
                                }

                                dtMBTable.AdddtMovBancoRow(movID,
                                DateTime.Parse(sqlReader["fecha"].ToString()),
                                concepto,
                                int.Parse(sqlReader["ConceptoMovCuentaID"].ToString()),
                                nombre,
                                Utils.GetSafeFloat(cargo),
                                Utils.GetSafeFloat(abono),
                                Utils.GetSafeFloat(fCurrSaldo),
                                userID,
                                DateTime.Parse(sqlReader["storeTS"].ToString()),
                                DateTime.Parse(sqlReader["updateTS"].ToString()),
                                cuentaID,
                                (sqlReader["numCabezas"].ToString() != "") ? double.Parse(sqlReader["numCabezas"].ToString()) : 0,
                                sqlReader["numCheque"].ToString().Length > 0 ? int.Parse(sqlReader["numCheque"].ToString()) : 0,
                                sqlReader["facturaOlarguillo"].ToString(),
                                sqlReader["chequeNombre"].ToString(), int.Parse(sqlReader["catalogoMovBancoFiscalID"].ToString()),
                                int.Parse(sqlReader["subCatalogoMovBancoFiscalID"].ToString()),
                                int.Parse(sqlReader["catalogoMovBancoInternoID"].ToString()),
                                int.Parse(sqlReader["subCatalogoMovBancoInternoID"].ToString()),
                                chequecobrado,
                                DateTime.Parse(sqlReader["fechacobrado"].ToString()),
                                sqlReader["subCatalogoInterno"].ToString(),
                                sqlReader["catalogoMovBancoInterno"].ToString(),
                                 sqlReader["catalogoMovBancoFiscal"].ToString(),
                                sqlReader["subCatalogoFiscal"].ToString(),
                                sqlReader["chequeNombre"].ToString(),
                                sqlReader["creditoFinancieroID"].ToString() == "" ? -1 : int.Parse(sqlReader["creditoFinancieroID"].ToString()),
                                sqlReader["bancoCreditoFinanciero"].ToString(),
                                sqlReader["numCreditoFinanciero"].ToString(),
                                (sqlReader["fechaCheque"].ToString().Length > 0) ? DateTime.Parse(sqlReader["fechaCheque"].ToString()) : DateTime.Parse(sqlReader["fecha"].ToString()));



                            }
                            catch (System.Exception ex)
                            {
                                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "getting mov bancos", ex);
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.SELECT, 
                        "getting mov bancos", ex);
                }
                finally
                {
                    sqlConn.Close();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "getting mov bancos", ex);
            }
            return true;
        }

        /// <summary>
        /// insert a new movement of cuenta
        /// </summary>
        /// <param name="dtrowaInsertar"> by ref dtMovBancoRow with all the data to insert</param>
        /// <param name="sError"> by ref error description if a exception ocurrs</param>
        /// <param name="userID"> current user</param>
        /// <param name="cuentaID"> cuenta id</param>
        /// <param name="cicloID"></param>
        /// <returns></returns>
        public static bool insertMovementdeBanco(ref dsMovBanco.dtMovBancoRow dtrowaInsertar, ref String sError, int userID, int cuentaID, bool esanticipo, int idproductoranticipo, ref ListBox lista, int tipoanticipo, double interesanual, double interesmoratorio, DateTime fechalimitedepago, int cicloID, string productorname)
        {
            int currentmonth, currentyear, previousmonth, previousyear;
            currentmonth = dtrowaInsertar.fecha.Month;
            currentyear = dtrowaInsertar.fecha.Year;
            bool retorno = true;
            string qryIns = "SELECT saldo from SaldosMensualesBancos where month = @month and year = @year and cuentaID = @cuentaID";

            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            try
            {
                cmdIns.Parameters.Add("@month", SqlDbType.Int).Value = currentmonth;
                cmdIns.Parameters.Add("@year", SqlDbType.Int).Value = currentyear;
                cmdIns.Parameters.Add("@cuentaID", SqlDbType.Int).Value = cuentaID;

                conGaribay.Open();
                if (cmdIns.ExecuteScalar() == null)
                {

                    // NO HAY SALDO PARA EL CURRENT MONTH
                    //CHECAMOS QUE EXISTA SALDO PARA EL MES ANTERIOR 

                    qryIns = "SELECT top 1(saldo) from SaldosMensualesBancos where fecha< @fecha and cuentaID = @cuentaID order by(fecha) DESC";
                    cmdIns.CommandText = qryIns;
                    DateTime fechaaux = new DateTime(currentyear, currentmonth, 1);
                    ///always remove parameters if the command is going to be reused
                    cmdIns.Parameters.Clear();
                    cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fechaaux;
                    cmdIns.Parameters.Add("@cuentaID", SqlDbType.Int).Value = cuentaID;
                    double saldo;
                    if (cmdIns.ExecuteScalar() != null)
                    {
                        saldo = double.Parse(cmdIns.ExecuteScalar().ToString());
                    }
                    else
                    {
                        saldo = 0.00f;
                    }
                    qryIns = "Insert into SaldosMensualesBancos(cuentaID,month,year,saldo,storeTS,updateTS,fecha) values(@cuentaID,@month,@year,@saldo,@storeTS,@updateTS, @fecha);";
                    cmdIns.Parameters.Clear();
                    cmdIns.Parameters.Add("@cuentaID", SqlDbType.Int).Value = cuentaID;
                    cmdIns.Parameters.Add("@month", SqlDbType.Int).Value = currentmonth;
                    cmdIns.Parameters.Add("@year", SqlDbType.Int).Value = currentyear;
                    cmdIns.Parameters.Add("@saldo", SqlDbType.Money).Value = saldo;
                    cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                    cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                    DateTime aux = new DateTime(currentyear, currentmonth, 28);
                    cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = aux;
                    cmdIns.CommandText = qryIns;
                    int numregistros;
                    numregistros = cmdIns.ExecuteNonQuery();
                    if (numregistros != 1)
                    {
                        throw new Exception("AL TRATAR DE AGREGAR EL SALDO DEL MES " + Utils.getMonthName(currentmonth) + ". SE MODIFICARON: " + numregistros.ToString() + " REGISTROS");
                    }




                }


                if (cuentaID == 14 && myConfig.GetBoolConfig("CHANGESIN5516", myConfig.CATEGORIA.MOVBANCOS, true) && new BasePage().UserID != 10)
                {
                    try
                    {
                        StringBuilder sBody = new StringBuilder();
                        sBody.Append("El Usuario "+ new BasePage().CurrentUserName + " agrego movimiento en la cuenta 5516 :");
                        foreach (DataColumn col in dtrowaInsertar.Table.Columns )
                        {
                            sBody.Append("<BR />");
                            sBody.Append(col.ColumnName);
                            sBody.Append(": ");
                            sBody.Append(dtrowaInsertar[col].ToString());
                        }
                        EMailUtils.SendTextEmail("cheliskis@gmail.com,mercedesdeandaorozco@hotmail.com", "modificacion en cuenta 5516",
                            sBody.ToString(), true);
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.SELECT, "err notificando cambios en cuenta", ref ex);
                    }
                }


                //INSERTAMOS EL MOVIMIENTO
                qryIns = "INSERT INTO MovimientosCuentasBanco (cuentaID, ConceptoMovCuentaID, fecha, cargo, abono, userID, nombre, facturaOlarguillo, numCabezas, numCheque, chequeNombre, catalogoMovBancoFiscalID, subCatalogoMovBancoFiscalID, catalogoMovBancoInternoID, subCatalogoMovBancoInternoID, creditoFinancieroID, fechacobrado, fechaCheque, chequecobrado) "
                    + " VALUES (@cuentaID,@ConceptoMovCuentaID,@fecha,@cargo,@abono,@userID,@nombre,@facturaOlarguillo,@numCabezas,@numCheque,@chequeNombre, @catalogoMovBancoFiscalID, @subCatalogoMovBancoFiscalID,@catalogoMovBancoInternoID,@subCatalogoMovBancoInternoID, @creditoFinancieroID, @fechacobrado, @fechaCheque, @chequecobrado);";
                qryIns += "SELECT NewID = SCOPE_IDENTITY();";
                cmdIns.Parameters.Clear();
                cmdIns.CommandText = qryIns;

                cmdIns.Parameters.Add("@cuentaID", SqlDbType.Int).Value = cuentaID;
                cmdIns.Parameters.Add("@ConceptoMovCuentaID", SqlDbType.Int).Value = dtrowaInsertar.conceptoID;
                cmdIns.Parameters.Add("@nombre", SqlDbType.VarChar).Value = dtrowaInsertar.nombre;
                cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtrowaInsertar.fecha;
                cmdIns.Parameters.Add("@cargo", SqlDbType.Money).Value = dtrowaInsertar.cargo;
                cmdIns.Parameters.Add("@abono", SqlDbType.Money).Value = dtrowaInsertar.abono;
                cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                cmdIns.Parameters.Add("@facturaOlarguillo", SqlDbType.NVarChar).Value = dtrowaInsertar.facturaOlarguillo;
                cmdIns.Parameters.Add("@numCabezas", SqlDbType.Float).Value = dtrowaInsertar.numCabezas;
                cmdIns.Parameters.Add("@numCheque", SqlDbType.Int).Value = dtrowaInsertar.numCheque;
                cmdIns.Parameters.Add("@chequeNombre", SqlDbType.NVarChar).Value = dtrowaInsertar.chequeNombre;
                cmdIns.Parameters.Add("@catalogoMovBancoFiscalID", SqlDbType.Int).Value = dtrowaInsertar.catalogoMovBancoFiscalID;
                cmdIns.Parameters.Add("@subCatalogoMovBancoFiscalID", SqlDbType.Int).Value = dtrowaInsertar.subCatalogoMovBancoFiscalID;
                cmdIns.Parameters.Add("@catalogoMovBancoInternoID", SqlDbType.Int).Value = dtrowaInsertar.catalogoMovBancoInternoID;
                cmdIns.Parameters.Add("@subCatalogoMovBancoInternoID", SqlDbType.Int).Value = dtrowaInsertar.subCatalogoMovBancoInternoID;
                cmdIns.Parameters.Add("@creditoFinancieroID", SqlDbType.Int).Value = dtrowaInsertar.creditoFinancieroID;
                if (dtrowaInsertar.IsfechachequedecobroNull())
                {
                    cmdIns.Parameters.Add("@fechacobrado", SqlDbType.DateTime).Value = dtrowaInsertar.fecha;
                    cmdIns.Parameters.Add("@chequecobrado", SqlDbType.Bit).Value = 0;
                }
                else
                {
                    cmdIns.Parameters.Add("@fechacobrado", SqlDbType.DateTime).Value = dtrowaInsertar.fechachequedecobro;
                    cmdIns.Parameters.Add("@chequecobrado", SqlDbType.Bit).Value = (dtrowaInsertar.IschequecobradoNull() || !dtrowaInsertar.chequecobrado)? 0 : 1;
                }
                               
                cmdIns.Parameters.Add("@fechaCheque", SqlDbType.DateTime).Value = dtrowaInsertar.fecha;


               
             //   cmdIns.Parameters.Add("@chequeQuienRecibe", SqlDbType.NVarChar).Value = dtrowaInsertar.chequeQuienRecibe;
                /*
                int rowsafected = cmdIns.ExecuteNonQuery();
                                if (rowsafected != 1)
                                {
                                    throw new Exception(string.Format(myConfig.StrFromMessages("MOVIMIENTOCUENTAEXECUTEFAILED"), "AGREGADO", "AGREGARON", rowsafected.ToString()));
                                }
                                string sqlst;
                                sqlst = "SELECT max(movbanID) FROM MovimientosCuentasBanco";
                                cmdIns.CommandText = sqlst;*/

                double cantidad;
                dtrowaInsertar.movBanID = int.Parse(cmdIns.ExecuteScalar().ToString());
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDEBANCO, Logger.typeUserActions.INSERT, userID, "AGREGO EL MOVIMIENTO DE BANCO NÚMERO: " + dtrowaInsertar.movBanID.ToString());
                if (esanticipo) {

                    if (insertAnticipo(cicloID, idproductoranticipo, userID, ref lista, dtrowaInsertar.movBanID, -1, interesanual, interesmoratorio, fechalimitedepago, ref sError, productorname, dtrowaInsertar.cargo, dtrowaInsertar.fecha))
                    {

                    }
                    else
                    {
                        retorno = false;

                    }
                }
                if (dtrowaInsertar.cargo == 0)
                {// ES ABONO
                    cantidad = double.Parse(dtrowaInsertar.abono.ToString());
                }
                else
                {
                    cantidad = double.Parse((dtrowaInsertar.cargo * -1).ToString());
                }
                if (!actualizaSaldoMeses(cantidad, currentmonth, currentyear, ref sError, userID, cuentaID))
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, "NO SE PUDO ACTUALIZAR EL SALDO DE MESES", "EL ERROR SE DIO AL AGREGAR EL MOVIMIENTO NÚMERO: " + dtrowaInsertar.movBanID.ToString() + "EN LA FECHA: " + Utils.getNowFormattedDate());
                }
            }
            catch (Exception exception)
            {
                sError = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, exception.Message, "EL ERROR SE DIO AL TRATAR DE AGREGAR UN MOVIMIENTO DE BANCO");
                retorno=  false;

            }
            finally
            {
                conGaribay.Close();
            }



            return retorno;
        }

        /// <summary>
        /// update all the saldos of meses after or equal of month received.
        /// </summary>
        /// <param name="cantidad"> cantidad to add to the saldo this must be negative for cargos and positive for abonos</param>
        /// <param name="month">Mes</param>
        /// <param name="year">año</param>
        /// <param name="sError">by ref return the error description if a execption is thrown</param>
        /// <param name="userID"></param>
        /// <param name="cuentaID"> cuenta ID to update</param>
        /// <returns></returns>
        public static bool actualizaSaldoMeses(double cantidad, int month, int year, ref String sError, int userID, int cuentaID)
        {
            string qrySel = "SELECT saldo, saldomensualID from SaldosMensualesBancos where fecha >= @fecha and cuentaID = @cuentaID";
            string qryIns;
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlConnection conGaribay2 = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdInsert = new SqlCommand();
            SqlCommand cmdSel = new SqlCommand(qrySel, conGaribay);
            String fecha;
            fecha = year.ToString() + "/" + month.ToString() + "/" + "01 00:00:00";

            try
            {
                cmdSel.Parameters.Clear();
                cmdSel.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Parse(fecha).ToString("yyyy/MM/dd 00:00:00");
                cmdSel.Parameters.Add("@cuentaID", SqlDbType.Int).Value = cuentaID;
                conGaribay.Open();
                conGaribay2.Open();
                SqlDataReader rowsamodificar;
                rowsamodificar = cmdSel.ExecuteReader();
                if (rowsamodificar.HasRows)
                {//HAY REGISTROS A MODIFICAR
                    while (rowsamodificar.Read())
                    {
                        cmdInsert.Parameters.Clear();
                        double saldoanterior, nuevosaldo;
                        string saldomensualID = rowsamodificar["saldomensualID"].ToString();
                        saldoanterior = double.Parse(rowsamodificar[0].ToString());
                        nuevosaldo = saldoanterior + cantidad;
                        qryIns = "Update SaldosMensualesBancos set saldo = @saldonuevo, updateTS = @updateTS where saldomensualID = @saldomensualID and cuentaID = @cuentaID";
                        cmdInsert.CommandText = qryIns;
                        cmdInsert.Connection = conGaribay2;
                        cmdInsert.Parameters.Add("@saldonuevo", SqlDbType.Money).Value = nuevosaldo;
                        cmdInsert.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                        cmdInsert.Parameters.Add("@saldomensualID", SqlDbType.Int).Value = saldomensualID;
                        cmdInsert.Parameters.Add("@cuentaID", SqlDbType.Int).Value = cuentaID;
                     //   Logger.Instance.LogMessage(Logger.typeLogMessage.DEBUG, Logger.typeUserActions.UPDATE, userID, "SALDO ANTERIOR: " + saldoanterior.ToString() + ", SALDO NUEVO: " + nuevosaldo.ToString() + ", saldomensualID: " + saldomensualID, "actualizaSaldoMeses function");
                        int registrosmodified = cmdInsert.ExecuteNonQuery();

                        if (registrosmodified != 1)
                        {
                            throw new Exception("AL INTENTAR ACTUALIZAR EL SALDO DE UN MES, NO SE ACTUALIZÓ EL NÚMERO DE REGISTROS ESPERADOS");
                        }
                    }


                }

            }
            catch (Exception exception)
            {
                sError = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, exception.Message, "EL ERROR SE DIO AL TRATAR DE ACTUALIZAR EL SALDO DE UN MES");

            }
            finally
            {
                conGaribay.Close();
                conGaribay2.Close();
            }
            return true;
        }

        ///MOV CAJA CHICA

        /// <summary>
        /// fill a data table with the movements of bancos between dates calculating the saldos
        /// </summary>
        /// <param name="cuentaID"></param>
        /// <param name="dtFechainicio"></param>
        /// <param name="dtFechafin"></param>
        /// <param name="fSaldoInicial"></param>
        /// <param name="fSaldoFinal"></param>
        /// <param name="dtTable"></param>
        /// <returns></returns>
        public static bool fillDTMovCajaChica(DateTime dtFechainicio, DateTime dtFechafin, ref float fSaldoInicial, ref float fSaldoFinal, ref dsMovCajaChica.dtMovCajaChicaDataTable dtMBTable, int bodegaID)
        {
            if (dtMBTable == null || dtFechafin == null || dtFechainicio == null)
            {
                return false;
            }
            fSaldoFinal = fSaldoInicial = 0.0F;
            try
            {
                //get the saldo of the previous month
                string sSql = "SELECT top 1 saldo, month, year from SaldosMensualesCajaChica where fecha< @fecha and bodegaID = @bodegaID order by(fecha) DESC";
                SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand sqlComm = new SqlCommand(sSql, sqlConn);
                sqlConn.Open();
                sqlComm.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtFechainicio.ToString("yyyy/MM/dd 00:00:00");
                sqlComm.Parameters.Add("@bodegaID", SqlDbType.Int).Value = bodegaID;
                SqlDataReader sqlReader = sqlComm.ExecuteReader();
                if (sqlReader.HasRows && sqlReader.Read())
                {
                    DateTime dtSaldo = new DateTime();
                    if (int.Parse(sqlReader["month"].ToString()) < 12)
                    {
                         dtSaldo = new DateTime(int.Parse(sqlReader["year"].ToString()), int.Parse(sqlReader["month"].ToString()) + 1, 1);
                    }
                    else{
                         dtSaldo = new DateTime(int.Parse(sqlReader["year"].ToString())+1, 1, 1);

                    }
                    fSaldoInicial = float.Parse(sqlReader["saldo"].ToString());
                    //now calculate the movements between the last saldo and the days between 
                    //the last saldo and the immediately previous days
                    sSql = "SELECT sum(abono - cargo) from MovimientosCaja where bodegaID = @bodegaID and fecha >= @fechaini and fecha < @fechafin ";
                    SqlConnection sqlConnSaldo = new SqlConnection(myConfig.ConnectionInfo);
                    SqlCommand sqlCommSaldo = new SqlCommand(sSql, sqlConnSaldo);
                    sqlConnSaldo.Open();
                    sqlCommSaldo.Parameters.Add("@bodegaID", SqlDbType.Int).Value = bodegaID;
                    sqlCommSaldo.Parameters.Add("@fechaini", SqlDbType.DateTime).Value = dtSaldo.ToString("yyyy/MM/dd 00:00:00");
                    sqlCommSaldo.Parameters.Add("@fechafin", SqlDbType.DateTime).Value = dtFechainicio.ToString("yyyy/MM/dd 00:00:00");
                    try
                    {
                        if (sqlCommSaldo.ExecuteScalar() != null)
                        {
                            string saldo = sqlCommSaldo.ExecuteScalar().ToString();
                            if (saldo != "")
                            {
                                fSaldoInicial += float.Parse(saldo);
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        //Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL,Logger.typeUserActions.SELECT,-1,"Error executing query", "dbfunctions.cs SELECT sum(abono - cargo) from MovimientosCuentasBanco where fecha >= @fechaini and fecha < @fechafin and cuentaID = @cuentaID");
                    }
                    finally
                    {
                        sqlConnSaldo.Close();
                    }
                }
                sqlConn.Close();
                sqlComm.Parameters.Clear();
                // sSql = "SELECT movimientoID, Observaciones, fecha, nombre, abono, cargo, userID, storeTS, updateTS, concepto from MovimientosCaja where fecha >= @fechaini and fecha <= @fechafin order by(fecha) ASC";
                sSql = "SELECT     MovimientosCaja.movimientoID, MovimientosCaja.bodegaID, MovimientosCaja.fecha, MovimientosCaja.nombre, MovimientosCaja.cargo, MovimientosCaja.abono, "
                + " MovimientosCaja.Observaciones, MovimientosCaja.userID, MovimientosCaja.updateTS, GruposCatalogosMovBancos.grupoCatalogo, "
                + " catalogoMovimientosBancos.catalogoMovBanco, SubCatalogoMovimientoBanco.subCatalogo, MovimientosCaja.facturaOlarguillo, MovimientosCaja.storeTS, " 
                + " MovimientosCaja.numCabezas, catalogoMovimientosBancos.catalogoMovBancoID, SubCatalogoMovimientoBanco.subCatalogoMovBancoID, Bodegas.bodega, MovimientosCaja.cobrado "
                + " FROM         MovimientosCaja INNER JOIN "
                + " catalogoMovimientosBancos ON MovimientosCaja.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID INNER JOIN "
                + " GruposCatalogosMovBancos ON catalogoMovimientosBancos.grupoCatalogoID = GruposCatalogosMovBancos.grupoCatalogosID INNER JOIN" 
                + " Bodegas ON MovimientosCaja.bodegaID = Bodegas.bodegaID LEFT OUTER JOIN "
                + " SubCatalogoMovimientoBanco ON MovimientosCaja.subCatalogoMovBancoID = SubCatalogoMovimientoBanco.subCatalogoMovBancoID where "
                + " MovimientosCaja.bodegaID = @bodegaID and MovimientosCaja.fecha >= @fechaini and MovimientosCaja.fecha <= @fechafin "
                + " ORDER BY MovimientosCaja.fecha DESC, MovimientosCaja.abono ASC, MovimientosCaja.cargo ASC";



                sqlComm.CommandText = sSql;
                sqlComm.Parameters.Add("@bodegaID", SqlDbType.Int).Value = bodegaID;
                sqlComm.Parameters.Add("@fechaini", SqlDbType.DateTime).Value = dtFechainicio.ToString("yyyy/MM/dd 00:00:00");
                sqlComm.Parameters.Add("@fechafin", SqlDbType.DateTime).Value = dtFechafin.ToString("yyyy/MM/dd 23:59:59");
                sqlConn.Open();
                sqlReader = sqlComm.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    dtMBTable.Rows.Clear();
                    float fCurrSaldo = fSaldoInicial;
                    while (sqlReader.Read())
                    {
                        int movID = 0, userID = 0;
                        float cargo = 0, abono = 0;
                      //  string concepto = "";
                        try
                        {
                            movID = int.Parse(sqlReader["movimientoID"].ToString());
                            userID = int.Parse(sqlReader["userID"].ToString());
                            abono = sqlReader["abono"] == null ? 0.0f : float.Parse(sqlReader["abono"].ToString());
                            cargo = sqlReader["cargo"] == null ? 0.0f : float.Parse(sqlReader["cargo"].ToString());
                            fCurrSaldo += abono - cargo;
                            fSaldoFinal = fCurrSaldo;
                            double numcabezas=0f;
                            if (sqlReader["numCabezas"]!=null){
                              ;
                            }

                            //AKI VOY/////////////////////////////////////////////////////////7
                            dtMBTable.AdddtMovCajaChicaRow(movID,
                                int.Parse(sqlReader["bodegaID"].ToString()),
                                sqlReader["bodega"].ToString(),
                                DateTime.Parse(sqlReader["fecha"].ToString()),
                                sqlReader["nombre"].ToString(),
                                cargo,
                                abono,
                                fCurrSaldo, sqlReader["Observaciones"].ToString(),
                                DateTime.Parse(sqlReader["storeTS"].ToString()),
                                DateTime.Parse(sqlReader["updateTS"].ToString()),
                                int.Parse(sqlReader["userID"].ToString()),
                                int.Parse(sqlReader["catalogoMovBancoID"].ToString()),
                                sqlReader["catalogoMovBanco"].ToString(),
                                sqlReader["facturaOlarguillo"].ToString().Length <= 0 ? "" : sqlReader["facturaOlarguillo"].ToString(),
                                sqlReader["subCatalogoMovBancoID"].ToString().Length <= 0 ? -1 : int.Parse(sqlReader["subCatalogoMovBancoID"].ToString()),
                                sqlReader["subCatalogo"].ToString().Length <= 0 ? "" : sqlReader["subCatalogo"].ToString(), double.Parse(sqlReader["numCabezas"].ToString()), bool.Parse(sqlReader["cobrado"].ToString()));

                        }
                        catch (System.Exception ex)
                        {
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {

            }
            return true;
        }

        /// <summary>
        /// insert a new movement of cuenta
        /// </summary>
        /// <param name="dtrowaInsertar"> by ref dtMovBancoRow with all the data to insert</param>
        /// <param name="sError"> by ref error description if a exception ocurrs</param>
        /// <param name="userID"> current user</param>
        /// <param name="cuentaID"> cuenta id</param>
        /// <param name="cicloID"></param>
        /// <returns></returns>
        /// 
        public static bool insertaMovCaja(ref dsMovCajaChica.dtMovCajaChicaRow dtrowaInsertar, ref String sError, int userID, int cicloID)
        {
            bool valorretorno = true;
            int currentmonth, currentyear, previousmonth, previousyear;
            currentmonth = dtrowaInsertar.fecha.Month;
            currentyear = dtrowaInsertar.fecha.Year;
            string qryIns = "SELECT saldo from SaldosMensualesCajaChica where bodegaID = @bodegaID AND  month = @month and year = @year";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            try
            {
                cmdIns.Parameters.Add("@bodegaID", SqlDbType.Int).Value = dtrowaInsertar.bodegaID;
                cmdIns.Parameters.Add("@month", SqlDbType.Int).Value = currentmonth;
                cmdIns.Parameters.Add("@year", SqlDbType.Int).Value = currentyear;
                conGaribay.Open();
                if (cmdIns.ExecuteScalar() == null)
                {
                    // NO HAY SALDO PARA EL CURRENT MONTH
                    //CHECAMOS QUE EXISTA SALDO PARA EL MES ANTERIOR 
                    qryIns = "SELECT top 1(saldo) from SaldosMensualesCajaChica where bodegaID = @bodegaID and  fecha< @fecha order by(fecha) DESC";
                    cmdIns.CommandText = qryIns;
                    DateTime fechaaux = new DateTime(currentyear, currentmonth, 1);
                    ///always remove parameters if the command is going to be reused
                    cmdIns.Parameters.Clear();
                    cmdIns.Parameters.Add("@bodegaID", SqlDbType.Int).Value = dtrowaInsertar.bodegaID;
                    cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fechaaux;
                    float saldo;
                    if (cmdIns.ExecuteScalar() != null)
                    {
                        saldo = float.Parse(cmdIns.ExecuteScalar().ToString());
                    }
                    else
                    {
                        saldo = 0.00f;
                    }
                    qryIns = "Insert into SaldosMensualesCajaChica(bodegaID,month,year,saldo,storeTS,updateTS,fecha) values(@bodegaID, @month,@year,@saldo,@storeTS,@updateTS, @fecha)";
                    cmdIns.Parameters.Clear();
                    cmdIns.Parameters.Add("@bodegaID", SqlDbType.Int).Value = dtrowaInsertar.bodegaID;
                    cmdIns.Parameters.Add("@month", SqlDbType.Int).Value = currentmonth;
                    cmdIns.Parameters.Add("@year", SqlDbType.Int).Value = currentyear;
                    cmdIns.Parameters.Add("@saldo", SqlDbType.Money).Value = saldo;
                    cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                    cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                    DateTime aux = new DateTime(currentyear, currentmonth, 28);
                    cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = aux;
                    cmdIns.CommandText = qryIns;
                    int numregistros;
                    numregistros = cmdIns.ExecuteNonQuery();
                    if (numregistros != 1)
                    {
                        throw new Exception("AL TRATAR DE AGREGAR EL SALDO DEL MES " + Utils.getMonthName(currentmonth) + ". SE MODIFICARON: " + numregistros.ToString() + " REGISTROS");
                    }
                }
                //INSERTAMOS EL MOVIMIENTO
                qryIns = "INSERT INTO  MovimientosCaja(cicloID, userID, nombre, abono, cargo, Observaciones, storeTS, updateTS, fecha, catalogoMovBancoID, subCatalogoMovBancoID, bodegaID, cobrado, facturaOlarguillo, numCabezas) VALUES (@cicloID, @userID, @nombre, @abono, @cargo, @Observaciones, @storeTS, @updateTS, @fecha, @catalogoMovBancoID, @subCatalogoMovBancoID, @bodegaID, @cobrado, @facturaOlarguillo, @numCabezas)";
                cmdIns.Parameters.Clear();
                cmdIns.CommandText = qryIns;
                cmdIns.Parameters.Add("@cicloID", SqlDbType.Int).Value = cicloID;
                cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                cmdIns.Parameters.Add("@nombre", SqlDbType.VarChar).Value = dtrowaInsertar.nombre;
                cmdIns.Parameters.Add("@abono", SqlDbType.Money).Value = dtrowaInsertar.abono;
                cmdIns.Parameters.Add("@cargo", SqlDbType.Money).Value = dtrowaInsertar.cargo;
                cmdIns.Parameters.Add("@Observaciones", SqlDbType.Text).Value = dtrowaInsertar.Observaciones;
                cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = dtrowaInsertar.storeTS;
                cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = dtrowaInsertar.updateTS;
                cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtrowaInsertar.fecha;
                cmdIns.Parameters.Add("@catalogoMovBancoID", SqlDbType.Int).Value = dtrowaInsertar.catalogoMovBancoInternoID;
                cmdIns.Parameters.Add("@subCatalogoMovBancoID", SqlDbType.Int).Value = dtrowaInsertar.subCatalogoMovBancoInternoID;
                cmdIns.Parameters.Add("@bodegaID", SqlDbType.Int).Value = dtrowaInsertar.bodegaID;
                cmdIns.Parameters.Add("@cobrado", SqlDbType.Bit).Value = dtrowaInsertar.cobrado;
                cmdIns.Parameters.Add("@facturaOlarguillo", SqlDbType.Text).Value = dtrowaInsertar.facturaOlarguillo;
                cmdIns.Parameters.Add("@numCabezas", SqlDbType.Float).Value = dtrowaInsertar.numCabezas;

                int rowsafected = cmdIns.ExecuteNonQuery();
                if (rowsafected != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("MOVCAJAEXECUTEFAILED"), "AGREGADO", "AGREGARON", rowsafected.ToString()));
                }
                string sqlst;
                sqlst = "SELECT max(movimientoID) FROM MovimientosCaja";
                cmdIns.CommandText = sqlst;
                double cantidad;
                dtrowaInsertar.movimientoID = (int)cmdIns.ExecuteScalar();
                cantidad = dtrowaInsertar.abono - dtrowaInsertar.cargo;
                if (!actualizaSaldoMesesCajaChica(cantidad, currentmonth, currentyear, ref sError, userID))
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, "NO SE PUDO ACTUALIZAR EL SALDO DE MESES", "EL ERROR SE DIO AL AGREGAR EL MOVIMIENTO DE CAJA NÚMERO: " + dtrowaInsertar.movimientoID.ToString() + "EN LA FECHA: " + Utils.getNowFormattedDate());
                }
            }
            catch (Exception exception)
            {
                sError = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, exception.Message, "EL ERROR SE DIO AL TRATAR DE AGREGAR UN MOVIMIENTO DE CAJA CHICA");
                valorretorno = false;
            }
            finally
            {
                conGaribay.Close();
            }
            return valorretorno;
        }

        public static bool insertaMovBanco(ref dsMovBanco.dtMovBancoRow dtrowaInsertar, ref String sError, int userID, int cuentaID,int cicloID, int movOrigenID, string chequeQuienrecibe, string Observaciones)
        {
            int currentmonth, currentyear, previousmonth, previousyear;
            currentmonth = dtrowaInsertar.fecha.Month;
            currentyear = dtrowaInsertar.fecha.Year;
            bool retorno = true;
            string qryIns = "SELECT saldo from SaldosMensualesBancos where month = @month and year = @year and cuentaID = @cuentaID";

            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            try
            {
                cmdIns.Parameters.Add("@month", SqlDbType.Int).Value = currentmonth;
                cmdIns.Parameters.Add("@year", SqlDbType.Int).Value = currentyear;
                cmdIns.Parameters.Add("@cuentaID", SqlDbType.Int).Value = cuentaID;

                conGaribay.Open();
                if (cmdIns.ExecuteScalar() == null)
                {

                    // NO HAY SALDO PARA EL CURRENT MONTH
                    //CHECAMOS QUE EXISTA SALDO PARA EL MES ANTERIOR 

                    qryIns = "SELECT top 1(saldo) from SaldosMensualesBancos where fecha< @fecha and cuentaID = @cuentaID order by(fecha) DESC";
                    cmdIns.CommandText = qryIns;
                    DateTime fechaaux = new DateTime(currentyear, currentmonth, 1);
                    ///always remove parameters if the command is going to be reused
                    cmdIns.Parameters.Clear();
                    cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fechaaux;
                    cmdIns.Parameters.Add("@cuentaID", SqlDbType.Int).Value = cuentaID;
                    double saldo;
                    if (cmdIns.ExecuteScalar() != null)
                    {
                        saldo = double.Parse(cmdIns.ExecuteScalar().ToString());
                    }
                    else
                    {
                        saldo = 0.00f;
                    }
                    qryIns = "Insert into SaldosMensualesBancos(cuentaID,month,year,saldo,storeTS,updateTS,fecha) values(@cuentaID,@month,@year,@saldo,@storeTS,@updateTS, @fecha);";
                    cmdIns.Parameters.Clear();
                    cmdIns.Parameters.Add("@cuentaID", SqlDbType.Int).Value = cuentaID;
                    cmdIns.Parameters.Add("@month", SqlDbType.Int).Value = currentmonth;
                    cmdIns.Parameters.Add("@year", SqlDbType.Int).Value = currentyear;
                    cmdIns.Parameters.Add("@saldo", SqlDbType.Money).Value = saldo;
                    cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                    cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                    DateTime aux = new DateTime(currentyear, currentmonth, 28);
                    cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = aux;
                    cmdIns.CommandText = qryIns;
                    int numregistros;
                    numregistros = cmdIns.ExecuteNonQuery();
                    if (numregistros != 1)
                    {
                        throw new Exception("AL TRATAR DE AGREGAR EL SALDO DEL MES " + Utils.getMonthName(currentmonth) + ". SE MODIFICARON: " + numregistros.ToString() + " REGISTROS");
                    }




                }
                //INSERTAMOS EL MOVIMIENTO
                qryIns = "INSERT INTO MovimientosCuentasBanco (cuentaID, ConceptoMovCuentaID, fecha, cargo, abono, userID, nombre, facturaOlarguillo, numCabezas, numCheque, chequeNombre, chequeQuienrecibe, chequecobrado, fechacobrado, Observaciones, catalogoMovBancoFiscalID, subCatalogoMovBancoFiscalID, catalogoMovBancoInternoID, subCatalogoMovBancoInternoID, creditoFinancieroID, storeTS, updateTS, movOrigenID, fechaCheque) VALUES ( @cuentaID, @ConceptoMovCuentaID, @fecha, @cargo, @abono, @userID, @nombre, @facturaOlarguillo, @numCabezas, @numCheque, @chequeNombre, @chequeQuienrecibe, @chequecobrado, @fechacobrado, @Observaciones, @catalogoMovBancoFiscalID, @subCatalogoMovBancoFiscalID, @catalogoMovBancoInternoID, @subCatalogoMovBancoInternoID, @creditoFinancieroID, @storeTS, @updateTS, @movOrigenID, @fechaCheque);";
                qryIns += "SELECT NewID = SCOPE_IDENTITY();";
                cmdIns.Parameters.Clear();
                cmdIns.CommandText = qryIns;

                //cmdIns.Parameters.Add("@cicloID", SqlDbType.Int).Value = cicloID;
                cmdIns.Parameters.Add("@cuentaID", SqlDbType.Int).Value = cuentaID;
                cmdIns.Parameters.Add("@ConceptoMovCuentaID", SqlDbType.Int).Value = dtrowaInsertar.conceptoID;
                cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtrowaInsertar.fecha;
                cmdIns.Parameters.Add("@cargo", SqlDbType.Money).Value = dtrowaInsertar.cargo;
                cmdIns.Parameters.Add("@abono", SqlDbType.Money).Value = dtrowaInsertar.abono;
                cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                cmdIns.Parameters.Add("@nombre", SqlDbType.VarChar).Value = dtrowaInsertar.nombre;
                cmdIns.Parameters.Add("@facturaOlarguillo", SqlDbType.NVarChar).Value = dtrowaInsertar.facturaOlarguillo;
                cmdIns.Parameters.Add("@numCabezas", SqlDbType.Float).Value = dtrowaInsertar.numCabezas;
                cmdIns.Parameters.Add("@numCheque", SqlDbType.Int).Value = dtrowaInsertar.numCheque;
                cmdIns.Parameters.Add("@chequeNombre", SqlDbType.NVarChar).Value = dtrowaInsertar.chequeNombre;
                cmdIns.Parameters.Add("@chequeQuienrecibe", SqlDbType.NVarChar).Value = chequeQuienrecibe;
                //cmdIns.Parameters.Add("@chequecobrado", SqlDbType.Bit).Value = dtrowaInsertar.chequecobrado;
                //cmdIns.Parameters.Add("@fechacobrado", SqlDbType.DateTime).Value = dtrowaInsertar.fecha;
                cmdIns.Parameters.Add("@Observaciones", SqlDbType.Text).Value = Observaciones;
                cmdIns.Parameters.Add("@catalogoMovBancoFiscalID", SqlDbType.Int).Value = dtrowaInsertar.catalogoMovBancoFiscalID;
                cmdIns.Parameters.Add("@subCatalogoMovBancoFiscalID", SqlDbType.Int).Value = dtrowaInsertar.subCatalogoMovBancoFiscalID;
                cmdIns.Parameters.Add("@catalogoMovBancoInternoID", SqlDbType.Int).Value = dtrowaInsertar.catalogoMovBancoInternoID;
                cmdIns.Parameters.Add("@subCatalogoMovBancoInternoID", SqlDbType.Int).Value = dtrowaInsertar.subCatalogoMovBancoInternoID;
                cmdIns.Parameters.Add("@creditoFinancieroID", SqlDbType.Int).Value = dtrowaInsertar.creditoFinancieroID;
                cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = dtrowaInsertar.storeTS;
                cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = dtrowaInsertar.updateTS;
                if (movOrigenID == -1)
                {
                    cmdIns.Parameters.Add("@movOrigenID", SqlDbType.Int).Value = DBNull.Value;
                }
                else
                {
                    cmdIns.Parameters.Add("@movOrigenID", SqlDbType.Int).Value = movOrigenID;
                }
                //cmdIns.Parameters.Add("@fechaCheque", SqlDbType.DateTime).Value = dtrowaInsertar.fecha;
                if (dtrowaInsertar.IsfechachequedecobroNull())
                {
                    cmdIns.Parameters.Add("@fechacobrado", SqlDbType.DateTime).Value = dtrowaInsertar.fecha;
                    cmdIns.Parameters.Add("@chequecobrado", SqlDbType.Bit).Value = 0;
                }
                else
                {
                    cmdIns.Parameters.Add("@fechacobrado", SqlDbType.DateTime).Value = dtrowaInsertar.fechachequedecobro;
                    cmdIns.Parameters.Add("@chequecobrado", SqlDbType.Bit).Value = (dtrowaInsertar.IschequecobradoNull() || !dtrowaInsertar.chequecobrado) ? 0 : 1;
                }
                cmdIns.Parameters.Add("@fechaCheque", SqlDbType.DateTime).Value = dtrowaInsertar.fecha;


                //   cmdIns.Parameters.Add("@chequeQuienRecibe", SqlDbType.NVarChar).Value = dtrowaInsertar.chequeQuienRecibe;
                /*
                int rowsafected = cmdIns.ExecuteNonQuery();
                                if (rowsafected != 1)
                                {
                                    throw new Exception(string.Format(myConfig.StrFromMessages("MOVIMIENTOCUENTAEXECUTEFAILED"), "AGREGADO", "AGREGARON", rowsafected.ToString()));
                                }
                                string sqlst;
                                sqlst = "SELECT max(movbanID) FROM MovimientosCuentasBanco";
                                cmdIns.CommandText = sqlst;*/

                double cantidad;
                dtrowaInsertar.movBanID = int.Parse(cmdIns.ExecuteScalar().ToString());
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDEBANCO, Logger.typeUserActions.INSERT, userID, "AGREGO EL MOVIMIENTO DE BANCO NÚMERO: " + dtrowaInsertar.movBanID.ToString());
                //if (esanticipo)
                //{

                //    if (insertAnticipo(cicloID, idproductoranticipo, userID, ref lista, dtrowaInsertar.movBanID, -1, interesanual, interesmoratorio, fechalimitedepago, ref sError, productorname))
                //    {

                //    }
                //    else
                //    {
                //        retorno = false;

                //    }
                //}
                if (dtrowaInsertar.cargo == 0)
                {// ES ABONO
                    cantidad = double.Parse(dtrowaInsertar.abono.ToString());
                }
                else
                {
                    cantidad = double.Parse((dtrowaInsertar.cargo * -1).ToString());
                }
                if (!actualizaSaldoMeses(cantidad, currentmonth, currentyear, ref sError, userID, cuentaID))
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, "NO SE PUDO ACTUALIZAR EL SALDO DE MESES", "EL ERROR SE DIO AL AGREGAR EL MOVIMIENTO NÚMERO: " + dtrowaInsertar.movBanID.ToString() + "EN LA FECHA: " + Utils.getNowFormattedDate());
                }
            }
            catch (Exception exception)
            {
                sError = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, exception.Message, "EL ERROR SE DIO AL TRATAR DE AGREGAR UN MOVIMIENTO DE BANCO");
                retorno = false;

            }
            finally
            {
                conGaribay.Close();
            }



            return retorno;
        }


        public static bool insertMovCajaChica(ref dsMovCajaChica.dtMovCajaChicaRow dtrowaInsertar, ref String sError, int userID, int cicloID, bool esanticipo, int idproductoranticipo, ref ListBox lista,int tipoanticipo, float interesanual, float interesmoratorio, DateTime fechalimitedepago, string nombreproductor)
        {
            bool valorretorno = true;
            int currentmonth, currentyear, previousmonth, previousyear;
            currentmonth = dtrowaInsertar.fecha.Month;
            currentyear = dtrowaInsertar.fecha.Year;
            string qryIns = "SELECT saldo from SaldosMensualesCajaChica where bodegaID = @bodegaID AND  month = @month and year = @year";

            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            try
            {
                cmdIns.Parameters.Add("@bodegaID",SqlDbType.Int).Value = dtrowaInsertar.bodegaID;
                cmdIns.Parameters.Add("@month", SqlDbType.Int).Value = currentmonth;
                cmdIns.Parameters.Add("@year", SqlDbType.Int).Value = currentyear;

                conGaribay.Open();
                if (cmdIns.ExecuteScalar() == null)
                {

                    // NO HAY SALDO PARA EL CURRENT MONTH
                    //CHECAMOS QUE EXISTA SALDO PARA EL MES ANTERIOR 

                    qryIns = "SELECT top 1(saldo) from SaldosMensualesCajaChica where bodegaID = @bodegaID and  fecha< @fecha order by(fecha) DESC";
                    cmdIns.CommandText = qryIns;
                    DateTime fechaaux = new DateTime(currentyear, currentmonth, 1);
                    ///always remove parameters if the command is going to be reused
                    cmdIns.Parameters.Clear();
                    cmdIns.Parameters.Add("@bodegaID",SqlDbType.Int).Value = dtrowaInsertar.bodegaID;
                    cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fechaaux;
                    float saldo;
                    if (cmdIns.ExecuteScalar() != null)
                    {
                        saldo = float.Parse(cmdIns.ExecuteScalar().ToString());
                    }
                    else
                    {
                        saldo = 0.00f;
                    }
                    qryIns = "Insert into SaldosMensualesCajaChica(bodegaID,month,year,saldo,storeTS,updateTS,fecha) values(@bodegaID, @month,@year,@saldo,@storeTS,@updateTS, @fecha)";
                    cmdIns.Parameters.Clear();
                    cmdIns.Parameters.Add("@bodegaID",SqlDbType.Int).Value = dtrowaInsertar.bodegaID;
                    cmdIns.Parameters.Add("@month", SqlDbType.Int).Value = currentmonth;
                    cmdIns.Parameters.Add("@year", SqlDbType.Int).Value = currentyear;
                    cmdIns.Parameters.Add("@saldo", SqlDbType.Money).Value = saldo;
                    cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                    cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                    DateTime aux = new DateTime(currentyear, currentmonth, 28);
                    cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = aux;
                    cmdIns.CommandText = qryIns;
                    int numregistros;
                    numregistros = cmdIns.ExecuteNonQuery();
                    if (numregistros != 1)
                    {
                        throw new Exception("AL TRATAR DE AGREGAR EL SALDO DEL MES " + Utils.getMonthName(currentmonth) + ". SE MODIFICARON: " + numregistros.ToString() + " REGISTROS");
                    }




                }
                //INSERTAMOS EL MOVIMIENTO
                qryIns = "INSERT INTO  MovimientosCaja(bodegaID, fecha, cargo, abono, userID, cicloID, Observaciones, nombre, catalogoMovBancoID, subCatalogoMovBancoID, facturaOlarguillo, numCabezas) VALUES (@bodegaID, @fecha, @cargo, @abono, @userID, @cicloID, @Observaciones, @nombre, @catalogoMovBancoID, @subCatalogoMovBancoID, @facturaOlarguillo, @numCabezas)";
                cmdIns.Parameters.Clear();
                cmdIns.CommandText = qryIns;
                //cmdIns.Parameters.Add("@conceptomovID", SqlDbType.Int).Value = conceptomovID;
                cmdIns.Parameters.Add("@bodegaID", SqlDbType.Int).Value = dtrowaInsertar.bodegaID;
                cmdIns.Parameters.Add("@nombre", SqlDbType.VarChar).Value = dtrowaInsertar.nombre;
                cmdIns.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtrowaInsertar.fecha;
                cmdIns.Parameters.Add("@cargo", SqlDbType.Money).Value = dtrowaInsertar.cargo;
                cmdIns.Parameters.Add("@abono", SqlDbType.Money).Value = dtrowaInsertar.abono;
                cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
//                 cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = dtrowaInsertar.storeTS;
//                 cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = dtrowaInsertar.updateTS;
                cmdIns.Parameters.Add("@cicloID", SqlDbType.Int).Value = cicloID;
                cmdIns.Parameters.Add("@Observaciones", SqlDbType.Text).Value = dtrowaInsertar.Observaciones;
                cmdIns.Parameters.Add("@catalogoMovBancoID", SqlDbType.Int).Value = dtrowaInsertar.catalogoMovBancoInternoID;
                cmdIns.Parameters.Add("@subCatalogoMovBancoID", SqlDbType.Int).Value = dtrowaInsertar.subCatalogoMovBancoInternoID;
                cmdIns.Parameters.Add("@facturaOlarguillo", SqlDbType.Text).Value = dtrowaInsertar.facturaOlarguillo;
                cmdIns.Parameters.Add("@numCabezas", SqlDbType.Float).Value = dtrowaInsertar.numCabezas;

                int rowsafected = cmdIns.ExecuteNonQuery();
                if (rowsafected != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("MOVCAJAEXECUTEFAILED"), "AGREGADO", "AGREGARON", rowsafected.ToString()));
                }
                string sqlst;
                sqlst = "SELECT max(movimientoID) FROM MovimientosCaja";
                cmdIns.CommandText = sqlst;
                double cantidad;
                dtrowaInsertar.movimientoID = (int)cmdIns.ExecuteScalar();
                cantidad = dtrowaInsertar.abono - dtrowaInsertar.cargo;
                if (!actualizaSaldoMesesCajaChica(cantidad, currentmonth, currentyear, ref sError, userID))
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, "NO SE PUDO ACTUALIZAR EL SALDO DE MESES", "EL ERROR SE DIO AL AGREGAR EL MOVIMIENTO DE CAJA NÚMERO: " + dtrowaInsertar.movimientoID.ToString() + "EN LA FECHA: " + Utils.getNowFormattedDate());
                }
                if(esanticipo){
                    //INSERTAMOS EL ANTICIPO

                    if (insertAnticipo(cicloID, idproductoranticipo, userID, ref lista, -1, dtrowaInsertar.movimientoID, interesanual, interesmoratorio, fechalimitedepago, ref sError, nombreproductor, dtrowaInsertar.cargo, dtrowaInsertar.fecha))
                    {

                    }
                    else{
                        valorretorno = false;

                    }
                    
                }
            }
            catch (Exception exception)
            {
                sError = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, exception.Message, "EL ERROR SE DIO AL TRATAR DE AGREGAR UN MOVIMIENTO DE CAJA CHICA");
                valorretorno =  false;

            }
            finally
            {
                conGaribay.Close();
            }



            return valorretorno;
        }

        /// <summary>
        /// update all the saldos of meses after or equal of month received.
        /// </summary>
        /// <param name="cantidad"> cantidad to add to the saldo this must be negative for cargos and positive for abonos</param>
        /// <param name="month">Mes</param>
        /// <param name="year">año</param>
        /// <param name="sError">by ref return the error description if a execption is thrown</param>
        /// <param name="userID"></param>
        /// <param name="cuentaID"> cuenta ID to update</param>
        /// <returns></returns>
        public static bool actualizaSaldoMesesCajaChica(double cantidad, int month, int year, ref String sError, int userID)
        {
            string qrySel = "SELECT saldo, saldomensualID from SaldosMensualesCajaChica where fecha >= @fecha ";
            string qryIns;
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlConnection conGaribay2 = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdInsert = new SqlCommand();
            SqlCommand cmdSel = new SqlCommand(qrySel, conGaribay);
            String fecha;
            fecha = year.ToString() + "/" + month.ToString() + "/" + "01 00:00:00";

            try
            {
                cmdSel.Parameters.Clear();
                cmdSel.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Parse(fecha).ToString("yyyy/MM/dd 00:00:00");
                conGaribay.Open();
                conGaribay2.Open();
                SqlDataReader rowsamodificar;
                rowsamodificar = cmdSel.ExecuteReader();
                if (rowsamodificar.HasRows)
                {//HAY REGISTROS A MODIFICAR
                    while (rowsamodificar.Read())
                    {
                        cmdInsert.Parameters.Clear();
                        double saldoanterior, nuevosaldo;
                        string saldomensualID = rowsamodificar["saldomensualID"].ToString();
                        saldoanterior = double.Parse(rowsamodificar[0].ToString());
                        nuevosaldo = saldoanterior + cantidad;
                        qryIns = "Update SaldosMensualesCajaChica set saldo = @saldonuevo, updateTS = @updateTS where saldomensualID = @saldomensualID";
                        cmdInsert.CommandText = qryIns;
                        cmdInsert.Connection = conGaribay2;
                        cmdInsert.Parameters.Add("@saldonuevo", SqlDbType.Money).Value = nuevosaldo;
                        cmdInsert.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                        cmdInsert.Parameters.Add("@saldomensualID", SqlDbType.Int).Value = saldomensualID;
                        //Logger.Instance.LogMessage(Logger.typeLogMessage.DEBUG, Logger.typeUserActions.UPDATE, userID, "SALDO ANTERIOR: " + saldoanterior.ToString() + ", SALDO NUEVO: " + nuevosaldo.ToString() + ", saldomensualID: " + saldomensualID, "actualizaSaldoMeses function caja chica");
                        int registrosmodified = cmdInsert.ExecuteNonQuery();

                        if (registrosmodified != 1)
                        {
                            throw new Exception("AL INTENTAR ACTUALIZAR EL SALDO DE CAJA CHICA DE UN MES, NO SE ACTUALIZÓ EL NÚMERO DE REGISTROS ESPERADOS");
                        }
                    }


                }

            }
            catch (Exception exception)
            {
                sError = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, exception.Message, "EL ERROR SE DIO AL TRATAR DE ACTUALIZAR EL SALDO DE CAJA CHICA DE UN MES");

            }
            finally
            {
                conGaribay.Close();
                conGaribay2.Close();
            }
            return true;
        }

        /// <summary>
        /// delete a movement of banco
        /// </summary>
        /// <param name="idMovBanco">id to delete</param>
        /// <param name="sError">by ref string to set if there is an error</param>
        /// <param name="userID">the current user</param>
        /// <param name="cuentaID">cuenta ID</param>
        /// <returns></returns>
        /// 

        public static bool deleteMovementdeBanco(int idMovBanco, string sError, int userID, int cuentaID)
        {
            string qryIns;
            int month = 0, year = 0;
            double cantidad = 0f;
            qryIns = "Select fecha,cargo,abono from MovimientosCuentasBanco where movbanID = @idMovBanco ";
            SqlConnection slconGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, slconGaribay);
            slconGaribay.Open();
            try
            {
                cmdIns.Parameters.Add("@idMovBanco", SqlDbType.Int).Value = idMovBanco;
                SqlDataReader sqlReader = cmdIns.ExecuteReader();
                if (sqlReader.HasRows && sqlReader.Read())
                {
                    cantidad = double.Parse(sqlReader[1].ToString()) - double.Parse(sqlReader[2].ToString());
                    month = DateTime.Parse(Utils.converttoshortFormatfromdbFormat(sqlReader[0].ToString())).Month;
                    year = DateTime.Parse(Utils.converttoshortFormatfromdbFormat(sqlReader[0].ToString())).Year;
                }

                if (cuentaID == 14 && myConfig.GetBoolConfig("CHANGESIN5516", myConfig.CATEGORIA.MOVBANCOS, true) && new BasePage().UserID != 10)
                {
                    try
                    {
                        StringBuilder sBody = new StringBuilder();
                        sBody.Append("El Usuario " + new BasePage().CurrentUserName + " elimino movimiento en la cuenta 5516");
                        EMailUtils.SendTextEmail("cheliskis@gmail.com,mercedesdeandaorozco@hotmail.com", "modificacion en cuenta 5516",
                            sBody.ToString(), true);
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.LogException(Logger.typeUserActions.SELECT, "err notificando cambios en cuenta", ref ex);
                    }
                }



                qryIns = "Delete from MovimientosCuentasBanco where movbanID = @movbanID ";
                cmdIns.Parameters.Clear();
                cmdIns.Parameters.Add("@movbanID", SqlDbType.Int).Value = idMovBanco;
                cmdIns.CommandText = qryIns;
                int numregistros;
                conGaribay.Open();
                cmdIns.Connection = conGaribay;
                numregistros = cmdIns.ExecuteNonQuery();
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDEBANCO,Logger.typeUserActions.DELETE,userID,"ELIMINÓ EL MOVIMIENTO DE BANCO NÚMERO: " + idMovBanco.ToString());
                if (numregistros != 1)
                {
                    throw new Exception("AL TRATAR DE ELIMINAR EL MOVIMIENTO DE BANCO NÚMERO: " + idMovBanco.ToString() + ". LA BASE DE DATOS REGRESÓ QUE SE ALTERARAON " + numregistros.ToString() + " REGISTROS");
                }
                /*
                if (!actualizaSaldoMeses(cantidad, month, year, ref sError, userID, cuentaID))
                                {
                                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, "NO SE PUDO ACTUALIZAR EL SALDO DE MESES", "EL ERROR SE DIO AL ELIMINAR EL MOVIMIENTO NÚMERO: " + idMovBanco.ToString() + "EN LA FECHA: " + Utils.getNowFormattedDate());
                
                                }*/
                
                ////////delete on cascade the destino mov
                SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand comm = new SqlCommand();
                try
                {
                    conn.Open();
                    comm.Connection = conn;
                    comm.CommandText = "SELECT MovimientosCuentasBanco.movbanID, MovimientosCuentasBanco.cuentaID FROM MovimientoOrigen INNER JOIN MovimientosCuentasBanco ON MovimientoOrigen.movOrigenID = MovimientosCuentasBanco.movOrigenID WHERE     (MovimientoOrigen.movbanID = @movBanOrigenID)";
                    comm.Parameters.Add("@movBanOrigenID", SqlDbType.Int).Value = idMovBanco;
                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        //Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.UPDATE, new BasePage().UserID, "Se encontró un movimiento destino para borrar", "deleteMovementdeBanco");
                        int iMovToUpt = int.Parse(reader["movbanID"].ToString());
                        int cuentaIDtoDel = int.Parse(reader["cuentaID"].ToString());

                        if (!dbFunctions.deleteMovementdeBanco(iMovToUpt, sError, new BasePage().UserID, cuentaIDtoDel))
                        {
                            Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, new BasePage().UserID, "No se pudo eliminar cuentaID: " + cuentaIDtoDel + " movID: " + iMovToUpt, "deleteMovementdeBanco");
                        }
                    }
                    else
                    {
                        //CHECK IF THERE IS AT LEAST A MOV OF CAJA CHICA.
                        conn.Close();
                        conn.Open();
                        comm.Connection = conn;
                        comm.CommandText = "SELECT MovimientoOrigen.movbanID, MovimientosCaja.movimientoID, MovimientosCaja.bodegaID FROM MovimientoOrigen INNER JOIN MovimientosCaja ON MovimientoOrigen.movOrigenID = MovimientosCaja.movOrigenID WHERE     (MovimientoOrigen.movbanID = @movBanOrigenID)";
                        comm.Parameters.Clear();
                        comm.Parameters.Add("@movBanOrigenID", SqlDbType.Int).Value = idMovBanco;

                        //es caja chica
                        SqlDataReader readermov = comm.ExecuteReader();
                        if (readermov.HasRows && readermov.Read())
                        {
                            dbFunctions.deleteMovementdeCaja(int.Parse(readermov["movimientoID"].ToString()), ref sError, new BasePage().UserID);
                        }
                    }
                }
                catch (Exception exception)
                {
                    sError = exception.Message;
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, userID, exception.Message, "EL ERROR SE DIO AL ELIMINAR UN MOVIMIENTO DE BANCO");
                }
                finally
                {
                    conn.Close();
                }
            }
            catch (Exception exception)
            {
                sError = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, userID, exception.Message, "EL ERROR SE DIO AL ELIMINAR UN MOVIMIENTO DE BANCO");
                return false;

            }
            finally
            {
                conGaribay.Close();
                slconGaribay.Close();
            }



            return true;
        }

        public static bool deleteMovementdeCaja(int idMovCaja, ref string sError, int userID)
        {
            string qryIns;
            int month = 0, year = 0;
            double cantidad = 0f;
            qryIns = "Select fecha,cargo,abono from MovimientosCaja where movimientoID = @movimientoID ";
            SqlConnection slconGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, slconGaribay);
            slconGaribay.Open();
            try
            {
                cmdIns.Parameters.Add("@movimientoID", SqlDbType.Int).Value = idMovCaja;
                SqlDataReader sqlReader = cmdIns.ExecuteReader();
                if (sqlReader.HasRows && sqlReader.Read())
                {
                    cantidad = double.Parse(sqlReader[1].ToString()) - double.Parse(sqlReader[2].ToString());
                    month = DateTime.Parse(Utils.converttoshortFormatfromdbFormat(sqlReader[0].ToString())).Month;
                    year = DateTime.Parse(Utils.converttoshortFormatfromdbFormat(sqlReader[0].ToString())).Year;
                }
               

                ////////delete on cascade the destino mov
                SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand comm = new SqlCommand();
                try
                {
                    conn.Open();
                     //AQUI CHECAMOS SI ESTE MOVIMIENTO FUE EL CAUSANTE DE OTRO MOVIMIENTOS, ES DECIR SI FUE EL ORIGEN Y ELIMINAMOS EL MOVIMIENTO QUE PROVOCÓ
                    comm.Connection = conn;
                    comm.CommandText = "SELECT     MovimientosCuentasBanco.movbanID, MovimientosCuentasBanco.cuentaID, MovimientosCaja.movimientoID,  MovimientoOrigen.movimientoID AS movCajaChicaOrigen FROM         MovimientoOrigen LEFT OUTER JOIN MovimientosCaja ON MovimientoOrigen.movOrigenID = MovimientosCaja.movOrigenID LEFT OUTER JOIN MovimientosCuentasBanco ON MovimientoOrigen.movOrigenID = MovimientosCuentasBanco.movOrigenID WHERE     (MovimientoOrigen.movimientoID = @movCajaChicaOrigen)";
                    comm.Parameters.Add("@movCajaChicaOrigen", SqlDbType.Int).Value = idMovCaja;
                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        //Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.UPDATE, new BasePage().UserID, "Se encontró un movimiento destino para borrar", "deleteMovementdeCaja");

                        if (reader["movbanID"] != null && reader["movbanID"].ToString().Trim().Length > 0)
                        {
                            int iMovToUpt = int.Parse(reader["movbanID"].ToString());
                            int cuentaIDtoDel = int.Parse(reader["cuentaID"].ToString());

                             
                            if (!dbFunctions.deleteMovementdeBanco(iMovToUpt, sError, new BasePage().UserID, cuentaIDtoDel))
                            {
                                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, new BasePage().UserID, "No se pudo eliminar cuentaID: " + cuentaIDtoDel + " movID: " + iMovToUpt, "deleteMovementdeCaja");
                            }
                        }
                        if (reader["movimientoID"] != null && reader["movimientoID"].ToString().Trim().Length >0)
                        {
                            if (!dbFunctions.deleteMovementdeCaja(int.Parse(reader["movimientoID"].ToString()), ref sError, new BasePage().UserID))
                            {
                                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, new BasePage().UserID, "No se pudo eliminar :" + reader["movimientoID"].ToString(), "deleteMovementdeCaja");
                            }
                        }
                        
                    }
                    reader.Close();
                    comm.CommandText = "DELETE FROM MovimientoOrigen WHERE (movimientoID = @movimientoID)";
                    comm.Parameters.Clear();
                    comm.Parameters.Add("@movimientoID", SqlDbType.Int).Value = idMovCaja;
                    
                    comm.ExecuteNonQuery();
                    conn.Close();
                    conn.Open();


                    //AQUI CHECAMOS SI ESTE MOVIMIENTO FUE CAUSADO POR OTRO Y LO ELIMINAMOS (ES DECIR, ELIMINAMOS EL ORIGEN).
                    //VEMOS SI ESTE MOVIMIENTO TIENE ORIGEN
                    comm.CommandText = "Select movOrigenID from MovimientosCaja where movimientoID = @movimientoID ";
                    comm.Parameters.Clear();
                    comm.Parameters.Add("@movimientoID", SqlDbType.Int).Value = idMovCaja;
                    if (comm.ExecuteScalar() != null && comm.ExecuteScalar().ToString().Length>0)
                    {
                        int movOrigenID = int.Parse(comm.ExecuteScalar().ToString());
                        // SI LO TIENE LO BUSCAMOS EN LA TABLA DE ORIGEN Y ELIMINAMOS
                        comm.CommandText = "SELECT     MovimientoOrigen.movbanID, MovimientoOrigen.movimientoID, MovimientosCuentasBanco.cuentaID ";
                        comm.CommandText+= " FROM  MovimientoOrigen LEFT OUTER JOIN  MovimientosCuentasBanco ON MovimientoOrigen.movbanID = MovimientosCuentasBanco.movbanID ";
                        comm.CommandText +=  " WHERE     (MovimientoOrigen.movOrigenID = @movOrigenID)";
                        comm.Parameters.Add("@movOrigenID", SqlDbType.Int).Value = movOrigenID;
                        SqlDataReader rdSacaMov = comm.ExecuteReader();
                        if (rdSacaMov.HasRows && rdSacaMov.Read())
                        {
                            //Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.UPDATE, new BasePage().UserID, "Se encontró un movimiento destino para borrar", "deleteMovementdeCaja");

                            if (rdSacaMov["movbanID"] != null && rdSacaMov["movbanID"].ToString().Trim().Length > 0)
                            {

                                int iMovToUpt = int.Parse(rdSacaMov["movbanID"].ToString());
                                int cuentaIDtoDel = int.Parse(rdSacaMov["cuentaID"].ToString());
                                rdSacaMov.Close();
                                comm.CommandText = "DELETE FROM MovimientoOrigen WHERE (movOrigenID = @movOrigenID)";
                                comm.Parameters.Clear();
                                comm.Parameters.Add("@movOrigenID", SqlDbType.Int).Value = movOrigenID;

                                comm.ExecuteNonQuery();


                                if (!dbFunctions.deleteMovementdeBanco(iMovToUpt, sError, new BasePage().UserID, cuentaIDtoDel))
                                {
                                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, new BasePage().UserID, "No se pudo eliminar cuentaID: " + cuentaIDtoDel + " movID: " + iMovToUpt, "deleteMovementdeCaja");
                                }
                            }
                            else
                            {

                                if (rdSacaMov["movimientoID"] != null && rdSacaMov["movimientoID"].ToString().Trim().Length > 0)
                                {
                                    int movID = int.Parse(rdSacaMov["movimientoID"].ToString());
                                    rdSacaMov.Close();
                                    comm.CommandText = "DELETE FROM MovimientoOrigen WHERE (movOrigenID = @movOrigenID)";
                                    comm.Parameters.Clear();
                                    comm.Parameters.Add("@movOrigenID", SqlDbType.Int).Value = movOrigenID;
                                    comm.ExecuteNonQuery();

                                    if (!dbFunctions.deleteMovementdeCaja(movID, ref sError, new BasePage().UserID))
                                    {
                                        Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, new BasePage().UserID, "No se pudo eliminar :" + reader["movimientoID"].ToString(), "deleteMovementdeCaja");
                                    }
                                }
                            }

                        }

                       

                    }
                  
                    
                }
                catch (Exception exception)
                {
                    sError = exception.Message;
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.DELETE, userID, exception.Message, "EL ERROR SE DIO AL ELIMINAR UN MOVIMIENTO DE BANCO");
                }
                finally
                {
                    conn.Close();
                }
                qryIns = "Delete from MovimientosCaja where movimientoID = @movimientoID ";
                cmdIns.Parameters.Clear();
                cmdIns.Parameters.Add("@movimientoID", SqlDbType.Int).Value = idMovCaja;
                cmdIns.CommandText = qryIns;
                int numregistros;
                conGaribay.Open();
                cmdIns.Connection = conGaribay;
                numregistros = cmdIns.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception("AL TRATAR DE ELIMINAR EL MOVIMIENTO DE CAJA CHICA NÚMERO: " + idMovCaja.ToString() + ". LA BASE DE DATOS REGRESÓ QUE SE ALTERARAON " + numregistros.ToString() + " REGISTROS");
                }


                if (!actualizaSaldoMesesCajaChica(cantidad, month, year, ref sError, userID))
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, "NO SE PUDO ACTUALIZAR EL SALDO DE MESES", "EL ERROR SE DIO AL ELIMINAR EL MOVIMIENTO NÚMERO: " + idMovCaja.ToString() + "EN LA FECHA: " + Utils.getNowFormattedDate());

                }
            }
            catch (Exception exception)
            {
                sError = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, exception.Message, "EL ERROR SE DIO AL ELIMINAR UN MOVIMIENTO DE BANCO");
                return false;

            }
            finally
            {
                conGaribay.Close();
                slconGaribay.Close();
            }



            return true;
        }


        public static bool updateMovementdeBanco(ref dsMovBanco.dtMovBancoRow dtrowupdate, int idMovBanco, ref string sError, int userID, int cuentaID)
        {
            string qryUp;
            int month = 0, year = 0;
            double cantidad = 0f, cantidadup = 0f;
            qryUp = "Select fecha,abono,cargo from MovimientosCuentasBanco where movbanID = @idMovBanco ";
            SqlConnection sqlconGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdUp = new SqlCommand(qryUp, sqlconGaribay);
            sqlconGaribay.Open();
            try
            {
                cmdUp.Parameters.Add("@idMovBanco", SqlDbType.Int).Value = idMovBanco;
                SqlDataReader sqlReader = cmdUp.ExecuteReader();
                if (sqlReader.HasRows && sqlReader.Read())
                {
                    cantidad = double.Parse(sqlReader[1].ToString()) - double.Parse(sqlReader[2].ToString());
                    cantidadup = double.Parse(dtrowupdate.abono.ToString()) - double.Parse(dtrowupdate.cargo.ToString());
                    cantidadup = (cantidad * -1) + cantidadup;
                    month = DateTime.Parse(Utils.converttoshortFormatfromdbFormat(sqlReader[0].ToString())).Month;
                    year = DateTime.Parse(Utils.converttoshortFormatfromdbFormat(sqlReader[0].ToString())).Year;
                }
                qryUp = "UPDATE MovimientosCuentasBanco SET cuentaID = @cuentaID, ConceptoMovCuentaID = @ConceptoMovCuentaID, fecha = @fecha, cargo = @cargo, abono = @abono, userID = @userID, updateTS = @updateTS, nombre=@nombre, facturaOlarguillo=@facturaOlarguillo, numCabezas=@numCabezas, numCheque=@numCheque, chequeNombre=@chequeNombre, chequecobrado=@chequecobrado, fechacobrado=@fechacobrado, catalogoMovBancoFiscalID = @catMovBancoFiscalID, subCatalogoMovBancoFiscalID = @subcatMovBancoFiscalID, catalogoMovBancoInternoID = @catMovBancoInternoID, subCatalogoMovBancoInternoID = @subcatMovBancoInternoID WHERE movbanID = @movbanID ";
              //  qryIns = "INSERT INTO MovimientosCuentasBanco (cuentaID, ConceptoMovCuentaID, fecha, cargo, abono, userID, nombre, facturaOlarguillo, numCabezas, numCheque, chequeNombre) VALUES (@cuentaID,@ConceptoMovCuentaID,@fecha,@cargo,@abono,@userID,@nombre,@facturaOlarguillo,@numCabezas,@numCheque,@chequeNombre);";
                
                cmdUp.Parameters.Clear();
                cmdUp.Parameters.Add("@movbanID", SqlDbType.Int).Value = idMovBanco;
                cmdUp.Parameters.Add("@cuentaID", SqlDbType.Int).Value = cuentaID;
                cmdUp.Parameters.Add("@ConceptoMovCuentaID", SqlDbType.VarChar).Value = dtrowupdate.conceptoID;
                cmdUp.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtrowupdate.fecha;
                cmdUp.Parameters.Add("@cargo", SqlDbType.Money).Value = dtrowupdate.cargo;
                cmdUp.Parameters.Add("@abono", SqlDbType.Money).Value = dtrowupdate.abono;
                cmdUp.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                cmdUp.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = dtrowupdate.nombre;
                cmdUp.Parameters.Add("@facturaOlarguillo", SqlDbType.NVarChar).Value = dtrowupdate.facturaOlarguillo;
                cmdUp.Parameters.Add("@numCabezas", SqlDbType.Int).Value = dtrowupdate.numCabezas;
                cmdUp.Parameters.Add("@numCheque", SqlDbType.Int).Value = dtrowupdate.numCheque;
                cmdUp.Parameters.Add("@chequeNombre", SqlDbType.NVarChar).Value = dtrowupdate.chequeNombre;
                cmdUp.Parameters.Add("@chequecobrado", SqlDbType.Bit).Value = dtrowupdate.chequecobrado==true ? 1: 0;
                cmdUp.Parameters.Add("@fechacobrado", SqlDbType.DateTime).Value = dtrowupdate.fecha; //= dtrowupdate.chequecobrado == true ? dtrowupdate.fechachequedecobro : dtrowupdate.fecha;
                cmdUp.Parameters.Add("@catMovBancoFiscalID", SqlDbType.Int).Value = dtrowupdate.catalogoMovBancoFiscalID;
                cmdUp.Parameters.Add("@subcatMovBancoFiscalID", SqlDbType.Int).Value = dtrowupdate.subCatalogoMovBancoFiscalID;
                cmdUp.Parameters.Add("@catMovBancoInternoID", SqlDbType.Int).Value = dtrowupdate.catalogoMovBancoInternoID;
                cmdUp.Parameters.Add("@subcatMovBancoInternoID", SqlDbType.Int).Value = dtrowupdate.subCatalogoMovBancoInternoID;
                cmdUp.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = dtrowupdate.updateTS; 
                cmdUp.CommandText = qryUp;
                int numregistros;
                conGaribay.Open();
                cmdUp.Connection = conGaribay;
                numregistros = cmdUp.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception("AL TRATAR DE MODIFICAR EL MOVIMIENTO DE BANCO NÚMERO: " + idMovBanco.ToString() + ". LA BASE DE DATOS REGRESÓ QUE SE ALTERARAON " + numregistros.ToString() + " REGISTROS");
                }
                if (!actualizaSaldoMeses(cantidadup, month, year, ref sError, userID, cuentaID))
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, userID, "NO SE PUDO ACTUALIZAR EL SALDO DE MESES", "EL ERROR SE DIO AL MODIFICAR EL MOVIMIENTO NÚMERO: " + idMovBanco.ToString() + "EN LA FECHA: " + Utils.getNowFormattedDate());

                }
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDEBANCO, Logger.typeUserActions.UPDATE, userID, "MODIFICÓ EL MOVIMIENTO DE BANCO NÚMERO: " + idMovBanco.ToString() + " EN LA FECHA: " + Utils.getNowFormattedDate());

            }
            catch (Exception exception)
            {
                sError = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, userID, exception.Message, "EL ERROR SE DIO AL MODIFICAR UN MOVIMIENTO DE BANCO");
                return false;
            }
            finally
            {
                conGaribay.Close();
                sqlconGaribay.Close();
            }
            return true;
        }

        //funciones de entrada y existencias de productos

        public static bool addEntradaPro(int productoID, int bodegaID, int tipoMovProdID, String fecha, float cantidad, String observaciones, int userID, int NDCDetalleID, int cicloID, ref String sError)
        {

            string sqlQuery = "INSERT INTO [EntradaDeProductos] (productoID,bodegaID,tipoMovProdID,cicloID,Fecha,cantidad,observaciones,userID,NDCDetalleID,storeTS,updateTS) VALUES (@productoID,@bodegaID,@tipoMovProdID,@cicloID,@Fecha,@cantidad,@observaciones,@userID,@NDCDetalleID,@storeTS,@updateTS)";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(sqlQuery, conGaribay);

            try
            {


                cmdIns.Parameters.Add("@productoID", SqlDbType.Int).Value = productoID;
                cmdIns.Parameters.Add("@bodegaID", SqlDbType.Int).Value = bodegaID;
                cmdIns.Parameters.Add("@tipoMovProdID", SqlDbType.Int).Value = tipoMovProdID;
                cmdIns.Parameters.Add("@cicloID", SqlDbType.Int).Value = cicloID;
                cmdIns.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = fecha;
                cmdIns.Parameters.Add("@cantidad", SqlDbType.Float).Value = cantidad;
                cmdIns.Parameters.Add("@observaciones", SqlDbType.NVarChar).Value = observaciones;
                cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                cmdIns.Parameters.Add("@NDCdetalleID", SqlDbType.Int).Value = NDCDetalleID;
                cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                conGaribay.Open();

                int numregistros;
                numregistros = cmdIns.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception("AL TRATAR DE INSERTAR UNA ENTRADA DE PRODUCTO. LA BASE DE DATOS REGRESÓ QUE SE ALTERARAON " + numregistros.ToString() + " REGISTROS");
                }






            }
            catch (Exception exception)
            {
                //sError = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, exception.Message, "EL ERROR SE DIÓ AL INGRESAR UNA ENTRADA DE PRODUCTO");
                return false;

            }

            finally
            {
                conGaribay.Close();

            }
            return true;

        }
        public static bool updateEntradaPro(int entradaID, int tipoMovProdID, float cantidad, float precio, string observaciones, ref string Serror, int bodegaID, int productoID, int cicloID, int userID, float cantidadant)
        {
            string sqlQuery = "UPDATE EntradaDeProductos SET tipoMovProdID = @tipoMovProdID, cantidad = @cantidad, Observaciones = @Observaciones, updateTS=@updateTS, precio=@precio  WHERE entradaprodID = @entradaprodID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(sqlQuery, conGaribay);

            try
            {

                cmdIns.Parameters.Add("@tipoMovProdID", SqlDbType.Int).Value = entradaID;
                cmdIns.Parameters.Add("@cantidad", SqlDbType.Float).Value = cantidad;
                cmdIns.Parameters.Add("@Observaciones", SqlDbType.Text).Value = observaciones;
                cmdIns.Parameters.Add("@precio", SqlDbType.Float).Value = precio;
                cmdIns.Parameters.Add("@entradaprodID", SqlDbType.Int).Value = tipoMovProdID;
                string dtNow = Utils.getNowFormattedDate();
                cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = dtNow;
                conGaribay.Open();
                int numregistros;
                numregistros = cmdIns.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception("AL TRATAR DE MODIFICAR UNA ENTRADA DE PRODUCTO. LA BASE DE DATOS REGRESÓ QUE SE ALTERARAON " + numregistros.ToString() + " REGISTROS");
                }
                else //INSERTAMOS LA DIFERENCIA
                {
                    if (!upExist(bodegaID, productoID, cantidad - cantidadant, cicloID, ref Serror))
                    {
                        return false;
                    }
                }



            }
            catch (Exception exception)
            {
                Serror = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, exception.Message, "EL ERROR SE DIÓ AL MODIFICAR UNA ENTRADA DE PRODUCTO");
                return false;

            }
            finally
            {
                conGaribay.Close();

            }
            return true;



        }
        public static bool deleteLiquidacion(int liqID, int userID, ref string sError)
        {
            bool sepudoeliminar = true;
            //CHECAMOS QUE NO ESTE COBRADA
            SqlConnection conEstaCobrada = new SqlConnection(myConfig.ConnectionInfo);
            SqlConnection conDeleteRelaciones = new SqlConnection(myConfig.ConnectionInfo);
            SqlConnection conBorraLiquidacion = new SqlConnection(myConfig.ConnectionInfo);
            conEstaCobrada.Open();
            conDeleteRelaciones.Open();
            conBorraLiquidacion.Open();

            try
            {
                //CHECAMOS QUE NO ESTE COBRADA
                string sqlCobrada = "Select cobrada from Liquidaciones where LiquidacionID = @liqID";
                SqlCommand cmdSelectCobrada = new SqlCommand(sqlCobrada, conEstaCobrada);
                cmdSelectCobrada.Parameters.Clear();
                cmdSelectCobrada.Parameters.Add("@liqID", SqlDbType.Int).Value = liqID;
                if (bool.Parse(cmdSelectCobrada.ExecuteScalar().ToString()))
                { //YA ESTA COBRADA
                    throw new Exception("LA LIQUIDACION NO SE PUEDE ELIMINAR UNA VEZ COBRADA");

                }
                //BORRRAMOS RELACIONES
                string sqlborrarelaciones = "delete from Liquidaciones_Anticipos where LiquidacionID = @liqID; ";
                sqlborrarelaciones += "delete from Liquidaciones_Boletas where LiquidacionID = @liqID; ";
                sqlborrarelaciones += "delete from PagosLiquidacion where liquidacionID = @liqID";
              
                SqlCommand cmdBorrarelaciones = new SqlCommand(sqlborrarelaciones, conDeleteRelaciones);
                cmdBorrarelaciones.Parameters.Clear();
                cmdBorrarelaciones.Parameters.Add("@liqID", SqlDbType.Int).Value = liqID;
                cmdBorrarelaciones.ExecuteNonQuery();

                //BORRAMOS LIQ

                string borraliq = "Delete from Liquidaciones where LiquidacionID = @liqID ";
                SqlCommand cmdBorraLiq = new SqlCommand(borraliq, conBorraLiquidacion);
                cmdBorraLiq.Parameters.Clear();
                cmdBorraLiq.Parameters.Add("@liqID", SqlDbType.Int).Value = liqID;
                cmdBorraLiq.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                sError = ex.Message;
                sepudoeliminar = false;

            }
            finally {
                conEstaCobrada.Close();
                conDeleteRelaciones.Close();
                conBorraLiquidacion.Close();
            }
                
            return sepudoeliminar;
        }
        public static bool deleteEntradaPro(int entradaID, int userID, ref string sError)
        {
            string qrystring; float cantidad = -1; int bodegaID = -1, productoID = -1, cicloID = -1;
            qrystring = "select cantidad, bodegaid, productoID, cicloID from EntradaDeProductos where entradaprodID = @entradaID ";
            SqlConnection conGaribaysel = new SqlConnection(myConfig.ConnectionInfo);
            SqlConnection conGaribaydel = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdSel = new SqlCommand(qrystring, conGaribaysel);

            try
            {
                cmdSel.Parameters.Add("@entradaID", SqlDbType.Int).Value = entradaID;
                conGaribaysel.Open();
                SqlDataReader reader = cmdSel.ExecuteReader();
                if (!reader.HasRows) { return false; }
                while (reader.Read())
                {
                    cantidad = float.Parse(reader[0].ToString());
                    bodegaID = int.Parse(reader[1].ToString());
                    productoID = int.Parse(reader[2].ToString());
                    cicloID = int.Parse(reader[3].ToString());
                }
                /*
                if (!upExist(bodegaID, productoID, cantidad * -1, cicloID, ref sError))
                                {
                                    return false;
                                }*/

                conGaribaydel.Open();
                qrystring = "delete from EntradaDeProductos where entradaprodID = @entradaID ";
                SqlCommand cmddel = new SqlCommand(qrystring, conGaribaydel);
                cmddel.Parameters.Add("@entradaID", SqlDbType.Int).Value = entradaID;
                int numregistros = cmddel.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception("AL TRATAR DE ELIMINAR UNA ENTRADA DE PRODUCTO. LA BASE DE DATOS REGRESÓ QUE SE ALTERARAON " + numregistros.ToString() + " REGISTROS");
                }

            }
            catch (Exception ex)
            {
                sError = ex.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, userID, ex.Message, "EL ERROR SE DIÓ AL ELIMINAR UNA ENTRADA DE PRODUCTO");
                return false;

            }
            finally
            {
                conGaribaydel.Close();
                conGaribaysel.Close();
                
            }


            return true;

        }

        public static bool addEntradaPro(int productoID, int bodegaID, int tipoMovProdID, String fecha, double cantidad, double precio, String observaciones, int userID, int cicloID, ref String sError, out int ID)
        {
            string sqlQuery = "INSERT INTO [EntradaDeProductos] (productoID,bodegaID,tipoMovProdID,cicloID,Fecha,cantidad,observaciones,userID,storeTS,updateTS, preciocompra) VALUES (@productoID,@bodegaID,@tipoMovProdID,@cicloID,@Fecha,@cantidad,@observaciones,@userID,@storeTS,@updateTS, @precio); select ID = SCOPE_IDENTITY();";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(sqlQuery, conGaribay);
            ID = -1;
            try
            {


                cmdIns.Parameters.Add("@productoID", SqlDbType.Int).Value = productoID;
                cmdIns.Parameters.Add("@bodegaID", SqlDbType.Int).Value = bodegaID;
                cmdIns.Parameters.Add("@tipoMovProdID", SqlDbType.Int).Value = tipoMovProdID;
                cmdIns.Parameters.Add("@cicloID", SqlDbType.Int).Value = cicloID;
                cmdIns.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = fecha;
                cmdIns.Parameters.Add("@cantidad", SqlDbType.Float).Value = cantidad;
                cmdIns.Parameters.Add("@observaciones", SqlDbType.NVarChar).Value = observaciones;
                cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                cmdIns.Parameters.Add("@precio", SqlDbType.Float).Value = precio;
                conGaribay.Open();
                int numregistros;
                numregistros = int.Parse(cmdIns.ExecuteScalar().ToString());
                if (numregistros <= 0)
                {
                    throw new Exception("AL TRATAR DE INSERTAR UNA ENTRADA DE PRODUCTO. LA BASE DE DATOS REGRESÓ QUE SE ALTERARAON " + numregistros.ToString() + " REGISTROS");
                }
                ID = numregistros;
                /*
                if(!dbFunctions.upExist(bodegaID,productoID,cantidad, cicloID,ref sError)){
                                    return false;
                
                                }*/



            }
            catch (Exception exception)
            {
                sError = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, exception.Message, "EL ERROR SE DIÓ AL INGRESAR UNA ENTRADA DE PRODUCTO");
                return false;

            }
            finally
            {
                conGaribay.Close();

            }
            return true;
        }
        public static bool addEntradaPro(int productoID, int bodegaID, int tipoMovProdID, String fecha, double cantidad, double precio, String observaciones, int userID, int cicloID, ref String sError)
        {

            string sqlQuery = "INSERT INTO [EntradaDeProductos] (productoID,bodegaID,tipoMovProdID,cicloID,Fecha,cantidad,observaciones,userID,storeTS,updateTS, preciocompra) VALUES (@productoID,@bodegaID,@tipoMovProdID,@cicloID,@Fecha,@cantidad,@observaciones,@userID,@storeTS,@updateTS, @precio)";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(sqlQuery, conGaribay);

            try
            {


                cmdIns.Parameters.Add("@productoID", SqlDbType.Int).Value = productoID;
                cmdIns.Parameters.Add("@bodegaID", SqlDbType.Int).Value = bodegaID;
                cmdIns.Parameters.Add("@tipoMovProdID", SqlDbType.Int).Value = tipoMovProdID;
                cmdIns.Parameters.Add("@cicloID", SqlDbType.Int).Value = cicloID;
                cmdIns.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = fecha;
                cmdIns.Parameters.Add("@cantidad", SqlDbType.Float).Value = cantidad;
                cmdIns.Parameters.Add("@observaciones", SqlDbType.NVarChar).Value = observaciones;
                cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                cmdIns.Parameters.Add("@precio", SqlDbType.Float).Value = precio;
                conGaribay.Open();
                int numregistros;
                numregistros = cmdIns.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception("AL TRATAR DE INSERTAR UNA ENTRADA DE PRODUCTO. LA BASE DE DATOS REGRESÓ QUE SE ALTERARAON " + numregistros.ToString() + " REGISTROS");
                }
                /*
                if(!dbFunctions.upExist(bodegaID,productoID,cantidad, cicloID,ref sError)){
                                    return false;
                
                                }*/



            }
            catch (Exception exception)
            {
                sError = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, exception.Message, "EL ERROR SE DIÓ AL INGRESAR UNA ENTRADA DE PRODUCTO");
                return false;

            }
            finally
            {
                conGaribay.Close();

            }
            return true;

        }



        public static bool addOrden_EntradaPro(int ordenID, int productoID, double cantidad, double precio,int sacos, bool tieneBoletas, int entradaProductoID, int boletaID)
        {

            string sqlQuery = "INSERT INTO Orden_de_entrada_detalle (ordenID, productoID, cantidad, precio, importe, sacos, tieneBoletas, entradaProductoID, boletaID) ";
            sqlQuery += " VALUES     (@ordenID, @productoID, @cantidad, @precio, @importe, @sacos, @tieneBoletas, @entradaProductoID, @boletaID)";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(sqlQuery, conGaribay);

            try
            {
                cmdIns.Parameters.Add("@ordenID", SqlDbType.Int).Value = ordenID;
                cmdIns.Parameters.Add("@productoID", SqlDbType.Int).Value = productoID;
                cmdIns.Parameters.Add("@cantidad", SqlDbType.Float).Value = cantidad;
                cmdIns.Parameters.Add("@precio", SqlDbType.Float).Value = precio;
                cmdIns.Parameters.Add("@importe", SqlDbType.Float).Value = precio*cantidad;
                cmdIns.Parameters.Add("@sacos", SqlDbType.Float).Value = sacos;
                cmdIns.Parameters.Add("@tieneBoletas", SqlDbType.Bit).Value = tieneBoletas;
                cmdIns.Parameters.Add("@entradaProductoID", SqlDbType.Int).Value = entradaProductoID;
                cmdIns.Parameters.Add("@boletaID", SqlDbType.Int).Value = boletaID;
                
                conGaribay.Open();
                int numregistros;
                numregistros = cmdIns.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception("AL TRATAR DE INSERTAR UNA ORDEN ENTRADA DE PRODUCTO. LA BASE DE DATOS REGRESÓ QUE SE ALTERARAON " + numregistros.ToString() + " REGISTROS");
                }
                


            }
            catch (Exception exception)
            {
                string sError = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT,-1, exception.Message, "EL ERROR SE DIÓ AL INGRESAR UNA ENTRADA DE PRODUCTO");
                return false;

            }
            finally
            {
                conGaribay.Close();

            }
            return true;

        }



        public static bool addEntradaPro(ref dsNCdetalle.dtNCdetalleDataTable dtNCdetalle, ref string sError, int cicloID, string FechaNota, int userID)
        {
            string query = "INSERT INTO [EntradaDeProductos] (productoID,sacos,bodegaID,tipoMovProdID,cicloID,Fecha,cantidad,observaciones,userID,storeTS,updateTS, preciocompra) values(@prodID, @sacos,@bodID, @tipoMov, @cicloID, @Fecha, @cantidad, @obser, @userID, @storeTS, @updateTS, @precio); SELECT NewID = SCOPE_IDENTITY();  ";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(query, conGaribay);

            try
            {
                DataTableReader reader = dtNCdetalle.CreateDataReader();
                int i = 0;
                conGaribay.Open();
                while (reader.Read())
                {
                    //@prodID, @bodID, @tipoMov, @cicloID, @Fecha, @cantidad, @obser, @userID, @storeTS, @updateTS; SELECT NewID = SCOPE_IDENTITY();  ";
                    if (float.Parse(reader["Cantidad"].ToString()) > 0)
                    {
                        cmdIns.Parameters.Clear();
                        cmdIns.Parameters.Add("@prodID", SqlDbType.Int).Value = int.Parse(reader["productoID"].ToString());
                        cmdIns.Parameters.Add("@sacos", SqlDbType.Float).Value = float.Parse(reader["Sacos"].ToString());
                        cmdIns.Parameters.Add("@bodID", SqlDbType.Int).Value = int.Parse(reader["bodegaID"].ToString());
                        cmdIns.Parameters.Add("@tipoMov", SqlDbType.Int).Value = 2;
                        cmdIns.Parameters.Add("@cicloID", SqlDbType.Int).Value = cicloID;
                        cmdIns.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = FechaNota;
                        cmdIns.Parameters.Add("@cantidad", SqlDbType.Float).Value = float.Parse(reader["Cantidad"].ToString());
                        cmdIns.Parameters.Add("@obser", SqlDbType.NVarChar).Value = "SE AGREGÓ ESTA ENTRADA DE PRODUCTO POR UNA INSERCIÓN DE NOTA DE COMPRA";
                        cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                        cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                        cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                        cmdIns.Parameters.Add("@precio", SqlDbType.Float).Value = float.Parse(reader["Precio Unitario"].ToString());
                        int entradaID = -1;
                        entradaID = int.Parse(cmdIns.ExecuteScalar().ToString());
                        if (entradaID == -1)
                        {
                            throw new Exception("AL TRATAR DE INSERTAR UNA ENTRADA DE PRODUCTO DEBIDO A UNA NOTA DE VENTA HUBO UN PROBLEMA. ");
                        }
                        dsNCdetalle.dtNCdetalleRow row = (dsNCdetalle.dtNCdetalleRow)(dtNCdetalle.Rows[i]);
                        row.entradaprodID = entradaID;
                    }
                    i++;

                }
                conGaribay.Close();


            }
            catch (Exception exception)
            {
                sError = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, exception.Message, "EL ERROR SE DIÓ AL INGRESAR UNA ENTRADA DE PRODUCTO");
                return false;

            }
            finally
            {
                conGaribay.Close();

            }
            return true;



        }



        public static bool upExist(int bodegaID, int productoID, float cantidad, int cicloID, ref String sError)
        {

            string sqlQuery = "UPDATE [Existencias] SET cantidad =cantidad + @cantidad  WHERE bodegaID = @bodegaID and productoID = @productoID and cicloID = @cicloID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(sqlQuery, conGaribay);

            try
            {
                cmdIns.Parameters.Add("@cantidad", SqlDbType.Float).Value = cantidad;
                cmdIns.Parameters.Add("@bodegaID", SqlDbType.Int).Value = bodegaID;
                cmdIns.Parameters.Add("@productoID", SqlDbType.Int).Value = productoID;
                cmdIns.Parameters.Add("@cicloID", SqlDbType.Int).Value = cicloID;


                conGaribay.Open();
                int rowsafected = cmdIns.ExecuteNonQuery();
                if (rowsafected != 1)
                {
                    throw new Exception("AL TRATAR DE MODIFICAR LA EXISTENCIA DE UN PRODUCTO. LA BASE DE DATOS REGRESÓ QUE SE ALTERARAON " + rowsafected.ToString() + " REGISTROS");
                }
            }
            catch (Exception exception)
            {
                sError = exception.Message;
                //Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userid, exception.Message, "EL ERROR SE DIÓ AL TRATAR DE MODIFICAR LA EXISTENCIA DE UN PRODUCTO");
                return false;

            }
            finally
            {
                conGaribay.Close();
            }
            return true;

        }

        public static bool updateMovementdeCajaChica(ref dsMovCajaChica.dtMovCajaChicaRow dtrowupdate, int idMovCaja, ref string sError, int userID)
        {
            string qryUp;
            int month = 0, year = 0;
            double cantidad = 0f, cantidadup = 0f;
            qryUp = "Select fecha,abono,cargo from MovimientosCaja where movimientoID = @movimientoID ";
            SqlConnection sqlconGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdUp = new SqlCommand(qryUp, sqlconGaribay);
            sqlconGaribay.Open();
            try
            {
                cmdUp.Parameters.Add("@movimientoID", SqlDbType.Int).Value = idMovCaja;
                SqlDataReader sqlReader = cmdUp.ExecuteReader();
                if (sqlReader.HasRows && sqlReader.Read())
                {
                    cantidad = double.Parse(sqlReader[1].ToString()) - double.Parse(sqlReader[2].ToString()); //CANTIDAD ANTERIOR
                    cantidadup = double.Parse(dtrowupdate.abono.ToString()) - double.Parse(dtrowupdate.cargo.ToString());
                    cantidadup = (cantidad * -1) + cantidadup;
                    month = DateTime.Parse(Utils.converttoshortFormatfromdbFormat(sqlReader[0].ToString())).Month;
                    year = DateTime.Parse(Utils.converttoshortFormatfromdbFormat(sqlReader[0].ToString())).Year;
                }
                qryUp = "UPDATE MovimientosCaja set nombre = @nombre, fecha = @fecha, cargo = @cargo, abono = @abono, observaciones = @observaciones, updateTS = @updateTS, userID = @userID, catalogoMovBancoID = @catalogoMovBancoID, subCatalogoMovBancoID = @subCatalogoMovBancoID, facturaOlarguillo = @facturaOlarguillo , numCabezas = @numCabezas  WHERE movimientoID = @movID ";
                cmdUp.Parameters.Clear();
                cmdUp.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = dtrowupdate.nombre;
                cmdUp.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtrowupdate.fecha;
                cmdUp.Parameters.Add("@cargo", SqlDbType.Money).Value = dtrowupdate.cargo;
                cmdUp.Parameters.Add("@abono", SqlDbType.Money).Value = dtrowupdate.abono;
                cmdUp.Parameters.Add("@observaciones", SqlDbType.Text).Value = dtrowupdate.Observaciones;
                cmdUp.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                cmdUp.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                cmdUp.Parameters.Add("@movID", SqlDbType.Int).Value = idMovCaja;
                cmdUp.Parameters.Add("@catalogoMovBancoID", SqlDbType.Int).Value = dtrowupdate.catalogoMovBancoInternoID;
                cmdUp.Parameters.Add("@subCatalogoMovBancoID", SqlDbType.Int).Value = dtrowupdate.IssubCatalogoMovBancoInternoIDNull()? -1: dtrowupdate.subCatalogoMovBancoInternoID;
                cmdUp.Parameters.Add("@facturaOlarguillo", SqlDbType.VarChar).Value = dtrowupdate.facturaOlarguillo;
                cmdUp.Parameters.Add("@numCabezas", SqlDbType.Float).Value = (float)dtrowupdate.numCabezas;

                cmdUp.CommandText = qryUp;
                int numregistros;
                conGaribay.Open();
                cmdUp.Connection = conGaribay;
                numregistros = cmdUp.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception("AL TRATAR DE MODIFICAR EL MOVIMIENTO DE CAJA CHICA NÚMERO: " + idMovCaja.ToString() + ". LA BASE DE DATOS REGRESÓ QUE SE ALTERARAON " + numregistros.ToString() + " REGISTROS");
                }
                if (!actualizaSaldoMesesCajaChica(cantidadup, month, year, ref sError, userID))
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, userID, "NO SE PUDO ACTUALIZAR EL SALDO DE MESES", "EL ERROR SE DIO AL MODIFICAR EL MOVIMIENTO NÚMERO: " + idMovCaja.ToString() + "EN LA FECHA: " + Utils.getNowFormattedDate());

                }
            }
            catch (Exception exception)
            {
                sError = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.UPDATE, userID, exception.Message, "EL ERROR SE DIO AL MODIFICAR UN MOVIMIENTO DE BANCO");
                return false;
            }
            finally
            {
                conGaribay.Close();
                sqlconGaribay.Close();
            }
            return true;
        }
        
        public static bool insertCheque(int userID, string sChequeNum, DateTime dtFecha, float dMonto, string sNombre, string sNombreRecibe, int movBancoID)
        {
            bool result = false;
            SqlConnection sqlConn = new SqlConnection();
            try
            {
                string sQuery = "insert Cheques(chequenumero,monto,nombre,nombrequienrecibe, movBancoID) values(@chequenumero,@monto,@nombre,@nombrequienrecibe, @movBancoID);";
                SqlCommand sqlComm = new SqlCommand(sQuery);
                sqlComm.Parameters.Add("@chequenumero", SqlDbType.NVarChar).Value = sChequeNum;
                sqlComm.Parameters.Add("@monto", SqlDbType.Float).Value = dMonto;
                sqlComm.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = sNombre;
                sqlComm.Parameters.Add("@nombrequienrecibe", SqlDbType.NVarChar).Value = sNombreRecibe;
                sqlComm.Parameters.Add("movBancoID", SqlDbType.Int).Value = movBancoID;
                sqlConn.ConnectionString = myConfig.ConnectionInfo;
                sqlComm.Connection = sqlConn;
                sqlConn.Open();
                if (int.Parse(sqlComm.ExecuteNonQuery().ToString()) == 1)
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CHEQUES, Logger.typeUserActions.INSERT, userID, string.Format("AGREGÓ EL CHEQUE NUMERO : {0} POR EL MONTO {1:c}", sChequeNum, dMonto));
                    result = true;
                }
                else
                {
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CHEQUES, Logger.typeUserActions.INSERT, userID, string.Format("EL CHEQUE NUMERO : {0} POR EL MONTO {1:c} NO PUDO SER AGREGADO", sChequeNum, dMonto));
                }

            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, string.Format("EL CHEQUE NUMERO : {0} POR EL MONTO {1:c} NO PUDO SER AGREGADO. Ex: {2}", sChequeNum, dMonto, ex.Message), "insertCheque");
            }
            finally
            {
                sqlConn.Close();
            }
            return result;
        }

        #region Funciones salida de producto

        public static int insertSalidaDeProducto(int iProductoID, int iBodegaID, int iTipoMovProdID, DateTime dtFecha, double fCantidad,
           String sObservaciones, int iCicloID, double fSacos, int iUserID)
        {
            int iNewID = -1;
            SqlConnection sqlConn = new SqlConnection();
            try
            {
                sqlConn.ConnectionString = myConfig.ConnectionInfo;
                sqlConn.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConn;
                sqlCommand.CommandText = "insert into SalidaDeProductos(cicloID, productoID,bodegaID,tipoMovProdID,fecha,cantidad,observaciones, userID, sacos, storeTS, updateTS)";
                sqlCommand.CommandText += "values(@cicloID, @productoID,@bodegaID,@tipoMovProdID,@fecha,@cantidad,@observaciones, @userID, @sacos, @storeTS, @updateTS);";
                sqlCommand.CommandText += "SELECT NewID = SCOPE_IDENTITY();";
                sqlCommand.Parameters.Add("@productoID", SqlDbType.Int).Value = iProductoID;
                sqlCommand.Parameters.Add("@bodegaID", SqlDbType.Int).Value = iBodegaID;
                sqlCommand.Parameters.Add("@tipoMovProdID", SqlDbType.Int).Value = iTipoMovProdID;
                sqlCommand.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtFecha;
                sqlCommand.Parameters.Add("@cantidad", SqlDbType.Float).Value = fCantidad;
                sqlCommand.Parameters.Add("@observaciones", SqlDbType.NVarChar).Value = sObservaciones;
                sqlCommand.Parameters.Add("@userID", SqlDbType.Int).Value = iUserID;
                sqlCommand.Parameters.Add("@sacos", SqlDbType.Float).Value = fSacos;
                sqlCommand.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.Now;
                sqlCommand.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.Now;
                sqlCommand.Parameters.Add("@cicloID", SqlDbType.Int).Value = 4;



                //  Logger.Instance.LogMessage(Logger.typeLogMessage.DEBUG, Logger.typeUserActions.INSERT, iUserID, string.Format("SACANDO {0} CANTIDAD DE LA ENTRADA {1}", fCantidad,), "insertSalidaDeProducto");
                iNewID = int.Parse(sqlCommand.ExecuteScalar().ToString());
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SALIDADEPRODUCTOS, Logger.typeUserActions.INSERT, iUserID, "AGREGÓ SALIDA DE PRODUCTO CON ID: " + iNewID.ToString());
                //  Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.INSERT, iUserID, string.Format("AGREGANDO SALIDA, {0} CANTIDAD DE LA ENTRADA {1}", fCantidad, iEntradaProdID), "insertSalidaDeProducto");
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, iUserID, "ERROR AL INTENTAR AGREGAR SALIDA", "insertSalidaDeProducto");
            }
            finally
            {
                sqlConn.Close();
            }
            return iNewID;
        }

        public static bool updateSalidaDeProducto(int salidadeproductoID, int iProductoID, int iBodegaID, int iTipoMovProdID, DateTime dtFecha, double fCantidad,
          String sObservaciones, int iCicloID, double fSacos, int iUserID)
        {
            bool exito = true;
            SqlConnection sqlConn = new SqlConnection();
            try
            {
                sqlConn.ConnectionString = myConfig.ConnectionInfo;
                sqlConn.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConn;
                sqlCommand.CommandText = "update SalidaDeProductos set productoID = @productoID, bodegaID = @bodegaID, tipoMovProdID = @tipoMovProdID, fecha = @fecha,cantidad = @cantidad,observaciones = @observaciones, userID = @userID, sacos = @sacos, updateTS = @updateTS where salidaprodID = @salidaprodID";
                sqlCommand.Parameters.Add("@productoID", SqlDbType.Int).Value = iProductoID;
                sqlCommand.Parameters.Add("@bodegaID", SqlDbType.Int).Value = iBodegaID;
                sqlCommand.Parameters.Add("@tipoMovProdID", SqlDbType.Int).Value = iTipoMovProdID;
                sqlCommand.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtFecha;
                sqlCommand.Parameters.Add("@cantidad", SqlDbType.Float).Value = fCantidad;
                sqlCommand.Parameters.Add("@observaciones", SqlDbType.NVarChar).Value = sObservaciones;
                sqlCommand.Parameters.Add("@userID", SqlDbType.Int).Value = iUserID;
                sqlCommand.Parameters.Add("@sacos", SqlDbType.Float).Value = fSacos;
                sqlCommand.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.Now;
                sqlCommand.Parameters.Add("@salidaprodID", SqlDbType.Int).Value = salidadeproductoID;
                //  Logger.Instance.LogMessage(Logger.typeLogMessage.DEBUG, Logger.typeUserActions.INSERT, iUserID, string.Format("SACANDO {0} CANTIDAD DE LA ENTRADA {1}", fCantidad,), "insertSalidaDeProducto");
                sqlCommand.ExecuteNonQuery();
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SALIDADEPRODUCTOS, Logger.typeUserActions.INSERT, iUserID, "MODIFICO SALIDA DE PRODUCTO CON ID: " + salidadeproductoID.ToString());
                //  Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.INSERT, iUserID, string.Format("AGREGANDO SALIDA, {0} CANTIDAD DE LA ENTRADA {1}", fCantidad, iEntradaProdID), "insertSalidaDeProducto");

            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, iUserID, "ERROR AL INTENTAR MODIFICAR SALIDA", "insertSalidaDeProducto");
                exito = false;
            }
            finally
            {
                sqlConn.Close();
            }
            return exito;
        }
        public static bool rellenaEstadodeCuenta (ref dsEstadodeCuenta.dtEstadodeCuentaDataTable dtEstadodeCuentaTable, int creditoID, DateTime fechaactual)
        {
            bool serelleno = true;
            string sql = "SELECT  Creditos.Interesanual,    Notasdeventa.Folio, Notasdeventa.Total, Notasdeventa.Fecha FROM Notasdeventa INNER JOIN Creditos ON Notasdeventa.creditoID = Creditos.creditoID  where creditoID = @creditoID and fecha <= @fechaactual order by NotasdeVenta.fecha DESC, NotasdeVenta.monto";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdSacaNotas = new SqlCommand(sql, conGaribay);
            conGaribay.Open();
            try{
                cmdSacaNotas.Parameters.Add("@creditoID", SqlDbType.Int).Value = creditoID;
                cmdSacaNotas.Parameters.Add("@fechaactual", SqlDbType.DateTime).Value = fechaactual;
                SqlDataReader rdNotas= cmdSacaNotas.ExecuteReader();
                while(rdNotas.Read())
                {
                    //CALCULAMOS EL INTERES DE LA NOTA 
                    
                    DateTime fechacorte;
                    int diasdecalculo = 0;
                    double saldodenota = 0, tasadeinteres = 0, interesgenerado=0;
                    double.TryParse(rdNotas["Total"].ToString(), out saldodenota);
                    double.TryParse(rdNotas["Interesanual"].ToString(), out tasadeinteres);

                    DateTime fechainicio = DateTime.Parse(rdNotas["Fecha"].ToString());

                    if(fechainicio.Month==12)
                    {
                      fechacorte = new DateTime(fechainicio.Year+1,1,1);  //SACAMOS LA PRIMER FECHA DE CORTE
                   
                    }
                    else
                    {
                      fechacorte = new DateTime(fechainicio.Year,fechainicio.Month+1,1);  //SACAMOS LA PRIMER FECHA DE CORTE
                    }
                    if (fechacorte > fechaactual)
                    {
                        fechacorte = fechaactual;

                    }
                    diasdecalculo = fechacorte.Subtract(fechainicio).Days;
                    interesgenerado = (tasadeinteres / 1200) * saldodenota;
                    saldodenota += interesgenerado;             
                    
                    while(fechaactual>fechacorte){

                    } 
                    
                }
            }
            catch(Exception ex){

            }
            finally
            {
                conGaribay.Close();
            }
            return serelleno;
        }
        public static bool insertSalidaDeProducto(ref dsNV.dtNVdetalleDataTable dtNVdetalle, ref string sError, int cicloID, string FechaNota, int userID)
        {
            string query = "insert SalidaDeProductos(tipoMovProdID,Fecha,cantidad,observaciones,entradaprodID,userID) values(@tipoMovProdID,@Fecha,@cantidad,@observaciones,@entradaprodID,@userID); SELECT NewID = SCOPE_IDENTITY();  ";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(query, conGaribay);

            try
            {
                DataTableReader reader = dtNVdetalle.CreateDataReader();
                int i = 0;
                conGaribay.Open();
                while (reader.Read())
                {
                    //@prodID, @bodID, @tipoMov, @cicloID, @Fecha, @cantidad, @obser, @userID, @storeTS, @updateTS; SELECT NewID = SCOPE_IDENTITY();  ";
                    if (float.Parse(reader["Cantidad"].ToString()) > 0)
                    {
                        cmdIns.Parameters.Clear();
                        cmdIns.Parameters.Add("@tipoMovProdID", SqlDbType.Int).Value = 3;
                        cmdIns.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = FechaNota;
                        cmdIns.Parameters.Add("@cantidad", SqlDbType.Float).Value = float.Parse(reader["Cantidad"].ToString());
                        cmdIns.Parameters.Add("@obser", SqlDbType.NVarChar).Value = "SE AGREGÓ ESTA SALIDA DE PRODUCTO POR UNA INSERCIÓN DE NOTA DE VENTA";
                        int entradaID = -1;
                        entradaID = int.Parse(cmdIns.ExecuteScalar().ToString());
                        if (entradaID == -1)
                        {
                            throw new Exception("AL TRATAR DE INSERTAR UNA ENTRADA DE PRODUCTO DEBIDO A UNA NOTA DE VENTA HUBO UN PROBLEMA. ");
                        }
                        dsNCdetalle.dtNCdetalleRow row = (dsNCdetalle.dtNCdetalleRow)(dtNVdetalle.Rows[i]);
                        row.entradaprodID = entradaID;
                    }
                    i++;

                }
                conGaribay.Close();


            }
            catch (Exception exception)
            {
                sError = exception.Message;
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, exception.Message, "EL ERROR SE DIÓ AL INGRESAR UNA ENTRADA DE PRODUCTO");
                return false;

            }
            finally
            {
                conGaribay.Close();

            }
            return true;
        }
        #endregion

        public static bool insertsol(string fecha, int creditoID, int productorID, int experiencia, int userID, ref int solID, string ejido)
        {
            string sqlQuery;
            sqlQuery = "insert into Solicitudes (fecha,creditoID, productorID, Experiencia, Monto, Superficieasembrar,Plazo, RecursosPropios, Descripciondegarantias, userID, Valordegarantias, ejido) ";
            sqlQuery += "values (@fecha, @creditoID,@productorID,@Experiencia,@Monto,@Superficieasembrar,@Plazo,@RecursosPropios,@Descripcion,@userID,@Valor, @ejido);";
            sqlQuery += "SELECT NewID = SCOPE_IDENTITY();";
            if (new BasePage().IsSistemBanco)
            {
                sqlQuery = dbFunctions.UpdateSDSForSisBanco(sqlQuery);

                SqlConnection ConGaribay = new SqlConnection();
                SqlCommand cmdins = new SqlCommand();

                try
                {
                    ConGaribay.ConnectionString = myConfig.ConnectionInfo;
                    cmdins.CommandText = sqlQuery;

                    ConGaribay.Open();
                    cmdins.Connection = ConGaribay;

                    cmdins.Parameters.Add("@fecha", SqlDbType.DateTime).Value = Utils.converttoshortFormatfromdbFormat(fecha);
                    cmdins.Parameters.Add("@creditoID", SqlDbType.Int).Value = creditoID;
                    cmdins.Parameters.Add("@productorID", SqlDbType.Int).Value = productorID;
                    cmdins.Parameters.Add("@Experiencia", SqlDbType.Int).Value = experiencia;
                    cmdins.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                    cmdins.Parameters.Add("@Monto", SqlDbType.Float).Value = 0.00;
                    cmdins.Parameters.Add("@Superficieasembrar", SqlDbType.Float).Value = 0.00;
                    cmdins.Parameters.Add("@Plazo", SqlDbType.Float).Value = 0.00;
                    cmdins.Parameters.Add("@RecursosPropios", SqlDbType.Float).Value = 0.00;
                    cmdins.Parameters.Add("@Descripcion", SqlDbType.Text).Value = "";
                    cmdins.Parameters.Add("@Valor", SqlDbType.Float).Value = 0.00;
                    cmdins.Parameters.Add("@ejido", SqlDbType.NVarChar).Value = ejido;
                    solID = int.Parse(cmdins.ExecuteScalar().ToString());
                    Logger.Instance.LogUserSessionRecord(Logger.typeModulo.SOLICITUDES, Logger.typeUserActions.INSERT, userID, "INSERTÓ LA SOLICITUD NÚMERO: " + solID.ToString());

                }
                catch (SqlException exc)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.INSERT, "NO SE PUDO INSERTAR UNA SOLICITUD", exc);
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, "NO SE PUDO INSERTAR UNA SOLICITUD", "insertarSolicitud");
                    return false;
                }
                finally
                {
                    ConGaribay.Close();
                }
            }
            else
            {
                try
                {
                    Solicitudes sol = new Solicitudes();
                    sol.Fecha = DateTime.Parse(fecha);
                    sol.CreditoID = creditoID;
                    sol.ProductorID = productorID;
                    sol.Experiencia = experiencia;
                    sol.UserID = userID;
                    sol.Valordegarantias = sol.RecursosPropios = sol.Superficieasembrar = sol.Monto = 0.00;
                    sol.Plazo = 0;
                    sol.Descripciondegarantias = "";
                    sol.Ejido = ejido;
                    sol.CostoTotalSeguro = sol.HectAseguradas = sol.SuperficieFinanciada = 0.0;
                    sol.StoreTS = sol.UpdateTS = Utils.Now;
                    sol.Insert();
                    solID = (int)sol.SolicitudID;
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(Logger.typeUserActions.INSERT, "err inserting sol", ex);
                    return false;
                }
            }

            return true;

        }

        public static bool inicializadtNota(ref dsNCdetalle.dtNCdetalleDataTable dtNCdetalle, int bodegaID)
        {
            if (dtNCdetalle == null)
            {
                return false;
            }
            try
            {
                string sSql = "SELECT     Productos.productoID, Productos.Nombre, Bodegas.bodegaID, Bodegas.bodega, productoGrupos.grupo, Productos.productoGrupoID, Presentaciones.presentacionID, ";
                sSql += " Presentaciones.Presentacion, Unidades.unidadID, Unidades.Unidad, Existencias.cantidad,  ";
                sSql += " FROM         Existencias INNER JOIN ";
                sSql += " Productos ON Existencias.productoID = Productos.productoID INNER JOIN ";
                sSql += " Bodegas ON Existencias.bodegaID = Bodegas.bodegaID INNER JOIN ";
                sSql += " productoGrupos ON Productos.productoGrupoID = productoGrupos.grupoID INNER JOIN ";
                sSql += " Unidades ON Productos.unidadID = Unidades.unidadID INNER JOIN ";
                sSql += " Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID ";
                sSql += " WHERE     (Bodegas.bodegaID = @bodegaID) ";
                SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand sqlComm = new SqlCommand(sSql, sqlConn);
                sqlConn.Open();
                sqlComm.Parameters.Add("@bodegaID", SqlDbType.Int).Value = bodegaID;
                SqlDataReader sqlReader = sqlComm.ExecuteReader();
                if (!sqlReader.HasRows)
                {
                    sqlConn.Close();
                    return false;
                }
                while (sqlReader.Read())
                {

                    decimal x;
                    x = 0.00M;
                    //                      "SELECT     Productos.productoID, Productos.Nombre, Bodegas.bodegaID, Bodegas.bodega, Productos.productTypeID, productoTypes.productoType,";
                    //                      sSql += " productoTypes.grupoID AS Expr1, productoGrupos.grupo, Presentaciones.presentacionID, Presentaciones.Presentacion, Unidades.unidadID, Unidades.Unidad";
                   // dtNCdetalle.AdddtNCdetalleRow(-1, -1, int.Parse(sqlReader["productoID"].ToString()), bodegaID, x, x, x, -1, sqlReader["Nombre"].ToString(), sqlReader["bodega"].ToString(), sqlReader["grupo"].ToString(), int.Parse(sqlReader["productoGrupoID"].ToString()), sqlReader["Presentacion"].ToString(), int.Parse(sqlReader["presentacionID"].ToString()), 0.00M, sqlReader["Unidad"].ToString(), int.Parse(sqlReader["unidadID"].ToString()),double.Parse(sqlReader["cantidad"].ToString()), (Boolean)sqlReader["tieneBoletas"]);

                }

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;


        }
        public static double sacaExistenciadeproducto(int productoID, int bodegaID)
        {
            double existencia = 0;
            SqlConnection conSacaEx = new SqlConnection(myConfig.ConnectionInfo);
            string sql = "SELECT Existencia FROM ExistenciasView WHERE (bodegaID = @bodID) AND (productoID = @productoID)";
            SqlCommand cmdSacaEx = new SqlCommand(sql, conSacaEx);
            conSacaEx.Open();
            try
            {
                cmdSacaEx.Parameters.Clear();
                cmdSacaEx.Parameters.Add("@productoID", SqlDbType.Int).Value = productoID;
                cmdSacaEx.Parameters.Add("@bodID", SqlDbType.Int).Value = bodegaID;
                //cmdSacaEx.Parameters.Add("@cicloID", SqlDbType.Int).Value = cicloID;
                existencia = cmdSacaEx.ExecuteScalar()!=null ? double.Parse(cmdSacaEx.ExecuteScalar().ToString()) : 0;


            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, "ERROR AL SACAR EXISTENCIA DE PRODUCTO", "URL", ref ex);
            }
            finally
            {
                conSacaEx.Close();
            }
            return existencia;
        }
        public static bool inicializadtNotadeVenta(ref dsNV.dtNVdetalleDataTable dtNVdetalle, int bodegaID)
        {
            if (dtNVdetalle == null)
            {
                return false;
            }
            try
            {
                string sSql = "SELECT     Productos.productoID, Productos.Nombre, Bodegas.bodegaID, Bodegas.bodega, Productos.productTypeID, productoTypes.productoType,";
                sSql += " productoTypes.grupoID AS Expr1, productoGrupos.grupo, Presentaciones.presentacionID, Presentaciones.Presentacion, Unidades.unidadID, Unidades.Unidad, Existencias.cantidad";
                sSql += " FROM         Existencias INNER JOIN";
                sSql += " Productos ON Existencias.productoID = Productos.productoID INNER JOIN";
                sSql += " Bodegas ON Existencias.bodegaID = Bodegas.bodegaID INNER JOIN";
                sSql += " productoTypes ON Productos.productTypeID = productoTypes.productTypeID INNER JOIN";
                sSql += " productoGrupos ON productoTypes.grupoID = productoGrupos.grupoID INNER JOIN";
                sSql += " Unidades ON Productos.unidadID = Unidades.unidadID INNER JOIN ";
                sSql += " Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID where Bodegas.bodegaID = @bodegaID";
                SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand sqlComm = new SqlCommand(sSql, sqlConn);
                sqlConn.Open();
                sqlComm.Parameters.Add("@bodegaID", SqlDbType.Int).Value = bodegaID;
                SqlDataReader sqlReader = sqlComm.ExecuteReader();
                if (!sqlReader.HasRows)
                {
                    sqlConn.Close();
                    return false;
                }
                while (sqlReader.Read())
                {

                    double x;
                    x = 0.00;
                    //                      "SELECT     Productos.productoID, Productos.Nombre, Bodegas.bodegaID, Bodegas.bodega, Productos.productTypeID, productoTypes.productoType,";
                    //                      sSql += " productoTypes.grupoID AS Expr1, productoGrupos.grupo, Presentaciones.presentacionID, Presentaciones.Presentacion, Unidades.unidadID, Unidades.Unidad";
                    dtNVdetalle.AdddtNVdetalleRow(-1, -1, int.Parse(sqlReader[0].ToString()), bodegaID, x, x, x, -1, sqlReader[1].ToString(), sqlReader[3].ToString(), sqlReader[7].ToString(), int.Parse(sqlReader[6].ToString()), sqlReader[9].ToString(), int.Parse(sqlReader[8].ToString()), sqlReader[5].ToString(), int.Parse(sqlReader[4].ToString()), 0.00, sqlReader[11].ToString(), int.Parse(sqlReader[10].ToString()), double.Parse(sqlReader[12].ToString()), -1, -1, Utils.Now, 0, false);

                }

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;


        }
        public static string UpdateSDSForSisBanco(string sSelectCmd)
        {
            if (new BasePage().IsSistemBanco)
            {
                sSelectCmd = sSelectCmd.ToUpper();
                sSelectCmd = sSelectCmd.Replace(" CREDITOS ", " CREDITOSSISBANCOS ");
                sSelectCmd = sSelectCmd.Replace("CREDITOS.", " CREDITOSSISBANCOS.");
                sSelectCmd = sSelectCmd.Replace(" SOLICITUDES ", " SOLICITUDESSISBANCOS ");
                sSelectCmd = sSelectCmd.Replace(".SOLICITUDES", ".SOLICITUDESSISBANCOS");
                sSelectCmd = sSelectCmd.Replace("SOLICITUDES.", "SOLICITUDESSISBANCOS.");

                sSelectCmd = sSelectCmd.Replace(" SOLICITUD_SEGUROAGRICOLA ", " SOLICITUDESSISBANCOS_SEGUROAGRICOLA ");
                sSelectCmd = sSelectCmd.Replace(".SOLICITUD_SEGUROAGRICOLA", ".SOLICITUDESSISBANCOS_SEGUROAGRICOLA");
                sSelectCmd = sSelectCmd.Replace("SOLICITUD_SEGUROAGRICOLA.", "SOLICITUDESSISBANCOS_SEGUROAGRICOLA.");
                sSelectCmd = sSelectCmd.Replace(" SEGUROSAGRICOLASPREDIOS ", " SEGUROSAGRICOLASPREDIOSSISBANCOS ");
                sSelectCmd = sSelectCmd.Replace("SEGUROSAGRICOLASPREDIOS.", "SEGUROSAGRICOLASPREDIOSSISBANCOS.");
                sSelectCmd = sSelectCmd.Replace("NOTASDEVENTA.", "NOTASDEVENTA_SISBANCO.");
                sSelectCmd = sSelectCmd.Replace("NOTASDEVENTA_DETALLE.", "NOTASDEVENTA_DETALLE_SISBANCO.");
                sSelectCmd = sSelectCmd.Replace(" NOTASDEVENTA ", " NOTASDEVENTA_SISBANCO ");
                sSelectCmd = sSelectCmd.Replace(" NOTASDEVENTA_DETALLE ", " NOTASDEVENTA_DETALLE_SISBANCO ");
                sSelectCmd = sSelectCmd.Replace("VDNVFERTILIZANTES.", "VDNVFERTILIZANTES_SISBANCO.");
                sSelectCmd = sSelectCmd.Replace("VDNVNOFERTILIZANTE.", "VDNVNOFERTILIZANTE_SISBANCO.");
                sSelectCmd = sSelectCmd.Replace(" VDNVFERTILIZANTES ", " VDNVFERTILIZANTES_SISBANCO ");
                sSelectCmd = sSelectCmd.Replace(" VDNVNOFERTILIZANTE ", " VDNVNOFERTILIZANTE_SISBANCO ");
                sSelectCmd = sSelectCmd.Replace("[VDNVFERTILIZANTES].", "[VDNVFERTILIZANTES_SISBANCO].");
                sSelectCmd = sSelectCmd.Replace("[VDNVNOFERTILIZANTE].", "[VDNVNOFERTILIZANTE_SISBANCO].");
                sSelectCmd = sSelectCmd.Replace(" [VDNVFERTILIZANTES] ", " [VDNVFERTILIZANTES_SISBANCO] ");
                sSelectCmd = sSelectCmd.Replace(" [VDNVNOFERTILIZANTE] ", " [VDNVNOFERTILIZANTE_SISBANCO] ");
                sSelectCmd = sSelectCmd.Replace(" SEGUROSAGRICOLAS ", " SEGUROSAGRICOLAS ");
                sSelectCmd = sSelectCmd.Replace("DBO.CREDITOS", "DBO.CREDITOSSISBANCOS");
                sSelectCmd = sSelectCmd.Replace("PAGOS_NOTAVENTA", "PAGOS_NOTAVENTA_SISBANCOS");
            }
            
            return sSelectCmd;
        }
        public static int insertaCredito(int productorID, int cicloID, DateTime Fecha, int Zona, Double interesAnual, int userID, int statusID, double LimitedeCredito)
        {
            int maximo = -1;
            string sqlQuery = "INSERT INTO Creditos (productorID, cicloID, Fecha, Zona, FechaFinCiclo, InteresAnual, userID, storeTS, updateTS, statusID, LimitedeCredito, finCalculoIntereses) VALUES (@productorID,@cicloID,@Fecha,@Zona,@FechaFinCiclo,@InteresAnual,@userID,@storeTS,@updateTS,@statusID, @limitedecredito, @fechaFinCiclo)";
            if (new BasePage().IsSistemBanco)
            {
                sqlQuery = dbFunctions.UpdateSDSForSisBanco(sqlQuery);
            }
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(sqlQuery, conGaribay);

            try
            {


                cmdIns.Parameters.Add("@productorID", SqlDbType.Int).Value = productorID;
                cmdIns.Parameters.Add("@cicloID", SqlDbType.NVarChar).Value = cicloID;
                cmdIns.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = Fecha;
                cmdIns.Parameters.Add("@Zona", SqlDbType.Int).Value = Zona;
                SqlConnection conGaribaytemp = new SqlConnection(myConfig.ConnectionInfo);
                string sqlQuerysel = "";
                if (Zona == 0)
                    sqlQuerysel = "Select FechaFinZona1 from Ciclos where cicloID = @cicloID";
                else
                    sqlQuerysel = "Select FechaFinZona2 from Ciclos where cicloID = @cicloID";
                SqlCommand cmdSel = new SqlCommand(sqlQuerysel, conGaribaytemp);
                cmdSel.Parameters.Add("@cicloID", SqlDbType.Int).Value = cicloID;
                conGaribaytemp.Open();
                string fecha = cmdSel.ExecuteScalar().ToString();
                cmdIns.Parameters.Add("@FechaFinCiclo", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(fecha);
                conGaribaytemp.Close();
                cmdIns.Parameters.Add("@InteresAnual", SqlDbType.Float).Value = interesAnual / 100;
                cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
                cmdIns.Parameters.Add("@statusID", SqlDbType.Int).Value = statusID;
                cmdIns.Parameters.Add("@limitedecredito", SqlDbType.Int).Value = LimitedeCredito;
                conGaribay.Open();
                int numregistros;
                numregistros = cmdIns.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception(string.Format(myConfig.StrFromMessages("CREDITOEXECUTEFAILED"), "AGREGADO", "AGREGARON", numregistros.ToString()));
                }
                sqlQuery = "SELECT max(creditoID) FROM Creditos ";
                if (new BasePage().IsSistemBanco)
                {
                    sqlQuery = dbFunctions.UpdateSDSForSisBanco(sqlQuery);
                }
                cmdIns.Parameters.Clear();
                cmdIns.CommandText = sqlQuery;
                maximo = (int)cmdIns.ExecuteScalar();
                Logger.Instance.LogUserSessionRecord(Logger.typeModulo.CREDITOS, Logger.typeUserActions.INSERT, userID, ("AGREGÓ EL CREDITO: " + maximo.ToString()));

            }
            catch (Exception exception)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, exception.Message, "dbfunctions->insetCredito");


            }
            finally
            {

                conGaribay.Close();

            }
            return maximo;
        }

        public static bool insertNotadeCompra(ref dsNCdetalle.dtNCdetalleDataTable dtNCdetalle, ref dsNCdatos.dtNCdatosRow dtRowdatos, ref string sError)
        {
            if (dtRowdatos == null || dtNCdetalle == null) { return false; }
            addEntradaPro(ref dtNCdetalle, ref sError, int.Parse(dtRowdatos.cicloID.ToString()), Utils.converttoLongDBFormat(dtRowdatos.fecha.ToString()), int.Parse(dtRowdatos.userID.ToString()));
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            string query = "insert into NotasdeCompra(proveedorID, cicloID, fecha, folio, total, subtotal, iva, observaciones, userID, tipopagoID, fechapago) ";
            query += " values(@proveedorID, @cicloID,@fecha, @folio, @total,@subtotal, @iva, @observaciones, @userID, @tipopagoID, @fechapago);";
            query += " declare @lastid INT; set @lastid = SCOPE_IDENTITY(); insert into NotasdeCompra_detalle(notadecompraID,productoID,bodegaID, cantidad, preciodecompra,importe,entradaprodID,sacos) ";
            DataTableReader reader = dtNCdetalle.CreateDataReader();
            int i = 0;
            while (reader.Read())
            {
                i++;
                if (float.Parse(reader["Cantidad"].ToString()) > 0)
                {
                    query += " SELECT @lastid,  ";
                    query += reader["productoID"].ToString();
                    query += " ,";
                    query += reader["bodegaID"].ToString();
                    query += " ,";
                    query += reader["Cantidad"].ToString();
                    query += " ,";
                    query += reader["Precio Unitario"].ToString();
                    query += " ,";
                    query += reader["Importe"].ToString();
                    query += " ,";
                    query += reader["entradaprodID"].ToString();
                    query += " ,";
                    query += reader["Sacos"].ToString();
                    query += " UNION ALL ";
                }

            }
            query = query.Remove(query.Length - 10);
            query += " select newID = @lastid";
            SqlCommand cmd = new SqlCommand(query, conGaribay);

            //SELECT @lastid,1,1.1 UNION ALL SELECT @lastid,2,2.1 UNION ALL SELECT @lastid,3,3.1
            try
            {
                conGaribay.Open();
                cmd.Parameters.Add("@proveedorID", SqlDbType.Int).Value = int.Parse(dtRowdatos.proveedorID.ToString());
                cmd.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(dtRowdatos.cicloID.ToString());
                cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = dtRowdatos.fecha.ToString();
                cmd.Parameters.Add("@folio", SqlDbType.NVarChar).Value = dtRowdatos.folio.ToString();
                cmd.Parameters.Add("@total", SqlDbType.Money).Value = double.Parse(dtRowdatos.total.ToString());
                cmd.Parameters.Add("@subtotal", SqlDbType.Money).Value = double.Parse(dtRowdatos.subtotal.ToString());
                cmd.Parameters.Add("@iva", SqlDbType.Money).Value = double.Parse(dtRowdatos.iva.ToString());
                cmd.Parameters.Add("@userID", SqlDbType.Int).Value = int.Parse(dtRowdatos.userID.ToString());
                cmd.Parameters.Add("@observaciones", SqlDbType.NVarChar).Value = dtRowdatos.observaciones.ToString();
                cmd.Parameters.Add("@tipopagoID", SqlDbType.Int).Value = int.Parse(dtRowdatos.tipodePagoID.ToString());
                cmd.Parameters.Add("@fechapago", SqlDbType.DateTime).Value = dtRowdatos.fechapago.ToString();
                //                  cmd.Parameters.Add("@storeTS",SqlDbType.DateTime).Value=Utils.getNowFormattedDate();
                //                  cmd.Parameters.Add("@updateTS",SqlDbType.DateTime).Value=Utils.getNowFormattedDate();
                int notaID = int.Parse(cmd.ExecuteScalar().ToString());
                dtRowdatos.notadecompraID = notaID;
            }
            catch (Exception exc)
            {
                sError = exc.Message;
                return false;
            }
            finally
            {
                conGaribay.Close();
            }

            return true;
        }
        public static bool deleteNotadeCompra(int notaid, ref string Serror)
        {
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            //string query = "delete from EntradaDeProductos where entradaprodid in (select EntradaDeProductos.entradaprodid from EntradaDeProductos INNER JOIN NotasdeCompra_detalle ON NotasdeCompra_detalle.entradaprodID = EntradaDeProductos.entradaprodID where  NotasdeCompra_detalle.notadecompraID = @notadecompraID); delete from NotasdeCompra where notadecompraID = @notadecompraID ";
            //borramos detalle
            string query = " delete from NotasDeCompra where notadecompraID =@notadecompraID;";
            SqlCommand delcmd = new SqlCommand(query, conGaribay);
            try
            {
                conGaribay.Open();
                delcmd.Parameters.Add("@notadecompraID", SqlDbType.Int).Value = notaid;
                delcmd.ExecuteNonQuery();

            }
            catch (Exception exc)
            {
                Serror = exc.Message;
                return false;
            }
            finally
            {
                conGaribay.Close();
            }
            return true;
        }
        public static bool deleteNotadeVenta(int notaid, ref string Serror)
        {
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            //string query = "delete from EntradaDeProductos where entradaprodid in (select EntradaDeProductos.entradaprodid from EntradaDeProductos INNER JOIN NotasdeCompra_detalle ON NotasdeCompra_detalle.entradaprodID = EntradaDeProductos.entradaprodID where  NotasdeCompra_detalle.notadecompraID = @notadecompraID); delete from NotasdeCompra where notadecompraID = @notadecompraID ";
            //borramos detalle
            string query = " delete from Notasdeventa where notadeventaID =@notadeventaID";
            if (new BasePage().IsSistemBanco)
            {
                query = dbFunctions.UpdateSDSForSisBanco(query);
            }
            SqlCommand delcmd = new SqlCommand(query, conGaribay);
            try
            {
                conGaribay.Open();
                delcmd.Parameters.Add("@notadeventaID", SqlDbType.Int).Value = notaid;
                delcmd.ExecuteNonQuery();

            }
            catch (Exception exc)
            {
                Serror = exc.Message;
                return false;
            }
            finally
            {
                conGaribay.Close();
            }
            return true;
        }
        

        public static bool insertNotadeVenta(ref dsNV.dtNVdetalleDataTable dtNVdetalle, ref dsNV.dtNVdatosRow dtRowdatos, ref string sError)
        {
            if (dtRowdatos == null || dtNVdetalle == null) { return false; }
            insertSalidaDeProducto(ref dtNVdetalle, ref sError, int.Parse(dtRowdatos.cicloID.ToString()), Utils.converttoLongDBFormat(dtRowdatos.fecha.ToString()), int.Parse(dtRowdatos.userID.ToString()));
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            string query = "insert into NotasdeVenta(productorID, cicloID, userID, tipopagoID, Folio, fechainicobrointereses, Fecha, Pagada, Total, Subtotal, Iva, creditoID, FechadePago, Interes, Observaciones) ";
            query += " values(@productorID, @cicloID,@userID, @tipopagoID, @Folio,@Fechaingreso, @fechainicobrointereses, @Fecha, @Pagada, @Total, @Subtotal, @Iva, @creditoID, @FechadePago, @Interes, @Observaciones);";
            query += " declare @lastid INT; set @lastid = SCOPE_IDENTITY(); insert into NotasdeVenta_detalle(notadeventaID,productoID,bodegaID, cantidad, precio,importe,salidaproductoID,sacos) ";
            DataTableReader reader = dtNVdetalle.CreateDataReader();
            int i = 0;
            while (reader.Read())
            {
                i++;
                if (float.Parse(reader["Cantidad"].ToString()) > 0)
                {
                    query += " SELECT @lastid,  ";
                    query += reader["productoID"].ToString();
                    query += " ,";
                    query += reader["bodegaID"].ToString();
                    query += " ,";
                    query += reader["Cantidad"].ToString();
                    query += " ,";
                    query += reader["Precio Unitario"].ToString();
                    query += " ,";
                    query += reader["Importe"].ToString();
                    query += " ,";
                    query += reader["salidaprodID"].ToString();
                    query += " ,";
                    query += reader["Sacos"].ToString();
                    query += " UNION ALL ";
                }

            }
            query = query.Remove(query.Length - 10);
            query += " select newID = @lastid";
            SqlCommand cmd = new SqlCommand(query, conGaribay);

            //   @productorID, @cicloID,@userID, @tipopagoID, @Folio, @fechainicobrointereses, @Fecha, @Pagada, @Total, @Subtotal, @Iva, @creditoID, @FechadePago, @Interes, @Observaciones);
            try
            {
                conGaribay.Open();
                cmd.Parameters.Add("@productorID", SqlDbType.Int).Value = dtRowdatos.productorID;
                cmd.Parameters.Add("@cicloID", SqlDbType.Int).Value = dtRowdatos.cicloID;
                cmd.Parameters.Add("@userID", SqlDbType.Int).Value = dtRowdatos.userID;
                cmd.Parameters.Add("@tipopagoID", SqlDbType.Int).Value = dtRowdatos.tipodePagoID;
                cmd.Parameters.Add("@Folio", SqlDbType.NVarChar).Value = dtRowdatos.folio;
                cmd.Parameters.Add("@fechainicobrointereses", SqlDbType.DateTime).Value = dtRowdatos.fechainicobrointereses.ToString();
                cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = dtRowdatos.fecha.ToString();
                cmd.Parameters.Add("@Pagada", SqlDbType.Bit).Value = dtRowdatos.pagada;
                cmd.Parameters.Add("@Total", SqlDbType.Money).Value = float.Parse(dtRowdatos.total.ToString());
                cmd.Parameters.Add("@Subtotal", SqlDbType.Money).Value = float.Parse(dtRowdatos.subtotal.ToString());
                cmd.Parameters.Add("@Iva", SqlDbType.Money).Value = float.Parse(dtRowdatos.iva.ToString());
                cmd.Parameters.Add("@creditoID", SqlDbType.Int).Value = dtRowdatos.creditoID;
                cmd.Parameters.Add("@FechadePago", SqlDbType.DateTime).Value = dtRowdatos.fechapago.ToString();
                cmd.Parameters.Add("@Interes", SqlDbType.Float).Value = float.Parse(dtRowdatos.interes.ToString());
                cmd.Parameters.Add("@Observaciones", SqlDbType.Text).Value = dtRowdatos.observaciones;
                //                  cmd.Parameters.Add("@storeTS",SqlDbType.DateTime).Value=Utils.getNowFormattedDate();
                //                  cmd.Parameters.Add("@updateTS",SqlDbType.DateTime).Value=Utils.getNowFormattedDate();
                int notaID = int.Parse(cmd.ExecuteScalar().ToString());
                dtRowdatos.notadeventaID = notaID;
            }
            catch (Exception exc)
            {
                sError = exc.Message;
                return false;
            }
            finally
            {
                conGaribay.Close();
            }

            return true;
        }

        public static int insertBoletas(ref dsBoletas.dtBoletasDataTable teibol)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            int iInsertedRows = 0;
            try
            {
                DataTable teibolToAdd = new DataTable();
                SqlCommand comm = new SqlCommand("SELECT * FROM BOLETAS", conn);
                conn.Open();
                SqlDataAdapter sqlAD = new SqlDataAdapter(comm);
                sqlAD.Fill(teibolToAdd);


                foreach (dsBoletas.dtBoletasRow row in teibol.Rows)
                {
                    teibolToAdd.ImportRow(row);
                }
                SqlCommandBuilder cb;
                cb = new SqlCommandBuilder(sqlAD);

                iInsertedRows = sqlAD.Update(teibolToAdd.GetChanges(DataRowState.Added));
                teibolToAdd.AcceptChanges();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, -1, "ERROR INGRESANDO BOLETAS EX:" + ex.Message, "insertBoletas");
            }
            finally
            {
                conn.Close();
            }
            return iInsertedRows;
        }
        public static bool insertPagoLiquidacion(ref dsLiquidacion.dtPagosRow pagoRow, ref string sError){
            int movimientocaja=-1;
            int movimientobanco=-1;
            ListBox a = new ListBox();
                
            Boolean valoraretornar=true;
            if(!pagoRow.IsEfectivoNull() && pagoRow.Efectivo>0){
                dsMovCajaChica.dtMovCajaChicaDataTable tb = new dsMovCajaChica.dtMovCajaChicaDataTable() ;
                dsMovCajaChica.dtMovCajaChicaRow rowMovCaja= tb.NewdtMovCajaChicaRow();
                rowMovCaja.cargo = pagoRow.Efectivo;
                rowMovCaja.abono = 0;// pagoRow.abono;
                rowMovCaja.bodegaID = pagoRow.bodegaID; 
                rowMovCaja.catalogoMovBancoInternoID = pagoRow.IscatalogoCajaChicaIDNull() ? -1 : pagoRow.catalogoCajaChicaID;
                rowMovCaja.subCatalogoMovBancoInternoID = pagoRow.IssubCatalogoCajaChicaIDNull() ? -1  :pagoRow.subCatalogoCajaChicaID;
                rowMovCaja.facturaOlarguillo = pagoRow.facturaolarguillo;
                rowMovCaja.fecha = pagoRow.fecha;
                rowMovCaja.nombre = pagoRow.nombre;
                rowMovCaja.numCabezas = 0;
                try
                {
                    if (!pagoRow.IsnumCabezasNull())
                    {
                        double dtemp = 0;
                        double.TryParse(pagoRow.numCabezas.ToString(), out dtemp);
                        rowMovCaja.numCabezas = dtemp;
                    }
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, -1, "error num cabezas threw exception", "insert pago");
                }
                
                rowMovCaja.Observaciones = pagoRow.Observaciones; 
                rowMovCaja.userID = pagoRow.userID;
                if(insertMovCajaChica(ref rowMovCaja,ref sError, rowMovCaja.userID,pagoRow.cicloID, false,-1,ref a,-1,0f,0f,Utils.Now,"")){
                    valoraretornar = true;
                    movimientocaja = rowMovCaja.movimientoID;
                }
            }
            else{ //ES MOV BANCO
                dsMovBanco.dtMovBancoDataTable movBanco = new dsMovBanco.dtMovBancoDataTable();
                dsMovBanco.dtMovBancoRow rowMovBanco = movBanco.NewdtMovBancoRow();
                rowMovBanco.abono = 0;// pagoRow.abono;
                rowMovBanco.cargo = pagoRow.monto;// .cargo;
                rowMovBanco.conceptoID = pagoRow.conceptoID;
                rowMovBanco.catalogoMovBancoFiscalID = pagoRow.catalogoFiscalMovBancoID;
                rowMovBanco.catalogoMovBancoInternoID = pagoRow.catalogoInternoMovBancoID;
                rowMovBanco.cuentaID = pagoRow.cuentaID;
                rowMovBanco.facturaOlarguillo = pagoRow.facturaolarguillo;
                rowMovBanco.fecha = pagoRow.fecha;
                rowMovBanco.subCatalogoMovBancoFiscalID = pagoRow.IssubCatalogoFiscalMovBancoIDNull() ? -1 : pagoRow.subCatalogoFiscalMovBancoID;
                rowMovBanco.subCatalogoMovBancoInternoID = pagoRow.IssubCatalogoInternoMovBancoIDNull() ? - 1 : pagoRow.subCatalogoInternoMovBancoID;    
                rowMovBanco.numCabezas = pagoRow.IsnumCabezasNull()? 0 : pagoRow.numCabezas;
                rowMovBanco.numCheque = pagoRow.IsnumChequeNull()? 0:  pagoRow.numCheque;
                rowMovBanco.chequeNombre = pagoRow.chequenombre;
                rowMovBanco.userID = pagoRow.userID;
                rowMovBanco.nombre = pagoRow.nombre;
                
                if(insertMovementdeBanco(ref rowMovBanco,ref sError,rowMovBanco.userID,rowMovBanco.cuentaID,false,-1,ref a,-1,0f,0f,Utils.Now,pagoRow.cicloID,"")){
                    valoraretornar = true;
                    movimientobanco = rowMovBanco.movBanID;
                }
            }
            //SqlConnection conInsertaPago = new SqlConnection(myConfig.ConnectionInfo);
            string query = "insert into PagosLiquidacion(liquidacionID, cicloID, productorID, fecha,";
            if(movimientobanco!=-1){
                query += " movbanID, ";
            }
            else{
                query += " movimientoID, ";
            }
            query+= " userID, storeTS) values(@liquidacionID, @cicloID, @productorID, @fecha, ";
            if(movimientobanco!=-1){
               query  += " @movBanID, ";
            }
            else{
                query += " @movimientoID, ";
            }
            query += "@userID, @storeTS)";

            SqlCommand cmdInserPago = new SqlCommand(query);
            SqlConnection connInsertPago = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                cmdInserPago.Connection = connInsertPago;
                connInsertPago.Open();
                cmdInserPago.Parameters.Add("@liquidacionID", SqlDbType.Int).Value = pagoRow.liquidacionID;
                cmdInserPago.Parameters.Add("@cicloID", SqlDbType.Int).Value = pagoRow.cicloID;
                cmdInserPago.Parameters.Add("@productorID", SqlDbType.Int).Value = pagoRow.productorID;
                cmdInserPago.Parameters.Add("@fecha", SqlDbType.DateTime).Value = pagoRow.fecha;
                if (movimientobanco != -1)
                {
                    cmdInserPago.Parameters.Add("@movBanID", SqlDbType.Int).Value = movimientobanco;
                }
                else
                {
                    cmdInserPago.Parameters.Add("@movimientoID", SqlDbType.Int).Value = movimientocaja;
                }
                cmdInserPago.Parameters.Add("@userID", SqlDbType.Int).Value = pagoRow.userID;
                cmdInserPago.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.Now;
                if (cmdInserPago.ExecuteNonQuery() != 1)
                {
                    throw new Exception("Error insertando el pago liquidacion en pagosliqudiaciones, devolvió -1, CallStack:" + Environment.StackTrace);
                }
            }
            catch (Exception error)
            {
                valoraretornar = false;
                sError = error.Message;
            }
            finally {
                connInsertPago.Close();
            }

            return valoraretornar;
        }
        public static void sacaFechasCiclo(int cicloID, ref DateTime dateinicio, ref DateTime datefin, int userID) {
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            string sql = "Select fechaInicio, fechaFinzona1 from Ciclos where cicloID = @cicloID";
            SqlCommand cmdSel = new SqlCommand(sql,conGaribay);
            conGaribay.Open();
            try
            {
                cmdSel.Parameters.Add("@cicloID",SqlDbType.Int).Value = cicloID;
                SqlDataReader reader = cmdSel.ExecuteReader();
                reader.Read();
                dateinicio = DateTime.Parse(reader[0].ToString());
                datefin = DateTime.Parse(reader[1].ToString());


            }
            catch(Exception ex){
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, userID, "ERROR AL SACAR LAS FECHAS DEL CICLO" + ex.Message, "ERROR AL SACAR LAS FECHAS DEL CICLO");
            }
            finally{

            }
        }
        public static bool insertAnticipo(int cicloID, int productorID, int userID, ref ListBox lista, int movBancoID, int movCajaID, double interesAnual, double interesMoratorio, DateTime fechalimite, ref string sError, string nombreproductor, double monto, DateTime fechaMov)
       {
           bool valoraretornar = true;
           string sqlins = "insert into Anticipos (cicloID,productorID, ";
           if(movCajaID!=-1) 
                sqlins += " movimientoID, ";
           else
                sqlins +=" movbanID, ";
           sqlins += " userID, storeTS, interesAnual, interesMoratorio, fechaLimitePagoPrestamo, monto, fecha) values(@cicloID, @productorID, ";
          if(movCajaID!=-1) 
                sqlins += " @movimientoID, ";
           else
                sqlins +=" @movbanID, ";
           sqlins += "@userID, @storeTS, @interesAnual, @interesMoratorio, @fechaLimitedePago, @monto, @fecha)";
           sqlins += " SELECT NewID = SCOPE_IDENTITY();";
           SqlConnection conGaribay2 = new SqlConnection(myConfig.ConnectionInfo);
           SqlConnection conDetalleAnticipo = new SqlConnection(myConfig.ConnectionInfo);
           SqlCommand cmdinse = new SqlCommand(sqlins,conGaribay2);
           SqlConnection conInsertinLiq = new SqlConnection(myConfig.ConnectionInfo);
           SqlConnection conSacaLiquidaciones = new SqlConnection(myConfig.ConnectionInfo);
           conGaribay2.Open();
           try
           {
               cmdinse.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fechaMov;
               cmdinse.Parameters.Add("@cicloID", SqlDbType.Int).Value = cicloID;
               cmdinse.Parameters.Add("@productorID", SqlDbType.Int).Value = productorID;
               if(movCajaID!=-1) 
                   cmdinse.Parameters.Add("@movimientoID", SqlDbType.Int).Value = movCajaID;
               else 
                   cmdinse.Parameters.Add("@movbanID", SqlDbType.Int).Value = movBancoID;
               cmdinse.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
               cmdinse.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = Utils.getNowFormattedDate();
               cmdinse.Parameters.Add("@interesAnual", SqlDbType.Float).Value = interesAnual ;
               cmdinse.Parameters.Add("@interesMoratorio", SqlDbType.Float).Value = interesMoratorio;
               cmdinse.Parameters.Add("@fechaLimitedePAgo", SqlDbType.DateTime).Value = fechalimite;
               cmdinse.Parameters.Add("@monto", SqlDbType.Float).Value = monto;
               
               //cmdinse.Parameters.Add("@boletaID", SqlDbType.Int).Value = boletaID;
               //CHANGE HERE TO ACCEPT MORE THAN ONE BOLETA.
               
               int anticipoID = int.Parse(cmdinse.ExecuteScalar().ToString());
               if(movCajaID!=-1)
               {
                   Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDECAJACHICA, 
                       Logger.typeUserActions.INSERT, 
                       userID, 
                       "INSERTO UN ANTICIPO AL PRODUCTOR: " + nombreproductor + " DEL MOVIMIENTO DE CAJA NÚMERO: " + movCajaID.ToString());
                   cmdinse.CommandText = "INSERT INTO dbo.Anticipos_Movimientos (anticipoID, movimientoID) VALUES (@anticipoID,@movimientoID);";
                   cmdinse.Parameters.Clear();
                   cmdinse.Parameters.Add("@anticipoID", SqlDbType.Int).Value = anticipoID;
                   cmdinse.Parameters.Add("@movimientoID", SqlDbType.Int).Value = movCajaID;
                   cmdinse.ExecuteNonQuery();
               }
               else{
                   Logger.Instance.LogUserSessionRecord(Logger.typeModulo.MOVIMIENTOSDEBANCO, Logger.typeUserActions.INSERT, userID, "INSERTO UN ANTICIPO AL PRODUCTOR: " + nombreproductor + " DEL MOVIMIENTO DE BANCO NÚMERO: " + movBancoID.ToString());

               }
               string detalleanticipo;
               detalleanticipo = "insert into Boletas_Anticipos(anticipoID, ticket) values (@anticipoID, @ticket)";
               
               SqlCommand cmdInsert = new SqlCommand(detalleanticipo, conDetalleAnticipo);
               conDetalleAnticipo.Open();
               for(int i=0; i<lista.Items.Count; i++){
                   cmdInsert.Parameters.Clear();
                   cmdInsert.Parameters.Add("@anticipoID", SqlDbType.Int).Value = anticipoID;
                   cmdInsert.Parameters.Add("@ticket", SqlDbType.NVarChar).Value = lista.Items[i].Value;
                   cmdInsert.ExecuteNonQuery();
               }
               //SI EL PRODUCTOR TIENE UNA LIQUIDACION ABIERTA SE LO ASIGNAMOS EL ANTICIPO A LA LISTA DETALLE
               conSacaLiquidaciones.Open();
               string sacaLiq = "select LiquidacionID from Liquidaciones where productorID = @prodID and cobrada = 0";
               SqlCommand  cmdsacaliq= new SqlCommand(sacaLiq,conSacaLiquidaciones);
               cmdsacaliq.Parameters.Clear();
               cmdsacaliq.Parameters.Add("@prodID",SqlDbType.Int).Value = productorID;
               int numliq = cmdsacaliq.ExecuteScalar() != null ?  int.Parse(cmdsacaliq.ExecuteScalar().ToString()) : -1;
               if(numliq!=-1){//HAY UNA LIQ
                   conInsertinLiq.Open();
                   string sqlinserinLiq = "insert into Liquidaciones_Anticipos(LiquidacionID, Anticipos) values(@liqID, @ant)";
                   SqlCommand cmdinserinliq = new SqlCommand(sqlinserinLiq,conInsertinLiq);
                   cmdinserinliq.Parameters.Clear();
                   cmdinserinliq.Parameters.Add("@liqID",SqlDbType.Int).Value = numliq;
                   cmdinserinliq.Parameters.Add("@ant",SqlDbType.Int).Value = anticipoID;
                   cmdinserinliq.ExecuteNonQuery();
                   Logger.Instance.LogUserSessionRecord(Logger.typeModulo.LIQUIDACIONES,Logger.typeUserActions.UPDATE,userID,"SE ASIGNO EL ANTICIPO: " + anticipoID.ToString() + " A LA LIQUIDACION: " + numliq.ToString());
               }

               
          

           }
           catch (Exception ex) {
               sError = ex.Message;
               Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, (new BasePage()).UserID, "ERROR AGREGANDO ANTICIPO: " + ex.Message, "insertAnticipo");
               valoraretornar = false;
              
           }
           finally{
               conGaribay2.Close();
               conDetalleAnticipo.Close();
           }
           return valoraretornar;      
       }
       public static bool realizaLiquidacion(int liquidacionid, ref dsLiquidacion.dtPagosDataTable dtPagos, ref string sError)
       {
           if (dtPagos == null) return false;
           bool valoraretornar = true;
           for (int i = 0; i < dtPagos.Rows.Count; i++)
           {
               dsLiquidacion.dtPagosRow row = dtPagos.NewdtPagosRow();
               row = (dsLiquidacion.dtPagosRow)(dtPagos.Rows[i]);
               row.liquidacionID = liquidacionid;
               if (!(insertPagoLiquidacion(ref row, ref sError)))
               {
                   valoraretornar = false;
               }
           }
           if (valoraretornar)
           {// SI NO SE HA CAGADO MARCAMOS BOLETAS COMO PAGADAS!!!!
               string queryBoletas = "select BoletaID from Liquidaciones_Boletas where LiquidacionID = @liqID";
               SqlConnection conSacaBoletas = new SqlConnection(myConfig.ConnectionInfo);
               SqlCommand cmdSelectBoletas = new SqlCommand(queryBoletas, conSacaBoletas);
               try
               {
                   conSacaBoletas.Open();
                   cmdSelectBoletas.Parameters.Clear();
                   cmdSelectBoletas.Parameters.Add("@liqID", SqlDbType.Int).Value = liquidacionid;
                   SqlDataReader readBoletas = cmdSelectBoletas.ExecuteReader();
                   ArrayList listaboletas = new ArrayList();
                   while (readBoletas.Read())
                   {
                       listaboletas.Add(readBoletas["BoletaID"].ToString());

                   }
                   string boletas = string.Join(",", (string[])(listaboletas.ToArray(typeof(string))));

                   string sqlupdateBoletas = "update Boletas set pagada=1 where boletaID in (";
                   sqlupdateBoletas = sqlupdateBoletas + boletas + ")";
                   SqlConnection conUpdate = new SqlConnection(myConfig.ConnectionInfo);
                   SqlCommand cmdUpdate = new SqlCommand(sqlupdateBoletas, conUpdate);
                   try
                   {
                       conUpdate.Open();
                       cmdUpdate.ExecuteNonQuery();
                   }
                   catch (Exception exc)
                   {
                        conUpdate.Close();
                        throw new Exception(exc.Message);


                   }
                   finally
                   {
                       conUpdate.Close();
                   }
                   //AQUI MARCAMOS NOTAS COMO PAGADAS



                   //FINALMENTE MARCAMOS LIQ COMO PAGADA
                   string sqlmarcaliquidacion = "update Liquidaciones set cobrada = 1, userIdejecuto = @userIdejecuto, fechaEjecucion = @fechaEjecucion where LiquidacionID = @liqID";
                   SqlConnection conMarcaLiquidacion = new SqlConnection(myConfig.ConnectionInfo);
                   SqlCommand cmdMarcaLiquidacion = new SqlCommand(sqlmarcaliquidacion, conMarcaLiquidacion);
                   try
                   {
                       conMarcaLiquidacion.Open();
                       cmdMarcaLiquidacion.Parameters.Add("@liqID", SqlDbType.Int).Value = liquidacionid;
                       cmdMarcaLiquidacion.Parameters.Add("@userIdejecuto",SqlDbType.Int).Value = (new BasePage()).UserID;
                       cmdMarcaLiquidacion.Parameters.Add("@fechaEjecucion",SqlDbType.DateTime).Value = Utils.Now;
                       cmdMarcaLiquidacion.ExecuteNonQuery();


                   }
                   catch (Exception exc)
                   {
                       conMarcaLiquidacion.Close();
                       throw new Exception(exc.Message);

                   }
                   finally {
                       conMarcaLiquidacion.Close();
                   
                   }



                   

               }
               catch (Exception ex)
               {
                   if (sError.Length > 0)
                       sError += ". ";
                   sError += ex.Message;
                   valoraretornar = false;
                   reverseLiquidacion(liquidacionid, ref sError);

               }
               finally
               {
                   conSacaBoletas.Close();

               }
              


           }

           return valoraretornar;
       }
       public static bool reverseLiquidacion(int liquidacionid, ref string sError)
       {
             
             bool valoraretornar = true;
             //BORRAMOS LOS MOVIMIENTOS 
             string queryMov = "select * from PagosLiquidacion where liquidacionID = @liqID";
             SqlConnection conBorraMov = new SqlConnection(myConfig.ConnectionInfo);
             SqlCommand cmdBorraMov = new SqlCommand(queryMov, conBorraMov);
             conBorraMov.Open();
             try{
                 cmdBorraMov.Parameters.Clear();
                 cmdBorraMov.Parameters.Add("@liqID", SqlDbType.Int).Value = liquidacionid;
                 SqlDataReader readMov = cmdBorraMov.ExecuteReader();
                 while (readMov.Read())
                 {
                     int movCajaID = 0, movBancoID = 0;
                     if(int.TryParse(readMov["movbanID"].ToString(),out movBancoID)){
                         movBancoID = int.Parse(readMov["movbanID"].ToString());
                         string sqldelMov = "delete from MovimientosCuentasBanco where movbanID = @movbanID";
                         SqlConnection conDeleteMov = new SqlConnection(myConfig.ConnectionInfo);
                         SqlCommand cmdDeleteMov = new SqlCommand(sqldelMov,conDeleteMov);
                         conDeleteMov.Open();
                         try{
                             cmdDeleteMov.Parameters.Clear();
                             cmdDeleteMov.Parameters.Add("@movbanID",SqlDbType.Int).Value = movBancoID;
                             cmdDeleteMov.ExecuteNonQuery();

                         }
                         catch(Exception ex){
                             conDeleteMov.Close();
                             throw new Exception(ex.Message);
                         }
                     }
                     else{
                         movCajaID = int.Parse(readMov["movimientoID"].ToString());
                         string sqldelMov = "delete from MovimientosCaja where movimientoID = @movID";
                         SqlConnection conDeleteMov = new SqlConnection(myConfig.ConnectionInfo);
                         SqlCommand cmdDeleteMov = new SqlCommand(sqldelMov, conDeleteMov);
                         conDeleteMov.Open();
                         try
                         {
                             cmdDeleteMov.Parameters.Clear();
                             cmdDeleteMov.Parameters.Add("@movID", SqlDbType.Int).Value = movCajaID;
                             cmdDeleteMov.ExecuteNonQuery();

                         }
                         catch (Exception ex)
                         {
                             conDeleteMov.Close();
                             throw new Exception(ex.Message);
                         }
                     }
                 }

             }
             catch(Exception ex){
                 conBorraMov.Close();
                 throw new Exception(ex.Message);
             }
             finally{
                 conBorraMov.Close();
             }
             
             //BORRAMOS LOS PAGOS
               string queryPagos = "delete  from PagosLiquidacion where liquidacionID = @liqID ";
               SqlConnection conDeletePagos = new SqlConnection(myConfig.ConnectionInfo);
               SqlCommand cmdDeletePagos = new SqlCommand(queryPagos, conDeletePagos);
               try
               {
                   conDeletePagos.Open();
                   cmdDeletePagos.Parameters.Clear();
                   cmdDeletePagos.Parameters.Add("@liqID", SqlDbType.Int).Value = liquidacionid;
                   cmdDeletePagos.ExecuteNonQuery();
                   //MARCAMOS COMO NO PAGADAS LAS BOLETAS
                   string sqlupdateBoletas = "update Boletas set pagada=0 where boletaID in (select boletaID from Liquidaciones_Boletas where LiquidacionID = @liqID)";
                   SqlConnection conUpdate = new SqlConnection(myConfig.ConnectionInfo);
                   SqlCommand cmdUpdate = new SqlCommand(sqlupdateBoletas, conUpdate);
                   try
                   {
                       conUpdate.Open();
                       cmdUpdate.Parameters.Add("@liqID",SqlDbType.Int).Value=liquidacionid;
                       cmdUpdate.ExecuteNonQuery();
                   }
                   catch (Exception exc)
                   {
                       conUpdate.Close();
                       throw new Exception(exc.Message);

                   }
                   finally
                   {
                       conUpdate.Close();
                   }

                   //MARCAMOS NOTAS COMO NO PAGADAS





                   // FINALMENTE MARCAMOS LIQ COMO NO PAGADA y borramos los pagos 

                   string sqlmarcaliquidacion = "update Liquidaciones set cobrada = 0 where LiquidacionID = @liqID ";
                   SqlConnection conMarcaLiquidacion = new SqlConnection(myConfig.ConnectionInfo);
                   SqlCommand cmdMarcaLiquidacion = new SqlCommand(sqlmarcaliquidacion, conMarcaLiquidacion);
                   try
                   {
                       conMarcaLiquidacion.Open();
                       cmdMarcaLiquidacion.Parameters.Add("@liqID", SqlDbType.Int).Value = liquidacionid;
                       cmdMarcaLiquidacion.ExecuteNonQuery();


                   }
                   catch (Exception exc)
                   {
                       conMarcaLiquidacion.Close();
                       throw new Exception(exc.Message);

                   }
                   finally
                   {
                       conMarcaLiquidacion.Close();

                   }



               }
               catch (Exception ex)
               {
                   if (sError.Length > 0)
                       sError += ". ";
                   sError += ex.Message;
                   valoraretornar = false;

               }
               finally
               {
                   conDeletePagos.Close();

               }
               


         

           return valoraretornar;
       }

       public static bool tarjetaDieselAlreadyExist(int folio)
       {
           bool bTarjetaExist = false;
           //check id
           SqlConnection con = new SqlConnection(myConfig.ConnectionInfo);
           try
           {
               con.Open();
               SqlCommand com = new SqlCommand();
               com.Connection = con;
               if (folio != null)
               {
                   com.CommandText = "select count(*) from TarjetasDiesel where folio = @folio";
                   com.Parameters.Add("@folio", SqlDbType.Int).Value = folio;
                   if (int.Parse(com.ExecuteScalar().ToString()) > 0)
                   {
                       bTarjetaExist = true;
                   }
               }


           }
           catch (System.Exception ex)
           {

           }
           finally
           {
               con.Close();
           }
           return bTarjetaExist;
       }

       public static bool addTarjetaDiesel(int movID, int folio, float monto, float litros, ref string sError, int userID, int tipo)
       {
           bool result;
           if (!dbFunctions.tarjetaDieselAlreadyExist(folio))
           {
               string sqlIns = "Insert into TarjetasDiesel(folio, monto, litros) values (@folio, @monto, @litros);";
               //sqlIns += "Insert into MovimientosCaja_TarjetasDiesel(movimientoID, folio) values (@movimientoID, @folio)";
               SqlConnection addConn = new SqlConnection(myConfig.ConnectionInfo);
               SqlCommand addComm = new SqlCommand(sqlIns, addConn);
               try
               {
                   addComm.Parameters.Add("@folio", SqlDbType.Int).Value = folio;
                   addComm.Parameters.Add("@monto", SqlDbType.Float).Value = monto;
                   addComm.Parameters.Add("@litros", SqlDbType.Float).Value = litros;

                   addConn.Open();
                   addComm.ExecuteNonQuery();

                   //Insertamos la relacion con el mov de caja
                   if(tipo==0){
                   addComm.CommandText = "Insert into MovimientosCaja_TarjetasDiesel(movimientoID, folio) values (@movimientoID, @folio)";
                   addComm.Parameters.Clear();
                   addComm.Parameters.Add("@movimientoID", SqlDbType.Int).Value = movID;
                   addComm.Parameters.Add("@folio", SqlDbType.Int).Value = folio;
                   addComm.ExecuteNonQuery();
                   }
                   else if(tipo==1){

                       addComm.CommandText = "Insert into Pagos_NotaVenta( fecha, notadeventaID, tarjetaDieselID) values (@fecha, @notadeventaID, @tarjetaDieselID)";
                       addComm.Parameters.Clear();
                       addComm.Parameters.Add("@fecha", SqlDbType.DateTime).Value = Utils.Now;
                       addComm.Parameters.Add("@notadeventaID", SqlDbType.Int).Value = movID;
                       addComm.Parameters.Add("@tarjetaDieselID", SqlDbType.Int).Value = folio;
                       
                       addComm.ExecuteNonQuery();

                   }else if(tipo==2){
                       addComm.CommandText = "Insert into Pagos_Credito( fecha, creditoID, tarjetaDieselID) values (@fecha, @creditoID, @tarjetaDieselID)";
                       addComm.Parameters.Clear();
                       addComm.Parameters.Add("@fecha", SqlDbType.DateTime).Value = Utils.Now;
                       addComm.Parameters.Add("@creditoID", SqlDbType.Int).Value = movID;
                       addComm.Parameters.Add("@tarjetaDieselID", SqlDbType.Int).Value = folio;

                       addComm.ExecuteNonQuery();

                   }

                   result = true;
               }
               catch (Exception ex)
               {
                   result = false;

                   Logger.Instance.LogException(Logger.typeUserActions.INSERT, "EL ERROR SE DIO AL TRATAR DE AGREGAR UNA TARJETA DE DIESEL",ref ex);
                   sError = "ERROR AL INSERTAR LA TARJETA DIESEL. EX : " + ex.Message;
               }
               finally
               {
                   addConn.Close();
               }
           }
           else
           {
               sError = "ERROR AL INSERTAR LA TARJETA DIESEL. TARJETA CON FOLIO "+folio +"YA EXISTE";
               Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, sError, "dbFunctions funcion addtarjetadiesel");
               
               result = false;
           }
           //this.txtFolio.ReadOnly = true;
           //this.btnAddTarjetaDiesel.Visible = false;
           //this.btnModTarjetaDiesel.Visible = true;
           return result;
       }

       public static bool upTarjetaDiesel(int folio, float monto, float litros, ref string sError, int userID)
       {
           bool result;
           string sqlIns = "Update TarjetasDiesel set monto=@monto, litros=@litros) Where folio = @folio";
           SqlConnection addConn = new SqlConnection(myConfig.ConnectionInfo);
           SqlCommand addComm = new SqlCommand(sqlIns, addConn);
           try
           {
               addComm.Parameters.Add("@monto", SqlDbType.Float).Value = monto;
               addComm.Parameters.Add("@litros", SqlDbType.Float).Value = litros;
               addComm.Parameters.Add("@folio", SqlDbType.Int).Value = folio;
               addConn.Open();
               addComm.ExecuteNonQuery();
               result = true;
           }
           catch (Exception ex)
           {
               Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "ERROR AL MODIFICAR UNA TARJETA DIESEL", ref ex);
               Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.INSERT, userID, ex.Message, "EL ERROR SE DIO AL TRATAR DE MODIFICAR UNA TARJETA DE DIESEL");
               sError = "ERROR AL MODIFICAR LA TARJETA DIESEL. EX : " + ex.Message;
               result = false;
           }
           finally
           {
               addConn.Close();
           }
           return result;
       }


        public static bool folioDeOrdenExiste(string sFolio, out int idOrden)
        {
            idOrden = -1;
            return false;
            /*
            bool existe;
            
                        string sqlIns = " SELECT ordenID FROM Orden_de_entrada WHERE (folio = @folio)";
            
                       SqlConnection addConn = new SqlConnection(myConfig.ConnectionInfo);
                       SqlCommand addComm = new SqlCommand(sqlIns, addConn);
                       try
                       {
                           addComm.Parameters.Add("@folio", SqlDbType.Int).Value = sFolio;
                           addConn.Open();
                           SqlDataReader rd = addComm.ExecuteReader();
                           if (rd.HasRows && rd.Read())
                           {
                               existe = true;
                               idOrden = int.Parse(rd["ordenID"].ToString());
                           }
                           else
                           {
                               idOrden = -1;
                               existe = false;
                           }
                           
                       }
                       catch (Exception ex)
                       {
                           Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "ERROR AL HACER LA CONSULTA PARA VERIFICAR SI EL FOLIO DE LA ORDEN DE ENTRADA YA EXISTE", ref ex);
                           idOrden = -1;
                           existe = false;
                       }
                       finally
                       {
                           addConn.Close();
                       }
             
            
             
                        return existe;*/
            

        }
    }
}