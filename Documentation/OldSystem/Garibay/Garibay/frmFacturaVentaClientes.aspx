<%@ Page Language="C#" Theme="skinverde" MasterPageFile="~/MasterPage.Master" Title = "Factura de Venta a Clientes" AutoEventWireup="True" CodeBehind="frmFacturaVentaClientes.aspx.cs" Inherits="Garibay.frmFacturaVentaClientes" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" runat="server"  contentplaceholderid="ContentPlaceHolder1">
    <asp:UpdatePanel ID="MainPnlUpdate" runat="Server">
<ContentTemplate>
        <table>
            <tr>
                <td rowspan="6">
                    <asp:Image ID="Image2" runat="server" Height="92px" 
                        ImageUrl="~/imagenes/LogoIPROJALMedium.jpg" Width="165px" />
                </td>
                <td rowspan="6">
                    <asp:Label ID="Label2" runat="server" Font-Size="X-Large" 
                        Text="INTEGRADORA DE PRODUCTORES DE JALISCO"></asp:Label>
&nbsp; S.P.R. DE R.L.<br />
                    <br />
                    Av. Patria Oriente No. 10<br />
                    C.P. 46600 Ameca, Jalisco.<br />
                    R.F.C. IPJ-030814-JAA<br />
                    Tel. 01(375) 758 1199</td>
                <td class="TableHeader" align= "center">
                    Factura No.</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:TextBox ID="txtNumFactura" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtFacturaIDToMod" runat="server" Visible="False" Width="15px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TableHeader" align="center">
                    FECHA</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:TextBox ID="txtFecha" runat="server" ReadOnly="True"></asp:TextBox>
                    <rjs:PopCalendar ID="PopCalendar3" runat="server" Separator="/" 
                        Control="txtFecha" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    CICLO:</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:DropDownList ID="drpdlCiclo" runat="server" DataSourceID="sdsCiclos" 
                        DataTextField="CicloName" DataValueField="cicloID" Height="23px" Width="181px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
                        SelectCommand="SELECT cicloID, CicloName FROM Ciclos WHERE (cerrado = @cerrado) ORDER BY fechaInicio DESC">
                    	<SelectParameters>
							<asp:Parameter DefaultValue="FALSE" Name="cerrado" />
						</SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:UpdatePanel ID="PnlCliente" runat="Server">
                    <ContentTemplate>
                        <table >
                            <tr>
                                <td align="center" class="TablaField" colspan="2">
                                    NOMBRE</td>
                                <td align="center" class="TablaField" colspan="2">
                                    DOMICILIO</td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:DropDownList ID="drpdlCliente" runat="server" Height="23px" Width="460px" 
                                        DataSourceID="sdsClientes" DataTextField="nombre" 
                                        DataValueField="clienteventaID">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsClientes" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        SelectCommand="SELECT [nombre], [clienteventaID] FROM [ClientesVentas] order by nombre">
                                    </asp:SqlDataSource><br />
                                    <asp:Button ID="btnAgregarClienteRapido" runat="server" 
                                        Text="Agregar Cliente Rápido" onclick="btnAgregarClienteRapido_Click" />
                                    <asp:Button ID="btnActualizaClientes" runat="server" 
                                        onclick="btnActualizaClientes_Click" Text="Actualizar Lista de Clientes" />
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtDomicilio" runat="server" Height="23px" Width="513px" 
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="TablaField">
                                    CIUDAD</td>
                                <td align="center" class="TablaField">
                                    ESTADO</td>
                                <td align="center" class="TablaField">
                                    R.F.C.</td>
                                <td align="center" class="TablaField">
                                    TELEFONO</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCiudad" runat="server" Height="22px" Width="276px" 
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEstado" runat="server" Height="23px" Width="187px" 
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRFC" runat="server" Height="23px" Width="140px" 
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTelefono" runat="server" Height="23px" Width="270px" 
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="TablaField">
                                    COLONIA</td>
                                <td align="center" class="TablaField">
                                    CP</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtColonia" runat="server" Width="274px" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtCP" runat="server" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Panel ID="pnlNewFactura" runat="Server" Visible="false">
                                        <asp:Label ID="lblAddResult" runat="server" Text=""></asp:Label>
                                    </asp:Panel>
                                </td>
                                <td align="right">
                                    <asp:CheckBox ID="chkAddBoletas" runat="server" 
                                        Text="Generar Factura con Boletas agregadas" Checked="True" /><br />
                                    <asp:Button ID="btnAgregarNewFactura" runat="server" 
                                        onclick="btnAgregarNewFactura_Click" Text="Agregar Nueva Factura" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate></asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                <asp:Panel runat="Server" id="pnlCentral">
                    <table>
                    	<tr>
                    		<td align="center">
                                    <asp:UpdateProgress runat="Server" ID="progress1" 
                                        AssociatedUpdatePanelID="pnlCentralData" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl="/imagenes/cargando.gif" />
                                        Procesando datos....
                                    </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <asp:UpdatePanel runat="Server" id="pnlCentralData">
                                        <ContentTemplate>
                                            <table >
                                                <tr>
                                                    <td align="center">
                                                        <table>
                                                            <tr>
                                                                <td align="center">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Panel ID="pnlBoletas" runat="Server">
                                                                        <asp:CheckBox ID="chkOpenAddBoletas" runat="server" 
                                                                            Text="Agregar Boletas Existentes" />
                                                                        <asp:Panel runat="Server" id="pnlAddBoletasExistentes">
                                                                            <table>
                                                                                <tr>
                                                                                    <td class="TablaField">Cliente:</td><td>
                                                                                    <asp:DropDownList ID="ddlClientesVentaboletas" runat="server" 
                                                                                        AutoPostBack="True" DataSourceID="sdsClientesBoletas" DataTextField="nombre" 
                                                                                        DataValueField="clienteventaID" 
                                                                                        onselectedindexchanged="ddlClientesVentaboletas_SelectedIndexChanged"></asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="sdsClientesBoletas" runat="server" 
                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                        SelectCommand="SELECT DISTINCT ClientesVentas.clienteventaID, ClientesVentas.nombre FROM ClientesVentas INNER JOIN ClienteVenta_Boletas ON ClientesVentas.clienteventaID = ClienteVenta_Boletas.clienteventaID WHERE ClienteVenta_Boletas.boletaID NOT IN ( select boletaid from FacturasCV_Boletas) ORDER BY ClientesVentas.nombre">
                                                                                    </asp:SqlDataSource>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <asp:GridView ID="gvBoletasDisponibles" runat="server" 
                                                                                            AutoGenerateColumns="False" DataKeyNames="boletaID" 
                                                                                            DataSourceID="sdsBoletasDisponibles" 
                                                                                            ondatabound="gvBoletasDisponibles_DataBound">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="Agregar">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:CheckBox ID="chkAddBoleta" runat="server" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="boletaID" HeaderText="boletaID" 
                                                                                                    InsertVisible="False" ReadOnly="True" SortExpression="boletaID" />
                                                                                                <asp:BoundField DataField="Ticket" HeaderText="Ticket" SortExpression="Ticket">
                                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre">
                                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="pesonetosalida" DataFormatString="{0:N2}" 
                                                                                                    HeaderText="Peso" SortExpression="pesonetosalida">
                                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="FechaEntrada" DataFormatString="{0:dd/MM/yyyy}" 
                                                                                                    HeaderText="Fecha" SortExpression="FechaEntrada">
                                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                                </asp:BoundField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                        <asp:SqlDataSource ID="sdsBoletasDisponibles" runat="server" 
                                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                            SelectCommand="SELECT DISTINCT Boletas.boletaID, Boletas.Ticket, Productos.Nombre, Boletas.pesonetosalida, Boletas.FechaEntrada, ClienteVenta_Boletas.clienteventaID FROM Boletas INNER JOIN ClienteVenta_Boletas ON Boletas.boletaID = ClienteVenta_Boletas.BoletaID INNER JOIN Productos ON Boletas.productoID = Productos.productoID WHERE (ClienteVenta_Boletas.clienteventaID = @clienteventaID) and Boletas.boletaID NOT IN ( select boletaid from FacturasCV_Boletas)">
                                                                                            <SelectParameters>
                                                                                                <asp:ControlParameter ControlID="ddlClientesVentaboletas" Name="clienteventaID" 
                                                                                                    PropertyName="SelectedValue" />
                                                                                            </SelectParameters>
                                                                                        </asp:SqlDataSource>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <asp:Button ID="btnAddBoletasIntoFactura" runat="server" 
                                                                                            onclick="btnAddBoletasIntoFactura_Click" 
                                                                                            Text="Agregar Boletas Seleccionadas a Factura" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                 
                                                                        <cc1:CollapsiblePanelExtender ID="pnlAddBoletasExistentes_CollapsiblePanelExtender" 
                                                                            runat="server" CollapseControlID="chkOpenAddBoletas" Collapsed="True" 
                                                                            Enabled="True" ExpandControlID="chkOpenAddBoletas" 
                                                                            TargetControlID="pnlAddBoletasExistentes">
                                                                        </cc1:CollapsiblePanelExtender>
                 
                                                                        <asp:GridView ID="gvBoletas" runat="server" AutoGenerateColumns="False" 
                                                                            DataKeyNames="boletaID" DataSourceID="sdsBoletasExistentes" 
                                                                            onrowupdated="gvBoletas_RowUpdated" onrowupdating="gvBoletas_RowUpdating" 
                                                                            onrowdeleted="gvBoletas_RowDeleted" onrowdeleting="gvBoletas_RowDeleting">
                                                                            <Columns>
                                                                                <asp:CommandField ButtonType="Button" CancelText="Cancelar" DeleteText="Quitar" 
                                                                                    EditText="Modificar" ShowDeleteButton="True" ShowEditButton="True" />
                                                                                <asp:BoundField DataField="boletaID" HeaderText="ID" 
                                                                                    InsertVisible="False" ReadOnly="True" SortExpression="boletaID" >
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Ticket" HeaderText="Ticket" 
                                                                                    SortExpression="Ticket" >
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField HeaderText="Producto" InsertVisible="False" 
                                                                                    SortExpression="productoID">
                                                                                    <EditItemTemplate>
                                                                                        <asp:DropDownList ID="DropDownList1" runat="server" 
                                                                                            DataSourceID="sdsProductoBoleta" DataTextField="Producto" 
                                                                                            DataValueField="productoID" SelectedValue='<%# Bind("productoID") %>'>
                                                                                        </asp:DropDownList>
                                                                                        <asp:SqlDataSource ID="sdsProductoBoleta" runat="server" 
                                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                            SelectCommand="SELECT [productoID], [Producto] FROM [vProductosParaDDL] ORDER BY [Producto]">
                                                                                        </asp:SqlDataSource>
                                                                                    </EditItemTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="PesoDeEntrada" DataFormatString="{0:N2}" 
                                                                                    HeaderText="Peso de Entrada" SortExpression="PesoDeEntrada">
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="PesoDeSalida" HeaderText="Peso de Salida" 
                                                                                    SortExpression="PesoDeSalida" DataFormatString="{0:N2}" >
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField HeaderText="P. Neto Salida" SortExpression="pesonetosalida">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("pesonetosalida","{0:n2}") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                        <asp:TextBox ID="TextBox2" runat="server" ReadOnly="True" 
                                                                                            Text='<%# Bind("pesonetosalida", "{0:n2}") %>'></asp:TextBox>
                                                                                    </EditItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Peso neto">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label2" runat="server" 
                                                                                            Text='<%# Bind("pesonetoapagar", "{0:n2}") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                        <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True" 
                                                                                            Text='<%# Bind("pesonetoapagar") %>'></asp:TextBox>
                                                                                    </EditItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="precioapagar" DataFormatString="{0:C2}" 
                                                                                    HeaderText="Precio" SortExpression="precioapagar">
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="totalapagar" DataFormatString="{0:C2}" 
                                                                                    HeaderText="Total a pagar" SortExpression="totalapagar" ReadOnly="True">
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="FolioDestino" HeaderText="Folio Destino" 
                                                                                    SortExpression="FolioDestino">
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="PesoDestino" DataFormatString="{0:N2}" 
                                                                                    HeaderText="Peso Destino" SortExpression="PesoDestino">
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                        <asp:SqlDataSource ID="sdsBoletasExistentes" runat="server" 
                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                            
                                                                            
                                                                            
                                                                            SelectCommand="SELECT Boletas.boletaID, Boletas.Ticket, Boletas.precioapagar * Boletas.pesonetoapagar AS totalapagar, Productos.Nombre, Boletas.PesoDeEntrada, Boletas.PesoDeSalida, Boletas.pesonetosalida, Boletas.pesonetoapagar, Boletas.precioapagar, Boletas.FolioDestino, Boletas.PesoDestino, Productos.productoID FROM Boletas INNER JOIN Productos ON Boletas.productoID = Productos.productoID INNER JOIN FacturasCV_Boletas ON Boletas.boletaID = FacturasCV_Boletas.boletaID WHERE (FacturasCV_Boletas.FacturaCV = @FacturaCV) ORDER BY Boletas.boletaID" 
                                                                            DeleteCommand="Delete from FacturasCV_Boletas where FacturasCV_Boletas.boletaId= @boletaId" 
                                                                            
                                                                            
                                                                            
                                                                            
                                                                            UpdateCommand="UPDATE Boletas SET Ticket = @Ticket, PesoDeEntrada = @PesoDeEntrada, PesoDeSalida = @PesoDeSalida, pesonetosalida = @pesonetosalida , pesonetoapagar =@pesonetoapagar, precioapagar = @precioapagar, FolioDestino = @FolioDestino, PesoDestino = @PesoDestino WHERE (boletaID = @boletaID)">
                                                                            <SelectParameters>
                                                                                <asp:ControlParameter ControlID="txtFacturaIDToMod" 
                                                                                    Name="FacturaCV" PropertyName="Text" />
                                                                            </SelectParameters>
                                                                            <DeleteParameters>
                                                                                <asp:Parameter Name="boletaId" />
                                                                            </DeleteParameters>
                                                                            <UpdateParameters>
                                                                                <asp:Parameter Name="Ticket" />
                                                                                <asp:Parameter Name="PesoDeEntrada" />
                                                                                <asp:Parameter Name="PesoDeSalida" />
                                                                                <asp:Parameter Name="pesonetosalida" />
                                                                                <asp:Parameter Name="pesonetoapagar" />
                                                                                <asp:Parameter Name="precioapagar" />
                                                                                <asp:Parameter Name="FolioDestino" />
                                                                                <asp:Parameter Name="PesoDestino" />
                                                                                <asp:Parameter Name="boletaId" />
                                                                            </UpdateParameters>
                                                                        </asp:SqlDataSource>
                                                                        <asp:UpdateProgress ID="UPPnlBoletas" runat="server" 
                                                                            AssociatedUpdatePanelID="MainPnlUpdate" DisplayAfter="0">
                                                                            <ProgressTemplate>
                                                                                <asp:Image ID="Image3" runat="server" ImageUrl="/imagenes/cargando.gif" />
                                                                                PROCESANDO DATOS...
                                                                            </ProgressTemplate>
                                                                        </asp:UpdateProgress>
                                                                        
                                                                        <asp:Button ID="btnAddBoletas" runat="server" Text="Agregar Nueva Boleta" 
                                                                            onclick="btnAddBoletas_Click" CausesValidation="False" />
                                                                        <asp:Button ID="btnActualizarBoletas" runat="server" 
                                                                            onclick="btnActualizarBoletas_Click" Text="Actualizar Lista de Boletas" 
                                                                            CausesValidation="False" />
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" class="TableHeader">
                                                        <asp:CheckBox ID="chkPnlAddProd" runat="server" Text="MOSTRAR PANEL PARA AGREGAR PRODUCTO" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="pnlAddProd" runat="Server">
                                                            <table>
                                                                <tr>
                                                                    <td class="TableField">
                                                                        Bodega:</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="drpdlBodega" runat="server" DataSourceID="sdsBodegas" 
                                                                            DataTextField="bodega" DataValueField="bodegaID" Height="24px">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="sdsBodegas" runat="server" 
                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                            SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                    <td class="TableField">
                                                                        Grupo:</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlGrupoProducto" runat="server" AutoPostBack="True" 
                                                                            DataSourceID="sdsGrupos" DataTextField="grupo" DataValueField="grupoID">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="sdsGrupos" runat="server" 
                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                            SelectCommand="SELECT [grupoID], [grupo] FROM [productoGrupos] ORDER BY [grupo]">
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                    <td class="TableField">
                                                                        Producto:</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="drpdlProducto" runat="server" DataSourceID="sdsProductos" 
                                                                            DataTextField="Nombre" DataValueField="productoID" Height="23px">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="sdsProductos" runat="server" 
                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                            
																			
                                                                            
                                                                            
                                                                            
                                                                            SelectCommand="SELECT Productos.productoID, LTRIM(dbo.Productos.Nombre + SPACE(1) + dbo.Presentaciones.Presentacion + '(' + dbo.Unidades.Unidad + ')') AS Nombre  FROM Productos INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID INNER JOIN Unidades ON Productos.unidadID = Unidades.unidadID WHERE (Productos.productoGrupoID = @productoGrupoID) ORDER BY Productos.Nombre">
                                                                            <SelectParameters>
                                                                                <asp:ControlParameter ControlID="ddlGrupoProducto" Name="productoGrupoID" 
                                                                                    PropertyName="SelectedValue" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TablaField">
                                                                        Cantidad:</td>
                                                                    <td class="TablaField">
                                                                        <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td class="TablaField">
                                                                        Precio:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPrecio" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td class="TablaField">
                                                                        Importe:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtImporte" runat="server" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" colspan="6">
                                                                        <asp:Button ID="btnAddproduct" runat="server" onclick="btnAddproduct_Click" 
                                                                            Text="Agregar Producto" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <cc1:CollapsiblePanelExtender ID="pnlAddProd_CollapsiblePanelExtender" 
                                                            runat="server" CollapseControlID="chkPnlAddProd" Collapsed="True" 
                                                            Enabled="True" ExpandControlID="chkPnlAddProd" TargetControlID="pnlAddProd">
                                                        </cc1:CollapsiblePanelExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                        	<tr>
                                                        		<td>
                                                                    <asp:GridView ID="grdvProNotas" runat="server" AutoGenerateColumns="False" 
                                                                        CellPadding="4" ForeColor="Black" GridLines="None" 
                                                                        onrowcancelingedit="grdvProNotas_RowCancelingEdit" 
                                                                        onrowdeleting="grdvProNotas_RowDeleting" onrowediting="grdvProNotas_RowEditing" 
                                                                        onrowupdating="grdvProNotas_RowUpdating">
                                                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                                        <HeaderStyle CssClass="TableHeader" />
                                                                        <AlternatingRowStyle BackColor="White" />
                                                                        <EmptyDataTemplate>
                                                                            &nbsp;
                                                                        </EmptyDataTemplate>
                                                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                                        <Columns>
                                                                            <asp:CommandField ButtonType="Button" CancelText="Cancelar" 
                                                                                DeleteText="Eliminar" EditText="Editar" ShowDeleteButton="True" 
                                                                                ShowEditButton="True" />
                                                                            <asp:TemplateField HeaderText="Cantidad">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtCantidadEdit" runat="server" Text='<%# Bind("Cantidad") %>'></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label3" runat="server" 
                                                                                        Text='<%# Bind("Cantidad", "{0:0.00}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Descripción">
                                                                                <EditItemTemplate>
                                                                                    <asp:DropDownList ID="ddlProdEdit" runat="server" DataSourceID="sdsProdEdit" 
                                                                                        DataTextField="Nombre" DataValueField="productoID">
                                                                                    </asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="sdsProdEdit" runat="server" 
                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                        SelectCommand="SELECT [productoID], [Nombre] FROM [Productos] ORDER BY [Nombre]">
                                                                                    </asp:SqlDataSource>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Producto") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Precio Unitario">
                                                                                <EditItemTemplate>
                                                                                    <asp:TextBox ID="txtPrecioEdit" runat="server" 
                                                                                        Text='<%# Bind("[precio]") %>'></asp:TextBox>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label4" runat="server" 
                                                                                        Text='<%# Bind("[precio]", "{0:c5}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="Importe" DataFormatString="{0:c}" 
                                                                                HeaderText="Importe" ReadOnly="True" >
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <asp:Label ID="Label8" runat="server" Font-Size="Small" 
                                                                        
                                                                        Text="NOTA: Las existencias solo se ven afectadas por las boletas relacionadas a esta factura"></asp:Label>
                                                                </td>
                                                        		<td align="right" valign="bottom">
                                                                    <table>
                                                                        <tr>
                                                                            <td align="right" class="TablaField">
                                                                                SubTotal:</td>
                                                                            <td align="right">
                                                                                <asp:Label ID="lblSubtotal" runat="server" Text="Label"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" class="TablaField">
                                                                                <asp:CheckBox ID="chbIVA" runat="server" AutoPostBack="True" 
                                                                                    CssClass="TablaField" oncheckedchanged="chbIVA_CheckedChanged" Text="IVA:" />
                                                                            </td>
                                                                            <td align="right">
                                                                                <asp:Label ID="lblIva" runat="server" Text="Label"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" class="TablaField">
                                                                                RET. IVA:</td>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txtRETIVA" runat="server" Width="100px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" class="TablaField">
                                                                                TOTAL:</td>
                                                                            <td align="right">
                                                                                <asp:Label ID="lblTotal" runat="server" Text="Label"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" class="TablaField">
                                                                                PAGOS:</td>
                                                                            <td align="right">
                                                                                <asp:Label ID="lblPagos" runat="server" Text="$ 0.00"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" class="TablaField">
                                                                                Restante a Pagar:</td>
                                                                            <td align="right">
                                                                                <asp:Label ID="lblRestanteAPagar" runat="server" Text="$ 0.00"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" class="TablaField">
                                                                                &nbsp;</td>
                                                                            <td align="right">
                                                                                <asp:Button ID="btnActualizaTotales" runat="server" 
                                                                                    onclick="btnActualizaTotales_Click" Text="Actualizar totales" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                        	</tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <table>
                                                            <tr>
                                                                <td rowspan="2">
                                                                    <asp:Panel ID="panelObservaciones" runat="Server" GroupingText="Observaciones:">
                                                                        <table align="center">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtObservaciones" runat="server" Height="50px" 
                                                                                        TextMode="MultiLine" Width="550px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                                <td>
                                                                    Fecha de Pago:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtFechapago" runat="server"></asp:TextBox>
                                                                    <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtFechapago" 
                                                                        Separator="/" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <table>
                                                        	<tr>
                                                        		<td>
                                                                    <asp:Panel ID="pnlFacturaResult" runat="server">
                                                                        <asp:Image ID="imgBien" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                                                                        <asp:Image ID="imgMal" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                                                                        <asp:Label ID="lblFacturaResult" runat="server" Text=""></asp:Label>
                                                                    </asp:Panel>
                                                                </td><td>
                                                                    <asp:Button ID="btnGuardaFactura" runat="server" 
                                                                        Text="Actualizar Datos de Factura" onclick="btnGuardaFactura_Click" 
                                                                        CausesValidation="False" />
                                                                        <br />
                                                                    <asp:Button ID="btnPrintFactura" runat="server" Text="Imprimir Factura" 
                                                                        CausesValidation="False" UseSubmitBehavior="False" />
                                                                </td>
                                                        	</tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" class="TableHeader">
                                                        MOVIMIENTOS BANCARIOS RELACIONADOS A FACTURA
                                                        <br />
                                                        <asp:Button ID="btnOpenNewMovBan" runat="server" Text="Nuevo Pago  a Factura" 
                                                            UseSubmitBehavior="False" Visible="False" />
                                                        <asp:Button ID="btnActualizaPagos" runat="server" 
                                                            onclick="btnActualizaPagos_Click" Text="Actualiza Lista de Pagos" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:GridView ID="gvPagosFactura" runat="server" AutoGenerateColumns="False" 
                                                            DataKeyNames="pagoFVID" DataSourceID="sdsPagosFactura" 
                                                            onrowdatabound="gvPagosFactura_RowDataBound" 
                                                            onrowdeleting="gvPagosFactura_RowDeleting" 
                                                            ondatabound="gvPagosFactura_DataBound" ShowFooter="True">
                                                            <Columns>
                                                                <asp:BoundField DataField="pagoFVID" HeaderText="ID" ReadOnly="True" 
                                                                    SortExpression="pagoFVID">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                                                    HeaderText="Fecha" SortExpression="fecha" />
                                                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="True" 
                                                                    SortExpression="Nombre">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" ReadOnly="True" 
                                                                    SortExpression="Cuenta">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="BancoCargo" SortExpression="BancoCargo">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("BancoCargo", "{0:C}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("BancoCargo") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblCargoBanco" runat="server" Text="$ 0.00"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BancoAbono" SortExpression="BancoAbono">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("BancoAbono", "{0:C}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("BancoAbono") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblAbonoBanco" runat="server" Text="$ 0.00"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CajaCargo" SortExpression="CajaCargo">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("CajaCargo", "{0:C}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("CajaCargo") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblCargoCajaChica" runat="server" Text="$ 0.00"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CajaAbono" SortExpression="CajaAbono">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("CajaAbono", "{0:C}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("CajaAbono") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblAbonoCajaChica" runat="server" Text="$ 0.00"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                                                                            CommandName="Delete" Text="Eliminar" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotales" runat="server" Text="$ 0.00"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <FooterStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:SqlDataSource ID="sdsPagosFactura" runat="server" 
                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                            
                                                            SelectCommand="SELECT * FROM [PagosFacturasCliente] WHERE ([FacturaCVID] = @FacturaCVID) ORDER BY [fecha] DESC" 
                                                            DeleteCommand="select 1;">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="txtFacturaIDToMod" DefaultValue="-1" 
                                                                    Name="FacturaCVID" PropertyName="Text" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkPagoEfectivo" runat="server" 
                                                            Text="Agregar pago en Efectivo" CssClass="TableHeader" />
                                                        <asp:Panel ID="pnlAddEfectivo" runat="server">
                                                            <asp:DetailsView ID="dvAddEfectivo" runat="server" AutoGenerateRows="False" 
                                                                DataKeyNames="movimientoID" DataSourceID="sdsEfectivo" DefaultMode="Insert" 
                                                                Height="50px" Width="125px" oniteminserted="dvAddEfectivo_ItemInserted" 
                                                                oniteminserting="dvAddEfectivo_ItemInserting">
                                                                <Fields>
                                                                    <asp:TemplateField HeaderText="Ciclo:" SortExpression="cicloID">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("cicloID") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <InsertItemTemplate>
                                                                            <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="sdsCiclos" 
                                                                                DataTextField="CicloName" DataValueField="cicloID" 
                                                                                SelectedValue='<%# Bind("cicloID") %>'>
                                                                            </asp:DropDownList>
                                                                        </InsertItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("cicloID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bodega:" SortExpression="bodegaID">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("bodegaID") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <InsertItemTemplate>
                                                                            <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="sdsBodegas" 
                                                                                DataTextField="bodega" DataValueField="bodegaID" 
                                                                                SelectedValue='<%# Bind("bodegaID") %>'>
                                                                            </asp:DropDownList>
                                                                        </InsertItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("bodegaID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Fecha:" SortExpression="fecha">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("fecha") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <InsertItemTemplate>
                                                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("fecha") %>'></asp:TextBox>
                                                                            <rjs:PopCalendar ID="PopCalendar7" runat="server" Control="TextBox3" 
                                                                                Separator="/" />
                                                                        </InsertItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("fecha") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nombre:" SortExpression="nombre">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("nombre") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <InsertItemTemplate>
                                                                            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("nombre") %>' 
                                                                                Width="300px"></asp:TextBox>
                                                                        </InsertItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("nombre") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="abono" HeaderText="Abono:" SortExpression="abono">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Catalogos:" SortExpression="catalogoMovBancoID">
                                                                        <EditItemTemplate>
                                                                        </EditItemTemplate>
                                                                        <InsertItemTemplate>
                                                                            <asp:DropDownList ID="ddlEfectivoGrupo" runat="server" AutoPostBack="True" 
                                                                                DataSourceID="sdsEfectivoGrupo" DataTextField="grupoCatalogo" 
                                                                                DataValueField="grupoCatalogosID" ondatabound="ddlEfectivoGrupo_DataBound" 
                                                                                onselectedindexchanged="ddlEfectivoGrupo_SelectedIndexChanged">
                                                                            </asp:DropDownList><br />
                                                                            <asp:SqlDataSource ID="sdsEfectivoGrupo" runat="server" 
                                                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                SelectCommand="SELECT * FROM [GruposCatalogosMovBancos] ORDER BY [grupoCatalogo]">
                                                                            </asp:SqlDataSource>
                                                                            <asp:DropDownList ID="ddlEfectivoCatalogo" runat="server" AutoPostBack="True" 
                                                                                DataSourceID="sdsEfectivoCatalogos" DataTextField="catalogoMovBanco" 
                                                                                DataValueField="catalogoMovBancoID" ondatabound="ddlEfectivoCatalogo_DataBound" 
                                                                                onselectedindexchanged="ddlEfectivoCatalogo_SelectedIndexChanged">
                                                                            </asp:DropDownList><br />
                                                                            <asp:SqlDataSource ID="sdsEfectivoCatalogos" runat="server" 
                                                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                SelectCommand="SELECT [catalogoMovBancoID], [grupoCatalogoID], [catalogoMovBanco] FROM [catalogoMovimientosBancos] ORDER BY [catalogoMovBanco]">
                                                                            </asp:SqlDataSource>
                                                                            <asp:DropDownList ID="ddlEfectivoSubCatalogo" runat="server" 
                                                                                DataSourceID="sdsEfectivoSubCatalogos" DataTextField="subCatalogo" 
                                                                                DataValueField="subCatalogoMovBancoID">
                                                                            </asp:DropDownList>
                                                                            <asp:SqlDataSource ID="sdsEfectivoSubCatalogos" runat="server" 
                                                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                SelectCommand="SELECT [subCatalogoMovBancoID], [subCatalogo], [catalogoMovBancoID] FROM [SubCatalogoMovimientoBanco] ORDER BY [subCatalogo]">
                                                                            </asp:SqlDataSource>
                                                                        </InsertItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("nombre") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                        
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Observaciones:" SortExpression="Observaciones">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox6" runat="server" 
                                                                                Text='<%# Bind("catalogoMovBancoID") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <InsertItemTemplate>
                                                                            
                                                                            <asp:TextBox ID="TextBox4" runat="server" Height="100px" 
                                                                                Text='<%# Bind("Observaciones") %>' TextMode="MultiLine" Width="300px"></asp:TextBox>
                                                                            
                                                                        </InsertItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("catalogoMovBancoID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Observaciones") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <InsertItemTemplate>
                                                                            <asp:TextBox ID="TextBox4" runat="server" Height="100px" 
                                                                                Text='<%# Bind("Observaciones") %>' TextMode="MultiLine" Width="300px"></asp:TextBox>
                                                                        </InsertItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("Observaciones") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:CommandField ShowInsertButton="True" ButtonType="Button" 
                                                                        InsertText="Insertar" ShowCancelButton="False" />
                                                                </Fields>
                                                            </asp:DetailsView>
                                                            <asp:SqlDataSource ID="sdsEfectivo" runat="server" 
                                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                DeleteCommand="DELETE FROM [MovimientosCaja] WHERE [movimientoID] = @movimientoID" 
                                                                InsertCommand="INSERT INTO MovimientosCaja(cicloID, userID, nombre, cargo, abono, Observaciones, fecha, catalogoMovBancoID, subCatalogoMovBancoID, bodegaID) VALUES (@cicloID, @userID, @nombre, @cargo, @abono, @Observaciones, @fecha, @catalogoMovBancoID, @subCatalogoMovBancoID, @bodegaID); set @newID = SCOPE_IDENTITY();INSERT INTO PagosFacturasClientesVenta (FacturaCVID, fecha, movCajaID, userID)VALUES (@FacturaCVID,@fecha,SCOPE_IDENTITY(),@userID)" 
                                                                SelectCommand="SELECT * FROM [MovimientosCaja]" 
                                                                
                                                                UpdateCommand="UPDATE [MovimientosCaja] SET [cicloID] = @cicloID, [userID] = @userID, [nombre] = @nombre, [cargo] = @cargo, [abono] = @abono, [Observaciones] = @Observaciones, [storeTS] = @storeTS, [updateTS] = @updateTS, [fecha] = @fecha, [catalogoMovBancoID] = @catalogoMovBancoID, [subCatalogoMovBancoID] = @subCatalogoMovBancoID, [facturaOlarguillo] = @facturaOlarguillo, [numCabezas] = @numCabezas, [bodegaID] = @bodegaID, [movOrigenID] = @movOrigenID, [cobrado] = @cobrado, [tipomonedaID] = @tipomonedaID WHERE [movimientoID] = @movimientoID" 
                                                                oninserted="sdsEfectivo_Inserted">
                                                                <DeleteParameters>
                                                                    <asp:Parameter Name="movimientoID" Type="Int32" />
                                                                </DeleteParameters>
                                                                <UpdateParameters>
                                                                    <asp:Parameter Name="cicloID" Type="Int32" />
                                                                    <asp:Parameter Name="userID" Type="Int32" />
                                                                    <asp:Parameter Name="nombre" Type="String" />
                                                                    <asp:Parameter Name="cargo" Type="Double" />
                                                                    <asp:Parameter Name="abono" Type="Double" />
                                                                    <asp:Parameter Name="Observaciones" Type="String" />
                                                                    <asp:Parameter Name="storeTS" Type="DateTime" />
                                                                    <asp:Parameter Name="updateTS" Type="DateTime" />
                                                                    <asp:Parameter Name="fecha" Type="DateTime" />
                                                                    <asp:Parameter Name="catalogoMovBancoID" Type="Int32" />
                                                                    <asp:Parameter Name="subCatalogoMovBancoID" Type="Int32" />
                                                                    <asp:Parameter Name="facturaOlarguillo" Type="String" />
                                                                    <asp:Parameter Name="numCabezas" Type="Double" />
                                                                    <asp:Parameter Name="bodegaID" Type="Int32" />
                                                                    <asp:Parameter Name="movOrigenID" Type="Int32" />
                                                                    <asp:Parameter Name="cobrado" Type="Boolean" />
                                                                    <asp:Parameter Name="tipomonedaID" Type="Int32" />
                                                                    <asp:Parameter Name="movimientoID" Type="Int32" />
                                                                </UpdateParameters>
                                                                <InsertParameters>
                                                                    <asp:Parameter Name="cicloID" Type="Int32" />
                                                                    <asp:Parameter Name="userID" Type="Int32" />
                                                                    <asp:Parameter Name="nombre" Type="String" />
                                                                    <asp:Parameter Name="cargo" Type="Double" />
                                                                    <asp:Parameter Name="abono" Type="Double" />
                                                                    <asp:Parameter Name="Observaciones" Type="String" />
                                                                    <asp:Parameter Name="storeTS" Type="DateTime" />
                                                                    <asp:Parameter Name="updateTS" Type="DateTime" />
                                                                    <asp:Parameter Name="fecha" Type="DateTime" />
                                                                    <asp:Parameter Name="catalogoMovBancoID" Type="Int32" />
                                                                    <asp:Parameter Name="subCatalogoMovBancoID" Type="Int32" />
                                                                    <asp:Parameter Name="facturaOlarguillo" Type="String" />
                                                                    <asp:Parameter Name="numCabezas" Type="Double" />
                                                                    <asp:Parameter Name="bodegaID" Type="Int32" />
                                                                    <asp:Parameter Name="movOrigenID" Type="Int32" />
                                                                    <asp:Parameter Name="cobrado" Type="Boolean" />
                                                                    <asp:Parameter Name="tipomonedaID" Type="Int32" />
                                                                    <asp:Parameter Name="newID" Type="Int32" Direction="Output" />
                                                                    <asp:Parameter Name="FacturaCVID" />
                                                                </InsertParameters>
                                                            </asp:SqlDataSource>
                                                            <asp:Label ID="lblEfectivoResult" runat="server" Font-Size="X-Large"></asp:Label>
                                                        </asp:Panel>
                                                        <cc1:CollapsiblePanelExtender ID="pnlAddEfectivo_CollapsiblePanelExtender" 
                                                            runat="server" CollapseControlID="chkPagoEfectivo" Collapsed="True" 
                                                            Enabled="True" ExpandControlID="chkPagoEfectivo" 
                                                            TargetControlID="pnlAddEfectivo">
                                                        </cc1:CollapsiblePanelExtender>
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkMostrarAgregarPago" runat="server" CssClass="TableHeader" 
                                                            Text="Mostrar Panel Para Agregar Nuevo Pago de Banco" />
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
                                                    <td>
                                                        <asp:Panel ID="pnlNewPago" runat="server">
                                                            <asp:Image ID="imgBienPago" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                                                            <asp:Image ID="imgMalPago" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                                                            <asp:Label ID="lblNewPagoResult" runat="server"></asp:Label>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </td>
                    	</tr>
                    </table>
                </asp:Panel>
                </td>
            </tr>
            
            <tr>
                <td colspan="3">
                    <table >
                        <tr>
   		 	<td>
   		 	    &nbsp;</td>
   		 	 <td colspan="1">
   		 	     &nbsp;</td>
   		 	            </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
    </table>

</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

<script type="text/javascript" src="/scripts/divFunctions.js"></script>
    <script type="text/javascript" src="/scripts/prototype.js"></script>
</asp:Content>
