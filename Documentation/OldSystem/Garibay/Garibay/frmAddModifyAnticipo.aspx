<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master"  AutoEventWireup="true" Title= "Anticipos" CodeBehind="frmAddModifyAnticipo.aspx.cs" Inherits="Garibay.frmAddModifyAnticipo" %>

<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

    <asp:UpdatePanel runat="Server" ID="UpdatePanel" >
<ContentTemplate>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/cargando.gif" />Cargando Datos...
    </ProgressTemplate>
    </asp:UpdateProgress>
    <script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>

         
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
                        Text="SI NO HAY EXC BORREN EL TEXTO"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAceptardtlst" runat="server" Text="Aceptar" 
                                />
                    </td>
                </tr>
            </table>
    </asp:Panel>
           <asp:Panel ID="panelagregar" runat="server">
        <table >
            <tr>
                <td class="TableHeader" colspan="2" align="center">
                    AGREGAR NUEVO ANTICIPO</td>
                <td>
                    <asp:TextBox ID="txtidToModify" runat="server" Visible="False" 
                        ontextchanged="txtidToModify_TextChanged"></asp:TextBox>
                </td>
                <td align="left" rowspan="9" valign="top">
                    <table class="style3">
                        <tr>
                            <td class="TableHeader">
                                CONCENTRADO DE BOLETAS AGREGADAS</td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:GridView ID="gridBoletas" runat="server" AutoGenerateColumns="False" 
                                    DataSourceID="SqlDataSource5">
                                    <Columns>
                                        <asp:BoundField DataField="Nombre" HeaderText="Producto" 
                                            SortExpression="Nombre" />
                                        <asp:BoundField DataField="suma" DataFormatString="{0:n}" 
                                            HeaderText="Suma en Kgs." ReadOnly="True" SortExpression="suma" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT SUM(Boletas.pesonetoentrada) AS suma, Productos.Nombre FROM Boletas INNER JOIN Productos ON Boletas.productoID = Productos.productoID WHERE (Boletas.productorID = @ProductorID) AND (Boletas.pagada = 0) GROUP BY Productos.Nombre">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="cmbProductor" DefaultValue="-1" 
                                            Name="ProductorID" PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Ciclo:</td>
                <td>
                     <asp:DropDownList ID="cmbCiclo" runat="server" DataSourceID="SqlDataSource2" 
                         DataTextField="CicloName" DataValueField="cicloID" 
                        Width="250px">
                     </asp:DropDownList>
                     <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                         ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                         SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos]">
                     </asp:SqlDataSource>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Productor:</td>
                <td>
                <br />
                    <asp:DropDownList ID="cmbProductor" runat="server" DataSourceID="SqlDataSource1" 
                        DataTextField="name" DataValueField="productorID" 
                       Width="250px" AutoPostBack="True">
                    </asp:DropDownList>
                    <cc1:ListSearchExtender ID="cmbProductor_ListSearchExtender" runat="server" 
                        Enabled="True" TargetControlID="cmbProductor" 
                        PromptText="Escriba para Buscar">
                    </cc1:ListSearchExtender>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        SelectCommand="Select productores.apaterno  + ' ' + productores.amaterno + ' ' + productores.nombre as name, productores.productorID from Productores  order by name">
                    </asp:SqlDataSource>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Fecha:</td>
                <td>
                    <asp:TextBox ID="txtFecha" runat="server" ReadOnly="True"></asp:TextBox>
                    <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtFecha" 
                        Separator="/" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="valFecha" runat="server" 
                        ControlToValidate="txtFecha" ErrorMessage="El campo fecha es necesario"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Tipo de anticipo:</td>
                <td>
                    <asp:DropDownList ID="cmbTipoAnticipo" runat="server" 
                        DataSourceID="SqlDataSource7" DataTextField="tipoAnticipo" 
                        DataValueField="tipoAnticipoID" Height="22px" Width="193px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource7" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT [tipoAnticipoID], [tipoAnticipo] FROM [TiposAnticipos]">
                    </asp:SqlDataSource>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>  
           
                <td colspan="2">
                 <div id = "divPrestamo" runat = "Server">
                  <table border="1">
                        <tr>
                            <td align="center" colspan="2" class="TableHeader">
                                DATOS DE PRÉSTAMO</td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Interés Anual:</td>
                            <td>
                                <asp:TextBox ID="txtInteresAnual" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Interés Moratorio:</td>
                            <td>
                                <asp:TextBox ID="txtInteresMoratorio" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Fecha límite de pago:</td>
                            <td>
                                <asp:TextBox ID="txtFechaLimiteDePAgo" runat="server" ReadOnly="True"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFechaLimiteDePAgo_CalendarExtender" runat="server" 
                                    Enabled="True" TargetControlID="txtFechaLimiteDePAgo">
                                </cc1:CalendarExtender>
                                <rjs:PopCalendar ID="PopCalendar2" runat="server" 
                                    Control="txtFechaLimiteDePAgo" Separator="/" />
                            </td>
                        </tr>
                    </table>
                  </div>  
                </td>
               
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Tipo de pago:</td>
                <td>
                    <asp:DropDownList ID="cmbTipodeMov" runat="server" AutoPostBack="True" 
                        Height="22px" onselectedindexchanged="cmbTipodeMov_SelectedIndexChanged" 
                        Width="249px">
                        <asp:ListItem Value="0">EFECTIVO</asp:ListItem>
                        <asp:ListItem>MOVIMIENTO DE BANCO</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNombre" runat="server" Width="266px"></asp:TextBox>
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
                    <asp:RequiredFieldValidator ID="valMontorequired0" runat="server" 
                        ControlToValidate="txtMonto" ErrorMessage="El campo monto es necesario"></asp:RequiredFieldValidator>
                    <br />
                </td>
            </tr>
            
            <tr>
                <td colspan="2">
                <div id="divMovBanco" runat="Server">
                    <table border="1">
                        <tr>
                            <td class="TableHeader" align="center" colspan="2">
                                DATOS MOVIMIENTO DE BANCO</td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Concepto:</td>
                            <td>
                                <asp:DropDownList ID="cmbConceptomovBanco" runat="server" 
                                    DataSourceID="SqlDataSource3" DataTextField="Concepto" 
                                    DataValueField="ConceptoMovCuentaID" Height="22px" Width="249px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT [ConceptoMovCuentaID], [Concepto] FROM [ConceptosMovCuentas]">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Cuenta:</td>
                            <td>
                                <asp:DropDownList ID="cmbCuenta" runat="server" DataSourceID="SqlDataSource4" 
                                    DataTextField="cuenta" DataValueField="cuentaID" Height="22px" Width="249px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT Bancos.nombre + '  ' + CuentasDeBanco.NumeroDeCuenta AS cuenta, CuentasDeBanco.cuentaID FROM Bancos INNER JOIN CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Grupo de catálogos de cuenta fiscal:
                            </td>
                            <td>
                                <asp:DropDownList ID="drpdlGrupoCuentaFiscal" runat="server" 
                                    AutoPostBack="True" DataSourceID="sdsGruposCatalogosfiscal" 
                                    DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID" Height="23px" 
                                    onselectedindexchanged="drpdlGrupoCuentaFiscal_SelectedIndexChanged" 
                                    Width="257px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsGruposCatalogosfiscal" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos]">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Catálogo de cuenta fiscal:</td>
                            <td>
                                <asp:DropDownList ID="drpdlCatalogocuentafiscal" runat="server" 
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
                                <asp:DropDownList ID="drpdlSubcatalogofiscal" runat="server" 
                                    AutoPostBack="True" DataSourceID="sdsSubcatalogofiscal" 
                                    DataTextField="subCatalogo" DataValueField="subCatalogoMovBancoID" 
                                    Height="23px" Width="258px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsSubcatalogofiscal" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT SubCatalogoMovimientoBanco.subCatalogo, SubCatalogoMovimientoBanco.subCatalogoMovBancoID FROM SubCatalogoMovimientoBanco INNER JOIN catalogoMovimientosBancos ON SubCatalogoMovimientoBanco.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID WHERE (SubCatalogoMovimientoBanco.catalogoMovBancoID = @catalogoMovBancoID)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="drpdlCatalogocuentafiscal" DefaultValue="-1" 
                                            Name="catalogoMovBancoID" PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divCheque" runat ="server">
                
                    <table border="1">
                    
                	    <tr>
                		    <td class="TableHeader" colspan="2" align="center">DATOS DE CHEQUE</td>
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
                                <asp:DropDownList ID="drpdlGrupoCatalogos" runat="server" AutoPostBack="True" 
                                    DataSourceID="sdsGruposCatalogos" DataTextField="grupoCatalogo" 
                                    DataValueField="grupoCatalogosID" Height="23px" 
                                    onselectedindexchanged="drpdlGrupoCatalogos_SelectedIndexChanged" Width="235px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsGruposCatalogos" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos]">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Catálogo de cuenta interna:</td>
                            <td>
                                <asp:DropDownList ID="drpdlCatalogoInterno" runat="server" AutoPostBack="True" 
                                    DataSourceID="sdsCatalogoCuentaInterna" DataTextField="catalogoMovBanco" 
                                    DataValueField="catalogoMovBancoID" Height="23px" Width="236px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsCatalogoCuentaInterna" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT catalogoMovBancoID, catalogoMovBanco FROM catalogoMovimientosBancos WHERE (grupoCatalogoID = @grupoCatalogoID)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="drpdlGrupoCatalogos" DefaultValue="-1" 
                                            Name="grupoCatalogoID" PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Subcatálogo de cuenta interna:</td>
                            <td>
                                <asp:DropDownList ID="drpdlSubcatologointerna" runat="server" 
                                    AutoPostBack="True" DataSourceID="sdsSubCatalogoInterna" 
                                    DataTextField="subCatalogo" DataValueField="subCatalogoMovBancoID" 
                                    Height="23px" Width="234px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsSubCatalogoInterna" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT SubCatalogoMovimientoBanco.subCatalogo, SubCatalogoMovimientoBanco.subCatalogoMovBancoID FROM SubCatalogoMovimientoBanco INNER JOIN catalogoMovimientosBancos ON SubCatalogoMovimientoBanco.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID WHERE (SubCatalogoMovimientoBanco.catalogoMovBancoID = @catalogoMovBancoID)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="drpdlCatalogoInterno" DefaultValue="-1" 
                                            Name="catalogoMovBancoID" PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    
                    </table>
                
                </div>
                
                    </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            
            <tr>
                
                <td style="text-align: right">
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
                         style="height: 26px" Height="20px" onclick="btnAgregar_Click"/>
                </td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" 
                        CausesValidation="False" />
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: right">
                    <table class="style3">
                        <tr>
                            <td class="TableHeader" style="text-align: center">
                                ANTICIPOS DADOS AL PRODUCTOR</td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:GridView ID="gridAnticipos" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="anticipoID" DataSourceID="SqlDataSource6">
                                    <Columns>
                                        <asp:BoundField DataField="anticipoID" HeaderText="# Anticipo" 
                                            InsertVisible="False" ReadOnly="True" SortExpression="anticipoID" />
                                        <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                                            SortExpression="Productor" />
                                        <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" ReadOnly="True" 
                                            SortExpression="Cuenta" />
                                        <asp:BoundField DataField="Concepto" HeaderText="Concepto" 
                                            SortExpression="Concepto" />
                                        <asp:BoundField DataField="Monto" DataFormatString="{0:c}" HeaderText="Monto" 
                                            ReadOnly="True" SortExpression="Monto" />
                                        <asp:BoundField DataField="numCheque" HeaderText="# Cheque" 
                                            SortExpression="numCheque" />
                                        <asp:BoundField DataField="Efectivo" DataFormatString="{0:c}" 
                                            HeaderText="Efectivo" ReadOnly="True" SortExpression="Efectivo" />
                                        <asp:BoundField DataField="tipoAnticipo" HeaderText="Tipo de anticipo" 
                                            SortExpression="tipoAnticipo" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSource6" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    
                                    SelectCommand="SELECT Anticipos.anticipoID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS Productor, Bancos.nombre + ' ' + CuentasDeBanco.NumeroDeCuenta AS Cuenta, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.cargo + MovimientosCuentasBanco.abono AS Monto, MovimientosCuentasBanco.numCheque, MovimientosCaja.cargo + MovimientosCaja.abono AS Efectivo, TiposAnticipos.tipoAnticipo FROM TiposAnticipos INNER JOIN Anticipos INNER JOIN Productores ON Anticipos.productorID = Productores.productorID ON TiposAnticipos.tipoAnticipoID = Anticipos.tipoAnticipoID LEFT OUTER JOIN CuentasDeBanco INNER JOIN MovimientosCuentasBanco ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco.cuentaID INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID ON Anticipos.movbanID = MovimientosCuentasBanco.movbanID LEFT OUTER JOIN MovimientosCaja ON Anticipos.movimientoID = MovimientosCaja.movimientoID WHERE (Productores.productorID = @productorID)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="cmbProductor" DefaultValue="-1" 
                                            Name="productorID" PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            </table>

  </asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style3
        {
            width: 100%;
        }
    </style>

    </asp:Content>

