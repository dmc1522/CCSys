<%@ Page Language="C#" Theme="skinrojo" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmUsuarios.aspx.cs" Inherits="Garibay.WebForm1" Title="Usuarios" %>
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

        
        

         <asp:Panel id="panellistaUsuarios" runat="Server">
           <table>
               <tr>
                   <td align="center" class="TableHeader">
                       USUARIOS</td>
               </tr>
               <tr>
                   <td>
                       <asp:Button ID="btnAgregarDeLista" runat="server" CssClass="Button" 
                           onclick="btnAgregar_Click" Text="Agregar" Width="100px" />
                       <asp:Button ID="btnModificarDeLista" runat="server" CssClass="Button" 
                           onclick="btnModificar_Click" Text="Modificar" Width="100px" />
                       <asp:Button ID="btnEliminar" runat="server" CssClass="Button" Text="Eliminar" 
                           Width="100px" onclick="btnEliminar_Click" />
                   </td>
               </tr>
               <tr>
                   <td>
                       <asp:GridView ID="gridUsers" runat="server" AllowPaging="True" 
                           AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                           DataKeyNames="userID,securitylevelID,username,securitylevel,enabled,Nombre,email" 
                           DataSourceID="SqlDataSource1" ForeColor="White" GridLines="None" 
                           onselectedindexchanged="gridUsers_SelectedIndexChanged">
                           <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                           <HeaderStyle CssClass="TableHeader" />
                           <AlternatingRowStyle BackColor="White" />
                           <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                           <Columns>
                               <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                                   ShowSelectButton="True" />
                               <asp:BoundField DataField="userID" HeaderText="# Usuario" InsertVisible="False" 
                                   ReadOnly="True" SortExpression="userID" />
                               <asp:BoundField DataField="username" HeaderText="Nombre de Usuario" 
                                   SortExpression="username" />
                               <asp:BoundField DataField="securitylevel" HeaderText="Nivel de Seguridad" 
                                   SortExpression="securitylevel" />
                               <asp:CheckBoxField DataField="enabled" HeaderText="Activo" 
                                   SortExpression="enabled" />
                               <asp:BoundField DataField="Nombre" HeaderText="Nombre" 
                                   SortExpression="Nombre" />
                               <asp:BoundField DataField="email" HeaderText="E-mail" SortExpression="email" />
                               <asp:BoundField DataField="securitylevelID" HeaderText="securitylevelID" 
                                   ReadOnly="True" SortExpression="securitylevelID" Visible="False" />
                           </Columns>
                       </asp:GridView>
                       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                           ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                           ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
                           
                           SelectCommand="SELECT Users.userID, Users.username, SecurityLevels.securitylevel, Users.enabled, Users.Nombre, SecurityLevels.securitylevelID, Users.email FROM Users INNER JOIN SecurityLevels ON Users.securitylevelID = SecurityLevels.securitylevelID">
                       </asp:SqlDataSource>

                   </td>
               </tr>
           </table>
         </asp:Panel>
         <asp:Panel id="panelagregarUsuario" runat="Server">
           <table>
           	<tr>
           		<td align="center" colspan="2" class="TableHeader">
                    <asp:Label ID="lblUsuarios" runat="server" Text="AGREGAR USUARIO"></asp:Label>
                </td>
           	</tr>
           	<tr>
           	   <td class="TablaField">Nombre de usuario:</td> <td> 
                <asp:TextBox ID="txtUsername" runat="server" Width="200px" MaxLength="20" 
                    ontextchanged="txtUsername_TextChanged"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="valUsername" runat="server" 
                        ControlToValidate="txtUsername" 
                        ErrorMessage="El campo Nombre de usuario es necesario" 
                        CssClass="Validator"></asp:RequiredFieldValidator>
                </td>
           	</tr>
           	<tr>
           	 <td class="TablaField">Contraseña:</td>
           	 <td>
                 <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="200px" 
                     MaxLength="20"></asp:TextBox>
                 
                </td>
           	 <td>
                 <asp:CustomValidator ID="valPassword" runat="server" 
                     ClientValidationFunction="validaPassword" ControlToValidate="txtPassword" 
                     CssClass="Validator" 
                     ErrorMessage="La longitud de la contraseña es mínimo de 6 caracteres" 
                     ValidateEmptyText="True"></asp:CustomValidator>
                </td>
           	</tr>
           	   <tr>
                   <td class="TablaField">
                       Repetir contraseña:</td>
                   <td>
                       <asp:TextBox ID="txtPassword2" runat="server" TextMode="Password" Width="200px" 
                           MaxLength="20"></asp:TextBox>
                   </td>
                   <td>
                       <asp:CompareValidator ID="valPassword2" runat="server" 
                           ControlToCompare="txtPassword2" ControlToValidate="txtPassword" 
                           CssClass="Validator" ErrorMessage="Las contraseñas deben de ser iguales"></asp:CompareValidator>
                   </td>
               </tr>
           	<tr>
           	   <td class="TablaField">Nivel de seguridad::</td> <td> 
                <asp:DropDownList ID="cmbLevelSecurity" runat="server" 
                    DataSourceID="SqlDataSource2" DataTextField="securitylevel" 
                    DataValueField="securitylevelID" Height="22px" Width="200px">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT [securitylevelID], [securitylevel] FROM [SecurityLevels]">
                </asp:SqlDataSource>
                </td>
                <td>&nbsp;</td>
           	</tr>
           	<tr>
           	   <td class="TablaField">Nombre:</td> <td> 
                <asp:TextBox ID="txtNombre" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                </td>
                <td>
                 <script type="text/javascript">
<!--
function validaPassword(objSource, objArgs)
{
// Get value.
var number = objArgs.Value;
// Check value and return result.
if (number.length >= 6)
{
objArgs.IsValid = true;
}
else
{
objArgs.IsValid = false;
}
}
// -->
</script>                  </td>
           	</tr>
           	<tr>
           	<td class="TablaField">
           	
           	    Activo:</td>
           	<td>
           	
                <asp:CheckBox ID="chkActivo" runat="server" />
                &nbsp;</td>
           	    <td>
                    &nbsp;</td>
           	</tr>
           	<tr>
           	<td class="TablaField">
           	
           	    E-mail :</td>
           	<td>
           	
                <asp:TextBox ID="txtemail" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                &nbsp;</td>
           	    <td>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtemail" 
                        ErrorMessage="Escriba una Dirección de Correo válida" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
           	</tr>
           	   <tr>
                   <td>
                   </td>
                   <td>
                       <asp:Button ID="btnAgregarDeForm" runat="server" CssClass="Button" 
                           onclick="btnAgregarDeForm_Click" Text="Agregar" Width="100px" />
                       <asp:Button ID="btnModificarDeForm" runat="server" CssClass="Button" 
                           onclick="btnModificarDeForm_Click" Text="Modificar" Width="100px" />
                       <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                           CssClass="Button" onclick="btnCancelar_Click" Text="Cancelar" Width="100px" />
                   </td>
               </tr>
           	</table>
         </asp:Panel>
</asp:Content>

