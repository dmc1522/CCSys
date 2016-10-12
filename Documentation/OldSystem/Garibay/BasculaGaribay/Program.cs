using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BasculaGaribay
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //WSConnector.Instance.Inicializa();
            Application.Run(new Login());
        }
    }
}
