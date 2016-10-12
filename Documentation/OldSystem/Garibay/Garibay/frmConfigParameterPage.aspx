<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmConfigParameterPage.aspx.cs" Inherits="Garibay.frmConfigParameterPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="/scripts/divFunctions.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
    <tr>
        <th>Parametro:</th><th>Valor</th>
    </tr>
    <tr>
        <td class="TableField">TIPO DE LETRA EN CHEQUE:</td><td>
            <asp:DropDownList ID="ddlChequeFont" runat="server" Width="275px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="TableField">
            <asp:Button ID="btnChequesConfig" runat="server" Text="Configurar cheques" />
        </td><td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TableField">&nbsp;</td><td>
            <asp:Button ID="btnSaveConfig" runat="server" onclick="btnSaveConfig_Click" 
                Text="Guardar Cambios" />
        </td>
    </tr>
</table>
</asp:Content>
