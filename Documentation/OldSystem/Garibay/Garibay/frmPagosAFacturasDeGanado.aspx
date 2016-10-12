<%@ Page Title="Pagos a Ganado" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmPagosAFacturasDeGanado.aspx.cs" Inherits="Garibay.frmPagosAFacturasDeGanado" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="Server" id="pnlMovimientoDetalle" Visible="False">
        <table>
            <tr>
                <td>Movimiento No. 
                    <asp:Label ID="lblMovBanID" runat="server" Text="0"></asp:Label></td>
            </tr>
    	    <tr>
    		    <td>
                    <asp:DetailsView ID="dvDetalleMov" runat="server" Height="50px" Width="125px" 
                        AutoGenerateRows="False" DataSourceID="sdsDetalleMovimiento">
                        <Fields>
                            <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" ReadOnly="True" 
                                SortExpression="Cuenta" >
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" 
                                DataFormatString="{0:dd/MM/yyyy}" >
                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cargo" HeaderText="Monto" SortExpression="cargo" 
                                DataFormatString="{0:C2}" >
                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" 
                                SortExpression="nombre" >
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numCheque" HeaderText="Cheque" 
                                SortExpression="numCheque" >
                                <ItemStyle HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="chequeNombre" HeaderText="Cheque Nombre" 
                                SortExpression="chequeNombre" >
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                        </Fields>
                    </asp:DetailsView>
                    <asp:SqlDataSource ID="sdsDetalleMovimiento" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        SelectCommand="SELECT LTRIM(Bancos.nombre + SPACE(1) + CuentasDeBanco.NumeroDeCuenta) AS Cuenta, MovimientosCuentasBanco.fecha, MovimientosCuentasBanco.cargo, MovimientosCuentasBanco.nombre, MovimientosCuentasBanco.numCheque, MovimientosCuentasBanco.chequeNombre FROM MovimientosCuentasBanco INNER JOIN CuentasDeBanco ON MovimientosCuentasBanco.cuentaID = CuentasDeBanco.cuentaID INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID WHERE (MovimientosCuentasBanco.movbanID = @movbanID)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="lblMovBanID" Name="movbanID" 
                                PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
    	    </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlAddNewPago" runat="Server">
            <div ID="divAgregarNuevoPago" runat="Server">
                <table>
                    <tr>
                        <td class="TablaField">Ciclo:</td>
                        <td>
                            <asp:DropDownList ID="ddlCiclos" runat="server" DataSourceID="sdsCiclos" 
                                DataTextField="CicloName" DataValueField="cicloID">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [CicloName] DESC">
                            </asp:SqlDataSource>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="TablaField">
                            Fecha:</td>
                        <td>
                            <asp:TextBox ID="txtFechaPago" runat="server" ReadOnly="True"></asp:TextBox>
                            <rjs:PopCalendar ID="PopCalendar7" runat="server" Control="txtFechaNPago" 
                                Separator="/" />
                        </td>
                        <td>&nbsp;</td>
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
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="TablaField">
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
                                                                                            DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID">
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
                                                                                            DataTextField="catalogoMovBanco" DataValueField="catalogoMovBancoID">
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
                                                                                <tr>
                                                                                    <td>
                                                                                        Observaciones:</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtObserv" runat="server" TextMode="MultiLine"></asp:TextBox>
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
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnAddPago" runat="server" onclick="btnAddPago_Click" 
                                Text="Agregar Pago a la Factura" />
                        </td>
                    </tr>
                </table>
            </div>
    </asp:Panel>
    
    
    <asp:Panel ID="pnlNewPagoResult" runat="server">
        <asp:Image ID="imgBienPago" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
        <asp:Image ID="imgMalPago" runat="server" ImageUrl="~/imagenes/tache.jpg" />
        <asp:Label ID="lblNewPagoResult" runat="server"></asp:Label>
    </asp:Panel>
    <asp:Panel runat="Server" id="pnlFacturasToAdd" Visible="False" >
        <table>
            <tr>
                <td>
                    <table>
                        <tr><td>Ganadero:</td><td>
                            <asp:DropDownList ID="ddlGanaderos" runat="server" DataSourceID="sdsGanaderos" 
                                DataTextField="Nombre" DataValueField="ganProveedorID" AutoPostBack="True" 
                                onselectedindexchanged="ddlGanaderos_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="sdsGanaderos" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                SelectCommand="SELECT DISTINCT gan_Proveedores.ganProveedorID, gan_Proveedores.Nombre FROM gan_Proveedores INNER JOIN FacturasdeGanado ON gan_Proveedores.ganProveedorID = FacturasdeGanado.ganProveedorID ORDER BY gan_Proveedores.Nombre">
                            </asp:SqlDataSource>
                        </td></tr>
                        <tr><td colspan="2">
                            <asp:GridView ID="gvFacturasGanado" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="FacturadeGanadoID" DataSourceID="sdsFacturasGanado">
                                <Columns>
                                    <asp:TemplateField HeaderText="Agregar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAgregar" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FacturadeGanadoID" HeaderText="Factura ID" 
                                        InsertVisible="False" ReadOnly="True" SortExpression="FacturadeGanadoID">
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                        HeaderText="Fecha" SortExpression="fecha" />
                                    <asp:BoundField DataField="total" DataFormatString="{0:C2}" HeaderText="Total" 
                                        SortExpression="total">
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Cantidad a Pagar">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCantidadAPagar" runat="server" 
                                                Text='<%# Bind("total", "{0:C2}") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="sdsFacturasGanado" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                SelectCommand="SELECT [FacturadeGanadoID], [fecha], [total] FROM [FacturasdeGanado] WHERE ([ganProveedorID] = @ganProveedorID) ORDER BY [fecha]">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlGanaderos" DefaultValue="-1" 
                                        Name="ganProveedorID" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            </td></tr>
                    </table>
                </td>
                <td align="center" valign="middle" >
                    <asp:Button ID="btnAgregarPagosDeFacturas" runat="server" Text="&gt;&gt;" 
                        onclick="Button1_Click" />
                    <br />
                    <br />
                    <asp:Button ID="btnQuitarPagosDeFacturas" runat="server" Text="&lt;&lt;" 
                        onclick="btnQuitarPagosDeFacturas_Click" />
                </td>
                <td>
                    <asp:GridView ID="gvFacturasRelacionadas" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="pagoID" 
                        DataSourceID="sdsFacturasRelacionadas">
                        <Columns>
                            <asp:TemplateField HeaderText="Quitar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkQuitar" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="FacturadeGanadoID" HeaderText="Factura ID" 
                                SortExpression="FacturadeGanadoID"></asp:BoundField>
                            <asp:BoundField DataField="montoPago" DataFormatString="{0:C2}" 
                                HeaderText="Monto Pagado" SortExpression="montoPago">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsFacturasRelacionadas" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT pagoID, FacturadeGanadoID, montoPago, movbanID FROM PagosFacturaDeGanado WHERE (movbanID = @movbanID)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="lblMovBanID" DefaultValue="-1" Name="movbanID" 
                                PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
