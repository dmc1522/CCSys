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
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Preview)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox_Preview
            // 
            this.PictureBox_Preview.BackColor = System.Drawing.Color.White;
            this.PictureBox_Preview.Location = new System.Drawing.Point(0, 0);
            this.PictureBox_Preview.Margin = new System.Windows.Forms.Padding(0);
            this.PictureBox_Preview.Name = "PictureBox_Preview";
            this.PictureBox_Preview.Size = new System.Drawing.Size(335, 199);
            this.PictureBox_Preview.TabIndex = 1;
            this.PictureBox_Preview.TabStop = false;
            // 
            // WFImageWrapperControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 203);
            this.Controls.Add(this.PictureBox_Preview);
            this.Name = "WFImageWrapperControl";
            this.Text = "WFImageWrapperControl";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Preview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.PictureBox PictureBox_Preview;
    }
}