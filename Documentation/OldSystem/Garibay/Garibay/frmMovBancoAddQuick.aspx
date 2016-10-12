<%@ Page Language="C#" AutoEventWireup="true" Theme="skinverde"  Title="Agregar nuevo movimiento de banco rápido"   CodeBehind="frmMovBancoAddQuick.aspx.cs" Inherits="Garibay.frmMovBancoAddQuick" %>

<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="/App_Themes/skinverde/basicStyle2.css">
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    <script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>
        <table>
            <tr>
                <td colspan="2" align="center" class="TableHeader">
                    MOVIMIENTO DE BANCO</td>
                <td align="center">
                    <asp:Label ID="lblNotaVentaID" runat="server" Text="lblNotaVentaID" 
                        Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" class="TableHeader">
                    <asp:Label ID="lblDescripcion" runat="server" Text="lblDescripcion"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblcredFinID" runat="server" 
                        Text="lblcredFinID" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Cuenta:
                </td>
                <td>
                        <asp:DropDownList ID="drpdlCuenta" runat="server" DataSourceID="sdsCuentas" 
                            DataTextField="cuenta" DataValueField="cuentaID" Height="22px" 
                            Width="449px">
                        </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsCuentas" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>"  
                        SelectCommand="SELECT Bancos.nombre + ' - ' + CuentasDeBanco.NumeroDeCuenta + ' - ' + CuentasDeBanco.Titular AS cuenta, CuentasDeBanco.cuentaID FROM CuentasDeBanco INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID ORDER BY cuenta">
                    </asp:SqlDataSource>
                </td>
                <td>
                        <asp:Label ID="lblmovID" runat="server" 
                        Text="lblmovID" Visible="False"></asp:Label>
                        <asp:Label ID="lblNotaCompraID" runat="server" Visible="False"></asp:Label>
                    </td>
            </tr>
            
            <tr>
                <td class="TablaField">
                    Fecha:</td>
                <td>
                        <asp:TextBox ID="txtFecha" runat="server" ReadOnly="True" Width="89px"></asp:TextBox>
                        <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtFecha" 
                            Separator="/" style="height: 16px" />
                    </td>
                <td>
                        <asp:RequiredFieldValidator ID="valFecha" runat="server" 
                            ControlToValidate="txtFecha" 
                        ErrorMessage="El campo fecha es necesario"></asp:RequiredFieldValidator>
                    </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Nombre interno:</td>
                <td>
                        <asp:TextBox ID="txtChequeNombre" runat="server" Width="250px"></asp:TextBox>
                    </td>
                <td>
                        <asp:RequiredFieldValidator ID="rfvNombreIntero" runat="server" 
                            ControlToValidate="txtChequeNombre" ErrorMessage="Nombre Interno es Requerido"></asp:RequiredFieldValidator>
                    </td>
            </tr>
            <tr>
                <td class="TablaField">
                    # Factura o larguillo:</td>
                <td>
                        <asp:TextBox ID="txtFacturaLarguillo" runat="server" Width="250px"></asp:TextBox>
                    </td>
                <td>
                        &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    # de Cabezas:</td>
                <td>
                        <asp:TextBox ID="txtNumCabezas" runat="server" Width="123px"></asp:TextBox>
                    </td>
                <td>
                        &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Concepto:</td>
                <td>
                        <asp:DropDownList ID="ddlConcepto" runat="server" DataSourceID="sdsConceptos" 
                            DataTextField="Concepto" DataValueField="ConceptoMovCuentaID" Height="23px" 
                            Width="167px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsConceptos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            
                        SelectCommand="SELECT [ConceptoMovCuentaID], [Concepto] FROM [ConceptosMovCuentas]  where concepto &lt;&gt; 'ANTICIPO LIQUIDACION' ORDER BY [Concepto]">
                        </asp:SqlDataSource>
                    </td>
                <td>
                        &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Grupo de catálogos de cuenta interna:</td>
                <td>
                    <asp:DropDownList ID="drpdlGrupoCatalogos" 
                            runat="server" AutoPostBack="True" DataSourceID="sdsGruposCatalogos" 
                            DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID" Height="23px" 
                            onselectedindexchanged="drpdlGrupoCatalogos_SelectedIndexChanged" 
                            Width="235px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsGruposCatalogos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            
                        SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos]">
                        </asp:SqlDataSource>
                        </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Catálogo de cuenta interna:</td>
                <td>
                    <asp:DropDownList ID="drpdlCatalogoInterno" 
                            runat="server" AutoPostBack="True" DataSourceID="sdsCatalogoCuentaInterna" 
                            DataTextField="catalogoMovBanco" DataValueField="catalogoMovBancoID" 
                            Height="23px" Width="236px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsCatalogoCuentaInterna" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            
                        SelectCommand="SELECT catalogoMovBancoID, claveCatalogo + ' -  ' + catalogoMovBanco AS catalogoMovBanco FROM catalogoMovimientosBancos WHERE (grupoCatalogoID = @grupoCatalogoID)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="drpdlGrupoCatalogos" DefaultValue="-1" 
                                    Name="grupoCatalogoID" PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Subcatálogo de cuenta interna:</td>
                <td>
                    <asp:DropDownList 
                            ID="drpdlSubcatologointerna" runat="server" AutoPostBack="True" 
                            DataSourceID="sdsSubCatalogoInterna" DataTextField="subCatalogo" 
                            DataValueField="subCatalogoMovBancoID" Height="23px" Width="234px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsSubCatalogoInterna" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            
                        SelectCommand="SELECT SubCatalogoMovimientoBanco.subCatalogoClave + ' - ' + SubCatalogoMovimientoBanco.subCatalogo AS subCatalogo, SubCatalogoMovimientoBanco.subCatalogoMovBancoID FROM SubCatalogoMovimientoBanco INNER JOIN catalogoMovimientosBancos ON SubCatalogoMovimientoBanco.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID WHERE (SubCatalogoMovimientoBanco.catalogoMovBancoID = @catalogoMovBancoID)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="drpdlCatalogoInterno" DefaultValue="-1" 
                                    Name="catalogoMovBancoID" PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                            <asp:CheckBox ID="chkMostrarFiscales" runat="server" 
                                Text="Los datos fiscales son diferentes que los internos." 
                                CssClass="TablaField" />
                            </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                            <asp:Panel ID="divConceptosFiscales" runat="Server">
                                <table class="style1">
                                    <tr>
                                        <td class="TablaField">
                                            Nombre Fiscal:</td>
                                        <td>
                                <asp:TextBox ID="txtNombre" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TablaField">
                                            Grupo de catálogos de cuenta interna:</td>
                                        <td>
                                            <asp:DropDownList 
                                    ID="drpdlGrupoCuentaFiscal" runat="server" AutoPostBack="True" 
                                    DataSourceID="sdsGruposCatalogosfiscal" DataTextField="grupoCatalogo" 
                                    DataValueField="grupoCatalogosID" Height="23px" 
                                    onselectedindexchanged="drpdlGrupoCuentaFiscal_SelectedIndexChanged" 
                                    Width="257px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsGruposCatalogosfiscal" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    
                                    
                                                SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos]">
                                </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TablaField">
                                            Catálogo de cuenta fiscal:</td>
                                        <td>
                                            <asp:DropDownList ID="drpdlCatalogocuentafiscal" 
                                    runat="server" AutoPostBack="True" DataSourceID="sdsCatalogoCuentaFiscal" 
                                    DataTextField="catalogoMovBanco" DataValueField="catalogoMovBancoID" 
                                    Height="23px" Width="256px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsCatalogoCuentaFiscal" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    
                                    
                                                SelectCommand="SELECT catalogoMovBancoID, claveCatalogo + ' -  ' + catalogoMovBanco AS catalogoMovBanco FROM catalogoMovimientosBancos WHERE (grupoCatalogoID = @grupoCatalogoID)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="drpdlGrupoCuentaFiscal" DefaultValue="-1" 
                                            Name="grupoCatalogoID" PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TablaField">
                                            Subcatálogo de cuenta fiscal:</td>
                                        <td>
                                            <asp:DropDownList ID="drpdlSubcatalogofiscal" 
                                    runat="server" AutoPostBack="True" DataSourceID="sdsSubcatalogofiscal" 
                                    DataTextField="subCatalogo" DataValueField="subCatalogoMovBancoID" 
                                    Height="23px" Width="258px">
                                </asp:DropDownList>
                                            <asp:SqlDataSource ID="sdsSubcatalogofiscal" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    
                                    
                                                SelectCommand="SELECT SubCatalogoMovimientoBanco.subCatalogoClave + ' - ' + SubCatalogoMovimientoBanco.subCatalogo AS subCatalogo, SubCatalogoMovimientoBanco.subCatalogoMovBancoID FROM SubCatalogoMovimientoBanco INNER JOIN catalogoMovimientosBancos ON SubCatalogoMovimientoBanco.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID WHERE (SubCatalogoMovimientoBanco.catalogoMovBancoID = @catalogoMovBancoID)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="drpdlCatalogocuentafiscal" DefaultValue="-1" 
                                            Name="catalogoMovBancoID" PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                </table>
                        </asp:Panel>
                            <cc1:CollapsiblePanelExtender ID="divConceptosFiscales_CollapsiblePanelExtender" 
                                runat="server" CollapseControlID="chkMostrarFiscales" Collapsed="True" 
                                Enabled="True" ExpandControlID="chkMostrarFiscales" 
                                TargetControlID="divConceptosFiscales">
                            </cc1:CollapsiblePanelExtender>
                    </td>
                <td>
                            &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    # Cheque (*)</td>
                <td>
                        <asp:TextBox ID="txtChequeNum" runat="server" Width="94px"></asp:TextBox>
                    </td>
                <td>
                        &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Tipo de movimiento:</td>
                <td>
                        <asp:DropDownList ID="cmbTipodeMov" runat="server" Height="22px" 
                            
                        Width="100px">
                            <asp:ListItem>CARGO</asp:ListItem>
                            <asp:ListItem>ABONO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                <td>
                        &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Monto:</td>
                <td>
                        <asp:TextBox ID="txtMonto" runat="server"></asp:TextBox>
                        <asp:Label ID="lblMontoAnt" runat="server" Text="lblMontoAnt" Visible="False"></asp:Label>
                        </td>
                <td>
                        <asp:RequiredFieldValidator ID="rfvMonto" runat="server" 
                            ControlToValidate="txtMonto" ErrorMessage="El Monto es requerido"></asp:RequiredFieldValidator>
                        </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="chkFactura" runat="server" 
                        Text="Relacionar movimiento con Factura de venta" CssClass="TablaField" />
                        <asp:UpdateProgress runat="Server" id="UPFacturas" 
                            AssociatedUpdatePanelID="pnlUPFactura" DisplayAfter="0">
        		        <ProgressTemplate>
        		            CARGANDO DATOS DE FACTURAS...<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/cargando.gif" />
        		        </ProgressTemplate>
        		        </asp:UpdateProgress>
                    <asp:Panel runat="Server" id="pnlFactura">
                    <asp:UpdatePanel runat="Server" id="pnlUPFactura">
                    <ContentTemplate>
                        <table>
                    	    <tr>
                    		    <td class="TablaField">Ciclo:</td> 
                    		    <td>
                                        <asp:DropDownList ID="ddlFacturaCiclo" runat="server" 
                                            DataSourceID="sdsFacturasCiclo" DataTextField="CicloName" 
                                            DataValueField="cicloID" 
                                            onselectedindexchanged="ddlFacturaCiclo_SelectedIndexChanged" 
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsFacturasCiclo" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [fechaInicio] DESC">
                                        </asp:SqlDataSource>
                                </td>
                    	    </tr>
                            <tr>
                                <td class="TablaField">
                                    Clientes</td>
                                <td>
                                    <asp:DropDownList ID="ddlFacturaClientesVenta" runat="server" 
                                        DataSourceID="sdsFacturaClienteVenta" DataTextField="nombre" 
                                        DataValueField="clienteventaID" 
                                        onselectedindexchanged="ddlFacturaClientesVenta_SelectedIndexChanged" 
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsFacturaClienteVenta" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        SelectCommand="SELECT DISTINCT ClientesVentas.clienteventaID, ClientesVentas.nombre FROM FacturasClientesVenta INNER JOIN ClientesVentas ON FacturasClientesVenta.clienteVentaID = ClientesVentas.clienteventaID WHERE (FacturasClientesVenta.cicloID = @cicloID) ORDER BY ClientesVentas.nombre DESC">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ddlFacturaCiclo" DefaultValue="-1" 
                                                Name="cicloID" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td class="TablaField">
                                    Factura:</td>
                                <td>
                                    <asp:DropDownList ID="ddlFacturas" runat="server" 
                                        DataSourceID="sdsFacturasClienteVenta" DataTextField="facturaNo" 
                                        DataValueField="FacturaCV">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsFacturasClienteVenta" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        SelectCommand="SELECT FacturaCV, facturaNo, clienteVentaID FROM FacturasClientesVenta WHERE (cicloID = @cicloID) AND (clienteVentaID = @clienteVentaID) ORDER BY facturaNo DESC">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ddlFacturaCiclo" DefaultValue="-1" 
                                                Name="cicloID" PropertyName="SelectedValue" />
                                            <asp:ControlParameter ControlID="ddlFacturaClientesVenta" DefaultValue="-1" 
                                                Name="clienteVentaID" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </asp:Panel>
                    <cc1:CollapsiblePanelExtender ID="pnlFactura_CollapsiblePanelExtender" 
                        runat="server" CollapseControlID="chkFactura" Collapsed="True" Enabled="True" 
                        ExpandControlID="chkFactura" TargetControlID="pnlFactura">
                    </cc1:CollapsiblePanelExtender>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="chkCreditoFinanciero" runat="server" 
                        Text="Relacionar con Credito Financiero" CssClass="TablaField" />
                    <asp:UpdateProgress runat="Server" id="UpdateProgress2" 
                        AssociatedUpdatePanelID="ppnlUPCreditoFinanciero" DisplayAfter="0">
    		        <ProgressTemplate>
    		            CARGANDO DATOS DE CREDITOS FINANCIEROS...<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/imagenes/cargando.gif" />
    		        </ProgressTemplate>
    		        </asp:UpdateProgress>
                    <asp:Panel ID="pnlCreditoFinanciero" runat="server">
                    <asp:UpdatePanel runat="Server" id="ppnlUPCreditoFinanciero">
                    <ContentTemplate>
                        <table>
                        	<tr>
                        		<td class="TablaField">BANCO:</td>
                        		<td>
                                    <asp:DropDownList ID="ddlCreditoFinBancos" runat="server" AutoPostBack="True" 
                                        DataSourceID="sdsCredFinBancos" DataTextField="nombre" DataValueField="bancoID" 
                                        onselectedindexchanged="ddlCreditoFinBancos_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsCredFinBancos" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        SelectCommand="SELECT [bancoID], [nombre] FROM [Bancos] ORDER BY [nombre]">
                                    </asp:SqlDataSource>
                                </td>
                        	</tr>
                            <tr>
                                <td class="TablaField">
                                    CREDITO:</td>
                                <td>
                                    <asp:DropDownList ID="ddlCreditosFinancieros" runat="server" 
                                        AutoPostBack="True" DataSourceID="sdsCreditosFinancieros" 
                                        DataTextField="Credito" DataValueField="creditoFinancieroID" 
                                        onselectedindexchanged="ddlCreditosFinancieros_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsCreditosFinancieros" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        SelectCommand="SELECT creditoFinancieroID, empresa_acreditada + ' - ' + numCredito + ' ' + numControl AS Credito FROM CreditosFinancieros WHERE (bancoID = @bancoID) ORDER BY Credito">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ddlCreditoFinBancos" DefaultValue="-1" 
                                                Name="bancoID" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td class="TablaField">
                                    CERTIFICADOS:</td>
                                <td>
                                    <asp:CheckBoxList ID="chkListCredBinCertificados" runat="server" 
                                        DataSourceID="sdsCredFinCertificados" DataTextField="Certificado" 
                                        DataValueField="CredFinCertID">
                                    </asp:CheckBoxList>
                                    <asp:SqlDataSource ID="sdsCredFinCertificados" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        SelectCommand="SELECT CredFinCertificados.CredFinCertID, Bodegas.bodega + ' - ' + Empresas.Empresa + ' - ' + CredFinCertificados.numdeCertificados AS Certificado FROM CredFinCertificados INNER JOIN Bodegas ON CredFinCertificados.bodegaID = Bodegas.bodegaID INNER JOIN Empresas ON CredFinCertificados.empresaCertificadaID = Empresas.empresaID WHERE (CredFinCertificados.CredFinCertID = @CredFinCertID)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ddlCreditosFinancieros" DefaultValue="-1" 
                                                Name="CredFinCertID" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </asp:Panel>
                    <cc1:CollapsiblePanelExtender ID="pnlCreditoFinanciero_CollapsiblePanelExtender" 
                        runat="server" CollapseControlID="chkCreditoFinanciero" Collapsed="True" 
                        Enabled="True" ExpandControlID="chkCreditoFinanciero" 
                        TargetControlID="pnlCreditoFinanciero">
                    </cc1:CollapsiblePanelExtender>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td >

                    <asp:CheckBox ID="chkNotadeVenta" runat="server" 
                        Text="Relacionar con Nota de Venta" CssClass="TablaField" />
                        <asp:UpdateProgress runat="Server" id="UpdateProgress1" 
                        AssociatedUpdatePanelID="UPNotaVenta" DisplayAfter="0">
    		        <ProgressTemplate>
    		            CARGANDO DATOS DE NOTAS DE VENTA...<asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/imagenes/cargando.gif" />
    		        </ProgressTemplate>
    		        </asp:UpdateProgress>
    		        <asp:Panel ID="pnlNotadeVenta" runat="server">
                    <asp:UpdatePanel runat="Server" id="UPNotaVenta">
                    <ContentTemplate>
                        <table>
                        	<tr>
                        		<td class="TablaField">Ciclo:</td>
                        		<td>
                                    <asp:DropDownList ID="drpdlCicloNotaVenta" runat="server" AutoPostBack="True" 
                                        DataSourceID="sdsCiclo" DataTextField="cicloname" DataValueField="cicloID" 
                                        
                                        Height="19px" Width="203px" 
                                        onselectedindexchanged="drpdlCicloNotaVenta_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsCiclo" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        SelectCommand="SELECT cicloID, cicloname FROM Ciclos ORDER BY cicloname">
                                    </asp:SqlDataSource>
                                </td>
                        	</tr>
                            <tr>
                                <td class="TablaField">
                                    Productor:</td>
                                <td>
                                    <asp:DropDownList ID="drpdlProductoresNotaVenta" runat="server" 
                                        AutoPostBack="True" DataSourceID="sdsProductorNotas" 
                                        DataTextField="name" DataValueField="productorID" onselectedindexchanged="drpdlProductoresNotaVenta_SelectedIndexChanged" 
                                       >
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsProductorNotas" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        SelectCommand="SELECT Distinct Notasdeventa.productorID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS name FROM Productores INNER JOIN Notasdeventa ON Productores.productorID = Notasdeventa.productorID WHERE ( Notasdeventa.cicloID = @cicloID AND Notasdeventa.pagada = 0)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="drpdlCicloNotaVenta" DefaultValue="-1" 
                                                Name="cicloID" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td class="TablaField">
                                    Nota:</td>
                                <td>
                                    <asp:DropDownList ID="dprdlNota" runat="server" DataSourceID="sdsNotasVenta" 
                                        DataTextField="NotadeVenta" DataValueField="notadeventaID">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsNotasVenta" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        
                                        SelectCommand="SELECT Folio + ' - ' + CAST(notadeventaID AS VARCHAR(50)) + ' ' + CONVERT (varchar(50), Fecha, 103) AS NotadeVenta, notadeventaID FROM Notasdeventa WHERE (productorID = @productorID)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="drpdlProductoresNotaVenta" DefaultValue="-1" 
                                                Name="productorID" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </asp:Panel>
                     <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" 
                        runat="server" CollapseControlID="chkNotadeVenta" Collapsed="True" 
                        Enabled="True" ExpandControlID="chkNotadeVenta" 
                        TargetControlID="pnlNotadeVenta">
                    </cc1:CollapsiblePanelExtender>

                </td>
                <td>
                        &nbsp;</td>
                <td>
                        &nbsp;</td>
            </tr>
            <tr>
                <td >
                   
                    <asp:CheckBox ID="chkNotadeCompra" runat="server" 
                        Text="Relacionar con Nota de Compra" CssClass="TablaField" />
                        <asp:UpdateProgress runat="Server" id="UpdateProgress3" 
                        AssociatedUpdatePanelID="UPNotaCompra" DisplayAfter="0">
    		        <ProgressTemplate>
    		            CARGANDO DATOS DE NOTAS DE COMPRA...<asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/imagenes/cargando.gif" />
    		        </ProgressTemplate>
    		        </asp:UpdateProgress>
    		        <asp:Panel ID="pnlNotaCompra" runat="server">
                    <asp:UpdatePanel runat="Server" id="UPNotaCompra">
                    <ContentTemplate>
                        <table>
                        	<tr>
                        		<td class="TablaField">Ciclo:</td>
                        		<td>
                                    <asp:DropDownList ID="drpdlCicloNotaCompra" runat="server" AutoPostBack="True" 
                                        DataSourceID="sdsCicloNotaCompra"  
                                        
                                        Height="19px" Width="203px" DataTextField="cicloname" 
                                        DataValueField="cicloID" onselectedindexchanged="drpdlCicloNotaCompra_SelectedIndexChanged" 
                                        >
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsCicloNotaCompra" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        SelectCommand="SELECT cicloID, cicloname FROM Ciclos ORDER BY cicloname">
                                    </asp:SqlDataSource>
                                </td>
                        	</tr>
                            <tr>
                                <td class="TablaField">
                                    Proveedor:</td>
                                <td>
                                    <asp:DropDownList ID="drpdlProveedoresNotaCompra" runat="server" 
                                        AutoPostBack="True" DataSourceID="sdsProveedoresnotaCompra" 
                                        DataTextField="Nombre" DataValueField="proveedorID" 
                                        onselectedindexchanged="drpdlProveedoresNotaCompra_SelectedIndexChanged" style="height: 22px" 
                                       >
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsProveedoresnotaCompra" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        
                                        SelectCommand="SELECT DISTINCT NotasDeCompra.proveedorID, Proveedores.Nombre FROM Proveedores INNER JOIN NotasDeCompra ON Proveedores.proveedorID = Proveedores.proveedorID WHERE (NotasDeCompra.cicloID = @cicloID) AND (NotasdeCompra.pagada = 0)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="drpdlCicloNotaCompra" DefaultValue="-1" 
                                                Name="cicloID" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td class="TablaField">
                                    Nota de compra:</td>
                                <td>
                                    <asp:DropDownList ID="drpdlNotaCompra" runat="server" DataSourceID="sdsNotaCompra" 
                                        DataTextField="NotaDeCompra" DataValueField="notadecompraID">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsNotaCompra" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        SelectCommand="SELECT Folio + ' - ' + CAST(notadecompraID AS VARCHAR(50)) + ' ' + CONVERT (varchar(50), Fecha, 103) AS NotaDeCompra, notadecompraID FROM NotasdeCompra WHERE (proveedorID = @proveedorID)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="drpdlProveedoresNotaCompra" DefaultValue="-1" 
                                                Name="proveedorID" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </asp:Panel>
                     <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" 
                        runat="server" CollapseControlID="chkNotadeCompra" Collapsed="True" 
                        Enabled="True" ExpandControlID="chkNotadeCompra" 
                        TargetControlID="pnlNotaCompra">
                    </cc1:CollapsiblePanelExtender>
                
                </td>
                <td>
                        &nbsp;</td>
                <td>
                        &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                                                <asp:Panel ID="pnlNewMov" runat="server" 
                        Visible="False">
                                                    <asp:Image ID="imgBien" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                                                    <asp:Image ID="imgMal" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                                                    <asp:Label ID="lblNewMovResult" runat="server" Font-Size="Large"></asp:Label>
                                                </asp:Panel>
                                            </td>
                <td>
                                                &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                                                <asp:Button ID="btnAgregar0" runat="server"
                                                    Text="Agregar" CausesValidation="False" onclick="btnAgregar0_Click" 
                                                     />
                                                <asp:Button ID="btnModificar" runat="server" Text="Modificar" 
                                                    onclick="btnModificar_Click" style="height: 26px" 
                                                    />
                                                <asp:Button ID="btnCancelar" runat="server" Text="Salir" 
                                                    onclick="btnCancelar_Click" CausesValidation="False" 
                                                     />
                                            </td>
                <td>
                                                &nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
