<%@ Page Language="C#" Theme="skinverde" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmAddModifyBoletas.aspx.cs" Inherits="Garibay.WebForm11" Title="Agregar Boleta"%>
<%@ Register assembly="RJS.Web.WebControl.PopCalendar.Net.2008" namespace="RJS.Web.WebControl" tagprefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Button
        {
            height: 26px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


                                                <asp:Panel ID="pnlMensaje" runat="server">
                                                    <table>
                                                        <tr>
                                                            <td style="text-align: center">
                                                                <asp:Image ID="imagenbien" runat="server" 
                ImageUrl="~/imagenes/palomita.jpg" Visible="False" />
                                                                <asp:Image ID="imagenmal" runat="server" 
                ImageUrl="~/imagenes/tache.jpg" Visible="False" />
                                                                <asp:Label ID="lblMensajetitle" runat="server" 
                SkinID="lblMensajeTitle" Text="PRUEBA"></asp:Label>
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
                                                                <asp:Label ID="lblMensajeException" runat="server" 
                SkinID="lblMensajeException" Text="SI NO HAY EXC BORREN EL TEXTO"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Button ID="btnAceptardtlst" runat="server" 
                Text="Aceptar" onclick="btnAceptardtlst_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                              <asp:Panel ID="pnlAgregar" runat="server">
                                               
                                      <table>
                                        <tr>
                                         
                                            <td class="TableHeader" align="center" colspan="2">
                                                <asp:Label ID="lblHeader" runat="server" Text="AGREGAR BOLETA"></asp:Label>
                                                <asp:TextBox ID="txtIdtoModify" runat="server" Visible="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField">
                                                CICLO:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlCiclo" runat="server" DataSourceID="sdsCiclo" 
                                                    DataTextField="CicloName" DataValueField="cicloID" Height="23px" Width="199px">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsCiclo" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                    
													SelectCommand="SELECT        cicloID, CicloName
FROM            Ciclos
WHERE cerrado=@cerrado
ORDER BY fechaInicio">
                                                	<SelectParameters>
														<asp:Parameter DefaultValue="FALSE" Name="cerrado" Type="Boolean" />
													</SelectParameters>
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                          <tr>
                                              <td class="TablaField">
                                                  PRODUCTOR:</td>
                                              <td>
                                                  <asp:DropDownList ID="ddlNewBoletaProductor" runat="server" 
                                                      DataSourceID="sdsNewBoletaProductor" DataTextField="Productor" 
                                                      DataValueField="productorID" Height="23px" Width="203px">
                                                  </asp:DropDownList>
                                                  <asp:SqlDataSource ID="sdsNewBoletaProductor" runat="server" 
                                                      ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                      SelectCommand="SELECT productorID, apaterno + ' ' + amaterno + ' ' + nombre AS Productor FROM Productores ORDER BY Productor">
                                                  </asp:SqlDataSource>
                                              </td>
                                          </tr>
                                        <tr>
                                            <td class="TablaField">
                                                # BOLETA:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewNumBoleta" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField">
                                                TICKET:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewTicket" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField">
                                                BODEGA:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlNewBoletaBodega" runat="server" 
                                                    DataSourceID="sdsNewBoletaBodega" DataTextField="bodega" 
                                                    DataValueField="bodegaID" Height="23px" Width="204px">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsNewBoletaBodega" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                    SelectCommand="SELECT [bodegaID], [bodega] FROM [Bodegas] ORDER BY [bodega]">
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField">
                                                FECHA ENTRADA:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewFechaEntrada" runat="server" ReadOnly="True"></asp:TextBox>
                                                <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txtNewFechaEntrada" 
                                                    Separator="/" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField">
                                                HORA ENTRADA:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewHoraEntrada" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField">
                                                PESO ENTRADA:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewPesoEntrada" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField">
                                                FECHA SALIDA:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewFechaSalida" runat="server" ReadOnly="True"></asp:TextBox>
                                                <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtNewFechaSalida" 
                                                    Separator="/" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField">
                                                HORA SALIDA:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewHoraSalida" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField">
                                                PESO SALIDA:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewPesoSalida" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField">
                                                PRODUCTO:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlNewBoletaProducto" runat="server" 
                                                    DataSourceID="sdsNewBoletaProductos" DataTextField="Producto" 
                                                    DataValueField="productoID" Height="23px" Width="210px">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="sdsNewBoletaProductos" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:GaribayConnectionString %>" 
                                                    
													SelectCommand="SELECT Productos.productoID, Productos.Nombre + ' - ' + Presentaciones.Presentacion AS Producto, Productos.productoGrupoID FROM Productos INNER JOIN Presentaciones ON Productos.presentacionID = Presentaciones.presentacionID ORDER BY  Productos.productoGrupoID,Productos.Nombre">
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TablaField">
                                                HUMEDAD:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewHumedad" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                          <tr>
                                              <td class="TablaField">
                                                  APLICAR DCTO . HUMEDAD:</td>
                                              <td>
                                                  <asp:CheckBox ID="chkHumedad" runat="server" />
                                              </td>
                                          </tr>
                                        <tr>
                                            <td class="TablaField">
                                                IMPUREZAS:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewImpurezas" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                          <tr>
                                              <td class="TablaField">
                                                  APLICAR DCTO. IMPUREZAS:</td>
                                              <td>
                                                  <asp:CheckBox ID="chkImpurezas" runat="server" />
                                              </td>
                                          </tr>
                                        <tr>
                                            <td class="TablaField">
                                                PRECIO:</td>
                                            <td>
                                                <asp:TextBox ID="txtNewPrecio" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                          <tr>
                                              <td class="TablaField">
                                                  APLICAR DCTO. SECADO:</td>
                                              <td>
                                                  <asp:CheckBox ID="chkSecado" runat="server" />
                                              </td>
                                          </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="btnAgregar" runat="server" onclick="btnAgregar_Click1" 
                                                    Text="Agregar" CssClass="Button" />
                                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="Button" 
                                                    onclick="btnCancelar_Click1" />
                                                <asp:Button ID="btnModificar" runat="server" onclick="btnModificar_Click" 
                                                    Text="Modificar" Visible="False" />
                                            </td>
                                        </tr>
                                                
                                    </table>
                                    </asp:Panel>
                               
                                </asp:Content>
