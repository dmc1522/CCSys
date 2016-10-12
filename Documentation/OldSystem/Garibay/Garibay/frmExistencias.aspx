<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmExistencias.aspx.cs" Inherits="Garibay.frmExistencias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table border="0">
	<tr>
		<td colspan="2" align="center" class="TableHeader">EXISTENCIA POR PRODUCTO POR CICLO</td>
	</tr>
	<tr>
		<td colspan="2">Solo se consideran las existencias agregadas por facturas de compra</td>
	</tr>
	<tr>
		<td class="TablaField">CICLO:</td>
		<td>
            <asp:DropDownList ID="ddlCiclos" runat="server" DataSourceID="sdsCiclos" 
                DataTextField="CicloName" DataValueField="cicloID">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [fechaInicio] DESC">
            </asp:SqlDataSource>
        </td>
	</tr>
	<tr>
		<td colspan="2">
		    <asp:GridView ID="gvExistencias0" runat="server" AutoGenerateColumns="False" 
    DataSourceID="sdsExistenciasDeFacturasCompra" ondatabound="gvExistencias_DataBound">
    <Columns>
        <asp:BoundField DataField="bodega" HeaderText="Bodega" 
            SortExpression="bodega" />
        <asp:BoundField DataField="Nombre" HeaderText="Producto" 
            SortExpression="Nombre" />
        <asp:BoundField DataField="Existencia" DataFormatString="{0:n}" 
            HeaderText="Existencia" ReadOnly="True" SortExpression="Existencia">
        <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:BoundField DataField="Unidad" HeaderText="Unidad" 
            SortExpression="Unidad" />
        <asp:BoundField DataField="Presentacion" HeaderText="Presentacion" 
            SortExpression="Presentacion" />
    </Columns>
</asp:GridView>
            <asp:SqlDataSource ID="sdsExistenciasDeFacturasCompra" runat="server">
            </asp:SqlDataSource>
        </td>
	</tr>
</table>

<table >
	<tr>
		<td>
		
		<table>
			<tr>
				<td class="TableHeader" colspan="2" align="center">FILTRAR</td>
			</tr>
			<tr>
			    <td class="TablaField">Producto:</td>
			    <td>
                    <asp:DropDownList ID="ddlProducto" runat="server" AutoPostBack="True" 
                        DataSourceID="sdsProductos" DataTextField="Producto" 
						DataValueField="productoID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsProductos" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
						SelectCommand="SELECT Productos.productoID, Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Producto, Productos.productoGrupoID FROM Productos INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID ORDER BY  Productos.productoGrupoID,Productos.Nombre">
                    </asp:SqlDataSource>
                </td>
			</tr>
		</table>
		
		</td>
	</tr>
	<tr>
	    <td>EXISTENCIAS TOTALES POR PRODUCTO</td>
	</tr>
	<tr>
		<td>
		    <asp:GridView ID="gvExistencias" runat="server" AutoGenerateColumns="False" 
    DataSourceID="sdsExistencias" ondatabound="gvExistencias_DataBound">
    <Columns>
        <asp:BoundField DataField="bodega" HeaderText="Bodega" 
            SortExpression="bodega" />
        <asp:BoundField DataField="Nombre" HeaderText="Producto" 
            SortExpression="Nombre" />
        <asp:BoundField DataField="Existencia" DataFormatString="{0:n}" 
            HeaderText="Existencia" ReadOnly="True" SortExpression="Existencia">
        <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:BoundField DataField="Unidad" HeaderText="Unidad" 
            SortExpression="Unidad" />
        <asp:BoundField DataField="Presentacion" HeaderText="Presentacion" 
            SortExpression="Presentacion" />
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="sdsExistencias" runat="server" 
    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
    
        
                SelectCommand="SELECT * FROM ExistenciasView WHERE (productoID = @productoID)
">
    <SelectParameters>
        <asp:ControlParameter ControlID="ddlProducto" DefaultValue="-1" 
            Name="productoID" PropertyName="SelectedValue" />
    </SelectParameters>
</asp:SqlDataSource>

		</td>
	</tr>
</table>
<br />
EXISTENCIAS TOTALES SIN CICLO:
 <asp:GridView ID="gvExistenciaTotal" runat="server" AutoGenerateColumns="False" 
    DataSourceID="sdsExistenciasTotal">
    <Columns>
        <asp:BoundField DataField="bodega" HeaderText="Bodega" 
            SortExpression="bodega" />
        <asp:BoundField DataField="Nombre" HeaderText="Producto" 
            SortExpression="Nombre" />
        <asp:BoundField DataField="Existencia" DataFormatString="{0:n}" 
            HeaderText="Existencia" ReadOnly="True" SortExpression="Existencia">
        <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:BoundField DataField="Presentacion" HeaderText="Presentacion" 
            SortExpression="Presentacion" />
        <asp:BoundField DataField="Unidad" HeaderText="Unidad" 
            SortExpression="Unidad" />
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="sdsExistenciasTotal" runat="server" 
    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
    
        
                
        
        SelectCommand="SELECT bodega, Nombre, entrada, Salida, Existencia, bodegaID, productoID, Presentacion, Unidad FROM ExistenciasView ORDER BY bodega, Nombre">
</asp:SqlDataSource>
</asp:Content>
