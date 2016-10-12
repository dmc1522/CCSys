<%@ Page Language="C#" Title= "Estado de cuenta de crédito" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmPrintEstadodeCuentaCredito.aspx.cs" Inherits="Garibay.frmPrintEstadodeCuentaCredito" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

         
        <table>
            <tr>
                <td colspan="6" align="center" class="TableHeader">
                    ESTADO DE CUENTA DEL CRÉDITO</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Al día de:</td>
                <td colspan="5">
                    <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
                    <rjs:PopCalendar ID="PopCalendar3" runat="server" Separator="/" 
                        Control="txtFecha" AutoPostBack="True" />
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Crédito:</td>
                <td colspan="5">
   		 	   			<asp:DropDownList ID="ddlCredito" runat="server"  
                                DataSourceID="SqlCreditos" DataTextField="Credito" 
                                DataValueField="creditoID" AutoPostBack="True" 
                            onselectedindexchanged="ddlCredito_SelectedIndexChanged">
                           </asp:DropDownList>
   		 	   		    <asp:SqlDataSource ID="SqlCreditos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            SelectCommand="SELECT DISTINCT CAST(dbo.Creditos.creditoID AS Varchar) + ' - ' + dbo.Productores.apaterno + ' ' + dbo.Productores.amaterno + ' ' + dbo.Productores.nombre AS Credito, dbo.Productores.apaterno + ' ' + dbo.Productores.amaterno + ' ' + dbo.Productores.nombre AS Productor, dbo.Creditos.creditoID FROM dbo.Creditos INNER JOIN dbo.Productores ON dbo.Creditos.productorID = dbo.Productores.productorID">
                        </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Población:</td>
                <td>
                    <asp:TextBox ID="txtPoblacion" runat="server" Width="296px"></asp:TextBox>
                </td>
                <td class="TablaField">
                    Teléfono:</td>
                <td>
                    <asp:TextBox ID="txtTelefono" runat="server" Width="229px"></asp:TextBox>
                </td>
                <td rowspan="2" class="TablaField">
                    Garantías:</td>
                <td rowspan="2">
                    <asp:TextBox ID="txtGarantias" runat="server" TextMode="MultiLine" 
                        Width="239px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Dirección:</td>
                <td>
                    <asp:TextBox ID="txtDireccion" runat="server" Width="293px"></asp:TextBox>
                </td>
                <td class="TablaField">
                    Autorizado por:</td>
                <td>
                    <asp:TextBox ID="txtAutorizadopor" runat="server" Width="228px"></asp:TextBox>
                </td>
            </tr>
        </table>

         
        <table>
            <tr>
                <td align="center" class="TableHeader">
                    ESTADO GENERAL</td>
            </tr>
            <tr>
                <td align="center" class="TableHeader">
                    <asp:DetailsView ID="dvEstadoGeneral" runat="server" AutoGenerateRows="False" 
                        DataSourceID="sdsEstadoGeneral" Height="50px" Width="229px" 
                        >
                        <Fields>
                            <asp:BoundField DataField="LimitedeCredito" DataFormatString="{0:c2}" 
                                HeaderText="Limite de Crédito" SortExpression="LimitedeCredito">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Font-Bold="True" Font-Size="X-Large" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalNotas" DataFormatString="{0:c2}" 
                                HeaderText="Notas (+)" SortExpression="TotalNotas">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Font-Bold="True" Font-Size="X-Large" HorizontalAlign="Right" />
                            </asp:BoundField>
<asp:BoundField DataField="TotalPrestamos" DataFormatString="{0:C2}" HeaderText="Préstamos (+)" 
                                SortExpression="TotalPrestamos">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Font-Bold="True" Font-Size="X-Large" HorizontalAlign="Right" />
</asp:BoundField>
<asp:BoundField DataField="TotalSeguro" DataFormatString="{0:c2}" HeaderText="Seguro (+)" 
                                SortExpression="TotalSeguro">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Font-Bold="True" Font-Size="X-Large" HorizontalAlign="Right" />
</asp:BoundField>
                            <asp:BoundField DataField="TotalIntereses" DataFormatString="{0:c2}" 
                                HeaderText="Intereses (+)" SortExpression="TotalIntereses">
                                 <ItemStyle Font-Bold="True" Font-Size="X-Large" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Left" />
</asp:BoundField>
                          
                            <asp:BoundField DataField="TotalAbonado" DataFormatString="{0:c2}" 
                                HeaderText="Abonos (-)" SortExpression="TotalAbonado">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Font-Bold="True" Font-Size="X-Large" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalDebe" DataFormatString="{0:c2}" 
                                HeaderText="Saldo Actual (=)" SortExpression="TotalDebe">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Font-Bold="True" Font-Size="X-Large" HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Font-Bold="True" Font-Size="X-Large" HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Fields>
                    </asp:DetailsView>
                    <asp:SqlDataSource ID="sdsEstadoGeneral" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="ReturnEstadodeCuentatOTALES" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlCredito" Name="creditoID" 
                                PropertyName="SelectedValue" Type="Int32" />
                            <asp:ControlParameter ControlID="txtFecha" Name="fechafin" PropertyName="Text" 
                                Type="DateTime" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            </table>
            


                    <table >
                        <tr>
                            <td align="center" class="TableHeader">
                                DETALLE DE PRODUCTOS COMPRADOS</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gridProductos" runat="server" AutoGenerateColumns="False" 
                                    DataSourceID="sdsProductos" DataKeyNames="Fecha" 
                                    ondatabound="gridProductos_DataBound">
                                    <Columns>
                                        <asp:BoundField DataField="notadeventaID" HeaderText="# Nota" 
                                            SortExpression="notadeventaID">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                            HeaderText="Fecha" SortExpression="Fecha" />
                                        <asp:BoundField DataField="Producto" HeaderText="Producto" ReadOnly="True" 
                                            SortExpression="Producto" />
                                        <asp:BoundField DataField="cantidad" DataFormatString="{0:n3}" 
                                            HeaderText="Cantidad" SortExpression="cantidad">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="precio" DataFormatString="{0:c2}" 
                                            HeaderText="Precio" SortExpression="precio">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Monto" DataFormatString="{0:c2}" HeaderText="Monto" 
                                            ReadOnly="True" SortExpression="Monto">
                                        <ItemStyle HorizontalAlign="Right" Font-Bold="True" Font-Size="X-Large" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdsProductos" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" SelectCommand="SELECT Notasdeventa.Fecha, Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Producto, NotasdeVenta_detalle.cantidad, NotasdeVenta_detalle.precio, NotasdeVenta_detalle.cantidad * NotasdeVenta_detalle.precio AS Monto, NotasdeVenta_detalle.notadeventaID FROM Notasdeventa INNER JOIN NotasdeVenta_detalle ON Notasdeventa.notadeventaID = NotasdeVenta_detalle.notadeventaID INNER JOIN Productos ON NotasdeVenta_detalle.productoID = Productos.productoID INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID WHERE (Notasdeventa.creditoID = @creditoID)
order by Notasdeventa.Fecha">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ddlCredito" DefaultValue="-1" Name="creditoID" 
                                            PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
        </table>
        
        <table>
            <tr>
                <td class="TableHeader">
                    DETALLE DE SEGURO CONTRADADO</td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="SDSSEGURO">
                        <Columns>
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" 
                                SortExpression="Nombre" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" 
                                SortExpression="Descripcion" />
                            <asp:BoundField DataField="CostoTotalSeguro" DataFormatString="{0:c2}" 
                                HeaderText="Costo" SortExpression="CostoTotalSeguro">
                            <ItemStyle Font-Bold="True" Font-Size="X-Large" HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SDSSEGURO" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
                        SelectCommand="SELECT dbo.SegurosAgricolas.Nombre, dbo.SegurosAgricolas.Descripcion, dbo.solicitud_SeguroAgricola.CostoTotalSeguro FROM dbo.solicitud_SeguroAgricola INNER JOIN dbo.Solicitudes ON dbo.solicitud_SeguroAgricola.solicitudID = dbo.Solicitudes.solicitudID INNER JOIN dbo.SegurosAgricolas ON dbo.solicitud_SeguroAgricola.seguroID = dbo.SegurosAgricolas.seguroID WHERE (dbo.Solicitudes.creditoID = @creditoID)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlCredito" DefaultValue="-1" Name="creditoID" 
                                PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
        
        <table>
            <tr>
                <td align="center" class="TableHeader">
                    CÁLCULO DE INTERES</td>
            </tr>
            <tr>
                <td align="center" class="TableHeader">
                    <asp:Button ID="btnAddPago" runat="server" onclick="btnAddPago_Click" 
                        Text="Agregar Pago a Crédito" />
                </td>
            </tr>
            <tr>
                <td>
            


                    <asp:GridView ID="gridEstadodeCuenta" runat="server" 
                        AutoGenerateColumns="False" ondatabound="gridEstadodeCuenta_DataBound" 
                        DataSourceID="sdsEstadodeCuenta">
                        <Columns>
                            <asp:BoundField DataField="mes" HeaderText="MES" />
                            <asp:BoundField HeaderText="Fecha" DataField="fecha" 
                                DataFormatString="{0:dd/MM/yyy}" />
<asp:BoundField DataField="concepto" HeaderText="Concepto"></asp:BoundField>
                            <asp:BoundField HeaderText="Monto. Crédito" DataField="montocredito" 
                                DataFormatString="{0:$#,##0.00;($#,##0.00);}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Fecha de pago" DataField="fechadePago" 
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField HeaderText="Días" DataField="totaldias" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Interes" DataField="intereses" 
                                DataFormatString="{0:$#,##0.00;($#,##0.00);}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="totaldebe" DataFormatString="{0:$#,##0.00;($#,##0.00);}" 
                                HeaderText="Debe" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="abonado" DataFormatString="{0:$#,##0.00;($#,##0.00);}" 
                                HeaderText="Abono" NullDisplayText="-" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="totaldebe" DataFormatString="{0:$#,##0.00;($#,##0.00);}" 
                                HeaderText="Total Debe" >
                            <ItemStyle Font-Bold="True" Font-Size="X-Large" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descpago" HeaderText="Desc. Pago" />
                        </Columns>
                    </asp:GridView>
                

                    <asp:SqlDataSource ID="sdsEstadodeCuenta" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="ReturnEstadodeCuenta" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlCredito" Name="creditoID" 
                                PropertyName="SelectedValue" Type="Int32" />
                            <asp:ControlParameter ControlID="txtFecha" Name="fechafin" PropertyName="Text" 
                                Type="DateTime" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                

                </td>
            </tr>
        </table>
                

</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    </asp:Content>

