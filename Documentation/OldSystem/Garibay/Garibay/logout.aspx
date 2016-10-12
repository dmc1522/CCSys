<%@ Page Language="C#" Theme="skinverde" AutoEventWireup="true" CodeBehind="logout.aspx.cs" Inherits="Garibay.logout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Logout</title>
    
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body style ="basicStyle.css" onload = "delay()">
    <script language = "Javascript">
        function delay()
        {
            window.setTimeout("redirecciona()", 10000);
        }

        function redirecciona()
        
        {
       
           window.top.location.href = 'default.aspx'
          
        }

      
    </script>

    <br /><br /><br /><br /><br /><br /><br /><br />
    <form id="form1" runat="server">
    <div id="div1" runat="Server" class="centrado">
    
        <table id="tabla1" class="style1" runat="Server">
            <tr>
                <td class="TableHeader">
                    <asp:Label ID="lblMsjLogout" runat="server" 
                        Text="¡Su sesión se ha cerrado exitosamente! Espere unos segundos mientras se redirecciona a la página principal"></asp:Label>
                    <br />
                    <br />
                    Si no funciona el redireccionamiento automático utilice el siguiente link<br />
                    <asp:LinkButton ID="linkDefault" runat="server" PostBackUrl="~/Default.aspx">Volver 
                    a página principal</asp:LinkButton>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
