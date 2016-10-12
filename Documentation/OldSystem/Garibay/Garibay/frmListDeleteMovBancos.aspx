<%@ Page Language="C#" Theme="skinrojo" AutoEventWireup="true" CodeBehind="frmListDeleteMovBancos.aspx.cs" Inherits="Garibay.frmListMovBancos" Title="Lista de movimientos de Bancos" MasterPageFile="~/MasterPage.Master" EnableEventValidation="false" %>

<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <asp:Panel ID="panelMensaje" runat="server" > 
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
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnAceptarMensaje" runat="server" CssClass="Button" 
                        Text="Aceptar" />
                </td>
            </tr>
        </table>
</asp:Panel>
<asp:Panel ID="panelagregar" runat="server" > 

         
        <table>
            <tr>
                <td class="TableHeader">
                    MOVIMIENTOS DE CUENTA DE BANCO</td>
            </tr>
            <tr>
                <td>
                <table >
                	<tr>
                		<td colspan="3" class="TableHeader">Filtros:</td>
                	</tr>
                	<tr>
                	<td class="TablaField">Cuenta:</td> <td colspan="2">
                        <asp:DropDownList ID="ddlCuentas" runat="server" DataSourceID="sdsComboCuentas" 
                            DataTextField="cuenta" DataValueField="cuentaID">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsComboCuentas" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            SelectCommand="SELECT CuentasDeBanco.cuentaID, Bancos.nombre + ' - ' + CuentasDeBanco.NumeroDeCuenta AS cuenta FROM Bancos INNER JOIN CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID ORDER BY cuenta">
                        </asp:SqlDataSource>
                    </td>
                	</tr>
                	<tr>
                	<td class="TablaField">Periodo:</td> <td>
                        Fecha inicio:
                        <asp:TextBox ID="txtFecha1" runat="server" ReadOnly="True"></asp:TextBox>
&nbsp;<rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtFecha1" Separator="/" />
                    </td>
                        <td>
                            Fecha fin:&nbsp;
                            <asp:TextBox ID="txtFecha2" runat="server" ReadOnly="True"></asp:TextBox>
                            <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtFecha2" 
                                Separator="/" />
                    </td>
                	</tr>
                	<tr>
                        <td class="TablaField">
                            Mostrar:</td>
                        <td colspan="2">
                            <asp:CheckBoxList ID="cblColToShow" runat="server" RepeatDirection="Horizontal" 
                                RepeatColumns="4">
                                <asp:ListItem>Nombre</asp:ListItem>
                                <asp:ListItem># de Factura o Larguillo</asp:ListItem>
                                <asp:ListItem># de Cabezas</asp:ListItem>
                                <asp:ListItem># de Cheque</asp:ListItem>
                                <asp:ListItem>Usuario</asp:ListItem>
                                <asp:ListItem>Fecha de ingreso</asp:ListItem>
                                <asp:ListItem>Ultima modificación</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                	<tr>
                	<td colspan="3">
                        <asp:Button ID="btnFiltrar" runat="server" onclick="Button1_Click" 
                            Text="Filtrar" />
                    <asp:Button ID="btnAgregarNuevo" runat="server" 
                        onclick="btnAgregarNuevo_Click" Text="Agregar" />

        
        

                        <asp:Button ID="btnEliminar" runat="server" onclick="btnEliminar_Click" 
                            Text="Eliminar" />

        
        

                        <asp:Button ID="btnImprimir" runat="server" onclick="btnImprimir_Click" 
                            Text="Exportar a Excel" />

        
        

                        </td> 
                	</tr>
                	<tr>
                	<td colspan="3">
                        &nbsp;</td> 
                	</tr>
                </table>
                <table>
                <table >
                	<tr>
                		<td colspan="1" class="TableHeader">Saldo inicial:</td>
                		<td style="text-align: center">
                            <asp:Label ID="lblSaldoinicial" runat="server" Text="Label" 
                                CssClass="TableField"></asp:Label>
                        &nbsp;
                        </td>
                		<td class="TableHeader">&nbsp;Saldo final</td>
                		<td class="TableField">
                            <asp:Label ID="lblSaldofinal" runat="server" Text="Label"></asp:Label>
                        </td>
                	</tr>
     
                	</tr>
                </table>
                
            <tr>
                <td>
                
                </td>
            </tr>
            <tr>
                <td>

        
        

    <asp:GridView ID="gridMovCuentasBanco" runat="server" AutoGenerateColumns="False" 
                 AllowPaging="True" ForeColor="Black" GridLines="None" 
                 CellPadding="4" DataSourceID="SqlDataSource1" 
                        onselectedindexchanged="gridMovCuentasBanco_SelectedIndexChanged" 
                        DataKeyNames="movbanID,fecha,abono,cargo" PageSize="100" >
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <HeaderStyle CssClass="TableHeader" />
        <AlternatingRowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <Columns>
                    <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                        ShowSelectButton="True" />
                    <asp:BoundField HeaderText="movbanID" DataField="movbanID" 
                        InsertVisible="False" ReadOnly="True" SortExpression="movbanID" 
                        ItemStyle-HorizontalAlign="Right" Visible="False" 
                        ConvertEmptyStringToNull="False" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Fecha" DataField="fecha" 
                        SortExpression="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                        ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" 
                        SortExpression="nombre" />
                    <asp:BoundField DataField="facturaOlarguillo" 
                        HeaderText="# de Factura o Larguillo" SortExpression="facturaOlarguillo" />
                    <asp:BoundField DataField="numCabezas" HeaderText="# de Cabezas" />
                    <asp:BoundField DataField="concepto" HeaderText="Concepto" 
                        SortExpression="concepto" />
                    <asp:BoundField DataField="numCheque" HeaderText="# de Cheque" />
                    <asp:BoundField DataField="cargo" DataFormatString="{0:c}" HeaderText="Cargo" 
                        ItemStyle-HorizontalAlign="Center" SortExpression="cargo">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="abono" DataFormatString="{0:c}" HeaderText="Abono" 
                        SortExpression="abono" />
                    <asp:BoundField DataField="saldo" DataFormatString="{0:c}" HeaderText="Saldo" 
                        SortExpression="saldo" />
                    <asp:BoundField HeaderText="Usuario" DataField="userID" SortExpression="Nombre"></asp:BoundField>
                    <asp:BoundField DataField="storeTS" HeaderText="Fecha de ingreso" 
                        SortExpression="storeTS" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" >
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Ultima modificación" DataField="updateTS" >
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:BoundField>
                </Columns>
    </asp:GridView>

                        
        

                </td>
            </tr>
            <tr>
                <td>

        
        

                    &nbsp;</td>
            </tr>
    </table>


        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
            
        
        
        SelectCommand="SELECT MovimientosCuentasBanco.movbanID, CuentasDeBanco.NumeroDeCuenta, tipomovimientoscuenta.tipomovimiento, MovimientosCuentasBanco.fecha, MovimientosCuentasBanco.monto, Users.Nombre, MovimientosCuentasBanco.storeTS, MovimientosCuentasBanco.updateTS, Cheques.chequenumero, Cheques.chequeID FROM CuentasDeBanco INNER JOIN MovimientosCuentasBanco ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco.cuentaID INNER JOIN tipomovimientoscuenta ON MovimientosCuentasBanco.tipomovcuentaID = tipomovimientoscuenta.tipomovcuentaID INNER JOIN Users ON MovimientosCuentasBanco.userID = Users.userID LEFT OUTER JOIN Cheques ON MovimientosCuentasBanco.chequeID = Cheques.chequeID">
        </asp:SqlDataSource>
</asp:Panel>

</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

   
 

    <style type="text/css">
        .TablaField
        {
            text-align: center;
        }
    </style>

   
 

</asp:Content>
