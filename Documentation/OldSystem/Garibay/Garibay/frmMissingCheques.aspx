<%@ Page Language="C#" Title="Cheques Faltantes" AutoEventWireup="true" CodeBehind="frmMissingCheques.aspx.cs" Inherits="Garibay.frmMissingCheques" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    </head>
<body>
    <form id="form2" runat="server">
    <div>
            
              <table >
            <tr>
                <td class="TableHeader">
                    CHEQUES FALTANTES</td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gridMissingCheques" runat="server" 
                        AutoGenerateColumns="False" AllowPaging="True" 
                        onpageindexchanging="gridMissingCheques_PageIndexChanging" PageSize="20">
                        <PagerSettings Position="TopAndBottom" />
                        <Columns>
                            <asp:BoundField DataField="chequenumero" HeaderText="Núm. Cheque" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>





