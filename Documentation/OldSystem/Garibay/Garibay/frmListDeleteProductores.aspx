<%@ Page Language="C#" Theme = "skinverde" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListDeleteProductores.aspx.cs" Inherits="Garibay.frmListDeleteProductores" Title="Lista de productores" %>



<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

        
        

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

        
        

    <table >
        <tr>
         <td class="TableHeader">LISTA DE PRODUCTORES</td>
        </tr>
        <tr>
         <td>
            <table >
            	<tr class =TableHeader">
            		<td class="TableHeader">Campos a mostrar:</td>
            	</tr>
            	<tr>
            	    <td class="TablaField">
                        <asp:CheckBoxList ID="cblColToShow" runat="server" RepeatColumns="6">
                            <asp:ListItem>IFE</asp:ListItem>
                            <asp:ListItem>Domicilio</asp:ListItem>
                            <asp:ListItem>Municipio</asp:ListItem>
                            <asp:ListItem>Estado</asp:ListItem>
                            <asp:ListItem>CP</asp:ListItem>
                            <asp:ListItem>RFC</asp:ListItem>
                            <asp:ListItem>Sexo</asp:ListItem>
                            <asp:ListItem>Tel. Trabajo</asp:ListItem>
                            <asp:ListItem>Celular</asp:ListItem>
                            <asp:ListItem>Fax</asp:ListItem>
                            <asp:ListItem>Email</asp:ListItem>
                            <asp:ListItem>Estado civil</asp:ListItem>
                            <asp:ListItem>Regimen</asp:ListItem>
                            <asp:ListItem>Fecha de ingreso</asp:ListItem>
                            <asp:ListItem>Ultima modificación</asp:ListItem>
                            <asp:ListItem Value="F. Nacimiento">F. Nacimiento</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
            	</tr>
            	<tr>
            	    <td class =TablaField">
                        <asp:Button ID="btnActualizaColumna" runat="server" Text="Actualiza Columnas" 
                            onclick="btnActualizaColumna_Click" />
                        <asp:Button ID="btnSeleccionaTodas" runat="server" 
                            onclick="btnSeleccionaTodas_Click" Text="Seleccionar Todas" />
                        <asp:Button ID="btnLimpiaFiltros" runat="server" 
                            onclick="btnLimpiaFiltros_Click" Text="Quitar Selección" />
                    </td>
            	</tr>
            </table>
         </td>
        </tr>
        <tr>
         <td >

                <asp:Button ID="btnAgregar" runat="server" 
                    onclick="btnAgregar_Click" Text="Agregar nuevo" />

                <asp:Button ID="btnVerEstadodeCuenta" runat="server" CssClass="centrado" 
                    style="text-align: center" Text="Estado de cuenta" 
                    onclick="btnVerEstadodeCuenta_Click" />

                <asp:Button ID="btnModificar" runat="server" 
                    style="text-align: center; width: 89px;" Text="Modificar" 
                    onclick="btnModificar_Click" />
                <asp:Button ID="btnEliminar" runat="server" 
                    style="text-align: center; height: 26px;" Text="Eliminar" 
                    onclick="btnEliminar_Click" />

                

                <asp:Button ID="btnPrintList" runat="server" 
                    style="text-align: center; height: 26px;" Text="Imprimir lista" onclick="btnPrintList_Click" 
                     />

                

                <asp:Button ID="btnExportarAExcel" runat="server" 
                    onclick="btnExportarAExcel_Click" Text="Exportar A Excel" />

                

            </td>
        </tr>
        <tr>
         <td>

                <asp:GridView ID="gridProductores" runat="server" AutoGenerateColumns="False" AllowSorting="True" 
                    onselectedindexchanged="gridProductores_SelectedIndexChanged" 
                    DataSourceID="SqlDataSource1" AllowPaging="True" 
                    DataKeyNames="productorID,apaterno,amaterno,nombre" PageSize="500" 
                    ondatabound="gridProductores_DataBound" 
                    onrowdatabound="gridProductores_RowDataBound" >
                 
        <Columns>
                    <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                        ShowSelectButton="True" />
                    <asp:BoundField DataField="productorID" HeaderText="# Productor" 
                        InsertVisible="False" ReadOnly="True" SortExpression="productorID" />
                    <asp:BoundField DataField="apaterno" HeaderText="A. Paterno" 
                        SortExpression="apaterno" />
                    <asp:BoundField DataField="amaterno" HeaderText="A. Materno" 
                        SortExpression="amaterno" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre(s)" 
                        SortExpression="nombre" >
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechanacimiento" DataFormatString="{0:dd/MM/yyyy}" 
                        HeaderText="F. Nacimiento" SortExpression="fechanacimiento" />
                    <asp:BoundField DataField="IFE" HeaderText="IFE" SortExpression="IFE" />
                    <asp:BoundField DataField="CURP" HeaderText="CURP" SortExpression="CURP" />
                    <asp:BoundField DataField="domicilio" HeaderText="Domicilio" 
                        SortExpression="domicilio" >
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="poblacion" HeaderText="Población" 
                        SortExpression="poblacion" >
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="municipio" HeaderText="Municipio" 
                        SortExpression="municipio" />
                    <asp:BoundField DataField="estado" HeaderText="Estado" 
                        SortExpression="estado" >
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CP" HeaderText="CP" SortExpression="CP" />
                    <asp:BoundField DataField="RFC" HeaderText="RFC" SortExpression="RFC" />
                    <asp:BoundField DataField="sexo" HeaderText="Sexo" SortExpression="sexo" />
                    <asp:BoundField DataField="telefono" HeaderText="Telefono" 
                        SortExpression="telefono" />
                    <asp:BoundField DataField="telefonotrabajo" HeaderText="Tel. Trabajo" 
                        SortExpression="telefonotrabajo" />
                    <asp:BoundField DataField="celular" HeaderText="Celular" 
                        SortExpression="celular" />
                    <asp:BoundField DataField="fax" HeaderText="Fax" SortExpression="fax" />
                    <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                    <asp:BoundField DataField="EstadoCivil" HeaderText="Estado civil" 
                        SortExpression="EstadoCivil" >
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Regimen" HeaderText="Regimen" 
                        SortExpression="Regimen" >
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="storeTS" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" 
                        HeaderText="Fecha de ingreso" SortExpression="storeTS" />
                    <asp:BoundField DataField="updateTS" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" 
                        HeaderText="Ultima modificación" SortExpression="updateTS" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkEditar" runat="server" 
                                NavigateUrl="~/frmListDeleteProductores.aspx">Editar</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
    </asp:GridView>

                        
        

        
        


                        
        

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
                
                    
                    
                    SelectCommand="SELECT Productores.productorID, Productores.apaterno, Productores.amaterno, Productores.nombre, Productores.fechanacimiento, Productores.IFE, Productores.CURP, Productores.domicilio, Productores.poblacion, Productores.municipio, Estados.estado, Productores.CP, Productores.RFC, Sexo.sexo, Productores.telefono, Productores.telefonotrabajo, Productores.celular, Productores.fax, Productores.email, EstadosCiviles.EstadoCivil, Regimenes.Regimen, Productores.storeTS, Productores.updateTS FROM Productores LEFT OUTER JOIN EstadosCiviles ON Productores.estadocivilID = EstadosCiviles.estadoCivilID LEFT OUTER JOIN Regimenes ON Productores.regimenID = Regimenes.regimenID LEFT OUTER JOIN Estados ON Productores.estadoID = Estados.estadoID LEFT OUTER JOIN Sexo ON Productores.sexoID = Sexo.sexoID ORDER BY Productores.apaterno, Productores.amaterno, Productores.nombre">
            </asp:SqlDataSource>
            
        

                        
        

        
        


                        
        

            </td>
        </tr>
        </table>

        
        

            
        


                        
        

</asp:Content>







<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    
</asp:Content>








