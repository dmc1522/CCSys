<%@ Page Title="Lista de Facturas de Proveedor" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListaFacturasProveedores.aspx.cs" Inherits="Garibay.frmListaFacturasProveedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel runat="Server" id="pnlCentral">
    <ContentTemplate>
    <table border="0" cellspacing="0" cellpadding="0">
	    <tr>
		    <th colspan="2">LISTA DE FACTURAS DE PROVEEDOR</th>
	    </tr>
	    <tr>
	        <td>CICLO:</td>
	        <td>
                <asp:DropDownList ID="ddlCiclos" runat="server" DataSourceID="sdsCiclos" 
                    DataTextField="CicloName" DataValueField="cicloId">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT DISTINCT FacturaDeProveedor.cicloId, Ciclos.CicloName FROM FacturaDeProveedor INNER JOIN Ciclos ON FacturaDeProveedor.cicloId = Ciclos.cicloID ORDER BY Ciclos.CicloName">
                </asp:SqlDataSource>
            </td>
	    </tr>
    </table>
        <asp:GridView ID="gvFacturas" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="facturaid" DataSourceID="sdsFacturas">
            <Columns>
                <asp:BoundField DataField="facturaid" HeaderText="ID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="facturaid" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" 
                    SortExpression="Nombre" />
                <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                    HeaderText="Fecha" SortExpression="fecha" />
                <asp:BoundField DataField="numFactura" HeaderText="Numero Factura" 
                    SortExpression="numFactura" />
                <asp:TemplateField HeaderText="Abrir">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1" runat="server" 
                            NavigateUrl='<%# GetFacturaURL(Eval("facturaid").ToString()) %>' Text="Abrir Factura"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="sdsFacturas" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
            SelectCommand="SELECT FacturaDeProveedor.facturaid, FacturaDeProveedor.cicloId, FacturaDeProveedor.proveedorID, Proveedores.Nombre, FacturaDeProveedor.numFactura, FacturaDeProveedor.fecha FROM FacturaDeProveedor INNER JOIN Proveedores ON FacturaDeProveedor.proveedorID = Proveedores.proveedorID WHERE (FacturaDeProveedor.cicloId = @cicloId) ORDER BY Proveedores.Nombre, FacturaDeProveedor.fecha DESC">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlCiclos" DefaultValue="-1" Name="cicloId" 
                    PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
