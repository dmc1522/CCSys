namespace BasculaGaribay
{
    partial class frmBoletasPendientesToolBox
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
            this.dgvBoletas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoletas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBoletas
            // 
            this.dgvBoletas.AllowUserToAddRows = false;
            this.dgvBoletas.AllowUserToDeleteRows = false;
            this.dgvBoletas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvBoletas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBoletas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBoletas.Location = new System.Drawing.Point(0, 0);
            this.dgvBoletas.Name = "dgvBoletas";
            this.dgvBoletas.ReadOnly = true;
            this.dgvBoletas.RowHeadersVisible = false;
            this.dgvBoletas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBoletas.Size = new System.Drawing.Size(117, 501);
            this.dgvBoletas.TabIndex = 9;
            this.dgvBoletas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBoletas_CellDoubleClick);
            this.dgvBoletas.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBoletas_CellContentDoubleClick);
            // 
            // frmBoletasPendientesToolBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(117, 501);
            this.Controls.Add(this.dgvBoletas);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmBoletasPendientesToolBox";
            this.Text = "Boletas Pendientes";
            this.Load += new System.EventHandler(this.frmBoletasPendientesToolBox_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBoletasPendientesToolBox_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoletas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBoletas;
    }
}