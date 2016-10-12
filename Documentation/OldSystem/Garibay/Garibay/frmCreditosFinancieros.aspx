<%@ Page Title="" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmCreditosFinancieros.aspx.cs" Inherits="Garibay.frmCreditosFinancieros" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>
    <table >
	<tr>
		<td class="TableHeader">
		FILTROS
		
		</td>
	</tr>
	<tr>
		<td>
		<table>
			<tr>
				<td class="TablaField">ESTATUS</td>
				<td>
					<asp:DropDownList ID="ddlEstado" runat="server">
						<asp:ListItem Selected="True" Value="0">TODOS</asp:ListItem>
						<asp:ListItem Value="1">PAGADOS</asp:ListItem>
						<asp:ListItem Value="2">NO PAGADOS</asp:ListItem>
					</asp:DropDownList>
				</td>
				
			</tr>
			<tr>
				<td colspan="2">
					<asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" 
						onclick="btnFiltrar_Click" />
				</td>
				
			</tr>
		</table>
		
		
		</td>
	</tr>
	<tr>
	<td>
	
	</td>
	</tr>
	<tr>
	    <td>
            <asp:GridView ID="gridCreditos" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" 
                DataKeyNames="creditoFinancieroID,nombre,numCredito,empresa_acreditada,monto,fechadeapertura,fechadevencimiento,tasadeinteres" 
                DataSourceID="sdsCreditos" PageSize="25" 
                onrowdeleting="gridCreditos_RowDeleting" 
                onrowdatabound="gridCreditos_RowDataBound">
                <Columns>
                    <asp:CommandField ButtonType="Button" SelectText=" &gt; " DeleteText="Eliminar" 
                        ShowDeleteButton="True" />
                    <asp:BoundField DataField="creditoFinancieroID" 
                        HeaderText="CreditoFinancieroID" Visible="False" />
                    <asp:BoundField DataField="nombre" HeaderText="Banco" SortExpression="nombre">
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="# Credito" SortExpression="numCredito">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("numCredito") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("numCredito") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="empresa_acreditada" HeaderText="Empresa acreditada" 
                        SortExpression="empresa_acreditada">
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="monto" DataFormatString="{0:c}" HeaderText="Monto" 
                        SortExpression="monto">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechadeapertura" DataFormatString="{0:dd/MM/yyyy}" 
                        HeaderText="Fecha Apertura" SortExpression="fechadeapertura">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechadevencimiento" 
                        DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Vencimiento" 
                        SortExpression="fechadevencimiento">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="tasadeinteres" 
                        HeaderText="Tasa de Interes" SortExpression="tasadeinteres">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:CheckBoxField DataField="pagado" HeaderText="Pagado" 
						SortExpression="pagado" />
                    <asp:TemplateField HeaderText="Abrir">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server">Abrir</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsCreditos" runat="server" 
                ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                
                SelectCommand="SELECT CreditosFinancieros.creditoFinancieroID, Bancos.nombre, CreditosFinancieros.numCredito, CreditosFinancieros.empresa_acreditada, CreditosFinancieros.monto, CreditosFinancieros.fechadeapertura, CreditosFinancieros.fechadevencimiento, CreditosFinancieros.tasadeinteres, CreditosFinancieros.userID, CreditosFinancieros.storeTS, CreditosFinancieros.updateTS, CreditosFinancieros.pagado FROM Bancos INNER JOIN CreditosFinancieros ON Bancos.bancoID = CreditosFinancieros.bancoID ORDER BY CreditosFinancieros.fechadevencimiento" 
                
                
                
				DeleteCommand="DELETE FROM CreditosFinancieros WHERE (creditoFinancieroID = @creditosfinancierosID)">
                <DeleteParameters>
                    <asp:Parameter Name="creditosfinancierosID" />
                </DeleteParameters>
            </asp:SqlDataSource>
        </td>
	</tr>
</table>


</asp:Content>
