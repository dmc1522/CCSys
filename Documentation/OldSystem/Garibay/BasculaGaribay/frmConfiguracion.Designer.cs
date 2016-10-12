namespace BasculaGaribay
{
    partial class frmConfiguracion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNombreServer = new System.Windows.Forms.Label();
            this.txtIPNombreServer = new System.Windows.Forms.TextBox();
            this.lblPuerto = new System.Windows.Forms.Label();
            this.txtPuerto = new System.Windows.Forms.TextBox();
            this.chkBoxModifiPuerto = new System.Windows.Forms.CheckBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNombreServer
            // 
            this.lblNombreServer.AutoSize = true;
            this.lblNombreServer.Location = new System.Drawing.Point(12, 13);
            this.lblNombreServer.Name = "lblNombreServer";
            this.lblNombreServer.Size = new System.Drawing.Size(49, 13);
            this.lblNombreServer.TabIndex = 0;
            this.lblNombreServer.Text = "Servidor:";
            // 
            // txtIPNombreServer
            // 
            this.txtIPNombreServer.Location = new System.Drawing.Point(12, 29);
            this.txtIPNombreServer.Name = "txtIPNombreServer";
            this.txtIPNombreServer.Size = new System.Drawing.Size(301, 20);
            this.txtIPNombreServer.TabIndex = 1;
            this.txtIPNombreServer.Text = "http://www.ws.corporativogaribay.com/Servicios.asmx";
            // 
            // lblPuerto
            // 
            this.lblPuerto.AutoSize = true;
            this.lblPuerto.Location = new System.Drawing.Point(15, 66);
            this.lblPuerto.Name = "lblPuerto";
            this.lblPuerto.Size = new System.Drawing.Size(38, 13);
            this.lblPuerto.TabIndex = 2;
            this.lblPuerto.Text = "Puerto";
            this.lblPuerto.Visible = false;
            // 
            // txtPuerto
            // 
            this.txtPuerto.Enabled = false;
            this.txtPuerto.Location = new System.Drawing.Point(71, 59);
            this.txtPuerto.Name = "txtPuerto";
            this.txtPuerto.Size = new System.Drawing.Size(54, 20);
            this.txtPuerto.TabIndex = 3;
            this.txtPuerto.Text = "80";
            this.txtPuerto.Visible = false;
            // 
            // chkBoxModifiPuerto
            // 
            this.chkBoxModifiPuerto.AutoSize = true;
            this.chkBoxModifiPuerto.Location = new System.Drawing.Point(131, 61);
            this.chkBoxModifiPuerto.Name = "chkBoxModifiPuerto";
            this.chkBoxModifiPuerto.Size = new System.Drawing.Size(103, 17);
            this.chkBoxModifiPuerto.TabIndex = 4;
            this.chkBoxModifiPuerto.Text = "Modificar Puerto";
            this.chkBoxModifiPuerto.UseVisualStyleBackColor = true;
            this.chkBoxModifiPuerto.Visible = false;
            this.chkBoxModifiPuerto.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(71, 114);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 5;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(158, 114);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmConfiguracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 149);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.chkBoxModifiPuerto);
            this.Controls.Add(this.txtPuerto);
            this.Controls.Add(this.lblPuerto);
            this.Controls.Add(this.txtIPNombreServer);
            this.Controls.Add(this.lblNombreServer);
            this.Name = "frmConfiguracion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración";
            this.Load += new System.EventHandler(this.Configuracion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNombreServer;
        private System.Windows.Forms.TextBox txtIPNombreServer;
        private System.Windows.Forms.Label lblPuerto;
        private System.Windows.Forms.TextBox txtPuerto;
        private System.Windows.Forms.CheckBox chkBoxModifiPuerto;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
    }
}