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
using System.Text;
using System.IO;
using System.Globalization;
using System.Threading;

namespace Garibay
{

    /// <summary>
    /// This class will serve as base class for all the web pages in my web-site.
    /// </summary>
    /// 
    
    

    public class BasePage : System.Web.UI.Page
    {


        private const int DefaultViewStateTimeout = 120;
        private string _viewStateConnectionString;
        private TimeSpan _viewStateTimeout;

        public Hashtable myQueryStrings;
        public bool hasCalendar = false;
        protected bool bUseMasterPage = true;

        /// <summary>
        /// load encrypted querystring and return the number of querystrings loaded.
        /// </summary>
        /// <returns>number of query parameters loaded.</returns>
        protected int LoadEncryptedQueryString()
        {
            if (Request.QueryString["data"] != null)
            {
                this.loadqueryStrings(Request.QueryString["data"].ToString());
            }
            return this.myQueryStrings.Count;
        }
        public String CurrentUserName
        {
            get
            {
                return this.Session["USUARIO"].ToString().ToUpper();
            }
        }

        public string CurrentUrl
        {
            get
            {
                return Request.Url.ToString();
            }
        }

        protected bool IsDesignMode
        {
            get { return (this.Context == null); }
        }

        protected bool IsSqlViewStateEnabled
        {
            get { return (this._viewStateConnectionString != null && this._viewStateConnectionString.Length > 0); }
        }

        public TimeSpan ViewStateTimeout
        {
            get { return this._viewStateTimeout; }
            set { this._viewStateTimeout = value; }
        }

        private string GetMacKeyModifier()
        {
            int value = this.TemplateSourceDirectory.GetHashCode() + this.GetType().Name.GetHashCode();
            if (this.ViewStateUserKey != null)
                return string.Concat(value.ToString(NumberFormatInfo.InvariantInfo), this.ViewStateUserKey);
            return value.ToString(NumberFormatInfo.InvariantInfo);

        }

        private LosFormatter GetLosFormatter()
        {
            if (this.EnableViewStateMac)
                return new LosFormatter(true, this.GetMacKeyModifier());
            return new LosFormatter();
        }

        private Guid GetViewStateGuid()
        {
            string viewStateKey;
            viewStateKey = this.Request.Form["__VIEWSTATEGUID"];
            if (viewStateKey == null || viewStateKey.Length < 1)
            {
                viewStateKey = this.Request.QueryString["__VIEWSTATEGUID"];
                if (viewStateKey == null || viewStateKey.Length < 1)
                    return Guid.NewGuid();
            }
            try
            {
                return new Guid(viewStateKey);
            }
            catch (FormatException)
            {
                return Guid.NewGuid();
            }
        }

        protected override void SavePageStateToPersistenceMedium(object state)
        {
            /*
            StringBuilder sb = new StringBuilder();
                        StringWriter swr = new StringWriter(sb);
            
                        LosFormatter formatter = new LosFormatter();
                        formatter.Serialize(swr, state);
                        swr.Close();
            
                        // Store the textual representation of ViewState in the 
                        // database or elsewhere
                        // The serialized view state is available via sb.ToString ()
                        this.Session[this.Request.Url.ToString()] = sb.ToString();*/
            
            /************************************************************************/
            Guid viewStateGuid;
            HtmlInputHidden control;
            if (this.IsDesignMode)
                return;
            if (!this.IsSqlViewStateEnabled)
            {
                base.SavePageStateToPersistenceMedium(state);
                return;
            }
            viewStateGuid = this.GetViewStateGuid();
            using (MemoryStream stream = new MemoryStream())
            {
                this.GetLosFormatter().Serialize(stream, state);
                using (SqlConnection connection = new SqlConnection(this._viewStateConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SetViewState", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@returnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                        command.Parameters.Add("@viewStateId", SqlDbType.UniqueIdentifier).Value = viewStateGuid;
                        command.Parameters.Add("@value", SqlDbType.Image).Value = stream.ToArray();
                        command.Parameters.Add("@timeout", SqlDbType.Int).Value = this._viewStateTimeout.TotalMinutes;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            control = this.FindControl("__VIEWSTATEGUID") as HtmlInputHidden;
            //ClientScript.RegisterHiddenField()
            if (control == null)
                ClientScript.RegisterHiddenField("__VIEWSTATEGUID", viewStateGuid.ToString());
            else
                control.Value = viewStateGuid.ToString();
            /************************************************************************/

        }
        protected override object LoadPageStateFromPersistenceMedium()
        {
            /*
            object objViewState = string.Empty;
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
                            object obj = formatter.Deserialize(strViewState);
                            if(obj != null)
                                objViewState = obj;
                        }
                        catch(Exception ex)
                        {
                            Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Error loading persistence", ref ex);
                            throw ex;
                        }
                        return objViewState;*/
            
            /************************************************************************/
            Guid viewStateGuid;
            byte[] rawData;
            if (this.IsDesignMode)
                return null;
            if (!this.IsSqlViewStateEnabled)
                return base.LoadPageStateFromPersistenceMedium();
            viewStateGuid = this.GetViewStateGuid();
            rawData = null;
            using (SqlConnection connection = new SqlConnection(this._viewStateConnectionString))
            {
                using (SqlCommand command = new SqlCommand("GetViewState", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@returnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add("@viewStateId", SqlDbType.UniqueIdentifier).Value = viewStateGuid;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            rawData = (byte[])Array.CreateInstance(typeof(byte), reader.GetInt32(0));
                        if (reader.NextResult() && reader.Read())
                            reader.GetBytes(0, 0, rawData, 0, rawData.Length);
                    }
                }
                connection.Close();
            }
            using (MemoryStream stream = new MemoryStream(rawData))
                return this.GetLosFormatter().Deserialize(stream);
            /************************************************************************/
        }
        public int SecurityLevel
        {
            get { return int.Parse(this.Session["SECURITYID"].ToString()); }
        }

        public int UserID
        {
            get {
                try
                {
                    return int.Parse(this.Session["USERID"].ToString());
                }
                catch
                {
                    return -1;
                }
            }
        }
      
        public int BodegaID
        {
            get 
            { 
                int id = -1;
                if (this.Session["CURRENTBODEGAID"] != null)
                {
                    int.TryParse(this.Session["CURRENTBODEGAID"].ToString(), out id);
                }
                return  id;
            }
            set
            {
                int id = -1;
                int.TryParse(value.ToString(), out id);
                this.Session["CURRENTBODEGAID"] = id;
            }
        }
        public override String StyleSheetTheme
        {
            get { return null; }
        }
        public BasePage(bool bUseMaster):this()
        {
            this.bUseMasterPage = bUseMaster;
        }
        public BasePage()
        {
            myQueryStrings = new Hashtable();

            if (this.IsDesignMode)
                return;
            this._viewStateConnectionString = myConfig.ConnectionInfo;
            try
            {
                this._viewStateTimeout = TimeSpan.FromMinutes(Convert.ToDouble(ConfigurationSettings.AppSettings["ViewStateTimeout"]));
                this._viewStateTimeout = this._viewStateTimeout.TotalMinutes < 1 ? TimeSpan.FromMinutes(BasePage.DefaultViewStateTimeout) : this._viewStateTimeout;
            }
            catch
            {
                this._viewStateTimeout = TimeSpan.FromMinutes(BasePage.DefaultViewStateTimeout);
            }
        }

        public bool IsSistemBanco
        {
            get
            {
                return this.Session["SISTEMABANCO"] != null && ((bool)this.Session["SISTEMABANCO"]) ? true : false;
            }
        }

        private void LoadSessionFromCookie()
        {
            Hashtable data = new Hashtable();
            HttpCookie galleta;
            galleta = Request.Cookies["galleta"];
            if (galleta == null)
            {
                galleta = new HttpCookie("galleta");
            }
            String cadena = "";
            if (galleta.Value != null)
            {
                cadena = galleta.Value.ToString();
            }
            else
            {
                if (this.Session["id"] != null)
                    cadena = this.Session["id"].ToString();
            }
            if (Utils.loadqueryStrings(cadena, ref data))
            {
                this.Session["SISTEMABANCO"] = bool.Parse(data["SISTEMABANCO"].ToString());
                this.Session["USERID"] = data["USERID"];
                this.Session["USUARIO"] = data["USUARIO"].ToString();
                this.Session["SECURITYID"] = data["SECURITYID"].ToString();
                DateTime cookievalid = DateTime.Parse(data["date"].ToString());
                int id = -1;
                if (DateTime.Now > cookievalid ||
                    this.Session["USERID"] == null ||
                    this.Session["SECURITYID"] == null ||
                    !int.TryParse(this.Session["USERID"].ToString(), out id) ||
                    id <=0 ||
                    !int.TryParse(this.Session["SECURITYID"].ToString(), out id) ||
                    id <=0)
                {
                    this.Session["USERID"] = null;
                }
            }
            else
                galleta.Expires = DateTime.Now.AddHours(-1);
            Response.Cookies.Add(galleta);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.LoadSessionFromCookie();
            if (!this.bUseMasterPage)
            {
                this.MasterPageFile = "~/Empty.Master";
            }
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-MX");
            int id = -1;
            if (this.Session["USERID"] == null || !int.TryParse(this.Session["USERID"].ToString(),out id))
            {

                this.Session["BACKPAGE"] = this.Request.Url;

                string strRedirect = "~/logout.aspx?data=";
                string valorencriptado = "valor=1&";
                strRedirect += Utils.encriptacadena(valorencriptado);
                this.Response.Redirect(strRedirect);

            }
            this.Page.MaintainScrollPositionOnPostBack = true;
            if (Session["Tema"] != null)
            {

                //Create object of DropDownList control available in Master Page, 
                //you can access the Master page by using Page.Master
//                 DropDownList ddlThemes = (DropDownList)Page.Master.FindControl("DDLTEMA");
//                 ddlThemes.SelectedValue = Session["Tema"].ToString();
//                 if (ddlThemes.SelectedValue == "skinrojo")
//                 {
//                     Session["SkinCalendar"] = "Classic";
// 
//                 }
//                 else{
                    Session["SkinCalendar"] = "Modern";
//          }
                //Re-set Page Theme with Session Variable
                this.Theme = Session["Tema"].ToString();

                //                 foreach (object aux in this.contro)
                //                 {
                // 
                //                     int a;
                //                     a = 10;
                //                 }

            }
            else
            {
                //Create Session variable, it should contain the theme name, that you want should be selected by default
                //Here, i wanted my theme "Caribbean Sun" to show by default
                Session["Tema"] = "skinverde";
                Session["SkinCalendar"] = "Modern";
                this.Theme = "skinverde";
                if (Page.Master != null)
                {
                    DropDownList ddlThemes = (DropDownList)Page.Master.FindControl("DDLTEMA");
                    if (ddlThemes != null)
                    {
                        ddlThemes.SelectedValue = Session["Tema"].ToString();
                    }
                }

            }
//            DateTime start = Utils.Now;
            if (hasCalendar)
            {
                this.applyCsstoCalendar(this.Master);
            }
//             Logger.Instance.LogMessage(Logger.typeLogMessage.INFO,
//                 Logger.typeUserActions.SELECT,
//                 this.UserID,
//                 string.Format("Took {0} Segs", (Utils.Now - start).TotalSeconds), 
//                 this.Request.Url.ToString());
            this.Title = "Garibay - " + this.Title;

        }
        public void applyCsstoCalendar(Control control)
        {
            return;
            foreach (Control controlaux in control.Controls)
            {
                if (controlaux.GetType().ToString() == "RJS.Web.WebControl.PopCalendar")
                {
                    ((RJS.Web.WebControl.PopCalendar)controlaux).CssClass = Session["SkinCalendar"].ToString();
                }
                else
                {
                    applyCsstoCalendar(controlaux);
                }

            }

        }
        public bool loadqueryStrings(String cadenaadividir)
        {
            return Utils.loadqueryStrings(cadenaadividir, ref this.myQueryStrings);
        }

        public bool validateSecurityLevel(String validLevels, String urltoredirect)
        {
            bool resul = false;
            string secID = "";
            string sSql = "SELECT securitylevelID FROM Users WHERE userID = @userID";
            SqlConnection sqlConn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand sqlComm = new SqlCommand(sSql, sqlConn);
                sqlComm.Parameters.Add("@userID", SqlDbType.Int).Value = int.Parse(this.Session["USERID"].ToString());
                sqlConn.Open();
                SqlDataReader dr = sqlComm.ExecuteReader();

                if (dr.HasRows && dr.Read())
                {
                    secID = dr["securitylevelID"].ToString();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "validateSecurityLevel", ref ex);
            }
            finally
            {
                sqlConn.Close();
            }
            if (validLevels != null)
            {
                string[] validLvl = validLevels.Split(';');
                foreach (string validlevel in validLvl)
                {
                    if (validlevel == secID)
                    {
                        resul = true;
                        break;
                    }
                }
            }
            if (urltoredirect != null && urltoredirect != "" && resul == false)
            {
                Server.Transfer(urltoredirect);
            }
            return resul;
        }
    }

}