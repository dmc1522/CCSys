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
    public partial class frmProveedor : DockContent
    {
        private Proveedores _myProveedor = new Proveedores();
        public Proveedores MyProveedor
        {
            get { return _myProveedor; }
            set { _myProveedor = value; }
        }
        public frmProveedor()
        {
            InitializeComponent();
        }

        private void frmProveedor_Load(object sender, EventArgs e)
        {
            this.LoadEstados();
            this.proveedoresBindingSource.DataSource = _myProveedor;
            if (this._myProveedor.ProveedorID != null && this._myProveedor.ProveedorID > 0)
            {
                this.Text = "Proveedor: " + this._myProveedor.ProveedorID.ToString(); 
            }

            this.EnableButtons();
        }

        private void LoadEstados()
        {
            Cursor.Current = Cursors.WaitCursor;
            this.estadosBindingSource.DataSource = WSConnector.Instance.GetAllEstados();
            Cursor.Current = Cursors.Default;
        }

        private void DataBinding()
        {
            this._myProveedor.Fechaalta = this._myProveedor.StoreTS = this._myProveedor.UpdateTS = DateTime.Now;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            this.DataBinding();
            Proveedores p = WSConnector.Instance.AddUpdateProveedor(_myProveedor);
            
            if (p.ProveedorID != null && p.ProveedorID > 0)
            {
                MessageBox.Show("Proveedor Agregado Exitosamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _myProveedor = p;
                this.proveedoresBindingSource.DataSource = _myProveedor;
                this.Text = "Proveedor: " + this._myProveedor.ProveedorID.ToString();
            }
            else
            {
                MessageBox.Show("Proveedor no pudo ser agregado.", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.EnableButtons();
        }
        
        private void EnableButtons()
        {
            this.btnAgregar.Enabled = this._myProveedor.ProveedorID == null ||
                                        this._myProveedor.ProveedorID <= 0;
            this.btnActualizar.Enabled = this._myProveedor.ProveedorID != null ||
                                        this._myProveedor.ProveedorID >= 0;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            this.DataBinding();
            Proveedores p = WSConnector.Instance.AddUpdateProveedor(_myProveedor);
            MessageBox.Show("Proveedor modificado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _myProveedor = p;
            this.proveedoresBindingSource.DataSource = _myProveedor;
            this.EnableButtons();
        }
    }
}
