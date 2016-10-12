<%@ Page Language="C#" MasterPageFile = "~/MasterPage.Master" Title= "Administración de clientes para ventas" AutoEventWireup="true" CodeBehind="frmClientesVentas.aspx.cs" Inherits="Garibay.frmClientesVentas" %>

<asp:Content ID="Content1" runat="server"     contentplaceholderid="ContentPlaceHolder1">
    &nbsp;&nbsp;&nbsp;
  <asp:UpdatePanel runat="Server" ID="UpdatePanel">
    <ContentTemplate>
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

 
<asp:Panel ID="panelListaClientes" runat="Server">
<table>
        <tr>
         <td align="center" class="TableHeader">
           
             LISTA DE CLIENTES PARA VENTA</td>
        </tr>
        <tr>
         <td>

  	     <asp:Button ID="btnMostrarAgregar" runat="server" CssClass="Button" Text="Agregar" 
                 Width="100px" CausesValidation="False" onclick="btnMostrarAgregar_Click" />

                <asp:Button ID="btnMostrarModificar" runat="server" CssClass="Button" 
                    style="text-align: center" Text="Modificar" Width="100px" 
                 CausesValidation="False" onclick="btnMostrarModificar_Click" />
                <asp:Button ID="btnEliminar" runat="server" CssClass="Button" 
                    style="text-align: center" Text="Eliminar" Width="100px" 
                 onclick="btnEliminar_Click" />

            </td>
        </tr>
        </table>
    <asp:GridView ID="grdvClientes" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
        ForeColor="White" GridLines="None" DataKeyNames="clienteventaID,nombre" 
        DataSourceID="sdsClientesVentas" 
        onselectedindexchanged="grdvClientes_SelectedIndexChanged">
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <HeaderStyle CssClass="TableHeader" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                ShowSelectButton="True" />
            <asp:BoundField DataField="clienteventaID" HeaderText="clienteventaID" 
                InsertVisible="False" ReadOnly="True" SortExpression="clienteventaID" Visible="False" />
            <asp:BoundField DataField="nombre" HeaderText="Nombre" 
                SortExpression="nombre" />
            <asp:BoundField DataField="domicilio" HeaderText="Domicilio" 
                SortExpression="domicilio" />
            <asp:BoundField DataField="colonia" HeaderText="Colonia" 
                SortExpression="colonia" />
            <asp:BoundField DataField="ciudad" HeaderText="Ciudad" 
                SortExpression="ciudad" />
            <asp:BoundField DataField="CP" HeaderText="CP" SortExpression="CP" />
            <asp:BoundField DataField="estado" HeaderText="Estado" 
                SortExpression="estado" />
            <asp:BoundField DataField="telefono" HeaderText="Telefono" 
                SortExpression="telefono" />
            <asp:BoundField DataField="RFC" HeaderText="RFC" SortExpression="RFC" />
        </Columns>
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
    </asp:GridView>

    <asp:SqlDataSource ID="sdsClientesVentas" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        
        SelectCommand="SELECT ClientesVentas.clienteventaID, ClientesVentas.nombre, ClientesVentas.domicilio, ClientesVentas.ciudad, Estados.estado, ClientesVentas.telefono, ClientesVentas.RFC, ClientesVentas.colonia, ClientesVentas.CP FROM ClientesVentas INNER JOIN Estados ON ClientesVentas.estadoID = Estados.estadoID">
    </asp:SqlDataSource>

</asp:Panel>

<asp:Panel id="panelAgregaCliente" runat="Server">
  <table>
  	<tr>
  		<td class="TableHeader" colspan="2">
            <asp:Label ID="lblNewCliente" runat="server" Text="AGREGAR NUEVO CLIENTE"></asp:Label>
        </td>
  	</tr>
  	<tr>
  	  <td class="TablaField">Nombre:</td>
  	  <td>
          <asp:TextBox ID="txtNombreCliente" runat="server" Height="22px" Width="292px"></asp:TextBox>
        </td>
  	  <td>
          <asp:RequiredFieldValidator ID="valNombre" runat="server" 
              ControlToValidate="txtNombreCliente" CssClass="Validator" 
              ErrorMessage="El campo nombre es necesario"></asp:RequiredFieldValidator>
        </td>
  	</tr>
  	  <tr>
          <td class="TablaField">
              Domicilio:</td>
          <td>
              <asp:TextBox ID="txtDomicilioCliente" runat="server" Width="200px"></asp:TextBox>
          </td>
          <td>
              <br />
          </td>
      </tr>
      <tr>
          <td class="TablaField">
              Ciudad:</td>
          <td>
              <asp:TextBox ID="txtCiudad" runat="server" Height="23px" Width="198px"></asp:TextBox>
          </td>
          <td>
              &nbsp;</td>
      </tr>
  	<tr>
  	  <td class="TablaField">Estado:</td>
  	  <td>
          <asp:DropDownList ID="drpdlEstado" runat="server" Height="23px" Width="195px" 
              DataSourceID="sdsEstados" DataTextField="estado" DataValueField="estadoID">
          </asp:DropDownList>
          <asp:SqlDataSource ID="sdsEstados" runat="server" 
              ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
              SelectCommand="SELECT [estadoID], [estado] FROM [Estados]">
          </asp:SqlDataSource>
        </td>
  	  <td>
          <br />
        </td>
  	</tr>
  	  <tr>
          <td class="TablaField">
              Teléfono:</td>
          <td>
              <asp:TextBox ID="txtTelefono" runat="server" Height="23px" Width="191px"></asp:TextBox>
          </td>
          <td>
              &nbsp;</td>
      </tr>
  	<tr>
  	  <td class="TablaField">R.F.C.</td>
  	  <td>
          <asp:TextBox ID="txtRFC" runat="server" Width="200px"></asp:TextBox>
        </td>
  	  <td>
          <br />
        </td>
  	</tr>
      <tr>
          <td class="TablaField">
              Colonia:</td>
          <td>
              <asp:TextBox ID="txtColonia" runat="server" Width="280px"></asp:TextBox>
          </td>
          <td>
              &nbsp;</td>
      </tr>
      <tr>
          <td class="TablaField">
              CP:</td>
          <td>
              <asp:TextBox ID="txtCP" runat="server"></asp:TextBox>
          </td>
          <td>
              &nbsp;</td>
      </tr>
      <tr>
          <td>
          </td>
          <td>
              <asp:Button ID="btnAgregarDeForm" runat="server" CssClass="Button" 
                  Text="Agregar" Width="100px" onclick="btnAgregarDeForm_Click" />
              <asp:Button ID="btnModificarDeForm" runat="server" CssClass="Button" 
                  Text="Modificar" Width="100px" onclick="btnModificarDeForm_Click" />
              <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                  CssClass="Button"  Text="Cancelar" Width="100px" 
                  onclick="btnCancelar_Click" />
          </td>
      </tr>
   </table>
</asp:Panel>
</ContentTemplate>
    </asp:UpdatePanel>

         
        

</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    
</asp:Content>

