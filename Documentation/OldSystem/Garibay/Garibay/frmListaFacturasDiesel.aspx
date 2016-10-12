<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="True" CodeBehind="frmListaFacturasDiesel.aspx.cs" Inherits="Garibay.Formulario_web14" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="panelMensaje" runat="server">
               <table >
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
	<asp:Panel ID="pnlLista" runat="server">
		<asp:GridView ID="grdvListaFacturas" runat="server" AutoGenerateColumns="False" 
			DataKeyNames="FacturaFolio" DataSourceID="SqlDataSource1" 
			onrowdatabound="grdvListaFacturas_RowDataBound">
			<Columns>
				<asp:BoundField DataField="FacturaFolio" HeaderText="FacturaFolio" 
					ReadOnly="True" SortExpression="FacturaFolio" />
				<asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" 
					DataFormatString="{0:dd/MM/yyyy}" />
				<asp:BoundField DataField="monto" HeaderText="Monto" SortExpression="monto" 
				DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" />
				<asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkOpen" runat="server" 
                            NavigateUrl='<%# GetFacturaNavigationURL(Eval("FacturaFolio").ToString()) %>' Text='<%# "ABRIR" %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
				<asp:TemplateField HeaderText="Tarjetas Diesel" ShowHeader="False">
					<ItemTemplate>
						<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="ELIMINAR" ShowHeader="False">
					<ItemTemplate>
						<asp:HyperLink ID="HyperLink1" runat="server" 
							NavigateUrl='<%# GetURLDelete(Eval("FacturaFolio").ToString()) %>' 
							Text="<%# getText() %>"></asp:HyperLink>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
		<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
			ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
			
			
            SelectCommand="SELECT DISTINCT FacturasDiesel.FacturaFolio, FacturasDiesel.Fecha, FacturasDiesel.monto FROM FacturasDiesel LEFT OUTER JOIN TarjetasDiesel ON FacturasDiesel.FacturaFolio = TarjetasDiesel.FacturaFolio ORDER BY FacturasDiesel.Fecha DESC">
		</asp:SqlDataSource>
	</asp:Panel>

</asp:Content>
