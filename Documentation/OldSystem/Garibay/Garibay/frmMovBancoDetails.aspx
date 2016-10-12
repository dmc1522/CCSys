<%@ Page Title="Detalles de Movimiento de Banco" Language="C#" AutoEventWireup="true" CodeBehind="frmMovBancoDetails.aspx.cs" Inherits="Garibay.MovBancoDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="panelMensaje" runat="server">
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
                    <asp:Label ID="lblMensajeOperationresult" runat="server" 
                        SkinID="lblMensajeOperationresult" Text="Label"></asp:Label>
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
                    <asp:TextBox ID="txtIDDetails" runat="server" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" 
                        DataKeyNames="movbanID" DataSourceID="sdsDetailsMovBanco" Height="50px" 
                        Width="300px">
                        <Fields>
                            <asp:BoundField DataField="movbanID" HeaderText="# movimiento" 
                                InsertVisible="False" ReadOnly="True" SortExpression="movbanID" />
                            <asp:BoundField DataField="cuenta" HeaderText="Cuenta" ReadOnly="True" 
                                SortExpression="cuenta" />
                            <asp:BoundField DataField="Concepto" HeaderText="Concepto" 
                                SortExpression="Concepto" />
                            <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                HeaderText="Fecha" SortExpression="fecha" />
                            <asp:BoundField DataField="cargo" DataFormatString="{0:c}" HeaderText="Cargo" 
                                SortExpression="cargo" />
                            <asp:BoundField DataField="abono" DataFormatString="{0:c}" HeaderText="Abono" 
                                SortExpression="abono" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" 
                                SortExpression="nombre" />
                            <asp:BoundField DataField="facturaOlarguillo" HeaderText="Factura o larguillo" 
                                SortExpression="facturaOlarguillo" />
                            <asp:BoundField DataField="numCabezas" DataFormatString="{0:n}" 
                                HeaderText="# cabezas" SortExpression="numCabezas" />
                            <asp:BoundField DataField="numCheque" HeaderText=" # cheque" 
                                SortExpression="numCheque" />
                            <asp:BoundField DataField="chequeNombre" HeaderText="Nombre Interno" 
                                SortExpression="chequeNombre" />
                            <asp:CheckBoxField DataField="chequecobrado" HeaderText="Cobrado" 
                                SortExpression="chequecobrado" />
                            <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" 
                                SortExpression="Observaciones" />
                            <asp:BoundField DataField="CatalogoFiscal" HeaderText="Catalogo Fiscal" 
                                SortExpression="CatalogoFiscal" />
                            <asp:BoundField DataField="SubcatalogoFiscal" HeaderText="Subcatálogo Fiscal" 
                                SortExpression="SubcatalogoFiscal" />
                            <asp:BoundField DataField="CatalogoInterno" HeaderText="Catalogo Interno" 
                                SortExpression="CatalogoInterno" />
                            <asp:BoundField DataField="SubCatalogoInterno" HeaderText="SubCatálogo Interno" 
                                SortExpression="SubCatalogoInterno" />
                            <asp:BoundField DataField="movOrigenID" HeaderText="# Mov. Origen" 
                                SortExpression="movOrigenID" />
                        </Fields>
                    </asp:DetailsView>
                    <asp:SqlDataSource ID="sdsDetailsMovBanco" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        SelectCommand="SELECT MovimientosCuentasBanco.movbanID, Bancos.nombre + ' - ' + CuentasDeBanco.NumeroDeCuenta AS cuenta, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.fecha, MovimientosCuentasBanco.cargo, MovimientosCuentasBanco.abono, MovimientosCuentasBanco.nombre, MovimientosCuentasBanco.facturaOlarguillo, MovimientosCuentasBanco.numCabezas, MovimientosCuentasBanco.numCheque, MovimientosCuentasBanco.chequeNombre, MovimientosCuentasBanco.chequeQuienrecibe, MovimientosCuentasBanco.chequecobrado, MovimientosCuentasBanco.Observaciones, catalogoMovimientosBancos.catalogoMovBanco AS CatalogoFiscal, SubCatalogoMovimientoBanco.subCatalogo AS SubcatalogoFiscal, catalogoMovimientosBancos_1.catalogoMovBanco AS CatalogoInterno, SubCatalogoMovimientoBanco_1.subCatalogo AS SubCatalogoInterno, MovimientosCuentasBanco.movOrigenID FROM MovimientosCuentasBanco INNER JOIN CuentasDeBanco ON MovimientosCuentasBanco.cuentaID = CuentasDeBanco.cuentaID INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID INNER JOIN catalogoMovimientosBancos ON MovimientosCuentasBanco.catalogoMovBancoFiscalID = catalogoMovimientosBancos.catalogoMovBancoID INNER JOIN catalogoMovimientosBancos AS catalogoMovimientosBancos_1 ON MovimientosCuentasBanco.catalogoMovBancoInternoID = catalogoMovimientosBancos_1.catalogoMovBancoID LEFT OUTER JOIN SubCatalogoMovimientoBanco AS SubCatalogoMovimientoBanco_1 ON catalogoMovimientosBancos_1.catalogoMovBancoID = SubCatalogoMovimientoBanco_1.catalogoMovBancoID LEFT OUTER JOIN SubCatalogoMovimientoBanco ON catalogoMovimientosBancos.catalogoMovBancoID = SubCatalogoMovimientoBanco.catalogoMovBancoID WHERE (MovimientosCuentasBanco.movbanID = @ID)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtIDDetails" DefaultValue="-1" Name="ID" 
                                PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnAceptardtlst" runat="server" 
                        Text="Aceptar" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    </form>
</body>
</html>
