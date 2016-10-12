<%@ Page Title="Reporte de seguros" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmReporteSegurosProAgro.aspx.cs" Inherits="Garibay.frmReporteSegurosProAgro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="TableField">Ciclo:</td><td>
                <asp:DropDownList ID="ddlCiclos" runat="server" AutoPostBack="True" 
                    DataSourceID="sdsCiclos" DataTextField="CicloName" DataValueField="cicloID" 
                    onselectedindexchanged="ddlCiclos_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsCiclos" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                    SelectCommand="SELECT [cicloID], [CicloName] FROM [Ciclos] ORDER BY [CicloName] DESC">
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvSeguros" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="solicitudID" DataSourceID="sdsSeguros">
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="True" 
                SortExpression="Nombre" >
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="Superficie" HeaderText="Superficie" 
                SortExpression="Superficie" DataFormatString="{0:n2}" >
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="NombrePredio" HeaderText="Predio" 
                SortExpression="NombrePredio" >
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="esquema" HeaderText="Esquema" 
                SortExpression="esquema" >
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="RiesgosCubiertos" HeaderText="RiesgosCubiertos" 
                SortExpression="Riesgos Cubiertos" >
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="dDeducible" HeaderText="Deducible" 
                SortExpression="dDeducible" DataFormatString="{0:n2}%" >
            <ItemStyle HorizontalAlign="Right" Wrap="True" />
            </asp:BoundField>
            <asp:BoundField DataField="Cobertura" HeaderText="Cobertura" 
                SortExpression="Cobertura" >
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="Sumaasegurada" HeaderText="Suma Asegurada" 
                SortExpression="Sumaasegurada" DataFormatString="{0:C2}" >
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="rendimientoProtegido" 
                HeaderText="Rendimiento Protegido" SortExpression="rendimientoProtegido" >
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="Domicilio" HeaderText="Domicilio" ReadOnly="True" 
                SortExpression="Domicilio" >
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="telefono" HeaderText="Telefono" 
                SortExpression="telefono">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsSeguros" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
        
        
        SelectCommand="SELECT LTRIM(ISNULL(Productores.apaterno, ' ') + ' ' + ISNULL(Productores.amaterno, ' ') + ' ' + Productores.nombre) AS Nombre, SegurosAgricolasPredios.Superficie, SegurosAgricolasPredios.Nombre AS NombrePredio, SegurosAgricolas.esquema, SegurosAgricolas.RiesgosCubiertos, SegurosAgricolas.dDeducible, SegurosAgricolas.Cobertura, Ciclos.Montoporhectarea AS Sumaasegurada, SegurosAgricolas.rendimientoProtegido, Solicitudes.solicitudID, LTRIM(Productores.domicilio + ' ' + ISNULL(Productores.colonia, ' ') + ' ' + Productores.poblacion + ' ' + Productores.municipio + ' ' + Estados.estado) AS Domicilio, Productores.telefono, Ciclos.cicloID FROM Solicitudes INNER JOIN Productores ON Solicitudes.productorID = Productores.productorID INNER JOIN solicitud_SeguroAgricola ON Solicitudes.solicitudID = solicitud_SeguroAgricola.solicitudID INNER JOIN SegurosAgricolasPredios ON solicitud_SeguroAgricola.sol_sa_ID = SegurosAgricolasPredios.sol_sa_ID INNER JOIN SegurosAgricolas ON solicitud_SeguroAgricola.seguroID = SegurosAgricolas.seguroID INNER JOIN Creditos ON Solicitudes.creditoID = Creditos.creditoID INNER JOIN Ciclos ON Creditos.cicloID = Ciclos.cicloID INNER JOIN Estados ON Productores.estadoID = Estados.estadoID WHERE (Ciclos.cicloID = @cicloID) ORDER BY Nombre">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlCiclos" Name="cicloID" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
