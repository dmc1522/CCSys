namespace BasculaGaribay
{
    partial class frmListProductores
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
            this.dgvProductores = new System.Windows.Forms.DataGridView();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.pnlFiltros = new System.Windows.Forms.Panel();
            this.chkColonia = new System.Windows.Forms.CheckBox();
            this.chkbproductorid = new System.Windows.Forms.CheckBox();
            this.chkbFM = new System.Windows.Forms.CheckBox();
            this.chkConyugue = new System.Windows.Forms.CheckBox();
            this.chkbFechaI = new System.Windows.Forms.CheckBox();
            this.chkbRegimen = new System.Windows.Forms.CheckBox();
            this.chkbEcivil = new System.Windows.Forms.CheckBox();
            this.chkbEmail = new System.Windows.Forms.CheckBox();
            this.chkBFax = new System.Windows.Forms.CheckBox();
            this.chkbCel = new System.Windows.Forms.CheckBox();
            this.chkbTelTabajo = new System.Windows.Forms.CheckBox();
            this.chkbTel = new System.Windows.Forms.CheckBox();
            this.chkbSexo = new System.Windows.Forms.CheckBox();
            this.chkbRFC = new System.Windows.Forms.CheckBox();
            this.chkbCP = new System.Windows.Forms.CheckBox();
            this.chkbEstado = new System.Windows.Forms.CheckBox();
            this.chkbMun = new System.Windows.Forms.CheckBox();
            this.chkPoblacion = new System.Windows.Forms.CheckBox();
            this.chkbDomicilio = new System.Windows.Forms.CheckBox();
            this.chkbCurp = new System.Windows.Forms.CheckBox();
            this.chkbife = new System.Windows.Forms.CheckBox();
            this.btnSelTodas = new System.Windows.Forms.Button();
            this.btnQuitSel = new System.Windows.Forms.Button();
            this.btnActualiza = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductores)).BeginInit();
            this.pnlFiltros.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvProductores
            // 
            this.dgvProductores.AllowUserToAddRows = false;
            this.dgvProductores.AllowUserToDeleteRows = false;
            this.dgvProductores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProductores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvProductores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductores.Location = new System.Drawing.Point(12, 153);
            this.dgvProductores.Name = "dgvProductores";
            this.dgvProductores.ReadOnly = true;
            this.dgvProductores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductores.Size = new System.Drawing.Size(838, 332);
            this.dgvProductores.TabIndex = 3;
            this.dgvProductores.SelectionChanged += new System.EventHandler(this.dgvProductores_SelectionChanged);
            this.dgvProductores.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductores_CellContentClick);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(295, 491);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(78, 32);
            this.btnAgregar.TabIndex = 4;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnModificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificar.Location = new System.Drawing.Point(391, 491);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(78, 32);
            this.btnModificar.TabIndex = 5;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Visible = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.Location = new System.Drawing.Point(489, 491);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(78, 32);
            this.btnEliminar.TabIndex = 6;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlFiltros.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFiltros.Controls.Add(this.chkColonia);
            this.pnlFiltros.Controls.Add(this.chkbproductorid);
            this.pnlFiltros.Controls.Add(this.chkbFM);
            this.pnlFiltros.Controls.Add(this.chkConyugue);
            this.pnlFiltros.Controls.Add(this.chkbFechaI);
            this.pnlFiltros.Controls.Add(this.chkbRegimen);
            this.pnlFiltros.Controls.Add(this.chkbEcivil);
            this.pnlFiltros.Controls.Add(this.chkbEmail);
            this.pnlFiltros.Controls.Add(this.chkBFax);
            this.pnlFiltros.Controls.Add(this.chkbCel);
            this.pnlFiltros.Controls.Add(this.chkbTelTabajo);
            this.pnlFiltros.Controls.Add(this.chkbTel);
            this.pnlFiltros.Controls.Add(this.chkbSexo);
            this.pnlFiltros.Controls.Add(this.chkbRFC);
            this.pnlFiltros.Controls.Add(this.chkbCP);
            this.pnlFiltros.Controls.Add(this.chkbEstado);
            this.pnlFiltros.Controls.Add(this.chkbMun);
            this.pnlFiltros.Controls.Add(this.chkPoblacion);
            this.pnlFiltros.Controls.Add(this.chkbDomicilio);
            this.pnlFiltros.Controls.Add(this.chkbCurp);
            this.pnlFiltros.Controls.Add(this.chkbife);
            this.pnlFiltros.Location = new System.Drawing.Point(12, 12);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Size = new System.Drawing.Size(838, 106);
            this.pnlFiltros.TabIndex = 0;
            // 
            // chkColonia
            // 
            this.chkColonia.AutoSize = true;
            this.chkColonia.Location = new System.Drawing.Point(219, 13);
            this.chkColonia.Name = "chkColonia";
            this.chkColonia.Size = new System.Drawing.Size(61, 17);
            this.chkColonia.TabIndex = 6;
            this.chkColonia.Text = "Colonia";
            this.chkColonia.UseVisualStyleBackColor = true;
            // 
            // chkbproductorid
            // 
            this.chkbproductorid.AutoSize = true;
            this.chkbproductorid.Location = new System.Drawing.Point(714, 13);
            this.chkbproductorid.Name = "chkbproductorid";
            this.chkbproductorid.Size = new System.Drawing.Size(103, 17);
            this.chkbproductorid.TabIndex = 18;
            this.chkbproductorid.Text = "ID del Productor";
            this.chkbproductorid.UseVisualStyleBackColor = true;
            // 
            // chkbFM
            // 
            this.chkbFM.AutoSize = true;
            this.chkbFM.Location = new System.Drawing.Point(714, 59);
            this.chkbFM.Name = "chkbFM";
            this.chkbFM.Size = new System.Drawing.Size(118, 17);
            this.chkbFM.TabIndex = 20;
            this.chkbFM.Text = "Última Modificación";
            this.chkbFM.UseVisualStyleBackColor = true;
            // 
            // chkConyugue
            // 
            this.chkConyugue.AutoSize = true;
            this.chkConyugue.Location = new System.Drawing.Point(580, 59);
            this.chkConyugue.Name = "chkConyugue";
            this.chkConyugue.Size = new System.Drawing.Size(74, 17);
            this.chkConyugue.TabIndex = 17;
            this.chkConyugue.Text = "Conyugue";
            this.chkConyugue.UseVisualStyleBackColor = true;
            // 
            // chkbFechaI
            // 
            this.chkbFechaI.AutoSize = true;
            this.chkbFechaI.Location = new System.Drawing.Point(714, 36);
            this.chkbFechaI.Name = "chkbFechaI";
            this.chkbFechaI.Size = new System.Drawing.Size(109, 17);
            this.chkbFechaI.TabIndex = 19;
            this.chkbFechaI.Text = "Fecha de Ingreso";
            this.chkbFechaI.UseVisualStyleBackColor = true;
            // 
            // chkbRegimen
            // 
            this.chkbRegimen.AutoSize = true;
            this.chkbRegimen.Location = new System.Drawing.Point(580, 13);
            this.chkbRegimen.Name = "chkbRegimen";
            this.chkbRegimen.Size = new System.Drawing.Size(68, 17);
            this.chkbRegimen.TabIndex = 15;
            this.chkbRegimen.Text = "Régimen";
            this.chkbRegimen.UseVisualStyleBackColor = true;
            // 
            // chkbEcivil
            // 
            this.chkbEcivil.AutoSize = true;
            this.chkbEcivil.Location = new System.Drawing.Point(580, 36);
            this.chkbEcivil.Name = "chkbEcivil";
            this.chkbEcivil.Size = new System.Drawing.Size(81, 17);
            this.chkbEcivil.TabIndex = 16;
            this.chkbEcivil.Text = "Estado Civil";
            this.chkbEcivil.UseVisualStyleBackColor = true;
            // 
            // chkbEmail
            // 
            this.chkbEmail.AutoSize = true;
            this.chkbEmail.Location = new System.Drawing.Point(477, 13);
            this.chkbEmail.Name = "chkbEmail";
            this.chkbEmail.Size = new System.Drawing.Size(55, 17);
            this.chkbEmail.TabIndex = 12;
            this.chkbEmail.Text = "E-Mail";
            this.chkbEmail.UseVisualStyleBackColor = true;
            // 
            // chkBFax
            // 
            this.chkBFax.AutoSize = true;
            this.chkBFax.Location = new System.Drawing.Point(477, 59);
            this.chkBFax.Name = "chkBFax";
            this.chkBFax.Size = new System.Drawing.Size(43, 17);
            this.chkBFax.TabIndex = 14;
            this.chkBFax.Text = "Fax";
            this.chkBFax.UseVisualStyleBackColor = true;
            // 
            // chkbCel
            // 
            this.chkbCel.AutoSize = true;
            this.chkbCel.Location = new System.Drawing.Point(477, 36);
            this.chkbCel.Name = "chkbCel";
            this.chkbCel.Size = new System.Drawing.Size(58, 17);
            this.chkbCel.TabIndex = 13;
            this.chkbCel.Text = "Celular";
            this.chkbCel.UseVisualStyleBackColor = true;
            // 
            // chkbTelTabajo
            // 
            this.chkbTelTabajo.AutoSize = true;
            this.chkbTelTabajo.Location = new System.Drawing.Point(344, 59);
            this.chkbTelTabajo.Name = "chkbTelTabajo";
            this.chkbTelTabajo.Size = new System.Drawing.Size(122, 17);
            this.chkbTelTabajo.TabIndex = 11;
            this.chkbTelTabajo.Text = "Teléfono de Trabajo";
            this.chkbTelTabajo.UseVisualStyleBackColor = true;
            // 
            // chkbTel
            // 
            this.chkbTel.AutoSize = true;
            this.chkbTel.Location = new System.Drawing.Point(344, 36);
            this.chkbTel.Name = "chkbTel";
            this.chkbTel.Size = new System.Drawing.Size(68, 17);
            this.chkbTel.TabIndex = 10;
            this.chkbTel.Text = "Teléfono";
            this.chkbTel.UseVisualStyleBackColor = true;
            // 
            // chkbSexo
            // 
            this.chkbSexo.AutoSize = true;
            this.chkbSexo.Location = new System.Drawing.Point(344, 13);
            this.chkbSexo.Name = "chkbSexo";
            this.chkbSexo.Size = new System.Drawing.Size(50, 17);
            this.chkbSexo.TabIndex = 9;
            this.chkbSexo.Text = "Sexo\r\n";
            this.chkbSexo.UseVisualStyleBackColor = true;
            // 
            // chkbRFC
            // 
            this.chkbRFC.AutoSize = true;
            this.chkbRFC.Location = new System.Drawing.Point(219, 59);
            this.chkbRFC.Name = "chkbRFC";
            this.chkbRFC.Size = new System.Drawing.Size(47, 17);
            this.chkbRFC.TabIndex = 8;
            this.chkbRFC.Text = "RFC";
            this.chkbRFC.UseVisualStyleBackColor = true;
            // 
            // chkbCP
            // 
            this.chkbCP.AutoSize = true;
            this.chkbCP.Location = new System.Drawing.Point(219, 36);
            this.chkbCP.Name = "chkbCP";
            this.chkbCP.Size = new System.Drawing.Size(91, 17);
            this.chkbCP.TabIndex = 7;
            this.chkbCP.Text = "Codigo Postal";
            this.chkbCP.UseVisualStyleBackColor = true;
            // 
            // chkbEstado
            // 
            this.chkbEstado.AutoSize = true;
            this.chkbEstado.Location = new System.Drawing.Point(114, 59);
            this.chkbEstado.Name = "chkbEstado";
            this.chkbEstado.Size = new System.Drawing.Size(59, 17);
            this.chkbEstado.TabIndex = 5;
            this.chkbEstado.Text = "Estado";
            this.chkbEstado.UseVisualStyleBackColor = true;
            // 
            // chkbMun
            // 
            this.chkbMun.AutoSize = true;
            this.chkbMun.Location = new System.Drawing.Point(114, 36);
            this.chkbMun.Name = "chkbMun";
            this.chkbMun.Size = new System.Drawing.Size(71, 17);
            this.chkbMun.TabIndex = 4;
            this.chkbMun.Text = "Municipio";
            this.chkbMun.UseVisualStyleBackColor = true;
            // 
            // chkPoblacion
            // 
            this.chkPoblacion.AutoSize = true;
            this.chkPoblacion.Location = new System.Drawing.Point(114, 13);
            this.chkPoblacion.Name = "chkPoblacion";
            this.chkPoblacion.Size = new System.Drawing.Size(73, 17);
            this.chkPoblacion.TabIndex = 3;
            this.chkPoblacion.Text = "Población";
            this.chkPoblacion.UseVisualStyleBackColor = true;
            // 
            // chkbDomicilio
            // 
            this.chkbDomicilio.AutoSize = true;
            this.chkbDomicilio.Location = new System.Drawing.Point(16, 59);
            this.chkbDomicilio.Name = "chkbDomicilio";
            this.chkbDomicilio.Size = new System.Drawing.Size(68, 17);
            this.chkbDomicilio.TabIndex = 2;
            this.chkbDomicilio.Text = "Domicilio";
            this.chkbDomicilio.UseVisualStyleBackColor = true;
            // 
            // chkbCurp
            // 
            this.chkbCurp.AutoSize = true;
            this.chkbCurp.Location = new System.Drawing.Point(16, 36);
            this.chkbCurp.Name = "chkbCurp";
            this.chkbCurp.Size = new System.Drawing.Size(56, 17);
            this.chkbCurp.TabIndex = 1;
            this.chkbCurp.Text = "CURP";
            this.chkbCurp.UseVisualStyleBackColor = true;
            // 
            // chkbife
            // 
            this.chkbife.AutoSize = true;
            this.chkbife.Location = new System.Drawing.Point(16, 13);
            this.chkbife.Name = "chkbife";
            this.chkbife.Size = new System.Drawing.Size(42, 17);
            this.chkbife.TabIndex = 0;
            this.chkbife.Text = "IFE";
            this.chkbife.UseVisualStyleBackColor = true;
            // 
            // btnSelTodas
            // 
            this.btnSelTodas.Location = new System.Drawing.Point(169, 124);
            this.btnSelTodas.Name = "btnSelTodas";
            this.btnSelTodas.Size = new System.Drawing.Size(122, 23);
            this.btnSelTodas.TabIndex = 1;
            this.btnSelTodas.Text = "Seleccionar Todas";
            this.btnSelTodas.UseVisualStyleBackColor = true;
            this.btnSelTodas.Click += new System.EventHandler(this.btnSelTodas_Click);
            // 
            // btnQuitSel
            // 
            this.btnQuitSel.Location = new System.Drawing.Point(309, 124);
            this.btnQuitSel.Name = "btnQuitSel";
            this.btnQuitSel.Size = new System.Drawing.Size(131, 23);
            this.btnQuitSel.TabIndex = 2;
            this.btnQuitSel.Text = "Quitar Selección";
            this.btnQuitSel.UseVisualStyleBackColor = true;
            this.btnQuitSel.Click += new System.EventHandler(this.btnQuitSel_Click);
            // 
            // btnActualiza
            // 
            this.btnActualiza.Location = new System.Drawing.Point(12, 124);
            this.btnActualiza.Name = "btnActualiza";
            this.btnActualiza.Size = new System.Drawing.Size(140, 23);
            this.btnActualiza.TabIndex = 0;
            this.btnActualiza.Text = "Actualizar Lista";
            this.btnActualiza.UseVisualStyleBackColor = true;
            this.btnActualiza.Click += new System.EventHandler(this.btnActualiza_Click);
            // 
            // frmListProductores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 535);
            this.Controls.Add(this.btnActualiza);
            this.Controls.Add(this.btnSelTodas);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.btnQuitSel);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.dgvProductores);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(814, 573);
            this.Name = "frmListProductores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Productores";
            this.Load += new System.EventHandler(this.frmListProductores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductores)).EndInit();
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProductores;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.CheckBox chkbEstado;
        private System.Windows.Forms.CheckBox chkbMun;
        private System.Windows.Forms.CheckBox chkPoblacion;
        private System.Windows.Forms.CheckBox chkbDomicilio;
        private System.Windows.Forms.CheckBox chkbCurp;
        private System.Windows.Forms.CheckBox chkbife;
        private System.Windows.Forms.CheckBox chkbCel;
        private System.Windows.Forms.CheckBox chkbTelTabajo;
        private System.Windows.Forms.CheckBox chkbTel;
        private System.Windows.Forms.CheckBox chkbSexo;
        private System.Windows.Forms.CheckBox chkbRFC;
        private System.Windows.Forms.CheckBox chkbCP;
        private System.Windows.Forms.CheckBox chkbEcivil;
        private System.Windows.Forms.CheckBox chkbEmail;
        private System.Windows.Forms.CheckBox chkBFax;
        private System.Windows.Forms.CheckBox chkbFM;
        private System.Windows.Forms.CheckBox chkbFechaI;
        private System.Windows.Forms.CheckBox chkbRegimen;
        private System.Windows.Forms.Button btnSelTodas;
        private System.Windows.Forms.Button btnQuitSel;
        private System.Windows.Forms.Button btnActualiza;
        private System.Windows.Forms.CheckBox chkbproductorid;
        private System.Windows.Forms.CheckBox chkColonia;
        private System.Windows.Forms.CheckBox chkConyugue;
    }
}