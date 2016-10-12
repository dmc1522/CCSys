<%@ Page Language="C#" Theme="skinverde" Title="Agregar Boletas a liquidacion" AutoEventWireup="true" CodeBehind="frmBoletasdeFacturaCV.aspx.cs" Inherits="Garibay.frmBoletasdeFacturaCV" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td colspan="2" class="TableHeader" align="center">Agregar Boletas a liquidacion</td>
            </tr>
            <tr>
                <td valign="top" width="50%">
                    <table width="100%">
                    	<tr>
                    		<td class="TablaField" colspan="2">Boletas Existentes</td>
                    	</tr>
                    	<tr>
                    		<td class="TablaField">Productores:</td>
                    		<td class="TablaField">
                                <asp:DropDownList ID="DropDownList1" runat="server" 
                                    DataSourceID="sdsProductoresBolAdded" DataTextField="Productor" 
                                    DataValueField="productorID">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsProductoresBolAdded" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" SelectCommand="SELECT DISTINCT Productores.productorID, LTRIM(Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre) AS Productor, Boletas.pagada FROM Productores INNER JOIN Boletas ON Productores.productorID = Boletas.productorID WHERE (Boletas.pesonetosalida &gt; 0) AND (Boletas.pagada = 0) AND (BoletaID not in (SELECT     boletaID
FROM         FacturasCV_Boletas)) ORDER BY Productor"></asp:SqlDataSource>
                            </td>
                    	</tr>
                    	<tr>
                    		<td colspan="2">
                                <asp:GridView ID="GridView1" runat="server" DataSourceID="sdsBoletasAdded">
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdsBoletasAdded" runat="server"></asp:SqlDataSource>
                            </td>
                    	</tr>
                    </table>
                </td>
                <td valign="top" width="50%">
                    <table>
                    	<tr>
                    		<td class="TablaField">Nueva Boleta</td>
                    	</tr>
                    	<tr>
                    		<td></td>
                    	</tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
