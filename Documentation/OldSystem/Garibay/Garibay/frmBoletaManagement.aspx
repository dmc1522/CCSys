<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmBoletaManagement.aspx.cs" Inherits="Garibay.frmBoletaManagement" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DetailsView ID="DetailsView1" runat="server" DataSourceID="sdsBoleta" 
        Height="50px" Width="125px">
    </asp:DetailsView>
    <asp:SqlDataSource ID="sdsBoleta" runat="server"></asp:SqlDataSource>
    <asp:ObjectDataSource ID="odsBoleta" runat="server" 
        onselecting="odsBoleta_Selecting"></asp:ObjectDataSource>
    Boletas
</asp:Content>
