<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRecoverPassword.aspx.cs" Inherits="Garibay.frmRecoverPassword" Title="Recordar Contraseña" Theme="skinverde" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
  
    <title></title>
</head>
<body >
<div class="centrado">
    <form id="form1" runat="server">
    <table align="center">
    	<tr style="border: medium double #FFFFFF">
            
            <td colspan="2" align="center" style="border: medium double #FFFFFF">
                <asp:Image ID="Image1" runat="server" 
                    ImageUrl="~/imagenes/IPROJALmedium.jpg" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlenviaemail" runat="server">
    
    <table align="center" >
        <tr style="border: medium double #FFFFFF">
            <td colspan="2" align="center" style="border: medium double #FFFFFF" 
                class="TableHeader">
                <asp:Label ID="lbltitulo" runat="server" 
                    Text="GENERAR NUEVA CONTRASEÑA" Font-Bold="True"></asp:Label>
            
 
            </td>
        </tr>
        
        <tr style="border: medium double #FFFFFF">
            <td align="center" style="border: medium double #FFFFFF" colspan="2">
        
        <asp:Label ID="Label1" runat="server" Text="Ingrese su Dirección de Correo Electrónico"></asp:Label>
            </td>
        </tr>
         <tr style="border: medium double #FFFFFF">
            <td align="center" style="border: medium double #FFFFFF" class="TablaField">
        
                <asp:Label ID="lbltitulo2" runat="server" Text="E-Mail"></asp:Label>
            </td>
             <td style="border: medium double #FFFFFF">
                 <asp:TextBox ID="txtemail" runat="server" Width="306px"></asp:TextBox>
             </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:RegularExpressionValidator ID="REVemail" runat="server" 
                    ControlToValidate="txtemail" ErrorMessage="Ingresa Un Correo Válido" 
                    Font-Bold="True" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RFVemail" runat="server" 
                    ControlToValidate="txtemail" ErrorMessage="El Campo Del E-mail es Necesario" 
                    Font-Bold="True" ></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="border: medium double #FFFFFF">
            <td align="center" colspan="2" style="border: medium double #FFFFFF">
                <asp:Button ID="btnEnviar" runat="server" onclick="btnEnviar_Click" 
                    Text="Enviar Datos de Acceso" />
            </td>
        </tr>
    </table>
    
    
    </asp:Panel>
 
    
    <asp:Panel ID="pnlMensaje" runat="server">
    
    <div>
    <table align="center" style="border: medium double #FFFFFF">
        <tr style="border: medium double #FFFFFF">
            
            <td align="center" style="border: medium double #FFFFFF" class="TableHeader">
                <asp:Label ID="lblMensaje" runat="server" 
                    Text="" Font-Bold="True" 
                    ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr style="border: medium double #FFFFFF">
            
            <td align="center" style="border: medium double #FFFFFF">
                <asp:Button ID="btnIntentarDeNuevo" runat="server" Text="Intentar de Nuevo" 
                    onclick="btnIntentarDeNuevo_Click" />
                    <asp:Button ID="btnredirec" runat="server" Text="Ir a la Página Principal" 
                    onclick="btnredirec_Click" />
            </td>
        </tr>
        
    </table>
    </div>
    
    </asp:Panel>
        </form>
     </div>
</body>
</html>
