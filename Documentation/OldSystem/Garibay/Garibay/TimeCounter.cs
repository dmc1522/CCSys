using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garibay
{
    public class TimeCounter
    {
        private TimeSpan stop;
        private TimeSpan start;
        private String sFunction = "";
        public TimeCounter()
        {
            this.sFunction = "Funcion sin nombre";
        }

        public TimeCounter(String sFun)
        {
            this.sFunction = sFun;
        }
        public void Start(String sFun)
        {
            this.sFunction = sFun;
            this.start = new TimeSpan(Utils.Now.Ticks);
        }
        public void Start()
        {
            this.start = new TimeSpan(Utils.Now.Ticks);
        }
        public void Stop()
        {
            this.stop = new TimeSpan(Utils.Now.Ticks);
        }
        public void StopAndLogTime()
        {
            this.Stop();
            this.LogTime();
        }
        public void LogTime()
        {
            Logger.Instance.LogMessage(Logger.typeLogMessage.INFO, Logger.typeUserActions.SELECT, new BasePage().UserID, this.sFunction + ": "+ stop.Subtract(start).TotalMilliseconds.ToString() + " ms", "Counter function");
        }
    }
}
