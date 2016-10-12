<%@ Page Language="C#" Theme="skinverde" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmListLogMessage.aspx.cs" Inherits="Garibay.WebForm13" Title="Registros de Mensajes" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%">
    <tr>
        <td align="center" class="TableHeader">
       REGISTROS DE MENSAJES
        </td>
    </tr>
    <tr>
        <td>
       
            <asp:Panel ID="pnlBoletas" runat="server" GroupingText="Filtros">
            <table>
                <tr>
                    <td class="TablaField">
                    TIPO:
                    </td>
                    <td>
                        <asp:DropDownList ID="drpdlfiltroTipo" runat="server" Width="200px" 
                            DataSourceID="SqlDataSource3" DataTextField="logmsgtype" 
                            DataValueField="logmsgTypeID" ondatabound="drpdlfiltroTipo_DataBound">
                        </asp:DropDownList>
                    </td>
                    <td class="TablaField">
                    USUARIO :
                    </td>
                    <td>
                        <asp:DropDownList ID="drpdlusuario" runat="server" Width="300px" 
                            DataSourceID="SqlDataSource4" DataTextField="Nombre" 
                            DataValueField="userID" ondatabound="drpdlusuario_DataBound">
                        </asp:DropDownList>
                    </td>
                    
                </tr>
                <tr>
                    <td colspan="4">
                    <table>
                        <tr>
                        <td class="TablaField">
                    ACCIÓN :
                    </td>
                    <td>
                        <asp:DropDownList ID="drpdlAccion" runat="server" Width="200px" 
                            DataTextField="fancyName" 
                            DataValueField="useractionID" DataSourceID="SqlDataSource5" 
                            ondatabound="drpdlAccion_DataBound">
                        </asp:DropDownList>
                    </td>
                            <td class="TablaField">
                                DE:
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechainicio" runat="server" Width="150px" ReadOnly="True"></asp:TextBox>
                               <rjs:PopCalendar ID="PopCalendar1" runat="server" style="width: 14px" 
                                    onselectionchanged="PopCalendar1_SelectionChanged" Separator="/" 
                                    Control="txtFechainicio" />
                            </td>
                            <td class="TablaField">
                                
                                A:
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaFin" runat="server" Width="150px" ReadOnly="True"></asp:TextBox>
                               <rjs:PopCalendar ID="PopCalendar2" runat="server" style="width: 14px" 
                                    onselectionchanged="PopCalendar2_SelectionChanged" Separator="/" 
                                    Control="txtFechaFin" />
                            </td>
                        </tr>
                    </table>
                    </td>
                </tr>
                <tr>
                    <td class="TablaField">
                    ELEMENTOS POR PÁGINA:
                    </td>
                    
                    <td>
                    <asp:DropDownList ID="ddlElemXPage" runat="server">
                    <asp:ListItem >10</asp:ListItem>
                    <asp:ListItem Selected="True">100</asp:ListItem>
                    <asp:ListItem>200</asp:ListItem>
                    <asp:ListItem>500</asp:ListItem>
                    <asp:ListItem>1000</asp:ListItem>
                </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </asp:Panel>
       </td>
    </tr>
    <tr>
        <td align="center">
            
            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar Resultados" 
                onclick="btnFiltrar_Click" />
            
            <asp:Button ID="btnLimpiarFiltros" runat="server" onclick="btnLimpiarFiltros_Click" 
                Text="Limpiar Filtros" />
            
        </td>
    </tr>
    <tr>
        <td>
                             
            &nbsp;</td>
    </tr>
</table>
            <asp:GridView ID="grdvLogMessages" runat="server" 
        AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource2" AllowPaging="True" AllowSorting="True" 
        DataKeyNames="logmsgnum,userid,logmsgTypeID,useractionID">
                <Columns>
                    <asp:BoundField HeaderText="Fecha" DataField="datestamp" 
                        SortExpression="datestamp" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                    <asp:BoundField HeaderText="Tipo" DataField="logmsgtype" 
                        SortExpression="logmsgtype" />
                    <asp:BoundField HeaderText="Usuario" DataField="Nombre" 
                        SortExpression="Nombre" />
                    <asp:BoundField HeaderText="Acción" DataField="fancyName" 
                        SortExpression="fancyName" />
                    <asp:BoundField HeaderText="Url" DataField="urlpage" 
                        SortExpression="urlpage" />
                    <asp:BoundField HeaderText="Mensaje" DataField="message" 
                        SortExpression="message" />
                    <asp:BoundField DataField="userid" HeaderText="userid" ReadOnly="True" 
                        SortExpression="userid" Visible="False" />
                    <asp:BoundField DataField="logmsgTypeID" HeaderText="logmsgTypeID" 
                        ReadOnly="True" SortExpression="logmsgTypeID" Visible="False" />
                    <asp:BoundField DataField="useractionID" HeaderText="useractionID" 
                        ReadOnly="True" SortExpression="useractionID" Visible="False" />
                </Columns>
            </asp:GridView>

    <br />

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        
        
        
        
        SelectCommand="SELECT LogMessages.logmsgnum, LogMsgTypes.logmsgtype, Users.Nombre, UsersActions.fancyName, LogMessages.urlpage, LogMessages.datestamp, LogMessages.message, LogMessages.userid, LogMessages.logmsgTypeID, LogMessages.useractionID FROM LogMessages INNER JOIN UsersActions ON LogMessages.useractionID = UsersActions.useractionID INNER JOIN LogMsgTypes ON LogMessages.logmsgTypeID = LogMsgTypes.logmsgTypeID INNER JOIN Users ON LogMessages.userid = Users.userID ORDER BY LogMessages.datestamp DESC"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        SelectCommand="SELECT [logmsgTypeID], [logmsgtype] FROM [LogMsgTypes]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        SelectCommand="SELECT [userID], [Nombre] FROM [Users]"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        SelectCommand="SELECT [useractionID], [fancyName] FROM [UsersActions]">
    </asp:SqlDataSource>

</asp:Content>
