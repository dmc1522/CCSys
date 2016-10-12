<%@ Page Title="" Language="C#" theme="skinverde" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmMyData.aspx.cs" Inherits="Garibay.frmMyData" %>
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

    <asp:Panel ID="pnlmodificarPass" runat="server">
    <table>
        <tr>
    		<td class="TableHeader" colspan="2" align="center" >
    		MODIFICAR CONTRASEÑA
    		</td>
    	</tr>
    	<tr>
    		<td class="TablaField">
    		    Contraseña Actual:
    		</td>
    		<td>
    		    <asp:TextBox ID="txtpassword" runat="server" MaxLength="20" TextMode="Password" 
                    ValidationGroup="primero" Width="200px"></asp:TextBox>
            </td>
            <td>
    		
    		    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtpassword" 
                    ErrorMessage="El Campo de Contraseña Actual es Necesario" 
                    ValidationGroup="primero"></asp:RequiredFieldValidator>
    		
    		</td>
    	</tr>
    	<tr>
            <td class="TablaField">
                Nueva Contraseña:
            </td>
            <td>
                <asp:TextBox ID="txtNewPassword" runat="server" MaxLength="20" 
                    TextMode="Password" Width="200px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtNewPassword" 
                    ErrorMessage="El campo Nueva Contraseña es Necesario" ValidationGroup="primero"></asp:RequiredFieldValidator>
                <br />
                <asp:CustomValidator ID="CustomValidator1" runat="server" 
                    ClientValidationFunction="validaPassword" ControlToValidate="txtNewPassword" 
                    ErrorMessage="La Contraseña Debe Ser Mayor de 6 Caracteres" 
                    ValidateEmptyText="True" ValidationGroup="primero"></asp:CustomValidator>
            </td>
        </tr>
    	<tr>
    		<td class="TablaField">
    		Confirmar Contraseña:
    		</td>
    		<td>
    		<asp:TextBox ID="txtconfirmPass" runat="server" TextMode="Password" Width="200px" 
                     MaxLength="20"></asp:TextBox>
            </td>
            <td>
    		
    		    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToValidate="txtconfirmPass" ErrorMessage="La Contraseña No Concuerda" 
                    ValidationGroup="primero" ControlToCompare="txtNewPassword"></asp:CompareValidator>
    		
    		</td>
    		
    	</tr>
    	<tr>
    		
    	    <td align="center" colspan="2">
                <asp:Button ID="btnAceptarPass" runat="server" Text="Aceptar" 
                    onclick="btnAceptarPass_Click" ValidationGroup="primero" />
            </td>
    		
    	</tr>
    </table>
    </asp:Panel>
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
</script>
    <asp:Panel ID="pnlDatosUsers" runat="server">
    <table>
           	<tr>
           		<td align="center" colspan="2" class="TableHeader">
                    <asp:Label ID="lblUsuarios" runat="server" Text="DATOS DE USUARIO"></asp:Label>
                </td>
           	</tr>
           	<tr>
           	   <td class="TablaField">Nombre:</td> <td> 
                <asp:TextBox ID="txtNombre" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                </td>
                <td>
                           </td>
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
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        ValidationGroup="segundo"></asp:RegularExpressionValidator>
                </td>
           	</tr>
           	   <tr>
                   
                   <td colspan="2" align="center">
                       <asp:Button ID="btnAceptarDatos" runat="server" Text="Aceptar" Width="100px" 
                           ValidationGroup="segundo" onclick="btnAceptarDatos_Click" />
                   </td>
               </tr>
           	</table>

        </asp:Panel>

</asp:Content>
