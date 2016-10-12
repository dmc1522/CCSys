<%@ Page Language="C#" Theme="skinverde" Title = "Reporte Desglosado Liquidaciones" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmReporteConcentradoLiquidaciones.aspx.cs" Inherits="Garibay.frmReporteConcentradoLiquidaciones" %>

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
                        REPORTE DESGLOSADO POR LIQUIDACIÓN</td>
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
        </td>
	</tr>
	<tr>
	    <td>
            <asp:GridView ID="gvLiquidaciones" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="LiquidacionID" DataSourceID="sdsLiquidaciones" 
                ondatabound="gvLiquidaciones_DataBound" ShowFooter="True">
                <Columns>
                    <asp:BoundField DataField="Productor" HeaderText="Productor" ReadOnly="True" 
                        SortExpression="Productor" />
                    <asp:BoundField DataField="LiquidacionID" HeaderText="# Liquidación" 
                        SortExpression="LiquidacionID" InsertVisible="False" ReadOnly="True" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PesoNeto" HeaderText="Peso Neto" 
                        SortExpression="PesoNeto" DataFormatString="{0:N2}" ReadOnly="True" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="totaldctoHumedad" HeaderText="Dcto. Humedad" SortExpression="totaldctoHumedad" 
                        DataFormatString="{0:N2}" ReadOnly="True" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="totaldctoImpurezas" HeaderText="Dcto. Impurezas" 
                        SortExpression="totaldctoImpurezas" DataFormatString="{0:N2}" 
                        ReadOnly="True" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="totaldctos" HeaderText="Total. Dctos" 
                        SortExpression="totaldctos" DataFormatString="{0:N2}" ReadOnly="True" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="KgNetos" DataFormatString="{0:N2}" 
                        HeaderText="Kg Netos" SortExpression="KgNetos" ReadOnly="True" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Importe" 
                        HeaderText="Importe" ReadOnly="True" SortExpression="Importe" DataFormatString="{0:C2}" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ImporteSecado" HeaderText="Dcto Secado" 
                        ReadOnly="True" SortExpression="ImporteSecado" DataFormatString="{0:C2}" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ImporteNeto" HeaderText="Importe Neto" 
                        ReadOnly="True" SortExpression="ImporteNeto" DataFormatString="{0:C2}" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalNotas" HeaderText="Notas" ReadOnly="True" 
                        SortExpression="TotalNotas" DataFormatString="{0:C2}" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalIntereses" HeaderText="Intereses" 
                        ReadOnly="True" SortExpression="TotalIntereses" DataFormatString="{0:C2}" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalSeguro" HeaderText="Seguro" ReadOnly="True" 
                        SortExpression="TotalSeguro" DataFormatString="{0:C2}" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalAPagar" HeaderText="Total a Pagar" 
                        ReadOnly="True" SortExpression="TotalAPagar" DataFormatString="{0:C2}" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsLiquidaciones" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                
                
                
                
                
                SelectCommand="SELECT     Liquidaciones.LiquidacionID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS Productor,
                          (SELECT     SUM(Boletas.pesonetoentrada) AS Expr1
                            FROM          Liquidaciones_Boletas INNER JOIN
                                                   Boletas ON Liquidaciones_Boletas.BoletaID = Boletas.boletaID
                            WHERE      (Liquidaciones_Boletas.LiquidacionID = Liquidaciones.LiquidacionID)) AS PesoNeto,
                          (SELECT     SUM(Boletas_8.dctoHumedad) AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_8 INNER JOIN
                                                   Boletas AS Boletas_8 ON Liquidaciones_Boletas_8.BoletaID = Boletas_8.boletaID
                            WHERE      (Liquidaciones_Boletas_8.LiquidacionID = Liquidaciones.LiquidacionID)) AS totaldctoHumedad,
                          (SELECT     SUM(Boletas_7.dctoImpurezas) AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_7 INNER JOIN
                                                   Boletas AS Boletas_7 ON Liquidaciones_Boletas_7.BoletaID = Boletas_7.boletaID
                            WHERE      (Liquidaciones_Boletas_7.LiquidacionID = Liquidaciones.LiquidacionID)) AS totaldctoImpurezas,
                          (SELECT     SUM(Boletas_6.dctoImpurezas + Boletas_6.dctoHumedad) AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_6 INNER JOIN
                                                   Boletas AS Boletas_6 ON Liquidaciones_Boletas_6.BoletaID = Boletas_6.boletaID
                            WHERE      (Liquidaciones_Boletas_6.LiquidacionID = Liquidaciones.LiquidacionID)) AS totaldctos,
                          (SELECT     SUM(Boletas_5.pesonetoentrada - Boletas_5.dctoImpurezas - Boletas_5.dctoHumedad) AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_5 INNER JOIN
                                                   Boletas AS Boletas_5 ON Liquidaciones_Boletas_5.BoletaID = Boletas_5.boletaID
                            WHERE      (Liquidaciones_Boletas_5.LiquidacionID = Liquidaciones.LiquidacionID)) AS KgNetos,
                          (SELECT     SUM((Boletas_4.pesonetoentrada - Boletas_4.dctoImpurezas - Boletas_4.dctoHumedad) * Boletas_4.precioapagar) AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_4 INNER JOIN
                                                   Boletas AS Boletas_4 ON Liquidaciones_Boletas_4.BoletaID = Boletas_4.boletaID
                            WHERE      (Liquidaciones_Boletas_4.LiquidacionID = Liquidaciones.LiquidacionID)) AS Importe,
                          (SELECT     SUM(Boletas_3.dctoSecado) AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_3 INNER JOIN
                                                   Boletas AS Boletas_3 ON Liquidaciones_Boletas_3.BoletaID = Boletas_3.boletaID
                            WHERE      (Liquidaciones_Boletas_3.LiquidacionID = Liquidaciones.LiquidacionID)) AS ImporteSecado,
                          (SELECT     SUM((Boletas_2.pesonetoentrada - Boletas_2.dctoImpurezas - Boletas_2.dctoHumedad) * Boletas_2.precioapagar) - SUM(Boletas_2.dctoSecado) 
                                                   AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_2 INNER JOIN
                                                   Boletas AS Boletas_2 ON Liquidaciones_Boletas_2.BoletaID = Boletas_2.boletaID
                            WHERE      (Liquidaciones_Boletas_2.LiquidacionID = Liquidaciones.LiquidacionID)) AS ImporteNeto, SUM(Liquidaciones.notas) AS TotalNotas, 
                      SUM(Liquidaciones.intereses) AS TotalIntereses, SUM(Liquidaciones.seguro) AS TotalSeguro,
                          (SELECT     SUM((Boletas_1.pesonetoentrada - Boletas_1.dctoImpurezas - Boletas_1.dctoHumedad) * Boletas_1.precioapagar) - SUM(Boletas_1.dctoSecado) 
                                                   AS Expr1
                            FROM          Liquidaciones_Boletas AS Liquidaciones_Boletas_1 INNER JOIN
                                                   Boletas AS Boletas_1 ON Liquidaciones_Boletas_1.BoletaID = Boletas_1.boletaID
                            WHERE      (Liquidaciones_Boletas_1.LiquidacionID = Liquidaciones.LiquidacionID)) - SUM(Liquidaciones.notas) - SUM(Liquidaciones.intereses) 
                      - SUM(Liquidaciones.seguro) AS TotalAPagar
FROM         Liquidaciones INNER JOIN
                      Productores ON Liquidaciones.productorID = Productores.productorID
WHERE     (Liquidaciones.cobrada = 1) AND(Liquidaciones.cicloID = @cicloID) 
GROUP BY Liquidaciones.LiquidacionID, Productores.apaterno, Productores.amaterno, Productores.nombre order by Productores.apaterno, Productores.amaterno, Productores.nombre">
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
