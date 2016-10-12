<%@ Page Title="Reporte de Liquidaciones Por Productor Por Producto" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="True" CodeBehind="frmReporteLiqXProductorXProductos.aspx.cs" Inherits="Garibay.frmReporteLiqXProductorXProductos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
	<tr>
		<td class="TableHeader">REPORTE POR PRODUCTOR POR PRODUCTO</td>
	</tr>
</table>
<asp:UpdatePanel runat="Server" id="pnlReporte">
    <ContentTemplate>
    <asp:UpdateProgress runat="Server" id="pnlProgress" 
            AssociatedUpdatePanelID="pnlReporte" DisplayAfter="0">
    <ProgressTemplate>
        <img src="/imagenes/cargando.gif" alt="Cargando..." /> GENERANDO REPORTE...
    </ProgressTemplate>
    </asp:UpdateProgress>
    <table>
	    <tr>
		    <td class="TablaField">CICLO:</td><td>
                <asp:DropDownList ID="ddlCiclos" runat="server" DataSourceID="sdsCiclos" 
                    DataTextField="CicloName" DataValueField="cicloID" AutoPostBack="True" 
                    onselectedindexchanged="ddlCiclos_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    
                    SelectCommand="SELECT DISTINCT Ciclos.cicloID, Ciclos.CicloName FROM Ciclos INNER JOIN Liquidaciones ON Ciclos.cicloID = Liquidaciones.cicloID ORDER BY Ciclos.cicloID DESC">
                </asp:SqlDataSource>
            </td>
	    </tr>
	    <tr>
		    <td class="TablaField">PRODUCTO:</td><td>
                <asp:DropDownList ID="ddlProductos" runat="server" DataSourceID="sdsProductos" 
                    DataTextField="Producto" DataValueField="productoID" AutoPostBack="True" 
                    onselectedindexchanged="ddlProductos_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsProductos" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    
                    SelectCommand="SELECT DISTINCT Productos.productoID, Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Producto FROM Productos INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID INNER JOIN Boletas ON Productos.productoID = Boletas.productoID INNER JOIN Liquidaciones_Boletas ON Boletas.boletaID = Liquidaciones_Boletas.BoletaID INNER JOIN Liquidaciones ON Liquidaciones_Boletas.LiquidacionID = Liquidaciones.LiquidacionID WHERE (Liquidaciones.cicloID = @cicloID) AND (Liquidaciones.cobrada = 1) ORDER BY Producto">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlCiclos" Name="cicloID" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
	    </tr>
	    <tr>
		    <td class="TablaField" colspan="2">
                <asp:Button ID="btnRefresh" runat="server" onclick="btnRefresh_Click" 
                    Text="Actualizar" />
            </td>
	    </tr>
    </table>
    <table>
        <tr>
            <td class="TablaField">AGRUPADO POR PRECIO</td>
        </tr>
        <tr>
            <td>
            <asp:GridView ID="gvPorProducto" runat="server" AutoGenerateColumns="False" 
                DataSourceID="sdsProductoPorPrecio" ShowFooter="True" 
                ondatabound="gvPorProducto_DataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Precio Pagado" SortExpression="precioapagar">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("precioapagar") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblprecioapagar" runat="server" ></asp:Label>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" 
                                Text='<%# Bind("precioapagar", "{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Peso Pagado" SortExpression="PesoaPagar">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" 
                                Text='<%# Eval("PesoaPagar", "{0:N2}") %>'></asp:Label>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblPesoaPagar" runat="server" ></asp:Label>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" 
                                Text='<%# Bind("PesoaPagar", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Pagado" SortExpression="TotalPagar">
                        <EditItemTemplate>
                            <asp:Label ID="Label2" runat="server" 
                                Text='<%# Eval("TotalPagar", "{0:C2}") %>'></asp:Label>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalPagar" runat="server" ></asp:Label>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" 
                                Text='<%# Bind("TotalPagar", "{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle Wrap="False" />
                        <ItemStyle Wrap="False" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cantidad de boletas" 
                        SortExpression="CantidadDeBoletas">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" 
                                Text='<%# Bind("CantidadDeBoletas") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" 
                                Text='<%# Bind("CantidadDeBoletas", "{0:N0}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:SqlDataSource ID="sdsProductoPorPrecio" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                
                
                    SelectCommand="SELECT Boletas.precioapagar, SUM(Boletas.pesonetoapagar) AS PesoaPagar, SUM(Boletas.pesonetoapagar * Boletas.precioapagar - Boletas.dctoSecado) AS TotalPagar, COUNT(*) AS CantidadDeBoletas FROM Boletas INNER JOIN Liquidaciones_Boletas ON Boletas.boletaID = Liquidaciones_Boletas.BoletaID INNER JOIN Liquidaciones ON Liquidaciones_Boletas.LiquidacionID = Liquidaciones.LiquidacionID WHERE (Liquidaciones.cobrada = 1) AND (Liquidaciones.cicloID = @cicloID) GROUP BY Boletas.precioapagar, Boletas.productoID HAVING (Boletas.productoID = @productoID) ORDER BY Boletas.precioapagar">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlCiclos" DefaultValue="-1" Name="cicloID" 
                        PropertyName="SelectedValue" />
                    <asp:ControlParameter ControlID="ddlProductos" DefaultValue="-1" 
                        Name="productoID" PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td class="TablaField">REPORTE</td>
        </tr>
        <tr>
            <td>    
       
        
                <asp:GridView ID="gvReporte" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="productoID,productorID" DataSourceID="sdsProductosComprados" 
                    onrowdatabound="gvReporte_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Productor" SortExpression="Productor">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Productor") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkProductor" runat="server" 
                                    NavigateUrl='<%# Eval("productorID") %>' Text='<%# Eval("Productor") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nombre" HeaderText="Producto" 
                            SortExpression="Nombre" />
                        <asp:BoundField DataField="PesoaPagar" DataFormatString="{0:N2}" 
                            HeaderText="PesoaPagar" ReadOnly="True" SortExpression="PesoaPagar">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TotalPagar" DataFormatString="{0:C2}" 
                            HeaderText="TotalPagar" ReadOnly="True" SortExpression="TotalPagar">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsProductosComprados" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT LTRIM(Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre) AS Productor, Productos.productoID, Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Nombre, SUM(Boletas.pesonetoapagar) AS PesoaPagar, SUM(Boletas.pesonetoapagar * Boletas.precioapagar - Boletas.dctoSecado) AS TotalPagar, Liquidaciones.productorID, Presentaciones.Presentacion FROM Boletas INNER JOIN Liquidaciones_Boletas ON Boletas.boletaID = Liquidaciones_Boletas.BoletaID INNER JOIN Liquidaciones ON Liquidaciones_Boletas.LiquidacionID = Liquidaciones.LiquidacionID INNER JOIN Productos ON Boletas.productoID = Productos.productoID INNER JOIN Productores ON Liquidaciones.productorID = Productores.productorID INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID WHERE (Liquidaciones.cobrada = 1) AND (Liquidaciones.cicloID = @cicloID) AND (Productos.productoID = @productoID) GROUP BY LTRIM(Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre), Productos.productoID, Productos.Nombre, Productores.apaterno, Productores.amaterno, Productores.nombre, Liquidaciones.productorID, Presentaciones.Presentacion ORDER BY Productor">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlCiclos" Name="cicloID" 
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="ddlProductos" Name="productoID" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
                </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
