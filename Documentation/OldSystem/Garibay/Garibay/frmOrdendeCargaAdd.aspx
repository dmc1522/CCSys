<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" Theme="skinverde" Title="Orden de carga" AutoEventWireup="true" CodeBehind="frmOrdendeCargaAdd.aspx.cs" Inherits="Garibay.frmOrdendeCargaAdd" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" runat="server"  contentplaceholderid="ContentPlaceHolder1">
    <asp:UpdatePanel ID="MainPnlUpdate" runat="Server">
<ContentTemplate>
        <table>
            <tr>
                <td rowspan="6">
                    <asp:Image ID="imgEmpresa" runat="server" Height="92px" 
                        ImageUrl="~/imagenes/LogoIPROJALMedium.jpg" Width="165px" />
                </td>
                <td rowspan="6">
                    <asp:Label ID="lblEmpresa" runat="server" Font-Size="X-Large" 
                        Text="INTEGRADORA DE PRODUCTORES DE JALISCO"></asp:Label>
&nbsp; S.P.R. DE R.L.<br />
                    <br />
                    Av. Patria Oriente No. 10<br />
                    C.P. 46600 Ameca, Jalisco.<br />
                    R.F.C. IPJ-030814-JAA<br />
                    Tel. 01(375) 758 1199</td>
                <td class="TableHeader" align= "center">
                    Orden de carga. No.</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:TextBox ID="txtNumOrdenCarga" runat="server" Font-Size="X-Large" 
                        ReadOnly="True" Width="142px"></asp:TextBox>
                    <asp:TextBox ID="txtIdToModify" runat="server" Visible="False" Width="15px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TableHeader" align="center">
                    FECHA</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:TextBox ID="txtFecha" runat="server" ReadOnly="True"></asp:TextBox>
                    <rjs:PopCalendar ID="PopCalendar3" runat="server" Separator="/" 
                        Control="txtFecha" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    CICLO:</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:DropDownList ID="drpdlCiclo" runat="server" DataSourceID="sdsCiclos" 
                        DataTextField="CicloName" DataValueField="cicloID" Height="23px" Width="181px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        SelectCommand="SELECT        cicloID, CicloName
FROM            Ciclos
WHERE cerrado=@cerrado 
ORDER BY fechaInicio">
                    	<SelectParameters>
							<asp:Parameter DefaultValue="FALSE" Name="cerrado" />
						</SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:UpdatePanel ID="PnlCliente" runat="Server">
                    <ContentTemplate>
                        
                        <table>
                            <tr>
                                <td class="TablaField">
                                    Proveedor:</td>
                                <td colspan="7">
                                    <asp:DropDownList ID="ddlProveedor" runat="server" DataSourceID="sdsProveedor" 
                                        DataTextField="Nombre" DataValueField="proveedorID">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsProveedor" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        SelectCommand="SELECT [proveedorID], [Nombre] FROM [Proveedores] ORDER BY [Nombre]">
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td class="TablaField">
                                    Cliente:</td>
                                <td colspan="7">
                                    <asp:DropDownList ID="ddlEmpresa" runat="server" DataSourceID="sdsEmpresas" 
                                        DataTextField="Empresa" DataValueField="empresaID" AutoPostBack="True" 
                                        onselectedindexchanged="ddlEmpresa_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsEmpresas" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        SelectCommand="SELECT [empresaID], [Empresa] FROM [Empresas] WHERE ([empresaID] &lt;&gt; @empresaID) ORDER BY [Empresa]">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="3" Name="empresaID" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td class = "TablaField">
                                    Chofer:</td>
                                <td colspan="7">
                                    <asp:TextBox ID="txtChofer" runat="server" Width="505px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class = "TablaField">
                                    Placas:</td>
                                <td colspan="7">
                                    <asp:TextBox ID="txtPlacas" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class = "TablaField">
                                    Marca:</td>
                                <td>
                                    <asp:TextBox ID="txtMarca" runat="server"></asp:TextBox>
                                </td>
                                <td class = "TablaField">
                                    Año:</td>
                                <td>
                                    <asp:TextBox ID="txtAño" runat="server"></asp:TextBox>
                                </td>
                                <td class = "TablaField">
                                    Color:</td>
                                <td >
                                    <asp:TextBox ID="txtColor" runat="server"></asp:TextBox>
                                </td>
                                <td class = "TablaField">
                                    Jaula:</td>
                                <td >
                                    <asp:TextBox ID="txtJaula" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class = "TablaField">
                                    Origen:</td>
                                <td colspan="7">
                                    <asp:TextBox ID="txtOrigen" runat="server" Width="629px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class = "TablaField">
                                    Producto:</td>
                                <td colspan="7">
                                    <asp:TextBox ID="txtProducto" runat="server" Width="631px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class = "TablaField">
                                    Presentación</td>
                                <td colspan="7">
                                    <asp:TextBox ID="txtPresentacion" runat="server" Width="630px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class = "TablaField">
                                    Bodega:</td>
                                <td colspan="7">
                                    <asp:TextBox ID="txtBodega" runat="server" Width="630px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class = "TablaField">
                                    Ubicación:</td>
                                <td colspan="7">
                                    <asp:TextBox ID="txtUbicacion" runat="server" Width="626px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        
                    </ContentTemplate></asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3">
                
                    <table>
                        <tr>
                            <td class = "TablaField">
                                Destino:</td>
                            <td>
                                <asp:TextBox ID="txtDestino" runat="server" Width="650px" Height="71px" 
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                      
                        <tr>
                            <td class = "TablaField">

                                Facturar a:</td>
                            <td > 
                                <asp:TextBox ID="txtFacturara" runat="server" Height="74px" TextMode="MultiLine" 
                                    Width="655px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class = "TablaField">
                                Observaciones:</td>
                            <td>
                                <asp:TextBox ID="txtObservaciones" runat="server" Height="175px" TextMode="MultiLine" 
                                    Width="655px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>

                            <td align="left" class="TablaField">
                                Email:</td>
                            <td align="left">
                                <asp:TextBox ID="txtEmail" runat="server" Width="265px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="TablaField">
                                CC:</td>
                            <td align="left">
                                <asp:TextBox ID="txtCC" runat="server" Width="265px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="TablaField">
                                Descripción:</td>
                            <td align="left">
                                <asp:TextBox ID="txtDescripcionEmail" runat="server" Height="175px" 
                                    TextMode="MultiLine" Width="655px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
                                    onclick="btnAgregar_Click" />
                                <asp:Button ID="btnModificar" runat="server" Text="Modificar" 
                                    onclick="btnModificar_Click" />
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
                                <asp:Button ID="btnPrint" runat="server" Text="Imprimir" />
                                <asp:Button ID="btnEnviar" runat="server" Text="Enviar por Correo" />
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Panel ID="pnlOrdenResult" runat="server">
                                    <asp:Image ID="imgBien" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                                    <asp:Image ID="imgMal" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                                    <asp:Label ID="lblOrdenResult" runat="server"></asp:Label>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                
                </td>
            </tr>
            
           
    </table>

</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <script type="text/javascript" src="/scripts/divFunctions.js"></script>
    <script type="text/javascript" src="/scripts/prototype.js"></script>
    </asp:Content>
