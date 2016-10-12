<%@ Page Title="Pagos a Proveedores" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmPagosAProveedores.aspx.cs" Inherits="Garibay.frmPagosAProveedores" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
	<tr>
		<td class="TableHeader">PAGOS A PROVEEDORES</td>
	</tr>
	<tr>
	    <td>
	        <asp:Panel ID="pnlFiltros" runat="server">
	        <table>
	        	<tr>
	        		<td class="TablaField">Proveedor: </td>
	        		<td>
                        <asp:DropDownList ID="drpdlProveedor" runat="server" AutoPostBack="True" 
                            DataSourceID="sdsProveedor" DataTextField="Nombre" DataValueField="proveedorID" 
                            Height="23px" onselectedindexchanged="drpdlProveedor_SelectedIndexChanged" 
                            Width="460px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsProveedor" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            SelectCommand="SELECT [Nombre], [proveedorID] FROM [Proveedores] order by Nombre">
                        </asp:SqlDataSource>
                    </td>
	        		<td class="TablaField">Ciclo:</td>
	        		<td>
                        <asp:DropDownList ID="drpdlCiclo" runat="server" AutoPostBack="True" 
                            DataSourceID="sdsCiclos" DataTextField="CicloName" DataValueField="cicloID" 
                            Height="23px" Width="181px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" SelectCommand="SELECT        cicloID, CicloName
FROM            Ciclos
WHERE cerrado=@cerrado
ORDER BY fechaInicio DESC">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="FALSE" Name="cerrado" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
	        	</tr>
	        </table>
            </asp:Panel>
	    </td>
	</tr>
	<tr>
	    <td>
                                                        <asp:GridView ID="grvPagos" 
                runat="server" AutoGenerateColumns="False" 
                                                            DataKeyNames="movbanID,movimientoID,pagosProveedoresID" 
                                                            DataSourceID="SqlPagos" onrowdatabound="grvPagos_RowDataBound" 
                                                            
                onrowdeleted="grvPagos_RowDeleted" onrowdeleting="grvPagos_RowDeleting" 
                                                            ShowFooter="True">
                                                            <Columns>
                                                                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                                                                <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyy}" 
                                                                    HeaderText="Fecha" SortExpression="fecha" />
                                                                <asp:BoundField DataField="movbanID" HeaderText="movbanID" 
                                                                    SortExpression="movbanID" visible="false" />
                                                                <asp:BoundField DataField="movimientoID" HeaderText="movimientoID" 
                                                                    SortExpression="movimientoID" visible="false" />
                                                                <asp:TemplateField HeaderText="Forma de Pago">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No. Cheque">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Banco">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Monto">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="pagosNotaCompraID" HeaderText="pagosNotaCompraID" 
                                                                    SortExpression="pagosNotaCompraID" Visible="False" />
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:SqlDataSource ID="SqlPagos" runat="server" 
                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                            
                                                            
                SelectCommand="SELECT fecha, movimientoID, movbanID, pagosProveedoresID FROM  Pagos_Proveedores  
WHERE (proveedorID = @proveedorID AND cicloID = @cicloID)">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="drpdlProveedor" DefaultValue="-1" 
                                                                    Name="proveedorID" PropertyName="SelectedValue" />
                                                                <asp:ControlParameter ControlID="drpdlCiclo" DefaultValue="-1" Name="cicloID" 
                                                                    PropertyName="SelectedValue" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
	                                                    <asp:Button ID="btnActualizar" runat="server" onclick="btnActualizar_Click" 
                                                            Text="Actualizar lista" />
	    </td>
	</tr>
	<tr>
	        <td>
                                                                        <asp:CheckBox ID="chkMostrarAgregarPago" runat="server" 
                                                                            
                    Text="Mostrar Panel Para Agregar Nuevo Pago" CssClass="TableHeader" />
                                                                    <asp:UpdatePanel ID="UpdateAddNewPago" runat="Server">
                                                                        <ContentTemplate>
                                                                        <div runat="Server" id="divAgregarNuevoPago">
                                                                        <table>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <br />
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    Fecha:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtFechaNPago" runat="server" ReadOnly="True"></asp:TextBox>
                                                                                    <rjs:PopCalendar ID="PopCalendar7" runat="server"  Control="txtFechaNPago" 
                                                                                        Separator="/" 
                                                                                         />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RequiredFieldValidator ID="valFecha0" runat="server" 
                                                                                        ControlToValidate="txtFechaNPago" 
                                                                                        ErrorMessage="El campo fecha es necesario"></asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    Tipo de pago:</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="cmbTipodeMovPago" runat="server" Height="22px" 
                                                                                        Width="249px">
                                                                                        
                                                                                        <asp:ListItem Value="1">MOVIMIENTO DE BANCO</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    <asp:Label ID="lblNombre0" runat="server" Text="Nombre:"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtNombrePago" runat="server" Width="266px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    Monto:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtMonto" runat="server" Width="266px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RequiredFieldValidator ID="valMontorequired1" runat="server" 
                                                                                        ControlToValidate="txtMonto" Display="Dynamic" 
                                                                                        ErrorMessage="El campo monto es necesario"></asp:RequiredFieldValidator>
                                                                                    <br />
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                                                                        ControlToValidate="txtMonto" Display="Dynamic" 
                                                                                        ErrorMessage="Escriba una cantida válida" 
                                                                                        ValidationExpression="\d+(.\d*)?"></asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField" colspan="3">
                                                                                    <div id="divPagoMovCaja" runat="Server">
                                                                                        <table>
                                                                                        	<tr>
                                                                                        		<td class="TablaField">El pago se hará de la caja:</td>
                                                                                        		<td>
                                                                                                    <asp:DropDownList ID="ddlPagosBodegas" runat="server" 
                                                                                                        DataSourceID="sdsPagosBodegas" DataTextField="bodega" 
                                                                                                        DataValueField="bodegaID">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:SqlDataSource ID="sdsPagosBodegas" runat="server" 
                                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                        
                                                                                                        
                                                                                                        SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]"></asp:SqlDataSource>
                                                                                                </td>
                                                                                        	</tr>
                                                                                            <tr>
                                                                                                <td class="TablaField">
                                                                                                    Grupo de catálogos:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="drpdlGrupoCatalogosCajaChica" runat="server" 
                                                                                                        AutoPostBack="True" DataSourceID="sdsGruposCatalogosCajaChica" 
                                                                                                        DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID" Height="23px" 
                                                                                                        Width="257px">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:SqlDataSource ID="sdsGruposCatalogosCajaChica" runat="server" 
                                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                        
                                                                                                        
                                                                                                        SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos] ORDER BY [grupoCatalogo]">
                                                                                                    </asp:SqlDataSource>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TablaField">
                                                                                                    Catálogo de cuenta:</td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="drpdlCatalogocuentaCajaChica" runat="server" 
                                                                                                        AutoPostBack="True" DataSourceID="sdsCatalogoCuentaCajaChica" 
                                                                                                        DataTextField="catalogoMovBanco" DataValueField="catalogoMovBancoID" 
                                                                                                        Height="23px" Width="256px">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:SqlDataSource ID="sdsCatalogoCuentaCajaChica" runat="server" 
                                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                        
                                                                                                        
                                                                                                        SelectCommand="SELECT catalogoMovBancoID, catalogoMovBanco FROM catalogoMovimientosBancos WHERE (grupoCatalogoID = @grupoCatalogoID) ORDER BY catalogoMovBanco">
                                                                                                        <SelectParameters>
                                                                                                            <asp:ControlParameter ControlID="drpdlGrupoCatalogosCajaChica" 
                                                                                                                DefaultValue="-1" Name="grupoCatalogoID" PropertyName="SelectedValue" />
                                                                                                        </SelectParameters>
                                                                                                    </asp:SqlDataSource>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TablaField">
                                                                                                    Subcatálogo de cuenta:</td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="drpdlSubcatalogoCajaChica" runat="server" DataSourceID="sdsSubcatalogoCajaChica" 
                                                                                                        DataTextField="subCatalogo" DataValueField="subCatalogoMovBancoID" 
                                                                                                        Height="23px" Width="258px">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:SqlDataSource ID="sdsSubcatalogoCajaChica" runat="server" 
                                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                        
                                                                                                        
                                                                                                        SelectCommand="SELECT SubCatalogoMovimientoBanco.subCatalogo, SubCatalogoMovimientoBanco.subCatalogoMovBancoID FROM SubCatalogoMovimientoBanco INNER JOIN catalogoMovimientosBancos ON SubCatalogoMovimientoBanco.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID WHERE (SubCatalogoMovimientoBanco.catalogoMovBancoID = @catalogoMovBancoID) ORDER BY SubCatalogoMovimientoBanco.subCatalogo">
                                                                                                        <SelectParameters>
                                                                                                            <asp:ControlParameter ControlID="drpdlCatalogocuentaCajaChica" 
                                                                                                                DefaultValue="-1" Name="catalogoMovBancoID" PropertyName="SelectedValue" />
                                                                                                        </SelectParameters>
                                                                                                    </asp:SqlDataSource>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                    <div ID="divMovBanco" runat="Server">
                                                                                        <table border="1">
                                                                                            <tr>
                                                                                                <td align="center" class="TableHeader" colspan="2">
                                                                                                    DATOS MOVIMIENTO DE BANCO</td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TablaField">
                                                                                                    Concepto:</td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="cmbConceptomovBancoPago" runat="server" 
                                                                                                        DataSourceID="sdsConceptoPago" DataTextField="Concepto" 
                                                                                                        DataValueField="ConceptoMovCuentaID" Height="22px" Width="249px">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:SqlDataSource ID="sdsConceptoPago" runat="server" 
                                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                        
                                                                                                        
                                                                                                        
                                                                                                        SelectCommand="SELECT [ConceptoMovCuentaID], [Concepto] FROM [ConceptosMovCuentas]  Where ConceptoMovCuentaID NOT IN (4,5,6,7) ORDER BY [Concepto]">
                                                                                                    </asp:SqlDataSource>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TablaField">
                                                                                                    Cuenta:</td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="cmbCuentaPago" runat="server" 
                                                                                                        DataSourceID="sdsCuentaPago" DataTextField="cuenta" DataValueField="cuentaID" 
                                                                                                        Height="22px" Width="427px">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:SqlDataSource ID="sdsCuentaPago" runat="server" 
                                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                        
                                                                                                        
                                                                                                        
                                                                                                        SelectCommand="SELECT Bancos.nombre + '  ' + CuentasDeBanco.NumeroDeCuenta + ' - ' + CuentasDeBanco.Titular AS cuenta, CuentasDeBanco.cuentaID FROM Bancos INNER JOIN CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID ORDER BY cuenta">
                                                                                                    </asp:SqlDataSource>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TablaField">
                                                                                                    Grupo de catálogos de cuenta fiscal:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="drpdlGrupoCuentaFiscal" runat="server" 
                                                                                                        AutoPostBack="True" DataSourceID="sdsGruposCatalogosfiscalPago" 
                                                                                                        DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID" Height="23px" 
                                                                                                        Width="257px">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:SqlDataSource ID="sdsGruposCatalogosfiscalPago" runat="server" 
                                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                        
                                                                                                        
                                                                                                        SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos]">
                                                                                                    </asp:SqlDataSource>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TablaField">
                                                                                                    Catálogo de cuenta fiscal:</td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="drpdlCatalogocuentafiscalPago" runat="server" 
                                                                                                        AutoPostBack="True" DataSourceID="sdsCatalogoCuentaFiscal" 
                                                                                                        DataTextField="catalogoMovBanco" DataValueField="catalogoMovBancoID" 
                                                                                                        Height="23px" Width="256px">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:SqlDataSource ID="sdsCatalogoCuentaFiscal" runat="server" 
                                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                        
                                                                                                        
                                                                                                        SelectCommand="SELECT catalogoMovBancoID, catalogoMovBanco FROM catalogoMovimientosBancos WHERE (grupoCatalogoID = @grupoCatalogoID)">
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
                                                                                                    <asp:DropDownList ID="drpdlSubcatalogofiscalPago" runat="server" DataSourceID="sdsSubcatalogofiscalPago" 
                                                                                                        DataTextField="subCatalogo" DataValueField="subCatalogoMovBancoID" 
                                                                                                        Height="23px" Width="258px">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:SqlDataSource ID="sdsSubcatalogofiscalPago" runat="server" 
                                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                        
                                                                                                        
                                                                                                        SelectCommand="SELECT SubCatalogoMovimientoBanco.subCatalogo, SubCatalogoMovimientoBanco.subCatalogoMovBancoID FROM SubCatalogoMovimientoBanco INNER JOIN catalogoMovimientosBancos ON SubCatalogoMovimientoBanco.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID WHERE (SubCatalogoMovimientoBanco.catalogoMovBancoID = @catalogoMovBancoID)">
                                                                                                        <SelectParameters>
                                                                                                            <asp:ControlParameter ControlID="drpdlCatalogocuentafiscalPago" 
                                                                                                                DefaultValue="-1" Name="catalogoMovBancoID" PropertyName="SelectedValue" />
                                                                                                        </SelectParameters>
                                                                                                    </asp:SqlDataSource>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TablaField" colspan="2">
                                                                                                    <div ID="divCheque" runat="server">
                                                                                                        <table border="1">
                                                                                                            <tr>
                                                                                                                <td align="center" class="TableHeader" colspan="2">
                                                                                                                    DATOS DE CHEQUE</td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="TablaField">
                                                                                                                    # Cheque (*):</td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtChequeNum" runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="TablaField">
                                                                                                                    # Factura o Larguillo:</td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtFacturaLarguillo" runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="TablaField">
                                                                                                                    Nombre interno:</td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtChequeNombre" runat="server" Width="282px"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="TablaField">
                                                                                                                    Grupo de catálogos de cuenta interna:</td>
                                                                                                                <td>
                                                                                                                    <asp:DropDownList ID="drpdlGrupoCatalogosInternaPago" runat="server" 
                                                                                                                        AutoPostBack="True" DataSourceID="sdsGruposCatalogosInternaPago" 
                                                                                                                        DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID" Height="23px" 
                                                                                                                        Width="235px">
                                                                                                                    </asp:DropDownList>
                                                                                                                    <asp:SqlDataSource ID="sdsGruposCatalogosInternaPago" runat="server" 
                                                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                                        
                                                                                                                        
                                                                                                                        SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos]">
                                                                                                                    </asp:SqlDataSource>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="TablaField">
                                                                                                                    Catálogo de cuenta interna:</td>
                                                                                                                <td>
                                                                                                                    <asp:DropDownList ID="drpdlCatalogoInternoPago" runat="server" 
                                                                                                                        AutoPostBack="True" DataSourceID="sdsCatalogoCuentaInternaPago" 
                                                                                                                        DataTextField="catalogoMovBanco" DataValueField="catalogoMovBancoID" 
                                                                                                                        Height="23px" Width="236px">
                                                                                                                    </asp:DropDownList>
                                                                                                                    <asp:SqlDataSource ID="sdsCatalogoCuentaInternaPago" runat="server" 
                                                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                                        
                                                                                                                        
                                                                                                                        SelectCommand="SELECT catalogoMovBancoID, catalogoMovBanco FROM catalogoMovimientosBancos WHERE (grupoCatalogoID = @grupoCatalogoID)">
                                                                                                                        <SelectParameters>
                                                                                                                            <asp:ControlParameter ControlID="drpdlGrupoCatalogosInternaPago" 
                                                                                                                                DefaultValue="-1" Name="grupoCatalogoID" PropertyName="SelectedValue" />
                                                                                                                        </SelectParameters>
                                                                                                                    </asp:SqlDataSource>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="TablaField">
                                                                                                                    Subcatálogo de cuenta interna:</td>
                                                                                                                <td>
                                                                                                                    <asp:DropDownList ID="drpdlSubcatologointernaPago" runat="server" DataSourceID="sdsSubCatalogoInternaPago" 
                                                                                                                        DataTextField="subCatalogo" DataValueField="subCatalogoMovBancoID" 
                                                                                                                        Height="23px" Width="234px">
                                                                                                                    </asp:DropDownList>
                                                                                                                    <asp:SqlDataSource ID="sdsSubCatalogoInternaPago" runat="server" 
                                                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                                        
                                                                                                                        
                                                                                                                        SelectCommand="SELECT SubCatalogoMovimientoBanco.subCatalogo, SubCatalogoMovimientoBanco.subCatalogoMovBancoID FROM SubCatalogoMovimientoBanco INNER JOIN catalogoMovimientosBancos ON SubCatalogoMovimientoBanco.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID WHERE (SubCatalogoMovimientoBanco.catalogoMovBancoID = @catalogoMovBancoID)">
                                                                                                                        <SelectParameters>
                                                                                                                            <asp:ControlParameter ControlID="drpdlCatalogoInternoPago" DefaultValue="-1" 
                                                                                                                                Name="catalogoMovBancoID" PropertyName="SelectedValue" />
                                                                                                                        </SelectParameters>
                                                                                                                    </asp:SqlDataSource>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:Panel ID="pnlNewPago" runat="server" >
                                                                                        <asp:Image ID="imgBienPago" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                                                                                        <asp:Image ID="imgMalPago" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                                                                                        <asp:Label ID="lblNewPagoResult" runat="server"></asp:Label>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:UpdateProgress ID="UpProgPagos" runat="server" 
                                                                                        AssociatedUpdatePanelID="UpdateAddNewPago" DisplayAfter="0">
                                                                                        <ProgressTemplate>
                                                                                            <asp:Image ID="Image5" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                                                                                            Procesando informacion de pago...
                                                                                        </ProgressTemplate>
                                                                                    </asp:UpdateProgress>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        </div>
                                                                    </ContentTemplate></asp:UpdatePanel>
	        </td>
	    
	</tr>
	<tr>
	
	<td>
                                                                                    <asp:Button ID="btnAddPago" 
            runat="server" 
                                                                                        
            Text="Agregar Pago a Proveedor" onclick="btnAddPago_Click" />
                                                                                </td></tr>
</table>
</asp:Content>
