<%@ Page Language="C#" Title= "Estado de cuenta de crédito" AutoEventWireup="true" CodeBehind="frmEstadodeCuentaCreditoPrint.aspx.cs" Inherits="Garibay.frmEstadodeCuentaCreditoPrint" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         
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
                    <asp:TextBox ID="txtFechaQuery" runat="server" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    CICLO:</td>
                <td colspan="5">
   		 	   			<asp:DropDownList ID="ddlCiclos" runat="server" AutoPostBack="True" 
                            DataSourceID="sdsCiclos" DataTextField="CicloName" DataValueField="cicloID" 
                            onselectedindexchanged="ddlCiclos_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [fechaInicio] DESC">
                        </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Crédito:</td>
                <td colspan="5">
   		 	   			<asp:DropDownList ID="ddlCredito" runat="server"  
                                DataSourceID="SqlCreditos" DataTextField="Productor" 
                                DataValueField="creditoID" AutoPostBack="True" 
                            onselectedindexchanged="ddlCredito_SelectedIndexChanged" 
                            style="height: 22px">
                           </asp:DropDownList>
   		 	   		    <asp:SqlDataSource ID="SqlCreditos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            
                            
                            
                            
                            SelectCommand="SELECT  dbo.Creditos.creditoID,  dbo.Productores.apaterno + ' ' + dbo.Productores.amaterno + ' ' + dbo.Productores.nombre + ' - '  + cast(dbo.Creditos.creditoID AS Varchar) AS Productor FROM dbo.Creditos INNER JOIN dbo.Productores ON dbo.Creditos.productorID = dbo.Productores.productorID WHERE (dbo.Creditos.cicloID = @cicloID) ORDER BY Productor">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlCiclos" DefaultValue="0" Name="cicloID" 
                                    PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                <b>
                    <asp:GridView ID="gvCreditoData" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="creditoID" DataSourceID="sdsCreditoData">
                        <Columns>
                            <asp:BoundField DataField="domicilio" HeaderText="Domicilio" 
                                SortExpression="domicilio" />
                            <asp:BoundField DataField="poblacion" HeaderText="Poblacion" 
                                SortExpression="poblacion" />
                            <asp:BoundField DataField="municipio" HeaderText="Municipio" 
                                SortExpression="municipio" />
                            <asp:BoundField DataField="CURP" HeaderText="CURP" SortExpression="CURP" />
                            <asp:BoundField DataField="telefono" HeaderText="Tel" 
                                SortExpression="telefono" />
                        </Columns>
                    </asp:GridView>
                </b>
                    <asp:SqlDataSource ID="sdsCreditoData" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT Creditos.creditoID, LTRIM(Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre) AS Productor, Productores.domicilio, Productores.poblacion, Productores.municipio, Productores.CURP, Productores.telefono FROM Creditos INNER JOIN Productores ON Creditos.productorID = Productores.productorID WHERE (Creditos.creditoID = @creditoID)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlCredito" Name="creditoID" 
                                PropertyName="SelectedValue" />
                        </SelectParameters>
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
                <td rowspan="3" class="TablaField">
                    Garantías:</td>
                <td rowspan="3">
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
            <tr>
                <td colspan="4">
                    <asp:Button ID="btnPrint" runat="server" Text="Imprimir Estado de Cuenta" 
                        onclick="btnPrint_Click" />
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
                            <asp:BoundField DataField="TotalNotasConInteres" DataFormatString="{0:c2}" 
                                HeaderText="Notas con Interés (+)" SortExpression="TotalNotasConInteres">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Font-Bold="True" Font-Size="X-Large" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalNotasSinInteres" DataFormatString="{0:c2}" 
                                HeaderText="Notas sin interés (+)" SortExpression="TotalNotasSinInteres">
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
                          
                            <asp:BoundField DataField="TotalDescuentos" DataFormatString="{0:c2}" 
                                HeaderText="Descuentos de interés (-)" SortExpression="TotalDescuentos">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Font-Bold="True" Font-Size="X-Large" HorizontalAlign="Right" />
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
                            <asp:ControlParameter ControlID="txtFechaQuery" Name="fechafin" PropertyName="Text" 
                                Type="DateTime" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            </table>
            


                    <table >
                        <tr>
                            <td align="center" class="TableHeader">
                                DETALLE DE FERTILIZANTES COMPRADOS</td>
                        </tr>
                        <tr>
                            <td align="center" class="TableHeader">
                                                                    <asp:GridView ID="grdvProNotasVenta0" runat="server" 
                                                                        AutoGenerateColumns="False" DataKeyNames="NDVdetalleID,notadeventaID,productoID" 
																		DataSourceID="sdsFertilizantes">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="productoID" 
                                                                                HeaderText="productoID" SortExpression="productoID" Visible="False" 
                                                                                ReadOnly="True" />
                                                                            <asp:BoundField DataField="Nombre" HeaderText="Producto" ReadOnly="True" >
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="fecha" HeaderText="Fecha" 
																				DataFormatString="{0:dd/MM/yyyy}" SortExpression="fecha" ReadOnly="True" />
                                                                            <asp:BoundField DataField="Presentacion" 
                                                                                HeaderText="Presentacion" SortExpression="Presentacion" ReadOnly="True" >
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="cantidad" DataFormatString="{0:n2}" 
                                                                                HeaderText="Cantidad" SortExpression="cantidad">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
																			<asp:BoundField DataField="Toneladas" HeaderText="Toneladas" ReadOnly="True" 
																				SortExpression="Toneladas" DataFormatString="{0:n3}" >
																			    <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
																			<asp:BoundField DataField="precio" DataFormatString="{0:c2}" 
                                                                                HeaderText="Precio" SortExpression="precio" >
																			<ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
																			<asp:BoundField DataField="Importe" DataFormatString="{0:c2}" 
																				HeaderText="Importe" ReadOnly="True" SortExpression="Importe" >
																			    <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
																			<asp:BoundField DataField="NDVdetalleID" HeaderText="NDVdetalleID" 
																				ReadOnly="True" SortExpression="NDVdetalleID" Visible="False" />
																			<asp:BoundField DataField="notadeventaID" HeaderText="notadeventaID" 
																				SortExpression="notadeventaID" Visible="False" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                	<asp:SqlDataSource ID="sdsFertilizantes" runat="server" 
																		ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
																		SelectCommand="SELECT * FROM [vDNVFertilizantes] WHERE ([creditoId] = @creditoId)" DeleteCommand="select 1" 
                                                                        UpdateCommand="select 1">
																		<SelectParameters>
																			<asp:ControlParameter ControlID="ddlCredito" DefaultValue="-2" 
																				Name="creditoId" PropertyName="SelectedValue" />
																		</SelectParameters>
																	</asp:SqlDataSource>
                                                                </td>
                        </tr>
                        <tr>
                            <td align="center" class="TableHeader">
                                DETALLE DE OTROS PRODUCTOS COMPRADOS</td>
                        </tr>
                        <tr>
                            <td class="TableHeader">
                                                                    <asp:GridView ID="grdvProNotasVenta" runat="server" AutoGenerateColumns="False" 
                                                                        DataKeyNames="NDVdetalleID,productoID,notadeventaID" 
																		DataSourceID="sdsNOFertilizante">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="productoID" 
                                                                                HeaderText="productoID" SortExpression="productoID" Visible="False" />
                                                                            <asp:BoundField DataField="Nombre" HeaderText="Producto" ReadOnly="True" >
                                                                            <ItemStyle Wrap="False" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="fecha" HeaderText="Fecha" 
																				DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" SortExpression="fecha" />
<asp:BoundField DataField="Presentacion" HeaderText="Presentacion" SortExpression="Presentacion"></asp:BoundField>
                                                                            <asp:BoundField DataField="cantidad" DataFormatString="{0:n2}" 
                                                                                HeaderText="Cantidad" SortExpression="cantidad">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="precio" DataFormatString="{0:c2}" 
                                                                                HeaderText="Precio" SortExpression="precio">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
																			<asp:BoundField DataField="Importe" DataFormatString="{0:c2}" 
																				HeaderText="Importe" ReadOnly="True" SortExpression="Importe" >
																			    <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="NDVdetalleID" 
                                                                                HeaderText="NDVdetalleID" SortExpression="NDVdetalleID" ReadOnly="True" 
                                                                                Visible="False" />
																			<asp:BoundField DataField="notadeventaID" HeaderText="notadeventaID" 
																				SortExpression="notadeventaID" Visible="False" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <asp:SqlDataSource ID="sdsNOFertilizante" runat="server" 
																		ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
																		SelectCommand="SELECT * FROM [vDNVNoFertilizante] WHERE creditoId = @creditoId" DeleteCommand="select 1" 
                                                                        UpdateCommand="select 1 ">
																		<SelectParameters>
																			<asp:ControlParameter ControlID="ddlCredito" DefaultValue="-2" 
																				Name="creditoId" PropertyName="SelectedValue" />
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
                    <asp:GridView ID="gvSeguro" runat="server" AutoGenerateColumns="False" 
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
                            <asp:BoundField DataField="Mes" HeaderText="MES" />
                            <asp:BoundField HeaderText="Fecha" DataField="Fecha" 
                                DataFormatString="{0:dd/MM/yyy}" />
<asp:BoundField DataField="Concepto" HeaderText="Concepto"></asp:BoundField>
                            <asp:BoundField HeaderText="Monto que genera interes" DataField="MontoConInteres" 
                                DataFormatString="{0:$#,##0.00;($#,##0.00);}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="montoSinInteres" 
                                DataFormatString="{0:$#,##0.00;($#,##0.00);}" 
                                HeaderText="Monto que no genera Interes" SortExpression="montoSinInteres">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Fecha de corte" DataField="FechaPago" 
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField HeaderText="Días" DataField="Días" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TasaInteres" HeaderText="Tasa de Interés" 
                                Visible="False">
                            <HeaderStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Interes" DataField="Intereses" 
                                DataFormatString="{0:$#,##0.00;($#,##0.00);}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CargoConInteres" DataFormatString="{0:$#,##0.00;($#,##0.00);}" 
                                HeaderText="Debe Con Interes" SortExpression="CargoConInteres" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Abono" DataFormatString="{0:$#,##0.00;($#,##0.00);}" 
                                HeaderText="Abono" NullDisplayText="-" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalDebeConInteres" DataFormatString="{0:$#,##0.00;($#,##0.00);}" 
                                HeaderText="Total Debe Con Interes" SortExpression="TotalDebeConInteres" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalDebeSinInteres" 
                                DataFormatString="{0:$#,##0.00;($#,##0.00);}" 
                                HeaderText="Total Debe Sin Interes" SortExpression="TotalDebeSinInteres">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descPago" HeaderText="Desc. Pago" />
                        </Columns>
                    </asp:GridView>
                

                    <asp:SqlDataSource ID="sdsEstadodeCuenta" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="ReturnEstadodeCuenta" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlCredito" Name="creditoID" 
                                PropertyName="SelectedValue" Type="Int32" />
                            <asp:ControlParameter ControlID="txtFechaQuery" Name="fechafin" PropertyName="Text" 
                                Type="DateTime"  />
                        </SelectParameters>
                    </asp:SqlDataSource>
                

                </td>
            </tr>
        </table>
                
</div>
                    <asp:GridView ID="gridViewNotasSinInteres" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="notadeventaID" 
                        DataSourceID="sdsNotasSinInteres" 
                        ondatabound="gridViewNotasSinInteres_DataBound" ShowFooter="True">
                        <Columns>
                            <asp:BoundField DataField="notadeventaID" HeaderText="# Nota de venta" 
                                InsertVisible="False" ReadOnly="True" SortExpression="notadeventaID">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                HeaderText="Fecha" SortExpression="Fecha">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Total" DataFormatString="{0:c2}" HeaderText="Total" 
                                SortExpression="Total">
                            <FooterStyle Font-Size="X-Large" />
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:SqlDataSource ID="sdsNotasSinInteres" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
        SelectCommand="SELECT notadeventaID, Fecha, Total FROM Notasdeventa WHERE (tipocalculodeinteresID = 1) AND (creditoID = @creditoId) AND (Fecha &lt;= @fechafin)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlCredito" Name="creditoId" 
                                PropertyName="SelectedValue" />
                            <asp:ControlParameter ControlID="txtFecha" Name="fechafin" 
                                PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
            


                    <asp:GridView ID="GridViewPagos" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="SqlDataSourcePagosCreditos">
                        <Columns>
                            <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                HeaderText="Fecha" SortExpression="fecha" />
                            <asp:BoundField DataField="Monto" DataFormatString="{0:c2}" HeaderText="Monto" 
                                ReadOnly="True" SortExpression="Monto">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Concepto" HeaderText="Concepto" ReadOnly="True" 
                                SortExpression="Concepto" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSourcePagosCreditos" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="select * from vListaPagosCredito where creditoId = @creditoId order by fecha ">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlCredito" DefaultValue="-1" Name="creditoId" 
                                PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                

    </form>
</body>
</html>
