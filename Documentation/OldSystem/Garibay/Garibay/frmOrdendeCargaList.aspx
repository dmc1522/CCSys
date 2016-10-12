<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" Title = "Lista de órdenes de carga" AutoEventWireup="true" CodeBehind="frmOrdendeCargaList.aspx.cs" Inherits="Garibay.frmOrdendeCargaList" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <table>
 	<tr>
 		<td class="TableHeader" style="text-align: center">
 		    LISTA DE ÓRDENES DE CARGA</td>
 	</tr>
 	<tr>
 		<td>
 		 <asp:Panel ID="panelFiltros" runat="Server" GroupingText="Filtrar resultados:">
 		  <table>
 		  	<tr>
 		  		<td>
 		  		 <table>
 		  		 	<tr>
 		  		<td class="TablaField">Empresa:</td>
 		  		<td>
                    <asp:DropDownList ID="ddlEmpresas" runat="server" DataSourceID="sdsEmpresa" 
                        DataTextField="Empresa" DataValueField="empresaID" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsEmpresa" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
						
						
                        
                        SelectCommand="SELECT Empresas.empresaID, Empresas.Empresa FROM Empresas INNER JOIN OrdenesDeCarga ON Empresas.empresaID = OrdenesDeCarga.empresaID WHERE (Empresas.empresaID &lt;&gt; @empresaID) ORDER BY Empresas.Empresa">
                    	<SelectParameters>
							<asp:Parameter DefaultValue="3" Name="empresaID" Type="Int32" />
						</SelectParameters>
                    </asp:SqlDataSource>
                </td>
 		  		<td class="TablaField">Proveedor:</td>
 		  		<td>
                    <asp:DropDownList ID="ddlProveedor" runat="server" 
                        DataSourceID="sdsProveedores" DataTextField="Nombre" 
                        DataValueField="proveedorID" Height="22px" Width="260px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsProveedores" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT distinct OrdenesDeCarga.proveedorID, Proveedores.Nombre FROM Proveedores INNER JOIN OrdenesDeCarga ON Proveedores.proveedorID = OrdenesDeCarga.proveedorID ORDER BY Proveedores.Nombre">
                    </asp:SqlDataSource>
                        </td>
 		  	</tr>
 		  		 </table>
 		  		</td>
 		  	</tr>
 		  	  <tr>
                  <td align="center">
                      <asp:Button ID="btnAgregarOrdenDeCarga" runat="server" CssClass="Button" 
                          onclick="btnAgregarOrdenDeCarga_Click" Text="Agregar Orden de Carga" />
                      <asp:Button ID="btnEliminar" runat="server" 
                          Text="Eliminar" Visible="False" />
                  </td>
              </tr>
 		  </table>
 		 </asp:Panel>
 		</td>
 	</tr>
 	<tr>
 	    <td>
                    <asp:GridView ID="gvOrdenesDeCarga" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="sdsListOrdenesDeCarga" DataKeyNames="ordenDeCargaID" 
                        onrowdatabound="gvOrdenesDeCarga_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Abrir">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBAbrirOrdenDeCarga" runat="server">Abrir</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Imprimir">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBPrintOrdenCarga" runat="server">Imprimir</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ordenDeCargaID" HeaderText="ordenDeCargaID" 
                                InsertVisible="False" SortExpression="ordenDeCargaID" Visible="False" />
                            <asp:BoundField DataField="Empresa" HeaderText="Empresa" 
                                SortExpression="Empresa" />
                            <asp:BoundField DataField="ProveedorName" HeaderText="Proveedor" 
                                SortExpression="ProveedorName" />
                            <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" 
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="bodega" HeaderText="Bodega" 
                                SortExpression="bodega" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsListOrdenesDeCarga" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        SelectCommand="SELECT Empresas.Empresa, OrdenesDeCarga.fecha, OrdenesDeCarga.chofer, OrdenesDeCarga.placas, OrdenesDeCarga.marca, OrdenesDeCarga.anio, OrdenesDeCarga.color, OrdenesDeCarga.jaula, OrdenesDeCarga.origen, OrdenesDeCarga.producto, OrdenesDeCarga.presentacion, OrdenesDeCarga.bodega, OrdenesDeCarga.ubicacion, OrdenesDeCarga.destino, OrdenesDeCarga.facturar_a, OrdenesDeCarga.observaciones, OrdenesDeCarga.storeTS, Users.Nombre, OrdenesDeCarga.emailToSend, OrdenesDeCarga.emailCC, OrdenesDeCarga.emaildescription, Users_1.Nombre AS SentbyUser, Proveedores.Nombre AS ProveedorName, OrdenesDeCarga.ordenDeCargaID FROM OrdenesDeCarga INNER JOIN Users ON OrdenesDeCarga.userID = Users.userID INNER JOIN Proveedores ON OrdenesDeCarga.proveedorID = Proveedores.proveedorID INNER JOIN Empresas ON OrdenesDeCarga.empresaID = Empresas.empresaID LEFT OUTER JOIN Users AS Users_1 ON OrdenesDeCarga.emailsentby = Users_1.userID WHERE (OrdenesDeCarga.empresaID = @empresaId) AND (OrdenesDeCarga.proveedorID = @proveedorId)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlEmpresas" Name="empresaId" 
                                PropertyName="SelectedValue" />
                            <asp:ControlParameter ControlID="ddlProveedor" Name="proveedorId" 
                                PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
 	    
 	    </td>
 	</tr>
 </table>
         
     

</asp:Content>
