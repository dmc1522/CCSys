<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" Title="Reporte General por Catálogo" AutoEventWireup="true" CodeBehind="frmReporteGralPorCatalogo.aspx.cs" Inherits="Garibay.frmReporteGralPorCatalogo" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
   <table>
            <tr>
                <td class="TableHeader" colspan="2">
                    CONCENTRADOGENERALO POR CATÁLOGO DE CUENTA</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Fecha inicio:&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtFecha1" runat="server" ReadOnly="True"></asp:TextBox>
                    <rjs:popcalendar ID="PopCalendar1" runat="server" AutoPostBack="True" 
                        Control="txtFecha1" onselectionchanged="PopCalendar1_SelectionChanged" 
                        Separator="/" />
                </td>
                <td class="TablaField">
                    Fecha fin:<asp:TextBox ID="txtFecha2" runat="server" ReadOnly="True"></asp:TextBox>
                    <rjs:popcalendar ID="PopCalendar2" runat="server" AutoPostBack="True" 
                        Control="txtFecha2" onselectionchanged="PopCalendar2_SelectionChanged" 
                        Separator="/"  />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtFecha1Larga" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtFecha2Larga" runat="server" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gridMovBancosCatalogo" runat="server" 
                        AutoGenerateColumns="False" DataSourceID="sdsMovBancosporCatalogo" 
                        SkinID="gridNormal" onprerender="gridMovBancosCatalogo_PreRender">
                        <Columns>
                            <asp:BoundField DataField="catalogoMovBanco" HeaderText="Catálogo" 
                                SortExpression="catalogoMovBanco" />
                            <asp:BoundField DataField="subCatalogo" HeaderText="SubCatálogo" 
                                SortExpression="subCatalogo" />
                            <asp:BoundField DataField="Cargos" DataFormatString="{0:C2}" 
                                HeaderText="Cargos" ReadOnly="True" SortExpression="Cargos" />
                            <asp:BoundField DataField="Abonos" DataFormatString="{0:C2}" 
                                HeaderText="Abonos" ReadOnly="True" SortExpression="Abonos" />
                            <asp:BoundField DataField="Total" DataFormatString="{0:C2}" HeaderText="Total" 
                                ReadOnly="True" SortExpression="Total" />
                            <asp:BoundField DataField="cuentaID" HeaderText="cuentaID" 
                                SortExpression="cuentaID" Visible="False" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsMovBancosporCatalogo" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
                        
                        
                        SelectCommand="SELECT catalogoMovimientosBancos.catalogoMovBanco, SUM(MovimientosCuentasBanco.cargo) + SUM(MovimientosCaja.cargo) AS Cargos, SUM(MovimientosCuentasBanco.abono) + SUM(MovimientosCaja.abono) AS Abonos, SUM(MovimientosCuentasBanco.abono) + SUM(MovimientosCaja.abono) - SUM(MovimientosCuentasBanco.cargo) - SUM(MovimientosCaja.cargo) AS Total, SubCatalogoMovimientoBanco.subCatalogo, MovimientosCuentasBanco.cuentaID FROM MovimientosCuentasBanco INNER JOIN catalogoMovimientosBancos ON MovimientosCuentasBanco.catalogoMovBancoFiscalID = catalogoMovimientosBancos.catalogoMovBancoID INNER JOIN MovimientosCaja ON catalogoMovimientosBancos.catalogoMovBancoID = MovimientosCaja.catalogoMovBancoID LEFT OUTER JOIN SubCatalogoMovimientoBanco ON MovimientosCuentasBanco.subCatalogoMovBancoInternoID = SubCatalogoMovimientoBanco.subCatalogoMovBancoID WHERE (MovimientosCuentasBanco.fecha &gt;= @fecha1) AND (MovimientosCuentasBanco.fecha &lt;= @fecha2) GROUP BY catalogoMovimientosBancos.catalogoMovBanco, SubCatalogoMovimientoBanco.subCatalogo, MovimientosCuentasBanco.cuentaID">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtFecha1Larga" 
                                DefaultValue="1900/01/01 00:00:00" Name="fecha1" PropertyName="Text" />
                            <asp:ControlParameter ControlID="txtFecha2Larga" 
                                DefaultValue="1900/01/01 00:00:00" Name="fecha2" PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
</table>

         
        

</asp:Content>
