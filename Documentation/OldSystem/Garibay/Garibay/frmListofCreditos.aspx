<%@ Page Language="C#" Title="Lista de créditos" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListofCreditos.aspx.cs" Inherits="Garibay.ListCreditos" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
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
                    LISTA DE CRÉDITOS</td>
            </tr>
            <tr>
                <td>
                <table >
                	<tr>
                		<td colspan="2" class="TableHeader">Filtros:</td>
                	</tr>
                	<tr>
                	<td class="TablaField">Ciclo:</td> <td>
                        <asp:DropDownList ID="cmbCiclo" runat="server" DataSourceID="DataSourceCiclo" 
                            DataTextField="CicloName" DataValueField="cicloID" Height="23px" 
                            Width="171px" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="DataSourceCiclo" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [fechaInicio] DESC">
                        </asp:SqlDataSource>
                    </td>
                	</tr>
                	<tr>
                	<td class="TablaField">
                        <asp:CheckBox ID="chkProductor" runat="server" Text="Productor:" />
                        </td> <td>
                            <asp:DropDownList ID="cmbProductor" runat="server" 
                                DataSourceID="DataSourceProductor" DataTextField="nompro" 
                                DataValueField="productorID" Height="24px" Width="292px">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="DataSourceProductor" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                
                                SelectCommand="SELECT Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS nompro, Productores.productorID FROM Productores INNER JOIN Creditos ON Productores.productorID = Creditos.productorID WHERE (Creditos.cicloID = @cicloID) ORDER BY nompro">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="cmbCiclo" DefaultValue="-1" Name="cicloID" 
                                        PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                    </td>
                	</tr>
                	<tr>
                        <td class="TablaField">
                            <asp:CheckBox ID="chkStatus" runat="server" Text="Estado:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbEstado" runat="server" DataSourceID="DataSourceStatus" 
                                DataTextField="status" DataValueField="statusID" Height="24px" 
                                Width="292px">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="DataSourceStatus" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                SelectCommand="SELECT [statusID], [status] FROM [CreditoStatus]">
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                	<tr>
                        <td class="TablaField">
                            Mostrar:</td>
                        <td>
                            <asp:CheckBoxList ID="cblColToShow" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>Zona</asp:ListItem>
                                <asp:ListItem>Usuario que lo agregó</asp:ListItem>
                                <asp:ListItem>Fecha de ingreso</asp:ListItem>
                                <asp:ListItem Value="Fecha de modificación">Fecha de modificación</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                	<tr>
                	<td colspan="2">
                        <asp:Button ID="btnFiltrar" runat="server"  
                            Text="Filtrar" onclick="btnFiltrar_Click" />
                    <asp:Button ID="btnAgregarNuevo" runat="server" 
                         Text="Agregar" onclick="btnAgregarNuevo_Click" />
                        <asp:Button ID="btnGenerar" runat="server" onclick="btnGenerar_Click" 
                            Text="Generar Reporte" />
                  <asp:Button ID="btnExport" runat="server" 
                            Text="Exportar A Excel" />

        
        

                        <asp:Button ID="btnEliminar" runat="server" onclick="btnEliminar_Click" 
                            Text="Eliminar" />

        
        

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
                            <asp:GridView ID="gridCreditos" runat="server" AutoGenerateColumns="False" CellPadding="4" 
                                DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="None" 
                                PageSize="100" DataKeyNames="creditoID" style="margin-right: 0px" 
                                onrowdatabound="gridCreditos_RowDataBound" 
                                onselectedindexchanged="gridCreditos_SelectedIndexChanged1">
                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                <HeaderStyle CssClass="TableHeader" />
                                <AlternatingRowStyle BackColor="White" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                <Columns>
                                    <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                                        ShowSelectButton="True" />
                                    <asp:BoundField DataField="creditoID" HeaderText="# credito" 
                                        InsertVisible="False" ReadOnly="True" SortExpression="creditoID" />
                                    <asp:BoundField DataField="prodname" HeaderText="Productor" 
                                        ItemStyle-HorizontalAlign="Left" ReadOnly="True" SortExpression="prodname">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                        HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" 
                                        SortExpression="Fecha">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Limitedecredito" 
                                        HeaderText="Límite de crédito" SortExpression="Limitedecredito" 
                                        DataFormatString="{0:c}" >
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Interesanual" 
                                        HeaderText="Interes anual" SortExpression="Interesanual" 
                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:P2}" >
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="status" HeaderText="Estado" 
                                        SortExpression="status" />
                                    <asp:CheckBoxField DataField="credActivo" HeaderText="Activo" 
                                        SortExpression="credActivo" />
                                    <asp:BoundField DataField="zona" HeaderText="Zona" SortExpression="zona" />
                                    <asp:BoundField DataField="userID" HeaderText="Usuario que lo agregó" 
                                        SortExpression="userID" />
                                    <asp:BoundField DataField="storeTS" DataFormatString="{0: dd/MM/yyyy hh:mm:ss}" 
                                        HeaderText="Fecha de ingreso" SortExpression="storeTS" />
                                    <asp:BoundField DataField="updateTS" 
                                        DataFormatString="{0: dd/MM/yyyy hh:mm:ss}" HeaderText="Fecha de modificación" 
                                        SortExpression="updateTS" />
                                    <asp:BoundField DataField="productorID" HeaderText="productorID" 
                                        SortExpression="productorID" Visible="False" />
                                    <asp:BoundField DataField="statusID" HeaderText="statusID" 
                                        SortExpression="statusID" Visible="False" />
                                    <asp:TemplateField HeaderText="Abrir">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server">HyperLink</asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Estado de Cuenta">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HPEstadodeCuenta" runat="server">HyperLink</asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
                        
                        
                        
                        
                        
                        SelectCommand="SELECT Creditos.creditoID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS prodname, Creditos.Fecha, Creditos.FechaFinCiclo, Creditos.Limitedecredito, Creditos.Interesanual, CreditoStatus.status, Creditos.zona, Creditos.userID, Creditos.storeTS, Creditos.updateTS, Creditos.productorID, Creditos.statusID, CreditoStatus.activo AS credActivo FROM Creditos INNER JOIN CreditoStatus ON Creditos.statusID = CreditoStatus.statusID INNER JOIN Productores ON Creditos.productorID = Productores.productorID WHERE (Creditos.cicloID = @cicloID) ORDER BY prodname">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="cmbCiclo" DefaultValue="-1" Name="cicloID" 
                                PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
     
                	</tr>
                </table>
                
</asp:Panel>
</ContentTemplate>
 </asp:UpdatePanel> 
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

   
 

    <style type="text/css">
        .TablaField
        {
            text-align: center;
        }
    </style>

   
 

</asp:Content>
