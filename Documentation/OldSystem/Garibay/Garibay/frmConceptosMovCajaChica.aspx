<%@ Page Language="C#" Theme ="skinrojo" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmConceptosMovCajaChica.aspx.cs" Inherits="Garibay.WebForm3" Title="Conceptos de Movimientos de Caja Chica" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
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
<asp:Panel ID="panelLista_conceptos" runat="server" Width="390px">

                <table>
                <tr>
                    <td class="TableHeader">
                        CONCEPTOS DE MOVIMIENTOS DE LA CAJA CHICA
                    </td>
                </tr>
                   <tr>
                    <td>
                
                    </td>
                </tr>
                <tr>
                <td align="center" >
                    <asp:Button ID="btnAgregarDeLista" runat="server" Text="Agregar" 
                        onclick="btnAgregarDeLista_Click" />
	                <asp:Button ID="btnModificarDeLista" runat="server" Text="Modificar" 
                        onclick="btnModificarDeLista_Click" />
                    <asp:Button ID="btnEliminarDeLista" runat="server" Text="Eliminar" 
                        onclick="btnEliminarDeLista_Click"/>
	               </td>
                </tr>
                <tr>
                    <td>
                <asp:GridView ID="grdvlista_conceptosCajaChica" runat="server" AutoGenerateColumns="False" 
                 AllowPaging="True" AllowSorting="True" ForeColor="White" GridLines="None" 
                 CellPadding="4" DataSourceID="sqldtsrcConCajaChica" DataKeyNames="conceptomovID,concepto" 
                            onselectedindexchanged="grdvlista_conceptosCajaChica_SelectedIndexChanged">
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <Columns>
                        <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                            ShowSelectButton="True" />
                        <asp:BoundField DataField="conceptomovID" HeaderText="ID" 
                            InsertVisible="False" ReadOnly="True" SortExpression="conceptomovID" 
                            Visible="False" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="concepto" HeaderText="concepto" 
                            SortExpression="concepto" />
                    </Columns>
                <HeaderStyle CssClass="TableHeader" />
                <AlternatingRowStyle BackColor="White" />
                <SelectedRowStyle Font-Names="Arial"/>
                </asp:GridView>
                        <asp:SqlDataSource ID="sqldtsrcConCajaChica" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
                            SelectCommand="SELECT * FROM [Conceptosmovimientos]"></asp:SqlDataSource>
                    </td>
                </tr>
             
               </table>
            
        
    
            </asp:Panel>
            <asp:Panel ID="panelNuevoConcepto" runat="server" Width="644px">
            <table>
                <tr>
                    <td colspan="2" align="center" class="TableHeader">
                    
                        &nbsp;<asp:Label ID="lblConceptoCajaChica" runat="server" 
                            Text="AGREGAR CONCEPTO DE MOV. CAJA CHICA"></asp:Label>
                    </td>
                    
                
                </tr>
           
            <tr>
                <td class="TablaField">
                    Concepto :
              
                </td>
                <td>
                    <asp:TextBox ID="txtConcepto" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RFVConcepto" runat="server" 
                    ControlToValidate="txtConcepto" ErrorMessage="El Campo del Concepto es necesario ">
                    </asp:RequiredFieldValidator>
                </td>
                
            </tr>
           
            <tr>
                <td align="center" colspan="2">
                    
                    <asp:Button ID="btnAceptar" runat="server" Text="Agregar" 
                        onclick="btnAceptar_Click" />
                    <asp:Button ID="btnModificar" runat="server" Text="Modificar" 
                        onclick="btnModificar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                    CausesValidation="False" onclick="cmdCancelar_Click" />
                </td>
            </tr>
            </table>
            </asp:Panel>


    </asp:Content>
