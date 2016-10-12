<%@ Page Language="C#" Theme="skinverde" Title = "Reporte Desglosado Liquidaciones por Productor" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmReporteConcentradoLiquidacionesPorProductor.aspx.cs" Inherits="Garibay.frmReporteConcentradoLiquidacionesPorProductor" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>
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
                    <td class="TableHeader" align="center" colspan="3">
                        REPORTE DESGLOSADO POR PRODUCTOR</td>
                </tr>
                <tr>
                    <td class="TableHeader">
                        FILTROS:</td>
                    <td class="TablaField">
                        Ciclo:</td>
                    <td>
                        <asp:DropDownList ID="drpdlCiclo" runat="server" AutoPostBack="True" 
                            DataSourceID="sdsCiclos" DataTextField="CicloName" DataValueField="cicloID" 
                            Height="23px" Width="164px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos]">
                        </asp:SqlDataSource>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvLiquidaciones" runat="server" AutoGenerateColumns="False" 
                DataSourceID="sdsLiquidaciones" ondatabound="gvLiquidaciones_DataBound" 
                ShowFooter="True" DataKeyNames="productorID" 
                onrowdatabound="gvLiquidaciones_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Productor" SortExpression="Productor">
                        <ItemTemplate>
                            
                            
                            <asp:HyperLink ID="HyperLink1" Text='<%# Bind("Productor") %>' runat="server">HyperLink</asp:HyperLink>
                        </ItemTemplate></asp:TemplateField><asp:BoundField DataField="PesoNeto" DataFormatString="{0:N2}" 
                        HeaderText="Peso Neto" ReadOnly="True" SortExpression="PesoNeto">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="totaldctoHumedad" DataFormatString="{0:N2}" 
                        HeaderText="Dcto. Humedad" ReadOnly="True" SortExpression="totaldctoHumedad">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="totaldctoImpurezas" DataFormatString="{0:N2}" 
                        HeaderText="Dcto. Impurezas" ReadOnly="True" 
                        SortExpression="totaldctoImpurezas">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="totaldctos" DataFormatString="{0:N2}" 
                        HeaderText="Total Dctos." ReadOnly="True" SortExpression="totaldctos">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="KgNetos" DataFormatString="{0:N2}" 
                        HeaderText="Kg Netos." ReadOnly="True" SortExpression="KgNetos">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Importe" DataFormatString="{0:C2}" 
                        HeaderText="Importe" ReadOnly="True" SortExpression="Importe">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ImporteSecado" DataFormatString="{0:C2}" 
                        HeaderText="Importe Secado" ReadOnly="True" SortExpression="ImporteSecado">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Importeneto" DataFormatString="{0:C2}" 
                        HeaderText="Importe Neto" ReadOnly="True" SortExpression="Importeneto">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalNotas" DataFormatString="{0:C2}" 
                        HeaderText="Notas" ReadOnly="True" SortExpression="TotalNotas">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalIntereses" DataFormatString="{0:C2}" 
                        HeaderText="Total Intereses" ReadOnly="True" SortExpression="TotalIntereses">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalSeguro" DataFormatString="{0:C2}" 
                        HeaderText="Total Seguro" ReadOnly="True" SortExpression="TotalSeguro">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalAPagar" DataFormatString="{0:C2}" 
                        HeaderText="Total a Pagar" ReadOnly="True" SortExpression="TotalAPagar">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="productorID" HeaderText="prodID" Visible="False" />
                </Columns>
            </asp:GridView>
        </td>
	</tr>
	<tr>
	    <td>
            <asp:SqlDataSource ID="sdsLiquidaciones" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                
                
                
                
                
                SelectCommand="SELECT    Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS Productor,
                          (SELECT     SUM(Boletas.pesonetoentrada) AS Expr1
                            FROM          Liquidaciones_Boletas INNER JOIN
                                                   Boletas ON Liquidaciones_Boletas.BoletaID = Boletas.boletaID
                            WHERE      (Liquidaciones_Boletas.LiquidacionID IN (Select LiquidacionID From Liquidaciones Where Liquidaciones.productorID = Productores.productorID AND Liquidaciones.cobrada = 1))) AS PesoNeto,

                         (SELECT     SUM(Boletas_8.dctoHumedad) AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_8 INNER JOIN
                                                   Boletas AS Boletas_8 ON Liquidaciones_Boletas_8.BoletaID = Boletas_8.boletaID
                          WHERE      (Liquidaciones_Boletas_8.LiquidacionID IN (Select LiquidacionID From Liquidaciones Where Liquidaciones.productorID = Productores.productorID AND Liquidaciones.cobrada = 1)))  AS totaldctoHumedad,
                          (SELECT     SUM(Boletas_7.dctoImpurezas) AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_7 INNER JOIN
                                                   Boletas AS Boletas_7 ON Liquidaciones_Boletas_7.BoletaID = Boletas_7.boletaID
                            WHERE      (Liquidaciones_Boletas_7.LiquidacionID IN (Select LiquidacionID From Liquidaciones Where Liquidaciones.productorID = Productores.productorID AND Liquidaciones.cobrada = 1)))  AS totaldctoImpurezas,
                          (SELECT     SUM(Boletas_6.dctoImpurezas + Boletas_6.dctoHumedad) AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_6 INNER JOIN
                                                   Boletas AS Boletas_6 ON Liquidaciones_Boletas_6.BoletaID = Boletas_6.boletaID
                           WHERE      (Liquidaciones_Boletas_6.LiquidacionID IN (Select LiquidacionID From Liquidaciones Where Liquidaciones.productorID = Productores.productorID AND Liquidaciones.cobrada = 1)))   AS totaldctos,
                          (SELECT     SUM(Boletas_5.pesonetoentrada - Boletas_5.dctoImpurezas - Boletas_5.dctoHumedad) AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_5 INNER JOIN
                                                   Boletas AS Boletas_5 ON Liquidaciones_Boletas_5.BoletaID = Boletas_5.boletaID
                            WHERE      (Liquidaciones_Boletas_5.LiquidacionID IN (Select LiquidacionID From Liquidaciones Where Liquidaciones.productorID = Productores.productorID AND Liquidaciones.cobrada = 1)))  AS KgNetos,
                          (SELECT     SUM((Boletas_4.pesonetoentrada - Boletas_4.dctoImpurezas - Boletas_4.dctoHumedad) * Boletas_4.precioapagar) AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_4 INNER JOIN
                                                   Boletas AS Boletas_4 ON Liquidaciones_Boletas_4.BoletaID = Boletas_4.boletaID
                            WHERE      (Liquidaciones_Boletas_4.LiquidacionID IN (Select LiquidacionID From Liquidaciones Where Liquidaciones.productorID = Productores.productorID AND Liquidaciones.cobrada = 1)))  AS Importe,
                          (SELECT     SUM(Boletas_3.dctoSecado) AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_3 INNER JOIN
                                                   Boletas AS Boletas_3 ON Liquidaciones_Boletas_3.BoletaID = Boletas_3.boletaID
                            WHERE      (Liquidaciones_Boletas_3.LiquidacionID IN (Select LiquidacionID From Liquidaciones Where Liquidaciones.productorID = Productores.productorID AND Liquidaciones.cobrada = 1)))   AS ImporteSecado,
                          (SELECT     SUM((Boletas_2.pesonetoentrada - Boletas_2.dctoImpurezas - Boletas_2.dctoHumedad) * Boletas_2.precioapagar) - SUM(Boletas_2.dctoSecado) 
                                                   AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_2 INNER JOIN
                                                   Boletas AS Boletas_2 ON Liquidaciones_Boletas_2.BoletaID = Boletas_2.boletaID
                             WHERE      (Liquidaciones_Boletas_2.LiquidacionID IN (Select LiquidacionID From Liquidaciones Where Liquidaciones.productorID = Productores.productorID AND Liquidaciones.cobrada = 1)))  AS Importeneto, 
                     sum(Liquidaciones.notas) as TotalNotas, SUM(Liquidaciones.intereses) AS TotalIntereses, SUM(Liquidaciones.seguro) AS TotalSeguro,
                          (SELECT     SUM((Boletas_1.pesonetoentrada - Boletas_1.dctoImpurezas - Boletas_1.dctoHumedad) * Boletas_1.precioapagar) - SUM(Boletas_1.dctoSecado) 
                                                   AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_1 INNER JOIN
                                                   Boletas AS Boletas_1 ON Liquidaciones_Boletas_1.BoletaID = Boletas_1.boletaID
                            WHERE      (Liquidaciones_Boletas_1.LiquidacionID IN (Select LiquidacionID From Liquidaciones Where Liquidaciones.productorID = Productores.productorID AND Liquidaciones.cobrada = 1))) - SUM(Liquidaciones.notas) - SUM(Liquidaciones.intereses) 
                      - SUM(Liquidaciones.seguro) AS TotalAPagar, Productores.productorID
FROM         Liquidaciones INNER JOIN
                      Productores ON Liquidaciones.productorID = Productores.productorID
WHERE     (Liquidaciones.cobrada = 1) AND(Liquidaciones.cicloID = @cicloID)
Group by Productores.productorID, Productores.apaterno, Productores.amaterno, Productores.nombre
 order by Productores.apaterno, Productores.amaterno, Productores.nombre">
                <SelectParameters>
                    <asp:ControlParameter ControlID="drpdlCiclo" DefaultValue="-1" Name="cicloID" 
                        PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
	</tr>
</table>
 </ContentTemplate>
 </asp:UpdatePanel> 
</asp:Content>
