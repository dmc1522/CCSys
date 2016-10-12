<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="True" CodeBehind="frmAddModifyPrestamo.aspx.cs" Inherits="Garibay.frmAddModifyPrestamo" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
	<tr>
		<td align="center" class="TableHeader">
            <asp:Label ID="lblTitle" runat="server" Text="AGREGAR PRESTAMO"></asp:Label>
        </td>
	</tr>
	<tr>
	    <td>
                                                                    
                                                                        <div runat="Server" id="divAgregarNuevoPago">
                                                                        <table>
                                                                            <tr>
                                                                                <td class="TablaField"Field">Ciclo:</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlCiclos" runat="server" DataSourceID="sqlCiclos" 
                                                                                        DataTextField="CicloName" DataValueField="cicloID" AutoPostBack="True" 
                                                                                        onselectedindexchanged="ddlCiclos_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="sqlCiclos" runat="server" 
                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                        
                                                                                        SelectCommand="SELECT cicloID, CicloName FROM Ciclos WHERE (cerrado = 0) ORDER BY fechaInicio DESC">
                                                                                    </asp:SqlDataSource>
                                                                                </td>
                                                                                
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    Crédito:</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlCredito" runat="server" DataSourceID="sqlCredito" 
                                                                                        DataTextField="nompro" DataValueField="creditoID" Height="24px" 
                                                                                        Width="292px" onselectedindexchanged="ddlCredito_SelectedIndexChanged" 
                                                                                        AutoPostBack="True">
                                                                                    </asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="sqlCredito" runat="server" 
                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                        
                                                                                        
                                                                                        
                                                                                        SelectCommand="SELECT Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre + ' - ' + CAST(Creditos.creditoID AS varchar) AS nompro, Creditos.creditoID, Creditos.cicloID FROM Productores INNER JOIN Creditos ON Productores.productorID = Creditos.productorID WHERE (Creditos.cicloID = @cicloID) ORDER BY nompro">
                                                                                        <SelectParameters>
                                                                                            <asp:ControlParameter ControlID="ddlCiclos" Name="cicloID" 
                                                                                                PropertyName="SelectedValue" />
                                                                                        </SelectParameters>
                                                                                    </asp:SqlDataSource>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtIdtoMod" runat="server" Visible="False"></asp:TextBox>
                                                                                    <br />
                                                                                    <asp:TextBox ID="txtMovBan" runat="server" Visible="False"></asp:TextBox>
                                                                                    <br />
                                                                                    <asp:TextBox ID="txtMovCaj" runat="server" Visible="False"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">Interés anual:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtInteresAnual" runat="server" Width="80px">18</asp:TextBox>
                                                                                    %</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">Interés moratorio:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtInteresMoratorio" runat="server" Width="80px">18</asp:TextBox>
                                                                                    %</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">Fecha límite de pago:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtFechaLimite" runat="server" ReadOnly="True"></asp:TextBox>
                                                                                    <rjs:PopCalendar ID="PopCalendar8" runat="server" Control="txtFechaLimite" 
                                                                                        Separator="/" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    Fecha:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtFechaNPago" runat="server" ReadOnly="True"></asp:TextBox>
                                                                                    <rjs:PopCalendar ID="PopCalendar7" runat="server"  Control="txtFechaNPago" 
                                                                                        Separator="/" 
                                                                                         />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RequiredFieldValidator ID="valFecha0" runat="server" 
                                                                                        ControlToValidate="txtFechaNPago" 
                                                                                        ErrorMessage="El campo fecha es necesario"></asp:RequiredFieldValidator>
                                                                                </td>
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
                                                                                    <asp:TextBox ID="txtMonto" runat="server" Width="266px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RequiredFieldValidator ID="valMontorequired1" runat="server" 
                                                                                        ControlToValidate="txtMonto" Display="Dynamic" 
                                                                                        ErrorMessage="El campo monto es necesario"></asp:RequiredFieldValidator>
                                                                                    <br />
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                                                                        ControlToValidate="txtMonto" Display="Dynamic" 
                                                                                        ErrorMessage="Escriba una cantida válida" 
                                                                                        ValidationExpression="\d+(.\d*)?"></asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:CheckBox ID="chkCarteraVencida" runat="server" Text="Es cartera vencida" />
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:Panel ID="pnlMensaje" runat="server" >
                                                                                        <asp:Image ID="imgBien" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                                                                                        <asp:Image ID="imgMal" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                                                                                        <asp:Label ID="lblNewPresResult" runat="server"></asp:Label>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:UpdateProgress ID="UpProgPagos" runat="server" 
                                                                                        AssociatedUpdatePanelID="UpdateAddNewPago" DisplayAfter="0">
                                                                                        <ProgressTemplate>
                                                                                            <asp:Image ID="Image5" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                                                                                            Procesando informacion de prestamo...
                                                                                        </ProgressTemplate>
                                                                                    </asp:UpdateProgress>
                                                                                    <asp:Button ID="btnAddPrestamo" runat="server" 
                                                                                        Text="Agregar Prestamo" onclick="btnAddPrestamo_Click" 
                                                                                        CausesValidation="False" />
                                                                                    <asp:Button ID="btnUpdatePres" runat="server" Text="Modificar Prestamo" 
                                                                                        onclick="btnUpdatePres_Click" />
                                                                                    <asp:HyperLink ID="linkImpCheque" runat="server" Visible="False">[linkImpCheque]</asp:HyperLink>
																						<asp:HyperLink ID="lnkImprimirPagare" runat="server" Visible="False">Imprimir Pagare</asp:HyperLink>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                <asp:UpdatePanel ID="UpdateAddNewPago" runat="Server">
                                                                                <ContentTemplate>
                                                                                
                                                                                <table>
                                                                                	<tr>
                                                                                		<td class="TablaField">Monto Entregado:</td><td><asp:Label ID="lblMontoEntregado" runat="server"></asp:Label></td>
                                                                                	</tr>
                                                                                	<tr>
                                                                                		<td class="TablaField">Monto Faltante a Entregar:</td><td><asp:Label ID="lblMontoRestante" runat="server"></asp:Label></td>
                                                                                	</tr>
                                                                                </table>
                                                                                <table>
                                                                                	<tr>
                                                                                		<td colspan="2">
                                                                                            <asp:GridView ID="gvPagosFactura" runat="server" AutoGenerateColumns="False" 
                                                                                                DataKeyNames="movimientoID,movbanID,numCheque,fechaMov,cargo" 
                                                                                                DataSourceID="sdsPagosFactura" ondatabound="gvPagosFactura_DataBound" 
                                                                                                onrowdeleting="gvPagosFactura_RowDeleting">
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="fechaMov" DataFormatString="{0:dd/MM/yyyy}" 
                                                                                                        HeaderText="Fecha" SortExpression="fechaMov" />
                                                                                                    <asp:BoundField DataField="Concepto" DataFormatString="{0:c2}" 
                                                                                                        HeaderText="Concepto" SortExpression="Concepto" />
                                                                                                    <asp:BoundField DataField="cargo" DataFormatString="{0:c2}" HeaderText="Cargo" 
                                                                                                        SortExpression="cargo">
                                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="numCheque" HeaderText="# cheque" 
                                                                                                        SortExpression="numCheque">
                                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                                    </asp:BoundField>
                                                                                                    <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" 
                                                                                                        SortExpression="Cuenta" />
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Button ID="Button1" runat="server" 
                                                                                                                onclientclick='<%# GetPrintChequeURL(Eval("movbanID").ToString()) %>' 
                                                                                                                Text="Imprimir" 
                                                                                                                Visible='<%# IsChequeVisible(Eval("numCheque").ToString()) %>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:CommandField ButtonType="Button" DeleteText="Eliminar" 
                                                                                                        ShowDeleteButton="True" />
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                            <asp:SqlDataSource ID="sdsPagosFactura" runat="server" 
                                                                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                DeleteCommand="select 1;" 
                                                                                                SelectCommand="SELECT ISNULL(MovimientosCuentasBanco.fecha, MovimientosCaja.fecha) AS fechaMov, ISNULL(MovimientosCuentasBanco.cargo, MovimientosCaja.cargo) AS cargo, MovimientosCuentasBanco.abono, MovimientosCuentasBanco.numCheque, ISNULL(ConceptosMovCuentas.Concepto, 'EFECTIVO') AS Concepto, Anticipos_Movimientos.movimientoID, Anticipos_Movimientos.movbanID, Bancos.nombre + ' - ' + CuentasDeBanco.NumeroDeCuenta + ' - ' + CuentasDeBanco.Titular AS Cuenta FROM CuentasDeBanco INNER JOIN MovimientosCuentasBanco INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco.cuentaID INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID RIGHT OUTER JOIN MovimientosCaja RIGHT OUTER JOIN Anticipos_Movimientos ON MovimientosCaja.movimientoID = Anticipos_Movimientos.movimientoID ON MovimientosCuentasBanco.movbanID = Anticipos_Movimientos.movbanID WHERE (Anticipos_Movimientos.anticipoID = @anticipoID)">
                                                                                                <SelectParameters>
                                                                                                    <asp:ControlParameter ControlID="txtIdtoMod" Name="anticipoID" 
                                                                                                        PropertyName="Text" />
                                                                                                </SelectParameters>
                                                                                            </asp:SqlDataSource>
                                                                                        </td>
                                                                                	</tr>
                                                                                </table>
                                                                                <asp:CheckBox ID="chkAddNewPago" runat="server" Visible="False" 
                                                                                        Text="Mostrar panel para agregar pago" />
                                                                                <asp:Panel runat="Server" id="pnlAddPagos" Visible="False">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td class="TablaField">
                                                                                            Tipo de pago:</td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="cmbTipodeMovPago" runat="server" Height="22px" 
                                                                                                Width="249px">
                                                                                                <asp:ListItem Value="0">EFECTIVO</asp:ListItem>
                                                                                                <asp:ListItem Value="1">MOVIMIENTO DE BANCO</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="TablaField">
                                                                                            Fecha movimiento:</td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtFechaPago" runat="server"></asp:TextBox>
                                                                                            <rjs:PopCalendar ID="PopCalendarPagos" runat="server" Separator="/" Control="txtFechaPago" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                    <div id="divPagoMovCaja" runat="Server">
                                                                                        <table>
                                                                                        	<tr>
                                                                                        		<td class="TablaField">Monto Efectivo:</td>
                                                                                        		<td>
                                                                                                    <asp:TextBox ID="txtMontoEfectivo" runat="server"></asp:TextBox>
                                                                                                </td>
                                                                                        	</tr>
                                                                                            <tr>
                                                                                                <td class="TablaField">
                                                                                                    El prestamo se hará de la caja:</td>
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
                                                                                                    <asp:DropDownList ID="drpdlSubcatalogoCajaChica" runat="server" DataSourceID="sdsSubcatalogoCajaChica" 
                                                                                                        DataTextField="subCatalogo" DataValueField="subCatalogoMovBancoID" 
                                                                                                        Height="23px" Width="258px">
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
                                                                                            <tr>
                                                                                                <td class="TablaField">
                                                                                                    Monto Movimiento:</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtMontoMovimiento" runat="server"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TablaField">
                                                                                                    Concepto:</td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="cmbConceptomovBancoPago" runat="server" 
                                                                                                        DataSourceID="sdsConceptoPago" DataTextField="Concepto" 
                                                                                                        DataValueField="ConceptoMovCuentaID" Height="22px" Width="249px">
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
                                                                                                    <asp:DropDownList ID="drpdlSubcatalogofiscalPago" runat="server" DataSourceID="sdsSubcatalogofiscalPago" 
                                                                                                        DataTextField="subCatalogo" DataValueField="subCatalogoMovBancoID" 
                                                                                                        Height="23px" Width="258px">
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
                                                                                                                    <asp:CheckBox ID="chkCobradoFecha" runat="server" 
                                                                                                                        Text="Marcar cheque como cobrado en la fecha:" />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Panel ID="pnlFechaCobrado" runat="server">
                                                                                                                        <asp:TextBox ID="txtFechaCheque" runat="server" ReadOnly="True"></asp:TextBox>
                                                                                                                        <rjs:PopCalendar ID="PopCalendar9" runat="server" Control="txtFechaCheque" 
                                                                                                                            Separator="/" />
                                                                                                                    </asp:Panel>
                                                                                                                    <cc1:CollapsiblePanelExtender ID="pnlFechaCobrado_CollapsiblePanelExtender" 
                                                                                                                        runat="server" CollapseControlID="chkCobradoFecha" Collapsed="True" 
                                                                                                                        Enabled="True" ExpandControlID="chkCobradoFecha" 
                                                                                                                        TargetControlID="pnlFechaCobrado">
                                                                                                                    </cc1:CollapsiblePanelExtender>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="TablaField">
                                                                                                                    # Factura o Larguillo:</td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtFacturaLarguillo" runat="server"></asp:TextBox>
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
                                                                                                                        DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID" Height="23px" 
                                                                                                                        Width="235px">
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
                                                                                                                        DataTextField="catalogoMovBanco" DataValueField="catalogoMovBancoID" 
                                                                                                                        Height="23px" Width="236px">
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
                                                                                                                    <asp:DropDownList ID="drpdlSubcatologointernaPago" runat="server" DataSourceID="sdsSubCatalogoInternaPago" 
                                                                                                                        DataTextField="subCatalogo" DataValueField="subCatalogoMovBancoID" 
                                                                                                                        Height="23px" Width="234px">
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
                                                                                                        </table>
                                                                                                    </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Panel ID="pnlAddMovMsg" runat="server" >
                                                                                                    <asp:Image ID="pnlAddMovMsgBien" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                                                                                                    <asp:Image ID="pnlAddMovMsgMal" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                                                                                                    <asp:Label ID="lblpnlAddMovMsgResult" runat="server" Font-Bold="True"></asp:Label>
                                                                                                </asp:Panel>
                                                                                                <asp:Button ID="btnAgregarMovimiento" runat="server" Text="Agregar Movimiento" 
                                                                                                    onclick="btnAgregarMovimiento_Click" CausesValidation="False" />
                                                                                                    
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                                    <cc1:CollapsiblePanelExtender ID="pnlAddPagos_CollapsiblePanelExtender" 
                                                                                        runat="server" Enabled="True" TargetControlID="pnlAddPagos">
                                                                                    </cc1:CollapsiblePanelExtender>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                                </td>
                                                                            </tr>
                                                                            
                                                                        </table>
                                                                        </div>
                                                                    </td>
	</tr>
</table>

</asp:Content>
