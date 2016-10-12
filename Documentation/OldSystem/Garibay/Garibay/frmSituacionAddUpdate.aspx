<%@ Page Title="Agregar/Actualizar situacion" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmSituacionAddUpdate.aspx.cs" Inherits="Garibay.frmSituacionAddUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
EN ESTA PAGINA PUEDE REPORTAR UN INCIDENTE, ALGUN PROBLEMA CON ALGUNA PAGINA, O CUALQUIER SOLICITUD DE NUEVO DESARROLLO.
    <table>
    <tr>
        <td colspan="2" class="TableHeader">Agregar / Actualizar Situacion</td>
        <td class="TableHeader">&nbsp;</td>
    </tr>
    <tr>
        <td>Situacion ID:</td>
        <td>
            <asp:TextBox ID="txtSituacionID" runat="server" ReadOnly="True"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="TableHeader">Fecha:</td>
        <td>
            <asp:Label ID="lblFecha" runat="server" Text="Label"></asp:Label>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="TableHeader">Titulo:</td>
        <td><asp:TextBox ID="txtTitulo" runat="server" Width="468px"></asp:TextBox></td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtTitulo" ErrorMessage="Titulo es requerido"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="TableHeader">Descripcion:</td>
        <td><asp:TextBox ID="txtDescripcion" runat="server" Height="187px" 
                TextMode="MultiLine" Width="473px"></asp:TextBox></td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="TableHeader">Activo:</td>
        <td>
            <asp:CheckBox ID="chkActivo" runat="server" Checked="True" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
                onclick="btnAgregar_Click" />
            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" 
                onclick="btnActualizar_Click" />
        </td>
        <td align="center">
            &nbsp;</td>
    </tr>
</table>
<BR />
HISTORIAL DE ACTUALIZACIONES<BR />
<asp:Panel runat="Server" id="pnlSituacionHistory">
    <asp:GridView ID="gvHistory" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="situacionID,situacionUpdateID" DataSourceID="sdsHistory">
        <Columns>
            <asp:BoundField DataField="situacionUpdateID" HeaderText="#" 
                InsertVisible="False" ReadOnly="True" SortExpression="situacionUpdateID" />
            <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" 
                HeaderText="Fecha" SortExpression="fecha" />
            <asp:BoundField DataField="descripcion" HeaderText="Descripcion" 
                SortExpression="descripcion" />
            <asp:BoundField DataField="username" HeaderText="Usuario" 
                SortExpression="username" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsHistory" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        SelectCommand="SELECT situacionHistory.situacionID, situacionHistory.situacionUpdateID, situacionHistory.descripcion, situacionHistory.fecha, Users.username FROM situacionHistory INNER JOIN Users ON situacionHistory.userID = Users.userID WHERE (situacionHistory.situacionID = @situacionID) ORDER BY situacionHistory.fecha DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtSituacionID" DefaultValue="-1" 
                Name="situacionID" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Panel>
<BR />
<BR />
<asp:Panel runat="Server" ID="pnlAddNewComment">
    <table>
        <tr>
            <td colspan="2" class="TableHeader">AGREGAR NUEVO COMENTARIO</td>
        </tr>
        <tr>
            <td class="TablaField">Descripcion:</td>
            <td>
                <asp:TextBox ID="txtHistoryDescripcion" runat="server" Height="155px" 
                    TextMode="MultiLine" Width="476px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="TablaField">
                &nbsp;</td>
            <td>
                <asp:Button ID="btnAddHistory" runat="server" onclick="btnAddHistory_Click" 
                    Text="Agregar Comentario" />
            </td>
        </tr>
    </table>
</asp:Panel>
</asp:Content>
