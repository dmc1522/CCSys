﻿<%@ Page Language="C#" Title= "Reporte por Sub catálogos de banco"MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmReporteSubCatalogoMovBancos.aspx.cs" Inherits="Garibay.frmReporteSubCatalogoMovBancos" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

           <table>
            <tr>
                <td class="TableHeader" colspan="2" align="center">
                    CONCENTRADO DE MOVIMIENTOS DE BANCO POR CATÁLOGO DE CUENTA</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Fecha inicio:&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtFecha1" runat="server" ReadOnly="True"></asp:TextBox>
                    <rjs:PopCalendar ID="PopCalendar1" runat="server" AutoPostBack="True" 
                        Control="txtFecha1" onselectionchanged="PopCalendar1_SelectionChanged" 
                        Separator="/" />
                    <asp:TextBox ID="txtFecha1Larga" runat="server" Visible="False" Height="16px" 
                        Width="41px"></asp:TextBox>
                    <asp:TextBox ID="txtFecha2Larga" runat="server" Visible="False" Height="16px" 
                        Width="33px"></asp:TextBox>
                </td>
                <td class="TablaField">
                    Fecha fin:<asp:TextBox ID="txtFecha2" runat="server" ReadOnly="True"></asp:TextBox>
                    <rjs:PopCalendar ID="PopCalendar2" runat="server" AutoPostBack="True" 
                        Control="txtFecha2" onselectionchanged="PopCalendar2_SelectionChanged" 
                        Separator="/" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table>
                        <tr>
                            <td class="TablaField">
                                Grupo:</td>
                            <td>
                                <asp:DropDownList ID="DropDownListGrupo" runat="server" 
                                    DataSourceID="SqlDataSourceGrupos" DataTextField="grupoCatalogo" 
                                    DataValueField="grupoCatalogosID" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSourceGrupos" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos] ORDER BY [grupoCatalogo]">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Catálogo:</td>
                            <td>
                                <asp:DropDownList ID="DropDownListCatalogoMov" runat="server" 
                                    DataSourceID="SqlDataSourceCatalogos" DataTextField="catalogoMovBanco" 
                                    DataValueField="catalogoMovBancoID" AutoPostBack="True" 
                                    onselectedindexchanged="DropDownListCatalogoMov_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSourceCatalogos" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT [catalogoMovBancoID], [catalogoMovBanco] FROM [catalogoMovimientosBancos] WHERE ([grupoCatalogoID] = @grupoCatalogoID)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="DropDownListGrupo" DefaultValue="-2" 
                                            Name="grupoCatalogoID" PropertyName="SelectedValue" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gridMovBancosCatalogo" runat="server" 
                        AutoGenerateColumns="False" DataSourceID="sdsMovBancosporCatalogo" 
                        SkinID="gridNormal">
                        <Columns>
                            <asp:BoundField DataField="subCatalogo" HeaderText="SubCatalogo" 
                                SortExpression="subCatalogo" />
                            <asp:BoundField DataField="Cargos" DataFormatString="{0:C2}" 
                                HeaderText="Cargos" ReadOnly="True" SortExpression="Cargos" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Abonos" DataFormatString="{0:C2}" 
                                HeaderText="Abonos" ReadOnly="True" SortExpression="Abonos" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Total" HeaderText="Total" 
                                SortExpression="Total" DataFormatString="{0:C2}" >
<ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsMovBancosporCatalogo" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
                        
                        
                        
                        SelectCommand="SELECT     SUM(MovimientosCuentasBanco.cargo) AS Cargos, SUM(MovimientosCuentasBanco.abono) AS Abonos, SUM(MovimientosCuentasBanco.abono)     - SUM(MovimientosCuentasBanco.cargo) AS Total, SubCatalogoMovimientoBanco.subCatalogo
FROM         MovimientosCuentasBanco INNER JOIN     SubCatalogoMovimientoBanco ON MovimientosCuentasBanco.subCatalogoMovBancoInternoID = SubCatalogoMovimientoBanco.subCatalogoMovBancoID
WHERE (MovimientosCuentasBanco.fecha &gt;= @fecha1) AND (MovimientosCuentasBanco.fecha &lt;= @fecha2)  AND MovimientosCuentasBanco.catalogoMovBancoInternoID  = @catalogoMovId 
GROUP BY SubCatalogoMovimientoBanco.subCatalogo">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtFecha1Larga" 
                                DefaultValue="1900/01/01 00:00:00" Name="fecha1" PropertyName="Text" />
                            <asp:ControlParameter ControlID="txtFecha2Larga" 
                                DefaultValue="1900/01/01 00:00:00" Name="fecha2" PropertyName="Text" />
                            <asp:ControlParameter ControlID="DropDownListCatalogoMov" DefaultValue="-2" 
                                Name="catalogoMovId" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" class="TableHeader">
                    &nbsp;(MOSTRAR SOLO UNA CUENTA)</td>
            </tr>
            <tr>
                <td colspan="2" align="left" class="TablaField"  >
                    Cuenta:&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="drpdlCuentas" runat="server" AutoPostBack="True" 
                        DataSourceID="sdsCuentas" DataTextField="Cuenta" DataValueField="cuentaID" 
                        Height="22px" Width="380px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsCuentas" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT CuentasDeBanco.cuentaID, Bancos.nombre + ' - ' + CuentasDeBanco.NumeroDeCuenta AS Cuenta FROM CuentasDeBanco INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left"  >
                    <asp:GridView ID="gridMovBancosCatalogoporCuenta" runat="server" 
                        AutoGenerateColumns="False" DataSourceID="sdsMovBancosporCatalogoporCuentaBanco" 
                        SkinID="gridNormal">
                        <Columns>
                            <asp:BoundField DataField="subCatalogo" HeaderText="SubCatalogo" 
                                SortExpression="subCatalogo" />
                            <asp:BoundField DataField="Cargos" DataFormatString="{0:C2}" 
                                HeaderText="Cargos" ReadOnly="True" SortExpression="Cargos" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Abonos" DataFormatString="{0:C2}" 
                                HeaderText="Abonos" ReadOnly="True" SortExpression="Abonos" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Total" DataFormatString="{0:C2}" HeaderText="Total" 
                                ReadOnly="True" SortExpression="Total" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsMovBancosporCatalogoporCuentaBanco" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
                        
                        
                        
                        SelectCommand="SELECT     SUM(MovimientosCuentasBanco.cargo) AS Cargos, SUM(MovimientosCuentasBanco.abono) AS Abonos, SUM(MovimientosCuentasBanco.abono)     - SUM(MovimientosCuentasBanco.cargo) AS Total, SubCatalogoMovimientoBanco.subCatalogo
FROM         MovimientosCuentasBanco INNER JOIN     SubCatalogoMovimientoBanco ON MovimientosCuentasBanco.subCatalogoMovBancoInternoID = SubCatalogoMovimientoBanco.subCatalogoMovBancoID
WHERE (MovimientosCuentasBanco.fecha &gt;= @fecha1) AND (MovimientosCuentasBanco.fecha &lt;= @fecha2)  AND MovimientosCuentasBanco.catalogoMovBancoInternoID  = @catalogoMovId AND MovimientosCuentasBanco.cuentaId = @cuentaId 
GROUP BY SubCatalogoMovimientoBanco.subCatalogo">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtFecha1Larga" 
                                DefaultValue="1900/01/01 00:00:00" Name="fecha1" PropertyName="Text" />
                            <asp:ControlParameter ControlID="txtFecha2Larga" 
                                DefaultValue="1900/01/01 00:00:00" Name="fecha2" PropertyName="Text" />
                            <asp:ControlParameter ControlID="DropDownListCatalogoMov" DefaultValue="-2" 
                                Name="catalogoMovId" PropertyName="SelectedValue" />
                            <asp:ControlParameter ControlID="drpdlCuentas" DefaultValue="-1" 
                                Name="cuentaID" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
</table>



        

</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style3
        {
            width: 100%;
        }
    </style>

</asp:Content>

