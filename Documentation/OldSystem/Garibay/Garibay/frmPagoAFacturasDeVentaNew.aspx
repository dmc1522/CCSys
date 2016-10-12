<%@ Page Title="Agregar Pago a Facturas de Venta" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmPagoAFacturasDeVentaNew.aspx.cs" Inherits="Garibay.frmPagoAFacturasDeVentaNew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>CLIENTE:</td><td>
                <asp:DropDownList ID="ddlClientesVenta" runat="server" 
                    DataSourceID="sdsClientesVenta" DataTextField="nombre" 
                    DataValueField="clienteventaID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsClientesVenta" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT [clienteventaID], [nombre] FROM [ClientesVentas] ORDER BY [nombre]">
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server" GroupingText="Facturas disponibles">
        <asp:CheckBox ID="chkAddFacturas" runat="server" 
            Text="Agregar Factura a Pago" />
        <asp:Panel ID="pnlAddFacturas" runat="server">
            <asp:GridView ID="gvFacturasDisponibles" runat="server" 
                DataSourceID="sdsFacturasDisponibles">
            </asp:GridView>
            <asp:SqlDataSource ID="sdsFacturasDisponibles" runat="server">
            </asp:SqlDataSource>
        </asp:Panel>
        <cc1:CollapsiblePanelExtender ID="pnlAddFacturas_CollapsiblePanelExtender" 
            runat="server" CollapseControlID="chkAddFacturas" Collapsed="True" 
            Enabled="True" ExpandControlID="chkAddFacturas" 
            TargetControlID="pnlAddFacturas">
        </cc1:CollapsiblePanelExtender>
    </asp:Panel>
</asp:Content>
