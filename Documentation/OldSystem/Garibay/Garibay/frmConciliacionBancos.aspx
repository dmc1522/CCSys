<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" Title="Conciliación con Banco" AutoEventWireup="true" CodeBehind="frmConciliacionBancos.aspx.cs" Inherits="Garibay.frmConciliacionBancos" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
 
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
      <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
    <ProgressTemplate>
        <asp:Image ID="imgLoading" runat="server" ImageUrl="~/imagenes/cargando.gif" />
        CARGANDO INFORMACION...
    </ProgressTemplate>
    </asp:UpdateProgress>

     <script type="text/javascript" src="/scripts/divFunctions.js"></script>

         
        <table >
            <tr>
                <td align="center" colspan="2" class="TableHeader">
                    CONCILIACIÓN BANCARIA</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Cuenta:</td>
                <td>
                    <asp:DropDownList ID="drpdlCuenta" runat="server" DataSourceID="sdsCuentas" 
                        DataTextField="cuenta" DataValueField="cuentaID" Height="23px" 
                        Width="490px" AutoPostBack="True" 
                        onselectedindexchanged="drpdlCuenta_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsCuentas" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT Bancos.nombre + ' - ' + CuentasDeBanco.NumeroDeCuenta + ' - ' + CuentasDeBanco.Titular AS cuenta, CuentasDeBanco.cuentaID FROM CuentasDeBanco INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID ORDER BY cuenta">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="TablaField" >
                    Mes:</td>
                <td >
                        <asp:DropDownList ID="drpdlMes" runat="server" AutoPostBack="True" 
                            Height="23px" onselectedindexchanged="drpdlMes_SelectedIndexChanged" 
                            Width="182px">
                            <asp:ListItem Value="1">ENERO</asp:ListItem>
                            <asp:ListItem Value="2">FEBRERO</asp:ListItem>
                            <asp:ListItem Value="3">MARZO</asp:ListItem>
                            <asp:ListItem Value="4">ABRIL</asp:ListItem>
                            <asp:ListItem Value="5">MAYO</asp:ListItem>
                            <asp:ListItem Value="6">JUNIO</asp:ListItem>
                            <asp:ListItem Value="7">JULIO</asp:ListItem>
                            <asp:ListItem Value="8">AGOSTO</asp:ListItem>
                            <asp:ListItem Value="9">SEPTIEMBRE</asp:ListItem>
                            <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
                            <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
                            <asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
                        </asp:DropDownList>
              
                </td>
            </tr>
            <tr>
                <td class="TablaField" >
                    Año:</td>
                <td >
                    <asp:DropDownList ID="drddlAnio" runat="server" Height="26px" Width="128px" 
                        AutoPostBack="True" 
                        onselectedindexchanged="drddlAnio_SelectedIndexChanged">
                        <asp:ListItem Value="2009">2009</asp:ListItem>
                        <asp:ListItem>2010</asp:ListItem>
                        <asp:ListItem>2011</asp:ListItem>
                        <asp:ListItem>2012</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtFechaIncioLargo" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtFechaFinLargo" runat="server" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" >
                    <asp:Button ID="btnConciliacion" runat="server" onclick="btnConciliacion_Click" 
                        Text="Actualizar" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="margin-left: 40px" class="TableHeader" >
                    <asp:CheckBox ID="chkMuestraNoConciliados" runat="server" 
                        Text="MOVIMIENTOS NO CONCILIADOS" />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" >
                 <asp:Panel ID="PanelNoConciliados" runat="Server">
                    <table>
                        <tr>
                            <td>
                    <asp:GridView ID="gvNoConciliados" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="sdsNoConciliados" style="margin-right: 0px" 
                        DataKeyNames="movbanID">
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                               <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                            <HeaderTemplate>
                          
                            <input id="chkAll" onclick="javascript:SelectAllCheckboxes(this);" 
                            runat="server" type="checkbox" visible="False" /> Conciliar/Cobrar

                            </HeaderTemplate>
                           </asp:TemplateField>

                            <asp:BoundField DataField="movbanID" HeaderText="movbanID" 
                                InsertVisible="False" ReadOnly="True" SortExpression="movbanID" 
                                Visible="False" />
                            <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" 
                                DataFormatString="{0:dd/MM/yyy}" />
                            <asp:BoundField DataField="numCheque" HeaderText="# Cheque" 
                                SortExpression="numCheque" />
                            <asp:BoundField DataField="chequeNombre" HeaderText="Nombre Fiscal" 
                                SortExpression="chequeNombre" />
                            <asp:BoundField DataField="Concepto" HeaderText="Concepto" 
                                SortExpression="Concepto" />
                            <asp:BoundField DataField="abono" HeaderText="Abono" SortExpression="abono" 
                                DataFormatString="{0:C2}" />
                            <asp:BoundField DataField="cargo" HeaderText="Cargo" SortExpression="cargo" 
                                DataFormatString="{0:C2}" />
                            <asp:BoundField DataField="cuentaID" HeaderText="cuentaID" 
                                SortExpression="cuentaID" Visible="False" />
                            <asp:BoundField DataField="ConceptoMovCuentaID" 
                                HeaderText="ConceptoMovCuentaID" SortExpression="ConceptoMovCuentaID" 
                                Visible="False" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre Interno" 
                                SortExpression="nombre" />
                            <asp:BoundField DataField="facturaOlarguillo" HeaderText="facturaOlarguillo" 
                                SortExpression="facturaOlarguillo" Visible="False" />
                            <asp:BoundField DataField="numCabezas" HeaderText="numCabezas" 
                                SortExpression="numCabezas" Visible="False" />
                            <asp:BoundField DataField="catalogoMovBancoFiscalID" 
                                HeaderText="catalogoMovBancoFiscalID" SortExpression="catalogoMovBancoFiscalID" 
                                Visible="False" />
                            <asp:BoundField DataField="subCatalogoMovBancoFiscalID" 
                                HeaderText="subCatalogoMovBancoFiscalID" 
                                SortExpression="subCatalogoMovBancoFiscalID" Visible="False" />
                            <asp:BoundField DataField="catalogoMovBancoInternoID" 
                                HeaderText="catalogoMovBancoInternoID" 
                                SortExpression="catalogoMovBancoInternoID" Visible="False" />
                            <asp:BoundField DataField="subCatalogoMovBancoInternoID" 
                                HeaderText="subCatalogoMovBancoInternoID" 
                                SortExpression="subCatalogoMovBancoInternoID" Visible="False" />
                            <asp:CheckBoxField DataField="chequecobrado" HeaderText="chequecobrado" 
                                SortExpression="chequecobrado" Visible="False" />
                            <asp:BoundField DataField="fechacobrado" HeaderText="fechacobrado" 
                                SortExpression="fechacobrado" Visible="False" />
                            <asp:BoundField DataField="catalogoMovBancoInterno" 
                                HeaderText="catalogoMovBancoInterno" SortExpression="catalogoMovBancoInterno" />
                            <asp:BoundField DataField="subCatalogoInterno" HeaderText="Sub-Catálogo Interno" 
                                SortExpression="subCatalogoInterno" />
                            <asp:BoundField DataField="catalogoMovBancoFiscal" 
                                HeaderText="Catálogo Fiscal" SortExpression="catalogoMovBancoFiscal" />
                            <asp:BoundField DataField="subCatalogoFiscal" HeaderText="Sub-CatálogoFiscal" 
                                SortExpression="subCatalogoFiscal" />
                            <asp:BoundField DataField="creditoFinancieroID" 
                                HeaderText="creditoFinancieroID" InsertVisible="False" ReadOnly="True" 
                                SortExpression="creditoFinancieroID" Visible="False" />
                            <asp:BoundField DataField="bancoCreditoFinanciero" 
                                HeaderText="bancoCreditoFinanciero" SortExpression="bancoCreditoFinanciero" 
                                Visible="False" />
                            <asp:BoundField DataField="numCreditoFinanciero" 
                                HeaderText="numCreditoFinanciero" SortExpression="numCreditoFinanciero" 
                                Visible="False" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsNoConciliados" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                                    SelectCommand="SELECT MovimientosCuentasBanco.movbanID, MovimientosCuentasBanco.chequeNombre, MovimientosCuentasBanco.fecha, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.abono, MovimientosCuentasBanco.cargo, MovimientosCuentasBanco.cuentaID, MovimientosCuentasBanco.ConceptoMovCuentaID, MovimientosCuentasBanco.nombre, MovimientosCuentasBanco.facturaOlarguillo, MovimientosCuentasBanco.numCabezas, MovimientosCuentasBanco.numCheque,  MovimientosCuentasBanco.catalogoMovBancoFiscalID, MovimientosCuentasBanco.subCatalogoMovBancoFiscalID, MovimientosCuentasBanco.catalogoMovBancoInternoID, MovimientosCuentasBanco.subCatalogoMovBancoInternoID, MovimientosCuentasBanco.chequecobrado, MovimientosCuentasBanco.fechacobrado, catalogoMovimientosBancos_1.catalogoMovBanco AS catalogoMovBancoInterno, SubCatalogoMovimientoBanco.subCatalogo AS subCatalogoInterno, catalogoMovimientosBancos.catalogoMovBanco AS catalogoMovBancoFiscal, SubCatalogoMovimientoBanco_1.subCatalogo AS subCatalogoFiscal, CreditosFinancieros.creditoFinancieroID, Bancos.nombre AS bancoCreditoFinanciero, CreditosFinancieros.numCredito AS numCreditoFinanciero FROM SubCatalogoMovimientoBanco RIGHT OUTER JOIN MovimientosCuentasBanco INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID INNER JOIN catalogoMovimientosBancos ON MovimientosCuentasBanco.catalogoMovBancoFiscalID = catalogoMovimientosBancos.catalogoMovBancoID INNER JOIN catalogoMovimientosBancos AS catalogoMovimientosBancos_1 ON MovimientosCuentasBanco.catalogoMovBancoInternoID = catalogoMovimientosBancos_1.catalogoMovBancoID ON SubCatalogoMovimientoBanco.subCatalogoMovBancoID = MovimientosCuentasBanco.subCatalogoMovBancoInternoID LEFT OUTER JOIN SubCatalogoMovimientoBanco AS SubCatalogoMovimientoBanco_1 ON MovimientosCuentasBanco.subCatalogoMovBancoFiscalID = SubCatalogoMovimientoBanco_1.subCatalogoMovBancoID LEFT OUTER JOIN Bancos INNER JOIN CreditosFinancieros ON Bancos.bancoID = CreditosFinancieros.bancoID ON MovimientosCuentasBanco.creditoFinancieroID = CreditosFinancieros.creditoFinancieroID
where MovimientosCuentasBanco.chequeCobrado = 0 AND MovimientosCuentasBanco.cuentaID = @cuentaID order by MovimientosCuentasBanco.fecha">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="drpdlCuenta" Name="cuentaID" 
                                PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" class="TableHeader" >
                    <asp:CheckBox ID="chkMuestraConciliados" runat="server" 
                        Text="MOVIMIENTOS CONCILIADOS" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" >
                <asp:Panel ID="Panelconciliados" runat="Server">
                    <table>
                        <tr>
                            <td>
                    <asp:GridView ID="gvConciliados" runat="server" 
                        AutoGenerateColumns="False" style="margin-right: 0px" 
                        DataSourceID="sdsConciliados" DataKeyNames="movbanID">
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                            <HeaderTemplate>
                          
                            <input id="chkAll0" onclick="javascript:SelectAllCheckboxes(this);" 
                            runat="server" type="checkbox" visible="False" />Conciliar/Cobrar

                            </HeaderTemplate>
                            <ItemTemplate>
                               <asp:CheckBox ID="chkSelect0" runat="server" />
                            </ItemTemplate>
                           </asp:TemplateField>

                            <asp:BoundField DataField="movbanID" HeaderText="movbanID" 
                                InsertVisible="False" ReadOnly="True" SortExpression="movbanID" 
                                Visible="False" />
                            <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" 
                                DataFormatString="{0:dd/MM/yyy}" />
                            <asp:BoundField DataField="numCheque" HeaderText="# Cheque" 
                                SortExpression="numCheque" />
                            <asp:BoundField DataField="chequeNombre" HeaderText="Nombre fiscal" 
                                SortExpression="chequeNombre" />
                            <asp:BoundField DataField="Concepto" HeaderText="Concepto" 
                                SortExpression="Concepto" />
                            <asp:BoundField DataField="abono" HeaderText="Abono" SortExpression="abono" 
                                DataFormatString="{0:C2}" />
                            <asp:BoundField DataField="cargo" HeaderText="Cargo" SortExpression="cargo" 
                                DataFormatString="{0:C2}" />
                            <asp:BoundField DataField="cuentaID" HeaderText="cuentaID" 
                                SortExpression="cuentaID" Visible="False" />
                            <asp:BoundField DataField="ConceptoMovCuentaID" 
                                HeaderText="ConceptoMovCuentaID" SortExpression="ConceptoMovCuentaID" 
                                Visible="False" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre interno" 
                                SortExpression="nombre" />
                            <asp:BoundField DataField="facturaOlarguillo" HeaderText="facturaOlarguillo" 
                                SortExpression="facturaOlarguillo" Visible="False" />
                            <asp:BoundField DataField="numCabezas" HeaderText="numCabezas" 
                                SortExpression="numCabezas" Visible="False" />
                            <asp:BoundField DataField="catalogoMovBancoFiscalID" 
                                HeaderText="catalogoMovBancoFiscalID" SortExpression="catalogoMovBancoFiscalID" 
                                Visible="False" />
                            <asp:BoundField DataField="subCatalogoMovBancoFiscalID" 
                                HeaderText="subCatalogoMovBancoFiscalID" 
                                SortExpression="subCatalogoMovBancoFiscalID" Visible="False" />
                            <asp:BoundField DataField="catalogoMovBancoInternoID" 
                                HeaderText="catalogoMovBancoInternoID" 
                                SortExpression="catalogoMovBancoInternoID" Visible="False" />
                            <asp:BoundField DataField="subCatalogoMovBancoInternoID" 
                                HeaderText="subCatalogoMovBancoInternoID" 
                                SortExpression="subCatalogoMovBancoInternoID" Visible="False" />
                            <asp:CheckBoxField DataField="chequecobrado" HeaderText="chequecobrado" 
                                SortExpression="chequecobrado" Visible="False" />
                            <asp:BoundField DataField="fechacobrado" HeaderText="fechacobrado" 
                                SortExpression="fechacobrado" Visible="False" />
                            <asp:BoundField DataField="catalogoMovBancoInterno" 
                                HeaderText="Catálogo Interno" SortExpression="catalogoMovBancoInterno" />
                            <asp:BoundField DataField="subCatalogoInterno" HeaderText="Sub-Catálogo Interno" 
                                SortExpression="subCatalogoInterno" />
                            <asp:BoundField DataField="catalogoMovBancoFiscal" 
                                HeaderText="Catálogo Fiscal" SortExpression="catalogoMovBancoFiscal" />
                            <asp:BoundField DataField="subCatalogoFiscal" HeaderText="Sub - catálogo Fiscal" 
                                SortExpression="subCatalogoFiscal" />
                            <asp:BoundField DataField="creditoFinancieroID" 
                                HeaderText="creditoFinancieroID" InsertVisible="False" ReadOnly="True" 
                                SortExpression="creditoFinancieroID" Visible="False" />
                            <asp:BoundField DataField="bancoCreditoFinanciero" 
                                HeaderText="bancoCreditoFinanciero" SortExpression="bancoCreditoFinanciero" 
                                Visible="False" />
                            <asp:BoundField DataField="numCreditoFinanciero" 
                                HeaderText="numCreditoFinanciero" SortExpression="numCreditoFinanciero" 
                                Visible="False" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsConciliados" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                                    SelectCommand="SELECT MovimientosCuentasBanco.movbanID, MovimientosCuentasBanco.chequeNombre, MovimientosCuentasBanco.fecha, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.abono, MovimientosCuentasBanco.cargo, MovimientosCuentasBanco.cuentaID, MovimientosCuentasBanco.ConceptoMovCuentaID, MovimientosCuentasBanco.nombre, MovimientosCuentasBanco.facturaOlarguillo, MovimientosCuentasBanco.numCabezas, MovimientosCuentasBanco.numCheque,  MovimientosCuentasBanco.catalogoMovBancoFiscalID, MovimientosCuentasBanco.subCatalogoMovBancoFiscalID, MovimientosCuentasBanco.catalogoMovBancoInternoID, MovimientosCuentasBanco.subCatalogoMovBancoInternoID, MovimientosCuentasBanco.chequecobrado, MovimientosCuentasBanco.fechacobrado, catalogoMovimientosBancos_1.catalogoMovBanco AS catalogoMovBancoInterno, SubCatalogoMovimientoBanco.subCatalogo AS subCatalogoInterno, catalogoMovimientosBancos.catalogoMovBanco AS catalogoMovBancoFiscal, SubCatalogoMovimientoBanco_1.subCatalogo AS subCatalogoFiscal, CreditosFinancieros.creditoFinancieroID, Bancos.nombre AS bancoCreditoFinanciero, CreditosFinancieros.numCredito AS numCreditoFinanciero FROM SubCatalogoMovimientoBanco RIGHT OUTER JOIN MovimientosCuentasBanco INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID INNER JOIN catalogoMovimientosBancos ON MovimientosCuentasBanco.catalogoMovBancoFiscalID = catalogoMovimientosBancos.catalogoMovBancoID INNER JOIN catalogoMovimientosBancos AS catalogoMovimientosBancos_1 ON MovimientosCuentasBanco.catalogoMovBancoInternoID = catalogoMovimientosBancos_1.catalogoMovBancoID ON SubCatalogoMovimientoBanco.subCatalogoMovBancoID = MovimientosCuentasBanco.subCatalogoMovBancoInternoID LEFT OUTER JOIN SubCatalogoMovimientoBanco AS SubCatalogoMovimientoBanco_1 ON MovimientosCuentasBanco.subCatalogoMovBancoFiscalID = SubCatalogoMovimientoBanco_1.subCatalogoMovBancoID LEFT OUTER JOIN Bancos INNER JOIN CreditosFinancieros ON Bancos.bancoID = CreditosFinancieros.bancoID ON MovimientosCuentasBanco.creditoFinancieroID = CreditosFinancieros.creditoFinancieroID
where MovimientosCuentasBanco.chequeCobrado = 1 AND MovimientosCuentasBanco.cuentaID = @cuentaID order by MovimientosCuentasBanco.fecha">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="drpdlCuenta" Name="cuentaID" 
                                PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                     </asp:Panel>
                </td>
            </tr>
    </table>
    

          <script language="javascript">

              function SelectAllCheckboxes(spanChk) {

                  // Added as ASPX uses SPAN for checkbox

                  var oItem = spanChk.children;
                  var theBox = (spanChk.type == "checkbox") ?
                spanChk : spanChk.children.item[0];
                  xState = theBox.checked;
                  elm = theBox.form.elements;

                  for (i = 0; i < elm.length; i++)
                      if (elm[i].type == "checkbox" &&
                      elm[i].id != theBox.id) {
                      //elm[i].click();

                      if (elm[i].checked != xState)
                          elm[i].click();
                      //elm[i].checked=xState;

                  }
              }
        </script>
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    </asp:Content>
