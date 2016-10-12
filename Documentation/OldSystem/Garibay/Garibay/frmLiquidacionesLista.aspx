<%@ Page Title="Lista de Liquidaciones" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmLiquidacionesLista.aspx.cs" Inherits="Garibay.frmLiquidacionesLista" %>
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
                    <td class="TableHeader" rowspan="3">
                        FILTROS:</td>
                    <td class="TablaField">
                        Ciclo:</td>
                    <td>
                        <asp:DropDownList ID="drpdlCiclo" runat="server" AutoPostBack="True" 
                            DataSourceID="sdsCiclos" DataTextField="CicloName" DataValueField="cicloID" 
                            Height="23px" Width="164px" 
                            onselectedindexchanged="drpdlCiclo_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            
                            SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [fechaInicio] DESC">
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td class="TablaField">
                        Productor:</td>
                    <td>
                    <br />
                        <asp:DropDownList ID="dprdlProductor" runat="server" AutoPostBack="True" 
                            DataSourceID="sdsProductores" DataTextField="nameproductor" 
                            DataValueField="productorID" Height="23px" 
                            ondatabound="dprdlProductor_DataBound" 
                            onselectedindexchanged="dprdlProductor_SelectedIndexChanged" Width="253px">
                        </asp:DropDownList>
                        <cc1:ListSearchExtender ID="dprdlProductor_ListSearchExtender" runat="server" 
                            Enabled="True" PromptText="Escribe para buscar" 
                            TargetControlID="dprdlProductor">
                        </cc1:ListSearchExtender>
                        <asp:SqlDataSource ID="sdsProductores" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" SelectCommand="SELECT distinct Liquidaciones.productorID, Productores.apaterno + ' ' + Productores.amaterno + ' '  + Productores.nombre AS nameproductor FROM Productores INNER JOIN Liquidaciones ON Productores.productorID = Liquidaciones.productorID  where Liquidaciones.cicloID = @cicloID order by nameproductor 
">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="drpdlCiclo" DefaultValue="-1" Name="cicloID" 
                                    PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td class="TablaField">
                        Estado:</td>
                    <td>
                        <asp:DropDownList ID="drpdlEstado" runat="server" AutoPostBack="True" 
                            Height="23px" onselectedindexchanged="drpdlEstado_SelectedIndexChanged" 
                            Width="182px">
                            <asp:ListItem Value="-1">MOSTRAR TODAS</asp:ListItem>
                            <asp:ListItem Value="0">NO COBRADAS</asp:ListItem>
                            <asp:ListItem Value="1">COBRADAS</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
	</tr>
	<tr>
	    <td>
            <asp:GridView ID="gvLiquidaciones" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="LiquidacionID,cobrada" DataSourceID="sdsLiquidaciones" 
                onselectedindexchanged="GridView1_SelectedIndexChanged" 
                onrowdatabound="gvLiquidaciones_RowDataBound" 
                ondatabound="gvLiquidaciones_DataBound">
                <Columns>
                    <asp:CommandField ButtonType="Button" ShowSelectButton="True" 
                        SelectText=" &gt; " />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" 
                        SortExpression="nombre" >
                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LiquidacionID" HeaderText="# Liquidacion" 
                        InsertVisible="False" ReadOnly="True" SortExpression="LiquidacionID" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CicloName" HeaderText="Ciclo" 
                        SortExpression="CicloName" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" 
                        DataFormatString="{0:dd/MM/yyy}" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="domicilio" HeaderText="Domicilio" 
                        SortExpression="domicilio" >
                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="poblacion" HeaderText="Problacion" 
                        SortExpression="poblacion" >
                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Boletas" DataFormatString="{0:c}" 
                        HeaderText="Total en Boletas" SortExpression="subTotal" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Anticipos" DataFormatString="{0:C2}" 
                        HeaderText="Anticipos" SortExpression="Anticipos">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Pagos_Creditos" DataFormatString="{0:C2}" 
                        HeaderText="Pagos_Creditos" SortExpression="Pagos_Creditos">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Pagos" DataFormatString="{0:C2}" HeaderText="Pagos" 
                        SortExpression="Pagos">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:CheckBoxField DataField="cobrada" HeaderText="Ya fue cobrada" 
                        ReadOnly="True" SortExpression="cobrada" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:CheckBoxField>
                    <asp:TemplateField HeaderText="Impresion">
                        <ItemTemplate>
                            <asp:HyperLink ID="LinkButton1" runat="server">IMPRIMIR</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Abrir" Visible="False">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkOpenLiq" runat="server">Abrir</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Abrir 2010">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" 
                                NavigateUrl='<%# GetNewOpenLiqUrl(Eval("LiquidacionID").ToString()) %>'>Abrir</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsLiquidaciones" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                
                
                
                
                SelectCommand="SELECT Liquidaciones.LiquidacionID, Liquidaciones.productorID, Ciclos.CicloName, Liquidaciones.fecha, Liquidaciones.nombre, Liquidaciones.domicilio, Liquidaciones.poblacion, Liquidaciones.cobrada, vTotalesLiquidacion.Boletas, vTotalesLiquidacion.Anticipos, vTotalesLiquidacion.Pagos_Creditos, vTotalesLiquidacion.Pagos FROM Liquidaciones INNER JOIN Ciclos ON Liquidaciones.cicloID = Ciclos.cicloID INNER JOIN vTotalesLiquidacion ON Liquidaciones.LiquidacionID = vTotalesLiquidacion.LiquidacionID WHERE (Liquidaciones.cicloID = @cicloID) ORDER BY Liquidaciones.nombre, Liquidaciones.fecha DESC, Liquidaciones.cobrada DESC">
                <SelectParameters>
                    <asp:ControlParameter ControlID="drpdlCiclo" DefaultValue="-1" Name="cicloID" 
                        PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:Button ID="btnOpenLiq" Text="Abrir Liquidación" runat="Server" 
                onclick="btnOpenLiq_Click" Visible="False" />
            <asp:Button ID="btnEliminarLiq" runat="server" Text="Eliminar Liquidación" 
                Visible="False" onclick="btnEliminarLiq_Click" />
        </td>
	</tr>
        <tr>
            <td>
                <asp:Panel ID="pnlDeleteLiq" runat="server" Visible="False">
                    <asp:Image ID="imgBien" runat="server" ImageUrl="~/imagenes/palomita.jpg" />
                    <asp:Image ID="imgMal" runat="server" ImageUrl="~/imagenes/tache.jpg" />
                    <asp:Label ID="lblDeleteResult" runat="server"></asp:Label>
                </asp:Panel>
            </td>
        </tr>
</table>
 </ContentTemplate>
 </asp:UpdatePanel> 
</asp:Content>
