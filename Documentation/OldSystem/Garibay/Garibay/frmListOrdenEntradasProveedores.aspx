<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListOrdenEntradasProveedores.aspx.cs" Inherits="Garibay.Formulario_web16" %>
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
			DataKeyNames="ordenID" DataSourceID="SqlDataSource1" >
			<Columns>
				<asp:BoundField DataField="CicloName" HeaderText="Ciclo" 
                    SortExpression="CicloName" />
				<asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" 
					DataFormatString="{0:dd/MM/yyyy}" />
				<asp:BoundField DataField="Folio" HeaderText="Folio" SortExpression="Folio" />
				<asp:BoundField DataField="Nombre" HeaderText="Nombre" 
					SortExpression="Nombre" />
				<asp:BoundField DataField="ordenID" HeaderText="ordenID" InsertVisible="False" 
					ReadOnly="True" SortExpression="ordenID" Visible="False" />
				<asp:TemplateField>
					<ItemTemplate>
						<asp:HyperLink ID="HyperLink1" runat="server" 
							NavigateUrl='<%# GetOrdenNavigationURL(Eval("ordenID").ToString()) %>'>ABRIR</asp:HyperLink>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField>
					<ItemTemplate>
						<asp:HyperLink ID="HyperLink2" runat="server" 
							NavigateUrl='<%# GetDeleteNavigationURL(Eval("ordenID").ToString()) %>'>ELIMINAR</asp:HyperLink>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
		<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
			ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
            SelectCommand="SELECT DISTINCT dbo.Orden_de_entrada.fecha, dbo.Orden_de_entrada.folio, dbo.Proveedores.Nombre, dbo.Orden_de_entrada.ordenID, dbo.Ciclos.cicloID, dbo.Ciclos.CicloName FROM dbo.Proveedores INNER JOIN dbo.Orden_de_entrada ON dbo.Proveedores.proveedorID = dbo.Orden_de_entrada.proveedorID INNER JOIN dbo.Ciclos ON dbo.Orden_de_entrada.cicloID = dbo.Ciclos.cicloID ORDER BY dbo.Ciclos.cicloID DESC, dbo.Proveedores.Nombre, dbo.Orden_de_entrada.fecha DESC" >
		</asp:SqlDataSource>
	</asp:Panel>

</asp:Content>
