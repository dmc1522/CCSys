<%@ Page Language="C#" Theme="skinrojo" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmAddModifySalidaDeProducto.aspx.cs" Inherits="Garibay.frmAddModifySalidaDeProducto" Title="Salida de Producto" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
 	<tr>
 		<td class="TableHeader" colspan="2">
            <asp:Label ID="lblSalidaProducto" runat="server" 
                Text="AGREGAR SALIDA DE PRODUCTO"></asp:Label>
            <asp:TextBox ID="textBoxID" runat="server" Visible="False"></asp:TextBox>
        </td>
 	</tr>
 	<tr>
 	 <td class="TablaField">Fecha:</td>
 	 <td>
         <asp:TextBox ID="textBoxFecha" runat="server"></asp:TextBox>
                <rjs:PopCalendar ID="PopCalendar1" runat="server" Separator="/" 
                    Control="textBoxFecha"  />
              
        </td>
 	 <td>&nbsp;</td>
 	</tr>
 	<tr>
 	 <td class="TablaField">Producto:</td>
 	 <td>
         <asp:DropDownList ID="cmbProducto" runat="server" Height="22px" Width="272px" 
             DataSourceID="sqlDataSourceProducto" DataTextField="Nombre" 
             DataValueField="productoID">
         </asp:DropDownList>
         <asp:SqlDataSource ID="sqlDataSourceProducto" runat="server" 
             ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
             SelectCommand="SELECT [productoID], [Nombre] FROM [Productos] ORDER BY [Nombre]">
         </asp:SqlDataSource>
        </td>
 	 <td></td>
 	</tr>
 	<tr>
 	 <td class="TablaField">Bodega:</td>
 	 <td>
         <asp:DropDownList ID="cmbBodega" runat="server" Height="22px" Width="271px" 
             DataSourceID="sqlDataSourceBodega" DataTextField="bodega" 
             DataValueField="bodegaID">
         </asp:DropDownList>
         <asp:SqlDataSource ID="sqlDataSourceBodega" runat="server" 
             ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
             SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
         </asp:SqlDataSource>
        </td>
 	 <td></td>
 	</tr>
 	<tr>
 	 <td class="TablaField">Tipo de movimiento:</td>
 	 <td>
         <asp:DropDownList ID="cmbTipoMovProd" runat="server" Height="22px" 
             Width="271px" DataSourceID="sqlDataSourceTiposDeSalida" 
             DataTextField="tipoMovimiento" DataValueField="tipoMovProdID">
         </asp:DropDownList>
         <asp:SqlDataSource ID="sqlDataSourceTiposDeSalida" runat="server" 
             ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
             SelectCommand="SELECT [tipoMovProdID], [tipoMovimiento] FROM [TiposMovimientoProducto] ORDER BY [tipoMovimiento]">
         </asp:SqlDataSource>
        </td>
 	 <td></td>
 	</tr>
 	
 	<tr>
 	 <td class="TablaField">Cantidad:</td>
 	 <td>
         <asp:TextBox ID="txtCantidad" runat="server" Width="139px"></asp:TextBox>
        </td>
 	 <td>
         <asp:RequiredFieldValidator ID="valCantidadRequired" runat="server" 
             ControlToValidate="txtCantidad" CssClass="Validator" 
             ErrorMessage="El campo cantidad es necesario"></asp:RequiredFieldValidator>
         <br />
         <asp:RegularExpressionValidator ID="valCantidadFormat" runat="server" 
             ControlToValidate="txtCantidad" CssClass="Validator" 
             ErrorMessage="Escriba una cantidad válida" ValidationExpression="\d+(.\d*)?"></asp:RegularExpressionValidator>
        </td>
 	</tr>
 	<tr>
 	 <td class="TablaField">Observaciones:</td>
 	 <td>
         <asp:TextBox ID="txtObservaciones" runat="server" Height="73px" 
             TextMode="MultiLine" Width="271px"></asp:TextBox>
        </td>
 	 <td></td>
 	</tr>
 	<tr>
 	 <td colspan="2">
         <asp:Panel ID="panelmensaje" runat="server">
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
                         <asp:Label ID="lblMensajeOperationresult" runat="server" 
                             SkinID="lblMensajeOperationresult" Text="Label"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td style="text-align: center">
                         <asp:Label ID="lblMensajeException" runat="server" SkinID="lblMensajeException" 
                             Text="SI NO HAY EXC BORREN EL TEXTO"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td align="center">
                         <asp:Button ID="btnAceptarList" runat="server" onclick="txtAceptarList_Click" 
                             Text="Aceptar" />
                     </td>
                 </tr>
             </table>
         </asp:Panel>
        </td>
 	 <td>&nbsp;</td>
 	</tr>
 	<tr>
 	 <td align="center" colspan="2">
         <asp:Button ID="btnAgregar" runat="server" CssClass="Button" Text="Agregar" 
             Width="101px" onclick="btnAgregar_Click" />
         <asp:Button ID="btnModificar" runat="server" CssClass="Button" Text="Modificar" 
             Width="100px" onclick="btnModificar_Click" />
         <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
             CssClass="Button" Text="Cancelar" Width="100px" 
             onclick="btnCancelar_Click" />
        </td>
 	</tr>
 </table>
</asp:Content>
