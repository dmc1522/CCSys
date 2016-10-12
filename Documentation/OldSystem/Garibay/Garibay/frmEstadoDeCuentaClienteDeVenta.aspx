<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/MasterPage.Master" Title="Estado de cuenta de cliente de venta" CodeBehind="frmEstadoDeCuentaClienteDeVenta.aspx.cs" Inherits="Garibay.frmEstadoDeCuentaClienteDeVenta" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>


<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
<asp:UpdatePanel runat="Server" ID="pnlDatos">
<ContentTemplate>
<asp:UpdateProgress runat="Server" ID="updProgress" 
        AssociatedUpdatePanelID="pnlDatos" DisplayAfter="0">
<ProgressTemplate>
    <img alt="Cargando..." src="imagenes/cargando.gif" /> Procesando...
</ProgressTemplate>
</asp:UpdateProgress>    
         
        <table >
            <tr>
                <td align="center" colspan="2" class="TableHeader">
                    ESTADO DE CUENTA DE LOS CLIENTES DE VENTA</td>
            </tr>
            <tr>
                <td align="center" class="TablaField">
                    CLIENTE:</td>
                <td class="style3">
                    <asp:DropDownList ID="drpdlClienteDeVenta" runat="server" AutoPostBack="True" 
                        DataSourceID="sdsClientesDeVenta" DataTextField="nombre" 
                        DataValueField="clienteventaID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsClientesDeVenta" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        SelectCommand="SELECT [clienteventaID], [nombre] FROM [ClientesVentas] ORDER BY [nombre]">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" class="TableHeader">
                    ESTADO DE CUENTA GENERAL.</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:DetailsView ID="DetailsViewEstadoGeneral" runat="server" 
                        AutoGenerateRows="False" DataSourceID="sdsEstadoGeneral" Height="50px" 
                        Width="125px">
                        <Fields>
                            <asp:BoundField DataField="KgsEntregado" DataFormatString="{0:n2}" 
                                HeaderText="KgsEntregado" SortExpression="KgsEntregado">
                            <ItemStyle Font-Bold="True" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalFacturado" DataFormatString="{0:c2}" 
                                HeaderText="TotalFacturado" SortExpression="TotalFacturado">
                            <ItemStyle Font-Bold="True" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalPagado" DataFormatString="{0:c2}" 
                                HeaderText="TotalPagado" SortExpression="TotalPagado">
                            <ItemStyle Font-Bold="True" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Total" DataFormatString="{0:c2}" HeaderText="Total" 
                                SortExpression="Total">
                            <ItemStyle Font-Bold="True" HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Fields>
                    </asp:DetailsView>
                    <asp:SqlDataSource ID="sdsEstadoGeneral" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="ReturnEstadodeCuentaClientesDeVenta" 
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="drpdlClienteDeVenta" Name="clienteVentaId" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td align="center">
                    <asp:GridView ID="gvConcentradoXProd" runat="server" 
                        AutoGenerateColumns="False" DataSourceID="sdsFacturasXProd">
                        <Columns>
                            <asp:BoundField DataField="Producto" HeaderText="Producto" 
                                SortExpression="Producto">
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Cantidad" DataFormatString="{0:N2}" 
                                HeaderText="Cantidad" SortExpression="Cantidad">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Total" DataFormatString="{0:C2}" HeaderText="Total" 
                                SortExpression="Total">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Pagos" DataFormatString="{0:C2}" 
                                HeaderText="Total Pagos" ReadOnly="True" SortExpression="totalPagos">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsFacturasXProd" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" SelectCommand="SELECT     vSaldoXProdDeFacturasDeVenta.clienteVentaID, vSaldoXProdDeFacturasDeVenta.Producto, SUM(vSaldoXProdDeFacturasDeVenta.Cantidad) AS cantidad, SUM(vSaldoXProdDeFacturasDeVenta.Total) AS Total, vSaldoXProdDeFacturasDeVenta.productoID,
ISNULL((
SELECT     SUM(vListaPagosYTotalesFacVenta.totalPagos) AS totalPagos
FROM         vListaPagosYTotalesFacVenta
where FacturaCVID in 
	(
		SELECT vSXPDFDV.FacturaCV
		FROM         vSaldoXProdDeFacturasDeVenta as vSXPDFDV
		WHERE     (vSXPDFDV.clienteVentaID = vSaldoXProdDeFacturasDeVenta.clienteVentaID) AND (vSXPDFDV.productoID = vSaldoXProdDeFacturasDeVenta.productoID)
	)
),0) as Pagos
FROM         vSaldoXProdDeFacturasDeVenta
GROUP BY clienteVentaID, Producto, productoID
HAVING vSaldoXProdDeFacturasDeVenta.clienteVentaID = @clienteVentaID
ORDER BY clienteVentaID, Producto
">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="drpdlClienteDeVenta" DefaultValue="-1" 
                                Name="clienteVentaID" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:CheckBox ID="chkShowBoletas" runat="server" CssClass="TableHeader" 
                        Text="MOSTRAR BOLETAS DEL CLIENTE DE VENTA" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Panel runat="Server" id="pnlBoletas">
                        <asp:GridView ID="gvBoletasDisponibles" runat="server" 
                            
AutoGenerateColumns="False" DataKeyNames="boletaID" 
                            DataSourceID="sdsBoletasDisponibles">
                            <Columns>
                                <asp:BoundField DataField="boletaID" HeaderText="# boleta" 
                                    InsertVisible="False" ReadOnly="True" SortExpression="boletaID" />
                                <asp:BoundField DataField="Ticket" HeaderText="Ticket" SortExpression="Ticket">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="pesonetosalida" DataFormatString="{0:N2}" 
                                    HeaderText="Peso" SortExpression="pesonetosalida">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FechaEntrada" DataFormatString="{0:dd/MM/yyyy}" 
                                    HeaderText="Fecha" SortExpression="FechaEntrada">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="sdsBoletasDisponibles" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            
SelectCommand="SELECT DISTINCT Boletas.boletaID, Boletas.Ticket, Productos.Nombre, Boletas.pesonetosalida, Boletas.FechaEntrada, ClienteVenta_Boletas.clienteventaID FROM Boletas INNER JOIN ClienteVenta_Boletas ON Boletas.boletaID = ClienteVenta_Boletas.BoletaID INNER JOIN Productos ON Boletas.productoID = Productos.productoID WHERE (ClienteVenta_Boletas.clienteventaID = @clienteventaID) ">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="drpdlClienteDeVenta" Name="clienteventaID" 
                                    PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </asp:Panel>
                    <cc1:CollapsiblePanelExtender ID="pnlBoletas_CollapsiblePanelExtender" 
                        runat="server" CollapseControlID="chkShowBoletas" Collapsed="True" 
                        Enabled="True" ExpandControlID="chkShowBoletas" TargetControlID="pnlBoletas">
                    </cc1:CollapsiblePanelExtender>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" class="TableHeader">
                    FACTURAS DEL CLIENTE DE VENTA</td>
            </tr>
            <tr>
                <td colspan="2" align="center">
            <asp:GridView ID="GridViewFacturas" runat="server" AutoGenerateColumns="False" 
                DataSourceID="sdsFacturas" 
                DataKeyNames="FacturaCV,facturaNo">
                <Columns>
                    <asp:TemplateField HeaderText="FacturaID" InsertVisible="False" 
                        SortExpression="FacturaCV">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("FacturaCV") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" 
                                NavigateUrl='<%# GetURLToOpenFactura(Eval("FacturaCV").ToString()) %>' Text='<%# Eval("FacturaCV") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                        HeaderText="Fecha" SortExpression="fecha" />
                    <asp:BoundField DataField="facturaNo" HeaderText="# Factura" 
                        SortExpression="facturaNo" />
                    <asp:TemplateField HeaderText="Subtotal" SortExpression="subtotal">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("subtotal") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblFooterSubTotal" runat="server" Text="$ 0.00"></asp:Label>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("subtotal", "{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Iva" SortExpression="IVA">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("IVA") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblFooterIVA" runat="server" Text="$ 0.00"></asp:Label>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("IVA", "{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total" SortExpression="total">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("total") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblFooterTOTAL" runat="server" Text="$ 0.00"></asp:Label>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("total", "{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Pagos" DataFormatString="{0:c2}" HeaderText="Pagos" 
                        ReadOnly="True" SortExpression="Pagos">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:CheckBoxField DataField="pagada" HeaderText="Pagada" 
                        SortExpression="pagada" />
                    <asp:BoundField DataField="clienteVentaID" HeaderText="clienteVentaID" 
                        SortExpression="clienteVentaID" Visible="False" />
                    <asp:BoundField DataField="cicloID" HeaderText="cicloID" 
                        SortExpression="cicloID" Visible="False" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsFacturas" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                
                
                
                
                
                
                
                
                
                
                
                
                        
                        
                        SelectCommand="SELECT ClientesVentas.nombre, FacturasClientesVenta.facturaNo, FacturasClientesVenta.fecha, FacturasClientesVenta.subtotal, FacturasClientesVenta.IVA, FacturasClientesVenta.total, FacturasClientesVenta.FacturaCV, FacturasClientesVenta.clienteVentaID, FacturasClientesVenta.cicloID, FacturasClientesVenta.pagada, vListaPagosYTotalesFacVenta.totalPagos AS Pagos FROM FacturasClientesVenta INNER JOIN ClientesVentas ON FacturasClientesVenta.clienteVentaID = ClientesVentas.clienteventaID INNER JOIN vListaPagosYTotalesFacVenta ON FacturasClientesVenta.FacturaCV = vListaPagosYTotalesFacVenta.FacturaCVID WHERE (FacturasClientesVenta.clienteVentaID = @clienteVentaID) ORDER BY ClientesVentas.nombre">
                <SelectParameters>
                    <asp:ControlParameter ControlID="drpdlClienteDeVenta" Name="clienteVentaID" 
                        PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="TableHeader" align="center">
                    PAGOS HECHOS POR EL CLIENTE DE VENTA</td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                                                        <asp:GridView ID="gvPagosFactura" 
                        runat="server" AutoGenerateColumns="False" DataSourceID="sdsPagosFactura" DataKeyNames="pagoFVID" 
                                                            onrowdeleted="gvPagosFactura_RowDeleted" 
                                                            onselectedindexchanged="gvPagosFactura_SelectedIndexChanged" 
                                                            onselectedindexchanging="gvPagosFactura_SelectedIndexChanging">
                                                            <Columns>
                                                                <asp:CommandField ButtonType="Button" DeleteText="Eliminar" 
                                                                    ShowCancelButton="False" ShowDeleteButton="True" SelectText="Modificar" 
                                                                    ShowSelectButton="True" />
                                                                <asp:BoundField DataField="fecha" HeaderText="Fecha" 
                                                                    SortExpression="fecha" DataFormatString="{0:dd/MM/yyyy}">
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="movimientoID" 
                                                                    HeaderText="# Mov. Caja Chica" SortExpression="movimientoID" 
                                                                    InsertVisible="False" ReadOnly="True" />
                                                                <asp:BoundField DataField="MontoCaja" HeaderText="Monto Caja" 
                                                                    SortExpression="MontoCaja" DataFormatString="{0:c2}">
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="movbanID" HeaderText="# Mov. Banco" ReadOnly="True" 
                                                                    SortExpression="movbanID" InsertVisible="False">
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Banco" HeaderText="Banco" SortExpression="Banco" />
                                                                <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" 
                                                                    SortExpression="Cuenta" />
                                                                <asp:BoundField DataField="MontoBanco" DataFormatString="{0:c2}" 
                                                                    HeaderText="Monto Banco" SortExpression="MontoBanco" />
                                                                <asp:BoundField DataField="clienteVentaID" HeaderText="clienteVentaID" 
                                                                    SortExpression="clienteVentaID" Visible="False" />
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:SqlDataSource ID="sdsPagosFactura" runat="server" 
                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                            
                                                            SelectCommand="SELECT DISTINCT PagosFacturasClientesVenta.fecha, MovimientosCaja.movimientoID, MovimientosCaja.abono AS MontoCaja, MovimientosCuentasBanco.movbanID, Bancos.nombre AS Banco, CuentasDeBanco.NumeroDeCuenta AS Cuenta, MovimientosCuentasBanco.abono AS MontoBanco, FacturasClientesVenta.clienteVentaID, PagosFacturasClientesVenta.pagoFVID FROM MovimientosCaja RIGHT OUTER JOIN PagosFacturasClientesVenta INNER JOIN FacturasClientesVenta ON PagosFacturasClientesVenta.FacturaCVID = FacturasClientesVenta.FacturaCV ON MovimientosCaja.movimientoID = PagosFacturasClientesVenta.movCajaID LEFT OUTER JOIN CuentasDeBanco INNER JOIN MovimientosCuentasBanco ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco.cuentaID INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID ON PagosFacturasClientesVenta.movbanID = MovimientosCuentasBanco.movbanID WHERE (FacturasClientesVenta.clienteVentaID = @ClienteVentaId)" 
                                                            DeleteCommand="DELETEPAGOFACTURACLIENTEVENTA" 
                                                            DeleteCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="drpdlClienteDeVenta" Name="ClienteVentaId" 
                                                                    PropertyName="SelectedValue" />
                                                            </SelectParameters>
                                                            <DeleteParameters>
                                                                <asp:Parameter Name="MOVBANID" Type="Int32" />
                                                            </DeleteParameters>
                                                        </asp:SqlDataSource>
                                                        <br />
                                                        <asp:Panel ID="pnlFactsDisponiblesModify" runat="server" Visible="False">
                                                            <asp:GridView ID="gvFacturasDisponiblesModify" runat="server" 
                            AutoGenerateColumns="False" DataKeyNames="FacturaCV" 
                            DataSourceID="sdsFacturasDisponibles">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="CheckBoxSelected0" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="FacturaCV" HeaderText="# factura" 
                                    InsertVisible="False" ReadOnly="True" SortExpression="FacturaCV">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="facturaNo" 
                                    HeaderText="Folio" SortExpression="facturaNo" />
                                                                    <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha" 
                                    SortExpression="fecha"></asp:BoundField>
                                                                    <asp:BoundField DataField="Total" DataFormatString="{0:c2}" HeaderText="Total" 
                                    ReadOnly="True" SortExpression="Total">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="totalPagos" DataFormatString="{0:C2}" 
                                    HeaderText="Pagos" ReadOnly="True" SortExpression="totalPagos">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="TotalPorPagar" DataFormatString="{0:c2}" 
                                    HeaderText="Por Pagar" SortExpression="TotalPorPagar" />
                                                                    <asp:TemplateField HeaderText="Pago">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtPago0" runat="server" 
                                        Text='<%# Eval("TotalPorPagar", "{0:C2}") %>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:Button ID="btnAddAPago" runat="server" Text="Agregar A Pago" />
                                                            <asp:Button ID="btnCancelPagoModify" runat="server" 
                                                                onclick="btnCancelPagoModify_Click" Text="Modificacion Terminada" />
                                                            <br />
                                                            <br />
                                                            <asp:GridView ID="gvFacturasDePago" runat="server" AutoGenerateColumns="False" 
                                                                DataKeyNames="pagoFVID" DataSourceID="sdsFacturasDePago">
                                                                <Columns>
                                                                    <asp:CommandField ButtonType="Button" CancelText="Cancelar" 
                                                                        EditText="Modificar" ShowEditButton="True" UpdateText="Actualizar" />
                                                                    <asp:BoundField DataField="FacturaCV" HeaderText="# factura" 
                                                                        InsertVisible="False" ReadOnly="True" SortExpression="FacturaCV">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="facturaNo" HeaderText="Folio" 
                                                                        SortExpression="facturaNo" />
                                                                    <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                                                        HeaderText="Fecha" SortExpression="fecha" />
                                                                    <asp:BoundField DataField="Total" DataFormatString="{0:c2}" HeaderText="Total" 
                                                                        ReadOnly="True" SortExpression="Total">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="totalPagos" DataFormatString="{0:C2}" 
                                                                        HeaderText="Pagos" ReadOnly="True" SortExpression="totalPagos">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="TotalPorPagar" DataFormatString="{0:c2}" 
                                                                        HeaderText="Por Pagar" SortExpression="TotalPorPagar" />
                                                                    <asp:TemplateField HeaderText="Pago">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox15" runat="server" 
                                                                                Text='<%# Bind("monto", "{0:C2}") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label12" runat="server" Text='<%# Eval("monto", "{0:C2}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:SqlDataSource ID="sdsFacturasDePago" runat="server" 
                                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                SelectCommand="SELECT FacturasClientesVenta.FacturaCV, FacturasClientesVenta.facturaNo, FacturasClientesVenta.fecha, vListaPagosYTotalesFacVenta.Total, vListaPagosYTotalesFacVenta.totalPagos, vListaPagosYTotalesFacVenta.Total - vListaPagosYTotalesFacVenta.totalPagos AS TotalPorPagar, PagosFacturasClientesVenta.monto, PagosFacturasClientesVenta.movbanID, PagosFacturasClientesVenta.pagoFVID FROM FacturasClientesVenta INNER JOIN vListaPagosYTotalesFacVenta ON FacturasClientesVenta.FacturaCV = vListaPagosYTotalesFacVenta.FacturaCVID INNER JOIN PagosFacturasClientesVenta ON FacturasClientesVenta.FacturaCV = PagosFacturasClientesVenta.FacturaCVID WHERE (PagosFacturasClientesVenta.movbanID = (SELECT movbanID FROM PagosFacturasClientesVenta AS PagosFacturasInternal WHERE (pagoFVID = @pagoFVID)))" 
                                                                
                                                                UpdateCommand="UPDATE PagosFacturasClientesVenta SET monto = @monto WHERE (pagoFVID = @pagoFVID)">
                                                                <SelectParameters>
                                                                    <asp:ControlParameter ControlID="gvPagosFactura" Name="pagoFVID" 
                                                                        PropertyName="SelectedValue" />
                                                                </SelectParameters>
                                                                <UpdateParameters>
                                                                    <asp:Parameter Name="monto" />
                                                                    <asp:Parameter Name="pagoFVID" />
                                                                </UpdateParameters>
                                                            </asp:SqlDataSource>
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel2" runat="server">
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlModifyPago" runat="server">
                                                        </asp:Panel>
                </td>
            </tr>
            </table>
            


        <table>
            <tr>
                <td>
                    <asp:Panel ID="pnlPagoResult" runat="server">
                        <asp:Label ID="lblPagoResult" runat="server" Font-Size="X-Large" 
                            Text="EL PAGO FUE:"></asp:Label>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkAddPago" runat="server" CssClass="TablaField" 
                        Text="Agregar nuevo pago" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlAddPago" runat="server">
                        <table>
                            <tr>
                                <td class="TablaField">
                                    Instrucciones:</td>
                            </tr>
                            <tr>
                                <td>
                                    <ul>
                                        <li>Seleccione las facturas que se relacionan con el pago</li>
                                        <li>Por cada factura, seleccione el monto a pagar; por defecto el monto total de la 
                                            factura es seleccionado</li>
                                        <li>Rellene los datos del movimiento de banco.</li>
                                        <li>Agregue el pago.</li>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="gvFacturasDisponibles" runat="server" 
                            AutoGenerateColumns="False" DataKeyNames="FacturaCV" 
                            DataSourceID="sdsFacturasDisponibles">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBoxSelected" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FacturaCV" HeaderText="# factura" 
                                    InsertVisible="False" ReadOnly="True" SortExpression="FacturaCV">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="facturaNo" 
                                    HeaderText="Folio" SortExpression="facturaNo" />
                                <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha" 
                                    SortExpression="fecha">
                                </asp:BoundField>
                                <asp:BoundField DataField="Total" DataFormatString="{0:c2}" HeaderText="Total" 
                                    ReadOnly="True" SortExpression="Total">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="totalPagos" DataFormatString="{0:C2}" 
                                    HeaderText="Pagos" ReadOnly="True" SortExpression="totalPagos">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalPorPagar" DataFormatString="{0:c2}" 
                                    HeaderText="Por Pagar" SortExpression="TotalPorPagar" />
                               <asp:TemplateField HeaderText="Pago">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPago" runat="server" 
                                        Text='<%# Eval("TotalPorPagar", "{0:C2}") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="sdsFacturasDisponibles" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            
                            
                            SelectCommand="SELECT FacturasClientesVenta.FacturaCV, FacturasClientesVenta.facturaNo, FacturasClientesVenta.fecha, ISNULL(vListaPagosYTotalesFacVenta.Total, 0) AS Total, ISNULL(vListaPagosYTotalesFacVenta.totalPagos, 0) AS totalPagos, ISNULL(vListaPagosYTotalesFacVenta.Total - vListaPagosYTotalesFacVenta.totalPagos, 0) AS TotalPorPagar FROM FacturasClientesVenta LEFT OUTER JOIN vListaPagosYTotalesFacVenta ON FacturasClientesVenta.FacturaCV = vListaPagosYTotalesFacVenta.FacturaCVID WHERE (FacturasClientesVenta.clienteVentaID = @clienteVentaId) AND (ISNULL(vListaPagosYTotalesFacVenta.Total - vListaPagosYTotalesFacVenta.totalPagos, 0) &gt; 0)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="drpdlClienteDeVenta" DefaultValue="-1" 
                                    Name="clienteVentaId" PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <br />
                        <table>
                            <tr>
                                <td class="TablaField">
                                    DATOS DEL MOVIMIENTO DE BANCO</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" 
                                        DataKeyNames="movbanID" DataSourceID="sdsMovimientoDeBanco" 
                                        DefaultMode="Insert" Height="50px" oniteminserted="DetailsView1_ItemInserted" 
                                        oniteminserting="DetailsView1_ItemInserting" 
                                        onpageindexchanging="DetailsView1_PageIndexChanging" Width="125px">
                                        <Fields>
                                            <asp:TemplateField HeaderText="Fecha:" SortExpression="fecha">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("fecha") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <InsertItemTemplate>
                                                    <asp:TextBox ID="TextBox8" runat="server" ReadOnly="True" 
                                                        Text='<%# Bind("fecha", "{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                                    <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="TextBox8" 
                                                        Separator="/" />
                                                    <popcalendar id="PopCalendar1" runat="server" control="TextBox1" 
                                                        separator="/" />
                                                </InsertItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("fecha") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombre:" SortExpression="nombre">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("nombre") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <InsertItemTemplate>
                                                    <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("nombre") %>' 
                                                        Width="312px"></asp:TextBox>
                                                </InsertItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("nombre") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="abono" HeaderText="Monto:" SortExpression="cargo" />
                                            <asp:TemplateField HeaderText="Cuenta:" SortExpression="cuentaID">
                                                <EditItemTemplate>
                                                    <asp:Panel ID="Panel1" runat="server">
                                                    </asp:Panel>
                                                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("numCheque") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <InsertItemTemplate>
                                                    <asp:Panel ID="pnlDatosCheque" runat="server">
                                                    </asp:Panel>
                                                    <asp:DropDownList ID="DropDownList2" runat="server" 
                                                        datasourceid="sdsCuentasBanco0" DataTextField="cuenta" 
                                                        DataValueField="cuentaID" SelectedValue='<%# Bind("cuentaID") %>'>
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="sdsCuentasBanco" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                        SelectCommand="SELECT Bancos.nombre + '  ' + CuentasDeBanco.NumeroDeCuenta + ' - ' + CuentasDeBanco.Titular AS cuenta, CuentasDeBanco.cuentaID FROM Bancos INNER JOIN CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID ORDER BY cuenta">
                                                    </asp:SqlDataSource>
                                                </InsertItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("numCheque") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("cuentaID") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <InsertItemTemplate>
                                                    <asp:DropDownList ID="DropDownList3" runat="server" 
                                                        datasourceid="sdsCuentasBanco0" DataTextField="cuenta" 
                                                        DataValueField="cuentaID" SelectedValue='<%# Bind("cuentaID") %>'>
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="sdsCuentasBanco0" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                        SelectCommand="SELECT Bancos.nombre + '  ' + CuentasDeBanco.NumeroDeCuenta + ' - ' + CuentasDeBanco.Titular AS cuenta, CuentasDeBanco.cuentaID FROM Bancos INNER JOIN CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID ORDER BY cuenta">
                                                    </asp:SqlDataSource>
                                                </InsertItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("cuentaID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Catalogo y subcatalogo Fiscal:" 
                                                SortExpression="catalogoMovBancoFiscalID">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox11" runat="server" 
                                                        Text='<%# Bind("catalogoMovBancoFiscalID") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <InsertItemTemplate>
                                                    <asp:DropDownList ID="ddlCatalogoFiscal" runat="server" AutoPostBack="True" 
                                                        DataSourceID="sdsCatalogoFiscal" DataTextField="catalogoMovBanco" 
                                                        DataValueField="catalogoMovBancoID" ondatabound="ddlCatalogoFiscal_DataBound" 
                                                        onselectedindexchanged="ddlCatalogoFiscal_SelectedIndexChanged" 
                                                        SelectedValue='<%# Bind("catalogoMovBancoFiscalID") %>'>
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:SqlDataSource ID="sdsCatalogoFiscal" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                        SelectCommand="SELECT [catalogoMovBancoID], [catalogoMovBanco] FROM [catalogoMovimientosBancos] WHERE ([grupoCatalogoID] = @grupoCatalogoID) ORDER BY [catalogoMovBanco]">
                                                        <SelectParameters>
                                                            <asp:Parameter DefaultValue="14" Name="grupoCatalogoID" Type="Int32" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                    <asp:DropDownList ID="ddlSubCatalogoFiscal" runat="server" 
                                                        DataSourceID="sdsSubcatalogoFiscal" DataTextField="subCatalogo" 
                                                        DataValueField="subCatalogoMovBancoID">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="sdsSubcatalogoFiscal" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                        SelectCommand="SELECT subCatalogoMovBancoID, subCatalogo, catalogoMovBancoID FROM SubCatalogoMovimientoBanco ORDER BY subCatalogo">
                                                    </asp:SqlDataSource>
                                                </InsertItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" 
                                                        Text='<%# Bind("catalogoMovBancoFiscalID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Concepto:" SortExpression="ConceptoMovCuentaID">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox12" runat="server" 
                                                        Text='<%# Bind("ConceptoMovCuentaID") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <InsertItemTemplate>
                                                    <asp:DropDownList ID="ddlConcepto" runat="server" AutoPostBack="True" 
                                                        DataSourceID="sdsConceptos" DataTextField="Concepto" 
                                                        DataValueField="ConceptoMovCuentaID" ondatabound="ddlConcepto_DataBound" 
                                                        onselectedindexchanged="ddlConcepto_SelectedIndexChanged" 
                                                        SelectedValue='<%# Bind("ConceptoMovCuentaID") %>'>
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="sdsConceptos" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                        SelectCommand="SELECT ConceptoMovCuentaID, Concepto FROM ConceptosMovCuentas WHERE (ConceptoMovCuentaID = 3) OR(ConceptoMovCuentaID = 1) ORDER BY Concepto">
                                                    </asp:SqlDataSource>
                                                </InsertItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label10" runat="server" 
                                                        Text='<%# Bind("ConceptoMovCuentaID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="numCheque">
                                                <InsertItemTemplate>
                                                    <asp:Panel ID="pnlDatosCheque0" runat="server">
                                                        <table>
                                                            <tr>
                                                                <td class="TablaField">
                                                                    Cheque:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtChequeNum" runat="server" Text='<%# Eval("numCheque") %>'></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TablaField">
                                                                    Nombre:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtChequeNombre" runat="server" Width="313px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </InsertItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="catalogoMovBancoInternoID">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <InsertItemTemplate>
                                                    <asp:CheckBox ID="chkChangeSubCat" runat="server" CssClass="TablaField" 
                                                        Text="Los Catalogos Internos son diferentes" />
                                                    <asp:Panel ID="pnlCatInternos" runat="server">
                                                        <table>
                                                            <tr>
                                                                <td class="TablaField">
                                                                    Grupo:</td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlGrupoInterno" runat="server" AutoPostBack="True" 
                                                                        DataSourceID="sdsGruposInternos" DataTextField="grupoCatalogo" 
                                                                        DataValueField="grupoCatalogosID" ondatabound="ddlGrupoInterno_DataBound" 
                                                                        onselectedindexchanged="ddlGrupoInterno_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:SqlDataSource ID="sdsGruposInternos" runat="server" 
                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                        SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos] ORDER BY [grupoCatalogo]">
                                                                    </asp:SqlDataSource>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TablaField">
                                                                    Catalogo Interno:</td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlCatalogoInterno" runat="server" AutoPostBack="True" 
                                                                        DataSourceID="sdsCatalogosInterno" DataTextField="catalogoMovBanco" 
                                                                        DataValueField="catalogoMovBancoID" ondatabound="ddlCatalogoInterno_DataBound" 
                                                                        onselectedindexchanged="ddlCatalogoInterno_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:SqlDataSource ID="sdsCatalogosInterno" runat="server" 
                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                        SelectCommand="SELECT [catalogoMovBancoID], [catalogoMovBanco], [grupoCatalogoID] FROM [catalogoMovimientosBancos] ORDER BY [catalogoMovBanco]">
                                                                    </asp:SqlDataSource>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TablaField">
                                                                    Sub-Catalogo:</td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlSubCatInterno" runat="server" 
                                                                        DataSourceID="sdsSubCatalogoInterno" DataTextField="subCatalogo" 
                                                                        DataValueField="subCatalogoMovBancoID">
                                                                    </asp:DropDownList>
                                                                    <asp:SqlDataSource ID="sdsSubCatalogoInterno" runat="server" 
                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                        SelectCommand="SELECT [subCatalogoMovBancoID], [subCatalogo], [catalogoMovBancoID] FROM [SubCatalogoMovimientoBanco] ORDER BY [subCatalogo]">
                                                                    </asp:SqlDataSource>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <cc1:CollapsiblePanelExtender ID="pnlCatInternos_CollapsiblePanelExtender" 
                                                        runat="server" CollapseControlID="chkChangeSubCat" Collapsed="True" 
                                                        Enabled="True" ExpandControlID="chkChangeSubCat" 
                                                        TargetControlID="pnlCatInternos">
                                                    </cc1:CollapsiblePanelExtender>
                                                </InsertItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label11" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Observaciones" SortExpression="Observaciones">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Observaciones") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <InsertItemTemplate>
                                                    <asp:TextBox ID="TextBox14" runat="server" Height="88px" 
                                                        Text='<%# Bind("Observaciones") %>' TextMode="MultiLine" Width="340px"></asp:TextBox>
                                                </InsertItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("Observaciones") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ButtonType="Button" InsertText="Agregar" 
                                                ShowInsertButton="True" />
                                        </Fields>
                                    </asp:DetailsView>
                                    <asp:SqlDataSource ID="sdsMovimientoDeBanco" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        DeleteCommand="DELETE FROM [MovimientosCuentasBanco] WHERE [movbanID] = @movbanID" 
                                        InsertCommand="ADDMOVBANCO" InsertCommandType="StoredProcedure" 
                                        SelectCommand="SELECT [fecha], [nombre], [cargo], [ConceptoMovCuentaID], [cuentaID], [catalogoMovBancoFiscalID], [subCatalogoMovBancoFiscalID], [numCheque], [chequeNombre], [catalogoMovBancoInternoID], [subCatalogoMovBancoInternoID], [Observaciones], [movbanID], [abono] FROM [MovimientosCuentasBanco] WHERE ([movbanID] = @movbanID)" 
                                        
                                        UpdateCommand="UPDATE [MovimientosCuentasBanco] SET [fecha] = @fecha, [nombre] = @nombre, [cargo] = @cargo, [ConceptoMovCuentaID] = @ConceptoMovCuentaID, [cuentaID] = @cuentaID, [catalogoMovBancoFiscalID] = @catalogoMovBancoFiscalID, [subCatalogoMovBancoFiscalID] = @subCatalogoMovBancoFiscalID, [numCheque] = @numCheque, [chequeNombre] = @chequeNombre, [catalogoMovBancoInternoID] = @catalogoMovBancoInternoID, [subCatalogoMovBancoInternoID] = @subCatalogoMovBancoInternoID, [Observaciones] = @Observaciones WHERE [movbanID] = @movbanID" 
                                        oninserted="sdsMovimientoDeBanco_Inserted">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="gvPagosFactura" Name="movbanID" 
                                                PropertyName="SelectedValue" Type="Int32" />
                                        </SelectParameters>
                                        <DeleteParameters>
                                            <asp:Parameter Name="movbanID" Type="Int32" />
                                        </DeleteParameters>
                                        <UpdateParameters>
                                            <asp:Parameter Name="fecha" Type="DateTime" />
                                            <asp:Parameter Name="nombre" Type="String" />
                                            <asp:Parameter Name="cargo" Type="Decimal" />
                                            <asp:Parameter Name="ConceptoMovCuentaID" Type="Int32" />
                                            <asp:Parameter Name="cuentaID" Type="Int32" />
                                            <asp:Parameter Name="catalogoMovBancoFiscalID" Type="Int32" />
                                            <asp:Parameter Name="subCatalogoMovBancoFiscalID" Type="Int32" />
                                            <asp:Parameter Name="numCheque" Type="Int32" />
                                            <asp:Parameter Name="chequeNombre" Type="String" />
                                            <asp:Parameter Name="catalogoMovBancoInternoID" Type="Int32" />
                                            <asp:Parameter Name="subCatalogoMovBancoInternoID" Type="Int32" />
                                            <asp:Parameter Name="Observaciones" Type="String" />
                                            <asp:Parameter Name="movbanID" Type="Int32" />
                                        </UpdateParameters>
                                        <InsertParameters>
                                            <asp:Parameter Name="fecha" Type="DateTime" />
                                            <asp:Parameter Name="nombre" Type="String" />
                                            <asp:Parameter Name="cargo" Type="Decimal" />
                                            <asp:Parameter Name="ConceptoMovCuentaID" Type="Int32" />
                                            <asp:Parameter Name="cuentaID" Type="Int32" />
                                            <asp:Parameter Name="catalogoMovBancoFiscalID" Type="Int32" />
                                            <asp:Parameter Name="subCatalogoMovBancoFiscalID" Type="Int32" />
                                            <asp:Parameter Name="numCheque" Type="Int32" />
                                            <asp:Parameter Name="chequeNombre" Type="String" />
                                            <asp:Parameter Name="catalogoMovBancoInternoID" Type="Int32" />
                                            <asp:Parameter Name="subCatalogoMovBancoInternoID" Type="Int32" />
                                            <asp:Parameter Name="Observaciones" Type="String" />
                                            <asp:Parameter Name="userID" />
                                            <asp:Parameter Name="fechaCheque" />
                                            <asp:Parameter Name="abono" />
                                            <asp:Parameter Name="fechacobrado" />
                                            <asp:Parameter Direction="Output" Name="newID" Type="Int32" />
                                        </InsertParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    
                    <cc1:CollapsiblePanelExtender ID="pnlAddPago_CollapsiblePanelExtender" 
                        runat="server" CollapseControlID="chkAddPago" Collapsed="True" Enabled="True" 
                        ExpandControlID="chkAddPago" TargetControlID="pnlAddPago">
                    </cc1:CollapsiblePanelExtender>
                    
                </td>
            </tr>
        </table>
            
</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>


 
