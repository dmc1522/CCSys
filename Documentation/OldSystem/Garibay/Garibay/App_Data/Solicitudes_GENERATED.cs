/****************************************************************************/
/* Code Author (written by Xin Zhao)                                        */
/*                                                                          */
/* This file was automatically generated using Code Author.                 */
/* Any manual changes to this file will be overwritten by a automated tool. */
/*                                                                          */
/* Date Generated: 28/05/2011 03:38:38 p.m.                                    */
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
public partial class Solicitudes
{
	
	
	#region Constants
	private static readonly string TABLE_NAME = "[dbo].[Solicitudes]";
	
	#endregion
	
	
	#region Fields
	private System.Int32? _solicitudID;
	private System.Int32? _creditoID;
	private System.Int32? _productorID;
	private System.Int32? _experiencia;
	private System.Double? _monto;
	private System.Double? _superficieFinanciada;
	private System.Double? _superficieasembrar;
	private System.Int32? _plazo;
	private System.Double? _recursosPropios;
	private System.String _descripciondegarantias;
	private System.Int32? _userID;
	private System.Double? _valordegarantias;
	private System.String _testigo1;
	private System.String _testigo2;
	private System.String _aval1;
	private System.String _aval2;
	private System.DateTime? _storeTS;
	private System.DateTime? _updateTS;
	private System.DateTime? _fecha;
	private System.Int32? _statusID;
	private System.Double? _hectAseguradas;
	private System.String _descParcelas;
	private System.Double? _costoTotalSeguro;
	private System.String _aval1Dom;
	private System.String _aval2Dom;
	private System.String _ubicacionGarantia;
	private System.Double? _otrosPasivosMonto;
	private System.String _otrosPasivosAQuienLeDebe;
	private System.Double? _ingNetosAnualOtrosCultivos;
	private System.Double? _ingNetosAnualGanaderia;
	private System.Double? _ingNetosComercioServicios;
	private System.Int32? _casaHabitacion;
	private System.Int32? _rastra;
	private System.Int32? _arado;
	private System.Int32? _cultivadora;
	private System.Int32? _subsuelo;
	private System.Int32? _tractor;
	private System.Int32? _sembradora;
	private System.Int32? _camioneta;
	private System.Double? _otrosActivos;
	private System.Double? _totalActivos;
	private System.Double? _garantiaLiquida;
	private System.String _conceptoSoporteGarantia;
	private System.Double? _montoSoporteGarantia;
	private System.String _domicilioDelDeposito;
	private System.String _firmaAutorizada1;
	private System.String _firmaAutorizada2;
	private System.String _firmaAutorizada3;
	private System.String _firmaAutorizada4;
	private System.String _firmaAutorizada5;
	private System.String _ejido;
	
	#endregion
	
	
	#region Properties
	public System.Int32? SolicitudID
	{
		get
		{
			return _solicitudID;
		}
		set
		{
			_solicitudID = value;
		}
	}
	
	public System.Int32? CreditoID
	{
		get
		{
			return _creditoID;
		}
		set
		{
			_creditoID = value;
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
	
	public System.Int32? Experiencia
	{
		get
		{
			return _experiencia;
		}
		set
		{
			_experiencia = value;
		}
	}
	
	public System.Double? Monto
	{
		get
		{
			return _monto;
		}
		set
		{
			_monto = value;
		}
	}
	
	public System.Double? SuperficieFinanciada
	{
		get
		{
			return _superficieFinanciada;
		}
		set
		{
			_superficieFinanciada = value;
		}
	}
	
	public System.Double? Superficieasembrar
	{
		get
		{
			return _superficieasembrar;
		}
		set
		{
			_superficieasembrar = value;
		}
	}
	
	public System.Int32? Plazo
	{
		get
		{
			return _plazo;
		}
		set
		{
			_plazo = value;
		}
	}
	
	public System.Double? RecursosPropios
	{
		get
		{
			return _recursosPropios;
		}
		set
		{
			_recursosPropios = value;
		}
	}
	
	public System.String Descripciondegarantias
	{
		get
		{
			return _descripciondegarantias;
		}
		set
		{
			_descripciondegarantias = value;
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
	
	public System.Double? Valordegarantias
	{
		get
		{
			return _valordegarantias;
		}
		set
		{
			_valordegarantias = value;
		}
	}
	
	public System.String Testigo1
	{
		get
		{
			return _testigo1;
		}
		set
		{
			_testigo1 = value;
		}
	}
	
	public System.String Testigo2
	{
		get
		{
			return _testigo2;
		}
		set
		{
			_testigo2 = value;
		}
	}
	
	public System.String Aval1
	{
		get
		{
			return _aval1;
		}
		set
		{
			_aval1 = value;
		}
	}
	
	public System.String Aval2
	{
		get
		{
			return _aval2;
		}
		set
		{
			_aval2 = value;
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
	
	public System.DateTime? Fecha
	{
		get
		{
			return _fecha;
		}
		set
		{
			_fecha = value;
		}
	}
	
	public System.Int32? StatusID
	{
		get
		{
			return _statusID;
		}
		set
		{
			_statusID = value;
		}
	}
	
	public System.Double? HectAseguradas
	{
		get
		{
			return _hectAseguradas;
		}
		set
		{
			_hectAseguradas = value;
		}
	}
	
	public System.String DescParcelas
	{
		get
		{
			return _descParcelas;
		}
		set
		{
			_descParcelas = value;
		}
	}
	
	public System.Double? CostoTotalSeguro
	{
		get
		{
			return _costoTotalSeguro;
		}
		set
		{
			_costoTotalSeguro = value;
		}
	}
	
	public System.String Aval1Dom
	{
		get
		{
			return _aval1Dom;
		}
		set
		{
			_aval1Dom = value;
		}
	}
	
	public System.String Aval2Dom
	{
		get
		{
			return _aval2Dom;
		}
		set
		{
			_aval2Dom = value;
		}
	}
	
	public System.String UbicacionGarantia
	{
		get
		{
			return _ubicacionGarantia;
		}
		set
		{
			_ubicacionGarantia = value;
		}
	}
	
	public System.Double? OtrosPasivosMonto
	{
		get
		{
			return _otrosPasivosMonto;
		}
		set
		{
			_otrosPasivosMonto = value;
		}
	}
	
	public System.String OtrosPasivosAQuienLeDebe
	{
		get
		{
			return _otrosPasivosAQuienLeDebe;
		}
		set
		{
			_otrosPasivosAQuienLeDebe = value;
		}
	}
	
	public System.Double? IngNetosAnualOtrosCultivos
	{
		get
		{
			return _ingNetosAnualOtrosCultivos;
		}
		set
		{
			_ingNetosAnualOtrosCultivos = value;
		}
	}
	
	public System.Double? IngNetosAnualGanaderia
	{
		get
		{
			return _ingNetosAnualGanaderia;
		}
		set
		{
			_ingNetosAnualGanaderia = value;
		}
	}
	
	public System.Double? IngNetosComercioServicios
	{
		get
		{
			return _ingNetosComercioServicios;
		}
		set
		{
			_ingNetosComercioServicios = value;
		}
	}
	
	public System.Int32? CasaHabitacion
	{
		get
		{
			return _casaHabitacion;
		}
		set
		{
			_casaHabitacion = value;
		}
	}
	
	public System.Int32? Rastra
	{
		get
		{
			return _rastra;
		}
		set
		{
			_rastra = value;
		}
	}
	
	public System.Int32? Arado
	{
		get
		{
			return _arado;
		}
		set
		{
			_arado = value;
		}
	}
	
	public System.Int32? Cultivadora
	{
		get
		{
			return _cultivadora;
		}
		set
		{
			_cultivadora = value;
		}
	}
	
	public System.Int32? Subsuelo
	{
		get
		{
			return _subsuelo;
		}
		set
		{
			_subsuelo = value;
		}
	}
	
	public System.Int32? Tractor
	{
		get
		{
			return _tractor;
		}
		set
		{
			_tractor = value;
		}
	}
	
	public System.Int32? Sembradora
	{
		get
		{
			return _sembradora;
		}
		set
		{
			_sembradora = value;
		}
	}
	
	public System.Int32? Camioneta
	{
		get
		{
			return _camioneta;
		}
		set
		{
			_camioneta = value;
		}
	}
	
	public System.Double? OtrosActivos
	{
		get
		{
			return _otrosActivos;
		}
		set
		{
			_otrosActivos = value;
		}
	}
	
	public System.Double? TotalActivos
	{
		get
		{
			return _totalActivos;
		}
		set
		{
			_totalActivos = value;
		}
	}
	
	public System.Double? GarantiaLiquida
	{
		get
		{
			return _garantiaLiquida;
		}
		set
		{
			_garantiaLiquida = value;
		}
	}
	
	public System.String ConceptoSoporteGarantia
	{
		get
		{
			return _conceptoSoporteGarantia;
		}
		set
		{
			_conceptoSoporteGarantia = value;
		}
	}
	
	public System.Double? MontoSoporteGarantia
	{
		get
		{
			return _montoSoporteGarantia;
		}
		set
		{
			_montoSoporteGarantia = value;
		}
	}
	
	public System.String DomicilioDelDeposito
	{
		get
		{
			return _domicilioDelDeposito;
		}
		set
		{
			_domicilioDelDeposito = value;
		}
	}
	
	public System.String FirmaAutorizada1
	{
		get
		{
			return _firmaAutorizada1;
		}
		set
		{
			_firmaAutorizada1 = value;
		}
	}
	
	public System.String FirmaAutorizada2
	{
		get
		{
			return _firmaAutorizada2;
		}
		set
		{
			_firmaAutorizada2 = value;
		}
	}
	
	public System.String FirmaAutorizada3
	{
		get
		{
			return _firmaAutorizada3;
		}
		set
		{
			_firmaAutorizada3 = value;
		}
	}
	
	public System.String FirmaAutorizada4
	{
		get
		{
			return _firmaAutorizada4;
		}
		set
		{
			_firmaAutorizada4 = value;
		}
	}
	
	public System.String FirmaAutorizada5
	{
		get
		{
			return _firmaAutorizada5;
		}
		set
		{
			_firmaAutorizada5 = value;
		}
	}
	
	public System.String Ejido
	{
		get
		{
			return _ejido;
		}
		set
		{
			_ejido = value;
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
		
		ds.Tables[TABLE_NAME].Columns.Add("solicitudID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("creditoID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("productorID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("Experiencia", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("Monto", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("superficieFinanciada", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("Superficieasembrar", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("Plazo", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("RecursosPropios", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("Descripciondegarantias", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("userID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("Valordegarantias", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("testigo1", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("testigo2", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("aval1", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("aval2", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("storeTS", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("updateTS", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("fecha", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("statusID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("hectAseguradas", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("descParcelas", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("CostoTotalSeguro", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("Aval1Dom", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("Aval2Dom", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("UbicacionGarantia", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("otrosPasivosMonto", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("otrosPasivosAQuienLeDebe", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("ingNetosAnualOtrosCultivos", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("ingNetosAnualGanaderia", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("ingNetosComercioServicios", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("casaHabitacion", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("rastra", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("Arado", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("Cultivadora", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("Subsuelo", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("tractor", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("sembradora", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("camioneta", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("otrosActivos", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("totalActivos", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("garantiaLiquida", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("ConceptoSoporteGarantia", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("montoSoporteGarantia", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("domicilioDelDeposito", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("firmaAutorizada1", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("firmaAutorizada2", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("firmaAutorizada3", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("firmaAutorizada4", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("firmaAutorizada5", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("ejido", typeof(System.String) );
		
		dr = ds.Tables[TABLE_NAME].NewRow();
		
		if (SolicitudID == null)
		dr["solicitudID"] = DBNull.Value;
		else
		dr["solicitudID"] = SolicitudID;
		
		if (CreditoID == null)
		dr["creditoID"] = DBNull.Value;
		else
		dr["creditoID"] = CreditoID;
		
		if (ProductorID == null)
		dr["productorID"] = DBNull.Value;
		else
		dr["productorID"] = ProductorID;
		
		if (Experiencia == null)
		dr["Experiencia"] = DBNull.Value;
		else
		dr["Experiencia"] = Experiencia;
		
		if (Monto == null)
		dr["Monto"] = DBNull.Value;
		else
		dr["Monto"] = Monto;
		
		if (SuperficieFinanciada == null)
		dr["superficieFinanciada"] = DBNull.Value;
		else
		dr["superficieFinanciada"] = SuperficieFinanciada;
		
		if (Superficieasembrar == null)
		dr["Superficieasembrar"] = DBNull.Value;
		else
		dr["Superficieasembrar"] = Superficieasembrar;
		
		if (Plazo == null)
		dr["Plazo"] = DBNull.Value;
		else
		dr["Plazo"] = Plazo;
		
		if (RecursosPropios == null)
		dr["RecursosPropios"] = DBNull.Value;
		else
		dr["RecursosPropios"] = RecursosPropios;
		
		if (Descripciondegarantias == null)
		dr["Descripciondegarantias"] = DBNull.Value;
		else
		dr["Descripciondegarantias"] = Descripciondegarantias;
		
		if (UserID == null)
		dr["userID"] = DBNull.Value;
		else
		dr["userID"] = UserID;
		
		if (Valordegarantias == null)
		dr["Valordegarantias"] = DBNull.Value;
		else
		dr["Valordegarantias"] = Valordegarantias;
		
		if (Testigo1 == null)
		dr["testigo1"] = DBNull.Value;
		else
		dr["testigo1"] = Testigo1;
		
		if (Testigo2 == null)
		dr["testigo2"] = DBNull.Value;
		else
		dr["testigo2"] = Testigo2;
		
		if (Aval1 == null)
		dr["aval1"] = DBNull.Value;
		else
		dr["aval1"] = Aval1;
		
		if (Aval2 == null)
		dr["aval2"] = DBNull.Value;
		else
		dr["aval2"] = Aval2;
		
		if (StoreTS == null)
		dr["storeTS"] = DBNull.Value;
		else
		dr["storeTS"] = StoreTS;
		
		if (UpdateTS == null)
		dr["updateTS"] = DBNull.Value;
		else
		dr["updateTS"] = UpdateTS;
		
		if (Fecha == null)
		dr["fecha"] = DBNull.Value;
		else
		dr["fecha"] = Fecha;
		
		if (StatusID == null)
		dr["statusID"] = DBNull.Value;
		else
		dr["statusID"] = StatusID;
		
		if (HectAseguradas == null)
		dr["hectAseguradas"] = DBNull.Value;
		else
		dr["hectAseguradas"] = HectAseguradas;
		
		if (DescParcelas == null)
		dr["descParcelas"] = DBNull.Value;
		else
		dr["descParcelas"] = DescParcelas;
		
		if (CostoTotalSeguro == null)
		dr["CostoTotalSeguro"] = DBNull.Value;
		else
		dr["CostoTotalSeguro"] = CostoTotalSeguro;
		
		if (Aval1Dom == null)
		dr["Aval1Dom"] = DBNull.Value;
		else
		dr["Aval1Dom"] = Aval1Dom;
		
		if (Aval2Dom == null)
		dr["Aval2Dom"] = DBNull.Value;
		else
		dr["Aval2Dom"] = Aval2Dom;
		
		if (UbicacionGarantia == null)
		dr["UbicacionGarantia"] = DBNull.Value;
		else
		dr["UbicacionGarantia"] = UbicacionGarantia;
		
		if (OtrosPasivosMonto == null)
		dr["otrosPasivosMonto"] = DBNull.Value;
		else
		dr["otrosPasivosMonto"] = OtrosPasivosMonto;
		
		if (OtrosPasivosAQuienLeDebe == null)
		dr["otrosPasivosAQuienLeDebe"] = DBNull.Value;
		else
		dr["otrosPasivosAQuienLeDebe"] = OtrosPasivosAQuienLeDebe;
		
		if (IngNetosAnualOtrosCultivos == null)
		dr["ingNetosAnualOtrosCultivos"] = DBNull.Value;
		else
		dr["ingNetosAnualOtrosCultivos"] = IngNetosAnualOtrosCultivos;
		
		if (IngNetosAnualGanaderia == null)
		dr["ingNetosAnualGanaderia"] = DBNull.Value;
		else
		dr["ingNetosAnualGanaderia"] = IngNetosAnualGanaderia;
		
		if (IngNetosComercioServicios == null)
		dr["ingNetosComercioServicios"] = DBNull.Value;
		else
		dr["ingNetosComercioServicios"] = IngNetosComercioServicios;
		
		if (CasaHabitacion == null)
		dr["casaHabitacion"] = DBNull.Value;
		else
		dr["casaHabitacion"] = CasaHabitacion;
		
		if (Rastra == null)
		dr["rastra"] = DBNull.Value;
		else
		dr["rastra"] = Rastra;
		
		if (Arado == null)
		dr["Arado"] = DBNull.Value;
		else
		dr["Arado"] = Arado;
		
		if (Cultivadora == null)
		dr["Cultivadora"] = DBNull.Value;
		else
		dr["Cultivadora"] = Cultivadora;
		
		if (Subsuelo == null)
		dr["Subsuelo"] = DBNull.Value;
		else
		dr["Subsuelo"] = Subsuelo;
		
		if (Tractor == null)
		dr["tractor"] = DBNull.Value;
		else
		dr["tractor"] = Tractor;
		
		if (Sembradora == null)
		dr["sembradora"] = DBNull.Value;
		else
		dr["sembradora"] = Sembradora;
		
		if (Camioneta == null)
		dr["camioneta"] = DBNull.Value;
		else
		dr["camioneta"] = Camioneta;
		
		if (OtrosActivos == null)
		dr["otrosActivos"] = DBNull.Value;
		else
		dr["otrosActivos"] = OtrosActivos;
		
		if (TotalActivos == null)
		dr["totalActivos"] = DBNull.Value;
		else
		dr["totalActivos"] = TotalActivos;
		
		if (GarantiaLiquida == null)
		dr["garantiaLiquida"] = DBNull.Value;
		else
		dr["garantiaLiquida"] = GarantiaLiquida;
		
		if (ConceptoSoporteGarantia == null)
		dr["ConceptoSoporteGarantia"] = DBNull.Value;
		else
		dr["ConceptoSoporteGarantia"] = ConceptoSoporteGarantia;
		
		if (MontoSoporteGarantia == null)
		dr["montoSoporteGarantia"] = DBNull.Value;
		else
		dr["montoSoporteGarantia"] = MontoSoporteGarantia;
		
		if (DomicilioDelDeposito == null)
		dr["domicilioDelDeposito"] = DBNull.Value;
		else
		dr["domicilioDelDeposito"] = DomicilioDelDeposito;
		
		if (FirmaAutorizada1 == null)
		dr["firmaAutorizada1"] = DBNull.Value;
		else
		dr["firmaAutorizada1"] = FirmaAutorizada1;
		
		if (FirmaAutorizada2 == null)
		dr["firmaAutorizada2"] = DBNull.Value;
		else
		dr["firmaAutorizada2"] = FirmaAutorizada2;
		
		if (FirmaAutorizada3 == null)
		dr["firmaAutorizada3"] = DBNull.Value;
		else
		dr["firmaAutorizada3"] = FirmaAutorizada3;
		
		if (FirmaAutorizada4 == null)
		dr["firmaAutorizada4"] = DBNull.Value;
		else
		dr["firmaAutorizada4"] = FirmaAutorizada4;
		
		if (FirmaAutorizada5 == null)
		dr["firmaAutorizada5"] = DBNull.Value;
		else
		dr["firmaAutorizada5"] = FirmaAutorizada5;
		
		if (Ejido == null)
		dr["ejido"] = DBNull.Value;
		else
		dr["ejido"] = Ejido;
		
		
		ds.Tables[TABLE_NAME].Rows.Add(dr);
		
	}
	
	protected void MapFrom(DataRow dr)
	{
		SolicitudID = dr["solicitudID"] != DBNull.Value ? Convert.ToInt32(dr["solicitudID"]) : SolicitudID = null;
		CreditoID = dr["creditoID"] != DBNull.Value ? Convert.ToInt32(dr["creditoID"]) : CreditoID = null;
		ProductorID = dr["productorID"] != DBNull.Value ? Convert.ToInt32(dr["productorID"]) : ProductorID = null;
		Experiencia = dr["Experiencia"] != DBNull.Value ? Convert.ToInt32(dr["Experiencia"]) : Experiencia = null;
		Monto = dr["Monto"] != DBNull.Value ? Convert.ToDouble(dr["Monto"]) : Monto = null;
		SuperficieFinanciada = dr["superficieFinanciada"] != DBNull.Value ? Convert.ToDouble(dr["superficieFinanciada"]) : SuperficieFinanciada = null;
		Superficieasembrar = dr["Superficieasembrar"] != DBNull.Value ? Convert.ToDouble(dr["Superficieasembrar"]) : Superficieasembrar = null;
		Plazo = dr["Plazo"] != DBNull.Value ? Convert.ToInt32(dr["Plazo"]) : Plazo = null;
		RecursosPropios = dr["RecursosPropios"] != DBNull.Value ? Convert.ToDouble(dr["RecursosPropios"]) : RecursosPropios = null;
		Descripciondegarantias = dr["Descripciondegarantias"] != DBNull.Value ? Convert.ToString(dr["Descripciondegarantias"]) : Descripciondegarantias = null;
		UserID = dr["userID"] != DBNull.Value ? Convert.ToInt32(dr["userID"]) : UserID = null;
		Valordegarantias = dr["Valordegarantias"] != DBNull.Value ? Convert.ToDouble(dr["Valordegarantias"]) : Valordegarantias = null;
		Testigo1 = dr["testigo1"] != DBNull.Value ? Convert.ToString(dr["testigo1"]) : Testigo1 = null;
		Testigo2 = dr["testigo2"] != DBNull.Value ? Convert.ToString(dr["testigo2"]) : Testigo2 = null;
		Aval1 = dr["aval1"] != DBNull.Value ? Convert.ToString(dr["aval1"]) : Aval1 = null;
		Aval2 = dr["aval2"] != DBNull.Value ? Convert.ToString(dr["aval2"]) : Aval2 = null;
		StoreTS = dr["storeTS"] != DBNull.Value ? Convert.ToDateTime(dr["storeTS"]) : StoreTS = null;
		UpdateTS = dr["updateTS"] != DBNull.Value ? Convert.ToDateTime(dr["updateTS"]) : UpdateTS = null;
		Fecha = dr["fecha"] != DBNull.Value ? Convert.ToDateTime(dr["fecha"]) : Fecha = null;
		StatusID = dr["statusID"] != DBNull.Value ? Convert.ToInt32(dr["statusID"]) : StatusID = null;
		HectAseguradas = dr["hectAseguradas"] != DBNull.Value ? Convert.ToDouble(dr["hectAseguradas"]) : HectAseguradas = null;
		DescParcelas = dr["descParcelas"] != DBNull.Value ? Convert.ToString(dr["descParcelas"]) : DescParcelas = null;
		CostoTotalSeguro = dr["CostoTotalSeguro"] != DBNull.Value ? Convert.ToDouble(dr["CostoTotalSeguro"]) : CostoTotalSeguro = null;
		Aval1Dom = dr["Aval1Dom"] != DBNull.Value ? Convert.ToString(dr["Aval1Dom"]) : Aval1Dom = null;
		Aval2Dom = dr["Aval2Dom"] != DBNull.Value ? Convert.ToString(dr["Aval2Dom"]) : Aval2Dom = null;
		UbicacionGarantia = dr["UbicacionGarantia"] != DBNull.Value ? Convert.ToString(dr["UbicacionGarantia"]) : UbicacionGarantia = null;
		OtrosPasivosMonto = dr["otrosPasivosMonto"] != DBNull.Value ? Convert.ToDouble(dr["otrosPasivosMonto"]) : OtrosPasivosMonto = null;
		OtrosPasivosAQuienLeDebe = dr["otrosPasivosAQuienLeDebe"] != DBNull.Value ? Convert.ToString(dr["otrosPasivosAQuienLeDebe"]) : OtrosPasivosAQuienLeDebe = null;
		IngNetosAnualOtrosCultivos = dr["ingNetosAnualOtrosCultivos"] != DBNull.Value ? Convert.ToDouble(dr["ingNetosAnualOtrosCultivos"]) : IngNetosAnualOtrosCultivos = null;
		IngNetosAnualGanaderia = dr["ingNetosAnualGanaderia"] != DBNull.Value ? Convert.ToDouble(dr["ingNetosAnualGanaderia"]) : IngNetosAnualGanaderia = null;
		IngNetosComercioServicios = dr["ingNetosComercioServicios"] != DBNull.Value ? Convert.ToDouble(dr["ingNetosComercioServicios"]) : IngNetosComercioServicios = null;
		CasaHabitacion = dr["casaHabitacion"] != DBNull.Value ? Convert.ToInt32(dr["casaHabitacion"]) : CasaHabitacion = null;
		Rastra = dr["rastra"] != DBNull.Value ? Convert.ToInt32(dr["rastra"]) : Rastra = null;
		Arado = dr["Arado"] != DBNull.Value ? Convert.ToInt32(dr["Arado"]) : Arado = null;
		Cultivadora = dr["Cultivadora"] != DBNull.Value ? Convert.ToInt32(dr["Cultivadora"]) : Cultivadora = null;
		Subsuelo = dr["Subsuelo"] != DBNull.Value ? Convert.ToInt32(dr["Subsuelo"]) : Subsuelo = null;
		Tractor = dr["tractor"] != DBNull.Value ? Convert.ToInt32(dr["tractor"]) : Tractor = null;
		Sembradora = dr["sembradora"] != DBNull.Value ? Convert.ToInt32(dr["sembradora"]) : Sembradora = null;
		Camioneta = dr["camioneta"] != DBNull.Value ? Convert.ToInt32(dr["camioneta"]) : Camioneta = null;
		OtrosActivos = dr["otrosActivos"] != DBNull.Value ? Convert.ToDouble(dr["otrosActivos"]) : OtrosActivos = null;
		TotalActivos = dr["totalActivos"] != DBNull.Value ? Convert.ToDouble(dr["totalActivos"]) : TotalActivos = null;
		GarantiaLiquida = dr["garantiaLiquida"] != DBNull.Value ? Convert.ToDouble(dr["garantiaLiquida"]) : GarantiaLiquida = null;
		ConceptoSoporteGarantia = dr["ConceptoSoporteGarantia"] != DBNull.Value ? Convert.ToString(dr["ConceptoSoporteGarantia"]) : ConceptoSoporteGarantia = null;
		MontoSoporteGarantia = dr["montoSoporteGarantia"] != DBNull.Value ? Convert.ToDouble(dr["montoSoporteGarantia"]) : MontoSoporteGarantia = null;
		DomicilioDelDeposito = dr["domicilioDelDeposito"] != DBNull.Value ? Convert.ToString(dr["domicilioDelDeposito"]) : DomicilioDelDeposito = null;
		FirmaAutorizada1 = dr["firmaAutorizada1"] != DBNull.Value ? Convert.ToString(dr["firmaAutorizada1"]) : FirmaAutorizada1 = null;
		FirmaAutorizada2 = dr["firmaAutorizada2"] != DBNull.Value ? Convert.ToString(dr["firmaAutorizada2"]) : FirmaAutorizada2 = null;
		FirmaAutorizada3 = dr["firmaAutorizada3"] != DBNull.Value ? Convert.ToString(dr["firmaAutorizada3"]) : FirmaAutorizada3 = null;
		FirmaAutorizada4 = dr["firmaAutorizada4"] != DBNull.Value ? Convert.ToString(dr["firmaAutorizada4"]) : FirmaAutorizada4 = null;
		FirmaAutorizada5 = dr["firmaAutorizada5"] != DBNull.Value ? Convert.ToString(dr["firmaAutorizada5"]) : FirmaAutorizada5 = null;
		Ejido = dr["ejido"] != DBNull.Value ? Convert.ToString(dr["ejido"]) : Ejido = null;
	}
	
	public static Solicitudes[] MapFrom(DataSet ds)
	{
		List<Solicitudes> objects;
		
		
		// Initialise Collection.
		objects = new List<Solicitudes>();
		
		// Validation.
		if (ds == null)
		throw new ApplicationException("Cannot map to dataset null.");
		else if (ds.Tables[TABLE_NAME].Rows.Count == 0)
		return objects.ToArray();
		
		if (ds.Tables[TABLE_NAME] == null)
		throw new ApplicationException("Cannot find table [dbo].[Solicitudes] in DataSet.");
		
		if (ds.Tables[TABLE_NAME].Rows.Count < 1)
		throw new ApplicationException("Table [dbo].[Solicitudes] is empty.");
		
		// Map DataSet to Instance.
		foreach (DataRow dr in ds.Tables[TABLE_NAME].Rows)
		{
			Solicitudes instance = new Solicitudes();
			instance.MapFrom(dr);
			objects.Add(instance);
		}
		
		// Return collection.
		return objects.ToArray();
	}
	
	
	#endregion
	
	
	#region CRUD Methods
	
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static Solicitudes Get(System.Int32 solicitudID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		Solicitudes instance;
		
		
		instance = new Solicitudes();
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspSolicitudes_SELECT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, solicitudID);
		
		// Get results.
		ds = db.ExecuteDataSet(dbCommand);
		// Verification.
		if (ds == null || ds.Tables[0].Rows.Count == 0) throw new ApplicationException("Could not get Solicitudes ID:" + solicitudID.ToString()+ " from Database.");
		// Return results.
		ds.Tables[0].TableName = TABLE_NAME;
		
		instance.MapFrom( ds.Tables[0].Rows[0] );
		return instance;
	}
	
	#region INSERT
	public void Insert(System.Int32? creditoID, System.Int32? productorID, System.Int32? experiencia, System.Double? monto, System.Double? superficieFinanciada, System.Double? superficieasembrar, System.Int32? plazo, System.Double? recursosPropios, System.String descripciondegarantias, System.Int32? userID, System.Double? valordegarantias, System.String testigo1, System.String testigo2, System.String aval1, System.String aval2, System.DateTime? storeTS, System.DateTime? updateTS, System.DateTime? fecha, System.Int32? statusID, System.Double? hectAseguradas, System.String descParcelas, System.Double? costoTotalSeguro, System.String aval1Dom, System.String aval2Dom, System.String ubicacionGarantia, System.Double? otrosPasivosMonto, System.String otrosPasivosAQuienLeDebe, System.Double? ingNetosAnualOtrosCultivos, System.Double? ingNetosAnualGanaderia, System.Double? ingNetosComercioServicios, System.Int32? casaHabitacion, System.Int32? rastra, System.Int32? arado, System.Int32? cultivadora, System.Int32? subsuelo, System.Int32? tractor, System.Int32? sembradora, System.Int32? camioneta, System.Double? otrosActivos, System.Double? totalActivos, System.Double? garantiaLiquida, System.String conceptoSoporteGarantia, System.Double? montoSoporteGarantia, System.String domicilioDelDeposito, System.String firmaAutorizada1, System.String firmaAutorizada2, System.String firmaAutorizada3, System.String firmaAutorizada4, System.String firmaAutorizada5, System.String ejido, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspSolicitudes_INSERT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, creditoID, productorID, experiencia, monto, superficieFinanciada, superficieasembrar, plazo, recursosPropios, descripciondegarantias, userID, valordegarantias, testigo1, testigo2, aval1, aval2, storeTS, updateTS, fecha, statusID, hectAseguradas, descParcelas, costoTotalSeguro, aval1Dom, aval2Dom, ubicacionGarantia, otrosPasivosMonto, otrosPasivosAQuienLeDebe, ingNetosAnualOtrosCultivos, ingNetosAnualGanaderia, ingNetosComercioServicios, casaHabitacion, rastra, arado, cultivadora, subsuelo, tractor, sembradora, camioneta, otrosActivos, totalActivos, garantiaLiquida, conceptoSoporteGarantia, montoSoporteGarantia, domicilioDelDeposito, firmaAutorizada1, firmaAutorizada2, firmaAutorizada3, firmaAutorizada4, firmaAutorizada5, ejido);
		
		if (transaction == null)
		this.SolicitudID = Convert.ToInt32(db.ExecuteScalar(dbCommand));
		else
		this.SolicitudID = Convert.ToInt32(db.ExecuteScalar(dbCommand, transaction));
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
	public void Insert(System.Int32? creditoID, System.Int32? productorID, System.Int32? experiencia, System.Double? monto, System.Double? superficieFinanciada, System.Double? superficieasembrar, System.Int32? plazo, System.Double? recursosPropios, System.String descripciondegarantias, System.Int32? userID, System.Double? valordegarantias, System.String testigo1, System.String testigo2, System.String aval1, System.String aval2, System.DateTime? storeTS, System.DateTime? updateTS, System.DateTime? fecha, System.Int32? statusID, System.Double? hectAseguradas, System.String descParcelas, System.Double? costoTotalSeguro, System.String aval1Dom, System.String aval2Dom, System.String ubicacionGarantia, System.Double? otrosPasivosMonto, System.String otrosPasivosAQuienLeDebe, System.Double? ingNetosAnualOtrosCultivos, System.Double? ingNetosAnualGanaderia, System.Double? ingNetosComercioServicios, System.Int32? casaHabitacion, System.Int32? rastra, System.Int32? arado, System.Int32? cultivadora, System.Int32? subsuelo, System.Int32? tractor, System.Int32? sembradora, System.Int32? camioneta, System.Double? otrosActivos, System.Double? totalActivos, System.Double? garantiaLiquida, System.String conceptoSoporteGarantia, System.Double? montoSoporteGarantia, System.String domicilioDelDeposito, System.String firmaAutorizada1, System.String firmaAutorizada2, System.String firmaAutorizada3, System.String firmaAutorizada4, System.String firmaAutorizada5, System.String ejido)
	{
		Insert(creditoID, productorID, experiencia, monto, superficieFinanciada, superficieasembrar, plazo, recursosPropios, descripciondegarantias, userID, valordegarantias, testigo1, testigo2, aval1, aval2, storeTS, updateTS, fecha, statusID, hectAseguradas, descParcelas, costoTotalSeguro, aval1Dom, aval2Dom, ubicacionGarantia, otrosPasivosMonto, otrosPasivosAQuienLeDebe, ingNetosAnualOtrosCultivos, ingNetosAnualGanaderia, ingNetosComercioServicios, casaHabitacion, rastra, arado, cultivadora, subsuelo, tractor, sembradora, camioneta, otrosActivos, totalActivos, garantiaLiquida, conceptoSoporteGarantia, montoSoporteGarantia, domicilioDelDeposito, firmaAutorizada1, firmaAutorizada2, firmaAutorizada3, firmaAutorizada4, firmaAutorizada5, ejido, null);
	}
	/// <summary>
	/// Insert current Solicitudes to database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Insert(DbTransaction transaction)
	{
		Insert(CreditoID, ProductorID, Experiencia, Monto, SuperficieFinanciada, Superficieasembrar, Plazo, RecursosPropios, Descripciondegarantias, UserID, Valordegarantias, Testigo1, Testigo2, Aval1, Aval2, StoreTS, UpdateTS, Fecha, StatusID, HectAseguradas, DescParcelas, CostoTotalSeguro, Aval1Dom, Aval2Dom, UbicacionGarantia, OtrosPasivosMonto, OtrosPasivosAQuienLeDebe, IngNetosAnualOtrosCultivos, IngNetosAnualGanaderia, IngNetosComercioServicios, CasaHabitacion, Rastra, Arado, Cultivadora, Subsuelo, Tractor, Sembradora, Camioneta, OtrosActivos, TotalActivos, GarantiaLiquida, ConceptoSoporteGarantia, MontoSoporteGarantia, DomicilioDelDeposito, FirmaAutorizada1, FirmaAutorizada2, FirmaAutorizada3, FirmaAutorizada4, FirmaAutorizada5, Ejido, transaction);
	}
	
	/// <summary>
	/// Insert current Solicitudes to database.
	/// </summary>
	public void Insert()
	{
		this.Insert((DbTransaction)null);
	}
	#endregion
	
	
	#region UPDATE
	public static void Update(System.Int32? solicitudID, System.Int32? creditoID, System.Int32? productorID, System.Int32? experiencia, System.Double? monto, System.Double? superficieFinanciada, System.Double? superficieasembrar, System.Int32? plazo, System.Double? recursosPropios, System.String descripciondegarantias, System.Int32? userID, System.Double? valordegarantias, System.String testigo1, System.String testigo2, System.String aval1, System.String aval2, System.DateTime? storeTS, System.DateTime? updateTS, System.DateTime? fecha, System.Int32? statusID, System.Double? hectAseguradas, System.String descParcelas, System.Double? costoTotalSeguro, System.String aval1Dom, System.String aval2Dom, System.String ubicacionGarantia, System.Double? otrosPasivosMonto, System.String otrosPasivosAQuienLeDebe, System.Double? ingNetosAnualOtrosCultivos, System.Double? ingNetosAnualGanaderia, System.Double? ingNetosComercioServicios, System.Int32? casaHabitacion, System.Int32? rastra, System.Int32? arado, System.Int32? cultivadora, System.Int32? subsuelo, System.Int32? tractor, System.Int32? sembradora, System.Int32? camioneta, System.Double? otrosActivos, System.Double? totalActivos, System.Double? garantiaLiquida, System.String conceptoSoporteGarantia, System.Double? montoSoporteGarantia, System.String domicilioDelDeposito, System.String firmaAutorizada1, System.String firmaAutorizada2, System.String firmaAutorizada3, System.String firmaAutorizada4, System.String firmaAutorizada5, System.String ejido, DbTransaction transaction)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspSolicitudes_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@solicitudID"].Value = solicitudID;
		dbCommand.Parameters["@creditoID"].Value = creditoID;
		dbCommand.Parameters["@productorID"].Value = productorID;
		dbCommand.Parameters["@experiencia"].Value = experiencia;
		dbCommand.Parameters["@monto"].Value = monto;
		dbCommand.Parameters["@superficieFinanciada"].Value = superficieFinanciada;
		dbCommand.Parameters["@superficieasembrar"].Value = superficieasembrar;
		dbCommand.Parameters["@plazo"].Value = plazo;
		dbCommand.Parameters["@recursosPropios"].Value = recursosPropios;
		dbCommand.Parameters["@descripciondegarantias"].Value = descripciondegarantias;
		dbCommand.Parameters["@userID"].Value = userID;
		dbCommand.Parameters["@valordegarantias"].Value = valordegarantias;
		dbCommand.Parameters["@testigo1"].Value = testigo1;
		dbCommand.Parameters["@testigo2"].Value = testigo2;
		dbCommand.Parameters["@aval1"].Value = aval1;
		dbCommand.Parameters["@aval2"].Value = aval2;
		dbCommand.Parameters["@storeTS"].Value = storeTS;
		dbCommand.Parameters["@updateTS"].Value = updateTS;
		dbCommand.Parameters["@fecha"].Value = fecha;
		dbCommand.Parameters["@statusID"].Value = statusID;
		dbCommand.Parameters["@hectAseguradas"].Value = hectAseguradas;
		dbCommand.Parameters["@descParcelas"].Value = descParcelas;
		dbCommand.Parameters["@costoTotalSeguro"].Value = costoTotalSeguro;
		dbCommand.Parameters["@aval1Dom"].Value = aval1Dom;
		dbCommand.Parameters["@aval2Dom"].Value = aval2Dom;
		dbCommand.Parameters["@ubicacionGarantia"].Value = ubicacionGarantia;
		dbCommand.Parameters["@otrosPasivosMonto"].Value = otrosPasivosMonto;
		dbCommand.Parameters["@otrosPasivosAQuienLeDebe"].Value = otrosPasivosAQuienLeDebe;
		dbCommand.Parameters["@ingNetosAnualOtrosCultivos"].Value = ingNetosAnualOtrosCultivos;
		dbCommand.Parameters["@ingNetosAnualGanaderia"].Value = ingNetosAnualGanaderia;
		dbCommand.Parameters["@ingNetosComercioServicios"].Value = ingNetosComercioServicios;
		dbCommand.Parameters["@casaHabitacion"].Value = casaHabitacion;
		dbCommand.Parameters["@rastra"].Value = rastra;
		dbCommand.Parameters["@arado"].Value = arado;
		dbCommand.Parameters["@cultivadora"].Value = cultivadora;
		dbCommand.Parameters["@subsuelo"].Value = subsuelo;
		dbCommand.Parameters["@tractor"].Value = tractor;
		dbCommand.Parameters["@sembradora"].Value = sembradora;
		dbCommand.Parameters["@camioneta"].Value = camioneta;
		dbCommand.Parameters["@otrosActivos"].Value = otrosActivos;
		dbCommand.Parameters["@totalActivos"].Value = totalActivos;
		dbCommand.Parameters["@garantiaLiquida"].Value = garantiaLiquida;
		dbCommand.Parameters["@conceptoSoporteGarantia"].Value = conceptoSoporteGarantia;
		dbCommand.Parameters["@montoSoporteGarantia"].Value = montoSoporteGarantia;
		dbCommand.Parameters["@domicilioDelDeposito"].Value = domicilioDelDeposito;
		dbCommand.Parameters["@firmaAutorizada1"].Value = firmaAutorizada1;
		dbCommand.Parameters["@firmaAutorizada2"].Value = firmaAutorizada2;
		dbCommand.Parameters["@firmaAutorizada3"].Value = firmaAutorizada3;
		dbCommand.Parameters["@firmaAutorizada4"].Value = firmaAutorizada4;
		dbCommand.Parameters["@firmaAutorizada5"].Value = firmaAutorizada5;
		dbCommand.Parameters["@ejido"].Value = ejido;
		
		if (transaction == null)
		db.ExecuteNonQuery(dbCommand);
		else
		db.ExecuteNonQuery(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
	public static void Update(System.Int32? solicitudID, System.Int32? creditoID, System.Int32? productorID, System.Int32? experiencia, System.Double? monto, System.Double? superficieFinanciada, System.Double? superficieasembrar, System.Int32? plazo, System.Double? recursosPropios, System.String descripciondegarantias, System.Int32? userID, System.Double? valordegarantias, System.String testigo1, System.String testigo2, System.String aval1, System.String aval2, System.DateTime? storeTS, System.DateTime? updateTS, System.DateTime? fecha, System.Int32? statusID, System.Double? hectAseguradas, System.String descParcelas, System.Double? costoTotalSeguro, System.String aval1Dom, System.String aval2Dom, System.String ubicacionGarantia, System.Double? otrosPasivosMonto, System.String otrosPasivosAQuienLeDebe, System.Double? ingNetosAnualOtrosCultivos, System.Double? ingNetosAnualGanaderia, System.Double? ingNetosComercioServicios, System.Int32? casaHabitacion, System.Int32? rastra, System.Int32? arado, System.Int32? cultivadora, System.Int32? subsuelo, System.Int32? tractor, System.Int32? sembradora, System.Int32? camioneta, System.Double? otrosActivos, System.Double? totalActivos, System.Double? garantiaLiquida, System.String conceptoSoporteGarantia, System.Double? montoSoporteGarantia, System.String domicilioDelDeposito, System.String firmaAutorizada1, System.String firmaAutorizada2, System.String firmaAutorizada3, System.String firmaAutorizada4, System.String firmaAutorizada5, System.String ejido)
	{
		Update(solicitudID, creditoID, productorID, experiencia, monto, superficieFinanciada, superficieasembrar, plazo, recursosPropios, descripciondegarantias, userID, valordegarantias, testigo1, testigo2, aval1, aval2, storeTS, updateTS, fecha, statusID, hectAseguradas, descParcelas, costoTotalSeguro, aval1Dom, aval2Dom, ubicacionGarantia, otrosPasivosMonto, otrosPasivosAQuienLeDebe, ingNetosAnualOtrosCultivos, ingNetosAnualGanaderia, ingNetosComercioServicios, casaHabitacion, rastra, arado, cultivadora, subsuelo, tractor, sembradora, camioneta, otrosActivos, totalActivos, garantiaLiquida, conceptoSoporteGarantia, montoSoporteGarantia, domicilioDelDeposito, firmaAutorizada1, firmaAutorizada2, firmaAutorizada3, firmaAutorizada4, firmaAutorizada5, ejido, null);
	}
	
	public static void Update(Solicitudes solicitudes)
	{
		solicitudes.Update();
	}
	
	public static void Update(Solicitudes solicitudes, DbTransaction transaction)
	{
		solicitudes.Update(transaction);
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
		sqlCommand = "[dbo].gspSolicitudes_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@solicitudID"].SourceColumn = "solicitudID";
		dbCommand.Parameters["@creditoID"].SourceColumn = "creditoID";
		dbCommand.Parameters["@productorID"].SourceColumn = "productorID";
		dbCommand.Parameters["@experiencia"].SourceColumn = "Experiencia";
		dbCommand.Parameters["@monto"].SourceColumn = "Monto";
		dbCommand.Parameters["@superficieFinanciada"].SourceColumn = "superficieFinanciada";
		dbCommand.Parameters["@superficieasembrar"].SourceColumn = "Superficieasembrar";
		dbCommand.Parameters["@plazo"].SourceColumn = "Plazo";
		dbCommand.Parameters["@recursosPropios"].SourceColumn = "RecursosPropios";
		dbCommand.Parameters["@descripciondegarantias"].SourceColumn = "Descripciondegarantias";
		dbCommand.Parameters["@userID"].SourceColumn = "userID";
		dbCommand.Parameters["@valordegarantias"].SourceColumn = "Valordegarantias";
		dbCommand.Parameters["@testigo1"].SourceColumn = "testigo1";
		dbCommand.Parameters["@testigo2"].SourceColumn = "testigo2";
		dbCommand.Parameters["@aval1"].SourceColumn = "aval1";
		dbCommand.Parameters["@aval2"].SourceColumn = "aval2";
		dbCommand.Parameters["@storeTS"].SourceColumn = "storeTS";
		dbCommand.Parameters["@updateTS"].SourceColumn = "updateTS";
		dbCommand.Parameters["@fecha"].SourceColumn = "fecha";
		dbCommand.Parameters["@statusID"].SourceColumn = "statusID";
		dbCommand.Parameters["@hectAseguradas"].SourceColumn = "hectAseguradas";
		dbCommand.Parameters["@descParcelas"].SourceColumn = "descParcelas";
		dbCommand.Parameters["@costoTotalSeguro"].SourceColumn = "CostoTotalSeguro";
		dbCommand.Parameters["@aval1Dom"].SourceColumn = "Aval1Dom";
		dbCommand.Parameters["@aval2Dom"].SourceColumn = "Aval2Dom";
		dbCommand.Parameters["@ubicacionGarantia"].SourceColumn = "UbicacionGarantia";
		dbCommand.Parameters["@otrosPasivosMonto"].SourceColumn = "otrosPasivosMonto";
		dbCommand.Parameters["@otrosPasivosAQuienLeDebe"].SourceColumn = "otrosPasivosAQuienLeDebe";
		dbCommand.Parameters["@ingNetosAnualOtrosCultivos"].SourceColumn = "ingNetosAnualOtrosCultivos";
		dbCommand.Parameters["@ingNetosAnualGanaderia"].SourceColumn = "ingNetosAnualGanaderia";
		dbCommand.Parameters["@ingNetosComercioServicios"].SourceColumn = "ingNetosComercioServicios";
		dbCommand.Parameters["@casaHabitacion"].SourceColumn = "casaHabitacion";
		dbCommand.Parameters["@rastra"].SourceColumn = "rastra";
		dbCommand.Parameters["@arado"].SourceColumn = "Arado";
		dbCommand.Parameters["@cultivadora"].SourceColumn = "Cultivadora";
		dbCommand.Parameters["@subsuelo"].SourceColumn = "Subsuelo";
		dbCommand.Parameters["@tractor"].SourceColumn = "tractor";
		dbCommand.Parameters["@sembradora"].SourceColumn = "sembradora";
		dbCommand.Parameters["@camioneta"].SourceColumn = "camioneta";
		dbCommand.Parameters["@otrosActivos"].SourceColumn = "otrosActivos";
		dbCommand.Parameters["@totalActivos"].SourceColumn = "totalActivos";
		dbCommand.Parameters["@garantiaLiquida"].SourceColumn = "garantiaLiquida";
		dbCommand.Parameters["@conceptoSoporteGarantia"].SourceColumn = "ConceptoSoporteGarantia";
		dbCommand.Parameters["@montoSoporteGarantia"].SourceColumn = "montoSoporteGarantia";
		dbCommand.Parameters["@domicilioDelDeposito"].SourceColumn = "domicilioDelDeposito";
		dbCommand.Parameters["@firmaAutorizada1"].SourceColumn = "firmaAutorizada1";
		dbCommand.Parameters["@firmaAutorizada2"].SourceColumn = "firmaAutorizada2";
		dbCommand.Parameters["@firmaAutorizada3"].SourceColumn = "firmaAutorizada3";
		dbCommand.Parameters["@firmaAutorizada4"].SourceColumn = "firmaAutorizada4";
		dbCommand.Parameters["@firmaAutorizada5"].SourceColumn = "firmaAutorizada5";
		dbCommand.Parameters["@ejido"].SourceColumn = "ejido";
		
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
	public static void Delete(System.Int32? solicitudID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspSolicitudes_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, solicitudID);
		
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
	public static void Delete(System.Int32? solicitudID)
	{
		Delete(
		solicitudID);
	}
	
	/// <summary>
	/// Delete current Solicitudes from database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Delete(DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspSolicitudes_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, SolicitudID);
		
		// Execute.
		if (transaction != null)
		{
			db.ExecuteNonQuery(dbCommand, transaction);
		}
		else
		{
			db.ExecuteNonQuery(dbCommand);
		}
		this.SolicitudID = null;
	}
	
	/// <summary>
	/// Delete current Solicitudes from database.
	/// </summary>
	public void Delete()
	{
		this.Delete((DbTransaction)null);
	}
	
	#endregion
	
	
	#region SEARCH
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static Solicitudes[] Search(System.Int32? solicitudID, System.Int32? creditoID, System.Int32? productorID, System.Int32? experiencia, System.Double? monto, System.Double? superficieFinanciada, System.Double? superficieasembrar, System.Int32? plazo, System.Double? recursosPropios, System.String descripciondegarantias, System.Int32? userID, System.Double? valordegarantias, System.String testigo1, System.String testigo2, System.String aval1, System.String aval2, System.DateTime? storeTS, System.DateTime? updateTS, System.DateTime? fecha, System.Int32? statusID, System.Double? hectAseguradas, System.String descParcelas, System.Double? costoTotalSeguro, System.String aval1Dom, System.String aval2Dom, System.String ubicacionGarantia, System.Double? otrosPasivosMonto, System.String otrosPasivosAQuienLeDebe, System.Double? ingNetosAnualOtrosCultivos, System.Double? ingNetosAnualGanaderia, System.Double? ingNetosComercioServicios, System.Int32? casaHabitacion, System.Int32? rastra, System.Int32? arado, System.Int32? cultivadora, System.Int32? subsuelo, System.Int32? tractor, System.Int32? sembradora, System.Int32? camioneta, System.Double? otrosActivos, System.Double? totalActivos, System.Double? garantiaLiquida, System.String conceptoSoporteGarantia, System.Double? montoSoporteGarantia, System.String domicilioDelDeposito, System.String firmaAutorizada1, System.String firmaAutorizada2, System.String firmaAutorizada3, System.String firmaAutorizada4, System.String firmaAutorizada5, System.String ejido)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspSolicitudes_SEARCH";
		dbCommand = db.GetStoredProcCommand(sqlCommand, solicitudID, creditoID, productorID, experiencia, monto, superficieFinanciada, superficieasembrar, plazo, recursosPropios, descripciondegarantias, userID, valordegarantias, testigo1, testigo2, aval1, aval2, storeTS, updateTS, fecha, statusID, hectAseguradas, descParcelas, costoTotalSeguro, aval1Dom, aval2Dom, ubicacionGarantia, otrosPasivosMonto, otrosPasivosAQuienLeDebe, ingNetosAnualOtrosCultivos, ingNetosAnualGanaderia, ingNetosComercioServicios, casaHabitacion, rastra, arado, cultivadora, subsuelo, tractor, sembradora, camioneta, otrosActivos, totalActivos, garantiaLiquida, conceptoSoporteGarantia, montoSoporteGarantia, domicilioDelDeposito, firmaAutorizada1, firmaAutorizada2, firmaAutorizada3, firmaAutorizada4, firmaAutorizada5, ejido);
		
		ds = db.ExecuteDataSet(dbCommand);
		ds.Tables[0].TableName = TABLE_NAME;
		return Solicitudes.MapFrom(ds);
	}
	
	
	public static Solicitudes[] Search(Solicitudes searchObject)
	{
		return Search ( searchObject.SolicitudID, searchObject.CreditoID, searchObject.ProductorID, searchObject.Experiencia, searchObject.Monto, searchObject.SuperficieFinanciada, searchObject.Superficieasembrar, searchObject.Plazo, searchObject.RecursosPropios, searchObject.Descripciondegarantias, searchObject.UserID, searchObject.Valordegarantias, searchObject.Testigo1, searchObject.Testigo2, searchObject.Aval1, searchObject.Aval2, searchObject.StoreTS, searchObject.UpdateTS, searchObject.Fecha, searchObject.StatusID, searchObject.HectAseguradas, searchObject.DescParcelas, searchObject.CostoTotalSeguro, searchObject.Aval1Dom, searchObject.Aval2Dom, searchObject.UbicacionGarantia, searchObject.OtrosPasivosMonto, searchObject.OtrosPasivosAQuienLeDebe, searchObject.IngNetosAnualOtrosCultivos, searchObject.IngNetosAnualGanaderia, searchObject.IngNetosComercioServicios, searchObject.CasaHabitacion, searchObject.Rastra, searchObject.Arado, searchObject.Cultivadora, searchObject.Subsuelo, searchObject.Tractor, searchObject.Sembradora, searchObject.Camioneta, searchObject.OtrosActivos, searchObject.TotalActivos, searchObject.GarantiaLiquida, searchObject.ConceptoSoporteGarantia, searchObject.MontoSoporteGarantia, searchObject.DomicilioDelDeposito, searchObject.FirmaAutorizada1, searchObject.FirmaAutorizada2, searchObject.FirmaAutorizada3, searchObject.FirmaAutorizada4, searchObject.FirmaAutorizada5, searchObject.Ejido);
	}
	
	/// <summary>
	/// Returns all Solicitudes objects.
	/// </summary>
	/// <returns>List of all Solicitudes objects. </returns>
	[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
	public static Solicitudes[] Search()
	{
		return Search ( null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
	}
	
	#endregion
	
	
	#endregion
	
	
	#endregion
	
	
}

