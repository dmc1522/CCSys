<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" Title="Agregar/Modificar crédito" AutoEventWireup="true" CodeBehind="frmAddModifyCreditos.aspx.cs" Inherits="Garibay.frmAddModifyCreditos" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
     <asp:UpdatePanel ID="upPanel" runat="Server">
    <ContentTemplate>
    <asp:UpdateProgress id= "upprog" runat="Server" AssociatedUpdatePanelID="upPanel" 
            DisplayAfter="0">
     <ProgressTemplate>
         <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/cargando.gif" />
         Cargando datos...
     </ProgressTemplate>
    
    </asp:UpdateProgress>

         
        &nbsp;<asp:Panel ID="panelmensaje" runat="server" >
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
                    <td align="center" >
                        <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" 
                        Height="50px" Width="486px" DataSourceID="SqlDataSource4" 
                            onpageindexchanging="DetailsView1_PageIndexChanging">
                            <Fields>
                              
                                <asp:BoundField DataField="CicloName" HeaderText="Ciclo:" 
                                    SortExpression="CicloName" />
                                <asp:BoundField DataField="status" HeaderText="Estado:" 
                                    SortExpression="status" />
                                <asp:BoundField DataField="Limitedecredito" 
                                    HeaderText="Límite de crédito:" SortExpression="Limitedecredito" 
                                    DataFormatString="{0:C2}" />
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha:" 
                                    SortExpression="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="zona" 
                                    HeaderText="Zona:" SortExpression="zona" />
                                <asp:BoundField DataField="FechaFinCiclo" HeaderText="Fecha de fin de ciclo:" 
                                    SortExpression="FechaFinCiclo" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="Interesanual" 
                                    HeaderText="Interes anual:" SortExpression="Interesanual" 
                                    DataFormatString="{0:P}" />
                                <asp:BoundField DataField="storeTS" DataFormatString="{0:dd/MM/yyyy hh:mm:ss} " 
                                    HeaderText="Fecha de ingreso:" SortExpression="storeTS" />
                                <asp:BoundField DataField="updateTS" 
                                    DataFormatString="{0:dd/MM/yyyy hh:mm:ss} " 
                                    HeaderText="Fecha de última modificación:" SortExpression="updateTS" />
                            </Fields>
                        </asp:DetailsView>
                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            SelectCommand="SELECT Productores.apaterno + '  ' + Productores.amaterno + ' ' + Productores.nombre as nombrepro, Ciclos.CicloName, CreditoStatus.status, Creditos.Limitedecredito, Creditos.Fecha, Creditos.zona, Creditos.FechaFinCiclo, Creditos.Interesanual, Creditos.storeTS, Creditos.updateTS FROM Creditos INNER JOIN CreditoStatus ON Creditos.statusID = CreditoStatus.statusID INNER JOIN Productores ON Creditos.productorID = Productores.productorID INNER JOIN Ciclos ON Creditos.cicloID = Ciclos.cicloID WHERE (Creditos.creditoID = @creditoID)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txtIDdetails" DefaultValue="-1" 
                                    Name="creditoID" PropertyName="Text" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Button ID="btnAceptardetails" runat="server" CssClass="Button" 
                        Text="Aceptar" Width="135px" onclick="btnAceptardetails_Click" />
                        <asp:TextBox ID="txtIDdetails" runat="server" Visible="False"></asp:TextBox>
                    </td>
                </tr>
            </table>
    </asp:Panel>
    <asp:Panel ID="panelagregar" runat="server">
        <table >
            <tr>
                <td colspan="2" class="TableHeader">
                    <asp:Label ID="lblTitle" runat="server" Text="AGREGAR UN NUEVO CRÉDITO"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Fecha:</td>
                <td>
                    <asp:TextBox ID="txtFecha" runat="server" ReadOnly="True"></asp:TextBox>
                    <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtFecha" 
                        Separator="/" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Productor:</td>
                <td>
                    <asp:DropDownList ID="cmbProductor" runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="nombrepro" 
                        DataValueField="productorID" Height="23px" Width="252px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT productorID, Productores.apaterno + ' ' +  Productores.amaterno  +  ' ' + Productores.nombre AS nombrepro FROM Productores order by apaterno">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Ciclo:</td>
                <td>
                    <asp:DropDownList ID="cmbCiclo" runat="server" Height="23px" Width="251px" 
                        DataSourceID="SqlDataSource2" DataTextField="CicloName" 
                        DataValueField="cicloID" AutoPostBack="True" 
                        onselectedindexchanged="cmbCiclo_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
						SelectCommand="SELECT        cicloID, CicloName
FROM            Ciclos
WHERE cerrado=@cerrado
ORDER BY fechaInicio">
                    	<SelectParameters>
							<asp:Parameter DefaultValue="FALSE" Name="cerrado" Type="Boolean" />
						</SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Estado:</td>
                <td>
                    <asp:DropDownList ID="cmbStatus" runat="server" DataSourceID="SqlDataSource3" 
                        DataTextField="status" DataValueField="statusID" Height="23px" 
                        Width="253px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT [statusID], [status] FROM [CreditoStatus]">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Zona:</td>
                <td>
                    <asp:DropDownList ID="cmbZona" runat="server" Height="23px" Width="150px" 
                        onselectedindexchanged="cmbZona_SelectedIndexChanged" AutoPostBack="True" 
                        >
                        <asp:ListItem Value="1">ZONA 1</asp:ListItem>
                        <asp:ListItem Value="2">ZONA 2</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Fecha de límite:</td>
                <td>
                    <asp:TextBox ID="txtFechaFinCredito" runat="server" ReadOnly="True" 
                        Enabled="False"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="valfecha0" runat="server" 
                        ControlToValidate="txtFecha" ErrorMessage="El campo fecha es necesario"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Estado del crédito:</td>
                <td>
                    <asp:DropDownList ID="dprdlStatus" runat="server" 
                        DataSourceID="sdsStatusCreditos" DataTextField="status" 
                        DataValueField="statusID" Height="23px" Width="230px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsStatusCreditos" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT [statusID], [status] FROM [CreditoStatus]">
                    </asp:SqlDataSource>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Límite de crédito:</td>
                <td>
                    <asp:TextBox ID="txtLimite" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:CompareValidator ID="cmpLimite" runat="server" 
                        ControlToValidate="txtLimite" 
                        ErrorMessage="Límite de crédito debe ser válido" Type="Double"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Interés anual:</td>
                <td>
                    <asp:TextBox ID="txtInteresAnual" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:CompareValidator ID="valcmpInteres0" runat="server" 
                        ControlToValidate="txtInteresAnual" 
                        ErrorMessage="Interés anual debe ser una cantidad válida" Type="Double"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="TableField" colspan="2">
                    <asp:Button ID="btnAceptar" runat="server" 
                        Text="Agregar" Width="116px" onclick="btnAceptar_Click" />
                    <asp:Button ID="btnModificar" runat="server" 
                        Text="Modificar" Width="114px" onclick="btnModificar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                        Text="Cancelar" Width="108px" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
  </ContentTemplate>
 </asp:UpdatePanel> 
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

   

    <style type="text/css">
        .TableHeader
        {
            text-align: center;
        }
    </style>

 

</asp:Content>

