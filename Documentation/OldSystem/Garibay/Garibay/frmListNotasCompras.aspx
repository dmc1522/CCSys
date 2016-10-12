<%@ Page Language="C#" Theme="skinrojo" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListNotasCompras.aspx.cs" Inherits="Garibay.WebForm9" Title="Lista de Notas de Compras" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
        
    
        
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

    <table >
	<tr align="center">
		<td class="TableHeader">
		NOTAS DE COMPRAS
		</td>
	</tr>
	<tr>
		<td>
		
		    <table>
                <tr>
                    <td class="TableHeader" colspan="3">
                        Filtros:</td>
                </tr>
                <tr>
                    <td class="TablaField">
                        CICLO:</td>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlCiclos" runat="server" DataSourceID="sdsCiclos" 
                            DataTextField="CicloName" DataValueField="cicloID">
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
                    <td colspan="2">
                        <asp:DropDownList ID="cmbProveedor" runat="server" 
                            DataSourceID="SqlDataSource2" DataTextField="Nombre" 
                            DataValueField="proveedorID" Height="23px" ondatabound="cmbProveedor_DataBound" 
                            Width="217px"> 
                          
                        </asp:DropDownList>
                        
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            SelectCommand="SELECT [proveedorID], [Nombre] FROM [Proveedores]">
                        </asp:SqlDataSource>
                        
                    </td>
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
                        <asp:CheckBoxList ID="cblColToShow" runat="server" RepeatColumns="4" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem>Subtotal</asp:ListItem>
                            <asp:ListItem>Iva</asp:ListItem>
                            <asp:ListItem># Usuario</asp:ListItem>
                            <asp:ListItem>Fecha de ingreso</asp:ListItem>
                            <asp:ListItem>Observaciones</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Button ID="btnFiltrar" runat="server" 
                            Text="Filtrar" onclick="btnFiltrar_Click" />
		
		    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="Button" 
                onclick="btnAgregar_Click" />
            
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="Button" 
                            onclick="btnEliminar_Click" />
         
		
            <asp:Button ID="btnVerNota" runat="server" Text="Ver Nota" CssClass="Button" 
                            onclick="btnVerNota_Click" />
         
		
		                <asp:Button ID="btnImprimir" runat="server" 
                            Text="Exportar a Excel" />
                    </td>
                </tr>
            </table>
    </td>
	</tr>
	<tr>
		<td style="text-align: center">
		
		    &nbsp;</td>
	</tr>
		</table>
	
		
		    <asp:GridView ID="grdvListEntPro" runat="server" 
                AllowSorting="True" AutoGenerateColumns="False" ForeColor="Black" 
                CellPadding="4" GridLines="None" DataSourceID="SqlDataSource1" 
        DataKeyNames="notadecompraID" 
        onselectedindexchanged="grdvListEntPro_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <HeaderStyle CssClass="TableHeader" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                
                <Columns>
                    <asp:CommandField ButtonType="Button" DeleteText="Eliminar" 
                        ShowDeleteButton="True" />
                    <asp:BoundField HeaderText="notadecompraID" DataField="notadecompraID" 
                        SortExpression="notadecompraID" InsertVisible="False" ReadOnly="True" 
                        Visible="False">

                    </asp:BoundField>
                   
                    <asp:BoundField HeaderText="Proveedor" DataField="Nombre" 
                        SortExpression="Nombre">
                    
                    </asp:BoundField>
                     <asp:TemplateField HeaderText="Folio" SortExpression="folio">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("folio") %>'></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label1" runat="server" Text='<%# Bind("folio") %>'></asp:Label>
                             &nbsp;<asp:HyperLink ID="lnkAbrir" runat="server" 
                                 NavigateUrl='<%# GetURLToOpenNC(Eval("notadecompraID").ToString()) %>' Text="ABRIR"></asp:HyperLink>
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Fecha" DataField="fecha" 
                        SortExpression="fecha" DataFormatString="{0: dd/MM/yyyy}">

                    </asp:BoundField>
                    
                    <asp:BoundField DataField="observaciones" HeaderText="Observaciones" 
                        SortExpression="observaciones" />
                    <asp:BoundField DataField="username" HeaderText="Usuario que agrego" 
                        SortExpression="username" />
                    
                </Columns>
  
            </asp:GridView>
		<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        
        
        
        
        
        SelectCommand="SELECT NotasDeCompra.notadecompraID, Proveedores.Nombre, NotasDeCompra.folio, NotasDeCompra.fecha, NotasDeCompra.observaciones, NotasDeCompra.userID, NotasDeCompra.storeTS, NotasDeCompra.proveedorID, Users.username, NotasDeCompra.cicloID FROM NotasDeCompra INNER JOIN Proveedores ON NotasDeCompra.proveedorID = Proveedores.proveedorID INNER JOIN Users ON NotasDeCompra.userID = Users.userID WHERE (NotasDeCompra.cicloID = @cicloID)" 
        DeleteCommand="gspNotasDeCompra_DELETE" 
        DeleteCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlCiclos" DefaultValue="-1" Name="cicloID" 
                    PropertyName="SelectedValue" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="notadecompraID" Type="Int32" />
            </DeleteParameters>
    </asp:SqlDataSource>
		</asp:Content>

