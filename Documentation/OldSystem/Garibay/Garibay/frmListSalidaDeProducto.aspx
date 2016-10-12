<%@ Page Language="C#" Theme="skinrojo" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListSalidaDeProducto.aspx.cs" Inherits="Garibay.frmListSalidaDeProducto" Title="Salida de productos" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
 	<tr>
 		<td colspan="6" class="TableHeader">LISTA DE SALIDA DE PRODUCTOS</td>
 	</tr>
 	<tr> 
 	 <td>
 	  <asp:Panel id="panelFiltros" runat="Server" GroupingText="Filtrar resultados:" 
             Width="877px">
 	    <table>
 	          <tr>
 	 <td class="TablaField"> 
 	     Producto:</td>
 	 <td>
         <asp:DropDownList ID="cmbProducto" runat="server" AutoPostBack="True" 
             DataSourceID="sdsProductos" DataTextField="Nombre" DataValueField="productoID" 
             Height="22px" Width="200px">
         </asp:DropDownList>
         <asp:SqlDataSource ID="sdsProductos" runat="server" 
             ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
             SelectCommand="SELECT DISTINCT Productos.productoID, Productos.Nombre, Productos.productoGrupoID FROM Productos INNER JOIN SalidaDeProductos ON Productos.productoID = SalidaDeProductos.productoID where SalidaDeProductos.bodegaId = @bodegaId order by Productos.productoGrupoID, Productos.nombre ">
             <SelectParameters>
                 <asp:ControlParameter ControlID="cmbBodega" Name="bodegaId" 
                     PropertyName="SelectedValue" />
             </SelectParameters>
         </asp:SqlDataSource>
        </td>
 	 <td class="TablaField">Bodega:</td>
 	              <td>
                      <asp:DropDownList ID="cmbBodega" runat="server" AutoPostBack="True" 
                          DataSourceID="sdsBodegas" DataTextField="bodega" DataValueField="bodegaID" 
                          Height="22px" Width="200px">
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="sdsBodegas" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                          SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
                      </asp:SqlDataSource>
                  </td>
 	</tr>
 	          <tr>
                  <td class="TablaField">
                      &nbsp;<asp:CheckBox ID="chkTipo" runat="server" Text="Por tipo de salida:" />
                  </td>
                  <td>
                      <asp:DropDownList ID="ddlTipodeSalida" runat="server" 
                          DataSourceID="sdsTipodeSalida" DataTextField="tipoMovimiento" 
                          DataValueField="tipoMovProdID" Height="22px" Width="200px">
                      </asp:DropDownList>
                      <asp:SqlDataSource ID="sdsTipodeSalida" runat="server" 
                          ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                          SelectCommand="SELECT [tipoMovProdID], [tipoMovimiento] FROM [TiposMovimientoProducto]">
                      </asp:SqlDataSource>
                  </td>
                  <td class="TablaField">
                      <asp:CheckBox ID="chkBodega" runat="server" Text="Por fecha. De:" />
                  </td>
                  <td>
                      <asp:TextBox ID="txtFechaDe" runat="server" ReadOnly="True" Width="173px"></asp:TextBox>
                      <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtFechaDe" />
                  </td>
                  <td class="TablaField">
                      A:</td>
                  <td>
                      <asp:TextBox ID="txtFechaA" runat="server" ReadOnly="True"></asp:TextBox>
                      <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtFechaA" />
                  </td>
              </tr>
 	<tr>
 	 <td colspan="2">
         <asp:Button ID="btnFiltrar" runat="server" CssClass="Button" 
             onclick="btnFiltrar_Click" Text="Filtrar" Width="156px" />
         <asp:Button ID="btnAgregarSalida" runat="server" CssClass="Button" 
             Text="Agregar Salida de Producto" Width="192px" 
             onclick="btnAgregarSalida_Click" />
        </td>
 	</tr>
 	      </table>
 	  </asp:Panel>
          
 	 </td>
 	</tr>
 	 
 	     
 	<tr>
 	 <td colspan="6">
 	 
 	     <asp:GridView ID="gridSalidaProducto" runat="server" AllowPaging="True" 
             AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
             ForeColor="Black" GridLines="None" DataKeyNames="salidaprodID" 
             DataSourceID="sdsListadeProductos" PageSize="50">
               <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
               <HeaderStyle CssClass="TableHeader" />
               <AlternatingRowStyle BackColor="White" />
               <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
             <Columns>
                 <asp:BoundField HeaderText="# Salida" DataField="salidaprodID" 
                     InsertVisible="False" ReadOnly="True" SortExpression="salidaprodID" />
                 <asp:BoundField HeaderText="Bodega" DataField="bodega" 
                     SortExpression="bodega" />
                 <asp:BoundField HeaderText="Fecha" DataField="Fecha" 
                     DataFormatString="{0:dd/MM/yyyy}" SortExpression="Fecha" />
                 <asp:BoundField HeaderText="Tipo de Movimiento" DataField="tipoMovimiento" 
                     SortExpression="tipoMovimiento" />
                 <asp:BoundField HeaderText="Producto" DataField="Nombre" 
                     SortExpression="Nombre" />
                 <asp:BoundField HeaderText="Cantidad" DataField="cantidad" 
                     DataFormatString="{0:c2}" SortExpression="cantidad" >
                 <ItemStyle HorizontalAlign="Right" />
                 </asp:BoundField>
                 <asp:BoundField HeaderText="Observaciones" DataField="observaciones" 
                     SortExpression="observaciones" />
             </Columns>
         </asp:GridView>
 	 
 	     <asp:SqlDataSource ID="sdsListadeProductos" runat="server" 
             ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
             SelectCommand="SELECT        dbo.SalidaDeProductos.salidaprodID, dbo.SalidaDeProductos.Fecha, dbo.TiposMovimientoProducto.tipoMovimiento, dbo.Productos.Nombre, dbo.Bodegas.bodega, 
                         dbo.SalidaDeProductos.cantidad, dbo.SalidaDeProductos.observaciones
FROM            dbo.SalidaDeProductos INNER JOIN
                         dbo.Productos ON dbo.SalidaDeProductos.productoID = dbo.Productos.productoID INNER JOIN
                         dbo.TiposMovimientoProducto ON dbo.SalidaDeProductos.tipoMovProdID = dbo.TiposMovimientoProducto.tipoMovProdID INNER JOIN
                         dbo.Bodegas ON dbo.SalidaDeProductos.bodegaID = dbo.Bodegas.bodegaID where SalidaDeProductos. productoId= @productoId and SalidaDeProductos.bodegaId = @bodegaId">
             <SelectParameters>
                 <asp:ControlParameter ControlID="cmbProducto" Name="productoId" 
                     PropertyName="SelectedValue" />
                 <asp:ControlParameter ControlID="cmbBodega" Name="bodegaId" 
                     PropertyName="SelectedValue" />
             </SelectParameters>
         </asp:SqlDataSource>
 	 
 	 </td>
 	</tr>
 </table>
</asp:Content>
