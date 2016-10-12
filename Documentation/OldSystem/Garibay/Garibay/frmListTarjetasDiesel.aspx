<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListTarjetasDiesel.aspx.cs" Inherits="Garibay.Formulario_web12" Title="Lista de Tarjetas de Diesel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="panelmensaje" runat="server" > 
        <table>
            <tr>
                <td style="text-align: center">
                           
                           <asp:Image ID="imagenbien" runat="server" ImageUrl="~/imagenes/palomita.jpg" 
                               Visible="False" />
                           <asp:Image ID="imagenmal" runat="server" ImageUrl="~/imagenes/tache.jpg" 
                               Visible="False" />
                           <asp:Label ID="lblMensajetitle" runat="server" SkinID="lblMensajeTitle" 
                               Text="PRUEBA"></asp:Label>
                       </td>
            </tr>
            <tr>
                <td style="text-align: center">
                           <asp:Label ID="lblMensajeOperationresult" runat="server"  Text="Label" 
                               SkinID="lblMensajeOperationresult"></asp:Label>
                       </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblMensajeException" runat="server" SkinID="lblMensajeException" 
                        Text="SI NO HAY EXC BORREN EL TEXTO"></asp:Label>
                </td>
            </tr>
        </table>

        
      
        </asp:Panel>

<table>
	<tr>
		<td class="TableHeader">
		LISTA DE TARJETAS DIESEL
		</td>
	</tr>
	<tr>
		<td>
			<asp:GridView ID="grdvTarjetas" runat="server" DataSourceID="SqlDataSource1" 
				AutoGenerateColumns="False" DataKeyNames="folio" 
				onrowdatabound="grdvTarjetas_RowDataBound" 
				onselectedindexchanged="grdvTarjetas_SelectedIndexChanged">
				<Columns>
					<asp:BoundField DataField="folio" HeaderText="Folio" ReadOnly="True" 
						SortExpression="folio" />
					<asp:BoundField DataField="monto" HeaderText="Monto" SortExpression="monto" />
					<asp:BoundField DataField="litros" HeaderText="Litros" 
						SortExpression="litros" />
					<asp:TemplateField HeaderText="Movimientos de Caja">
						<ItemTemplate>
							<asp:HyperLink ID="HyperLink1" runat="server">HyperLink</asp:HyperLink>
						</ItemTemplate>
						<ItemStyle HorizontalAlign="Center" />
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Factura Gas.">
						<ItemTemplate>
							<asp:HyperLink ID="HyperLink3" runat="server">HyperLink</asp:HyperLink>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Eliminar">
						<ItemTemplate>
							<asp:HyperLink ID="HyperLink2" runat="server">HyperLink</asp:HyperLink>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			
			</asp:GridView>
			<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
				ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
				SelectCommand="SELECT [folio], [monto], [litros] FROM [TarjetasDiesel]"></asp:SqlDataSource>
			</td>
	</tr>
</table>
</asp:Content>
