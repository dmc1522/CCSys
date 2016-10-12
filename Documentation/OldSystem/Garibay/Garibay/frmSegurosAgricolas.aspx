<%@ Page Language="C#" Theme="skinverde"  MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmSegurosAgricolas.aspx.cs" Inherits="Garibay.Formulario_web1" Title="Seguros Agrícolas" %>
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
  <table>
        <tr>
            <td class="TableHeader" align="center">
            SEGUROS AGRÍCOLAS
    
            </td>
            
       </tr>
       <tr>
       <td>
       <table>
       	<tr>
       		<td class="TableHeader" align="center">
       		LISTA DE SEGUROS AGRICOLAS
       		</td>
       	</tr>
       	<tr>
       		<td align="center">
				   <asp:TextBox ID="txtidModDel" runat="server" Visible="False"></asp:TextBox>
				   <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
					   onclick="btnAgregar_Click" />
       		</td>
       	</tr>
       	<tr>
       		<td align="center">
				   <asp:GridView ID="grdvSeguros" runat="server" AutoGenerateColumns="False" 
					   DataKeyNames="seguroID,Nombre" DataSourceID="sqlseguros" 
					   onrowdatabound="grdvSeguros_RowDataBound" 
					   onselectedindexchanged="grdvSeguros_SelectedIndexChanged">
					   <Columns>
						   <asp:BoundField DataField="seguroID" HeaderText="No. Seguro" 
							   InsertVisible="False" ReadOnly="True" SortExpression="seguroID" />
						   <asp:BoundField DataField="Nombre" HeaderText="Nombre" 
							   SortExpression="Nombre" />
						   <asp:BoundField DataField="CostoPorHectarea" DataFormatString="{0:C2}" 
							   HeaderText="Costo por Hectárea" SortExpression="CostoPorHectarea" />
						   <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" 
							   SortExpression="Descripcion" />
						   <asp:BoundField DataField="storeTS" DataFormatString="{0:dd/MM/yyyy}" 
							   HeaderText="Añadido en:" SortExpression="storeTS" />
						   <asp:BoundField DataField="updateTS" DataFormatString="{0:dd/MM/yyyy}" 
							   HeaderText="Modificado en:" SortExpression="updateTS" />
					   	<asp:TemplateField HeaderText="ABRIR">
							<ItemTemplate>
								<asp:HyperLink ID="LinkButton1" runat="server" 
									NavigateUrl="~/frmSegurosAgricolas.aspx">Abrir</asp:HyperLink>
							</ItemTemplate>
						   </asp:TemplateField>
						   <asp:TemplateField HeaderText="ELIMINAR">
							   <ItemTemplate>
								   <asp:HyperLink ID="LinkButton2" runat="server" 
									   NavigateUrl="~/frmSegurosAgricolas.aspx">Eliminar</asp:HyperLink>
							   </ItemTemplate>
						   </asp:TemplateField>
					   </Columns>
				   
				   </asp:GridView>
       			   <asp:SqlDataSource ID="sqlseguros" runat="server" 
					   ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
					   SelectCommand="SELECT * FROM [SegurosAgricolas]"></asp:SqlDataSource>
       		</td>
       	</tr>
       	
       </table>
       </td>
       </tr>
       <tr>
       <td>
		   <asp:Panel ID="pnlAdd" runat="server">
		   
       <table>
       	<tr>
       		<td colspan="3" class="TableHeader" align="center">
				   <asp:Label ID="lblAddMod" runat="server" Text="AGREGAR SEGURO AGRÍCOLA"></asp:Label>
       		</td>
       	</tr>
       	<tr>
       		<td class="TablaField">
				   NOMBRE :
       		</td>
       		<td>
				   <asp:TextBox ID="txtNombre" runat="server" Width="200px"></asp:TextBox>
       		</td>
       		<td>
				   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="El Campo del Nombre es Necesario" ControlToValidate="txtNombre"></asp:RequiredFieldValidator>
       		</td>
       	</tr>
       	<tr>
       		<td class="TablaField">
				   COSTO POR HECTÁREA :
       		</td>
       		<td>
				   <asp:TextBox ID="txtMontoXhectarea" runat="server"></asp:TextBox>
       		</td>
       		<td>
				   <asp:RangeValidator ID="RangeValidator1" runat="server" 
					   ErrorMessage="El Monto es Inválido  " ControlToValidate="txtMontoXhectarea" 
					   Type="Double"></asp:RangeValidator>
       		</td>
       		
       	</tr>
       	<tr>
       		<td class="TablaField">
			DESCRIPCIÓN :
       		</td>
       		<td colspan="2">
				   <asp:TextBox ID="txtDescrip" runat="server" TextMode="MultiLine" Height="120px" 
					   Width="350px"></asp:TextBox>
       		</td>
       	</tr>
       	<tr>
       		<td colspan="2">
			<asp:Button ID="btnAddAbajo" runat="server" Text="Agregar" 
					onclick="btnAddAbajo_Click" />
			
				   <asp:Button ID="btnModificarAbajo" runat="server" Text="Modificar" 
					onclick="btnModificarAbajo_Click" />
				   <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
					onclick="btnCancelar_Click" />
       		</td>
       	</tr>
       </table>
       </asp:Panel>
       </td>
       </tr>

    </table>
</asp:Content>
