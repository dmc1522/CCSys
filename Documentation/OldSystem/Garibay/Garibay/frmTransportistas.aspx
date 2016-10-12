<%@ Page Title="Transportistas" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmTransportistas.aspx.cs" Inherits="Garibay.frmTransportistas" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel runat="Server" id="pnlUpdateTransportista">
    <ContentTemplate>
    
    <asp:CheckBox ID="chkNewTransportista" runat="server" 
        Text="AGREGAR NUEVO TRANSPORTISTA" />
    <asp:Panel ID="pnlAddTransportista" runat="server">
        <asp:DetailsView ID="dvTransportista" runat="server" Height="50px" 
            Width="125px" AutoGenerateRows="False" DataKeyNames="transportistaID" 
            DataSourceID="sdsTransportista" DefaultMode="Insert" 
            oniteminserted="dvTransportista_ItemInserted">
            <Fields>
                <asp:BoundField DataField="transportistaID" HeaderText="ID:" 
                    SortExpression="transportistaID" InsertVisible="False" ReadOnly="True" />
                <asp:TemplateField HeaderText="A. Paterno:" SortExpression="apaterno">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("apaterno") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("apaterno") %>' 
                            Width="250px"></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("apaterno") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="A. Materno:" SortExpression="amaterno">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("amaterno") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("amaterno") %>' 
                            Width="250px"></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("amaterno") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nombres:" SortExpression="nombres">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("nombres") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("nombres") %>' 
                            Width="250px"></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("nombres") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Domicilio" SortExpression="Domicilio">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Domicilio") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Domicilio") %>' 
                            Width="250px"></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Domicilio") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Poblacion" SortExpression="Poblacion">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Poblacion") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Poblacion") %>' 
                            Width="250px"></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Poblacion") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ButtonType="Button" ShowInsertButton="True" 
                    CancelText="Cancelar" InsertText="Agregar" />
            </Fields>
        </asp:DetailsView>          
        <asp:SqlDataSource ID="sdsTransportista" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
            DeleteCommand="DELETE FROM [Transportistas] WHERE [transportistaID] = @transportistaID" 
            InsertCommand="INSERT INTO [Transportistas] ([apaterno], [amaterno], [nombres], [Domicilio], [Poblacion]) VALUES (@apaterno, @amaterno, @nombres, @Domicilio, @Poblacion)" 
            SelectCommand="SELECT * FROM [Transportistas] ORDER BY [amaterno], [apaterno], [nombres]" 
            
            
            
            UpdateCommand="UPDATE [Transportistas] SET [apaterno] = @apaterno, [amaterno] = @amaterno, [nombres] = @nombres, [Domicilio] = @Domicilio, [Poblacion] = @Poblacion WHERE [transportistaID] = @transportistaID">
            <DeleteParameters>
                <asp:Parameter Name="transportistaID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="apaterno" Type="String" />
                <asp:Parameter Name="amaterno" Type="String" />
                <asp:Parameter Name="nombres" Type="String" />
                <asp:Parameter Name="Domicilio" Type="String" />
                <asp:Parameter Name="Poblacion" Type="String" />
                <asp:Parameter Name="transportistaID" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="apaterno" Type="String" />
                <asp:Parameter Name="amaterno" Type="String" />
                <asp:Parameter Name="nombres" Type="String" />
                <asp:Parameter Name="Domicilio" Type="String" />
                <asp:Parameter Name="Poblacion" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
        <asp:Label ID="lblAddTransportistaResult" runat="server" Font-Size="X-Large"></asp:Label>
    </asp:Panel>
    <cc1:CollapsiblePanelExtender ID="pnlAddTransportista_CollapsiblePanelExtender" 
        runat="server" CollapseControlID="chkNewTransportista" Collapsed="True" 
        Enabled="True" ExpandControlID="chkNewTransportista" 
        TargetControlID="pnlAddTransportista">
    </cc1:CollapsiblePanelExtender>
    <cc1:CollapsiblePanelExtender ID="Panel1_CollapsiblePanelExtender" 
        runat="server" Enabled="True" TargetControlID="pnlAddTransportista">
    </cc1:CollapsiblePanelExtender>
    
    
    <asp:GridView ID="gvTransportistas" runat="server" 
    AutoGenerateColumns="False" DataKeyNames="transportistaID" 
    DataSourceID="sdsTransportista" onrowdeleted="gvTransportistas_RowDeleted">
        <Columns>
            <asp:CommandField ButtonType="Button" DeleteText="Eliminar" 
                ShowDeleteButton="True" />
            <asp:BoundField DataField="transportistaID" HeaderText="ID" 
                InsertVisible="False" ReadOnly="True" SortExpression="transportistaID" />
            <asp:BoundField DataField="apaterno" HeaderText="A. Paterno" 
                SortExpression="apaterno">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="amaterno" HeaderText="A. Materno" 
                SortExpression="amaterno">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="nombres" HeaderText="Nombres" 
                SortExpression="nombres">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="Domicilio" HeaderText="Domicilio" 
                SortExpression="Domicilio">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="Poblacion" HeaderText="Poblacion" 
                SortExpression="Poblacion">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

