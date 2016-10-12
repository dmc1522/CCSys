<%@ Page Title="Formato de Orden de Compra" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmOrdenCompraFormato.aspx.cs" Inherits="Garibay.frmOrdenCompraFormato" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .style3
    {
        height: 23px;
    }
        .style4
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="TableHeader" colspan="2">ORDEN DE COMPRA<asp:TextBox ID="txtNotaID" 
                    runat="server" Width="0px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="TablaField">FECHA:</td>
            <td>
                <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
                <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtFecha" 
                    Separator="/" />
            </td>
        </tr>
        <tr>
            <td class="TablaField">PROVEEDORES:</td>
            <td>
                <asp:DropDownList ID="ddlProveedores" runat="server" 
                    DataSourceID="sdsProveedores" DataTextField="Nombre" 
                    DataValueField="proveedorID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsProveedores" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT [proveedorID], [Nombre] FROM [Proveedores] ORDER BY [Nombre]">
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td class="TablaField">CLIENTE:</td>
            <td>
                <asp:DropDownList ID="ddlEmpresa" runat="server" DataSourceID="sdsEmpresas" 
                    DataTextField="Empresa" DataValueField="empresaID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsEmpresas" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT [empresaID], [Empresa] FROM [Empresas] WHERE ([empresaID] &lt;&gt; @empresaID) ORDER BY [Empresa]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="3" Name="empresaID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        
        <tr>
            <td class="TablaField" colspan="2">PRODUCTO</td>
        </tr>
        
        <tr>
            <td class="TablaField">COMPRA DE:</td>
            <td>
                <asp:TextBox ID="txtComprade" runat="server" Width="391px"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td class="TablaField">PRECIO DE:</td>
            <td>
                <asp:TextBox ID="txtPrecioDe" runat="server" Width="391px"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td class="TablaField">ENTREGA:</td>
            <td>
                <asp:TextBox ID="txtEntrega" runat="server" Width="391px"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnlNewPago" runat="server">
                    <br />
                    <asp:Image ID="imgBienPago" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                    <asp:Image ID="imgMalPago" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                    <asp:Label ID="lblNewNota" runat="server"></asp:Label>
                </asp:Panel>
            </td>
        </tr>
        
        <tr>
            <td colspan="2">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
                    onclick="btnAgregar_Click" />
                    <asp:Button ID="btnPrint" runat="server" Text="IMPRIMIR" />
            </td>
        </tr>
        
    </table>
</asp:Content>
