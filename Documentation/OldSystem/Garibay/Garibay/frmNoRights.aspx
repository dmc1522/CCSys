<%@ Page Language="C#" Theme="skinverde" AutoEventWireup="true" CodeBehind="frmNoRights.aspx.cs" Inherits="Garibay.frmNoRights" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>No Rights</title>
    
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body style ="basicStyle.css">
    <br /><br /><br /><br /><br /><br /><br /><br />
    <form id="form1" runat="server">
    <div id="div1" runat="Server" class="centrado">
    
        <table id="tabla1" class="style1" runat="Server">
            <tr>
                <td class="TableHeader">
                    <asp:Label ID="lblMsjLogout" runat="server" 
                        Text="No tienes derecho de entrar a esta página"></asp:Label>
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
