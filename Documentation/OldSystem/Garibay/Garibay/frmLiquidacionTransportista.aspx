<%@ Page Title="LIQUIDACION DE TRANSPORTISTA" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmLiquidacionTransportista.aspx.cs" Inherits="Garibay.frmLiquidacionTransportista" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="Server" id="updatePanelCompleto">
    <ContentTemplate>
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
        	<tr>
        		<td align="right">
        		    <table>
        		    	<tr>
        		    		<td class="TablaField">LIQUIDACION:</td>
        		    		<td><asp:Label ID="lblLiqNum" runat="server" Text=""></asp:Label></td>
        		    	</tr>
        		        <tr>
                            <td class="TablaField">FECHA:</td>
                            <td>
                                <asp:TextBox ID="txtFecha" runat="server" ReadOnly="True"></asp:TextBox>
                                <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtFecha" 
                                    Separator="/" />
                            </td>
                        </tr>
        		    </table>
        		</td>
        	</tr>
        </table>
        <table>
            <tr>
                <td class="TablaField">TRANSPORTISTA:</td>
                <td>
                    <asp:DropDownList ID="ddlTransportista" runat="server" 
                        DataSourceID="sdsTransportistas" DataTextField="Transportista" 
                        DataValueField="transportistaID" AutoPostBack="True" 
                        onselectedindexchanged="ddlTransportista_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsTransportistas" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT transportistaID, LTRIM(apaterno + SPACE(1) + amaterno + SPACE(1) + nombres) AS Transportista FROM Transportistas ORDER BY Transportista">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:DetailsView ID="dvTransportista" runat="server" Height="50px" 
                        Width="125px" AutoGenerateRows="False" DataKeyNames="transportistaID" 
                        DataSourceID="sdsTransportistaData">
                        <Fields>
                            <asp:BoundField DataField="Domicilio" HeaderText="Domicilio" 
                                SortExpression="Domicilio">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Poblacion" HeaderText="Poblacion" 
                                SortExpression="Poblacion">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                        </Fields>
                    </asp:DetailsView>
                    <asp:SqlDataSource ID="sdsTransportistaData" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT [transportistaID], [Domicilio], [Poblacion] FROM [Transportistas] WHERE ([transportistaID] = @transportistaID)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlTransportista" DefaultValue="-1" 
                                Name="transportistaID" PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
        	<tr>
        		<td align="right"><asp:Button ID="btnNewLiq" runat="server" 
                        Text="Agregar Nueva Liquidacion" onclick="btnNewLiq_Click" /></td>
        	</tr>
        </table>
        <asp:Panel runat="Server" id="pnlCentral" Visible="False">
            <table border="0" cellspacing="0" cellpadding="0" width="100%">
            	<tr>
            		<td>
                        <asp:CheckBox ID="chkAddBoletas" runat="server" Text="Agregar Boletas a la Liquidacion" />
                        <asp:Panel runat="Server" id="pnlAddBoletas">
                            <table>
                            	<tr>
                            		<td class="TablaField">CLIENTE:</td>
                            		<td>
                                        <asp:DropDownList ID="ddlClientes" runat="server" DataSourceID="sdsClientes" 
                                            DataTextField="nombre" DataValueField="clienteventaID" AutoPostBack="True" 
                                            onselectedindexchanged="ddlClientes_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsClientes" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            SelectCommand="SELECT [clienteventaID], [nombre] FROM [ClientesVentas] ORDER BY [nombre]">
                                        </asp:SqlDataSource>
                                    </td>
                            	</tr>
                                <tr>
                                    <td colspan="2">
                                        
                                        <asp:GridView ID="gvBoletasClientes" runat="server" AutoGenerateColumns="False" 
                                            DataKeyNames="boletaID" DataSourceID="sdsBoletasCliente">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkAddBoleta" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="boletaID" HeaderText="Boleta ID" 
                                                    InsertVisible="False" ReadOnly="True" SortExpression="boletaID">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Producto" HeaderText="Producto" ReadOnly="True" 
                                                    SortExpression="Producto" />
                                                <asp:BoundField DataField="FechaEntrada" DataFormatString="{0:dd/MM/yyyy}" 
                                                    HeaderText="Fecha" SortExpression="FechaEntrada" />
                                                <asp:BoundField DataField="PesoDeEntrada" DataFormatString="{0:N2}" 
                                                    HeaderText="Peso De Entrada" SortExpression="PesoDeEntrada">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PesoDeSalida" DataFormatString="{0:N2}" 
                                                    HeaderText="Peso De Salida" SortExpression="PesoDeSalida">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="pesonetosalida" DataFormatString="{0:N2}" 
                                                    HeaderText="Peso Neto Salida" SortExpression="pesonetosalida">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FolioDestino" HeaderText="Folio Destino" 
                                                    SortExpression="FolioDestino">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PesoDestino" DataFormatString="{0:N2}" 
                                                    HeaderText="Peso Destino" SortExpression="PesoDestino">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="sdsBoletasCliente" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            SelectCommand="SELECT DISTINCT ClienteVenta_Boletas.clienteventaID, Boletas.boletaID, vProductosParaDDL.Producto, Boletas.FechaEntrada, Boletas.PesoDeEntrada, Boletas.PesoDeSalida, Boletas.pesonetosalida, Boletas.FolioDestino, Boletas.PesoDestino FROM Boletas INNER JOIN ClienteVenta_Boletas ON Boletas.boletaID = ClienteVenta_Boletas.BoletaID INNER JOIN vProductosParaDDL ON Boletas.productoID = vProductosParaDDL.productoID WHERE (ClienteVenta_Boletas.clienteventaID = @clienteventaID) AND (Boletas.boletaID NOT IN (SELECT boletaID FROM LiqTransportista_Boletas))">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="ddlClientes" Name="clienteventaID" 
                                                    PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        <asp:Button ID="btnAddBoletasLiq" runat="server" 
                                            onclick="btnAddBoletasLiq_Click" Text="Agregar Boletas a Liquidacion" />
                                        
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
            		    <cc1:CollapsiblePanelExtender ID="pnlAddBoletas_CollapsiblePanelExtender" 
                            runat="server" CollapseControlID="chkAddBoletas" Collapsed="True" 
                            Enabled="True" ExpandControlID="chkAddBoletas" TargetControlID="pnlAddBoletas">
                        </cc1:CollapsiblePanelExtender>
            		</td>
            	</tr>
            </table>
            
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="gvBoletas" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="boletaID" DataSourceID="sdsBoletasLiq" 
                            onrowdeleted="gvBoletas_RowDeleted" onrowupdated="gvBoletas_RowUpdated">
                                <Columns>
                                    <asp:CommandField ButtonType="Button" CancelText="Cancelar" DeleteText="Quitar" 
                                        EditText="Editar" ShowDeleteButton="True" ShowEditButton="True" />
                                    <asp:BoundField DataField="boletaID" HeaderText="BoletaID" 
                                        SortExpression="boletaID" ReadOnly="True">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FechaEntrada" DataFormatString="{0:dd/MM/yyyy}" 
                                        HeaderText="Fecha" SortExpression="FechaEntrada" />
                                    <asp:BoundField DataField="bodega" HeaderText="Bodega" SortExpression="bodega" 
                                        ReadOnly="True">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="chofer" HeaderText="Chofer" SortExpression="chofer" 
                                        ReadOnly="True">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Nombre" HeaderText="Producto" 
                                        SortExpression="Nombre" ReadOnly="True">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Ticket" HeaderText="Ticket" 
                                        SortExpression="Ticket" />
                                    <asp:BoundField DataField="FolioDestino" HeaderText="Folio Destino" 
                                        SortExpression="FolioDestino">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PesoDeEntrada" DataFormatString="{0:N2}" 
                                        HeaderText="Bruto" SortExpression="PesoDeEntrada">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PesoDeSalida" DataFormatString="{0:N2}" 
                                        HeaderText="Tara" SortExpression="PesoDeSalida">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="pesonetosalida" DataFormatString="{0:N2}" 
                                        HeaderText="Peso Origen" SortExpression="pesonetosalida">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PesoDestino" DataFormatString="{0:N2}" 
                                        HeaderText="Peso Destino" SortExpression="PesoDestino">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Merma" DataFormatString="{0:N2}" HeaderText="Merma" 
                                        ReadOnly="True" SortExpression="Merma">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PrecioTransportista" DataFormatString="{0:C2}" 
                                        HeaderText="$ TON" SortExpression="PrecioTransportista">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Importe" DataFormatString="{0:C2}" 
                                        HeaderText="Importe" ReadOnly="True" SortExpression="Importe">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PrecioPorKgTransportista" DataFormatString="{0:c2}" 
                                        HeaderText="Precio por kg. Transportista" 
                                        SortExpression="PrecioPorKgTransportista">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="A_Pagar" HeaderText="A Pagar" 
                                        SortExpression="A_Pagar" DataFormatString="{0:C2}" >
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:CheckBoxField DataField="UsaPesoDestinoParaTransportista" 
                                        HeaderText="Usar peso destino" SortExpression="UsaPesoDestinoParaTransportista">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:CheckBoxField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="sdsBoletasLiq" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                DeleteCommand="DELETE FROM LiqTransportista_Boletas WHERE (boletaID = @boletaID)" 
                                SelectCommand="SELECT LiqTransportistaID, FechaEntrada, bodega, chofer, Nombre, Ticket, FolioDestino, PesoDeEntrada, PesoDeSalida, pesonetosalida, PesoDestino, Merma, PrecioTransportista, Importe, UsaPesoDestinoParaTransportista, boletaID, PrecioPorKgTransportista, PrecioPorKgTransportista*pesonetosalida as A_Pagar FROM LiqTransDetalles WHERE (LiqTransportistaID = @LiqTransportistaID) ORDER BY FechaEntrada" 
                                
                            
                            
                            UpdateCommand="UPDATE Boletas SET FolioDestino = @FolioDestino, PesoDestino = @PesoDestino, PrecioTransportista = @PrecioTransportista, UsaPesoDestinoParaTransportista = @UsaPesoDestinoParaTransportista,  pesonetosalida = @pesonetosalida, PrecioPorKgTransportista= @PrecioPorKgTransportista WHERE (boletaID = @boletaID)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="lblLiqNum" DefaultValue="-1" 
                                        Name="LiqTransportistaID" PropertyName="Text" />
                                </SelectParameters>
                                <DeleteParameters>
                                    <asp:Parameter Name="boletaID" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="FolioDestino" />
                                    <asp:Parameter Name="PesoDestino" />
                                    <asp:Parameter Name="PrecioTransportista" />
                                    <asp:Parameter Name="UsaPesoDestinoParaTransportista" />
                                    <asp:Parameter Name="pesonetosalida" />
                                    <asp:Parameter Name="PrecioPorKgTransportista" />
                                    <asp:Parameter Name="boletaID" />
                                </UpdateParameters>
                            </asp:SqlDataSource>
                        
                    
                    </td>
                </tr>
                <tr>
                    <td class="TableHeader">
                        TOTALES DE TRANSPORTISTA</td>
                </tr>
                <tr>
                    <td>
                        <asp:DetailsView ID="DetailsViewEstadoGeneral" runat="server" 
                            AutoGenerateRows="False" Height="50px" Width="125px" 
                            DataSourceID="sdsTotalesTransportista">
                            <Fields>
                                <asp:BoundField DataField="Total" DataFormatString="{0:c2}" HeaderText="Total" 
                                    SortExpression="Total">
                                    <HeaderStyle Font-Bold="True" Font-Size="Large" />
                                    <ItemStyle Font-Bold="True" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalPagos" DataFormatString="{0:C2}" 
                                    HeaderText="Total en pagos" SortExpression="TotalPagos">
                                    <HeaderStyle Font-Bold="True" Font-Size="Large" />
                                    <ItemStyle Font-Bold="True" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalDebe" DataFormatString="{0:c2}" 
                                    HeaderText="Total debe" SortExpression="TotalDebe">
                                    <HeaderStyle Font-Bold="True" Font-Size="Large" />
                                    <ItemStyle Font-Bold="True" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LiqTransportistaID" HeaderText="LiqTransportistaID" 
                                    ReadOnly="True" SortExpression="LiqTransportistaID" Visible="False" />
                            </Fields>
                        </asp:DetailsView>
                        <asp:SqlDataSource ID="sdsTotalesTransportista" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            SelectCommand="SELECT [Total], [TotalPagos], [TotalDebe], [LiqTransportistaID] FROM [vTotalesLiquidacionTransportista] WHERE ([LiqTransportistaID] = @LiqTransportistaID)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="lblLiqNum" DefaultValue="-1" 
                                    Name="LiqTransportistaID" PropertyName="Text" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td class="TableHeader">
                        PAGOS A LA LIQUIDACION </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvPagos" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="pagoTransportistaId" DataSourceID="sdsPagos" 
                            onrowdeleted="gvPagos_RowDeleted">
                            <Columns>
                                <asp:CommandField ButtonType="Button" DeleteText="Eliminar" 
                                    ShowDeleteButton="True" />
                                <asp:BoundField DataField="pagoTransportistaId" 
                                    HeaderText="pagoTransportistaId" InsertVisible="False" ReadOnly="True" 
                                    SortExpression="pagoTransportistaId" Visible="False" />
                                <asp:BoundField DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                    HeaderText="Fecha" ReadOnly="True" SortExpression="Fecha" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="True" 
                                    SortExpression="Nombre" />
                                <asp:BoundField DataField="Monto" DataFormatString="{0:c2}" HeaderText="Monto" 
                                    ReadOnly="True" SortExpression="Monto">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="sdsPagos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            SelectCommand="SELECT PagosLiquidacionTransportista.pagoTransportistaId, ISNULL(MovimientosCaja.fecha, MovimientosCuentasBanco.fecha) AS Fecha, ISNULL(MovimientosCaja.nombre, MovimientosCuentasBanco.nombre) AS Nombre, ISNULL(MovimientosCaja.cargo, MovimientosCuentasBanco.cargo) AS Monto FROM PagosLiquidacionTransportista LEFT OUTER JOIN MovimientosCaja ON PagosLiquidacionTransportista.movCajaId = MovimientosCaja.movimientoID LEFT OUTER JOIN MovimientosCuentasBanco ON PagosLiquidacionTransportista.movBanId = MovimientosCuentasBanco.movbanID WHERE (PagosLiquidacionTransportista.liquidaciontransportistaid = @liquidaciontransportistaid)" 
                            DeleteCommand="DELETEPAGOFACTURATRANSPORTISTA" 
                            DeleteCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="lblLiqNum" Name="liquidaciontransportistaid" 
                                    PropertyName="Text" />
                            </SelectParameters>
                            <DeleteParameters>
                                <asp:Parameter Name="pagoTransportistaId" Type="Int32" />
                            </DeleteParameters>
                        </asp:SqlDataSource>
                        <asp:Label ID="lblPagoDeleteResult" runat="server" Enabled="False" 
                            Font-Size="X-Large"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TableHeader">
                        <asp:CheckBox ID="chkEfectivo" runat="server" Text="AGREGAR PAGO EN EFECTIVO" />
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:Panel ID = "pnlEfectivo" runat="Server">
                        <asp:DetailsView ID="dvAddEfectivo" runat="server" AutoGenerateRows="False" 
                            DataKeyNames="movimientoID" DataSourceID="sdsEfectivo" DefaultMode="Insert" 
                            Height="50px" oniteminserted="dvAddEfectivo_ItemInserted" 
                            oniteminserting="dvAddEfectivo_ItemInserting" Width="125px">
                            <Fields>
                                <asp:TemplateField HeaderText="Ciclo:" SortExpression="cicloID">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("cicloID") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="sdsCiclos" 
                                            DataTextField="CicloName" DataValueField="cicloID" 
                                            SelectedValue='<%# Bind("cicloID") %>'>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            SelectCommand="SELECT cicloID, CicloName FROM Ciclos WHERE (cerrado = @cerrado) ORDER BY fechaInicio DESC">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="FALSE" Name="cerrado" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
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
                                        <asp:DropDownList ID="DropDownList5" runat="server" DataSourceID="sdsBodegas" 
                                            DataTextField="bodega" DataValueField="bodegaID" 
                                            SelectedValue='<%# Bind("bodegaID") %>'>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsBodegas" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
                                        </asp:SqlDataSource>
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
                                        <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("fecha") %>'></asp:TextBox>
                                        <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="TextBox16" 
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
                                        <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("nombre") %>' 
                                            Width="300px"></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("nombre") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="cargo" HeaderText="Cargo:" SortExpression="cargo">
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
                                        </asp:DropDownList>
                                        <br />
                                        <asp:SqlDataSource ID="sdsEfectivoGrupo" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            SelectCommand="SELECT * FROM [GruposCatalogosMovBancos] ORDER BY [grupoCatalogo]">
                                        </asp:SqlDataSource>
                                        <asp:DropDownList ID="ddlEfectivoCatalogo" runat="server" AutoPostBack="True" 
                                            DataSourceID="sdsEfectivoCatalogos" DataTextField="catalogoMovBanco" 
                                            DataValueField="catalogoMovBancoID" ondatabound="ddlEfectivoCatalogo_DataBound" 
                                            onselectedindexchanged="ddlEfectivoCatalogo_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <br />
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
                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("nombre") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Observaciones:" SortExpression="Observaciones">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox18" runat="server" 
                                            Text='<%# Bind("catalogoMovBancoID") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="TextBox19" runat="server" Height="100px" 
                                            Text='<%# Bind("Observaciones") %>' TextMode="MultiLine" Width="300px"></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label14" runat="server" Text='<%# Bind("catalogoMovBancoID") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox20" runat="server" Text='<%# Bind("Observaciones") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="TextBox21" runat="server" Height="100px" 
                                            Text='<%# Bind("Observaciones") %>' TextMode="MultiLine" Width="300px"></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label15" runat="server" Text='<%# Bind("Observaciones") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:CommandField ButtonType="Button" InsertText="Insertar" 
                                    ShowCancelButton="False" ShowInsertButton="True" />
                            </Fields>
                        </asp:DetailsView>
                        <asp:Label ID="lblEfectivoResult" runat="server" Font-Size="X-Large"></asp:Label>
                        <asp:SqlDataSource ID="sdsEfectivo" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            DeleteCommand="DELETE FROM [MovimientosCaja] WHERE [movimientoID] = @movimientoID" 
                            InsertCommand="INSERT INTO MovimientosCaja(cicloID, userID, nombre, cargo, abono, Observaciones, fecha, catalogoMovBancoID, subCatalogoMovBancoID, bodegaID) VALUES (@cicloID, @userID, @nombre, @cargo, @abono, @Observaciones, @fecha, @catalogoMovBancoID, @subCatalogoMovBancoID, @bodegaID); set @newID = SCOPE_IDENTITY();INSERT INTO PagosLiquidacionTransportista(liquidaciontransportistaid, movCajaID, userID)VALUES (@liquidaciontransportistaid,SCOPE_IDENTITY(),@userID)" 
                            SelectCommand="SELECT * FROM [MovimientosCaja]" 
                            UpdateCommand="UPDATE [MovimientosCaja] SET [cicloID] = @cicloID, [userID] = @userID, [nombre] = @nombre, [cargo] = @cargo, [abono] = @abono, [Observaciones] = @Observaciones, [storeTS] = @storeTS, [updateTS] = @updateTS, [fecha] = @fecha, [catalogoMovBancoID] = @catalogoMovBancoID, [subCatalogoMovBancoID] = @subCatalogoMovBancoID, [facturaOlarguillo] = @facturaOlarguillo, [numCabezas] = @numCabezas, [bodegaID] = @bodegaID, [movOrigenID] = @movOrigenID, [cobrado] = @cobrado, [tipomonedaID] = @tipomonedaID WHERE [movimientoID] = @movimientoID">
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
                                <asp:Parameter Name="fecha" Type="DateTime" />
                                <asp:Parameter Name="catalogoMovBancoID" Type="Int32" />
                                <asp:Parameter Name="subCatalogoMovBancoID" Type="Int32" />
                                <asp:Parameter Name="bodegaID" Type="Int32" />
                                <asp:Parameter Name="newID" />
                                <asp:Parameter Name="liquidaciontransportistaid" />
                            </InsertParameters>
                        </asp:SqlDataSource>
                        </asp:Panel>
                        <cc1:CollapsiblePanelExtender ID="pnlEfectivo_CollapsiblePanelExtender" 
                            runat="server" CollapseControlID="chkEfectivo" Collapsed="True" Enabled="True" 
                            ExpandControlID="chkEfectivo" TargetControlID="pnlEfectivo">
                        </cc1:CollapsiblePanelExtender>
                    </td>
                </tr>
                <tr>
                    <td class="TableHeader">
                        <asp:CheckBox ID="chkBanco" runat="server" Text="AGREGAR PAGO DE BANCO" />
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:Panel id = "pnlBancos" runat="Server">
                        <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" 
                            DataKeyNames="movbanID" DataSourceID="sdsMovimientoDeBanco" 
                            DefaultMode="Insert" Height="50px" oniteminserted="DetailsView1_ItemInserted" 
                            oniteminserting="DetailsView1_ItemInserting" 
                            onpageindexchanging="DetailsView1_PageIndexChanging" Width="125px">
                            <Fields>
                                <asp:TemplateField HeaderText="Fecha:" SortExpression="fecha">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("fecha") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="TextBox8" runat="server" ReadOnly="True" 
                                            Text='<%# Bind("fecha", "{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                        <popcalendar id="PopCalendar8" runat="server" control="TextBox1" 
                                            separator="/" />
                                        <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="TextBox8" 
                                            Separator="/" />
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("fecha") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre:" SortExpression="nombre">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("nombre") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("nombre") %>' 
                                            Width="312px"></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("nombre") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="cargo" HeaderText="Monto:" SortExpression="cargo" />
                                <asp:TemplateField HeaderText="Cuenta:" SortExpression="cuentaID">
                                    <EditItemTemplate>
                                        <asp:Panel ID="Panel1" runat="server">
                                        </asp:Panel>
                                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("numCheque") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:Panel ID="pnlDatosCheque" runat="server">
                                        </asp:Panel>
                                        <asp:DropDownList ID="DropDownList2" runat="server" 
                                            datasourceid="sdsCuentasBanco0" DataTextField="cuenta" 
                                            DataValueField="cuentaID" SelectedValue='<%# Bind("cuentaID") %>'>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsCuentasBanco" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            SelectCommand="SELECT Bancos.nombre + '  ' + CuentasDeBanco.NumeroDeCuenta + ' - ' + CuentasDeBanco.Titular AS cuenta, CuentasDeBanco.cuentaID FROM Bancos INNER JOIN CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID ORDER BY cuenta">
                                        </asp:SqlDataSource>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("numCheque") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("cuentaID") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="DropDownList3" runat="server" 
                                            datasourceid="sdsCuentasBanco0" DataTextField="cuenta" 
                                            DataValueField="cuentaID" SelectedValue='<%# Bind("cuentaID") %>'>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsCuentasBanco0" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            SelectCommand="SELECT Bancos.nombre + '  ' + CuentasDeBanco.NumeroDeCuenta + ' - ' + CuentasDeBanco.Titular AS cuenta, CuentasDeBanco.cuentaID FROM Bancos INNER JOIN CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID ORDER BY cuenta">
                                        </asp:SqlDataSource>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("cuentaID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Catalogo y subcatalogo Fiscal:" 
                                    SortExpression="catalogoMovBancoFiscalID">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox11" runat="server" 
                                            Text='<%# Bind("catalogoMovBancoFiscalID") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="ddlCatalogoFiscal" runat="server" AutoPostBack="True" 
                                            DataSourceID="sdsCatalogoFiscal" DataTextField="catalogoMovBanco" 
                                            DataValueField="catalogoMovBancoID" ondatabound="ddlCatalogoFiscal_DataBound" 
                                            onselectedindexchanged="ddlCatalogoFiscal_SelectedIndexChanged" 
                                            SelectedValue='<%# Bind("catalogoMovBancoFiscalID") %>'>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:SqlDataSource ID="sdsCatalogoFiscal" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            SelectCommand="SELECT [catalogoMovBancoID], [catalogoMovBanco] FROM [catalogoMovimientosBancos] WHERE ([grupoCatalogoID] = @grupoCatalogoID) ORDER BY [catalogoMovBanco]">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="3" Name="grupoCatalogoID" Type="Int32" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        <asp:DropDownList ID="ddlSubCatalogoFiscal" runat="server" 
                                            DataSourceID="sdsSubcatalogoFiscal" DataTextField="subCatalogo" 
                                            DataValueField="subCatalogoMovBancoID">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsSubcatalogoFiscal" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            SelectCommand="SELECT subCatalogoMovBancoID, subCatalogo, catalogoMovBancoID FROM SubCatalogoMovimientoBanco ORDER BY subCatalogo">
                                        </asp:SqlDataSource>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" 
                                            Text='<%# Bind("catalogoMovBancoFiscalID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Concepto:" SortExpression="ConceptoMovCuentaID">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox12" runat="server" 
                                            Text='<%# Bind("ConceptoMovCuentaID") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="ddlConcepto" runat="server" AutoPostBack="True" 
                                            DataSourceID="sdsConceptos" DataTextField="Concepto" 
                                            DataValueField="ConceptoMovCuentaID" ondatabound="ddlConcepto_DataBound" 
                                            onselectedindexchanged="ddlConcepto_SelectedIndexChanged" 
                                            SelectedValue='<%# Bind("ConceptoMovCuentaID") %>'>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsConceptos" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            SelectCommand="SELECT ConceptoMovCuentaID, Concepto FROM ConceptosMovCuentas WHERE (ConceptoMovCuentaID = 3) OR(ConceptoMovCuentaID = 1) ORDER BY Concepto">
                                        </asp:SqlDataSource>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label10" runat="server" 
                                            Text='<%# Bind("ConceptoMovCuentaID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="numCheque">
                                    <InsertItemTemplate>
                                        <asp:Panel ID="pnlDatosCheque" runat="server">
                                            <table>
                                                <tr>
                                                    <td class="TablaField">
                                                        Cheque:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtChequeNum" runat="server" Text='<%# Eval("numCheque") %>'></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TablaField">
                                                        Nombre:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtChequeNombre" runat="server" Width="313px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </InsertItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="catalogoMovBancoInternoID">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:CheckBox ID="chkChangeSubCat" runat="server" CssClass="TablaField" 
                                            Text="Los Catalogos Internos son diferentes" />
                                        <asp:Panel ID="pnlCatInternos" runat="server">
                                            <table>
                                                <tr>
                                                    <td class="TablaField">
                                                        Grupo:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlGrupoInterno" runat="server" AutoPostBack="True" 
                                                            DataSourceID="sdsGruposInternos" DataTextField="grupoCatalogo" 
                                                            DataValueField="grupoCatalogosID" ondatabound="ddlGrupoInterno_DataBound" 
                                                            onselectedindexchanged="ddlGrupoInterno_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="sdsGruposInternos" runat="server" 
                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                            SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos] ORDER BY [grupoCatalogo]">
                                                        </asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TablaField">
                                                        Catalogo Interno:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCatalogoInterno" runat="server" AutoPostBack="True" 
                                                            DataSourceID="sdsCatalogosInterno" DataTextField="catalogoMovBanco" 
                                                            DataValueField="catalogoMovBancoID" ondatabound="ddlCatalogoInterno_DataBound" 
                                                            onselectedindexchanged="ddlCatalogoInterno_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="sdsCatalogosInterno" runat="server" 
                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                            SelectCommand="SELECT [catalogoMovBancoID], [catalogoMovBanco], [grupoCatalogoID] FROM [catalogoMovimientosBancos] ORDER BY [catalogoMovBanco]">
                                                        </asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TablaField">
                                                        Sub-Catalogo:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlSubCatInterno" runat="server" 
                                                            DataSourceID="sdsSubCatalogoInterno" DataTextField="subCatalogo" 
                                                            DataValueField="subCatalogoMovBancoID">
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="sdsSubCatalogoInterno" runat="server" 
                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                            SelectCommand="SELECT [subCatalogoMovBancoID], [subCatalogo], [catalogoMovBancoID] FROM [SubCatalogoMovimientoBanco] ORDER BY [subCatalogo]">
                                                        </asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                       
                                        <cc1:CollapsiblePanelExtender ID="pnlCatInternos_CollapsiblePanelExtender" 
                                            runat="server" CollapseControlID="chkChangeSubCat" Collapsed="True" 
                                            Enabled="True" ExpandControlID="chkChangeSubCat" 
                                            TargetControlID="pnlCatInternos">
                                        </cc1:CollapsiblePanelExtender>
                                       
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Observaciones" SortExpression="Observaciones">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Observaciones") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="TextBox14" runat="server" Height="88px" 
                                            Text='<%# Bind("Observaciones") %>' TextMode="MultiLine" Width="340px"></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("Observaciones") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ButtonType="Button" InsertText="Agregar" 
                                    ShowInsertButton="True" />
                            </Fields>
                        </asp:DetailsView>
                        <asp:SqlDataSource ID="sdsMovimientoDeBanco" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            DeleteCommand="DELETE FROM [MovimientosCuentasBanco] WHERE [movbanID] = @movbanID" 
                            InsertCommand="ADDMOVBANCO" InsertCommandType="StoredProcedure" 
                            SelectCommand="SELECT [fecha], [nombre], [cargo], [ConceptoMovCuentaID], [cuentaID], [catalogoMovBancoFiscalID], [subCatalogoMovBancoFiscalID], [numCheque], [chequeNombre], [catalogoMovBancoInternoID], [subCatalogoMovBancoInternoID], [Observaciones], [movbanID], [abono] FROM [MovimientosCuentasBanco] WHERE ([movbanID] = @movbanID)" 
                            
                            UpdateCommand="UPDATE [MovimientosCuentasBanco] SET [fecha] = @fecha, [nombre] = @nombre, [cargo] = @cargo, [ConceptoMovCuentaID] = @ConceptoMovCuentaID, [cuentaID] = @cuentaID, [catalogoMovBancoFiscalID] = @catalogoMovBancoFiscalID, [subCatalogoMovBancoFiscalID] = @subCatalogoMovBancoFiscalID, [numCheque] = @numCheque, [chequeNombre] = @chequeNombre, [catalogoMovBancoInternoID] = @catalogoMovBancoInternoID, [subCatalogoMovBancoInternoID] = @subCatalogoMovBancoInternoID, [Observaciones] = @Observaciones WHERE [movbanID] = @movbanID" 
                            oninserted="sdsMovimientoDeBanco_Inserted">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="gvPagos" Name="movbanID" 
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                            <DeleteParameters>
                                <asp:Parameter Name="movbanID" Type="Int32" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="fecha" Type="DateTime" />
                                <asp:Parameter Name="nombre" Type="String" />
                                <asp:Parameter Name="cargo" Type="Decimal" />
                                <asp:Parameter Name="ConceptoMovCuentaID" Type="Int32" />
                                <asp:Parameter Name="cuentaID" Type="Int32" />
                                <asp:Parameter Name="catalogoMovBancoFiscalID" Type="Int32" />
                                <asp:Parameter Name="subCatalogoMovBancoFiscalID" Type="Int32" />
                                <asp:Parameter Name="numCheque" Type="Int32" />
                                <asp:Parameter Name="chequeNombre" Type="String" />
                                <asp:Parameter Name="catalogoMovBancoInternoID" Type="Int32" />
                                <asp:Parameter Name="subCatalogoMovBancoInternoID" Type="Int32" />
                                <asp:Parameter Name="Observaciones" Type="String" />
                                <asp:Parameter Name="movbanID" Type="Int32" />
                            </UpdateParameters>
                            <InsertParameters>
                                <asp:Parameter Name="fecha" Type="DateTime" />
                                <asp:Parameter Name="nombre" Type="String" />
                                <asp:Parameter Name="cargo" Type="Decimal" />
                                <asp:Parameter Name="ConceptoMovCuentaID" Type="Int32" />
                                <asp:Parameter Name="cuentaID" Type="Int32" />
                                <asp:Parameter Name="catalogoMovBancoFiscalID" Type="Int32" />
                                <asp:Parameter Name="subCatalogoMovBancoFiscalID" Type="Int32" />
                                <asp:Parameter Name="numCheque" Type="Int32" />
                                <asp:Parameter Name="chequeNombre" Type="String" />
                                <asp:Parameter Name="catalogoMovBancoInternoID" Type="Int32" />
                                <asp:Parameter Name="subCatalogoMovBancoInternoID" Type="Int32" />
                                <asp:Parameter Name="Observaciones" Type="String" />
                                <asp:Parameter Name="userID" />
                                <asp:Parameter Name="fechaCheque" />
                                <asp:Parameter Name="abono" />
                                <asp:Parameter Name="fechacobrado" />
                                <asp:Parameter Direction="InputOutput" Name="newID" Type="Int32" />
                            </InsertParameters>
                        </asp:SqlDataSource>
                        <asp:Label ID="lblPagoResult" runat="server" Font-Size="X-Large" 
                            Text="EL PAGO FUE:"></asp:Label>
                            </asp:Panel>
                        <cc1:CollapsiblePanelExtender ID="pnlBancos_CollapsiblePanelExtender" 
                            runat="server" CollapseControlID="chkBanco" Collapsed="True" Enabled="True" 
                            ExpandControlID="chkBanco" TargetControlID="pnlBancos">
                        </cc1:CollapsiblePanelExtender>
                    </td>
                </tr>
                
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
