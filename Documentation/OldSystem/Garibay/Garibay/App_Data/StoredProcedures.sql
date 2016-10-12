/****************************************************************************/
/* Code Author (written by Xin Zhao)                                        */
/*                                                                          */
/* This file was automatically generated using Code Author.                 */
/* Any manual changes to this file will be overwritten by a automated tool. */
/*                                                                          */
/* Date Generated: 08/09/2012 12:05:04 p.m.                                    */
/*                                                                          */
/* www.CodeAuthor.org                                                       */
/****************************************************************************/
/*************************************************/
/* [dbo].gspBoletas_INSERT */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspBoletas_INSERT') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspBoletas_INSERT
GO

CREATE PROCEDURE [dbo].gspBoletas_INSERT
(
@cicloID AS int = null,
@userID AS int = null,
@productorID AS int = null,
@humedad AS float = null,
@dctoHumedad AS float = null,
@impurezas AS float = null,
@totaldescuentos AS money = null,
@pesonetoapagar AS float = null,
@precioapagar AS money = null,
@importe AS money = null,
@placas AS varchar(50) = null,
@chofer AS varchar(250) = null,
@pagada AS bit = null,
@storeTS AS datetime = null,
@updateTS AS datetime = null,
@productoID AS int = null,
@numeroBoleta AS varchar(15) = null,
@ticket AS varchar(15) = null,
@codigoClienteProvArchivo AS varchar(10) = null,
@nombreProductor AS varchar(50) = null,
@fechaEntrada AS datetime = null,
@pesadorEntrada AS varchar(50) = null,
@pesoDeEntrada AS float = null,
@basculaEntrada AS varchar(50) = null,
@fechaSalida AS datetime = null,
@pesoDeSalida AS float = null,
@pesadorSalida AS varchar(50) = null,
@basculaSalida AS varchar(50) = null,
@pesonetoentrada AS float = null,
@pesonetosalida AS float = null,
@dctoImpurezas AS money = null,
@dctoSecado AS money = null,
@totalapagar AS money = null,
@bodegaID AS int = null,
@applyHumedad AS bit = null,
@applyImpurezas AS bit = null,
@applySecado AS bit = null,
@folioDestino AS varchar(15) = null,
@pesoDestino AS float = null,
@merma AS float = null,
@flete AS float = null,
@importeFlete AS float = null,
@precioNetoDestino AS float = null,
@dctoGranoChico AS float = null,
@dctoGranoDanado AS float = null,
@dctoGranoQuebrado AS float = null,
@dctoGranoEstrellado AS float = null,
@transportistaID AS int = null,
@cabezasDeGanado AS int = null,
@llevaFlete AS bit = null,
@deGranjaACorrales AS bit = null,
@lastEditDate AS datetime = null,
@creationDate AS datetime = null,
@precioTransportista AS money = null,
@usaPesoDestinoParaTransportista AS bit = null
)
AS

INSERT INTO
  [dbo].[Boletas]
(
  [cicloID],
  [userID],
  [productorID],
  [humedad],
  [dctoHumedad],
  [impurezas],
  [totaldescuentos],
  [pesonetoapagar],
  [precioapagar],
  [importe],
  [placas],
  [chofer],
  [pagada],
  [storeTS],
  [updateTS],
  [productoID],
  [NumeroBoleta],
  [Ticket],
  [codigoClienteProvArchivo],
  [NombreProductor],
  [FechaEntrada],
  [PesadorEntrada],
  [PesoDeEntrada],
  [BasculaEntrada],
  [FechaSalida],
  [PesoDeSalida],
  [PesadorSalida],
  [BasculaSalida],
  [pesonetoentrada],
  [pesonetosalida],
  [dctoImpurezas],
  [dctoSecado],
  [totalapagar],
  [bodegaID],
  [applyHumedad],
  [applyImpurezas],
  [applySecado],
  [FolioDestino],
  [PesoDestino],
  [Merma],
  [Flete],
  [ImporteFlete],
  [PrecioNetoDestino],
  [dctoGranoChico],
  [dctoGranoDanado],
  [dctoGranoQuebrado],
  [dctoGranoEstrellado],
  [transportistaID],
  [cabezasDeGanado],
  [llevaFlete],
  [deGranjaACorrales],
  [LastEditDate],
  [CreationDate],
  [PrecioTransportista],
  [UsaPesoDestinoParaTransportista]
)
VALUES
(
  @cicloID,
  @userID,
  @productorID,
  @humedad,
  @dctoHumedad,
  @impurezas,
  @totaldescuentos,
  @pesonetoapagar,
  @precioapagar,
  @importe,
  @placas,
  @chofer,
  @pagada,
  @storeTS,
  @updateTS,
  @productoID,
  @numeroBoleta,
  @ticket,
  @codigoClienteProvArchivo,
  @nombreProductor,
  @fechaEntrada,
  @pesadorEntrada,
  @pesoDeEntrada,
  @basculaEntrada,
  @fechaSalida,
  @pesoDeSalida,
  @pesadorSalida,
  @basculaSalida,
  @pesonetoentrada,
  @pesonetosalida,
  @dctoImpurezas,
  @dctoSecado,
  @totalapagar,
  @bodegaID,
  @applyHumedad,
  @applyImpurezas,
  @applySecado,
  @folioDestino,
  @pesoDestino,
  @merma,
  @flete,
  @importeFlete,
  @precioNetoDestino,
  @dctoGranoChico,
  @dctoGranoDanado,
  @dctoGranoQuebrado,
  @dctoGranoEstrellado,
  @transportistaID,
  @cabezasDeGanado,
  @llevaFlete,
  @deGranjaACorrales,
  @lastEditDate,
  @creationDate,
  @precioTransportista,
  @usaPesoDestinoParaTransportista
)

SELECT SCOPE_IDENTITY()

GO

/*************************************************/
/* [dbo].gspBoletas_SELECT */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspBoletas_SELECT') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspBoletas_SELECT
GO

CREATE PROCEDURE [dbo].gspBoletas_SELECT
(
@boletaID AS int
)
AS

SELECT
  *
FROM
  [dbo].[Boletas]
WHERE
  boletaID = @boletaID

GO

/*************************************************/
/* [dbo].gspBoletas_UPDATE */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspBoletas_UPDATE') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspBoletas_UPDATE
GO

CREATE PROCEDURE [dbo].gspBoletas_UPDATE
(
@boletaID int = null,
@cicloID int = null,
@userID int = null,
@productorID int = null,
@humedad float = null,
@dctoHumedad float = null,
@impurezas float = null,
@totaldescuentos money = null,
@pesonetoapagar float = null,
@precioapagar money = null,
@importe money = null,
@placas varchar(50) = null,
@chofer varchar(250) = null,
@pagada bit = null,
@storeTS datetime = null,
@updateTS datetime = null,
@productoID int = null,
@numeroBoleta varchar(15) = null,
@ticket varchar(15) = null,
@codigoClienteProvArchivo varchar(10) = null,
@nombreProductor varchar(50) = null,
@fechaEntrada datetime = null,
@pesadorEntrada varchar(50) = null,
@pesoDeEntrada float = null,
@basculaEntrada varchar(50) = null,
@fechaSalida datetime = null,
@pesoDeSalida float = null,
@pesadorSalida varchar(50) = null,
@basculaSalida varchar(50) = null,
@pesonetoentrada float = null,
@pesonetosalida float = null,
@dctoImpurezas money = null,
@dctoSecado money = null,
@totalapagar money = null,
@bodegaID int = null,
@applyHumedad bit = null,
@applyImpurezas bit = null,
@applySecado bit = null,
@folioDestino varchar(15) = null,
@pesoDestino float = null,
@merma float = null,
@flete float = null,
@importeFlete float = null,
@precioNetoDestino float = null,
@dctoGranoChico float = null,
@dctoGranoDanado float = null,
@dctoGranoQuebrado float = null,
@dctoGranoEstrellado float = null,
@transportistaID int = null,
@cabezasDeGanado int = null,
@llevaFlete bit = null,
@deGranjaACorrales bit = null,
@lastEditDate datetime = null,
@creationDate datetime = null,
@precioTransportista money = null,
@usaPesoDestinoParaTransportista bit = null
)
AS

UPDATE
  [dbo].[Boletas]
SET
  [cicloID] = @cicloID,
  [userID] = @userID,
  [productorID] = @productorID,
  [humedad] = @humedad,
  [dctoHumedad] = @dctoHumedad,
  [impurezas] = @impurezas,
  [totaldescuentos] = @totaldescuentos,
  [pesonetoapagar] = @pesonetoapagar,
  [precioapagar] = @precioapagar,
  [importe] = @importe,
  [placas] = @placas,
  [chofer] = @chofer,
  [pagada] = @pagada,
  [storeTS] = @storeTS,
  [updateTS] = @updateTS,
  [productoID] = @productoID,
  [NumeroBoleta] = @numeroBoleta,
  [Ticket] = @ticket,
  [codigoClienteProvArchivo] = @codigoClienteProvArchivo,
  [NombreProductor] = @nombreProductor,
  [FechaEntrada] = @fechaEntrada,
  [PesadorEntrada] = @pesadorEntrada,
  [PesoDeEntrada] = @pesoDeEntrada,
  [BasculaEntrada] = @basculaEntrada,
  [FechaSalida] = @fechaSalida,
  [PesoDeSalida] = @pesoDeSalida,
  [PesadorSalida] = @pesadorSalida,
  [BasculaSalida] = @basculaSalida,
  [pesonetoentrada] = @pesonetoentrada,
  [pesonetosalida] = @pesonetosalida,
  [dctoImpurezas] = @dctoImpurezas,
  [dctoSecado] = @dctoSecado,
  [totalapagar] = @totalapagar,
  [bodegaID] = @bodegaID,
  [applyHumedad] = @applyHumedad,
  [applyImpurezas] = @applyImpurezas,
  [applySecado] = @applySecado,
  [FolioDestino] = @folioDestino,
  [PesoDestino] = @pesoDestino,
  [Merma] = @merma,
  [Flete] = @flete,
  [ImporteFlete] = @importeFlete,
  [PrecioNetoDestino] = @precioNetoDestino,
  [dctoGranoChico] = @dctoGranoChico,
  [dctoGranoDanado] = @dctoGranoDanado,
  [dctoGranoQuebrado] = @dctoGranoQuebrado,
  [dctoGranoEstrellado] = @dctoGranoEstrellado,
  [transportistaID] = @transportistaID,
  [cabezasDeGanado] = @cabezasDeGanado,
  [llevaFlete] = @llevaFlete,
  [deGranjaACorrales] = @deGranjaACorrales,
  [LastEditDate] = @lastEditDate,
  [CreationDate] = @creationDate,
  [PrecioTransportista] = @precioTransportista,
  [UsaPesoDestinoParaTransportista] = @usaPesoDestinoParaTransportista
WHERE
  [boletaID] = @boletaID

GO

/*************************************************/
/* [dbo].gspBoletas_DELETE */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspBoletas_DELETE') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspBoletas_DELETE
GO

CREATE PROCEDURE [dbo].gspBoletas_DELETE
(
@boletaID int
)
AS

DELETE
  [dbo].[Boletas]
WHERE
  [boletaID] = @boletaID

GO

/*************************************************/
/* [dbo].gspBoletas_SEARCH */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspBoletas_SEARCH') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspBoletas_SEARCH
GO

CREATE PROCEDURE [dbo].gspBoletas_SEARCH
(
@boletaID int = null,
@cicloID int = null,
@userID int = null,
@productorID int = null,
@humedad float = null,
@dctoHumedad float = null,
@impurezas float = null,
@totaldescuentos money = null,
@pesonetoapagar float = null,
@precioapagar money = null,
@importe money = null,
@placas varchar(50) = null,
@chofer varchar(250) = null,
@pagada bit = null,
@storeTS datetime = null,
@updateTS datetime = null,
@productoID int = null,
@numeroBoleta varchar(15) = null,
@ticket varchar(15) = null,
@codigoClienteProvArchivo varchar(10) = null,
@nombreProductor varchar(50) = null,
@fechaEntrada datetime = null,
@pesadorEntrada varchar(50) = null,
@pesoDeEntrada float = null,
@basculaEntrada varchar(50) = null,
@fechaSalida datetime = null,
@pesoDeSalida float = null,
@pesadorSalida varchar(50) = null,
@basculaSalida varchar(50) = null,
@pesonetoentrada float = null,
@pesonetosalida float = null,
@dctoImpurezas money = null,
@dctoSecado money = null,
@totalapagar money = null,
@bodegaID int = null,
@applyHumedad bit = null,
@applyImpurezas bit = null,
@applySecado bit = null,
@folioDestino varchar(15) = null,
@pesoDestino float = null,
@merma float = null,
@flete float = null,
@importeFlete float = null,
@precioNetoDestino float = null,
@dctoGranoChico float = null,
@dctoGranoDanado float = null,
@dctoGranoQuebrado float = null,
@dctoGranoEstrellado float = null,
@transportistaID int = null,
@cabezasDeGanado int = null,
@llevaFlete bit = null,
@deGranjaACorrales bit = null,
@lastEditDate datetime = null,
@creationDate datetime = null,
@precioTransportista money = null,
@usaPesoDestinoParaTransportista bit = null
)
AS

SELECT
  *
FROM
  [dbo].[Boletas]
WHERE
  (@boletaID IS NULL OR [boletaID] = @boletaID)
AND
  (@cicloID IS NULL OR [cicloID] = @cicloID)
AND
  (@userID IS NULL OR [userID] = @userID)
AND
  (@productorID IS NULL OR [productorID] = @productorID)
AND
  (@humedad IS NULL OR [humedad] = @humedad)
AND
  (@dctoHumedad IS NULL OR [dctoHumedad] = @dctoHumedad)
AND
  (@impurezas IS NULL OR [impurezas] = @impurezas)
AND
  (@totaldescuentos IS NULL OR [totaldescuentos] = @totaldescuentos)
AND
  (@pesonetoapagar IS NULL OR [pesonetoapagar] = @pesonetoapagar)
AND
  (@precioapagar IS NULL OR [precioapagar] = @precioapagar)
AND
  (@importe IS NULL OR [importe] = @importe)
AND
  (@placas IS NULL OR @placas = '' OR [placas] LIKE @placas + '%')
AND
  (@chofer IS NULL OR @chofer = '' OR [chofer] LIKE @chofer + '%')
AND
  (@pagada IS NULL OR [pagada] = @pagada)
AND
  (@storeTS IS NULL OR [storeTS] = @storeTS)
AND
  (@updateTS IS NULL OR [updateTS] = @updateTS)
AND
  (@productoID IS NULL OR [productoID] = @productoID)
AND
  (@numeroBoleta IS NULL OR @numeroBoleta = '' OR [NumeroBoleta] LIKE @numeroBoleta + '%')
AND
  (@ticket IS NULL OR @ticket = '' OR [Ticket] LIKE @ticket + '%')
AND
  (@codigoClienteProvArchivo IS NULL OR @codigoClienteProvArchivo = '' OR [codigoClienteProvArchivo] LIKE @codigoClienteProvArchivo + '%')
AND
  (@nombreProductor IS NULL OR @nombreProductor = '' OR [NombreProductor] LIKE @nombreProductor + '%')
AND
  (@fechaEntrada IS NULL OR [FechaEntrada] = @fechaEntrada)
AND
  (@pesadorEntrada IS NULL OR @pesadorEntrada = '' OR [PesadorEntrada] LIKE @pesadorEntrada + '%')
AND
  (@pesoDeEntrada IS NULL OR [PesoDeEntrada] = @pesoDeEntrada)
AND
  (@basculaEntrada IS NULL OR @basculaEntrada = '' OR [BasculaEntrada] LIKE @basculaEntrada + '%')
AND
  (@fechaSalida IS NULL OR [FechaSalida] = @fechaSalida)
AND
  (@pesoDeSalida IS NULL OR [PesoDeSalida] = @pesoDeSalida)
AND
  (@pesadorSalida IS NULL OR @pesadorSalida = '' OR [PesadorSalida] LIKE @pesadorSalida + '%')
AND
  (@basculaSalida IS NULL OR @basculaSalida = '' OR [BasculaSalida] LIKE @basculaSalida + '%')
AND
  (@pesonetoentrada IS NULL OR [pesonetoentrada] = @pesonetoentrada)
AND
  (@pesonetosalida IS NULL OR [pesonetosalida] = @pesonetosalida)
AND
  (@dctoImpurezas IS NULL OR [dctoImpurezas] = @dctoImpurezas)
AND
  (@dctoSecado IS NULL OR [dctoSecado] = @dctoSecado)
AND
  (@totalapagar IS NULL OR [totalapagar] = @totalapagar)
AND
  (@bodegaID IS NULL OR [bodegaID] = @bodegaID)
AND
  (@applyHumedad IS NULL OR [applyHumedad] = @applyHumedad)
AND
  (@applyImpurezas IS NULL OR [applyImpurezas] = @applyImpurezas)
AND
  (@applySecado IS NULL OR [applySecado] = @applySecado)
AND
  (@folioDestino IS NULL OR @folioDestino = '' OR [FolioDestino] LIKE @folioDestino + '%')
AND
  (@pesoDestino IS NULL OR [PesoDestino] = @pesoDestino)
AND
  (@merma IS NULL OR [Merma] = @merma)
AND
  (@flete IS NULL OR [Flete] = @flete)
AND
  (@importeFlete IS NULL OR [ImporteFlete] = @importeFlete)
AND
  (@precioNetoDestino IS NULL OR [PrecioNetoDestino] = @precioNetoDestino)
AND
  (@dctoGranoChico IS NULL OR [dctoGranoChico] = @dctoGranoChico)
AND
  (@dctoGranoDanado IS NULL OR [dctoGranoDanado] = @dctoGranoDanado)
AND
  (@dctoGranoQuebrado IS NULL OR [dctoGranoQuebrado] = @dctoGranoQuebrado)
AND
  (@dctoGranoEstrellado IS NULL OR [dctoGranoEstrellado] = @dctoGranoEstrellado)
AND
  (@transportistaID IS NULL OR [transportistaID] = @transportistaID)
AND
  (@cabezasDeGanado IS NULL OR [cabezasDeGanado] = @cabezasDeGanado)
AND
  (@llevaFlete IS NULL OR [llevaFlete] = @llevaFlete)
AND
  (@deGranjaACorrales IS NULL OR [deGranjaACorrales] = @deGranjaACorrales)
AND
  (@lastEditDate IS NULL OR [LastEditDate] = @lastEditDate)
AND
  (@creationDate IS NULL OR [CreationDate] = @creationDate)
AND
  (@precioTransportista IS NULL OR [PrecioTransportista] = @precioTransportista)
AND
  (@usaPesoDestinoParaTransportista IS NULL OR [UsaPesoDestinoParaTransportista] = @usaPesoDestinoParaTransportista)

GO

/*************************************************/
/* [dbo].gspProductores_INSERT */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspProductores_INSERT') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspProductores_INSERT
GO

CREATE PROCEDURE [dbo].gspProductores_INSERT
(
@apaterno AS varchar(50) = null,
@amaterno AS varchar(50) = null,
@nombre AS varchar(50) = null,
@fechanacimiento AS datetime = null,
@ife AS varchar(50) = null,
@curp AS varchar(50) = null,
@domicilio AS varchar(MAX) = null,
@poblacion AS varchar(50) = null,
@municipio AS varchar(50) = null,
@estadoID AS int = null,
@cp AS varchar(50) = null,
@rfc AS varchar(50) = null,
@sexoID AS int = null,
@telefono AS varchar(50) = null,
@telefonotrabajo AS varchar(50) = null,
@celular AS varchar(50) = null,
@fax AS varchar(50) = null,
@email AS varchar(50) = null,
@estadocivilID AS int = null,
@regimenID AS int = null,
@codigoBoletasFile AS varchar(10) = null,
@storeTS AS datetime = null,
@updateTS AS datetime = null,
@colonia AS varchar(255) = null,
@conyugue AS varchar(255) = null,
@lastEditDate AS datetime = null,
@creationDate AS datetime = null
)
AS

INSERT INTO
  [dbo].[Productores]
(
  [apaterno],
  [amaterno],
  [nombre],
  [fechanacimiento],
  [IFE],
  [CURP],
  [domicilio],
  [poblacion],
  [municipio],
  [estadoID],
  [CP],
  [RFC],
  [sexoID],
  [telefono],
  [telefonotrabajo],
  [celular],
  [fax],
  [email],
  [estadocivilID],
  [regimenID],
  [codigoBoletasFile],
  [storeTS],
  [updateTS],
  [colonia],
  [conyugue],
  [LastEditDate],
  [CreationDate]
)
VALUES
(
  @apaterno,
  @amaterno,
  @nombre,
  @fechanacimiento,
  @ife,
  @curp,
  @domicilio,
  @poblacion,
  @municipio,
  @estadoID,
  @cp,
  @rfc,
  @sexoID,
  @telefono,
  @telefonotrabajo,
  @celular,
  @fax,
  @email,
  @estadocivilID,
  @regimenID,
  @codigoBoletasFile,
  @storeTS,
  @updateTS,
  @colonia,
  @conyugue,
  @lastEditDate,
  @creationDate
)

SELECT SCOPE_IDENTITY()

GO

/*************************************************/
/* [dbo].gspProductores_SELECT */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspProductores_SELECT') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspProductores_SELECT
GO

CREATE PROCEDURE [dbo].gspProductores_SELECT
(
@productorID AS int
)
AS

SELECT
  *
FROM
  [dbo].[Productores]
WHERE
  productorID = @productorID

GO

/*************************************************/
/* [dbo].gspProductores_UPDATE */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspProductores_UPDATE') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspProductores_UPDATE
GO

CREATE PROCEDURE [dbo].gspProductores_UPDATE
(
@productorID int = null,
@apaterno varchar(50) = null,
@amaterno varchar(50) = null,
@nombre varchar(50) = null,
@fechanacimiento datetime = null,
@ife varchar(50) = null,
@curp varchar(50) = null,
@domicilio varchar(MAX) = null,
@poblacion varchar(50) = null,
@municipio varchar(50) = null,
@estadoID int = null,
@cp varchar(50) = null,
@rfc varchar(50) = null,
@sexoID int = null,
@telefono varchar(50) = null,
@telefonotrabajo varchar(50) = null,
@celular varchar(50) = null,
@fax varchar(50) = null,
@email varchar(50) = null,
@estadocivilID int = null,
@regimenID int = null,
@codigoBoletasFile varchar(10) = null,
@storeTS datetime = null,
@updateTS datetime = null,
@colonia varchar(255) = null,
@conyugue varchar(255) = null,
@lastEditDate datetime = null,
@creationDate datetime = null
)
AS

UPDATE
  [dbo].[Productores]
SET
  [apaterno] = @apaterno,
  [amaterno] = @amaterno,
  [nombre] = @nombre,
  [fechanacimiento] = @fechanacimiento,
  [IFE] = @ife,
  [CURP] = @curp,
  [domicilio] = @domicilio,
  [poblacion] = @poblacion,
  [municipio] = @municipio,
  [estadoID] = @estadoID,
  [CP] = @cp,
  [RFC] = @rfc,
  [sexoID] = @sexoID,
  [telefono] = @telefono,
  [telefonotrabajo] = @telefonotrabajo,
  [celular] = @celular,
  [fax] = @fax,
  [email] = @email,
  [estadocivilID] = @estadocivilID,
  [regimenID] = @regimenID,
  [codigoBoletasFile] = @codigoBoletasFile,
  [storeTS] = @storeTS,
  [updateTS] = @updateTS,
  [colonia] = @colonia,
  [conyugue] = @conyugue,
  [LastEditDate] = @lastEditDate,
  [CreationDate] = @creationDate
WHERE
  [productorID] = @productorID

GO

/*************************************************/
/* [dbo].gspProductores_DELETE */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspProductores_DELETE') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspProductores_DELETE
GO

CREATE PROCEDURE [dbo].gspProductores_DELETE
(
@productorID int
)
AS

DELETE
  [dbo].[Productores]
WHERE
  [productorID] = @productorID

GO

/*************************************************/
/* [dbo].gspProductores_SEARCH */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspProductores_SEARCH') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspProductores_SEARCH
GO

CREATE PROCEDURE [dbo].gspProductores_SEARCH
(
@productorID int = null,
@apaterno varchar(50) = null,
@amaterno varchar(50) = null,
@nombre varchar(50) = null,
@fechanacimiento datetime = null,
@ife varchar(50) = null,
@curp varchar(50) = null,
@domicilio varchar(MAX) = null,
@poblacion varchar(50) = null,
@municipio varchar(50) = null,
@estadoID int = null,
@cp varchar(50) = null,
@rfc varchar(50) = null,
@sexoID int = null,
@telefono varchar(50) = null,
@telefonotrabajo varchar(50) = null,
@celular varchar(50) = null,
@fax varchar(50) = null,
@email varchar(50) = null,
@estadocivilID int = null,
@regimenID int = null,
@codigoBoletasFile varchar(10) = null,
@storeTS datetime = null,
@updateTS datetime = null,
@colonia varchar(255) = null,
@conyugue varchar(255) = null,
@lastEditDate datetime = null,
@creationDate datetime = null
)
AS

SELECT
  *
FROM
  [dbo].[Productores]
WHERE
  (@productorID IS NULL OR [productorID] = @productorID)
AND
  (@apaterno IS NULL OR @apaterno = '' OR [apaterno] LIKE @apaterno + '%')
AND
  (@amaterno IS NULL OR @amaterno = '' OR [amaterno] LIKE @amaterno + '%')
AND
  (@nombre IS NULL OR @nombre = '' OR [nombre] LIKE @nombre + '%')
AND
  (@fechanacimiento IS NULL OR [fechanacimiento] = @fechanacimiento)
AND
  (@ife IS NULL OR @ife = '' OR [IFE] LIKE @ife + '%')
AND
  (@curp IS NULL OR @curp = '' OR [CURP] LIKE @curp + '%')
AND
  (@poblacion IS NULL OR @poblacion = '' OR [poblacion] LIKE @poblacion + '%')
AND
  (@municipio IS NULL OR @municipio = '' OR [municipio] LIKE @municipio + '%')
AND
  (@estadoID IS NULL OR [estadoID] = @estadoID)
AND
  (@cp IS NULL OR @cp = '' OR [CP] LIKE @cp + '%')
AND
  (@rfc IS NULL OR @rfc = '' OR [RFC] LIKE @rfc + '%')
AND
  (@sexoID IS NULL OR [sexoID] = @sexoID)
AND
  (@telefono IS NULL OR @telefono = '' OR [telefono] LIKE @telefono + '%')
AND
  (@telefonotrabajo IS NULL OR @telefonotrabajo = '' OR [telefonotrabajo] LIKE @telefonotrabajo + '%')
AND
  (@celular IS NULL OR @celular = '' OR [celular] LIKE @celular + '%')
AND
  (@fax IS NULL OR @fax = '' OR [fax] LIKE @fax + '%')
AND
  (@email IS NULL OR @email = '' OR [email] LIKE @email + '%')
AND
  (@estadocivilID IS NULL OR [estadocivilID] = @estadocivilID)
AND
  (@regimenID IS NULL OR [regimenID] = @regimenID)
AND
  (@codigoBoletasFile IS NULL OR @codigoBoletasFile = '' OR [codigoBoletasFile] LIKE @codigoBoletasFile + '%')
AND
  (@storeTS IS NULL OR [storeTS] = @storeTS)
AND
  (@updateTS IS NULL OR [updateTS] = @updateTS)
AND
  (@colonia IS NULL OR @colonia = '' OR [colonia] LIKE @colonia + '%')
AND
  (@conyugue IS NULL OR @conyugue = '' OR [conyugue] LIKE @conyugue + '%')
AND
  (@lastEditDate IS NULL OR [LastEditDate] = @lastEditDate)
AND
  (@creationDate IS NULL OR [CreationDate] = @creationDate)

GO

/*************************************************/
/* [dbo].gspProductos_INSERT */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspProductos_INSERT') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspProductos_INSERT
GO

CREATE PROCEDURE [dbo].gspProductos_INSERT
(
@nombre AS varchar(50) = null,
@codigo AS varchar(10) = null,
@descripcion AS text = null,
@precio1 AS float = null,
@precio2 AS float = null,
@precio3 AS float = null,
@precio4 AS float = null,
@storeTS AS datetime = null,
@updateTS AS datetime = null,
@unidadID AS int = null,
@presentacionID AS int = null,
@codigoBascula AS varchar(10) = null,
@productoGrupoID AS int = null,
@casaagricolaID AS int = null,
@lastEditDate AS datetime = null,
@creationDate AS datetime = null
)
AS

INSERT INTO
  [dbo].[Productos]
(
  [Nombre],
  [codigo],
  [descripcion],
  [precio1],
  [precio2],
  [precio3],
  [precio4],
  [storeTS],
  [updateTS],
  [unidadID],
  [presentacionID],
  [codigoBascula],
  [productoGrupoID],
  [casaagricolaID],
  [LastEditDate],
  [CreationDate]
)
VALUES
(
  @nombre,
  @codigo,
  @descripcion,
  @precio1,
  @precio2,
  @precio3,
  @precio4,
  @storeTS,
  @updateTS,
  @unidadID,
  @presentacionID,
  @codigoBascula,
  @productoGrupoID,
  @casaagricolaID,
  @lastEditDate,
  @creationDate
)

SELECT SCOPE_IDENTITY()

GO

/*************************************************/
/* [dbo].gspProductos_SELECT */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspProductos_SELECT') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspProductos_SELECT
GO

CREATE PROCEDURE [dbo].gspProductos_SELECT
(
@productoID AS int
)
AS

SELECT
  *
FROM
  [dbo].[Productos]
WHERE
  productoID = @productoID

GO

/*************************************************/
/* [dbo].gspProductos_UPDATE */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspProductos_UPDATE') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspProductos_UPDATE
GO

CREATE PROCEDURE [dbo].gspProductos_UPDATE
(
@productoID int = null,
@nombre varchar(50) = null,
@codigo varchar(10) = null,
@descripcion text = null,
@precio1 float = null,
@precio2 float = null,
@precio3 float = null,
@precio4 float = null,
@storeTS datetime = null,
@updateTS datetime = null,
@unidadID int = null,
@presentacionID int = null,
@codigoBascula varchar(10) = null,
@productoGrupoID int = null,
@casaagricolaID int = null,
@lastEditDate datetime = null,
@creationDate datetime = null
)
AS

UPDATE
  [dbo].[Productos]
SET
  [Nombre] = @nombre,
  [codigo] = @codigo,
  [descripcion] = @descripcion,
  [precio1] = @precio1,
  [precio2] = @precio2,
  [precio3] = @precio3,
  [precio4] = @precio4,
  [storeTS] = @storeTS,
  [updateTS] = @updateTS,
  [unidadID] = @unidadID,
  [presentacionID] = @presentacionID,
  [codigoBascula] = @codigoBascula,
  [productoGrupoID] = @productoGrupoID,
  [casaagricolaID] = @casaagricolaID,
  [LastEditDate] = @lastEditDate,
  [CreationDate] = @creationDate
WHERE
  [productoID] = @productoID

GO

/*************************************************/
/* [dbo].gspProductos_DELETE */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspProductos_DELETE') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspProductos_DELETE
GO

CREATE PROCEDURE [dbo].gspProductos_DELETE
(
@productoID int
)
AS

DELETE
  [dbo].[Productos]
WHERE
  [productoID] = @productoID

GO

/*************************************************/
/* [dbo].gspProductos_SEARCH */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspProductos_SEARCH') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspProductos_SEARCH
GO

CREATE PROCEDURE [dbo].gspProductos_SEARCH
(
@productoID int = null,
@nombre varchar(50) = null,
@codigo varchar(10) = null,
@descripcion text = null,
@precio1 float = null,
@precio2 float = null,
@precio3 float = null,
@precio4 float = null,
@storeTS datetime = null,
@updateTS datetime = null,
@unidadID int = null,
@presentacionID int = null,
@codigoBascula varchar(10) = null,
@productoGrupoID int = null,
@casaagricolaID int = null,
@lastEditDate datetime = null,
@creationDate datetime = null
)
AS

SELECT
  *
FROM
  [dbo].[Productos]
WHERE
  (@productoID IS NULL OR [productoID] = @productoID)
AND
  (@nombre IS NULL OR @nombre = '' OR [Nombre] LIKE @nombre + '%')
AND
  (@codigo IS NULL OR @codigo = '' OR [codigo] LIKE @codigo + '%')
AND
  (@precio1 IS NULL OR [precio1] = @precio1)
AND
  (@precio2 IS NULL OR [precio2] = @precio2)
AND
  (@precio3 IS NULL OR [precio3] = @precio3)
AND
  (@precio4 IS NULL OR [precio4] = @precio4)
AND
  (@storeTS IS NULL OR [storeTS] = @storeTS)
AND
  (@updateTS IS NULL OR [updateTS] = @updateTS)
AND
  (@unidadID IS NULL OR [unidadID] = @unidadID)
AND
  (@presentacionID IS NULL OR [presentacionID] = @presentacionID)
AND
  (@codigoBascula IS NULL OR @codigoBascula = '' OR [codigoBascula] LIKE @codigoBascula + '%')
AND
  (@productoGrupoID IS NULL OR [productoGrupoID] = @productoGrupoID)
AND
  (@casaagricolaID IS NULL OR [casaagricolaID] = @casaagricolaID)
AND
  (@lastEditDate IS NULL OR [LastEditDate] = @lastEditDate)
AND
  (@creationDate IS NULL OR [CreationDate] = @creationDate)

GO

/*************************************************/
/* [dbo].gspUsers_INSERT */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspUsers_INSERT') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspUsers_INSERT
GO

CREATE PROCEDURE [dbo].gspUsers_INSERT
(
@username AS varchar(10) = null,
@password AS varchar(50) = null,
@securitylevelID AS int = null,
@enabled AS bit = null,
@nombre AS varchar(50) = null,
@email AS varchar(50) = null,
@lastEditDate AS datetime = null,
@creationDate AS datetime = null
)
AS

INSERT INTO
  [dbo].[Users]
(
  [username],
  [password],
  [securitylevelID],
  [enabled],
  [Nombre],
  [email],
  [LastEditDate],
  [CreationDate]
)
VALUES
(
  @username,
  @password,
  @securitylevelID,
  @enabled,
  @nombre,
  @email,
  @lastEditDate,
  @creationDate
)

SELECT SCOPE_IDENTITY()

GO

/*************************************************/
/* [dbo].gspUsers_SELECT */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspUsers_SELECT') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspUsers_SELECT
GO

CREATE PROCEDURE [dbo].gspUsers_SELECT
(
@userID AS int
)
AS

SELECT
  *
FROM
  [dbo].[Users]
WHERE
  userID = @userID

GO

/*************************************************/
/* [dbo].gspUsers_UPDATE */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspUsers_UPDATE') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspUsers_UPDATE
GO

CREATE PROCEDURE [dbo].gspUsers_UPDATE
(
@userID int = null,
@username varchar(10) = null,
@password varchar(50) = null,
@securitylevelID int = null,
@enabled bit = null,
@nombre varchar(50) = null,
@email varchar(50) = null,
@lastEditDate datetime = null,
@creationDate datetime = null
)
AS

UPDATE
  [dbo].[Users]
SET
  [username] = @username,
  [password] = @password,
  [securitylevelID] = @securitylevelID,
  [enabled] = @enabled,
  [Nombre] = @nombre,
  [email] = @email,
  [LastEditDate] = @lastEditDate,
  [CreationDate] = @creationDate
WHERE
  [userID] = @userID

GO

/*************************************************/
/* [dbo].gspUsers_DELETE */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspUsers_DELETE') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspUsers_DELETE
GO

CREATE PROCEDURE [dbo].gspUsers_DELETE
(
@userID int
)
AS

DELETE
  [dbo].[Users]
WHERE
  [userID] = @userID

GO

/*************************************************/
/* [dbo].gspUsers_SEARCH */
/*************************************************/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].gspUsers_SEARCH') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].gspUsers_SEARCH
GO

CREATE PROCEDURE [dbo].gspUsers_SEARCH
(
@userID int = null,
@username varchar(10) = null,
@password varchar(50) = null,
@securitylevelID int = null,
@enabled bit = null,
@nombre varchar(50) = null,
@email varchar(50) = null,
@lastEditDate datetime = null,
@creationDate datetime = null
)
AS

SELECT
  *
FROM
  [dbo].[Users]
WHERE
  (@userID IS NULL OR [userID] = @userID)
AND
  (@username IS NULL OR @username = '' OR [username] LIKE @username + '%')
AND
  (@password IS NULL OR @password = '' OR [password] LIKE @password + '%')
AND
  (@securitylevelID IS NULL OR [securitylevelID] = @securitylevelID)
AND
  (@enabled IS NULL OR [enabled] = @enabled)
AND
  (@nombre IS NULL OR @nombre = '' OR [Nombre] LIKE @nombre + '%')
AND
  (@email IS NULL OR @email = '' OR [email] LIKE @email + '%')
AND
  (@lastEditDate IS NULL OR [LastEditDate] = @lastEditDate)
AND
  (@creationDate IS NULL OR [CreationDate] = @creationDate)

GO

