<%@ Page Language="C#" Theme="skinverde" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmConverProductos.aspx.cs" Inherits="Garibay.Formulario_web11" Title="Conversión de Producto" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

  <script type="text/javascript" src="/scripts/divFunctions.js"></script>
    <script type="text/javascript" src="/scripts/prototype.js"></script>

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
		<td class="TableHeader"  colspan="4">
		CONVERSIÓN DE PRODUCTO GRANEL A EMBASADO(SACOS 50 KG)
		</td>
	</tr>
	<tr valign="middle">
		<td >
		
			<table>
			<tr>
			<td class="TableHeader">
			CICLO :
			</td>
			<td>
		    <asp:DropDownList ID="ddlCiclos" runat="server" DataSourceID="SqlDataSource1" 
			DataTextField="CicloName" DataValueField="cicloID">
		</asp:DropDownList>
			
			</td>
			</tr>
			<tr>
					<td class="TableHeader" >
					BODEGA :
					</td>
					<td >
						<asp:DropDownList ID="dddlBodega" runat="server" DataSourceID="SqlDataSource2" 
							DataTextField="bodega" DataValueField="bodegaID">
						</asp:DropDownList>
						<asp:SqlDataSource ID="SqlDataSource2" runat="server" 
							ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
							SelectCommand="SELECT * FROM [Bodegas]">
						</asp:SqlDataSource>
					</td>
					
				</tr>
				<tr>
					<td class="TableHeader" >
					PRODUCTO ORÍGEN :
					</td>
					<td >
						<asp:DropDownList ID="drpdlOrigen" runat="server" DataSourceID="SqlProductos" 
							DataTextField="Producto" DataValueField="productoID">
						</asp:DropDownList>
						<asp:SqlDataSource ID="SqlProductos" runat="server" 
							ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
							
							SelectCommand="SELECT Productos.productoID, Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Producto, Productos.productoGrupoID FROM Productos INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID ORDER BY  Productos.productoGrupoID,Productos.Nombre">
						</asp:SqlDataSource>
					</td>
					
				</tr>
				
				<tr>
					<td class="TablaField" >
					CANTIDAD :
					</td>
					<td >
						<asp:TextBox ID="txtCantidadOrigen" runat="server"></asp:TextBox>
					</td>
					
				</tr>
				<tr>
					<td class="TablaField" >
					EXISTENCIA ACTUAL :
					</td>
					<td >
						<asp:TextBox ID="txtExistenciaO" runat="server" ReadOnly="true"></asp:TextBox>
					</td>
					
				</tr>
				
			</table>
		</td>
		<td>
		<asp:Label ID="Label1" runat="server" Text=">>" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
		</td>
		
		<td >
				<table>
				<tr>
					<td class="TableHeader" >
					PRODUCTO DESTINO :
					</td>
					<td >
						<asp:DropDownList ID="drpdlDestino" runat="server" DataSourceID="SqlProductos" 
							DataTextField="Producto" DataValueField="productoID">
						</asp:DropDownList>
						
					</td>
					
				</tr>
				<tr>
					<td class="TablaField" >
				     CANTIDAD :
					</td>
					<td >
						
						<asp:TextBox ID="txtCantidadDestino" runat="server"></asp:TextBox>
					</td>
					
				</tr>
				<tr>
					<td class="TablaField" >
					EXISTENCIA ACTUAL :
					</td>
					<td >
						<asp:TextBox ID="txtExistenciaDestino" runat="server" ReadOnly="true"></asp:TextBox>
					</td>
					
				</tr>
				
			</table>
		</td>
	</tr>
	<tr>
	<td  colspan="4" align="center">
	
		<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
			ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
			
			
            SelectCommand="SELECT cicloID, CicloName FROM Ciclos WHERE (cerrado = @cerrado) ORDER BY fechaInicio DESC">
			<SelectParameters>
				<asp:Parameter DefaultValue="FALSE" Name="cerrado" Type="Boolean" />
			</SelectParameters>
		</asp:SqlDataSource>
		<asp:Button ID="btnConvert" runat="server" Text="CONVERTIR" 
			onclick="btnConvert_Click" />
		<asp:Button ID="btnCancelar" runat="server" Text="CANCELAR" 
			onclick="btnCancelar_Click" />
	</td>
	</tr>
	
</table>
</asp:Content>
