<%@ Page Theme="skinverde" Title="Saldos Mensuales Caja Chica" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmCajaChicaSaldosMensuales.aspx.cs" Inherits="Garibay.frmCajaChicaSaldosMensuales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel runat="Server" ID="UpdatePanel"><ContentTemplate>
<asp:UpdateProgress runat="Server" ID="UpdateProgress" 
        AssociatedUpdatePanelID="UpdatePanel" DisplayAfter="0">
<ProgressTemplate>
    <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/cargando.gif" />CARGANDO SALDOS....
</ProgressTemplate>
</asp:UpdateProgress>
    <table>
    	<tr>
    		<td class="TableField">Bodega:</td><td>
                <asp:DropDownList ID="ddlBodegas" runat="server" AutoPostBack="True" 
                    DataSourceID="sdsBodegas" DataTextField="bodega" DataValueField="bodegaID" 
                    onselectedindexchanged="ddlBodegas_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsBodegas" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]"></asp:SqlDataSource>
            </td>
    	</tr>
    </table>
    <asp:SqlDataSource ID="sdsSaldos" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        
    
        
        SelectCommand="SELECT YEAR(MovimientosCaja.fecha) AS anio, MONTH(MovimientosCaja.fecha) AS mes, SUM(MovimientosCaja.cargo) AS cargos, SUM(MovimientosCaja.abono) AS abono, MovimientosCaja.bodegaID, (SELECT SUM(abono - cargo) AS Expr1 FROM MovimientosCaja AS MC WHERE (bodegaID = movimientoscaja.bodegaid) AND (YEAR(fecha) &lt;= YEAR(MovimientosCaja.fecha)) AND (MONTH(fecha) &lt;= MONTH(MovimientosCaja.fecha))) AS Saldo FROM MovimientosCaja INNER JOIN Meses ON MONTH(MovimientosCaja.fecha) = Meses.monthID GROUP BY YEAR(MovimientosCaja.fecha), MONTH(MovimientosCaja.fecha), MovimientosCaja.bodegaID HAVING (MovimientosCaja.bodegaID = @bodegaID) ORDER BY anio DESC, mes DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlBodegas" DefaultValue="-1" Name="bodegaID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="gvSaldosMensuales" runat="server" 
    AllowSorting="True" AutoGenerateColumns="False" DataSourceID="sdsSaldos" 
    PageSize="100">
        <Columns>
            <asp:BoundField DataField="anio" HeaderText="Año" SortExpression="anio" />
            <asp:BoundField DataField="mes" HeaderText="Mes" SortExpression="mes" />
            <asp:BoundField DataField="abono" DataFormatString="{0:C2}" HeaderText="Abono" 
                SortExpression="abono">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="cargos" HeaderText="Cargos" SortExpression="cargos" 
                DataFormatString="{0:C2}" >
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="Saldo" HeaderText="Saldo" SortExpression="Saldo" 
                DataFormatString="{0:c}" />
        </Columns>
    </asp:GridView>
    </ContentTemplate></asp:UpdatePanel>
</asp:Content>
