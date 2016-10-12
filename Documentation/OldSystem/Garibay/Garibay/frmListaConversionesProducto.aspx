<%@ Page Title="Lista de Conversiones de Producto" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="True" CodeBehind="frmListaConversionesProducto.aspx.cs" Inherits="Garibay.frmListaConversionesProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td class="TablaField">LISTA DE CONVERSIONES DE PRODUCTO</td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvConversiones" runat="server" AutoGenerateColumns="False" 
                DataSourceID="sdsListaConversion" DataKeyNames="conversionproductoID" 
                onrowdeleted="gvConversiones_RowDeleted" 
                onrowdeleting="gvConversiones_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                        HeaderText="Fecha" SortExpression="fecha" />
                    <asp:BoundField DataField="Producto_Salida" HeaderText="Producto_Salida" 
                        SortExpression="Producto_Salida" />
                    <asp:BoundField DataField="Cantidad_de_Salida" HeaderText="Cantidad_de_Salida" 
                        SortExpression="Cantidad_de_Salida">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Producto_Entrada" HeaderText="Producto_Entrada" 
                        SortExpression="Producto_Entrada" />
                    <asp:BoundField DataField="Cantidad_de_Entrada" 
                        HeaderText="Cantidad_de_Entrada" SortExpression="Cantidad_de_Entrada">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:CommandField DeleteText="Eliminar" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsListaConversion" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                
                SelectCommand="SELECT ConversionProducto.conversionproductoID, ConversionProducto.fecha, EntradaDeProductos.entradaprodID, Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Producto_Entrada, EntradaDeProductos.cantidad AS Cantidad_de_Entrada, SalidaDeProductos.salidaprodID, Productos_1.Nombre + ' - ' + Presentaciones_1.Presentacion AS Producto_Salida, SalidaDeProductos.cantidad AS Cantidad_de_Salida FROM ConversionProducto INNER JOIN EntradaDeProductos ON ConversionProducto.entradaID = EntradaDeProductos.entradaprodID INNER JOIN Productos ON EntradaDeProductos.productoID = Productos.productoID INNER JOIN SalidaDeProductos ON ConversionProducto.salidaID = SalidaDeProductos.salidaprodID INNER JOIN Productos AS Productos_1 ON SalidaDeProductos.productoID = Productos_1.productoID INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID INNER JOIN Presentaciones AS Presentaciones_1 ON Productos_1.presentacionID = Presentaciones_1.presentacionID ORDER BY ConversionProducto.fecha DESC" 
                
                DeleteCommand="SET NOCOUNT ON;DELETE FROM EntradaDeProductos WHERE entradaprodID IN (SELECT entradaid FROM conversionproducto WHERE ConversionProducto.conversionproductoID = @conversionproductoID); DELETE FROM SalidaDeProductos WHERE salidaprodID IN (SELECT salidaID FROM conversionproducto WHERE ConversionProducto.conversionproductoID = @conversionproductoID); SET NOCOUNT OFF; DELETE FROM ConversionProducto FROM ConversionProducto WHERE (ConversionProducto.conversionproductoID = @conversionproductoID);">
                <DeleteParameters>
                    <asp:Parameter Name="conversionproductoID" />
                </DeleteParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblResultMsg" runat="server" Text=""></asp:Label>
            </td>
    </tr>
</table>
   
</asp:Content>
