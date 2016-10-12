<%@ Page Title="Reporte de cabezas por mes" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmReporteGanXMonth.aspx.cs" Inherits="Garibay.frmReporteGanXMonth" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0">
    	<tr>
    		<td colspan="2" class="TableHeader">Filtros:</td>
    	</tr>
    	<tr>
    		<td class="TablaField">Año:</td><td>
                <asp:DropDownList ID="ddlAnio" runat="server" DataSourceID="sdsYears" 
                    DataTextField="Year" DataValueField="Year">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsYears" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT Year FROM vReporteGanXMonth GROUP BY Year ORDER BY Year DESC">
                </asp:SqlDataSource>
            </td>
    	</tr>
    	<tr>
    		<td class="TablaField">Mes:</td><td>
                <asp:DropDownList ID="ddlMes" runat="server" DataSourceID="sdsMes" 
                    DataTextField="month" DataValueField="monthID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsMes" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT [monthID], [month] FROM [Meses] ORDER BY [monthID]">
                </asp:SqlDataSource>
            </td>
    	</tr>
    	<tr>
    		<td colspan="2" align="center"><asp:Button ID="btnFiltrar" runat="server" 
                    Text="Filtrar" onclick="btnFiltrar_Click" /></td>
    	</tr>
    </table>
    
    <asp:GridView ID="gvReporte" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="ganProveedorID" DataSourceID="sdsReporteXMonth" 
        ShowFooter="True" ondatabound="gvReporte_DataBound">
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" 
                SortExpression="Nombre" />
            <asp:TemplateField HeaderText="Total" SortExpression="Total">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Total") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Total", "{0:C2}") %>'></asp:Label>
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
            <asp:BoundField DataField="Mes" HeaderText="Mes" SortExpression="Mes" />
            <asp:BoundField DataField="Year" HeaderText="Año" SortExpression="Year" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsReporteXMonth" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        SelectCommand="SELECT vReporteGanXMonth.* FROM vReporteGanXMonth ORDER BY Nombre, Year DESC, Month DESC">
    </asp:SqlDataSource>
    
</asp:Content>
