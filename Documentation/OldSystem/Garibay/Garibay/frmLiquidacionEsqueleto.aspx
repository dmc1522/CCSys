<%@ Page Language="C#" Title="Imprimir Liquidacion" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmLiquidacionEsqueleto.aspx.cs" Inherits="Garibay.frmLiquidacionEsqueletoaspx" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
   <asp:Panel ID="panelEsqueleto" runat="Server">
         
        <table >
            <tr>
                <td>
                        <asp:GridView ID="gvBoletas" runat="server" AutoGenerateColumns="False" 
                            ShowFooter="True"  >
                            <Columns>
                                <asp:CommandField ButtonType="Button" EditText="Editar" ShowEditButton="True" 
                                    CausesValidation="False" DeleteText="Eliminar" ShowDeleteButton="True" />
                                <asp:BoundField DataField="boletaID" HeaderText="BoletaID" ReadOnly="True" 
                                    Visible="False" />
                                <asp:BoundField DataField="NumeroBoleta" HeaderText="BOLETA No." 
                                    ReadOnly="True">
                                    <ItemStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Ticket" HeaderText="TICKET" />
                                <asp:BoundField DataField="pesonetoentrada" DataFormatString="{0:n}" 
                                    HeaderText="KG." ItemStyle-HorizontalAlign="Right" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Producto" HeaderText="DESCRIPCIÓN" ReadOnly="True" />
                                <asp:BoundField DataField="humedad" DataFormatString="{0:n}" HeaderText="HUM." 
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="dctoHumedad" DataFormatString="{0:n}" 
                                    HeaderText="DSCTO HUM." ItemStyle-HorizontalAlign="Right" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="impurezas" DataFormatString="{0:n}" 
                                    HeaderText="IMPUREZAS" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="dctoImpurezas" DataFormatString="{0:n}" 
                                    HeaderText="DSCTO IMPUREZAS" ReadOnly="True" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="pesonetoapagar" DataFormatString="{0:n}" 
                                    HeaderText="KG NETOS" ItemStyle-HorizontalAlign="Right" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="precioapagar" DataFormatString="{0:C5}" 
                                    HeaderText="PRECIO (por KG)" ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="importe" DataFormatString="{0:c}" 
                                    HeaderText="IMPORTE" ItemStyle-HorizontalAlign="Right" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="dctoSecado" DataFormatString="{0:c}" 
                                    HeaderText="DSCTO SECADO" ItemStyle-HorizontalAlign="Right" 
                                    ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="totalapagar" DataFormatString="{0:c}" 
                                    HeaderText="TOTAL A PAGAR" ReadOnly="True" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                                            </td>
            </tr>
            <tr>
                <td>
                                                            <asp:GridView ID="gvAnticipos" 
                        runat="server" AutoGenerateColumns="False" 
                                                                DataKeyNames="anticipoID" 
                        DataSourceID="sdsAnticipos">
                                                                <Columns>
                                                                    <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                                                        HeaderText="Fecha" SortExpression="fecha">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="anticipoID" HeaderText="#" InsertVisible="False" 
                                                                        ReadOnly="True" SortExpression="anticipoID" />
                                                                    <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                                                                        SortExpression="Productor" />
                                                                    <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" ReadOnly="True" 
                                                                        SortExpression="Cuenta" Visible="False" />
                                                                    <asp:BoundField DataField="Concepto" HeaderText="Concepto" 
                                                                        SortExpression="Concepto" />
                                                                    <asp:BoundField DataField="numCheque" HeaderText="Cheque" 
                                                                        ItemStyle-HorizontalAlign="Right" SortExpression="numCheque">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Monto" DataFormatString="{0:c}" HeaderText="Monto" 
                                                                        ItemStyle-HorizontalAlign="Right" ReadOnly="True" SortExpression="Monto">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Efectivo" DataFormatString="{0:c}" 
                                                                        HeaderText="Efectivo" ItemStyle-HorizontalAlign="Right" ReadOnly="True" 
                                                                        SortExpression="Efectivo">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:TextBox ID="txtLiquidacionID" runat="server"></asp:TextBox>
                                                        </td>
            </tr>
            <tr>
                <td>
                                                            <asp:GridView ID="gvPagosLiquidacion" runat="server" 
                                                                AutoGenerateColumns="False" 
                                                                DataSourceID="sdsAnticipos" >
                                                                <Columns>
                                                                    <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                                                        HeaderText="Fecha" SortExpression="fecha" />
                                                                    <asp:BoundField DataField="productorNombre" HeaderText="Productor" ReadOnly="True" 
                                                                        SortExpression="Productor" />
                                                                    <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" ReadOnly="True" 
                                                                        SortExpression="Cuenta" Visible="False" />
                                                                    <asp:BoundField DataField="Concepto" HeaderText="Concepto" 
                                                                        SortExpression="Concepto" />
                                                                    <asp:BoundField DataField="numCheque" HeaderText="Cheque" 
                                                                        ItemStyle-HorizontalAlign="Right" SortExpression="numCheque">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Monto" DataFormatString="{0:c}" HeaderText="Monto" 
                                                                        ItemStyle-HorizontalAlign="Right" ReadOnly="True" SortExpression="Monto">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Efectivo" DataFormatString="{0:c}" 
                                                                        HeaderText="Efectivo" ItemStyle-HorizontalAlign="Right" ReadOnly="True" 
                                                                        SortExpression="Efectivo">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:SqlDataSource ID="sdsAnticipos" runat="server" 
                                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                
                                                                
                        
                                                                SelectCommand="SELECT Anticipos.anticipoID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS Productor, Bancos.nombre + ' ' + CuentasDeBanco.NumeroDeCuenta AS Cuenta, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.cargo + MovimientosCuentasBanco.abono AS Monto, MovimientosCuentasBanco.numCheque, MovimientosCaja.cargo + MovimientosCaja.abono AS Efectivo, Liquidaciones_Anticipos.LiquidacionID, Anticipos.fecha FROM Liquidaciones_Anticipos INNER JOIN Anticipos INNER JOIN Productores ON Anticipos.productorID = Productores.productorID ON Liquidaciones_Anticipos.Anticipos = Anticipos.anticipoID LEFT OUTER JOIN CuentasDeBanco INNER JOIN MovimientosCuentasBanco ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco.cuentaID INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID ON Anticipos.movbanID = MovimientosCuentasBanco.movbanID LEFT OUTER JOIN MovimientosCaja ON Anticipos.movimientoID = MovimientosCaja.movimientoID WHERE  Anticipos.tipoAnticipoID = 1
 AND  (Liquidaciones_Anticipos.LiquidacionID = @liquidacionID)">
                                                                <SelectParameters>
                                                                    <asp:ControlParameter ControlID="txtLiquidacionID" DefaultValue="-1" 
                                                                        Name="liquidacionID" PropertyName="Text" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                            <br />
                                                            <br />
                                                            <asp:DetailsView ID="dvTotalesLiq" runat="server" AutoGenerateRows="False" 
                                                                DataKeyNames="LiquidacionID" DataSourceID="sdsTotalesLiq" Height="50px" 
                                                                Width="125px">
                                                                <Fields>
                                                                    <asp:BoundField DataField="Boletas" DataFormatString="{0:C2}" 
                                                                        HeaderText="Boletas" ReadOnly="True" SortExpression="Boletas">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Anticipos" DataFormatString="{0:C2}" 
                                                                        HeaderText="Anticipos" ReadOnly="True" SortExpression="Anticipos">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Pagos_Creditos" DataFormatString="{0:C2}" 
                                                                        HeaderText="Pagos a creditos" ReadOnly="True" SortExpression="Pagos_Creditos">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Pagos" DataFormatString="{0:C2}" HeaderText="Pagos" 
                                                                        ReadOnly="True" SortExpression="Pagos">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Saldo" DataFormatString="{0:C2}" HeaderText="Saldo" 
                                                                        SortExpression="Saldo">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                </Fields>
                                                            </asp:DetailsView>
                                                            <br />
                                                            <asp:SqlDataSource ID="sdsTotalesLiq" runat="server" 
                                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                SelectCommand="SELECT LiquidacionID, Boletas, Anticipos, Pagos_Creditos, Pagos, Boletas - Anticipos - Pagos_Creditos - Pagos AS Saldo FROM vTotalesLiquidacion WHERE (LiquidacionID = @LiquidacionID)">
                                                                <SelectParameters>
                                                                    <asp:ControlParameter ControlID="txtLiquidacionID" Name="LiquidacionID" 
                                                                        PropertyName="Text" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                            <br />
                                                            <br />
                                                            <asp:GridView ID="gvCreditosEnLiquidacion" runat="server" 
                                                                AutoGenerateColumns="False" DataKeyNames="creditoID,LiquidacionID" 
                                                                DataSourceID="sdsCreditosEnLiquidacion">
                                                                <Columns>
                                                                    <asp:BoundField DataField="creditoID" HeaderText="CreditoID" ReadOnly="True" 
                                                                        SortExpression="creditoID" />
                                                                    <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                                                                        SortExpression="Productor" />
                                                                    <asp:BoundField DataField="pago" DataFormatString="{0:C2}" 
                                                                        HeaderText="Abono al credito" SortExpression="pago">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <br />
                                                            <asp:SqlDataSource ID="sdsCreditosEnLiquidacion" runat="server" 
                                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                DeleteCommand="DELETE FROM Liquidaciones_Creditos WHERE (creditoID = @creditoID)" 
                                                                SelectCommand="SELECT Liquidaciones_Creditos.creditoID, LTRIM(Productores.apaterno + SPACE(1) + Productores.amaterno + SPACE(1) + Productores.nombre) AS Productor, Liquidaciones_Creditos.pago, Liquidaciones_Creditos.LiquidacionID FROM Liquidaciones_Creditos INNER JOIN Creditos ON Liquidaciones_Creditos.creditoID = Creditos.creditoID INNER JOIN Productores ON Creditos.productorID = Productores.productorID WHERE (Liquidaciones_Creditos.LiquidacionID = @LiquidacionID)" 
                                                                UpdateCommand="UPDATE Liquidaciones_Creditos SET pago = @pago WHERE (creditoID = @creditoID)">
                                                                <SelectParameters>
                                                                    <asp:ControlParameter ControlID="txtLiquidacionID" Name="liquidacionID" 
                                                                        PropertyName="Text" />
                                                                </SelectParameters>
                                                                <DeleteParameters>
                                                                    <asp:Parameter Name="creditoID" />
                                                                </DeleteParameters>
                                                                <UpdateParameters>
                                                                    <asp:Parameter Name="pago" />
                                                                    <asp:Parameter Name="creditoID" />
                                                                </UpdateParameters>
                                                            </asp:SqlDataSource>
                                                            </td>
            </tr>
    </table>
    </asp:Panel>
    
        <asp:Panel ID="pnlResult" runat="server" >
            <asp:Image ID="imgBienResult" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
            <asp:Image ID="imgMalResult" runat="server" ImageUrl="~/imagenes/tache.jpg" />
            <asp:Label ID="lblResult" runat="server"></asp:Label>
        </asp:Panel>
    
    
   


</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    </asp:Content>
