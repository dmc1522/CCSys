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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

[DataObject]
[Serializable]
public partial class Boletas
{
	
	
	#region Constants
	private static readonly string TABLE_NAME = "[dbo].[Boletas]";
	
	#endregion
	
	
	#region Fields
	private System.Int32? _boletaID;
	private System.Int32? _cicloID;
	private System.Int32? _userID;
	private System.Int32? _productorID;
	private System.Double? _humedad;
	private System.Double? _dctoHumedad;
	private System.Double? _impurezas;
	private System.Decimal? _totaldescuentos;
	private System.Double? _pesonetoapagar;
	private System.Decimal? _precioapagar;
	private System.Decimal? _importe;
	private System.String _placas;
	private System.String _chofer;
	private System.Boolean? _pagada;
	private System.DateTime? _storeTS;
	private System.DateTime? _updateTS;
	private System.Int32? _productoID;
	private System.String _numeroBoleta;
	private System.String _ticket;
	private System.String _codigoClienteProvArchivo;
	private System.String _nombreProductor;
	private System.DateTime? _fechaEntrada;
	private System.String _pesadorEntrada;
	private System.Double? _pesoDeEntrada;
	private System.String _basculaEntrada;
	private System.DateTime? _fechaSalida;
	private System.Double? _pesoDeSalida;
	private System.String _pesadorSalida;
	private System.String _basculaSalida;
	private System.Double? _pesonetoentrada;
	private System.Double? _pesonetosalida;
	private System.Decimal? _dctoImpurezas;
	private System.Decimal? _dctoSecado;
	private System.Decimal? _totalapagar;
	private System.Int32? _bodegaID;
	private System.Boolean? _applyHumedad;
	private System.Boolean? _applyImpurezas;
	private System.Boolean? _applySecado;
	private System.String _folioDestino;
	private System.Double? _pesoDestino;
	private System.Double? _merma;
	private System.Double? _flete;
	private System.Double? _importeFlete;
	private System.Double? _precioNetoDestino;
	private System.Double? _dctoGranoChico;
	private System.Double? _dctoGranoDanado;
	private System.Double? _dctoGranoQuebrado;
	private System.Double? _dctoGranoEstrellado;
	private System.Int32? _transportistaID;
	private System.Int32? _cabezasDeGanado;
	private System.Boolean? _llevaFlete;
	private System.Boolean? _deGranjaACorrales;
	private System.DateTime? _lastEditDate;
	private System.DateTime? _creationDate;
	private System.Decimal? _precioTransportista;
	private System.Boolean? _usaPesoDestinoParaTransportista;
	
	#endregion
	
	
	#region Properties
	public System.Int32? BoletaID
	{
		get
		{
			return _boletaID;
		}
		set
		{
			_boletaID = value;
		}
	}
	
	public System.Int32? CicloID
	{
		get
		{
			return _cicloID;
		}
		set
		{
			_cicloID = value;
		}
	}
	
	public System.Int32? UserID
	{
		get
		{
			return _userID;
		}
		set
		{
			_userID = value;
		}
	}
	
	public System.Int32? ProductorID
	{
		get
		{
			return _productorID;
		}
		set
		{
			_productorID = value;
		}
	}
	
	public System.Double? Humedad
	{
		get
		{
			return _humedad;
		}
		set
		{
			_humedad = value;
		}
	}
	
	public System.Double? DctoHumedad
	{
		get
		{
			return _dctoHumedad;
		}
		set
		{
			_dctoHumedad = value;
		}
	}
	
	public System.Double? Impurezas
	{
		get
		{
			return _impurezas;
		}
		set
		{
			_impurezas = value;
		}
	}
	
	public System.Decimal? Totaldescuentos
	{
		get
		{
			return _totaldescuentos;
		}
		set
		{
			_totaldescuentos = value;
		}
	}
	
	public System.Double? Pesonetoapagar
	{
		get
		{
			return _pesonetoapagar;
		}
		set
		{
			_pesonetoapagar = value;
		}
	}
	
	public System.Decimal? Precioapagar
	{
		get
		{
			return _precioapagar;
		}
		set
		{
			_precioapagar = value;
		}
	}
	
	public System.Decimal? Importe
	{
		get
		{
			return _importe;
		}
		set
		{
			_importe = value;
		}
	}
	
	public System.String Placas
	{
		get
		{
			return _placas;
		}
		set
		{
			_placas = value;
		}
	}
	
	public System.String Chofer
	{
		get
		{
			return _chofer;
		}
		set
		{
			_chofer = value;
		}
	}
	
	public System.Boolean? Pagada
	{
		get
		{
			return _pagada;
		}
		set
		{
			_pagada = value;
		}
	}
	
	public System.DateTime? StoreTS
	{
		get
		{
			return _storeTS;
		}
		set
		{
			_storeTS = value;
		}
	}
	
	public System.DateTime? UpdateTS
	{
		get
		{
			return _updateTS;
		}
		set
		{
			_updateTS = value;
		}
	}
	
	public System.Int32? ProductoID
	{
		get
		{
			return _productoID;
		}
		set
		{
			_productoID = value;
		}
	}
	
	public System.String NumeroBoleta
	{
		get
		{
			return _numeroBoleta;
		}
		set
		{
			_numeroBoleta = value;
		}
	}
	
	public System.String Ticket
	{
		get
		{
			return _ticket;
		}
		set
		{
			_ticket = value;
		}
	}
	
	public System.String CodigoClienteProvArchivo
	{
		get
		{
			return _codigoClienteProvArchivo;
		}
		set
		{
			_codigoClienteProvArchivo = value;
		}
	}
	
	public System.String NombreProductor
	{
		get
		{
			return _nombreProductor;
		}
		set
		{
			_nombreProductor = value;
		}
	}
	
	public System.DateTime? FechaEntrada
	{
		get
		{
			return _fechaEntrada;
		}
		set
		{
			_fechaEntrada = value;
		}
	}
	
	public System.String PesadorEntrada
	{
		get
		{
			return _pesadorEntrada;
		}
		set
		{
			_pesadorEntrada = value;
		}
	}
	
	public System.Double? PesoDeEntrada
	{
		get
		{
			return _pesoDeEntrada;
		}
		set
		{
			_pesoDeEntrada = value;
		}
	}
	
	public System.String BasculaEntrada
	{
		get
		{
			return _basculaEntrada;
		}
		set
		{
			_basculaEntrada = value;
		}
	}
	
	public System.DateTime? FechaSalida
	{
		get
		{
			return _fechaSalida;
		}
		set
		{
			_fechaSalida = value;
		}
	}
	
	public System.Double? PesoDeSalida
	{
		get
		{
			return _pesoDeSalida;
		}
		set
		{
			_pesoDeSalida = value;
		}
	}
	
	public System.String PesadorSalida
	{
		get
		{
			return _pesadorSalida;
		}
		set
		{
			_pesadorSalida = value;
		}
	}
	
	public System.String BasculaSalida
	{
		get
		{
			return _basculaSalida;
		}
		set
		{
			_basculaSalida = value;
		}
	}
	
	public System.Double? Pesonetoentrada
	{
		get
		{
			return _pesonetoentrada;
		}
		set
		{
			_pesonetoentrada = value;
		}
	}
	
	public System.Double? Pesonetosalida
	{
		get
		{
			return _pesonetosalida;
		}
		set
		{
			_pesonetosalida = value;
		}
	}
	
	public System.Decimal? DctoImpurezas
	{
		get
		{
			return _dctoImpurezas;
		}
		set
		{
			_dctoImpurezas = value;
		}
	}
	
	public System.Decimal? DctoSecado
	{
		get
		{
			return _dctoSecado;
		}
		set
		{
			_dctoSecado = value;
		}
	}
	
	public System.Decimal? Totalapagar
	{
		get
		{
			return _totalapagar;
		}
		set
		{
			_totalapagar = value;
		}
	}
	
	public System.Int32? BodegaID
	{
		get
		{
			return _bodegaID;
		}
		set
		{
			_bodegaID = value;
		}
	}
	
	public System.Boolean? ApplyHumedad
	{
		get
		{
			return _applyHumedad;
		}
		set
		{
			_applyHumedad = value;
		}
	}
	
	public System.Boolean? ApplyImpurezas
	{
		get
		{
			return _applyImpurezas;
		}
		set
		{
			_applyImpurezas = value;
		}
	}
	
	public System.Boolean? ApplySecado
	{
		get
		{
			return _applySecado;
		}
		set
		{
			_applySecado = value;
		}
	}
	
	public System.String FolioDestino
	{
		get
		{
			return _folioDestino;
		}
		set
		{
			_folioDestino = value;
		}
	}
	
	public System.Double? PesoDestino
	{
		get
		{
			return _pesoDestino;
		}
		set
		{
			_pesoDestino = value;
		}
	}
	
	public System.Double? Merma
	{
		get
		{
			return _merma;
		}
		set
		{
			_merma = value;
		}
	}
	
	public System.Double? Flete
	{
		get
		{
			return _flete;
		}
		set
		{
			_flete = value;
		}
	}
	
	public System.Double? ImporteFlete
	{
		get
		{
			return _importeFlete;
		}
		set
		{
			_importeFlete = value;
		}
	}
	
	public System.Double? PrecioNetoDestino
	{
		get
		{
			return _precioNetoDestino;
		}
		set
		{
			_precioNetoDestino = value;
		}
	}
	
	public System.Double? DctoGranoChico
	{
		get
		{
			return _dctoGranoChico;
		}
		set
		{
			_dctoGranoChico = value;
		}
	}
	
	public System.Double? DctoGranoDanado
	{
		get
		{
			return _dctoGranoDanado;
		}
		set
		{
			_dctoGranoDanado = value;
		}
	}
	
	public System.Double? DctoGranoQuebrado
	{
		get
		{
			return _dctoGranoQuebrado;
		}
		set
		{
			_dctoGranoQuebrado = value;
		}
	}
	
	public System.Double? DctoGranoEstrellado
	{
		get
		{
			return _dctoGranoEstrellado;
		}
		set
		{
			_dctoGranoEstrellado = value;
		}
	}
	
	public System.Int32? TransportistaID
	{
		get
		{
			return _transportistaID;
		}
		set
		{
			_transportistaID = value;
		}
	}
	
	public System.Int32? CabezasDeGanado
	{
		get
		{
			return _cabezasDeGanado;
		}
		set
		{
			_cabezasDeGanado = value;
		}
	}
	
	public System.Boolean? LlevaFlete
	{
		get
		{
			return _llevaFlete;
		}
		set
		{
			_llevaFlete = value;
		}
	}
	
	public System.Boolean? DeGranjaACorrales
	{
		get
		{
			return _deGranjaACorrales;
		}
		set
		{
			_deGranjaACorrales = value;
		}
	}
	
	public System.DateTime? LastEditDate
	{
		get
		{
			return _lastEditDate;
		}
		set
		{
			_lastEditDate = value;
		}
	}
	
	public System.DateTime? CreationDate
	{
		get
		{
			return _creationDate;
		}
		set
		{
			_creationDate = value;
		}
	}
	
	public System.Decimal? PrecioTransportista
	{
		get
		{
			return _precioTransportista;
		}
		set
		{
			_precioTransportista = value;
		}
	}
	
	public System.Boolean? UsaPesoDestinoParaTransportista
	{
		get
		{
			return _usaPesoDestinoParaTransportista;
		}
		set
		{
			_usaPesoDestinoParaTransportista = value;
		}
	}
	
	#endregion
	
	
	#region Methods
	
	
	#region Mapping Methods
	
	protected void MapTo(DataSet ds)
	{
		DataRow dr;
		
		
		if (ds == null)
		ds = new DataSet();
		
		if (ds.Tables["TABLE_NAME"] == null)
		ds.Tables.Add(TABLE_NAME);
		
		ds.Tables[TABLE_NAME].Columns.Add("boletaID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("cicloID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("userID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("productorID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("humedad", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("dctoHumedad", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("impurezas", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("totaldescuentos", typeof(System.Decimal) );
		ds.Tables[TABLE_NAME].Columns.Add("pesonetoapagar", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("precioapagar", typeof(System.Decimal) );
		ds.Tables[TABLE_NAME].Columns.Add("importe", typeof(System.Decimal) );
		ds.Tables[TABLE_NAME].Columns.Add("placas", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("chofer", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("pagada", typeof(System.Boolean) );
		ds.Tables[TABLE_NAME].Columns.Add("storeTS", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("updateTS", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("productoID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("NumeroBoleta", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("Ticket", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("codigoClienteProvArchivo", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("NombreProductor", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("FechaEntrada", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("PesadorEntrada", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("PesoDeEntrada", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("BasculaEntrada", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("FechaSalida", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("PesoDeSalida", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("PesadorSalida", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("BasculaSalida", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("pesonetoentrada", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("pesonetosalida", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("dctoImpurezas", typeof(System.Decimal) );
		ds.Tables[TABLE_NAME].Columns.Add("dctoSecado", typeof(System.Decimal) );
		ds.Tables[TABLE_NAME].Columns.Add("totalapagar", typeof(System.Decimal) );
		ds.Tables[TABLE_NAME].Columns.Add("bodegaID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("applyHumedad", typeof(System.Boolean) );
		ds.Tables[TABLE_NAME].Columns.Add("applyImpurezas", typeof(System.Boolean) );
		ds.Tables[TABLE_NAME].Columns.Add("applySecado", typeof(System.Boolean) );
		ds.Tables[TABLE_NAME].Columns.Add("FolioDestino", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("PesoDestino", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("Merma", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("Flete", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("ImporteFlete", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("PrecioNetoDestino", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("dctoGranoChico", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("dctoGranoDanado", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("dctoGranoQuebrado", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("dctoGranoEstrellado", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("transportistaID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("cabezasDeGanado", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("llevaFlete", typeof(System.Boolean) );
		ds.Tables[TABLE_NAME].Columns.Add("deGranjaACorrales", typeof(System.Boolean) );
		ds.Tables[TABLE_NAME].Columns.Add("LastEditDate", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("CreationDate", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("PrecioTransportista", typeof(System.Decimal) );
		ds.Tables[TABLE_NAME].Columns.Add("UsaPesoDestinoParaTransportista", typeof(System.Boolean) );
		
		dr = ds.Tables[TABLE_NAME].NewRow();
		
		if (BoletaID == null)
		dr["boletaID"] = DBNull.Value;
		else
		dr["boletaID"] = BoletaID;
		
		if (CicloID == null)
		dr["cicloID"] = DBNull.Value;
		else
		dr["cicloID"] = CicloID;
		
		if (UserID == null)
		dr["userID"] = DBNull.Value;
		else
		dr["userID"] = UserID;
		
		if (ProductorID == null)
		dr["productorID"] = DBNull.Value;
		else
		dr["productorID"] = ProductorID;
		
		if (Humedad == null)
		dr["humedad"] = DBNull.Value;
		else
		dr["humedad"] = Humedad;
		
		if (DctoHumedad == null)
		dr["dctoHumedad"] = DBNull.Value;
		else
		dr["dctoHumedad"] = DctoHumedad;
		
		if (Impurezas == null)
		dr["impurezas"] = DBNull.Value;
		else
		dr["impurezas"] = Impurezas;
		
		if (Totaldescuentos == null)
		dr["totaldescuentos"] = DBNull.Value;
		else
		dr["totaldescuentos"] = Totaldescuentos;
		
		if (Pesonetoapagar == null)
		dr["pesonetoapagar"] = DBNull.Value;
		else
		dr["pesonetoapagar"] = Pesonetoapagar;
		
		if (Precioapagar == null)
		dr["precioapagar"] = DBNull.Value;
		else
		dr["precioapagar"] = Precioapagar;
		
		if (Importe == null)
		dr["importe"] = DBNull.Value;
		else
		dr["importe"] = Importe;
		
		if (Placas == null)
		dr["placas"] = DBNull.Value;
		else
		dr["placas"] = Placas;
		
		if (Chofer == null)
		dr["chofer"] = DBNull.Value;
		else
		dr["chofer"] = Chofer;
		
		if (Pagada == null)
		dr["pagada"] = DBNull.Value;
		else
		dr["pagada"] = Pagada;
		
		if (StoreTS == null)
		dr["storeTS"] = DBNull.Value;
		else
		dr["storeTS"] = StoreTS;
		
		if (UpdateTS == null)
		dr["updateTS"] = DBNull.Value;
		else
		dr["updateTS"] = UpdateTS;
		
		if (ProductoID == null)
		dr["productoID"] = DBNull.Value;
		else
		dr["productoID"] = ProductoID;
		
		if (NumeroBoleta == null)
		dr["NumeroBoleta"] = DBNull.Value;
		else
		dr["NumeroBoleta"] = NumeroBoleta;
		
		if (Ticket == null)
		dr["Ticket"] = DBNull.Value;
		else
		dr["Ticket"] = Ticket;
		
		if (CodigoClienteProvArchivo == null)
		dr["codigoClienteProvArchivo"] = DBNull.Value;
		else
		dr["codigoClienteProvArchivo"] = CodigoClienteProvArchivo;
		
		if (NombreProductor == null)
		dr["NombreProductor"] = DBNull.Value;
		else
		dr["NombreProductor"] = NombreProductor;
		
		if (FechaEntrada == null)
		dr["FechaEntrada"] = DBNull.Value;
		else
		dr["FechaEntrada"] = FechaEntrada;
		
		if (PesadorEntrada == null)
		dr["PesadorEntrada"] = DBNull.Value;
		else
		dr["PesadorEntrada"] = PesadorEntrada;
		
		if (PesoDeEntrada == null)
		dr["PesoDeEntrada"] = DBNull.Value;
		else
		dr["PesoDeEntrada"] = PesoDeEntrada;
		
		if (BasculaEntrada == null)
		dr["BasculaEntrada"] = DBNull.Value;
		else
		dr["BasculaEntrada"] = BasculaEntrada;
		
		if (FechaSalida == null)
		dr["FechaSalida"] = DBNull.Value;
		else
		dr["FechaSalida"] = FechaSalida;
		
		if (PesoDeSalida == null)
		dr["PesoDeSalida"] = DBNull.Value;
		else
		dr["PesoDeSalida"] = PesoDeSalida;
		
		if (PesadorSalida == null)
		dr["PesadorSalida"] = DBNull.Value;
		else
		dr["PesadorSalida"] = PesadorSalida;
		
		if (BasculaSalida == null)
		dr["BasculaSalida"] = DBNull.Value;
		else
		dr["BasculaSalida"] = BasculaSalida;
		
		if (Pesonetoentrada == null)
		dr["pesonetoentrada"] = DBNull.Value;
		else
		dr["pesonetoentrada"] = Pesonetoentrada;
		
		if (Pesonetosalida == null)
		dr["pesonetosalida"] = DBNull.Value;
		else
		dr["pesonetosalida"] = Pesonetosalida;
		
		if (DctoImpurezas == null)
		dr["dctoImpurezas"] = DBNull.Value;
		else
		dr["dctoImpurezas"] = DctoImpurezas;
		
		if (DctoSecado == null)
		dr["dctoSecado"] = DBNull.Value;
		else
		dr["dctoSecado"] = DctoSecado;
		
		if (Totalapagar == null)
		dr["totalapagar"] = DBNull.Value;
		else
		dr["totalapagar"] = Totalapagar;
		
		if (BodegaID == null)
		dr["bodegaID"] = DBNull.Value;
		else
		dr["bodegaID"] = BodegaID;
		
		if (ApplyHumedad == null)
		dr["applyHumedad"] = DBNull.Value;
		else
		dr["applyHumedad"] = ApplyHumedad;
		
		if (ApplyImpurezas == null)
		dr["applyImpurezas"] = DBNull.Value;
		else
		dr["applyImpurezas"] = ApplyImpurezas;
		
		if (ApplySecado == null)
		dr["applySecado"] = DBNull.Value;
		else
		dr["applySecado"] = ApplySecado;
		
		if (FolioDestino == null)
		dr["FolioDestino"] = DBNull.Value;
		else
		dr["FolioDestino"] = FolioDestino;
		
		if (PesoDestino == null)
		dr["PesoDestino"] = DBNull.Value;
		else
		dr["PesoDestino"] = PesoDestino;
		
		if (Merma == null)
		dr["Merma"] = DBNull.Value;
		else
		dr["Merma"] = Merma;
		
		if (Flete == null)
		dr["Flete"] = DBNull.Value;
		else
		dr["Flete"] = Flete;
		
		if (ImporteFlete == null)
		dr["ImporteFlete"] = DBNull.Value;
		else
		dr["ImporteFlete"] = ImporteFlete;
		
		if (PrecioNetoDestino == null)
		dr["PrecioNetoDestino"] = DBNull.Value;
		else
		dr["PrecioNetoDestino"] = PrecioNetoDestino;
		
		if (DctoGranoChico == null)
		dr["dctoGranoChico"] = DBNull.Value;
		else
		dr["dctoGranoChico"] = DctoGranoChico;
		
		if (DctoGranoDanado == null)
		dr["dctoGranoDanado"] = DBNull.Value;
		else
		dr["dctoGranoDanado"] = DctoGranoDanado;
		
		if (DctoGranoQuebrado == null)
		dr["dctoGranoQuebrado"] = DBNull.Value;
		else
		dr["dctoGranoQuebrado"] = DctoGranoQuebrado;
		
		if (DctoGranoEstrellado == null)
		dr["dctoGranoEstrellado"] = DBNull.Value;
		else
		dr["dctoGranoEstrellado"] = DctoGranoEstrellado;
		
		if (TransportistaID == null)
		dr["transportistaID"] = DBNull.Value;
		else
		dr["transportistaID"] = TransportistaID;
		
		if (CabezasDeGanado == null)
		dr["cabezasDeGanado"] = DBNull.Value;
		else
		dr["cabezasDeGanado"] = CabezasDeGanado;
		
		if (LlevaFlete == null)
		dr["llevaFlete"] = DBNull.Value;
		else
		dr["llevaFlete"] = LlevaFlete;
		
		if (DeGranjaACorrales == null)
		dr["deGranjaACorrales"] = DBNull.Value;
		else
		dr["deGranjaACorrales"] = DeGranjaACorrales;
		
		if (LastEditDate == null)
		dr["LastEditDate"] = DBNull.Value;
		else
		dr["LastEditDate"] = LastEditDate;
		
		if (CreationDate == null)
		dr["CreationDate"] = DBNull.Value;
		else
		dr["CreationDate"] = CreationDate;
		
		if (PrecioTransportista == null)
		dr["PrecioTransportista"] = DBNull.Value;
		else
		dr["PrecioTransportista"] = PrecioTransportista;
		
		if (UsaPesoDestinoParaTransportista == null)
		dr["UsaPesoDestinoParaTransportista"] = DBNull.Value;
		else
		dr["UsaPesoDestinoParaTransportista"] = UsaPesoDestinoParaTransportista;
		
		
		ds.Tables[TABLE_NAME].Rows.Add(dr);
		
	}
	
	protected void MapFrom(DataRow dr)
	{
		BoletaID = dr["boletaID"] != DBNull.Value ? Convert.ToInt32(dr["boletaID"]) : BoletaID = null;
		CicloID = dr["cicloID"] != DBNull.Value ? Convert.ToInt32(dr["cicloID"]) : CicloID = null;
		UserID = dr["userID"] != DBNull.Value ? Convert.ToInt32(dr["userID"]) : UserID = null;
		ProductorID = dr["productorID"] != DBNull.Value ? Convert.ToInt32(dr["productorID"]) : ProductorID = null;
		Humedad = dr["humedad"] != DBNull.Value ? Convert.ToDouble(dr["humedad"]) : Humedad = null;
		DctoHumedad = dr["dctoHumedad"] != DBNull.Value ? Convert.ToDouble(dr["dctoHumedad"]) : DctoHumedad = null;
		Impurezas = dr["impurezas"] != DBNull.Value ? Convert.ToDouble(dr["impurezas"]) : Impurezas = null;
		Totaldescuentos = dr["totaldescuentos"] != DBNull.Value ? Convert.ToDecimal(dr["totaldescuentos"]) : Totaldescuentos = null;
		Pesonetoapagar = dr["pesonetoapagar"] != DBNull.Value ? Convert.ToDouble(dr["pesonetoapagar"]) : Pesonetoapagar = null;
		Precioapagar = dr["precioapagar"] != DBNull.Value ? Convert.ToDecimal(dr["precioapagar"]) : Precioapagar = null;
		Importe = dr["importe"] != DBNull.Value ? Convert.ToDecimal(dr["importe"]) : Importe = null;
		Placas = dr["placas"] != DBNull.Value ? Convert.ToString(dr["placas"]) : Placas = null;
		Chofer = dr["chofer"] != DBNull.Value ? Convert.ToString(dr["chofer"]) : Chofer = null;
		Pagada = dr["pagada"] != DBNull.Value ? Convert.ToBoolean(dr["pagada"]) : Pagada = null;
		StoreTS = dr["storeTS"] != DBNull.Value ? Convert.ToDateTime(dr["storeTS"]) : StoreTS = null;
		UpdateTS = dr["updateTS"] != DBNull.Value ? Convert.ToDateTime(dr["updateTS"]) : UpdateTS = null;
		ProductoID = dr["productoID"] != DBNull.Value ? Convert.ToInt32(dr["productoID"]) : ProductoID = null;
		NumeroBoleta = dr["NumeroBoleta"] != DBNull.Value ? Convert.ToString(dr["NumeroBoleta"]) : NumeroBoleta = null;
		Ticket = dr["Ticket"] != DBNull.Value ? Convert.ToString(dr["Ticket"]) : Ticket = null;
		CodigoClienteProvArchivo = dr["codigoClienteProvArchivo"] != DBNull.Value ? Convert.ToString(dr["codigoClienteProvArchivo"]) : CodigoClienteProvArchivo = null;
		NombreProductor = dr["NombreProductor"] != DBNull.Value ? Convert.ToString(dr["NombreProductor"]) : NombreProductor = null;
		FechaEntrada = dr["FechaEntrada"] != DBNull.Value ? Convert.ToDateTime(dr["FechaEntrada"]) : FechaEntrada = null;
		PesadorEntrada = dr["PesadorEntrada"] != DBNull.Value ? Convert.ToString(dr["PesadorEntrada"]) : PesadorEntrada = null;
		PesoDeEntrada = dr["PesoDeEntrada"] != DBNull.Value ? Convert.ToDouble(dr["PesoDeEntrada"]) : PesoDeEntrada = null;
		BasculaEntrada = dr["BasculaEntrada"] != DBNull.Value ? Convert.ToString(dr["BasculaEntrada"]) : BasculaEntrada = null;
		FechaSalida = dr["FechaSalida"] != DBNull.Value ? Convert.ToDateTime(dr["FechaSalida"]) : FechaSalida = null;
		PesoDeSalida = dr["PesoDeSalida"] != DBNull.Value ? Convert.ToDouble(dr["PesoDeSalida"]) : PesoDeSalida = null;
		PesadorSalida = dr["PesadorSalida"] != DBNull.Value ? Convert.ToString(dr["PesadorSalida"]) : PesadorSalida = null;
		BasculaSalida = dr["BasculaSalida"] != DBNull.Value ? Convert.ToString(dr["BasculaSalida"]) : BasculaSalida = null;
		Pesonetoentrada = dr["pesonetoentrada"] != DBNull.Value ? Convert.ToDouble(dr["pesonetoentrada"]) : Pesonetoentrada = null;
		Pesonetosalida = dr["pesonetosalida"] != DBNull.Value ? Convert.ToDouble(dr["pesonetosalida"]) : Pesonetosalida = null;
		DctoImpurezas = dr["dctoImpurezas"] != DBNull.Value ? Convert.ToDecimal(dr["dctoImpurezas"]) : DctoImpurezas = null;
		DctoSecado = dr["dctoSecado"] != DBNull.Value ? Convert.ToDecimal(dr["dctoSecado"]) : DctoSecado = null;
		Totalapagar = dr["totalapagar"] != DBNull.Value ? Convert.ToDecimal(dr["totalapagar"]) : Totalapagar = null;
		BodegaID = dr["bodegaID"] != DBNull.Value ? Convert.ToInt32(dr["bodegaID"]) : BodegaID = null;
		ApplyHumedad = dr["applyHumedad"] != DBNull.Value ? Convert.ToBoolean(dr["applyHumedad"]) : ApplyHumedad = null;
		ApplyImpurezas = dr["applyImpurezas"] != DBNull.Value ? Convert.ToBoolean(dr["applyImpurezas"]) : ApplyImpurezas = null;
		ApplySecado = dr["applySecado"] != DBNull.Value ? Convert.ToBoolean(dr["applySecado"]) : ApplySecado = null;
		FolioDestino = dr["FolioDestino"] != DBNull.Value ? Convert.ToString(dr["FolioDestino"]) : FolioDestino = null;
		PesoDestino = dr["PesoDestino"] != DBNull.Value ? Convert.ToDouble(dr["PesoDestino"]) : PesoDestino = null;
		Merma = dr["Merma"] != DBNull.Value ? Convert.ToDouble(dr["Merma"]) : Merma = null;
		Flete = dr["Flete"] != DBNull.Value ? Convert.ToDouble(dr["Flete"]) : Flete = null;
		ImporteFlete = dr["ImporteFlete"] != DBNull.Value ? Convert.ToDouble(dr["ImporteFlete"]) : ImporteFlete = null;
		PrecioNetoDestino = dr["PrecioNetoDestino"] != DBNull.Value ? Convert.ToDouble(dr["PrecioNetoDestino"]) : PrecioNetoDestino = null;
		DctoGranoChico = dr["dctoGranoChico"] != DBNull.Value ? Convert.ToDouble(dr["dctoGranoChico"]) : DctoGranoChico = null;
		DctoGranoDanado = dr["dctoGranoDanado"] != DBNull.Value ? Convert.ToDouble(dr["dctoGranoDanado"]) : DctoGranoDanado = null;
		DctoGranoQuebrado = dr["dctoGranoQuebrado"] != DBNull.Value ? Convert.ToDouble(dr["dctoGranoQuebrado"]) : DctoGranoQuebrado = null;
		DctoGranoEstrellado = dr["dctoGranoEstrellado"] != DBNull.Value ? Convert.ToDouble(dr["dctoGranoEstrellado"]) : DctoGranoEstrellado = null;
		TransportistaID = dr["transportistaID"] != DBNull.Value ? Convert.ToInt32(dr["transportistaID"]) : TransportistaID = null;
		CabezasDeGanado = dr["cabezasDeGanado"] != DBNull.Value ? Convert.ToInt32(dr["cabezasDeGanado"]) : CabezasDeGanado = null;
		LlevaFlete = dr["llevaFlete"] != DBNull.Value ? Convert.ToBoolean(dr["llevaFlete"]) : LlevaFlete = null;
		DeGranjaACorrales = dr["deGranjaACorrales"] != DBNull.Value ? Convert.ToBoolean(dr["deGranjaACorrales"]) : DeGranjaACorrales = null;
		LastEditDate = dr["LastEditDate"] != DBNull.Value ? Convert.ToDateTime(dr["LastEditDate"]) : LastEditDate = null;
		CreationDate = dr["CreationDate"] != DBNull.Value ? Convert.ToDateTime(dr["CreationDate"]) : CreationDate = null;
		PrecioTransportista = dr["PrecioTransportista"] != DBNull.Value ? Convert.ToDecimal(dr["PrecioTransportista"]) : PrecioTransportista = null;
		UsaPesoDestinoParaTransportista = dr["UsaPesoDestinoParaTransportista"] != DBNull.Value ? Convert.ToBoolean(dr["UsaPesoDestinoParaTransportista"]) : UsaPesoDestinoParaTransportista = null;
	}
	
	public static Boletas[] MapFrom(DataSet ds)
	{
		List<Boletas> objects;
		
		
		// Initialise Collection.
		objects = new List<Boletas>();
		
		// Validation.
		if (ds == null)
		throw new ApplicationException("Cannot map to dataset null.");
		else if (ds.Tables[TABLE_NAME].Rows.Count == 0)
		return objects.ToArray();
		
		if (ds.Tables[TABLE_NAME] == null)
		throw new ApplicationException("Cannot find table [dbo].[Boletas] in DataSet.");
		
		if (ds.Tables[TABLE_NAME].Rows.Count < 1)
		throw new ApplicationException("Table [dbo].[Boletas] is empty.");
		
		// Map DataSet to Instance.
		foreach (DataRow dr in ds.Tables[TABLE_NAME].Rows)
		{
			Boletas instance = new Boletas();
			instance.MapFrom(dr);
			objects.Add(instance);
		}
		
		// Return collection.
		return objects.ToArray();
	}
	
	
	#endregion
	
	
	#region CRUD Methods
	
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static Boletas Get(System.Int32 boletaID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		Boletas instance;
		
		
		instance = new Boletas();
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspBoletas_SELECT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, boletaID);
		
		// Get results.
		ds = db.ExecuteDataSet(dbCommand);
		// Verification.
		if (ds == null || ds.Tables[0].Rows.Count == 0) throw new ApplicationException("Could not get Boletas ID:" + boletaID.ToString()+ " from Database.");
		// Return results.
		ds.Tables[0].TableName = TABLE_NAME;
		
		instance.MapFrom( ds.Tables[0].Rows[0] );
		return instance;
	}
	
	#region INSERT
	public void Insert(System.Int32? cicloID, System.Int32? userID, System.Int32? productorID, System.Double? humedad, System.Double? dctoHumedad, System.Double? impurezas, System.Decimal? totaldescuentos, System.Double? pesonetoapagar, System.Decimal? precioapagar, System.Decimal? importe, System.String placas, System.String chofer, System.Boolean? pagada, System.DateTime? storeTS, System.DateTime? updateTS, System.Int32? productoID, System.String numeroBoleta, System.String ticket, System.String codigoClienteProvArchivo, System.String nombreProductor, System.DateTime? fechaEntrada, System.String pesadorEntrada, System.Double? pesoDeEntrada, System.String basculaEntrada, System.DateTime? fechaSalida, System.Double? pesoDeSalida, System.String pesadorSalida, System.String basculaSalida, System.Double? pesonetoentrada, System.Double? pesonetosalida, System.Decimal? dctoImpurezas, System.Decimal? dctoSecado, System.Decimal? totalapagar, System.Int32? bodegaID, System.Boolean? applyHumedad, System.Boolean? applyImpurezas, System.Boolean? applySecado, System.String folioDestino, System.Double? pesoDestino, System.Double? merma, System.Double? flete, System.Double? importeFlete, System.Double? precioNetoDestino, System.Double? dctoGranoChico, System.Double? dctoGranoDanado, System.Double? dctoGranoQuebrado, System.Double? dctoGranoEstrellado, System.Int32? transportistaID, System.Int32? cabezasDeGanado, System.Boolean? llevaFlete, System.Boolean? deGranjaACorrales, System.DateTime? lastEditDate, System.DateTime? creationDate, System.Decimal? precioTransportista, System.Boolean? usaPesoDestinoParaTransportista, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspBoletas_INSERT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, cicloID, userID, productorID, humedad, dctoHumedad, impurezas, totaldescuentos, pesonetoapagar, precioapagar, importe, placas, chofer, pagada, storeTS, updateTS, productoID, numeroBoleta, ticket, codigoClienteProvArchivo, nombreProductor, fechaEntrada, pesadorEntrada, pesoDeEntrada, basculaEntrada, fechaSalida, pesoDeSalida, pesadorSalida, basculaSalida, pesonetoentrada, pesonetosalida, dctoImpurezas, dctoSecado, totalapagar, bodegaID, applyHumedad, applyImpurezas, applySecado, folioDestino, pesoDestino, merma, flete, importeFlete, precioNetoDestino, dctoGranoChico, dctoGranoDanado, dctoGranoQuebrado, dctoGranoEstrellado, transportistaID, cabezasDeGanado, llevaFlete, deGranjaACorrales, lastEditDate, creationDate, precioTransportista, usaPesoDestinoParaTransportista);
		
		if (transaction == null)
		this.BoletaID = Convert.ToInt32(db.ExecuteScalar(dbCommand));
		else
		this.BoletaID = Convert.ToInt32(db.ExecuteScalar(dbCommand, transaction));
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
	public void Insert(System.Int32? cicloID, System.Int32? userID, System.Int32? productorID, System.Double? humedad, System.Double? dctoHumedad, System.Double? impurezas, System.Decimal? totaldescuentos, System.Double? pesonetoapagar, System.Decimal? precioapagar, System.Decimal? importe, System.String placas, System.String chofer, System.Boolean? pagada, System.DateTime? storeTS, System.DateTime? updateTS, System.Int32? productoID, System.String numeroBoleta, System.String ticket, System.String codigoClienteProvArchivo, System.String nombreProductor, System.DateTime? fechaEntrada, System.String pesadorEntrada, System.Double? pesoDeEntrada, System.String basculaEntrada, System.DateTime? fechaSalida, System.Double? pesoDeSalida, System.String pesadorSalida, System.String basculaSalida, System.Double? pesonetoentrada, System.Double? pesonetosalida, System.Decimal? dctoImpurezas, System.Decimal? dctoSecado, System.Decimal? totalapagar, System.Int32? bodegaID, System.Boolean? applyHumedad, System.Boolean? applyImpurezas, System.Boolean? applySecado, System.String folioDestino, System.Double? pesoDestino, System.Double? merma, System.Double? flete, System.Double? importeFlete, System.Double? precioNetoDestino, System.Double? dctoGranoChico, System.Double? dctoGranoDanado, System.Double? dctoGranoQuebrado, System.Double? dctoGranoEstrellado, System.Int32? transportistaID, System.Int32? cabezasDeGanado, System.Boolean? llevaFlete, System.Boolean? deGranjaACorrales, System.DateTime? lastEditDate, System.DateTime? creationDate, System.Decimal? precioTransportista, System.Boolean? usaPesoDestinoParaTransportista)
	{
		Insert(cicloID, userID, productorID, humedad, dctoHumedad, impurezas, totaldescuentos, pesonetoapagar, precioapagar, importe, placas, chofer, pagada, storeTS, updateTS, productoID, numeroBoleta, ticket, codigoClienteProvArchivo, nombreProductor, fechaEntrada, pesadorEntrada, pesoDeEntrada, basculaEntrada, fechaSalida, pesoDeSalida, pesadorSalida, basculaSalida, pesonetoentrada, pesonetosalida, dctoImpurezas, dctoSecado, totalapagar, bodegaID, applyHumedad, applyImpurezas, applySecado, folioDestino, pesoDestino, merma, flete, importeFlete, precioNetoDestino, dctoGranoChico, dctoGranoDanado, dctoGranoQuebrado, dctoGranoEstrellado, transportistaID, cabezasDeGanado, llevaFlete, deGranjaACorrales, lastEditDate, creationDate, precioTransportista, usaPesoDestinoParaTransportista, null);
	}
	/// <summary>
	/// Insert current Boletas to database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Insert(DbTransaction transaction)
	{
		Insert(CicloID, UserID, ProductorID, Humedad, DctoHumedad, Impurezas, Totaldescuentos, Pesonetoapagar, Precioapagar, Importe, Placas, Chofer, Pagada, StoreTS, UpdateTS, ProductoID, NumeroBoleta, Ticket, CodigoClienteProvArchivo, NombreProductor, FechaEntrada, PesadorEntrada, PesoDeEntrada, BasculaEntrada, FechaSalida, PesoDeSalida, PesadorSalida, BasculaSalida, Pesonetoentrada, Pesonetosalida, DctoImpurezas, DctoSecado, Totalapagar, BodegaID, ApplyHumedad, ApplyImpurezas, ApplySecado, FolioDestino, PesoDestino, Merma, Flete, ImporteFlete, PrecioNetoDestino, DctoGranoChico, DctoGranoDanado, DctoGranoQuebrado, DctoGranoEstrellado, TransportistaID, CabezasDeGanado, LlevaFlete, DeGranjaACorrales, LastEditDate, CreationDate, PrecioTransportista, UsaPesoDestinoParaTransportista, transaction);
	}
	
	/// <summary>
	/// Insert current Boletas to database.
	/// </summary>
	public void Insert()
	{
		this.Insert((DbTransaction)null);
	}
	#endregion
	
	
	#region UPDATE
	public static void Update(System.Int32? boletaID, System.Int32? cicloID, System.Int32? userID, System.Int32? productorID, System.Double? humedad, System.Double? dctoHumedad, System.Double? impurezas, System.Decimal? totaldescuentos, System.Double? pesonetoapagar, System.Decimal? precioapagar, System.Decimal? importe, System.String placas, System.String chofer, System.Boolean? pagada, System.DateTime? storeTS, System.DateTime? updateTS, System.Int32? productoID, System.String numeroBoleta, System.String ticket, System.String codigoClienteProvArchivo, System.String nombreProductor, System.DateTime? fechaEntrada, System.String pesadorEntrada, System.Double? pesoDeEntrada, System.String basculaEntrada, System.DateTime? fechaSalida, System.Double? pesoDeSalida, System.String pesadorSalida, System.String basculaSalida, System.Double? pesonetoentrada, System.Double? pesonetosalida, System.Decimal? dctoImpurezas, System.Decimal? dctoSecado, System.Decimal? totalapagar, System.Int32? bodegaID, System.Boolean? applyHumedad, System.Boolean? applyImpurezas, System.Boolean? applySecado, System.String folioDestino, System.Double? pesoDestino, System.Double? merma, System.Double? flete, System.Double? importeFlete, System.Double? precioNetoDestino, System.Double? dctoGranoChico, System.Double? dctoGranoDanado, System.Double? dctoGranoQuebrado, System.Double? dctoGranoEstrellado, System.Int32? transportistaID, System.Int32? cabezasDeGanado, System.Boolean? llevaFlete, System.Boolean? deGranjaACorrales, System.DateTime? lastEditDate, System.DateTime? creationDate, System.Decimal? precioTransportista, System.Boolean? usaPesoDestinoParaTransportista, DbTransaction transaction)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspBoletas_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@boletaID"].Value = boletaID;
		dbCommand.Parameters["@cicloID"].Value = cicloID;
		dbCommand.Parameters["@userID"].Value = userID;
		dbCommand.Parameters["@productorID"].Value = productorID;
		dbCommand.Parameters["@humedad"].Value = humedad;
		dbCommand.Parameters["@dctoHumedad"].Value = dctoHumedad;
		dbCommand.Parameters["@impurezas"].Value = impurezas;
		dbCommand.Parameters["@totaldescuentos"].Value = totaldescuentos;
		dbCommand.Parameters["@pesonetoapagar"].Value = pesonetoapagar;
		dbCommand.Parameters["@precioapagar"].Value = precioapagar;
		dbCommand.Parameters["@importe"].Value = importe;
		dbCommand.Parameters["@placas"].Value = placas;
		dbCommand.Parameters["@chofer"].Value = chofer;
		dbCommand.Parameters["@pagada"].Value = pagada;
		dbCommand.Parameters["@storeTS"].Value = storeTS;
		dbCommand.Parameters["@updateTS"].Value = updateTS;
		dbCommand.Parameters["@productoID"].Value = productoID;
		dbCommand.Parameters["@numeroBoleta"].Value = numeroBoleta;
		dbCommand.Parameters["@ticket"].Value = ticket;
		dbCommand.Parameters["@codigoClienteProvArchivo"].Value = codigoClienteProvArchivo;
		dbCommand.Parameters["@nombreProductor"].Value = nombreProductor;
		dbCommand.Parameters["@fechaEntrada"].Value = fechaEntrada;
		dbCommand.Parameters["@pesadorEntrada"].Value = pesadorEntrada;
		dbCommand.Parameters["@pesoDeEntrada"].Value = pesoDeEntrada;
		dbCommand.Parameters["@basculaEntrada"].Value = basculaEntrada;
		dbCommand.Parameters["@fechaSalida"].Value = fechaSalida;
		dbCommand.Parameters["@pesoDeSalida"].Value = pesoDeSalida;
		dbCommand.Parameters["@pesadorSalida"].Value = pesadorSalida;
		dbCommand.Parameters["@basculaSalida"].Value = basculaSalida;
		dbCommand.Parameters["@pesonetoentrada"].Value = pesonetoentrada;
		dbCommand.Parameters["@pesonetosalida"].Value = pesonetosalida;
		dbCommand.Parameters["@dctoImpurezas"].Value = dctoImpurezas;
		dbCommand.Parameters["@dctoSecado"].Value = dctoSecado;
		dbCommand.Parameters["@totalapagar"].Value = totalapagar;
		dbCommand.Parameters["@bodegaID"].Value = bodegaID;
		dbCommand.Parameters["@applyHumedad"].Value = applyHumedad;
		dbCommand.Parameters["@applyImpurezas"].Value = applyImpurezas;
		dbCommand.Parameters["@applySecado"].Value = applySecado;
		dbCommand.Parameters["@folioDestino"].Value = folioDestino;
		dbCommand.Parameters["@pesoDestino"].Value = pesoDestino;
		dbCommand.Parameters["@merma"].Value = merma;
		dbCommand.Parameters["@flete"].Value = flete;
		dbCommand.Parameters["@importeFlete"].Value = importeFlete;
		dbCommand.Parameters["@precioNetoDestino"].Value = precioNetoDestino;
		dbCommand.Parameters["@dctoGranoChico"].Value = dctoGranoChico;
		dbCommand.Parameters["@dctoGranoDanado"].Value = dctoGranoDanado;
		dbCommand.Parameters["@dctoGranoQuebrado"].Value = dctoGranoQuebrado;
		dbCommand.Parameters["@dctoGranoEstrellado"].Value = dctoGranoEstrellado;
		dbCommand.Parameters["@transportistaID"].Value = transportistaID;
		dbCommand.Parameters["@cabezasDeGanado"].Value = cabezasDeGanado;
		dbCommand.Parameters["@llevaFlete"].Value = llevaFlete;
		dbCommand.Parameters["@deGranjaACorrales"].Value = deGranjaACorrales;
		dbCommand.Parameters["@lastEditDate"].Value = lastEditDate;
		dbCommand.Parameters["@creationDate"].Value = creationDate;
		dbCommand.Parameters["@precioTransportista"].Value = precioTransportista;
		dbCommand.Parameters["@usaPesoDestinoParaTransportista"].Value = usaPesoDestinoParaTransportista;
		
		if (transaction == null)
		db.ExecuteNonQuery(dbCommand);
		else
		db.ExecuteNonQuery(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
	public static void Update(System.Int32? boletaID, System.Int32? cicloID, System.Int32? userID, System.Int32? productorID, System.Double? humedad, System.Double? dctoHumedad, System.Double? impurezas, System.Decimal? totaldescuentos, System.Double? pesonetoapagar, System.Decimal? precioapagar, System.Decimal? importe, System.String placas, System.String chofer, System.Boolean? pagada, System.DateTime? storeTS, System.DateTime? updateTS, System.Int32? productoID, System.String numeroBoleta, System.String ticket, System.String codigoClienteProvArchivo, System.String nombreProductor, System.DateTime? fechaEntrada, System.String pesadorEntrada, System.Double? pesoDeEntrada, System.String basculaEntrada, System.DateTime? fechaSalida, System.Double? pesoDeSalida, System.String pesadorSalida, System.String basculaSalida, System.Double? pesonetoentrada, System.Double? pesonetosalida, System.Decimal? dctoImpurezas, System.Decimal? dctoSecado, System.Decimal? totalapagar, System.Int32? bodegaID, System.Boolean? applyHumedad, System.Boolean? applyImpurezas, System.Boolean? applySecado, System.String folioDestino, System.Double? pesoDestino, System.Double? merma, System.Double? flete, System.Double? importeFlete, System.Double? precioNetoDestino, System.Double? dctoGranoChico, System.Double? dctoGranoDanado, System.Double? dctoGranoQuebrado, System.Double? dctoGranoEstrellado, System.Int32? transportistaID, System.Int32? cabezasDeGanado, System.Boolean? llevaFlete, System.Boolean? deGranjaACorrales, System.DateTime? lastEditDate, System.DateTime? creationDate, System.Decimal? precioTransportista, System.Boolean? usaPesoDestinoParaTransportista)
	{
		Update(boletaID, cicloID, userID, productorID, humedad, dctoHumedad, impurezas, totaldescuentos, pesonetoapagar, precioapagar, importe, placas, chofer, pagada, storeTS, updateTS, productoID, numeroBoleta, ticket, codigoClienteProvArchivo, nombreProductor, fechaEntrada, pesadorEntrada, pesoDeEntrada, basculaEntrada, fechaSalida, pesoDeSalida, pesadorSalida, basculaSalida, pesonetoentrada, pesonetosalida, dctoImpurezas, dctoSecado, totalapagar, bodegaID, applyHumedad, applyImpurezas, applySecado, folioDestino, pesoDestino, merma, flete, importeFlete, precioNetoDestino, dctoGranoChico, dctoGranoDanado, dctoGranoQuebrado, dctoGranoEstrellado, transportistaID, cabezasDeGanado, llevaFlete, deGranjaACorrales, lastEditDate, creationDate, precioTransportista, usaPesoDestinoParaTransportista, null);
	}
	
	public static void Update(Boletas boletas)
	{
		boletas.Update();
	}
	
	public static void Update(Boletas boletas, DbTransaction transaction)
	{
		boletas.Update(transaction);
	}
	
	/// <summary>
	/// Updates changes to the database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Update(DbTransaction transaction)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspBoletas_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@boletaID"].SourceColumn = "boletaID";
		dbCommand.Parameters["@cicloID"].SourceColumn = "cicloID";
		dbCommand.Parameters["@userID"].SourceColumn = "userID";
		dbCommand.Parameters["@productorID"].SourceColumn = "productorID";
		dbCommand.Parameters["@humedad"].SourceColumn = "humedad";
		dbCommand.Parameters["@dctoHumedad"].SourceColumn = "dctoHumedad";
		dbCommand.Parameters["@impurezas"].SourceColumn = "impurezas";
		dbCommand.Parameters["@totaldescuentos"].SourceColumn = "totaldescuentos";
		dbCommand.Parameters["@pesonetoapagar"].SourceColumn = "pesonetoapagar";
		dbCommand.Parameters["@precioapagar"].SourceColumn = "precioapagar";
		dbCommand.Parameters["@importe"].SourceColumn = "importe";
		dbCommand.Parameters["@placas"].SourceColumn = "placas";
		dbCommand.Parameters["@chofer"].SourceColumn = "chofer";
		dbCommand.Parameters["@pagada"].SourceColumn = "pagada";
		dbCommand.Parameters["@storeTS"].SourceColumn = "storeTS";
		dbCommand.Parameters["@updateTS"].SourceColumn = "updateTS";
		dbCommand.Parameters["@productoID"].SourceColumn = "productoID";
		dbCommand.Parameters["@numeroBoleta"].SourceColumn = "NumeroBoleta";
		dbCommand.Parameters["@ticket"].SourceColumn = "Ticket";
		dbCommand.Parameters["@codigoClienteProvArchivo"].SourceColumn = "codigoClienteProvArchivo";
		dbCommand.Parameters["@nombreProductor"].SourceColumn = "NombreProductor";
		dbCommand.Parameters["@fechaEntrada"].SourceColumn = "FechaEntrada";
		dbCommand.Parameters["@pesadorEntrada"].SourceColumn = "PesadorEntrada";
		dbCommand.Parameters["@pesoDeEntrada"].SourceColumn = "PesoDeEntrada";
		dbCommand.Parameters["@basculaEntrada"].SourceColumn = "BasculaEntrada";
		dbCommand.Parameters["@fechaSalida"].SourceColumn = "FechaSalida";
		dbCommand.Parameters["@pesoDeSalida"].SourceColumn = "PesoDeSalida";
		dbCommand.Parameters["@pesadorSalida"].SourceColumn = "PesadorSalida";
		dbCommand.Parameters["@basculaSalida"].SourceColumn = "BasculaSalida";
		dbCommand.Parameters["@pesonetoentrada"].SourceColumn = "pesonetoentrada";
		dbCommand.Parameters["@pesonetosalida"].SourceColumn = "pesonetosalida";
		dbCommand.Parameters["@dctoImpurezas"].SourceColumn = "dctoImpurezas";
		dbCommand.Parameters["@dctoSecado"].SourceColumn = "dctoSecado";
		dbCommand.Parameters["@totalapagar"].SourceColumn = "totalapagar";
		dbCommand.Parameters["@bodegaID"].SourceColumn = "bodegaID";
		dbCommand.Parameters["@applyHumedad"].SourceColumn = "applyHumedad";
		dbCommand.Parameters["@applyImpurezas"].SourceColumn = "applyImpurezas";
		dbCommand.Parameters["@applySecado"].SourceColumn = "applySecado";
		dbCommand.Parameters["@folioDestino"].SourceColumn = "FolioDestino";
		dbCommand.Parameters["@pesoDestino"].SourceColumn = "PesoDestino";
		dbCommand.Parameters["@merma"].SourceColumn = "Merma";
		dbCommand.Parameters["@flete"].SourceColumn = "Flete";
		dbCommand.Parameters["@importeFlete"].SourceColumn = "ImporteFlete";
		dbCommand.Parameters["@precioNetoDestino"].SourceColumn = "PrecioNetoDestino";
		dbCommand.Parameters["@dctoGranoChico"].SourceColumn = "dctoGranoChico";
		dbCommand.Parameters["@dctoGranoDanado"].SourceColumn = "dctoGranoDanado";
		dbCommand.Parameters["@dctoGranoQuebrado"].SourceColumn = "dctoGranoQuebrado";
		dbCommand.Parameters["@dctoGranoEstrellado"].SourceColumn = "dctoGranoEstrellado";
		dbCommand.Parameters["@transportistaID"].SourceColumn = "transportistaID";
		dbCommand.Parameters["@cabezasDeGanado"].SourceColumn = "cabezasDeGanado";
		dbCommand.Parameters["@llevaFlete"].SourceColumn = "llevaFlete";
		dbCommand.Parameters["@deGranjaACorrales"].SourceColumn = "deGranjaACorrales";
		dbCommand.Parameters["@lastEditDate"].SourceColumn = "LastEditDate";
		dbCommand.Parameters["@creationDate"].SourceColumn = "CreationDate";
		dbCommand.Parameters["@precioTransportista"].SourceColumn = "PrecioTransportista";
		dbCommand.Parameters["@usaPesoDestinoParaTransportista"].SourceColumn = "UsaPesoDestinoParaTransportista";
		
		ds = new DataSet();
		this.MapTo( ds );
		ds.AcceptChanges();
		ds.Tables[0].Rows[0].SetModified();
		if (transaction == null)
		db.UpdateDataSet(ds, TABLE_NAME, null, dbCommand, null, UpdateBehavior.Standard);
		else
		db.UpdateDataSet(ds, TABLE_NAME, null, dbCommand, null, transaction);
		return;
	}
	
	/// <summary>
	/// Updates changes to the database.
	/// </summary>
	public void Update()
	{
		this.Update((DbTransaction)null);
	}
	#endregion
	
	
	#region DELETE
	[DataObjectMethodAttribute(DataObjectMethodType.Delete, false)]
	public static void Delete(System.Int32? boletaID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspBoletas_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, boletaID);
		
		// Execute.
		if (transaction != null)
		{
			db.ExecuteNonQuery(dbCommand, transaction);
		}
		else
		{
			db.ExecuteNonQuery(dbCommand);
		}
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
	public static void Delete(System.Int32? boletaID)
	{
		Delete(
		boletaID);
	}
	
	/// <summary>
	/// Delete current Boletas from database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Delete(DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspBoletas_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, BoletaID);
		
		// Execute.
		if (transaction != null)
		{
			db.ExecuteNonQuery(dbCommand, transaction);
		}
		else
		{
			db.ExecuteNonQuery(dbCommand);
		}
		this.BoletaID = null;
	}
	
	/// <summary>
	/// Delete current Boletas from database.
	/// </summary>
	public void Delete()
	{
		this.Delete((DbTransaction)null);
	}
	
	#endregion
	
	
	#region SEARCH
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static Boletas[] Search(System.Int32? boletaID, System.Int32? cicloID, System.Int32? userID, System.Int32? productorID, System.Double? humedad, System.Double? dctoHumedad, System.Double? impurezas, System.Decimal? totaldescuentos, System.Double? pesonetoapagar, System.Decimal? precioapagar, System.Decimal? importe, System.String placas, System.String chofer, System.Boolean? pagada, System.DateTime? storeTS, System.DateTime? updateTS, System.Int32? productoID, System.String numeroBoleta, System.String ticket, System.String codigoClienteProvArchivo, System.String nombreProductor, System.DateTime? fechaEntrada, System.String pesadorEntrada, System.Double? pesoDeEntrada, System.String basculaEntrada, System.DateTime? fechaSalida, System.Double? pesoDeSalida, System.String pesadorSalida, System.String basculaSalida, System.Double? pesonetoentrada, System.Double? pesonetosalida, System.Decimal? dctoImpurezas, System.Decimal? dctoSecado, System.Decimal? totalapagar, System.Int32? bodegaID, System.Boolean? applyHumedad, System.Boolean? applyImpurezas, System.Boolean? applySecado, System.String folioDestino, System.Double? pesoDestino, System.Double? merma, System.Double? flete, System.Double? importeFlete, System.Double? precioNetoDestino, System.Double? dctoGranoChico, System.Double? dctoGranoDanado, System.Double? dctoGranoQuebrado, System.Double? dctoGranoEstrellado, System.Int32? transportistaID, System.Int32? cabezasDeGanado, System.Boolean? llevaFlete, System.Boolean? deGranjaACorrales, System.DateTime? lastEditDate, System.DateTime? creationDate, System.Decimal? precioTransportista, System.Boolean? usaPesoDestinoParaTransportista)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspBoletas_SEARCH";
		dbCommand = db.GetStoredProcCommand(sqlCommand, boletaID, cicloID, userID, productorID, humedad, dctoHumedad, impurezas, totaldescuentos, pesonetoapagar, precioapagar, importe, placas, chofer, pagada, storeTS, updateTS, productoID, numeroBoleta, ticket, codigoClienteProvArchivo, nombreProductor, fechaEntrada, pesadorEntrada, pesoDeEntrada, basculaEntrada, fechaSalida, pesoDeSalida, pesadorSalida, basculaSalida, pesonetoentrada, pesonetosalida, dctoImpurezas, dctoSecado, totalapagar, bodegaID, applyHumedad, applyImpurezas, applySecado, folioDestino, pesoDestino, merma, flete, importeFlete, precioNetoDestino, dctoGranoChico, dctoGranoDanado, dctoGranoQuebrado, dctoGranoEstrellado, transportistaID, cabezasDeGanado, llevaFlete, deGranjaACorrales, lastEditDate, creationDate, precioTransportista, usaPesoDestinoParaTransportista);
		
		ds = db.ExecuteDataSet(dbCommand);
		ds.Tables[0].TableName = TABLE_NAME;
		return Boletas.MapFrom(ds);
	}
	
	
	public static Boletas[] Search(Boletas searchObject)
	{
		return Search ( searchObject.BoletaID, searchObject.CicloID, searchObject.UserID, searchObject.ProductorID, searchObject.Humedad, searchObject.DctoHumedad, searchObject.Impurezas, searchObject.Totaldescuentos, searchObject.Pesonetoapagar, searchObject.Precioapagar, searchObject.Importe, searchObject.Placas, searchObject.Chofer, searchObject.Pagada, searchObject.StoreTS, searchObject.UpdateTS, searchObject.ProductoID, searchObject.NumeroBoleta, searchObject.Ticket, searchObject.CodigoClienteProvArchivo, searchObject.NombreProductor, searchObject.FechaEntrada, searchObject.PesadorEntrada, searchObject.PesoDeEntrada, searchObject.BasculaEntrada, searchObject.FechaSalida, searchObject.PesoDeSalida, searchObject.PesadorSalida, searchObject.BasculaSalida, searchObject.Pesonetoentrada, searchObject.Pesonetosalida, searchObject.DctoImpurezas, searchObject.DctoSecado, searchObject.Totalapagar, searchObject.BodegaID, searchObject.ApplyHumedad, searchObject.ApplyImpurezas, searchObject.ApplySecado, searchObject.FolioDestino, searchObject.PesoDestino, searchObject.Merma, searchObject.Flete, searchObject.ImporteFlete, searchObject.PrecioNetoDestino, searchObject.DctoGranoChico, searchObject.DctoGranoDanado, searchObject.DctoGranoQuebrado, searchObject.DctoGranoEstrellado, searchObject.TransportistaID, searchObject.CabezasDeGanado, searchObject.LlevaFlete, searchObject.DeGranjaACorrales, searchObject.LastEditDate, searchObject.CreationDate, searchObject.PrecioTransportista, searchObject.UsaPesoDestinoParaTransportista);
	}
	
	/// <summary>
	/// Returns all Boletas objects.
	/// </summary>
	/// <returns>List of all Boletas objects. </returns>
	[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
	public static Boletas[] Search()
	{
		return Search ( null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
	}
	
	#endregion
	
	
	#endregion
	
	
	#endregion
	
	
}

