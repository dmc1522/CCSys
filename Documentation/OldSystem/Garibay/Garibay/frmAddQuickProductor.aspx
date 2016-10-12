<%@ Page Language="C#" AutoEventWireup="true" Title="Agregar Productor Rápido" CodeBehind="frmAddQuickProductor.aspx.cs" Inherits="Garibay.frmAddQuickProductor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table>
            <tr>
                <td class="TableHeader" colspan="2">
                    AGREGAR PRODUCTOR RÁPIDO</td>
            </tr>
            <tr>
                <td colspan="2">
                
                    &nbsp;</td>
                   
            </tr>
           
            <tr>
                <td class="TablaField">
                    Apellido Paterno:</td>
                <td>
                    <asp:TextBox ID="txtPaterno" runat="server" Width="263px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField" >
                    Apelllido Materno:</td>
                <td >
                    <asp:TextBox ID="txtMaterno" runat="server" Width="261px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Nombre(s):</td>
                <td>
                    <asp:TextBox ID="txtNombres" runat="server" Width="260px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Domicilio:</td>
                <td>
                    <asp:TextBox ID="txtDomicilio" runat="server" Width="260px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Poblacion:</td>
                <td>
                    <asp:TextBox ID="txtPoblacion" runat="server" Width="260px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Colonia:</td>
                <td>
                    <asp:TextBox ID="txtColonia" runat="server" Width="260px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Municipio:</td>
                <td>
                    <asp:TextBox ID="txtMunicipio" runat="server" Width="260px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Estado:</td>
                <td>
					<asp:DropDownList ID="ddlEstado" runat="server" DataSourceID="slqEstados" 
						DataTextField="estado" DataValueField="estadoID">
					</asp:DropDownList>
                	<asp:SqlDataSource ID="slqEstados" runat="server" 
						ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
						SelectCommand="SELECT * FROM [Estados]"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Teléfono:</td>
                <td>
                    <asp:TextBox ID="txtTelefono" runat="server" Width="260px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Celular:</td>
                <td>
                    <asp:TextBox ID="txtCelular" runat="server" Width="260px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TablaField">
                    Código del archivo de boletas:</td>
                <td>
                    <asp:TextBox ID="txtCodeBoletas" runat="server" Width="259px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TableField" colspan="2">
                <asp:Label ID="lblProductoresParecidos" runat="server" 
                    Text="NOMBRES PARECIDOS QUE YA ESTAN EN EL SISTEMA:" Visible="False"></asp:Label>
                    <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="sdsProductoresRepetidos" Visible="False">
                    <Columns>
                        <asp:BoundField DataField="apaterno" HeaderText="A. Paterno" 
                            SortExpression="apaterno" />
                        <asp:BoundField DataField="amaterno" HeaderText="A. Materno" 
                            SortExpression="amaterno" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" 
                            SortExpression="nombre" />
                        <asp:BoundField DataField="codigoBoletasFile" 
                            HeaderText="Cod. en arhivo Boletas" SortExpression="codigoBoletasFile" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsProductoresRepetidos" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    
                    
                        
                        SelectCommand="SELECT apaterno, amaterno, nombre, codigoBoletasFile FROM Productores WHERE (apaterno LIKE '%' + @apaterno + '%') AND (amaterno LIKE '%' + @amaterno + '%') AND (nombre LIKE '%' + @nombre + '%') ORDER BY apaterno, amaterno, nombre">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtPaterno" Name="apaterno" 
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="txtMaterno" Name="amaterno" 
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="txtNombres" Name="nombre" PropertyName="Text" 
                            Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:Label ID="lblProductorYaExiste" runat="server" 
                    Text="YA EXISTE UN PRODUCTOR CON EL MISMO NOMBRE EN EL SISTEMA" 
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
                    Text="Validar Productor" />
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
