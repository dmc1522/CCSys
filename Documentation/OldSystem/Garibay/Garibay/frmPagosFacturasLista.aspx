<%@ Page Title="Lista de Pagos a Facturas de Venta" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmPagosFacturasLista.aspx.cs" Inherits="Garibay.frmPagosFacturasLista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
	<tr>
		<td class="TableHeader">PAGOS RELACIONADOS A FACTURAS</td>
	</tr>
	<tr><td>Solo se muestran los pagos que estan relacionados a una factura.</td></tr>
	<tr><td>
        <asp:HyperLink ID="HyperLink2" runat="server" 
            NavigateUrl="~/frmPagoAFacturas.aspx">Agregar Nuevo Pago a Facturas</asp:HyperLink></td></tr>
</table>
<table>
    <tr>
    <td class="TablaField">Ciclo:</td><td>
        <asp:DropDownList ID="ddlCiclos" runat="server" AutoPostBack="True" 
            DataSourceID="sdsCiclos" DataTextField="CicloName" DataValueField="cicloID" 
            onselectedindexchanged="ddlCiclos_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:SqlDataSource ID="sdsCiclos" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
            SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [CicloName] DESC">
        </asp:SqlDataSource>
    </td>
    </tr>
</table>
    <asp:GridView ID="gvPagos" runat="server" DataSourceID="sdsPagosFacturas" 
        AutoGenerateColumns="False" DataKeyNames="movbanID">
        <Columns>
            <asp:TemplateField HeaderText="ID" InsertVisible="False" 
                SortExpression="movbanID">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" 
                        NavigateUrl='<%# GetURL(Eval("movbanID").ToString()) %>' Text='<%# Eval("movbanID") %>'></asp:HyperLink>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("movbanID") %>'></asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="nombre" HeaderText="Nombre" 
                SortExpression="nombre" />
            <asp:BoundField DataField="NumeroDeCuenta" HeaderText="# Cuenta" 
                SortExpression="NumeroDeCuenta" />
            <asp:BoundField DataField="Titular" HeaderText="Titular" 
                SortExpression="Titular" />
            <asp:BoundField DataField="abono" DataFormatString="{0:C2}" HeaderText="Monto" 
                SortExpression="abono">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="Facts" HeaderText="Facturas Relacionadas" 
                ReadOnly="True" SortExpression="Facts" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsPagosFacturas" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        SelectCommand="SELECT DISTINCT MovimientosCuentasBanco.movbanID, Bancos.nombre, CuentasDeBanco.NumeroDeCuenta, CuentasDeBanco.Titular, MovimientosCuentasBanco.abono, dbo.udf_update_concat(MovimientosCuentasBanco.movbanID) AS Facts FROM PagosFacturasClientesVenta INNER JOIN MovimientosCuentasBanco ON PagosFacturasClientesVenta.movbanID = MovimientosCuentasBanco.movbanID INNER JOIN CuentasDeBanco ON MovimientosCuentasBanco.cuentaID = CuentasDeBanco.cuentaID INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID LEFT OUTER JOIN FacturasClientesVenta ON PagosFacturasClientesVenta.FacturaCVID = FacturasClientesVenta.FacturaCV WHERE (FacturasClientesVenta.cicloID = @cicloID) ORDER BY MovimientosCuentasBanco.movbanID DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCiclos" Name="cicloID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
