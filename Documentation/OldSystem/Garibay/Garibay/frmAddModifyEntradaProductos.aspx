<%@ Page Language="C#" Theme="skinrojo" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmAddModifyEntradaProductos.aspx.cs" Inherits="Garibay.WebForm7" Title="Entrada de productos" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Button
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:Panel ID="panelmensaje" runat="server" > 
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
                
                    <asp:DetailsView ID="DetvEntradaProductos" runat="server" AutoGenerateRows="False" 
                        DataKeyNames="entradaprodID" DataSourceID="SqlDataSource5" Height="50px" 
                        Width="125px">
                        <Fields>
                            <asp:BoundField DataField="entradaprodID" HeaderText="ID" InsertVisible="False" 
                                ReadOnly="True" SortExpression="entradaprodID" />
                            <asp:BoundField DataField="Nombre" HeaderText="Producto" 
                                SortExpression="Nombre" />
                            <asp:BoundField DataField="bodega" HeaderText="Bodega" 
                                SortExpression="bodega" />
                            <asp:BoundField DataField="tipoMovimiento" HeaderText="Tipo de Movimiento" 
                                SortExpression="tipoMovimiento" />
                            <asp:BoundField DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha" 
                                SortExpression="Fecha" />
                            <asp:BoundField DataField="cantidad" HeaderText="Cantidad" 
                                SortExpression="cantidad" />
                            <asp:BoundField DataField="Precio" DataFormatString="{0:c}" HeaderText="Precio" 
                                SortExpression="Precio" />
                            <asp:BoundField DataField="observaciones" HeaderText="Observaciones" 
                                SortExpression="observaciones" />
                            <asp:BoundField DataField="Expr1" HeaderText="Usuario" SortExpression="Expr1" />
                            <asp:BoundField DataField="NDCdetalleID" HeaderText="Detalle de compra" 
                                SortExpression="NDCdetalleID" />
                            <asp:BoundField DataField="storeTS" DataFormatString="{0:dd/MM/yyyy mm:hh:ss}" 
                                HeaderText="Insertado en:" SortExpression="storeTS" />
                            <asp:BoundField DataField="updateTS" DataFormatString="{0:dd/MM/yyyy mm:hh:ss}" 
                                HeaderText="Modificado en:" SortExpression="updateTS" />
                        </Fields>
                    </asp:DetailsView>
                
                    </td>
                </tr>
                <tr>
                    <td align="center">
                    
                        <asp:Button ID="txtAceptarList" runat="server" Text="Aceptar" 
                            onclick="txtAceptarList_Click" />
                    
                    </td>
                </tr>
            </table>

            
          
            </asp:Panel>
<asp:Panel ID="panelagregar" runat="server" > 
<table>
	<tr>
		<td class="TableHeader" colspan="2">
		
		    <asp:Label ID="lblEntPro" runat="server" 
                Text="AGREGAR NUEVA ENTRADA DE PRODUCTO"></asp:Label>
		
		</td>
		<td>
		</td>
	</tr>
	<tr>
	    <td class="TablaField">
	    Producto :
	    </td>
	    <td>
	        <asp:DropDownList ID="drpdlproducto" runat="server" Height="28px" 
                Width="235px" DataSourceID="SqlDataSource2" DataTextField="Expr1" 
                DataValueField="productoID">
            </asp:DropDownList>
	    </td>
	    <td>
	        &nbsp;</td>
	</tr>
	<tr>
	    <td class="TablaField">
	        Ciclo:</td>
	    <td>
	        <asp:DropDownList ID="cmbCiclo" runat="server" DataSourceID="SqlDataSource1" 
                DataTextField="CicloName" DataValueField="cicloID" Height="28px" Width="235px">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                
				
                SelectCommand="SELECT cicloID, CicloName FROM Ciclos WHERE (cerrado = @cerrado) ORDER BY fechaInicio DESC">
            	<SelectParameters>
					<asp:Parameter DefaultValue="FALSE" Name="cerrado" Type="Boolean" />
				</SelectParameters>
            </asp:SqlDataSource>
	    </td>
	    <td>
	        &nbsp;</td>
	</tr>
	<tr>
        <td class="TablaField">
            Bodega :
        </td>
        <td>
            <asp:DropDownList ID="drpdlBodega" runat="server" DataSourceID="SqlDataSource3" 
                DataTextField="bodega" DataValueField="bodegaID" Height="23px" Width="172px">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
	<tr>
	    <td class="TablaField">
	    Tipo de Movimiento :
	    </td>
	    <td>
	        <asp:DropDownList ID="drpdlTipoMov" runat="server" Height="21px" 
                Width="171px" DataSourceID="SqlDataSource4" DataTextField="tipoMovimiento" 
                DataValueField="tipoMovProdID">
            </asp:DropDownList>
	    </td>
	    <td>
	        <asp:TextBox ID="txtcanAux" runat="server" Visible="False" Width="3px"></asp:TextBox>
	    </td>
	</tr>
	<tr>
        <td class="TablaField">
            Fecha:</td>
        <td>
            <asp:TextBox ID="txtFecha" runat="server" ReadOnly="True"></asp:TextBox>
            <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtFecha" 
                Separator="/" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
	<tr>
	    <td class="TablaField">
	    Cantidad :
	    </td>
	    <td>
	        <asp:TextBox ID="txtCant" runat="server"></asp:TextBox>
	    </td>
	    <td>
	        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtCant" CssClass="Validator" 
                ErrorMessage="El campo Cantidad es necesario"></asp:RequiredFieldValidator>
            <br />
            <asp:CompareValidator ID="cvalidator" runat="server" 
                ControlToValidate="txtCant" ErrorMessage="Ingresa una cantidad Válida" 
                Type="Double"></asp:CompareValidator>
	    </td>
	</tr>
	<tr>
        <td class="TablaField">
            Precio:</td>
        <td>
            <asp:TextBox ID="txtPrecio" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txtPrecio" CssClass="Validator" 
                ErrorMessage="El campo precio es necesario"></asp:RequiredFieldValidator>
            <br />
            <asp:CompareValidator ID="cvalidatorprecio" runat="server" 
                ControlToValidate="txtPrecio" ErrorMessage="Ingresa una cantidad Válida" 
                Type="Double"></asp:CompareValidator>
        </td>
    </tr>
	<tr>
	    <td class="TablaField">
	    Observaciones :
	    </td>
	    <td>
	        <asp:TextBox ID="txtobser" runat="server" Height="63px" TextMode="MultiLine"></asp:TextBox>
	    </td>
	    <td>
	        &nbsp;</td>
	</tr>
	
	<tr>
	    <td colspan="3">
	    
	    </td>
	    
	</tr>
	<tr align="center">
	    <td colspan="2">
	        <asp:TextBox ID="txtIdToModify" runat="server" Width="16px" Visible="False">-1</asp:TextBox>
	        <asp:TextBox ID="txtIdDetails" runat="server" Visible="False" Width="16px">-1</asp:TextBox>
	        <asp:Button ID="btnAgregar" runat="server" CssClass="Button" Text="Agregar" 
                onclick="btnAgregar_Click" />
            <asp:Button ID="btnModificar" runat="server" CssClass="Button" Text="Modificar" 
                onclick="btnModificar_Click" />
            <asp:Button ID="btnCancelar" runat="server" CssClass="Button" Text="Cancelar" 
                CausesValidation="False" onclick="btnCancelar_Click" />
	    </td>
	    <td>
	    </td>
	</tr>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        
		SelectCommand="SELECT Productos.productoID, Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Expr1 FROM Productos INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID ORDER BY Productos.Nombre">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        SelectCommand="SELECT [tipoMovProdID], [tipoMovimiento] FROM [TiposMovimientoProducto]">
    </asp:SqlDataSource>
</table>
</asp:Panel>
    <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        
        
        SelectCommand="SELECT EntradaDeProductos.entradaprodID, Productos.Nombre, Bodegas.bodega, TiposMovimientoProducto.tipoMovimiento, EntradaDeProductos.Fecha, EntradaDeProductos.cantidad, EntradaDeProductos.observaciones, Users.Nombre AS Expr1, EntradaDeProductos.storeTS, EntradaDeProductos.updateTS, EntradaDeProductos.preciocompra AS precio FROM Bodegas INNER JOIN EntradaDeProductos ON Bodegas.bodegaID = EntradaDeProductos.bodegaID INNER JOIN Productos ON EntradaDeProductos.productoID = Productos.productoID INNER JOIN TiposMovimientoProducto ON EntradaDeProductos.tipoMovProdID = TiposMovimientoProducto.tipoMovProdID INNER JOIN Users ON EntradaDeProductos.userID = Users.userID WHERE (EntradaDeProductos.entradaprodID = @entradaprodID)">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtIdDetails" DefaultValue="-1" 
                Name="entradaprodID" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
