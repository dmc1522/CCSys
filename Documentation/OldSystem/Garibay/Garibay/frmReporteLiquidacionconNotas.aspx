<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" Title ="Reporte Liquidaciones con Notas" AutoEventWireup="true" CodeBehind="frmReporteLiquidacionconNotas.aspx.cs" Inherits="Garibay.frmReporteLiquidacionconNotas" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upPanel" runat="Server">
    <ContentTemplate>
    <asp:UpdateProgress id= "upprog" runat="Server" AssociatedUpdatePanelID="upPanel" 
            DisplayAfter="0">
     <ProgressTemplate>
         <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/cargando.gif" />
         Cargando datos...
     </ProgressTemplate>
    
    </asp:UpdateProgress>
    
   
    <table >
	<tr>
		<td>
            <table>
                <tr>
                    <td class="TableHeader" align="center" colspan="3">
                        REPORTE DE LIQUIDACIONES COBRADAS Y QUE TIENEN NOTAS </td>
                </tr>
                <tr>
                    <td class="TableHeader">
                        FILTROS:</td>
                    <td class="TablaField">
                        Ciclo:</td>
                    <td>
                        <asp:DropDownList ID="drpdlCiclo" runat="server" AutoPostBack="True" 
                            DataSourceID="sdsCiclos" DataTextField="CicloName" DataValueField="cicloID" 
                            Height="23px" Width="164px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos]">
                        </asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </td>
	</tr>
	<tr>
	    <td>
            <asp:GridView ID="gvLiquidaciones" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="LiquidacionID" DataSourceID="sdsLiquidaciones" 
                ondatabound="gvLiquidaciones_DataBound" ShowFooter="True">
                <Columns>
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" 
                        SortExpression="nombre" />
                    <asp:BoundField DataField="LiquidacionID" HeaderText="# Liquidacion" 
                        InsertVisible="False" ReadOnly="True" SortExpression="LiquidacionID" />
                    <asp:BoundField DataField="fecha" HeaderText="Fecha" 
                        SortExpression="fecha" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="subTotal" HeaderText="Boletas" SortExpression="subTotal" 
                        DataFormatString="{0:C2}" >
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="notas" HeaderText="Notas" 
                        SortExpression="notas" DataFormatString="{0:C2}" >
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="intereses" HeaderText="Interés" 
                        SortExpression="intereses" DataFormatString="{0:C2}" >
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="seguro" DataFormatString="{0:C2}" 
                        HeaderText="Seguro" SortExpression="seguro" >
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalPagos" DataFormatString="{0:C2}" 
                        HeaderText="Pagos" ReadOnly="True" SortExpression="TotalPagos" >
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsLiquidaciones" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                
                
                
                
                
                SelectCommand="SELECT Liquidaciones.nombre, Liquidaciones.LiquidacionID, Liquidaciones.fecha, Liquidaciones.subTotal, Liquidaciones.notas, Liquidaciones.intereses, Liquidaciones.seguro, (SELECT ISNULL(SUM(MovimientosCaja.cargo),0.00) + ISNULL(SUM(MovimientosCuentasBanco.cargo),0.00) AS Pagos FROM PagosLiquidacion LEFT OUTER JOIN MovimientosCuentasBanco ON PagosLiquidacion.movbanID = MovimientosCuentasBanco.movbanID LEFT OUTER JOIN MovimientosCaja ON PagosLiquidacion.movimientoID = MovimientosCaja.movimientoID WHERE (PagosLiquidacion.liquidacionID = Liquidaciones.LiquidacionID)) AS TotalPagos FROM Liquidaciones INNER JOIN Ciclos ON Liquidaciones.cicloID = Ciclos.cicloID WHERE (Liquidaciones.cicloID = @cicloID) AND (Liquidaciones.cobrada = 1) AND (Liquidaciones.notas &gt; 0) ORDER BY Liquidaciones.liquidacionID,Liquidaciones.nombre, Liquidaciones.fecha DESC">
                <SelectParameters>
                    <asp:ControlParameter ControlID="drpdlCiclo" DefaultValue="-1" Name="cicloID" 
                        PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
	</tr>
</table>
 </ContentTemplate>
 </asp:UpdatePanel> 
</asp:Content>
