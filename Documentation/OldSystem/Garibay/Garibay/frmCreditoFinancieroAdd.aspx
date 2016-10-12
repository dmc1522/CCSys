<%@ Page Title="Credito Financiero" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmCreditoFinancieroAdd.aspx.cs" Inherits="Garibay.frmCreditoFinancieroAdd" %>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript" type="text/javascript" src="/scripts/divFunctions.js"></script>

    <table>
    <tr>
        <td class="TableHeader">
            AGREGAR NUEVO CREDITO FINANCIERO<asp:TextBox ID="txtIDtoMod" runat="server" 
                Visible="False" Height="16px" Width="33px"></asp:TextBox>
            
        </td>
    </tr>
    <tr>
        <td>
            <table >
                <tr>
                    <td class="TablaField">BANCO/INSTITUCION:</td>
                    <td>
                        <asp:DropDownList ID="ddlBanco" runat="server" 
                            DataSourceID="sdsBancoInstitucion" DataTextField="nombre" 
                            DataValueField="bancoID">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsBancoInstitucion" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                            SelectCommand="SELECT [bancoID], [nombre] FROM [Bancos] ORDER BY [nombre]">
                        </asp:SqlDataSource>
                    </td>
                    <td rowspan="11" valign="top" align="left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TablaField">NUMERO DE CREDITO::<td>
                        <asp:TextBox ID="txtNumCredito" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TablaField">
                        # DE CONTROL:</td>
                    <td>
                        <asp:TextBox ID="txtNumControl" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TablaField">
                        EMPRESA ACREDITADA:</td>
                    <td>
                        <asp:TextBox ID="txtEmpresaAcreditada" runat="server" Width="400px"></asp:TextBox>
                        <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtEmpresaAcreditada" 
                            ErrorMessage="Empresa Acreditada es requerido"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="TablaField">MONTO:</td>
                    <td>
                        <asp:TextBox ID="txtMonto" runat="server"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtMonto" ErrorMessage="Monto es requerido"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="TablaField">
                                        FECHA DE APERTURA:</td>
                    <td>
                        <asp:TextBox ID="txtFechaApertura" runat="server" ReadOnly="True"></asp:TextBox>
                        <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtFechaApertura" 
                            Separator="/" />
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtFechaApertura" ErrorMessage="Fecha Requerida"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="TablaField">
                    FECHA DE VENCIMIENTO:</td>
                    <td>
                        <asp:TextBox ID="txtfechaVencimiento" runat="server" ReadOnly="True"></asp:TextBox>
                        <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtfechaVencimiento" 
                            Separator="/" />
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="txtfechaVencimiento" ErrorMessage="Fecha Requerida"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="TablaField">TASA DE INTERES:</td>  
                    <td>
                        <asp:TextBox ID="txtTasaInteres" runat="server" Width="40px"></asp:TextBox>
                        %<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="txtTasaInteres" ErrorMessage="Campo Requerido"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="TablaField">PAGADO</td>  
                    <td>
						<asp:CheckBox ID="chkPagado" runat="server" /> 
                    </td>
                </tr>
                <tr>
                    <td class="TablaField">GARANTIA HIPOTECARIA:</td>  
                    <td>
                        <asp:TextBox ID="txtGarantiaHip" runat="server" Height="123px" TextMode="MultiLine" 
                            Width="395px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" 
                            Text="Agregar Nuevo Credito" />
                            <asp:Button ID="btnModificar" runat="server" onclick="btnModificar_Click" 
                            Text="Modificar Crédito" />
                            </td>
                </tr>

                     
                <tr>
                    <td colspan="2">
                                                    <asp:Panel ID="pnlMensajeAddCredito" runat="server">
                                                        <asp:Image ID="imagenmal" runat="server" ImageUrl="imagenes/tache.jpg" />
                                                        <asp:Image ID="imagenbien" runat="server" ImageUrl="~/imagenes/palomita.jpg" 
                                                            Visible="False" />
                                                        <asp:Label
                                                            ID="lblMsgResult0" runat="server" Text="Message"></asp:Label>
                                                    </asp:Panel>
                            </td>
                </tr>

                     
            </table></td>
    </tr>
</table>
<asp:UpdatePanel runat="Server" id="pnlMovimientos">
    <ContentTemplate>
        <table>
        	<tr>
        		<td>&nbsp;</td>
        	</tr>
        	<tr>
                <td class="TablaField">
                    CERTIFICADOS RELACIONADOS CON EL CRÉDITO</td>
            </tr>
            <tr>
                <td class="TablaField">
                    <asp:Button ID="btnAgregarCert" runat="server" 
                        Text="Agregar Certificado al crédito" />
                    <asp:Button ID="btnRecargaCertificados" runat="server" 
                        onclick="btnRecargaCertificados_Click" Text="Recargar lista de certificados" />
                </td>
            </tr>
        	<tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="CredFinCertID" DataSourceID="sdsCertificados" 
                        onrowdatabound="GridView1_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Empresa" HeaderText="Depositante" 
                                SortExpression="Empresa" />
                            <asp:BoundField DataField="EmpresaAcreditada" HeaderText="Empresa Acreditada" 
                                ReadOnly="True" SortExpression="EmpresaAcreditada" />
                            <asp:BoundField DataField="Producto" HeaderText="Producto" 
                                SortExpression="Producto" />
                            <asp:BoundField DataField="KG" DataFormatString="{0:n2}" HeaderText="Unidades" 
                                SortExpression="KG" />
                            <asp:BoundField DataField="Precio" DataFormatString="{0:c2}" 
                                HeaderText="Precio" SortExpression="Precio" />
                            <asp:BoundField DataField="MontoDelCert" DataFormatString="{0:c2}" 
                                HeaderText="Monto" SortExpression="MontoDelCert" />
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
                            <asp:BoundField DataField="numCredito" HeaderText="numCredito" 
                                SortExpression="numCredito" Visible="False" />
                            <asp:BoundField DataField="creditoFinancieroID" 
                                HeaderText="creditoFinancieroID" InsertVisible="False" 
                                SortExpression="creditoFinancieroID" Visible="False" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsCertificados" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        DeleteCommand="Delete from [CredFinCertificados] WHERE [CredFinCertID] = @CredFinCertID" 
                        SelectCommand="SELECT Empresas.Empresa,CreditosFinancieros.empresaQueEmite as EmpresaAcreditada, Productos.Nombre AS Producto, CredFinCertificados.KG, CredFinCertificados.Precio, CredFinCertificados.MontoDelCert, CredFinCertificados.CredFinCertID, CreditosFinancieros.numCredito, CreditosFinancieros.creditoFinancieroID FROM CredFinCertificados INNER JOIN Empresas ON CredFinCertificados.empresaCertificadaID = Empresas.empresaID INNER JOIN Productos ON CredFinCertificados.productoID = Productos.productoID INNER JOIN Certificado_Credito_ClienteVenta ON CredFinCertificados.CredFinCertID = Certificado_Credito_ClienteVenta.CredFinCertID INNER JOIN CreditosFinancieros ON Certificado_Credito_ClienteVenta.CreditoID = CreditosFinancieros.creditoFinancieroID where CreditosFinancieros.creditoFinancieroID = @creditoID" 
                        UpdateCommand="UPDATE [CredFinCertificados] SET 
                                                    [numdeCertificados] = @numdeCertificados, [fechaEmision] = @fechaEmision,  [fechaVencimiento] = @fechaVencimiento,
                                                     [KG] = @KG,  [Precio] = @Precio,  [MontoDelCert] = @MontoDelCert  WHERE [CredFinCertID] = @CredFinCertID">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtIDtoMod" DefaultValue="-1" Name="creditoID" 
                                PropertyName="Text" />
                        </SelectParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="CredFinCertID" />
                        </DeleteParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="numdeCertificados" Type="Int32" />
                            <asp:Parameter Name="fechaEmision" Type="DateTime" />
                            <asp:Parameter Name="fechaVencimiento" Type="DateTime" />
                            <asp:Parameter Name="KG" Type="Double" />
                            <asp:Parameter Name="Precio" Type="Double" />
                            <asp:Parameter Name="MontoDelCert" Type="Double" />
                            <asp:Parameter Name="CredFinCertID" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        	<tr>
                <td class="TablaField">
                    MOVIMIENTOS RELACIONADOS AL CRÉDITO</td>
            </tr>
        	<tr>
                <td class="TablaField">
                    <asp:Button ID="btnAddMov" runat="server" 
                        Text="Agregar Movimiento al crédito" />
                    <asp:Button ID="btnRecargaMovements" runat="server" onclick="ecargaMovements_Click" 
                        Text="Recargar lista de movimientos" />
                </td>
            </tr>
        	<tr>
        	    <td>
        	        <asp:GridView ID="gridMovCuentasBanco" runat="server" 
                        AutoGenerateColumns="False" CellPadding="4" 
                        DataKeyNames="movbanID,fecha,abono,cargo,subCatalogoMovBancoInternoID,subCatalogoMovBancoFiscalID,catalogoMovBancoInternoID,catalogoMovBancoFiscalID,numCheque,concepto" 
                        DataSourceID="sdsMovCredito" ForeColor="Black" GridLines="None" 
                        onrowdatabound="gridMovCuentasBanco_RowDataBound" 
                        ondatabound="gridMovCuentasBanco_DataBound" ShowFooter="True">
                        <RowStyle BackColor="#FFFBD6" Font-Size="Small" ForeColor="#333333" />
                        <HeaderStyle CssClass="TableHeader" Font-Size="Small" />
                        <AlternatingRowStyle BackColor="White" Font-Size="Small" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <Columns>
                            <asp:BoundField ConvertEmptyStringToNull="False" DataField="movbanID" 
                                HeaderText="movbanID" InsertVisible="False" ItemStyle-HorizontalAlign="Right" 
                                ReadOnly="True" SortExpression="movbanID" Visible="False">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Fecha" SortExpression="fecha">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFecha" runat="server" Text='<%# Bind("fecha") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" 
                                        Text='<%# Bind("fecha", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="chequeNombre" HeaderText="Nombre Fiscal" 
                                SortExpression="chequeNombre" />
                            <asp:BoundField DataField="facturaOlarguillo" 
                                HeaderText="# de Factura o Larguillo" SortExpression="facturaOlarguillo" />
                            <asp:BoundField DataField="numCabezas" HeaderText="# de Cabezas" />
                            <asp:TemplateField HeaderText="Concepto">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("concepto") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="catalogoMovBancoFiscal" HeaderText="Catalogo Fiscal" 
                                SortExpression="catalogoMovBancoFiscal" />
                            <asp:BoundField DataField="subCatalogoFiscal" HeaderText="Subcatalogo fiscal" 
                                SortExpression="subCatalogoFiscal" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre interno" 
                                SortExpression="nombre" />
                            <asp:BoundField DataField="catalogoMovBancoInterno" 
                                HeaderText="Catalogo interno" SortExpression="catalogoMovBancoInterno" />
                            <asp:BoundField DataField="subCatalogoInterno" HeaderText="Subcatalogo interno" 
                                SortExpression="subCatalogoInterno" />
                            <asp:BoundField DataField="numCheque" 
                                HeaderText="# de Cheque" SortExpression="numCheque" />
                            <asp:TemplateField HeaderText="Cargo" SortExpression="cargo">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCargo" runat="server" Text='<%# Bind("cargo") %>'></asp:TextBox>
                                    <br />
                                    <asp:Label ID="lblCargo" runat="server" Text='<%# Bind("cargo", "{0:c}") %>' 
                                        Visible="False"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCargo0" runat="server" Text='<%# Bind("cargo", "{0:c}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Abono" SortExpression="abono">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAbono" runat="server" Text='<%# Bind("abono") %>'></asp:TextBox>
                                    <br />
                                    <asp:Label ID="lblAbono" runat="server" Text='<%# Bind("abono", "{0:c}") %>' 
                                        Visible="False"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAbono0" runat="server" Text='<%# Bind("abono", "{0:c}") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cobrado">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkChequeCobrado" runat="server" 
                                        Checked='<%# Bind("chequecobrado") %>' />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkChequeCobrado0" runat="server" 
                                        Checked='<%# Bind("chequecobrado") %>' Enabled="False" />
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="userID" HeaderText="Usuario" ReadOnly="True" 
                                SortExpression="Nombre" />
                            <asp:TemplateField HeaderText="Abrir">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="LinkButton1" runat="server">Abrir</asp:HyperLink>
                                </ItemTemplate>
                        </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
        	        <br />
        	        <asp:SqlDataSource ID="sdsMovCredito" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                        
                        
                        SelectCommand="SELECT MovimientosCuentasBanco.movbanID, MovimientosCuentasBanco.chequeNombre, MovimientosCuentasBanco.fecha, ConceptosMovCuentas.Concepto, MovimientosCuentasBanco.abono, MovimientosCuentasBanco.cargo, MovimientosCuentasBanco.userID, MovimientosCuentasBanco.storeTS, MovimientosCuentasBanco.updateTS, MovimientosCuentasBanco.cuentaID, MovimientosCuentasBanco.ConceptoMovCuentaID, MovimientosCuentasBanco.nombre, MovimientosCuentasBanco.facturaOlarguillo, MovimientosCuentasBanco.numCabezas, MovimientosCuentasBanco.numCheque, MovimientosCuentasBanco.chequeNombre AS NombreCheque, MovimientosCuentasBanco.catalogoMovBancoFiscalID, MovimientosCuentasBanco.subCatalogoMovBancoFiscalID, MovimientosCuentasBanco.catalogoMovBancoInternoID, MovimientosCuentasBanco.subCatalogoMovBancoInternoID, MovimientosCuentasBanco.chequecobrado, MovimientosCuentasBanco.fechacobrado, catalogoMovimientosBancos_1.catalogoMovBanco AS catalogoMovBancoInterno, SubCatalogoMovimientoBanco.subCatalogo AS subCatalogoInterno, catalogoMovimientosBancos.catalogoMovBanco AS catalogoMovBancoFiscal, SubCatalogoMovimientoBanco_1.subCatalogo AS subCatalogoFiscal, CreditosFinancieros.creditoFinancieroID, Bancos.nombre AS bancoCreditoFinanciero, CreditosFinancieros.numCredito AS numCreditoFinanciero, MovimientosCuentasBanco.fechaCheque FROM Bancos INNER JOIN CreditosFinancieros ON Bancos.bancoID = CreditosFinancieros.bancoID INNER JOIN MovimientosCuentasBanco INNER JOIN ConceptosMovCuentas ON MovimientosCuentasBanco.ConceptoMovCuentaID = ConceptosMovCuentas.ConceptoMovCuentaID INNER JOIN catalogoMovimientosBancos ON MovimientosCuentasBanco.catalogoMovBancoFiscalID = catalogoMovimientosBancos.catalogoMovBancoID INNER JOIN catalogoMovimientosBancos AS catalogoMovimientosBancos_1 ON MovimientosCuentasBanco.catalogoMovBancoInternoID = catalogoMovimientosBancos_1.catalogoMovBancoID ON CreditosFinancieros.creditoFinancieroID = MovimientosCuentasBanco.creditoFinancieroID LEFT OUTER JOIN SubCatalogoMovimientoBanco ON MovimientosCuentasBanco.subCatalogoMovBancoInternoID = SubCatalogoMovimientoBanco.subCatalogoMovBancoID LEFT OUTER JOIN SubCatalogoMovimientoBanco AS SubCatalogoMovimientoBanco_1 ON MovimientosCuentasBanco.subCatalogoMovBancoFiscalID = SubCatalogoMovimientoBanco_1.subCatalogoMovBancoID WHERE (MovimientosCuentasBanco.creditoFinancieroID = @creditoFinancieroID)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtIDtoMod" DefaultValue="-1" 
                                Name="creditoFinancieroID" PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
        	    </td>
        	</tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
