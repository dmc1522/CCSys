<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmPrintPaqueteFormatos.aspx.cs" Inherits="Garibay.frmPrintPaqueteFormatos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
    	<tr>
    		<td class="TableHeader">IMPRIMIR PAQUETE DE FORMATOS</td>
    	</tr>
    	<tr>
    	    <td>
    	    
    	        <asp:Panel ID="pnlFiltros" runat="server">
                </asp:Panel>
    	    
    	    </td>
    	</tr>
    	<tr>
    	    <td>
		<asp:Panel ID="pnlSolicitudes" runat="server">
	
	<table>
	<tr>
	<td class="TablaField">
	    SOLICITUDES</td>
	<td></td>
	<td class="TablaField">
	    &nbsp;</td>
	</tr>
	<tr>
		<td>
			<asp:GridView ID="grdvSolNoPrint" runat="server" 
				AutoGenerateColumns="False" DataKeyNames="solicitudID,productorID" 
                DataSourceID="SqlDataSource1">
				<Columns>
					
					<asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSol" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
					
					<asp:BoundField DataField="solicitudID" HeaderText="# Solicitud" ReadOnly="True" 
						SortExpression="solicitudID" InsertVisible="False" >
					</asp:BoundField>
					<asp:BoundField DataField="productor" HeaderText="Productor" 
                        SortExpression="productor" ReadOnly="True" >
					</asp:BoundField>
					<asp:BoundField DataField="fecha" HeaderText="Fecha" 
						SortExpression="fecha" DataFormatString="{0:dd,MM,aa}" >
					</asp:BoundField>
				    <asp:BoundField DataField="Monto" DataFormatString="{0:c2}" HeaderText="Monto" 
                        SortExpression="Monto" />
                    <asp:BoundField DataField="productorID" HeaderText="productorID" 
                        InsertVisible="False" ReadOnly="True" SortExpression="productorID" 
                        Visible="False" />
				</Columns>
			
			</asp:GridView>
		
			<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
				ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
				
				
                SelectCommand="SELECT Solicitudes.solicitudID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS productor, Solicitudes.Monto, Solicitudes.fecha, Productores.productorID FROM Solicitudes INNER JOIN Productores ON Solicitudes.productorID = Productores.productorID">
			</asp:SqlDataSource>
		
		</td>
		<td>
		<table>
			<tr>
				<td>
					&nbsp;</td>
			</tr>
			<tr>
				<td align="center">
				    &nbsp;</td>
			</tr>
		</table>
		</td>
		<td>
			&nbsp;</td>
		
		</tr>
	</table>	
	<table >
<tr>

		<td class="tablaField">
			<asp:Button ID="btnPrint" runat="server" onclick="btnPrint_Click" 
                Text="Imprimir paquete" />
        </td>
			<td>
				&nbsp;</td>
	</tr>
	
</table>

</asp:Panel>		


            </td>
    	</tr>
    </table>
</asp:Content>
