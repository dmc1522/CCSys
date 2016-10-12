<%@ Page Title="Presentaciones de productos" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmPresentaciones.aspx.cs" Inherits="Garibay.frmPresentaciones" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:CheckBox ID="chkAgregarNuevo" runat="server" 
        Text="AGREGAR NUEVA PRESENTACION" />
    <asp:Panel ID="Panel1" runat="server">
        <asp:DetailsView ID="dvPresentacion" runat="server" 
    AutoGenerateRows="False" DataSourceID="odsPresentaciones" Height="50px" 
    Width="125px" DefaultMode="Insert" onitemcommand="dvPresentacion_ItemCommand" 
            onitemcreated="dvPresentacion_ItemCreated" 
            oniteminserted="dvPresentacion_ItemInserted">
            <Fields>
                <asp:BoundField DataField="Presentacion" HeaderText="Presentacion" 
            SortExpression="Presentacion" />
                <asp:BoundField DataField="Peso" HeaderText="Peso" SortExpression="Peso" />
                <asp:CommandField ShowInsertButton="True" ButtonType="Button" 
                    CancelText="Cancelar" InsertText="Agregar" />
            </Fields>
        </asp:DetailsView>
    </asp:Panel>
    <cc1:CollapsiblePanelExtender ID="Panel1_CollapsiblePanelExtender" 
        runat="server" CollapseControlID="chkAgregarNuevo" Collapsed="True" 
        Enabled="True" ExpandControlID="chkAgregarNuevo" TargetControlID="Panel1">
    </cc1:CollapsiblePanelExtender>
        <asp:ObjectDataSource ID="odsPresentaciones" runat="server" 
            DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="Search" 
            TypeName="Presentaciones" UpdateMethod="Update">
            <DeleteParameters>
                <asp:Parameter Name="presentacionID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="presentacionID" Type="Int32" />
                <asp:Parameter Name="presentacion" Type="String" />
                <asp:Parameter Name="peso" Type="Double" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="presentacion" Type="String" />
                <asp:Parameter Name="peso" Type="Double" />
            </InsertParameters>
        </asp:ObjectDataSource>
        <asp:GridView ID="gvPresentaciones" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="PresentacionID" DataSourceID="odsPresentaciones" 
            onrowupdated="gvPresentaciones_RowUpdated" 
            onrowupdating="gvPresentaciones_RowUpdating">
            <Columns>
                <asp:BoundField DataField="PresentacionID" HeaderText="ID" ReadOnly="True" 
                    SortExpression="PresentacionID">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Presentacion" HeaderText="Presentacion" 
                    SortExpression="Presentacion" />
                <asp:BoundField DataField="Peso" DataFormatString="{0:N0}" HeaderText="Peso" 
                    SortExpression="Peso">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:CommandField ButtonType="Button" CancelText="Cancelar" 
                    DeleteText="Eiminar" EditText="Modificar" 
                    ShowEditButton="True" />
            </Columns>
        </asp:GridView>
</asp:Content>
