<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" Title ="Busqueda General" AutoEventWireup="true" CodeBehind="frmBusquedaGeneral.aspx.cs" Inherits="Garibay.frmBusquedaGeneral" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

      
        <table>
            <tr>
                <td class="TableHeader" align="center">
                    BÚSQUEDA GENERAL RÁPIDA</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Palabra a buscar:<asp:TextBox ID="txtPalabraaBuscar" runat="server"></asp:TextBox>
                    <asp:Button ID="btnFind" runat="server" Text="Buscar" onclick="btnFind_Click" />
                </td>
            </tr>
            <tr>
                <td>
                        <asp:CheckBoxList ID="cblColToShow" runat="server" RepeatColumns="6">
                            <asp:ListItem>Productores</asp:ListItem>
                            <asp:ListItem>Movimientos de Banco</asp:ListItem>
                            <asp:ListItem>Movimientos de Caja</asp:ListItem>
                            <asp:ListItem>Liquidaciones</asp:ListItem>
                            <asp:ListItem>Notas de Venta</asp:ListItem>
                            <asp:ListItem>Notas de Compra</asp:ListItem>
                            <asp:ListItem>Soluciones y créditos</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
            </tr>
            <tr>
                <td>
                        <asp:Button ID="btnActualizaGrids" runat="server" 
                            onclick="btnActualizaGrids_Click" Text="Seleccionar todas las columnas" />
                        <asp:Button ID="btnQuiterSeleccion" runat="server" 
                            onclick="btnQuiterSeleccion_Click" Text="Quitar selección a columnas" />
                    </td>
            </tr>
        </table>
        <asp:Panel id = "pnlProductores" runat="Server" >
        <table>
            <tr>
                <td align="center" class="TableHeader">
                    BÚSQUEDA EN DATOS PRODUCTORES</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="gridProductores" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="productorID" DataSourceID="sdsProductores" 
                        onrowdatabound="gridProductores_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="productorID" HeaderText="# Productor" 
                                InsertVisible="False" ReadOnly="True" SortExpression="productorID" />
                            <asp:BoundField DataField="nombreprod" HeaderText="Nombre " ReadOnly="True" 
                                SortExpression="nombreprod" />
                            <asp:TemplateField HeaderText="Abrir" InsertVisible="False">
                                <EditItemTemplate>
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HPProductor" runat="server">Abrir</asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsProductores" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT distinct productorID, apaterno + ' ' + amaterno +' '  + nombre AS nombreprod FROM Productores WHERE (nombre LIKE @nombre OR amaterno LIKE @nombre OR apaterno LIKE @nombre)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtPalabraaBuscar" DefaultValue="&quot;&quot;" 
                                Name="nombre" PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
        </asp:Panel>
        <asp:Panel id = "pnlMovimientosBanco" runat="Server">
        <table>
            <tr>
                <td align="center" class="TableHeader">
                    BÚSQUEDA EN MOVIMIENTOS DE BANCO</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="gridMovBanco" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="movbanID" DataSourceID="sdsMovBancos" 
                        onrowdatabound="gridMovBanco_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="movbanID" HeaderText="# Mov" InsertVisible="False" 
                                ReadOnly="True" SortExpression="movbanID" />
                            <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                HeaderText="Fecha" SortExpression="fecha" />
                            <asp:BoundField DataField="numCheque" HeaderText="# Cheque" 
                                SortExpression="numCheque" />
                            <asp:BoundField DataField="cuenta" HeaderText="Cuenta" ReadOnly="True" 
                                SortExpression="cuenta" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre Fiscal" 
                                SortExpression="nombre" />
                            <asp:BoundField DataField="chequeNombre" HeaderText="Nombre Interno" 
                                SortExpression="chequeNombre" />
                            <asp:BoundField DataField="cargo" DataFormatString="{0:c2}" HeaderText="Cargo" 
                                SortExpression="cargo">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="abono" DataFormatString="{0:c2}" HeaderText="Abono" 
                                SortExpression="abono">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Abrir" InsertVisible="False">
                                <EditItemTemplate>
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HPMovBanco" runat="server">Abrir</asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsMovBancos" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
                        SelectCommand="SELECT MovimientosCuentasBanco.movbanID, MovimientosCuentasBanco.numCheque, MovimientosCuentasBanco.fecha,  Bancos.nombre + ' ' + CAST(CuentasDeBanco.NumeroDeCuenta AS VarChar) AS cuenta, MovimientosCuentasBanco.nombre, MovimientosCuentasBanco.chequeNombre, MovimientosCuentasBanco.cargo, MovimientosCuentasBanco.abono, MovimientosCuentasBanco.observaciones FROM MovimientosCuentasBanco INNER JOIN CuentasDeBanco ON MovimientosCuentasBanco.cuentaID = CuentasDeBanco.cuentaID INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID WHERE (MovimientosCuentasBanco.nombre LIKE @nombre) OR MovimientosCuentasBanco.cargo LIKE @nombre OR MovimientosCuentasBanco.abono LIKE @nombre OR MovimientosCuentasBanco.observaciones LIKE @nombre OR MovimientosCuentasBanco.numCheque LIKE @nombre">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtPalabraaBuscar" DefaultValue="" 
                                Name="nombre" PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
        </asp:Panel>
        <asp:Panel id = "pnlMovimientosCaja" runat="Server">
       
        <table>
            <tr>
                <td align="center" class="TableHeader">
                    BÚSQUEDA EN MOVIMIENTOS DE CAJA</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="gridCaja" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="movimientoID" DataSourceID="sdsMovCaja" 
                        onrowdatabound="gridCaja_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="movimientoID" HeaderText="#Movimiento" 
                                InsertVisible="False" ReadOnly="True" SortExpression="movimientoID" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" 
                                SortExpression="nombre" />
                            <asp:BoundField DataField="cargo" DataFormatString="{0:c2}" HeaderText="Cargo" 
                                SortExpression="cargo">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="abono" DataFormatString="{0:c2}" HeaderText="Abono" 
                                SortExpression="abono" />
                            <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" 
                                SortExpression="Observaciones" />
                            <asp:TemplateField HeaderText="Abrir" InsertVisible="False">
                                <EditItemTemplate>
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HPMovCaja" runat="server">Abrir</asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsMovCaja" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT movimientoID, nombre, cargo, abono, Observaciones FROM MovimientosCaja WHERE (nombre LIKE @nombre) OR (abono LIKE @nombre) OR (cargo LIKE @nombre) OR (observaciones LIKE @nombre)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtPalabraaBuscar" Name="nombre" 
                                PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
        </asp:Panel>
        <asp:Panel id="pnlLiquidaciones" runat="Server">
        <table>
            <tr>
                <td align="center" colspan="2" class="TableHeader">
                    BÚSQUEDA EN LIQUIDACIONES</td>
            </tr>       
            <tr>
                <td align="center" colspan="2">
                    <asp:GridView ID="gridLiquidaciones" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="LiquidacionID" DataSourceID="sdsLiquidaciones" 
                        onrowdatabound="gridLiquidaciones_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="LiquidacionID" HeaderText="# Liquidacion" 
                                InsertVisible="False" ReadOnly="True" SortExpression="LiquidacionID" />
                            <asp:BoundField DataField="NameProd" HeaderText="Productor" ReadOnly="True" 
                                SortExpression="NameProd" />
                            <asp:BoundField DataField="total" DataFormatString="{0:c2}" HeaderText="Total" 
                                SortExpression="total">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Abrir" InsertVisible="False">
                                <EditItemTemplate>
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HPliquidacion" runat="server">Abrir</asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsLiquidaciones" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT Liquidaciones.LiquidacionID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS NameProd, Liquidaciones.total FROM Liquidaciones INNER JOIN Productores ON Liquidaciones.productorID = Productores.productorID WHERE (Productores.apaterno LIKE @nombre) OR (Productores.amaterno LIKE @nombre) OR (Productores.nombre LIKE @nombre) OR (Liquidaciones.total LIKE @nombre) OR (Liquidaciones.LiquidacionID LIKE @nombre)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtPalabraaBuscar" Name="nombre" 
                                PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
        </asp:Panel>
        <asp:Panel id="pnlNotasdeVenta" runat="Server">
        <table>
            <tr>
                <td align="center" colspan="2" class="TableHeader">
                    BÚSQUEDA EN NOTAS DE VENTA</td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:GridView ID="gridNotasdeVenta" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="notadeventaID" DataSourceID="sdsNotas" 
                        onrowdatabound="gridNotasdeVenta_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="notadeventaID" HeaderText="# Nota de venta" 
                                InsertVisible="False" ReadOnly="True" SortExpression="notadeventaID" />
                            <asp:BoundField DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                HeaderText="Fecha" SortExpression="Fecha" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="True" 
                                SortExpression="Nombre" />
                            <asp:BoundField DataField="Subtotal" DataFormatString="{0:C2}" 
                                HeaderText="Subtotal" SortExpression="Subtotal">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Iva" DataFormatString="{0:C2}" HeaderText="Iva" 
                                SortExpression="Iva">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Total" DataFormatString="{0:C2}" HeaderText="Total" 
                                SortExpression="Total">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" 
                                SortExpression="Observaciones" />
                            <asp:TemplateField HeaderText="Abrir">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HPNotadeVenta" runat="server">Abrir</asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsNotas" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT Notasdeventa.notadeventaID, Notasdeventa.Fecha, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS Nombre, Notasdeventa.Subtotal, Notasdeventa.Iva, Notasdeventa.Total, Notasdeventa.Observaciones FROM Notasdeventa INNER JOIN Productores ON Notasdeventa.productorID = Productores.productorID WHERE (Notasdeventa.notadeventaID LIKE @nombre) OR (Notasdeventa.Fecha LIKE @nombre) OR (Productores.apaterno LIKE @nombre) OR (Productores.amaterno LIKE @nombre) OR (Productores.nombre LIKE @nombre) OR (Notasdeventa.Subtotal LIKE @nombre) OR (Notasdeventa.Iva LIKE @nombre) OR (Notasdeventa.Total LIKE @nombre) OR (Notasdeventa.Observaciones LIKE @nombre)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtPalabraaBuscar" Name="nombre" 
                                PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
        </asp:Panel>
        <asp:Panel id="pnlNotasdeCompra" runat="Server">
        <table>
            <tr>
                <td align="center" colspan="2" class="TableHeader">
                    BÚSQUEDA EN NOTAS DE COMPRA</td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:GridView ID="gridNotasdeCompra" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="notadecompraID" DataSourceID="sdsNotasCompra" 
                        onrowdatabound="gridNotasdeCompra_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="notadecompraID" HeaderText="# Nota de Compra" 
                                InsertVisible="False" ReadOnly="True" SortExpression="notadecompraID" />
                            <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                HeaderText="Fecha" SortExpression="fecha" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" 
                                SortExpression="Nombre" />
                            <asp:BoundField DataField="subtotal" DataFormatString="{0:c2}" 
                                HeaderText="Subtotal" SortExpression="subtotal">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IVA" DataFormatString="{0:C2}" HeaderText="IVA" 
                                SortExpression="IVA">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="total" DataFormatString="{0:c2}" HeaderText="Total" 
                                SortExpression="total">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Abrir">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HPNotadeCompra" runat="server">Abrir</asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsNotasCompra" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT NotasDeCompra.notadecompraID, NotasDeCompra.fecha, Proveedores.Nombre, NotasDeCompra.subtotal, NotasDeCompra.IVA, NotasDeCompra.total FROM NotasDeCompra INNER JOIN Proveedores ON NotasDeCompra.proveedorID = Proveedores.proveedorID WHERE (NotasDeCompra.notadecompraID LIKE @nombre) OR (NotasDeCompra.fecha LIKE @nombre) OR (Proveedores.Nombre LIKE @nombre) OR (NotasDeCompra.subtotal LIKE @nombre) OR (NotasDeCompra.IVA LIKE @nombre) OR (NotasDeCompra.total LIKE @nombre)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtPalabraaBuscar" Name="nombre" 
                                PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
        </asp:Panel>
        <asp:Panel id="pnlSolicitudesyCreditos" runat="Server">
        <table>
            <tr>
                <td align="center" colspan="2" class="TableHeader">
                    BÚSQUEDA EN SOLICITUDES Y CRÉDITOS</td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:GridView ID="gridSolyCreditos" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="solicitudID,creditoID" DataSourceID="sdsCreditos" 
                        onrowdatabound="gridSolyCreditos_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="solicitudID" HeaderText="# Solicitud" 
                                InsertVisible="False" ReadOnly="True" SortExpression="solicitudID" />
                            <asp:BoundField DataField="creditoID" HeaderText="# Crédito" 
                                SortExpression="creditoID" />
                            <asp:BoundField DataField="NombreProd" HeaderText="Productor" ReadOnly="True" 
                                SortExpression="NombreProd" />
                            <asp:BoundField DataField="Superficieasembrar" DataFormatString="{0:n2}" 
                                HeaderText="Superficie a sembrar" SortExpression="Superficieasembrar">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Monto" DataFormatString="{0:c2}" HeaderText="Monto" 
                                SortExpression="Monto">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Abrir Sol." InsertVisible="False">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HPSolicitud" runat="server">Abrir</asp:HyperLink>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Abrir crédito" InsertVisible="False">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HPCredito" runat="server">Abrir</asp:HyperLink>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsCreditos" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT Solicitudes.solicitudID, Solicitudes.creditoID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS NombreProd, Solicitudes.Superficieasembrar, Solicitudes.Monto FROM Productores INNER JOIN Solicitudes ON Productores.productorID = Solicitudes.productorID WHERE (Solicitudes.solicitudID LIKE @nombre) OR (Solicitudes.creditoID LIKE @nombre) OR (Productores.apaterno LIKE @nombre) OR (Productores.amaterno LIKE @nombre) OR (Productores.nombre LIKE @nombre) OR (Solicitudes.Superficieasembrar LIKE @nombre) OR (Solicitudes.Monto LIKE @nombre)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtPalabraaBuscar" Name="nombre" 
                                PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
    </table>
    </asp:Panel>


</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    </asp:Content>

