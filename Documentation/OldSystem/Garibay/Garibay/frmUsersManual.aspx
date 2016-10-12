<%@ Page Title="Manual de Usuario" Theme="skinverde" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmUsersManual.aspx.cs" Inherits="Garibay.frmUsersManual" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/App_Themes/skinverde/basicStyle2.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width=100%>
        <tr>
            <td align="center" class="TableHeader" width="100%"><a name="TOP"></a>MANUAL DE USUARIO</td>
        </tr>
    </table>
<table>
    <tr>
        <td class="TablaField">CONTENIDO</td>
    </tr>
</table>
<ul>
    <li><a href="#CONSIDERACIONES">CONSIDERACIONES GENERALES</a></li>
    <li><a href="#INGRESO">INGRESO AL SISTEMA</a><ul>
        <li>OLVIDO DE CONTRASEÑA</li>
        </ul>
    </li>
    <li><a href="#PRODUCTORES">PRODUCTORES</a><ul>
        <li><a href="#PRODADD">AGREGAR/MODIFICAR</a></li>
        <li>ELIMINAR</li>
        </ul>
    </li>
    <li><a href="#NOTASCOMPRA">NOTAS DE COMPRA</a><ul>
        <li><a href="#NOTASCOMPRA">ORGANIZACION</a></li>
        </ul>
    </li>
</ul>
                        
   
<hr />                        
<table>
	<tr>
		<td class="TablaField"><a name="CONSIDERACIONES">CONSIDERACIONES GENERALES</a></td><td align="right"><a href="#TOP">Regresar</a></td>
	</tr>
</table>
<ul>
                <li>TODO INGRESO DE DATOS DEBE DE SER EN MAYUSCULAS</li>
                <li>EL SISTEMA CUENTA CON UN REGISTRO DE SESION.</li>
                <li>EL SISTEMA CUENTA CON REGISTRO DE INACTIVIDAD DESPUES DE UN PERIODO DE TIEMPO LA 
                    SESION SE VUELVE INVALIDA Y SE LE PEDIRA QUE SE VUELVA A REGISTRAR.</li>
            </ul>
            <hr />
             <table>
                <tr>
                    <td class="TablaField"><a name="INGRESO">
                        INGRESO AL SISTEMA</a></td>
                    <td align="right">
                        <a href="#TOP">Regresar</a></td>
                </tr>
            </table>
            <p style="text-align: justify">PARA EL INGRESO AL SISTEMA DEBE DE ESCRIBIR SU NOMBRE DE USUARIO 
            Y SU CONTRASEÑA OBTENIDAS DEL ADMINISTRADOR DEL SISTEMA.</p>
            <img src="/Imagenes_Manual/login.jpg" 
        style="height: 221px; width: 268px" />
            <p style="text-align: justify">DESPUES DE HABER INGRESADO SU NOMBRE DE USUARIO Y SU 
            CONTRASEÑA PULSE SOBRE EL BOTON &quot;&lt;&lt;ENTRAR&gt;&gt;&quot;</p>
<hr />        
<table>
	<tr>
		<td class="TablaField"><a name="PRODUCTORES">PRODUCTORES</a></td><td align="right"><a href="#TOP">Regresar</a></td>
	</tr>
</table>
<p style="text-align: justify">EL MODULO DE USUARIO PERMITE MANTENER EL CONTROL Y LA INFORMACION DE TODOS LOS PRODUCTORES, ESTA INFORMACION ES INDEPENDIENTE DEL CICLO, 
    Y ES MANTENIDA Y SELECCIONABLE ENTRE ÉSTOS.</p>
    <p style="text-align: justify">LAS PAGINAS PARA EL CONTROL DE USUARIO SE UBICAN 
        DENTRO DEL LA OPCION DEL MENU &quot;PRODUCTORES (DATOS)&quot;</p>
        <img src="/Imagenes_Manual/productoresmenu.jpg" style="text-align: center">
<hr />    
    <table>
	    <tr>
		    <td class="TablaField"><a name="PRODADD">AGREGAR/MODIFICAR PRODUCTOR</a></td><td align="right"><a href="#TOP">Regresar</a></td>
	    </tr>
    </table>
    <p style="text-align: justify">PARA INGRESAR UN NUEVO PRODUCTOR SE SIGUEN LOS 
        SIGUIENTES PASOS:</p>
    <ol>
        <li>
                SE PULSA SOBRE LA OPCION PRODUCTORES(DATOS) DEL MENU PRINCIPAL
        </li>
        <li>
            
            DESPUES SE PULSA SOBRE LA OPCION &quot;Agregar Productor&quot;</li>
        <li>
            
            &nbsp;SE MOSTRARA LA PAGINA PARA AGREGAR UN NUEVO PRODUCTOR.<BR />
            <img src="/Imagenes_Manual/ProductoresAdd.jpg" 
                style="height: 381px; width: 349px" /></li>
        <li>
            SE RELLENAN LOS DATOS DEL PRODUCTOR EN LAS CAJAS DE TEXTO. (NOTA: ES REQUERIDO 
            UN APELLIDO Y UN NOMBRE PARA PODER DAR DE ALTA UN PRODUCTOR)</li>
        <li>
            
            SE PULSA SOBRE EL BOTON VALIDAR, EL CUAL BUSCARA TODOS LOS PRODUCTORES QUE 
            TENGAN COINCIDENCIA CON EL NOMBRE DEL PRODUCTOR A AGREGAR.<br />
            <img src="/Imagenes_Manual/prodvalidar.jpg" /></li>
    </ol>
    <hr />
    <table>
	    <tr>
		    <td class="TablaField"><a name="NOTASCOMPRA">NOTAS DE COMPRA</a></td><td align="right"><a href="#TOP">Regresar</a></td>
	    </tr>
    </table>
    <p style="text-align: justify">LA ORGANIZACION DE LAS NOTAS DE COMPRA ESTA DADA POR 
        EL SIGUIENTE DIAGRAMA:</p>
    <p style="text-align: center"><img src="Imagenes_Manual/ciclo_compras.png" 
            style="height: 226px; width: 747px" /></p>  
    <p style="text-align: justify">PRIMERO SE REALIZA UNA NOTA DE COMPRA (ORDEN DE COMPRA) EN LA CUAL</p>
</asp:Content>
