/****************************************************************************/
/* Code Author (written by Xin Zhao)                                        */
/*                                                                          */
/* This file was automatically generated using Code Author.                 */
/* Any manual changes to this file will be overwritten by a automated tool. */
/*                                                                          */
/* Date Generated: 11/07/2011 10:21:35 p.m.                                    */
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
public partial class NotasDeCompra
{
	
	
	#region Constants
	private static readonly string TABLE_NAME = "[dbo].[NotasDeCompra]";
	
	#endregion
	
	
	#region Fields
	private System.Int32? _notadecompraID;
	private System.Int32? _proveedorID;
	private System.Int32? _cicloID;
	private System.DateTime? _fecha;
	private System.String _folio;
	private System.Boolean? _llevaIVA;
	private System.String _observaciones;
	private System.Int32? _userID;
	private System.DateTime? _storeTS;
	private System.DateTime? _updateTS;
	private System.DateTime? _fechapago;
	private System.Boolean? _pagada;
	private System.Int32? _tipomonedaID;
	
	#endregion
	
	
	#region Properties
	public System.Int32? NotadecompraID
	{
		get
		{
			return _notadecompraID;
		}
		set
		{
			_notadecompraID = value;
		}
	}
	
	public System.Int32? ProveedorID
	{
		get
		{
			return _proveedorID;
		}
		set
		{
			_proveedorID = value;
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
	
	public System.String Folio
	{
		get
		{
			return _folio;
		}
		set
		{
			_folio = value;
		}
	}
	
	public System.Boolean? LlevaIVA
	{
		get
		{
			return _llevaIVA;
		}
		set
		{
			_llevaIVA = value;
		}
	}
	
	public System.String Observaciones
	{
		get
		{
			return _observaciones;
		}
		set
		{
			_observaciones = value;
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
	
	public System.DateTime? Fechapago
	{
		get
		{
			return _fechapago;
		}
		set
		{
			_fechapago = value;
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
	
	public System.Int32? TipomonedaID
	{
		get
		{
			return _tipomonedaID;
		}
		set
		{
			_tipomonedaID = value;
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
		
		ds.Tables[TABLE_NAME].Columns.Add("notadecompraID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("proveedorID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("cicloID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("fecha", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("folio", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("llevaIVA", typeof(System.Boolean) );
		ds.Tables[TABLE_NAME].Columns.Add("observaciones", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("userID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("storeTS", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("updateTS", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("fechapago", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("pagada", typeof(System.Boolean) );
		ds.Tables[TABLE_NAME].Columns.Add("tipomonedaID", typeof(System.Int32) );
		
		dr = ds.Tables[TABLE_NAME].NewRow();
		
		if (NotadecompraID == null)
		dr["notadecompraID"] = DBNull.Value;
		else
		dr["notadecompraID"] = NotadecompraID;
		
		if (ProveedorID == null)
		dr["proveedorID"] = DBNull.Value;
		else
		dr["proveedorID"] = ProveedorID;
		
		if (CicloID == null)
		dr["cicloID"] = DBNull.Value;
		else
		dr["cicloID"] = CicloID;
		
		if (Fecha == null)
		dr["fecha"] = DBNull.Value;
		else
		dr["fecha"] = Fecha;
		
		if (Folio == null)
		dr["folio"] = DBNull.Value;
		else
		dr["folio"] = Folio;
		
		if (LlevaIVA == null)
		dr["llevaIVA"] = DBNull.Value;
		else
		dr["llevaIVA"] = LlevaIVA;
		
		if (Observaciones == null)
		dr["observaciones"] = DBNull.Value;
		else
		dr["observaciones"] = Observaciones;
		
		if (UserID == null)
		dr["userID"] = DBNull.Value;
		else
		dr["userID"] = UserID;
		
		if (StoreTS == null)
		dr["storeTS"] = DBNull.Value;
		else
		dr["storeTS"] = StoreTS;
		
		if (UpdateTS == null)
		dr["updateTS"] = DBNull.Value;
		else
		dr["updateTS"] = UpdateTS;
		
		if (Fechapago == null)
		dr["fechapago"] = DBNull.Value;
		else
		dr["fechapago"] = Fechapago;
		
		if (Pagada == null)
		dr["pagada"] = DBNull.Value;
		else
		dr["pagada"] = Pagada;
		
		if (TipomonedaID == null)
		dr["tipomonedaID"] = DBNull.Value;
		else
		dr["tipomonedaID"] = TipomonedaID;
		
		
		ds.Tables[TABLE_NAME].Rows.Add(dr);
		
	}
	
	protected void MapFrom(DataRow dr)
	{
		NotadecompraID = dr["notadecompraID"] != DBNull.Value ? Convert.ToInt32(dr["notadecompraID"]) : NotadecompraID = null;
		ProveedorID = dr["proveedorID"] != DBNull.Value ? Convert.ToInt32(dr["proveedorID"]) : ProveedorID = null;
		CicloID = dr["cicloID"] != DBNull.Value ? Convert.ToInt32(dr["cicloID"]) : CicloID = null;
		Fecha = dr["fecha"] != DBNull.Value ? Convert.ToDateTime(dr["fecha"]) : Fecha = null;
		Folio = dr["folio"] != DBNull.Value ? Convert.ToString(dr["folio"]) : Folio = null;
		LlevaIVA = dr["llevaIVA"] != DBNull.Value ? Convert.ToBoolean(dr["llevaIVA"]) : LlevaIVA = null;
		Observaciones = dr["observaciones"] != DBNull.Value ? Convert.ToString(dr["observaciones"]) : Observaciones = null;
		UserID = dr["userID"] != DBNull.Value ? Convert.ToInt32(dr["userID"]) : UserID = null;
		StoreTS = dr["storeTS"] != DBNull.Value ? Convert.ToDateTime(dr["storeTS"]) : StoreTS = null;
		UpdateTS = dr["updateTS"] != DBNull.Value ? Convert.ToDateTime(dr["updateTS"]) : UpdateTS = null;
		Fechapago = dr["fechapago"] != DBNull.Value ? Convert.ToDateTime(dr["fechapago"]) : Fechapago = null;
		Pagada = dr["pagada"] != DBNull.Value ? Convert.ToBoolean(dr["pagada"]) : Pagada = null;
		TipomonedaID = dr["tipomonedaID"] != DBNull.Value ? Convert.ToInt32(dr["tipomonedaID"]) : TipomonedaID = null;
	}
	
	public static NotasDeCompra[] MapFrom(DataSet ds)
	{
		List<NotasDeCompra> objects;
		
		
		// Initialise Collection.
		objects = new List<NotasDeCompra>();
		
		// Validation.
		if (ds == null)
		throw new ApplicationException("Cannot map to dataset null.");
		else if (ds.Tables[TABLE_NAME].Rows.Count == 0)
		return objects.ToArray();
		
		if (ds.Tables[TABLE_NAME] == null)
		throw new ApplicationException("Cannot find table [dbo].[NotasDeCompra] in DataSet.");
		
		if (ds.Tables[TABLE_NAME].Rows.Count < 1)
		throw new ApplicationException("Table [dbo].[NotasDeCompra] is empty.");
		
		// Map DataSet to Instance.
		foreach (DataRow dr in ds.Tables[TABLE_NAME].Rows)
		{
			NotasDeCompra instance = new NotasDeCompra();
			instance.MapFrom(dr);
			objects.Add(instance);
		}
		
		// Return collection.
		return objects.ToArray();
	}
	
	
	#endregion
	
	
	#region CRUD Methods
	
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static NotasDeCompra Get(System.Int32 notadecompraID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		NotasDeCompra instance;
		
		
		instance = new NotasDeCompra();
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspNotasDeCompra_SELECT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, notadecompraID);
		
		// Get results.
		ds = db.ExecuteDataSet(dbCommand);
		// Verification.
		if (ds == null || ds.Tables[0].Rows.Count == 0) throw new ApplicationException("Could not get NotasDeCompra ID:" + notadecompraID.ToString()+ " from Database.");
		// Return results.
		ds.Tables[0].TableName = TABLE_NAME;
		
		instance.MapFrom( ds.Tables[0].Rows[0] );
		return instance;
	}
	
	#region INSERT
	public void Insert(System.Int32? proveedorID, System.Int32? cicloID, System.DateTime? fecha, System.String folio, System.Boolean? llevaIVA, System.String observaciones, System.Int32? userID, System.DateTime? storeTS, System.DateTime? updateTS, System.DateTime? fechapago, System.Boolean? pagada, System.Int32? tipomonedaID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspNotasDeCompra_INSERT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, proveedorID, cicloID, fecha, folio, llevaIVA, observaciones, userID, storeTS, updateTS, fechapago, pagada, tipomonedaID);
		
		if (transaction == null)
		this.NotadecompraID = Convert.ToInt32(db.ExecuteScalar(dbCommand));
		else
		this.NotadecompraID = Convert.ToInt32(db.ExecuteScalar(dbCommand, transaction));
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
	public void Insert(System.Int32? proveedorID, System.Int32? cicloID, System.DateTime? fecha, System.String folio, System.Boolean? llevaIVA, System.String observaciones, System.Int32? userID, System.DateTime? storeTS, System.DateTime? updateTS, System.DateTime? fechapago, System.Boolean? pagada, System.Int32? tipomonedaID)
	{
		Insert(proveedorID, cicloID, fecha, folio, llevaIVA, observaciones, userID, storeTS, updateTS, fechapago, pagada, tipomonedaID, null);
	}
	/// <summary>
	/// Insert current NotasDeCompra to database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Insert(DbTransaction transaction)
	{
		Insert(ProveedorID, CicloID, Fecha, Folio, LlevaIVA, Observaciones, UserID, StoreTS, UpdateTS, Fechapago, Pagada, TipomonedaID, transaction);
	}
	
	/// <summary>
	/// Insert current NotasDeCompra to database.
	/// </summary>
	public void Insert()
	{
		this.Insert((DbTransaction)null);
	}
	#endregion
	
	
	#region UPDATE
	public static void Update(System.Int32? notadecompraID, System.Int32? proveedorID, System.Int32? cicloID, System.DateTime? fecha, System.String folio, System.Boolean? llevaIVA, System.String observaciones, System.Int32? userID, System.DateTime? storeTS, System.DateTime? updateTS, System.DateTime? fechapago, System.Boolean? pagada, System.Int32? tipomonedaID, DbTransaction transaction)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspNotasDeCompra_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@notadecompraID"].Value = notadecompraID;
		dbCommand.Parameters["@proveedorID"].Value = proveedorID;
		dbCommand.Parameters["@cicloID"].Value = cicloID;
		dbCommand.Parameters["@fecha"].Value = fecha;
		dbCommand.Parameters["@folio"].Value = folio;
		dbCommand.Parameters["@llevaIVA"].Value = llevaIVA;
		dbCommand.Parameters["@observaciones"].Value = observaciones;
		dbCommand.Parameters["@userID"].Value = userID;
		dbCommand.Parameters["@storeTS"].Value = storeTS;
		dbCommand.Parameters["@updateTS"].Value = updateTS;
		dbCommand.Parameters["@fechapago"].Value = fechapago;
		dbCommand.Parameters["@pagada"].Value = pagada;
		dbCommand.Parameters["@tipomonedaID"].Value = tipomonedaID;
		
		if (transaction == null)
		db.ExecuteNonQuery(dbCommand);
		else
		db.ExecuteNonQuery(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
	public static void Update(System.Int32? notadecompraID, System.Int32? proveedorID, System.Int32? cicloID, System.DateTime? fecha, System.String folio, System.Boolean? llevaIVA, System.String observaciones, System.Int32? userID, System.DateTime? storeTS, System.DateTime? updateTS, System.DateTime? fechapago, System.Boolean? pagada, System.Int32? tipomonedaID)
	{
		Update(notadecompraID, proveedorID, cicloID, fecha, folio, llevaIVA, observaciones, userID, storeTS, updateTS, fechapago, pagada, tipomonedaID, null);
	}
	
	public static void Update(NotasDeCompra notasDeCompra)
	{
		notasDeCompra.Update();
	}
	
	public static void Update(NotasDeCompra notasDeCompra, DbTransaction transaction)
	{
		notasDeCompra.Update(transaction);
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
		sqlCommand = "[dbo].gspNotasDeCompra_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@notadecompraID"].SourceColumn = "notadecompraID";
		dbCommand.Parameters["@proveedorID"].SourceColumn = "proveedorID";
		dbCommand.Parameters["@cicloID"].SourceColumn = "cicloID";
		dbCommand.Parameters["@fecha"].SourceColumn = "fecha";
		dbCommand.Parameters["@folio"].SourceColumn = "folio";
		dbCommand.Parameters["@llevaIVA"].SourceColumn = "llevaIVA";
		dbCommand.Parameters["@observaciones"].SourceColumn = "observaciones";
		dbCommand.Parameters["@userID"].SourceColumn = "userID";
		dbCommand.Parameters["@storeTS"].SourceColumn = "storeTS";
		dbCommand.Parameters["@updateTS"].SourceColumn = "updateTS";
		dbCommand.Parameters["@fechapago"].SourceColumn = "fechapago";
		dbCommand.Parameters["@pagada"].SourceColumn = "pagada";
		dbCommand.Parameters["@tipomonedaID"].SourceColumn = "tipomonedaID";
		
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
	public static void Delete(System.Int32? notadecompraID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspNotasDeCompra_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, notadecompraID);
		
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
	public static void Delete(System.Int32? notadecompraID)
	{
		Delete(
		notadecompraID);
	}
	
	/// <summary>
	/// Delete current NotasDeCompra from database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Delete(DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspNotasDeCompra_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, NotadecompraID);
		
		// Execute.
		if (transaction != null)
		{
			db.ExecuteNonQuery(dbCommand, transaction);
		}
		else
		{
			db.ExecuteNonQuery(dbCommand);
		}
		this.NotadecompraID = null;
	}
	
	/// <summary>
	/// Delete current NotasDeCompra from database.
	/// </summary>
	public void Delete()
	{
		this.Delete((DbTransaction)null);
	}
	
	#endregion
	
	
	#region SEARCH
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static NotasDeCompra[] Search(System.Int32? notadecompraID, System.Int32? proveedorID, System.Int32? cicloID, System.DateTime? fecha, System.String folio, System.Boolean? llevaIVA, System.String observaciones, System.Int32? userID, System.DateTime? storeTS, System.DateTime? updateTS, System.DateTime? fechapago, System.Boolean? pagada, System.Int32? tipomonedaID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspNotasDeCompra_SEARCH";
		dbCommand = db.GetStoredProcCommand(sqlCommand, notadecompraID, proveedorID, cicloID, fecha, folio, llevaIVA, observaciones, userID, storeTS, updateTS, fechapago, pagada, tipomonedaID);
		
		ds = db.ExecuteDataSet(dbCommand);
		ds.Tables[0].TableName = TABLE_NAME;
		return NotasDeCompra.MapFrom(ds);
	}
	
	
	public static NotasDeCompra[] Search(NotasDeCompra searchObject)
	{
		return Search ( searchObject.NotadecompraID, searchObject.ProveedorID, searchObject.CicloID, searchObject.Fecha, searchObject.Folio, searchObject.LlevaIVA, searchObject.Observaciones, searchObject.UserID, searchObject.StoreTS, searchObject.UpdateTS, searchObject.Fechapago, searchObject.Pagada, searchObject.TipomonedaID);
	}
	
	/// <summary>
	/// Returns all NotasDeCompra objects.
	/// </summary>
	/// <returns>List of all NotasDeCompra objects. </returns>
	[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
	public static NotasDeCompra[] Search()
	{
		return Search ( null, null, null, null, null, null, null, null, null, null, null, null, null);
	}
	
	#endregion
	
	
	#endregion
	
	
	#endregion
	
	
}

