<%@ Page Language="C#" Theme="skinrojo" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmProveedores.aspx.cs" Inherits="Garibay.WebForm5" Title="Proveedores" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        </table>

        
      
        </asp:Panel>

<asp:Panel ID="panelfmrprovedor" runat="server" > 
<asp:Panel ID="panelLista_Proveedores" runat="server">

<table>
     <tr>
                        <td align="center" class="TableHeader">
                            PROVEEDORES</td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
   <tr>
                        <td align="left">
                            <asp:Button ID="btnAgregardeLista" runat="server" Text="Agregar" 
                                CssClass="Button" onclick="btnAgregardeLista_Click" />
                            <asp:Button ID="btnModificarDeLista" runat="server" Text="Modificar" CssClass="Button" 
                                onclick="btnModificar_Click" />
                            <asp:Button ID="btnEliminarDeLista" runat="server" Text="Eliminar" 
                                CssClass="Button" onclick="btnEliminarDeLista_Click" />
                            <asp:Button ID="btnCuentasProv" runat="server" 
                                Text="Ver Cuentas de Banco del Proveedor" onclick="btnCuentasProv_Click" />
                        </td>
                    </tr>
    <tr>
        <td>
            
             <table>
               
                    <tr>
                        <td>
                            <asp:GridView ID="gridProveedores" runat="server" AllowPaging="True" 
                                AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                                
                                DataKeyNames="proveedorID,Nombre,Direccion,CP,Comunidad,Municipio,Teléfono,Celular,Fechaalta,Nombrecontacto,banco,Observaciones,estadoID" 
                                DataSourceID="SqlDataSource3" ForeColor="White" 
                                GridLines="None" Width="100%" 
                                onselectedindexchanged="gridProveedores_SelectedIndexChanged">
                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                <HeaderStyle CssClass="TableHeader" />
                                <AlternatingRowStyle BackColor="White" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                <Columns>
                                    <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                                        ShowSelectButton="True" />
                                    <asp:BoundField DataField="proveedorID" HeaderText="proveedorID" 
                                        InsertVisible="False" ReadOnly="True" SortExpression="proveedorID" 
                                        Visible="False" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" 
                                        SortExpression="Nombre" />
                                    <asp:BoundField DataField="Direccion" HeaderText="Direccion" 
                                        SortExpression="Direccion" />
                                    <asp:BoundField DataField="CP" HeaderText="CP" SortExpression="CP" />
                                    <asp:BoundField DataField="estado" HeaderText="Estado" 
                                        SortExpression="estado" />
                                    <asp:BoundField DataField="Municipio" HeaderText="Municipio" 
                                        SortExpression="Municipio" />
                                    <asp:BoundField DataField="Comunidad" HeaderText="Comunidad" 
                                        SortExpression="Comunidad" />
                                    <asp:BoundField DataField="Teléfono" HeaderText="Teléfono" 
                                        SortExpression="Teléfono" />
                                    <asp:BoundField DataField="Celular" HeaderText="Celular" 
                                        SortExpression="Celular" />
                                    <asp:BoundField DataField="Fechaalta" HeaderText="Fecha de alta" 
                                        SortExpression="Fechaalta" />
                                    <asp:BoundField DataField="Nombrecontacto" HeaderText="Nombre de contacto" 
                                        SortExpression="Nombrecontacto" />
                                    <asp:BoundField DataField="banco" HeaderText="Banco" SortExpression="banco" />
                                    <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" 
                                        SortExpression="Observaciones" />
                                    <asp:BoundField DataField="estadoID" HeaderText="estadoID" 
                                        SortExpression="estadoID" Visible="False" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                
                                SelectCommand="SELECT Proveedores.proveedorID, Proveedores.Nombre, Proveedores.Direccion, Proveedores.CP, Proveedores.Comunidad, Proveedores.Municipio, Proveedores.Teléfono, Proveedores.Celular, Proveedores.Fechaalta, Proveedores.Nombrecontacto, Proveedores.banco, Proveedores.Observaciones, Estados.estado, Proveedores.estadoID FROM Proveedores INNER JOIN Estados ON Proveedores.estadoID = Estados.estadoID">
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                 
                 
                </table>
           
        </td>

    </tr>
    
</table>
 </asp:Panel>
 <asp:Panel ID="panelAgregarProvedor" runat="server">
        <table>
        	<tr>
        		<td colspan="2" class="TableHeader">
        		    <asp:Label ID="lblProveedores" runat="server" Text="AGREGAR NUEVO PROVEEDOR"></asp:Label>
        		</td>
        	</tr>
        	
        	<tr>
        	    <td class="TablaField">
        	    Nombre :
        	    </td>
        	    <td>
        	        <asp:TextBox ID="TxtNombre" runat="server" Width="450px"></asp:TextBox>
        	    </td>
        	    <td>
        	        <asp:RequiredFieldValidator ID="RFVnombre" runat="server" 
                        ErrorMessage="El campo del Nombre es necesario" 
                        ControlToValidate="TxtNombre"></asp:RequiredFieldValidator>
        	    </td>
        	</tr>
        	<tr>
        	    <td class="TablaField">
        	        Dirección :</td>
        	    <td>
        	        <asp:TextBox ID="txtdireccion" runat="server" Width="450px"></asp:TextBox>
        	    </td>
        	    <td>
        	        </td>
        	</tr>
        	<tr>
        	    <td class="TablaField">
        	        Código Postal :</td>
        	    <td>
        	        <asp:TextBox ID="txtCp" runat="server" Width="200px"></asp:TextBox>
        	    </td>
        	    <td>
        	        </td>
        	</tr>
        	<tr>
        	    <td class="TablaField">
        	        Estado :
        	    </td>
        	    <td>
        	        <asp:DropDownList ID="drplstEstado" runat="server" Width="200px" 
                        DataSourceID="SqlDataSource2" DataTextField="estado" DataValueField="estadoID">
        	        <asp:ListItem Value="0">AGUASCALIENTES</asp:ListItem>
                    <asp:ListItem Value="1">JALISCO</asp:ListItem>
                    </asp:DropDownList>
        	    </td>
        	    <td>
        	        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT [estado], [estadoID] FROM [Estados]">
                    </asp:SqlDataSource>
        	        </td>
        	</tr>
        	<tr>
        	    <td class="TablaField">
        	        Municipio :
        	    </td>
        	    <td>
        	        <asp:TextBox ID="txtMunicipio" runat="server" Width="200px"></asp:TextBox>
        	    </td>
        	    <td>
        	    </td>
        	</tr>
        	<tr>
        	    <td class="TablaField">
        	        Comunidad :
        	    </td>
        	    <td>
        	        <asp:TextBox ID="txtComunidad" runat="server" Width="200px"></asp:TextBox>
        	    </td>
        	    <td>
        	    </td>
        	</tr>
        	<tr>
        	    <td class="TablaField">
        	        Télefono:
        	    </td>
        	    <td>
        	        <asp:TextBox ID="txtTelefono" runat="server" Width="200px"></asp:TextBox>
        	    </td>
        	    <td>
        	    </td>
        	</tr>
        	<tr>
        	    <td class="TablaField">
        	        Celular :
        	    </td>
        	    <td>
        	        <asp:TextBox ID="txtCel" runat="server" Width="200px"></asp:TextBox>
        	    </td>
        	    <td>
        	    </td>
        	</tr>
        	
        	<tr>
        	    <td class="TablaField">
        	    Nombre de Contacto:
        	    </td>
        	    <td>
        	        <asp:TextBox ID="txtNomCon" runat="server" Width="450px"></asp:TextBox>
        	    </td>
        	    <td>
        	    </td>
        	</tr>
        	<tr>
        	    <td class="TablaField">
        	        Banco:
        	    </td>
        	    <td>
        	        <asp:TextBox ID="txtBanco" runat="server" Height="55px" TextMode="MultiLine" 
                        Width="300px"></asp:TextBox>
        	    </td>
        	    <td>
        	    </td>
        	</tr>
        	<tr>
        	    <td class="TablaField">
        	        Observaciones:
        	    </td>
        	    <td>
        	        <asp:TextBox ID="txtObser" runat="server" Height="55px" TextMode="MultiLine" 
                        Width="300px"></asp:TextBox>
        	    </td>
        	    <td>
        	    </td>
        	</tr>
        	<tr>
        	<td colspan="2" align="center">
        	 <asp:Button ID="btnAceptar" runat="server" CssClass="Button" 
            Text="Agregar" onclick="btnAceptar_Click"/>
            <asp:Button ID="btnModificarpro" runat="server" CssClass="Button" 
            Text="Modificar" onclick="btnModificarpro_Click" />
        <asp:Button ID="btnCancelar" runat="server" CssClass="Button" Text="Cancelar" 
            CausesValidation="False" onclick="btnCancelar_Click1"/>
        	</td>
        	
        	</tr>
        </table>
        </asp:Panel>
        </asp:Panel>
         <asp:Panel ID="panelCuentasProv" runat="server">
         <asp:Panel ID="panellistaCuentas" runat="server" > 
        


        <table>
        <tr>
            <td class="TableHeader">
                <asp:Label ID="lblListaCuentaProv" runat="server" Text="Cuentas de Banco del Proveedor "></asp:Label>
            </td>
        </tr>
        <tr>
         	    <td>
                     <asp:Button ID="btnAgregarCuentaProv_lista" runat="server" Text="Agregar Cuenta" 
                         onclick="btnAgregarCuenta_Click" />
                     <asp:Button ID="btnModificarCuentaProv_lista" runat="server" Text="Modificar Cuenta" 
                         onclick="btnModificarCuentaProv_Click" />
                     <asp:Button ID="btnEliminarCuentaProv" runat="server" Text="Eliminar Cuenta" 
                         onclick="btnEliminarCuentaProv_Click" />
         	         <asp:Button ID="btnRegresarLista" runat="server" 
                         onclick="btnRegresarLista_Click" Text="Regresar a Lista de Proveedores" />
         	    </td>
         	</tr>
        	<tr>
        		<td>
        	        <asp:GridView ID="grdvCuentasProv" runat="server" AllowPaging="True" 
                        AllowSorting="True" AutoGenerateColumns="False" 
                        DataKeyNames="cuentaID,bancoID,proveedorID,nombre,Expr1,numCuenta,Expr2" 
                        DataSourceID="SqlDSCuentasBancoProv" 
                        onselectedindexchanged="grdvCuentasProv_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                                ShowSelectButton="True" />
                            <asp:BoundField DataField="Expr1" HeaderText="Proveedor" 
                                SortExpression="Expr1" />
                            <asp:BoundField DataField="numCuenta" HeaderText="No. Cuenta" 
                                SortExpression="numCuenta" />
                            <asp:BoundField DataField="nombre" HeaderText="Banco" SortExpression="nombre" />
                            <asp:BoundField DataField="Expr2" HeaderText="Titular" SortExpression="Expr2" />
                            <asp:BoundField DataField="cuentaID" HeaderText="cuentaID" 
                                InsertVisible="False" ReadOnly="True" SortExpression="cuentaID" 
                                Visible="False" />
                            <asp:BoundField DataField="bancoID" HeaderText="bancoID" ReadOnly="True" 
                                SortExpression="bancoID" Visible="False" />
                            <asp:BoundField DataField="proveedorID" HeaderText="proveedorID" 
                                ReadOnly="True" SortExpression="proveedorID" Visible="False" />
                        </Columns>
                    </asp:GridView>
                    <asp:TextBox ID="txtcuentasdeProv" runat="server" Visible="False" Width="5px">-1</asp:TextBox>
        	    </td>
            </tr>
        </table>
        </asp:Panel>
         <asp:Panel ID="panelAgregarCuentaProv" runat="server" > 
         <table>
         	<tr>
         		<td class="TableHeader" align="center" colspan="2">
                     <asp:Label ID="lblcuentadebancoprov" runat="server" Text="Agregar Nueva Cuenta de Banco de Proveedor"></asp:Label>
         		</td>
         	</tr>
         	<tr>
         	    <td class="TablaField">
         	    Proveedor :
         	    </td>
                 
         	    <td>
         	    <asp:DropDownList ID="drpdlProveedor" runat="server" Width="300px" 
                        DataSourceID="sqldsproveedores" DataTextField="Nombre" 
                        DataValueField="proveedorID">
                 </asp:DropDownList>
         	    </td>
         	</tr>
         	<tr>
         	    <td class="TablaField">
         	    Banco :
         	    </td>
                 
         	    <td>
         	    <asp:DropDownList ID="drpdlbanco" runat="server" Width="200px" 
                        DataSourceID="sqldsBancos" DataTextField="nombre" DataValueField="bancoID">
                 </asp:DropDownList>
         	    </td>
         	</tr>
         	<tr>
         	    <td class="TablaField">
         	    Cuenta de Banco :
         	   
         	    </td>
                 
         	    <td>
         	    <asp:TextBox ID="txtCuentaBanco" runat="server" MaxLength="10"></asp:TextBox>
         	    </td>
         	    <td>
         	        <asp:RequiredFieldValidator ID="rfvcuentaBanco" runat="server" 
                        ControlToValidate="txtCuentaBanco" 
                        ErrorMessage="El Campo de la Cuenta de Banco es Necesario"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="revtitular" runat="server" 
                        ControlToValidate="txtCuentaBanco" 
                        ErrorMessage="El Campo Cuenta de Banco deber ser de 10 Dígitos " 
                        ValidationExpression="\d\d\d\d\d\d\d\d\d\d"></asp:RegularExpressionValidator>
         	    </td>
         	</tr>
         	<tr>
         	    <td class="TablaField">
         	    Titular :
         	    </td>
                 
         	    <td>
         	    <asp:TextBox ID="txtTitular" runat="server" MaxLength="0" Width="300px"></asp:TextBox>
         	    </td>
         	    <td>
         	        <asp:RequiredFieldValidator ID="rfvtitular" runat="server" 
                        ControlToValidate="txtTitular" ErrorMessage="El Campo Títular es Necesario"></asp:RequiredFieldValidator>
         	    </td>
         	</tr>
         	<tr>
         	    <td>
                     <asp:Button ID="btnAgregarcuentaProv" runat="server" Text="Agregar" 
                         onclick="btnAgregarcuentaProv_Click" />
                     <asp:Button ID="btnModificarcuentaProv" runat="server" Text="Modificar" 
                         onclick="btnModificarcuentaProv_Click1" />
                     <asp:Button ID="btncancelarcuentasProv" runat="server" Text="Cancelar" 
                         onclick="btncancelarcuentasProv_Click" />
         	    </td>
         	</tr>
         </table>
         </asp:Panel>
        </asp:Panel>
     <asp:SqlDataSource ID="SqlDSCuentasBancoProv" runat="server" 
         ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
         SelectCommand="SELECT CuentasBancoProveedores.cuentaID, CuentasBancoProveedores.bancoID, Bancos.nombre, CuentasBancoProveedores.proveedorID, Proveedores.Nombre AS Expr1, CuentasBancoProveedores.numCuenta, CuentasBancoProveedores.nombre AS Expr2 FROM Bancos INNER JOIN CuentasBancoProveedores ON Bancos.bancoID = CuentasBancoProveedores.bancoID INNER JOIN Proveedores ON CuentasBancoProveedores.proveedorID = Proveedores.proveedorID WHERE (CuentasBancoProveedores.proveedorID = @proveedorID)">
         <SelectParameters>
             <asp:ControlParameter ControlID="txtcuentasdeProv" DefaultValue="-1" 
                 Name="proveedorID" PropertyName="Text" />
         </SelectParameters>
     </asp:SqlDataSource>
     <asp:SqlDataSource ID="sqldsproveedores" runat="server" 
         ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
         SelectCommand="SELECT [proveedorID], [Nombre] FROM [Proveedores]"></asp:SqlDataSource>
     <asp:SqlDataSource ID="sqldsBancos" runat="server" 
         ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
         SelectCommand="SELECT * FROM [Bancos]"></asp:SqlDataSource>
</asp:Content>
