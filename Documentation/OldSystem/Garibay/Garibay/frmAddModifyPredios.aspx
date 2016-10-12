<%@ Page Language="C#" Theme="skinrojo" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmAddModifyPredios.aspx.cs" Inherits="Garibay.frmAddModifyPredios" Title="Predios" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
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
            <tr>
                <td align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnAgregarOtro" runat="server" Text="Agregar Otro Predio" 
                                onclick="btnAceptardtlst_Click" />
                    <asp:Button ID="btnIrALista" runat="server" onclick="btnIrALista_Click" 
                        Text="Ir a Lista de Predios" />
                    <asp:Button ID="btnRregresaraSol" runat="server" 
                        onclick="btnRregresaraSol_Click" Text="Regresar a solicitud" Visible="False" />
                    <asp:TextBox ID="txtPredioID" runat="server" Width="28px" Visible="False"></asp:TextBox>
                </td>
            </tr>
        </table>
    
    </asp:Panel>
    <asp:Panel ID="PanelAgregar" runat="server">
    <table>
    	<tr>
    		<td class="TableHeader" colspan="2">
                <asp:Label ID="lblPredios" runat="server" Text="AGREGAR PREDIO"></asp:Label>
            </td>
    	</tr>
    	<tr>
    		<td class="TablaField">
                <asp:CheckBox ID="chkDatosPropietarioID" runat="server" Text="El Predio Contiene Datos de Propietario"/>
                
            </td>
    	</tr>
    	<tr>
    	<td colspan="2">
    	<div id="divPropietarioPredioID" runat="Server">
    	
    	<table>
    		<tr>
    		<td class="TablaField"> PROPIETARIO:
    	  </td>
    	  <td><br />
              <asp:DropDownList ID="ddlPropietario" runat="server" 
                  DataSourceID="sdsPropietario" DataTextField="Propietario" 
                  DataValueField="productorID">
              </asp:DropDownList>
              <cc1:ListSearchExtender ID="ddlPropietario_ListSearchExtender" runat="server" 
                  Enabled="True" PromptText="" TargetControlID="ddlPropietario">
              </cc1:ListSearchExtender>
              <asp:SqlDataSource ID="sdsPropietario" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                  SelectCommand="SELECT productorID, LTRIM(apaterno + ' ' + amaterno + ' ' + nombre) AS Propietario FROM Productores ORDER BY Propietario">
              </asp:SqlDataSource>
            </td>
    	  
    	</tr>
    	
    	</table>
    	</div>
    		</td>
    		<tr>
    		<td>
              <asp:Button ID="btnAddQuickProductor" runat="server" 
                  Text="Agregar Propietario/Productor" />
              <br />
              <asp:Button ID="btnActualizarListas" runat="server" 
                  onclick="btnActualizarListas_Click" Text="Actualizar Listas" />
            </td>
            </tr>
    	  
        </tr>
        <tr>
            <td class="TablaField">
                FOLIO PROPIETARIO:</td>
            <td>
                <asp:TextBox ID="txtFolioPropietario" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
    		
    		</tr>
    	<tr>
  
            <td class="TablaField">
                PRODUCTOR:</td>
            <td>
            <br />
                <asp:DropDownList ID="cmbProductor" runat="server" 
                    DataSourceID="sdsProductores" DataTextField="Productor" 
                    DataValueField="productorID" Height="22px" Width="368px">
                </asp:DropDownList>
                <cc1:ListSearchExtender ID="cmbProductor_ListSearchExtender" runat="server" 
                    Enabled="True" PromptText="" TargetControlID="cmbProductor">
                </cc1:ListSearchExtender>
                <asp:SqlDataSource ID="sdsProductores" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    
                    SelectCommand="SELECT LTRIM(apaterno + ' ' + amaterno + ' ' + nombre) AS Productor, productorID FROM Productores ORDER BY Productor ASC">
                </asp:SqlDataSource>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    	<tr>
    	 <td class="TablaField">
    	 
    	     FOLIO DE PRODUCTOR:</td>
    	     <td>
                 <asp:TextBox ID="txtFolioProductor" runat="server"></asp:TextBox>
            </td>
    	     <td>
                 &nbsp;</td>
    	</tr>
    	<tr>
            <td class="TablaField">
                DDR:</td>
            <td>
                <asp:TextBox ID="txtDDR" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="TablaField">
                CADER:</td>
            <td>
                <asp:TextBox ID="txtCADER" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="TablaField">
                Nombre del predio:</td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server" Height="22px" Width="200px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    	<tr>
            <td class="TablaField">
                FOLIO DEL PREDIO:</td>
            <td>
                <asp:TextBox ID="txtFolioDelPredio" runat="server" Height="22px" Width="172px"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
    	<tr>
    	  <td class="TablaField"> EJIDO:</td>
    	  <td>
              <asp:TextBox ID="txtEjido" runat="server" Width="200px"></asp:TextBox>
            </td>
    	  <td>
              &nbsp;</td>
    	</tr>
    	<tr>
    	  <td class="TablaField"> CODIGO CULTIVO:</td>
    	  <td>
              <asp:TextBox ID="txtCodigoCultivo" runat="server"></asp:TextBox>
            </td>
    	  <td>&nbsp;</td>
    	</tr>
    	<tr>
            <td class="TablaField">
                CULTIVO:</td>
            <td>
                <asp:DropDownList ID="cmbCultivo" runat="server" DataSourceID="sdsCultivo" 
                    DataTextField="Cultivo" DataValueField="CultivoID" Width="200px">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsCultivo" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT [CultivoID], [Cultivo] FROM [Cultivos]">
                </asp:SqlDataSource>
            </td>
            <td>
            </td>
        </tr>
    	<tr>
    	  <td class="TablaField"> SUPERFICIE:</td>
    	  <td>
              <asp:TextBox ID="txtSuperficie" runat="server" Width="200px"></asp:TextBox>
            </td>
    	  <td>
              &nbsp;</td>
    	</tr>
    	<tr>
            <td class="TablaField">
                Folio PROCAMPO:</td>
            <td>
                <asp:TextBox ID="txtFolioPROCAMPO" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    	<tr>
    	  <td class="TablaField"> REGISTRO ALTERNO:</td>
    	  <td>
              <asp:TextBox ID="txtRegistroAlterno" runat="server" AutoCompleteType="Disabled" 
                  Width="200px"></asp:TextBox>
            </td>
    	  <td>
              &nbsp;</td>
    	</tr>
    	<tr>
    	  <td class="TablaField"> Norte:</td>
    	  <td>
              <asp:TextBox ID="txtNorte" runat="server" Width="200px"></asp:TextBox>
            </td>
    	  <td>
              &nbsp;</td>
    	</tr>
    	<tr>
    	  <td class="TablaField"> Sur:</td>
    	  <td>
              <asp:TextBox ID="txtSur" runat="server" Width="200px"></asp:TextBox>
            </td>
    	  <td>
              &nbsp;</td>
    	</tr>
    	<tr>
    	  <td class="TablaField"> Este:</td>
    	  <td>
              <asp:TextBox ID="txtEste" runat="server" Width="200px"></asp:TextBox>
            </td>
    	  <td>
              &nbsp;</td>
    	</tr>
    	<tr>
    	<td class="TablaField"> Oeste:</td>
    	  <td>
              <asp:TextBox ID="txtOeste" runat="server" Width="200px"></asp:TextBox>
            </td>
    	  <td>
              &nbsp;</td>
    	</tr>
    	<tr>
    	 <td colspan="2" align="center">
             <asp:Button ID="btnAgregarDeForm" runat="server" CssClass="Button" Text="Agregar" 
                 Width="100px" onclick="btnAgregarDeForm_Click" />
             <asp:Button ID="btnModificarDeForm" runat="server" CssClass="Button" 
                 Text="Modificar" Width="100px" onclick="btnModificarDeForm_Click" />
             <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                 CssClass="Button" Text="Cancelar" Width="100px" 
                 onclick="btnCancelar_Click" />
             <asp:TextBox ID="txtPredioIDToMod" runat="server" Visible="False" Width="16px"></asp:TextBox>
             <asp:TextBox ID="txtSolID" runat="server" Visible="False"></asp:TextBox>
            </td>
    	 <td></td>
    	</tr>
    </table>
    </asp:Panel>     
    
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <script type="text/javascript" src="/scripts/divFunctions.js"></script>
</asp:Content>
