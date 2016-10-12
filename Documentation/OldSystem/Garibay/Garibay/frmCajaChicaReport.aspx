<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" Title="Reporte de movimientos de caja chica" Theme="skinverde" AutoEventWireup="true" CodeBehind="frmCajaChicaReport.aspx.cs" Inherits="Garibay.frmCajaChicaReport" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <asp:Panel ID="panelMensaje" runat="server" > 
        <table>
            <tr>
                <td style="text-align: center">
                           
                           <asp:Image ID="imagenbien" runat="server" ImageUrl="~/imagenes/palomita.jpg" 
                               Visible="False" />
                           <asp:Image ID="imagenmal" runat="server" ImageUrl="~/imagenes/tache.jpg" 
                               Visible="False" />
                           <asp:Label ID="lblMensajetitle" runat="server" SkinID="lblMensajeTitle" 
                               Text="PRUEBA"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                           <asp:Label ID="lblMensajeOperationresult" runat="server"  Text="Label" 
                               SkinID="lblMensajeOperationresult"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblMensajeException" runat="server" SkinID="lblMensajeException" 
                        Text="SI NO HAY EXC BORREN EL TEXTO"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnAceptarMensaje" runat="server" CssClass="Button" 
                        Text="Aceptar" />
                </td>
            </tr>
        </table>
</asp:Panel>
<asp:Panel ID="panelagregar" runat="server" > 

         
        <table>
            <tr>
                <td class="TableHeader">
                    CONCENTRADO DE MOVIMIENTOS DE CAJA CHICA</td>
            </tr>
            <tr>
                <td>
                <table >
                	<tr>
                		<td colspan="3" class="TableHeader">Filtros:</td>
                	</tr>
                	<tr>
                	<td class="TablaField">Periodo:</td> <td>
                        Fecha inicio:
                        <asp:TextBox ID="txtFecha1" runat="server" ReadOnly="True"></asp:TextBox>
&nbsp;<rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtFecha1" Separator="/" />
                    </td>
                        <td>
                            Fecha fin:&nbsp;
                            <asp:TextBox ID="txtFecha2" runat="server" ReadOnly="True"></asp:TextBox>
                            <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtFecha2" 
                                Separator="/" />
                    </td>
                	</tr>
                	<tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtFecha1formatted" runat="server" ReadOnly="True" 
                                Visible="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFecha2formatted" runat="server" ReadOnly="True" 
                                Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                	<tr>
                	<td colspan="3">
                        <asp:Button ID="btnFiltrar" runat="server" onclick="Button1_Click" 
                            Text="Filtrar" />

        
        

                        <asp:Button ID="btnImprimir" runat="server" 
                            Text="Exportar a Excel" onclick="btnImprimir_Click" />

        
        

                        </td> 
                	</tr>
                </table>
                <table>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gridConcentrado" runat="server" AllowPaging="True" 
                                AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                                DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="None" 
                                
                                PageSize="100">
                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                <HeaderStyle CssClass="TableHeader" />
                                <AlternatingRowStyle BackColor="White" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                <Columns>
                                    <asp:BoundField DataField="catalogoMovBanco" HeaderText="Catálogo de cuenta" 
                                        ItemStyle-HorizontalAlign="Right" SortExpression="catalogoMovBanco">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="sumacargos" DataFormatString="{0:c}" 
                                        HeaderText="Cargos" ItemStyle-HorizontalAlign="Center" ReadOnly="True" 
                                        SortExpression="sumacargos">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="sumaabonos" DataFormatString="{0:c}" 
                                        HeaderText="Abonos" ReadOnly="True" SortExpression="sumaabonos" />
                                    <asp:BoundField DataField="total" DataFormatString="{0:c}" HeaderText="Total" 
                                        ReadOnly="True" SortExpression="total" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        ProviderName="<%$ ConnectionStrings:GaribayConnectionString.ProviderName %>" 
                        
                        
                        SelectCommand="SELECT catalogoMovimientosBancos.catalogoMovBanco, SUM(MovimientosCaja.cargo) AS sumacargos, SUM(MovimientosCaja.abono) AS sumaabonos, SUM(MovimientosCaja.abono) - SUM(MovimientosCaja.cargo) AS total FROM MovimientosCaja INNER JOIN catalogoMovimientosBancos ON MovimientosCaja.catalogoMovBancoID = catalogoMovimientosBancos.catalogoMovBancoID WHERE (MovimientosCaja.fecha &gt;= @fecha1) AND (MovimientosCaja.fecha &lt;= @fecha2) GROUP BY catalogoMovimientosBancos.catalogoMovBanco">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtFecha1formatted" 
                                DefaultValue="1900/01/01 00:00:00" Name="fecha1" PropertyName="Text" />
                            <asp:ControlParameter ControlID="txtFecha2formatted" 
                                DefaultValue="2100/01/01 00:00:00" Name="fecha2" PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
     
                	</tr>
                </table>
                
</asp:Panel>

</asp:Content>
