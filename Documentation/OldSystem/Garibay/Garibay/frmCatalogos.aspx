<%@ Page Language="C#" Theme="skinverde" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmCatalogos.aspx.cs" Inherits="Garibay.WebForm14" Title="Untitled Page" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
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
    <asp:Panel id="panelUnauthorized" runat="Server">
        <asp:CheckBox ID="checkBoxShowFormSendAuth" runat="server" 
            Text="USTED NO TIENE PRIVILEGIOS PARA ADMINISTRAR LOS CATALOGOS, marque para solicitar que se agregue. modifique o elimine algún catálogo" />
        <asp:Panel ID="panelFormSendAuth" runat="server">
            <table>
                <tr>
                    <td class="TablaField">
                        Catálogo</td>
                    <td>
                        <asp:TextBox ID="textBoxCatalogo" runat="server" Width="171px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TablaField" colspan="2">
                        Acción a realizar (seleccionar sólo una opción):<asp:CheckBoxList 
                            ID="checkBoxListAction" runat="server">
                            <asp:ListItem Selected="True">AGREGAR</asp:ListItem>
                            <asp:ListItem>MODIFICAR</asp:ListItem>
                            <asp:ListItem>ELIMINAR</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="TablaField">
                        Nuevo nombre (si quiere modificar):</td>
                    <td>
                        <asp:TextBox ID="textBoxNuevoCatalogo" runat="server" Width="173px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Label ID="lblAddResult" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="buttonSend" runat="server" onclick="buttonSend_Click" 
                            Text="Enviar" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <cc1:CollapsiblePanelExtender ID="panelFormSendAuth_CollapsiblePanelExtender" 
            runat="server" CollapseControlID="checkBoxShowFormSendAuth" Collapsed="True" 
            Enabled="True" ExpandControlID="checkBoxShowFormSendAuth" 
            TargetControlID="panelFormSendAuth">
        </cc1:CollapsiblePanelExtender>
    
    
    </asp:Panel>
    <asp:Panel ID="panelCatalogo" runat="server">
    
    <table>
    <tr>
    	<td class="TableHeader" colspan="2">
    CATÁLOGOS
    </td>
    </tr>
    <tr>
    
    
    
    
    		<td class="TableHeader">
    		    Grupo:</td>
    	    <td class="TableHeader">
                <asp:DropDownList ID="drpDlGrupo" runat="server" AutoPostBack="True" 
                    DataSourceID="sdsGrupos" DataTextField="grupoCatalogo" 
                    DataValueField="grupoCatalogosID" Height="23px" 
                    onselectedindexchanged="drpDlGrupo_SelectedIndexChanged" Width="246px">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsGrupos" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos]">
                </asp:SqlDataSource>
            </td>
    	</tr>
    	
    
    <tr>
    
                    <td colspan="2">
                        <asp:Button ID="btnAgregarListaCatalogo" runat="server" 
                            onclick="btnAgregarListaCatalogo_Click" Text="Agregar" />
                        <asp:Button ID="btnModificarListaCatalogo" runat="server" 
                            onclick="btnModificarListaCatalogo_Click" Text="Modificar" />
                        <asp:Button ID="btnEliminarCatalogo" runat="server" CausesValidation="False" 
                            onclick="btnEliminarCatalogo_Click" Text="Eliminar" />
                        <asp:Button ID="btnVerSubCatalogo" runat="server" CausesValidation="False" 
                            onclick="btnVerSubCatalogo_Click" Text="Ver Sub-Catálogo" />
                    </td>
                </tr>
    <tr>
    <td class="TablaField" colspan="2">
        <table>
            <tr>
                <td>
                    ELEMENTOS POR PÁGINA:
                </td>
                <td>
                    <asp:DropDownList ID="ddlElemXPage" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlElemXPage_SelectedIndexChanged">
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem Selected="True">100</asp:ListItem>
                        <asp:ListItem>200</asp:ListItem>
                        <asp:ListItem>500</asp:ListItem>
                        <asp:ListItem>1000</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </td>
    </tr>
    
        <tr>
            <td colspan="2">
                <asp:GridView ID="GrdvCatalogos" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" 
                    DataKeyNames="catalogoMovBancoID,grupoCatalogoID,claveCatalogo,catalogoMovBanco,grupoCatalogo" 
                    DataSourceID="SqlCatalogos" 
                    onselectedindexchanged="GrdvCatalogos_SelectedIndexChanged1">
                    <Columns>
                        <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                            ShowSelectButton="True" />
                        <asp:BoundField DataField="catalogoMovBanco" 
                            HeaderText="Catalogo de Movimiento de Banco" 
                            SortExpression="catalogoMovBanco" />
                        <asp:BoundField DataField="catalogoMovBancoID" HeaderText="CatalogoMovBancoID" 
                            ReadOnly="True" SortExpression="catalogoMovBancoID" Visible="False" />
                        <asp:BoundField DataField="grupoCatalogoID" HeaderText="grupoCatalogoID" 
                            SortExpression="grupoCatalogoID" Visible="False" />
                        <asp:BoundField DataField="claveCatalogo" HeaderText="Clave" 
                            SortExpression="claveCatalogo" />
                        <asp:BoundField DataField="grupoCatalogo" HeaderText="Grupo" 
                            SortExpression="grupoCatalogo" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlCatalogos" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT catalogoMovimientosBancos.catalogoMovBancoID, catalogoMovimientosBancos.grupoCatalogoID, catalogoMovimientosBancos.claveCatalogo, catalogoMovimientosBancos.catalogoMovBanco, GruposCatalogosMovBancos.grupoCatalogo FROM catalogoMovimientosBancos INNER JOIN GruposCatalogosMovBancos ON catalogoMovimientosBancos.grupoCatalogoID = GruposCatalogosMovBancos.grupoCatalogosID where catalogoMovimientosBancos.grupoCatalogoID =  @group">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="drpDlGrupo" DefaultValue="-1" Name="group" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlGrupoCatálogos" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT * FROM [GruposCatalogosMovBancos]">
                </asp:SqlDataSource>
            </td>
        </tr>
    
    </table>
    </asp:Panel>

    <asp:Panel ID="Pnladdcatalogo" runat="server">
    
    <table>
    <tr>
    <td class="TableHeader" colspan="2">
        <asp:Label ID="lblAddmodifyCatalogo" runat="server" Text="AGREGAR CATALOGO DE MOVIMIENTO DE BANCO"></asp:Label>
    </td>
    </tr>
    	<tr>
    <td class="TablaField">Grupo de Catálogo:
    </td>
    <td>
        <asp:DropDownList ID="ddlGrupoCat" runat="server" 
            DataSourceID="SqlGrupoCatálogos" DataTextField="grupoCatalogo" 
            DataValueField="grupoCatalogosID">
        </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td class="TablaField">Clave de Catálogo:
    </td>
    <td>
        <asp:TextBox ID="txtclaveCatalogo" runat="server" MaxLength="5"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td class="TablaField">Catálogo Movimiento de Banco:
    </td>
    <td>
        <asp:TextBox ID="txtcatalogoMovBanco" runat="server" Width="200px"></asp:TextBox>
    </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
    
    
            <asp:Button ID="btnAgregarCatalogo" runat="server"
            Text="Agregar" onclick="btnAgregarCatalogo_Click" />
            <asp:Button ID="btnModificarCatalogo" runat="server"
            Text="Modificar" onclick="btnModificarCatalogo_Click"/>
            <asp:Button ID="btnCancelarCatalogo" runat="server" Text="Cancelar" 
            CausesValidation="False" onclick="btnCancelarCatalogo_Click"/>
    
    
        </td>
    </tr>
    </table>
</asp:Panel>    
    <asp:Panel ID="pnlSubCatalogo" runat="server">
    <table>
    	<tr>
    		<td class="TableHeader" colspan="2">
                <asp:Label ID="lblsubCatalogos" runat="server" Text="SUBCATALOGOS DE MOVIMIENTO DE BANCO"></asp:Label>
    		</td>
    	</tr>
    	<tr>
    
    
    
    
    		<td class="TableHeader">
    		    Catálogo:</td>
    	    <td>
                <asp:DropDownList ID="drpdlCatalogoforSub" runat="server" AutoPostBack="True" 
                    DataSourceID="sdsGruposSubCatalogos" DataTextField="catalogoMovBanco" 
                    DataValueField="catalogoMovBancoID" Height="23px" 
                    onselectedindexchanged="drpdlCatalogoforSub_SelectedIndexChanged" Width="206px">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsGruposSubCatalogos" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT distinct catalogoMovimientosBancos.catalogoMovBanco, SubCatalogoMovimientoBanco.catalogoMovBancoID FROM SubCatalogoMovimientoBanco INNER JOIN catalogoMovimientosBancos ON SubCatalogoMovimientoBanco.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID">
                </asp:SqlDataSource>
            </td>
    	</tr>
    	<tr>
    	<td colspan="2">
                  <asp:Button ID="btnAgregarListaSubCatalogo" runat="server" 
                      onclick="btnAgregarListaSubCatalogo_Click" Text="Agregar" />
                  <asp:Button ID="btnModificarListaSubCatalogo" runat="server" 
                      onclick="btnModificarListaSubCatalogo_Click" Text="Modificar" />
                  <asp:Button ID="btnEliminarSubCatalogo" runat="server" CausesValidation="False" 
                      onclick="btnEliminarSubCatalogo_Click" Text="Eliminar" />
                  <asp:Button ID="btnVerCatalogos" runat="server" CausesValidation="False" 
                      onclick="btnVerCatalogos_Click" Text="Volver a Catálogos" />
    	
    	</td>
    	</tr>
    	<tr>
    	<td class="TablaField" colspan="2">
            <table>
                <tr>
                    <td>
                        ELEMENTOS POR PÁGINA:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlElemXpageSub" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlElemXpageSub_SelectedIndexChanged">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem Selected="True">100</asp:ListItem>
                            <asp:ListItem>200</asp:ListItem>
                            <asp:ListItem>500</asp:ListItem>
                            <asp:ListItem>1000</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
    	</td>
    	</tr>
    
        <tr>
            <td colspan="2">
                <asp:GridView ID="grdvSubCatalogos" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" 
                    DataKeyNames="subCatalogoMovBancoID,catalogoMovBanco,subCatalogoClave,subCatalogo,catalogoMovBancoID,grupoCatalogoID" 
                    DataSourceID="SqlSubCatalogos" 
                    onselectedindexchanged="grdvSubCatalogos_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                            ShowSelectButton="True" />
                        <asp:BoundField DataField="subCatalogoMovBancoID" HeaderText="ID" 
                            ReadOnly="True" SortExpression="subCatalogoMovBancoID" Visible="False" />
                        <asp:BoundField DataField="subCatalogo" HeaderText="SubCatálogo" 
                            SortExpression="subCatalogo" />
                        <asp:BoundField DataField="catalogoMovBanco" HeaderText="Catálogo" 
                            SortExpression="catalogoMovBanco" />
                        <asp:BoundField DataField="subCatalogoClave" HeaderText="Clave del Subcatálogo" 
                            SortExpression="subCatalogoClave" />
                        <asp:BoundField DataField="catalogoMovBancoID" HeaderText="catalogoMovBancoID" 
                            SortExpression="catalogoMovBancoID" Visible="False" />
                        <asp:BoundField DataField="grupoCatalogoID" HeaderText="grupoCatalogoID" 
                            SortExpression="grupoCatalogoID" Visible="False" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlSubCatalogos" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT catalogoMovimientosBancos.catalogoMovBanco, SubCatalogoMovimientoBanco.subCatalogoMovBancoID, SubCatalogoMovimientoBanco.subCatalogoClave, SubCatalogoMovimientoBanco.subCatalogo, SubCatalogoMovimientoBanco.catalogoMovBancoID, catalogoMovimientosBancos.grupoCatalogoID FROM catalogoMovimientosBancos INNER JOIN SubCatalogoMovimientoBanco ON catalogoMovimientosBancos.catalogoMovBancoID = SubCatalogoMovimientoBanco.catalogoMovBancoID WHERE (SubCatalogoMovimientoBanco.catalogoMovBancoID = @catsubID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="drpdlCatalogoforSub" DefaultValue="-1" 
                            Name="catsubID" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="sqlcatalogo" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT catalogoMovimientosBancos.catalogoMovBancoID, catalogoMovimientosBancos.catalogoMovBanco FROM catalogoMovimientosBancos INNER JOIN GruposCatalogosMovBancos ON catalogoMovimientosBancos.grupoCatalogoID = GruposCatalogosMovBancos.grupoCatalogosID WHERE (catalogoMovimientosBancos.grupoCatalogoID = @grupoCatID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="drpdlGrupoAddSubcatalogo" DefaultValue="-1" 
                            Name="grupoCatID" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    
    </table>
    
    </asp:Panel>
    <asp:Panel ID="pnladdSubCatalogo" runat="server">
    <table>
    	<tr>
    		<td class="TableHeader" colspan="2">
                <asp:Label ID="lblAddmodifySubcatalogo" runat="server" Text="AGREGAR SUBCATÁLOGO DE MOVIMIENTO DE BANCO"></asp:Label>
                
    		</td>
    		
    	</tr>
    	<tr>
    	<td class="TablaField">
    	    Grupo al que pertenece:</td>
    	<td>
            <asp:DropDownList ID="drpdlGrupoAddSubcatalogo" runat="server" 
                AutoPostBack="True" DataSourceID="sdsGrupoaddSub" DataTextField="grupoCatalogo" 
                DataValueField="grupoCatalogosID" Height="23px" Width="176px">
            </asp:DropDownList>
            <asp:SqlDataSource ID="sdsGrupoaddSub" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                SelectCommand="SELECT [grupoCatalogosID], [grupoCatalogo] FROM [GruposCatalogosMovBancos]">
            </asp:SqlDataSource>
    	</td>
    	</tr>
    	<tr>
            <td class="TablaField">
                Catálogo al que pertenece:
            </td>
            <td>
                <asp:DropDownList ID="ddlCatalogo" runat="server" AutoPostBack="True" 
                    DataSourceID="sqlcatalogo" DataTextField="catalogoMovBanco" 
                    DataValueField="catalogoMovBancoID" Height="23px" Width="164px">
                </asp:DropDownList>
            </td>
        </tr>
    	<tr>
    	<td class="TablaField">
    	Sub-Catálogo: 
    	</td>
    	<td>
            <asp:TextBox ID="txtSubCatalogo" runat="server"></asp:TextBox>
    	</td>
    	</tr>
    	<tr>
    	<td class="TablaField">
    	Clave del Sub-Catalogo:
    	
    	</td>
    	<td>
    	<asp:TextBox ID="txtCalveSubCatalogo" runat="server"></asp:TextBox>
            
    	</td>
    	</tr>
    	    <tr>
        <td colspan="2" align="center">
    
    
            <asp:Button ID="btnAgregarSubCatalogo" runat="server"
            Text="Agregar" onclick="btnAgregarSubCatalogo_Click" />
            <asp:Button ID="btnModificarSubCatalogo" runat="server"
            Text="Modificar" onclick="btnModificarSubCatalogo_Click" />
            <asp:Button ID="btnCancelarSubCatalogo" runat="server" Text="Cancelar" 
            CausesValidation="False" onclick="btnCancelarSubCatalogo_Click"/>
    
    
        </td>
    </tr>
    </table>
    
    </asp:Panel>
</asp:Content>


