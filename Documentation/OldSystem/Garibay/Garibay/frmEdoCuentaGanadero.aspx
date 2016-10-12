<%@ Page Title="Estado de cuenta de proveedor de ganado" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmEdoCuentaGanadero.aspx.cs" Inherits="Garibay.frmEdoCuentaGanadero" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
    	<tr>
    		<td class="TableHeader" colspan="2">ESTADO DE CUENTA DE GANADERO</td>
    	</tr>
    	<tr>
    		<td class="TablaField">Ganadero:</td>
    		<td>
                <asp:DropDownList ID="ddlGanadero" runat="server" DataSourceID="sdsGanadero" 
                    DataTextField="Nombre" DataValueField="ganProveedorID" 
                    onselectedindexchanged="ddlGanadero_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsGanadero" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    
                    SelectCommand="SELECT [ganProveedorID], [Nombre] FROM [gan_Proveedores] ORDER BY [Nombre]" 
                    oninserted="sdsGanadero_Inserted">
                </asp:SqlDataSource>
            </td>
    	</tr>
    </table>
    <br />
    <table>
        <tr>
            <td class="TableHeader">ESTADO DE CUENTA GENERAL</td>
        </tr>
        <tr>
            <td>
                <asp:DetailsView ID="dvGeneral" runat="server" Height="50px" Width="125px" 
                    AutoGenerateRows="False" DataSourceID="sdsEstadoGeneral">
                    <Fields>
                        <asp:BoundField DataField="KgsEntregado" DataFormatString="{0:C2}" 
                            HeaderText="Kgs Entregados" SortExpression="KgsEntregado" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TotalFacturado" DataFormatString="{0:C2}" 
                            HeaderText="Total Facturado" SortExpression="TotalPagado">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TotalAnticipo" DataFormatString="{0:c2}" 
                            HeaderText="Total Anticipos" SortExpression="TotalAnticipo">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TotalPagado" DataFormatString="{0:c2}" 
                            HeaderText="Total Pagado" SortExpression="TotalPagado">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Total" DataFormatString="{0:C2}" HeaderText="Total" 
                            SortExpression="Total">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Fields>
                </asp:DetailsView>
                <asp:SqlDataSource ID="sdsEstadoGeneral" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="ReturnEstadodeCuentaGanadero" 
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlGanadero" DefaultValue="-1" 
                            Name="ganProveedorId" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                FACTURAS DEL GANADERO&nbsp;</td>
        </tr>
        <tr>
            <td>
    <asp:GridView ID="gvFacturas" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="FacturadeGanadoID" DataSourceID="sdFacturasGanado" 
        ondatabound="gvFacturas_DataBound" ShowFooter="True">
        <Columns>
            <asp:TemplateField HeaderText="Factura" InsertVisible="False" 
                SortExpression="FacturadeGanadoID">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("FacturadeGanadoID") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" 
                        NavigateUrl='<%# GetOpenURL(Eval("FacturadeGanadoID").ToString()) %>' 
                        Text='<%# Eval("FacturadeGanadoID") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                HeaderText="Fecha" SortExpression="fecha" />
            <asp:BoundField DataField="Nombre" HeaderText="Proveedor" 
                SortExpression="Nombre" />
            <asp:TemplateField HeaderText="Total" SortExpression="total">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("total") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("total", "{0:C2}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Becerros" SortExpression="Becerros">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Becerros") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblBecerros" runat="server"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Becerros") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ganado" SortExpression="Ganado">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Ganado") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblGanado" runat="server"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Ganado") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Vacas" SortExpression="Vacas">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Vacas") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblVacas" runat="server"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Vacas") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Vaquillas" SortExpression="Vaquillas">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Vaquillas") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblVaquillas" runat="server"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Vaquillas") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:BoundField DataField="Pagos" DataFormatString="{0:C2}" HeaderText="Pagos" 
                SortExpression="Pagos">
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="Creditos" DataFormatString="{0:C2}" 
                HeaderText="Creditos" SortExpression="Creditos">
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdFacturasGanado" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        
        
        
    
                    
                    SelectCommand="SELECT FacturasdeGanado.FacturadeGanadoID, FacturasdeGanado.fecha, gan_Proveedores.Nombre, FacturasdeGanado.total, ISNULL((SELECT SUM(No) AS Expr1 FROM FacturasDeGanadoDetalle GROUP BY productoID, FacturadeGanadoID HAVING (productoID = 16) AND (FacturadeGanadoID = FacturasdeGanado.FacturadeGanadoID)), 0) AS Becerros, ISNULL((SELECT SUM(No) AS Expr1 FROM FacturasDeGanadoDetalle AS FacturasDeGanadoDetalle_3 GROUP BY productoID, FacturadeGanadoID HAVING (productoID = 148) AND (FacturadeGanadoID = FacturasdeGanado.FacturadeGanadoID)), 0) AS Ganado, ISNULL((SELECT SUM(No) AS Expr1 FROM FacturasDeGanadoDetalle AS FacturasDeGanadoDetalle_2 GROUP BY productoID, FacturadeGanadoID HAVING (productoID = 15) AND (FacturadeGanadoID = FacturasdeGanado.FacturadeGanadoID)), 0) AS Vacas, ISNULL((SELECT SUM(No) AS Expr1 FROM FacturasDeGanadoDetalle AS FacturasDeGanadoDetalle_1 GROUP BY productoID, FacturadeGanadoID HAVING (productoID = 14) AND (FacturadeGanadoID = FacturasdeGanado.FacturadeGanadoID)), 0) AS Vaquillas, gan_Proveedores.ganProveedorID,  vTotalesPagos.Pagos, vTotalesPagos.Creditos FROM FacturasdeGanado INNER JOIN gan_Proveedores ON FacturasdeGanado.ganProveedorID = gan_Proveedores.ganProveedorID INNER JOIN vTotalesPagos ON FacturasdeGanado.FacturadeGanadoID = vTotalesPagos.FacturadeGanadoID where FacturasDeGanado.ganproveedorID = @proveeedorId ORDER BY gan_Proveedores.Nombre">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlGanadero" DefaultValue="-1" 
                Name="proveeedorId" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td class="TableHeader">
                ANTICIPOS</td>
        </tr>
        <tr>
            <td>
                            <asp:GridView ID="gvAnticiposDadosAlProductor" runat="server" 
                                AutoGenerateColumns="False" DataKeyNames="movbanID" 
                                DataSourceID="sdsAnticiposDadosAlProductor">
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
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="sdsAnticiposDadosAlProductor" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                
                                
                    SelectCommand="SELECT MovimientosCuentasBanco.fecha AS fechaMov, MovimientosCuentasBanco.cargo, MovimientosCuentasBanco.abono, MovimientosCuentasBanco.numCheque, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.movbanID, Anticipos_FacturasGanado.ganProveedorID, Anticipos_FacturasGanado.FacturadeGanadoID FROM MovimientosCuentasBanco INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID INNER JOIN Anticipos_FacturasGanado ON MovimientosCuentasBanco.movbanID = Anticipos_FacturasGanado.movbanID WHERE (Anticipos_FacturasGanado.ganProveedorID = @ganProveedorID) AND (Anticipos_FacturasGanado.FacturadeGanadoID IS NOT NULL)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlGanadero" Name="ganProveedorID" 
                                        PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            </td>
        </tr>
        <tr>
            <td class="TableHeader">
                PAGOS HECHOS AL GANADERO</td>
        </tr>
        <tr>
            <td>
                            <asp:GridView ID="gvPagosFactura" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="movbanID" DataSourceID="sdsPagosFactura" 
                                onrowdeleted="gvPagosFactura_RowDeleted">
                                <Columns>
                                    <asp:CommandField ButtonType="Button" DeleteText="Eliminar" 
                                        ShowDeleteButton="True" />
                                    <asp:BoundField DataField="pagoID" HeaderText="pagoID" ReadOnly="True" 
                                        SortExpression="pagoID" InsertVisible="False" Visible="False">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fechaMov" DataFormatString="{0:dd/MM/yyyy}" 
                                        HeaderText="Fecha" SortExpression="fechaMov" />
                                    <asp:BoundField DataField="montoPago" HeaderText="Monto" 
                                        SortExpression="montoPago" DataFormatString="{0:c2}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Concepto" HeaderText="Concepto" 
                                        SortExpression="Concepto" DataFormatString="{0:c2}">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="numCheque" 
                                        HeaderText="# cheque" SortExpression="numCheque">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="sdsPagosFactura" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                DeleteCommand="DELETEPAGOFACTURAGANADO" 
                                
                                
                                
                    
                                
                                SelectCommand="SELECT PagosFacturaDeGanado.pagoID, MovimientosCuentasBanco.fecha AS fechaMov, PagosFacturaDeGanado.montoPago, MovimientosCuentasBanco.numCheque, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.movbanID FROM MovimientosCuentasBanco INNER JOIN PagosFacturaDeGanado ON MovimientosCuentasBanco.movbanID = PagosFacturaDeGanado.movbanID INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID 
INNER JOIN FacturasDeGanado On  FacturasDeGanado.FacturadeGanadoID = PagosFacturaDeGanado.FacturadeGanadoID WHERE (FacturasDeGanado.ganProveedorId = @ganProveedorId)" 
                                DeleteCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlGanadero" Name="ganProveedorId" 
                                        PropertyName="SelectedValue" />
                                </SelectParameters>
                                <DeleteParameters>
                                    <asp:Parameter Name="MOVBANID" Type="Int32" />
                                </DeleteParameters>
                            </asp:SqlDataSource>
                            <asp:Panel ID="pnlPagoResult" runat="server">
                                <asp:Label ID="lblPagoResult" runat="server" 
    Text="EL PAGO FUE:" Font-Size="X-Large" Font-Bold="True"></asp:Label>
                            </asp:Panel>
                        </td>
        </tr>
    </table>
    
    <table>
        <tr>
            <td><asp:CheckBox ID="chkAddPago" runat="server" CssClass="TablaField" Text="Agregar nuevo pago" /></td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlAddPago" runat="server">
                    <table>
                    	<tr>
                    		<td class="TablaField">Instrucciones:</td>
                    	</tr>
                    	<tr>
                    		<td>
                                <ul>
                                    <li>Seleccione las facturas que se relacionan con el pago</li>
                                    <li>Por cada factura, seleccione el monto a pagar; por defecto el monto total de la 
                                        factura es seleccionado</li>
                                    <li>Rellene los datos del movimiento de banco.</li>
                                    <li>Agregue el pago.</li>
                                </ul>
                            </td>
                    	</tr>
                    </table>
                    <asp:GridView ID="gvFacturasDisponibles" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="FacturadeGanadoID" 
                        DataSourceID="sdsFacturasDisponibles">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAdd" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="FacturadeGanadoID" HeaderText="Factura ID" 
                                InsertVisible="False" ReadOnly="True" SortExpression="FacturadeGanadoID">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                HeaderText="Fecha" SortExpression="fecha" />
                            <asp:BoundField DataField="total" DataFormatString="{0:C2}" HeaderText="Total" 
                                SortExpression="total">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Pagos" DataFormatString="{0:C2}" HeaderText="Pagos" 
                                SortExpression="Pagos">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Creditos" DataFormatString="{0:C2}" 
                                HeaderText="Creditos" SortExpression="Creditos">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Por_Pagar" DataFormatString="{0:C2}" 
                                HeaderText="Por_Pagar" SortExpression="Por_Pagar">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Pago">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPago" runat="server" 
                                        Text='<%# Eval("Por_Pagar", "{0:C2}") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsFacturasDisponibles" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
                        SelectCommand="SELECT FacturasdeGanado.FacturadeGanadoID, FacturasdeGanado.fecha, FacturasdeGanado.ganProveedorID, vTotalesPagos.Total AS total, vTotalesPagos.Pagos, vTotalesPagos.Creditos, vTotalesPagos.Total - vTotalesPagos.Pagos - vTotalesPagos.Creditos AS Por_Pagar FROM FacturasdeGanado INNER JOIN vTotalesPagos ON FacturasdeGanado.FacturadeGanadoID = vTotalesPagos.FacturadeGanadoID WHERE (FacturasdeGanado.ganProveedorID = @ganProveedorID) AND (vTotalesPagos.Total - vTotalesPagos.Pagos - vTotalesPagos.Creditos &gt; 0.1) ORDER BY FacturasdeGanado.fecha">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlGanadero" Name="ganProveedorID" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <br />
                    <table>
                    	<tr>
                    		<td class="TablaField">DATOS DEL MOVIMIENTO DE BANCO</td>
                    	</tr>
                    	<tr>
                    	    <td>
                                <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" 
                                    DataKeyNames="movbanID" DataSourceID="sdsMovimientoDeBanco" 
                                    DefaultMode="Insert" Height="50px" Width="125px" 
                                    oniteminserted="DetailsView1_ItemInserted" 
                                    oniteminserting="DetailsView1_ItemInserting">
                                    <Fields>
                                        <asp:TemplateField HeaderText="Fecha:" SortExpression="fecha">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("fecha") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True" 
                                                    Text='<%# Bind("fecha", "{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                                <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="TextBox1" 
                                                    Separator="/" />
                                            </InsertItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("fecha") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre:" SortExpression="nombre">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("nombre") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("nombre") %>' 
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
                                                    DataSourceID="sdsCuentasBanco" DataTextField="cuenta" DataValueField="cuentaID" 
                                                    SelectedValue='<%# Bind("cuentaID") %>'>
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
                                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("cuentaID") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <asp:DropDownList ID="DropDownList2" runat="server" 
                                                    DataSourceID="sdsCuentasBanco" DataTextField="cuenta" DataValueField="cuentaID" 
                                                    SelectedValue='<%# Bind("cuentaID") %>'>
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsCuentasBanco" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                    SelectCommand="SELECT Bancos.nombre + '  ' + CuentasDeBanco.NumeroDeCuenta + ' - ' + CuentasDeBanco.Titular AS cuenta, CuentasDeBanco.cuentaID FROM Bancos INNER JOIN CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID ORDER BY cuenta">
                                                </asp:SqlDataSource>
                                            </InsertItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("cuentaID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Catalogo y subcatalogo Fiscal:" 
                                            SortExpression="catalogoMovBancoFiscalID">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox4" runat="server" 
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
                                                <asp:TextBox ID="TextBox2" runat="server" 
                                                    Text='<%# Bind("ConceptoMovCuentaID") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <asp:DropDownList ID="ddlConcepto" runat="server" DataSourceID="sdsConceptos" 
                                                    DataTextField="Concepto" DataValueField="ConceptoMovCuentaID" 
                                                    SelectedValue='<%# Bind("ConceptoMovCuentaID") %>' AutoPostBack="True" 
                                                    ondatabound="ddlConcepto_DataBound" 
                                                    onselectedindexchanged="ddlConcepto_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsConceptos" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                    SelectCommand="SELECT ConceptoMovCuentaID, Concepto FROM ConceptosMovCuentas WHERE (ConceptoMovCuentaID = 3) OR(ConceptoMovCuentaID = 1) ORDER BY Concepto">
                                                </asp:SqlDataSource>
                                            </InsertItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("ConceptoMovCuentaID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="numCheque">
                                            <InsertItemTemplate>
                                                <asp:Panel ID="pnlDatosCheque" runat="server">
                                                    <table>
                                                        <tr>
                                                            <td class="TablaField">Cheque:</td>
                                                            <td><asp:TextBox ID="txtChequeNum" runat="server" Text='<%# Eval("numCheque") %>'></asp:TextBox></td>
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
                                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <asp:CheckBox ID="chkChangeSubCat" runat="server" 
                                                    Text="Los Catalogos Internos son diferentes" CssClass="TablaField" />
                                                <asp:Panel ID="pnlCatInternos" runat="server">
                                                    <table>
                                        	            <tr>
                                        		            <td class="TablaField">Grupo:</td>
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
                                                <asp:Label ID="Label6" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Observaciones" SortExpression="Observaciones">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Observaciones") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server" Height="88px" 
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
                                    InsertCommand="ADDMOVBANCO" 
                                    SelectCommand="SELECT [fecha], [nombre], [cargo], [ConceptoMovCuentaID], [cuentaID], [catalogoMovBancoFiscalID], [subCatalogoMovBancoFiscalID], [numCheque], [chequeNombre], [catalogoMovBancoInternoID], [subCatalogoMovBancoInternoID], [Observaciones], [movbanID], [abono] FROM [MovimientosCuentasBanco] WHERE ([movbanID] = @movbanID)" 
                                    
                                    
                                    UpdateCommand="UPDATE [MovimientosCuentasBanco] SET [fecha] = @fecha, [nombre] = @nombre, [cargo] = @cargo, [ConceptoMovCuentaID] = @ConceptoMovCuentaID, [cuentaID] = @cuentaID, [catalogoMovBancoFiscalID] = @catalogoMovBancoFiscalID, [subCatalogoMovBancoFiscalID] = @subCatalogoMovBancoFiscalID, [numCheque] = @numCheque, [chequeNombre] = @chequeNombre, [catalogoMovBancoInternoID] = @catalogoMovBancoInternoID, [subCatalogoMovBancoInternoID] = @subCatalogoMovBancoInternoID, [Observaciones] = @Observaciones WHERE [movbanID] = @movbanID" 
                                    InsertCommandType="StoredProcedure" 
                                    oninserted="sdsMovimientoDeBanco_Inserted">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="gvPagosFactura" Name="movbanID" 
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
                                        <asp:Parameter Name="newID" Direction="InputOutput" Type="Int32" />
                                    </InsertParameters>
                                </asp:SqlDataSource>
                            </td>
                    	</tr>
                    </table>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="pnlAddPago_CollapsiblePanelExtender" 
                    runat="server" CollapseControlID="chkAddPago" Collapsed="True" Enabled="True" 
                    ExpandControlID="chkAddPago" TargetControlID="pnlAddPago">
                </cc1:CollapsiblePanelExtender>
            </td>
        </tr>
    </table>
</asp:Content>
