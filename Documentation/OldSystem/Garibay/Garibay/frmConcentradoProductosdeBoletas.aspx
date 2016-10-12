<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" Title="Concentrado por producto de boletas" AutoEventWireup="true" CodeBehind="frmConcentradoProductosdeBoletas.aspx.cs" Inherits="Garibay.frmConcentradoProductosdeBoletas" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

         
        <table >
            <tr>
                <td class="TablaField">
                    MOSTRAR BOLETAS DEL CICLO: </td>
                <td>
                    <asp:DropDownList ID="drpdlCiclo" runat="server" AutoPostBack="True" 
                        DataSourceID="sdsCiclos" DataTextField="CicloName" DataValueField="cicloID" 
                        Height="23px" onselectedindexchanged="drpdlCiclo_SelectedIndexChanged" 
                        Width="274px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos]">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader" colspan="2">
                    REPORTE DE BOLETAS POR GRUPO DE PRODUCTOS </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gridEntrada" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="sdsBoletasEntrada" ondatabound="gridEntrada_DataBound">
                        <RowStyle Font-Size="X-Large" />
                        <Columns>
                            <asp:BoundField DataField="grupo" HeaderText="Grupo" 
                                SortExpression="grupo" />
                            <asp:BoundField DataField="Producto" HeaderText="Producto" 
                                SortExpression="Producto" />
                            <asp:BoundField DataField="totalentrada" DataFormatString="{0:n}" 
                                HeaderText="Total en boletas de entrada (KG)" ReadOnly="True" 
                                SortExpression="totalentrada" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="totalsalida" DataFormatString="{0:n}" 
                                HeaderText="Total en boletas de salida (KG)" ReadOnly="True" 
                                SortExpression="totalsalida" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Totalisimo" DataFormatString="{0:n}" 
                                HeaderText="Total (KG)" ReadOnly="True" SortExpression="Totalisimo" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle Font-Size="X-Large" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsBoletasEntrada" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
                        SelectCommand="SELECT productoGrupos.grupo, Productos.Nombre + ' - ' + Presentaciones.Presentacion + ' - ' + Unidades.Unidad AS Producto, SUM(Boletas.pesonetoentrada) AS totalentrada, SUM(Boletas.pesonetosalida) AS totalsalida, SUM(Boletas.pesonetoentrada) - SUM(Boletas.pesonetosalida) AS Totalisimo FROM Boletas INNER JOIN Productos ON Boletas.productoID = Productos.productoID INNER JOIN productoGrupos ON Productos.productoGrupoID = productoGrupos.grupoID INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID INNER JOIN Unidades ON Productos.unidadID = Unidades.unidadID WHERE (Boletas.cicloID = @cicloID) GROUP BY Productos.Nombre, productoGrupos.grupo, Unidades.Unidad, Presentaciones.Presentacion ORDER BY Productos.Nombre">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="drpdlCiclo" DefaultValue="-1" Name="cicloID" 
                                PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" class="TableHeader">
                    REPORT DE BOLETAS POR GRUPO&nbsp; DE PRODUCTOS Y PRODUCTOR</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gridBoletasPorProductor" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="sdsBoletasPorProductor" 
                        ondatabound="gridBoletasPorProductor_DataBound">
                        <RowStyle Font-Size="X-Large" />
                        <Columns>
                            <asp:BoundField DataField="grupo" HeaderText="Grupo" 
                                SortExpression="grupo" />
                            <asp:BoundField DataField="Productor" HeaderText="Productor" 
                                SortExpression="Productor" />
                            <asp:BoundField DataField="Producto" HeaderText="Producto" 
                                SortExpression="Producto" />
                            <asp:BoundField DataField="totalentrada" DataFormatString="{0:n}" 
                                HeaderText="Total en boletas de entrada (KG)" ReadOnly="True" 
                                SortExpression="totalentrada" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="totalsalida" DataFormatString="{0:n}" 
                                HeaderText="Total en boletas de salida (KG)" ReadOnly="True" 
                                SortExpression="totalsalida" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Totalisimo" DataFormatString="{0:n}" 
                                HeaderText="Total (KG)" ReadOnly="True" SortExpression="Totalisimo" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle Font-Size="X-Large" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsBoletasPorProductor" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        SelectCommand="SELECT DISTINCT productoGrupos.grupo, Boletas.productorID, (SELECT apaterno + ' ' + ' ' + amaterno + ' ' + nombre AS Productor FROM Productores WHERE (productorID = Boletas.productorID)) AS Productor, Productos.Nombre + ' - ' + Presentaciones.Presentacion + ' - ' + Unidades.Unidad AS Producto, SUM(Boletas.pesonetoentrada) AS totalentrada, SUM(Boletas.pesonetosalida) AS totalsalida, SUM(Boletas.pesonetoentrada) - SUM(Boletas.pesonetosalida) AS Totalisimo FROM Boletas INNER JOIN Productos ON Boletas.productoID = Productos.productoID INNER JOIN productoGrupos ON Productos.productoGrupoID = productoGrupos.grupoID INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID INNER JOIN Unidades ON Productos.unidadID = Unidades.unidadID WHERE (Boletas.cicloID = @cicloID) GROUP BY productoGrupos.grupo, Productos.Nombre, Unidades.Unidad, Presentaciones.Presentacion, Boletas.productorID ORDER BY productoGrupos.grupo, productor">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="4" Name="cicloID" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            </table>


</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    </asp:Content>
