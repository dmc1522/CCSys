using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace BasculaGaribay
{
    public partial class frmBoletasListFromDS : DockContent
    {
        public frmBoletasListFromDS()
        {
            InitializeComponent();
        }

        private void boletasBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.boletasBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.garibayDataSet);

        }

        private void frmBoletasListFromDS_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'garibayDataSet.Boletas' table. You can move, or remove it, as needed.
            this.boletasTableAdapter.Fill(this.garibayDataSet.dbo_Boletas);

        }

        private void SyncDatabasetoolStripButton_Click(object sender, EventArgs e)
        {
            WSConnector.Instance.SyncDatabase();
            boletasTableAdapter.Fill(this.garibayDataSet.dbo_Boletas);
        }
    }
}
