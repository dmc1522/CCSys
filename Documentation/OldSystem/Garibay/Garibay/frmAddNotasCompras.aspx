<%@  Page Language="C#" Theme="skinverde" MasterPageFile="~/MasterPage.Master" AutoEventWireup="True" CodeBehind="frmAddNotasCompras.aspx.cs" Inherits="Garibay.frmAddNotasCompras" Title="Nota de Compras" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="/scripts/divFunctions.js"></script>
    <script type="text/javascript" src="/scripts/prototype.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0">
        <tr>
            <td class="TablaField">NOTA:</td><td>LAS NOTAS DE COMPRA SON CONSIDERADAS COMO LAS "COMPRAS ANTICIPADAS"</td>
        </tr>
    </table>
    <asp:Panel ID="MainPnlUpdate" runat="server">
      <table>
       <tr>
        <td rowspan="5">
                    <asp:Image ID="Image2" runat="server" Height="92px" 
                        ImageUrl="~/imagenes/LogoIPROJALMedium.jpg" Width="165px" />
                </td>
        <td rowspan="5">
                    <asp:Label ID="Label2" runat="server" Font-Size="X-Large" 
                        Text="INTEGRADORA DE PRODUCTORES DE JALISCO"></asp:Label>
                    S.P.R. DE R.L.<br />
                    <br />
                    Av. Patria Oriente No. 10<br />
                    C.P. 46600 Ameca, Jalisco.<br />
                    R.F.C. IPJ-030814-JAA<br />
                    Tel. 01(375) 758 1199</td>
        <td class="TableHeader" align= "center">
                    Nota No.
                </td>
           <td align="center" class="TableHeader">
               <asp:TextBox ID="txtNumNota" runat="server" Height="22px"></asp:TextBox>
               <asp:TextBox ID="txtNotaIDToMod" runat="server" Visible="False" Width="15px"></asp:TextBox>
           </td>
       </tr>
       <tr><td class="TableHeader" align="center" colspan="2">FECHA</td></tr>
       <tr>
        <td align="center" colspan="2">
                    <asp:TextBox ID="txtFecha" runat="server" ReadOnly="True"></asp:TextBox>
                    <rjs:PopCalendar ID="PopCalendar3" runat="server" Separator="/" 
                        Control="txtFecha" />
                </td>
       </tr>
       <tr><td align="center" class="TableHeader" colspan="2">CICLO:</td></tr>
       <tr>
          <td align="center" colspan="2">
                    <asp:DropDownList ID="drpdlCiclo" runat="server" DataSourceID="sdsCiclos" 
                        DataTextField="CicloName" DataValueField="cicloID" Height="23px" Width="181px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsCiclos" runat="server" 
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
         <td colspan="4">
         
                    <asp:Panel ID="PnlProveedor" runat="Server">
                    
                        <table >
                            <tr>
                                <td align="center" class="TablaField" colspan="2">NOMBRE</td>
                                <td align="center" class="TablaField" colspan="3">DOMICILIO</td>
                            
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:DropDownList ID="drpdlProveedor" runat="server" Height="23px" Width="460px" 
                                        DataSourceID="sdsProveedor" DataTextField="Nombre" 
                                        DataValueField="proveedorID" AutoPostBack="True" 
                                        onselectedindexchanged="drpdlProveedor_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsProveedor" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        SelectCommand="SELECT [Nombre], [proveedorID] FROM [Proveedores] order by Nombre">
                                    </asp:SqlDataSource><br />
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtDomicilio" runat="server" Height="23px" Width="513px" 
                                        ReadOnly="True"></asp:TextBox>
                                </td>
     
                            </tr>
                            <tr>
                             <td align="center" class="TablaField" colspan="2">NOMBRE DE CONTACTO</td>
                             <td align="center" class="TablaField">MUNICIPIO</td>
                             <td align="center" class="TablaField" colspan="2">CP</td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                <asp:TextBox ID="txtNomContacto" runat="server" Height="23px" Width="513px" 
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtMunicipio" runat="server" ReadOnly="True" Width="274px"></asp:TextBox>
                                </td>
                                <td align="left" colspan="2">
                                    <asp:TextBox ID="txtCP" runat="server" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="TablaField">COMUNIDAD</td>
                                <td align="center" class="TablaField">&nbsp;</td>
                                <td align="center" class="TablaField">CELULAR</td>
                                <td align="center" class="TablaField" colspan="2">TELEFONO</td>
                            </tr>
                            <tr>
                             <td>
                                 <asp:TextBox ID="txtComunidad" runat="server" Height="22px" ReadOnly="True" 
                                     Width="500px"></asp:TextBox>
                                </td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 <asp:TextBox ID="txtCelular" runat="server" Height="23px" ReadOnly="True" 
                                     Width="222px"></asp:TextBox>
                                </td>
                             <td colspan="2">
                                 <asp:TextBox ID="txtTelefono" runat="server" Height="23px" ReadOnly="True" 
                                     Width="231px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td class="TablaField">
                                    MONEDA:</td>
                                <td>
                                    <asp:DropDownList ID="ddlTipoDeMoneda" runat="server" 
                                        DataSourceID="sdsTiposDeMoneda" DataTextField="Moneda" 
                                        DataValueField="tipomonedaID">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsTiposDeMoneda" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        SelectCommand="SELECT [tipomonedaID], [Moneda] FROM [TiposDeMoneda] ORDER BY [Moneda] DESC">
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                         </table>
                     </asp:Panel>
                                                      
          </td>
       </tr>
       <tr>
        <td></td>
        <td>
            
           </td>
        <td colspan="2">
         <asp:Panel ID="pnlAddNota" runat="server">
              <asp:Label ID="lblAddResult" runat="server" Text=""></asp:Label>
          
              <asp:Button ID="btnAgregarNewNotaCompra" runat="server" 
                  onclick="btnAgregarNewNotaCompra_Click" Text="Agregar Nueva Nota" 
                  CausesValidation="False" />
         </asp:Panel> 
        </td>
       </tr>
       <tr>
        <td></td>
        <td>
            
           </td>
        <td colspan="2"></td>
       </tr>
      </table>
      <table width="100%"> 
       <tr>
         <td align="center" colspan="3">
            <asp:UpdateProgress runat="Server" ID="progressCentral" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
                <ProgressTemplate>
                    <asp:Image ID="Image7" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                        Procesando Datos...
                </ProgressTemplate>
            </asp:UpdateProgress>
                <asp:Panel runat="Server" id="pnlCentral">
                    <table>
                    	<tr>
                    	    <td align="center">     
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                            <table >
                                                
                                                <tr>
                                                <td align="center" class="TableHeader">
                                                <asp:CheckBox ID="chkPnlAddProd" runat="server" 
                                                     Text="Mostrar panel para agregar producto" CssClass="TableField" />
                                                </td></tr>
                                                <tr>
                                                <td align="center">
                                                        <asp:Panel ID="pnlAddProd" runat="Server">
                                                            <table>
                                                                <tr>
                                                                    <td class="TablaField">Bodega:</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="drpdlBodega" runat="server" DataSourceID="sdsBodegas" 
                                                                            DataTextField="bodega" DataValueField="bodegaID" Height="24px" Width="179px">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="sdsBodegas" runat="server" 
                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                            SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                    <td class="TablaField">Grupo:</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlGrupos" runat="server" AutoPostBack="True" 
                                                                            DataSourceID="sqlGrupos" DataTextField="grupo" DataValueField="grupoID">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="sqlGrupos" runat="server" 
                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                            SelectCommand="SELECT [grupoID], [grupo] FROM [productoGrupos]">
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                    <td class="TablaField">
                                                                        Producto:</td>
                                                                    <td colspan="2">
                                                                        <asp:DropDownList ID="drpdlProducto" runat="server" DataSourceID="sqlProductos" 
                                                                            DataTextField="Nombre" DataValueField="productoID" Width="150px">
                                                                        </asp:DropDownList>
                                                                        <asp:SqlDataSource ID="sqlProductos" runat="server" 
                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                            SelectCommand="SELECT Productos.productoID, Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Nombre FROM Productos INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID Where Productos.productoGrupoID = @grupoID ORDER BY Nombre ">
                                                                            <SelectParameters>
                                                                                <asp:ControlParameter ControlID="ddlGrupos" DefaultValue="-1" Name="grupoID" 
                                                                                    PropertyName="SelectedValue" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="TablaField">Cantidad:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td class="TablaField">Precio:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPrecio" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td class="TablaField">
                                                                        Importe:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtImporte" runat="server" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnAddproduct" runat="server" CausesValidation="False" 
                                                                            onclick="btnAddproduct_Click" Text="Agregar Producto" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" colspan="7">
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <cc1:CollapsiblePanelExtender ID="pnlAddProd_CollapsiblePanelExtender" 
                                                            runat="server" CollapseControlID="chkPnlAddProd" Collapsed="True" 
                                                            Enabled="True" ExpandControlID="chkPnlAddProd" TargetControlID="pnlAddProd">
                                                        </cc1:CollapsiblePanelExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <table>
                                                        	<tr>
                                                        		<td>
                                                                    <asp:GridView ID="grdvProNotas" runat="server" AutoGenerateColumns="False" 
                                                                        onrowcancelingedit="grdvProNotas_RowCancelingEdit" 
                                                                        onrowdeleting="grdvProNotas_RowDeleting" DataSourceID="sdsDetallePRocs" 
                                                                        ondatabound="grdvProNotas_DataBound" DataKeyNames="NDCdetalleID" 
                                                                        onrowdeleted="grdvProNotas_RowDeleted" 
                                                                        onrowupdated="grdvProNotas_RowUpdated" 
                                                                        onrowupdating="grdvProNotas_RowUpdating1">
                                                                        <Columns>
                                                                            <asp:CommandField ButtonType="Button" CancelText="Cancelar" 
                                                                                CausesValidation="False" DeleteText="Eliminar" EditText="Editar" 
                                                                                ShowDeleteButton="True" ShowEditButton="True" />
                                                                            <asp:TemplateField HeaderText="Bodega" SortExpression="Bodega">
                                                                                <EditItemTemplate>
                                                                                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="sdsProdEdit" 
                                                                                        DataTextField="bodega" DataValueField="bodegaID" 
                                                                                        SelectedValue='<%# Bind("BodegaID") %>'>
                                                                                    </asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="sdsProdEdit" runat="server" 
                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                        SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
                                                                                    </asp:SqlDataSource>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Bodega") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Producto" SortExpression="Producto">
                                                                                <EditItemTemplate>
                                                                                    <asp:DropDownList ID="DropDownList2" runat="server" 
                                                                                        DataSourceID="sdsDetalleProdEdit" DataTextField="Nombre" 
                                                                                        DataValueField="productoID" SelectedValue='<%# Bind("ProductoID") %>'>
                                                                                    </asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="sdsDetalleProdEdit" runat="server" 
                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                        SelectCommand="SELECT [productoID], [Nombre] FROM [Productos] ORDER BY [Nombre]">
                                                                                    </asp:SqlDataSource>
                                                                                </EditItemTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Producto") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" 
                                                                                SortExpression="Cantidad">
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Preciodecompra" DataFormatString="{0:C}" 
                                                                                HeaderText="Preciodecompra" SortExpression="Preciodecompra">
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="importe" DataFormatString="{0:C}" 
                                                                                HeaderText="Importe" ReadOnly="True" SortExpression="importe">
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Sacos" HeaderText="Sacos" SortExpression="Sacos">
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="NDCdetalleID" HeaderText="NDCdetalleID" 
                                                                                SortExpression="NDCdetalleID" Visible="False" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <asp:SqlDataSource ID="sdsDetallePRocs" runat="server" 
                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                        DeleteCommand="gspNotasDeCompraDetalle_DELETE" 
                                                                        DeleteCommandType="StoredProcedure" onupdated="sdsDetallePRocs_Updated" 
                                                                        SelectCommand="SELECT NDCdetalleID, notadecompraID, productoID, bodegaID, cantidad, preciodecompra, sacos, Producto, bodega, importe FROM notasdecompraDetalle WHERE (notadecompraID = @notadecompraID) ORDER BY Producto" 
                                                                        UpdateCommand="gspNotasDeCompraDetalle_UPDATE" 
                                                                        UpdateCommandType="StoredProcedure">
                                                                        <SelectParameters>
                                                                            <asp:ControlParameter ControlID="txtNotaIDToMod" DefaultValue="-1" 
                                                                                Name="notadecompraID" PropertyName="Text" Type="Int32" />
                                                                        </SelectParameters>
                                                                        <DeleteParameters>
                                                                            <asp:Parameter Name="ndCdetalleID" Type="Int32" />
                                                                        </DeleteParameters>
                                                                        <UpdateParameters>
                                                                            <asp:Parameter Name="ndCdetalleID" Type="Int32" />
                                                                            <asp:Parameter Name="notadecompraID" Type="Int32" />
                                                                            <asp:Parameter Name="productoID" Type="Int32" />
                                                                            <asp:Parameter Name="bodegaID" Type="Int32" />
                                                                            <asp:Parameter Name="cantidad" Type="Double" />
                                                                            <asp:Parameter Name="preciodecompra" Type="Decimal" />
                                                                            <asp:Parameter Name="sacos" Type="Double" />
                                                                        </UpdateParameters>
                                                                    </asp:SqlDataSource>
                                                                    <asp:ObjectDataSource ID="odsDetalles" runat="server" DeleteMethod="Delete" 
                                                                        InsertMethod="Insert" SelectMethod="Search" TypeName="NotasDeCompraDetalle" 
                                                                        UpdateMethod="Update">
                                                                        <DeleteParameters>
                                                                            <asp:ControlParameter ControlID="grdvProNotas" Name="ndCdetalleID" 
                                                                                PropertyName="SelectedValue" Type="Int32" />
                                                                        </DeleteParameters>
                                                                        <UpdateParameters>
                                                                            <asp:Parameter Name="ndCdetalleID" Type="Int32" />
                                                                            <asp:Parameter Name="notadecompraID" Type="Int32" />
                                                                            <asp:Parameter Name="productoID" Type="Int32" />
                                                                            <asp:Parameter Name="bodegaID" Type="Int32" />
                                                                            <asp:Parameter Name="cantidad" Type="Double" />
                                                                            <asp:Parameter Name="preciodecompra" Type="Decimal" />
                                                                            <asp:Parameter Name="importe" Type="Decimal" />
                                                                            <asp:Parameter Name="sacos" Type="Double" />
                                                                        </UpdateParameters>
                                                                        <SelectParameters>
                                                                            <asp:ControlParameter ControlID="txtNotaIDToMod" Name="notadecompraID" 
                                                                                PropertyName="Text" Type="Int32" />
                                                                        </SelectParameters>
                                                                        <InsertParameters>
                                                                            <asp:Parameter Name="notadecompraID" Type="Int32" />
                                                                            <asp:Parameter Name="productoID" Type="Int32" />
                                                                            <asp:Parameter Name="bodegaID" Type="Int32" />
                                                                            <asp:Parameter Name="cantidad" Type="Double" />
                                                                            <asp:Parameter Name="preciodecompra" Type="Decimal" />
                                                                            <asp:Parameter Name="importe" Type="Decimal" />
                                                                            <asp:Parameter Name="sacos" Type="Double" />
                                                                        </InsertParameters>
                                                                    </asp:ObjectDataSource>
                                                                    <asp:Label ID="Label8" runat="server" Font-Size="Small"></asp:Label>
                                                                </td>
                                                        	    <td align="right" valign="bottom">
                                                                    <table>
                                                                        <tr>
                                                                            <td align="right" class="TablaField" colspan="2">
                                                                                <asp:DetailsView ID="dvTotales" runat="server" AutoGenerateRows="False" 
                                                                                    DataKeyNames="notadecompraID" DataSourceID="sdsNCTotales" Height="50px" 
                                                                                    Width="125px">
                                                                                    <Fields>
                                                                                        <asp:BoundField DataField="SubTotal" DataFormatString="{0:C2}" 
                                                                                            HeaderText="Subtotal" SortExpression="SubTotal">
                                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Pagos" DataFormatString="{0:C2}" HeaderText="Pagos" 
                                                                                            ReadOnly="True" SortExpression="Pagos">
                                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Total" DataFormatString="{0:C2}" HeaderText="Total" 
                                                                                            ReadOnly="True" SortExpression="Total">
                                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                                        </asp:BoundField>
                                                                                    </Fields>
                                                                                </asp:DetailsView>
                                                                                <asp:SqlDataSource ID="sdsNCTotales" runat="server" 
                                                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                    
                                                                                    SelectCommand="SELECT notadecompraID, SubTotal, Pagos, Total FROM NotasDeCompraTotales WHERE (notadecompraID = @notadecompraID) ORDER BY notadecompraID">
                                                                                    <SelectParameters>
                                                                                        <asp:ControlParameter ControlID="txtNotaIDToMod" Name="notadecompraID" 
                                                                                            PropertyName="Text" Type="Int32" />
                                                                                    </SelectParameters>
                                                                                </asp:SqlDataSource>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td align="right">
                                                                                <asp:Button ID="btnActualizaTotales" runat="server" CausesValidation="False" 
                                                                                    onclick="btnActualizaTotales_Click1" Text="Actualizar totales" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                        	</tr>
                                                            <tr>
                                                                <td align="right">
                                                                    &nbsp;</td>
                                                                <td align="right">
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr><td></td></tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <p>
                                                                        NOTA: SI REALIZA ALGUNA MODIFICACION NO OLVIDE ACTUALIZAR LA NOTA</p>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnlNotaCompraResult" runat="server">
                                                                        <asp:Image ID="imgBien" runat="server" ImageUrl="~/imagenes/palomita.jpg" 
                                                                            Visible="False" />
                                                                        <asp:Image ID="imgMal" runat="server" ImageUrl="~/imagenes/tache.jpg" 
                                                                            Visible="False" />
                                                                        <asp:Label ID="lblNotaCompraResult" runat="server" Font-Size="X-Small">NOTA DE COMPRA GUARDADA</asp:Label>
                                                                    </asp:Panel>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnGuardaNotaCompra" runat="server" 
                                                                        onclick="btnGuardaNotaCompra_Click" Text="Actualizar Datos de Nota" 
                                                                        CausesValidation="False" />
                                                                    <br />
                                                                    <asp:Button ID="btnPrintNotaCompra" runat="server" CausesValidation="False" 
                                                                        Text="Imprimir Nota" UseSubmitBehavior="False" Visible="False" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:GridView ID="grvPagos" runat="server" AutoGenerateColumns="False" 
                                                            DataKeyNames="pagosNotaCompraID" 
                                                            DataSourceID="SqlPagos" onrowdatabound="grvPagos_RowDataBound" 
                                                            onrowdeleted="grvPagos_RowDeleted" onrowdeleting="grvPagos_RowDeleting" 
                                                            ShowFooter="True" onrowcancelingedit="grvPagos_RowCancelingEdit" 
                                                            onrowediting="grvPagos_RowEditing" onrowupdated="grvPagos_RowUpdated" 
                                                            onrowupdating="grvPagos_RowUpdating">
                                                            <Columns>
                                                                <asp:CommandField ButtonType="Button" CancelText="Cancelar" 
                                                                    CausesValidation="False" EditText="Modificar" ShowEditButton="True" />
                                                                <asp:BoundField DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                                                    HeaderText="Fecha" SortExpression="Fecha" ReadOnly="True" />
                                                                <asp:BoundField DataField="FormaDePago" HeaderText="Forma De Pago" 
                                                                    SortExpression="FormaDePago" ReadOnly="True" >
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Moneda" HeaderText="Moneda" ReadOnly="True" 
                                                                    SortExpression="Moneda">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="numCheque" HeaderText="Cheque" 
                                                                    SortExpression="numCheque" ReadOnly="True" >
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Monto_Devolucion" HeaderText="Monto Devolucion" 
                                                                    SortExpression="Monto_Devolucion" DataFormatString="{0:C2}" 
                                                                    ReadOnly="True" >
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Pago_a_Proveedor" DataFormatString="{0:C2}" 
                                                                    HeaderText="Pago a Proveedor" ReadOnly="True" SortExpression="Pago_a_Proveedor">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="tasaDeCambio" DataFormatString="{0:N3}" 
                                                                    HeaderText="Tasa De Cambio" SortExpression="tasaDeCambio">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Abono_Aplicable" DataFormatString="{0:C2}" 
                                                                    HeaderText="Abono_Aplicable" ReadOnly="True" SortExpression="Abono_Aplicable">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:SqlDataSource ID="SqlPagos" runat="server" 
                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                            
                                                            
                                                            SelectCommand="SELECT Pagos_NotaCompra.pagosNotaCompraID, ISNULL(MovimientosCuentasBanco.fecha, MovimientosCaja.fecha) AS Fecha, ISNULL(ConceptosMovCuentas.Concepto, 'EFECTIVO') AS FormaDePago, MovimientosCuentasBanco.numCheque, ISNULL(MovimientosCuentasBanco.abono, MovimientosCaja.abono) AS Monto_Devolucion, ISNULL(MovimientosCuentasBanco.cargo, MovimientosCaja.cargo) AS Pago_a_Proveedor, Pagos_NotaCompra.tasaDeCambio, ISNULL(MovimientosCuentasBanco.cargo, MovimientosCaja.cargo) * Pagos_NotaCompra.tasaDeCambio AS Abono_Aplicable, ISNULL(TiposDeMoneda.Moneda, TiposDeMoneda_1.Moneda) AS Moneda FROM Pagos_NotaCompra LEFT OUTER JOIN ConceptosMovCuentas INNER JOIN MovimientosCuentasBanco ON ConceptosMovCuentas.ConceptoMovCuentaID = MovimientosCuentasBanco.ConceptoMovCuentaID INNER JOIN CuentasDeBanco ON MovimientosCuentasBanco.cuentaID = CuentasDeBanco.cuentaID INNER JOIN TiposDeMoneda ON CuentasDeBanco.tipomonedaID = TiposDeMoneda.tipomonedaID ON Pagos_NotaCompra.movbanID = MovimientosCuentasBanco.movbanID LEFT OUTER JOIN MovimientosCaja INNER JOIN TiposDeMoneda AS TiposDeMoneda_1 ON MovimientosCaja.tipomonedaID = TiposDeMoneda_1.tipomonedaID ON Pagos_NotaCompra.movimientoID = MovimientosCaja.movimientoID WHERE (Pagos_NotaCompra.notadecompraID = @notadecompraID)" 
                                                            UpdateCommand="UPDATE Pagos_NotaCompra SET tasaDeCambio = @tasaDeCambio WHERE (pagosNotaCompraID = @pagosNotaCompraID)">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="txtNotaIDToMod" DefaultValue="-1" 
                                                                    Name="notadecompraID" PropertyName="Text" />
                                                            </SelectParameters>
                                                            <UpdateParameters>
                                                                <asp:Parameter Name="tasaDeCambio" />
                                                                <asp:Parameter Name="pagosNotaCompraID" />
                                                            </UpdateParameters>
                                                        </asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <table>
                                                        	<tr>
                                                        		<td rowspan="2">
                                                                    <asp:Panel ID="panelObservaciones" runat="Server" GroupingText="Observaciones:">
                                                                        <table align="center">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtObservaciones" runat="server" Height="50px" 
                                                                                        TextMode="MultiLine" Width="550px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                                <td class="TablaField">
                                                                        Fecha de Pago:</td>
                                                        	</tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtFechapago" runat="server"></asp:TextBox>
                                                                    <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtFechapago" 
                                                                        Separator="/" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td align="center" class="TableHeader">
                                                        PAGOS RELACIONADOS A NOTA DE COMPRA
                                                        <br />
                                                        <asp:Button ID="btnOpenNewMovBan" runat="server" 
                                                            Text="Agregar Mov de Banco (pago)" UseSubmitBehavior="False" />
                                                        <asp:Button ID="btnOpenNewMovCaja" runat="server" 
                                                            Text="Agregar Mov de Caja chica (pago)" UseSubmitBehavior="False" />
                                                        <asp:Button ID="btnActualizaPagos" runat="server" 
                                                            onclick="btnActualizaPagos_Click" Text="Actualiza Lista de Pagos" />
                                                    </td>
                                                </tr>--%>
                                                <tr>
													<td rowspan="4">
													 <table>
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <asp:CheckBox ID="chkMostrarAgregarPago" runat="server" 
                                                                            Text="Mostrar Panel Para Agregar Nuevo Pago" CssClass="TableHeader" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                    <asp:UpdatePanel ID="UpdateAddNewPago" runat="Server"><ContentTemplate>
                                                                        <div runat="Server" id="divAgregarNuevoPago">
                                                                        <table>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <br />
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    Fecha:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtFechaNPago" runat="server" ReadOnly="True"></asp:TextBox>
                                                                                    <rjs:PopCalendar ID="PopCalendar6" runat="server"  Control="txtFechaNPago" Separator="/" 
                                                                                         />
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
                                                                                        Width="249px" 
                                                                                        onselectedindexchanged="cmbTipodeMovPago_SelectedIndexChanged">
                                                                                        
                                                                                        <asp:ListItem Value="1">MOVIMIENTO DE BANCO</asp:ListItem>
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
                                                                                    <div id="divPagoMovCaja" runat="Server">
                                                                                        <table>
                                                                                        	<tr>
                                                                                        		<td class="TablaField">El pago se hará de la caja:</td>
                                                                                        		<td>
                                                                                                    <asp:DropDownList ID="ddlPagosBodegas" runat="server" 
                                                                                                        DataSourceID="sdsPagosBodegas" DataTextField="bodega" DataValueField="bodegaID">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:SqlDataSource ID="sdsPagosBodegas" runat="server" 
                                                                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                                                        SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]"></asp:SqlDataSource>
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
                                                                                                    <asp:DropDownList ID="drpdlSubcatalogoCajaChica" runat="server" DataSourceID="sdsSubcatalogoCajaChica" 
                                                                                                        DataTextField="subCatalogo" DataValueField="subCatalogoMovBancoID" 
                                                                                                        Height="23px" Width="258px">
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
                                                                                                    <asp:DropDownList ID="drpdlSubcatalogofiscalPago" runat="server" DataSourceID="sdsSubcatalogofiscalPago" 
                                                                                                        DataTextField="subCatalogo" DataValueField="subCatalogoMovBancoID" 
                                                                                                        Height="23px" Width="258px">
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
                                                                                                                    <asp:TextBox ID="txtChequeNombre" runat="server" Width="282px"></asp:TextBox>
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
                                                                                                                    <asp:DropDownList ID="drpdlSubcatologointernaPago" runat="server" DataSourceID="sdsSubCatalogoInternaPago" 
                                                                                                                        DataTextField="subCatalogo" DataValueField="subCatalogoMovBancoID" 
                                                                                                                        Height="23px" Width="234px">
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
                                                                                    <asp:Panel ID="pnlNewPago" runat="server" >
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
                                                                                    <asp:Button ID="btnAddPago" runat="server" 
                                                                                        Text="Agregar Pago a la Nota" onclick="btnAddPago_Click" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        </div>
                                                                    </ContentTemplate></asp:UpdatePanel>
                                                                    </td>
                                                                </tr>
                                                            </table>
													</td>
                                                    <td align="center">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                <td>
                                                    <table>
                                                    	<tr>
                                                    		<td>
                                                                
                                                            </td>
                                                    		                                                    <td>
                                                                                                                    &nbsp;</td>

                                                    	</tr>
                                                    </table>
                                                </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>   
                                                       
                                                            </td>
                                                                                                            <td>
                                                                                                                &nbsp;</td>

                                                        </tr>
                                                    </table>
                                                </td>
                                                </tr>
                                                <%--<tr>
                                                    <td>
                                                        <asp:GridView ID="gvPagos" runat="server" AutoGenerateColumns="False" 
                                                            DataKeyNames="pagosNotaCompraID" DataSourceID="sdsPagosNotaCompra">
                                                            <Columns>
                                                                <asp:BoundField DataField="pagosNotaCompraID" HeaderText="#" 
                                                                    ReadOnly="True" SortExpression="pagosNotaCompraID" />
                                                                <asp:BoundField DataField="movimientoID" HeaderText="# Mov Caja" 
                                                                    SortExpression="movimientoID" />
                                                                <asp:BoundField DataField="movbanID" HeaderText="# Mov Banco" 
                                                                    SortExpression="movbanID" />
                                                                <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" 
                                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                                <asp:TemplateField HeaderText="Nombre" SortExpression="NombreBanco">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("NombreBanco") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("NombreBanco") %>'></asp:Label>
                                                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("NombreCaja") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Cuenta" HeaderText="Cuenta" ReadOnly="True" 
                                                                    SortExpression="Cuenta" />
                                                                <asp:BoundField DataField="BancoCargo" DataFormatString="{0:C2}" HeaderText="BancoCargo" 
                                                                    SortExpression="BancoCargo" >
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="BancoAbono" DataFormatString="{0:C2}" HeaderText="BancoAbono" 
                                                                    SortExpression="BancoAbono" >
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CajaCargo" DataFormatString="{0:C2}" HeaderText="CajaCargo" 
                                                                    SortExpression="CajaCargo" >
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CajaAbono" DataFormatString="{0:C2}" HeaderText="CajaAbono" 
                                                                    SortExpression="CajaAbono" >
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:SqlDataSource ID="sdsPagosNotaCompra" runat="server" 
                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                            
                                                            
                                                            SelectCommand="SELECT pagosNotaCompraID, notadecompraID, movimientoID, movbanID, fecha, NombreBanco, NombreCaja, Cuenta, BancoCargo, BancoAbono, CajaCargo, CajaAbono FROM PagosNotasDeCompra WHERE (notadecompraID = @notadecompraID) ORDER BY BancoCargo, BancoAbono, CajaCargo, CajaAbono">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="txtNotaIDToMod" DefaultValue="-1" 
                                                                    Name="notadecompraID" PropertyName="Text" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </td>
                                                </tr>--%>
                                            </table>   
                                </ContentTemplate>
                                </asp:UpdatePanel> 
                            </td>
                    	</tr>
                    </table>
                </asp:Panel>
         </td>
       </tr>
      </table>
    </asp:Panel>
</asp:Content>
