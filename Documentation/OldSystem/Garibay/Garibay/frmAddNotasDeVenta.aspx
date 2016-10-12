<%@ Page Language="C#" Theme="skinrojo" MasterPageFile="~/MasterPage.Master" AutoEventWireup="True" CodeBehind="frmAddNotasDeVenta.aspx.cs" Inherits="Garibay.frmNotasDeVenta" Title="Nota de venta"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="/scripts/divFunctions.js"></script>
    <script type="text/javascript" src="/scripts/prototype.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table width="100%">
    <tr> 
        <td align="right">
            <asp:HyperLink ID="lnkNewNV" runat="server" NavigateUrl="~/frmAddNotasDeVenta.aspx" >Agregar Nueva Nota de Venta</asp:HyperLink>
        </td>
    </tr>
</table>
	<asp:Panel ID="MainPnlUpdate" runat="server">
    
      <table>
      <tr>
      <td class="TableHeader" colspan="3" align="center">
      NOTA DE VENTA
      
      </td>
      </tr>
       <tr>
        <td rowspan="7">
                    <asp:Image ID="Image2" runat="server" Height="92px" 
                        ImageUrl="~/imagenes/LogoIPROJALMedium.jpg" Width="165px" />
                </td>
        <td rowspan="7">
                    <asp:Label ID="lblremitente" runat="server" Font-Size="X-Large" 
                        Text="INTEGRADORA DE PRODUCTORES DE JALISCO"></asp:Label>
                         S.P.R. DE R.L.<br />
                        <asp:Label ID="lblOrigen" runat="server" 
                        Text="C.P. 46600 Ameca, Jalisco"></asp:Label>
                        <br />
                        <asp:Label ID="lblDomicilio" runat="server" 
                        Text="Av. Patria Oriente No. 10"></asp:Label>
                        <br />
                        <asp:Label ID="lblTelefono" runat="server" 
                        Text=" 01(375) 758 1199"></asp:Label>
                        
                                    <br />
                    
                    
                    R.F.C. IPJ-030814-JAA
                    </td>
        <td class="TableHeader" align= "center">
                    Nota No.
                </td>
       </tr>
          <tr>
              <td align="center" class="TableHeader">
                  &nbsp;</td>
          </tr>
       <tr>
         <td align="center">
                    <asp:Label ID="lblNumOrdenDeSalida" runat="server"></asp:Label>
                    <asp:TextBox ID="txtNotaIDToMod" runat="server" Visible="False" Width="15px"></asp:TextBox>
                </td>
            </tr>
       <tr><td class="TableHeader" align="center">FECHA</td></tr>
       <tr>
        <td align="center">
                    <asp:TextBox ID="txtFecha" runat="server" ReadOnly="True"></asp:TextBox>
                    <rjs:PopCalendar ID="PopCalendar3" runat="server" Separator="/" 
                        Control="txtFecha" />
                </td>
       </tr>
       <tr><td align="center" class="TableHeader">CICLO:</td></tr>
       <tr>
          <td align="center">
                    <asp:DropDownList ID="cmbCiclo" runat="server" DataSourceID="sdsCiclos" 
                        DataTextField="CicloName" DataValueField="cicloID" Height="23px" Width="181px">
                    </asp:DropDownList>
                    
                    <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
						SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos]  WHERE cerrado=0 ORDER BY [fechaInicio] DESC">
                    </asp:SqlDataSource>
          </td>
       </tr>
       <tr>
         <td colspan="3">










   	                        <asp:Panel ID="panelFolio" runat="Server" GroupingText="Folio" Visible="False">
   	                        <table>
   	                        <tr>
   	                        <td class="TablaField">
   	                        Folio:
                                   
   	                        </td>
   	                        <td>
   	                        <asp:TextBox ID="txtFolio" runat="server"></asp:TextBox>
   	                        </td>
   	                        <td>
                                   &nbsp;</td>
   	                        
   	                        </tr>
   	                        </table>
   	                        </asp:Panel>
   	                        
   	                             <asp:Panel ID="panelProductor" runat="Server" GroupingText="Productor">
                            <table>
                                <tr>
                                    <td class="TablaField">
                                        CLIENTE:</td>
                                    <td>
                                    <br />
                                        <asp:DropDownList ID="cmbNombre" runat="server" AutoPostBack="True" 
                                            DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="productorID" 
                                            Height="22px" 
                                            Width="380px" onselectedindexchanged="cmbNombre_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="cmbNombre_ListSearchExtender" runat="server" 
                                            Enabled="True" PromptText="" TargetControlID="cmbNombre">
                                        </cc1:ListSearchExtender>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                            
                                            SelectCommand="select productores.productorID, LTRIM(productores.apaterno + ' ' + productores.amaterno + ' ' + productores.nombre) as name from Productores order by name">
                                        </asp:SqlDataSource>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAgregarClienteRapido" runat="server" 
                                            Text="Agregar Nuevo Cliente" CausesValidation="False"/>
                                        <asp:Button ID="btnActulizarcmbClientes" runat="server" 
                                            Text="Actualizar Lista de Clientes" 
                                            onclick="btnActulizarcmbClientes_Click" CausesValidation="False"/>
                                    </td>
                                    </tr>
                                    
                                    <tr>
                                    
                                    <td class="TablaField">
                                        DESTINO(MUNICIPIO):</td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtDestino" runat="server" Width="421px" 
                                            Height="22px"></asp:TextBox>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td class="TablaField">
                                        Domicílio:</td>
                                        <td class="TablaField">
                                        Población</td>
                                        <td class="TablaField">
                                        Estado</td>
                                    
                                    </tr>
                                    
                                    <tr>
                                    
                                    <td>
                                        <asp:TextBox ID="txtDomicilio" runat="server" Width="180px" ReadOnly="True" 
                                          ></asp:TextBox>
                                    </td>
                                
                                    
                                    <td>
                                        <asp:TextBox ID="txtPoblacion" runat="server" Width="180px" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                    
                                        <asp:TextBox ID="txtEstado" runat="server" Width="180px" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TablaField">
                                        Teléfono:</td>
                                        <td class="TablaField">
                                        Célular:</td>
                                        <td class="TablaField">
                                        IFE:</td>
                                      </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtTelefono" runat="server" Width="180px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCelular" runat="server" Width="180px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtIFE" runat="server" Width="180px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td colspan="2" >
                                        </td>
                                        <td>
                                            
                                        </td>
                                        </tr>
                                        </table>
                        </asp:Panel>

<%-- panel de datos de transporte --%>
                        <asp:Panel ID="pnlDatosdeNV" runat="Server" GroupingText="Otros Datos">
                            <table>
                                <tr>
                                <td class="TablaField">
                                        # PERMISO:</td>
                                        <td class="TablaField">
                                        TRANSPORTISTA:</td>
                                        <td class="TablaField">
                                        NOMBRE DEL CHOFER:</td>
                                        <td class="TablaField">
   	                        Tipo de Calculo de Intereses :
   	                            <asp:SqlDataSource ID="SqlDataSource6" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="SELECT [tipoDeCalculoID], [tipoDeCalculo] FROM [TiposDeCalculoDeInteres]">
                                </asp:SqlDataSource>
   	                        </td>
   	                        
                                    
                                    </tr>
                                    
                                    <tr>
                                    
                                    <td>
                                        <asp:TextBox ID="txtPermiso" runat="server" Width="180px" 
                                          ></asp:TextBox>
                                    </td>
                       
                                    
                                    <td>
                                        <asp:TextBox ID="txtTransportista" runat="server" Width="180px"></asp:TextBox>
                                    </td>
                                    <td>
                                    
                                        <asp:TextBox ID="txtNombreChofer" runat="server" Width="180px"></asp:TextBox>
                                    </td>
                                    <td>
                                   <asp:DropDownList ID="ddltipoCalculoIntereses" runat="server" Width="200px" 
                                       DataSourceID="SqlDataSource6" DataTextField="tipoDeCalculo" 
                                       DataValueField="tipoDeCalculoID">
                                   </asp:DropDownList>
   	                        </td>
                                </tr>
                                <%--<tr>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvPermiso" runat="server" ErrorMessage="El Campo de Permiso es Necesario" ControlToValidate="txtPermiso"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        
                                    <asp:RequiredFieldValidator ID="rfvTransportista" runat="server" ErrorMessage="El Campo de Transportista es Necesario"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                                        </td>
                            
                                    
                                    </tr>
                                --%>
                                <tr>
                                    <td class="TablaField">
                                        TRACTOR CAMION:</td>
                                        <td class="TablaField">
                                        COLOR:</td>
                                        <td class="TablaField">
                                        PLACAS:</td>
                                        
   		 	   	<td class="TablaField">
                <asp:CheckBox ID="chkaCredito" runat="server" Text="Esta Nota de Venta es con Crédito"/>
   		 	   	
   		 	   	
                                        </td>
                                        
                                        
                                        
                                        
                                      </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtTractorCamion" runat="server" Width="180px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtColor" runat="server" Width="180px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPlacas" runat="server" Width="180px"></asp:TextBox>
                                            </td>
                                            <td>
                                            
   		 	   	<div id="divaCredito" runat="Server">
   		 	   	<table>
   		 	   	
   		 	   		<tr>
   		 	   		<td class="TablaField">
   		 	   		Crédito :
   		 	   		    <asp:SqlDataSource ID="SqlCreditos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            
                            
                            SelectCommand="SELECT  LTRIM(Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre) + ' -  ' + CAST(Creditos.creditoID AS Varchar) AS Credito, Creditos.creditoID FROM Creditos INNER JOIN Productores ON Creditos.productorID = Productores.productorID WHERE (Creditos.statusID = 1) order by Credito">
                        </asp:SqlDataSource>
   		 	   		</td>
   		 	   			<td>
   		 	   			<asp:DropDownList ID="ddlCredito" runat="server"  
                                DataSourceID="SqlCreditos" DataTextField="Credito" 
                                DataValueField="creditoID">
                           </asp:DropDownList>
   		 	   			</td>
   		 	   		</tr>
   		 	   	</table>
   		 	   	      
   		 	   	</div>
                                            </td>
                                        </tr>
                                        </table>
                        </asp:Panel>
                                                      
          </td>
       </tr>
       <tr>
        <td></td>
        <td>
            
            &nbsp;</td>
        <td>
         <asp:Panel ID="pnlAddNota" runat="server">
              <asp:Label ID="lblAddResult" runat="server" Text=""></asp:Label>
          
              &nbsp;<asp:CheckBox ID="chkboxFertilizante" runat="server" 
                  Text="NOTA DE FERTILIZANTE" />
&nbsp;<asp:Button ID="btnAgregarNotaV" runat="server" Text="Agregar Nota de Venta" 
                                                onclick="btnAgregarNotaV_Click" 
                  CausesValidation="False" />
         </asp:Panel> 
        </td>
       </tr>
      </table>
      <table> 
       <tr>
         <td align="center">
            <asp:Panel runat="Server" id="pnlCentral">
            <asp:UpdatePanel runat="Server" id="UpPnlCentral">
            <ContentTemplate>                
                    <table>
                    	<tr>
                    	    <td align="center">                    
                                    <asp:Panel runat="Server" id="pnlCentralData">
                                            <table >
                                                <tr>
                                                 <td align="center" class="TableHeader">
                                       <asp:CheckBox ID="chkAgregarBoletas" runat="server" Text="Mostrar Agregar Boletas a La Nota de Venta" Visible="false"/>
                                   <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" ExpandControlID="chkAgregarBoletas" TargetControlID="pnlBoletas"
                                                                            CollapseControlID="chkAgregarBoletas" Collapsed="True" 
                                                            Enabled="True">
                                   </cc1:CollapsiblePanelExtender>
                                                 </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <table>
                                                            <tr>
																<td align="center">
                                                                <asp:GridView ID="gvBoletas" runat="server" AutoGenerateColumns="False" 
                                                                    DataKeyNames="boletaID,NumeroBoleta,Ticket" DataSourceID="sdsBoletasNV" 
                                                                    onrowdatabound="gvBoletas_RowDataBound" 
                                                                    onrowdeleting="gvBoletas_RowDeleting">
                                                                    <Columns>
                                                                        <asp:CommandField ButtonType="Button" CausesValidation="False" 
                                                                            DeleteText="Eliminar" ShowCancelButton="False" ShowDeleteButton="True" />
                                                                        <asp:BoundField DataField="boletaID" HeaderText="boletaID" 
                                                                            InsertVisible="False" ReadOnly="True" SortExpression="boletaID" 
                                                                            Visible="False" />
                                                                        <asp:BoundField DataField="NumeroBoleta" HeaderText="NumeroBoleta" 
                                                                            SortExpression="NumeroBoleta">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Ticket" HeaderText="Ticket" SortExpression="Ticket">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Producto" HeaderText="Producto" 
                                                                            ItemStyle-HorizontalAlign="Right" SortExpression="Producto">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="NombreProductor" HeaderText="NombreProductor" 
                                                                            ItemStyle-HorizontalAlign="Right" SortExpression="NombreProductor">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="FechaSalida" DataFormatString="{0:dd/MM/yyyy}" 
                                                                            HeaderText="FechaSalida" SortExpression="FechaSalida" />
                                                                        <asp:BoundField DataField="pesonetosalida" DataFormatString="{0:n}" 
                                                                            HeaderText="pesonetosalida" SortExpression="pesonetosalida">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="totalapagar" DataFormatString="{0:C2}" 
                                                                            HeaderText="totalapagar" SortExpression="totalapagar">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="bodega" HeaderText="bodega" 
                                                                            SortExpression="bodega" />
                                                                        <asp:BoundField DataField="precioapagar" DataFormatString="{0:C2}" 
                                                                            HeaderText="precioapagar" SortExpression="precioapagar" Visible="False">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="FolioDestino" HeaderText="FolioDestino" 
                                                                            SortExpression="FolioDestino" />
                                                                        <asp:BoundField DataField="PesoDestino" DataFormatString="{0:n}" 
                                                                            HeaderText="PesoDestino" SortExpression="PesoDestino">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Merma" DataFormatString="{0:n}" HeaderText="Merma" 
                                                                            SortExpression="Merma">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Flete" DataFormatString="{0:n}" HeaderText="Flete" 
                                                                            SortExpression="Flete">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="ImporteFlete" DataFormatString="{0:C2}" 
                                                                            HeaderText="ImporteFlete" SortExpression="ImporteFlete">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="PrecioNetoDestino" DataFormatString="{0:C2}" 
                                                                            HeaderText="PrecioNetoDestino" SortExpression="PrecioNetoDestino">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Editar">
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink ID="lnkEditar" runat="server">Editar</asp:HyperLink>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <asp:SqlDataSource ID="sdsBoletasNV" runat="server" 
                                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                    SelectCommand="SELECT Boletas.boletaID, Boletas.cicloID, Boletas.userID, Boletas.productorID, Boletas.productoID, Boletas.NumeroBoleta, Boletas.Ticket, Boletas.NombreProductor, Boletas.FechaSalida, Boletas.pesonetosalida, Boletas.importe, Boletas.totalapagar, Productos.Nombre AS Producto, Bodegas.bodega, Boletas.precioapagar, Boletas.FolioDestino, Boletas.PesoDestino, Boletas.Merma, Boletas.Flete, Boletas.ImporteFlete, Boletas.PrecioNetoDestino, NotasdeVenta_Boletas.notadeventaID FROM Boletas INNER JOIN Productos ON Boletas.productoID = Productos.productoID INNER JOIN Bodegas ON Boletas.bodegaID = Bodegas.bodegaID INNER JOIN NotasdeVenta_Boletas ON Boletas.boletaID = NotasdeVenta_Boletas.boletaID WHERE (NotasdeVenta_Boletas.notadeventaID = @notadeventaID)">
                                                                    <SelectParameters>
                                                                        <asp:ControlParameter ControlID="txtNotaIDToMod" DefaultValue="-1" 
                                                                            Name="notadeventaID" PropertyName="Text" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Panel ID="pnlBoletas" runat="server">
                                       <asp:Button ID="btnAgregarBoleta" runat="server" Text="Agregar Boleta a la Nota" CausesValidation="False" 
                                                                            Width="214px" />
                                       <asp:Button ID="btnActualizarListaBoletas" runat="server" Text="Actualizar Lista de Boletas" 
                                                                            onclick="btnActualizarListaBoletas_Click" CausesValidation="False" />
                                   </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td align="center" class="TableHeader">
                                               <asp:CheckBox ID="chkMostrarAgregarProductos" runat="server" 
                                    Text="Mostrar Agregar Productos a La Nota de Venta" 
                                    oncheckedchanged="chkMostrarAgregarProductos_CheckedChanged"/>
                                                </td></tr>
                                                <tr>
                                                <td>


                                                        <cc1:CollapsiblePanelExtender ID="pnlAddProd_CollapsiblePanelExtender" 
                                                            runat="server" CollapseControlID="chkMostrarAgregarProductos" Collapsed="True" 
                                                            Enabled="True" ExpandControlID="chkMostrarAgregarProductos" TargetControlID="pnladdproducto">
                                                        </cc1:CollapsiblePanelExtender>


   <asp:Panel ID="pnladdproducto" runat="server">
                                       
   	                    		
   	                    		<table>
   	                    		<tr>
   	                    		<td class="TableHeader" colspan="2">
   	                    		    Agregar Nuevo Producto
   	                    		
   	                    		</td>
   	                    		</tr>
   	                    		<tr>
   	                    		<td colspan="4">
                                       <asp:UpdateProgress ID="ProgressProductos" runat="server" 
                                           AssociatedUpdatePanelID="UpPnlCentral" DisplayAfter="0">
                                            <ProgressTemplate>
                                                <img alt="" src="imagenes/cargando.gif" />PROCESANDO DATOS...
                                            </ProgressTemplate>
                                       </asp:UpdateProgress>
   	                    		</td>
   	                    		
   	                    		<td>
   	                    		    &nbsp;</td>
   	                    		<td>
   	                    		       &nbsp;</td>
   	                    		
   	                    		
   	                    		<td>&nbsp;</td>
   		          
   		          
   		          <td>
                      &nbsp;</td>
   	                    		
   	                    	</tr>	
   	                    		    <tr>
                                        <td class="TablaField">
                                            Grupo:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlGrupos" runat="server" AutoPostBack="True" 
                                                DataSourceID="sqlGrupos" DataTextField="grupo" DataValueField="grupoID">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="TablaField">
                                            Producto :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlProductos" runat="server" DataSourceID="sqlProductos" 
                                                DataTextField="Nombre" DataValueField="productoID" Width="150px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="TablaField">
                                            Bodega :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlBodega" runat="server" DataSourceID="SqlBodegas" 
                                                DataTextField="bodega" DataValueField="bodegaID" Width="150px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="TablaField">
                                            FECHA:</td>
                                        <td>
                                            <asp:TextBox ID="txtFechaproducto" runat="server" ReadOnly="True"></asp:TextBox>
                                            <rjs:PopCalendar ID="PopCalendar5" runat="server" Control="txtFechaproducto" 
                                                Separator="/" />
                                        </td>
                                    </tr>
   	                    		<tr>
   	                    		<td class="TablaField">
   	                    		    Cantidad:
   	                    		</td>
   	                    		<td>
                                 <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>  
                                  <asp:TextBox ID="txtSacos" runat="server" Visible="false"></asp:TextBox>  
   	                    		</td>
   	                    		
   	                    		<td class="TablaField">
   	                    		    Precio :
   	                    		</td>
   	                    		<td>
                                 <asp:TextBox ID="txtPrecio" runat="server"></asp:TextBox>  
   	                    		</td>
   	                    		
   	                    		<%--<td class="TablaField">
   	                    		    Importe :
   	                    		</td>
   	                    		--%>
   	                    		<td>
                                 <asp:TextBox ID="txtImporte" runat="server" ReadOnly="True" Visible="false"></asp:TextBox>  
   	                    		</td>
   	                    		</tr>
   	                    		<tr>
   	                    		<td class="TablaField">
   	                    		    Existencia :
   	                    		</td>
   	                    		<td><asp:TextBox ID="txtExistencia" runat="server" ReadOnly="True"></asp:TextBox>
                                       
   	                    		</td>
   	                    		<td>
                                       
   	                    		</td>
   	                    		</tr>
   	                    		<tr>
   	                    		<td colspan="8" align="center">
                                       <asp:SqlDataSource ID="sqlGrupos" runat="server" 
                                           ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                           SelectCommand="SELECT [grupoID], [grupo] FROM [productoGrupos]">
                                       </asp:SqlDataSource>
                                       <asp:SqlDataSource ID="sqlProductos" runat="server" 
                                           ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                           
										   
                                           SelectCommand="SELECT Productos.productoID, Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Nombre FROM Productos INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID Where Productos.productoGrupoID = @grupoID ORDER BY Nombre ">
                                           <SelectParameters>
                                               <asp:ControlParameter ControlID="ddlGrupos" DefaultValue="-1" Name="grupoID" 
                                                   PropertyName="SelectedValue" />
                                           </SelectParameters>
                                       </asp:SqlDataSource>
                                       <asp:SqlDataSource ID="SqlBodegas" runat="server" 
                                           ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                           SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas]">
                                       </asp:SqlDataSource>
   	                    		
   	                    		       <asp:Button ID="btnAgregarProducto" runat="server" Text="Agregar Producto a la Nota de Venta" 
                                           onclick="btnAgregarProducto_Click" style="height: 26px" 
                                           CausesValidation="False" />
   	                    		
   	                    		</td>
   	                    		</tr>
   	                    		
   	                    		</table>
                                   
                                 </asp:Panel>
   	                    






                                                      
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <table width="100%" align="left">
                                                        	<tr>
                                                        		<td align="left">
                                                                    	<asp:GridView ID="grdvProNotasVenta" runat="server" AutoGenerateColumns="False" 
                                        
                                        
                                       
                                                                onrowdeleting="grdvProNotasVenta_RowDeleting" 
                                                                onrowediting="grdvProNotasVenta_RowEditing" 
                                                                onrowcancelingedit="grdvProNotasVenta_RowCancelingEdit" 
                                                                onrowupdating="grdvProNotasVenta_RowUpdating" DataKeyNames="productoID" 
																			                        onrowdatabound="grdvProNotasVenta_RowDataBound" onrowdeleted="grdvProNotasVenta_RowDeleted" 
																			onrowupdated="grdvProNotasVenta_RowUpdated">
                                                            <Columns>
                                                                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" 
                                                                    ShowEditButton="True" CausesValidation="True" />
                                                                <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
											                        HeaderText="Fecha" SortExpression="Fecha" ReadOnly="true"/>
                                                                <asp:TemplateField HeaderText="Producto" SortExpression="Producto">
											                        <EditItemTemplate>
												                        <asp:DropDownList ID="ddlProdEdit" runat="server" DataSourceID="sdsProdEdit" 
													                        DataTextField="Nombre" DataValueField="productoID">
												                        </asp:DropDownList>
												                        <asp:SqlDataSource ID="sdsProdEdit" runat="server" 
													                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
													                        SelectCommand="SELECT [productoID], [Nombre] FROM [Productos] ORDER BY [Nombre]">
												                        </asp:SqlDataSource>
											                        </EditItemTemplate>
											                        <ItemTemplate>
												                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
											                        </ItemTemplate>
										                        </asp:TemplateField>
										                        <asp:TemplateField HeaderText="Presentación">
											                        <ItemTemplate>
												                        <asp:Label ID="lblPresentacionGrid" runat="server" Text="Label"></asp:Label>
											                        </ItemTemplate>
										                        </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cantidad" SortExpression="cantidad">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtCantidadEdit" runat="server" Text='<%# Bind("cantidad") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("cantidad", "{0:N2}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Toneladas" SortExpression="peso">
											                        <EditItemTemplate>
												                        <asp:TextBox ID="txtSacosEdit" runat="server" Text='<%# Bind("peso") %>' 
                                                                            ReadOnly="True"></asp:TextBox>
											                        </EditItemTemplate>
											                        <ItemTemplate>
												                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("peso","{0:N2}") %>'></asp:Label>
											                        </ItemTemplate>
											                        <ItemStyle HorizontalAlign="Right" />
										                        </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Precio" SortExpression="precio">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtPrecioEdit" runat="server" Text='<%# Bind("precio", "{0:C2}") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("precio", "{0:C2}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="productoID" HeaderText="ProductoID" 
                                                                    Visible="False" />
                                                                <asp:BoundField DataField="importe" HeaderText="Importe" 
											                        SortExpression="importe" DataFormatString="{0:c2}" />
                                                                <asp:TemplateField HeaderText="Tiene Boletas?" SortExpression="tieneBoletas">
                                                                    <EditItemTemplate>
                                                                        <asp:CheckBox ID="chkboxSalidaPro" runat="server" />
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkTieneBoletas" runat="server" />                                         
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                           
                                                           </asp:GridView>
                                  
 																<asp:Label ID="Label8" runat="server" Font-Size="Small"></asp:Label>
                                                                </td>
                                                        		<td align="right" valign="bottom">


																		<table>
                                                                        <tr>
                                                                            <td align="right" class="TablaField">
                                                                                SubTotal:</td>
                                                                            <td align="right">
                                                                                <asp:Label ID="lblSubtotal" runat="server" Text="Label"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" class="TablaField">
                                                                                <asp:CheckBox ID="chbIVA" runat="server" 
                                                                                    CssClass="TablaField" Text="IVA:" 
                                                                                    oncheckedchanged="chbIVA_CheckedChanged" AutoPostBack="True" />
                                                                            </td>
                                                                            <td align="right">
                                                                                <asp:Label ID="lblIva" runat="server" Text="Label"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                        <td class="TablaField">
                                                                        PAGOS
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblPagos" runat="server" Text="0"></asp:Label>
                                                                        </td>
                                                                        
                                                                        </tr>
                                                                        
                                                                        <tr>
                                                                            <td align="right" class="TablaField">
                                                                                TOTAL:</td>
                                                                            <td align="right">
                                                                                <asp:Label ID="lblTotal" runat="server" Text="Label"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" class="TablaField">
                                                                                &nbsp;</td>
                                                                            <td align="right">
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                            <tr>
                                                                                <td align="right" class="TablaField">
                                                                                    &nbsp;</td>
                                                                                <td align="right">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                    </table>
                                                                </td>
                                                        	</tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                    
                                                       <asp:GridView ID="grvPagos" runat="server" AutoGenerateColumns="False" 
													       DataKeyNames="movbanID,movimientoID,tarjetaDieselID,PagoNotaVentaID,boletaID,chequesRecibidoID" DataSourceID="SqlPagos" 
													       onrowdatabound="grvPagos_RowDataBound" ShowFooter="True" onrowdeleting="grvPagos_RowDeleting" 
															onrowdeleted="grvPagos_RowDeleted">
    												   
												   	    <Columns>
														    <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
														    <asp:BoundField DataField="fecha" HeaderText="fecha" SortExpression="fecha" 
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
														    <asp:ControlParameter ControlID="lblNumOrdenDeSalida" DefaultValue="-1" 
															    Name="notadeventaID" PropertyName="Text" />
													    </SelectParameters>
												       </asp:SqlDataSource>
                                                    
                                                    
                                                    </td>
                                                </tr>
                                                <tr>
                                                                    <td>
                                                                        <asp:Panel ID="pnlNotaVentaResult" runat="server">
                                                                            <asp:Image ID="imgBien" runat="server" ImageUrl="~/imagenes/palomita.jpg" 
                                                                                Visible="False" />
                                                                            <asp:Image ID="imgMal" runat="server" ImageUrl="~/imagenes/tache.jpg" 
                                                                                Visible="False" />
                                                                            <asp:Label ID="lblNotaVentaResult" runat="server"></asp:Label>
                                                                        </asp:Panel>
                                                                    </td>
                                                            
                                                            </tr>
                                                <tr>
                                                    <td align="left">
                                                        <table>
                                                            <tr>
                                                                <td rowspan="2" align="left">
                                                                      <asp:Panel ID="panelObservaciones" runat="Server" GroupingText="Observaciones:">
   		 	   					
   		 	   		
   		 	   		 				<asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" 
                     							 Height="50px" Width="660px">
                      								</asp:TextBox>
   		 	 										</asp:Panel>
                                                                </td>
                                                                <td class="TablaField">Fecha de Pago:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtFechapago" runat="server"></asp:TextBox>
                                                                    <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtFechapago" Separator="/" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <table>
                                                        	<tr>
                                                        	    <td>
                                                        	        <p>
                                                                            NOTA: SI REALIZA ALGUNA MODIFICACION NO OLVIDE ACTUALIZAR LA NOTA
                                                                            </p>
                                                        	    </td>
                                                        	    <td>
                                                        	        <asp:Button ID="btnGuardaNotaVenta" runat="server" CausesValidation="False" 
                                                                        onclick="btnGuardaNotaVenta_Click" Text="Actualizar Datos de Nota" />
                                                                    <asp:Button ID="btnPrintNotaVenta" runat="server" CausesValidation="False" 
                                                                        Text="Imprimir Nota" UseSubmitBehavior="False" />
                                                                    <br />
                                                        	        <asp:HyperLink ID="lnkImprimePagare" runat="server" 
                                                                        NavigateUrl="~/frmAddNotasDeVenta.aspx" Visible="False">IMPRIMIR PAGARE</asp:HyperLink>
                                                        	    </td>
                                                        	</tr>
                                                        	
                                                        	</tr>
                                                       
                                                <tr>
                                                    <td rowspan="4">
                                                        <table>
                                                            <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chkAddNewPago" runat="server" 
                                                                    Text="MOSTRAR PANEL PARA AGREGAR PAGO." CssClass="TableHeader" />
                                                             <asp:Panel ID="pnlAgregarNuevoPago" runat="Server">
                                                                <table>
                                                                    <tr>
                                                                    <td>
                                                                                        
                                                                                        <table>
                                                                                        <tr>
                                                                                            <td class="TablaField">
                                                                                                Fecha:
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtFechaNPago" runat="server" ReadOnly="True"></asp:TextBox>
                                                                                                <rjs:PopCalendar ID="PopCalendar6" runat="server" Control="txtFechaNPago" 
                                                                                                    Separator="/" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:RequiredFieldValidator ID="valFecha0" runat="server" 
                                                                                                    ControlToValidate="txtFechaNPago" ErrorMessage="El campo fecha es necesario"></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="TablaField">
                                                                                                Tipo de pago:</td>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="cmbTipodeMovPago" runat="server" Height="22px" 
                                                                                                    Width="249px" 
                                                                                                    onselectedindexchanged="cmbTipodeMovPago_SelectedIndexChanged">
                                                                                                    <asp:ListItem Value="0">EFECTIVO</asp:ListItem>
                                                                                                    <asp:ListItem Value="1">CHEQUE</asp:ListItem>
                                                                                                    <asp:ListItem Value="2">TARJETA DIESEL</asp:ListItem>
                                                                                                    <asp:ListItem Value="3">BOLETA</asp:ListItem>
                                                                                                    <asp:ListItem Value="4">TRANSFERENCIA</asp:ListItem>
                                                                                                    <asp:ListItem Value="5">DEPOSITO</asp:ListItem>
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
                                                                                                <asp:TextBox ID="txtMonto" runat="server" Width="266px">1</asp:TextBox>
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
                                                                                        </table>
                                                                                        
														     <div ID="divDiesel" runat="Server">
                                                                                             
                                                                                                        <table>
                                                                                                            <tr>
                                                                                                                <td class="TablaField">
                                                                                                                    Folio:</td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtfoliodiesel" runat="server"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:CompareValidator ID="valFolio" runat="server" ControlToValidate="txtFolio" 
                                                                                                                        ErrorMessage="Escriba un folio válido" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                     
                                                                                                            <tr>
                                                                                                                <td class="TablaField">
                                                                                                                    Litros:</td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtLitrosTarjetaDiesel" runat="server" ></asp:TextBox>
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
                                                                                                                    &nbsp;</td>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                             
                                                                                                    
                                                                                                </div>
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
                                                                                                       <%-- sdsd--%>
                                                                                                       
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
                                                                                                        
                                                                                                        
                                                                                                        
                                                                                                    </table>
                                                                                                </div>
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
                                                                                                                                Banco:</td>
                                                                                                                            <td>
                                                                                                                                <asp:DropDownList ID="drpdlBancos" runat="server" 
                                                                                                                                    AutoPostBack="True" DataSourceID="sdsBanco" 
                                                                                                                                    DataTextField="nombre" DataValueField="bancoID" Height="23px" 
                                                                                                                                    Width="235px" 
                                                                                                                                    onselectedindexchanged="drpdlGrupoCatalogosInternaPago_SelectedIndexChanged">
                                                                                                                                </asp:DropDownList>
                                                                                                                                <asp:SqlDataSource ID="sdsBanco" runat="server" 
                                                                                                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                                                    
                                                                                                                                    SelectCommand="SELECT [bancoID], [nombre] FROM [Bancos]">
                                                                                                                                </asp:SqlDataSource>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                            
                                                                                                                    </table>
                                                                                                                </div>
											                 <div ID="divboletas" runat="Server">
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
                                                        
													    SelectCommand="SELECT Productos.productoID, Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Expr1 FROM Productos INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID ORDER BY Productos.Nombre">
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
                                                    <rjs:PopCalendar ID="PopCalendar1" runat="server"  Control="txtNewFechaEntrada" 
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
											                 </table>
    											             
                                                                                                   
                                                                                                </div>

                                                            </td>
                                                                    </tr>
                                                            <tr>
                                                                <td>
                                                                <asp:Panel ID="pnlNewPago" runat="server">
                                                                                                        <asp:Image ID="imgBienPago" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                                                                                                        <asp:Image ID="imgMalPago" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                                                                                                        <asp:Label ID="lblNewPagoResult" runat="server"></asp:Label>
                                                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="top">
                                                                    <asp:UpdatePanel ID="UpdateAddNewPago" runat="Server">
                                                                        <ContentTemplate>
                                                                          
                                                                                 <table>
                                                                                 
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:UpdateProgress ID="UpProgPagos" runat="server" 
                                                                                                AssociatedUpdatePanelID="UpdateAddNewPago" DisplayAfter="0">
                                                                                                <ProgressTemplate>
                                                                                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                                                                                                    Procesando informacion de pago...
                                                                                                </ProgressTemplate>
                                                                                            </asp:UpdateProgress>
                                                                                            <asp:Button ID="btnAddPago" runat="server" onclick="btnAddPago_Click" 
                                                                                                Text="Agregar Pago a la Nota" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                        
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                                </table>
                                                            </asp:Panel>
                                                                <cc1:CollapsiblePanelExtender ID="pnlAgregarNuevoPago_CollapsiblePanelExtender" 
                                                                    runat="server" CollapseControlID="chkAddNewPago" Collapsed="True" 
                                                                    Enabled="True" ExpandControlID="chkAddNewPago" 
                                                                    TargetControlID="pnlAgregarNuevoPago">
                                                                </cc1:CollapsiblePanelExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                             </asp:Panel>
                                                    </td>
                                                </tr>
                                     
                                                
                                                
                                                
                                            </table>   
                                    
                           
            </ContentTemplate>
            </asp:UpdatePanel>
            </asp:Panel>
         </td>
       </tr>
      </table>
    </asp:Panel>
</asp:Content>








