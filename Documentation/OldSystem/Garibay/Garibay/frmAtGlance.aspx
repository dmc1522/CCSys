<%@ Page Theme="skinverde" Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="True" CodeBehind="frmAtGlance.aspx.cs" Inherits="Garibay.frmAtGlance" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="Server" ID="updateAtGlance">
<ContentTemplate>
    <div runat="Server" id="divAtGlance">
    <table class="centrado">
        <tr>
            <td class="TableHeader">
                BODEGA PARA TRABAJAR:</td>
            <td>
                <asp:DropDownList ID="ddlCurrBodega" runat="server" AutoPostBack="True" 
                    DataSourceID="sdsCurrBodega" DataTextField="bodega" DataValueField="bodegaID" 
                    onselectedindexchanged="ddlCurrBodega_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsCurrBodega" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table border="0" cellspacing="0" cellpadding="0" width="100%" 
                    style="border-spacing: 5px; border-collapse: separate;">
                	<tr>
                		<td align="center">
                		<asp:Label ID="lblBancos" runat="server" Text="BANCOS"></asp:Label>
                		</td>
                        <td align="center">
                        
							 <asp:Label ID="Label2" runat="server" Text="NOTAS DE COMPRA"></asp:Label>
							 
					    </td>
                        <td align="center">
                         <asp:Label ID="Label4" runat="server" Text="FACTURAS DE VENTAS"></asp:Label>
                         </td>
                	    <td align="center">
                	    <asp:Label ID="Label6" runat="server" Text="SOLICITUDES"></asp:Label>
                            </td>
                	    <td align="center">
                	    <asp:Label ID="Label8" runat="server" Text="CREDITOS FINANCIEROS"></asp:Label>
                            </td>
                	</tr>
                    <tr>
                        <td align="center">
                        <asp:HyperLink ID="HyperLink25" runat="server" 
								NavigateUrl="frmBancosSaldosMensuales.aspx">Ver Saldos de Cuentas de Banco</asp:HyperLink>
                        
                        </td>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink9" runat="server" 
                                NavigateUrl="~/frmListNotasCompras.aspx">Lista de Notas de Compra</asp:HyperLink></td>
                        <td align="center">
                                   <asp:HyperLink ID="HyperLink7" runat="server" 
                                NavigateUrl="~/frmFacturaVentaClientes.aspx">Agregar Factura de Venta</asp:HyperLink>
                        </td>
                        <td align="center">
                              <asp:HyperLink ID="HyperLink16" runat="server" 
								NavigateUrl="~/frmAddSolicitud2010.aspx">Agregar Nueva Solicitud</asp:HyperLink></td>
                        <td align="center">
                        <asp:HyperLink ID="HyperLink22" runat="server" 
								NavigateUrl="frmCreditosFinancieros.aspx">Lista de Créditos Financieros</asp:HyperLink>
                    
                        </td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                        
                              
                                </td>
                    </tr>
                    <tr>
                        <td align="center">
                         <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/frmMovBancos.aspx">Movimientos de Cuentas de Banco</asp:HyperLink>   
                        </td>
                        <td align="center">
                          <asp:HyperLink ID="HyperLink10" runat="server" 
                                NavigateUrl="~/frmAddNotasCompras.aspx">Agregar Nueva Nota de Compra</asp:HyperLink>
                            </td>
                        <td align="center">
                        <asp:HyperLink ID="HyperLink8" runat="server" 
                                NavigateUrl="~/frmListaFacturaVentaaClientes.aspx">Lista de Facturas de Venta</asp:HyperLink>
                            
                        </td>
                        <td align="center">
                       <asp:HyperLink ID="HyperLink20" runat="server" 
								NavigateUrl="frmListSolicitudes.aspx">Lista de Solicitudes</asp:HyperLink>
                            </td>
                        <td align="center">
                        <asp:HyperLink ID="HyperLink23" runat="server" 
								NavigateUrl="frmCreditoFinancieroAdd.aspx">Agregar Crédito Financiero</asp:HyperLink>
                            
                        </td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                         
                                </td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink27" runat="server" 
                                NavigateUrl="~/frmOrdendeCargaAdd.aspx">Orden de carga</asp:HyperLink>
                        </td>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink53" runat="server" 
                                NavigateUrl="~/frmPagosFacturasLista.aspx">Pagos a Facturas de Venta</asp:HyperLink>
                        </td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink28" runat="server" 
                                NavigateUrl="~/frmOrdenCompraFormato.aspx">Formato de orden de compra</asp:HyperLink>
                        </td>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink67" runat="server" 
                                NavigateUrl="~/frmEstadoDeCuentaClienteDeVenta.aspx">Edo. Cuenta Clientes de Venta</asp:HyperLink>
                        </td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                        <asp:Label ID="Label1" runat="server" Text="CAJA CHICA"></asp:Label>
                            
                        </td>
                        <td align="center">
                           <asp:Label ID="Label3" runat="server" Text="NOTAS DE VENTA"></asp:Label>
                            
                              </td>
                        <td align="center">
                        <asp:Label ID="Label5" runat="server" Text="LIQUIDACIONES"></asp:Label>
                            
                        </td>
                        <td align="center">
                         <asp:Label ID="Label7" runat="server" Text="CREDITOS"></asp:Label>
                            </td>
                        <td align="center">
                        <asp:Label ID="Label9" runat="server" Text="PRODUCTORES"></asp:Label>
                            
                        </td>
                        <td align="center">
                           </td>
                        <td align="center">
                        
                            </td>
                    </tr>
                    <tr>
                        <td align="center">
                         <asp:HyperLink ID="HyperLink2" runat="server" 
                                NavigateUrl="~/frmCajaChicaSaldosMensuales.aspx">Ver Saldos de Caja chica</asp:HyperLink>
                            
                        </td>
                        <td align="center">
                          <asp:HyperLink ID="HyperLink12" runat="server" 
                                NavigateUrl="~/frmNotadeVentaAddNew.aspx">Agregar Nueva Nota de Venta</asp:HyperLink>
                            </td>
                        <td align="center">
                           <asp:HyperLink ID="HyperLink4" runat="server" 
                                NavigateUrl="~/frmLiquidacion2010.aspx">Realizar nueva liquidacion</asp:HyperLink>
                        </td>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink19" runat="server" 
                                NavigateUrl="frmListofCreditos.aspx">Lista de Créditos</asp:HyperLink></td>
                        <td align="center">
                         <asp:HyperLink ID="HyperLink6" runat="server" 
                                NavigateUrl="~/frmAddModifyProductores.aspx">Agregar Nuevo Productor</asp:HyperLink>
                        </td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                          </td>
                    </tr>
                    <tr>
                        <td align="center">
                         <asp:HyperLink ID="HyperLink1" runat="server" 
                                NavigateUrl="~/frmAddMovCajaChica.aspx">Agregar nuevo movimiento de Caja chica</asp:HyperLink>
                            
                        </td>
                        <td align="center">
							<asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="frmListNotasDeVenta.aspx">Lista de Notas de Venta</asp:HyperLink>
                        </td>
                        <td align="center">
                          <asp:HyperLink ID="HyperLink5" runat="server" 
                                NavigateUrl="~/frmLiquidacionesLista.aspx">Lista de Liquidaciones</asp:HyperLink>  
                        </td>
                        <td align="center">
                           <asp:HyperLink ID="HyperLink18" runat="server" 
                                NavigateUrl="frmAddModifyCreditos.aspx">Agregar Nuevo Crédito</asp:HyperLink>
                            </td>
                        <td align="center">
                        <asp:HyperLink ID="HyperLink13" runat="server" 
								NavigateUrl="frmDocsProductor.aspx">Documentos del Productor</asp:HyperLink>
						</td>
						<td align="center">
						
                            </td>
                        <td align="center">
                            </td>
                    </tr>
                    <tr>
                        <td align="center">
                         
                             </td>
                        <td align="center">
                                
                            </td>
                        <td align="center">
                        
                            &nbsp;</td>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink26" runat="server" 
                                NavigateUrl="~/frmEstadodeCuentaCredito.aspx">Estado de Cuenta de crédito</asp:HyperLink>
                        </td>
                        <td align="center">
                               </td>
                            <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            </td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="left" colspan="3" rowspan="16" valign="top">
                            <table>
                            	<tr>
                            		<td>CICLO:</td><td>
                                        <asp:DropDownList ID="ddlCicloReporte" runat="server" 
                                            DataSourceID="sdsReporteCiclo" DataTextField="CicloName" 
                                            DataValueField="cicloID" AutoPostBack="True" 
                                            onselectedindexchanged="ddlCicloReporte_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsReporteCiclo" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [fechaInicio] DESC">
                                        </asp:SqlDataSource>
                                    </td>
                            	</tr>
                                <tr>
                                    <td colspan="2">
                                        <table>
                                            <tr>
                                                <td>MATRIZ</td>
                                            </tr>
                                            <tr>
                                                <td>
                                        <asp:GridView ID="gvReporteBoletas" runat="server" AutoGenerateColumns="False" 
                                            DataSourceID="sdsReporteBoletas">
                                            <Columns>
                                                <asp:BoundField DataField="grupo" HeaderText="Grupo" SortExpression="grupo" />
                                                <asp:TemplateField HeaderText="Producto" SortExpression="Producto">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkProducto" runat="server" 
                                                            NavigateUrl='<%# GetReporteEntradasSalidasURL(Eval("productoID").ToString()) %>' Text='<%# Eval("Producto") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Producto") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Entradas" HeaderText="Entradas" ReadOnly="True" 
                                                    SortExpression="Entradas" DataFormatString="{0:N2}" >
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Salidas" HeaderText="Salidas" ReadOnly="True" 
                                                    SortExpression="Salidas" DataFormatString="{0:N2}" >
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Inventario" HeaderText="Inventario" ReadOnly="True" 
                                                    SortExpression="Inventario" DataFormatString="{0:N2}" >
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="sdsReporteBoletas" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            
                                            
                                            SelectCommand="SELECT dbo.productoGrupos.grupo, LTRIM(dbo.Productos.Nombre + SPACE(1) + dbo.Presentaciones.Presentacion + SPACE(1) + dbo.Unidades.Unidad) AS Producto, ISNULL((SELECT SUM(pesonetoentrada) AS Expr1 FROM dbo.Boletas GROUP BY productoID, cicloID, bodegaID HAVING (bodegaID = 1) AND (productoID = dbo.Productos.productoID) AND (cicloID = @cicloId)), 0) AS Entradas, ISNULL((SELECT SUM(pesonetosalida) AS Expr1 FROM dbo.Boletas AS Boletas_2 GROUP BY productoID, cicloID, bodegaID HAVING (bodegaID = 1) AND (productoID = dbo.Productos.productoID) AND (cicloID = @cicloId)), 0) AS Salidas, ISNULL((SELECT SUM(pesonetoentrada - pesonetosalida) AS Expr1 FROM dbo.Boletas AS Boletas_1 GROUP BY productoID, cicloID, bodegaID HAVING (bodegaID = 1) AND (productoID = dbo.Productos.productoID) AND (cicloID = @cicloId)), 0) AS Inventario, dbo.Productos.productoID FROM dbo.Presentaciones INNER JOIN dbo.Productos ON dbo.Presentaciones.presentacionID = dbo.Productos.presentacionID INNER JOIN dbo.Unidades ON dbo.Productos.unidadID = dbo.Unidades.unidadID INNER JOIN dbo.productoGrupos ON dbo.Productos.productoGrupoID = dbo.productoGrupos.grupoID WHERE (dbo.productoGrupos.grupoID = 3) OR (dbo.productoGrupos.grupoID = 7) ORDER BY dbo.productoGrupos.grupo, producto">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="ddlCicloReporte" DefaultValue="" 
                                                    Name="cicloId" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                                </td>
                                            </tr>
                                        	<tr>
                                        		<td>MARGARITAS</td>
                                        	</tr>
                                        	<tr>
                                        	    <td>
                                        <asp:GridView ID="gvReporteBoletasMargaritas" runat="server" 
                                            AutoGenerateColumns="False" DataSourceID="sdsReporteBoletasMargaritas">
                                            <Columns>
                                                <asp:BoundField DataField="grupo" HeaderText="Grupo" SortExpression="grupo" />
                                                <asp:TemplateField HeaderText="Producto" SortExpression="Producto">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkProducto0" runat="server" 
                                                            NavigateUrl='<%# GetReporteEntradasSalidasURL(Eval("productoID").ToString()) %>' 
                                                            Text='<%# Eval("Producto") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="Label20" runat="server" Text='<%# Eval("Producto") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Entradas" DataFormatString="{0:N2}" 
                                                    HeaderText="Entradas" ReadOnly="True" SortExpression="Entradas">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Salidas" DataFormatString="{0:N2}" 
                                                    HeaderText="Salidas" ReadOnly="True" SortExpression="Salidas">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Inventario" DataFormatString="{0:N2}" 
                                                    HeaderText="Inventario" ReadOnly="True" SortExpression="Inventario">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="sdsReporteBoletasMargaritas" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            
                                                        SelectCommand="SELECT dbo.productoGrupos.grupo, LTRIM(dbo.Productos.Nombre + SPACE(1) + dbo.Presentaciones.Presentacion + SPACE(1) + dbo.Unidades.Unidad) AS Producto, ISNULL((SELECT SUM(pesonetoentrada) AS Expr1 FROM dbo.Boletas GROUP BY productoID, cicloID, bodegaID HAVING (bodegaID = 2) AND (productoID = dbo.Productos.productoID) AND (cicloID = @cicloId)), 0) AS Entradas, ISNULL((SELECT SUM(pesonetosalida) AS Expr1 FROM dbo.Boletas AS Boletas_2 GROUP BY productoID, cicloID, bodegaID HAVING (bodegaID = 2) AND (productoID = dbo.Productos.productoID) AND (cicloID = @cicloId)), 0) AS Salidas, ISNULL((SELECT SUM(pesonetoentrada - pesonetosalida) AS Expr1 FROM dbo.Boletas AS Boletas_1 GROUP BY productoID, cicloID, bodegaID HAVING (bodegaID = 2) AND (productoID = dbo.Productos.productoID) AND (cicloID = @cicloId)), 0) AS Inventario, dbo.Productos.productoID FROM dbo.Presentaciones INNER JOIN dbo.Productos ON dbo.Presentaciones.presentacionID = dbo.Productos.presentacionID INNER JOIN dbo.Unidades ON dbo.Productos.unidadID = dbo.Unidades.unidadID INNER JOIN dbo.productoGrupos ON dbo.Productos.productoGrupoID = dbo.productoGrupos.grupoID WHERE (dbo.productoGrupos.grupoID = 3) OR (dbo.productoGrupos.grupoID = 7) ORDER BY dbo.productoGrupos.grupo, producto">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="ddlCicloReporte" DefaultValue="" 
                                                    Name="cicloId" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                                </td>
                                        	</tr>
                                        </table>
                                        <br />
                                        <asp:GridView ID="gvPagosGas" runat="server" AutoGenerateColumns="False" 
                                            DataSourceID="sdsPagosGas">
                                            <Columns>
                                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" 
                                                    SortExpression="Nombre" />
                                                <asp:BoundField DataField="Efectivo" DataFormatString="{0:C2}" 
                                                    HeaderText="Efectivo" ReadOnly="True" SortExpression="Efectivo">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Bancos" DataFormatString="{0:C2}" 
                                                    HeaderText="Bancos" ReadOnly="True" SortExpression="Bancos">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Total_Pagado" DataFormatString="{0:C2}" 
                                                    HeaderText="Total_Pagado" ReadOnly="True" SortExpression="Total_Pagado">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="sdsPagosGas" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            SelectCommand="SELECT Productos.Nombre, SUM(ISNULL(MovimientosCaja.cargo, 0)) AS Efectivo, SUM(ISNULL(MovimientosCuentasBanco.cargo, 0)) AS Bancos, SUM(ISNULL(MovimientosCaja.cargo, 0)) + SUM(ISNULL(MovimientosCuentasBanco.cargo, 0)) AS Total_Pagado, Boletas.productoID, Boletas.cicloID FROM Boletas INNER JOIN Liquidaciones_Boletas ON Boletas.boletaID = Liquidaciones_Boletas.BoletaID INNER JOIN PagosLiquidacion INNER JOIN Liquidaciones ON PagosLiquidacion.liquidacionID = Liquidaciones.LiquidacionID ON Liquidaciones_Boletas.LiquidacionID = Liquidaciones.LiquidacionID INNER JOIN Productos ON Boletas.productoID = Productos.productoID LEFT OUTER JOIN MovimientosCuentasBanco ON PagosLiquidacion.movbanID = MovimientosCuentasBanco.movbanID LEFT OUTER JOIN MovimientosCaja ON PagosLiquidacion.movimientoID = MovimientosCaja.movimientoID GROUP BY Boletas.productoID, Productos.Nombre, Boletas.cicloID HAVING (Boletas.productoID = 157) AND (Boletas.cicloID = @cicloID)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="ddlCicloReporte" Name="cicloID" 
                                                    PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                        <asp:Label ID="Label10" runat="server" Text="GANADERA"></asp:Label>
                            
                        </td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            FACTURAS DIESEL</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                         <asp:HyperLink ID="HyperLink14" runat="server" 
                                NavigateUrl="~/frmGanProveedores.aspx">Proveedores de Ganado</asp:HyperLink>
                            
                        </td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink56" runat="server" 
                                NavigateUrl="~/frmListaFacturasDiesel.aspx">Lista de Facturas de Gasolina</asp:HyperLink>
                        </td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink54" runat="server" 
                                NavigateUrl="~/frmFacturaGanado.aspx">Liquidaciones de Ganado</asp:HyperLink>
                            
                        </td>
                        <td align="center">
							&nbsp;</td>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink57" runat="server" 
                                NavigateUrl="~/frmFacturaDiesel.aspx">Nueva Factura Diesel</asp:HyperLink>
                        </td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                         
                             <asp:HyperLink ID="HyperLink55" runat="server" 
                                 NavigateUrl="~/frmListaFacturasDeGanado.aspx">Lista de Facturas de Ganado</asp:HyperLink>
                         
                             </td>
                        <td align="center">
                                
                            </td>
                        <td align="center">
                        
                            </td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink61" runat="server" 
                                NavigateUrl="~/frmPagosAFacturasDeGanado.aspx">Pagos Facturas de Ganado</asp:HyperLink>
                        </td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink68" runat="server" 
                                NavigateUrl="~/frmEdoCuentaGanadero.aspx">Estado de cuenta de Ganadero</asp:HyperLink>
                        </td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink70" runat="server" 
                                NavigateUrl="~/frmReporteGanXMonth.aspx">Reporte por Mes de Cabezas</asp:HyperLink>
                        </td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            FLETES</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink64" runat="server" 
                                NavigateUrl="~/frmTransportistas.aspx">TRANSPORTISTAS</asp:HyperLink>
                        </td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink65" runat="server" 
                                NavigateUrl="~/frmLiquidacionTransportista.aspx">LIQUIDACION A TRANSPORTISTA</asp:HyperLink>
                        </td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink66" runat="server" 
                                NavigateUrl="~/frmListaLiqTransportistas.aspx">Lista de liquidaciones a transportistas</asp:HyperLink>
                        </td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
     <table>
    	<tr>
    		<td>
                <asp:CheckBox ID="chkShowReports" runat="server" Text="MOSTRAR REPORTES" /></td>
    	</tr>
    	<tr>
    	    <td>
                <asp:Panel ID="pnlReportes" runat="server">
                    <table>
                    	<tr>
                    		<td>
                                <asp:HyperLink ID="HyperLink69" runat="server" 
                                    NavigateUrl="~/frmReporteGanXMonth.aspx">Reporte por Mes de Cabezas</asp:HyperLink>
                            </td>
                    		<td>REPORTE CONCENTRADO DE CABEZAS DE GANADO POR MES</td>
                    	</tr>
                        <tr>
                            <td>
                                <asp:HyperLink ID="HyperLink63" runat="server" 
                                    NavigateUrl="~/ReporteGeneralBoletasLiquidadasYFacturadasConcentradasPorProducto.aspx">Reporte General de entradas y salidas por boletas</asp:HyperLink>
                            </td>
                            <td>
                                BOLETAS LIQUIDADAS Y FACTURADAS&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HyperLink ID="HyperLink62" runat="server" 
                                    NavigateUrl="~/frmEntradasSalidasBoletasxDia.aspx">REPORTE DE ENTRADAS Y SALIDAS DE BOLETAS POR DIA</asp:HyperLink>
                            </td>
                            <td>
                                ENTRADAS Y SALIDAS DE PRODUCTO POR BOLETAS POR DIA</td>
                        </tr>
                        <tr>
                            <td>
                                <a href="frmReporteLiqXProductorXProductos.aspx">REPORTE DE LIQUIDACIONES POR 
                                PRODUCTO POR PRODUCTOR</a></td>
                            <td>REPORTE DE LIQUIDACIONES AGRUPADO POR PRODUCTOR Y POR PRODUCTO, CON DESGLOSADO 
                                POR PRODUCTOR.</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HyperLink ID="HyperLink29" runat="server" 
                                    NavigateUrl="~/frmReporteSeguros.aspx">REPORTE DE SEGUROS</asp:HyperLink>
                            </td>
                            <td>REPORTE DE SEGUROS DE PRODUCTORES</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HyperLink ID="HyperLink50" runat="server" 
                                    NavigateUrl="~/frmReporteSegurosProAgro.aspx">REPORTE DE SEGUROS PROAGRO</asp:HyperLink>
                            </td>
                            <td>REPORTE DE SEGUROS PARA PROAGRO</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HyperLink ID="HyperLink58" runat="server" 
                                    NavigateUrl="~/frmReporteGarantiasSolicitudes.aspx">REPORTE DE GARANTIAS</asp:HyperLink>
                            </td>
                            <td>
                                REPORTE DE GARANTIAS DE LAS SOLICITUDES</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HyperLink ID="HyperLink59" runat="server" 
                                    NavigateUrl="~/frmReporteGeneralCreditos.aspx">REPORTE GLOBAL DE CRÉDITOS</asp:HyperLink>
                            </td>
                            <td>
                                REPORTE GLOBAL DE CRÉDITOS</td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="pnlReportes_CollapsiblePanelExtender" 
                    runat="server" CollapseControlID="chkShowReports" Collapsed="True" 
                    Enabled="True" ExpandControlID="chkShowReports" TargetControlID="pnlReportes">
                </cc1:CollapsiblePanelExtender>
            </td>
    	</tr>
    </table>
      <table>
        <tr>   
            <td colspan="2" class="TableHeader">Operaciones realizadas el dia de hoy</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
      	<tr>
      		<td class="TablaField">Liquidaciones agregadas:</td> <td align="right">
            <asp:Label ID="lblCountLiq" runat="server"></asp:Label>
            </td>
      	    <td align="right">
                &nbsp;</td>
      	    <td align="right" class="TablaField">
                Notas de Venta agregadas:</td>
            <td align="right">
                <asp:Label ID="lblCountNV" runat="server"></asp:Label>
            </td>
            <td align="right">
                &nbsp;</td>
      	</tr>
      	  <tr>
              <td>
                  &nbsp;</td>
              <td align="right">
                  &nbsp;</td>
              <td align="left">
                  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                      DataSourceID="sdsLiqAddedDesglosadas">
                      <Columns>
                          <asp:BoundField DataField="username" HeaderText="Usuario" 
                              SortExpression="username" />
                          <asp:BoundField DataField="Expr1" HeaderText="Cantidad Ingresada" 
                              ReadOnly="True" SortExpression="Expr1" />
                      </Columns>
                  </asp:GridView>
                  <asp:SqlDataSource ID="sdsLiqAddedDesglosadas" runat="server" 
                      ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                      SelectCommand="SELECT Users.username, COUNT(Liquidaciones.LiquidacionID) AS Expr1 FROM Liquidaciones INNER JOIN Users ON Liquidaciones.userID = Users.userID WHERE (Liquidaciones.storeTS &gt;= DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0)) GROUP BY Users.username ORDER BY Users.username">
                  </asp:SqlDataSource>
              </td>
              <td align="left">
                  &nbsp;</td>
              <td align="left">
                  &nbsp;</td>
              <td align="left">
                  <asp:GridView ID="gvNotasVenta" runat="server" AutoGenerateColumns="False" 
                      DataSourceID="sdsCountNV">
                      <Columns>
                          <asp:BoundField DataField="Usuario" HeaderText="Usuario" 
                              SortExpression="Usuario" />
                          <asp:BoundField DataField="Agregadas" HeaderText="Agregadas" ReadOnly="True" 
                              SortExpression="Agregadas" />
                      </Columns>
                  </asp:GridView>
                  <asp:SqlDataSource ID="sdsCountNV" runat="server" 
                      ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                      SelectCommand="SELECT Users.Nombre AS Usuario, COUNT(*) AS Agregadas FROM Notasdeventa INNER JOIN Users ON Notasdeventa.userID = Users.userID WHERE (Notasdeventa.storeTS &gt;= DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0)) GROUP BY Users.Nombre">
                  </asp:SqlDataSource>
              </td>
          </tr>
      	<tr>
      	    <td class="TablaField">Boletas Agregadas:</td><td align="right">
            <asp:Label ID="lblCountBoletas" runat="server"></asp:Label>
            </td>
      	    <td align="right">
                &nbsp;</td>
      	    <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
      	</tr>
          <tr>
              <td>
                  &nbsp;</td>
              <td align="right">
                  &nbsp;</td>
              <td align="left">
                  <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                      DataSourceID="sdsBoletasAddedDesglosada">
                      <Columns>
                          <asp:BoundField DataField="username" HeaderText="Usuario" 
                              SortExpression="username" />
                          <asp:BoundField DataField="Expr1" HeaderText="Boletas Ingresadas" 
                              ReadOnly="True" SortExpression="Expr1" />
                      </Columns>
                  </asp:GridView>
                  <asp:SqlDataSource ID="sdsBoletasAddedDesglosada" runat="server" 
                      ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                      SelectCommand="SELECT Users.username, COUNT(Boletas.boletaID) AS Expr1 FROM Users INNER JOIN Boletas ON Users.userID = Boletas.userID WHERE (Boletas.storeTS &gt;= DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0)) GROUP BY Users.username">
                  </asp:SqlDataSource>
              </td>
              <td align="left">
                  &nbsp;</td>
              <td align="left">
                  &nbsp;</td>
              <td align="left">
                  &nbsp;</td>
          </tr>
      </table>
      <table>
      	<tr>
      		<th>Ultimas Acciones realizadas:</th>
      	</tr>
      	<tr>
      	    <td>
      	            <asp:GridView ID="gvLastActions" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="sdsLastActions">
                    <Columns>
                        <asp:BoundField DataField="Tiempo" HeaderText="TIEMPO" 
                            SortExpression="Tiempo" />
                        <asp:BoundField DataField="Nombre" HeaderText="NOMBRE" 
                            SortExpression="Nombre" />
                        <asp:BoundField DataField="Modulo" HeaderText="MODULO" 
                            SortExpression="Modulo" />
                        <asp:BoundField DataField="Accion" HeaderText="ACCION" 
                            SortExpression="Accion" />
                        <asp:BoundField DataField="descripcion" HeaderText="DESCRIPCION" 
                            SortExpression="descripcion" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsLastActions" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT TOP 20 UserSessionRecords.timestamp as Tiempo, Users.Nombre, Modules.module as Modulo, UsersActions.fancyName as Accion, UserSessionRecords.description as descripcion FROM Modules INNER JOIN UserSessionRecords ON Modules.moduleID = UserSessionRecords.moduleID INNER JOIN Users ON UserSessionRecords.userID = Users.userID INNER JOIN UsersActions ON UserSessionRecords.useractionID = UsersActions.useractionID ORDER BY UserSessionRecords.timestamp DESC">
                </asp:SqlDataSource>
            </td>
      	</tr>
      </table>
    </div>
    
    <div runat="Server" id="divAtGlanceBanco">
    <table class="centrado">
        <tr>
            <td colspan="2">
                <table border="0" cellspacing="0" cellpadding="0" width="100%" 
                    style="border-spacing: 5px; border-collapse: separate;">
                	<tr>
                		<td align="center">
                		    <asp:Label ID="Label13" runat="server" Text="SOLICITUDES"></asp:Label>
                		</td>
                        <td align="center">
                        
							 <asp:Label ID="Label16" runat="server" Text="NOTAS DE VENTA"></asp:Label>
							 
					    </td>
                        <td align="center">
                            <asp:Label ID="Label19" runat="server" Text="PRODUCTORES"></asp:Label>
                         </td>
                	    <td align="center">
                	        &nbsp;</td>
                	</tr>
                    <tr>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink21" runat="server" 
                                NavigateUrl="~/frmAddSolicitud2010.aspx">Agregar Nueva Solicitud</asp:HyperLink>
                        
                        </td>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink40" runat="server" 
                                NavigateUrl="~/frmNotadeVentaAddNew.aspx">Agregar Nueva Nota de Venta</asp:HyperLink>
                        </td>
                        <td align="center">
                                   <asp:HyperLink ID="HyperLink43" runat="server" 
                                       NavigateUrl="~/frmAddModifyProductores.aspx">Agregar Nuevo Productor</asp:HyperLink>
                        </td>
                        <td align="center">
                              &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink34" runat="server" 
                                NavigateUrl="frmListSolicitudes.aspx">Lista de Solicitudes</asp:HyperLink>
                        </td>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink45" runat="server" 
                                NavigateUrl="frmListNotasDeVenta.aspx">Lista de Notas de Venta</asp:HyperLink>
                            </td>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink48" runat="server" 
                                NavigateUrl="frmDocsProductor.aspx">Documentos del Productor</asp:HyperLink>
                            
                        </td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:HyperLink ID="HyperLink49" runat="server" 
                                NavigateUrl="~/frmEstadodeCuentaCredito.aspx">Estado de Cuenta de crédito</asp:HyperLink>
                        </td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
							&nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                         
                             </td>
                        <td align="center">
                                
                            </td>
                        <td align="center">
                        
                            </td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
    <br />
    
   
    
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
