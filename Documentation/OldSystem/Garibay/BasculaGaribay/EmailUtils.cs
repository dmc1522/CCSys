using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Mail;

namespace BasculaGaribay
{
    public class EmailUtils
    {
        string _gMailAccount = "sistemacontrolparafinanciera@gmail.com";
        string _password = "chiludos";
        string _to = "melvinquintero@hotmail.com, cheliskis@gmail.com";
        string _subject;
        string _message;
	    public string GMailAccount
	    {
		    get { return _gMailAccount; }
		    set { _gMailAccount = value; }
	    }
        public string Password
        {
          get { return _password; }
          set { _password = value; }
        }
	    public string To
	    {
		    get { return _to; }
		    set { _to = value; }
	    }
        
	    public string Subject
	    {
		    get { return _subject; }
		    set { _subject = value; }
	    }
        
	    public string Message
	    {
		    get { return _message; }
		    set { _message = value; }
	    }
        public void SendEmail(string gMailAccount, string password, string to, string subject, string message)
        {
            this._gMailAccount = gMailAccount;
            this._password = password;
            this._to = to;
            this._subject = subject;
            this._message = message;
            Thread t = new Thread(new ThreadStart(this.SendEmailThread));
            t.Priority = ThreadPriority.BelowNormal;
            t.Start();
        }

        public void SendEmail(string subject, string message)
        {
            this._subject = subject;
            this._message = message;
            Thread t = new Thread(new ThreadStart(this.SendEmailThread));
            t.Priority = ThreadPriority.BelowNormal;
            t.Start();
        }

        private void SendEmailThread()
        {
#if DEBUG
            ;
#else
            try
            {
                NetworkCredential loginInfo = new NetworkCredential(this._gMailAccount, this._password);
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(_gMailAccount);
                msg.To.Add(new MailAddress("melvinquintero@hotmail.com"));
                msg.To.Add(new MailAddress("cheliskis@gmail.com"));
                msg.Subject = _subject;
                msg.Body = _message;
                msg.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = loginInfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);
            }
            catch
            {

            }
            finally
            {
            }
#endif
        }
    }
}
