<%@ Page Language="C#" Theme="skinverde" Title="Nueva Boleta" AutoEventWireup="true" CodeBehind="frmBoletaNewQuick.aspx.cs" Inherits="Garibay.frmBoletaNewQuick" %>

<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    </head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>

    <div>
    <table>
    	<tr>
    		<td>
    		    <table>
                    <tr>
                        <td class="TableHeader" align="center" colspan="2">
                            AGREGAR BOLETA RÁPIDA&nbsp;<asp:Label ID="lblNumBoleta" runat="server" 
                                Visible="False"></asp:Label>
                            <asp:Label ID="lblLiqID" runat="server" Text="lblLiqID" Visible="False"></asp:Label>
                            <asp:Label ID="lblNotaCompraID" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField" align="right">
                            CICLO:</td>
                        <td>
                            <asp:DropDownList ID="drpdlCiclo" runat="server" DataSourceID="sdsCiclos" 
                                DataTextField="CicloName" DataValueField="cicloID" Height="23px" Width="233px">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos]">
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table>
                	            <tr>
                		            <td class="TablaField">
                                        <asp:RadioButton ID="rbProductor" runat="server" Text="Productor" />
                                       
                                    </td>
                		            <td>
                		            <asp:Panel runat="Server" ID="pnlProductor">
                		                <table>
                		    	            <tr>
                		    		            <td><br />
                                                    <asp:DropDownList ID="ddlNewBoletaProductor" runat="server" 
                                                        DataSourceID="sdsNewBoletaProductor" DataTextField="Productor" 
                                                        DataValueField="productorID" Height="23px" Width="364px">
                                                    </asp:DropDownList>
                                                            <cc1:ListSearchExtender ID="ddlNewBoletaProductor_ListSearchExtender" 
                                                                runat="server" Enabled="True" PromptText="" 
                                                                TargetControlID="ddlNewBoletaProductor">
                                                            </cc1:ListSearchExtender>
                                                    <asp:SqlDataSource ID="sdsNewBoletaProductor" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                        
                                                                SelectCommand="SELECT productorID, LTRIM(apaterno + ' ' + amaterno + ' ' + nombre) AS Productor FROM Productores ORDER BY Productor">
                                                    </asp:SqlDataSource>
                                                </td>
                		    	            </tr>
                		                </table>
                		            </asp:Panel>
                		                
                		            </td>
                	            </tr>
                	            <tr>
                		            <td class="TablaField">
                		           
                                        <asp:RadioButton ID="rbProveedor" runat="server" Text="Proveedor" />
                                        
                                    
                                    </td>
                		            <td>
                		             <asp:Panel id="pnlProveedor" runat="Server">
                		                <asp:DropDownList ID="drpdlProveedor" runat="server" 
                                            DataSourceID="sdsProveedor" DataTextField="Nombre" DataValueField="proveedorID" 
                                            Height="22px" Width="367px">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsProveedor" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            SelectCommand="SELECT [proveedorID], [Nombre] FROM [Proveedores] ORDER BY [Nombre]">
                                        </asp:SqlDataSource>
                                         </asp:Panel>
                		            </td>
                	            </tr>
                	            <tr>
                		            <td class="TablaField">
                                        <asp:RadioButton ID="rbClienteVenta" runat="server" Text="Cliente de Venta" />
                                      
                                    </td>
                		            <td>
                		                <asp:Panel runat="Server" id="pnlClienteVenta">
                		                    <table>
                		        	            <tr>
                		        		            <td>
                		        		                <asp:DropDownList ID="ddlClientes" runat="server" DataSourceID="sdsClientes" 
                                                            DataTextField="nombre" DataValueField="clienteventaID" Height="23px" 
                                                            Width="358px">
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="sdsClientes" runat="server" 
                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                            SelectCommand="SELECT [clienteventaID], [nombre] FROM [ClientesVentas] ORDER BY [clienteventaID]">
                                                        </asp:SqlDataSource>
                		        		            </td>
                		        	            </tr>
                		                    </table>
                		                </asp:Panel>
                		               
                		            </td>
                	            </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField" align="right">
                            # BOLETO DE BASCULA:</td>
                        <td>
                            <asp:TextBox ID="txtNewNumBoleta" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField" align="right">
                            # DE FOLIO:</td>
                        <td>
                            <asp:TextBox ID="txtNewTicket" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField" align="right">
                            CHOFER:</td>
                        <td>
                            <asp:TextBox ID="txtChofer" runat="server" Width="322px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField" align="right">
                            PLACAS:</td>
                        <td>
                            <asp:TextBox ID="txtPlacas" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField" align="right">PRODUCTO:</td>
                        <td>
                            <asp:DropDownList ID="ddlNewBoletaProducto" runat="server" 
                                DataSourceID="sdsNewBoletaProductos" DataTextField="Nombre" 
                                DataValueField="productoID">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="sdsNewBoletaProductos" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                SelectCommand="SELECT [productoID], [Nombre] FROM [Productos] ORDER BY [Nombre]">
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="TablaField">
                            BODEGA:</td>
                        <td>
                            <asp:DropDownList ID="ddlNewBoletaBodega" runat="server" 
                                DataSourceID="sdsNewBoletaBodega" DataTextField="bodega" 
                                DataValueField="bodegaID">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="sdsNewBoletaBodega" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField" align="right">FECHA ENTRADA:</td>
                        <td>
                            <asp:TextBox ID="txtNewFechaEntrada" runat="server"></asp:TextBox>
                            <rjs:PopCalendar ID="PopCalendar5" runat="server"  Control="txtNewFechaEntrada" 
                                Separator="/"/>
                            <br />
                            <asp:CheckBox ID="chkChangeFechaSalidaNewBoleta" runat="server" 
                                Text="Fecha Salida es Diferente a la de Entrada" />
                            <asp:Panel ID="divFechaSalidaNewBoleta" runat="Server">
                                FECHA SALIDA:
                                <asp:TextBox ID="txtNewFechaSalida" runat="server" ReadOnly="True"></asp:TextBox>
                                <rjs:PopCalendar ID="PopCalendar4" runat="server" Control="txtNewFechaSalida" 
                                    Separator="/" />
                            </asp:Panel>
                           
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField" align="right">PESO BRUTO:</td>
                        <td>
                            <asp:TextBox ID="txtNewPesoEntrada" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField" align="right">PESO TARA:</td>
                        <td>
                            <asp:TextBox ID="txtNewPesoSalida" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField" align="right">
                            PESO NETO:</td>
                        <td>
                            <asp:TextBox ID="txtPesoNetoNewBoleta" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField" align="right">
                            HUMEDAD:</td>
                        <td>
                            <asp:TextBox ID="txtNewHumedad" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField" align="right">
                            IMPUREZAS:</td>
                        <td>
                            <asp:TextBox ID="txtNewImpurezas" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField" align="right">
                            PRECIO:</td>
                        <td>
                            <asp:TextBox ID="txtNewPrecio" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField" align="right">
                            SECADO:</td>
                        <td>
                            <asp:TextBox ID="txtNewSecado" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField" align="left" colspan="2">
                            <asp:CheckBox ID="chkTransportista" runat="server" 
                                Text="Relacionar con transportista" />
                            <asp:Panel ID="pnlTransportista" runat="server">
                            <table>
                            	<tr>
                            		<td class="TablaField">Transportista</td>
                            		<td>
                                        <asp:DropDownList ID="ddlTransportista" runat="server" 
                                            DataSourceID="sdsTransportistas" DataTextField="transportista" 
                                            DataValueField="transportistaID">
                                        </asp:DropDownList>
                            		    <asp:SqlDataSource ID="sdsTransportistas" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            SelectCommand="SELECT [transportistaID], [transportista] FROM [Transportistas] ORDER BY [transportista]">
                                        </asp:SqlDataSource>
                            		</td>
                            	</tr>
                            </table>
                            </asp:Panel>
                            <cc1:CollapsiblePanelExtender ID="pnlTransportista_CollapsiblePanelExtender" 
                                runat="server" Enabled="True" TargetControlID="pnlTransportista" 
                                CollapseControlID="chkTransportista" Collapsed="True" 
                                ExpandControlID="chkTransportista">
                            </cc1:CollapsiblePanelExtender>
                        </td>
                    </tr>
                    </table>
    		</td>
    		<td valign="bottom" align="left">
    		<asp:Panel runat="Server" id="pnlDatosBoletaSalida">
    		    <table>
    		    	<tr>
    		    		<td colspan="2" class="TableHeader">DATOS DE BOLETA DE SALIDA</td>
    		    	</tr>
    		    	<tr>
    		    	    <td class="TablaField">FOLIO DESTINO:</td>
    		    	    <td>
                            <asp:TextBox ID="txtFolioDestino" runat="server"></asp:TextBox>
                        </td>
    		    	</tr>
    		        <tr>
                        <td class="TablaField">
                            PESO DESTINO:</td>
                        <td>
                            <asp:TextBox ID="txtPesoDestino" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField">
                            MERMA:</td>
                        <td>
                            <asp:TextBox ID="txtMerma" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField">
                            IMPORTE:</td>
                        <td>
                            <asp:TextBox ID="txtImporte" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField">
                            FLETE:</td>
                        <td>
                            <asp:TextBox ID="txtFlete" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField">
                            IMPORTE FLETE:</td>
                        <td>
                            <asp:TextBox ID="txtImporteFlete" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField">
                            PRECIO NETO:</td>
                        <td>
                            <asp:TextBox ID="txtPrecioNeto" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField">
                            GRANO CHICO:</td>
                        <td>
                            <asp:TextBox ID="txtGranoChico" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField">
                            GRANO DAÑADO:</td>
                        <td>
                            <asp:TextBox ID="txtGranoDanado" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField">
                            GRANO QUEBRADO:</td>
                        <td>
                            <asp:TextBox ID="txtGranoQuebrado" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField">
                            GRANO ESTRELLADO:</td>
                        <td>
                            <asp:TextBox ID="txtGranoEstrellado" runat="server"></asp:TextBox>
                        </td>
                    </tr>
    		    </table>
    		</asp:Panel>
    		   
    		</td>
    	</tr>
    	<tr><td colspan="2"> 
    	 <asp:CheckBox ID="chkBoxNotadeCompra" runat="server" 
                    Text="Boleta esta relacionada a Nota de Compra" />
                <asp:Panel runat="Server" id="pnlNotadeCompra">
                    <table>
                    	<tr>
                    		<td class="TableField">NOTA DE COMPRA:</td>
                    		<td>
                                <asp:DropDownList ID="drpdlNotadeCompra" runat="server" 
                                    DataSourceID="sdsNotaCompra" DataTextField="NotaCompra" 
                                    DataValueField="notadecompraID">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsNotaCompra" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    
                                    SelectCommand="SELECT (NotasDeCompra.folio + ' - ' + Proveedores.Nombre) as NotaCompra, NotasDeCompra.notadecompraID FROM NotasDeCompra INNER JOIN Proveedores ON NotasDeCompra.proveedorID = Proveedores.proveedorID">
                                </asp:SqlDataSource>
                            </td>
                    	</tr>
                    </table>
                </asp:Panel>
    		 
    	</td></tr>
    	<tr>
    		<td colspan="2">
    		<asp:CheckBox ID="chkFacturaClienteVenta" runat="server" 
                    Text="Boleta esta relacionada a una Factura de Venta" />
    	 
    		
                <asp:Panel runat="Server" id="pnlFactura">
                    <table>
                    	<tr>
                    		<td class="TableField">FACTURA:</td>
                    		<td>
                                <asp:DropDownList ID="ddlFacturaClienteVenta" runat="server" 
                                    DataSourceID="sdsFacturasVenta" DataTextField="Factura" 
                                    DataValueField="FacturaCV">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsFacturasVenta" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT FacturasClientesVenta.FacturaCV, ClientesVentas.nombre + ' - ' + FacturasClientesVenta.facturaNo AS Factura FROM FacturasClientesVenta INNER JOIN ClientesVentas ON FacturasClientesVenta.clienteVentaID = ClientesVentas.clienteventaID ORDER BY FacturasClientesVenta.fecha DESC">
                                </asp:SqlDataSource>
                            </td>
                    	</tr>
                    </table>
                </asp:Panel>
    		  
               
    		</td>
    	</tr>
    	<tr>
    		<td colspan="2">
                <asp:CheckBox ID="chkBoxNotadeVenta" runat="server" 
                    Text="Boleta esta relacionada a Nota de Venta" />
                <br />
                <asp:Panel runat="Server" id="pnlNotadeVenta">
                    <table>
                    	<tr>
                    		<td class="TableField">NOTA DE VENTA:</td>
                    		<td>
                                <asp:DropDownList ID="drpdlNotadeVenta" runat="server" 
                                    DataSourceID="sdsNotadeVenta" DataTextField="NotadeVenta" 
                                    DataValueField="notadeventaID">
                                </asp:DropDownList>
                                
                                <asp:SqlDataSource ID="sdsNotadeVenta" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" SelectCommand="SELECT Notasdeventa.notadeventaID, Notasdeventa.Folio + ' - ' + Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS NotadeVenta 
FROM Notasdeventa INNER JOIN Productores ON Notasdeventa.productorID = Productores.productorID">
                                </asp:SqlDataSource>
                                
                            </td>
                    	</tr>
                    </table>
                </asp:Panel>
    		  
               
    		</td>
    	</tr>
    	<tr>
    		<td colspan="2">
                            <asp:Panel ID="pnlNewBoleta" runat="server" Visible="False">
                                <asp:Image ID="imgBien" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                                <asp:Image ID="imgMal" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                                <asp:Label ID="lblNewBoletaResult" runat="server"></asp:Label>
                            </asp:Panel>
    		</td>
    	</tr>
    	<tr>
    		<td colspan="2">
                            <asp:Button ID="btnAgregar" runat="server" onclick="btnAgregar_Click" 
                                Text="Agregar" CausesValidation="False" 
                                style="height: 26px" />
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" 
                                onclick="btnModificar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Salir" 
                                onclick="btnCancelar_Click" />
    		</td>
    	</tr>
    </table>
     
    </div>
    </form>
</body>
</html>
