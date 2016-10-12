using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Authentication;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Web.Security;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Net;
using Microsoft.Synchronization.Data;
using System.Data.SqlServerCe;
using System.Configuration;
using System.Data.Linq;



namespace BasculaGaribay
{
    public class WSConnector: WSGaribay.Servicios
    {
        private static WSConnector _instance = new WSConnector();
        private TimeSpan TimeValidCache = new TimeSpan(16, 0, 0);

        private List<string[]> BoletasData = new List<string[]>();

        private bool _IsOfflineMode = false;
        
        public bool IsOfflineMode
        {
            get { return _IsOfflineMode; }
            set 
            { 
                _IsOfflineMode = value;
                if (this._IsOfflineMode)
                {
                    this.LoadCacheForOfflineMode();
                    this.LoginOffline();
                }
            }
        }
        
        private WSConnector()
        {
        }
        
        public void Inicializa()
        {
            //verify connection
            //if (this.TestConnection())
            {
                //If there is connection with DB then we query all the information from DB
                /*PRODUCTORES = 1,
        PRODUCTOS,
        BODEGAS,
        PRODUCTORESFORCMB,
        CICLOS,
        CLIENTESVENTAS,
        PROVEEDORESGANADO,
        BOLETASPENDIENTES*/
                //this.GetAllProductoresforCmb()
            }
            //verify data.
        }

        #region private variables
        private String sUsername = "";
        private String sPassword = "";
        private String sNombre = "";
        private int _idsession =-1;
        public int Idsession
        {
            get { return _idsession; }
            set { _idsession = value; }
        }
        private int _UserID = -1;
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public bool sessionValida = false;
        #endregion

        #region PROPERTIES DEFINITION
        public  String Username
        {
            get
            {
                return this.sUsername;
            }
            set
            { 
                this.sUsername = value;
            }
        }
        public bool SessionValida
        {
            get
            {
                return this.sessionValida;
            }
            set
            {
                this.sessionValida = value;
            }
        }
        
        public String Password
        {
            get 
            {
                return this.sPassword;
            }
            set
            {
                
                //my implementation of String to SHA1 string
//                 System.Security.Cryptography.SHA1Managed m = new System.Security.Cryptography.SHA1Managed();
//                 String temp = value;
//                 byte[] b = UTF8Encoding.UTF8.GetBytes(temp);
//                 b = m.ComputeHash(b);
//                 StringBuilder sb = new StringBuilder();
//                 foreach(Byte byt in b)
//                 {
//                     sb.AppendFormat("{0:x}",byt);
//                 }
//                 temp = sb.ToString().ToUpper();
                this.sPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(value, "SHA1"); ;
            }
        }
        public int idSession
        {
            get
            {
                return this._idsession;
            }
            set
            {
                this._idsession = value;
            }
        }
        public string NombreUsuario
        {
            get
            {
                return this.sNombre;
            }
            set
            {
                this.sNombre=value;
            }
        }

        public static WSConnector Instance
        {
            get
            {
                return WSConnector._instance;
            }
        }
        #endregion 

        #region FUNCTIONS
        /// <summary>
        /// function to login into web service
        /// </summary>
        /// <returns>true on success</returns>
        public bool Login()
        {
        
            String newidsession = "";
            bool bResult = false;
            String id = "";
            String sNombreUsuario = "";
            try
            {
//                 if (!this.TestDB())
//                 {
//                     this._IsOfflineMode = true;
//                     return false;
//                 }

                if (this.Login(Utils.encriptacadena(this.sUsername), Utils.encriptacadena(this.sPassword), out id, out sNombreUsuario))
                {
                    if (bResult = Utils.desEncriptaCadena(id, out newidsession))
                    {
                        _idsession = int.Parse(newidsession);
                        this.SessionValida = bResult;
                        this.sNombre = sNombreUsuario;
                        return bResult;
                    }
                    else return bResult;
                }
                else return bResult;
            }
            catch (WebException ex)
            {
                Logger.Instance.LogException(ex);
                this._IsOfflineMode = ex.Status == WebExceptionStatus.ConnectFailure;
            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(ex);
                this._IsOfflineMode = false;
            }
            int iInserted = -1;
            int iId = -1;
            if (this._IsOfflineMode &&
                (bResult = this.TryLogin(this.sUsername, 
                this.sPassword, 
                out iId,
                out iInserted,
                out sNombreUsuario))
                )
            {
                this.SessionValida = bResult;
                this.sNombre = sNombreUsuario;
                this.LoadCacheForOfflineMode();
                return bResult;
            }

            return false;            
        }
        public bool Login(string user, string pass)
        {

            String newidsession = "";
            bool bResult = false;
            String id = "";
            String sNombreUsuario = "";

            if (this.Login(Utils.encriptacadena(user), Utils.encriptacadena(pass), out id, out sNombreUsuario))
            {
                if (bResult = Utils.desEncriptaCadena(id, out newidsession))
                {
                    _idsession = int.Parse(newidsession);
                    this.SessionValida = bResult;
                    return bResult;
                }
                else return bResult;
            }
            else return bResult;


        }

        public bool TestConnection()
        {
            return true;
//             bool bRes = false;
//             try
//             {
//                 bRes = this.TestDB();
//             }
//             catch (System.Exception ex)
//             {
//                 bRes = false;
//                 Logger.Instance.LogException(ex);
//             }
//             return bRes;
        }

        public bool Getproductor(String id, out String[] datos)
        {
            String mensaje = "";
            Boolean boleano=false;
            if(this.validaLaSession(out mensaje)){
        
            return this.GetProductorWS(id, _idsession, out datos);
            }
            datos = null;
            return boleano;

        }

        public bool GetAllRegimenes(out DataTable dtRegimenes)
        {
               String mensaje = "";
            Boolean boleano=false;
            dtRegimenes = new DataTable();
            if(this.validaLaSession(out mensaje)){
        
        
            boleano = this.GetRegimenes(out dtRegimenes);
            }
            
            return boleano;
            
        
        }
        
        public bool GetAllBodegas(out DataTable dtBodegas)
        {
            String msg = "";
            Boolean bResult = false;
            dtBodegas = new DataTable();
            if (this.validaLaSession(out msg))
            {
                try
                {
                    String sResult = string.Empty;
                    String sDecompressed = string.Empty;
                    if (Cache.Instance.CheckIfCacheIsValid(CacheTables.BODEGAS))
                    {
                        Debug.WriteLine("USING CACHE!!!");
                        CacheElement cache = Cache.Instance.GetCache(CacheTables.BODEGAS);
                        sDecompressed = (string)cache.Value;
                    }
                    else
                    {
                        sResult = this.GetBodegas();
                        sDecompressed = CompressUtils.Decompress(sResult);
                        Cache.Instance.AddCacheElement(CacheTables.BODEGAS, sDecompressed, TimeValidCache);
                    }
                    DataSet ds = new DataSet();
                    bResult = Utils.LoadXMLinDataSet(sDecompressed, out ds);
                    dtBodegas = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);
                    MessageBox.Show(ex.Message);
                    bResult = false;
                }
            }
            return bResult;
        
        }


        public bool validaLaSession(out String mensaje)
        {
            mensaje = "";
            Boolean boleano = false;
            try
            {
                if (this._IsOfflineMode)
                {
                    return true;
                }
                else
                {
                    this.IsOfflineMode = !this.TestConnection();
                    if (this.IsOfflineMode)
                    {
                        if (this.Login())
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                }
	            Cursor.Current = Cursors.WaitCursor;
	            boleano = this.validaSession(this._idsession, out mensaje);
	            //si no se valida entonces intentamos relogin
	            if (!boleano)
	            {
	                
	                MessageBox.Show(mensaje, "ERROR!");
	                Login frmLogin = new Login(1);
	                frmLogin.ShowDialog();
	                //checamos relogin
	                boleano = this.validaSession(this._idsession, out mensaje);
                    this._IsOfflineMode = false;
	            }
            }
            catch(SoapException ex)
            {
                Logger.Instance.LogException(ex);
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            this.SessionValida = boleano;
            return boleano;
        
        }


        public bool GetAllEstadosCiviles(out DataTable dtEstadosCiviles)
        {
            String msg = "";
            dtEstadosCiviles = new DataTable();
            if (this.validaLaSession(out msg))
            {
                
                return this.GetEstadoCiviles(out dtEstadosCiviles);
            }
            return false;
               
        }

        public bool GetAllEstados(out DataTable dtEstados)
        {
            
            String msg = "";
            Boolean bResult = false;
            dtEstados = new DataTable();
            if (this.validaLaSession(out msg))
            {
                try
                {
                    String sResult = this.GetEstados();
                    DataTable dtResult = new DataTable();
                    String sDecompressed = CompressUtils.Decompress(sResult);
                    DataSet ds = new DataSet();
                    bResult = Utils.LoadXMLinDataSet(sDecompressed, out ds);
                    dtEstados = ds.Tables[0];
                    
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);
                    MessageBox.Show(ex.Message);
                    bResult = false;
                }
            }
            return bResult;
            
        }

        public Estados [] GetAllEstados()
        {

            String msg = "";
            Boolean bResult = false;
            Estados[] edos = null;
            if (this.validaLaSession(out msg))
            {
                try
                {
                    String sResult = this.GetEstados();
                    DataTable dtResult = new DataTable();
                    String sDecompressed = CompressUtils.Decompress(sResult);
                    DataSet ds = new DataSet();
                    bResult = Utils.LoadXMLinDataSet(sDecompressed, out ds);
                    edos = Estados.MapFrom(ds);

                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);
                    MessageBox.Show(ex.Message);
                    bResult = false;
                }
            }
            return edos;

        }

        public bool GetAllCiclos(out DataTable dtCiclos)
        {
            String msg = "";
            Boolean bResult = false;
            dtCiclos = new DataTable();
            if (this.validaLaSession(out msg))
            {
                try
                {
                    String sResult = string.Empty;
                    String sDecompressed = string.Empty;
                    if (Cache.Instance.CheckIfCacheIsValid(CacheTables.CICLOS))
                    {
                        Debug.WriteLine("USING CACHE!!!");
                        CacheElement cache = Cache.Instance.GetCache(CacheTables.CICLOS);
                        sDecompressed = (string)cache.Value;
                    }
                    else
                    {
                        sResult = this.GetCiclos();
                        sDecompressed = CompressUtils.Decompress(sResult);
                        Cache.Instance.AddCacheElement(CacheTables.CICLOS, sDecompressed, TimeValidCache);
                    }

            
                    DataSet ds = new DataSet();
                    bResult = Utils.LoadXMLinDataSet(sDecompressed, out ds);
                    dtCiclos = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);
                    MessageBox.Show(ex.Message);
                    bResult = false;
                }
            }
            return bResult;
            
        }

        //public DataTable GetAllProductores()
        //{
        //    String sResult = this.GetProductores();
        //    DataTable dtResult = new DataTable();
        //    String sDecompressed = CompressUtils.Decompress(sResult);
        //    DataSet ds = new DataSet();
        //    Boolean bResult = Utils.LoadXMLinDataSet(sDecompressed, out ds);
        //    dtResult = ds.Tables[0];
        //    return dtResult;

        //}

        public bool GetAllBodegasforCmb(out DataTable dtBodegas)
        {

            String msg = "";
            Boolean bResult = false;
            dtBodegas = new DataTable();
            if (this.validaLaSession(out msg))
            {
                try
                {
            DataTable dtResult = new DataTable();
            
            String sResult =this.GetBodegasBoletas();
            String sDecompressed = CompressUtils.Decompress(sResult);
            DataSet ds = new DataSet();
            bResult = Utils.LoadXMLinDataSet(sDecompressed, out ds);
            dtBodegas = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);
                    MessageBox.Show(ex.Message);
                    bResult = false;
                }
            }
            return bResult;
            
            
        }
        public bool GetAllCiclosforCmb(out DataTable dtCiclos)
        {

            String msg = "";
            Boolean bResult = false;
            dtCiclos = new DataTable();
            if (this.validaLaSession(out msg))
            {
                try
                {


                    String sResult = this.GetCiclosBoletas();
                    String sDecompressed = CompressUtils.Decompress(sResult);
                    DataSet ds = new DataSet();
                    bResult = Utils.LoadXMLinDataSet(sDecompressed, out ds);
                    dtCiclos = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);
                    bResult = false;
                }
            }
            return bResult;


        }

        public bool GetAllClientesforCmb(out DataTable dtClientes)
        {

            String msg = "";
            Boolean bResult = false;
            dtClientes = new DataTable();
            if (this.validaLaSession(out msg))
            {
                try
                {


                    String sResult = this.GetClientesVentasWithTODOS();
                    String sDecompressed = CompressUtils.Decompress(sResult);
                    DataSet ds = new DataSet();
                    bResult = Utils.LoadXMLinDataSet(sDecompressed, out ds);
                    dtClientes = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);
                    bResult = false;
                }
            }
            return bResult;


        }
        public bool GetAllProductoresforCmbCache()
        {
            String msg = "";
            Boolean bResult = false;
            DataTable dtProductores = new DataTable();
            if (this.validaLaSession(out msg))
            {
                try
                {

                    String sResult = string.Empty;
                    if (Cache.Instance.CheckIfCacheIsValid(CacheTables.PRODUCTORESFORCMB))
                    {
                        sResult = (string)Cache.Instance.GetCache(CacheTables.PRODUCTORESFORCMB).Value;
                    }
                    else
                    {
                        sResult = this.GetProductorBoletas();
                        Cache.Instance.AddCacheElement(CacheTables.PRODUCTORESFORCMB, sResult, TimeValidCache);
                    }
                    String sDecompressed = CompressUtils.Decompress(sResult);
                    DataSet ds = new DataSet();
                    bResult = Utils.LoadXMLinDataSet(sDecompressed, out ds);
                    dtProductores = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);
                    bResult = false;
                }
            }
            return bResult;
        }


        public bool GetAllProductoresforCmb(out DataTable dtProductores)
        {   
            String msg = "";
            Boolean bResult = false;
            dtProductores = new DataTable();
            if (this.validaLaSession(out msg))
            {
                try
                {

                    String sResult = string.Empty;
                    if (Cache.Instance.CheckIfCacheIsValid(CacheTables.PRODUCTORESFORCMB))
                    {
                        sResult = (string)Cache.Instance.GetCache(CacheTables.PRODUCTORESFORCMB).Value;
                    }
                    else
                    {
                        sResult = this.GetProductorBoletas();
                        Cache.Instance.AddCacheElement(CacheTables.PRODUCTORESFORCMB, sResult, TimeValidCache);
                    }
                    String sDecompressed = CompressUtils.Decompress(sResult);
                    DataSet ds = new DataSet();
                    bResult = Utils.LoadXMLinDataSet(sDecompressed, out ds);
                    dtProductores = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);
                    bResult = false;
                }
            }
            return bResult;
        }
        public bool GetAllProductosforCmb(out DataTable dtProductos)
        {
            String msg = "";
            Boolean bResult = false;
            dtProductos = new DataTable();
            if (this.validaLaSession(out msg))
            {
                try
                {
            String sResult= this.GetProductosBoletas();
            
            String sDecompressed = CompressUtils.Decompress(sResult);
            DataSet ds = new DataSet();
            bResult = Utils.LoadXMLinDataSet(sDecompressed, out ds);
            dtProductos = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);
                    bResult = false;
                }
            }
            return bResult;
        }

        public bool GetAllProductos(out DataTable dtProductos)
        {
            String msg = "";
            Boolean bResult = false;
            dtProductos = new DataTable();
            if (this.validaLaSession(out msg))
            {
                try
                {
                    String sResult = string.Empty;
                    String sDecompressed = string.Empty;
                    if (Cache.Instance.CheckIfCacheIsValid(CacheTables.PRODUCTOS))
                    {
                        Debug.WriteLine("USING CACHE!!!");
                        CacheElement cache = Cache.Instance.GetCache(CacheTables.PRODUCTOS);
                        sDecompressed = (string)cache.Value;
                    }
                    else
                    {
                        sResult = this.GetProductos();
                        sDecompressed = CompressUtils.Decompress(sResult);
                        Cache.Instance.AddCacheElement(CacheTables.PRODUCTOS, sDecompressed, TimeValidCache);
                    }
                    DataSet ds = new DataSet();
                    bResult = Utils.LoadXMLinDataSet(sDecompressed, out ds);
                    dtProductos = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);
                    bResult = false;
                }
            }
            return bResult;
        }

        public bool GetAllClientesVentas(out DataTable dtClientesVentas)
        {
            String msg = "";
            Boolean bResult = false;
            dtClientesVentas = new DataTable();
            if (this.validaLaSession(out msg))
            {
                try
                {
                    String sResult = string.Empty;
                    String sDecompressed = string.Empty;
                    if (Cache.Instance.CheckIfCacheIsValid(CacheTables.CLIENTESVENTAS))
                    {
                        Debug.WriteLine("USING CACHE!!!");
                        CacheElement cache = Cache.Instance.GetCache(CacheTables.CLIENTESVENTAS);
                        sDecompressed = (string)cache.Value;
                    }
                    else
                    {
                        sResult = this.GetClientesVentas();
                        sDecompressed = CompressUtils.Decompress(sResult);
                        Cache.Instance.AddCacheElement(CacheTables.CLIENTESVENTAS, sDecompressed, TimeValidCache);
                    }
                    DataSet ds = new DataSet();
                    bResult = Utils.LoadXMLinDataSet(sDecompressed, out ds);
                    dtClientesVentas = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);
                    bResult = false;
                }
            }
            return bResult;
        }

        public bool GetProvDeGanado(out DataTable dtProvDeGanado)
        {
            String msg = "";
            Boolean bResult = false;
            dtProvDeGanado = new DataTable();
            if (this.validaLaSession(out msg))
            {
                try
                {
                    String sResult = string.Empty;
                    String sDecompressed = string.Empty;
                    if (Cache.Instance.CheckIfCacheIsValid(CacheTables.PROVEEDORESGANADO))
                    {
                        Debug.WriteLine("USING CACHE!!!");
                        CacheElement cache = Cache.Instance.GetCache(CacheTables.PROVEEDORESGANADO);
                        sDecompressed = (string)cache.Value;
                    }
                    else
                    {
                        sResult = this.GetProveedoresDeGanado();
                        sDecompressed = CompressUtils.Decompress(sResult);
                        Cache.Instance.AddCacheElement(CacheTables.PROVEEDORESGANADO, sDecompressed, TimeValidCache);
                    }
                    DataSet ds = new DataSet();
                    bResult = Utils.LoadXMLinDataSet(sDecompressed, out ds);
                    dtProvDeGanado = ds.Tables[0];
                }
                catch(Exception ex)
                {
                    Logger.Instance.LogException(ex);
                    bResult = false;
                }
            }
            return bResult;
        }

        

        public bool GetAllProductores(out DataTable dtProductores)
        {
            DateTime startime = DateTime.Now;
            Debug.WriteLine("GetAllProductores ->>>");
            Debug.WriteLine("start time: " + startime.ToString("HH:mm:ss"));
            String msg = "";
            Boolean bResult = false;
            dtProductores = new DataTable();
            if (this.validaLaSession(out msg))
            {
                try
                {
                    String sResult = string.Empty;
                    String sDecompressed = string.Empty;
                    if(Cache.Instance.CheckIfCacheIsValid(CacheTables.PRODUCTORES))
                    {
                        Debug.WriteLine("USING CACHE!!!");
                        CacheElement cache = Cache.Instance.GetCache(CacheTables.PRODUCTORES);
                        sDecompressed = (string)cache.Value;
                    }
                    else
                    {
                        sResult = this.GetProductorescmb();
                        sDecompressed = CompressUtils.Decompress(sResult);
                        Cache.Instance.AddCacheElement(CacheTables.PRODUCTORES, sDecompressed, TimeValidCache);
                    }
                    
                    DataSet ds = new DataSet();
                    bResult = Utils.LoadXMLinDataSet(sDecompressed, out ds);
                    dtProductores = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);
                    bResult = false;
                }
            }
            Debug.WriteLine("stop time: " + DateTime.Now.ToString("HH:mm:ss"));
            Debug.WriteLine("Took: " + (DateTime.Now - startime).TotalSeconds.ToString() + " segs.");
            return bResult;
        }

        public bool GetaBoleta(String idtoModify, out String[] datos)
        {
            String msg="";
            if(this.validaLaSession(out msg))
            {
                return this.SelectBoletas(idtoModify, _idsession, out datos);
            }
            datos = null;
            return false;
            
        }

        public bool GetaBoletaAsDataTable(String idtoModify, out DataTable datos)
        {
            String msg = "";
            datos = new DataTable();
            if (this.validaLaSession(out msg))
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    string sResult = string.Empty;
                    if(!this.IsOfflineMode)
                    {
                        if (this.SelectBoletasAsDataTable(idtoModify, _idsession, out sResult))
                        {
                            String sDecompressed = CompressUtils.Decompress(sResult);
                            DataSet ds = new DataSet();
                            if (Utils.LoadXMLinDataSet(sDecompressed, out ds))
                            {
                                datos = ds.Tables[0];
                            }
                        }
                    }
                    else
                    {
                        datos = this.GetBoletaData(int.Parse(idtoModify));
                    }
                    
                     //= ds.Tables[0];
                    return true;
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(ex);
                }

                //return this.SelectBoletas(idtoModify, _idsession, out datos);
            }
            //datos = null;
            return false;

        }

        public bool GetProductores(String sFields, out DataTable dt)
        {
            String msg = "";
            DataTable dtResult = new DataTable();
            bool bResult = false;
            if (this.validaLaSession(out msg))
            {

                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    String sResult = this.Query("PRODUCTORES", sFields, _idsession);
                    String sDecompressed = CompressUtils.Decompress(sResult);
                    DataSet ds = new DataSet();
                    bResult = Utils.LoadXMLinDataSet(sDecompressed, out ds);
                    dtResult = ds.Tables[0];
                    bResult = true;
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(ex);
                }
            }
            dt = dtResult;
            Cursor.Current = Cursors.Default;
            return bResult;
        }

        public bool GetBoletas(String filtros,out DataTable dt)
        {
            //String msg = "";
            dt = new DataTable();
            
            bool bResult = false;
            //if (this.validaLaSession(out msg))
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    String sResult = this.Query("BOLETAS", filtros, _idsession);
                    String sDecompressed = CompressUtils.Decompress(sResult);
                    DataSet ds = new DataSet();
                    bResult = Utils.LoadXMLinDataSet(sDecompressed, out ds);
                    dt = ds.Tables[0];
                    bResult = true;
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.LogException(ex);
                }

                
                Cursor.Current = Cursors.Default;
            }
            return bResult;
        }

        public bool GetBoletasPendientes(out DataTable dt)
        {
            //String msg = "";
            dt = new DataTable();

            bool bResult = false;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                String sResult = this.QueryBoletasPendientes(_idsession);
                String sDecompressed = CompressUtils.Decompress(sResult);
                DataSet ds = new DataSet();
                bResult = Utils.LoadXMLinDataSet(sDecompressed, out ds);
                dt = ds.Tables[0];
                bResult = true;
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(ex);
            }


            Cursor.Current = Cursors.Default;

            return bResult;
        }

        public bool InsertProductor(String[] datos, out String exc)
        {
            Boolean boleano = false;
            exc="";
            
            if (this.validaLaSession(out exc))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    boleano = this.InsertPro(datos, _idsession.ToString(), out exc);

                    Cursor.Current = Cursors.Default;
                    boleano = true;
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);
                    exc = ex.Message;
                }
            }
            return boleano;

            
        }

        public void logOut()
        {
            String id = this._idsession.ToString();
            //clean the session
            
            this.sUsername = "";
            this.sPassword = "";
            this._idsession = -1;
            Cursor.Current = Cursors.WaitCursor;
            
            //log this action
            try
            {
                this.LogOut(id);
            }catch(Exception ex)
            {
                Logger.Instance.LogException(ex);
            }
            Cursor.Current = Cursors.Default;
            

        }
        public bool ModifyProductor(String[] datos, String id, out String exc)
        {
            
            Boolean boleano = false;
            if (this.validaLaSession(out exc))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    boleano = this.ModifyPro(datos, id, _idsession, out exc);
                    Cursor.Current = Cursors.Default;
                }
                catch(Exception ex)
                {
                    Logger.Instance.LogException(ex);
                    MessageBox.Show(ex.Message);
                    exc = ex.Message;
                }
            }

            return boleano;
            
        }
        public bool ModificarBoleta(String[] datos, String id, out String exc)
        {
            Boolean boleano = false;
            if (this.validaLaSession(out exc))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (!this.IsOfflineMode)
                    {
                        boleano = this.UpdateBoleta(datos, id, _idsession, out exc);
                    }
                    else
                    {
                        boleano = this.UpdateBoletaOffLine(datos,out exc, int.Parse(id));
                    }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);
                    MessageBox.Show(ex.Message);
                    exc = ex.Message;
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
            return boleano;

        }

        public bool deleteProductor(int id, String productor, out String msg)
        {

            Boolean boleano = false;
           
            if (this.validaLaSession(out msg))
            {
                Cursor.Current = Cursors.WaitCursor;
            
            boleano = this.deletePro(id, productor, _idsession, out msg);
            }
            Cursor.Current = Cursors.Default;
            return boleano;
        }


        public bool deleteBoleta(int id, out String mensaje)
        {
            
            Boolean boleano = false;
           
            if (this.validaLaSession(out mensaje))
            {
            Cursor.Current = Cursors.WaitCursor;
            boleano = this.deleteBol(id, _idsession, out mensaje);
            
                }
            Cursor.Current = Cursors.Default;
            return boleano;
            
        }

        public bool InsertBoletaOffLine(String[] datos, out String exc, out int newID)
        {
            bool bResult = false;
            newID = -1;
            exc = "";
            return true;
            try
            {
                Dbo_Boletas bol = new Dbo_Boletas();
                bol.CicloID = int.Parse(datos[0]);
                bol.UserID = this._UserID;
                bol.ProductorID = int.Parse(datos[2]);
                bol.Humedad = double.Parse(datos[3]);
                bol.DctoHumedad = double.Parse(datos[4]);
                bol.Impurezas = double.Parse(datos[5]);
                bol.Totaldescuentos = decimal.Parse(datos[6]);
                bol.Pesonetoapagar = double.Parse(datos[7]);
                bol.Precioapagar = decimal.Parse(datos[8]);
                bol.Importe = decimal.Parse(datos[9]);
                bol.Placas = datos[10];
                bol.Chofer = datos[11];
                bol.Pagada = bool.Parse(datos[12]);
                bol.StoreTS = DateTime.Now;
                bol.UpdateTS = DateTime.Now;
                bol.ProductoID = int.Parse(datos[13]);
                bol.NumeroBoleta = datos[14];
                bol.Ticket = datos[15];
                bol.NombreProductor = datos[17];
                bol.FechaEntrada = DateTime.Parse(datos[18]);
                bol.PesadorEntrada = datos[19];
                bol.PesoDeEntrada = double.Parse(datos[20]);
                bol.BasculaEntrada = datos[21];
                bol.FechaSalida = DateTime.Parse(datos[22]);
                bol.PesoDeSalida = double.Parse(datos[23]);
                bol.PesadorSalida = datos[24];
                bol.BasculaSalida = datos[25];
                bol.Pesonetoentrada = double.Parse(datos[26]);
                bol.Pesonetosalida = double.Parse(datos[27]);
                bol.DctoImpurezas = decimal.Parse(datos[28]);
                bol.DctoSecado = decimal.Parse(datos[29]);
                bol.Totalapagar = decimal.Parse(datos[30]);
                bol.BodegaID = int.Parse(datos[31]);
                bol.ApplyHumedad = bool.Parse(datos[32]);
                bol.ApplyImpurezas = bool.Parse(datos[33]);
                bol.ApplySecado = bool.Parse(datos[34]);
                bol.CabezasDeGanado = int.Parse(datos[37]);
                bol.LlevaFlete = bool.Parse(datos[39]);
                bol.DeGranjaACorrales = bool.Parse(datos[40]);
                bol.ClienteventaID = int.Parse(datos[35]);
                bol.GanProveedorID = int.Parse(datos[38]);


                
                

                //////////////////////////////////////////////////////////////////////////
                /*
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
                    }*/

                //////////////////////////////////////////////////////////////////////////
            }
            catch (SqlCeException ex)
            {
                Logger.Instance.LogException(ex);
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(ex);
            }
            return bResult;
        }

        public bool InsertarBoleta(String[] datos, out String exc, out int newID)
        {
            Boolean boleano = false;
            exc="";
            newID = -1;
            
            if (this.validaLaSession(out exc))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (this._IsOfflineMode)
                    {
                        boleano = this.InsertBoletaOffLine(datos, out exc, out newID);
                    }
                    else
                    {
                        boleano = this.InsertBoleta(datos, _idsession, out exc, out newID);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);
                    MessageBox.Show(ex.Message);
                    exc = ex.Message;
                }            
            }
            Cursor.Current = Cursors.Default;
            return boleano;

        }


        public void SaveCacheForOfflineMode()
        {
            try
            {
                Cache.Instance.SaveCache();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(ex);
            }
        }

        public void LoadCacheForOfflineMode()
        {
            try
            {
                Cache.Instance.LoadCache();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(ex);
            }
        }

        private void LoadTodaysBoletas()
        {
            string filtros = string.Empty;
            filtros = "FechaEntrada > '"+ DateTime.Now.ToString("yyyy/MM/dd 00:00:00") +"' AND";
            filtros = "FechaEntrada < '"+ DateTime.Now.ToString("yyyy/MM/dd 23:59:59") +"'";
            DataTable dt = new DataTable();
            if(this.GetBoletas(filtros, out dt))
            {
                Cache.Instance.AddCacheElement(CacheTables.BOLETASPENDIENTES, dt);
            }
        }


        protected bool TryLogin(String username, String password, out int iUserID, out int idInserted, out String sUsuario)
        {
            WSGaribayDataSetTableAdapters.dbo_UsersTableAdapter userA = new WSGaribayDataSetTableAdapters.dbo_UsersTableAdapter();
            WSGaribayDataSet.dbo_UsersDataTable dt = userA.GetDataByLogin(username, password);
            iUserID = -1;
            sUsuario = "Usuario NO Validado";
            idInserted = -1;
            if (dt.Rows.Count == 1)
            {
                WSGaribayDataSet.dbo_UsersRow row = (WSGaribayDataSet.dbo_UsersRow)dt.Rows[0];
                iUserID = row.userID;
                this.UserID = row.userID;
                sUsuario = row.Nombre;
            }
            return (iUserID > -1);
        }

        public void SyncDatabase()
        {
            try
            {
                GaribayCacheSyncAgent syncAgent = new GaribayCacheSyncAgent();

                /*
                syncAgent.Boletas.SyncDirection = SyncDirection.Bidirectional;
                                syncAgent.Boletas.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;*/
                

                Microsoft.Synchronization.Data.SyncStatistics syncStats = syncAgent.Synchronize();
                Logger.Instance.LogMessage("Boletas Downloaded: " + syncStats.TotalChangesDownloaded.ToString() +
                " Boletas Downloaded Failed: " + syncStats.DownloadChangesFailed.ToString() +
                " Boletas Updated: " + syncStats.UploadChangesApplied.ToString() +
                " Boletas Updated Failed: " + syncStats.UploadChangesFailed.ToString());

                MessageBox.Show("Boletas Downloaded: " + syncStats.TotalChangesDownloaded.ToString() +
                " Boletas Downloaded Failed: " + syncStats.DownloadChangesFailed.ToString() +
                " Boletas Updated: " + syncStats.UploadChangesApplied.ToString() +
                " Boletas Updated Failed: " + syncStats.UploadChangesFailed.ToString());

/*
                
                                syncAgent.Boletas.SyncDirection = SyncDirection.Snapshot;
                                syncAgent.Boletas.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
                
                                syncStats = syncAgent.Synchronize();
                                Logger.Instance.LogMessage("Boletas Downloaded: " + syncStats.TotalChangesDownloaded.ToString() +
                                " Boletas Downloaded Failed: " + syncStats.DownloadChangesFailed.ToString() +
                                " Boletas Updated: " + syncStats.UploadChangesApplied.ToString() +
                                " Boletas Updated Failed: " + syncStats.UploadChangesFailed.ToString());*/
                

            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(ex);
            }
        }

        private DataTable GetBoletaData(int boletaid)
        {
            WSGaribayDataSet.dbo_BoletasDataTable dt = null;
            try
            {
                WSGaribayDataSetTableAdapters.dbo_BoletasTableAdapter BoletasAd = new WSGaribayDataSetTableAdapters.dbo_BoletasTableAdapter();
                dt = BoletasAd.GetDataByBoletaID(boletaid);
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(ex);
            }
            return dt;
        }

        public void RecreateDatabase()
        {/*
            GaribayCacheSyncAgent syncAgent = new GaribayCacheSyncAgent();

            syncAgent.Boletas.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            syncAgent.Boletas.SyncDirection = SyncDirection.Snapshot;
            syncAgent.ClienteVenta_Boletas.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            syncAgent.ClienteVenta_Boletas.SyncDirection = SyncDirection.Snapshot;
            syncAgent.dbo_ClientesVentas.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            syncAgent.dbo_ClientesVentas.SyncDirection = SyncDirection.Snapshot;
            syncAgent.dbo_gan_Proveedores.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            syncAgent.dbo_gan_Proveedores.SyncDirection = SyncDirection.Snapshot;
            syncAgent.dbo_gan_Proveedores_Boletas.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            syncAgent.dbo_gan_Proveedores_Boletas.SyncDirection = SyncDirection.Snapshot;
            syncAgent.dbo_Productores.CreationOption = TableCreationOption.DropExistingOrCreateNewTable;
            syncAgent.dbo_Productores.SyncDirection = SyncDirection.Snapshot;

            Microsoft.Synchronization.Data.SyncStatistics syncStats = syncAgent.Synchronize();
            Logger.Instance.LogMessage("Boletas Downloaded: " + syncStats.TotalChangesDownloaded.ToString() +
            " Boletas Downloaded Failed: " + syncStats.DownloadChangesFailed.ToString() +
            " Boletas Updated: " + syncStats.UploadChangesApplied.ToString() +
            " Boletas Updated Failed: " + syncStats.UploadChangesFailed.ToString());

            MessageBox.Show("Boletas Downloaded: " + syncStats.TotalChangesDownloaded.ToString() +
            " Boletas Downloaded Failed: " + syncStats.DownloadChangesFailed.ToString() +
            " Boletas Updated: " + syncStats.UploadChangesApplied.ToString() +
            " Boletas Updated Failed: " + syncStats.UploadChangesFailed.ToString());*/
        }


        public Proveedores[] GetAllProveedores()
        {
            Proveedores []provs = null;

            String msg = "";
            if (this.validaLaSession(out msg))
            {
                try
                {
                    String sResult = string.Empty;
                    String sDecompressed = string.Empty;
                    sResult = this.GetProveedores();
                    sDecompressed = CompressUtils.Decompress(sResult);
                    DataSet ds = new DataSet();
                    if (Utils.LoadXMLinDataSet(sDecompressed, out ds))
                    {
                        provs = Proveedores.MapFrom(ds);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);
                }
            }
            return provs;
        }

        public bool UpdateBoletaOffLine(String[] datos, out String exc, int iID)
        {
            bool bResult = false;
            exc = "";
            return true;
            try
            {
                this.BoletasData.Add(datos);
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(ex);
                exc = ex.ToString();
            }
            return bResult;
        }

        public bool DeleteProveedor(int ProvToDelID)
        {
            Boolean boleano = false;
            string mensaje;
            Cursor.Current = Cursors.WaitCursor;
            if (this.validaLaSession(out mensaje))
            {
                boleano = this.deleteProv(ProvToDelID, _idsession, out mensaje);
            }
            Cursor.Current = Cursors.Default;
            return boleano;
        }

        private void LoginOffline()
        {
            string sNombreUsuario = string.Empty;
            int iInserted = -1;
            int iId = -1;
            if (this._IsOfflineMode &&
                (this.TryLogin(this.sUsername,
                this.sPassword,
                out iId,
                out iInserted,
                out sNombreUsuario))
                )
            {
                this.SessionValida = true;
                this.sNombre = sNombreUsuario;
                this.LoadCacheForOfflineMode();
            }

        }

        public Proveedores AddUpdateProveedor(Proveedores prov)
        {
            String sXMLResult = "";
            DataSet ds = prov.MapTo();
            ds.Tables[0].TableName = "Proveedores";
            sXMLResult = this.DataSetToXML(ds);
            sXMLResult = BasculaGaribay.CompressUtils.Compress(sXMLResult);
            if(Utils.LoadXMLinDataSet(CompressUtils.Decompress(this.WorkWithProveedor(sXMLResult)),out ds))
            {
                prov = Proveedores.MapFrom(ds)[0];
            }
            return prov;
        }

        private String DataSetToXML(DataSet ds)
        {
            StringWriter tw = new StringWriter();
            ds.WriteXml(tw, XmlWriteMode.WriteSchema);
            return tw.ToString();
        }
        #endregion

    }
}
