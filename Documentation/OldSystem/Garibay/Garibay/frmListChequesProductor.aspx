<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListChequesProductor.aspx.cs" Inherits="Garibay.frmListChequesProductor" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
	<tr>
		<td class="TableHeader">LISTA DE CHEQUES DE PRODUCTOR</td>
	</tr>
	<tr>
	    <td>
			<asp:TextBox ID="txtCobAnterior" runat="server" Visible="false"></asp:TextBox>
	        <asp:TextBox ID="txtTipoPago" runat="server" Visible="False"></asp:TextBox>
	        
	    
            <asp:GridView ID="grdvCheques" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="chequeRecibidoID,numcheque,ANombreDe,monto" DataSourceID="sqlCheques" 
                onrowdatabound="grdvCheques_RowDataBound" 
                onrowdeleting="grdvCheques_RowDeleting" 
                onselectedindexchanged="grdvCheques_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="numcheque" HeaderText="# Cheque" 
                        SortExpression="numcheque" />
                    <asp:BoundField DataField="ANombreDe" HeaderText="A Nombre De" 
                        SortExpression="ANombreDe" />
                    <asp:BoundField DataField="nombre" HeaderText="Banco" 
                        SortExpression="nombre" />
                    <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" 
						DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="monto" HeaderText="Monto" SortExpression="monto" 
						DataFormatString="{0:C2}" />
                    <asp:BoundField DataField="fechacobrado" HeaderText="Fecha Cobrado" 
                        SortExpression="fechacobrado" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:CheckBoxField DataField="cobrado" HeaderText="Cobrado" 
                        SortExpression="cobrado" />
                    <asp:TemplateField HeaderText="Eliminar" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDel" runat="server" CausesValidation="False" 
                                CommandName="Delete" Text="Eliminar"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="chequeRecibidoID" HeaderText="chequeRecibidoID" 
                        ReadOnly="True" SortExpression="chequeRecibidoID" Visible="False" />
                    <asp:BoundField DataField="userID" HeaderText="userID" 
                        SortExpression="userID" Visible="False" />
                	<asp:TemplateField>
						<EditItemTemplate>
							<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
						</EditItemTemplate>
						<ItemTemplate>
							<asp:HyperLink ID="HyperLink1" runat="server" 
								NavigateUrl='<%# GetURLOpen(Eval("chequeRecibidoID").ToString()) %>'>Abrir</asp:HyperLink>
						</ItemTemplate>
					</asp:TemplateField>
                </Columns>
            </asp:GridView>
	    
	    </td>
	</tr>
	<tr>
	    <td>
            <asp:TextBox ID="txtIdToModify" runat="server" Visible="False"></asp:TextBox>
		</td>
	</tr>
	<tr>
	    <td>
            <asp:SqlDataSource ID="sqlCheques" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                DeleteCommand="DELETE FROM ChequesRecibidos WHERE (chequeRecibidoID = @chequeRecibidoID );" 
                SelectCommand="SELECT ChequesRecibidos.numcheque, ChequesRecibidos.ANombreDe, ChequesRecibidos.fecha, ChequesRecibidos.monto, ChequesRecibidos.fechacobrado, ChequesRecibidos.cobrado, ChequesRecibidos.chequeRecibidoID, ChequesRecibidos.userID, Bancos.nombre FROM ChequesRecibidos INNER JOIN Bancos ON ChequesRecibidos.bancoID = Bancos.bancoID">
                <DeleteParameters>
                    <asp:Parameter Name="chequeRecibidoID" />
                </DeleteParameters>
            </asp:SqlDataSource>
        </td>
	</tr>
	<tr>
	    <td>
            <asp:Panel ID="pnlModCheque" runat="server" Visible="False">
            <table>
                <tr>
                    <td colspan="2">MODIFICAR DATOS DE CHEQUE</td>
                </tr>
            	<tr>
            		<td># de cheque: </td>
            		<td>
                        <asp:TextBox ID="txtNumCheq" runat="server"></asp:TextBox>
                    </td>
            		<td></td>
            	</tr>
            	<tr>
            		<td>Origen </td>
            		<td>
						<asp:DropDownList ID="ddlBancoOrigen" runat="server" 
							DataSourceID="SqlBancosOrigen" DataTextField="nombre" DataValueField="bancoID">
						</asp:DropDownList>
                    	<asp:SqlDataSource ID="SqlBancosOrigen" runat="server" 
							ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
							SelectCommand="SELECT [bancoID], [nombre] FROM [Bancos]">
						</asp:SqlDataSource>
                    </td>
            		<td></td>
            	</tr>
            	<tr>
            		<td>Fecha :</td>
            		<td>
                        <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
                        <rjs:PopCalendar ID="PopCalendar1" runat="server"  style="height: 16px" 
                        Separator="/" Control="txtFecha" />
                        <asp:TextBox ID="txtFechaCobrado" runat="server" Visible="False"></asp:TextBox>
                    </td>
            		<td></td>
            	</tr>
            	<tr>
            		<td>Nombre: </td>
            		<td>
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    </td>
            		<td></td>
            	</tr>
            	<tr>
            		<td>Monto: </td>
            		<td>
                        <asp:TextBox ID="txtMonto" runat="server"></asp:TextBox>
                    </td>
            		<td></td>
            	</tr>
            	<tr>
            		<td>Marcar como cobrado: </td>
            		<td>
                        <asp:CheckBox ID="chkCobrado" runat="server" />
                    </td>
            		<td></td>
            	</tr>
            	<tr>
            	<td>
            	
            	 <table >
                                                
                                            
                                                <tr>
                                                <td align="center">
                                                    
                                                    
                                                    </td>
                                                </tr>
                                                
                                                </table>
            	
            	
            	</td>
            	</tr>
            	<tr>
            	    <td colspan="2">
            	    <div id="divMovimientos" runat="Server">
                        <asp:Panel ID="pnlMovimiento" runat="server">
							<asp:RadioButton ID="rdbCaja" runat="server" Checked="true" GroupName="pagos" Text="Movimiento de Caja Chica" />
							<br />
							<asp:RadioButton ID="rdbBanco" runat="server" BorderStyle="NotSet" GroupName="pagos" Text="Movimiento de Banco" />
                        <div ID="divPagoMovCaja" runat="Server">
                                                                                                    <table>
                                                                                                    <tr>
                                                                                                            <td align="center" class="TableHeader" colspan="2">
                                                                                                                DATOS MOVIMIENTO DE CAJA CHICA</td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td class="TablaField">
                                                                                                                El pago se hará de la caja:</td>
                                                                                                            <td>
                                                                                                                <asp:DropDownList ID="ddlPagosBodegas" runat="server" 
                                                                                                                    DataSourceID="sdsPagosBodegas" DataTextField="bodega" DataValueField="bodegaID">
                                                                                                                </asp:DropDownList>
                                                                                                                <asp:SqlDataSource ID="sdsPagosBodegas" runat="server" 
                                                                                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                                    SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
                                                                                                                </asp:SqlDataSource>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td class="TablaField">
                                                                                                                Grupo de catálogos:
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:DropDownList ID="drpdlGrupoCatalogosCajaChica" runat="server" 
                                                                                                                    AutoPostBack="True" DataSourceID="sdsGruposCatalogosCajaChica" 
                                                                                                                    DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID" Height="23px" 
                                                                                                                    Width="257px">
                                                                                                                </asp:DropDownList>
                                                                                                                <asp:SqlDataSource ID="sdsGruposCatalogosCajaChica" runat="server" 
                                                                                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                                    SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos] ORDER BY [grupoCatalogo]">
                                                                                                                </asp:SqlDataSource>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td class="TablaField">
                                                                                                                Catálogo de cuenta:</td>
                                                                                                            <td>
                                                                                                                <asp:DropDownList ID="drpdlCatalogocuentaCajaChica" runat="server" 
                                                                                                                    AutoPostBack="True" DataSourceID="sdsCatalogoCuentaCajaChica" 
                                                                                                                    DataTextField="catalogoMovBanco" DataValueField="catalogoMovBancoID" 
                                                                                                                    Height="23px" Width="256px">
                                                                                                                </asp:DropDownList>
                                                                                                                <asp:SqlDataSource ID="sdsCatalogoCuentaCajaChica" runat="server" 
                                                                                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                                    SelectCommand="SELECT catalogoMovBancoID, catalogoMovBanco FROM catalogoMovimientosBancos WHERE (grupoCatalogoID = @grupoCatalogoID) ORDER BY catalogoMovBanco">
                                                                                                                    <SelectParameters>
                                                                                                                        <asp:ControlParameter ControlID="drpdlGrupoCatalogosCajaChica" 
                                                                                                                            DefaultValue="-1" Name="grupoCatalogoID" PropertyName="SelectedValue" />
                                                                                                                    </SelectParameters>
                                                                                                                </asp:SqlDataSource>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td class="TablaField">
                                                                                                                Subcatálogo de cuenta:</td>
                                                                                                            <td>
                                                                                                                <asp:DropDownList ID="drpdlSubcatalogoCajaChica" runat="server" 
                                                                                                                    DataSourceID="sdsSubcatalogoCajaChica" DataTextField="subCatalogo" 
                                                                                                                    DataValueField="subCatalogoMovBancoID" Height="23px" Width="258px">
                                                                                                                </asp:DropDownList>
                                                                                                                <asp:SqlDataSource ID="sdsSubcatalogoCajaChica" runat="server" 
                                                                                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                                    SelectCommand="SELECT SubCatalogoMovimientoBanco.subCatalogo, SubCatalogoMovimientoBanco.subCatalogoMovBancoID FROM SubCatalogoMovimientoBanco INNER JOIN catalogoMovimientosBancos ON SubCatalogoMovimientoBanco.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID WHERE (SubCatalogoMovimientoBanco.catalogoMovBancoID = @catalogoMovBancoID) ORDER BY SubCatalogoMovimientoBanco.subCatalogo">
                                                                                                                    <SelectParameters>
                                                                                                                        <asp:ControlParameter ControlID="drpdlCatalogocuentaCajaChica" 
                                                                                                                            DefaultValue="-1" Name="catalogoMovBancoID" PropertyName="SelectedValue" />
                                                                                                                    </SelectParameters>
                                                                                                                </asp:SqlDataSource>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </div>
																						 <div ID="divMovBanco" runat="Server">
                                                                                                    <table border="1">
                                                                                                        <tr>
                                                                                                            <td align="center" class="TableHeader" colspan="2">
                                                                                                                DATOS MOVIMIENTO DE BANCO</td>
                                                                                                        </tr>
                                                                                                       <%-- sdsd--%>
                                                                                                       
                                                                                                        <tr>
                                                                                                            <td class="TablaField">
                                                                                                                Cuenta:</td>
                                                                                                            <td>
                                                                                                                <asp:DropDownList ID="cmbCuentaPago" runat="server" 
                                                                                                                    DataSourceID="sdsCuentaPago" DataTextField="cuenta" DataValueField="cuentaID" 
                                                                                                                    Height="22px" Width="427px">
                                                                                                                </asp:DropDownList>
                                                                                                                <asp:SqlDataSource ID="sdsCuentaPago" runat="server" 
                                                                                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                                    SelectCommand="SELECT Bancos.nombre + '  ' + CuentasDeBanco.NumeroDeCuenta + ' - ' + CuentasDeBanco.Titular AS cuenta, CuentasDeBanco.cuentaID FROM Bancos INNER JOIN CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID ORDER BY cuenta">
                                                                                                                </asp:SqlDataSource>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td class="TablaField">
                                                                                                                Grupo de catálogos de cuenta fiscal:
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:DropDownList ID="drpdlGrupoCuentaFiscal" runat="server" 
                                                                                                                    AutoPostBack="True" DataSourceID="sdsGruposCatalogosfiscalPago" 
                                                                                                                    DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID" Height="23px" 
                                                                                                                    Width="257px">
                                                                                                                </asp:DropDownList>
                                                                                                                <asp:SqlDataSource ID="sdsGruposCatalogosfiscalPago" runat="server" 
                                                                                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                                    SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos]">
                                                                                                                </asp:SqlDataSource>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td class="TablaField">
                                                                                                                Catálogo de cuenta fiscal:</td>
                                                                                                            <td>
                                                                                                                <asp:DropDownList ID="drpdlCatalogocuentafiscalPago" runat="server" 
                                                                                                                    AutoPostBack="True" DataSourceID="sdsCatalogoCuentaFiscal" 
                                                                                                                    DataTextField="catalogoMovBanco" DataValueField="catalogoMovBancoID" 
                                                                                                                    Height="23px" Width="256px">
                                                                                                                </asp:DropDownList>
                                                                                                                <asp:SqlDataSource ID="sdsCatalogoCuentaFiscal" runat="server" 
                                                                                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                                    SelectCommand="SELECT catalogoMovBancoID, catalogoMovBanco FROM catalogoMovimientosBancos WHERE (grupoCatalogoID = @grupoCatalogoID)">
                                                                                                                    <SelectParameters>
                                                                                                                        <asp:ControlParameter ControlID="drpdlGrupoCuentaFiscal" DefaultValue="-1" 
                                                                                                                            Name="grupoCatalogoID" PropertyName="SelectedValue" />
                                                                                                                    </SelectParameters>
                                                                                                                </asp:SqlDataSource>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td class="TablaField">
                                                                                                                Subcatálogo de cuenta fiscal:</td>
                                                                                                            <td>
                                                                                                                <asp:DropDownList ID="drpdlSubcatalogofiscalPago" runat="server" 
                                                                                                                    DataSourceID="sdsSubcatalogofiscalPago" DataTextField="subCatalogo" 
                                                                                                                    DataValueField="subCatalogoMovBancoID" Height="23px" Width="258px">
                                                                                                                </asp:DropDownList>
                                                                                                                <asp:SqlDataSource ID="sdsSubcatalogofiscalPago" runat="server" 
                                                                                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                                    SelectCommand="SELECT SubCatalogoMovimientoBanco.subCatalogo, SubCatalogoMovimientoBanco.subCatalogoMovBancoID FROM SubCatalogoMovimientoBanco INNER JOIN catalogoMovimientosBancos ON SubCatalogoMovimientoBanco.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID WHERE (SubCatalogoMovimientoBanco.catalogoMovBancoID = @catalogoMovBancoID)">
                                                                                                                    <SelectParameters>
                                                                                                                        <asp:ControlParameter ControlID="drpdlCatalogocuentafiscalPago" 
                                                                                                                            DefaultValue="-1" Name="catalogoMovBancoID" PropertyName="SelectedValue" />
                                                                                                                    </SelectParameters>
                                                                                                                </asp:SqlDataSource>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        
                                                                                                        
                                                                                                        
                                                                                                    </table>
                                                                                                </div>
                        </asp:Panel>
                        
                        </div>
                           <%-- <cc1:CollapsiblePanelExtender ID="pnlAddProd_CollapsiblePanelExtender" 
                                                            runat="server" CollapseControlID="chkCobrado" Collapsed="True" 
                                                            Enabled="True" ExpandControlID="chkCobrado" TargetControlID="pnlMovimiento">
                                                        </cc1:CollapsiblePanelExtender>--%>
                    </td>
            	</tr>
            	<tr>
            		<td>
						<asp:Button ID="btnModificarCheque" runat="server" Text="Modificar" 
							onclick="btnModificarCheque_Click" /></td>
            		<td>
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
                    </td>
            		<td></td>
            	</tr>
            	
            	
            </table>
            </asp:Panel>
        </td>
	</tr>
	<tr>
	<td>
	 <asp:Panel ID="panelMensaje" runat="server">
               <table >
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
                        Text=""></asp:Label>
                </td>
            </tr>
                   
                   
               </table>
           </asp:Panel>

	</td>
	</tr>
</table>
</asp:Content>
