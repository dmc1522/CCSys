<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" Title = "Lista de situaciones" AutoEventWireup="true" CodeBehind="frmSituacionesLista.aspx.cs" Inherits="Garibay.frmSituacionesLista" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

         
        <table>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvSituaciones" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="sdsListSituaciones" DataKeyNames="situacionID" 
                        onrowdatabound="gvSituaciones_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="situacionID" HeaderText="situacionID" 
                                InsertVisible="False" ReadOnly="True" SortExpression="situacionID" 
                                Visible="False" />
                            <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                                HeaderText="Fecha" SortExpression="fecha" />
                            <asp:BoundField DataField="titulo" HeaderText="Título" 
                                SortExpression="titulo" />
                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" 
                                SortExpression="descripcion" />
                            <asp:CheckBoxField DataField="activa" HeaderText="Activa" 
                                SortExpression="activa" />
                            <asp:BoundField DataField="Nombre" HeaderText="Usuario" 
                                SortExpression="Nombre" />
                            <asp:TemplateField HeaderText="Abrir">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HLOpen" runat="server">Abrir</asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsListSituaciones" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
                        SelectCommand="SELECT Situacion.fecha, Situacion.titulo, Situacion.descripcion, Situacion.activa, Users.Nombre, Situacion.situacionID FROM Situacion INNER JOIN Users ON Situacion.userID = Users.userID order by fecha desc">
                    </asp:SqlDataSource>
                </td>
            </tr>
    </table>


</asp:Content>
