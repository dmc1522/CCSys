<%@ Page Title="" Language="C#" Theme="skinverde" MasterPageFile="~/MasterPage.Master" AutoEventWireup="True" CodeBehind="frmAddSolicitud2010.aspx.cs" Inherits="Garibay.frmAddSolicitud2010" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            height: 37px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
	<tr>
		<td class="TableHeader">AGREGAR UNA NUEVA SOLICITUD DE CRÉDITO</td>
	</tr>
</table>
<asp:UpdatePanel runat="Server" ID="pnlAllSolicitud">
<ContentTemplate>

    <asp:Panel ID="pnlCredito" runat="server" GroupingText="Datos Generales">
        <table>
            <tr>
                <td class="TablaField">
                    Ciclo:</td>
                <td>
                    <asp:DropDownList ID="ddlCiclo" runat="server" 
                        DataSourceID="dataSourceCiclo" DataTextField="CicloName" 
                        DataValueField="cicloID" Height="22px" 
                        onselectedindexchanged="ddlCiclo_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="dataSourceCiclo" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT cicloID, CicloName FROM Ciclos WHERE cerrado=@cerrado ORDER BY CicloName DESC">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="FALSE" Name="cerrado" Type="Boolean" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td class="TablaField">
                    Fecha:</td>
                <td>
                    <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
                    <rjs:PopCalendar ID="PopCalendar3" runat="server" AutoPostBack="False" 
                        Control="txtFecha" 
                        Separator="/" />
                </td>
                <td class="TableField">
                    Solicitud No.
                </td>
                <td>
                    <asp:TextBox ID="txtSolID" runat="Server" Font-Bold="True" Font-Size="X-Large" 
                        ReadOnly="True" Width="103px">Nueva</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Interes Anual:</td>
                <td class="style4">
                    <asp:TextBox ID="txtInteres" runat="server" Width="50px">24</asp:TextBox>
                    %</td>
                <td class="style4">
                    Estado de la solicitud:</td>
                <td class="style4">
                    <asp:DropDownList ID="ddlEstadoSolicitud" runat="server" 
                        DataSourceID="sdsEstadoSolicitud" DataTextField="status" 
                        DataValueField="statusID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsEstadoSolicitud" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT [statusID], [status] FROM [SolicitudStatus] ORDER BY [statusID]">
                    </asp:SqlDataSource>
                </td>
                <td class="style4">
                    </td>
                <td class="style4">
                    </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:CheckBox ID="chkAddToACredito" runat="server" 
                        Text="Agregar a credito a existente" />
                    <asp:Panel ID="pnlAddToACredito" runat="server">
                        <asp:DropDownList ID="ddlAddToACredito" runat="server" 
                            DataSourceID="sdsCreditos" DataTextField="name" DataValueField="creditoID">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsCreditos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            SelectCommand="SELECT Creditos.creditoID, Productores.apaterno + SPACE(1) + Productores.amaterno + SPACE(1) + Productores.nombre  + ' - ' + Cast(Creditos.creditoID as VARCHAR) AS name FROM Productores INNER JOIN Creditos ON Productores.productorID = Creditos.productorID order by name">
                        </asp:SqlDataSource>
                    </asp:Panel>
                    <cc1:CollapsiblePanelExtender ID="pnlAddToACredito_CollapsiblePanelExtender" 
                        runat="server" CollapseControlID="chkAddToACredito" Enabled="True" 
                        ExpandControlID="chkAddToACredito" TargetControlID="pnlAddToACredito">
                    </cc1:CollapsiblePanelExtender>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:UpdateProgress runat="Server" ID="progressArribaProductor" 
        AssociatedUpdatePanelID="pnlAllSolicitud" DisplayAfter="0">
    <ProgressTemplate>
        <img src="imagenes/cargando.gif" alt="Cargando..." />CARGANDO DATOS...
    </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel ID="pnlProductor" runat="server" GroupingText="PRODUCTOR:" >
        <table>
            <tr>
                <td class="TablaField">
                    Productor:</td>
                <td>
                    <asp:DropDownList ID="cmbProductores" runat="server" AutoPostBack="True" 
                        DataSourceID="SqlDataSource4" DataTextField="name" DataValueField="productorID" 
                        onselectedindexchanged="cmbProductores_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
                        
                        SelectCommand="SELECT productorID, LTRIM(apaterno + ' ' + amaterno + ' ' +  nombre) as name FROM Productores order by name">
                    </asp:SqlDataSource>
                </td>
                <td class="TablaField">
                    Teléfono:
                </td>
                <td>
                    <asp:TextBox ID="txtTelefono" runat="server"  ReadOnly="True" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Domicilio:</td>
                <td>
                    <asp:TextBox ID="txtDomicilio" runat="server" ReadOnly="True" Height="53px" 
                        TextMode="MultiLine" Width="300px"></asp:TextBox>
                </td>
                <td class="TablaField">
                    Celular:</td>
                <td>
                    <asp:TextBox ID="txtCelular" runat="server"  ReadOnly="True" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Colonia:</td>
                <td>
                    <asp:TextBox ID="txtColonia" runat="server" ReadOnly="True" Width="300px"></asp:TextBox>
                </td>
                <td class="TablaField">
                    Sexo:</td>
                <td>
                    <asp:DropDownList ID="cmbSexo0" runat="server" DataSourceID="SqlDataSource5" 
                        DataTextField="sexo" DataValueField="sexoID" Enabled="False" Height="22px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
                        SelectCommand="SELECT [sexoID], [sexo] FROM [Sexo]"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Población:</td>
                <td>
                    <asp:TextBox ID="txtPoblacion" runat="server" ReadOnly="True" Width="300px"></asp:TextBox>
                </td>
                <td class="TablaField">
                    Fax:</td>
                <td>
                    <asp:TextBox ID="txtFax" runat="server" ReadOnly="True" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    IFE:</td>
                <td>
                    <asp:TextBox ID="txtIfe" runat="server" ReadOnly="True" Width="300px"></asp:TextBox>
                </td>
                <td class="TablaField">
                    Teléfono trabajo:</td>
                <td>
                    <asp:TextBox ID="txtTelefonotrabajo" runat="server"  
                        ReadOnly="True" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    CURP:</td>
                <td>
                    <asp:TextBox ID="txtCurp" runat="server"  ReadOnly="True" Width="300px"></asp:TextBox>
                </td>
                <td class="TablaField">
                    Código postal:</td>
                <td>
                    <asp:TextBox ID="txtCodigopostal" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    RFC:</td>
                <td>
                    <asp:TextBox ID="txtRfc" runat="server"  ReadOnly="True" Width="300px"></asp:TextBox>
                </td>
                <td class="TablaField">
                    Estado Civil:</td>
                <td>
                    <asp:DropDownList ID="cmbEstadoCivil" runat="server" 
                        DataSourceID="SqlDataSource3" DataTextField="EstadoCivil" 
                        DataValueField="estadoCivilID" Enabled="False" Height="22px" >
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
                        
                        
                        SelectCommand="SELECT [estadoCivilID], [EstadoCivil] FROM [EstadosCiviles]">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Municipio:</td>
                <td>
                    <asp:TextBox ID="txtMunicipio" runat="server"  ReadOnly="True" Width="300px"></asp:TextBox>
                </td>
                <td class="TablaField">
                    Régimen:</td>
                <td>
                    <asp:DropDownList ID="cmbRegimen" runat="server" DataSourceID="SqlDataSource2" 
                        DataTextField="Regimen" DataValueField="regimenID" Enabled="False" 
                        Height="22px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
                        SelectCommand="SELECT [regimenID], [Regimen] FROM [Regimenes]">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Estado:</td>
                <td>
                    <asp:DropDownList ID="cmbEstado" runat="server" DataSourceID="SqlDataSource1" 
                        DataTextField="estado" DataValueField="estadoID" Enabled="False" >
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
                        SelectCommand="SELECT [estadoID], [estado] FROM [Estados]">
                    </asp:SqlDataSource>
                </td>
                <td class="TablaField">
                    Conyugue:</td>
                <td>
                    <asp:TextBox ID="txtConyugue" runat="server" ReadOnly="True" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Correo electrónico:</td>
                <td>
                    <asp:TextBox ID="txtCorreo" runat="server" ReadOnly="True" Width="300px"></asp:TextBox>
                </td>
                <td class="TablaField">
                    Experiencia:</td>
                <td>
                    <asp:TextBox ID="txtExperiencia" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Ejido:</td>
                <td>
                    <asp:TextBox ID="textBoxEjido" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="btnNewSolicitud" runat="server" Text="Crear nueva solicitud" 
                        onclick="btnNewSolicitud_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
<br />
    <asp:Panel runat="Server" id="pnlSolicitudesData" Visible="false">
        <asp:Panel ID="pnlOtrosPasivos" runat="Server" GroupingText="OTROS PASIVOS">
        <table>
	        <tr>
	            <td class="TablaField">MONTO:</td><td class="TablaField">A QUIEN LE DEBE Y QUE CONCEPTO:</td>
	        </tr>
	        <tr>
	            <td>
                    <asp:TextBox ID="txtOtrosPasivosMonto" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtOtrosPasivosAquienLeDebe" runat="server" Width="600px"></asp:TextBox>
                </td>
	        </tr>
        </table>
        </asp:Panel>
        <br />
        <asp:UpdateProgress ID="progressArribaProductor0" runat="Server" 
            AssociatedUpdatePanelID="pnlAllSolicitud" DisplayAfter="0">
            <ProgressTemplate>
                <img alt="Cargando..." src="imagenes/cargando.gif" />PROCESANDO DATOS...
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:Panel runat="server" ID="pnlSuperficie" GroupingText="SUPERFICIE">
            <table>
    	        <tr>
    	            <td class="TablaField">PLAZO</td>
    	            <td class="TablaField">
                        SUPERFICIE FINANCIADA (Has)</td>
    	            <td class="TablaField">SUPERFICIE TOTAL A SEMBRAR (Has)</td>
    	            <td class="TablaField">
                        MONTO POR HECTAREA</td>
                    <td class="TablaField">
                        MONTO DEL CREDITO</td>
    	        </tr>
    	        <tr>
    	            <td>
                        <asp:TextBox ID="txtPlazo" runat="server" Width="50px"></asp:TextBox>Meses
                    </td>
    	            <td>
                        <asp:TextBox ID="txtSuperficieFinanciada" runat="server" AutoPostBack="True" 
                            ontextchanged="txtSuperficieFinanciada_TextChanged"></asp:TextBox>
                    </td>
    	            <td><asp:TextBox ID="txtSuperifieASembrar" runat="server"></asp:TextBox></td>
    	            <td>
                        <asp:TextBox ID="txtMontoPorHectarea" runat="server" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMontoDelCredito" runat="server" ReadOnly="True"></asp:TextBox>
                    </td>
    	        </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAportacionProductor" runat="server" 
                            Text="Aportacion del productor:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAportacionProductor" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <asp:Panel runat="server" ID="pnlValorActualActivos" 
                GroupingText="VALOR ESTIMADO ACTUAL DE ACTIVOS">
            <table>
    	        <tr>
    	            <td class="TablaField">CASA HABITACION (UNIDADES):</td>
    	            <td>
                        <asp:TextBox ID="txtActualActivosCasaHabitacion" runat="server" Width="50px"></asp:TextBox></td>
    	            <td class="TablaField">RASTRA (UNIDADES):</td>
    	            <td>
                        <asp:TextBox ID="txtActualActivosRastra" runat="server" Width="50px"></asp:TextBox></td>
    	        </tr>
    	        <tr>
    	            <td class="TablaField">ARADO (UNIDADES):</td>
    	            <td>
                        <asp:TextBox ID="txtActualActivosArado" runat="server" Width="50px"></asp:TextBox></td>
    	            <td class="TablaField">CULTIVADORA (UNIDADES):</td>
    	            <td>
                        <asp:TextBox ID="txtActualActivosCultivadora" runat="server" Width="50px"></asp:TextBox></td>
    	        </tr>
    	        <tr>
    	            <td class="TablaField">SUBSUELO (UNIDADES):</td>
    	            <td>
                        <asp:TextBox ID="txtActualActivosSubsuelo" runat="server" Width="50px"></asp:TextBox></td>
       	            <td class="TablaField">TRACTOR (UNIDADES):</td>
    	            <td>
                        <asp:TextBox ID="txtActualActivosTractor" runat="server" Width="50px"></asp:TextBox></td>
    	        </tr>
    	        <tr>
    	            <td class="TablaField">SEMBRADORA (UNIDADES):</td>
    	            <td>
                        <asp:TextBox ID="txtActualActivosSembradora" runat="server" Width="50px"></asp:TextBox></td>
    	            <td class="TablaField">CAMIONETA (UNIDADES):</td>
    	            <td>
                        <asp:TextBox ID="txtActualActivosCamioneta" runat="server" Width="50px"></asp:TextBox></td>
    	        </tr>
    	        <tr>
    	            <td class="TablaField">OTROS ACTIVOS ($):</td>
    	            <td colspan="3">
                        <asp:TextBox ID="txtActualActivosOtrosActivos" runat="server" Width="100px"></asp:TextBox>
                    </td>
    	        </tr>
                <tr>
                    <td class="TablaField">
                        TOTAL DE ACTIVOS:</td>
                    <td colspan="3">
                        <asp:TextBox ID="txtActualActivosTotalActivos" runat="server" ReadOnly="True" 
                            Width="150px"></asp:TextBox>
                        <asp:Button ID="btnUpdateTotalActivos" runat="server" 
                            onclick="btnUpdateTotalActivos_Click" Text="Calcula Total de Activos" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="Server" 
            AssociatedUpdatePanelID="pnlAllSolicitud" DisplayAfter="0">
            <ProgressTemplate>
                <img alt="Cargando..." src="imagenes/cargando.gif" />PROCESANDO DATOS...
            </ProgressTemplate>
        </asp:UpdateProgress>
        <br />
        <asp:Panel runat="Server" ID="pnlDatosDeGarantia" GroupingText="DATOS DE GARANTIAS">
            <table>
    	        <tr>
    	            <td class="TablaField">DESCRIPCION DE LAS GARANTIAS:</td>
    	            <td>
                        <asp:TextBox ID="txtDescGarantias" runat="server" Height="89px" 
                            TextMode="MultiLine" Width="400px"></asp:TextBox>
                    </td>
    	        </tr>
    	        <tr>
                    <td class="TablaField">
                        GARANTIA LIQUIDA ($):</td>
                    <td>
                        <asp:TextBox ID="txtDatosGarantiaGarantiaLiquida" runat="server"></asp:TextBox>
                    </td>
                </tr>
    	        <tr>
    	            <td class="TablaField">CONCEPTO SOPORTE GARANTIA:</td>
    	            <td>
                        <asp:TextBox ID="txtDatosGarantiaConcepto" runat="server" Width="400px"></asp:TextBox></td>
    	        </tr>
    	        <tr>
    	            <td class="TablaField">MONTO SOPORTE GARANTIA ($):</td>
    	            <td>
                        <asp:TextBox ID="txtDatosGarantiaMontoSoporte" runat="server"></asp:TextBox></td>
    	        </tr>
    	        <tr>
    	            <td class="TablaField">DOMICILIO DEL DEPOSITO:</td>
    	            <td>
                        <asp:TextBox ID="txtDatosGarantiaDomicilioDeposito" runat="server" 
                            Width="400px"></asp:TextBox></td>
    	        </tr>
            </table>
        </asp:Panel>
        <br />
        <asp:Panel ID="pnlDatosAvales" runat="server" GroupingText="DATOS DE LOS AVALES">
            <table>        
                <tr>
                    <td class="TablaField">
                        AVAL 1:</td>
                    <td>
                        <asp:TextBox ID="txtAval1" runat="server" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TablaField">
                        DOMICILIO AVAL 1:</td>
                    <td>
                        <asp:TextBox ID="txtDomAval1" runat="server" Width="500px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TablaField">
                        AVAL 2:</td>
                    <td>
                        <asp:TextBox ID="txtAval2" runat="server" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TablaField">
                        DOMICILIO AVAL 2:</td>
                    <td>
                        <asp:TextBox ID="txtDomAval2" runat="server" Width="500px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <asp:Panel runat="Server" ID="pnlFirmasAutorizadas" 
                GroupingText="DATOS DE LOS AUTORIZADOS A RECOGER AGROINSUMOS:">
            <table>
                <tr>
                    <td class="TablaField">AUTORIZADO 1:</td>
                    <td><asp:TextBox ID="txtAutorizado1" runat="server" Width="400px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="TablaField">AUTORIZADO 2:</td>
                    <td><asp:TextBox ID="txtAutorizado2" runat="server" Width="400px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="TablaField">AUTORIZADO 3:</td>
                    <td><asp:TextBox ID="txtAutorizado3" runat="server" Width="400px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="TablaField">AUTORIZADO 4:</td>
                    <td><asp:TextBox ID="txtAutorizado4" runat="server" Width="400px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="TablaField">AUTORIZADO 5:</td>
                    <td><asp:TextBox ID="txtAutorizado5" runat="server" Width="400px"></asp:TextBox></td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <asp:Panel runat="Server" id="pnlDatosTestigos" 
                GroupingText="DATOS DE LOS TESTIGOS">
            <table>
                <tr>
                    <td class="TablaField">TESTIGO 1:</td>
                    <td><asp:TextBox ID="txtTestigo1" runat="server" Width="500px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="TablaField">TESTIGO 2:</td>
                    <td><asp:TextBox ID="txtTestigo2" runat="server" Width="500px"></asp:TextBox></td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <asp:Panel ID="pnlSeguroResult" runat="server" Visible="False">
            <asp:Image ID="imgBienSeguroResult" runat="server" 
                ImageUrl="~/imagenes/palomita.jpg" />
            <asp:Image ID="imgMalSeguroResult" runat="server" 
                ImageUrl="~/imagenes/tache.jpg" />
            <asp:Label ID="lblSeguroResult" runat="server"></asp:Label>
        </asp:Panel>
        <br />
            <asp:Panel ID="pnlSeguro" runat="server" GroupingText="SEGURO AGRICOLA">
                <asp:GridView ID="gridViewSegurosAgricolas" runat="server" 
                    AutoGenerateColumns="False" DataKeyNames="sol_sa_ID" 
                    DataSourceID="sdsSegurosAgregados" 
                    onrowdatabound="gridViewSegurosAgricolas_RowDataBound" 
                    onrowdeleting="gridViewSegurosAgricolas_RowDeleting" 
                    onselectedindexchanged="gridViewSegurosAgricolas_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField ButtonType="Button" DeleteText="Eliminar" 
                            EditText="Actualizar" SelectText="Modificar" ShowDeleteButton="True" 
                            ShowSelectButton="True" UpdateText="Modificar" />
                        <asp:BoundField DataField="Nombre" HeaderText="Seguro" 
                            SortExpression="Nombre" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion del Seguro" 
                            SortExpression="Descripcion" />
                        <asp:BoundField DataField="hectAseguradas" DataFormatString="{0:n2}" 
                            HeaderText="Hectáreas aseguradas." SortExpression="hectAseguradas">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CostoPorHectarea" DataFormatString="{0:c2}" 
                            HeaderText="Costo por hectárea" SortExpression="CostoPorHectarea">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CostoTotalSeguro" DataFormatString="{0:c2}" 
                            HeaderText="Costo total de seguro" SortExpression="CostoTotalSeguro">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" 
                                    NavigateUrl='<%# Eval("Nombre") %>'>Imprimir</asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsSegurosAgregados" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    DeleteCommand="select 1;" InsertCommand="select 1;" 
                    SelectCommand="SELECT SegurosAgricolas.Nombre, SegurosAgricolas.Descripcion, solicitud_SeguroAgricola.hectAseguradas, SegurosAgricolas.CostoPorHectarea, solicitud_SeguroAgricola.CostoTotalSeguro, solicitud_SeguroAgricola.sol_sa_ID FROM Solicitudes INNER JOIN solicitud_SeguroAgricola ON Solicitudes.solicitudID = solicitud_SeguroAgricola.solicitudID INNER JOIN SegurosAgricolas ON solicitud_SeguroAgricola.seguroID = SegurosAgricolas.seguroID WHERE (solicitud_SeguroAgricola.solicitudID = @solicitudId)" 
                    UpdateCommand="select 1;">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtSolID" DefaultValue="-1" Name="solicitudId" 
                            PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
                <table>
                    <tr>
                        <td colspan="2">
                            
                            <asp:UpdateProgress ID="UpdateProgress2" runat="Server" 
                                AssociatedUpdatePanelID="pnlAllSolicitud" DisplayAfter="0">
                                <ProgressTemplate>
                                    <img alt="Cargando..." src="imagenes/cargando.gif" />PROCESANDO DATOS...
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                        <td rowspan="4" valign="top">
                            <asp:Panel ID="pnlAddPredioSeguros" runat="Server" Visible="false">
                                <asp:GridView ID="gridViewPrediosSeguro" runat="server" AutoGenerateColumns="False" DataKeyNames="SegAgrPrediosID" DataSourceID="sdsPrediosSeguro" onrowdeleting="gridViewPrediosSeguro_RowDeleting">
                                    <Columns>
                                        <asp:CommandField ButtonType="Button" DeleteText="Eliminar" 
                                            ShowDeleteButton="True" />
                                        <asp:BoundField DataField="SegAgrPrediosID" HeaderText="SegAgrPrediosID" 
                                            InsertVisible="False" ReadOnly="True" SortExpression="SegAgrPrediosID" 
                                            Visible="False" />
                                        <asp:BoundField DataField="Folio" HeaderText="Folio" SortExpression="Folio" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" 
                                            SortExpression="Nombre" />
                                        <asp:BoundField DataField="Superficie" DataFormatString="{0:n2}" 
                                            HeaderText="Superficie" SortExpression="Superficie">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" 
                                            SortExpression="Ubicacion" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdsPrediosSeguro" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    DeleteCommand="select 1" 
                                    SelectCommand="SELECT SegurosAgricolasPredios.SegAgrPrediosID, SegurosAgricolasPredios.Folio, SegurosAgricolasPredios.Nombre, SegurosAgricolasPredios.Superficie, SegurosAgricolasPredios.Ubicacion FROM SegurosAgricolasPredios INNER JOIN solicitud_SeguroAgricola ON SegurosAgricolasPredios.sol_sa_ID = solicitud_SeguroAgricola.sol_sa_ID where   solicitud_SeguroAgricola.sol_sa_ID = @solsaID">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="lblNumSol_Seguro" DefaultValue="" 
                                            Name="solsaID" PropertyName="Text" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <table>
                                    <tr>
                                        <td class="TableHeader" colspan="2">
                                            AGREGAR PREDIO A SEGURO
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableField">
                                            Folio predio:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSeguroFolioPredio" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableField">
                                            Nombre predio:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSeguroNombrePredio" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableField">
                                            Superificie:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSeguroPredioSuperficie" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableField">
                                            Ubicacion:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSeguroPredioUbicacion" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="TableField" colspan="2">
                                            <asp:Button ID="btnAgregarPredioSeguro" runat="server" CausesValidation="False" onclick="btnAgregarPredioSeguro_Click" Text="Agregar Predio" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField" colspan="2">
                            <asp:Label ID="lblNumSol_Seguro" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField">
                            Tipo de seguro:</td>
                        <td>
                            <asp:DropDownList ID="drpdlTipoSeguro" runat="server" AutoPostBack="True" 
                                DataSourceID="sdsTipoSeguro" DataTextField="Descripcion" 
                                DataValueField="seguroID" 
                                onselectedindexchanged="drpdlTipoSeguro_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="sdsTipoSeguro" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                SelectCommand="SELECT seguroID,  Nombre + '-  $  ' +  cast (CostoPorHectarea as nVarChar) + ' - '  + cast(Descripcion as Nvarchar) as Descripcion FROM SegurosAgricolas order by CostoPorHectarea ASC">
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div ID="divdatosSeguro" runat="Server">
                                <table>
                                    <tr>
                                        <td class="TablaField">
                                            Descripción de seguro:</td>
                                        <td>
                                            <asp:TextBox ID="txtDescripcionSeguro" runat="server" ReadOnly="True" 
                                                TextMode="MultiLine" Height="103px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TablaField">
                                            Costo por hectárea:</td>
                                        <td>
                                            <asp:TextBox ID="txtCostoporHectarea" runat="server" ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TablaField">
                                            Has. Aseguradas:</td>
                                        <td>
                                            <asp:TextBox ID="txtHasAseguradas" runat="server" AutoPostBack="True" 
                                                ontextchanged="txtHasAseguradas_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TablaField">
                                            Costo Total de Seguro:</td>
                                        <td>
                                            <asp:TextBox ID="txtCostoTotalSeguro" runat="server" ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TablaField">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Button ID="btnAddSeguro" runat="server" onclick="btnAddSeguro_Click" 
                                                Text="Agregar Seguro" />
                                            <asp:Button ID="btnModificarSeguro" runat="server" 
                                                onclick="btnModificarSeguro_Click" Text="Modificar Seguro" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Panel ID="pnlNewSeguro" runat="server" Visible="False">
                                                <asp:Image ID="imgBien" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                                                <asp:Image ID="imgMal" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                                                <asp:Label ID="lblNewSeguro" runat="server"></asp:Label>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        <br />
        <br />
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
	        <tr>
		        <td align="center">
		            <asp:Panel ID="pnlBotones" runat="server">
                        <asp:Panel ID="pnlSolicitudResult" runat="server" Visible="False">
                            <table border="0" style="border-spacing: 15px; border-collapse: separate;">
                            	<tr>
                            		<td>
                                        <asp:HyperLink ID="lnkPrintSolicitud" runat="server" 
                                            NavigateUrl="~/frmAddSolicitud2010.aspx">Imprimir solicitud</asp:HyperLink>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:HyperLink ID="lnkPagare" runat="server" 
                                            NavigateUrl="~/frmAddSolicitud2010.aspx">PAGARE</asp:HyperLink>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:HyperLink ID="lnkContrato" runat="server" 
                                            NavigateUrl="~/frmAddSolicitud2010.aspx">CONTRATO</asp:HyperLink>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:HyperLink ID="lnkTermAndCond" runat="server" 
                                            NavigateUrl="~/frmAddSolicitud2010.aspx">Terminos y condiciones</asp:HyperLink>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:HyperLink ID="lnkBuro" runat="server" 
                                            NavigateUrl="~/frmAddSolicitud2010.aspx">Buro de Credito</asp:HyperLink>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:HyperLink ID="lnkCartaCompromiso" runat="server" 
                                            NavigateUrl="~/frmAddSolicitud2010.aspx">Carta Compromiso</asp:HyperLink>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:LinkButton ID="LBNotaDeVenta" runat="server" 
                                            PostBackUrl="~/frmNotadeVentaAddNew.aspx">Agregar nota de venta</asp:LinkButton>
                                    </td>
                            	</tr>
                            </table>
                            <asp:Image ID="imgBienSolicitudResult" runat="server" 
                                ImageUrl="~/imagenes/palomita.jpg" />
                            <asp:Image ID="imgMalSolicitudResult" runat="server" 
                                ImageUrl="~/imagenes/tache.jpg" />
                            <asp:Label ID="lblSolicitudResult" runat="server"></asp:Label>
                        </asp:Panel>
                        <asp:Button ID="btnAgregar" runat="server" onclick="btnAgregar_Click" 
                            Text="Guardar Solicitud" Height="35px" />
                        
                    </asp:Panel>
                </td>
	        </tr>
        </table>
    </asp:Panel>

</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
