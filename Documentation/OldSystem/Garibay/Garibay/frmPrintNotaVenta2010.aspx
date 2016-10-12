<%@ Page Title="Nota de Venta 2010" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmPrintNotaVenta2010.aspx.cs" Inherits="Garibay.frmPrintNotaVenta2010" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="txtNotaID" runat="server">-1</asp:TextBox>
    <br />
        <asp:GridView ID="grdvProNotasVentaFertilizantes" runat="server" 
            AutoGenerateColumns="False" 
			DataSourceID="sdsFertilizantes">
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Producto" 
                    SortExpression="Nombre" />
                <asp:BoundField DataField="Presentacion" 
                    HeaderText="Presentación" SortExpression="Presentacion" ReadOnly="True" />
				<asp:BoundField DataField="cantidad" HeaderText="Cantidad" 
					SortExpression="cantidad" DataFormatString="{0:N2}" >
				<ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Toneladas" DataFormatString="{0:N3}" 
                    HeaderText="Toneladas" ReadOnly="True" SortExpression="Toneladas">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
				<asp:BoundField DataField="precio" DataFormatString="{0:C2}" 
					HeaderText="Precio" SortExpression="precio" >
				<ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Importe" DataFormatString="{0:C2}" 
                    HeaderText="Importe" ReadOnly="True" SortExpression="Importe">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="sdsFertilizantes" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        SelectCommand="SELECT [Presentacion], [cantidad], [precio], [Toneladas], [Importe], [Nombre] FROM [vDNVFertilizantes] WHERE ([notadeventaID] = @notadeventaID) ORDER BY [Nombre]">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtNotaID" Name="notadeventaID" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
        </asp:SqlDataSource>

        <asp:GridView ID="grdvProNotasVentaInsumos" runat="server" AutoGenerateColumns="False" 
			DataSourceID="sdsNOFertilizantes">
            <Columns>
                <asp:BoundField DataField="Nombre" 
                    HeaderText="Producto" SortExpression="Nombre" />
                <asp:BoundField DataField="Presentacion" HeaderText="Presentación" 
                    ReadOnly="True" SortExpression="Presentacion" />
                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" 
                    DataFormatString="{0:N2}" SortExpression="cantidad" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="precio" 
                    HeaderText="Precio" SortExpression="precio" DataFormatString="{0:C2}" >
				<ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
				<asp:BoundField DataField="Importe" HeaderText="Importe" 
					SortExpression="Importe" DataFormatString="{0:C2}" ReadOnly="True" >
				<ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="sdsNOFertilizantes" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        SelectCommand="SELECT [Nombre], [Presentacion], [cantidad], [precio], [Importe] FROM [vDNVNoFertilizante] WHERE ([notadeventaID] = @notadeventaID) ORDER BY [Nombre]">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtNotaID" Name="notadeventaID" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
                                                    
        <asp:GridView ID="grvPagos" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="movbanID,movimientoID,tarjetaDieselID,PagoNotaVentaID,boletaID,chequesRecibidoID" DataSourceID="SqlPagos" 
	       onrowdatabound="grvPagos_RowDataBound" ShowFooter="True">
	   
   	    <Columns>
		    <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" 
			    DataFormatString="{0:dd/MM/yyy}"/>
		    <asp:BoundField DataField="movbanID" HeaderText="movbanID" 
			    SortExpression="movbanID" visible="false"/>
		    <asp:BoundField DataField="movimientoID" HeaderText="movimientoID" 
			    SortExpression="movimientoID" visible="false"/>
		    <asp:BoundField DataField="tarjetaDieselID" HeaderText="tarjetaDieselID" 
			    SortExpression="tarjetaDieselID" visible="false"/>
	        <asp:TemplateField HeaderText="Forma de Pago">
			    <ItemTemplate>
				    <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
			    </ItemTemplate>
		    </asp:TemplateField>
		    <asp:TemplateField HeaderText="No. Cheque /Folio">
			    <ItemTemplate>
				    <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
			    </ItemTemplate>
		    </asp:TemplateField>
		    <asp:TemplateField HeaderText="Banco">
			    <ItemTemplate>
				    <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
			    </ItemTemplate>
		    </asp:TemplateField>
		    <asp:TemplateField HeaderText="Monto">
			    <ItemTemplate>
				    <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
			    </ItemTemplate>
		        <ItemStyle HorizontalAlign="Right" />
		    </asp:TemplateField>
	        <asp:BoundField DataField="PagoNotaVentaID" HeaderText="PagoNotaVentaID" 
			    SortExpression="PagoNotaVentaID" Visible="False" />
	        <asp:BoundField DataField="boletaID" HeaderText="boletaID" 
			    SortExpression="boletaID" Visible="False" />
	       </Columns>
	   
       </asp:GridView>
       <asp:SqlDataSource ID="SqlPagos" runat="server" 
	       ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
            SelectCommand="SELECT fecha, movbanID, movimientoID, tarjetaDieselID, PagoNotaVentaID, boletaID, chequesRecibidoID FROM Pagos_NotaVenta WHERE (notadeventaID = @notadeventaID)">
		    <SelectParameters>
			    <asp:ControlParameter ControlID="txtNotaID" DefaultValue="-1" 
				    Name="notadeventaID" PropertyName="Text" />
		    </SelectParameters>
	       </asp:SqlDataSource>
                                                    
                                                    
    <asp:GridView ID="gvConcentradoPagos" runat="server" AutoGenerateColumns="False" 
		DataSourceID="SqlDataSource4">
		<Columns>
			<asp:BoundField DataField="CajaChica" HeaderText="CajaChica" ReadOnly="True" 
				SortExpression="CajaChica" DataFormatString="{0:C2}" >
			<ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
			<asp:BoundField DataField="Banco" HeaderText="Banco" ReadOnly="True" 
				SortExpression="Banco" DataFormatString="{0:C2}" >
			<ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
			<asp:BoundField DataField="Tarjeta" HeaderText="Tarjeta" ReadOnly="True" 
				SortExpression="Tarjeta" DataFormatString="{0:C2}" >
			<ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
			<asp:BoundField DataField="Boletas" DataFormatString="{0:c2}" 
				HeaderText="Boletas" SortExpression="Boletas" >
		    <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
		</Columns>
	</asp:GridView>
	<br />
	<asp:SqlDataSource ID="SqlDataSource4" runat="server" 
		ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
		
		
		
		
        SelectCommand="SELECT     ISNULL(SUM(MovimientosCaja.abono), 0) AS CajaChica, ISNULL(SUM(MovimientosCuentasBanco.abono), 0) AS Banco, ISNULL(SUM(TarjetasDiesel.monto), 0) 
                      AS Tarjeta, ISNULL(SUM(Boletas.totalapagar), 0) AS Boletas
FROM         Pagos_NotaVenta LEFT OUTER JOIN
                      Boletas ON Pagos_NotaVenta.boletaID = Boletas.boletaID LEFT OUTER JOIN
                      MovimientosCaja ON Pagos_NotaVenta.movimientoID = MovimientosCaja.movimientoID LEFT OUTER JOIN
                      MovimientosCuentasBanco ON Pagos_NotaVenta.movbanID = MovimientosCuentasBanco.movbanID LEFT OUTER JOIN
                      TarjetasDiesel ON TarjetasDiesel.folio = Pagos_NotaVenta.tarjetaDieselID WHERE (Pagos_NotaVenta.notadeventaID = @notadeventaID)">
		<SelectParameters>
			<asp:ControlParameter ControlID="txtNotaID" DefaultValue="-1" 
				Name="notadeventaID" PropertyName="Text" />
		</SelectParameters>
	</asp:SqlDataSource>
	                                                    
                                                    
</asp:Content>
