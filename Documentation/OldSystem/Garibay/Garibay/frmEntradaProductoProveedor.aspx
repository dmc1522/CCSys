<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmEntradaProductoProveedor.aspx.cs" Inherits="Garibay.Formulario_web15" %>
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
                        Text="SI NO HAY EXC BORREN EL TEXTO"></asp:Label><asp:HyperLink ID="hlnkAbrir" runat="server">HyperLink</asp:HyperLink>
                </td>
            </tr>
        </table>

        
      
        </asp:Panel>
	<table>
<tr>
<td class="TableHeader" colspan="2">
ORDEN DE ENTRADA DE PRODUCTO
</td>
</tr>
<tr>
	<td Class="TablaField">
	PROVEEDOR:
		<asp:TextBox ID="txtFolioActual" runat="server" Visible="False"></asp:TextBox>
	</td>
	<td>
	<asp:DropDownList ID="ddlProveedor" runat="server" DataSourceID="SqlProveedores" 
			DataTextField="Nombre" DataValueField="proveedorID">
	
	</asp:DropDownList>
		<asp:SqlDataSource ID="SqlProveedores" runat="server" 
			ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
			SelectCommand="SELECT [proveedorID], [Nombre] FROM [Proveedores]">
		</asp:SqlDataSource>
	</td>	
</tr>
	<tr>
		<td class="TablaField">
		CICLO:
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
	Fecha
	</td>
	<td>
		<asp:TextBox ID="txtFecha" runat="server" ReadOnly="true"></asp:TextBox>
		<rjs:PopCalendar ID="PopCalendar3" runat="server" Separator="/" 
                        Control="txtFecha" />
	</td>
	</tr>
	<tr>
	<td class="TablaField">
	FOLIO :
	</td>
	<td>
		<asp:TextBox ID="txtFolio" runat="server"></asp:TextBox>
	</td>	
</tr>

	<tr>
	<td class="TablaField">
		<asp:Label ID="lblOrdenID" runat="server" Text=""></asp:Label>
		<asp:TextBox ID="txtOrdenID" runat="server" Visible="false"></asp:TextBox>
	
	</td>
	
	</tr>
	<tr>
	<td align="right"">
		<asp:Button ID="btnAddOrdenEntrada" runat="server" 
			Text="Agregar Orden de Entrada" onclick="btnAddOrdenEntrada_Click" />
			<asp:Button ID="btnModifyOrdenEntrada" runat="server" 
			Text="Modificar Orden de Entrada" onclick="btnModifyOrdenEntrada_Click"/>
	</td>
	</tr>
</table>
	<asp:Panel ID="pnlAddProd" runat="server" Visible="false">
	<table>
	        <tr>
                                                <td align="center" class="TableHeader">
                                                <asp:CheckBox ID="chkPnlAddProd" runat="server" 
                                                     Text="Mostrar panel para agregar producto (No Fertilizante a Granel)" 
														CssClass="TableField" />
                                                </td></tr>
                                                <tr>
                                                <td align="center">
                                                        <asp:Panel ID="Panel1" runat="Server">
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
                                                                            DataTextField="Nombre" DataValueField="productoID" Width="150px">
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
                                                                   
                                                                    
                                                                    <td>
                                                                        <asp:Button ID="btnAddproduct" runat="server" CausesValidation="False" 
																			Text="Agregar Producto" onclick="btnAddproduct_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" colspan="6">
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <cc1:CollapsiblePanelExtender ID="pnlAddProd_CollapsiblePanelExtender" 
                                                            runat="server" CollapseControlID="chkPnlAddProd" Collapsed="True" 
                                                            Enabled="True" ExpandControlID="chkPnlAddProd" TargetControlID="Panel1">
                                                        </cc1:CollapsiblePanelExtender>
                                                    </td>
                                                </tr>
</table>

												<table>
                                                <tr>
                                                 <td align="center" class="TableHeader">
												<asp:CheckBox ID="chkAgregarBoletas" runat="server" Text="Mostrar Agregar Boletas a La Orden de Entrada" Visible="true"/>
												<cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" ExpandControlID="chkAgregarBoletas" TargetControlID="pnlBoletas"
                                                 CollapseControlID="chkAgregarBoletas" Collapsed="True" 
                                                            Enabled="True">
												</cc1:CollapsiblePanelExtender>
                                                 </td>
													</tr>
													<tr>
                                                    <td align="center">
                                                        <table>
                                                            <tr>
																<td align="center">
                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Panel ID="pnlBoletas" runat="server">
                                                                    <div ID="divboletas" runat="Server">
											                 <table>
											           <%--      <tr>
                                                <td class="TablaField" align="right">
                                                    PRODUCTOR:</td>
                                                <td>
                                                <br />
                                                    <asp:DropDownList ID="ddlNewBoletaProductor" runat="server" 
                                                        DataSourceID="sdsNewBoletaProductor" DataTextField="Productor" 
                                                        DataValueField="productorID" Height="23px" Width="211px">
                                                    </asp:DropDownList>
                                                    <cc1:ListSearchExtender ID="ddlNewBoletaProductor_ListSearchExtender" 
                                                        runat="server" Enabled="True" PromptText="Escriba para buscar" 
                                                        TargetControlID="ddlNewBoletaProductor">
                                                    </cc1:ListSearchExtender>
                                                    <asp:SqlDataSource ID="sdsNewBoletaProductor" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                        SelectCommand="SELECT productorID, apaterno + ' ' + amaterno + ' ' + nombre AS Productor FROM Productores ORDER BY Productor">
                                                    </asp:SqlDataSource>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td class="TablaField" align="right">
                                                    # BOLETO
                                                    <br />
                                                    DE BASCULA:</td>
                                                <td>
                                                    <asp:TextBox ID="txtNewNumBoleta" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TablaField" align="right">
                                                    # DE FOLIO:</td>
                                                <td>
                                                    <asp:TextBox ID="txtNewTicket" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TablaField" align="right">
                                                    PRODUCTO:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlNewBoletaProducto" runat="server" 
                                                        DataSourceID="sdsNewBoletaProductos" DataTextField="Expr1" 
                                                        DataValueField="productoID">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="sdsNewBoletaProductos" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                        
													    SelectCommand="SELECT Productos.productoID, Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Expr1 FROM Productos INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID ORDER BY Productos.Nombre">
                                                    </asp:SqlDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="TablaField">
                                                    BODEGA:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlNewBoletaBodega" runat="server" 
                                                        DataSourceID="sdsNewBoletaBodega" DataTextField="bodega" 
                                                        DataValueField="bodegaID">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="sdsNewBoletaBodega" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                        SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
                                                    </asp:SqlDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TablaField" align="right">
                                                    FECHA ENTRADA:</td>
                                                <td>
                                                    <asp:TextBox ID="txtNewFechaEntrada" runat="server"></asp:TextBox>
                                                    <rjs:PopCalendar ID="PopCalendar1" runat="server"  Control="txtNewFechaEntrada" 
                                                        Separator="/"/>
                                                    <br />
                                                    <asp:CheckBox ID="chkChangeFechaSalidaNewBoleta" runat="server" 
                                                        Text="Fecha Salida es Diferente a la de Entrada" />
                                                    <div ID="divFechaSalidaNewBoleta" runat="Server">
                                                        <br />
                                                        FECHA SALIDA:
                                                        <asp:TextBox ID="txtNewFechaSalida" runat="server" ReadOnly="True"></asp:TextBox>
                                                        <rjs:PopCalendar ID="PopCalendar4" runat="server" Control="txtNewFechaSalida" 
                                                            Separator="/" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TablaField" align="right">
                                                    PESO BRUTO:</td>
                                                <td>
                                                    <asp:TextBox ID="txtNewPesoEntrada" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TablaField" align="right">
                                                    PESO TARA:</td>
                                                <td>
                                                    <asp:TextBox ID="txtNewPesoSalida" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TablaField" align="right">
                                                    PESO NETO:</td>
                                                <td>
                                                    <asp:TextBox ID="txtPesoNetoNewBoleta" runat="server" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TablaField" align="right">
                                                    HUMEDAD:</td>
                                                <td>
                                                    <asp:TextBox ID="txtNewHumedad" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TablaField" align="right">
                                                    IMPUREZAS:</td>
                                                <td>
                                                    <asp:TextBox ID="txtNewImpurezas" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TablaField" align="right">
                                                    PRECIO:</td>
                                                <td>
                                                    <asp:TextBox ID="txtNewPrecio" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TablaField" align="right">
                                                    SECADO:</td>
                                                <td>
                                                    <asp:TextBox ID="txtNewSecado" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
											                 </table>
    											             
                                                                                                   
                                                                                                </div>
                                       <asp:Button ID="btnAgregarBoleta" runat="server" Text="Agregar Boleta a la Nota" CausesValidation="False" 
                                                                            Width="214px" onclick="btnAgregarBoleta_Click" />
                               
                                   </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                
                                                </table>
<table>
	<tr>
		<td>
		PRODUCTOS RECIBIDOS DEL PROVEEDOR
		</td>
	
		
	</tr>
	<tr>
		<td>
		
		<asp:GridView ID="grdvProductosRecibidos" runat="server" 
				AutoGenerateColumns="False" DataSourceID="SqlProductosRecibidos" 
				DataKeyNames="ordenDetalleID" onrowdeleting="grdvProductosRecibidos_RowDeleting">
			<Columns>
				<asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
				<asp:BoundField DataField="grupo" HeaderText="Grupo" SortExpression="grupo" />
				<asp:BoundField DataField="ordenDetalleID" HeaderText="ordenDetalleID" 
					SortExpression="ordenDetalleID" InsertVisible="False" ReadOnly="True" Visible="False" />
				<asp:BoundField DataField="Nombre" HeaderText="Nombre" 
					SortExpression="Nombre" />
				<asp:BoundField DataField="Presentacion" HeaderText="Presentacion" 
					SortExpression="Presentacion" />
				<asp:BoundField DataField="cantidad" HeaderText="Cantidad" 
					SortExpression="cantidad" DataFormatString="{0:n2}">
					<ItemStyle HorizontalAlign="Right" />
				</asp:BoundField>
				<asp:BoundField DataField="importe" DataFormatString="{0:c2}" 
					HeaderText="Importe" SortExpression="importe" >
					<ItemStyle HorizontalAlign="Right" />
				</asp:BoundField>
			</Columns>
		</asp:GridView>
			<asp:SqlDataSource ID="SqlProductosRecibidos" runat="server" 
				ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
				
				
				
				SelectCommand="SELECT productoGrupos.grupo, Orden_de_entrada_detalle.ordenDetalleID, Productos.Nombre, Presentaciones.Presentacion, Orden_de_entrada_detalle.cantidad, Orden_de_entrada_detalle.importe FROM Orden_de_entrada INNER JOIN Orden_de_entrada_detalle ON Orden_de_entrada.ordenID = Orden_de_entrada_detalle.ordenID INNER JOIN Productos ON Productos.productoID = Orden_de_entrada_detalle.productoID INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID INNER JOIN productoGrupos ON Productos.productoGrupoID = productoGrupos.grupoID WHERE (Orden_de_entrada.ordenID = @OrdenID) order by productoGrupos.grupo" 
				
				
				DeleteCommand="DELETE FROM Orden_de_entrada_detalle WHERE ordenDetalleID = @ordenDetalleID">
				<SelectParameters>
					<asp:ControlParameter ControlID="txtOrdenID" DefaultValue="-1" 
						Name="OrdenID" PropertyName="Text" />
				</SelectParameters>
				<DeleteParameters>
					<asp:Parameter Name="ordenDetalleID" />
				</DeleteParameters>
			</asp:SqlDataSource>
		</td>
	</tr>
</table>

	</asp:Panel>


</asp:Content>
