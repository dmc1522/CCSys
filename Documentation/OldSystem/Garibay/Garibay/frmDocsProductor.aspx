<%@ Page Title="Documentos por productor" Language="C#" MasterPageFile="~/MasterPage.Master" Theme="skinrojo" AutoEventWireup="true" CodeBehind="frmDocsProductor.aspx.cs" Inherits="Garibay.frmDocsProductor" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table >
	<tr>
		<td class="TableHeader" colspan="2">DOCUMENTOS POR PRODUCTOR</td>
	</tr>
	<tr><td class="TableHeader">Seleccionar productor:
        </td>
        <td>
        <br />
            <asp:SqlDataSource ID="sdsProductores" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT LTRIM(apaterno + ' ' + amaterno + ' ' + nombre) AS Productor, productorID FROM Productores ORDER BY Productor">
            </asp:SqlDataSource>
            <asp:DropDownList ID="ddlProductor" runat="server" DataSourceID="sdsProductores" 
                    DataTextField="Productor" DataValueField="productorID" AutoPostBack="True" 
                    onselectedindexchanged="ddlProductor_SelectedIndexChanged">
            </asp:DropDownList>
            <cc1:ListSearchExtender ID="ddlProductor_ListSearchExtender" runat="server" 
                Enabled="True" PromptText="" TargetControlID="ddlProductor">
            </cc1:ListSearchExtender>
        </td>
    </tr>
</table>
<table>
	<tr>
		<td>
            <table>
            	<tr>
            		<td align="center" colspan="2">
            		
            		<asp:Panel ID="PanelActionResult" runat="server">
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
            		
            		</td>
            		<td align="center">
            		
            		    &nbsp;</td>
            	</tr>
            	<tr>
            		<td class="TableHeader" align="center" colspan="2">AGREGAR NUEVO DOCUMENTO</td>
                    <td>
                        &nbsp;</td>
            	</tr>
            	<tr>
            		<td class="TablaField">Nombre de Documento:</td><td>
                        <asp:TextBox ID="txtNombreDoc" runat="server" Width="284px"></asp:TextBox>
                    </td>
            	    <td>
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                            ControlToValidate="txtNombreDoc" 
                            ErrorMessage="Nombre de documento es requerido"></asp:RequiredFieldValidator></td>
            	</tr>
            	<tr>
            		<td class="TablaField">Archivo:</td><td>
                        <asp:FileUpload ID="filUpArchivo" runat="server" Width="281px" />
                    </td>
            	    <td>
                        <asp:RequiredFieldValidator ID="rfvFileUpload" runat="server" 
                            ControlToValidate="filUpArchivo" ErrorMessage="Debe de seleccionar un archivo"></asp:RequiredFieldValidator>
                    </td>
            	</tr>
            	<tr>
            		<td align="center" colspan="2">
                        <asp:Button ID="btnAgregar" runat="server" onclick="btnAgregar_Click" 
                            Text="Agregar" />
                    </td>
            		<td align="center">
                        &nbsp;</td>
            	</tr>
            </table>
        </td>
		<td valign="top">
            &nbsp;</td>
	</tr>
	<tr>
		<td class="TableHeader">Documentos del productor</td>
	</tr>
	<tr>
		<td>
            <asp:Button ID="btnEliminar" runat="server" CausesValidation="False" 
                Text="Eliminar" onclick="btnEliminar_Click" />
        </td>
	</tr>
	<tr><td valign="top">
        <asp:SqlDataSource ID="sdsDocumentos" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
            
            
            SelectCommand="SELECT docID, documentName, storeTS, filename, productorID FROM Documents WHERE (productorID = @productorID) ORDER BY documentName" 
            DeleteCommand="select 1;">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlProductor" Name="productorID" 
                    PropertyName="SelectedValue" DefaultValue="-1" />
            </SelectParameters>
            </asp:SqlDataSource>
        <asp:GridView ID="gvDocumentos" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" DataSourceID="sdsDocumentos" 
            DataKeyNames="docID,productorID,filename" 
            onrowdatabound="gvDocumentos_RowDataBound" 
            onrowdeleting="gvDocumentos_RowDeleting">
            <Columns>
                <asp:CommandField ButtonType="Button" DeleteText="Eliminar" 
                    ShowCancelButton="False" ShowDeleteButton="True" />
                <asp:BoundField DataField="documentName" HeaderText="Documento" 
                    SortExpression="documentName" >
                </asp:BoundField>
                <asp:BoundField DataField="filename" HeaderText="filename" 
                    SortExpression="filename" />
                <asp:BoundField DataField="storeTS" 
                    DataFormatString="{0:dd/MM/yyyy hh:mm:ss tt}" HeaderText="Fecha de Ingreso" 
                    SortExpression="storeTS" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkDownload" runat="server">Descargar</asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </td>
	</tr>
</table>
</asp:Content>
