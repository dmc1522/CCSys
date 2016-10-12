<%@ Page Title="Lista de Anticipos" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListaAnticipos.aspx.cs" Inherits="Garibay.frmListaAnticipos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="TablaField">Ciclo:</td><td>
                <asp:DropDownList ID="ddlCiclos" runat="server" DataSourceID="sdsCiclos" 
                    DataTextField="CicloName" DataValueField="cicloID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT        cicloID, CicloName
FROM            Ciclos
WHERE cerrado=@cerrado
ORDER BY fechaInicio DESC">
					<SelectParameters>
						<asp:Parameter DefaultValue="FALSE" Name="cerrado" />
					</SelectParameters>
                </asp:SqlDataSource>
                <asp:Button ID="btnActualizar" runat="server" onclick="btnActualizar_Click" 
                    Text="Actualizar" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvAnticipos" runat="server" AutoGenerateColumns="False" 
    DataKeyNames="anticipoID" DataSourceID="sdsAnticipos">
        <Columns>
            <asp:BoundField DataField="anticipoID" HeaderText="anticipoID" 
                InsertVisible="False" ReadOnly="True" SortExpression="anticipoID" />
            <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                SortExpression="Productor" />
            <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" ReadOnly="True" 
                SortExpression="Cuenta" />
            <asp:BoundField DataField="Concepto" HeaderText="Concepto" 
                SortExpression="Concepto" />
            <asp:BoundField DataField="numCheque" HeaderText="numCheque" 
                SortExpression="numCheque">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="Monto" DataFormatString="{0:C2}" HeaderText="Monto" 
                ReadOnly="True" SortExpression="Monto">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="Efectivo" DataFormatString="{0:C2}" 
                HeaderText="Efectivo" ReadOnly="True" SortExpression="Efectivo">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="tipoAnticipo" HeaderText="tipoAnticipo" 
                SortExpression="tipoAnticipo" />
            <asp:CheckBoxField DataField="cobrada" HeaderText="Cobrada" 
                SortExpression="cobrada">
            <ItemStyle HorizontalAlign="Center" />
            </asp:CheckBoxField>
            <asp:TemplateField HeaderText="En Liquidacion" InsertVisible="False" 
                SortExpression="LiquidacionID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("LiquidacionID") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="lnkAbrirLiq" runat="server" 
                        NavigateUrl='<%# GetURLToOpenLiq(Eval("LiquidacionID").ToString()) %>' Text='<%# Eval("LiquidacionID") %>' ></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsAnticipos" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    
                                    
                                    
        
    
        SelectCommand="SELECT Anticipos.anticipoID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS Productor, Bancos.nombre + ' ' + CuentasDeBanco.NumeroDeCuenta AS Cuenta, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.cargo + MovimientosCuentasBanco.abono AS Monto, MovimientosCuentasBanco.numCheque, MovimientosCaja.cargo + MovimientosCaja.abono AS Efectivo, TiposAnticipos.tipoAnticipo, Productores.apaterno, Productores.amaterno, Productores.nombre, Liquidaciones.LiquidacionID, Liquidaciones.cobrada FROM Liquidaciones INNER JOIN Liquidaciones_Anticipos ON Liquidaciones.LiquidacionID = Liquidaciones_Anticipos.LiquidacionID RIGHT OUTER JOIN TiposAnticipos INNER JOIN Anticipos INNER JOIN Productores ON Anticipos.productorID = Productores.productorID ON TiposAnticipos.tipoAnticipoID = Anticipos.tipoAnticipoID ON Liquidaciones_Anticipos.Anticipos = Anticipos.anticipoID LEFT OUTER JOIN CuentasDeBanco INNER JOIN MovimientosCuentasBanco ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco.cuentaID INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID ON Anticipos.movbanID = MovimientosCuentasBanco.movbanID LEFT OUTER JOIN MovimientosCaja ON Anticipos.movimientoID = MovimientosCaja.movimientoID WHERE (Anticipos.cicloID = @cicloID) ORDER BY Productor ASC">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCiclos" DefaultValue="-1" 
                                            Name="cicloID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
