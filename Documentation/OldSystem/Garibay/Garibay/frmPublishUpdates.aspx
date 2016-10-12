


<%@ Page Title="Ultimas actualizaciones" Theme="skinverde"Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmPublishUpdates.aspx.cs" Inherits="Garibay.frmPublishUpdates" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul>
         <li>5ta reunion<ul>
             <li>cambios menores en las paginas</li>
             <li>nuevo menu agregado.</li>
             <li>las observaciones se imprimen en la nota de venta.</li>
             <li>se muestra la opcion para abrir la factura de gasolinera.</li>
             <li>Se hace la resta entre el monto de la factura de gasolinera contra la suma de 
                 los montos de las tarjetas diesel.</li>
             <li>Corregido detalle al realizar la busqueda en la pagina de busqueda.</li>
             <li>El estado de cuenta ahora muestra los pagos hechos con boletas de entrada</li>
             <li>Los productos se filtran por grupo al agregar una nota de compra</li>
             <li>Se agregaron nuevos elementos de búsqueda en la página de busqueda general</li>
             <li>se puede pagar con boletas de maiz las notas de venta.</li>
             <li>se acomodaron en paneles para realizar los pagos en la nota de venta.</li>
             <li>Se agregaron las opciones de pago al credito como: efectivo, transferencias, 
                 depositos, boletas de maiz.</li>
             <li>la nota se autosalva al realizar modificaciones</li>
             <li>los productos en la nota de compra se agrupan por el grupo de producto.</li>
             <li>Se borran los archivos temporales en la impresión de la nota de compra.</li>
             <li>La página de búsqueda general se modificó para que sólo permita hacer la búsqueda en modulos que el cliente elija.</li>
             <li>Corregido problema con la factura de venta.</li>
             
             </ul>
           
         
        </li>
         <li>22/04/2010<ul>
        
             <li>Los pagos se cargan correctamente en estado de cuenta</li>
             <li>Cambios en estado de cuenta de productor, para evitar error que se pasaba de la fecha</li>
             <li>Los elementos que son "ceros" ya no son mostrados en estado de cuenta</li>
             <li>impresion de nota de venta probada.</li>
             <li>la seleccion de las tarjetas diesel para relacionarlas a una factura de gasolina 
                 ahora se hacen por checkboxes en lugar de seleccionar una por una.</li>
             <li>al eliminar un movimiento de caja chica se elimina la relacion con los pagos de 
                 facturas de venta.</li>
             <li>al eliminar un movimiento de bancos&nbsp; se elimina la relacion con los pagos 
                 de facturas de venta.</li>
             <li>Las facturas de tarjetas diesel ahora se llaman facturas de gasolina.</li>
             <li>Acomodado formato de fechas en los pagos de nota de venta</li>
             <li>Agregada pagina para relacionar un prestamo a un credito</li>
             <li>Se pueden agregar facturas de gasolina y se relacionan con tarjetas diesel 
                 correctamente.</li>
             <li>Se puede modificar el folio de una tarjeta diesel despues de agregar una en la 
                 caja chica.</li>
             <li>Se cambio el boton de selecciona para eliminar un movimiento de caja chica por 
                 un hipervinculo.</li>
             <li>ya se tiene el monto de la factura de venta.</li>
             <li></li>
             </ul>
           
         
        </li>
         <li>21/04/2010<ul>
             <li>Nota de venta se imprime correctamente</li>
             <li>Se muestra la lista de prestamos</li>
             <li>Los productos en la nota de venta se agrupan por el grupo de producto</li>
             <li>Ya se pueden relacionar (conciliar) las tarjetas diesel con una factura diesel</li>
             <li>Se ve la lista de facturas de diesel</li>
             <li>se puede relacionar las tarjetas diesel con una factura de diesel</li>
             </ul>
           
         
        </li>
         <li>20/04/2010<ul>
             <li>la tasa de interes de los creditos financieros ahora es texto.</li>
             <li>Al agregar un pago a un credito, ahora es reflejado en el estado de cuenta.</li>
             <li>La existencia se actualiza correctamente al guardar o actualizar una nota de 
                 venta</li>
             <li>la existencia se actualiza correctamente en la ota de compra</li>
             <li>Corregido problema al calcular los pagos en la nota de venta.</li>
             </ul>
           
         
        </li>
         <li>4ta reunion<ul>
             <li>VERIFICAR CALCULO DE PLAZO. Ahora ya se calcula bien automáticamente el plazo 
                 del crédito en la solicitud.</li>
             <li>No es necesario que sea relacionado a un predio, se muestra una leyenda con una 
                 advertencia, pero hay créditos que no solicitan apoyos.</li>
             <li>Nuevo menu se ha agregado.</li>
             <li>Presentación de 50 kg agregada a la base de datos.</li>
             <li>acomodado fecha al seleccionarla en la nota de venta</li>
             <li>Se permite agregar los pagos a un credito.</li>
             <li>Los productos se agregan correctamente en la nota de compra</li>
             <li>Los productos de la nota de venta se permite modificar</li>
             <li>los productos en la nota de compra se permite modificar</li>
             <li>En la lista de productos se muestra concatenado la presentacion</li>
             <li>las tarjetas diesel ya se muestran en el credito</li>
             </ul>
           
         
        </li>
         <li>15/04/2010<ul>
             <li>Al cambiarse el estado de una solicitud, se cambia el status del credito.</li>
             <li>Corregido detalle para mostrar correctamente los filtros de la lista de notas de 
                 venta.</li>
             <li>Los botones que no era necesario causar validacion en la nota de venta se han 
                 corregido.</li>
             <li>Cuando una nota de venta es guardada se muestra un mensaje que dice que fue 
                 guardada correctamente</li>
             <li>Si un movimiento de caja chica tiene una tarjeta diesel relacionada, los dos 
                 pueden ser modificados.</li>
             <li>La lista de creditos permite el filtrado de éstas.</li>
             <li></li>
             </ul>
           
         
        </li>
         <li>14/04/2010<ul>
             <li>arreglado detalle de calendarios en la nota de venta</li>
             <li>en la nota de venta el iva es calculado correctamente</li>
             <li>el formato inicial de la fecha cuando es cargada una nota de venta ha sido 
                 corregido.</li>
             <li>Cuando un pago es eliminado de un credito se eliminan los movimientos 
                 relacionados.</li>
             <li>En las notas de venta y facturas de venta se acomodo el ciclo que es 
                 preseleccionado.</li>
             <li>Arreglado diseño para agregar tarjetas diesel compradas con un movimiento de 
                 caja chica.</li>
             <li>se pueden agregar tarjetas diesel a los movimientos de caja chica.</li>
             </ul>
           
         
        </li>
         <li>13/04/2010<ul>
             <li>Se agrego la lista de tarjetas diesel</li>
             <li>La presentacion se muestra en la listas de productos para diferenciarlos</li>
             <li>La entrada de productos esta en el menu</li>
             <li>el orden de los productos se acomodo en la lista de entrada de productos</li>
             <li>en la lista de anticipos se muestra si el anticipo fue pagado.</li>
             <li>en la lista de anticipos se muestra la liquidacion donde fue pagada.</li>
             <li>Los periodos pueden ser bloqueados para evitar agregar, modificar y eliminar 
                 movimientos.</li>
             </ul>
           
         
        </li>
         <li>Cambios exclusivos de solicitudes<ul>
                <li>AGREGAR EXTENDER PARA DROPDOWN DE PRODUCTOR EN SOLICITUDES PARA PODER TECLEAR EL NOMBRE A BUSCAR</li>
                <li>CAMBIAR ORDER DE VALOR DE GARANTIAS Y MONTO DE GARANTIAS (LOS TEXTBOXES ESTÁN VOLTEADOS)</li>
                <li>FORMATO DE SOLICITUD: POBLACION + MUNICIPIO + ESTADO</li>
                <li>MODIFICAR EL FORMATO DE SOLICITUD EN EL CAMPO PLAZO</li>
                <li>AL AGREGARLA QUE SALGAN LOS LINKS PARA LA IMPRESIÓN</li>
                <li>PERMITIR ELIMINAR FIRMAS AUTORIZADAS</li>
                <li>VERIFICAR XQ NO SE AGREGA LA FIRMA EN SERVER REAL.</li>
                <li>CAMPO CLAVE DE LA PERSONA ES IGUAL A NÚMERO DE SOLICITUD (FORMATO SOLICITUD).</li>
                <li>CONTRATO VERIFICAR LAS LINEAS DEL FORMATO LÍNEA IZQUIERDA CONTINUA Y QUE SALGA 
                    EN UNA HOJA TAMAÑO CARTA. </li>
                <li>AGREGAR UBICACIÓN DE LA GARANTÍA Y MERTERO AL FORMATO DE LA CARÁTULA ANEXA</li>
                <li>AJUSTAR CAMPO DE COSTO DE CULTIVO DE MAIZ DE TEMPORAL</li>
                <li>DESCRIPCION DE GARANTIAS EN SOLICITUD MÁS AMPLIO</li>
                <li>TASA FIJA = 18 ANUAL EN CARATULA ANEXA</li>
                <li>TASA ANUAL = 18 EN PAGARÉ TAMBIÉN</li>
                <li>POR LO PRONTO QUITAR EL INTERES EN PAGARÉ</li>
                <li>VERIFICAR FECHA EN PAGARÉ QUE NO DIGA EL NOMBRE DEL DÍA</li>
                <li>A 05 DE AGOSTO DE 2010 *** EJEMPLO</li>
                <li>VERIFICAR LOS DOMICILIOS AL AGREGAR SOLICITUD.</li>
                <li>CUANDO SE MODIFIQUE LA FECHA QUE SE RECALCULE PLAZO</li>
                <li>FORMATO DE SEGURO QUITAR LA SEGUNDA HOJA Y MODIFICAR</li>
                <li>BORRAR DATOS DEL SEGURO AL CAMBIAR DROPDWONLIST A SIN SEGURO</li>
                <li>AGREGAR FIRMA DE ACREDITANTE (LAS MARGARITAS).</li>
             </ul>
           
         
        </li>
         <li>3a Junta<ul>
            <li>Cambio en el diseño de las solicitudes</li>
            <li>Firmas autorizadas se pueden agregar a la solicitud</li>
            <li>Se puede filtrar los créditos en la lista</li>
            <li>Se pueden agregar los pagos en la misma página de facturas de venta, notas de venta y notas de compra.</li>
            <li>Se pueden eliminar las solicitudes.</li>
            <li>Se pueden eliminar los créditos.</li>
            <li>Se cambió el diseño de la página de inicio.</li>
            <li>Se permite imprimit el formato de carátula anexa y pagaré en la lista de solicitudes</li>
            <li>Se prerellenan los datos de los pagos en la nota de compra</li>
            <li>Se prerellenan los datos de los pagos en la nota de venta</li>
            <li>Se prerellenan los datos de los pagos en la Factura de venta</li>
            <li>Se actualizo la pagina de inicio para mostrar mas opciones comunes</li>
            <li>Se acomodo la impresion de cheques de cuenta ganadera y de la cuena de iprojal, 
             ahora la configuracion es independiente de cuenta</li>
            <li>No se permite realizar modificaciones sobre movimientos conciliados/cobrados</li>
            <li>Los movimientos de caja chica permiten mostrar en que liquidacion fueron 
             pagados.</li>
            <li>modificaciones en las solicitudes para ajustar los cambios que Luzma comentó.</li>
            <li>se puede imprimir ademas en un solo formato el formato de la caratula anexa&nbsp; 
             el pagaré</li>
            <li>las firmas autorizadas ya aparecen en la solicitud</li>
            <li>Se acomodo el formato de reporte de situaciones</li>
            <li>las situaciones pueden ser modificadas.</li>
            <li>Se pueden bloquear los periodos de los movimientos de cuentas de banco.</li>
            <li>Al agregar un movimiento de banco si es cheque entonces permite decir si el cheque fue cobrado en otra fecha</li>
            <li>La validacion del tiraje de cheques ha sido añadida.</li>
            <li>Los datos de los tirajes son configurados desde la pagina de Cuentas de banco</li>
            <li>Pagina de liquidaciones agregada.</li>
            <li>Lista de boletas relacionadas a un movimiento de caja chica acomodadas para mostrar solo 10</li>
           </ul>
           
         
        </li>
       <li>2da Junta<ul>
           <li>se preselecciona el precio del producto en la nota de venta.</li>
           <li>Las acciones realizadas en las solicitudes son registradas</li>
           <li>La impresion de formatos es logeada en el sistema.</li>
           <li>La seleccion del menu es guardada durante la sesion.</li>
           <li>Se selecciona el precio 2 en la nota de venta si es a credito</li>
           <li>Se agrego tabla para los pagos de los creditos</li>
           <li>se agrego tabla para la conversion de productos.</li>
           <li>Se agregaron los filtros dentro de las cuentas de banco, se permite elegir 
               primero el banco, luego la empresa y luego la cuenta.</li>
           <li>al agregar pagos en la liqudiacion ya no se cambia la cuenta al agregar el pago.</li>
           <li>Todos los productos se muestran sin problemas en la lista de productos ya no mas 
               paginas.</li>
           <li>los creditos ya pueden ser modificados.</li>
           <li>Se agrego a la base de datos las tablas para el control de bloqueo de los 
               periodos en los bancos.</li>
           </ul>
        </li>
        <li>**** CAMBIOS HECHOS CON LOS COMENTARIOS HECHOS DESPUÉS DE LA 1ra JUNTA ******<ul>
            <li>Se implemento los seguros agricolas.</li>
            <li>Se relaciona un seguro agricola con una solicitud</li>
            <li>En la nota de venta, se ha quitado el folio y sólo se dejo el No. Nota que es autonumérico.</li>
            <li>En NV. Se cargan los datos de chofer y transportista automáticamente con los datos del productor pero permitiéndolos modificar después.</li>
            <li>Se permite modificar el status de una solicitud en la lista de solicitudes</li>
            <li>En la solicitud, la carátula anexa se imprime correctamente</li>
            <li>Se permite modificar el status de una solicitud en la lista de solicitudes</li>
            <li>Fecha límite de pago se muestra ahora en los créditos</li>
            <li>Al eliminar una nota de venta, los datos relacionados a esta se eliminan 
                correctamente</li>
            <li>Filtrar por producto las existencias en la página Existencias de productos.</li>
            <li>se acomodo el orden de los apellidos en el formato de caratula anexa.</li>
            
            </ul>
        </li>
        <li>28/03/2010<ul>
            <li>Actualizaciones en boletas</li>
            <li>Actualizaciones en Nota de Venta</li>
            <li>Actualizaciones en Nota de Compra</li>
            </ul>
        </li>
        <li>27/03/2010<ul>
            <li>Se agregó relacion de Boletas - Transportistas</li>
            </ul>
        </li>
        <li>24/03/2010<ul>
            <li>corregido problema con las facturas de venta</li>
            <li>boletas afectan correctamente la existencia</li>
            </ul>
        </li>
        <li>23/03/2010<ul>
            <li>Se esta cambiando las boletas de salida por nota de venta, para realizar la 
                salida de producto directamente al agregar la nota,</li>
            <li>Se esta cambiando la nota de venta para realizar el calculo de intereses sobre 
                el saldo promedio mensual como lo hace Banco.</li>
            <li>Revision y actualizacion de formatos.</li>
            </ul>
        </li>
        <li>22/03/2010<ul>
            <li>El permiso del directorio donde se guardan los documentos se configuraron 
                correctamente, ahora ya se guardan y se eliminan sin problemas desde el disco 
                duro.</li>
            <li>Se agrego funcionalidad para buscar el nombre del productor en la lista de 
                documentos.</li>
            <li>Modificando notas de venta para permitir el calculo de la utilidad en el 
                producto vendido.</li>
            <li>Acomodado problema al agregar una nueva situacion.</li>
            <li>Tablas extra en notas de venta fueron eliminadas, solo fue dejada una que tenia 
                toda la informacion de los pagos para mantener el mismo formato con la factura 
                de venta y la nota de compra</li>
            </ul>
        </li>
        <li>21/03/2010<ul>
            <li>Se modifico todo el modulo de guardado de documentos, ya que se provocaba un 
                desborde de la base de datos y bloqueaba el sistemna.</li>
            <li>se modifico la pagina para descargar los documentos y funciona ok.</li>
            <li>calculo de intereses modificado.</li>
            </ul>
        </li>
        <li>19/03/2010<ul>
            <li>Revision de todo el sistema de calculo de interes</li>
            <li>base de datos modificada en todas las notas de venta para permitir el nuevo 
                calculo de interes</li>
            <li>agregado soporte para calcular el saldo de todos los productores.</li>
            </ul>
        </li>
        <li>18/03/2010<ul>
            <li>Nota de venta, tiene una nueva tabla para mostrar los pagos, se eliminaran las 
                otras dos tablas por separado al hacer la ultima prueba</li>
            <li>se abre para agregar nuevas boletas en la nota de venta.</li>
            <li>corregido los formatos al agregar los pagos a tabla.</li>
            <li>formato para agregar nueva boleta a la nota de venta agregado.</li>
            <li>Se encontro un bug al agregar el detalle en la nota de venta, se esta revisando.</li>
            <li>nuevas vistas para ver la existencia y lo spagos de la nota de venta se 
                agregaron a la base de datos.</li>
            <li>Se hara revision de la base de datos en vivo por que se encontro un detalle de 
                incremento de medida desmedio en el dia de hoy.</li>
            <li>Modificada el ingreso de boletas rapida, para correccion al agregar a factura de 
                venta, y con la nota de venta.</li>
            <li>formula para calcular la existencia corregida para la factura de venta y para la 
                nota de venta.</li>
            </ul>
        </li>
        <li>17/03/2010<ul>
            <li>Eliminar nota de compra está implementada para borrar todas las relaciones con la nota de compra eliminada.</li>
            <li>Corregidos detalles en los movimientos de banco y movimientos de caja chica rápidos.</li>
            <li>hipervinculo para abrir nota de compra en lista agregado.</li>
            
            </ul>
        </li>
        <li>16/03/2010<ul>
            <li>Arreglado problema con bodega en la pagina principal.</li>
             <li>Se modificarons los mov de banco para el filtro de la tarjeta de crédito.</li>
            <li>funcionalidad de traspaso entre cajas verificada.</li>
            <li>Soporte para eliminar todos los datos de la nota de compra, detalle, pagos, 
                boletas añadido directamente sobre la base de datos con stored procedures.</li>
            <li>Soporte con stored procedure para eliminar boletas de nota de compra al eliminar 
                ésta.</li>
            <li>Nota de venta tiene soporte ajax para realizar las consultas mas rapido.</li>
            <li>reacomod de codigo ademas de tabular éste, en la nota de venta.</li>
            <li>formato en detalle de nota de venta agregado.</li>
            </ul>
        </li>
        <li>Acumulativo Code drop 8- Final Release<ul>
            <li>diseño de notas de compra acomodado.</li>
            <li>notas de venta aceptan agregar con folio</li>
            <li>Notas de compra permite al agregar el detalle relacionar o no con boletas para 
                entrada de producto</li>
            <li>Seleccionar nota de venta en agregar mov bancarios.</li>
            <li>Ya no se preselecciona un productor al agregar un movimiento de banco a una nota 
                de compra</li>
            <li>Pagos para notas de compra se muestran en una sola tabla en lugar de dos.</li>
            <li>Pruebas con notas de compra completadas al agregar y mostrar.</li>
            <li>Lista de compras verificada.</li>
            <li>Facturas de Venta verificadas</li>
            
            </ul>
        </li>
        <li>11/03/2010<ul>
            <li>Notas de venta.</li>
            </ul>
        </li>
        <li>10/03/2010<ul>
            <li>Nota de compra verificada</li>
            <li>entrada de existencia por nota de compra verificada.</li>
            <li>reparaciones menores de formato en lista de boletas.</li>
            <li>Pagos verificados en la nota de compra.</li>
            <li></li>
            </ul>
        </li>
        <li>09/03/2010<ul>
             <li>Se cambió el formato en la listas de pagos a las notas de compra y venta para que mostratan la fecha y montos correctamente</li>
             <li>Se modificó el formulario para agregar una boleta para que permita relacionarla a notas de compra y factura de venta </li>
             <li>Se quitó la columna de ID en la lista de pagos de las notas de compra y de venta</li>
             <li>Se cambio el formato del diseño de la nota de compra</li>
             <li>&nbsp;Se cambio las tablas de pagos para la nota de compra</li>
             <li>los detalles afectan directamente a la existencia asi como la actualizan al 
                 eliminar.</li>
            
             </ul>
        </li>
         <li>08/03/2010<ul>
             <li>Los ciclos se reordenaron en la liquidacion.</li>
             <li>Actualizaciones para ingresar la nota de compra,</li>
             <li>se actualiza los datos de la nota de compra para modificar existencia de detalle</li>
             <li>el detalle de la nota de compra afecta la existencia</li>
             <li>las boletas se relacionan directamente con la nota de compra.</li>
             <li>el detalle permite eliminarse.</li>
             <li>pagos de caja chica se relacionan a la nota de compra</li>
             <li>pagos de bancos se relacionan a la nota de compra.</li>
             </ul>
        </li>
         <li>07/03/2010<ul>
             <li>resuelto problema con el formato html en el correo al agregar situacion.</li>
             </ul>
        </li>
         <li>06/03/2010<ul>
             <li>resuelto problema al abrir los movimientos de caja chica</li>
             <li>al hacer cambio de bodega en nota de venta se recalculan las existencias.</li>
             <li>Formato de tablas de pagos en notas de venta acomodado.</li>
             <li>botones de agregar pagos en notas de venta no causan validacion.</li>
             <li>los pagos de las notas de venta se agregan correctamente</li>
             <li>Corregido problema al reportar situaciones.</li>
             </ul>
        </li>
         <li>04/03/2010<ul>
             <li>Agregado extender para busqueda de nombre de productor en la nota de venta
             </li>
             <li>FIX: arreglado problema al cargar nota de venta que no cargaba al productor de 
                 la nota</li>
             <li>&nbsp;La lista de notas de venta se muestra correctamente y permite abrir notas 
                 de venta. </li>
             <li>FIX: arreglado problema que no mostraba las facturas de venta</li>
             <li>se calculan los pagos de la nota de venta correctamente dentro de ésta.</li>
             <li>Se formatearon las columnas de la tabla dentro de nota de venta los botones para 
                 agregar un nuevo pago de nota de venta por caja chica o banco funcionan 
                 correctamente</li>
             </ul>
        </li>
         <li>03/03/2010<ul>
             <li>Reporte de certificados por empres y por bodega.</li>
             <li>JS agregado a la nota de compra para permitir cargar los datos de los 
                 productores en las notas de venta</li>
             <li>Validacion por JS en la nota de venta para evitar inventarios negativos.</li>
             </ul>
        </li>
         <li>02/03/2010<ul>
             <li>Las situaciones se agregan correctamente</li>
             <li>se puede agregar historial a las situaciones.</li>
             <li>la lista de situaciones muestra correctamente los datos</li>
             <li>Los pagos de movimientos de banco son relacionados correctamente a las notas de compra</li>
             <li>Los pagos de movimientos de caja chica son relacionados correctamente a las 
                 notas de compra.</li>
             </ul>
        </li>
         <li>28/02/2010<ul>
             <li>Corregido el agregar movimiento a caja chica para las notas de venta.</li>
              <li>Corregida la lista de situaciones.</li>
             
             </ul>
        </li>
         <li>27/02/2010<ul>
             <li>Agregado soporte para agregar situaciones.</li>
             <li>Se agregan los movimientos de caja chica a las notas de venta.</li>
             </ul>
        </li>
         <li>26/02/2010<ul>
             <li>actualizada la manera de calcular los saldos para la TDC</li>
             <li>Al agregar la boleta de salida de manera rapida permite los datos nuevos</li>
             <li>se permite agregar, modificar y cancelar solicitudes.</li>
             <li>Se permite relacionar las solicitudes con los creditos.</li>
             <li>Impresion de formatos.</li>
             <li></li>
             </ul>
        </li>
         <li>25/02/2010<ul>
             <li>Se pueden agregar nuevas notas de compra</li>
             <li>las notas de venta permite relacion con productor.</li>
             <li>Se permiten agregar el detalle de la nota de venta.</li>
             <li>diseño de las notas de compra basico faltante de listo de movimientos de bancos 
                 y caja</li>
             <li>notas de compra permiten la relacion con proveedores</li>
             <li>notas de compra permiten relacion con detalle de nota de compra</li>
             <li>se logean las acciones al imprimir los formatos.</li>
             <li>Se registran las acciones realizadas en las facturas de venta.</li>
             <li>Se pueden agregar y modificar solicitudes</li>
             <li>Se pueden cambiar de estado las solicitudes (CANCELADA, APROBADA, NUEVA)</li>
             <li>Se puede imprimir la solicitud</li>
             <li>Se puede imprimir el contrato de la solicitud</li>
             <li>Se puede imprimir la carátula anexa</li>
             
             </ul>
        </li>
         <li>24/02/2010<ul>
             <li>Hipervinculo para abrir pagina de editar productores</li>
             <li>Las solicitudes se pueden imprimir</li>
             
             </ul>
        </li>
         <li>23/02/2010<ul>
             <li>Se pueden agregar notas de venta</li>
             <li>Se agrega el chofer y las placas a las boletas</li>
             <li>Las facturas de venta permiten modificar las boletas de salida relacionadas a 
                 ésta</li>
             <li>se cargan los datos de la boleta de salida para permitir modificar.</li>
             <li>Creditos tienen status</li>
             <li>Solicitudes tienen status</li>
             <li>las boletas modifican el chofer y las placas.</li>
             <li>Los totales de la lista de factura se cargan correctamente</li>
             </ul>
        </li>
         <li>22/02/2010<ul>
             <li>Los movimientos de banco se pueden agregar a los certificados de los creditos 
                 financieros relacionando desde la pagina de agregar movimiento de banco rapido.</li>
             <li>las solicitudes se agregan correctamente</li>
             <li>corregido problema de impresion en facturas de venta.</li>
             <li>los movimientos de banco se relacionan a facturas.</li>
             <li>se muestran los movimientos de banco a facturas</li>
             <li>las boletas de salida son agregadads a las facturas de venta.</li>
             </ul>
        </li>
         <li>21/02/2010<ul>
             <li>Las boletas de salida se muestran correctamente en la lista de boletas en la 
                 factura de venta.</li>
             </ul>
        </li>
         <li>20/02/2010<ul>
             <li>Los creditos se agregan, modifican correctamente y se actualiza su estado.</li>
             <li>base de datos actualizada para creditos</li>
             <li>base de datos actualizada para predios</li>
             <li>Las facturas de venta se imprimen correctamente.</li>
             </ul>
        </li>
         <li>19/02/2010<ul>
             <li>Corregido problema al calcular los saldos mensuales por periodo, ahora cuando se 
                 calcula por periodo y no es periodo de mes se calcula por mes como se ha venido 
                 haciendo.</li>
             <li>Las facturas de venta son eliminadas sin problemas.</li>
             <li>Se pueden abrir los predios para modificar</li>
             <li>movimientos de banco pueden agregarse a las facturas desde la pagina de agregar 
                 movimiento de banco rapido.</li>
             </ul>
        </li>
         <li>18/02/2010<ul>
             <li>Los saldos para las tarjetas de credito se calculan por periodo</li>
             <li>Soporte basico de agregar y relacionar pagos a una factura de venta agregado.</li>
             <li>los saldos de la tarjeta de credito, son calculados por periodo en el caso de 
                 que en el movimiento de banco se seleccione un periodo en lugar de un mes.</li>
             </ul>
        </li>
         <li>17/02/2010<ul>
             <li>Corregido problema al registrar informacion al agregar un predio</li>
             <li>Primera version de la pagina para agregar una boleta ha sido agregada. falta 
                 agregar el soporte para agregar boletas de salida.</li>
             <li>liberada version 1.1 libre de bugs encontrados hasta el momento.</li>
             </ul>
        </li>
         <li>16/02/2010<ul>
             <li>los predios pueden ser eliminados sin problema</li>
             <li>las boletas pueden ser relacionadas con un cliente de venta. -&gt; se estan 
                 acomodando los campos que se usaran para las boletas de salida.</li>
             <li>Las configuraciones de los cheques son por cuenta ahora, ademas se agrego el 
                 formulario de configuracion.</li>
             </ul>
        </li>
         <li>14/02/2010<ul>
             <li>Resuelto problema al ingresar un movimiento de banco al colocar un numero de 
                 cheque, ya que solo permitía abonos.</li>
             <li>se registran mas datos al ingresar un predio en el sistema.</li>
             <li>se quito la funcion que registraba informacion al actualizar el saldo mensual de 
                 la caja chica.</li>
             </ul>
        </li>
         <li>13/02/2010<ul>
             <li>Los predios pueden ser modificados</li>
             <li>La lista de predios permite abrir un predio para modificarlo.</li>
             <li>agregar movimiento de banco popup permite relacionar el movimiento de banco a un 
                 credito financiero.</li>
             </ul>
        </li>
         <li>11/02/2010<ul>
             <li>los certificados tienen numero de cabezas&nbsp; y bodega.</li>
             <li>se muestra el numero de cabezas y bodegas en la lista de certificados</li>
             <li>El reporte de certificados se agrupa por empresa y por bodega</li>
             <li>se selecciona el concepto cheque cuando el numero de cheque es un numero valido 
                 y mayor que cero.</li>
             <li>las facturas de venta ya se pueden agregar y abrir de la lista.</li>
             </ul>
        </li>
         <li>10/02/2010<ul>
             <li>Corregido problema que no dejaba editar numero de cheque en los movimientos de 
                 banco.</li>
             <li>el propietario es opcional ahora para los predios.</li>
             </ul>
        </li>
         <li>09/02/2010<ul>
             <li>se agregó la pagina para agregar certificado financiero de pagina emergente.</li>
             <li>Los mensajes de actualizacion de saldos han sido removidos de las cuentas de 
                 banco.</li>
             <li>Se han acomodado las columnas en la lista de liquidaciones</li>
             <li>Se ha acomodado las columnas en la lista de predios</li>
             <li>Lista de certificados ya no tira una excepcion.</li>
             <li>Pagina de agregar certificado funciona correctamente para ligar a un credito o a 
                 un cliente de venta.</li>
             <li>Al agregar un certificado desde la pagina de un credito se liga directamente con 
                 el credito.</li>
             <li>Correcciones en la pagina para agregar factura de venta.</li>
             </ul>
        </li>
         <li>07/02/2010<ul>
             <li>Eliminada seleccion de credito financiero ya que no era necesaria,</li>
             <li>los creditos financieros pueden ser modificados, resuelto problema al modificar.</li>
             <li>los clientes de venta pueden ser modificados.</li>
             <li>filtros en la pagina de log messages arreglados.</li>
             <li>Se muestran correctamente los pagos en el reporte de liquidaciones con notas.</li>
             <li>Se registra cuando un usuario realiza cambios en certificados.</li>
             <li>Ahora se muestra el # de certificado en la lista de certificados</li>
             <li>Se muestran los totales en la lista de certificados</li>
             <li>Se muestran los totales en los movimientos de banco relacionados a un credito 
                 financiero.</li>
             <li>Se corrigio Bug en el reporte de certificados.</li>
             <li>Ya no se agregan mensajes de registro cuando se actualizan los saldos.</li>
             <li>Corregido problema al agregar una factura de clientes de venta</li>
             </ul>
        </li>
         <li>04/02/2010<ul>
           <li>Se agregó el menú de almacenadora.</li>
           <li>Se agregó la página de lista de certificados</li>
           <li>Se permite agregar o modificar un certificado</li>
           <li>Se guarda la relación de un certificado con un cliente de venta o con crédito financiero</li>
             <li>Se modifico tabla en Base de datos para agregar nuevos campos de predio</li>
             <li>Se agregan predios con los nuevos campos</li>
             <li>Se cambio la lista de predios para tener los nuevos campos.</li>
           
           
           </ul>
        </li>
         <li>30/01/2010<ul>
           <li>Se puede agregar un movimiento de banco rápido desde la página Creditos Financieros.</li>
           <li>Se guarda un mensaje cuando se agregar,modifican y/o eliminan: Catálogos de cuenta, subcatálogos de cuentas y créditos financieros</li>
           <li>Se pueden agregar, modificar y/o eliminar clientes de venta sin problemas.</li>
           <li>Se pueden agregar, modificar y/o eliminar los certificados de los créditos financieros sin problemas.</li>
           </ul>
        </li>
        <li>29/01/2010<ul>
           <li>Se agregó soporte para evitar doble click sobre creditos financieros.</li>
           <li>Al agregar un movimiento de banco los campos y las listas se reinicializan para 
               evitar que al seleccionar cargo se agregue como abono.</li>
           <li>Los descuentos se recalculan cuando se agrega o modifica una boleta.</li>
            <li>Se Cambio TODO el diseño del sitio de Comercializadora las margaritas a IPROJAL</li>
            <li>Se quito toda la funcionalidad para tener impurezas, secado y humedad general 
                por liquidacion. del sitio web y de la base de datos</li>
            <li>Se actualizo funcion de calculo de saldos de cheques</li>
           </ul>
        </li>
        <li>27/01/2010<ul>
           <li>Los creditos financieros ahora tienen la empresa que los emite</li>
           <li>se puede agregar colonia y cp en cliente de venta.</li>
           <li>datos de cliente de venta se cargan dinámicamente de forma correcta</li>
           <li>Se creó el reporte de certificados agrupado por Producto y Empresa.</li>
           <li>Se creó el reporte general de certificados.</li>
           <li>Se quitaron funciones para el cálculo de tiempos en liquidaciones.</li>
           <li>Se creó el reporte de certificados agrupado por Producto y Empresa.</li>
           </ul>
        </li>
        <li>26/01/2010<ul>
            <li>Corregido problema por el que al agregar más de un movimiento de banco no se volvía a mostrar la capa de asignar a anticipo.</li>
            <li>Se cambió el orden en que se muestran los créditos financieros por fecha de 
                vencimiento.</li>
            <li>Se agregó soporte para evitar que se den doble clics en la página de créditos 
                financieros.</li>
            <li>Se agregó el producto en la lista de certificados asignados a un crédito 
                financiero.</li>
           
            </ul>
        </li>
        <li>25/01/2010<ul>
            <li>Los certificados ahora soportan la relacion con un producto.</li>
            <li>Corregido problema que no dejaba modificar productores que fueron agregados en 
                la pagina &quot;rapida&quot;.</li>
            <li>reporte desglosado de liquidaciones por productor ahora agrupa las boletas por 
                precio tambien</li>
            <li>reporte desglosado de liquidaciones por productor ahora se muestra el nombre de 
                la cuenta para los pagos del banco.</li>
            </ul>
        </li>
        <li>23/01/2010<ul>
            <li>nuevo reporte por catalogo de cuentas agregado,</li>
            <li>creditos financieros soporta garantia hipotecaria</li>
            <li>se actualizaron los campos de certificados de creditos financieros</li>
            <li>los creditos financieros se pueden modificar.</li>
            <li>se muestran los productos de las boletas comprados en el reporte desglosado por 
                liquidacion por productor.</li>
            </ul>
        </li>
        <li>21/01/2010<ul>
            <li>Se agregó funcionalidad para créditos financieros.&nbsp;</li>
            <li>Corregido problema al mostrar los productores que habían sido agregados como "rápidos".</li>
            
            </ul>
        </li>
        <li>20/01/2010<ul>
            <li>Se agregó funcionalidad basica para el usuario Observador.&nbsp;</li>
            <li>se agregó funcionalidad para la TDC el filtrado por periodos.</li>
            <li>Reporte de liquidaciones desglosado agregado.</li>
            </ul>
        </li>
        <li>.<ul>
            <li>se preselecciona &quot;abono&quot; al abrir los movimientos de banco.</li>
            <li>pagina de saldos mensuales de bancos utiliza el nuevo metodo para calcular el 
                saldo.</li>
            <li>correccion al actualizar el producto de boleta en la liquidiacion.</li>
            </ul>
        </li>
        <li>10/12/2010<ul>
          <li>Se agregaron nuevas validaciones para el modo observador</li>
          <li>Se corrigió la impresión de los cheques en el campo nombre.</li>
          <li></li>
          <li>Corregido problema para imprimir un cheque con el nuevo campo en los 
              movimientos.</li>
          <li>Se Cambio las funciones para el calculo de los saldos mensuales.</li>
          </ul>
        </li>
      <li>10/12/2010<ul>
          <li>SE AGREGO MODO OBSERVADOR en TODAS las paginas</li>
          <li>Se agrego soporte para mover las fechas de los cheques hasta cuando son cobrados 
              y afecten el saldo del mes.</li>
          <li>Se agrego el desglose de las liquidaciones y boletas ingresadas.</li>
          <li>Corregido problema para imprimir un cheque con el nuevo campo en los 
              movimientos.</li>
          <li>Se Cambio las funciones para el calculo de los saldos mensuales.</li>
          </ul>
        </li>
        <li>09/12/2010<ul>
          <li>Se agrego desglose de liquidaciones y boletas agregadas.</li>
          <li>Link para nueva liquidacion en la lista de liquidaciones</li>
          </ul>
        </li>
        <li>08/01/2010<ul>
          <li>Se actualizan las listas de todos los productores en la página de Agregar Liquidación después que se agrega un productor rápido</li>
          <li>Se creo reporte para mostrar las liquidaciones cobradas que ya tienen notas </li>
          <li>Se creo reporte en excel de los movimientos de bancos por mes de una cuenta</li>  
      
         
    
          </ul>
        </li>
     <li>07/01/2010<ul>
          <li>Se registra cuando se modifica un movimiento de banco</li>
          <li>Se registra cuando se visita un productor rápido </li>
          <li>Se registra cuando se agrega un productor desde "Agregar productor rápido"</li>
          <li>Se registra correctamente cuando se visita la página Agregar/Modificar liquidación</li>
          <li>Se muestra con formato de moneda los campos "total, interes y seguro" en la página de Ver Liquidación</li>
          <li>Se muestra formato de moneda en los campos de cargo y abono en Conciliación de movimientos de banco</li>
          <li>Se registra cuando un usuario abre la lista de liquidaciones</li>
          <li>Se reescribió el cálculo de saldos mensuales de los saldos</li>
          <li>Se reescribió función para cálculo de saldo inicial y final de los movimientos de banco</li>
      
         
    
          </ul>
        </li>
     <li>06/01/2010<ul>
          <li>Se agregó página para la conciliación de movimientos de banco</li>
          <li>Se agregó funcionalidad para imprimir un movimiento de banco recién agregado cuando sea un cheque</li>
          <li>Corregido el problema en la impresión del cheque</li>
          <li>Cambios en la lista de movimientos de banco para marcar como cobrados desde la lista o dirigirte a la página de conciliación</li>
          <li>Corregido pequeño problema en el cálculo de saldos mensuales de movimientos de banco</li>
          <li>Se agregó un contador de versiones automático</li>
    
          </ul>
        </li>
      <li>05/01/2010<ul>
          <li>Se corrigio pequeño problema visual en pagina de agregar productor rapido</li>
          <li>se creo DATASET para manejar los datos de las facturas de clientes.</li>
          <li>Se agrego pagina para agrgar un cliente de facturas &quot;rapido&quot; sin necesidad de 
              cambiar o salir de la pagina de la factura.</li>
          <li>Se modificó funcion JS para cuando cambie un cliente de factura se actualice 
              automaticamente pero primero borrando los datos del productor anterior</li>
          <li>Se cambio botones para agregar un productor rapido abajo de la lista de 
              productores en la liquidiacion.</li>
          <li>Se agrego soporte para agregar una nueva factura de cliente</li>
          <li>Se agrego soporte para cargar una factura guardada.</li>
          <li>añadido soporte para numero de builds.</li>
          </ul>
        </li>
        <li>04/01/2010<ul>
          <li>Corregido problema al cambiar la bodega al editar una boleta en la lista de 
              boletas de la liquidacion.</li>
          <li>Se modificó el reporte general de liquidaciones para obtener los datos de cada 
              liquidacion.</li>
          <li>Se acomodó alineacion en las columnas de la tabla del reporte general de 
              liquidaciones.</li>
          <li>Se modificó el reporte por productos de boletas.</li>
          <li>Se agrego la lista de facturas de clientes.</li>
          <li>Se agrego la opcion de lista de facturas de clientes en el menu</li>
          <li>se modifico la factura de clientes para permitir acceso rapido a los datos de 
              las boletas.</li>
          <li>Se agregaron los filtros en la lista de facturas de clientes.</li>
          <li>SE AGREGO NUEVA PAGINA PARA AGREGAR UN PRODUCTOR CON LOS DATOS MINIMOS DESDE LA 
              LIQUIDACION.</li>
          </ul>
        </li>
        <li>03/01/2010<ul>
          <li>Se pueden agregar los clientes para facturas de venta.</li>
          <li>Se muestran las ultimas acciones realizadas por los usuarios en la pagina de 
              inicio</li>
          <li>Se agregaron boletas:<ul>
              <li>boletas disponibles para agregar: total 1451<ul>
                  <li>se agregaron: 605 Boletas</li>
                  <li>mazorca blanco 230 </li>
                  <li>granel blanco 344 </li>
                  <li>granel amarillo 10 </li>
                  <li>mazorca amarillo 5 </li>
                  <li>rastrojo 6 </li>
                  <li>silo de caña 10 </li>
                  </ul>
              </li>
              <li>846 Boletas no pudieron ser agregadas debido a problemas con codigo de 
                  productores o las boletas ya estaban en el sistema.</li>
              </ul>
          </li>
          <li>Se modificaron 573 productores donde se actualizo su codigo de boletas del 
              archivo.</li>
          </ul>
        </li>
        <li>02/01/2010<ul>
          <li>Se obtiene la cantidad de liquidaciones agregadas hoy </li>
          <li>se obtiene la cantidad de boletas agregadas hoy</li>
          </ul>
        </li>
                                <li>31/12/2009<ul>
          <li>Corregido problema con reporte de liquidaciones que obtenia mal la consulta</li>
          <li>Corregido problema de suma de pagos en boletas</li>
          <li>Se agrego reporte de liqudiaciones</li>
          <li>se corrigio problema menor al calcular los pagos de las liquidiaciones.</li>
          <li>Se creo una nueva tabla para mostrar los pagos.</li>
          </ul>
        </li>
        <li>30/12/2009<ul>
          <li>Se corrigio los mensajes de log cuando se agregaban boletas que registraban en 
              salida de producto,&nbsp; cuando era en entrada. (Trigger corregido)</li>
          <li>Se registra en el historial cuando un usuario imprime un cheque.</li>
          <li>Se registra en el historial cuando se abre una liquidacion.</li>
          <li>EL TIPO DE LETRA UTILIZADA EN LOS CHEQUES SE CONFIGURA EN LA PAGINA DE 
              CONFIGURACION GLOBAL</li>
          <li>SE AGREGO LA PAGINA DE CONFIGURACION GLOBAL.</li>
          <li>EL TIPO DE LETRA EN LOS CHEQUES ES LA MISMA PARA TODOS LOS CONCEPTOS.</li>
          <li>Se quito el signo de pesos de la cantidad del cheque</li>
          <li>se agrego funcion JS para multiplicar en factura de cliente.</li>
          <li>se agregan y modifican los productos de una factura de cliente.</li>
          <li>Se agrego funcionalidad para ordenar los movimientos de banco dinamicamente.</li>
          <li>Se agrego la funcionalidad para guardar parametros y valores de configuracion en 
              la base de datos.</li>
          <li>corrigiendo codigos de productores agregados sin su codigo.</li>
          </ul>
        </li>
        <li>29/12/2009<ul>
          <li>El orden de los movimientos de banco ahora es dinamico, no es necesario publicar 
              para cambiar el orden</li>
          <li>Se agregó compresión en todo el sitio para acelerar la carga de las paginas</li>
          <li>Se probó la compresión del sitio para verificar funcionamiento despues de 
              aplicar la compresion</li>
          <li>Se agregaron validaciones para evitar comprimir archivos comprimidos.</li>
          <li>La variable VIEWSTATE ya se carga en sesion para evitar el payload excesivo en 
              las paginas.</li>
          <li>Se excluyo la pagina de queryexistencias ya que no era necesaria la compresion</li>
          <li>Se agregó contador de cabezas de ganado</li>
          <li>Las acciones en las paginas de cheques y liquidaciones son registradas 
              correctamente.</li>
          <li>Se agrego funciones para evitar doble click en pagina de movimientos de banco.</li>
          <li>Se agregó soporte AJAX</li>
          <li>Los datos del cliente se cargan utilizando funciones ajax (response content &lt; 
              256 bytes!!)</li>
          <li>Se creo pagina para consultar datos de cliente</li>
          <li>Los datos de la existencia se consultan utilizando ajax (response content &lt; 10 
              bytes!! 8-O )</li>
          <li>se creo pagina para consultar existencia.</li>
          <li>Se agrego archivo Javascript dentro de agregar productor para mostrar 
              &quot;procesando&quot;</li>
          </ul>
        </li>
        <li>27/12/2009<ul>
          <li>pagina de movimientos de bancos optimizada.</li>
          <li>varias paginas ahora se cachean para acelerar la carga mientras se resuelve lo 
              de prodigy.</li>
          <li>se reviso la factura para venta de maiz, en la proxima publicacion se empezara a 
              atrabajar con ella.</li>
          <li>tambien se cargaron todas las boletas restantes de dias pasados.</li>
          </ul>
        </li>
        <li>26/12/2009<ul>
          <li>Actualizacion nocturna, se optimizo todo el codigo que se pudo para la 
              liquidacion, se redujo el tiempo de carga de la pagina en casi el 70%.</li>
          </ul>
        </li>
        <li>26/12/2009<ul>
           <li>Se creó la página para imprimir un cheque directamente sin necesidad de hacer liquidación o movimiento. </li>    
           </ul>
        </li>
        <li>24/12/2009 (feliz navidad)<ul>
            <li>el orden de las columnas de los pagos en la liquidiacion se cambio. </li>
            <li>se volvio a cambiar el tamaño de la fecha en el cheque</li>
            <li>se volvio a llamar a telmex por el problema del internet.</li>
            </ul>
         </li>
        <li>22/12/2009 y todos los dias anteriores que por publicar rapido no se pudo 
            actualizar<ul>
                <li>se preselecciona cuenta de iprojal de bancomer en liquidaciones como default.</li>
                <li>se agrego el grupo SERVICIOS CONTABLES</li>
                <li>se agrego el catalogo HORARIOS MENSUALES dentro de SERVICIOS CONTABLES</li>
                <li>se agrego el catalogo DICTAMENES dentro de SERVICIOS CONTABLES</li>
                <li>corregido problema con el nombre de los cheques,</li>
                <li>corregido problema que mostraba 0/100 en lugar de 00/100</li>
                <li>Se agrego otra validacion para evitar productores duplicados.</li>
                <li>Se agregó validacion en pagina de inicio para evitar entradas duplicadas.</li>
                <li>La bodega en las liquidaciones se muestra.</li>
                <li>Se agrego funcion para evitar doble click</li>
                <li>Se agrego la funcion JS generica para deshabilitar botones</li>
                <li>se deshabilita los botones en la liquidacion para evitar doble click.</li>
                <li>Se muestra &quot;imprimir&quot; para cada pago despues de que la liquidacion es realizada.</li>
            </ul>
         </li>
        <li>16/12/2009<ul>
            <li>La fecha en el cheque se muestra en formato largo</li>
            </ul>
         </li>
        <li>Es permitido ingresar cheques con monto cero cuando el catalogo es &quot;cheque 
            cancelado&quot;</li>
        <li>Se preselecciona el tipo de catalogo dentro de la liquidacion cuando ésta tiene 
            algun producto en el catalogo.</li>
        <li>Modificado el metodo que permitia regresar a la pagina que queria ser visitada, 
            despues de reentrar al sistema.</li>
        <li>las facturas de compra se deshabilitaron por un fallo en una comprobacion.</li>
        <li>12/12/2009<ul>
            <li>Se pueden ingresar los Clientes para venta de maiz.</li>
            <li>Se puede ver la informacion del movimiento de banco origen</li>
            <li>Al agregar un anticipo, éste se agrega a la ultima liquidacion abierta.</li>
            </ul>
         </li>
        <li>10/12/2009<ul>
            <li>En pagos de liquidaciones siempre se muestran los catalogos internos.</li>
            <li>No se puede agregar un pago si la liquidacion ya esta pagada.</li>
            <li>Si un movimiento de banco ya esta verificado ya no se puede modificar.</li>
            <li>Se cambio el diseño de la liquidacion.</li>
            </ul>
         </li>
        <li>09/12/2009 publicacion con actualizaciones acumulativas<ul>
            <li>la humedad, impurezas y secado ahora pueden ser calculados independiente por 
                boleta en la liquidacion. </li>
            <li>las notas, intereses y seguro pueden ser pagados en la liquidacion. </li>
            <li>se reparó problema que hacia que los pagos no se calcularan correctamente al 
                imprimir liquidacion. </li>
            <li>se agregó soporte para que los cheques sean impresos en la liquidacion. </li>
            <li>se agregó el primer borrador de la factura de IPROJAL </li>
            <li>se desahabilita el panel para nuevo pago en la liquidacion cuando ésta ya esta 
                pagada. </li>
            <li>cuando un movimiento de banco esta como cobrado no se puede modificar el monto.
            </li>
            <li>se ha cambiado el modo en que las excepciones se registraban </li>
            <li>se hicieron cambios en el calendario. </li>
            <li>se agregó el registro del nombre de usuario al regristrarse en la pagina.
            </li>
            <li>se cambio el diseño de la liquidacion ahora las columnas tienen ancho 
                independiente. </li>
            <li>se muestra la fecha para los anticipos en la liquidacion al imprimir </li>
            <li>se muestra la fecha para los pagos en la liquidacion al imprimir</li>
            </ul>
         </li>
        <li>05/12/2009<ul>
            <li>Agregado nuevo informador de progreso cuando se agrega un nuevo pago en la 
                liquidacion.</li>
            <li>Informador de trabajo agregado al validar liquidacion.</li>
            <li>Agregado vinculo para ir a lista de liquidaciones desde la pagina de aregar 
                nueva liquidacion.</li>
            </ul>
         </li>
        <li>03/12/2009<ul>
            <li>Alineacion en reporte de columnas de boletas corregido.</li>
            <li>Se agregó funciones para que al agregar un movimiento se seleccione 
                dinamicamente si es un Abono/Cargo</li>
            </ul>
         </li>
        <li>03/12/2009<ul>
            <li>los datos del producto son actualizados junto con la liquidacion.</li>
            <li>Cuando se agrega un anticipo se guarda un registro de ello.</li>
            <li>al agregar un nuevo anticipo se agrega a la ultima liquidacion abierta.</li>
            <li>se muestra correctamente los datos del movimiento de origen en la caja chica, 
                esto es que tambien incluye el numero de cheque.</li>
            <li>Se ha corregido el orden de los movimientos de banco.</li>
            </ul>
         </li>
        <li>02/12/2009<ul>
            <li>Para agregar un productor la validacion es forzosa.</li>
            </ul>
         </li>
        <li>01/12/2009<ul>
            <li>Se muestran mas datos de pagos y anticipos en reporte por boletas.</li>
            <li>Corregido problema para cargar la pagina de cheques faltantes.</li>
            <li>Se ordenan correctamente las listas en los anticipos.</li>
            <li>Proble que se mostraba el ID del producto al editar una boleta en las 
                liquidaciones corregido.</li>
            <li>Corregido problema que no mostraba las boletas asignada de anticipos en los 
                movimientos de caja chica.</li>
            <li>Cuando se agrega un anticipo que contiene boletas, los datos de la boleta son 
                limpiados cada vez que se agrega una boleta.</li>
            <li>Corregido problema que no dejaba modificar productos</li>
            <li>Acomodada alineacion de columnas en el reporte de boletas por producto.</li>
            <li>Corregido problemas al agregar boletas al sistema por medio de un archivo de 
                excel.</li>
            </ul>
         </li>
        <li>30/11/2009 Actualización nocturna<ul>
            <li>Se cambio el orden de cómo se muestran los movimientos de caja chica.</li>
            <li>Se cambió el orden de la lista de productores en la página para asignar
                anticipos a liquidación (por orden alfabético). </li>
            <li>Se eliminó columna de productoID en la lista de boletas en las liquidaciones.</li>
            <li>Se muestran correctamente las boletas relacionadas con un anticipo en 
                la lista de movimientos de caja chica.</li>
            <li>Al agregar un movimiento de banco se borra correctamente los datos del boleta
                agregada al anticipo y se esconde la sección para agregar el anticipo.</li>
            </ul>
         </li>

        <li>27/11/2009<ul>
            <li>Resuelto problema que no dejaba actualizar movimientos de cuenta.</li>
            <li>Validacion extra añadida al agregar nuevo productor, se muestran los productores 
                que tienen nombres parecidos. </li>
            <li>Se valida que no haya otro productor con el mismo nombre.</li>
            <li>Se eliminan los espacios en los apellidos y nombres de los productores al 
                agregarlos.</li>
            <li>Problema al crear una nueva liquidacion que no funcionaba al agregar boletas 
                corregido.</li>
            <li>Se edita correctamente los movimientos de caja chica.</li>
            </ul>
         </li>
         <li>27/11/2009 -&gt; 28/11/2009 Actualizacion nocturna<ul>

            <li>Ahora las boletas se pueden modificar desde la lista.</li>
            
            </ul>
         <li>26/11/2009 -&gt; 27/11/2009 Actualizacion nocturna<ul>
            <li>Corregido problema que no dejaba modificar productores.</li>
            <li>las liquidaciones pueden se eliminadas sin problemas.</li>
            <li>los movimientos de banco son ordenados correctamente</li>
            <li>al eliminar un movimiento de caja chica el destino se elimina tambien.</li>
            <li>&gt;&gt;al generarse un problema se guarda mas informacion del error</li>
            <li>Pagina de catalogos de banco corregida</li>
            <li>Pagina para manejo de subcatalogos de bancos añadida.</li>
            <li>En la lista de boletas, si la boleta esta relacionada a una liquidacion se 
                muestra &quot;abrir&quot; para abrir la liquidacion</li>
            <li>El producto puede ser modificado&nbsp; de la boleta en la lista de boletas de la 
                liquidacion</li>
            <li>Se muestra el movimiento origen en los movimientos de banco.</li>
            <li>Se muestra el origen de los movimientos de caja chica.</li>
            </ul>
         </li>
        <li>25/11/2009<ul>
            <li>las liquidaciones aplican humedad y secado por default.</li>
            <li>solucionado problema que no dejaba cargar productores</li>
            <li>se han detectado problemas menores al cargar las boletas que ya han sido 
                reparados</li>
            </ul>
         </li>
        <li>24/11/2009 (actualizacion nocturna)<ul>
            <li>se pueden agregar movimientos entre cajas</li>
            <li>al eliminar un movimiento se elimina en cascada cuando el movimiento de caja 
                chica es borrado</li>
            <li>No se permite duplicar un numero de cheque.</li>
            </ul>
         </li>
        <li>23/11/2009 (ACTUALIZACION NOCTURNA)<ul>
             <li>Se muestra la sumatoria de columnas en la lista de boletas de la liquidacion</li>
             <li>Corregido el problema que no mostraba la liquidacion realizada.</li>
             <li>Corregido problema que no validaba correctamente el monto en la caja chica.</li>
             <li>Los movimientos de caja chica tienen la opcion de "cobrado"</li>
             <li>Todos los movimientos de cuentas de banco ahorita tienen la opcion para mostrarse como cobrados</li>
             <li>En el reporte de movimientos de bancos por catalogos, se agrupan los catalogos</li>
             <li>Los saldos negativos en caja chica y bancos se muestran en rojo</li>
             <li>los numeros de cuenta de banco soportan ahora hasta 20 caracteres.</li>
             <li>Se permite agregar CLABE a las cuentas de banco en la pagina de cuentas de 
                 banco.</li>
             <li>los movimientos de caja chica ahora son modificables</li>
             </ul>
         </li>
        
        
         <li>23/11/2009<ul>
             <li>Se registra cuando un usuario deshace la liquidacion</li>
             <li>Se registra cuando un usuario realiza la liquidiacion</li>
             <li>validacion para evitar agregar movimientos de banco en cero o invalidos 
                 agregado.</li>
             <li>se muestra la informacion del catalogo de caja chica.</li>
             <li>Arreglado problema que no se cargaban los pagos correctamente en el formato de 
                 impresion de la liquidacion</li>
             </ul>
         </li>
         <li>22/11/2009<ul>
             <li>al actualizar un movimiento se actualiza el movimiento &quot;destino&quot; 
                 (Impact: medium -&gt; big)</li>
             <li>Se a cambiado el orden y agrupacion en la lista de liquidaciones, se agrupan por nombre y se muestra una sola vez la liquidacion</li>
             </ul>
         </li>
         <li>21/11/2009<ul>
             <li>Se puede &quot;deshacer&quot; la liquidacion</li>
             <li>Se registra cuando un usuario imprime una liquidación.</li>
             <li>Se muestran la informacion del catalogo en la caja chica</li>
             <li>Se valida el nombre del productor que no se repita</li>
             </ul>
         </li>
         <li>20/11/2009<ul>
             <li>Reporte por catalogo de movimientos de bancos agregado</li>
             <li>Reporte de boletas por producto</li>
             <li>Ya se pueden realizar traspasos de bancos a banco y de banco a caja</li>
             </ul>
         </li>
          </ul>
    <ul>
         <li>19/11/2009 11:10 AM<ul>
             <li>Reparada funcion para guardar correctamente los pagos de las liquidaciones 
                 (insert into Pagosliquidaciones)</li>
             </ul>
         </li>
         <li>19/11/2009 1:30 AM<ul>
             <li>Arreglado error que ponia la descripción en el número de factura o larguillo.</li>
             <li>Ahora se puede editar el peso en las boletas cuando se hace una liquidación 
                 cuando es 0.</li>
             <li>Se permite filtrar las liquidaciones por nombre de productor, por ciclo y por 
                 cobradas y no cobradas.</li>
             <li>Se muestran las boletas relacionadas con un anticipo en los movimientos de banco 
                 y de caja chica.</li>
             <li>Se corrigió la lista de pagos de la liquidación para que muestra la información 
                 de la cuenta y el banco.</li>
             <li>Cuando se agrega un pago a una liquidación, sólo se muestran los conceptos de 
                 banco que sean válidos para hacer el pago</li>
             <li>Se agregó la opción de catálogos de cuentas para ver la lista y agregar.</li>
             <li>Problema al abrir una liquidacion realizada no cargaba la informacion de los pagos. (La informacion de los pagos se obtiene de los pagos en sí evitando leer el XML)</li>
             </ul>
         </li>
         <li>18/11/2009 4:30 PM 
            
            <ul>
                <li>Se cambió el orden de como se muestran las liquidaciones.</li>
                
                
            </ul>
         </li>
         <li>18/11/2009 1:20 PM 
            <ul>
                <li>Se agregó una función para preseleccionar el valor del productor en todas las partes donde se muestra en la liquidación.</li>
                <li>Se cambió la función para el calculo de totales.</li>
                
            </ul>
        </li>
        
          <li>18/11/2009 12:30 PM 
            <ul>
                <li>Se agregó la opción "Traer todas las boletas del productor" al hacer una nueva liquidación.</li>
                <li>Se cambió la impresión de la liquidación para imprimir en mayúsculas en la tabla de boletas.</li>
                
            </ul>
        </li>
        
        <li>18/11/2009 (12:50 AM)<ul>
            <li>no se puede agregar una liquidacion sin un pago valido</li>
            <li>al ingresar un pago en liquidaciones se limpian los conceptos a vacios.</li>
            <li>los anticipos son calculados correctamente en la liquidacion.</li>
            <li>se han ocultado las columnas innecesarias al realizar la impresion en los 
                anticipos y pagos.</li>
            <li>se oculta &quot;eliminar&quot; en los pagos que se han realizado una vez que la 
                liquidacion es realizada.</li>
            </ul>
        </li>
        <li>17/11/2009 (3:45 PM)<ul>
            <li>los datos de descuentos de las boletas son guardados correctamente. </li>
            <li>los datos de las boletas de la liquidacion son cargados correctamente al abrir 
                una liquidacion </li>
            <li>suma correctamente los totales de las boletas en la liquidacion </li>
            <li>funcion para el calculo de secado actualizada si humedad &gt;= 16 entonces 
                ((humedad - 16)*10 + 50 ) * tonelada si humedad &lt; 16 entonces no descontar nada
            </li>
            <li>impresion cambiada de vertical a horizontal. </li>
            <li>incremento del tamaño de letra en la impresion de la liquidacion </li>
            <li>arreglado problema al realizar pago con caja chica se agregaba el pago en cero
            </li>
            <li>arreglado problema que no se agregaban los pagos a la caja chica ni a bancos al 
                realizar liquidacion </li>
            </ul>
        </li>
        <li>17/11/2009
            <ul>
                <li>solucionado problema para dar de alta una nueva liquidacion.</li>
                <li>se muestran ceros cuando se ven los movimientos de un credito pero no hay 
                    creditos existentes</li>
                <li>se muestra la opcion &quot;TODOS LOS PRODUCTOS&quot; al ver la lista de&nbsp; boletas para 
                    filtar.</li>
                <li>Al agregar una nueva boleta en liquidaciones los campos se limpian para permitir 
                    una nueva boleta.</li>
            </ul>
        </li>
      
          
    </ul>
</asp:Content>
