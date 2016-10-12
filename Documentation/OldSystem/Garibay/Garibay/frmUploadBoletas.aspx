<%@ Page Language="C#" theme="skinrojo" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmUploadBoletas.aspx.cs" Inherits="Garibay.WebForm12" Title="Cargar Boletas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table>
    	<tr>
    		<td colspan="2" class="TableHeader">Seleccione Archivo de boletas:</td>
    	</tr>
    	<tr><td class="TablaField">
            <asp:FileUpload ID="FUBoletas" runat="server" />
            </td><td>
                <asp:Button ID="btnCargaArchivo" runat="server" onclick="btnCargaArchivo_Click" 
                    Text="Cargar Archivo" />
            </td></tr>
    	<tr><td class="TablaField">Nombre del Archivo:</td><td>
            <asp:Label ID="lblFileName" runat="server" Text="-"></asp:Label>
            </td></tr>
    	<tr><td class="TablaField">Tamaño:</td><td>
            <asp:Label ID="lblFileSize" runat="server" Text="-"></asp:Label>
            </td></tr>
    	</table>
    	

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="False">
    <ContentTemplate>
        <table>
        <tr><td>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
            <ProgressTemplate>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/cargando.gif" />
                Procesando Boletas...
            </ProgressTemplate>
            </asp:UpdateProgress>
            </td></tr>
    	    <tr>
    		    <td class="TableHeader">
                    <asp:Button ID="btnProcesar" runat="server" onclick="btnProcesar_Click" 
                        Text="Procesar Boletas" />
                </td>
    	    </tr>
    	    <tr><td>
    	    
    	    <asp:panel runat="Server" ID="PanelBoletas" Visible="False">
    	    <table>
    	    <tr>
                <td class="TableHeader">
                    Filtrar Boletas por:</td>
            </tr>
            <tr>
            <td>
                <table >
                <tr>
		            <td class="TablaField" >Clientes/Proveedores:</td>
		            <td >
                        <asp:DropDownList ID="ddlClientesProveedores" runat="server" 
                            onselectedindexchanged="ddlClientesProveedores_SelectedIndexChanged" 
                            AutoPostBack="True">
                            <asp:ListItem Value="Cliente">Clientes</asp:ListItem>
                            <asp:ListItem Value="Proveedor">Proveedores</asp:ListItem>
                        </asp:DropDownList>
                    </td>
	            </tr>	
                    <tr>
                        <td class="TablaField">
                            PRODUCTO:</td>
                        <td>
                            <asp:DropDownList ID="ddlProductos" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlProductos_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
            	</tr>
    	    
    	    <tr>
    	        <td>
    	            <table >
    	            	<tr>
    	            		<td class="TableHeader">BOLETAS A AGREGAR</td>
    	            	</tr>
    	            	<tr>
    	            		<td class="TableHeader">Filtrar Boletas Por:</td>
    	            	</tr>
    	                <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td class="TablaField">
                                            <asp:CheckBox ID="chkFilterClienteProv" runat="server" Text="Nombre:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFilterClienteProveedor" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TablaField">
                                            <asp:CheckBox ID="chkFilterCodigoClienteProv" runat="server" 
                                                Text="Codigo Cliente/Proveedor:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFilterCodigo" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TablaField">
                                            <asp:CheckBox ID="chkFilterEntradaSalida" runat="server" 
                                                Text="Tipo de Boleta:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFilterEntradaSalida" runat="server">
                                                <asp:ListItem Selected="True">Entrada</asp:ListItem>
                                                <asp:ListItem>Salida</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="btnUpdateGridView" runat="server" onclick="Button1_Click" 
                                                Text="Actualizar lista de boletas" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            Boletas en Archivo:
                                            <asp:Label ID="lblBoletasInFile" runat="server" Font-Bold="True" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            Boletas en la lista disponibles para ingresar:
                                            <asp:Label ID="lblBoletasInGrid" runat="server" Font-Bold="True" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
    	                <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                BOLETAS QUE YA ESTAN EN EL SISTEMA</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvBoletasYaIngresadas" runat="server" 
                                    AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="NumeroBoleta" HeaderText="Boleta" >
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Ticket" HeaderText="Ticket">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigoClienteProvArchivo" 
                                            HeaderText="Codigo Cliente" />
                                        <asp:BoundField DataField="NombreProductor" HeaderText="Nombre">
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Producto" HeaderText="Codigo Producto" />
                                        <asp:BoundField DataField="Placas" HeaderText="Placas" />
                                        <asp:BoundField DataField="FechaEntrada" 
                                            DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderText="Fecha de Entrada">
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PesoDeEntrada" DataFormatString="{0:n}" 
                                            HeaderText="Peso De Entrada" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaSalida" 
                                            DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderText="Fecha Salida">
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PesoDeSalida" DataFormatString="{0:n}" 
                                            HeaderText="Peso De Salida" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="pesonetoentrada" DataFormatString="{0:n}" 
                                            HeaderText="Peso Neto Entrada" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="pesonetosalida" DataFormatString="{0:n}" 
                                            HeaderText="Peso Neto Salida" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="humedad" DataFormatString="{0:n}" 
                                            HeaderText="Humedad" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="dctoHumedad" DataFormatString="{0:n}" 
                                            HeaderText="Dcto x Humedad" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                BOLETAS DISPONIBLES PARA INGRESAR</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvBoletasAdd" runat="server" AutoGenerateColumns="False" 
                                    PageSize="100">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRowSelected" runat="server" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkBolToAddSelADD" runat="server" AutoPostBack="True" 
                                                    oncheckedchanged="chkBolToAddSelADD_CheckedChanged" />
                                            </HeaderTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NumeroBoleta" HeaderText="Boleta" />
                                        <asp:BoundField DataField="codigoClienteProvArchivo" 
                                            HeaderText="Codigo Cliente" />
                                        <asp:BoundField DataField="NombreProductor" HeaderText="Nombre">
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Producto" HeaderText="Codigo Producto" />
                                        <asp:BoundField DataField="Placas" HeaderText="Placas" />
                                        <asp:BoundField DataField="FechaEntrada" 
                                            DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderText="Fecha de Entrada">
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PesoDeEntrada" DataFormatString="{0:n}" 
                                            HeaderText="Peso De Entrada" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaSalida" 
                                            DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderText="Fecha Salida">
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PesoDeSalida" DataFormatString="{0:n}" 
                                            HeaderText="Peso De Salida" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="pesonetoentrada" DataFormatString="{0:n}" 
                                            HeaderText="Peso Neto Entrada" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="pesonetosalida" DataFormatString="{0:n}" 
                                            HeaderText="Peso Neto Salida" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="humedad" DataFormatString="{0:n}" 
                                            HeaderText="Humedad" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="dctoHumedad" DataFormatString="{0:n}" 
                                            HeaderText="Dcto x Humedad" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnAddBolToDBList" runat="server" 
                                    onclick="btnAddBolToDBList_Click" Text="Agregar Boletas a Lista" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TablaField">
                                BOLETAS QUE SERÁN INGRESADAS AL SISTEMA:</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvBoletasAddiNDB" runat="server" AutoGenerateColumns="False" 
                                    PageSize="100" onrowcancelingedit="gvBoletasAddiNDB_RowCancelingEdit" 
                                    onrowediting="gvBoletasAddiNDB_RowEditing" 
                                    onrowupdating="gvBoletasAddiNDB_RowUpdating">
                                    <Columns>
                                        <asp:CommandField ButtonType="Button" CancelText="Cancelar" 
                                            DeleteText="Eliminar" EditText="Editar" ShowEditButton="True" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRowToDel" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NumeroBoleta" HeaderText="Boleta" ReadOnly="True" />
                                        <asp:BoundField DataField="Ticket" 
                                            HeaderText="Ticket" />
                                        <asp:BoundField DataField="codigoClienteProvArchivo" 
                                            HeaderText="Codigo Cliente" ReadOnly="True" />
                                        <asp:BoundField DataField="NombreProductor" HeaderText="Nombre" ReadOnly="True">
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Producto" HeaderText="Codigo Producto" 
                                            ReadOnly="True" />
                                        <asp:BoundField DataField="Placas" HeaderText="Placas" />
                                        <asp:BoundField DataField="FechaEntrada" 
                                            DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderText="Fecha de Entrada" 
                                            ReadOnly="True">
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PesoDeEntrada" DataFormatString="{0:n}" 
                                            HeaderText="Peso De Entrada" ItemStyle-HorizontalAlign="Right" 
                                            ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FechaSalida" 
                                            DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderText="Fecha Salida" 
                                            ReadOnly="True">
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PesoDeSalida" DataFormatString="{0:n}" 
                                            HeaderText="Peso De Salida" ItemStyle-HorizontalAlign="Right" 
                                            ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="pesonetoentrada" DataFormatString="{0:n}" 
                                            HeaderText="Peso Neto Entrada" ItemStyle-HorizontalAlign="Right" 
                                            ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="pesonetosalida" DataFormatString="{0:n}" 
                                            HeaderText="Peso Neto Salida" ItemStyle-HorizontalAlign="Right" 
                                            ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="humedad" DataFormatString="{0:n}" 
                                            HeaderText="Humedad" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="dctoHumedad" DataFormatString="{0:n}" 
                                            HeaderText="Dcto x Humedad" ItemStyle-HorizontalAlign="Right" 
                                            ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnQuitarBoletas" runat="server" 
                                    onclick="btnQuitarBoletas_Click" Text="Quitar Boletas de lista" />
                            </td>
                        </tr>
                       
    	                <tr>
                            <td>
                                <table>
                                	<tr>
                                		<td class="TablaField">BOLETAS A AGREGAR:</td><td>
                                            <asp:Label ID="lblBoletasAgregar" runat="server" Text="0"></asp:Label></td>
                                	</tr>
                                    <tr>
                                        <td class="TablaField">
                                            BOLETAS AGREGADAS: </td>
                                        <td>
                                            <asp:Label ID="lblBoletasAdded" runat="server" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TablaField">
                                            AGREGAR LAS BOLETAS AL CICLO:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlCiclos" runat="server" DataSourceID="sdsCiclos" 
                                                DataTextField="CicloName" DataValueField="cicloID">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [CicloName]">
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TablaField">
                                            AGREGAR BOLETAS A BODEGA:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlBodegaBoletasAdd" runat="server" 
                                                DataSourceID="sdsBodegas" DataTextField="bodega" DataValueField="bodegaID">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="sdsBodegas" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="btnAddBoletasInDB" runat="server" 
                                                onclick="btnAddBoletasInDB_Click" Text="Agregar Boletas" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                       
    	                <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                       
    	            </table>
    	        </td>
    	    </tr>
            
            </table>
            </asp:panel>
            </td></tr>
          
        </table>
</ContentTemplate>
    </asp:UpdatePanel>
    	
      
</asp:Content>
