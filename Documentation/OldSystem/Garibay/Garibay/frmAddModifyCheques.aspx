<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" Theme="skinverde" CodeBehind="frmAddModifyCheques.aspx.cs" Inherits="Garibay.frmAddModifyCheques" Title="Cheques" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .TablaField
        {
            text-align: center;
        }
        .TableHeader
        {
            text-align: center;
        }
    </style>
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
            <tr>
                <td align="center" >
                    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" 
                        Height="50px" Width="486px" DataSourceID="SqlDataSource4">
                        <Fields>
                            <asp:BoundField DataField="chequenumero" HeaderText="# Cheque:" 
                                SortExpression="chequenumero" />
                            <asp:BoundField DataField="NumeroDeCuenta" HeaderText="Numero de Cuenta:" 
                                SortExpression="NumeroDeCuenta" />
                            <asp:BoundField DataField="CicloName" HeaderText="CicloName" 
                                SortExpression="CicloName" />
                            <asp:BoundField DataField="fecha" HeaderText="Fecha:" 
                                SortExpression="fecha" DataFormatString="{0:dd/MM/yyyy }" />
                            <asp:BoundField DataField="monto" HeaderText="Monto:" 
                                SortExpression="monto" DataFormatString="{0:c}" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre:" 
                                SortExpression="nombre" />
                            <asp:BoundField DataField="nombrequienrecibe" 
                                HeaderText="Nombre de quien recibe:" SortExpression="nombrequienrecibe" />
                            <asp:BoundField DataField="chequestatus" 
                                HeaderText="Status del cheque:" SortExpression="chequestatus" />
                            <asp:BoundField DataField="storeTS" HeaderText="Fecha ingreso:" 
                                SortExpression="storeTS" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" />
                            <asp:BoundField DataField="updateTS" HeaderText="Fecha de última modificación:" 
                                SortExpression="updateTS" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" />
                        </Fields>
                    </asp:DetailsView>
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
                        
                        
                        SelectCommand="SELECT Cheques.chequenumero, CuentasDeBanco.NumeroDeCuenta, Cheques.fecha, Cheques.monto, Cheques.nombre, Cheques.nombrequienrecibe, ChequeStatus.chequestatus, Cheques.storeTS, Cheques.updateTS, Ciclos.CicloName FROM Cheques INNER JOIN ChequeStatus ON Cheques.chequestatusID = ChequeStatus.chequestatusID INNER JOIN CuentasDeBanco ON Cheques.cuentaID = CuentasDeBanco.cuentaID INNER JOIN Ciclos ON Cheques.cicloID = Ciclos.cicloID WHERE (Cheques.chequeID = @chequeID)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtIDdetails" DefaultValue="-1" 
                                Name="chequeID" PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnAceptardetails" runat="server" CssClass="Button" 
                        onclick="btnAceptardetails_Click" Text="Aceptar" Width="135px" />
                </td>
            </tr>
        </table>

        
      
        </asp:Panel>
       <asp:Panel ID="paneldatosocultos" runat="server" Visible="False">
            <asp:TextBox ID="txtIDdetails" runat="server" 
    Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtIdtomodify" runat="server" Visible="False"></asp:TextBox>
     </asp:Panel>
       <asp:Panel ID="panelagregar" runat="server">
        <table>
         <tr>
          <td class="TableHeader" colspan="2">
              <asp:Label ID="lblHeader" runat="server" Text="AGREGAR CHEQUE"></asp:Label>
             </td>
          <td>
              &nbsp;</td>
         </tr>
            <tr>
                <td class="TablaField">
                    Número de cheque:</td>
                <td>
                    <asp:TextBox ID="txtNumCheque" runat="server" Width="162px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="valChequerequiered" runat="server" 
                        ControlToValidate="txtNumCheque" 
                        ErrorMessage="El campo número de cheque es necesario"></asp:RequiredFieldValidator>
                    <br />
                </td>
            </tr>
         <tr>
          <td class="TablaField">No. de Cuenta:</td>
          <td>
              <asp:DropDownList ID="cmbCuenta" runat="server" DataSourceID="SqlDataSource2" 
                  DataTextField="NumeroDeCuenta" DataValueField="cuentaID" Height="23px" 
                  Width="159px">
              </asp:DropDownList>
             </td>
          <td>
              <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                  ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                  SelectCommand="SELECT [cuentaID], [NumeroDeCuenta] FROM [CuentasDeBanco]">
              </asp:SqlDataSource>
             </td>
         </tr>
            <tr>
                <td class="TablaField">
                    Ciclo:</td>
                <td>
                    <asp:DropDownList ID="cmbCiclo" runat="server" DataSourceID="SqlDataSource5" 
                        DataTextField="CicloName" DataValueField="cicloID" Height="23px" Width="159px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos]">
                    </asp:SqlDataSource>
                </td>
            </tr>
         <tr>
          <td class="TablaField">Fecha:</td>
          <td>
              <asp:TextBox ID="txtFecha" runat="server" ReadOnly="True"></asp:TextBox>
              <rjs:PopCalendar ID="PopCalendar1" runat="server" 
                  OnSelectionChanged="PopCalendar1_SelectionChanged" Separator="/" />
             </td>
          <td>
              <asp:RequiredFieldValidator ID="valFecha" runat="server" 
                  ControlToValidate="txtFecha" ErrorMessage="El campo Fecha es necesario"></asp:RequiredFieldValidator>
             </td>
         </tr>
         <tr>
          <td class="TablaField">Monto:</td>
          <td>
              <asp:TextBox ID="txtMonto" runat="server" Width="152px" Height="22px"></asp:TextBox>
             </td>
          <td>
              <asp:RequiredFieldValidator ID="valMontoRequired" runat="server" 
                  ControlToValidate="txtMonto" ErrorMessage="El campo Monto es necesario"></asp:RequiredFieldValidator>
              <br />
              <asp:CompareValidator ID="valMonto" runat="server" ControlToValidate="txtMonto" 
                  ErrorMessage="Escriba una cantidad válida" Operator="DataTypeCheck" 
                  Type="Double"></asp:CompareValidator>
             </td>
         </tr>
         <tr>
          <td class="TablaField">Nombre:</td>
          <td>
              <asp:TextBox ID="txtNombre" runat="server" Width="287px"></asp:TextBox>
             </td>
          <td>
              <asp:RequiredFieldValidator ID="valNombreRequired" runat="server" 
                  ControlToValidate="txtNombre" ErrorMessage="El campo Nombre es necesario"></asp:RequiredFieldValidator>
             </td>
         </tr>
         <tr>
          <td class="TablaField">Nombre de quien recibe:</td>
          <td>
              <asp:TextBox ID="txtRecibe" runat="server" Width="289px"></asp:TextBox>
             </td>
          <td>
              <asp:RequiredFieldValidator ID="valRecibeRequired" runat="server" 
                  ControlToValidate="txtRecibe" ErrorMessage="El campo Recibió es necesario"></asp:RequiredFieldValidator>
             </td>
         </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnAceptar" runat="server" onclick="btnAceptar_Click" 
                        Text="Agregar" Width="135px" />
                    <asp:Button ID="btnModificar" runat="server" onclick="btnModificar_Click" 
                        Text="Modificar" Width="135px" />
                    <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                        Text="Cancelar" Width="147px" onclick="btnCancelar_Click" />
                </td>
            </tr>
        </table>
       </asp:Panel>
</asp:Content>
