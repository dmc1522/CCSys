namespace BasculaGaribay
{
    partial class FormBoletasSinSalir
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtCantidadBoletas = new System.Windows.Forms.TextBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.dgvBoletas = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnActualizarDatos = new System.Windows.Forms.Button();
            this.btnLimpiarFiltros = new System.Windows.Forms.Button();
            this.cmbBodegas = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTicket = new System.Windows.Forms.TextBox();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.cmbCiclos = new System.Windows.Forms.ComboBox();
            this.lblNoBoleta = new System.Windows.Forms.Label();
            this.lblCiclo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoletas)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(791, 520);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Boletas Leidas:";
            // 
            // txtCantidadBoletas
            // 
            this.txtCantidadBoletas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCantidadBoletas.Location = new System.Drawing.Point(876, 517);
            this.txtCantidadBoletas.Name = "txtCantidadBoletas";
            this.txtCantidadBoletas.ReadOnly = true;
            this.txtCantidadBoletas.Size = new System.Drawing.Size(71, 20);
            this.txtCantidadBoletas.TabIndex = 12;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.Location = new System.Drawing.Point(582, 511);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 31);
            this.btnEliminar.TabIndex = 11;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnModificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificar.Location = new System.Drawing.Point(480, 511);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(75, 31);
            this.btnModificar.TabIndex = 10;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(376, 511);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(76, 31);
            this.btnAgregar.TabIndex = 9;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // dgvBoletas
            // 
            this.dgvBoletas.AllowUserToAddRows = false;
            this.dgvBoletas.AllowUserToDeleteRows = false;
            this.dgvBoletas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBoletas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvBoletas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBoletas.Location = new System.Drawing.Point(12, 125);
            this.dgvBoletas.Name = "dgvBoletas";
            this.dgvBoletas.ReadOnly = true;
            this.dgvBoletas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBoletas.Size = new System.Drawing.Size(1031, 380);
            this.dgvBoletas.TabIndex = 8;
            this.dgvBoletas.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBoletas_CellContentDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.Controls.Add(this.btnActualizarDatos);
            this.panel1.Controls.Add(this.btnLimpiarFiltros);
            this.panel1.Controls.Add(this.cmbBodegas);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtTicket);
            this.panel1.Controls.Add(this.btnFiltrar);
            this.panel1.Controls.Add(this.cmbCiclos);
            this.panel1.Controls.Add(this.lblNoBoleta);
            this.panel1.Controls.Add(this.lblCiclo);
            this.panel1.Location = new System.Drawing.Point(115, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(801, 96);
            this.panel1.TabIndex = 7;
            this.panel1.Tag = "";
            // 
            // btnActualizarDatos
            // 
            this.btnActualizarDatos.Location = new System.Drawing.Point(83, 64);
            this.btnActualizarDatos.Name = "btnActualizarDatos";
            this.btnActualizarDatos.Size = new System.Drawing.Size(116, 23);
            this.btnActualizarDatos.TabIndex = 22;
            this.btnActualizarDatos.Text = "Actualizar Datos";
            this.btnActualizarDatos.UseVisualStyleBackColor = true;
            this.btnActualizarDatos.Click += new System.EventHandler(this.btnActualizarDatos_Click);
            // 
            // btnLimpiarFiltros
            // 
            this.btnLimpiarFiltros.Location = new System.Drawing.Point(303, 62);
            this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            this.btnLimpiarFiltros.Size = new System.Drawing.Size(92, 26);
            this.btnLimpiarFiltros.TabIndex = 21;
            this.btnLimpiarFiltros.Text = "Limpiar Filtros";
            this.btnLimpiarFiltros.UseVisualStyleBackColor = true;
            this.btnLimpiarFiltros.Click += new System.EventHandler(this.btnLimpiarFiltros_Click);
            // 
            // cmbBodegas
            // 
            this.cmbBodegas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBodegas.FormattingEnabled = true;
            this.cmbBodegas.Location = new System.Drawing.Point(258, 9);
            this.cmbBodegas.Name = "cmbBodegas";
            this.cmbBodegas.Size = new System.Drawing.Size(196, 21);
            this.cmbBodegas.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Bodega";
            // 
            // txtTicket
            // 
            this.txtTicket.Location = new System.Drawing.Point(71, 34);
            this.txtTicket.Name = "txtTicket";
            this.txtTicket.Size = new System.Drawing.Size(100, 20);
            this.txtTicket.TabIndex = 10;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(205, 62);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(92, 26);
            this.btnFiltrar.TabIndex = 5;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // cmbCiclos
            // 
            this.cmbCiclos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCiclos.FormattingEnabled = true;
            this.cmbCiclos.Location = new System.Drawing.Point(52, 7);
            this.cmbCiclos.Name = "cmbCiclos";
            this.cmbCiclos.Size = new System.Drawing.Size(117, 21);
            this.cmbCiclos.TabIndex = 7;
            // 
            // lblNoBoleta
            // 
            this.lblNoBoleta.AutoSize = true;
            this.lblNoBoleta.Location = new System.Drawing.Point(1, 37);
            this.lblNoBoleta.Name = "lblNoBoleta";
            this.lblNoBoleta.Size = new System.Drawing.Size(68, 13);
            this.lblNoBoleta.TabIndex = 3;
            this.lblNoBoleta.Text = "# de Ticket :";
            // 
            // lblCiclo
            // 
            this.lblCiclo.AutoSize = true;
            this.lblCiclo.Location = new System.Drawing.Point(3, 12);
            this.lblCiclo.Name = "lblCiclo";
            this.lblCiclo.Size = new System.Drawing.Size(33, 13);
            this.lblCiclo.TabIndex = 1;
            this.lblCiclo.Text = "Ciclo:";
            // 
            // FormBoletasSinSalir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 564);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCantidadBoletas);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.dgvBoletas);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormBoletasSinSalir";
            this.Text = "Lista de Boletas sin salir";
            this.Load += new System.EventHandler(this.FormBoletasSinSalir_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoletas)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCantidadBoletas;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView dgvBoletas;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnActualizarDatos;
        private System.Windows.Forms.Button btnLimpiarFiltros;
        private System.Windows.Forms.ComboBox cmbBodegas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTicket;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.ComboBox cmbCiclos;
        private System.Windows.Forms.Label lblNoBoleta;
        private System.Windows.Forms.Label lblCiclo;

    }
}