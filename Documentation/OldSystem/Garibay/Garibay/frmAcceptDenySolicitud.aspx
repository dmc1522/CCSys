<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmAcceptDenySolicitud.aspx.cs" Inherits="Garibay.frmAcceptDenySolicitud" Title="Solicitudes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <table>
  <tr>
   <td class="TableHeader">AUTORIZACION DE SOLICITUDES</td>
  </tr>
  <tr>
   <td>
    
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
           <Columns>
               <asp:CommandField ShowSelectButton="True" />
               <asp:BoundField HeaderText="ID Solicitud " />
               <asp:BoundField HeaderText="Credito" />
               <asp:BoundField HeaderText="Productor" />
               <asp:BoundField HeaderText="Monto" />
               <asp:BoundField HeaderText="Plazo" />
               <asp:BoundField HeaderText="Superficie a sembrar" />
               <asp:BoundField HeaderText="Experiencia" />
               <asp:BoundField HeaderText="Valor de garantías" />
               <asp:BoundField HeaderText="Recursos propios" />
           </Columns>
       </asp:GridView>
    
   </td>
  </tr>
  <tr>
   <td align="center">
       <asp:Button ID="btnAutorizar" runat="server" CssClass="Button" 
           Text="Marcar como autorizada" />
       <asp:Button ID="btnNoAutorizar" runat="server" 
           Text="Marcar como No autorizada" />
       <asp:Button ID="btnCancelar" runat="server" CssClass="Button" Text="Cancelar" />
      </td>
  </tr>
 </table>
</asp:Content>
