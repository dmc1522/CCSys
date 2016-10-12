<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" Title="Report global de créditos" AutoEventWireup="true" CodeBehind="frmReporteGeneralCreditos.aspx.cs" Inherits="Garibay.frmReporteGeneralCreditos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

         
        <table>
            <tr>
                <td>
                    REPORTE GLOBAL DE CRÉDITOS</td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                Fecha:<asp:TextBox ID="TextBoxFecha" runat="server"></asp:TextBox>
                    <rjs:PopCalendar ID="PopCalendar3" runat="server" Separator="/" 
                        Control="TextBoxFecha" AutoPostBack="True" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Ciclo:
                    <asp:DropDownList ID="ddlCiclo" runat="server" 
                        DataSourceID="dataSourceCiclo" DataTextField="CicloName" 
                        DataValueField="cicloID" Height="22px" AutoPostBack="True" 
                                    onselectedindexchanged="ddlCiclo_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="dataSourceCiclo" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
                                    SelectCommand="SELECT cicloID, CicloName FROM Ciclos WHERE cerrado=@cerrado ORDER BY CicloName DESC">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="FALSE" Name="cerrado" Type="Boolean" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridViewReporteGlobal" runat="server" 
                                    AutoGenerateColumns="False" DataSourceID="sdsReporteGlobal" 
                                    ondatabound="GridViewReporteGlobal_DataBound" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField DataField="Nombre" HeaderText="Productor" 
                                            SortExpression="Nombre">
                                        <FooterStyle HorizontalAlign="Left" />
                                        <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Poblacion" HeaderText="Poblacion" 
                                            SortExpression="Poblacion" >
                                        <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Telefono" HeaderText="Telefono" 
                                            SortExpression="Telefono" />
                                        <asp:BoundField DataField="LimitedeCredito" DataFormatString="{0:C2}" 
                                            HeaderText="Limite de Credito" SortExpression="LimitedeCredito">
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotalNotasConInteres" DataFormatString="{0:C2}" 
                                            HeaderText="Notas sujetas a intereses" SortExpression="TotalNotasConInteres">
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotalNotasSinInteres" DataFormatString="{0:C2}" 
                                            HeaderText="Notas no sujetas a intereses" SortExpression="TotalNotasSinInteres">
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotalPrestamos" DataFormatString="{0:C2}" 
                                            HeaderText="Préstamos" SortExpression="TotalPrestamos">
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotalSeguro" DataFormatString="{0:C2}" 
                                            HeaderText="Seguro" SortExpression="TotalSeguro">
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotalIntereses" DataFormatString="{0:C2}" 
                                            HeaderText="Intereses" SortExpression="TotalIntereses">
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Adeudos" DataFormatString="{0:C2}" 
                                            HeaderText="Total Adeudos" SortExpression="Adeudos">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotalAbonado" DataFormatString="{0:C2}" 
                                            HeaderText="Abonado" SortExpression="TotalAbonado">
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotalDescuentos" DataFormatString="{0:c2}" 
                                            HeaderText="Descuentos de intereses" SortExpression="TotalDescuentos">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotalDebe" DataFormatString="{0:C2}" 
                                            HeaderText="Total" SortExpression="TotalDebe">
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdsReporteGlobal" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                    SelectCommand="ReturnReporteGlobalCreditos" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="TextBoxFecha" Name="fecha" PropertyName="Text" 
                                            Type="DateTime" />
                                        <asp:ControlParameter ControlID="ddlCiclo" DefaultValue="-1" Name="cicloID" 
                                            PropertyName="SelectedValue" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        </table>
                </td>
            </tr>
    </table>


</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

</asp:Content>
