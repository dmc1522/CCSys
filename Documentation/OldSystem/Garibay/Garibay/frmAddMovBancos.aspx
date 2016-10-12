<%@ Page Language="C#" Theme="skinrojo" AutoEventWireup="true" CodeBehind="frmAddMovBancos.aspx.cs" Inherits="Garibay.frmAddMovBancos" MasterPageFile="MasterPage.Master" Title="Agregar movimiento de cuentas de banco" %>

<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
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
                        
                            <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="300px">
                            </asp:DetailsView>
                        
                        </td>
                   </tr>
                   <tr>
                        <td align="center">
                        
                            <asp:Button ID="btnAceptardtlst" runat="server" Text="Aceptar" 
                                onclick="btnAceptardtlst_Click" />
                        
                            <asp:Button ID="btnPrintCheque" runat="server" 
                                Text="Imprimir cheque" onclick="btnPrintCheque_Click" Visible="False" />
                        
                        </td>
                   </tr>
               </table>
           </asp:Panel>
           <asp:Panel ID="panelagregar" runat="server">
        <table >
            <tr>
                <td class="TableHeader" colspan="2">
                    <asp:Label ID="lblHeader" runat="server" 
                        Text="AGREGAR NUEVO MOVIMIENTO DE BANCO"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtidToModify" runat="server" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Fecha:</td>
                <td>
                     <asp:TextBox ID="txtFecha" runat="server" ReadOnly="True"></asp:TextBox>
                    <rjs:PopCalendar ID="PopCalendar1" runat="server" 
                        onselectionchanged="PopCalendar1_SelectionChanged" style="height: 16px" 
                        Separator="/" Control="txtFecha" /></td>
                <td>
                    <asp:RequiredFieldValidator ID="valFecha" runat="server" 
                        ControlToValidate="txtFecha" ErrorMessage="El campo fecha es necesario"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Cuenta:</td>
                <td>
                    <asp:DropDownList ID="cmbCuenta" runat="server" DataSourceID="SqlDataSource1" 
                        DataTextField="Cuenta" DataValueField="cuentaID" 
                        onselectedindexchanged="cmbCuenta_SelectedIndexChanged" Width="250px">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Tipo de movimiento:</td>
                <td>
                    <asp:DropDownList ID="cmbTipodeMov" runat="server" Width="249px" Height="22px" 
                        onselectedindexchanged="cmbTipodeMov_SelectedIndexChanged">
                        <asp:ListItem>CARGO</asp:ListItem>
                        <asp:ListItem>ABONO</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Concepto:</td>
                <td>
                <script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>
                   <asp:DropDownList ID="ddlConcepto" runat="server" DataSourceID="sdsConceptos" 
                        DataTextField="Concepto" DataValueField="ConceptoMovCuentaID" 
                        Height="23px" Width="167px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsConceptos" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        SelectCommand="SELECT [ConceptoMovCuentaID], [Concepto] FROM [ConceptosMovCuentas]  where concepto &lt;&gt; 'ANTICIPO LIQUIDACION' ORDER BY [Concepto]">
                    </asp:SqlDataSource></td>
                <td>
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td class="TablaField">
                    Nombre fiscal:</td>
                <td>
                    <asp:TextBox ID="txtNombre" runat="server" Width="413px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Monto:</td>
                <td>
                    <asp:TextBox ID="txtMonto" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtMonto" ErrorMessage="Escriba una cantida válida" 
                        ValidationExpression="\d+(.\d*)?"></asp:RegularExpressionValidator>
                    <br />
                    <asp:RequiredFieldValidator ID="valMontorequired" runat="server" 
                        ControlToValidate="txtMonto" ErrorMessage="El campo monto es necesario"></asp:RequiredFieldValidator>
                </td>
            </tr>
            
            <tr>
                <td class="TablaField">
                    Grupo de catálogos de cuenta fiscal:</td>
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
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Catalogo de cuenta fiscal:</td>
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
                <td>
                    &nbsp;</td>
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
                <td>
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td colspan="3">    
                <div id="divCheque" runat="Server" >
                    <table border="1">
                    
                	    <tr>
                		    <td class="TableHeader" colspan="2">Datos del cheque:</td>
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
                                # de Factura o Larguillo:</td>
                            <td>
                                <asp:TextBox ID="txtFacturaLarguillo" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                # de Cabezas:</td>
                            <td>
                                <asp:TextBox ID="txtNumCabezas" runat="server"></asp:TextBox>
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
            </tr>
            
            <tr>
                
                <td style="text-align: right">
                    <asp:Button ID="btnModificar" runat="server"  
                        style="height: 26px" Text="Modificar" onclick="btnModificar_Click" />
                    <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="1px"></asp:TextBox>
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
                        onclick="btnAgregar_Click1" style="height: 26px"/>
                </td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" 
                        CausesValidation="False" onclick="btnCancel_Click" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
                        
                        SelectCommand="SELECT CuentasDeBanco.cuentaID, Bancos.nombre + ' - ' + CuentasDeBanco.NumeroDeCuenta AS Cuenta FROM CuentasDeBanco INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID ORDER BY Cuenta"></asp:SqlDataSource>
                </td>
            </tr>
            </table>

  </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    </asp:Content>
