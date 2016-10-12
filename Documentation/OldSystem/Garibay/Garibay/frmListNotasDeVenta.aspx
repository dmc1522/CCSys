<%@ Page Language="C#" Theme="skinrojo" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListNotasDeVenta.aspx.cs" Inherits="Garibay.frmListNotasDeVenta" Title="Notas de venta" %>
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
    <table>
 	<tr>
 		<td class="TableHeader" style="text-align: center">
 		    LISTA DE NOTAS DE VENTA</td>
 	</tr>
 	<tr>
 		<td>
 		 <asp:Panel ID="panelFiltros" runat="Server" GroupingText="Filtrar resultados:">
 		  <table>
 		  	<tr>
 		  		<td>
 		  		 <table>
 		  		 	<tr>
 		  		<td class="TablaField">CICLO:</td>
 		  		<td>
                    <asp:DropDownList ID="ddlCiclos" runat="server" DataSourceID="sdsCiclos" 
                        DataTextField="CicloName" DataValueField="cicloID" AutoPostBack="True" 
						onselectedindexchanged="ddlCiclos_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
						
						
                        SelectCommand="SELECT cicloID, CicloName FROM Ciclos WHERE (cerrado = @cerrado) ORDER BY CicloName DESC">
                    	<SelectParameters>
							<asp:Parameter DefaultValue="FALSE" Name="cerrado" />
						</SelectParameters>
                    </asp:SqlDataSource>
                </td>
 		  		<td class="TablaField">&nbsp;</td>
 		  		<td>
                    &nbsp;</td>
                <td class="TablaField">&nbsp;</td>
                <td>
                    &nbsp;</td>
 		  	</tr>
 		  		     <tr>
                         <td class="TablaField">
                             Productor:</td>
                         <td>
                             <asp:DropDownList ID="cmbCliente" runat="server" Height="22px" Width="260px" 
								 DataSourceID="SqlProductores" DataTextField="Nombre" 
								 DataValueField="productorID">
                             </asp:DropDownList>
                         	<asp:SqlDataSource ID="SqlProductores" runat="server" 
								 ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
								 
								 
								 SelectCommand="SELECT  0 AS productorID,' TODOS LOS PRODUCTORES' as Nombre, 0 as cicloID  UNION SELECT Productores.productorID, LTRIM(Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre) AS Nombre, Notasdeventa.cicloID FROM Productores INNER JOIN Notasdeventa ON Productores.productorID = Notasdeventa.productorID INNER JOIN Ciclos ON Notasdeventa.cicloID = Ciclos.cicloID WHERE (Ciclos.cerrado = @cerrado) ORDER BY Nombre">
							    <SelectParameters>
									<asp:Parameter DefaultValue="FALSE" Name="cerrado" />
								</SelectParameters>
							 </asp:SqlDataSource>
                         </td>
                         <td class="TablaField">
                             Tipo de pago:</td>
                         <td>
                             <asp:DropDownList ID="cmbTipoPago" runat="server" Height="22px" Width="185px" 
								 DataSourceID="SqlTipoPago" DataTextField="tipopago" DataValueField="tipopagoID">
                             </asp:DropDownList>
                         	<asp:SqlDataSource ID="SqlTipoPago" runat="server" 
								 ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
								 SelectCommand="SELECT 0 AS tipopagoID, ' TODOS' AS tipopago UNION SELECT tipopagoID, tipopago FROM TiposDePago">
							 </asp:SqlDataSource>
                         </td>
                         <td class="TablaField">
                             Crédito:</td>
                         <td>
                             <asp:DropDownList ID="cmbCredito" runat="server" Height="22px" Width="200px" 
								 DataSourceID="SqlCredito" DataTextField="Credito" DataValueField="creditoID">
                             </asp:DropDownList>
                         	<asp:SqlDataSource ID="SqlCredito" runat="server" 
								 ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
								 
								 
								 SelectCommand="SELECT        ' TODOS LOS CREDITOS' AS Credito, 0 AS creditoID , 0 as cicloID 
UNION SELECT CAST(Notasdeventa.creditoID AS Varchar) + ' - ' + Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS Credito, Notasdeventa.creditoID, Notasdeventa.cicloID FROM Productores INNER JOIN Notasdeventa ON Productores.productorID = Notasdeventa.productorID INNER JOIN Creditos ON Notasdeventa.creditoID = Creditos.creditoID">
							 </asp:SqlDataSource>
                         </td>
                     </tr>
 		  		 </table>
 		  		</td>
 		  	</tr>
 		  	<tr>
 		  	 <td>
 		  	  <table>
 		  	  	<tr>
 		  	  <td class="TablaField">Folio:</td>
 		  	  <td>
                  <asp:TextBox ID="txtFolio" runat="server" AutoCompleteType="Disabled" 
                      Width="153px"></asp:TextBox>
                </td>
 		  	  <td class="TablaField">De:</td>
 		  	  <td>
                  <asp:TextBox ID="txtFechaDe" runat="server" ReadOnly="True"></asp:TextBox>
                  <rjs:PopCalendar ID="PopCalendar1" runat="server" 
                      onselectionchanged="PopCalendar1_SelectionChanged" AutoPostBack="False" 
					  Control="txtFechaDe" />
                </td>
 		  	  <td class="TablaField">A:</td>
 		  	  <td>
                  <asp:TextBox ID="txtFechaA" runat="server" ReadOnly="True"></asp:TextBox>
                  <rjs:PopCalendar ID="PopCalendar2" runat="server" 
                      onselectionchanged="PopCalendar2_SelectionChanged" style="width: 14px" 
					  AutoPostBack="False" Control="txtFechaA" />
                </td>
   		  	</tr>
 		  	  </table>
 		  	 </td>
 		  	</tr>
 		  	<tr>
 		  	 <td align="center">
   		  	 
   		  	     <asp:Button ID="btnAgregarNota" runat="server" CssClass="Button" 
                     onclick="btnAgregarNota_Click" Text="Agregar Nota de Venta" />
   		  	 
   		  	     <asp:Button ID="btnFiltrar" runat="server" CssClass="Button" Text="Filtrar" 
                     Width="100px" onclick="btnFiltrar_Click" />
                     <asp:Button ID="btnEliminarFiltros" runat="server" CssClass="Button"  
					 Text="Limpiar Filtros" onclick="btnEliminarFiltros_Click" />
   		  	 
   		  	     <asp:Button ID="btnEliminar" runat="server" onclick="btnEliminar_Click" 
                     Text="Eliminar" Visible="False" />
   		  	 
   		  	 </td>
 		  	</tr>
 		  </table>
 		 </asp:Panel>
 		</td>
 	</tr>
 	<tr>
 	    <td>
 	         <asp:GridView ID="gridNotasVenta" runat="server" 
                AllowSorting="True" AutoGenerateC="False" CellPadding="4" 
                GridLines="None" ForeColor="Black" AutoGenerateColumns="False" 
                DataSourceID="sdsNotasVenta" DataKeyNames="notadeventaID" 
                onselectedindexchanged="gridNotasVenta_SelectedIndexChanged1">
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <HeaderStyle CssClass="TableHeader" />
                <AlternatingRowStyle BackColor="White" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <Columns>
                    <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                        ShowSelectButton="True" />
                    <asp:BoundField HeaderText="Ciclo" DataField="CicloName" 
                        SortExpression="CicloName" />
                    <asp:BoundField HeaderText="Productor" DataField="Productor" ReadOnly="True" 
                        SortExpression="Productor" >
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="ID" InsertVisible="False" 
                        SortExpression="notadeventaID">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("notadeventaID") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("notadeventaID") %>'></asp:Label>
                            &nbsp;<asp:HyperLink ID="lnkAbrir" runat="server" NavigateUrl='<%# GetURLToOpenNV(Eval("notadeventaID").ToString()) %>'
                                Text='Abrir'></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Wrap="False" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Folio" DataField="Folio" SortExpression="Folio" />
                    <asp:BoundField HeaderText="Fecha" DataField="Fecha" 
                        DataFormatString="{0:dd/MM/yyyy}" SortExpression="Fecha" />
                    <asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C2}" 
                        SortExpression="Total" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="userID" DataField="userID" 
                        SortExpression="userID" />
					<asp:BoundField DataField="creditoID" HeaderText="Numero de Crédito" 
						SortExpression="creditoID">
					<ItemStyle HorizontalAlign="Center" />
					</asp:BoundField>
					<asp:CheckBoxField DataField="acredito" HeaderText="acredito" 
						SortExpression="acredito" Visible="False" />
                	<asp:TemplateField HeaderText="IMPRIMIR" Visible="False">
						<ItemTemplate>
							<asp:HyperLink ID="HyperLink1" runat="server" 
								NavigateUrl='<%# GetURLPrintNota(Eval("notadeventaID").ToString()) %>'>IMPRIMIR</asp:HyperLink>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateField>
                    <asp:TemplateField HeaderText="Nueva nota de venta" Visible="False">
                        <ItemTemplate>
                            <asp:HyperLink ID="HPLinkNewNota" runat="server" 
                                NavigateUrl='<%# GetURLToOpenNVNueva(Eval("notadeventaID").ToString()) %>'>ABRIR</asp:HyperLink>
                                
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CheckBoxField DataField="NVPagada" HeaderText="Pagada" 
                        SortExpression="NVPagada">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:CheckBoxField>
                </Columns>
            </asp:GridView>
 	    
 	        <asp:SqlDataSource ID="sdsNotasVenta" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                
				
				
                 
                 
                 SelectCommand="SELECT Ciclos.cicloID, Ciclos.CicloName, LTRIM(Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre) AS Productor, Notasdeventa.productorID, Notasdeventa.notadeventaID, Notasdeventa.userID, Notasdeventa.Folio, Notasdeventa.Fecha, Notasdeventa.Pagada, Notasdeventa.Subtotal, Notasdeventa.Iva, Notasdeventa.Total, Notasdeventa.storeTS, Notasdeventa.acredito, Creditos.creditoID, ISNULL(Creditos.pagado, Notasdeventa.Pagada) AS NVPagada FROM Notasdeventa INNER JOIN Ciclos ON Notasdeventa.cicloID = Ciclos.cicloID INNER JOIN Productores ON Notasdeventa.productorID = Productores.productorID LEFT OUTER JOIN Creditos ON Notasdeventa.creditoID = Creditos.creditoID WHERE (Ciclos.cicloID = @cicloID) ORDER BY Notasdeventa.notadeventaID DESC">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlCiclos" DefaultValue="-1" Name="cicloID" 
                        PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>
 	    
 	    </td>
 	</tr>
 </table>
</asp:Content>
