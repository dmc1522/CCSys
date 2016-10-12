<%@ Page Language="C#" Theme="skinverde" Title ="Detalle de Reporte desglosado por Productor" AutoEventWireup="true" CodeBehind="frmReporteDesglosadoLiquidacionesconDetalleProductor.aspx.cs" Inherits="Garibay.frmReporteDesglosadoLiquidacionesconDetalleProductor" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
  
 <script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>
    
    
   
    <table >
	<tr>
	    <td align="center" class="TableHeader">
            REPORTE DESGLOSADO POR LIQUIDACIÓN<asp:TextBox ID="txtIdProductor" runat="server" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txtIdCicloID" runat="server" Visible="False"></asp:TextBox>
        </td>
	</tr>
	<tr>
	    <td>
            <asp:GridView ID="gvLiquidaciones" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="LiquidacionID" DataSourceID="sdsLiquidaciones" 
                ondatabound="gvLiquidaciones_DataBound" ShowFooter="True">
                <Columns>
                    <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                        SortExpression="Productor" />
                    <asp:BoundField DataField="LiquidacionID" HeaderText="# Liquidación" 
                        SortExpression="LiquidacionID" InsertVisible="False" ReadOnly="True" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PesoNeto" HeaderText="Peso Neto" 
                        SortExpression="PesoNeto" DataFormatString="{0:N2}" ReadOnly="True" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="totaldctoHumedad" HeaderText="Dcto. Humedad" SortExpression="totaldctoHumedad" 
                        DataFormatString="{0:N2}" ReadOnly="True" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="totaldctoImpurezas" HeaderText="Dcto. Impurezas" 
                        SortExpression="totaldctoImpurezas" DataFormatString="{0:N2}" 
                        ReadOnly="True" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="totaldctos" HeaderText="Total. Dctos" 
                        SortExpression="totaldctos" DataFormatString="{0:N2}" ReadOnly="True" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="KgNetos" DataFormatString="{0:N2}" 
                        HeaderText="Kg Netos" SortExpression="KgNetos" ReadOnly="True" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Importe" 
                        HeaderText="Importe" ReadOnly="True" SortExpression="Importe" DataFormatString="{0:C2}" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ImporteSecado" HeaderText="Dcto Secado" 
                        ReadOnly="True" SortExpression="ImporteSecado" DataFormatString="{0:C2}" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ImporteNeto" HeaderText="Importe Neto" 
                        ReadOnly="True" SortExpression="ImporteNeto" DataFormatString="{0:C2}" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalNotas" HeaderText="Notas" ReadOnly="True" 
                        SortExpression="TotalNotas" DataFormatString="{0:C2}" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalIntereses" HeaderText="Intereses" 
                        ReadOnly="True" SortExpression="TotalIntereses" DataFormatString="{0:C2}" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalSeguro" HeaderText="Seguro" ReadOnly="True" 
                        SortExpression="TotalSeguro" DataFormatString="{0:C2}" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalAPagar" HeaderText="Total a Pagar" 
                        ReadOnly="True" SortExpression="TotalAPagar" DataFormatString="{0:C2}" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle HorizontalAlign="Right" />
            </asp:GridView>
            <asp:SqlDataSource ID="sdsLiquidaciones" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                
                
                
                
                
                
                SelectCommand="SELECT     Liquidaciones.LiquidacionID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS Productor,
                          (SELECT     SUM(Boletas.pesonetoentrada) AS Expr1
                            FROM          Liquidaciones_Boletas INNER JOIN
                                                   Boletas ON Liquidaciones_Boletas.BoletaID = Boletas.boletaID
                            WHERE      (Liquidaciones_Boletas.LiquidacionID = Liquidaciones.LiquidacionID)) AS PesoNeto,
                          (SELECT     SUM(Boletas_8.dctoHumedad) AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_8 INNER JOIN
                                                   Boletas AS Boletas_8 ON Liquidaciones_Boletas_8.BoletaID = Boletas_8.boletaID
                            WHERE      (Liquidaciones_Boletas_8.LiquidacionID = Liquidaciones.LiquidacionID)) AS totaldctoHumedad,
                          (SELECT     SUM(Boletas_7.dctoImpurezas) AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_7 INNER JOIN
                                                   Boletas AS Boletas_7 ON Liquidaciones_Boletas_7.BoletaID = Boletas_7.boletaID
                            WHERE      (Liquidaciones_Boletas_7.LiquidacionID = Liquidaciones.LiquidacionID)) AS totaldctoImpurezas,
                          (SELECT     SUM(Boletas_6.dctoImpurezas + Boletas_6.dctoHumedad) AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_6 INNER JOIN
                                                   Boletas AS Boletas_6 ON Liquidaciones_Boletas_6.BoletaID = Boletas_6.boletaID
                            WHERE      (Liquidaciones_Boletas_6.LiquidacionID = Liquidaciones.LiquidacionID)) AS totaldctos,
                          (SELECT     SUM(Boletas_5.pesonetoentrada - Boletas_5.dctoImpurezas - Boletas_5.dctoHumedad) AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_5 INNER JOIN
                                                   Boletas AS Boletas_5 ON Liquidaciones_Boletas_5.BoletaID = Boletas_5.boletaID
                            WHERE      (Liquidaciones_Boletas_5.LiquidacionID = Liquidaciones.LiquidacionID)) AS KgNetos,
                          (SELECT     SUM((Boletas_4.pesonetoentrada - Boletas_4.dctoImpurezas - Boletas_4.dctoHumedad) * Boletas_4.precioapagar) AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_4 INNER JOIN
                                                   Boletas AS Boletas_4 ON Liquidaciones_Boletas_4.BoletaID = Boletas_4.boletaID
                            WHERE      (Liquidaciones_Boletas_4.LiquidacionID = Liquidaciones.LiquidacionID)) AS Importe,
                          (SELECT     SUM(Boletas_3.dctoSecado) AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_3 INNER JOIN
                                                   Boletas AS Boletas_3 ON Liquidaciones_Boletas_3.BoletaID = Boletas_3.boletaID
                            WHERE      (Liquidaciones_Boletas_3.LiquidacionID = Liquidaciones.LiquidacionID)) AS ImporteSecado,
                          (SELECT     SUM((Boletas_2.pesonetoentrada - Boletas_2.dctoImpurezas - Boletas_2.dctoHumedad) * Boletas_2.precioapagar) - SUM(Boletas_2.dctoSecado) 
                                                   AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_2 INNER JOIN
                                                   Boletas AS Boletas_2 ON Liquidaciones_Boletas_2.BoletaID = Boletas_2.boletaID
                            WHERE      (Liquidaciones_Boletas_2.LiquidacionID = Liquidaciones.LiquidacionID)) AS ImporteNeto, SUM(Liquidaciones.notas) AS TotalNotas, 
                      SUM(Liquidaciones.intereses) AS TotalIntereses, SUM(Liquidaciones.seguro) AS TotalSeguro,
                          (SELECT     SUM((Boletas_1.pesonetoentrada - Boletas_1.dctoImpurezas - Boletas_1.dctoHumedad) * Boletas_1.precioapagar) - SUM(Boletas_1.dctoSecado) 
                                                   AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_1 INNER JOIN
                                                   Boletas AS Boletas_1 ON Liquidaciones_Boletas_1.BoletaID = Boletas_1.boletaID
                            WHERE      (Liquidaciones_Boletas_1.LiquidacionID = Liquidaciones.LiquidacionID)) - SUM(Liquidaciones.notas) - SUM(Liquidaciones.intereses) 
                      - SUM(Liquidaciones.seguro) AS TotalAPagar
FROM         Liquidaciones INNER JOIN
                      Productores ON Liquidaciones.productorID = Productores.productorID
WHERE     (Liquidaciones.cobrada = 1) AND(Liquidaciones.cicloID = @cicloID)  AND (Productores.productorID = @prodID)
GROUP BY Liquidaciones.LiquidacionID, Productores.apaterno, Productores.amaterno, Productores.nombre order by Productores.apaterno, Productores.amaterno, Productores.nombre">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtIdCicloID" DefaultValue="-1" Name="cicloID" 
                        PropertyName="Text" />
                    <asp:ControlParameter ControlID="txtIdProductor" DefaultValue="-1" 
                        Name="prodID" PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
	</tr>
	<tr>
	    <td>
            &nbsp;</td>
	</tr>
	<tr>
	    <td align="center" class="TableHeader">
            TOTAL DE ANTICIPOS DADOS AL PRODUCTOR</td>
	</tr>
	<tr>
	    <td>
            <asp:GridView ID="gvAnticipos" runat="server" AutoGenerateColumns="False" 
                DataSourceID="sdsAnticipos" ondatabound="gvAnticipos_DataBound" 
                ShowFooter="True">
                <Columns>
                    <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                        SortExpression="Productor" />
                    <asp:BoundField DataField="LiquidacionID" HeaderText="# Liquidacion" 
                        InsertVisible="False" ReadOnly="True" SortExpression="LiquidacionID">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Banco" HeaderText="Banco" ReadOnly="True" 
                        SortExpression="Banco" />
                    <asp:BoundField DataField="productorID" HeaderText="productorID" 
                        InsertVisible="False" ReadOnly="True" SortExpression="productorID" 
                        Visible="False" />
                    <asp:BoundField DataField="numCheque" HeaderText="# Cheque" 
                        SortExpression="numCheque">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Monto" DataFormatString="{0:C2}" HeaderText="Monto" 
                        NullDisplayText="$0.00" SortExpression="Monto">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Efectivo" DataFormatString="{0:C2}" 
                        HeaderText="Efectivo" NullDisplayText="$0.00" SortExpression="Efectivo">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle HorizontalAlign="Right" />
            </asp:GridView>
            <asp:SqlDataSource ID="sdsAnticipos" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                SelectCommand="SELECT     Bancos.nombre + ' - ' + CuentasDeBanco.NumeroDeCuenta AS Banco, Liquidaciones.LiquidacionID, Productores.productorID, 
                      Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS Productor, MovimientosCuentasBanco.numCheque, 
                      MovimientosCuentasBanco.cargo AS Monto, MovimientosCaja.cargo AS Efectivo
FROM         CuentasDeBanco INNER JOIN
                      MovimientosCuentasBanco ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco.cuentaID INNER JOIN
                      Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID RIGHT OUTER JOIN
                      Liquidaciones_Anticipos INNER JOIN
                      Liquidaciones ON Liquidaciones_Anticipos.LiquidacionID = Liquidaciones.LiquidacionID INNER JOIN
                      Anticipos ON Liquidaciones_Anticipos.Anticipos = Anticipos.anticipoID INNER JOIN
                      Productores ON Liquidaciones.productorID = Productores.productorID LEFT OUTER JOIN
                      MovimientosCaja ON Anticipos.movimientoID = MovimientosCaja.movimientoID ON MovimientosCuentasBanco.movbanID = Anticipos.movbanID
WHERE     (Liquidaciones.cobrada = 1) AND (Liquidaciones.cicloID = @cicloID) AND (Liquidaciones.productorID = @prodID)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtIdCicloID" DefaultValue="-1" Name="cicloID" 
                        PropertyName="Text" />
                    <asp:ControlParameter ControlID="txtIdProductor" DefaultValue="-1" 
                        Name="prodID" PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
	</tr>
	<tr>
	    <td>
            &nbsp;</td>
	</tr>
	<tr>
	    <td align="center" class="TableHeader">
            TOTAL DE PAGOS DADOS AL PRODUCTOR</td>
	</tr>
	<tr>
	    <td>
            <asp:GridView ID="gvPagos" runat="server" AutoGenerateColumns="False" 
                DataSourceID="sdsPagos" ondatabound="gvPagos_DataBound" ShowFooter="True">
                <Columns>
                    <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                        SortExpression="Productor" />
                    <asp:BoundField DataField="LiquidacionID" HeaderText="# Liquidación" 
                        InsertVisible="False" ReadOnly="True" SortExpression="LiquidacionID">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Banco" HeaderText="Banco" ReadOnly="True" 
                        SortExpression="Banco" />
                    <asp:BoundField DataField="productorID" HeaderText="productorID" 
                        InsertVisible="False" ReadOnly="True" SortExpression="productorID" 
                        Visible="False" />
                    <asp:BoundField DataField="numCheque" HeaderText="# Cheque" 
                        SortExpression="numCheque">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Monto" DataFormatString="{0:C2}" HeaderText="Monto" 
                        NullDisplayText="$0.00" SortExpression="Monto">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Efectivo" DataFormatString="{0:C2}" 
                        HeaderText="Efectivo" NullDisplayText="$0.00" SortExpression="Efectivo">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle HorizontalAlign="Right" />
            </asp:GridView>
            <asp:SqlDataSource ID="sdsPagos" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                
                SelectCommand="SELECT Bancos.nombre + ' - ' + CuentasDeBanco.NumeroDeCuenta + ' - ' + CuentasDeBanco.Titular AS Banco, Liquidaciones.LiquidacionID, Productores.productorID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS Productor, MovimientosCuentasBanco.numCheque, MovimientosCuentasBanco.cargo AS Monto, MovimientosCaja.cargo AS Efectivo FROM CuentasDeBanco INNER JOIN MovimientosCuentasBanco ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco.cuentaID INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID RIGHT OUTER JOIN Liquidaciones INNER JOIN Productores ON Liquidaciones.productorID = Productores.productorID INNER JOIN PagosLiquidacion ON Liquidaciones.LiquidacionID = PagosLiquidacion.liquidacionID LEFT OUTER JOIN MovimientosCaja ON PagosLiquidacion.movimientoID = MovimientosCaja.movimientoID ON MovimientosCuentasBanco.movbanID = PagosLiquidacion.movbanID WHERE (Liquidaciones.cobrada = 1) AND (Liquidaciones.cicloID = @cicloID) AND (Liquidaciones.productorID = @prodID)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtIdCicloID" Name="cicloID" 
                        PropertyName="Text" />
                    <asp:ControlParameter ControlID="txtIdProductor" Name="prodID" 
                        PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
	</tr>
	<tr>
	    <td>
            &nbsp;</td>
	</tr>
	<tr>
	    <td align="center" class="TableHeader">
            PRODUCTOS PAGADOS EN LAS LIQUIDACIONES</td>
	</tr>
	<tr>
	    <td>
            <asp:GridView ID="gridProductosPagados" runat="server" 
                AutoGenerateColumns="False" DataKeyNames="productoID" 
                DataSourceID="sdsProductos" ondatabound="gridProductosPagados_DataBound" 
                ShowFooter="True">
                <Columns>
                    <asp:BoundField DataField="productoID" HeaderText="productoID" 
                        InsertVisible="False" ReadOnly="True" SortExpression="productoID" 
                        Visible="False" />
                    <asp:BoundField DataField="Nombre" HeaderText="Producto" 
                        SortExpression="Nombre">
                    <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PesoaPagar" DataFormatString="{0:N2}" 
                        HeaderText="Kg Totales" ReadOnly="True" SortExpression="PesoaPagar">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="precioapagar" DataFormatString="{0:C2}" 
                        HeaderText="Precio" SortExpression="precioapagar">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalPagar" DataFormatString="{0:C2}" 
                        HeaderText="Total Pagado" ReadOnly="True" SortExpression="TotalPagar">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle HorizontalAlign="Right" />
            </asp:GridView>
            <asp:SqlDataSource ID="sdsProductos" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                SelectCommand="SELECT Productos.productoID, Productos.Nombre, SUM(Boletas.pesonetoapagar) AS PesoaPagar, SUM(Boletas.pesonetoapagar * Boletas.precioapagar - Boletas.dctoSecado) AS TotalPagar, Boletas.precioapagar FROM Boletas INNER JOIN Liquidaciones_Boletas ON Boletas.boletaID = Liquidaciones_Boletas.BoletaID INNER JOIN Liquidaciones ON Liquidaciones_Boletas.LiquidacionID = Liquidaciones.LiquidacionID INNER JOIN Productos ON Boletas.productoID = Productos.productoID WHERE (Liquidaciones.cobrada = 1) AND (Liquidaciones.cicloID = @cicloID) AND (Liquidaciones.productorID = @productorID) GROUP BY Productos.productoID, Productos.Nombre, Boletas.precioapagar">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtIdCicloID" DefaultValue="-1" Name="cicloID" 
                        PropertyName="Text" />
                    <asp:ControlParameter ControlID="txtIdProductor" DefaultValue="-1" 
                        Name="productorID" PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
	</tr>
</table>

   
    </form>
</body>
</html>