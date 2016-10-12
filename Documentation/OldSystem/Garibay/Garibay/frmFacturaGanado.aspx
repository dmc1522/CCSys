<%@ Page Title="Liquidaciones de Ganado" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="True" CodeBehind="frmFacturaGanado.aspx.cs" Inherits="Garibay.frmFacturaGanado" %>
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
                <td>
                    <asp:GridView ID="gvFacturasEnCero" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="FacturadeGanadoID" DataSourceID="sdsFacturasEnCero" 
                        ondatabound="gvFacturasEnCero_DataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="# Factura" InsertVisible="False" 
                                SortExpression="FacturadeGanadoID">
                                <EditItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("FacturadeGanadoID") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" 
                                        NavigateUrl='<%# GetOpenFacturaURL(Eval("FacturadeGanadoID").ToString()) %>' 
                                        Text='<%# Eval("FacturadeGanadoID") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                HeaderText="fecha" SortExpression="fecha" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsFacturasEnCero" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        SelectCommand="SELECT FacturadeGanadoID, fecha, total, ganProveedorID FROM FacturasdeGanado WHERE ((SELECT COUNT(*) AS Expr1 FROM FacturasDeGanadoDetalle WHERE (FacturadeGanadoID = FacturasdeGanado.FacturadeGanadoID)) = 0) AND (ganProveedorID = @ganProveedorID)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlProveedores" Name="ganProveedorID" 
                                PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnNewFactura" runat="server" onclick="btnNewFactura_Click" 
                        Text="Nueva Factura de Ganado" />
                </td>
            </tr>
        </table>
        <asp:Panel runat="Server" id="pnlDetalle" Visible="False">
        
            <table width="100%">
                <tr>
                    <td>
                        <asp:Panel runat="Server" id="pnlAnticiposProveedor" 
                            GroupingText="Anticipos a Productor">
                            <asp:GridView ID="gvAnticiposDadosAlProductor" runat="server" 
                                AutoGenerateColumns="False" DataKeyNames="movbanID" 
                                DataSourceID="sdsAnticiposDadosAlProductor" 
                                ondatabound="gvAnticiposDadosAlProductor_DataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAddAnticipo" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="pagoID" HeaderText="pagoID" InsertVisible="False" 
                                        ReadOnly="True" SortExpression="pagoID" Visible="False" />
                                    <asp:BoundField DataField="fechaMov" DataFormatString="{0:dd/MM/yyyy}" 
                                        HeaderText="Fecha" SortExpression="fechaMov" />
                                    <asp:BoundField DataField="Concepto" DataFormatString="{0:c2}" 
                                        HeaderText="Concepto" SortExpression="Concepto" />
                                    <asp:BoundField DataField="cargo" DataFormatString="{0:c2}" HeaderText="Cargo" 
                                        SortExpression="cargo">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="abono" DataFormatString="{0:c2}" HeaderText="Abono" 
                                        SortExpression="abono">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="numCheque" HeaderText="# cheque" 
                                        SortExpression="numCheque">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="sdsAnticiposDadosAlProductor" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                SelectCommand="SELECT MovimientosCuentasBanco.fecha AS fechaMov, MovimientosCuentasBanco.cargo, MovimientosCuentasBanco.abono, MovimientosCuentasBanco.numCheque, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.movbanID, Anticipos_FacturasGanado.ganProveedorID, Anticipos_FacturasGanado.FacturadeGanadoID FROM MovimientosCuentasBanco INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID INNER JOIN Anticipos_FacturasGanado ON MovimientosCuentasBanco.movbanID = Anticipos_FacturasGanado.movbanID WHERE (Anticipos_FacturasGanado.ganProveedorID = @ganProveedorID) AND (Anticipos_FacturasGanado.FacturadeGanadoID IS NULL)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlProveedores" Name="ganProveedorID" 
                                        PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:Button ID="btnAddAnticiposAFactura" runat="server" 
                                onclick="btnAddAnticiposAFactura_Click" 
                                Text="Agregar anticipos seleccionados a Factura" />
                                <br />
                            <asp:Label ID="lblNotaAnticipos" runat="server" 
                                Text="Para agregar el anticipo a la factura, seleccionelo y pulse el boton."></asp:Label>
                        </asp:Panel>
                    </td>
                </tr>
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
                                    <td class="TablaField">
                                        Grupo:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlGrupos" runat="server" DataSourceID="sqlGrupos" 
                                            DataTextField="grupo" DataValueField="grupoID">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="TablaField">
                                        Producto :</td>
                                    <td>
                                        <asp:DropDownList ID="ddlProductos" runat="server" DataSourceID="sqlProductos" 
                                            DataTextField="Nombre" DataValueField="productoID">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TablaField">
                                        Arete:</td>
                                    <td>
                                        <asp:TextBox ID="txtArete" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="TablaField">
                                        Factura:</td>
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
                                    <td align="center" colspan="4">
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
                                        <asp:Button ID="btnAgregarProducto" runat="server" CausesValidation="False" 
                                            onclick="btnAgregarProducto_Click" style="height: 26px" 
                                            Text="Agregar Producto" />
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
                                ondatabound="gvGanado_DataBound" ShowFooter="True">
                                <Columns>
                                    <asp:CommandField ButtonType="Button" DeleteText="Eliminar" 
                                        ShowDeleteButton="True" />
                                    <asp:CommandField ButtonType="Button" EditText="Modificar" 
                                        ShowEditButton="True" />
                                    <asp:TemplateField HeaderText="Arete" SortExpression="arete">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtArete" runat="server" Text='<%# Bind("arete") %>' 
                                                Width="85px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("arete") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Factura" SortExpression="Factura">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtFactura" runat="server" Text='<%# Bind("Factura") %>' 
                                                Width="85px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Factura") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="KG" SortExpression="KG">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtKG" runat="server" Text='<%# Bind("KG") %>' Width="85px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblKGBrutos" runat="server" Text="Label"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("KG", "{0:N2}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No." SortExpression="No">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtNo" runat="server" Text='<%# Bind("No") %>' Width="85px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalCabezas" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("No", "{0:N0}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descripcion" SortExpression="Nombre">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlProducto" runat="server" 
                                                DataSourceID="sdsEditProductos" DataTextField="Nombre" 
                                                DataValueField="productoID" SelectedValue='<%# Bind("productoID") %>'>
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="sdsEditProductos" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                SelectCommand="SELECT [productoID], [Nombre] FROM [Productos] WHERE ([productoGrupoID] = @productoGrupoID) ORDER BY [Nombre]">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="ddlGrupos" Name="productoGrupoID" 
                                                        PropertyName="SelectedValue" Type="Int32" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Nombre") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dieta" SortExpression="dieta">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDieta" runat="server" Text='<%# Bind("dieta") %>' 
                                                Width="45px"></asp:TextBox>%
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("dieta", "{0:N0}%") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="KG Netos" SortExpression="KGNetos">
                                        <EditItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("KGNetos", "{0:N2}") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblKGNetos" runat="server" Text="Label"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("KGNetos", "{0:N2}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Precio" HeaderText="Precio" 
                                        SortExpression="Precio" DataFormatString="{0:C2}" >
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Total" SortExpression="Total">
                                        <EditItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Total", "{0:C2}") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotales" runat="server" Text="Label"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("Total", "{0:C2}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
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
                                    <asp:BoundField DataField="Creditos" DataFormatString="{0:C2}" 
                                        HeaderText="Creditos" SortExpression="Creditos">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RestanteAPagar" DataFormatString="{0:C2}" 
                                        HeaderText="Restante A Pagar" SortExpression="RestanteAPagar">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Fields>
                            </asp:DetailsView>
                            
                            <asp:SqlDataSource ID="sdsTotalesPagosDetalle" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                
                                
                                SelectCommand="SELECT Total, Pagos, Creditos, (Total - Pagos - Creditos) as RestanteAPagar FROM vTotalesPagos WHERE (FacturadeGanadoID = @FacturadeGanadoID)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtFolio" Name="FacturadeGanadoID" 
                                        PropertyName="Text" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
        	        </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="gvPesosMerma" runat="server" AutoGenerateColumns="False" 
                                DataSourceID="sdsPesosMerma" onrowcancelingedit="gvPesosMerma_RowCancelingEdit" 
                                onrowediting="gvPesosMerma_RowEditing" onrowupdated="gvPesosMerma_RowUpdated" 
                                onrowupdating="gvPesosMerma_RowUpdating" PageSize="1">
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
                        <td align="right" valign="bottom">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="right">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="TableHeader" colspan="2">
                            ANTICIPOS</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:GridView ID="gvAnticiposFactura" runat="server" 
                                AutoGenerateColumns="False" DataKeyNames="movbanID" 
                                DataSourceID="sdsAnticiposGanado" ondatabound="gvPagosFactura_DataBound" 
                                onrowdeleting="gvAnticiposFactura_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="pagoID" HeaderText="pagoID" InsertVisible="False" 
                                        ReadOnly="True" SortExpression="pagoID" Visible="False" />
                                    <asp:BoundField DataField="fechaMov" DataFormatString="{0:dd/MM/yyyy}" 
                                        HeaderText="Fecha" SortExpression="fechaMov" />
                                    <asp:BoundField DataField="Concepto" DataFormatString="{0:c2}" 
                                        HeaderText="Concepto" SortExpression="Concepto" />
                                    <asp:BoundField DataField="cargo" DataFormatString="{0:c2}" HeaderText="Cargo" 
                                        SortExpression="cargo">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="abono" DataFormatString="{0:c2}" HeaderText="Abono" 
                                        SortExpression="abono">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="numCheque" HeaderText="# cheque" 
                                        SortExpression="numCheque">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:CommandField ButtonType="Button" DeleteText="Eliminar" 
                                        ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="sdsAnticiposGanado" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                
                                SelectCommand="SELECT MovimientosCuentasBanco.fecha AS fechaMov, MovimientosCuentasBanco.cargo, MovimientosCuentasBanco.abono, MovimientosCuentasBanco.numCheque, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.movbanID, Anticipos_FacturasGanado.FacturadeGanadoID FROM MovimientosCuentasBanco INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID INNER JOIN Anticipos_FacturasGanado ON MovimientosCuentasBanco.movbanID = Anticipos_FacturasGanado.movbanID WHERE (Anticipos_FacturasGanado.FacturadeGanadoID = @FacturadeGanadoID)" 
                                DeleteCommand="select 1;">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtFolio" Name="FacturadeGanadoID" 
                                        PropertyName="Text" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="TableHeader" colspan="2">
                            MOVIMIENTOS BANCARIOS RELACIONADOS A LA FACTURA</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:GridView ID="gvPagosFactura" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="pagoID,movbanID" DataSourceID="sdsPagosFactura" 
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
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="Button1" runat="server" 
                                                onclick='<%# GetPrintChequeURL(Eval("movbanID").ToString()) %>'
                                                onclientclick='<%# GetPrintChequeURL(Eval("movbanID").ToString()) %>' 
                                                Text="Imprimir" 
                                                Visible='<%# IsChequeVisible(Eval("numCheque").ToString()) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                            <table>
                            	<tr>
                            		<td class="TablaField">PAGOS A CREDITOS</td>
                            	</tr>
                            	<tr>
                            		<td>
                                        <asp:GridView ID="gvPagosCreditos" runat="server" AutoGenerateColumns="False" 
                                            DataKeyNames="creditoID" DataSourceID="sdsPagosCreditos" 
                                            ondatabound="gvPagosCreditos_DataBound" 
                                            onrowdeleted="gvPagosCreditos_RowDeleted" 
                                            onrowupdating="gvPagosCreditos_RowUpdating">
                                            <Columns>
                                                <asp:CommandField ButtonType="Button" CancelText="Cancelar" DeleteText="Quitar" 
                                                    EditText="Editar" ShowDeleteButton="True" ShowEditButton="True" />
                                                <asp:TemplateField HeaderText="Fecha" SortExpression="fecha">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server" 
                                                            Text='<%# Bind("fecha", "{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                                        <rjs:PopCalendar ID="PopCalendar7" runat="server" Control="TextBox1" 
                                                            Separator="/" />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" 
                                                            Text='<%# Bind("fecha", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                                                    SortExpression="Productor" />
                                                <asp:BoundField DataField="monto" DataFormatString="{0:C2}" HeaderText="Monto" 
                                                    SortExpression="monto">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="sdsPagosCreditos" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            DeleteCommand="DELETE FROM FacturaGanado_Credito WHERE (creditoID = @creditoID)" 
                                            SelectCommand="SELECT FacturaGanado_Credito.FacturadeGanadoID, FacturaGanado_Credito.fecha, LTRIM(Productores.apaterno + SPACE(1) + Productores.amaterno + SPACE(1) + Productores.nombre) AS Productor, FacturaGanado_Credito.monto, FacturaGanado_Credito.creditoID FROM Productores INNER JOIN Creditos ON Productores.productorID = Creditos.productorID INNER JOIN FacturaGanado_Credito ON Creditos.creditoID = FacturaGanado_Credito.creditoID WHERE (FacturaGanado_Credito.FacturadeGanadoID = @FacturadeGanadoID)" 
                                            UpdateCommand="UPDATE FacturaGanado_Credito SET monto = @monto, fecha = @fecha WHERE (creditoID = @creditoID)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="txtFolio" Name="FacturadeGanadoID" 
                                                    PropertyName="Text" />
                                            </SelectParameters>
                                            <DeleteParameters>
                                                <asp:Parameter Name="creditoID" />
                                            </DeleteParameters>
                                            <UpdateParameters>
                                                <asp:Parameter Name="monto" />
                                                <asp:Parameter Name="fecha" />
                                                <asp:Parameter Name="creditoID" />
                                            </UpdateParameters>
                                        </asp:SqlDataSource>
                                    </td>
                            	</tr>
                            </table>
                            <asp:CheckBox ID="chkAddPagoCredito" runat="server" 
                                Text="Mostrar panel para agregar pago a credito" />
                            <asp:Panel ID="pnlPagoCredito" runat="server">
                                <table>
                                	<tr>
                                		<td>CICLO:</td><td>
                                            <asp:DropDownList ID="ddlCiclosPagoCredito" runat="server" AutoPostBack="True" 
                                                DataSourceID="sdsCicloPagoCredito" DataTextField="CicloName" 
                                                DataValueField="cicloID" 
                                                onselectedindexchanged="ddlCiclosPagoCredito_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="sdsCicloPagoCredito" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [fechaInicio] DESC">
                                            </asp:SqlDataSource>
                                        </td>
                                	</tr>
                                    <tr>
                                        <td>
                                            FECHA DEL PAGO:</td>
                                        <td style="font-size: small">
                                            <asp:TextBox ID="txtFechaPagoCredito" runat="server"></asp:TextBox>
                                            <rjs:PopCalendar ID="PopCalendar8" runat="server" AutoPostBack="True" 
                                                Control="txtFechaPagoCredito" 
                                                onselectionchanged="PopCalendar8_SelectionChanged" Separator="/" />
                                            <br />
                                            El saldo de los creditos es calculado usando como fecha de fin de periodo esta 
                                            fecha.</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            CREDITO:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlCreditoAPagar" runat="server" 
                                                DataSourceID="sdsCreditosAPagar" DataTextField="NombreYDebe" 
                                                DataValueField="CreditoID">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="sdsCreditosAPagar" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                SelectCommand="ReturnReporteGlobalCreditosForLiq" 
                                                SelectCommandType="StoredProcedure">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="txtFechaPagoCredito" Name="fechafin" 
                                                        PropertyName="Text" Type="DateTime" />
                                                    <asp:ControlParameter ControlID="ddlCiclosPagoCredito" Name="cicloId" 
                                                        PropertyName="SelectedValue" Type="Int32" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ABONO:</td>
                                        <td>
                                            <asp:TextBox ID="txtAbonoCredito" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            MARCAR PAGADO:</td>
                                        <td>
                                            <asp:CheckBox ID="chkMarcarPagado" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Button ID="btnAddAbonoCredito" runat="server" 
                                                onclick="btnAddAbonoCredito_Click" Text="Agregar Abono" />
                                            <br />
                                            <asp:Label ID="lblAddAbonoResult" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:CollapsiblePanelExtender ID="pnlPagoCredito_CollapsiblePanelExtender" 
                                runat="server" CollapseControlID="chkAddPagoCredito" Collapsed="True" 
                                Enabled="True" ExpandControlID="chkAddPagoCredito" 
                                TargetControlID="pnlPagoCredito">
                            </cc1:CollapsiblePanelExtender>
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
                                                                                <td>
                                                                                    Observaciones:</td>
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
                            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar Datos" 
                                onclick="btnActualizar_Click" />
                            <asp:Button ID="btnPrint" runat="server" Text="Imprimir" /></td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>


</asp:Content>
