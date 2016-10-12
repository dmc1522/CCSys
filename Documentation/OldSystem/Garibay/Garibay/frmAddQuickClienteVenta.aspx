<%@ Page Language="C#" Title="Agregar Cliente para Venta"  AutoEventWireup="true" CodeBehind="frmAddQuickClienteVenta.aspx.cs" Inherits="Garibay.frmAddQuickClienteVenta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table>
            <tr>
                <td class="TableHeader" colspan="2" align="center">
                    AGREGAR CLIENTE DE VENTA RÁPIDO</td>
            </tr>
            <tr>
                <td colspan="2">
                
                    &nbsp;</td>
                   
            </tr>
           
            <tr>
                <td class="TablaField">
                    Nombre:</td>
                <td>
                    <asp:TextBox ID="txtNombre" runat="server" Width="263px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField" >
                    Domicilio:</td>
                <td >
                    <asp:TextBox ID="txtDomicilio" runat="server" Width="261px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Ciudad:</td>
                <td>
                    <asp:TextBox ID="txtCiudad" runat="server" Width="260px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Estado:</td>
                <td>
                    <asp:DropDownList ID="drpdlEstado" runat="server" DataSourceID="sdsEstados" 
                        DataTextField="estado" DataValueField="estadoID" Height="22px" Width="259px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsEstados" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT [estado], [estadoID] FROM [Estados] ORDER BY [estado]">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Teléfono:</td>
                <td>
                    <asp:TextBox ID="txtTelefono" runat="server" Width="259px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    RFC:</td>
                <td>
                    <asp:TextBox ID="txtRfc" runat="server" Width="256px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TableField" colspan="2">
                <asp:Label ID="lblProductoresParecidos" runat="server" 
                    Text="NOMBRES PARECIDOS QUE YA ESTAN EN EL SISTEMA:" Visible="False"></asp:Label>
                    <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="sdsClientesRepetidos" Visible="False">
                    <Columns>
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" 
                            SortExpression="nombre" />
                        <asp:BoundField DataField="domicilio" HeaderText="omicilio" 
                            SortExpression="domicilio" />
                        <asp:BoundField DataField="ciudad" HeaderText="Ciudad" 
                            SortExpression="ciudad" />
                        <asp:BoundField DataField="telefono" 
                            HeaderText="Telefono" SortExpression="telefono" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" 
                            SortExpression="estado" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsClientesRepetidos" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    
                    
                        
                        
                        SelectCommand="SELECT ClientesVentas.nombre, ClientesVentas.domicilio, ClientesVentas.ciudad, ClientesVentas.telefono, Estados.estado FROM ClientesVentas INNER JOIN Estados ON ClientesVentas.estadoID = Estados.estadoID order by ClientesVentas.nombre ASC">
                </asp:SqlDataSource>
                <asp:Label ID="lblProductorYaExiste" runat="server" 
                    Text="YA EXISTE UN CLIENTE CON EL MISMO  NOMBRE EN EL SISTEMA" 
                    Font-Bold="True" Font-Size="Large" Visible="False"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td class="TableField" colspan="2">
                    <asp:Panel ID="panelmensaje" runat="server">
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
                                    <asp:Label ID="lblMensajeOperationresult" runat="server" 
                                        SkinID="lblMensajeOperationresult" Text="Label"></asp:Label>
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
                                    <asp:Button ID="btnAceptardetails" runat="server" CssClass="Button" 
                                        Text="Aceptar" Width="135px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
             
            <tr>
                <td class="TableField" colspan="2">
                <asp:Button ID="btnValidar" runat="server" onclick="btnValidar_Click" 
                    Text="Validar Cliente" />
                <asp:Button ID="btnAceptar" runat="server" Text="Agregar" 
                    Width="135px" onclick="btnAceptar_Click" Visible="False" />
                    <asp:Button ID="btnSalir" runat="server" onclick="btnSalir_Click" 
                        Text="Salir" />
                </td>
            </tr>
             
        </table>
    
    </div>
    </form>
</body>
</html>
