<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmReporteBoletasDeSalida.aspx.cs" Inherits="Garibay.frmReporteBoletasDeSalida" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
	<tr>
		<td>CICLO:</td><td>
            <asp:DropDownList ID="ddlCiclo" runat="server" DataSourceID="sdsCiclos" 
                DataTextField="CicloName" DataValueField="cicloID">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [fechaInicio] DESC">
            </asp:SqlDataSource>
        </td>
	</tr>
</table>

    <asp:GridView ID="gvReporte" runat="server" AutoGenerateColumns="False" 
        DataSourceID="sdsReporte">
        <Columns>
            <asp:BoundField DataField="boletaID" HeaderText="boletaID" 
                InsertVisible="False" ReadOnly="True" SortExpression="boletaID" />
            <asp:BoundField DataField="cicloID" HeaderText="cicloID" 
                SortExpression="cicloID" />
            <asp:BoundField DataField="Ticket" HeaderText="Ticket" 
                SortExpression="Ticket" />
            <asp:BoundField DataField="clienteventaID" HeaderText="clienteventaID" 
                InsertVisible="False" ReadOnly="True" SortExpression="clienteventaID" />
            <asp:BoundField DataField="nombre" HeaderText="nombre" 
                SortExpression="nombre" />
            <asp:BoundField DataField="Producto" HeaderText="Producto" 
                SortExpression="Producto" />
            <asp:BoundField DataField="FechaEntrada" HeaderText="FechaEntrada" 
                SortExpression="FechaEntrada" />
            <asp:BoundField DataField="PesoDeEntrada" HeaderText="PesoDeEntrada" 
                SortExpression="PesoDeEntrada" />
            <asp:BoundField DataField="PesoDeSalida" HeaderText="PesoDeSalida" 
                SortExpression="PesoDeSalida" />
            <asp:BoundField DataField="pesonetosalida" HeaderText="pesonetosalida" 
                SortExpression="pesonetosalida" />
            <asp:BoundField DataField="Factura" HeaderText="Factura" ReadOnly="True" 
                SortExpression="Factura" />
        </Columns>
    </asp:GridView>


    <asp:SqlDataSource ID="sdsReporte" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        SelectCommand="SELECT Boletas.boletaID, Boletas.cicloID, Boletas.Ticket, ClientesVentas.clienteventaID, ClientesVentas.nombre, Productos.Nombre AS Producto, Boletas.FechaEntrada, Boletas.PesoDeEntrada, Boletas.PesoDeSalida, Boletas.pesonetosalida, 'ID: ' + CONVERT (varchar(255), FacturasClientesVenta.FacturaCV) + ' Folio: ' + FacturasClientesVenta.facturaNo AS Factura FROM Boletas INNER JOIN ClienteVenta_Boletas ON Boletas.boletaID = ClienteVenta_Boletas.BoletaID INNER JOIN ClientesVentas ON ClienteVenta_Boletas.clienteventaID = ClientesVentas.clienteventaID INNER JOIN FacturasCV_Boletas ON Boletas.boletaID = FacturasCV_Boletas.boletaID INNER JOIN FacturasClientesVenta ON FacturasCV_Boletas.FacturaCV = FacturasClientesVenta.FacturaCV INNER JOIN Productos ON Boletas.productoID = Productos.productoID WHERE (Boletas.cicloID = @cicloID)">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCiclo" Name="cicloID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>


</asp:Content>
