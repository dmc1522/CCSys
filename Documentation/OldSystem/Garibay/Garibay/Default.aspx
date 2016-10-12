<%@ Page Language="C#" theme="skinverde" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Garibay._Default" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>INTEGRADORA DE PRODUCTORES DE JALISCO SPR DE RL</title> 
    <style type="text/css">
        .TableHeader
        {
            text-align: center;
        }
        .style1
        {
            width: 100%;
        }
    </style>   
</head>
<body >
<script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>
    <form id="form2" runat="server">
    <div class="centrado" runat="Server" id="divcentrado">
    <asp:Panel ID="pnlLogin" runat="Server">
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
    	<tr>
    		<td align="center" valign="middle">
    		    <table >
                <tr>
                    <td>
                    <asp:Image ID="Image1" runat="server" 
                        ImageUrl="~/imagenes/IPROJALmedium.jpg" /></td>
                </tr>
                <tr>
                    <td>
                        <table class="style1">
                            <tr>
                                <td class="TableHeader" colspan="2" style="text-align: center">
                                    Bienvenido</td>
                            </tr>
                            <tr>
                                <td class="TablaField">
                    Usuario:</td>
                                <td>
                    <asp:TextBox ID="txtUsuario" runat="server" Width="274px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TablaField">
                    Contraseña:</td>
                                <td>
                    <asp:TextBox ID="txtContrasena0" runat="server" Width="273px" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:CheckBox ID="chkSistemaBanco" runat="server" 
                                        Text="Sistema de Control de Credito de Banco" Visible="False" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
              
                    <asp:Button ID="btnEntrar" runat="server" Text="ENTRAR" 
                        Width="151px" onclick="btnEntrar_Click" />
                    <asp:Button ID="btnSalir" runat="server" Text="SALIR" Width="153px" onclick="btnSalir_Click" />
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                                <td align="center">
                                    <asp:HyperLink ID="HyperLink1" runat="server" 
                                        NavigateUrl="~/frmRecoverPassword.aspx">¿Olvidaste tu Contraseña?</asp:HyperLink>
                                </td>
                </tr>
                </table>
    		</td>
    		<td align="center" valign="middle">
    		    &nbsp;</td>
    		<td align="center" valign="middle">
    		    <table >
                <tr>
                    <td>
                    <asp:Image ID="Image2" runat="server" 
                        ImageUrl="~/imagenes/LogoMargaritasMedium.jpg" /></td>
                </tr>
                <tr>
                    <td>
                        <table class="style1">
                            <tr>
                                <td class="TableHeader" colspan="2" style="text-align: center">
                                    Bienvenido</td>
                            </tr>
                            <tr>
                                <td class="TablaField">
                    Usuario:</td>
                                <td>
                    <asp:TextBox ID="txtUsuarioMargaritas" runat="server" Width="274px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TablaField">
                    Contraseña:</td>
                                <td>
                    <asp:TextBox ID="txtPassMargaritas" runat="server" Width="273px" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
              
                    <asp:Button ID="Button1" runat="server" Text="ENTRAR" 
                        Width="151px" onclick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" Text="SALIR" Width="153px" onclick="btnSalir_Click" />
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                                <td align="center">
                                    <asp:HyperLink ID="HyperLink2" runat="server" 
                                        NavigateUrl="~/frmRecoverPassword.aspx">¿Olvidaste tu Contraseña?</asp:HyperLink>
                                </td>
                </tr>
                </table>
    		</td>
    	</tr>
    </table>
        
                    <asp:Panel ID="panelMensaje" runat="server" Visible="False">
                       <table  width="100%">
                           <tr>
                               <td  style="text-align: center">
                                   <asp:Image ID="imagenmal" runat="server" ImageUrl="~/imagenes/tache.jpg" 
                                       Visible="True" />
                                   <asp:Label ID="lblMensaje" runat="server" SkinID="lblMensajeTitle"></asp:Label>
                               </td>
                           </tr>
                           <tr>
                               <td style="text-align: center">
                                   <asp:Label ID="lblLoginResult" runat="server" 
                                       SkinID="lblMensajeOperationresult"></asp:Label>
                               </td>
                           </tr>
                           <tr>
                            <td align="center">
                                <asp:Label ID="lblMsjIntentos" runat="server" SkinID="lblMensajeException"></asp:Label>
                               </td>
                           </tr>
                       </table>
                   </asp:Panel>
        
    </asp:Panel>
    <asp:Panel ID="pnlWelcome" runat="server">
        <table>
            <tr>
                <td><a href="frmAtGlance.aspx">Bienvenido</a></td>
            </tr>
        </table>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
