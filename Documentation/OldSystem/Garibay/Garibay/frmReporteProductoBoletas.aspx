<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" Title = "Concentrado por producto de boletas" AutoEventWireup="true" CodeBehind="frmReporteProductoBoletas.aspx.cs" Inherits="Garibay.frmReporteProductoBoletas" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

         
        <table >
            <tr>
                <td class="TableHeader" colspan="2">
                    REPORTE CONCENTRADO DE BOLETAS POR PRODUCTO</td>
            </tr>
            <tr>
                <td class="TablaField">
                    Ciclo:</td>
                <td align="left">
                    <asp:DropDownList ID="drpdlCiclo" runat="server" DataSourceID="sdsCiclos" 
                        DataTextField="CicloName" DataValueField="cicloID" Height="23px" 
                        Width="206px" AutoPostBack="True" 
                        onselectedindexchanged="drpdlCiclo_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [fechaInicio] DESC">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gridBoletasProductos" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="productoID" 
                        DataSourceID="sdsProductosBoletas">
                        <Columns>
                            <asp:BoundField DataField="productoID" HeaderText="productoID" 
                                InsertVisible="False" ReadOnly="True" SortExpression="productoID" 
                                Visible="False" />
                            <asp:BoundField DataField="Nombre" HeaderText="Producto" 
                                SortExpression="Nombre" />
                            <asp:BoundField DataField="TotalBoletas" HeaderText="# de boletas" 
                                ReadOnly="True" SortExpression="TotalBoletas" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="KG" HeaderText="KG" ReadOnly="True" 
                                SortExpression="KG" DataFormatString="{0:n}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PromedioHumedad" HeaderText="Promedio de Humedad" 
                                ReadOnly="True" SortExpression="PromedioHumedad" 
                                DataFormatString="{0:n}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalDctoHumedad" 
                                HeaderText="Total Dcto. de Humedad" ReadOnly="True" 
                                SortExpression="TotalDctoHumedad" DataFormatString="{0:n}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PromedioImpurezas" 
                                HeaderText="Promedio de Impurezas" ReadOnly="True" 
                                SortExpression="PromedioImpurezas" DataFormatString="{0:n}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalDctoImpurezas" 
                                HeaderText="Total Dcto. de Impurezas" ReadOnly="True" 
                                SortExpression="TotalDctoImpurezas" DataFormatString="{0:n}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Preciopromedio" HeaderText="Precio promedio" 
                                ReadOnly="True" SortExpression="Preciopromedio" 
                                DataFormatString="{0:C2}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="KGNETOS" HeaderText="KG NETOS" ReadOnly="True" 
                                SortExpression="KGNETOS" DataFormatString="{0:n}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsProductosBoletas" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        SelectCommand="SELECT Productos.productoID, Productos.Nombre, COUNT(Boletas.boletaID) AS TotalBoletas, SUM(Boletas.pesonetoentrada) AS KG, AVG(Boletas.humedad) AS PromedioHumedad, SUM(Boletas.dctoHumedad) AS TotalDctoHumedad, AVG(Boletas.impurezas) AS PromedioImpurezas, SUM(Boletas.dctoImpurezas) AS TotalDctoImpurezas, AVG(Boletas.precioapagar) AS Preciopromedio, SUM(Boletas.pesonetoentrada) - SUM(Boletas.dctoImpurezas) - SUM(Boletas.dctoHumedad) AS KGNETOS FROM Productos INNER JOIN Boletas ON Productos.productoID = Boletas.productoID WHERE (Boletas.cicloID = @cicloID) AND (Boletas.PesoDeEntrada &gt; 0 OR Boletas.PesoDeSalida&gt;0) GROUP BY Productos.productoID, Productos.Nombre ORDER BY Productos.Nombre">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="drpdlCiclo" DefaultValue="-1" Name="cicloID" 
                                PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            </table>


</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    </asp:Content>
