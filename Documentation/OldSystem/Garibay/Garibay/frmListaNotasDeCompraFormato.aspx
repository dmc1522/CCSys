<%@ Page Title="Lista de formatos de orden de compra" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListaNotasDeCompraFormato.aspx.cs" Inherits="Garibay.frmListaNotasDeCompraFormato" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:GridView ID="gvNotas" runat="server" AutoGenerateColumns="False" 
    DataSourceID="sdsLista" DataKeyNames="empresaID,ordenID">
    <Columns>
        <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
            HeaderText="Fecha" SortExpression="fecha" />
        <asp:BoundField DataField="Empresa" HeaderText="Empresa" 
            SortExpression="Empresa" />
        <asp:BoundField DataField="Nombre" HeaderText="Proveedor" 
            SortExpression="Nombre" />
        <asp:BoundField DataField="comprade" HeaderText="Compra de" 
            SortExpression="comprade" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:HyperLink ID="HyperLink1" runat="server" 
                    NavigateUrl='<%# getOpenLink(Eval("ordenid").ToString()) %>'> Abrir</asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="sdsLista" runat="server" 
    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
    
        SelectCommand="SELECT ordenDeCompraFormato.empresaID, ordenDeCompraFormato.fecha, ordenDeCompraFormato.comprade, ordenDeCompraFormato.preciode, ordenDeCompraFormato.entrega, Empresas.Empresa, Proveedores.Nombre, ordenDeCompraFormato.ordenID FROM ordenDeCompraFormato INNER JOIN Empresas ON ordenDeCompraFormato.empresaID = Empresas.empresaID INNER JOIN Proveedores ON ordenDeCompraFormato.proveedorID = Proveedores.proveedorID ORDER BY Empresas.Empresa">
</asp:SqlDataSource>

</asp:Content>
