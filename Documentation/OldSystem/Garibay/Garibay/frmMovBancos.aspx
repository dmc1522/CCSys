<%@ Page EnableEventValidation="false" Title="CUENTA DE BANCO" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="True" CodeBehind="frmMovBancos.aspx.cs" Inherits="Garibay.frmMovBancos" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>
    <style type="text/css">
        .Button
        {
            height: 26px;
        }
        .style3
        {
            width: 100%;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
    <ProgressTemplate>
        <asp:Image ID="imgLoading" runat="server" ImageUrl="~/imagenes/cargando.gif" />
        CARGANDO INFORMACION...
    </ProgressTemplate>
    </asp:UpdateProgress>
    <table>
	<tr>
		<td>
		    <table>
		    <tr>
		    		<td class="TablaField">BANCO : </td> <td colspan="5">
                    <asp:DropDownList ID="drpdlBancos" runat="server" Height="23px" 
						AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="nombre" 
						DataValueField="bancoID" onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
						SelectCommand="SELECT DISTINCT Bancos.nombre, Bancos.bancoID FROM Bancos INNER JOIN CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID">
                    </asp:SqlDataSource>
                    </td>
		    	    <td>
                        &nbsp;</td>
		    	</tr>
		    <tr>
		    		<td class="TablaField">EMPRESA TITULAR : </td> <td colspan="5">
                    <asp:DropDownList ID="drpdltitular" runat="server" DataSourceID="SqlDataSource1" 
                        DataTextField="Titular" DataValueField="Titular" Height="23px" AutoPostBack="True" 
						onselectedindexchanged="DropDownList1_SelectedIndexChanged" >
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
						SelectCommand="SELECT DISTINCT Titular FROM CuentasDeBanco WHERE (bancoID = @bancoID)">
                    	<SelectParameters>
							<asp:ControlParameter ControlID="drpdlBancos" DefaultValue="-1" Name="bancoID" 
								PropertyName="SelectedValue" />
						</SelectParameters>
                    </asp:SqlDataSource>
                    </td>
		    	    <td>
                        &nbsp;</td>
		    	</tr>
		    	<tr>
		    		<td class="TablaField">CUENTA:</td> <td colspan="5">
                    <asp:DropDownList ID="drpdlCuenta" runat="server" DataSourceID="sdsCuentas" 
                        DataTextField="cuenta" DataValueField="cuentaID" Height="23px" AutoPostBack="True" 
                        onselectedindexchanged="drpdlCuenta_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsCuentas" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
						
						
                        
                        SelectCommand="SELECT  '(' + TiposDeMoneda.Moneda + ')   -'  + Bancos.nombre + ' - ' + CuentasDeBanco.NumeroDeCuenta + ' - ' + CuentasDeBanco.Titular AS cuenta, CuentasDeBanco.cuentaID FROM CuentasDeBanco INNER JOIN Bancos ON CuentasDeBanco.bancoID = Bancos.bancoID INNER JOIN TiposDeMoneda ON CuentasDeBanco.tipomonedaID = TiposDeMoneda.tipomonedaID WHERE (CuentasDeBanco.bancoID = @bancoID) AND (CuentasDeBanco.Titular = @Titular) ORDER BY cuenta">
                    	<SelectParameters>
							<asp:ControlParameter ControlID="drpdlBancos" DefaultValue="-1" Name="bancoID" 
								PropertyName="SelectedValue" />
							<asp:ControlParameter ControlID="drpdltitular" DefaultValue="&quot;&quot;" 
								Name="Titular" PropertyName="SelectedValue" />
						</SelectParameters>
                    </asp:SqlDataSource>
                    </td>
		    	    <td>
                        &nbsp;</td>
		    	</tr>
		    	<tr>
                    <td class="TablaField">
                        <asp:CheckBox ID="chkPorMes" runat="server" AutoPostBack="True" 
                            Text="Por Mes - Año" oncheckedchanged="chkPorMes_CheckedChanged" />
                        <cc1:MutuallyExclusiveCheckBoxExtender ID="chkPorMes_MutuallyExclusiveCheckBoxExtender" 
                            runat="server" Enabled="True" Key="0" TargetControlID="chkPorMes">
                        </cc1:MutuallyExclusiveCheckBoxExtender>
                    </td>
                    <td class="TablaField">
                        MES:</td>
                    <td class="TablaField">
                        &nbsp;</td>
                    <td>
                        <asp:DropDownList ID="drpdlMes" runat="server" AutoPostBack="True" 
                            Height="23px" onselectedindexchanged="drpdlMes_SelectedIndexChanged" 
                            Width="182px">
                            <asp:ListItem Value="1">ENERO</asp:ListItem>
                            <asp:ListItem Value="2">FEBRERO</asp:ListItem>
                            <asp:ListItem Value="3">MARZO</asp:ListItem>
                            <asp:ListItem Value="4">ABRIL</asp:ListItem>
                            <asp:ListItem Value="5">MAYO</asp:ListItem>
                            <asp:ListItem Value="6">JUNIO</asp:ListItem>
                            <asp:ListItem Value="7">JULIO</asp:ListItem>
                            <asp:ListItem Value="8">AGOSTO</asp:ListItem>
                            <asp:ListItem Value="9">SEPTIEMBRE</asp:ListItem>
                            <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
                            <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
                            <asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="TablaField">
                        AÑO:</td>
                    <td>
                        <asp:DropDownList ID="drddlAnio" runat="server" AutoPostBack="True" 
                            Height="26px" onselectedindexchanged="drddlAnio_SelectedIndexChanged" 
                            Width="128px">
                            <asp:ListItem Value="2009">2009</asp:ListItem>
                            <asp:ListItem>2010</asp:ListItem>
                            <asp:ListItem>2011</asp:ListItem>
                            <asp:ListItem>2012</asp:ListItem>
                            <asp:ListItem>2013</asp:ListItem>
                            <asp:ListItem>2014</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td rowspan="2">
                        <asp:Label ID="lblPeriodoBloqueado" runat="server" 
                            Text="Este periodo ha sido bloqueado, no se pueden agregar, modificar ni eliminar movimientos "></asp:Label>
                        <asp:Button ID="btnBloquearPeriodo" runat="server" Text="Bloquear Periodo" 
                            onclick="btnBloquearPeriodo_Click" />
                        <asp:Button ID="btnDesbloquearPeriodo" runat="server" 
                            Text="DesBloquear Periodo" onclick="btnDesbloquearPeriodo_Click" />
                    </td>
                </tr>
		    	<tr>
		    	    <td class="TablaField">
                        <asp:CheckBox ID="chkPorFechas" runat="server" AutoPostBack="True" 
                            Text="Por Rango de Fechas" 
                            oncheckedchanged="chkPorFechas_CheckedChanged" />
                        <cc1:MutuallyExclusiveCheckBoxExtender ID="chkPorFechas_MutuallyExclusiveCheckBoxExtender" 
                            runat="server" Enabled="True" Key="0" TargetControlID="chkPorFechas">
                        </cc1:MutuallyExclusiveCheckBoxExtender>
                    </td><td class="TablaField">
                        FECHA INICIO:</td>
		    	    <td class="TablaField">
                        &nbsp;</td>
		    	    <td>
                        <asp:TextBox ID="txtFechaIncio" runat="server" ReadOnly="True"></asp:TextBox>
                        <rjs:PopCalendar ID="PopCalendar5" runat="server" AutoPostBack="True" 
                            Separator="/" Control = "txtFechaIncio" 
                            onselectionchanged="PopCalendar5_SelectionChanged"/>
                    </td>
                    <td class="TablaField">
                        FECHA FIN:
                    </td>
                    <td>
                        <asp:TextBox ID="txtFechaFin" runat="server" ReadOnly="True"></asp:TextBox>
                        <rjs:PopCalendar ID="PopCalendar6" runat="server" AutoPostBack="True" 
                            Separator="/" Control = "txtFechaFin" 
                            onselectionchanged="PopCalendar6_SelectionChanged"/>
                    </td>
		    	</tr>
		    	<tr>
                    <td class="TablaField">
                        CABEZAS DE GANADO:&nbsp;
                    </td>
                    <td colspan="5">
                        <asp:Label ID="lblContador" runat="server" Font-Size="XX-Large" Text="Label"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
		    	<tr>
                    <td colspan="6">
                        <asp:Button ID="btnIrAconciliación" runat="server" 
                            onclick="btnIrAconciliación_Click" Text="Ir a conciliación" />
                            <br />
                            <asp:Button ID="btnGeneraReporte" runat="server" Text="Generar Reporte" 
                                onclick="btnGeneraReporte_Click" CausesValidation="False" />
                            <asp:Button ID="btnImprimeReporte" runat="server" Text="Imprimir Reporte" 
                            CausesValidation="False" />
                    </td>
                    <td>
                        <asp:Panel ID="pnlResPeriodoBloq" runat="server">
                            <asp:Label ID="lblResPeriodoBloq" runat="server" 
                                Text="Resultado periodo bloqueado"></asp:Label>
                        </asp:Panel>
                    </td>
                </tr>
		    	<tr>
                    <td colspan="6">
                    <asp:Panel runat="Server" ID="pnlChequesAnteriores">
                        <table border="2">
                            <tr>
                                <td class="TablaField">Cheques no conciliados de meses anteriores</td>
                            </tr>
                            <tr>
                                <td>
                                    
                                    <asp:GridView ID="gvChequesNoConciliados" runat="server" 
                                        AutoGenerateColumns="False" DataSourceID="sdsChequesNoConciliados">
                                        <Columns>
                                            <asp:BoundField DataField="ANIO" HeaderText="AÑO" ReadOnly="True" 
                                                SortExpression="ANIO" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="month" HeaderText="MES" SortExpression="month" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Cantidad" HeaderText="CANTIDAD" ReadOnly="True" 
                                                SortExpression="Cantidad" ItemStyle-HorizontalAlign="Right" />
                                        </Columns>
                                    </asp:GridView>
                                    
                                    <asp:TextBox ID="txtFechaCheqNoConc" runat="server"></asp:TextBox>
                                    <asp:SqlDataSource ID="sdsChequesNoConciliados" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        SelectCommand="SELECT COUNT(MovimientosCuentasBanco.movbanID) AS Cantidad, Meses.month, YEAR(MovimientosCuentasBanco.fecha) AS ANIO FROM MovimientosCuentasBanco INNER JOIN Meses ON MONTH(MovimientosCuentasBanco.fecha) = Meses.monthID WHERE (MovimientosCuentasBanco.numCheque &gt; 0) AND (MovimientosCuentasBanco.fecha &lt; @fecha) AND (MovimientosCuentasBanco.chequecobrado = 0) AND (MovimientosCuentasBanco.cuentaID = @cuentaID) GROUP BY YEAR(MovimientosCuentasBanco.fecha), Meses.month, MovimientosCuentasBanco.cuentaID">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="txtFechaCheqNoConc" Name="fecha" 
                                                PropertyName="Text" />
                                            <asp:ControlParameter ControlID="drpdlCuenta" DefaultValue="-1" Name="cuentaID" 
                                                PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTraerChequesNoConciliados" runat="server" Font-Size="Large"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnTraerChequesAlMes" runat="server" 
                                        onclick="btnTraerChequesAlMes_Click" Text="Traer cheques a este mes" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    </td>
                    <td valign="top">
                        <asp:CheckBox ID="chkPeriodosBloqueados" runat="server" 
                            Text="Ver Periodos Bloqueados" />
                        <asp:Panel runat="Server" id="pnlPeriodosBloqueados">
                            <asp:GridView ID="gvPeriodosBloqueados" runat="server" 
                                AutoGenerateColumns="False" DataKeyNames="perbloqID" 
                                DataSourceID="sdsPeriodosBloqueados" 
                                onrowdeleted="gvPeriodosBloqueados_RowDeleted" 
                                onrowdeleting="gvPeriodosBloqueados_RowDeleting">
                                <Columns>
                                    <asp:CommandField ButtonType="Button" DeleteText="Quitar Bloqueo" 
                                        ShowDeleteButton="True" />
                                    <asp:BoundField DataField="periodoINI" DataFormatString="{0:dd/MM/yyyy}" 
                                        HeaderText="Fecha Inicio" SortExpression="periodoINI" />
                                    <asp:BoundField DataField="periodoFIN" DataFormatString="{0:dd/MM/yyyy}" 
                                        HeaderText="Fecha Fin" SortExpression="periodoFIN" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="sdsPeriodosBloqueados" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                DeleteCommand="DELETE FROM [PeriodosBloqueados] WHERE [perbloqID] = @original_perbloqID" 
                                InsertCommand="INSERT INTO [PeriodosBloqueados] ([periodoINI], [periodoFIN]) VALUES (@periodoINI, @periodoFIN)" 
                                OldValuesParameterFormatString="original_{0}" 
                                SelectCommand="SELECT [perbloqID], [periodoINI], [periodoFIN] FROM [PeriodosBloqueados] WHERE ([cuentaid] = @cuentaid) ORDER BY [periodoINI]" 
                                
                                UpdateCommand="UPDATE [PeriodosBloqueados] SET [periodoINI] = @periodoINI, [periodoFIN] = @periodoFIN WHERE [perbloqID] = @original_perbloqID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="drpdlCuenta" DefaultValue="-1" Name="cuentaid" 
                                        PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                                <DeleteParameters>
                                    <asp:Parameter Name="original_perbloqID" Type="Int32" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="periodoINI" Type="DateTime" />
                                    <asp:Parameter Name="periodoFIN" Type="DateTime" />
                                    <asp:Parameter Name="original_perbloqID" Type="Int32" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="periodoINI" Type="DateTime" />
                                    <asp:Parameter Name="periodoFIN" Type="DateTime" />
                                </InsertParameters>
                            </asp:SqlDataSource>
                        </asp:Panel>
                        <cc1:CollapsiblePanelExtender ID="pnlPeriodosBloqueados_CollapsiblePanelExtender" 
                            runat="server" CollapseControlID="chkPeriodosBloqueados" Collapsed="True" 
                            Enabled="True" ExpandControlID="chkPeriodosBloqueados" 
                            TargetControlID="pnlPeriodosBloqueados">
                        </cc1:CollapsiblePanelExtender>
                    </td>
                </tr>
		    	<tr>
		    	    <td colspan="6">
                        &nbsp;</td>
		    	    <td>
                        &nbsp;</td>
		    	</tr>
		    </table>
		</td>
		<td>
            <table>
                <tr>
                    <td align="center" class="TableHeader" >
                    
                        <asp:Label ID="lblCalculoSaldos" runat="server"></asp:Label>
                    
                    </td>
                </tr>
                <tr>
                    <td align="center">
                   
                      <asp:GridView ID="gridSaldosMensuales" 
                            runat="server" AutoGenerateColumns="False" DataKeyNames="cuentaID">
                            <Columns>
                                <asp:BoundField DataField="year" HeaderText="Año" />
                                <asp:BoundField DataField="month" HeaderText="Mes" SortExpression="month">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldo" DataFormatString="{0:c}" HeaderText="Saldo" 
                                    SortExpression="saldo">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="gridSaldosporPeriodos" 
                            runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Periodo"  
                                    HeaderText="Periodo" />
                                
                                <asp:BoundField DataField="saldo" DataFormatString="{0:C2}" 
                                    HeaderText="Saldo" />
                            </Columns>
                        </asp:GridView>
                   
                    </td>
                </tr>
            </table>
        </td>
		<td valign="top" align="center">
            <br />
                             
            </td>
	</tr>
	<tr> 
	    <td colspan="3" style="border-style: solid">
	        <asp:CheckBox ID="chkMostrarAddMov" runat="server" 
                Text="Mostrar panel para agregar nuevo movimiento" Checked="True" />
	        <asp:TextBox ID="txtidToModify" runat="server" Visible="False"></asp:TextBox>
	        <br />
	        <asp:Panel id="panelAgregar" runat="Server">
	        <table>
	        	<tr>
	        		<td class="TablaField" colspan="11">
                        <asp:Label ID="lblHeader" runat="server" 
                            Text="AGREGAR NUEVO MOVIMIENTO DE BANCO"></asp:Label>
                    </td>
	        		<td rowspan="9" valign="top">
	        		    &nbsp;</td>
	        		<td rowspan="9">
	        		    &nbsp;</td>
	        		
	        	</tr>
	        	<tr>
                    <td class="TablaField">
                        FECHA:</td>
                    <td class="TablaField">
                        Nombre interno:</td>
                    <td class="TablaField">
                        # de Factura o Larguillo:</td>
                    <td class="TablaField">
                        # de Cabezas:</td>
                    <td class="TablaField">
                        Concepto:</td>
                    <td rowspan="2" valign="top">
                        Grupo de catalogos de cuenta interna:<asp:DropDownList ID="drpdlGrupoCatalogos" 
                            runat="server" AutoPostBack="True" DataSourceID="sdsGruposCatalogos" 
                            DataTextField="grupoCatalogo" DataValueField="grupoCatalogosID" Height="23px" 
                            onselectedindexchanged="drpdlGrupoCatalogos_SelectedIndexChanged" Width="235px">
                        </asp:DropDownList><br />
                        <asp:SqlDataSource ID="sdsGruposCatalogos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos]">
                        </asp:SqlDataSource>
                        Catalogo de cuenta interna:<asp:DropDownList ID="drpdlCatalogoInterno" 
                            runat="server" AutoPostBack="True" DataSourceID="sdsCatalogoCuentaInterna" 
                            DataTextField="catalogoMovBanco" DataValueField="catalogoMovBancoID" 
                            Height="23px" Width="236px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsCatalogoCuentaInterna" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            SelectCommand="SELECT catalogoMovBancoID, claveCatalogo + ' -  ' + catalogoMovBanco AS catalogoMovBanco FROM catalogoMovimientosBancos WHERE (grupoCatalogoID = @grupoCatalogoID)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="drpdlGrupoCatalogos" DefaultValue="-1" 
                                    Name="grupoCatalogoID" PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <br />SubCatalogo de cuenta interna:<asp:DropDownList 
                            ID="drpdlSubcatologointerna" runat="server" AutoPostBack="True" 
                            DataSourceID="sdsSubCatalogoInterna" DataTextField="subCatalogo" 
                            DataValueField="subCatalogoMovBancoID" Height="23px" Width="234px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsSubCatalogoInterna" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            SelectCommand="SELECT SubCatalogoMovimientoBanco.subCatalogoClave + ' - ' + SubCatalogoMovimientoBanco.subCatalogo AS subCatalogo, SubCatalogoMovimientoBanco.subCatalogoMovBancoID FROM SubCatalogoMovimientoBanco INNER JOIN catalogoMovimientosBancos ON SubCatalogoMovimientoBanco.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID WHERE (SubCatalogoMovimientoBanco.catalogoMovBancoID = @catalogoMovBancoID)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="drpdlCatalogoInterno" DefaultValue="-1" 
                                    Name="catalogoMovBancoID" PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                    <td rowspan="2" valign="top">
                            <asp:CheckBox ID="chkMostrarFiscales" runat="server" 
                                Text="Los datos fiscales son diferentes que los internos." />
                            <div ID="divConceptosFiscales" runat="Server">
                            <br />
                                Nombre Fiscal:<br />
                                <asp:TextBox ID="txtNombre" runat="server" Width="250px"></asp:TextBox><br />
                                Grupo de catalogos de cuenta interna:<asp:DropDownList 
                                    ID="drpdlGrupoCuentaFiscal" runat="server" AutoPostBack="True" 
                                    DataSourceID="sdsGruposCatalogosfiscal" DataTextField="grupoCatalogo" 
                                    DataValueField="grupoCatalogosID" Height="23px" 
                                    onselectedindexchanged="drpdlGrupoCuentaFiscal_SelectedIndexChanged" 
                                    Width="257px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsGruposCatalogosfiscal" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos]">
                                </asp:SqlDataSource>
                                Catalogo de cuenta fiscal:<asp:DropDownList ID="drpdlCatalogocuentafiscal" 
                                    runat="server" AutoPostBack="True" DataSourceID="sdsCatalogoCuentaFiscal" 
                                    DataTextField="catalogoMovBanco" DataValueField="catalogoMovBancoID" 
                                    Height="23px" Width="256px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsCatalogoCuentaFiscal" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT catalogoMovBancoID, claveCatalogo + ' -  ' + catalogoMovBanco AS catalogoMovBanco FROM catalogoMovimientosBancos WHERE (grupoCatalogoID = @grupoCatalogoID)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="drpdlGrupoCuentaFiscal" DefaultValue="-1" 
                                            Name="grupoCatalogoID" PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                Subcatalogo de cuenta fiscal<asp:DropDownList ID="drpdlSubcatalogofiscal" 
                                    runat="server" AutoPostBack="True" DataSourceID="sdsSubcatalogofiscal" 
                                    DataTextField="subCatalogo" DataValueField="subCatalogoMovBancoID" 
                                    Height="23px" Width="258px">
                                </asp:DropDownList>
                                :<asp:SqlDataSource ID="sdsSubcatalogofiscal" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT SubCatalogoMovimientoBanco.subCatalogoClave + ' - ' + SubCatalogoMovimientoBanco.subCatalogo AS subCatalogo, SubCatalogoMovimientoBanco.subCatalogoMovBancoID FROM SubCatalogoMovimientoBanco INNER JOIN catalogoMovimientosBancos ON SubCatalogoMovimientoBanco.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID WHERE (SubCatalogoMovimientoBanco.catalogoMovBancoID = @catalogoMovBancoID)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="drpdlCatalogocuentafiscal" DefaultValue="-1" 
                                            Name="catalogoMovBancoID" PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                        </div>
                    </td>
                    <td class="TablaField">
                        # Cheque (*):</td>
                    <td class="TablaField">
                        Credito:</td>
                    <td class="TablaField">
                        Tipo de movimiento:</td>
                    <td class="TablaField">
                        Monto:</td>
                </tr>
	        	<tr>
	        		<td valign="top">
                        <asp:TextBox ID="txtFecha" runat="server" ReadOnly="True" Width="89px"></asp:TextBox>
                        <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtFecha" 
                            Separator="/" style="height: 16px" />
                            <br />
                        <asp:RequiredFieldValidator ID="valFecha" runat="server" 
                            ControlToValidate="txtFecha" ErrorMessage="El campo fecha es necesario"></asp:RequiredFieldValidator>
                    </td>
	        		<td valign="top">
                        <asp:TextBox ID="txtChequeNombre" runat="server" Width="250px"></asp:TextBox>
                    </td>
	        		<td valign="top">
                        <asp:TextBox ID="txtFacturaLarguillo" runat="server" Width="75px"></asp:TextBox>
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtNumCabezas" runat="server" Width="69px"></asp:TextBox>
                    </td>
	        		<td valign="top">
                        <asp:DropDownList ID="ddlConcepto" runat="server" DataSourceID="sdsConceptos" 
                            DataTextField="Concepto" DataValueField="ConceptoMovCuentaID" Height="23px" 
                            Width="167px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsConceptos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            SelectCommand="SELECT [ConceptoMovCuentaID], [Concepto] FROM [ConceptosMovCuentas]  where concepto &lt;&gt; 'ANTICIPO LIQUIDACION' ORDER BY [Concepto]">
                        </asp:SqlDataSource>
                    </td>
	        		<td valign="top">
                        <asp:TextBox ID="txtChequeNum" runat="server" Width="94px"></asp:TextBox>
                        <br />
                        
                            <table>
                        	    <tr>
                        		    <td>
                                        <asp:CheckBox ID="chkFechaChequeCobrado" runat="server" Text="Marcar el cheque cobrado con fecha:"  />
                                    </td>
                        	    </tr>
                        	    <tr>
                        	        <td>
                        	            <asp:Panel runat="Server" ID="pnlChequeFechaCobrado">
                                            <asp:TextBox ID="txtFechaChequeCobrado" runat="server"></asp:TextBox>
                        	                <rjs:PopCalendar ID="PopCalendar7" runat="server" 
                                                Control="txtFechaChequeCobrado" Separator="/" />
                                        </asp:Panel>
                        	            <cc1:CollapsiblePanelExtender ID="pnlChequeFechaCobrado_CollapsiblePanelExtender" 
                                            runat="server" CollapseControlID="chkFechaChequeCobrado" Collapsed="True" 
                                            Enabled="True" ExpandControlID="chkFechaChequeCobrado" 
                                            TargetControlID="pnlChequeFechaCobrado">
                                        </cc1:CollapsiblePanelExtender>
                        	        </td>
                        	    </tr>
                            </table>
                    </td>
                    <td valign="top">
                        <asp:CheckBox ID="chkAssignToCredit" runat="server" 
                            Text="Relacionar con credito" />
                            <div runat="Server" id="divAssignCredito">
                                <asp:DropDownList ID="ddlCreditoFinanciero" runat="server" 
                                    DataSourceID="sdsCreditosFinancieros" DataTextField="Credito" 
                                    DataValueField="creditoFinancieroID">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsCreditosFinancieros" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT CreditosFinancieros.creditoFinancieroID, Bancos.nombre + ' - ' + CreditosFinancieros.numCredito AS Credito FROM Bancos INNER JOIN CreditosFinancieros ON Bancos.bancoID = CreditosFinancieros.bancoID ORDER BY Credito"></asp:SqlDataSource>
                            </div>
                    </td>
	        		<td valign="top">
                        <asp:DropDownList ID="cmbTipodeMov" runat="server" Height="22px" 
                            onselectedindexchanged="cmbTipodeMov_SelectedIndexChanged" Width="100px">
                            <asp:ListItem>CARGO</asp:ListItem>
                            <asp:ListItem>ABONO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
	        		<td valign="top">
                        <asp:TextBox ID="txtMonto" runat="server"></asp:TextBox>
                        <br />
                        <br />
                    </td>
	        		
	        	</tr>
	            <tr>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td colspan="4" valign="top">
                        <div ID="divConCuentaYCajaDestino" runat="Server">
                            <div ID="divCuentaDestino" runat="Server">
                                Cuenta Destino: <br />
                                <asp:DropDownList ID="ddlCuentaDestino" runat="server" 
                                    DataSourceID="sdsCuentaDestino" DataTextField="Cuenta" 
                                    DataValueField="cuentaID">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsCuentaDestino" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    
                                    
                                    SelectCommand="SELECT CuentasDeBanco.cuentaID, '( ' + TiposDeMoneda.Moneda + ')  -  ' + Bancos.nombre + ' - ' + CuentasDeBanco.NumeroDeCuenta + ' - ' + CuentasDeBanco.Titular AS Cuenta FROM Bancos INNER JOIN CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID INNER JOIN TiposDeMoneda ON CuentasDeBanco.tipomonedaID = TiposDeMoneda.tipomonedaID INNER JOIN TraspasosPermitidos ON CuentasDeBanco.cuentaID = TraspasosPermitidos.cuentaDestinoID WHERE (TraspasosPermitidos.cuentaID = @cuentaID) ORDER BY Cuenta">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="drpdlCuenta" DefaultValue="-1" Name="cuentaid" 
                                            PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <div runat="Server" id="pnlTipoDeCambio">
                                Tipo de cambio:<br />
                                <asp:TextBox ID="txtTipodeCambio" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div runat="Server" id="divCajaDestino">
                                Caja Destino: <br />
                                <asp:DropDownList ID="ddlCajaDestino" runat="server" 
                                    DataSourceID="sdsCajaDestino" DataTextField="bodega" DataValueField="bodegaID">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsCajaDestino" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
                                </asp:SqlDataSource>
                                Ciclo: 
                                <asp:DropDownList ID="ddlCicloDestino" runat="server" 
                                    DataSourceID="sdsCicloDestino" DataTextField="CicloName" 
                                    DataValueField="cicloID">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsCicloDestino" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    
									SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] where cerrado=0 ORDER BY [fechaInicio] DESC">
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                </tr>
	            <tr>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td colspan="4" valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                </tr>
	            <tr>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top" align="right" colspan="7">
                        <asp:CheckBox ID="chkboxAnticipoGanado" runat="server" CssClass="TableHeader" 
                            Text="ES ANTICIPO DE GANADO" />
                        <asp:Panel runat="Server" id="panelProveedorGanado">
                            <table>
                            	<tr>
                            		<td>
                                        <asp:DropDownList ID="ddlProveedorGanado" runat="server" 
                                            DataSourceID="sdsProveedoresGanado" DataTextField="Nombre" 
                                            DataValueField="ganProveedorID">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsProveedoresGanado" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            SelectCommand="SELECT [ganProveedorID], [Nombre] FROM [gan_Proveedores] ORDER BY [Nombre]">
                                        </asp:SqlDataSource>
                                    </td>
                            	</tr>
                            </table>
                        </asp:Panel>
                        <cc1:CollapsiblePanelExtender ID="panelProveedorGanado_CollapsiblePanelExtender" 
                            runat="server" CollapseControlID="chkboxAnticipoGanado" Collapsed="True" 
                            Enabled="True" ExpandControlID="chkboxAnticipoGanado" 
                            TargetControlID="panelProveedorGanado">
                        </cc1:CollapsiblePanelExtender>
                    </td>
                </tr>
	            <tr>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td align="right" colspan="7" valign="top">
                        <asp:CheckBox ID="chkboxAnticipo" runat="server" CssClass="TableHeader" 
                            Text="Es anticipo" />
                        <div ID="divanticipo" runat="server">
                            <table>
                                <tr>
                                    <td align="center" class="TableHeader" colspan="3">
                                        DATOS DEL ANTICIPO</td>
                                    <td align="center" class="TableHeader">
                                        RELACIONAR BOLETAS A ANTICIPO<asp:UpdateProgress ID="UpdateProgress2" 
                                            runat="Server" AssociatedUpdatePanelID="UpdatePanelAnticipoBoletas" 
                                            DisplayAfter="0">
                                            <ProgressTemplate>
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                                                CARGANDO DATOS DE BOLETAS...
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TablaField">
                                        Productor:</td>
                                    <td>
                                        <br />
                                        <asp:DropDownList ID="drpdlProductorAnticipo" runat="server" 
                                            DataSourceID="sdsProductoresAnticipo" DataTextField="name" 
                                            DataValueField="productorID" Width="367px">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsProductoresAnticipo" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            SelectCommand="SELECT productorID, LTRIM(apaterno + ' ' + amaterno + ' '  + nombre) as name  FROM Productores order by name">
                                        </asp:SqlDataSource>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td rowspan="4">
                                        <asp:UpdatePanel ID="UpdatePanelAnticipoBoletas" runat="Server">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td align="center" class="TableHeader">
                                                            Agregar Boleta Rápida al productor:</td>
                                                        <td align="LEFT">
                                                            <table class="style3">
                                                                <tr>
                                                                    <td class="TablaField">
                                                                        # Folio:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTicket" runat="server"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TablaField">
                                                                        Ciclo:</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlCicloQuickBoleta" runat="server" 
                                                                            DataSourceID="sdsCicloQuickBoleta" DataTextField="CicloName" 
                                                                            DataValueField="cicloID" Height="23px" Width="199px">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="sdsCicloQuickBoleta" runat="server" 
                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                            
                                                                            SelectCommand="SELECT cicloID, CicloName FROM Ciclos WHERE (cerrado = 0) ORDER BY fechaInicio DESC">
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TablaField">
                                                                        Producto:</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlNewQuickBoletaProducto" runat="server" 
                                                                            DataSourceID="sdsNewQuickBoletaProductos" DataTextField="producto" 
                                                                            DataValueField="productoID" Height="22px" Width="171px">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="sdsNewQuickBoletaProductos" runat="server" 
                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                            SelectCommand="SELECT Productos.productoID, Productos.Nombre + ' - ' + Presentaciones.Presentacion AS producto FROM Productos INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID ORDER BY Productos.Nombre">
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TablaField">
                                                                        Bodega:</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlNewQuickBoletaBodega" runat="server" 
                                                                            DataSourceID="sdsNewQuickBoletaBodega" DataTextField="bodega" 
                                                                            DataValueField="bodegaID" Height="23px" Width="177px">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="sdsNewQuickBoletaBodega" runat="server" 
                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                            SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:Button ID="btnAgregarBoletadesdeTicket" runat="server" 
                                                                CausesValidation="False" CssClass="Button" 
                                                                onclick="btnAgregarBoletadesdeTicket_Click" Text="Agregar boleta a anticipo" />
                                                        </td>
                                                        <td align="LEFT" valign="top">
                                                            BOLETAS RELACIONADAS
                                                            <br />
                                                            CON EL ANTICIPO<asp:ListBox ID="listBoxAgregadas" runat="server" Height="120px" 
                                                                Width="142px"></asp:ListBox>
                                                            <br />
                                                            <asp:Button ID="btnQuitarBoleta" runat="server" CausesValidation="False" 
                                                                onclick="btnQuitarBoleta_Click" Text="Quitar Boleta" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            &nbsp;</td>
                                                        <td align="LEFT">
                                                            <asp:CheckBox ID="chkNewBoleta" runat="server" 
                                                                Text="AGREGAR NUEVA BOLETA Y ASIGNAR A ANTICIPO" />
                                                        </td>
                                                        <td align="LEFT">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            <div ID="divNewBoleta" runat="Server">
                                                                <table>
                                                                    <tr>
                                                                        <td align="center" class="TableHeader" colspan="2">
                                                                            AGREGAR NUEVA BOLETA</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" class="TablaField">
                                                                            CICLO:</td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="ddlCiclo" runat="server" DataSourceID="sdsCiclo" 
                                                                                DataTextField="CicloName" DataValueField="cicloID" Height="23px" Width="199px">
                                                                            </asp:DropDownList>
                                                                            <asp:SqlDataSource ID="sdsCiclo" runat="server" 
                                                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                
                                                                                SelectCommand="SELECT cicloID, CicloName FROM Ciclos WHERE (cerrado = 0) ORDER BY fechaInicio DESC">
                                                                            </asp:SqlDataSource>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" class="TablaField">
                                                                            PRODUCTOR:</td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="ddlNewBoletaProductor" runat="server" 
                                                                                DataSourceID="sdsNewBoletaProductor" DataTextField="Productor" 
                                                                                DataValueField="productorID" Height="23px" Width="203px">
                                                                            </asp:DropDownList>
                                                                            <asp:SqlDataSource ID="sdsNewBoletaProductor" runat="server" 
                                                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                SelectCommand="SELECT productorID, apaterno + ' ' + amaterno + ' ' + nombre AS Productor FROM Productores ORDER BY Productor">
                                                                            </asp:SqlDataSource>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" class="TablaField">
                                                                            # BOLETO
                                                                            <br />
                                                                            DE BASCULA:</td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtNewNumBoleta" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" class="TablaField">
                                                                            # DE FOLIO:</td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtNewTicket" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" class="TablaField">
                                                                            PRODUCTO:</td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="ddlNewBoletaProducto" runat="server" 
                                                                                DataSourceID="sdsNewBoletaProductos" DataTextField="Producto" 
                                                                                DataValueField="productoID" Height="22px" Width="171px">
                                                                            </asp:DropDownList>
                                                                            <asp:SqlDataSource ID="sdsNewBoletaProductos" runat="server" 
                                                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                SelectCommand="SELECT Productos.productoID, Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Producto FROM Productos INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID ORDER BY Productos.Nombre">
                                                                            </asp:SqlDataSource>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" class="TablaField">
                                                                            BODEGA:</td>
                                                                        <td align="left">
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
                                                                        <td align="right" class="TablaField">
                                                                            FECHA ENTRADA:</td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtNewFechaEntrada" runat="server" ReadOnly="True"></asp:TextBox>
                                                                            <rjs:PopCalendar ID="PopCalendar3" runat="server" Control="txtNewFechaEntrada" 
                                                                                Separator="/" />
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
                                                                        <td align="right" class="TablaField">
                                                                            PESO BRUTO:</td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtNewPesoEntrada" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" class="TablaField">
                                                                            PESO TARA:</td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtNewPesoSalida" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" class="TablaField">
                                                                            PESO NETO:</td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtPesoNetoNewBoleta" runat="server" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" class="TablaField">
                                                                            HUMEDAD:</td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtNewHumedad" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" class="TablaField">
                                                                            IMPUREZAS:</td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtNewImpurezas" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" class="TablaField">
                                                                            PRECIO:</td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtNewPrecio" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" class="TablaField">
                                                                            SECADO:</td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtNewSecado" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" colspan="2">
                                                                            <asp:Button ID="btnAgregarBol" runat="server" CausesValidation="False" 
                                                                                CssClass="Button" onclick="btnAgregarBol_Click" 
                                                                                Text="Agregar y Asignar a Anticipo" />
                                                                            <asp:Panel ID="panelNewBoletaResult" runat="server">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td style="text-align: center">
                                                                                            <asp:Image ID="imgNewPalomita" runat="server" 
                                                                                                ImageUrl="~/imagenes/palomita.jpg" Visible="False" />
                                                                                            <asp:Image ID="imgNewTache" runat="server" ImageUrl="~/imagenes/tache.jpg" 
                                                                                                Visible="False" />
                                                                                            <asp:Label ID="lblMensajeNewBoleta" runat="server" SkinID="lblMensajeTitle" 
                                                                                                Text="RESULTADO AL AGREGAR BOLETA"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="text-align: center">
                                                                                            <asp:Label ID="lblMsgResult" runat="server" SkinID="lblMensajeOperationresult" 
                                                                                                Text="Label"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            &nbsp;</td>
                                                        <td align="center" valign="top">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TablaField">
                                        Tipo de anticipo:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipoAnticipo" runat="server" 
                                            DataSourceID="sdsTipoAnticipo" DataTextField="tipoAnticipo" 
                                            DataValueField="tipoAnticipoID" Width="250px">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="sdsTipoAnticipo" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            SelectCommand="SELECT [tipoAnticipoID], [tipoAnticipo] FROM [TiposAnticipos]">
                                        </asp:SqlDataSource>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3" valign="top">
                                        <div ID="divPrestamo" runat="Server">
                                            <table>
                                                <tr>
                                                    <td class="TablaField">
                                                        Interes anual:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtInteresAnual" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TablaField">
                                                        Interes moratorio:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtInteresmoratorio" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TablaField">
                                                        Fecha limite de pago:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaLimite" runat="server" ReadOnly="True"></asp:TextBox>
                                                        <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtFechaLimite" 
                                                            Separator="/" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3">
                                        &nbsp;</td>
                                    <td align="center">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
	            <tr>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td align="right" colspan="5" valign="top">
                        <asp:CheckBox ID="chkNoResetControls" runat="server" 
                            Text="No Borrar los campos despues de agregar" /><br />
                        <asp:Button ID="btnAgregar" runat="server" onclick="btnAgregar_Click1" 
                            style="height: 26px" Text="AGREGAR EL NUEVO MOVIMIENTO" />
                    </td>
                </tr>
	            <tr>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td valign="top">
                        &nbsp;</td>
                    <td align="right" colspan="5" valign="top">
                        <asp:Panel ID="panelNewMovimientodeBanco" runat="server">
                            <table>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Image ID="imgNewPalomitaBanco" runat="server" 
                                            ImageUrl="~/imagenes/palomita.jpg" Visible="False" />
                                        <asp:Image ID="imgNewTacheBanco" runat="server" ImageUrl="~/imagenes/tache.jpg" 
                                            Visible="False" />
                                        <asp:Label ID="lblMensajeNewMovBanco" runat="server" SkinID="lblMensajeTitle" 
                                            Text="RESULTADO AL AGREGAR BOLETA"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblMsgResultAddMovBanco" runat="server" 
                                            SkinID="lblMensajeOperationresult" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        &nbsp;<asp:HyperLink ID="HyperLink2" runat="server">HyperLink</asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
	            <tr>
                    <td colspan="13" style="border-style: solid">
                        &nbsp;</td>
                </tr>
	            <tr>
                    <td colspan="13">
                        &nbsp;</td>
                </tr>
	        </table>
	        </asp:Panel>
	    </td>
	</tr>
        <tr>
            <td colspan="3">
                <table>
                    <tr>
                        <td align="center" class="TableHeader" colspan="3">
                            LISTA DE MOVIMIENTOS DE BANCO</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table>
                                <tr>
                                    <td class="TablaField">
                                        Total Cargos:</td>
                                    <td align="right">
                                        <asp:Label ID="lblTotalCargos" runat="server" Text="$ 0.00"></asp:Label>
                                    </td>
                                    <td class="TablaField">
                                        Total Abonos:</td>
                                    <td align="right">
                                        <asp:Label ID="lblTotalAbonos" runat="server" Text="$ 0.00"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TablaField">
                                        Saldo inicial:&nbsp;
                                    </td>
                                    <td>
                                        &nbsp;<asp:Label ID="lblSaldoInicial" runat="server" Font-Bold="True" 
                                            Font-Size="Large" Text="lblSaldoInicial"></asp:Label>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td class="TablaField">
                                        Saldo final:
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblSaldofinal" runat="server" Font-Bold="True" Font-Size="Large" 
                                            Text="lblSaldoFinal"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblFechaInicio" runat="server" Text="lblfechaInicio" 
                                Visible="False"></asp:Label>
                            <asp:Label ID="lblfechaFIn" runat="server" Text="lblFechaFin" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="TablaField">
                            Mostrar:</td>
                        <td>
                            <asp:CheckBoxList ID="cblColToShow" runat="server" RepeatColumns="4" 
                                RepeatDirection="Horizontal" Width="793px">
                                <asp:ListItem Selected="True">Nombre</asp:ListItem>
                                <asp:ListItem Selected="True"># de Factura o Larguillo</asp:ListItem>
                                <asp:ListItem Selected="True"># de Cabezas</asp:ListItem>
                                <asp:ListItem Selected="True"># de Cheque</asp:ListItem>
                                <asp:ListItem>Usuario</asp:ListItem>
                                <asp:ListItem>Fecha de ingreso</asp:ListItem>
                                <asp:ListItem>Ultima modificacion</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td>
                            <asp:Button ID="btnActualizar" runat="server" CausesValidation="False" 
                                CssClass="Button" onclick="btnActualizar_Click" Text="Actualizar" />
                            <asp:Button ID="btnActualizaChequeStatus" runat="server" 
                                CausesValidation="False" Height="27px" onclick="btnActualizaChequeStatus_Click" 
                                Text="Actualizar estado de Cobrado" Width="249px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="pnlMovBancos" runat="server">
                                <asp:GridView ID="gridMovCuentasBanco" runat="server" 
                                AutoGenerateColumns="False" CellPadding="4"     
                                
                                    DataKeyNames="movbanID,fecha,abono,cargo,subCatalogoMovBancoInternoID,subCatalogoMovBancoFiscalID,catalogoMovBancoInternoID,catalogoMovBancoFiscalID,conceptoID,numCheque,concepto,nombre" ForeColor="Black" 
                                GridLines="None" onrowcancelingedit="gridMovCuentasBanco_RowCancelingEdit" 
                                onrowdeleting="gridMovCuentasBanco_RowDeleting" 
                                onrowediting="gridMovCuentasBanco_RowEditing" 
                                onrowupdating="gridMovCuentasBanco_RowUpdating" 
                                onselectedindexchanged="gridMovCuentasBanco_SelectedIndexChanged" 
                                onrowdatabound="gridMovCuentasBanco_RowDataBound" 
                                onprerender="gridMovCuentasBanco_PreRender">
                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" Font-Size="Small" />
                                <HeaderStyle CssClass="TableHeader" Font-Size="Small" />
                                <AlternatingRowStyle BackColor="White" Font-Size="Small" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <EditItemTemplate>
                                            <asp:Button ID="Button1" runat="server" CausesValidation="True" 
                                                CommandName="Update" Text="Actualizar" />
                                            &nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False" 
                                                CommandName="Cancel" Text="Cancelar" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Button ID="btnModificar" runat="server" CausesValidation="False" 
                                                CommandName="Edit" Text="Modificar" />
                                            &nbsp;<asp:Button ID="btnEliminar" runat="server" CausesValidation="False" 
                                                CommandName="Delete" Text="Eliminar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField ConvertEmptyStringToNull="False" DataField="movbanID" 
                                        HeaderText="movbanID" InsertVisible="False" ItemStyle-HorizontalAlign="Right" 
                                        ReadOnly="True" SortExpression="movbanID" Visible="False">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Fecha" SortExpression="fecha">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtFecha" runat="server" Text='<%# Bind("fecha") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" 
                                                Text='<%# Bind("fecha", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre Fiscal" 
                                        SortExpression="nombre" />
                                    <asp:BoundField DataField="facturaOlarguillo" 
                                        HeaderText="# de Factura o Larguillo" 
                                        SortExpression="facturaOlarguillo" />
                                    <asp:BoundField DataField="numCabezas" HeaderText="# de Cabezas" />
                                    <asp:TemplateField HeaderText="Concepto">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("concepto") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="drpdlConceptosgrid" runat="server" 
                                                DataSourceID="sdsConceptosgrid" DataTextField="Concepto" 
                                                DataValueField="ConceptoMovCuentaID" Height="23px" 
                                                Width="190px">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="sdsConceptosgrid" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                SelectCommand="SELECT [ConceptoMovCuentaID], [Concepto] FROM [ConceptosMovCuentas]">
                                            </asp:SqlDataSource>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Catalogo Fiscal" SortExpression="concepto">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="drpdlCatalogoFiscalgrid" runat="server" 
                                                DataSourceID="sdsCatalogoFiscalgrid" DataTextField="catalogo" 
                                                DataValueField="catalogoMovBancoID" Height="23px" Width="217px">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="sdsCatalogoFiscalgrid" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                SelectCommand="SELECT catalogoMovBancoID, claveCatalogo + ' - ' + catalogoMovBanco AS catalogo FROM catalogoMovimientosBancos">
                                            </asp:SqlDataSource>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("conceptofiscal") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subcatalogo fiscal">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="drpSubCatalogoFiscalgrid" runat="server" 
                                                DataSourceID="sdsSubCatalogoFiscalgrid" DataTextField="Expr1" 
                                                DataValueField="subCatalogoMovBancoID" Height="23px" Width="206px" 
                                                ondatabound="drpSubCatalogoFiscalgrid_DataBound">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="sdsSubCatalogoFiscalgrid" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                SelectCommand="SELECT subCatalogoMovBancoID, subCatalogoClave + ' - ' + subCatalogo AS Expr1 FROM SubCatalogoMovimientoBanco">
                                            </asp:SqlDataSource>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("subconceptofiscal") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="nombreinterno" HeaderText="Nombre interno" />
                                    <asp:TemplateField HeaderText="Catalogo interno">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="drpdlCatalogoInternogrid" runat="server" 
                                                DataSourceID="sdsCatalogoInternogrid" DataTextField="Expr1" 
                                                DataValueField="catalogoMovBancoID" Height="23px" Width="214px">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="sdsCatalogoInternogrid" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                
                                                SelectCommand="SELECT catalogoMovBancoID, claveCatalogo + ' - ' + catalogoMovBanco AS Expr1 FROM catalogoMovimientosBancos">
                                            </asp:SqlDataSource>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("conceptointerno") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subcatalogo interno">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="drpdlSubcatalogoInternogrid" runat="server" 
                                                DataSourceID="sdsSubcatalogoInternogrid" DataTextField="subcatalogo" 
                                                DataValueField="subCatalogoMovBancoID" Height="23px" Width="211px" 
                                                ondatabound="drpdlSubcatalogoInternogrid_DataBound">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="sdsSubcatalogoInternogrid" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                SelectCommand="SELECT subCatalogoMovBancoID, subCatalogoClave + ' - ' + subCatalogo AS subcatalogo FROM SubCatalogoMovimientoBanco">
                                            </asp:SqlDataSource>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("subconceptointerno") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="# de Cheque">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtNumCheque" runat="server" Text='<%# Bind("numCheque") %>'></asp:TextBox>
                                            <asp:Label ID="lblNumCheque" runat="server" Text='<%# Bind("numCheque") %>' 
                                                Visible="False"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNumCheque" runat="server" Text='<%# Bind("numCheque") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cargo" SortExpression="cargo">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCargo" runat="server" Text='<%# Bind("cargo") %>'></asp:TextBox>
                                            <br />
                                            <asp:Label ID="lblCargo" runat="server" Text='<%# Bind("cargo", "{0:c}") %>' 
                                                Visible="False"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCargo" runat="server" Text='<%# Bind("cargo", "{0:c}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Abono" SortExpression="abono">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtAbono" runat="server" Text='<%# Bind("abono") %>'></asp:TextBox>
                                            <br />
                                            <asp:Label ID="lblAbono" runat="server" Text='<%# Bind("abono", "{0:c}") %>' 
                                                Visible="False"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAbono" runat="server" Text='<%# Bind("abono", "{0:c}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:c}" 
                                        ReadOnly="True" SortExpression="saldo" />
                                    <asp:TemplateField HeaderText="Cobrado">
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="chkChequeCobrado" runat="server" Checked = '<%# Bind("chequecobrado") %>'
                                                />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkChequeCobrado" runat="server" 
                                                Checked = '<%# Bind("chequecobrado") %>'
                                                 />
                                            <asp:TextBox ID="txtFechaCobrado" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="userID" HeaderText="Usuario" 
                                        SortExpression="Nombre" ReadOnly="True" />
                                    <asp:BoundField DataField="storeTS" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderText="Fecha de ingreso" 
                                        ReadOnly="True" SortExpression="storeTS" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="updateTS" HeaderText="Ultima modificacion" 
                                        ReadOnly="True" >
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="bancoCreditoFinanciero" 
                                        HeaderText="Banco Credito" ReadOnly="True">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="numCredito" HeaderText="# Credito" 
                                        ReadOnly="True">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="catalogoMovBancoFiscalID" Visible="False" />
                                    <asp:BoundField DataField="catalogoMovBancoInternoID" 
                                        HeaderText="catalogoMovBancoInternoID" Visible="False" />
                                    <asp:BoundField DataField="subCatalogoMovBancoInternoID" Visible="False" 
                                        HeaderText="subCatalogoMovBancoInternoID" />
                                    <asp:BoundField DataField="subCatalogoMovBancoInternoID" 
                                        HeaderText="subCatalogoMovBancoFiscalID" Visible="False" />
                                    <asp:BoundField DataField="conceptoID" 
                                        HeaderText="conceptoID" Visible="False" />
                                    <asp:BoundField 
                                        HeaderText="Boletas asignadas a anticipo" />
                                    <asp:TemplateField HeaderText="Mov. Origen">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server">HyperLink</asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableHeader" colspan="3">
                            SALDOS DE CHEQUES</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table>
                                <tr>
                                    <td class="TablaField">
                                        Numero total de cheques girados:</td>
                                    <td>
                                        <asp:Label ID="lblChequesGirados" runat="server" Font-Bold="True" 
                                            Font-Size="Large" Text="lblChequesGirados"></asp:Label>
                                    </td>
                                    <td class="TablaField">
                                        Monto en cheques girados:</td>
                                    <td>
                                        <asp:Label ID="lblTotalGirados" runat="server" Font-Bold="True" 
                                            Font-Size="Large" Text="lblTotalGirados"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TablaField">
                                        Numero total de cheques no cobrados:</td>
                                    <td>
                                        <asp:Label ID="lblChequesNoCobrados" runat="server" Font-Bold="True" 
                                            Font-Size="Large" Text="lblChequesNoCobrados"></asp:Label>
                                    </td>
                                    <td class="TablaField">
                                        Monto en cheques no cobrados:</td>
                                    <td>
                                        <asp:Label ID="lblTotalNoCobrados" runat="server" Font-Bold="True" 
                                            Font-Size="Large" Text="lblTotalNoCobrados"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TablaField">
                                        Numero total de cheques cobrados:</td>
                                    <td>
                                        <asp:Label ID="lblChequesCobrados" runat="server" Font-Bold="True" 
                                            Font-Size="Large" Text="lblChequesCobrados"></asp:Label>
                                    </td>
                                    <td class="TablaField">
                                        Monto en cheques cobrados:</td>
                                    <td>
                                        <asp:Label ID="lblTotalCobrados" runat="server" Font-Bold="True" 
                                            Font-Size="Large" Text="lblTotalCobrados"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnMissingCheques" runat="server" Text="Cheques Faltantes" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
