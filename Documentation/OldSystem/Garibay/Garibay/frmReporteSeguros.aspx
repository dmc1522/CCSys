<%@ Page Title="Reporte de Seguros" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmReporteSeguros.aspx.cs" Inherits="Garibay.Formulario_web17" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td colspan="4" class="TableHeader" align="center">REPORTE DE SEGUROS</td>
    </tr>
    
    <tr>
        <td  class="TableHeader" align="center">FILTROS:</td>
        <td class="TablaField">Seguro: </td><td>
        <asp:DropDownList ID="ddlSeguros" runat="server" AutoPostBack="True" 
            DataSourceID="sdsSeguros" DataTextField="Descripcion" 
            DataValueField="seguroID" Height="16px" Width="170px" 
			onselectedindexchanged="ddlSeguros_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:SqlDataSource ID="sdsSeguros" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
            
			SelectCommand="SELECT seguroID,  Nombre + '-  $  ' +  cast (CostoPorHectarea as nVarChar) + ' - '  + cast(Descripcion as Nvarchar) as Descripcion FROM SegurosAgricolas order by CostoPorHectarea ASC">
        </asp:SqlDataSource>
              
        </td>
        <td  align="center">
            &nbsp;</td>
    </tr>
    
</table>
    <asp:GridView ID="gvReporte" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlSeguros">
		<Columns>
			<asp:BoundField DataField="Nombre" HeaderText="Nombre" 
				SortExpression="Nombre" />
			<asp:BoundField DataField="hectAseguradas" DataFormatString="{0:c2}" 
				HeaderText="Hectareas Aseguradas" SortExpression="hectAseguradas">
			<ItemStyle HorizontalAlign="Right" />
			</asp:BoundField>
			<asp:BoundField DataField="CostoTotalSeguro" DataFormatString="{0:c2}" 
				HeaderText="Costo Total del Seguro" SortExpression="CostoTotalSeguro">
			<ItemStyle HorizontalAlign="Right" />
			</asp:BoundField>
			<asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
				HeaderText="Fecha" SortExpression="fecha" />
		</Columns>
    </asp:GridView>

    <asp:SqlDataSource ID="SqlSeguros" runat="server" 
		ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
		
		SelectCommand="SELECT Productores.apaterno + SPACE(1) + Productores.amaterno + SPACE(1) + Productores.nombre AS Nombre, solicitud_SeguroAgricola.hectAseguradas, solicitud_SeguroAgricola.CostoTotalSeguro, Solicitudes.fecha FROM Solicitudes INNER JOIN solicitud_SeguroAgricola ON Solicitudes.solicitudID = solicitud_SeguroAgricola.solicitudID INNER JOIN SegurosAgricolas ON solicitud_SeguroAgricola.seguroID = SegurosAgricolas.seguroID INNER JOIN Productores ON Solicitudes.productorID = Productores.productorID WHERE (SegurosAgricolas.seguroID = @seguroID)">
		<SelectParameters>
			<asp:ControlParameter ControlID="ddlSeguros" DefaultValue="-1" Name="seguroID" 
				PropertyName="SelectedValue" />
		</SelectParameters>
	</asp:SqlDataSource>


</asp:Content>
