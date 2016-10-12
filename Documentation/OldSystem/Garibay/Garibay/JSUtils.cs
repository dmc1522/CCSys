using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Garibay
{
    public class JSUtils
    {
        public static void AddAlertonButton (ref System.Web.UI.WebControls.Button btn, string mensaje)
        {
            
            string msgConf = "return alert('  " +  mensaje + " ') ";
            btn.Attributes.Add("onclick", msgConf);
        }
        public static void AddConfirmToCtrlOnClick(ref System.Web.UI.WebControls.Button ctrl, string msg)
        {
            JSUtils.AddConfirmToCtrlOnClick(ref ctrl, msg, true);
        }

        public static void AddConfirmToButton(ref Button ctrl, string msg)
        {
            string msgConf = "";
            if (ctrl != null)
            {
                msgConf = "return ";
                msgConf += " confirm('" + msg + "');";
                ctrl.OnClientClick = msgConf;
            }
        }

        public static void AddConfirmToHyperlink(ref HyperLink ctrl, string msg)
        {
            string msgConf = "";
            if (ctrl != null)
            {
                msgConf = "return ";
                msgConf += " confirm('" + msg + "');";
                ctrl.Attributes["OnClick"] += msgConf;
            }
        }

        public static void AddConfirmToCtrlOnClick(ref System.Web.UI.WebControls.Button ctrl, string msg, bool AddReturn)
        {
            string msgConf = "";
            if (ctrl != null)
            {
                if (ctrl.Attributes["OnClick"] != null)
                {
                    msgConf = " confirm('" + msg + "');";
                    ctrl.Attributes["OnClick"] += ";" + msgConf;
                }
                else
                {
                    if (AddReturn)
                    {
                        msgConf = "return ";
                    }
                    msgConf += " confirm('" + msg + "');";
                    ctrl.Attributes.Add("onclick", msgConf);
                }
            }

        }


        public static void AddMsgToCtrlOnClick(ref System.Web.UI.WebControls.Button ctrl, string msg)
        {
            JSUtils.AddMsgToCtrlOnClick(ref ctrl, msg, true);
        }
        public static void AddMsgToCtrlOnClick(ref System.Web.UI.WebControls.Button ctrl, string msg, bool AddReturn)
        {
            string msgConf = "";
            if (ctrl != null)
            {
                if (ctrl.Attributes["OnClick"] != null)
                {
                    msgConf = " alert('" + msg + "');";
                    ctrl.Attributes["OnClick"] += ";" + msgConf;
                }
                else
                {
                    if (AddReturn)
                    {
                        msgConf = "return ";
                    }
                    msgConf += " alert('" + msg + "');";
                    ctrl.Attributes.Add("onclick", msgConf);
                }
            }

        }
        public static void closeCurrentWindow(ref System.Web.UI.WebControls.Button ctrl)
        {
            if (ctrl != null)
            {
                if (ctrl.Attributes["OnClick"] != null)
                {
                    String msgConf = " window.close();";
                    ctrl.Attributes["OnClick"] += ";" + msgConf;
                }
                else
                {
                    ctrl.Attributes.Add("onclick", "window.close();");
                }
            }
        }
        public static void OpenNewWindowOnClick(ref System.Web.UI.WebControls.Button ctrl, String strRedirect, String ventanatitle, bool bAddOnReturn)
        {
            WebControl wbCtrl = (WebControl)ctrl;
            OpenNewWindowOnClick(ref wbCtrl, strRedirect, ventanatitle, bAddOnReturn);
        }
        public static void OpenNewWindowOnClick(ref System.Web.UI.WebControls.HyperLink ctrl, String strRedirect, String ventanatitle, bool bAddOnReturn)
        {
            WebControl wbCtrl = (WebControl)ctrl;
            OpenNewWindowOnClick(ref wbCtrl, strRedirect, ventanatitle, bAddOnReturn);
        }
        public static void OpenNewWindowOnClick(ref System.Web.UI.WebControls.LinkButton ctrl, String strRedirect, String ventanatitle, bool bAddOnReturn)
        {
            WebControl wbCtrl = (WebControl)ctrl;
            OpenNewWindowOnClick(ref wbCtrl, strRedirect, ventanatitle, bAddOnReturn);
        }
        public static void OpenNewWindowOnClick(ref System.Web.UI.WebControls.WebControl ctrl, String strRedirect, String ventanatitle, bool bAddOnReturn)
        {
            if (ctrl == null)
            {
                return;
            }
            String sOnClick = "javascript:url('";
            sOnClick += strRedirect + "', '";
            sOnClick += ventanatitle;
            sOnClick += "',200,200,300,300);";
            if (bAddOnReturn)
            {
                sOnClick += "return false;";
            }
            ctrl.Attributes.Add("onClick", sOnClick);
        }
        public static void AddDisableWhenClick(ref System.Web.UI.WebControls.Button ctrl, Page pPage)
        {
            if (ctrl != null)
            {
                String sOnClick;
                sOnClick = "return DisableButton('" + ctrl.ClientID + "');";
                //sOnClick += pPage.GetPostBackEventReference(ctrl).ToString();
                if (ctrl.Attributes["OnClick"] != null)
                {
                    sOnClick += ctrl.Attributes["OnClick"];
                    ctrl.Attributes["OnClick"] = sOnClick;
                }
                else
                {
                    ctrl.Attributes.Add("OnClick", sOnClick);
                }
                
                
            }
        }

        public static void AddToogleDivWithChk(ref System.Web.UI.WebControls.CheckBox chk, HtmlGenericControl wcDiv)
        {
            try
            {
                String sOnchangeAB = "ShowHideDivOnChkBox('";
                sOnchangeAB += chk.ClientID + "','";
                sOnchangeAB += wcDiv.ClientID + "')";
                chk.Attributes.Add("onclick", sOnchangeAB);
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Error agregando js para las capas en control", "AddToogleDivWithChk", ref ex);
            }
        }

        public static void ShowHideDivIfChk(ref HtmlGenericControl wcDiv, System.Web.UI.WebControls.CheckBox chk)
        {
            try
            {
                wcDiv.Attributes.Add("style", chk.Checked ? "visibility: visible; display: block" : "visibility: hidden; display: none");
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "Error agregando style para la capas", "ShowHideDivIfChk", ref ex);
            }
        }
    }
}
