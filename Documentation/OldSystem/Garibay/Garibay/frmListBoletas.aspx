<%@ Page Language="C#" Theme="skinrojo" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListBoletas.aspx.cs" Inherits="Garibay.WebForm10" Title="Lista de Boletas" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
    <tr>
        <td align="center" class="TableHeader">
       LISTA DE BOLETAS
        </td>
    </tr>
    <tr>
        <td>
       
            <asp:Panel ID="pnlBoletas" runat="server" GroupingText="Filtros">
            <table>
                <tr>
                    <td class="TablaField">
                        <asp:RadioButton ID="rbProductor" runat="server" Checked="True" 
                            GroupName="filtroProdCliente" Text="Productor:" />
                        &nbsp;</td>
                    <td>
                        <asp:DropDownList ID="drpdlfiltroProductor" runat="server" 
                            DataSourceID="sdsProductores" DataTextField="name" 
                            DataValueField="productorID" ondatabound="drpdlfiltroProductor_DataBound">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsProductores" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            
                            SelectCommand="SELECT DISTINCT Boletas.productorID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS name FROM Productores INNER JOIN Boletas ON Productores.productorID = Boletas.productorID WHERE (Boletas.cicloID = @cicloID) ORDER BY name">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="drpdlCiclo" DefaultValue="-1" Name="cicloID" 
                                    PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                    <td class="TablaField">
                    Ciclo:
                    </td>
                    <td>
                        <asp:DropDownList ID="drpdlCiclo" runat="server" 
                            AutoPostBack="True" DataSourceID="sdsCiclos" DataTextField="CicloName" 
                            DataValueField="cicloID" 
                            onselectedindexchanged="drpdlCiclo_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [fechaFinZona1] DESC">
                        </asp:SqlDataSource>
                    </td>
                    
                </tr>
                <tr>
                    <td class="TablaField">
                        <asp:RadioButton ID="rbClienteVenta" runat="server" 
                            GroupName="filtroProdCliente" Text="Cliente de Venta" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlClientesVenta" runat="server" 
                            DataSourceID="sdsClientesVenta" DataTextField="nombre" 
                            DataValueField="clienteventaID">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsClientesVenta" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            SelectCommand="SELECT DISTINCT ClientesVentas.clienteventaID, ClientesVentas.nombre FROM ClientesVentas INNER JOIN ClienteVenta_Boletas ON ClientesVentas.clienteventaID = ClienteVenta_Boletas.clienteventaID ORDER BY ClientesVentas.nombre">
                        </asp:SqlDataSource>
                    </td>
                    <td class="TablaField">
                        # de boleta:</td>
                    <td>
                        <asp:TextBox ID="txtNumBoleta" runat="server" Width="120px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TablaField">
                        <asp:RadioButton ID="rbProvGanado" runat="server" GroupName="filtroProdCliente" 
                            Text="Ganadero:" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGanaderos" runat="server" DataSourceID="sdsGanaderos" 
                            DataTextField="Nombre" DataValueField="ganProveedorID">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsGanaderos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            SelectCommand="SELECT DISTINCT gan_Proveedores.ganProveedorID, gan_Proveedores.Nombre FROM gan_Proveedores INNER JOIN gan_Proveedores_Boletas ON gan_Proveedores.ganProveedorID = gan_Proveedores_Boletas.ganProveedorID ORDER BY gan_Proveedores.Nombre">
                        </asp:SqlDataSource>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                 <tr>
                    <td class="TablaField">
                        <asp:RadioButton ID="rbProveedor" runat="server" GroupName="filtroProdCliente" 
                            Text="Proveedor:" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownListProveedor" runat="server" DataSourceID="sdsProveedores" 
                            DataTextField="Nombre" DataValueField="proveedorId">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsProveedores" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            SelectCommand="SELECT DISTINCT Proveedores.proveedorID, Proveedores.Nombre FROM Proveedores INNER JOIN boleta_proveedor ON boleta_proveedor.proveedorID = Proveedores.proveedorId 
                                           INNER JOIN Boletas ON Boletas.BoletaID = boleta_proveedor.boletaId ORDER BY Proveedores.Nombre">
                        </asp:SqlDataSource>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                    <table>
                        <tr>
                            <td class="TablaField">
                                Producto:
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlProductos" runat="server" 
                                    onselectedindexchanged="ddlProductos_SelectedIndexChanged" 
                                    DataSourceID="sdsProductos" DataTextField="Nombre" 
                                    DataValueField="productoID" ondatabound="ddlProductos_DataBound">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsProductos" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    
                                    
                                    SelectCommand="SELECT DISTINCT dbo.Boletas.productoID, LTRIM(dbo.Productos.Nombre + SPACE(1) + dbo.Presentaciones.Presentacion + '(' + dbo.Unidades.Unidad + ')') AS Nombre FROM dbo.Productos INNER JOIN dbo.Boletas ON dbo.Productos.productoID = dbo.Boletas.productoID INNER JOIN dbo.Unidades ON dbo.Productos.unidadID = dbo.Unidades.unidadID INNER JOIN dbo.Presentaciones ON dbo.Productos.presentacionID = dbo.Presentaciones.presentacionID ORDER BY Nombre">
                                </asp:SqlDataSource>
                            </td>
                            <td class="TablaField">
                                Tipo de Boleta:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdlTipo" runat="server" Height="23px" Width="155px">
                                    <asp:ListItem Value="0">TODOS LOS TIPOS</asp:ListItem>
                                    <asp:ListItem Value="1">ENTRADA</asp:ListItem>
                                    <asp:ListItem Value="2">SALIDA</asp:ListItem>
                                </asp:DropDownList>
                                                  
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Bodega:</td>
                            <td colspan="2">
                                <asp:DropDownList ID="drpdlBodega" runat="server" 
                                    DataSourceID="sdsBodegaFilters" DataTextField="bodega" 
                                    DataValueField="bodegaID">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsBodegaFilters" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT distinct (Boletas.bodegaID),  Bodegas.bodega FROM Bodegas INNER JOIN Boletas ON Bodegas.bodegaID = Boletas.bodegaID WHERE (Boletas.cicloID = @cicloID)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="drpdlCiclo" DefaultValue="-1" Name="cicloID" 
                                            PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                <asp:CheckBox ID="chkPeriodoFilter" runat="server" Text="Periodo:" />
                            </td>
                            <td class="TablaField">
                                Inicio:</td>
                            <td>
                                <asp:TextBox ID="txtFechaIni" runat="server"></asp:TextBox>
                                <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtFechaIni" 
                                    Separator="/" />
                            </td>
                            <td class="TablaField">
                                Fin:<asp:TextBox ID="txtFechaFin" runat="server"></asp:TextBox>
                                <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtFechaFin" 
                                    Separator="/" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnFiltrar" runat="server" onclick="btnFiltrar_Click" 
                            Text="Filtrar Resultados" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
       </td>
    </tr>
    <tr>
        <td align="center">
            
                        <asp:GridView ID="gvBoletas" runat="server" AutoGenerateColumns="False" 
                            
                            onselectedindexchanged="gvBoletas_SelectedIndexChanged" 
                            DataSourceID="sdsBoletas" DataKeyNames="boletaID,LiquidacionID" ondatabound="gvBoletas_DataBound" 
                            ShowFooter="True">
                            <Columns>
                                <asp:CommandField ButtonType="Button" SelectText=" &gt; " 
                                    ShowSelectButton="True" />
                                <asp:BoundField DataField="NumeroBoleta" HeaderText="# de boleta" 
                                    SortExpression="NumeroBoleta" />
                                <asp:BoundField DataField="bodega" HeaderText="Bodega" 
                                    SortExpression="bodega" />
                                <asp:BoundField DataField="Ticket" HeaderText="Ticket" 
                                    SortExpression="Ticket" />
                                <asp:BoundField DataField="FechaEntrada" DataFormatString="{0:dd/MM/yyyy}" 
                                    HeaderText="Fecha" SortExpression="FechaEntrada" />
                                <asp:BoundField DataField="name" HeaderText="Productor" SortExpression="name" >
                                <ItemStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nombre" HeaderText="Descripción" 
                                    SortExpression="Nombre" >
                                <ItemStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Peso neto de entrada" 
                                    SortExpression="pesonetoentrada">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("pesonetoentrada") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblPesoEntradaTotal" runat="server" Text="0.00"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" 
                                            Text='<%# Bind("pesonetoentrada", "{0:n}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Peso neto de salida" 
                                    SortExpression="pesonetosalida">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("pesonetosalida") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblPesoSalidaTotal" runat="server" Text="0.00"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" 
                                            Text='<%# Bind("pesonetosalida", "{0:n}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cabezas de Ganado" 
                                    SortExpression="cabezasDeGanado">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("cabezasDeGanado") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblCabezasTotal" runat="server" Text="0"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("cabezasDeGanado") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Humedad" SortExpression="humedad">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("humedad") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblHumedadAVG" runat="server" Text="0.00"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("humedad", "{0:n}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dcto de humedad" SortExpression="dctoHumedad">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("dctoHumedad") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblDctoHumedadTotal" runat="server" Text="0.00"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" 
                                            Text='<%# Bind("dctoHumedad", "{0:n}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Impurezas" SortExpression="impurezas">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("impurezas") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblImpurezasAVG" runat="server" Text="0.00"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("impurezas", "{0:n}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dcto de impurezas" 
                                    SortExpression="dctoImpurezas">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("dctoImpurezas") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblDctoImpurezasTotal" runat="server" Text="0.00"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" 
                                            Text='<%# Bind("dctoImpurezas", "{0:n}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dcto de secado" SortExpression="dctoSecado">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("dctoSecado") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblSecadoTotal" runat="server" Text="$ 0.00"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("dctoSecado", "{0:c}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="productoID" HeaderText="productoID" 
                                    SortExpression="productoID" Visible="False" />
                                <asp:BoundField DataField="productorID" HeaderText="productorID" 
                                    SortExpression="productorID" Visible="False" />
                                <asp:BoundField DataField="LiquidacionID" HeaderText="Liquidacion" 
                                    SortExpression="LiquidacionID" Visible="True" />
                                <asp:TemplateField HeaderText="" SortExpression="LiquidacionID">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("LiquidacionID") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkLiq" runat="server" 
                                            NavigateUrl='<%# GetNewOpenLiqUrl(Eval("LiquidacionID").ToString()) %>' 
                                            Visible='<%# GetLiqLinkVisible(Eval("LiquidacionID").ToString()) %>'>Abrir</asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="bodegaID" HeaderText="bodegaID" 
                                    SortExpression="bodegaID" Visible="False" />
                                <asp:TemplateField HeaderText="Factura de Cliente" SortExpression="FacturaCV">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkFacturaCV" runat="server" 
                                            NavigateUrl='<%# GetOpenLiqFacturaCV(Eval("FacturaCV").ToString()) %>' Text='<%# Eval("FacturaCV") %>' 
                                            Visible='<%# GetFacturaLinkVisible(Eval("FacturaCV").ToString()) %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("FacturaCV") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CheckBoxField DataField="Pagada" HeaderText="Pagada/Cobrada" 
                                    SortExpression="Pagada" />
                            </Columns>
                        </asp:GridView>
            
                        <asp:SqlDataSource ID="sdsBoletas" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            
                            
                            
                            
                            
                            
                            
                            SelectCommand="
                            SELECT Boletas.NumeroBoleta, 
       Boletas.Ticket,                             
       ISNULL(Proveedores.Nombre,ISNULL(gan_Proveedores.Nombre, ISNULL(ClientesVentas.nombre, LTRIM(Productores.apaterno + SPACE(1) + Productores.amaterno + SPACE(1) + Productores.nombre)))) AS name, 
       Productos.Nombre, Boletas.pesonetoentrada, Boletas.pesonetosalida, Boletas.humedad, Boletas.dctoHumedad, Boletas.impurezas, Boletas.dctoImpurezas, Boletas.dctoSecado, Boletas.productoID, 
       Boletas.productorID, Boletas.boletaID, Liquidaciones_Boletas.LiquidacionID, Boletas.bodegaID, Bodegas.bodega, Boletas.FechaEntrada, Boletas.cabezasDeGanado, ClienteVenta_Boletas.clienteventaID, 
       gan_Proveedores.ganProveedorID, boleta_proveedor.proveedorId, FacturasCV_Boletas.FacturaCV, 
       ISNULL(Liquidaciones.cobrada, ISNULL(FacturasClientesVenta.pagada, CONVERT (Bit, 0))) AS Pagada 
       FROM gan_Proveedores INNER JOIN gan_Proveedores_Boletas ON gan_Proveedores.ganProveedorID = gan_Proveedores_Boletas.ganProveedorID 
       RIGHT OUTER JOIN boleta_proveedor
	   INNER JOIN Proveedores ON boleta_proveedor.proveedorID = Proveedores.proveedorID
      
       RIGHT OUTER JOIN FacturasCV_Boletas 
       INNER JOIN FacturasClientesVenta ON FacturasCV_Boletas.FacturaCV = FacturasClientesVenta.FacturaCV 
       RIGHT OUTER JOIN Boletas 
       INNER JOIN Productos ON Boletas.productoID = Productos.productoID 
       INNER JOIN Bodegas ON Boletas.bodegaID = Bodegas.bodegaID 
          ON FacturasCV_Boletas.boletaID = Boletas.boletaID 
          ON boleta_proveedor.boletaId = Boletas.boletaId
          ON gan_Proveedores_Boletas.boletaID = Boletas.boletaID 
          
       LEFT OUTER JOIN Productores ON Boletas.productorID = Productores.productorID LEFT OUTER JOIN Liquidaciones INNER JOIN Liquidaciones_Boletas ON Liquidaciones.LiquidacionID = Liquidaciones_Boletas.LiquidacionID ON Boletas.boletaID = Liquidaciones_Boletas.BoletaID LEFT OUTER JOIN ClientesVentas INNER JOIN ClienteVenta_Boletas ON ClientesVentas.clienteventaID = ClienteVenta_Boletas.clienteventaID ON Boletas.boletaID = ClienteVenta_Boletas.BoletaID
       WHERE (Boletas.cicloID = @cicloID)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="drpdlCiclo" DefaultValue="-1" Name="cicloID" 
                                    PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
            
        </td>
    </tr>
    <tr>
        <td>
                             
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
                onclick="btnAgregar_Click" />
            <asp:Button ID="btnModificar" runat="server" Text="Modificar" 
                onclick="btnModificar_Click" />
                             
            <asp:Button ID="btnEliminarBoleta" runat="server" 
                onclick="btnEliminarBoleta_Click" Text="Eliminar" />
                             
        </td>
    </tr>
</table>
            </asp:Content>
