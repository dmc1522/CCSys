<%@ Page Language="C#" Theme="skinverde" AutoEventWireup="true"  Title="Seguridad" CodeBehind="frmUnauthorizedAccess.aspx.cs" Inherits="Garibay.frmUnauthorizedAccess" %>


<html>
<head runat="server">
    <title>Logout</title>
    
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body style ="basicStyle.css">
    <script language = "Javascript">
        function delay() {
            window.setTimeout("redirecciona()", 10000);
        }

        function redirecciona() {

            window.top.location.href = 'frmAtGlance.aspx'

        }

      
    </script>

    <br /><br /><br /><br /><br /><br /><br /><br />
    <form id="form1" runat="server">
    <div id="div1" runat="Server" class="centrado">
    
        <table id="tabla1" class="style1" runat="Server">
            <tr>
                <td class="TableHeader">
                    <asp:Label ID="lblMsjLogout" runat="server" 
                        
                        Text="¡Usted no tiene los derechos para visitar esta página! Espere unos segundos mientras se redirecciona a la página principal"></asp:Label>
                    <br />
                    <br />
                    Si no funciona el redireccionamiento automático utilice el siguiente link<br />
                    <asp:LinkButton ID="linkDefault" runat="server" 
                        PostBackUrl="~/frmAtGlance.aspx">Volver 
                    a página principal</asp:LinkButton>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

        

</asp:Content>
