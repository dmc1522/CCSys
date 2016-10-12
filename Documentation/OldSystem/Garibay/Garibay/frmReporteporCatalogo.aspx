<%@ Page Title="Reporte por catalogo de Cuentas" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmReporteporCatalogo.aspx.cs" Inherits="Garibay.frmReporteporCatalogo" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
    <tr>
        <td colspan="4" class="TableHeader" align="center">REPORTE DE CARGOS Y ABONOS POR 
            CATÁLOGO</td>
    </tr>
    <tr>
        <td colspan="4" class="TableHeader" align="center">PERIODO:</td>
    </tr>
    <tr>
        <td>DE:</td><td>
        <asp:TextBox ID="txtDE" runat="server"></asp:TextBox>
                <rjs:PopCalendar ID="PopCalendar1" runat="server" 
                    OnSelectionChanged = "PopCalendar1_SelectionChanged" Separator="/" 
                    Control="txtDE" AutoPostBack="True"  />
              
        </td><td>A:</td><td>
        <asp:TextBox ID="txtA" runat="server"></asp:TextBox>
                <rjs:PopCalendar ID="PopCalendar2" runat="server" 
                    OnSelectionChanged = "PopCalendar2_SelectionChanged" Separator="/" 
                    Control="txtA" AutoPostBack="True"  />
              
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar Reporte" />
            <asp:TextBox ID="txtFechaInicio" runat="server" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtFechaFin" runat="server" Visible="False"></asp:TextBox>
        </td>
    </tr>
</table>
    <asp:GridView ID="gvReporte" runat="server" AutoGenerateColumns="False" 
    DataKeyNames="grupoCatalogosID,catalogoMovBancoID" DataSourceID="sdsReporte">
        <Columns>
            <asp:BoundField DataField="grupoCatalogo" HeaderText="Grupo Catálogo" 
                SortExpression="grupoCatalogo" />
            <asp:BoundField DataField="catalogoMovBanco" HeaderText="Catálogo " 
                SortExpression="catalogoMovBanco" />
            <asp:BoundField DataField="BancoCargos" HeaderText="Cargos Banco" 
                ReadOnly="True" SortExpression="BancoCargos" DataFormatString="{0:C}" >
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="BancoAbonos" HeaderText="Abonos Banco" 
                ReadOnly="True" SortExpression="BancoAbonos" DataFormatString="{0:C}" >
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="CajaCargos" HeaderText="Cargos Caja Chica" ReadOnly="True" 
                SortExpression="CajaCargos" DataFormatString="{0:C}" >
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="CajaAbonos" HeaderText="Abonos Caja" ReadOnly="True" 
                SortExpression="CajaAbonos" DataFormatString="{0:C}" >
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
<asp:SqlDataSource ID="sdsReporte" runat="server" 
    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
    
        SelectCommand="SELECT GruposCatalogosMovBancos.grupoCatalogosID, GruposCatalogosMovBancos.grupoCatalogo, 
catalogoMovimientosBancos.catalogoMovBancoID, catalogoMovimientosBancos.catalogoMovBanco, 
(SELECT SUM(cargo) AS BancoCargos FROM MovimientosCuentasBanco
 WHERE (catalogoMovBancoInternoID = catalogoMovimientosBancos.catalogoMovBancoID) AND MovimientosCuentasBanco.fecha &gt;= @fechainicio AND MovimientosCuentasBanco.fecha&lt;= @fechafin) AS BancoCargos, 
(SELECT SUM(abono) AS BancoAbonos FROM MovimientosCuentasBanco AS MovimientosCuentasBanco_1 
 WHERE (catalogoMovBancoInternoID = catalogoMovimientosBancos.catalogoMovBancoID) AND MovimientosCuentasBanco_1.fecha &gt;= @fechainicio AND MovimientosCuentasBanco_1.fecha&lt;= @fechafin)AS BancoAbonos, 
(SELECT SUM(cargo) AS Expr1 FROM MovimientosCaja 
 WHERE (catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID) AND MovimientosCaja.fecha &gt;= @fechainicio AND MovimientosCaja.fecha&lt;= @fechafin) AS CajaCargos, 
(SELECT SUM(abono) AS Expr1 FROM MovimientosCaja AS MovimientosCaja_1 
 WHERE (catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID) AND MovimientosCaja_1.fecha &gt;= @fechainicio AND MovimientosCaja_1.fecha&lt;= @fechafin) AS CajaAbonos 
 FROM catalogoMovimientosBancos INNER JOIN GruposCatalogosMovBancos ON catalogoMovimientosBancos.grupoCatalogoID = GruposCatalogosMovBancos.grupoCatalogosID ORDER BY GruposCatalogosMovBancos.grupoCatalogo, catalogoMovimientosBancos.catalogoMovBanco">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtFechaInicio" DefaultValue="2010/01/01" 
            Name="fechainicio" PropertyName="Text" />
        <asp:ControlParameter ControlID="txtFechaFin" DefaultValue="2010/12/31" 
            Name="fechafin" PropertyName="Text" />
    </SelectParameters>
</asp:SqlDataSource>
</asp:Content>
