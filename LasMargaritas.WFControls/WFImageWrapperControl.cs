using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LasMargaritas.WFControls
{
    public partial class WFImageWrapperControl : Form
    {
        public IntPtr PreviewImageHandle
        {          
            get
            {
                return this.PictureBox_Preview.Handle;
            }
        }

        public string Title
        {
            get
            {
                return this.lblDescription.Text;
            }
            set
            {
                this.lblDescription.Text = value;
            }

        }

        public WFImageWrapperControl()
        {
            TopLevel = false;
            InitializeComponent();
        }
    }
}
