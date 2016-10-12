<%@ Page Language="C#" Theme ="skinverde" Title = "Agregar/Modificar Certificado" AutoEventWireup="true" CodeBehind="frmCertificadoAddQuick.aspx.cs" Inherits="Garibay.frmCertificadoAddQuick" %>

<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    </head>
<body>
<script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>

    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" 
                    EnableScriptGlobalization="True" ScriptMode="Release">
                </asp:ScriptManager>
   <asp:UpdatePanel id = "panelUP" runat="Server">
    <ContentTemplate>
    <div>
    
     <asp:UpdateProgress ID="UpdateProgress3" runat="server" 
                    AssociatedUpdatePanelID="panelUP" DisplayAfter="0">
                    <ProgressTemplate>
                        <asp:Image ID="Image7" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                        Procesando Solicitud...
                    </ProgressTemplate>
                </asp:UpdateProgress>
        <table>
            <tr>
                <td align="center" class="TableHeader" colspan="2">
                    <asp:Label ID="lblAction" runat="server" Text="AGREGAR NUEVO CERTIFICADO"></asp:Label>
                    <asp:Label ID="lblcertID" runat="server" 
                        Text="lblcertID" Visible="False"></asp:Label>
                    <asp:Label ID="lblcredID" runat="server" Text="lblcredID" Visible="False"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left" class="TablaField">
                    Número de Certificados:</td>
                <td align="left">
                                                    <asp:TextBox ID="txtNumCertificados" 
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="TablaField">
                    Fecha de emisión:</td>
                <td align="left">
                                                    <asp:TextBox ID="txtCFFechaEmision" 
                        runat="server" ReadOnly="True"></asp:TextBox>
                                                    <rjs:PopCalendar ID="PopCalendar3" 
                        runat="server" Control="txtCFFechaEmision" 
                                                        Separator="/" />
                </td>
            </tr>
            <tr>
                <td align="left" class="TablaField">
                    Bodega:</td>
                <td align="left">
                                                    <asp:DropDownList ID="drpdlBodega" runat="server" DataSourceID="sdsBodegas" 
                                                        DataTextField="bodega" DataValueField="bodegaID" Height="22px" Width="317px">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="sdsBodegas" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                        SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas]">
                                                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td align="left" class="TablaField">
                    Producto:</td>
                <td align="left">
                    <asp:DropDownList ID="drpdlProducto" runat="server" DataSourceID="sdsProducto" 
                        DataTextField="Nombre" DataValueField="productoID" Height="22px" Width="201px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsProducto" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT [productoID], [Nombre] FROM [Productos] ORDER BY [Nombre]">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td align="left" class="TablaField">
                    Empresa a la que se emite:</td>
                <td align="left">
                                                    <asp:DropDownList ID="drpdlEmpresa" 
                        runat="server" DataSourceID="sdsEmpresas" 
                                                        DataTextField="Empresa" 
                        DataValueField="empresaID" Height="22px" 
                                                        Width="440px">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="sdsEmpresas" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                        
                                                        
                        SelectCommand="SELECT [Empresa], [empresaID] FROM [Empresas]">
                                                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td align="left" class="TablaField">
                    # Cabezas:</td>
                <td align="left">
                    <asp:TextBox ID="txtnumCabezas" runat="server" Height="22px" Width="249px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="TablaField">
                    Fecha de vencimiento:</td>
                <td align="left">
                                                    <asp:TextBox ID="txtCFFechaVencimiento" 
                        runat="server" ReadOnly="True"></asp:TextBox>
                                                    <rjs:PopCalendar ID="PopCalendar4" runat="server" 
                                                        Control="txtCFFechaVencimiento" 
                        Separator="/" />
                </td>
            </tr>
            <tr>
                <td align="left" class="TablaField">
                    KG:</td>
                <td align="left">
                                                    <asp:TextBox ID="txtCFKG" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="TablaField">
                    PRECIO:</td>
                <td align="left">
                                                    <asp:TextBox ID="txtCFPrecio" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="TablaField">
                    Monto del certificado:</td>
                <td align="left">
                                                    <asp:TextBox ID="txtCFMonto" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="TablaField">
                    SEGURO:</td>
                <td align="left">
                                                    <asp:DropDownList ID="ddlCFSeguro" 
                        runat="server" datasourceid="sdsSeguro" 
                                                        DataTextField="credFinSeguro" DataValueField="credFinSeguroID" 
                                                        Height="23px" Width="240px">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="sdsSeguro" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                        
                                                        
                        SelectCommand="SELECT [credFinSeguroID], [credFinSeguro] FROM [credFinSeguro] ORDER BY [credFinSeguro]">
                                                    </asp:SqlDataSource>
                </td>
            </tr>
            </table>
      <asp:UpdatePanel ID="pnlDivs" runat="Server">
      <ContentTemplate>
        <table>
            <tr>
                <td class="TablaField">
                                                    <asp:CheckBox ID="chkAsignarCredito" runat="server" 
                                                        Text="Asignar a crédito financiero" AutoPostBack="True" />
                                                    <cc1:MutuallyExclusiveCheckBoxExtender ID="chkAsignarCredito_MutuallyExclusiveCheckBoxExtender" 
                                                        runat="server" Enabled="True" Key="0" 
                        TargetControlID="chkAsignarCredito">
                                                    </cc1:MutuallyExclusiveCheckBoxExtender>
                </td>
                <td>
                                                    <div ID = "divAsignaraCredito" runat="Server">
                                                        <asp:DropDownList ID="drpdlCred" runat="server" DataSourceID="sdsCreditos" 
                                                            DataTextField="credito" DataValueField="creditoFinancieroID" Height="22px" 
                                                            Width="672px">
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="sdsCreditos" runat="server" 
                                                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                            DeleteCommand="DELETE FROM CreditosFinancieros WHERE (creditoFinancieroID = @creditosfinancierosID)" 
                                                            
                                                            SelectCommand="SELECT CreditosFinancieros.creditoFinancieroID, convert(varchar(255),CreditosFinancieros.numCredito) + ' - ' +  Bancos.nombre   + ' - ' + CreditosFinancieros.empresa_acreditada + ' - $ ' +convert(varchar(255), Cast(CreditosFinancieros.monto as decimal(16,2)) ) as credito FROM Bancos INNER JOIN CreditosFinancieros ON Bancos.bancoID = CreditosFinancieros.bancoID ORDER BY  convert(float, CreditosFinancieros.numCredito) DESC">
                                                            <DeleteParameters>
                                                                <asp:Parameter Name="creditosfinancierosID" />
                                                            </DeleteParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                    
                                                                
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    <asp:CheckBox ID="chkAsignarClienteVenta" runat="server" 
                        Text="Asignar a cliente de venta" AutoPostBack="True" />
                    <cc1:MutuallyExclusiveCheckBoxExtender ID="chkAsignarClienteVenta_MutuallyExclusiveCheckBoxExtender" 
                        runat="server" Enabled="True" Key="0" TargetControlID="chkAsignarClienteVenta">
                    </cc1:MutuallyExclusiveCheckBoxExtender>
                </td>
                <td>
                 <div id= "divAsignaraClientedeVenta" runat="Server">   
                        <table>
                            <tr>
                                <td>
                                    Cliente de venta:</td>
                                <td>
                                    <asp:DropDownList ID="drpdlClienteVenta" runat="server" 
                                        DataSourceID="sdsClientes" DataTextField="nombre" 
                                        DataValueField="clienteventaID" Height="22px" Width="246px">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsClientes" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        SelectCommand="SELECT [clienteventaID], [nombre] FROM [ClientesVentas] ORDER BY [nombre]">
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </div>
                         
                </td>
            </tr>
        </table>
        </ContentTemplate>
        </asp:UpdatePanel>
        <table>
            <tr>
                <td>
                                                <asp:Panel ID="pnlNewMov" runat="server" 
                        Visible="False">
                                                    <asp:Image ID="imgBien" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                                                    <asp:Image ID="imgMal" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                                                    <asp:Label ID="lblResult" runat="server" Font-Size="Large"></asp:Label>
                                                </asp:Panel>
                                            </td>
            </tr>
            <tr>
                <td>
                                                <asp:Button ID="btnAgregar0" runat="server"
                                                    Text="Agregar" CausesValidation="False" onclick="btnAgregar0_Click" 
                                                     />
                                                <asp:Button ID="btnModificar" runat="server" Text="Modificar" onclick="btnModificar_Click" 
                                                    />
                                                <asp:Button ID="btnCancelar" runat="server" Text="Salir" 
                                                     />
                                            </td>
            </tr>
        </table>
        
    </div>
     </ContentTemplate> 
      
    </asp:UpdatePanel> 
    </form>
</body>

</html>
