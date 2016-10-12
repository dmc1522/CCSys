<%@ Page Language="C#" Title="Ajustes de Cheques" AutoEventWireup="true" CodeBehind="frmAjustesCheques.aspx.cs" Inherits="Garibay.frmAjustesCheques" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table >
            <tr>
                <td>
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
                                <td style="text-align: center">
                                    <asp:Button ID="btnAceptardetails" runat="server" CssClass="Button" 
                                         Text="Aceptar" Width="135px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                 <asp:Panel ID="paneldatos" runat="Server">
                    <table >
                        <tr>
                            <td class="TableHeader" align="center" colspan="2">
                                AJUSTES DE IMPRESIÓN DE CHEQUES</td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Cuenta de Banco:</td>
                            <td>
                                <asp:DropDownList ID="cmbBanco" runat="server" AutoPostBack="True" 
                                    DataSourceID="SqlDataSource1" DataTextField="cuenta" DataValueField="cuentaID" 
                                    Height="23px" onselectedindexchanged="cmbBanco_SelectedIndexChanged" 
                                    Width="600px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    
                                    SelectCommand="SELECT Bancos.nombre + '  ' + CuentasDeBanco.NumeroDeCuenta + ' - ' + CuentasDeBanco.Titular AS cuenta, CuentasDeBanco.cuentaID, CuentasDeBanco.bancoID FROM Bancos INNER JOIN CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID ORDER BY cuenta">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                     
                        <tr>
                        <td ColSpan="2">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="campoChequeID,cuentaID" DataSourceID="SqlDataSource2">
                                <Columns>
                                    <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                                    <asp:BoundField DataField="campo" HeaderText="campo" ReadOnly="True" 
                                        SortExpression="campo" />
                                    <asp:BoundField DataField="campoChequeID" HeaderText="campoChequeID" 
                                        ReadOnly="True" SortExpression="campoChequeID" Visible="False" />
                                    <asp:BoundField DataField="cuentaID" HeaderText="cuentaID" 
                                        SortExpression="cuentaID" Visible="False" />
                                    <asp:BoundField DataField="posX" HeaderText="posX" SortExpression="posX" />
                                    <asp:BoundField DataField="posY" HeaderText="posY" SortExpression="posY" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                SelectCommand="SELECT CamposCheque.campo, PosCamposCheque.campoChequeID, PosCamposCheque.cuentaID, PosCamposCheque.posX, PosCamposCheque.posY FROM CamposCheque INNER JOIN PosCamposCheque ON CamposCheque.campoChequeID = PosCamposCheque.campoChequeID WHERE PosCamposCheque.cuentaID=@cuentaID" 
                                UpdateCommand="UPDATE [PosCamposCheque] SET [posX]=@posX, [posY] = @posY WHERE [cuentaID] = @cuentaID and [campoChequeID] = @campoChequeID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="cmbBanco" DefaultValue="0" Name="cuentaID" 
                                        PropertyName="SelectedValue" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="posX" />
                                    <asp:Parameter Name="posY" />
                                    <asp:Parameter Name="cuentaID" />
                                    <asp:Parameter Name="campoChequeID" />
                                </UpdateParameters>
                            </asp:SqlDataSource>
                        </td>
                        </tr>
                        
                        <tr>
                            <td align="center" colspan="2">
                            <asp:TextBox ID="txtPosY" runat="server" Visible="False"></asp:TextBox>
                            <asp:TextBox ID="txtPosX" runat="server" Visible="False"></asp:TextBox>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
                        onclick="btnGuardar_Click" style="height: 26px" Visible="False" />
                    <asp:Button ID="btnSalir" runat="server" Text="Salir" Height="26px" Width="57px" 
                                    onclick="btnSalir_Click" Visible="False" />
                            </td>
                        </tr>
                    </table>
                  </asp:Panel>
                </td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>


