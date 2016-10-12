<%@ Page Language="C#" Title = "Grupos de catálogos" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmGruposCatalogos.aspx.cs" Inherits="Garibay.frmGruposCatalogos" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

         
        
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
   
    
     <p class="TableHeader">LISTA DE GRUPOS CATÁLOGOS 
     </p>
     <p>
         <asp:GridView ID="GridViewGruposCatalogos" runat="server" 
             AutoGenerateColumns="False" DataKeyNames="grupoCatalogosID" 
             DataSourceID="SqlDataSourceGruposCatalogos" 
             onrowdeleted="GridViewGruposCatalogos_RowDeleted" 
             onrowupdated="GridViewGruposCatalogos_RowUpdated">
             <Columns>
                 <asp:CommandField ButtonType="Button" CancelText="Cancelar" 
                     DeleteText="Eliminar" EditText="Editar" SelectText="&gt;" 
                     ShowDeleteButton="True" ShowEditButton="True" UpdateText="Modificar" 
                     InsertText="Nuevo" NewText="Nuevo" />
                 <asp:BoundField DataField="grupoCatalogosID" HeaderText="grupoCatalogosID" 
                     InsertVisible="False" ReadOnly="True" SortExpression="grupoCatalogosID" 
                     Visible="False" />
                 <asp:BoundField DataField="grupoCatalogo" HeaderText="Catalogo" 
                     SortExpression="grupoCatalogo" />
             </Columns>
         </asp:GridView>
         <asp:SqlDataSource ID="SqlDataSourceGruposCatalogos" runat="server" 
             ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
             DeleteCommand="DELETE FROM [GruposCatalogosMovBancos] WHERE [grupoCatalogosID] = @grupoCatalogosID" 
             InsertCommand="INSERT INTO [GruposCatalogosMovBancos] ([grupoCatalogo]) VALUES (@grupoCatalogo)" 
             SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos] ORDER BY [grupoCatalogo]" 
             UpdateCommand="UPDATE [GruposCatalogosMovBancos] SET [grupoCatalogo] = @grupoCatalogo WHERE [grupoCatalogosID] = @grupoCatalogosID">
             <DeleteParameters>
                 <asp:Parameter Name="grupoCatalogosID" Type="Int32" />
             </DeleteParameters>
             <UpdateParameters>
                 <asp:Parameter Name="grupoCatalogo" Type="String" />
                 <asp:Parameter Name="grupoCatalogosID" Type="Int32" />
             </UpdateParameters>
             <InsertParameters>
                 <asp:Parameter Name="grupoCatalogo" Type="String" />
             </InsertParameters>
         </asp:SqlDataSource>
     </p>
     
     <div id = "divAgregar">
     <p class="TableHeader">
     AGREGAR NUEVO
         </p>
     <p class="TablaField">
     Nombre: 
         <asp:TextBox ID="TextBoxGuardar" runat="server"></asp:TextBox>
     </p>
     <p>
     
         <asp:Button ID="ButtonGuardar" runat="server" Text="Guardar" 
             onclick="ButtonGuardar_Click" />
     
     </p>
     </div>
    
</asp:Content>
         
