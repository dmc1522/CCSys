<%@ Page Title="FORMATO PARA IMPRESION DE CHEQUES" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmOnlyPrintCheque.aspx.cs" Inherits="Garibay.frmOnlyPrintCheque" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
    <tr><td colspan="2" class="TableHeader">FORMATO DE IMPRESION DE CHEQUE</td></tr>
    <tr>
        <td class="TablaField">BANCO:</td><td>
        <asp:DropDownList ID="ddlCuenta" runat="server" DataSourceID="sdsCuenta" 
            DataTextField="Cuenta" DataValueField="bancoID">
        </asp:DropDownList>
        <asp:SqlDataSource ID="sdsCuenta" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
            SelectCommand="SELECT Bancos.bancoID, Bancos.nombre + ' - ' + CuentasDeBanco.NumeroDeCuenta + ' - ' + CuentasDeBanco.Titular AS Cuenta FROM Bancos INNER JOIN CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID ORDER BY Cuenta">
        </asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td class="TablaField">FECHA:</td><td>
        <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
                <rjs:PopCalendar ID="PopCalendar1" runat="server" Separator="/" 
                    Control="txtFecha"  />
              
        </td>
    </tr>
    <tr>
        <td class="TablaField">NOMBRE:</td><td>
        <asp:TextBox ID="txtNombre" runat="server" Width="466px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="TablaField">CANTIDAD:</td><td>
        <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="TablaField">CONCEPTO:</td><td>
        <asp:TextBox ID="txtConcepto" runat="server" Width="461px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="TablaField" align="center" colspan="2">
            <asp:Button ID="btnImprimir" runat="server" onclick="btnImprimir_Click" 
                Text="IMPRIME CHEQUE" />
        </td>
    </tr>
</table>
</asp:Content>
