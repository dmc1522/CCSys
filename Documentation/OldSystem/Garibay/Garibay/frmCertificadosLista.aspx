<%@ Page Language="C#"  MasterPageFile ="~/MasterPage.Master" Title = "Lista de certificados" AutoEventWireup="true" CodeBehind="frmCertificadosLista.aspx.cs" Inherits="Garibay.frmCreditosFinancierosLista" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

         <script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>
   
        <table>
            <tr>
                <td align="left" >
                    <asp:Button ID="btnActualiza" runat="server" onclick="btnActualiza_Click" 
                        Text="Actualizar Lista" />
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader" >
                    LISTA DE CERTIFICADOS</td>
            </tr>
            <tr>
                <td >
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="sdsCertificados" DataKeyNames="CredFinCertID" 
                        onrowdatabound="GridView1_RowDataBound" ondatabound="GridView1_DataBound" 
                        ShowFooter="True">
                        <Columns>
                            <asp:BoundField DataField="Empresa" HeaderText="Depositante" 
                                SortExpression="Empresa" />
                            <asp:BoundField DataField="bodega" HeaderText="Bodega" 
                                SortExpression="bodega" />
                            <asp:BoundField DataField="numdecertificados" HeaderText="# Certificados" 
                                SortExpression="numdecertificados" />
                            <asp:BoundField DataField="EmpresaAcredita" HeaderText="Empresa Acreditada" 
                                SortExpression="EmpresaAcredita" ReadOnly="True" />
                            <asp:BoundField DataField="numCabezas" HeaderText="# Cabezas" 
                                SortExpression="numCabezas" />
                            <asp:BoundField DataField="Producto" HeaderText="Producto" 
                                SortExpression="Producto" />
                            <asp:BoundField DataField="KG" HeaderText="Unidades" 
                                SortExpression="KG" DataFormatString="{0:n2}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="Precio" 
                                DataFormatString="{0:c2}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MontoDelCert" HeaderText="Monto" 
                                SortExpression="MontoDelCert" DataFormatString="{0:c2}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="credito_cliente" HeaderText="Credito/Cliente" 
                                SortExpression="credito_cliente" ReadOnly="True" />
                            <asp:TemplateField HeaderText="Abrir">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server">Abrir</asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CredFinCertID" HeaderText="CredFinCertID" 
                                InsertVisible="False" ReadOnly="True" SortExpression="CredFinCertID" 
                                Visible="False" />
                            <asp:BoundField DataField="bodegaID" HeaderText="bodegaID" 
                                SortExpression="bodegaID" Visible="False" />
                        </Columns>
                    </asp:GridView>
                                    <asp:SqlDataSource ID="sdsCertificados" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                        SelectCommand="SET CONCAT_NULL_YIELDS_NULL OFF;
SELECT CredFinCertificados.numdeCertificados, Empresas.Empresa, CreditosFinancieros.empresaQueEmite + ClientesVentas.nombre AS EmpresaAcredita, Productos.Nombre AS Producto, CredFinCertificados.KG, CredFinCertificados.Precio, CredFinCertificados.MontoDelCert, CONVERT (varchar(255), CreditosFinancieros.numCredito) + ClientesVentas.nombre AS credito_cliente, CredFinCertificados.CredFinCertID, CredFinCertificados.bodegaID, Bodegas.bodega, CredFinCertificados.numCabezas FROM Bodegas INNER JOIN CredFinCertificados INNER JOIN Empresas ON CredFinCertificados.empresaCertificadaID = Empresas.empresaID INNER JOIN Productos ON CredFinCertificados.productoID = Productos.productoID ON Bodegas.bodegaID = CredFinCertificados.bodegaID LEFT OUTER JOIN CreditosFinancieros RIGHT OUTER JOIN Certificado_Credito_ClienteVenta ON CreditosFinancieros.creditoFinancieroID = Certificado_Credito_ClienteVenta.CreditoID ON CredFinCertificados.CredFinCertID = Certificado_Credito_ClienteVenta.CredFinCertID LEFT OUTER JOIN ClientesVentas ON Certificado_Credito_ClienteVenta.ClienteVentaID = ClientesVentas.clienteventaID ORDER BY Empresas.Empresa, Bodegas.bodega"
                                        
                                        
                                        
                                        
                                        
                                        UpdateCommand="UPDATE [CredFinCertificados] SET 
                                                    [numdeCertificados] = @numdeCertificados, [fechaEmision] = @fechaEmision,  [fechaVencimiento] = @fechaVencimiento,
                                                     [KG] = @KG,  [Precio] = @Precio,  [MontoDelCert] = @MontoDelCert  WHERE [CredFinCertID] = @CredFinCertID" 
                                        
                                        
                        
                        
                        
                        
                        
                        
                        
                        DeleteCommand="Delete from [CredFinCertificados] WHERE [CredFinCertID] = @CredFinCertID">
                                        <DeleteParameters>
                                            <asp:Parameter Name="CredFinCertID" />
                                        </DeleteParameters>
                                        <UpdateParameters>
                                            <asp:Parameter Name="numdeCertificados" Type ="Int32" />
                                            <asp:Parameter Name="fechaEmision" Type="DateTime" />
                                            <asp:Parameter Name="fechaVencimiento" Type="DateTime" />
                                            <asp:Parameter Name="KG" Type="Double" />
                                            <asp:Parameter Name="Precio" Type="Double" />
                                            <asp:Parameter Name="MontoDelCert" Type="Double" />
                                            <asp:Parameter Name="CredFinCertID" Type = "Int32"/>
                                            
                                            
                                        </UpdateParameters>
                                    </asp:SqlDataSource>
                </td>
            </tr>
    </table>


</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">


</asp:Content>
