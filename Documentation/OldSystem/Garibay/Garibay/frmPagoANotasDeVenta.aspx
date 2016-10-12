<%@ Page Title="Pago a Nota de Venta" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmPagoANotasDeVenta.aspx.cs" Inherits="Garibay.frmPagoANotasDeVenta" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:UpdatePanel runat="Server" ID="pnlAddPago">
<ContentTemplate>
    <table>
        <tr>
            <td class="TablaField">Productor:</td>
            <td>
                <asp:DropDownList ID="cmbProductoresPago" runat="server" 
                    DataSourceID="sdsProductoresPago" DataTextField="name" 
                    DataValueField="productorID">
                </asp:DropDownList>
                <cc1:ListSearchExtender ID="cmbProductoresPago_ListSearchExtender" 
                    runat="server" Enabled="True" PromptText="Escriba para Buscar" 
                    TargetControlID="cmbProductoresPago">
                </cc1:ListSearchExtender>
                <asp:SqlDataSource ID="sdsProductoresPago" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="Select LTRIM(productores.apaterno  + SPACE(1) + productores.amaterno + SPACE(1) + productores.nombre) as name, productores.productorID from Productores  order by name">
                </asp:SqlDataSource>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="TablaField">
                Fecha:</td>
            <td>
                <asp:TextBox ID="txtFechaPago" runat="server" ReadOnly="True"></asp:TextBox>
                <rjs:PopCalendar ID="PopCalendar3" runat="server" Control="txtFechaPago" 
                    Separator="/" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="TablaField">
                Tipo de pago:</td>
            <td>
                <asp:DropDownList ID="cmbTipodeMovPago" runat="server" Height="22px" 
                    Width="249px">
                    <asp:ListItem Value="0">EFECTIVO</asp:ListItem>
                    <asp:ListItem>MOVIMIENTO DE BANCO</asp:ListItem>
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
                <br />
            </td>
        </tr>
        <tr>
            <td class="TablaField" colspan="3">
                <div ID="divPagoMovCaja" runat="Server">
                    <table>
                        <tr>
                            <td class="TablaField">
                                El pago se hará de la caja:</td>
                            <td>
                                <asp:DropDownList ID="ddlPagosBodegas" runat="server" 
                                    DataSourceID="sdsPagosBodegas" DataTextField="bodega" DataValueField="bodegaID">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsPagosBodegas" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    
                                    SelectCommand="SELECT bodegaID, bodega FROM Bodegas ORDER BY bodegaID DESC">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Grupo de catálogos:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdlGrupoCatalogosCajaChica" runat="server" 
                                    AutoPostBack="True" DataSourceID="sdsGruposCatalogosCajaChica" 
                                    DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID" 
                                    onselectedindexchanged="drpdlGrupoCatalogosCajaChica_SelectedIndexChanged">
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
                                    onselectedindexchanged="drpdlCatalogocuentaCajaChica_SelectedIndexChanged">
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
                                <asp:DropDownList ID="drpdlSubcatalogoCajaChica" runat="server" 
                                    DataSourceID="sdsSubcatalogoCajaChica" DataTextField="subCatalogo" 
                                    DataValueField="subCatalogoMovBancoID">
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
                                    DataValueField="ConceptoMovCuentaID">
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
                                    DataSourceID="sdsCuentaPago" DataTextField="cuenta" DataValueField="cuentaID">
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
                                    DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID" 
                                    onselectedindexchanged="drpdlGrupoCuentaFiscal_SelectedIndexChanged">
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
                                    onselectedindexchanged="drpdlCatalogocuentafiscalPago_SelectedIndexChanged">
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
                                                <asp:TextBox ID="txtChequeNombre0" runat="server" Width="282px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField">
                                                Grupo de catálogos de cuenta interna:</td>
                                            <td>
                                                <asp:DropDownList ID="drpdlGrupoCatalogosInternaPago" runat="server" 
                                                    AutoPostBack="True" DataSourceID="sdsGruposCatalogosInternaPago" 
                                                    DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID" 
                                                    onselectedindexchanged="drpdlGrupoCatalogosInternaPago_SelectedIndexChanged">
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
                                                    onselectedindexchanged="drpdlCatalogoInternoPago_SelectedIndexChanged">
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
                                                <asp:DropDownList ID="drpdlSubcatologointernaPago" runat="server" 
                                                    DataSourceID="sdsSubCatalogoInternaPago" DataTextField="subCatalogo" 
                                                    DataValueField="subCatalogoMovBancoID">
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
                <asp:Panel ID="pnlNewPagoResult" runat="server">
                    <asp:Image ID="imgBienPago" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                    <asp:Image ID="imgMalPago" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                    <asp:Label ID="lblNewPagoResult" runat="server"></asp:Label>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:UpdateProgress ID="UpProgPagos" runat="server" 
                    AssociatedUpdatePanelID="pnlAddPago" DisplayAfter="0">
                    <ProgressTemplate>
                        <asp:Image ID="Image35" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                        Procesando informacion de pago...
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:Button ID="btnAddPago" runat="server" onclick="btnAddPago_Click" 
                    Text="Agregar Pago" />
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="sdsMontosPago" runat="server"></asp:SqlDataSource>
    <table>
        <tr>
            <td class="TablaField">Ciclo:</td>
            <td>
                <asp:DropDownList ID="ddlCiclosNV" runat="server" DataSourceID="sdsNVCiclos" 
                    DataTextField="CicloName" DataValueField="cicloID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsNVCiclos" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [fechaInicio] DESC">
                </asp:SqlDataSource>
            </td>
        </tr>
    	<tr>
            <td class="TablaField">
                Productor:</td>
            <td>
                <asp:DropDownList ID="ddlProductoresNV" runat="server" 
                    DataSourceID="sdsNVProductores" DataTextField="Productor" 
                    DataValueField="productorID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsNVProductores" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT DISTINCT Productores.productorID, LTRIM(Productores.apaterno + SPACE(1) + Productores.amaterno + SPACE(1) + Productores.nombre) AS Productor FROM Productores INNER JOIN Notasdeventa ON Productores.productorID = Notasdeventa.productorID WHERE (Notasdeventa.cicloID = @cicloID) ORDER BY Productor">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlCiclosNV" Name="cicloID" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <table>
        <tr>
    		<td>Notas Disponibles:</td>
    	    <td>
                Notas agregadas al movimiento</td>
    	</tr>
    	<tr>
    		<td valign="top">
                <asp:GridView ID="gvNotasDisponibles" runat="server" 
                    AutoGenerateColumns="False" DataKeyNames="notadeventaID" 
                    DataSourceID="sdsNVDisponibles">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkAdd" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="notadeventaID" HeaderText="ID" InsertVisible="False" 
                            ReadOnly="True" SortExpression="notadeventaID" />
                        <asp:BoundField DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" 
                            HeaderText="Fecha" SortExpression="Fecha" />
                        <asp:BoundField DataField="Total" DataFormatString="{0:C2}" HeaderText="Total" 
                            SortExpression="Total">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Monto">
                            <ItemTemplate>
                                <asp:TextBox ID="txtMonto" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsNVDisponibles" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT notadeventaID, Fecha, Total FROM Notasdeventa WHERE (cicloID = @cicloID) AND (productorID = @productorID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlCiclosNV" DefaultValue="-1" Name="cicloID" 
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="ddlProductoresNV" DefaultValue="-1" 
                            Name="productorID" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
    	    <td valign="top">
                <asp:GridView ID="gvNotasRelacionadas" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="notadeventaID" DataSourceID="sdsNVRelacionadas">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkRemove" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="notadeventaID" HeaderText="ID" InsertVisible="False" 
                            ReadOnly="True" SortExpression="notadeventaID" />
                        <asp:BoundField DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" 
                            HeaderText="Fecha" SortExpression="Fecha" />
                        <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                            SortExpression="Productor">
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="monto" DataFormatString="{0:C2}" HeaderText="Monto" 
                            SortExpression="monto" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsNVRelacionadas" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT Notasdeventa.notadeventaID, Notasdeventa.Fecha, LTRIM(Productores.apaterno + SPACE(1) + Productores.amaterno + SPACE(1) + Productores.nombre) AS Productor, Pagos_NotaVenta.monto, Pagos_NotaVenta.movbanID, Pagos_NotaVenta.movimientoID FROM Notasdeventa INNER JOIN Productores ON Notasdeventa.productorID = Productores.productorID INNER JOIN Pagos_NotaVenta ON Notasdeventa.notadeventaID = Pagos_NotaVenta.notadeventaID">
                </asp:SqlDataSource>
            </td>
    	</tr>
    	<tr>
    	    <td align="center">
                <asp:Button ID="btnAgregarPagos" runat="server" Text="Agregar" />
            </td>
    	    <td align="center">
                <asp:Button ID="btnRemove" runat="server" Text="Quitar Pagos" />
            </td>
    	</tr>
    </table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
