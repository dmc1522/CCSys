<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPrintNotaVenta.aspx.cs" Inherits="Garibay.frmPrintNotaVenta" Title="nota venta print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                                                                    	<asp:GridView ID="grdvProNotasVenta" runat="server" AutoGenerateColumns="False" 
                                        
                                        
                      
																			                        
            >
                                                            <Columns>
                                                                <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
											                        HeaderText="Fecha" SortExpression="Fecha" ReadOnly="true"/>
                                                                <asp:BoundField HeaderText="Producto" SortExpression="Producto" 
                                                                    DataField="Nombre">
											                       
										                        </asp:BoundField>
										                        <asp:BoundField HeaderText="Presentación" SortExpression = "Presentacion" 
                                                                    DataField="Presentacion">
											                    </asp:BoundField>
<asp:BoundField DataField="cantidad" HeaderText="Cantidad" SortExpression="cantidad" DataFormatString="{0:N2}">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Toneladas" SortExpression="peso" DataField = "peso" 
                                                                    DataFormatString="{0:N2}">
											                        
										                        <ItemStyle HorizontalAlign="Right" />
											                        
										                        </asp:BoundField>
                                                                <asp:BoundField HeaderText="Precio" SortExpression="precio" DataField="precio" 
                                                                    DataFormatString="{0:c2}">
                                                                  
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                  
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Importe" HeaderText="Importe" 
											                        SortExpression="Importe" DataFormatString="{0:C2}" />
                                                            </Columns>
                                                           
                                                           </asp:GridView>
                                  
    	<asp:TextBox ID="txtNotaventaID" runat="server"></asp:TextBox>
    
                                                                        <asp:CheckBox ID="chkboxFertilizante" runat="server" 
                  Text="NOTA DE FERTILIZANTE" />
    
    </div>
    <asp:GridView ID="grdNota" runat="server" AutoGenerateColumns="False">
		<Columns>
			<asp:BoundField DataField="notadeventaID" HeaderText="notadeventaID" />
			<asp:BoundField DataField="Productor" HeaderText="Productor" />
			<asp:BoundField DataField="fecha" HeaderText="Fecha" 
				DataFormatString="{0:dd/MM/yyyy}" />
			<asp:BoundField DataField="folio" HeaderText="Folio" />
			<asp:BoundField DataField="subtotal" HeaderText="subTotal" 
				DataFormatString="{0:C2}" />
			<asp:BoundField DataField="total" HeaderText="total" 
				DataFormatString="{0:C2}" />
			<asp:BoundField DataField="iva" HeaderText="Iva" DataFormatString="{0:C2}" />
			<asp:BoundField DataField="observaciones" HeaderText="Observaciones" />
			<asp:BoundField DataField="fechapago" HeaderText="fechapago" 
				DataFormatString="{0:dd/MM/yyyy}" />
			<asp:BoundField DataField="interes" HeaderText="interes" />
			<asp:BoundField DataField="numeropermiso" HeaderText="numeropemiso" />
			<asp:BoundField DataField="transportista" HeaderText="transportista" />
			<asp:BoundField DataField="nombrechofer" HeaderText="Nombrechofer" />
			<asp:BoundField DataField="tractorcamion" HeaderText="Tractorcamion" />
			<asp:BoundField DataField="color" HeaderText="Color" />
			<asp:BoundField DataField="origen" HeaderText="Origen" />
			<asp:BoundField DataField="remitente" HeaderText="Remitente" />
			<asp:BoundField DataField="domicilio" HeaderText="Domicilio" />
			<asp:BoundField DataField="telefono" HeaderText="Telefono" />
			<asp:BoundField DataField="destino" HeaderText="Destino" />
			<asp:BoundField DataField="CicloName" HeaderText="Ciclo" />
			<asp:BoundField DataField="placas" HeaderText="Placas" />
			<asp:CheckBoxField DataField="acredito" HeaderText="acredito" />
			<asp:BoundField DataField="creditoID" HeaderText="Credito" />
			<asp:BoundField DataField="hechoxNombre" HeaderText="hechoxNombre" />
		</Columns>
	</asp:GridView>
   
    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" 
		DataSourceID="SqlDataSource4">
		<Columns>
			<asp:BoundField DataField="CajaChica" HeaderText="CajaChica" ReadOnly="True" 
				SortExpression="CajaChica" DataFormatString="{0:C2}" />
			<asp:BoundField DataField="Banco" HeaderText="Banco" ReadOnly="True" 
				SortExpression="Banco" DataFormatString="{0:C2}" />
			<asp:BoundField DataField="Tarjeta" HeaderText="Tarjeta" ReadOnly="True" 
				SortExpression="Tarjeta" DataFormatString="{0:C2}" />
			<asp:BoundField DataField="Boletas" DataFormatString="{0:c2}" 
				HeaderText="Boletas" SortExpression="Boletas" />
		</Columns>
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource4" runat="server" 
		ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
		
		
		
		SelectCommand="SELECT ISNULL(SUM(MovimientosCaja.abono), 0) AS CajaChica, ISNULL(SUM(MovimientosCuentasBanco.abono), 0) AS Banco, ISNULL(SUM(TarjetasDiesel.monto), 0) AS Tarjeta, ISNULL(SUM(Boletas.totalapagar), 0) AS Boletas FROM Pagos_NotaVenta INNER JOIN Boletas ON Pagos_NotaVenta.boletaID = Boletas.boletaID LEFT OUTER JOIN MovimientosCaja ON Pagos_NotaVenta.movimientoID = MovimientosCaja.movimientoID LEFT OUTER JOIN MovimientosCuentasBanco ON Pagos_NotaVenta.movbanID = MovimientosCuentasBanco.movbanID LEFT OUTER JOIN TarjetasDiesel ON TarjetasDiesel.folio = Pagos_NotaVenta.tarjetaDieselID WHERE (Pagos_NotaVenta.notadeventaID = @notadeventaID)">
		<SelectParameters>
			<asp:ControlParameter ControlID="txtNotaventaID" DefaultValue="-1" 
				Name="notadeventaID" PropertyName="Text" />
		</SelectParameters>
	</asp:SqlDataSource>
	<asp:GridView ID="grvPagos" runat="server" AutoGenerateColumns="False" 
													   
		DataKeyNames="movbanID,movimientoID,tarjetaDieselID,PagoNotaVentaID,boletaID" DataSourceID="SqlPagos" 
													   ShowFooter="True" onrowdatabound="grvPagos_RowDataBound1">
												   
												   	<Columns>
														<asp:BoundField DataField="fecha" HeaderText="fecha" SortExpression="fecha" 
															DataFormatString="{0:dd/MM/yyy}"/>
														<asp:BoundField DataField="movbanID" HeaderText="movbanID" 
															SortExpression="movbanID" visible="false"/>
														<asp:BoundField DataField="movimientoID" HeaderText="movimientoID" 
															SortExpression="movimientoID" visible="false"/>
														<asp:BoundField DataField="tarjetaDieselID" HeaderText="tarjetaDieselID" 
															SortExpression="tarjetaDieselID" visible="false"/>
													    <asp:BoundField HeaderText="Forma de Pago" />
														<asp:BoundField HeaderText="No. Cheque/Folio" />
														<asp:BoundField HeaderText="Banco" />
														<asp:BoundField DataFormatString="{0:c2}" HeaderText="Monto" />
													    <asp:BoundField DataField="PagoNotaVentaID" HeaderText="PagoNotaVentaID" 
															SortExpression="PagoNotaVentaID" Visible="False" />
													    <asp:BoundField DataField="boletaID" HeaderText="boletaID" 
															SortExpression="boletaID" Visible="False" />
													   </Columns>
												   
												   </asp:GridView>
    <asp:SqlDataSource ID="SqlPagos" runat="server" 
													   ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
													   
														SelectCommand="SELECT fecha, movbanID, movimientoID, tarjetaDieselID, PagoNotaVentaID, boletaID FROM Pagos_NotaVenta WHERE (notadeventaID = @notadeventaID)">
													<SelectParameters>
														<asp:ControlParameter ControlID="txtNotaventaID" DefaultValue="-1" 
															Name="notadeventaID" PropertyName="Text" />
													</SelectParameters>
												   </asp:SqlDataSource>
	
    </form>
</body>
</html>
