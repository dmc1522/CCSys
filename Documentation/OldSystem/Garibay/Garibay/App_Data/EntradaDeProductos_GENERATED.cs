/****************************************************************************/
/* Code Author (written by Xin Zhao)                                        */
/*                                                                          */
/* This file was automatically generated using Code Author.                 */
/* Any manual changes to this file will be overwritten by a automated tool. */
/*                                                                          */
/* Date Generated: 16/06/2011 09:00:43 p.m.                                    */
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
public partial class EntradaDeProductos
{
	
	
	#region Constants
	private static readonly string TABLE_NAME = "[dbo].[EntradaDeProductos]";
	
	#endregion
	
	
	#region Fields
	private System.Int32? _entradaprodID;
	private System.Int32? _productoID;
	private System.Int32? _bodegaID;
	private System.Int32? _tipoMovProdID;
	private System.DateTime? _fecha;
	private System.Double? _cantidad;
	private System.String _observaciones;
	private System.Int32? _userID;
	private System.DateTime? _storeTS;
	private System.DateTime? _updateTS;
	private System.Int32? _cicloID;
	private System.Double? _preciocompra;
	private System.Double? _sacos;
	
	#endregion
	
	
	#region Properties
	public System.Int32? EntradaprodID
	{
		get
		{
			return _entradaprodID;
		}
		set
		{
			_entradaprodID = value;
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
	
	public System.Int32? TipoMovProdID
	{
		get
		{
			return _tipoMovProdID;
		}
		set
		{
			_tipoMovProdID = value;
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
	
	public System.Double? Cantidad
	{
		get
		{
			return _cantidad;
		}
		set
		{
			_cantidad = value;
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
	
	public System.Double? Preciocompra
	{
		get
		{
			return _preciocompra;
		}
		set
		{
			_preciocompra = value;
		}
	}
	
	public System.Double? Sacos
	{
		get
		{
			return _sacos;
		}
		set
		{
			_sacos = value;
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
		
		ds.Tables[TABLE_NAME].Columns.Add("entradaprodID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("productoID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("bodegaID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("tipoMovProdID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("Fecha", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("cantidad", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("observaciones", typeof(System.String) );
		ds.Tables[TABLE_NAME].Columns.Add("userID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("storeTS", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("updateTS", typeof(System.DateTime) );
		ds.Tables[TABLE_NAME].Columns.Add("cicloID", typeof(System.Int32) );
		ds.Tables[TABLE_NAME].Columns.Add("preciocompra", typeof(System.Double) );
		ds.Tables[TABLE_NAME].Columns.Add("sacos", typeof(System.Double) );
		
		dr = ds.Tables[TABLE_NAME].NewRow();
		
		if (EntradaprodID == null)
		dr["entradaprodID"] = DBNull.Value;
		else
		dr["entradaprodID"] = EntradaprodID;
		
		if (ProductoID == null)
		dr["productoID"] = DBNull.Value;
		else
		dr["productoID"] = ProductoID;
		
		if (BodegaID == null)
		dr["bodegaID"] = DBNull.Value;
		else
		dr["bodegaID"] = BodegaID;
		
		if (TipoMovProdID == null)
		dr["tipoMovProdID"] = DBNull.Value;
		else
		dr["tipoMovProdID"] = TipoMovProdID;
		
		if (Fecha == null)
		dr["Fecha"] = DBNull.Value;
		else
		dr["Fecha"] = Fecha;
		
		if (Cantidad == null)
		dr["cantidad"] = DBNull.Value;
		else
		dr["cantidad"] = Cantidad;
		
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
		
		if (CicloID == null)
		dr["cicloID"] = DBNull.Value;
		else
		dr["cicloID"] = CicloID;
		
		if (Preciocompra == null)
		dr["preciocompra"] = DBNull.Value;
		else
		dr["preciocompra"] = Preciocompra;
		
		if (Sacos == null)
		dr["sacos"] = DBNull.Value;
		else
		dr["sacos"] = Sacos;
		
		
		ds.Tables[TABLE_NAME].Rows.Add(dr);
		
	}
	
	protected void MapFrom(DataRow dr)
	{
		EntradaprodID = dr["entradaprodID"] != DBNull.Value ? Convert.ToInt32(dr["entradaprodID"]) : EntradaprodID = null;
		ProductoID = dr["productoID"] != DBNull.Value ? Convert.ToInt32(dr["productoID"]) : ProductoID = null;
		BodegaID = dr["bodegaID"] != DBNull.Value ? Convert.ToInt32(dr["bodegaID"]) : BodegaID = null;
		TipoMovProdID = dr["tipoMovProdID"] != DBNull.Value ? Convert.ToInt32(dr["tipoMovProdID"]) : TipoMovProdID = null;
		Fecha = dr["Fecha"] != DBNull.Value ? Convert.ToDateTime(dr["Fecha"]) : Fecha = null;
		Cantidad = dr["cantidad"] != DBNull.Value ? Convert.ToDouble(dr["cantidad"]) : Cantidad = null;
		Observaciones = dr["observaciones"] != DBNull.Value ? Convert.ToString(dr["observaciones"]) : Observaciones = null;
		UserID = dr["userID"] != DBNull.Value ? Convert.ToInt32(dr["userID"]) : UserID = null;
		StoreTS = dr["storeTS"] != DBNull.Value ? Convert.ToDateTime(dr["storeTS"]) : StoreTS = null;
		UpdateTS = dr["updateTS"] != DBNull.Value ? Convert.ToDateTime(dr["updateTS"]) : UpdateTS = null;
		CicloID = dr["cicloID"] != DBNull.Value ? Convert.ToInt32(dr["cicloID"]) : CicloID = null;
		Preciocompra = dr["preciocompra"] != DBNull.Value ? Convert.ToDouble(dr["preciocompra"]) : Preciocompra = null;
		Sacos = dr["sacos"] != DBNull.Value ? Convert.ToDouble(dr["sacos"]) : Sacos = null;
	}
	
	public static EntradaDeProductos[] MapFrom(DataSet ds)
	{
		List<EntradaDeProductos> objects;
		
		
		// Initialise Collection.
		objects = new List<EntradaDeProductos>();
		
		// Validation.
		if (ds == null)
		throw new ApplicationException("Cannot map to dataset null.");
		else if (ds.Tables[TABLE_NAME].Rows.Count == 0)
		return objects.ToArray();
		
		if (ds.Tables[TABLE_NAME] == null)
		throw new ApplicationException("Cannot find table [dbo].[EntradaDeProductos] in DataSet.");
		
		if (ds.Tables[TABLE_NAME].Rows.Count < 1)
		throw new ApplicationException("Table [dbo].[EntradaDeProductos] is empty.");
		
		// Map DataSet to Instance.
		foreach (DataRow dr in ds.Tables[TABLE_NAME].Rows)
		{
			EntradaDeProductos instance = new EntradaDeProductos();
			instance.MapFrom(dr);
			objects.Add(instance);
		}
		
		// Return collection.
		return objects.ToArray();
	}
	
	
	#endregion
	
	
	#region CRUD Methods
	
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static EntradaDeProductos Get(System.Int32 entradaprodID)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		EntradaDeProductos instance;
		
		
		instance = new EntradaDeProductos();
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspEntradaDeProductos_SELECT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, entradaprodID);
		
		// Get results.
		ds = db.ExecuteDataSet(dbCommand);
		// Verification.
		if (ds == null || ds.Tables[0].Rows.Count == 0) throw new ApplicationException("Could not get EntradaDeProductos ID:" + entradaprodID.ToString()+ " from Database.");
		// Return results.
		ds.Tables[0].TableName = TABLE_NAME;
		
		instance.MapFrom( ds.Tables[0].Rows[0] );
		return instance;
	}
	
	#region INSERT
	public void Insert(System.Int32? productoID, System.Int32? bodegaID, System.Int32? tipoMovProdID, System.DateTime? fecha, System.Double? cantidad, System.String observaciones, System.Int32? userID, System.DateTime? storeTS, System.DateTime? updateTS, System.Int32? cicloID, System.Double? preciocompra, System.Double? sacos, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspEntradaDeProductos_INSERT";
		dbCommand = db.GetStoredProcCommand(sqlCommand, productoID, bodegaID, tipoMovProdID, fecha, cantidad, observaciones, userID, storeTS, updateTS, cicloID, preciocompra, sacos);
		
		if (transaction == null)
		this.EntradaprodID = Convert.ToInt32(db.ExecuteScalar(dbCommand));
		else
		this.EntradaprodID = Convert.ToInt32(db.ExecuteScalar(dbCommand, transaction));
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
	public void Insert(System.Int32? productoID, System.Int32? bodegaID, System.Int32? tipoMovProdID, System.DateTime? fecha, System.Double? cantidad, System.String observaciones, System.Int32? userID, System.DateTime? storeTS, System.DateTime? updateTS, System.Int32? cicloID, System.Double? preciocompra, System.Double? sacos)
	{
		Insert(productoID, bodegaID, tipoMovProdID, fecha, cantidad, observaciones, userID, storeTS, updateTS, cicloID, preciocompra, sacos, null);
	}
	/// <summary>
	/// Insert current EntradaDeProductos to database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Insert(DbTransaction transaction)
	{
		Insert(ProductoID, BodegaID, TipoMovProdID, Fecha, Cantidad, Observaciones, UserID, StoreTS, UpdateTS, CicloID, Preciocompra, Sacos, transaction);
	}
	
	/// <summary>
	/// Insert current EntradaDeProductos to database.
	/// </summary>
	public void Insert()
	{
		this.Insert((DbTransaction)null);
	}
	#endregion
	
	
	#region UPDATE
	public static void Update(System.Int32? entradaprodID, System.Int32? productoID, System.Int32? bodegaID, System.Int32? tipoMovProdID, System.DateTime? fecha, System.Double? cantidad, System.String observaciones, System.Int32? userID, System.DateTime? storeTS, System.DateTime? updateTS, System.Int32? cicloID, System.Double? preciocompra, System.Double? sacos, DbTransaction transaction)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspEntradaDeProductos_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@entradaprodID"].Value = entradaprodID;
		dbCommand.Parameters["@productoID"].Value = productoID;
		dbCommand.Parameters["@bodegaID"].Value = bodegaID;
		dbCommand.Parameters["@tipoMovProdID"].Value = tipoMovProdID;
		dbCommand.Parameters["@fecha"].Value = fecha;
		dbCommand.Parameters["@cantidad"].Value = cantidad;
		dbCommand.Parameters["@observaciones"].Value = observaciones;
		dbCommand.Parameters["@userID"].Value = userID;
		dbCommand.Parameters["@storeTS"].Value = storeTS;
		dbCommand.Parameters["@updateTS"].Value = updateTS;
		dbCommand.Parameters["@cicloID"].Value = cicloID;
		dbCommand.Parameters["@preciocompra"].Value = preciocompra;
		dbCommand.Parameters["@sacos"].Value = sacos;
		
		if (transaction == null)
		db.ExecuteNonQuery(dbCommand);
		else
		db.ExecuteNonQuery(dbCommand, transaction);
		return;
	}
	
	[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
	public static void Update(System.Int32? entradaprodID, System.Int32? productoID, System.Int32? bodegaID, System.Int32? tipoMovProdID, System.DateTime? fecha, System.Double? cantidad, System.String observaciones, System.Int32? userID, System.DateTime? storeTS, System.DateTime? updateTS, System.Int32? cicloID, System.Double? preciocompra, System.Double? sacos)
	{
		Update(entradaprodID, productoID, bodegaID, tipoMovProdID, fecha, cantidad, observaciones, userID, storeTS, updateTS, cicloID, preciocompra, sacos, null);
	}
	
	public static void Update(EntradaDeProductos entradaDeProductos)
	{
		entradaDeProductos.Update();
	}
	
	public static void Update(EntradaDeProductos entradaDeProductos, DbTransaction transaction)
	{
		entradaDeProductos.Update(transaction);
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
		sqlCommand = "[dbo].gspEntradaDeProductos_UPDATE";
		dbCommand = db.GetStoredProcCommand(sqlCommand);
		db.DiscoverParameters(dbCommand);
		dbCommand.Parameters["@entradaprodID"].SourceColumn = "entradaprodID";
		dbCommand.Parameters["@productoID"].SourceColumn = "productoID";
		dbCommand.Parameters["@bodegaID"].SourceColumn = "bodegaID";
		dbCommand.Parameters["@tipoMovProdID"].SourceColumn = "tipoMovProdID";
		dbCommand.Parameters["@fecha"].SourceColumn = "Fecha";
		dbCommand.Parameters["@cantidad"].SourceColumn = "cantidad";
		dbCommand.Parameters["@observaciones"].SourceColumn = "observaciones";
		dbCommand.Parameters["@userID"].SourceColumn = "userID";
		dbCommand.Parameters["@storeTS"].SourceColumn = "storeTS";
		dbCommand.Parameters["@updateTS"].SourceColumn = "updateTS";
		dbCommand.Parameters["@cicloID"].SourceColumn = "cicloID";
		dbCommand.Parameters["@preciocompra"].SourceColumn = "preciocompra";
		dbCommand.Parameters["@sacos"].SourceColumn = "sacos";
		
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
	public static void Delete(System.Int32? entradaprodID, DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspEntradaDeProductos_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, entradaprodID);
		
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
	public static void Delete(System.Int32? entradaprodID)
	{
		Delete(
		entradaprodID);
	}
	
	/// <summary>
	/// Delete current EntradaDeProductos from database.
	/// </summary>
	/// <param name="transaction">optional SQL Transaction</param>
	public void Delete(DbTransaction transaction)
	{
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspEntradaDeProductos_DELETE";
		dbCommand = db.GetStoredProcCommand(sqlCommand, EntradaprodID);
		
		// Execute.
		if (transaction != null)
		{
			db.ExecuteNonQuery(dbCommand, transaction);
		}
		else
		{
			db.ExecuteNonQuery(dbCommand);
		}
		this.EntradaprodID = null;
	}
	
	/// <summary>
	/// Delete current EntradaDeProductos from database.
	/// </summary>
	public void Delete()
	{
		this.Delete((DbTransaction)null);
	}
	
	#endregion
	
	
	#region SEARCH
	[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]
	public static EntradaDeProductos[] Search(System.Int32? entradaprodID, System.Int32? productoID, System.Int32? bodegaID, System.Int32? tipoMovProdID, System.DateTime? fecha, System.Double? cantidad, System.String observaciones, System.Int32? userID, System.DateTime? storeTS, System.DateTime? updateTS, System.Int32? cicloID, System.Double? preciocompra, System.Double? sacos)
	{
		DataSet ds;
		Database db;
		string sqlCommand;
		DbCommand dbCommand;
		
		
		db = DatabaseFactory.CreateDatabase();
		sqlCommand = "[dbo].gspEntradaDeProductos_SEARCH";
		dbCommand = db.GetStoredProcCommand(sqlCommand, entradaprodID, productoID, bodegaID, tipoMovProdID, fecha, cantidad, observaciones, userID, storeTS, updateTS, cicloID, preciocompra, sacos);
		
		ds = db.ExecuteDataSet(dbCommand);
		ds.Tables[0].TableName = TABLE_NAME;
		return EntradaDeProductos.MapFrom(ds);
	}
	
	
	public static EntradaDeProductos[] Search(EntradaDeProductos searchObject)
	{
		return Search ( searchObject.EntradaprodID, searchObject.ProductoID, searchObject.BodegaID, searchObject.TipoMovProdID, searchObject.Fecha, searchObject.Cantidad, searchObject.Observaciones, searchObject.UserID, searchObject.StoreTS, searchObject.UpdateTS, searchObject.CicloID, searchObject.Preciocompra, searchObject.Sacos);
	}
	
	/// <summary>
	/// Returns all EntradaDeProductos objects.
	/// </summary>
	/// <returns>List of all EntradaDeProductos objects. </returns>
	[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
	public static EntradaDeProductos[] Search()
	{
		return Search ( null, null, null, null, null, null, null, null, null, null, null, null, null);
	}
	
	#endregion
	
	
	#endregion
	
	
	#endregion
	
	
}

