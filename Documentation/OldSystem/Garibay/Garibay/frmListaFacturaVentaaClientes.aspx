<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" Title="Lista de Facturas de Venta a Clientes" AutoEventWireup="true" CodeBehind="frmListaFacturaVentaaClientes.aspx.cs" Inherits="Garibay.frmListaFacturaVentaaClientes" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

    <script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>
 <asp:UpdatePanel ID="upPanel" runat="Server">
    <ContentTemplate>
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
                               <br />
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
    <asp:UpdateProgress id= "upprog" runat="Server" AssociatedUpdatePanelID="upPanel" 
            DisplayAfter="0">
     <ProgressTemplate>
         <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/cargando.gif" />
         Cargando datos...
     </ProgressTemplate>
    
    </asp:UpdateProgress>
    <table >
	<tr>
		<td>
            <table>
                <tr>
                    <td class="TableHeader" rowspan="2">
                        FILTROS:</td>
                    <td class="TablaField">
                        Ciclo:</td>
                    <td>
                        <asp:DropDownList ID="drpdlCiclo" runat="server" AutoPostBack="True" 
                            DataSourceID="sdsCiclos" DataTextField="CicloName" DataValueField="cicloID" 
                            Height="23px" Width="164px" 
                            onselectedindexchanged="drpdlCiclo_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [CicloName] DESC">
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td class="TablaField">
                        Cliente:</td>
                    <td>
                    <br />
                        <asp:DropDownList ID="drpdlCliente" runat="server" AutoPostBack="True" 
                            DataSourceID="sdsClientes" DataTextField="nombre" 
                            DataValueField="clienteventaID" Height="23px" 
                            ondatabound="drpdlCliente_DataBound" 
                            onselectedindexchanged="drpdlCliente_SelectedIndexChanged">
                        </asp:DropDownList>
                        <cc1:ListSearchExtender ID="drpdlCliente_ListSearchExtender" runat="server" 
                            Enabled="True" PromptText="Escribe para buscar" 
                            TargetControlID="drpdlCliente">
                        </cc1:ListSearchExtender>
                        <asp:SqlDataSource ID="sdsClientes" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            
                            SelectCommand="SELECT DISTINCT ClientesVentas.clienteventaID, ClientesVentas.nombre FROM ClientesVentas INNER JOIN FacturasClientesVenta ON ClientesVentas.clienteventaID = FacturasClientesVenta.clienteVentaID WHERE (FacturasClientesVenta.cicloID = @cicloID) ORDER BY ClientesVentas.nombre">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="drpdlCiclo" Name="cicloID" 
                                    PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                </table>
        </td>
	</tr>
	<tr>
	    <td>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataSourceID="sdsFacturas" onrowdatabound="GridView1_RowDataBound" 
                DataKeyNames="FacturaCV,facturaNo" onrowcommand="GridView1_RowCommand" 
                ondatabound="GridView1_DataBound" ShowFooter="True" AllowSorting="True">
                <Columns>
                    <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                        ShowSelectButton="True" />
                    <asp:BoundField DataField="FacturaCV" HeaderText="FacturaID" 
                        InsertVisible="False" ReadOnly="True" SortExpression="FacturaCV" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="nombre" HeaderText="Cliente" 
                        SortExpression="nombre" />
                    <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                        HeaderText="fecha" SortExpression="fecha" />
                    <asp:BoundField DataField="facturaNo" HeaderText="# Factura" 
                        SortExpression="facturaNo" />
                    <asp:TemplateField HeaderText="Subtotal" SortExpression="subtotal">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("subtotal") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblFooterSubTotal" runat="server" Text="$ 0.00"></asp:Label>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("subtotal", "{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Iva" SortExpression="IVA">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("IVA") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblFooterIVA" runat="server" Text="$ 0.00"></asp:Label>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("IVA", "{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total" SortExpression="total">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("total") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblFooterTOTAL" runat="server" Text="$ 0.00"></asp:Label>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("total", "{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Pagos" DataFormatString="{0:c2}" HeaderText="Pagos" 
                        ReadOnly="True" SortExpression="Pagos">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:CheckBoxField DataField="pagada" HeaderText="Pagada" 
                        SortExpression="pagada" />
                    <asp:TemplateField HeaderText="Abrir" SortExpression="nombre">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkOpenFac" runat="server">Abrir</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="clienteVentaID" HeaderText="clienteVentaID" 
                        SortExpression="clienteVentaID" Visible="False" />
                    <asp:BoundField DataField="cicloID" HeaderText="cicloID" 
                        SortExpression="cicloID" Visible="False" />
                    <asp:TemplateField HeaderText="Eliminar" ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="btnEliminar" runat="server" CausesValidation="false" CommandName="" 
                                Text="Eliminar" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsFacturas" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                
                
                
                
                
                
                
                
                
                
                SelectCommand="SELECT ClientesVentas.nombre, FacturasClientesVenta.facturaNo, FacturasClientesVenta.fecha, FacturasClientesVenta.subtotal, FacturasClientesVenta.IVA, FacturasClientesVenta.total, FacturasClientesVenta.FacturaCV, FacturasClientesVenta.clienteVentaID, FacturasClientesVenta.cicloID, (SELECT ISNULL(SUM(MovimientosCaja.abono), 0) + ISNULL(SUM(MovimientosCuentasBanco.abono), 0) AS Pagos FROM MovimientosCuentasBanco RIGHT OUTER JOIN PagosFacturasClientesVenta ON MovimientosCuentasBanco.movbanID = PagosFacturasClientesVenta.movbanID LEFT OUTER JOIN MovimientosCaja ON PagosFacturasClientesVenta.movCajaID = MovimientosCaja.movimientoID WHERE (PagosFacturasClientesVenta.FacturaCVID = FacturasClientesVenta.FacturaCV)) AS Pagos, FacturasClientesVenta.pagada FROM FacturasClientesVenta INNER JOIN ClientesVentas ON FacturasClientesVenta.clienteVentaID = ClientesVentas.clienteventaID ORDER BY ClientesVentas.nombre">
            </asp:SqlDataSource>
            <asp:Button ID="btnOpenFac" Text="Abrir Factura" runat="Server" 
                Visible="False" onclick="btnOpenFac_Click" />
            <asp:Button ID="btnDelteFactura" runat="server" Text="Eliminar Factura" 
                Visible="False" onclick="btnDelteFactura_Click" />
        </td>
	</tr>
        <tr>
            <td>
                <asp:Panel ID="pnlDeleteLiq" runat="server" Visible="False">
                    <asp:Image ID="imgBien" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                    <asp:Image ID="imgMal" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                    <asp:Label ID="lblDeleteResult" runat="server"></asp:Label>
                </asp:Panel>
            </td>
        </tr>
</table>
         
</ContentTemplate>
 </asp:UpdatePanel>      

</asp:Content>
