<%@ Page Title="Liquidacion" theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="True" CodeBehind="frmLiquidacion2010.aspx.cs" Inherits="Garibay.frmLiquidacion2010" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript">
    $(document).ready(function() {
        //getSaldoCredito();
        $("#<% = ddlCreditosDisponiblesParaPagar.ClientID %>").change(function() {
            getSaldoCredito();

        });
        function getSaldoCredito() {
            var creditoId = $("#<% = ddlCreditosDisponiblesParaPagar.ClientID %>").val();
            PageMethods.getCreditoSaldo(creditoId, getSaldoSuccess, getSaldoError);
        }
        function getSaldoSuccess(response) {
            $("#<% = labelSaldoCredito.ClientID %>").text(response);
        }
        function getSaldoError(error) {
            alert(error);
        }
    });

   

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="Server" id="pnlLiquidacion" UpdateMode="Conditional">
    <ContentTemplate>
    <table>
	    <tr>
		    <td colspan="2" align="right">
                <asp:HyperLink ID="HyperLink3" runat="server" Font-Size="X-Large" 
                    NavigateUrl="~/frmLiquidacion2010.aspx">AGREGAR UNA NUEVA LIQUIDACION</asp:HyperLink>
            </td>
	        <td>
                &nbsp;</td>
	    </tr>
	    <tr>
            <td class="TableHeader" colspan="2">
                DATOS DE LIQUIDACION:</td>
            <td>
                &nbsp;</td>
        </tr>
	    <tr>
            <td class="TablaField">FECHA:</td>
            <td>
                <asp:TextBox ID="txtFechaLiquidacion" runat="server" ReadOnly="True" 
                    Width="75px"></asp:TextBox>
                <rjs:PopCalendar ID="PopCalendar11" runat="server" Control="txtFechaLiquidacion" 
                    Separator="/" />
            </td>
            <td rowspan="8">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="TablaField">
                CICLO:</td>
            <td>
                <asp:DropDownList ID="ddlCiclo" runat="server" AutoPostBack="True" 
                    DataSourceID="sdsCiclos" DataTextField="CicloName" DataValueField="cicloID" 
                    onselectedindexchanged="ddlCiclo_SelectedIndexChanged" 
                    Font-Bold="True" Font-Size="X-Large">
                </asp:DropDownList> <<-- NO OLVIDE VERIFICAR EL CICLO AL QUE AGREGA LA LIQUIDACION.
                <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>"                      	    
                    SelectCommand="SELECT cicloID, CicloName FROM dbo.Ciclos WHERE (cerrado = @cerrado) ORDER BY CicloName DESC">
            	    <SelectParameters>
					    <asp:Parameter DefaultValue="FALSE" Name="cerrado" Type="Boolean" />
				    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td class="TablaField">
                PRODUCTOR:</td>
            <td><br />
                <asp:DropDownList ID="ddlProductor" runat="server" 
                    DataSourceID="sdsProductores" DataTextField="Productor" 
                    DataValueField="productorID"  
                    AutoPostBack="True" onselectedindexchanged="ddlProductor_SelectedIndexChanged">
                </asp:DropDownList>
                <cc1:ListSearchExtender ID="ddlProductor_ListSearchExtender" runat="server" 
                    Enabled="True" PromptText="Escriba para empezar a buscar" 
                    TargetControlID="ddlProductor">
                </cc1:ListSearchExtender>
                <asp:SqlDataSource ID="sdsProductores" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    
                    SelectCommand="SELECT productorID, LTRIM(apaterno + ' ' + amaterno + ' ' + nombre) AS Productor FROM Productores ORDER BY Productor">
                </asp:SqlDataSource>
                <asp:Button ID="btnAddQuickProductor" runat="server" 
                    Text="Agregar Productor Rápido" />
                <asp:Button ID="btnActualizaComboProductores" runat="server" 
                    onclick="btnActualizaComboProductores_Click" 
                    Text="Actualizar Lista de Productores" />
            </td>
        </tr>
        <tr>
            <td class="TablaField" colspan="2">
                <asp:CheckBox ID="chkBoxTraeBoletas" runat="server" Checked="True" 
                    Text="Asignar todas las boletas de este productor a esta liquidación" />
            </td>
        </tr>
        <tr>
            <td class="TablaField" colspan="2">
                <asp:CheckBox ID="chkAsignarAnticipos" runat="server" Checked="True" 
                    Text="Asignar todos los anticipos de este productor a esta liquidación" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" >
                <asp:Label ID="lblValidacionRes" runat="server" Text=""></asp:Label><br />
                <asp:GridView ID="gvLiquidacionesSinCerrar" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="LiquidacionID" DataSourceID="sdsLiquidacionesYaEnSistema" 
                    Visible="False">
                    <Columns>
                        <asp:BoundField DataField="LiquidacionID" HeaderText="# Liq" 
                            InsertVisible="False" ReadOnly="True" SortExpression="LiquidacionID" />
                        <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                            HeaderText="Fecha" SortExpression="fecha">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="subTotal" DataFormatString="{0:c}" 
                            HeaderText="SubTotal" SortExpression="subTotal">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="cobrada" HeaderText="Realizada" 
                            SortExpression="cobrada" />
                        <asp:BoundField DataField="cantidadBoletas" HeaderText="Cantidad de Boletas" 
                            SortExpression="cantidadBoletas" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" 
                                    NavigateUrl='<%# GetLiqExistenteURL(Eval("LiquidacionID").ToString()) %>'>ABRIR</asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsLiquidacionesYaEnSistema" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT Liquidaciones.LiquidacionID, Liquidaciones.fecha, Liquidaciones.subTotal, Liquidaciones.cobrada, Liquidaciones.cicloID, Liquidaciones.productorID, COUNT(Liquidaciones_Boletas.BoletaID) AS cantidadBoletas FROM Liquidaciones LEFT OUTER JOIN Liquidaciones_Boletas ON Liquidaciones.LiquidacionID = Liquidaciones_Boletas.LiquidacionID WHERE (Liquidaciones.cobrada = 0) AND (Liquidaciones.cicloID = @cicloID) AND (Liquidaciones.productorID = @productorID) GROUP BY Liquidaciones.LiquidacionID, Liquidaciones.fecha, Liquidaciones.subTotal, Liquidaciones.cobrada, Liquidaciones.cicloID, Liquidaciones.productorID">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlCiclo" Name="cicloID" 
                            PropertyName="SelectedValue" />
                        <asp:ControlParameter ControlID="ddlProductor" Name="productorID" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnVerificarAntesAdd" runat="server" 
                    onclick="btnVerificarAntesAdd_Click" Text="Validar Liquidacion" />
                <asp:Button ID="btnAgregarLiquidacion" runat="server" CausesValidation="False" 
                    onclick="btnAgregarLiquidacion_Click" Text="Agregar Nueva Liquidación" 
                    Visible="False" />
            </td>
        </tr>
    </table>
    </ContentTemplate>
</asp:UpdatePanel>
    <asp:Panel ID="pnlCentral" runat="server" Visible="false">
    <table>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                <table>                 
                    <tr>
                        <td>
                            Liquidación Número:
                            <asp:Label ID="lblNumLiquidacion" runat="server" Font-Bold="True" 
                                Font-Size="X-Large" Text="0"></asp:Label>
                            <asp:TextBox ID="txtLiquidacionID" runat="server" Visible="False" Width="10px">-1</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DetailsView ID="dvProductorData" runat="server" AutoGenerateRows="False" 
                                DataKeyNames="productorID" DataSourceID="sdsProductorData" Height="50px" 
                                Width="125px">
                                <Fields>
                                    <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                                        SortExpression="Productor">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fechanacimiento" DataFormatString="{0:dd/MM/yyyy}" 
                                        HeaderText="Fecha Nacimiento" SortExpression="fechanacimiento">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IFE" HeaderText="IFE" SortExpression="IFE" />
                                    <asp:BoundField DataField="CURP" HeaderText="CURP" SortExpression="CURP" />
                                    <asp:BoundField DataField="domicilio" HeaderText="Domicilio" 
                                        SortExpression="domicilio">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="poblacion" HeaderText="Poblacion" 
                                        SortExpression="poblacion">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="municipio" HeaderText="Municipio" 
                                        SortExpression="municipio">
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="estado" HeaderText="Estado" 
                                        SortExpression="estado" />
                                    <asp:BoundField DataField="telefono" HeaderText="Telefono" 
                                        SortExpression="telefono" />
                                    <asp:BoundField DataField="celular" HeaderText="Celular" 
                                        SortExpression="celular" />
                                </Fields>
                            </asp:DetailsView>
                            <asp:SqlDataSource ID="sdsProductorData" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                SelectCommand="SELECT LTRIM(Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre) AS Productor, Productores.fechanacimiento, Productores.IFE, Productores.CURP, Productores.domicilio, Productores.poblacion, Productores.municipio, Estados.estado, Productores.telefono, Productores.celular, Productores.productorID FROM Productores INNER JOIN Estados ON Productores.estadoID = Estados.estadoID WHERE (Productores.productorID = @productorID)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlProductor" Name="productorID" 
                                        PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                <table>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkAddBoletaExistente" runat="server" CssClass="TablaField" 
                                Text="Agregar Boleta Existente" />
                            <asp:Panel ID="pnlAddBoletaExistente" runat="Server">
                                <asp:UpdatePanel ID="updatePanelBoletaExistente" runat="Server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:UpdateProgress ID="UpdateProgress4" runat="server" 
                                            AssociatedUpdatePanelID="updatePanelBoletaExistente" DisplayAfter="0">
                                            <ProgressTemplate>
                                                <asp:Image ID="Image33" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                                                Cargando información...
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <table>
                                            <tr>
                                                <td class="TablaField">
                                                    Ciclo:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCicloBoletaExistente" runat="server" 
                                                        AutoPostBack="True" DataSourceID="sdsCiclosBoletasExistentes" 
                                                        DataTextField="CicloName" DataValueField="cicloID" 
                                                        onselectedindexchanged="ddlCicloBoletaExistente_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="sdsCiclosBoletasExistentes" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                        SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [CicloName] DESC">
                                                    </asp:SqlDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TablaField">
                                                    Productores:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlProductoresBolExistentes" runat="server" 
                                                        AutoPostBack="True" DataSourceID="sdsProductoresBolExistentes" 
                                                        DataTextField="Productor" DataValueField="productorID" 
                                                        onselectedindexchanged="ddlProductoresBolExistentes_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="sdsProductoresBolExistentes" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                        SelectCommand="SELECT DISTINCT Productores.productorID, LTRIM(Productores.apaterno + SPACE(1) + Productores.amaterno + SPACE(1) + Productores.nombre) AS Productor FROM Boletas INNER JOIN Productores ON Boletas.productorID = Productores.productorID WHERE (Boletas.cicloID = @cicloID) AND (Boletas.boletaID NOT IN (SELECT BoletaID FROM Liquidaciones_Boletas)) ORDER BY Productor">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="ddlCicloBoletaExistente" Name="cicloID" 
                                                                PropertyName="SelectedValue" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:GridView ID="gvBoletasExistentes" runat="server" 
                                                        AutoGenerateColumns="False" DataKeyNames="boletaID" 
                                                        DataSourceID="sdsBoletasExistentes">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Agregar">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkBoletaAdd" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="boletaID" HeaderText="boletaID" 
                                                                InsertVisible="False" ReadOnly="True" SortExpression="boletaID" />
                                                            <asp:BoundField DataField="Ticket" HeaderText="Ticket" 
                                                                SortExpression="Ticket" />
                                                            <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                                                                SortExpression="Productor" />
                                                            <asp:BoundField DataField="Nombre" HeaderText="Producto" 
                                                                SortExpression="Nombre" />
                                                            <asp:BoundField DataField="pesonetoentrada" DataFormatString="{0:N2}" 
                                                                HeaderText="PESO NETO" SortExpression="pesonetoentrada">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="pesonetoapagar" DataFormatString="{0:N2}" 
                                                                HeaderText="PESO NETO A PAGAR" SortExpression="pesonetoapagar">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="precioapagar" DataFormatString="{0:C2}" 
                                                                HeaderText="PRECIO" SortExpression="precioapagar">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:SqlDataSource ID="sdsBoletasExistentes" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                        SelectCommand="SELECT Boletas.boletaID, Boletas.Ticket, LTRIM(Productores.apaterno + SPACE(1) + Productores.amaterno + SPACE(1) + Productores.nombre) AS Productor, Boletas.pesonetoentrada, Boletas.pesonetoapagar, Boletas.precioapagar, Productos.Nombre FROM Boletas INNER JOIN Productores ON Boletas.productorID = Productores.productorID INNER JOIN Productos ON Boletas.productoID = Productos.productoID WHERE (Boletas.cicloID = @cicloID) AND (Productores.productorID = @productorID) AND (Boletas.boletaID NOT IN (SELECT BoletaID FROM Liquidaciones_Boletas)) ORDER BY Boletas.boletaID">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="ddlCicloBoletaExistente" Name="cicloID" 
                                                                PropertyName="SelectedValue" />
                                                            <asp:ControlParameter ControlID="ddlProductoresBolExistentes" 
                                                                Name="productorID" PropertyName="SelectedValue" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="2">
                                                    <asp:Button ID="btnUpdateBoletasExistentes" runat="server" 
                                                        onclick="btnUpdateBoletasExistentes_Click" Text="Actualizar Lista" />
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Button ID="btnAgregarBoletasaLiq" runat="server" 
                                            onclick="btnAgregarBoletasaLiq_Click" Text="Agregar Boletas A Liquidacion" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                            <cc1:CollapsiblePanelExtender ID="pnlAddBoletaExistente_CollapsiblePanelExtender" 
                                runat="server" CollapseControlID="chkAddBoletaExistente" Collapsed="True" 
                                Enabled="True" ExpandControlID="chkAddBoletaExistente" 
                                TargetControlID="pnlAddBoletaExistente">
                            </cc1:CollapsiblePanelExtender>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            
                            <asp:CheckBox ID="chkAddNewBoleta" runat="server" CssClass="TablaField" 
                                Text="Mostrar Panel para agregar NUEVA boleta a esta Liquidacion" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlNewBoleta" runat="Server">
                               <asp:UpdatePanel ID="updatePanelAddNewBoleta" runat="Server" UpdateMode="Conditional">
                                <ContentTemplate>                              
                                <asp:DetailsView ID="dvNewBoleta" runat="server" AutoGenerateRows="False" 
                                    DataKeyNames="boletaID" DataSourceID="sdsBoletasData" DefaultMode="Insert" 
                                    Height="50px" oniteminserted="dvNewBoleta_ItemInserted" 
                                    oniteminserting="dvNewBoleta_ItemInserting" Width="125px">
                                    <Fields>
                                        <asp:TemplateField HeaderText="Productor" SortExpression="productorID">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("productorID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("productorID") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <asp:DropDownList ID="DropDownList1" runat="server" 
                                                    DataSourceID="sdsProductores" DataTextField="Productor" 
                                                    DataValueField="productorID" SelectedValue='<%# Bind("productorID") %>'>
                                                </asp:DropDownList>
                                            </InsertItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Producto" SortExpression="productoID">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("productoID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("productoID") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <asp:DropDownList ID="DropDownList2" runat="server" 
                                                    DataSourceID="sdsProductosParaNewBoleta" DataTextField="Producto" 
                                                    DataValueField="productoID" SelectedValue='<%# Bind("productoID") %>'>
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsProductosParaNewBoleta" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                    SelectCommand="SELECT Productos.productoID, Productos.productoGrupoID,Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Producto FROM Productos INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID WHERE (Productos.productoGrupoID &lt;&gt; 1) AND (Productos.productoGrupoID &lt;&gt; 4) AND (Productos.productoGrupoID &lt;&gt; 2) ORDER BY Productos.productoGrupoID, Productos.Nombre">
                                                </asp:SqlDataSource>
                                            </InsertItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bodega" SortExpression="bodegaID">
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("bodegaID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("bodegaID") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <asp:DropDownList ID="DropDownList3" runat="server" 
                                                    DataSourceID="sdsBodegasNewBoleta" DataTextField="bodega" 
                                                    DataValueField="bodegaID" SelectedValue='<%# Bind("bodegaID") %>'>
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsBodegasNewBoleta" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                    SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
                                                </asp:SqlDataSource>
                                            </InsertItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Ticket" HeaderText="Ticket" 
                                            SortExpression="Ticket" />
                                        <asp:TemplateField HeaderText="Fecha Entrada" SortExpression="FechaEntrada">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("FechaEntrada") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("FechaEntrada") %>'></asp:TextBox>
                                                <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="TextBox1" 
                                                    Separator="/" />
                                            </InsertItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("FechaEntrada") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PesoDeEntrada" HeaderStyle-Wrap="false" 
                                            HeaderText="Peso De Entrada" SortExpression="PesoDeEntrada">
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Fecha Salida" SortExpression="FechaSalida">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("FechaSalida") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <InsertItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("FechaSalida") %>'></asp:TextBox>
                                                <rjs:PopCalendar ID="PopCalendar3" runat="server" Control="TextBox2" 
                                                    Separator="/" />
                                            </InsertItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("FechaSalida") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PesoDeSalida" HeaderStyle-Wrap="false" 
                                            HeaderText="Peso De Salida" SortExpression="PesoDeSalida">
                                            <HeaderStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="precioapagar" HeaderText="Precio" 
                                            SortExpression="precioapagar" />
                                        <asp:BoundField DataField="humedad" HeaderText="Humedad" 
                                            SortExpression="humedad" />
                                        <asp:BoundField DataField="impurezas" HeaderText="Impurezas" 
                                            SortExpression="impurezas" />
                                        <asp:BoundField DataField="chofer" HeaderText="Chofer" 
                                            SortExpression="chofer" />
                                        <asp:BoundField DataField="placas" HeaderText="Placas" 
                                            SortExpression="placas" />
                                        <asp:CommandField ButtonType="Button" InsertText="Agregar Boleta" 
                                            ShowInsertButton="True" />
                                    </Fields>
                                </asp:DetailsView>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" 
                                    AssociatedUpdatePanelID="updatePanelAddNewBoleta" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                                        Cargando información...
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <asp:Panel ID="pnlAddBoletaResult" runat="Server">
                                    <asp:Label ID="lblAddBoletaResult" runat="server" Font-Bold="True" 
                                        Font-Size="Medium"></asp:Label>
                                </asp:Panel>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                            <cc1:CollapsiblePanelExtender ID="pnlNewBoleta_CollapsiblePanelExtender" 
                                runat="server" CollapseControlID="chkAddNewBoleta" Collapsed="True" 
                                Enabled="True" ExpandControlID="chkAddNewBoleta" TargetControlID="pnlNewBoleta">
                            </cc1:CollapsiblePanelExtender>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">                
                <asp:GridView ID="gvBoletas" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="boletaID" DataSourceID="sdsBoletasData" 
                    ondatabound="gvBoletas_DataBound" onrowdeleted="gvBoletas_RowDeleted" 
                    onrowediting="gvBoletas_RowEditing" onrowupdated="gvBoletas_RowUpdated" 
                    onrowupdating="gvBoletas_RowUpdating" 
                    onselectedindexchanged="gvBoletas_SelectedIndexChanged" ShowFooter="True">
                    <Columns>
                        <asp:CommandField ButtonType="Button" CausesValidation="False" 
                            DeleteText="Quitar de lista" EditText="Editar" ShowDeleteButton="True" 
                            ShowEditButton="True" />
                        <asp:BoundField DataField="boletaID" HeaderText="BoletaID" ReadOnly="True" />
                        <asp:BoundField DataField="Ticket" HeaderText="TICKET" />
                        <asp:TemplateField HeaderText="DESCRIPCION" SortExpression="productoID">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Producto") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlProductoBoleta" runat="server" 
                                    DataSourceID="sdsProductosBoletas" DataTextField="Nombre" 
                                    DataValueField="productoID" SelectedValue='<%# Bind("productoID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsProductosBoletas" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT productoID, Nombre FROM Productos WHERE (productoGrupoID &lt;&gt; 4) ORDER BY Nombre">
                                </asp:SqlDataSource>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PesoDeEntrada" DataFormatString="{0:N2}" 
                            HeaderText="Peso De Entrada" SortExpression="PesoDeEntrada">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PesoDeSalida" DataFormatString="{0:N2}" 
                            HeaderText="Peso De Salida" SortExpression="PesoDeSalida">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Peso Neto" SortExpression="pesonetoentrada">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" 
                                    Text='<%# Eval("pesonetoentrada", "{0:N2}") %>'></asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblPesoNeto" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" 
                                    Text='<%# Bind("pesonetoentrada", "{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HUM.">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("humedad") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("humedad", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DSCTO HUM.">
                            <EditItemTemplate>
                                <asp:Label ID="Label2" runat="server" 
                                    Text='<%# Eval("dctoHumedad", "{0:n}") %>'></asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblDctoHum" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" 
                                    Text='<%# Bind("dctoHumedad", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IMPUREZAS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("impurezas") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("impurezas", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DSCTO IMPUREZAS">
                            <EditItemTemplate>
                                <asp:Label ID="Label3" runat="server" 
                                    Text='<%# Eval("dctoImpurezas", "{0:n}") %>'></asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblDctoImpurezas" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" 
                                    Text='<%# Bind("dctoImpurezas", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="KG NETOS">
                            <EditItemTemplate>
                                <asp:Label ID="Label4" runat="server" 
                                    Text='<%# Eval("pesonetoapagar", "{0:n}") %>'></asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblKgNetos" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" 
                                    Text='<%# Bind("pesonetoapagar", "{0:n}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="precioapagar" DataFormatString="{0:C}" 
                            HeaderText="PRECIO (por KG)" ItemStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="IMPORTE">
                            <EditItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("importe", "{0:c}") %>'></asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblImporte" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label9" runat="server" Text='<%# Bind("importe", "{0:c}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DSCTO SECADO">
                            <EditItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("dctoSecado", "{0:c}") %>'></asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblDctoSecado" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label10" runat="server" 
                                    Text='<%# Bind("dctoSecado", "{0:c}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TOTAL A PAGAR">
                            <EditItemTemplate>
                                <asp:Label ID="Label7" runat="server" 
                                    Text='<%# Eval("totalapagar", "{0:c}") %>'></asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalPagar" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" 
                                    Text='<%# Bind("totalapagar", "{0:c}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="productoID" HeaderText="productoID" 
                            Visible="False" />
                        <asp:TemplateField HeaderText="Aplicar Desc Humedad">
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkApplyHumABol" runat="server" 
                                    Checked='<%# Bind("applyHumedad") %>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" 
                                    Checked='<%# Bind("applyHumedad") %>' Enabled="False" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Aplicar Desc Impurezas">
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkApplyImpABol" runat="server" 
                                    Checked='<%# Bind("applyImpurezas") %>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox2" runat="server" 
                                    Checked='<%# Bind("applyImpurezas") %>' Enabled="False" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Aplicar Secado">
                            <EditItemTemplate>
                                <asp:CheckBox ID="chkApplySecadoABoleta" runat="server" 
                                    Checked='<%# Bind("applySecado") %>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox3" runat="server" 
                                    Checked='<%# Bind("applySecado") %>' Enabled="False" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bodega">
                            <EditItemTemplate>
                                <asp:DropDownList ID="drpdlBodega" runat="server" DataSourceID="sdsBodegaGrid" 
                                    DataTextField="bodega" DataValueField="bodegaID">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsBodegaGrid" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas]">
                                </asp:SqlDataSource>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("bodega") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="bodegaID" HeaderText="bodegaID" Visible="false" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsBoletasData" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    DeleteCommand="DELETE FROM Liquidaciones_Boletas WHERE (BoletaID = @BoletaID)" 
                    InsertCommand="INSERT INTO Boletas(userID, productorID, productoID, bodegaID, Ticket, placas, FechaEntrada, PesoDeEntrada, FechaSalida, PesoDeSalida, humedad, impurezas, precioapagar, chofer, applyHumedad, applyImpurezas, applySecado, dctoHumedad, dctoImpurezas, dctoSecado, pesonetoentrada, pesonetoapagar, cicloID, totalapagar, importe) VALUES (@userID, @productorID, @productoID, @bodegaID, @Ticket, @placas, @FechaEntrada, @PesoDeEntrada, @FechaSalida, @PesoDeSalida, @humedad, @impurezas, @precioapagar, @chofer, @applyHumedad, @applyImpurezas, @applySecado, @dctoHumedad, @dctoImpurezas, @dctoSecado, @pesonetoentrada, @pesonetoapagar, @cicloID, @totalapagar, @importe); insert into Liquidaciones_Boletas(LiquidacionID, BoletaID) select @LiquidacionID, IDENT_CURRENT('Boletas');" 
                    SelectCommand="SELECT     Boletas.boletaID, Boletas.cicloID, Boletas.userID, Boletas.productorID, Boletas.productoID, Boletas.bodegaID, Boletas.NumeroBoleta, Boletas.Ticket,  Boletas.codigoClienteProvArchivo, Boletas.NombreProductor, Boletas.placas, Boletas.FechaEntrada, Boletas.PesadorEntrada, Boletas.PesoDeEntrada,  Boletas.BasculaEntrada, Boletas.FechaSalida, Boletas.PesoDeSalida, Boletas.PesadorSalida, Boletas.BasculaSalida, Boletas.pesonetoentrada,  Boletas.pesonetosalida, Boletas.humedad, Boletas.dctoHumedad, Boletas.impurezas, Boletas.dctoImpurezas, Boletas.totaldescuentos, Boletas.pesonetoapagar,  Boletas.precioapagar, Boletas.importe, Boletas.dctoSecado, Boletas.totalapagar, Boletas.chofer, Boletas.pagada, Boletas.storeTS, Boletas.updateTS,  Productos.Nombre AS Producto, Boletas.applyHumedad, Boletas.applyImpurezas, Boletas.applySecado, Bodegas.bodega FROM Boletas INNER JOIN Liquidaciones_Boletas ON Boletas.boletaID = Liquidaciones_Boletas.BoletaID INNER JOIN Productos ON Boletas.productoID = Productos.productoID INNER JOIN Bodegas ON Boletas.bodegaID = Bodegas.bodegaID WHERE (Liquidaciones_Boletas.LiquidacionID = @LiqID)" 
                    UpdateCommand="UPDATE Boletas SET productoID = @productoID, Ticket = @Ticket, pesonetoentrada = @pesonetoentrada, humedad = @humedad, dctoHumedad = @dctoHumedad, impurezas = @impurezas, dctoImpurezas = @dctoImpurezas, totaldescuentos = @totaldescuentos, pesonetoapagar = @pesonetoapagar, precioapagar = @precioapagar, importe = @importe, dctoSecado = @dctoSecado, totalapagar = @totalapagar, applyHumedad = @applyHumedad, applyImpurezas = @applyImpurezas, applySecado = @applySecado, PesoDeEntrada = @PesoDeEntrada, PesoDeSalida = @PesoDeSalida WHERE (boletaID = @boletaID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblNumLiquidacion" Name="LiqID" 
                            PropertyName="Text" />
                    </SelectParameters>
                    <DeleteParameters>
                        <asp:Parameter Name="BoletaID" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="productoID" />
                        <asp:Parameter Name="Ticket" />
                        <asp:Parameter Name="pesonetoentrada" />
                        <asp:Parameter Name="humedad" />
                        <asp:Parameter Name="dctoHumedad" />
                        <asp:Parameter Name="impurezas" />
                        <asp:Parameter Name="dctoImpurezas" />
                        <asp:Parameter Name="totaldescuentos" />
                        <asp:Parameter Name="pesonetoapagar" />
                        <asp:Parameter Name="precioapagar" />
                        <asp:Parameter Name="importe" />
                        <asp:Parameter Name="dctoSecado" />
                        <asp:Parameter Name="totalapagar" />
                        <asp:Parameter Name="applyHumedad" />
                        <asp:Parameter Name="applyImpurezas" />
                        <asp:Parameter Name="applySecado" />
                        <asp:Parameter Name="PesoDeEntrada" />
                        <asp:Parameter Name="PesoDeSalida" />
                        <asp:Parameter Name="boletaID" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="userID" />
                        <asp:Parameter Name="productorID" />
                        <asp:Parameter Name="productoID" />
                        <asp:Parameter Name="bodegaID" />
                        <asp:Parameter Name="Ticket" />
                        <asp:Parameter Name="placas" />
                        <asp:Parameter Name="FechaEntrada" />
                        <asp:Parameter Name="PesoDeEntrada" />
                        <asp:Parameter Name="FechaSalida" />
                        <asp:Parameter Name="PesoDeSalida" />
                        <asp:Parameter Name="humedad" />
                        <asp:Parameter Name="impurezas" />
                        <asp:Parameter Name="precioapagar" />
                        <asp:Parameter Name="chofer" />
                        <asp:Parameter Name="applyHumedad" />
                        <asp:Parameter Name="applyImpurezas" />
                        <asp:Parameter Name="applySecado" />
                        <asp:Parameter Name="dctoHumedad" />
                        <asp:Parameter Name="dctoImpurezas" />
                        <asp:Parameter Name="dctoSecado" />
                        <asp:Parameter Name="pesonetoentrada" />
                        <asp:Parameter Name="pesonetoapagar" />
                        <asp:Parameter Name="cicloID" />
                        <asp:Parameter Name="totalapagar" />
                        <asp:Parameter Name="importe" />
                        <asp:Parameter Name="LiquidacionID" />
                    </InsertParameters>
                </asp:SqlDataSource>
                <asp:Panel ID="pnlBoletaGridViewResult" runat="Server">
                    <asp:Label ID="lblBoletaGridViewResult" runat="server" Text=""></asp:Label>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;</td>
            <td align="left">
                <table width="100%">
                    <tr>
                        <td>
                            <table>
                                <tr >
                                    <td class="TablaField">
                                        ANTICIPOS:
                                        </td>
                                </tr>
                                <tr >
                                    <td>
                               </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvAnticipos" runat="server" AutoGenerateColumns="False" 
                                            DataKeyNames="anticipoID" DataSourceID="sdsAnticipos" 
                                            onrowdeleted="gvAnticipos_RowDeleted" ondatabound="gvAnticipos_DataBound">
                                            <Columns>
                                                <asp:CommandField ButtonType="Button" DeleteText="Quitar" 
                                                    ShowDeleteButton="True" />
                                                <asp:BoundField DataField="anticipoID" HeaderText="# Anticipo" 
                                                    InsertVisible="False" ReadOnly="True" SortExpression="anticipoID">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                                    HeaderText="Fecha" SortExpression="fecha" />
                                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="True" 
                                                    SortExpression="Nombre" />
                                                <asp:BoundField DataField="monto" DataFormatString="{0:C2}" HeaderText="Monto" 
                                                    SortExpression="monto">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="sdsAnticipos" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            DeleteCommand="DELETE FROM Liquidaciones_Anticipos WHERE (Anticipos = @anticipoID)" 
                                            SelectCommand="SELECT Anticipos.anticipoID, Anticipos.fecha, LTRIM(Productores.apaterno + SPACE(1) + Productores.amaterno + SPACE(1) + Productores.nombre) AS Nombre, Anticipos.monto FROM Anticipos INNER JOIN Liquidaciones_Anticipos ON Anticipos.anticipoID = Liquidaciones_Anticipos.Anticipos INNER JOIN Productores ON Anticipos.productorID = Productores.productorID WHERE (Anticipos.tipoAnticipoID = 1) AND (Liquidaciones_Anticipos.LiquidacionID = @LiquidacionID)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="txtLiquidacionID" Name="LiquidacionID" 
                                                    PropertyName="Text" />
                                            </SelectParameters>
                                            <DeleteParameters>
                                                <asp:Parameter Name="anticipoID" />
                                            </DeleteParameters>
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlAnticiposGridViewResult" runat="Server">
                                            <asp:Label ID="lblAnticiposGridViewResult" runat="server"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                             <asp:CheckBox ID="CheckBoxMostrarAnticipos" runat="server"  CssClass="TablaField"
                                                 Text="Agregar anticipos a la liquidación" />
                                                        <asp:Panel ID="PanelMostrarAnticipos" runat="server">
                                                        <table>
    <tr>
        <td class="TablaField">Ciclo:</td><td>
            <asp:DropDownList ID="ddlCiclosAnticipos" runat="server" DataSourceID="SqlDataSourceCiclosAnticiposDisponibles" 
                DataTextField="CicloName" DataValueField="cicloID" AutoPostBack="True" 
                onselectedindexchanged="ddlCiclosAnticipos_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSourceCiclosAnticiposDisponibles" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                SelectCommand="SELECT        cicloID, CicloName
    FROM            Ciclos
    WHERE cerrado=@cerrado
    ORDER BY fechaInicio DESC">
			    <SelectParameters>
				    <asp:Parameter DefaultValue="FALSE" Name="cerrado" />
			    </SelectParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
                                                            <tr>
                                                                <td class="TablaField">
                                                                    PRODUCTOR:</td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlProductoresAnticipos" runat="server" 
                                                                        AutoPostBack="True" DataSourceID="sdsProductoresAnticipos" 
                                                                        DataTextField="Productor" DataValueField="productorID" 
                                                                        onselectedindexchanged="ddlProductoresAnticipos_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:SqlDataSource ID="sdsProductoresAnticipos" runat="server" 
                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                        
                                                                        SelectCommand="SELECT DISTINCT Productores.productorID, LTRIM(Productores.apaterno + SPACE(1) + Productores.amaterno + SPACE(1) + Productores.nombre) AS Productor FROM Productores INNER JOIN Anticipos ON Productores.productorID = Anticipos.productorID WHERE (Anticipos.anticipoID NOT IN (SELECT Anticipos FROM Liquidaciones_Anticipos)) AND (Anticipos.tipoAnticipoID = 1) ORDER BY Productor">
                                                                    </asp:SqlDataSource>
                                                                </td>
                                                            </tr>
                                                          
    </table>
    <asp:GridView ID="GridViewAnticiposDisponibles" runat="server" AutoGenerateColumns="False" 
    DataKeyNames="anticipoID" DataSourceID="SqlDataSourceAnticiposDisponibles">
    <Columns>
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <asp:CheckBox ID="chkRowSelected" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="anticipoID" HeaderText="# Anticipo" 
            InsertVisible="False" ReadOnly="True" SortExpression="anticipoID" />
        <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
            SortExpression="Productor" />
        <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" ReadOnly="True" 
            SortExpression="Cuenta" />
        <asp:BoundField DataField="Concepto" HeaderText="Concepto" 
            SortExpression="Concepto" />
        <asp:BoundField DataField="numCheque" HeaderText="# Cheque" 
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
            SortExpression="tipoAnticipo" Visible="False" />
        <asp:CheckBoxField DataField="cobrada" HeaderText="Cobrada" 
            SortExpression="cobrada" Visible="False">
        <ItemStyle HorizontalAlign="Center" />
        </asp:CheckBoxField>
    </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceAnticiposDisponibles" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                
                                
                                



                                                                
                                                                SelectCommand="SELECT Anticipos.anticipoID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS Productor, Bancos.nombre + ' ' + CuentasDeBanco.NumeroDeCuenta AS Cuenta, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.cargo + MovimientosCuentasBanco.abono AS Monto, MovimientosCuentasBanco.numCheque, MovimientosCaja.cargo + MovimientosCaja.abono AS Efectivo, TiposAnticipos.tipoAnticipo, Productores.apaterno, Productores.amaterno, Productores.nombre, Liquidaciones.LiquidacionID, Liquidaciones.cobrada, Anticipos.productorID FROM Liquidaciones INNER JOIN Liquidaciones_Anticipos ON Liquidaciones.LiquidacionID = Liquidaciones_Anticipos.LiquidacionID RIGHT OUTER JOIN TiposAnticipos INNER JOIN Anticipos INNER JOIN Productores ON Anticipos.productorID = Productores.productorID ON TiposAnticipos.tipoAnticipoID = Anticipos.tipoAnticipoID ON Liquidaciones_Anticipos.Anticipos = Anticipos.anticipoID LEFT OUTER JOIN CuentasDeBanco INNER JOIN MovimientosCuentasBanco ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco.cuentaID INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID ON Anticipos.movbanID = MovimientosCuentasBanco.movbanID LEFT OUTER JOIN MovimientosCaja ON Anticipos.movimientoID = MovimientosCaja.movimientoID WHERE (Anticipos.cicloID = @cicloID) AND (Anticipos.tipoAnticipoID = 1) AND (Liquidaciones_Anticipos.LiquidacionID IS NULL) AND (Anticipos.productorID = @productorID) ORDER BY Productor">
    <SelectParameters>
        <asp:ControlParameter ControlID="ddlCiclosAnticipos" DefaultValue="-1" 
                                        Name="cicloID" 
            PropertyName="SelectedValue" />
        <asp:ControlParameter ControlID="ddlProductoresAnticipos" DefaultValue="" 
            Name="productorID" PropertyName="SelectedValue" />
    </SelectParameters>
    </asp:SqlDataSource>
                                                            <asp:Button ID="btnAgregarAnticipos" runat="server" 
                                                                onclick="btnAgregarAnticipos_Click" Text="Agregar anticipos" />
                                                        </asp:Panel>
                                                        <cc1:CollapsiblePanelExtender ID="PanelMostrarAnticipos_CollapsiblePanelExtender" 
                                                            runat="server" CollapseControlID="CheckBoxMostrarAnticipos" Enabled="True" 
                                                            ExpandControlID="CheckBoxMostrarAnticipos" 
                                                            TargetControlID="PanelMostrarAnticipos" Collapsed="True">
                                                        </cc1:CollapsiblePanelExtender>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="TablaField">
                                        CREDITOS A ABONAR</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvCreditosEnLiquidacion" runat="server" 
                                            AutoGenerateColumns="False" DataKeyNames="creditoID" 
                                            DataSourceID="sdsCreditosEnLiquidacion" 
                                            onrowdeleted="gvCreditosEnLiquidacion_RowDeleted" 
                                            ondatabound="gvCreditosEnLiquidacion_DataBound">
                                            <Columns>
                                                <asp:CommandField ButtonType="Button" DeleteText="Eliminar Pago A Credito" 
                                                    ShowDeleteButton="True">
                                                    <ItemStyle Wrap="False" />
                                                </asp:CommandField>
                                                <asp:BoundField DataField="creditoID" HeaderText="CreditoID" ReadOnly="True" 
                                                    SortExpression="creditoID" />
                                                <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                                                    SortExpression="Productor" />
                                                <asp:BoundField DataField="pago" DataFormatString="{0:C2}" 
                                                    HeaderText="Abono al credito" SortExpression="pago">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="sdsCreditosEnLiquidacion" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            DeleteCommand="ELIMINACREDITODELIQUIDACION" 
                                            SelectCommand="SELECT Liquidaciones_Creditos.creditoID, LTRIM(Productores.apaterno + SPACE(1) + Productores.amaterno + SPACE(1) + Productores.nombre) AS Productor, Liquidaciones_Creditos.pago, Liquidaciones_Creditos.LiquidacionID FROM Liquidaciones_Creditos INNER JOIN Creditos ON Liquidaciones_Creditos.creditoID = Creditos.creditoID INNER JOIN Productores ON Creditos.productorID = Productores.productorID WHERE (Liquidaciones_Creditos.LiquidacionID = @LiquidacionID)" 
                                            
                                            UpdateCommand="UPDATE Liquidaciones_Creditos SET pago = @pago WHERE (creditoID = @creditoID)" 
                                            DeleteCommandType="StoredProcedure">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="txtLiquidacionID" Name="liquidacionID" 
                                                    PropertyName="Text" />
                                            </SelectParameters>
                                            <DeleteParameters>
                                                <asp:Parameter Name="creditoID" />
                                                <asp:ControlParameter ControlID="txtLiquidacionID" Name="liquidacionID" 
                                                    PropertyName="Text" />
                                            </DeleteParameters>
                                            <UpdateParameters>
                                                <asp:Parameter Name="pago" />
                                                <asp:Parameter Name="creditoID" />
                                            </UpdateParameters>
                                        </asp:SqlDataSource>
                                        <asp:Panel ID="pnlCreditosGridViewResult" runat="Server">
                                            <asp:Label ID="lblCreditosGridViewResult" runat="server"></asp:Label>
                                        </asp:Panel>
                                        <asp:CheckBox ID="chkAddCreditoALiq" runat="server" CssClass="TablaField" 
                                            Text="Agregar Credito a Abonar" />
                                        <asp:Panel ID="pnlAddCreditoALiq" runat="Server">
                                            <asp:UpdatePanel ID="updatePanelCreditos" runat="server" UpdateMode="Conditional">                                         
                                            <ContentTemplate>
                                              <table>
                                                <tr>
                                                    <td class="TablaField">
                                                        CICLO:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCicloParaCreditoParaLiq" runat="server" 
                                                            AutoPostBack="True" DataSourceID="sdsCiclosParaCreditosParaLiq" 
                                                            DataTextField="CicloName" DataValueField="cicloID" 
                                                            onselectedindexchanged="ddlCicloParaCreditoParaLiq_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="sdsCiclosParaCreditosParaLiq" runat="server" 
                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                            SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [CicloName] DESC">
                                                        </asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TablaField">
                                                        CREDITO:</td>
                                                    <td>
                                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" 
                                                            AssociatedUpdatePanelID="updatePanelCreditos" DisplayAfter="0">
                                                            <ProgressTemplate>
                                                                <asp:Image ID="Image6" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                                                                Cargando información...
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                        <asp:DropDownList ID="ddlCreditosDisponiblesParaPagar" runat="server"
                                                           DataSourceID="sdsCreditosDisponiblesParaPagar" DataTextField="Credito" 
                                                            DataValueField="CreditoId">
                                                        </asp:DropDownList>
                                                        <br />
                                                        Saldo: <asp:Label runat="server" ID="labelSaldoCredito" Font-Bold="true" Font-Size="X-Large" Text="--Seleccione un crédito--"></asp:Label>
                                                        <asp:SqlDataSource ID="sdsCreditosDisponiblesParaPagar" runat="server" 
                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                            SelectCommand="SELECT CAST(CreditoId as VARCHAR) + ' - '  + Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.Nombre as Credito, Creditos.CreditoId FROM Creditos
    INNER JOIN Productores ON Productores.ProductorId = Creditos.ProductorId
    WHERE cicloId = @cicloId 
    ORDER BY apaterno, amaterno, nombre">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="ddlCicloParaCreditoParaLiq" Name="cicloId" 
                                                                    PropertyName="SelectedValue" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TablaField">
                                                        ABONO A PAGAR:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtAbonoACredito" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TablaField">
                                                        MARCAR EL CREDITO COMO PAGADO:</td>
                                                    <td>
                                                        <asp:CheckBox ID="chkMarcarCreditoPagado" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Panel ID="pnlAddCreditoALiqResult" runat="Server" Visible="False">
                                                            <asp:Label ID="lblpnlAddCreditoALiqResult" runat="server" Text="Label"></asp:Label>
                                                        </asp:Panel>
                                                        <asp:Button ID="btnAgregarPagoACredito" runat="server" 
                                                            onclick="btnAgregarPagoACredito_Click" Text="Agregar Abono a Credito" />
                                                    </td>
                                                </tr>
                                            </table>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>                                          
                                        </asp:Panel>
                                        <cc1:CollapsiblePanelExtender ID="pnlAddCreditoALiq_CollapsiblePanelExtender" 
                                            runat="server" CollapseControlID="chkAddCreditoALiq" Collapsed="True" 
                                            Enabled="True" ExpandControlID="chkAddCreditoALiq" 
                                            TargetControlID="pnlAddCreditoALiq">
                                        </cc1:CollapsiblePanelExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="TablaField">
                                        PAGOS A LA LIQUIDACION</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvPagosLiquidacion" runat="server" 
                                            AutoGenerateColumns="False" DataKeyNames="pagoLiqID" 
                                            DataSourceID="sdsPagosDeLiquidacion" ondatabound="gvPagosLiquidacion_DataBound" 
                                            onrowdeleted="gvPagosLiquidacion_RowDeleted">
                                            <Columns>
                                                <asp:CommandField ButtonType="Button" DeleteText="Eliminar Pago" 
                                                    ShowDeleteButton="True" />
                                                <asp:BoundField DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                                    HeaderText="Fecha" ReadOnly="True" SortExpression="Fecha">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                                                    SortExpression="Productor" />
                                                <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" ReadOnly="True" 
                                                    SortExpression="Cuenta" />
                                                <asp:BoundField DataField="Concepto" HeaderText="Concepto" ReadOnly="True" 
                                                    SortExpression="Concepto" />
                                                <asp:BoundField DataField="numCheque" HeaderText="Num Cheque" 
                                                    SortExpression="numCheque">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Monto" DataFormatString="{0:C2}" HeaderText="Monto" 
                                                    ReadOnly="True" SortExpression="Monto" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink2" runat="server" 
                                                            NavigateUrl='<%# GetChequePrintLink(Eval("movbanID").ToString()) %>' 
                                                            onclick='<%# GetChequePrintLink(Eval("movbanID").ToString()) %>' 
                                                            Visible='<%# GetChequeVisible(Eval("numCheque").ToString()) %>'>IMPRIMIR</asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="sdsPagosDeLiquidacion" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            DeleteCommand="ELIMINAPAGOLIQUIDACION" DeleteCommandType="StoredProcedure" 
                                            SelectCommand="SELECT PagosLiquidacion.pagoLiqID, PagosLiquidacion.liquidacionID, ISNULL(MovimientosCuentasBanco.fecha, MovimientosCaja.fecha) AS Fecha, ISNULL(MovimientosCuentasBanco.nombre, MovimientosCaja.nombre) AS Productor, Bancos.nombre + SPACE(1) + CuentasDeBanco.NumeroDeCuenta AS Cuenta, ISNULL(ConceptosMovCuentas.Concepto, 'EFECTIVO') AS Concepto, MovimientosCuentasBanco.numCheque, ISNULL(MovimientosCuentasBanco.cargo, MovimientosCaja.cargo) AS Monto, MovimientosCuentasBanco.movbanID FROM CuentasDeBanco INNER JOIN MovimientosCuentasBanco ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco.cuentaID INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID RIGHT OUTER JOIN PagosLiquidacion LEFT OUTER JOIN MovimientosCaja ON PagosLiquidacion.movimientoID = MovimientosCaja.movimientoID ON MovimientosCuentasBanco.movbanID = PagosLiquidacion.movbanID WHERE (PagosLiquidacion.liquidacionID = @liquidacionID)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="txtLiquidacionID" Name="liquidacionID" 
                                                    PropertyName="Text" />
                                            </SelectParameters>
                                            <DeleteParameters>
                                                <asp:Parameter Name="pagoLiqID" />
                                            </DeleteParameters>
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkAddNewPago" runat="server" CssClass="TablaField" 
                                            Text="Agregar Nuevo Pago" />
                                        <asp:Panel ID="panelNuevoPago" runat="Server">
                                            <asp:UpdatePanel ID="UpdateAddNewPago" runat="Server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div ID="divAgregarNuevoPago" runat="Server">
                                                        <table>
                                                            <tr>
                                                                <td class="TablaField">
                                                                    Productor:</td>
                                                                <td>
                                                                    <br />
                                                                    <asp:DropDownList ID="cmbProductoresPago" runat="server" 
                                                                        DataSourceID="sdsProductoresPago" DataTextField="name" 
                                                                        DataValueField="productorID">
                                                                    </asp:DropDownList>
                                                                    <cc1:ListSearchExtender ID="cmbProductoresPago_ListSearchExtender" 
                                                                        runat="server" Enabled="True" PromptText="Escriba para Buscar" 
                                                                        TargetControlID="cmbProductoresPago">
                                                                    </cc1:ListSearchExtender>
                                                                    <asp:SqlDataSource ID="sdsProductoresPago" runat="server" 
                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                        SelectCommand="Select LTRIM(productores.apaterno  + SPACE(1) + productores.amaterno + SPACE(1) + productores.nombre) as name, productores.productorID from Productores  order by name">
                                                                    </asp:SqlDataSource>
                                                                </td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TablaField">
                                                                    Fecha:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFechaPago" runat="server" ReadOnly="True"></asp:TextBox>
                                                                    <rjs:PopCalendar ID="PopCalendar3" runat="server" Control="txtFechaPago" 
                                                                        Separator="/" />
                                                                </td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TablaField">
                                                                    Tipo de pago:</td>
                                                                <td>
                                                                    <asp:DropDownList ID="cmbTipodeMovPago" runat="server" Height="22px" 
                                                                        Width="249px">
                                                                        <asp:ListItem Value="0">EFECTIVO</asp:ListItem>
                                                                        <asp:ListItem>MOVIMIENTO DE BANCO</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TablaField">
                                                                    <asp:Label ID="lblNombre0" runat="server" Text="Nombre:"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNombrePago" runat="server" Width="266px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TablaField">
                                                                    Monto:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtMonto" runat="server" Width="266px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TablaField" colspan="3">
                                                                    <div ID="divPagoMovCaja" runat="Server">
                                                                        <table>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    El pago se hará de la caja:</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlPagosBodegas" runat="server" 
                                                                                        DataSourceID="sdsPagosBodegas" DataTextField="bodega" DataValueField="bodegaID">
                                                                                    </asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="sdsPagosBodegas" runat="server" 
                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                        
                                                                                        
                                                                                        SelectCommand="SELECT bodegaID, bodega FROM dbo.Bodegas WHERE (bodegaID &lt;&gt; 1) ORDER BY bodegaID DESC">
                                                                                    </asp:SqlDataSource>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    Grupo de catálogos:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drpdlGrupoCatalogosCajaChica" runat="server" 
                                                                                        AutoPostBack="True" DataSourceID="sdsGruposCatalogosCajaChica" 
                                                                                        DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID" 
                                                                                        onselectedindexchanged="drpdlGrupoCatalogosCajaChica_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="sdsGruposCatalogosCajaChica" runat="server" 
                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                        SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos] ORDER BY [grupoCatalogo]">
                                                                                    </asp:SqlDataSource>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    Catálogo de cuenta:</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drpdlCatalogocuentaCajaChica" runat="server" 
                                                                                        AutoPostBack="True" DataSourceID="sdsCatalogoCuentaCajaChica" 
                                                                                        DataTextField="catalogoMovBanco" DataValueField="catalogoMovBancoID" 
                                                                                        onselectedindexchanged="drpdlCatalogocuentaCajaChica_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="sdsCatalogoCuentaCajaChica" runat="server" 
                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                        SelectCommand="SELECT catalogoMovBancoID, catalogoMovBanco FROM catalogoMovimientosBancos WHERE (grupoCatalogoID = @grupoCatalogoID) ORDER BY catalogoMovBanco">
                                                                                        <SelectParameters>
                                                                                            <asp:ControlParameter ControlID="drpdlGrupoCatalogosCajaChica" 
                                                                                                DefaultValue="-1" Name="grupoCatalogoID" PropertyName="SelectedValue" />
                                                                                        </SelectParameters>
                                                                                    </asp:SqlDataSource>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    Subcatálogo de cuenta:</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drpdlSubcatalogoCajaChica" runat="server" 
                                                                                        DataSourceID="sdsSubcatalogoCajaChica" DataTextField="subCatalogo" 
                                                                                        DataValueField="subCatalogoMovBancoID">
                                                                                    </asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="sdsSubcatalogoCajaChica" runat="server" 
                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                        SelectCommand="SELECT SubCatalogoMovimientoBanco.subCatalogo, SubCatalogoMovimientoBanco.subCatalogoMovBancoID FROM SubCatalogoMovimientoBanco INNER JOIN catalogoMovimientosBancos ON SubCatalogoMovimientoBanco.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID WHERE (SubCatalogoMovimientoBanco.catalogoMovBancoID = @catalogoMovBancoID) ORDER BY SubCatalogoMovimientoBanco.subCatalogo">
                                                                                        <SelectParameters>
                                                                                            <asp:ControlParameter ControlID="drpdlCatalogocuentaCajaChica" 
                                                                                                DefaultValue="-1" Name="catalogoMovBancoID" PropertyName="SelectedValue" />
                                                                                        </SelectParameters>
                                                                                    </asp:SqlDataSource>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                    <div ID="divMovBanco" runat="Server">
                                                                        <table border="1">
                                                                            <tr>
                                                                                <td align="center" class="TableHeader" colspan="2">
                                                                                    DATOS MOVIMIENTO DE BANCO</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    Concepto:</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="cmbConceptomovBancoPago" runat="server" 
                                                                                        DataSourceID="sdsConceptoPago" DataTextField="Concepto" 
                                                                                        DataValueField="ConceptoMovCuentaID">
                                                                                    </asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="sdsConceptoPago" runat="server" 
                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                        SelectCommand="SELECT [ConceptoMovCuentaID], [Concepto] FROM [ConceptosMovCuentas]  Where ConceptoMovCuentaID NOT IN (4,5,6,7) ORDER BY [Concepto]">
                                                                                    </asp:SqlDataSource>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    Cuenta:</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="cmbCuentaPago" runat="server" 
                                                                                        DataSourceID="sdsCuentaPago" DataTextField="cuenta" DataValueField="cuentaID">
                                                                                    </asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="sdsCuentaPago" runat="server" 
                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                        SelectCommand="SELECT Bancos.nombre + '  ' + CuentasDeBanco.NumeroDeCuenta + ' - ' + CuentasDeBanco.Titular AS cuenta, CuentasDeBanco.cuentaID FROM Bancos INNER JOIN CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID ORDER BY cuenta">
                                                                                    </asp:SqlDataSource>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    Grupo de catálogos de cuenta fiscal:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drpdlGrupoCuentaFiscal" runat="server" 
                                                                                        AutoPostBack="True" DataSourceID="sdsGruposCatalogosfiscalPago" 
                                                                                        DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID" 
                                                                                        onselectedindexchanged="drpdlGrupoCuentaFiscal_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="sdsGruposCatalogosfiscalPago" runat="server" 
                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                        SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos]">
                                                                                    </asp:SqlDataSource>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    Catálogo de cuenta fiscal:</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drpdlCatalogocuentafiscalPago" runat="server" 
                                                                                        AutoPostBack="True" DataSourceID="sdsCatalogoCuentaFiscal" 
                                                                                        DataTextField="catalogoMovBanco" DataValueField="catalogoMovBancoID" 
                                                                                        onselectedindexchanged="drpdlCatalogocuentafiscalPago_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="sdsCatalogoCuentaFiscal" runat="server" 
                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                        SelectCommand="SELECT catalogoMovBancoID, catalogoMovBanco FROM catalogoMovimientosBancos WHERE (grupoCatalogoID = @grupoCatalogoID)">
                                                                                        <SelectParameters>
                                                                                            <asp:ControlParameter ControlID="drpdlGrupoCuentaFiscal" DefaultValue="-1" 
                                                                                                Name="grupoCatalogoID" PropertyName="SelectedValue" />
                                                                                        </SelectParameters>
                                                                                    </asp:SqlDataSource>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    Subcatálogo de cuenta fiscal:</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drpdlSubcatalogofiscalPago" runat="server" 
                                                                                        DataSourceID="sdsSubcatalogofiscalPago" DataTextField="subCatalogo" 
                                                                                        DataValueField="subCatalogoMovBancoID">
                                                                                    </asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="sdsSubcatalogofiscalPago" runat="server" 
                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                        SelectCommand="SELECT SubCatalogoMovimientoBanco.subCatalogo, SubCatalogoMovimientoBanco.subCatalogoMovBancoID FROM SubCatalogoMovimientoBanco INNER JOIN catalogoMovimientosBancos ON SubCatalogoMovimientoBanco.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID WHERE (SubCatalogoMovimientoBanco.catalogoMovBancoID = @catalogoMovBancoID)">
                                                                                        <SelectParameters>
                                                                                            <asp:ControlParameter ControlID="drpdlCatalogocuentafiscalPago" 
                                                                                                DefaultValue="-1" Name="catalogoMovBancoID" PropertyName="SelectedValue" />
                                                                                        </SelectParameters>
                                                                                    </asp:SqlDataSource>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField" colspan="2">
                                                                                    <div ID="divCheque" runat="server">
                                                                                        <table border="1">
                                                                                            <tr>
                                                                                                <td align="center" class="TableHeader" colspan="2">
                                                                                                    DATOS DE CHEQUE</td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TablaField">
                                                                                                    # Cheque (*):</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtChequeNum" runat="server"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TablaField">
                                                                                                    # Factura o Larguillo:</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtFacturaLarguillo" runat="server"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TablaField">
                                                                                                    Nombre interno:</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtChequeNombre0" runat="server" Width="282px"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TablaField">
                                                                                                    Grupo de catálogos de cuenta interna:</td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="drpdlGrupoCatalogosInternaPago" runat="server" 
                                                                                                        AutoPostBack="True" DataSourceID="sdsGruposCatalogosInternaPago" 
                                                                                                        DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID" 
                                                                                                        onselectedindexchanged="drpdlGrupoCatalogosInternaPago_SelectedIndexChanged">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:SqlDataSource ID="sdsGruposCatalogosInternaPago" runat="server" 
                                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                        SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos]">
                                                                                                    </asp:SqlDataSource>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TablaField">
                                                                                                    Catálogo de cuenta interna:</td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="drpdlCatalogoInternoPago" runat="server" 
                                                                                                        AutoPostBack="True" DataSourceID="sdsCatalogoCuentaInternaPago" 
                                                                                                        DataTextField="catalogoMovBanco" DataValueField="catalogoMovBancoID" 
                                                                                                        onselectedindexchanged="drpdlCatalogoInternoPago_SelectedIndexChanged">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:SqlDataSource ID="sdsCatalogoCuentaInternaPago" runat="server" 
                                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                        SelectCommand="SELECT catalogoMovBancoID, catalogoMovBanco FROM catalogoMovimientosBancos WHERE (grupoCatalogoID = @grupoCatalogoID)">
                                                                                                        <SelectParameters>
                                                                                                            <asp:ControlParameter ControlID="drpdlGrupoCatalogosInternaPago" 
                                                                                                                DefaultValue="-1" Name="grupoCatalogoID" PropertyName="SelectedValue" />
                                                                                                        </SelectParameters>
                                                                                                    </asp:SqlDataSource>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TablaField">
                                                                                                    Subcatálogo de cuenta interna:</td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="drpdlSubcatologointernaPago" runat="server" 
                                                                                                        DataSourceID="sdsSubCatalogoInternaPago" DataTextField="subCatalogo" 
                                                                                                        DataValueField="subCatalogoMovBancoID">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:SqlDataSource ID="sdsSubCatalogoInternaPago" runat="server" 
                                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                        SelectCommand="SELECT SubCatalogoMovimientoBanco.subCatalogo, SubCatalogoMovimientoBanco.subCatalogoMovBancoID FROM SubCatalogoMovimientoBanco INNER JOIN catalogoMovimientosBancos ON SubCatalogoMovimientoBanco.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID WHERE (SubCatalogoMovimientoBanco.catalogoMovBancoID = @catalogoMovBancoID)">
                                                                                                        <SelectParameters>
                                                                                                            <asp:ControlParameter ControlID="drpdlCatalogoInternoPago" DefaultValue="-1" 
                                                                                                                Name="catalogoMovBancoID" PropertyName="SelectedValue" />
                                                                                                        </SelectParameters>
                                                                                                    </asp:SqlDataSource>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                    <asp:Panel ID="pnlNewPagoResult" runat="server">
                                                                        <asp:Image ID="imgBienPago" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                                                                        <asp:Image ID="imgMalPago" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                                                                        <asp:Label ID="lblNewPagoResult" runat="server"></asp:Label>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                    <asp:UpdateProgress ID="UpProgPagos" runat="server" 
                                                                        AssociatedUpdatePanelID="UpdateAddNewPago" DisplayAfter="0">
                                                                        <ProgressTemplate>
                                                                            <asp:Image ID="Image35" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                                                                            Procesando informacion de pago...
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                    <asp:Button ID="btnAddPago" runat="server" onclick="btnAddPago_Click" 
                                                                        Text="Agregar Pago a la liquidacion" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                        <cc1:CollapsiblePanelExtender ID="panelNuevoPago_CollapsiblePanelExtender" 
                                            runat="server" CollapseControlID="chkAddNewPago" Collapsed="True" 
                                            Enabled="True" ExpandControlID="chkAddNewPago" TargetControlID="panelNuevoPago">
                                        </cc1:CollapsiblePanelExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="right" valign="top">
                <table>
                    <tr>
                        <td>
                          <asp:DetailsView ID="dvTotalesLiq" runat="server" AutoGenerateRows="False" 
                                DataKeyNames="LiquidacionID" DataSourceID="sdsTotalesLiq" Height="50px" 
                                Width="125px">
                                <Fields>
                                    <asp:BoundField DataField="Boletas" DataFormatString="{0:C2}" 
                                        HeaderText="Boletas" ReadOnly="True" SortExpression="Boletas">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Anticipos" DataFormatString="{0:C2}" 
                                        HeaderText="Anticipos" ReadOnly="True" SortExpression="Anticipos">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pagos_Creditos" DataFormatString="{0:C2}" 
                                        HeaderText="Pagos_Creditos" ReadOnly="True" SortExpression="Pagos_Creditos">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pagos" DataFormatString="{0:C2}" HeaderText="Pagos" 
                                        ReadOnly="True" SortExpression="Pagos">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Saldo" DataFormatString="{0:C2}" HeaderText="Saldo" 
                                        SortExpression="Saldo">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Fields>
                            </asp:DetailsView>
                            <asp:SqlDataSource ID="sdsTotalesLiq" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                SelectCommand="SELECT LiquidacionID, Boletas, Anticipos, Pagos_Creditos, Pagos, Boletas - Anticipos - Pagos_Creditos - Pagos AS Saldo FROM vTotalesLiquidacion WHERE (LiquidacionID = @LiquidacionID)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtLiquidacionID" Name="LiquidacionID" 
                                        PropertyName="Text" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Panel ID="pnlCreditoPendiente" runat="server" Font-Size="X-Large" 
                                Font-Bold="True" Font-Underline="True" ForeColor="Red">
                                ESTA PERSONA TIENE UN CREDITO QUE NO HA SIDO PAGADO</asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnDeshacer" runat="server" CausesValidation="False" 
                                onclick="btnDeshacer_Click1" Text="Deshacer Liquidación" Width="163px" />
                            <asp:Button ID="btnPrintLiquidacion" runat="server" 
                                onclick="btnPrintLiquidacion_Click" Text="Imprimir liquidación" />
                            <asp:Button ID="btnRealizaLiq" runat="server" CausesValidation="False" 
                                onclick="btnRealizaLiq_Click" Text="Realizar Liquidación" />
                            <asp:Panel ID="pnlLiquidacionResult" runat="server">
                                <asp:Image ID="imgLiquidacionBien" runat="server" 
                                    ImageUrl="~/imagenes/palomita.jpg" />
                                <asp:Image ID="imgLiquidacionMal" runat="server" 
                                    ImageUrl="~/imagenes/tache.jpg" />
                                <br />
                                <asp:Label ID="lblNewLiquidacionresult" runat="server"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                          
            </td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;</td>
            <td align="right" colspan="2">
                
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                
            </td>
        </tr>
    </table>  
    </asp:Panel>
</asp:Content>
