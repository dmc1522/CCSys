using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace Garibay
{
    public class EMailUtils
    {
        //Members must be properties but due time issues :-) I made public.
        public static bool SendTextEmail(String sTo, String sSubject, String sContent, bool bIsbodyHTML)
        {
            bool bRes = false;
            try
            {
                bRes = EMailUtils.SendTextEmail(sTo, sSubject, sContent, bIsbodyHTML, "");
            }
            catch 
            {
                //Logger.Instance.LogException(Logger.typeUserActions.SELECT, "sending email", ref ex);
            }
            return bRes;
        }
        public static bool SendTextEmail(String sTo, String sSubject, String sContent, bool bIsbodyHTML, String sBCC)
        {
            bool bResult = false;

            try
            {
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress("noreply@corporativogaribay.com");
                correo.To.Add(sTo);
                if (sBCC.Length > 0)
                {
                    correo.Bcc.Add(sBCC);
                }
                correo.Subject = sSubject;
                correo.IsBodyHtml = bIsbodyHTML;
//                 if (bIsbodyHTML)
//                 {
//                     correo.Body = HttpUtility.HtmlDecode(sContent);
//                 }
//                 else
//                 {
                    correo.Body = sContent;
                //}
                SmtpClient smtp = new SmtpClient("localhost");
#if DEBUG
                ;
#else
                smtp.Send(correo);
#endif
                bResult = true;
            }
            catch 
            {
                //Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "Error enviando mail. to:" + sTo + " subject:" + sSubject + " content:" + sContent, "Send Email", ref ex);

            }
            return bResult;
        }

    }
}
