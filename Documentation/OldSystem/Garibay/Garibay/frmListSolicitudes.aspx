<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="True" CodeBehind="frmListSolicitudes.aspx.cs" Inherits="Garibay.frmListSolicitudes" Title="Solicitudes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
            height: 17px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0">
    <ProgressTemplate>
        <asp:Image ID="imgLoading" runat="server" ImageUrl="~/imagenes/cargando.gif" />
        CARGANDO INFORMACION...
    </ProgressTemplate>
    </asp:UpdateProgress>
    <table>
    <tr>
        <td>
        
            <asp:Panel ID="panelmensaje" runat="server">
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
                            <asp:Label ID="lblMensajeOperationresult" runat="server" 
                                SkinID="lblMensajeOperationresult" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Label ID="lblMensajeException" runat="server" SkinID="lblMensajeException" 
                                Text="SI NO HAY EXC BORREN EL TEXTO"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        
        </td>
    </tr>
  <tr>
   <td class="TableHeader">
       LISTA DE SOLICITUDES</td>
  </tr>
  <tr>
   <td>
    <asp:Panel ID="panelFiltros" runat="server" GroupingText="Filtrar resultados">
     <table>
      <tr>
       <td class="TablaField">
           Ciclo:</td>
       <td>
           <asp:DropDownList ID="ddlCiclo" runat="server" Height="22px" Width="160px" 
               DataSourceID="dataSourceCiclo" DataTextField="CicloName" 
               DataValueField="cicloID" AutoPostBack="True" 
               onselectedindexchanged="ddlCiclo_SelectedIndexChanged">
           </asp:DropDownList>
           <asp:SqlDataSource ID="dataSourceCiclo" runat="server" 
               ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
               
               SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [CicloName] DESC">
           </asp:SqlDataSource>
       </td>
       <td class="TablaField">
           <asp:CheckBox ID="cbCredito" runat="server" CssClass="TablaField" 
               Text="Crédito" />
           &nbsp;</td>
       <td>
           <asp:DropDownList ID="ddlCredito" runat="server" Height="22px" Width="240px" 
               DataSourceID="dataSourceCredito" DataTextField="nombre" 
               DataValueField="creditoID">
           </asp:DropDownList>
           <asp:SqlDataSource ID="dataSourceCredito" runat="server" 
               ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
               
               SelectCommand="SELECT Creditos.creditoID, CAST(Creditos.creditoID AS nvarchar) + ' - ' + Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS nombre FROM Creditos  INNER JOIN Productores ON Creditos.productorID = Productores.productorID WHERE cicloID = @cicloID">
               <SelectParameters>
                   <asp:ControlParameter ControlID="ddlCiclo" Name="cicloID" 
                       PropertyName="SelectedValue" />
               </SelectParameters>
           </asp:SqlDataSource>
          </td>
      </tr>
      <tr>
       <td class="TablaField">Mostrar:</td>
       <td colspan="3">
           <asp:CheckBoxList ID="cblMostrar" runat="server" CssClass="TablaField" 
               RepeatColumns="3" RepeatDirection="Horizontal">
               <asp:ListItem>Experiencia</asp:ListItem>
               <asp:ListItem>Plazo</asp:ListItem>
               <asp:ListItem>Recursos propios</asp:ListItem>
               <asp:ListItem>Descripción de garantías</asp:ListItem>
               <asp:ListItem>Valor de garantías</asp:ListItem>
           </asp:CheckBoxList>
          </td>
      </tr>
      <tr>
       <td align="center" colspan="4">
           <asp:Button ID="btnActualiza" runat="server" Text="Actualizar columnas" 
               onclick="btnActualiza_Click" />
           <asp:Button ID="btnSelAll" runat="server" Text="Seleccionar todas" 
               onclick="btnSelAll_Click" />
           <asp:Button ID="btnUnSel" runat="server" Text="Quitar seleccion" 
               onclick="btnUnSel_Click" />
          </td>
      </tr>
      <tr>
       
       <td align="center" colspan="4">
                  <asp:Button ID="btnFiltrar" runat="server" onclick="btnFiltrar_Click" 
               style="height: 26px" Text="Filtrar" Width="80px" />

       </td>
      </tr>
     </table>
    </asp:Panel>
   </td>
  </tr>
  <tr>
   <td align="center" class="style3">
       <asp:Button ID="btnUpdateStatus" runat="server" Height="22px" 
           onclick="btnUpdateStatus_Click" Text="Actualiza estado de solicitud" 
           Width="266px" />
       <asp:Button ID="btnAgregar" runat="server" CssClass="Button" onclick="btnAgregar_Click" 
           Text="Agregar" Width="100px" />
       <asp:Button ID="btnImprimir" runat="server" CssClass="Button" 
           Text="Imprimir Formato Solicitud" />
       <asp:Button ID="btnImprimirContrato" runat="server" 
           onclick="btnImprimirContrato_Click" Text="Imprimir Contrato" />
       <asp:Button ID="btnEliminar" runat="server" onclick="btnEliminar_Click1" 
           Text="Eliminar Solicitud" />
      </td>
  </tr>
  <tr>
   <td align="center">
       <asp:GridView ID="gridSolicitudes" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
           DataKeyNames="solicitudID" 
           DataSourceID="dataSourceLista" 
           onselectedindexchanged="gridSolicitudes_SelectedIndexChanged" 
           onrowdatabound="gridSolicitudes_RowDataBound">
           <Columns>
               <asp:CommandField ButtonType="Button" SelectText="&gt;" 
                   ShowSelectButton="True" />
               <asp:BoundField HeaderText="# Solicitud" DataField="solicitudID" 
                   InsertVisible="False" ReadOnly="True" SortExpression="solicitudID" >
                   <HeaderStyle Font-Size="XX-Small" />
                   <ItemStyle HorizontalAlign="Right" />
               </asp:BoundField>
               <asp:BoundField HeaderText="# Credito" DataField="creditoID" 
                   InsertVisible="False" ReadOnly="True" SortExpression="creditoID" >
                   <HeaderStyle Font-Size="XX-Small" />
                   <ItemStyle HorizontalAlign="Right" />
               </asp:BoundField>
               <asp:BoundField HeaderText="Nombre" DataField="nombre" 
                   SortExpression="nombre" ReadOnly="True" >
                   <ItemStyle Wrap="False" />
               </asp:BoundField>
               <asp:BoundField DataField="Monto" 
                   HeaderText="Monto" SortExpression="Monto" DataFormatString="{0:c}">
                   <ItemStyle HorizontalAlign="Right" />
               </asp:BoundField>
               <asp:BoundField HeaderText="Superficie a sembrar" DataField="Superficieasembrar" 
                   SortExpression="Superficieasembrar" DataFormatString="{0:N2}" >
                   <ItemStyle HorizontalAlign="Right" />
               </asp:BoundField>
               <asp:BoundField HeaderText="Experiencia" DataField="Experiencia" 
                   SortExpression="Experiencia" Visible="False" >
                   <ItemStyle HorizontalAlign="Right" />
               </asp:BoundField>
               <asp:BoundField DataField="Plazo" HeaderText="Plazo" SortExpression="Plazo" 
                   Visible="False">
                   <ItemStyle HorizontalAlign="Right" />
               </asp:BoundField>
               <asp:BoundField DataField="RecursosPropios" DataFormatString="{0:c}" 
                   HeaderText="Recursos propios" SortExpression="RecursosPropios" 
                   Visible="False">
                   <ItemStyle HorizontalAlign="Right" />
               </asp:BoundField>
               <asp:BoundField DataField="Descripciondegarantias" 
                   HeaderText="Descripción de garantías" SortExpression="Descripciondegarantias" 
                   Visible="False" />
               <asp:BoundField DataField="Valordegarantias" DataFormatString="{0:c}" 
                   HeaderText="Valor de garantías" SortExpression="Valordegarantias" 
                   Visible="False">
                   <ItemStyle HorizontalAlign="Right" />
               </asp:BoundField>
               <asp:BoundField DataField="cicloID" HeaderText="cicloID" 
                   SortExpression="cicloID" Visible="False" />
               <asp:BoundField DataField="status" HeaderText="Estado Actual" 
                   SortExpression="status" />
               <asp:TemplateField HeaderText="Estado Nuevo" InsertVisible="False">
                   <EditItemTemplate>
                       <asp:Label ID="Label1" runat="server"></asp:Label>
                   </EditItemTemplate>
                   <ItemTemplate>
                       <asp:DropDownList ID="drpdlStatus" runat="server" DataSourceID="sdsStatus" 
                           DataTextField="status" DataValueField="statusID" 
                           SelectedValue='<%# Bind("statusID") %>'>
                       </asp:DropDownList>
                       <asp:SqlDataSource ID="sdsStatus" runat="server" 
                           ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                           SelectCommand="SELECT [statusID], [status] FROM [SolicitudStatus]">
                       </asp:SqlDataSource>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:BoundField DataField="statusID" HeaderText="statusID" 
                   SortExpression="statusID" Visible="False" />
               <asp:TemplateField HeaderText="Abrir Solicitud">
                   <ItemTemplate>
                       <asp:HyperLink ID="lnkSolicitud2010" runat="server">Abrir</asp:HyperLink>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Solicitud">
                   <ItemTemplate>
                       <asp:LinkButton ID="LBSolicitud2010" runat="server">IMPRIMIR</asp:LinkButton>
                   </ItemTemplate>
                   <EditItemTemplate>
                       <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                   </EditItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Contrato" InsertVisible="False">
                   <EditItemTemplate>
                       <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                   </EditItemTemplate>
                   <ItemTemplate>
                       <asp:LinkButton ID="LBPrintContrato" runat="server">IMPRIMIR</asp:LinkButton>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Term y Cond.">
                   <ItemTemplate>
                       <asp:LinkButton ID="LBTermsAndCon" runat="server">IMPRIMIR</asp:LinkButton>
                   </ItemTemplate>
                   <EditItemTemplate>
                       <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                   </EditItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="PAGARE">
                   <ItemTemplate>
                       <asp:HyperLink ID="lnkPagare" runat="server" 
                           NavigateUrl="~/frmListSolicitudes.aspx">IMPRIMIR</asp:HyperLink>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Buro crédito">
                   <ItemTemplate>
                       <asp:LinkButton ID="LBBuroCredito" runat="server">IMPRIMIR</asp:LinkButton>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="C. Compromiso">
                   <ItemTemplate>
                       <asp:LinkButton ID="LBCartaCompromiso" runat="server">IMPRIMIR</asp:LinkButton>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Evaluacion">
                   <ItemTemplate>
                       <asp:LinkButton ID="LBEvaluacion" runat="server">EVALUACION</asp:LinkButton>
                   </ItemTemplate>
               </asp:TemplateField>
           </Columns>
       </asp:GridView>
       <asp:SqlDataSource ID="dataSourceLista" runat="server" 
           ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
           
           
           
           
           SelectCommand="SELECT Solicitudes.solicitudID, Solicitudes.creditoID, Productores.apaterno + ' ' + Productores.amaterno + ' ' + Productores.nombre AS nombre, Solicitudes.Experiencia, Solicitudes.Monto, Solicitudes.Superficieasembrar, Solicitudes.Plazo, Solicitudes.RecursosPropios, Solicitudes.Descripciondegarantias, Solicitudes.Valordegarantias, Creditos.cicloID, SolicitudStatus.status, SolicitudStatus.statusID FROM Solicitudes INNER JOIN Productores ON Solicitudes.productorID = Productores.productorID INNER JOIN Creditos ON Solicitudes.creditoID = Creditos.creditoID INNER JOIN SolicitudStatus ON Solicitudes.statusID = SolicitudStatus.statusID ORDER BY nombre">
       </asp:SqlDataSource>
      </td>
  </tr>
 </table>
 </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
