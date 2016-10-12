<%@ Page Title="" EnableEventValidation="true" Language="C#" Theme="skinverde" MasterPageFile="~/MasterPage.Master" AutoEventWireup="True" CodeBehind="frmLiquidacionAdd.aspx.cs" Inherits="Garibay.frmLiquidacionAdd" %>

<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>

    
<table width="100%">
    <tr> 
        <td align="right">
            <asp:HyperLink ID="lnkIrALista" runat="server" NavigateUrl="frmLiquidacionesLista.aspx">Ir a lista de liquidaciones</asp:HyperLink> 
            <br />
            <asp:HyperLink ID="lnkNewLiq" runat="server" NavigateUrl="frmLiquidacion2010.aspx" >Realizar otra nueva liquidacion</asp:HyperLink>
        </td>
    </tr>
</table>

<asp:UpdatePanel runat="server" ID="updatePanelAddLiquidacion">
<ContentTemplate>
    <asp:Panel ID="panelAddLiquidacion" runat="server">
    <table>
    	<tr>
    		<td colspan="2" class="TableHeader">DATOS DE LIQUIDACION:</td>
    	    <td>
                &nbsp;</td>
    	</tr>
    	<tr>
            <td class="TablaField">FECHA:</td>
            <td>
                <rjs:PopCalendar ID="PopCalendar1" runat="server" AutoPostBack="True" 
                    Control="txtFechaLiquidacion" 
                    onselectionchanged="PopCalendar1_SelectionChanged" Separator="/" />
                <asp:TextBox ID="txtFechaLiquidacion" runat="server" ReadOnly="True" 
                    Width="75px"></asp:TextBox>
                <asp:TextBox ID="txtFechaLuiquidacionLong" runat="server" ReadOnly="True" 
                    Width="234px"></asp:TextBox>
            </td>
            <td rowspan="10">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="TablaField">
                CICLO:</td>
            <td>
                <asp:DropDownList ID="ddlCiclo" runat="server" AutoPostBack="True" 
                    DataSourceID="sdsCiclos" DataTextField="CicloName" DataValueField="cicloID" 
                    onselectedindexchanged="ddlCiclo_SelectedIndexChanged">
                </asp:DropDownList> <<-- NO OLVIDE VERIFICAR EL CICLO AL QUE AGREGA LA LIQUIDACION.
                <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    
                    
					
                    SelectCommand="SELECT cicloID, CicloName FROM Ciclos WHERE (cerrado = @cerrado) ORDER BY fechaInicio DESC">
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
                    DataValueField="productorID">
                </asp:DropDownList>
                <cc1:ListSearchExtender ID="ddlProductor_ListSearchExtender" runat="server" 
                    Enabled="True" PromptText="Escriba para buscar" TargetControlID="ddlProductor">
                </cc1:ListSearchExtender>
                <asp:SqlDataSource ID="sdsProductores" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    
                    SelectCommand="SELECT productorID, LTRIM(apaterno + ' ' + amaterno + ' ' + nombre) AS Productor FROM Productores ORDER BY Productor">
                </asp:SqlDataSource>
                <asp:Button ID="btnAddQuickProductor" runat="server" 
                    Text="Agregar Productor Rápido" onclick="btnAddQuickProductor_Click" />
                <asp:Button ID="btnActualizaComboProductores" runat="server" 
                    onclick="btnActualizaComboProductores_Click" 
                    Text="Actualizar Lista de Productores" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
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
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="LiquidacionID" DataSourceID="sdsLiquidacionesYaEnSistema" 
                    onrowdatabound="GridView1_RowDataBound" Visible="False">
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
                                <asp:HyperLink ID="HyperLink1" runat="server">ABRIR</asp:HyperLink>
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
                <asp:UpdateProgress ID="UpdateProgress3" runat="server" 
                    AssociatedUpdatePanelID="updatePanelAddLiquidacion" DisplayAfter="0">
                    <ProgressTemplate>
                        <asp:Image ID="Image7" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                        Procesando Solicitud...
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnVerificarAntesAdd" runat="server" 
                    onclick="btnVerificarAntesAdd_Click" Text="Validar Liquidacion" />
                <asp:Button ID="btnAgregarLiquidacion" runat="server" CausesValidation="False" 
                    onclick="btnAgregarLiquidacion_Click" Text="Agregar Nueva Liquidación" />
            </td>
        </tr>
    </table>
    </asp:Panel>


<asp:UpdatePanel ID="updatePanelBoletas" runat="server" Visible="False">
<ContentTemplate>
    <table border="2">
	<tr>
		<td align="center" class="TableHeader">FORMATO DE LIQUIDACIÓN</td>
	</tr>
	<tr>
	    <td>
	        <table>
	        	<tr>
		            <td colspan="3">
		             
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                            AssociatedUpdatePanelID="updatePanelBoletas" DisplayAfter="0">
                            <ProgressTemplate>
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                                Cargando información...
                            </ProgressTemplate>
                        </asp:UpdateProgress>
		             
                        </td>
	            </tr>
	            <tr>
                    <td colspan="3">
                        Liquidación Número:
                        <asp:Label ID="lblNumLiquidacion" runat="server" Font-Bold="True" 
                            Font-Size="X-Large" Text="Label"></asp:Label>
                        <asp:TextBox ID="txtLiquidacionID" runat="server" Visible="False" Width="10px">-1</asp:TextBox>
                    </td>
                </tr>
	            <tr>
		            <td >NOMBRE:<asp:TextBox ID="txtProductorID" runat="server" Visible="False" 
                            Width="19px">-1</asp:TextBox>
                    </td><td>
                    <asp:TextBox ID="txtNombre" runat="server" Width="360px"></asp:TextBox>
                    </td><td rowspan="4" align="right" valign="top">
                        <asp:Image ID="Image2" runat="server" 
                            ImageUrl="~/imagenes/IPROJALsmall.jpg" />
                    </td>
	            </tr>
	            <tr>
		            <td>DOMICILIO:</td><td>
                    <asp:TextBox ID="txtDomicilio" runat="server" Width="360px"></asp:TextBox>
                    </td>
	            </tr>
	            <tr>
		            <td>POBLACIÓN:</td><td>
                    <asp:TextBox ID="txtPoblacion" runat="server" Width="360px"></asp:TextBox>
                    </td>
	            </tr>
	            <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="chkUpdateProductorData" runat="server" Checked="True" 
                            Text="Actualizar datos del productor con los datos de la liquidacion" />
                    </td>
                </tr>
	        </table>
	    </td>
	</tr>
	<tr>
	    <td>
	        <table>
	        	<tr>
                    <td class="TableHeader">
                        <asp:CheckBox ID="chkAgregarBoletas" runat="server" 
                            Text="Mostrar Agregar Boletas" />
                    </td>
                </tr>
	        	<tr>
	        	    <td>
	        	        <div id="divAddBoletas" runat="Server">
	        	        <table>
	        	        	<tr>
                            <td align="left" valign="top">
                                    AGREGAR BOLETA EXISTENTE</td>
                                <td align="left" valign="top">
                                    AGREGAR UNA NUEVA BOLETA</td>
                        </tr>
	        	            <tr>
                                <td align="left" valign="top">
                                    <table class="TablaField">
                                        <tr>
                                            <td class="TablaField">
                                                Productor:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlProdBoletas" runat="server" AutoPostBack="True" 
                                                    DataSourceID="sdsProdBoletas" DataTextField="Productor" 
                                                    DataValueField="productorID" 
                                                    onselectedindexchanged="ddlProdBoletas_SelectedIndexChanged" Height="23px" 
                                                    Width="296px">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsProdBoletas" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                    
                                                    SelectCommand="SELECT DISTINCT Productores.productorID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS Productor FROM Productores INNER JOIN Boletas ON Productores.productorID = Boletas.productorID LEFT OUTER JOIN Liquidaciones_Boletas ON Boletas.boletaID = Liquidaciones_Boletas.BoletaID WHERE (Boletas.cicloID = @cicloID) AND (Liquidaciones_Boletas.LiquidacionID IS NULL) ORDER BY Productor">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="ddlCiclo" DefaultValue="-1" Name="cicloID" 
                                                            PropertyName="SelectedValue" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField">
                                                Producto:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlProductoExistBoleta" runat="server" 
                                                    AutoPostBack="True" DataSourceID="sdsProductosExistBoleta" 
                                                    DataTextField="Nombre" DataValueField="productoID" 
                                                    onselectedindexchanged="ddlProductoExistBoleta_SelectedIndexChanged" 
                                                    Height="23px" Width="306px">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsProductosExistBoleta" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                    
													SelectCommand="SELECT DISTINCT Productos.productoID, Productos.productoGrupoID, Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Nombre FROM Boletas INNER JOIN Productos ON Boletas.productoID = Productos.productoID INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID WHERE (Boletas.cicloID = @cicloID) AND (Boletas.productorID = @productorID) ORDER BY Productos.productoGrupoID ,Nombre">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="ddlCiclo" DefaultValue="-1" Name="cicloID" 
                                                            PropertyName="SelectedValue" Type="Int32" />
                                                        <asp:ControlParameter ControlID="ddlProdBoletas" DefaultValue="-1" 
                                                            Name="productorID" PropertyName="SelectedValue" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="gvExistBoletas" runat="server" AllowPaging="True" 
                                                    AutoGenerateColumns="False" DataKeyNames="boletaID" ShowFooter="True">
                                                    <Columns>
                                                        <asp:TemplateField ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkRowSelected" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="NumeroBoleta" HeaderText="# Boleta" 
                                                            SortExpression="NumeroBoleta" />
                                                        <asp:BoundField DataField="Ticket" HeaderText="Ticket" 
                                                            SortExpression="Ticket" />
                                                        <asp:BoundField DataField="Producto" HeaderText="Descripción" 
                                                            SortExpression="Producto" />
                                                        <asp:BoundField DataField="pesonetoentrada" DataFormatString="{0:n}" 
                                                            HeaderText="KG." ItemStyle-HorizontalAlign="Right" 
                                                            SortExpression="pesonetoentrada">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="boletaID" HeaderText="boletaID" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="margin-left: 40px">
                                                <asp:Button ID="btnAgregarBoletaaLista" runat="server" onclick="Button2_Click" 
                                                    Text="Agregar boleta(s) a Lista" CausesValidation="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="left" valign="top">
                                    <table>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                PRODUCTOR:</td>
                                            <td>
                                            <br />
                                                <asp:DropDownList ID="ddlNewBoletaProductor" runat="server" 
                                                    DataSourceID="sdsNewBoletaProductor" DataTextField="Productor" 
                                                    DataValueField="productorID" Height="23px" Width="211px">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender ID="ddlNewBoletaProductor_ListSearchExtender" 
                                                    runat="server" Enabled="True" PromptText="Escriba para buscar" 
                                                    TargetControlID="ddlNewBoletaProductor">
                                                </cc1:ListSearchExtender>
                                                <asp:SqlDataSource ID="sdsNewBoletaProductor" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                    SelectCommand="SELECT productorID, apaterno + ' ' + amaterno + ' ' + nombre AS Productor FROM Productores ORDER BY Productor">
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                # BOLETO
                                                <br />
                                                DE BASCULA:</td>
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
                                                PRODUCTO:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlNewBoletaProducto" runat="server" 
                                                    DataSourceID="sdsNewBoletaProductos" DataTextField="Expr1" 
                                                    DataValueField="productoID">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsNewBoletaProductos" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                    
													
                                                    SelectCommand="SELECT Productos.productoID, Productos.productoGrupoID,Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Expr1 FROM Productos INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID WHERE (Productos.productoGrupoID &lt;&gt; 1) AND (Productos.productoGrupoID &lt;&gt; 4) AND (Productos.productoGrupoID &lt;&gt; 2) ORDER BY Productos.productoGrupoID, Productos.Nombre">
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
                                            <td class="TablaField" align="right">
                                                FECHA ENTRADA:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewFechaEntrada" runat="server"></asp:TextBox>
                                                <rjs:PopCalendar ID="PopCalendar5" runat="server"  Control="txtNewFechaEntrada" 
                                                    Separator="/"/>
                                                <br />
                                                <asp:CheckBox ID="chkChangeFechaSalidaNewBoleta" runat="server" 
                                                    Text="Fecha Salida es Diferente a la de Entrada" />
                                                <div ID="divFechaSalidaNewBoleta" runat="Server">
                                                    <br />
                                                    FECHA SALIDA:
                                                    <asp:TextBox ID="txtNewFechaSalida" runat="server" ReadOnly="True"></asp:TextBox>
                                                    <rjs:PopCalendar ID="PopCalendar4" runat="server" Control="txtNewFechaSalida" 
                                                        Separator="/" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                PESO BRUTO:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewPesoEntrada" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                PESO TARA:</td>
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
                                            <td class="TablaField" colspan="2">
                                                <asp:Panel ID="pnlNewBoleta" runat="server" Visible="False">
                                                    <asp:Image ID="imgBien" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                                                    <asp:Image ID="imgMal" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                                                    <asp:Label ID="lblNewBoletaResult" runat="server"></asp:Label>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" colspan="2">
                                                <asp:Button ID="btnAddNewBoleta" runat="server" onclick="btnAddNewBoleta_Click" 
                                                    Text="Guardar y Agregar a Lista" CausesValidation="False" 
                                                    style="height: 26px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
	        	        </table>
	        	    </div>
	        	    </td>
	        	</tr>
	        	<tr>
	        		<td>
	        		    <table>
	        		        <tr><td>
                                &nbsp;</td><td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
	        		    </table>
	        		</td>
	        	</tr>
	            <tr>
                    <td>
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" 
                            AssociatedUpdatePanelID="updatePanelBoletas" DisplayAfter="0">
                            <ProgressTemplate>
                                <asp:Image ID="Image6" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                                Procesando informacion...
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
	            <tr>
                    <td>
                        <asp:GridView ID="gvBoletas" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="PesoDeEntrada,PesoDeSalida,productoID,Producto,bodegaID" 
                            onrowcancelingedit="gvBoletas_RowCancelingEdit" 
                            onrowdeleting="gvBoletas_RowDeleting" onrowediting="gvBoletas_RowEditing" 
                            onrowupdating="gvBoletas_RowUpdating" 
                            onselectedindexchanged="gvBoletas_SelectedIndexChanged" ShowFooter="True">
                            <Columns>
                                <asp:CommandField ButtonType="Button" CausesValidation="False" 
                                    DeleteText="Quitar de lista" EditText="Editar" ShowDeleteButton="True" 
                                    ShowEditButton="True" />
                                <asp:BoundField DataField="boletaID" HeaderText="BoletaID" ReadOnly="True" 
                                    Visible="False" />
                                <asp:BoundField DataField="NumeroBoleta" HeaderText="BOLETA No." 
                                    NullDisplayText=" " ReadOnly="True">
                                    <ItemStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Ticket" HeaderText="TICKET" />
                                <asp:TemplateField HeaderText="KG.">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtKilos" runat="server" 
                                            Text='<%# Bind("pesonetoentrada", "{0:n}") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" 
                                            Text='<%# Bind("pesonetoentrada", "{0:n}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DESCRIPCIÓN">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Producto") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="drpdlProductoupBoletas" runat="server" 
                                            DataSourceID="sdsProductoListBoleta" DataTextField="Nombre" 
                                            DataValueField="productoID" Height="22px" Width="213px">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsProductoListBoleta" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            SelectCommand="SELECT [productoID], [Nombre] FROM [Productos]">
                                        </asp:SqlDataSource>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="humedad" DataFormatString="{0:n}" HeaderText="HUM." 
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="dctoHumedad" DataFormatString="{0:n}" 
                                    HeaderText="DSCTO HUM." ItemStyle-HorizontalAlign="Right" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="impurezas" DataFormatString="{0:n}" 
                                    HeaderText="IMPUREZAS">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="dctoImpurezas" DataFormatString="{0:n}" 
                                    HeaderText="DSCTO IMPUREZAS" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="pesonetoapagar" DataFormatString="{0:n}" 
                                    HeaderText="KG NETOS" ItemStyle-HorizontalAlign="Right" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="precioapagar" DataFormatString="{0:C5}" 
                                    HeaderText="PRECIO (por KG)" ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="importe" DataFormatString="{0:c}" 
                                    HeaderText="IMPORTE" ItemStyle-HorizontalAlign="Right" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="dctoSecado" DataFormatString="{0:c}" 
                                    HeaderText="DSCTO SECADO" ItemStyle-HorizontalAlign="Right" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="totalapagar" DataFormatString="{0:c}" 
                                    HeaderText="TOTAL A PAGAR" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PesoDeEntrada" HeaderText="pesodeentrada" 
                                    ReadOnly="True" Visible="False" />
                                <asp:BoundField DataField="PesoDeSalida" HeaderText="pesodesalida" 
                                    ReadOnly="True" Visible="False" />
                                <asp:BoundField DataField="productoID" HeaderText="productoID" 
                                    Visible="False" />
                                <asp:TemplateField HeaderText="Aplicar Desc Humedad">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkApplyHumABol" runat="server" Checked = '<%# Bind("applyHumedad") %>' />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Enabled="False" Checked = '<%# Bind("applyHumedad") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Aplicar Desc Impurezas">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkApplyImpABol" runat="server" Checked = '<%# Bind("applyImpurezas") %>' />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server" Enabled="False" Checked = '<%# Bind("applyImpurezas") %>'/>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Aplicar Secado">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkApplySecadoABoleta" runat="server" Checked = '<%# Bind("applySecado") %>'/>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox3" runat="server" Enabled="False" Checked = '<%# Bind("applySecado") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bodega">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="drpdlBodega" runat="server" DataSourceID="sdsBodegaGrid" 
                                            DataTextField="bodega" DataValueField="bodegaID" Height="23px" Width="192px">
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
                                <asp:BoundField DataField="bodegaID" HeaderText="bodegaID" Visible = "false"/>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
	            <tr>
                    <td align="right">
                        <table>
                        	<tr>
                        		<td>
                        		    <table>
                        		        <tr>
                        		            <td align="left" valign="top">
                        		                &nbsp;</td>
                        		            <td align="left" valign="top">
                                                <table>
                                                    <tr>
                                                        <td align="left" class="TableHeader">
                                                            &nbsp;</td>
                                                        <td align="left">
                                                            <asp:CheckBox ID="CheckBoxMostrarAnticipos" runat="server" />
                                                            <asp:Panel ID="PanelMostrarAnticipos" runat="server">
                                                            <table>
        <tr>
            <td class="TablaField">Ciclo:</td><td>
                <asp:DropDownList ID="ddlCiclos" runat="server" DataSourceID="SqlDataSourceCiclosAnticiposDisponibles" 
                    DataTextField="CicloName" DataValueField="cicloID">
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
                <asp:Button ID="btnActualizar" runat="server" onclick="btnActualizar_Click" 
                    Text="Actualizar" />
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
                                    
                                    
                                    
        
    
        
                                                                    SelectCommand="SELECT Anticipos.anticipoID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS Productor, Bancos.nombre + ' ' + CuentasDeBanco.NumeroDeCuenta AS Cuenta, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.cargo + MovimientosCuentasBanco.abono AS Monto, MovimientosCuentasBanco.numCheque, MovimientosCaja.cargo + MovimientosCaja.abono AS Efectivo, TiposAnticipos.tipoAnticipo, Productores.apaterno, Productores.amaterno, Productores.nombre, Liquidaciones.LiquidacionID, Liquidaciones.cobrada FROM Liquidaciones INNER JOIN Liquidaciones_Anticipos ON Liquidaciones.LiquidacionID = Liquidaciones_Anticipos.LiquidacionID RIGHT OUTER JOIN TiposAnticipos INNER JOIN Anticipos INNER JOIN Productores ON Anticipos.productorID = Productores.productorID ON TiposAnticipos.tipoAnticipoID = Anticipos.tipoAnticipoID ON Liquidaciones_Anticipos.Anticipos = Anticipos.anticipoID LEFT OUTER JOIN CuentasDeBanco INNER JOIN MovimientosCuentasBanco ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco.cuentaID INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID ON Anticipos.movbanID = MovimientosCuentasBanco.movbanID LEFT OUTER JOIN MovimientosCaja ON Anticipos.movimientoID = MovimientosCaja.movimientoID WHERE (Anticipos.cicloID = @cicloID) AND (Anticipos.tipoAnticipoID = 1) AND (Liquidaciones_Anticipos.LiquidacionID IS NULL) ORDER BY Productor">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCiclos" DefaultValue="-1" 
                                            Name="cicloID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
                                                            </asp:Panel>
                                                            <cc1:CollapsiblePanelExtender ID="PanelMostrarAnticipos_CollapsiblePanelExtender" 
                                                                runat="server" CollapseControlID="CheckBoxMostrarAnticipos" Enabled="True" 
                                                                ExpandControlID="CheckBoxMostrarAnticipos" 
                                                                TargetControlID="PanelMostrarAnticipos">
                                                            </cc1:CollapsiblePanelExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="TableHeader">
                                                            &nbsp;</td>
                                                        <td align="left" class="TableHeader">
                                                            ANTICIPOS A LA LIQUIDACIÓN:
                                                            <asp:Button ID="btnAsignarAnticipoaLiq" runat="server" CausesValidation="False" 
                                                                onclick="btnAsignarAnticipoaLiq_Click" Text="Asignar Anticipo" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            <asp:SqlDataSource ID="sdsAnticipos" runat="server" 
                                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                SelectCommand="SELECT Anticipos.anticipoID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS Productor, Bancos.nombre + ' ' + CuentasDeBanco.NumeroDeCuenta AS Cuenta, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.cargo + MovimientosCuentasBanco.abono AS Monto, MovimientosCuentasBanco.numCheque, MovimientosCaja.cargo + MovimientosCaja.abono AS Efectivo, Liquidaciones_Anticipos.LiquidacionID, Anticipos.fecha FROM Liquidaciones_Anticipos INNER JOIN Anticipos INNER JOIN Productores ON Anticipos.productorID = Productores.productorID ON Liquidaciones_Anticipos.Anticipos = Anticipos.anticipoID LEFT OUTER JOIN CuentasDeBanco INNER JOIN MovimientosCuentasBanco ON CuentasDeBanco.cuentaID = MovimientosCuentasBanco.cuentaID INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID ON Anticipos.movbanID = MovimientosCuentasBanco.movbanID LEFT OUTER JOIN MovimientosCaja ON Anticipos.movimientoID = MovimientosCaja.movimientoID WHERE (Liquidaciones_Anticipos.LiquidacionID = @liquidacionID)">
                                                                <SelectParameters>
                                                                    <asp:ControlParameter ControlID="txtLiquidacionID" DefaultValue="-1" 
                                                                        Name="liquidacionID" PropertyName="Text" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                            <asp:GridView ID="gvAnticipos" runat="server" AutoGenerateColumns="False" 
                                                                DataKeyNames="anticipoID" DataSourceID="sdsAnticipos">
                                                                <Columns>
                                                                    <asp:BoundField DataField="anticipoID" HeaderText="#" InsertVisible="False" 
                                                                        ReadOnly="True" SortExpression="anticipoID" />
                                                                    <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                                                        HeaderText="Fecha" SortExpression="fecha" />
                                                                    <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                                                                        SortExpression="Productor" />
                                                                    <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" ReadOnly="True" 
                                                                        SortExpression="Cuenta" />
                                                                    <asp:BoundField DataField="Concepto" HeaderText="Concepto" 
                                                                        SortExpression="Concepto" />
                                                                    <asp:BoundField DataField="numCheque" HeaderText="Cheque" 
                                                                        ItemStyle-HorizontalAlign="Right" SortExpression="numCheque">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Monto" DataFormatString="{0:c}" HeaderText="Monto" 
                                                                        ItemStyle-HorizontalAlign="Right" ReadOnly="True" SortExpression="Monto">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Efectivo" DataFormatString="{0:c}" 
                                                                        HeaderText="Efectivo" ItemStyle-HorizontalAlign="Right" ReadOnly="True" 
                                                                        SortExpression="Efectivo">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TableHeader">
                                                            &nbsp;</td>
                                                        <td class="TableHeader">
                                                            PAGOS DE LA LIQUIDACION</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            <asp:GridView ID="gvPagosLiquidacion" runat="server" 
                                                                AutoGenerateColumns="False" onrowdatabound="gvPagosLiquidacion_RowDataBound" 
                                                                onrowdeleting="gvPagosLiquidacion_RowDeleting">
                                                                <Columns>
                                                                    <asp:CommandField ButtonType="Button" CancelText="Cancelar" 
                                                                        CausesValidation="False" DeleteText="Eliminar" ShowDeleteButton="True" />
                                                                    <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                                                        HeaderText="Fecha" />
                                                                    <asp:BoundField DataField="productorNombre" HeaderText="Productor" 
                                                                        ReadOnly="True" SortExpression="Productor" />
                                                                    <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" ReadOnly="True" 
                                                                        SortExpression="Cuenta" />
                                                                    <asp:BoundField DataField="Concepto" HeaderText="Concepto" 
                                                                        SortExpression="Concepto" />
                                                                    <asp:BoundField DataField="numCheque" HeaderText="Cheque" 
                                                                        ItemStyle-HorizontalAlign="Right" SortExpression="numCheque">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Monto" DataFormatString="{0:c}" HeaderText="Monto" 
                                                                        ItemStyle-HorizontalAlign="Right" ReadOnly="True" SortExpression="Monto">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Efectivo" DataFormatString="{0:c}" 
                                                                        HeaderText="Efectivo" ItemStyle-HorizontalAlign="Right" ReadOnly="True" 
                                                                        SortExpression="Efectivo">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="btnPrintCheque" runat="server">IMPRIMIR</asp:HyperLink>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <asp:CheckBox ID="chkMostrarAgregarPago" runat="server" 
                                                                            Text="Mostrar Panel Para Agregar Nuevo Pago" />
                                                                        <asp:UpdateProgress ID="UpdateProgressAddNewPago" runat="server" 
                                                                            AssociatedUpdatePanelID="UpdateAddNewPago" DisplayAfter="0">
                                                                            <ProgressTemplate>
                                                                                <asp:Image ID="Image4" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                                                                                Cargando información...
                                                                            </ProgressTemplate>
                                                                        </asp:UpdateProgress>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <asp:UpdatePanel ID="UpdateAddNewPago" runat="Server">
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
                                                                                                    DataValueField="productorID" Width="250px">
                                                                                                </asp:DropDownList>
                                                                                                <cc1:ListSearchExtender ID="cmbProductoresPago_ListSearchExtender" 
                                                                                                    runat="server" Enabled="True" PromptText="Escriba para Buscar" 
                                                                                                    TargetControlID="cmbProductoresPago">
                                                                                                </cc1:ListSearchExtender>
                                                                                                <asp:SqlDataSource ID="sdsProductoresPago" runat="server" 
                                                                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                    SelectCommand="Select LTRIM(productores.apaterno  + ' ' + productores.amaterno + ' ' + productores.nombre) as name, productores.productorID from Productores  order by name">
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
                                                                                                <asp:RequiredFieldValidator ID="valFecha0" runat="server" 
                                                                                                    ControlToValidate="txtFechaPago" ErrorMessage="El campo fecha es necesario"></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="TablaField">
                                                                                                Tipo de pago:</td>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="cmbTipodeMovPago" runat="server" Height="22px" 
                                                                                                    onselectedindexchanged="cmbTipodeMovPago_SelectedIndexChanged" Width="249px">
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
                                                                                                <asp:RequiredFieldValidator ID="valMontorequired1" runat="server" 
                                                                                                    ControlToValidate="txtMonto" Display="Dynamic" 
                                                                                                    ErrorMessage="El campo monto es necesario"></asp:RequiredFieldValidator>
                                                                                                <br />
                                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                                                                                    ControlToValidate="txtMonto" Display="Dynamic" 
                                                                                                    ErrorMessage="Escriba una cantida válida" ValidationExpression="\d+(.\d*)?"></asp:RegularExpressionValidator>
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
                                                                                                                    SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
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
                                                                                                                    DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID" Height="23px" 
                                                                                                                    Width="257px">
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
                                                                                                                    Height="23px" Width="256px">
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
                                                                                                                    DataValueField="subCatalogoMovBancoID" Height="23px" Width="258px">
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
                                                                                                                    DataValueField="ConceptoMovCuentaID" Height="22px" Width="249px">
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
                                                                                                                    DataSourceID="sdsCuentaPago" DataTextField="cuenta" DataValueField="cuentaID" 
                                                                                                                    Height="22px" Width="427px">
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
                                                                                                                    DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID" Height="23px" 
                                                                                                                    Width="257px">
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
                                                                                                                    Height="23px" Width="256px">
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
                                                                                                                    DataValueField="subCatalogoMovBancoID" Height="23px" Width="258px">
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
                                                                                                                                    DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID" Height="23px" 
                                                                                                                                    Width="235px">
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
                                                                                                                                    Height="23px" Width="236px">
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
                                                                                                                                    DataValueField="subCatalogoMovBancoID" Height="23px" Width="234px">
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
                                                                                                <asp:Panel ID="pnlNewPago" runat="server">
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
                                                                                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/imagenes/cargando.gif" />
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
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                        		        </tr>
                        		        <tr>
                        		            <td>&nbsp;</td>
                        		            <td>
                                            </td>
                        		        </tr>
                        		    </table>
                        		</td>
                        		<td valign="top">
                        		    <table >
                        	            <tr>
                                            <td class="TablaField">
                                                SUBTOTAL:</td>
                                            <td>
                                                <asp:Label ID="lblTotalAPagar" runat="server" Text="0" Font-Bold="True" 
                                                    Font-Size="X-Large"></asp:Label></td>
                        	            </tr>
                                        <tr>
                                            <td class="TablaField">
                                                ANTICIPOS:</td>
                                            <td>
                                                <asp:Label ID="lblAnticipos" runat="server" Font-Bold="True" 
                                                    Font-Size="X-Large" Text="0"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField">
                                                NOTAS:</td>
                                            <td>
                                                <asp:TextBox ID="txtTotalNotas" runat="server" Width="111px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField">
                                                INTERESES:</td>
                                            <td>
                                                <asp:TextBox ID="txtIntereses" runat="server" Width="111px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField">
                                                SEGURO:</td>
                                            <td>
                                                <asp:TextBox ID="txtSeguro" runat="server" Width="111px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField">
                                                TOTAL FINAL POR PAGAR:</td>
                                            <td>
                                                <asp:Label ID="lblTotalFinal" runat="server" Font-Bold="True" 
                                                    Font-Size="X-Large" Text="0"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField">
                                                PAGOS:</td>
                                            <td>
                                                <asp:Label ID="lblPagos" runat="server" Font-Bold="True" Font-Size="X-Large" 
                                                    Text="0"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2">
                                                <asp:Button ID="btnSaveLiq" runat="server" Text="Guardar Cambios" 
                                                    onclick="btnSaveLiq_Click" CausesValidation="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2">
                                                
                                                <asp:Button ID="btnRealizaLiq" runat="server" Text="Realizar Liquidación" 
                                                    CausesValidation="False" onclick="btnRealizaLiq_Click" />
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2">
                                                <asp:Button ID="btnPrintLiquidacion" runat="server" 
                                                    Text="Imprimir liquidación" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2">
                                                <asp:Button ID="btnDeshacer" runat="server" Height="23px" 
                                                    onclick="btnDeshacer_Click1" Text="Deshacer Liquidación" Width="163px" 
                                                    CausesValidation="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
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
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblLastSaved" runat="server" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
	        </table>
        </td>
	</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>

</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
