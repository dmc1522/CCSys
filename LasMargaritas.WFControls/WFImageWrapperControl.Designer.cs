namespace LasMargaritas.WFControls
{
    partial class WFImageWrapperControl
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
            this.PictureBox_Preview = new System.Windows.Forms.PictureBox();
            this.lblDescription = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Preview)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox_Preview
            // 
            this.PictureBox_Preview.BackColor = System.Drawing.Color.White;
            this.PictureBox_Preview.Location = new System.Drawing.Point(12, 12);
            this.PictureBox_Preview.Name = "PictureBox_Preview";
            this.PictureBox_Preview.Size = new System.Drawing.Size(335, 206);
            this.PictureBox_Preview.TabIndex = 1;
            this.PictureBox_Preview.TabStop = false;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(123, 8);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(0, 25);
            this.lblDescription.TabIndex = 2;
            // 
            // WFImageWrapperControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 237);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.PictureBox_Preview);
            this.Name = "WFImageWrapperControl";
            this.Text = "WFImageWrapperControl";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Preview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox PictureBox_Preview;
        private System.Windows.Forms.Label lblDescription;
    }
}