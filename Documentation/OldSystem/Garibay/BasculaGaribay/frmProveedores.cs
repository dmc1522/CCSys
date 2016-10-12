using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace BasculaGaribay
{
    public partial class frmProveedores : DockContent
    {
        public frmProveedores()
        {
            InitializeComponent();
        }

        private void btnUpdateList_Click(object sender, EventArgs e)
        {
            this.UpdateLista();
        }

        private void UpdateLista()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.proveedoresDataGridView.DataSource = WSConnector.Instance.GetAllProveedores();
                this.proveedoresDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {
            this.UpdateLista();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de eliminar el proveedor?, se eliminara cualquier informacion relacionada con este.", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Proveedores p = null;
                if (proveedoresDataGridView.SelectedRows.Count == 1 &&
                    proveedoresDataGridView.SelectedRows[0].DataBoundItem.GetType() == typeof(Proveedores))
                {
                    p = (Proveedores)proveedoresDataGridView.SelectedRows[0].DataBoundItem;
                    if (WSConnector.Instance.DeleteProveedor((int)p.ProveedorID))
                    {
                        MessageBox.Show("El Proveedor fue eliminado exitosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    MessageBox.Show("Debe de seleccionar un solo provedor", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void proveedoresDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.UpdateProveedor(e.RowIndex);
        }

        private void UpdateProveedor(int updateIndex)
        {
            if (updateIndex >= 0 &&
                this.proveedoresDataGridView.Rows[updateIndex].DataBoundItem.GetType() == typeof(Proveedores))
            {
                frmProveedor dlg = new frmProveedor();
                dlg.MyProveedor = (Proveedores)this.proveedoresDataGridView.Rows[updateIndex].DataBoundItem;
                Principal.OpenFormInDock(dlg);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(this.proveedoresDataGridView.SelectedRows.Count == 1)
            {
                this.UpdateProveedor(this.proveedoresDataGridView.SelectedRows[0].Index);
            }
        }
    }
}
