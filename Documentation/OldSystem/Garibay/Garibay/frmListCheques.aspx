<%@ Page Language="C#"  MasterPageFile="~/MasterPage.Master" Title="Lista de Cheques" Theme="skinverde" AutoEventWireup="true" CodeBehind="frmListCheques.aspx.cs" Inherits="Garibay.frmListadeCheques" %>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar.Net.2008" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

 <asp:Panel ID="panelLista" runat="server" Width="100%" >
<table >
        <tr>
         <td class="TableHeader">
                LISTA DE CHEQUES
  

             </td>
        </tr>
        <tr>
         <td >

                <table>
                	<tr>
                		<td colspan="5" class="TableHeader"> FILTROS</td>
                	</tr>
                	<tr><td class="TablaField">CUENTA:</td><td colspan="4">
                        <asp:DropDownList ID="ddlCuentasDeBanco" runat="server" 
                            DataSourceID="sdsCuentas" DataTextField="Cuenta" DataValueField="cuentaID" 
                            Height="23px" Width="288px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsCuentas" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            SelectCommand="SELECT Bancos.nombre + ' - ' + CuentasDeBanco.NumeroDeCuenta AS Cuenta, CuentasDeBanco.cuentaID FROM Bancos INNER JOIN CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID ORDER BY Cuenta">
                        </asp:SqlDataSource>
                        </td></tr>
                    <tr>
                        <td class="TablaField">
                            FECHA:&nbsp; </td>
                        <td>
                            De:&nbsp; </td>
                        <td>
                            <asp:TextBox ID="txtFechaDe" runat="server"></asp:TextBox>
                            <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtFechaDe" 
                                Separator="/" />
                        </td>
                        <td>
                            A:</td>
                        <td>
                            <asp:TextBox ID="txtFechaA" runat="server"></asp:TextBox>
                            <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtFechaA" 
                                Separator="/" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField">
                            ESTADO DEL CHEQUE:</td>
                        <td colspan="4">
                            <asp:DropDownList ID="cmbStatus" runat="server" Height="23px" Width="184px">
                                <asp:ListItem Value="-1">TODOS LOS ESTADOS</asp:ListItem>
                                <asp:ListItem Value="0">COBRADOS</asp:ListItem>
                                <asp:ListItem Value="1">NO COBRADOS</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="TablaField">
                            CAMPOS A MOSTRAR:</td>
                        <td colspan="4">
                            <asp:CheckBoxList ID="cblCampos" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>Fecha de última actualización</asp:ListItem>
                                <asp:ListItem>#Usuario</asp:ListItem>
                                <asp:ListItem>Fecha de ingreso</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" 
                    onclick="btnFiltrar_Click" />
                <asp:Button ID="btnPrintList" runat="server" 
                    style="text-align: center; height: 26px;" Text="Imprimir lista" 
                    onclick="btnPrintList_Click" />
                &nbsp;<asp:Button ID="btnAjustesImpresion" runat="server" 
                    Text="Ajustes de Impresión de cheque" />
            </td>
        </tr>
        <tr>
         <td >

                <asp:GridView ID="gridCheques" ViewState="true"
                 runat="server" AutoGenerateColumns="False"  
                 DataSourceID="SqlDataSource1" 
                 onselectedindexchanged="gridCheques_SelectedIndexChanged" AllowPaging="True" 
                    PageSize="100" DataKeyNames="movbanID" 
                    onrowcancelingedit="gridCheques_RowCancelingEdit" 
                    onrowediting="gridCheques_RowEditing" 
                    onrowupdating="gridCheques_RowUpdating" 
                    onrowcommand="gridCheques_RowCommand" >
                <Columns>
                    <asp:CommandField ButtonType="Button" CancelText="Cancelar" 
                        EditText="Modificar" SelectText="&gt;" ShowEditButton="True" 
                        UpdateText="Actualizar">
                        <ControlStyle Font-Size="X-Small" />
                    </asp:CommandField>
                    <asp:BoundField DataField="movbanID" HeaderText="movbanID" 
                        InsertVisible="False" SortExpression="movbanID" 
                        Visible="False" />

                
               <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" 
                        DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" ReadOnly="True" 
                        SortExpression="nombre" />
                    <asp:BoundField DataField="facturaOlarguillo" 
                        HeaderText="# Factura o larguillo" ReadOnly="True" 
                        SortExpression="facturaOlarguillo">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="numCabezas" HeaderText="Número de cabezas" 
                        ReadOnly="True" SortExpression="numCabezas">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Concepto" HeaderText="Concepto" ReadOnly="True" 
                        SortExpression="Concepto" />
                    <asp:BoundField DataField="numCheque" HeaderText="# Cheque" 
                        SortExpression="chequenumero" ReadOnly="True" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cargo" HeaderText="Cargo" 
                        SortExpression="monto" DataFormatString="{0:c}" ReadOnly="True" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="abono" DataFormatString="{0:c}" HeaderText="Abono" 
                        ReadOnly="True" SortExpression="abono">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="chequeQuienrecibe" HeaderText="Quien Recibe" 
                        SortExpression="nombrequienrecibe" ReadOnly="True" />
                    <asp:CheckBoxField DataField="chequecobrado" HeaderText="Cobrado" 
                        SortExpression="chequecobrado" />
                    <asp:BoundField DataField="userID" HeaderText="# Usuario" 
                        SortExpression="userID" ReadOnly="True">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="storeTS" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" 
                        HeaderText="Fecha de ingreso" ReadOnly="True" SortExpression="storeTS" />
                    <asp:BoundField DataField="updateTS" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" 
                        HeaderText="Fecha de última actualización" ReadOnly="True" 
                        SortExpression="updateTS" />
                    <asp:BoundField DataField="fechacobrado" HeaderText="Fecha de cobro" 
                        SortExpression="fechacobrado" DataFormatString="{0:dd/MM/yyyy}" />
                   <asp:TemplateField>
                      <ItemTemplate>
                        <asp:Button ID="PrintButton" runat="server" 
                          CommandName="printCheque" 
                          CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                          Text="Imprimir" />
                      </ItemTemplate> 
                    </asp:TemplateField>
                    
                </Columns>
            </asp:GridView>
  



            </td>
        </tr>
        <tr>
         <td>
  

             <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                 ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
                 
                 
                 
                 
                 
                 
                 
                 SelectCommand="SELECT MovimientosCuentasBanco.fecha, MovimientosCuentasBanco.numCheque, MovimientosCuentasBanco.cargo, MovimientosCuentasBanco.chequeNombre, MovimientosCuentasBanco.chequeQuienrecibe, MovimientosCuentasBanco.nombre, MovimientosCuentasBanco.storeTS, MovimientosCuentasBanco.chequecobrado, MovimientosCuentasBanco.facturaOlarguillo, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.abono, MovimientosCuentasBanco.updateTS, MovimientosCuentasBanco.userID, MovimientosCuentasBanco.numCabezas, MovimientosCuentasBanco.fechacobrado, MovimientosCuentasBanco.movbanID FROM CuentasDeBanco INNER JOIN MovimientosCuentasBanco ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco.cuentaID INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID WHERE (CuentasDeBanco.cuentaID = @cuentaID) AND (MovimientosCuentasBanco.numCheque &lt;&gt; 0) ORDER BY MovimientosCuentasBanco.fecha DESC" 
                 UpdateCommand="UPDATE MovimientosCuentasBanco SET fechacobrado = @fechacobrado, chequecobrado = @chequecobrado WHERE (movbanID = @movbanID)">
                 <SelectParameters>
                     <asp:ControlParameter ControlID="ddlCuentasDeBanco" Name="cuentaID" 
                         PropertyName="SelectedValue" />
                 </SelectParameters>
                 <UpdateParameters>
                     <asp:Parameter Name="fechacobrado" />
                     <asp:Parameter Name="chequecobrado" />
                     <asp:Parameter Name="movbanID" />
                 </UpdateParameters>
             </asp:SqlDataSource>
    
  

             <table >
                 <tr>
                     <td class="TablaField">
                         Número total de cheques girados:</td>
                     <td>
                         <asp:Label ID="lblChequesGirados" runat="server" Text="lblChequesGirados"></asp:Label>
                     </td>
                     <td class="TablaField">
                         Monto en cheques girados:</td>
                     <td>
                         <asp:Label ID="lblTotalGirados" runat="server" Text="lblTotalGirados"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td class="TablaField">
                         Número total de cheques no cobrados:</td>
                     <td>
                         <asp:Label ID="lblChequesNoCobrados" runat="server" Text="lblChequesNoCobrados"></asp:Label>
                     </td>
                     <td class="TablaField">
                         Monto en cheques no cobrados:</td>
                     <td>
                         <asp:Label ID="lblTotalNoCobrados" runat="server" Text="lblTotalNoCobrados"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td class="TablaField">
                         Número total de cheques cobrados:</td>
                     <td>
                         <asp:Label ID="lblChequesCobrados" runat="server" Text="lblChequesCobrados"></asp:Label>
                     </td>
                     <td class="TablaField">
                         Monto en cheques cobrados:</td>
                     <td>
                         <asp:Label ID="lblTotalCobrados" runat="server" Text="lblTotalCobrados"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
             </table>
    
  

            </td>
        </tr>
        </table>
  </asp:Panel>

        

        <script language="JavaScript">

            function url() {

                hidden = window.open('/frmAjustesCheques.aspx', 'Ajustes de Impresión de Cheques', 'top=200,left=200,width=400,height=220,maximize=no,status=no,resizable=no,scrollbars=no');

            }

    </script>
            
        


   </asp:Content>
     

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .TableHeader
        {
            text-align: left;
        }
        </style>

</asp:Content>

   
