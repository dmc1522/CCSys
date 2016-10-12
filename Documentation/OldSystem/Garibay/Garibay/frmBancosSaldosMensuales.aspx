<%@ Page Title="Saldos mensuales" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmBancosSaldosMensuales.aspx.cs" Inherits="Garibay.frmBancosSaldosMensuales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
    <tr>
        <td align="center" class="TableHeader">
            LISTA DE SALDOS MENSUALES PARA LOS MOVIMIENTOS DE BANCO</td>
    </tr>
    <tr>
        <td>
       
            <asp:Panel ID="pnlFiltros" runat="server" GroupingText="Filtros">
            <table>
                <tr>
                    <td class="TablaField">
                        Cuenta:</td>
                    <td>
                        <asp:DropDownList ID="cmbCuenta" runat="server" DataSourceID="SqlDataSource3" 
                            DataTextField="Cuenta" DataValueField="cuentaID" Height="22px" 
                            Width="423px" AutoPostBack="True" 
                            onselectedindexchanged="cmbCuenta_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    </table>
        </asp:Panel>
       </td>
    </tr>
   <tr>
        <td>
                             
                        <asp:GridView ID="gridSaldosMensuales" runat="server" 
                            AutoGenerateColumns="False" DataKeyNames="cuentaID">
                            <Columns>
                                <asp:BoundField DataField="year" HeaderText="Año" />
                                <asp:BoundField DataField="month" HeaderText="Mes" SortExpression="month">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo" DataFormatString="{0:c}" HeaderText="Saldo" 
                                    SortExpression="saldo">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
            <br />
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                
                
                SelectCommand="SELECT CuentasDeBanco.cuentaID, Bancos.nombre + ' - ' + CuentasDeBanco.NumeroDeCuenta + ' - ' + CuentasDeBanco.Titular AS Cuenta FROM CuentasDeBanco INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID ORDER BY Cuenta">
            </asp:SqlDataSource>
        </td>
    </tr>
</table>
</asp:Content>
