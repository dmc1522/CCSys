<%@ Page Title="Reporte de precios de venta" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmReportePreciosVenta.aspx.cs" Inherits="Garibay.frmReportePreciosVenta" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
    <tr>
        <td colspan="4" class="TableHeader" align="center">REPORTE DE PRECIOS DE VENTA</td>
    </tr>
    
    <tr>
        <td  class="TableHeader" align="center">FILTROS:</td>
        <td class="TablaField">Tipo de producto: </td><td>
        <asp:DropDownList ID="ddlTiposProducto" runat="server" AutoPostBack="True" 
            DataSourceID="sdsTipoProducto" DataTextField="grupo" 
            DataValueField="grupoID" Height="16px" Width="170px">
        </asp:DropDownList>
        <asp:SqlDataSource ID="sdsTipoProducto" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
            SelectCommand="SELECT [grupoID], [grupo] FROM [productoGrupos]">
        </asp:SqlDataSource>
              
        </td>
        <td  align="center">
            <asp:Button ID="btnPrint" runat="server" Text="IMPRIMIR" 
                onclick="btnPrint_Click" />
        </td>
    </tr>
    
</table>
    <asp:GridView ID="gvReporte" runat="server" AutoGenerateColumns="False" 
        DataSourceID="sdsReportePrecios">
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Producto" 
                SortExpression="Nombre" />
            <asp:BoundField DataField="precio1" HeaderText="Precio" 
                SortExpression="precio1" DataFormatString="{0:c}" />
        </Columns>
    </asp:GridView>

    <asp:SqlDataSource ID="sdsReportePrecios" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        SelectCommand="SELECT Productos.Nombre, Productos.precio1 FROM productoGrupos INNER JOIN Productos ON productoGrupos.grupoID = Productos.productoGrupoID WHERE (productoGrupos.grupoID = @grupoID)">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlTiposProducto" DefaultValue="-1" 
                Name="grupoID" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
