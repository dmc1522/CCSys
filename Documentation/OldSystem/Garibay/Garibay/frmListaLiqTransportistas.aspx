<%@ Page Title="Lista de Liquidaciones de Transportistas" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListaLiqTransportistas.aspx.cs" Inherits="Garibay.frmListaLiqTransportistas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
    	<tr>
    		<td class="TablaField">TRANSPORTISTA:
                <asp:DropDownList ID="ddlTransportistas" runat="server" 
                    DataSourceID="sdsTransportistas" DataTextField="Transportista" 
                    DataValueField="transportistaID" AutoPostBack="True" 
                    onselectedindexchanged="ddlTransportistas_SelectedIndexChanged">
                </asp:DropDownList>
    		    <asp:SqlDataSource ID="sdsTransportistas" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    
                    
                    SelectCommand="SELECT DISTINCT Transportistas.transportistaID, LTRIM(Transportistas.apaterno + SPACE(1) + Transportistas.amaterno + SPACE(1) + Transportistas.nombres) AS Transportista FROM Transportistas INNER JOIN LiquidacionTransportistas ON LiquidacionTransportistas.transportistaID = Transportistas.transportistaID UNION ALL SELECT - 1 AS Expr1, ' TODOS' AS Expr2 ORDER BY Transportista">
                </asp:SqlDataSource>
            </td> 
    	</tr>
    	<tr>
    		<td>
    <asp:GridView ID="gvLiquidacionesTrans" runat="server" 
        AutoGenerateColumns="False" DataKeyNames="LiqTransportistaID" 
        DataSourceID="sdsLiquidaciones" onrowdeleted="gvLiquidacionesTrans_RowDeleted">
        <Columns>
            <asp:CommandField ButtonType="Button" DeleteText="Eliminar" 
                ShowDeleteButton="True" />
            <asp:TemplateField HeaderText="Abrir" InsertVisible="False" 
                SortExpression="LiqTransportistaID">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("LiqTransportistaID") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" 
                        NavigateUrl='<%# GetLiqExistenteURL(Eval("LiqTransportistaID").ToString()) %>' 
                        Text='<%# Eval("LiqTransportistaID") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                HeaderText="Fecha" SortExpression="fecha" />
            <asp:BoundField DataField="Transportista" HeaderText="Transportista" 
                SortExpression="Transportista" />
            <asp:BoundField DataField="TotalAPagar" DataFormatString="{0:C2}" 
                HeaderText="Total. Pagar" SortExpression="TotalAPagar">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="TotalPagado" DataFormatString="{0:c2}" 
                HeaderText="Total. Pagado" SortExpression="TotalPagado">
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsLiquidaciones" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        
        SelectCommand="ReturnListaLiquidacionesTransportista" 
    SelectCommandType="StoredProcedure" DeleteCommand="DeleteLiquidacionesTransportista" 
                    DeleteCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlTransportistas" Name="TransportistaId" 
                PropertyName="SelectedValue" Type="Int32" />
            <asp:Parameter DefaultValue="-1" Name="LiquidacionId" Type="Int32" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="LiqTransportistaID" Type="Int32" />
        </DeleteParameters>
    </asp:SqlDataSource>
            </td> 
    	</tr>
    	<tr>
    		<td align="center">
                <asp:Label ID="labelResult" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
            </td> 
    	</tr>
    </table>
    </asp:Content>
