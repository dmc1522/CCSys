<%@ Page Title="Lista de Facturas de Ganado" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListaFacturasDeGanado.aspx.cs" Inherits="Garibay.frmListaFacturasDeGanado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
    <tr>
        <td>Ganaderos:</td> <td>
            <asp:DropDownList ID="ddlGanaderos" runat="server" AutoPostBack="True" 
                DataSourceID="sdsGanaderos" DataTextField="Nombre" 
                DataValueField="ganProveedorID" 
                onselectedindexchanged="ddlGanaderos_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sdsGanaderos" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                SelectCommand="SELECT 0 AS ganProveedorID, ' TODOS LOS GANADEROS' AS Nombre UNION SELECT DISTINCT gan_Proveedores.ganProveedorID, gan_Proveedores.Nombre FROM gan_Proveedores INNER JOIN FacturasdeGanado ON gan_Proveedores.ganProveedorID = FacturasdeGanado.ganProveedorID ORDER BY Nombre">
            </asp:SqlDataSource>
        </td>
    </tr>
</table>
    <asp:GridView ID="gvFacturas" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="FacturadeGanadoID" DataSourceID="sdFacturasGanado" 
        ondatabound="gvFacturas_DataBound" ShowFooter="True" AllowSorting="True">
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
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("total", "{0:C2}") %>'></asp:Label>
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
        
        
        
    SelectCommand="SELECT FacturasdeGanado.FacturadeGanadoID, FacturasdeGanado.fecha, gan_Proveedores.Nombre, FacturasdeGanado.total, ISNULL((SELECT SUM(No) AS Expr1 FROM FacturasDeGanadoDetalle GROUP BY productoID, FacturadeGanadoID HAVING (productoID = 16) AND (FacturadeGanadoID = FacturasdeGanado.FacturadeGanadoID)), 0) AS Becerros, ISNULL((SELECT SUM(No) AS Expr1 FROM FacturasDeGanadoDetalle AS FacturasDeGanadoDetalle_3 GROUP BY productoID, FacturadeGanadoID HAVING (productoID = 148) AND (FacturadeGanadoID = FacturasdeGanado.FacturadeGanadoID)), 0) AS Ganado, ISNULL((SELECT SUM(No) AS Expr1 FROM FacturasDeGanadoDetalle AS FacturasDeGanadoDetalle_2 GROUP BY productoID, FacturadeGanadoID HAVING (productoID = 15) AND (FacturadeGanadoID = FacturasdeGanado.FacturadeGanadoID)), 0) AS Vacas, ISNULL((SELECT SUM(No) AS Expr1 FROM FacturasDeGanadoDetalle AS FacturasDeGanadoDetalle_1 GROUP BY productoID, FacturadeGanadoID HAVING (productoID = 14) AND (FacturadeGanadoID = FacturasdeGanado.FacturadeGanadoID)), 0) AS Vaquillas, gan_Proveedores.ganProveedorID,  vTotalesPagos.Pagos, vTotalesPagos.Creditos FROM FacturasdeGanado INNER JOIN gan_Proveedores ON FacturasdeGanado.ganProveedorID = gan_Proveedores.ganProveedorID INNER JOIN vTotalesPagos ON FacturasdeGanado.FacturadeGanadoID = vTotalesPagos.FacturadeGanadoID ORDER BY gan_Proveedores.Nombre">
    </asp:SqlDataSource>
</asp:Content>
