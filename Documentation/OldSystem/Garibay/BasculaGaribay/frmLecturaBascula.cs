using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using System.Diagnostics;


namespace BasculaGaribay
{
    public partial class frmLecturaBascula : Form
    {
        Thread lector;
        private delegate void putPeso(String elemento);
        private putPeso delegado;
        SerialPort puertoSerial;
        private int _Peso = 0;
        private bool _KeepThreadRunning = true;
        
        public bool KeepThreadRunning
        {
            get { lock (this) { return _KeepThreadRunning; } }
            set { lock(this) {_KeepThreadRunning = value; }}
        }
        
        public int Peso
        {
            get { lock (this) { return _Peso; } }
            set { lock (this) { _Peso = value; } }
        }

        public frmLecturaBascula(int TipoDePesada)
        {
            try
            {
                InitializeComponent();
                lector = new Thread(new ThreadStart(obtenLectura));
                delegado = new putPeso(escribeEnForm);           
            }
            catch 
            {
            	
            }

        }

        private void frmLecturaBascula_Load(object sender, EventArgs e)
        {
            try
            {
                puertoSerial = new SerialPort("COM1");
                puertoSerial.Open();
                lector.Start();
                
            }
            catch(Exception ex)
            {
#if DEBUG
                MessageBox.Show("Error al leer de báscula. Ex " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
            }
        }
        private void escribeEnForm(String peso)
        {
            try
            {
                this.textBox1.Text = peso;
                this.textBox1.Refresh();
            }
            catch 
            {
            	
            }
        }

        private void obtenLectura()
        {
            int d;
            String a;
            string[] partes;
            try
            {
                while (this.KeepThreadRunning)
                {
                    try
                    {
                        a = puertoSerial.ReadLine();
                        //algoritmo para obtener el peso de la lectura del puerto
                        //Debug.WriteLine(a);
                        partes = a.Split(',');
//                         a = a.Replace("ST,GS,", "");
//                         a = a.Replace("kg\r", "");
                        a = partes[2];
                        a = a.Replace("kg\r", "");
                        if (this.KeepThreadRunning && int.TryParse(a, out d))
                        {
                            this._Peso = d;
                            
                            this.Invoke(delegado, new object[] { d.ToString() });
                            Thread.Sleep(10);
                        }
                    }
                    catch(Exception ex)
                    {
#if DEBUG
                        MessageBox.Show("Error al leer de báscula. Ex " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                       
                    }
                    //Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show("Error al leer de báscula. Ex " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);            	
#endif
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLecturaBascula_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.KeepThreadRunning = false;
                puertoSerial.Close();
                puertoSerial.Dispose();
                this.lector.Abort();
            }
            catch 
            {
            }
        }
    }
}
