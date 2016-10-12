<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master"  Title = "Reporte de Garantias sobre Creditos" AutoEventWireup="true" CodeBehind="frmReporteGarantiasSolicitudes.aspx.cs" Inherits="Garibay.frmReporteGarantiasSolicitudes" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    
    <table>
    <tr>
        <td colspan="3" class="TableHeader" align="center">REPORTE DE GARANTIAS PUESTAS 
            SOBRE LOS CRÉDITOS</td>
    </tr>
    
    <tr>
        <td  class="TableHeader" align="center">FILTROS:</td>
        <td class="TablaField">Ciclo: </td><td>
        <asp:DropDownList ID="ddlCiclos" runat="server" AutoPostBack="True" 
            DataSourceID="sdsCiclos" DataTextField="cicloname" 
            DataValueField="cicloId" Height="16px" Width="170px">
        </asp:DropDownList>
        <asp:SqlDataSource ID="sdsCiclos" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
            
			SelectCommand="SELECT cicloID, CicloName FROM Ciclos ORDER BY CicloName DESC">
        </asp:SqlDataSource>
              
        </td>
    </tr>
    
</table>
 <asp:GridView ID="gvReporte" runat="server" AutoGenerateColumns="False" 
        DataSourceID="sdsGarantias">
		<Columns>
			<asp:BoundField DataField="Nombre" HeaderText="Nombre" 
				SortExpression="Nombre" ReadOnly="True" />
			<asp:BoundField DataField="superficieFinanciada" 
				HeaderText="Sup. Financiada" SortExpression="superficieFinanciada">
			</asp:BoundField>
			<asp:BoundField DataField="Monto" DataFormatString="{0:c2}" 
				HeaderText="Monto" SortExpression="Monto">
			</asp:BoundField>
			<asp:BoundField DataField="ConceptoSoporteGarantia" 
                HeaderText="Concepto soporte garantia" 
                SortExpression="ConceptoSoporteGarantia" />
            <asp:BoundField DataField="Descripciondegarantias" 
                HeaderText="Descripcion de garantias" SortExpression="Descripciondegarantias" />
            <asp:BoundField DataField="Valordegarantias" DataFormatString="{0:c2}" 
                HeaderText="Valor Estimado de Garantias" SortExpression="Valordegarantias" />
            <asp:BoundField DataField="montoSoporteGarantia" DataFormatString="{0:c2}" 
                HeaderText="Valor Estimado SoporteGarantia" 
                SortExpression="montoSoporteGarantia" />
            <asp:BoundField DataField="status" HeaderText="Estado " 
                SortExpression="status" />
		</Columns>
    </asp:GridView>

    <asp:SqlDataSource ID="sdsGarantias" runat="server" 
		ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
		
		
    
        SelectCommand="SELECT Productores.apaterno + SPACE(1) + Productores.amaterno + SPACE(1) + Productores.nombre AS Nombre, Solicitudes.fecha, Solicitudes.superficieFinanciada, Solicitudes.Monto, Solicitudes.ConceptoSoporteGarantia, Solicitudes.Descripciondegarantias, SolicitudStatus.status, Solicitudes.Valordegarantias, Solicitudes.montoSoporteGarantia FROM Solicitudes INNER JOIN Productores ON Solicitudes.productorID = Productores.productorID INNER JOIN SolicitudStatus ON Solicitudes.statusID = SolicitudStatus.statusID INNER JOIN Creditos ON Solicitudes.creditoID = Creditos.creditoID WHERE (Creditos.cicloID = @cicloId)">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCiclos" Name="cicloId" 
                PropertyName="SelectedValue" />
        </SelectParameters>
	</asp:SqlDataSource>

         
        

</asp:Content>
