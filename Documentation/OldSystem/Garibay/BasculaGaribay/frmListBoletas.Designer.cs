namespace BasculaGaribay
{
    partial class frmListBoletas
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbProveedores = new System.Windows.Forms.ComboBox();
            this.proveedoresBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbProveedorGanado = new System.Windows.Forms.ComboBox();
            this.rdbProveedores = new System.Windows.Forms.RadioButton();
            this.rdbProveedorGanado = new System.Windows.Forms.RadioButton();
            this.rdbCliente = new System.Windows.Forms.RadioButton();
            this.rdbProductor = new System.Windows.Forms.RadioButton();
            this.cmbClientesVentas = new System.Windows.Forms.ComboBox();
            this.btnActualizarDatos = new System.Windows.Forms.Button();
            this.btnLimpiarFiltros = new System.Windows.Forms.Button();
            this.grbfiltrarPorFecha = new System.Windows.Forms.GroupBox();
            this.chkbFiltrarxFecha = new System.Windows.Forms.CheckBox();
            this.DtpkfechaFin = new System.Windows.Forms.DateTimePicker();
            this.lblFEchaFIn = new System.Windows.Forms.Label();
            this.dtpkFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.lblFechaInicio = new System.Windows.Forms.Label();
            this.txtBoleta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbBodegas = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTicket = new System.Windows.Forms.TextBox();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.cmbTipodeBoleta = new System.Windows.Forms.ComboBox();
            this.cmbCiclos = new System.Windows.Forms.ComboBox();
            this.cmbProductos = new System.Windows.Forms.ComboBox();
            this.cmbProductores = new System.Windows.Forms.ComboBox();
            this.lblTipoBoleta = new System.Windows.Forms.Label();
            this.lblNoBoleta = new System.Windows.Forms.Label();
            this.lblProducto = new System.Windows.Forms.Label();
            this.lblCiclo = new System.Windows.Forms.Label();
            this.dgvBoletas = new System.Windows.Forms.DataGridView();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.txtCantidadBoletas = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.proveedoresBindingSource)).BeginInit();
            this.grbfiltrarPorFecha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoletas)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.Controls.Add(this.cmbProveedores);
            this.panel1.Controls.Add(this.cmbProveedorGanado);
            this.panel1.Controls.Add(this.rdbProveedores);
            this.panel1.Controls.Add(this.rdbProveedorGanado);
            this.panel1.Controls.Add(this.rdbCliente);
            this.panel1.Controls.Add(this.rdbProductor);
            this.panel1.Controls.Add(this.cmbClientesVentas);
            this.panel1.Controls.Add(this.btnActualizarDatos);
            this.panel1.Controls.Add(this.btnLimpiarFiltros);
            this.panel1.Controls.Add(this.grbfiltrarPorFecha);
            this.panel1.Controls.Add(this.txtBoleta);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbBodegas);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtTicket);
            this.panel1.Controls.Add(this.btnFiltrar);
            this.panel1.Controls.Add(this.cmbTipodeBoleta);
            this.panel1.Controls.Add(this.cmbCiclos);
            this.panel1.Controls.Add(this.cmbProductos);
            this.panel1.Controls.Add(this.cmbProductores);
            this.panel1.Controls.Add(this.lblTipoBoleta);
            this.panel1.Controls.Add(this.lblNoBoleta);
            this.panel1.Controls.Add(this.lblProducto);
            this.panel1.Controls.Add(this.lblCiclo);
            this.panel1.Location = new System.Drawing.Point(33, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(801, 202);
            this.panel1.TabIndex = 0;
            this.panel1.Tag = "";
            // 
            // cmbProveedores
            // 
            this.cmbProveedores.DataSource = this.proveedoresBindingSource;
            this.cmbProveedores.DisplayMember = "Nombre";
            this.cmbProveedores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProveedores.FormattingEnabled = true;
            this.cmbProveedores.Location = new System.Drawing.Point(111, 92);
            this.cmbProveedores.Name = "cmbProveedores";
            this.cmbProveedores.Size = new System.Drawing.Size(222, 21);
            this.cmbProveedores.TabIndex = 28;
            this.cmbProveedores.ValueMember = "ProveedorID";
            // 
            // proveedoresBindingSource
            // 
            this.proveedoresBindingSource.DataSource = typeof(Proveedores);
            // 
            // cmbProveedorGanado
            // 
            this.cmbProveedorGanado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProveedorGanado.FormattingEnabled = true;
            this.cmbProveedorGanado.Location = new System.Drawing.Point(129, 65);
            this.cmbProveedorGanado.Name = "cmbProveedorGanado";
            this.cmbProveedorGanado.Size = new System.Drawing.Size(204, 21);
            this.cmbProveedorGanado.TabIndex = 28;
            // 
            // rdbProveedores
            // 
            this.rdbProveedores.AutoSize = true;
            this.rdbProveedores.Location = new System.Drawing.Point(12, 93);
            this.rdbProveedores.Name = "rdbProveedores";
            this.rdbProveedores.Size = new System.Drawing.Size(88, 17);
            this.rdbProveedores.TabIndex = 27;
            this.rdbProveedores.Text = "Proveedores:";
            this.rdbProveedores.UseVisualStyleBackColor = true;
            this.rdbProveedores.CheckedChanged += new System.EventHandler(this.rdbCliente_CheckedChanged);
            // 
            // rdbProveedorGanado
            // 
            this.rdbProveedorGanado.AutoSize = true;
            this.rdbProveedorGanado.Location = new System.Drawing.Point(12, 66);
            this.rdbProveedorGanado.Name = "rdbProveedorGanado";
            this.rdbProveedorGanado.Size = new System.Drawing.Size(115, 17);
            this.rdbProveedorGanado.TabIndex = 27;
            this.rdbProveedorGanado.Text = "Proveedor Ganado";
            this.rdbProveedorGanado.UseVisualStyleBackColor = true;
            this.rdbProveedorGanado.CheckedChanged += new System.EventHandler(this.rdbCliente_CheckedChanged);
            // 
            // rdbCliente
            // 
            this.rdbCliente.AutoSize = true;
            this.rdbCliente.Location = new System.Drawing.Point(12, 37);
            this.rdbCliente.Name = "rdbCliente";
            this.rdbCliente.Size = new System.Drawing.Size(57, 17);
            this.rdbCliente.TabIndex = 26;
            this.rdbCliente.Text = "Cliente";
            this.rdbCliente.UseVisualStyleBackColor = true;
            this.rdbCliente.CheckedChanged += new System.EventHandler(this.rdbCliente_CheckedChanged);
            // 
            // rdbProductor
            // 
            this.rdbProductor.AutoSize = true;
            this.rdbProductor.Checked = true;
            this.rdbProductor.Location = new System.Drawing.Point(12, 8);
            this.rdbProductor.Name = "rdbProductor";
            this.rdbProductor.Size = new System.Drawing.Size(71, 17);
            this.rdbProductor.TabIndex = 25;
            this.rdbProductor.TabStop = true;
            this.rdbProductor.Text = "Productor";
            this.rdbProductor.UseVisualStyleBackColor = true;
            this.rdbProductor.CheckedChanged += new System.EventHandler(this.rdbCliente_CheckedChanged);
            // 
            // cmbClientesVentas
            // 
            this.cmbClientesVentas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClientesVentas.FormattingEnabled = true;
            this.cmbClientesVentas.Location = new System.Drawing.Point(111, 38);
            this.cmbClientesVentas.Name = "cmbClientesVentas";
            this.cmbClientesVentas.Size = new System.Drawing.Size(222, 21);
            this.cmbClientesVentas.Sorted = true;
            this.cmbClientesVentas.TabIndex = 24;
            // 
            // btnActualizarDatos
            // 
            this.btnActualizarDatos.Location = new System.Drawing.Point(71, 163);
            this.btnActualizarDatos.Name = "btnActualizarDatos";
            this.btnActualizarDatos.Size = new System.Drawing.Size(116, 23);
            this.btnActualizarDatos.TabIndex = 22;
            this.btnActualizarDatos.Text = "Actualizar Datos";
            this.btnActualizarDatos.UseVisualStyleBackColor = true;
            this.btnActualizarDatos.Click += new System.EventHandler(this.btnActualizarDatos_Click);
            // 
            // btnLimpiarFiltros
            // 
            this.btnLimpiarFiltros.Location = new System.Drawing.Point(291, 161);
            this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            this.btnLimpiarFiltros.Size = new System.Drawing.Size(92, 26);
            this.btnLimpiarFiltros.TabIndex = 21;
            this.btnLimpiarFiltros.Text = "Limpiar Filtros";
            this.btnLimpiarFiltros.UseVisualStyleBackColor = true;
            this.btnLimpiarFiltros.Click += new System.EventHandler(this.btnLimpiarFiltros_Click);
            // 
            // grbfiltrarPorFecha
            // 
            this.grbfiltrarPorFecha.Controls.Add(this.chkbFiltrarxFecha);
            this.grbfiltrarPorFecha.Controls.Add(this.DtpkfechaFin);
            this.grbfiltrarPorFecha.Controls.Add(this.lblFEchaFIn);
            this.grbfiltrarPorFecha.Controls.Add(this.dtpkFechaInicio);
            this.grbfiltrarPorFecha.Controls.Add(this.lblFechaInicio);
            this.grbfiltrarPorFecha.Location = new System.Drawing.Point(517, 85);
            this.grbfiltrarPorFecha.Name = "grbfiltrarPorFecha";
            this.grbfiltrarPorFecha.Size = new System.Drawing.Size(268, 76);
            this.grbfiltrarPorFecha.TabIndex = 20;
            this.grbfiltrarPorFecha.TabStop = false;
            this.grbfiltrarPorFecha.Text = "Filtrar Por Fecha";
            // 
            // chkbFiltrarxFecha
            // 
            this.chkbFiltrarxFecha.AutoSize = true;
            this.chkbFiltrarxFecha.Location = new System.Drawing.Point(10, 36);
            this.chkbFiltrarxFecha.Name = "chkbFiltrarxFecha";
            this.chkbFiltrarxFecha.Size = new System.Drawing.Size(102, 17);
            this.chkbFiltrarxFecha.TabIndex = 19;
            this.chkbFiltrarxFecha.Text = "Filtrar por Fecha";
            this.chkbFiltrarxFecha.UseVisualStyleBackColor = true;
            this.chkbFiltrarxFecha.CheckedChanged += new System.EventHandler(this.chkbFiltrarxFecha_CheckedChanged);
            // 
            // DtpkfechaFin
            // 
            this.DtpkfechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpkfechaFin.Location = new System.Drawing.Point(150, 48);
            this.DtpkfechaFin.Name = "DtpkfechaFin";
            this.DtpkfechaFin.Size = new System.Drawing.Size(108, 20);
            this.DtpkfechaFin.TabIndex = 18;
            // 
            // lblFEchaFIn
            // 
            this.lblFEchaFIn.AutoSize = true;
            this.lblFEchaFIn.Location = new System.Drawing.Point(126, 54);
            this.lblFEchaFIn.Name = "lblFEchaFIn";
            this.lblFEchaFIn.Size = new System.Drawing.Size(17, 13);
            this.lblFEchaFIn.TabIndex = 15;
            this.lblFEchaFIn.Text = "A:";
            // 
            // dtpkFechaInicio
            // 
            this.dtpkFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpkFechaInicio.Location = new System.Drawing.Point(150, 19);
            this.dtpkFechaInicio.Name = "dtpkFechaInicio";
            this.dtpkFechaInicio.Size = new System.Drawing.Size(108, 20);
            this.dtpkFechaInicio.TabIndex = 17;
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.Location = new System.Drawing.Point(119, 24);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(25, 13);
            this.lblFechaInicio.TabIndex = 16;
            this.lblFechaInicio.Text = "DE:";
            // 
            // txtBoleta
            // 
            this.txtBoleta.Location = new System.Drawing.Point(689, 43);
            this.txtBoleta.Name = "txtBoleta";
            this.txtBoleta.Size = new System.Drawing.Size(100, 20);
            this.txtBoleta.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(619, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "# de boleta:";
            // 
            // cmbBodegas
            // 
            this.cmbBodegas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBodegas.FormattingEnabled = true;
            this.cmbBodegas.Location = new System.Drawing.Point(307, 130);
            this.cmbBodegas.Name = "cmbBodegas";
            this.cmbBodegas.Size = new System.Drawing.Size(196, 21);
            this.cmbBodegas.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Bodega";
            // 
            // txtTicket
            // 
            this.txtTicket.Location = new System.Drawing.Point(129, 130);
            this.txtTicket.Name = "txtTicket";
            this.txtTicket.Size = new System.Drawing.Size(100, 20);
            this.txtTicket.TabIndex = 10;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(193, 161);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(92, 26);
            this.btnFiltrar.TabIndex = 5;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // cmbTipodeBoleta
            // 
            this.cmbTipodeBoleta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipodeBoleta.FormattingEnabled = true;
            this.cmbTipodeBoleta.Items.AddRange(new object[] {
            "TODAS LAS BOLETAS",
            "ENTRADA",
            "SALIDA"});
            this.cmbTipodeBoleta.Location = new System.Drawing.Point(435, 38);
            this.cmbTipodeBoleta.Name = "cmbTipodeBoleta";
            this.cmbTipodeBoleta.Size = new System.Drawing.Size(177, 21);
            this.cmbTipodeBoleta.TabIndex = 9;
            // 
            // cmbCiclos
            // 
            this.cmbCiclos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCiclos.FormattingEnabled = true;
            this.cmbCiclos.Location = new System.Drawing.Point(668, 12);
            this.cmbCiclos.Name = "cmbCiclos";
            this.cmbCiclos.Size = new System.Drawing.Size(117, 21);
            this.cmbCiclos.TabIndex = 7;
            // 
            // cmbProductos
            // 
            this.cmbProductos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductos.FormattingEnabled = true;
            this.cmbProductos.Location = new System.Drawing.Point(435, 4);
            this.cmbProductos.Name = "cmbProductos";
            this.cmbProductos.Size = new System.Drawing.Size(177, 21);
            this.cmbProductos.TabIndex = 8;
            // 
            // cmbProductores
            // 
            this.cmbProductores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductores.FormattingEnabled = true;
            this.cmbProductores.Location = new System.Drawing.Point(111, 9);
            this.cmbProductores.Name = "cmbProductores";
            this.cmbProductores.Size = new System.Drawing.Size(222, 21);
            this.cmbProductores.TabIndex = 6;
            // 
            // lblTipoBoleta
            // 
            this.lblTipoBoleta.AutoSize = true;
            this.lblTipoBoleta.Location = new System.Drawing.Point(351, 41);
            this.lblTipoBoleta.Name = "lblTipoBoleta";
            this.lblTipoBoleta.Size = new System.Drawing.Size(78, 13);
            this.lblTipoBoleta.TabIndex = 4;
            this.lblTipoBoleta.Text = "Tipo de boleta:";
            this.lblTipoBoleta.Click += new System.EventHandler(this.label5_Click);
            // 
            // lblNoBoleta
            // 
            this.lblNoBoleta.AutoSize = true;
            this.lblNoBoleta.Location = new System.Drawing.Point(59, 133);
            this.lblNoBoleta.Name = "lblNoBoleta";
            this.lblNoBoleta.Size = new System.Drawing.Size(68, 13);
            this.lblNoBoleta.TabIndex = 3;
            this.lblNoBoleta.Text = "# de Ticket :";
            // 
            // lblProducto
            // 
            this.lblProducto.AutoSize = true;
            this.lblProducto.Location = new System.Drawing.Point(376, 7);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(53, 13);
            this.lblProducto.TabIndex = 2;
            this.lblProducto.Text = "Producto:";
            // 
            // lblCiclo
            // 
            this.lblCiclo.AutoSize = true;
            this.lblCiclo.Location = new System.Drawing.Point(619, 17);
            this.lblCiclo.Name = "lblCiclo";
            this.lblCiclo.Size = new System.Drawing.Size(33, 13);
            this.lblCiclo.TabIndex = 1;
            this.lblCiclo.Text = "Ciclo:";
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
            this.dgvBoletas.Location = new System.Drawing.Point(12, 220);
            this.dgvBoletas.Name = "dgvBoletas";
            this.dgvBoletas.ReadOnly = true;
            this.dgvBoletas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBoletas.Size = new System.Drawing.Size(867, 314);
            this.dgvBoletas.TabIndex = 1;
            this.dgvBoletas.SelectionChanged += new System.EventHandler(this.dgvBoletas_SelectionChanged);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(294, 540);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(76, 31);
            this.btnAgregar.TabIndex = 2;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnModificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificar.Location = new System.Drawing.Point(398, 540);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(75, 31);
            this.btnModificar.TabIndex = 3;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.Location = new System.Drawing.Point(500, 540);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 31);
            this.btnEliminar.TabIndex = 4;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // txtCantidadBoletas
            // 
            this.txtCantidadBoletas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCantidadBoletas.Location = new System.Drawing.Point(794, 546);
            this.txtCantidadBoletas.Name = "txtCantidadBoletas";
            this.txtCantidadBoletas.ReadOnly = true;
            this.txtCantidadBoletas.Size = new System.Drawing.Size(71, 20);
            this.txtCantidadBoletas.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(709, 549);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Boletas Leidas:";
            // 
            // frmListBoletas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 573);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCantidadBoletas);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.dgvBoletas);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmListBoletas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de Boletas";
            this.Load += new System.EventHandler(this.frmListBoletas_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.proveedoresBindingSource)).EndInit();
            this.grbfiltrarPorFecha.ResumeLayout(false);
            this.grbfiltrarPorFecha.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoletas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Label lblTipoBoleta;
        private System.Windows.Forms.Label lblNoBoleta;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.Label lblCiclo;
        private System.Windows.Forms.TextBox txtTicket;
        private System.Windows.Forms.ComboBox cmbTipodeBoleta;
        private System.Windows.Forms.ComboBox cmbCiclos;
        private System.Windows.Forms.ComboBox cmbProductos;
        private System.Windows.Forms.ComboBox cmbProductores;
        private System.Windows.Forms.DataGridView dgvBoletas;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.TextBox txtCantidadBoletas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBodegas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoleta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblFechaInicio;
        private System.Windows.Forms.Label lblFEchaFIn;
        private System.Windows.Forms.DateTimePicker DtpkfechaFin;
        private System.Windows.Forms.DateTimePicker dtpkFechaInicio;
        private System.Windows.Forms.Button btnLimpiarFiltros;
        private System.Windows.Forms.GroupBox grbfiltrarPorFecha;
        private System.Windows.Forms.CheckBox chkbFiltrarxFecha;
        private System.Windows.Forms.Button btnActualizarDatos;
        private System.Windows.Forms.ComboBox cmbClientesVentas;
        private System.Windows.Forms.RadioButton rdbCliente;
        private System.Windows.Forms.RadioButton rdbProductor;
        private System.Windows.Forms.RadioButton rdbProveedorGanado;
        private System.Windows.Forms.ComboBox cmbProveedorGanado;
        private System.Windows.Forms.ComboBox cmbProveedores;
        private System.Windows.Forms.RadioButton rdbProveedores;
        private System.Windows.Forms.BindingSource proveedoresBindingSource;
    }
}