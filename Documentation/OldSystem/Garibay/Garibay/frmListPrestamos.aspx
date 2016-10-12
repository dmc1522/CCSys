<%@ Page Title="Lista de préstamos" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListPrestamos.aspx.cs" Inherits="Garibay.frmListPrestamos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:UpdatePanel ID="upPanel" runat="Server">
    <ContentTemplate>
    <asp:UpdateProgress id= "upprog" runat="Server" AssociatedUpdatePanelID="upPanel" 
            DisplayAfter="0">
     <ProgressTemplate>
         <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/cargando.gif" />
         Cargando datos...
     </ProgressTemplate>
    
    </asp:UpdateProgress>
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
                        Text="Aceptar" onclick="btnAceptarMensaje_Click" />
                </td>
            </tr>
        </table>
</asp:Panel>
<asp:Panel ID="panelagregar" runat="server" > 

         
        <table>
            <tr>
                <td class="TableHeader">
                    LISTA DE PRESTAMOS</td>
            </tr>
            <tr>
                <td>
                <table >
                	<tr>
                		<td colspan="2" class="TableHeader">Filtros:</td>
                	</tr>
                	<tr>
                	<td class="TablaField">Ciclo:</td> <td>
                        <asp:DropDownList ID="ddlCiclos" runat="server" DataSourceID="sqlCiclos" 
                            DataTextField="CicloName" DataValueField="cicloID" AutoPostBack="True" 
                            onselectedindexchanged="ddlCiclos_SelectedIndexChanged1">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sqlCiclos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            SelectCommand="SELECT cicloID, CicloName FROM Ciclos WHERE (cerrado = 0) ORDER BY fechaInicio DESC">
                        </asp:SqlDataSource>
                    </td>
                	</tr>
                	<tr>
                	<td class="TablaField">
                        <asp:CheckBox ID="chkCredito" runat="server" Text="Credito:" />
                        </td> <td>
                            <asp:DropDownList ID="ddlCredito" runat="server" 
                                DataSourceID="sqlCredito" DataTextField="nompro" 
                                DataValueField="creditoID" Height="24px" Width="292px">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="sqlCredito" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                
                                
                                
                                
                                
                                SelectCommand="SELECT LTRIM(Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre) +' - ' + cast(Creditos.creditoID as varchar) AS nompro, Creditos.creditoID FROM Productores INNER JOIN Creditos ON Productores.productorID = Creditos.productorID  ORDER BY nompro">
                            </asp:SqlDataSource>
                    </td>
                	</tr>
                	
                	<tr>
                        <td class="TablaField">
                            Mostrar:</td>
                        <td>
                            <asp:CheckBoxList ID="cblColToShow" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>Usuario que lo agregó</asp:ListItem>
                                <asp:ListItem>Fecha de ingreso</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                	<tr>
                	<td colspan="2">
                        <asp:Button ID="btnFiltrar" runat="server"  
                            Text="Filtrar" onclick="btnFiltrar_Click" />
                    <asp:Button ID="btnAgregarNuevo" runat="server" 
                         Text="Agregar" onclick="btnAgregarNuevo_Click" />

        
        

                        <asp:Button ID="btnEliminar" runat="server" onclick="btnEliminar_Click" 
                            Text="Eliminar" Height="26px" />

        
        

                        </td> 
                	</tr>
                	<tr>
                	<td colspan="2">
                        &nbsp;</td> 
                	</tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="gdvPrestamos" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="anticipoID,creditoID,fecha" DataSourceID="sqlPrestamos" 
                                onrowdatabound="gdvPrestamos_RowDataBound" 
                                onselectedindexchanged="gdvPrestamos_SelectedIndexChanged" 
                                onrowdeleted="gdvPrestamos_RowDeleted" 
								onrowdeleting="gdvPrestamos_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Eliminar" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" 
                                                CommandName="Delete" Text="Eliminar"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="nombre" HeaderText="Productor" ReadOnly="True" 
                                        SortExpression="nombre" />
                                    <asp:BoundField DataField="anticipoID" HeaderText="# Prestamo" 
                                        InsertVisible="False" ReadOnly="True" SortExpression="anticipoID" />
                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" 
                                        DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="interesAnual" HeaderText="Interes Anual" 
                                        SortExpression="interesAnual" DataFormatString="{0:P2}">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    <asp:BoundField DataField="interesMoratorio" HeaderText="Interes Moratorio" 
                                        SortExpression="interesMoratorio" DataFormatString="{0:P2}">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        
                                    <asp:BoundField DataField="fechaLimitePagoPrestamo" 
                                        HeaderText="Fecha Límite de Pago" SortExpression="fechaLimitePagoPrestamo" 
                                        DataFormatString="{0:dd/MM/yyyy}"/>
                                    <asp:BoundField DataField="storeTS" HeaderText="Fecha de ingreso" 
                                        SortExpression="storeTS" Visible="False" DataFormatString="{0:dd/MM/yyyy}"/>
                                    <asp:BoundField DataField="userID" HeaderText="Usuario que lo agregó" 
                                        SortExpression="userID" Visible="False" />
                                    <asp:BoundField DataField="monto" DataFormatString="{0:c}" HeaderText="Monto" 
                                        SortExpression="monto">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Abrir">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="linkAbrir" runat="server">Abrir</asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                	<asp:TemplateField HeaderText="Pagaré" Visible="False">
										<ItemTemplate>
											<asp:HyperLink ID="lnkPagare" runat="server" NavigateUrl="frmListPrestamos.aspx">Pagare</asp:HyperLink>
										</ItemTemplate>
									</asp:TemplateField>
                                	<asp:BoundField DataField="creditoID" HeaderText="Credito" />
                                    <asp:CheckBoxField DataField="esCarteraVencida" HeaderText="Cartera vencida" 
                                        SortExpression="esCarteraVencida" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:SqlDataSource ID="sqlPrestamos" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                SelectCommand="SELECT Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS nombre, Anticipos.anticipoID, Anticipos.tipoAnticipoID, Anticipos.cicloID, Anticipos.productorID, Anticipos.fecha, Anticipos.movbanID, Anticipos.movimientoID, Anticipos.interesAnual, Anticipos.interesMoratorio, Anticipos.fechaLimitePagoPrestamo, Anticipos.userID, Anticipos.storeTS, Anticipos.monto, credito_prestamo.creditoID, Anticipos.esCarteraVencida FROM credito_prestamo INNER JOIN Creditos ON credito_prestamo.creditoID = Creditos.creditoID INNER JOIN Productores ON Creditos.productorID = Productores.productorID INNER JOIN Anticipos ON credito_prestamo.anticipoID = Anticipos.anticipoID WHERE (Anticipos.tipoAnticipoID = 2) AND (Anticipos.cicloID = @cicloID) ORDER BY nombre" 
                                DeleteCommand="DELETE FROM Anticipos WHERE anticipoID = @anticipoID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlCiclos" DefaultValue="-1" Name="cicloID" 
                                        PropertyName="SelectedValue" />
                                </SelectParameters>
                                <DeleteParameters>
                                    <asp:Parameter Name="anticipoID" />
                                </DeleteParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                         
                	</tr>
                </table>
                
</asp:Panel>
</ContentTemplate>
 </asp:UpdatePanel> 

</asp:Content>