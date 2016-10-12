using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace Garibay
{
    public partial class frmAddSolicitud2010 : BasePage
    {
        internal void AddJSToControls()
        {
            StringBuilder sJS = new StringBuilder();
            sJS.Append("CopyTextBoxValue('");
            sJS.Append(this.txtSuperficieFinanciada.ClientID);
            sJS.Append("','");
            sJS.Append(this.txtSuperifieASembrar.ClientID);
            sJS.Append("');");
            this.txtSuperficieFinanciada.Attributes.Add("Onchange", sJS.ToString());
        }

        protected string UpdateSDSForSisBanco(string sSelectCmd)
        {
            sSelectCmd = sSelectCmd.Replace(" Creditos ", " CreditosSisBancos ");
            sSelectCmd = sSelectCmd.Replace(" Creditos.", " CreditosSisBancos.");
            sSelectCmd = sSelectCmd.Replace(" Solicitudes ", " SolicitudesSisBancos ");
            sSelectCmd = sSelectCmd.Replace("Solicitudes.", " SolicitudesSisBancos.");
            sSelectCmd = sSelectCmd.Replace(" solicitud_SeguroAgricola ", " SolicitudesSisBancos_SeguroAgricola ");
            sSelectCmd = sSelectCmd.Replace("solicitud_SeguroAgricola.", " SolicitudesSisBancos_SeguroAgricola.");
            sSelectCmd = sSelectCmd.Replace(" SegurosAgricolasPredios ", " SegurosAgricolasPrediosSisBancos ");
            sSelectCmd = sSelectCmd.Replace("SegurosAgricolasPredios.", " SegurosAgricolasPrediosSisBancos.");
            return sSelectCmd;
        }

        protected void UpdateIfSistemaBanco()
        {
            if (this.IsSistemBanco)
            {
                sdsSegurosAgregados.SelectCommand = this.UpdateSDSForSisBanco(sdsSegurosAgregados.SelectCommand);
                sdsPrediosSeguro.SelectCommand = this.UpdateSDSForSisBanco(sdsPrediosSeguro.SelectCommand);
                sdsTipoSeguro.SelectCommand = this.UpdateSDSForSisBanco(sdsTipoSeguro.SelectCommand);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.txtInteres.Text = "18";
                this.lblAportacionProductor.Visible = this.txtAportacionProductor.Visible = this.IsSistemBanco;
                this.txtMontoDelCredito.ReadOnly = this.txtMontoPorHectarea.ReadOnly = !this.IsSistemBanco;
                if (this.IsSistemBanco)
                {
                    this.sdsPrediosSeguro.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.sdsPrediosSeguro.SelectCommand);
                }
                if (!this.IsPostBack)
                {
                    this.ddlAddToACredito.DataBind();
                    this.pnlAddToACredito_CollapsiblePanelExtender.Collapsed = true;
                    this.UpdateIfSistemaBanco();
                    this.AddJSToControls();
                    this.ddlEstadoSolicitud.DataBind();
                    this.ddlCiclo.DataBind();
                    this.txtFecha.Text = Utils.Now.ToString("dd/MM/yyyy");
                    this.cmbProductores.DataBind();
                    this.cmbSexo0.DataBind();
                    this.cmbEstado.DataBind();
                    this.cmbEstadoCivil.DataBind();
                    this.cmbRegimen.DataBind();
                    this.cmbProductores.DataBind();
                    this.cargadatosProductor(int.Parse(this.cmbProductores.SelectedValue));
                    this.pnlSolicitudesData.Visible = false;
                    this.CalculaMontoDelCredito();
                    this.drpdlTipoSeguro.DataBind();
                    this.drpdlTipoSeguro.SelectedIndex = 0;
                    this.LoadSelectedSeguroData();
                    if (this.LoadEncryptedQueryString() > 0)
                    {
                        int iSolicitudID = -1;
                        if (int.TryParse(this.myQueryStrings["SolID"].ToString(), out iSolicitudID) && iSolicitudID > 0)
                        {
                            this.txtSolID.Text = iSolicitudID.ToString();
                            this.LoadSolicitudData();
                            this.chkAddToACredito.Visible = false;
                        }
                    }
                    this.UpdateAddModifySegurosBtns();
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "page load solicitud", ref ex);
            }
            this.pnlSolicitudResult.Visible = false;
            this.pnlSeguroResult.Visible = false;
            this.pnlNewSeguro.Visible = false;
        }

        protected void cmbProductores_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cargadatosProductor(int.Parse(this.cmbProductores.SelectedValue));
        }

        protected bool cargadatosProductor(int idproductor)
        {
            string qrySel = "SELECT Productores.IFE, Productores.CURP, Productores.domicilio, "
            + " Productores.poblacion, Productores.municipio, Productores.estadoID, Productores.CP, "
            + " Productores.RFC, Productores.telefono, Productores.telefonotrabajo, Productores.celular, "
            + " Productores.fax, Productores.email, Productores.estadocivilID, Productores.regimenID, "
            + " Productores.sexoID, Productores.colonia, Productores.conyugue FROM Productores "
            + " WHERE productorID = @productorID";
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            SqlCommand cmdsel = new SqlCommand(qrySel, conGaribay);
            conGaribay.Open();
            try
            {
                cmdsel.Parameters.Add("@productorID", SqlDbType.Int).Value = idproductor;
                SqlDataReader datos;
                datos = cmdsel.ExecuteReader();
                if (!datos.HasRows)
                {
                    return false;
                }
                if (datos.Read())
                {
                    this.txtIfe.Text = datos["IFE"].ToString();
                    this.txtCurp.Text = datos["CURP"].ToString();
                    this.txtDomicilio.Text = datos["domicilio"].ToString();
                    this.txtPoblacion.Text = datos[3].ToString();
                    this.txtMunicipio.Text = datos[4].ToString();
                    this.cmbEstado.SelectedValue = datos[5].ToString();
                    this.txtCodigopostal.Text = datos[6].ToString();
                    this.txtRfc.Text = datos[7].ToString();
                    this.txtTelefono.Text = datos[8].ToString();
                    this.txtTelefonotrabajo.Text = datos[9].ToString();
                    this.txtCelular.Text = datos[10].ToString();
                    this.txtFax.Text = datos[11].ToString();
                    this.txtCorreo.Text = datos[12].ToString();
                    this.cmbEstadoCivil.SelectedValue = datos[13].ToString();
                    if (datos[14] != null && datos[14].ToString().Length > 0)
                    {
                        this.cmbRegimen.SelectedValue = datos[14].ToString();
                    }
                    else
                    {
                        this.cmbRegimen.SelectedIndex = 0;
                    }
                    this.cmbSexo0.SelectedValue = datos[15].ToString();
                    this.txtColonia.Text =  datos["colonia"] == null? "": datos["colonia"].ToString();
                    this.txtConyugue.Text = datos["conyugue"] == null? "" : datos["conyugue"].ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "cargadatosProductor", ref ex);
                return false;
            }
            finally
            {
                conGaribay.Close();
            }
            return true;
        }

        internal int GetIntFromText(TextBox t)
        {
            int iValue = 0;
            if (!int.TryParse(t.Text, out iValue))
            {
                iValue = 0;
            }
            return iValue;
        }
        internal double GetDoubleFromText(TextBox t)
        {
            double iValue = 0;
            if (!double.TryParse(t.Text, out iValue))
            {
                iValue = 0;
            }
            return iValue;
        }
        protected void UpdateCreditoData()
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand cmdins = new SqlCommand();
                cmdins.Connection = conn;
                cmdins.CommandText = "update "
                    + (this.IsSistemBanco ? "CreditosSisBancos" : "Creditos")
                    + " set productorID = @productorID, cicloID = @cicloID, fecha=@fecha, userID = @userID, "
                    + " statusID = @statusID, LimiteDeCredito=@LimiteDeCredito, InteresAnual=@InteresAnual where creditoID IN "
                    + " (SELECT Creditos.creditoID FROM Creditos INNER JOIN "
                    + (this.IsSistemBanco ? " SolicitudesSisBancos " : " Solicitudes ") 
                    + " ON Creditos.creditoID = Solicitudes.creditoID WHERE (Solicitudes.solicitudID = "
                    + " @solicitudID))";
                if (this.IsSistemBanco)
                {
                    cmdins.CommandText = this.UpdateSDSForSisBanco(cmdins.CommandText);
                }
                cmdins.Parameters.Add("@statusID", SqlDbType.Int).Value = int.Parse(this.ddlEstadoSolicitud.SelectedValue);
                cmdins.Parameters.Add("@productorID", SqlDbType.Int).Value = int.Parse(this.cmbProductores.SelectedValue);
                cmdins.Parameters.Add("@cicloID", SqlDbType.Int).Value = int.Parse(this.ddlCiclo.SelectedValue);
                cmdins.Parameters.Add("@fecha", SqlDbType.DateTime).Value = Utils.converttoLongDBFormat(this.txtFecha.Text);
                cmdins.Parameters.Add("@userID", SqlDbType.Int).Value = this.UserID;
                double limite = 0;
                double.TryParse(this.txtMontoDelCredito.Text, out limite);
                cmdins.Parameters.Add("@LimiteDeCredito", SqlDbType.Float).Value = limite;
                cmdins.Parameters.Add("@solicitudID", SqlDbType.Int).Value = this.txtSolID.Text;
                double dTasaInteres = 0;
                if (!double.TryParse(this.txtInteres.Text, out dTasaInteres))
                {
                    dTasaInteres = 0;
                }
                cmdins.Parameters.Add("@InteresAnual", SqlDbType.Float).Value = dTasaInteres / 100;
                cmdins.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "update credito", ref ex);
            }
            finally
            {
                conn.Close();
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "UPDATE    "
                + (this.IsSistemBanco ? " SolicitudesSisBancos " : " Solicitudes ")
                + " SET fecha = @fecha, productorID = @productorID, "
                + " Experiencia = @Experiencia, otrosPasivosMonto = @otrosPasivosMonto, "
                + " otrosPasivosAQuienLeDebe = @otrosPasivosAQuienLeDebe, "
                + " superficieFinanciada = @superficieFinanciada, Superficieasembrar = @Superficieasembrar, "
                + " casaHabitacion = @casaHabitacion, rastra = @rastra, Arado = @Arado, "
                + " Cultivadora = @Cultivadora, Subsuelo = @Subsuelo, tractor = @tractor, "
                + " sembradora = @sembradora, camioneta = @camioneta, otrosActivos = @otrosActivos, "
                + " garantiaLiquida = @garantiaLiquida, ConceptoSoporteGarantia = @ConceptoSoporteGarantia, "
                + " montoSoporteGarantia = @montoSoporteGarantia, "
                + " domicilioDelDeposito = @domicilioDelDeposito, aval1 = @aval1, Aval1Dom = @Aval1Dom, "
                + " aval2 = @aval2, Aval2Dom = @Aval2Dom, firmaAutorizada1 = @firmaAutorizada1, "
                + " firmaAutorizada2 = @firmaAutorizada2, firmaAutorizada3 = @firmaAutorizada3, "
                + " firmaAutorizada4 = @firmaAutorizada4, firmaAutorizada5 = @firmaAutorizada5, "
                + " testigo1 = @testigo1, testigo2 = @testigo2, totalActivos = @totalActivos, Monto=@Monto, "
                + " plazo=@plazo, Descripciondegarantias=@Descripciondegarantias, ejido= @ejido ";
                if (this.IsSistemBanco)
                    comm.CommandText += ", aportaciondelproductor=@aportaciondelproductor ";
                comm.CommandText += " WHERE "
                + " (solicitudID = @solicitudID)";
                comm.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Parse(this.txtFecha.Text);
                comm.Parameters.Add("@productorID", SqlDbType.Int).Value = int.Parse(this.cmbProductores.SelectedValue);
                comm.Parameters.Add("@Experiencia", SqlDbType.Int).Value = GetIntFromText(this.txtExperiencia);
                comm.Parameters.Add("@otrosPasivosMonto", SqlDbType.Float).Value = GetDoubleFromText(this.txtOtrosPasivosMonto);
                comm.Parameters.Add("@otrosPasivosAQuienLeDebe", SqlDbType.VarChar).Value = this.txtOtrosPasivosAquienLeDebe.Text;
                comm.Parameters.Add("@superficieFinanciada", SqlDbType.Float).Value = GetDoubleFromText(this.txtSuperficieFinanciada);
                comm.Parameters.Add("@Superficieasembrar", SqlDbType.Float).Value = GetDoubleFromText(this.txtSuperifieASembrar);
                comm.Parameters.Add("@casaHabitacion", SqlDbType.Int).Value = GetIntFromText(this.txtActualActivosCasaHabitacion);
                comm.Parameters.Add("@rastra", SqlDbType.Int).Value = GetIntFromText(this.txtActualActivosRastra);
                comm.Parameters.Add("@Arado", SqlDbType.Int).Value = GetIntFromText(this.txtActualActivosArado);
                comm.Parameters.Add("@Cultivadora", SqlDbType.Int).Value = GetIntFromText(this.txtActualActivosCultivadora);
                comm.Parameters.Add("@Subsuelo", SqlDbType.Int).Value = GetIntFromText(this.txtActualActivosSubsuelo);
                comm.Parameters.Add("@tractor", SqlDbType.Int).Value = GetIntFromText(this.txtActualActivosTractor);
                comm.Parameters.Add("@sembradora", SqlDbType.Int).Value = GetIntFromText(this.txtActualActivosSembradora);
                comm.Parameters.Add("@camioneta", SqlDbType.Int).Value = GetIntFromText(this.txtActualActivosCamioneta);
                comm.Parameters.Add("@otrosActivos", SqlDbType.Float).Value = GetDoubleFromText(this.txtActualActivosOtrosActivos);
                comm.Parameters.Add("@garantiaLiquida", SqlDbType.Float).Value = GetDoubleFromText(this.txtDatosGarantiaGarantiaLiquida);
                comm.Parameters.Add("@ConceptoSoporteGarantia", SqlDbType.VarChar).Value = this.txtDatosGarantiaConcepto.Text;
                comm.Parameters.Add("@montoSoporteGarantia", SqlDbType.Float).Value = GetDoubleFromText(this.txtDatosGarantiaMontoSoporte);
                comm.Parameters.Add("@domicilioDelDeposito", SqlDbType.Text).Value = this.txtDatosGarantiaDomicilioDeposito.Text;
                comm.Parameters.Add("@aval1", SqlDbType.VarChar).Value = this.txtAval1.Text;
                comm.Parameters.Add("@Aval1Dom", SqlDbType.VarChar).Value = this.txtDomAval1.Text;
                comm.Parameters.Add("@aval2", SqlDbType.VarChar).Value = this.txtAval2.Text;
                comm.Parameters.Add("@Aval2Dom", SqlDbType.VarChar).Value = this.txtDomAval2.Text;
                comm.Parameters.Add("@firmaAutorizada1", SqlDbType.VarChar).Value = this.txtAutorizado1.Text;
                comm.Parameters.Add("@firmaAutorizada2", SqlDbType.VarChar).Value = this.txtAutorizado2.Text;
                comm.Parameters.Add("@firmaAutorizada3", SqlDbType.VarChar).Value = this.txtAutorizado3.Text;
                comm.Parameters.Add("@firmaAutorizada4", SqlDbType.VarChar).Value = this.txtAutorizado4.Text;
                comm.Parameters.Add("@firmaAutorizada5", SqlDbType.VarChar).Value = this.txtAutorizado5.Text;
                comm.Parameters.Add("@testigo1", SqlDbType.VarChar).Value = this.txtTestigo1.Text;
                comm.Parameters.Add("@testigo2", SqlDbType.VarChar).Value = this.txtTestigo2.Text;
                comm.Parameters.Add("@totalActivos", SqlDbType.Float).Value = Utils.GetSafeFloat(this.txtActualActivosTotalActivos.Text);
                comm.Parameters.Add("@Monto", SqlDbType.Float).Value = Utils.GetSafeFloat(this.txtMontoDelCredito.Text);
                comm.Parameters.Add("@plazo", SqlDbType.Int).Value = Utils.GetSafeInt(this.txtPlazo);
                comm.Parameters.Add("@Descripciondegarantias", SqlDbType.Text).Value = this.txtDescGarantias.Text;
                comm.Parameters.Add("@solicitudID", SqlDbType.Int).Value = GetIntFromText(this.txtSolID);
                comm.Parameters.Add("@ejido", SqlDbType.NVarChar).Value = this.textBoxEjido.Text;
                if (this.IsSistemBanco)
                    comm.Parameters.Add("@aportaciondelproductor", SqlDbType.Float).Value = GetDoubleFromText(this.txtAportacionProductor);

                if (((int)comm.ExecuteNonQuery()) == 1)
                {
                    this.UpdateSolicitudStatus();
                    this.UpdateCreditoData();
                    this.ShowPnlSolicitudResult("SOLICITUD GUARDADA CORRECTAMENTE  A LAS: " + Utils.Now.ToString(), true);
                }
                else
                {
                    this.ShowPnlSolicitudResult("SOLICITUD NO SE PUDO GUARDAR", false);
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "Actualizando solicitud", ref ex);
                this.ShowPnlSolicitudResult("NO SE PUDO GUARDAR LA NOTA EL ERROR ES: " + ex.Message, false);
            }
            finally
            {
                conn.Close();
            }
        }
        internal void ShowPnlSolicitudResult(string text, bool IsOK)
        {
            this.pnlSolicitudResult.Visible = true;
            this.imgBienSolicitudResult.Visible = IsOK;
            this.imgMalSolicitudResult.Visible = !IsOK;
            this.lblSolicitudResult.Text = text;

        }
        internal void UpdatePrintLinks()
        {

            //TODO: update link to print new solicitudes
            string sFileName = "SOLICITUD.pdf";
            sFileName = sFileName.Replace(" ", "_");
            string sURL = "frmDescargaTmpFile.aspx";
            string datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
            datosaencriptar = datosaencriptar + "solID=" + this.txtSolID.Text + "&";
            datosaencriptar += "impSol2010=1&";
            string URLcomplete = sURL + "?data=";
            URLcomplete += Utils.encriptacadena(datosaencriptar);
            JSUtils.OpenNewWindowOnClick(ref this.lnkPrintSolicitud, URLcomplete, "Solicitud", true);


            sFileName = "TERMINOS Y CONDICIONES.pdf";
            sFileName = sFileName.Replace(" ", "_");
            sURL = "frmDescargaTmpFile.aspx";
            datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
            datosaencriptar = datosaencriptar + "solID=" + this.txtSolID.Text + "&";
            datosaencriptar += "imptermsandcond=1&";
            URLcomplete = sURL + "?data=";
            URLcomplete += Utils.encriptacadena(datosaencriptar);
            JSUtils.OpenNewWindowOnClick(ref this.lnkTermAndCond, URLcomplete, "Terminos y condiciones", true);



            //////////////////////////////////////////////////////////////////////////
            /*string sFileName = "PAGARE.pdf";
            sFileName = sFileName.Replace(" ", "_");
            string sURL = "frmDescargaTmpFile.aspx";
            string datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
            
            datosaencriptar += "solID=" + sol.SolicitudID.ToString() + "&";
            datosaencriptar += "impPagare=1&";
            string URLcomplete = sURL + "?data=";
            URLcomplete += Utils.encriptacadena(datosaencriptar);
            lnk.NavigateUrl = this.Request.Url.ToString();
            lnk.Text = "PAGARE";
            JSUtils.OpenNewWindowOnClick(ref lnk, URLcomplete, "Pagare", true);*/
            //////////////////////////////////////////////////////////////////////////

            Solicitudes sol;
            if (this.IsSistemBanco)
                sol = SolicitudesSisBancos.Get(int.Parse(this.txtSolID.Text));
            else
                sol = Solicitudes.Get(int.Parse(this.txtSolID.Text));
            sFileName = "PAGARE.pdf";
            sFileName = sFileName.Replace(" ", "_");
            sURL = "frmDescargaTmpFile.aspx";
            datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
            datosaencriptar = datosaencriptar + "creditoID=" + sol.CreditoID.ToString() + "&";
            datosaencriptar = datosaencriptar + "solID=" + this.txtSolID.Text + "&";
            datosaencriptar += "impPagare=1&";
            URLcomplete = sURL + "?data=";
            URLcomplete += Utils.encriptacadena(datosaencriptar);
            JSUtils.OpenNewWindowOnClick(ref this.lnkPagare, URLcomplete, "Pagare", true);

            sFileName = "CONTRATO.pdf";
            sFileName = sFileName.Replace(" ", "_");
            sURL = "frmDescargaTmpFile.aspx";
            datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
            datosaencriptar = datosaencriptar + "solID=" + this.txtSolID.Text + "&";
            datosaencriptar += "impcont=1&";
            URLcomplete = sURL + "?data=";
            URLcomplete += Utils.encriptacadena(datosaencriptar);
            JSUtils.OpenNewWindowOnClick(ref this.lnkContrato, URLcomplete, "Contrato", true);



            sFileName = "BUROCREDITO.pdf";
            sFileName = sFileName.Replace(" ", "_");
            sURL = "frmDescargaTmpFile.aspx";
            datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
            datosaencriptar = datosaencriptar + "solID=" + this.txtSolID.Text + "&";
            datosaencriptar += "buroCredito=1&";
            URLcomplete = sURL + "?data=";
            URLcomplete += Utils.encriptacadena(datosaencriptar);
            JSUtils.OpenNewWindowOnClick(ref this.lnkBuro, URLcomplete, "BuroCredito", true);


            sFileName = "CARTACOMPROMISO.pdf";
            sFileName = sFileName.Replace(" ", "_");
            sURL = "frmDescargaTmpFile.aspx";
            datosaencriptar = "filename=" + sFileName + "&ContentType=application/pdf&";
            datosaencriptar = datosaencriptar + "solID=" + this.txtSolID.Text +"&";
            datosaencriptar += "cartaCompromiso=1&";
            URLcomplete = sURL + "?data=";
            URLcomplete += Utils.encriptacadena(datosaencriptar);
            JSUtils.OpenNewWindowOnClick(ref this.lnkCartaCompromiso, URLcomplete, "CartaCompromiso", true);



        }
        protected void gridViewPrediosSeguro_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            string query = "delete from SegurosAgricolasPredios"
                + (this.IsSistemBanco ? "SisBancos" : " ")
                + " where SegAgrPrediosID = @SegAgrPrediosID ";
            conGaribay.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(query, conGaribay);
                cmd.Parameters.Add("@SegAgrPrediosID", SqlDbType.Int).Value = int.Parse(this.gridViewPrediosSeguro.DataKeys[e.RowIndex]["SegAgrPrediosID"].ToString());
                cmd.ExecuteNonQuery();
                this.gridViewPrediosSeguro.DataBind();
                this.txtSeguroNombrePredio.Text = string.Empty;
                this.txtSeguroPredioSuperficie.Text = string.Empty;
                this.txtSeguroPredioUbicacion.Text = string.Empty;
                this.gridViewSegurosAgricolas.DataBind();

            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error deleting predio to seguro in solicitud", ref ex);

            }
        }
        internal void LoadSelectedSeguroData()
        {
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "SELECT Descripcion, CostoPorHectarea FROM SegurosAgricolas WHERE (seguroID = @seguroID)";
                comm.Parameters.Add("@seguroID", SqlDbType.Int).Value = this.drpdlTipoSeguro.SelectedValue;
                SqlDataReader rd = dbFunctions.ExecuteReader(comm);
                if (rd != null && rd.HasRows && rd.Read())
                {
                    this.txtDescripcionSeguro.Text = rd["Descripcion"].ToString();
                    this.txtCostoporHectarea.Text = ((double)rd["CostoPorHectarea"]).ToString("C2");
                }
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "obteniendo datos de seguro", ref ex);
            }
            finally
            {
                if (comm.Connection != null)
                {
                    comm.Connection.Close();
                }
            }
        }

        protected void drpdlTipoSeguro_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadSelectedSeguroData();
            this.CalculaCostoSeguro();
        }
        internal void CalculaCostoSeguro()
        {
            this.txtCostoTotalSeguro.Text = (Utils.GetSafeFloat(this.txtCostoporHectarea.Text) *
                                            Utils.GetSafeFloat(this.txtHasAseguradas.Text)).ToString("C2");
        }
        protected void btnAddSeguro_Click(object sender, EventArgs e)
        {
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conGaribay.Open();
                SqlCommand cmdGaribay = new SqlCommand();
                cmdGaribay.Connection = conGaribay;
                cmdGaribay.CommandText = "insert into solicitud_SeguroAgricola (solicitudID, seguroID, "
                + " hectAseguradas, descParcelas, CostoTotalSeguro) " 
                + "values (@solicitudId, @seguroID, @hectAseguradas, @descParcelas, @CostoTotalSeguro) ";
                if (this.IsSistemBanco)
                {
                    cmdGaribay.CommandText = cmdGaribay.CommandText.Replace("solicitud_SeguroAgricola", "SolicitudesSisBancos_SeguroAgricola");
                }

                cmdGaribay.Parameters.Add("@solicitudId", SqlDbType.Int).Value = int.Parse(this.txtSolID.Text);
                cmdGaribay.Parameters.Add("@seguroID", SqlDbType.Int).Value = int.Parse(this.drpdlTipoSeguro.SelectedValue);
                double hectAseguradas = 0, costoTotalSeguro = 0, costoPorHectarea = 0;
                double.TryParse(this.txtHasAseguradas.Text, out hectAseguradas);
                cmdGaribay.Parameters.Add("@hectAseguradas", SqlDbType.Float).Value = hectAseguradas;
                cmdGaribay.Parameters.Add("@descParcelas", SqlDbType.Text).Value = ""; //  this.txtDescParcelas.Text;
                //double.TryParse(this.txtCostoporHectarea.Text, out costoPorHectarea);
                costoPorHectarea = Utils.GetSafeFloat(this.txtCostoporHectarea.Text);
                costoTotalSeguro = costoPorHectarea * hectAseguradas;
                cmdGaribay.Parameters.Add("@CostoTotalSeguro", SqlDbType.Float).Value = costoTotalSeguro;
                cmdGaribay.ExecuteNonQuery();
                this.pnlNewSeguro.Visible = true;
                this.lblNewSeguro.Text = "SEGURO AGREGADO EXITOSAMENTE A LA SOLICITUD.";
                this.imgBien.Visible = true;
                this.imgMal.Visible = false;
                this.txtHasAseguradas.Text = string.Empty;
                this.txtCostoTotalSeguro.Text = "0.00";
                if (this.IsSistemBanco)
                {
                    this.sdsSegurosAgregados.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.sdsSegurosAgregados.SelectCommand);
                }
                this.gridViewSegurosAgricolas.DataBind();
                this.pnlSeguro.Visible = true;

            }
            catch (System.Exception ex)
            {
                this.pnlNewSeguro.Visible = true;
                this.lblNewSeguro.Text = "NO SE HA AGREGADO EL SEGURO A LA SOLICITUD. Error: " + ex.Message;
                this.imgBien.Visible = false;
                this.imgMal.Visible = true;
            }
            finally
            {
                conGaribay.Close();
                this.gridViewSegurosAgricolas.DataBind();
                //this.calculaLimiteDeCredito();
                //this.calculaRecComplementatios();
            }
        }

        protected void btnModificarSeguro_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conn.Open();
                //UPDATE    solicitud_SeguroAgricola SET seguroID =, hectAseguradas =, descParcelas =, CostoTotalSeguro = WHERE     (sol_sa_ID = @sol_sa_ID)
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "UPDATE solicitud_SeguroAgricola SET seguroID=@seguroID, "
                + " hectAseguradas=@hectAseguradas, CostoTotalSeguro=@CostoTotalSeguro WHERE "
                + " (sol_sa_ID = @sol_sa_ID)";

                if (this.IsSistemBanco)
                {
                    comm.CommandText = comm.CommandText.Replace("solicitud_SeguroAgricola", "SolicitudesSisBancos_SeguroAgricola");
                }

                comm.Parameters.Add("@seguroID", SqlDbType.Int).Value = this.drpdlTipoSeguro.SelectedValue;
                double dHAseguradas = 0;
                if (!double.TryParse(this.txtHasAseguradas.Text, out dHAseguradas))
                {
                    dHAseguradas = 0.0;
                }
                comm.Parameters.Add("@hectAseguradas", SqlDbType.Float).Value = dHAseguradas;
                double dCostoSeguro = 0;
                if (!double.TryParse(this.txtCostoTotalSeguro.Text, out dCostoSeguro))
                {
                    dCostoSeguro = Utils.GetSafeFloat(this.txtCostoTotalSeguro.Text);
                }
                comm.Parameters.Add("@CostoTotalSeguro", SqlDbType.Float).Value = dCostoSeguro;
                comm.Parameters.Add("@sol_sa_ID", SqlDbType.Int).Value = this.lblNumSol_Seguro.Text;
                comm.Connection = conn;
                comm.ExecuteNonQuery();
                this.LimpiaCamposSeguro();
                this.gridViewSegurosAgricolas.SelectedIndex = -1;
                this.gridViewSegurosAgricolas.DataBind();
                this.UpdateAddModifySegurosBtns();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "updating seguro", ref ex);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnAgregarPredioSeguro_Click(object sender, EventArgs e)
        {
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            string query = "insert into SegurosAgricolasPredios (sol_sa_ID, Folio, Nombre, Superficie, "
            + "Ubicacion) values (" 
            + " @sol_sa_ID, @Folio, @Nombre, @Superficie, @Ubicacion) ";
            if (this.IsSistemBanco)
            {
                query = query.Replace("SegurosAgricolasPredios", "SegurosAgricolasPrediosSisBancos");
            }
            SqlCommand cmdGaribay = new SqlCommand(query, conGaribay);
            try
            {
                conGaribay.Open();
                cmdGaribay.Parameters.Add("@sol_sa_ID", SqlDbType.Int).Value = int.Parse(this.lblNumSol_Seguro.Text);
                cmdGaribay.Parameters.Add("@Folio", SqlDbType.NVarChar).Value = this.txtSeguroFolioPredio.Text;
                cmdGaribay.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = this.txtSeguroNombrePredio.Text;
                double sup = 0;
                if(!double.TryParse(this.txtSeguroPredioSuperficie.Text, out sup))
                {
                    sup = 0;
                }
                cmdGaribay.Parameters.Add("@Superficie", SqlDbType.Float).Value = sup;
                cmdGaribay.Parameters.Add("@Ubicacion", SqlDbType.NVarChar).Value = this.txtSeguroPredioUbicacion.Text;
                cmdGaribay.ExecuteNonQuery();
                if (this.IsSistemBanco)
                {
                    this.sdsPrediosSeguro.SelectCommand = dbFunctions.UpdateSDSForSisBanco(this.sdsPrediosSeguro.SelectCommand);
                }
                this.gridViewPrediosSeguro.DataBind();
                this.txtSeguroNombrePredio.Text = string.Empty;
                this.txtSeguroPredioSuperficie.Text = string.Empty;
                this.txtSeguroPredioUbicacion.Text = string.Empty;
                this.txtSeguroFolioPredio.Text = string.Empty;

            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "Error insertig predio to seguro in solicitud", ref ex);

            }
            finally
            {
                conGaribay.Close();
            }
        }

        internal void LimpiaCamposSeguro()
        {
            this.drpdlTipoSeguro.DataBind();
            this.drpdlTipoSeguro.SelectedIndex = 0;
            this.drpdlTipoSeguro_SelectedIndexChanged(null, null);
            this.txtHasAseguradas.Text = "";
            this.txtCostoTotalSeguro.Text = "";
            this.lblNumSol_Seguro.Text = "";

        }

        internal void LoadSolicitudData()
        {
            SqlConnection conn = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                conn.Open();
                comm.CommandText = "SELECT Creditos.cicloID, Solicitudes.fecha, Creditos.Interesanual, "
                + " Solicitudes.productorID, Solicitudes.Experiencia, Solicitudes.otrosPasivosMonto, "
                + " Solicitudes.otrosPasivosAQuienLeDebe, Solicitudes.superficieFinanciada, "
                + " Solicitudes.Superficieasembrar, Solicitudes.casaHabitacion, Solicitudes.rastra, "
                + " Solicitudes.Arado, Solicitudes.Cultivadora, Solicitudes.Subsuelo, Solicitudes.tractor, "
                + " Solicitudes.sembradora, Solicitudes.camioneta, Solicitudes.otrosActivos, "
                + " Solicitudes.garantiaLiquida, Solicitudes.ConceptoSoporteGarantia, Solicitudes.montoSoporteGarantia, "
                + " Solicitudes.domicilioDelDeposito, Solicitudes.aval1, Solicitudes.Aval1Dom, Solicitudes.aval2, "
                + " Solicitudes.Aval2Dom, Solicitudes.firmaAutorizada1, Solicitudes.firmaAutorizada2, Solicitudes.firmaAutorizada3, "
                + " Solicitudes.firmaAutorizada4, Solicitudes.firmaAutorizada5, Solicitudes.testigo1, Solicitudes.testigo2, Solicitudes.monto, Solicitudes.plazo, Solicitudes.Descripciondegarantias, "
                + " Solicitudes.statusID, Solicitudes.ejido ";
                if (this.IsSistemBanco)
                    comm.CommandText += ", Solicitudes.aportaciondelproductor ";
                comm.CommandText += " FROM Solicitudes INNER JOIN Creditos ON Solicitudes.creditoID = Creditos.creditoID "
                + " WHERE (Solicitudes.solicitudID = @solicitudID)";

                if (this.IsSistemBanco)
                {
                    comm.CommandText = comm.CommandText.Replace(" Creditos ", " CreditosSisBancos ");
                    comm.CommandText = comm.CommandText.Replace(" Creditos.", " CreditosSisBancos.");
                    comm.CommandText = comm.CommandText.Replace(" Solicitudes ", " SolicitudesSisBancos ");
                    comm.CommandText = comm.CommandText.Replace("Solicitudes.", "SolicitudesSisBancos.");
                }

                comm.Parameters.Add("@solicitudID", SqlDbType.Int).Value = int.Parse(this.txtSolID.Text.ToString());
                SqlDataReader rd = comm.ExecuteReader();
                if (rd.HasRows && rd.Read())
                {
                    this.ddlCiclo.DataBind();
                    this.ddlCiclo.SelectedValue = rd["cicloID"].ToString();
                    this.ObtenMontoPorHectareaCredito();
                    this.txtFecha.Text = DateTime.Parse(rd["fecha"].ToString()).ToString("dd/MM/yyyy");
                    this.txtInteres.Text = (((double)rd["Interesanual"]) * 100).ToString("N2");
                    this.cmbProductores.DataBind();
                    this.cmbProductores.SelectedValue = rd["productorID"].ToString();
                    this.cargadatosProductor((int)rd["productorID"]);
                    this.txtExperiencia.Text = ((int)rd["Experiencia"]).ToString();
                    if (rd["otrosPasivosMonto"] != null && rd["otrosPasivosMonto"].ToString().Length > 0)
                    {
                        this.txtOtrosPasivosMonto.Text = ((double)rd["otrosPasivosMonto"]).ToString("N2");
                    }
                    this.txtOtrosPasivosAquienLeDebe.Text = rd["otrosPasivosAQuienLeDebe"].ToString();
                    this.txtSuperficieFinanciada.Text = ((double)rd["superficieFinanciada"]).ToString("N2");
                    this.txtSuperifieASembrar.Text = ((double)rd["Superficieasembrar"]).ToString("N2");
                    this.txtActualActivosCasaHabitacion.Text = ((int)rd["casaHabitacion"]).ToString();
                    this.txtActualActivosRastra.Text = ((int)rd["rastra"]).ToString();
                    this.txtActualActivosArado.Text = ((int)rd["Arado"]).ToString();
                    this.txtActualActivosCultivadora.Text = ((int)rd["Cultivadora"]).ToString();
                    this.txtActualActivosSubsuelo.Text = ((int)rd["Subsuelo"]).ToString();
                    this.txtActualActivosTractor.Text = ((int)rd["tractor"]).ToString();
                    this.txtActualActivosSembradora.Text = ((int)rd["sembradora"]).ToString();
                    this.txtActualActivosCamioneta.Text = ((int)rd["camioneta"]).ToString();
                    this.txtActualActivosOtrosActivos.Text = ((double)rd["otrosActivos"]).ToString("N2");
                    this.txtDatosGarantiaGarantiaLiquida.Text = ((double)rd["garantiaLiquida"]).ToString("N2");
                    this.txtDatosGarantiaConcepto.Text = rd["ConceptoSoporteGarantia"].ToString();
                    this.txtDatosGarantiaMontoSoporte.Text = ((double)rd["montoSoporteGarantia"]).ToString("N2");
                    this.txtDatosGarantiaDomicilioDeposito.Text = rd["domicilioDelDeposito"].ToString();
                    this.txtAval1.Text = rd["aval1"].ToString(); this.txtDomAval1.Text = rd["Aval1Dom"].ToString();
                    this.txtAval2.Text = rd["aval2"].ToString(); this.txtDomAval2.Text = rd["Aval2Dom"].ToString();
                    this.txtAutorizado1.Text = rd["firmaAutorizada1"].ToString();
                    this.txtAutorizado2.Text = rd["firmaAutorizada2"].ToString();
                    this.txtAutorizado3.Text = rd["firmaAutorizada3"].ToString();
                    this.txtAutorizado4.Text = rd["firmaAutorizada4"].ToString();
                    this.txtAutorizado5.Text = rd["firmaAutorizada5"].ToString();
                    this.txtTestigo1.Text = rd["testigo1"].ToString();
                    this.txtTestigo2.Text = rd["testigo2"].ToString();
                    this.txtMontoDelCredito.Text = ((double)rd["Monto"]).ToString("C2");
                    this.ddlEstadoSolicitud.DataBind();
                    this.ddlEstadoSolicitud.SelectedValue = rd["statusID"].ToString();
                    this.textBoxEjido.Text = rd["ejido"].ToString();
                    if(this.IsSistemBanco)
                        this.txtAportacionProductor.Text = ((double)rd["aportaciondelproductor"]).ToString("C2");
                    if (rd["Descripciondegarantias"] != null)
                    {
                        this.txtDescGarantias.Text = rd["Descripciondegarantias"].ToString();
                    }
                    if (((int)rd["Plazo"]) <= 0)
                    {
                        this.CalculaPlazo();
                    }
                    else
                    {
                        this.txtPlazo.Text = ((int)rd["Plazo"]).ToString();
                    }
                    this.UpdatePrintLinks();
                    this.CalculaValorEstimadoActivos();
                    this.btnNewSolicitud.Visible = false;
                    this.pnlSolicitudesData.Visible = true;
                }
            }
            catch (System.Exception ex)
            {
            	Logger.Instance.LogException(Logger.typeUserActions.SELECT, "cargando datos de solicitud", ref ex);
            }
            finally
            {
                conn.Close();
            }
        }

        internal void CalculaPlazo()
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT fechaFinZona1 FROM Ciclos WHERE (cicloID = @cicloID)";
            comm.Parameters.Add("@cicloID", SqlDbType.Int).Value = this.ddlCiclo.SelectedValue;
            SqlDataReader r = dbFunctions.ExecuteReader(comm);
            if (r != null && r.HasRows && r.Read())
            {
                DateDifference dateDifference = new DateDifference((DateTime)r["fechaFinZona1"], DateTime.Parse(this.txtFecha.Text));
                int totalmonths = (dateDifference.Years * 12) + dateDifference.Months;
                this.txtPlazo.Text = totalmonths.ToString();
                if (comm.Connection != null)
                {
                    comm.Connection.Close();
                }
            }
        }


        protected void btnNewSolicitud_Click(object sender, EventArgs e)
        {
            bool bRedirect = false;
            string sRedirect = string.Empty;
            try
            {
                double dTasaInteres = 0;
                int iExperiencia = 0;
                if (!double.TryParse(this.txtInteres.Text, out dTasaInteres))
                {
                    dTasaInteres = 0;
                }
                if (!int.TryParse(this.txtExperiencia.Text, out iExperiencia))
                {
                    iExperiencia = 0;
                }
                int iCreditoID =-1;
                if (!this.chkAddToACredito.Checked)
                {
                    iCreditoID= dbFunctions.insertaCredito(int.Parse(this.cmbProductores.SelectedValue),
                    int.Parse(this.ddlCiclo.SelectedValue),
                    DateTime.Parse(this.txtFecha.Text),
                    1,
                    dTasaInteres,
                    this.UserID,
                    0,
                    0);
                }
                else
                {
                    int.TryParse(this.ddlAddToACredito.SelectedValue, out iCreditoID);
                }
                  
                int iSolicitudID = -1;
                if (iCreditoID > 0 && 
                    dbFunctions.insertsol(this.txtFecha.Text,
                        iCreditoID,
                        int.Parse(this.cmbProductores.SelectedValue),
                        iExperiencia, 
                        this.UserID,
                        ref iSolicitudID, this.textBoxEjido.Text)
                    )
                {
                    this.txtSolID.Text = iSolicitudID.ToString();
                    this.UpdateSolicitudStatus();
                    bRedirect = true;
                    String sNewUrl = "~/frmAddSolicitud2010.aspx?data=";
                    sNewUrl += Utils.encriptacadena("SolID=" + iSolicitudID.ToString());
                    sRedirect = sNewUrl;
                }
                
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.INSERT, "agregando nueva solicitud", ref ex);
            }
            if (bRedirect)
            {
                Response.Redirect(sRedirect);
            }
        }

        internal void UpdateSolicitudStatus()
        {
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "UPDATE Solicitudes SET statusID = @statusID "
                + " WHERE (solicitudID = @solicitudID)";
                if (this.IsSistemBanco)
                {
                    comm.CommandText = comm.CommandText.Replace(" Creditos ", " CreditosSisBancos ");
                    comm.CommandText = comm.CommandText.Replace(" Creditos.", " CreditosSisBancos.");
                    comm.CommandText = comm.CommandText.Replace(" Solicitudes ", " SolicitudesSisBancos ");
                    comm.CommandText = comm.CommandText.Replace(" Solicitudes.", " SolicitudesSisBancos.");
                }
                comm.Parameters.Add("@statusID", SqlDbType.Int).Value = this.ddlEstadoSolicitud.SelectedValue;
                comm.Parameters.Add("@solicitudID", SqlDbType.Int).Value = this.txtSolID.Text;


                dbFunctions.ExecuteCommand(comm);
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.UPDATE, "UpdateSolicitudStatus", ref ex);
            }
            finally
            {
                if (comm.Connection != null)
                {
                    comm.Connection.Close();
                }
            }
        }

        protected void ddlCiclo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.CalculaMontoDelCredito();
            }
            catch (System.Exception ex)
            {
                Logger.Instance.LogException(Logger.typeUserActions.SELECT, "obteniendo monto por hectarea", ref ex);
            }

        }
        internal void ObtenMontoPorHectareaCredito()
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT Montoporhectarea, fechaFinZona1 FROM Ciclos WHERE (cicloID = @cicloID)";
            comm.Parameters.Add("@cicloID", SqlDbType.Int).Value = this.ddlCiclo.SelectedValue;

            double dMontoPorHectarea = 0;
            dMontoPorHectarea = dbFunctions.GetExecuteDoubleScalar(comm, 0.0);
            this.txtMontoPorHectarea.Text = dMontoPorHectarea.ToString("N2");
        }
        internal void CalculaMontoDelCredito()
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT Montoporhectarea,fechaFinZona1 FROM Ciclos WHERE (cicloID = @cicloID)";
            comm.Parameters.Add("@cicloID", SqlDbType.Int).Value = this.ddlCiclo.SelectedValue;
            SqlDataReader r = dbFunctions.ExecuteReader(comm);
            if (r != null && r.HasRows && r.Read())
            {
                double dMontoPorHectarea = 0;
                dMontoPorHectarea = (double)r["Montoporhectarea"];
                this.txtMontoPorHectarea.Text = dMontoPorHectarea.ToString("N2");

                double dMontoCredito = 0;
                dMontoCredito = Utils.GetSafeFloat(this.txtSuperficieFinanciada.Text) * dMontoPorHectarea;
                this.txtMontoDelCredito.Text = dMontoCredito.ToString("N2");

                


                //saca plazo
                //this.txtFechaLimite.Text = ((DateTime)(rd["fechaFinZona1"])).ToString("dd/MM/yyyy");
                DateDifference dateDifference = new DateDifference((DateTime)r["fechaFinZona1"], DateTime.Parse(this.txtFecha.Text));
                int totalmonths = (dateDifference.Years * 12) + dateDifference.Months;
                this.txtPlazo.Text = totalmonths.ToString();

                if (comm.Connection != null)
                {
                    comm.Connection.Close();
                }

            }
        }

        protected void txtMontoPorHectarea_TextChanged(object sender, EventArgs e)
        {
            this.CalculaMontoDelCredito();
        }

        protected void txtSuperficieFinanciada_TextChanged(object sender, EventArgs e)
        {
            this.CalculaMontoDelCredito();
        }

        protected void txtSuperifieASembrar_TextChanged(object sender, EventArgs e)
        {

        }

        internal void CalculaValorEstimadoActivos()
        {
            double dEstimadoActivos = 0;

            dEstimadoActivos += Utils.GetSafeInt(this.txtSuperifieASembrar) * 50000;
            dEstimadoActivos += Utils.GetSafeInt(this.txtActualActivosCasaHabitacion) * 65000;
            dEstimadoActivos += Utils.GetSafeInt(this.txtActualActivosRastra) * 15000;
            dEstimadoActivos += Utils.GetSafeInt(this.txtActualActivosArado) * 10000;
            dEstimadoActivos += Utils.GetSafeInt(this.txtActualActivosCultivadora) * 6500;
            dEstimadoActivos += Utils.GetSafeInt(this.txtActualActivosSubsuelo) * 8000;
            dEstimadoActivos += Utils.GetSafeInt(this.txtActualActivosTractor) * 100000;
            dEstimadoActivos += Utils.GetSafeInt(this.txtActualActivosSembradora) * 25000;
            dEstimadoActivos += Utils.GetSafeInt(this.txtActualActivosCamioneta) * 50000;
            dEstimadoActivos += Utils.GetSafeFloat(this.txtActualActivosOtrosActivos.Text);
            this.txtActualActivosTotalActivos.Text = dEstimadoActivos.ToString("C2");
        }

        protected void btnUpdateTotalActivos_Click(object sender, EventArgs e)
        {
            this.CalculaValorEstimadoActivos();
        }

        protected void txtHasAseguradas_TextChanged(object sender, EventArgs e)
        {
            this.CalculaCostoSeguro();
        }

        protected void gridViewSegurosAgricolas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //TODO: Change link
            HyperLink lnk = (HyperLink)e.Row.FindControl("HyperLink1");
            if (lnk != null)
            {
                string queryStr = "printSeguro=1&filename=Seguro.pdf&solSeguroID=" + this.gridViewSegurosAgricolas.DataKeys[e.Row.RowIndex]["sol_sa_ID"].ToString();
                JSUtils.OpenNewWindowOnClick(ref lnk, "frmDescargaTmpFile.aspx" + Utils.GetEncriptedQueryString(queryStr), "IMPRIMIR SEGURO", true);
            }
        }
        internal void UpdateAddModifySegurosBtns()
        {
            this.btnAddSeguro.Visible = !(this.gridViewSegurosAgricolas.SelectedIndex > -1);
            this.btnModificarSeguro.Visible = (this.gridViewSegurosAgricolas.SelectedIndex > -1);
            this.pnlAddPredioSeguros.Visible = this.gridViewSegurosAgricolas.SelectedIndex > -1;
        }
        protected void gridViewSegurosAgricolas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection conGaribay = new SqlConnection(myConfig.ConnectionInfo);
            try
            {
                conGaribay.Open();
                SqlCommand cmdGaribay = new SqlCommand();
                cmdGaribay.Connection = conGaribay;
                cmdGaribay.CommandText = "DELETE FROM solicitud_SeguroAgricola WHERE sol_sa_ID = @sol_sa_ID;";
                if (this.IsSistemBanco)
                {
                    cmdGaribay.CommandText = cmdGaribay.CommandText.Replace(" Creditos ", " CreditosSisBancos ");
                    cmdGaribay.CommandText = cmdGaribay.CommandText.Replace(" Creditos.", " CreditosSisBancos.");
                    cmdGaribay.CommandText = cmdGaribay.CommandText.Replace(" Solicitudes ", " SolicitudesSisBancos ");
                    cmdGaribay.CommandText = cmdGaribay.CommandText.Replace(" Solicitudes.", " SolicitudesSisBancos.");
                    cmdGaribay.CommandText = cmdGaribay.CommandText.Replace(" solicitud_SeguroAgricola ", " SolicitudesSisBancos_SeguroAgricola ");
                }
                cmdGaribay.Parameters.Add("@sol_sa_ID", SqlDbType.Int).Value = this.gridViewSegurosAgricolas.DataKeys[e.RowIndex]["sol_sa_ID"].ToString();
                cmdGaribay.ExecuteNonQuery();
                this.pnlSeguroResult.Visible = true;
                this.lblSeguroResult.Text = "SEGURO ELIMINADO EXITOSAMENTE";
                this.imgBienSeguroResult.Visible = true;
                this.imgMalSeguroResult.Visible = false;

            }
            catch (System.Exception ex)
            {
                this.pnlSeguroResult.Visible = true;
                this.lblSeguroResult.Text = "NO SE PUDO ELIMINAR EL SEGURO, SE HA GUARDADO UN REGISTRO DEL ERROR.";
                this.imgBienSeguroResult.Visible = true;
                this.imgMalSeguroResult.Visible = false;
            }
            finally
            {
                conGaribay.Close();

//                 this.calculaLimiteDeCredito();
//                 this.calculaRecComplementatios();
                this.pnlAddPredioSeguros.Visible = false;
                this.gridViewSegurosAgricolas.SelectedIndex = -1;
                this.gridViewSegurosAgricolas.DataBind();
                this.gridViewPrediosSeguro.DataBind();
                this.UpdateAddModifySegurosBtns();
            }
        }
        protected void gridViewSegurosAgricolas_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pnlAddPredioSeguros.Visible = (this.gridViewSegurosAgricolas.SelectedIndex > -1);
            this.lblNumSol_Seguro.Text = this.gridViewSegurosAgricolas.SelectedDataKey["sol_sa_ID"].ToString();
            this.UpdateAddModifySegurosBtns();
            this.LoadSelectedSeguroData();
            this.LoadSelectedGridSeguroData();
        }

        internal void LoadSelectedGridSeguroData()
        {
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.CommandText = "SELECT sol_sa_ID, seguroID, hectAseguradas, CostoTotalSeguro "
                    + " FROM solicitud_SeguroAgricola WHERE (sol_sa_ID = @sol_sa_ID)";

                if (this.IsSistemBanco)
                {
                    comm.CommandText = comm.CommandText.Replace(" Creditos ", " CreditosSisBancos ");
                    comm.CommandText = comm.CommandText.Replace(" Creditos.", " CreditosSisBancos.");
                    comm.CommandText = comm.CommandText.Replace(" Solicitudes ", " SolicitudesSisBancos ");
                    comm.CommandText = comm.CommandText.Replace(" Solicitudes.", " SolicitudesSisBancos.");
                    comm.CommandText = comm.CommandText.Replace(" solicitud_SeguroAgricola ", " SolicitudesSisBancos_SeguroAgricola ");
                }

                comm.Parameters.Add("@sol_sa_ID", SqlDbType.Int).Value = this.gridViewSegurosAgricolas.SelectedDataKey["sol_sa_ID"].ToString();
                SqlDataReader rd = dbFunctions.ExecuteReader(comm);
                if (rd != null && rd.HasRows && rd.Read())
                {
                    this.drpdlTipoSeguro.DataBind();
                    this.drpdlTipoSeguro.SelectedValue = rd["seguroID"].ToString();
                    this.drpdlTipoSeguro_SelectedIndexChanged(null, null);
                    this.txtHasAseguradas.Text = rd["hectAseguradas"].ToString();
                    this.txtCostoTotalSeguro.Text = rd["CostoTotalSeguro"].ToString();
                }
                if (comm.Connection != null)
                {
                    comm.Connection.Close();
                }
            }
            catch (System.Exception ex)
            {
            	Logger.Instance.LogException(Logger.typeUserActions.SELECT,"error obteniendo datos del seguro para modificar", ref ex);
            }
        }

    }
}
