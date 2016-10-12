<%@ Page Language="C#" Theme="skinrojo" MasterPageFile = "~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListEntradaProductos.aspx.cs" Inherits="Garibay.WebForm6" Title="Entrada de Productos" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:Panel ID="panelmensaje" runat="server" > 
        <table>
            <tr>
                <td style="text-align: center">
                           
                           <asp:Image ID="imagenbien" runat="server" ImageUrl="~/imagenes/palomita.jpg" 
                               Visible="False" />
                           <asp:Image ID="imagenmal" runat="server" ImageUrl="~/imagenes/tache.jpg" 
                               Visible="False" />
                           <asp:Label ID="lblMensajetitle" runat="server" SkinID="lblMensajeTitle" 
                               Text="PRUEBA"></asp:Label>
                       </td>
            </tr>
            <tr>
                <td style="text-align: center">
                           <asp:Label ID="lblMensajeOperationresult" runat="server"  Text="Label" 
                               SkinID="lblMensajeOperationresult"></asp:Label>
                       </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblMensajeException" runat="server" SkinID="lblMensajeException" 
                        Text="SI NO HAY EXC BORREN EL TEXTO"></asp:Label>
                </td>
            </tr>
        </table>

        
      
        </asp:Panel>


    <table>
	<tr align="center">
		<td class="TableHeader">
		ENTRADA DE PRODUCTOS
		</td>
	</tr>
	<tr>
		<td>
		<asp:Panel ID="pnlfiltros" runat="server" GroupingText="Filtrar Resultados" 
                Width="100%">
		    <table>
		    	<tr>
		    		
		    		<td class="TablaField" width="100px">
		    		    Producto :</td>
		    		<td>
		    		<asp:DropDownList ID="drpdlProducto" runat="server" Width="240px" 
                            DataSourceID="SqlDataSource3" DataTextField="Producto" 
                            DataValueField="productoID" ondatabound="drpdlProducto_DataBound">
                    </asp:DropDownList>
		    		</td>
		    		<td class="TablaField" >
		    		    Bodega :
		    		</td>
		    		<td width="100px">
		    		    <asp:DropDownList ID="drpdlbodega" runat="server" DataSourceID="SqlDataSource4" 
                            DataTextField="bodega" DataValueField="bodegaID" ondatabound="drpdlbodega_DataBound" 
                            
                            Width="150px">
                        </asp:DropDownList>
		    		</td>
		    		<td class="TablaField">
		    		    T. de Movimiento :
		    		</td>
		    		<td>
		    		    <asp:DropDownList ID="drpdlTipoMovimientoP" runat="server" 
                            ondatabound="drpdlTipoMovimientoP_DataBound" Width="150px" 
                            DataSourceID="SqlDataSource5" DataTextField="tipoMovimiento" 
                            DataValueField="tipoMovProdID">
                        </asp:DropDownList>
		    		</td>
		    	</tr>
		    	<tr>
		    	
		    	    <td colspan="6">
		    	    <table>
		    	    	<tr>
		    	    		<td class="TableHeader" width="50px">
		    	    		    Grupo:
		    		</td>
		    		<td>
		    		    <asp:DropDownList ID="ddlGrupo" runat="server" DataSourceID="sdsGrupoProductos" 
                            DataTextField="grupo" DataValueField="grupoID" ondatabound="ddlGrupo_DataBound">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsGrupoProductos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            SelectCommand="SELECT [grupoID], [grupo] FROM [productoGrupos] ORDER BY [grupo]">
                        </asp:SqlDataSource>
		    		</td>
		    		<td class="TableHeader">
		    		    Fecha:</td>
		    		<td class="TablaField" width="50px" >
		    		    De:</td>
		    		<td>
		    		    <asp:TextBox ID="txtFechainicio" runat="server" ReadOnly="True" Width="150px"></asp:TextBox>
                        <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtFechainicio" 
                            onselectionchanged="click_selectionChanged" Separator="/" style="width: 14px" />
                       
		    		</td>
		    		<td>
		    		    A:</td>
		    		<td >
		    		    <asp:TextBox ID="txtFechafin" runat="server" ReadOnly="True" Width="150px"></asp:TextBox>
                            </td>
		    		<td>
		    		
                        
		    		    <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtFechafin" 
                            onselectionchanged="click_selectionChanged1" Separator="/" 
                            style="width: 14px" />
                            </td>
		    	    </tr>
		    	    </table>
		    				</td>
		    	    	
		    	</tr>
		    	<tr>
		    	
		    	<td colspan="2">
		    	<table>
		    		
		    	
		    	<tr>
		    	
		    	    <td class="TablaField">
		    	    Elementos por Página :
		    	    </td>
		    	    <td>
		    	        <asp:DropDownList ID="drpdlElemXpag" runat="server" Width="100px">
                            <asp:ListItem>1000</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                            <asp:ListItem Selected="True">200</asp:ListItem>
                            <asp:ListItem>500</asp:ListItem>
                        </asp:DropDownList>
		    	    </td>
		    	</tr>
		    	</table>
		    	</td>
		    	</tr>
		    	<tr>
		    		<td colspan="6" align="center">
		    		     <asp:Button ID="btnfiltrar" runat="server" Text="Filtrar Resultados" 
                             CssClass="Button" onclick="btnfiltrar_Click"/>
		  		         <asp:Button ID="btnLimpiarFiltros" runat="server" 
                             onclick="btnLimpiarFiltros_Click" Text="Limpiar Filtros" />
		  		    </td>
		    		
		    	</tr>
		    </table>
		    
            </asp:Panel>
		</td>
	</tr>
	<tr>
		<td>
		
		    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="Button" 
                onclick="btnAgregar_Click" />
            <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="Button" 
                onclick="btnModificar_Click" />
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="Button" 
                onclick="btnEliminar_Click" />
		
		    <asp:Button ID="btnImprimeList" runat="server" onclick="btnImprimeList_Click" 
                Text="Imprimir Lista" Visible="False" />
		
		    <asp:Button ID="btnExportaraExcel" runat="server" 
                onclick="btnExportaraExcel_Click" Text="Exportar a Excel" />
		
		</td>
	</tr>
		</table>
	
		
		    <asp:GridView ID="grdvListEntPro" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" ForeColor="Black" 
                CellPadding="4" GridLines="None" DataKeyNames="entradaprodID,Nombre,bodega,tipoMovimiento,Fecha,cantidad,observaciones,storeTS,updateTS,CicloName,cicloID,productoID,bodegaID" 
        DataSourceID="SqlDataSource2" 
         onselectedindexchanged="grdvListEntPro_SelectedIndexChanged" 
         PageSize="100">
                <AlternatingRowStyle BackColor="White" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <HeaderStyle CssClass="TableHeader" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                
                <Columns>
                    <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                        ShowSelectButton="True" />
                    <asp:BoundField HeaderText="No." DataField="entradaprodID" InsertVisible="False" 
                        ReadOnly="True" SortExpression="entradaprodID"> 
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Producto" DataField="Nombre" 
                        SortExpression="Nombre">
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Bodega" DataField="bodega" SortExpression="bodega">
                    </asp:BoundField>
                    <asp:BoundField DataField="GrupoAndPresentacion" 
                        HeaderText="Grupo-Presentacion" SortExpression="GrupoAndPresentacion" />
                    <asp:BoundField HeaderText="Tipo de Movimiento" DataField="tipoMovimiento" 
                        SortExpression="tipoMovimiento">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Fecha" DataField="Fecha" SortExpression="Fecha" 
                        DataFormatString="{0:dd/MM/yyyy}">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Cantidad" DataField="cantidad" 
                        SortExpression="cantidad" DataFormatString="{0:n}">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Precio" DataFormatString="{0:c}" HeaderText="Precio" 
                        SortExpression="Precio" />
                    <asp:BoundField HeaderText="Observaciones" DataField="observaciones" 
                        SortExpression="observaciones">
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Usuario" DataField="Nombre" SortExpression="Nombre">
                    </asp:BoundField>
                    <asp:BoundField DataField="CicloName" HeaderText="Ciclo" 
                        SortExpression="CicloName" />
                    <asp:BoundField DataField="storeTS" HeaderText="Insertado en:" 
                        SortExpression="storeTS" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" >
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="updateTS" HeaderText="Modificado en:" 
                        SortExpression="updateTS" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" >
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cicloID" HeaderText="cicloID" 
                        SortExpression="cicloID" Visible="False" />
                    <asp:BoundField DataField="productoID" HeaderText="productoID" 
                        SortExpression="productoID" Visible="False" />
                    <asp:BoundField DataField="bodegaID" HeaderText="bodegaID" 
                        SortExpression="bodegaID" Visible="False" />
                </Columns>
  
            </asp:GridView>
		<asp:SqlDataSource ID="SqlDataSource3" runat="server" 
         ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
         
         SelectCommand="SELECT Productos.productoID, Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Producto, Productos.productoGrupoID FROM Productos INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID ORDER BY Productos.productoGrupoID, Productos.Nombre">
     </asp:SqlDataSource>
		<asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        
         
         
         
         
         SelectCommand="SELECT EntradaDeProductos.entradaprodID, Productos.Nombre, Bodegas.bodega, TiposMovimientoProducto.tipoMovimiento, EntradaDeProductos.Fecha, EntradaDeProductos.cantidad, EntradaDeProductos.observaciones, Users.Nombre AS Expr1, EntradaDeProductos.storeTS, EntradaDeProductos.updateTS, Ciclos.CicloName, EntradaDeProductos.cicloID, EntradaDeProductos.productoID, EntradaDeProductos.bodegaID, EntradaDeProductos.preciocompra AS Precio, productoGrupos.grupo + ' - ' + Presentaciones.Presentacion AS GrupoAndPresentacion, productoGrupos.grupoID FROM EntradaDeProductos INNER JOIN Productos ON EntradaDeProductos.productoID = Productos.productoID INNER JOIN Bodegas ON EntradaDeProductos.bodegaID = Bodegas.bodegaID INNER JOIN Users ON EntradaDeProductos.userID = Users.userID INNER JOIN TiposMovimientoProducto ON EntradaDeProductos.tipoMovProdID = TiposMovimientoProducto.tipoMovProdID INNER JOIN Ciclos ON EntradaDeProductos.cicloID = Ciclos.cicloID INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID INNER JOIN productoGrupos ON Productos.productoGrupoID = productoGrupos.grupoID ORDER BY EntradaDeProductos.Fecha DESC">
    </asp:SqlDataSource>
		<asp:SqlDataSource ID="SqlDataSource4" runat="server" 
         ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
         SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas]">
     </asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
         ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
         
         SelectCommand="SELECT [tipoMovProdID], [tipoMovimiento] FROM [TiposMovimientoProducto]">
     </asp:SqlDataSource>
		</asp:Content>
