<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" Theme="skinrojo" AutoEventWireup="true" CodeBehind="frmAddMovCajaChica.aspx.cs" Inherits="Garibay.WebForm4" Title="Nuevo Movimiento" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="Server" id="UpdatePanelMain">
<ContentTemplate>
<asp:UpdateProgress runat="server" ID="UpdateProgress" 
        AssociatedUpdatePanelID="UpdatePanelMain" DisplayAfter="0">
    <ProgressTemplate>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/cargando.gif" />CARGANDO INFORMACIÓN...
    </ProgressTemplate>
</asp:UpdateProgress>
    
    <asp:Panel ID="panelMensaje" runat="server" > 
        <table>
            <tr>
                <td style="text-align: center">
                           
                           <asp:Image ID="imagenbien" runat="server" ImageUrl="~/imagenes/palomita.jpg" 
                               Visible="False" />
                           <asp:Image ID="imagenmal" runat="server" ImageUrl="~/imagenes/tache.jpg" 
                               Visible="False" />
                           <asp:Label ID="lblMensajetitle" runat="server" SkinID="lblMensajeTitle" 
                               Text="PRUEBA"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                           <asp:Label ID="lblMensajeOperationresult" runat="server"  Text="Label" 
                               SkinID="lblMensajeOperationresult"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblMensajeException" runat="server" SkinID="lblMensajeException" 
                        Text="SI NO HAY EXC BORREN EL TEXTO"></asp:Label>
                </td>
            </tr>
            <tr>
                        <td align="center">
                        
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                
                                
                                
                                SelectCommand="SELECT Ciclos.CicloName, MovimientosCaja.fecha, MovimientosCaja.nombre, MovimientosCaja.cargo, MovimientosCaja.abono, MovimientosCaja.Observaciones, MovimientosCaja.storeTS, MovimientosCaja.updateTS FROM MovimientosCaja INNER JOIN Ciclos ON MovimientosCaja.cicloID = Ciclos.cicloID WHERE (MovimientosCaja.movimientoID = @movimientoID)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtIDDetails" DefaultValue="-1" 
                                        Name="movimientoID" PropertyName="Text" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="300px" 
                                AutoGenerateRows="False" DataSourceID="SqlDataSource1">
                                <Fields>
                                   
                                    <asp:BoundField DataField="CicloName" HeaderText="Ciclo:" 
                                        SortExpression="CicloName" />
                                    <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                        HeaderText="Fecha:" SortExpression="fecha" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre:" 
                                        SortExpression="nombre" />
                                    <asp:BoundField DataField="cargo" DataFormatString="{0:c}" HeaderText="Cargo:" 
                                        SortExpression="cargo" />
                                    <asp:BoundField DataField="abono" DataFormatString="{0:c}" HeaderText="Abono:" 
                                        SortExpression="abono" />
                                    <asp:BoundField DataField="Observaciones" HeaderText="Observaciones:" 
                                        SortExpression="Observaciones" />
                                    <asp:BoundField DataField="storeTS" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" 
                                        HeaderText="Fecha de ingreso:" SortExpression="storeTS" />
                                    <asp:BoundField DataField="updateTS" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" 
                                        HeaderText="Fecha de modificación:" SortExpression="updateTS" />
                                </Fields>
                            </asp:DetailsView>
                        
                        </td>
                   </tr>
                   <tr>
                        <td align="center">
                        
                            <asp:Button ID="btnAceptardtlst" runat="server" Text="Aceptar" 
                                onclick="btnAceptardtlst_Click" />
                        
                            <asp:TextBox ID="txtIDDetails" runat="server" Visible="False"></asp:TextBox>
                        
                        </td>
                   </tr>
        </table>
</asp:Panel>

<asp:Panel ID="panelagregar" runat="server"> 
<table>
	<tr>
		<td align="center" class="TableHeader" colspan="2">
		    <asp:Label ID="lblMovCajaChica" runat="server" 
                Text="AGREGAR NUEVO MOVIMIENTO DE CAJA CHICA"></asp:Label>
		
		</td>
	</tr>
	
	<tr>
	    <td class="TablaField">
	        Bodega:</td>
	    <td >
	        <asp:DropDownList ID="ddlBodegas" runat="server" AutoPostBack="True" 
                DataSourceID="sdsBodegas" DataTextField="bodega" DataValueField="bodegaID" 
                onselectedindexchanged="ddlBodegas_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sdsBodegas" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
            </asp:SqlDataSource>
	    </td>
	    <td>
	        &nbsp;</td>
	</tr>
	<tr>
        <td class="TablaField">
            Ciclo :
        </td>
        <td>
            <asp:DropDownList ID="ddlIdCiclo" runat="server" DataSourceID="SqlDataSource3" 
                DataTextField="CicloName" DataValueField="cicloID" Width="155px">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                
				
				
                SelectCommand="SELECT cicloID, CicloName FROM Ciclos WHERE (cerrado = @cerrado) ORDER BY fechaInicio DESC">
            	<SelectParameters>
					<asp:Parameter DefaultValue="FALSE" Name="cerrado" Type="Boolean" />
				</SelectParameters>
            </asp:SqlDataSource>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="ddlIdCiclo" ErrorMessage="El campo Ciclo es necesario"></asp:RequiredFieldValidator>
        </td>
    </tr>
	<tr>
	    <td class="TablaField">
	    Nombre :
	    </td>
	    <td >
	        <asp:TextBox ID="txtNombre" runat="server" Width="300px"></asp:TextBox>
	    </td>
	    <td>
	        <asp:RequiredFieldValidator ID="RFVNombre" runat="server" 
                ControlToValidate="txtNombre" 
                ErrorMessage="El campo del Nombre es necesario"></asp:RequiredFieldValidator>
                <br />
        </td>
	   
	</tr>
	<tr>
	    <td class="TablaField">
	        Tipo de movimiento:</td>
	    <td >
	        <asp:DropDownList ID="cmbTipodeMov" runat="server" Height="22px" Width="249px">
                <asp:ListItem>CARGO</asp:ListItem>
                <asp:ListItem>ABONO</asp:ListItem>
            </asp:DropDownList>
	    </td>
	    <td>
	        
            <br />
        </td>
	   
	</tr>
	<tr>
	    <td class="TablaField">
	        Monto:</td>
	    <td >
	        <asp:TextBox ID="txtMonto" runat="server">1</asp:TextBox>
	    </td>
	    <td>
	        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txtMonto" ErrorMessage="El campo Monto es necesario"></asp:RequiredFieldValidator>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="txtMonto" ErrorMessage="Escriba una cantidad válida" 
                ValidationExpression="^\$?(\d{1,3},?(\d{3},?)*\d{3}(.\d{0,3})?|\d{1,3}(.\d{2})?)$"></asp:RegularExpressionValidator>
            <br />
        </td>
	   
	</tr>
	
	<tr>
        <td class="TablaField">
            Fecha:</td>
        <td>
            <asp:TextBox ID="txtFecha" runat="server" ReadOnly="True"></asp:TextBox>
            <rjs:PopCalendar ID="PopCalendar1" runat="server" Separator="/" Control = "txtFecha" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
	
	<tr>
        <td class="TablaField">
            Grupo de catálogo de cuenta interna:</td>
        <td>
            <asp:DropDownList ID="drpdlGrupoCatalogos" runat="server" AutoPostBack="True" 
                DataSourceID="sdsGruposCatalogos" DataTextField="grupoCatalogo" 
                DataValueField="grupoCatalogosID" Height="23px" 
                onselectedindexchanged="drpdlGrupoCatalogos_SelectedIndexChanged" Width="235px">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sdsGruposCatalogos" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos]">
            </asp:SqlDataSource>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TablaField">
            Catálogo de cuenta interna:</td>
        <td>
            <asp:DropDownList ID="drpdlCatalogoInterno" runat="server" AutoPostBack="True" 
                DataSourceID="sdsCatalogoCuentaInterna" DataTextField="catalogoMovBanco" 
                DataValueField="catalogoMovBancoID" Height="23px" Width="236px">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sdsCatalogoCuentaInterna" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                SelectCommand="SELECT catalogoMovBancoID, catalogoMovBanco FROM catalogoMovimientosBancos WHERE (grupoCatalogoID = @grupoCatalogoID)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="drpdlGrupoCatalogos" DefaultValue="-1" 
                        Name="grupoCatalogoID" PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
        <td>
            &nbsp;</td>
    </tr>
	
	<tr>
	    <td class="TablaField">
	        Subcatálogo de cuenta interna:</td>
	    <td >
	        <asp:DropDownList ID="drpdlSubcatologointerna" runat="server" 
                AutoPostBack="True" DataSourceID="sdsSubCatalogoInterna" 
                DataTextField="subCatalogo" DataValueField="subCatalogoMovBancoID" 
                Height="23px" 
                Width="234px">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sdsSubCatalogoInterna" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                SelectCommand="SELECT SubCatalogoMovimientoBanco.subCatalogo, SubCatalogoMovimientoBanco.subCatalogoMovBancoID FROM SubCatalogoMovimientoBanco INNER JOIN catalogoMovimientosBancos ON SubCatalogoMovimientoBanco.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID WHERE (SubCatalogoMovimientoBanco.catalogoMovBancoID = @catalogoMovBancoID)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="drpdlCatalogoInterno" DefaultValue="-1" 
                        Name="catalogoMovBancoID" PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>
	    </td>
	    <td>
	        &nbsp;</td>
	</tr>
	<tr>
        <td colspan="3">
            <div ID="divConCuentaYCajaDestino" runat="Server">
                DATOS DE TRASPASO:
                <div ID="divCuentaDestino" runat="Server">
                    Cuenta Destino:
                    <asp:DropDownList ID="ddlCuentaDestino" runat="server" 
                        DataSourceID="sdsCuentaDestino" DataTextField="Cuenta" 
                        DataValueField="cuentaID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsCuentaDestino" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT CuentasDeBanco.cuentaID, Bancos.nombre + ' - ' + CuentasDeBanco.NumeroDeCuenta + ' - ' + CuentasDeBanco.Titular AS Cuenta FROM Bancos INNER JOIN CuentasDeBanco ON Bancos.bancoID = CuentasDeBanco.bancoID ORDER BY Cuenta">
                    </asp:SqlDataSource>
                </div>
                <div ID="divCajaDestino" runat="Server">
                    Caja Destino:
                    <asp:DropDownList ID="ddlCajaDestino" runat="server" 
                        DataSourceID="sdsCajaDestino" DataTextField="bodega" DataValueField="bodegaID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsCajaDestino" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        SelectCommand="SELECT bodegaID, bodega FROM Bodegas WHERE (bodegaID &lt;&gt; @bodegaID) ORDER BY bodega">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlBodegas" DefaultValue="-1" Name="bodegaID" 
                                PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    Ciclo:
                    <asp:DropDownList ID="ddlCicloDestino" runat="server" 
                        DataSourceID="sdsCicloDestino" DataTextField="CicloName" 
                        DataValueField="cicloID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsCicloDestino" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
						SelectCommand="SELECT cicloID, CicloName FROM Ciclos WHERE (cerrado = @cerrado) ORDER BY fechaInicio">
                    	<SelectParameters>
							<asp:Parameter DefaultValue="FALSE" Name="cerrado" />
						</SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td class="TablaField">
            # de cabezas:</td>
        <td>
            <asp:TextBox ID="txtNumCabezas" runat="server" Height="23px" Width="174px"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TablaField">
            # de factura o larguilllo:</td>
        <td>
            <asp:TextBox ID="txtNumFacturaoLarguillo" runat="server" Width="172px"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TablaField">
            Observaciones :
        </td>
        <td>
            <asp:TextBox ID="txtObser" runat="server" Height="84px" TextMode="MultiLine" 
                Width="294px"></asp:TextBox>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:UpdatePanel runat="Server" ID="UpdatePanelBoletas">
            <ContentTemplate>
                <asp:UpdateProgress runat="server" ID="UpdateProgressBoletas" 
                        AssociatedUpdatePanelID="UpdatePanelBoletas" DisplayAfter="0">
                    <ProgressTemplate>
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/cargando.gif" />CARGANDO INFORMACIÓN...
                    </ProgressTemplate>
                </asp:UpdateProgress>
            <asp:CheckBox ID="chkboxAnticipo" runat="server" Text="Es anticipo" />
            <div id="divanticipo" runat="Server">
            
                <table >
                    <tr>
                        <td align="center" class="TableHeader" colspan="5">
                            DATOS DEL ANTICIPO</td>
                    </tr>
                    <tr>
                        <td class="TablaField">
                            Productor:</td>
                        <td colspan="2">
                            <br />
                            <asp:DropDownList ID="drpdlProductor" runat="server" 
                                DataSourceID="sdsProductores" DataTextField="name" DataValueField="productorID" 
                                Width="367px" >
                            </asp:DropDownList>
                            <cc1:ListSearchExtender ID="drpdlProductor_ListSearchExtender" runat="server" 
                                Enabled="True" TargetControlID="drpdlProductor" 
                                PromptText="Escriba para buscar">
                            </cc1:ListSearchExtender>
                            <asp:SqlDataSource ID="sdsProductores" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                
                                SelectCommand="SELECT productorID, LTRIM(apaterno + ' ' + amaterno + ' '  + nombre) as name  FROM Productores order by name">
                            </asp:SqlDataSource>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="TablaField">
                            Tipo de anticipo:</td>
                        <td colspan="2">
                            <asp:DropDownList ID="drpdlTipoAnticipo" runat="server" Width="250px" 
                                DataSourceID="sdsTiposAnticipos" DataTextField="tipoAnticipo" 
                                DataValueField="tipoAnticipoID">
                                <asp:ListItem>ANTICIPO NORMAL</asp:ListItem>
                                <asp:ListItem>PRESTAMO</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="sdsTiposAnticipos" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                SelectCommand="SELECT [tipoAnticipoID], [tipoAnticipo] FROM [TiposAnticipos]">
                            </asp:SqlDataSource>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="5">
                        <div id="divPrestamo" runat="Server">
                        
                        
                            <table >
                                <tr>
                                    <td class="TablaField">
                                        Interés anual:</td>
                                    <td>
                                        <asp:TextBox ID="txtInteresAnual" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TablaField">
                                        Interés moratorio:</td>
                                    <td>
                                        <asp:TextBox ID="txtInteresmoratorio" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TablaField">
                                        Fecha límite de pago:</td>
                                    <td>
                                        <asp:TextBox ID="txtFechaLimite" runat="server" ReadOnly="True"></asp:TextBox>
                                        <rjs:PopCalendar ID="PopCalendar3" runat="server" Separator="/" Control="txtFechaLimite"/>
                                    </td>
                                </tr>
                            </table>
                        
                        
                        </div>
                            </td>
                    </tr>
                    <tr>
                        <td align="center" class="TableHeader" colspan="5">
                            RELACIONAR BOLETAS CON ANTICIPO</td>
                    </tr>
                    <tr>
                        <td align="center" class="TableHeader">
                            Agregar boleta rápida
                            <br />
                            al productor:</td>
                        <td >
                         <table >
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
                                                                        :<asp:SqlDataSource ID="sdsCicloQuickBoleta" runat="server" 
                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                            
																			SelectCommand="SELECT cicloID, CicloName FROM Ciclos WHERE (cerrado = @cerrado) ORDER BY fechaInicio DESC">
                                                                        	<SelectParameters>
																				<asp:Parameter DefaultValue="FALSE" Name="cerrado" />
																			</SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TablaField">
                                                                        Producto:</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlNewQuickBoletaProducto" runat="server" 
                                                                            DataSourceID="sdsNewQuickBoletaProductos" DataTextField="Nombre" 
                                                                            DataValueField="productoID" Height="22px" Width="171px">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="sdsNewQuickBoletaProductos" runat="server" 
                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                            
                                                                            SelectCommand="SELECT [productoID], [Nombre] FROM [Productos] ORDER BY [Nombre]">
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
                        <td align="left">
                            &nbsp;</td>
                        <td align="left">
                            BOLETAS RELACIONADAS A ANTICIPO</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3" valign="top">
                            <asp:CheckBox ID="chkNewBoleta" runat="server" Text="AGREGAR NUEVA BOLETA Y ASIGNAR A ANTICIPO" />
                            <div runat="Server" id="divNewBoleta">
                               <table>
                                        <tr>
                                         
                                            <td class="TableHeader" align="center" colspan="2">
                                                AGREGAR NUEVA BOLETA</td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                CICLO:</td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlCiclo" runat="server" DataSourceID="sdsCiclo" 
                                                    DataTextField="CicloName" DataValueField="cicloID" Height="23px" Width="199px">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsCiclo" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                    
													SelectCommand="SELECT cicloID, CicloName FROM Ciclos WHERE (cerrado = @cerrado) ORDER BY fechaInicio DESC">
                                                	<SelectParameters>
														<asp:Parameter DefaultValue="FALSE" Name="cerrado" />
													</SelectParameters>
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                          <tr>
                                              <td class="TablaField" align="right">
                                                  PRODUCTOR:</td>
                                              <td align="left">
                                              <br />
                                                  <asp:DropDownList ID="ddlNewBoletaProductor" runat="server" 
                                                      DataSourceID="sdsNewBoletaProductor" DataTextField="Productor" 
                                                      DataValueField="productorID" Height="23px" Width="203px">
                                                  </asp:DropDownList>
                                                  <cc1:ListSearchExtender ID="ddlNewBoletaProductor_ListSearchExtender" 
                                                      runat="server" Enabled="True" TargetControlID="ddlNewBoletaProductor" 
                                                      PromptText="Escriba para buscar">
                                                  </cc1:ListSearchExtender>
                                                  <asp:SqlDataSource ID="sdsNewBoletaProductor" runat="server" 
                                                      ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                      
                                                      SelectCommand="SELECT productorID, LTRIM(apaterno + ' ' + amaterno + ' ' + nombre) AS Productor FROM Productores ORDER BY Productor">
                                                  </asp:SqlDataSource>
                                              </td>
                                          </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                # BOLETO
                                                <br />
                                                DE BASCULA:</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtNewNumBoleta" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                # DE FOLIO:</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtNewTicket" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                PRODUCTO:</td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlNewBoletaProducto" runat="server" 
                                                    DataSourceID="sdsNewBoletaProductos" DataTextField="Nombre" 
                                                    DataValueField="productoID" Height="22px" Width="171px">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsNewBoletaProductos" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                    SelectCommand="SELECT [productoID], [Nombre] FROM [Productos] ORDER BY [Nombre]">
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
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
                                            <td class="TablaField" align="right">
                                                FECHA ENTRADA:</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtNewFechaEntrada" runat="server" ReadOnly="True"></asp:TextBox>
                                                <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtNewFechaEntrada" 
                                                    Separator="/" />
                                                    <br />
                                                <asp:CheckBox ID="chkChangeFechaSalidaNewBoleta" runat="server" Text="Fecha Salida es Diferente a la de Entrada" />
                                                    <div runat="Server" id="divFechaSalidaNewBoleta">
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
                                            <td align="left">
                                                <asp:TextBox ID="txtNewPesoEntrada" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                PESO TARA:</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtNewPesoSalida" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                PESO NETO:</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtPesoNetoNewBoleta" runat="server" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                HUMEDAD:</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtNewHumedad" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                IMPUREZAS:</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtNewImpurezas" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                PRECIO:</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtNewPrecio" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField" align="right">
                                                SECADO:</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtNewSecado" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="btnAgregarBol" runat="server" onclick="btnAgregarBol_Click" 
                                                    Text="Agregar y Asignar a Anticipo" CssClass="Button" 
                                                    CausesValidation="False" />
                                                    <br />
                                                     <asp:Panel ID="panelNewBoletaResult" runat="server" > 
                                                        <table>
                                                            <tr>
                                                                <td style="text-align: center">
                                                                           
                                                                           <asp:Image ID="imgNewPalomita" runat="server" ImageUrl="~/imagenes/palomita.jpg" 
                                                                               Visible="False" />
                                                                           <asp:Image ID="imgNewTache" runat="server" ImageUrl="~/imagenes/tache.jpg" 
                                                                               Visible="False" />
                                                                           <asp:Label ID="lblMensajeNewBoleta" runat="server" SkinID="lblMensajeTitle" 
                                                                               Text="RESULTADO AL AGREGAR BOLETA"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: center">
                                                                           <asp:Label ID="lblMsgResult" runat="server"  Text="Label" 
                                                                               SkinID="lblMensajeOperationresult"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                                
                                    </table>
                                    </div>
                                </td>
                        <td align="left" valign="top">
                            <asp:ListBox ID="listBoxAgregadas" runat="server" Height="120px" Width="142px">
                            </asp:ListBox><br />
                            <asp:Button ID="btnQuitarBoletadeAnticipo" runat="server" 
                                CausesValidation="False" onclick="btnQuitarBoletadeAnticipo_Click" 
                                Text="Quitar boleta de lista" />
                        </td>
                    </tr>
                </table>
            
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td>
            &nbsp;</td>
    </tr>
</table>
<table>
    <tr>
       <td>
            <asp:CheckBox ID="chbMostrarPnlAddTarjDiesel" runat="server" 
                CssClass="TableHeader" Text="Mostrar panel para agregar tarjeta diesel" />
                <div id="divdiesel" runat="Server">
           <asp:Panel ID="pnlAddTarjetaDiesel" runat="server">
               <table>
                 <tr>
                    <td class="TablaField">Folio:</td>
                    <td><asp:TextBox ID="txtFolio" runat="server"></asp:TextBox></td>
                    <td><asp:CompareValidator ID="valFolio" runat="server" 
                            ControlToValidate="txtFolio" 
                            ErrorMessage="Escriba un folio válido" Operator="DataTypeCheck" 
                            Type="Integer"></asp:CompareValidator></td>
                 </tr>
                 <tr>
                    <td class="TablaField">Monto:</td>
                    <td>
                        <asp:TextBox ID="txtMontoTarjetaDiesel" runat="server"></asp:TextBox>
                     </td>
                    <td>
                        <asp:CompareValidator ID="valMonto" runat="server" 
                            ControlToValidate="txtMontoTarjetaDiesel" 
                            ErrorMessage="Escriba una cantidad válida" Operator="DataTypeCheck" 
                            Type="Double"></asp:CompareValidator>
                     </td>
                 </tr>
                 <tr>
                    <td class="TablaField">Litros:</td>
                    <td>
                        <asp:TextBox ID="txtLitrosTarjetaDiesel" runat="server"></asp:TextBox>
                     </td>
                    <td>
                        <asp:CompareValidator ID="valLitrosTarjDiesel" runat="server" 
                            ControlToValidate="txtLitrosTarjetaDiesel" 
                            ErrorMessage="Escriba una cantidad válida" Operator="DataTypeCheck" 
                            Type="Double"></asp:CompareValidator>
                     </td>
                 </tr>
                 <tr>
                    <td colspan="2">
                        <asp:Button ID="btnAddTarjetaDiesel" runat="server" 
                            onclick="btnAddTarjetaDiesel_Click" Text="Agregar" Visible="False" />
                        <asp:Button ID="btnModTarjetaDiesel" runat="server" 
                            onclick="btnModTarjetaDiesel_Click" Text="Modificar" Visible="False" />
                     </td>
                    <td>
                        
                     </td>
                    <td></td>
                 </tr>
               </table>
           </asp:Panel>
       </div>
            <%--<cc1:CollapsiblePanelExtender ID="pnlAddTarjetaDiesel_CollapsiblePanelExtender" 
                runat="server" CollapseControlID="chbMostrarPnlAddTarjDiesel" Collapsed="True" 
                Enabled="True" ExpandControlID="chbMostrarPnlAddTarjDiesel" 
                TargetControlID="pnlAddTarjetaDiesel">
            </cc1:CollapsiblePanelExtender>--%>
       
       </td>
    </tr>
	<tr>
	    <td colspan="2"   align="center">
	
	        <asp:TextBox ID="txtidtomodify" runat="server" Visible="False" Width="1px"></asp:TextBox>
	
	        <asp:TextBox ID="txtmoverAlista" runat="server" Visible="False" Width="1px"></asp:TextBox>
	
	 <asp:Button ID="cmdAceptar" runat="server"
            Text="Aceptar" onclick="cmdAceptar_Click" />
            <asp:Button ID="btnModificar" runat="server"
            Text="Modificar" onclick="btnModificar_Click"  />
        <asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" 
            CausesValidation="False" onclick="cmdCancelar_Click" style="height: 26px" />
	    </td>
	</tr>
	
</table>
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
