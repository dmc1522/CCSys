<%@ Page Title="REPORTE ENTRADAS SALIDAS DE BOLETAS POR DIA" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmEntradasSalidasBoletasxDia.aspx.cs" Inherits="Garibay.frmEntradasSalidasBoletasxDia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
    <tr> 
        <th class="TableHeader" colspan="2">REPORTE ENTRADAS SALIDAS DE BOLETAS POR DIA</th>
    </tr>
    <tr>
        <td class = "TablaField">CICLO:</td> <td>
            <asp:DropDownList ID="ddlCiclo" runat="server" AutoPostBack="True" 
                DataSourceID="sdsCiclos" DataTextField="CicloName" DataValueField="cicloID" 
                onselectedindexchanged="ddlCiclo_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [fechaInicio] DESC">
            </asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td class = "TablaField">PRODUCTO:</td> <td>
            <asp:DropDownList ID="ddlProductos" runat="server" DataSourceID="sdsProductos" 
                DataTextField="Producto" DataValueField="productoID" AutoPostBack="True" 
                onselectedindexchanged="ddlProductos_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sdsProductos" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                
                SelectCommand="SELECT DISTINCT vProductosParaDDL.productoID, vProductosParaDDL.Producto FROM Boletas INNER JOIN vProductosParaDDL ON Boletas.productoID = vProductosParaDDL.productoID ORDER BY vProductosParaDDL.Producto">
            </asp:SqlDataSource>
        </td>
    </tr>
</table>
    <asp:GridView ID="gvReporte" runat="server" AutoGenerateColumns="False" 
        DataSourceID="sdsReporteBoletas" ondatabound="gvReporte_DataBound" 
        ShowFooter="True">
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" 
                SortExpression="Nombre" />
            <asp:BoundField DataField="anio" HeaderText="Año" SortExpression="anio">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Mes" HeaderText="Mes" SortExpression="Mes">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Dia" HeaderText="Dia" SortExpression="Dia">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Entrada" SortExpression="Entrada">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Entrada", "{0:N2}") %>'></asp:Label>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblEntrada" runat="server" Text="0"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Entrada", "{0:N2}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="KG_Descuentos" SortExpression="KG_Descuentos">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("KG_Descuentos") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblDescuentos" runat="server" Text="0"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" 
                        Text='<%# Bind("KG_Descuentos", "{0:N2}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Entrada_Neto" SortExpression="Entrada_Neto">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Entrada_Neto") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblEntradaNeto" runat="server" Text="0"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" 
                        Text='<%# Bind("Entrada_Neto", "{0:N2}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Salida" SortExpression="Salida">
                <EditItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Salida", "{0:N2}") %>'></asp:Label>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblSalida" runat="server" Text="0"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Salida", "{0:N2}") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Cantidad_De_Boletas" 
                SortExpression="Cantidad_De_Boletas">
                <EditItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Cantidad_De_Boletas") %>'></asp:Label>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblBoletas" runat="server" Text="0"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Cantidad_De_Boletas") %>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsReporteBoletas" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        
        SelectCommand="SELECT * FROM [vEntradasSalidasBoletasXDia] WHERE (([productoID] = @productoID) AND ([cicloID] = @cicloID)) ORDER BY [anio] DESC, [Mes] DESC, [Dia] DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlProductos" DefaultValue="-1" 
                Name="productoID" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="ddlCiclo" DefaultValue="-1" Name="cicloID" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
