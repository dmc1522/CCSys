using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WeifenLuo.WinFormsUI.Docking;
using System.Drawing.Printing;
using System.Diagnostics;



namespace BasculaGaribay
{
    public partial class frmAddBoletas : DockContent
    {
        public Boolean escribeboleano;
        private int _CurrentPage = 0;
        private bool _PrintFirstPart = true;

        public int CurrentPage
        {
            get { return _CurrentPage; }
            set { _CurrentPage = value; }
        }
        //Este Ejemplo puede que funcione con otras teclas, si quieres puedes probar.

        //Declaración de constantes  
        private const int KEYEVENTF_EXTENDEDKEY = 0x1;
        private const int KEYEVENTF_KEYUP = 0x2;

        //Declaracion Api 
        [DllImport("user32")]
        private static extern void keybd_event(Keys bVk, int bScan, int dwFlags, int dwExtraInfo);

        String[] datos;
        String idtoModify;

        public frmAddBoletas()
        {
            InitializeComponent();
            this.btnModify.Enabled = false;
            this.btnAdd.Enabled = true;
            this.escribeboleano = false;
            this.txtPesadorEntrada.Text = WSConnector.Instance.NombreUsuario;
            this.txtPesadorSalida.Text = WSConnector.Instance.NombreUsuario;
        }

        public frmAddBoletas(String idtoModify)
        {
            InitializeComponent();
            this.btnModify.Enabled = true;
            this.btnAdd.Enabled = false;
            this.datos = new String[38];
            this.idtoModify = idtoModify;
            this.escribeboleano = false;
        }

        private DataTable BoletaToModify;
        
        private bool cargaDatosaModificar()
        {
            try
            {
                return WSConnector.Instance.GetaBoletaAsDataTable(this.idtoModify, out BoletaToModify);
            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(ex);
                MessageBox.Show(ex.Message, "ERROR!!");
                return false;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void CheckEnabledObteners()
        {
            double peso = 0;
            if(!double.TryParse(this.txtPesoEntrada.Text, out peso))
            {
                peso = 0;
            }
            this.btnObtener1.Enabled = peso <= 0;
            if (!double.TryParse(this.txtPesoSalida.Text, out peso))
            {
                peso = 0;
            }
            this.btnObtener2.Enabled = peso <= 0;
        }

        private void EnableControls(bool isModifying)
        {
            this.txtTicket.Enabled = !isModifying;
            this.grpAsignarA.Enabled = !isModifying;
            this.grpbProductor.Enabled = !isModifying;
            this.grpbClienteVenta.Enabled = !isModifying;
            this.cmbClientesVentas.Enabled = !isModifying;
            this.grpProveedorGanado.Enabled = !isModifying;
            
            this.btnAdd.Enabled = !isModifying;
            this.btnModify.Enabled = isModifying;
        }

        private void frmAddBoletas_Load(object sender, EventArgs e)
        {
            string sError = string.Empty;
            if(!WSConnector.Instance.validaLaSession(out sError))
            {
                MessageBox.Show("La session no se pudo validar, la boleta no será agregada", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.btnObtener1.Enabled = true;
            this.btnObtener2.Enabled = false;
#if DEBUG
            this.txtPesoEntrada.Enabled = this.txtPesoSalida.Enabled = true;
#endif
            try
            {
                this.dtpFechaEntrada.Value = DateTime.Now;
                this.dtpFechaSalida.Value = DateTime.Now;
                if (!System.Console.CapsLock)
                {
                    keybd_event(Keys.CapsLock, 0x45, KEYEVENTF_EXTENDEDKEY | 0, 0);
                    keybd_event(Keys.CapsLock, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
                }
                CalculaTotales();
                this.updateCombos();
                if(this.idtoModify!=null)
                {
                    if(this.cargaDatosaModificar())
                    {
                        this.EnableControls(true);
                        DataRow dr = this.BoletaToModify.Rows[0];
                        this.cmbCiclo.SelectedValue = (int)dr["cicloID"];
                        if(((int)dr["productorID"]) > -1)
                        {
                            this.rdbProductor.Checked = true;
                            this.cmbProductor.SelectedValue = ((int)dr["productorID"]);
                        }
                        if (dr["clienteventaID"] != null && dr["clienteventaID"].ToString().Trim().Length > 0 && ((int)dr["clienteventaID"]) > -1)
                        {
                            this.rdbClienteVenta.Checked = true;
                            this.cmbClientesVentas.SelectedValue = ((int)dr["clienteventaID"]);
                        }

                        if (dr["ganProveedorID"] != null && dr["ganProveedorID"].ToString().Trim().Length > 0 && ((int)dr["ganProveedorID"]) > -1)
                        {
                            this.rdbProveedorGanado.Checked = true;
                            this.cmbProveedorGanado.SelectedValue = ((int)dr["ganProveedorID"]);
                        }

                        if (dr["proveedorID"] != null && dr["proveedorID"].ToString().Trim().Length > 0 && ((int)dr["proveedorID"]) > -1)
                        {
                            this.rdbProveedor.Checked = true;
                            this.cmbProveedores.SelectedValue = ((int)dr["proveedorID"]);
                        }

                        this.chkLlevaFlete.Checked = ((bool)dr["llevaFlete"]);


                        this.txtHumedad.Text = ((double)dr["humedad"]).ToString("N2");
                        this.txtImpurezas.Text = ((double)dr["impurezas"]).ToString("N2");
                        this.txtPrecio.Text = (double.Parse(dr["precioapagar"].ToString())).ToString("N2");
                        this.txtplacas.Text = (dr["placas"]).ToString();
                        this.txtChofer.Text = (dr["chofer"]).ToString();
                     
                        //this.chkbPagada.Checked = datos[13]=="True"? true:false;
                        this.cmbProducto.SelectedValue = ((int)dr["productoID"]);
                        this.txtNoBoleta.Text = dr["NumeroBoleta"].ToString();
                        this.txtTicket.Text = dr["Ticket"].ToString();
                        this.Text = "Boleta ID: " + idtoModify.ToString() + " Folio : " + this.txtTicket.Text;
                        this.dtpFechaEntrada.Value = ((DateTime)dr["FechaEntrada"]);
                        this.txtPesadorEntrada.Text = dr["PesadorEntrada"].ToString();
                        this.txtPesoEntrada.Text = (int.Parse(dr["PesoDeEntrada"].ToString())).ToString();
                        this.btnObtener1.Enabled = (int.Parse(dr["PesoDeEntrada"].ToString())) > 0 ? false : true;


                        //this.txtBasculaEntrada.Text = datos[24];
                        this.dtpFechaSalida.Value = ((DateTime)dr["FechaSalida"]);
                        this.txtPesoSalida.Text = (int.Parse(dr["PesoDeSalida"].ToString())).ToString();
                        this.btnObtener2.Enabled =  (int.Parse(dr["PesoDeEntrada"].ToString())) == 0 || int.Parse(dr["PesoDeSalida"].ToString()) > 0? false: true;

                        this.txtPesadorSalida.Text = dr["PesadorSalida"].ToString();
                        //this.txtBasculaSalida.Text = datos[28];
                        this.cmbBodega.SelectedValue = ((int)dr["bodegaID"]);
                        this.chkbDescHumedad.Checked = ((bool)dr["applyHumedad"]);
                        this.chkbimpurezas.Checked = ((bool)dr["applyImpurezas"]);
                        this.chkbSecado.Checked = ((bool)dr["applySecado"]);

                        this.txtCabezasDeGanado.Text = ((int)dr["cabezasDeGanado"]).ToString();

                        this.chkGanadoACorrales.Checked = (bool)(dr["deGranjaACorrales"]);

                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(ex);
                MessageBox.Show(ex.Message, "ERROR!!");
            }
            if(!WSConnector.Instance.SessionValida)
            {
                this.btnAdd.Enabled = false;
            }
            this.CheckEnabledObteners();
        }

        private DataTable dtCiclos = new DataTable();
        private DataTable dtProductores = new DataTable();
        private DataTable dtBodegas = new DataTable();
        private DataTable dtProductos = new DataTable();
        private DataTable dtClientesVentas = new DataTable();
        private DataTable dtProvGanado = new DataTable();

        private void updateCombos()
        {
            try
            {
                DateTime starttime = DateTime.Now;
                Debug.WriteLine("loading combos de boleta :" + starttime.ToString("HH:mm:ss"));
                if (WSConnector.Instance.GetAllCiclos(out dtCiclos))
                {
                    this.cmbCiclo.DataSource = dtCiclos;
                    this.cmbCiclo.ValueMember = "cicloID";
                    this.cmbCiclo.DisplayMember = "Ciclo";

                    if(WSConnector.Instance.GetAllProductores(out dtProductores))
                    {
                        this.cmbProductor.DataSource = dtProductores;
                        this.cmbProductor.ValueMember = "productorID";
                        this.cmbProductor.DisplayMember = "Nombre";
                        this.cmbProductor.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("No se pudieron cargar los productores, esto pudiera ser un error de la red\nPulse el boton \"Actualizar lista\" para intentar recargar los productores", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if(WSConnector.Instance.GetAllBodegas(out dtBodegas))
                    {
                        this.cmbBodega.DataSource = dtBodegas;
                        this.cmbBodega.ValueMember = "bodegaID";
                        this.cmbBodega.DisplayMember = "bodega";
                        this.cmbBodega.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("No se pudieron cargar las bodegas, esto pudiera ser un error de la red, cierre y abra otra vez la ventana para agregar una boleta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if(WSConnector.Instance.GetAllProductos(out dtProductos))
                    {
                        dtProductos.DefaultView.RowFilter = "productoGrupoID <> 4 and productoid <> 88";
                        this.cmbProducto.DataSource = dtProductos;
                        this.cmbProducto.ValueMember = "productoID";
                        this.cmbProducto.DisplayMember = "Nombre";
                        this.cmbProducto.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("No se pudieron cargar los productos, esto pudiera ser un error de la red, cierre y abra otra vez la ventana para agregar una boleta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (WSConnector.Instance.GetAllClientesVentas(out dtClientesVentas))
                    {
                        this.cmbClientesVentas.DataSource = dtClientesVentas;
                        this.cmbClientesVentas.ValueMember = "clienteventaID";
                        this.cmbClientesVentas.DisplayMember = "nombre";
                        this.cmbClientesVentas.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("No se pudieron cargar los clientes de venta, esto pudiera ser un error de la red, cierre y abra otra vez la ventana para agregar una boleta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (WSConnector.Instance.GetProvDeGanado(out this.dtProvGanado))
                    {
                        this.cmbProveedorGanado.DataSource = dtProvGanado;
                        this.cmbProveedorGanado.ValueMember = "ganProveedorID";
                        this.cmbProveedorGanado.DisplayMember = "Nombre";
                        this.cmbProveedorGanado.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("No se pudieron cargar los proveedores de ganado, esto pudiera ser un error de la red, cierre y abra otra vez la ventana para agregar una boleta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    this.proveedoresBindingSource.DataSource = WSConnector.Instance.GetAllProveedores();

                }
                //MessageBox.Show("loading combos de boleta took:" + (DateTime.Now - starttime).TotalSeconds.ToString() + " segs");
                Debug.WriteLine("loading combos de boleta took:" + (DateTime.Now - starttime).TotalSeconds.ToString()+ " segs");
            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(ex);
                MessageBox.Show(ex.Message, "ERROR!!");
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddBoleta(bool isPrinting)
        {
            String mensajeError = "";
            String exc = "";
            int newID = -1;
            try
            {
                if (camposvalidos(out mensajeError))
                {
                    if (WSConnector.Instance.InsertarBoleta(arregloDeDatos(), out exc, out newID))
                    {
                        this.idtoModify = newID.ToString();
                        if(!isPrinting)
                            MessageBox.Show("Se ha agregado la boleta con  el ID " + this.idtoModify.ToString() + " satisfactoriamente", "ÉXITO");
                        this.Text = "Boleta ID  " + idtoModify + " Folio " + this.txtTicket.Text;
                        this.btnAdd.Enabled = false;
                        this.btnModify.Enabled = true;
                        this.idtoModify = newID.ToString();
                        this.btnObtener1.Enabled = true;
                        this.EnableControls(true);

                    }
                    else
                        MessageBox.Show(exc);
                }
                else
                {
                    MessageBox.Show(mensajeError, "ERROR");
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex);
                MessageBox.Show(ex.Message, "ERROR!!");

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.AddBoleta(false);
        }
        private bool camposvalidos(out String mensajeError)
        {
            Boolean valido = true;
            mensajeError = "";
            double numero;
            if(this.txtHumedad.Text=="")
            {
                valido = false;
                mensajeError = "EL CAMPO DE HUMEDAD NO DEBE DE SER VACIO \n";
            }
            else
                if (!double.TryParse(this.txtHumedad.Text,out numero))
                {
                    valido = false;
                    mensajeError += "EL CAMPO DE HUMEDAD NO ES VÁLIDO :" + this.txtHumedad.Text + "  \n";
                }

            if (this.txtImpurezas.Text == "")
            {
                valido = false;
                mensajeError += "EL CAMPO DE IMPUREZAS NO DEBE DE SER VACIO \n";
            }
            else
                if (!double.TryParse(this.txtImpurezas.Text, out numero))
                {
                    valido = false;
                    mensajeError += "EL CAMPO DE IMPUREZAS NO ES VÁLIDO :" + this.txtImpurezas.Text + "  \n";
                }
            
            if(this.txtPesoEntrada.Text=="")
            {
                valido = false;
                mensajeError += "EL CAMPO DE PESO DE ENTRADA NO DEBE DE SER VACIO \n";
            }
            else
                if (!double.TryParse(this.txtPesoEntrada.Text, out numero))
                {
                    valido = false;
                    mensajeError += "EL CAMPO DE PESO DE ENTRADA NO ES VÁLIDO :"+this.txtPesoEntrada.Text+"  \n";
                }
            
            
            if (this.txtPesoSalida.Text == "")
            {

                valido = false;
                mensajeError += "EL CAMPO DE PESO DE SALIDA NO DEBE DE SER VACIO \n";
            }
            else
                if (!double.TryParse(this.txtPesoSalida.Text, out numero))
                {
                    valido = false;
                    mensajeError += "EL CAMPO DE PESO DE SALIDA NO ES VÁLIDO :" + this.txtPesoSalida.Text + "  \n";
                }
           

            if (this.txtPrecio.Text == "")
            {
                valido = false;
                mensajeError += "EL CAMPO DE PRECIO NO DEBE DE SER VACIO \n";
            }
            else
                if (!double.TryParse(this.txtPrecio.Text, out numero))
                {
                    valido = false;
                    mensajeError += "EL CAMPO DEL PRECIO NO ES VÁLIDO :" + this.txtPrecio.Text + "  \n";
                }
            return valido;
        }
           
        private String[] arregloDeDatos()
        {
           String[] datos = new String[42];
           try
           {
               //cicloID
               datos[0] = this.cmbCiclo.SelectedValue.ToString();
               //idsession, no es el id user, me falta sacarlo en el webservice
               datos[1] = WSConnector.Instance.Idsession.ToString();
               //productor id
               datos[2] = "-1";
               datos[35] = "-1";
               datos[41] = datos[38] = "-1";
               if(this.rdbProductor.Checked)
               {
                   datos[2] = this.cmbProductor.SelectedValue.ToString();
               }
               if(this.rdbClienteVenta.Checked)
               {
                   datos[35] = this.cmbClientesVentas.SelectedValue.ToString();
               }
               if (this.rdbProveedorGanado.Checked)
               {
                   datos[38] = this.cmbProveedorGanado.SelectedValue.ToString();
               }
               if (this.rdbProveedor.Checked)
               {
                   datos[41] = this.cmbProveedores.SelectedValue.ToString();
               }
               
               //humedad
               datos[3] = this.txtHumedad.Text;
               //
               float PesoNetoEntrada=0;
               float PesoNetoSalida=0;
               //sacar si es de entrada o salida
               if (double.Parse(this.txtPesoEntrada.Text) - double.Parse(this.txtPesoSalida.Text) > 0)
               {
                   PesoNetoEntrada = float.Parse(this.txtPesoEntrada.Text) - float.Parse(this.txtPesoSalida.Text);
               }
               else
               {
                   PesoNetoSalida = float.Parse(this.txtPesoSalida.Text)- float.Parse(this.txtPesoEntrada.Text);
               }


               float humedad=0;             
               float.TryParse(this.txtHumedad.Text, out humedad);
           
               //descuento por humedad
               datos[4] = Utils.getDesctoHumedad(humedad,PesoNetoEntrada).ToString();
               //impurezas
               datos[5] = this.txtImpurezas.Text;
               //totaldescuentos
               //double totaldescuentos = 0;
               datos[6] = ((chkbDescHumedad.Checked ? Utils.getDesctoHumedad(humedad, PesoNetoEntrada) : 0) - (chkbimpurezas.Checked ? Utils.getDesctoImpurezas(float.Parse(this.txtImpurezas.Text), PesoNetoEntrada) : 0) - (chkbSecado.Checked ? Utils.getDesctoSecado(humedad, PesoNetoEntrada) : 0)).ToString();
               //pesonetoapagar
               //float pesonetoapagar=0;
               //peso neto  apagar
               datos[7] = (PesoNetoEntrada - (chkbDescHumedad.Checked ? Utils.getDesctoHumedad(humedad, PesoNetoEntrada) : 0) + (chkbimpurezas.Checked ? Utils.getDesctoImpurezas(float.Parse(this.txtImpurezas.Text), PesoNetoEntrada) : 0)).ToString();
               //precioapagar
               datos[8] = this.txtPrecio.Text.ToUpper();
               //importe
               decimal importe = 0;
               importe = (decimal)(PesoNetoEntrada * float.Parse(this.txtPrecio.Text));
               datos[9] = importe.ToString();
               //placas
               datos[10] = this.txtplacas.Text.ToUpper();
               //chofer
               datos[11] = this.txtChofer.Text.ToUpper();
               //si esta pagada o no
               //if (chkbPagada.Checked)
               //    datos[12] = "TRUE";
               //else 
               datos[12] = "FALSE";
               //productoID
               datos[13] = this.cmbProducto.SelectedValue.ToString();
               //numero de boleta
               datos[14] = this.txtNoBoleta.Text;
               //ticket
               datos[15] = this.txtTicket.Text;
               
               //codigodesabequechingados
               datos[16] = "";
               //productor
               datos[17] = this.cmbProductor.Text;
               //fecha de entrada
               datos[18] = this.dtpFechaEntrada.Value.ToString();
               //pesador de entrada
               datos[19] = this.txtPesadorEntrada.Text.ToUpper();
               //peso de entrada
               datos[20] = this.txtPesoEntrada.Text;
               //bascula de entrada
               datos[21] = "MATRIZ GRANJA"; //this.txtBasculaEntrada.Text.ToUpper();
               //fecha de salida
               datos[22] = this.dtpFechaSalida.Value.ToString();
               //peso de salida
               datos[23] = this.txtPesoSalida.Text;
               //pesador de salida
               datos[24] = this.txtPesadorSalida.Text.ToUpper();
               //bascula de salida
               datos[25] = "MATRIZ GRANJA";//this.txtBasculaSalida.Text.ToUpper();
               //peso neto de entrada
               datos[26] = PesoNetoEntrada.ToString();
               //peso neto de salida
               datos[27] = PesoNetoSalida.ToString();
               //descuento de impurezas
               datos[28] = Utils.getDesctoImpurezas(float.Parse(this.txtImpurezas.Text), PesoNetoEntrada).ToString();
               //descuento de secado
               datos[29] = Utils.getDesctoSecado(humedad,PesoNetoEntrada).ToString();
               //float fPrecio = 0;
               //total a pagar
               datos[30] = (PesoNetoEntrada * float.Parse(txtPrecio.Text) - (chkbSecado.Checked ? Utils.getDesctoSecado(humedad, PesoNetoEntrada) : 0)).ToString(); 
               //bodega id
               datos[31] = this.cmbBodega.SelectedValue.ToString();
               //apply descuentos
               if (chkbDescHumedad.Checked)
                   datos[32] = "TRUE";
               else datos[32] = "FALSE";

               if (chkbimpurezas.Checked)
                   datos[33] = "TRUE";
               else datos[33] = "FALSE";

               if (chkbSecado.Checked)
                   datos[34] = "TRUE";
               else datos[34] = "FALSE";




               int cabezas = 0;
               if (!int.TryParse(this.txtCabezasDeGanado.Text, out cabezas))
               {
                   cabezas = 0;
               }
               datos[37] = cabezas.ToString();
               datos[39] = this.chkLlevaFlete.Checked.ToString();
               datos[40] = this.chkGanadoACorrales.Checked.ToString();
           }
           catch (Exception ex)
           {
               Logger.Instance.LogException(ex);
               MessageBox.Show(ex.Message, "ERROR!");
               return null;

           }
           return datos;

       }

        private void UpdateBoleta(bool isPrinting)
        {
            String mensajeError = "";
            String exc = "";
            try
            {

                if (camposvalidos(out mensajeError))
                {
                    if (WSConnector.Instance.ModificarBoleta(arregloDeDatos(), idtoModify, out exc))
                    {
                        if (!isPrinting)
                        {
                            MessageBox.Show("Se ha Modificado la boleta con el ID : " + idtoModify + " satisfactoriamente", "ÉXITO");
                        }
                        this.Text = "Boleta ID " + idtoModify + " Folio " + this.txtTicket.Text;
                        this.btnAdd.Enabled = false;
                        this.btnObtener1.Enabled = (int.Parse(this.txtPesoEntrada.Text)) > 0 ? false : true;
                        this.btnObtener2.Enabled = (int.Parse(this.txtPesoEntrada.Text)) == 0 || int.Parse(this.txtPesoSalida.Text) > 0 ? false : true;
                    }
                    else
                        MessageBox.Show(exc);
                }
                else
                {
                    MessageBox.Show(mensajeError, "ERROR");
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex);
                MessageBox.Show(ex.Message, "ERROR!!");
            }
        }
        private void btnModify_Click(object sender, EventArgs e)
        {
            this.UpdateBoleta(false);
        }

        private void txtPesoEntrada_TextChanged(object sender, EventArgs e)
        {
            CalculaTotales();
        }

        private void txtPesoSalida_TextChanged(object sender, EventArgs e)
        {
            CalculaTotales();
            
        }



        private void lblsearchPro_Click(object sender, EventArgs e)
        {
            escribeboleano = true;
        }


        private void txtBuscarPro_Click(object sender, EventArgs e)
        {
            this.txtBuscarPro.Text = "";
        }

        private void txtBuscarPro_TextChanged(object sender, EventArgs e)
        {
            String s = "";
            if(this.txtBuscarPro.Text!="")
            {
                for(int i=0;i<this.cmbProductor.Items.Count;i++)
                {
                    DataRowView elemento= (DataRowView)this.cmbProductor.Items[i];
                    Object[] ds = elemento.Row.ItemArray;
                    s = ds[1].ToString();
                    if(s.StartsWith(this.txtBuscarPro.Text))
                    {
                        this.cmbProductor.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddProductor fr = new frmAddProductor();
            Principal.OpenFormInDock(fr);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Cache.Instance.InvalidChaceEntry(CacheTables.PRODUCTORES);
                Cache.Instance.InvalidChaceEntry(CacheTables.PRODUCTORESFORCMB);
                if (WSConnector.Instance.GetAllProductores(out dtProductores))
                {
                    this.cmbProductor.DataSource = dtProductores;
                    this.cmbProductor.ValueMember = "productorID";
                    this.cmbProductor.DisplayMember = "Nombre";
                    this.cmbProductor.SelectedIndex = 0;
                    this.txtBuscarPro.Text = "Escriba aquí para Buscar ....";
                }
                else
                {
                    MessageBox.Show("LOS PRODUCTORES NO PUDIERON SER CONSULTADOS, POR FAVOR VUELVA A ACTUALIZAR LA LISTA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(ex);
                MessageBox.Show(ex.Message, "ERROR!!");
            }
        }


        private void btnObtener1_Click(object sender, EventArgs e)
        {
            try
            {
                frmLecturaBascula frm = new frmLecturaBascula(1);
                frm.ShowDialog();
                this.txtPesoEntrada.Text = frm.Peso.ToString();
                this.dtpFechaEntrada.Value = DateTime.Now;
                this.txtPesadorEntrada.Text = WSConnector.Instance.NombreUsuario;
                this.CheckEnabledObteners();
            }
            catch(Exception ex) 
            {
                Logger.Instance.LogException(ex);
            }
        }

        private void vcc_Click(object sender, EventArgs e)
        {
            try
            {
                frmLecturaBascula frm = new frmLecturaBascula(2);
                frm.ShowDialog();
                this.txtPesoSalida.Text = frm.Peso.ToString();
                this.dtpFechaSalida.Value = DateTime.Now;
                this.txtPesadorSalida.Text = WSConnector.Instance.NombreUsuario;
            }
            catch(Exception ex) 
            {
                Logger.Instance.LogException(ex);
            }
        }

        private void rdbClienteVenta_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateProdClienteProdGanado();
        }

        private void CalculaTotales()
        {   
            double pesoEntrada = 0, pesoSalida = 0, pesoNeto = 0, humedad = 0, dctoHumedad = 0, impurezas = 0, dctoImpurezas = 0, dctoSecado = 0, kgTotales = 0, precio = 0, totalAPagar=0,PesoAPagar=0;
            double.TryParse(this.txtPesoEntrada.Text, out pesoEntrada);
            double.TryParse(this.txtPesoSalida.Text, out pesoSalida);
            kgTotales = Math.Abs(pesoEntrada - pesoSalida);
            pesoNeto = kgTotales;
            this.txtPesoNeto.Text=string.Format("{0:n2}", pesoNeto);

            double.TryParse(this.txtHumedad.Text, out humedad);
            if(this.chkbDescHumedad.Checked)
            {
                dctoHumedad = Utils.getDesctoHumedad(humedad, pesoNeto);
            }
              this.txtDescuentoXHumedad.Text=dctoHumedad.ToString();
              
            double.TryParse(this.txtImpurezas.Text, out impurezas);
            if (this.chkbimpurezas.Checked)
            {
                 dctoImpurezas = Utils.getDesctoImpurezas(impurezas, pesoNeto);
            }
            this.txtDescuentoXImpuresaz.Text = string.Format("{0:n2}", dctoImpurezas); 
            PesoAPagar=pesoNeto-dctoHumedad-dctoImpurezas;
            this.txtPesoTotalAPagar.Text = string.Format("{0:n2}", PesoAPagar);
            double.TryParse(this.txtPrecio.Text, out precio);
            this.txtSubtotal.Text = string.Format("{0:C2}",PesoAPagar * precio);
            if (this.chkbSecado.Checked)
            {
                dctoSecado = Utils.getDesctoSecado(humedad,pesoNeto);
            }
            this.txtDescuentoSecado.Text = string.Format("{0:C2}", dctoSecado);

            totalAPagar = (PesoAPagar * precio) - dctoSecado;
            this.txtTotalApagar.Text = string.Format("{0:C2}", totalAPagar);
            
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void txtHumedad_TextChanged(object sender, EventArgs e)
        {
            CalculaTotales();
        }

        private void txtImpurezas_TextChanged(object sender, EventArgs e)
        {
            CalculaTotales();
        }

        private void chkbDescHumedad_CheckedChanged(object sender, EventArgs e)
        {
            CalculaTotales();
        }

        private void chkbimpurezas_CheckedChanged(object sender, EventArgs e)
        {
            CalculaTotales();
        }

        private void chkbSecado_CheckedChanged(object sender, EventArgs e)
        {
            CalculaTotales();
        }

        private void txtDescuentoSecado_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            CalculaTotales();
        }

        private void rdbProductor_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateProdClienteProdGanado();
        }

        private void UpdateProdClienteProdGanado()
        {
            this.grpbProductor.Enabled = this.rdbProductor.Checked;
            this.grpbClienteVenta.Enabled = this.rdbClienteVenta.Checked;
            this.grpProveedorGanado.Enabled = this.rdbProveedorGanado.Checked;
            this.grpProveedor.Enabled = this.rdbProveedor.Checked;
            if (this.rdbProductor.Checked || 
                this.rdbClienteVenta.Checked ||
                this.rdbProveedor.Checked)
            {
                this.tabControl1.SelectedTab = tabMaiz;
                dtProductos.DefaultView.RowFilter = this.rdbProveedor.Checked ? "productoGrupoID <> -1" : "productoGrupoID <> 4";
                this.cmbProducto.DataSource = dtProductos;
            }
            else
            {
                this.tabControl1.SelectedTab = tabGanado;
                dtProductos.DefaultView.RowFilter = "productoGrupoID = 4";
                this.cmbProducto.DataSource = dtProductos;
            }

        }

        private void btnPrintPrimeraparte_Click(object sender, EventArgs e)
        {
            this.btnObtener1.Enabled = (int.Parse(this.txtPesoEntrada.Text)) > 0 ? false : true;
            if (this.btnModify.Enabled)
            {
                this.UpdateBoleta(true);
            }
            if(this.btnAdd.Enabled)
            {
                this.AddBoleta(true);
            }
            this._PrintFirstPart = true;
            _CurrentPage = 0;
            PrintDocument printDocument = new PrintDocument();
            printDocument.DocumentName = "Boleta: " + this.lblTicket.Text;
            PrintDialog pd = new PrintDialog();
            pd.Document = printDocument;
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            PaperSize ps = new PaperSize();
            ps.Width = 425;
            ps.Height = 551;
            printDocument.DefaultPageSettings.PaperSize = ps;
            printDocument.Print();

            this.CheckEnabledObteners();
            
        }

        void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.HasMorePages = ++CurrentPage < e.PageSettings.PrinterSettings.Copies;
            Graphics g = e.Graphics;
            Brush brush = Brushes.Black;

            g.PageUnit = GraphicsUnit.Millimeter;

            //Point pOrigen = new Point(1, 1);



            float fAjusteX =  0.0f;
            float fAjusteY =  0.0f;
            double fFontSize = 8;


            try
            {
                System.Drawing.Font fnt = new System.Drawing.Font("Arial", (float)fFontSize, GraphicsUnit.Point);
                System.Drawing.Font fntDetalle = new System.Drawing.Font("Arial", (float)fFontSize, GraphicsUnit.Point);


                String sText = string.Empty;
                float px, py;
                //fecha dia 
                px = 0.0f + fAjusteX;
                py = 10.0f + fAjusteY;

                SizeF fontSize = g.MeasureString("TEST", fntDetalle);
                if (this._PrintFirstPart)
                {
                    sText = "BOLETA ID: " + this.idtoModify + " Ticket: " + this.txtTicket.Text ;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    fontSize = g.MeasureString(sText.ToUpper(), fntDetalle);

                    if(this.rdbProductor.Checked)
                    {
                        sText = this.cmbProductor.Text;
                    }
                    if (this.rdbClienteVenta.Checked)
                    {
                        sText = this.cmbClientesVentas.Text;
                    }
                    if (this.rdbProveedorGanado.Checked)
                    {
                        sText = this.cmbProveedorGanado.Text;
                    }
                    if (this.rdbProveedor.Checked)
                    {
                        sText = this.cmbProveedores.Text;
                    }
                    
                    py += fontSize.Height;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);


                    sText = "PLACAS: " + this.txtplacas.Text;
                    py += fontSize.Height;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    sText = "CHOFER: ";
                    py += fontSize.Height;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    sText = this.txtChofer.Text;
                    py += fontSize.Height;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    sText = "PRODUCTO: ";
                    py += fontSize.Height;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    sText = this.cmbProducto.Text;
                    py += fontSize.Height;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    sText = "PRIMERA PESADA";
                    py += fontSize.Height * 2;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    sText = "FECHA: " + this.dtpFechaEntrada.Value.ToString("dd/MM/yyyy HH:mm:ss");
                    py += fontSize.Height;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    sText = "PESO: " + this.txtPesoEntrada.Text + " Kg";
                    py += fontSize.Height;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                }
                else
                {
                    py += fontSize.Height * 13;
                    sText = "SEGUNDA PESADA   Ticket: " + this.txtTicket.Text;
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    py += fontSize.Height;
                    sText = "FECHA: " + this.dtpFechaSalida.Value.ToString("dd/MM/yyyy HH:mm:ss");
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    py += fontSize.Height;
                    sText = "PESO: " + this.txtPesoSalida.Text + " Kg";
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);

                    py += fontSize.Height;
                    sText = "PESO NETO: " + this.txtPesoNeto.Text + " Kg";
                    g.DrawString(sText.ToUpper(), fnt, brush, px, py);

                    py += fontSize.Height;
                    if(!this.rdbProveedorGanado.Checked)
                    {
                        double humedad = 0;
                        if (double.TryParse(this.txtHumedad.Text, out humedad) && humedad > 0)
                        {
                            sText = "HUMEDAD: " + humedad.ToString("N2");
                        }
                        else
                        {
                            sText = "HUMEDAD: ________";
                        }
                        g.DrawString(sText.ToUpper(), fnt, brush, px, py);


                        py += fontSize.Height;
                        double impurezas = 0;
                        if (double.TryParse(this.txtImpurezas.Text, out impurezas) && impurezas > 0)
                        {
                            sText = "IMPUREZAS: " + impurezas.ToString("N2");
                        }
                        else
                        {
                            sText = "IMPUREZAS: ________";
                        }
                        g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    }
                    else
                    {
                        sText = "CABEZAS: " + this.txtCabezasDeGanado.Text;
                        g.DrawString(sText.ToUpper(), fnt, brush, px, py);
                    }
                    
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(ex);
            }
        }

        private void btnPrintParte2_Click(object sender, EventArgs e)
        {
            if (this.btnModify.Enabled)
            {
                this.UpdateBoleta(true);
            }
            if (this.btnAdd.Enabled)
            {
                this.AddBoleta(true);
            }
            this._PrintFirstPart = false;
            _CurrentPage = 0;
            PrintDocument printDocument = new PrintDocument();
            printDocument.DocumentName = "Boleta: " + this.lblTicket.Text;
            PaperSize ps = new PaperSize();
            ps.Width = 425;
            ps.Height = 551;
            printDocument.DefaultPageSettings.PaperSize = ps;
            PrintDialog pd = new PrintDialog();
            pd.Document = printDocument;
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            printDocument.Print();

            this.EnableControls(true);
        }

        private void btnNuevaBoleta_Click(object sender, EventArgs e)
        {

            this.dtpFechaEntrada.Value = DateTime.Now;
            this.dtpFechaSalida.Value = DateTime.Now;
            CalculaTotales();
            this.EnableControls(false);
            this.rdbProductor.Checked = true;
            this.cmbProductor.SelectedIndex = 0;
            this.rdbClienteVenta.Checked = this.rdbProveedorGanado.Checked = false;
            this.UpdateProdClienteProdGanado();
            this.txtHumedad.Text = this.txtImpurezas.Text = this.txtPesoEntrada.Text = this.txtPesoSalida.Text = "0";
            this.txtTicket.Text = string.Empty;
            this.chkbDescHumedad.Checked = this.chkbimpurezas.Checked = this.chkbSecado.Checked = true;
            this.chkLlevaFlete.Checked = false;
            this.txtChofer.Text = this.txtplacas.Text = string.Empty;
            this.txtPrecio.Text = this.txtCabezasDeGanado.Text = "0";

            this.idtoModify = string.Empty;

            this.btnAdd.Enabled = true;
            this.btnModify.Enabled = false;
            this.Text = "Nueva Boleta";
            this.btnObtener1.Enabled = true;
            this.btnObtener2.Enabled = false;
            string sError = string.Empty;
            if (!WSConnector.Instance.validaLaSession(out sError))
            {
            }

        }

        private void btnEnablePesos_Click(object sender, EventArgs e)
        {
            string sStr;
            frmLogin dlg = new frmLogin();
            DialogResult dr =  dlg.ShowDialog(this);
            if (dr == DialogResult.OK && WSConnector.Instance.validaLaSession(out sStr))
            {
                this.txtPesoEntrada.Enabled = this.txtPesoSalida.Enabled = true;
                this.cmbProducto.Enabled = true;
                this.txtTicket.Enabled = true;
                this.grpAsignarA.Enabled = true;
                this.grpbProductor.Enabled = true;
                this.grpbClienteVenta.Enabled = true;
                this.cmbClientesVentas.Enabled = true;
                this.grpProveedorGanado.Enabled = true;
            }
        }


    }
}
