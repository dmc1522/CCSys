<%@ Page Language="C#" Theme="skinrojo"   MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmPagosCreditosAdd.aspx.cs" Inherits="Garibay.frmPagosCreditosAdd"  Title="PAGOS A CREDITOS"%>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

         <script type="text/javascript" src="/scripts/divFunctions.js"></script>
         <script type="text/javascript" src="/scripts/prototype.js"></script>
        <table>
            <tr>
                <td colspan="2" class="TableHeader">
                    AGREGAR UN PAGO A UN CRÉDITO</td>
            </tr>
            <tr>
                <td class="TablaField">
                                        CICLO:</td>
                <td>
                    <asp:DropDownList ID="cmbCiclo" runat="server" DataSourceID="sdsCiclos" 
                        DataTextField="CicloName" DataValueField="cicloID" Height="23px" 
						Width="181px" AutoPostBack="True">
                    </asp:DropDownList>
                    
                    <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
						SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos]  WHERE cerrado=0 ORDER BY [fechaInicio] DESC">
                    </asp:SqlDataSource>
          		  <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Crédito:</td>
                <td>
                    <asp:DropDownList ID="ddlCredito" runat="server"  
                                DataSourceID="SqlCreditos" DataTextField="Credito" 
                                DataValueField="creditoID" AutoPostBack="True" 
						onselectedindexchanged="ddlCredito_SelectedIndexChanged">
                           </asp:DropDownList>
   		 	   		    <asp:SqlDataSource ID="SqlCreditos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            
                            
                            
						
                        SelectCommand="SELECT DISTINCT LTRIM(Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre + ' - ' + CAST(Creditos.creditoID AS Varchar)) AS Credito, Creditos.creditoID FROM Creditos INNER JOIN Productores ON Creditos.productorID = Productores.productorID ORDER BY Credito">
                        </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
            <td colspan="2">
            <div ID="divAgregarNuevoPago" runat="Server">                                                                                 
                                                                                    <table>
                                                                                    <tr>
                                                                                        <td class="TablaField">
                                                                                            Fecha:
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtFechaNPago" runat="server" ReadOnly="True"></asp:TextBox>
                                                                                            <rjs:PopCalendar ID="PopCalendar6" runat="server" Control="txtFechaNPago" 
                                                                                                Separator="/" /> 
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RequiredFieldValidator ID="valFecha0" runat="server" 
                                                                                                ControlToValidate="txtFechaNPago" ErrorMessage="El campo fecha es necesario"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="TablaField">
                                                                                            Tipo de pago:</td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="cmbTipodeMovPago" runat="server" Height="22px" 
                                                                                                Width="249px">
                                                                                                <asp:ListItem Value="0">EFECTIVO</asp:ListItem>
                                                                                                <asp:ListItem Value="1">CHEQUE</asp:ListItem>
                                                                                                <asp:ListItem Value="2">TARJETA DIESEL</asp:ListItem>
                                                                                                <asp:ListItem Value="3">BOLETA</asp:ListItem>
                                                                                                <asp:ListItem Value="4">TRANSFERENCIA</asp:ListItem>
                                                                                                <asp:ListItem Value="5">DEPOSITO</asp:ListItem>
                                                                                            </asp:DropDownList>
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
                                                                                            <asp:TextBox ID="txtMonto" runat="server" Width="266px">1</asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RequiredFieldValidator ID="valMontorequired1" runat="server" 
                                                                                                ControlToValidate="txtMonto" Display="Dynamic" 
                                                                                                ErrorMessage="El campo monto es necesario"></asp:RequiredFieldValidator>
                                                                                            <br />
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                                                                                ControlToValidate="txtMonto" Display="Dynamic" 
                                                                                                ErrorMessage="Escriba una cantida válida" ValidationExpression="\d+(.\d*)?"></asp:RegularExpressionValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    </table>
                                                                                    </div>
				 <div ID="divDiesel" runat="Server">
                                                                                         
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td class="TablaField">
                                                                                                                Folio:</td>
                                                                                                            <td>
                                                                                                                <asp:TextBox ID="txtfoliodiesel" runat="server"></asp:TextBox>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:CompareValidator ID="valFolio" runat="server" ControlToValidate="txtfoliodiesel" 
                                                                                                                    ErrorMessage="Escriba un folio válido" Operator="DataTypeCheck" 
                                                                                                                    Type="Integer"></asp:CompareValidator>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                 
                                                                                                        <tr>
                                                                                                            <td class="TablaField">
                                                                                                                Litros:</td>
                                                                                                            <td>
                                                                                                                <asp:TextBox ID="txtLitrosTarjetaDiesel" runat="server" ></asp:TextBox>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:CompareValidator ID="valLitrosTarjDiesel" runat="server" 
                                                                                                                    ControlToValidate="txtLitrosTarjetaDiesel" 
                                                                                                                    ErrorMessage="Escriba una cantidad válida" Operator="DataTypeCheck" 
                                                                                                                    Type="Double"></asp:CompareValidator>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td colspan="2">
                                                                                                                &nbsp;</td>
                                                                                                            <td>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                         
                                                                                                
                                                                                            </div>
				 <div ID="divPagoMovCaja" runat="Server">
                                                                                                <table>
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
                                                                                                                Width="257px" 
                                                                                                                onselectedindexchanged="drpdlGrupoCatalogosCajaChica_SelectedIndexChanged">
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
                                                                                                                Height="23px" Width="256px" 
                                                                                                                onselectedindexchanged="drpdlCatalogocuentaCajaChica_SelectedIndexChanged">
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
                                                                                                                DataValueField="subCatalogoMovBancoID" Height="23px" Width="258px" 
                                                                                                                onselectedindexchanged="drpdlSubcatalogoCajaChica_SelectedIndexChanged">
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
                                                                                                                Width="257px" 
                                                                                                                onselectedindexchanged="drpdlGrupoCuentaFiscal_SelectedIndexChanged">
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
                                                                                                                Height="23px" Width="256px" 
                                                                                                                onselectedindexchanged="drpdlCatalogocuentafiscalPago_SelectedIndexChanged">
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
                                                                                                                DataValueField="subCatalogoMovBancoID" Height="23px" Width="258px" 
                                                                                                                onselectedindexchanged="drpdlSubcatalogofiscalPago_SelectedIndexChanged">
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
                                                                                                                                Width="235px" 
                                                                                                                                onselectedindexchanged="drpdlGrupoCatalogosInternaPago_SelectedIndexChanged">
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
                                                                                                                            <asp:DropDownList ID="drpdlSubcatologointernaPago" runat="server" 
                                                                                                                                DataSourceID="sdsSubCatalogoInternaPago" DataTextField="subCatalogo" 
                                                                                                                                DataValueField="subCatalogoMovBancoID" Height="23px" Width="234px">
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
				 <div ID="divboletas" runat="Server">
											             <table>
											             <tr>
                                            <td class="TablaField" align="right">
                                                PRODUCTOR:</td>
                                            <td>
                                            <br />
                                                <asp:DropDownList ID="ddlNewBoletaProductor" runat="server" 
                                                    DataSourceID="sdsNewBoletaProductor" DataTextField="Productor" 
                                                    DataValueField="productorID" Height="23px" Width="211px">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender ID="ddlNewBoletaProductor_ListSearchExtender" 
                                                    runat="server" Enabled="True" PromptText="Escriba para buscar" 
                                                    TargetControlID="ddlNewBoletaProductor">
                                                </cc1:ListSearchExtender>
                                                <asp:SqlDataSource ID="sdsNewBoletaProductor" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                    SelectCommand="SELECT productorID, apaterno + ' ' + amaterno + ' ' + nombre AS Productor FROM Productores ORDER BY Productor">
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                # BOLETO
                                                <br />
                                                DE BASCULA:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewNumBoleta" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                # DE FOLIO:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewTicket" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                PRODUCTO:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlNewBoletaProducto" runat="server" 
                                                    DataSourceID="sdsNewBoletaProductos" DataTextField="Expr1" 
                                                    DataValueField="productoID">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsNewBoletaProductos" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                    
													SelectCommand="SELECT Productos.productoID, Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Expr1 FROM Productos INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID ORDER BY Productos.Nombre">
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="TablaField">
                                                BODEGA:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlNewBoletaBodega" runat="server" 
                                                    DataSourceID="sdsNewBoletaBodega" DataTextField="bodega" 
                                                    DataValueField="bodegaID">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsNewBoletaBodega" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                    SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                FECHA ENTRADA:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewFechaEntrada" runat="server"></asp:TextBox>
                                                <rjs:PopCalendar ID="PopCalendar1" runat="server"  Control="txtNewFechaEntrada" 
                                                    Separator="/"/>
                                                <br />
                                                <asp:CheckBox ID="chkChangeFechaSalidaNewBoleta" runat="server" 
                                                    Text="Fecha Salida es Diferente a la de Entrada" />
                                                <div ID="divFechaSalidaNewBoleta" runat="Server">
                                                    <br />
                                                    FECHA SALIDA:
                                                    <asp:TextBox ID="txtNewFechaSalida" runat="server" ReadOnly="True"></asp:TextBox>
                                                    <rjs:PopCalendar ID="PopCalendar4" runat="server" Control="txtNewFechaSalida" 
                                                        Separator="/" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                PESO BRUTO:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewPesoEntrada" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                PESO TARA:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewPesoSalida" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                PESO NETO:</td>
                                            <td>
                                                <asp:TextBox ID="txtPesoNetoNewBoleta" runat="server" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                HUMEDAD:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewHumedad" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                IMPUREZAS:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewImpurezas" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                PRECIO:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewPrecio" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                SECADO:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewSecado" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
											             </table>
											             
                                                                                               
                                                                                            </div>
            </td>
            </tr>       
            
            <tr>
                <td colspan="2">
                <asp:Panel ID="pnlNewPago" runat="server">
                                                                                                <asp:Image ID="imgBienPago" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                                                                                                <asp:Image ID="imgMalPago" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                                                                                                <asp:Label ID="lblNewPagoResult" runat="server"></asp:Label>
                                                                                            </asp:Panel>
                </td>
            </tr>                 
            <tr>
                <td colspan ="2">
                <asp:UpdatePanel ID="UpdateAddNewPago" runat="Server">
                                                                        <ContentTemplate>
                                                                          
                                                                                 <table>
                                                                                 
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:UpdateProgress ID="UpProgPagos" runat="server" 
                                                                                                AssociatedUpdatePanelID="UpdateAddNewPago" DisplayAfter="0">
                                                                                                <ProgressTemplate>
                                                                                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                                                                                                    Procesando informacion de pago...
                                                                                                </ProgressTemplate>
                                                                                            </asp:UpdateProgress>
                                                                                            <asp:Button ID="btnAddPago" runat="server" onclick="btnAddPago_Click" 
                                                                                                Text="Agregar Pago a Crédito" />
                                                                                            <asp:Button ID="btnEdoDeCuenta" runat="server" onclick="btnEdoDeCuenta_Click" 
                                                                                                Text="Ir a Edo. de Cuenta" />
                                                                                            <asp:GridView ID="grvPagos" runat="server" AutoGenerateColumns="False" 
                                                                                                DataKeyNames="movbanID,movimientoID,tarjetaDieselID,boletaID,pagoCreditoID" 
                                                                                                DataSourceID="SqlPagos" onrowdatabound="grvPagos_RowDataBound" 
                                                                                                onrowdeleting="grvPagos_RowDeleting" ShowFooter="True" 
                                                                                                onrowdeleted="grvPagos_RowDeleted">
                                                                                                <Columns>
                                                                                                    <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                                                                                                    <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyy}" 
                                                                                                        HeaderText="Fecha" SortExpression="fecha" />
                                                                                                    <asp:BoundField DataField="movbanID" HeaderText="movbanID" 
                                                                                                        SortExpression="movbanID" visible="false" />
                                                                                                    <asp:BoundField DataField="movimientoID" HeaderText="movimientoID" 
                                                                                                        SortExpression="movimientoID" visible="false" />
                                                                                                    <asp:BoundField DataField="tarjetaDieselID" HeaderText="tarjetaDieselID" 
                                                                                                        SortExpression="tarjetaDieselID" visible="false" />
                                                                                                    <asp:TemplateField HeaderText="Forma de Pago">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="No. Cheque /Folio">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Banco">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Monto">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:BoundField DataField="boletaID" HeaderText="boletaID" 
                                                                                                        SortExpression="boletaID" Visible="False" />
                                                                                                    <asp:BoundField DataField="pagoCreditoID" HeaderText="pagoCreditoID" 
                                                                                                        InsertVisible="False" ReadOnly="True" SortExpression="pagoCreditoID" 
                                                                                                        Visible="False" />
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                            <asp:SqlDataSource ID="SqlPagos" runat="server" 
                                                                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                SelectCommand="SELECT fecha, movBanID, movimientoID, tarjetaDieselID, boletaID, pagoCreditoID FROM Pagos_Credito WHERE (creditoID = @creditoID)">
                                                                                                <SelectParameters>
                                                                                                    <asp:ControlParameter ControlID="ddlCredito" DefaultValue="-1" Name="creditoID" 
                                                                                                        PropertyName="SelectedValue" />
                                                                                                </SelectParameters>
                                                                                            </asp:SqlDataSource>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                        
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                                                
                                                   &nbsp;</td>
            </tr>
    </table>


</asp:Content>
