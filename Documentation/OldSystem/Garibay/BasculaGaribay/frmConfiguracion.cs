using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BasculaGaribay
{
    public partial class frmConfiguracion : Form
    {
        String url;
        public frmConfiguracion()
        {
            InitializeComponent();
        }
        public frmConfiguracion(String url){
            this.url = url;
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            this.txtPuerto.Enabled = false;
            if (chkBoxModifiPuerto.Checked)
            {
                this.txtPuerto.Enabled = true;
            }
            
        }

        private void Configuracion_Load(object sender, EventArgs e)
        {

        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            this.url = this.txtIPNombreServer.Text;
            this.Close();
        }

        public String Url
        {
            get
            {
                return this.url;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
    }
}
