using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.IO;
using BasculaGaribay;



namespace WSGaribay
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://www.ws.cheliskis.com")]

    
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Servicios : System.Web.Services.WebService
    {
       
        internal string ConnectionInfo
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["GaribayConnectionString"].ConnectionString;
            }
        }

        [WebMethod(Description="Hello World Function :-)")]
        public string HelloWorld()
        {
            return "Hello World from Garibay Web Service";
        }

        #region VALIDATEANDRECORDENTRY
        protected bool TryLogin(String username, String password, out int iUserID, out int idInserted, out String sUsuario)
        {
            
            bool validated = false;
            int userID = -1;
            DateTime lastTry = DateTime.MinValue;
            string user = null;
            int ntries = 0;
            string ip = null;
            idInserted = -1;
            sUsuario = "";

            string qryIns = "INSERT INTO loginRecords VALUES (@datestamp, @username, @trynum, @IPAddress, @loginFromID);select loginRecordID = SCOPE_IDENTITY();";
            string qrySel = "SELECT TOP 1 * FROM loginRecords WHERE username = @username ORDER BY loginRecordID DESC";
            SqlConnection sqlCon = new SqlConnection(this.ConnectionInfo);
            try
            {
                
                SqlCommand cmdSel = new SqlCommand(qrySel, sqlCon);
                cmdSel.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                sqlCon.Open();
                SqlDataReader rdr = cmdSel.ExecuteReader();
                if (rdr.HasRows && rdr.Read())
                {
                    lastTry = (DateTime)rdr["datestamp"];
                    user = rdr["username"].ToString();
                    ntries = (int)rdr["trynum"];
                    ip = rdr["IPAddress"].ToString();
                }
                sqlCon.Close();
                System.TimeSpan dif = WSUtils.myNow - lastTry;
                if (dif.TotalMinutes > 10 || (dif.TotalMinutes < 10 && ntries < 5))
                {
                    
                    if (validated = this.validateUser(username, password, out userID, out sUsuario))
                    {
                        ntries = 0;
                    }
                    else
                    {

                        if (dif.TotalMinutes > 10)
                        {
                            ntries = 1;
                        }
                        else
                        {
                            ntries++;
                        }
                    }
                    SqlCommand cmdIns = new SqlCommand(qryIns, sqlCon);
                    cmdIns.Parameters.Add("@datestamp", SqlDbType.DateTime).Value = WSUtils.myNow;//Utils.converttoLongDBFormat(WSUtils.myNow.ToString());
                    cmdIns.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                    cmdIns.Parameters.Add("@trynum", SqlDbType.Int).Value = ntries;
                    cmdIns.Parameters.Add("@IPAddress", SqlDbType.NVarChar).Value = HttpContext.Current.Request.UserHostAddress;
                    cmdIns.Parameters.Add("@loginFromID", SqlDbType.Int).Value = 1;
                    sqlCon.Open();
                    idInserted=int.Parse(cmdIns.ExecuteScalar().ToString());
                    this.LogUserSessionRecord(typeModulo.WSLOGIN, typeUserActions.LOGIN, userID, "EL USUARIO "+ username +" HA INGRESADO A LA SISTEMA DE LA BASCULA");
                }
                else
                {
                    ntries = 0;
                    System.TimeSpan esp = lastTry.AddMinutes(10) - WSUtils.myNow;
//                     this.lblMsjIntentos.Visible = true;
//                     this.lblMsjIntentos.Text = string.Format(myConfig.StrFromMessages("USERBLOCKED"), this.txtUsuario.Text.ToUpper(), esp.Minutes.ToString(), esp.Seconds.ToString());
//                     this.lblLoginResult.Text = "";
//                     this.lblMensaje.Text = "";
//                     this.panelMensaje.Visible = true;

                }
            }
            catch (Exception err3)
            {
//                 this.lblMsjIntentos.Text = err3.Message;
//                 this.panelMensaje.Visible = true;
            }
            finally
            {
                sqlCon.Close();
            }
            iUserID = userID;
            return validated;
        }
        #endregion

        private bool validateUser(String sUser, String sPassword, out int iUserID, out String sNombre)
        {
            bool bResult = false;
            sNombre = "";
            iUserID = -1;
            string sSql = "SELECT userID , Nombre FROM Users WHERE username = @Username AND password = @Password and enabled = 1;";
            SqlConnection sqlConn = new SqlConnection(this.ConnectionInfo);
            try
            {
                SqlCommand sqlComm = new SqlCommand(sSql, sqlConn);
                sqlComm.Parameters.Add("@Username", SqlDbType.VarChar).Value = sUser;
                sqlComm.Parameters.Add("@Password", SqlDbType.VarChar).Value = sPassword;

                sqlConn.Open();
                SqlDataReader rd=sqlComm.ExecuteReader();
                if(rd.HasRows&&rd.Read()){

                    iUserID = int.Parse(rd[0].ToString());
                    sNombre = rd[1].ToString().ToUpper();
                    bResult = true;
                }

            }
            catch (System.Exception ex)
            {
                //this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.LOGIN, sacaIDUsuario(iSessionID.ToString()), ex.Message, "Error enviado desde Bascula, Error al tratar de actualizar la fecha del ultimo ingreso al sistema de la sessión");
            }
            finally
            {
                sqlConn.Close();
            }
            return bResult;
        }

        private bool IsSessionValid(String sUsername, String sPassword)
        {
            //validate here the session
            //if the session(user) is invalid then log the message.
            return true;
        }

        private bool IsNotOutOfLimitTime(DateTime lastTry, out String mensaje)
        {
            //validate here the datetime
            
            System.TimeSpan dif = WSUtils.myNow - lastTry;
            mensaje = "";
            if(dif.Minutes>30){
                mensaje = "Session Inválida por Inactividad, vuelva a Iniciar Sessión";
                return false;
            }

            //if the date in not out of range then log the message.
            return true;
        }
        private bool isTheSameIP(String dbIp,  out String mensaje)
        {
            mensaje = "";
            if(dbIp!=HttpContext.Current.Request.UserHostAddress){
                mensaje = "La IP actual no es la misma que con la que Inició Session, vuelva a Iniciar Sessión";
                return false;
            }
            //if the session(user) is invalid then log the message.
            
            return true;
        }

        


        [WebMethod(Description = "Validá la sessión")]
        public bool validaSession(int idSession, out String mensajeError)
        {
            bool bResult = false;
            mensajeError = "";
           String sqlQuery= "SELECT WSSessions.WSSessionID AS WSSsessions, WSSessions.lastActivityTS AS lastActivityTS, loginRecords.IPAddress AS IPAddress";
           sqlQuery += " FROM loginRecords INNER JOIN";
           sqlQuery += " WSSessions ON loginRecords.loginRecordID = WSSessions.loginRecordID WHERE (WSSessions.WSSessionID = @WSSessionID)";


           SqlConnection sqlConn = new SqlConnection(this.ConnectionInfo);
           SqlCommand sqlComm = new SqlCommand(sqlQuery, sqlConn);
           SqlDataReader dr;

            try
            {
                sqlComm.Parameters.Add("@WSSessionID", SqlDbType.Int).Value = idSession;
                sqlConn.Open();
                dr = sqlComm.ExecuteReader();
                if(dr.HasRows&&dr.Read())
                { //si existe esa sessión
                    if (IsNotOutOfLimitTime((DateTime)dr["lastActivityTS"],out mensajeError)) 
                    {
                        if (isTheSameIP(dr["IPAddress"].ToString(),out mensajeError))
                        {
                            bResult = true;
                        }
                        else
                        {
                            this.LogMessage(typeLogMessage.INFO, typeUserActions.LOGIN, sacaIDUsuario(idSession.ToString()),"SE INVÁLIDO LA SESSIÓN POR QUE LA IP FUE DISTINTA" , "Error enviado desde Bascula, Error al tratar de validar la session");
                        }
                    }
                    else 
                    {
                        this.LogMessage(typeLogMessage.INFO, typeUserActions.LOGIN, sacaIDUsuario(idSession.ToString()), "SE INVÁLIDO LA SESSIÓN POR QUE SE EXECDIÓ EL LIMITE DE TIMEPO DE INACTIVIDAD", "Error enviado desde Bascula, Error al tratar de validar la session");
                    }
                }          
            }
            catch(Exception ex)
            {
                mensajeError = ex.Message;
                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.LOGIN, sacaIDUsuario(idSession.ToString()), ex.Message, "Error enviado desde Bascula, Error al tratar de validar la session");
            }
            finally
            {
                sqlConn.Close();
            }
            bResult = true;
            return bResult;
        }

        [WebMethod(Description="Get the last error :-(")]
        public String GetMyLastError()
        {
            String sLastError = "";
            return sLastError;
        }

        private void LogError(String sError)
        {
            //log the error into Database
            // get this error with GetMyLastError
        }

        [WebMethod(Description="Function to login into the Web service")]
        public bool Login(String sUsername, String sPassword, out String idSession, out String sUsuario)
        {
            bool bValid = false;
            int iUserID = -1;
            int idInserted=-1;
            int id = -1;
            idSession = null;
            sUsuario = "";
            try
            {
                sUsername = WSUtils.desEncriptaCadena(sUsername);
                sPassword = WSUtils.desEncriptaCadena(sPassword);
            
            if(sUsername.Length <= 0 || 
                sUsername.Length >= 20 || 
                sPassword.Length <= 0 || 
                sPassword.Length > 40)
            {
                return false;
                
            }
            bValid = this.TryLogin(sUsername, sPassword, out iUserID, out idInserted, out sUsuario);
            if (bValid)
            {
                id = this.GetWSSessionID(iUserID, idInserted);
                this.UpdateEntryTime(id, idInserted);
               
                idSession = WSUtils.encriptacadena(id.ToString());
                
            }
            }
            catch
            {
                //this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.LOGIN, sacaIDUsuario(iSessionID.ToString()), ex.Message, "Error enviado desde Bascula, Error al tratar de actualizar la fecha del ultimo ingreso al sistema de la sessión");
                return false;

            }
            return bValid;
        }


        [WebMethod(Description="Test DB Connection")]
        public bool TestDB()
        {
            return true;
//             DataSet ds = new DataSet();
//             SqlCommand comm = new SqlCommand("select * from ciclos");
//             return this.FillDataSet(comm, out ds);
        }


        internal void UpdateEntryTime(int iSessionID, int loginRecordID)
        {
            SqlCommand comm = new SqlCommand();
            SqlConnection conn = new SqlConnection(this.ConnectionInfo);
            try
            {
                conn.Open();
                comm.Connection = conn;
                comm.CommandText = "update WSSessions set entryTS = @entryTS,  lastActivityTS = @lastTS , loginRecordID=@loginRecordID where WSSessionID = @iSessionID";
                comm.Parameters.Add("@entryTS", SqlDbType.DateTime).Value = WSUtils.myNow;
                comm.Parameters.Add("@lastTS", SqlDbType.DateTime).Value = WSUtils.myNow;
                comm.Parameters.Add("@loginRecordID", SqlDbType.Int).Value = loginRecordID;
                comm.Parameters.Add("@iSessionID", SqlDbType.Int).Value = iSessionID;
               
                int iAffected = (int)comm.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.UPDATE, sacaIDUsuario(iSessionID.ToString()), ex.Message, "Error enviado desde Bascula, Error al tratar de actualizar la fecha del ultimo ingreso al sistema de la sessión");
            }
            finally
            {
                conn.Close();
            }
        }
        internal void UpdateLastActivity(int iSessionID)
        {
            SqlCommand comm = new SqlCommand();
            SqlConnection conn = new SqlConnection(this.ConnectionInfo);
            try
            {
                conn.Open();
                comm.Connection = conn;
                comm.CommandText = "update WSSessions set lastActivityTS = @lastTS where WSSessionID = @iSessionID";
                comm.Parameters.Add("@lastTS", SqlDbType.DateTime).Value = WSUtils.myNow;
                comm.Parameters.Add("@iSessionID", SqlDbType.Int).Value = iSessionID;
                int iAffected = (int)comm.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {

                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.UPDATE, sacaIDUsuario(iSessionID.ToString()), ex.Message, "Error enviado desde Bascula, Error al tratar de actualizar la fecha de la ultima actividad del usuario");
            }
            finally
            {
                conn.Close();
            }
        }

        internal int GetWSSessionID(int iUserID, int loginRecordID)
        {
            int iSessionID = -1;
            SqlCommand comm = new SqlCommand();
            SqlConnection conn = new SqlConnection(this.ConnectionInfo);
            try
            {
                conn.Open();
                comm.Connection = conn;
                comm.CommandText = "select WSSessionID from WSSessions where UserID = @UserID";
                comm.Parameters.Add("@UserID", SqlDbType.Int).Value = iUserID;
                if (comm.ExecuteScalar() == null)
                {
                    comm.CommandText = "insert into WSSessions(UserID,entryTS,lastActivityTS,loginRecordID) values(@UserID,@entryTS,@lastActivityTS,@loginRecordID)";
                    comm.Parameters.Clear();                    
                    comm.Parameters.Add("@UserID", SqlDbType.Int).Value = iUserID;
                    comm.Parameters.Add("@entryTS", SqlDbType.DateTime).Value = WSUtils.myNow;
                    comm.Parameters.Add("@lastActivityTS", SqlDbType.DateTime).Value = WSUtils.myNow;
                    comm.Parameters.Add("@loginRecordID", SqlDbType.Int).Value = loginRecordID;
                    comm.ExecuteNonQuery();
                    iSessionID = this.GetWSSessionID(iUserID,loginRecordID);
                }
                else
                {
                    iSessionID = (int)comm.ExecuteScalar();
                    //comm.CommandText = "UPDATE WSSessions SET where UserID = @UserID";
                    //comm.Parameters.Clear();
                    //comm.Parameters.Add("@UserID", SqlDbType.Int).Value = iUserID;
                    //comm.Parameters.Add("@entryTS", SqlDbType.DateTime).Value = WSUtils.myNow;
                    //comm.Parameters.Add("@lastActivityTS", SqlDbType.DateTime).Value = WSUtils.myNow;
                    //comm.Parameters.Add("@loginRecordID", SqlDbType.Int).Value = loginRecordID;
                    //comm.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.SELECT, iUserID, ex.Message, "Error enviado desde Bascula, Error al tratar de obtener la sessionid del usuario");
            }
            finally
            {
                conn.Close();
            }

            return iSessionID;
        }

        [WebMethod(Description="obvious ¬¬")]
        public void LogOut(String idsession)
        {
            LogUserSessionRecord(typeModulo.WSLOGIN,typeUserActions.LOGIN,sacaIDUsuario(idsession),"EL USUARIO "+ sacaNombreUsuarioDeWSsessionID(idsession) +" ha salido del sistema de la bascula");
         
         
            //and log the call of the function
        }

        [WebMethod(Description="Query Data from DB")]
        public String Query(String sQueryName,String fields,int idsession)
        {
            //The result is going to be encrypted. ^^
            String sResult = "";
            switch(sQueryName)
            {
                case "PRODUCTORES":
                    sResult = this.QueryProductores(fields,idsession);
                    sResult = BasculaGaribay.CompressUtils.Compress(sResult);
                    break;
                case "BOLETAS":
                    sResult = this.QueryBoletas(fields,idsession);
                    sResult = BasculaGaribay.CompressUtils.Compress(sResult);
                    break;
            }
            return sResult;
        }



        [WebMethod(Description = "Get Ciclos")]
        public String GetCiclos()
        {
            SqlCommand comm = new SqlCommand();
            String sXMLResult="";
            comm.CommandText = "SELECT cicloID, CicloName as Ciclo FROM Ciclos WHERE (cerrado = 0) order by CicloName DESC";
            DataSet ds = new DataSet();
            if (!this.FillDataSet(comm, out ds ))
            {
            }
            sXMLResult = this.DataSetToXML(ds);
            sXMLResult = BasculaGaribay.CompressUtils.Compress(sXMLResult);
            
            return sXMLResult;
            //return ds.Tables[0];
        }


        [WebMethod(Description = "Get Ciclos")]
        public String GetCiclosBoletas()
        {
            SqlCommand comm = new SqlCommand();

            comm.CommandText = "SELECT 0 AS cicloID, '  TODOS' AS Ciclo UNION SELECT cicloID, CicloName as Ciclo FROM Ciclos WHERE (cerrado = 0) order by Ciclo";
            DataSet ds = new DataSet();
            if (!this.FillDataSet(comm, out ds))
            {
            }
            String sXMLResult = this.DataSetToXML(ds);
            sXMLResult = BasculaGaribay.CompressUtils.Compress(sXMLResult);
            
            return sXMLResult;
            //return ds.Tables[0];
        }

        [WebMethod(Description = "Select a Boleta")]
        public bool SelectBoletas(String id, int iSessionID, out String[] datos1)
        {
            this.UpdateLastActivity(iSessionID);
            string qrySel = "SELECT Boletas.*, ClienteVenta_Boletas.clienteventaID FROM ClienteVenta_Boletas RIGHT JOIN Boletas ON ClienteVenta_Boletas.BoletaID = Boletas.boletaID WHERE Boletas.boletaID=@boletaID";
            
            
            SqlDataReader datos;
            datos1 = new String[51];
            
            SqlConnection sqlCon = new SqlConnection(this.ConnectionInfo);
            SqlCommand cmdSel = new SqlCommand(qrySel, sqlCon);
            cmdSel.Parameters.Add("@boletaID", SqlDbType.Int).Value = int.Parse(id);
            try
            {

                sqlCon.Open();
                datos = cmdSel.ExecuteReader();
                datos.Read();

                for (int i = 0; i < datos.FieldCount; i++)
                    datos1[i] = datos[i].ToString();

                datos1[48] = datos[49].ToString();
                datos1[49] = datos[48].ToString();


                return true;



            }
            catch (Exception ex)
            {

                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.SELECT, sacaIDUsuario(iSessionID.ToString()), ex.Message, "Error enviado desde Bascula, Error al tratar de seleccionar los datos de una boleta");
            }
            finally
            {
                sqlCon.Close();
            }


            return false;


        }



        [WebMethod(Description = "Select a Boleta")]
        public bool SelectBoletasAsDataTable(String id, int iSessionID, out string datos1)
        {
            this.UpdateLastActivity(iSessionID);
            string qrySel = "SELECT     Boletas.boletaID, Boletas.cicloID, Boletas.userID, Boletas.productorID, Boletas.humedad, Boletas.dctoHumedad, Boletas.impurezas, Boletas.totaldescuentos, Boletas.pesonetoapagar, Boletas.precioapagar, Boletas.importe, Boletas.placas, Boletas.chofer, Boletas.pagada, Boletas.storeTS, Boletas.updateTS,  Boletas.productoID, Boletas.NumeroBoleta, Boletas.Ticket, Boletas.codigoClienteProvArchivo, Boletas.NombreProductor, Boletas.FechaEntrada,  Boletas.PesadorEntrada, Boletas.PesoDeEntrada, Boletas.BasculaEntrada, Boletas.FechaSalida, Boletas.PesoDeSalida, Boletas.PesadorSalida,  Boletas.BasculaSalida, Boletas.pesonetoentrada, Boletas.pesonetosalida, Boletas.dctoImpurezas, Boletas.dctoSecado, Boletas.totalapagar, Boletas.bodegaID,  Boletas.applyHumedad, Boletas.applyImpurezas, Boletas.applySecado, Boletas.FolioDestino, Boletas.PesoDestino, Boletas.Merma, Boletas.Flete,  Boletas.ImporteFlete, Boletas.PrecioNetoDestino, Boletas.dctoGranoChico, Boletas.dctoGranoDanado, Boletas.dctoGranoQuebrado, Boletas.dctoGranoEstrellado,  Boletas.transportistaID, Boletas.cabezasDeGanado, ISNULL(ClienteVenta_Boletas.clienteventaID, - 1) AS clienteventaID,  ISNULL(gan_Proveedores_Boletas.ganProveedorID, - 1) AS ganProveedorID, Boletas.llevaFlete, Boletas.deGranjaACorrales, ISNULL(boleta_proveedor.proveedorID,  - 1) AS proveedorID FROM         boleta_proveedor RIGHT OUTER JOIN Boletas ON boleta_proveedor.boletaID = Boletas.boletaID LEFT OUTER JOIN gan_Proveedores_Boletas ON Boletas.boletaID = gan_Proveedores_Boletas.boletaID LEFT OUTER JOIN ClienteVenta_Boletas ON Boletas.boletaID = ClienteVenta_Boletas.BoletaID WHERE Boletas.boletaID=@boletaID";
            datos1 = string.Empty;
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.CommandText = qrySel;
                comm.Parameters.Add("@boletaID", SqlDbType.Int).Value = int.Parse(id);
                DataSet ds = new DataSet();
                if (!this.FillDataSet(comm, out ds))
                {
                }
                String sXMLResult = this.DataSetToXML(ds);
                sXMLResult = BasculaGaribay.CompressUtils.Compress(sXMLResult);

                datos1 = sXMLResult;
                return true;



            }
            catch (Exception ex)
            {
                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.SELECT, sacaIDUsuario(iSessionID.ToString()), ex.Message, "Error enviado desde Bascula, Error al tratar de seleccionar los datos de una boleta");
            }


            return false;


        }
        [WebMethod(Description = "Get productores para cmb filtros")]
        public String GetProductorBoletas()
        {
            SqlCommand comm = new SqlCommand();

            comm.CommandText = "SELECT 0 AS productorID, '  TODOS' AS Nombre UNION SELECT productorID, LTRIM(apaterno + ' ' + amaterno + ' ' + nombre) as Nombre FROM Productores order by Nombre";
            DataSet ds = new DataSet();
            if (!this.FillDataSet(comm, out ds))
            {
            }
            String sXMLResult = this.DataSetToXML(ds);
            sXMLResult = BasculaGaribay.CompressUtils.Compress(sXMLResult);

            return sXMLResult;
            //return ds.Tables[0];

        }





        [WebMethod(Description = "Get Productos")]
        public String GetProductos()
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT Productos.productoID, Productos.productoGrupoID,Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Nombre FROM Productos INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID WHERE (Productos.productoGrupoID <> 1) AND (Productos.productoGrupoID <> 2) ORDER BY Productos.productoGrupoID, Productos.Nombre";
            
            DataSet ds = new DataSet();
            if (!this.FillDataSet(comm, out ds))
            {
            }
            String sXMLResult = this.DataSetToXML(ds);
            sXMLResult = BasculaGaribay.CompressUtils.Compress(sXMLResult);

            return sXMLResult;
            
                    }


        [WebMethod(Description = "Get  Clientes de venta")]
        public String GetClientesVentas()
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT 'TODOS' AS Nombre, 0 AS clienteventaID UNION SELECT nombre, clienteventaID FROM ClientesVentas ORDER BY NOMBRE";

            DataSet ds = new DataSet();
            if (!this.FillDataSet(comm, out ds))
            {
            }
            String sXMLResult = this.DataSetToXML(ds);
            sXMLResult = BasculaGaribay.CompressUtils.Compress(sXMLResult);

            return sXMLResult;

        }


        [WebMethod(Description = "Get Proveedores de Ganado")]
        public String GetProveedoresDeGanado()
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT * FROM gan_Proveedores order by Nombre ASC";
            DataSet ds = new DataSet();
            if (!this.FillDataSet(comm, out ds))
            {
            }
            String sXMLResult = this.DataSetToXML(ds);
            sXMLResult = BasculaGaribay.CompressUtils.Compress(sXMLResult);
            return sXMLResult;
        }

        [WebMethod(Description = "Get Proveedores")]
        public string GetProveedores()
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT * FROM Proveedores order by Nombre ASC";
            DataSet ds = new DataSet();
            if (!this.FillDataSet(comm, out ds))
            {
            }
            ds.Tables[0].TableName = "Proveedores";
            String sXMLResult = this.DataSetToXML(ds);
            sXMLResult = BasculaGaribay.CompressUtils.Compress(sXMLResult);
            return sXMLResult;
        }


        [WebMethod(Description = "Get  Clientes de venta")]
        public String GetClientesVentasWithTODOS()
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT '  TODOS' AS nombre, 0 AS clienteventaID  UNION SELECT nombre, clienteventaID FROM ClientesVentas ORDER BY nombre ASC";

            DataSet ds = new DataSet();
            if (!this.FillDataSet(comm, out ds))
            {
            }
            String sXMLResult = this.DataSetToXML(ds);
            sXMLResult = BasculaGaribay.CompressUtils.Compress(sXMLResult);

            return sXMLResult;

        }

        [WebMethod(Description = "Get Productos")]
        public String GetProductosBoletas()
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT 0 AS productoID, '  TODOS' AS Nombre UNION SELECT productoID, Nombre FROM Productos order by Nombre";
            DataSet ds = new DataSet();
            if (!this.FillDataSet(comm, out ds))
            {
            }
            String sXMLResult = this.DataSetToXML(ds);
            sXMLResult = BasculaGaribay.CompressUtils.Compress(sXMLResult);

            return sXMLResult;
            //return ds.Tables[0];

        }


        [WebMethod(Description = "Get Bodegas")]
        public String GetBodegas()
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT bodegaID, bodega FROM Bodegas order by bodega";
            DataSet ds = new DataSet();
            if (!this.FillDataSet(comm, out ds))
            {
            }
            String sXMLResult = this.DataSetToXML(ds);
            sXMLResult = BasculaGaribay.CompressUtils.Compress(sXMLResult);

            return sXMLResult;
            //return ds.Tables[0];
        }

        [WebMethod(Description = "Get Bodegas for cmb filters")]
        public String GetBodegasBoletas()
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT 0 as bodegaID, '  TODAS' AS bodega UNION SELECT bodegaID, bodega FROM Bodegas order by bodega";
            DataSet ds = new DataSet();
            if (!this.FillDataSet(comm, out ds))
            {
            }
            String sXMLResult = this.DataSetToXML(ds);
            sXMLResult = BasculaGaribay.CompressUtils.Compress(sXMLResult);

            return sXMLResult;
            //return ds.Tables[0];
        }


        internal String QueryBoletas(String fields, int iSessionID)
        {
            this.UpdateLastActivity(iSessionID);
            String sXMLResult = "";
            
            StringBuilder sCommandText = new StringBuilder();
            sCommandText.Append("SELECT * FROM vBoletas where " + fields);
            try
            {
             
                SqlCommand comm = new SqlCommand(sCommandText.ToString());

                DataSet ds;
                if (this.FillDataSet(comm, out ds))
                {
                    sXMLResult = this.DataSetToXML(ds);
                }
            }
            catch (Exception exc) {
                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.SELECT, sacaIDUsuario(iSessionID.ToString()), exc.Message, "Error enviado desde Bascula, Error al tratar de seleccionar las boletas");
            }
            return sXMLResult;
        }

        [WebMethod(Description = "Query Boletas Pendientes from DB")]
        public String QueryBoletasPendientes(int iSessionID)
        {
            this.UpdateLastActivity(iSessionID);
            String sXMLResult = "";
            StringBuilder sCommandText = new StringBuilder();
            sCommandText.Append("SELECT     Boletas.Ticket, Boletas.boletaID, Ciclos.CicloName FROM Boletas INNER JOIN Ciclos ON Boletas.cicloID = Ciclos.cicloID where Boletas.pesodesalida = 0 AND Boletas.CICLOID >4 AND Boletas.BodegaID = 1 ");
            try
            {
                SqlCommand comm = new SqlCommand(sCommandText.ToString());
                DataSet ds;
                if (this.FillDataSet(comm, out ds))
                {
                    sXMLResult = this.DataSetToXML(ds);
                    sXMLResult = BasculaGaribay.CompressUtils.Compress(sXMLResult);
                }
            }
            catch (Exception exc)
            {
                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.SELECT, sacaIDUsuario(iSessionID.ToString()), exc.Message, "Error enviado desde Bascula, Error al tratar de seleccionar las boletas");
            }
            return sXMLResult;
        }

        internal String QueryProductores(string fields, int iSessionID)
        {
            this.UpdateLastActivity(iSessionID);
            String sXMLResult = "";
            StringBuilder sCommandText = new StringBuilder();
            try
            {
                sCommandText.Append("SELECT ");
                sCommandText.Append(fields);
                
                sCommandText.Append(" FROM Productores INNER JOIN Sexo ON Productores.sexoID = Sexo.sexoID INNER JOIN Estados ON Productores.estadoID = Estados.estadoID INNER JOIN EstadosCiviles ON Productores.estadocivilID = EstadosCiviles.estadoCivilID INNER JOIN Regimenes ON Productores.regimenID = Regimenes.regimenID  order by apaterno, amaterno, nombre");

                // add conditions,

                //add order

                SqlCommand comm = new SqlCommand(sCommandText.ToString());
                DataSet ds;
                if (this.FillDataSet(comm, out ds))
                {
                    sXMLResult = this.DataSetToXML(ds);
                }
            }catch(Exception exc){

                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.SELECT, sacaIDUsuario(iSessionID.ToString()), exc.Message, "Error enviado desde Bascula, Error al tratar de seleccionar los productores");
            }


            return sXMLResult;
        }

        private String DataSetToXML(DataSet ds)
        {   
            StringWriter tw = new StringWriter();
            ds.WriteXml(tw, XmlWriteMode.WriteSchema);
            return tw.ToString();
        }

        internal bool FillDataSet(SqlCommand comm, out DataSet ds)
        {
            bool res = false;
            ds = new DataSet();
            SqlConnection conn = new SqlConnection(this.ConnectionInfo);
            try
            {
                comm.Connection = conn;
                SqlDataAdapter sqlDA = new SqlDataAdapter(comm);
                sqlDA.Fill(ds);
                res = true;
            }
            catch (System.Exception ex)
            {
            	res = false;
            }
            finally 
            {
                conn.Close();
            }

            return res;
        }

        [WebMethod(Description = "Get Régimenes")]
       public bool GetRegimenes(out DataTable dtRegimenes)
        {
            Boolean boleano=false;
            SqlCommand comm = new SqlCommand();
            dtRegimenes = new DataTable();
            comm.CommandText = "SELECT regimenID, Regimen FROM Regimenes order by Regimen";
            DataSet ds = new DataSet();
            if (!this.FillDataSet(comm, out ds))
            {
                return boleano;
            }
            dtRegimenes=ds.Tables[0];
            boleano=true;
            return boleano;
        }
        
        [WebMethod(Description = "Get Estados Civiles")]
         public bool GetEstadoCiviles(out DataTable dtEstadosCiviles)
        {
            dtEstadosCiviles = new DataTable();
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT estadoCivilID, EstadoCivil FROM EstadosCiviles order by EstadoCivil";
            DataSet ds = new DataSet();
            if (!this.FillDataSet(comm, out ds))
            {
                return false;
            }
            dtEstadosCiviles=ds.Tables[0];
            return true;
        }


        
            [WebMethod(Description = "Get productores para cmb")]
        public String GetProductorescmb()
        {
            SqlCommand comm = new SqlCommand();

            comm.CommandText = "SELECT productorID, apaterno + ' ' + amaterno + ' ' + nombre as Nombre FROM Productores order by Nombre";
            DataSet ds = new DataSet();
            if (!this.FillDataSet(comm, out ds))
            {
            }
            String sXMLResult = this.DataSetToXML(ds);
            sXMLResult = BasculaGaribay.CompressUtils.Compress(sXMLResult);

            return sXMLResult;
            //return ds.Tables[0];

        }

        [WebMethod(Description = "Get Estados ")]
        public String GetEstados()
        {
            String sXMLResult = "";
            
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT estadoID, estado FROM Estados order by estado";
            DataSet ds = new DataSet();
            if (!this.FillDataSet(comm, out ds))
            {
            }
            ds.Tables[0].TableName = "Estados";
            sXMLResult = this.DataSetToXML(ds);
            sXMLResult = BasculaGaribay.CompressUtils.Compress(sXMLResult);

                            
            return sXMLResult;
            //return ds.Tables[0];
            
            
        }






        [WebMethod(Description = "Insert Productor")]
        public bool InsertPro(String[] datos, String SessionID, out String exc)
        {

            int sessionID = int.Parse(SessionID);
            
           // datos[0] = this.txtPaterno.Text.ToUpper();
           //datos[1] = this.txtMaterno.Text.ToUpper();
           //datos[2] = this.txtNombre.Text.ToUpper();
           //datos[3] = this.dtPckrfecha.Text.ToUpper();
           //datos[4] = this.txtIfe.Text.ToUpper();
           //datos[5] = this.txtcurp.Text.ToUpper();
           //datos[6] = this.txtDomicilio.Text.ToUpper();
           //datos[7] = this.txtPoblacion.Text.ToUpper();
           //datos[8] = this.txtMunicipio.Text.ToUpper();
           //datos[9] = this.cmbEstado.SelectedIndex.ToString().ToUpper();
           //datos[10] = this.txtCp.Text;
           //datos[11] = this.txtRFC.Text.ToUpper();
           // if (rdFemenino.Checked)
           //    datos[12] = "1";
           //else
           //    datos[12] = "2";
           //datos[13] = this.txtTelefono.Text;
           //datos[14] = this.txtTtrabajo.Text;
           //datos[15] = this.txtCel.Text;
           //datos[16] = this.txtFax.Text;
           //datos[17] = this.txtEmail.Text.ToUpper();
           //datos[18] = this.cmbEstadoCivil.SelectedIndex.ToString();
           //datos[19] = this.cmbRegimen.SelectedIndex.ToString();
           //datos[20] = this.txtCodigoboletafile.Text;
           //datos[21] = WSUtils.myNow.ToShortDateString();
           //datos[22] = WSUtils.myNow.ToShortDateString();

            this.UpdateLastActivity(sessionID);
            string sqlQuery = "INSERT INTO Productores (apaterno, amaterno, nombre, fechanacimiento,";
            sqlQuery += " IFE, CURP, domicilio, poblacion, municipio, estadoID, CP, RFC, sexoID, telefono, telefonotrabajo, celular, fax, email, estadocivilID, regimenID, codigoBoletasFile, storeTS, updateTS, colonia, conyugue) ";
            sqlQuery += " VALUES(@apaterno,@amaterno,@nombre,@fechanacimiento, ";
            sqlQuery += "@IFE,@CURP,@domicilio,@poblacion,@municipio,@estadoID,@CP,@RFC,@sexoID,@telefono,@telefonotrabajo,@celular,@fax,@email,@estadocivilID,@regimenID, @codigoBoletasFile, @storeTS, @updateTS, @colonia, @conyugue) ";
            SqlConnection sqlCon = new SqlConnection(this.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(sqlQuery, sqlCon);

            try
            {
                sqlCon.Open();

                cmdIns.Parameters.Add("@apaterno", SqlDbType.NVarChar).Value = datos[0];
                cmdIns.Parameters.Add("@amaterno", SqlDbType.NVarChar).Value = datos[1];
                cmdIns.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = datos[2];
                cmdIns.Parameters.Add("@fechanacimiento", SqlDbType.DateTime).Value = WSUtils.converttoshortFormatfromdbFormat(datos[3]);
                cmdIns.Parameters.Add("@IFE", SqlDbType.NVarChar).Value = datos[4];
                cmdIns.Parameters.Add("@CURP", SqlDbType.NVarChar).Value = datos[5];
                cmdIns.Parameters.Add("@domicilio", SqlDbType.NText).Value = datos[6];
                cmdIns.Parameters.Add("@poblacion", SqlDbType.NVarChar).Value = datos[7];
                cmdIns.Parameters.Add("@municipio", SqlDbType.NVarChar).Value = datos[8];
                cmdIns.Parameters.Add("@estadoID", SqlDbType.Int).Value = int.Parse(datos[9]);
                cmdIns.Parameters.Add("@CP", SqlDbType.NVarChar).Value = datos[10];
                cmdIns.Parameters.Add("@RFC", SqlDbType.NVarChar).Value = datos[11];
                cmdIns.Parameters.Add("@sexoID", SqlDbType.Int).Value = int.Parse(datos[12]);
                cmdIns.Parameters.Add("@telefono", SqlDbType.NVarChar).Value = datos[13];
                cmdIns.Parameters.Add("@telefonotrabajo", SqlDbType.NVarChar).Value = datos[14];
                cmdIns.Parameters.Add("@celular", SqlDbType.NVarChar).Value = datos[15];
                cmdIns.Parameters.Add("@fax", SqlDbType.NVarChar).Value = datos[16];
                cmdIns.Parameters.Add("@email", SqlDbType.NVarChar).Value = datos[17];
                cmdIns.Parameters.Add("@estadocivilID", SqlDbType.Int).Value = int.Parse(datos[18]);
                cmdIns.Parameters.Add("@regimenID", SqlDbType.Int).Value = int.Parse(datos[19]);
                cmdIns.Parameters.Add("@codigoBoletasFile", SqlDbType.VarChar).Value = datos[20];
                cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = WSUtils.myNow;
                cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = WSUtils.myNow;
                cmdIns.Parameters.Add("@colonia", SqlDbType.VarChar).Value = datos[21];
                cmdIns.Parameters.Add("@conyugue", SqlDbType.VarChar).Value = datos[22];

            
                if(cmdIns.ExecuteNonQuery() == 1)
                {
                    if(bool.Parse(datos[23]))
                    {
                        try
                        {
                            cmdIns.Parameters.Clear();
                            cmdIns.CommandText = "INSERT INTO ClientesVentas "
                                + " (nombre, domicilio, ciudad, telefono, estadoID, RFC, userID, colonia, CP) "
                                + " VALUES (@nombre,@domicilio,@ciudad,@telefono,@estadoID,@RFC,@userID,@colonia,@CP)";
                            cmdIns.Parameters.Add("@nombre", SqlDbType.VarChar).Value = (datos[2] + " " + datos[1] + " " + datos[0]).Trim();
                            cmdIns.Parameters.Add("@domicilio", SqlDbType.VarChar).Value = datos[6];
                            cmdIns.Parameters.Add("@ciudad", SqlDbType.VarChar).Value = datos[8];
                            cmdIns.Parameters.Add("@telefono", SqlDbType.VarChar).Value = datos[13];
                            cmdIns.Parameters.Add("estadoID", SqlDbType.Int).Value = int.Parse(datos[9]);
                            cmdIns.Parameters.Add("@RFC", SqlDbType.NVarChar).Value = datos[11];
                            cmdIns.Parameters.Add("@userID", SqlDbType.NVarChar).Value = this.sacaIDUsuario(sessionID.ToString());
                            cmdIns.Parameters.Add("@colonia", SqlDbType.VarChar).Value = datos[21];
                            cmdIns.Parameters.Add("@CP", SqlDbType.NVarChar).Value = datos[10];
                            if(cmdIns.ExecuteNonQuery() == 1)
                            {
                                this.LogUserSessionRecord(typeModulo.WSPRODUCTORES, typeUserActions.INSERT, this.sacaIDUsuario(sessionID.ToString()), "EL USUARIO " + this.sacaNombreUsuarioDeWSsessionID(sessionID.ToString()) + " INSERTÓ EL CLIENTE DE VENTA " + datos[0] + " " + datos[1] + " " + datos[2]);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            LogMessage(typeLogMessage.CRITICAL, typeUserActions.INSERT, this.sacaIDUsuario(sessionID.ToString()), "no se pudo agregar productor como cliente de venta: " + ex.ToString(), "BASCULA");
                        }

                    }


                    if (bool.Parse(datos[24]))
                    {
                        try
                        {
                            cmdIns.Parameters.Clear();
                            cmdIns.CommandText = "INSERT INTO gan_Proveedores "
                                + " (Nombre, direccion, ciudad, estadoID, userID, RFC) "
                                + " VALUES (@Nombre,@direccion,@ciudad,@estadoID,@userID,@RFC)";
                            cmdIns.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = (datos[2] + " " + datos[1] + " " + datos[0]).Trim();
                            cmdIns.Parameters.Add("@direccion", SqlDbType.VarChar).Value = datos[6];
                            cmdIns.Parameters.Add("@ciudad", SqlDbType.VarChar).Value = datos[8];
                            cmdIns.Parameters.Add("estadoID", SqlDbType.Int).Value = int.Parse(datos[9]);
                            cmdIns.Parameters.Add("@RFC", SqlDbType.NVarChar).Value = datos[11];
                            cmdIns.Parameters.Add("@userID", SqlDbType.NVarChar).Value = this.sacaIDUsuario(sessionID.ToString());
                            if (cmdIns.ExecuteNonQuery() == 1)
                            {
                                this.LogUserSessionRecord(typeModulo.WSPRODUCTORES, typeUserActions.INSERT, this.sacaIDUsuario(sessionID.ToString()), "EL USUARIO " + this.sacaNombreUsuarioDeWSsessionID(sessionID.ToString()) + " INSERTÓ EL PROVEEDOR DE GANADO " + datos[0] + " " + datos[1] + " " + datos[2]);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            LogMessage(typeLogMessage.CRITICAL, typeUserActions.INSERT, this.sacaIDUsuario(sessionID.ToString()), "no se pudo agregar productor como cliente de venta: " + ex.ToString(), "BASCULA");
                        }

                    }
                
                }

                this.LogUserSessionRecord(typeModulo.WSPRODUCTORES, typeUserActions.INSERT, this.sacaIDUsuario(sessionID.ToString()), "EL USUARIO " + this.sacaNombreUsuarioDeWSsessionID(sessionID.ToString()) + " INSERTÓ EL PRODUCTOR " + datos[0] + " " + datos[1] + " " + datos[2]);
                
            }
            catch (Exception err3)
            {
                exc = err3.Message;
                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.INSERT, sacaIDUsuario(sessionID.ToString()), err3.Message, "Error enviado desde Bascula, Error al tratar de insertar un productor");
                return false;

//                 
            }
            finally
            {
                sqlCon.Close();
            }
            exc = "";
            return true;
        }

        private int sacaIDUsuario(String idsession){

            String sqlQuery = "SELECT UserID FROM WSSessions WHERE WSSessionID=@WSSessionID";
            SqlConnection sqlCon = new SqlConnection(this.ConnectionInfo);
            SqlCommand cmdSel = new SqlCommand(sqlQuery, sqlCon);
            SqlDataReader rd;
            int iduser = -1;
            
            try
            {
                sqlCon.Open();

                cmdSel.Parameters.Add("@WSSessionID", SqlDbType.Int).Value = int.Parse(idsession);
                
                rd = cmdSel.ExecuteReader();
                if(rd.HasRows&&rd.Read()){
                iduser=int.Parse(rd[0].ToString());
                }


                
                
            }
            catch (Exception exc)
            {
                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.SELECT, sacaIDUsuario(idsession.ToString()), exc.Message, "Error enviado desde Bascula, Error al tratar de seleccionar el id del usuario con el idSession");


            }finally
            {
                sqlCon.Close();
            }
            return iduser;

      }

        private String sacaNombreUsuarioDeuserID(String idsession)
        {

            String sqlQuery = "SELECT Nombre FROM Users WHERE userID=@userID";
            SqlConnection sqlCon = new SqlConnection(this.ConnectionInfo);
            SqlCommand cmdSel = new SqlCommand(sqlQuery, sqlCon);
            SqlDataReader rd;
            String nombreuser = ""; ;

            try
            {
                sqlCon.Open();

                cmdSel.Parameters.Add("@userID", SqlDbType.Int).Value = int.Parse(idsession);

                rd = cmdSel.ExecuteReader();
                if (rd.HasRows && rd.Read())
                {
                    nombreuser = rd[0].ToString();
                }




            }
            catch (Exception exc)
            {

                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.SELECT, sacaIDUsuario(idsession.ToString()), exc.Message, "Error enviado desde Bascula, Error al tratar de seleccionar el nombre del usuario con el userid");

            }
            finally
            {
                sqlCon.Close();
            }
            return nombreuser;

        }

        private String sacaNombreUsuarioDeWSsessionID(String idsession)
        {

            String sqlQuery = "SELECT Users.Nombre FROM Users INNER JOIN WSSessions ON WSSessions.userID=Users.userID WHERE WSSessions.WSSessionID=@WSSessionID";
            SqlConnection sqlCon = new SqlConnection(this.ConnectionInfo);
            SqlCommand cmdSel = new SqlCommand(sqlQuery, sqlCon);
            SqlDataReader rd;
            String nombreuser = ""; ;

            try
            {
                sqlCon.Open();

                cmdSel.Parameters.Add("@WSSessionID", SqlDbType.Int).Value = int.Parse(idsession);

                rd = cmdSel.ExecuteReader();
                if (rd.HasRows && rd.Read())
                {
                    nombreuser = rd[0].ToString();
                }




            }
            catch (Exception exc)
            {
                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.SELECT, sacaIDUsuario(idsession.ToString()), exc.Message, "Error enviado desde Bascula, Error al tratar de seleccionar el nombre del usuario con el idSession");


            }
            finally
            {
                sqlCon.Close();
            }
            return nombreuser;

        }

//         internal bool existeBoleta(String numeroBoleta) 
//         {
//             bool bResult = false;
//             String sqlQuery = "SELECT * FROM Boletas WHERE NumeroBoleta=@NumeroBoleta";
//             
//             SqlConnection sqlCon = new SqlConnection(this.ConnectionInfo);
//             SqlCommand cmdSel = new SqlCommand(sqlQuery, sqlCon);
// 
//             try
//             {
//                 sqlCon.Open();
//                 cmdSel.Parameters.Add("@NumeroBoleta", SqlDbType.Int).Value = int.Parse(numeroBoleta);
//                 SqlDataReader dr;
//                 dr = cmdSel.ExecuteReader();
//                 if (dr.HasRows) 
//                 { 
//                     bResult = false; 
//                 }
//             }
//             catch
//             {
//                 return false;
//             }
//             finally 
//             {
//                 sqlCon.Close();
//             }
//             return true;
//         }
        [WebMethod(Description = "Insert Boleta")]
        public bool InsertBoleta(String[] datos,  int iSessionID,out String exc, out int newID)
        {
            this.UpdateLastActivity(iSessionID);
            newID=-1;
            if(true/*this.existeBoleta(datos[14])*/){
            exc = "";
            String sqlQuery = "INSERT INTO Boletas (cicloID, userID, productorID, humedad,dctoHumedad, ";
            sqlQuery += "impurezas, totaldescuentos, pesonetoapagar, precioapagar, importe, placas, chofer, pagada, storeTS, updateTS, productoID, NumeroBoleta, Ticket, NombreProductor, FechaEntrada, PesadorEntrada, PesoDeEntrada, BasculaEntrada, FechaSalida, PesoDeSalida, PesadorSalida, BasculaSalida,pesonetoentrada, pesonetosalida, dctoImpurezas, dctoSecado, totalapagar, bodegaID, applyHumedad, applyImpurezas, applySecado, cabezasDeGanado, llevaFlete, deGranjaACorrales)";
            
            sqlQuery += "VALUES (@cicloID, @userID, @productorID, @humedad, @dctoHumedad, @impurezas, @totaldescuentos, @pesonetoapagar, @precioapagar, @importe, @placas, ";
            sqlQuery += "@chofer, @pagada, @storeTS, @updateTS, @productoID, @NumeroBoleta, @Ticket, @NombreProductor, @FechaEntrada, @PesadorEntrada, @PesoDeEntrada, @BasculaEntrada, @FechaSalida, @PesoDeSalida, @PesadorSalida, @BasculaSalida, @pesonetoentrada, @pesonetosalida, @dctoImpurezas, @dctoSecado, @totalapagar, @bodegaID, @applyHumedad, @applyImpurezas, @applySecado, @cabezasDeGanado, @llevaFlete, @deGranjaACorrales); select boletaID = SCOPE_IDENTITY();";
            
            SqlConnection sqlCon = new SqlConnection(this.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(sqlQuery, sqlCon);

            try
            {
                sqlCon.Open();

                cmdIns.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(datos[0]);
                cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = sacaIDUsuario(datos[1]);// int.Parse(datos[1]);
                cmdIns.Parameters.Add("@productorID", SqlDbType.Int).Value = int.Parse(datos[2]);
                cmdIns.Parameters.Add("@humedad", SqlDbType.Float).Value = float.Parse(datos[3]);
                cmdIns.Parameters.Add("@dctoHumedad", SqlDbType.Float).Value = float.Parse(datos[4]);
                cmdIns.Parameters.Add("@impurezas", SqlDbType.Float).Value = float.Parse(datos[5]);
                cmdIns.Parameters.Add("@totaldescuentos", SqlDbType.Float).Value = float.Parse(datos[6]);
                cmdIns.Parameters.Add("@pesonetoapagar", SqlDbType.Float).Value = float.Parse(datos[7]);
                cmdIns.Parameters.Add("@precioapagar", SqlDbType.Float).Value = float.Parse(datos[8]);
                cmdIns.Parameters.Add("@importe", SqlDbType.Float).Value = float.Parse(datos[9]);
                cmdIns.Parameters.Add("@placas", SqlDbType.VarChar).Value = datos[10];
                cmdIns.Parameters.Add("@chofer", SqlDbType.VarChar).Value = datos[11];
                if (datos[12] == "TRUE")
                    cmdIns.Parameters.Add("@pagada", SqlDbType.Bit).Value = true;
                else
                    cmdIns.Parameters.Add("@pagada", SqlDbType.Bit).Value = false;
                cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = WSUtils.myNow;
                cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = WSUtils.myNow;

                cmdIns.Parameters.Add("@productoID", SqlDbType.Int).Value = int.Parse(datos[13]);
                cmdIns.Parameters.Add("@NumeroBoleta", SqlDbType.VarChar).Value = datos[14];
                cmdIns.Parameters.Add("@Ticket", SqlDbType.VarChar).Value = datos[15];
                //cmdIns.Parameters.Add("@codigoClienteProvArchivo", SqlDbType.NVarChar).Value = datos[16];
                cmdIns.Parameters.Add("@NombreProductor", SqlDbType.VarChar).Value = datos[17];
                cmdIns.Parameters.Add("@FechaEntrada", SqlDbType.DateTime).Value = WSUtils.converttoLongDBFormat(datos[18]);
                cmdIns.Parameters.Add("@PesadorEntrada", SqlDbType.VarChar).Value = datos[19];
                cmdIns.Parameters.Add("@PesoDeEntrada", SqlDbType.Float).Value = float.Parse(datos[20]);
                cmdIns.Parameters.Add("@BasculaEntrada", SqlDbType.VarChar).Value = datos[21];
                cmdIns.Parameters.Add("@FechaSalida", SqlDbType.DateTime).Value = WSUtils.converttoLongDBFormat(datos[22]);
                
                cmdIns.Parameters.Add("@PesoDeSalida", SqlDbType.Float).Value = float.Parse(datos[23]);
                cmdIns.Parameters.Add("@PesadorSalida", SqlDbType.VarChar).Value = datos[24];
                cmdIns.Parameters.Add("@BasculaSalida", SqlDbType.VarChar).Value = datos[25];
                cmdIns.Parameters.Add("@pesonetoentrada", SqlDbType.Float).Value = float.Parse(datos[26]);

                cmdIns.Parameters.Add("@pesonetosalida", SqlDbType.Float).Value = float.Parse(datos[27]);
                cmdIns.Parameters.Add("@dctoImpurezas", SqlDbType.Float).Value = float.Parse(datos[28]);
                cmdIns.Parameters.Add("@dctoSecado", SqlDbType.Float).Value = float.Parse(datos[29]);
                cmdIns.Parameters.Add("@totalapagar", SqlDbType.Float).Value = float.Parse(datos[30]);
                cmdIns.Parameters.Add("@bodegaID", SqlDbType.Int).Value = int.Parse(datos[31]);

                cmdIns.Parameters.Add("@cabezasDeGanado", SqlDbType.Int).Value = int.Parse(datos[37]);

                if (datos[32] == "TRUE")
                    cmdIns.Parameters.Add("@applyHumedad", SqlDbType.Bit).Value = true;
                else
                    cmdIns.Parameters.Add("@applyHumedad", SqlDbType.Bit).Value = false;
                if (datos[33] == "TRUE")
                    cmdIns.Parameters.Add("@applyImpurezas", SqlDbType.Bit).Value = true;
                else
                    cmdIns.Parameters.Add("@applyImpurezas", SqlDbType.Bit).Value = false; 
                if (datos[34] == "TRUE")
                    cmdIns.Parameters.Add("@applySecado", SqlDbType.Bit).Value =true;
                else
                    cmdIns.Parameters.Add("@applySecado", SqlDbType.Bit).Value = false;

                cmdIns.Parameters.Add("@llevaFlete", SqlDbType.Bit).Value = bool.Parse(datos[39]);
                cmdIns.Parameters.Add("@deGranjaACorrales", SqlDbType.Bit).Value = bool.Parse(datos[40]);


                newID = int.Parse(cmdIns.ExecuteScalar().ToString());
                cmdIns.Parameters.Clear();
                if (datos[35] != "-1")
                {
                    cmdIns.CommandText = "insert into ClienteVenta_Boletas (clienteventaID,BoletaID) values (@clienteventaID,@BoletaID);";
                    cmdIns.Parameters.Add("@clienteventaID", SqlDbType.Int).Value = int.Parse(datos[35]);
                    cmdIns.Parameters.Add("@BoletaID", SqlDbType.Int).Value = newID;
                    cmdIns.ExecuteNonQuery();    
                }
                if (datos[38] != "-1")
                {
                    cmdIns.CommandText = "insert into gan_Proveedores_Boletas (ganProveedorID,BoletaID) values (@ganProveedorID,@BoletaID);";
                    cmdIns.Parameters.Add("@ganProveedorID", SqlDbType.Int).Value = int.Parse(datos[38]);
                    cmdIns.Parameters.Add("@BoletaID", SqlDbType.Int).Value = newID;
                    cmdIns.ExecuteNonQuery();
                }
                if (datos[41] != "-1")
                {
                    cmdIns.CommandText = "insert into boleta_proveedor (boletaID,proveedorID) "
                    + " values (@BoletaID, @proveedorID);";
                    cmdIns.Parameters.Add("@proveedorID", SqlDbType.Int).Value = int.Parse(datos[41]);
                    cmdIns.Parameters.Add("@BoletaID", SqlDbType.Int).Value = newID;
                    cmdIns.ExecuteNonQuery();
                }
                this.LogUserSessionRecord(typeModulo.WSBOLETAS, typeUserActions.INSERT, this.sacaIDUsuario(iSessionID.ToString()), "EL USUARIO " + this.sacaNombreUsuarioDeWSsessionID(iSessionID.ToString()) + " INSERTÓ LA BOLETA NÚMERO " + datos[14]);    

            }
            catch (Exception err3)
            {
                exc = err3.Message;
                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.INSERT, sacaIDUsuario(iSessionID.ToString()), err3.Message, "Error enviado desde Bascula, Error al tratar de insertar una boleta");
                return false;

                //                 
            }
            finally
            {
                sqlCon.Close();
            }
            exc = "";
            return true;
            }else{
                exc = "NO SE PUEDE AGREGAR LA BOLETA, LA BOLETA NÚMERO " + datos[14] + " YA SE ENCUENTRA EN EL SISTEMA";
                return false;
            }

        }

        [WebMethod(Description = "Update Boleta")]
        public bool UpdateBoleta(String[] datos, String id, int iSessionID, out String exc)
        {
            this.UpdateLastActivity(iSessionID);
            exc = "";
            String sqlQuery = "UPDATE  Boletas SET cicloID=@cicloID, userID=@userID, productorID = @productorID, humedad =@humedad, dctoHumedad=@dctoHumedad, ";
            sqlQuery += "impurezas=@impurezas, totaldescuentos=@totaldescuentos, pesonetoapagar=@pesonetoapagar, precioapagar=@precioapagar, importe=@importe, placas=@placas, chofer=@chofer, pagada=@pagada, storeTS=@storeTS, updateTS=@updateTS, productoID=@productoID, NumeroBoleta=@NumeroBoleta, Ticket=@Ticket, NombreProductor=@NombreProductor, FechaEntrada=@FechaEntrada, PesadorEntrada=@PesadorEntrada, PesoDeEntrada=@PesoDeEntrada, BasculaEntrada=@BasculaEntrada, FechaSalida=@FechaSalida, PesoDeSalida=@PesoDeSalida, PesadorSalida=@PesadorSalida, BasculaSalida=@BasculaSalida, ";
            sqlQuery += "pesonetoentrada=@pesonetoentrada, pesonetosalida=@pesonetosalida, dctoImpurezas=@dctoImpurezas, dctoSecado=@dctoSecado, totalapagar=@totalapagar, bodegaID=@bodegaID, applyHumedad=@applyHumedad, applyImpurezas=@applyImpurezas, applySecado=@applySecado, cabezasDeGanado=@cabezasDeGanado, llevaFlete=@llevaFlete, deGranjaACorrales = @deGranjaACorrales WHERE boletaID=" + id;
            
            SqlConnection sqlCon = new SqlConnection(this.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(sqlQuery, sqlCon);

            try
            {
                sqlCon.Open();

                cmdIns.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(datos[0]);
                cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = sacaIDUsuario(datos[1]);// int.Parse(datos[1]);
                cmdIns.Parameters.Add("@productorID", SqlDbType.Int).Value = int.Parse(datos[2]);
                cmdIns.Parameters.Add("@humedad", SqlDbType.Float).Value = float.Parse(datos[3]);
                cmdIns.Parameters.Add("@dctoHumedad", SqlDbType.Float).Value = float.Parse(datos[4]);
                cmdIns.Parameters.Add("@impurezas", SqlDbType.Float).Value = float.Parse(datos[5]);
                cmdIns.Parameters.Add("@totaldescuentos", SqlDbType.Float).Value = float.Parse(datos[6]);
                cmdIns.Parameters.Add("@pesonetoapagar", SqlDbType.Float).Value = float.Parse(datos[7]);
                cmdIns.Parameters.Add("@precioapagar", SqlDbType.Float).Value = float.Parse(datos[8]);
                cmdIns.Parameters.Add("@importe", SqlDbType.Float).Value = float.Parse(datos[9]);
                cmdIns.Parameters.Add("@placas", SqlDbType.VarChar).Value = datos[10];
                cmdIns.Parameters.Add("@chofer", SqlDbType.VarChar).Value = datos[11];
                if (datos[12] == "TRUE")
                    cmdIns.Parameters.Add("@pagada", SqlDbType.Bit).Value = true;
                else
                    cmdIns.Parameters.Add("@pagada", SqlDbType.Bit).Value = false;
                cmdIns.Parameters.Add("@storeTS", SqlDbType.DateTime).Value = WSUtils.myNow;
                cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = WSUtils.myNow;

                cmdIns.Parameters.Add("@productoID", SqlDbType.Int).Value = int.Parse(datos[13]);
                cmdIns.Parameters.Add("@NumeroBoleta", SqlDbType.VarChar).Value = datos[14];
                cmdIns.Parameters.Add("@Ticket", SqlDbType.VarChar).Value = datos[15];
                //cmdIns.Parameters.Add("@codigoClienteProvArchivo", SqlDbType.NVarChar).Value = datos[16];
                cmdIns.Parameters.Add("@NombreProductor", SqlDbType.VarChar).Value = datos[17];
                cmdIns.Parameters.Add("@FechaEntrada", SqlDbType.DateTime).Value = WSUtils.converttoLongDBFormat(datos[18]);
                cmdIns.Parameters.Add("@PesadorEntrada", SqlDbType.VarChar).Value = datos[19];
                cmdIns.Parameters.Add("@PesoDeEntrada", SqlDbType.Float).Value = float.Parse(datos[20]);
                cmdIns.Parameters.Add("@BasculaEntrada", SqlDbType.VarChar).Value = datos[21];
                cmdIns.Parameters.Add("@FechaSalida", SqlDbType.DateTime).Value = WSUtils.converttoLongDBFormat(datos[22]);

                cmdIns.Parameters.Add("@PesoDeSalida", SqlDbType.Float).Value = float.Parse(datos[23]);
                cmdIns.Parameters.Add("@PesadorSalida", SqlDbType.VarChar).Value = datos[24];
                cmdIns.Parameters.Add("@BasculaSalida", SqlDbType.VarChar).Value = datos[25];
                cmdIns.Parameters.Add("@pesonetoentrada", SqlDbType.Float).Value = float.Parse(datos[26]);

                cmdIns.Parameters.Add("@pesonetosalida", SqlDbType.Float).Value = float.Parse(datos[27]);
                cmdIns.Parameters.Add("@dctoImpurezas", SqlDbType.Float).Value = float.Parse(datos[28]);
                cmdIns.Parameters.Add("@dctoSecado", SqlDbType.Float).Value = float.Parse(datos[29]);
                cmdIns.Parameters.Add("@totalapagar", SqlDbType.Float).Value = float.Parse(datos[30]);
                cmdIns.Parameters.Add("@bodegaID", SqlDbType.Int).Value = int.Parse(datos[31]);

                cmdIns.Parameters.Add("@cabezasDeGanado", SqlDbType.Int).Value = int.Parse(datos[37]);

                if (datos[32] == "TRUE")
                    cmdIns.Parameters.Add("@applyHumedad", SqlDbType.Bit).Value = true;
                else
                    cmdIns.Parameters.Add("@applyHumedad", SqlDbType.Bit).Value = false;
                if (datos[33] == "TRUE")
                    cmdIns.Parameters.Add("@applyImpurezas", SqlDbType.Bit).Value = true;
                else
                    cmdIns.Parameters.Add("@applyImpurezas", SqlDbType.Bit).Value = false;
                if (datos[34] == "TRUE")
                    cmdIns.Parameters.Add("@applySecado", SqlDbType.Bit).Value = true;
                else
                    cmdIns.Parameters.Add("@applySecado", SqlDbType.Bit).Value = false;

                cmdIns.Parameters.Add("@llevaFlete", SqlDbType.Bit).Value = bool.Parse(datos[39]);
                cmdIns.Parameters.Add("@deGranjaACorrales", SqlDbType.Bit).Value = bool.Parse(datos[40]);

                cmdIns.ExecuteNonQuery();
                
                cmdIns.Parameters.Clear();
                cmdIns.Dispose();


                cmdIns.Parameters.Clear();
                cmdIns.CommandText = "delete from ClienteVenta_Boletas where BoletaID = @BoletaID;";
                cmdIns.CommandText += "delete from gan_Proveedores_Boletas where BoletaID = @BoletaID;";
                cmdIns.CommandText += "delete from boleta_proveedor where BoletaID = @BoletaID;";

                cmdIns.Parameters.Add("@BoletaID", SqlDbType.Int).Value = int.Parse(id);
                cmdIns.ExecuteNonQuery();

                cmdIns.Parameters.Clear();
                cmdIns.Parameters.Add("@BoletaID", SqlDbType.Int).Value = int.Parse(id);
                if (datos[35] != "-1")
                {
                    cmdIns.CommandText = "insert into ClienteVenta_Boletas (clienteventaID,BoletaID) values (@clienteventaID,@BoletaID);";
                    cmdIns.Parameters.Add("@clienteventaID", SqlDbType.Int).Value = int.Parse(datos[35]);
                    cmdIns.ExecuteNonQuery();
                }
                if (datos[38] != "-1")
                {
                    cmdIns.CommandText = "insert into gan_Proveedores_Boletas (ganProveedorID,BoletaID) values (@ganProveedorID,@BoletaID);";
                    cmdIns.Parameters.Add("@ganProveedorID", SqlDbType.Int).Value = int.Parse(datos[38]);
                    cmdIns.ExecuteNonQuery();
                }
                if (datos[41] != "-1")
                {
                    cmdIns.CommandText = "insert into boleta_proveedor (boletaID,proveedorID) "
                    + " values (@BoletaID, @proveedorID);";
                    cmdIns.Parameters.Add("@proveedorID", SqlDbType.Int).Value = int.Parse(datos[41]);
                    cmdIns.ExecuteNonQuery();
                }
                this.LogUserSessionRecord(typeModulo.WSBOLETAS, typeUserActions.UPDATE, this.sacaIDUsuario(iSessionID.ToString()), "EL USUARIO " + this.sacaNombreUsuarioDeWSsessionID(iSessionID.ToString()) + " MODIFICÓ LA BOLETA NÚMERO " + datos[15].ToString() + " id: " + id);    
            }
            catch (Exception err3)
            {
                exc = err3.Message;
               
                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.UPDATE, sacaIDUsuario(iSessionID.ToString()), err3.Message, "Error enviado desde Bascula, Error al tratar de modificar una boleta");
                return false;

                //                 
            }
            finally
            {
                sqlCon.Close();
            }
            exc = "";
            return true;
        }



        [WebMethod(Description = "update Productor")]
        public bool ModifyPro(String[] datos, String id, int iSessionID, out String exc)
        {
         
            
            
           // datos[0] = this.txtPaterno.Text.ToUpper();
           //datos[1] = this.txtMaterno.Text.ToUpper();
           //datos[2] = this.txtNombre.Text.ToUpper();
           //datos[3] = this.dtPckrfecha.Text.ToUpper();
           //datos[4] = this.txtIfe.Text.ToUpper();
           //datos[5] = this.txtcurp.Text.ToUpper();
           //datos[6] = this.txtDomicilio.Text.ToUpper();
           //datos[7] = this.txtPoblacion.Text.ToUpper();
           //datos[8] = this.txtMunicipio.Text.ToUpper();
           //datos[9] = this.cmbEstado.SelectedIndex.ToString().ToUpper();
           //datos[10] = this.txtCp.Text;
           //datos[11] = this.txtRFC.Text.ToUpper();
           // if (rdFemenino.Checked)
           //    datos[12] = "1";
           //else
           //    datos[12] = "2";
           //datos[13] = this.txtTelefono.Text;
           //datos[14] = this.txtTtrabajo.Text;
           //datos[15] = this.txtCel.Text;
           //datos[16] = this.txtFax.Text;
           //datos[17] = this.txtEmail.Text.ToUpper();
           //datos[18] = this.cmbEstadoCivil.SelectedIndex.ToString();
           //datos[19] = this.cmbRegimen.SelectedIndex.ToString();
           //datos[20] = this.txtCodigoboletafile.Text;
           //datos[21] = WSUtils.myNow.ToShortDateString();
           //datos[22] = WSUtils.myNow.ToShortDateString();
            this.UpdateLastActivity(iSessionID);
            string sqlQuery = "UPDATE Productores SET apaterno = @apaterno, amaterno= @amaterno, nombre = @nombre, fechanacimiento = @fechanacimiento, IFE = @IFE, CURP = @CURP, domicilio = @domicilio,";
            sqlQuery += " poblacion = @poblacion, municipio = @municipio, estadoID = @estadoID, CP = @CP, RFC = @RFC, sexoID = @SexoID, telefono = @telefono,  telefonotrabajo = @telefonotrabajo,";
            sqlQuery += " celular = @celular, estadocivilID = @estadocivilID, regimenID = @regimenID, email = @email, codigoBoletasFile = @codigoBoletasFile, updateTS = @updateTS WHERE productorID = @productorID";



            
            SqlConnection sqlCon = new SqlConnection(this.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(sqlQuery, sqlCon);

            try
            {
                sqlCon.Open();

                cmdIns.Parameters.Add("@apaterno", SqlDbType.NVarChar).Value = datos[0];
                cmdIns.Parameters.Add("@amaterno", SqlDbType.NVarChar).Value = datos[1];
                cmdIns.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = datos[2];
                cmdIns.Parameters.Add("@fechanacimiento", SqlDbType.DateTime).Value = WSUtils.converttoshortFormatfromdbFormat(datos[3]);
                cmdIns.Parameters.Add("@IFE", SqlDbType.NVarChar).Value = datos[4];
                cmdIns.Parameters.Add("@CURP", SqlDbType.NVarChar).Value = datos[5];
                cmdIns.Parameters.Add("@domicilio", SqlDbType.NText).Value = datos[6];
                cmdIns.Parameters.Add("@poblacion", SqlDbType.NVarChar).Value = datos[7];
                cmdIns.Parameters.Add("@municipio", SqlDbType.NVarChar).Value = datos[8];
                cmdIns.Parameters.Add("@estadoID", SqlDbType.Int).Value = int.Parse(datos[9]);
                cmdIns.Parameters.Add("@CP", SqlDbType.NVarChar).Value = datos[10];
                cmdIns.Parameters.Add("@RFC", SqlDbType.NVarChar).Value = datos[11];
                cmdIns.Parameters.Add("@sexoID", SqlDbType.Int).Value = int.Parse(datos[12]);
                cmdIns.Parameters.Add("@telefono", SqlDbType.NVarChar).Value = datos[13];
                cmdIns.Parameters.Add("@telefonotrabajo", SqlDbType.NVarChar).Value = datos[14];
                cmdIns.Parameters.Add("@celular", SqlDbType.NVarChar).Value = datos[15];
                cmdIns.Parameters.Add("@fax", SqlDbType.NVarChar).Value = datos[16];
                cmdIns.Parameters.Add("@email", SqlDbType.NVarChar).Value = datos[17];
                cmdIns.Parameters.Add("@estadocivilID", SqlDbType.Int).Value = int.Parse(datos[18]);
                cmdIns.Parameters.Add("@regimenID", SqlDbType.Int).Value = int.Parse(datos[19]);
                cmdIns.Parameters.Add("@codigoBoletasFile", SqlDbType.VarChar).Value = datos[20];
                cmdIns.Parameters.Add("@updateTS", SqlDbType.DateTime).Value = WSUtils.myNow;
                cmdIns.Parameters.Add("@productorID", SqlDbType.Int).Value = int.Parse(id);

            
            cmdIns.ExecuteNonQuery();
            this.LogUserSessionRecord(typeModulo.WSPRODUCTORES, typeUserActions.UPDATE, this.sacaIDUsuario(iSessionID.ToString()), "EL USUARIO " + this.sacaNombreUsuarioDeWSsessionID(iSessionID.ToString()) + " MODIFICÓ EL PRODUCTOR " + datos[0] + " " + datos[1] + " " + datos[2]);    
            
                
                
            }
            catch (Exception err3)
            {
                exc = err3.Message;
                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.UPDATE, sacaIDUsuario(iSessionID.ToString()), err3.Message, "Error enviado desde Bascula, Error al tratar de modificar un productor");
                return false;

//                 
            }
            finally
            {
                sqlCon.Close();
            }
            exc = "";
            return true;
        }

        internal bool productorSinBoletasOrLuquidaciones(int id, String productor,int iSessionID, out String mensaje)
        {
            string qrySel = "SELECT productorID FROM Boletas WHERE productorID=@productorID UNION SELECT productorID FROM Liquidaciones WHERE productorID=@productorID";
            SqlConnection sqlCon = new SqlConnection(this.ConnectionInfo);
            SqlCommand cmdSel = new SqlCommand(qrySel, sqlCon);
            SqlDataReader rd;
        
            try{
                cmdSel.Parameters.Add("@productorID", SqlDbType.NVarChar).Value = id;
                sqlCon.Open();
                rd = cmdSel.ExecuteReader();
                if(rd.HasRows){
                    mensaje = "El Productor " + productor + " no se ha podido Eliminar \n dado que tiene registro en Boletas y/o Liquidaciones";
                    return false;
                }
            }catch(Exception exc){
                mensaje = exc.Message;
                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.SELECT, sacaIDUsuario(iSessionID.ToString()), exc.Message, "Error enviado desde Bascula, Error al tratar de comprobar que el productor a eliminar no tenga liquidaciones ni boletas registradas");
                return false;
            }
            finally{
                sqlCon.Close();
            }
            mensaje = "";
            return true;
        }


        internal bool boletaSinLuquidacionesOrAnticipos(int id,int iSessionID, out String mensaje)
        {
            string qrySel = "SELECT boletaID FROM Boletas_Anticipos WHERE boletaID=@boletaID UNION SELECT boletaID FROM Liquidaciones_Boletas WHERE boletaID=@boletaID";            SqlConnection sqlCon = new SqlConnection(this.ConnectionInfo);
            SqlCommand cmdSel = new SqlCommand(qrySel, sqlCon);
            SqlDataReader rd;

            try
            {
                cmdSel.Parameters.Add("@boletaID", SqlDbType.Int).Value = id;
                sqlCon.Open();
                rd = cmdSel.ExecuteReader();
                if (rd.HasRows)
                {
                    mensaje = "La Boleta con el ID: " + id + " no se ha podido Eliminar \n dado que tiene registro en Liquidaciones y / o en Anticipos  ";
                    return false;
                }
            }
            catch (Exception exc)
            {
                mensaje = exc.Message;
                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.SELECT, sacaIDUsuario(iSessionID.ToString()), exc.Message, "Error enviado desde Bascula, Error al tratar de comprobar que la boleta a eliminar no tenga anticipos ni Liquidaciones");
                return false;
            }
            finally
            {
                sqlCon.Close();
            }
            mensaje = "";
            return true;
        }


        [WebMethod(Description = "Delete Productor")]
        public bool deletePro(int id, String productor, int iSessionID, out String mensaje)
        {
            this.UpdateLastActivity(iSessionID);
            if(this.productorSinBoletasOrLuquidaciones(id, productor, iSessionID, out mensaje)){



            string qryDel = "DELETE FROM Productores WHERE productorID=@productorID";
            SqlConnection sqlCon = new SqlConnection(this.ConnectionInfo);
            SqlCommand cmdDel = new SqlCommand(qryDel, sqlCon);

            try
            {

                sqlCon.Open();

                cmdDel.Parameters.Add("@productorID", SqlDbType.NVarChar).Value = id;
                
                cmdDel.ExecuteNonQuery();
                this.LogUserSessionRecord(typeModulo.WSPRODUCTORES, typeUserActions.DELETE, this.sacaIDUsuario(iSessionID.ToString()), "EL USUARIO " + this.sacaNombreUsuarioDeWSsessionID(iSessionID.ToString()) + " ELIMINÓ EL PRODUCTOR " + productor);    
                


            }
            catch (Exception err3)
            {
                mensaje = err3.Message;
                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.DELETE, sacaIDUsuario(iSessionID.ToString()), err3.Message, "Error enviado desde Bascula, Error al tratar de eliminar un Productor");
                return false;

                //                 
            }
            finally
            {
                sqlCon.Close();
            }

            
            mensaje = "El Productor "+ productor+ " ha sido Eliminado Satisfactoriamente";
            return true;

                }
            else{


                return false;

            }
        }

        [WebMethod(Description = "Delete Boleta")]
        public bool deleteBol(int id, int iSessionID , out String mensaje)
        {
            this.UpdateLastActivity(iSessionID);


            if (this.boletaSinLuquidacionesOrAnticipos(id,iSessionID, out mensaje))
            {

                string qryDel = "DELETE FROM Boletas WHERE boletaID=@boletaID";
                SqlConnection sqlCon = new SqlConnection(this.ConnectionInfo);
                SqlCommand cmdDel = new SqlCommand(qryDel, sqlCon);

                try
                {
                    sqlCon.Open();
                    cmdDel.Parameters.Add("@boletaID", SqlDbType.NVarChar).Value = id;
                    cmdDel.ExecuteNonQuery();
                    cmdDel.Parameters.Clear();
                    cmdDel.CommandText = "DELETE FROM ClienteVenta_Boletas WHERE boletaID=@boletaID";
                    cmdDel.Parameters.Add("@boletaID", SqlDbType.Int).Value = id;
                    cmdDel.ExecuteNonQuery();
                    cmdDel.CommandText = "DELETE FROM boleta_proveedor WHERE boletaID=@boletaID";
                    cmdDel.Parameters.Add("@boletaID", SqlDbType.Int).Value = id;
                    cmdDel.ExecuteNonQuery();
                    cmdDel.CommandText = "DELETE FROM gan_Proveedores_Boletas WHERE boletaID=@boletaID";
                    cmdDel.Parameters.Add("@boletaID", SqlDbType.Int).Value = id;
                    cmdDel.ExecuteNonQuery();
                    this.LogUserSessionRecord(typeModulo.WSBOLETAS, typeUserActions.DELETE, this.sacaIDUsuario(iSessionID.ToString()), "EL USUARIO " + this.sacaNombreUsuarioDeWSsessionID(iSessionID.ToString()) + " ELIMINÓ LA BOLETA CON EL ID " + id.ToString());    
                }
                catch (Exception err3)
                {
                    mensaje = err3.Message;
                    this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.DELETE, sacaIDUsuario(iSessionID.ToString()), err3.Message, "Error enviado desde Bascula, Error al tratar de eliminar una boleta");
                    return false;
                }
                finally
                {
                    sqlCon.Close();
                }


                mensaje = "El la Boleta con el ID: " + id + " ha sido Eliminada Satisfactoriamente";
                return true;
            }
            else {
                return false;
            }

            
        }


        [WebMethod(Description = "Delete Proveedor")]
        public bool deleteProv(int pID, int iSessionID, out String mensaje)
        {            
            bool bResult = false;
            try
            {
                this.UpdateLastActivity(iSessionID);
                Proveedores pTodel = Proveedores.Get(pID);
                if (pTodel != null)
                {
                    pTodel.Delete();
                    bResult = true;
                    mensaje = "Proveedor eliminado exitosamente.";
                }
                else
                    mensaje = "Existe mas de un proveedor con esa informacion";
            }
            catch (System.Exception ex)
            {
                mensaje = ex.ToString();
            }
            return bResult;
        }
        
        [WebMethod(Description = "Add/Update Proveedor")]
        public string WorkWithProveedor(String data)
        {
            Proveedores[] provs = new Proveedores[1];
            if (data != null && data.Length >0)
            {
                DataSet ds = new DataSet();
                if (WSUtils.LoadXMLinDataSet(CompressUtils.Decompress(data), out ds))
                {
                    provs = Proveedores.MapFrom(ds);
                    if (provs.Length == 1)
                    {
                        Proveedores p = provs[0];
                        if (p.ProveedorID == null || p.ProveedorID <= 0)
                        {
                            p.Insert();
                        }
                        else
                        {
                            p.Update();
                        }
                    }
                }
            }
            return BasculaGaribay.CompressUtils.Compress(this.DataSetToXML(provs[0].MapTo()));
        }


        [WebMethod(Description = "Get info Productor ")]
        public bool GetProductorWS(String id, int iSessionID, out String[] datos1)
        {
            bool bResult = false;
            this.UpdateLastActivity(iSessionID);
            string qrySel = "SELECT *  FROM Garibay.dbo.Productores WHERE Productores.productorID = ";
            qrySel += id;
            
            SqlDataReader datos;
            datos1 = new String[24];
            SqlConnection sqlCon = new SqlConnection(this.ConnectionInfo);
            SqlCommand cmdSel = new SqlCommand(qrySel, sqlCon);
            try
            {
                sqlCon.Open();
                datos = cmdSel.ExecuteReader();
                datos.Read();
                for (int i = 0; i < 24; i++)
                    datos1[i] = datos[i].ToString();               
                bResult = true;
            }
            catch(Exception ex)
            {
                this.LogMessage(typeLogMessage.CRITICAL, typeUserActions.SELECT, sacaIDUsuario(iSessionID.ToString()), ex.Message, "Error enviado desde Bascula, Error al leer los datos de un productor");
            }
            finally
            {
                sqlCon.Close();
            }
            return bResult;
        }


        public enum typeModulo{
            WSLOGIN=0,
            
            WSPRODUCTORES=2, 
            WSBOLETAS=21,
            
        }


        public enum typeLogMessage
        {
            CRITICAL = 0,
            INFO,
            DEBUG


        }


        public enum typeUserActions
        {
            SELECT = 0,
            INSERT, UPDATE, DELETE, LOGIN

        }


        public void LogUserSessionRecord(typeModulo Modulo, typeUserActions UserAction, int iduser, string description)
        {
            string qryIns = "INSERT INTO UserSessionRecords(userID,moduleID,useractionID,timestamp,description) VALUES (@userID,@moduleID, @useractionID, @timestamp, @description)";
            SqlConnection conGaribay = new SqlConnection(this.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            try
            {
                cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = iduser;
                cmdIns.Parameters.Add("@moduleID", SqlDbType.Int).Value = (int)Modulo;
                cmdIns.Parameters.Add("@useractionID", SqlDbType.Int).Value = (int)UserAction;
                cmdIns.Parameters.Add("@timestamp", SqlDbType.DateTime).Value = WSUtils.myNow;
                cmdIns.Parameters.Add("@description", SqlDbType.Text).Value = description;
                conGaribay.Open();
                cmdIns.ExecuteNonQuery();
                //int numregistros = cmdIns.ExecuteNonQuery();
                //if (numregistros != 1)
                //{
                //    throw new Exception("NO HA SIDO POSIBLE LOGGEAR UNA ACCIÓN EN LA BASE DE DATOS.");
                //}

            }
            catch (Exception err3)
            {
                
                this.LogMessage(typeLogMessage.CRITICAL, UserAction, iduser, err3.Message, "Error enviado desde LogUserSessionRecords de la Bascula");

            }
            finally
            {
                conGaribay.Close();
            }

        }

        public void LogMessage(typeLogMessage MessageID, typeUserActions UserAction, int iduser, string description, string urlpage)
        {
            string qryIns = "INSERT INTO LogMessages (logmsgTypeID,userID, useractionID, urlpage, datestamp,  message) VALUES (@logmsgTypeID,@userID, @useractionID, @urlpage, @datestamp,  @message)";
            SqlConnection conGaribay = new SqlConnection(this.ConnectionInfo);
            SqlCommand cmdIns = new SqlCommand(qryIns, conGaribay);
            try
            {
                cmdIns.Parameters.Add("@logmsgTypeID", SqlDbType.Int).Value = (int)MessageID;
                cmdIns.Parameters.Add("@userID", SqlDbType.Int).Value = iduser;
                cmdIns.Parameters.Add("@useractionID", SqlDbType.Int).Value = (int)UserAction;
                cmdIns.Parameters.Add("@urlpage", SqlDbType.NVarChar).Value = urlpage;
                cmdIns.Parameters.Add("@datestamp", SqlDbType.DateTime).Value = WSUtils.myNow;
                //                 if (MessageID == typeLogMessage.CRITICAL)
                //                 {
                //                     description += +" stackTrace: " + Environment.StackTrace;
                //                 }
                cmdIns.Parameters.Add("@message", SqlDbType.Text).Value = description;
                conGaribay.Open();
                int numregistros = cmdIns.ExecuteNonQuery();
                if (numregistros != 1)
                {
                    throw new Exception("NO HA SIDO POSIBLE LOGGEAR UNA ACCIÓN EN LA BASE DE DATOS.");
                }
                //string cadenaaloggear = "SE GENERÓ UN MENSAJE DEL TIPO: ";
                //cadenaaloggear += MessageID.ToString();
                //cadenaaloggear += ", CUANDO EL USUARIO CON EL ID: ";
                //cadenaaloggear += iduser.ToString();
                //cadenaaloggear += " "; cadenaaloggear += UserAction.ToString();
                //cadenaaloggear += ". EN LA PÁGINA: ";
                //cadenaaloggear += urlpage;
                //cadenaaloggear += ". EL MENSAJE FUE: "; cadenaaloggear += description;
               // this.LogFile(cadenaaloggear);

            }

            catch (Exception exc)
            {

                string cadenaaloggear = "SE DIO LA SIGUIENTE EXCEPCION AL TRATAR DE LOGGEAR UN MENSAJE: ";
                cadenaaloggear += exc.Message;
                cadenaaloggear += " EL MENSAJE ERA DEL TIPO: ";
                cadenaaloggear += MessageID.ToString();
                cadenaaloggear += ", CUANDO EL USUARIO CON EL ID: ";
                cadenaaloggear += iduser.ToString();
                cadenaaloggear += " "; cadenaaloggear += UserAction.ToString();
                cadenaaloggear += ". EN LA PÁGINA: ";
                cadenaaloggear += urlpage;
                cadenaaloggear += ". EL MENSAJE FUE: "; cadenaaloggear += description;
                //this.LogFile(cadenaaloggear);

            }
            finally
            {
                conGaribay.Close();
            }

        }


    }
}
