<%@ Page Language="C#" Theme="skinrojo" AutoEventWireup="True" CodeBehind="frmCuentasdeBanco.aspx.cs" Inherits="Garibay.frmCuentasdeBanco" Title="Cuentas de Banco"  MasterPageFile="~/MasterPage.Master"%>

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
        </table>
</asp:Panel>


    <asp:Panel ID="panelLista" runat="server" Width="592px">
    
        <table >
            <tr>
                <td class="TableHeader">
                    CUENTAS DE BANCO</td>
            </tr>
            <tr>
                <td style="text-align: left">
                               </td>
            </tr>
            <tr>
                <td>

        
        
   
   
     
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnAgregardelaLista" runat="server"
                                        Text="Agregar" CausesValidation="False" 
                                        onclick="btnAgregardelaLista_Click" />
                                    <asp:Button ID="btnModificardelaLista" runat="server"
                                        Text="Modificar" CausesValidation="False" onclick="btnModificardelaLista_Click" />
                                    <asp:Button ID="btnEliminardelaLista" runat="server" CausesValidation="False" 
                                        Text="Eliminar" onclick="btnEliminardelaLista_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gridCuentasdeBanco" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                                        ForeColor="Black" GridLines="None" DataSourceID="SqlDataSource1" 
                                        onselectedindexchanged="gridCuentasdeBanco_SelectedIndexChanged" 
                                        
										
                                        
                                        DataKeyNames="cuentaID,bancoID,nombre,NumeroDeCuenta,Titular,CLABE,numchequeinicio,numchequefin,Moneda,tipomonedaID" 
                                        PageSize="50">
                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                        <HeaderStyle CssClass="TableHeader" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                        <Columns>
                                            <asp:CommandField ButtonType="Button" SelectText="&gt;" 
												ShowSelectButton="True" />
                                            <asp:BoundField HeaderText="cuentaID" DataField="cuentaID" 
                                                InsertVisible="False" ReadOnly="True" SortExpression="cuentaID" 
                                                Visible="False" >
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Moneda" HeaderText="Moneda" 
                                                SortExpression="Moneda" />
                                            <asp:BoundField HeaderText="Numero De Cuenta" DataField="NumeroDeCuenta" 
                                                SortExpression="NumeroDeCuenta" >
                                            	<ControlStyle Width="300px" />
												<ItemStyle Width="250px" />
											</asp:BoundField>
                                            <asp:BoundField HeaderText="Banco" DataField="nombre" 
                                                SortExpression="nombre" >
                                            
                                            	<ControlStyle Width="300px" />
												<ItemStyle Width="250px" />
											</asp:BoundField>
                                            
                                            <asp:BoundField DataField="Titular" HeaderText="Titular" 
                                                SortExpression="Titular" >
                                            
                                            	<ControlStyle Width="500px" />
												<ItemStyle Width="500px" />
											</asp:BoundField>
                                            
                                            <asp:BoundField DataField="bancoID" HeaderText="bancoID" ReadOnly="True" 
                                                SortExpression="bancoID" Visible="False" />
                                            
                                            <asp:BoundField DataField="CLABE" HeaderText="CLABE" SortExpression="CLABE" >
                                            
                                        		<ControlStyle Width="300px" />
												<ItemStyle Width="120px" />
											</asp:BoundField>
											<asp:BoundField DataField="numchequeinicio" HeaderText="# Cheque Inicio" 
												SortExpression="numchequeinicio" />
											<asp:BoundField DataField="numchequefin" HeaderText="# Cheque Fin" 
												SortExpression="numchequefin" />
                                            
                                            <asp:BoundField DataField="tipomonedaID" HeaderText="tipomonedaID" 
                                                ReadOnly="True" SortExpression="tipomonedaID" Visible="False" />
                                            
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        </td>
                        </tr>
                      </table>
         </asp:Panel>
        <asp:Panel ID="Panelagregar" runat="server" Width="624px">
          
             <table>
                        <tr>
                            <td class="TableHeader" colspan="2">
                                <asp:Label ID="lblHeader" runat="server" Text="AGREGAR NUEVA CUENTA DE BANCO"></asp:Label>
                            </td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Banco:</td>
                            <td>
                                <asp:DropDownList ID="cmbBanco" runat="server" Height="18px" Width="176px" 
                                    DataSourceID="SqlDataSource2" DataTextField="nombre" DataValueField="bancoID">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
                                    SelectCommand="SELECT * FROM [Bancos]"></asp:SqlDataSource>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Número de la cuenta:</td>
                            <td>
                                <asp:TextBox ID="txtNumerodecuenta" runat="server" Width="176px" MaxLength="20"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="valNumdecuenta" runat="server" 
                                    ErrorMessage="El campo número de cuenta es necesario" 
                                    ControlToValidate="txtNumerodecuenta"></asp:RequiredFieldValidator>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Titular de la cuenta</td>
                            <td>
                                <asp:TextBox ID="txtTitular" runat="server" Height="22px" Width="176px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="valTitulardecuenta" runat="server" 
                                    ControlToValidate="txtTitular"
                                    ErrorMessage="El campo titular de la cuenta es necesario"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                CLABE:</td>
                            <td>
                                <asp:TextBox ID="txtCLABE" runat="server" Height="22px" MaxLength="50" 
                                    Width="176px"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Número de Cheque Inicial:</td>
                            <td>
                                <asp:TextBox ID="txtInicio" runat="server" Height="22px" MaxLength="50" 
                                    Width="176px"></asp:TextBox>
                            </td>
                            <td>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="EL campo de Numero de Cheque Inicial es Necesario" ControlToValidate="txtInicio"></asp:RequiredFieldValidator>  
								</td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Número de Cheque Final:</td>
                            <td>
                                <asp:TextBox ID="txtFin" runat="server" Height="22px" MaxLength="50" 
                                    Width="176px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="EL campo de Numero de Cheque Final es Necesario" ControlToValidate="txtFin"></asp:RequiredFieldValidator>  </td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Tipo de moneda:</td>
                            <td>
                                <asp:DropDownList ID="drpdlTipodeMoneda" runat="server" 
                                    DataSourceID="sdsTipoDeMoneda" DataTextField="Moneda" 
                                    DataValueField="tipomonedaID">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsTipoDeMoneda" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT [tipomonedaID], [Moneda] FROM [TiposDeMoneda]">
                                </asp:SqlDataSource>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                PERMITIR TRASPASOS CON CUENTAS:</td>
                            <td colspan="2" nowrap="nowrap">
                                <asp:CheckBoxList ID="cblCuentasPermitidas" runat="server" 
                                    DataSourceID="sdsCuentasBancos" DataTextField="Cuenta" 
                                    DataValueField="cuentaID">
                                </asp:CheckBoxList>
                                <asp:SqlDataSource ID="sdsCuentasBancos" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    
                                    SelectCommand="SELECT CuentasDeBanco.cuentaID, Bancos.nombre + ' ' + CuentasDeBanco.NumeroDeCuenta + ' ' + CuentasDeBanco.Titular AS Cuenta FROM CuentasDeBanco INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID WHERE (CuentasDeBanco.cuentaID &lt;&gt; @cuentaID) ORDER BY Cuenta">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtCuentaSelectedID" Name="cuentaID" 
                                            PropertyName="Text" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <asp:TextBox ID="txtCuentaSelectedID" runat="server" Visible="False" 
                                    Width="1px">-1</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:Button ID="btnAgregardeabajo" runat="server"
                                    Text="Agregar" onclick="btnAgregardeabajo_Click" />
                                <asp:Button ID="btnModificardeAbajo" runat="server"
                                    Text="Modificar" onclick="btnModificardeAbajo_Click" />
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" 
                                    onclick="btnCancel_Click" Text="Cancelar" />
                            </td>
                            <td style="text-align: center">
                                &nbsp;</td>
                        </tr>
                        </table>
       </asp:Panel>
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
        
        
        
        
        
		
        SelectCommand="SELECT CuentasDeBanco.cuentaID, Bancos.nombre, CuentasDeBanco.NumeroDeCuenta, CuentasDeBanco.Titular, Bancos.bancoID, CuentasDeBanco.CLABE, CuentasDeBanco.numchequeinicio, CuentasDeBanco.numchequefin, TiposDeMoneda.Moneda, TiposDeMoneda.tipomonedaID FROM CuentasDeBanco INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID INNER JOIN TiposDeMoneda ON CuentasDeBanco.tipomonedaID = TiposDeMoneda.tipomonedaID ORDER BY Bancos.nombre, CuentasDeBanco.NumeroDeCuenta, CuentasDeBanco.Titular"></asp:SqlDataSource>
    
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    </asp:Content>
