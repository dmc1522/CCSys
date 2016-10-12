using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Windows.Forms;

namespace BasculaGaribay
{
    public class Logger 
    {
        private static Logger UniqueInstance = new Logger();
        private StreamWriter sw = null;
        private string FileName = "LogFile.txt";
        public static Logger Instance
        {
            get { return UniqueInstance; }
        }
        private Logger()
        {
            this.FileName = DateTime.Now.ToString("dd-MMM-yyyy") + ".txt";
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            this.LogException((Exception)e.ExceptionObject);
        }

        void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            this.LogException(e.Exception);
        }
        public string LogFileName
        {
            get { return Path.GetFullPath(this.FileName); }
        }
        public void LogMessage(string sMessage)
        {
            if (sMessage != null)
            {
                string FunctionName = string.Empty;
                string FileName = string.Empty;
                int LineNumber = -1;
                StackFrame sf = new StackFrame(1, true);
                MethodBase mb = sf.GetMethod();
                FunctionName = mb.Name;
                FileName = sf.GetFileName();
                if (FileName != null)
                {
                    FileName = FileName.Substring(FileName.LastIndexOf('\\') + 1);
                }
                LineNumber = sf.GetFileLineNumber();
                StringBuilder MessageToWrite = new StringBuilder();
                MessageToWrite.Append(DateTime.Now.ToString("HH:mm:ss.ffff"));
                MessageToWrite.Append(" ");
                MessageToWrite.Append(FileName);
                MessageToWrite.Append("!");
                MessageToWrite.Append(FunctionName);
                MessageToWrite.Append(":");
                MessageToWrite.Append(LineNumber.ToString());
                MessageToWrite.Append("==>\t");
                MessageToWrite.Append(sMessage);
                WriteMessageInFile(MessageToWrite.ToString());
            }
        }

        public void LogException(Exception ex)
        {
            try
            {
                string FunctionName = string.Empty;
                string FileName = string.Empty;
                int LineNumber = -1;
                StackFrame sf = new StackFrame(2, true);
                MethodBase mb = sf.GetMethod();
                FunctionName = mb.Name;
                FileName = sf.GetFileName();
                if (FileName != null)
                {
                    FileName = FileName.Substring(FileName.LastIndexOf('\\') + 1);
                }
                LineNumber = sf.GetFileLineNumber();
                StringBuilder MessageToWrite = new StringBuilder();
                MessageToWrite.Append(DateTime.Now.ToString("HH:mm:ss.ffff"));
                MessageToWrite.Append(" ");
                MessageToWrite.Append(FileName);
                MessageToWrite.Append("!");
                MessageToWrite.Append(FunctionName);
                MessageToWrite.Append(":");
                MessageToWrite.Append(LineNumber.ToString());
                MessageToWrite.Append("==>\t");
                MessageToWrite.Append("Exception Ocurred: ");
                MessageToWrite.Append(ex.Message);
                MessageToWrite.Append(" StackTrace:");
                MessageToWrite.Append(ex.StackTrace);
                WriteMessageInFile(MessageToWrite.ToString());
                EmailUtils eu = new EmailUtils();
                eu.SendEmail("BASCULA GARIBAY: Exception occurred", MessageToWrite.ToString());
            }
            catch{}
            
        }


        public void LogExceptionWithMsg(Exception ex, string msg)
        {
            try
            {
                string FunctionName = string.Empty;
                string FileName = string.Empty;
                int LineNumber = -1;
                StackFrame sf = new StackFrame(2, true);
                MethodBase mb = sf.GetMethod();
                FunctionName = mb.Name;
                FileName = sf.GetFileName();
                if (FileName != null)
                {
                    FileName = FileName.Substring(FileName.LastIndexOf('\\') + 1);
                }
                LineNumber = sf.GetFileLineNumber();
                StringBuilder MessageToWrite = new StringBuilder();
                MessageToWrite.Append(DateTime.Now.ToString("HH:mm:ss.ffff"));
                MessageToWrite.Append(" ");
                MessageToWrite.Append(FileName);
                MessageToWrite.Append("!");
                MessageToWrite.Append(FunctionName);
                MessageToWrite.Append(":");
                MessageToWrite.Append(LineNumber.ToString());
                MessageToWrite.Append("==>\t");
                MessageToWrite.Append("Exception Ocurred: ");
                MessageToWrite.Append(ex.Message);
                MessageToWrite.Append(" StackTrace:");
                MessageToWrite.Append(ex.StackTrace);
                MessageToWrite.Append(System.Environment.NewLine);
                MessageToWrite.Append("extra data:");
                MessageToWrite.Append(System.Environment.NewLine);
                MessageToWrite.Append(msg);
                WriteMessageInFile(MessageToWrite.ToString());
                EmailUtils eu = new EmailUtils();
                eu.SendEmail("Exception occurred", MessageToWrite.ToString());
            }
            catch { }

        }

        public void WriteMessageInFile(string Message)
        {
            try
            {
                if (sw == null)
                {
                    sw = new StreamWriter(this.FileName, true);
                }
                sw.WriteLine(Message);
                sw.Flush();
            }
            catch {}
        }
    }
}
