<%@ Page Language="C#" Theme="skinrojo" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmProductos.aspx.cs" Inherits="Garibay.frmProductos" Title="Productos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

 
<asp:Panel ID="panelListaProductos" runat="Server">
<table>
        <tr>
         <td align="center" class="TableHeader">
           
             PRODUCTOS</td>
        </tr>
        <tr>
         <td>

  	     <asp:Button ID="btnAgregarDeLista" runat="server" CssClass="Button" Text="Agregar" 
                 Width="100px" onclick="btnAgregar_Click" />

                <asp:Button ID="btnModificarDeLista" runat="server" CssClass="Button" 
                    style="text-align: center" Text="Modificar" Width="100px" 
                 onclick="cmdModificar_Click" />
                <asp:Button ID="btnEliminar" runat="server" CssClass="Button" 
                    style="text-align: center" Text="Eliminar" Width="100px" 
                 onclick="btnEliminar_Click" />

            </td>
        </tr>
        </table>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        
        
        
        
        
        SelectCommand="SELECT Productos.productoID, Productos.Nombre, Productos.codigo, Productos.descripcion, Productos.precio1, Productos.precio2, Productos.precio3, Productos.precio4, Productos.storeTS, Productos.updateTS, productoGrupos.grupo, Productos.unidadID, Productos.presentacionID, Unidades.Unidad, Presentaciones.Presentacion, Productos.codigoBascula, Productos.productoGrupoID, Productos.Nombre + '  -  ' + Presentaciones.Presentacion AS ProdPres, CasaAgricola.CasaAgricola, CasaAgricola.casaagricolaID FROM productoGrupos INNER JOIN Productos ON Productos.productoGrupoID = productoGrupos.grupoID INNER JOIN Unidades ON Productos.unidadID = Unidades.unidadID INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID INNER JOIN CasaAgricola ON Productos.casaagricolaID = CasaAgricola.casaagricolaID ORDER BY productoGrupos.grupo">
    </asp:SqlDataSource>
    <asp:GridView ID="grdvProductos" runat="server" 
        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
        DataKeyNames="productoID,Nombre,codigo,descripcion,precio1,precio2,precio3,precio4,storeTS,updateTS,grupo,unidadID,presentacionID,Unidad,Presentacion,codigoBascula,productoGrupoID,CasaAgricola,casaagricolaID" 
        DataSourceID="SqlDataSource3" ForeColor="White" GridLines="None" 
        onselectedindexchanged="grdvProductos_SelectedIndexChanged">
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <HeaderStyle CssClass="TableHeader" />
        <AlternatingRowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <Columns>
            <asp:CommandField ButtonType="Button" CausesValidation="False" 
                SelectText="&gt;" ShowCancelButton="False" ShowSelectButton="True" />
            <asp:BoundField DataField="productoID" HeaderText="productoID" 
                InsertVisible="False" ReadOnly="True" SortExpression="productoID" 
                Visible="False" />
            <asp:BoundField DataField="Grupo" HeaderText="Grupo" SortExpression="grupo">
            </asp:BoundField>
            <asp:BoundField DataField="ProdPres" HeaderText="Producto - Presentacion" 
                SortExpression="ProdPres" >
            </asp:BoundField>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" 
                Visible="False" />
            <asp:BoundField DataField="codigo" HeaderText="Código" 
                SortExpression="codigo" />
            <asp:BoundField DataField="precio1" HeaderText="Precio 1" 
                SortExpression="precio1" DataFormatString="{0:c}" />
            <asp:BoundField DataField="precio2" HeaderText="Precio 2" 
                SortExpression="precio2" DataFormatString="{0:c}" />
            <asp:BoundField DataField="precio3" HeaderText="Precio 3" 
                SortExpression="precio3" DataFormatString="{0:c}" />
            <asp:BoundField DataField="precio4" HeaderText="Precio 4" 
                SortExpression="precio4" DataFormatString="{0:c}" />
            <asp:BoundField DataField="descripcion" HeaderText="Descripción" 
                SortExpression="descripcion" />
            <asp:BoundField DataField="Unidad" 
                HeaderText="Unidad" SortExpression="Unidad">
            </asp:BoundField>
            <asp:BoundField DataField="Presentacion" 
                HeaderText="Presentacion" SortExpression="Presentacion" Visible="False">
            </asp:BoundField>
            <asp:BoundField DataField="codigoBascula" 
                HeaderText="Cod. báscula" SortExpression="codigoBascula">
            </asp:BoundField>
            <asp:BoundField DataField="unidadID" 
                HeaderText="unidadID" SortExpression="unidadID" Visible="False">
            </asp:BoundField>
            <asp:BoundField DataField="presentacionID" 
                HeaderText="presentacionID" SortExpression="presentacionID" 
                Visible="False" />
            <asp:BoundField DataField="productoGrupoID" HeaderText="productoGrupoID" 
                SortExpression="productoGrupoID" Visible="False" />
        </Columns>
    </asp:GridView>

</asp:Panel>

<asp:Panel id="panelAgregaProducto" runat="Server">
  <table>
  	<tr>
  		<td class="TableHeader" colspan="2">
            <asp:Label ID="lblPredios" runat="server" Text="AGREGAR PRODUCTO"></asp:Label>
        </td>
  	</tr>
  	<tr>
  	  <td class="TablaField">Grupo:</td>
  	  <td>
          <asp:DropDownList ID="cmbGrupoID" runat="server" AutoPostBack="True" 
              DataSourceID="SqlDataSource5" DataTextField="grupo" DataValueField="grupoID" 
              Height="22px" onselectedindexchanged="cmbGrupoID_SelectedIndexChanged" 
              Width="200px">
          </asp:DropDownList>
        </td>
  	  <td>
          <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
              ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
              SelectCommand="SELECT [grupo], [grupoID] FROM [productoGrupos]">
          </asp:SqlDataSource>
        </td>
  	</tr>
  	  <tr>
          <td class="TablaField">
              Nombre:</td>
          <td>
              <asp:TextBox ID="txtNombre" runat="server" Width="200px"></asp:TextBox>
          </td>
          <td>
              <asp:RequiredFieldValidator ID="valNombre" runat="server" 
                  ControlToValidate="txtNombre" CssClass="Validator" 
                  ErrorMessage="El campo nombre es necesario"></asp:RequiredFieldValidator>
              <br />
          </td>
      </tr>
      <tr>
          <td class="TablaField">
              Casa Agricola:</td>
          <td>
              <asp:DropDownList ID="ddlCasaAgricola" runat="server" 
                  DataSourceID="sdsCasasAgricolas" DataTextField="CasaAgricola" 
                  DataValueField="casaagricolaID">
              </asp:DropDownList>
              <asp:SqlDataSource ID="sdsCasasAgricolas" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                  SelectCommand="SELECT [casaagricolaID], [CasaAgricola] FROM [CasaAgricola] ORDER BY [casaagricolaID]">
              </asp:SqlDataSource>
          </td>
          <td>
              &nbsp;</td>
      </tr>
      <tr>
          <td class="TablaField">
              Unidad - Presentación:</td>
          <td>
              <asp:DropDownList ID="ddlunidad" runat="server" DataSourceID="sdsUnidades" 
                  DataTextField="Unidad" DataValueField="unidadID">
              </asp:DropDownList>
              <asp:DropDownList ID="ddlpresentacion" runat="server" 
                  DataSourceID="sdsPresentaciones" DataTextField="Presentacion" 
                  DataValueField="presentacionID" Height="22px" Width="114px">
              </asp:DropDownList>
          </td>
          <td>
              <asp:SqlDataSource ID="sdsUnidades" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                  SelectCommand="SELECT [unidadID], [Unidad] FROM [Unidades] ORDER BY [Unidad]">
              </asp:SqlDataSource>
              <asp:SqlDataSource ID="sdsPresentaciones" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                  SelectCommand="SELECT [presentacionID], [Presentacion] FROM [Presentaciones] ORDER BY [Presentacion]">
              </asp:SqlDataSource>
          </td>
      </tr>
  	<tr>
  	  <td class="TablaField">Codigo de barras:</td>
  	  <td>
          <asp:TextBox ID="txtCodigo" runat="server" Width="200px"></asp:TextBox>
        </td>
  	  <td>
          <br />
        </td>
  	</tr>
  	<tr>
  	  <td class="TablaField">Precio de venta 1:</td>
  	  <td>
          <asp:TextBox ID="txtPrecio1" runat="server" Width="200px"></asp:TextBox>
        </td>
  	  <td>
          <asp:CompareValidator ID="CompareValidator2" runat="server" 
              ControlToValidate="txtPrecio1" CssClass="Validator" 
              ErrorMessage="Escriba una cantidad válida." Operator="DataTypeCheck" 
              Type="Double"></asp:CompareValidator>
          <br />
          <asp:RequiredFieldValidator ID="valPrecio1Required" runat="server" 
              ControlToValidate="txtPrecio1" CssClass="Validator" 
              ErrorMessage="El campo Precio 1 es necesario"></asp:RequiredFieldValidator>
        </td>
  	</tr>
  	  	<tr>
  	  <td class="TablaField">Precio de venta 2:</td>
  	  <td>
          <asp:TextBox ID="txtPrecio2" runat="server" Width="200px"></asp:TextBox>
        </td>
  	  <td>
          <asp:CompareValidator ID="CompareValidator3" runat="server" 
              ControlToValidate="txtPrecio2" CssClass="Validator" 
              ErrorMessage="Escriba una cantidad válida." Operator="DataTypeCheck" 
              Type="Double"></asp:CompareValidator>
          <br />
          <asp:RequiredFieldValidator ID="valPrecio2Required" runat="server" 
              ControlToValidate="txtPrecio2" 
              ErrorMessage="El campo Precio 2 es necesario" CssClass="Validator"></asp:RequiredFieldValidator>
        </td>
  	</tr>
  	<tr>
  	  <td class="TablaField">Precio de venta 3:</td>
  	  <td>
          <asp:TextBox ID="txtPrecio3" runat="server" Width="200px"></asp:TextBox>
        </td>
  	  <td>
          <asp:CompareValidator ID="CompareValidator4" runat="server" 
              ControlToValidate="txtPrecio3" CssClass="Validator" 
              ErrorMessage="Escriba una cantidad válida." Operator="DataTypeCheck" 
              Type="Double"></asp:CompareValidator>
          <br />
          <asp:RequiredFieldValidator ID="valPrecio3Required" runat="server" 
              ControlToValidate="txtPrecio3" 
              ErrorMessage="El campo Precio 3 es necesario" CssClass="Validator"></asp:RequiredFieldValidator>
        </td>
  	</tr>
  	<tr>
  	  <td class="TablaField">Precio de venta 4:</td>
  	  <td>
          <asp:TextBox ID="txtPrecio4" runat="server" Width="200px"></asp:TextBox>
        </td>
  	  <td>
          <asp:CompareValidator ID="CompareValidator5" runat="server" 
              ControlToValidate="txtPrecio4" CssClass="Validator" 
              ErrorMessage="Escriba una cantidad válida." Operator="DataTypeCheck" 
              Type="Double"></asp:CompareValidator>
          <br />
          <asp:RequiredFieldValidator ID="valPrecio4Required" runat="server" 
              ControlToValidate="txtPrecio4" 
              ErrorMessage="El campo Precio 4 es necesario" CssClass="Validator"></asp:RequiredFieldValidator>
        </td>
  	</tr>
  	  <tr>
          <td class="TablaField">
              Descripción:</td>
          <td>
              <asp:TextBox ID="txtDescripcion" runat="server" Height="68px" 
                  TextMode="MultiLine" Width="285px"></asp:TextBox>
          </td>
          <td>
              &nbsp;</td>
      </tr>
  	<tr>
  	  <td class="TablaField">Codigo en Archivo de boletas:</td>
  	  <td>
          <asp:TextBox ID="txtCodigoBoletasFile" runat="server"></asp:TextBox>
        </td>
  	</tr>
      <tr>
          <td>
          </td>
          <td>
              <asp:Button ID="btnAgregarDeForm" runat="server" CssClass="Button" 
                  onclick="btnAgregarDeForm_Click" Text="Agregar" Width="100px" />
              <asp:Button ID="btnModificarDeForm" runat="server" CssClass="Button" 
                  onclick="btnModificarDeForm_Click" Text="Modificar" Width="100px" />
              <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                  CssClass="Button" onclick="btnCancelar_Click" Text="Cancelar" Width="100px" />
          </td>
      </tr>
      <tr>
          <td colspan="2" class="TableHeader">
              CAMBIOS DE PRECIO AL PRODUCTO</td>
      </tr>
      <tr>
          <td colspan="2">
              <asp:GridView ID="gvCambiosPrecio" runat="server" AllowPaging="True" 
                  AllowSorting="True" AutoGenerateColumns="False" DataSourceID="sdsCambiosPrecio" 
                  PageSize="30">
                  <Columns>
                      <asp:BoundField DataField="storeTS" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" 
                          HeaderText="Fecha de Cambio" SortExpression="storeTS" />
                      <asp:BoundField DataField="precio1" DataFormatString="{0:c}" 
                          HeaderText="Precio 1" SortExpression="precio1" />
                      <asp:BoundField DataField="precio2" DataFormatString="{0:c}" 
                          HeaderText="Precio 2" SortExpression="precio2" />
                      <asp:BoundField DataField="precio3" DataFormatString="{0:c}" 
                          HeaderText="Precio 3" SortExpression="precio3" />
                      <asp:BoundField DataField="precio4" DataFormatString="{0:c}" 
                          HeaderText="Precio 4" SortExpression="precio4" />
                  </Columns>
              </asp:GridView>
              <asp:SqlDataSource ID="sdsCambiosPrecio" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                  
                  SelectCommand="SELECT precio1, precio2, precio3, precio4, storeTS, productoID FROM cambiosdePrecioProducto WHERE (productoID = @productoID) ORDER BY storeTS DESC">
                  <SelectParameters>
                      <asp:ControlParameter ControlID="grdvProductos" Name="productoID" 
                          PropertyName="SelectedValue" Type="Int32" />
                  </SelectParameters>
              </asp:SqlDataSource>
          </td>
      </tr>
   </table>
</asp:Panel>
</ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>