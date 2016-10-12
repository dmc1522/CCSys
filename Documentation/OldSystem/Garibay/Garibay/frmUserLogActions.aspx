<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmUserLogActions.aspx.cs" Inherits="Garibay.frmUserLogActions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="PanelFiltros" runat="server">
        <table>
        	<tr><asp:SqlDataSource ID="sdsUsuarios" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        SelectCommand="SELECT [userID], [Nombre] FROM [Users] ORDER BY [username]"></asp:SqlDataSource>
        		<td class="TablaField">
                    <asp:CheckBox ID="chkFiltroUsuarios" runat="server" />Usuario:</td><td>
                <asp:DropDownList ID="ddlUsuarios" runat="server" 
                    DataSourceID="sdsUsuarios" DataTextField="Nombre" DataValueField="userID">
        </asp:DropDownList></td>
        	</tr>
        	<tr>
                <asp:SqlDataSource ID="sdsModules" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT [moduleID], [module] FROM [Modules] ORDER BY [module]"></asp:SqlDataSource>
        		<td class="TablaField">
                    <asp:CheckBox ID="chkFiltroModulos" runat="server" />Modulo:</td><td>
                <asp:DropDownList ID="ddlModulos" runat="server" 
                    DataSourceID="sdsModules" DataTextField="module" DataValueField="moduleID">
        </asp:DropDownList></td>
        	</tr>
        	<tr><td class="TablaField">Elementos por página:</td><td>
                <asp:DropDownList ID="ddlElemXPage" runat="server">
                    <asp:ListItem >10</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    <asp:ListItem>200</asp:ListItem>
                    <asp:ListItem>500</asp:ListItem>
                    <asp:ListItem Selected="True">1000</asp:ListItem>
                </asp:DropDownList>
            </td></tr>
            <tr><td colspan="3">
                <asp:Button ID="btnAplicaFiltros" runat="server" Text="Aplicar Filtros" 
                    onclick="btnAplicaFiltros_Click" /></td></tr>
        </table>
        <asp:SqlDataSource ID="sdsListaUserLog" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
            
            
            SelectCommand="SELECT UserSessionRecords.timestamp, Users.Nombre, Modules.module, UsersActions.fancyName, UserSessionRecords.description, UserSessionRecords.moduleID, UserSessionRecords.userID FROM Modules INNER JOIN UserSessionRecords ON Modules.moduleID = UserSessionRecords.moduleID INNER JOIN Users ON UserSessionRecords.userID = Users.userID INNER JOIN UsersActions ON UserSessionRecords.useractionID = UsersActions.useractionID ORDER BY UserSessionRecords.timestamp DESC"></asp:SqlDataSource>
        <asp:GridView ID="GridViewUserLog" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            DataSourceID="sdsListaUserLog" PageSize="1000">
            <Columns>
                <asp:BoundField DataField="timestamp" 
                    DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderText="Fecha" 
                    SortExpression="timestamp" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" 
                    SortExpression="Nombre" />
                <asp:BoundField DataField="module" HeaderText="Modulo" 
                    SortExpression="module" />
                <asp:BoundField DataField="fancyName" HeaderText="Acción" 
                    SortExpression="fancyName" />
                <asp:BoundField DataField="description" HeaderText="Descripción" 
                    SortExpression="description" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
