<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" Theme="skinrojo" AutoEventWireup="true" CodeBehind="frmListMovCajaChica.aspx.cs" Inherits="Garibay.WebForm2" Title="Movimientos de Caja Chica" %>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar.Net.2008" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>
    </asp:Content>

 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <td style="text-align: center">
                    <asp:Button ID="btnAceptarMensaje" runat="server" CssClass="Button" 
                        Text="Aceptar" />
                </td>
            </tr>
        </table>
</asp:Panel>
<asp:Panel ID="panelagregar" runat="server" > 

         
        <table>
            <tr>
                <td class="TableHeader">
                    MOVIMIENTOS DE CAJA CHICA</td>
            </tr>
            <tr>
                <td>
                <table >
                	<tr>
                		<td colspan="3" class="TableHeader">Filtros:</td>
                	</tr>
                	<tr>
                	<td class="TablaField">Bodega:</td> <td>
                        <asp:DropDownList ID="ddlBodegas" runat="server" DataSourceID="sdsBodegas" 
                            DataTextField="bodega" DataValueField="bodegaID">
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
                            Periodo:</td>
                        <td>
                            Fecha inicio:
                            <asp:TextBox ID="txtFecha1" runat="server" ReadOnly="True"></asp:TextBox>
                            &nbsp;<rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtFecha1" 
                                Separator="/" />
                        </td>
                        <td>
                            Fecha fin:&nbsp;
                            <asp:TextBox ID="txtFecha2" runat="server" ReadOnly="True"></asp:TextBox>
                            <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtFecha2" 
                                Separator="/" />
                        </td>
                    </tr>
                	<tr>
                        <td class="TablaField">
                            Mostrar:</td>
                        <td colspan="2">
                            <asp:CheckBoxList ID="cblColToShow" runat="server" RepeatDirection="Horizontal" >
                                <asp:ListItem>Observaciones</asp:ListItem>
                                <asp:ListItem>Usuario</asp:ListItem>
                                <asp:ListItem>Fecha de ingreso</asp:ListItem>
                                <asp:ListItem>Fecha de modificación</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                	<tr>
                	<td colspan="3">
                        <asp:Button ID="btnFiltrar" runat="server" 
                            Text="Filtrar" onclick="btnFiltrar_Click" />
                    <asp:Button ID="btnAgregarNuevo" runat="server" 
                        Text="Agregar" onclick="btnAgregarNuevo_Click" />
                    <asp:Button ID="btnModificar" runat="server" Text="Modificar" onclick="btnModificar_Click" 
                         />

        
        

                        <asp:Button ID="btnEliminar" runat="server" 
                            Text="Eliminar" onclick="btnEliminar_Click" />

        
        

                        <asp:Button ID="btnImprimir" runat="server" 
                            Text="Exportar a Excel" onclick="btnImprimir_Click" />

        
        

                        <asp:Button ID="btnActualizaStatus" runat="server" 
                            onclick="btnActualizaStatus_Click" Text="Actualizar estado cobrado" />

        
        

                        </td> 
                	</tr>
                	<tr>
                	<td colspan="3">
                        &nbsp;</td> 
                	</tr>
                </table>
                <table>
                 <asp:Panel ID= "panelsaldos" runat="Server">
                <table >
                	<tr>
                		<td colspan="1" class="TableHeader">Saldo inicial:</td>
                		<td style="text-align: center">
                            <asp:Label ID="lblSaldoinicial" runat="server" Text="Label" 
                                CssClass="TableField"></asp:Label>
                        &nbsp;
                        </td>
                		<td class="TableHeader">&nbsp;Saldo final</td>
                		<td class="TableField">
                            <asp:Label ID="lblSaldofinal" runat="server" Text="Label"></asp:Label>
                        </td>
                	</tr>
     
                	</tr>
                </table>
                </asp:Panel>
                
            <tr>
                <td>
                
                </td>
            </tr>
            <tr>
                <td>

        
        

    <asp:GridView ID="gridMovCajaChica" runat="server" AutoGenerateColumns="False" 
                        ForeColor="Black" GridLines="None" 
                 CellPadding="4" DataSourceID="SqlDataSource1" 
                       
                        PageSize="100" DataKeyNames="movimientoID,catalogoMovBancoInternoID,subCatalogoMovBancoInternoID" 
                        onselectedindexchanged="gridMovCajaChica_SelectedIndexChanged" 
                        onrowddatabound="gridMovCajaChica_RowDataBound" 
                        onprerender="gridMovCajaChica_PreRender" 
                        onrowediting="gridMovCajaChica_RowEditing" 
                        onrowcancelingedit="gridMovCajaChica_RowCancelingEdit" 
                        onrowupdating="gridMovCajaChica_RowUpdating" 
                        onrowdatabound="gridMovCajaChica_RowDataBound" 
                        onrowdeleting="gridMovCajaChica_RowDeleting" >
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <HeaderStyle CssClass="TableHeader" />
        <AlternatingRowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <Columns>
                    <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                        ShowSelectButton="True" CancelText="Cancelar" EditText="Modificar" 
                        ShowEditButton="True" UpdateText="Actualizar" />
                        <asp:TemplateField HeaderText="Eliminar" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" 
                                CommandName="Delete" Text="Eliminar"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="movimientoID" HeaderText="movimientoID" 
                        InsertVisible="False" SortExpression="movimientoID" Visible="False" />
                    <asp:TemplateField HeaderText="Fecha" SortExpression="fecha">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFecha" runat="server" Text='<%# Bind("fecha") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" 
                                Text='<%# Bind("fecha", "{0:dd/MM/yyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" 
                        SortExpression="nombre" />
                    <asp:TemplateField HeaderText="Catálogo de cuenta" InsertVisible="False">
                        <EditItemTemplate>
                            <asp:DropDownList ID="drpdlCatalogo" runat="server" DataSourceID="sdsCatalogo" 
                                DataTextField="catalogo" DataValueField="catalogoMovBancoID" Height="23px" 
                                Width="217px">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="sdsCatalogo" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                SelectCommand="SELECT catalogoMovBancoID, claveCatalogo + ' - ' + catalogoMovBanco AS catalogo FROM catalogoMovimientosBancos">
                            </asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" 
                                Text='<%# Bind("catalogoMovBancoInterno") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subcatálogo de cuenta">
                        <EditItemTemplate>
                            <asp:DropDownList ID="drpSubCatalogo" runat="server" 
                                DataSourceID="sdsSubCatalogo" DataTextField="Expr1" 
                                DataValueField="subCatalogoMovBancoID" Height="23px" 
                                ondatabound="drpSubCatalogo_DataBound" Width="206px">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="sdsSubCatalogo" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                SelectCommand="SELECT subCatalogoMovBancoID, subCatalogoClave + ' - ' + subCatalogo AS Expr1 FROM SubCatalogoMovimientoBanco">
                            </asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" 
                                Text='<%# Bind("subcatalogoMovBancoInterno") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="cargo" 
                        HeaderText="Cargo" DataFormatString="{0:c}" SortExpression="cargo" />
                    <asp:BoundField DataField="abono" DataFormatString="{0:c}" HeaderText="Abono" 
                        SortExpression="abono" />
                    <asp:BoundField HeaderText="Saldo" DataField="saldo" 
                        DataFormatString="{0:c}" 
                        SortExpression="cargo" ReadOnly="True" >
                    </asp:BoundField>
                    <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" 
                        SortExpression="Observaciones" ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="facturaOlarguillo" HeaderText="Factura o larguillo" 
                        SortExpression="facturaOlarguillo" />
                    <asp:BoundField DataField="numCabezas" DataFormatString="{0:n}" 
                        HeaderText="# Cabezas" SortExpression="numCabezas" />
                    <asp:TemplateField HeaderText="Cobrado">
                        <EditItemTemplate>
                            <asp:CheckBox ID="chkCobrado" runat="server" Checked='<%# Bind("cobrado") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkCobrado" runat="server" Checked = '<%# Bind("cobrado") %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Usuario" DataField="userID" SortExpression="userID" 
                        ReadOnly="True"></asp:BoundField>
                    <asp:BoundField DataField="storeTS" HeaderText="Fecha de ingreso" 
                        SortExpression="storeTS" ReadOnly="True" />
                    <asp:BoundField DataField="updateTS" HeaderText="Fecha de modificación" 
                        SortExpression="updateTS" ReadOnly="True" />
                    <asp:TemplateField HeaderText="Mov. Origen">
						<ItemTemplate>
							<asp:HyperLink ID="HyperLink1" runat="server">HyperLink</asp:HyperLink>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:BoundField DataField="subCatalogoMovBancoInternoID" HeaderText="subcatID" 
						Visible="False" />
					<asp:BoundField DataField="catalogoMovBancoInternoID" HeaderText="catID" 
						Visible="False" />
					<asp:TemplateField HeaderText="No. Liquidacion">
						<EditItemTemplate>
							<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
						</EditItemTemplate>
						<ItemTemplate>
							<asp:HyperLink ID="HyperLink2" runat="server">HyperLink</asp:HyperLink>
						</ItemTemplate>
						<FooterStyle HorizontalAlign="Center" />
						<ItemStyle HorizontalAlign="Center" />
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Boletas de Liq.">
						<EditItemTemplate>
							<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
						</EditItemTemplate>
						<ItemTemplate>
							<asp:Label ID="Label4" runat="server"></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Anticipo">
						<EditItemTemplate>
							<asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
						</EditItemTemplate>
						<ItemTemplate>
							<asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Boletas asingadas a anticipo" 
						InsertVisible="False">
						<EditItemTemplate>
							<asp:Label ID="Label1" runat="server"></asp:Label>
						</EditItemTemplate>
						<ItemTemplate>
							<asp:Label ID="Label5" runat="server"></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
                   
                    
                   
                </Columns>
    </asp:GridView>

                        
        

                </td>
            </tr>
            <tr>
                <td>

        
        

                    &nbsp;</td>
            </tr>
    </table>


        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
            
        
        
        
            
            
                        
                        
                        
                        SelectCommand="SELECT MovimientosCaja.movimientoID, MovimientosCaja.fecha, MovimientosCaja.nombre, MovimientosCaja.cargo, MovimientosCaja.abono, MovimientosCaja.Observaciones, MovimientosCaja.userID, MovimientosCaja.storeTS, MovimientosCaja.updateTS, MovimientosCaja.facturaOlarguillo, MovimientosCaja.numCabezas, catalogoMovimientosBancos.catalogoMovBanco, SubCatalogoMovimientoBanco.subCatalogo FROM MovimientosCaja INNER JOIN catalogoMovimientosBancos ON MovimientosCaja.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID INNER JOIN SubCatalogoMovimientoBanco ON catalogoMovimientosBancos.catalogoMovBancoID = SubCatalogoMovimientoBanco.catalogoMovBancoID ORDER BY MovimientosCaja.fecha, MovimientosCaja.abono, MovimientosCaja.cargo">
        </asp:SqlDataSource>
     </asp:Panel>
</asp:Content>
