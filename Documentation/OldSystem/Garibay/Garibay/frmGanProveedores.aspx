<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" Title="PROVEEDORES PARA LA VENTA DE GANADO" AutoEventWireup="true" CodeBehind="frmGanProveedores.aspx.cs" Inherits="Garibay.frmGanProveedores" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

    <script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>
    <asp:UpdatePanel runat="server" ID="updatePanelProveedoresGanado">
<ContentTemplate>

                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                            AssociatedUpdatePanelID="updatePanelProveedoresGanado" DisplayAfter="0">
                            <ProgressTemplate>
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                                Cargando información...
                            </ProgressTemplate>
                        </asp:UpdateProgress>

         
        <asp:Panel ID="panelmensaje" runat="server">
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
                        <asp:Label ID="lblMensajeOperationresult" runat="server" 
                            SkinID="lblMensajeOperationresult" Text="Label"></asp:Label>
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
    <asp:Panel ID="panellistaUsuarios" runat="Server">
        <table>
            <tr>
                <td align="center" class="TableHeader">
                    PROVEEDORES VENTA DE GANADO&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnAgregarDeLista" runat="server" CssClass="Button" 
                        onclick="btnAgregarDeLista_Click" Text="Agregar" Width="100px" />
                    <asp:Button ID="btnModificarDeLista" runat="server" CssClass="Button" 
                        Text="Modificar" Width="100px" onclick="btnModificarDeLista_Click" />
                    <asp:Button ID="btnEliminar" runat="server" CssClass="Button" Text="Eliminar" 
                        Width="100px" onclick="btnEliminar_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gridProvGan" runat="server" AllowPaging="True" 
                        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                        DataSourceID="SqlDataSource1" ForeColor="White" GridLines="None" 
                        onselectedindexchanged="gridProvGan_SelectedIndexChanged" 
                        DataKeyNames="ganProveedorID,Nombre,direccion,ciudad,estadoID,RFC,estado">
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <HeaderStyle CssClass="TableHeader" />
                        <AlternatingRowStyle BackColor="White" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <Columns>
                            <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                                ShowSelectButton="True" />
                            <asp:BoundField DataField="ganProveedorID" HeaderText="ganProveedorID" 
                                InsertVisible="False" ReadOnly="True" SortExpression="ganProveedorID" 
                                Visible="False" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" 
                                SortExpression="Nombre" />
                            <asp:BoundField DataField="direccion" HeaderText="Dirección" 
                                SortExpression="direccion" />
                            <asp:BoundField DataField="ciudad" HeaderText="Ciudad" 
                                SortExpression="ciudad" />
                            <asp:BoundField DataField="estado" HeaderText="Estado" 
                                SortExpression="estado" />
                            <asp:BoundField DataField="RFC" HeaderText="RFC" SortExpression="RFC" />
                            <asp:BoundField DataField="estadoID" HeaderText="estadoID" 
                                SortExpression="estadoID" Visible="False" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
                        
                        SelectCommand="SELECT gan_Proveedores.Nombre, gan_Proveedores.direccion, gan_Proveedores.ciudad, gan_Proveedores.estadoID, gan_Proveedores.RFC, Estados.estado, gan_Proveedores.ganProveedorID FROM gan_Proveedores INNER JOIN Estados ON gan_Proveedores.estadoID = Estados.estadoID">
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="panelagregarUsuario" runat="Server">
        <table>
            <tr>
                <td align="center" class="TableHeader" colspan="2">
                    <asp:Label ID="lblProveedores" runat="server" 
                        Text="AGREGAR PROVEEDOR PARA VENTA DE GANADO"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Nombre:</td>
                <td>
                    <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="valname" runat="server" 
                        ControlToValidate="txtNombre" CssClass="Validator" 
                        ErrorMessage="El campo Nombre es necesario:"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Dirección:</td>
                <td>
                    <asp:TextBox ID="txtDireccion" runat="server" MaxLength="50" 
                        Width="200px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Ciudad:</td>
                <td>
                    <asp:TextBox ID="txtCiudad" runat="server" Height="22px" Width="197px" 
                        MaxLength="50"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Estado:</td>
                <td>
                    <asp:DropDownList ID="cmbEstado" runat="server" 
                        DataSourceID="SqlDataSource2" DataTextField="estado" 
                        DataValueField="estadoID" Height="22px" Width="200px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        SelectCommand="SELECT [estadoID], [estado] FROM [Estados] ORDER BY [estado]">
                    </asp:SqlDataSource>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    RFC:</td>
                <td>
                    <asp:TextBox ID="txtRfc" runat="server" MaxLength="50" Width="306px"></asp:TextBox>
                </td>
                <td>

                    <script type="text/javascript">

<!--
function validaPassword(objSource, objArgs)
{
// Get value.
var number = objArgs.Value;
// Check value and return result.
if (number.length >= 6)
{
objArgs.IsValid = true;
}
else
{
objArgs.IsValid = false;
}
}
// -->
</script>

                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnAgregarDeForm" runat="server" CssClass="Button" 
                        onclick="btnAgregarDeForm_Click" Text="Agregar" Width="100px" />
                    <asp:Button ID="btnModificarDeForm" runat="server" CssClass="Button" 
                        Text="Modificar" Width="100px" onclick="btnModificarDeForm_Click" />
                    <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                        CssClass="Button" Text="Cancelar" Width="100px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>


</asp:Content>
