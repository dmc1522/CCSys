<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListDeleteCheques.aspx.cs" Inherits="Garibay.frmListDeleteCheques" Title="Cheques" %>
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
                           <asp:Label ID="lblMensajetitle" runat="server" SkinID="blMensajeTitle" 
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
    		<td class="TableHeader">LISTA DE CHEQUES</td>
    	</tr>
    	<tr>
    	  <td> 
              <asp:Button ID="btnAgregarDeLista" runat="server" CssClass="Button" 
                  Text="Agregar" Width="100px" onclick="btnAgregarDeLista_Click"  />
              <asp:Button ID="btnModificarDeLista" runat="server" CssClass="Button" 
                  Text="Modificar" Width="100px" onclick="btnModificarDeLista_Click"  />
              <asp:Button ID="btnEliminar" runat="server" CssClass="Button" Text="Eliminar" 
                  Width="100px" onclick="btnEliminar_Click" />
            </td>
    	</tr>
    	<tr>
    	 <td>
    	 
    	     <asp:GridView ID="gridCheques" runat="server" AllowPaging="True" 
                 AllowSorting="True" AutoGenerateColumns="False" 
                 DataSourceID="SqlDataSource2">
                 <Columns>
                     <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                         ShowSelectButton="True" />
                     <asp:BoundField DataField="chequeID" HeaderText="# Cheque" 
                         InsertVisible="False" ReadOnly="True" SortExpression="chequeID" >
                         <ItemStyle HorizontalAlign="Right" />
                     </asp:BoundField>
                     <asp:BoundField DataField="NumeroDeCuenta" HeaderText="No. de Cuenta" 
                         SortExpression="NumeroDeCuenta" />
                     <asp:BoundField DataField="nombre" HeaderText="Nombre" 
                         SortExpression="nombre" />
                     <asp:BoundField DataField="nombrequienrecibe" HeaderText="Recibió" 
                         SortExpression="nombrequienrecibe" />
                     <asp:BoundField DataField="chequestatus" HeaderText="Status" 
                         SortExpression="chequestatus" />
                     <asp:BoundField DataField="fecha" HeaderText="Fecha" 
                         DataFormatString="{0:dd/MM/yyyy}" SortExpression="fecha" >
                         <ItemStyle HorizontalAlign="Right" />
                     </asp:BoundField>
                     <asp:BoundField DataField="fechaingreso" HeaderText="Fecha de ingreso" DataFormatString="{0:dd/MM/yyyy}"
                         SortExpression="fechaingreso" >
                         <ItemStyle HorizontalAlign="Right" />
                     </asp:BoundField>
                     <asp:BoundField DataField="monto" HeaderText="Monto" SortExpression="monto" 
                         DataFormatString="{0:c}" >
                         <ItemStyle HorizontalAlign="Right" />
                     </asp:BoundField>
                 </Columns>
             </asp:GridView>
             <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                 ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                 SelectCommand="SELECT Cheques.fechaingreso, Cheques.fecha, Cheques.monto, Cheques.nombre, Cheques.nombrequienrecibe, ChequeStatus.chequestatus, CuentasDeBanco.NumeroDeCuenta, Cheques.chequeID FROM Cheques INNER JOIN ChequeStatus ON Cheques.chequestatusID = ChequeStatus.chequestatusID INNER JOIN CuentasDeBanco ON Cheques.cuentaID = CuentasDeBanco.cuentaID">
             </asp:SqlDataSource>
    	 
    	 </td>
    	</tr>
    </table>
</asp:Content>
