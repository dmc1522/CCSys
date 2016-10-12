<%@ Page Title="Lista de movimientos por Credito" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListPerCreditoFinanciero.aspx.cs" Inherits="Garibay.frmListPerCreditoFinanciero" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table >
	<tr>
		<td class="TablaField">Credito Financiero:</td> <td>
            <asp:DropDownList ID="ddlCreditoFinanciero" runat="server" AutoPostBack="True" 
                DataSourceID="sdsCreditosFinancieros" DataTextField="cuenta" 
                DataValueField="creditoFinancieroID" 
                onselectedindexchanged="DropDownList1_SelectedIndexChanged" Width="484px">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sdsCreditosFinancieros" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                SelectCommand="SELECT Bancos.nombre + ' - ' + CreditosFinancieros.numCredito  + ' - ' + CAST(CreditosFinancieros.monto AS varchar(50)) AS cuenta, CreditosFinancieros.creditoFinancieroID FROM Bancos INNER JOIN CreditosFinancieros ON Bancos.bancoID = CreditosFinancieros.bancoID"></asp:SqlDataSource>
        </td>
	</tr>
</table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="sdsConcentradoCreditos">
        <Columns>
            <asp:BoundField DataField="Cargos" DataFormatString="{0:c}" HeaderText="Cargos" 
                ReadOnly="True" SortExpression="Cargos">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="Abonos" DataFormatString="{0:c}" HeaderText="Abonos" 
                ReadOnly="True" SortExpression="Abonos">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="Saldo" DataFormatString="{0:c}" HeaderText="Saldo" 
                ReadOnly="True" SortExpression="Saldo">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsConcentradoCreditos" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        SelectCommand="SELECT SUM(cargo) AS Cargos, SUM(abono) AS Abonos, sum(abono) - sum(cargo) as Saldo FROM MovimientosCuentasBanco WHERE (creditoFinancieroID = @creditoFinancieroID)">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCreditoFinanciero" DefaultValue="-99" 
                Name="creditoFinancieroID" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
<br />
                            <asp:GridView ID="gridMovCuentasBanco" runat="server" 
                                AutoGenerateColumns="False" CellPadding="4" 
                                
    DataKeyNames="movbanID,fecha,abono,cargo,chequecobrado" ForeColor="Black" 
                                GridLines="None" 
    DataSourceID="sdsMovCreditosFinancieros" PageSize="100">
                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                <HeaderStyle CssClass="TableHeader" />
                                <AlternatingRowStyle BackColor="White" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                <Columns>
                                    <asp:BoundField ConvertEmptyStringToNull="False" DataField="movbanID" 
                                        HeaderText="movbanID" InsertVisible="False" ItemStyle-HorizontalAlign="Right" 
                                        ReadOnly="True" SortExpression="movbanID" Visible="False">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                        HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" ReadOnly="True" 
                                        SortExpression="fecha">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre Fiscal" ReadOnly="True" 
                                        SortExpression="nombre" />
                                    <asp:BoundField DataField="facturaOlarguillo" 
                                        HeaderText="# de Factura o Larguillo" ReadOnly="True" 
                                        SortExpression="facturaOlarguillo" />
                                    <asp:BoundField DataField="numCabezas" HeaderText="# de Cabezas" 
                                        ReadOnly="True" />
                                    <asp:BoundField DataField="concepto" HeaderText="Concepto" />
                                    <asp:BoundField DataField="catalogoMovBancoFiscal" HeaderText="Catalogo Fiscal" 
                                        ReadOnly="True" SortExpression="concepto" />
                                    <asp:BoundField DataField="subCatalogoFiscal" HeaderText="Subcatalogo fiscal" />
                                    <asp:BoundField DataField="chequeNombre" HeaderText="Nombre interno" />
                                    <asp:BoundField DataField="catalogoMovBancoInterno" HeaderText="Catalogo interno" />
                                    <asp:BoundField DataField="subCatalogoInterno" 
                                        HeaderText="Subcatalogo interno" />
                                    <asp:BoundField DataField="numCheque" HeaderText="# de Cheque" 
                                        ReadOnly="True" />
                                    <asp:BoundField DataField="cargo" DataFormatString="{0:c}" HeaderText="Cargo" 
                                        ItemStyle-HorizontalAlign="Center" ReadOnly="True" SortExpression="cargo">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="abono" DataFormatString="{0:c}" HeaderText="Abono" 
                                        ReadOnly="True" SortExpression="abono" />
                                    <asp:CheckBoxField DataField="chequecobrado" HeaderText="Cheque Cobrado" />
                                    <asp:BoundField DataField="userID" HeaderText="Usuario" ReadOnly="True" 
                                        SortExpression="Nombre" />
                                    <asp:BoundField DataField="storeTS" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" 
                                        HeaderText="Fecha de ingreso" ReadOnly="True" SortExpression="storeTS" 
                                        Visible="False">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="updateTS" HeaderText="Ultima modificacion" 
                                        ReadOnly="True" Visible="False">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="bancoCreditoFinanciero" HeaderText="Banco Credito" 
                                        Visible="False" />
                                    <asp:BoundField DataField="numCreditoFinanciero" HeaderText="# Credito" 
                                        Visible="False" />
                                </Columns>
                            </asp:GridView>
    <asp:SqlDataSource ID="sdsMovCreditosFinancieros" runat="server" 
    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
    
        SelectCommand="SELECT MovimientosCuentasBanco.movbanID, MovimientosCuentasBanco.chequeNombre, MovimientosCuentasBanco.fecha, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.abono, MovimientosCuentasBanco.cargo, MovimientosCuentasBanco.userID, MovimientosCuentasBanco.storeTS, MovimientosCuentasBanco.updateTS, MovimientosCuentasBanco.cuentaID, MovimientosCuentasBanco.ConceptoMovCuentaID, MovimientosCuentasBanco.nombre, MovimientosCuentasBanco.facturaOlarguillo, MovimientosCuentasBanco.numCabezas, MovimientosCuentasBanco.numCheque, MovimientosCuentasBanco.chequeNombre AS Expr1, MovimientosCuentasBanco.catalogoMovBancoFiscalID, MovimientosCuentasBanco.subCatalogoMovBancoFiscalID, MovimientosCuentasBanco.catalogoMovBancoInternoID, MovimientosCuentasBanco.subCatalogoMovBancoInternoID, MovimientosCuentasBanco.chequecobrado, MovimientosCuentasBanco.fechacobrado, catalogoMovimientosBancos_1.catalogoMovBanco AS catalogoMovBancoInterno, SubCatalogoMovimientoBanco.subCatalogo AS subCatalogoInterno, catalogoMovimientosBancos.catalogoMovBanco AS catalogoMovBancoFiscal, SubCatalogoMovimientoBanco_1.subCatalogo AS subCatalogoFiscal, CreditosFinancieros.creditoFinancieroID, Bancos.nombre AS bancoCreditoFinanciero, CreditosFinancieros.numCredito AS numCreditoFinanciero FROM SubCatalogoMovimientoBanco RIGHT OUTER JOIN MovimientosCuentasBanco INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID INNER JOIN catalogoMovimientosBancos ON MovimientosCuentasBanco.catalogoMovBancoFiscalID = catalogoMovimientosBancos.catalogoMovBancoID INNER JOIN catalogoMovimientosBancos AS catalogoMovimientosBancos_1 ON MovimientosCuentasBanco.catalogoMovBancoInternoID = catalogoMovimientosBancos_1.catalogoMovBancoID ON SubCatalogoMovimientoBanco.subCatalogoMovBancoID = MovimientosCuentasBanco.subCatalogoMovBancoInternoID LEFT OUTER JOIN SubCatalogoMovimientoBanco AS SubCatalogoMovimientoBanco_1 ON MovimientosCuentasBanco.subCatalogoMovBancoFiscalID = SubCatalogoMovimientoBanco_1.subCatalogoMovBancoID LEFT OUTER JOIN Bancos INNER JOIN CreditosFinancieros ON Bancos.bancoID = CreditosFinancieros.bancoID ON MovimientosCuentasBanco.creditoFinancieroID = CreditosFinancieros.creditoFinancieroID WHERE (CreditosFinancieros.creditoFinancieroID = @creditoFinancieroID) ORDER BY MovimientosCuentasBanco.fecha">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCreditoFinanciero" DefaultValue="-1" 
                Name="creditoFinancieroID" PropertyName="SelectedValue" />
        </SelectParameters>
</asp:SqlDataSource>
</asp:Content>
