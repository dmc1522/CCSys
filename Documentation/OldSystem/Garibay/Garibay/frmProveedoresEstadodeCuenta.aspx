<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" Title = "Estado de cuenta a proveedores" AutoEventWireup="true" CodeBehind="frmProveedoresEstadodeCuenta.aspx.cs" Inherits="Garibay.frmProveedoresEstadodeCuenta" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

         
        &nbsp;&nbsp;&nbsp;

         
        <table>
            <tr>
                <td colspan="4" align="center" class="TableHeader">
                    ESTADO DE CUENTA DEL PROVEEDOR</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Al día de:</td>
                <td colspan="3">
                    <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
                    <rjs:PopCalendar ID="PopCalendar3" runat="server" Separator="/" 
                        Control="txtFecha" AutoPostBack="True" />
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    CICLO:</td>
                <td colspan="3">
   		 	   			<asp:DropDownList ID="ddlCiclos" runat="server" AutoPostBack="True" 
                            DataSourceID="sdsCiclos" DataTextField="CicloName" 
                            DataValueField="cicloID">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [fechaInicio] DESC">
                        </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Proveedor:</td>
                <td colspan="3">
   		 	   			<asp:DropDownList ID="ddlProveedores" runat="server"  
                                DataSourceID="sdsProveedor" DataTextField="Nombre" 
                                DataValueField="proveedorID" AutoPostBack="True" 
                            style="height: 22px" 
                            onselectedindexchanged="ddlProveedores_SelectedIndexChanged">
                           </asp:DropDownList>
   		 	   		    <asp:SqlDataSource ID="sdsProveedor" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            
                            SelectCommand="SELECT DISTINCT Orden_de_entrada.proveedorID, Proveedores.Nombre FROM Proveedores INNER JOIN Orden_de_entrada ON Proveedores.proveedorID = Orden_de_entrada.proveedorID WHERE (Orden_de_entrada.cicloID = @cicloId) ORDER BY Proveedores.Nombre">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlCiclos" Name="cicloId" 
                                    PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Dirección:</td>
                <td>
                    <asp:TextBox ID="txtDireccion" runat="server" Width="293px"></asp:TextBox>
                </td>
                <td class="TablaField">
                    Teléfono:</td>
                <td>
                    <asp:TextBox ID="txtTelefono" runat="server" Width="229px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Comunidad:</td>
                <td>
                    <asp:TextBox ID="txtComunidad" runat="server" Width="296px"></asp:TextBox>
                </td>
                <td class="TablaField">
                    Estado:</td>
                <td>
                    <asp:TextBox ID="txtEstado" runat="server" Width="228px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Municipio:</td>
                <td>
                    <asp:TextBox ID="textBoxMunicipio" runat="server" Width="294px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>

         
        <table>
            <tr>
                <td align="center" class="TableHeader">
                    ESTADO GENERAL</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:DetailsView ID="detailsViewTotales" runat="server" 
                        AutoGenerateRows="False" DataSourceID="sdsTotales" Height="50px" Width="125px">
                        <Fields>
                            <asp:BoundField DataField="cuantoMeEntrego" DataFormatString="{0:c2}" 
                                HeaderText="Monto entregado" SortExpression="cuantoMeEntrego">
                            <ItemStyle Font-Bold="True" Font-Size="X-Large" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cuantoLePague" DataFormatString="{0:c2}" 
                                HeaderText="Monto Pagado" SortExpression="cuantoLePague">
                            <ItemStyle Font-Bold="True" Font-Size="X-Large" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldoFinal" DataFormatString="{0:c2}" 
                                HeaderText="Saldo" SortExpression="saldoFinal">
                            <ItemStyle Font-Bold="True" Font-Size="X-Large" HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Fields>
                    </asp:DetailsView>
                    <asp:SqlDataSource ID="sdsTotales" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="ReturnEstadodeCuentaProveedoresTotales" 
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlProveedores" Name="proveedorID" 
                                PropertyName="SelectedValue" Type="Int32" />
                            <asp:ControlParameter ControlID="txtFecha" Name="fechafin" PropertyName="Text" 
                                Type="DateTime" />
                            <asp:ControlParameter ControlID="ddlCiclos" Name="cicloID" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            </table>
            


                    <table >
                        <tr>
                            <td align="center" class="TableHeader">
                                DETALLE DE PRODUCTOS COMPRADOS</td>
                        </tr>
                        <tr>
                            <td>
                                                                    	<asp:GridView ID="grdvProNotasVenta" 
                                    runat="server" AutoGenerateColumns="False" 
                                    DataSourceID="sdsEstadodeCuenta" ondatabound="grdvProNotasVenta_DataBound" DataKeyNames="ordendetalleID" 
                                                                            onrowediting="grdvProNotasVenta_RowEditing" 
                                                                            onrowupdating="grdvProNotasVenta_RowUpdating" 
                                                                            onrowdatabound="grdvProNotasVenta_RowDataBound">
                                                            <Columns>
                                                                <asp:CommandField ButtonType="Button" CausesValidation="False" 
                                                                    EditText="Modificar" ShowEditButton="True" />
                                                                <asp:BoundField DataField="fecharemision" HeaderText="Fecha remisión" 
                                                                    DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" />
                                                                <asp:BoundField DataField="remision" HeaderText="Nota/Remisión" 
                                                                    ReadOnly="True" >
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Factura">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("factura") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtNumFactura" runat="server" Text='<%# Bind("factura") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Fecha de factura">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" 
                                                                            Text='<%# Bind("fechafactura", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtFechaFactura" runat="server" 
                                                                            Text='<%# Bind("fechafactura") %>'></asp:TextBox>
                                                                        <rjs:PopCalendar ID="PopCalendar3" runat="server" AutoPostBack="False" 
                                                                            Control="txtFechaFactura" Separator="/" />
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="producto" HeaderText="Producto" ReadOnly="True" >
                                                                <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="casaagricola" HeaderText="Casa agrícola" 
                                                                    ReadOnly="True" >
                                                                <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="cantidad" HeaderText="No. Piezas" ReadOnly="True" >
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="descripcion" HeaderText="Descripción" 
                                                                    ReadOnly="True" >
                                                                <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="preciolista" HeaderText="Precio Lista" 
                                                                    DataFormatString="{0:c2}" ReadOnly="True" >
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Descuento">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("descuento", "{0:0%}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtDescuento" runat="server" Text='<%# Bind("descuento") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="preciounitario" DataFormatString="{0:C2}" 
                                                                    HeaderText="Precio Unitario" ReadOnly="True" >
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="importe" HeaderText="Importe" 
                                                                    DataFormatString="{0:c2}" ReadOnly="True" >
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Descuento del">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" 
                                                                            Text='<%# Bind("descuentodel", "{0:0%}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtDescuentoDel" runat="server" 
                                                                            Text='<%# Bind("descuentodel") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="total" DataFormatString="{0:c2}" 
                                                                    HeaderText="Total" ReadOnly="True" >
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="fechavencimiento" 
											                        HeaderText="Fecha vencimiento" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True"/>
                                                                <asp:BoundField DataField="monto" DataFormatString="{0:c2}" 
                                                                    HeaderText="Monto" ReadOnly="True" >
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="pendienteporpagar" DataFormatString="{0:c2}" 
                                                                    HeaderText="Pendiente por pagar" ReadOnly="True" >
                                                                <ItemStyle Font-Bold="True" Font-Size="X-Large" HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="fechadepago" DataFormatString="{0:dd/MM/yyyy}" 
                                                                    HeaderText="Fecha de pago" ReadOnly="True" />
                                                                <asp:BoundField DataField="numcuenta" HeaderText="No. cuenta" ReadOnly="True" />
                                                                <asp:BoundField DataField="nombrecuenta" HeaderText="Nombre de cuenta" 
                                                                    ReadOnly="True" >
                                                                <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="banco" HeaderText="Banco" ReadOnly="True" >
                                                                <ItemStyle Wrap="False" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="numcheque" HeaderText="No. cheque " ReadOnly="True" >
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ordendetalleID" 
                                                                    HeaderText="ordendetalleID" Visible="False" ReadOnly="True" />
                                                                <asp:BoundField DataField="fecha" HeaderText="Fecha" Visible="False" />
                                                            </Columns>
                                                           
                                                           </asp:GridView>
                                  
                                                                        <asp:SqlDataSource ID="sdsEstadodeCuenta" runat="server" 
                                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                                            SelectCommand="ReturnEstadodeCuentaProveedores" 
                                                                            SelectCommandType="StoredProcedure" UpdateCommand="select 1">
                                                                            <SelectParameters>
                                                                                <asp:ControlParameter ControlID="ddlProveedores" Name="proveedorID" 
                                                                                    PropertyName="SelectedValue" Type="Int32" />
                                                                                <asp:ControlParameter ControlID="txtFecha" Name="fechafin" PropertyName="Text" 
                                                                                    Type="DateTime" />
                                                                                <asp:ControlParameter ControlID="ddlCiclos" Name="cicloID" 
                                                                                    PropertyName="SelectedValue" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                  
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                            
                                                                        NOTAS A CREDITO</td>
                        </tr>
                        <tr>
                            <td align="left">
                            
                                <asp:GridView ID="gvNotasDeCredito" runat="server" AutoGenerateColumns="False" 
                                                    DataKeyNames="notadecreditoID" DataSourceID="sdsNotasDeCredito">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Button" CancelText="Cancelar" 
                                                            DeleteText="Eliminar" ShowDeleteButton="True" />
                                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" 
                                                            DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:BoundField DataField="monto" HeaderText="Monto" 
                                                            SortExpression="monto" DataFormatString="{0:C2}" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion" 
                                                            SortExpression="descripcion" />
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Label ID="lblNotaDeCreditoResult" runat="server" Text=""></asp:Label>
                                                <BR />
                                <asp:SqlDataSource ID="sdsNotasDeCredito" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                    DeleteCommand="DELETE FROM [Proveedores_NotaDeCredito] WHERE [notadecreditoID] = @notadecreditoID" 
                                                    InsertCommand="INSERT INTO [Proveedores_NotaDeCredito] ([proveedorID], [cicloID], [fecha], [monto], [descripcion]) VALUES (@proveedorID, @cicloID, @fecha, @monto, @descripcion)" 
                                                    SelectCommand="SELECT * FROM [Proveedores_NotaDeCredito] WHERE (([proveedorID] = @proveedorID) AND ([cicloID] = @cicloID)) ORDER BY [fecha]" 
                                                    
                                                                            UpdateCommand="UPDATE [Proveedores_NotaDeCredito] SET [proveedorID] = @proveedorID, [cicloID] = @cicloID, [fecha] = @fecha, [monto] = @monto, [descripcion] = @descripcion WHERE [notadecreditoID] = @notadecreditoID">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="ddlProveedores" Name="proveedorID" 
                                                            PropertyName="SelectedValue" Type="Int32" />
                                                        <asp:ControlParameter ControlID="ddlCiclos" Name="cicloID" 
                                                            PropertyName="SelectedValue" Type="Int32" />
                                                    </SelectParameters>
                                                    <DeleteParameters>
                                                        <asp:Parameter Name="notadecreditoID" Type="Int32" />
                                                    </DeleteParameters>
                                                    <UpdateParameters>
                                                        <asp:Parameter Name="proveedorID" Type="Int32" />
                                                        <asp:Parameter Name="cicloID" Type="Int32" />
                                                        <asp:Parameter Name="fecha" Type="DateTime" />
                                                        <asp:Parameter Name="monto" Type="Decimal" />
                                                        <asp:Parameter Name="descripcion" Type="String" />
                                                        <asp:Parameter Name="notadecreditoID" Type="Int32" />
                                                    </UpdateParameters>
                                                    <InsertParameters>
                                                        <asp:Parameter Name="proveedorID" Type="Int32" />
                                                        <asp:Parameter Name="cicloID" Type="Int32" />
                                                        <asp:Parameter Name="fecha" Type="DateTime" />
                                                        <asp:Parameter Name="monto" Type="Decimal" />
                                                        <asp:Parameter Name="descripcion" Type="String" />
                                                    </InsertParameters>
                                                </asp:SqlDataSource>
                                <asp:CheckBox ID="chkAddNotaCredito" runat="server" Text="Agregar Nota de Credito" />                          	
                                <asp:Panel runat="Server" id="pnlNewNotaCredito">
                                    <table>
                                    	<tr>
                                    		<td>
                                                <asp:DetailsView ID="dvNotaDeCredito" runat="server" Height="50px" 
                                                    Width="125px" AutoGenerateRows="False" DataKeyNames="notadecreditoID" 
                                                    DataSourceID="sdsNotasDeCredito" DefaultMode="Insert" 
                                                    oniteminserted="dvNotaDeCredito_ItemInserted" 
                                                    oniteminserting="dvNotaDeCredito_ItemInserting">
                                                    <Fields>
                                                        <asp:TemplateField HeaderText="Fecha:" SortExpression="fecha">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("fecha") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <InsertItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("fecha") %>'></asp:TextBox>
                                                                <rjs:PopCalendar ID="PopCalendar8" runat="server" Control="TextBox1" 
                                                                    Separator="/" />
                                                            </InsertItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("fecha") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="False" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="monto" HeaderText="Monto:" SortExpression="monto" />
                                                        <asp:TemplateField HeaderText="Descripcion:" SortExpression="descripcion">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("descripcion") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <InsertItemTemplate>
                                                                <asp:TextBox ID="TextBox2" runat="server" Height="86px" 
                                                                    Text='<%# Bind("descripcion") %>' TextMode="MultiLine" Width="221px"></asp:TextBox>
                                                            </InsertItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ButtonType="Button" CancelText="Cancelar" 
                                                            DeleteText="Eliminar" EditText="Editar" InsertText="Insertar" 
                                                            ShowCancelButton="False" ShowInsertButton="True" />
                                                    </Fields>
                                                </asp:DetailsView>
                                    		</td>
                                    	</tr>
                                    </table>
                                </asp:Panel>
                                                                        <cc1:CollapsiblePanelExtender ID="pnlNewNotaCredito_CollapsiblePanelExtender" 
                                                                            runat="server" CollapseControlID="chkAddNotaCredito" Collapsed="True" 
                                                                            Enabled="True" ExpandControlID="chkAddNotaCredito" 
                                                                            TargetControlID="pnlNewNotaCredito">
                                                                        </cc1:CollapsiblePanelExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                            
                                                                        PAGOS A PROVEEDOR</td>
                        </tr>
                        <tr>
                            <td align="left">
                            
                                                        <asp:GridView ID="grvPagos" 
                runat="server" AutoGenerateColumns="False" 
                                                            DataKeyNames="movbanID,movimientoID,pagosProveedoresID" 
                                                            DataSourceID="SqlPagos" onrowdatabound="grvPagos_RowDataBound" 
                                                            ShowFooter="True">
                                                            <Columns>
                                                                <asp:CommandField Visible="False" />
                                                                <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyy}" 
                                                                    HeaderText="Fecha" SortExpression="fecha" />
                                                                <asp:BoundField DataField="movbanID" HeaderText="movbanID" 
                                                                    SortExpression="movbanID" visible="false" />
                                                                <asp:BoundField DataField="movimientoID" HeaderText="movimientoID" 
                                                                    SortExpression="movimientoID" visible="false" />
                                                                <asp:TemplateField HeaderText="Forma de Pago">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No. Cheque">
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
                                                                <asp:BoundField DataField="pagosNotaCompraID" HeaderText="pagosNotaCompraID" 
                                                                    SortExpression="pagosNotaCompraID" Visible="False" />
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:SqlDataSource ID="SqlPagos" runat="server" 
                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                            
                                                            
                
                                                                            SelectCommand="SELECT fecha, movimientoID, movbanID, pagosProveedoresID FROM Pagos_Proveedores WHERE (proveedorID = @proveedorID) AND (cicloID = @cicloID)">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="ddlProveedores" DefaultValue="-1" 
                                                                    Name="proveedorID" PropertyName="SelectedValue" />
                                                                <asp:ControlParameter ControlID="ddlCiclos" DefaultValue="-1" Name="cicloID" 
                                                                    PropertyName="SelectedValue" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                                    	
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:CheckBox ID="chkMostrarAgregarPago" runat="server" 
                                    Text="Mostrar Panel Para Agregar Nuevo Pago" CssClass="TableHeader" />
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
                                                                                    Fecha:  <td>
                                                                                    <asp:TextBox ID="txtFechaNPago" runat="server" ReadOnly="True"></asp:TextBox>
                                                                                    <rjs:PopCalendar ID="PopCalendar7" runat="server"  Control="txtFechaNPago" 
                                                                                        Separator="/" 
                                                                                         />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RequiredFieldValidator ID="valFecha0" runat="server" 
                                                                                        ControlToValidate="txtFechaNPago" 
                                                                                        ErrorMessage="El campo fecha es necesario"></asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    Tipo de pago:</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="cmbTipodeMovPago" runat="server" Height="22px" 
                                                                                        Width="249px">
                                                                                        
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
                                                                                    Cargo:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCargo" runat="server" Width="266px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="TablaField">
                                                                                    Abono:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtAbono" runat="server" Width="266px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <br />
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
                                                                                                        DataSourceID="sdsPagosBodegas" DataTextField="bodega" 
                                                                                                        DataValueField="bodegaID">
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
                                                                                    <asp:Button ID="btnAddPagoaProveedor" runat="server" 
                                                                                        onclick="btnAddPagoaProveedor_Click" Text="Agregar pago" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        </div>
                                                                   	
                            </td>
                        </tr>
        </table>
      

</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    </asp:Content>

