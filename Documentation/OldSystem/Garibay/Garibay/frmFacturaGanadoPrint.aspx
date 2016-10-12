<%@ Page Title="Liquidaciones de Ganado" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="True" CodeBehind="frmFacturaGanadoPrint.aspx.cs" Inherits="Garibay.frmFacturaGanadoPrint" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="Server" id="UpdatePanelContent">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td><img alt="Logo Garibay" src="imagenes/Garibay.gif" 
                        style="height: 104px; width: 185px" />  </td>
                <td valign="top">
                    <table>
            	        <tr><td>Av. Patria ote. #10</td></tr>
            	        <tr><td>Ameca, Jalisco  46600</td></tr>
            	        <tr><td>Tel. 01375 7581199 Fax 01375 7580262</td></tr>
                    </table>
                </td>
                <td valign="top">
                    <table>
            	        <tr>
            		        <td class="TablaField">Folio:</td>
            	        </tr>
            	        <tr>
            		        <td>
                                <asp:TextBox ID="txtFolio" runat="server" ReadOnly="True" Width="80px"></asp:TextBox>
                            </td>
            	        </tr>
                        <tr>
                            <td>
                                FECHA:</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtFecha" runat="server" Width="113px"></asp:TextBox>
                                <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtFecha" 
                                    Separator="/" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td valign="top">
                    &nbsp;</td>
                <td align="right" valign="top">
                    
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Panel ID="pnlProveedor" runat="server" GroupingText="DATOS DEL PROVEEDOR">
                        <table>
                    	    <tr>
                    		    <td class="TablaField">PROVEEDOR:</td>
                    	        <td>
                                    <asp:DropDownList ID="ddlProveedores" runat="server" AutoPostBack="True" 
                                        DataSourceID="sdsProveedores" DataTextField="Nombre" 
                                        DataValueField="ganProveedorID" 
                                        onselectedindexchanged="ddlProveedores_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsProveedores" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        SelectCommand="SELECT [ganProveedorID], [Nombre] FROM [gan_Proveedores] ORDER BY [Nombre]">
                                    </asp:SqlDataSource>
                                </td>
                    	    </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:DetailsView ID="dvProveedorData" runat="server" AutoGenerateRows="False" 
                                        DataSourceID="sdsProveedorData" Height="50px" Width="125px">
                                        <Fields>
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" 
                                                SortExpression="Nombre" >
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="direccion" HeaderText="Direccion" 
                                                SortExpression="direccion" >
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ciudad" HeaderText="Ciudad" 
                                                SortExpression="ciudad" >
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="estado" HeaderText="Estado" 
                                                SortExpression="estado" >
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RFC" HeaderText="RFC" SortExpression="RFC" >
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                        </Fields>
                                    </asp:DetailsView>
                                    <asp:SqlDataSource ID="sdsProveedorData" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        SelectCommand="SELECT gan_Proveedores.Nombre, gan_Proveedores.direccion, gan_Proveedores.ciudad, Estados.estado, gan_Proveedores.RFC FROM gan_Proveedores INNER JOIN Estados ON gan_Proveedores.estadoID = Estados.estadoID WHERE (gan_Proveedores.ganProveedorID = @ganProveedorID)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="ddlProveedores" Name="ganProveedorID" 
                                                PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td><asp:Button ID="btnNewFactura" runat="server" onclick="btnNewFactura_Click" 
                        Text="Nueva Factura de Ganado" /></td>
            </tr>
        </table>
        <asp:Panel runat="Server" id="pnlDetalle" Visible="False">
        
            <table width="100%">
                <tr>
                    <td>
                        <asp:CheckBox ID="chkAddProducto" runat="server" 
                            Text="AGREGAR PRODUCTO A LA LIQUIDACION" />
                        <asp:Panel ID="pnladdproducto" runat="server" GroupingText="AGREGAR PRODUCTO">	
	                        <table>
	                            <tr>
	                                <td colspan="4">
                                           <asp:UpdateProgress ID="ProgressProductos" runat="server" 
                                               AssociatedUpdatePanelID="UpdatePanelContent" DisplayAfter="0">
                                                <ProgressTemplate>
                                                    <img alt="" src="imagenes/cargando.gif" />PROCESANDO DATOS...
                                                </ProgressTemplate>
                                           </asp:UpdateProgress>
	                                </td>
                                </tr>	
	                            <tr>
                                    <td class="TablaField">Grupo:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlGrupos" runat="server" 
                                            DataSourceID="sqlGrupos" DataTextField="grupo" DataValueField="grupoID">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="TablaField">Producto :</td>
                                    <td>
                                        <asp:DropDownList ID="ddlProductos" runat="server" DataSourceID="sqlProductos" 
                                            DataTextField="Nombre" DataValueField="productoID" >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
	                            <tr>
	                                <td class="TablaField">Arete:</td>
	                                <td>
                                        <asp:TextBox ID="txtArete" runat="server"></asp:TextBox>
	                                </td>
    		                        <td class="TablaField">Factura:</td>
	                                <td>
                                        <asp:TextBox ID="txtFactura" runat="server"></asp:TextBox>
                                    </td>
	                            </tr>
	                            <tr>
                                    <td class="TablaField">
                                        KG:</td>
                                    <td>
                                        <asp:TextBox ID="txtKG" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="TablaField">
                                        Dieta:</td>
                                    <td>
                                        <asp:TextBox ID="txtDieta" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TablaField">
                                        Cantidad:</td>
                                    <td>
                                        <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="TablaField">
                                        Precio :</td>
                                    <td>
                                        <asp:TextBox ID="txtPrecio" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
	                            <tr>
	                                <td colspan="4" align="center">
                                       <asp:SqlDataSource ID="sqlGrupos" runat="server" 
                                           ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                           
                                            SelectCommand="SELECT [grupoID], [grupo] FROM [productoGrupos] WHERE ([grupoID] = @grupoID)">
                                           <SelectParameters>
                                               <asp:Parameter DefaultValue="4" Name="grupoID" Type="Int32" />
                                           </SelectParameters>
                                       </asp:SqlDataSource>
                                       <asp:SqlDataSource ID="sqlProductos" runat="server" 
                                           ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                           SelectCommand="SELECT Productos.productoID, Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Nombre FROM Productos INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID Where Productos.productoGrupoID = @grupoID ORDER BY Nombre ">
                                           <SelectParameters>
                                               <asp:ControlParameter ControlID="ddlGrupos" DefaultValue="-1" Name="grupoID" 
                                                   PropertyName="SelectedValue" />
                                           </SelectParameters>
                                       </asp:SqlDataSource>
                        		
	                                   <asp:Button ID="btnAgregarProducto" runat="server" Text="Agregar Producto" 
                                           onclick="btnAgregarProducto_Click" style="height: 26px" 
                                           CausesValidation="False" />
            		                </td>
	                            </tr>
	                        </table>
	                    </asp:Panel>
                        <cc1:CollapsiblePanelExtender ID="pnladdproducto_CollapsiblePanelExtender" 
                            runat="server" CollapseControlID="chkAddProducto" Collapsed="True" 
                            Enabled="True" ExpandControlID="chkAddProducto" 
                            TargetControlID="pnladdproducto">
                        </cc1:CollapsiblePanelExtender>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlDetallesyPagos" runat="server" GroupingText="Detalle">
            
                <table width="100%">
        	        <tr>
        		        <td align="center">
                            <asp:GridView ID="gvGanado" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="facturaGanDetalleID,productoID" 
                                DataSourceID="sdsDetalleGanado" onrowdeleting="gvGanado_RowDeleting" 
                                onrowcancelingedit="gvGanado_RowCancelingEdit" 
                                onrowediting="gvGanado_RowEditing" 
                                onselectedindexchanged="gvGanado_SelectedIndexChanged" 
                                onrowupdated="gvGanado_RowUpdated" onrowupdating="gvGanado_RowUpdating" 
                                ondatabound="gvGanado_DataBound">
                                <Columns>
                                    <asp:CommandField ButtonType="Button" DeleteText="Eliminar" 
                                        ShowDeleteButton="True" />
                                    <asp:CommandField ButtonType="Button" EditText="Modificar" 
                                        ShowEditButton="True" />
                                    <asp:BoundField DataField="arete" HeaderText="Arete" SortExpression="arete" />
                                    <asp:BoundField DataField="Factura" HeaderText="Factura" 
                                        SortExpression="Factura" />
                                    <asp:BoundField DataField="KG" DataFormatString="{0:N2}" HeaderText="KG" 
                                        SortExpression="KG" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="No" DataFormatString="{0:N0}" HeaderText="No" 
                                        SortExpression="No" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Descripcion" 
                                        SortExpression="Nombre" />
                                    <asp:BoundField DataField="dieta" DataFormatString="{0:N0}%" HeaderText="Dieta" 
                                        SortExpression="dieta" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="KGNetos" HeaderText="KG Netos" 
                                        SortExpression="KGNetos" DataFormatString="{0:N2}" ReadOnly="True" >
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Precio" HeaderText="Precio" 
                                        SortExpression="Precio" DataFormatString="{0:C2}" >
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Total" HeaderText="Total" ReadOnly="True" 
                                        SortExpression="Total" DataFormatString="{0:C2}" >
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="productoID" HeaderText="productoID" 
                                        InsertVisible="False" SortExpression="productoID" Visible="False" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="sdsDetalleGanado" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                
                                SelectCommand="SELECT FacturasDeGanadoDetalle.facturaGanDetalleID, FacturasDeGanadoDetalle.arete, FacturasDeGanadoDetalle.Factura, FacturasDeGanadoDetalle.KG, FacturasDeGanadoDetalle.No, Productos.productoID, Productos.Nombre, FacturasDeGanadoDetalle.dieta, FacturasDeGanadoDetalle.KGNetos, FacturasDeGanadoDetalle.Precio, FacturasDeGanadoDetalle.KGNetos * FacturasDeGanadoDetalle.Precio AS Total FROM FacturasDeGanadoDetalle INNER JOIN Productos ON FacturasDeGanadoDetalle.productoID = Productos.productoID WHERE (FacturasDeGanadoDetalle.FacturadeGanadoID = @FacturadeGanadoID) Order by Productos.Nombre" 
                                
                                DeleteCommand="DELETE FROM FacturasDeGanadoDetalle WHERE (facturaGanDetalleID = @facturaGanDetalleID)" 
                                UpdateCommand="UPDATE FacturasDeGanadoDetalle SET arete = @arete, Factura = @Factura, KG = @KG, No = @No, productoID = @productoID, dieta = @dieta, KGNetos = @KGNetos, Precio = @Precio WHERE (facturaGanDetalleID = @facturaGanDetalleID)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtFolio" Name="FacturadeGanadoID" 
                                        PropertyName="Text" />
                                </SelectParameters>
                                <DeleteParameters>
                                    <asp:Parameter Name="facturaGanDetalleID" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="arete" />
                                    <asp:Parameter Name="Factura" />
                                    <asp:Parameter Name="KG" />
                                    <asp:Parameter Name="No" />
                                    <asp:Parameter Name="productoID" />
                                    <asp:Parameter Name="dieta" />
                                    <asp:Parameter Name="KGNetos" />
                                    <asp:Parameter Name="Precio" />
                                    <asp:Parameter Name="facturaGanDetalleID" />
                                </UpdateParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="sdsConcentradoDetalle" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                
                                SelectCommand="SELECT Productos.productoID, Productos.Nombre, SUM(FacturasDeGanadoDetalle.No) AS Cabezas, SUM(FacturasDeGanadoDetalle.KG) AS KGS, SUM(FacturasDeGanadoDetalle.KGNetos) AS KGNetos FROM FacturasDeGanadoDetalle INNER JOIN Productos ON FacturasDeGanadoDetalle.productoID = Productos.productoID GROUP BY Productos.productoID, Productos.Nombre, FacturasDeGanadoDetalle.FacturadeGanadoID HAVING (FacturasDeGanadoDetalle.FacturadeGanadoID = @FacturadeGanadoID) ORDER BY Productos.Nombre">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtFolio" Name="FacturadeGanadoID" 
                                        PropertyName="Text" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
        	            <td align="right" valign="bottom">
                            <asp:GridView ID="gvConcentrado" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="productoID" DataSourceID="sdsConcentradoDetalle">
                                <Columns>
                                    <asp:BoundField DataField="Nombre" HeaderText="Producto" 
                                        SortExpression="Nombre" />
                                    <asp:BoundField DataField="Cabezas" HeaderText="Cabezas" ReadOnly="True" 
                                        SortExpression="Cabezas">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="KGS" DataFormatString="{0:N2}" HeaderText="KGS" 
                                        SortExpression="KGS">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="KGNetos" DataFormatString="{0:N2}" 
                                        HeaderText="KGNetos" SortExpression="KGNetos">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            <asp:DetailsView ID="dvTotales" runat="server" Height="50px" Width="125px" 
                                AutoGenerateRows="False" DataSourceID="sdsTotalesPagosDetalle">
                                <Fields>
                                    <asp:BoundField DataField="Total" DataFormatString="{0:C2}" HeaderText="Total" 
                                        SortExpression="Total">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pagos" DataFormatString="{0:C2}" HeaderText="Pagos" 
                                        ReadOnly="True" SortExpression="Pagos">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Fields>
                            </asp:DetailsView>
                            
                            <asp:SqlDataSource ID="sdsTotalesPagosDetalle" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                SelectCommand="SELECT [Total], [Pagos] FROM [vTotalesPagos] WHERE ([FacturadeGanadoID] = @FacturadeGanadoID)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtFolio" Name="FacturadeGanadoID" 
                                        PropertyName="Text" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
        	        </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvPesosMerma" runat="server" AutoGenerateColumns="False" 
                                DataSourceID="sdsPesosMerma" PageSize="1">
                                <Columns>
                                    <asp:CommandField ButtonType="Button" EditText="Modificar" 
                                        ShowEditButton="True" UpdateText="Actualizar" />
                                    <asp:BoundField DataField="pesoembarcado" DataFormatString="{0:N2}" 
                                        HeaderText="Peso Embarcado" SortExpression="pesoembarcado">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="pesodestino" DataFormatString="{0:N2}" 
                                        HeaderText="Peso Destino" SortExpression="pesodestino">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Merma" DataFormatString="{0:N2}" HeaderText="Merma" 
                                        ReadOnly="True" SortExpression="Merma">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MermaPorcentaje" DataFormatString="{0:N2}%" 
                                        HeaderText="% Merma" SortExpression="MermaPorcentaje" ReadOnly="True" >
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="sdsPesosMerma" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                SelectCommand="SELECT pesoembarcado, pesodestino, pesoembarcado - pesodestino AS Merma, ((pesoembarcado - pesodestino )*100 / ISNULL(NULLIF(pesoembarcado,0),1) ) as MermaPorcentaje  FROM FacturasdeGanado WHERE (FacturadeGanadoID = @FacturadeGanadoID)" 
                                
                                UpdateCommand="UPDATE FacturasdeGanado SET pesoembarcado = @pesoembarcado, pesodestino = @pesodestino WHERE (FacturadeGanadoID = @FacturadeGanadoID)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtFolio" Name="FacturadeGanadoID" 
                                        PropertyName="Text" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="pesoembarcado" />
                                    <asp:Parameter Name="pesodestino" />
                                    <asp:ControlParameter ControlID="txtFolio" Name="FacturadeGanadoID" 
                                        PropertyName="Text" />
                                </UpdateParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td align="right">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar Datos" />
                            <asp:Button ID="btnPrint" runat="server" Text="Imprimir" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="TableHeader" colspan="2">
                            MOVIMIENTOS BANCARIOS RELACIONADOS A LA FACTURA</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:GridView ID="gvPagosFactura" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="pagoID" DataSourceID="sdsPagosFactura" 
                                onrowdeleting="gvPagosFactura_RowDeleting" 
                                ondatabound="gvPagosFactura_DataBound">
                                <Columns>
                                    <asp:BoundField DataField="pagoID" HeaderText="pagoID" ReadOnly="True" 
                                        SortExpression="pagoID" InsertVisible="False" Visible="False">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fechaMov" DataFormatString="{0:dd/MM/yyyy}" 
                                        HeaderText="Fecha" SortExpression="fechaMov" />
                                    <asp:BoundField DataField="Concepto" HeaderText="Concepto" 
                                        SortExpression="Concepto" DataFormatString="{0:c2}">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cargo" HeaderText="Cargo" 
                                        SortExpression="cargo" DataFormatString="{0:c2}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="abono" DataFormatString="{0:c2}" 
                                        HeaderText="Abono" SortExpression="abono">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="numCheque" 
                                        HeaderText="# cheque" SortExpression="numCheque">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:CommandField ButtonType="Button" ShowDeleteButton="True" 
                                        DeleteText="Eliminar" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="sdsPagosFactura" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                DeleteCommand="select 1;" 
                                
                                SelectCommand="SELECT PagosFacturaDeGanado.pagoID, MovimientosCuentasBanco.fecha AS fechaMov, MovimientosCuentasBanco.cargo, MovimientosCuentasBanco.abono, MovimientosCuentasBanco.numCheque, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.movbanID FROM MovimientosCuentasBanco INNER JOIN PagosFacturaDeGanado ON MovimientosCuentasBanco.movbanID = PagosFacturaDeGanado.movbanID INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID WHERE (PagosFacturaDeGanado.FacturadeGanadoID = @factId)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtFolio" Name="factId" PropertyName="Text" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:CheckBox ID="chkMostrarAgregarPago" runat="server" CssClass="TablaField" 
                                Text="Mostrar Panel Para Agregar Nuevo Pago" />
                                <asp:UpdatePanel ID="UpdateAddNewPago" runat="Server">
                                    <ContentTemplate>
                                        <div ID="divAgregarNuevoPago" runat="Server">
                                            <table>
                                                
                                                <tr>
                                                    <td class="TablaField">
                                                        Fecha:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaNPago" runat="server" ReadOnly="True"></asp:TextBox>
                                                        <rjs:PopCalendar ID="PopCalendar6" runat="server" Control="txtFechaNPago" 
                                                            Separator="/" />
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="TablaField">
                                                        <asp:Label ID="lblNombre0" runat="server" Text="Nombre:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNombrePago" runat="server" Width="266px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="TablaField">
                                                        Monto:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtMonto" runat="server" Width="266px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TablaField" colspan="3">
                                                        <div ID="divMovBanco" runat="Server">
                                                            <table border="1">
                                                                <tr>
                                                                    <td align="center" class="TableHeader" colspan="2">
                                                                        DATOS MOVIMIENTO DE BANCO</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TablaField">
                                                                        Concepto:</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cmbConceptomovBancoPago" runat="server" 
                                                                            DataSourceID="sdsConceptoPago" DataTextField="Concepto" 
                                                                            DataValueField="ConceptoMovCuentaID" Height="22px">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="sdsConceptoPago" runat="server" 
                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                            SelectCommand="SELECT [ConceptoMovCuentaID], [Concepto] FROM [ConceptosMovCuentas]  Where ConceptoMovCuentaID NOT IN (4,5,6,7) ORDER BY [Concepto]">
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TablaField">
                                                                        Cuenta:</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="cmbCuentaPago" runat="server" 
                                                                            DataSourceID="sdsCuentaPago" DataTextField="cuenta" DataValueField="cuentaID" 
                                                                            Height="22px">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="sdsCuentaPago" runat="server" 
                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                            SelectCommand="SELECT Bancos.nombre + '  ' + CuentasDeBanco.NumeroDeCuenta + ' - ' + CuentasDeBanco.Titular AS cuenta, CuentasDeBanco.cuentaID FROM Bancos INNER JOIN CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID ORDER BY cuenta">
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TablaField">
                                                                        Grupo de catálogos de cuenta fiscal:
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="drpdlGrupoCuentaFiscal" runat="server" 
                                                                            AutoPostBack="True" DataSourceID="sdsGruposCatalogosfiscalPago" 
                                                                            DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="sdsGruposCatalogosfiscalPago" runat="server" 
                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                            SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos]">
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TablaField">
                                                                        Catálogo de cuenta fiscal:</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="drpdlCatalogocuentafiscalPago" runat="server" 
                                                                            AutoPostBack="True" DataSourceID="sdsCatalogoCuentaFiscal" 
                                                                            DataTextField="catalogoMovBanco" DataValueField="catalogoMovBancoID" 
                                                                            Height="23px">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="sdsCatalogoCuentaFiscal" runat="server" 
                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                            SelectCommand="SELECT catalogoMovBancoID, catalogoMovBanco FROM catalogoMovimientosBancos WHERE (grupoCatalogoID = @grupoCatalogoID)">
                                                                            <SelectParameters>
                                                                                <asp:ControlParameter ControlID="drpdlGrupoCuentaFiscal" DefaultValue="-1" 
                                                                                    Name="grupoCatalogoID" PropertyName="SelectedValue" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TablaField">
                                                                        Subcatálogo de cuenta fiscal:</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="drpdlSubcatalogofiscalPago" runat="server" 
                                                                            DataSourceID="sdsSubcatalogofiscalPago" DataTextField="subCatalogo" 
                                                                            DataValueField="subCatalogoMovBancoID">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="sdsSubcatalogofiscalPago" runat="server" 
                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                            SelectCommand="SELECT SubCatalogoMovimientoBanco.subCatalogo, SubCatalogoMovimientoBanco.subCatalogoMovBancoID FROM SubCatalogoMovimientoBanco INNER JOIN catalogoMovimientosBancos ON SubCatalogoMovimientoBanco.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID WHERE (SubCatalogoMovimientoBanco.catalogoMovBancoID = @catalogoMovBancoID)">
                                                                            <SelectParameters>
                                                                                <asp:ControlParameter ControlID="drpdlCatalogocuentafiscalPago" 
                                                                                    DefaultValue="-1" Name="catalogoMovBancoID" PropertyName="SelectedValue" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TablaField" colspan="2">
                                                                        <div ID="divCheque" runat="server">
                                                                            <table border="1">
                                                                                <tr>
                                                                                    <td align="center" class="TableHeader" colspan="2">
                                                                                        DATOS DE CHEQUE</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="TablaField">
                                                                                        # Cheque (*):</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtChequeNum" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="TablaField">
                                                                                        Nombre interno:</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtChequeNombre" runat="server" Width="282px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="TablaField">
                                                                                        Grupo de catálogos de cuenta interna:</td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="drpdlGrupoCatalogosInternaPago" runat="server" 
                                                                                            AutoPostBack="True" DataSourceID="sdsGruposCatalogosInternaPago" 
                                                                                            DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID">
                                                                                        </asp:DropDownList>
                                                                                        <asp:SqlDataSource ID="sdsGruposCatalogosInternaPago" runat="server" 
                                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                            SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos]">
                                                                                        </asp:SqlDataSource>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="TablaField">
                                                                                        Catálogo de cuenta interna:</td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="drpdlCatalogoInternoPago" runat="server" 
                                                                                            AutoPostBack="True" DataSourceID="sdsCatalogoCuentaInternaPago" 
                                                                                            DataTextField="catalogoMovBanco" DataValueField="catalogoMovBancoID">
                                                                                        </asp:DropDownList>
                                                                                        <asp:SqlDataSource ID="sdsCatalogoCuentaInternaPago" runat="server" 
                                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                            SelectCommand="SELECT catalogoMovBancoID, catalogoMovBanco FROM catalogoMovimientosBancos WHERE (grupoCatalogoID = @grupoCatalogoID)">
                                                                                            <SelectParameters>
                                                                                                <asp:ControlParameter ControlID="drpdlGrupoCatalogosInternaPago" 
                                                                                                    DefaultValue="-1" Name="grupoCatalogoID" PropertyName="SelectedValue" />
                                                                                            </SelectParameters>
                                                                                        </asp:SqlDataSource>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="TablaField">
                                                                                        Subcatálogo de cuenta interna:</td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="drpdlSubcatologointernaPago" runat="server" 
                                                                                            DataSourceID="sdsSubCatalogoInternaPago" DataTextField="subCatalogo" 
                                                                                            DataValueField="subCatalogoMovBancoID">
                                                                                        </asp:DropDownList>
                                                                                        <asp:SqlDataSource ID="sdsSubCatalogoInternaPago" runat="server" 
                                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                            SelectCommand="SELECT SubCatalogoMovimientoBanco.subCatalogo, SubCatalogoMovimientoBanco.subCatalogoMovBancoID FROM SubCatalogoMovimientoBanco INNER JOIN catalogoMovimientosBancos ON SubCatalogoMovimientoBanco.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID WHERE (SubCatalogoMovimientoBanco.catalogoMovBancoID = @catalogoMovBancoID)">
                                                                                            <SelectParameters>
                                                                                                <asp:ControlParameter ControlID="drpdlCatalogoInternoPago" DefaultValue="-1" 
                                                                                                    Name="catalogoMovBancoID" PropertyName="SelectedValue" />
                                                                                            </SelectParameters>
                                                                                        </asp:SqlDataSource>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Observaciones:</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtObserv" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:UpdateProgress ID="UpProgPagos" runat="server" 
                                                            AssociatedUpdatePanelID="UpdateAddNewPago" DisplayAfter="0">
                                                            <ProgressTemplate>
                                                                <asp:Image ID="Image5" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                                                                Procesando informacion de pago...
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                        <asp:Button ID="btnAddPago" runat="server" onclick="btnAddPago_Click" 
                                                            Text="Agregar Pago a la Factura" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                              <asp:Panel ID="pnlNewPago" runat="server">
                                                                <asp:Image ID="imgBienPago" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                                                                <asp:Image ID="imgMalPago" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                                                                <asp:Label ID="lblNewPagoResult" runat="server"></asp:Label>
                                                            </asp:Panel></td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>


</asp:Content>
