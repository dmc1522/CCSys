<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" Title="Reporte General de Liquidaciones" AutoEventWireup="true" CodeBehind="frmReporteGeneralLiquidaciones.aspx.cs" Inherits="Garibay.frmReporteGeneralLiquidaciones" %>

<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

         
        <table >
            <tr>
                <td class="TablaField">
                    CICLO:<asp:DropDownList ID="drpdlCiclo" runat="server" Height="23px" 
                        Width="214px" AutoPostBack="True" DataSourceID="sdsCiclo" 
                        DataTextField="CicloName" DataValueField="cicloID" 
                        onselectedindexchanged="drpdlCiclo_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsCiclo" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [CicloName] DESC">
                    </asp:SqlDataSource>
                </td>
            </tr>
            </table>


        <table>
            <tr>
                <td>
                    <asp:GridView ID="gvReporte" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="sdsReporte" onrowdatabound="gvReporte_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="totalLiquidaciones" HeaderText="Cantidad de Liq." 
                                ReadOnly="True" SortExpression="totalLiquidaciones" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalBoletas" DataFormatString="{0:n}" 
                                HeaderText="# Boletas" ReadOnly="True" SortExpression="TotalBoletas" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="totalNotas" DataFormatString="{0:c}" 
                                HeaderText="Notas" ReadOnly="True" SortExpression="Notas" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="totalSeguro" DataFormatString="{0:c}" 
                                HeaderText="Seguro" ReadOnly="True" SortExpression="totalSeguro" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="totalIntereses" DataFormatString="{0:c}" 
                                HeaderText="Intereses" ReadOnly="True" SortExpression="totalIntereses" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="totalAnticipos" DataFormatString="{0:c}" 
                                HeaderText="Anticipos" ReadOnly="True" 
                                SortExpression="totalAnticipos" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataFormatString="{0:C2}" 
                                HeaderText="Total Pagado de Bancos" SortExpression="TotalBancos" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataFormatString="{0:C2}" 
                                HeaderText="Total Pagado de Caja" SortExpression="TotalCaja" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsReporte" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
                        SelectCommand="SELECT     COUNT(LiquidacionID) AS totalLiquidaciones, SUM(notas) AS totalNotas, SUM(seguro) AS totalSeguro, SUM(intereses) AS totalIntereses, SUM(subTotal) 
                      AS TotalBoletas, SUM(anticipos) AS totalAnticipos, SUM(notas) + SUM(seguro) + SUM(intereses) + SUM(subTotal) - SUM(anticipos) AS TotalAPagar
FROM         Liquidaciones
WHERE (Liquidaciones.cicloID = @cicloID)  AND (Liquidaciones.cobrada = 1)
">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="drpdlCiclo" Name="cicloID" 
                                PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <br />
                    <br />
                    DESGLOSE POR PRODUCTO<asp:GridView ID="gvTotalPorProducto" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="sdsTotalPagos">
                        <Columns>
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" 
                                SortExpression="Nombre" />
                            <asp:BoundField 
                                HeaderText="Total Pagado" ReadOnly="True" 
                                SortExpression="totalPagado" DataField="totalPagado" 
                                DataFormatString="{0:C2}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsTotalPagos" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
                        
                        
                        SelectCommand="SELECT Productos.Nombre, SUM(Boletas.totalapagar) AS totalPagado FROM Liquidaciones INNER JOIN Liquidaciones_Boletas ON Liquidaciones.LiquidacionID = Liquidaciones_Boletas.LiquidacionID INNER JOIN Boletas ON Liquidaciones_Boletas.BoletaID = Boletas.boletaID INNER JOIN Productos ON Boletas.productoID = Productos.productoID
WHERE (Liquidaciones.cicloID = @cicloID) AND (Liquidaciones.cobrada = 1) 
 GROUP BY Productos.productoID, Productos.Nombre ORDER BY Productos.Nombre">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="drpdlCiclo" Name="cicloID" 
                                PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>


</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

   
    </asp:Content>
