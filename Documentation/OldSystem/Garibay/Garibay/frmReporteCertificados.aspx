<%@ Page Language="C#" Theme="skinverde" Title="Reporte de Certificados" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmReporteCertificados.aspx.cs" Inherits="Garibay.frmReporteCertificados" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>
    <style type="text/css">
        .style3
        {
            height: 35px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upPanel" runat="Server">
    <ContentTemplate>
    <asp:UpdateProgress id= "upprog" runat="Server" AssociatedUpdatePanelID="upPanel" 
            DisplayAfter="0">
     <ProgressTemplate>
         <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/cargando.gif" />
         Cargando datos...
     </ProgressTemplate>
    
    </asp:UpdateProgress>
    
   
    <table >
	<tr>
		<td>
            <table>
                <tr>
                    <td class="TableHeader" align="center">
                        REPORTE DE LOS CERTIFICADOS AGRUPADO POR EMPRESA Y POR PRODUCTO</td>
                </tr>
            </table>
            <asp:GridView ID="gvCertificados" runat="server" AutoGenerateColumns="False" 
                DataSourceID="sdsReporte">
                <Columns>
                    <asp:BoundField DataField="Empresa" 
                        HeaderText="Empresa" SortExpression="Empresa">
                    </asp:BoundField>
                    <asp:BoundField DataField="Nombre" 
                        HeaderText="Producto" SortExpression="Nombre">
                    </asp:BoundField>
                    <asp:BoundField DataField="Precio" DataFormatString="{0:c2}" 
                        HeaderText="Precio" 
                        SortExpression="Precio">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalKilos" DataFormatString="{0:N2}" 
                        HeaderText="Total Kilos" ReadOnly="True" SortExpression="TotalKilos">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </td>
	</tr>
	<tr>
	    <td class="style3">
            <asp:SqlDataSource ID="sdsReporte" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                
                
                
                
                
                
                SelectCommand="SELECT Productos.Nombre, CredFinCertificados.Precio, SUM(CredFinCertificados.KG) AS TotalKilos, Empresas.Empresa FROM CredFinCertificados INNER JOIN Productos ON CredFinCertificados.productoID = Productos.productoID INNER JOIN Empresas ON CredFinCertificados.empresaCertificadaID = Empresas.empresaID GROUP BY Productos.Nombre, CredFinCertificados.Precio, Empresas.Empresa ORDER BY Empresas.Empresa, Productos.Nombre, CredFinCertificados.Precio">
            </asp:SqlDataSource>
        </td>
	</tr>
</table>
<table>
	<tr>
		<td class="TableHeader">REPORTE POR BODEGA Y CABEZAS</td>
	</tr>
	<tr>
	    <td>
            <asp:GridView ID="gvCertificados0" runat="server" AutoGenerateColumns="False" 
                DataSourceID="sdsReporte2">
                <Columns>
                    <asp:BoundField DataField="Empresa" HeaderText="Empresa" 
                        SortExpression="Empresa" />
                    <asp:BoundField DataField="bodega" HeaderText="Bodega" 
                        SortExpression="bodega" />
                    <asp:BoundField DataField="Nombre" HeaderText="Producto" 
                        SortExpression="Nombre" />
                    <asp:BoundField DataField="TotalKilos" DataFormatString="{0:N2}" 
                        HeaderText="Total Kilos" ReadOnly="True" SortExpression="TotalKilos">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cabezas" DataFormatString="{0:N2}" 
                        HeaderText="# Cabezas" ReadOnly="True" SortExpression="Cabezas">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Importe" DataFormatString="{0:C2}" 
                        HeaderText="Importe" ReadOnly="True" SortExpression="Importe">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsReporte2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                SelectCommand="SELECT Empresas.Empresa, Bodegas.bodega, Productos.Nombre, SUM(CredFinCertificados.KG) AS TotalKilos, SUM(CredFinCertificados.numCabezas) AS Cabezas, SUM(CredFinCertificados.Precio * CredFinCertificados.KG) AS Importe FROM CredFinCertificados INNER JOIN Productos ON CredFinCertificados.productoID = Productos.productoID INNER JOIN Empresas ON CredFinCertificados.empresaCertificadaID = Empresas.empresaID INNER JOIN Bodegas ON CredFinCertificados.bodegaID = Bodegas.bodegaID GROUP BY Productos.Nombre, Empresas.Empresa, Bodegas.bodega ORDER BY Empresas.Empresa, Bodegas.bodega, Productos.Nombre">
            </asp:SqlDataSource>
        </td>
	</tr>
</table>
 </ContentTemplate>
 </asp:UpdatePanel> 
</asp:Content>
