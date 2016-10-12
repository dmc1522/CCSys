<%@ Page Language="C#" Theme="skinrojo" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListDeletePredios.aspx.cs" Inherits="Garibay.frmListDeletePredios" Title="Lista de Predios"%>

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
    <table>
    	<tr>
    		<td class="TableHeader">LISTA DE PREDIOS</td>
    	</tr>
    	<tr>
    	  <td> 
    	    <table>
    	    	<tr>
    	    		<td class="TablaField">Mostrar:</td><td>
                    <asp:CheckBoxList ID="cblCampos" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem>Propietario</asp:ListItem>
                        <asp:ListItem>Norte</asp:ListItem>
                        <asp:ListItem>Sur</asp:ListItem>
                        <asp:ListItem>Este</asp:ListItem>
                        <asp:ListItem>Oeste</asp:ListItem>
                        <asp:ListItem>Fecha Creacion</asp:ListItem>
                        <asp:ListItem>Ultima modificación</asp:ListItem>
                        <asp:ListItem>userID</asp:ListItem>
                    </asp:CheckBoxList>
                    </td>
    	    	</tr>
    	    	<tr>
    	    		<td class="TablaField">Cuantos Predios mostrar por&nbsp; pagina:</td><td>
                    <asp:DropDownList ID="ddlCantXPage" runat="server">
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                        <asp:ListItem>200</asp:ListItem>
                        <asp:ListItem>500</asp:ListItem>
                        <asp:ListItem>1000</asp:ListItem>
                        <asp:ListItem>2000</asp:ListItem>
                        <asp:ListItem>Todos</asp:ListItem>
                    </asp:DropDownList>
                    </td>
    	    	</tr>
    	    </table>
          </td>
    	</tr>
    	<tr>
    	  <td> 
              <asp:Button ID="btnMostrarColumnas" runat="server" 
                  onclick="btnMostrarColumnas_Click" Text="Actualizar Lista" />
              <asp:Button ID="btnAgregarDeLista" runat="server" CssClass="Button" 
                  Text="Agregar" Width="100px" onclick="btnAgregarDeLista_Click" />
              <asp:Button ID="btnExportarAExcel" runat="server" 
                  onclick="btnExportarAExcel_Click" Text="Exportar a Excel" />
            </td>
    	</tr>
    </table>
    
     <asp:GridView ID="gridPredios" runat="server" AutoGenerateColumns="False" 
        DataSourceID="sdsPredios" onrowdatabound="gridPredios_RowDataBound" 
        DataKeyNames="predioID,folioPredio" onrowcommand="gridPredios_RowCommand">
         <Columns>
         <asp:BoundField DataField="folioPredio" HeaderText="Folio Predio" 
                 SortExpression="folioPredio">
             <ItemStyle HorizontalAlign="Right" />
             </asp:BoundField>
             <asp:BoundField DataField="RegistroAlterno" HeaderText="Registro Alterno" 
                 SortExpression="RegistroAlterno">
             <ItemStyle HorizontalAlign="Right" />
             </asp:BoundField>
             <asp:BoundField DataField="Superficie" DataFormatString="{0:n}" 
                 HeaderText="Superficie" SortExpression="Superficie">
             <ItemStyle HorizontalAlign="Right" />
             </asp:BoundField>
             <asp:BoundField DataField="Propietario" HeaderText="Propietario" 
                 ReadOnly="True" SortExpression="Propietario">
             <ItemStyle Wrap="False" />
             </asp:BoundField>
             <asp:BoundField DataField="folioPropietario" HeaderText="Folio Propietario" 
                 SortExpression="folioPropietario">
             <ItemStyle HorizontalAlign="Right" />
             </asp:BoundField>
             <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                 SortExpression="Productor">
             <ItemStyle Wrap="False" />
             </asp:BoundField>
             <asp:BoundField DataField="folioProductor" HeaderText="Folio Productor" 
                 SortExpression="folioProductor">
             <ItemStyle HorizontalAlign="Right" />
             </asp:BoundField>
             <asp:BoundField DataField="DDR" HeaderText="DDR" SortExpression="DDR">
             <ItemStyle HorizontalAlign="Right" />
             </asp:BoundField>
             <asp:BoundField DataField="CADER" HeaderText="CADER" SortExpression="CADER">
             <ItemStyle HorizontalAlign="Right" />
             </asp:BoundField>
             <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre">
             <ItemStyle Wrap="False" />
             </asp:BoundField>
             
             <asp:BoundField DataField="Ejido" HeaderText="Ejido" SortExpression="Ejido">
             <ItemStyle Wrap="False" />
             </asp:BoundField>
             <asp:BoundField DataField="codigoCultivo" HeaderText="Codigo Cultivo" 
                 SortExpression="codigoCultivo">
             <ItemStyle HorizontalAlign="Right" />
             </asp:BoundField>
             <asp:BoundField DataField="Cultivo" HeaderText="Cultivo" 
                 SortExpression="Cultivo" />
             
             <asp:BoundField DataField="FolioPROCAMPO" HeaderText="Folio PROCAMPO" 
                 SortExpression="FolioPROCAMPO" />
             
             <asp:BoundField DataField="Norte" HeaderText="Norte" SortExpression="Norte">
             <ItemStyle Wrap="False" />
             </asp:BoundField>
             <asp:BoundField DataField="Sur" HeaderText="Sur" SortExpression="Sur">
             <ItemStyle Wrap="False" />
             </asp:BoundField>
             <asp:BoundField DataField="Este" HeaderText="Este" SortExpression="Este">
             <ItemStyle Wrap="False" />
             </asp:BoundField>
             <asp:BoundField DataField="Oeste" HeaderText="Oeste" SortExpression="Oeste">
             <ItemStyle Wrap="False" />
             </asp:BoundField>
             <asp:BoundField DataField="storeTS" DataFormatString="{0:dd/MM/yyyy}" 
                 HeaderText="Fecha Creacion" SortExpression="storeTS" >
                 </asp:BoundField>
             <asp:BoundField DataField="updateTS" DataFormatString="{0:dd/MM/yyyy}" 
                 HeaderText="Ultima modificación" SortExpression="updateTS" >
                 </asp:BoundField>
        <asp:BoundField DataField="predioID" HeaderText="predioID" 
                 InsertVisible="False" ReadOnly="True" SortExpression="predioID" 
                 Visible="False">
             <ItemStyle HorizontalAlign="Right" />
             </asp:BoundField>
             <asp:BoundField DataField="userID" HeaderText="userID" 
                 SortExpression="userID" >
                 </asp:BoundField>
             <asp:TemplateField HeaderText="Abrir">
              <ItemTemplate>
              <asp:HyperLink ID="linkOpenPredio" runat="server">ABRIR</asp:HyperLink>
              </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Eliminar">
                 <EditItemTemplate>
                     <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                 </EditItemTemplate>
                 <ItemTemplate>
                     <asp:Button ID="btnDeletePredio" runat="server" Text="Eliminar" />
                 </ItemTemplate>
             </asp:TemplateField>
         </Columns>

    </asp:GridView>
    <asp:SqlDataSource ID="sdsPredios" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        
        
        SelectCommand="SELECT Predios.folioPredio, Predios.RegistroAlterno, Predios.Superficie, Predios.predioID, LTRIM(Propietarios.apaterno + ' ' + Propietarios.amaterno + ' ' + Propietarios.nombre) AS Propietario, Predios.folioPropietario, LTRIM(Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre) AS Productor, Predios.folioProductor, Predios.DDR, Predios.CADER, Predios.Nombre, Predios.Ejido, Predios.codigoCultivo, Cultivos.Cultivo, Predios.FolioPROCAMPO, Predios.Norte, Predios.Sur, Predios.Este, Predios.Oeste, Predios.storeTS, Predios.updateTS, Predios.userID FROM Predios INNER JOIN Productores ON Predios.productorID = Productores.productorID INNER JOIN Cultivos ON Predios.CultivoID = Cultivos.CultivoID LEFT OUTER JOIN Productores AS Propietarios ON Predios.propietarioID = Propietarios.productorID ORDER BY Productor">
    </asp:SqlDataSource>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .TableHeader
        {
            text-align: center;
        }
    </style>

</asp:Content>


