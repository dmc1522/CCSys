<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master" Title="Factura de compra" CodeBehind="frmFacturasDeCompra.aspx.cs" Inherits="Garibay.frmFacturasDeCompra" %>

<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="/scripts/divFunctions.js"></script>
    <script type="text/javascript" src="/scripts/prototype.js"></script>

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
                    <asp:HyperLink ID="hlnkAbrir" runat="server" Visible="False">HyperLink</asp:HyperLink>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table>
        <tr>
        <td class="TableHeader" colspan="2">
            FACTURA DE COMPRA:&nbsp;<asp:Label ID="lblFacturaId" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
	        <td Class="TablaField">
	            Proveedor:
		        </td>
	        <td>
	        <asp:DropDownList ID="ddlProveedor" runat="server" DataSourceID="SqlProveedores" 
			        DataTextField="Nombre" DataValueField="proveedorID">
        	
	        </asp:DropDownList>
		        <asp:SqlDataSource ID="SqlProveedores" runat="server" 
			        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
			        SelectCommand="SELECT [proveedorID], [Nombre] FROM [Proveedores] ORDER BY [Nombre]">
		        </asp:SqlDataSource>
	        </td>	
        </tr>
	    <tr>
		    <td class="TablaField">
		        Ciclo:
		    </td>
		    <td>
		    <asp:DropDownList ID="ddlCiclos" runat="server" DataSourceID="sqlCiclos" 
				    DataTextField="CicloName" DataValueField="cicloID">
    		
		    </asp:DropDownList>
			    <asp:SqlDataSource ID="sqlCiclos" runat="server" 
				    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
    				
				    SelectCommand="SELECT cicloID, CicloName FROM Ciclos WHERE (cerrado = @cerrado) order by CicloName DESC">
				    <SelectParameters>
					    <asp:Parameter DefaultValue="False" Name="cerrado" />
				    </SelectParameters>
			    </asp:SqlDataSource>
		    </td>
	    </tr>
	    <tr>
	    <td class="TablaField">
	        Fecha:
	    </td>
	    <td>
		    <asp:TextBox ID="txtFecha" runat="server" ReadOnly="true"></asp:TextBox>
		    <rjs:PopCalendar ID="PopCalendar3" runat="server" Separator="/" 
                            Control="txtFecha" />
	    </td>
	    </tr>
	    <tr>
	    <td class="TablaField">
	        Folio :
	    </td>
	    <td>
		    <asp:TextBox ID="txtFolio" runat="server"></asp:TextBox>
	    </td>	
    </tr>

	    <tr>
	    <td class="TablaField">
	        Moneda:</td>
	    <td>
		    <asp:DropDownList ID="ddlMoneda" runat="server" DataSourceID="sdsMoneda" 
                DataTextField="Moneda" DataValueField="tipomonedaID">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sdsMoneda" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                SelectCommand="SELECT [tipomonedaID], [Moneda] FROM [TiposDeMoneda] ORDER BY [Moneda] DESC">
            </asp:SqlDataSource>
	    </td>	
    </tr>

	    <tr>
	    <td class="TablaField">
	        Observaciones:</td>
	    <td>
		    <asp:TextBox ID="txtObservaciones" runat="server" Height="50px" 
                TextMode="MultiLine" Width="531px"></asp:TextBox>
	    </td>	
    </tr>

	    <tr>
	    <td class="TablaField" colspan="2">
		    <asp:Button ID="btnAddFacturaCompra" runat="server" 
			    Text="Agregar factura" Height="22px" 
                Width="228px" onclick="btnAddOrdenEntrada_Click" />
                <asp:Button ID="btnModifyFacturaCompra" runat="server" Height="22px" 
                    Text="Guardar factura" Width="234px" 
                onclick="btnModifyFacturaCompra_Click" Visible="False" />
	    </td>
    </tr>

	</table>
	
                                       <asp:UpdateProgress ID="ProgressProductos" runat="server" 
                                           AssociatedUpdatePanelID="pnlCentral" 
        DisplayAfter="0">
                                            <ProgressTemplate>
                                                <img alt="" src="imagenes/cargando.gif" />PROCESANDO DATOS...
                                            </ProgressTemplate>
                                       </asp:UpdateProgress>
	
	<asp:UpdatePanel runat="Server" id="pnlCentral" Visible="False">
	<ContentTemplate>
	        <table >
		        <tr>
			        <td> 
                        <asp:CheckBox ID="chkNewProducto" runat="server" 
                            Text="Agregar nuevo producto a factura" /></td>
		        </tr>
	        </table>
	        <asp:Panel ID="pnlAddProd" runat="server" Visible="false">
	            <table>
                                                        <tr>
                                                        <td align="center">
                                                                    <table>
                                                                        <tr>
                                                                            <td class="TablaField">Bodega:</td>
                                                                            <td>
                                                                                <asp:DropDownList ID="drpdlBodega" runat="server" DataSourceID="sdsBodegas" 
                                                                                    DataTextField="bodega" DataValueField="bodegaID" Height="24px" Width="179px">
                                                                                </asp:DropDownList>
                                                                                <asp:SqlDataSource ID="sdsBodegas" runat="server" 
                                                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                    SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
                                                                                </asp:SqlDataSource>
                                                                            </td>
                                                                            <td class="TablaField">Grupo:</td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlGrupos" runat="server" AutoPostBack="True" 
                                                                                    DataSourceID="sqlGrupos" DataTextField="grupo" DataValueField="grupoID">
                                                                                </asp:DropDownList>
                                                                                <asp:SqlDataSource ID="sqlGrupos" runat="server" 
                                                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                    
																			        SelectCommand="SELECT [grupoID], [grupo] FROM [productoGrupos] WHERE ([grupoID] &lt;&gt; @grupoID)">
                                                                        	        <SelectParameters>
																				        <asp:Parameter DefaultValue="1" Name="grupoID" Type="Int32" />
																			        </SelectParameters>
                                                                                </asp:SqlDataSource>
                                                                            </td>
                                                                            <td class="TablaField">
                                                                                Producto:</td>
                                                                            <td>
                                                                                <asp:DropDownList ID="drpdlProducto" runat="server" DataSourceID="sqlProductos" 
                                                                                    DataTextField="Nombre" DataValueField="productoID">
                                                                                </asp:DropDownList>
                                                                                <asp:SqlDataSource ID="sqlProductos" runat="server" 
                                                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                    SelectCommand="SELECT Productos.productoID, Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Nombre FROM Productos INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID Where Productos.productoGrupoID = @grupoID ORDER BY Nombre ">
                                                                                    <SelectParameters>
                                                                                        <asp:ControlParameter ControlID="ddlGrupos" DefaultValue="-1" Name="grupoID" 
                                                                                            PropertyName="SelectedValue" />
                                                                                    </SelectParameters>
                                                                                </asp:SqlDataSource>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TablaField">Cantidad:</td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
                                                                            </td>
                                                                             <td class="TablaField">Precio:</td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtPrecio" runat="server"></asp:TextBox>
                                                                            </td>
                                                                           
                                                                            
                                                                            <td colspan="2">
                                                                                <asp:Button ID="btnAddproduct" runat="server" CausesValidation="False" 
																			        Text="Agregar Producto" onclick="btnAddproduct_Click" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" colspan="6">
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                    </table>
                                                             </td>
                                                        </tr>
                </table>
            </asp:Panel>
        												
            <cc1:CollapsiblePanelExtender ID="pnlAddProd_CollapsiblePanelExtender" 
                runat="server" CollapseControlID="chkNewProducto" Collapsed="True" 
                Enabled="True" ExpandControlID="chkNewProducto" TargetControlID="pnlAddProd">
            </cc1:CollapsiblePanelExtender>
        												
        <table>
	        <tr>
		        <td>
		        PRODUCTOS RECIBIDOS DEL PROVEEDOR
		        </td>
        	
        		
	            <td rowspan="2">
                    <table>
                        <tr>
                            <td class="TableField">
                                Subtotal:</td>
                            <td>
                                <asp:TextBox ID="TextBoxSubtotal" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TableField">
                                Iva:</td>
                            <td>
                                <asp:TextBox ID="TextBoxIva" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TableField">
                                Total:</td>
                            <td>
                                <asp:TextBox ID="TextBoxTotal" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TableField">
                                Descuento:</td>
                            <td>
                                <asp:TextBox ID="TextBoxDescuento" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
        	
        		
	        </tr>
	        <tr>
		        <td>
        		
		        <asp:GridView ID="grdvProductosRecibidos" runat="server" 
				        AutoGenerateColumns="False" DataKeyNames="facturaProveedordetalleID" 
                        DataSourceID="sdsDetalle" onrowupdated="grdvProductosRecibidos_RowUpdated" 
                        onrowupdating="grdvProductosRecibidos_RowUpdating">
		            <Columns>
                        <asp:CommandField ButtonType="Button" CancelText="Cancelar" 
                            EditText="Modificar" ShowEditButton="True" DeleteText="Eliminar" 
                            ShowDeleteButton="True" />
                        <asp:BoundField DataField="facturaProveedordetalleID" HeaderText="ID" 
                            ReadOnly="True" SortExpression="facturaProveedordetalleID" Visible="False" />
                        <asp:TemplateField HeaderText="Bodega" SortExpression="bodegaID">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList1" runat="server" 
                                    DataSourceID="sdsBodegasEdit" DataTextField="bodega" DataValueField="bodegaID" 
                                    SelectedValue='<%# Bind("bodegaID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsBodegasEdit" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
                                </asp:SqlDataSource>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("bodega") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Producto" SortExpression="productoID">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList2" runat="server" 
                                    DataSourceID="sdsProductosEdit" DataTextField="Producto" 
                                    DataValueField="productoID" SelectedValue='<%# Bind("productoID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsProductosEdit" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT [productoID], [Producto] FROM [vProductosParaDDL] ORDER BY [Producto]">
                                </asp:SqlDataSource>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Producto") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="precio" DataFormatString="{0:c2}" 
                            HeaderText="Precio" SortExpression="precio">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cantidad" DataFormatString="{0:n2}" 
                            HeaderText="Cantidad" SortExpression="cantidad">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Importe" DataFormatString="{0:c2}" 
                            HeaderText="Importe" SortExpression="Importe" ReadOnly="True">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="facturaid" HeaderText="facturaid" 
                            SortExpression="facturaid" Visible="False" />
                    </Columns>
		        </asp:GridView>
		            <asp:SqlDataSource ID="sdsDetalle" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        DeleteCommand="gspFacturaDeProveedorDetalle_DELETE" 
                        DeleteCommandType="StoredProcedure" 
                        SelectCommand="SELECT * FROM [vFacturaProveedorDetalles] WHERE ([facturaid] = @facturaid)" 
                        UpdateCommand="gspFacturaDeProveedorDetalle_UPDATE" 
                        UpdateCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="lblFacturaId" DefaultValue="-1" 
                                Name="facturaid" PropertyName="Text" Type="Int32" />
                        </SelectParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="facturaProveedordetalleID" Type="Int32" />
                        </DeleteParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="facturaProveedordetalleID" Type="Int32" />
                            <asp:Parameter Name="facturaid" Type="Int32" />
                            <asp:Parameter Name="productoID" Type="Int32" />
                            <asp:Parameter Name="precio" Type="Double" />
                            <asp:Parameter Name="cantidad" Type="Double" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
		        </td>
	        </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>

	</ContentTemplate>
	</asp:UpdatePanel>


</asp:Content>
