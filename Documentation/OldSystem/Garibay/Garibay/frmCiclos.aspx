<%@ Page Title="Ciclos" Theme="skinrojo" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmCiclos.aspx.cs" Inherits="Garibay.frmCiclos" %>
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

    <asp:Panel ID="panelLista_ciclos" runat="server">
    <table>
    <tr>
        <td class="TableHeader"  >
            CICLOS</td>
    </tr>

    <tr>
        <td align="left">
    
    
            <asp:Button ID="btnAgregarLista" runat="server" CssClass="Button" 
            Text="Agregar" onclick="btnAgregarLista_Click" />
            <asp:Button ID="btnModificarLista" runat="server" CssClass="Button" 
            Text="Modificar" onclick="btnModificarLista_Click" />
            <asp:Button ID="btnEliminar" runat="server" CssClass="Button" Text="Eliminar" 
            CausesValidation="False" onclick="btnEliminar_Click" />
    
    
        </td>
    </tr>
    <tr>
    
       <td>
       
            
    

                <asp:GridView ID="gridCiclos" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                    
                    DataKeyNames="cicloID,CicloName,fechaInicio,fechaFinZona1,fechaFinZona2,Montoporhectarea,cerrado" 
                    DataSourceID="SqlDataSource1" ForeColor="White" 
                    GridLines="None"  
                    Width="100%" onselectedindexchanged="gridCiclos_SelectedIndexChanged" 
                 >
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <HeaderStyle CssClass="TableHeader" />
                    <AlternatingRowStyle BackColor="White" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <Columns>
                        <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                            ShowSelectButton="True" />
                        <asp:BoundField DataField="cicloID" HeaderText="Ciclo ID" InsertVisible="False" 
                            ReadOnly="True" SortExpression="cicloID" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CicloName" HeaderText="Nombre de Ciclo" 
                            SortExpression="CicloName" />
                        <asp:BoundField DataField="fechaInicio" HeaderText="Fecha de Inicio" 
                            SortExpression="fechaInicio" DataFormatString="{0:dd/MM/yyyy}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaFinZona1" HeaderText="Fecha de fin Zona 1" 
                            SortExpression="fechaFinZona1" DataFormatString="{0:dd/MM/yyyy}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaFinZona2" DataFormatString="{0:dd/MM/yyyy}" 
                            HeaderText="Fecha de fin Zona 2" SortExpression="fechaFinZona2">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Montoporhectarea" HeaderText="Monto por hectárea" 
                            SortExpression="Montoporhectarea" DataFormatString="{0:c}" >
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="cerrado" HeaderText="Cerrado" 
                            SortExpression="cerrado" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
                    SelectCommand="SELECT * FROM [Ciclos]"></asp:SqlDataSource>
               
    
        </td>
    </tr>
    
   
    
    </table>
    </asp:Panel>
        <asp:Panel ID="pnlNuevo_ciclo" runat="server">
    <table>
    <tr>
        <td colspan="2" class="TableHeader">
            <asp:Label ID="lblCiclos" runat="server" Text="AGREGAR NUEVO CICLO"></asp:Label>
    
        </td>
    </tr>
    
    <tr>
        <td class="TablaField" >Nombre del Ciclo:
        </td>
         
        <td>
            <asp:TextBox ID="txtNombreCiclo" runat="server" Width="200px"></asp:TextBox>
           
        </td>
        <td>
             <asp:RequiredFieldValidator ID="RFVNameCiclo" runat="server" 
                ErrorMessage="El campo del Nombre es necesario" ControlToValidate="txtNombreCiclo" 
                Font-Size="Medium" CssClass="Validator"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="TablaField">
            
            
            Inicio de Ciclo: 
    
        </td>
        <td>
            <asp:TextBox ID="txtInicioCiclo" runat="server" ReadOnly="True" Width="200px"></asp:TextBox>
            <rjs:PopCalendar ID="PopCalendar1" runat="server" 
                onselectionchanged="PopCalendar1_SelectionChanged" style="width: 14px" 
                Separator="/" Control="txtInicioCiclo" />
                                                  
        
        </td>
        <td>
         <asp:RequiredFieldValidator ID="RFVInicioCiclo" runat="server" 
                ControlToValidate="txtInicioCiclo" ErrorMessage="El campo del Inicio de Ciclo es necesario" 
                Font-Size="Medium" CssClass="Validator"></asp:RequiredFieldValidator>
        
        </td>
    </tr>
    <tr>
        <td class="TablaField" >
            
            
            
            Fin de Ciclo Zona 1: 
    
        </td>
        <td>
        
            <asp:TextBox ID="txtFinCiclo" runat="server" ReadOnly="True" Width="200px"></asp:TextBox>
                             <rjs:PopCalendar ID="PopCalendar2" runat="server" 
                                onselectionchanged="PopCalendar2_SelectionChanged" 
                Separator="/" Control="txtFinCiclo"/>
        
    
        </td>
        <td>
                                 <asp:RequiredFieldValidator ID="RFVFinCiclo" runat="server" 
                            ControlToValidate="txtFinCiclo" ErrorMessage="El campo de Fin de Ciclo Zona 1 es necesario" 
                            Font-Size="Medium" CssClass="Validator"></asp:RequiredFieldValidator>
        </td>
    </tr>
        <tr>
        <td class="TablaField" >
            
            
            
            Fin de Ciclo Zona 2: 
    
        </td>
        <td>
        
            <asp:TextBox ID="txtFinCiclo2" runat="server" ReadOnly="True" Width="200px"></asp:TextBox>
                             <rjs:PopCalendar ID="PopCalendar3" runat="server" 
                                onselectionchanged="PopCalendar3_SelectionChanged" 
                Separator="/" Control="txtFinCiclo2"/>
        
    
        </td>
        <td>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtFinCiclo" ErrorMessage="El campo de Fin de Ciclo Zona 2 es necesario" 
                            Font-Size="Medium" CssClass="Validator"></asp:RequiredFieldValidator>
        </td>
    </tr>

    <tr>
        <td class="TablaField">
            
           El Ciclo esta cerrado?
    
        </td>
        <td align="left">
        
        
        
            <asp:CheckBox ID="chkCiclo_Cerrado" runat="server" Font-Size="Large" 
                Text="SI" />
        
    
        </td>
        <td>
        
        </td>
    </tr>
    <tr>
        <td class="TablaField">
            
            Monto por Hectárea: 
    
        </td>
        <td>
        
            <asp:TextBox ID="TxtMontoXhec" runat="server" Width="200px"></asp:TextBox>
        
        </td>
        <td>
                <asp:RequiredFieldValidator ID="RFVMontoXhec" runat="server" 
                ControlToValidate="TxtMontoXhec" ErrorMessage="El campo del Monto es necesario" 
                Font-Size="Medium" CssClass="Validator"></asp:RequiredFieldValidator>
                <br />
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToValidate="TxtMontoXhec" CssClass="Validator" 
                    ErrorMessage="Ingrese un monto válido" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
        </td>
    </tr>
    
    <tr>
        <td>
        </td>
        <td>
            &nbsp;</td>
        <td>
        
            &nbsp;</td>
    </tr>
       
    <tr>
        <td colspan="2" align="center">
    
    
            <asp:Button ID="btnAceptar" runat="server" CssClass="Button" 
            Text="Agregar" onclick="btnAceptar_Click" />
            <asp:Button ID="btnModificarCiclo" runat="server" CssClass="Button" 
            Text="Modificar" onclick="btnModificarCiclo_Click" />
            <asp:Button ID="btnCancelar" runat="server" CssClass="Button" Text="Cancelar" 
            CausesValidation="False" onclick="cmdCancelar_Click" />
    
    
        </td>
    </tr>
        </table>
        </asp:Panel>
</asp:Content>

