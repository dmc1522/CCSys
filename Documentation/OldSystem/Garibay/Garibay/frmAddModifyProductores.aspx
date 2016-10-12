<%@ Page Language="C#" Theme = "skinrojo" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmAddModifyProductores.aspx.cs" Inherits="Garibay.frmProductoresaspx" Title="Productores" %>
 
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
 
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

    <script type="text/javascript" src="/scripts/divFunctions.js"></script>
    <asp:UpdatePanel runat="Server" ID="mainpanelupdate">
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
            <tr>
                <td align="center" >
                    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" 
                        DataSourceID="SqlDataSource4" Height="50px" Width="486px">
                        <Fields>
                            <asp:BoundField DataField="productorID" HeaderText="# Productor" 
                                InsertVisible="False" ReadOnly="True" SortExpression="productorID" />
                            <asp:BoundField DataField="apaterno" HeaderText="A. Paterno" 
                                SortExpression="apaterno" />
                            <asp:BoundField DataField="amaterno" HeaderText="A. Materno" 
                                SortExpression="amaterno" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre(s)" 
                                SortExpression="nombre" />
                            <asp:BoundField DataField="fechanacimiento" DataFormatString="{0:dd/MM/yyyy}" 
                                HeaderText="F. de Nacimiento" SortExpression="fechanacimiento" />
                            <asp:BoundField DataField="IFE" HeaderText="IFE" SortExpression="IFE" />
                            <asp:BoundField DataField="CURP" HeaderText="CURP" SortExpression="CURP" />
                            <asp:BoundField DataField="domicilio" HeaderText="Domicilio" 
                                SortExpression="domicilio" />
                            <asp:BoundField DataField="poblacion" HeaderText="Poblacion" 
                                SortExpression="poblacion" />
                            <asp:BoundField DataField="municipio" HeaderText="Municipio" 
                                SortExpression="municipio" />
                            <asp:BoundField DataField="estado" HeaderText="Estado" 
                                SortExpression="estado" />
                            <asp:BoundField DataField="CP" HeaderText="CP" SortExpression="CP" />
                            <asp:BoundField DataField="RFC" HeaderText="RFC" SortExpression="RFC" />
                            <asp:BoundField DataField="sexo" HeaderText="Sexo" SortExpression="sexo" />
                            <asp:BoundField DataField="telefono" HeaderText="Teléfono" 
                                SortExpression="telefono" />
                            <asp:BoundField DataField="telefonotrabajo" HeaderText="T. Trabajo" 
                                SortExpression="telefonotrabajo" />
                            <asp:BoundField DataField="celular" HeaderText="Celular" 
                                SortExpression="celular" />
                            <asp:BoundField DataField="fax" HeaderText="Fax" SortExpression="fax" />
                            <asp:BoundField DataField="email" HeaderText="C. Electrónico" 
                                SortExpression="email" />
                            <asp:BoundField DataField="EstadoCivil" HeaderText="Estado civil" 
                                SortExpression="EstadoCivil" />
                            <asp:BoundField DataField="Regimen" HeaderText="Régimen" 
                                SortExpression="Regimen" />
                            <asp:BoundField DataField="codigoBoletasFile" 
                                HeaderText="Codigo en Archivo de Boletas:" SortExpression="codigoBoletasFile" />
                            <asp:BoundField DataField="storeTS" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" 
                                HeaderText="Fecha de Ingreso" SortExpression="storeTS" />
                            <asp:BoundField DataField="updateTS" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" 
                                HeaderText="Ultima Modificación" SortExpression="updateTS" />
                        	<asp:BoundField DataField="colonia" HeaderText="Colonia" />
							<asp:BoundField DataField="conyugue" HeaderText="Conyugue" />
                        </Fields>
                    </asp:DetailsView>
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
                        
                        
                        
                        
						SelectCommand="SELECT Productores.productorID, Productores.apaterno, Productores.amaterno, Productores.nombre, Productores.fechanacimiento, Productores.IFE, Productores.CURP, Productores.domicilio, Productores.poblacion, Productores.municipio, Estados.estado, Productores.CP, Productores.RFC, Sexo.sexo, Productores.telefono, Productores.telefonotrabajo, Productores.celular, Productores.fax, Productores.email, EstadosCiviles.EstadoCivil, Regimenes.Regimen, Productores.storeTS, Productores.updateTS, Productores.codigoBoletasFile , Productores.colonia, Productores.conyugue FROM Productores INNER JOIN Sexo ON Productores.sexoID = Sexo.sexoID INNER JOIN Regimenes ON Productores.regimenID = Regimenes.regimenID INNER JOIN Estados ON Productores.estadoID = Estados.estadoID INNER JOIN EstadosCiviles ON Productores.estadocivilID = EstadosCiviles.estadoCivilID WHERE (Productores.productorID = @productorID)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtIDdetails" DefaultValue="-1" Name="productorID" 
                                PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnAceptardetails" runat="server" CssClass="Button" 
                        onclick="btnAceptardetails_Click" Text="Aceptar" Width="135px" />
                </td>
            </tr>
        </table>

        
      
        </asp:Panel>

        
        <asp:Panel ID="paneldatosocultos" runat="server" Visible="False">
            <asp:TextBox ID="txtIDdetails" runat="server" 
    Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtIdtomodify" runat="server" Visible="False"></asp:TextBox>
     </asp:Panel>
    
  <asp:Panel ID="panelagregar" runat="server"> 
    <table>
        <tr>
            <td class="TableHeader" colspan="2">
                <asp:Label ID="lblHeader" runat="server" Text="AGREGAR NUEVO PRODUCTOR"></asp:Label>
                <asp:Label ID="lblNombreaModificar" runat="server" Text="NOMBREAMODIFICAR"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                Apellido paterno:</td>
            <td >
                <asp:TextBox ID="txtApaterno" runat="server" Width="302px" MaxLength="50"></asp:TextBox>
            </td>
            <td >
                <asp:RequiredFieldValidator ID="valApaterno" runat="server" 
                    ControlToValidate="txtApaterno" 
                    ErrorMessage="El campo apellido paterno es necesario"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                Apellido materno:</td>
            <td >
                <asp:TextBox ID="txtAMaterno" runat="server" Width="302px" MaxLength="50"></asp:TextBox>
            </td>
            <td >
                &nbsp;</td>
        </tr>
        <tr>
            <td class="TablaField" >
                Nombre(s):</td>
            <td >
                <asp:TextBox ID="txtNombres" runat="server" Width="303px" MaxLength="50" 
                    AutoPostBack="True" ontextchanged="txtNombres_TextChanged"></asp:TextBox>
            </td>
            <td >
                <asp:RequiredFieldValidator ID="valNombres" runat="server" 
                    ControlToValidate="txtNombres" 
                    ErrorMessage="El campo nombre(s) es necesario"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                Fecha de nacimiento:</td>
            <td >
                <asp:TextBox ID="txtFechanacimiento" runat="server" Width="137px" 
                    ReadOnly="True"></asp:TextBox>
                <rjs:PopCalendar ID="PopCalendar1" runat="server" 
                    OnSelectionChanged = "PopCalendar1_SelectionChanged" Separator="/" 
                    Control="txtFechanacimiento"  />
              
            </td>
            <td align="left" rowspan="7" valign="top" >
                <br />
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                IFE:</td>
            <td >
                <asp:TextBox ID="txtIfe" runat="server" Width="302px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                CURP:</td>
            <td >
                <asp:TextBox ID="txtCurp" runat="server" Width="302px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                RFC:</td>
            <td >
                <asp:TextBox ID="txtRfc" runat="server" Width="303px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                Colonia:</td>
            <td >
                <asp:TextBox ID="txtColonia" runat="server" Width="303px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                Domicilio:</td>
            <td >
                <asp:TextBox ID="txtDomicilio" runat="server" Width="302px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                Población:</td>
            <td >
                <asp:TextBox ID="txtPoblacion" runat="server" Width="302px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                Municipio:</td>
            <td >
                <asp:TextBox ID="txtMunicipio" runat="server" Width="302px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                Estado:</td>
            <td >
                <asp:DropDownList ID="cmbEstado" runat="server" Height="22px" Width="302px" 
                    DataSourceID="SqlDataSource1" DataTextField="estado" 
                    DataValueField="estadoID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
                    SelectCommand="SELECT [estadoID], [estado] FROM [Estados]">
                </asp:SqlDataSource>
            </td>
            <td >
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                Código Postal:</td>
            <td >
                <asp:TextBox ID="txtCodigopostal" runat="server" Width="131px"></asp:TextBox>
            </td>
            <td >
                <br />
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                Sexo:</td>
            <td >
                <asp:RadioButtonList ID="rdSex" runat="server">
                    <asp:ListItem Value="2" Selected="True">MASCULINO</asp:ListItem>
                    <asp:ListItem Value="1">FEMENINO</asp:ListItem>
                </asp:RadioButtonList>
                                </td>
            <td >
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                Teléfono:</td>
            <td>
                <asp:TextBox ID="txtTelefono" runat="server" Width="150px" MaxLength="50"></asp:TextBox>
            </td>
            <td >
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                Teléfono de trabajo:</td>
            <td >
                <asp:TextBox ID="txtTelefonotrabajo" runat="server" Width="150px" 
                    MaxLength="50"></asp:TextBox>
            </td>
            <td >
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                Celular:</td>
            <td >
                <asp:TextBox ID="txtCelular" runat="server" Width="150px" MaxLength="50"></asp:TextBox>
            </td>
            <td >
                
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                Fax:</td>
            <td >
                <asp:TextBox ID="txtFax" runat="server" Width="151px" MaxLength="50"></asp:TextBox>
            </td>
            <td >
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                Correo electrónico:</td>
            <td >
                <asp:TextBox ID="txtCorreo" runat="server" Width="302px" MaxLength="50"></asp:TextBox>
            </td>
            <td >
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                    ControlToValidate="txtCorreo" ErrorMessage="Escriba una dirección de correo válida." 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                Estado Civil:</td>
            <td >
                <asp:DropDownList ID="cmbEstadoCivil" runat="server" Height="22px" 
                    Width="201px" DataSourceID="SqlDataSource3" DataTextField="EstadoCivil" 
                    DataValueField="estadoCivilID">
                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
                    SelectCommand="SELECT [estadoCivilID], [EstadoCivil] FROM [EstadosCiviles]">
                </asp:SqlDataSource>
                                </td>
            <td >
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                Régimen:</td>
            <td >
                <asp:DropDownList ID="cmbRegimen" runat="server" Height="22px" Width="202px" 
                    DataSourceID="SqlDataSource2" DataTextField="Regimen" 
                    DataValueField="regimenID">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
                    SelectCommand="SELECT [regimenID], [Regimen] FROM [Regimenes]">
                </asp:SqlDataSource>
            </td>
            <td >
            </td>
        </tr>
        <tr>
            <td class="TablaField" >
                Conyugue:</td>
            <td >
                <asp:TextBox ID="txtConyugue" runat="server" Width="303px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="TablaField">
                Codigo en Archivo de Boletas:</td>
            <td>
                <asp:TextBox ID="txtCodigoBoletasFile" runat="server"></asp:TextBox>
            </td>
            <td class="style3">
                </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Label ID="lblProductoresParecidos" runat="server" 
                    Text="NOMBRES PARECIDOS QUE YA ESTAN EN EL SISTEMA:"></asp:Label>
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="sdsProductoresRepetidos" Visible="False">
                    <Columns>
                        <asp:BoundField DataField="apaterno" HeaderText="apaterno" 
                            SortExpression="apaterno" />
                        <asp:BoundField DataField="amaterno" HeaderText="amaterno" 
                            SortExpression="amaterno" />
                        <asp:BoundField DataField="nombre" HeaderText="nombre" 
                            SortExpression="nombre" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsProductoresRepetidos" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT [apaterno], [amaterno], [nombre] FROM [Productores] WHERE (([apaterno] LIKE '%' + @apaterno + '%') AND ([amaterno] LIKE '%' + @amaterno + '%') AND ([nombre] LIKE '%' + @nombre + '%')) ORDER BY [apaterno], [amaterno], [nombre]">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtApaterno" Name="apaterno" 
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="txtAMaterno" Name="amaterno" 
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="txtNombres" Name="nombre" PropertyName="Text" 
                            Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
                <asp:Label ID="lblProductorYaExiste" runat="server" 
                    Text="YA EXISTE UN PRODUCTOR CON EL MISMO NOMBRE EN EL SISTEMA" 
                    Font-Bold="True" Font-Size="X-Large" Visible="False"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" >
                <asp:Button ID="btnValidar" runat="server" onclick="btnValidar_Click" 
                    Text="Validar Productor" />
                <asp:Button ID="btnAceptar" runat="server" Text="Agregar" 
                    Width="135px" onclick="btnAceptar_Click" Visible="False" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" 
                    Width="135px" onclick="btnModificar_Click" />
		<asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                    Text="Cancelar" Width="147px" 
                    onclick="btnCancelar_Click" />
                </td>
            <td >
                </td>
        </tr>
    </table>
    <p>
        </p>


   
     </table>
</asp:Panel>
  

     
  
  

</ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <style type="text/css">
		.style3
		{
			height: 13px;
		}
	</style>

</asp:Content>

