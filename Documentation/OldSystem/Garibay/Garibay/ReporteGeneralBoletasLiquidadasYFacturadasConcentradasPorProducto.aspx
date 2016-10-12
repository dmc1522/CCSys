<%@ Page Language="C#" Theme="skinverde" MasterPageFile="~/MasterPage.Master" Title="Reporte concentrado por producto de las boletas liquidadas y facturadas" AutoEventWireup="true" CodeBehind="ReporteGeneralBoletasLiquidadasYFacturadasConcentradasPorProducto.aspx.cs" Inherits="Garibay.ReporteGeneralBoletasLiquidadasYFacturadasConcentradasPorProducto" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

         
        <table>
            <tr>
                <td class="TablaField">
                    Ciclo:</td>
                <td>
                    <asp:DropDownList ID="DropDownListCiclo" runat="server" AutoPostBack="True" 
                        DataSourceID="sdsCiclos" DataTextField="CicloName" DataValueField="cicloID" 
                        onselectedindexchanged="DropDownListCiclo_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [fechaInicio] DESC">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Producto:</td>
                <td>
                    <asp:DropDownList ID="DropDownListProducto" runat="server" AutoPostBack="True" 
                        DataSourceID="sdsProductos" DataTextField="Producto" DataValueField="productoID" 
                        onselectedindexchanged="DropDownListProducto_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsProductos" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        SelectCommand="SELECT DISTINCT vProductosParaDDL.productoID, vProductosParaDDL.Producto FROM Boletas INNER JOIN vProductosParaDDL ON Boletas.productoID = vProductosParaDDL.productoID WHERE (Boletas.cicloID = @cicloID) ORDER BY vProductosParaDDL.Producto">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownListCiclo" Name="cicloID" 
                                PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
    </table>
    <table>
        <tr>
            <td class="TableHeader">CONCENTRADO DE BOLETAS LIQUIDADAS:</td>
        </tr>
    </table>
    <asp:GridView ID="GridViewBoletasLiquidadas" 
        runat="server" AutoGenerateColumns="False" 
            DataKeyNames="productorId,productoId" 
            DataSourceID="SqlDataSourceBoletasLiquidadas" 
            ondatabound="GridViewBoletasLiquidadas_DataBound" ShowFooter="True">
            <Columns>
                <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                    SortExpression="Productor" >
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Nombre" HeaderText="Producto" 
                    SortExpression="Nombre" >
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="CantidadBoletas" HeaderText="Cantidad de Boletas" 
                    SortExpression="CantidadBoletas">
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PesoBruto" DataFormatString="{0:N2}" 
                    HeaderText="Peso Bruto" SortExpression="PesoBruto">
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Descuento_Humedad" DataFormatString="{0:N2}" 
                    HeaderText="Dcto Humedad (KG)" SortExpression="Descuento_Humedad">
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Descuento_Impurezas" DataFormatString="{0:N2}" 
                    HeaderText="Dcto Impurezas (KG)" SortExpression="Descuento_Impurezas">
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="PesoNeto" DataFormatString="{0:N2}" 
                    HeaderText="Peso Neto" SortExpression="PesoNeto">
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="TotalAPagar" DataFormatString="{0:C2}" 
                    HeaderText="Total A Pagar" SortExpression="TotalAPagar">
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Descuento_Secado" DataFormatString="{0:C2}" 
                    HeaderText="Secado" SortExpression="Descuento_Secado">
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="anticipos" DataFormatString="{0:C2}" 
                    HeaderText="Anticipos" ReadOnly="True" SortExpression="anticipos">
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Pagos_Creditos" DataFormatString="{0:C2}" 
                    HeaderText="Pagos a Creditos" ReadOnly="True" SortExpression="Pagos_Creditos">
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Pagos" DataFormatString="{0:C2}" HeaderText="Pagos" 
                    ReadOnly="True" SortExpression="Pagos">
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Restante" DataFormatString="{0:C2}" 
                    HeaderText="Restante a Pagar" ReadOnly="True" SortExpression="Restante">
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceBoletasLiquidadas" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
            
            SelectCommand="SELECT * FROM [vBoletasLiquidadasYFacturadas] WHERE (([cicloid] = @cicloid) AND ([productoId] = @productoId)) ORDER BY [Productor]">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListCiclo" Name="cicloid" 
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="DropDownListProducto" Name="productoId" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <table>
        <tr>
            <td class="TableHeader">CONCENTRADO DE BOLETAS NO LIQUIDADAS:</td>
        </tr>
    </table>
    <asp:GridView ID="gvBoletasNoLiquidadas" 
        runat="server" AutoGenerateColumns="False" 
            DataKeyNames="productorID,productoID" 
            DataSourceID="SqlDataSourceBoletasNoLiquidadas" ShowFooter="True" 
            ondatabound="gvBoletasNoLiquidadas_DataBound">
            <Columns>
                <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                    SortExpression="Productor">
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre">
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Cantidad de Boletas" 
                    SortExpression="CantidadBoletas">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CantidadBoletas") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblTotalBoletas" runat="server"></asp:Label>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" 
                            Text='<%# Bind("CantidadBoletas", "{0:N0}") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Peso Bruto" SortExpression="PesoBruto">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("PesoBruto") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblPesoBruto" runat="server"></asp:Label>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("PesoBruto", "{0:N2}") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Peso Neto" SortExpression="PesoNeto">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("PesoNeto") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblPesoNeto" runat="server"></asp:Label>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("PesoNeto", "{0:N2}") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
            </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceBoletasNoLiquidadas" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
            SelectCommand="SELECT * FROM [vBoletasSinLiquidar] WHERE (([cicloID] = @cicloID) AND ([productoID] = @productoID)) ORDER BY [Productor]">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListCiclo" Name="cicloID" 
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="DropDownListProducto" Name="productoID" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <table>
        <tr>
            <td class="TableHeader">CONCENTRADO DE BOLETAS FACTURADAS:</td>
        </tr>
    </table>
    <asp:GridView ID="gvBoletasFacturadas" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="productoID,clienteventaID" 
            DataSourceID="SqlDataSourceBoletasFacturadas" ShowFooter="True" 
            ondatabound="gvBoletasFacturadas_DataBound">
        <Columns>
            <asp:BoundField DataField="Cliente" HeaderText="Cliente" 
                SortExpression="Cliente">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Cantidad de boletas" 
                SortExpression="CantidadBoletas">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CantidadBoletas") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblTotalBoletas" runat="server"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("CantidadBoletas") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Peso Bruto" SortExpression="PesoBruto">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("PesoBruto") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblPesoBruto" runat="server"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("PesoBruto", "{0:N2}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Peso Neto" SortExpression="PesoNeto">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("PesoNeto") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblPesoNeto" runat="server"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("PesoNeto", "{0:N2}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total A Pagar" SortExpression="TotalAPagar">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("TotalAPagar") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblTotalAPagar" runat="server" ></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" 
                        Text='<%# Bind("TotalAPagar", "{0:c2}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Pagos" SortExpression="Pagos">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Pagos", "{0:c2}") %>'></asp:Label>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblPagos" runat="server"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Pagos", "{0:c2}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceBoletasFacturadas" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
            SelectCommand="SELECT * FROM [vBoletasFacturadas] WHERE (([cicloID] = @cicloID) AND ([productoID] = @productoID)) ORDER BY [Cliente]">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListCiclo" Name="cicloID" 
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="DropDownListProducto" Name="productoID" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <table>
        <tr>
            <td class="TableHeader">CONCENTRADO DE BOLETAS SIN FACTURAR:</td>
        </tr>
    </table>
    <asp:GridView ID="gvBoletasNoFacturadas" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="productoID,clienteventaID" 
            DataSourceID="SqlDataSourceBoletasNoFacturadas" ShowFooter="True" 
            ondatabound="gvBoletasNoFacturadas_DataBound">
        <Columns>
            <asp:BoundField DataField="Cliente" HeaderText="Cliente" 
                SortExpression="Cliente">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="CantidadBoletas" 
                SortExpression="CantidadBoletas">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CantidadBoletas") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblCantidadBoletas" runat="server" ></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("CantidadBoletas") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PesoBruto" SortExpression="PesoBruto">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("PesoBruto") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblPesoBruto" runat="server" ></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("PesoBruto", "{0:N2}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PesoNeto" SortExpression="PesoNeto">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("PesoNeto") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblPesoNeto" runat="server" ></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("PesoNeto", "{0:N2}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceBoletasNoFacturadas" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
            SelectCommand="SELECT * FROM [vBoletasNOFacturadas] WHERE (([cicloID] = @cicloID) AND ([productoID] = @productoID)) ORDER BY [Cliente]">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownListCiclo" Name="cicloID" 
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="DropDownListProducto" Name="productoID" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>


</asp:Content>
