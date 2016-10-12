<%@ Page Title="Pago a Facturas" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmPagoAFacturas.aspx.cs" Inherits="Garibay.frmPagoAFacturas" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="Server" ID="UpdatePanelContent">
<ContentTemplate>

    

    <table>                                                                       
    <tr>
        <td class="TablaField">
            Mov ID:</td>
        <td>
            <asp:TextBox ID="txtMovID" runat="server" ReadOnly="True" Width="101px"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>

        <tr>
            <td class="TablaField">
                Ciclo:</td>
            <td>
                <asp:DropDownList ID="ddlCiclo" runat="server" DataSourceID="sdsCiclos" 
                    DataTextField="CicloName" DataValueField="cicloID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    
                    SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [CicloName] ASC">
                </asp:SqlDataSource>
            </td>
            <td>
                &nbsp;</td>
        </tr>

        <tr>
            <td class="TablaField">
                Fecha:</td>
            <td>
                <asp:TextBox ID="txtFechaNPago" runat="server" ReadOnly="True"></asp:TextBox>
                <rjs:PopCalendar ID="PopCalendar6" runat="server" Control="txtFechaNPago" 
                    Separator="/" />
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
            <br />
        </td>
    </tr>
    <tr>
        <td class="TablaField" colspan="3">
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
                                DataValueField="ConceptoMovCuentaID" Height="22px">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="sdsConceptoPago" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                
                                SelectCommand="SELECT [ConceptoMovCuentaID], [Concepto] FROM [ConceptosMovCuentas]  Where ConceptoMovCuentaID NOT IN (3,4,5,6,7) ORDER BY [Concepto]">
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField">
                            Cuenta:</td>
                        <td>
                            <asp:DropDownList ID="cmbCuentaPago" runat="server" 
                                DataSourceID="sdsCuentaPago" DataTextField="cuenta" DataValueField="cuentaID" 
                                Height="22px">
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
                                DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID">
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
                                Height="23px">
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
                            <asp:DropDownList ID="drpdlSubcatalogofiscalPago" runat="server" 
                                DataSourceID="sdsSubcatalogofiscalPago" DataTextField="subCatalogo" 
                                DataValueField="subCatalogoMovBancoID">
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
                        <td class="TablaField">
                            Observaciones:</td>
                        <td>
                            <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" 
                                Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdateProgress ID="UpProgPagos" runat="server" 
                AssociatedUpdatePanelID="UpdatePanelContent" DisplayAfter="0">
                <ProgressTemplate>
                    <asp:Image ID="Image5" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                    Procesando informacion de pago...
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:Button ID="btnAddPago" runat="server" onclick="btnAddPago_Click" 
                Text="Agregar Pago a la(s) Factura(s)" />
            <asp:Panel ID="pnlNewPago" runat="server">
                <asp:Image ID="imgBienPago" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                <asp:Image ID="imgMalPago" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                <asp:Label ID="lblNewPagoResult" runat="server"></asp:Label>
            </asp:Panel>
        </td>
    </tr>
    </table>
    
    <asp:Panel runat="Server" ID="PanelFacturasToMov" Visible="False">
        <table>
	        <tr>
		        <td colspan="3" class="TableHeader">
                    <asp:DetailsView ID="dvSaldo" runat="server" AutoGenerateRows="False" 
                        DataKeyNames="movbanID" DataSourceID="sdsSaldos" Height="50px" Width="125px">
                        <Fields>
                            <asp:BoundField DataField="Monto" DataFormatString="{0:C2}" HeaderText="Monto" 
                                SortExpression="Monto">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Facturas" DataFormatString="{0:C2}" 
                                HeaderText="Facturas" ReadOnly="True" SortExpression="Facturas">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Saldo" DataFormatString="{0:C2}" HeaderText="Saldo" 
                                ReadOnly="True" SortExpression="Saldo">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Fields>
                    </asp:DetailsView>
                    <asp:SqlDataSource ID="sdsSaldos" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT MovimientosCuentasBanco.abono AS Monto, SUM(ISNULL(FacturasClientesVenta.total, 0)) AS Facturas, MovimientosCuentasBanco.abono - SUM(ISNULL(FacturasClientesVenta.total, 0)) AS Saldo, MovimientosCuentasBanco.movbanID FROM PagosFacturasClientesVenta INNER JOIN MovimientosCuentasBanco ON PagosFacturasClientesVenta.movbanID = MovimientosCuentasBanco.movbanID INNER JOIN FacturasClientesVenta ON PagosFacturasClientesVenta.FacturaCVID = FacturasClientesVenta.FacturaCV GROUP BY MovimientosCuentasBanco.movbanID, MovimientosCuentasBanco.abono HAVING (MovimientosCuentasBanco.movbanID = @movbanID)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtMovID" Name="movbanID" 
                                PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
	        </tr>
	        <tr>
                <td class="TableHeader" colspan="3">
                    AGREGAR PAGO A FACTURAS</td>
            </tr>
	        <tr>
		        <td colspan="3">
		            <table>
		                <tr>
                            <td class="TablaField">
                                Cliente:</td>
                            <td>
                                <asp:DropDownList ID="ddlClientesVenta" runat="server" 
                                    DataSourceID="sdsClientes" DataTextField="nombre" 
                                    DataValueField="clienteventaID" AutoPostBack="True" 
                                    onselectedindexchanged="ddlClientesVenta_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsClientes" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT [clienteventaID], [nombre] FROM [ClientesVentas] ORDER BY [nombre]">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
		            </table>
		        </td>
	        </tr>
	        <tr>
		        <td>
                    <asp:GridView ID="GridViewFacturasDisponibles" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="FacturaCV" 
                        DataSourceID="sdsFacturasDisponibles">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkFacturaID" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="FacturaCV" HeaderText="FacturaCV" 
                                InsertVisible="False" ReadOnly="True" SortExpression="FacturaCV" />
                            <asp:BoundField DataField="facturaNo" HeaderText="facturaNo" 
                                SortExpression="facturaNo" />
                            <asp:BoundField DataField="total" DataFormatString="{0:C2}" HeaderText="Total" 
                                SortExpression="total">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsFacturasDisponibles" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT [FacturaCV], [facturaNo], [total] FROM [FacturasClientesVenta] WHERE (([cicloID] = @cicloID) AND ([clienteVentaID] = @clienteVentaID))">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlCiclo" Name="cicloID" 
                                PropertyName="SelectedValue" Type="Int32" />
                            <asp:ControlParameter ControlID="ddlClientesVenta" Name="clienteVentaID" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
		        <td>
                    <asp:Button ID="btnAddFacturaAPago" runat="server" onclick="acturaaPago_Click" 
                        Text="&gt;&gt;" />
                        <br />
                    <asp:Button ID="btnRemoveFacturaPago" runat="server" onclick="btnRemoveFacturaPago_Click" 
                        Text="&lt;&lt;" />
                </td>
		        <td>
                    <asp:GridView ID="GridViewFacturasEnPago" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="pagoFVID" 
                        DataSourceID="sdsFacturasEnPago">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkFacturaID" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="FacturaCV" HeaderText="FacturaCV" 
                                InsertVisible="False" ReadOnly="True" SortExpression="FacturaCV" />
                            <asp:BoundField DataField="facturaNo" HeaderText="facturaNo" 
                                SortExpression="facturaNo" />
                            <asp:BoundField DataField="total" DataFormatString="{0:C2}" HeaderText="Total" 
                                SortExpression="total">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsFacturasEnPago" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        SelectCommand="SELECT PagosFacturasClientesVenta.pagoFVID, FacturasClientesVenta.FacturaCV, FacturasClientesVenta.facturaNo, FacturasClientesVenta.total FROM FacturasClientesVenta INNER JOIN PagosFacturasClientesVenta ON FacturasClientesVenta.FacturaCV = PagosFacturasClientesVenta.FacturaCVID INNER JOIN MovimientosCuentasBanco ON PagosFacturasClientesVenta.movbanID = MovimientosCuentasBanco.movbanID WHERE (MovimientosCuentasBanco.movbanID = @movbanID)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtMovID" Name="movbanID" 
                                PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
	        </tr>
        </table>
    </asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
