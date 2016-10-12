using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
//using NPOI.HSSF.UserModel;
using System.IO;
using System.Data.SqlClient;

namespace Garibay
{
    public partial class WebForm12 : Garibay.BasePage
    {
        private String sSessionBoletasFileContent=  "BoletasFileContent";
        private String sSessionBoletasFileName ="BoletasFileName";
        private String sSessionBoletasFileSize = "BoletasFileSize";
        private String sSessionDTBoletas = "BoletasDataTable";
        private String sSessionDTBoletasToAdd = "BoletasToAdd";
        private String sSessionDVBoletasAdd = "BoletasAdd";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                dsBoletas.dtBoletasDataTable teibol = new dsBoletas.dtBoletasDataTable();
                this.Session[this.sSessionDTBoletasToAdd] =  teibol;
            }
            else
            {
                dsBoletas.dtBoletasDataTable teibol = (dsBoletas.dtBoletasDataTable)this.Session[this.sSessionDTBoletasToAdd];
                this.gvBoletasAddiNDB.DataSource = teibol;
                if (this.Session[this.sSessionDTBoletas] != null)
                {
                    this.gvBoletasAdd.DataSource = (DataView)this.Session[this.sSessionDVBoletasAdd];
                    //this.gvBoletasAdd.DataBind();
                    this.gvBoletasAddiNDB.DataBind();
                    this.gvBoletasYaIngresadas.DataBind();
                    this.ddlProductos.DataBind();
                }

            }
            this.compruebasecurityLevel();
            
        }

        protected void compruebasecurityLevel()
        {
            if (this.SecurityLevel == 4)
            {
                this.Response.Redirect("~/frmUnauthorizedAccess.aspx");
            }

        }
        protected void btnCargaArchivo_Click(object sender, EventArgs e)
        {
            if (this.FUBoletas.HasFile)
            {
                this.lblFileName.Text = this.FUBoletas.FileName;
                this.lblFileSize.Text = string.Format("{0:f} KB",this.FUBoletas.FileContent.Length / 1024);
                this.Session[this.sSessionBoletasFileContent] = this.FUBoletas.FileBytes;
                this.Session[this.sSessionBoletasFileName] = this.FUBoletas.FileName;
                this.Session[this.sSessionBoletasFileSize] = this.FUBoletas.FileContent.Length;
                this.UpdatePanel1.Visible = true;    
            }
        }

        protected void updateDDLClientesProv()
        {
            DataTable dtBoletas = (DataTable)this.Session[this.sSessionDTBoletas];
            DataTable dtClientesProv = dtBoletas.DefaultView.ToTable(true, new string[2] { "NombreProductor", "TipoClienteProd" });

            DataView dv = new DataView(dtClientesProv);
            dv.Sort = "NombreProductor";
            dv.RowFilter = "TipoClienteProd = '" + this.ddlClientesProveedores.SelectedItem.Value + "'";
            this.ddlFilterClienteProveedor.DataSource = dv;
            this.ddlFilterClienteProveedor.DataValueField = "NombreProductor";
            this.ddlFilterClienteProveedor.DataTextField = "NombreProductor";
            this.ddlFilterClienteProveedor.DataBind();
        }

        protected void updateDDLCodigo()
        {
            DataTable dtBoletas = (DataTable)this.Session[this.sSessionDTBoletas];
            DataTable dtClientesProv = dtBoletas.DefaultView.ToTable(true, new string[2] { "codigoClienteProvArchivo", "TipoClienteProd" });

            DataView dv = new DataView(dtClientesProv);
            dv.Sort = "codigoClienteProvArchivo";
            dv.RowFilter = "TipoClienteProd = '" + this.ddlClientesProveedores.SelectedItem.Value + "'";
            this.ddlFilterCodigo.DataSource = dv;
            this.ddlFilterCodigo.DataValueField = "codigoClienteProvArchivo";
            this.ddlFilterCodigo.DataTextField = "codigoClienteProvArchivo";
            this.ddlFilterCodigo.DataBind();
        }
        protected void updateGridView()
        {
            try
            {
	            DataTable dtBoletas = (DataTable)this.Session[this.sSessionDTBoletas];
                DataView dv = new DataView(dtBoletas);
                dv.Sort = "NombreProductor";
                dv.RowFilter = "productoID = " + this.ddlProductos.SelectedValue;
                dv.RowFilter += " AND TipoClienteProd = '" + this.ddlClientesProveedores.SelectedItem.Value + "'";
                if (this.chkFilterClienteProv.Checked)
                {
                    dv.RowFilter += " AND NombreProductor = '" + this.ddlFilterClienteProveedor.SelectedItem.Value + "'";
                }
                if (this.chkFilterCodigoClienteProv.Checked)
                {
                    dv.RowFilter += " AND codigoClienteProvArchivo ='"+ this.ddlFilterCodigo.SelectedItem.Value +"'";
                }
                if (this.chkFilterEntradaSalida.Checked)
                {
                    if (this.ddlFilterEntradaSalida.SelectedItem.Value == "Entrada")
                    {
                        dv.RowFilter += " AND pesonetoentrada > 0";
                    }
                    else
                    {
                        dv.RowFilter += " AND pesonetosalida > 0";
                    }
                }
                this.gvBoletasAdd.DataSource = dv;
                this.gvBoletasAdd.DataBind();

                String sIDSinAgregar = "";
                String sIDYaAgregadas = "";

                if (this.gvBoletasAdd.Rows.Count > 0)
                {
                    sIDSinAgregar = "'" + this.gvBoletasAdd.Rows[0].Cells[1].Text + "'";
                    for (int i = 1; i < this.gvBoletasAdd.Rows.Count; i++)
                    {
                        sIDSinAgregar += ",";
                        sIDSinAgregar += "'"+this.gvBoletasAdd.Rows[i].Cells[1].Text+"'";
                    }
                }

                SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                conn.Open();
                comm.CommandText = "SELECT NumeroBoleta FROM Boletas WHERE (NumeroBoleta IN (" + sIDSinAgregar + "))";
                SqlDataReader SqlReader = comm.ExecuteReader();
                this.gvBoletasYaIngresadas.DataSource = null;
                if (SqlReader.HasRows && SqlReader.Read())
                {
                    sIDYaAgregadas = "'"+SqlReader[0].ToString()+"'";
                    while (SqlReader.Read())
                    {
                        sIDYaAgregadas += ",";
                        sIDYaAgregadas += "'"+SqlReader[0].ToString()+"'";
                    }
                    DataView dvAdded = new DataView(dtBoletas);
                    dvAdded.RowFilter = "NumeroBoleta in ("+ sIDYaAgregadas +")";
                    this.gvBoletasYaIngresadas.DataSource = dvAdded;
                    this.gvBoletasYaIngresadas.DataBind();
                    ((DataView)this.gvBoletasAdd.DataSource).RowFilter += " AND NumeroBoleta NOT in ("+ sIDYaAgregadas +")";
                    this.gvBoletasAdd.DataBind();
                }
                this.Session[this.sSessionDVBoletasAdd] = (DataView)this.gvBoletasAdd.DataSource;
                this.lblBoletasInGrid.Text = this.gvBoletasAdd.Rows.Count.ToString();

                String sIDyaenDB = "";
                dsBoletas.dtBoletasDataTable teibol = (dsBoletas.dtBoletasDataTable)this.Session[this.sSessionDTBoletasToAdd];
                this.gvBoletasAddiNDB.DataSource = teibol;
                this.gvBoletasAddiNDB.DataBind();
                if (this.gvBoletasAddiNDB.Rows.Count > 0)
                {
                    sIDyaenDB = "'" + this.gvBoletasAddiNDB.Rows[0].Cells[1].Text+"'";
                    for (int i = 1; i < this.gvBoletasAddiNDB.Rows.Count; i++)
                    {
                        sIDyaenDB += ",'" + this.gvBoletasAddiNDB.Rows[i].Cells[1].Text+"'";
                    }
                    ((DataView)this.gvBoletasAdd.DataSource).RowFilter += " AND NumeroBoleta not in (" + sIDyaenDB + ")";
                    this.gvBoletasAdd.DataBind();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, -1, ex.Message, this.Request.Url.ToString());
            }
            
        }
        protected void ddlClientesProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.updateDDLClientesProv();
            this.updateDDLCodigo();
            updateDDLProductos();
            this.updateGridView();
        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            try
            {
	            dsBoletas.dtBoletasDataTable dtBoletas = new dsBoletas.dtBoletasDataTable();
	            MemoryStream data = new MemoryStream((((byte [])this.Session[this.sSessionBoletasFileContent])), false);
	            ExcelFileReader book = new ExcelFileReader(ref data);
	            book.Open();
	            this.ddlCiclos.DataBind();
	            int i = book.CurrentSheet.FirstRowNum + 1;
	            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
	            conn.Open();
	            SqlCommand comm = new SqlCommand();
	            comm.CommandText = "SELECT productoID, Nombre, codigoBascula FROM Productos ORDER BY codigoBascula";
	            comm.Connection = conn;
	            SqlDataAdapter sqlDA = new SqlDataAdapter(comm);
	            DataTable dtProds = new DataTable();
	            sqlDA.Fill(dtProds);
	
	            SqlCommand commProductores = new SqlCommand();
	            SqlConnection connProductores = new SqlConnection(myConfig.ConnectionInfo);
	            connProductores.Open();
	            commProductores.CommandText = "SELECT PRODUCTORID, APATERNO + ' ' + AMATERNO + ' ' + NOMBRE AS PRODUCTOR, CODIGOBOLETASFILE FROM PRODUCTORES ORDER BY CODIGOBOLETASFILE ASC";
                commProductores.Connection = connProductores;
	            SqlDataAdapter sqlDAProductores = new SqlDataAdapter(commProductores);
	            DataTable dtProductores = new DataTable();
	            sqlDAProductores.Fill(dtProductores);
	
	            // data of productores
	            do
	            {
	                string sTipo = book.getStringCellValue(i, 1);
	                if (sTipo == "Cliente" || sTipo == "Proveedor")
	                {
	                    // dtBoletas.Rows.Add(new object[3] { book.getStringCellValue(i, 2), sTipo, book.getStringCellValue(i + 4, 2) });
	
	                    String sNombre = book.getStringCellValue(i, 2);
	                    String sCodigo = book.getStringCellValue(i, 0);
	                    i += 4;
	                    while (book.getStringCellValue(i, 0) != "")
	                    {
	                        try
	                        {
		                        dsBoletas.dtBoletasRow newRow = dtBoletas.NewdtBoletasRow();
	                            newRow["codigoClienteProvArchivo"] = sCodigo;
		                        newRow["TipoClienteProd"] = sTipo;
		                        newRow["NombreProductor"] = sNombre;
	                            newRow["Producto"] = book.getStringCellValue(i, 0);
	                            DataRow[] foundRows = dtProds.Select(" codigoBascula = '"+ newRow["Producto"] +"'");
	                            if (foundRows.Length >0)
	                            {
	                                newRow["Producto"] = foundRows[0]["Nombre"].ToString();
	                                newRow["productoID"] = foundRows[0]["productoID"].ToString();
	                            }
	                            else
	                            {
	                                i++;
	                                continue;
	                            }
	                            newRow["userID"] = int.Parse(this.Session["USERID"].ToString());
	                            newRow["cicloID"] = int.Parse(this.ddlCiclos.SelectedItem.Value);
	
	                            DataRow[] drProductorID = dtProductores.Select(" codigoboletasfile = '" + newRow["codigoClienteProvArchivo"] + "'");
	                            if (drProductorID.Length > 0)
	                            {
	                                newRow["productorID"] = drProductorID[0]["productorID"];
	                                newRow["NombreProductor"] = drProductorID[0]["productor"];
	
	                            }
	                            else
	                            {
	                                i++;
	                                continue;
	                            }
	                              //este hardcode no va <<<<<<<<<<<<
	
		                        newRow["Placas"] = book.getStringCellValue(i, 1);
		                        newRow["NumeroBoleta"] = book.getStringCellValue(i, 2);
		                        newRow["FechaEntrada"] = DateTime.Parse(book.getStringCellValue(i, 3) + " " + book.getStringCellValue(i, 4));
		                        newRow["PesadorEntrada"] = book.getStringCellValue(i, 5);
	                            newRow["PesoDeEntrada"] = book.getFloatCellValue(i, 6);
		                        newRow["BasculaEntrada"] = book.getStringCellValue(i, 8);
		                        newRow["FechaSalida"] = DateTime.Parse(book.getStringCellValue(i, 9) + " " + book.getStringCellValue(i, 10));
	                            newRow["PesoDeSalida"] = book.getFloatCellValue(i, 11);
	                            newRow["pesonetoentrada"] = book.getFloatCellValue(i, 13);
	                            newRow["pesonetosalida"] =  book.getFloatCellValue(i, 15);
		                        newRow["PesadorSalida"] = book.getStringCellValue(i, 17);
		                        newRow["BasculaSalida"] = book.getStringCellValue(i++, 18);
		
		                        dtBoletas.Rows.Add(newRow);
	                        }
	                        catch (System.Exception ex)
	                        {
                                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(Session["USERID"].ToString()), " Error loading boletas data EX:" + ex.Message, this.Request.Url.ToString());
	                        }
	                    }
	                }
	            } while (++i < book.LastRowNum);
	
	            this.Session[this.sSessionDTBoletas] = dtBoletas;
	            this.lblBoletasInFile.Text = dtBoletas.Rows.Count.ToString();
	            this.PanelBoletas.Visible = true;
	            this.btnProcesar.Visible = false;
	            this.updateDDLClientesProv();
	            this.updateDDLCodigo();
	            this.updateDDLProductos();
	            this.updateGridView();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogMessage(Logger.typeLogMessage.CRITICAL, Logger.typeUserActions.SELECT, int.Parse(Session["USERID"].ToString()), " Error loading boletas data EX:" + ex.Message, this.Request.Url.ToString());
            }
            finally
            {
            }
        }

        protected void updateDDLProductos()
        {
            DataTable dtBoletas = (DataTable)this.Session[this.sSessionDTBoletas];
            DataTable dtProductos = dtBoletas.DefaultView.ToTable(true, new string[3] { "productoID", "Producto", "TipoClienteProd" });


            dtProductos.DefaultView.RowFilter = "TipoClienteProd = '" + this.ddlClientesProveedores.SelectedItem.Value + "'";


            this.ddlProductos.DataSource = dtProductos;
            this.ddlProductos.DataValueField = "productoID";
            this.ddlProductos.DataTextField = "Producto";
            this.ddlProductos.DataBind();
            if (dtProductos.DefaultView.Count > 0)
            {
                this.ddlProductos.SelectedIndex = 0;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            this.updateGridView();
        }

        protected void btnAddBolToDBList_Click(object sender, EventArgs e)
        {
            try
            {
                this.gvBoletasAdd.DataSource = this.gvBoletasAdd.DataSource;
                dsBoletas.dtBoletasDataTable teibol = (dsBoletas.dtBoletasDataTable)this.Session[this.sSessionDTBoletasToAdd];
                foreach (GridViewRow row in this.gvBoletasAdd.Rows)
                {
                    CheckBox check = (CheckBox)row.Cells[0].FindControl("chkRowSelected");
                    if (check.Checked)
                    {
                        DataView dv = (DataView)this.Session[this.sSessionDVBoletasAdd];
                        DataRow newRow = dv[row.RowIndex].Row;// (dsBoletas.dtBoletasRow)dv.Table.Rows[row.RowIndex];
                        teibol.ImportRow(newRow);   
                    }
                }
                this.gvBoletasAddiNDB.DataSource = teibol;
                this.gvBoletasAddiNDB.DataBind();
                this.Session[this.sSessionDTBoletasToAdd] = teibol;
                this.updateGridView();
                this.lblBoletasAgregar.Text = this.gvBoletasAddiNDB.Rows.Count.ToString();
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        protected void btnQuitarBoletas_Click(object sender, EventArgs e)
        {
            try
            {
                dsBoletas.dtBoletasDataTable teibol = (dsBoletas.dtBoletasDataTable)this.Session[this.sSessionDTBoletasToAdd];
                this.gvBoletasAddiNDB.DataSource = this.gvBoletasAddiNDB.DataSource;
                for (int i = this.gvBoletasAddiNDB.Rows.Count - 1; i >= 0; i-- )
                {
                    GridViewRow row = this.gvBoletasAddiNDB.Rows[i];
                    CheckBox check = (CheckBox)row.Cells[0].FindControl("chkRowToDel");
                    if (check.Checked)
                    {
                        teibol.Rows[row.RowIndex].Delete();
                    }
                }
                teibol.AcceptChanges();
                this.gvBoletasAddiNDB.DataSource = teibol;
                this.gvBoletasAddiNDB.DataBind();
                this.Session[this.sSessionDTBoletasToAdd] = teibol;
                this.updateGridView();
                this.lblBoletasAgregar.Text = this.gvBoletasAddiNDB.Rows.Count.ToString();
            }
            catch (System.Exception ex)
            {

            }
        }

        protected void btnAddBoletasInDB_Click(object sender, EventArgs e)
        {
            dsBoletas.dtBoletasDataTable teibol = (dsBoletas.dtBoletasDataTable)this.Session[this.sSessionDTBoletasToAdd];
            foreach(dsBoletas.dtBoletasRow row in teibol.Rows )
            {
                row.cicloID = int.Parse(this.ddlCiclos.SelectedValue);
                row.bodegaID = int.Parse(this.ddlBodegaBoletasAdd.SelectedValue);
            }
            this.lblBoletasAdded.Text = dbFunctions.insertBoletas(ref teibol).ToString();
            teibol.Rows.Clear();
            this.updateGridView();
        }

        protected void ddlProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.updateDDLClientesProv();
            this.updateDDLCodigo();
            this.updateGridView();
        }

        protected void gvBoletasAddiNDB_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvBoletasAddiNDB.EditIndex = e.NewEditIndex;
            this.gvBoletasAddiNDB.DataBind();
        }

        protected void gvBoletasAddiNDB_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvBoletasAddiNDB.EditIndex = -1;
            this.gvBoletasAddiNDB.DataBind();
        }

        protected void gvBoletasAddiNDB_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            dsBoletas.dtBoletasDataTable teibol = (dsBoletas.dtBoletasDataTable)this.Session[this.sSessionDTBoletasToAdd];
            GridViewRow row = this.gvBoletasAddiNDB.Rows[e.RowIndex];
            teibol.Rows[e.RowIndex]["Ticket"] = ((TextBox)row.Cells[3].Controls[0]).Text;
            teibol.Rows[e.RowIndex]["Placas"] = ((TextBox)row.Cells[7].Controls[0]).Text;
            float KG =0;
            if (float.TryParse(teibol.Rows[e.RowIndex]["pesonetoentrada"].ToString(),out KG) && KG <= 0)
            {
                if(!float.TryParse(teibol.Rows[e.RowIndex]["pesonetosalida"].ToString(),out KG))
                    KG = 0;
            }
            teibol.Rows[e.RowIndex]["humedad"] = float.Parse(((TextBox)row.Cells[14].Controls[0]).Text);
            teibol.Rows[e.RowIndex]["dctoHumedad"] = Utils.getDesctoHumedad(float.Parse(((TextBox)row.Cells[14].Controls[0]).Text), KG);
            this.Session[this.sSessionDTBoletasToAdd] = teibol;
            this.gvBoletasAddiNDB.EditIndex = -1;
            this.gvBoletasAddiNDB.DataBind();
        }


        private void ToogleChecked(ref GridView gv, bool checkState)
        {
            foreach (GridViewRow row in gv.Rows) 
            { 
                // Access the CheckBox 
                CheckBox cb = (CheckBox)row.FindControl("chkRowSelected"); 
                if (cb != null) 
                    cb.Checked = checkState; 
            } 
        }

        protected void chkBolToAddSelADD_CheckedChanged(object sender, EventArgs e)
        {
            this.ToogleChecked(ref this.gvBoletasAdd, ((CheckBox)sender).Checked);
        }
    }
}
