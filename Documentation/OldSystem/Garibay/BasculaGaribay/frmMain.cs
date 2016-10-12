using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace BasculaGaribay
{
    public partial class Login : Form
    {
        private Boolean relogin;
        public Login()
        {
            relogin = false;
            InitializeComponent();
        }
        public Login(int relog)
        {
            relogin = true;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
#if DEBUG
            this.txtUsuario.Text = "manuelq";
            this.txtPassword.Text = "M4nu3l#1";
#endif
        }
        
        private bool nonulls()
        {
            String mensajeError = "";
            if(this.txtUsuario.Text==""){
                mensajeError = " El Campo Usuario NO debe de estar vacio \n";

            }
            if(this.txtPassword.Text==""){
                mensajeError += " El Campo de la Contraseña NO debe de estar vacio";
            }
            if(mensajeError=="")
            {
                return true;
            }
            else 
            {
                MessageBox.Show(mensajeError, "ERROR!!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false; 
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
#if DEBUG
            WSConnector.Instance.Url = "http://www.ws.cheliskis.com/Servicios.asmx";
#else
            WSConnector.Instance.Url = "http://www.ws.cheliskis.com/Servicios.asmx";
#endif

            //validate user and pass are not empty
            try
            {
                this.button1.Enabled = this.button2.Enabled = false;
                if (nonulls())
                {


                    WSConnector.Instance.Username = this.txtUsuario.Text;
                    WSConnector.Instance.Password = this.txtPassword.Text;
                    if (WSConnector.Instance.Login())
                    {
                        //MessageBox.Show("Nombre de Usuario y contraseña Correctos", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (!relogin)
                        {
                            Principal myform = new Principal();
                            this.Hide();
                            myform.ShowDialog();
                        }
                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("No se pudo iniciar session en el sistema" + System.Environment.NewLine + "Nombre de usuario o contraseña incorrectos.",
                            "Error",
                             MessageBoxButtons.OK,
                              MessageBoxIcon.Stop);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR!!");
            }
            finally
            {
                this.button1.Enabled = this.button2.Enabled = true;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfigurations_Click(object sender, EventArgs e)
        {
            try
            {
                frmConfiguracion cfg = new frmConfiguracion();
                cfg.ShowDialog(this);
                WSConnector.Instance.Url = cfg.Url;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"ERROR!!");
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                this.button1_Click(sender, null);
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                this.txtPassword.Focus();
            }
        }

    }
}
