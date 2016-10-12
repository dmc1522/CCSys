<%@ Page Language="C#" Title="Asignar anticipos a liquidación" MasterPageFile="~/MasterPage.Master"  AutoEventWireup="true" CodeBehind="frmAsignaranticiposaLiq.aspx.cs" Inherits="Garibay.frmAsignaranticiposaLiq" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

         
        <table >
            <tr>
                <td class="TableHeader">
                    LISTA DE ANTICIPOS SIN ASIGNAR A UNA LIQUIDACIÓN</td>
            </tr>
            <tr>
                <td>
                    <table >
                        <tr>
                            <td class="TablaField">
                                Productor:</td>
                            <td>
                                <asp:DropDownList ID="cmbProductor" runat="server" 
                                    DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="productorID" 
                                    Height="22px" Width="275px" AutoPostBack="True" 
                                    onselectedindexchanged="cmbProductorLiquidacion_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    
                                    
                                    
                                    SelectCommand="SELECT   DISTINCT( Anticipos.productorID), Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS name FROM Productores INNER JOIN Anticipos ON Productores.productorID = Anticipos.productorID order by name">
                                </asp:SqlDataSource>
                            </td>
                            <td>
                                <asp:Button ID="btnBackLiq" runat="server" onclick="btnBackLiq_Click" 
                                    Text="Regresar a la liquidacion" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    
                                    SelectCommand="SELECT Anticipos.anticipoID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS Productor, Bancos.nombre + ' ' + CuentasDeBanco.NumeroDeCuenta AS Cuenta, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.cargo + MovimientosCuentasBanco.abono AS Monto, MovimientosCuentasBanco.numCheque, MovimientosCaja.cargo + MovimientosCaja.abono AS Efectivo, Liquidaciones_Anticipos.LiquidacionID, TiposAnticipos.tipoAnticipo FROM TiposAnticipos INNER JOIN Anticipos INNER JOIN Productores ON Anticipos.productorID = Productores.productorID ON TiposAnticipos.tipoAnticipoID = Anticipos.tipoAnticipoID LEFT OUTER JOIN Liquidaciones_Anticipos ON Anticipos.anticipoID = Liquidaciones_Anticipos.Anticipos LEFT OUTER JOIN CuentasDeBanco INNER JOIN MovimientosCuentasBanco ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco.cuentaID INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID ON Anticipos.movbanID = MovimientosCuentasBanco.movbanID LEFT OUTER JOIN MovimientosCaja ON Anticipos.movimientoID = MovimientosCaja.movimientoID WHERE (Productores.productorID = @productorID) AND (Liquidaciones_Anticipos.LiquidacionID IS NULL)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="cmbProductor" DefaultValue="-1" 
                                            Name="productorID" PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <asp:GridView ID="gridAnticipossinasignar" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="anticipoID" DataSourceID="SqlDataSource2" 
                                    onselectedindexchanged="gridAnticipossinasignar_SelectedIndexChanged">
                                    <Columns>
                                        <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                                            ShowSelectButton="True" />
                                        <asp:BoundField DataField="anticipoID" HeaderText="# Anticipo" 
                                            InsertVisible="False" ReadOnly="True" SortExpression="anticipoID" />
                                        <asp:BoundField DataField="tipoAnticipo" HeaderText="Tipo de Anticipo" 
                                            SortExpression="tipoAnticipo" />
                                        <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                                            SortExpression="Productor" />
                                        <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" ReadOnly="True" 
                                            SortExpression="Cuenta" />
                                        <asp:BoundField DataField="Concepto" HeaderText="Concepto" 
                                            SortExpression="Concepto" />
                                        <asp:BoundField DataField="Monto" HeaderText="Monto" ReadOnly="True" 
                                            SortExpression="Monto" />
                                        <asp:BoundField DataField="numCheque" HeaderText="# Cheque" 
                                            SortExpression="numCheque" />
                                        <asp:BoundField DataField="Efectivo" HeaderText="Efectivo" ReadOnly="True" 
                                            SortExpression="Efectivo" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Button ID="btnAgregar" runat="server" CssClass="Button" 
                                    onclick="btnAgregar_Click" Text="Agregar a liquidación" />
                                <asp:Button ID="btnEliminarAnticipo" runat="server" CssClass="Button" 
                                   Text="Eliminar anticipo" onclick="btnEliminarAnticipo_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" class="TableHeader">
                                LISTA DE ANTICIPOS AGREGADOS A UNA LIQUIDACIÓN</td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                Productor:</td>
                            <td colspan="2">
                                <asp:DropDownList ID="cmbProductorLiquidacion" runat="server" 
                                    DataSourceID="SqlDataSource3" DataTextField="name" DataValueField="LiquidacionID" 
                                    Height="22px" Width="275px" AutoPostBack="True" 
                                    onselectedindexchanged="cmbProductorLiquidacion_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    
                                    
                                    
                                    
                                    SelectCommand="SELECT LTRIM(Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre) + ' - ' +  CAST(Liquidaciones.liquidacionID AS nvarchar) AS name, Liquidaciones.LiquidacionID FROM Liquidaciones INNER JOIN Productores ON Liquidaciones.productorID = Productores.productorID order by name">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="gridAnticiposAgregados" runat="server" 
                                    AutoGenerateColumns="False" DataSourceID="SqlDataSource4" 
                                    onselectedindexchanged="gridAnticiposAgregados_SelectedIndexChanged" 
                                    DataKeyNames="anticipoID">
                                    <Columns>
                                        <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                                            ShowSelectButton="True" />
                                        <asp:BoundField DataField="anticipoID" HeaderText="# Anticipo" 
                                            InsertVisible="False" ReadOnly="True" SortExpression="anticipoID" />
                                        <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                                            SortExpression="Productor" />
                                        <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" ReadOnly="True" 
                                            SortExpression="Cuenta" />
                                        <asp:BoundField DataField="Concepto" HeaderText="Concepto" 
                                            SortExpression="Concepto" />
                                        <asp:BoundField DataField="Monto" HeaderText="Monto" ReadOnly="True" 
                                            SortExpression="Monto" />
                                        <asp:BoundField DataField="numCheque" HeaderText="# Cheque" 
                                            SortExpression="numCheque" />
                                        <asp:BoundField DataField="Efectivo" HeaderText="Efectivo" ReadOnly="True" 
                                            SortExpression="Efectivo" />
                                        <asp:BoundField DataField="tipoAnticipo" HeaderText="Tipo de Anticipo" 
                                            SortExpression="tipoAnticipo" />
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    
                                    
                                    SelectCommand="SELECT Anticipos.anticipoID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS Productor, Bancos.nombre + ' ' + CuentasDeBanco.NumeroDeCuenta AS Cuenta, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.cargo + MovimientosCuentasBanco.abono AS Monto, MovimientosCuentasBanco.numCheque, MovimientosCaja.cargo + MovimientosCaja.abono AS Efectivo, Liquidaciones_Anticipos.LiquidacionID, TiposAnticipos.tipoAnticipo FROM Liquidaciones INNER JOIN Liquidaciones_Anticipos ON Liquidaciones.LiquidacionID = Liquidaciones_Anticipos.LiquidacionID RIGHT OUTER JOIN TiposAnticipos INNER JOIN Anticipos INNER JOIN Productores ON Anticipos.productorID = Productores.productorID ON TiposAnticipos.tipoAnticipoID = Anticipos.tipoAnticipoID ON Liquidaciones_Anticipos.Anticipos = Anticipos.anticipoID LEFT OUTER JOIN CuentasDeBanco INNER JOIN MovimientosCuentasBanco ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco.cuentaID INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID ON Anticipos.movbanID = MovimientosCuentasBanco.movbanID LEFT OUTER JOIN MovimientosCaja ON Anticipos.movimientoID = MovimientosCaja.movimientoID WHERE (Liquidaciones_Anticipos.LiquidacionID = @liquidacionID) AND (Liquidaciones.cobrada = 0)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="cmbProductorLiquidacion" Name="liquidacionID" 
                                            PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Button ID="btnRemover" runat="server" CssClass="Button" 
                                    onclick="btnRemover_Click" Text="Quitar de la liquidación" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
    </table>


</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    </asp:Content>
