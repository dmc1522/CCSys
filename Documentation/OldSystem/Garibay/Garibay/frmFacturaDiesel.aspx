<%@ Page Title="Factura de Gasolinera" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="True" CodeBehind="frmFacturaDiesel.aspx.cs" Inherits="Garibay.Formulario_web13" %>

<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
	<tr>
		<td class="TableHeader">
		FACTURA DE GASOLINERA
		
		</td>
		<td>
		    <asp:HyperLink ID="HyperLink1" runat="server" 
                NavigateUrl="~/frmListaFacturasDiesel.aspx">Ver Lista de Facturas de Gasolinera</asp:HyperLink>
		
		</td>
	</tr>
	<tr>
		<td class="TablaField">
			
		<asp:Label ID="lblFoliotitle" runat="server" Text="Folio" Visible="False"></asp:Label>
			<asp:Label ID="lblFolio" runat="server" Text=""></asp:Label>
		</td>
		<td>
			
		    &nbsp;</td>
	</tr>
</table>

	<asp:Panel ID="pnlAgregar" runat="server">
	<table>
		<tr>
			<td class="tablaField">
			Fecha : 
			</td>
			<td>
				<asp:TextBox ID="txtFechaFactura" runat="server" ReadOnly="true"></asp:TextBox>
			<rjs:PopCalendar ID="PopCalendar1" runat="server" 
                         style="height: 16px" 
                        Separator="/" Control="txtFechaFactura" />
			</td>
		</tr>
		<tr>
		<td>
		No. Factura
		</td>
		<td>
			<asp:TextBox ID="txtNoFactura" runat="server"></asp:TextBox>
		</td>
		</tr>
				<tr>
		<td class="tablaField">
		    Monto:</td>
		<td>
			<asp:TextBox ID="txtMonto" runat="server"></asp:TextBox>
		</td>
		</tr>

		<tr>
            <td class="tablaField">
                Monto en tarjetas diesel :
            </td>
            <td align="right">
                <asp:Label ID="lblMonto" runat="server" Text="$ 0.00"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tablaField">
                Total Pagos:</td>
            <td align="right">
                <asp:Label ID="lblTotalPagos" runat="server" Text="$ 0.00"></asp:Label>
            </td>
        </tr>

		<tr>
		<td class="tablaField">
			Monto Restante:</td>
		    <td align="right">
                <asp:Label ID="lblMontoRestante" runat="server" Text="$ 0.00"></asp:Label>
            </td>
		</tr>
	    <tr>
            <td>
                <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" 
                    Text="NuevaFactura" />
                <asp:Button ID="btnSaveFactura" runat="server" onclick="btnSaveFactura_Click" 
                    Text="Guardar Datos De Factura" Visible="False" />
                <asp:TextBox ID="txtIdtoModify" runat="server" Visible="False"></asp:TextBox>
            </td>
        </tr>
	</table>
	</asp:Panel>
	<table border="0" cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td>
			    <asp:GridView ID="gvPagosFactura" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="movbanID" DataSourceID="sdsPagosFactura" 
                    onrowdeleting="gvPagosFactura_RowDeleting" 
                    ondatabound="gvPagosFactura_DataBound">
                    <Columns>
                        <asp:BoundField DataField="movbanID" HeaderText="movbanID" ReadOnly="True" 
                            SortExpression="movbanID" InsertVisible="False" Visible="False">
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaMov" DataFormatString="{0:dd/MM/yyyy}" 
                            HeaderText="Fecha" SortExpression="fechaMov" />
                        <asp:BoundField DataField="Concepto" HeaderText="Concepto" 
                            SortExpression="Concepto" DataFormatString="{0:c2}">
                        </asp:BoundField>
                        <asp:BoundField DataField="cargo" HeaderText="Cargo" 
                            SortExpression="cargo" DataFormatString="{0:c2}">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="abono" DataFormatString="{0:c2}" 
                            HeaderText="Abono" SortExpression="abono">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="numCheque" 
                            HeaderText="# cheque" SortExpression="numCheque">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" 
                                    onclientclick='<%# GetPrintChequeURL(Eval("movbanID").ToString()) %>' 
                                    Text="Imprimir" 
                                    Visible='<%# IsChequeVisible(Eval("numCheque").ToString()) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Button" ShowDeleteButton="True" 
                            DeleteText="Eliminar" />
                    </Columns>
                </asp:GridView>
                            <asp:SqlDataSource ID="sdsPagosFactura" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                DeleteCommand="select 1;" 
                                
                                
                                
                    SelectCommand="SELECT MovimientosCuentasBanco.fecha AS fechaMov, MovimientosCuentasBanco.cargo, MovimientosCuentasBanco.abono, MovimientosCuentasBanco.numCheque, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.movbanID FROM MovimientosCuentasBanco INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID INNER JOIN PagosFacturasDiesel ON MovimientosCuentasBanco.movbanID = PagosFacturasDiesel.movbanID WHERE (PagosFacturasDiesel.FacturaFolio = @FacturaFolio)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="lblFolio" Name="FacturaFolio" 
                                        PropertyName="Text" />
                                </SelectParameters>
                            </asp:SqlDataSource>
			</td>
		</tr>
	</table>
	<table>
		<tr>
			<td>
                <asp:CheckBox ID="chkAddNewPago" runat="server" Text="Agregar nuevo pago a Factura" CssClass="TablaField" />
            </td>
		</tr>
	</table>
    <asp:Panel ID="UpdateAddNewPago" runat="Server">
            <div ID="divAgregarNuevoPago" runat="Server">
                <table>
                    
                    <tr>
                        <td class="TablaField">
                            Fecha:</td>
                        <td>
                            <asp:TextBox ID="txtFechaNPago" runat="server" ReadOnly="True"></asp:TextBox>
                            <rjs:PopCalendar ID="PopCalendar6" runat="server" Control="txtFechaNPago" 
                                Separator="/" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td class="TablaField">
                            <asp:Label ID="lblNombre0" runat="server" Text="Nombre:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombrePago" runat="server" Width="266px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="TablaField">
                            Monto:</td>
                        <td>
                            <asp:TextBox ID="txtMontoPago" runat="server" Width="266px"></asp:TextBox>
                        </td>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField" colspan="3">
                            <div ID="divMovBanco" runat="Server">
                                <table border="1">
                                    <tr>
                                        <td align="center" class="TableHeader" colspan="2">
                                            DATOS MOVIMIENTO DE BANCO</td>
                                    </tr>
                                    <tr>
                                        <td class="TablaField">
                                            Concepto:</td>
                                        <td>
                                            <asp:DropDownList ID="cmbConceptomovBancoPago" runat="server" 
                                                DataSourceID="sdsConceptoPago" DataTextField="Concepto" 
                                                DataValueField="ConceptoMovCuentaID" Height="22px">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="sdsConceptoPago" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                SelectCommand="SELECT [ConceptoMovCuentaID], [Concepto] FROM [ConceptosMovCuentas]  Where ConceptoMovCuentaID NOT IN (4,5,6,7) ORDER BY [Concepto]">
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TablaField">
                                            Cuenta:</td>
                                        <td>
                                            <asp:DropDownList ID="cmbCuentaPago" runat="server" 
                                                DataSourceID="sdsCuentaPago" DataTextField="cuenta" DataValueField="cuentaID" 
                                                Height="22px">
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
                                                DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID">
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
                                                Height="23px">
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
                                                DataValueField="subCatalogoMovBancoID">
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
                                    <tr>
                                        <td class="TablaField" colspan="2">
                                            <div ID="divCheque" runat="server">
                                                <table border="1">
                                                    <tr>
                                                        <td align="center" class="TableHeader" colspan="2">
                                                            DATOS DE CHEQUE</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TablaField">
                                                            # Cheque (*):</td>
                                                        <td>
                                                            <asp:TextBox ID="txtChequeNum" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TablaField">
                                                            Nombre interno:</td>
                                                        <td>
                                                            <asp:TextBox ID="txtChequeNombre" runat="server" Width="282px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TablaField">
                                                            Grupo de catálogos de cuenta interna:</td>
                                                        <td>
                                                            <asp:DropDownList ID="drpdlGrupoCatalogosInternaPago" runat="server" 
                                                                AutoPostBack="True" DataSourceID="sdsGruposCatalogosInternaPago" 
                                                                DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID">
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="sdsGruposCatalogosInternaPago" runat="server" 
                                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos]">
                                                            </asp:SqlDataSource>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TablaField">
                                                            Catálogo de cuenta interna:</td>
                                                        <td>
                                                            <asp:DropDownList ID="drpdlCatalogoInternoPago" runat="server" 
                                                                AutoPostBack="True" DataSourceID="sdsCatalogoCuentaInternaPago" 
                                                                DataTextField="catalogoMovBanco" DataValueField="catalogoMovBancoID">
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="sdsCatalogoCuentaInternaPago" runat="server" 
                                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                SelectCommand="SELECT catalogoMovBancoID, catalogoMovBanco FROM catalogoMovimientosBancos WHERE (grupoCatalogoID = @grupoCatalogoID)">
                                                                <SelectParameters>
                                                                    <asp:ControlParameter ControlID="drpdlGrupoCatalogosInternaPago" 
                                                                        DefaultValue="-1" Name="grupoCatalogoID" PropertyName="SelectedValue" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TablaField">
                                                            Subcatálogo de cuenta interna:</td>
                                                        <td>
                                                            <asp:DropDownList ID="drpdlSubcatologointernaPago" runat="server" 
                                                                DataSourceID="sdsSubCatalogoInternaPago" DataTextField="subCatalogo" 
                                                                DataValueField="subCatalogoMovBancoID">
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="sdsSubCatalogoInternaPago" runat="server" 
                                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                SelectCommand="SELECT SubCatalogoMovimientoBanco.subCatalogo, SubCatalogoMovimientoBanco.subCatalogoMovBancoID FROM SubCatalogoMovimientoBanco INNER JOIN catalogoMovimientosBancos ON SubCatalogoMovimientoBanco.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID WHERE (SubCatalogoMovimientoBanco.catalogoMovBancoID = @catalogoMovBancoID)">
                                                                <SelectParameters>
                                                                    <asp:ControlParameter ControlID="drpdlCatalogoInternoPago" DefaultValue="-1" 
                                                                        Name="catalogoMovBancoID" PropertyName="SelectedValue" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Observaciones:</td>
                                                        <td>
                                                            <asp:TextBox ID="txtObserv" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnAddPago" runat="server" onclick="btnAddPago_Click" 
                                Text="Agregar Pago a la Factura" />
                            <asp:Panel ID="pnlNewPago" runat="server">
                                <asp:Image ID="imgBienPago" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                                <asp:Image ID="imgMalPago" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                                <asp:Label ID="lblNewPagoResult" runat="server"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
    </asp:Panel>
		<cc1:CollapsiblePanelExtender ID="UpdateAddNewPago_CollapsiblePanelExtender" 
        runat="server" CollapseControlID="chkAddNewPago" Collapsed="True" 
        Enabled="True" ExpandControlID="chkAddNewPago" 
        TargetControlID="UpdateAddNewPago">
    </cc1:CollapsiblePanelExtender>
		<asp:Panel ID="pnlTarjetas" runat="server" Visible="false">
	
	<table>
	<tr>
	<td class="TableHeader">
	TARJETAS DISPONIBLES
	</td>
	<td></td>
	<td class="TableHeader">
	TARJETAS AGREGADAS A FACTURA
	</td>
	</tr>
	<tr>
		<td>
			<asp:GridView ID="grdvNoRelacionadas" runat="server" 
				AutoGenerateColumns="False" DataKeyNames="folio" DataSourceID="SqlDataSource1">
				<Columns>
					
					<asp:TemplateField ShowHeader="False">
						<ItemTemplate>
							<asp:CheckBox ID="CheckBox1" runat="server" />
						</ItemTemplate>
					</asp:TemplateField>
					
					<asp:BoundField DataField="folio" HeaderText="Folio" ReadOnly="True" 
						SortExpression="folio" >
						<ItemStyle HorizontalAlign="Right" />
					</asp:BoundField>
					<asp:BoundField DataField="monto" HeaderText="Monto" SortExpression="monto" 
						DataFormatString="{0:c2}" >
						<ItemStyle HorizontalAlign="Right" />
					</asp:BoundField>
					<asp:BoundField DataField="litros" HeaderText="Litros" 
						SortExpression="litros" >
						<ItemStyle HorizontalAlign="Right" />
					</asp:BoundField>
				    <asp:BoundField DataField="PagoNotaVentaID" HeaderText="Nota de Venta" 
                        InsertVisible="False" SortExpression="PagoNotaVentaID">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
				</Columns>
			
			</asp:GridView>
		
			<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
				ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
				
				
                SelectCommand="SELECT TarjetasDiesel.folio, TarjetasDiesel.monto, TarjetasDiesel.litros, Pagos_NotaVenta.PagoNotaVentaID FROM TarjetasDiesel LEFT OUTER JOIN Pagos_NotaVenta ON TarjetasDiesel.folio = Pagos_NotaVenta.tarjetaDieselID WHERE (TarjetasDiesel.FacturaFolio IS NULL) OR (TarjetasDiesel.FacturaFolio = - 1)">
			</asp:SqlDataSource>
		
		</td>
		<td>
		<table>
			<tr>
				<td>
					<asp:Button ID="btnaddAFactura" runat="server" Text=">>" 
						onclick="btnaddAFactura_Click" />
					</td>
			</tr>
			<tr>
				<td>
				<asp:Button ID="btnRemovFromFactura" runat="server" Text="<<" 
						onclick="btnRemovFromFactura_Click" />
				</td>
			</tr>
		</table>
		</td>
		<td>
			<asp:GridView ID="grdvRelaionados" runat="server" AutoGenerateColumns="False" 
				DataKeyNames="folio" DataSourceID="SqlDataSource2">
				<Columns>
					<asp:TemplateField ShowHeader="False">
						<ItemTemplate>
							<asp:CheckBox ID="CheckBox2" runat="server" />
						</ItemTemplate>
					</asp:TemplateField>
					<asp:BoundField DataField="folio" HeaderText="Folio" ReadOnly="True" 
						SortExpression="folio" >
						<ItemStyle HorizontalAlign="Right" />
					</asp:BoundField>
					<asp:BoundField DataField="monto" HeaderText="Monto" SortExpression="monto" 
						DataFormatString="{0:c2}" >
						<ItemStyle HorizontalAlign="Right" />
					</asp:BoundField>
					<asp:BoundField DataField="litros" HeaderText="Litros" 
						SortExpression="litros" >
						<ItemStyle HorizontalAlign="Right" />
					</asp:BoundField>
				    <asp:BoundField DataField="PagoNotaVentaID" HeaderText="Nota de Venta" 
                        InsertVisible="False" SortExpression="PagoNotaVentaID">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
				</Columns>
			
			</asp:GridView>
			<asp:SqlDataSource ID="SqlDataSource2" runat="server" 
				ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
				
                SelectCommand="SELECT TarjetasDiesel.folio, TarjetasDiesel.monto, TarjetasDiesel.litros, Pagos_NotaVenta.PagoNotaVentaID FROM TarjetasDiesel LEFT OUTER JOIN Pagos_NotaVenta ON TarjetasDiesel.folio = Pagos_NotaVenta.tarjetaDieselID WHERE (TarjetasDiesel.FacturaFolio = @FacturaFolio)">
				<SelectParameters>
					<asp:ControlParameter ControlID="txtIdtoModify" DefaultValue="-1" 
						Name="FacturaFolio" PropertyName="Text" />
				</SelectParameters>
			</asp:SqlDataSource>
		</td>
		
		</tr>
	</table>	
	<table >
<tr>

		<td class="tablaField">
			&nbsp;</td>
			<td>
				&nbsp;</td>
	</tr>
	
</table>

</asp:Panel>		


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

</asp:Content>
